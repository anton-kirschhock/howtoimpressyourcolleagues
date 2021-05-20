using System;

using Kirschhock.HTIYC.Common.Abstractions;

namespace Kirschhock.HTIYC.Domain.DomainEvents.Facts
{
    public class AddFactCommand : ICommand
    {
        public IResource ParentTopic { get; set; }
        public Fact FactToAdd { get; set; }

        public AddFactCommand(IResource parentTopic, Fact factToAdd)
        {
            ParentTopic = parentTopic ?? throw new ArgumentNullException(nameof(parentTopic));
            FactToAdd = factToAdd ?? throw new ArgumentNullException(nameof(factToAdd));
        }
    }
}
