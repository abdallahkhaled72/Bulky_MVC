using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailRepository> _logger;

        public EmailRepository(IConfiguration configuration, ILogger<EmailRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailConfig = _configuration.GetSection("EmailConfiguration");

            // Add these debug lines to check what values are being read
            _logger.LogInformation($"SMTP Server: {emailConfig["SmtpServer"]}");
            _logger.LogInformation($"Port: {emailConfig["Port"]}");

            using (var client = new SmtpClient())
            {
                client.Host = emailConfig["SmtpServer"];
                client.Port = int.Parse(emailConfig["Port"]);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(
                        emailConfig["Username"],
                        emailConfig["Password"]
                    );

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(emailConfig["From"]),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
            }

        }

        public async Task SendPasswordResetEmailAsync(string email, string callBackUrl)
        {



        

            var subject = "Reset your password";
            var message = $"Please reset your password by <a href='{callBackUrl}'> clicking here </a>.";

            await SendEmailAsync(email, subject, message);
        }
    }
}
