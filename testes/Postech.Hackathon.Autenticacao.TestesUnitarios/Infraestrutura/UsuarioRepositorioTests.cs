using MongoDB.Driver;
using Moq;
using Postech.Hackathon.Autenticacao.Dominio.Entidades;
using Postech.Hackathon.Autenticacao.Dominio.Enumeradores;
using Postech.Hackathon.Autenticacao.Infra.Repositorios;

namespace Postech.Hackathon.Autenticacao.TestesUnitarios.Infraestrutura
{
    public class UsuarioRepositorioTests
    {
        private readonly Mock<IMongoClient> _mongoClientMock;
        private readonly Mock<IMongoDatabase> _mongoDatabaseMock;
        private readonly Mock<IMongoCollection<Usuario>> _mongoCollectionMock;
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public UsuarioRepositorioTests()
        {
            _mongoClientMock = new Mock<IMongoClient>();
            _mongoDatabaseMock = new Mock<IMongoDatabase>();
            _mongoCollectionMock = new Mock<IMongoCollection<Usuario>>();

            _mongoClientMock.Setup(client => client.GetDatabase(It.IsAny<string>(), null))
                .Returns(_mongoDatabaseMock.Object);

            _mongoDatabaseMock.Setup(db => db.GetCollection<Usuario>(It.IsAny<string>(), null))
                .Returns(_mongoCollectionMock.Object);

            _usuarioRepositorio = new UsuarioRepositorio(_mongoClientMock.Object);
        }

        [Fact]
        public void CadastrarUsuario_DeveInserirUsuarioNaColecao()
        {
            // Arrange
            var usuario = new Usuario("Breno", "teste@teste.com", "123456789", "123456", TipoPerfilEnumerador.Medico);

            // Act
            _usuarioRepositorio.CadastrarUsuario(usuario);

            // Assert
            _mongoCollectionMock.Verify(collection => collection.InsertOne(usuario, null, default), Times.Once);
        }

        [Fact]
        public void ObterUsuarioPorEmailOuDocumento_DeveRetornarUsuarioCorreto()
        {
            // Arrange
            var email = "teste@teste.com";
            var documento = "123456789";
            var tipoPerfil = TipoPerfilEnumerador.Medico;
            var usuarioEsperado = new Usuario("Breno", email, documento, "123456", tipoPerfil);

            var filtro = Builders<Usuario>.Filter.And(
                Builders<Usuario>.Filter.Eq(u => u.TipoPerfil, tipoPerfil),
                Builders<Usuario>.Filter.Or(
                    Builders<Usuario>.Filter.Eq(u => u.Email, email),
                    Builders<Usuario>.Filter.Eq(u => u.Documento, documento)
                ));

            var asyncCursorMock = new Mock<IAsyncCursor<Usuario>>();
            asyncCursorMock.Setup(cursor => cursor.Current).Returns(new List<Usuario> { usuarioEsperado });
            asyncCursorMock.SetupSequence(cursor => cursor.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            _mongoCollectionMock.Setup(collection => collection.FindSync(It.IsAny<FilterDefinition<Usuario>>(), It.IsAny<FindOptions<Usuario, Usuario>>(), default))
                .Returns(asyncCursorMock.Object);

            // Act
            var usuarioObtido = _usuarioRepositorio.ObterUsuarioPorEmailOuDocumento(email, documento, tipoPerfil);

            // Assert
            Assert.Equal(usuarioEsperado, usuarioObtido);
        }
    }
}
