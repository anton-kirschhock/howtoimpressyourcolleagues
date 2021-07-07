using System;
using System.Linq;

using MediatR;

namespace Kirschhock.HTIYC.Common.Abstractions
{
    public interface IQuery<TEntity> : IRequest<IQueryable<TEntity>>
    { }

    public interface IQueryById<TEntity, TIdentifier> : IRequest<TEntity>
    {
        TIdentifier Id { get; }
    }

    public interface IQueryById<TEntity> : IQueryById<TEntity, Guid> { }
}