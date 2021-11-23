using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Common.Abstractions;
using Kirschhock.HTIYC.Infrastructure.DbModels;
using Kirschhock.HTIYC.Infrastructure.MongoDb;

using MongoDB.Driver;

namespace Kirschhock.HTIYC.Infrastructure.Topics.Commands.Handlers
{
    internal class DeleteTopicCommandHandler : ICommandHandler<DeleteTopicCommand, bool>
    {
        private readonly MongoDbContext context;

        public DeleteTopicCommandHandler(MongoDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<TopicDTO>.Filter.Where(e => e.Id == request.TopicId);

            var topic = (await context.Topics.FindAsync(filter, cancellationToken: cancellationToken)).FirstOrDefault(cancellationToken);
            if (topic == null) return false;

            await context.Topics.DeleteOneAsync(filter, cancellationToken: cancellationToken);

            return true;
        }
    }
}
