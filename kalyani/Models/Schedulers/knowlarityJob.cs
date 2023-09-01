using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;
using Quartz;
using Quartz.Impl;

namespace AutoSherpa_project.Models.Schedulers
{
    public class knowlarityJob
    {
        public static IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
        public static void InitializeScheduler()
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {
                string cronSchedule = "0 0 0/4 1/1 * ? *";
                logger.Info("\n\n -------- knowlaritycallsynch Cron Scheduler Started -------------|AT: " + cronSchedule);

                scheduler.Start();

                IJobDetail jobDetail = JobBuilder.Create<knowlarityScheduler>().Build();
                //ITrigger trigger = TriggerBuilder.Create().WithIdentity("tatateleservicesTriger", "tatateleservicesGroup")
                //    .StartNow().WithCronSchedule(cronSchedule).Build();

                ITrigger trigger = TriggerBuilder.Create().WithIdentity("knowlaritycallsynchTrigger", "knowlaritycallsynchGroup")
                    .StartNow().WithSimpleSchedule(s => s.WithIntervalInMinutes(3).RepeatForever()).Build();

                scheduler.ScheduleJob(jobDetail, trigger);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        logger.Info("\n\n -------- knowlaritycallsynch Scheduler OutBlock -------\n" + ex.InnerException.InnerException.Message);
                    }
                    else
                    {
                        logger.Info("\n\n -------- knowlaritycallsynch SchedulerOutBlock -------\n" + ex.InnerException.Message);
                    }
                }
                else
                {
                    logger.Info("\n\n -------- knowlaritycallsynch Scheduler -------\n" + ex.Message);
                }
            }

        }
    }
}