
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Common.Abstractions;

namespace Kirschhock.HTIYC.Domain
{
    /// <summary>
    /// Describes a Topic of multiple facts
    /// </summary>
    public class Topic : INamedResource
    {
        private string displayName;
        private string description;

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

        public ICollection<Fact> Facts { get; set; } = new List<Fact>();


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
            return fact;
        }


        public async Task<Fact> RemoveFactByNameAsync(string name)
        {
            var fact = Facts.FirstOrDefault(e => e.Name.ToLower() == name.ToLower());
            if (fact != null)
            {
                Facts.Remove(fact);
            }
            return fact;
        }
    }
}
