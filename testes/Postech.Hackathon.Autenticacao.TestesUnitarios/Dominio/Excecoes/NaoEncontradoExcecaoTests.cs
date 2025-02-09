using Postech.Hackathon.Autenticacao.Dominio.Excecoes;

namespace Postech.Hackathon.Autenticacao.TestesUnitarios.Dominio.Excecoes
{
    public class NaoEncontradoExcecaoTests
    {
        [Fact]
        public void LancarQuandoEntidadeNula_DeveLancarExcecaoQuandoEntidadeForNula()
        {
            // Arrange
            object? entidadeNula = null;
            var mensagemDeErro = "Entidade não encontrada";

            // Act & Assert
            var excecao = Assert.Throws<NaoEncontradoExcecao>(() => NaoEncontradoExcecao.LancarQuandoEntidadeNula(entidadeNula, mensagemDeErro));
            Assert.Equal(mensagemDeErro, excecao.Message);
        }

        [Fact]
        public void LancarQuandoEntidadeNula_NaoDeveLancarExcecaoQuandoEntidadeNaoForNula()
        {
            // Arrange
            var entidadeNaoNula = new object();
            var mensagemDeErro = "Entidade não encontrada";

            // Act & Assert
            NaoEncontradoExcecao.LancarQuandoEntidadeNula(entidadeNaoNula, mensagemDeErro);
        }
    }
}
