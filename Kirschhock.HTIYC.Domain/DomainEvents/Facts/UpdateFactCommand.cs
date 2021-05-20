using System;

using Kirschhock.HTIYC.Common.Abstractions;

namespace Kirschhock.HTIYC.Domain.DomainEvents.Facts
{
    public class UpdateFactCommand : ICommand
    {
        public IResource ParentTopic { get; set; }
        public Fact FactToUpdate { get; set; }

        public UpdateFactCommand(IResource parentTopic, Fact factToUpdate)
        {
            ParentTopic = parentTopic ?? throw new ArgumentNullException(nameof(parentTopic));
            FactToUpdate = factToUpdate ?? throw new ArgumentNullException(nameof(factToUpdate));
        }
    }
}
