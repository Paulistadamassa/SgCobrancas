namespace SgCobrancas.ApiService.DTOs
{
    public class EmailDTO
    {
        public int Id { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SenderEmail { get; set; }
        public string EmailPassword { get; set; }
    }
}
