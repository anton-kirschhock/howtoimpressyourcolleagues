using System.Threading.Tasks;

namespace Kirschhock.HTIYC.Domain.Factories
{
    public interface IAggregateFactory<TAggregate>
    {
        TAggregate CreateBlank();

        Task<TAggregate> CreateAsync();
    }
}
