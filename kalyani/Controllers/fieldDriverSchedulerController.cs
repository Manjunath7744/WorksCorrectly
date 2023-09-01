using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoSherpa_project.Models;
using AutoSherpa_project.Models.ViewModels;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace AutoSherpa_project.Controllers
{
    [AuthorizeFilter]
    public class fieldDriverSchedulerController : Controller
    {
        #region Kalyani Driver Scheduling

        // GET: fieldDriverScheduler
        [HttpGet]
        public ActionResult DriverScheduler()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    long wyzUIserId = Convert.ToInt32(Session["UserId"].ToString());
                    long? workshopId = db.wyzusers.FirstOrDefault(m => m.id == wyzUIserId).workshop_id;
                    var ddlworkshops = db.workshops.Where(m => m.id == workshopId).Select(m => new { workshopid = m.id, name = m.workshopName }).OrderBy(m => m.name).ToList();
                    ViewBag.ddlworkshops = ddlworkshops;
                }
            }
            catch (Exception ex)
            {

            }



            return View();
        }

        public ActionResult getdriverservicebookedDetails(string fromDateNew, string toDateNew, long cityIs, long statusIs, string lastDispo)
        {
            List<bookedserviceDriver> driverbookeddetails = new List<bookedserviceDriver>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (lastDispo == null)
                    {
                        lastDispo = "";
                    }
                    string str = @"CALL driverSchedulingData(@infromdate,@intodate,@inwshopid,@instatusid,@inlastdispo,@intypeOfPickup);";

                    MySqlParameter[] sqlParameter = new MySqlParameter[]
                    {
                        new MySqlParameter("infromdate",fromDateNew),
                        new MySqlParameter("intodate", toDateNew),
                        new MySqlParameter("inwshopid", cityIs),
                        new MySqlParameter("instatusid", statusIs),
                        new MySqlParameter("inlastdispo", lastDispo),
                        new MySqlParameter("intypeOfPickup", "")
                };
                    driverbookeddetails = db.Database.SqlQuery<bookedserviceDriver>(str, sqlParameter).ToList();

                    var result = Json(new { data = driverbookeddetails, exceptin = "" }, JsonRequestBehavior.AllowGet);
                    result.MaxJsonLength = int.MaxValue;
                    return result;
                }

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        return Json(new { data = driverbookeddetails, exceptin = ex.InnerException.InnerException.Message });
                    }
                    else
                    {
                        return Json(new { data = driverbookeddetails, exceptin = ex.InnerException.Message });
                    }

                }
                else
                {
                    return Json(new { data = driverbookeddetails, exceptin = ex.Message });
                }
            }
            return null;
        }

        public ActionResult driverListScheduleByWorkshopId(long workshopId, string scheduleDate)
        {
            try
            {
                DateTime sDate = Convert.ToDateTime(scheduleDate);
                List<PickupDropDataOnTabLoad> DriverList = new List<PickupDropDataOnTabLoad>();

                using (var db = new AutoSherDBContext())
                {
                    List<wyzuser> DriverListData = db.wyzusers.Where(m => m.workshop_id == workshopId && m.role == "Driver").ToList();
                    List<bookingdatetime> timeSlot = db.bookingdatetimes.ToList();
                    foreach (wyzuser dl in DriverListData)
                    {
                        PickupDropDataOnTabLoad pickup = new PickupDropDataOnTabLoad();
                        if (db.drivers.Count(u => u.wyzUser_id == dl.id && u.isactive == true)>0)
                        {

                            driver ins = db.drivers.Where(u => u.wyzUser_id == dl.id && u.isactive == true).FirstOrDefault();
                            long id = ins.id;
                            if (db.servicebookeds.Any(x => x.driver_id == id))
                            {
                                List<TimeSpan> datesList = new List<TimeSpan>();

                                var pickupidList = db.pickupdrops.Where(m => m.driver_id == id && m.pickupDate == sDate).ToList();
                                foreach (var pickupid in pickupidList)
                                {
                                    var pickupListOfTodayFrom = db.pickupdrops.FirstOrDefault(u => u.id == pickupid.id && u.pickupDate == sDate);
                                    var pickupListOfTodayTo = db.pickupdrops.FirstOrDefault(u => u.id == pickupid.id && u.pickupDate == sDate);
                                    if (pickupListOfTodayFrom != null && pickupListOfTodayTo != null)
                                    {
                                        TimeSpan t1 = pickupListOfTodayFrom.timeFrom ?? default(TimeSpan);
                                        TimeSpan t2 = pickupListOfTodayFrom.timeTo ?? default(TimeSpan);

                                        TimeSpan span = t1 - t2;


                                        int start = t1.Hours;
                                        int End = t2.Hours;
                                        int EntriesCount;

                                        string startTime = t1.Hours + ":" + t1.Minutes;
                                        string endTime = t2.Hours + ":" + t2.Minutes;

                                        TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));
                                        EntriesCount = duration.Hours;
                                        EntriesCount = EntriesCount + EntriesCount;
                                        if (duration.Minutes == 30)

                                        {
                                            EntriesCount = EntriesCount + 1;
                                        }

                                        TimeSpan StartTime = TimeSpan.FromHours(start);
                                        int Difference = 30; //In minutes.
                                        int j;
                                        if (t1.Minutes == 0)
                                        {
                                            j = 0;
                                            //EntriesCount=EntriesCount + 1;
                                        }
                                        else
                                        {
                                            j = 1;
                                            EntriesCount = EntriesCount + 1;
                                        }

                                        Dictionary<TimeSpan, TimeSpan> Entries = new Dictionary<TimeSpan, TimeSpan>();
                                        for (int i = j; i < EntriesCount; i++)
                                        {
                                            Entries.Add(StartTime.Add(TimeSpan.FromMinutes(Difference * i)), StartTime.Add(TimeSpan.FromMinutes(Difference * i)));

                                        }
                                        foreach (var e in Entries)
                                        {
                                            datesList.Add(e.Key);
                                        }
                                    }

                                    pickup.listTime = datesList;


                                }
                            }

                            pickup.id = ins.id;
                            pickup.userName = ins.driverName;
                            DriverList.Add(pickup);
                        }
                    }
                    SchedularDataOnTabLoad driverData = new SchedularDataOnTabLoad();
                    return Json(new { DriverList, timeSlot, JsonRequestBehavior.AllowGet });
                }
            }

            catch (Exception ex)
            {
            }

            return Json(new { });
        }
        public ActionResult assignDriver(string selected_camps, long? startTime, long? formTime, long agentId, string dateIs)
        {
            long WyzUserId = Convert.ToInt64(Session["UserId"].ToString());
            long? bookingId = Convert.ToInt64(selected_camps);
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    servicebooked service_Booked = db.servicebookeds.FirstOrDefault(u => u.serviceBookedId == bookingId);
                    long vehicleId = Convert.ToInt64(service_Booked.vehicle_vehicle_id);
                    long custId = Convert.ToInt64(service_Booked.customer_id);
                    //if (service_Booked.pickupDrop_id != null)
                    //{
                    //    pickupdrop pick = db.pickupdrops.FirstOrDefault(m => m.id == service_Booked.pickupDrop_id);
                    //    pick.driver = null;
                    //    db.pickupdrops.AddOrUpdate(pick);
                    //}
                    pickupdrop pick_up = db.pickupdrops.FirstOrDefault(m => m.id == service_Booked.pickupDrop_id);
                    driver driver_link = db.drivers.FirstOrDefault(u => u.id == agentId && u.isactive == true);
                    pick_up.driver_id = driver_link.id;
                    formTime = formTime + 1;
                    bookingdatetime bookingFrom = db.bookingdatetimes.FirstOrDefault(x => x.id == startTime);
                    bookingdatetime bookingTo = db.bookingdatetimes.FirstOrDefault(x => x.id == formTime);
                    pick_up.timeFrom = bookingFrom.startTime;
                    pick_up.timeTo = bookingTo.startTime;
                    pick_up.pickUpAddress = service_Booked.serviceBookingAddress;
                    pick_up.pickupDate = Convert.ToDateTime(dateIs);
                    db.pickupdrops.AddOrUpdate(pick_up);

                    service_Booked.driver_id = driver_link.id;
                    service_Booked.isPickupRequired = true;
                    //  service_Booked.pickupdrop=pick_up;
                    db.servicebookeds.AddOrUpdate(service_Booked);
                    db.SaveChanges();

                    sendSMSDriverCustomer(service_Booked, agentId);
                    //new CallLoggingController().autosmsday(WyzUserId, vehicleId, custId, "Booking Driver", "Service", 0, agentId, 0, "");

                    return Json(new { success = true });

                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false });

            }
            return Json(new { success = false });
        }

        public ActionResult getSMSTemplateMessage(int smsId, int locId, long vehicleId, string typeOfDispo)
        {

            //Session["smsId"] = smsId;
            //Session["locId"] = locId;
            string WyzUserId = Session["UserId"].ToString();
            try
            {
                //smstemplate template = new smstemplate();
                using (var db = new AutoSherDBContext())
                {
                    string str = @"CALL sendsms(@inwyzuser_id,@invehicle_id,@inlocid,@insmsid,@ininsid);";

                    MySqlParameter[] sqlParameter = new MySqlParameter[]
                    {
                       new MySqlParameter("@inwyzuser_id",WyzUserId),
                       new MySqlParameter("@invehicle_id",vehicleId),
                       new MySqlParameter("@inlocid",locId.ToString()),
                       new MySqlParameter("@insmsid",smsId.ToString()),
                       new MySqlParameter("@ininsid","0"),
                    };

                    string template = db.Database.SqlQuery<string>(str, sqlParameter).FirstOrDefault().ToString();

                    return Json(new { success = true, sms = template });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        //public ActionResult getSMSbyworkshopAndSMSType(long locId, string smstypeid, long custId, long vehiId,string typeOfDispo)
        //{
        //    int UserId = Convert.ToInt32(Session["UserId"].ToString());

        //    try
        //    {
        //        using (var db = new AutoSherDBContext())
        //        {
        //            String userName = getUserProfile().getId();
        //            WyzUser userdata = wyzRepo.getUserbyUserName(userName);

        //            smstemplate template = db.smstemplates.FirstOrDefault(u => u.smsType == smstypeid);

        //            String smsname = smsTrigger_repo.getUpdatedSmsTemplate(userdata.getId(), vehiId, locId, template.getSmsId());

        //            string str = @"CALL sendsms(@inwyzuser_id,@invehicle_id,@inlocid,@insmsid,@ininsid);";

        //            MySqlParameter[] sqlParameter = new MySqlParameter[]
        //            {
        //               new MySqlParameter("@inwyzuser_id",UserId.ToString()),
        //               new MySqlParameter("@invehicle_id",vehiId.ToString()),
        //               new MySqlParameter("@inlocid",locId.ToString()),
        //               new MySqlParameter("@insmsid",()),
        //               new MySqlParameter("@ininsid","0"),
        //            };

        //            string template = db.Database.SqlQuery<string>(str, sqlParameter).FirstOrDefault().ToString();


        //            //logger.info("sms " + smsname);
        //            if (smsname != null)
        //            {
        //                return ok(toJson(smsname));
        //            }
        //            else
        //            {
        //                return ok("Sorry!! No SMS found");

        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //}



        public void sendSMSDriverCustomer(servicebooked service_Booked, long agentId)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    driver driver_link = db.drivers.FirstOrDefault(u => u.id == agentId && u.isactive == true);

                    if (service_Booked.driver != null)
                    {

                        long smsCount1 = db.smstemplates.Count(m => m.smsType == "Booking Driver");


                        if (smsCount1 > 0)
                        {
                            smstemplate smstemp = db.smstemplates.FirstOrDefault(m => m.smsType == "Booking Driver");
                            if (smstemp != null)
                            {
                                if (smstemp.inActive == false)
                                {
                                    bool sucess = sendsmsAPI(service_Booked, smstemp, agentId, "customer", "");
                                }
                            }
                        }

                        long smsCount = db.smstemplates.Count(m => m.smsType == "Driver");
                        if (smsCount > 0)
                        {

                            smstemplate smstemp = db.smstemplates.FirstOrDefault(m => m.smsType == "Driver");
                            if (smstemp != null)
                            {
                                if (smstemp.inActive == false)
                                {
                                    bool sucess = sendsmsAPI(service_Booked, smstemp, agentId, "driver", "");
                                }
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public bool sendsmsAPI(servicebooked service_Booked, smstemplate smstemp, long driverId, string smsTo, string customMsg)
        {
            smsparameter parameter = new smsparameter();
            smsinteraction smsinteraction = new smsinteraction();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string str = @"CALL sendsms(@inwyzuser_id,@invehicle_id,@inlocid,@insmsid,@ininsid);";

                    MySqlParameter[] sqlParameter = new MySqlParameter[]
                    {
                               new MySqlParameter("@inwyzuser_id",service_Booked.wyzUser_id),
                               new MySqlParameter("@invehicle_id",service_Booked.vehicle_vehicle_id),
                               new MySqlParameter("@inlocid",service_Booked.workshop_id),
                               new MySqlParameter("@insmsid",smstemp.smsId),
                               new MySqlParameter("@ininsid","0"),
                    };
                    string message = db.Database.SqlQuery<string>(str, sqlParameter).FirstOrDefault().ToString();


                    parameter = db.smsparameters.FirstOrDefault();

                    string APIURL = string.Empty;
                    string uri = smstemp.smsAPI;
                    //string message = template.smsTemplate1;
                    string phNum = string.Empty;

                    customer customer = db.customers.Include("phones").FirstOrDefault(m => m.id == service_Booked.customer_id);

                    if (smsTo == "customer")
                    {
                        int count = db.phones.Count(m => m.customer_id == service_Booked.customer_id && m.isPreferredPhone == true);
                        if (count > 0)
                        {
                            phNum = db.phones.FirstOrDefault(m => m.customer_id == service_Booked.customer_id && m.isPreferredPhone == true).phoneNumber;
                        }
                    }
                    else if (smsTo == "driver" && driverId != 0)
                    {
                        int count = db.drivers.Count(m => m.id == driverId && m.isactive == true);
                        if (count > 0)
                        {
                            phNum = db.drivers.FirstOrDefault(m => m.id == driverId && m.isactive == true).driverPhoneNum;
                        }
                    }

                    if (customMsg != "" && customMsg != null)
                    {
                        APIURL = uri + parameter.phone + "=" + phNum + "&" + parameter.message + "=" + customMsg + "|" + message + "&" + parameter.senderid + "=" + smstemp.dealerName;
                    }
                    else
                    {
                        APIURL = uri + parameter.phone + "=" + phNum + "&" + parameter.message + "=" + message + "&" + parameter.senderid + "=" + smstemp.dealerName;
                    }
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

                    smsinteraction.interactionDate = DateTime.Now.ToString("dd/MM/yyyy");
                    smsinteraction.interactionDateAndTime = DateTime.Now;
                    smsinteraction.interactionTime = DateTime.Now.ToString("HH:mm:ss");
                    smsinteraction.interactionType = "Text Msg";
                    smsinteraction.responseFromGateway = response_string;
                    smsinteraction.customer_id = customer.id;
                    smsinteraction.vehicle_vehicle_id = service_Booked.vehicle_vehicle_id;
                    smsinteraction.wyzUser_id = Convert.ToInt64(Session["UserId"].ToString());
                    smsinteraction.mobileNumber = phNum;
                    smsinteraction.smsType = smstemp.smsId.ToString();
                    if (customMsg != "" && customMsg != null)
                    {
                        smsinteraction.smsMessage = customMsg + "|" + message;
                    }
                    else
                    {
                        smsinteraction.smsMessage = message;
                    }
                    smsinteraction.isAutoSMS = true;


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
                    db.smsinteractions.Add(smsinteraction);
                    db.SaveChanges();

                }
            }

            catch (Exception ex)
            {
                //using (var db = new AutoSherDBContext())
                //{
                //    smsinteraction.interactionDate = DateTime.Now.ToString("dd-MM-yyyy");
                //    smsinteraction.interactionDateAndTime = DateTime.Now;
                //    smsinteraction.interactionTime = DateTime.Now.ToString("HH:mm:ss");
                //    smsinteraction.interactionType = "Text Msg";

                //    if (ex.Message.Contains("inner exception"))
                //    {
                //        smsinteraction.responseFromGateway = ex.InnerException.Message;
                //    }
                //    else
                //    {
                //        smsinteraction.responseFromGateway = ex.Message;
                //    }


                //    smsinteraction.customer_id = custId;
                //    smsinteraction.vehicle_vehicle_id = vehicleId;
                //    smsinteraction.wyzUser_id = wyzId;
                //    smsinteraction.mobileNumber = phNumber;
                //    smsinteraction.smsType = smsId;
                //    smsinteraction.isAutoSMS = true;
                //    if (customMsg != "" && customMsg != null)
                //    {
                //        smsinteraction.smsMessage = customMsg + "|" + sendingMsg;
                //    }
                //    else
                //    {
                //        smsinteraction.smsMessage = sendingMsg;
                //    }
                //    smsinteraction.isAutoSMS = true;

                //    smsinteraction.smsStatus = true;

                //    if (ex.Message.Contains("inner exception"))
                //    {
                //        smsinteraction.reason = ex.InnerException.Message;
                //    }
                //    else
                //    {
                //        smsinteraction.reason = ex.Message;
                //    }
                //    //smsinteraction.reason = "Send Successfully";

                //    db.smsinteractions.Add(smsinteraction);
                //    db.SaveChanges();
                return false;
                // }
            }
            return false;
        }


        public class bookedserviceDriver
        {
            public string customerName { get; set; }
            public string serviceBookedId { get; set; }
            public string sheduleddate { get; set; }
            public string scheduledtime { get; set; }
            public string vehregNo { get; set; }
            public string creName { get; set; }
            public string serviceBookingAddress { get; set; }
            public string phoneNumber { get; set; }
            public string customer_Id { get; set; }
            public string vehicle_vehicle_id { get; set; }
            public string chassisno { get; set; }
            public string lastDisposition { get; set; }
            public string typeOfPickup { get; set; }
            public string pickupDate { get; set; }
            public string timeFrom { get; set; }
            public string timeTo { get; set; }
            public string driverName { get; set; }
            public string model { get; set; }
            public string serviceType { get; set; }
            public string creremarks { get; set; }
            public string pickupdroptype { get; set; }
            public string workshopname { get; set; }
            public string TimeRage { get; set; }
            public string pickdropid { get; set; }
            public string workshop_id { get; set; }
            public string driverstatus { get; set; }
            public string dstatus { get; set; }
            public string pickupdropstatus { get; set; }
            public string creappoitmenttime { get; set; }
            public string pickupTime { get; set; }

        }

        public ActionResult sendSMS(long vehID, long custId, string smstemplate, long smsId, long bookedId, string smsto)
        {
            long WyzUserId = Convert.ToInt64(Session["UserId"].ToString());
            try
            {
                string phNum = null;
                smstemplate template = new smstemplate();
                smsparameter parameter = new smsparameter();
                using (var db = new AutoSherDBContext())
                {
                    if (smsto == "Driver")
                    {
                        long? driverId = db.servicebookeds.FirstOrDefault(m => m.serviceBookedId == bookedId).driver_id;
                        phNum = db.drivers.FirstOrDefault(m => m.id == driverId && m.isactive == true).driverPhoneNum;
                    }
                    else if (smsto == "Customer")
                    {
                        phNum = db.phones.FirstOrDefault(m => m.customer_id == custId && m.isPreferredPhone).phoneNumber;
                    }


                    template = db.smstemplates.FirstOrDefault(m => m.smsId == smsId);
                    parameter = db.smsparameters.FirstOrDefault();

                    string APIURL = string.Empty;
                    string uri = template.smsAPI;
                    string message = smstemplate;

                    APIURL = uri + parameter.phone + "=" + phNum.Trim() + "&" + parameter.message + "=" + message + "&" + parameter.senderid + "=" + template.dealerName;

                    WebRequest request = WebRequest.Create(APIURL);
                    HttpWebRequest httpWebRequest = (HttpWebRequest)request;
                    httpWebRequest.Method = "GET";
                    httpWebRequest.Accept = "application/json";

                    HttpWebResponse response = null;
                    response = (HttpWebResponse)httpWebRequest.GetResponse();

                    string response_string = string.Empty;
                    using (Stream strem = response.GetResponseStream())
                    {
                        StreamReader sr = new StreamReader(strem);
                        response_string = sr.ReadToEnd();

                        sr.Close();
                    }

                    smsinteraction smsinteraction = new smsinteraction();

                    smsinteraction.interactionDate = DateTime.Now.ToString("dd/MM/yyy");
                    smsinteraction.interactionDateAndTime = DateTime.Now;
                    smsinteraction.interactionTime = DateTime.Now.ToString("HH:mm:ss");
                    smsinteraction.interactionType = "Text Msg";
                    smsinteraction.responseFromGateway = response_string;
                    smsinteraction.customer_id = custId;
                    smsinteraction.vehicle_vehicle_id = custId;
                    smsinteraction.wyzUser_id = WyzUserId;
                    smsinteraction.mobileNumber = phNum;
                    smsinteraction.smsType = template.smsId.ToString();
                    smsinteraction.smsMessage = message;
                    smsinteraction.isAutoSMS = false;


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
                    db.smsinteractions.Add(smsinteraction);
                    db.SaveChanges();
                }

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }


        #endregion


        #region New Driver Scheduling

        [HttpGet]
        public ActionResult ServiceDriverScheduler()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    long wyzUIserId = Convert.ToInt32(Session["UserId"].ToString());
                    ViewBag.defaultWorkshop = db.wyzusers.FirstOrDefault(m => m.id == wyzUIserId).workshop_id;
                    
                    //var listOfRoleId = db.userworkshops.Where(r => r.userWorkshop_id == wyzUIserId).Select(r => r.workshopList_id).ToList();
                    //var ddlworkshops = db.workshops.Where(r => listOfRoleId.Contains(r.id)).Select(m => new { workshopid = m.id, name = m.workshopName }).ToList();
                    var ddlworkshops = db.workshops.Where(m=>m.isinsurance==false).OrderBy(m=>m.workshopName).Select(m => new { workshopid = m.id, name = m.workshopName }).ToList();

                    //long? workshopId = db.wyzusers.FirstOrDefault(m => m.id == wyzUIserId).workshop_id;
                    //var ddlworkshops = db.workshops.Where(m => m.id == workshopId).Select(m => new { workshopid = m.id, name = m.workshopName }).OrderBy(m => m.name).ToList();
                    ViewBag.ddlworkshops = ddlworkshops;
                }
            }
            catch (Exception ex)
            {

            }



            return View();
        }
        public ActionResult assignServiceDriver(string BookingId, string pickupTime, string timeRange, long DriverId, string scheduledate, string pickupType, string pickupdropid)
        {
            using (var db = new AutoSherDBContext())
            {
                string exception = "";
                long WyzUserId = Convert.ToInt64(Session["UserId"].ToString());
                long? bookingId = Convert.ToInt64(BookingId);
                long pickupDrop = Convert.ToInt64(pickupType);
                using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        servicebooked service_Booked = db.servicebookeds.FirstOrDefault(u => u.serviceBookedId == bookingId);
                        long vehicleId = Convert.ToInt64(service_Booked.vehicle_vehicle_id);
                        long custId = Convert.ToInt64(service_Booked.customer_id);

                        if (pickupdropid == "1")
                        {
                            service_Booked.lastallocateddriver_id = service_Booked.driver_id; ;
                            service_Booked.driver_id = DriverId;

                            long BookingDetails_Id = new CallLoggingController().AssignDriverAndBook(Convert.ToDateTime(scheduledate), (Convert.ToDateTime(pickupTime).TimeOfDay).ToString(), 1, service_Booked.typeOfPickup,
                              service_Booked.serviceBookingAddress, service_Booked.serviceBookingDropAddress, WyzUserId, vehicleId, custId, DriverId,
                              service_Booked.driver_id, null, timeRange, service_Booked.workshop_id, Convert.ToInt64(BookingId), db);

                            new CallLoggingController().allocateDriver("Rescheduled", vehicleId, null, DriverId, db, BookingDetails_Id, custId, 1, "Booking", WyzUserId);
                        }
                        else if (pickupdropid == "2")
                        {
                            service_Booked.lastallocateddropdriver_id = service_Booked.dropdriver_id; ;
                            service_Booked.dropdriver_id = DriverId;
                            long BookingDetails_Id = new CallLoggingController().AssignDriverAndBook(Convert.ToDateTime(scheduledate), (Convert.ToDateTime(pickupTime).TimeOfDay).ToString(), 2, service_Booked.typeOfPickup,
                                  service_Booked.serviceBookingAddress, service_Booked.serviceBookingDropAddress, WyzUserId, vehicleId, custId, DriverId,
                                  service_Booked.driver_id, null, timeRange, service_Booked.workshop_id, Convert.ToInt64(BookingId), db);

                            new CallLoggingController().allocateDriver("Rescheduled", vehicleId, null, DriverId, db, BookingDetails_Id, custId, 2, "Booking", WyzUserId);

                        }
                        service_Booked.isPickupRequired = true;
                        db.servicebookeds.AddOrUpdate(service_Booked);
                        db.SaveChanges();

                        sendSMSDriverCustomer(service_Booked, DriverId);
                        dbTransaction.Commit();

                        return Json(new { success = true, exception = exception });
                    }
                    catch (Exception ex)
                    {
                        dbTransaction.Rollback();

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
                        return Json(new { success = false, exception = exception });
                    }


                }

            }

        }

        public ActionResult ManagergetdriverservicebookedDetails(string fromDateNew, string toDateNew, long? cityIs, string statusIs, string lastDispo, string typeofpickup, string statusd, string driverId)
        {
            List<bookedserviceDriver> driverbookeddetails = new List<bookedserviceDriver>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (lastDispo == null)
                    {
                        lastDispo = "";
                    }
                    string str = @"CALL cremanagerdriverSchedulingData(@infromdate,@intodate,@inwshopid,@instatusid,@inlastdispo,@intypeOfPickup,@dstatus,@driverId);";

                    MySqlParameter[] sqlParameter = new MySqlParameter[]
                    {
                        new MySqlParameter("infromdate",fromDateNew),
                        new MySqlParameter("intodate", toDateNew),
                        new MySqlParameter("inwshopid", cityIs),
                        new MySqlParameter("instatusid", statusIs),
                        new MySqlParameter("inlastdispo", lastDispo),
                        new MySqlParameter("intypeOfPickup", typeofpickup),
                        new MySqlParameter("dstatus", statusd),
                        new MySqlParameter("driverId", driverId)
                };
                    driverbookeddetails = db.Database.SqlQuery<bookedserviceDriver>(str, sqlParameter).ToList();

                    var result = Json(new { data = driverbookeddetails, exceptin = "" }, JsonRequestBehavior.AllowGet);
                    result.MaxJsonLength = int.MaxValue;
                    return result;
                }

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        return Json(new { data = driverbookeddetails, exceptin = ex.InnerException.InnerException.Message });
                    }
                    else
                    {
                        return Json(new { data = driverbookeddetails, exceptin = ex.InnerException.Message });
                    }

                }
                else
                {
                    return Json(new { data = driverbookeddetails, exceptin = ex.Message });
                }
            }
            return null;
        }
        public ActionResult cancelServiceDriver(string customerId, string vehicleId, string pickupdropId, string pickupdropType, string typeofPickup)
        {
            try
            {
                int userId = Convert.ToInt32(Session["UserId"].ToString());
                long vehicleIds = Convert.ToInt64(vehicleId);
                long customerIds = Convert.ToInt64(customerId);
                 

                using (var db = new AutoSherDBContext())
                {
                    
                    if (typeofPickup == "Pickup")
                    {
                        new CallLoggingController().allocateDriver("Cancelled", vehicleIds, null, 0, db, 0, customerIds, 1, "Booking", userId);

                    }
                    else if (typeofPickup == "Drop")
                    {
                        new CallLoggingController().allocateDriver("Cancelled", vehicleIds, null, 0, db, 0, customerIds, 2, "Booking", userId);

                    }
                    else if (typeofPickup == "PickupDrop")
                    {
                        new CallLoggingController().allocateDriver("Cancelled", vehicleIds, null, 0, db, 0, customerIds, 1, "Booking", userId);
                        new CallLoggingController().allocateDriver("Cancelled", vehicleIds, null, 0, db, 0, customerIds, 2, "Booking", userId);
                    }
                }
                return Json(new { success = true });


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
                    return Json(new { data = "", exceptin = ex.Message });
                }
                return Json(new { success = false, exception = exception });

            }
        }
        public ActionResult blockscancelbuttons(long servicebookedId)
        {
            string pickupblock = "false";
            string dropblock = "false";
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    var bookingdetails = db.driverBookingDetails.Where(m => m.serviceBookedId == servicebookedId).Select(m => m.id).ToList();
                    var schedulerdata = db.driverSchedulers.Where(m => bookingdetails.Contains(m.driverBookingdetails_id)).Select(m => m.id).ToList();
                    List<long> longs = schedulerdata.ConvertAll(i => (long)i);

                    if (db.driverBookingDetails.Count(m => bookingdetails.Contains(m.id) && m.PickUpDrop == 1) == 0)
                    {
                        pickupblock = "true";
                    }
                    if (db.driverBookingDetails.Count(m => bookingdetails.Contains(m.id) && m.PickUpDrop == 2) == 0)
                    {
                        dropblock = "true";
                    }

                    if (db.driverAppInteraction.Count(m => longs.Contains(m.DriverScheduler_Id) && m.Disposition == "PickUpComplete" && m.IsPickUp) > 0)
                    {
                        pickupblock = "true";
                    }
                    if (db.driverAppInteraction.Count(m => longs.Contains(m.DriverScheduler_Id) && m.Disposition == "DropComplete" && m.IsDrop) > 0)
                    {
                        dropblock = "true";
                    }
                }
                return Json(new { success = true, pickupblock = pickupblock, dropblock = dropblock });


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
                    return Json(new { data = "", exceptin = ex.Message });
                }
                return Json(new { success = false, exception = exception });

            }
        }
        #endregion

        #region display driver details
        public ActionResult listdriverReports()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    var workshops = db.workshops.Where(m=>m.isinsurance==false).Select(m => new { workshopid = m.id, name = m.workshopName }).ToList();
                    ViewBag.ddlworkshops = workshops;

                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        public ActionResult driverdetailsbyworkshop(long? workshopId)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    List<long> wyzusrIdOfUserWorkshop = db.userworkshops.Where(m => m.workshopList_id == workshopId).Select(m => m.userWorkshop_id).ToList();
                    List<long> driverWyzIdList = db.wyzusers.Where(m => wyzusrIdOfUserWorkshop.Contains(m.id) && m.role == "Driver").Select(m => m.id).ToList();

                    var driverList = db.drivers.Where(m => driverWyzIdList.Contains(m.wyzUser_id ?? default(long)) && m.isactive == true).Select(m => new { id = m.id, driverName = m.driverName }).ToList();
                    return Json(new { success = true, driverDetails = driverList });

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
                    return Json(new { data = "", exceptin = ex.Message });
                }
                return Json(new { success = false, exception = exception });

            }
        }

        public ActionResult getPostSalesReports(string PostSalesData)
        {
            string exception = "";
            DataTable PSFReports = new DataTable();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string role = Session["UserRole"].ToString();
                    long userId = Convert.ToInt64(Session["UserId"]);

                    reportFilter filter = new reportFilter();

                    if (PostSalesData != null)
                    {
                        filter = JsonConvert.DeserializeObject<reportFilter>(PostSalesData);
                    }
                    long workshopList = filter.selected_Workshop == null || filter.selected_Workshop=="" ? 0 : Convert.ToInt64(filter.selected_Workshop);


                    string conStr = ConfigurationManager.ConnectionStrings["AutoSherContext"].ConnectionString;

                    using (MySqlConnection connection = new MySqlConnection(conStr))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("Driverappdata", connection))
                        {
                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("inworkshop_id",workshopList));
                            adapter.Fill(PSFReports);
                        }
                    }

                    var results = JsonConvert.SerializeObject(PSFReports, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    var result = Json(new { data = results, exception = exception }, JsonRequestBehavior.AllowGet);
                    result.MaxJsonLength = int.MaxValue;
                    return result;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("inner exception"))
                {
                    if (ex.InnerException.Message.Contains("inner exception"))
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
                var results = JsonConvert.SerializeObject(PSFReports, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                return Json(new { data = results, exception = exception }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}