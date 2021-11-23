using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Common.Abstractions;
using Kirschhock.HTIYC.Infrastructure.DbModels;
using Kirschhock.HTIYC.Infrastructure.MongoDb;

using MongoDB.Driver;

namespace Kirschhock.HTIYC.Infrastructure.Facts.Commands.Handlers
{
    internal class DeleteFactCommandHandler : ICommandHandler<DeleteFactCommand, bool>
    {
        private readonly MongoDbContext context;

        public DeleteFactCommandHandler(MongoDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(DeleteFactCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<TopicDTO>.Filter.Where(e => e.Id == request.TopicId);

            var topic = (await context.Topics.FindAsync(filter, cancellationToken: cancellationToken)).FirstOrDefault(cancellationToken);
            if (topic == null) return false;

            var fact = topic.Facts.FirstOrDefault(e => e.Id == request.FactId);
            topic.Facts.Remove(fact);
            var updateDefinition = Builders<TopicDTO>.Update.Set(e => e.Facts, topic.Facts);
            await context.Topics.UpdateOneAsync(filter, updateDefinition);

            return true;
        }
    }
}
