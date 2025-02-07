using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Postech.Hackathon.Autenticacao.Dominio.Servicos.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        /// Gera um token com base no nome e nos escopos fornecidos.
        /// </summary>
        /// <param name="id">id do usuario para o qual o token será gerado.</param>
        /// <param name="name">Nome para o qual o token será gerado.</param>
        /// <param name="escopos">Lista de escopos associados ao token.</param>
        /// <param name="escopos">Lista de papeis associados ao token.</param>
        /// <returns>Token gerado como uma string.</returns>
        public string GerarToken(Guid id, string name, List<string> escopos, List<string> papeis)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim("Id", id.ToString()),
            };

            claims.AddRange(escopos.Select(escopo => new Claim(ClaimTypes.Role, escopo)));
            claims.AddRange(papeis.Select(papeis => new Claim("Papel", papeis)));

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
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
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
