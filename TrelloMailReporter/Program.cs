using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Mail;
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
        // elementi neccessari per fare le chiamate in caso di neccessita di potrebbe fare una view che le chieda al utente
        static string Key = "9936fabac5fdc5f00e46ff3a454e9feb";
        static string Token = "27f3bbdeb9724521082f710e5dafbb9cfb56b315d90b2a27d502a6a391abad01";
        static string IdBoard = "5ddd5dad735c842669b7b819";
        // creazione del mio modello di api per le chiamate
        static Api myApi = new Api(Key, Token, IdBoard);
        //creazione del modello di liste di card
        List<Card> model = PopolateModel.Popola(myApi);

        public static void SendEmail()
        {
            ExcelPackage ex = ReportMethods.ExportExcelTotal(myApi);
            CreazioneExl.CreazioneFile(ex, "Index");

            MailMessage Msg = new MailMessage();

            //Imposta il mittente
            Msg.From = new MailAddress("limi1998@live.it", "Limi");

            //La proprietà .To è una collezione di destinatari,
            //quindi possiamo addizionare quanti destinatari vogliamo.
            Msg.To.Add(new MailAddress("pierobagaggiololavoro@gmail.com", "Limi Prova"));

            //Imposto oggetto
            Msg.Subject = "Inviare Mail con C#";

            //Imposto contenuto
            Msg.Body = "Mail inviata";
            Msg.IsBodyHtml = true;

            //aggiunta allegato alla mail
            Msg.Attachments.Add(new System.Net.Mail.Attachment( "Index"));

            //Imposto il Server Smtp
            SmtpClient Smtp = new SmtpClient("smtp.live.com", 25);

            //Possiamo impostare differenti metodi di spedizione.
            //Imposta consegna diretta.
            Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            //Alcuni Server SMTP richiedono l'accesso autenticato
            Smtp.UseDefaultCredentials = false;
            NetworkCredential Credential = new
            NetworkCredential("limi1998@live.it", "VeroInterista");
            Smtp.Credentials = Credential;

            //Certificato SSL
            Smtp.EnableSsl = true;

            //Spediamo la mail
            Smtp.Send(Msg);
        }
    }
}