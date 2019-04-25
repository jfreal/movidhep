using Movid.App.Models;
using System.Web.Http.Filters;

namespace Movid.App.Controllers.Api.Filters
{
    public class SetClinicContextFromRouteParams : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            object forClinic;
            if (actionContext.ActionArguments.TryGetValue("postedModel", out forClinic))
            {
                var cntrl = (BaseApiController) actionContext.ControllerContext.Controller;

                var clinic = cntrl.RavenSession.Load<Clinic>("clinics/" + ((PostedModelForClinic)forClinic).ClinicId);

                cntrl.Clinic = clinic;
            }

            base.OnActionExecuting(actionContext);
        }
    }
}