using System;
using System.Linq;
using System.Web.Http.ModelBinding;
using Movid.App.Infrastructure;
using Movid.App.Models.Api;
using Stripe;
using System.Collections.Generic;
using System.Web.Http;

namespace Movid.App.Controllers.Api
{
    public class ChangePlanRequest
    {
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string Cvc { get; set; }
        public string PlanId { get; set; }
        public int Quantity { get; set; }
    }

    [RoutePrefix("api/plan")]
    public class PlanApiController : BaseApiController
    {
        

        [HttpPost]
        [Route("change")]
        public ApiResponse ChangePlan(ChangePlanRequest postedModel)
        {            
            var stripeApi = new StripeApi();

            var expirationSplit = postedModel.Expiration.Split('/');

            try
            {
                
                if (string.IsNullOrWhiteSpace(Account.StripeCustomerId) || stripeApi.GetCustomer(Account.StripeCustomerId) == null )
                {
                    var response = stripeApi.StartMonthly(Account.Id.ToString(), LoggedInUser.Email, postedModel.PlanId,
                                                         postedModel.CardNumber, expirationSplit[0], expirationSplit[1],
                                                         postedModel.Cvc);

                    Account.StripeCustomerId = response;
                }
                else
                {

                    var response = stripeApi.UpdateMonthly(Account.StripeCustomerId, postedModel.PlanId,
                                                      postedModel.CardNumber, expirationSplit[0], expirationSplit[1],
                                                      postedModel.Cvc);
                }
            }
            catch(StripeException exception)
            {
                return new ApiResponse(exception.StripeError.Message);
            }

            Account.ActivePlanId = postedModel.PlanId;
            RavenSession.SaveChanges();


            return new ApiResponse(success: "Credit card added and your plan was changed successfuly, thanks for using Movid HEP.");
        }

        //TODO this doesn't really belong here
        [Route("current")]
        public CurrentAccountStatusViewModel GetCurrent()
        {
            if (string.IsNullOrWhiteSpace(Account.StripeCustomerId))
            {
                var freeVm = new CurrentAccountStatusViewModel()
                {
                    PlanName = "Free Trial",
                    Price = 0,
                    StartedOn = Account.CreatedDate,
                    Quantity = 1,
                    TrialLength = 30,
                    StartDate = Account.CreatedDate
                };

                return freeVm;
            }

            var customer = new StripeApi().GetCustomer(Account.StripeCustomerId);

            var vm = new CurrentAccountStatusViewModel()
                         {
                             PlanName = customer.StripeSubscription.StripePlan.Name,
                             Id = customer.StripeSubscription.StripePlan.Id,
                             Price = customer.StripeSubscription.StripePlan.AmountInCents.Value * .01,
                             StartedOn = customer.StripeSubscription.PeriodStart.Value,
                             Quantity = customer.StripeSubscription.Quantity,
                             StartDate = Account.CreatedDate,
                             TrialLength = 30
                         };

            return vm;
        }

        [Route("options/{group}")]
        public List<PlanViewModel> GetSubscriptionOptions(string group)
        {
            var stripePlans = new StripeApi().GetPlans();
            
            if (group.ToLower() == "upgrade")
            {
                stripePlans = stripePlans.Where(x => x.Id != "Free");
            }

            return stripePlans.Select(stripePlan => new PlanViewModel(stripePlan)).OrderBy(x=>x.Ordinal).ToList();
        }
    }

    public class CurrentAccountStatusViewModel
    {
        public string PlanName { get; set; }
        public double? Price { get; set; }
        public DateTime StartedOn { get; set; }
        
        public int Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public int TrialLength { get; set; }

        public int DaysRemaining
        {
            get
            {
                var daysLeft = TrialLength + (StartDate - DateTime.Now).Days;

                if (daysLeft < 0)
                    return 0;

                return daysLeft;
            }
        }

        public bool TrialExpired
        {
            get
            {
                //return true;
                return DaysRemaining <= 0;
            }
        }

        public string Id { get; set; }
    }

    public class PlanViewModel
    {
        public PlanViewModel(StripePlan stripePlan)
        {
            StripePlan = stripePlan;
            if (stripePlan.Id == "Free")
            {
                Ordinal = 0;
                HasQuantity = false;
            }

            if (stripePlan.Id.Contains("Multi"))
            {
                Ordinal = 2;
                HasQuantity = true;
                QuantityString = "office location";
            }

            if (stripePlan.Id.Contains("Academic"))
            {
                Ordinal = 999;
                HasQuantity = true;
                QuantityString = "classroom";
            }


                Quantity = 1;
            
        }

        public int Quantity { get; set; }

        public StripePlan StripePlan { get; set; }

        public bool HasQuantity { get; set; }
        public string QuantityString { get; set; }
        public int Ordinal { get; set; }
        public DateTime Test { get; set; }

    }
}
