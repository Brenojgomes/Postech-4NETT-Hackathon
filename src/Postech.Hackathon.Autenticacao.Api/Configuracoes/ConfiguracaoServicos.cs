using Postech.Hackathon.Autenticacao.Dominio.Servicos;
using Postech.Hackathon.Autenticacao.Dominio.Servicos.Interfaces;

namespace Postech.Hackathon.Autenticacao.Api.Configuracoes
{
    public static class ConfiguracaoServicos
    {
        /// <summary>
        /// Adiciona as dependencias dos serviços.
        /// </summary>
        public static IServiceCollection AdicionarDependenciasServicos(this IServiceCollection services)
        {
            services.AddScoped<IServicoToken, ServicoToken>();
            return services;
        }
    }
}
