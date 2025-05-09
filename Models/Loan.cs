using System.ComponentModel.DataAnnotations;

namespace LibraryManagementService.Models
{
    public class Loan
    {
        public int LoanId { get; set; }

        [Required(ErrorMessage = "Book is required")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Borrower is required")]
        public int BorrowerId { get; set; }

        [Required(ErrorMessage = "Loan date is required")]
        public DateTime LoanDate { get; set; } = DateTime.Now;

        public Book? Book { get; set; } 
        public Borrower? Borrower { get; set; } 
    }
}
