using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ZimFund.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        // Injeta a configuração do appsettings.json via injeção de dependência
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Método assíncrono que envia um e-mail
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            // Cria o cliente SMTP com o servidor do Gmail
            var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpServer"])
            {
                // Define a porta
                Port = int.Parse(_configuration["EmailSettings:SmtpPort"]),

                // Autentica com o e-mail e a senha do app do gmail
                Credentials = new NetworkCredential(
                    _configuration["EmailSettings:SenderEmail"],
                    _configuration["EmailSettings:SenderPassword"]
                ),

                // Habilita criptografia SSL
                EnableSsl = true
            };

            // Monta o e-mail que será enviado
            var mailMessage = new MailMessage
            {
                // Endereço do remetente
                From = new MailAddress(_configuration["EmailSettings:FromEmail"]),

                // Assunto do e-mail
                Subject = subject,
                
                // Conteúdo do corpo 
                Body = body,

                // Informa que o corpo pode conter HTML
                IsBodyHtml = true
            };

            // Adiciona o destinatário
            mailMessage.To.Add(toEmail);

            // Envia o e-mail de forma assíncrona
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
