namespace Presentation.ViewModels;

public class AuthorEditViewModel
{
    public int AuthorId { get; set; }
    // Removed DataAnnotations; validation is handled via FluentValidation in Application layer
    public string Name { get; set; } = string.Empty;
}
