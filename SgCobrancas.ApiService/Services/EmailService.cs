using MimeKit;
using MailKit.Net.Smtp;

namespace SgCobrancas.ApiService.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpoHost = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _emailFrom = "";
        private readonly string _emailPassword = "";

        public async Task<bool> EnviarMensagemAsync(string destinario, string assunto, string corpo)
        {
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
