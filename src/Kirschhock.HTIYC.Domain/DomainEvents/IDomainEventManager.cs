using System.Threading.Tasks;

using MediatR;

namespace Kirschhock.HTIYC.Domain.DomainEvents
{
    public interface IDomainEventManager
    {
        Task<TResponse> RaiseEventAndWaitAsync<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>;
        Task RaiseEventAsync<T>(T @event) where T : INotification;
    }
}