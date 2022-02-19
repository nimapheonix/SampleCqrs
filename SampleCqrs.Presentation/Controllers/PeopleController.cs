using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleCqrs.Application.Contract;
using SampleCqrs.Application.Contracts;
using SampleCqrs.Application.Features.People.Commands.CreatePerson;
using SampleCqrs.Application.Features.People.Commands.DeletePerson;
using SampleCqrs.Application.Features.People.Commands.UpdatePerson;
using SampleCqrs.Application.Features.People.Queries.GetPeople;
using SampleCqrs.Application.Features.People.Queries.GetPerson;

namespace SampleCqrs.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        //
        private readonly ISender _sender;
        //
        public PeopleController(ISender sender) => _sender = sender;
        //
        [HttpGet]
        [Route("Count")]
        [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPeople(CancellationToken cancellationToken)
        {
            // creating query
            var query = new GetCountQuery();
            //sending query to meditR
            var issueTasks = await _sender.Send(query, cancellationToken);
            // rutrun query result to client
            return Ok(issueTasks);
            //
        }
        //
        [HttpGet]
        [Route("GetPeople/{skip},{take}")]
        [ProducesResponseType(typeof(List<PersonDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPeople(CancellationToken cancellationToken, [FromRoute] int skip = 0, [FromRoute] int take = 0)
        {
            // creating query
            var query = new GetPeopleQuery(skip, take);
            //sending query to meditR
            var issueTasks = await _sender.Send(query, cancellationToken);
            // rutrun query result to client
            return Ok(issueTasks);
            //
        }
        //
        [HttpGet]
        [Route("GetPerson/{id}")]
        [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPerson(CancellationToken cancellationToken, [FromRoute] string id)
        {
            // creating query
            var query = new GetPersonQuery(id);
            // sending query to meditR
            var issueTasks = await _sender.Send(query, cancellationToken);
            // rutrun query result to client
            return Ok(issueTasks);
            //
        }
        //
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(PersonDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreatePersonRequest request, CancellationToken cancellationToken)
        {
            // mapping request to command 
            CreatePersonCommand command = request.Adapt<CreatePersonCommand>();
            // sending command to mediatR
            string id = await _sender.Send(command, cancellationToken);
            // return created entity to client
            return CreatedAtAction(nameof(GetPerson), new { id = id }, request);
            //
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdatePersonRequest request, CancellationToken cancellationToken)
        {
            // mapping request to command 
            UpdatePersonCommand command = request.Adapt<UpdatePersonCommand>();
            //  sending command to mediatR
            await _sender.Send(command, cancellationToken);
            // return to client
            return NoContent();
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] string id, CancellationToken cancellationToken)
        {
            // mapping request to command 
            DeletePersonCommand command = new DeletePersonCommand(id);
            //  sending command to mediatR
            await _sender.Send(command, cancellationToken);
            // return to client
            return NoContent();
        }

    }
}
