using Movid.App.Models;
using Movid.Shared;

namespace Movid.App.Controllers.Api
{
    public class WebApiMovidAppContext : IMovidAppContext
    {
        public User LoggedInUser { get; set; }
        public Account Account { get; set; }
        public Clinic Clinic { get; set; }
        public bool ApplicationAdministrator { get; set; }
    }
}