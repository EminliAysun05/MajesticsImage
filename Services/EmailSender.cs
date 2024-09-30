using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;

namespace MajesticAdminPanelTask.Services
{
    public class EmailSender
    {
        private readonly IConfiguration _config;
        //private readonly string _smtpHost;
        //private readonly int _smtpPort;
        //private readonly bool _enableSsl;
        //private readonly string _smtpUser;
        //private readonly string _smtpPass;

        public EmailSender( IConfiguration config)
        {
  
            _config = config;
        }

        public void SendEmail(string toEmail, string subject, string body)
        {
           

            MailMessage message = new();
            message.From = new MailAddress(_config.GetSection("Smtp:From").Value);
            //string body = File.ReadAllText("wwwroot/Templates/VerifyEmail.html");
            //body = body;
            message.IsBodyHtml = true;
            message.Body = body;
            message.Subject = subject;
            message.To.Add(toEmail);


            SmtpClient smtpClient = new();
            smtpClient.Port = Convert.ToInt32(_config.GetSection("Smtp:Port").Value);
            smtpClient.Host = _config.GetSection("Smtp:Host").Value;
            smtpClient.EnableSsl = true;

            smtpClient.Credentials = new NetworkCredential(_config.GetSection("Smtp:From").Value, _config.GetSection("Smtp:Password").Value);
              smtpClient.Send(message);



            //if (string.IsNullOrEmpty(toEmail))
            //{
            //    throw new ArgumentNullException(nameof(toEmail), "Email address cannot be null or empty.");
            //}

            //using (var client = new SmtpClient(_config.GetSection("Smtp:Host").Value,  Convert.ToInt32(_config.GetSection("Smtp:Port").Value))  )
            //{
            //    client.Credentials = new NetworkCredential(_config.GetSection("Smtp:Username").Value, _config.GetSection("Smtp:Password").Value);
            //    client.EnableSsl = Convert.ToBoolean(  _config.GetSection("Smtp:EnableSSL").Value);
            //    var check = _config.GetSection("Smtp:Username").Value;
            //    var mailMessage = new MailMessage
            //    {
            //        From = new MailAddress(_config.GetSection("Smtp:Username").Value),
            //        Subject = subject,
            //        Body = body,
            //        IsBodyHtml = true
            //    };

            //    mailMessage.To.Add(toEmail);

            //      client.Send(mailMessage);
        }
        }

    }

