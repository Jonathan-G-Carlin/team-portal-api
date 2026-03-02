using System.ComponentModel.DataAnnotations;

namespace TeamPortal.Api.Dtos
{
    public class UpdateUserDto
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        [Required]
        [MaxLength(100)]
        public string FamilyName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;
    }
}