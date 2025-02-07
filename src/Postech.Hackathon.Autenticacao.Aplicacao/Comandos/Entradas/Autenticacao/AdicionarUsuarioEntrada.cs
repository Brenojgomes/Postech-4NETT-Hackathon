using MediatR;
using Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Saidas;
using Postech.Hackathon.Autenticacao.Aplicacao.ViewModels.Autenticacao;
using Postech.Hackathon.Autenticacao.Dominio.Enumeradores;
using System.ComponentModel.DataAnnotations;

namespace Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Entradas.Autenticacao
{
    /// <summary>
    /// Classe que representa a entrada para adicionar um usuário.
    /// </summary>
    public class AdicionarUsuarioEntrada : IRequest<SaidaPadrao<UsuarioViewModel>>
    {
        /// <summary>
        /// Nome do usuário.
        /// </summary>
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; }

        /// <summary>
        /// Email do usuário.
        /// </summary>
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        public string Email { get; set; }

        /// <summary>
        /// Documento do usuário.
        /// </summary>
        [Required(ErrorMessage = "O campo Documento é obrigatório.")]
        public string Documento { get; set; }

        /// <summary>
        /// Senha do usuário.
        /// </summary>
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Senha { get; set; }

        /// <summary>
        /// Tipo de perfil do usuário.
        /// </summary>
        [Required(ErrorMessage = "O campo TipoPerfil é obrigatório.")]
        public TipoPerfilEnumerador TipoPerfil { get; set; }
    }
}
