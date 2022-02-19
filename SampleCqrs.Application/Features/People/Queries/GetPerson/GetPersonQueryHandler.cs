using Mapster;
using SampleCqrs.Application.Abstractions.Messaging;
using SampleCqrs.Application.Contract;
using SampleCqrs.Application.Features.People.Queries.GetPerson;
using SampleCqrs.Domain.People;

namespace SampleCqrs.Application.Features.People.Queries.GetPeople
{
    public sealed record GetPersonQueryHandler : IQueryHandler<GetPersonQuery, PersonDto> 
    {
        //
        private readonly IPersonRepository _personRespository;
        //
        public GetPersonQueryHandler(IPersonRepository personRespository)
        {
            _personRespository = personRespository;
        }
        //
        public async Task<PersonDto> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            var person = await _personRespository.Find(request.id,cancellationToken);
            return person.Adapt<PersonDto>();
        }
        //
    }
}
