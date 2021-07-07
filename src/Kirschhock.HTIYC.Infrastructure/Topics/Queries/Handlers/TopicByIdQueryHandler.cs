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
    internal class TopicByIdQueryHandler : IQueryByIdHandler<TopicByIdQuery, Topic>
    {
        private readonly MongoDbContext context;
        private readonly IAggregateFactory<Topic> topicFactory;

        public TopicByIdQueryHandler(MongoDbContext context,
                                     IAggregateFactory<Topic> topicFactory)
        {
            this.context = context;
            this.topicFactory = topicFactory;
        }

        public async Task<Topic> Handle(TopicByIdQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<TopicDTO>.Filter.Where(e => e.Id == request.Id);
            var projection = Builders<TopicDTO>.Projection
                                               .Expression(e => new
                                               {
                                                   e.Id,
                                                   e.Name,
                                                   e.DisplayName,
                                                   e.Description,
                                                   Facts = e.Facts.Select(fact => new
                                                   {
                                                       fact.Id,
                                                       fact.Name,
                                                       fact.Title,
                                                       fact.Description,
                                                       fact.ReadMoreLink,
                                                   })
                                               });

            var res = await context.Topics.Find(filter)
                                                 .Project(projection)
                                                 .Limit(1)
                                                 .ToListAsync(cancellationToken: cancellationToken)
                                                 ;

            return res.Select(e => topicFactory.CreateBlank().Set(e.Id,
                                                                  e.Name,
                                                                  e.DisplayName,
                                                                  e.Description,
                                                                  e.Facts.Select(fact => new Fact().Set(fact.Id,
                                                                                                        fact.Name,
                                                                                                        fact.Title,
                                                                                                        fact.Description,
                                                                                                        fact.ReadMoreLink)).ToList()
                                                                  ))
                        .FirstOrDefault();
        }
    }
}
