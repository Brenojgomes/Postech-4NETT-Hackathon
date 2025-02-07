using Moq;
using Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Entradas.Autenticacao;
using Postech.Hackathon.Autenticacao.Aplicacao.Manipuladores.Autenticacao;
using Postech.Hackathon.Autenticacao.Dominio.Entidades;
using Postech.Hackathon.Autenticacao.Dominio.Excecoes;
using Postech.Hackathon.Autenticacao.Dominio.Interfaces.Repositorios;
using Postech.Hackathon.Autenticacao.Dominio.Servicos.Interfaces;

namespace Postech.Hackathon.Autenticacao.TestesUnitarios.Aplicacao.Manipuladores
{
    public class AutenticarServicoHandlerTests
    {
        private readonly Mock<IServicoRepositorio> _repositorioMock;
        private readonly Mock<IServicoToken> _servicoTokenMock;
        private readonly AutenticarServicoHandler _handler;

        public AutenticarServicoHandlerTests()
        {
            _repositorioMock = new Mock<IServicoRepositorio>();
            _servicoTokenMock = new Mock<IServicoToken>();
            _handler = new AutenticarServicoHandler(_repositorioMock.Object, _servicoTokenMock.Object);
        }

        [Fact]
        public async Task Handle_DeveAutenticarServicoERetornarSaidaPadrao()
        {
            // Arrange
            var comando = new AutenticarServicoEntrada
            {
                ClientId = Guid.NewGuid(),
                ClientSecret = "client-secret"
            };

            var servico = new Servico
            {
                Id = Guid.NewGuid(),
                Nome = "Servico Teste",
                Escopos = new List<string> { "escopo1", "escopo2" },
                Papeis = new List<string> { "papel1", "papel2" }
            };

            _repositorioMock.Setup(r => r.ObterServico(comando.ClientId, comando.ClientSecret)).Returns(servico);
            _servicoTokenMock.Setup(s => s.GerarToken(servico.Id, servico.Nome, servico.Escopos, servico.Papeis)).Returns("fake-jwt-token");

            // Act
            var resultado = await _handler.Handle(comando, CancellationToken.None);

            // Assert
            Assert.True(resultado.Sucesso);
            Assert.Equal(" O serviço foi autenticado com sucesso.", resultado.Mensagem);
            Assert.NotNull(resultado.Dados);
            Assert.Equal("fake-jwt-token", resultado.Dados.Token);
        }

        [Fact]
        public async Task Handle_DeveLancarExcecaoQuandoServicoNaoEncontrado()
        {
            // Arrange
            var comando = new AutenticarServicoEntrada
            {
                ClientId = Guid.NewGuid(),
                ClientSecret = "client-secret"
            };

            _repositorioMock.Setup(r => r.ObterServico(comando.ClientId, comando.ClientSecret)).Returns((Servico)null);

            // Act & Assert
            var excecao = await Assert.ThrowsAsync<NaoEncontradoExcecao>(() => _handler.Handle(comando, CancellationToken.None));
            Assert.Equal("O serviço não foi localizado no sistema.", excecao.Message);
        }
    }
}
