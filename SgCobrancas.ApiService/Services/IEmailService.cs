namespace SgCobrancas.ApiService.Services
{
    public interface IEmailService
    {
        Task<bool> EnviarMensagemAsync(string destinario, string assunto, string corpo);
    }
}
