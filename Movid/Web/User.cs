
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movid.Shared
{
    //eventually this shouldn't be shared between the two apps,
    //just the blog needs this
    public class User : IOwnable
    {
        private List<string> _onboardingTasks;
        private List<string> _roles;

        private List<int> _clinicIds;
        private UserStatus _status;

        public List<int> ClinicIds
        {
            get { return _clinicIds ?? (_clinicIds = new List<int>()); }
            set { _clinicIds = value; }
        }

        public int AccountId { get; set; }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        [ScaffoldColumn(false)]
        public string Password { get; set; }

        public List<string> Roles
        {
            get { return _roles ?? (_roles = new List<string>()); }
            set { _roles = value; }
        }

        public DateTime CreatedOn { get; set; }
        public DateTime LastLogin { get; set; }

        public List<string> OnboardingTasks
        {
            get { return _onboardingTasks ?? (_onboardingTasks = new List<string>()); }
            set { _onboardingTasks = value; }
        }

        public UserStatus Status
        {
            get
            {
                if (Roles.Contains("AccountAdmin"))
                {
                    return UserStatus.Admin;
                }

                return _status;
            }
            set { _status = value; }
        }


        public int UserId { get; set; }

        public int ClinicId { get; set; }
    }

    public enum UserStatus
    {
        Invited, Disabled, Active, Admin
    }
}
