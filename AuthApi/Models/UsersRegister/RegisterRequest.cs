namespace RollingDiceApi.Models.UsersRegister
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterUserRequest
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string LastName { get; set; }

        [EmailAddress]
        [Required]
        [MaxLength(128)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$", ErrorMessage = "Please provide correct Email address")]
        public string Email { get; set; }

        [Required]
        [Compare("ConfirmPassword")]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [Compare("ConfirmPassword")]
        [StringLength(20, MinimumLength = 6)]
        public string ConfirmPassword { get; set; }

        public string? UserPhoto { get; set; }
    }
}
