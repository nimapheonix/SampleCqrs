using FluentValidation;
using MediatR;
using SampleCqrs.Application.Abstractions.Messaging;
using ValidationException = SampleCqrs.Application.Exceptions.ValidationException;

namespace SampleCqrs.Application.Bevaviors
{
    public sealed class QueryValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IQuery<TResponse>
    {
        //
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        //
        public QueryValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;
        //
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!_validators.Any()) { return await next(); }
            //
            var context = new ValidationContext<TRequest>(request);
            //
            var errorsDictionary = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .GroupBy(
                x => x.PropertyName,
                y => y.ErrorMessage,
                (propertyName, errorMessages) => new
                {
                    key = propertyName,
                    values = errorMessages.Distinct().ToArray()

                }).ToDictionary(x => x.key, y => y.values);
            if (errorsDictionary.Any())
            {
                throw new ValidationException(errorsDictionary);
            }
            return await next();
        }
    }
}
