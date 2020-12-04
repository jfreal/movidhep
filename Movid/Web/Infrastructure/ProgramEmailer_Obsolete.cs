using Movid.App.Controllers;
using Movid.App.Models;
using Movid.Shared;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Movid.App.Infrastructure
{
    //public static class Emailer_Obselete
    //{
    //    public static void SendContent(string toEmail, string subject, string content)
    //    {
    //        try
    //        {
    //            //create a new message object
    //            var message = SendGrid.GetInstance();

    //            var to = new List<string>() { toEmail };
    //            const string from = "john@movidhep.com";

    //            //set the message recipients
    //            foreach (string recipient in to)
    //            {
    //                message.AddTo(recipient);
    //            }

    //            //set the sender
    //            message.From = new MailAddress(from);

    //            //set the message body
    //            message.Html = content;

    //            message.Subject = subject;

    //            var transportInstance = SendGridTransportFactory.Create();

    //            transportInstance.Deliver(message);
    //        }
    //        catch (CookieException)
    //        {
    //            //smtp fails at axis
    //        }
    //    }
    //}

    //public static class AccountEmailer
    //{
    //    public static void SendResetEmail(string email, string url)
    //    {
    //        try
    //        {
    //            //create a new message object
    //            var message = SendGrid.GetInstance();

    //            var to = new List<string>() { email };
    //            const string from = "reset@movidhep.com";

    //            //set the message recipients
    //            foreach (string recipient in to)
    //            {
    //                message.AddTo(recipient);
    //            }

    //            //set the sender
    //            message.From = new MailAddress(from);

    //            //set the message body
    //            message.Html = "<html><p>Hi " + email + "</p>" +
    //                "<p>Use the following link to reset your password:</p>" +
    //                "<p><a href=" + url + ">" + url + "</a></p></html>";

    //            message.Subject = "Reset your app.movidhep Password";

    //            var transportInstance = SendGridTransportFactory.Create();

    //            transportInstance.Deliver(message);
    //        }
    //        catch (CookieException)
    //        {
    //            //smtp fails at axis
    //        }
    //    }
    //}


    public class ProgramEmailer
    {

        private readonly IMovidAppContext _movidAppContext;
        public ProgramEmailer(IMovidAppContext movidAppContext)
        {
            _movidAppContext = movidAppContext;
        }

        public void SendRandomizedProgramToLoggedInUser()
        {

            //try
            //{
            //    var message = CreateEmailMessage(_movidAppContext.LoggedInUser.Email);

            //    //set the message body
            //    message.Html = "<html><p>Hi " + _movidAppContext.LoggedInUser.Email + "</p>" +
            //        "<p>" + _movidAppContext.Clinic.EmailMessage + "</p><p>Your new Program is available at: " +
            //                   "<a href=http://app.movidhep.com/program/sample/" + _movidAppContext.Clinic.Id + "/" + +_movidAppContext.LoggedInUser.Id +
            //                   ">http://app.movidhep.com/program/sample/" + _movidAppContext.Clinic.Id + "/" + _movidAppContext.LoggedInUser.Id +"</a></p></html>";


            //    var transportInstance = SendGridTransportFactory.Create();


            //    //send the mail
            //    transportInstance.Deliver(message);
            //}
            //catch (CookieException)
            //{
            //    //smtp fails at axis
            //}
        }

        public void SendWelcomeEmail()
        {
            
        }

        public void SendToPatient(int programId,  string toEmail, string shortUrl){

            //try
            //{
            //    var message = CreateEmailMessage(toEmail);

            //    //set the message body
            //    message.Html = "<html><p>Hi " + toEmail + "</p>" + 
            //        "<p>" + _movidAppContext.Clinic.EmailMessage + "</p><p>Your new Program is available at: <a href=http://app.movidhep.com/Program/" + shortUrl +
            //                   ">http://app.movidhep.com/program/" + shortUrl + "</a></p></html>";


            //    var transportInstance = SendGridTransportFactory.Create();

            //    AddProgramTracking(programId, message);

            //    //send the mail
            //    transportInstance.Deliver(message);
            //}
            //catch(CookieException)
            //{
            //    //smtp fails at axis
            //}
        }

        //private SendGrid CreateEmailMessage(string email)
        //{
        //    var message = SendGrid..GetInstance();
        //    message.EnableClickTracking();

        //    var to = new List<string>() { email };
        //    var from = GetFromAddress(_movidAppContext.Clinic, _movidAppContext.LoggedInUser);

        //    foreach (string recipient in to)
        //    {
        //        message.AddTo(recipient);
        //    }

        //    message.From = new MailAddress(@from);
        //    message.Subject = GenerateProgramEmailSubject(_movidAppContext.Clinic);
        //    return message;
        //}

        //private static void AddProgramTracking(int programId, SendGrid message)
        //{
        //    var trackingIdentifer = new Dictionary<string, string>();
        //    trackingIdentifer["programId"] = programId.ToString();

        //    message.AddUniqueArgs(trackingIdentifer);

        //    message.SetCategory("Program-sent");
        //}

        public string GenerateProgramEmailSubject(Clinic clinic)
        {
            if (string.IsNullOrWhiteSpace(clinic.Name))
            {
                return "Your new exercise plan.";
            }

            return string.Format("{0} has sent you a new home exercise program!", clinic.Name);
        }

        public static string GetFromAddress(Clinic clinic, User user)
        {
            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                return user.Email;
            }

            if (!string.IsNullOrWhiteSpace(clinic.Email))
            {
                return clinic.Email;
            }

            return "newprogram@movidhep.com";
        }

    }

    public static class SendGridTransportFactory
    {
        //public static SendGridMail.Web Create()
        //{
        //            string username = "azure_e8533bd24d0620a8861bfcdba75effc3@azure.com";
        //            string password = "3o0gtwnh";

        //        var transportInstance = SendGridMail.Web.GetInstance(new NetworkCredential(username, password));
        

        //    return transportInstance;
        //}
    }
    
}