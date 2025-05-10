using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Borrowers.Queries;
using Application.Borrowers.Commands;
using LibraryManagement.ViewModels;

namespace LibraryManagement.Controllers
{
    public class BorrowerController : Controller
    {
        private readonly IMediator _mediator;

        public BorrowerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchString)
        {
            var borrowers = await _mediator.Send(new GetBorrowersQuery(searchString));

            var viewModels = borrowers.Select(b => new BorrowerListViewModel
            {
                BorrowerId = b.BorrowerId,
                Name = b.Name.Value
            }).ToList();

            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var borrower = await _mediator.Send(new GetBorrowerByIdQuery(id));
            if (borrower == null) return NotFound();

            var viewModel = new BorrowerDetailViewModel
            {
                BorrowerId = borrower.BorrowerId,
                Name = borrower.Name.Value
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new BorrowerCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BorrowerCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var command = new CreateBorrowerCommand(model.Name);
            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var borrower = await _mediator.Send(new GetBorrowerByIdQuery(id));
            if (borrower == null) return NotFound();

            var viewModel = new BorrowerEditViewModel
            {
                BorrowerId = borrower.BorrowerId,
                Name = borrower.Name.Value
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BorrowerEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var command = new UpdateBorrowerCommand(model.BorrowerId, model.Name);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var borrower = await _mediator.Send(new GetBorrowerByIdQuery(id));
            if (borrower == null) return NotFound();

            var viewModel = new BorrowerDeleteViewModel
            {
                BorrowerId = borrower.BorrowerId,
                Name = borrower.Name.Value
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _mediator.Send(new DeleteBorrowerCommand(id));
            if (!success) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
