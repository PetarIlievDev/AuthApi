namespace RollingDiceApi.Models.UsersRegister
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterUserRequest
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public required string LastName { get; set; }

        [EmailAddress]
        [Required]
        [MaxLength(128)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$", ErrorMessage = "Please provide correct Email address")]
        public required string Email { get; set; }

        [Required]
        [Compare("ConfirmPassword")]
        [StringLength(20, MinimumLength = 6)]
        public required string Password { get; set; }

        [Required]
        [Compare("ConfirmPassword")]
        [StringLength(20, MinimumLength = 6)]
        public required string ConfirmPassword { get; set; }

        public string? UserPhoto { get; set; }
    }
}
