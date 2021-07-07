using System.Threading.Tasks;

using MediatR;

namespace Kirschhock.HTIYC.Domain.DomainEvents
{
    internal class DomainEventManager : IDomainEventManager
    {
        private readonly IMediator mediator;

        public DomainEventManager(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task RaiseEventAsync<T>(T @event)
            where T : INotification
            => await mediator.Publish(@event);


        public async Task<TResponse> RaiseEventAndWaitAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<TResponse>
            => await mediator.Send(request);

    }
}
