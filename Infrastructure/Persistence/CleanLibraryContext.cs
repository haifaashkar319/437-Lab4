using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Entities;
using Infrastructure.Identity;
using Core.Domain.ValueObjects;
namespace Infrastructure.Persistence
{


    public class CleanLibraryContext : IdentityDbContext<ApplicationUser>
    {
        public CleanLibraryContext(DbContextOptions<CleanLibraryContext> options) : base(options)
        {  
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Author → AuthorName VO
    modelBuilder.Entity<Author>()
        .Property(a => a.Name)
        .HasConversion(
            name => name.Value, // how to store in DB
            value => new AuthorName(value) // how to recreate
        );

    // Book → Title and Genre VOs
    modelBuilder.Entity<Book>()
        .Property(b => b.Title)
        .HasConversion(
            t => t.Value,
            v => new Title(v)
        );

    modelBuilder.Entity<Book>()
        .Property(b => b.Genre)
        .HasConversion(
            g => g.Value,
            v => new Genre(v)
        );

    // Borrower → BorrowerName VO
    modelBuilder.Entity<Borrower>()
        .Property(b => b.Name)
        .HasConversion(
            n => n.Value,
            v => new BorrowerName(v)
        );

    // Loan FK config
    modelBuilder.Entity<Loan>()
        .HasOne(l => l.Book)
        .WithMany(b => b.Loans)
        .HasForeignKey(l => l.BookId);

    modelBuilder.Entity<Loan>()
        .HasOne(l => l.Borrower)
        .WithMany(b => b.Loans)
        .HasForeignKey(l => l.BorrowerId);
}

    }

}