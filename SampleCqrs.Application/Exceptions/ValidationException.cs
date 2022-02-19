using SampleCqrs.Domain.Exceptions;

namespace SampleCqrs.Application.Exceptions
{
    public sealed class ValidationException : DomainException
    {
        public ValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary)
            : base("Validation Failure", "One or more validation error occurred")
            => ErrorsDictionary = errorsDictionary;
        public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }
    }
}
