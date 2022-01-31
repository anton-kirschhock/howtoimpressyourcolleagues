using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Common.Abstractions;
using Kirschhock.HTIYC.Domain;

using Microsoft.EntityFrameworkCore;

namespace Kirschhock.HTIYC.Infrastructure.Topics.Queries.Handlers
{
    internal class TopicQueryHandler : IQueryHandler<TopicQuery, Topic>
    {
        private readonly HTIYCContext context;

        public TopicQueryHandler(HTIYCContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<Topic>> Handle(TopicQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(context.Topics.AsNoTracking());
        }
    }
}
