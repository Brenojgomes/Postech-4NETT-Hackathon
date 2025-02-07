using Microsoft.AspNetCore.Diagnostics;
using Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Saidas;
using Postech.Hackathon.Autenticacao.Dominio.Excecoes;
using System.Net;

namespace Postech.Hackathon.Autenticacao.Api.Configuracoes
{
    public static class ConfiguracaoManipuladorExcecoes
    {
        /// <summary>
        /// Configura o middleware para tratar exceções de forma personalizada.
        /// </summary>
        /// <param name="app">O construtor da aplicação.</param>
        public static void UsarManipuladorExcecoesPersonalizado(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(configurar =>
            {
                configurar.Run(async contexto =>
                {
                    var erroTratado = contexto.Features.Get<IExceptionHandlerPathFeature>();

                    if (erroTratado?.Error is not null)
                    {
                        int codigoStatus = (int)HttpStatusCode.InternalServerError;
                        string mensagemErro = "Ocorreu um erro inesperado";

                        switch (erroTratado.Error)
                        {
                            case ExcecaoDeDominio:
                                codigoStatus = (int)HttpStatusCode.BadRequest;
                                mensagemErro = erroTratado.Error.Message;
                                break;

                            case NaoEncontradoExcecao:
                                codigoStatus = (int)HttpStatusCode.NotFound;
                                mensagemErro = erroTratado.Error.Message;
                                break; 
                            
                            case UnauthorizedAccessException:
                                codigoStatus = (int)HttpStatusCode.Unauthorized;
                                mensagemErro = erroTratado.Error.Message;
                                break;

                            case ExcecaoDeConflito:
                                codigoStatus = (int)HttpStatusCode.Conflict;
                                mensagemErro = erroTratado.Error.Message;
                                break;
                        }

                        contexto.Response.StatusCode = codigoStatus;
                        await contexto.Response.WriteAsJsonAsync(new SaidaPadrao(false, mensagemErro));
                    }
                });
            });
        }
    }
}
