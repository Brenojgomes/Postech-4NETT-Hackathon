using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postech.Hackathon.Autenticacao.Infra.Configuracoes
{
    /// <summary>
    /// Fornece métodos de extensão para configurar os serviços de infraestrutura.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ConfiguracaoModuloInfraestrutura
    {
        /// <summary>
        /// Adiciona os serviços de infraestrutura à <see cref="IServiceCollection"/> especificada.
        /// </summary>
        /// <param name="services">A <see cref="IServiceCollection"/> onde os serviços serão adicionados.</param>
        /// <returns>A <see cref="IServiceCollection"/> modificada.</returns>
        public static IServiceCollection AdicionarInfraestrutura(this IServiceCollection services)
        {
            services.AdicionarMongo();
            services.AdicionarRepositorios();
            return services;
        }      
    }
}
