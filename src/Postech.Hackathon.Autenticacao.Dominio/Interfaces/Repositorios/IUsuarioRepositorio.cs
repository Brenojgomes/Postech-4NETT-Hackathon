using Postech.Hackathon.Autenticacao.Dominio.Entidades;
using Postech.Hackathon.Autenticacao.Dominio.Enumeradores;

namespace Postech.Hackathon.Autenticacao.Dominio.Interfaces.Repositorios
{
    /// <summary>
    /// Interface para operações relacionadas ao repositório de usuários.
    /// </summary>
    public interface IUsuarioRepositorio
    {
        /// <summary>
        /// Cadastra um novo usuário.
        /// </summary>
        /// <param name="usuario">O usuário a ser cadastrado.</param>
        /// <returns>O usuário cadastrado.</returns>
        Usuario CadastrarUsuario(Usuario usuario);

        /// <summary>
        /// Obtém um usuário pelo documento.
        /// </summary>
        /// <param name="documento">O documento do usuário.</param>
        /// <param name="senha">A senha do usuário.</param>
        /// <param name="tipoPerfil">O tipo de perfil do usuário.</param>
        /// <returns>O usuário correspondente ao documento.</returns>
        Usuario ObterUsuarioPeloDocumento(string documento, TipoPerfilEnumerador tipoPerfil);

        /// <summary>
        /// Obtém um usuário pelo email.
        /// </summary>
        /// <param name="email">O email do usuário.</param>
        /// <param name="senha">A senha do usuário.</param>
        /// <param name="tipoPerfil">O tipo de perfil do usuário.</param>
        /// <returns>O usuário correspondente ao email.</returns>
        Usuario ObterUsuarioPeloEmail(string email, TipoPerfilEnumerador tipoPerfil);
    }
}
