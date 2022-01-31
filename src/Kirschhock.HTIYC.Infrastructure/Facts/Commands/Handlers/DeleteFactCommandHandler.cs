using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Common.Abstractions;

using MongoDB.Driver;

namespace Kirschhock.HTIYC.Infrastructure.Facts.Commands.Handlers
{
    internal class DeleteFactCommandHandler : ICommandHandler<DeleteFactCommand, bool>
    {
        private readonly HTIYCContext context;

        public DeleteFactCommandHandler(HTIYCContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(DeleteFactCommand request, CancellationToken cancellationToken)
        {
            var topicToDelete = context.Facts.FirstOrDefault(e => e.Id == request.FactId);
            context.Entry(topicToDelete).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            return context.SaveChanges() == 1;
        }
    }
}
