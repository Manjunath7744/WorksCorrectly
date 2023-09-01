using Newtonsoft.Json.Linq;
using NLog;
using Quartz;
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
    public class AutoSMSCronJob : schedulerCommonFunction, IJob
    {
        public async Task Execute(IJobExecutionContext context)
     {
            string siteRoot = HttpRuntime.AppDomainAppVirtualPath;
            Logger logger = LogManager.GetLogger("apkRegLogger");

            logger.Info("\n Auto SMS Cron Job Started: " + DateTime.Now);
            if (siteRoot != "/")
            {
                try
                {
                    long totalSent, totalFailed = 0;
                    using (var db = new AutoSherDBContext())
                    {

                        dealer dealerData = db.dealers.FirstOrDefault();

                        schedulers schedulerDetails = db.schedulers.FirstOrDefault(m => m.dealerid == dealerData.id && m.scheduler_name == "autocron-sms" && m.isActive == true);

                        if (schedulerDetails!=null && schedulerDetails.isActive == true)
                        {

                            startScheduler("autocron-sms");

                            int maxLength = 100;
                         
                            if (schedulerDetails.datalenght != 0)
                            {
                                maxLength = schedulerDetails.datalenght;
                            }

                            List<smscrondata> smsList = db.Smscrondatas.Where(m => m.sentstatus == 0).OrderBy(m => m.id).Skip(0).Take(maxLength).ToList();

                            if (smsList.Count > 0)
                            {
                                foreach (var sms in smsList)
                                {

                                    try
                                    {
                                        string APIURL = string.Empty;

                                        APIURL = sms.smsapi;
                                        if (dealerData.dealerCode != "KATARIA" && dealerData.dealerCode != "AMMOTORS")
                                        {
                                            var phonenumber = "+91" + sms.phonenumber;
                                            APIURL = APIURL.Replace("REPLACEPHONE", phonenumber);
                                            
                                        }
                                        else
                                        {
                                            var phonenumber = sms.phonenumber;
                                            APIURL = APIURL.Replace("REPLACEPHONE", phonenumber); 
                                        }


                                        //APIURL = APIURL.Replace("REPLACEPHONE", sms.phonenumber);
                                        string urlEnCodedSMS = HttpUtility.UrlEncode(sms.sms);
                                        APIURL = APIURL.Replace("REPLACEMESSAGE", urlEnCodedSMS);
                                        WebRequest request = WebRequest.Create(APIURL);
                                        HttpWebRequest httpWebRequest = (HttpWebRequest)request;
                                        httpWebRequest.Method = "GET";
                                        httpWebRequest.Accept = "application/json";

                                        logger.Info("\n\n--AUTO SMS Cron Api Started with url: " + APIURL + "\n CustId= " + sms.customer_id + " || VehicleId= " + sms.vehicle_id);

                                        HttpWebResponse response = null;
                                        response = (HttpWebResponse)httpWebRequest.GetResponse();

                                        string response_string = string.Empty;
                                        using (Stream strem = response.GetResponseStream())
                                        {
                                            StreamReader sr = new StreamReader(strem);
                                            response_string = sr.ReadToEnd();

                                            sr.Close();
                                        }

                                        logger.Info("\n\n--AUTO SMS Cron Api returned with response\n: " + response_string + "\n CustId= " + sms.customer_id + " || VehicleId= " + sms.vehicle_id);

                                        if (response_string != null && response_string != "")
                                        {
                                            smsstatu status = new smsstatu();
                                            bool IsSuccessResponse = false;

                                            if (dealerData.dealerCode == "INDUS")
                                            {
                                                Regex r = new Regex(@"^\d+$");
                                                if (r.IsMatch(response_string))
                                                {
                                                    //only number
                                                    sms.sentstatus = 1;
                                                    sms.reasons = "Send Successfully";
                                                    sms.responseFromGateway = response_string;
                                                    sms.sentdatetime = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                }
                                                else
                                                {
                                                    sms.sentstatus = 0;
                                                    sms.reasons = "Failed to sent";
                                                    sms.responseFromGateway = response_string;
                                                    sms.sentdatetime = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                    totalFailed++;
                                                }
                                            }
                                            if (dealerData.dealerCode == "RISHABHFOURWHEELS")
                                            {
                                                Regex r = new Regex(@"^[a-zA-Z0-9]*");
                                                if (r.IsMatch(response_string))
                                                {
                                                    //only number
                                                    sms.sentstatus = 1;
                                                    sms.reasons = "Send Successfully";
                                                    sms.responseFromGateway = response_string;
                                                    sms.sentdatetime = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                }
                                                else
                                                {
                                                    sms.sentstatus = 0;
                                                    sms.reasons = "Failed to sent";
                                                    sms.responseFromGateway = response_string;
                                                    sms.sentdatetime = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                    totalFailed++;
                                                }
                                            }
                                            else if (dealerData.dealerCode == "SOMANIHYUNDAI")
                                            {
                                                if (response_string.Contains("S."))
                                                {
                                                    //only number
                                                    sms.sentstatus = 1;
                                                    sms.reasons = "Send Successfully";
                                                    sms.responseFromGateway = response_string;
                                                    sms.sentdatetime = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                }
                                                else
                                                {
                                                    sms.sentstatus = 0;
                                                    sms.reasons = "Failed to sent";
                                                    sms.responseFromGateway = response_string;
                                                    sms.sentdatetime = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                    totalFailed++;
                                                }
                                            }
                                            else if (dealerData.dealerCode == "SUKHMANI" || dealerData.dealerCode == "KATARIA" || dealerData.dealerCode == "AMMOTORS")
                                            {
                                                if (response_string.Contains("success") || response_string.Contains("Success") || response_string.Contains("OK") || response_string.Contains("SMS-SHOOT-ID/ammotors"))
                                                {
                                                    //only number
                                                    sms.sentstatus = 1;
                                                    sms.reasons = "Send Successfully";
                                                    sms.responseFromGateway = response_string;
                                                    sms.sentdatetime = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                }
                                                else
                                                {
                                                    sms.sentstatus = 0;
                                                    sms.reasons = "Failed to sent";
                                                    sms.responseFromGateway = response_string;
                                                    sms.sentdatetime = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                    totalFailed++;
                                                }
                                            }
                                            else if (dealerData.dealerCode == "KALYANIMOTORS" )
                                            {
                                                if (response_string.Contains("<status>OK</status>"))
                                                {
                                                    sms.sentstatus = 1;
                                                    sms.reasons = "Send Successfully";
                                                    sms.responseFromGateway = response_string;
                                                    sms.sentdatetime = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                }
                                                else
                                                {
                                                    sms.sentstatus = 0;
                                                    sms.reasons = "Failed to sent";
                                                    sms.responseFromGateway = response_string;
                                                    sms.sentdatetime = DateTime.Now;
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
                                                    sms.sentstatus = 1;
                                                    sms.reasons = "Send Successfully";
                                                    sms.responseFromGateway = response_string;
                                                    sms.sentdatetime = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                }
                                                else
                                                {
                                                    sms.sentstatus = 0;
                                                    sms.reasons = "Failed to sent";
                                                    sms.responseFromGateway = response_string;
                                                    sms.sentdatetime = DateTime.Now;
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
                                                    sms.sentstatus = 1;
                                                    sms.reasons = "Send Successfully";
                                                    sms.responseFromGateway = response_string;
                                                    sms.sentdatetime = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                }
                                                else
                                                {
                                                    sms.sentstatus = 0;
                                                    sms.reasons = "Failed to sent";
                                                    sms.responseFromGateway = response_string;
                                                    sms.sentdatetime = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                    totalFailed++;
                                                }
                                            }                              
                                            else if (dealerData.dealerCode == "ABTMARUTHI")
                                            {
                                                dynamic api_result = JObject.Parse(response_string);
                                                string Reqstatus = api_result.success;

                                                if (Reqstatus == "1")
                                                {
                                                    sms.sentstatus = 1;
                                                    sms.reasons = "Send Successfully";
                                                    sms.responseFromGateway = response_string;
                                                    sms.sentdatetime = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                }
                                                else
                                                {
                                                    sms.sentstatus = 0;
                                                    sms.reasons = "Failed to sent";
                                                    sms.responseFromGateway = response_string;
                                                    sms.sentdatetime = DateTime.Now;
                                                    IsSuccessResponse = true;
                                                    totalFailed++;
                                                }
                                            }
                                            if (IsSuccessResponse == false)
                                            {
                                                status = db.smsstatus.FirstOrDefault(m => m.code.Contains(response_string));
                                                if (status == null)
                                                {
                                                    sms.sentstatus = 0;
                                                    sms.reasons = "Failed to sent.";
                                                    sms.responseFromGateway = response_string;
                                                    sms.sentdatetime = DateTime.Now;
                                                    totalFailed++;
                                                }
                                                else if (status != null)
                                                {
                                                    sms.sentstatus = 1;
                                                    sms.reasons = status.description;
                                                    sms.responseFromGateway = response_string;
                                                    sms.sentdatetime = DateTime.Now;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            sms.sentstatus = 0;
                                            sms.reasons = "Sent Failure, Null response from Gateway";
                                            sms.responseFromGateway = response_string;
                                            sms.sentdatetime = DateTime.Now;
                                            totalFailed++;
                                        }


                                        db.SaveChanges();
                                    }
                                    catch (Exception ex)
                                    {
                                        string exception = "";

                                        sms.sentstatus = 0;
                                        sms.reasons = "Sent Successfully";

                                        sms.sentdatetime = DateTime.Now;

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

                                        logger.Info("\n AutoSMS Data Loop try exception: \n" + exception);
                                        sms.responseFromGateway = exception;
                                        db.SaveChanges();
                                        totalFailed++;

                                    }

                                }

                                if (dealerData.dealerCode == "INDUS" || dealerData.dealerCode== "POPULAR")
                                {
                                    db.Database.ExecuteSqlCommandAsync("CALL updatesmsintearctionintotable()");
                                }
                            }
                            stopScheduler("autocron-sms");

                            if (smsList.Count > 0)
                            {
                                sendEmail(smsList.Count, totalFailed, dealerData.dealerName);
                            }
                        }
                        else
                        {
                            logger.Info("\n autocron-sms Synch Inactive / Not Exist / Already Running");
                        }
                    }
                }
                catch (Exception ex)
                {
                    stopScheduler("autocron-sms");
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

                    logger.Info("\n AutoSMS outer try exception: \n" + exception);

                }
            }
        }


    }
}