using System.Threading;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Common.Abstractions;
using Kirschhock.HTIYC.Domain.DomainEvents.Topics;

namespace Kirschhock.HTIYC.Infrastructure.Topics.CommandHandlers
{
    internal class AddTopicCommandHandler : ICommandHandler<AddTopicCommand>
    {
        public Task Handle(AddTopicCommand notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}