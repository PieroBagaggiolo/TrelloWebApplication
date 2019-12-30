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
        // elementi neccessari per fare le chiamate in caso di neccessita di potrebbe fare una view che le chieda al utente
        static string Key = "9936fabac5fdc5f00e46ff3a454e9feb";
        static string Token = "27f3bbdeb9724521082f710e5dafbb9cfb56b315d90b2a27d502a6a391abad01";
        static string IdBoard = "5ddd5dad735c842669b7b819";
        // creazione del mio modello di api per le chiamate
        static Api myApi = new Api(Key, Token, IdBoard);
        //creazione del modello di liste di card
        List<Card> model = PopolateModel.Popola(myApi);

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
            ExcelPackage ex = ReportMethods.ExportExcelTotal(model);
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
                Msg.To.Add(new MailAddress("nunzio22598@gmail.com", "Nunzio prova"));
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

                //Msg.Attachments.Add(new System.Net.Mail.Attachment(memoryStream, "EmailXml.xlsx"));

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
