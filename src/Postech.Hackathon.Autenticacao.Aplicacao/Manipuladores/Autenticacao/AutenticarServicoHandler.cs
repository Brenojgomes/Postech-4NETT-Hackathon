﻿using MediatR;
using Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Entradas.Autenticacao;
using Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Saidas;
using Postech.Hackathon.Autenticacao.Aplicacao.ViewModels.AutenticacaoServicos;
using Postech.Hackathon.Autenticacao.Dominio.Excecoes;
using Postech.Hackathon.Autenticacao.Dominio.Interfaces.Repositorios;
using Postech.Hackathon.Autenticacao.Dominio.Servicos.Interfaces;

namespace Postech.Hackathon.Autenticacao.Aplicacao.Manipuladores.Autenticacao
{
    /// <summary>
    /// Manipulador responsável pela autenticação de serviços.
    /// </summary>
    public class AutenticarServicoHandler(IServicoRepositorio repositorio, IServicoToken servicoToken) : IRequestHandler<AutenticarServicoEntrada, SaidaPadrao<ServicoViewModel>>
    {
        /// <summary>
        /// Repositório de serviços.
        /// </summary>
        private readonly IServicoRepositorio _repositorio = repositorio;

        /// <summary>
        /// Serviço para geração de tokens.
        /// </summary>
        private readonly IServicoToken _servicoToken = servicoToken;

        /// <summary>
        /// Método responsável por tratar a autenticação do serviço.
        /// </summary>
        /// <param name="comando">Comando contendo as informações de autenticação.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Resultado da autenticação com o token gerado.</returns>
        public async Task<SaidaPadrao<ServicoViewModel>> Handle(AutenticarServicoEntrada comando, CancellationToken cancellationToken)
        {
            var servico = _repositorio.ObterServico(comando.ClientId, comando.ClientSecret);
            NaoEncontradoExcecao.LancarQuandoEntidadeNula(servico, "O serviço não foi localizado no sistema.");

            var token = _servicoToken.GerarToken(servico.Id,servico.Nome, servico.Escopos, servico.Papeis);
            var servicoViewModel = new ServicoViewModel(token);

            return new SaidaPadrao<ServicoViewModel>(true, " O serviço foi autenticado com sucesso.", servicoViewModel);
        }
    }
}
