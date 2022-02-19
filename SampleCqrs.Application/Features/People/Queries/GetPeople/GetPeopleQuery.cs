using SampleCqrs.Application.Abstractions.Messaging;
using SampleCqrs.Application.Contract;

namespace SampleCqrs.Application.Features.People.Queries.GetPeople
{
    public sealed record GetPeopleQuery(int skip,int take) : IQuery<List<PersonDto>>;
}
