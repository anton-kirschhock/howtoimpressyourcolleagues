using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Common.Abstractions;
using Kirschhock.HTIYC.Domain.DomainEvents.Topics;

using MongoDB.Driver;

namespace Kirschhock.HTIYC.Infrastructure.Topics.CommandHandlers
{
    internal class UpdateTopicCommandHandler : ICommandHandler<UpdateTopicCommand>
    {
        private readonly HTIYCContext context;

        public UpdateTopicCommandHandler(HTIYCContext context)
        {
            this.context = context;
        }

        public async Task Handle(UpdateTopicCommand notification, CancellationToken cancellationToken)
        {
            var topic = context.Topics.FirstOrDefault(e => e.Id == notification.TopicToUpdate.Id);

            if (topic != null)
            {
                topic.Description = notification.TopicToUpdate.Description;
                topic.DisplayName = notification.TopicToUpdate.DisplayName;
                await context.SaveChangesAsync();
            }

        }
    }
}
