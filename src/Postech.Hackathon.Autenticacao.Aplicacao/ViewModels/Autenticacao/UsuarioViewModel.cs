namespace Postech.Hackathon.Autenticacao.Aplicacao.ViewModels.Autenticacao
{
    public class UsuarioViewModel
    {
        public Guid IdUsuario { get; set; }

        public string Token { get; set; }

        public UsuarioViewModel(Guid idUsuario, string token)
        {
            IdUsuario = idUsuario;
            Token = token;
        }
    }
}
