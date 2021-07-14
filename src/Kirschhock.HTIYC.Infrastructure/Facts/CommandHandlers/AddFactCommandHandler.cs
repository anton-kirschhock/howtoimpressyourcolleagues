using System.Threading;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Common.Abstractions;
using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Domain.DomainEvents.Facts;
using Kirschhock.HTIYC.Infrastructure.DbModels;
using Kirschhock.HTIYC.Infrastructure.DbModels.Mappings.Abstractions;
using Kirschhock.HTIYC.Infrastructure.MongoDb;

using MongoDB.Driver;

namespace Kirschhock.HTIYC.Infrastructure.Facts.CommandHandlers
{
    internal class AddFactCommandHandler : ICommandHandler<AddFactCommand>
    {
        private readonly MongoDbContext context;
        private readonly IMapper<Fact, FactDTO> mapper;

        public AddFactCommandHandler(MongoDbContext context,
                                     IMapper<Fact, FactDTO> mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        public async Task Handle(AddFactCommand notification, CancellationToken cancellationToken)
        {
            var filter = Builders<TopicDTO>.Filter.Where(e => e.Id == notification.ParentTopic.Id);

            var topic = (await context.Topics.FindAsync(filter, cancellationToken: cancellationToken)).FirstOrDefault(cancellationToken);
            if (topic == null) return;

            var fact = await mapper.MapAsync(notification.FactToAdd);
            topic.Facts.Add(fact);
            var updateDefinition = Builders<TopicDTO>.Update.Set(e => e.Facts, topic.Facts);
            await context.Topics.UpdateOneAsync(filter, updateDefinition);
        }
    }
}
