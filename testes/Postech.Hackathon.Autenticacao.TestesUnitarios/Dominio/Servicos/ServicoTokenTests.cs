using Microsoft.Extensions.Configuration;
using Moq;
using Postech.Hackathon.Autenticacao.Dominio.Servicos;

namespace Postech.Hackathon.Autenticacao.TestesUnitarios.Dominio.Servicos
{
    public class ServicoTokenTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly ServicoToken _servicoToken;

        public ServicoTokenTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(config => config["Jwt:Key"]).Returns("supersecretkey12345678901234567890");
            _configurationMock.Setup(config => config["Jwt:Issuer"]).Returns("testIssuer");
            _configurationMock.Setup(config => config["Jwt:Audience"]).Returns("testAudience");

            _servicoToken = new ServicoToken(_configurationMock.Object);
        }

        [Fact]
        public void GerarToken_DeveRetornarTokenValido()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userName = "testUser";
            var escopos = new List<string> { "escopo1", "escopo2" };
            var papeis = new List<string> { "papel1", "papel2" };

            // Act
            var token = _servicoToken.GerarToken(userId, userName, escopos, papeis);

            // Assert
            Assert.False(string.IsNullOrEmpty(token));
        }

        [Fact]
        public void ValidarToken_DeveRetornarTrueParaTokenValido()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userName = "testUser";
            var escopos = new List<string> { "escopo1", "escopo2" };
            var papeis = new List<string> { "papel1", "papel2" };
            var token = _servicoToken.GerarToken(userId, userName, escopos, papeis);

            // Act
            var isValid = _servicoToken.ValidarToken(token);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void ValidarToken_DeveRetornarFalseParaTokenInvalido()
        {
            // Arrange
            var invalidToken = "invalidToken";

            // Act
            var isValid = _servicoToken.ValidarToken(invalidToken);

            // Assert
            Assert.False(isValid);
        }
    }
}