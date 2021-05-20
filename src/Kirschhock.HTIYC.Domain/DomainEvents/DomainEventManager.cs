using System;
using System.Threading.Tasks;

using MediatR;

namespace Kirschhock.HTIYC.Domain.DomainEvents
{
    public static class DomainEventManager
    {
        [ThreadStatic]
        public static Func<IMediator> MediatorResolver;

        public static async Task RaiseEvent<T>(T @event)
            where T : INotification

        {
            await MediatorResolver().Publish(@event);
        }
    }
}
