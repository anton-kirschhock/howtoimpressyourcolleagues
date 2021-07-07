
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Common.Abstractions;
using Kirschhock.HTIYC.Domain.DomainEvents;
using Kirschhock.HTIYC.Domain.DomainEvents.Facts;
using Kirschhock.HTIYC.Domain.DomainEvents.Topics;

namespace Kirschhock.HTIYC.Domain
{
    /// <summary>
    /// Describes a Topic of multiple facts
    /// </summary>
    public class Topic : INamedResource
    {
        private string displayName;
        private string description;
        internal IDomainEventManager DomainEventManager { get; private set; }

        public Guid Id { get; set; }
        public string Name { get; protected set; }

        public string DisplayName
        {
            get => displayName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));

                displayName = value;
                NotifyPropertyChanged();
            }
        }
        public string Description
        {
            get => description;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));

                description = value;
                NotifyPropertyChanged();
            }
        }

        public ICollection<Fact> Facts { get; set; } = new List<Fact>();

        /// <summary>
        /// Used for Mapping Only
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Topic(IDomainEventManager eventManager)
        {
            this.DomainEventManager = eventManager;
        }

        public Topic(string displayName)
        {
            Id = Guid.NewGuid();
            Name = displayName.Replace(" ", "-").ToLower().Normalize();
            DisplayName = displayName;
        }

        public async Task<Fact> AddFactAsync(string name, string title)
        {
            var fact = new Fact(this, name, title);
            Facts.Add(fact);
            await DomainEventManager.RaiseEventAsync(new AddFactCommand(this, fact));
            return fact;
        }


        public async Task<Fact> RemoveFactByNameAsync(string name)
        {
            var fact = Facts.FirstOrDefault(e => e.Name.ToLower() == name.ToLower());
            if (fact != null)
            {
                Facts.Remove(fact);
                await DomainEventManager.RaiseEventAsync(new RemoveFactCommand(this, fact));
            }
            return fact;
        }

        protected async void NotifyPropertyChanged()
        {
            await DomainEventManager.RaiseEventAsync(new UpdateTopicCommand(this));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Topic Set(Guid id, string name, string displayName, string description, List<Fact> facts)
        {
            Id = id;
            Name = name;
            this.displayName = displayName;
            this.description = description;
            facts?.ForEach(fact => fact.SetAggregateRoot(this, DomainEventManager));
            Facts = facts;

            return this;
        }

    }
}
