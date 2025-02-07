using MediatR;
using Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Saidas;
using Postech.Hackathon.Autenticacao.Aplicacao.ViewModels.AutenticacaoServicos;

namespace Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Entradas.Autenticacao
{
    /// <summary>
    /// Classe que representa a entrada para autenticação de serviços.
    /// </summary>
    public class AutenticarServicoEntrada : IRequest<SaidaPadrao<ServicoViewModel>>
    {
        /// <summary>
        /// Identificador único do cliente.
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        /// Segredo do cliente utilizado para autenticação.
        /// </summary>
        public string ClientSecret { get; set; }
    }
}
