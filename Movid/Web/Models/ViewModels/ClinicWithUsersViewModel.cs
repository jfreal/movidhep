using System;
using System.Collections.Generic;
using Movid.Shared;

namespace Movid.App.Models.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public DateTime LastLogin { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class ClinicWithUsersViewModel
    {
        public class ClinicViewModel
        {
            public Clinic Details { get; set; }
            public List<UserViewModel> Users { get; set; }
        }

        public List<ClinicViewModel> Clinics { get; set; }

        public ClinicWithUsersViewModel(List<User> users, IEnumerable<Clinic> clinics)
        {
            Clinics = new List<ClinicViewModel>();

            foreach (var clinic in clinics)
            {
                var vm = new ClinicViewModel()
                             {
                                 Details = clinic,
                                 Users = new List<UserViewModel>()
                             };

                foreach (var user in users)
                {
                    if (user.ClinicIds.Contains(clinic.Id) || user.Roles.Contains("AccountAdmin"))
                    {
                        vm.Users.Add(new UserViewModel()
                                         {
                                             Id = user.Id,
                                             Email = user.Email,
                                             Name = user.Name = user.Name,
                                             Status = user.Status.ToString(),
                                             LastLogin = user.LastLogin,
                                             CreatedOn = user.CreatedOn
                                         });
                    }
                }

                Clinics.Add(vm);
            }
        }
    }
}