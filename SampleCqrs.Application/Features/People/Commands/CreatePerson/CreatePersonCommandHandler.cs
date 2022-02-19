using Mapster;
using SampleCqrs.Application.Abstractions.Messaging;
using SampleCqrs.Domain.People;

namespace SampleCqrs.Application.Features.People.Commands.CreatePerson
{
    public sealed class CreatePersonCommandHandler : ICommandHandler<CreatePersonCommand, string>
    {
        //
        private readonly IPersonRepository _personResponsitory;
        //
        public CreatePersonCommandHandler(IPersonRepository personResponsitory)
        {
            this._personResponsitory = personResponsitory;
        }
        //
        public async Task<string> Handle(CreatePersonCommand command, CancellationToken cancellationToken)
        {
            // mapping command to entity
            Person person = command.Adapt<Person>();
            //
            await _personResponsitory.Add(person, cancellationToken);
            //
            return person.Id;
        }
        //
    }
}
