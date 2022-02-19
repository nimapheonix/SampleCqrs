using MediatR;
using SampleCqrs.Application.Abstractions.Messaging;

namespace SampleCqrs.Application.Features.People.Commands.DeletePerson
{
    public sealed record DeletePersonCommand(string id) : ICommand<Unit>;
}
