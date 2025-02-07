using Postech.Hackathon.Autenticacao.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postech.Hackathon.Autenticacao.Dominio.Entidades
{
    /// <summary>
    /// Representa um usuário no sistema de autenticação.
    /// </summary>
    public class Usuario : EntidadeBase
    {
        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public string Nome { get; private set; }

        /// <summary>
        /// Email do usuário.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Documento de identificação do usuário.
        /// </summary>
        public string Documento { get; private set; }

        /// <summary>
        /// Senha do usuário.
        /// </summary>
        public string Senha { get; private set; }

        /// <summary>
        /// Tipo de perfil do usuário.
        /// </summary>
        public TipoPerfilEnumerador TipoPerfil { get; private set; }

        /// <summary>
        /// Lista de escopos associados ao usuário.
        /// </summary>
        public List<string> Escopos { get; private set; }

        public Usuario(string nome, string email, string documento, string senha, TipoPerfilEnumerador tipoPerfil, List<string> escopos)
        {
            Nome = nome;
            Email = email;
            Documento = documento;
            Senha = senha;
            TipoPerfil = tipoPerfil;
            Escopos = escopos;
        }
    }
}
