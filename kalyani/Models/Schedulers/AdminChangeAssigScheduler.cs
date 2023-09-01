using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoSherpa_project.Models;

namespace AutoSherpa_project.Models.Schedulers
{
    public class AdminChangeAssigScheduler
    {
        public static IScheduler adminChangeAssignscheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
        public static void InitializeScheduler()
        {
            try
            {
                adminChangeAssignscheduler.Start();

                IJobDetail jobDetail = JobBuilder.Create<AdminChangeAssignJob>().Build();
                ITrigger trigger = TriggerBuilder.Create().WithIdentity("adminChangeAssignTrigger", "adminChangeAssignGroup")
                    .StartNow().WithSimpleSchedule(s => s.WithIntervalInMinutes(30).RepeatForever()).Build();

                adminChangeAssignscheduler.ScheduleJob(jobDetail, trigger);
            }
            catch(Exception ex)
            {

            }
            //Getting Defauls Scheduler Job
           
        }
    }
}