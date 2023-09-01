using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;


namespace AutoSherpa_project.Models.Schedulers
{
    [DisallowConcurrentExecution]
    public class AutoWhatsappCronJob : schedulerCommonFunction, IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            string siteRoot = HttpRuntime.AppDomainAppVirtualPath;
            Logger logger = LogManager.GetLogger("apkRegLogger");
            string response_string = string.Empty;

            logger.Info("\n Auto Whatsapp Cron Job Started: " + DateTime.Now);

            if (siteRoot != "/")
            {
                try
                {
                    using (var db = new AutoSherDBContext())
                    {
                        schedulers schedulerDetails = db.schedulers.FirstOrDefault(m => m.scheduler_name == "whatsappcron" && m.isActive == true);

                        if (schedulerDetails != null && schedulerDetails.isActive == true)
                        {

                            startScheduler("whatsappcron");

                            List<whatsupscrondata> WhatsappList = db.whatsupscrondata.Where(m => m.sentstatus == 0).OrderBy(m => m.id).Skip(0).Take(500).ToList();

                            if (WhatsappList.Count > 0)
                            {
                                foreach (var whatsapp in WhatsappList)
                                {
                                    smsinteraction smsinteraction = new smsinteraction();

                                    try
                                    {
                                        smsinteraction.customer_id = whatsapp.customer_id;
                                        smsinteraction.vehicle_vehicle_id = whatsapp.vehicle_id;
                                        smsinteraction.isAutoSMS = true;
                                        smsinteraction.interactionDate = DateTime.Now.ToString();
                                        smsinteraction.interactionDateAndTime = DateTime.Now;
                                        smsinteraction.interactionTime = DateTime.Now.TimeOfDay.ToString();
                                        smsinteraction.interactionType = "WhatsappCron Msg";
                                        smsinteraction.smsStatus = true;
                                        smsinteraction.mobileNumber = whatsapp.phonenumber;
                                        smsinteraction.smsType = whatsapp.smsid;
                                        smsinteraction.smsMessage = whatsapp.sms;

                                        WebRequest request = WebRequest.Create("https://e2ewebservice20190528111726.azurewebsites.net/api/ORAIWhatsappNotification");
                                        var httprequest = (HttpWebRequest)request;

                                        httprequest.PreAuthenticate = true;
                                        httprequest.Method = "POST";
                                        httprequest.ContentType = "application/json";
                                        httprequest.Accept = "application/json";
                                        dynamic requestbody = new JObject();

                                        requestbody.From = System.Configuration.ConfigurationManager.AppSettings["From"];
                                        requestbody.Body = whatsapp.sms;
                                        requestbody.To = "+91" + whatsapp.phonenumber;
                                        requestbody.AccountSid = System.Configuration.ConfigurationManager.AppSettings["TWILIO_ACCOUNT_SID"];
                                        requestbody.AuthToken = System.Configuration.ConfigurationManager.AppSettings["TWILIO_AUTH_TOKEN"];

                                        using (var streamWriter = new StreamWriter(httprequest.GetRequestStream()))
                                        {
                                            var bodyContent = JsonConvert.SerializeObject(requestbody);
                                            bodyContent = bodyContent.Replace("\\\\n", "\\n");
                                            streamWriter.Write(bodyContent);

                                            streamWriter.Flush();
                                            streamWriter.Close();
                                        }

                                        HttpWebResponse response = null;
                                        response = (HttpWebResponse)httprequest.GetResponse();

                                        using (Stream strem = response.GetResponseStream())
                                        {
                                            StreamReader sr = new StreamReader(strem);
                                            response_string = sr.ReadToEnd();
                                            sr.Close();
                                        }

                                        response_string = JsonConvert.SerializeObject(response_string);
                                        smsinteraction.responseFromGateway = response_string;

                                        //whatsapp.responseFromGateway = JsonConvert.SerializeObject();

                                        whatsapp.updated_date = DateTime.Now;
                                        whatsapp.sentstatus = 1;

                                        smsinteraction.responseFromGateway = whatsapp.responseFromGateway;
                                        smsinteraction.reason = "Sent Successfully";
                                        whatsapp.reasons = "Sent Successfully";

                                        logger.Info("\n\n--AUTO Whatsapp Cron Api returned with response\n: " + whatsapp.responseFromGateway + "\n CustId= " + whatsapp.customer_id + " || VehicleId= " + whatsapp.vehicle_id);


                                    }
                                    catch (Exception ex)
                                    {
                                        string exception = "";

                                        whatsapp.sentstatus = 0;

                                        //whatsapp.reasons = "Sent Successfully";
                                        whatsapp.reasons = "Error while sending";

                                        whatsapp.sentdatetime = DateTime.Now;

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

                                        logger.Info("\n Auto Whatsapp Data Loop try exception: \n" + exception);
                                        whatsapp.responseFromGateway = exception;
                                        smsinteraction.responseFromGateway = exception;

                                    }
                                    db.smsinteractions.Add(smsinteraction);
                                    db.whatsupscrondata.AddOrUpdate(whatsapp);
                                    db.SaveChanges();
                                }

                            }
                            stopScheduler("whatsappcron");
                        }
                        else
                        {
                            logger.Info("\n autocron-Whatsapp Synch Inactive / Not Exist / Already Running");
                        }
                    }
                }
                catch (Exception ex)
                {
                    stopScheduler("whatsappcron");
                    string exception = "";

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

                    logger.Info("\n Auto Whatsapp outer try exception: \n" + exception);

                }
            }
        }

        
    }
    public class AutoCronWhatsappJob
    {
        public static IScheduler autoWhatsappScheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
        public static void InitializeScheduler()
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");

            try
            {
                string cronSchedule = "0 0 0/1 1/1 * ? *";
                logger.Info("\n\n -------- AutoWhatsapp Scheduler Started -------------|AT: " + cronSchedule);

                autoWhatsappScheduler.Start();

                IJobDetail jobDetail = JobBuilder.Create<AutoWhatsappCronJob>().Build();

                //ITrigger trigger = TriggerBuilder.Create().WithIdentity("autoWhatsappTrigger", "autoWhatsappGroup")
                //    .StartNow().Build();

                ITrigger trigger = TriggerBuilder.Create().WithIdentity("autoWhatsappTrigger", "autoWhatsappGroup")
                   .StartNow().WithCronSchedule(cronSchedule).Build();


                autoWhatsappScheduler.ScheduleJob(jobDetail, trigger);
            }
            catch (Exception ex)
            {


                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        logger.Info("\n\n -------- AutoWhatsapp Scheduler OutBlock -------\n" + ex.InnerException.InnerException.Message);
                    }
                    else
                    {
                        logger.Info("\n\n -------- AutoWhatsapp SchedulerOutBlock -------\n" + ex.InnerException.Message);
                    }
                }
                else
                {
                    logger.Info("\n\n -------- AutoWhatsapp Scheduler -------\n" + ex.Message);
                }

            }

        }
    }
}