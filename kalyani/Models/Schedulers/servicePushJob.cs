using Newtonsoft.Json;
using NLog;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using AutoSherpa_project.Models;
using AutoSherpa_project.Models.ViewModels;

namespace AutoSherpa_project.Models.Schedulers
{
    public class servicePushJob : IJob
    {
        //Write Scheduler code here
        public async Task Execute(IJobExecutionContext context)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            
            try
            {
                logger.Info("\n INDUS-SMR-Push started at: " + DateTime.Now);
                string siteRoot = HttpRuntime.AppDomainAppVirtualPath;
                if (siteRoot != "/")
                {
                    using (AutoSherDBContext db = new AutoSherDBContext())
                    {
                        dealer baseUrl = db.dealers.FirstOrDefault();

                        schedulers schedulerDetails = db.schedulers.FirstOrDefault(m => m.dealerid == baseUrl.id && m.scheduler_name == "service-push");
                        if(schedulerDetails.isActive==true)
                        {
                            if (!string.IsNullOrEmpty(baseUrl.indusBaseURL) && !string.IsNullOrEmpty(baseUrl.indusServicePassword) && !string.IsNullOrEmpty(baseUrl.indusServiceUserName))
                            {
                                DateTime todaysDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                                long? lastId = 0;
                                servicePush dataInServicePush = db.servicePush.OrderByDescending(x => x.id).FirstOrDefault();
                                if (dataInServicePush != null)
                                {
                                    lastId = dataInServicePush.callHistoryCubeId;
                                }

                                int maxlength = 100;
                                
                                if(schedulerDetails.datalenght!=0)
                                {
                                    maxlength = schedulerDetails.datalenght;
                                }

                                List<callhistorycube> data = db.callhistorycubes.Where(x => (x.typeofpickup == "Pickup Only" ||
                                  x.typeofpickup == "Pick Up Required" || x.typeofpickup == "Door Step Service" || x.typeofpickup == "QWIK Service" ||
                                  x.typeofpickup == "Pickup Drop Required" || x.typeofpickup == "Pickup & MOS" || x.typeofpickup == "Road Side Assistance" ||
                                  x.typeofpickup == "Mobile Service Support") && x.callDate == todaysDate && x.id > lastId).OrderBy(m=>m.id).Skip(0).Take(maxlength).ToList();

                                foreach (var item in data)
                                {
                                    try
                                    {
                                        servicePushVM pushVM = new servicePushVM();
                                        //List<phone> phone = db.phones.Where(x => x.customer_id == item.customer_id && x.phoneNumber != item.preffered_Contact_number).ToList();
                                        string engineNo = item.chassisNo.Substring(0, item.chassisNo.IndexOf('C'));
                                        string chassisNo = item.chassisNo.Substring(item.chassisNo.IndexOf('C'));


                                        pushVM.service_category = item.serviceType;
                                        pushVM.customer_name = item.Customer_name;
                                        pushVM.customer_address_present = item.permenant_address;
                                        pushVM.customer_address_office = item.office_address;
                                        pushVM.model = item.Model;
                                        pushVM.color = item.color;
                                        pushVM.chassis_no = chassisNo;
                                        pushVM.engine_no = engineNo;
                                        pushVM.registration_no = item.Veh_Reg_no;
                                        pushVM.pickup_location = item.pickUpAddress;
                                        pushVM.pickup_date_time = item.scheduledDateTime;
                                        pushVM.mobile_no1 = item.preffered_Contact_number;
                                        //if (phone.Count >= 1)
                                        //{
                                        //    pushVM.mobile_no2 = phone[0].phoneNumber.ToString();
                                        //}
                                        //else
                                        //{
                                        //    pushVM.mobile_no2 = "";
                                        //}
                                        //if (phone.Count >= 2)
                                        //{
                                        //    pushVM.mobile_no3 = phone[1].phoneNumber.ToString();
                                        //}
                                        //else
                                        //{
                                        //    pushVM.mobile_no3 = "";
                                        //}
                                        pushVM.mobile_no2 = "";
                                        pushVM.mobile_no3 = "";

                                        pushVM.workshop_name = item.Booked_workshop;
                                        pushVM.smr_name = item.Cre_Name;
                                        pushVM.other_information = "";

                                        WebRequest request = WebRequest.Create(baseUrl.indusBaseURL);
                                        var httprequest = (HttpWebRequest)request;

                                        httprequest.PreAuthenticate = true;
                                        httprequest.Method = "POST";
                                        httprequest.ContentType = "application/json";


                                        string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(baseUrl.indusServiceUserName + ":" + baseUrl.indusServicePassword));
                                        httprequest.Headers.Add("Authorization", "Basic " + encoded);
                                        httprequest.Accept = "application/json";

                                        using (var streamWriter = new StreamWriter(httprequest.GetRequestStream()))
                                        {
                                            var bodyContent = JsonConvert.SerializeObject(pushVM);
                                            streamWriter.Write(bodyContent);

                                            streamWriter.Flush();
                                            streamWriter.Close();

                                            logger.Info("Sending Service data: \n" + bodyContent + "\n On " + DateTime.Now);
                                        }

                                        HttpWebResponse response = null;
                                        response = (HttpWebResponse)httprequest.GetResponse();

                                        string response_string = string.Empty;
                                        using (Stream strem = response.GetResponseStream())
                                        {
                                            StreamReader sr = new StreamReader(strem);
                                            response_string = sr.ReadToEnd();

                                            sr.Close();
                                        }
                                        servicePush servicePush = new servicePush();
                                        servicePush.callHistoryCubeId = item.id;
                                        servicePush.updateDateAndTime = DateTime.Now;
                                        db.servicePush.Add(servicePush);
                                        db.SaveChanges();
                                    }
                                    catch (Exception ex)
                                    {
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
                                        logger.Info("IndusServicePush(DataLoop) Exception: \n" + exception);
                                    }

                                }
                            }
                            //return Json(new { success = true/*,data=data*/ }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
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
                logger.Info("IndusServicePush(OuterLoop) Exception: \n" + exception);
            }
        }
    }
}