using NLog;
using PagedList;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace AutoSherpa_project.Models.Schedulers
{
    [DisallowConcurrentExecution]
    public class Event_DailyEventMsgNotification : IJob
    {
        Logger logger = LogManager.GetLogger("apkRegLogger");
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    
                    logger.Info("\n\n Event_DailyEventMsgNotification Code started  : DateTime - " + DateTime.Now);

                    string dealerName = db.dealers.FirstOrDefault().dealerName;

                    string str = @"CALL Event_dailyeventmsgNotification()";
                    string mailBody = "<table><tr><th><p style='font-size:12px;'>Upload Type</p></th><th><p style='font-size:12px;'>File Name</p></th><th><p style='font-size:12px;'>File Status</p></th><th><p style='font-size:12px;'>Error Status</p></th></tr>";
                    List<DailyEventMail> dailyEventMail = db.Database.SqlQuery<DailyEventMail>(str).ToList();
                    foreach (var bodyDetails in dailyEventMail)
                    {
                        mailBody = mailBody + "<tr><td ><p style='font-size:12px;'>" + bodyDetails.UploadtypeName + "</p></td>" + "<td><p style='font-size:12px;'>" + bodyDetails.fileName + "</p></td>" + "<td><p style='font-size:12px;'>" + bodyDetails.fileRunningStatus + "</p></td>" + "<td><p style='font-size:12px;'>" + bodyDetails.error_status + "</p></td></tr>";
                    }
                    mailBody = mailBody + "</table>";

                    emailcredential emailcredential = db.emailcredentials.FirstOrDefault(m => m.userEmail == "updates@autosherpas.com" && m.inActive == true);

                    string EmailFrom = emailcredential.userEmail;

                    List<string> mailIds = new List<string>() { "product.crm@wyzmindz.com", "support.CRM@wyzmindz.com", "updates@autosherpas.com" };
                    foreach (string to in mailIds)
                    {
                    string from = emailcredential.userEmail; //From address    
                    MailMessage message = new MailMessage(from, to);
                    message.Subject = dealerName + " Upload file status";
                    message.Body = mailBody;

                    //message.BodyEncoding = System.Text.Encoding.UTF8;
                    message.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient(emailcredential.hostapi, Convert.ToInt32(emailcredential.portnumber));
                    System.Net.NetworkCredential basicCredential1 = new
                    System.Net.NetworkCredential(emailcredential.userEmail, emailcredential.userPassword);
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = basicCredential1;
                    client.Send(message);
                    }

                }

                logger.Info("\n\n Event_DailyEventMsgNotification Code Ended  : DateTime - " + DateTime.Now);
            }
             catch (Exception ex)
            {
                string exception = string.Empty;

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        exception = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        exception = ex.InnerException.Message;
                    }
                }
                else
                {
                    exception = ex.Message;
                }

                if (ex.StackTrace.Contains(':'))
                {
                    exception = exception + "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)];
                }

                logger.Error("Event_DailyEventMsgNotification scheduler error(outer block): \n" + exception);
            }
        }
    }
    public class DailyEventMsgNotification 
    {
        public static IScheduler dailyEventMsgNotification = StdSchedulerFactory.GetDefaultScheduler().Result;
        public static void InitializeScheduler()
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {
                logger.Info("\n Event_DailyEventMsgNotification Running  --> DateTime: " + DateTime.Now);

                //string cronScheduler = "0 0 8 1/1 * ? *";
                string cronScheduler = "0 30 8 1/1 * ? *";

                AutoSherDBContext db = new AutoSherDBContext();
                var dealerCode = db.dealers.FirstOrDefault().dealerCode;
                if(dealerCode == "KATARIA" || dealerCode == "INDUS")
                {
                    cronScheduler = "0 30 8 1/1 * ? *";
                }

                dailyEventMsgNotification.Start();
                IJobDetail jobDetail = JobBuilder.Create<Event_DailyEventMsgNotification>().Build();
               // ITrigger trigger = TriggerBuilder.Create().WithIdentity("AutoEmail", "AutoEmailSms").StartNow().Build();
               ITrigger trigger = TriggerBuilder.Create().WithIdentity("AutoEmail", "AutoEmailSms").StartNow().WithCronSchedule(cronScheduler).Build();
                dailyEventMsgNotification.ScheduleJob(jobDetail, trigger);

            }
            catch(Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        logger.Info("\n\n -------- Event_DailyEventMsgNotification Scheduler OutBlock -------\n" + ex.InnerException.InnerException.Message);
                    }
                    else
                    {
                        logger.Info("\n\n -------- Event_DailyEventMsgNotification SchedulerOutBlock -------\n" + ex.InnerException.Message);
                    }
                }
                else
                {
                    logger.Info("\n\n -------- Event_DailyEventMsgNotification Scheduler -------\n" + ex.Message);
                }

            }


        }


    }


}
public class DailyEventMail
{
    public string UploadtypeName { get; set; }
    public string fileName { get; set; }
    public string error_status { get; set; }
    public string fileRunningStatus { get; set; }
}
