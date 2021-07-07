
using Kirschhock.HTIYC.Common.Abstractions;
using Kirschhock.HTIYC.Domain;

namespace Kirschhock.HTIYC.Infrastructure.Topics.Queries
{
    public class TopicQuery : IQuery<Topic>
    {
        public const int DefaultPageSize = 25;
        public int Skip { get; }
        public int Take { get; }

        public TopicQuery(int skip = 0, int take = DefaultPageSize)
        {
            Skip = skip;
            Take = take;
        }

        public TopicQuery NextPage(int pageSize = DefaultPageSize)
        {
            return new TopicQuery(pageSize * Skip, DefaultPageSize);
        }

    }
}
