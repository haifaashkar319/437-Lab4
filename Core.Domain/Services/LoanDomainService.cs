namespace Core.Domain.Services;

public class LoanDomainService
{
    public bool CanBorrowMoreBooks(int currentLoanCount)
    {
        return currentLoanCount < 3;
    }

    public bool IsBookAlreadyLoaned(IEnumerable<int> activeLoanedBookIds, int bookId)
    {
        return activeLoanedBookIds.Contains(bookId);
    }
}
// this code if to chekc if the user can borrow more books or not (depending if they borrowed 3 books or not)
// and also to check if the book is already loaned or not (if the book id is in the list of active loaned books).