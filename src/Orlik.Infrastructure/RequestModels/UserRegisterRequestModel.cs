namespace Orlik.Infrastructure.RequestModels
{
    public class UserRegisterRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
    }
}
