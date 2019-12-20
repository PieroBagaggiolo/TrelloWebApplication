using Quartz;
using Quartz.Impl;

namespace TrelloMailReporter.MailScheduledJob
{
    public class JobScheduler
    {
        //Questo metodo verrà chiamato sul metodo start di global.asax cosi' quando lanciamo l'app parte la funzione
        public static void Start()
        {
            //IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            //scheduler.Start();
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().GetAwaiter().GetResult();
            //IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<SendMailJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                //scelgo ogni quanto eseguire il job ed a che ora farlo partire
                  (s =>
                     s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(9, 35))
                  )
                .Build();
            scheduler.ScheduleJob(job, trigger);
        }
    }
}
