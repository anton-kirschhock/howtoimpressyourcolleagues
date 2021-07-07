using System;
using System.ComponentModel;

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

        internal IDomainEventManager DomainEventManager { get; private set; }

        public Guid Id { get; set; }
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

        public Topic Topic { get; protected set; }

        /// <summary>
        /// FOR MAPPING ONLY
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Fact()
        {
        }
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
            Id = Guid.NewGuid();
            Topic = topic ?? throw new ArgumentNullException(nameof(topic));
            DomainEventManager = topic.DomainEventManager;
            Name = name;
            this.title = title;
        }

        protected async void NotifyPropertyChanged()
        {
            await DomainEventManager.RaiseEventAsync(new UpdateFactCommand(Topic, this));
        }

        /// <summary>
        /// ONLY USE DURING MAPPING!
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="readMoreLink"></param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Fact Set(Guid id,
                        string name,
                        string title,
                        string description,
                        string readMoreLink)
        {
            Id = id;
            Name = name;
            this.title = title;
            this.description = description;
            this.readMoreLink = readMoreLink;

            return this;
        }


        /// <summary>
        /// ONLY USE DURING MAPPING!
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="domainEventManager"></param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Fact SetAggregateRoot(Topic topic, IDomainEventManager domainEventManager)
        {
            Topic = topic;
            DomainEventManager = domainEventManager;

            return this;
        }
    }
}
