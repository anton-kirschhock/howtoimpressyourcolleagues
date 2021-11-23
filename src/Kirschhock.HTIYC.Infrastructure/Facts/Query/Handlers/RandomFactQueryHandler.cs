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
        class SimplifiedTopic
        {
            public string Name { get; set; }
        }

        public Task<IQueryable<Fact>> Handle(RandomFactQuery request, CancellationToken cancellationToken)
        {
            var topicTitle = request.Topic;
            if (string.IsNullOrWhiteSpace(topicTitle))
            {
                var allTopics = context.Topics.FindSync(Builders<TopicDTO>.Filter.Empty,
                                                        new FindOptions<TopicDTO, SimplifiedTopic>()
                                                        {
                                                            Projection = Builders<TopicDTO>.Projection.Expression(e => new SimplifiedTopic { Name = e.Name })
                                                        }
                                                        ).ToList();

                topicTitle = allTopics.ElementAt(new Random().Next(0, Math.Max(0, allTopics.Count - 1))).Name;
            }

            var filter = Builders<TopicDTO>.Filter.Where(e => e.Name.ToLower() == topicTitle.ToLower());
            var projection = Builders<TopicDTO>.Projection
                                               .Expression(e => new TopicDTO
                                               {
                                                   Id = e.Id,
                                                   Name = e.Name,
                                                   DisplayName = e.DisplayName,
                                                   Description = e.Description,
                                                   Facts = e.Facts.Select(fact => new FactDTO
                                                   {
                                                       Id = fact.Id,
                                                       Name = fact.Name,
                                                       Title = fact.Title,
                                                       Description = fact.Description,
                                                       ReadMoreLink = fact.ReadMoreLink,
                                                   }).ToList()
                                               });

            var res = context.Topics.FindSync(filter, new FindOptions<TopicDTO, TopicDTO>() { Projection = projection }).ToList();

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

            var indexedCount = (topic.Facts?.Count - 1);
            Fact randomFact = null;

            if (indexedCount != null && indexedCount >= 0)
                randomFact = topic?.Facts?.ElementAt(new Random().Next(0, Math.Max(0, indexedCount.Value)));

            return Task.FromResult(new List<Fact>() { randomFact }.AsQueryable());
        }
    }
}
