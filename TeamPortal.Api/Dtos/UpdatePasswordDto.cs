using System.ComponentModel.DataAnnotations;

namespace TeamPortal.Api.Dtos
{
    public class UpdatePasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; } = string.Empty;
        [Required]
        public string NewPassword { get; set; } = string.Empty;
    }
}
