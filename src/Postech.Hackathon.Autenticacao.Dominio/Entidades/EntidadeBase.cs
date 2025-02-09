using System.Diagnostics.CodeAnalysis;

namespace Postech.Hackathon.Autenticacao.Dominio.Entidades
{
    /// <summary>
    /// Classe base para entidades do domínio.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class EntidadeBase
    {
        /// <summary>
        /// Identificador único da entidade.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Data e hora em que a entidade foi criada.
        /// </summary>
        public DateTime CriadoEm { get; protected set; }

        protected EntidadeBase()
        {
            Id = Guid.NewGuid();
            CriadoEm = DateTime.UtcNow;
        }
    }
}
