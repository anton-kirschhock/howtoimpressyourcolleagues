
using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Infrastructure.Configuration;

using Microsoft.Extensions.Options;

using MongoDB.Driver;

namespace Kirschhock.HTIYC.Infrastructure.MongoDb
{
    public class MongoDbContext
    {
        public IMongoClient Client { get; }
        public IMongoDatabase Database { get; }

        public IMongoCollection<Topic> Topics => Database.GetCollection<Topic>(nameof(Topic));

        public MongoDbContext(MongoDbClientFactory clientFactory, IOptions<DatabaseConfiguration> databaseOptions)
        {
            this.Client = clientFactory();
            Database = Client.GetDatabase(databaseOptions.Value.DatabaseName);
        }
    }
}
