using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels;

public class AuthorViewModel
{
    public int AuthorId { get; set; }

    [Required]
    [MinLength(3)]
    [Display(Name = "Author Name")]
    public string Name { get; set; } = string.Empty;
}
