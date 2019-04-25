using System;
using System.Net;

using Movid.App.Application;
using Movid.App.Infrastructure;
using Movid.App.Infrastructure.Raven;
using Movid.App.Models;
using System.Linq;
using System.Web.Mvc;

namespace Movid.App.Controllers
{
    [Authorize]
    public class ProtocolController : AppController
    {
        [Route("protocols")]
        public ViewResult List()
        {
            var protocols = RavenSession.Query<Protocol>(typeof(ProtocolsByOwnershipAndByName).Name)
                .Where(x => x.UserId == LoggedInUser.Id).OrderByDescending(x => x.Created).ToList();

            return View(protocols);
        }

        [Route("protocol/{id}")]
        public JsonResult FindById(int id)
        {
            var protocol = RavenSession.Load<Protocol>("protocols/" + id);

            return Json(protocol, JsonRequestBehavior.AllowGet);
        }

        //[Route("protocol/automated/{id}")]
        //public ViewResult AutomatedMarketingProtocol(int id)
        //{
        //    var protocol = RavenSession.Query<Protocol>().FirstOrDefault(x => x.Group == LoggedInUser.Group && x.Id == id);

        //    return View(protocol);
        //}

        //[HttpPost]
        //[Route("protocol/automated/{id}")]
        //public ActionResult AutomatedMarketingProtocol(int id, MarketingPlan postedModel)
        //{
        //    var protocol = RavenSession.Query<Protocol>().FirstOrDefault(x => x.Group == LoggedInUser.Group && x.Id == id);

        //    protocol.MarketingPlan = postedModel;

        //    RavenSession.SaveChanges();

        //    HighFive("Marketing Program saved.");

        //    return RedirectToAction("List");
        //}

        [HttpPost]
        [Route("protocol/quick-send")]
        public ActionResult QuickSend(string email, int protocolId)
        {
            var protocol = RavenSession.Load<Protocol>("protocols/" + protocolId);

            if (string.IsNullOrWhiteSpace(email))
                return Json(new { success = false });

            if (protocol == null)
                return HttpNotFound();

            var newProgram = new Program(){Email = email};

            Ownership.Assign(newProgram, this);

            newProgram.Exercises = protocol.Exercises;
            newProgram.Greeting = Account.Settings.DefaultGreeting;
            
            
            newProgram.ShortUrl = ShortUrl.Create();
            newProgram.Created = DateTime.Now;

            RavenSession.Store(newProgram);
            RavenSession.SaveChanges();

            new ProgramEmailer(this).SendToPatient(newProgram.Id, newProgram.Email, newProgram.ShortUrl);

            var highFive = string.Format("Protocol {0} sent to {1}", protocol.Name, newProgram.Email);
            HighFive(highFive);

            return RedirectToAction("List");
        }


        [Route("protocols/delete/{protocolId}")]
        public ActionResult Delete(int protocolId)
        {
            var plan = RavenSession.Load<Protocol>("protocols/" + protocolId);

            if (plan == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Protocol not found");

            if (!Ownership.Owns(plan, this)) return HttpNotFound();

            if (LoggedInUser.ClinicIds.Contains(plan.ClinicId))
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Protocol not found");

            RavenSession.Delete(plan);
            RavenSession.SaveChanges();

            HighFive("Protocol deleted successfuly");

            return RedirectToAction("List");
        }

    }
}
