﻿using MediatR;
using Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Saidas;
using Postech.Hackathon.Autenticacao.Aplicacao.ViewModels.Autenticacao;
using Postech.Hackathon.Autenticacao.Dominio.Enumeradores;

namespace Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Entradas.Autenticacao
{
    /// <summary>
    /// Classe que representa a entrada para autenticação de um usuário.
    /// </summary>
    public class AutenticarUsuarioEntrada : IRequest<SaidaPadrao<UsuarioViewModel>>
    {
        /// <summary>
        /// E-mail do usuário.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Documento do usuário.
        /// </summary>
        public string Documento { get; set; }

        /// <summary>
        /// Senha do usuário.
        /// </summary>
        public string Senha { get; set; }

        /// <summary>
        /// Tipo de perfil do usuário.
        /// </summary>
        public TipoPerfilEnumerador TipoPerfil { get; set; }
    }
}
