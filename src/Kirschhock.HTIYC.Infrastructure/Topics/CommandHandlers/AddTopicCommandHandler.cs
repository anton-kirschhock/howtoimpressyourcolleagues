using System.Threading;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Common.Abstractions;
using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Domain.DomainEvents.Topics;
using Kirschhock.HTIYC.Infrastructure.DbModels;
using Kirschhock.HTIYC.Infrastructure.DbModels.Mappings.Abstractions;
using Kirschhock.HTIYC.Infrastructure.MongoDb;

namespace Kirschhock.HTIYC.Infrastructure.Topics.CommandHandlers
{
    internal class AddTopicCommandHandler : ICommandHandler<AddTopicCommand>
    {
        private readonly MongoDbContext mongoDbContext;
        private readonly IMapper<Topic, TopicDTO> mapper;

        public AddTopicCommandHandler(MongoDbContext mongoDbContext,
                                      IMapper<Topic, TopicDTO> mapper)
        {
            this.mongoDbContext = mongoDbContext;
            this.mapper = mapper;
        }

        public async Task Handle(AddTopicCommand notification, CancellationToken cancellationToken)
        {
            var dto = await mapper.MapAsync(notification.Topic);
            mongoDbContext.Topics.InsertOne(dto);
        }
    }
}