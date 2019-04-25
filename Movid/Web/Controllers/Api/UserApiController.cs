using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Movid.App.Application;
using Movid.App.Controllers.Api.Filters;
using Movid.App.Infrastructure;
using Movid.App.Models;
using Movid.App.Models.Api;
using Movid.App.Models.ViewModels;
using Movid.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Raven.Json.Linq;
using Movid.App.Infrastructure.Emails;

namespace Movid.App.Controllers.Api
{
    public class AccountApiController : BaseApiController
    {
        [Route("api/users/{userId}")]
        public UserViewModel GetUser(int userId)
        {
            var user = RavenSession.Load<User>("users/" + userId);

            if (!Ownership.Owns(user, this)) throw new HttpResponseException(HttpStatusCode.NotFound);

            var userVm = new UserViewModel()
                             {
                                 Email = user.Email,
                                 Id = user.Id,
                                 Name = user.Name,
                                 Status = user.Status.ToString(),
                                 LastLogin = user.LastLogin,
                                 CreatedOn = user.CreatedOn
                             };

            return userVm;
        }

        [Route("api/toggle-user/{userId}")]
        public ApiResponse Disable(int userId)
        {
            var user = RavenSession.Load<User>("users/" + userId);

            if (user == null || user.AccountId != Account.Id)
            {
                return new ApiResponse("User not found");
            }

            if (!Ownership.Owns(user, this)) return new ApiResponse("User not found");

            if (user.Status != UserStatus.Disabled)
            {
                user.Status = UserStatus.Active;
            }

            if (user.Status != UserStatus.Active)
            {
                user.Status = UserStatus.Disabled;
            }

            RavenSession.SaveChanges();

            return new ApiResponse(success: "User status changed");
        }

        [Route("api/users/edit")]
        [HttpPost]
        public ApiResponse SaveUser(UserPostedModel postedModel)
        {
            if (!ModelState.IsValid)
            {
                return new ApiResponse("Validation errors occured.");
            }

            var user = RavenSession.Load<User>("users/" + postedModel.Id);

            if (user == null || user.AccountId != Account.Id)
            {
                return new ApiResponse("User not found");
            }

            if (!Ownership.Owns(user, this)) return new ApiResponse("User not found");

            user.Name = postedModel.Name;
            user.Email = postedModel.Email;
            RavenSession.SaveChanges();

            return new ApiResponse(success: string.Format("User {0} edited", user.Email));
        }

        [Route("api/users/add")]
        [SetClinicContextFromRouteParams]
        public ApiResponse AddUser(UserPostedModel postedModel)
        {
            var existingUserWithSameEmail =
                RavenSession.Query<User>()
                            .Any(x => x.AccountId == LoggedInUser.AccountId && x.Email == postedModel.Email);

            if (existingUserWithSameEmail)
            {
                return new ApiResponse(error: "A user with this email already exists");
            }

            var user = new User()
                           {
                               Name = postedModel.Name,
                               Status = UserStatus.Invited,
                               Email = postedModel.Email,
                               ClinicIds = new List<int>() { postedModel.ClinicId },
                               CreatedOn = DateTime.Now,
                               AccountId = LoggedInUser.AccountId
                           };

            RavenSession.Store(user);

            var invitation = new UserInvitation()
                                        {
                                            ClinicId = postedModel.ClinicId,
                                            ToUserId = user.Id,
                                            Created = DateTime.Now
                                        };

            Ownership.Assign(invitation, this);
            invitation.ClinicId = postedModel.ClinicId;

            RavenSession.Store(invitation);
            RavenSession.SaveChanges();

            SendInvitation(invitation, user);

            return new ApiResponse(success: "User created") { Model = user };
        }

        [HttpPost]
        [Route("api/users/resend-invitation")]
        public ApiResponse ResendInvitation(UserPostedModel postedModel)
        {
            var invitation = RavenSession.Query<UserInvitation>().FirstOrDefault(x => x.ToUserId == postedModel.Id);

            if (invitation == null)
            {
                return new ApiResponse("Invitation not sent yet or can't be found.");
            }

            var user = RavenSession.Load<User>("users/" + invitation.ToUserId);

            if (user == null)
            {
                return new ApiResponse("Invited user can't be found");
            }

            if (!Ownership.Owns(user, this)) return new ApiResponse("User not found");

            SendInvitation(invitation, user);
            return new ApiResponse(success: "Invitation email resent successfuly");
        }

        private static void SendInvitation(UserInvitation userInvitation, User user)
        {
            using (WebClient client = new WebClient())
            {
                var baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
                var url = baseUrl + "internal-email/invitation/" + userInvitation.Id;
                string inviteEmailContent = client.DownloadString(url);
                //Emailer.SendContent(user.Email, "You've been invited to Movid HEP", inviteEmailContent);

                new Emailer(null).SendEmail(EmailEnum.SendInvitation, user.Email, inviteEmailContent, 0);
            }
        }

        [HttpPost]
        [Route("api/user/attachment/{userId}")]
        public async Task<HttpResponseMessage> SaveAttachment(int userId)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            foreach (var file in provider.Contents)
            {
                if (file.Headers.ContentType != null)
                {
                    var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                    var buffer = file.ReadAsStreamAsync().Result;

                    MvcApplication.Store.DatabaseCommands.PutAttachment("userheadshot/" + userId, null, buffer,
                                                                        new RavenJObject());
                }

                //Do whatever you want with filename and its binaray data.
            }

            return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse(success: "New headshot uploaded ok."));
        }
    }
}