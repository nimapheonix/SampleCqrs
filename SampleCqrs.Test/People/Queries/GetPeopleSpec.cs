using FluentAssertions;
using Mapster;
using SampleCqrs.Application.Contract;
using SampleCqrs.Application.Features.People.Queries.GetPeople;
using SampleCqrs.Domain.People;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;
using static SampleCqrs.Test.People.Configuration.PersistanceConfiguration;

namespace SampleCqrs.Test.People.Queries
{
    public class GetPeopleSpec
    {
        //
        private readonly GetPeopleQueryHandler _getPeopleQueryHandler;
        private readonly IPersonRepository _personRepository;
        //
        public GetPeopleSpec()
        {
            _personRepository = GetPersistanceConfiguration();
            this._getPeopleQueryHandler = new GetPeopleQueryHandler(_personRepository);
        }
        //
        [Fact]
        public async void Get_people_must_fetch_values()
        {
            GetPeopleQuery query = new GetPeopleQuery(0, 0);
            List<PersonDto> people = await _getPeopleQueryHandler.Handle(query, CancellationToken.None);
            people.Count().Should().NotBe(0);
        }
        //
        [Fact]
        public async void Get_people_must_change_data_on_pagination()
        {
            List<Person> people = GetFakePeople();
            await _personRepository.AddRange(people, CancellationToken.None);
            //
            GetPeopleQuery query = new GetPeopleQuery(0, 0);
            List<PersonDto> fetchedPeople = await _getPeopleQueryHandler.Handle(query, CancellationToken.None);
            fetchedPeople.Count().Should().Be(0);
            //
            query = new GetPeopleQuery(0, 10);
            fetchedPeople = await _getPeopleQueryHandler.Handle(query, CancellationToken.None);
            fetchedPeople.Count().Should().Be(10);
            //
            query = new GetPeopleQuery(15, 10);
            fetchedPeople = await _getPeopleQueryHandler.Handle(query, CancellationToken.None);
            fetchedPeople.Should().Contain(people[5].Adapt<PersonDto>());
            fetchedPeople.Should().Contain(people[14].Adapt<PersonDto>());
            //
        }
        //
        private List<Person> GetFakePeople() => new List<Person>()
            {
                new Person("added_person_1_first_name","added_person_1_last_name"),
                new Person("added_person_2_first_name","added_person_2_last_name"),
                new Person("added_person_3_first_name","added_person_3_last_name"),
                new Person("added_person_4_first_name","added_person_4_last_name"),
                new Person("added_person_5_first_name","added_person_5_last_name"),
                new Person("added_person_6_first_name","added_person_6_last_name"),
                new Person("added_person_7_first_name","added_person_7_last_name"),
                new Person("added_person_8_first_name","added_person_8_last_name"),
                new Person("added_person_9_first_name","added_person_9_last_name"),
                new Person("added_person_10_first_name","added_person_10_last_name"),
                new Person("added_person_11_first_name","added_person_11_last_name"),
                new Person("added_person_12_first_name","added_person_12_last_name"),
                new Person("added_person_13_first_name","added_person_13_last_name"),
                new Person("added_person_14_first_name","added_person_14_last_name"),
                new Person("added_person_15_first_name","added_person_15_last_name"),
                new Person("added_person_16_first_name","added_person_16_last_name"),
                new Person("added_person_17_first_name","added_person_17_last_name"),
                new Person("added_person_18_first_name","added_person_18_last_name"),
                new Person("added_person_19_first_name","added_person_19_last_name"),
                new Person("added_person_20_first_name","added_person_20_last_name"),
            };
        //
    }
}
