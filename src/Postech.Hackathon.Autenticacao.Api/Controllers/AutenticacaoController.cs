using MediatR;
using Microsoft.AspNetCore.Mvc;
using Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Entradas.Autenticacao;
using Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Saidas;
using Postech.Hackathon.Autenticacao.Aplicacao.ViewModels.Autenticacao;
using Postech.Hackathon.Autenticacao.Aplicacao.ViewModels.AutenticacaoServicos;

namespace Postech.Hackathon.Autenticacao.Api.Controllers
{
    /// <summary>
    /// Controlador responsável pela autenticação de usuários e serviços.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AutenticacaoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Adiciona um novo usuário.
        /// </summary>
        /// <param name="comando">Dados do usuário a ser adicionado.</param>
        /// <returns>Resultado da operação de adição do usuário.</returns>
        [HttpPost("usuarios")]
        public async Task<SaidaPadrao<UsuarioViewModel>> Usuarios([FromBody] AdicionarUsuarioEntrada comando)
        {
            var teste = await _mediator.Send(comando);
            return teste;
        }

        /// <summary>
        /// Autentica um usuário existente.
        /// </summary>
        /// <param name="comando">Dados de autenticação do usuário.</param>
        /// <returns>Resultado da operação de autenticação do usuário.</returns>
        [HttpPost("autenticacoes-usuarios")]
        public async Task<SaidaPadrao<UsuarioViewModel>> AutenticarUsuario([FromBody] AutenticarUsuarioEntrada comando)
        {
            var teste = await _mediator.Send(comando);
            return teste;
        }

        /// <summary>
        /// Autentica um serviço.
        /// </summary>
        /// <param name="comando">Dados de autenticação do serviço.</param>
        /// <returns>Resultado da operação de autenticação do serviço.</returns>
        [HttpPost("autenticacoes-servicos")]
        public async Task<SaidaPadrao<ServicoViewModel>> AutenticarServico([FromBody] AutenticarServicoEntrada comando)
        {
            var teste = await _mediator.Send(comando);
            return teste;
        }
    }
}
