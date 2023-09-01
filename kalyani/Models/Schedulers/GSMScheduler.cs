using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.Schedulers
{
    
    public class GSMScheduler
    {
        public static IScheduler GsmScheduler = StdSchedulerFactory.GetDefaultScheduler().Result;

        public static void InitializeScheduler()
        {
            //Getting Defauls Scheduler Job

            int min = 10;
            GsmScheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<GSMCallSynchJob>().Build();
            ITrigger trigger = TriggerBuilder.Create().WithIdentity("trigger1", "group1")
                .StartNow().WithSimpleSchedule(s => s.WithIntervalInMinutes(min).RepeatForever()).Build();

            GsmScheduler.ScheduleJob(jobDetail, trigger);

        }
    }
}