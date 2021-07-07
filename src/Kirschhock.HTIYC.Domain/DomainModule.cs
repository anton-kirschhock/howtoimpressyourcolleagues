
using Kirschhock.HTIYC.Domain.DomainEvents;
using Kirschhock.HTIYC.Domain.Factories;

using Microsoft.Extensions.DependencyInjection;

namespace Kirschhock.HTIYC.Domain
{
    public static class DomainModule
    {
        public static IServiceCollection AddDomain(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<IDomainEventManager, DomainEventManager>();
            serviceDescriptors.AddScoped<IAggregateFactory<Topic>, TopicFactory>();

            return serviceDescriptors;
        }
    }
}