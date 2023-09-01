using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.Schedulers
{
    public class servicePushScheduler
    {
        public static IScheduler indusServicePushscheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
        public static void InitializeScheduler()
        {
            try
            {
                int min = 15;

                //using (var db = new AutoSherDBContext())
                //{
                //    min = db.dealers.FirstOrDefault().assignmentinterval;
                //}

                indusServicePushscheduler.Start();

                IJobDetail jobDetail = JobBuilder.Create<servicePushJob>().Build();
                ITrigger trigger = TriggerBuilder.Create().WithIdentity("servicePushTrigger", "servicePushGroup")
                    .StartNow().WithSimpleSchedule(s => s.WithIntervalInMinutes(min).RepeatForever()).Build();

                indusServicePushscheduler.ScheduleJob(jobDetail, trigger);
            }
            catch (Exception ex)
            {

            }
            //Getting Defauls Scheduler Job

        }
    }
}