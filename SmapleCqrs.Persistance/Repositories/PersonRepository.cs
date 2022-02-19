using SampleCqrs.Domain.Common;
using SampleCqrs.Domain.People;

namespace SmapleCqrs.Persistance.Repositories
{
    public sealed class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(SampleCqrsDbContext context) : base(context) { }
    }
}
