using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Postech.Hackathon.Autenticacao.Infra.Configuracoes
{
    /// <summary>
    /// Configuração do MongoDB.
    /// </summary>
    public static class ConfiguracaoMongoDb
    {
        /// <summary>
        /// Adiciona os serviços do MongoDB à <see cref="IServiceCollection"/> especificada.
        /// </summary>
        public static IServiceCollection AdicionarMongo(this IServiceCollection services)
        {
            services.AddSingleton<OpcoesMongoDb>(sp =>
            {
                var configuracao = sp.GetService<IConfiguration>();
                var opcoes = new OpcoesMongoDb();
                configuracao.GetSection("Mongo").Bind(opcoes);
                return opcoes;
            });

            services.AddSingleton<IMongoClient>(sp =>
            {
                var configuracao = sp.GetService<IConfiguration>();
                var opcoes = sp.GetService<OpcoesMongoDb>();
                var cliente = new MongoClient(opcoes.StringConexao);
                return cliente;
            });

            services.AddTransient(sp =>
            {
                BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
                var cliente = sp.GetService<IMongoClient>();
                var opcoes = sp.GetService<OpcoesMongoDb>();
                return cliente.GetDatabase(opcoes.NomeBancoDados);
            });

            return services;
        }
    }
}
