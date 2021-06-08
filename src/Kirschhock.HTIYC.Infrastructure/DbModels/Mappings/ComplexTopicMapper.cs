
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Infrastructure.DbModels.Mappings.Abstractions;
using Kirschhock.HTIYC.Infrastructure.MongoDb;

namespace Kirschhock.HTIYC.Infrastructure.DbModels.Mappings
{
    internal class ComplexTopicMapper : IComplexTopicMapper
    {
        private readonly MongoDbContext context;
        private readonly IMapper<FactDTO, Fact> factDtoMapper;
        private readonly ISimpleTopicMapper simpleTopicMapper;

        public ComplexTopicMapper(MongoDbContext context,
                           ISimpleFactMapper factDtoMapper,
                           ISimpleTopicMapper simpleTopicMapper)
        {
            this.context = context;
            this.factDtoMapper = factDtoMapper;
            this.simpleTopicMapper = simpleTopicMapper;
        }

        public async Task<Topic> MapAsync(TopicDTO source, params KeyValuePair<string, object>[] args)
        {
            var resolveFacts = args?.Any(e => e.Key == ISimpleTopicMapper.ResolveFacts && e.Value is bool val && val) ?? false;
            var res = new Topic();
            List<Fact> facts = null;
            if (resolveFacts)
                foreach (var factId in source.Facts)
                {
                    var factDto = await context.FindOneAsync<FactDTO>(f => f.Id == factId);
                    var fact = await factDtoMapper.MapAsync(
                         factDto,
                         new KeyValuePair<string, object>(ISimpleFactMapper.UseTopic, res)
                      );

                    facts.Add(fact);
                }

            var topic = await simpleTopicMapper.MapAsync(source);
            topic.Facts = facts;
            return topic;
        }
    }
}
