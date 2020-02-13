using OfficeOpenXml;
using Quartz;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using TrelloUtilities;
using TrelloUtilities.Models;
using TrelloUtilities.Utility;
using TrelloWebApplication.Models;
using TrelloWebApplication.Utiliti;
namespace TrelloMailReporter.MailScheduledJob
{
    public class SendMailJob : IJob
    {
        private DatabaseContext db = new DatabaseContext();
        public delegate void Del(ref ExcelPackage fileEx);
        PopolateModel popModel = new PopolateModel();
        ReportMethods dlg = new ReportMethods();
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() => SendMail());
        }
        /// <summary>
        /// metodo per l'invio di una mail con in allegato il file excel
        /// </summary>
        public void SendMail()
        {
            if (2<=db.Emails.Count())
            {
                Api myApi = popModel.Crea();                
                var checkkiamo = db.Tracings.ToList().Where(g => g.Board.IdBoard == myApi.idBrod && g.Check == false);
                //se la lista checkkiamo ha uno o più valori significa che ci sono state modifiche sulla board
                //su cui mi trovo e devo inviarela mail
                if (checkkiamo.Count()>=1)
                {
                    Email[] p = db.Emails.ToArray();
                    string emailSend = p[0].SenderEmail;
                    string password = p[0].Password;
                    SecurityPWD secPsw = new SecurityPWD();
                    secPsw.Decrypt(password);
                    using (var memoryStream = new MemoryStream())
                    {
                        //creazione allegato excel prima di inviare la mail
                        Del del = dlg.DelegateMethod;
                        ExcelPackage ex = new ExcelPackage();
                        del(ref ex);
                        ex.SaveAs(memoryStream);
                        memoryStream.Position = 0;
                        //Crea oggetto di tipo MailMessage
                        MailMessage Msg = new MailMessage();
                        //Imposta il mittente
                        //Msg.From = new MailAddress("trelloreporterapp@hotmail.com", "Limi");
                        Msg.From = new MailAddress(emailSend, "Trello Web App");
                        //La proprietà .To è una collezione di destinatari,
                        //quindi possiamo addizionare quanti destinatari vogliamo.
                        var primo = 0;
                        foreach (var mailCredentials in db.Emails.ToArray())
                        {
                            if (primo == 0)
                            {
                                primo = 1;
                            }
                            else
                            {
                                Msg.To.Add(new MailAddress(mailCredentials.ReceiverEmail, mailCredentials.ReceiverEmail));
                            }
                        }
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
                        NetworkCredential Credential = new NetworkCredential(emailSend, password);
                        Smtp.Credentials = Credential;

                        //Certificato SSL
                        Smtp.EnableSsl = true;

                        //Spediamo la mail
                        Smtp.Send(Msg);

                        //Lancio CheckEvents di TraceMethod che spunterà tutti gli eventi della tabella tracing
                        TraceMethod check = new TraceMethod();
                        check.CheckEvents();
                    }
                }              
            }
        }
    }
}