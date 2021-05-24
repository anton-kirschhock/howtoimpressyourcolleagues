using System.Linq;

using MediatR;

namespace Kirschhock.HTIYC.Common.Abstractions
{
    public interface IQueryHandler<TQuery, TEntity> : IRequestHandler<TQuery, IQueryable<TEntity>>
        where TQuery : IQuery<TEntity>
    { }
}