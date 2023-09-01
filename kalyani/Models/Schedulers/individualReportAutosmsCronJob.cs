using Newtonsoft.Json.Linq;
using NLog;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace AutoSherpa_project.Models.Schedulers
{
    [DisallowConcurrentExecution]
    public class individualReportAutosmsCronJob: schedulerCommonFunction, IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            string siteRoot = HttpRuntime.AppDomainAppVirtualPath;
            Logger logger = LogManager.GetLogger("apkRegLogger");

            logger.Info("\n individualReport AUTO SMS Cron Job Started: " + DateTime.Now);
            if (siteRoot != "/")
            {
                try
                {
                    string exception = "";
                    string response_string = string.Empty;
                    long totalSent, totalFailed = 0;
                    using (var db = new AutoSherDBContext())
                    {

                        dealer dealerData = db.dealers.FirstOrDefault();

                        schedulers schedulerDetails = db.schedulers.FirstOrDefault(m => m.dealerid == dealerData.id && m.scheduler_name == "individualReportsms");

                        if (schedulerDetails != null && schedulerDetails.isActive == true)
                        {

                            startScheduler("individualReportsms");

                            int maxLength = 20000;

                            if (schedulerDetails.datalenght != 0)
                            {
                                maxLength = schedulerDetails.datalenght;
                            }

                            List<individualreportsmscrondata> smsList = db.Individualreportsmscrondatas.Where(m => m.sentstatus == false).OrderBy(m => m.id).Skip(0).Take(maxLength).ToList();

                            if (smsList.Count > 0)
                            {
                                foreach (var sms in smsList)
                                {

                                    try
                                    {
                                        string APIURL = string.Empty;

                                        APIURL = sms.smsapi;

                                        APIURL = APIURL.Replace("REPLACEPHONE", sms.phonenumber);

                                        string urlEnCodedSMS = sms.sms;
                                        APIURL = APIURL.Replace("REPLACEMESSAGE", urlEnCodedSMS);

                                        WebRequest request = WebRequest.Create(APIURL);
                                        HttpWebRequest httpWebRequest = (HttpWebRequest)request;
                                        httpWebRequest.Method = "GET";
                                        httpWebRequest.Accept = "application/json";

                                        logger.Info("\n\n--individualReport AUTO SMS Cron Api Started with url: " + APIURL + "\n CustId= " + sms.customer_id + " || VehicleId= " + sms.vehicle_id);

                                        HttpWebResponse response = null;
                                        response = (HttpWebResponse)httpWebRequest.GetResponse();

                                        using (Stream strem = response.GetResponseStream())
                                        {
                                            StreamReader sr = new StreamReader(strem);
                                            response_string = sr.ReadToEnd();

                                            sr.Close();
                                        }
                                        sms.reasons = response_string;

                                        logger.Info("\n\n--individualReport AUTO SMS Cron Api returned with response\n: " + response_string + "\n CustId= " + sms.customer_id + " || VehicleId= " + sms.vehicle_id);

                                        if (response_string != null && response_string != "")
                                        {
                                            smsstatu status = new smsstatu();
                                            bool IsSuccessResponse = false;

                                            if (dealerData.dealerCode == "INDUS" || dealerData.dealerCode == "testdb")
                                            {
                                                Regex r = new Regex(@"^\d+$");
                                                if (r.IsMatch(response_string))
                                                {
                                                    //only number
                                                    sms.sentstatus = true;
                                                    sms.updated_date = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                }
                                                else
                                                {
                                                    sms.sentstatus = false;
                                                    sms.updated_date = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                    totalFailed++;
                                                }
                                            }
                                            else if (dealerData.dealerCode == "KALYANIMOTORS")
                                            {
                                                if (response_string.Contains("<status>OK</status>"))
                                                {
                                                    sms.sentstatus = true;
                                                    sms.updated_date = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                }
                                                else
                                                {
                                                    sms.sentstatus = false;
                                                    sms.updated_date = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                    totalFailed++;
                                                }
                                            }
                                            else if ( dealerData.dealerCode == "SOMANIHYUNDAI")
                                            {
                                                if (response_string.Contains("S."))
                                                {
                                                    sms.sentstatus = true;
                                                    sms.updated_date = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                }
                                                else
                                                {
                                                    sms.sentstatus = false;
                                                    sms.updated_date = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                    totalFailed++;
                                                }
                                            }
                                            else if (dealerData.dealerCode == "CAUVERYFORD" || dealerData.dealerCode == "ADVAITHHYUNDAI")
                                            {
                                                dynamic api_result = JObject.Parse(response_string);
                                                string Reqstatus = api_result.status;

                                                if (Reqstatus == "OK")
                                                {
                                                    sms.sentstatus = true;
                                                    sms.updated_date = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                }
                                                else
                                                {
                                                    sms.sentstatus = false;
                                                    sms.updated_date = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                    totalFailed++;
                                                }
                                            }
                                            else if (dealerData.dealerCode == "POPULAR")
                                            {
                                                dynamic api_result = JObject.Parse(response_string);
                                                string Reqstatus = api_result.success;

                                                if (Reqstatus == "1")
                                                {
                                                    sms.sentstatus = true;
                                                    sms.updated_date = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                }
                                                else
                                                {
                                                    sms.sentstatus = false;
                                                    sms.updated_date = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                    totalFailed++;
                                                }
                                            }

                                            if (IsSuccessResponse == false)
                                            {
                                                status = db.smsstatus.FirstOrDefault(m => m.code.Contains(response_string));
                                                if (status == null)
                                                {
                                                    sms.sentstatus = false;
                                                    sms.updated_date = DateTime.Now;
                                                    totalFailed++;
                                                }
                                                else if (status != null)
                                                {
                                                    sms.sentstatus = true;
                                                    sms.updated_date = DateTime.Now;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            sms.sentstatus = false;
                                            sms.updated_date = DateTime.Now;
                                            totalFailed++;
                                        }


                                        db.SaveChanges();
                                    }
                                    catch (Exception ex)
                                    {
                                        

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
                                        sms.sentstatus = false;
                                        sms.reasons = exception;

                                        sms.updated_date = DateTime.Now;
                                        logger.Info("\n individualReport AutoSMS Data Loop try exception: \n" + exception);
                                        db.SaveChanges();
                                        totalFailed++;

                                    }
                                    finally
                                    {
                                        smsinteraction smsinteraction = new smsinteraction();
                                        smsinteraction.interactionDate = DateTime.Now.ToString("dd-MM-yyyy");
                                        smsinteraction.interactionDateAndTime = DateTime.Now;
                                        smsinteraction.interactionTime = DateTime.Now.ToString("HH:mm:ss");
                                        smsinteraction.interactionType = "Text Msg";
                                        smsinteraction.responseFromGateway = response_string;
                                        smsinteraction.customer_id = sms.customer_id;
                                        smsinteraction.vehicle_vehicle_id = sms.vehicle_id;
                                        smsinteraction.wyzUser_id = sms.wyzuser_id;
                                        smsinteraction.mobileNumber = sms.phonenumber;
                                        smsinteraction.smsType = sms.smsid;
                                        smsinteraction.smsMessage = sms.sms;
                                        smsinteraction.isAutoSMS = false;
                                        smsinteraction.smsStatus = sms.sentstatus;
                                        smsinteraction.reason = response_string;
                                        db.smsinteractions.Add(smsinteraction);
                                        db.SaveChanges();

                                    }

                                }
                            }
                            stopScheduler("individualReportsms");

                            if (smsList.Count > 0)
                            {
                                sendEmail(smsList.Count, totalFailed, dealerData.dealerName);
                            }
                        }
                        else
                        {
                            logger.Info("\n individualReport-sms Synch Inactive / Not Exist / Already Running");
                        }
                    }
                }
                catch (Exception ex)
                {
                    stopScheduler("individualReportsms");
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

                    logger.Info("\nindividualReport AutoSMS outer try exception: \n" + exception);

                }
            }
        }

    }
    public class IndividualReportRunJob
    {
        public static IScheduler autoSMSIndividualReportScheduler = StdSchedulerFactory.GetDefaultScheduler().Result;

        public static void InitializeScheduler()
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            //Getting Defauls Scheduler Job
            try
            {
                string cronSchedule = "0 0 0/4 1/1 * ? *";
                logger.Info("\n\n --------individualReport AutoSMS Scheduler Started -------------|AT: " + cronSchedule);

                autoSMSIndividualReportScheduler.Start();

                IJobDetail jobDetail = JobBuilder.Create<individualReportAutosmsCronJob>().Build();
                //ITrigger trigger = TriggerBuilder.Create().WithIdentity("individualReportcronSMSTrigger", "individualReportcronSMSGroup").StartNow().Build();
                //ITrigger trigger = TriggerBuilder.Create().WithIdentity("individualReportcronSMSTrigger", "individualReportcronSMSGroup")
                //.StartNow().WithSimpleSchedule(s => s.WithIntervalInMinutes(5).RepeatForever()).Build();
                ITrigger trigger = TriggerBuilder.Create().WithIdentity("individualReportcronSMSTrigger", "individualReportcronSMSGroup")
                .StartNow().WithCronSchedule(cronSchedule).Build();

                autoSMSIndividualReportScheduler.ScheduleJob(jobDetail, trigger);
            }
            catch (Exception ex)
            {


                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        logger.Info("\n\n --------individualReport AutoSMS Scheduler OutBlock -------\n" + ex.InnerException.InnerException.Message);
                    }
                    else
                    {
                        logger.Info("\n\n --------individualReport AutoSMS SchedulerOutBlock -------\n" + ex.InnerException.Message);
                    }
                }
                else
                {
                    logger.Info("\n\n --------individualReport AutoSMS Scheduler -------\n" + ex.Message);
                }

            }

        }
    }
}