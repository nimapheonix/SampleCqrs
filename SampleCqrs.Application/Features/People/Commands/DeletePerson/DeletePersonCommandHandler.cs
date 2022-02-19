using MediatR;
using SampleCqrs.Application.Abstractions.Messaging;
using SampleCqrs.Domain.Exceptions;
using SampleCqrs.Domain.People;

namespace SampleCqrs.Application.Features.People.Commands.DeletePerson
{
    public sealed class DeletePersonCommandHandler : ICommandHandler<DeletePersonCommand, Unit>
    {
        //
        private readonly IPersonRepository _personRespository;
        //
        public DeletePersonCommandHandler(IPersonRepository personRespository)
        {
            _personRespository = personRespository;
        }
        //
        public async Task<Unit> Handle(DeletePersonCommand command, CancellationToken cancellationToken)
        {
            var person = await _personRespository.Find(command.id, cancellationToken);
            //
            if (person == null)
            {
                throw new NotFoundException(nameof(Person), command.id);
            }
            //
            await _personRespository.Delete(person, cancellationToken);
            return Unit.Value;
        }
        //
    }
}
