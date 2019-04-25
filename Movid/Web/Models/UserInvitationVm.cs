using Movid.Shared;

namespace Movid.App.Models
{
    public class UserInvitationVm
    {
        public User To { get; set; }
        public User From { get; set; }
        public Clinic Clinic { get; set; }
        public UserInvitation UserInvitation { get; set; }
    }
}