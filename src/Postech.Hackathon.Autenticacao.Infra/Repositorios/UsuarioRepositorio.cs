using MongoDB.Driver;
using Postech.Hackathon.Autenticacao.Dominio.Entidades;
using Postech.Hackathon.Autenticacao.Dominio.Enumeradores;
using Postech.Hackathon.Autenticacao.Dominio.Excecoes;
using Postech.Hackathon.Autenticacao.Dominio.Interfaces.Repositorios;

namespace Postech.Hackathon.Autenticacao.Infra.Repositorios
{
    /// <summary>
    /// Repositório para gerenciar usuários no MongoDB.
    /// </summary>
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        /// <summary>
        /// Banco de dados do MongoDB.
        /// </summary>
        private readonly IMongoDatabase _bancoDeDados;

        /// <summary>
        /// Construtor do repositório de usuários.
        /// </summary>
        /// <param name="mongoClient">Cliente do MongoDB.</param>
        public UsuarioRepositorio(IMongoClient mongoClient)
        {
            _bancoDeDados = mongoClient.GetDatabase("autenticacao");
        }

        /// <summary>
        /// Cadastra um novo usuário.
        /// </summary>
        /// <param name="usuario">O usuário a ser cadastrado.</param>
        /// <returns>O usuário cadastrado.</returns>
        public Usuario CadastrarUsuario(Usuario usuario)
        {
            var colecaoUsuarios = ObterColecaoUsuarios();
            try
            {
                colecaoUsuarios.InsertOne(usuario);
                return usuario;
            }
            catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                if (ex.WriteError.Message.Contains("Documento"))
                {
                    throw new ExcecaoDeConflito("Um usuário já está cadastrado para o documento informado.");
                }
                else if (ex.WriteError.Message.Contains("Email"))
                {
                    throw new ExcecaoDeConflito("Um usuário já está cadastrado para o email informado.");
                }
                throw;
            }
        }

        /// <summary>
        /// Obtém um usuário pelo email ou documento e tipo de perfil.
        /// </summary>
        /// <param name="email">O email do usuário.</param>
        /// <param name="documento">O documento do usuário.</param>
        /// <param name="tipoPerfil">O tipo de perfil do usuário.</param>
        /// <returns>O usuário encontrado ou null.</returns>
        public Usuario ObterUsuarioPorEmailOuDocumento(string email, string documento, TipoPerfilEnumerador tipoPerfil)
        {
            var colecaoUsuarios = ObterColecaoUsuarios();
            var filtro = Builders<Usuario>.Filter.And(
                Builders<Usuario>.Filter.Eq(u => u.TipoPerfil, tipoPerfil),
                Builders<Usuario>.Filter.Or(
                    Builders<Usuario>.Filter.Eq(u => u.Email, email),
                    Builders<Usuario>.Filter.Eq(u => u.Documento, documento)
                ));

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
