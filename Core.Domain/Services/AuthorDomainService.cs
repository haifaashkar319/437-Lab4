namespace Core.Domain.Services;

public class AuthorDomainService
{
    public bool IsDuplicateAuthorName(IEnumerable<string> existingNames, string newName)
    {
        return existingNames.Any(n => n.Equals(newName, StringComparison.OrdinalIgnoreCase));
    }

    public bool HasReachedMaxBooks(int bookCount)
    {
        return bookCount >= 10;
    }
}

// this code is to validate the author name and check if the author has reached the maximum number of books they can write.