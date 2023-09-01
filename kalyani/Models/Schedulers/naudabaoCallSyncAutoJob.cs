using System;
using System.Linq;
using System.Web;
using NLog;
using Quartz;
using Quartz.Impl;

namespace AutoSherpa_project.Models.Schedulers
{
    public class naudabaoCallSyncAutoJob
    {
        public static IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;

        public static void InitializeScheduler()
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            logger.Info("Naudabao Synch Scheduler Entered" + DateTime.Now);

            try
            {
                //using (var db = new AutoSherDBContext())
                {
                    string cronSchedule = "0 30 21 1/1 * ? *";
                    {
                        logger.Info("Naudabao Synch Scheduler Started"+DateTime.Now);
                        scheduler.Start();
                        IJobDetail jobDetail = JobBuilder.Create<naudabaoCallSync>().Build();

                        ITrigger trigger = TriggerBuilder.Create().WithIdentity("naudabaotoMySQlSyncTrigger", "naudabaotoMYSqlSyncGroup")
                            .StartNow().WithCronSchedule(cronSchedule).Build();
                        scheduler.ScheduleJob(jobDetail, trigger);

                        //ITrigger trigger = TriggerBuilder.Create().WithIdentity("naudabaoSyncTrigger", "naudabaoSyncGroup")
                        //   .StartNow().WithSimpleSchedule(s => s.WithIntervalInMinutes(30).RepeatForever()).Build();
                        //scheduler.ScheduleJob(jobDetail, trigger);

                        //}
                    }
                }
            }
            catch (Exception ex)
            {

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        logger.Info("\n\n -------- Naudabao Scheduler OutBlock -------\n" + ex.InnerException.InnerException.Message);
                    }
                    else
                    {
                        logger.Info("\n\n -------- Naudabao SchedulerOutBlock -------\n" + ex.InnerException.Message);
                    }
                }
                else
                {
                    logger.Info("\n\n -------- Naudabao Scheduler -------\n" + ex.Message);
                }

            }
        }

    }
}