using SendGridMail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Movid.App.Infrastructure.Emails
{
    public static class SendGridEmailer
    {
        public static void Send(string from, List<string> toEmails, string subject, string body, Dictionary<string, string> trackingIdentifers, string category, bool isEnableClickTracking)
        {
            //try
            //{
            //    var message = CreateEmailMessage(from, toEmails, subject, body);

            //    if (trackingIdentifers != null) message.AddUniqueArgs(trackingIdentifers);
            //    if (!string.IsNullOrEmpty(category)) message.SetCategory(category);
            //    if (isEnableClickTracking) message.EnableClickTracking();

            //    var transportInstance = GetSendGridInstance();
            //    transportInstance.Deliver(message);
            //}
            //catch (Exception)
            //{                
                
            //}
        }

        //private static SendGrid CreateEmailMessage(string from, List<string> toEmails, string subject, string body)
        //{
        //    var message = SendGrid.GetInstance();
        //    message.AddTo(toEmails);
        //    message.From = new MailAddress(from);
        //    message.Subject = subject;
        //    message.Html = body;
        //    return message;
        //}

        //private static SendGridMail.Web GetSendGridInstance()
        //{
        //    var username = ConfigurationManager.AppSettings["SendEmailUserName"];
        //    var password = ConfigurationManager.AppSettings["SendEmailPassWord"];
        //    return SendGridMail.Web.GetInstance(new NetworkCredential(username, password));
        //}
    }
}