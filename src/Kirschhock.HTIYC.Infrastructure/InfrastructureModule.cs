using System;

using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Infrastructure.Configuration;

using MediatR;

using Microsoft.EntityFrameworkCore;
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

            serviceDescriptors.AddDbContext<HTIYCContext>((ctx, opt) =>
            {
                opt.UseNpgsql(ctx.GetRequiredService<IOptions<DatabaseConfiguration>>().Value.ConnectionString, dbOpt =>
                {
                });
            });

            // Configure MediatR
            serviceDescriptors.AddMediatR(typeof(InfrastructureModule).Assembly);

            // Add own modules
            serviceDescriptors.AddDomain();

            return serviceDescriptors;
        }
    }
}
