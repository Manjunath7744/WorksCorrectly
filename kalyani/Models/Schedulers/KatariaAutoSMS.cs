using System;
using System.Collections.Generic;
using System.Linq;
using Quartz;
using System.Threading.Tasks;
using NLog;
using MySql.Data.MySqlClient;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace AutoSherpa_project.Models.Schedulers
{
    [DisallowConcurrentExecution]
    public class KatariaAutoSMS : IJob
    {
        Logger logger = LogManager.GetLogger("apkRegLogger");
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {

                JobDataMap smsParameter = context.JobDetail.JobDataMap;
                int taggingid, moduleType;
                long wyzId, vehicleId, custId, driverId, departmentId, workshopId, dispositionId, smsId = 0;
                string customMsg, DealerCode, phoneNumbers = string.Empty, response_string = string.Empty, APIURL, url, phNum, finalPhoneNumber = string.Empty, finalSmsMessage = string.Empty, notRequiredReason = string.Empty, innerRequiredReason;

                JobDataMap dataMap = context.JobDetail.JobDataMap;

                wyzId = dataMap.GetLong("WyzId");
                vehicleId = dataMap.GetLong("vehicleId");
                custId = dataMap.GetLong("custId");
                driverId = dataMap.GetLong("driverId");
                departmentId = dataMap.GetLong("departmentId");
                workshopId = dataMap.GetLong("workshopId");
                dispositionId = dataMap.GetLong("dispositionId");
                moduleType = dataMap.GetInt("moduleType");
                customMsg = dataMap.GetString("customMsg");
                taggingid = dataMap.GetInt("taggingid");
                DealerCode = dataMap.GetString("DealerCode");
                phoneNumbers = dataMap.GetString("phoneNumbers");
                notRequiredReason = dataMap.GetString("notRequiredReason");
                innerRequiredReason = dataMap.GetString("innerRequiredReason");

                using (var db = new AutoSherDBContext())
                {

                    logger.Info("\n\n AutoSMS Code started  : vehicle Id -  " + vehicleId + "  SMS Type - " + dispositionId + "  DateTime - " + DateTime.Now);
 
                    smsinteraction smsinteraction = new smsinteraction();
                    smsparameter parameter = new smsparameter();

                    try
                    {
                        smsinteraction.customer_id = custId;
                        smsinteraction.vehicle_vehicle_id = vehicleId;
                        smsinteraction.wyzUser_id = wyzId;
                        smsinteraction.isAutoSMS = true;
                        smsinteraction.interactionDate = DateTime.Now.ToString();
                        smsinteraction.interactionDateAndTime = DateTime.Now;
                        smsinteraction.interactionTime = DateTime.Now.TimeOfDay.ToString();
                        smsinteraction.interactionType = "Text Msg";
                        smsinteraction.smsStatus = true;


                        List<smsinteraction> smsInter = new List<smsinteraction>();
                        List<long> nonContactIds = new List<long>();
                        List<string> smsInterDates = new List<string>();
                        List<long> smsIdTypes = new List<long>();
                        DateTime interactionDate = DateTime.Now;
                        if (!(string.IsNullOrEmpty(notRequiredReason)))
                        {
                            string dispoReason = notRequiredReason.Trim();
                            if (!(string.IsNullOrEmpty(innerRequiredReason))) {
                                dispoReason = innerRequiredReason.Trim();
                            }
                            if (db.calldispositiondatas.Count(m => m.disposition == dispoReason) > 0)
                            {
                                dispositionId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == dispoReason).dispositionId;
                            }
                            else
                            {
                                response_string = "disposition not present in callDisposition table";
                            }

                        }

                        var callDispositionDetails = db.calldispotitionautotemplates.FirstOrDefault(m => m.dispositionid_fk == dispositionId && (m.roletype == moduleType || m.roletype == 0));
                        
                        if (callDispositionDetails != null && callDispositionDetails.isenabledsmstemplate && callDispositionDetails.smstemplateid != 0)
                        {
                            smsId = callDispositionDetails.smstemplateid;
                            url = db.smstemplates.FirstOrDefault(m => m.smsId == callDispositionDetails.smstemplateid).smsAPI;
                            customer customer = db.customers.Include("phones").FirstOrDefault(m => m.id == custId);
                            phNum = customer.phones.FirstOrDefault(m => m.isPreferredPhone == true).phoneNumber;
                            parameter = db.smsparameters.FirstOrDefault();
                            string str = @"CALL sendsms(@inwyzuser_id,@invehicle_id,@inlocid,@insmsid,@ininsid);";


                            MySqlParameter[] sqlParameter = new MySqlParameter[]
                            {
                                       new MySqlParameter("@inwyzuser_id",wyzId.ToString()),
                                       new MySqlParameter("@invehicle_id",vehicleId.ToString()),
                                       new MySqlParameter("@inlocid",workshopId),
                                       new MySqlParameter("@insmsid",callDispositionDetails.smstemplateid),
                                       new MySqlParameter("@ininsid","0"),
                            };
                            finalSmsMessage = db.Database.SqlQuery<string>(str, sqlParameter).FirstOrDefault();

                            //phNum = "+91" + phNum;
                            smsinteraction.mobileNumber = phNum;

                            List<string> phNUmbers = new List<string>();

                            if (phNum.Contains(','))
                            {
                                phNUmbers = phNum.Split(',').ToList();
                            }
                            else
                            {
                                phNUmbers.Add(phNum);
                            }

                            foreach (var phoneNumber in phNUmbers)
                            {
                                try
                                {

                                    List<smstemplate> template = db.smstemplates.ToList();
                                    APIURL = url + parameter.phone + "=" + phoneNumber + "&" + parameter.message + "=" + finalSmsMessage + "&" + parameter.senderid + "=" + template.FirstOrDefault(m => m.smsId == smsId).dealerName;
                                    smsinteraction.apiurl = APIURL;
                                    finalPhoneNumber = finalPhoneNumber + "," + phoneNumber;

                                    WebRequest request = WebRequest.Create(APIURL);
                                    HttpWebRequest httpWebRequest = (HttpWebRequest)request;
                                    httpWebRequest.Method = "GET";
                                    httpWebRequest.Accept = "application/json";
                                    HttpWebResponse response = null;
                                    response = (HttpWebResponse)httpWebRequest.GetResponse();

                                    response_string = string.Empty;
                                    using (Stream strem = response.GetResponseStream())
                                    {
                                        StreamReader sr = new StreamReader(strem);
                                        response_string = sr.ReadToEnd();

                                        sr.Close();
                                    }
                                    smsinteraction.reason = "Send Successfully";
                                }
                                catch (Exception ex)
                                {
                                    smsinteraction.smsStatus = false;
                                    smsinteraction.reason = "Error while sending";
                                    if (ex.Message.Contains("inner exception"))
                                    {
                                        response_string = ex.InnerException.Message;
                                    }
                                    else
                                    {
                                        response_string = ex.Message;
                                    }
                                }
                                finally
                                {
                                    smsinteraction.mobileNumber = finalPhoneNumber;
                                    smsinteraction.smsType = smsId.ToString();
                                    smsinteraction.smsMessage = finalSmsMessage;
                                    
                                    
                                    smsinteraction.responseFromGateway = response_string;
                                }
                            }
                        }
                        else
                        {
                            smsinteraction.mobileNumber = finalPhoneNumber;
                            smsinteraction.smsType = smsId.ToString();
                            smsinteraction.smsMessage = finalSmsMessage;
                            smsinteraction.smsStatus = false;
                            smsinteraction.reason = "Template id is not activated in CallDisposition Auto templates";
                            smsinteraction.responseFromGateway = response_string;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("inner exception"))
                        {
                            response_string = ex.InnerException.Message;

                        }
                        else
                        {
                            response_string = ex.Message;
                        }
                        smsinteraction.mobileNumber = finalPhoneNumber;
                        smsinteraction.smsType = smsId.ToString();
                        smsinteraction.smsMessage = finalSmsMessage;
                        smsinteraction.smsStatus = false;
                        smsinteraction.reason = "Error while sending";
                        smsinteraction.responseFromGateway = response_string;
                    }                   
                    logger.Info("\n\n AutoSMS Code Ended  : vehicle Id -  " + vehicleId + "  SMS Type -  + smsId +   DateTime - " + DateTime.Now);
                    db.smsinteractions.Add(smsinteraction);
                    db.SaveChanges();
                }
            }


            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        logger.Info("\n\n -------- AutoSMS OutBlock Exception --------\n" + ex.InnerException.InnerException.Message);
                    }
                    else
                    {
                        logger.Info("\n\n -------- AutoSMS OutBlock Exception --------\n" + ex.InnerException.Message);
                    }
                }
                else
                {
                    logger.Info("\n\n -------- AutoSMS OutBlock Exception --------\n" + ex.Message);

                }

            }
              logger.Info("\n\n AutoSMS Code Ended: " + DateTime.Now);

        }
         
    }
}