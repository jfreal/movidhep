using System;
using Movid.App.Application;
using Movid.App.Infrastructure;
using Movid.App.Infrastructure.Emails;
using Movid.App.Models;
using System.Linq;
using System.Web.Mvc;
using Movid.App.Models.ViewModels;
using Movid.Shared;
using System.Collections.Generic;

namespace Movid.App.Controllers
{
    [Authorize]
    public class ProgramBuilderController : AppController
    {
        [Route("program-builder")]
        public ActionResult Index()
        {
            return View("ProgramBuilder", new ProgramBuilderViewModel()
            {
                Clinic = Clinic, 
                User = LoggedInUser,
                Account = Account
            });
        }

        [Route("program-builder/program/{programId}")]
        public ActionResult LoadProgram(int programId)
        {
            var program = RavenSession.Load<Program>("programs/" + programId);

            if (program == null)
                return HttpNotFound("Program not found");

            if (!Ownership.Owns(program, this)) return HttpNotFound();

            return View("ProgramBuilder", new ProgramBuilderViewModel() { Clinic = Clinic, User = LoggedInUser, ExerciseSet = program });
        }

        [Route("program-builder/protocol/{protocolId}")]
        public ActionResult LoadProtocol(int protocolId)
        {
            var program = RavenSession.Query<Protocol>().FirstOrDefault(x => x.Id == protocolId && x.UserId == LoggedInUser.Id);

            if (program == null)
                return HttpNotFound("Protocol not found");

            if (!Ownership.Owns(program, this)) return HttpNotFound();

            return View("ProgramBuilder", new ProgramBuilderViewModel() { Clinic = Clinic, User = LoggedInUser, ExerciseSet = program });
        }

        [HttpPost]
        [Route("program-builder/edit/program")]
        public ActionResult EditProgram(Program postedProgram, bool? resend = false)
        {
            var program = RavenSession.Load<Program>("programs/" + postedProgram.Id);

            if (!Ownership.Owns(program, this)) return HttpNotFound();

            UpdateModel(program);

            RavenSession.SaveChanges();

            new ProgramEmailer(this).SendToPatient(program.Id, program.Email, program.ShortUrl);

            return Json(true);
        }


        [HttpPost]
        [Route("program-builder/edit/protocol")]
        public ActionResult EditProtocol(Protocol postedProgram)
        {
            var loadedProtocol = RavenSession.Load<Protocol>("protocols/" + postedProgram.Id);

            if (!Ownership.Owns(loadedProtocol, this)) return HttpNotFound();

            UpdateModel(loadedProtocol);

            RavenSession.SaveChanges();

            return Json(true);
        }

        [HttpPost]
        [Route("newprogram")]
        public ActionResult NewProgram(Program newProgram)
        {
            if (string.IsNullOrWhiteSpace(newProgram.Email))
                return Json(new { success = false });

            newProgram.ShortUrl = ShortUrl.Create();
            newProgram.Created = DateTime.Now;

            Ownership.Assign(newProgram, this);

            if (!LoggedInUser.OnboardingTasks.Contains(OnboardingTask.CreatedProgram.ToString()))
                LoggedInUser.OnboardingTasks.Add(OnboardingTask.CreatedProgram.ToString());

            RavenSession.Store(newProgram);
            RavenSession.SaveChanges();

            new Emailer(this).SendEmail(EmailEnum.SendToPatient, newProgram.Email, newProgram.ShortUrl, newProgram.Id);

            // new ProgramEmailer(this).SendToPatient(newProgram.Id, newProgram.Email, newProgram.ShortUrl);

            return Json(true);
        }

        [HttpPost]
        [Route("program-builder/protocol/new")]
        public ActionResult NewProtocol(Protocol postedProtocol)
        {
            postedProtocol.ShortUrl = ShortUrl.Create();

            postedProtocol.Created = DateTime.Now;

            Ownership.Assign(postedProtocol, this);

            RavenSession.Store(postedProtocol);
            RavenSession.SaveChanges();

            return Json(true);
        }
    }
}
