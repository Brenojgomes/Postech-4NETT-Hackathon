using MediatR;
using Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Saidas;

namespace Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Entradas.Autenticacao
{
    /// <summary>
    /// Classe que representa a entrada para validação de um token.
    /// </summary>
    public class ValidarTokenEntrada : IRequest<SaidaPadrao>
    {
        /// <summary>
        /// Token a ser validado.
        /// </summary>
        public string Token { get; set; }
    }
}
