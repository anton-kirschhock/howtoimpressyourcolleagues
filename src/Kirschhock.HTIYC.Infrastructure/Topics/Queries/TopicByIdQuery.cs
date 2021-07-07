
using System;

using Kirschhock.HTIYC.Common.Abstractions;
using Kirschhock.HTIYC.Domain;

namespace Kirschhock.HTIYC.Infrastructure.Topics.Queries
{
    public class TopicByIdQuery : IQueryById<Topic>
    {
        public Guid Id { get; }

        public TopicByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
