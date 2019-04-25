namespace Movid.App.Controllers.Api
{
    public class UserPostedModel : PostedModelForClinic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}