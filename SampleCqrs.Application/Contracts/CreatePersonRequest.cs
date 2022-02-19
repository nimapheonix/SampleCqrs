namespace SampleCqrs.Application.Contracts
{
    public sealed record CreatePersonRequest(string FirstName, string LastName);
}
