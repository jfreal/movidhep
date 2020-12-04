using System;
using System.Collections.Generic;
using System.Configuration;
using Stripe;

namespace Movid.App.Infrastructure
{
    public class StripeApi
    {
        public StripeApi()
        {
            PrivateKey = ConfigurationManager.AppSettings["StripePrivate"].ToString();
        }

        protected string PrivateKey { get; set; }

        public IEnumerable<Stripe.Plan> GetPlans()
        {
            var planService = new Stripe.PlanService(PrivateKey);
            IEnumerable<Stripe.Plan> response = planService.List();

            return response;
        }

        public Stripe.Customer GetCustomer(string customerId)
        {
            var customerService = new Stripe.CustomerService(PrivateKey);
            Stripe.Customer stripeCustomer = customerService.Get(customerId);

            return stripeCustomer;
        }
            
        public string StartMonthly(string description, string email, string planId, string cardNumber, string expMonth, string expYear, string csc)
        {

            //var customerService = new StripeCustomerService(PrivateKey);

            //var newCustomer = new StripeCustomerCreateOptions();

            //// set these properties if it makes you happy
            //newCustomer.Email = email;
            //newCustomer.Description = description;

            //// set these properties if passing full card details (do not
            //// set these properties if you have set TokenId)
            //newCustomer.CardNumber = cardNumber;
            //newCustomer.CardExpirationYear = expYear;
            //newCustomer.CardExpirationMonth = expMonth;
            //newCustomer.CardCvc = csc;           

            //newCustomer.PlanId = planId;                         
            //newCustomer.Quantity = 1;            

            //StripeCustomer stripeCustomer = customerService.Create(newCustomer);

            //return stripeCustomer.Id;

            return "ok";
        }

        public string UpdateMonthly(string accountId, string planId, string cardNumber, string expMonth, string expYear, string csc)
        {
            //var customerService = new StripeCustomerService(PrivateKey);

            //var newCustomer = new StripeCustomerUpdateSubscriptionOptions();

            //// set these properties if passing full card details (do not
            //// set these properties if you have set TokenId)
            //newCustomer.CardNumber = cardNumber;
            //newCustomer.CardExpirationYear = expYear;
            //newCustomer.CardExpirationMonth = expMonth;
            //newCustomer.CardCvc = csc;

            //newCustomer.PlanId = planId;
            //newCustomer.Quantity = 1;

            //var stripeCustomer = customerService.UpdateSubscription(accountId,newCustomer);

            //return stripeCustomer.Id;

            return "ok";
        }
    }
}