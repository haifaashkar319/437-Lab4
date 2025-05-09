using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagement.ViewModels;

public class BookCreateViewModel
{

    public string Title { get; set; } = string.Empty;

    public string Genre { get; set; } = string.Empty;

    public int AuthorId { get; set; }

    public IEnumerable<SelectListItem>? Authors { get; set; }
}
