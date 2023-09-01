using NLog;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.Schedulers
{
    public class FirebaseJob
    {
        public static IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
        public static void InitializeScheduler()
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {
                string cronSchedule = "0 0 0/4 1/1 * ? *";
                //string cronSchedule = "0 0/15 0-08 ? * *";
                logger.Info("\n\n -------- Firebase Cron Scheduler Started -------------|AT: " + cronSchedule);

                scheduler.Start();

                IJobDetail jobDetail = JobBuilder.Create<FirebaseScheduler>().Build();
                ITrigger trigger = TriggerBuilder.Create().WithIdentity("trigger1", "group1")
                    .StartNow().WithCronSchedule(cronSchedule).Build();

                //ITrigger trigger = TriggerBuilder.Create().WithIdentity("trigger1", "group1")
                //    .StartNow().WithSimpleSchedule(s => s.WithIntervalInMinutes(15).RepeatForever()).Build();

                scheduler.ScheduleJob(jobDetail, trigger);
            }
            catch(Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        logger.Info("\n\n -------- Firebase Scheduler OutBlock -------\n" + ex.InnerException.InnerException.Message);
                    }
                    else
                    {
                        logger.Info("\n\n -------- Firebase SchedulerOutBlock -------\n" + ex.InnerException.Message);
                    }
                }
                else
                {
                    logger.Info("\n\n -------- Firebase Scheduler -------\n" + ex.Message);
                }
            }
            //Getting Defauls Scheduler Job
           
        }
    }
}