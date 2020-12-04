using Movid.App.Models;
using Movid.Shared;
using Raven.Client;
using Raven.Client.Documents.Session;

namespace Movid.App.Application
{
    public static class UserInvitationBuilder
    {
        public static UserInvitationVm Build(string id, IDocumentSession ravenSession)
        {
            var userInvitation = ravenSession.Load<UserInvitation>("UserInvitations/" + id);

            var clinic = ravenSession.Load<Clinic>("clinics/" + userInvitation.ClinicId);
            var from = ravenSession.Load<User>("users/" + userInvitation.UserId);
            var to = ravenSession.Load<User>("users/" + userInvitation.ToUserId);

            var viewModel = new UserInvitationVm()
            {
                Clinic = clinic,
                UserInvitation = userInvitation,
                From = @from,
                To = to
            };

            return viewModel;
        }
    }
}