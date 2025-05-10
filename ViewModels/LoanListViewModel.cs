using System;

namespace LibraryManagement.ViewModels
{
    public class LoanListViewModel
    {
        public int LoanId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string BorrowerName { get; set; } = string.Empty;
        public DateTime LoanDate { get; set; } = DateTime.Today;  // Set default to today
        public DateTime DueDate { get; set; } = DateTime.Today.AddDays(14); // Or any default logic
    }
}
