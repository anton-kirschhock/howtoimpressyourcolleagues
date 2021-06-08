
using Kirschhock.HTIYC.Domain;

namespace Kirschhock.HTIYC.Infrastructure.DbModels.Mappings.Abstractions
{
    public interface ISimpleTopicMapper : IMapper<Topic, TopicDTO>, IMapper<TopicDTO, Topic>
    {
        public const string ResolveFacts = "ResolveFacts";
    }
}