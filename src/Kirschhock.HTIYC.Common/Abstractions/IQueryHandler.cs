using System;
using System.Linq;

using MediatR;

namespace Kirschhock.HTIYC.Common.Abstractions
{
    public interface IQueryHandler<TQuery, TEntity> : IRequestHandler<TQuery, IQueryable<TEntity>>
        where TQuery : IQuery<TEntity>
    { }

    public interface IQueryByIdHandler<TQuery, TEntity, TIndentifier> : IRequestHandler<TQuery, TEntity>
        where TQuery : IQueryById<TEntity, TIndentifier>
    { }

    public interface IQueryByIdHandler<TQuery, TEntity> : IQueryByIdHandler<TQuery, TEntity, Guid>
        where TQuery : IQueryById<TEntity>
    { }
}