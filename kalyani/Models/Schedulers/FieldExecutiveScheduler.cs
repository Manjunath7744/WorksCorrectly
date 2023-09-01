using AutoSherpa_project.Models.Scheduler;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.Schedulers
{
    public class FieldExecutiveScheduler
    {
        public static IScheduler fieldExecutiveScheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
        public static void InitializeScheduler()
        {
            //Getting Defauls Scheduler Job

            //int min = 30;
            fieldExecutiveScheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<FieldSchedulerJob>().Build();
            ITrigger trigger = TriggerBuilder.Create().WithIdentity("FEPushingTrigger", "FEPushingGroup")
                .StartNow().WithSimpleSchedule(s => s.WithIntervalInMinutes(2).RepeatForever()).Build();

            IJobDetail jobDetai2 = JobBuilder.Create<FESchedulerPulling>().Build();
            ITrigger trigger2 = TriggerBuilder.Create().WithIdentity("FEPullingTrigger", "FEPullingGroup")
                .StartNow().WithSimpleSchedule(s => s.WithIntervalInMinutes(10).RepeatForever()).Build();

            fieldExecutiveScheduler.ScheduleJob(jobDetail, trigger);
            fieldExecutiveScheduler.ScheduleJob(jobDetai2, trigger2);
        }
    }

}