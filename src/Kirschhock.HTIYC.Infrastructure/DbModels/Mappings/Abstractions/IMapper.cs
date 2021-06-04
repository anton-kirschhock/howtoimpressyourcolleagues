using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kirschhock.HTIYC.Infrastructure.DbModels.Mappings.Abstractions
{
    public interface IMapper<TSource, TDestination>
        where TSource : class
        where TDestination : class
    {
        Task<TDestination> MapAsync(TSource source, params KeyValuePair<string, object>[] args);
    }
}
