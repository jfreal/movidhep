using System.Linq;
using Movid.App.Infrastructure;
using Movid.App.Models;
using Movid.App.Models.Api;
using System.Web.Http;
using Movid.App.Infrastructure.Emails;

namespace Movid.App.Controllers.Api
{
    public class ClinicPost
    {
        public int ClinicId { get; set; }
    }

    public class ProgramApiController : BaseApiController
    {
        [HttpPost]
        [Route("api/program/sendRandom")]
        public ApiResponse SendRandomToLoggedInUser(ClinicPost clinic)
        {
            var clinicId = LoggedInUser.ClinicIds.First();

            var context = new WebApiMovidAppContext()
                              {
                                  Account = this.Account,
                                  ApplicationAdministrator = false,
                                  Clinic = RavenSession.Load<Clinic>("clinics/" + clinicId),
                                  LoggedInUser = this.LoggedInUser
                              };

            //var emailer = new ProgramEmailer(context);
            //emailer.SendRandomizedProgramToLoggedInUser();

            new Emailer(context).SendEmail(EmailEnum.SendRandomizedProgramToLoggedInUser, string.Empty, string.Empty, 0);

            return new ApiResponse(success: "Sample program sent to " + LoggedInUser.Email);
        }
    }
}