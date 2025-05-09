using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels;

public class AuthorEditViewModel
{
    public int AuthorId { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
    public string Name { get; set; } = string.Empty;
}
