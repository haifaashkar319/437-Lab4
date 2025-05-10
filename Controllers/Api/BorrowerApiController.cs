using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Core.Domain.Entities;
using Application.Borrowers.Commands;
using Application.Borrowers.Queries;

namespace LibraryManagement.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BorrowersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BorrowersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Borrower>>> GetBorrowers()
        {
            var query = new GetBorrowersQuery();
            var borrowers = await _mediator.Send(query);
            return Ok(borrowers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Borrower>> GetBorrower(int id)
        {
            var query = new GetBorrowerByIdQuery(id);
            var borrower = await _mediator.Send(query);

            if (borrower == null)
                return NotFound();

            return Ok(borrower);
        }

        [HttpPost]
        public async Task<ActionResult<Borrower>> PostBorrower(CreateBorrowerCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdBorrowerId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBorrower), new { id = createdBorrowerId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBorrower(int id, UpdateBorrowerCommand command)
        {
            if (id != command.BorrowerId)
                return BadRequest();

            var result = await _mediator.Send(command);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrower(int id)
        {
            var command = new DeleteBorrowerCommand(id);
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
