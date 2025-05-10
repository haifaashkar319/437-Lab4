using System.ComponentModel.DataAnnotations;

namespace LibraryManagementService.DTOs
{
    public class ChangePasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "New password must be at least 6 characters.")]
        public string NewPassword     { get; set; } = string.Empty;
    }
}
