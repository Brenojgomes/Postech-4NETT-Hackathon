using Postech.Hackathon.Autenticacao.Dominio.Entidades;
using Postech.Hackathon.Autenticacao.Dominio.Enumeradores;

namespace Postech.Hackathon.Autenticacao.TestesUnitarios.Dominio.Entidades
{
    public class UsuarioTests
    {
        [Fact]
        public void Usuario_DeveInicializarComValoresCorretos()
        {
            // Arrange
            var nome = "João";
            var email = "joao@example.com";
            var documento = "123456789";
            var senha = "senha123";
            var tipoPerfil = TipoPerfilEnumerador.Medico;

            // Act
            var usuario = new Usuario(nome, email, documento, senha, tipoPerfil);

            // Assert
            Assert.Equal(nome, usuario.Nome);
            Assert.Equal(email, usuario.Email);
            Assert.Equal(documento, usuario.Documento);
            Assert.Equal(senha, usuario.Senha);
            Assert.Equal(tipoPerfil, usuario.TipoPerfil);
            Assert.Contains("medico.read", usuario.Escopos);
            Assert.Contains("medico.write", usuario.Escopos);
            Assert.Contains("medico", usuario.Papeis);
        }

        [Fact]
        public void CadastrarEscopos_DeveRetornarEscoposCorretosParaMedico()
        {
            // Arrange
            var tipoPerfil = TipoPerfilEnumerador.Medico;
            var usuario = new Usuario("João", "joao@example.com", "123456789", "senha123", tipoPerfil);

            // Act
            var escopos = usuario.CadastrarEscopos(tipoPerfil);

            // Assert
            Assert.Contains("medico.read", escopos);
            Assert.Contains("medico.write", escopos);
        }

        [Fact]
        public void CadastrarEscopos_DeveRetornarEscoposCorretosParaPaciente()
        {
            // Arrange
            var tipoPerfil = TipoPerfilEnumerador.Paciente;
            var usuario = new Usuario("Maria", "maria@example.com", "987654321", "senha456", tipoPerfil);

            // Act
            var escopos = usuario.CadastrarEscopos(tipoPerfil);

            // Assert
            Assert.Contains("paciente.read", escopos);
            Assert.Contains("paciente.write", escopos);
        }

        [Fact]
        public void CadastrarPapeis_DeveRetornarPapeisCorretosParaMedico()
        {
            // Arrange
            var tipoPerfil = TipoPerfilEnumerador.Medico;
            var usuario = new Usuario("João", "joao@example.com", "123456789", "senha123", tipoPerfil);

            // Act
            var papeis = usuario.CadastrarPapeis(tipoPerfil);

            // Assert
            Assert.Contains("medico", papeis);
        }

        [Fact]
        public void CadastrarPapeis_DeveRetornarPapeisCorretosParaPaciente()
        {
            // Arrange
            var tipoPerfil = TipoPerfilEnumerador.Paciente;
            var usuario = new Usuario("Maria", "maria@example.com", "987654321", "senha456", tipoPerfil);

            // Act
            var papeis = usuario.CadastrarPapeis(tipoPerfil);

            // Assert
            Assert.Contains("paciente", papeis);
        }
    }
}
