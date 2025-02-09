using MongoDB.Driver;
using Postech.Hackathon.Autenticacao.Dominio.Entidades;
using Postech.Hackathon.Autenticacao.Dominio.Interfaces.Repositorios;

namespace Postech.Hackathon.Autenticacao.Infra.Repositorios
{
    /// <summary>
    /// Repositório para gerenciar serviços no MongoDB.
    /// </summary>
    public class ServicoRepositorio(IMongoClient mongoClient) : IServicoRepositorio
    {
        /// <summary>
        /// Banco de dados do MongoDB.
        /// </summary>
        private readonly IMongoDatabase _bancoDeDados = mongoClient.GetDatabase("autenticacao");

        /// <summary>
        /// Obtém um serviço com base no ClientId e ClientSecret fornecidos.
        /// </summary>
        /// <param name="ClientId">Identificador do cliente.</param>
        /// <param name="ClientSecret">Segredo do cliente.</param>
        /// <returns>Um objeto Servico correspondente ou null se não encontrado.</returns>
        public Servico ObterServico(Guid ClientId, string ClientSecret)
        {
            var colecaoServicos = ObterColecaoServicos();
            var filtro = Builders<Servico>.Filter.And(
                Builders<Servico>.Filter.Eq(u => u.ClientId, ClientId.ToString()),
                Builders<Servico>.Filter.Eq(u => u.ClientSecret, ClientSecret));
            return colecaoServicos.Find(filtro).FirstOrDefault();
        }

        /// <summary>
        /// Obtém a coleção de serviços do banco de dados.
        /// </summary>
        /// <returns>Uma coleção de serviços.</returns>
        public IMongoCollection<Servico> ObterColecaoServicos()
        {
            return _bancoDeDados.GetCollection<Servico>("servicos");
        }
    }
}
