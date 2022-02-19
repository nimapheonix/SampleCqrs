using SampleCqrs.Application.Abstractions.Messaging;

namespace SampleCqrs.Application.Features.People.Commands.CreatePerson
{
    public sealed class CreatePersonCommand : ICommand<string>
    {
        //
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //
        public CreatePersonCommand() { }
        //
        public CreatePersonCommand(string firstName, string lastName) : this() => (FirstName, LastName) = (firstName, lastName);
        //
    }

}
