namespace Presentation.ViewModels;

public class AuthorCreateViewModel
{
    // Removed DataAnnotations; validation is handled in the Application layer (FluentValidation)
    public string Name { get; set; } = string.Empty;
}
