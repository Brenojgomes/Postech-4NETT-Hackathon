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
        /// Obtém um usuário pelo email ou documento e tipo de perfil.
        /// </summary>
        /// <param name="email">O email do usuário.</param>
        /// <param name="documento">O documento do usuário.</param>
        /// <param name="tipoPerfil">O tipo de perfil do usuário.</param>
        /// <returns>O usuário encontrado ou null.</returns>
        Usuario ObterUsuarioPorEmailOuDocumento(string email, string documento, TipoPerfilEnumerador tipoPerfil);
    }
}
