using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Common.Abstractions;
using Kirschhock.HTIYC.Domain;

namespace Kirschhock.HTIYC.Infrastructure.Facts.Query.Handlers
{
    internal class RandomFactQueryHandler : IQueryHandler<RandomFactQuery, Fact>
    {
        private readonly HTIYCContext context;

        public RandomFactQueryHandler(HTIYCContext context)
        {
            this.context = context;
        }


        public Task<IQueryable<Fact>> Handle(RandomFactQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(context.Facts.OrderBy(r => Guid.NewGuid()).Take(1));
        }
    }
}
