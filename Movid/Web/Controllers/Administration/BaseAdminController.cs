using Movid.App.Application;

namespace Movid.App.Controllers.Administration
{
    [MustHaveRole(Roles = "Admin")]
    public class BaseAdminController : AppController
    {
    }
}
