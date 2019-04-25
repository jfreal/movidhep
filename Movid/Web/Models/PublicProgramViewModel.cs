using Movid.App.Controllers;
using Movid.App.Models.ViewModels;
using Movid.Shared;

namespace Movid.App.Models
{
    public class PublicProgramViewModel
    {
        public Program Program { get; set; }
        public User From { get; set; }
        public Clinic Clinic { get; set; }
    }
}