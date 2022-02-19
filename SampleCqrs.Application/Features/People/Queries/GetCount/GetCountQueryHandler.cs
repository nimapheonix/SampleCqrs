using SampleCqrs.Application.Abstractions.Messaging;
using SampleCqrs.Domain.People;

namespace SampleCqrs.Application.Features.People.Queries.GetPeople
{
    public sealed record GetCountQueryHandler : IQueryHandler<GetCountQuery, long>
    {
        //
        private readonly IPersonRepository _personRespository;
        //
        public GetCountQueryHandler(IPersonRepository personRespository)
        {
            _personRespository = personRespository;
        }
        //
        public async Task<long> Handle(GetCountQuery request, CancellationToken cancellationToken)
        {
            return await _personRespository.Count(cancellationToken);
        }
        //
    }
}
