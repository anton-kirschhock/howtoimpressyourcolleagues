using System.Threading;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Common.Abstractions;
using Kirschhock.HTIYC.Domain.DomainEvents.Topics;

namespace Kirschhock.HTIYC.Infrastructure.Topics.CommandHandlers
{
    internal class AddTopicCommandHandler : ICommandHandler<AddTopicCommand>
    {
        private readonly HTIYCContext context;

        public AddTopicCommandHandler(HTIYCContext context)
        {
            this.context = context;
        }

        public Task Handle(AddTopicCommand notification, CancellationToken cancellationToken)
        {
            context.Topics.Add(notification.Topic);
            context.SaveChanges();

            return Task.CompletedTask;
        }
    }
}