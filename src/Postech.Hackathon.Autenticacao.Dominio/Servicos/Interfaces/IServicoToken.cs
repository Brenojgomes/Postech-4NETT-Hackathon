namespace Postech.Hackathon.Autenticacao.Dominio.Servicos.Interfaces
{
    /// <summary>
    /// Interface para serviços de geração de tokens.
    /// </summary>
    public interface IServicoToken
    {
        /// <summary>
        /// Gera um token com base no nome e nos escopos fornecidos.
        /// </summary>
        /// <param name="name">Nome para o qual o token será gerado.</param>
        /// <param name="escopos">Lista de escopos associados ao token.</param>
        /// <returns>Token gerado como uma string.</returns>
        string GerarToken(string name, List<string> escopos);
    }
}
