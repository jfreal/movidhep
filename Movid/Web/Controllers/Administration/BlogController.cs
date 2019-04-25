using Movid.App.Application;
using Movid.Shared.Model;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Movid.App.Controllers.Administration
{
    [MustHaveRole(Roles = "Admin")]
    [RoutePrefix("administration/blog")]
    public class BlogController : BaseAdminController
    {
        [Route()]
        public ActionResult List()
        {
            var last10Posts = RavenSession.Query<BlogEntry>().Customize(x=>x.WaitForNonStaleResults(TimeSpan.FromSeconds(5))).OrderByDescending(x => x.Published).ToList();

            return View(last10Posts);
        }

        [Route("create")]
        public ActionResult Create()
        {
            return View(new BlogEntry());
        }

        [HttpPost]
        [Route("create")]
        public ActionResult Create(BlogEntry postedModel)
        {
            if (!ModelState.IsValid)
            {
                return View(new BlogEntry());
            }

            RavenSession.Store(postedModel);
            RavenSession.SaveChanges();

            HighFive("Blog post added");

            if (postedModel.AppLevel)
            {
             
            }

            return RedirectToAction("List");
        }

        [Route("edit/{blogId}")]
        public ActionResult Edit(int blogId)
        {
            var blogEntry = RavenSession.Load<BlogEntry>("blogentries/" + blogId);

            if (blogEntry == null)
            {
                return HttpNotFound();
            }

            return View(blogEntry);
        }

        [Route("edit/{blogId}")]
        [HttpPost]
        public ActionResult Edit(BlogEntry postedModel)
        {
            if (!ModelState.IsValid)
            {
                return View(postedModel);
            }

            var blogEntry = RavenSession.Load<BlogEntry>("blogentries/" + postedModel.Id);

            if (blogEntry == null)
            {
                return HttpNotFound();
            }

            UpdateModel(blogEntry);

            RavenSession.SaveChanges();
            
            HighFive("Blog post edited");

            return RedirectToAction("List");
        }

    }
}
