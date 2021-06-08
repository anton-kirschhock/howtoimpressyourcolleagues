
using Kirschhock.HTIYC.Domain;

namespace Kirschhock.HTIYC.Infrastructure.DbModels.Mappings.Abstractions
{
    /// <summary>
    /// Use this mapper to map a <see cref="Fact"/> to a <see cref="FactDTO"/> without any complex Resolving logic
    /// </summary>
    public interface ISimpleFactMapper : IMapper<Fact, FactDTO>, IMapper<FactDTO, Fact>
    {
        public const string UseTopic = "UseTopic";
    }
}
