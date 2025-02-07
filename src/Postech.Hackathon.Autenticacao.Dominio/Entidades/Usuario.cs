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
        /// Lista de papeis associados ao usuário.
        /// </summary>
        public List<string> Papeis { get; private set; }

        /// <summary>
        /// Lista de escopos associados ao usuário.
        /// </summary>
        public List<string> Escopos { get; private set; }

        public Usuario(string nome, string email, string documento, string senha, TipoPerfilEnumerador tipoPerfil)
        {
            Nome = nome;
            Email = email;
            Documento = documento;
            Senha = senha;
            TipoPerfil = tipoPerfil;
            Escopos = CadastrarEscopos(tipoPerfil);
            Papeis = CadastrarPapeis(tipoPerfil);
        }

        /// <summary>
        /// Cadastro de escopos de acordo com o tipo de perfil do usuário.
        /// </summary>
        public List<string> CadastrarEscopos(TipoPerfilEnumerador tipoPerfil)
        {
            List<string> escopos = new List<string>();
            switch (tipoPerfil)
            {
                case TipoPerfilEnumerador.Medico:
                    escopos.Add("medico.read");
                    escopos.Add("medico.write");
                    break;
                case TipoPerfilEnumerador.Paciente:
                    escopos.Add("paciente.read");
                    escopos.Add("paciente.write");
                    break;
            }
            return escopos;
        }

        /// <summary>
        /// Cadastro de papeis de acordo com o tipo de perfil do usuário.
        /// </summary>
        public List<string> CadastrarPapeis(TipoPerfilEnumerador tipoPerfil)
        {
            List<string> papeis = new List<string>();
            switch (tipoPerfil)
            {
                case TipoPerfilEnumerador.Medico:
                    papeis.Add("medico");
                    break;
                case TipoPerfilEnumerador.Paciente:
                    papeis.Add("paciente");
                    break;
            }
            return papeis;  
        }
    }
}
