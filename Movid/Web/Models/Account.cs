using System;

namespace Movid.App.Models
{
    public class Account
    {
        public Account()
        {
            
        }
        
        public int Id { get; set; }
     
        public DateTime CreatedDate { get; set; }
        public string ActivePlanId { get; set; }
        public string StripeCustomerId { get; set; }
        
        public bool ExercisesUpToDate { get; set; }
        
        public DateTime LastMasterPush { get; set; }
        public DateTime LastNotificationsRead { get; set; }
        public DateTime LastLogin { get; set; }

        private AccountSettings _settings;
        public AccountSettings Settings
        {
            get { return _settings ?? (_settings = new AccountSettings()); }
            set { _settings = value; }
        }

        //the last time a user from this account logged in
        
    }
}