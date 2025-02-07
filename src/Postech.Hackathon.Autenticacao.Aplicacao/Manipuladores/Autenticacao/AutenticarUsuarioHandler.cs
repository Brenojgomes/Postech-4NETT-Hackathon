using MediatR;
using Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Entradas.Autenticacao;
using Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Saidas;
using Postech.Hackathon.Autenticacao.Aplicacao.ViewModels.Autenticacao;
using Postech.Hackathon.Autenticacao.Dominio.Entidades;
using Postech.Hackathon.Autenticacao.Dominio.Excecoes;
using Postech.Hackathon.Autenticacao.Dominio.Interfaces.Repositorios;
using Postech.Hackathon.Autenticacao.Dominio.Servicos.Interfaces;

namespace Postech.Hackathon.Autenticacao.Aplicacao.Manipuladores.Autenticacao
{
    /// <summary>
    /// Manipulador responsável pela autenticação de usuários.
    /// </summary>
    public class AutenticarUsuarioHandler(IUsuarioRepositorio repositorio, IServicoToken servicoToken) : IRequestHandler<AutenticarUsuarioEntrada, SaidaPadrao<UsuarioViewModel>>
    {
        /// <summary>
        /// Repositório de usuários.
        /// </summary>
        private readonly IUsuarioRepositorio _repositorio = repositorio;

        /// <summary>
        /// Serviço para geração de tokens.
        /// </summary>
        private readonly IServicoToken _servicoToken = servicoToken;

        /// <summary>
        /// Método que trata a autenticação do usuário.
        /// </summary>
        /// <param name="comando">Comando com os dados de autenticação do usuário.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Resultado da autenticação com informações do usuário.</returns>
        public async Task<SaidaPadrao<UsuarioViewModel>> Handle(AutenticarUsuarioEntrada comando, CancellationToken cancellationToken)
        {
            Usuario usuario = null;
            if (!string.IsNullOrEmpty(comando.Documento))
                usuario = _repositorio.ObterUsuarioPeloDocumento(comando.Documento, comando.TipoPerfil);
            else if (!string.IsNullOrEmpty(comando.Email))
                usuario = _repositorio.ObterUsuarioPeloEmail(comando.Email, comando.TipoPerfil);
            else
                ExcecaoDeDominio.LancarQuando(true, "Informe o email ou documento do usuário.");

            NaoEncontradoExcecao.LancarQuandoEntidadeNula(usuario, "O usuário não foi localizado no sistema.");

            bool senhaValida = BCrypt.Net.BCrypt.Verify(comando.Senha, usuario.Senha);
            if (!senhaValida)
                throw new UnauthorizedAccessException("Credenciais inválidas.");

            var token = _servicoToken.GerarToken(usuario.Id, usuario.Nome, usuario.Escopos, usuario.Papeis);

            var usuarioViewModel = new UsuarioViewModel(usuario.Id, token);

            return new SaidaPadrao<UsuarioViewModel>(true, "O usuário foi autenticado com sucesso.", usuarioViewModel);
        }
    }
}
