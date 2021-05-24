using System;
using System.Threading.Tasks;

using MediatR;

namespace Kirschhock.HTIYC.Domain.DomainEvents
{
    public static class DomainEventManager
    {
        [ThreadStatic]
        internal static Func<IMediator> MediatorResolver;

        public static async Task RaiseEventAsync<T>(T @event)
            where T : INotification
            => await MediatorResolver().Publish(@event);
        

        public static async Task<TResponse> RaiseEventAndWaitAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<TResponse>
            => await MediatorResolver().Send(request);
    }
}
