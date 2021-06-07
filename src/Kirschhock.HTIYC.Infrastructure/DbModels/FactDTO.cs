using System;

using Kirschhock.HTIYC.Common.Abstractions;

using MongoDB.Bson.Serialization.Attributes;

namespace Kirschhock.HTIYC.Infrastructure.DbModels
{
    public class FactDTO : IResource
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ReadMoreLink { get; set; }
        public Guid TopicId { get; set; }
    }
}
