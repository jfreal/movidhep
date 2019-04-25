using Movid.App.Infrastructure;
using Movid.App.Application;
using Movid.App.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Movid.App.Controllers
{
    [Authorize]
    public class ProgramController : AppController
    {
        [Route("programs")]
        public ViewResult List()
        {
            var plan = RavenSession.Query<Program>().Where(x=>x.AccountId==LoggedInUser.AccountId).OrderByDescending(x=>x.Created).ToList();

            return View(plan);
        }

        //[HttpPost]
        //[Route("planovertime")]
        //public JsonResult PlanOverTime()
        //{
        //    var results = RavenSession.Query<UniqueProgramByDate>("UniquePlansByDate").Where(x => x.Group == LoggedInUser.Group).OrderBy(x=>x.Date).ToList();

        //    var graphData = (from r in results
                             
        //               select new long[]
        //                          {
        //                              r.Date.ToJavascriptTimestamp(),
        //                              r.Count
        //                          }).ToArray();


        //        return Json(graphData, JsonRequestBehavior.AllowGet);

        //}

        [Route("programs/delete/{programId}")]
        public ActionResult Delete(int programId)
        {
            var plan = RavenSession.Load<Program>("programs/" + programId);            

            if (plan == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Program not found");

            if (!Ownership.Owns(plan, this)) return HttpNotFound();

            if( LoggedInUser.Id != plan.UserId)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Program not found");

            RavenSession.Delete(plan);
            RavenSession.SaveChanges();

            HighFive("Program deleted successfuly");

            return RedirectToAction("List");
        }
        
        [Route("programs/{programId}/resend")]
        public ActionResult Resend(int programId)
        {
            var program = RavenSession.Load<Program>("programs/" + programId);
 
            new ProgramEmailer(this).SendToPatient(program.Id, program.Email, program.ShortUrl);

            HighFive("Program resent to " + program.Email);

            return RedirectToAction("List");
        }
    }
}
