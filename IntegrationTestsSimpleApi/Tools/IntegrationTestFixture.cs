using Xunit;

namespace IntegrationTestsSimpleApi.Tools
{
    [Collection(Name)]
    public class IntegrationTestFixture : IClassFixture<DatabaseFixture>
    {
        private const string Name = nameof(IntegrationTestFixture);
        private readonly DatabaseFixture _fixture;

        public IntegrationTestFixture(DatabaseFixture fixture) => _fixture = fixture;
    }
}
