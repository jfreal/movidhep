using Movid.Shared;
using System;

namespace Movid.App.Models
{
    public class UserInvitation : IOwnable
    {
        public int ToUserId { get; set; }
        public int UserId { get; set; }
        public int ClinicId { get; set; }
        public bool Validated { get; set; }

        public DateTime Created { get; set; }

        public Guid Id { get; set; }

        public int AccountId { get; set; }
    }
}