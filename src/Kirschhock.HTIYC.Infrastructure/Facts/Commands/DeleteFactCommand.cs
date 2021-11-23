using System;

using Kirschhock.HTIYC.Common.Abstractions;

namespace Kirschhock.HTIYC.Infrastructure.Facts.Commands
{
    public class DeleteFactCommand : ICommand<bool>
    {
        public Guid TopicId { get; set; }
        public Guid FactId { get; set; }

        public DeleteFactCommand(Guid topicId, Guid factId)
        {
            TopicId = topicId;
            FactId = factId;
        }
    }
}
