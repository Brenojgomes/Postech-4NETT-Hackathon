using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Postech.Hackathon.Autenticacao.Api.Configuracoes
{
    /// <summary>
    /// Classe responsável pela configuração da autenticação JWT na aplicação.
    /// </summary>
    public static class ConfiguracaoAutenticacao
    {
        /// <summary>
        /// Adiciona a configuração de autenticação ao contêiner de serviços.
        /// </summary>
        /// <param name="services">Coleção de serviços onde a configuração será adicionada.</param>
        public static void AdicionarConfiguracaoDeAutenticacao(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        }
    }
}
