using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postech.Hackathon.Autenticacao.Dominio.Excecoes
{
    /// <summary>
    /// Representa erros que ocorrem dentro do domínio da aplicação.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ExcecaoDeDominio(string mensagem) : Exception(mensagem)
    {
        /// <summary>
        /// Lança uma ExcecaoDeDominio caso a regra especificada seja inválida.
        /// </summary>
        /// <param name="regraInvalida">Indica se a regra é inválida.</param>
        /// <param name="mensagem">A mensagem de erro.</param>
        public static void LancarQuando(bool regraInvalida, string mensagem)
        {
            if (regraInvalida)
            {
                throw new ExcecaoDeDominio(mensagem);
            }
        }
    }
}
