using FluentAssertions;
using SampleCqrs.Application.Features.People.Commands.DeletePerson;
using SampleCqrs.Domain.Exceptions;
using SampleCqrs.Domain.People;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static SampleCqrs.Test.People.Configuration.PersistanceConfiguration;

namespace SampleCqrs.Test.People.Commands
{
    public class DeletePersonSpec
    {
        //
        private readonly DeletePersonCommandHandler _deletePersonCommandHandler;
        private readonly IPersonRepository _personRepository;
        //
        public DeletePersonSpec()
        {
            _personRepository = GetPersistanceConfiguration();
            this._deletePersonCommandHandler = new DeletePersonCommandHandler(_personRepository);
        }
        //
        [Fact]
        public async void Deleting_person_should_decrease_total_count()
        {
            Person person = new Person("Someone Else", "One Family");
            await _personRepository.Add(person, CancellationToken.None);
            long beforCount = await this._personRepository.Count(CancellationToken.None);
            await _deletePersonCommandHandler.Handle(new DeletePersonCommand(person.Id), CancellationToken.None);
            long afterCount = await this._personRepository.Count(CancellationToken.None);
            afterCount.Should().Be(beforCount - 1);
        }
        //
        [Fact]
        public void Cannot_delete_without_id()
        {
            var validator = new DeletePersonCommandValidator();
            var validationResult = validator.Validate(new DeletePersonCommand(string.Empty));
            validationResult.Errors.Should().Contain(x => x.PropertyName.Equals(nameof(DeletePersonCommand.id)) && x.ErrorCode.Equals("NotEmptyValidator"));
        }
        //
        [Fact]
        public void Cannot_delete_with_corrupted_id()
        {
            var validator = new DeletePersonCommandValidator();
            var validationResult = validator.Validate(new DeletePersonCommand("fdsghfdahgsddsagh"));
            validationResult.Errors.Should().Contain(x => x.PropertyName.Equals(nameof(DeletePersonCommand.id)) && x.ErrorCode.Equals("ExactLengthValidator"));
        }
        //
        [Fact]
        public void Cannot_delete_with_not_exist_id()
        {
            Func<Task> func = async () => await _deletePersonCommandHandler.Handle(new DeletePersonCommand(Guid.NewGuid().ToString()), CancellationToken.None);
            func.Should().ThrowAsync<NotFoundException>();
        }
        //
    }
}
