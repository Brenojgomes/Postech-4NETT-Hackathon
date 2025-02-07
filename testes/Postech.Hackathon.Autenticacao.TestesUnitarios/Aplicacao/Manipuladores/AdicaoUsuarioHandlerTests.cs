using Moq;
using Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Entradas.Autenticacao;
using Postech.Hackathon.Autenticacao.Aplicacao.Manipuladores.Autenticacao;
using Postech.Hackathon.Autenticacao.Dominio.Entidades;
using Postech.Hackathon.Autenticacao.Dominio.Enumeradores;
using Postech.Hackathon.Autenticacao.Dominio.Interfaces.Repositorios;
using Postech.Hackathon.Autenticacao.Dominio.Servicos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postech.Hackathon.Autenticacao.TestesUnitarios.Aplicacao.Manipuladores
{
    public class AdicaoUsuarioHandlerTests
    {
        private readonly Mock<IUsuarioRepositorio> _repositorioMock;
        private readonly Mock<IServicoToken> _servicoTokenMock;
        private readonly AdicaoUsuarioHandler _handler;

        public AdicaoUsuarioHandlerTests()
        {
            _repositorioMock = new Mock<IUsuarioRepositorio>();
            _servicoTokenMock = new Mock<IServicoToken>();
            _handler = new AdicaoUsuarioHandler(_repositorioMock.Object, _servicoTokenMock.Object);
        }

        [Fact]
        public async Task Handle_DeveAdicionarUsuarioERetornarSaidaPadrao()
        {
            // Arrange
            var comando = new AdicionarUsuarioEntrada
            {
                Nome = "João",
                Email = "joao@example.com",
                Documento = "123456789",
                Senha = "senha123",
                TipoPerfil = TipoPerfilEnumerador.Medico
            };

            var usuario = new Usuario(comando.Nome, comando.Email, comando.Documento, BCrypt.Net.BCrypt.HashPassword(comando.Senha), comando.TipoPerfil);
            _repositorioMock.Setup(r => r.CadastrarUsuario(It.IsAny<Usuario>())).Verifiable();
            _servicoTokenMock.Setup(s => s.GerarToken(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<List<string>>()))
                .Returns("fake-jwt-token");

            // Act
            var resultado = await _handler.Handle(comando, CancellationToken.None);

            // Assert
            _repositorioMock.Verify(r => r.CadastrarUsuario(It.IsAny<Usuario>()), Times.Once);
            Assert.True(resultado.Sucesso);
            Assert.Equal("O usuário foi cadastrado com sucesso", resultado.Mensagem);
            Assert.NotNull(resultado.Dados);
            Assert.Equal("fake-jwt-token", resultado.Dados.Token);
        }
    }
}
