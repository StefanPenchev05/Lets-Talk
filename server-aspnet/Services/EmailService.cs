using System.Net;
using System.Net.Mail;
using Server.ViewModels;

namespace Server.Services
{
    // This class is responsible for sending emails.
    public class EmailManager : IEmailService
    {
        // Configuration object to access app settings.
        private readonly IConfiguration _configuration;
        // Service to render views to strings.
        private readonly IViewRenderService _viewRenderService;

        // Constructor that takes IConfiguration and IViewRenderService as parameters.
        public EmailManager(IConfiguration configuration, IViewRenderService viewRenderService){
            _configuration = configuration;
            _viewRenderService = viewRenderService;
        }

        // Method to send an email asynchronously.
        public async Task SendEmailAsync(string viewName, string _subject, LoginViewModel model,  Dictionary<string, object> additionalData = null){
            // Retrieve email settings from configuration.
            var fromEmail = _configuration["EmailSettings:Email"];
            var password = _configuration["EmailSettings:Password"];

            // Create a new SMTP client with the specified settings.
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, password),
                EnableSsl = true
            };

            // Render the specified view to a string.
            var body = await _viewRenderService.RenderToStringAsync($"{viewName}", model, additionalData);

            // Create a new mail message with the specified settings.
            var mailMessage = new MailMessage(fromEmail, model.UsernameOrEmail, _subject, body)
            {
                IsBodyHtml = true
            };

            // Add the recipient to the mail message.
            mailMessage.To.Add(model.UsernameOrEmail);

            // Send the mail message asynchronously.
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}