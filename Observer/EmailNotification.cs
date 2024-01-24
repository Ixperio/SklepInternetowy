using Sklep.Db_Context;
using Sklep.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace Sklep.Observer
{
    public class EmailNotification : IObserver
    {
        private MyDbContext _db;

        private readonly SmtpClient smtpClient;
        public EmailNotification()
        {
            this._db = MyDbContext.GetInstance();
            smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            smtpClient.Host = "smtp-relay.brevo.com";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("expensestracker4@gmail.com", "1kdBMJPFTqmEYwVR");
        }

        public void Update(IOrderObserver order)
        {
            Zamowienia zamowienia = order as Zamowienia;
            Adress adress = _db.Adress.FirstOrDefault(x => x.AdressId == zamowienia.adresId);
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("expensestracker4@gmail.com");
            mailMessage.To.Add(adress.Email);
            mailMessage.Subject = "Zmiana statusu zamówienia";
            mailMessage.Body = $"Twoje zamówienie zmieni³o status na {zamowienia.status}";

            smtpClient.Send(mailMessage);
            var x = 10;
        }
    }
}
