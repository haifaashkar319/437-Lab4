namespace Core.Domain.Services;

public class BookDomainService
{
    public bool IsDuplicateTitle(IEnumerable<string> existingTitles, string newTitle)
    {
        return existingTitles.Any(t => t.Equals(newTitle, StringComparison.OrdinalIgnoreCase));
    }
}

// in services, i am adding the reusable logic, that doesn't belong to the entity itself which is Book here.
// this is a function to check if the title is duplicate or not, so it can be used in multiple places in the application.