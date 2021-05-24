using Kirschhock.HTIYC.Common.Abstractions;

namespace Kirschhock.HTIYC.Domain.DomainEvents.Topics
{
    public class AddTopicCommand: ICommand
    {
        public Topic Topic { get; protected set; }
        public AddTopicCommand(Topic topic)
        {
            Topic = topic;
        }
    }
}