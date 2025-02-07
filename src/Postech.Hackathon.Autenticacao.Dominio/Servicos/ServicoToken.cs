using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Postech.Hackathon.Autenticacao.Dominio.Entidades;
using Postech.Hackathon.Autenticacao.Dominio.Servicos.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Postech.Hackathon.Autenticacao.Dominio.Servicos
{
    /// <summary>
    /// Classe responsável pela geração de tokens JWT.
    /// </summary>
    public class ServicoToken : IServicoToken
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ServicoToken"/>.
        /// </summary>
        /// <param name="configuration">Configuração da aplicação.</param>
        public ServicoToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Gera um token JWT com base no nome e nos escopos fornecidos.
        /// </summary>
        /// <param name="name">Nome do usuário.</param>
        /// <param name="escopos">Lista de escopos do usuário.</param>
        /// <returns>Token JWT gerado.</returns>
        public string GerarToken(string name, List<string> escopos)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name)
            };

            claims.AddRange(escopos.Select(escopo => new Claim(ClaimTypes.Role, escopo)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = audience,
                Issuer = issuer
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Verifica se o token é válido.
        /// </summary>
        /// <param name="token">JWT Token</param>
        public bool ValidarToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
