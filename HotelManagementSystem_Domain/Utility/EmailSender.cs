using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Domain.Utility
{
    public class EmailSender: IEmailSender
    {
        private EmailSetting _emailSetting { get; }
        public EmailSender(IOptions<EmailSetting> emailSetting)
        {
            _emailSetting = emailSetting.Value;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Execute(email, subject, htmlMessage).Wait();
            return Task.FromResult(0);
        }
        public async Task Execute(string Email, string Subject, string Message)
        {
            try
            {
                string toEmail = string.IsNullOrEmpty(Email) ?
                    _emailSetting.ToEmail : Email;
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSetting.UsernameEmail, "My Email Name"),
                };
                mail.To.Add(toEmail);
                mail.CC.Add(_emailSetting.CcEmail);
                mail.Subject = "Booking App:" + Subject;
                mail.Body = Message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                using (SmtpClient smtp = new SmtpClient
                    (_emailSetting.PrimaryDomain, _emailSetting.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSetting.UsernameEmail, _emailSetting.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }

        }

    }
}
