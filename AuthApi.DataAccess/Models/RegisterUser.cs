namespace AuthApi.DataAccess.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    public class RegisterUser
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string LastName { get; set; }

        [EmailAddress]
        [Required]
        [MaxLength(128)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 6)]
        public string Password { get; set; }

        [AllowNull]
        public string? Image { get; set; }
    }
}
