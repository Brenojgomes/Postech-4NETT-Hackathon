using MongoDB.Driver;
using Postech.Hackathon.Autenticacao.Dominio.Entidades;
using Postech.Hackathon.Autenticacao.Dominio.Enumeradores;
using Postech.Hackathon.Autenticacao.Dominio.Interfaces.Repositorios;

namespace Postech.Hackathon.Autenticacao.Infra.Repositorios
{
    /// <summary>
    /// Repositório para gerenciar usuários no MongoDB.
    /// </summary>
    public class UsuarioRepositorio(IMongoClient mongoClient) : IUsuarioRepositorio
    {
        /// <summary>
        /// Banco de dados do MongoDB.
        /// </summary>
        private readonly IMongoDatabase _bancoDeDados = mongoClient.GetDatabase("autenticacao");

        /// <summary>
        /// Cadastra um novo usuário.
        /// </summary>
        /// <param name="usuario">O usuário a ser cadastrado.</param>
        /// <returns>O usuário cadastrado.</returns>
        public Usuario CadastrarUsuario(Usuario usuario)
        {
            var colecaoUsuarios = ObterColecaoUsuarios();
            colecaoUsuarios.InsertOne(usuario);
            return usuario;
        }

        /// <summary>
        /// Obtém um usuário pelo documento, senha e tipo de perfil.
        /// </summary>
        /// <param name="documento">O documento do usuário.</param>
        /// <param name="senha">A senha do usuário.</param>
        /// <param name="tipoPerfil">O tipo de perfil do usuário.</param>
        /// <returns>O usuário encontrado ou null.</returns>
        public Usuario ObterUsuarioPeloDocumento(string documento, string senha, TipoPerfilEnumerador tipoPerfil)
        {
            var colecaoUsuarios = ObterColecaoUsuarios();
            var filtro = Builders<Usuario>.Filter.And(
                Builders<Usuario>.Filter.Eq(u => u.Documento, documento),
                Builders<Usuario>.Filter.Eq(u => u.Senha, senha),
                Builders<Usuario>.Filter.Eq(u => u.TipoPerfil, tipoPerfil));

            return colecaoUsuarios.Find(filtro).FirstOrDefault();
        }

        /// <summary>
        /// Obtém um usuário pelo email, senha e tipo de perfil.
        /// </summary>
        /// <param name="email">O email do usuário.</param>
        /// <param name="senha">A senha do usuário.</param>
        /// <param name="tipoPerfil">O tipo de perfil do usuário.</param>
        /// <returns>O usuário encontrado ou null.</returns>
        public Usuario ObterUsuarioPeloEmail(string email, string senha, TipoPerfilEnumerador tipoPerfil)
        {
            var colecaoUsuarios = ObterColecaoUsuarios();
            var filtro = Builders<Usuario>.Filter.And(
                Builders<Usuario>.Filter.Eq(u => u.Email, email),
                Builders<Usuario>.Filter.Eq(u => u.Senha, senha),
                Builders<Usuario>.Filter.Eq(u => u.TipoPerfil, tipoPerfil));

            return colecaoUsuarios.Find(filtro).FirstOrDefault();
        }

        /// <summary>
        /// Obtém a coleção de usuários do banco de dados.
        /// </summary>
        /// <returns>A coleção de usuários.</returns>
        public IMongoCollection<Usuario> ObterColecaoUsuarios()
        {
            return _bancoDeDados.GetCollection<Usuario>("usuarios");
        }
    }
}
