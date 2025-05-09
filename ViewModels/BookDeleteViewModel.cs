using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels;


public class BookDeleteViewModel
{
public int BookId { get; set; }
[Display(Name = "Title")]
public string Title { get; set; } = string.Empty;

[Display(Name = "Genre")]
public string Genre { get; set; } = string.Empty;

[Display(Name = "Author")]
public string AuthorName { get; set; } = string.Empty;
}