namespace SampleCqrs.Domain.Exceptions
{
    public sealed class NotFoundException : DomainException
    {
        public NotFoundException(string entityName,string id) : base("Not found", $"{entityName} with \"{id}\" id  not exist!") { }
    }
}
