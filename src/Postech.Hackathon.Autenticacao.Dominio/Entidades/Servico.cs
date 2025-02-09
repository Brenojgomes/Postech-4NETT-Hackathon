namespace Postech.Hackathon.Autenticacao.Dominio.Entidades
{
    /// <summary>
    /// Representa um serviço de autenticação.
    /// </summary>
    public class Servico : EntidadeBase
    {
        /// <summary>
        /// Identificador do cliente.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Nome do serviço.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Segredo do cliente.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Lista de papeis associados ao serviço.
        /// </summary>
        public List<string> Papeis { get; set; }

        /// <summary>
        /// Lista de escopos associados ao serviço.
        /// </summary>
        public List<string> Escopos { get; set; }
    }
}
