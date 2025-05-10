using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels
{
    public class BorrowerEditViewModel : BorrowerCreateViewModel
    {
        public int BorrowerId { get; set; }
    }
}
