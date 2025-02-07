using Postech.Hackathon.Autenticacao.Dominio.Excecoes;

namespace Postech.Hackathon.Autenticacao.TestesUnitarios.Dominio.Excecoes
{
    public class ExcecaoDeConflitoTests
    {
        [Fact]
        public void Construtor_DeveInicializarComMensagemCorreta()
        {
            // Arrange
            var mensagemEsperada = "Mensagem de conflito";

            // Act
            var excecao = new ExcecaoDeConflito(mensagemEsperada);

            // Assert
            Assert.Equal(mensagemEsperada, excecao.Message);
        }

        [Fact]
        public void LancarQuandoVerdadeiro_DeveLancarExcecaoQuandoCondicaoVerdadeira()
        {
            // Arrange
            var mensagem = "Conflito detectado";
            var condicao = true;

            // Act & Assert
            var excecao = Assert.Throws<ExcecaoDeConflito>(() => ExcecaoDeConflito.LancarQuandoVerdadeiro(condicao, mensagem));
            Assert.Equal(mensagem, excecao.Message);
        }

        [Fact]
        public void LancarQuandoVerdadeiro_NaoDeveLancarExcecaoQuandoCondicaoFalsa()
        {
            // Arrange
            var mensagem = "Conflito detectado";
            var condicao = false;

            // Act & Assert
            ExcecaoDeConflito.LancarQuandoVerdadeiro(condicao, mensagem);
        }
    }
}
