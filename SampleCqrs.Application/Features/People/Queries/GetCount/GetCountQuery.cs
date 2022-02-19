using SampleCqrs.Application.Abstractions.Messaging;

namespace SampleCqrs.Application.Features.People.Queries.GetPeople
{
    public sealed record GetCountQuery() : IQuery<long>;
}
