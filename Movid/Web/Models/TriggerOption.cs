using System.ComponentModel;

namespace Movid.App.Models
{
    public enum TriggerOption
    {
        [Description("Last Email")]
        LastEmail,
        [Description("Patient Changed Status")]
        PatientChangesStatus,
        [Description("Creation of Patient")]
        Creation
    }
}