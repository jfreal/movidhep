using Movid.App.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace Movid.App.Controllers
{
    public class EmailEventController : AppController
    {
        [Route("receive-sendgrid-event")]
        [HttpPost]
        public ActionResult Index(List<SendGridEmailEvent> postedModel)
        {
            foreach (var sendGridEmailEvent in postedModel)
            {
                RavenSession.Store(sendGridEmailEvent);

                var plan = RavenSession.Load<Program>("programs/" + sendGridEmailEvent.ProgramId);
                
                //sometimes a Program will be deleted
                if (plan == null)
                {
                    continue;
                }
                
                plan.EmailEvents.Add(sendGridEmailEvent);
            }

            RavenSession.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);    
        }

    }
}
