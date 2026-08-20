using SgCobrancas.ApiService.DTOs;

namespace SgCobrancas.ApiService.Services
{
    public interface IEmailService
    {
        Task<bool> EnviarMensagemAsync(int id, string destinario, string assunto, string corpo);
        Task<EmailDTO> CreateEmail(EmailDTO email);
        Task<EmailDTO> EditEmail(int id, EmailDTO email);
        Task<EmailDTO> GetEmailById(int id);
    }
}
