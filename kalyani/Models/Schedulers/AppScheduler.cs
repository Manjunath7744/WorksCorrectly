using AutoSherpa_project.Models.Scheduler;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.Schedulers
{
    public class AppScheduler
    {
        public static IScheduler appSchedulers = StdSchedulerFactory.GetDefaultScheduler().Result;
        public static void InitializeScheduler()
        {
            //Getting Defauls Scheduler Job

            //int min = 30;
            appSchedulers.Start();

            IJobDetail jobDetail = JobBuilder.Create<FieldSchedulerJob>().Build();
            ITrigger trigger = TriggerBuilder.Create().WithIdentity("FEPushingTrigger", "FEPushingGroup")
                .StartNow().WithSimpleSchedule(s => s.WithIntervalInMinutes(8).RepeatForever()).Build();

            IJobDetail jobDetai2 = JobBuilder.Create<FESchedulerPulling>().Build();
            ITrigger trigger2 = TriggerBuilder.Create().WithIdentity("FEPullingTrigger", "FEPullingGroup")
                .StartNow().WithSimpleSchedule(s => s.WithIntervalInMinutes(15).RepeatForever()).Build();



            IJobDetail driverJobs1 = JobBuilder.Create<DriverPushingJob>().Build();
            ITrigger driverTrigger1 = TriggerBuilder.Create().WithIdentity("DriverPushingTrigger", "DriverPushingGroup")
                .StartNow().WithSimpleSchedule(s => s.WithIntervalInMinutes(10).RepeatForever()).Build();


            IJobDetail driverJobs2 = JobBuilder.Create<FESchedulerPulling>().Build();
            ITrigger driverTrigger2 = TriggerBuilder.Create().WithIdentity("DriverPullingTrigger", "DriverPullingGroup")
                .StartNow().WithSimpleSchedule(s => s.WithIntervalInMinutes(10).RepeatForever()).Build();
            
            //FE scheduler Code
            //appSchedulers.ScheduleJob(jobDetail, trigger);
            //appSchedulers.ScheduleJob(jobDetai2, trigger2);

            //Driver Scheduler Code
            appSchedulers.ScheduleJob(driverJobs1, driverTrigger1);
            //appSchedulers.ScheduleJob(driverJobs2, driverTrigger2);
        }
    }

}