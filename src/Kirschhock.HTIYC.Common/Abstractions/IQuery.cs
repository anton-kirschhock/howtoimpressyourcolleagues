using MediatR;
using System.Linq;

namespace Kirschhock.HTIYC.Common.Abstractions
{
    public interface IQuery<TEntity> : IRequest<IQueryable<TEntity>>
    { }
}