using System;
using System.Collections.Generic;

using Kirschhock.HTIYC.Common.Abstractions;

using MongoDB.Bson.Serialization.Attributes;

namespace Kirschhock.HTIYC.Infrastructure.DbModels
{
    public class TopicDTO : IResource
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

        public ICollection<Guid> Facts { get; set; } = new List<Guid>();
    }
}
