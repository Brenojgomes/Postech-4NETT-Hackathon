namespace Postech.Hackathon.Autenticacao.Aplicacao.ViewModels.AutenticacaoServicos
{
    public class ServicoViewModel
    {
        public string Token { get; set; }

        public ServicoViewModel(string token)
        {
            Token = token;
        }
    }
}
