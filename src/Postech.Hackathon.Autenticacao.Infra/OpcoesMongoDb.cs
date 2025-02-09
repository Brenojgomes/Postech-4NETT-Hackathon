using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postech.Hackathon.Autenticacao.Infra
{
    /// <summary>
    /// Representa as opções de configuração para conexão com o MongoDB.
    /// </summary>
    public class OpcoesMongoDb
    {
        /// <summary>
        /// A string de conexão com o MongoDB.
        /// </summary>
        public string StringConexao { get; set; }

        /// <summary>
        /// O nome do banco de dados no MongoDB.
        /// </summary>
        public string NomeBancoDados { get; set; }
    }
}
