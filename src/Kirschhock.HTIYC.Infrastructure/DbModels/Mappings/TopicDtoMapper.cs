
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Infrastructure.DbModels.Mappings.Abstractions;

namespace Kirschhock.HTIYC.Infrastructure.DbModels.Mappings
{
    internal class TopicDtoMapper : IMapper<Topic, TopicDTO>
    {

        public Task<TopicDTO> MapAsync(Topic source, params KeyValuePair<string, object>[] args)
            => Task.FromResult(new TopicDTO()
            {
                Id = source.Id,
                Name = source.Name,
                DisplayName = source.DisplayName,
                Description = source.Description,
                Facts = source.Facts.Select(fact => new FactDTO()
                {
                    Id = fact.Id,
                    Name = fact.Name,
                    Description = fact.Description,
                    ReadMoreLink = fact.ReadMoreLink,
                    Title = fact.Title
                }).ToList()
            });
    }
}
