
using System.Threading.Tasks;

using Kirschhock.HTIYC.Domain.DomainEvents;
using Kirschhock.HTIYC.Domain.DomainEvents.Topics;

namespace Kirschhock.HTIYC.Domain.Factories
{
    internal class TopicFactory : IAggregateFactory<Topic>
    {
        private readonly IDomainEventManager eventManager;

        public TopicFactory(IDomainEventManager eventManager)
        {
            this.eventManager = eventManager;
        }

        public Topic CreateBlank()
            => new(eventManager);

        public async Task<Topic> CreateAsync()
        {
            var topic = new Topic(eventManager);
            await eventManager.RaiseEventAsync(new AddTopicCommand(topic));
            return topic;
        }
    }
}
