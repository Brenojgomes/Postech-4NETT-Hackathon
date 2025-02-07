using Microsoft.Extensions.DependencyInjection;
using Postech.Hackathon.Autenticacao.Dominio.Interfaces.Repositorios;
using Postech.Hackathon.Autenticacao.Infra.Repositorios;

namespace Postech.Hackathon.Autenticacao.Infra.Configuracoes
{
    public static class ConfiguracaoRepositorios
    {
        /// <summary>
        /// Adiciona os serviços de repositório.
        /// </summary>
        public static IServiceCollection AdicionarRepositorios(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IServicoRepositorio, ServicoRepositorio>();
            return services;
        }
    }
}
