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
        /// <param name="id">id do usuario para o qual o token será gerado.</param>
        /// <param name="name">Nome para o qual o token será gerado.</param>
        /// <param name="escopos">Lista de escopos associados ao token.</param>
        /// <param name="escopos">Lista de papeis associados ao token.</param>
        /// <returns>Token gerado como uma string.</returns>
        string GerarToken(Guid id, string name, List<string> escopos, List<string> papeis);

        /// <summary>
        /// Verifica se o token é válido.
        /// </summary>
        /// <param name="token">JWT Token</param>
        bool ValidarToken(string token);
    }
}
