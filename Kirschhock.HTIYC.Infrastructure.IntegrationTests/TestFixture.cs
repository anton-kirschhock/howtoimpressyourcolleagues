using System;
using System.IO;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Domain;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kirschhock.HTIYC.Infrastructure.IntegrationTests
{
    public class TestFixture : IDisposable
    {
        public IServiceProvider ServiceProvider { get; private set; }
        private readonly IServiceScope scope;
        public TestFixture()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("TestConfiguration.json")
                .Build();

            var serviceCollection = new ServiceCollection()
                .AddInfrastructure(dbOpt => configuration.GetSection("database").Bind(dbOpt))
                .AddDomain();

            var nonScopedSp = serviceCollection.BuildServiceProvider();
            scope = BeginServiceScope(nonScopedSp);

            Task.Run(async () => await SeedTestDataAsync()).Wait();
        }

        public IServiceScope BeginServiceScope(IServiceProvider nonScopedServiceProvider)
        {
            var scope = nonScopedServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            ServiceProvider = scope.ServiceProvider;

            return scope;
        }
        public async Task ClearTestDataAsync()
        {
            var dbContext = ServiceProvider.GetRequiredService<HTIYCContext>();
            // TODO Delete everything
        }

        public async Task SeedTestDataAsync()
        {
            var dbContext = ServiceProvider.GetRequiredService<HTIYCContext>();

            //for (var i = 0; i < 5; i++)
            //{
            //    var topic = new TopicDTO
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = $"topic{i}",
            //        Description = $"A test topic with idx {i}",
            //        DisplayName = $"Topic {i}"
            //    };

            //    for (var j = 0; j < 5; j++)
            //    {
            //        var fact = new FactDTO
            //        {
            //            Id = Guid.NewGuid(),
            //            Name = $"fact{i}-{j}",
            //            Description = $"A test Fact with idx {j} for topic {i}",
            //            ReadMoreLink = "https://kirschhock.com",
            //            Title = $"Fact {i}-{j}",
            //        };
            //        await dbContext.Facts.InsertOneAsync(fact);
            //        topic.Facts.Add(fact);

            //    }
            //}
        }

        public async void Dispose()
        {
            await ClearTestDataAsync();
        }
    }
}
