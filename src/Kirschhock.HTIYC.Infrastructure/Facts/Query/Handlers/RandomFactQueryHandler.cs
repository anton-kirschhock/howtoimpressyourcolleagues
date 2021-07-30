using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Common.Abstractions;
using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Domain.Factories;
using Kirschhock.HTIYC.Infrastructure.DbModels;
using Kirschhock.HTIYC.Infrastructure.MongoDb;

using MediatR;

using MongoDB.Driver;

namespace Kirschhock.HTIYC.Infrastructure.Facts.Query.Handlers
{
    internal class RandomFactQueryHandler : IQueryHandler<RandomFactQuery, Fact>
    {
        private readonly MongoDbContext context;
        private readonly IAggregateFactory<Topic> topicFactory;
        private readonly IMediator mediator;

        public RandomFactQueryHandler(MongoDbContext context,
                                      IAggregateFactory<Topic> topicFactory)
        {
            this.context = context;
            this.topicFactory = topicFactory;
        }

        public async Task<IQueryable<Fact>> Handle(RandomFactQuery request, CancellationToken cancellationToken)
        {
            var topicTitle = request.Topic;
            if (string.IsNullOrWhiteSpace(topicTitle))
            {
                var allTopics = await context.Topics.Find(Builders<TopicDTO>.Filter.Empty)
                                                     .Project(Builders<TopicDTO>.Projection.Expression(e => new { e.Name }))
                                                     .ToListAsync(cancellationToken: cancellationToken);

                topicTitle = allTopics.ElementAt(new Random().Next(0, allTopics.Count-1)).Name;
            }

            var filter = Builders<TopicDTO>.Filter.Where(e => e.Name.ToLower() == topicTitle.ToLower());
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
                                                 .ToListAsync(cancellationToken: cancellationToken);

            var topic = res.Select(e => topicFactory.CreateBlank().Set(e.Id,
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
            
            var randomFact = topic.Facts.ElementAt(new Random().Next(0, topic.Facts.Count-1));

            return new List<Fact>() { randomFact }.AsQueryable();
        }
    }
}
