using System.Diagnostics.CodeAnalysis;

namespace Postech.Hackathon.Autenticacao.Dominio.Excecoes
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Exceção lançada quando uma entidade não é encontrada.
    /// </summary>
    public class NaoEncontradoExcecao(string mensagem) : Exception(mensagem)
    {
        /// <summary>
        /// Lança uma NaoEncontradoExcecao se a entidade fornecida for nula.
        /// </summary>
        /// <param name="entidade">A entidade a ser verificada.</param>
        /// <param name="mensagemDeErro">A mensagem de erro a ser incluída na exceção.</param>
        public static void LancarQuandoEntidadeNula(object? entidade, string mensagemDeErro)
        {
            if (entidade is not null) return;
            throw new NaoEncontradoExcecao(mensagemDeErro);
        }
    }
}
