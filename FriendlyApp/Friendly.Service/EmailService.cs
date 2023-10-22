using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Friendly.Service
{
    public class EmailService : IEmailService
    {
        private IConfiguration _configuration;
        private ILogger<EmailService> _logger;
        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            try
            {
                var apiKey = _configuration["SendGridAPIKey"];
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("friendlyteam@engineer.com", "Friendly App Team");
                var to = new EmailAddress(toEmail);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    _logger.LogInformation("Email sent successfully to: {Email}, Subject: {Subject}", toEmail, subject);
                }
                else
                {
                    _logger.LogError("Failed to send email to: {Email}, Subject: {Subject}. Error: {Error}", toEmail, subject, response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while sending email to: {Email}, Subject: {Subject}", toEmail, subject);
            }
        }
    }
}
