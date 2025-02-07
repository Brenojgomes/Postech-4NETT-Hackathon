using MediatR;
using Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Entradas.Autenticacao;
using Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Saidas;
using Postech.Hackathon.Autenticacao.Aplicacao.Manipuladores.Autenticacao;
using Postech.Hackathon.Autenticacao.Aplicacao.ViewModels.Autenticacao;
using Postech.Hackathon.Autenticacao.Aplicacao.ViewModels.AutenticacaoServicos;

namespace Postech.Hackathon.Autenticacao.Api.Configuracoes
{
    public static class ConfiguracaoManipuladores
    {
        /// <summary>
        /// Adiciona as dependencias dos manipuladores.
        /// </summary>
        public static IServiceCollection AdicionarDependenciaManipuladores(this IServiceCollection services)
        {
            //Usuarios
            services.AddScoped<IRequestHandler<AdicionarUsuarioEntrada, SaidaPadrao<UsuarioViewModel>>, AdicaoUsuarioHandler>();
            services.AddScoped<IRequestHandler<AutenticarUsuarioEntrada, SaidaPadrao<UsuarioViewModel>>, AutenticarUsuarioHandler>();

            //Servicos
            services.AddScoped<IRequestHandler<AutenticarServicoEntrada, SaidaPadrao<ServicoViewModel>>, AutenticarServicoHandler>();
            return services;
        }
    }
}
