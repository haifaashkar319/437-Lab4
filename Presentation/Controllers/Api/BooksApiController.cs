using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Core.Domain.Entities;
using Application.Books.Commands;
using Application.Books.Queries;

namespace Presentation.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetBooks(int page = 1, int pageSize = 10, string? search = null)
        {
            var query = new GetBooksQuery(search); 
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var query = new GetBookByIdQuery(id);
            var book = await _mediator.Send(query);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(CreateBookCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdBookId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBook), new { id = createdBookId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, UpdateBookCommand command)
        {
            if (id != command.BookId)
                return BadRequest();

            var result = await _mediator.Send(command);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var command = new DeleteBookCommand(id);
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
