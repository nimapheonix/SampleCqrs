using MediatR;
using SampleCqrs.Application.Abstractions.Messaging;

namespace SampleCqrs.Application.Features.People.Commands.UpdatePerson
{
    public sealed class UpdatePersonCommand : ICommand<Unit>
    {
        //
        public string Id { get;  set; }
        public string RowVersion { get;  set; }
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        //
        public UpdatePersonCommand() { }
        public UpdatePersonCommand(string id, string rowVersion, string firstName, string lastName) : this() => (Id, RowVersion, FirstName, LastName) = (id, rowVersion, firstName, lastName);
        //
    }
}
