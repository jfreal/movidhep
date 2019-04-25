using Movid.App.Infrastructure;
using Movid.App.Models;
using Movid.Shared;
using Movid.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PublicProgramViewModel = Movid.App.Models.ViewModels.PublicProgramViewModel;

namespace Movid.App.Controllers
{
    public class PublicController : BaseController
    {
        [Route("program/sample/{clinicId?}/{userId?}")]
        public ActionResult Sample(int? clinicId, int? userId)
        {
            var program = GenerateRandomProgram();
            
            PublicProgramViewModel vm;

            if (clinicId.HasValue && userId.HasValue)
            {
                var clinic = RavenSession.Load<Clinic>("clinics/" + clinicId);
                var user = RavenSession.Load<User>("users/" + userId);

                vm = new PublicProgramViewModel() { Program = program, Clinic = clinic, From = user, Settings = new AccountSettings(){DefaultGreeting="Hi, this is a sample plan."}};

                return View("Program", vm);
            }

            var fakeClinic = new Clinic()
                                        {
                                            Address = "123 Some Street",
                                            Address2 = "APT 8",
                                            CityStateZip = "Sometown, CT, 8839",
                                            Name = "Sample Clinic",
                                            Phone = "555 555-5555",
                                            Email = "sample@yourclinic.com"
                                        };

            var fakeUser = new User()
                           {
                               Name = "John Smith",
                               Email = "asdf@asdf.com"
                           };
            
            vm = new PublicProgramViewModel() { Program = program, Clinic = fakeClinic, From = fakeUser, Settings = new AccountSettings(){DefaultGreeting="Hi, this is a sample plan."}};

            return View("Program", vm);
        }

        private Program GenerateRandomProgram()
        {
            var plan = new Program();

            var random = new Random();
            var take = random.Next(2, 5);

            var randoExercises = RavenSession.Query<Exercise>().Customize(x => x.RandomOrdering()).Take(take).ToList();

            foreach (var exer in randoExercises)
            {
                foreach (var detail in exer.ExerciseDetails)
                {
                    detail.Value = random.Next(0, 6).ToString();
                }
            }

            plan.Exercises = new List<Exercise>(randoExercises);
            return plan;
        }


        [Route("program/{id}/{lang?}")]
        public ActionResult Program(string id, string lang)
        {
            var plan = RavenSession.Query<Program>().FirstOrDefault(p => p.ShortUrl == id);
            
            if (plan == null)
                return HttpNotFound("Program not found");

            SaveView(plan);

            if (!string.IsNullOrWhiteSpace(lang))
                GoogleTranslate.TranslateExercise(plan,lang);

            var clinic = RavenSession.Load<Clinic>("clinics/" + plan.ClinicId);
            var user = RavenSession.Load<User>("users/" + plan.UserId);
            var account = RavenSession.Load<Account>("accounts/" + plan.AccountId);
            
            var vm = new PublicProgramViewModel() {Program = plan, From = user, Clinic = clinic, Settings = account.Settings};

            return View(vm);
        }





        private void SaveView(Program program)
        {
            var viewEvent = new ProgramView()
                                {
                                    When = DateTime.Now,
                                    Ip = HttpContext.Request.UserHostAddress,
                                    Browser = HttpContext.Request.Browser.Browser,
                                    User = HttpContext.Request.IsAuthenticated ? LoggedInUser.Email : ""
                                };

            program.PlanViews.Add(viewEvent);
            RavenSession.SaveChanges();
        }
    }

}
