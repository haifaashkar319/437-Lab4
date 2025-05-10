using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels
{
    public class BorrowerEditViewModel : BorrowerCreateViewModel
    {
        public int BorrowerId { get; set; }
    }
}
