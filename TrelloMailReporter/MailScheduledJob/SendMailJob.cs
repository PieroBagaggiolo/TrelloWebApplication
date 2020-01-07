using OfficeOpenXml;
using Quartz;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using TrelloUtilities;
using TrelloWebApplication.Models;
using TrelloWebApplication.Utiliti;

namespace TrelloMailReporter.MailScheduledJob
{
    public class SendMailJob : IJob
    {

        public delegate void Del(ref ExcelPackage fileEx);
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() => SendMail());
        }
        /// <summary>
        /// metodo per l'invio di una mail con in allegato il file excel
        /// </summary>
        public void SendMail()
        {
            //creazione allegato excel prima di inviare la mail
            Del del = ReportMethods.DelegateMethod;
            ExcelPackage ex = new ExcelPackage();
            del(ref ex);
            using (var memoryStream = new MemoryStream())
            {
                ex.SaveAs(memoryStream);
                memoryStream.Position = 0;
                //Crea oggetto di tipo MailMessage
                MailMessage Msg = new MailMessage();
                
                //Imposta il mittente
                Msg.From = new MailAddress("trelloreporterapp@hotmail.com", "Limi");

                //La proprietà .To è una collezione di destinatari,
                //quindi possiamo addizionare quanti destinatari vogliamo.

                Msg.To.Add(new MailAddress("myslim.derjaj@euris.it", "Nunzio prova"));
                Msg.To.Add(new MailAddress("pierobagaggiololavoro@gmail.com", "Piero prova"));


                //Imposto oggetto
                Msg.Subject = "Mail notifiche TrelloReporterApp ";

                //Imposto contenuto
                Msg.Body = "Mail automatica di notifiche giornaliera";
                Msg.IsBodyHtml = true;

                //Creo l'allegato e lo passo alla mail
                var attachment = new System.Net.Mail.Attachment(memoryStream, "Report.xlsx");
                attachment.ContentType = new ContentType("application/vnd.ms-excel");
                Msg.Attachments.Add(attachment);

                //Imposto il Server Smtp
                SmtpClient Smtp = new SmtpClient("smtp.live.com", 25);

                //Possiamo impostare differenti metodi di spedizione.
                //Imposta consegna diretta.
                Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                //Alcuni Server SMTP richiedono l'accesso autenticato
                Smtp.UseDefaultCredentials = false;
                NetworkCredential Credential = new NetworkCredential("trelloreporterapp@hotmail.com", "Trello123");
                Smtp.Credentials = Credential;

                //Certificato SSL
                Smtp.EnableSsl = true;

                //Spediamo la mail
                Smtp.Send(Msg);                                     
            }
        }
    }
}