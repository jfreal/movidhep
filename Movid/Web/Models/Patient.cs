using System.ComponentModel.DataAnnotations;

namespace Movid.App.Models
{
    public class Patient
    {
        [Required]
        [UIHint("email")]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        public string PhoneNumber { get; set; }
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public string Group { get; set; }

        public PatientStatus State { get; set; } 
    }

    public enum PatientStatus
    {
        Discharged, Active
    }
}