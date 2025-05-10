using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Infrastructure.Persistence;
using Presentation.ViewModels;
using Application.Books.Commands;

namespace Presentation.Controllers
{
    public class BooksController : Controller
    {
        private readonly IMediator _mediator;
        private readonly CleanLibraryContext _context;

        public BooksController(IMediator mediator, CleanLibraryContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var books = _context.Books.Include(b => b.Author).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(b =>
                    b.Title.Value.ToLower().Contains(searchString.ToLower()) ||
                    b.Author.Name.Value.ToLower().Contains(searchString.ToLower()));
            }

            var viewModels = await books
                .Select(b => new BookListViewModel
                {
                    BookId = b.BookId,
                    Title = b.Title.Value,
                    Genre = b.Genre.Value,
                    AuthorName = b.Author.Name.Value
                })
                .ToListAsync();

            return View(viewModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null) return NotFound();

            var viewModel = new BookDetailViewModel
            {
                BookId = book.BookId,
                Title = book.Title.Value,
                Genre = book.Genre.Value,
                AuthorName = book.Author.Name.Value
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var authors = await _context.Authors.ToListAsync();

            var viewModel = new BookCreateViewModel
            {
                Authors = authors.Select(a => new SelectListItem
                {
                    Value = a.AuthorId.ToString(),
                    Text = a.Name.Value
                })
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Authors = _context.Authors.Select(a => new SelectListItem
                {
                    Value = a.AuthorId.ToString(),
                    Text = a.Name.Value
                });
                return View(model);
            }

            var command = new CreateBookCommand(model.Title, model.Genre, model.AuthorId);
            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.BookId == id);
            if (book == null) return NotFound();

            var viewModel = new BookEditViewModel
            {
                BookId = book.BookId,
                Title = book.Title.Value,
                Genre = book.Genre.Value,
                AuthorId = book.AuthorId,
                Authors = _context.Authors.Select(a => new SelectListItem
                {
                    Value = a.AuthorId.ToString(),
                    Text = a.Name.Value
                })
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Authors = _context.Authors.Select(a => new SelectListItem
                {
                    Value = a.AuthorId.ToString(),
                    Text = a.Name.Value
                });
                return View(model);
            }

            var book = await _context.Books.FindAsync(model.BookId);
            if (book == null) return NotFound();

            book.Title = new Core.Domain.ValueObjects.Title(model.Title);
            book.Genre = new Core.Domain.ValueObjects.Genre(model.Genre);
            book.AuthorId = model.AuthorId;

            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.BookId == id);
            if (book == null) return NotFound();

            var viewModel = new BookDeleteViewModel
            {
                BookId = book.BookId,
                Title = book.Title.Value,
                Genre = book.Genre.Value,
                AuthorName = book.Author.Name.Value
            };

            return View(viewModel);
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
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
