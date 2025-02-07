using Microsoft.Extensions.DependencyInjection;
using Postech.Hackathon.Autenticacao.Api.Configuracoes;
using Postech.Hackathon.Autenticacao.Infra.Configuracoes;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AdicionarInfraestrutura();
builder.Services.AdicionarDependenciasServicos();
builder.Services.AdicionarDependenciaManipuladores();
builder.Services.AdicionaMediatR();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
