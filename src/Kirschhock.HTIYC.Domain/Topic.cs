
using System;
using System.Collections.Generic;
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

        public int Id { get; set; }
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

        public Topic(string displayName)
        {
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

        /// <summary>
        /// Creates a new <see cref="Topic"/> And raises the correlated event(s)
        /// </summary>
        public static async Task<Topic> NewTopic(string displayName)
        {
            var topic = new Topic(displayName);
            await DomainEventManager.RaiseEventAsync(new AddTopicCommand(topic));
            return topic;
        }
    }
}
