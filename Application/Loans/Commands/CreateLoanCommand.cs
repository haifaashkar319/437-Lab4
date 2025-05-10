using MediatR;
using System;

namespace Application.Loans.Commands
{
    public class CreateLoanCommand : IRequest<int>
    {
        public int BookId { get; set; }
        public int BorrowerId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; } 

        public CreateLoanCommand() { }

        public CreateLoanCommand(int bookId, int borrowerId, DateTime loanDate, DateTime dueDate)
        {
            BookId = bookId;
            BorrowerId = borrowerId;
            LoanDate = loanDate;
            DueDate = dueDate;
        }
    }
}
