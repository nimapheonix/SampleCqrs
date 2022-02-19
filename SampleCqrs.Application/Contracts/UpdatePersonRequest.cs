namespace SampleCqrs.Application.Contracts
{
    public sealed record UpdatePersonRequest(string Id, string RowVersion, string FirstName, string LastName);
}
