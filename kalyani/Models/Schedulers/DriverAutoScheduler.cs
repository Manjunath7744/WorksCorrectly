using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.Schedulers
{
    public class DriverAutoScheduler
    {
        public static IScheduler driverScheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
        public static void InitializeScheduler()
        {
            //Getting Defauls Scheduler Job

            //int min = 30;
            driverScheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<DriverPushingJob>().Build();
            ITrigger trigger = TriggerBuilder.Create().WithIdentity("DriverPushingTrigger", "DriverPushingGroup")
                .StartNow().WithSimpleSchedule(s => s.WithIntervalInMinutes(5).RepeatForever()).Build();

            IJobDetail jobDetai2 = JobBuilder.Create<DriverPushingJob>().Build();
            ITrigger trigger2 = TriggerBuilder.Create().WithIdentity("DriverPushingTrigger", "DriverPushingGroup")
                .StartNow().WithSimpleSchedule(s => s.WithIntervalInMinutes(10).RepeatForever()).Build();

            driverScheduler.ScheduleJob(jobDetail, trigger);
            driverScheduler.ScheduleJob(jobDetai2, trigger2);
        }
    }
}