using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Infrastructure.DbModels.Mappings.Abstractions;

namespace Kirschhock.HTIYC.Infrastructure.DbModels.Mappings
{
    internal class FactMapper : IMapper<Fact, FactDTO>, IMapper<FactDTO, Fact>
    {
        public Task<FactDTO> MapAsync(Fact source, params KeyValuePair<string, object>[] args)
        {
            throw new NotImplementedException();
        }

        public Task<Fact> MapAsync(FactDTO source, params KeyValuePair<string, object>[] args)
        {
            throw new NotImplementedException();
        }
    }
}
