using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Saidas
{
    /// <summary>
    /// Retorno padrão dos endpoints.
    /// </summary>
    public class SaidaPadrao<T>
    {
        public SaidaPadrao() { }

        public SaidaPadrao(bool sucesso, string mensagem, T data)
        {
            this.Sucesso = sucesso;
            Mensagem = mensagem;
            Data = data;
        }

        public SaidaPadrao(bool sucesso, T data)
        {
            Sucesso = sucesso;
            Data = data;
        }

        public SaidaPadrao(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }

        /// <summary>
        /// Indica se o processamento solicitado foi concluído com sucesso.
        /// </summary>
        [JsonPropertyName("sucesso")]
        public bool Sucesso { get; init; }

        /// <summary>
        /// Mensagem que descreve o resultado do processamento.
        /// </summary>
        [JsonPropertyName("mensagem")]
        public string Mensagem { get; init; }

        /// <summary>
        /// Conjunto de dados retornados do processamento solicitado.
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; init; }
    }

    /// <summary>
    /// Retorno padrão dos endpoints.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public record SaidaPadrao
    {
        public SaidaPadrao() { }

        public SaidaPadrao(bool sucesso, string mensagem, object data)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Data = data;
        }

        public SaidaPadrao(bool sucesso, object data)
        {
            Sucesso = sucesso;
            Data = data;
        }

        public SaidaPadrao(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }

        /// <summary>
        /// Indica se o processamento solicitado foi concluído com sucesso.
        /// </summary>
        [JsonPropertyName("sucesso")]
        public bool Sucesso { get; init; }

        /// <summary>
        /// Mensagem que descreve o resultado do processamento.
        /// </summary>
        [JsonPropertyName("mensagem")]
        public string Mensagem { get; init; }

        /// <summary>
        /// Conjunto de dados retornados do processamento solicitado.
        /// </summary>
        [JsonPropertyName("data")]
        public object Data { get; init; }
    }
}