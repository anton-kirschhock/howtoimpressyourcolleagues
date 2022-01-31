using System;
using System.ComponentModel;

using Kirschhock.HTIYC.Common.Abstractions;

namespace Kirschhock.HTIYC.Domain
{
    public class Fact : INamedResource
    {
        private string title;
        private string description;
        private string readMoreLink;


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
            Name = name;
            this.title = title;
        }
    }
}
