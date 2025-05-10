using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels;

public class AuthorViewModel
{
    public int AuthorId { get; set; }

    [Required]
    [MinLength(3)]
    [Display(Name = "Author Name")]
    public string Name { get; set; } = string.Empty;
}
