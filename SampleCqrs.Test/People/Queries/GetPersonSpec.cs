using FluentAssertions;
using SampleCqrs.Application.Features.People.Queries.GetPeople;
using SampleCqrs.Application.Features.People.Queries.GetPerson;
using SampleCqrs.Domain.People;
using System.Threading;
using Xunit;
using static SampleCqrs.Test.People.Configuration.PersistanceConfiguration;

namespace SampleCqrs.Test.People.Queries
{
    public class GetPersonSpec
    {
        //
        private readonly GetPersonQueryHandler _getPeopleQueryHandler;
        private readonly IPersonRepository _personRepository;
        //
        public GetPersonSpec()
        {
            _personRepository = GetPersistanceConfiguration();
            this._getPeopleQueryHandler = new GetPersonQueryHandler(_personRepository);
        }
        //
        [Fact]
        public async void Get_person_must_work_fine()
        {
            var addPerson = new Person("first_name", "second_name");
            await _personRepository.Add(addPerson, CancellationToken.None);
            //
            GetPersonQuery query = new GetPersonQuery(addPerson.Id);
            var fetchedPerson = await _getPeopleQueryHandler.Handle(query, CancellationToken.None);
            //
            fetchedPerson.Should().NotBe(null);
            fetchedPerson.Id.Should().Be(addPerson.Id);
            fetchedPerson.LastModified.Should().Be(addPerson.LastModified);
            fetchedPerson.RowVersion.Should().Be(addPerson.RowVersion);
            fetchedPerson.FirstName.Should().Be(addPerson.FirstName);
            fetchedPerson.LastName.Should().Be(addPerson.LastName);
            //
        }
    }
}
