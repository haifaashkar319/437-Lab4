using System.ComponentModel.DataAnnotations;

namespace LibraryManagementService.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        
        [Required(ErrorMessage = "The Name field is required.")]
        [RegularExpression(@"^[A-Za-z][A-Za-z. -]{2,24}$", ErrorMessage = "Invalid Name")]
        public string Name { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
   
}

