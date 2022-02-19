using FluentAssertions;
using SampleCqrs.Application.Features.People.Commands.UpdatePerson;
using SampleCqrs.Domain.Exceptions;
using SampleCqrs.Domain.People;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static SampleCqrs.Test.People.Configuration.PersistanceConfiguration;

namespace SampleCqrs.Test.People
{
    public class UpdatePersonSpec
    {
        //
        private readonly UpdatePersonCommandHandler _updatePersonCommandHandler;
        private readonly IPersonRepository _personRepository;
        //
        public UpdatePersonSpec()
        {
            _personRepository = GetPersistanceConfiguration();
            this._updatePersonCommandHandler = new UpdatePersonCommandHandler(_personRepository);
        }
        //
        [Theory]
        [InlineData("first_name", "last_name", "new_first_name", "new_last_name")]
        public async void Update_should_person_change_properties(
            string first_name,
            string last_name,
            string new_first_name,
            string new_last_name)
        {
            Person for_compare_person = new Person(first_name, last_name);
            Person for_change_person = new Person(
                for_compare_person.Id,
                for_compare_person.LastModified,
                for_compare_person.RowVersion,
                for_compare_person.FirstName,
                for_compare_person.LastName
                );
            // Assurance of exactly being the same
            for_change_person.Id.Should().Be(for_compare_person.Id);
            for_change_person.LastModified.Should().Be(for_compare_person.LastModified);
            for_change_person.RowVersion.Should().Be(for_compare_person.RowVersion);
            for_change_person.FirstName.Should().Be(for_compare_person.FirstName);
            for_change_person.LastName.Should().Be(for_compare_person.LastName);
            //
            await _personRepository.Add(for_change_person, CancellationToken.None);
            // updating person valued
            for_change_person.Update(new_first_name, new_last_name);
            await _personRepository.Update(for_change_person, CancellationToken.None);
            // refetch person from database asurrance changs saved
            for_change_person = await _personRepository.Find(for_compare_person.Id, CancellationToken.None);
            for_change_person.Should().NotBeNull();
            // chacking firstname and lastname chaned to destination value
            for_change_person.FirstName.Should().Be(new_first_name);
            for_change_person.LastName.Should().Be(new_last_name);
            // checkng row version is changed during update process
            for_change_person.RowVersion.Should().NotBe(for_compare_person.RowVersion);
            // checkng last modified date is changed during update process
            for_change_person.LastModified.Should().NotBe(for_compare_person.LastModified);
            //
        }
        //
        [Fact]
        public async void Cannot_update_person_when_someone_changed_data_after_yoour_ftech_before_your_saving()
        {

            Person person = new Person("one_name", "one_family");
            await _personRepository.Add(person, CancellationToken.None);
            //
            person.RowVersion = Guid.NewGuid().ToString();
            Func<Task> func = async () => await _personRepository.Update(person, CancellationToken.None);
            func.Should().ThrowAsync<OutOfDateDataException>();
        }
        //
        [Fact]
        public void Cannot_update_person_without_first_name()
        {
            var validator = new UpdatePersonCommandValidator();
            var validationResult = validator.Validate(new UpdatePersonCommand(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), string.Empty, "Some LastName"));
            validationResult.Errors.Should().Contain(x => x.PropertyName.Equals(nameof(Person.FirstName)) && x.ErrorCode.Equals("NotEmptyValidator"));
        }
        //
        [Fact]
        public void Cannot_update_person_with_first_name_lower_than_2_char()
        {
            var validator = new UpdatePersonCommandValidator();
            var validationResult = validator.Validate(new UpdatePersonCommand(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "a", "One LastName"));
            validationResult.Errors.Should().Contain(x => x.PropertyName.Equals(nameof(Person.FirstName)) && x.ErrorCode.Equals("MinimumLengthValidator"));
        }
        //
        [Fact]
        public void Cannot_update_person_without_last_name()
        {
            var validator = new UpdatePersonCommandValidator();
            var validationResult = validator.Validate(new UpdatePersonCommand(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "One FirstName", string.Empty));
            validationResult.Errors.Should().Contain(x => x.PropertyName.Equals(nameof(Person.LastName)) && x.ErrorCode.Equals("NotEmptyValidator"));
        }
        //
        [Fact]
        public void Cannot_update_person_with_last_name_lower_than_2_char()
        {
            var validator = new UpdatePersonCommandValidator();
            var validationResult = validator.Validate(new UpdatePersonCommand(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "One FirstName", "a"));
            validationResult.Errors.Should().Contain(x => x.PropertyName.Equals(nameof(Person.LastName)) && x.ErrorCode.Equals("MinimumLengthValidator"));
        }
        //
        [Fact]
        public void Cannot_update_without_id()
        {
            var validator = new UpdatePersonCommandValidator();
            var validationResult = validator.Validate(new UpdatePersonCommand(string.Empty, Guid.NewGuid().ToString(), "One FirstName", "One LastNme"));
            validationResult.Errors.Should().Contain(x => x.PropertyName.Equals(nameof(UpdatePersonCommand.Id)) && x.ErrorCode.Equals("NotEmptyValidator"));
        }
        //
        [Fact]
        public void Cannot_update_with_corrupted_id()
        {
            var validator = new UpdatePersonCommandValidator();
            var validationResult = validator.Validate(new UpdatePersonCommand("abcdefg", Guid.NewGuid().ToString(), "One FirstName", "One LastNme"));
            validationResult.Errors.Should().Contain(x => x.PropertyName.Equals(nameof(UpdatePersonCommand.Id)) && x.ErrorCode.Equals("ExactLengthValidator"));
        }
        //
        [Fact]
        public async void Cannot_update_with_not_exist_id()
        {
            Func<Task> func = async () => await _updatePersonCommandHandler.Handle(new UpdatePersonCommand(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "One FirstName", "One LastNme"), CancellationToken.None);
            func.Should().ThrowAsync<NotFoundException>();
        }
        //
    }
}