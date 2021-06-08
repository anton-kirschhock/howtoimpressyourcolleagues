using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Infrastructure.DbModels.Mappings.Abstractions;

namespace Kirschhock.HTIYC.Infrastructure.DbModels.Mappings
{
    internal class SimpleFactMapper : ISimpleFactMapper
    {
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

        public Task<Fact> MapAsync(FactDTO source, params KeyValuePair<string, object>[] args)
        {
            Topic topic = null;
            // When we get a topic delivered from the caller
            if (args.Any(e => e.Key == ISimpleFactMapper.UseTopic && e.Value is Topic t && t != null))
            {
                topic = args.First(e => e.Key == ISimpleFactMapper.UseTopic && e.Value is Topic).Value as Topic;
            }

            return Task.FromResult(
                new Fact()
                .Set(topic,
                     source.Id,
                     source.Name,
                     source.Title,
                     source.Description,
                     source.ReadMoreLink)
                );
        }
    }
}
