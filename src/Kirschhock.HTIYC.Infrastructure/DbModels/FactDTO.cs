using System;

using Kirschhock.HTIYC.Common.Abstractions;

using MongoDB.Bson;

namespace Kirschhock.HTIYC.Infrastructure.DbModels
{
    public class FactDTO : IResource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ReadMoreLink { get; set; }
        public ObjectId TopicId { get; set; }
    }
}
