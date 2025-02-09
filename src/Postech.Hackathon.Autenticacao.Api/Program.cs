using Postech.Hackathon.Autenticacao.Api.Configuracoes;
using Postech.Hackathon.Autenticacao.Infra.Configuracoes;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:5010");
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true);

builder.Services.AdicionarConfiguracaoDeAutenticacao(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AdicionarInfraestrutura();
builder.Services.AdicionarDependenciasServicos();
builder.Services.AdicionarDependenciaManipuladores();
builder.Services.AdicionarMediatR();
builder.Services.AdicionarConfiguracaoSwagger();

var app = builder.Build();
app.UsarManipuladorExcecoesPersonalizado();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Postech.Hackathon.Autenticacao.Api v1");
});


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
