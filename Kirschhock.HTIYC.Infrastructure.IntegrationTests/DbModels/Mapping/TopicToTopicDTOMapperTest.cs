using System.Collections.Generic;
using System.Threading.Tasks;

using Kirschhock.HTIYC.Domain;
using Kirschhock.HTIYC.Infrastructure.DbModels;
using Kirschhock.HTIYC.Infrastructure.DbModels.Mappings.Abstractions;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace Kirschhock.HTIYC.Infrastructure.IntegrationTests.DbModels.Mapping
{
    public class TopicToTopicDTOMapperTest : IClassFixture<TestFixture>
    {
        private readonly TestFixture fixture;

        public TopicToTopicDTOMapperTest(TestFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory, MemberData(nameof(InMemoryTopicTestData))]
        public async Task TestSimpleMapTopic(Topic topicToMap)
        {
            var mapper = fixture.ServiceProvider.GetRequiredService<IMapper<Topic, TopicDTO>>();
            var dto = await mapper.MapAsync(topicToMap);

            Assert.Equal(topicToMap.Id, dto.Id);
            Assert.Equal(topicToMap.Name, dto.Name);
            Assert.Equal(topicToMap.DisplayName, dto.DisplayName);
            Assert.Equal(topicToMap.Description, dto.Description);
        }

        public static IEnumerable<object[]> InMemoryTopicTestData =>
               new List<object[]>
               {
                   //new object[] { new Topic().Set(Guid.NewGuid(),"test-1", "Test 1", "Test 1", new List<Fact>()) },
                   //new object[] { new Topic().Set(Guid.NewGuid(),"test-2", "Test 2", "Test 2", new List<Fact>()) },
                   //new object[] { new Topic().Set(Guid.NewGuid(),"test-3", "Test 3", "Test 3", new List<Fact>()) },
                   //new object[] { new Topic().Set(Guid.NewGuid(),"test-4", "Test 4", "Test 4", new List<Fact>()) },
                   //new object[] { new Topic().Set(Guid.NewGuid(),"test-5", "Test 5", "Test 5", new List<Fact>()) }
               };
    }
}