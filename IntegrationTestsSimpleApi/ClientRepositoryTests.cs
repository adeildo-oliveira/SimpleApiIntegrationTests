using FluentAssertions;
using IntegrationTestsSimpleApi.Builders;
using IntegrationTestsSimpleApi.Tools;
using SimpleApiIntegrationTests.Repository;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTestsSimpleApi
{
    public class ClientRepositoryTests : IntegrationTestFixture
    {
        private readonly IClientRepository _clientRepository;
        private readonly DatabaseFixture _fixture;

        public ClientRepositoryTests(DatabaseFixture fixture) : base(fixture)
        {
            _fixture = fixture;
            _clientRepository = _fixture.GetService<IClientRepository>();
        }

        [Fact]
        public async Task DeveInserirCliente()
        {
            _fixture.ClearDataBase();
            var clienteBuilder = new ClienteBuilder().Instanciar();
            await _clientRepository.AddAsync(clienteBuilder);

            var result = await _clientRepository.GetByIdAsync(1);

            result.Id.Should().Be(1);
            result.Nome.Should().Be("Nome Cliente");
            result.SobreNome.Should().Be("Sobre Nome Cliente");
        }

        [Fact]
        public async Task DeveObterCliente()
        {
            _fixture.ClearDataBase();
            await _clientRepository.AddAsync(new ClienteBuilder().Instanciar());
            await _clientRepository.AddAsync(new ClienteBuilder().ComNome("Nome Cliente 2").Instanciar());

            var result = await _clientRepository.GetByIdAsync(2);

            result.Id.Should().Be(2);
            result.Nome.Should().Be("Nome Cliente 2");
            result.SobreNome.Should().Be("Sobre Nome Cliente");
        }

        [Fact]
        public async Task DeveObterTodosOsClientes()
        {
            _fixture.ClearDataBase();
            await _clientRepository.AddAsync(new ClienteBuilder().Instanciar());
            await _clientRepository.AddAsync(new ClienteBuilder().ComNome("Nome Cliente 2").Instanciar());

            var results = await _clientRepository.GetAllAsync();

            var resultsClients = results.OrderBy(x => x.Id).ToArray();
            results.Should().HaveCount(2);
            resultsClients[0].Id.Should().Be(1);
            resultsClients[0].Nome.Should().Be("Nome Cliente");
            resultsClients[0].SobreNome.Should().Be("Sobre Nome Cliente");
            resultsClients[1].Id.Should().Be(2);
            resultsClients[1].Nome.Should().Be("Nome Cliente 2");
            resultsClients[1].SobreNome.Should().Be("Sobre Nome Cliente");
        }
    }
}
