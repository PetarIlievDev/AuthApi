namespace RollingDiceApi.Models.LogIn
{
    using System.ComponentModel.DataAnnotations;

    public class LogInRequest
    {
        [EmailAddress]
        [Required]
        [MaxLength(254)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$", ErrorMessage = "Please provide correct Email address")]
        public required string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public required string Password { get; set; }
    }
}
