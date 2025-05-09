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
    public class BorrowersController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BorrowersController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Borrower>>> GetBorrowers()
        {
            return await _context.Borrowers
                                 .Include(b => b.Loans)
                                 .ThenInclude(l => l.Book)
                                 .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Borrower>> GetBorrower(int id)
        {
            var borrower = await _context.Borrowers
                                         .Include(b => b.Loans)
                                         .ThenInclude(l => l.Book)
                                         .FirstOrDefaultAsync(b => b.BorrowerId == id);

            if (borrower == null)
                return NotFound();

            return borrower;
        }

        [HttpPost]
        public async Task<ActionResult<Borrower>> PostBorrower(Borrower borrower)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Borrowers.Add(borrower);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBorrower), new { id = borrower.BorrowerId }, borrower);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBorrower(int id, Borrower borrower)
        {
            if (id != borrower.BorrowerId)
                return BadRequest();

            _context.Entry(borrower).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Borrowers.Any(b => b.BorrowerId == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrower(int id)
        {
            var borrower = await _context.Borrowers.FindAsync(id);
            if (borrower == null)
                return NotFound();

            _context.Borrowers.Remove(borrower);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
