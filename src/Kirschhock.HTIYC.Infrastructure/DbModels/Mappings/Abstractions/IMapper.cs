using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kirschhock.HTIYC.Infrastructure.DbModels.Mappings.Abstractions
{
    /// <summary>
    /// Mapper interface to map from a <see cref="TSource"/> to a <see cref="TDestination"/> type
    /// </summary>
    /// <typeparam name="TSource">The type where to map FROM</typeparam>
    /// <typeparam name="TDestination">the type to map TO</typeparam>
    public interface IMapper<TSource, TDestination>
        where TSource : class
        where TDestination : class
    {
        /// <summary>
        /// Maps a <see cref="TSource"/> to a <see cref="TDestination"/>
        /// </summary>
        /// <param name="source">The <see cref="TSource"/> type to map From</param>
        /// <param name="args">Optional arguments in a <see cref="KeyValuePair{string, object}"/></param>
        /// <returns>The mapped <see cref="TDestination"/></returns>
        Task<TDestination> MapAsync(TSource source, params KeyValuePair<string, object>[] args);
    }
}
