using Kirschhock.HTIYC.Domain.DomainEvents;
using Microsoft.Extensions.DependencyInjection;

namespace Kirschhock.HTIYC.Domain
{
    public static class DomainModule
    {
        public static IServiceCollection AddDomain(this IServiceCollection serviceDescriptors)
        {
            DomainEventManager.MediatorResolver = () =>
            {
                // TODO
                throw new System.NotImplementedException();
            };

            return serviceDescriptors;
        }
    }
}