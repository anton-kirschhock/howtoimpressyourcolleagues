using System;

using Kirschhock.HTIYC.Common.Abstractions;

namespace Kirschhock.HTIYC.Infrastructure.Topics.Commands
{
    public class DeleteTopicCommand : ICommand<bool>
    {
        public Guid TopicId { get; set; }

        public DeleteTopicCommand(Guid topicId)
        {
            TopicId = topicId;
        }
    }
}
