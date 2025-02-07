using Moq;
using Postech.Hackathon.Autenticacao.Aplicacao.Comandos.Entradas.Autenticacao;
using Postech.Hackathon.Autenticacao.Aplicacao.Manipuladores.Autenticacao;
using Postech.Hackathon.Autenticacao.Dominio.Entidades;
using Postech.Hackathon.Autenticacao.Dominio.Enumeradores;
using Postech.Hackathon.Autenticacao.Dominio.Excecoes;
using Postech.Hackathon.Autenticacao.Dominio.Interfaces.Repositorios;
using Postech.Hackathon.Autenticacao.Dominio.Servicos.Interfaces;

namespace Postech.Hackathon.Autenticacao.TestesUnitarios.Aplicacao.Manipuladores
{
    public class AutenticarUsuarioHandlerTests
    {
        private readonly Mock<IUsuarioRepositorio> _repositorioMock;
        private readonly Mock<IServicoToken> _servicoTokenMock;
        private readonly AutenticarUsuarioHandler _handler;

        public AutenticarUsuarioHandlerTests()
        {
            _repositorioMock = new Mock<IUsuarioRepositorio>();
            _servicoTokenMock = new Mock<IServicoToken>();
            _handler = new AutenticarUsuarioHandler(_repositorioMock.Object, _servicoTokenMock.Object);
        }

        [Fact]
        public async Task Handle_DeveAutenticarUsuarioERetornarSaidaPadrao()
        {
            // Arrange
            var comando = new AutenticarUsuarioEntrada
            {
                Email = "joao@example.com",
                Senha = "senha123",
                TipoPerfil = TipoPerfilEnumerador.Medico
            };

            var usuario = new Usuario("João", comando.Email, "123456789", BCrypt.Net.BCrypt.HashPassword(comando.Senha), comando.TipoPerfil);
            _repositorioMock.Setup(r => r.ObterUsuarioPorEmailOuDocumento(comando.Email,comando.Documento, comando.TipoPerfil)).Returns(usuario);
            _servicoTokenMock.Setup(s => s.GerarToken(usuario.Id, usuario.Nome, usuario.Escopos, usuario.Papeis)).Returns("fake-jwt-token");

            // Act
            var resultado = await _handler.Handle(comando, CancellationToken.None);

            // Assert
            Assert.True(resultado.Sucesso);
            Assert.Equal("O usuário foi autenticado com sucesso.", resultado.Mensagem);
            Assert.NotNull(resultado.Dados);
            Assert.Equal("fake-jwt-token", resultado.Dados.Token);
        }

        [Fact]
        public async Task Handle_DeveLancarExcecaoQuandoUsuarioNaoEncontrado()
        {
            // Arrange
            var comando = new AutenticarUsuarioEntrada
            {
                Email = "joao@example.com",
                Senha = "senha123",
                TipoPerfil = TipoPerfilEnumerador.Medico
            };

            _repositorioMock.Setup(r => r.ObterUsuarioPorEmailOuDocumento(comando.Email,comando.Documento, comando.TipoPerfil)).Returns((Usuario)null);

            // Act & Assert
            var excecao = await Assert.ThrowsAsync<NaoEncontradoExcecao>(() => _handler.Handle(comando, CancellationToken.None));
            Assert.Equal("O usuário não foi localizado no sistema.", excecao.Message);
        }

        [Fact]
        public async Task Handle_DeveLancarExcecaoQuandoSenhaInvalida()
        {
            // Arrange
            var comando = new AutenticarUsuarioEntrada
            {
                Email = "joao@example.com",
                Senha = "senha123",
                TipoPerfil = TipoPerfilEnumerador.Medico
            };

            var usuario = new Usuario("João", comando.Email, "123456789", BCrypt.Net.BCrypt.HashPassword("senhaDiferente"), comando.TipoPerfil);
            _repositorioMock.Setup(r => r.ObterUsuarioPorEmailOuDocumento(comando.Email, comando.Documento, comando.TipoPerfil)).Returns(usuario);

            // Act & Assert
            var excecao = await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(comando, CancellationToken.None));
            Assert.Equal("Credenciais inválidas.", excecao.Message);
        }

        [Fact]
        public async Task Handle_DeveLancarExcecaoQuandoEmailEDocumentoNaoInformados()
        {
            // Arrange
            var comando = new AutenticarUsuarioEntrada
            {
                Senha = "senha123",
                TipoPerfil = TipoPerfilEnumerador.Medico
            };

            // Act & Assert
            var excecao = await Assert.ThrowsAsync<ExcecaoDeDominio>(() => _handler.Handle(comando, CancellationToken.None));
            Assert.Equal("Informe o email ou documento do usuário.", excecao.Message);
        }
    }
}
