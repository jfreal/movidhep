using System.Collections.Generic;
using System.Linq;
using Movid.App.Infrastructure.Raven;
using Movid.App.Models;
using Movid.Shared;
using Movid.Shared.Model;
using Raven.Client;
using Raven.Client.Linq;
using System;

namespace Movid.App.Application
{
    public class UserOnboardProcess
    {
        private IDocumentSession _ravenSession;
        public UserOnboardProcess(IDocumentSession ravenSession)
        {
            _ravenSession = ravenSession;
        }

        public User RegisterNewTrial(string name, string email, string password)
        {
            //do these need to go in a transaction?  probably not, besides a big server failure there is no reason these inserts would fail
            //and the biggest risk is just a support cost
            var accountId = CreateNewAccount();

            CopyExercises(accountId);

            var clinicId = CreateNewClinic(accountId, "");

            var user = CreateNewAccountAdminUser(accountId, clinicId, name, email, password);

            return user;

        }

        public User CreateNewAccountAdminUser(int accountId, int clinicId, string name, string email, string password)
        {
            var user = new User()
                           {
                               Name = name,
                               Email = email,
                               Password = password,
                               AccountId = accountId,
                               CreatedOn = DateTime.Now,
                               LastLogin = DateTime.Now,
                               Roles = new List<string>() { "AccountAdmin" }
                           };

            user.ClinicIds.Add(clinicId);

            _ravenSession.Store(user);
            _ravenSession.SaveChanges();

            return user;
        }

        public bool CopyExercises(int accountId)
        {
            var exercises = _ravenSession.Query<Exercise>(typeof(ByOwnableAndName).Name).Where(x => x.Master);

            var enumerator = _ravenSession.Advanced.Stream(exercises);


            using (var bulkInsert = MvcApplication.Store.BulkInsert())
            {

                while (enumerator.MoveNext())
                {
                    var exer = enumerator.Current.Document;

                    var copy = exer.Copy();
                    copy.Id = 0;
                    copy.AccountId = accountId;
                    copy.OriginalExercise = exer.Id;
                    copy.Master = false;
                    bulkInsert.Store(copy);
                }
            }

            return true;
        }



        public int CreateNewAccount(int planId = 0)
        {
            var account = new Account()
                              {
                                  CreatedDate = DateTime.Now
                              };

            account.ActivePlanId = "Free";
            _ravenSession.Store(account);
            _ravenSession.SaveChanges();

            return account.Id;
        }

        public int CreateNewClinic(int accountId, string clinicName)
        {
            if (string.IsNullOrWhiteSpace(clinicName))
            {
                clinicName = "My Clinic";
            }

            var clinic = new Clinic()
                             {
                                 AccountId = accountId,
                                 Name = clinicName
                             };

            _ravenSession.Store(clinic);
            _ravenSession.SaveChanges();
            return clinic.Id;
        }


    }

}