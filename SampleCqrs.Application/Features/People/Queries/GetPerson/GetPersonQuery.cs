using SampleCqrs.Application.Abstractions.Messaging;
using SampleCqrs.Application.Contract;

namespace SampleCqrs.Application.Features.People.Queries.GetPerson
{
    public sealed record GetPersonQuery(string id) : IQuery<PersonDto>;
}
