using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Authors.Queries;
using Application.Authors.Commands;
using LibraryManagement.ViewModels;

namespace LibraryManagement.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchString)
        {
            var authors = await _mediator.Send(new GetAuthorsQuery(searchString));

            // Map domain entities to view models
            var viewModels = authors.Select(a => new AuthorListViewModel
            {
                AuthorId = a.AuthorId,
                Name = a.Name.Value
            }).ToList();

            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var author = await _mediator.Send(new GetAuthorByIdQuery(id));
            if (author == null) return NotFound();

            var viewModel = new AuthorDetailViewModel
            {
                AuthorId = author.AuthorId,
                Name = author.Name.Value // mapping AuthorName to string
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new AuthorCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var command = new CreateAuthorCommand(model.Name);
            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _mediator.Send(new GetAuthorByIdQuery(id));
            if (author == null) return NotFound();

            var viewModel = new AuthorEditViewModel
            {
                AuthorId = author.AuthorId,
                Name = author.Name.Value // mapping AuthorName to string
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ModelState.AddModelError("Name", "Name is required");
                var author = await _mediator.Send(new GetAuthorByIdQuery(id));
                return View(author);
            }

            var command = new UpdateAuthorCommand(id, name);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _mediator.Send(new GetAuthorByIdQuery(id));
            if (author == null) return NotFound();

            var viewModel = new AuthorDeleteViewModel
            {
                AuthorId = author.AuthorId,
                Name = author.Name.Value // mapping AuthorName to string
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _mediator.Send(new DeleteAuthorCommand(id));
            if (!success) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
