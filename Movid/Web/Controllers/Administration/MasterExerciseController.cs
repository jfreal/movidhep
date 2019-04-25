using AutoMapper;
using Movid.App.Application;
using Movid.App.Infrastructure;
using Movid.App.Infrastructure.Raven;
using Movid.Shared.Model;
using Movid.Shared.PagedList;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Movid.App.Controllers.Administration
{

    public class MasterExerciseSearchResults
    {
        public StaticPagedList<MasterExercise> Results { get; set; }
        public string SearchTerm { get; set; }
        public int Page { get; set; }
        public SuggestionQueryResult SuggestionQueryResult { get; set; }
    }

    [Authorize]
    [MustHaveRole(Roles="Admin")]
    [RoutePrefix("administration")]
    public class MasterExerciseController : AppController
    {
        [Route("exercises")]
        public ActionResult List(string searchTerm, int? page)
        {
            if (!page.HasValue)
                page = 1;

            var size = 50;
            var skip = (page.Value - 1)*size;

            RavenQueryStatistics stats;
            IEnumerable<MasterExercise> exercises;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                exercises = RavenSession
                    .Query<MasterExercise>()
                    .Customize(x => x.WaitForNonStaleResults(TimeSpan.FromSeconds(5)))
                    .Statistics(out stats)
                    .OrderBy(x => x.Name)
                    .Skip((page.Value - 1)*size)
                    .Take(size);
            }
            else
            {
                    exercises = Queryable.Skip(RavenSession
                                                   .Query<MasterExerciseSearch.ReduceResult, MasterExerciseSearch>()
                                                   .Statistics(out stats)
                                                   .Where(x => x.Query.StartsWith(searchTerm) && x.Master)
                                                   .OrderBy(x => x.Query), skip)
                                         .Take(size)
                                         .OfType<MasterExercise>();
            }

            var exer = exercises.ToList();

            var paged = new StaticPagedList<MasterExercise>(exer, page.Value, 50, stats.TotalResults - stats.SkippedResults);

            var vm = new MasterExerciseSearchResults() { Results = paged, SearchTerm = searchTerm};

            return View("List", vm);
        }
        
        [Route("exercise/create")]
        public ActionResult Create()
        {
            return View(new ExerciseViewModel());
        }

        [Route("exercise/create")]
        [HttpPost]
        public ActionResult Create(ExerciseViewModel postedModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(postedModel);
                }

                var newExercise = new MasterExercise();
                newExercise.Name = postedModel.Name.Trim();
                newExercise.Description = postedModel.Description;
                newExercise.CreatedOn = DateTime.Now;
                
                string[] lines = postedModel.Categories.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                newExercise.Categories = new List<string>(lines.Where(x => !string.IsNullOrWhiteSpace(x)));
                
                if( !string.IsNullOrWhiteSpace(postedModel.VideoId))
                    newExercise.VideoId = postedModel.VideoId.Trim();
                
                RavenSession.Store(newExercise);
                RavenSession.SaveChanges();

                if (!string.IsNullOrWhiteSpace(postedModel.VideoId))
                {

                    var success = Thumbnailer.GenerateThumbs(newExercise.Id, newExercise.VideoId);
                    if (!success)
                    {
                        this.WarnUser("We couldn't generate a thumbnail image for this exercise.  The video id could be wrong or Vimeo may be not be " +
                                      "responding.  We'll try to generate the thumbnail again but it would be a good idea to double" +
                                      "check the video id. Sorry about that.");
                    }
                }

                HighFive("New master exercise created ok.");

                return RedirectToAction("List");
            }
            catch
            {
                return View(postedModel);
            }
        }

        [Route("exercise/edit/{id}")]
        public ActionResult Edit(int id)
        {
            var exercise = RavenSession.Load<MasterExercise>("masterexercises/" + id);

            if (!ApplicationAdministrator) return HttpNotFound();

            var vm = new ExerciseViewModel();

            Mapper.Map(exercise, vm);

            vm.Categories = string.Join("\r\n", exercise.Categories );
            

            return View(vm);
        }

        [Route("exercise/edit/{id}")]
        [HttpPost]
        public ActionResult Edit(ExerciseViewModel postedModel, string categories)
        {
            if (!ModelState.IsValid)
            {
                return View(postedModel);
            }
          
            var exr = RavenSession.Load<MasterExercise>("masterexercises/" + postedModel.Id);

            if (!ApplicationAdministrator) 
                return HttpNotFound();

            UpdateModel(exr);

            string[] lines = categories.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            exr.Categories = new List<string>(lines.Where(x=>!string.IsNullOrWhiteSpace(x)));

            exr.Name =  exr.Name.Trim();

            RavenSession.SaveChanges();
            HighFive("Master exercise edited ok.");

            return RedirectToAction("List");
        }

        [Route("exercise/delete/{id}")]
        public ActionResult Delete(int id)
        {
            
            var exr = RavenSession.Load<MasterExercise>("masterexercises/" + id);

            if (!Ownership.Owns(exr, this)) return HttpNotFound();

            RavenSession.Delete(exr);
            RavenSession.SaveChanges();

            HighFive("Master exercise deleted.");

            return RedirectToAction("List");
          
        }
       
        [Route("library/refresh-thumb")]
        public bool RefreshThumb(string exerciseId, string videoId)
        {
            var exr = RavenSession.Load<MasterExercise>("masterexercises/" + exerciseId);

            Thumbnailer.GenerateThumbs(exr.Id, exr.VideoId);

            RavenSession.SaveChanges();

            return true;
        }

        


    }
}
