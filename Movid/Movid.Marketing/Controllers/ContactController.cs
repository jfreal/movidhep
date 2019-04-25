using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;

using System.Web.Mvc;
using Movid.Web.Infrastructure;
//using SendGridMail;

namespace Movid.Marketing.Controllers
{
    public class ContactUsModel
    {
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }

    public class ContactController : Controller
    {
        [Route("contact")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("contact/send")]
        public ActionResult Index(ContactUsModel postedModel)
        {
            if (!ModelState.IsValid)
                return View(postedModel);

            SendEmail(postedModel.Name, postedModel.Email, postedModel.Message);

            this.HighFive("Message Sent");

            return RedirectToAction("Index");
        }

        public void SendEmail(string name, string from, string message)
        {
            ////create a new message object
            //var email = SendGrid.GetInstance();

            //var _to = new List<string>() { "john@movidhep.com" };
            
            ////set the message recipients
            //foreach (string recipient in _to)
            //{
            //    email.AddTo(recipient);
            //}

            ////set the sender
            //email.From = new MailAddress(from);

            ////set the message body
            //email.Html = message;

            ////set the message subject
            //email.Subject = "New Contact Us Message";

            //var username = "azure_e8533bd24d0620a8861bfcdba75effc3@azure.com";
            //var password = "3o0gtwnh";

            //var cred = new NetworkCredential(username, password);
            //SendGrid myMessage = SendGrid.GetInstance();
            
            //var credentials = new NetworkCredential("username", "password");

            //    // Create an Web transport for sending email.
            //    var transportWeb = SendGridMail.Web.GetInstance(credentials);

            //// Send the email.
            //transportWeb.DeliverAsync(myMessage);
        }
    }
}
