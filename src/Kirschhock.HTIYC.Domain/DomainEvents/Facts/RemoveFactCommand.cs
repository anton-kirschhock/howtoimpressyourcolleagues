
using System;

using Kirschhock.HTIYC.Common.Abstractions;

namespace Kirschhock.HTIYC.Domain.DomainEvents.Facts
{
    public class RemoveFactCommand : ICommand
    {
        public IResource ParentTopic { get; set; }
        public IResource FactToRemove { get; set; }

        public RemoveFactCommand(IResource parentTopic, IResource factToRemove)
        {
            ParentTopic = parentTopic ?? throw new ArgumentNullException(nameof(parentTopic));
            FactToRemove = factToRemove ?? throw new ArgumentNullException(nameof(factToRemove));
        }
    }
}
