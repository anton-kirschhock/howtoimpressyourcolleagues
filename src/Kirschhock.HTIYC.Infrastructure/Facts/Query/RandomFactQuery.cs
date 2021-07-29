
using Kirschhock.HTIYC.Common.Abstractions;
using Kirschhock.HTIYC.Domain;

namespace Kirschhock.HTIYC.Infrastructure.Facts.Query
{
    public class RandomFactQuery : IQuery<Fact>
    {
        public string Topic { get; protected set; }

        public RandomFactQuery(string topic)
        {
            Topic = topic;
        }
    }
}
