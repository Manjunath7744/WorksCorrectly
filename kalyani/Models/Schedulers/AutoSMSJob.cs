using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoSherpa_project.Models;
using NLog;
using MySql.Data.MySqlClient;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace AutoSherpa_project.Models.Schedulers
{
    public class AutoSMSJob : IJob
    {
        Logger logger = LogManager.GetLogger("apkRegLogger");
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {

                JobDataMap smsParameter = context.JobDetail.JobDataMap;
                int taggingid;
                long wyzId, vehicleId, custId, driverId, departmentId, workshopId, saId;
                string smsType, dispoType, customMsg, DealerCode, phoneNumbers;
                JobDataMap dataMap = context.JobDetail.JobDataMap;

                wyzId = dataMap.GetLong("WyzId");
                vehicleId = dataMap.GetLong("vehicleId");
                custId = dataMap.GetLong("custId");
                driverId = dataMap.GetLong("driverId");
                departmentId = dataMap.GetLong("departmentId");
                workshopId = dataMap.GetLong("workshopId");
                smsType = dataMap.GetString("smsType");
                dispoType = dataMap.GetString("dispoType");
                customMsg = dataMap.GetString("customMsg");
                taggingid = dataMap.GetInt("taggingid");
                DealerCode = dataMap.GetString("DealerCode");
                phoneNumbers = dataMap.GetString("phoneNumbers");

                //wyzId = context.Scheduler.Context.GetLong("WyzId");
                //vehicleId = context.Scheduler.Context.GetLong("vehicleId");
                //custId = context.Scheduler.Context.GetLong("custId");
                //driverId = context.Scheduler.Context.GetLong("driverId");
                //departmentId = context.Scheduler.Context.GetLong("departmentId");

                //workshopId = context.Scheduler.Context.GetLong("workshopId");

                //smsType = context.Scheduler.Context.GetString("smsType");
                //dispoType = context.Scheduler.Context.GetString("dispoType");
                //customMsg = context.Scheduler.Context.GetString("customMsg");
                //taggingid = context.Scheduler.Context.GetInt("taggingid");
                //DealerCode = context.Scheduler.Context.GetString("DealerCode");
                //phoneNumbers = context.Scheduler.Context.GetString("phoneNumbers");



                using (var db = new AutoSherDBContext())
                {

                    logger.Info("\n\n AutoSMS Code started  : vehicle Id -  " + vehicleId + "  SMS Type - " + smsType + "  DateTime - " + DateTime.Now);
                    long moduleType = 0;
                    bool nonContactSmsDay = false;
                    string phNumber = "", smsId = "", sendingMsg = "";
                    smsinteraction smsinteraction = new smsinteraction();

                    if (dispoType == "Service")
                    {
                        moduleType = 1;
                    }
                    else if (dispoType == "Insurance")
                    {
                        moduleType = 2;
                    }
                    else if (dispoType == "PSF")
                    {
                        moduleType = 4;
                    }
                    else if (dispoType == "postsales")
                    {
                        moduleType = 5;
                    }
                    smsparameter parameter = new smsparameter();

                    try
                    {
                        //using (AutoSherDBContext db = new AutoSherDBContext())
                        //{
                        List<smsinteraction> smsInter = new List<smsinteraction>();
                        List<long> nonContactIds = new List<long>();
                        List<string> smsInterDates = new List<string>();
                        List<long> smsIdTypes = new List<long>();
                        DateTime interactionDate = DateTime.Now;

                        if (smsType == "NONCONTACT" || smsType == "INSNONCONTACT")
                        {
                            smsInter = db.smsinteractions.Where(m => m.customer_id == custId && m.vehicle_vehicle_id == vehicleId).ToList();

                            string currentDate = DateTime.Now.ToString("dd-MM-yyyy");
                            long smstype = 0;
                            nonContactIds = db.smstemplates.Where(m => m.smsType == "NONCONTACT" || m.smsType == "INSNONCONTACT").Select(m => m.smsId).ToList();

                            if (smsInter != null && smsInter.Count() > 0)
                            {
                                smsInterDates = smsInter.Select(m => m.interactionDate.Replace('/', '-')).ToList();
                                smsIdTypes = smsInter.Select(m => long.Parse(m.smsType)).ToList();

                                if (smsInterDates.Contains(currentDate))
                                {
                                    foreach (var id in nonContactIds)
                                    {
                                        if (smsIdTypes.Contains(id))
                                        {
                                            nonContactSmsDay = true;
                                            break;
                                        }
                                    }

                                }
                                //interactionDate = Convert.ToDateTime(smsInter.interactionDate.Replace('/', '-'));
                                //smstype = long.Parse(smsInter.smsgType);
                            }
                        }


                        if (nonContactSmsDay == false)
                        {
                            //long wyzLoc = db.wyzusers.FirstOrDefault(m => m.id == wyzId).location_cityId ?? default(long);
                            //
                            smstemplate template = db.smstemplates.FirstOrDefault(m => m.inActive == false && m.smsType == smsType && (m.deliveryType == "Auto" || m.deliveryType == "Both") && (m.moduletype == moduleType || m.moduletype == 3));


                            if (template != null)
                            {

                                if (workshopId == 0)
                                {
                                    if (dispoType == "Service" || dispoType == "PSF" || dispoType == "postsales")
                                    {
                                        workshopId = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehicleId).vehicleWorkshop_id ?? default(long);
                                    }
                                    else if (dispoType == "Insurance")
                                    {
                                        workshopId = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehicleId).insduelocation_id;
                                    }

                                }

                                string str = @"CALL sendsms(@inwyzuser_id,@invehicle_id,@inlocid,@insmsid,@ininsid);";

                                MySqlParameter[] sqlParameter = new MySqlParameter[]
                                {
                                       new MySqlParameter("@inwyzuser_id",wyzId.ToString()),
                                       new MySqlParameter("@invehicle_id",vehicleId.ToString()),
                                       new MySqlParameter("@inlocid",workshopId),
                                       new MySqlParameter("@insmsid",template.smsId),
                                       new MySqlParameter("@ininsid","0"),
                                };
                                string msg = db.Database.SqlQuery<string>(str, sqlParameter).FirstOrDefault().ToString();
                                parameter = db.smsparameters.FirstOrDefault();

                                string APIURL = string.Empty;
                                string uri;
                                if (DealerCode == "AMMOTORS")
                                {
                                     uri = template.smsAPI.Replace("&\n", "&").Replace("&\r", "&");
                                }
                                else
                                {
                                     uri = template.smsAPI;
                                }
                                    
                                //string message = template.smsTemplate1;
                                string phNum = string.Empty;

                                customer customer = db.customers.Include("phones").FirstOrDefault(m => m.id == custId);

                                if (taggingid != 0)
                                {
                                    if (db.taggingusers.Count(m => m.id == taggingid) > 0)
                                    {
                                        phNum = db.taggingusers.FirstOrDefault(m => m.id == taggingid).phoneNumber;
                                    }
                                }
                                //else if (driverId != 0)
                                //{
                                //    phNum = db.drivers.FirstOrDefault(m => m.id == driverId).driverPhoneNum;
                                //}
                                else if (driverId != 0 && dispoType == "Insurance")
                                {
                                    //phNum = db.drivers.FirstOrDefault(m => m.id == driverId).driverPhoneNum;
                                    phNum = db.insuranceagents.FirstOrDefault(m => m.insuranceAgentId == driverId).insuranceAgentNumber;
                                }
                                else if (driverId != 0 && dispoType == "Service")
                                {
                                    if (driverId != 0 && dispoType == "Service" && smsType == "SA Booking")
                                    {
                                        phNum = db.serviceadvisors.FirstOrDefault(m => m.advisorId == driverId).advisorNumber;
                                    }
                                    else
                                    {
                                        phNum = db.drivers.FirstOrDefault(m => m.id == driverId && m.isactive == true).driverPhoneNum;
                                    }
                                    // phNum = db.drivers.FirstOrDefault(m => m.id == driverId).driverPhoneNum;
                                }
                                else if (driverId != 0 && dispoType == "PSF")
                                {
                                    phNum = db.serviceadvisors.FirstOrDefault(m => m.advisorId == driverId).advisorNumber;
                                    
                                }
                                else if (departmentId != 0)
                                {
                                    complainttype compType = db.complainttypes.FirstOrDefault(m => m.id == departmentId);
                                    phNum = db.complainttypes.FirstOrDefault(m => m.id == departmentId).taggedUserNumber;
                                    smsinteraction.smsHeader = compType.taggedUserName + "|" + compType.departmentName;
                                }
                                else if (!string.IsNullOrEmpty(phoneNumbers))
                                {
                                    phNum = phoneNumbers;

                                }
                                else
                                {
                                    phNum = customer.phones.FirstOrDefault(m => m.isPreferredPhone == true).phoneNumber;
                                    if (DealerCode != "AMMOTORS")
                                    {
                                        phNum = "+91" + phNum;
                                    }
                                }

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


                                    if (DealerCode == "HANSHYUNDAI")
                                    {
                                        if (customMsg != "" && customMsg != null)
                                        {
                                            //APIURL = uri + parameter.phone + "=" + phNum + "&" + parameter.message + "=" + customMsg + "|" + msg + "&" + parameter.senderid + "=" + template.dealerName;
                                            APIURL = uri.Replace("MOBILENUMBER", phoneNumber.Trim()).Replace("MESSAGECONTENT", customMsg);
                                        }
                                        else
                                        {
                                            //APIURL = uri + parameter.phone + "=" + phNum + "&" + parameter.message + "=" + msg + "&" + parameter.senderid + "=" + template.dealerName;
                                            APIURL = uri.Replace("MOBILENUMBER", phoneNumber.Trim()).Replace("MESSAGECONTENT", msg);
                                        }
                                    }
                                    //else if(DealerCode == "ADVAITHHYUNDAI" || DealerCode == "CAUVERYFORD" || DealerCode == "MAGNUM")
                                    //{
                                    //    if (customMsg != "" && customMsg != null)
                                    //    {
                                    //        APIURL = uri +parameter.senderid+"="+ template.dealerName +"&" + parameter.phone + "=" + phoneNumber + "&" + parameter.message + "=" + customMsg + "|" + msg + "&template_id=" + template.templateId;
                                    //    }
                                    //    else
                                    //    {
                                    //        APIURL = uri + parameter.senderid + "=" + template.dealerName+"&" + parameter.phone + "=" + phoneNumber + "&" + parameter.message + "=" + msg + "&template_id=" + template.templateId;
                                    //    }
                                    //}
                                    else
                                    {
                                        if (customMsg != "" && customMsg != null)
                                        {
                                            APIURL = uri + parameter.phone + "=" + phoneNumber + "&" + parameter.message + "=" + customMsg + "|" + msg + "&" + parameter.senderid + "=" + template.dealerName;
                                        }
                                        else
                                        {
                                            APIURL = uri + parameter.phone + "=" + phoneNumber + "&" + parameter.message + "=" + msg + "&" + parameter.senderid + "=" + template.dealerName;
                                        }
                                    }


                                    phNumber = phoneNumber;
                                    smsId = template.smsId.ToString();
                                    sendingMsg = msg;

                                    //APIURL = "http://103.16.101.52:8080/sendsms/bulksms?username=autr-autosherpa&password=test123&type=0&dlr=1&destination=8722771489&message=testing_" + custId + "&source=SHERPA";
                                    WebRequest request = WebRequest.Create(APIURL);
                                    HttpWebRequest httpWebRequest = (HttpWebRequest)request;
                                    httpWebRequest.Method = "GET";
                                    httpWebRequest.Accept = "application/json";
                                    //request.Method = "GET";
                                    //request.ContentType = "application/json";

                                    HttpWebResponse response = null;
                                    response = (HttpWebResponse)httpWebRequest.GetResponse();

                                    string response_string = string.Empty;
                                    using (Stream strem = response.GetResponseStream())
                                    {
                                        StreamReader sr = new StreamReader(strem);
                                        response_string = sr.ReadToEnd();

                                        sr.Close();
                                    }

                                    smsinteraction.interactionDate = DateTime.Now.ToString("dd-MM-yyyy");
                                    smsinteraction.interactionDateAndTime = DateTime.Now;
                                    smsinteraction.interactionTime = DateTime.Now.ToString("HH:mm:ss");
                                    smsinteraction.interactionType = "Text Msg";
                                    smsinteraction.responseFromGateway = response_string;
                                    smsinteraction.customer_id = custId;
                                    smsinteraction.vehicle_vehicle_id = vehicleId;
                                    smsinteraction.wyzUser_id = wyzId;
                                    smsinteraction.mobileNumber = phNum;
                                    smsinteraction.smsType = template.smsId.ToString();
                                    if (customMsg != "" && customMsg != null)
                                    {
                                        smsinteraction.smsMessage = customMsg + "|" + msg;
                                    }
                                    else
                                    {
                                        smsinteraction.smsMessage = msg;
                                    }
                                    smsinteraction.isAutoSMS = true;

                                    if (DealerCode == "INDUS" )
                                    {
                                        Regex r = new Regex(@"^\d+$");
                                        if (r.IsMatch(response_string))
                                        {
                                            smsinteraction.smsStatus = true;
                                            smsinteraction.reason = "Send Successfully";
                                        }
                                        else
                                        {
                                            smsinteraction.smsStatus = false;
                                            smsinteraction.reason = "Sending Failed";
                                        }
                                    }
                                    else
                                    {
                                        if (response_string.Contains(parameter.sucessStatus))
                                        {
                                            smsinteraction.smsStatus = true;
                                            smsinteraction.reason = "Send Successfully";
                                        }
                                        else
                                        {
                                            smsstatu status = new smsstatu();

                                            //response_string = "200";

                                            status = db.smsstatus.FirstOrDefault(m => response_string.Contains(m.code));
                                            if (status == null)
                                            {
                                                smsinteraction.smsStatus = false;
                                                smsinteraction.reason = "Sending Failed";
                                            }
                                            else if (status != null)
                                            {
                                                smsinteraction.smsStatus = false;
                                                smsinteraction.reason = status.description;
                                            }
                                        }

                                    }
                                    db.smsinteractions.Add(smsinteraction);
                                    db.SaveChanges();
                                }


                            }
                        }
                        //}
                    }
                    catch (Exception ex)
                    {
                        //using (var dbContext = new AutoSherDBContext())
                        //{
                        smsinteraction.interactionDate = DateTime.Now.ToString("dd-MM-yyyy");
                        smsinteraction.interactionDateAndTime = DateTime.Now;
                        smsinteraction.interactionTime = DateTime.Now.ToString("HH:mm:ss");
                        smsinteraction.interactionType = "Text Msg";

                        if (ex.Message.Contains("inner exception"))
                        {
                            smsinteraction.responseFromGateway = ex.InnerException.Message;
                        }
                        else
                        {
                            smsinteraction.responseFromGateway = ex.Message;
                        }


                        smsinteraction.customer_id = custId;
                        smsinteraction.vehicle_vehicle_id = vehicleId;
                        smsinteraction.wyzUser_id = wyzId;
                        smsinteraction.mobileNumber = phNumber;
                        smsinteraction.smsType = smsId;

                        if (customMsg != "" && customMsg != null)
                        {
                            smsinteraction.smsMessage = customMsg + "|" + sendingMsg;
                        }
                        else
                        {
                            smsinteraction.smsMessage = sendingMsg;
                        }
                        smsinteraction.isAutoSMS = true;

                        smsinteraction.smsStatus = false;

                        if (ex.Message.Contains("inner exception"))
                        {
                            if (ex.InnerException != null)
                            {
                                if (ex.InnerException.Message.ToLower().Contains("insufficient"))
                                {
                                    smsinteraction.reason = "Insufficient Credits, Please Add SMS Credits";
                                }
                                else
                                {
                                    smsinteraction.reason = ex.InnerException.Message;
                                }
                            }
                            else
                            {
                                if (ex.Message.ToLower().Contains("insufficient"))
                                {
                                    smsinteraction.reason = "Insufficient Credits, Please Add SMS Credits";
                                }
                                else
                                {
                                    smsinteraction.reason = ex.Message;
                                }
                            }

                        }
                        else
                        {
                            smsinteraction.reason = ex.Message;
                        }
                        //smsinteraction.reason = "Send Successfully";

                        db.smsinteractions.Add(smsinteraction);
                        db.SaveChanges();
                        //}
                    }


                }
                logger.Info("\n\n AutoSMS Code Ended  : vehicle Id -  " + vehicleId + "  SMS Type - " + smsType + "  DateTime - " + DateTime.Now);

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
            //  logger.Info("\n\n AutoSMS Code Ended: " + DateTime.Now);
        }
    }

}