using Microsoft.AspNetCore.Mvc;
using MediatR;
using AutoMapper;
using Application.Authors.DTOs;
using Application.Authors.Commands;
using Application.Authors.Queries;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthorsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchString)
        {
            var authors = await _mediator.Send(new GetAuthorsQuery(searchString));
            var viewModelList = _mapper.Map<List<Presentation.ViewModels.AuthorListViewModel>>(authors);
            return View(viewModelList);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var author = await _mediator.Send(new GetAuthorByIdQuery(id));
            if (author == null) return NotFound();

            var dto = _mapper.Map<AuthorDto>(author);
            return View(dto);
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

            var dto = _mapper.Map<CreateAuthorDto>(model);
            var command = new CreateAuthorCommand(dto.Name);
            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _mediator.Send(new GetAuthorByIdQuery(id));
            if (author == null) return NotFound();

            // Map domain entity to view model
            var viewModel = new AuthorEditViewModel
            {
                AuthorId = author.AuthorId,
                Name = author.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AuthorEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dto = _mapper.Map<UpdateAuthorDto>(model);
            var command = new UpdateAuthorCommand(dto.AuthorId, dto.Name);
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
                Name = author.Name
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
