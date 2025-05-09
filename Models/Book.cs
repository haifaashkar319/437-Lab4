using System.ComponentModel.DataAnnotations;

namespace LibraryManagementService.Models
{

  public class Book
  {
    public int BookId { get; set; }
    
    [Required(ErrorMessage = "The Title field is required.")]
    [RegularExpression(@"^[A-Za-z][A-Za-z .-]{2,49}$",
    ErrorMessage = "Invalid Title.")]
    public string Title { get; set; }
    public int AuthorId { get; set; }

    [Required(ErrorMessage = "Invalid Genre.")]
    [StringLength(30, ErrorMessage = "Invalid Genre.")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Invalid Genre.")]
    public string Genre { get; set; }
    public Author? Author { get; set; }
    public ICollection<Loan>? Loans { get; set; }
  }

}
