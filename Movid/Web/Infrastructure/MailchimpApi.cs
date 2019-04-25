using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MailChimp;
using MailChimp.Helper;
using MailChimp.Lists;

namespace Movid.App.Infrastructure
{
    public class MailchimpApi
    {
        public MailChimpManager MailChimpManager { get; set; }
        public MailchimpApi()
        {
            MailChimpManager = new MailChimpManager("428723bdcc616d3b336069adf05d0dd1-us3");
        }

        public bool SubscribeToGettingStarted(string email)
        {
            return SubscribeToList(email, "4ab3dd8460");
        }

        public bool SubscribeToGeneralUpdates(string email)
        {
            return SubscribeToList(email, "8bc183e6b1");
        }

        private bool SubscribeToList(string email, string listId)
        {
            var emailParam = new EmailParameter()
            {
                Email = email
            };

            EmailParameter results = MailChimpManager.Subscribe(listId, emailParam);

            return !string.IsNullOrWhiteSpace(results.EUId);
        }
    }
}