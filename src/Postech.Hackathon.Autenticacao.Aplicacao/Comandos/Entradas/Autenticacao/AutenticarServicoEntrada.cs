using MediatR;
using Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Saidas;
using Postech.Hackathon.Autenticacao.Aplicacao.ViewModels.AutenticacaoServicos;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "O campo ClientId é obrigatório.")]
        public Guid ClientId { get; set; }

        /// <summary>
        /// Segredo do cliente utilizado para autenticação.
        /// </summary>
        [Required(ErrorMessage = "O campo ClientSecret é obrigatório.")]
        public string ClientSecret { get; set; }
    }
}
