using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Core.Domain.Entities;
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
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var loans = await _mediator.Send(new GetLoansQuery());

            var viewModels = loans.Select(l => new LoanListViewModel
            {
                LoanId = l.LoanId,
                BookTitle = l.Book.Title.Value,
                BorrowerName = l.Borrower.Name.Value,
                LoanDate = l.LoanDate,
                DueDate = l.DueDate
            }).ToList();

            return View(viewModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var loan = await _mediator.Send(new GetLoanByIdQuery(id));
            if (loan == null) return NotFound();

            var viewModel = new LoanDetailViewModel
            {
                LoanId = loan.LoanId,
                BookTitle = loan.Book.Title.Value,
                BorrowerName = loan.Borrower.Name.Value,
                LoanDate = loan.LoanDate,
                DueDate = loan.DueDate
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new LoanCreateViewModel
            {
                Books = _context.Books.Select(b => new SelectListItem
                {
                    Value = b.BookId.ToString(),
                    Text = b.Title.Value
                }),
                Borrowers = _context.Borrowers.Select(b => new SelectListItem
                {
                    Value = b.BorrowerId.ToString(),
                    Text = b.Name.Value
                })
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoanCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Books = _context.Books.Select(b => new SelectListItem
                {
                    Value = b.BookId.ToString(),
                    Text = b.Title.Value
                });
                model.Borrowers = _context.Borrowers.Select(b => new SelectListItem
                {
                    Value = b.BorrowerId.ToString(),
                    Text = b.Name.Value
                });

                return View(model);
            }

            var command = new CreateLoanCommand(model.BookId, model.BorrowerId, model.LoanDate, model.DueDate);
            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var loan = await _mediator.Send(new GetLoanByIdQuery(id));
            if (loan == null) return NotFound();

            var books = _context.Books.Select(b => new SelectListItem
            {
                Value = b.BookId.ToString(),
                Text = b.Title.Value
            });

            var borrowers = _context.Borrowers.Select(b => new SelectListItem
            {
                Value = b.BorrowerId.ToString(),
                Text = b.Name.Value
            });

            var viewModel = new LoanEditViewModel
            {
                LoanId = loan.LoanId,
                BookId = loan.BookId,
                BorrowerId = loan.BorrowerId,
                LoanDate = loan.LoanDate,
                DueDate = loan.DueDate,
                Books = books,
                Borrowers = borrowers
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LoanEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Books = _context.Books.Select(b => new SelectListItem
                {
                    Value = b.BookId.ToString(),
                    Text = b.Title.Value
                });

                model.Borrowers = _context.Borrowers.Select(b => new SelectListItem
                {
                    Value = b.BorrowerId.ToString(),
                    Text = b.Name.Value
                });

                return View(model);
            }

            var command = new UpdateLoanCommand(model.LoanId, model.BookId, model.BorrowerId, model.LoanDate, model.DueDate);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var loan = await _mediator.Send(new GetLoanByIdQuery(id));
            if (loan == null) return NotFound();

            var viewModel = new LoanDeleteViewModel
            {
                LoanId = loan.LoanId,
                BookTitle = loan.Book.Title.Value,
                BorrowerName = loan.Borrower.Name.Value,
                LoanDate = loan.LoanDate,
                DueDate = loan.DueDate
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _mediator.Send(new DeleteLoanCommand(id));
            if (!success) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
