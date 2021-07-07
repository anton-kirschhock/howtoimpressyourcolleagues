using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Common.Abstractions;
using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Domain.Factories;
using Kirschhock.HTIYC.Infrastructure.DbModels;
using Kirschhock.HTIYC.Infrastructure.MongoDb;

using MongoDB.Driver;

namespace Kirschhock.HTIYC.Infrastructure.Topics.Queries.Handlers
{
    internal class TopicQueryHandler : IQueryHandler<TopicQuery, Topic>
    {
        private readonly MongoDbContext mongoDbContext;
        private readonly IAggregateFactory<Topic> topicFactory;

        public TopicQueryHandler(MongoDbContext mongoDbContext,
                                 IAggregateFactory<Topic> topicFactory)
        {
            this.mongoDbContext = mongoDbContext;
            this.topicFactory = topicFactory;
        }

        public async Task<IQueryable<Topic>> Handle(TopicQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<TopicDTO>.Filter.Empty;
            var projection = Builders<TopicDTO>.Projection
                                               .Expression(e => new { e.Id, e.Name, e.DisplayName, e.Description });
            var res = await mongoDbContext.Topics.Find(filter)
                                                 .Project(projection)
                                                 .Skip(request.Skip)
                                                 .Limit(request.Take)
                                                 .ToListAsync(cancellationToken: cancellationToken)
                                                 ;

            return res.Select(e => topicFactory.CreateBlank().Set(e.Id,
                                                                  e.Name,
                                                                  e.DisplayName,
                                                                  e.Description,
                                                                  null)).AsQueryable();
        }
    }
}
