using System.Net;
using System.Net.Mail;
using Server.Models.ViewModels;

namespace Server.Services
{
    public class EmailManager
    {
        private readonly IConfiguration _configuration;
        private readonly IViewRenderService _viewRenderService;


        public EmailManager(IConfiguration configuration, IViewRenderService viewRenderService){
            _configuration = configuration;
            _viewRenderService = viewRenderService;
        }

        public async Task SendEmailAsync(string viewName,string _subject,LoginViewModel model,  Dictionary<string, object> additionalData = null){
            var fromEmail = _configuration["EmailSettings:Email"];
            var password = _configuration["EmailSettings:Password"];

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, password),
                EnableSsl = true
            };

            var body = await _viewRenderService.RenderToStringAsync($"{viewName}", model, additionalData);

            var mailMessage = new MailMessage(fromEmail, model.UsernameOrEmail, _subject, body)
            {
                IsBodyHtml = true
            };

            mailMessage.To.Add(model.UsernameOrEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}