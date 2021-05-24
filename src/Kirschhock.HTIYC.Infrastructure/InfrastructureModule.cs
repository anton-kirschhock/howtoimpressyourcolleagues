using System;
using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Infrastructure.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Kirschhock.HTIYC.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceDescriptors,
                                                           Action<DatabaseConfiguration> configureDatabase
            )
        {
            serviceDescriptors.AddDomain();
            
            serviceDescriptors.AddMediatR(typeof(InfrastructureModule).Assembly,
                                          typeof(DomainModule).Assembly);
            return serviceDescriptors;
        }
    }
}
