using Quartz;
using Quartz.Impl;

namespace TrelloMailReporter.MailScheduledJob
{
    public class JobScheduler
    {
        /// <summary>
        /// Questo metodo verrà chiamato sul metodo start di global.asax cosi' quando lanciamo l'app parte la funzione in background in automatico
        /// </summary>
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().GetAwaiter().GetResult();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<SendMailJob>().Build();

            //Creo il trigger che imposta come programmare il job SendMail
            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                //scelgo ogni quanto eseguire il job ed a che ora farlo partire
                  (s =>
                     s.WithIntervalInSeconds(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(16, 51))
                  )
                .Build();

            //passo allo scheduler il job e ogni quanto eseguirlo (trigger)
            scheduler.ScheduleJob(job, trigger);
        }
    }
}
