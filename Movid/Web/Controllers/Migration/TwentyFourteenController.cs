using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movid.App.Application;
using Movid.App.Infrastructure.Raven;
using Movid.App.Models;
using Movid.Shared;
using Movid.Shared.Model;

namespace Movid.App.Controllers.Migration
{
    public class TwentyFourteenController : AppController
    {
        //
        // GET: /TwentyFourteen/
        [Route("migrate2014")]
        public ActionResult Index()
        {
            var users = RavenSession.Query<User>().Take(100);
            var onboarder = new UserOnboardProcess(RavenSession);
            RavenSession.Advanced.MaxNumberOfRequestsPerSession = 10000;
            foreach (var user in users)
            {
                if( !user.Roles.Contains(UserRoles.AccountAdmin.ToString()))
                {
                    user.Roles.Add(UserRoles.AccountAdmin.ToString());
                }

                if (user.AccountId == 0)
                {
                    var accountId = onboarder.CreateNewAccount();
                    user.AccountId = accountId;

                    if (!user.ClinicIds.Any())
                    {
                        var clinicId  = onboarder.CreateNewClinic(accountId, "My Clinic");
                        user.ClinicIds.Add(clinicId);

                    }
                }

                
            }

            RavenSession.SaveChanges();

            return View();
        }

        [Route("migrateexercises2014")]
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

                
            

            //var enumerator = RavenSession.Advanced.Stream(exercises);


            //using (var bulkInsert = MvcApplication.Store.BulkInsert())
            //{

            //    while (enumerator.MoveNext())
            //    {
            //        var exer = enumerator.Current.Document;

            //        var copy = exer.Copy();
            //        copy.Id = 0;
            //        copy.AccountId = accountId;
            //        copy.OriginalExercise = exer.Id;
            //        //_ravenSession.Store(copy);

            //        bulkInsert.Store(copy);
            //    }

            //    //_ravenSession.SaveChanges();

            //}

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

        [Route("dailyfix!")]
        public void DailyFix()
        {
            var exercises = RavenSession.Query<Exercise>()
                            .Where(x => x.AccountId == 705).Take(1024).ToList();

            foreach (var exercise in exercises)
            {
                foreach (var detail in exercise.ExerciseDetails)
                {
                    if (detail.Label == "Frequency")
                    {
                        var newDetails = new List<string>(detail.Options);

                        if (!newDetails.Contains("Per Hour"))
                        {
                            newDetails.Add("Per Hour");
                            detail.Options = newDetails.ToArray();
                        }
                    }
                }
            }

            RavenSession.SaveChanges();
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
