using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Common.Abstractions;

using MongoDB.Driver;

namespace Kirschhock.HTIYC.Infrastructure.Topics.Commands.Handlers
{
    internal class DeleteTopicCommandHandler : ICommandHandler<DeleteTopicCommand, bool>
    {
        private readonly HTIYCContext context;

        public DeleteTopicCommandHandler(HTIYCContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
        {
            var topic = context.Topics.FirstOrDefault(e => e.Id == request.TopicId);
            if (topic == null)
                return false;

            context.Topics.Remove(topic);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
