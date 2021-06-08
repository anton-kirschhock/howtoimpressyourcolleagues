using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Infrastructure.DbModels.Mappings.Abstractions;
using Kirschhock.HTIYC.Infrastructure.MongoDb;

namespace Kirschhock.HTIYC.Infrastructure.DbModels.Mappings
{
    internal class ComplexFactMapper : IComplexFactMapper
    {
        private readonly MongoDbContext context;
        private readonly IMapper<TopicDTO, Topic> topicDtoMapper;
        private readonly ISimpleFactMapper simpleFactMapper;

        public ComplexFactMapper(MongoDbContext context,
                          IMapper<TopicDTO, Topic> topicDtoMapper,
                          ISimpleFactMapper simpleFactMapper)
        {
            this.context = context;
            this.topicDtoMapper = topicDtoMapper;
            this.simpleFactMapper = simpleFactMapper;
        }

        public async Task<Fact> MapAsync(FactDTO source, params KeyValuePair<string, object>[] args)
        {
            Topic topic = null;
            // When we get a topic delivered from the caller
            if (args.Any(e => e.Key == IComplexFactMapper.UseTopic && e.Value is Topic t && t != null))
            {

                topic = args.First(e => e.Key == IComplexFactMapper.UseTopic && e.Value is Topic).Value as Topic;
            }

            if (topic == null) // if we have no topic yet...
            {
                //... and we should resolve the topic, do it
                if (args?.Any(e => e.Key == IComplexFactMapper.ResolveTopic && e.Value is bool val && val) ?? false)
                {
                    var topicDto = await context.FindOneAsync<TopicDTO>(e => e.Id == source.TopicId);
                    topic = await topicDtoMapper.MapAsync(topicDto, new KeyValuePair<string, object>(ISimpleTopicMapper.ResolveFacts, false));
                }
                // Future: more optional topic related resolving stuff here
            }

            return await simpleFactMapper.MapAsync(source,
                                                   new KeyValuePair<string, object>(ISimpleFactMapper.UseTopic,
                                                                                    topic));
        }
    }
}
