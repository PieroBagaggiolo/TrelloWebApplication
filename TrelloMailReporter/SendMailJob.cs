using Quartz;
using Quartz.Impl;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TrelloMailReporter
{
    public class SendMailJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() => SendMail());
        }
        public void SendMail()
        {
            MailMessage Msg = new MailMessage();

            //Imposta il mittente
            Msg.From = new MailAddress("trelloreporterapp@hotmail.com", "Limi");


            //La proprietà .To è una collezione di destinatari,
            //quindi possiamo addizionare quanti destinatari vogliamo.
            Msg.To.Add(new MailAddress("nunzio22598@gmail.com", "Piero prova"));

            //Imposto oggetto
            Msg.Subject = "Mail notifiche TrelloReporterApp ";

            //Imposto contenuto
            Msg.Body = "Mail automatica di notifiche giornaliera";
            Msg.IsBodyHtml = true;


            //Imposto il Server Smtp
            SmtpClient Smtp = new SmtpClient("smtp.live.com", 25);

            //Possiamo impostare differenti metodi di spedizione.
            //Imposta consegna diretta.
            Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            //Alcuni Server SMTP richiedono l'accesso autenticato
            Smtp.UseDefaultCredentials = false;
            NetworkCredential Credential = new

            NetworkCredential("trelloreporterapp@hotmail.com", "Trello123");

            Smtp.Credentials = Credential;

            //Certificato SSL
            Smtp.EnableSsl = true;

            //Spediamo la mail
            Smtp.Send(Msg);
        }
    }
}