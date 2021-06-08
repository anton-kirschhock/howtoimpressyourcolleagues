
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Infrastructure.DbModels.Mappings.Abstractions;
using Kirschhock.HTIYC.Infrastructure.MongoDb;

namespace Kirschhock.HTIYC.Infrastructure.DbModels.Mappings
{
    internal class SimpleTopicMapper : ISimpleTopicMapper
    {
        private readonly MongoDbContext context;
        private readonly IMapper<FactDTO, Fact> factDtoMapper;

        public SimpleTopicMapper(MongoDbContext context,
                           ISimpleFactMapper factDtoMapper)
        {
            this.context = context;
            this.factDtoMapper = factDtoMapper;
        }

        public Task<TopicDTO> MapAsync(Topic source, params KeyValuePair<string, object>[] args)
            => Task.FromResult(new TopicDTO()
            {
                Id = source.Id,
                Name = source.Name,
                DisplayName = source.DisplayName,
                Description = source.Description,
                Facts = source.Facts.Select(e => e.Id).ToList()
            });

        public Task<Topic> MapAsync(TopicDTO source, params KeyValuePair<string, object>[] args)
        {
            var res = new Topic();
            res.Set(source.Id,
                    source.Name,
                    source.DisplayName,
                    source.Description,
                    new List<Fact>());

            return Task.FromResult(res);
        }
    }
}
