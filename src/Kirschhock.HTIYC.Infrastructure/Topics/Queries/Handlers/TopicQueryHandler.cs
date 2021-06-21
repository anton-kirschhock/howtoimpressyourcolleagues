using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Common.Abstractions;
using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Infrastructure.DbModels;
using Kirschhock.HTIYC.Infrastructure.DbModels.Mappings.Abstractions;
using Kirschhock.HTIYC.Infrastructure.MongoDb;

using MongoDB.Driver;

namespace Kirschhock.HTIYC.Infrastructure.Topics.Queries.Handlers
{
    internal class TopicQueryHandler : IQueryHandler<TopicQuery, Topic>
    {
        private readonly MongoDbContext mongoDbContext;

        public TopicQueryHandler(MongoDbContext mongoDbContext, ISimpleTopicMapper simpleTopicMapper)
        {
            this.mongoDbContext = mongoDbContext;
        }

        public async Task<IQueryable<Topic>> Handle(TopicQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<TopicDTO>.Filter.Empty;
            var projection = Builders<TopicDTO>.Projection
                                               .Expression(e => new Topic().Set(e.Id,
                                                                                e.Name,
                                                                                e.DisplayName,
                                                                                e.Description,
                                                                                new List<Fact>()));
            var res = await mongoDbContext.Topics.Find(filter)
                                                 .Project(projection)
                                                 .Skip(request.Skip)
                                                 .Limit(request.Take)
                                                 .ToListAsync(cancellationToken: cancellationToken)
                                                 ;

            return res.AsQueryable();
        }
    }
}
