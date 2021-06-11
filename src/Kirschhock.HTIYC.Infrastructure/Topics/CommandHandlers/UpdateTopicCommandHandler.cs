using System.Threading;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Common.Abstractions;
using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Domain.DomainEvents.Topics;
using Kirschhock.HTIYC.Infrastructure.DbModels;
using Kirschhock.HTIYC.Infrastructure.DbModels.Mappings.Abstractions;
using Kirschhock.HTIYC.Infrastructure.MongoDb;

using MongoDB.Driver;

namespace Kirschhock.HTIYC.Infrastructure.Topics.CommandHandlers
{
    internal class UpdateTopicCommandHandler : ICommandHandler<UpdateTopicCommand>
    {
        private readonly MongoDbContext mongoDbContext;
        private readonly IMapper<Topic, TopicDTO> mapper;

        public UpdateTopicCommandHandler(MongoDbContext mongoDbContext,
                                      IMapper<Topic, TopicDTO> mapper)
        {
            this.mongoDbContext = mongoDbContext;
            this.mapper = mapper;
        }

        public async Task Handle(UpdateTopicCommand notification, CancellationToken cancellationToken)
        {
            var dto = await mapper.MapAsync(notification.TopicToUpdate);
            await mongoDbContext.Topics.UpdateOneAsync(Builders<TopicDTO>
                                                               .Filter
                                                               .Eq(e => e.Id, notification.TopicToUpdate.Id),
                                                       Builders<TopicDTO>
                                                               .Update
                                                               .Set(e => e.DisplayName, dto.DisplayName)
                                                               .Set(e => e.Description, dto.Description)
                                                               .Set(e => e.Facts, dto.Facts)
                                                       );
        }
    }
}
