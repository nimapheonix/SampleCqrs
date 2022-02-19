using Mapster;
using MediatR;
using SampleCqrs.Application.Abstractions.Messaging;
using SampleCqrs.Domain.Exceptions;
using SampleCqrs.Domain.People;

namespace SampleCqrs.Application.Features.People.Commands.UpdatePerson
{
    public sealed class UpdatePersonCommandHandler : ICommandHandler<UpdatePersonCommand, Unit>
    {
        //
        private readonly IPersonRepository _personResponsitory;
        //
        public UpdatePersonCommandHandler(IPersonRepository personResponsitory)
        {
            this._personResponsitory = personResponsitory;
        }
        //
        public async Task<Unit> Handle(UpdatePersonCommand command, CancellationToken cancellationToken)
        {
            //
            var person = await _personResponsitory.Find(command.Id, cancellationToken);
            if (person == null) { throw new NotFoundException(nameof(Person), command.Id); }
            if (!person.RowVersion.Equals(command.RowVersion)) { throw new OutOfDateDataException(nameof(Person)); }
            // mapping command to entity
            person.Update(command.FirstName, command.LastName);
            //
            await _personResponsitory.Update(person, cancellationToken);
            //
            return Unit.Value;
        }
        //
    }
}
