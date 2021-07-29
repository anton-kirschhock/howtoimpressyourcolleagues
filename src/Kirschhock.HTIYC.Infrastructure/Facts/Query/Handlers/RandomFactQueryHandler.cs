using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Common.Abstractions;
using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Infrastructure.MongoDb;

namespace Kirschhock.HTIYC.Infrastructure.Facts.Query.Handlers
{
    internal class RandomFactQueryHandler : IQueryHandler<RandomFactQuery, Fact>
    {
        private readonly MongoDbContext context;

        public RandomFactQueryHandler(MongoDbContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<Fact>> Handle(RandomFactQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
