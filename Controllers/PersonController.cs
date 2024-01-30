using System;
using System.Linq;
using System.Web.Mvc;
using Sklep.Models.ModelViews;
using Sklep.Models;
using Sklep.Db_Context;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace Sklep.Controllers
{
    public class PersonController : Controller
    {
        private readonly MyDbContext _db;

        public PersonController()
        {
            this._db = MyDbContext.GetInstance();
        }

        public ActionResult Index()
        {
            if (Request.Cookies["Koszyk"] != null)
            {
                HttpCookie existingCookie = Request.Cookies["Koszyk"];
                string cookieValue = existingCookie.Value;
                ViewBag.WartoscKoszyka = cookieValue;
            }
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (Request.Cookies["Koszyk"] != null)
            {
                HttpCookie existingCookie = Request.Cookies["Koszyk"];
                string cookieValue = existingCookie.Value;
                ViewBag.WartoscKoszyka = cookieValue;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Register(PersonRegistration personRegistered)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Utworzono nowe konto!";
                Person person = new Person()
                {
                    Email = personRegistered.Email,
                    Phone = personRegistered.PhoneNumber,
                    Name = personRegistered.FirstName,
                    Surname = personRegistered.LastName,
                    Birthday = personRegistered.BirthDate,
                    LogowanieId = 0,
                    AccountTypeId = 0
                };

                person.Logowanie = new Logowanie
                {
                    Login = personRegistered.Login,
                    Password = personRegistered.Password
                };

                string resetToken = Guid.NewGuid().ToString();
                person.ResetToken = resetToken;

                _db.Logowanie.Add(person.Logowanie);
                _db.Person.Add(person);
                _db.SaveChanges();
            }
            else
            {
                ViewBag.Message = "Wprowadzono nieprawidłowe dane!";
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            int? userId = Session["UserId"] as int?;
            if (!userId.HasValue)
            {
                if (Request.Cookies["KoszykWartosc"] != null)
                {
                    HttpCookie existingCookie = Request.Cookies["KoszykWartosc"];
                    string cookieValue = existingCookie.Value;
                    ViewBag.WartoscKoszyka = cookieValue;
                }

                return View();
            }
            else
            {
                return View("Account");
            }
        }

        [HttpPost]
        public ActionResult Login(PersonLogin personLogged)
        {
            if (Request.Cookies["KoszykWartosc"] != null)
            {
                HttpCookie existingCookie = Request.Cookies["KoszykWartosc"];
                string cookieValue = existingCookie.Value;
                ViewBag.WartoscKoszyka = cookieValue;
            }
            if (ModelState.IsValid)
            {
                int d = _db.Logowanie.SingleOrDefault(d => d.Login == personLogged.Login && d.Password == personLogged.Password).LogowanieId;
                var c = _db.Person.SingleOrDefault(p => p.LogowanieId == d);

                if (c != null)
                {
                    Session["UserId"] = c.PersonId;
                    ViewBag.Person = c;
                    return View("Account");
                }
                else
                {
                    return View("Login");
                }
            }

            return View();
        }

        public ActionResult AccountEdit()
        {
            int? userId = Session["UserId"] as int?;
            if (userId.HasValue)
            {
                Person person = _db.Person.SingleOrDefault(p => p.PersonId == userId);
                ViewBag.Person = person;
                return View();
            }
            else
            {
                return View("Login");
            }
        }

        public ActionResult Account()
        {
            int? userId = Session["UserId"] as int?;
            if (userId.HasValue)
            {
                Person person = _db.Person.SingleOrDefault(p => p.PersonId == userId.Value);
                if (person != null)
                {
                    return View(person);
                }
                else
                {
                    return View("Login");
                }
            }
            else
            {
                return View("Login");
            }
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            var user = _db.Person.SingleOrDefault(u => u.Email == email);

            if (user != null)
            {
                string resetToken = Guid.NewGuid().ToString();
                user.ResetToken = resetToken;
                _db.SaveChanges();

                SendPasswordResetEmail(user.Email, resetToken);

                ViewBag.Message = "E-mail z linkiem do resetowania hasła został wysłany.";
            }
            else
            {
                ViewBag.ErrorMessage = "Podany adres e-mail nie istnieje w naszym systemie.";
            }

            return View();
        }

        private void SendPasswordResetEmail(string userEmail, string resetToken)
        {
            var callbackUrl = Url.Action("ResetPassword", "Person", new { email = userEmail, token = resetToken }, protocol: Request.Url.Scheme);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("expensestracker4@gmail.com");
            mailMessage.To.Add(userEmail);
            mailMessage.Subject = "Reset Password";
            mailMessage.Body = $"Aby zresetować hasło, kliknij <a href='{callbackUrl}'>tutaj</a>.";

            mailMessage.IsBodyHtml = true;

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                smtpClient.Host = "smtp-relay.brevo.com";
                smtpClient.Port = 587;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("expensestracker4@gmail.com", "1kdBMJPFTqmEYwVR");

                smtpClient.Send(mailMessage);
            }
        }

        [HttpGet]
        public ActionResult ResetPassword(string email, string token)
        {
            var model = new PasswordResetModel
            {
                Email = email,
                Token = token
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult ResetPassword(PasswordResetModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _db.Person.SingleOrDefault(u => u.Email == model.Email && u.ResetToken == model.Token);

                if (user != null)
                {
                    user.ResetToken = null;
                    user.Password = model.NewPassword;

                    var logowanie = _db.Logowanie.SingleOrDefault(l => l.LogowanieId == user.LogowanieId);
                    if (logowanie != null)
                    {
                        logowanie.Password = model.NewPassword;
                    }

                    _db.SaveChanges();

                    ViewBag.ResetSuccess = true;
                    return RedirectToAction("Login", "Person");
                }
            }

            return View(model);
        }
    }
}
