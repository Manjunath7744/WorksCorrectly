using AutoSherpa_project.Models.Scheduler;
using NLog;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AutoSherpa_project.Models.Schedulers
{
    public class EventsScheduler
    {
        public static IScheduler EventsSchedulers = StdSchedulerFactory.GetDefaultScheduler().Result;
        public static void InitializeScheduler()
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {
                string cronSchedule = "0 30 08 ? * *";
                logger.Info("\n\n -------- Events Scheduler Started -------------|AT: " + cronSchedule);

                EventsSchedulers.Start();

                IJobDetail jobDetail = JobBuilder.Create<EventsJob>().Build();
                //ITrigger trigger = TriggerBuilder.Create().WithIdentity("eventsTrigger", "eventsGroup").StartNow().Build();
                ITrigger trigger = TriggerBuilder.Create().WithIdentity("eventsTrigger", "eventsGroup")
                    .StartNow().WithCronSchedule(cronSchedule).Build();

                EventsSchedulers.ScheduleJob(jobDetail, trigger);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        logger.Info("\n\n -------- Events Scheduler OutBlock -------\n" + ex.InnerException.InnerException.Message);
                    }
                    else
                    {
                        logger.Info("\n\n -------- Events SchedulerOutBlock -------\n" + ex.InnerException.Message);
                    }
                }
                else
                {
                    logger.Info("\n\n -------- Events Scheduler -------\n" + ex.Message);
                }

            }

        }
    }
}