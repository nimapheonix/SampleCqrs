using MediatR;

namespace SampleCqrs.Application.Abstractions.Messaging
{
    public interface ICommand<out TResponse> : IRequest<TResponse> { }
}
