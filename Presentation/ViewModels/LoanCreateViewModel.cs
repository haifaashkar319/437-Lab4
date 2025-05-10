using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Presentation.ViewModels
{
    public class LoanCreateViewModel
    {
        public int BookId { get; set; }
        public int BorrowerId { get; set; }
        public DateTime LoanDate { get; set; } = DateTime.Today;  // Set default to today
        public DateTime DueDate { get; set; } = DateTime.Today.AddDays(14); // Or any default logic

        public IEnumerable<SelectListItem>? Books { get; set; }
        public IEnumerable<SelectListItem>? Borrowers { get; set; }
    }
}
