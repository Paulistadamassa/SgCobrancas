using MimeKit;
using MailKit.Net.Smtp;

namespace SgCobrancas.ApiService.Services
{
    public class EmailService : IEmailService
    {
        private string _smtpoHost = "";
        private int _smtpPort = 0;
        private string _emailFrom = "";
        private string _emailPassword = "";

        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> EnviarMensagemAsync(string destinario, string assunto, string corpo)
        {
            _smtpoHost = _configuration["EmailSettings:SmtpServer"]!;
            _smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]!);
            _emailFrom = _configuration["EmailSettings:SenderEmail"]!;
            _emailPassword = _configuration["EmailSettings:Password"]!;
            var mensagem = new MimeMessage();
            mensagem.From.Add(new MailboxAddress("Cobrança", _emailFrom));
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
    }
}
