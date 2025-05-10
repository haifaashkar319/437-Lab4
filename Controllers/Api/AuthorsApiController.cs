using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Core.Domain.Entities;
using Application.Authors.Commands;
using Application.Authors.Queries;

namespace LibraryManagement.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            var query = new GetAuthorsQuery();
            var authors = await _mediator.Send(query);
            return Ok(authors);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var query = new GetAuthorByIdQuery(id); // Use constructor instead of object initializer
            var author = await _mediator.Send(query);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        // POST: api/Authors
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(CreateAuthorCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdAuthorId = await _mediator.Send(command); // Assuming the handler returns the ID of the created author
            return CreatedAtAction(nameof(GetAuthor), new { id = createdAuthorId }, null); // Pass the ID and null for the body
        }

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, UpdateAuthorCommand command)
        {
            if (id != command.AuthorId)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var command = new DeleteAuthorCommand(id); // Use constructor instead of object initializer
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}