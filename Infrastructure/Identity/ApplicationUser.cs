using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity{
    public class ApplicationUser : IdentityUser
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public bool IsLibrarian { get; set; } = false;

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        }
}
