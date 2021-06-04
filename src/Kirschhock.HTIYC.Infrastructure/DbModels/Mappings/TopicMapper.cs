
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Infrastructure.DbModels.Mappings.Abstractions;
using Kirschhock.HTIYC.Infrastructure.MongoDb;

namespace Kirschhock.HTIYC.Infrastructure.DbModels.Mappings
{
    internal class TopicMapper : IMapper<Topic, TopicDTO>, IMapper<TopicDTO, Topic>
    {
        private readonly MongoDbContext context;
        private readonly IMapper<FactDTO, Fact> factDtoMapper;

        public TopicMapper(MongoDbContext context,
                           IMapper<FactDTO, Fact> factDtoMapper)
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

        public async Task<Topic> MapAsync(TopicDTO source, params KeyValuePair<string, object>[] args)
        {
            var resolveFacts = args?.Any(e => e.Key == "ResolveFacts" && e.Value is bool val && val) ?? false;
            var res = new Topic();
            List<Fact> facts = null;
            if (resolveFacts)
                foreach (var factId in source.Facts)
                {
                    var factDto = await context.FindOneAsync<FactDTO>(f => f.Id == factId);
                    var fact = await factDtoMapper.MapAsync(
                         factDto,
                         new KeyValuePair<string, object>("Topic", res)
                      );

                    facts.Add(fact);
                }

            res.Set(source.Id,
                    source.Name,
                    source.DisplayName,
                    source.Description,
                    facts);

            return res;
        }
    }
}
