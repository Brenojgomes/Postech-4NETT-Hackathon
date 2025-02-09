using MediatR;
using Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Entradas.Autenticacao;
using Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Saidas;
using Postech.Hackathon.Autenticacao.Dominio.Servicos.Interfaces;

namespace Postech.Hackathon.Autenticacao.Aplicacao.Manipuladores.Autenticacao
{
    public class ValidarTokenHandler(IServicoToken servicoToken) : IRequestHandler<ValidarTokenEntrada, SaidaPadrao>
    {
        /// <summary>
        /// Serviço para manipulação de tokens.
        /// </summary>
        private readonly IServicoToken _servicoToken = servicoToken;

        /// <summary>
        /// Método que valida o Token.
        /// </summary>
        /// <param name="comando">Comando com os dados do token.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Resultado da autenticação.</returns>
        public async Task<SaidaPadrao> Handle(ValidarTokenEntrada comando, CancellationToken cancellationToken)
        {
            var tokenValido = _servicoToken.ValidarToken(comando.Token);
            if(!tokenValido)
                throw new UnauthorizedAccessException("O token está inválido");

            return new SaidaPadrao(tokenValido, "O token foi validado com sucesso.");
        }
    }
}
