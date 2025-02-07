using Moq;
using Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Entradas.Autenticacao;
using Postech.Hackathon.Autenticacao.Aplicacao.Manipuladores.Autenticacao;
using Postech.Hackathon.Autenticacao.Dominio.Servicos.Interfaces;

namespace Postech.Hackathon.Autenticacao.TestesUnitarios.Aplicacao.Manipuladores
{
    public class ValidarTokenHandlerTests
    {
        private readonly Mock<IServicoToken> _servicoTokenMock;
        private readonly ValidarTokenHandler _handler;

        public ValidarTokenHandlerTests()
        {
            _servicoTokenMock = new Mock<IServicoToken>();
            _handler = new ValidarTokenHandler(_servicoTokenMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucessoQuandoTokenValido()
        {
            // Arrange
            var comando = new ValidarTokenEntrada
            {
                Token = "valid-token"
            };

            _servicoTokenMock.Setup(s => s.ValidarToken(comando.Token)).Returns(true);

            // Act
            var resultado = await _handler.Handle(comando, CancellationToken.None);

            // Assert
            Assert.True(resultado.Sucesso);
            Assert.Equal("O token foi validado com sucesso.", resultado.Mensagem);
        }

        [Fact]
        public async Task Handle_DeveRetornarFalhaQuandoTokenInvalido()
        {
            // Arrange
            var comando = new ValidarTokenEntrada
            {
                Token = "invalid-token"
            };

            _servicoTokenMock.Setup(s => s.ValidarToken(comando.Token)).Returns(false);

            // Act
            var resultado = await _handler.Handle(comando, CancellationToken.None);

            // Assert
            Assert.False(resultado.Sucesso);
            Assert.Equal(" O token está inválido", resultado.Mensagem);
        }
    }
}
