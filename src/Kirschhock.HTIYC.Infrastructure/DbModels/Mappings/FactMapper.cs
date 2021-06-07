using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Infrastructure.DbModels.Mappings.Abstractions;
using Kirschhock.HTIYC.Infrastructure.MongoDb;

namespace Kirschhock.HTIYC.Infrastructure.DbModels.Mappings
{
    internal class FactMapper : IMapper<Fact, FactDTO>, IMapper<FactDTO, Fact>
    {
        public const string ResolveTopic = "ResolveTopic";
        public const string UseTopic = "UseTopic";
        private readonly MongoDbContext context;
        private readonly IMapper<TopicDTO, Topic> topicDtoMapper;

        public FactMapper(MongoDbContext context,
                          IMapper<TopicDTO, Topic> topicDtoMapper)
        {
            this.context = context;
            this.topicDtoMapper = topicDtoMapper;
        }

        public Task<FactDTO> MapAsync(Fact source, params KeyValuePair<string, object>[] args)
            => Task.FromResult(new FactDTO
            {
                Id = source.Id,
                Description = source.Description,
                Name = source.Name,
                ReadMoreLink = source.ReadMoreLink,
                Title = source.Title,
                TopicId = source.Topic.Id
            });

        public async Task<Fact> MapAsync(FactDTO source, params KeyValuePair<string, object>[] args)
        {
            Topic topic = null;
            // When we get a topic delivered from the caller
            if (args.Any(e => e.Key == UseTopic && e.Value is Topic t && t != null))
            {

                topic = args.First(e => e.Key == UseTopic && e.Value is Topic).Value as Topic;
            }

            if (topic == null) // if we have no topic yet...
            {
                //... and we should resolve the topic, do it
                if (args?.Any(e => e.Key == ResolveTopic && e.Value is bool val && val) ?? false)
                {
                    var topicDto = await context.FindOneAsync<TopicDTO>(e => e.Id == source.TopicId);
                    topic = await topicDtoMapper.MapAsync(topicDto, new KeyValuePair<string, object>(TopicMapper.ResolveFacts, false));
                }
                // Future: more optional topic related resolving stuff here
            }
            return new Fact()
                .Set(topic,
                     source.Id,
                     source.Name,
                     source.Title,
                     source.Description,
                     source.ReadMoreLink);
        }
    }
}
