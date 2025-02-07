using MongoDB.Driver;
using Moq;
using Postech.Hackathon.Autenticacao.Dominio.Entidades;
using Postech.Hackathon.Autenticacao.Infra.Repositorios;

namespace Postech.Hackathon.Autenticacao.TestesUnitarios.Infraestrutura
{
    public class ServicoRepositorioTests
    {
        private readonly Mock<IMongoClient> _mongoClientMock;
        private readonly Mock<IMongoDatabase> _mongoDatabaseMock;
        private readonly Mock<IMongoCollection<Servico>> _mongoCollectionMock;
        private readonly ServicoRepositorio _servicoRepositorio;

        public ServicoRepositorioTests()
        {
            _mongoClientMock = new Mock<IMongoClient>();
            _mongoDatabaseMock = new Mock<IMongoDatabase>();
            _mongoCollectionMock = new Mock<IMongoCollection<Servico>>();

            _mongoClientMock.Setup(client => client.GetDatabase(It.IsAny<string>(), null))
                .Returns(_mongoDatabaseMock.Object);

            _mongoDatabaseMock.Setup(db => db.GetCollection<Servico>(It.IsAny<string>(), null))
                .Returns(_mongoCollectionMock.Object);

            _servicoRepositorio = new ServicoRepositorio(_mongoClientMock.Object);
        }

        [Fact]
        public void ObterServico_DeveRetornarServicoCorreto()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            var clientSecret = "secret";
            var servicoEsperado = new Servico
            {
                ClientId = clientId.ToString(),
                ClientSecret = clientSecret
            };

            var filtro = Builders<Servico>.Filter.And(
                Builders<Servico>.Filter.Eq(u => u.ClientId, clientId.ToString()),
                Builders<Servico>.Filter.Eq(u => u.ClientSecret, clientSecret));

            var asyncCursorMock = new Mock<IAsyncCursor<Servico>>();
            asyncCursorMock.Setup(cursor => cursor.Current).Returns(new List<Servico> { servicoEsperado });
            asyncCursorMock.SetupSequence(cursor => cursor.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            _mongoCollectionMock.Setup(collection => collection.FindSync(It.IsAny<FilterDefinition<Servico>>(), It.IsAny<FindOptions<Servico, Servico>>(), default))
                .Returns(asyncCursorMock.Object);

            // Act
            var servicoObtido = _servicoRepositorio.ObterServico(clientId, clientSecret);

            // Assert
            Assert.Equal(servicoEsperado, servicoObtido);
        }

        [Fact]
        public void ObterColecaoServicos_DeveRetornarColecaoCorreta()
        {
            // Act
            var colecaoObtida = _servicoRepositorio.ObterColecaoServicos();

            // Assert
            Assert.Equal(_mongoCollectionMock.Object, colecaoObtida);
        }
    }
}
