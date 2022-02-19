using Moq;
using SampleCqrs.Domain.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SampleCqrs.Test.People.Configuration
{
    public static class PersistanceConfiguration
    {
        public static IPersonRepository GetPersistanceConfiguration()
        {
            List<Person> persons = new List<Person>()
            {
                new Person("person_1_first_name","person_1_last_name"),
                new Person("person_2_first_name","person_2_last_name"),
                new Person("person_3_first_name","person_3_last_name"),
                new Person("person_4_first_name","person_4_last_name"),
                new Person("person_5_first_name","person_5_last_name"),
                new Person("person_6_first_name","person_6_last_name"),
                new Person("person_7_first_name","person_7_last_name"),
                new Person("person_8_first_name","person_8_last_name"),
                new Person("person_9_first_name","person_9_last_name"),
                new Person("person_10_first_name","person_10_last_name"),
            };
            //
            var mockRepository = new Mock<IPersonRepository>();
            // simulate Count
            mockRepository.Setup(x => x.Count(It.IsAny<CancellationToken>())).Returns(async () =>
            {
                return persons.Count;
            });
            // simulate Get
            mockRepository.Setup(x => x.Get(It.IsAny<CancellationToken>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(
                (CancellationToken cancellation, int skip, int take) =>
                {
                    return persons.Skip(skip).Take(take);
                });

            // simulate Find
            mockRepository.Setup(x => x.Find(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync((string id, CancellationToken cancellationToken) =>
                {
                    return persons.FirstOrDefault(x => x.Id.Equals(id));
                }
            );
            //simulate Add
            mockRepository.Setup(x => x.Add(It.IsAny<Person>(), It.IsAny<CancellationToken>())).Callback((Person person, CancellationToken cancellationToken) =>
               {
                   persons.Add(person);
               });
            //simulate Add
            mockRepository.Setup(x => x.AddRange(It.IsAny<List<Person>>(), It.IsAny<CancellationToken>())).Callback((List<Person> people, CancellationToken cancellationToken) =>
            {
                persons.AddRange(people);
            });
            // simulate Update
            mockRepository.Setup(x => x.Update(It.IsAny<Person>(), It.IsAny<CancellationToken>())).Callback((Person person, CancellationToken cancellationToken) =>
               {
                   persons.FirstOrDefault(x => x.Id.Equals(person.Id))?.Update(person.FirstName, person.LastName);
               });
            // simulate Delete
            mockRepository.Setup(x => x.Delete(It.IsAny<Person>(), It.IsAny<CancellationToken>())).Callback((Person person, CancellationToken cancellationToken) =>
              {
                  persons.Remove(person);
              });
            //
            return mockRepository.Object;
        }
    }
}
