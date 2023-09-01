using NLog;
using Quartz;
using Quartz.Impl;
using System;
//using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.Schedulers
{
    public class AutoSMSCronScheduler
    {
        public static IScheduler autoSMSScheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
        public static void InitializeScheduler()
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            //Getting Defauls Scheduler Job
            try
            {
                string cronSchedule = "0 0 0/4 1/1 * ? *";
                //string cronSchedule = "0,19 0,20 0,18 ? * * *";
                logger.Info("\n\n -------- AutoSMS Scheduler Started -------------|AT: "+ cronSchedule);

                autoSMSScheduler.Start();

                IJobDetail jobDetail = JobBuilder.Create<AutoSMSCronJob>().Build();
             // ITrigger trigger = TriggerBuilder.Create().WithIdentity("autoSMSTrigger", "autoSMSGroup").StartNow().Build();
                ITrigger trigger = TriggerBuilder.Create().WithIdentity("autoSMSTrigger", "autoSMSGroup")
                    .StartNow().WithCronSchedule(cronSchedule).Build();

                autoSMSScheduler.ScheduleJob(jobDetail, trigger);
            }
            catch(Exception ex)
            {
                
                
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        logger.Info("\n\n -------- AutoSMS Scheduler OutBlock -------\n" + ex.InnerException.InnerException.Message);
                    }
                    else
                    {
                        logger.Info("\n\n -------- AutoSMS SchedulerOutBlock -------\n" + ex.InnerException.Message);
                    }
                }
                else
                {
                    logger.Info("\n\n -------- AutoSMS Scheduler -------\n" + ex.Message);
                }

            }

        }
    }
}