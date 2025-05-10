using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Core.Domain.Entities;
using Application.Loans.Commands;
using Application.Loans.Queries;

namespace LibraryManagement.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoansController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoansController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoans()
        {
            var query = new GetLoansQuery();
            var loans = await _mediator.Send(query);
            return Ok(loans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetLoan(int id)
        {
            var query = new GetLoanByIdQuery(id);
            var loan = await _mediator.Send(query);

            if (loan == null)
                return NotFound();

            return Ok(loan);
        }

        [HttpPost]
        public async Task<ActionResult<Loan>> PostLoan(CreateLoanCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdLoanId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetLoan), new { id = createdLoanId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoan(int id, UpdateLoanCommand command)
        {
            if (id != command.LoanId)
                return BadRequest();

            var result = await _mediator.Send(command);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var command = new DeleteLoanCommand(id);
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
