using System.Net;

namespace Movid.App.Infrastructure
{
    public class SendGridEmailer
    {
        public static void NewAccountOpened(string email)
        {

            try
            {
                ////create a new message object
                //var message = SendGrid.GetInstance();

                //var _to = new List<string>() { email };
                //var _from = "john@movidhep.com";


                //message.EnableClickTracking();

                ////set the message recipients
                //foreach (string recipient in _to)
                //{
                //    message.AddTo(recipient);
                //}

                ////set the sender
                //message.From = new MailAddress(_from);

                ////set the message body
                //message.Html = "<html><p>Hey " + email + "</p>" +
                //               @"<p>This is John from Movidhep.  Thanks for signing up!</p> 
                //<p>If you have any questions please feel free to connect with me by replying directly to 
                //this email (john@movidhep.com) or giving me a call at 8607782522.</p> 
                //<p>Use the following link to sign in to your account here:</p>" +
                //               "<a href=\"https://app.movidhep.com/account/login\">https://app.movidhep.com/account/login</a></p><p>Best,<br />-John</p></html>";

                ////set the message subject
                //message.Subject = "John from Movid HEP with your account details";

                //var username = "azure_e8533bd24d0620a8861bfcdba75effc3@azure.com";
                //var password = "3o0gtwnh";

                ////create an instance of the Web transport mechanism
                //var transportInstance =
                //    SendGridMail.Web.GetInstance(new NetworkCredential(username, password));

                //var trackingIdentifer = new Dictionary<string, string>();
                //trackingIdentifer["emailAccount"] = email.ToString();

                //message.AddUniqueArgs(trackingIdentifer);

                //message.SetCategory("account-registered");

                ////send the mail
                //transportInstance.DeliverAsync(message);
            }
            catch (CookieException)
            {
                //smtp fails at axis
            }
        }

        public static void CompanyNewUserNotification(string email)
        {

            try
            {
            //    //create a new message object
            //    var message = SendGrid.GetInstance();

            //    var to = new List<string>() { "john@movidhep.com", "ray@movidhep.com", "mike@movidhep.com" };
            //    const string from = "noreply@movidhep.com";


            //    //set the message recipients
            //    foreach (string recipient in to)
            //    {
            //        message.AddTo(recipient);
            //    }

            //    //set the sender
            //    message.From = new MailAddress(from);
            //    var whoSignedUp = string.Format("{0} just signed up for a trial!", email);
            //    message.Subject = whoSignedUp;
            //    //set the message body
            //    message.Html = "<html><p>Wahoo!</p>" +
            //                   @"<p>"
            //                   + whoSignedUp +
            //                   "</p>";
                              

            //var username = "azure_e8533bd24d0620a8861bfcdba75effc3@azure.com";
            //    var password = "3o0gtwnh";

            //    //create an instance of the Web transport mechanism
            //    var transportInstance =
            //        SendGridMail.Web.GetInstance(new NetworkCredential(username, password));
                
            //    transportInstance.Deliver(message);
            }
            catch (CookieException)
            {
                //smtp fails at axis
            }
        }
    }
}