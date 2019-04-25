using Movid.App.Application;
using Movid.App.Models;
using Movid.Shared;
using Movid.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Movid.App.Controllers.Migration
{
    public class MasterCollectionMigrationController : AppController
    {
        //4/20/2014

        [Route("create-masters")]
        public ActionResult Index()
        {
            var exercises = RavenSession.Query<Exercise>().Where(x=>x.Master).Take(1024);

            RavenSession.Advanced.MaxNumberOfRequestsPerSession = 10000;
            foreach (var exer in exercises)
            {
                var newMaster = new MasterExercise();

                newMaster.AccountId = 0;
                newMaster.Name = exer.Name;
                newMaster.Categories = exer.Categories;
                newMaster.ClinicId = 0;
                newMaster.Description = exer.Description;
                newMaster.ExerciseDetails = exer.ExerciseDetails;
                newMaster.MasterExerciseId = exer.Id;
                newMaster.Master = true;
                newMaster.UserId = 0;
                newMaster.VideoId = exer.VideoId;
                newMaster.CreatedOn = DateTime.Now;

                RavenSession.Store(newMaster);
            }
            
            RavenSession.SaveChanges();

            return View();
        }

        [Route("account-exercise-status")]
        public bool Exer()
        {
            //DeleteOldExercises();


            var accounts = RavenSession.Query<Account>().Where(x => !x.ExercisesUpToDate);

            foreach (Account account in accounts)
            {
                var onboardProcess = new UserOnboardProcess(RavenSession);

                onboardProcess.CopyExercises(account.Id);

                account.LastMasterPush = DateTime.Now;
                account.ExercisesUpToDate = true;

                RavenSession.SaveChanges();
            }

                
            


            return true;
        }

        public void DeleteOldExercises()
        {


            var exercises = RavenSession.Query<Exercise>()
                            .Where(x => !x.Master ).Take(1024).ToList();

            foreach (var exercise in exercises)
            {
                RavenSession.Delete(exercise);
            }

            RavenSession.SaveChanges();
        }

        [Route("delete-old-attachments")]
        public void DailyFix()
        {
            
                MvcApplication.Store.DatabaseCommands.DeleteAttachment("smallthumb/0", null);
                MvcApplication.Store.DatabaseCommands.DeleteAttachment("largethumb/0", null);
            
        }

         [Route("delete-orphaned")]
        public void DeleteOphaned()
        {
            var accountIds = RavenSession.Query<User>().ToList().Select(x => x.AccountId).ToList();

            var exerciseAccountIds = RavenSession.Query<Exercise>().Where(x=>!x.Master).Select(x => x.AccountId).Distinct().ToList();

            var no = exerciseAccountIds.Except(accountIds).FirstOrDefault();

            var exercises = RavenSession.Query<Exercise>()
               .Where(x => !x.Master && x.AccountId == no).Take(1024).ToList();

            foreach (var exercise in exercises)
            {
                RavenSession.Delete(exercise);
            }

            RavenSession.SaveChanges();
        }

    }
}
