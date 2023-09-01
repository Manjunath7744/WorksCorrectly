using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;
using Quartz;
using Quartz.Impl;

namespace AutoSherpa_project.Models.Schedulers
{
    public class tatateleservicesJob
    {
        public static IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
        public static void InitializeScheduler()
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {
                string cronSchedule = "0 0 0/4 1/1 * ? *";
                logger.Info("\n\n -------- tatateleservicesJob Cron Scheduler Started -------------|AT: " + cronSchedule);

                scheduler.Start();

                IJobDetail jobDetail = JobBuilder.Create<tatateleservicesScheduler>().Build();
                //ITrigger trigger = TriggerBuilder.Create().WithIdentity("tatateleservicesTriger", "tatateleservicesGroup")
                //    .StartNow().WithCronSchedule(cronSchedule).Build();

                ITrigger trigger = TriggerBuilder.Create().WithIdentity("tatateleservicesTriger", "tatateleservicesGroup")
                    .StartNow().WithSimpleSchedule(s => s.WithIntervalInMinutes(5).RepeatForever()).Build();

                scheduler.ScheduleJob(jobDetail, trigger);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        logger.Info("\n\n -------- tatateleservicesJob Scheduler OutBlock -------\n" + ex.InnerException.InnerException.Message);
                    }
                    else
                    {
                        logger.Info("\n\n -------- tatateleservicesJob SchedulerOutBlock -------\n" + ex.InnerException.Message);
                    }
                }
                else
                {
                    logger.Info("\n\n -------- tatateleservicesJob Scheduler -------\n" + ex.Message);
                }
            }

        }
    }
}