using Postech.Hackathon.Autenticacao.Dominio.Excecoes;

namespace Postech.Hackathon.Autenticacao.TestesUnitarios.Dominio.Excecoes
{
    public class ExcecaoDeDominioTests
    {
        [Fact]
        public void LancarQuando_DeveLancarExcecaoQuandoRegraInvalida()
        {
            // Arrange
            var regraInvalida = true;
            var mensagem = "Regra inválida";

            // Act & Assert
            var excecao = Assert.Throws<ExcecaoDeDominio>(() => ExcecaoDeDominio.LancarQuando(regraInvalida, mensagem));
            Assert.Equal(mensagem, excecao.Message);
        }

        [Fact]
        public void LancarQuando_NaoDeveLancarExcecaoQuandoRegraValida()
        {
            // Arrange
            var regraInvalida = false;
            var mensagem = "Regra inválida";

            // Act & Assert
            ExcecaoDeDominio.LancarQuando(regraInvalida, mensagem);
        }
    }
}
