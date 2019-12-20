
//﻿using OfficeOpenXml;
//using Quartz;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Mail;
//using System.Threading.Tasks;
//using TrelloUtilities;
//using TrelloWebApplication.Models;
//using TrelloWebApplication.Utiliti;


//namespace TrelloMailReporter
//{
//    public class SendMailJob : IJob
//    {
//        public Task Execute(IJobExecutionContext context)
//        {           
           
////        //    MailMessage Msg = new MailMessage();

////        //    //Imposta il mittente
////        //    Msg.From = new MailAddress("trelloreporterapp@hotmail.com", "Trello Server");

////        //    //La proprietà .To è una collezione di destinatari,
////        //    //quindi possiamo addizionare quanti destinatari vogliamo.
////        //    Msg.To.Add(new MailAddress("pierobagaggiololavoro@gmail.com", "Limi Prova"));

////        //    //Imposto oggetto
////        //    Msg.Subject = "Inviare Mail con C#";

////        //    //Imposto contenuto
////        //    Msg.Body = "Mail sended succesfully";
////        //    Msg.IsBodyHtml = true;

////        //    //Imposto il Server Smtp
////        //    SmtpClient Smtp = new SmtpClient("smtp.live.com", 25);

////        //    //Possiamo impostare differenti metodi di spedizione.
////        //    //Imposta consegna diretta.
////        //    Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

////        //    //Alcuni Server SMTP richiedono l'accesso autenticato
////        //    Smtp.UseDefaultCredentials = false;
////        //    NetworkCredential Credential = new
////        //    NetworkCredential("trelloreporterapp@hotmail.com", "Trello123");
////        //    Smtp.Credentials = Credential;

////        //    //Certificato SSL
////        //    Smtp.EnableSsl = true;

////        //    //Spediamo la mail
////        //    Smtp.Send(Msg);


////        //    // define the job and tie it to our SendMailJob class
////        //    IJobDetail job = JobBuilder.Create<SendMailJob>()
////        //        .WithIdentity("job1", "group1")
////        //        .Build();

////        //    // Trigger the job to run now, and then repeat every 24 hours
////        //    ITrigger trigger = TriggerBuilder.Create()
////        //        .WithIdentity("trigger1", "group1")
////        //        .StartNow()
////        //        .WithSimpleSchedule(x => x
////        //            .WithIntervalInHours(24)
////        //            .RepeatForever())
////        //        .Build();
////        //}

//    }
//}
