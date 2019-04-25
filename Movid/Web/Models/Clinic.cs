using Movid.Shared;

namespace Movid.App.Models
{
    public class Clinic
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string CityStateZip { get; set; }
        public string ProgramGreeting { get; set; }

        public string EmailMessage { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
    }
}