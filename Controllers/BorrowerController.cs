using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagementService.Data;
using LibraryManagementService.Models;

namespace LibraryManagement.Controllers
{
    public class BorrowerController : Controller
    {
        private readonly LibraryContext _context;

        public BorrowerController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchString)
        {
            var borrowers = _context.Borrowers.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                borrowers = borrowers.Where(b => b.Name.ToLower().Contains(searchString));
            }

            return View(await borrowers.ToListAsync());
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var borrower = await _context.Borrowers
                .Include(b => b.Loans)
                .ThenInclude(l => l.Book)
                .FirstOrDefaultAsync(m => m.BorrowerId == id);

            if (borrower == null) return NotFound();

            return View(borrower);
        }

        // GET: Borrower/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BorrowerId,Name")] Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine($"Adding Borrower: {borrower.Name}"); // ✅ Debug message

                _context.Add(borrower);
                await _context.SaveChangesAsync();

                Console.WriteLine("Borrower added successfully!"); // ✅ Debug message
                return RedirectToAction(nameof(Index));
            }

            Console.WriteLine("ModelState is invalid!"); // ✅ Debug message
            return View(borrower);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrower = await _context.Borrowers.FindAsync(id);
            if (borrower == null)
            {
                return NotFound();
            }
            return View(borrower);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BorrowerId,Name")] Borrower borrower)
        {
            if (id != borrower.BorrowerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrower);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowerExists(borrower.BorrowerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(borrower);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrower = await _context.Borrowers
                .FirstOrDefaultAsync(m => m.BorrowerId == id);
            if (borrower == null)
            {
                return NotFound();
            }

            return View(borrower);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrower = await _context.Borrowers.FindAsync(id);
            if (borrower != null)
            {
                _context.Borrowers.Remove(borrower);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowerExists(int id)
        {
            return _context.Borrowers.Any(e => e.BorrowerId == id);
        }
    }
}
