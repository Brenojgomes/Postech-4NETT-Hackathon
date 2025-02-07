using System.Diagnostics.CodeAnalysis;

namespace Postech.Hackathon.Autenticacao.Dominio.Excecoes
{
    /// <summary>
    /// Representa erros de conflito na persistencia de dados.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ExcecaoDeConflito(string mensagem) : Exception(mensagem)
    {

    }
}