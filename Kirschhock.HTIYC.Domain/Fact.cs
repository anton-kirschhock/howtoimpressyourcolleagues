using System;

using Kirschhock.HTIYC.Common.Abstractions;
using Kirschhock.HTIYC.Domain.DomainEvents;
using Kirschhock.HTIYC.Domain.DomainEvents.Facts;

namespace Kirschhock.HTIYC.Domain
{
    public class Fact : INamedResource
    {
        private string title;
        private string description;
        private string readMoreLink;

        public int Id { get; set; }

        public string Name { get; protected set; }

        public string Title
        {
            get => title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));

                title = value;
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

        public string ReadMoreLink
        {
            get => readMoreLink;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));

                readMoreLink = value;
                NotifyPropertyChanged();
            }
        }

        public Topic Topic { get; set; }

        /// <summary>
        /// Creates a new fact for a <see cref="Topic"/>
        /// </summary>
        /// <param name="topic">The parent <see cref="Topic"/></param>
        /// <param name="name">A short name you'd like to give to this fact</param>
        /// <param name="title">A longer title to give more context</param>
        public Fact(Topic topic, string name, string title)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException($"'{nameof(title)}' cannot be null or whitespace.", nameof(title));

            Topic = topic ?? throw new ArgumentNullException(nameof(topic));
            Name = name;
            this.title = title;
        }

        protected async void NotifyPropertyChanged()
        {
            await DomainEventManager.RaiseEvent(new UpdateFactCommand(Topic, this));
        }
    }
}
