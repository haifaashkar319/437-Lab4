using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagementService.Data;
using LibraryManagementService.Models;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagement.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks(
            int page = 1,
            int pageSize = 10,
            string? search = null)
        {
            var query = _context.Books
                .Include(b => b.Author)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                string lowered = search.ToLower();
                query = query.Where(b =>
                    b.Title.ToLower().Contains(lowered) ||
                    b.Genre.ToLower().Contains(lowered) ||
                    (b.Author != null && b.Author.Name.ToLower().Contains(lowered)));
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var books = await query
            .Include(b => b.Author)
            .Select(b => new {
                b.BookId,
                b.Title,
                b.Genre,
                b.AuthorId,
                Author = new {
                    b.Author.AuthorId,
                    b.Author.Name
                }
            })
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
            return Ok(new
            {
                currentPage = page,
                pageSize,
                totalCount,
                totalPages,
                results = books
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
                return NotFound();

            return book;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.BookId)
                return BadRequest();

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Books.Any(b => b.BookId == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
