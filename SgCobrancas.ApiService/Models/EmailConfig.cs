namespace SgCobrancas.ApiService.Models
{
    public class EmailConfig
    {
        public int Id { get; set; }
        //public int UserId { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SenderEmail { get; set; }
        public string EmailPassword { get; set; }
    }
}
