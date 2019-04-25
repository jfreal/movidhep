namespace Movid.Shared
{
    public interface IOwnable
    {
        int AccountId { get; set; }
        int UserId { get; set; }
        int ClinicId { get; set; }
    }
}