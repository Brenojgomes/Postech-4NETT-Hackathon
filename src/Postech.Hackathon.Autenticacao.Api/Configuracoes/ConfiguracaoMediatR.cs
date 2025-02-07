using System.Diagnostics.CodeAnalysis;

namespace Postech.Hackathon.Autenticacao.Api.Configuracoes
{
    [ExcludeFromCodeCoverage]
    public static class ConfiguracaoMediatR
    {
        /// <summary>
        /// Adiciona os serviços MediatR à coleção de serviços especificada.
        /// </summary>
        public static void AdicionaMediatR(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });
        }
    }
}
