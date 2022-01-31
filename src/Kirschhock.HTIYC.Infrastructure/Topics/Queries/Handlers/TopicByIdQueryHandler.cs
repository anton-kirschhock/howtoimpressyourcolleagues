using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Common.Abstractions;
using Kirschhock.HTIYC.Domain;

namespace Kirschhock.HTIYC.Infrastructure.Topics.Queries.Handlers
{
    internal class TopicByIdQueryHandler : IQueryByIdHandler<TopicByIdQuery, Topic>
    {
        private readonly HTIYCContext context;

        public TopicByIdQueryHandler(HTIYCContext context)
        {
            this.context = context;
        }

        public Task<Topic> Handle(TopicByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(context.Topics.FirstOrDefault(e => e.Id == request.Id));
        }
    }
}
