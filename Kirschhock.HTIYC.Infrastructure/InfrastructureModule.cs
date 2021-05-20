using System;

using Kirschhock.HTIYC.Infrastructure.Configuration;

using Microsoft.Extensions.DependencyInjection;

namespace Kirschhock.HTIYC.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceDescriptors,
                                                           Action<DatabaseConfiguration> configureDatabase
            )
        {

            return serviceDescriptors;
        }
    }
}
