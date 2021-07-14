
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Infrastructure.Configuration;
using Kirschhock.HTIYC.Infrastructure.DbModels;

using Microsoft.Extensions.Options;

using MongoDB.Driver;

namespace Kirschhock.HTIYC.Infrastructure.MongoDb
{
    public class MongoDbContext
    {
        public IMongoClient Client { get; }
        public IMongoDatabase Database { get; }

        public IMongoCollection<TopicDTO> Topics => Database.GetCollection<TopicDTO>(nameof(TopicDTO));


        public MongoDbContext(MongoDbClientFactory clientFactory, IOptions<DatabaseConfiguration> databaseOptions)
        {
            this.Client = clientFactory();
            Database = Client.GetDatabase(databaseOptions.Value.DatabaseName);
        }

        public async Task<TDTO> FindOneAsync<TDTO>(Expression<Func<TDTO, bool>> predicate, CancellationToken cancellationToken = default)
            => await Database.GetCollection<TDTO>(nameof(TDTO))
                             .Find(predicate)
                             .FirstOrDefaultAsync(cancellationToken);
    }
}
