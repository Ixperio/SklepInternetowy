using Sklep.Db_Context;
using Sklep.Models;
using Sklep.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Sklep.Observer
{
    //Obserwator zmiany zamówienia , wysy³aj¹cy powiadomienia na adres email - Katarzyna Grygo
    public class EmailNotification : IObserver
    {
        private MyDbContext _db;

        private readonly SmtpClient smtpClient;
        public EmailNotification()
        {
            this._db = MyDbContext.GetInstance();
            smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = false;
            smtpClient.Host = "arturleszczak.pl";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("test@arturleszczak.pl", "mLg1Wr8gSGmx");
        }

        public void Update(IOrderObserver order)
        {
            ZamowieniaKlienci zamowienia = order as ZamowieniaKlienci;
            Adress adress = _db.Adress.FirstOrDefault(x => x.AdressId == zamowienia.adresId);
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("test@arturleszczak.pl");
            mailMessage.To.Add(adress.Email);
            mailMessage.Subject = "Zmiana statusu zamówienia";
            mailMessage.Body = $"Twoje zamówienie zmieni³o status na {zamowienia.status}";

            smtpClient.Send(mailMessage);
            var x = 10;
        }

        public void InfoZeStrony(ContactFormView cfv)
        {
    
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("test@arturleszczak.pl");
            mailMessage.To.Add("kontakt@arturleszczak.pl");
            mailMessage.Subject = "Otrzymano zapytanie poprzez formularz na stronie!";
            mailMessage.Body = $"U¿ytkownik - {cfv.name} - {cfv.email} | Napsia³ poprzez formularz na stronie kontaktowej : ' {cfv.message} '";

            smtpClient.Send(mailMessage);
       
        }

        public void InfoDoExperta(ContactFormExpertView cfv)
        {

            var produkt = _db.Products.FirstOrDefault(p => p.ProduktId == cfv.produktId && p.isVisible == true && p.isDeleted == false);
            if(produkt != null)
            {
                string expertEmail = _db.Person.SingleOrDefault(per => per.PersonId == produkt.adderId).Email;
                if(expertEmail == null)
                {
                    expertEmail = "kontakt@arturleszczak.pl";
                }

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("test@arturleszczak.pl");
                mailMessage.To.Add(expertEmail);
                mailMessage.Subject = "Otrzymano zapytanie odnoœnie produktu!";
                mailMessage.Body = $"U¿ytkownik - {cfv.name} - {cfv.email} | Wys³a³ zapytanie odnoœnie produktu ( {produkt.Nazwa} | ID : {produkt.ProduktId}) - Treœæ pytania : ' {cfv.message} '";

                smtpClient.Send(mailMessage);
            }
        }

    }
}
