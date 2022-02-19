using Mapster;
using SampleCqrs.Application.Abstractions.Messaging;
using SampleCqrs.Application.Contract;
using SampleCqrs.Domain.People;

namespace SampleCqrs.Application.Features.People.Queries.GetPeople
{
    public sealed record GetPeopleQueryHandler : IQueryHandler<GetPeopleQuery, List<PersonDto>>
    {
        //
        private readonly IPersonRepository _personRespository;
        //
        public GetPeopleQueryHandler(IPersonRepository personRespository)
        {
            _personRespository = personRespository;
        }
        //
        public async Task<List<PersonDto>> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
        {
            var issueTasks = await _personRespository.Get(cancellationToken,request.skip,request.take);
            return issueTasks.Adapt<List<PersonDto>>();
        }
        //
    }
}
