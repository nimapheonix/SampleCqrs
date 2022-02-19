using FluentAssertions;
using SampleCqrs.Application.Features.People.Commands.CreatePerson;
using SampleCqrs.Domain.People;
using System.Threading;
using Xunit;
using static SampleCqrs.Test.People.Configuration.PersistanceConfiguration;


namespace SampleCqrs.Test.People.Commands
{
    public class CreatePersonSpec
    {
        //
        private readonly CreatePersonCommandHandler _createPersonCommandHandler;
        private readonly IPersonRepository _personRepository;
        //
        public CreatePersonSpec()
        {
            _personRepository = GetPersistanceConfiguration();
            this._createPersonCommandHandler = new CreatePersonCommandHandler(_personRepository);
        }
        //
        [Fact]
        public async void Adding_person_should_increase_total_count()
        {
            long beforCount = await this._personRepository.Count(CancellationToken.None);
            await _createPersonCommandHandler.Handle(new CreatePersonCommand("Another Person", "Another Person Family"), CancellationToken.None);
            long afterCount = await this._personRepository.Count(CancellationToken.None);
            afterCount.Should().Be(beforCount + 1);
        }
        //
        [Fact]
        public async void Cannot_insert_person_without_first_name()
        {
            var validator = new CreatePersonCommandValidator();
            var validationResult = validator.Validate(new CreatePersonCommand(string.Empty, "Some LastName"));
            validationResult.Errors.Should().Contain(x => x.PropertyName.Equals(nameof(Person.FirstName)) && x.ErrorCode.Equals("NotEmptyValidator"));
        }
        //
        [Fact]
        public async void Cannot_insert_person_with_first_name_lower_than_2_char()
        {
            var validator = new CreatePersonCommandValidator();
            var validationResult = validator.Validate(new CreatePersonCommand("a", "One LastName"));
            validationResult.Errors.Should().Contain(x => x.PropertyName.Equals(nameof(Person.FirstName)) && x.ErrorCode.Equals("MinimumLengthValidator"));
        }
        //
        [Fact]
        public async void Cannot_insert_person_without_last_name()
        {
            var validator = new CreatePersonCommandValidator();
            var validationResult = validator.Validate(new CreatePersonCommand("One FirstName", string.Empty));
            validationResult.Errors.Should().Contain(x => x.PropertyName.Equals(nameof(Person.LastName)) && x.ErrorCode.Equals("NotEmptyValidator"));
        }
        //
        [Fact]
        public async void Cannot_insert_person_with_last_name_lower_than_2_char()
        {
            var validator = new CreatePersonCommandValidator();
            var validationResult = validator.Validate(new CreatePersonCommand("One FirstName", "a"));
            validationResult.Errors.Should().Contain(x => x.PropertyName.Equals(nameof(Person.LastName)) && x.ErrorCode.Equals("MinimumLengthValidator"));
        }
        //
    }
}
