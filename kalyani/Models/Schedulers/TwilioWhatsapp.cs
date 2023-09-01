using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;


namespace AutoSherpa_project.Models.Schedulers
{
    [DisallowConcurrentExecution]
    public class TwilioWhatsapp : IJob
    {
        Logger logger = LogManager.GetLogger("apkRegLogger");
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {

                long wyzId, vehicleId, custId, dispositionId, workshopId;
                string notRequiredReason = string.Empty, innerRequiredReason = string.Empty, DealerCode = string.Empty, response_string = string.Empty, phNum = string.Empty, finalSmsMessage = string.Empty;
                int moduleType;

                JobDataMap dataMap = context.JobDetail.JobDataMap;

                wyzId = dataMap.GetLong("WyzId");
                vehicleId = dataMap.GetLong("vehicleId");
                custId = dataMap.GetLong("custId");
                dispositionId = dataMap.GetLong("dispositionId");
                notRequiredReason = dataMap.GetString("notRequiredReason");
                innerRequiredReason = dataMap.GetString("innerRequiredReason");
                moduleType = dataMap.GetInt("moduleType");
                workshopId = dataMap.GetLong("workshopId");
                DealerCode = dataMap.GetString("DealerCode");

                using (var db = new AutoSherDBContext())
                {

                    logger.Info("\n\n AutoWhatsapp Code started  : vehicle Id -  " + vehicleId + "  SMS Type - " + dispositionId + "  DateTime - " + DateTime.Now);

                    smsinteraction smsinteraction = new smsinteraction();
                    smsparameter parameter = new smsparameter();

                    try
                    {
                        if (db.authenticationcredentials.Count(m => m.apiname == "ORAIWhatsapp" && m.isactive == true) > 0)
                        {
                           
                            var katariaCredentialsDetails = db.authenticationcredentials.FirstOrDefault(m => m.apiname == "ORAIWhatsapp" && m.isactive == true);
                            smsinteraction.customer_id = custId;
                        smsinteraction.vehicle_vehicle_id = vehicleId;
                        smsinteraction.isAutoSMS = true;
                        smsinteraction.interactionDate = DateTime.Now.ToString();
                        smsinteraction.interactionDateAndTime = DateTime.Now;
                        smsinteraction.interactionTime = DateTime.Now.TimeOfDay.ToString();
                        smsinteraction.interactionType = "Whatsapp Msg";
                        smsinteraction.smsStatus = true;
                        
                        if (!(string.IsNullOrEmpty(notRequiredReason)))
                        {
                            string dispoReason = notRequiredReason.Trim();
                            if (!(string.IsNullOrEmpty(innerRequiredReason)))
                            {
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

                        if (callDispositionDetails != null && callDispositionDetails.isenabledwhatsapptemplate && callDispositionDetails.whatsapptemplateid != 0)
                        {
                                
                                dispositionId = callDispositionDetails.smstemplateid;

                                //phNum = db.phones.FirstOrDefault(m => m.isPreferredPhone == true).phoneNumber;
                                phNum = db.phones.FirstOrDefault(m => m.customer_id == custId && m.isPreferredPhone == true).phoneNumber;
                            phNum = "+91" + phNum;
                            parameter = db.smsparameters.FirstOrDefault();
                            string str = @"CALL sendsms(@inwyzuser_id,@invehicle_id,@inlocid,@insmsid,@ininsid);";
                            MySqlParameter[] sqlParameter = new MySqlParameter[]
                            {
                                       new MySqlParameter("@inwyzuser_id",wyzId.ToString()),
                                       new MySqlParameter("@invehicle_id",vehicleId.ToString()),
                                       new MySqlParameter("@inlocid",workshopId),
                                       new MySqlParameter("@insmsid",callDispositionDetails.whatsapptemplateid),
                                       new MySqlParameter("@ininsid","0"),
                            };
                                finalSmsMessage = db.Database.SqlQuery<string>(str, sqlParameter).FirstOrDefault();
                               //var varsmstemplate = db.Database.SqlQuery<string>(str, sqlParameter).FirstOrDefault();
                               
                            smsinteraction.smsMessage = finalSmsMessage;

                             
                            //phNum = katariaCredentialsDetails.phno;
                            smsinteraction.mobileNumber = phNum;
                        
                            WebRequest request = WebRequest.Create("https://e2ewebservice20190528111726.azurewebsites.net/api/ORAIWhatsappNotification");
                            var httprequest = (HttpWebRequest)request;

                            httprequest.PreAuthenticate = true;
                            httprequest.Method = "POST";
                            httprequest.ContentType = "application/json";
                            httprequest.Accept = "application/json";
                            dynamic requestbody = new JObject();

                                requestbody.From = System.Configuration.ConfigurationManager.AppSettings["From"];
                                //requestbody.From = "+17739853901";

                                requestbody.Body = finalSmsMessage;
                                
                                requestbody.To = phNum;

                                requestbody.AccountSid = System.Configuration.ConfigurationManager.AppSettings["TWILIO_ACCOUNT_SID"];

                                requestbody.AuthToken = System.Configuration.ConfigurationManager.AppSettings["TWILIO_AUTH_TOKEN"];
                                //requestbody.AccountSid = "AC9c40252b3f6083bac701e5d32b3b2dc9";

                                //requestbody.AuthToken = "7c3829eb59e072f8609527580b8bee02";
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
                        }
                        else
                        {
                            smsinteraction.mobileNumber = phNum;
                               
                                smsinteraction.smsType = dispositionId.ToString();
                                
                                smsinteraction.smsMessage = finalSmsMessage;
                                
                                smsinteraction.smsStatus = false;
                               
                                smsinteraction.reason = "Template id is not activated in CallDisposition Auto templates";
                                
                                smsinteraction.responseFromGateway = response_string;
                                
                            }
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
                      
                        smsinteraction.mobileNumber = phNum;
                        smsinteraction.smsType = dispositionId.ToString();
                        smsinteraction.smsMessage = finalSmsMessage;
                        smsinteraction.smsStatus = false;
                        smsinteraction.reason = "Error while sending";
                        smsinteraction.responseFromGateway = response_string;
                    }
                    logger.Info("\n\n AutoWhatsapp Code Ended  : vehicle Id -  " + vehicleId + "  SMS Type -  + smsId +   DateTime - " + DateTime.Now);
                    db.smsinteractions.Add(smsinteraction);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {

            }
           
        }
    }
}