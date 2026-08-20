using MimeKit;
using MailKit.Net.Smtp;
using SgCobrancas.ApiService.DTOs;
using SgCobrancas.ApiService.Data;
using SgCobrancas.ApiService.Migrations;

namespace SgCobrancas.ApiService.Services
{
    public class EmailService : IEmailService
    {
        private readonly AppDbContext _db;
        public EmailService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> EnviarMensagemAsync(int id, string destinario, string assunto, string corpo)
        {
            if (id == null || id <= 0) return false;
            var email = _db.EmailConfigs.FirstOrDefault(e => e.Id == id);
            if (email == null) return false;

            var _smtpoHost = email.SmtpServer;
            var _smtpPort = email.SmtpPort;
            var _emailFrom = email.SenderEmail;
            var _emailPassword = email.EmailPassword;

            var mensagem = new MimeMessage();
            mensagem.From.Add(new MailboxAddress("CobranÃ§a", _emailFrom));
            mensagem.To.Add(new MailboxAddress("", destinario));
            mensagem.Subject = assunto;
            mensagem.Body = new TextPart("html") { Text = corpo };

            var client = new SmtpClient();
            await client.ConnectAsync(_smtpoHost, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_emailFrom, _emailPassword);
            await client.SendAsync(mensagem);
            await client.DisconnectAsync(true);

            return true;
        }

        public async Task<EmailDTO> CreateEmail(EmailDTO email)
        {
            if (email == null) return null;

            var emailConfigExists = _db.EmailConfigs.Any(e => e.SmtpServer == email.SmtpServer);
            if (!emailConfigExists)
            {
                var emailConfig = new Models.EmailConfig
                {
                    SmtpServer = email.SmtpServer,
                    SmtpPort = email.SmtpPort,
                    SenderEmail = email.SenderEmail,
                    EmailPassword = email.EmailPassword
                };
                _db.EmailConfigs.Add(emailConfig);
                await _db.SaveChangesAsync();
            }

            return email;
        }

        public async Task<EmailDTO> GetEmailById(int id)
        {
            var emailConfig = await _db.EmailConfigs.FindAsync(id);
            if (emailConfig == null) return null;
            var emailDTO = new EmailDTO
            {
                Id = emailConfig.Id,
                SmtpServer = emailConfig.SmtpServer,
                SmtpPort = emailConfig.SmtpPort,
                SenderEmail = emailConfig.SenderEmail,
                EmailPassword = emailConfig.EmailPassword
            };
            return emailDTO;
        }

        public async Task<EmailDTO> EditEmail(int id, EmailDTO email)
        {
            var emailConfig = await _db.EmailConfigs.FindAsync(id);
            if (emailConfig == null) return null;
            if (email == null) return null;

            emailConfig.SmtpServer = email.SmtpServer;
            emailConfig.SmtpPort = email.SmtpPort;
            emailConfig.SenderEmail = email.SenderEmail;
            emailConfig.EmailPassword = email.EmailPassword;
            await _db.SaveChangesAsync();
            email.Id = id;
            return email;
        }
    }
}