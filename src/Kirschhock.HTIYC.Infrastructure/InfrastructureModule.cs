using System;

using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Infrastructure.Configuration;
using Kirschhock.HTIYC.Infrastructure.DbModels;
using Kirschhock.HTIYC.Infrastructure.DbModels.Mappings;
using Kirschhock.HTIYC.Infrastructure.DbModels.Mappings.Abstractions;
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
            serviceDescriptors.AddMediatR(typeof(InfrastructureModule).Assembly);

            // Configure mappers
            serviceDescriptors.AddScoped<IMapper<Fact, FactDTO>, FactMapper>();
            serviceDescriptors.AddScoped<IMapper<FactDTO, Fact>, FactMapper>();

            serviceDescriptors.AddScoped<IMapper<Topic, TopicDTO>, TopicMapper>();
            serviceDescriptors.AddScoped<IMapper<TopicDTO, Topic>, TopicMapper>();

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
