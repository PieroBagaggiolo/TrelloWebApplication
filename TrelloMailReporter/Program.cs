using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using TrelloUtilities;
using TrelloWebApplication.Models;
using TrelloWebApplication.Utiliti;

//using Quartz;
//using Quartz.Impl;
//using Quartz.Logging;

namespace TrelloMailReporter
{
    public class Program
    {

        public static void SendEmail()
        {
            MailMessage Msg = new MailMessage();

            //Imposta il mittente
            Msg.From = new MailAddress("trelloreporterapp@hotmail.com", "Limi");


            //La proprietà .To è una collezione di destinatari,
            //quindi possiamo addizionare quanti destinatari vogliamo.
            Msg.To.Add(new MailAddress("pierobagaggiololavoro@gmail.com", "Piero prova"));

            //Imposto oggetto
            Msg.Subject = "Mail notifiche ";

            //Imposto contenuto

            Msg.Body = "Mail automatica di notifiche giornaliera";
            Msg.IsBodyHtml = true;

            

            //Imposto il Server Smtp
            SmtpClient Smtp = new SmtpClient("smtp.live.com", 25);

            //Possiamo impostare differenti metodi di spedizione.
            //Imposta consegna diretta.
            Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //Alcuni Server SMTP richiedono l'accesso autenticato
            var attachmentFilename = "C:/Users/sava/Download/Index.xlsx";
            //fuAttatchment.SaveAs("somewhere local" + fuAttatchment.FileName);
            //Msg.Attachments.Add(new System.Net.Mail.Attachment("C:/Users/sava/Download/" + fuAttatchment.FileName));
           
            //if (attachmentFilename != null)
            //{
            //    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(attachmentFilename, MediaTypeNames.Application.Octet);
            //    ContentDisposition disposition = attachment.ContentDisposition;
            //    disposition.CreationDate = File.GetCreationTime(attachmentFilename);
            //    disposition.ModificationDate = File.GetLastWriteTime(attachmentFilename);
            //    disposition.ReadDate = File.GetLastAccessTime(attachmentFilename);
            //    disposition.FileName = Path.GetFileName(attachmentFilename);
            //    disposition.Size = new FileInfo(attachmentFilename).Length;
            //    disposition.DispositionType = DispositionTypeNames.Attachment;
            //    Msg.Attachments.Add(attachment);
            //}
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