
using Kirschhock.HTIYC.Domain;

namespace Kirschhock.HTIYC.Infrastructure.DbModels.Mappings.Abstractions
{
    /// <summary>
    /// Use this mapper to map a <see cref="FactDTO"/> to a <see cref="Fact"/> with complex resolving logic
    /// </summary>
    public interface IComplexFactMapper : IMapper<FactDTO, Fact>
    {
        public const string ResolveTopic = "ResolveTopic";
        public const string UseTopic = "UseTopic";

    }
}
