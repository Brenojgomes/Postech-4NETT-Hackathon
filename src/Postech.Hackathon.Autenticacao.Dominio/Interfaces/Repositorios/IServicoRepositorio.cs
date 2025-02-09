using Postech.Hackathon.Autenticacao.Dominio.Entidades;

namespace Postech.Hackathon.Autenticacao.Dominio.Interfaces.Repositorios
{
    /// <summary>
    /// Interface para o repositório de serviços.
    /// </summary>
    public interface IServicoRepositorio
    {
        /// <summary>
        /// Obtém um serviço com base no ID do cliente e no segredo do cliente.
        /// </summary>
        /// <param name="ClientId">ID do cliente.</param>
        /// <param name="ClientSecret">Segredo do cliente.</param>
        /// <returns>Um objeto do tipo Servico.</returns>
        Servico ObterServico(Guid ClientId, string ClientSecret);
    }
}
