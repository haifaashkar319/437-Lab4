using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MediatR;
using Infrastructure.Persistence;
using LibraryManagement.ViewModels;
using Application.Loans.Commands;
using Application.Loans.Queries;

namespace LibraryManagement.Controllers
{
    public class LoanController : Controller
    {
        private readonly IMediator _mediator;
        private readonly CleanLibraryContext _context;

        public LoanController(IMediator mediator, CleanLibraryContext context)
        {
            _mediator = mediator;
            _context  = context;
        }

        // GET: /Loan
        public async Task<IActionResult> Index()
        {
            var loanDtos = await _mediator.Send(new GetLoansQuery());
            var vms = loanDtos
                .Select(l => new LoanListViewModel {
                    LoanId       = l.LoanId,
                    BookTitle    = l.BookTitle,
                    BorrowerName = l.BorrowerName,
                    LoanDate     = l.LoanDate,
                    DueDate      = l.ReturnDate ?? l.LoanDate.AddDays(14)
                })
                .ToList();
            return View(vms);
        }

        // GET: /Loan/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var loan = await _mediator.Send(new GetLoanByIdQuery(id));
            if (loan == null) return NotFound();

            var vm = new LoanDetailViewModel {
                LoanId       = loan.LoanId,
                BookTitle    = loan.BookTitle,
                BorrowerName = loan.BorrowerName,
                LoanDate     = loan.LoanDate,
                DueDate      = loan.ReturnDate ?? loan.LoanDate.AddDays(14)
            };
            return View(vm);
        }

        // GET: /Loan/Create
        public IActionResult Create()
        {
            var vm = new LoanCreateViewModel {
                Books = _context.Books.Select(b => new SelectListItem {
                    Value = b.BookId.ToString(),
                    Text  = b.Title.Value
                }),
                Borrowers = _context.Borrowers.Select(b => new SelectListItem {
                    Value = b.BorrowerId.ToString(),
                    Text  = b.Name.Value
                })
            };
            return View(vm);
        }

        // POST: /Loan/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoanCreateViewModel model)
        {
            if (!ModelState.IsValid) {
                // re-populate dropdowns on error
                model.Books = _context.Books.Select(b => new SelectListItem {
                    Value = b.BookId.ToString(),
                    Text  = b.Title.Value
                });
                model.Borrowers = _context.Borrowers.Select(b => new SelectListItem {
                    Value = b.BorrowerId.ToString(),
                    Text  = b.Name.Value
                });
                return View(model);
            }

            var cmd = new CreateLoanCommand(
                model.BookId,
                model.BorrowerId,
                model.LoanDate,
                model.DueDate
            );
            await _mediator.Send(cmd);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Loan/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var loan = await _mediator.Send(new GetLoanByIdQuery(id));
            if (loan == null) return NotFound();

            var vm = new LoanEditViewModel {
                LoanId       = loan.LoanId,
                BookId       = loan.BookId,
                BorrowerId   = loan.BorrowerId,
                LoanDate     = loan.LoanDate,
                DueDate      = loan.ReturnDate ?? loan.LoanDate.AddDays(14),
                Books        = _context.Books.Select(b => new SelectListItem {
                    Value = b.BookId.ToString(),
                    Text  = b.Title.Value
                }),
                Borrowers    = _context.Borrowers.Select(b => new SelectListItem {
                    Value = b.BorrowerId.ToString(),
                    Text  = b.Name.Value
                })
            };
            return View(vm);
        }

        // POST: /Loan/Edit
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LoanEditViewModel model)
        {
            if (!ModelState.IsValid) {
                model.Books = _context.Books.Select(b => new SelectListItem {
                    Value = b.BookId.ToString(),
                    Text  = b.Title.Value
                });
                model.Borrowers = _context.Borrowers.Select(b => new SelectListItem {
                    Value = b.BorrowerId.ToString(),
                    Text  = b.Name.Value
                });
                return View(model);
            }

            var cmd = new UpdateLoanCommand(
                model.LoanId,
                model.BookId,
                model.BorrowerId,
                model.LoanDate,
                model.DueDate
            );
            var ok = await _mediator.Send(cmd);
            if (!ok) return NotFound();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Loan/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var loan = await _mediator.Send(new GetLoanByIdQuery(id));
            if (loan == null) return NotFound();

            var vm = new LoanDeleteViewModel {
                LoanId       = loan.LoanId,
                BookTitle    = loan.BookTitle,
                BorrowerName = loan.BorrowerName,
                LoanDate     = loan.LoanDate,
                DueDate      = loan.ReturnDate ?? loan.LoanDate.AddDays(14)
            };
            return View(vm);
        }

        // POST: /Loan/DeleteConfirmed/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _mediator.Send(new DeleteLoanCommand(id));
            if (!ok) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
