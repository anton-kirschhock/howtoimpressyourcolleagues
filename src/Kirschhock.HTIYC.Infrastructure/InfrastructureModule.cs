using System;

using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Infrastructure.Configuration;
using Kirschhock.HTIYC.Infrastructure.MongoDb;

using MediatR;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using MongoDB.Driver;

namespace Kirschhock.HTIYC.Infrastructure
{
    // Helping Delegates for DI 
    /// <summary>
    /// Factory delegate to get a configured <see cref="IMongoClient"/>
    /// </summary>
    /// <returns><see cref="IMongoClient"/></returns>
    public delegate IMongoClient MongoDbClientFactory();

    public static class InfrastructureModule
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceDescriptors,
                                                           Action<DatabaseConfiguration> configureDatabase
            )
        {
            // Configure all Configurations
            serviceDescriptors.Configure(configureDatabase);

            // Configure MediatR
            serviceDescriptors.AddMediatR(typeof(InfrastructureModule).Assembly,
                                          typeof(DomainModule).Assembly);

            // Add own modules
            serviceDescriptors.AddDomain();

            // Custom Services
            serviceDescriptors.AddScoped<MongoDbContext>();

            // Add Factories
            serviceDescriptors.AddScoped<MongoDbClientFactory>((sp) =>
            {
                return () =>
                {
                    var options = sp.GetRequiredService<IOptions<DatabaseConfiguration>>();
                    return new MongoClient(options.Value.ConnectionString);
                };
            });

            return serviceDescriptors;
        }
    }
}
