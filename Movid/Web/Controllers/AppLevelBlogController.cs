using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movid.Shared.Model;
using Movid.Web.Infrastructure;

namespace Movid.App.Controllers
{
    public class AppLevelBlogController : AppController
    {
        [Route("unread-updates")]
        public ActionResult UnreadUpdates(bool markAsRead = false)
        {
            if (markAsRead)
            {
                Account.LastNotificationsRead = DateTime.Now;
                RavenSession.SaveChanges();
                
                this.HighFive("All messages marked as read");
            }

            var unreadBlogs = RavenSession.Query<BlogEntry>().Where(x => x.AppLevel && x.Published > Account.LastNotificationsRead).ToList();

            return View(unreadBlogs);
        }

    }
}
