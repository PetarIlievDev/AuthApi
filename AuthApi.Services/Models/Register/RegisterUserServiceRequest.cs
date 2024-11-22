namespace AuthApi.Services.Models.Register
{
    public class RegisterUserServiceRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Image { get; set; }
    }
}
