using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IConfiguration config, ILogger<EmailSender> logger)
        {
            _config = config;
            _logger = logger;
        }


        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // logic to send email
            //return Task.CompletedTask;


            var emailConfig = _config.GetSection("EmailConfiguration");

            try
            {
                using var client = new SmtpClient(emailConfig["SmtpServer"], int.Parse(emailConfig["Port"]))
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(
                            emailConfig["Username"],
                            emailConfig["Password"]
                        )

                };

                var message = new MailMessage
                {
                    From = new MailAddress(emailConfig["From"],"Your Book Store"),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true
                };
                message.To.Add(email);

                await client.SendMailAsync(message);
                _logger.LogInformation($"Email sent to {email}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error sending email");
                throw;
            }
        }
    }
}
