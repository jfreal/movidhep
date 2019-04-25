using Movid.Shared;

namespace Movid.App.Models.ViewModels
{
    public class PublicProgramViewModel
    {
        public Program Program { get; set; }
        public Clinic Clinic { get; set; }
        public AccountSettings Settings { get; set; }

        //who sent the program
        public User From { get; set; }
    }
}