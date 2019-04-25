using Movid.App.Controllers;
using Movid.App.Models;
using Movid.Shared;

namespace Movid.App.Application
{
    public static class Ownership
    {
        public static void Assign(IOwnable ownable, IMovidAppContext appContext)
        {
            ownable.AccountId = appContext.Account.Id;
            ownable.ClinicId = appContext.Clinic.Id;
            ownable.UserId = appContext.LoggedInUser.Id;
        }

        public static bool Owns(IOwnable ownable, IMovidAppContext appContext)
        {
            return (!(ownable.AccountId != appContext.LoggedInUser.AccountId) || appContext.ApplicationAdministrator);
        }
    }
}