using Movid.App.Controllers;
using Movid.App.Models;
using Movid.Shared;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Movid.App.Infrastructure.Emails
{
    public class Emailer
    {
        private readonly IMovidAppContext _movidAppContext;
        public Emailer(IMovidAppContext movidAppContext)
        {
            _movidAppContext = movidAppContext;
        }

        public void SendEmail(EmailEnum emailEnum, string toEmail, string otherContent, Int32 programId)
        {
            try
            {
                switch (emailEnum)
                {
                    case EmailEnum.NewAccountOpen:
                        NewAccountOpened(toEmail);
                        break;
                    case EmailEnum.CompanyNewUserNotification:
                        CompanyNewUserNotification(toEmail);
                        break;
                    case EmailEnum.SendInvitation:
                        SendInvitation(toEmail, otherContent);
                        break;
                    case EmailEnum.SendResetEmail:
                        SendResetEmail(toEmail, otherContent);
                        break;
                    case EmailEnum.SendRandomizedProgramToLoggedInUser:
                        SendRandomizedProgramToLoggedInUser();
                        break;
                    case EmailEnum.SendToPatient:
                        SendToPatient(toEmail, programId, otherContent);
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void NewAccountOpened(string email)
        {
            try
            {
                var subject = "John from Movid HEP with your account details";
                string url = "https://app.movidhep.com/account/login";

                var body = new StringBuilder();
                body.AppendFormat("<html><p>Hey {0}</p>", email);
                body.Append(@"<p>This is John from Movidhep. Thanks for signing up!</p>");
                body.Append("<p>If you have any questions please feel free to connect with me by replying directly to this email (john@movidhep.com) or giving me a call at 8607782522.</p>");
                body.Append("<p>Use the following link to sign in to your account here:</p>");
                body.AppendFormat("<a href='{0}'> {0} </a></p><p>Best,<br />-John</p></html>", url);

                var trackingIdentifers = new Dictionary<string, string>();
                trackingIdentifers["emailAccount"] = email.ToString();

                SendGridEmailer.Send("john@movidhep.com", new List<string> { email }, subject, body.ToString(), trackingIdentifers, "account-registered", true);
            }
            catch (CookieException) { }
        }

        private void CompanyNewUserNotification(string email)
        {
            try
            {
                var subject = string.Format("{0} just signed up for a trial!", email);
                var toEmails = new List<string>() { "john@movidhep.com", "ray@movidhep.com", "mike@movidhep.com" };

                var body = new StringBuilder();
                body.Append("<html><p>Wahoo!</p>");
                body.AppendFormat("<p>{0}</p></html>", subject);

                SendGridEmailer.Send("noreply@movidhep.com", toEmails, subject, body.ToString(), null, null, false);
            }
            catch (CookieException) { }
        }

        private void SendInvitation(string email, string body)
        {
            try
            {
                SendGridEmailer.Send("john@movidhep.com", new List<string> { email }, "You've been invited to Movid HEP", body, null, null, false);
            }

            catch (CookieException) { }
        }

        private void SendResetEmail(string email, string url)
        {
            try
            {
                var subject = "Reset your app.movidhep Password";
                var body = new StringBuilder();
                body.AppendFormat("<html><p>Hi {0}</p>", email);
                body.Append("<p>Use the following link to reset your password:</p>");
                body.AppendFormat("<p><a href='{0}'> {0} </a></p></html>", url);

                SendGridEmailer.Send("reset@movidhep.com", new List<string> { email }, subject, body.ToString(), null, null, false);
            }
            catch (CookieException) { }
        }

        private void SendRandomizedProgramToLoggedInUser()
        {
            try
            {
                var from = GetFromAddress(_movidAppContext.Clinic, _movidAppContext.LoggedInUser);
                var subject = GenerateProgramEmailSubject(_movidAppContext.Clinic);
                var url = string.Format("http://app.movidhep.com/program/sample/{0}/{1}", _movidAppContext.Clinic.Id, _movidAppContext.LoggedInUser.Id);

                var body = new StringBuilder();
                body.AppendFormat("<html><p>Hi {0}</p>", _movidAppContext.LoggedInUser.Email);
                body.AppendFormat("<p>{0}</p>", _movidAppContext.Clinic.EmailMessage);
                body.Append("<p>Your new Program is available at:");
                body.AppendFormat("<a href='{0}'> {0} </a></p></html> ", url);

                SendGridEmailer.Send(from, new List<string> { _movidAppContext.LoggedInUser.Email }, subject, body.ToString(), null, null, true);
            }
            catch (CookieException) { }
        }

        private void SendToPatient(string email, int programId, string shortUrl)
        {
            try
            {
                var from = GetFromAddress(_movidAppContext.Clinic, _movidAppContext.LoggedInUser);
                var subject = GenerateProgramEmailSubject(_movidAppContext.Clinic);
                var url = string.Format("http://app.movidhep.com/Program/{0}", shortUrl);

                var body = new StringBuilder();
                body.AppendFormat("<html><p>Hi {0}</p>", email);
                body.AppendFormat("<p> {0}</p>", _movidAppContext.Clinic.EmailMessage);
                body.Append("<p>Your new Program is available at:");
                body.AppendFormat("<a href='{0}'> {0} </a></p></html> ", url);
                
                var trackingIdentifers = new Dictionary<string, string>();                
                trackingIdentifers["programId"] = programId.ToString();

                SendGridEmailer.Send(from, new List<string> { email }, subject, body.ToString(), trackingIdentifers, "Program-sent", true);
            }
            catch (CookieException) { }
        }

        private string GenerateProgramEmailSubject(Clinic clinic)
        {
            return string.IsNullOrWhiteSpace(clinic.Name) ? "Your new exercise plan." : string.Format("{0} has sent you a new home exercise program!", clinic.Name);
        }

        private static string GetFromAddress(Clinic clinic, User user)
        {
            if (user != null && !string.IsNullOrWhiteSpace(user.Email)) return user.Email;

            if (!string.IsNullOrWhiteSpace(clinic.Email)) return clinic.Email;

            return "newprogram@movidhep.com";
        }
    }
}