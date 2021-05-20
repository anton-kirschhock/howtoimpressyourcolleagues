
using Kirschhock.HTIYC.Common.Abstractions;

namespace Kirschhock.HTIYC.Domain.DomainEvents.Topics
{
    public class UpdateTopicCommand : ICommand
    {
        public Topic TopicToUpdate { get; set; }

        public UpdateTopicCommand(Topic topicToUpdate)
        {
            TopicToUpdate = topicToUpdate;
        }
    }
}
