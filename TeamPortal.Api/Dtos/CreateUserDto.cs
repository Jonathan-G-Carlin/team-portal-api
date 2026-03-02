using System.ComponentModel.DataAnnotations;

namespace TeamPortal.Api.Dtos;

public class CreateUserDto
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
    [Required]
    [MaxLength(256)]
    public string Password { get; set; } = string.Empty; 

}
