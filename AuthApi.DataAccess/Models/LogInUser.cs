namespace AuthApi.DataAccess.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class LogInUser
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }

        public RegisterUser RegisterUser { get; set; }
        public Guid UserId { get; set; }
    }
}
