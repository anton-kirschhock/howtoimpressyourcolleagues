using System.Collections.Generic;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Domain.DomainEvents;
using Kirschhock.HTIYC.Infrastructure.DbModels.Mappings.Abstractions;

namespace Kirschhock.HTIYC.Infrastructure.DbModels.Mappings
{
    internal class FactDtoMapper : IMapper<Fact, FactDTO>
    {
        private readonly IDomainEventManager domainEventManager;

        public FactDtoMapper(IDomainEventManager domainEventManager)
        {
            this.domainEventManager = domainEventManager;
        }

        public Task<FactDTO> MapAsync(Fact source, params KeyValuePair<string, object>[] args)
            => Task.FromResult(new FactDTO
            {
                Id = source.Id,
                Description = source.Description,
                Name = source.Name,
                ReadMoreLink = source.ReadMoreLink,
                Title = source.Title
            });
    }
}
