using MediatR;
using Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Entradas.Autenticacao;
using Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Saidas;
using Postech.Hackathon.Autenticacao.Aplicacao.ViewModels.Autenticacao;
using Postech.Hackathon.Autenticacao.Dominio.Entidades;
using Postech.Hackathon.Autenticacao.Dominio.Enumeradores;
using Postech.Hackathon.Autenticacao.Dominio.Interfaces.Repositorios;
using Postech.Hackathon.Autenticacao.Dominio.Servicos.Interfaces;

namespace Postech.Hackathon.Autenticacao.Aplicacao.Manipuladores.Autenticacao
{
    /// <summary>
    /// Manipulador responsável pela adição de usuários.
    /// </summary>
    public class AdicaoUsuarioHandler(IUsuarioRepositorio repositorio, IServicoToken servicoToken) : IRequestHandler<AdicionarUsuarioEntrada, SaidaPadrao<UsuarioViewModel>>
    {
        /// <summary>
        /// Repositório de usuários.
        /// </summary>
        private readonly IUsuarioRepositorio _repositorio = repositorio;

        /// <summary>
        /// Serviço de geração de token.
        /// </summary>
        private readonly IServicoToken _servicoToken = servicoToken;

        /// <summary>
        /// Manipula o comando para adicionar um usuário.
        /// </summary>
        /// <param name="comando">Comando contendo os dados do usuário a ser adicionado.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Resultado da operação de adição do usuário.</returns>
        public async Task<SaidaPadrao<UsuarioViewModel>> Handle(AdicionarUsuarioEntrada comando, CancellationToken cancellationToken)
        {
            List<string> escopos = new List<string>();

            if (comando.TipoPerfil == TipoPerfilEnumerador.Medico)
                escopos.Add("medico");
            else if (comando.TipoPerfil == TipoPerfilEnumerador.Paciente)
                escopos.Add("paciente");

            var usuario = new Usuario(comando.Nome, comando.Email, comando.Documento, comando.Senha, comando.TipoPerfil, escopos);
            _repositorio.CadastrarUsuario(usuario);

            var token = _servicoToken.GerarToken(usuario.Nome, escopos);

            var usuarioViewModel = new UsuarioViewModel(usuario.Id, token);

            return new SaidaPadrao<UsuarioViewModel>(true, "O usuário foi cadastrado com sucesso", usuarioViewModel);
        }
    }
}
