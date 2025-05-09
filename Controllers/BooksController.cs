using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Core.Domain.Entities;
using Application.Books.Commands;
using Infrastructure.Persistence;

namespace LibraryManagement.Controllers
{
    public class BooksController : Controller
    {
        private readonly IMediator _mediator;
        private readonly CleanLibraryContext _context;  // Updated type

        public BooksController(IMediator mediator, CleanLibraryContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        // TODO: Migrate to GetBooksQuery
        public async Task<IActionResult> Index(string searchString)
        {
            var books = from b in _context.Books.Include(b => b.Author)
                        select b;

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Value.ToLower().Contains(searchString) || b.Author.Name.Value.ToLower().Contains(searchString));
            }

            return View(await books.ToListAsync());
        }

        // TODO: Migrate to GetBookByIdQuery
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null) return NotFound();

            return View(book);
        }

        // Using hybrid approach - MediatR for Create, context for dropdown
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,AuthorId,Genre")] Book book)
        {
            if (ModelState.IsValid)
            {
                var command = new CreateBookCommand(book.Title.Value, book.Genre.Value, book.AuthorId);
                await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }

            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "Name", book.AuthorId);
            return View(book);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "Name", book.AuthorId);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,AuthorId,Genre")] Book book)
        {
            if (id != book.BookId)
            {
                Console.WriteLine("Book ID mismatch!");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid!");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "Name", book.AuthorId);
                return View(book);
            }

            try
            {
                Console.WriteLine($"Updating Book: ID={book.BookId}, Title={book.Title}, Genre={book.Genre}, Author={book.AuthorId}");

                _context.Attach(book);
                _context.Entry(book).State = EntityState.Modified; // Ensure EF Core tracks changes

                await _context.SaveChangesAsync();
                Console.WriteLine("Book updated successfully!");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.BookId))
                {
                    Console.WriteLine("Book no longer exists!");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null) return NotFound();

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Book with ID {id} deleted successfully.");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}