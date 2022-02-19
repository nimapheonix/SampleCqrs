namespace SampleCqrs.Domain.Exceptions
{
    public sealed class OutOfDateDataException : DomainException
    {
        public OutOfDateDataException(string entityName) : base(nameof(OutOfDateDataException), $"Your fetched {entityName} data is out of date, Please refresh data") { }
    }
}
