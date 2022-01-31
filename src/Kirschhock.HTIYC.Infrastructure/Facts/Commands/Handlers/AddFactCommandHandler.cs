using System.Threading;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Common.Abstractions;
using Kirschhock.HTIYC.Domain.DomainEvents.Facts;

namespace Kirschhock.HTIYC.Infrastructure.Facts.Handlers
{
    internal class AddFactCommandHandler : ICommandHandler<AddFactCommand>
    {
        private readonly HTIYCContext context;

        public AddFactCommandHandler(HTIYCContext context)
        {
            this.context = context;
        }


        public Task Handle(AddFactCommand notification, CancellationToken cancellationToken)
        {
            context.Facts.Add(notification.FactToAdd);
            return Task.CompletedTask;
        }
    }
}
