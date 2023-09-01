using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using System.Windows.Interop;
using AutoSherpa_project.Models;
using AutoSherpa_project.Models.Schedulers;
using AutoSherpa_project.Models.ViewModels;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using Org.BouncyCastle.Asn1.Ocsp;
using Quartz;
using Quartz.Impl;

namespace AutoSherpa_project.Controllers
{
    [AuthorizeFilter]
    public class CallLoggingController : Controller
    {

        //AutoSherDBContext db = null;
        // GET: CallLogging
        [ActionName("Call_Logging"), HttpGet]
        public ActionResult CallLog(string id)
        {
            CallLoggingViewModel callLog = new CallLoggingViewModel();
            try
            {
                //clear360ProfilePrevSession(); // To Clear Session values used in 360 degree form...........
                Session["RoleFor"] = null;
                Session["PageFor"] = null;
                Session["CusId"] = null;
                Session["VehiId"] = null;
                Session["typeOfDispo"] = null;
                Session["appointBookId"] = null;
                Session["isCallInitiated"] = null;
                Session["AndroidUniqueId"] = null;
                Session["GSMUniqueId"] = null;
                Session["DialedNumber"] = null;
                Session["psfDayType"] = null;
                Session["interactionid"] = null;
                Session["MakeCallFrom"] = null;
                Session["isPSFRM"] = null;
                Session["NCReason"] = null;
                bool isMCP = false;

                if (Session["inComingParameter"] != null)
                {
                    id = string.Empty;
                    id = Session["inComingParameter"].ToString();
                    if (Session["DealerCode"].ToString() == "BHANDARIAUTOMOBILE")
                    {
                    Session["inComingParameter"] = null;
                    }
                }
                else
                {

                    if (Session["UserRole"].ToString() != "CREManager" && Session["UserRole"].ToString() != "RM")
                    {
                        TempData["Exceptions"] = "Invalid Url Operation..[CallLogging]....";
                        return RedirectToAction("LogOff", "Home");
                    }
                }



                //Session.Timeout = 90;
                string logFor = string.Empty;
                Dictionary<string, int> userLogData = new Dictionary<string, int>();
                int UserId = Convert.ToInt32(Session["UserId"].ToString());

                callLog.wyzuser = new wyzuser();
                callLog.cust = new customer();
                callLog.vehi = new vehicle();
                callLog.callinteraction = new callinteraction();
                callLog.workshopList = new List<workshop>();
                callLog.allworkshopList = new List<workshop>();
                callLog.locationList = new List<location>();
                callLog.servicetypeList = new List<servicetype>();
                callLog.walkinlocationList = new List<fieldwalkinlocation>();
                callLog.addOnCoversList = new List<addoncover>();
                callLog.renewaltypes = new List<renewaltype>();
                callLog.companiesList = new List<insurancecompany>();
                callLog.Latestservices = new service();
                callLog.LatestInsurance = new insurance();
                callLog.smstemplates = new List<smstemplate>();
                callLog.offerList = new List<specialoffermaster>();
                callLog.insuranceagents = new List<insuranceagent>();
                callLog.tagList = new List<tagginguser>();
                callLog.smrCall = new smrforecasteddata();
                //callLog.citystates = new List<citystate>();
                callLog.psf_Qt_History = new List<psf_qt_history>();
                callLog.listingForm = new ListingForm();
                callLog.servicebooked = new servicebooked();
                callLog.phonesAdd = new List<phone>();
                callLog.listingForm.upsellleads = new List<upselllead>();
                callLog.insudisposition = new insurancedisposition();
                callLog.appointbooked = new appointmentbooked();
                callLog.newCar = new NewCar();
                callLog.srdisposition = new srdisposition();
                callLog.tags = new List<TaggingView>();
                callLog.pickupdrop = new pickupdrop();
                callLog.walkinlocationLists = new List<fieldwalkinlocation>();
                callLog.FieldwalkinlocationList = new List<fieldwalkinlocation>();
                callLog.coupons = new List<coupon_viewdetails>();
                callLog.emailtemplates = new List<emailtemplate>();
                callLog.custPhoneList = new List<phone>();
                callLog.insPolicyDrop = new inspolicydrop();
                callLog.pmscitylist = new List<pmscity>();
                callLog.pmsmodels = new List<pmsmodel>();
                callLog.marutiremarkslist = new List<marutistdremarks>();
                callLog.workshop = new workshop();
                callLog.insuranceexceldata = new insuranceexceldata();//added
                callLog.policyrenewallist = new policyrenewallist();//added

                for (int i = 0; i < 4; i++)
                {

                    callLog.phonesAdd.Add(new phone());
                }

                if (id.Contains(','))
                {
                    //userLogData["BucketId"] = Convert.ToInt32(id.Split(',')[0]);
                    if (id.Split(',')[1] == "S")
                    {
                        ViewBag.typeOfDispo = "Service";
                        logFor = "Service";
                        if (id.Split(',')[0].Contains("_MCP"))
                        {
                            ViewBag.typeOfDispo = "Service_MCP";
                            logFor = "Service";
                            isMCP = true;
                        }
                    }
                    else if (id.Split(',')[1] == "I")
                    {
                        ViewBag.typeOfDispo = "insurance";
                        logFor = "insurance";
                    }
                    string pageFor = "";
                    if (isMCP)
                    {
                        pageFor = "CRE";
                    }
                    else
                    {
                        pageFor = id.Split(',')[0];
                    }

                    Session["CusId"] = Convert.ToInt32(id.Split(',')[2]);
                    Session["VehiId"] = Convert.ToInt32(id.Split(',')[3]);
                    Session["typeOfDispo"] = logFor;


                    //userLogData["CusId"] = Convert.ToInt32(id.Split(',')[2]);
                    //userLogData["VehiID"] = Convert.ToInt32(id.Split(',')[3]);

                    //Session["CurLogDetails"] = userLogData;


                    int cusid = Convert.ToInt32(id.Split(',')[2]);
                    int vehId = Convert.ToInt32(id.Split(',')[3]);
                    ViewBag.vehiId = vehId;


                    callLog.CustomerId = cusid;
                    callLog.VehicleId = vehId;
                    callLog.UserId = UserId;
                    using (var db = new AutoSherDBContext())
                    {
                        if (id.Split(',')[1] == "S")
                        {
                            callLog = get360ProfileData(callLog, db, "Service", Session["DealerCode"].ToString(), Session["OEM"].ToString());
                            Session["VehiReg"] = callLog.vehi.chassisNo;
                            if (isMCP)
                            {
                                ViewBag.typeOfDispo = "Service_MCP";
                            }
                            else
                            {
                                ViewBag.typeOfDispo = "Service";
                            }



                            long assignWyzId = 0;

                            if (pageFor == "Shw" || pageFor == "CRE")
                            {
                                Session["PageFor"] = "CRE";
                                if (pageFor == "Shw")
                                {
                                    Session["MakeCallFrom"] = "ServiceSearch";
                                }
                            }
                            else if (pageFor == "Hdd")
                            {
                                Session["PageFor"] = "Search";
                            }
                            else if (pageFor == "CREManager")
                            {
                                Session["PageFor"] = "CREManager";
                            }

                            //int cnt = db.callinteractions.Where(m => m.vehicle_vehicle_id == vehId && m.customer_id == cusid && m.assignedInteraction_id != null).Count();
                            long countInteraction = db.callinteractions.Count(k => k.vehicle_vehicle_id == vehId && k.customer_id == cusid && k.assignedInteraction_id != null);
                            if (countInteraction > 0)
                            {
                                long lastid = db.callinteractions.Where(k => k.vehicle_vehicle_id == vehId && k.customer_id == cusid && k.assignedInteraction_id != null).Max(model => model.id);
                                if (lastid > 0)
                                {

                                    callLog.callinteraction = db.callinteractions.Include("appointmentbooked").Include("srdispositions").Include("wyzuser").Include("wyzuser.workshop").Include("assignedinteraction").Include("assignedinteraction.wyzuser").Include("assignedinteraction.campaign").Include("srdispositions.calldispositiondata").Include("servicebooked").Include("servicebooked.serviceadvisor").Include("servicebooked.pickupdrop").Include("servicebooked.workshop").FirstOrDefault(m => m.id == lastid);
                                    if (callLog.callinteraction.srdispositions.Count > 0)
                                    {
                                        long srId = callLog.callinteraction.srdispositions.FirstOrDefault().id;

                                        var selectedUpsell = db.upsellleads.Where(m => m.srDisposition_id == srId).Select(m => new { upsellId = m.upsellId, tagTo = m.taggedTo, comment = m.upsellComments }).ToList();

                                        if (selectedUpsell != null || selectedUpsell.Count() >= 0)
                                        {
                                            ViewBag.selectedUpsell = selectedUpsell;
                                        }
                                        else
                                        {
                                            ViewBag.selectedUpsell = null;
                                        }
                                    }
                                    else
                                    {
                                        ViewBag.selectedUpsell = null;
                                    }
                                }
                            }

                            callLog.tags = db.Database.SqlQuery<TaggingView>("select distinct(upsellLeadId) from taggingusers where moduleTypeId=1 or moduleTypeId=5 or moduleTypeId=3;").ToList();
                            List<TaggingView> allTags = db.Database.SqlQuery<TaggingView>("select distinct(upsellLeadId),name,upsellType,wyzUser_id from taggingusers;").ToList();
                            //customer custo = new customer();

                            for (int i = 0; i < callLog.tags.Count(); i++)
                            {
                                callLog.tags[i].name = allTags.FirstOrDefault(m => m.upsellLeadId == callLog.tags[i].upsellLeadId).name;
                                callLog.tags[i].upsellType = allTags.FirstOrDefault(m => m.upsellLeadId == callLog.tags[i].upsellLeadId).upsellType;
                                callLog.tags[i].wyzUserId = allTags.FirstOrDefault(m => m.upsellLeadId == callLog.tags[i].upsellLeadId).wyzUserId;

                            }

                            for (int i = 0; i < callLog.tags.Count(); i++)
                            {
                                callLog.listingForm.upsellleads.Add(new upselllead());
                            }
                        }
                        else if (id.Split(',')[1] == "I")
                        {
                            callLog = get360ProfileData(callLog, db, "Insurance", Session["DealerCode"].ToString(), Session["OEM"].ToString());
                            Session["VehiReg"] = callLog.vehi.chassisNo;
                            ViewBag.typeOfDispo = "insurance";
                            ViewBag.ISFIELDENABLED = db.dealers.FirstOrDefault().isfieldexecutive ? "True" : "False";
                            ViewBag.ISPOLICYFIELDENABLED = db.dealers.FirstOrDefault().ispolicydropexecutive ? "True" : "False";

                            if (pageFor == "Shw" || pageFor == "CRE")
                            {
                                Session["PageFor"] = "CRE";
                                if (pageFor == "Shw")
                                {
                                    Session["MakeCallFrom"] = "InsuranceSearch";
                                }
                            }
                            else if (pageFor == "Hdd")
                            {
                                Session["PageFor"] = "Search";
                            }
                            else if (pageFor == "CREManager")
                            {
                                Session["PageFor"] = "CREManager";
                            }
                            else if (pageFor == "CRE")
                            {
                                Session["PageFor"] = "CRE";
                            }

                            //callLog.smstemplates = getSMSTemplate("Service");
                            long countInteraction = db.callinteractions.Count(k => k.vehicle_vehicle_id == vehId && k.customer_id == cusid && k.insuranceAssignedInteraction_id != null);
                            if (countInteraction > 0)
                            {
                                long lastid = db.callinteractions.Where(k => k.vehicle_vehicle_id == vehId && k.customer_id == cusid && k.insuranceAssignedInteraction_id != null).Max(model => model.id);
                                if (lastid > 0)
                                {
                                    callLog.callinteraction = db.callinteractions.Include("insurancedispositions").Include("srdispositions").Include("vehicle").Include("appointmentbooked").FirstOrDefault(m => m.id == lastid);
                                    //callLog.callinteraction.appointmentbooked.insu
                                    if (callLog.callinteraction.appointmentbooked != null)
                                    {

                                        if (callLog.callinteraction.appointmentbooked.typeOfPickup == "Field")

                                        {    //callLog.
                                            {
                                                if (db.dealers.FirstOrDefault().isfieldexecutive)

                                                    if (db.pickupdrops.Any(m => m.id == callLog.callinteraction.appointmentbooked.pickupDrop_id))
                                                    {

                                                        var fromToins = db.pickupdrops.Where(m => m.id == callLog.callinteraction.appointmentbooked.pickupDrop_id).FirstOrDefault();
                                                        if (fromToins.timeFrom != null && fromToins.timeTo != null)//newly added
                                                        {
                                                            long fromns = db.bookingdatetimes.FirstOrDefault(m => m.startTime == fromToins.timeFrom).id;
                                                            long toins = db.bookingdatetimes.FirstOrDefault(m => m.startTime == fromToins.timeTo).id;

                                                            callLog.listingForm.time_From_insExist = fromns;
                                                            callLog.listingForm.time_To_insExist = toins - 1;
                                                            callLog.listingForm.time_From = fromns;
                                                            callLog.listingForm.time_To_ins = toins - 1;
                                                        }

                                                    }
                                            }
                                        }



                                    }
                                }
                            }
                            if (Session["DealerCode"].ToString() == "INDUS")
                            {
                                int policyduedateCount = db.insuranceassignedinteractions.Count(m => (m.campaign_id == 31 || m.campaign_id == 32) && m.vehicle_vehicle_id == vehId);
                                if (policyduedateCount > 0)
                                {
                                    int policyduedatenullCount = db.insuranceassignedinteractions.Count(m => (m.campaign_id == 31 || m.campaign_id == 32) && m.vehicle_vehicle_id == vehId && m.policyDueDate != null);
                                    if (policyduedatenullCount > 0)
                                    {
                                        DateTime policyDueDate = Convert.ToDateTime(db.insuranceassignedinteractions.FirstOrDefault(m => (m.campaign_id == 31 || m.campaign_id == 32) && m.vehicle_vehicle_id == vehId && m.policyDueDate != null).policyDueDate);
                                        ViewBag.insupulicyDueDate = policyDueDate.ToString("dd-MM-yyyy");
                                    }
                                    else
                                    {
                                        ViewBag.insupulicyDueDate = "";
                                    }
                                }
                            }
                           
                            if (db.insuranceforecasteddatas.Any(m => m.vehicle_id == vehId && m.customer_id == cusid.ToString()))
                            {
                                ViewBag.renewalType = db.insuranceforecasteddatas.FirstOrDefault(m => m.vehicle_id == vehId && m.customer_id == cusid.ToString()).renewaltype;
                            }

                            if (db.insuranceassignedinteractions.Count(m => m.vehicle_vehicle_id == vehId) > 0)
                            {

                                var insassigncoupon = db.insuranceassignedinteractions.FirstOrDefault(m => m.vehicle_vehicle_id == vehId);
                                if (insassigncoupon.campaign_id != null)
                                {
                                    long camp_id = (long)insassigncoupon.campaign_id;
                                    DateTime? policyDueDate = insassigncoupon.policyDueDate;
                                    if (Session["DealerCode"].ToString() == "HARPREETFORD")
                                    {
                                        string str = @"CALL coupon_details(@inwyzuser_id,@policyexpdate);";
                                        MySqlParameter[] sqlParameter = new MySqlParameter[] { new MySqlParameter("inwyzuser_id", UserId), new MySqlParameter("policyexpdate", policyDueDate) };
                                        callLog.coupons = db.Database.SqlQuery<coupon_viewdetails>(str, sqlParameter).ToList();
                                    }
                                    else
                                    {

                                        string str = @"CALL coupon_details(@inwyzuser_id,@policyexpdate,@campagin_id);";
                                        MySqlParameter[] sqlParameter = new MySqlParameter[] { new MySqlParameter("inwyzuser_id", UserId), new MySqlParameter("policyexpdate", policyDueDate), new MySqlParameter("campagin_id", camp_id) };
                                        callLog.coupons = db.Database.SqlQuery<coupon_viewdetails>(str, sqlParameter).ToList();
                                    }
                                }

                            }
                            long insu_ass_id = callLog.callinteraction.insuranceAssignedInteraction_id ?? default(long);


                            callLog.tags = db.Database.SqlQuery<TaggingView>("select distinct(upsellLeadId) from taggingusers where moduleTypeId=2 or moduleTypeId=5;").ToList();
                            List<TaggingView> allTags = db.Database.SqlQuery<TaggingView>("select distinct(upsellLeadId),name,upsellType,wyzUser_id from taggingusers;").ToList();
                            //customer custo = new customer();

                            for (int i = 0; i < callLog.tags.Count(); i++)
                            {
                                callLog.tags[i].name = allTags.FirstOrDefault(m => m.upsellLeadId == callLog.tags[i].upsellLeadId).name;
                                callLog.tags[i].upsellType = allTags.FirstOrDefault(m => m.upsellLeadId == callLog.tags[i].upsellLeadId).upsellType;
                                callLog.tags[i].wyzUserId = allTags.FirstOrDefault(m => m.upsellLeadId == callLog.tags[i].upsellLeadId).wyzUserId;
                            }

                            for (int i = 0; i < callLog.tags.Count(); i++)
                            {
                                callLog.listingForm.upsellleads.Add(new upselllead());
                            }
                        }

                        if (db.callinteractions.Count(k => k.vehicle_vehicle_id == vehId && k.customer_id == cusid && k.appointmentBooked_appointmentId != null && k.insuranceAssignedInteraction_id != null) > 0)
                        // if (callLog.callinteraction != null && callLog.callinteraction.appointmentbooked != null && db.Couponinteractions.Count(m => m.appointmentBookedId == callLog.callinteraction.appointmentbooked.appointmentId) > 0)
                        {
                            long lastid = db.callinteractions.Where(k => k.vehicle_vehicle_id == vehId && k.customer_id == cusid && k.appointmentBooked_appointmentId != null && k.insuranceAssignedInteraction_id != null).Max(model => model.id);
                            if (lastid > 0)
                            {
                                long? appointmentId = db.callinteractions.FirstOrDefault(m => m.id == lastid).appointmentBooked_appointmentId;
                                if (db.Couponinteractions.Count(m => m.appointmentBookedId == appointmentId) > 0)
                                {
                                    var couponInteraction = db.Couponinteractions.FirstOrDefault(m => m.appointmentBookedId == appointmentId);
                                    callLog.coupondetails = couponInteraction.coupondeatails + "(" + ((Convert.ToDateTime(couponInteraction.couponexpirydate)).ToString("dd-MM-yyyy")) + ")";
                                }
                                else
                                {
                                    callLog.coupondetails = db.appointmentbookeds.FirstOrDefault(m => m.appointmentId == appointmentId).coupon;
                                }
                            }
                        }
                        if (Session["UserRole1"].ToString() == "1")
                        {
                            var ddlTagging = db.campaigns.Where(m => m.campaignType == "TaggingSMR" && m.isactive == true && m.campaignName != "ISP" && m.campaignName != "ISP/MCP/EW" && m.campaignName != "ISP/MCP" && m.campaignName != "ISP/EW").Select(model => new { id = model.id, TaggingCamp = model.campaignName }).ToList();
                            ViewBag.ddltagging = ddlTagging;
                        }
                        else
                        {
                            var ddlTagging = db.campaigns.Where(m => m.campaignType == "TaggingINS" && m.isactive == true && m.campaignName != "ISP" && m.campaignName != "ISP/MCP/EW" && m.campaignName != "ISP/MCP" && m.campaignName != "ISP/EW").Select(model => new { id = model.id, TaggingCamp = model.campaignName }).ToList();
                            ViewBag.ddltagging = ddlTagging;
                        }

                        // removing ToIntCast
                        callLog.selectedTagList = callLog.cust.leadtag?.Split(',')?.ToList();
                        //callLog.selectedTagList = callLog.cust.leadtag?.Split(',')?.Select(s => Convert.ToInt32(s))?.ToList();

                        //getEdited Policy Due Date

                        if (db.insuranceassignedinteractions.FirstOrDefault(v => v.vehicle_vehicle_id == vehId) is insuranceassignedinteraction insAssingdInteractions)
                        {
                            callLog.PolicyEditedDueDate = insAssingdInteractions.PolicyDueDateEditedIA?.ToString("dd-MM-yyyy") ?? "";
                        }
                        else if (db.vehicles.FirstOrDefault(v => v.vehicle_id == vehId) is vehicle vehicleDetails)
                        {
                            callLog.PolicyEditedDueDate = vehicleDetails.PolicyDueDateEdited?.ToString("dd-MM-yyyy") ?? "";
                        }


                    }
                }

                else
                {
                    Session.RemoveAll();
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception ex)
            {
                TempData["Exceptions"] = ex.Message.ToString();
                if (ex.Message.Contains("inner exception"))
                {
                    TempData["Exceptions"] = ex.InnerException.Message;
                }
                TempData["ControllerName"] = "Call Log";
                return RedirectToAction("LogOff", "Home");
            }


            return View(callLog);
        }

        [ActionName("UpdateAssignedManualDate"), HttpPost]
        public ActionResult UpdateAssingedManualDate([System.Web.Http.FromBody] long vehicleId, [System.Web.Http.FromBody] string manualAssignedDate)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {

                long vehId = vehicleId;
                logger.Info("Entered UpdateAssignedManualDate");
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    // update vehicle table
                    var vehicle = db.vehicles.FirstOrDefault(v => v.vehicle_id == vehId);
                    if (vehicle != null)
                    {
                        vehicle.PolicyDueDateEdited = DateTime.ParseExact(manualAssignedDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        vehicle.updateddate = DateTime.Now;
                        vehicle.updatecrename = Session["UserName"].ToString();
                        db.vehicles.AddOrUpdate(vehicle);
                        db.SaveChanges();
                    }

                    var insuranceAssignInteraction = db.insuranceassignedinteractions.FirstOrDefault(m => m.vehicle_vehicle_id == vehId);
                    if (insuranceAssignInteraction != null)
                    {
                        insuranceAssignInteraction.PolicyDueDateEditedIA = DateTime.ParseExact(manualAssignedDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                        insuranceAssignInteraction.PolicyDueDateUpdatedDate = DateTime.Now;

                        insuranceAssignInteraction.PolicyDueDateUpdatedByUser = Session["UserName"].ToString();
                        db.insuranceassignedinteractions.AddOrUpdate(insuranceAssignInteraction);
                        db.SaveChanges();
                    }

                    var insuranceforecasteddata = db.insuranceforecasteddatas.FirstOrDefault(m => m.vehicle_id == vehId);
                    if (insuranceforecasteddata != null)
                    {
                        insuranceforecasteddata.duedateEdited = DateTime.ParseExact(manualAssignedDate, "dd-MM-yyyy", CultureInfo.InvariantCulture); ;
                        insuranceforecasteddata.PolicyDueDateUpdatedByUser = Session["UserName"].ToString();
                        insuranceforecasteddata.PolicyDueDateUpdatedDate = DateTime.Now;
                        db.insuranceforecasteddatas.AddOrUpdate(insuranceforecasteddata);
                        db.SaveChanges();
                    }

                }
                Response.StatusCode = 200;
                return Json("Data Updated");

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                logger.Error(ex, "error occured while updating manual due date");
                logger.Error(ex.InnerException, "inner exception");
                return Json("Unable to update data");
            }

        }

        public CallLoggingViewModel get360ProfileData(CallLoggingViewModel callLog, AutoSherDBContext db, string DispoType, string DealerCode, string OEM)
        {
            callLog.ispsfDynamic = false;
            long UserId = callLog.UserId;
            long vehId = callLog.VehicleId, cusid = callLog.CustomerId;
            string ceiStatus = "";
                   
            callLog.wyzuser = db.wyzusers.Include("location").Include("workshop ").SingleOrDefault(m => m.id == UserId);
            callLog.cust = db.customers.Include("emails").Include("addresses").Include("vehicles").Include("insurances").Include("servicebookeds").FirstOrDefault(m => m.id == cusid);
            callLog.vehi = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehId);
            callLog.pmscitylist = db.Pmscity.ToList();
            callLog.pmsmodels = db.Pmsmodels.ToList();
            callLog.cust.emails = callLog.cust.emails.OrderByDescending(m => m.isPreferredEmail == true).ToList();
            callLog.cust.addresses = callLog.cust.addresses.OrderByDescending(m => m.isPreferred == true).ToList();
            callLog.cust.servicebookeds = callLog.cust.servicebookeds.OrderByDescending( m => m.serviceBookedType != null || m.serviceBookedType == null ).ToList();
            callLog.cust.appointmentbookeds = callLog.cust.appointmentbookeds.OrderByDescending( m => m.appointmentDate!= null || m.appointmentDate == null ).ToList();
            callLog.cust.appointmentbookeds = callLog.cust.appointmentbookeds.OrderByDescending( m => m.appointmentFromTime!= null || m.appointmentFromTime == null ).ToList();
            callLog.cust.insuranceassignedinteractions = callLog.cust.insuranceassignedinteractions.OrderByDescending( m => m.policyDueDate!= null || m.policyDueDate == null ).ToList();
            //callLog.custPhoneList = db.Database.SqlQuery<phone>("select * from phone p join(select max(phone_Id)phid from phone where customer_id=@custId group by phoneNumber)Ph on ph.phid=p.phone_Id and p.customer_id=@custId order by phone_Id desc Limit 0,5", new MySqlParameter("@custId", cusid)).ToList();
            callLog.custPhoneList = db.Database.SqlQuery<phone>("select * from phone p join(select max(phone_Id)phid from phone where customer_id=@custId group by phoneNumber)Ph on ph.phid=p.phone_Id and p.customer_id=@custId order by phone_Id desc Limit 0,5", new MySqlParameter("@custId", cusid)).ToList();
            //callLog.wyzUserMngPhn = db.wyzusers.FirstOrDefault(m => m.userName == callLog.wyzuser.creManager)?.phoneNumber;
            //callLog.wyzUserMngPhn = db.workshops.FirstOrDefault(m => m.id == ).workshopPhone;
            var chassisno = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehId).chassisNo;
            
            if (db.insuranceexceldatas.Any(m=>m.chassisNo== chassisno))
            {
                callLog.insuranceexceldata = db.insuranceexceldatas.FirstOrDefault(m => m.chassisNo == chassisno);
            }
          if(db.Policyrenewallists.Any(m=>m.chassisno==chassisno))
            {
                callLog.policyrenewallist = db.Policyrenewallists.FirstOrDefault(m => m.chassisno == chassisno);
            }
               
            
            callLog.custPhoneList = callLog.custPhoneList.OrderByDescending(m => m.isPreferredPhone == true).ToList();
            //callLog.coupons = db.Coupons.Where(m => m.isactive).ToList();
            callLog.listingForm.issmsEnabled = db.dealers.FirstOrDefault().issmsenabled;

            //if (Session["DealerCode"].ToString() == "AMMOTORS" && !string.IsNullOrEmpty(callLog.vehi.saleDate.ToString()))
            //{

            //    DateTime saledateIncrement = Convert.ToDateTime(callLog.vehi.saleDate.ToString());

            //    callLog.vehi.OEMWarrentyDate = saledateIncrement.AddYears(2).AddDays(-1);
            //}

            //callLog.coupons = db.Coupons.Where(m => m.isactive).ToList();


            if (db.services.Any(m => m.vehicle_vehicle_id == vehId))
            {
               var workshopid = db.services.FirstOrDefault(m => m.vehicle_vehicle_id == vehId).workshop_id;
               
                callLog.workshop = db.workshops.FirstOrDefault(m => m.id == workshopid);

            }
            callLog.followupReasons = db.FollowupReasons.Where(m => m.isActive).OrderBy(m => m.followUpReason).ToList();
            
            if (DealerCode == "KATARIA")
            {
                callLog.marutiremarkslist = db.GetMarutistdremarks.Where(m => m.isActive).OrderBy(m => m.stdremarks).ToList();
            }
            //string ceiStatus = db.Database.SqlQuery<string>("select if(saledate between  date_format(date_sub((select billdate from service where vehicle_vehicle_id=vehicle_id  order by id desc limit 0,1),interval 36 month),'%Y-%m-01') and  date_sub((select billdate from service where vehicle_vehicle_id=vehicle_id  order by id desc limit 0,1),interval 12 month),'CEI ','NONCEI ')   from vehicle where  vehicle_id=@id;", new MySqlParameter("@id", vehId)).FirstOrDefault();
            //string ceiStatus = db.Database.SqlQuery<string>("select if(saledate between date_sub(curdate(), interval 3 year)  and date_sub(curdate(), interval 1 year),'CEI','NONCEI') as custcat   from vehicle where  vehicle_id=@id;", new MySqlParameter("@id", vehId)).FirstOrDefault();
            if (DispoType == "PSF")
            {
                callLog.ispsfDynamic = db.dealers.FirstOrDefault().isPsfDynamic;
                ceiStatus = db.Database.SqlQuery<string>("select if (iscei=1,'CEI', 'NONCEI')  as custcat  from psfassignedinteraction where  vehicle_vehicle_id=@id order by id desc ;", new MySqlParameter("@id", vehId)).FirstOrDefault();

            }
            else
            {
                if (DealerCode == "INDUS")
                {
                    ceiStatus = db.Database.SqlQuery<string>("select if(saledate between date_format(date_sub((select max(BillDate) from service where vehicle_vehicle_id=vehicle_id),interval 36 month),'%Y-%m-01') and last_day(date_sub((select max(BillDate) from service where vehicle_vehicle_id=vehicle_id),interval 12 month))and   lastServiceType regexp 'PMS|paid|PS|Free|FS1|FR2|FR3|FR4|FS2|FS3|periodical|periodic|FR|running|RR|TV1|TV2|TV3','CEI',if(saledate between  date_format(date_sub((select max(BillDate) from service where vehicle_vehicle_id=vehicle_id),interval 60 month),'%Y-%m-01') and last_day((select max(BillDate) from service where vehicle_vehicle_id=vehicle_id))and   lastServiceType regexp 'BANDP','CEI','NONCEI'))as custcat from vehicle where vehicle_id  = @id;", new MySqlParameter("@id", vehId)).FirstOrDefault();
                }
                else
                {
                    //ceiStatus = db.Database.SqlQuery<string>("select if (saledate between date_format(date_sub((select max(BillDate) from service where vehicle_vehicle_id= vehicle_id),interval 36 month),'%Y-%m-01') and last_day(date_sub((select max(BillDate) from service where vehicle_vehicle_id= vehicle_id),interval 12 month))and lastServiceType regexp 'PMS|paid|PS|Free|FS1|FR2|FR3|FR4|FS2|FS3|periodical|periodic|FR|TV1|TV2|TV3','CEI','NONCEI')as custcat", new MySqlParameter("@id", vehId)).FirstOrDefault();
                    //ceiStatus = db.Database.SqlQuery<string>("select if (saledate between date_format(date_sub((select max(BillDate) from service where vehicle_vehicle_id= vehicle_id),interval 36 month),'%Y-%m-01') and last_day(date_sub((select max(BillDate) from service where vehicle_vehicle_id= vehicle_id),interval 12 month))and  (select max(lastServiceType) from service where vehicle_vehicle_id= vehicle_id ) regexp 'PMS|paid|PS|Free|FS1|FR2|FR3|FR4|FS2|FS3|periodical|periodic|FR|TV1|TV2|TV3','CEI','NONCEI')as custcat    from vehicle where vehicle_id = @id;", new MySqlParameter("@id", vehId)).FirstOrDefault();
                    ceiStatus = db.Database.SqlQuery<string>("select if(saledate between date_sub(curdate(), interval 3 year)  and date_sub(curdate(), interval 1 year),'CEI','NONCEI') as custcat   from vehicle where  vehicle_id=@id;", new MySqlParameter("@id", vehId)).FirstOrDefault();
                }

            }

            callLog.ceicustCat = ceiStatus;
            if (DispoType != "PostSalesFeedback")
            {
                if (DealerCode == "INDUS")
                {
                    List<PsfPullOut> psfpulloutDetails = new List<PsfPullOut>();
                    callLog.psfPullOuts = db.Database.SqlQuery<PsfPullOut>("select jobCardNumber as JobCardNo,(select campaignname from campaign where id =campaign_id) as  PSFDay,psfassignedInteraction_id as CubeId, if(psfstatus='Satisfied(RoboCall)',1,0) as roboStatus,1 as isrobocall  from psfassignmentrecords where vehicle_id=" + vehId + ";").ToList();
                    if (callLog.psfPullOuts == null || callLog.psfPullOuts.Count == 0)
                    {
                        callLog.psfPullOuts = db.Database.SqlQuery<PsfPullOut>("select jobCardNumber as JobCardNo,psfCallingDayType as PSFDay,max(id) as CubeId  from psfcallhistorycube where vehicle_vehicle_id=" + vehId + " group by psfCallingDayType,jobCardNumber;").ToList();
                    }
                }
                else
                {
                    callLog.psfPullOuts = db.Database.SqlQuery<PsfPullOut>("select jobCardNumber as JobCardNo,psfCallingDayType as PSFDay,max(id) as CubeId from psfcallhistorycube where vehicle_vehicle_id=" + vehId + " group by psfCallingDayType,jobCardNumber;").ToList();
                }
            }


            var Viewcitystates = db.citystates.Select(h => new { state = h.state }).OrderBy(m => m.state).Distinct().ToList();
            var newList = Viewcitystates.OrderBy(x => x.state).ToList(); // ToList optional

            callLog.citystatesList = new List<SelectListItem>();
            callLog.stateList = new List<SelectListItem>();

            foreach (var city in newList)
            {
                callLog.citystatesList.Add(new SelectListItem { Text = city.state, Value = city.state });
                callLog.stateList.Add(new SelectListItem { Text = city.state, Value = city.state });
            }


            if (DealerCode == "PAWANHYUNDAI")
            {
                if (callLog.vehi.saleDate != null)
                {
                    DateTime salesDate = Convert.ToDateTime(callLog.vehi.saleDate);
                    double totalDays = (DateTime.Now.Date - salesDate.Date).TotalDays;
                    if (totalDays <= 60)
                    {
                        callLog.CustCategory = "CX1";
                    }
                    else if (totalDays >= 61 && totalDays <= 1095)
                    {
                        callLog.CustCategory = "GOLD";
                    }
                    else if (totalDays >= 1096 && totalDays <= 1825)
                    {
                        callLog.CustCategory = "PLATINUM";
                    }
                    else if (totalDays >= 1826 && totalDays <= 2555)
                    {
                        callLog.CustCategory = "DIAMOND";
                    }
                    else if (totalDays > 2556)
                    {
                        callLog.CustCategory = "ABOVE 7 YRS";
                    }
                }
                else
                {
                    callLog.CustCategory = "SaleNA";
                }

            }

            List<phone> phone = db.phones.Where(m => m.customer_id == cusid).ToList();
            List<email> emails = db.emails.Where(m => m.customer_id == cusid).ToList();
            List<address> addresses = db.addresses.Where(m => m.customer_Id == cusid).ToList();

            //********************** Diff ^||******************

            callLog.workshopList = db.workshops.Where(m => m.workshopName == db.wyzusers.FirstOrDefault(K => K.id == UserId).location.name).ToList();
            callLog.servicetypeList = db.servicetypes.Where(m => m.isActive).ToList();
            if (DispoType == "Service" || DispoType == "Insurance")
            {

                callLog.walkinlocationList = db.fieldwalkinlocations.ToList();
                callLog.addOnCoversList = db.addoncovers.ToList();
                callLog.renewaltypes = db.renewaltypes.ToList();
                callLog.companiesList = db.insurancecompanies.ToList();

                callLog.walkinlocationLists = db.fieldwalkinlocations.Where(m => m.typeOfLocation == "2").ToList();
                callLog.FieldwalkinlocationList = db.fieldwalkinlocations.Where(m => m.typeOfLocation == "1").ToList();
            }


            if (DealerCode != "HARPREETFORD" && DealerCode != "HANSHYUNDAI"/* && DealerCode != "GALAXYTOYOTA"*/)
            {
                callLog.smstemplates = getSMSTemplate(DispoType);
            }


            long countOfServicePresent = db.services.Where(m => m.vehicle_vehicle_id == vehId && m.lastServiceDate != null).Count();

            //latest Service Getting
            if (countOfServicePresent != 0)
            {
                long lastId = 0;

                if (OEM == "MARUTI SUZUKI")
                {
                    var latestService = db.services.Where(m => m.vehicle_vehicle_id == vehId && m.lastServiceDate != null).OrderByDescending(x => x.billDate).FirstOrDefault();
                    lastId = latestService.id;
                }
                else
                {
                    var latestService = db.services.Where(m => m.vehicle_vehicle_id == vehId && m.lastServiceDate != null).OrderByDescending(x => x.jobCardDate).FirstOrDefault();
                    lastId = latestService.id;
                }

                if (DispoType == "PSF")
                {
                    List<service> serviceList = db.services.Include("psfassignedinteractions").Where(m => m.vehicle.vehicle_id == vehId && m.lastServiceDate != null).ToList();

                    if (serviceList.Count() != 0)
                    {
                        DateTime maxDate = Convert.ToDateTime(serviceList.Max(m => m.lastServiceDate));
                        callLog.lastService = serviceList.FirstOrDefault(m => m.lastServiceDate == maxDate);
                    }


                    //Hibernate.initialize(lastService.getPsfAssignedInteraction());
                    //modeOfServiceExisting = getLatestServiceBookedType(cusid,vehId, callLog.lastService);
                }

                //long lastId = latestservice.id;
                //long lastId = db.services.Where(m => m.vehicle_vehicle_id == vehId && m.lastServiceDate != null).Max(k => k.id);

                callLog.Latestservices = db.services.Include("workshop").SingleOrDefault(m => m.id == lastId);

            }

            if (db.insurances.Where(m => m.vehicle != null && m.vehicle.vehicle_id == vehId).Count() != 0)
            {
                //long lastInsurence = db.insurances.Include("workshop").Where(m => m.vehicle != null && m.vehicle.vehicle_id == vehId).Max(k => k.id);
                //callLog.LatestInsurance = db.insurances.SingleOrDefault(m => m.id == lastInsurence);

                var lastInsurence = db.insurances.Where(m => m.vehicle != null && m.vehicle.vehicle_id == vehId).OrderByDescending(k => k.policyIssueDate).FirstOrDefault();

                if (lastInsurence != null)
                {
                    callLog.LatestInsurance = lastInsurence;
                }

            }

            //List<string> complaintOFCust = new List<string>();
            //complaintOFCust.Add("");
            //complaintOFCust.Add("");

            //callLog.complaintOFCust = complaintOFCust;

            //List<String> complaintOFCust = call_int_repo.ComplaintStatusandCount(customerData.Id());


            if (db.smrforecasteddatas.Any(m => m.vehicle_id == vehId))
            {
                callLog.smrCall = db.smrforecasteddatas.SingleOrDefault(m => m.vehicle_id == vehId);
            }

            callLog.tagList = db.taggingusers.Where(m => m.upsellLeadId != 0 && (m.moduleTypeId == 3 || m.moduleTypeId == 1)).Distinct().ToList();




            callLog.emailtemplates = getEmailTemplateList(DispoType);
            if (DispoType == "Service" || DispoType == "Insurance")
            {
                callLog.locationList = getLocationByDispoType(DispoType);

                if (DispoType == "Service")
                {
                    callLog.allworkshopList = db.workshops.Where(m => m.isinsurance == false).ToList();
                }
                else
                {
                    callLog.allworkshopList = db.workshops.Where(m => m.isinsurance == true).ToList();
                }
            }
            else
            {
                var userWorkShopList = db.userworkshops.Where(m => m.userWorkshop_id == UserId).Select(m => m.workshopList_id).ToList();
                callLog.workshopList = db.workshops.Where(m => userWorkShopList.Contains(m.id)).OrderByDescending(m => m.workshopName).ToList();
                callLog.allworkshopList = db.workshops.OrderBy(m => m.workshopName).ToList();
                callLog.locationList = db.locations.ToList();
            }

            callLog.lastPSFAssignStatus = getLastPSFAssignStatusOfVehicle(Convert.ToInt32(vehId));
            callLog.finaldispostion = db.calldispositiondatas.FirstOrDefault(m => m.id == callLog.lastPSFAssignStatus.finalDisposition_id);

            if (DispoType == "PostSalesFeedback")
            {

                if (DealerCode == "INDUS")
                {
                    callLog.psfPullOuts = db.Database.SqlQuery<PsfPullOut>("select psfCallingDayType as PSFDay,max(id) as CubeId from postsalescallhistory where calldispositiondata_id IN(22,44) AND vehicle_vehicle_id=" + vehId + " group by psfCallingDayType;").ToList();
                }


                if (db.Postsalesassignedinteractions.Count(m => m.id == callLog.postsalesinteractionId) > 0)
                {
                    string postsalesDate = string.Empty;
                    postsalesassignedinteraction postassigninter = db.Postsalesassignedinteractions.FirstOrDefault(m => m.id == callLog.postsalesinteractionId);
                    if (postassigninter != null)
                    {
                        callLog.postsalesAssignedCRE = db.wyzusers.FirstOrDefault(m => m.id == postassigninter.wyzUser_id).userName;
                        callLog.postsalesAssignedCampaign = db.campaigns.FirstOrDefault(m => m.id == postassigninter.campaign_id).campaignName;
                        if (postassigninter.salesworkshop_id != 0)
                        {
                            if (db.Salesworkshops.Count(m => m.workshop_id == postassigninter.salesworkshop_id) > 0)
                            {
                                callLog.postsalesAssignedworkshop = db.Salesworkshops.FirstOrDefault(m => m.workshop_id == postassigninter.salesworkshop_id).salesWorkshopname;
                            }
                        }
                        else
                        {
                            callLog.postsalesAssignedworkshop = "-";
                        }

                        if (db.campaigns.Count(m => m.id == postassigninter.campaign_id) > 0)
                        {
                            callLog.postsalesDayName = db.campaigns.FirstOrDefault(m => m.id == postassigninter.campaign_id).campaignName;

                        }
                        if (!(string.IsNullOrEmpty(callLog.postsalesDayName)) && callLog.postsalesDayName == "PostSales 2nd Day")
                        {
                            if (postassigninter.psf1 != null)
                            {
                                postsalesDate = Convert.ToDateTime(postassigninter.psf1).ToString("dd-MM-yyyy");
                            }
                            else
                            {
                                postsalesDate = "-";
                            }
                        }
                        if (!(string.IsNullOrEmpty(callLog.postsalesDayName)) && callLog.postsalesDayName == "PostSales 15th Day")
                        {
                            if (postassigninter.psf1 != null)
                            {
                                postsalesDate = Convert.ToDateTime(postassigninter.psf2).ToString("dd-MM-yyyy");
                            }
                            else
                            {
                                postsalesDate = "-";
                            }
                        }
                        if (!(string.IsNullOrEmpty(callLog.postsalesDayName)) && callLog.postsalesDayName == "PostSales 30th Day")
                        {
                            if (postassigninter.psf1 != null)
                            {
                                postsalesDate = Convert.ToDateTime(postassigninter.psf3).ToString("dd-MM-yyyy");
                            }
                            else
                            {
                                postsalesDate = "-";
                            }
                        }
                        callLog.postsalesAssignedDate = postsalesDate;
                    }

                }
            }


            if (callLog.smrCall != null && callLog.smrCall.nextServiceDate != null)
            {
                //DateTime.Now.ToShortTimeString
                long diff = Convert.ToInt32(DateTime.Now.ToLocalTime().Hour) - Convert.ToInt32(Convert.ToDateTime(callLog.smrCall.nextServiceDate).ToLocalTime().Hour);
                long diffDays = diff / (24 * 60 * 60 * 1000);

                //long years = (diffDays / 365);
                long months = (diffDays / 30);
                //long days = (diffDays % 365) % 7;

                //String disp = String.valueOf(years) + "Y " + String.valueOf(months) + "M " + String.valueOf(days)+ "D";
                string disp = months.ToString() + "M";
                callLog.noShowCall = disp;
            }
            else
            {

                callLog.noShowCall = "";
            }
           

            #region Box View AssignCre,UpdatedDate,Workshop and Campaign for all(S,I,P)

            //Assign CRE Data workshop ect -------------------------> Service
            if (db.assignedinteractions.Count(m => m.customer_id == cusid && m.vehical_Id == vehId) > 0)
            {
                assignedinteraction assignInter = db.assignedinteractions.Include("campaign").Include("wyzuser").FirstOrDefault(m => m.customer_id == cusid && m.vehical_Id == vehId);
                //if (callLog.callinteraction.assignedinteraction != null)
                //{
                if (assignInter.wyzuser != null)
                {
                    callLog.AssignCRE = assignInter.wyzuser.userName;
                    if (assignInter.location_id != 0 && assignInter.location_id != null)
                    {
                        callLog.AssignWorkShop = db.workshops.FirstOrDefault(m => m.id == assignInter.location_id).workshopName;
                    }
                    else
                    {
                        callLog.AssignWorkShop = "-";
                    }
                }
                else
                {
                    callLog.AssignCRE = "Not Assigned";
                    callLog.AssignWorkShop = "-";
                }

                if (assignInter.uplodedCurrentDate == null)
                {
                    callLog.UploadedDate = "-";
                }
                else
                {
                    callLog.UploadedDate = Convert.ToDateTime(assignInter.uplodedCurrentDate).ToString("dd-MM-yyyy");
                }


                if (assignInter.nextServiceDate != null)
                {
                    callLog.NextServiceDate = Convert.ToDateTime(assignInter.nextServiceDate).ToString("dd-MM-yyyy");
                }

                Regex r = new Regex(@"^\d+$");
                if (!string.IsNullOrEmpty(assignInter.nextServiceType))
                {
                    if (r.IsMatch(assignInter.nextServiceType))
                    {
                        long ServiceTypeId = long.Parse(assignInter.nextServiceType);
                        servicetype type = callLog.servicetypeList.FirstOrDefault(m => m.id == ServiceTypeId);
                        if (type != null)
                        {
                            callLog.NextServiceType = type.serviceTypeName;
                        }
                    }
                    else
                    {
                        //TempData["Exceptions"] = "Invalid NextServiceType";
                        //return RedirectToAction("LogOff", "Home");
                        callLog.NextServiceType = "Invalid NextService";
                    }
                }

                if (assignInter.campaign != null)
                {
                    callLog.AssignCampaign = assignInter.campaign.campaignName;
                }
                else
                {
                    callLog.AssignCampaign = "-";
                }


                if (DealerCode == "BRIDGEWAYMOTORS" && DispoType == "Service")
                {
                    List<long> tagging_ids = new List<long>();
                    List<long> zero = new List<long>() { 0 };
                    tagging_ids = db.taggedassignments.Where(m => m.vehicle_id == assignInter.vehical_Id && m.active == true).Select(m => m.tagging_id).ToList();
                    var campaignid = db.campaigns.Where(m => tagging_ids.Contains(m.id)).FirstOrDefault();
                    if (tagging_ids != null && tagging_ids != zero && campaignid != null)
                    {
                        foreach (var tagging_id in tagging_ids)
                        {

                            List<campaign> campaignName = db.campaigns.Where(m => m.id == tagging_id).ToList();
                            if (campaignName.Count() != 0)
                            {
                                callLog.AssignTaggingName = db.campaigns.FirstOrDefault(m => m.id == tagging_id).campaignName;
                            }

                            else
                            {
                                callLog.AssignTaggingName = "-";
                            }
                        }
                    }

                    else
                    {
                        callLog.AssignTaggingName = "-";
                    }
                }

                else
                {
                    if (assignInter.tagging_id != null && assignInter.tagging_id != 0)
                    {
                        if (db.campaigns.Any(m => m.id == assignInter.campaign_id))
                        {
                            callLog.AssignTaggingName = db.campaigns.FirstOrDefault(m => m.id == assignInter.tagging_id).campaignName;
                        }
                        else
                        {
                            callLog.AssignTaggingName = "-";
                        }
                    }
                    else
                    {
                        callLog.AssignTaggingName = "-";
                    }
                }
            }

            //Assign CRE Data Workshop ect --------------------------->Insurance
            if (db.insuranceassignedinteractions.Count(m => m.customer_id == cusid && m.vehicle_vehicle_id == vehId) > 0)
            {
                insuranceassignedinteraction interasction = db.insuranceassignedinteractions.Include("wyzuser").Include("campaign").Include("wyzuser.workshop").FirstOrDefault(m => m.customer_id == cusid && m.vehicle_vehicle_id == vehId);
                if (interasction.wyzuser != null)
                {
                    callLog.InsAssignCRE = interasction.wyzuser.firstName + "(" + interasction.wyzuser.userName + ")";
                    if (interasction.location_id != 0 && interasction != null)
                    {
                        //if (DealerCode == "PODDARCARWORLD" || DealerCode == "SHIVAMHYUNDAI")
                        //{
                            if (db.workshops.Count(m => m.id == interasction.location_id) > 0)
                            {
                                callLog.InsAssignWorkShop = db.workshops.FirstOrDefault(m => m.id == interasction.location_id).workshopName;
                            }
                            else
                            {
                                callLog.InsAssignWorkShop = "-";
                            }
                       // }
                        //else
                        //{

                            //if (db.workshops.Count(m => m.location_cityId == interasction.location_id) > 0)
                            //{
                            //    callLog.InsAssignWorkShop = db.workshops.FirstOrDefault(m => m.location_cityId == interasction.location_id).workshopName;
                            //}
                            //else
                            //{
                            //    callLog.InsAssignWorkShop = "-";
                            //}
                        //}
                    }
                    else
                    {
                        callLog.InsAssignWorkShop = "-";
                    }

                }
                else
                {
                    callLog.InsAssignCRE = "Not Assigned";
                    callLog.InsAssignWorkShop = "-";
                }

                if (interasction.uplodedCurrentDate == null)
                {
                    callLog.InsUploadedDate = "-";
                }
                else
                {
                    callLog.InsUploadedDate = Convert.ToDateTime(interasction.uplodedCurrentDate).ToString("dd-MM-yyyy");
                }

                if (interasction.policyDueDate != null)
                {
                    callLog.PolicyDueDate = Convert.ToDateTime(interasction.policyDueDate).ToString("dd-MM-yyyy");
                }
                else
                {
                    callLog.PolicyDueDate = "-";
                }


                if (interasction.campaign != null)
                {
                    callLog.InsAssignCampaign = interasction.campaign.campaignName;
                }
                else
                {
                    callLog.InsAssignCampaign = "-";
                }


                if (interasction.tagging_id != null && interasction.tagging_id != 0)
                {
                    if (db.campaigns.Any(m => m.id == interasction.campaign_id))
                    {
                        callLog.InsAssignTaggingName = db.campaigns.FirstOrDefault(m => m.id == interasction.tagging_id).campaignName;
                    }
                    else
                    {
                        callLog.InsAssignTaggingName = "-";
                    }
                }
                else
                {
                    callLog.InsAssignTaggingName = "-";
                }
            }

            //Assign CRE Data Workshop ect --------------------------->PSF
            if (db.psfassignedinteractions.Count(m => m.vehicle_vehicle_id == vehId) > 0)
            {
                List<psfassignedinteraction> psfAssign = new List<psfassignedinteraction>();
                psfAssign = db.psfassignedinteractions.Include("campaign").Include("wyzuser").Where(m => m.vehicle_vehicle_id == vehId).ToList();

                if (psfAssign != null && psfAssign.Count > 0)
                {
                    callLog.psfboxview = new List<psfBoxView>();

                    foreach (var psfEle in psfAssign)
                    {
                        psfBoxView psfView = new psfBoxView();

                        //if (psfEle.campaign_id == typeOfPSF && psfEle.wyzUser_id == UserId && psfEle.customer_id == cid)
                        //{
                        //    lastworkshopId = psfEle.workshop_id ?? default(long);
                        //}

                        if (psfEle.wyzuser != null)
                        {
                            psfView.wyzUser = psfEle.wyzuser.userName;
                        }
                        else
                        {
                            psfView.wyzUser = "Not Assigned";
                        }

                        if (psfEle.campaign != null)
                        {
                            psfView.campaignName = psfEle.campaign.campaignName;
                        }
                        else
                        {
                            psfView.campaignName = "-";
                        }

                        if (psfEle.workshop_id != null && psfEle.workshop_id != 0)
                        {
                            psfView.workshopName = db.workshops.FirstOrDefault(m => m.id == psfEle.workshop_id).workshopName;
                        }
                        else
                        {
                            psfView.workshopName = "-";
                        }

                        if (psfEle.uplodedCurrentDate != null)
                        {
                            psfView.updateedDate = Convert.ToDateTime(psfEle.uplodedCurrentDate).ToString("dd-MM-yyyy");
                        }
                        else
                        {
                            psfView.updateedDate = "-";
                        }

                        callLog.psfboxview.Add(psfView);
                    }
                }
            }
            #endregion

            #region Offers Tags
            if (OEM == "MARUTI SUZUKI")
            {
                if (callLog.vehi.saleDate != null && callLog.Latestservices != null && callLog.Latestservices.lastServiceDate != null)
                {
                    DateTime vehicleSaleDate = Convert.ToDateTime(callLog.vehi.saleDate);
                    DateTime LastserviceDate = Convert.ToDateTime(callLog.Latestservices.lastServiceDate);
                    if ((vehicleSaleDate.AddYears(3) > DateTime.Now.Date) && (LastserviceDate.Month > 18 || LastserviceDate.Month < 24))
                    {
                        callLog.offers = "Car Care";
                    }
                    else if ((vehicleSaleDate.AddYears(3) > DateTime.Now.Date) && (LastserviceDate.Month > 24))
                    {
                        callLog.offers = "WA & WB";
                    }

                    else if (vehicleSaleDate.AddYears(5) < DateTime.Now.Date && (LastserviceDate.Month > 18 || LastserviceDate.Month < 24))
                    {
                        callLog.offers = "Fixed Cost";

                    }
                    else if (vehicleSaleDate.AddYears(5) < DateTime.Now.Date && (LastserviceDate.Month > 24))
                    {
                        callLog.offers = "Fixed Cost";

                    }
                    else if (((vehicleSaleDate.AddYears(3) > vehicleSaleDate) && vehicleSaleDate.AddYears(5) > DateTime.Now.Date) && (LastserviceDate.Month > 18 || LastserviceDate.Month < 24))
                    {
                        callLog.offers = "WA & WB";
                    }
                    else if (((vehicleSaleDate.AddYears(3) > vehicleSaleDate) && (vehicleSaleDate.AddYears(5) > DateTime.Now.Date)) && (LastserviceDate.Month > 24))
                    {
                        callLog.offers = "Fixed Cost";
                    }

                }
                else
                {
                    callLog.offers = "-";
                }

            }
            #endregion
            return callLog;
        }


        public List<location> getLocationByDispoType(string typeOfDispo)
        {
            List<location> locations = new List<location>();
            using (var db = new AutoSherDBContext())
            {
                if (typeOfDispo == "Service")
                {
                    locations = db.locations.Where(m => m.moduleType == "1" || m.moduleType == "3").OrderBy(m => m.name).ToList();
                }
                else if (typeOfDispo == "Insurance")
                {
                    locations = db.locations.Where(m => m.moduleType == "2" || m.moduleType == "3").OrderBy(m => m.name).ToList();
                }
            }
            return locations;
        }

        public string assignDispoCRE(string dispoType)
        {
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            long userId = Convert.ToInt32(Session["UserId"].ToString());

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        long campId = db.campaigns.FirstOrDefault(m => m.campaignName == "Customer Search").id;
                        long managerId = db.wyzusers.FirstOrDefault(m => m.userName == db.wyzusers.FirstOrDefault(x => x.id == userId).creManager && m.role == "CREManager").id;


                        if (dispoType == "Service")
                        {
                            assignedinteraction assign = new assignedinteraction();

                            assign.callMade = "No";
                            assign.displayFlag = false;
                            assign.uplodedCurrentDate = DateTime.Now;
                            assign.campaign_id = campId;
                            assign.customer_id = cusId;
                            assign.vehical_Id = vehiId;
                            assign.wyzUser_id = userId;
                            //assign.nextServiceDate = DateTime.Now.AddDays(2);
                            assign.assigned_wyzuser_id = userId;
                            assign.assigned_manager_id = managerId;

                            if (db.smrforecasteddatas.Any(m => m.vehicle_id == vehiId))
                            {
                                smrforecasteddata smrForecast = db.smrforecasteddatas.FirstOrDefault(m => m.vehicle_id == vehiId);

                                if (smrForecast.workshop_id != null && smrForecast.workshop_id != 0)
                                {
                                    assign.location_id = smrForecast.workshop_id;
                                }
                                else
                                {
                                    assign.location_id = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehiId).vehicleWorkshop_id;
                                }
                                if (Session["DealerCode"].ToString() == "KATARIA")
                                {
                                    assign.nextServiceDate = DateTime.Now.AddDays(2);
                                }
                                else
                                {
                                    assign.nextServiceDate = smrForecast.nextServiceDate;
                                }
                                if (smrForecast.ServiceTypeId == null || smrForecast.ServiceTypeId == 0)
                                {
                                    assign.nextServiceType = "0";
                                }
                                else
                                {
                                    assign.nextServiceType = Convert.ToString(smrForecast.ServiceTypeId ?? default(int));
                                }

                            }
                            else
                            {
                                vehicle veh = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehiId);
                                assign.location_id = veh.vehicleWorkshop_id;

                                assign.nextServiceDate = DateTime.Now.AddDays(2);
                                assign.nextServiceType = "0";
                            }


                            db.assignedinteractions.Add(assign);
                            db.SaveChanges();


                            assignedcallsreport assignCalls = new assignedcallsreport();

                            assignCalls.assignInteractionID = assign.id;
                            assignCalls.assignedDate = DateTime.Now;
                            assignCalls.assignmentType = "Service";
                            assignCalls.moduletypeId = 1;
                            assignCalls.uploadId = "0";
                            assignCalls.vehicleId = vehiId;
                            assignCalls.wyzuserId = userId;
                            assignCalls.campaignId = campId;
                            assignCalls.assigned_manager_id = managerId;


                            if (db.smrforecasteddatas.Any(m => m.vehicle_id == vehiId))
                            {
                                smrforecasteddata smrForecast = db.smrforecasteddatas.FirstOrDefault(m => m.vehicle_id == vehiId);

                                if (db.servicetypes.Any(m => m.serviceTypeName == smrForecast.nextservicetype))
                                {
                                    assignCalls.dueType = db.servicetypes.FirstOrDefault(m => m.serviceTypeName == smrForecast.nextservicetype).id;
                                }

                                assignCalls.dueDate = smrForecast.nextServiceDate;
                            }
                            else
                            {
                                vehicle veh = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehiId);
                                if (db.servicetypes.Any(m => m.serviceTypeName == veh.nextServicetype))
                                {
                                    assignCalls.dueType = db.servicetypes.FirstOrDefault(m => m.serviceTypeName == veh.nextServicetype).id;
                                }

                                assignCalls.dueDate = veh.nextServicedate;
                            }
                            assignCalls.isautoassigned = false;

                            db.assignedcallsreports.Add(assignCalls);
                            db.SaveChanges();
                        }
                        else if (dispoType == "Insurance")
                        {
                            insuranceassignedinteraction insu_assign = new insuranceassignedinteraction();

                            insu_assign.callMade = "No";
                            insu_assign.displayFlag = false;
                            insu_assign.uplodedCurrentDate = DateTime.Now;
                            insu_assign.customer_id = cusId;
                            insu_assign.vehicle_vehicle_id = vehiId;
                            insu_assign.wyzUser_id = userId;
                            insu_assign.campaign_id = campId;
                            insu_assign.policyDueDate = DateTime.Now.AddDays(2);

                            if (db.insuranceforecasteddatas.Any(m => m.vehicle_id == vehiId))
                            {
                                insuranceforecasteddata insForecast = new insuranceforecasteddata();

                                if (insForecast.location_id != 0)
                                {
                                    insu_assign.location_id = insForecast.location_id;
                                }
                                else
                                {
                                    insu_assign.location_id = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehiId).insduelocation_id;
                                }
                            }
                            else
                            {
                                vehicle veh = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehiId);
                                insu_assign.location_id = veh.insduelocation_id;
                            }


                            db.insuranceassignedinteractions.Add(insu_assign);
                            db.SaveChanges();


                            assignedcallsreport assignCalls = new assignedcallsreport();

                            assignCalls.assignInteractionID = insu_assign.id;
                            assignCalls.assignedDate = DateTime.Now;
                            assignCalls.assignmentType = "Service";
                            assignCalls.moduletypeId = 1;
                            assignCalls.uploadId = "0";
                            assignCalls.vehicleId = vehiId;
                            assignCalls.wyzuserId = userId;
                            assignCalls.campaignId = campId;
                            assignCalls.assigned_manager_id = managerId;

                            if (db.insuranceforecasteddatas.Any(m => m.vehicle_id == vehiId))
                            {
                                insuranceforecasteddata insForecast = new insuranceforecasteddata();

                                insForecast = db.insuranceforecasteddatas.FirstOrDefault(m => m.vehicle_id == vehiId);
                                assignCalls.dueType = insForecast.renewaltype;
                                assignCalls.dueDate = insForecast.policyexpirydate;
                            }
                            else
                            {
                                //vehicle veh = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehiId);
                                //if (veh.nextRenewalType != null && !veh.nextRenewalType.Equals(""))
                                //{
                                //    string dueTypes = veh.nextRenewalType;
                                //    long dueId = 0;
                                //    if (dueTypes.Contains("Renewal"))
                                //    {
                                //        dueId = db.renewaltypes.FirstOrDefault(u => u.renewalTypeName == dueTypes).id;
                                //    }
                                //    else
                                //    {
                                //        long renewalId = long.Parse(dueTypes);
                                //        dueId = db.renewaltypes.FirstOrDefault(u => u.id == renewalId).id;
                                //    }

                                //    assignCalls.dueType = dueId;
                                //}
                                //else
                                //{
                                //    //insu_assign.policyDueType = "0";
                                //}
                                //assignCalls.dueDate = Convert.ToDateTime(insu_assign.policyDueDate);
                            }
                            assignCalls.isautoassigned = false;

                            db.assignedcallsreports.Add(assignCalls);
                            db.SaveChanges();
                        }

                        dbTrans.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        return ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        return ex.InnerException.Message;
                    }
                }
                else
                {
                    return ex.Message;
                }
            }

            return "True";
        }

        [HttpPost, ActionName("Call_Logging")]
        public ActionResult CallLoggingPost(CallLoggingViewModel callLogging)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");

            string DispoType = Session["typeOfDispo"].ToString();
            string submissionResult = string.Empty, assignError = "";
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());

            if (cusId != callLogging.CustomerId && vehiId != callLogging.VehicleId)
            {
                TempData["Exceptions"] = "Invalid disposition...";

                logger.Info("\n\n------- " + DispoType.ToUpper() + "Invalid Submition: " + DateTime.Now + "\n Disposing CustId: " + cusId + "Disposing VehId: " + vehiId + " User: " + Session["UserName"].ToString() + "\n Prev-Open Cust: " + callLogging.CustomerId + " Prev-Open VehiId" + callLogging.VehicleId);
                return RedirectToAction("LogOff", "Home");
            }

            logger.Info("\n\n------- " + DispoType.ToUpper() + " Submition started : " + DateTime.Now + "\n CustId: " + cusId + " VehId: " + vehiId + " User: " + Session["UserName"].ToString());
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    ListingForm listingFormData = callLogging.listingForm;
                    srdisposition srData = callLogging.srdisposition;
                    callinteraction callinteraction = callLogging.callinteraction;
                    servicebooked serviceBooked = callLogging.servicebooked;
                    calldispositiondata disposition = callLogging.finaldispostion;
                    insurancedisposition inData = callLogging.insudisposition;
                    appointmentbooked appointmentbooked = callLogging.appointbooked;
                    customer custNew = callLogging.cust;
                    if (Session["DealerCode"].ToString() == "BRIDGEWAYMOTORS" && Session["UserRole1"].ToString() == "2")
                    {
                        if (inData != null)
                        {
                            ViewBag.superCREId = inData.supCREId;
                        }
                    }
                    if (Session["DealerCode"].ToString() == "ABTMARUTHI")
                    {
                        Session["DialedNumber"] = callLogging.intalkDialNumber;
                        Session["isCallInitiated"] = callLogging.intalkIsCallInitiated;
                        Session["GSMUniqueId"] = callLogging.intalkGSMUniqueId;
                        Session["NCReason"] = callLogging.intalkNCReason;
                    }

                    if (callinteraction == null)
                    {
                        callinteraction = new callinteraction();
                    }


                    if (DispoType == "Service")
                    {
                        if (recordDisposition(0, "service", db, 0) == 0404)
                        {
                            assignError = assignDispoCRE("Service");
                            if (assignError != "True")
                            {
                                //goto errorWhileAssig;
                            }
                        }
                    }
                    else if (DispoType == "insurance")
                    {
                        if (recordDisposition(0, "insurance", db, 0) == 0404)
                        {
                            assignError = assignDispoCRE("Insurance");
                            if (assignError != "True")
                            {
                                //goto errorWhileAssig;
                            }
                        }
                    }

                    if (srData.typeOfDisposition == "Contact")
                    {
                        string action = listingFormData.dispoCustAns;

                        if (callinteraction == null)
                        {
                            callinteraction = new callinteraction();
                        }

                        try
                        {


                            if (listingFormData != null && srData != null)
                            {
                                if (DispoType == "insurance")
                                {
                                    submissionResult = insuranceDataSubmit(action, callinteraction, listingFormData, inData, appointmentbooked);
                                    Session["appointBookId"] = null;
                                }
                                else if (DispoType == "Service")
                                {
                                    submissionResult = serviceDataSubmit(action, callinteraction, listingFormData, srData, serviceBooked);
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                    else if (srData.typeOfDisposition == "NonContact")
                    {
                        //long cusId = Convert.ToInt32(Session["CusId"].ToString());
                        //long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
                        long userId = Convert.ToInt32(Session["UserId"].ToString());
                        int currentDisposition = 0;
                        long ass_id = 0;
                        bool isSuperCre = false, isSuperCreControl = false, isUserControl = false;

                        if ((Session["DealerCode"].ToString() == "HARPREETFORD" || Session["DealerCode"].ToString() == "HANSHYUNDAI") && Session["UserRole1"].ToString() == "1")
                        {
                            List<emailcredential> emailcredentials = db.emailcredentials.Where(m => m.emailType == srData.typeOfDisposition).ToList();
                            autoEmailDay(cusId, userId, vehiId, "NONCONTACT", emailcredentials.FirstOrDefault().fromemail, emailcredentials.FirstOrDefault().userPassword, null, null, null, null, null);
                        }

                        using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
                        {
                            try
                            {

                                if (Session["typeOfDispo"].ToString() == "Service")
                                {
                                    if (db.assignedinteractions.Any(m => m.customer_id == cusId && m.vehical_Id == vehiId))
                                    {
                                        if (Session["IsSuperCRE"] != null && Convert.ToBoolean(Session["IsSuperCRE"]) == true)
                                        {
                                            isSuperCre = true;
                                            isSuperCreControl = db.dealers.FirstOrDefault().superControl ?? default(bool);
                                            isUserControl = db.dealers.FirstOrDefault().userControl ?? default(bool);
                                        }

                                        string callLogFrom = string.Empty;

                                        if (Session["MakeCallFrom"] != null)
                                        {
                                            callLogFrom = Session["MakeCallFrom"].ToString();
                                        }
                                        else
                                        {
                                            callLogFrom = "bucket";
                                        }

                                        if (isSuperCreControl == false && callLogFrom != "bucket")
                                        {
                                            if (isSuperCre == true)
                                            {
                                                userId = db.assignedinteractions.FirstOrDefault(m => m.customer_id == cusId && m.vehical_Id == vehiId).wyzUser_id ?? default(long);
                                            }

                                        }



                                        if (listingFormData.dispoNotTalk == "Other")
                                        {
                                            listingFormData.dispoNotTalk = "NoOther";
                                        }
                                        currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == listingFormData.dispoNotTalk).dispositionId;

                                        if (currentDisposition == 0)
                                        {
                                            TempData["SubmissionResult"] = "no disposition found, cannot do booking";
                                            return RedirectToAction("ReturnToBucket", new { @id = 1 });
                                        }
                                        long? serviceMaxId = 0;

                                        serviceMaxId = getServiceBookedStatus();

                                        servicebooked serviceExit = new servicebooked();
                                        if (serviceMaxId != 0)
                                        {
                                            serviceExit = db.servicebookeds.FirstOrDefault(m => m.serviceBookedId == serviceMaxId);
                                            serviceExit.customer_id = cusId;
                                            serviceExit.vehicle_vehicle_id = vehiId;
                                            serviceExit.wyzUser_id = userId;
                                            serviceExit.chaser_id = userId;

                                            serviceExit.updatedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                            //serviceExit.serviceBookStatus_id = currentDisposition;-- commented on 05-05-2020
                                            db.servicebookeds.AddOrUpdate(serviceExit);
                                            db.SaveChanges();

                                        }

                                        //**************** Updating AssignedInteraction *********************

                                        if (isSuperCre == true && isSuperCreControl == true)
                                        {
                                            ass_id = recordDisposition(2, "service", db, currentDisposition, userId, true, false);
                                        }
                                        else
                                        {
                                            ass_id = recordDisposition(2, "service", db, currentDisposition, userId);
                                        }
                                        //****************************End***************************

                                        //***************** CallInteraction starts******************
                                        callinteraction.callCount = 1;
                                        callinteraction.callDate = DateTime.Now.ToString("dd-MM-yyyy");
                                        callinteraction.callMadeDateAndTime = DateTime.Now;
                                        callinteraction.callTime = DateTime.Now.ToString("HH:mm:ss");
                                        callinteraction.dealerCode = Session["DealerCode"].ToString();
                                        if (Session["isCallInitiated"] != null)
                                        {
                                            callinteraction.isCallinitaited = "initiated";
                                            if (Session["MakeCallFrom"] != null)
                                            {
                                                callinteraction.makeCallFrom = Session["MakeCallFrom"].ToString();
                                            }
                                            else
                                            {
                                                callinteraction.makeCallFrom = "Service";
                                            }


                                            if (Session["AndroidUniqueId"] != null)
                                            {
                                                callinteraction.uniqueidForCallSync = int.Parse(Session["AndroidUniqueId"].ToString());
                                            }
                                            else if (Session["GSMUniqueId"] != null)
                                            {
                                                //callinteraction.uniqueIdGSM = double.Parse(Session["GSMUniqueId"].ToString());
                                                callinteraction.uniqueIdGSM = Session["GSMUniqueId"].ToString().Split(';')[0];
                                            }
                                        }
                                        if (Session["MakeCallFrom"] != null)
                                        {
                                            callinteraction.makeCallFrom = Session["MakeCallFrom"].ToString();
                                        }
                                        else
                                        {
                                            callinteraction.makeCallFrom = "Service";
                                        }
                                        callinteraction.callStatus = Convert.ToBoolean(Session["NCReason"]);

                                        if (Session["DialedNumber"] != null)
                                        {
                                            callinteraction.dailedNoIs = Session["DialedNumber"].ToString();
                                        }

                                        callinteraction.assignedInteraction_id = ass_id;
                                        callinteraction.customer_id = cusId;
                                        callinteraction.vehicle_vehicle_id = vehiId;
                                        callinteraction.wyzUser_id = userId;
                                        callinteraction.agentName = Session["UserName"].ToString();
                                        callinteraction.campaign_id = db.assignedinteractions.FirstOrDefault(m => m.id == ass_id).campaign_id;
                                        if (serviceMaxId != 0)
                                        {
                                            callinteraction.serviceBooked_serviceBookedId = serviceExit.serviceBookedId;
                                        }

                                        int taggId = 0;
                                        string camp = "";

                                        callinteraction.tagging_id = taggId;
                                        callinteraction.tagging_name = camp;
                                        callinteraction.chasserCall = false;

                                        db.callinteractions.Add(callinteraction);
                                        db.SaveChanges();

                                        if (Session["GSMUniqueId"] != null)
                                        {
                                            gsmsynchdata gsm = new gsmsynchdata();
                                            gsm.Callinteraction_id = callinteraction.id;
                                            gsm.CallMadeDateTime = DateTime.Now;
                                            gsm.UniqueGsmId = Session["GSMUniqueId"].ToString().Split(';')[0];
                                            gsm.TenantUrl = Session["GSMUniqueId"].ToString().Split(';')[1];
                                            gsm.callFor = Convert.ToInt32(Session["UserRole1"]);
                                            db.gsmsynchdata.Add(gsm);
                                            db.SaveChanges();
                                        }
                                        string remarks = "", comments = "";

                                        for (int i = 0; i < listingFormData.remarksList.Count; i++)
                                        {
                                            if (remarks == "" && listingFormData.remarksList[i] != "")
                                            {
                                                srData.remarks = listingFormData.remarksList[i];
                                            }

                                            if (comments == "" && listingFormData.commentsList[i] != "")
                                            {
                                                srData.comments = listingFormData.commentsList[i];
                                            }
                                        }

                                        //if (isSuperCreControl==true)
                                        //{

                                        //    assignedinteraction assignInter = db.assignedinteractions.FirstOrDefault(m => m.id == ass_id);

                                        //    change_assignment_records changeAssignRecord = new change_assignment_records();
                                        //    changeAssignRecord.assignedinteraction_id = assignInter.id;
                                        //    changeAssignRecord.campaign_id = assignInter.campaign_id ?? default(long);
                                        //    changeAssignRecord.last_wyzuserId = assignInter.wyzUser_id ?? default(long);

                                        //    assignInter.wyzUser_id = userId;
                                        //    //assignInter.assigned_wyzuser_id = superCreId;
                                        //    //if(db.wyzusers.Any(m=>m.id==superCreId))
                                        //    //{
                                        //    //    string superCreManagerName=db.wyzusers.FirstOrDefault(m => m.id == superCreId).creManager;
                                        //    //    assignInter.assigned_manager_id = db.wyzusers.FirstOrDefault(m => m.userName == superCreManagerName).id;
                                        //    //}
                                        //    db.assignedinteractions.AddOrUpdate(assignInter);
                                        //    db.SaveChanges();

                                        //    changeAssignRecord.new_wyzuserId = userId;
                                        //    changeAssignRecord.updatedDate = DateTime.Now;
                                        //    changeAssignRecord.moduletypeIs = 1;
                                        //    db.change_assignment_records.Add(changeAssignRecord);
                                        //    db.SaveChanges();
                                        //}

                                        if (isSuperCre == true && isSuperCreControl == false)
                                        {
                                            srData.remarks = srData.remarks + "- By SuperCre-" + Session["UserName"].ToString();
                                            srData.comments = srData.comments + "- By SuperCre-" + Session["UserName"].ToString();
                                        }

                                        int upselCount = 0;
                                        //********************** Sr_Disposition Saving part ********************
                                        srData.InOutCallName = "OutCall";
                                        srData.assignedToSA = "0";
                                        srData.cityName = listingFormData.cityName;
                                        srData.callDispositionData_id = currentDisposition;
                                        srData.callInteraction_id = callinteraction.id;
                                        srData.callinteraction = callinteraction;
                                        srData.upsellCount = upselCount;
                                        db.srdispositions.Add(srData);
                                        db.SaveChanges();
                                        submissionResult = "True";

                                        if (Session["DealerCode"].ToString() == "KATARIA" && listingFormData.issmsEnabled)
                                        {

                                            autosmsKataria(userId, vehiId, cusId, currentDisposition, string.Empty, string.Empty, Convert.ToInt32(Session["UserRole1"]), 0, 0, 0, "", 0);
                                        }

                                        else

                                        {

                                            if (currentDisposition == 6 || currentDisposition == 7 || currentDisposition == 8)
                                            {
                                                autosmsday(userId, vehiId, cusId, "NONCONTACT", "Service", 0, 0, 0, "", 0);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        TempData["SubmissionResult"] = "no assignment found, cannot do booking";
                                        return RedirectToAction("ReturnToBucket", new { @id = 1 });
                                    }
                                    dbTransaction.Commit();
                                }
                                else
                                {
                                    submissionResult = nonContact(callinteraction, listingFormData, inData);//Non Contacts for insurance
                                    if (submissionResult == "True")
                                    {
                                        if (currentDisposition == 6 || currentDisposition == 7 || currentDisposition == 8)
                                        {
                                            autosmsday(userId, vehiId, cusId, "INSNONCONTACT", "Insurance", 0, 0, 0, "", 0);
                                        }
                                        submissionResult = "True";
                                    }
                                }
                                //sr_disposition.


                            }
                            catch (Exception ex)
                            {
                                dbTransaction.Rollback();
                                if (ex.InnerException != null)
                                {
                                    if (ex.InnerException.InnerException != null)
                                    {
                                        submissionResult = ex.InnerException.InnerException.Message;
                                    }
                                    else
                                    {
                                        submissionResult = ex.InnerException.Message;
                                    }
                                }
                                else
                                {
                                    submissionResult = ex.Message;
                                }
                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        submissionResult = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        submissionResult = ex.InnerException.Message;
                    }
                }
                else
                {
                    submissionResult = ex.Message;
                }


            }

            //errorWhileAssig:
            //    submissionResult = assignError;

            TempData["SubmissionResult"] = submissionResult;
            logger.Info("\n\n------- " + DispoType.ToUpper() + " Submition Ended : " + DateTime.Now);
            if (DispoType == "insurance")
            {
                return RedirectToAction("ReturnToBucket", new { @id = 2 });
            }
            else if (DispoType == "Service")
            {
                return RedirectToAction("ReturnToBucket", new { @id = 1 });
            }
            else
            {
                return RedirectToAction("ReturnToBucket", new { @id = 1 });
            }

        }

        public long getServiceBookedStatus()
        {
            long serviceMaxId = 0, presentAssignId = 0;
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (db.servicebookeds.Count(m => m.customer_id == cusId && m.vehicle_vehicle_id == vehiId) > 0)
                    {
                        serviceMaxId = db.servicebookeds.Where(m => m.customer_id == cusId && m.vehicle_vehicle_id == vehiId && m.serviceBookStatus_id != 35).Max(m => m.serviceBookedId);
                    }

                    if (serviceMaxId != 0)
                    {
                        if (db.callinteractions.Count(m => m.serviceBooked_serviceBookedId == serviceMaxId) > 0)
                        {
                            presentAssignId = recordDisposition(0, "service", db, 0);
                            if (presentAssignId != 0404)
                            {
                                long maxCallInterId = db.callinteractions.Where(m => m.serviceBooked_serviceBookedId == serviceMaxId).Max(m => m.id);
                                long? LatestAssinIdInCallInter = db.callinteractions.FirstOrDefault(m => m.id == maxCallInterId).assignedInteraction_id;

                                if (LatestAssinIdInCallInter == presentAssignId)
                                {
                                    return serviceMaxId;
                                }
                                else
                                {
                                    return 0;
                                }
                            }
                            else
                            {
                                return 0;
                            }
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return serviceMaxId;
        }

        public long getInsuranceAppointmentStatus()
        {
            long appointMaxId = 0, presentAssignId = 0;
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (db.appointmentbookeds.Count(m => m.customer_id == cusId && m.vehicle_id == vehiId && m.insuranceBookStatus_id != 35) > 0)
                    {
                        appointMaxId = db.appointmentbookeds.Where(m => m.customer_id == cusId && m.vehicle_id == vehiId).Max(m => m.appointmentId);
                    }

                    if (appointMaxId != 0)
                    {
                        if (db.callinteractions.Count(m => m.appointmentBooked_appointmentId == appointMaxId) > 0)
                        {
                            presentAssignId = recordDisposition(0, "insurance", db, 0);
                            if (presentAssignId != 0404)
                            {
                                long maxCallInterId = db.callinteractions.Where(m => m.appointmentBooked_appointmentId == appointMaxId).Max(m => m.id);
                                long? LatestAssinIdInCallInter = db.callinteractions.FirstOrDefault(m => m.id == maxCallInterId).insuranceAssignedInteraction_id;

                                if (LatestAssinIdInCallInter == presentAssignId)
                                {
                                    return appointMaxId;
                                }
                                else
                                {
                                    return 0;
                                }
                            }
                            else
                            {
                                return 0;
                            }
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return appointMaxId;
        }

        //Updation for assignInteraction,insuiaassint and ext
        public long recordDisposition(int stage, string typeOfDipo, AutoSherDBContext db, int? finalDispoId = 0, long assignWyzId = 0, bool isSuperCre = false, bool isEscalation = false, long pd_location = 0, string policyDropDate = "", long feid = 0, string pinCode = "", long pickupId = 0)
        {

            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            long userId = Convert.ToInt32(Session["UserId"].ToString());
            //try
            //{
            //using (var db = new AutoSherDBContext())
            //{
            if (typeOfDipo == "service")
            {
                if (stage == 0)//To get Id only
                {
                    if (db.assignedinteractions.Any(m => m.customer_id == cusId && m.vehical_Id == vehiId))
                    {
                        return db.assignedinteractions.FirstOrDefault(m => m.customer_id == cusId && m.vehical_Id == vehiId).id;
                    }
                    else
                    {
                        return 0404;
                    }
                }
                else if (stage == 1)//Initiated
                {
                    if (db.assignedinteractions.Where(m => m.customer_id == cusId && m.vehical_Id == vehiId && m.wyzUser_id == userId).Count() != 0)
                    {
                        assignedinteraction assInter = db.assignedinteractions.FirstOrDefault(m => m.customer_id == cusId && m.vehical_Id == vehiId && m.wyzUser_id == userId);
                        assInter.callMade = "Initiated";
                        //assInter.uplodedCurrentDate = DateTime.Now;
                        db.SaveChanges();
                        return assInter.id;
                    }
                }
                else if (stage == 2)//Yes
                {
                    if (db.assignedinteractions.Where(m => m.customer_id == cusId && m.vehical_Id == vehiId).Count() != 0)
                    {
                        string lastdispo = string.Empty;
                        assignedinteraction assInter = db.assignedinteractions.FirstOrDefault(m => m.customer_id == cusId && m.vehical_Id == vehiId);
                        assInter.callMade = "Yes";
                        //assInter.uplodedCurrentDate = DateTime.Now;

                        //swapping finaldisposition to lastDisposition..............
                        if (assInter.finalDisposition_id != null)
                        {
                            lastdispo = db.calldispositiondatas.FirstOrDefault(m => m.id == assInter.finalDisposition_id).disposition;
                        }
                        assInter.finalDisposition_id = finalDispoId;
                        assInter.lastDisposition = lastdispo;

                        if (assignWyzId != 0)
                        {
                            string callLogFrom = string.Empty;
                            bool isUserControl = db.dealers.FirstOrDefault().userControl ?? default(bool);

                            if (Session["MakeCallFrom"] != null)
                            {
                                callLogFrom = Session["MakeCallFrom"].ToString();
                            }
                            else
                            {
                                callLogFrom = "bucket";
                            }
                            #region Update Data
                            //if ((callLogFrom != "bucket" || isEscalation == true) && (isUserControl == false || isUserControl == true || isEscalation == true))
                            //{
                            //    long loginUserId = long.Parse(Session["UserId"].ToString());

                            //    if (loginUserId != assInter.wyzUser_id || isEscalation == true)
                            //    {

                            //        if (lastdispo != "Book My Service" && (isUserControl == false || isSuperCre == true || isEscalation == true))
                            //        {
                            //            change_assignment_records changeAssignRecord = new change_assignment_records();
                            //            changeAssignRecord.assignedinteraction_id = assInter.id;
                            //            changeAssignRecord.campaign_id = assInter.campaign_id ?? default(long);
                            //            changeAssignRecord.last_wyzuserId = assInter.wyzUser_id ?? default(long);

                            //            assInter.wyzUser_id = assignWyzId;
                            //            //assignInter.assigned_wyzuser_id = superCreId;
                            //            //if(db.wyzusers.Any(m=>m.id==superCreId))
                            //            //{
                            //            //    string superCreManagerName=db.wyzusers.FirstOrDefault(m => m.id == superCreId).creManager;
                            //            //    assignInter.assigned_manager_id = db.wyzusers.FirstOrDefault(m => m.userName == superCreManagerName).id;
                            //            //}

                            //            changeAssignRecord.new_wyzuserId = assignWyzId;
                            //            changeAssignRecord.updatedDate = DateTime.Now;
                            //            changeAssignRecord.moduletypeIs = 1;
                            //            db.change_assignment_records.Add(changeAssignRecord);
                            //            db.SaveChanges();
                            //        }
                            //    }
                            //}
                            #endregion
                            long loginUserId = long.Parse(Session["UserId"].ToString());
                            if (((callLogFrom != "bucket") && (db.dealers.FirstOrDefault().superControl == true && isSuperCre == true && assInter.wyzUser_id != loginUserId)) || (isEscalation == true))
                            {
                                change_assignment_records changeAssignRecord = new change_assignment_records();
                                changeAssignRecord.assignedinteraction_id = assInter.id;
                                changeAssignRecord.campaign_id = assInter.campaign_id ?? default(long);
                                changeAssignRecord.last_wyzuserId = assInter.wyzUser_id ?? default(long);
                                changeAssignRecord.new_wyzuserId = assignWyzId;
                                changeAssignRecord.updatedDate = DateTime.Now;
                                changeAssignRecord.moduletypeIs = 1;
                                db.change_assignment_records.Add(changeAssignRecord);
                                db.SaveChanges();
                                assInter.wyzUser_id = assignWyzId;
                            }
                        }

                        db.assignedinteractions.AddOrUpdate(assInter);
                        db.SaveChanges();
                        return assInter.id;

                    }
                }
                else if (stage == 3)//No
                {
                    if (db.assignedinteractions.Where(m => m.customer_id == cusId && m.vehical_Id == vehiId && m.wyzUser_id == userId).Count() != 0)
                    {
                        assignedinteraction assInter = db.assignedinteractions.FirstOrDefault(m => m.customer_id == cusId && m.vehical_Id == vehiId && m.wyzUser_id == userId);
                        assInter.callMade = "No";
                        //assInter.uplodedCurrentDate = DateTime.Now;

                        //swapping finaldisposition to lastDisposition..............
                        calldispositiondata calldispositiondata = new calldispositiondata();
                        if (assInter.finalDisposition_id == null)
                        {
                            assInter.finalDisposition_id = 3;
                        }
                        calldispositiondata = db.calldispositiondatas.FirstOrDefault(m => m.id == assInter.finalDisposition_id);
                        assInter.finalDisposition_id = finalDispoId;
                        assInter.lastDisposition = calldispositiondata.disposition;
                        db.SaveChanges();
                        return assInter.id;

                    }
                }
            }
            else if (typeOfDipo == "insurance")
            {
                if (stage == 0)//NotDialed
                {
                    if (db.insuranceassignedinteractions.Any(m => m.customer_id == cusId && m.vehicle_vehicle_id == vehiId))
                    {
                        return db.insuranceassignedinteractions.FirstOrDefault(m => m.customer_id == cusId && m.vehicle_vehicle_id == vehiId).id;
                    }
                    else
                    {
                        return 0404;
                    }
                }
                else if (stage == 1)//Initiated
                {
                    if (db.insuranceassignedinteractions.Where(m => m.customer_id == cusId && m.vehicle_vehicle_id == vehiId && m.wyzUser_id == userId).Count() != 0)
                    {
                        insuranceassignedinteraction assInter = db.insuranceassignedinteractions.FirstOrDefault(m => m.customer_id == cusId && m.vehicle_vehicle_id == vehiId && m.wyzUser_id == userId);
                        assInter.callMade = "Initiated";
                        //assInter.uplodedCurrentDate = DateTime.Now;
                        db.SaveChanges();
                        return assInter.id;
                    }
                }
                else if (stage == 2)//Yes
                {
                    bool isUserControl = db.dealers.FirstOrDefault().userControl ?? default(bool);
                    long loginUserId = long.Parse(Session["UserId"].ToString());

                    insuranceassignedinteraction assInter = db.insuranceassignedinteractions.FirstOrDefault(m => m.customer_id == cusId && m.vehicle_vehicle_id == vehiId);
                    assInter.callMade = "Yes";
                    //assInter.uplodedCurrentDate = DateTime.Now;
                    //swapping finaldisposition to lastDisposition..............
                    calldispositiondata calldispositiondata = new calldispositiondata();
                    if (assInter.finalDisposition_id != null)
                    {
                        calldispositiondata = db.calldispositiondatas.FirstOrDefault(m => m.id == assInter.finalDisposition_id);
                        assInter.lastDisposition = calldispositiondata.disposition;
                    }
                    if (pd_location != 0 && policyDropDate != "")
                    {
                        assInter.pd_locationId = pd_location;
                        assInter.appointmentDate = Convert.ToDateTime(policyDropDate);
                        if (feid != 0 && pickupId != 0)
                        {
                            assInter.FEID = feid;
                            assInter.policyPincode = pinCode;
                            assInter.pickUPID = pickupId;
                        }
                    }
                    assInter.finalDisposition_id = finalDispoId;

                    //if ((db.dealers.FirstOrDefault().INSsuperControl == true && isSuperCre == true && assInter.wyzUser_id != loginUserId) || (assInter.wyzUser_id != loginUserId && isSuperCre == false && isUserControl == false))
                    if ((assInter.wyzUser_id != loginUserId) && ((db.dealers.FirstOrDefault().INSsuperControl == true && isSuperCre == true)))
                    {
                        change_assignment_records changeAssignRecord = new change_assignment_records();
                        changeAssignRecord.assignedinteraction_id = assInter.id;
                        changeAssignRecord.campaign_id = assInter.campaign_id ?? default(long);
                        changeAssignRecord.last_wyzuserId = assInter.wyzUser_id ?? default(long);

                        changeAssignRecord.new_wyzuserId = loginUserId;
                        changeAssignRecord.updatedDate = DateTime.Now;
                        changeAssignRecord.moduletypeIs = 2;
                        db.change_assignment_records.Add(changeAssignRecord);

                        assInter.wyzUser_id = loginUserId; //-----------------> change assignments......

                        db.SaveChanges();
                    }
                    if (Session["DealerCode"].ToString() == "BRIDGEWAYMOTORS" && Session["UserRole1"].ToString() == "2")
                    {
                        if (ViewBag.superCREId != 0)
                        {
                            assInter.wyzUser_id = ViewBag.superCREId;
                        }
                    }
                    db.insuranceassignedinteractions.AddOrUpdate(assInter);
                    db.SaveChanges();
                    return assInter.id;

                }
                else if (stage == 3)//No
                {
                    if (db.insuranceassignedinteractions.Where(m => m.customer_id == cusId && m.vehicle_vehicle_id == vehiId && m.wyzUser_id == userId).Count() != 0)
                    {
                        insuranceassignedinteraction assInter = db.insuranceassignedinteractions.FirstOrDefault(m => m.customer_id == cusId && m.vehicle_vehicle_id == vehiId && m.wyzUser_id == userId);
                        assInter.callMade = "No";
                        //assInter.uplodedCurrentDate = DateTime.Now;

                        //swapping finaldisposition to lastDisposition..............
                        calldispositiondata calldispositiondata = new calldispositiondata();
                        if (assInter.finalDisposition_id != null)
                        {
                            calldispositiondata = db.calldispositiondatas.FirstOrDefault(m => m.id == assInter.finalDisposition_id);
                            assInter.lastDisposition = calldispositiondata.disposition;
                        }
                        assInter.finalDisposition_id = finalDispoId;
                        db.SaveChanges();
                        return assInter.id;

                    }
                }
            }
            //}
            //}
            //catch (Exception ex)
            //{

            //}
            return 0;
        }

        public ActionResult ReturnToBucket(int id, int? psfDay = 3)
        {
            if (TempData["SubmissionResult"] != null)
            {
                TempData["SubmissionResult"] = TempData["SubmissionResult"];
            }

            if (id == 1)
            {

                return RedirectToAction("Service", "CREServiceLog");
            }
            else if (id == 2)
            {
                return RedirectToAction("Insurance", "CREInsurance");
            }
            else if (id == 3)
            {
                return RedirectToAction("PSFDetails", "PSF", new { @id = psfDay });
            }
            else if (id == 6)
            {
                if (psfDay == 6)
                {
                    return RedirectToAction("PSFDetails", "PSF", new { @id = psfDay });
                }
                else
                {
                    return RedirectToAction("PSFDetails", "PSF", new { @id = psfDay });
                }

            }
            else if (id == 900)
            {
                return RedirectToAction("psfComplaintDetails", "PSF", null);
            }
            else if (id == 500)
            {
                return RedirectToAction("PSFRM", "PSF", null);
            }
            else if (id == 101)
            {
                return RedirectToAction("mcpispew", "mcpispew", null);
            }

            else if (id == 105 || id == 107)
            {
                return RedirectToAction("postSalesLogs", "postSalesDetails", new { @id = psfDay });

            }
            else if (id == 9000)
            {
                return RedirectToAction("postsalesComplaintLogs", "postSalesDetails", null);
            }
            else
            {
                return RedirectToAction("Service", "CREServiceLog");
            }
        }

        //Change assignment for SMR while saving the data
        public void doSMRChangeAssignments(long wyzUserId, long ass_id, AutoSherDBContext db)
        {

            bool isUserControl = db.dealers.FirstOrDefault().userControl ?? default(bool);

            string callLogFrom = string.Empty;

            if (Session["MakeCallFrom"] != null)
            {
                callLogFrom = Session["MakeCallFrom"].ToString();
            }
            else
            {
                callLogFrom = "bucket";
            }

            if (callLogFrom != "bucket" && isUserControl == false)
            {
                long loginUserId = long.Parse(Session["UserId"].ToString());

                assignedinteraction assignInter = db.assignedinteractions.FirstOrDefault(m => m.id == ass_id);

                if (loginUserId != assignInter.wyzUser_id)
                {
                    change_assignment_records changeAssignRecord = new change_assignment_records();
                    changeAssignRecord.assignedinteraction_id = assignInter.id;
                    changeAssignRecord.campaign_id = assignInter.campaign_id ?? default(long);
                    changeAssignRecord.last_wyzuserId = assignInter.wyzUser_id ?? default(long);

                    assignInter.wyzUser_id = wyzUserId;
                    //assignInter.assigned_wyzuser_id = superCreId;
                    //if(db.wyzusers.Any(m=>m.id==superCreId))
                    //{
                    //    string superCreManagerName=db.wyzusers.FirstOrDefault(m => m.id == superCreId).creManager;
                    //    assignInter.assigned_manager_id = db.wyzusers.FirstOrDefault(m => m.userName == superCreManagerName).id;
                    //}
                    db.assignedinteractions.AddOrUpdate(assignInter);
                    db.SaveChanges();

                    changeAssignRecord.new_wyzuserId = wyzUserId;
                    changeAssignRecord.updatedDate = DateTime.Now;
                    changeAssignRecord.moduletypeIs = 1;
                    db.change_assignment_records.Add(changeAssignRecord);
                    db.SaveChanges();
                }
            }
        }


        //callinteraction callinteraction, srdisposition sr_disposition, ListingForm listingForm, servicebooked servicebooked, string bookingMode
        public string serviceDataSubmit(string bookingMode, callinteraction callinteraction, ListingForm listingForm, srdisposition sr_disposition, servicebooked servicebooked)
        {
            string returnString = string.Empty;
            int currentDisposition = 0;

            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            long userId = Convert.ToInt32(Session["UserId"].ToString());
            servicebooked serviceExit = new servicebooked();
            long selectedWorkshop = 0;
            //For Driver Changes
            long PickUPDrop = 0;
            TimeSpan pickUptime = Convert.ToDateTime(listingForm.PickUpStartTime).TimeOfDay;
            TimeSpan droppickUptime = Convert.ToDateTime(listingForm.PickUpStartTime).TimeOfDay;

            using (var db = new AutoSherDBContext())
            {
                using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        string scheduledDate = listingForm.serviceScheduledDate;
                        string scheduledTime = listingForm.serviceScheduledTime;
                        long? serviceMaxId = 0;

                        if (db.assignedinteractions.Count(m => m.customer_id == cusId && m.vehical_Id == vehiId) > 0)
                        {
                            bool isSuperCre = false;
                            bool isSuperControl = false;
                            bool isUserControl = db.dealers.FirstOrDefault().userControl ?? default(bool);

                            if (Session["IsSuperCRE"] != null && Convert.ToBoolean(Session["IsSuperCRE"]) == true)
                            {
                                isSuperCre = true;

                                isSuperControl = db.dealers.FirstOrDefault().superControl ?? default(bool);
                            }

                            string callLogFrom = string.Empty;

                            if (Session["MakeCallFrom"] != null)
                            {
                                callLogFrom = Session["MakeCallFrom"].ToString();
                            }
                            else
                            {
                                callLogFrom = "bucket";
                            }

                            if (isSuperControl == false && callLogFrom != "bucket")
                            {
                                if (isSuperCre == true)
                                {
                                    userId = db.assignedinteractions.FirstOrDefault(m => m.customer_id == cusId && m.vehical_Id == vehiId).wyzUser_id ?? default(long);
                                }

                            }

                            int /*currentDisposition = 0,*/ secondary_dispo = 0;
                            long ass_id = 0;

                            if (string.IsNullOrEmpty(bookingMode))
                            {
                                return "Call Action is empty";
                            }
                            else
                            {
                                if (bookingMode == "Book My Service" || bookingMode == "Rescheduled" || bookingMode == "Confirmed" || bookingMode == "Service Not Required" || bookingMode == "Call Me Later" || bookingMode == "Cancelled" || bookingMode == "Other")
                                {

                                    if (bookingMode != "Other")
                                    {
                                        if (bookingMode == "Book My Service" || bookingMode == "Rescheduled")
                                        {
                                            currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Book My Service").dispositionId;
                                        }

                                        else
                                        {
                                            currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == bookingMode).dispositionId;
                                        }

                                    }
                                    else
                                    {
                                        var calldispositiondatas = db.calldispositiondatas.Where(m => m.disposition.Contains(sr_disposition.othersSMR)).FirstOrDefault();
                                        if (calldispositiondatas != null)
                                        {
                                            currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == sr_disposition.othersSMR).dispositionId;
                                        }
                                    }

                                    if (currentDisposition == 0)
                                    {
                                        return "No disposition record found, cannot do booking...";
                                    }

                                    //****************** Service booking insert *******************
                                    if (bookingMode == "Book My Service" || bookingMode == "Rescheduled")
                                    {
                                        if (bookingMode == "Rescheduled")
                                        {
                                            long maxId = 0;
                                            if (db.servicebookeds.Where(m => m.customer_id == cusId && m.vehicle_vehicle_id == vehiId).Count() > 0)
                                            {
                                                maxId = db.servicebookeds.Where(m => m.customer_id == cusId && m.vehicle_vehicle_id == vehiId).Max(m => m.serviceBookedId);
                                            }

                                            if (maxId != 0)
                                            {
                                                servicebooked serviceCancel = db.servicebookeds.FirstOrDefault(m => m.serviceBookedId == maxId);
                                                if (Session["DealerCode"].ToString() == "INDUS" || Session["DealerCode"].ToString() == "testdb" | Session["DealerCode"].ToString() == "POPULARHYUNDAI" | Session["DealerCode"].ToString() == "KUNHYUNDAI")
                                                {
                                                    if (serviceCancel.typeOfPickup == "Pickup Only" || serviceCancel.typeOfPickup == "Pickup Drop Required" || serviceCancel.typeOfPickup == "Drop Only")
                                                    {
                                                        servicebooked.lastallocateddriver_id = serviceCancel.driver_id;
                                                        servicebooked.lastallocateddropdriver_id = serviceCancel.dropdriver_id;

                                                        if (serviceCancel.typeOfPickup == "Pickup Only")
                                                        {
                                                            allocateDriver("Cancelled", vehiId, null, 0, db, 0, cusId, 1, "Booking", userId);
                                                        }
                                                        else if (serviceCancel.typeOfPickup == "Drop Only")
                                                        {
                                                            allocateDriver("Cancelled", vehiId, null, 0, db, 0, cusId, 2, "Booking", userId);
                                                        }
                                                        else
                                                        {
                                                            allocateDriver("Cancelled", vehiId, null, 0, db, 0, cusId, 1, "Booking", userId);
                                                            allocateDriver("Cancelled", vehiId, null, 0, db, 0, cusId, 2, "Booking", userId);
                                                        }

                                                    }
                                                    //if (servicebooked.typeOfPickup == "Pickup Only" || servicebooked.typeOfPickup == "Pickup Drop Required")
                                                    //{
                                                    //    servicebooked.lastallocateddriver_id = serviceCancel.driver_id;
                                                    //}
                                                    //else
                                                    //{
                                                    //    allocateDriver("Cancelled", vehiId, null, 0, db, 0, cusId, 1, "Booking", userId);
                                                    //}
                                                }

                                                serviceCancel.pickupDrop_id = null;
                                                serviceCancel.serviceBookStatus_id = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Cancelled").dispositionId;
                                                db.servicebookeds.AddOrUpdate(serviceCancel);
                                                db.SaveChanges();
                                            }
                                        }

                                        servicebooked.serviceBookedType = db.servicetypes.FirstOrDefault(m => m.id == servicebooked.serviceBookType_id).serviceTypeName;

                                        if (servicebooked.typeOfPickup != "Customer Drive-In" && servicebooked.typeOfPickup != "QWIK Service")
                                        {
                                            servicebooked.isPickupRequired = true;
                                            if (Session["DealerCode"].ToString() == "INDUS" || Session["DealerCode"].ToString() == "testdb" || Session["DealerCode"].ToString() == "POPULARHYUNDAI" || Session["DealerCode"].ToString() == "KUNHYUNDAI")
                                            {
                                                if (servicebooked.typeOfPickup == "Pickup Only" || servicebooked.typeOfPickup == "Pickup Drop Required" || servicebooked.typeOfPickup == "Drop Only")
                                                {
                                                    long? driverId = servicebooked.driver_id;

                                                    if (servicebooked.typeOfPickup == "Pickup Only")
                                                    {
                                                        if (driverId == 0 || driverId == null)
                                                        {
                                                            driverId = null;
                                                        }
                                                        if (servicebooked.servicepickupdate == null)
                                                        {
                                                            servicebooked.servicepickupdate = Convert.ToDateTime(listingForm.serviceScheduledDate);
                                                        }
                                                        servicebooked.servicedropdate = null;
                                                        servicebooked.driver_id = driverId;
                                                        servicebooked.pickupdroptype = 1;

                                                    }
                                                    else if (servicebooked.typeOfPickup == "Drop Only")
                                                    {
                                                        if (driverId == 0 || driverId == null)
                                                        {
                                                            driverId = null;
                                                        }
                                                        if (servicebooked.servicedropdate == null)
                                                        {
                                                            servicebooked.servicedropdate = Convert.ToDateTime(listingForm.serviceScheduledDate);
                                                        }
                                                        servicebooked.servicepickupdate = null;
                                                        servicebooked.dropdriver_id = driverId;
                                                        servicebooked.driver_id = null;
                                                        servicebooked.pickupdroptype = 2;
                                                    }
                                                    else
                                                    {
                                                        if (driverId == 0 || driverId == null)
                                                        {
                                                            driverId = null;
                                                        }
                                                        if (servicebooked.servicepickupdate == null)
                                                        {
                                                            servicebooked.servicepickupdate = Convert.ToDateTime(listingForm.serviceScheduledDate);
                                                        }
                                                        if (servicebooked.servicedropdate == null)
                                                        {
                                                            servicebooked.servicedropdate = Convert.ToDateTime(listingForm.serviceScheduledDate);
                                                        }
                                                        servicebooked.dropdriver_id = null;

                                                        servicebooked.driver_id = driverId;
                                                        servicebooked.pickupdroptype = 3;

                                                    }

                                                }
                                                else
                                                {
                                                    servicebooked.driver_id = null;
                                                }

                                            }
                                            else
                                            {
                                                servicebooked.driver_id = null;
                                                pickupdrop pickUp = new pickupdrop();
                                                pickUp.pickUpAddress = servicebooked.serviceBookingAddress;
                                                pickUp.pickupDate = DateTime.Now;
                                                db.pickupdrops.Add(pickUp);
                                                db.SaveChanges();

                                                servicebooked.pickupDrop_id = pickUp.id;
                                            }
                                        }
                                        else
                                        {
                                            servicebooked.serviceBookingAddress = "";
                                            servicebooked.isPickupRequired = false;
                                            servicebooked.serviceBookingAddress = "";
                                            servicebooked.serviceBookingDropAddress = "";
                                            servicebooked.driver_id = null;
                                        }

                                        DateTime scheduledDateTime = Convert.ToDateTime(scheduledDate + " " + scheduledTime);
                                        servicebooked.scheduledDateTime = scheduledDateTime;


                                        servicebooked.customer_id = cusId;
                                        servicebooked.vehicle_vehicle_id = vehiId;
                                        servicebooked.wyzUser_id = userId;
                                        servicebooked.chaser_id = userId;
                                        servicebooked.updatedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                        servicebooked.serviceBookStatus_id = currentDisposition;
                                        db.servicebookeds.Add(servicebooked);
                                        db.SaveChanges();

                                        selectedWorkshop = servicebooked.workshop_id ?? default(long);
                                        if (Session["DealerCode"].ToString() == "INDUS" || Session["DealerCode"].ToString() == "testdb" || Session["DealerCode"].ToString() == "POPULARHYUNDAI" || Session["DealerCode"].ToString() == "KUNHYUNDAI")
                                        {
                                            if (servicebooked.typeOfPickup == "Pickup Only" || servicebooked.typeOfPickup == "Pickup Drop Required" || servicebooked.typeOfPickup == "Drop Only")
                                            {
                                                if (servicebooked.typeOfPickup == "Pickup Only")
                                                {
                                                    long BookingDetails_Id = AssignDriverAndBook(Convert.ToDateTime(servicebooked.servicepickupdate), pickUptime.ToString(), 1, servicebooked.typeOfPickup,
                                                      servicebooked.serviceBookingAddress, servicebooked.serviceBookingDropAddress, userId, vehiId, cusId, servicebooked.driver_id,
                                                      servicebooked.lastallocateddriver_id, null, listingForm.PickupTimeRange, servicebooked.workshop_id, servicebooked.serviceBookedId, db);

                                                    allocateDriver(bookingMode, vehiId, null, servicebooked.driver_id ?? default(long), db, BookingDetails_Id, cusId, 1, "Booking", userId);
                                                }
                                                else if (servicebooked.typeOfPickup == "Drop Only")
                                                {
                                                    long BookingDetails_Id = AssignDriverAndBook(Convert.ToDateTime(servicebooked.servicedropdate), droppickUptime.ToString(), 2, servicebooked.typeOfPickup,
                                                 servicebooked.serviceBookingAddress, servicebooked.serviceBookingDropAddress, userId, vehiId, cusId, servicebooked.dropdriver_id,
                                                 servicebooked.lastallocateddropdriver_id, null, listingForm.dropPickupTimeRange, servicebooked.workshop_id, servicebooked.serviceBookedId, db);
                                                    allocateDriver(bookingMode, vehiId, null, servicebooked.dropdriver_id ?? default(long), db, BookingDetails_Id, cusId, 2, "Booking", userId);

                                                }
                                                else
                                                {

                                                    long BookingDetails_Id = 0;
                                                    BookingDetails_Id = AssignDriverAndBook(Convert.ToDateTime(servicebooked.servicepickupdate), pickUptime.ToString(), 1, servicebooked.typeOfPickup,
                                                servicebooked.serviceBookingAddress, servicebooked.serviceBookingDropAddress, userId, vehiId, cusId, servicebooked.driver_id,
                                                servicebooked.lastallocateddriver_id, null, listingForm.PickupTimeRange, servicebooked.workshop_id, servicebooked.serviceBookedId, db);
                                                    allocateDriver(bookingMode, vehiId, null, servicebooked.driver_id ?? default(long), db, BookingDetails_Id, cusId, 1, "Booking", userId);

                                                    TimeSpan newSpan = new TimeSpan(0, 6, 0, 0);

                                                    droppickUptime = droppickUptime.Add(newSpan);
                                                    BookingDetails_Id = AssignDriverAndBook(Convert.ToDateTime(servicebooked.servicedropdate), droppickUptime.ToString(), 2, servicebooked.typeOfPickup,
                                                servicebooked.serviceBookingAddress, servicebooked.serviceBookingDropAddress, userId, vehiId, cusId, servicebooked.dropdriver_id,
                                                servicebooked.lastallocateddriver_id, null, listingForm.dropPickupTimeRange, servicebooked.workshop_id, servicebooked.serviceBookedId, db);
                                                    allocateDriver(bookingMode, vehiId, null, servicebooked.dropdriver_id ?? default(long), db, BookingDetails_Id, cusId, 2, "Booking", userId);

                                                }

                                            }
                                            else
                                            {
                                                servicebooked.driver_id = null;
                                            }

                                        }
                                    }
                                    else
                                    {

                                        serviceMaxId = getServiceBookedStatus();

                                        if (serviceMaxId != 0)
                                        {
                                            serviceExit = db.servicebookeds.FirstOrDefault(m => m.serviceBookedId == serviceMaxId);


                                            if (bookingMode == "Cancelled" || bookingMode == "Confirmed" || bookingMode == "Service Not Required" || bookingMode == "Call Me Later")
                                            {

                                                if (bookingMode == "Cancelled")
                                                {
                                                    if (Session["DealerCode"].ToString() == "INDUS" || Session["DealerCode"].ToString() == "testdb")
                                                    {
                                                        if (serviceExit.typeOfPickup == "Pickup Only" || serviceExit.typeOfPickup == "Pickup Drop Required" || serviceExit.typeOfPickup == "Drop Only")
                                                        {
                                                            //allocateDriver(bookingMode, vehiId, null, 0, db, 0, cusId, 1, "Booking", userId);
                                                            if (servicebooked.typeOfPickup == "Pickup Only")
                                                            {
                                                                allocateDriver(bookingMode, vehiId, null, 0, db, 0, cusId, 1, "Booking", userId);
                                                            }
                                                            else if (servicebooked.typeOfPickup == "Drop Only")
                                                            {
                                                                allocateDriver(bookingMode, vehiId, null, 0, db, 0, cusId, 2, "Booking", userId);
                                                            }
                                                            else
                                                            {
                                                                allocateDriver(bookingMode, vehiId, null, 0, db, 0, cusId, 1, "Booking", userId);
                                                                allocateDriver(bookingMode, vehiId, null, 0, db, 0, cusId, 2, "Booking", userId);
                                                            }
                                                        }
                                                    }
                                                }
                                                if (bookingMode == "Cancelled" && listingForm.ServiceBookingCancel == "Pickup Drop")
                                                {
                                                    serviceExit.isPickupRequired = false;
                                                    serviceExit.pickupDrop_id = null;
                                                    serviceExit.typeOfPickup = "Customer Drive-In";

                                                    // secondary_dispo = currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Pick Up Cancelled").dispositionId;
                                                }
                                                serviceExit.customer_id = cusId;
                                                serviceExit.vehicle_vehicle_id = vehiId;
                                                serviceExit.wyzUser_id = userId;
                                                serviceExit.updatedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                                if (bookingMode != "Call Me Later" && bookingMode != "Other" && listingForm.ServiceBookingCancel == "Service Booking")
                                                {
                                                    serviceExit.serviceBookStatus_id = currentDisposition; //--changed on 05 - 05 - 2020
                                                }
                                                //serviceExit.callInteraction_id = callinteraction.id;
                                                db.servicebookeds.AddOrUpdate(serviceExit);
                                                db.SaveChanges();
                                                selectedWorkshop = serviceExit.workshop_id ?? default(long);

                                            }
                                        }
                                    }

                                    //********************************* Assigned Interaction *******************************
                                    if (bookingMode == "Cancelled")
                                    {
                                        if (listingForm.ServiceBookingCancel != "Service Booking")
                                        {
                                            ass_id = recordDisposition(0, "service", db, currentDisposition);//To get only Id
                                        }
                                        else
                                        {
                                            ass_id = recordDisposition(2, "service", db, currentDisposition);
                                        }

                                    }
                                    else if (bookingMode == "Other" || isSuperControl == true)
                                    {
                                        if (sr_disposition.othersSMR == "DNC")
                                        {
                                            customer cust = db.customers.FirstOrDefault(m => m.id == cusId);
                                            cust.doNotDisturb = true;
                                            db.customers.AddOrUpdate(cust);
                                            db.SaveChanges();
                                            ass_id = recordDisposition(2, "service", db, currentDisposition);
                                        }
                                        else if (sr_disposition.othersSMR == "Escalation" || isSuperControl == true)
                                        {
                                            long superCreId = 0;
                                            if (sr_disposition.othersSMR == "Escalation")
                                            {
                                                superCreId = sr_disposition.supCREId;
                                                ass_id = recordDisposition(2, "service", db, currentDisposition, superCreId, false, true);
                                            }
                                            else
                                            {
                                                superCreId = userId;
                                                ass_id = recordDisposition(2, "service", db, currentDisposition, superCreId, true, false);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ass_id = recordDisposition(2, "service", db, currentDisposition, userId);
                                    }



                                    //***************** Other Work End ************************

                                    //***************** CallInteraction starts******************
                                    callinteraction.callCount = 1;
                                    callinteraction.callDate = DateTime.Now.ToString("dd-MM-yyyy");
                                    callinteraction.callMadeDateAndTime = DateTime.Now;
                                    callinteraction.callTime = DateTime.Now.ToString("HH:mm:ss");
                                    callinteraction.dealerCode = Session["DealerCode"].ToString();
                                    if (Session["isCallInitiated"] != null)
                                    {
                                        callinteraction.isCallinitaited = "initiated";
                                        if (Session["MakeCallFrom"] != null)
                                        {
                                            callinteraction.makeCallFrom = Session["MakeCallFrom"].ToString();
                                        }
                                        else
                                        {
                                            callinteraction.makeCallFrom = "Service";
                                        }

                                        if (Session["AndroidUniqueId"] != null)
                                        {
                                            callinteraction.uniqueidForCallSync = int.Parse(Session["AndroidUniqueId"].ToString());
                                        }
                                        else if (Session["GSMUniqueId"] != null)
                                        {
                                            callinteraction.uniqueIdGSM = Session["GSMUniqueId"].ToString().Split(';')[0];
                                        }
                                    }

                                    callinteraction.callStatus = Convert.ToBoolean(Session["NCReason"]);
                                    if (Session["MakeCallFrom"] != null)
                                    {
                                        callinteraction.makeCallFrom = Session["MakeCallFrom"].ToString();
                                    }
                                    else
                                    {
                                        callinteraction.makeCallFrom = "Service";
                                    }
                                    if (Session["DialedNumber"] != null)
                                    {
                                        callinteraction.dailedNoIs = Session["DialedNumber"].ToString();
                                    }
                                    callinteraction.assignedInteraction_id = ass_id;
                                    callinteraction.customer_id = cusId;
                                    callinteraction.vehicle_vehicle_id = vehiId;
                                    callinteraction.wyzUser_id = userId;

                                    int tagCount = db.taggedassignments.Where(m => m.vehicle_id == vehiId && m.module_id == 1).Count();
                                    int taggId = 0;
                                    string camp = "";
                                    if (tagCount > 0)
                                    {
                                        if (db.taggedassignments.Count(m => m.vehicle_id == vehiId && m.module_id == 1 && m.active == true) > 0)
                                        {
                                            taggId = Convert.ToInt32(db.taggedassignments.FirstOrDefault(m => m.vehicle_id == vehiId && m.module_id == 1 && m.active == true).tagging_id);

                                            if (db.campaigns.Any(m => m.id == taggId))
                                            {
                                                camp = db.campaigns.FirstOrDefault(m => m.id == taggId).campaignName;
                                            }
                                        }

                                    }
                                    //ass_id = 0;
                                    callinteraction.agentName = Session["UserName"].ToString();
                                    callinteraction.campaign_id = db.assignedinteractions.FirstOrDefault(m => m.id == ass_id).campaign_id;
                                    callinteraction.tagging_id = taggId;
                                    callinteraction.tagging_name = camp;
                                    callinteraction.chasserCall = false;
                                    if (bookingMode == "Book My Service" || bookingMode == "Rescheduled")
                                    {
                                        callinteraction.serviceBooked_serviceBookedId = servicebooked.serviceBookedId;
                                    }
                                    else
                                    {
                                        if (serviceMaxId != 0)
                                        {
                                            if (listingForm.ServiceBookingCancel != "Service Booking" || bookingMode != "Cancelled")
                                            {
                                                callinteraction.serviceBooked_serviceBookedId = serviceExit.serviceBookedId;
                                            }
                                            else if (listingForm.ServiceBookingCancel == "Service Booking" && bookingMode == "Cancelled")
                                            {
                                                callinteraction.serviceBooked_serviceBookedId = serviceExit.serviceBookedId;
                                            }

                                        }

                                    }
                                    db.callinteractions.Add(callinteraction);
                                    db.SaveChanges();

                                    if (Session["GSMUniqueId"] != null)
                                    {
                                        gsmsynchdata gsm = new gsmsynchdata();
                                        gsm.Callinteraction_id = callinteraction.id;
                                        gsm.CallMadeDateTime = DateTime.Now;
                                        gsm.UniqueGsmId = Session["GSMUniqueId"].ToString().Split(';')[0];
                                        gsm.TenantUrl = Session["GSMUniqueId"].ToString().Split(';')[1];
                                        gsm.callFor = Convert.ToInt32(Session["UserRole1"]);
                                        db.gsmsynchdata.Add(gsm);
                                        db.SaveChanges();
                                    }
                                    //***************** CallInteraction End******************
                                    string remarks = "", comments = "";

                                    for (int i = 0; i < listingForm.remarksList.Count; i++)
                                    {
                                        if (remarks == "" && listingForm.remarksList[i] != "")
                                        {
                                            sr_disposition.remarks = listingForm.remarksList[i];
                                        }
                                    }

                                    for(int i = 0; i < listingForm.commentsList.Count; i++)
                                    {
                                        if (comments == "" && listingForm.commentsList[i] != "")
                                        {
                                            sr_disposition.comments = listingForm.commentsList[i];
                                        }
                                    }

                                    if (isSuperCre == true && isSuperControl == false)
                                    {
                                        sr_disposition.remarks = sr_disposition.remarks + " - By-" + Session["UserName"].ToString();
                                        sr_disposition.comments = sr_disposition.comments + " - By-" + Session["UserName"].ToString();
                                    }

                                    int upselCount = 0;
                                    //********************** Sr_Disposition Saving part ********************
                                    sr_disposition.InOutCallName = "OutCall";
                                    sr_disposition.cityName = listingForm.cityName;
                                    if (secondary_dispo != 0)
                                    {
                                        sr_disposition.callDispositionData_id = secondary_dispo;
                                    }
                                    else
                                    {
                                        sr_disposition.callDispositionData_id = currentDisposition;
                                    }
                                    if (sr_disposition.noServiceReason == "Dissatisfied with Insurance")
                                    {
                                        sr_disposition.noServiceReasonTaggedTo = listingForm.noServiceReasonTaggedTo1;
                                        sr_disposition.noServiceReasonTaggedToComments = listingForm.noServiceReasonTaggedToComments1;
                                    }
                                    if (sr_disposition.noServiceReason == "Dissatisfaction with Claim Issue")
                                    {
                                        sr_disposition.noServiceReasonTaggedTo = listingForm.noServiceReasonTaggedToClaims;
                                        sr_disposition.noServiceReasonTaggedToComments = listingForm.noServiceReasonTaggedToCommentsClaims;
                                    }
                                    if (sr_disposition.noServiceReason == "Already Serviced")
                                    {
                                        if (sr_disposition.ServicedAtOtherDealerRadio == "Autorized Workshop")
                                        {
                                            sr_disposition.authorised_Or_Not = "Autorized Workshop";
                                            sr_disposition.mileageAtService = listingForm.mileageAtService;
                                        }
                                        else if (sr_disposition.ServicedAtOtherDealerRadio == "Non Autorized workshop")
                                        {
                                            sr_disposition.dealerName = listingForm.dealerNameNonAuth;
                                            sr_disposition.dateOfService = listingForm.dateOfServiceNonAuth;
                                            sr_disposition.serviceType = listingForm.serviceTypeNonAuth;
                                            sr_disposition.mileageAtService = sr_disposition.mileageAsOnDate;
                                            sr_disposition.authorised_Or_Not = "Non Authorized workshop";

                                        }
                                    }
                                    sr_disposition.callInteraction_id = callinteraction.id;
                                    sr_disposition.callinteraction = callinteraction;
                                    sr_disposition.upsellCount = upselCount;
                                    sr_disposition.assignedToSA = "0";
                                    db.srdispositions.Add(sr_disposition);
                                    db.SaveChanges();
                                    //sr_disposition.

                                    if (bookingMode == "Book My Service" || bookingMode == "Rescheduled")
                                    {
                                        if (listingForm.LeadYes == "Capture Lead Yes")
                                        {
                                            foreach (var upsel in listingForm.upsellleads)
                                            {
                                                if (upsel.taggedTo != null)
                                                {
                                                    upselllead upsell = new upselllead();
                                                    upsell = upsel;
                                                    upsell.vehicle_vehicle_id = vehiId;
                                                    upsell.srDisposition_id = sr_disposition.id;
                                                    long upselUserId;
                                                    upselUserId = db.taggingusers.FirstOrDefault(m => m.upsellLeadId == upsel.upsellId).id;

                                                    if (upselUserId != 0)
                                                    {
                                                        upsell.taggingUsers_id = upselUserId;
                                                    }

                                                    upsell.taggedTo = upsel.taggedTo;
                                                    upsell.upSellType = upsel.upSellType;
                                                    //upsel.srDisposition_id = sr_disposition.id;
                                                    db.upsellleads.Add(upsell);
                                                    db.SaveChanges();
                                                    upselCount++;
                                                }
                                            }
                                        }
                                    }

                                    sr_disposition.upsellCount = upselCount;
                                    db.SaveChanges();

                                    if (listingForm.VehicleSoldYes == "VehicleSold Yes")
                                    {
                                        soldNewCustomerVehicles vehiclesold = new soldNewCustomerVehicles();
                                        vehiclesold.custmerId = cusId;
                                        vehiclesold.wyzuserid = userId;
                                        vehiclesold.customerFName = listingForm.customerFName;
                                        vehiclesold.customerLName = listingForm.customerLName;
                                        vehiclesold.Callinteraction_id = callinteraction.id;
                                        vehiclesold.state = listingForm.state;
                                        vehiclesold.city = listingForm.city;
                                        vehiclesold.vehicleRegNo = listingForm.vehicleRegNo;
                                        vehiclesold.chassisNo = listingForm.chassisNo;
                                        vehiclesold.variant = listingForm.variant;
                                        vehiclesold.model = listingForm.model;
                                        vehiclesold.dealershipName = listingForm.dealershipName;
                                        vehiclesold.saleDate = listingForm.saleDate;
                                        List<string> phoneList = listingForm.phoneList;
                                        string phoneNo = "";
                                        if (phoneList != null)
                                        {
                                            foreach (var phone in phoneList)
                                            {
                                                phoneNo = phoneNo + "," + phone;
                                            }
                                        }
                                        vehiclesold.addressLine1 = listingForm.addressLine1;
                                        vehiclesold.addressLine2 = listingForm.addressLine2;
                                        vehiclesold.pincode = listingForm.pincode;
                                        vehiclesold.initial = listingForm.initial;
                                        db.SoldNewCustomerVehicles.Add(vehiclesold);
                                        db.SaveChanges();
                                    }

                                    //auto SMS part
                                }
                            }
                        }
                        else
                        {
                            return "Record not found in assignedinteraction, cannot do booking...";
                        }
                        dbTransaction.Commit();
                        returnString = "True";
                        //---------------------- Sending SMS---------------------------------
                        if (Session["DealerCode"].ToString() == "KATARIA")
                        {
                            String noserviceinnerReason = string.Empty;
                            String noServiceReason = sr_disposition.noServiceReason;
                            if (sr_disposition.noServiceReason == "Distance from Dealer Location")
                            {
                                noserviceinnerReason = sr_disposition.DistancefromDealerLocationRadio;
                            }

                            if (servicebooked.typeOfPickup == "Customer Drive-In" || servicebooked.typeOfPickup == "Pickup Drop Required" || servicebooked.typeOfPickup == "Pickup Only")
                            {
                                noServiceReason = servicebooked.typeOfPickup;
                            }

                            if (sr_disposition.noServiceReason == "Already Serviced")
                            {
                                if (sr_disposition.reasonForHTML == "Serviced At My Dealer")
                                {
                                    noserviceinnerReason = "Serviced At My Dealer";
                                }
                                if (sr_disposition.reasonForHTML == "Serviced At Other Dealer")
                                {
                                    noserviceinnerReason = "Serviced At Other Dealer";
                                }
                            }
                            if (listingForm.issmsEnabled)
                            {
                                autosmsKataria(userId, vehiId, cusId, currentDisposition, noServiceReason, noserviceinnerReason, Convert.ToInt32(Session["UserRole1"]), 0, 0, 0, "", selectedWorkshop);
                                AutoWhatsappKataria(userId, vehiId, cusId, currentDisposition, noServiceReason, noserviceinnerReason, Convert.ToInt32(Session["UserRole1"]), 0);
                            }

                            if (db.dealers.Count(m => m.isemailenabled) > 0)
                            {

                                if (sr_disposition.noServiceReason == "Dissatisfaction with Claim Issue")
                                {
                                    noServiceReason = "Dissatisfied with previous service";
                                }
                                if (servicebooked.typeOfPickup == "Customer Drive-In")
                                {
                                    noServiceReason = servicebooked.typeOfPickup;
                                }
                                if (sr_disposition.noServiceReason == "Already Serviced")
                                {
                                    if (sr_disposition.reasonForHTML == "Serviced At My Dealer")
                                    {
                                        noserviceinnerReason = "Serviced At My Dealer";
                                    }
                                    if (sr_disposition.reasonForHTML == "Serviced At Other Dealer")
                                    {
                                        noserviceinnerReason = "Serviced At Other Dealer";
                                    }
                                }
                                if (Session["DealerCode"].ToString() == "KATARIA" || sr_disposition.followUpReason == "Dissatisfied with Last Service")
                                {
                                    noserviceinnerReason = "Dissatisfied with Last Service";
                                }
                                autoKatariaEmailDay(cusId, userId, vehiId, currentDisposition, noServiceReason, noserviceinnerReason, String.Empty, listingForm.fromEmailId, Convert.ToInt32(Session["UserRole1"]));
                            }
                        }

                        else

                        {

                            #region Sending Service Dispo related sms

                            if (bookingMode == "Confirmed" && listingForm.issmsEnabled == true)
                            {
                                if (Session["DealerCode"].ToString() == "ADVAITHHYUNDAI")
                                {
                                    autosmsday(userId, vehiId, cusId, "N-1 day", "Service", 0, 0, 0, "", selectedWorkshop);
                                }
                                else
                                {
                                    autosmsday(userId, vehiId, cusId, "Confirmed Booked", "Service", 0, 0, 0, "", selectedWorkshop);
                                }
                            }

                            if (servicebooked.isPickupRequired == true)
                            {
                                autosmsday(userId, vehiId, cusId, "PickUp Booking", "Service", 0, 0, 0, "", selectedWorkshop);
                                if (Session["DealerCode"].ToString() == "INDUS" || Session["DealerCode"].ToString() == "testdb")
                                {
                                    autosmsday(userId, vehiId, cusId, "SA Booking", "Service", 0, servicebooked.serviceAdvisor_advisorId ?? default(long), 0, "", selectedWorkshop);
                                }

                            }
                            else if (servicebooked.isPickupRequired == false && (bookingMode == "Book My Service" || bookingMode == "Rescheduled") && listingForm.issmsEnabled == true)
                            {
                                autosmsday(userId, vehiId, cusId, "Self Booking", "Service", 0, 0, 0, "", selectedWorkshop);
                                if (Session["DealerCode"].ToString() == "INDUS" || Session["DealerCode"].ToString() == "testdb"/* || Session["DealerCode"].ToString() == "KATARIA"*/)
                                {
                                    autosmsday(userId, vehiId, cusId, "SA Booking", "Service", 0, servicebooked.serviceAdvisor_advisorId ?? default(long), 0, "", selectedWorkshop);
                                }
                            }

                            //listingForm.driverId
                            if (listingForm.userfeedback != null && listingForm.userfeedback == "feedback Yes")
                            {
                                if (sr_disposition.departmentForFB != null)
                                {
                                    string customMsg = string.Empty;
                                    long deptId = long.Parse(sr_disposition.departmentForFB);
                                    customMsg = "Feedback: " + db.complainttypes.FirstOrDefault(m => m.id == deptId).departmentName;
                                    autosmsday(userId, vehiId, cusId, "Feedback", "Service", 0, 0, deptId, customMsg, 0);
                                }
                            }

                            if (sr_disposition.noServiceReason != null)
                            {
                                if (sr_disposition.noServiceReason == "Dissatisfied with previous service" ||
                                    sr_disposition.noServiceReason == "Dissatisfied with Sales" ||
                                    sr_disposition.noServiceReason == "Dissatisfied with Insurance" ||
                                    sr_disposition.noServiceReason == "Dissatisfaction with Claim Issue")
                                {
                                    autosmsday(userId, vehiId, cusId, "COMPLAINT", "Service", 0, 0, 0, "", 0);
                                    string customMsg = string.Empty;

                                    if (sr_disposition.noServiceReason == "Dissatisfied with previous service")
                                    {

                                        string type = "DISSAT1";
                                        customMsg = "DSAT : " + sr_disposition.noServiceReason;
                                        autosmsday(userId, vehiId, cusId, type, "Service", 0, 0, 0, customMsg, 0);
                                        //if (Session["DealerCode"].ToString() == "HARPREETFORD" || Session["DealerCode"].ToString() == "HANSHYUNDAI" || Session["DealerCode"].ToString() == "testdb")
                                        //{
                                        //    if (listingForm.fromEmailId != "" && listingForm.fromEmailId != null && listingForm.toEmailId != "" && listingForm.toEmailId != null)
                                        //    {
                                        //        autoEmailDay(cusId, userId, vehiId, "DISSATPREVIOUSSERVICE", listingForm.fromEmailId, listingForm.fromEPassword, listingForm.toEmailId, null, listingForm.toCCEmailId);
                                        //    }
                                        //}
                                        //else
                                        //{
                                        if ((db.emailtemplates.Count(m => m.inActive == false && m.emailType == "DISSATPREVIOUSSERVICE") > 0) && listingForm.fromEmailId != "" && listingForm.fromEmailId != null)
                                        {
                                            autoEmailDay(cusId, userId, vehiId, "DISSATPREVIOUSSERVICE", listingForm.fromEmailId, listingForm.fromEPassword, null, null, null, null, null);
                                        }
                                        //}
                                    }
                                    if (sr_disposition.noServiceReason == "Dissatisfaction with Claim Issue")
                                    {

                                        string type = "DISSAT1";
                                        customMsg = "DSAT : " + sr_disposition.noServiceReason;

                                        if ((db.emailtemplates.Count(m => m.inActive == false && m.emailType == "DISSATCLAIM") > 0) && listingForm.fromclaimEmailId != "" && listingForm.fromclaimEmailId != null)
                                        {
                                            autoEmailDay(cusId, userId, vehiId, "DISSATCLAIM", listingForm.fromclaimEmailId, listingForm.fromclaimEPassword, null, null, null, null, null);
                                        }
                                    }
                                    if (sr_disposition.noServiceReason == "Dissatisfied with Insurance")
                                    {

                                        string type = "DISSAT1";
                                        customMsg = "DSAT : " + sr_disposition.noServiceReason;

                                        if ((db.emailtemplates.Count(m => m.inActive == false && m.emailType == "SMRDISSATWITHINSURANCE") > 0) && listingForm.fromEmailId != "" && listingForm.fromEmailId != null)
                                        {
                                            autoEmailDay(cusId, userId, vehiId, "SMRDISSATWITHINSURANCE", listingForm.fromEmailId, listingForm.fromEPassword, null, null, null, null, null);
                                        }
                                    }
                                    else
                                    {
                                        int taggingid = 0;
                                        if (sr_disposition.noServiceReason == "Dissatisfied with Sales")
                                        {
                                            taggingid = 26;
                                        }
                                        //else if (sr_disposition.noServiceReason == "Dissatisfied with Insurance")
                                        //{
                                        //    taggingid = 27;
                                        //}
                                        else
                                        {
                                            taggingid = 28;
                                        }
                                        string type1 = "DISSAT2";
                                        customMsg = "DSAT : " + sr_disposition.noServiceReason;

                                        autosmsday(userId, vehiId, cusId, type1, "Service", taggingid, 0, 0, customMsg, 0);

                                    }
                                }
                            }

                            if (listingForm.userfeedbackAlreadyService != null && listingForm.userfeedbackAlreadyService == "feedback Yes AlreadyService")
                            {
                                if (sr_disposition.departmentForFB != null && sr_disposition.departmentForFB != "0")
                                {
                                    string customMsg = string.Empty;
                                    long deptId = long.Parse(sr_disposition.departmentForFB);
                                    customMsg = "Feedback: " + db.complainttypes.FirstOrDefault(m => m.id == deptId).departmentName;
                                    autosmsday(userId, vehiId, cusId, "Feedback", "Service", 0, 0, deptId, customMsg, 0);
                                }
                            }



                            #endregion

                        }


                    }
                    catch (Exception ex)
                    {
                        dbTransaction.Rollback();

                        if (ex.InnerException != null)
                        {
                            if (ex.InnerException.InnerException != null)
                            {
                                return ex.InnerException.InnerException.Message;
                            }
                            else
                            {
                                return ex.InnerException.Message;
                            }
                        }
                        else
                        {
                            return ex.Message;
                        }
                    }
                }
            }
            return returnString;
        }

        //****************************** Insurance SubmitData **********************
        /************* Insurance Save Chethan *******/

        public string nonContact(callinteraction callInter, ListingForm listing, insurancedisposition irData)
        {
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            long userId = Convert.ToInt32(Session["UserId"].ToString());
            int currentDisposition = 0;
            string submissionResult = "";
            long ass_id = 0;
            using (var db = new AutoSherDBContext())
            {
                using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        bool isSuperCre = false;
                        bool isINSSuperControl = false;
                        bool isUserControl = db.dealers.FirstOrDefault().userControl ?? default(bool);

                        if (Session["IsSuperCRE"] != null && Convert.ToBoolean(Session["IsSuperCRE"]) == true)
                        {
                            isSuperCre = true;

                            isINSSuperControl = db.dealers.FirstOrDefault().INSsuperControl ?? default(bool);
                        }

                        string callLogFrom = string.Empty;

                        if (Session["MakeCallFrom"] != null)
                        {
                            callLogFrom = Session["MakeCallFrom"].ToString();
                        }
                        else
                        {
                            callLogFrom = "bucket";
                        }

                        if (isINSSuperControl == false && callLogFrom != "bucket")
                        {
                            if (isSuperCre == true)
                            {
                                userId = db.insuranceassignedinteractions.FirstOrDefault(m => m.customer_id == cusId && m.vehicle_vehicle_id == vehiId).wyzUser_id ?? default(long);
                            }

                        }

                        if (listing.dispoNotTalk == "Other")
                        {
                            listing.dispoNotTalk = "NoOther";
                        }
                        currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == listing.dispoNotTalk).dispositionId;
                        long? insuranceMaxId = 0;
                        long? appId = Convert.ToInt64(Session["appointBookId"]);

                        insuranceMaxId = getInsuranceAppointmentStatus();
                        appointmentbooked insuranceExit = new appointmentbooked();
                        if (insuranceMaxId != 0)
                        {
                            insuranceExit = db.appointmentbookeds.FirstOrDefault(m => m.appointmentId == insuranceMaxId);
                            insuranceExit.customer_id = cusId;
                            insuranceExit.vehicle_id = vehiId;
                            insuranceExit.wyzuser_id = userId;
                            //insuranceExit.insuranceBookStatus_id = currentDisposition;
                            db.appointmentbookeds.AddOrUpdate(insuranceExit);
                            db.SaveChanges();
                        }

                        //**************** Updating AssignedInteraction *********************

                        ass_id = recordDisposition(2, "insurance", db, currentDisposition, 0, isSuperCre);

                        //****************************End***************************

                        //***************** CallInteraction starts******************
                        callInter.callCount = 1;
                        callInter.callDate = DateTime.Now.ToString("dd-MM-yyyy");
                        callInter.callMadeDateAndTime = DateTime.Now;
                        callInter.callTime = DateTime.Now.ToString("HH:mm:ss");
                        callInter.dealerCode = Session["DealerCode"].ToString();
                        // callInter.isCallinitaited = "No";

                        if (Session["isCallInitiated"] != null)
                        {
                            callInter.isCallinitaited = "Initiated";
                            if (Session["MakeCallFrom"] != null)
                            {
                                callInter.makeCallFrom = Session["MakeCallFrom"].ToString();
                            }
                            else
                            {
                                callInter.makeCallFrom = "insurance";
                            }

                            if (Session["UniqueId"] != null)
                            {
                                callInter.uniqueIdGSM = Session["UniqueId"].ToString();
                            }
                        }

                        callInter.callStatus = Convert.ToBoolean(Session["NCReason"]);
                        if (Session["MakeCallFrom"] != null)
                        {
                            callInter.makeCallFrom = Session["MakeCallFrom"].ToString();
                        }
                        else
                        {
                            callInter.makeCallFrom = "Service";
                        }
                        if (Session["DialedNumber"] != null)
                        {
                            callInter.dailedNoIs = Session["DialedNumber"].ToString();
                        }
                        //callInter.makeCallFrom = "insurance";
                        callInter.insuranceAssignedInteraction_id = ass_id;
                        callInter.customer_id = cusId;
                        callInter.vehicle_vehicle_id = vehiId;
                        callInter.wyzUser_id = userId;
                        callInter.agentName = Session["UserName"].ToString();
                        callInter.chasserCall = false;
                        callInter.campaign_id = db.insuranceassignedinteractions.FirstOrDefault(m => m.id == ass_id).campaign_id;
                        if (insuranceMaxId != 0)
                        {
                            callInter.appointmentBooked_appointmentId = insuranceExit.appointmentId;
                        }
                        int taggId = 0;
                        string camp = "";
                        callInter.tagging_id = taggId;
                        callInter.tagging_name = camp;
                        callInter.chasserCall = false;
                        db.callinteractions.Add(callInter);
                        db.SaveChanges();

                        if (Session["GSMUniqueId"] != null)
                        {
                            gsmsynchdata gsm = new gsmsynchdata();
                            gsm.Callinteraction_id = callInter.id;
                            gsm.CallMadeDateTime = DateTime.Now;
                            gsm.UniqueGsmId = Session["GSMUniqueId"].ToString().Split(';')[0];
                            gsm.TenantUrl = Session["GSMUniqueId"].ToString().Split(';')[1];
                            gsm.callFor = Convert.ToInt32(Session["UserRole1"]);
                            db.gsmsynchdata.Add(gsm);
                            db.SaveChanges();
                        }
                        string remarks = "", comments = "";

                        for (int i = 0; i < listing.remarksList.Count; i++)
                        {
                            if (remarks == "" && listing.remarksList[i] != "")
                            {
                                irData.customerComments = listing.remarksList[i];
                            }

                            if (comments == "" && listing.commentsList[i] != "")
                            {
                                irData.comments = listing.commentsList[i];
                            }
                        }


                        int upselCount = 0;

                        //********************** Insurance_Disposition Saving part ********************
                        irData.InOutCallName = "OutCall";
                        irData.cityName = listing.cityName;
                        irData.callDispositionData_id = currentDisposition;
                        irData.callInteraction_id = callInter.id;
                        irData.callinteraction = callInter;
                        irData.upsellCount = upselCount;

                        if (isSuperCre == true && isINSSuperControl == false)
                        {
                            irData.customerComments = irData.customerComments + " - By-" + Session["UserName"].ToString();
                            irData.comments = irData.comments + " - By-" + Session["UserName"].ToString();
                        }

                        db.insurancedispositions.Add(irData);
                        db.SaveChanges();
                        db.Database.ExecuteSqlCommand("call Triggerinsertinsurancecallhistrycube(@newid);", new MySqlParameter("@newid", callInter.id));
                        dbTransaction.Commit();
                        submissionResult = "True";
                        if (Session["DealerCode"].ToString() == "KATARIA" && listing.issmsEnabled)
                        {

                            autosmsKataria(userId, vehiId, cusId, currentDisposition, string.Empty, string.Empty, Convert.ToInt32(Session["UserRole1"]), 0, 0, 0, "", 0);
                        }
                    }
                    catch (Exception ex)
                    {
                        dbTransaction.Rollback();

                        if (ex.InnerException != null)
                        {
                            if (ex.InnerException.InnerException != null)
                            {
                                submissionResult = ex.InnerException.InnerException.Message;
                            }
                            else
                            {
                                submissionResult = ex.InnerException.Message;
                            }
                        }
                        else
                        {
                            submissionResult = ex.Message;
                        }

                    }
                }
            }

            return submissionResult;
        }

        public string insuranceDataSubmit(string action, callinteraction callInter, ListingForm listing, insurancedisposition irData, appointmentbooked appointmentbooked)
        {
            string submisstionResult = string.Empty;
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            long userId = Convert.ToInt32(Session["UserId"].ToString());
            using (var db = new AutoSherDBContext())
            {
                using (DbContextTransaction dbTra = db.Database.BeginTransaction())
                {
                    try
                    {
                        bool iskalyani = false;
                        if (Session["OEM"].ToString() == "MARUTI SUZUKI")
                        {
                            iskalyani = true;
                        }
                        submisstionResult = _insuranceOutBoundSubmit(callInter, irData, listing, appointmentbooked, action);
                        //if ("Book Appointment" == action)
                        //{
                        //    submisstionResult = _insuranceOutBoundSubmit(callInter, irData, listing, appointmentbooked, "Book Appointment");

                        //    if (iskalyani)
                        //    {
                        //        // submisstionResult = performKalyaniInsuranceBookingActions(callInter, irData, listing, appointmentbooked, "Book Appointment");
                        //    }
                        //    else
                        //    {
                        //        //submisstionResult = performInsuranceBookingActions(callInter, irData, listing, appointmentbooked, "Book Appointment");
                        //    }
                        //}
                        //else if ("Renewal Not Required" == action)
                        //{
                        //    submisstionResult = _insuranceOutBoundSubmit(callInter, irData, listing, appointmentbooked, "Renewal Not Required");

                        //    if (iskalyani)
                        //    {
                        //        //submisstionResult = performKalyaniInsuranceBookingActions(callInter, irData, listing, appointmentbooked, "Renewal Not Required");
                        //    }
                        //    else
                        //    {
                        //        //submisstionResult = performInsuranceBookingActions(callInter, irData, listing, appointmentbooked, "Renewal Not Required");
                        //    }
                        //}
                        //else if ("ConfirmedInsu" == action)
                        //{
                        //    submisstionResult = _insuranceOutBoundSubmit(callInter, irData, listing, appointmentbooked, "ConfirmedInsu");

                        //    if (iskalyani)
                        //    {
                        //        //    submisstionResult = performKalyaniInsuranceBookingActions(callInter, irData, listing, appointmentbooked, "ConfirmedInsu");
                        //    }
                        //    else
                        //    {
                        //        // submisstionResult = performInsuranceBookingActions(callInter, irData, listing, appointmentbooked, "ConfirmedInsu");
                        //    }
                        //}
                        ////else if ("Reschedule" == action)
                        ////{
                        ////    submisstionResult = performInsuranceBookingActions(callInter, irData, listing, appointmentbooked, "Service Not Required");
                        ////}
                        //else if ("Cancel Appointment" == action)
                        //{
                        //    submisstionResult = _insuranceOutBoundSubmit(callInter, irData, listing, appointmentbooked, "Cancel Appointment");

                        //    if (iskalyani)
                        //    {
                        //        //   submisstionResult = performKalyaniInsuranceBookingActions(callInter, irData, listing, appointmentbooked, "Cancel Appointment");
                        //    }
                        //    else
                        //    {
                        //        // submisstionResult = performInsuranceBookingActions(callInter, irData, listing, appointmentbooked, "Cancel Appointment");

                        //    }
                        //}
                        //else if ("Paid" == action)
                        //{
                        //    submisstionResult = _insuranceOutBoundSubmit(callInter, irData, listing, appointmentbooked, "Paid");

                        //    if (iskalyani)
                        //    {
                        //        //  submisstionResult = performKalyaniInsuranceBookingActions(callInter, irData, listing, appointmentbooked, "Paid");
                        //    }
                        //    else
                        //    {
                        //        //submisstionResult = performInsuranceBookingActions(callInter, irData, listing, appointmentbooked, "Paid");

                        //    }
                        //}
                        //else if ("Call Me Later" == action)
                        //{
                        //    submisstionResult = _insuranceOutBoundSubmit(callInter, irData, listing, appointmentbooked, "Call Me Later");

                        //    if (iskalyani)
                        //    {
                        //        //submisstionResult = performKalyaniInsuranceBookingActions(callInter, irData, listing, appointmentbooked, "Call Me Later");
                        //    }
                        //    else
                        //    {
                        //        //submisstionResult = performInsuranceBookingActions(callInter, irData, listing, appointmentbooked, "Call Me Later");
                        //    }
                        //}
                        //else if ("INS Others" == action)
                        //{
                        //    submisstionResult = _insuranceOutBoundSubmit(callInter, irData, listing, appointmentbooked, "INS Others");

                        //    if (iskalyani)
                        //    {
                        //        //submisstionResult = performKalyaniInsuranceBookingActions(callInter, irData, listing, appointmentbooked, "INS Others");
                        //    }
                        //    else
                        //    {
                        //        //submisstionResult = performInsuranceBookingActions(callInter, irData, listing, appointmentbooked, "INS Others");
                        //    }
                        //}
                        if (irData.renewalNotRequiredReason != null)
                        {
                            if (irData.renewalNotRequiredReason == "Dissatisfied with previous service" ||
                                irData.renewalNotRequiredReason == "Dissatisfied with Sales" ||
                                irData.renewalNotRequiredReason == "Dissatisfied with Insurance")
                            {

                                string customMsg = string.Empty;
                                if (irData.renewalNotRequiredReason == "Dissatisfied with previous service")
                                {

                                    string type = "DISSAT1";
                                    customMsg = "DSAT : " + irData.renewalNotRequiredReason;

                                    if ((db.emailtemplates.Count(m => m.inActive == false && m.emailType == "INSDISSATPREVIOUSSERVICE") > 0) && listing.fromEmailId != "" && listing.fromEmailId != null)
                                    {
                                        autoEmailDay(cusId, userId, vehiId, "INSDISSATPREVIOUSSERVICE", listing.fromEmailId, listing.fromEPassword, null, null, null, null, null);
                                    }

                                }

                                if (irData.renewalNotRequiredReason == "Dissatisfied with Insurance")
                                {

                                    string type = "DISSAT1";
                                    customMsg = "DSAT : " + irData.renewalNotRequiredReason;

                                    if ((db.emailtemplates.Count(m => m.inActive == false && m.emailType == "INSDISSATWITHINSURANCE") > 0) && listing.fromEmailId != "" && listing.fromEmailId != null)
                                    {
                                        autoEmailDay(cusId, userId, vehiId, "INSDISSATWITHINSURANCE", listing.fromEmailId, listing.fromEPassword, null, null, null, null, null);
                                    }

                                }

                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        dbTra.Rollback();
                        if (ex.InnerException != null)
                        {
                            if (ex.InnerException.InnerException != null)
                            {
                                return ex.InnerException.InnerException.Message;
                            }
                            else
                            {
                                return ex.InnerException.Message;
                            }
                        }
                        else
                        {
                            return ex.Message;
                        }
                    }
                }
                
            }
            return submisstionResult;
        }
        #region old first insu saving

        public string performInsuranceBookingActions(callinteraction callinteraction, insurancedisposition ir_disposition, ListingForm listingForm, appointmentbooked appointmentbooked, string bookingMode)
        {
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            long userId = Convert.ToInt32(Session["UserId"].ToString());
            long agentId = 0;
            int currentDisposition = 0;
            using (var db = new AutoSherDBContext())
            {
                using (DbContextTransaction dbTrans = db.Database.BeginTransaction())
                {
                    try
                    {

                        appointmentbooked appointExist = new appointmentbooked();
                        pickupdrop pickup = new pickupdrop();
                        long? appointMaxId = 0;
                        if (bookingMode == "Book Appointment" || bookingMode == "Renewal Not Required" || bookingMode == "ConfirmedInsu" || bookingMode == "Reschedule" || bookingMode == "Cancel Appointment" || bookingMode == "Paid" || bookingMode == "Call Me Later")
                        {
                            int/* currentDisposition = 0, */cancelDisposition = 0;
                            long ass_id = 0;
                            if (bookingMode == "Book Appointment")
                            {
                                currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Book Appointment").dispositionId;
                            }
                            else if (bookingMode == "Renewal Not Required")
                            {
                                currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Renewal Not Required").dispositionId;
                            }
                            else if (bookingMode == "ConfirmedInsu")
                            {
                                currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Confirmed").dispositionId;
                            }
                            //else if (bookingMode == "Reschedule")
                            //{
                            //    currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Reschedule").dispositionId;
                            //}
                            else if (bookingMode == "Cancel Appointment")
                            {
                                currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Cancelled").dispositionId;
                            }
                            else if (bookingMode == "Paid")
                            {
                                currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Paid").dispositionId;
                            }
                            else if (bookingMode == "Call Me Later")
                            {
                                currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Call Me Later").dispositionId;
                            }
                            if (bookingMode == "Book Appointment" || bookingMode == "Reschedule")
                            {
                                long appId = Convert.ToInt64(Session["appointBookId"]);
                                int appIdCount = db.appointmentbookeds.Where(m => m.appointmentId == appId && m.customer_id == cusId).Count();
                                if (appIdCount > 0)
                                {
                                    appointmentbooked rescheduleappointment = new appointmentbooked();
                                    callinteraction reschedulecallInteraction = new callinteraction();
                                    {
                                        if (Session["DealerCode"].ToString() != "PAWANHYUNDAI")
                                        {
                                            int countfield = db.appointmentbookeds.Where(m => m.appointmentId == appId && m.typeOfPickup == "Field").Count();
                                            if (countfield > 0)
                                            {
                                                long? id = db.appointmentbookeds.FirstOrDefault(m => m.appointmentId == appId).pickupDrop_id;
                                                db.pickupdrops.Where(x => x.id == id).ToList().ForEach(x => { x.timeFrom = null; x.timeTo = null; });
                                            }
                                        }
                                    }
                                    cancelDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Cancelled").dispositionId;
                                    rescheduleappointment = db.appointmentbookeds.Where(m => m.appointmentId == appId && m.customer_id == cusId).FirstOrDefault();
                                    rescheduleappointment.insuranceBookStatus_id = cancelDisposition;
                                    db.appointmentbookeds.AddOrUpdate(rescheduleappointment);
                                    db.SaveChanges();

                                }
                                appointmentbooked.customer_id = cusId;
                                appointmentbooked.chaserId = userId;
                                appointmentbooked.wyzuser_id = userId;
                                appointmentbooked.vehicle_id = vehiId;
                                appointmentbooked.insuranceBookStatus_id = currentDisposition;

                                if (appointmentbooked.typeOfPickup == "Field")
                                {
                                    if (Session["DealerCode"].ToString() != "PAWANHYUNDAI")
                                    {
                                        long from;
                                        long to;
                                        if (listingForm.time_To_insExist != 0 && listingForm.time_To_ins == 0 && listingForm.time_From_insExist != 0 && listingForm.time_To_ins == 0)
                                        {
                                            from = listingForm.time_From_insExist;
                                            to = listingForm.time_To_insExist + 1;

                                        }
                                        else
                                        {
                                            from = listingForm.time_From_ins;
                                            to = listingForm.time_To_ins + 1;
                                        }
                                        agentId = listingForm.insuAgentIdIns;
                                        bookingdatetime bookingFrom = db.bookingdatetimes.FirstOrDefault(x => x.id == from);
                                        bookingdatetime bookingTo = db.bookingdatetimes.FirstOrDefault(x => x.id == to);
                                        pickup.timeFrom = bookingFrom.startTime;
                                        pickup.timeTo = bookingTo.startTime;
                                        pickup.pickupDate = listingForm.appointmentScheduled;
                                        pickup.pickUpAddress = appointmentbooked.addressOfVisit;
                                        db.pickupdrops.AddOrUpdate(pickup);
                                        db.SaveChanges();

                                        appointmentbooked.appointmentDate = listingForm.appointmentScheduled;
                                        appointmentbooked.pickupDrop_id = pickup.id;
                                        appointmentbooked.appointmentFromTime = pickup.timeFrom.ToString() + " to " + pickup.timeTo.ToString();
                                        appointmentbooked.insuranceAgent_insuranceAgentId = agentId;
                                    }
                                    else if (Session["DealerCode"].ToString() == "PAWANHYUNDAI")
                                    {
                                        appointmentbooked.appointmentDate = listingForm.appointmentDateField;
                                        appointmentbooked.appointmentFromTime = listingForm.appointmentFromTimeField;
                                    }
                                }
                                if (appointmentbooked.typeOfPickup == "Online")
                                {
                                    appointmentbooked.appointmentDate = listingForm.appointmentDateOnline;
                                    appointmentbooked.appointmentFromTime = listingForm.appointmentFromTimeOnline;
                                }

                                db.appointmentbookeds.Add(appointmentbooked);
                                db.SaveChanges();
                            }
                            else
                            /*when user clicks renewal not required,cancel,confirm,call me later,paid if the appointment is already booked then appointment table updated otherwise no action required in appointment table.*/
                            {
                                appointMaxId = getInsuranceAppointmentStatus();
                                if (appointMaxId != 0)
                                {
                                    appointExist = db.appointmentbookeds.FirstOrDefault(m => m.appointmentId == appointMaxId);

                                    if (bookingMode == "ConfirmedInsu" || bookingMode == "Renewal Not Required" || bookingMode == "Cancel Appointment" || bookingMode == "Paid" || bookingMode == "Call Me Later")
                                    {


                                        if (bookingMode == "Cancel Appointment")
                                        {
                                            long appId = Convert.ToInt64(Session["appointBookId"]);

                                            //if appointment type is field  then cancel the appotment time from scheduler(using pickupdrop_id)
                                            if (Session["DealerCode"].ToString() != "PAWANHYUNDAI")
                                            {
                                                int countfield = db.appointmentbookeds.Where(m => m.appointmentId == appId && m.typeOfPickup == "Field").Count();
                                                if (countfield > 0)
                                                {
                                                    long? id = db.appointmentbookeds.FirstOrDefault(m => m.appointmentId == appId).pickupDrop_id;
                                                    db.pickupdrops.Where(x => x.id == id).ToList().ForEach(x => { x.timeFrom = null; x.timeTo = null; });
                                                }
                                            }

                                        }
                                        appointExist.customer_id = cusId;
                                        appointExist.vehicle_id = vehiId;
                                        appointExist.wyzuser_id = userId;
                                        appointExist.insuranceBookStatus_id = currentDisposition;
                                        db.appointmentbookeds.AddOrUpdate(appointExist);
                                        db.SaveChanges();
                                    }
                                }
                            }

                            ass_id = recordDisposition(2, "insurance", db, currentDisposition);

                            //***************** CallInteraction starts******************//

                            callinteraction.callCount = 1;
                            callinteraction.callDate = DateTime.Now.ToString("dd-MM-yyyy");
                            callinteraction.callMadeDateAndTime = DateTime.Now;
                            callinteraction.callTime = DateTime.Now.ToString("HH:mm:ss");
                            callinteraction.dealerCode = Session["DealerCode"].ToString();
                            if (Session["isCallInitiated"] != null)
                            {
                                callinteraction.isCallinitaited = "initiated";
                                if (Session["MakeCallFrom"] != null)
                                {
                                    callinteraction.makeCallFrom = Session["MakeCallFrom"].ToString();
                                }
                                else
                                {
                                    callinteraction.makeCallFrom = "insurance";
                                }

                                if (Session["AndroidUniqueId"] != null)
                                {
                                    callinteraction.uniqueidForCallSync = int.Parse(Session["AndroidUniqueId"].ToString());
                                }
                                else if (Session["GSMUniqueId"] != null)
                                {
                                    callinteraction.uniqueIdGSM = Session["GSMUniqueId"].ToString().Split(';')[0];
                                }
                            }


                            callinteraction.callStatus = Convert.ToBoolean(Session["NCReason"]);
                            if (Session["MakeCallFrom"] != null)
                            {
                                callinteraction.makeCallFrom = Session["MakeCallFrom"].ToString();
                            }
                            else
                            {
                                callinteraction.makeCallFrom = "insurance";
                            }
                            if (Session["DialedNumber"] != null)
                            {
                                callinteraction.dailedNoIs = Session["DialedNumber"].ToString();
                            }
                            callinteraction.insuranceAssignedInteraction_id = ass_id;
                            callinteraction.customer_id = cusId;
                            callinteraction.vehicle_vehicle_id = vehiId;
                            callinteraction.wyzUser_id = userId;

                            if (bookingMode == "Book Appointment" || bookingMode == "Reschedule")
                            {
                                callinteraction.appointmentBooked_appointmentId = appointmentbooked.appointmentId;
                            }
                            else
                            {
                                if (appointMaxId != 0)
                                {
                                    if (bookingMode == "Cancel Appointment")
                                    {
                                        callinteraction.appointmentBooked_appointmentId = appointExist.appointmentId;
                                    }
                                    else
                                    {
                                        callinteraction.appointmentBooked_appointmentId = appointExist.appointmentId;
                                    }

                                    //callinteraction.appointmentBooked_appointmentId = appointmentbooked.appointmentId;
                                }
                                else
                                {
                                }

                            }

                            int tagCount = db.taggedassignments.Where(m => m.vehicle_id == vehiId && m.module_id == 1).Count();
                            int taggId = 0;
                            string camp = "";
                            if (tagCount > 0)
                            {
                                taggId = Convert.ToInt32(db.taggedassignments.FirstOrDefault(m => m.vehicle_id == vehiId && m.module_id == 1 && m.active == true).tagging_id);
                                if (db.campaigns.Any(m => m.id == taggId))
                                {
                                    camp = db.campaigns.FirstOrDefault(m => m.id == taggId).campaignName;
                                }
                            }
                            callinteraction.agentName = Session["UserName"].ToString();
                            callinteraction.campaign_id = db.insuranceassignedinteractions.FirstOrDefault(m => m.id == ass_id).campaign_id;
                            callinteraction.tagging_id = taggId;
                            callinteraction.tagging_name = camp;
                            callinteraction.chasserCall = false;
                            db.callinteractions.Add(callinteraction);
                            db.SaveChanges();

                            if (Session["GSMUniqueId"] != null)
                            {
                                gsmsynchdata gsm = new gsmsynchdata();
                                gsm.Callinteraction_id = callinteraction.id;
                                gsm.CallMadeDateTime = DateTime.Now;
                                gsm.UniqueGsmId = Session["GSMUniqueId"].ToString().Split(';')[0];
                                gsm.TenantUrl = Session["GSMUniqueId"].ToString().Split(';')[1];

                                db.gsmsynchdata.Add(gsm);
                                db.SaveChanges();
                            }

                            long callId = callinteraction.id;

                            //***************** CallInteraction End******************


                            List<string> remakListData = listingForm.remarksList;
                            string remaks = "";
                            foreach (string sa in remakListData)
                            {
                                if (sa != null && sa != "")
                                {

                                    remaks = remaks + sa;
                                }

                            }
                            ir_disposition.comments = remaks;

                            List<string> addonListData = listingForm.addonsList;
                            string addonData = "";
                            if (addonListData != null)
                            {
                                foreach (string sa in addonListData)
                                {

                                    if (sa != null)
                                    {

                                        addonData = addonData + sa;
                                    }

                                }
                            }
                            ir_disposition.addons = addonData + "," + listingForm.otherAddon;
                            int upselCount = 0;
                            //********************** insurance_Disposition Saving part ********************
                            if (ir_disposition.typeOfAutherization == "Unauthorized Dealer")
                            {
                                ir_disposition.dateOfRenewal = listingForm.dateOfRenewalNonAuth;
                                ir_disposition.insuranceProvidedBy = listingForm.insuranceProvidedUnAuth;
                            }

                            if (listingForm.VehicleSoldYes == "VehicleSold Yes")
                            {
                                soldNewCustomerVehicles vehiclesold = new soldNewCustomerVehicles();
                                vehiclesold.custmerId = cusId;
                                vehiclesold.wyzuserid = userId;
                                vehiclesold.customerFName = listingForm.customerFName;
                                vehiclesold.customerLName = listingForm.customerLName;
                                vehiclesold.Callinteraction_id = callinteraction.id;
                                vehiclesold.state = listingForm.state;
                                vehiclesold.city = listingForm.city;
                                vehiclesold.vehicleRegNo = listingForm.vehicleRegNo;
                                vehiclesold.chassisNo = listingForm.chassisNo;
                                vehiclesold.variant = listingForm.variant;
                                vehiclesold.model = listingForm.model;
                                vehiclesold.dealershipName = listingForm.dealershipName;
                                vehiclesold.saleDate = listingForm.saleDate;
                                List<string> phoneList = listingForm.phoneList;
                                string phoneNo = "";
                                foreach (var phone in phoneList)
                                {
                                    phoneNo = phoneNo + "," + phone;
                                }
                                vehiclesold.addressLine1 = listingForm.addressLine1;
                                vehiclesold.addressLine2 = listingForm.addressLine2;
                                vehiclesold.pincode = listingForm.pincode;
                                vehiclesold.initial = listingForm.initial;
                                db.SoldNewCustomerVehicles.Add(vehiclesold);
                                db.SaveChanges();
                            }

                            if (listingForm.assignedSA != null)
                            {
                                long asignedSaId = db.taggingusers.FirstOrDefault(m => m.name == listingForm.assignedSA).id;
                                ir_disposition.assignedToSA = asignedSaId;
                            }

                            if (ir_disposition.renewalNotRequiredReason == "Dissatisfied with Insurance")
                            {
                                ir_disposition.noServiceReasonTaggedTo = listingForm.noServiceReasonTaggedTo1;
                                ir_disposition.noServiceReasonTaggedToComments = listingForm.noServiceReasonTaggedToComments1;
                            }

                            ir_disposition.InOutCallName = "OutCall";
                            // ir_disposition.cityName = listingForm.cityName;
                            ir_disposition.callDispositionData_id = currentDisposition;
                            ir_disposition.callInteraction_id = callinteraction.id;
                            ir_disposition.callinteraction = callinteraction;
                            ir_disposition.upsellCount = upselCount;
                            db.insurancedispositions.Add(ir_disposition);
                            db.SaveChanges();
                            //sr_disposition.

                            if (bookingMode == "Book Appointment" || bookingMode == "Reschedule")

                            {
                                if (listingForm.LeadYes == "Capture Lead Yes")
                                {
                                    if (listingForm.upsellleads != null)
                                        foreach (var upsel in listingForm.upsellleads)
                                        {
                                            if (upsel.taggedTo != null)
                                            {
                                                upsel.vehicle_vehicle_id = vehiId;
                                                upsel.insuranceDisposition_id = ir_disposition.id;
                                                upsel.taggedTo = upsel.taggedTo;
                                                long upselUserId;
                                                upselUserId = db.taggingusers.FirstOrDefault(m => m.upsellLeadId == upsel.upsellId).id;

                                                if (upselUserId != 0)
                                                {
                                                    upsel.taggingUsers_id = upselUserId;
                                                }
                                                db.upsellleads.Add(upsel);
                                                db.SaveChanges();
                                                upselCount++;
                                            }
                                        }
                                }
                            }
                            ir_disposition.upsellCount = upselCount++;
                            db.SaveChanges();

                            db.Database.ExecuteSqlCommand("call Triggerinsertinsurancecallhistrycube(@newid);", new MySqlParameter("@newid", callId));



                        }
                        dbTrans.Commit();

                        //-----------------------------Triggering Auto SMS ------------------------------
                        if (Session["DealerCode"].ToString() == "KATARIA" && listingForm.issmsEnabled)
                        {

                            autosmsKataria(userId, vehiId, cusId, currentDisposition, ir_disposition.renewalNotRequiredReason, string.Empty, Convert.ToInt32(Session["UserRole1"]), 0, 0, 0, "", 0);
                        }

                        else

                        {
                            if (bookingMode == "Book Appointment" || bookingMode == "Reschedule")
                            {
                                if (agentId != 0)
                                {
                                    autosmsday(userId, vehiId, cusId, "Field Executive", "Insurance", 0, agentId, 0, "", 0);
                                }
                                if (listingForm.issmsEnabled == true)
                                {
                                    autosmsday(userId, vehiId, cusId, "APPOINTMENT", "Insurance", 0, 0, 0, "", 0);
                                }
                            }
                            string customMsg = string.Empty;
                            //Compaints
                            if (ir_disposition.renewalNotRequiredReason != null && (ir_disposition.renewalNotRequiredReason == "Dissatisfied with previous service" || ir_disposition.renewalNotRequiredReason == "Dissatisfied with Sales" || ir_disposition.renewalNotRequiredReason == "Dissatisfied with Insurance"))
                            {
                                string smsTypeva = "COMPLAINT";
                                autosmsday(userId, vehiId, cusId, smsTypeva, "Insurance", 0, 0, 0, "COMPLAINT", 0);

                                // dissat sms
                                int taggingId = 0;
                                if (ir_disposition.renewalNotRequiredReason == "Dissatisfied with Sales")
                                {
                                    taggingId = 26;
                                }
                                else if (ir_disposition.renewalNotRequiredReason == "Dissatisfied with Insurance")
                                {
                                    taggingId = 27;
                                }
                                else
                                {
                                    taggingId = 29;
                                }

                                string smsType1 = "INSDISSAT";

                                customMsg = "INS DISSAT : " + ir_disposition.renewalNotRequiredReason;
                                autosmsday(userId, vehiId, cusId, smsType1, "Insurance", taggingId, 0, 0, customMsg, 0);

                            }

                            // feedback sms
                            if (listingForm.CustomerfeedbackRNR == "Yes")
                            {
                                long departmentId = long.Parse(ir_disposition.departmentForFB);

                                complainttype complainttype = db.complainttypes.FirstOrDefault(m => m.id == departmentId);

                                string dept = complainttype.departmentName;
                                customMsg = "INS Feedback : " + dept;
                                autosmsday(userId, vehiId, cusId, "INSFEEDBACK", "Insurance", 0, 0, departmentId, customMsg, 0);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        if (ex.InnerException != null)
                        {
                            if (ex.InnerException.InnerException != null)
                            {
                                return ex.InnerException.InnerException.Message;
                            }
                            else
                            {
                                return ex.InnerException.Message;
                            }
                        }
                        else
                        {
                            return ex.Message;
                        }
                    }
                }

            }
            return "True";
        }

        #endregion

        #region kalyani Insurance Saving Part
        //public string performKalyaniInsuranceBookingActions(callinteraction callinteraction, insurancedisposition ir_disposition, ListingForm listingForm, appointmentbooked appointmentbooked, string bookingMode)
        //{
        //    long cusId = Convert.ToInt32(Session["CusId"].ToString());
        //    long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
        //    long userId = Convert.ToInt32(Session["UserId"].ToString());
        //    long lastinsuraceId = 0;
        //    bool bookedExist = false;
        //    using (var db = new AutoSherDBContext())
        //    {
        //        using (DbContextTransaction dbTrans = db.Database.BeginTransaction())
        //        {
        //            try
        //            {

        //                appointmentbooked appointExist = new appointmentbooked();
        //                pickupdrop pickup = new pickupdrop();
        //                long? appointMaxId = 0;
        //                if (bookingMode == "Book Appointment" || bookingMode == "Renewal Not Required" || bookingMode == "ConfirmedInsu" || bookingMode == "Reschedule" || bookingMode == "Cancel Appointment" || bookingMode == "Paid" || bookingMode == "Call Me Later" || bookingMode == "INS Others")
        //                {
        //                    int currentDisposition = 0, cancelDisposition = 0;
        //                    long ass_id = 0;
        //                    if (bookingMode == "Book Appointment")
        //                    {
        //                        currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Book Appointment").dispositionId;
        //                    }
        //                    else if (bookingMode == "Renewal Not Required")
        //                    {
        //                        currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Renewal Not Required").dispositionId;
        //                    }
        //                    else if (bookingMode == "ConfirmedInsu")
        //                    {
        //                        currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Confirmed").dispositionId;
        //                    }
        //                    else if (bookingMode == "Cancel Appointment")
        //                    {
        //                        currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Cancelled").dispositionId;
        //                    }
        //                    else if (bookingMode == "Paid")
        //                    {
        //                        currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Paid").dispositionId;
        //                    }
        //                    else if (bookingMode == "Call Me Later")
        //                    {
        //                        currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Call Me Later").dispositionId;
        //                    }
        //                    else if (bookingMode == "INS Others")
        //                    {
        //                        currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "INS Others").dispositionId;
        //                    }


        //                    if (bookingMode == "Book Appointment" || bookingMode == "Reschedule")
        //                    {
        //                        long appId = Convert.ToInt64(Session["appointBookId"]);
        //                        int appIdCount = db.appointmentbookeds.Where(m => m.appointmentId == appId && m.customer_id == cusId).Count();
        //                        if (appIdCount > 0)
        //                        {
        //                            appointmentbooked rescheduleappointment = new appointmentbooked();
        //                            cancelDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Cancelled").dispositionId;
        //                            rescheduleappointment = db.appointmentbookeds.Where(m => m.appointmentId == appId && m.customer_id == cusId).FirstOrDefault();
        //                            rescheduleappointment.insuranceBookStatus_id = cancelDisposition;
        //                            db.appointmentbookeds.AddOrUpdate(rescheduleappointment);
        //                            db.SaveChanges();
        //                            bookedExist = true;

        //                        }
        //                        appointmentbooked.customer_id = cusId;
        //                        appointmentbooked.chaserId = userId;
        //                        appointmentbooked.wyzuser_id = userId;
        //                        appointmentbooked.vehicle_id = vehiId;
        //                        appointmentbooked.insuranceBookStatus_id = currentDisposition;

        //                        db.appointmentbookeds.Add(appointmentbooked);
        //                        db.SaveChanges();
        //                        if (appointmentbooked.typeOfPickup == "Field")
        //                        {
        //                            if (bookedExist)
        //                            {
        //                                fieldFirbaseUpdation(db,"Reschedule", appointmentbooked.appointmentId, 0, appId, 0, 0, false, lastinsuraceId);
        //                            }
        //                            else
        //                            {
        //                                fieldFirbaseUpdation(db,"New", appointmentbooked.appointmentId, 0, 0, 0, 0, false, lastinsuraceId);
        //                            }
        //                        }


        //                    }
        //                    else
        //                    /*when user clicks renewal not required,cancel,confirm,call me later,paid if the appointment is already booked then appointment table updated otherwise no action required in appointment table.*/
        //                    {
        //                        appointMaxId = getInsuranceAppointmentStatus();
        //                        if (appointMaxId != 0)
        //                        {
        //                            appointExist = db.appointmentbookeds.FirstOrDefault(m => m.appointmentId == appointMaxId);

        //                            if (bookingMode == "ConfirmedInsu" || bookingMode == "Renewal Not Required" || bookingMode == "Cancel Appointment" || bookingMode == "Paid" || bookingMode == "Call Me Later" || bookingMode == "INS Others")
        //                            {
        //                                appointExist.customer_id = cusId;
        //                                appointExist.vehicle_id = vehiId;
        //                                appointExist.wyzuser_id = userId;
        //                                appointExist.insuranceBookStatus_id = currentDisposition;
        //                                db.appointmentbookeds.AddOrUpdate(appointExist);
        //                                db.SaveChanges();
        //                            }
        //                        }
        //                    }


        //                    ass_id = recordDisposition(2, "insurance", db, currentDisposition);

        //                    //***************** CallInteraction starts******************//

        //                    callinteraction.callCount = 1;
        //                    callinteraction.callDate = DateTime.Now.ToString("dd-MM-yyyy");
        //                    callinteraction.callMadeDateAndTime = DateTime.Now;
        //                    callinteraction.callTime = DateTime.Now.ToString("HH:mm:ss");
        //                    callinteraction.dealerCode = Session["DealerCode"].ToString();
        //                    if (Session["isCallInitiated"] != null)
        //                    {
        //                        callinteraction.isCallinitaited = "initiated";
        //                        if (Session["MakeCallFrom"] != null)
        //                        {
        //                            callinteraction.makeCallFrom = Session["MakeCallFrom"].ToString();
        //                        }
        //                        else
        //                        {
        //                            callinteraction.makeCallFrom = "insurance";
        //                        }

        //                        if (Session["AndroidUniqueId"] != null)
        //                        {
        //                            callinteraction.uniqueidForCallSync = int.Parse(Session["AndroidUniqueId"].ToString());
        //                        }
        //                        else if (Session["GSMUniqueId"] != null)
        //                        {
        //                            callinteraction.uniqueIdGSM = Session["GSMUniqueId"].ToString().Split(';')[0];
        //                        }
        //                    }
        //                    else
        //                    {
        //                        callinteraction.isCallinitaited = "NotDialed";
        //                        if (Session["MakeCallFrom"] != null)
        //                        {
        //                            callinteraction.makeCallFrom = Session["MakeCallFrom"].ToString();
        //                        }
        //                        else
        //                        {
        //                            callinteraction.makeCallFrom = "insurance";
        //                        }
        //                    }
        //                    if (Session["DialedNumber"] != null)
        //                    {
        //                        callinteraction.dailedNoIs = Session["DialedNumber"].ToString();
        //                    }
        //                    callinteraction.insuranceAssignedInteraction_id = ass_id;
        //                    callinteraction.customer_id = cusId;
        //                    callinteraction.vehicle_vehicle_id = vehiId;
        //                    callinteraction.wyzUser_id = userId;

        //                    if (bookingMode == "Book Appointment" || bookingMode == "Reschedule")
        //                    {
        //                        callinteraction.appointmentBooked_appointmentId = appointmentbooked.appointmentId;
        //                    }
        //                    else
        //                    {
        //                        if (appointMaxId != 0)
        //                        {
        //                            //if(bookingMode!= "Cancel Appointment")
        //                            //{
        //                            callinteraction.appointmentBooked_appointmentId = appointExist.appointmentId;
        //                            //}

        //                            //callinteraction.appointmentBooked_appointmentId = appointmentbooked.appointmentId;
        //                        }
        //                        else
        //                        {
        //                        }

        //                    }

        //                    int tagCount = db.taggedassignments.Where(m => m.vehicle_id == vehiId && m.module_id == 1 && m.active == true).Count();
        //                    int taggId = 0;
        //                    string camp = "";
        //                    if (tagCount > 0)
        //                    {
        //                        if (db.taggedassignments.Any(m => m.vehicle_id == vehiId && m.module_id == 1 && m.active == true))
        //                        {
        //                            taggId = Convert.ToInt32(db.taggedassignments.FirstOrDefault(m => m.vehicle_id == vehiId && m.module_id == 1 && m.active == true).tagging_id);

        //                            if (db.campaigns.Any(m => m.id == taggId))
        //                            {
        //                                camp = db.campaigns.FirstOrDefault(m => m.id == taggId).campaignName;
        //                            }
        //                        }
        //                    }



        //                    callinteraction.agentName = Session["UserName"].ToString();
        //                    callinteraction.campaign_id = db.insuranceassignedinteractions.FirstOrDefault(m => m.id == ass_id).campaign_id;
        //                    callinteraction.tagging_id = taggId;
        //                    callinteraction.tagging_name = camp;
        //                    callinteraction.chasserCall = false;
        //                    db.callinteractions.Add(callinteraction);
        //                    db.SaveChanges();


        //                    if (Session["GSMUniqueId"] != null)
        //                    {
        //                        gsmsynchdata gsm = new gsmsynchdata();
        //                        gsm.Callinteraction_id = callinteraction.id;
        //                        gsm.CallMadeDateTime = DateTime.Now;
        //                        gsm.UniqueGsmId = Session["GSMUniqueId"].ToString().Split(';')[0];
        //                        gsm.TenantUrl = Session["GSMUniqueId"].ToString().Split(';')[1];

        //                        db.gsmsynchdata.Add(gsm);
        //                        db.SaveChanges();
        //                    }
        //                    long callId = callinteraction.id;

        //                    //***************** CallInteraction End******************


        //                    List<string> remakListData = listingForm.remarksList;
        //                    string remaks = "";
        //                    foreach (string sa in remakListData)
        //                    {
        //                        if (sa != null && sa != "")
        //                        {

        //                            remaks = remaks + sa;
        //                        }

        //                    }
        //                    List<string> feedbackListData = listingForm.customerfeedbackList;
        //                    string feedback = "";
        //                    foreach (string sa in feedbackListData)
        //                    {
        //                        if (sa != null && sa != "")
        //                        {

        //                            feedback = feedback + sa;
        //                        }

        //                    }
        //                    ir_disposition.comments = remaks;
        //                    ir_disposition.customerComments = feedback;

        //                    List<string> addonListData = listingForm.addonsList;
        //                    string addonData = "";
        //                    if (addonListData != null)
        //                    {
        //                        foreach (string sa in addonListData)
        //                        {

        //                            if (sa != null)
        //                            {

        //                                addonData = addonData + sa;
        //                            }

        //                        }
        //                    }
        //                    ir_disposition.addons = addonData + "," + listingForm.otherAddon;
        //                    int upselCount = 0;
        //                    //********************** insurance_Disposition Saving part ********************
        //                    if (ir_disposition.typeOfAutherization == "Unauthorized Dealer")
        //                    {
        //                        ir_disposition.dateOfRenewal = listingForm.dateOfRenewalNonAuth;
        //                        ir_disposition.insuranceProvidedBy = listingForm.insuranceProvidedUnAuth;
        //                    }

        //                    if (listingForm.VehicleSoldYes == "VehicleSold Yes")
        //                    {
        //                        soldNewCustomerVehicles vehiclesold = new soldNewCustomerVehicles();
        //                        vehiclesold.custmerId = cusId;
        //                        vehiclesold.wyzuserid = userId;
        //                        vehiclesold.customerFName = listingForm.customerFName;
        //                        vehiclesold.customerLName = listingForm.customerLName;
        //                        vehiclesold.state = listingForm.state;
        //                        vehiclesold.city = listingForm.city;
        //                        vehiclesold.vehicleRegNo = listingForm.vehicleRegNo;
        //                        vehiclesold.chassisNo = listingForm.chassisNo;
        //                        vehiclesold.variant = listingForm.variant;
        //                        vehiclesold.model = listingForm.model;
        //                        vehiclesold.dealershipName = listingForm.dealershipName;
        //                        vehiclesold.saleDate = listingForm.saleDate;
        //                        List<string> phoneList = listingForm.phoneList;
        //                        string phoneNo = "";
        //                        foreach (var phone in phoneList)
        //                        {
        //                            phoneNo = phoneNo + "," + phone;
        //                        }
        //                        vehiclesold.addressLine1 = listingForm.addressLine1;
        //                        vehiclesold.addressLine2 = listingForm.addressLine2;
        //                        vehiclesold.pincode = listingForm.pincode;
        //                        vehiclesold.initial = listingForm.initial;
        //                        db.SoldNewCustomerVehicles.Add(vehiclesold);
        //                        db.SaveChanges();
        //                    }

        //                    if (listingForm.assignedSA != null)
        //                    {
        //                        long asignedSaId = db.taggingusers.FirstOrDefault(m => m.name == listingForm.assignedSA).id;
        //                        ir_disposition.assignedToSA = asignedSaId;
        //                    }

        //                    if (ir_disposition.renewalNotRequiredReason == "Dissatisfied with Insurance")
        //                    {
        //                        ir_disposition.noServiceReasonTaggedTo = listingForm.noServiceReasonTaggedTo1;
        //                        ir_disposition.noServiceReasonTaggedToComments = listingForm.noServiceReasonTaggedToComments1;
        //                    }
        //                    // if (bookingMode == "INS Others" && (listingForm.othersINS == "DNM" || listingForm.othersINS == "DNC"))

        //                    ir_disposition.othersINS = listingForm.othersINS;

        //                    if (listingForm.othersINS == "Policy Drop")
        //                    {
        //                        fieldFirbaseUpdation(db,"New", 0, ass_id, 0, 0, 0, false, lastinsuraceId);
        //                    }

        //                    ir_disposition.InOutCallName = "OutCall";
        //                    ir_disposition.callDispositionData_id = currentDisposition;
        //                    ir_disposition.callInteraction_id = callinteraction.id;
        //                    ir_disposition.callinteraction = callinteraction;
        //                    ir_disposition.upsellCount = upselCount;
        //                    db.insurancedispositions.Add(ir_disposition);
        //                    db.SaveChanges();
        //                    //sr_disposition.

        //                    if (bookingMode == "Book Appointment" || bookingMode == "Reschedule")

        //                    {
        //                        if (listingForm.LeadYes == "Capture Lead Yes")
        //                        {
        //                            if (listingForm.upsellleads != null)
        //                                foreach (var upsel in listingForm.upsellleads)
        //                                {
        //                                    if (upsel.taggedTo != null)
        //                                    {
        //                                        upsel.vehicle_vehicle_id = vehiId;
        //                                        upsel.insuranceDisposition_id = ir_disposition.id;
        //                                        db.upsellleads.Add(upsel);
        //                                        db.SaveChanges();
        //                                        upselCount++;
        //                                    }
        //                                }
        //                        }
        //                    }
        //                    ir_disposition.upsellCount = upselCount++;
        //                    db.SaveChanges();

        //                    db.Database.ExecuteSqlCommand("call Triggerinsertinsurancecallhistrycube(@newid);", new MySqlParameter("@newid", callId));



        //                }
        //                dbTrans.Commit();

        //                //------------------------- Triggering AutoSMS ----------------------------

        //                if (bookingMode == "Book Appointment" || bookingMode == "Reschedule")
        //                {
        //                    autosmsday(userId, vehiId, cusId, "APPOINTMENT", "Insurance", 0, 0, 0, "", 0);
        //                }

        //                string customMsg = string.Empty;
        //                //Complaints
        //                if (ir_disposition.renewalNotRequiredReason != null && (ir_disposition.renewalNotRequiredReason == "Dissatisfied with previous service" || ir_disposition.renewalNotRequiredReason == "Dissatisfied with Sales" || ir_disposition.renewalNotRequiredReason == "Dissatisfied with Insurance"))
        //                {
        //                    string smsTypeva = "COMPLAINT";
        //                    autosmsday(userId, vehiId, cusId, smsTypeva, "Insurance", 0, 0, 0, "COMPLAINT", 0);

        //                    // dissat sms
        //                    int taggingId = 0;
        //                    if (ir_disposition.renewalNotRequiredReason == "Dissatisfied with Sales")
        //                    {
        //                        taggingId = 26;
        //                    }
        //                    else if (ir_disposition.renewalNotRequiredReason == "Dissatisfied with Insurance")
        //                    {
        //                        taggingId = 27;
        //                    }
        //                    else
        //                    {
        //                        taggingId = 29;
        //                    }

        //                    string smsType1 = "INSDISSAT";

        //                    customMsg = "INS DISSAT : " + ir_disposition.renewalNotRequiredReason;
        //                    autosmsday(userId, vehiId, cusId, smsType1, "Insurance", taggingId, 0, 0, customMsg, 0);

        //                }

        //                // feedback sms
        //                if (listingForm.CustomerfeedbackRNR == "Yes")
        //                {
        //                    long departmentId = long.Parse(ir_disposition.departmentForFB);

        //                    complainttype complainttype = db.complainttypes.FirstOrDefault(m => m.id == departmentId);

        //                    string dept = complainttype.departmentName;
        //                    customMsg = "INS Feedback : " + dept;
        //                    autosmsday(userId, vehiId, cusId, "INSFEEDBACK", "Insurance", 0, 0, departmentId, customMsg, 0);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                dbTrans.Rollback();
        //                if (ex.Message.Contains("inner exception"))
        //                {
        //                    return ex.InnerException.Message;
        //                }
        //                else
        //                {
        //                    return ex.Message;
        //                }
        //            }
        //        }

        //    }
        //    return "True";
        //}

        #endregion

        #region Field Execution Firebase Adding and Updating 

        ///***** *********/
        ////public bool fieldFirbaseUpdation( AutoSherDBContext db, string action, long newappId, long newasgnintId, long oldappId, long oldasgnintId, long tobepushed, bool ispolicy, long lastinsId)
        ////{
        ////  //  try
        ////   // {
        ////     //   using (var db = new AutoSherDBContext())
        ////       // {
        ////            fieldexecutivefirebaseupdation fieldexecutivefirebaseupdation = new fieldexecutivefirebaseupdation();

        ////            if (action == "New")
        ////            {
        ////                fieldexecutivefirebaseupdation.appointmentbookedid = newappId;
        ////                fieldexecutivefirebaseupdation.insassignedid = newasgnintId;
        ////                fieldexecutivefirebaseupdation.tobepushed = 0;
        ////                db.Fieldexecutivefirebaseupdations.Add(fieldexecutivefirebaseupdation);
        ////            }
        ////            else if (action == "Reschedule")
        ////            {
        ////        if (ispolicy)
        ////        {
        ////            if (db.Fieldexecutivefirebaseupdations.Any(u => u.insassignedid == oldasgnintId))
        ////            {
        ////                var fieldsexecutives = db.Fieldexecutivefirebaseupdations.FirstOrDefault(u => u.insassignedid == oldasgnintId);
        ////            fieldsexecutives.insassignedid = newasgnintId;
        ////            fieldsexecutives.tobepushed = 1;
        ////            db.Fieldexecutivefirebaseupdations.AddOrUpdate(fieldsexecutives);
        ////            }
        ////        }
        ////        else
        ////        {
        ////            if (db.appointmentbookeds.Any(m => m.appointmentId == oldappId))
        ////            {
        ////                var appintmnts = db.appointmentbookeds.FirstOrDefault(m => m.appointmentId == oldappId);
        ////                appintmnts.lastinsuranceagentid = lastinsId;
        ////                db.appointmentbookeds.AddOrUpdate(appintmnts);
        ////                if (db.Fieldexecutivefirebaseupdations.Any(u => u.appointmentbookedid == oldappId))
        ////                {
        ////                    var fieldsexecutives = db.Fieldexecutivefirebaseupdations.FirstOrDefault(u => u.appointmentbookedid == oldappId);
        ////                fieldsexecutives.appointmentbookedid = newappId;
        ////                fieldsexecutives.tobepushed = 1;
        ////                db.Fieldexecutivefirebaseupdations.AddOrUpdate(fieldsexecutives);
        ////            }
        ////            }
        ////        }

        ////            }
        ////            db.SaveChanges();

        ////       // }
        ////  //  }
        ////    //catch (Exception ex)
        ////    //{

        ////    //}
        ////    return true;
        ////}


        /***************** Chethan End **************************/
        #endregion

        #region Supporting Function
        public ActionResult getDealer()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    var dealerList = db.dealerpanels.Select(m => new { workshopCode = m.workshopCode, dealerName = m.dealerName }).ToList();
                    return Json(new { success = true, dealerPanellist = dealerList });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }


        }

        //************************************* Api function ******************
        public List<smstemplate> getSMSTemplate(string typeofDis)
        {
            List<smstemplate> smstemplates = new List<smstemplate>();
            //try
            //{

            using (var db = new AutoSherDBContext())
            {
                if (typeofDis == "Service")
                {
                    smstemplates = db.smstemplates.Where(m => (m.deliveryType == "Manual" || m.deliveryType == "Both"/* || m.deliveryType == "Auto"*/) && (m.moduletype == 1 || m.moduletype == 3) && m.inActive == false && m.isWhatsapp == false).ToList();
                }
                else if (typeofDis == "Insurance")
                {
                    smstemplates = db.smstemplates.Where(m => (m.deliveryType == "Manual" || m.deliveryType == "Both" || m.deliveryType == "Auto") && (m.moduletype == 2 || m.moduletype == 3) && m.inActive == false && m.isWhatsapp == false).ToList();
                }
                else if (typeofDis == "PSF" || typeofDis == "PSFComMgr")
                {
                    smstemplates = db.smstemplates.Where(m => (m.deliveryType == "Manual" || m.deliveryType == "Both") && (m.moduletype == 4 || m.moduletype == 3) && m.inActive == false && m.isWhatsapp == false).ToList();
                }
            }
            //}
            //catch (Exception ex)
            //{

            //}
            return smstemplates;
        }

        public List<emailtemplate> getEmailTemplateList(string typeofDis)
        {
            List<emailtemplate> template = new List<emailtemplate>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (typeofDis == "Service")
                    {
                        template = db.emailtemplates.Where(m => m.sendingType == "Manual" && (m.moduleType == 1 || m.moduleType == 3) && m.inActive == false).ToList();
                    }
                    else if (typeofDis == "Insurance")
                    {
                        template = db.emailtemplates.Where(m => m.sendingType == "Manual" && (m.moduleType == 3 || m.moduleType == 2) && m.inActive == false).ToList();
                    }
                    else if (typeofDis == "PSF")
                    {
                        template = db.emailtemplates.Where(m => m.sendingType == "Manual" && (m.moduleType == 3 || m.moduleType == 4) && m.inActive == false).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return template;
        }


        public ActionResult getUniqueID()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    long last_id = db.uniqueidforcalls.Max(m => m.id);
                    int last_num = db.uniqueidforcalls.FirstOrDefault(m => m.id == last_id).callinitiating_id;
                    last_num = last_num + 1;

                    uniqueidforcall uniqueidforcall = new uniqueidforcall();
                    uniqueidforcall.callinitiating_id = last_num;
                    db.uniqueidforcalls.Add(uniqueidforcall);
                    db.SaveChanges();

                    return Json(new { success = true, unqId = uniqueidforcall.id });
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = false });
        }

        public ActionResult initializetatcloudPhone(string phNum)
        {
            long cusId, vehiId;
            Dictionary<string, string> dict = (Dictionary<string, string>)Session["CurLogDetails"];
            cusId = Convert.ToInt32(Session["CusId"].ToString());
            vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            int WyzUserId = Convert.ToInt32(Session["UserId"].ToString());
            try
            {
                string Authorization = string.Empty;
                using (var db = new AutoSherDBContext())
                {
                    tatateleservicescredintials apiDetails = db.Tatateleservicescredintials.FirstOrDefault(m => m.apiType == "clicktocall");

                    Authorization = apiDetails.authorizationkey;
                    Session["DialedNumber"] = phNum;


                    string baseURL = apiDetails.apiurl;
                    string agentphoneNuber = db.wyzusers.FirstOrDefault(m => m.id == WyzUserId).phoneNumber;
                    if (Authorization == null || Authorization == "")
                    {
                        return Json(new { success = false, error = "Unable to connect, Authorization Key Not Found" });
                    }

                    dynamic requestbody = new JObject();
                    requestbody.agent_number = agentphoneNuber;
                    requestbody.destination_number = phNum;
                    requestbody.get_call_id = 1;


                    WebRequest request = WebRequest.Create(baseURL);
                    var httprequest = (HttpWebRequest)request;

                    httprequest.PreAuthenticate = true;
                    httprequest.Method = "POST";
                    httprequest.ContentType = "application/json";
                    httprequest.Headers["Authorization"] = Authorization;
                    httprequest.Accept = "application/json";

                    using (var streamWriter = new StreamWriter(httprequest.GetRequestStream()))
                    {
                        var bodyContent = JsonConvert.SerializeObject(requestbody);
                        streamWriter.Write(bodyContent);

                        streamWriter.Flush();
                        streamWriter.Close();
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

                    dynamic api_result = JObject.Parse(response_string);
                    bool success = api_result.success;
                    if (success == false)
                    {
                        string error = api_result.message;
                        Session["NCReason"] = false;
                        return Json(new { success = false, error = error });
                    }
                    else if (success == true)
                    {
                        Session["NCReason"] = true;
                        Session["isCallInitiated"] = "initiated";
                        Session["GSMUniqueId"] = api_result.call_id + ";tatateleservices";
                        return Json(new { success = true });
                    }
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
            return Json(new { success = false });
        }

        public ActionResult initializeCall(string phNum, long uniqueid)
        {
            long cusId, vehiId, UniqId;
            Dictionary<string, string> dict = (Dictionary<string, string>)Session["CurLogDetails"];
            cusId = Convert.ToInt32(Session["CusId"].ToString());
            vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            string vehiRegNum = "", custName = "unkown";
            int WyzUserId = Convert.ToInt32(Session["UserId"].ToString());
            try
            {
                string androidKey = "";
                if (HomeController.wyzCRM1.Contains(Session["DealerCode"].ToString()))
                {
                    androidKey = System.Configuration.ConfigurationManager.AppSettings["androidKeyWyzCrm1"];
                }
                else if (HomeController.wyzCRM.Contains(Session["DealerCode"].ToString()))
                {
                    androidKey = System.Configuration.ConfigurationManager.AppSettings["androidKeyWyzCrm"];
                }
                else if (HomeController.autosherpa1.Contains(Session["DealerCode"].ToString()))
                {
                    androidKey = System.Configuration.ConfigurationManager.AppSettings["androidKeyautosherpa1"];
                }
                else if (HomeController.WyzCrmNew.Contains(Session["DealerCode"].ToString()))
                {
                    androidKey = System.Configuration.ConfigurationManager.AppSettings["androidKeywyznew"];

                }
                else if (HomeController.autosherpa3.Contains(Session["DealerCode"].ToString()))
                {
                    androidKey = System.Configuration.ConfigurationManager.AppSettings["androidKeyautosherpa3"];
                }
                using (var db = new AutoSherDBContext())
                {
                    customer cust = db.customers.FirstOrDefault(m => m.id == cusId);
                    vehicle vehicle = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehiId);
                    UniqId = db.uniqueidforcalls.FirstOrDefault(m => m.id == uniqueid).callinitiating_id;
                    Session["DialedNumber"] = phNum;
                    if (cust.customerName != null)
                    {
                        custName = cust.customerName;
                    }


                    if (vehicle.vehicleRegNo != null)
                    {
                        vehiRegNum = vehicle.vehicleRegNo;
                    }
                    else if (vehicle.chassisNo != null)
                    {
                        vehiRegNum = vehicle.chassisNo;
                    }

                    string baseURL = "https://fcm.googleapis.com/fcm/send";
                    string toKey = db.wyzusers.FirstOrDefault(m => m.id == WyzUserId).registrationId;
                    if (toKey == null || toKey == "")
                    {
                        return Json(new { success = false, error = "Unable to connect, no reg.key found" });
                    }

                    dynamic data1 = new JObject();
                    data1.to = toKey;
                    dynamic data2 = new JObject();
                    data2.command = "Intiate";
                    data2.phonenumber = phNum;
                    data2.id = UniqId;
                    data2.makeCallFrom = "service";
                    data2.type = "Web";
                    data2.customername = custName;
                    data2.priority = "high";
                    data2.vehicleRegNo = vehiRegNum;
                    data1.data = data2;


                    //var body = JsonConvert.SerializeObject(data1);

                    WebRequest request = WebRequest.Create(baseURL);
                    var httprequest = (HttpWebRequest)request;

                    httprequest.PreAuthenticate = true;
                    httprequest.Method = "POST";
                    httprequest.ContentType = "application/json";
                    httprequest.Headers["Authorization"] = "key=" + androidKey;
                    httprequest.Accept = "application/json";

                    using (var streamWriter = new StreamWriter(httprequest.GetRequestStream()))
                    {
                        var bodyContent = JsonConvert.SerializeObject(data1);
                        streamWriter.Write(bodyContent);

                        streamWriter.Flush();
                        streamWriter.Close();
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

                    dynamic api_result = JObject.Parse(response_string);
                    int success = api_result.success;
                    long multiCast = api_result.multicast_id;
                    if (success == 0)
                    {
                        string error = api_result.results[0].error;
                        Session["NCReason"] = false;
                        return Json(new { success = false, error = error });
                    }
                    else if (success == 1)
                    {
                        Session["NCReason"] = true;
                        Session["isCallInitiated"] = "initiated";
                        Session["AndroidUniqueId"] = UniqId;
                        return Json(new { success = true });
                    }
                    //string status=response_string.
                    Session["DialedNumber"] = phNum;

                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
            return Json(new { success = false });
        }

        public ActionResult initializeGSMCall(string phNum)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");

            int UserId = Convert.ToInt32(Session["UserId"].ToString());

            try
            {
                int cusId = Convert.ToInt32(Session["CusId"].ToString());
                int vehiId = Convert.ToInt32(Session["VehiId"].ToString());

                using (var db = new AutoSherDBContext())
                {
                    wyzuser wyzuser = db.wyzusers.Include("tenant").FirstOrDefault(m => m.id == UserId);

                    string API_key = wyzuser.gsmRegistrationId;
                    string url = wyzuser.tenant.tenentAddress;
                    string PhNum = phNum.Trim();
                    string tenentShortAddress = wyzuser.tenant.gsmip;

                    //var match = Regex.Match(tenentShortAddress, @"\b(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\b");
                    //if (match.Success)
                    //{
                    //    tenentShortAddress= match.Captures[0].ToString();
                    //}


                    WebRequest request = WebRequest.Create(url);
                    HttpWebRequest httpRequest = (HttpWebRequest)request;

                    //string status=response_string.
                    Session["DialedNumber"] = PhNum;

                    httpRequest.Method = "POST";
                    httpRequest.ContentType = "application/json";
                    httpRequest.Accept = "application/json";
                    httpRequest.Headers["Authorization"] = "key=" + API_key;

                    logger.Info("\n----------GSM API Key ------ " + API_key + "URL" + url);

                    if (PhNum[0] == '0')
                    {
                        PhNum = PhNum.Substring(1, PhNum.Length - 1);
                    }

                    logger.Info("\n----------GSM CALL Initiated at ------ DateTime " + DateTime.Now + "\n User " + wyzuser.userName + "\n UserId:" + UserId + " || CustId:" + cusId + " || VehicleId:" + vehiId);
                    logger.Info("\n----------GSM CALL Values ------ " + wyzuser.extensionId + "ExtensionId" + phNum);

                    dynamic data = new JObject();
                    //data.extn = API_key;
                    data.extn = wyzuser.extensionId;
                    data.mobile = PhNum;
                    logger.Info("\n----------GSM Body ------ " + JsonConvert.SerializeObject(data));

                    using (var streamWritter = new StreamWriter(httpRequest.GetRequestStream()))
                    {
                        string bodyContect = JsonConvert.SerializeObject(data);

                        streamWritter.Write(bodyContect);
                        streamWritter.Flush();
                        streamWritter.Close();
                        logger.Info("\n----------GSM Body ------ " + data);

                    }

                    HttpWebResponse response = null;
                    response = (HttpWebResponse)httpRequest.GetResponse();

                    string resposonse_string = string.Empty;
                    using (var stream = response.GetResponseStream())
                    {
                        StreamReader sr = new StreamReader(stream);
                        resposonse_string = sr.ReadToEnd();
                        sr.Close();
                    }
                    logger.Info("\n----------resposonse_string Body ------ " + resposonse_string);

                    dynamic api_result = JObject.Parse(resposonse_string);
                    string status = api_result.status;
                    //double uniqueId=0;
                    string uniqueId = string.Empty;

                    if (api_result.uniqueid != null)
                    {
                        uniqueId = api_result.uniqueid.ToString();
                    }

                    if (status == "1")
                    {
                        Session["isCallInitiated"] = "initiated";
                        Session["GSMUniqueId"] = uniqueId + ";" + tenentShortAddress;
                        Session["NCReason"] = true;

                        logger.Info("\n----------GSM CALL Completed at ------ DateTime " + DateTime.Now + "\n ----UniqueId: " + uniqueId + " \n User " + wyzuser.userName + "\n UserId:" + UserId + " || CustId:" + cusId + " || VehicleId:" + vehiId);
                    }
                    else
                    {
                        string msg = Convert.ToString(api_result.message);
                        Session["NCReason"] = false;

                        return Json(new { success = false, error = msg });
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
                logger.Info("\n----------GSM CALL Exception at ------ DateTime " + exception);



                return Json(new { success = false, error = ex.Message.ToString() });

            }

            return Json(new { success = true });
        }

        public ActionResult initializetatknowlarityPhone(string phNum)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            long cusId, vehiId;
            Dictionary<string, string> dict = (Dictionary<string, string>)Session["CurLogDetails"];
            cusId = Convert.ToInt32(Session["CusId"].ToString());
            vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            int WyzUserId = Convert.ToInt32(Session["UserId"].ToString());
            string Message = string.Empty;
            try
            {
                logger.Info("\n Knwolarity Click to call  Started at - {0} Phone Number {1} \n ", DateTime.Now, phNum);

                using (var db = new AutoSherDBContext())
                {
                    phNum = phNum.Trim();
                    knowlaritycredintials knowlarityDetails = db.Knowlaritycredintials.FirstOrDefault(m => m.apiType == "clicktocall");
                    Session["DialedNumber"] = phNum;
                    string baseURL = knowlarityDetails.apiurl;
                    string Authorization = knowlarityDetails.authorizationkey;
                    string Accesskey = knowlarityDetails.acceskey;
                    var agenDetails = db.wyzusers.FirstOrDefault(m => m.id == WyzUserId);

                    if ((Authorization == null || Authorization == "") && (Accesskey == null || Accesskey == ""))
                    {
                        logger.Info("\n Knwolarity Click to call Authorization || Accesskey Not Found  \n ");
                        return Json(new { success = false, error = "Unable to connect, Authorization Key Not Found" });
                    }
                    if (phNum.Length == 10)
                    {
                        phNum = "+91" + phNum;
                    }
                    else if (phNum.Length == 12)
                    {
                        phNum = "+" + phNum;
                    }

                    dynamic requestbody = new JObject();
                    requestbody.k_number = knowlarityDetails.knumber;
                    requestbody.agent_number = agenDetails.phoneNumber;
                    requestbody.customer_number = phNum;
                    requestbody.caller_id = agenDetails.knowlarityCaller_id;

                    baseURL = baseURL.Replace("{channel}", knowlarityDetails.Channel);

                    WebRequest request = WebRequest.Create(baseURL);
                    var httprequest = (HttpWebRequest)request;

                    httprequest.PreAuthenticate = true;
                    httprequest.Method = "POST";
                    httprequest.ContentType = "application/json";
                    httprequest.Headers["x-api-key"] = Accesskey;
                    httprequest.Headers["Authorization"] = Authorization;
                    httprequest.Accept = "application/json";

                    using (var streamWriter = new StreamWriter(httprequest.GetRequestStream()))
                    {
                        var bodyContent = JsonConvert.SerializeObject(requestbody);
                        streamWriter.Write(bodyContent);
                        logger.Info("\n Knwolarity Click to call  Request Body - {0} \n ", bodyContent);


                        streamWriter.Flush();
                        streamWriter.Close();
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
                    logger.Info("\n Knwolarity Click to call  Response - {0} \n ", response_string);

                    dynamic api_result = JObject.Parse(response_string);
                    if (response_string.Contains("success"))
                    {
                        var suceesResponse = api_result.success;
                        Session["NCReason"] = true;
                        Session["isCallInitiated"] = "initiated";
                        Session["GSMUniqueId"] = suceesResponse.call_id + ";knowlarity";
                        Message = suceesResponse.message;
                        return Json(new { success = true, Message = Message });
                        //return Json(new { success = true, Message = Message, request = JsonConvert.SerializeObject(requestbody), response_string, response = JsonConvert.SerializeObject(api_result) });
                    }
                    else
                    {
                        var errorResponse = api_result.error;
                        Session["NCReason"] = false;
                        Message = errorResponse.message;
                        return Json(new { success = false, Message = Message });
                    }
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, Message = ex.Message });
                logger.Info("\n Knwolarity Click to call  Exception  - {1} Phone Number {1} \n ", ex.Message, phNum);

            }
        }
        public ActionResult getLoyality()
        {
            try
            {
                string regNo = Session["VehiReg"].ToString();
                string baseURL = "http://182.73.132.44:8100/api/customer/GetAvailblePointBasedOnChasis";
                //string toKey = db.wyzusers.SingleOrDefault(m => m.id == WyzUserId).registrationId;

                dynamic data = new JObject();
                data.Card_ID = null;
                data.CardNumber = regNo;
                data.Dealer_ID = null;


                //var body = JsonConvert.SerializeObject(data1);

                WebRequest request = WebRequest.Create(baseURL);
                var httprequest = (HttpWebRequest)request;

                httprequest.PreAuthenticate = true;
                httprequest.Method = "POST";
                httprequest.ContentType = "application/json";
                httprequest.Headers["Authorization"] = "AS:3560014d6d9f5fc2b702c682444df08929c903a8";
                //httprequest.Accept = "application/json";

                using (var streamWriter = new StreamWriter(httprequest.GetRequestStream()))
                {
                    var bodyContent = JsonConvert.SerializeObject(data);
                    streamWriter.Write(bodyContent);

                    streamWriter.Flush();
                    streamWriter.Close();
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

                dynamic api_result = JObject.Parse(response_string);

                int loyalityPoint = api_result.AvailablePoints;
                string cardNo = api_result.CardNumber;

                return Json(new { success = true, loyalityPoint = loyalityPoint, cardNo = cardNo });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message.ToString() });
            }
        }

        //SMS
        public ActionResult getTemplateMessage(int smsId, int locId, string msgType)
        {

            if (msgType != "email")
            {
                Session["smsId"] = smsId;
                Session["locId"] = locId;
                //Session["locId"] = 0;
            }
            int UserId = Convert.ToInt32(Session["UserId"].ToString());
            int vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            try
            {
                //smstemplate template = new smstemplate();
                using (var db = new AutoSherDBContext())
                {

                    if (msgType == "sms")
                    {
                        string str = @"CALL sendsms(@inwyzuser_id,@invehicle_id,@inlocid,@insmsid,@ininsid);";

                        MySqlParameter[] sqlParameter = new MySqlParameter[]
                        {
                       new MySqlParameter("@inwyzuser_id",UserId.ToString()),
                       new MySqlParameter("@invehicle_id",vehiId.ToString()),
                       new MySqlParameter("@inlocid",locId.ToString()),
                       new MySqlParameter("@insmsid",smsId.ToString()),
                       new MySqlParameter("@ininsid","0"),
                        };

                        var incomingTemplate = db.Database.SqlQuery<string>(str, sqlParameter).FirstOrDefault();

                        if (incomingTemplate != null)
                        {
                            string template = incomingTemplate.ToString();
                            return Json(new { success = true, sms = template });
                        }
                        else
                        {
                            return Json(new { success = false, error = "SMS Template Text Is Empty!." });
                        }



                    }
                    else
                    {
                        string emailSub = "", emailTemplate = "";

                        emailSub = getEmailBodyAndSub(UserId.ToString(), vehiId.ToString(), locId.ToString(), smsId.ToString(), "0", 0, "1");
                        emailTemplate = getEmailBodyAndSub(UserId.ToString(), vehiId.ToString(), locId.ToString(), smsId.ToString(), "0", 0, "2");

                        return Json(new { success = true, emailSub, emailTemplate });
                    }


                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        public ActionResult sendSMS(string phNum, string smstemplate)
        {
            int smsId = Convert.ToInt32(Session["smsId"].ToString());
            int locId = 0;

            phNum = phNum.Trim();
            //if (Session["DealerCode"].ToString() == "KATARIA" && phNum.Length == 10)
            //{
            //    phNum = "91" + phNum;
            //}

            if (Session["locId"] != null)
            {
                Convert.ToInt32(Session["locId"].ToString());
            }
            int custId = Convert.ToInt32(Session["CusId"].ToString());
            int vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            int UserId = Convert.ToInt32(Session["UserId"].ToString());

            try
            {
                smstemplate template = new smstemplate();
                smsparameter parameter = new smsparameter();
                using (var db = new AutoSherDBContext())
                {
                    template = db.smstemplates.FirstOrDefault(m => m.smsId == smsId);
                    parameter = db.smsparameters.FirstOrDefault();

                    string APIURL = string.Empty;
                    string uri = template.smsAPI;
                    string message = smstemplate;

                    if (Session["DealerCode"].ToString() == "HANSHYUNDAI")
                    {
                        APIURL = uri.Replace("MOBILENUMBER", phNum.Trim()).Replace("MESSAGECONTENT", message);
                    }

                    //else if (Session["DealerCode"].ToString() == "ADVAITHHYUNDAI" || Session["DealerCode"].ToString() == "ADVAITHHYUNDAI" || Session["DealerCode"].ToString() == "ADVAITHHYUNDAI")
                    //{
                    //    {
                    //        APIURL = uri + parameter.senderid + "=" + template.dealerName + "&" + parameter.phone + "=" + phNum.Trim() + "&" + parameter.message + "=" + message + "&template_id=" + template.templateId;
                    //    }
                    //}
                    else
                    {
                        APIURL = uri + parameter.phone + "=" + phNum.Trim() + "&" + parameter.message + "=" + message + "&" + parameter.senderid + "=" + template.dealerName;
                    }



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

                    smsinteraction smsinteraction = new smsinteraction();

                    smsinteraction.apiurl = APIURL;
                    smsinteraction.interactionDate = DateTime.Now.ToString("dd-MM-yyyy");
                    smsinteraction.interactionDateAndTime = DateTime.Now;
                    smsinteraction.interactionTime = DateTime.Now.ToString("HH:mm:ss");
                    smsinteraction.interactionType = "Text Msg";
                    smsinteraction.responseFromGateway = response_string;
                    smsinteraction.customer_id = custId;
                    smsinteraction.vehicle_vehicle_id = vehiId;
                    smsinteraction.wyzUser_id = UserId;
                    smsinteraction.mobileNumber = phNum;
                    smsinteraction.smsType = template.smsId.ToString();
                    smsinteraction.smsMessage = message;
                    smsinteraction.isAutoSMS = false;


                    if (Session["DealerCode"].ToString() == "INDUS" || Session["DealerCode"].ToString() == "INDUSTV")
                    {
                        Regex r = new Regex(@"^\d+$");
                        if (r.IsMatch(response_string))
                        {
                            //only number
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

                    else if (Session["DealerCode"].ToString() == "BLUEHYUNDAI")
                    {

                        if (response_string.Contains(phNum))
                        {
                            smsinteraction.smsStatus = true;
                            smsinteraction.reason = "Send Successfully";
                        }
                        else
                        {
                            smsinteraction.smsStatus = false;
                            smsinteraction.reason = response_string;
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

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult sendWhatsappMsg(string phNum, string msg, string reason, string responseFrom)
        {
            int smsId = 0;
            int locId = 0;

            if (responseFrom == "message")
            {
                smsId = Convert.ToInt32(Session["smsId"].ToString());
                locId = Convert.ToInt32(Session["locId"].ToString());
            }

            int custId = Convert.ToInt32(Session["CusId"].ToString());
            int vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            int UserId = Convert.ToInt32(Session["UserId"].ToString());

            try
            {
                using (var db = new AutoSherDBContext())
                {

                    smsinteraction smsinteraction = new smsinteraction();

                    smsinteraction.interactionDate = DateTime.Now.ToString("dd-MM-yyyy");
                    smsinteraction.interactionDateAndTime = DateTime.Now;
                    smsinteraction.interactionTime = DateTime.Now.ToString("HH:mm:ss");
                    smsinteraction.interactionType = "WhatsApp Msg";
                    smsinteraction.responseFromGateway = "";
                    smsinteraction.customer_id = custId;
                    smsinteraction.vehicle_vehicle_id = vehiId;
                    smsinteraction.wyzUser_id = UserId;
                    smsinteraction.mobileNumber = phNum.Substring(2, 10);
                    if (responseFrom == "message")
                    {
                        smsinteraction.smsType = smsId.ToString();
                    }
                    smsinteraction.smsMessage = msg;
                    smsinteraction.isAutoSMS = false;

                    if (responseFrom == "message")
                    {
                        smsinteraction.smsType = smsId.ToString();
                    }

                    if (reason == "Send Successfully")
                    {
                        smsinteraction.smsStatus = true;
                    }
                    else
                    {
                        smsinteraction.smsStatus = false;
                    }
                    smsinteraction.reason = reason;
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

        public ActionResult uploadDocument(CallLoggingViewModel callLogging)
        {
            //if(document!=null || document!="")
            //{
            documentuploadhistory doc = new documentuploadhistory();
            doc = callLogging.docHistory;
            List<string> fileError = new List<string>();
            string fileString = string.Empty;
            string uploadedFileName = string.Empty;
            string finalFileLink = string.Empty;
            string fileUploadPath = Server.MapPath("~/UploadedFiles/" + Session["DealerCode"].ToString() + "/" + Session["UserName"].ToString() + "/");
            string fileURL = string.Empty;
            string fileLink = string.Empty;
            int custId = Convert.ToInt32(Session["CusId"].ToString());
            int vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            try
            {
                using (var db = new AutoSherDBContext())
                {

                    db.Configuration.LazyLoadingEnabled = false;

                    if (!Directory.Exists(fileUploadPath))
                    {
                        Directory.CreateDirectory(fileUploadPath);
                    }



                    foreach (var file in doc.fileList)
                    {
                        string extension = string.Empty;
                        extension = Path.GetExtension(file.FileName);//newly added
                        extension = extension.ToLower();
                        if (extension == ".docx" || extension == ".png" || extension == ".xlsx" || extension == ".xls" || extension == ".jpg" || extension == ".jpeg" || extension == ".pdf")
                        {
                            //if(extension==".pdf")
                            //{
                            //    long fileSize = file.ContentLength * 1024;

                            //    if(fileSize<=1024)
                            //    {s

                            //    }
                            //}
                            string savingFileName = file.FileName.Split('.')[0] + "_" + custId + "_" + DateTime.Now.ToString("dd-MM-yyyy").Replace("-", "") + "_" + DateTime.Now.ToString("HH:mm:ss").Replace(":", "") + extension;
                            //file.FileName = savingFileName;
                            //file.SaveAs(fileUploadPath + Path.GetFileName(file.FileName));
                            file.SaveAs(fileUploadPath + savingFileName);
                            fileString = fileString + fileUploadPath + savingFileName + ";";
                            uploadedFileName = uploadedFileName + file.FileName + ";";
                            finalFileLink = finalFileLink + savingFileName + ";";
                        }
                        else
                        {
                            fileError.Add("cann't upload File " + file.FileName);
                        }
                    }
                    int UserId = Convert.ToInt32(Session["UserId"].ToString());

                    uploadedFileName = uploadedFileName.Remove((uploadedFileName.Length - 1));
                    finalFileLink = finalFileLink.Remove((finalFileLink.Length - 1));
                    fileString = fileString.Remove((fileString.Length - 1));

                    doc.customerId = custId;
                    doc.filePath = fileString;
                    doc.uploadFileName = uploadedFileName;
                    doc.uploadDateTime = DateTime.Now;
                    doc.user = Session["UserName"].ToString();
                    doc.userId = UserId;

                    db.documentuploadhistories.Add(doc);
                    db.SaveChanges();
                    //fileUploadPath = fileUploadPath.Remove(0);
                    fileURL = "/UploadedFiles/" + Session["DealerCode"].ToString() + "/" + Session["UserName"].ToString() + "/";
                    fileLink = finalFileLink + "&" + fileURL;
                }
                var UserName = Session["UserName"].ToString();
                //return JavaScript("fileSuccess()");
                return Json(new { success = true, files = fileLink, UserName, docId = doc.id });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
            //}


            return Json(new { success = false });
        }

        //Getting Already Stored Files 
        public ActionResult getDocuments()
        {
            //string fileURL = "/UploadedFiles/" + Session["DealerCode"].ToString() + "/" + Session["UserName"].ToString() + "/";
            string fileLink = string.Empty;
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long userId = Convert.ToInt32(Session["UserId"].ToString());
            string usernames = string.Empty, docFileName = string.Empty;
            string mainFileStream = string.Empty;
            string fileNames = string.Empty;
            string deptName = string.Empty;
            string uploadedDateTime = string.Empty;
            string indiFileName = string.Empty;
            string docId = string.Empty;
            List<documentuploadhistory> doc = new List<documentuploadhistory>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (db.documentuploadhistories.Any(m => m.customerId == cusId))
                    {
                        doc = db.documentuploadhistories.Where(m => m.customerId == cusId).ToList();
                    }

                    for (int i = 0; i < doc.Count(); i++)
                    {
                        string fileURL = "/UploadedFiles/" + Session["DealerCode"].ToString() + "/" + doc[i].user + "/";

                        string uploadedFileName = string.Empty;
                        List<string> filePath = new List<string>();
                        filePath = doc[i].filePath.Split(';').ToList();
                        for (int k = 0; k < filePath.Count(); k++)
                        {
                            if (k == 0)
                            {
                                uploadedFileName = (filePath[k].Substring(filePath[k].IndexOf(doc[i].user))).Split('\\')[1];
                            }
                            else
                            {
                                uploadedFileName = uploadedFileName + ";" + (filePath[k].Substring(filePath[k].IndexOf(doc[i].user))).Split('\\')[1];
                            }
                        }

                        fileLink = uploadedFileName + "#" + fileURL;
                        if (i == 0)
                        {
                            docId = doc[i].id.ToString();
                            usernames = doc[i].user;
                            mainFileStream = fileLink;
                            docFileName = doc[i].uploadFileName;
                            fileNames = doc[i].documentName;
                            deptName = doc[i].deptName;
                            uploadedDateTime = Convert.ToDateTime(doc[i].uploadDateTime).ToString("dd-MM-yyyy") + " " + Convert.ToDateTime(doc[i].uploadDateTime).TimeOfDay;
                        }
                        else
                        {
                            docId = docId + "," + doc[i].id.ToString();
                            docFileName = docFileName + "%" + doc[i].uploadFileName;
                            mainFileStream = mainFileStream + "%" + fileLink;
                            usernames = usernames + "," + doc[i].user;
                            fileNames = fileNames + "," + doc[i].documentName;
                            deptName = deptName + "," + doc[i].deptName;
                            uploadedDateTime = uploadedDateTime + "," + Convert.ToDateTime(doc[i].uploadDateTime).ToString("dd-MM-yyyy") + " " + Convert.ToDateTime(doc[i].uploadDateTime).TimeOfDay;
                        }
                        fileLink = string.Empty;
                    }
                    return Json(new { success = true, files = mainFileStream, docId = docId, docFileName = docFileName, fileName = fileNames, usernames = usernames, deptName = deptName, uploadedDateTime = uploadedDateTime });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        //getting emails List for sending Email
        public ActionResult getEmailCredentials(long docId, string getCredentialFor)
        {
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long userId = Convert.ToInt32(Session["UserId"].ToString());
            List<emailcredential> emailCred = new List<emailcredential>();
            documentuploadhistory docHistory = new documentuploadhistory();
            string filesList = "", filenames = "", uploadedUser = "";
            List<string> UploadedfilesList = new List<string>();
            string emailsList = "";
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (getCredentialFor == "doc")
                    {
                        if (db.documentuploadhistories.Any(m => m.id == docId))
                        {
                            docHistory = db.documentuploadhistories.FirstOrDefault(m => m.id == docId);
                            uploadedUser = docHistory.user;
                            filenames = docHistory.uploadFileName;
                            UploadedfilesList = docHistory.filePath.Split(';').ToList();

                            for (int k = 0; k < UploadedfilesList.Count(); k++)
                            {
                                if (k == 0)
                                {
                                    filesList = (UploadedfilesList[k].Substring(UploadedfilesList[k].IndexOf(docHistory.user))).Split('\\')[1];
                                }
                                else
                                {
                                    filesList = filesList + ";" + (UploadedfilesList[k].Substring(UploadedfilesList[k].IndexOf(docHistory.user))).Split('\\')[1];
                                }
                            }
                        }
                    }


                    if (db.emailcredentials.Any(m => m.common == true && m.inActive == false))
                    {
                        emailCred = db.emailcredentials.Where(m => m.common == true && m.inActive == false).ToList();
                        //emailCred.AddRange(db.emailcredentials.Where(m => m.moduleType==1).ToList());

                        for (int i = 0; i < emailCred.Count(); i++)
                        {
                            if (i == 0)
                            {
                                emailsList = emailCred[i].userEmail;
                            }
                            else
                            {
                                emailsList = emailsList + ";" + emailCred[i].userEmail;
                            }
                        }
                    }

                }

                return Json(new { success = true, files = filesList, uploadedUser = uploadedUser, emails = emailsList, fileNames = filenames, getCredentialFor = getCredentialFor });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }


        //Sending Email..................
        //Sending Email..................
        public ActionResult sendEmail(CallLoggingViewModel callLog)
        {
            Email email = callLog.email;
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long userId = Convert.ToInt32(Session["UserId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            emailinteraction emailinter = new emailinteraction();
            string exception = "";
            try
            {
                if (email.EmailFrom == "--Select--" || email.EmailFrom == "")
                {

                    return Json(new { success = false, error = "Please select email-Id" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    using (var db = new AutoSherDBContext())
                    {
                        if (string.IsNullOrEmpty(email.Password))
                        {
                            email.Password = db.emailcredentials.FirstOrDefault(m => m.userEmail == email.EmailFrom).userPassword;
                            //return Json(new { success = false, error = "Please enter password" },JsonRequestBehavior.AllowGet);
                        }

                        if (db.emailcredentials.Any(m => m.userEmail == email.EmailFrom))
                        {
                            string[] attachments = new string[10];
                            int semCount = 0;
                            if (email.Attachments != null)
                            {
                                semCount = email.Attachments.Split(';').Length;
                            }

                            if (email.EmailFor == "doc")
                            {
                                //string filePath = Server.MapPath("~") + "/UploadedFiles/ADVAITHHYUNDAI/" + Session["UserName"].ToString() + "/";

                                if (semCount != 2)
                                {
                                    attachments = email.Attachments.Split(';');
                                    attachments = attachments.Take(attachments.Count() - 1).ToArray();
                                }
                                else
                                {
                                    email.Attachments = email.Attachments.Remove((email.Attachments.Length - 1));
                                }
                            }



                            //semCount = 2;

                            using (MailMessage mailMessage = new MailMessage())
                            {
                                //string hid = Request.Form["File"];
                                mailMessage.To.Add(email.EmailTo);
                                mailMessage.From = new MailAddress(email.EmailFrom);
                                mailMessage.Body = email.Body;
                                mailMessage.Subject = email.Subject;
                                //mailMessage.Attachments.Add(m.File);

                                if (email.EmailFor == "doc")
                                {
                                    if (semCount == 2)
                                    {
                                        System.Net.Mail.Attachment attachment;
                                        attachment = new System.Net.Mail.Attachment(Server.MapPath("~/UploadedFiles/" + Session["DealerCode"].ToString() + "/" + email.UploadedUser + "/" + email.Attachments));
                                        //mail.Attachments.Add(attachment);

                                        mailMessage.Attachments.Add(attachment);
                                        //mailMessage.Attachments.Add(new Attachment())
                                    }
                                    else
                                    {
                                        foreach (var str in attachments)
                                        {
                                            if (str != null)
                                            {
                                                System.Net.Mail.Attachment attachment;
                                                attachment = new System.Net.Mail.Attachment(Server.MapPath("~/UploadedFiles/" + Session["DealerCode"].ToString() + "/" + email.UploadedUser + "/" + str));
                                                mailMessage.Attachments.Add(attachment);
                                            }
                                        }
                                    }
                                }


                                var emailCredentials = db.emailcredentials.FirstOrDefault(m => m.userEmail == email.EmailFrom);

                                using (SmtpClient smtp = new SmtpClient())
                                {
                                    smtp.Host = emailCredentials.hostapi;
                                    smtp.Port = int.Parse(emailCredentials.portnumber);
                                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    smtp.UseDefaultCredentials = true;
                                    smtp.Credentials = new System.Net.NetworkCredential(email.EmailFrom, email.Password);
                                    smtp.EnableSsl = true;
                                    smtp.Send(mailMessage);
                                }

                                emailinter.wyzUser_id = userId;
                                emailinter.customer_id = cusId;
                                emailinter.vehicle_id = vehiId;
                                emailinter.toEmailAddress = email.EmailTo;
                                emailinter.emailSubject = email.Subject;
                                emailinter.emailContent = email.Body;
                                emailinter.interactionDate = DateTime.Now.ToString("dd-MM-yyyy");
                                emailinter.interactionDateAndTime = DateTime.Now;
                                emailinter.interactionTime = DateTime.Now.ToString("HH:mm:ss");
                                emailinter.emailStatus = true;
                                emailinter.fromEmailAddress = email.EmailFrom;
                                if (!string.IsNullOrEmpty(email.EmailFor))
                                {
                                    emailinter.emailType = email.EmailFor;
                                }
                                else
                                {
                                    emailinter.emailType = "Custom";
                                }
                                emailinter.reason = "Email Sent";
                                db.emailinteractions.Add(emailinter);
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            return Json(new { success = false, error = "Please enter correct password" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                exception = ex.Message;
                using (var db = new AutoSherDBContext())
                {
                    emailinter.wyzUser_id = userId;
                    emailinter.customer_id = cusId;
                    emailinter.vehicle_id = vehiId;
                    emailinter.toEmailAddress = email.EmailTo;
                    emailinter.emailSubject = email.Subject;
                    emailinter.emailContent = email.Body;
                    emailinter.interactionDate = DateTime.Now.ToString("dd-MM-yyyy");
                    emailinter.interactionDateAndTime = DateTime.Now;
                    emailinter.interactionTime = DateTime.Now.ToString("HH:mm:ss");
                    emailinter.exceptionResponse = ex.Message;
                    emailinter.emailStatus = false;
                    emailinter.fromEmailAddress = email.EmailFrom;
                    if (!string.IsNullOrEmpty(email.EmailFor))
                    {
                        emailinter.emailType = email.EmailFor;
                    }
                    else
                    {
                        emailinter.emailType = "Custom";
                    }

                    emailinter.reason = "Email not sent";
                    db.emailinteractions.Add(emailinter);
                    db.SaveChanges();
                }

            }
            return Json(new { success = false, error = exception }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getSMSHistoryOfCustomer()
        {
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            List<SMSInteractionhistory> sms_int_data = new List<SMSInteractionhistory>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    List<smsinteraction> sms_int = db.smsinteractions.Include("wyzuser").Where(m => m.customer_id == cusId).OrderByDescending(m => m.smsId).ToList();

                    foreach (var sms_li in sms_int)
                    {
                        SMSInteractionhistory smseachData = new SMSInteractionhistory();
                        //sms_li.wyzuser = db.smsinteractions.FirstOrDefault(m => m.customer_id == cusId).wyzuser;
                        string user = "";
                        if (sms_li.wyzuser != null)
                        {
                            user = sms_li.wyzuser.userName;
                        }

                        smseachData.interactionDate = sms_li.interactionDate;
                        smseachData.interactionTime = sms_li.interactionTime;
                        smseachData.WyzuserName = user;
                        smseachData.reason = sms_li.interactionType;
                        smseachData.smsMessage = sms_li.smsMessage;
                        smseachData.mobileNumber = sms_li.mobileNumber;

                        if (!string.IsNullOrEmpty(sms_li.smsType))
                        {
                            if (int.Parse(sms_li.smsType) != 0)
                            {
                                if (int.Parse(sms_li.smsType) == 004 && Session["DealerCode"].ToString() == "INDUS")
                                {
                                    smseachData.smsType = "Driver App";
                                }
                                else
                                {
                                    long smsTypeId = int.Parse(sms_li.smsType);
                                    smstemplate smsTemp = db.smstemplates.SingleOrDefault(m => m.smsId == smsTypeId);
                                    smseachData.smsType = smsTemp.smsType;
                                }
                            }

                            else
                            {
                                if (sms_li.isAutoSMS == true)
                                {
                                    smseachData.smsType = "AUTO";
                                }
                                else
                                {
                                    smseachData.smsType = "-";
                                }
                            }
                        }
                        else
                        {
                            smseachData.smsType = "Doc Upload";
                        }



                        if (sms_li.smsStatus)
                        {
                            smseachData.smsStatus = true;
                        }
                        else
                        {

                            smseachData.smsStatus = false;
                        }
                        sms_int_data.Add(smseachData);
                    }
                }

                return Json(new { success = true, data = sms_int_data });
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

                return Json(new { success = false, exception });

            }
        }

        public ActionResult getEmailHistoryOfCustomerId()
        {
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            List<SMSInteractionhistory> email_int_data = new List<SMSInteractionhistory>();

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    List<emailinteraction> email_int = db.emailinteractions.Where(m => m.customer_id == cusId).OrderByDescending(m => m.id).ToList();

                    foreach (var email_li in email_int)
                    {

                        SMSInteractionhistory smseachData = new SMSInteractionhistory();

                        string user = "";
                        if (email_li.wyzUser_id != 0)
                        {
                            wyzuser wyz = db.wyzusers.SingleOrDefault(m => m.id == email_li.wyzUser_id);
                            user = wyz.userName;
                        }

                        smseachData.interactionDate = email_li.interactionDate;
                        smseachData.interactionTime = email_li.interactionTime;
                        smseachData.WyzuserName = user;
                        smseachData.toEmailAddress = email_li.toEmailAddress;
                        smseachData.fromEmailAddress = email_li.fromEmailAddress;

                        if (string.IsNullOrEmpty(email_li.cc))
                        {
                            smseachData.ccEmailAddress = "-";
                        }
                        else
                        {
                            smseachData.ccEmailAddress = email_li.cc;
                        }

                        if (email_li.emailType != null)
                        {
                            smseachData.smsType = email_li.emailType;
                        }
                        else
                        {
                            smseachData.smsType = "-";
                        }

                        smseachData.smsMessage = email_li.emailContent;
                        smseachData.emailsubject = email_li.emailSubject;
                        smseachData.reason = email_li.reason;

                        email_int_data.Add(smseachData);
                    }
                }

                return Json(new { success = true, data = email_int_data });
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

                return Json(new { success = false, exception });
            }
        }

        public ActionResult getComplaintHistoryVeh()
        {
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            List<ComplaintInformation> complaintHistory = new List<ComplaintInformation>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    List<complaint> complaintVeh = db.complaints.Where(m => m.vehicle_vehicle_id == vehiId).ToList();


                    foreach (var sa in complaintVeh)
                    {

                        ComplaintInformation compInf = new ComplaintInformation();
                        compInf.complaintNumber = sa.complaintNumber;
                        compInf.saleDate = sa.issueDate;
                        compInf.sourceName = sa.sourceName;
                        compInf.functionName = sa.functionName;
                        compInf.subcomplaintType = sa.subcomplaintType;
                        compInf.Description = sa.complaintStatus;

                        complaintHistory.Add(compInf);

                    }
                }
                return Json(new { data = complaintHistory, draw = Request["draw"], recordsTotal = complaintHistory.Count, recordsFiltered = complaintHistory.Count }, JsonRequestBehavior.AllowGet);
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
                return Json(new { data = "", draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0, exception = exception }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult getAssignmentHistoryOfVehicleId(string moduleType)
        {
            int UserId = Convert.ToInt32(Session["UserId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            List<long> takenList = new List<long>();
            List<SMSInteractionhistory> sms_int_data = new List<SMSInteractionhistory>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (moduleType == "service")
                    {


                        string campaign = "";
                        string reassign = "";
                        string reassignhistory = "";
                        string Cremangeruser = "";

                        string dealer = db.wyzusers.SingleOrDefault(m => m.id == UserId).dealerName;
                        List<assignedcallsreport> assign = db.assignedcallsreports.Where(m => m.vehicleId == vehiId && m.moduletypeId == 1).ToList();


                        foreach (var assign_li in assign)
                        {
                            SMSInteractionhistory smseachData = new SMSInteractionhistory();

                            if (!takenList.Contains(assign_li.assignInteractionID))
                            {
                                takenList.Add(assign_li.assignInteractionID);
                                wyzuser wyz = new wyzuser();
                                string user = "";
                                if (assign_li.wyzuserId != 0)
                                {
                                    long id = assign_li.wyzuserId;
                                    user = db.wyzusers.FirstOrDefault(m => m.id == id).userName;
                                }
                                smseachData.resonforDrop = db.Database.SqlQuery<string>("select  whendeleted  from deletedstatusreport  where moduletype_id=1 and  assigned_id=@id;", new MySqlParameter("@id", assign_li.assignInteractionID)).FirstOrDefault();
                                if (smseachData.resonforDrop == null)
                                {
                                    smseachData.resonforDrop = "-";
                                }

                                assignedinteraction assignAvail = new assignedinteraction();
                                assignAvail = db.assignedinteractions.SingleOrDefault(m => m.id == assign_li.assignInteractionID && m.vehical_Id == vehiId);

                                string avail = "Removed";
                                if (assignAvail != null)
                                {
                                    avail = "Active";
                                }

                                string managerUser = "Auto";
                                bool autoassigned = db.assignedcallsreports.FirstOrDefault(m => m.assignInteractionID == assign_li.assignInteractionID).isautoassigned;
                                if (autoassigned == false)
                                {
                                    long id = assign_li.wyzuserId;
                                    Cremangeruser = db.wyzusers.FirstOrDefault(m => m.id == id).creManager;
                                    managerUser = Cremangeruser;
                                }
                                //if (assignAvail != null && assignAvail.isautoassigned == false)
                                //{
                                //    long id = assign_li.wyzuserId;
                                //    Cremangeruser = db.wyzusers.FirstOrDefault(m => m.id == id).creManager;
                                //    managerUser = Cremangeruser;
                                //}

                                callhistorycube callhistory = new callhistorycube();


                                int countcalhistory = db.callhistorycubes.Count(m => m.assignedInteraction_id == assign_li.assignInteractionID && m.vehicle_id == vehiId);
                                if (countcalhistory > 0)
                                {
                                    callhistory = db.callhistorycubes.FirstOrDefault(m => m.assignedInteraction_id == assign_li.assignInteractionID && m.vehicle_id == vehiId);
                                }

                                string lastDispo = "";
                                string serviceType = "-";
                                if (callhistory.secondary_dispostion != null)
                                {
                                    lastDispo = callhistory.secondary_dispostion;
                                }
                                if (callhistory.nextServicetype != null)
                                {
                                    serviceType = callhistory.nextServicetype;
                                }
                                long? cid = assign_li.campaignId;
                                if (cid != 0 && cid != null)
                                {
                                    campaign camp = db.campaigns.Where(c => c.id == cid).FirstOrDefault();
                                    campaign = camp.campaignName;
                                }


                                int countassignmentRecorrd = db.change_assignment_records.Count(m => m.assignedinteraction_id == assign_li.assignInteractionID && m.moduletypeIs == 1);

                                if (countassignmentRecorrd > 0)
                                {
                                    List<change_assignment_records> chng = db.change_assignment_records.Where(m => m.assignedinteraction_id == assign_li.assignInteractionID && m.moduletypeIs == 1).ToList();
                                    reassign = "Yes";
                                    foreach (change_assignment_records c in chng)
                                    {
                                        wyzuser w = db.wyzusers.Where(m => m.id == c.new_wyzuserId).FirstOrDefault();

                                        string d = c.updatedDate.ToString("dd/MM/yyyy");
                                        reassignhistory += w.userName + " -> " + d + "<br>";
                                    }
                                }
                                else
                                {
                                    reassign = "No";
                                    reassignhistory = "";
                                }

                                string assignid = "ASSIGNID" + assign_li.assignInteractionID;
                                smseachData.campaign = campaign;
                                smseachData.reassign = reassign;
                                smseachData.reassignhistory = reassignhistory;

                                smseachData.assignDate = assign_li.assignedDate;
                                smseachData.WyzuserName = user;
                                smseachData.reason = managerUser; //db.wyzusers.FirstOrDefault(m => m.id == assign_li.assigned_manager_id).userName;
                                smseachData.assignedId = assignid;
                                smseachData.smsMessage = lastDispo;
                                smseachData.smsType = avail;
                                smseachData.serviceTypes = serviceType;

                                // smseachData.assignedBy = db.wyzusers.FirstOrDefault(m => m.id == assign_li.assigned_manager_id).userName;

                                sms_int_data.Add(smseachData);
                            }
                        }
                    }
                    else if (moduleType == "psf")
                    {


                        string campaign = "";
                        string reassign = "";
                        string reassignhistory = "";
                        string Cremangeruser = "";

                        string dealer = db.wyzusers.SingleOrDefault(m => m.id == UserId).dealerName;
                        List<assignedcallsreport> assign = db.assignedcallsreports.Where(m => m.vehicleId == vehiId && m.moduletypeId == 4).ToList();


                        foreach (var assign_li in assign)
                        {
                            SMSInteractionhistory smseachData = new SMSInteractionhistory();

                            if (!takenList.Contains(assign_li.assignInteractionID))
                            {
                                takenList.Add(assign_li.assignInteractionID);
                                wyzuser wyz = new wyzuser();
                                string user = "";
                                if (assign_li.wyzuserId != 0)
                                {
                                    long id = assign_li.wyzuserId;
                                    user = db.wyzusers.FirstOrDefault(m => m.id == id).userName;
                                }
                                smseachData.resonforDrop = db.Database.SqlQuery<string>("select  whendeleted  from deletedstatusreport  where moduletype_id=4 and  assigned_id=@id;", new MySqlParameter("@id", assign_li.assignInteractionID)).FirstOrDefault();
                                if (smseachData.resonforDrop == null)
                                {
                                    smseachData.resonforDrop = "-";
                                }

                                psfassignedinteraction assignAvail = new psfassignedinteraction();
                                assignAvail = db.psfassignedinteractions.SingleOrDefault(m => m.id == assign_li.assignInteractionID && m.vehicle_vehicle_id == vehiId);

                                string avail = "Removed";
                                if (assignAvail != null)
                                {
                                    avail = "Active";
                                }

                                string managerUser = "Auto";
                                //if (assignAvail != null && assignAvail.isautoassigned == false)
                                //{
                                //    long id = assign_li.wyzuserId;
                                //    Cremangeruser = db.wyzusers.FirstOrDefault(m => m.id == id).creManager;
                                //    managerUser = Cremangeruser;
                                //}
                                bool autoassigned = db.assignedcallsreports.FirstOrDefault(m => m.assignInteractionID == assign_li.assignInteractionID).isautoassigned;
                                if (autoassigned == false)
                                {
                                    long id = assign_li.wyzuserId;
                                    Cremangeruser = db.wyzusers.FirstOrDefault(m => m.id == id).creManager;
                                    managerUser = Cremangeruser;
                                }
                                psfcallhistorycube callhistory = new psfcallhistorycube();


                                int countcalhistory = db.psfcallhistorycubes.Count(m => m.psfAssignedInteraction_id == assign_li.assignInteractionID && m.vehicle_vehicle_id == vehiId);
                                if (countcalhistory > 0)
                                {
                                    callhistory = db.psfcallhistorycubes.FirstOrDefault(m => m.psfAssignedInteraction_id == assign_li.assignInteractionID && m.vehicle_vehicle_id == vehiId);
                                }

                                string lastDispo = "";
                                string serviceType = "-";
                                if (callhistory.SecondaryDisposition != null)
                                {
                                    lastDispo = callhistory.SecondaryDisposition;
                                }
                                if (callhistory.serviceType != null)
                                {
                                    serviceType = callhistory.serviceType;
                                }
                                long? cid = assign_li.campaignId;
                                if (cid != 0)
                                {
                                    campaign camp = db.campaigns.Where(c => c.id == cid).FirstOrDefault();
                                    campaign = camp.campaignName;
                                }


                                int countassignmentRecorrd = db.change_assignment_records.Count(m => m.assignedinteraction_id == assign_li.assignInteractionID && m.moduletypeIs == 4);

                                if (countassignmentRecorrd > 0)
                                {
                                    List<change_assignment_records> chng = db.change_assignment_records.Where(m => m.assignedinteraction_id == assign_li.assignInteractionID && m.moduletypeIs == 4).ToList();
                                    reassign = "Yes";
                                    foreach (change_assignment_records c in chng)
                                    {
                                        wyzuser w = db.wyzusers.Where(m => m.id == c.new_wyzuserId).FirstOrDefault();

                                        string d = c.updatedDate.ToString("dd/MM/yyyy");
                                        reassignhistory += w.userName + " -> " + d + "<br>";
                                    }
                                }
                                else
                                {
                                    reassign = "No";
                                    reassignhistory = "";
                                }

                                string assignid = "ASSIGNID" + assign_li.assignInteractionID;
                                smseachData.campaign = campaign;
                                smseachData.reassign = reassign;
                                smseachData.reassignhistory = reassignhistory;

                                smseachData.assignDate = assign_li.assignedDate;
                                smseachData.WyzuserName = user;
                                smseachData.reason = managerUser; //db.wyzusers.FirstOrDefault(m => m.id == assign_li.assigned_manager_id).userName;
                                smseachData.assignedId = assignid;
                                smseachData.smsMessage = lastDispo;
                                smseachData.smsType = avail;
                                smseachData.serviceTypes = serviceType;

                                // smseachData.assignedBy = db.wyzusers.FirstOrDefault(m => m.id == assign_li.assigned_manager_id).userName;

                                sms_int_data.Add(smseachData);
                            }
                        }
                    }
                    else
                    {
                        string campaign = "";
                        string reassign = "";
                        string reassignhistory = "";
                        string Cremangeruser = "";


                        string dealer = db.wyzusers.SingleOrDefault(m => m.id == UserId).dealerName;
                        List<assignedcallsreport> assign = db.assignedcallsreports.Where(m => m.vehicleId == vehiId && m.moduletypeId == 2).ToList();

                        foreach (var assign_li in assign)
                        {
                            SMSInteractionhistory smseachData = new SMSInteractionhistory();

                            if (!takenList.Contains(assign_li.assignInteractionID))
                            {
                                takenList.Add(assign_li.assignInteractionID);

                                wyzuser wyz = new wyzuser();
                                string user = "";
                                if (assign_li.wyzuserId != 0)
                                {
                                    long id = assign_li.wyzuserId;
                                    user = db.wyzusers.FirstOrDefault(m => m.id == id).userName;
                                }

                                smseachData.resonforDrop = db.Database.SqlQuery<string>("select  whendeleted  from deletedstatusreport  where moduletype_id=2 and assigned_id=@id;", new MySqlParameter("@id", assign_li.assignInteractionID)).FirstOrDefault();
                                smseachData.dateofdrop = db.Database.SqlQuery<DateTime>("select  deleted_date  from deletedstatusreport  where moduletype_id=2 and assigned_id=@id;", new MySqlParameter("@id", assign_li.assignInteractionID)).FirstOrDefault();
                                if (smseachData.resonforDrop == null)
                                {
                                    smseachData.resonforDrop = "-";
                                }
                                if (smseachData.dateofdrop == null)
                                {
                                    smseachData.dateofdrop = null;
                                }
                                insuranceassignedinteraction assignAvail = new insuranceassignedinteraction();
                                assignAvail = db.insuranceassignedinteractions.SingleOrDefault(m => m.id == assign_li.assignInteractionID && m.vehicle_vehicle_id == vehiId);

                                string avail = "Removed";
                                if (assignAvail != null)
                                {
                                    avail = "Active";
                                }

                                string managerUser = "Auto";
                                bool autoassigned = db.assignedcallsreports.FirstOrDefault(m => m.assignInteractionID == assign_li.assignInteractionID).isautoassigned;
                                if (autoassigned == false)
                                {
                                    long id = assign_li.wyzuserId;
                                    Cremangeruser = db.wyzusers.FirstOrDefault(m => m.id == id).creManager;
                                    managerUser = Cremangeruser;
                                }
                                //if (assignAvail != null && assignAvail.isAutoAssigned == false)
                                //{
                                //    long id = assign_li.wyzuserId;
                                //    Cremangeruser = db.wyzusers.FirstOrDefault(m => m.id == id).creManager;
                                //    managerUser = Cremangeruser;
                                //}

                                insurancecallhistorycube callhistory = new insurancecallhistorycube();


                                int countcalhistory = db.insurancecallhistorycubes.Count(m => m.insuranceAssignedInteraction_id == assign_li.assignInteractionID && m.vehicle_vehicle_id == vehiId);
                                if (countcalhistory > 0)
                                {
                                    callhistory = db.insurancecallhistorycubes.FirstOrDefault(m => m.insuranceAssignedInteraction_id == assign_li.assignInteractionID && m.vehicle_vehicle_id == vehiId);
                                }

                                string lastDispo = "";
                                if (callhistory.SecondaryDisposition != null)
                                {
                                    lastDispo = callhistory.SecondaryDisposition;
                                }
                                long? cid = assign_li.campaignId;
                                if (cid != 0)
                                {
                                    campaign camp = db.campaigns.Where(c => c.id == cid).FirstOrDefault();
                                    campaign = camp.campaignName;
                                }


                                int countassignmentRecorrd = db.change_assignment_records.Count(m => m.assignedinteraction_id == assign_li.assignInteractionID && m.moduletypeIs == 2);

                                if (countassignmentRecorrd > 0)
                                {
                                    List<change_assignment_records> chng = db.change_assignment_records.Where(m => m.assignedinteraction_id == assign_li.assignInteractionID && m.moduletypeIs == 2).ToList();
                                    reassign = "Yes";
                                    foreach (change_assignment_records c in chng)
                                    {
                                        wyzuser w = db.wyzusers.Where(m => m.id == c.new_wyzuserId).FirstOrDefault();

                                        string d = c.updatedDate.ToString("dd/MM/yyyy");
                                        reassignhistory += w.userName + " -> " + d + "<br>";
                                    }
                                }
                                else
                                {
                                    reassign = "No";
                                    reassignhistory = "";
                                }

                                string assignid = "ASSIGNID" + assign_li.assignInteractionID;
                                smseachData.campaign = campaign;
                                smseachData.reassign = reassign;
                                smseachData.reassignhistory = reassignhistory;

                                smseachData.assignDate = assign_li.assignedDate;
                                smseachData.WyzuserName = user;
                                smseachData.reason = managerUser;
                                smseachData.assignedId = assignid;
                                smseachData.smsMessage = lastDispo;
                                smseachData.smsType = avail;

                                //smseachData.assignedBy = db.wyzusers.FirstOrDefault(m => m.id == assign_li.assigned_manager_id).userName;

                                sms_int_data.Add(smseachData);
                            }
                        }
                    }
                }
                return Json(new { data = sms_int_data, draw = Request["draw"], recordsTotal = sms_int_data.Count, recordsFiltered = sms_int_data.Count }, JsonRequestBehavior.AllowGet);
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
                return Json(new { data = "", draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0, exception = exception }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult insuranceHistoryOfCustomerId()
        {
            string userName = Session["UserName"].ToString();
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            string msgURL = "";
            List<insurancequotation> insurHisData = new List<insurancequotation>();
            //payUTransaction payU = new payUTransaction();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    //insurancequotation ins = new insurancequotation();
                    insurHisData = db.insurancequotations.Where(u => u.customer_id == cusId).ToList();

                    foreach (var pay in insurHisData)
                    {
                        if (db.PayUTransactions.Count(m => m.fkquotationid == pay.id) > 0)
                        {
                            var paydetails = db.PayUTransactions.FirstOrDefault(m => m.fkquotationid == pay.id);
                            pay.status = paydetails.status;
                            pay.response_transaction_id = paydetails.response_transaction_id;

                        }
                        else
                        {
                            pay.status = "-";
                            pay.response_transaction_id = "-";
                        }

                    }
                    //ins.status = db.PayUTransactions.Where(m => m.fkquotationid == ins.id).FirstOrDefault().status;
                    //ins.response_transaction_id = db.PayUTransactions.Where(m => m.fkquotationid == ins.id).FirstOrDefault().response_transaction_id;


                    //foreach (var assign_li in insurHisData)
                    //{
                    //    payUTransaction payU = new payUTransaction();

                    //    assign_li.status = payU.status;
                    //    assign_li.response_transaction_id = payU.response_transaction_id;

                    //}


                    //customer customerData = db.customers.Include("addresses").Include("phones").Include("segments").Include("insurances").Include("vehicles").FirstOrDefault(m => m.customerId == cusId.ToString());
                    //payU = db.PayUTransactions.Where(m => m.customer_id == cusId).ToList();



                    //office address==---->address.-> addressType=="Office";

                    //List<phone> phnlist = customerData.phones.ToList();
                    //List<phone> phnlist1 = phnlist.Where(m => m.isPreferredPhone == true).ToList();
                    //  phone phnlist1 = db.phones.FirstOrDefault(m => m.customer_id == cusId && m.isPreferredPhone == true);

                    // phone phn1 = phnlist1.FirstOrDefault();
                    //  string phn = phnlist1.phoneNumber;
                    //if (phn.Length > 10)
                    //{
                    //    phn = phn.Substring(phn.Length - 10);
                    //}
                    //phn = "91" + phn;

                    //wyzuser userdata = db.wyzusers.FirstOrDefault(m => m.userName == userName);

                    //smstemplate smsTemp = db.smstemplates.FirstOrDefault(m => m.smsType == "INS QUOTATION"); //smsTrigger_repo.getSmsTemplateByType("INS QUOTATION");
                    //if (smsTemp.inActive ?? default(bool))
                    //{
                    //    foreach (var ins in insurHisData)
                    //    {
                    //        msgURL = "<label class='btn btn-default' title='Following Message is INACTIVE'><img src='/assets/images/no_whatsapp_logo.png' style='width: 20px;'></label>";
                    //        ins.msgURL = msgURL;
                    //    }
                    //}
                    //else
                    //{
                    //    if (isValid(phn))
                    //    {
                    //        foreach (var ins in insurHisData)
                    //        {
                    //            /*
                    //             * String smsname =
                    //             * insurRepo.getUpdatedSMSforInsQuotation(customerId,
                    //             * userdata.getId(), "INS QUOTATION", ins.getId());
                    //             * logger.info("ins quotation smsname: " + smsname); if
                    //             * (smsname!=null && !smsname.equals("")) { String href =
                    //             * ""; href = "https://api.whatsapp.com/send?phone=" + phn +
                    //             * "&text=" + smsname; msgURL =
                    //             * "<label onclick=showWhatsappPopup('" + smsname +
                    //             * "');><a href='" + href +
                    //             * "' class='btn btn-default' target='_blank' id='whatsapps'><img src='/assets/images/whatsapp_logo.png' style='width: 20px;'></a></label>"
                    //             * ; } else { msgURL =
                    //             * "<label class='btn btn-default' title='Following Message is INACTIVE'><img src='/assets/images/no_whatsapp_logo.png' style='width: 20px;'></label>"
                    //             * ; }
                    //             */
                    //            msgURL = "<label onclick=showWhatsappPopup('" + ins.id
                    //                    + "');><img src='/assets/images/whatsapp_logo.png' style='width: 20px;'></label>";

                    //            ins.msgURL = msgURL;

                    //        }
                    //    }
                    //    else
                    //    {
                    //        foreach (var ins in insurHisData)
                    //        {
                    //            msgURL = "<label class='btn btn-default' title='Invalid Phone Number'><img src='/assets/images/no_whatsapp_logo.png' style='width: 20px;'></label>";
                    //            ins.msgURL = msgURL;
                    //        }
                    //    }
                    //}
                    // email button
                    //foreach (var ins in insurHisData)
                    //{
                    //    //string emailURL = "<label onclick=sendEmail(" + userdata.role1 + "," + userdata.id + ","
                    //    //        + ins.id + "," + customerData.id + "," + customerData.vehicles.FirstOrDefault().vehicle_id
                    //    //        + ");><img src='/assets/images/email_logo.jpg' style='width:35px;'></label>";
                    //    //ins.emailURL = emailURL;
                    //}

                }
                return Json(new { data = insurHisData, draw = Request["draw"], recordsTotal = insurHisData.Count, recordsFiltered = insurHisData.Count }, JsonRequestBehavior.AllowGet);
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
                return Json(new { data = "", draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0, exception = exception }, JsonRequestBehavior.AllowGet);

            }
        }

        //support function for Insurance History..........
        public bool isValid(string s)
        {
            if (s.Length > 9)
            {
                string phoneNum = s.Substring(s.Length - 10, s.Length);
                char num = (phoneNum[0]);
                if (num == '6' || num == '7' || num == '8' || num == '9')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //function to fetch cityname based on state
        public ActionResult getCityNames(string state)
        {

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    var cities = db.citystates.Where(m => m.state == state).OrderBy(m => m.city).Select(m => m.city).ToList();
                    return Json(new { success = true, cities });
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { succee = false, cities = new citystate() });
        }

        public ActionResult getServiceDataOfCustomer()
        {
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    List<vehicle> vehicle_data = db.vehicles.Where(m => m.customer_id == cusId).ToList();

                    List<service> service_data = getAllServiceListOfVehicle(vehicle_data);

                    List<VehicleDataOnTabLoad> veh_data = new List<VehicleDataOnTabLoad>();
                    List<ServiceDataOnTabLoad> serv_data = new List<ServiceDataOnTabLoad>();

                    foreach (var addServiceData in service_data)
                    {

                        if (addServiceData.vehicle != null)
                        {

                            VehicleDataOnTabLoad vehicle = new VehicleDataOnTabLoad();

                            vehicle.vehicleRegNo = addServiceData.vehicle.vehicleRegNo;
                            vehicle.model = addServiceData.vehicle.model;
                            vehicle.variant = addServiceData.vehicle.variant;
                            vehicle.color = addServiceData.vehicle.color;
                            addServiceData.vehicle = db.vehicles.Include("customer").FirstOrDefault(m => m.vehicle_id == addServiceData.vehicle.vehicle_id);
                            addServiceData.vehicle.customer = db.customers.Include("segments").FirstOrDefault(m => m.customerId == addServiceData.vehicle.customer.customerId);

                            if (addServiceData.vehicle.customer.segments != null)
                            {
                                vehicle.category = addServiceData.vehicle.customer.customerCategory;
                            }

                            veh_data.Add(vehicle);

                        }
                        string workshop = "-";
                        if (addServiceData.workshop != null)
                        {
                            workshop = addServiceData.workshop.workshopName;
                        }

                        ServiceDataOnTabLoad service = new ServiceDataOnTabLoad();
                        service.jobCardDate = Convert.ToDateTime(addServiceData.jobCardDate).ToString("dd-MM-yyyy");
                        service.jobCardNumber = addServiceData.jobCardNumber;
                        service.serviceDate = addServiceData.lastServiceDate;
                        service.serviceType = addServiceData.lastServiceType;
                        service.lastServiceMeterReading = addServiceData.lastServiceMeterReading;
                        service.serviceAdvisor = addServiceData.saName;
                        service.serviceLocaton = addServiceData.serviceDealerName;
                        service.billDate = addServiceData.billDate == null ? "" : Convert.ToDateTime(addServiceData.billDate).ToString("dd-MM-yyyy");
                        service.custName = addServiceData.customerName;
                        service.kilometer = addServiceData.serviceOdometerReading;
                        service.partAmt = addServiceData.partsAmt ?? default(double);
                        service.labourAmt = addServiceData.labAmt ?? default(double);
                        service.mfgPartsTotal = addServiceData.mfgPartsTotal ?? default(double);
                        service.localPartsTotal = addServiceData.localPartsTotal ?? default(double);
                        service.maximileTotal = addServiceData.maximileTotal ?? default(double);
                        service.oilConsumablesTotal = addServiceData.oilConsumablesTotal ?? default(double);
                        service.maxiCareTotal = addServiceData.maxiCareTotal ?? default(double);
                        service.mfgAccessoriesTotal = addServiceData.mfgAccessoriesTotal ?? default(double);
                        service.localAccessoriesTotal = addServiceData.localAccessoriesTotal ?? default(double);
                        service.inhouseLabourTotal = addServiceData.inhouseLabourTotal ?? default(double);
                        service.outLabourTotal = addServiceData.outLabourTotal ?? default(double);
                        service.menuCodeDesc = addServiceData.menuCodeDesc;
                        service.billAmt = Convert.ToDouble(addServiceData.billAmt);

                        service.phonenumber = addServiceData.phonenumber;
                        service.workshop = workshop;
                        service.laborDetails = addServiceData.laborDetails;
                        service.defectDetails = addServiceData.defectDetails;
                        service.jobcardlocation = addServiceData.jobcardLocation;
                        service.technician = addServiceData.technician;
                        service.id = addServiceData.id;
                        service.vehicle_vehicle_id = addServiceData.vehicle_vehicle_id;
                        upload upload = getUpload(addServiceData.upload_id ?? default(long));
                        service.kminhours = addServiceData.kminhours;

                        if (upload != null)
                        {
                            service.uploadDateuploadDate = upload.uploadedDateTime ?? default(DateTime);
                        }

                        serv_data.Add(service);
                    }

                    CallLogsOnLoadTabData serviceLoadData = new CallLogsOnLoadTabData();
                    serviceLoadData.serviceLoadList = serv_data;
                    serviceLoadData.vehicleLoadList = veh_data;
                    // logger.info("serv_data : "+serv_data.size()+" veh_data :
                    // "+veh_data.size());
                    return Json(new { res = true, serviceLoadData = serviceLoadData });
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { res = false });
        }

        public List<service> getAllServiceListOfVehicle(List<vehicle> vehicle_data)
        {
            List<service> list_services = new List<service>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    foreach (var vehicle_iterate in vehicle_data)
                    {
                        long vehicleId = vehicle_iterate.vehicle_id;

                        List<service> servicesList = db.services.Include("vehicle").Include("workshop").Where(m => m.vehicle.vehicle_id == vehicleId).ToList(); // source.services(em).where(u->u.getVehicle().getVehicle_id() == vehicleId).toList();
                        list_services.AddRange(servicesList);

                    }

                    long countOfService = list_services.Where(m => m.jobCardDate != null).Count(); //list_services.stream().filter(u->u.getJobCardDate() != null).count();
                    long countOflistService = list_services.Count(); // .size();


                    if (countOfService == countOflistService)
                    {
                        if (Session["OEM"].ToString() == "MARUTI SUZUKI")
                        {
                            return list_services.OrderByDescending(m => m.billDate).ToList();
                        }
                        else
                        {
                            return list_services.OrderByDescending(m => m.jobCardDate).ToList();
                            //return list_services.OrderByDescending(m => m.id).ToList();
                        }

                    }
                    else if (countOfService < countOflistService)
                    {

                        List<service> list_services_new = new List<service>();

                        List<service> nulllistis = list_services.Where(m => m.jobCardDate == null).ToList();

                        List<service> newlist = list_services.Where(m => m.jobCardDate != null).ToList();

                        List<service> sortedlistIS;


                        if (Session["OEM"].ToString() == "MARUTI SUZUKI")
                        {
                            sortedlistIS = newlist.OrderByDescending(m => m.billDate).ToList();
                        }
                        else
                        {
                            sortedlistIS = newlist.OrderByDescending(m => m.jobCardDate).ToList();
                        }

                        list_services_new.AddRange(sortedlistIS);
                        list_services_new.AddRange(nulllistis);
                        return list_services_new;

                    }
                    else
                    {

                        return list_services;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return list_services;
        }

        public upload getUpload(long id)
        {
            upload up = new upload();

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    long countUp = Convert.ToInt32(db.uploads.Where(m => m.id == id).Count());//source.uploads(em).where(u->u.getId() == id).count();

                    if (countUp > 0)
                    {
                        up = db.uploads.Include("formdatas").FirstOrDefault(m => m.id == id);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return up;
        }

        public ActionResult getServiceByJobcardlocation(string jocardloc)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    service service = db.services.FirstOrDefault(m => m.jobcardLocation == jocardloc);

                    string defect = "";
                    string labour = "";
                    string jobCard = "";

                    if (service.defectDetails != null)
                    {
                        defect = service.defectDetails;
                    }

                    if (service.laborDetails != null)
                    {
                        labour = service.laborDetails;
                    }

                    if (service.jobCardRemarks != null)
                    {
                        jobCard = service.jobCardRemarks;
                    }

                    List<string> ajaxData = new List<string>();
                    ajaxData.Add(defect);
                    ajaxData.Add(labour);
                    ajaxData.Add(jobCard);

                    return Json(new { success = true, defectDetails = ajaxData });
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = false });
        }
        #endregion
        //*********** Functions for Disposition form--Insurance ************
        public ActionResult insuranceCompanies()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    List<insurancecompany> insCompanyList = new List<insurancecompany>();
                    insCompanyList = db.insurancecompanies.ToList();
                    return Json(new { success = true, insData = insCompanyList });
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = false });
        }


        public ActionResult filtersforQuotation()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {

                    List<irda_od_premium> irdaData = db.irda_od_premium.ToList();
                    List<string> uniqueage = irdaData.Select(m => m.vehicleAge).Distinct().ToList();
                    List<string> uniqueCC = irdaData.Select(m => m.cubicCapacity).Distinct().ToList();
                    List<string> uniqueaZone = irdaData.Select(m => m.zone).Distinct().ToList();
                    List<string> uniqueVehTyp = irdaData.Select(m => m.vehicleType).Distinct().ToList();



                    filteredQuoations filtQuote = new filteredQuoations();
                    filtQuote.ageList = uniqueage;
                    filtQuote.ccList = uniqueCC;
                    filtQuote.zoneList = uniqueaZone;
                    filtQuote.vehicleTypeList = uniqueVehTyp;

                    return Json(new { success = true, quoteData = filtQuote });
                }
            }
            catch (Exception ex)
            {

            }

            return Json(new { success = true });
        }

        public ActionResult vehicleTypeByZoneData(string selectedZone)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    List<string> vehTypZone = db.irda_od_premium.Where(m => m.zone == selectedZone).Select(m => m.vehicleType).Distinct().ToList();
                    return Json(new { success = true, vehTypeData = vehTypZone });
                }
            }
            catch (Exception ex)
            {

            }

            return Json(new { success = false });
        }

        public ActionResult ccByVehTypeData(string selectedVehType)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    List<string> ccData = db.irda_od_premium.Where(m => m.vehicleType == selectedVehType).Select(m => m.cubicCapacity).Distinct().ToList();
                    return Json(new { success = true, ccData = ccData });
                }
            }
            catch (Exception ex)
            {

            }

            return Json(new { success = false });
        }

        public ActionResult ageByCCTypeData(string selectedcc)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    List<String> ageData = db.irda_od_premium.Where(m => m.cubicCapacity == selectedcc).Select(m => m.vehicleAge).Distinct().ToList();
                    return Json(new { success = true, ageData = ageData });
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = false });
        }

        public ActionResult IRBasedOnFilter(string selectedZone, string selectedType, string selectedcc, string selectedAge)
        {

            List<irda_od_premium> irData = new List<irda_od_premium>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    irData = db.irda_od_premium.Where(m => m.zone == selectedZone && m.vehicleType == selectedType && m.cubicCapacity == selectedcc && m.vehicleAge == selectedAge).ToList();
                    if (irData.Count() > 0)
                    {
                        return Json(new { success = true, irData = irData[0] });

                    }
                    else
                    {
                        return Json(new { success = true, irData = irData });
                    }
                }
            }
            catch (Exception ex)
            {

            }


            return Json(new { success = false });
        }

        public ActionResult getBasicODVaue(double odvValue, double idvValue)
        {

            try
            {
                NumberFormatInfo nfi = new NumberFormatInfo();
                nfi.NumberDecimalSeparator = ".##";

                double OD_Value = (odvValue) / 100;

                double OD_Basic = (OD_Value) * (idvValue);


                string afterformate = OD_Basic.ToString(nfi);

                return Json(new { success = true, basicodValue = afterformate });
            }
            catch (Exception ex)
            {

            }

            return Json(new { success = false });
        }

        //#region save insurancequotaion/payUapi
        //public ActionResult saveInsuranceQuotation(string jsonData)
        //{
        //    insurancequotation insuQuote = new insurancequotation();
        //    if (jsonData != null)
        //    {
        //        insuQuote = JsonConvert.DeserializeObject<insurancequotation>(jsonData);
        //    }

        //    try
        //    {
        //        insuQuote.createdCRE = Session["UserName"].ToString();
        //        insuQuote.createdDate = DateTime.Now;
        //        using (var db = new AutoSherDBContext())
        //        {
        //            db.insurancequotations.Add(insuQuote);
        //            long customerID = insuQuote.customer_id;
        //            long vehicleID = insuQuote.vehicle_id;
        //            customer customerDetails = db.customers.Include("address").Include("phone").Where(m=>m.id==customerID).FirstOrDefault();
        //            payUTransaction payUInvoice = new payUTransaction();
        //            payUInvoice.customer_id = customerID;
        //            payUInvoice.vehicle_id = vehicleID;
        //            payUInvoice.amount = Convert.ToInt64(insuQuote.totalPremiumWithTax);
        //            payUInvoice.productinfo = "insuranceQuotation";
        //            payUInvoice.firstname = customerDetails.customerName;
        //            payUInvoice.email = customerDetails.emails.FirstOrDefault(m=>m.isPreferredEmail == true).emailAddress;
        //            payUInvoice.phone = customerDetails.phones.FirstOrDefault(m=>m.isPreferredPhone == true).phoneNumber;
        //            payUInvoice.address1 = customerDetails.addresses.FirstOrDefault(m=>m.isPreferred == true).concatenatedAdress;
        //            payUInvoice.city = customerDetails.addresses.FirstOrDefault(m=>m.isPreferred == true).city;
        //            payUInvoice.state = customerDetails.addresses.FirstOrDefault(m=>m.isPreferred == true).state;
        //            payUInvoice.country = customerDetails.addresses.FirstOrDefault(m=>m.isPreferred == true).country;
        //            payUInvoice.zipcode = customerDetails.addresses.FirstOrDefault(m=>m.isPreferred == true).pincode;
        //            payUInvoice.validation_period = 7;
        //            payUInvoice.send_email_now = 1;
        //            autosendPayUInvoiceCreatePolicy(payUInvoice);


        //            //{“amount“:”10000”,”txnid“:”abaac3332″,”productinfo“:”iPhone”,”firstname“:”Samir”,”em ail“:”test @test.com”,
        //            //    ”phone“:”9988776655”,”address1“:”testaddress”,”city“:”Mumbai”,
        //            //    ”stat e“:”Maharashtra”,”country“:”India”,”zipcode“:”122002″,”template_id“:”14″,”validation_period“: 6,”send_email_now“:”1”}
        //        }

        //        return Json(new { success = true });
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return Json(new { success = false });
        //}




        //#endregion


        //****** Functions in Common Disposition Page ***********

        //......./\/\\/\/\/\/\............Saving edit -----add Mobile number in Edit popUp
        //public ActionResult saveaddcustomermobno(string custMobNo, string remarks)
        //{
        //    int UserId = Convert.ToInt32(Session["UserId"]);
        //    int cusId = Convert.ToInt32(Session["CusId"].ToString());
        //    int value = 0;
        //    try
        //    {
        //        using (var db = new AutoSherDBContext())
        //        {
        //            if (db.phones.Where(m => m.customer != null && m.customer.customerId == cusId.ToString() && m.phoneNumber == custMobNo).Count() != 0)
        //            {
        //                return Json(new { data = value });
        //            }
        //            else
        //            {
        //                List<phone> ph = new List<phone>();
        //                ph = db.phones.Where(m => m.isPreferredPhone == true).ToList();

        //                foreach (var phone in ph)
        //                {
        //                    phone.isPreferredPhone = false;
        //                    db.SaveChanges();
        //                }


        //                customer custData = db.customers.FirstOrDefault(m => m.id == cusId);

        //                phone phoneData = new phone();
        //                phoneData.phoneNumber = custMobNo;
        //                phoneData.updatedBy = Session["UserId"].ToString(); //.setUpdatedBy(String.valueOf(wyzUser_id));
        //                phoneData.isPreferredPhone = true;//.setIsPreferredPhone(true);

        //                phoneData.customer = custData;//.setCustomer(custData);
        //                phoneData.remarks = remarks + "/" + DateTime.Now.ToShortDateString();
        //                db.phones.Add(phoneData);
        //                db.SaveChanges();

        //                return Json(new { data = phoneData.phone_Id });

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return Json(new { data = 0 });
        //}

        //UpsellLead Hide/show module on NextLead Button
        public ActionResult getUpsellLeadsSeletedInLastSB(long srId = 0)
        {
            List<upselllead> upsell = new List<upselllead>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    upsell = db.upsellleads.Where(m => m.srdisposition != null && m.srdisposition.id == srId).ToList();
                    var data = JsonConvert.SerializeObject(upsell);

                    return Json(new { success = true, upselData = upsell });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.ToString() });
            }
        }

        public ActionResult getLeadByUserLocation(string moduleType)
        {
            List<complainttype> duplicateCompa_type = new List<complainttype>();
            List<complainttype> compa_type = new List<complainttype>();
            List<string> dept_type = new List<string>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (moduleType == "insurance" || moduleType == "insuranceSearch")
                    {
                        long modId = ModuleType.INSURANCE;

                        duplicateCompa_type = db.complainttypes.Where(m => m.moduleTypeId == 3 || m.moduleTypeId == modId).ToList();
                        foreach (var types in duplicateCompa_type)
                        {
                            if (compa_type.Count(m => m.departmentName == types.departmentName) == 0)
                            {
                                compa_type.Add(types);
                            }

                        }
                    }
                    else
                    {
                        long modId = ModuleType.SERVICE;
                        compa_type = db.complainttypes.Where(m => m.moduleTypeId == 3 || m.moduleTypeId == modId).ToList();
                    }
                }
                return Json(new { success = true, leadlist = compa_type });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.ToString().Substring(0, 20) });
            }
        }

        public ActionResult getLeadTagByDepartment(long locId, string deptName, int moduleId)
        {
            List<string> taggedPerson = new List<string>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    taggedPerson = db.complainttypes.Where(m => m.departmentName == deptName && (m.moduleTypeId == 3 || m.moduleTypeId == moduleId)).Select(m => m.taggedUserName).ToList();
                }

                return Json(new { success = true, leadlist = taggedPerson });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.ToString().Substring(0, 20) });
            }
        }

        public ActionResult getTagNameByUpselLeadType(long locId, long upselId)
        {
            List<string> taggingUser = new List<string>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    taggingUser = db.taggingusers.Where(m => m.upsellLeadId == upselId).Select(m => m.name).ToList();
                }
                return Json(new { success = true, leadlist = taggingUser });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.ToString().Substring(0, 20) });
            }
        }

        public ActionResult getLeadNamesbyLeadId(long leadId, long userId, string moduletype)
        {
            //long userId = Convert.ToInt32(Session["UserId"].ToString());
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    wyzuser useris = db.wyzusers.SingleOrDefault(m => m.id == userId);

                    List<tagginguser> tagginguser = new List<tagginguser>();
                    List<tagginguser> tagginguser1 = new List<tagginguser>();
                    List<tagginguser> tagginguser3 = new List<tagginguser>();
                    List<tagginguser> tagginguserdata = new List<tagginguser>();

                    List<tagginguser> tagginguserlist = leadListByuserIdModule(leadId, userId, moduletype);

                    //logger.info("tagginguserlist" + tagginguserlist.size());

                    foreach (var ta in tagginguserlist)
                    {
                        //if (ta.location != null)
                        //{

                        //    if (ta.location.name == useris.location.name)
                        //    {
                        tagginguser.Add(ta);
                        //    }
                        //    else
                        //    {
                        //        tagginguser3.Add(ta);

                        //    }

                        //}
                        //else
                        //{
                        //    tagginguser1.Add(ta);
                        //}
                    }

                    if (tagginguser.Count() == 0)
                    {
                        //tagginguserdata.AddRange(tagginguser1);
                        tagginguserdata.AddRange(tagginguser);

                    }
                    else
                    {
                        tagginguserdata.AddRange(tagginguser);
                        //tagginguserdata.AddRange(tagginguser1);
                    }
                    if (tagginguserdata.Count() == 0)
                    {
                        List<tagginguser> dummyTag = db.taggingusers.Where(m => m.upsellLeadId == 0).ToList();
                        tagginguserdata.AddRange(dummyTag);
                    }

                    foreach (var ta in tagginguserdata)
                    {

                        ta.upsellleads = null;
                        ta.wyzuser = null;
                        ta.location = null;

                    }
                    var data = tagginguserdata.Select(m => new { id = m.id, leadName = m.name });
                    return Json(new { success = true, leadName = data });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.ToString().Substring(0, 20) });
            }

        }

        //function support for getLeadNamesbyLeadId****
        public List<tagginguser> leadListByuserIdModule(long leadId, long userid, string moduleType, int locationId = 0)
        {
            List<tagginguser> taggList = new List<tagginguser>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    int lead_id = (int)leadId;

                    List<location> userLocation = db.locations.Where(m => m.cityId == db.wyzusers.FirstOrDefault(k => k.id == userid).location_cityId).ToList();

                    //logger.info("userLocation : " + userLocation.size() + "moduleType " + moduleType);
                    if (moduleType == "insurance" || moduleType == "insuranceSearch")
                    {
                        long modId = ModuleType.INSURANCE;
                        List<tagginguser> tagListByLeadId = db.taggingusers.Include("location").Where(m => (m.moduleTypeId == 5 || m.moduleTypeId == modId || m.moduleTypeId == ModuleType.SERVICE_INSURANCE) && m.upsellLeadId == lead_id).ToList();
                        //logger.info("tagListByLeadId service: " + tagListByLeadId.size());
                        foreach (var tg in tagListByLeadId)
                        {
                            taggList.Add(tg);
                        }
                        //List<tagginguser> tagListByLeadId = new List<tagginguser>();

                        //if (locationId != 0)
                        //{
                        //    tagListByLeadId = db.taggingusers.Where(m => (m.moduleTypeId == modId || m.moduleTypeId == 5 || m.moduleTypeId == ModuleType.SERVICE_INSURANCE) && m.location_cityId == locationId && m.upsellLeadId == lead_id).ToList();
                        //}
                        //else
                        //{
                        //    long? userLoc = db.wyzusers.FirstOrDefault(k => k.id == userid).location_cityId;
                        //    tagListByLeadId = db.taggingusers.Where(m => (m.moduleTypeId == modId || m.moduleTypeId == 5 || m.moduleTypeId == ModuleType.SERVICE_INSURANCE) && m.upsellLeadId == lead_id).ToList();
                        //}


                        //foreach (var tg in tagListByLeadId)
                        //{

                        //    if (tg.location != null)
                        //    {

                        //        foreach (var la in userLocation)
                        //        {

                        //            if (tg.location.cityId == la.cityId)
                        //            {
                        //                taggList.Add(tg);
                        //            }

                        //        }

                        //    }
                        //    else
                        //    {
                        //        taggList.Add(tg);
                        //    }
                        //}

                    }
                    else if (moduleType == "service" || moduleType == "Service")
                    {
                        long modId = ModuleType.SERVICE;
                        List<tagginguser> tagListByLeadId = db.taggingusers.Include("location").Where(m => (m.moduleTypeId == 5 || m.moduleTypeId == modId || m.moduleTypeId == ModuleType.SERVICE_INSURANCE) && m.upsellLeadId == lead_id).ToList();
                        //logger.info("tagListByLeadId service: " + tagListByLeadId.size());
                        foreach (var tg in tagListByLeadId)
                        {
                            taggList.Add(tg);
                        }
                    }
                    else if (moduleType == "PSF")
                    {
                        long modId = ModuleType.PSF;
                        List<tagginguser> tagListByLeadId = db.taggingusers.Include("location").Where(m => (m.moduleTypeId == 3 || m.moduleTypeId == modId) && m.upsellLeadId == lead_id).ToList();
                        //logger.info("tagListByLeadId service: " + tagListByLeadId.size());
                        foreach (var tg in tagListByLeadId)
                        {

                            taggList.Add(tg);

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return taggList;

        }


        public ActionResult getLeadNamesbyLeadLocationData(long leadId, long userId, string moduletype, string locaisName)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    List<tagginguser> tagginguser2 = new List<tagginguser>();
                    List<tagginguser> tagginguser1 = new List<tagginguser>();
                    List<tagginguser> tagginguserdata = new List<tagginguser>();

                    int locId = 0;
                    if (locaisName != null && locaisName != "")
                    {
                        locId = Convert.ToInt32(db.locations.FirstOrDefault(m => m.name == locaisName).cityId);
                    }

                    List<tagginguser> tagginguserlist = leadListByuserIdModule(leadId, userId, moduletype, locId);

                    //logger.info("tagginguserlist" + tagginguserlist.size());

                    foreach (var ta in tagginguserlist)
                    {
                        if (ta.location != null && ta.location.name == locaisName)
                        {

                            tagginguser1.Add(ta);

                        }
                    }

                    tagginguserdata.AddRange(tagginguser1);
                    if (tagginguser1.Count() == 0)
                    {
                        foreach (var ta in tagginguserlist)
                        {
                            if (ta.location == null)
                            {

                                tagginguser2.Add(ta);

                            }
                        }
                        tagginguserdata.AddRange(tagginguser2);
                    }
                    if (tagginguserdata.Count() == 0)
                    {
                        List<tagginguser> dummyTag = db.taggingusers.Where(m => m.upsellLeadId == 0).ToList();
                        tagginguserdata.AddRange(dummyTag);
                    }
                    //logger.info("tagginguser size" + tagginguserdata.size());
                    foreach (var ta in tagginguserdata)
                    {

                        ta.upsellleads = null;
                        ta.wyzuser = null;
                        ta.location = null;

                    }
                    //var data = JsonConvert.SerializeObject(tagginguserdata);
                    var taggedData = tagginguserdata.Select(m => new { id = m.id, name = m.name });
                    return Json(new { success = true, leadName = taggedData });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.ToString().Substring(0, 20) });
            }
        }

        public ActionResult taggedUserNameByDeptIns(string deptName)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string taggUserName = "";
                    if (db.complainttypes.Where(m => m.departmentName == deptName).Count() > 0)
                    {
                        taggUserName = db.complainttypes.FirstOrDefault(m => m.departmentName == deptName).taggedUserName;
                    }

                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.ToString().Substring(0, 20) });
            }


        }

        //service not required
        public ActionResult getWorkshopListName(long workshopId)
        {
            string locName = string.Empty;
            wyzuser userdata = new wyzuser();
            int userId = Convert.ToInt32(Session["UserId"].ToString());
            List<string> workshopList1 = new List<string>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    //userdata = db.wyzusers.SingleOrDefault(m => m.id == userId);
                    //List<> workshoplistdata = new List<workshop>();

                    //if (workshopId != 0)
                    //{
                    //    workshopList1 = db.workshops.Where(m => m.id == workshopId).Select(m => m.workshopName).ToList();
                    //}
                    //else
                    //{
                    //    workshopList1 = db.workshops.Select(m => m.workshopName).ToList();
                    //}
                    if (workshopId != 0)
                    {
                        locName = db.workshops.FirstOrDefault(m => m.id == workshopId).workshopName;
                    }
                    workshopList1 = db.workshops.Where(m => m.isinsurance == false).Select(m => m.workshopName).ToList();
                }
                return Json(new { success = true, workshoplist = workshopList1, selectedName = locName });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.ToString().Substring(0, 20) });
            }

        }

        //Renewal not required
        public ActionResult getlatestlocationName(long workshopId)
        {
            string locName = string.Empty;
            wyzuser userdata = new wyzuser();
            int userId = Convert.ToInt32(Session["UserId"].ToString());
            List<string> workshopList1 = new List<string>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (workshopId != 0)
                    {
                        locName = db.locations.FirstOrDefault(m => m.cityId == workshopId).name;
                    }
                    workshopList1 = db.locations.Select(m => m.name).ToList();

                }
                return Json(new { success = true, workshoplist = workshopList1, selectedName = locName });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.ToString().Substring(0, 20) });
            }

        }


        public ActionResult getListWorkshopByLocation(string selectedCity)
        {

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (db.locations.Where(m => m.name == selectedCity).Count() > 0)
                    {
                        var workshopList = db.locations.Include("workshops").FirstOrDefault(m => m.name == selectedCity).workshops.Select(m => new { id = m.id, workshopName = m.workshopName });
                        return Json(new { success = true, workshoplist = workshopList });
                    }
                    else
                    {
                        List<workshop> emptyWorkshop = new List<workshop>();
                        return Json(new { success = true, workshoplist = emptyWorkshop });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.ToString().Substring(0, 20) });
            }

        }

        public ActionResult getServiceAdvisorOfWorkshop(long workshopId)
        {
            List<serviceadvisor> advisor = new List<serviceadvisor>();
            List<serviceadvisor> activeList = new List<serviceadvisor>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    advisor = db.serviceadvisors.Where(m => m.workshop != null && m.workshop_id == workshopId).ToList();
                    if (advisor.Count() != 0)
                    {
                        foreach (var ad in advisor)
                        {
                            if (ad.isActive == true)
                            {
                                activeList.Add(ad);
                            }
                        }
                    }
                    else
                    {
                        activeList = db.serviceadvisors.Where(m => m.isActive == true).ToList();
                    }
                }

                var data = activeList.Select(m => new { advisorName = m.advisorName, advisorId = m.advisorId }).ToList();

                return Json(new { success = true, advisorlist = data });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.ToString().Substring(0, 20) });
            }

        }

        //My autoSa
        public ActionResult autoSA(long workshopId, string date)
        {
            List<AutoSA> sa = new List<AutoSA>();
            string selectedDate = string.Empty;
            if (!string.IsNullOrEmpty(date))
            {
                selectedDate = Convert.ToDateTime(date.ToString()).ToString("yyyy-MM-dd");
            }
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    string str = @"CALL prioritised_single_salist(@date,@workshopId)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                       new MySqlParameter("@date", selectedDate),
                       new MySqlParameter("@workshopId", workshopId)
                    };

                    sa = db.Database.SqlQuery<AutoSA>(str, param).ToList();
                    sa = sa.OrderBy(m => m.priority).ToList();

                    //var data = sa.Select(m => new { advisorId = m.advisorId, priority = m.priority, advisorNumber = m.advisorName });
                    return Json(new { success = true, data = sa, work = workshopId, date = date, selectedDate = selectedDate });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message, work = workshopId, date = date, selectedDate = selectedDate });
            }
            return Json(new { success = false });
        }

        public ActionResult listWorkshops(string selectedCity)
        {
            //List<workshop> workshoplist = new List<workshop>();
            AutoSherDBContext dba = new AutoSherDBContext();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    //db.Configuration.LazyLoadingEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;
                    long cId = 0;

                    cId = dba.locations.FirstOrDefault(m => m.name == selectedCity).cityId;
                    var workshoplist = dba.workshops.Where(m => m.location_cityId == cId).Select(m => new { id = m.id, workshopName = m.workshopName }).ToList();
                    //var data = JsonConvert.SerializeObject(workshoplist);

                    return Json(workshoplist);
                }

            }
            catch (Exception ex)
            {

            }
            return Json(new { workshoplist = new List<workshop>() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getCallHistory(string typeIs)
        {

            //Logger logger = LogManager.GetLogger("apkRegLogger");
            string gsmAndroid = "";
            long vehicalId = Convert.ToInt32(Session["VehiId"].ToString());

            //Parameters of Paging..........
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            //string searchPattern = Request["search[value]"];

            int maxCount = 0;

            List<interactiondata> interDataList = new List<interactiondata>();

            string exception = "";
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (typeIs == "PSF")
                    {
                        maxCount = db.psfcallhistorycubes.Where(m => m.vehicle_vehicle_id == vehicalId).Count();
                        List<psfcallhistorycube> psfCallHistoryByVehicle = db.psfcallhistorycubes.Where(m => m.vehicle_vehicle_id == vehicalId).OrderByDescending(m => m.callDate).Skip(start).Take(length).ToList();

                        if (psfCallHistoryByVehicle != null && psfCallHistoryByVehicle.Count > 0)
                        {
                            psfCallHistoryByVehicle = psfCallHistoryByVehicle.OrderByDescending(m => m.id).ToList();
                        }

                        foreach (var call_data in psfCallHistoryByVehicle)
                        {

                            interactiondata interData = new interactiondata();

                            interData.CallDate = Convert.ToDateTime(call_data.callDate).ToString("dd-MM-yyyy");
                            interData.Campaign = call_data.psfCallingDayType;
                            interData.CreId = call_data.creName;
                            interData.AssignId = "AASIGNID" + call_data.psfAssignedInteraction_id.ToString();
                            interData.Time = call_data.callTime.ToString();
                            interData.jobCardNumber = call_data.jobCardNumber;
                            if (string.IsNullOrEmpty(call_data.callType))
                            {
                                interData.CallType = call_data.callType;
                            }
                            else
                            {
                                interData.CallType = "-";
                            }
                            //if (call_data.gsm_android != null)
                            //{
                            //    gsmAndroid = "(" + call_data.gsm_android + ")";
                            //}
                            interData.IsCallInitiated = call_data.isCallinitaited;// + gsmAndroid;
                            interData.gsmAndroid = call_data.gsm_android;

                            if (!string.IsNullOrEmpty(call_data.makeCallFrom))
                            {
                                interData.CallMadeType = call_data.makeCallFrom;
                            }
                            else
                            {
                                interData.CallMadeType = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.dailedNumber))
                            {
                                interData.DailedNo = call_data.dailedNumber;
                            }
                            else
                            {
                                interData.DailedNo = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.SecondaryDisposition))
                            {
                                interData.SecondaryDispo = call_data.SecondaryDisposition;
                            }
                            else
                            {
                                interData.SecondaryDispo = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.SecondaryDisposition))
                            {
                                if (call_data.SecondaryDisposition == "Call Me Later")
                                {
                                    interData.Details = Convert.ToDateTime(call_data.psfFollowUpDate).ToString("dd-MM-yyyy") + "/" + call_data.psfFollowUpTime;
                                }
                                else if (!string.IsNullOrEmpty(call_data.Remarks))
                                {
                                    interData.Details = call_data.Remarks;
                                }
                                else
                                {
                                    interData.Details = "-";
                                }
                            }
                            else
                            {
                                interData.Details = "-";
                            }


                            if (!string.IsNullOrEmpty(call_data.Remarks))
                            {
                                interData.CreRemarks = call_data.Remarks;
                            }
                            else
                            {
                                interData.CreRemarks = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.Comments))
                            {
                                interData.Feedback = call_data.Comments;
                            }
                            else
                            {
                                interData.Feedback = "-";
                            }



                            //filepath
                            if (!string.IsNullOrEmpty(call_data.filepath))
                            {
                                interData.FilePath = call_data.filepath;
                            }
                            else
                            {
                                //var callsynchData = db.callSyncDatas.FirstOrDefault(m => m.callinteraction_id == call_data.cicallinteraction_id);
                                //if (callsynchData != null)
                                //{
                                //    interData.FilePath = callsynchData.filepath;
                                //}
                                //else
                                //{
                                var callinteraction = db.callinteractions.FirstOrDefault(m => m.id == call_data.cicallinteraction_id);
                                if (callinteraction != null)
                                {
                                    interData.FilePath = callinteraction.filePath;
                                }
                                else
                                {
                                    interData.FilePath = "-";
                                }
                                //}
                            }
                            interDataList.Add(interData);
                        }
                    }
                    else if (typeIs == "INS")
                    {
                        maxCount = db.insurancecallhistorycubes.Where(m => m.vehicle_vehicle_id == vehicalId).Count();
                        //maxCount = db.Insurancecallhistorycube_Stages.Where(m => m.vehicle_vehicle_id == vehicalId).Count();
                        List<insurancecallhistorycube> insuranceCallHistoryByVehicle = db.insurancecallhistorycubes.Where(m => m.vehicle_vehicle_id == vehicalId).OrderByDescending(X => X.callDate).Skip(start).Take(length).ToList();
                        //List<insurancecallhistorycube_stage1> insuranceCallHistoryByVehicle = db.Insurancecallhistorycube_Stages.Where(m => m.vehicle_vehicle_id == vehicalId).OrderByDescending(X => X.callDate).Skip(start).Take(length).ToList();

                        if (insuranceCallHistoryByVehicle != null && insuranceCallHistoryByVehicle.Count() > 0)
                        {
                            insuranceCallHistoryByVehicle = insuranceCallHistoryByVehicle.OrderByDescending(m => m.id).ToList();
                        }

                        foreach (var call_data in insuranceCallHistoryByVehicle)
                        {
                            interactiondata interData = new interactiondata();


                            if (call_data.callDate != null)
                            {
                                interData.CallDate = Convert.ToDateTime(call_data.callDate).ToString("dd-MM-yyyy");
                            }
                            else
                            {
                                interData.CallDate = "-";
                            }

                            interData.Campaign = call_data.campaignTYpe;

                            if (Session["DealerCode"].ToString() == "INDUS")
                            {
                                var wyzUser = db.wyzusers.FirstOrDefault(m => m.id == call_data.wyzUser_id);

                                if (wyzUser != null)
                                {
                                    interData.CreId = wyzUser.firstName + "(" + wyzUser.userName + ")";
                                }
                                else
                                {
                                    interData.CreId = call_data.creName;
                                }
                            }
                            else
                            {
                                interData.CreId = call_data.creName;
                            }

                            interData.AssignId = "AASIGNID" + call_data.insuranceAssignedInteraction_id.ToString();
                            interData.Time = call_data.callTime.ToString();

                            if (string.IsNullOrEmpty(call_data.callType))
                            {
                                interData.CallType = call_data.callType;
                            }
                            else
                            {
                                interData.CallType = "-";
                            }
                            //if (call_data.gsm_android != null)
                            //{
                            //    gsmAndroid = "(" + call_data.gsm_android + ")";
                            //}
                            interData.IsCallInitiated = call_data.isCallinitaited;// + gsmAndroid;
                            interData.gsmAndroid = call_data.gsm_android;

                            if (!string.IsNullOrEmpty(call_data.makeCallFrom))
                            {
                                interData.CallMadeType = call_data.makeCallFrom;
                            }
                            else
                            {
                                interData.CallMadeType = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.dailedNumber))
                            {
                                interData.DailedNo = call_data.dailedNumber;
                            }
                            else
                            {
                                interData.DailedNo = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.SecondaryDisposition))
                            {
                                interData.SecondaryDispo = call_data.SecondaryDisposition;
                            }
                            else
                            {
                                interData.SecondaryDispo = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.SecondaryDisposition))
                            {
                                if (call_data.SecondaryDisposition == "Call Me Later")
                                {
                                    interData.Details = Convert.ToDateTime(call_data.followUpDate).ToString("dd-MM-yyyy") + " " + call_data.followUpTime;
                                }
                                else if (call_data.SecondaryDisposition == "Book Appointment")
                                {
                                    if (!string.IsNullOrEmpty(call_data.Reshedule_Status) && call_data.Reshedule_Status == "Rescheduled")
                                    {
                                        string InsAgentName = "";
                                        if (!string.IsNullOrEmpty(call_data.insuranceAgent_insuranceAgentId))
                                        {
                                            long agentId = long.Parse(call_data.insuranceAgent_insuranceAgentId);

                                            insuranceagent agent = db.insuranceagents.FirstOrDefault(m => m.insuranceAgentId == agentId);
                                            if (agent != null)
                                            {
                                                InsAgentName = "|" + agent.insuranceAgentName;
                                            }
                                        }
                                        else if (!string.IsNullOrEmpty(call_data.insuranceAgentData))
                                        {
                                            long agentId = long.Parse(call_data.insuranceAgentData);

                                            insuranceagent agent = db.insuranceagents.FirstOrDefault(m => m.insuranceAgentId == agentId);
                                            if (agent != null)
                                            {
                                                InsAgentName = "|" + agent.insuranceAgentName;
                                            }
                                        }
                                        if (Session["DealerCode"].ToString() == "INDUS")
                                        {
                                            interData.Details = call_data.typeOfPickup + "|" + call_data.Tertiary_disposition + "|" + call_data.Reshedule_Status + InsAgentName + "|" + call_data.insuranceCompany + "|" + call_data.premiumwithdiscount.ToString();
                                        }
                                        else

                                            interData.Details = call_data.typeOfPickup + "|" + call_data.Tertiary_disposition + "|" + call_data.Reshedule_Status + InsAgentName;
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(call_data.insuranceAgent_insuranceAgentId))
                                        {
                                            long agentId = long.Parse(call_data.insuranceAgent_insuranceAgentId);

                                            insuranceagent agent = db.insuranceagents.FirstOrDefault(m => m.insuranceAgentId == agentId);
                                            if (agent != null)
                                            {
                                                interData.Details = agent.insuranceAgentName + "|" + Convert.ToDateTime(call_data.appointmentDate).ToString("dd-MM-yyyy") + "  " + call_data.appointmentFromTime + "|" + call_data.typeOfPickup + "|" + call_data.insuranceCompany + "|" + call_data.premiumwithdiscount.ToString();
                                            }
                                            else
                                            {
                                                interData.Details = Convert.ToDateTime(call_data.appointmentDate).ToString("dd-MM-yyyy") + "  " + call_data.appointmentFromTime + "|" + call_data.typeOfPickup + "|" + call_data.insuranceCompany + "|" + call_data.premiumwithdiscount.ToString();
                                            }
                                        }
                                        else if (!string.IsNullOrEmpty(call_data.insuranceAgentData))
                                        {
                                            long agentId = long.Parse(call_data.insuranceAgentData);

                                            insuranceagent agent = db.insuranceagents.FirstOrDefault(m => m.insuranceAgentId == agentId);
                                            if (agent != null)
                                            {
                                                interData.Details = agent.insuranceAgentName + "|" + Convert.ToDateTime(call_data.appointmentDate).ToString("dd-MM-yyyy") + "  " + call_data.appointmentFromTime + "|" + call_data.typeOfPickup + "|" + call_data.insuranceCompany + "|" + call_data.premiumwithdiscount.ToString();

                                            }
                                            else
                                            {
                                                interData.Details = Convert.ToDateTime(call_data.appointmentDate).ToString("dd-MM-yyyy") + "  " + call_data.appointmentFromTime + "|" + call_data.typeOfPickup + "|" + call_data.insuranceCompany + "|" + call_data.premiumwithdiscount.ToString();
                                            }
                                        }
                                        else
                                        {
                                            interData.Details = Convert.ToDateTime(call_data.appointmentDate).ToString("dd-MM-yyyy") + "  " + call_data.appointmentFromTime + "|" + call_data.typeOfPickup + "|" + call_data.insuranceCompany + "|" + call_data.premiumwithdiscount.ToString();
                                        }

                                    }
                                }
                                else
                                {
                                    interData.Details = Convert.ToDateTime(call_data.callDate).ToString("dd-MM-yyyy") + "  " + call_data.callTime.ToString().Substring(0, call_data.callTime.ToString().IndexOf(":") + 3);
                                }
                            }
                            else
                            {
                                interData.Details = "-";
                            }


                            if (!string.IsNullOrEmpty(call_data.comments))
                            {
                                interData.CreRemarks = call_data.comments;
                            }
                            else
                            {
                                interData.CreRemarks = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.customerComments))
                            {
                                interData.Feedback = call_data.customerComments;
                            }
                            else
                            {
                                interData.Feedback = "-";
                            }



                            //filepath
                            if (!string.IsNullOrEmpty(call_data.filepath))
                            {
                                interData.FilePath = call_data.filepath;
                            }
                            else
                            {
                                //var callsynchData = db.callSyncDatas.FirstOrDefault(m => m.callinteraction_id == call_data.cicallinteraction_id);
                                //if (callsynchData != null)
                                //{
                                //    interData.FilePath = callsynchData.filepath;
                                //}
                                //else
                                //{
                                var callinteraction = db.callinteractions.FirstOrDefault(m => m.id == call_data.cicallinteraction_id);
                                if (callinteraction != null)
                                {
                                    interData.FilePath = callinteraction.filePath;
                                }
                                else
                                {
                                    interData.FilePath = "-";
                                }
                                //}
                            }


                            interDataList.Add(interData);
                        }
                    }
                    else if (typeIs == "PostSalesFeedback")
                    {
                        maxCount = db.Postsalescallhistories.Where(m => m.vehicle_vehicle_id == vehicalId).Count();
                        List<postsalescallhistory> postsalesfeedbackCallHistoryByVehicle = db.Postsalescallhistories.Where(m => m.vehicle_vehicle_id == vehicalId).OrderByDescending(m => m.callDate).Skip(start).Take(length).ToList();

                        if (postsalesfeedbackCallHistoryByVehicle != null && postsalesfeedbackCallHistoryByVehicle.Count > 0)
                        {
                            postsalesfeedbackCallHistoryByVehicle = postsalesfeedbackCallHistoryByVehicle.OrderByDescending(m => m.id).ToList();
                        }

                        foreach (var call_data in postsalesfeedbackCallHistoryByVehicle)
                        {

                            interactiondata interData = new interactiondata();

                            interData.CallDate = Convert.ToDateTime(call_data.callDate).ToString("dd-MM-yyyy");
                            interData.Campaign = call_data.psfCallingDayType;
                            interData.CreId = call_data.creName;
                            interData.AssignId = "AASIGNID" + call_data.postAssignedInteraction_id.ToString();
                            interData.Time = call_data.callTime.ToString();

                            if (string.IsNullOrEmpty(call_data.callType))
                            {
                                interData.CallType = call_data.callType;
                            }
                            else
                            {
                                interData.CallType = "-";
                            }
                            //if (call_data.gsm_android != null)
                            //{
                            //    gsmAndroid = "(" + call_data.gsm_android + ")";
                            //}
                            interData.IsCallInitiated = call_data.isCallinitaited;// + gsmAndroid;
                            interData.gsmAndroid = call_data.gsm_android;

                            if (!string.IsNullOrEmpty(call_data.makeCallFrom))
                            {
                                interData.CallMadeType = call_data.makeCallFrom;
                            }
                            else
                            {
                                interData.CallMadeType = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.dailedNumber))
                            {
                                interData.DailedNo = call_data.dailedNumber;
                            }
                            else
                            {
                                interData.DailedNo = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.SecondaryDisposition))
                            {
                                interData.SecondaryDispo = call_data.SecondaryDisposition;
                            }
                            else
                            {
                                interData.SecondaryDispo = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.SecondaryDisposition))
                            {
                                if (call_data.SecondaryDisposition == "Call Me Later")
                                {
                                    interData.Details = Convert.ToDateTime(call_data.psfFollowUpDate).ToString("dd-MM-yyyy") + "/" + call_data.psfFollowUpTime;
                                }
                                else if (!string.IsNullOrEmpty(call_data.Remarks))
                                {
                                    interData.Details = call_data.Remarks;
                                }
                                else
                                {
                                    interData.Details = "-";
                                }
                            }
                            else
                            {
                                interData.Details = "-";
                            }


                            if (!string.IsNullOrEmpty(call_data.Remarks))
                            {
                                interData.CreRemarks = call_data.Remarks;
                            }
                            else
                            {
                                interData.CreRemarks = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.Comments))
                            {
                                interData.Feedback = call_data.Comments;
                            }
                            else
                            {
                                interData.Feedback = "-";
                            }



                            //filepath
                            if (!string.IsNullOrEmpty(call_data.filepath))
                            {
                                interData.FilePath = call_data.filepath;
                            }
                            else
                            {
                                //var callsynchData = db.callSyncDatas.FirstOrDefault(m => m.callinteraction_id == call_data.cicallinteraction_id);
                                //if (callsynchData != null)
                                //{
                                //    interData.FilePath = callsynchData.filepath;
                                //}
                                //else
                                //{
                                var callinteraction = db.callinteractions.FirstOrDefault(m => m.id == call_data.cicallinteraction_id);
                                if (callinteraction != null)
                                {
                                    interData.FilePath = callinteraction.filePath;
                                }
                                else
                                {
                                    interData.FilePath = "-";
                                }
                                //}
                            }
                            interDataList.Add(interData);
                        }
                    }

                    else
                    {
                        //db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                        maxCount = db.callhistorycubes.Where(m => m.vehicle_id == vehicalId).Count();
                        List<callhistorycube> serviceCallHistoryByVehicle = db.callhistorycubes.Where(m => m.vehicle_id == vehicalId).OrderByDescending(m => m.callDate).Skip(start).Take(length).ToList();

                        if (serviceCallHistoryByVehicle != null && serviceCallHistoryByVehicle.Count() > 0)
                        {
                            serviceCallHistoryByVehicle = serviceCallHistoryByVehicle.OrderByDescending(m => m.id).ToList();
                        }

                        foreach (var call_data in serviceCallHistoryByVehicle)
                        {
                            interactiondata interData = new interactiondata();
                            //interactiondata interData = new interactiondata();

                            interData.CallDate = Convert.ToDateTime(call_data.callDate).ToString("dd-MM-yyyy");
                            interData.Campaign = call_data.calling_data_type;
                            interData.CreId = call_data.Cre_Name;
                            interData.AssignId = "AASIGNID" + call_data.assignedInteraction_id.ToString();
                            interData.Time = call_data.callTime.Split('.')[0];

                            if (string.IsNullOrEmpty(call_data.callType))
                            {
                                interData.CallType = "-";
                            }
                            else
                            {
                                interData.CallType = call_data.callType;
                            }

                            if (string.IsNullOrEmpty(call_data.isCallinitaited))
                            {
                                interData.IsCallInitiated = "-";
                            }
                            else
                            {
                                //if (call_data.gsm_android != null)
                                //{
                                //    gsmAndroid = "(" + call_data.gsm_android + ")";
                                //}
                                interData.IsCallInitiated = call_data.isCallinitaited;// + gsmAndroid;
                                interData.gsmAndroid = call_data.gsm_android;
                            }

                            if (string.IsNullOrEmpty(call_data.makeCallFrom))
                            {
                                interData.CallMadeType = "-";
                            }
                            else
                            {
                                interData.CallMadeType = call_data.makeCallFrom;
                            }

                            if (string.IsNullOrEmpty(call_data.dailedNumber))
                            {
                                interData.DailedNo = "-";
                            }
                            else
                            {
                                interData.DailedNo = call_data.dailedNumber;
                            }

                            if (string.IsNullOrEmpty(call_data.secondary_dispostion))
                            {
                                interData.SecondaryDispo = "-";
                            }
                            else
                            {
                                interData.SecondaryDispo = call_data.secondary_dispostion;
                            }

                            if (string.IsNullOrEmpty(call_data.nextServicetype))
                            {
                                interData.ServiveType = "-";
                            }
                            else
                            {
                                interData.ServiveType = call_data.nextServicetype;
                            }

                            if (!string.IsNullOrEmpty(call_data.secondary_dispostion))
                            {
                                if (call_data.secondary_dispostion == "Call Me Later")
                                {
                                    interData.Details = Convert.ToDateTime(call_data.followUpDate).ToString("dd-MM-yyyy") + " " + call_data.followUpTime;
                                }
                                else if (call_data.secondary_dispostion == "Book My Service")
                                {
                                    if (call_data.Reshedule_Status != null && call_data.Reshedule_Status == "Rescheduled")
                                    {
                                        //sr_data.NoServiceReason = Convert.ToDateTime(call_data.scheduledDateTime).ToString("dd-MM-yyyy HH:mm:ss");
                                        interData.Details = call_data.Tertiary_disposition + "|" + call_data.Reshedule_Status + "|" + call_data.typeofpickup;
                                    }
                                    else
                                    {
                                        //sr_data.NoServiceReason = Convert.ToDateTime(call_data.scheduledDateTime).ToString("dd-MM-yyyy HH:mm:ss");
                                        interData.Details = call_data.Tertiary_disposition + "|" + call_data.typeofpickup;
                                    }
                                }
                                else if (!string.IsNullOrEmpty(call_data.currentMileage) && call_data.noServiceReason == "Kms Not Covered")
                                {
                                    interData.Details = call_data.noServiceReason + "/" + call_data.currentMileage + " Km";
                                }
                                else if (!string.IsNullOrEmpty(call_data.transferedCity) && call_data.noServiceReason == "Distance from Dealer Location")
                                {
                                    interData.Details = call_data.noServiceReason + "/" + call_data.transferedCity;
                                }
                                else if (!string.IsNullOrEmpty(call_data.Reason) && call_data.noServiceReason == "Other Service")
                                {
                                    interData.Details = call_data.noServiceReason + "/" + call_data.Reason;
                                }
                                else if (call_data.secondary_dispostion == "Service Not Required")
                                {
                                    interData.Details = call_data.noServiceReason + "/" + call_data.authorisedOrNot + "/" + call_data.alredyServicedDealerName;
                                }
                                else
                                {
                                    interData.Details = call_data.noServiceReason;
                                }
                            }
                            else
                            {
                                interData.Details = "-";
                            }

                            if (string.IsNullOrEmpty(call_data.customer_remarks))
                            {
                                interData.CreRemarks = "-";
                            }
                            else
                            {
                                interData.CreRemarks = call_data.customer_remarks;
                            }

                            if (string.IsNullOrEmpty(call_data.customerRemarks))
                            {
                                interData.Feedback = "-";
                            }
                            else
                            {
                                interData.Feedback = call_data.customerRemarks;
                            }


                            //filepath
                            if (!string.IsNullOrEmpty(call_data.filepath))
                            {
                                interData.FilePath = call_data.filepath;
                            }
                            else
                            {
                                // var callsynchData = db.callSyncDatas.FirstOrDefault(m => m.callinteraction_id == call_data.Call_interaction_id);
                                //if(callsynchData!=null)
                                // {
                                //     if(!string.IsNullOrEmpty(callsynchData.filepath))
                                //     {
                                //         interData.FilePath = callsynchData.filepath;
                                //     }
                                //     else
                                //     {
                                //         interData.FilePath = "-";
                                //     }

                                // }
                                // else
                                // {
                                var callinteraction = db.callinteractions.FirstOrDefault(m => m.id == call_data.Call_interaction_id);
                                if (callinteraction != null)
                                {
                                    if (!string.IsNullOrEmpty(callinteraction.filePath))
                                    {
                                        interData.FilePath = callinteraction.filePath;
                                    }
                                    else
                                    {
                                        interData.FilePath = "-";
                                    }

                                }
                                else
                                {
                                    interData.FilePath = "-";
                                }
                                //}
                            }


                            interDataList.Add(interData);
                        }
                    }
                }

                return Json(new { data = interDataList, draw = Request["draw"], recordsTotal = maxCount, recordsFiltered = maxCount, exception = exception }, JsonRequestBehavior.AllowGet);
                //return Json(new { success = true, interaction_list = callhistoryData });
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
                return Json(new { data = "", draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0, exception = exception }, JsonRequestBehavior.AllowGet);
            }

        }

        //********************************** Start of PSF *************************************
        public long recordPSFDisposition(int stage, AutoSherDBContext db, int? finalDispoId = 0, long? psf_assigninter = 0)
        {
            psfassignedinteraction psfinter = new psfassignedinteraction();
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            long userId = Convert.ToInt32(Session["UserId"].ToString());
            //try
            //{
            int campaignId = 0;

            if (Session["psfDayType"] != null)
            {
                campaignId = Convert.ToInt32(Session["psfDayType"].ToString());
            }


            //using (var db = new AutoSherDBContext())
            //{
            //if (campaignId != 7)
            //{
            //    psf_assigninter = db.callinteractions.FirstOrDefault(m => m.id == psf_assigninter).psfAssignedInteraction_id;
            //}

            if (stage == 1)//initiated
            {
                psfinter = db.psfassignedinteractions.FirstOrDefault(m => m.customer_id == cusId && m.vehicle_vehicle_id == vehiId && m.campaign_id == campaignId);
                psfinter.callMade = "Initiated";
                db.SaveChanges();
                //string lastDispo = db.calldispositiondatas.SingleOrDefault(m => m.dispositionId == psfinter.finalDisposition_id).disposition;
            }
            else if (stage == 2)
            {
                string lastDispo = "";
                psfinter = db.psfassignedinteractions.FirstOrDefault(m => m.id == psf_assigninter);
                psfinter.callMade = "Yes";
                if (psfinter.finalDisposition_id != null)
                {
                    lastDispo = db.calldispositiondatas.SingleOrDefault(m => m.dispositionId == psfinter.finalDisposition_id).disposition;
                }

                if (finalDispoId == 63)
                {
                    psfinter.isResolved = true;
                }

                psfinter.lastDisposition = lastDispo;
                psfinter.finalDisposition_id = finalDispoId;
                db.SaveChanges();
            }
            //}
            //}
            //catch (Exception ex)
            //{

            //}

            return psfinter.id;
        }


        //PSF HTTP Get Method
        public ActionResult CallLogging_PSF(string id)
        {

            id = string.Empty;
            if (Session["inComingParameter"] != null)
            {
                id = Session["inComingParameter"].ToString();
            }
            else
            {
                TempData["Exceptions"] = "Invalid Url Operation..[CallLogging]....";
                return RedirectToAction("LogOff", "Home");
            }

            CallLoggingViewModel callLog = new CallLoggingViewModel();
            ViewBag.typeOfDispo = "PSF";

            long cid, vehicle_id, interactionid, dispositionHistory, typeOfPSF;
            string pageFor;
            pageFor = id.Split(',')[0];
            cid = Convert.ToInt32(id.Split(',')[1]);
            vehicle_id = Convert.ToInt32(id.Split(',')[2]);
            interactionid = Convert.ToInt32(id.Split(',')[3]);
            dispositionHistory = Convert.ToInt32(id.Split(',')[4]);
            typeOfPSF = Convert.ToInt32(id.Split(',')[5]);

            ViewBag.typeOfPSF = typeOfPSF;
            ViewBag.interactionid = interactionid;
            ViewBag.dispositionHistory = dispositionHistory;
            ViewBag.vehiId = vehicle_id;


            int UserId = Convert.ToInt32(Session["UserId"].ToString());

            PSF_CallLogging psf = new PSF_CallLogging();

            callLog.workshopList = new List<workshop>();
            callLog.wyzuser = new wyzuser();
            callLog.lastPSFAssign = new psfassignedinteraction();
            callLog.lastPSFAssignStatus = new psfassignedinteraction();
            callLog.LatestInsurance = new insurance();
            callLog.psfLastInteraction = new psfinteraction();
            callLog.cust = new customer();
            callLog.vehi = new vehicle();
            callLog.templates = new List<smstemplate>();
            callLog.lastService = new service();
            callLog.LatestInsurance = new insurance();
            callLog.Latestservices = new service();
            callLog.finaldispostion = new calldispositiondata();

            try
            {
                using (var db = new AutoSherDBContext())
                {

                    long assignWyzId = 0;
                    if (pageFor == "Search")
                    {
                        if (db.psfassignedinteractions.Any(m => m.customer_id == cid && m.vehicle_vehicle_id == vehicle_id))
                        {
                            assignWyzId = db.psfassignedinteractions.FirstOrDefault(m => m.customer_id == cid && m.vehicle_vehicle_id == vehicle_id).wyzUser_id ?? default(long);
                            long wyzUserId = long.Parse(Session["UserId"].ToString());
                            if (wyzUserId == assignWyzId)
                            {
                                Session["PageFor"] = "CRE";
                            }
                            else
                            {
                                Session["PageFor"] = "Search";
                            }
                        }
                        else
                        {
                            Session["PageFor"] = "CREManager";
                        }
                    }
                    else if (pageFor == "CREManager")
                    {
                        Session["PageFor"] = "CREManager";
                    }
                    else if (pageFor == "CRE")
                    {
                        Session["PageFor"] = "CRE";
                    }


                    callLog.wyzuser = db.wyzusers.Include("location").SingleOrDefault(m => m.id == UserId);
                    callLog.cust = db.customers.Include("phones").Include("segments").Include("emails").Include("addresses").Include("vehicles").Include("insurances").FirstOrDefault(m => m.id == cid);
                    callLog.psf_Qt_History = db.psf_qt_history.Where(m => m.vehicle_id == vehicle_id).ToList();
                    callLog.vehi = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehicle_id);
                    //Supporting Viewbags
                    ViewBag.citystates = db.citystates.Select(m => new { state = m.state }).OrderBy(m => m.state).Distinct().ToList();
                    string dealername = callLog.wyzuser.dealerName;
                    callLog.allworkshopList = db.workshops.ToList();
                    //long uniqueid = repo.getUniqueIdForCallInitaiating();

                    //7---->3rd Day_________________ 5 is------->5th day
                    if (typeOfPSF == 7)
                    {
                        Session["PSFDay"] = "3rdDay";
                    }
                    if (typeOfPSF == 5)
                    {
                        if (callLog.cust.segments != null)
                        {

                            List<segment> seg = callLog.cust.segments.ToList();

                            string custType = "";

                            foreach (segment se in seg)
                            {

                                if (se.type != null)
                                {
                                    custType = se.type;
                                }

                            }

                            if (custType.ToLower() == "gold")
                            {
                                Session["PSFDay"] = "15thGoldDay";
                            }
                            else
                            {
                                Session["PSFDay"] = "15thNonGoldDay";
                            }
                        }
                    }

                    //Session["PageFor"] =
                    Session["CusId"] = callLog.cust.id;
                    Session["VehiId"] = callLog.vehi.vehicle_id;
                    Session["interactionid"] = interactionid;
                    Session["typeOfDispo"] = "PSF";



                    callLog.workshopList = db.workshops.ToList();

                    long countOfServicePresent = db.services.Where(m => m.vehicle.vehicle_id == vehicle_id && m.lastServiceDate != null).Count(); //call_int_repo.getCountOfServiceHistoryOfVehicle(vehicleData.getVehicle_id());
                    ViewBag.countOfServicePresent = countOfServicePresent;
                    //logger.info("countOfServicePresent count : " + countOfServicePresent);

                    string modeOfServiceExisting = "";

                    //long countOfServicePresent = db.services.Where(m => m.vehicle_vehicle_id == vehId && m.lastServiceDate != null).Count();

                    //latest Service Getting
                    if (countOfServicePresent != 0)
                    {
                        long lastId = db.services.Where(m => m.vehicle_vehicle_id == vehicle_id && m.lastServiceDate != null).Max(k => k.id);

                        callLog.Latestservices = db.services.Include("workshop").SingleOrDefault(m => m.id == lastId);
                    }

                    if (db.insurances.Where(m => m.vehicle != null && m.vehicle.vehicle_id == vehicle_id).Count() != 0)
                    {
                        long lastInsurence = db.insurances.Include("workshop").Where(m => m.vehicle != null && m.vehicle.vehicle_id == vehicle_id).Max(k => k.id);
                        callLog.LatestInsurance = db.insurances.SingleOrDefault(m => m.id == lastInsurence);
                    }



                    List<string> complaintOFCust = new List<string>();
                    complaintOFCust.Add("");
                    complaintOFCust.Add("");

                    if (countOfServicePresent != 0)
                    {
                        List<service> serviceList = db.services.Include("psfassignedinteractions").Where(m => m.vehicle.vehicle_id == vehicle_id && m.lastServiceDate != null).ToList();

                        if (serviceList.Count() != 0)
                        {
                            DateTime maxDate = Convert.ToDateTime(serviceList.Max(m => m.lastServiceDate));
                            callLog.lastService = serviceList.FirstOrDefault(m => m.lastServiceDate == maxDate);
                        }
                        //Hibernate.initialize(lastService.getPsfAssignedInteraction());
                        modeOfServiceExisting = getLatestServiceBookedType(cid, vehicle_id, callLog.lastService);
                    }
                    //Assign Date,Workshop in Box
                    if (db.psfassignedinteractions.Any(m => m.customer_id == cid && m.vehicle_vehicle_id == vehicle_id && m.campaign_id == typeOfPSF))
                    {
                        psfassignedinteraction psfAssign = db.psfassignedinteractions.Include("campaign").FirstOrDefault(m => m.customer_id == cid && m.vehicle_vehicle_id == vehicle_id && m.campaign_id == typeOfPSF);
                        ViewBag.Service_assignCRE = Session["UserName"].ToString();
                        ViewBag.Service_date = Convert.ToDateTime(psfAssign.uplodedCurrentDate).ToString("dd-MM-yyyy");
                        ViewBag.Service_workshop = db.workshops.FirstOrDefault(m => m.id == psfAssign.workshop_id).workshopName;
                        ViewBag.Service_campaign = psfAssign.campaign.campaignName;
                    }

                    callLog.lastPSFAssignStatus = getLastPSFAssignStatusOfVehicle(int.Parse(vehicle_id.ToString()));
                    if (db.insurances.Where(m => m.vehicle != null && m.vehicle.vehicle_id == vehicle_id).Count() != 0)
                    {
                        long lastInsurence = db.insurances.Include("workshop").Where(m => m.vehicle != null && m.vehicle.vehicle_id == vehicle_id).Max(k => k.id);
                        callLog.LatestInsurance = db.insurances.SingleOrDefault(m => m.id == lastInsurence);
                    }

                    callLog.smstemplates = getSMSTemplate("PSF");

                    List<string> optionsList = new List<string>();
                    optionsList.Add("Not Happy");
                    optionsList.Add("Ok");
                    optionsList.Add("Good");
                    optionsList.Add("No Ratings");
                    callLog.locationList = db.locations.ToList();
                    ViewBag.optionList = optionsList;

                    callLog.lastPSFAssignStatus = getLastPSFAssignStatusOfVehicle(int.Parse(vehicle_id.ToString()));
                    callLog.finaldispostion = db.calldispositiondatas.FirstOrDefault(m => m.id == callLog.lastPSFAssignStatus.finalDisposition_id);

                    //************ Taking UpsellLead from Tagging User***************
                    callLog.tags = db.Database.SqlQuery<TaggingView>("select distinct(upsellLeadId) from taggingusers where moduleTypeId=4 or moduleTypeId=5;").ToList();
                    List<TaggingView> allTags = db.Database.SqlQuery<TaggingView>("select upsellLeadId,name,upsellType,wyzUser_id from taggingusers where moduleTypeId=4 or moduleTypeId=5;").ToList();
                    //customer custo = new customer();

                    for (int i = 0; i < callLog.tags.Count(); i++)
                    {
                        callLog.tags[i].name = allTags.FirstOrDefault(m => m.upsellLeadId == callLog.tags[i].upsellLeadId).name;
                        callLog.tags[i].upsellType = allTags.FirstOrDefault(m => m.upsellLeadId == callLog.tags[i].upsellLeadId).upsellType;
                        callLog.tags[i].wyzUserId = allTags.FirstOrDefault(m => m.upsellLeadId == callLog.tags[i].upsellLeadId).wyzUserId;

                    }

                    for (int i = 0; i < callLog.tags.Count(); i++)
                    {
                        callLog.listingForm.upsellleads.Add(new upselllead());
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return View("Call_Logging", callLog);
        }

        [HttpPost]
        public ActionResult addPsfDispo(CallLoggingViewModel callLog)
        {
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            long userId = Convert.ToInt32(Session["UserId"].ToString());
            long interactionId = Convert.ToInt32(Session["interactionid"].ToString());
            string submissionResult = string.Empty;
            psfinteraction psfinter = callLog.psfinteraction;
            ListingForm listingFormData = callLog.listingForm;
            psfassignedinteraction psfassignedinteraction = new psfassignedinteraction();
            callinteraction callinteraction = new callinteraction();
            int currentDispo = 0;
            long psfAss_id = 0;
            //using (var db = new AutoSherDBContext())
            //{
            using (var db = new AutoSherDBContext())
            {
                using (DbContextTransaction dbTrans = db.Database.BeginTransaction())
                {
                    try
                    {

                        if (db.psfassignedinteractions.Any(m => m.customer_id == cusId && m.vehicle_vehicle_id == vehiId))
                        {
                            if (psfinter.isContacted == "No")
                            {
                                currentDispo = db.calldispositiondatas.FirstOrDefault(m => m.disposition == psfinter.PSFDispositon).dispositionId;
                                psfAss_id = recordPSFDisposition(2, db, currentDispo, interactionId);

                                callinteraction.callCount = 1;
                                callinteraction.callDate = DateTime.Now.ToString("dd-MM-yyyy");
                                callinteraction.callMadeDateAndTime = DateTime.Now;
                                callinteraction.callTime = DateTime.Now.ToString("HH:mm:ss");
                                callinteraction.dealerCode = Session["DealerCode"].ToString();
                                callinteraction.dissatPSFintId = 0;
                                if (Session["isCallInitiated"] != null)
                                {
                                    callinteraction.isCallinitaited = "initiated";
                                    callinteraction.makeCallFrom = "PSF";

                                    //if(Session["UniqueId"] !=null)
                                    //{

                                    if (Session["AndroidUniqueId"] != null)
                                    {
                                        callinteraction.uniqueidForCallSync = int.Parse(Session["AndroidUniqueId"].ToString());
                                    }
                                    else if (Session["GSMUniqueId"] != null)
                                    {
                                        callinteraction.uniqueIdGSM = Session["GSMUniqueId"].ToString().Split(';')[0];
                                    }

                                    //}
                                }

                                callinteraction.callStatus = Convert.ToBoolean(Session["NCReason"]);
                                if (Session["DialedNumber"] != null)
                                {
                                    callinteraction.dailedNoIs = Session["DialedNumber"].ToString();
                                }
                                callinteraction.psfAssignedInteraction_id = psfAss_id;
                                callinteraction.customer_id = cusId;
                                callinteraction.vehicle_vehicle_id = vehiId;
                                callinteraction.wyzUser_id = userId;
                                callinteraction.chasserCall = false;
                                callinteraction.agentName = Session["UserName"].ToString();
                                callinteraction.campaign_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).campaign_id;
                                db.callinteractions.Add(callinteraction);
                                db.SaveChanges();


                                if (Session["GSMUniqueId"] != null)
                                {
                                    gsmsynchdata gsm = new gsmsynchdata();
                                    gsm.Callinteraction_id = callinteraction.id;
                                    gsm.CallMadeDateTime = DateTime.Now;
                                    gsm.UniqueGsmId = Session["GSMUniqueId"].ToString().Split(';')[0];
                                    gsm.TenantUrl = Session["GSMUniqueId"].ToString().Split(';')[1];
                                    gsm.callFor = Convert.ToInt32(Session["UserRole1"]);
                                    db.gsmsynchdata.Add(gsm);
                                    db.SaveChanges();
                                }
                                psfinter.callInteraction_id = callinteraction.id;
                                psfinter.callDispositionData_id = currentDispo;
                                db.psfinteractions.Add(psfinter);
                                db.SaveChanges();
                            }
                            else if (psfinter.isContacted == "Yes")
                            {
                                string remarks = "";
                                int upselCount = 0;
                                string disposition = listingFormData.psfDisposition;
                                if (psfinter.isContacted == "Yes")//dissatisfied
                                {
                                    if (psfinter.q1_CompleteSatisfication == "PSFSelf No")
                                    {
                                        currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == "Dissatisfied with PSF").dispositionId;
                                        psfAss_id = recordPSFDisposition(2, db, currentDispo, interactionId);
                                    }
                                    else if (psfinter.q1_CompleteSatisfication == "PSFSelf Yes")
                                    {
                                        currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == "PSF_Yes").dispositionId;
                                        psfAss_id = recordPSFDisposition(2, db, currentDispo, interactionId);
                                    }
                                    else if (disposition == "Call Me Later")
                                    {
                                        currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == "Call Me Later").dispositionId;
                                        psfAss_id = recordPSFDisposition(2, db, currentDispo, interactionId);
                                    }

                                    callinteraction.dissatPSFintId = 0;
                                    callinteraction.callCount = 1;
                                    callinteraction.callDate = DateTime.Now.ToString("dd-MM-yyyy");
                                    callinteraction.callMadeDateAndTime = DateTime.Now;
                                    callinteraction.callTime = DateTime.Now.ToString("HH:mm:ss");
                                    callinteraction.dealerCode = Session["DealerCode"].ToString();
                                    if (Session["isCallInitiated"] != null)
                                    {
                                        callinteraction.isCallinitaited = "initiated";
                                        callinteraction.makeCallFrom = "PSF";

                                        if (Session["AndroidUniqueId"] != null)
                                        {
                                            callinteraction.uniqueidForCallSync = int.Parse(Session["AndroidUniqueId"].ToString());
                                        }
                                        else if (Session["GSMUniqueId"] != null)
                                        {
                                            callinteraction.uniqueIdGSM = Session["GSMUniqueId"].ToString().Split(';')[0];
                                        }
                                    }


                                    callinteraction.callStatus = Convert.ToBoolean(Session["NCReason"]);
                                    if (Session["DialedNumber"] != null)
                                    {
                                        callinteraction.dailedNoIs = Session["DialedNumber"].ToString();
                                    }
                                    callinteraction.psfAssignedInteraction_id = psfAss_id;
                                    callinteraction.customer_id = cusId;
                                    callinteraction.vehicle_vehicle_id = vehiId;
                                    callinteraction.wyzUser_id = userId;
                                    callinteraction.agentName = Session["UserName"].ToString();
                                    callinteraction.campaign_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).campaign_id;
                                    db.callinteractions.Add(callinteraction);
                                    db.SaveChanges();

                                    if (Session["GSMUniqueId"] != null)
                                    {
                                        gsmsynchdata gsm = new gsmsynchdata();
                                        gsm.Callinteraction_id = callinteraction.id;
                                        gsm.CallMadeDateTime = DateTime.Now;
                                        gsm.UniqueGsmId = Session["GSMUniqueId"].ToString().Split(';')[0];
                                        gsm.TenantUrl = Session["GSMUniqueId"].ToString().Split(';')[1];
                                        gsm.callFor = Convert.ToInt32(Session["UserRole1"]);
                                        db.gsmsynchdata.Add(gsm);
                                        db.SaveChanges();
                                    }

                                    if (listingFormData.remarksList.Count > 0)
                                    {
                                        foreach (var str in listingFormData.remarksList)
                                        {
                                            if (remarks == "" && str != "")
                                            {
                                                remarks = str;
                                            }
                                        }
                                    }

                                    psfinter.remarks = remarks;
                                    psfinter.upsellCount = upselCount;

                                    psfinter.callInteraction_id = callinteraction.id;
                                    psfinter.callDispositionData_id = currentDispo;
                                    db.psfinteractions.Add(psfinter);
                                    db.SaveChanges();

                                    if (psfinter.q1_CompleteSatisfication == "PSFSelf Yes")
                                    {
                                        if (listingFormData.LeadYesH == "Yes")
                                        {
                                            foreach (var upsel in listingFormData.upsellleads)
                                            {
                                                if (upsel.taggedTo != null)
                                                {
                                                    upsel.vehicle_vehicle_id = vehiId;
                                                    upsel.psfInteraction_id = psfinter.id;
                                                    //upsel.srDisposition_id = sr_disposition.id;
                                                    db.upsellleads.Add(upsel);
                                                    db.SaveChanges();
                                                    upselCount++;
                                                }
                                            }
                                            psfinter.upsellCount = upselCount;
                                            db.SaveChanges();
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            TempData["SubmissionResult"] = "No assignment found";
                            return RedirectToAction("ReturnToBucket", new { @id = 3 });
                        }
                        dbTrans.Commit();
                        submissionResult = "True";


                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        if (ex.InnerException != null)
                        {
                            if (ex.InnerException.InnerException != null)
                            {
                                submissionResult = ex.InnerException.InnerException.Message;
                            }
                            else
                            {
                                submissionResult = ex.InnerException.Message;
                            }
                        }
                        else
                        {
                            submissionResult = ex.Message;
                        }

                    }
                }
            }
            TempData["SubmissionResult"] = submissionResult;
            return RedirectToAction("ReturnToBucket", new { @id = 3 });
        }


        #region IndusPsf Flow
        //PSF HTTP Get Method
        //public ActionResult CallLoggingIndus_PSF(string id)
        //{

        //    Session["RoleFor"] = null;
        //    Session["PageFor"] = null;
        //    Session["CusId"] = null;
        //    Session["VehiId"] = null;
        //    Session["typeOfDispo"] = null;
        //    Session["appointBookId"] = null;
        //    Session["isCallInitiated"] = null;
        //    Session["AndroidUniqueId"] = null;
        //    Session["GSMUniqueId"] = null;
        //    Session["DialedNumber"] = null;
        //    Session["psfDayType"] = null;
        //    Session["interactionid"] = null;
        //    Session["MakeCallFrom"] = null;
        //    Session["isPSFRM"] = null;
        //    Session["NCReason"] = null;

        //    if (Session["inComingParameter"] != null)
        //    {
        //        id = string.Empty;
        //        id = Session["inComingParameter"].ToString();
        //    }
        //    else
        //    {
        //        TempData["Exceptions"] = "Invalid Url Operation..[CallLogging]....";
        //        if (Session["UserRole"].ToString() != "CREManager" && Session["UserRole"].ToString() != "RM")
        //        {
        //            return RedirectToAction("LogOff", "Home");
        //        }
        //        //return RedirectToAction("LogOff", "Home");
        //    }

        //    CallLoggingViewModel callLog = new CallLoggingViewModel();
        //    ViewBag.typeOfDispo = "PSF";

        //    long cid, vehicle_id, interactionid, dispositionHistory, typeOfPSF, lastworkshopId = 0;
        //    string pageFor;
        //    pageFor = id.Split(',')[0];
        //    cid = Convert.ToInt32(id.Split(',')[1]);
        //    vehicle_id = Convert.ToInt32(id.Split(',')[2]);
        //    interactionid = Convert.ToInt32(id.Split(',')[3]);
        //    dispositionHistory = Convert.ToInt32(id.Split(',')[4]);
        //    typeOfPSF = Convert.ToInt32(id.Split(',')[5]);
        //    ViewBag.typeOfPSF = typeOfPSF;
        //    ViewBag.interactionid = interactionid;

        //    Session["psfDayType"] = typeOfPSF;
        //    if (dispositionHistory == 900)
        //    {
        //        ViewBag.dispositionHistory = "PSFComplaints";
        //        ViewBag.typeOfPSF = 900;
        //    }
        //    else if (dispositionHistory == 500)
        //    {
        //        Session["isPSFRM"] = true;
        //        ViewBag.dispositionHistory = "PSFRM";
        //    }
        //    else if (dispositionHistory == 63)
        //    {
        //        ViewBag.isResolved = true;
        //        ViewBag.dispositionHistory = "PSFDays";
        //    }
        //    else
        //    {
        //        ViewBag.isResolved = false;
        //        ViewBag.dispositionHistory = "PSFDays";
        //    }

        //    ViewBag.vehiId = vehicle_id;

        //    long UserId = Convert.ToInt32(Session["UserId"].ToString());

        //    PSF_CallLogging psf = new PSF_CallLogging();

        //    callLog.workshopList = new List<workshop>();
        //    callLog.wyzuser = new wyzuser();
        //    callLog.lastPSFAssign = new psfassignedinteraction();
        //    callLog.lastPSFAssignStatus = new psfassignedinteraction();
        //    callLog.LatestInsurance = new insurance();
        //    callLog.psfLastInteraction = new psfinteraction();
        //    callLog.cust = new customer();
        //    callLog.vehi = new vehicle();
        //    callLog.rework = new rework();
        //    callLog.templates = new List<smstemplate>();
        //    callLog.lastService = new service();
        //    callLog.LatestInsurance = new insurance();
        //    callLog.smrCall = new smrforecasteddata();
        //    callLog.listingForm = new ListingForm();
        //    callLog.listingForm.upsellleads = new List<upselllead>();
        //    callLog.psfPullOuts = new List<PsfPullOut>();
        //    callLog.emailtemplates = new List<emailtemplate>();
        //    callLog.Latestservices = new service();
        //    callLog.finaldispostion = new calldispositiondata();
        //    callLog.indusPsfInteraction = new IndusPSFInteraction();
        //    callLog.custPhoneList = new List<phone>();

        //    //callLog.listingForm.upsellleads = new List<upselllead>();

        //    try
        //    {
        //        using (var db = new AutoSherDBContext())
        //        {

        //            long assignWyzId = 0;
        //            if (pageFor == "Shw")
        //            {
        //                Session["PageFor"] = "CRE";
        //            }
        //            else if (pageFor == "Hdd")
        //            {
        //                Session["PageFor"] = "Search";
        //            }
        //            else if (pageFor == "CREManager")
        //            {
        //                Session["PageFor"] = "CREManager";
        //            }
        //            else if (pageFor == "CRE")
        //            {
        //                Session["PageFor"] = "CRE";
        //            }


        //            callLog.wyzuser = db.wyzusers.Include("location").SingleOrDefault(m => m.id == UserId);
        //            callLog.cust = db.customers.Include("segments").Include("emails").Include("addresses").Include("vehicles").Include("insurances").FirstOrDefault(m => m.id == cid);
        //            callLog.custPhoneList = db.Database.SqlQuery<phone>("select * from phone p join(select max(phone_Id)phid from phone where customer_id=@custId group by phoneNumber)Ph on ph.phid=p.phone_Id and p.customer_id=@custId order by phone_Id desc Limit 0,5", new MySqlParameter("@custId", cid)).ToList();

        //            callLog.psf_Qt_History = db.psf_qt_history.Where(m => m.vehicle_id == vehicle_id).ToList();
        //            callLog.vehi = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehicle_id);
        //            //Supporting Viewbags
        //            ViewBag.citystates = db.citystates.Select(m => new { state = m.state }).Distinct().ToList();
        //            string dealername = callLog.wyzuser.dealerName;

        //            var userWorkShopList = db.userworkshops.Where(m => m.userWorkshop_id == UserId).Select(m => m.workshopList_id).ToList();
        //            callLog.workshopList = db.workshops.Where(m => userWorkShopList.Contains(m.id)).OrderByDescending(m => m.workshopName).ToList();
        //            //long uniqueid = repo.getUniqueIdForCallInitaiating();

        //            Session["PSFDay"] = "6thDay";

        //            callLog.emailtemplates = getEmailTemplateList("PSF");
        //            //Session["PageFor"] =
        //            Session["CusId"] = callLog.cust.id;
        //            Session["VehiId"] = callLog.vehi.vehicle_id;
        //            Session["interactionid"] = interactionid;
        //            Session["typeOfDispo"] = "PSF";
        //            //callLog.workshopList = db.workshops.ToList();

        //            long countOfServicePresent = db.services.Where(m => m.vehicle.vehicle_id == vehicle_id && m.lastServiceDate != null).Count(); //call_int_repo.getCountOfServiceHistoryOfVehicle(vehicleData.getVehicle_id());
        //            ViewBag.countOfServicePresent = countOfServicePresent;
        //            string modeOfServiceExisting = "";

        //            //latest Service Getting
        //            if (countOfServicePresent != 0)
        //            {
        //                long lastId = db.services.Where(m => m.vehicle_vehicle_id == vehicle_id && m.lastServiceDate != null).Max(k => k.id);

        //                callLog.Latestservices = db.services.Include("workshop").SingleOrDefault(m => m.id == lastId);
        //            }

        //            callLog.psfPullOuts = db.Database.SqlQuery<PsfPullOut>("select jobCardNumber as JobCardNo,psfCallingDayType as PSFDay,max(id) as CubeId from psfcallhistorycube where vehicle_vehicle_id=" + vehicle_id + " group by psfCallingDayType,jobCardNumber;").ToList();

        //            if (db.insurances.Where(m => m.vehicle != null && m.vehicle.vehicle_id == vehicle_id).Count() != 0)
        //            {
        //                long lastInsurence = db.insurances.Include("workshop").Where(m => m.vehicle != null && m.vehicle.vehicle_id == vehicle_id).Max(k => k.id);
        //                callLog.LatestInsurance = db.insurances.SingleOrDefault(m => m.id == lastInsurence);
        //            }

        //            List<string> complaintOFCust = new List<string>();
        //            complaintOFCust.Add("");
        //            complaintOFCust.Add("");

        //            if (countOfServicePresent != 0)
        //            {
        //                List<service> serviceList = db.services.Include("psfassignedinteractions").Where(m => m.vehicle.vehicle_id == vehicle_id && m.lastServiceDate != null).ToList();

        //                if (serviceList.Count() != 0)
        //                {
        //                    DateTime maxDate = Convert.ToDateTime(serviceList.Max(m => m.lastServiceDate));
        //                    callLog.lastService = serviceList.FirstOrDefault(m => m.lastServiceDate == maxDate);
        //                }
        //                //Hibernate.initialize(lastService.getPsfAssignedInteraction());
        //                modeOfServiceExisting = getLatestServiceBookedType(cid, vehicle_id, callLog.lastService);
        //            }

        //            //Next Service Box Data
        //            if (db.smrforecasteddatas.Any(m => m.vehicle_id == vehicle_id))
        //            {
        //                callLog.smrCall = db.smrforecasteddatas.SingleOrDefault(m => m.vehicle_id == vehicle_id);
        //            }

        //            if (callLog.smrCall.nextServiceDate != null)
        //            {
        //                //DateTime.Now.ToShortTimeString
        //                long diff = Convert.ToInt32(DateTime.Now.ToLocalTime().Hour) - Convert.ToInt32(Convert.ToDateTime(callLog.smrCall.nextServiceDate).ToLocalTime().Hour);
        //                long diffDays = diff / (24 * 60 * 60 * 1000);

        //                //long years = (diffDays / 365);
        //                long months = (diffDays / 30);
        //                //long days = (diffDays % 365) % 7;

        //                //String disp = String.valueOf(years) + "Y " + String.valueOf(months) + "M " + String.valueOf(days)+ "D";
        //                string disp = months.ToString() + "M";
        //                ViewBag.noShowCall = disp;
        //            }
        //            else
        //            {

        //                ViewBag.noShowCall = "";
        //            }

        //            #region Box View AssignCre,UpdatedDate,Workshop and Campaign for all(S,I,P)

        //            //Assign CRE Data workshop ect -------------------------> Service
        //            if (db.assignedinteractions.Any(m => m.customer_id == cid && m.vehical_Id == vehicle_id))
        //            {
        //                assignedinteraction assignInter = db.assignedinteractions.Include("campaign").Include("wyzuser").FirstOrDefault(m => m.customer_id == cid && m.vehical_Id == vehicle_id);
        //                //if (callLog.callinteraction.assignedinteraction != null)
        //                //{
        //                if (assignInter.wyzuser != null)
        //                {
        //                    ViewBag.Service_assignCRE = assignInter.wyzuser.userName;
        //                    if (assignInter.location_id != 0 && assignInter.location_id != null)
        //                    {
        //                        ViewBag.Service_workshop = db.workshops.FirstOrDefault(m => m.id == assignInter.location_id).workshopName;
        //                    }
        //                    else
        //                    {
        //                        ViewBag.Service_workshop = "-";
        //                    }
        //                }
        //                else
        //                {
        //                    ViewBag.Service_assignCRE = "Not Assigned";
        //                    ViewBag.Service_workshop = "-";
        //                }

        //                if (assignInter.uplodedCurrentDate == null)
        //                {
        //                    ViewBag.Service_date = "-";
        //                }
        //                else
        //                {
        //                    ViewBag.Service_date = Convert.ToDateTime(assignInter.uplodedCurrentDate).ToString("dd-MM-yyyy");
        //                }
        //                //
        //                if (assignInter.nextServiceDate != null)
        //                {
        //                    ViewBag.nextServiceDate = Convert.ToDateTime(assignInter.nextServiceDate).ToString("dd-MM-yyyy");
        //                }

        //                if (!string.IsNullOrEmpty(assignInter.nextServiceType))
        //                {
        //                    long ServiceTypeId = long.Parse(assignInter.nextServiceType);

        //                    servicetype type = db.servicetypes.FirstOrDefault(m => m.id == ServiceTypeId);
        //                    if (type != null)
        //                    {
        //                        ViewBag.nextServiceType = type.serviceTypeName;
        //                    }
        //                }


        //                if (assignInter.campaign != null)
        //                {
        //                    ViewBag.Service_campaign = assignInter.campaign.campaignName;
        //                }
        //                else
        //                {
        //                    ViewBag.Service_campaign = "-";
        //                }


        //                if (assignInter.tagging_id != null && assignInter.tagging_id != 0)
        //                {
        //                    if (db.campaigns.Any(m => m.id == assignInter.campaign_id))
        //                    {
        //                        ViewBag.servicetaging_name = db.campaigns.FirstOrDefault(m => m.id == assignInter.tagging_id).campaignName;
        //                    }
        //                    else
        //                    {
        //                        ViewBag.servicetaging_name = "-";
        //                    }
        //                }
        //                else
        //                {
        //                    ViewBag.servicetaging_name = "-";
        //                }

        //            }

        //            //Assign CRE Data Workshop ect --------------------------->Insurance
        //            if (db.insuranceassignedinteractions.Any(m => m.customer_id == cid && m.vehicle_vehicle_id == vehicle_id))
        //            {
        //                insuranceassignedinteraction interasction = db.insuranceassignedinteractions.Include("wyzuser").Include("campaign").Include("wyzuser.workshop").FirstOrDefault(m => m.customer_id == cid && m.vehicle_vehicle_id == vehicle_id);
        //                if (interasction.wyzuser != null)
        //                {
        //                    ViewBag.insurance_cre = interasction.wyzuser.userName;
        //                    ViewBag.insurance_creName = interasction.wyzuser.firstName;

        //                    if (interasction.location_id != 0 && interasction != null)
        //                    {
        //                        ViewBag.insurance_workshop = db.workshops.FirstOrDefault(m => m.id == interasction.location_id).workshopName;
        //                    }
        //                    else
        //                    {
        //                        ViewBag.insurance_workshop = "-";
        //                    }

        //                }
        //                else
        //                {
        //                    ViewBag.insurance_cre = "Not Assigned";
        //                    ViewBag.insurance_creName = "-";

        //                    ViewBag.insurance_workshop = "-";
        //                }

        //                if (interasction.uplodedCurrentDate == null)
        //                {
        //                    ViewBag.insurance_date = "-";
        //                }
        //                else
        //                {
        //                    ViewBag.insurance_date = Convert.ToDateTime(interasction.uplodedCurrentDate).ToString("dd-MM-yyyy");
        //                }


        //                if (interasction.campaign != null)
        //                {
        //                    ViewBag.insurance_campaign = interasction.campaign.campaignName;
        //                }
        //                else
        //                {
        //                    ViewBag.insurance_campaign = "-";
        //                }


        //                if (interasction.tagging_id != null && interasction.tagging_id != 0)
        //                {
        //                    if (db.campaigns.Any(m => m.id == interasction.campaign_id))
        //                    {
        //                        ViewBag.insurancetaging_name = db.campaigns.FirstOrDefault(m => m.id == interasction.tagging_id).campaignName;
        //                    }
        //                    else
        //                    {
        //                        ViewBag.insurancetaging_name = "-";
        //                    }
        //                }
        //                else
        //                {
        //                    ViewBag.insurancetaging_name = "-";
        //                }
        //            }
        //            string ceiStatus = db.Database.SqlQuery<string>("select if(saledate between  date_format(date_sub((select billdate from service where vehicle_vehicle_id=vehicle_id  order by id desc limit 0,1),interval 36 month),'%Y-%m-01') and  date_sub((select billdate from service where vehicle_vehicle_id=vehicle_id  order by id desc limit 0,1),interval 12 month),'CEI ','NONCEI ')   from vehicle where  vehicle_id=@id;", new MySqlParameter("@id", vehicle_id)).FirstOrDefault();
        //            ViewBag.ceicustCat = ceiStatus;
        //            //Assign CRE Data Workshop ect --------------------------->PSF
        //            if (db.psfassignedinteractions.Any(m => m.vehicle_vehicle_id == vehicle_id))
        //            {
        //                List<psfassignedinteraction> psfAssign = new List<psfassignedinteraction>();
        //                psfAssign = db.psfassignedinteractions.Include("campaign").Include("wyzuser").Where(m => m.vehicle_vehicle_id == vehicle_id).ToList();

        //                if (psfAssign != null && psfAssign.Count > 0)
        //                {



        //                    callLog.psfboxview = new List<psfBoxView>();

        //                    foreach (var psfEle in psfAssign)
        //                    {
        //                        psfBoxView psfView = new psfBoxView();

        //                        if (psfEle.campaign_id == typeOfPSF)
        //                        {
        //                            lastworkshopId = psfEle.workshop_id ?? default(long);
        //                        }

        //                        if (psfEle.wyzuser != null)
        //                        {
        //                            psfView.wyzUser = psfEle.wyzuser.userName;
        //                        }
        //                        else
        //                        {
        //                            psfView.wyzUser = "Not Assigned";
        //                        }

        //                        if (psfEle.campaign != null)
        //                        {
        //                            psfView.campaignName = psfEle.campaign.campaignName;
        //                        }
        //                        else
        //                        {
        //                            psfView.campaignName = "-";
        //                        }

        //                        if (psfEle.workshop_id != null && psfEle.workshop_id != 0)
        //                        {
        //                            psfView.workshopName = db.workshops.FirstOrDefault(m => m.id == psfEle.workshop_id).workshopName;
        //                        }
        //                        else
        //                        {
        //                            psfView.workshopName = "-";
        //                        }

        //                        if (psfEle.uplodedCurrentDate != null)
        //                        {
        //                            psfView.updateedDate = Convert.ToDateTime(psfEle.uplodedCurrentDate).ToString("dd-MM-yyyy");
        //                        }
        //                        else
        //                        {
        //                            psfView.updateedDate = "-";
        //                        }


        //                        callLog.psfboxview.Add(psfView);
        //                    }
        //                }
        //            }
        //            #endregion


        //            callLog.lastPSFAssignStatus = getLastPSFAssignStatusOfVehicle(int.Parse(vehicle_id.ToString()));
        //            if (db.insurances.Where(m => m.vehicle != null && m.vehicle.vehicle_id == vehicle_id).Count() != 0)
        //            {
        //                long lastInsurence = db.insurances.Include("workshop").Where(m => m.vehicle != null && m.vehicle.vehicle_id == vehicle_id).Max(k => k.id);
        //                callLog.LatestInsurance = db.insurances.SingleOrDefault(m => m.id == lastInsurence);
        //            }


        //            if (Session["DealerCode"].ToString() != "HARPREETFORD" && Session["DealerCode"].ToString() != "HANSHYUNDAI" && Session["DealerCode"].ToString() != "GALAXYTOYOTA")
        //            {
        //                callLog.smstemplates = getSMSTemplate("PSF");
        //            }

        //            callLog.lastPSFAssignStatus = getLastPSFAssignStatusOfVehicle(int.Parse(vehicle_id.ToString()));
        //            callLog.finaldispostion = db.calldispositiondatas.FirstOrDefault(m => m.id == callLog.lastPSFAssignStatus.finalDisposition_id);

        //            //************ Taking UpsellLead from Tagging User***************
        //            callLog.tags = db.Database.SqlQuery<TaggingView>("select distinct(upsellLeadId) from taggingusers where moduleTypeId=4 or moduleTypeId=5;").ToList();
        //            List<TaggingView> allTags = db.Database.SqlQuery<TaggingView>("select upsellLeadId,name,upsellType,wyzUser_id from taggingusers where moduleTypeId=4 or moduleTypeId=5;").ToList();
        //            //customer custo = new customer();
        //            callLog.locationList = db.locations.ToList();
        //            callLog.allworkshopList = db.workshops.ToList();
        //            for (int i = 0; i < callLog.tags.Count(); i++)
        //            {
        //                callLog.tags[i].name = allTags.FirstOrDefault(m => m.upsellLeadId == callLog.tags[i].upsellLeadId).name;
        //                callLog.tags[i].upsellType = allTags.FirstOrDefault(m => m.upsellLeadId == callLog.tags[i].upsellLeadId).upsellType;
        //                callLog.tags[i].wyzUserId = allTags.FirstOrDefault(m => m.upsellLeadId == callLog.tags[i].upsellLeadId).wyzUserId;
        //            }

        //            for (int i = 0; i < callLog.tags.Count(); i++)
        //            {
        //                callLog.listingForm.upsellleads.Add(new upselllead());
        //            }
        //            if (dispositionHistory == 900 || dispositionHistory == 500)
        //            {


        //                //callLog.listingForm.upsellleads = new List<upselllead>();
        //                for (int i = 0; i < callLog.tags.Count(); i++)
        //                {
        //                    callLog.listingForm.upsellleads.Add(new upselllead());
        //                }
        //                //long maxReowrkId = db.reworks.Where(m => m.customer_id == cid).Max(m => m.id);
        //                //if(maxReowrkId!=0)
        //                //{
        //                //    callLog.rework = db.reworks.FirstOrDefault(m => m.id == maxReowrkId);
        //                //}

        //                if (dispositionHistory == 900)
        //                {
        //                    long dispoComplaintId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Exact Nature of Complaint").id;
        //                    var natureOfComplaints = db.calldispositiondatas.Where(m => m.mainDispositionId == dispoComplaintId).ToList();
        //                    ViewBag.natureOfComplaints = natureOfComplaints;


        //                    long RMId = db.roles.FirstOrDefault(m => m.role1 == "RM").id;
        //                    var RMIdList = db.userroles.Where(m => m.roles_id == RMId).Select(m => m.users_id).ToList();
        //                    var RMList = db.wyzusers.Where(m => m.role == "RM").Select(m => new { id = m.id, name = m.userName }).ToList();
        //                    ViewBag.RMList = RMList;
        //                    ViewBag.locationList = db.locations.Select(m => new { id = m.cityId, name = m.name }).ToList();
        //                }

        //            }
        //            else
        //            {
        //                if (dispositionHistory != 63)
        //                {
        //                    long techId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Technical").id;
        //                    long nontechId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Non Technical").id;


        //                    callLog.techQuestions = db.calldispositiondatas.Where(m => m.mainDispositionId == techId).ToList();
        //                    callLog.nonTechQuestion = db.calldispositiondatas.Where(m => m.mainDispositionId == nontechId).ToList();

        //                    List<calldispositiondata> callDispo;
        //                    callDispo = new List<calldispositiondata>();

        //                    foreach (var dispo in callLog.techQuestions)
        //                    {
        //                        callDispo.AddRange(db.calldispositiondatas.Where(m => m.mainDispositionId == dispo.id));
        //                    }
        //                    callLog.techQuestions.AddRange(callDispo);
        //                    callDispo.Clear();

        //                    foreach (var dispo in callLog.nonTechQuestion)
        //                    {
        //                        callDispo.AddRange(db.calldispositiondatas.Where(m => m.mainDispositionId == dispo.id));
        //                    }
        //                    callLog.nonTechQuestion.AddRange(callDispo);
        //                    callDispo.Clear();

        //                    long complaintMngId = 0;

        //                    if (db.roles.Any(m => m.role1 == "Complaint Manager"))
        //                    {
        //                        complaintMngId = db.roles.FirstOrDefault(m => m.role1 == "Complaint Manager").id;
        //                    }

        //                    if (complaintMngId != 0)
        //                    {
        //                        //long loginUserWorkshopId = db.wyzusers.FirstOrDefault(m => m.id == UserId).workshop_id ?? default(long);
        //                        var complaintCreId = db.userroles.Where(m => m.roles_id == complaintMngId).Select(m => m.users_id).ToList();
        //                        var complaintCre = db.wyzusers.Where(m => complaintCreId.Contains(m.id) && m.workshop_id == lastworkshopId).Select(m => new { id = m.id, name = m.userName }).ToList();
        //                        ViewBag.complaintCre = complaintCre;
        //                    }
        //                    else
        //                    {
        //                        ViewBag.complaintCre = new List<SelectList>();
        //                    }
        //                }

        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Exceptions"] = ex.Message.ToString();
        //        if (ex.Message.Contains("inner exception"))
        //        {
        //            TempData["Exceptions"] = ex.InnerException.Message;
        //        }
        //        TempData["ControllerName"] = "Call Log";
        //        //return RedirectToAction("LogOff", "Home");
        //    }

        //    return View("Call_Logging", callLog);
        //}

        //public ActionResult CallLoggingIndus_PSF(string id)
        //{

        //    Session["RoleFor"] = null;
        //    Session["PageFor"] = null;
        //    Session["CusId"] = null;
        //    Session["VehiId"] = null;
        //    Session["typeOfDispo"] = null;
        //    Session["appointBookId"] = null;
        //    Session["isCallInitiated"] = null;
        //    Session["AndroidUniqueId"] = null;
        //    Session["GSMUniqueId"] = null;
        //    Session["DialedNumber"] = null;
        //    Session["psfDayType"] = null;
        //    Session["interactionid"] = null;
        //    Session["MakeCallFrom"] = null;
        //    Session["isPSFRM"] = null;
        //    Session["NCReason"] = null;

        //    if (Session["inComingParameter"] != null)
        //    {
        //        id = string.Empty;
        //        id = Session["inComingParameter"].ToString();
        //    }
        //    else
        //    {
        //        TempData["Exceptions"] = "Invalid Url Operation..[CallLogging]....";
        //        if (Session["UserRole"].ToString() != "CREManager" && Session["UserRole"].ToString() != "RM")
        //        {
        //            return RedirectToAction("LogOff", "Home");
        //        }
        //        //return RedirectToAction("LogOff", "Home");
        //    }

        //    CallLoggingViewModel callLog = new CallLoggingViewModel();
        //    ViewBag.typeOfDispo = "PSF";

        //    long cid, vehicle_id, interactionid, dispositionHistory, typeOfPSF = 0, lastworkshopId = 0, bucket_id = 0;
        //    string pageFor;
        //    pageFor = id.Split(',')[0];
        //    cid = Convert.ToInt32(id.Split(',')[1]);
        //    vehicle_id = Convert.ToInt32(id.Split(',')[2]);
        //    interactionid = Convert.ToInt32(id.Split(',')[3]);
        //    dispositionHistory = Convert.ToInt32(id.Split(',')[4]);


        //    ViewBag.interactionid = interactionid;


        //    if (dispositionHistory == 900)
        //    {
        //        bucket_id = Convert.ToInt32(id.Split(',')[5]);
        //        ViewBag.dispositionHistory = "PSFComplaints";
        //        ViewBag.typeOfPSF = 900;
        //    }
        //    else if (dispositionHistory == 500)
        //    {
        //        Session["isPSFRM"] = true;
        //        ViewBag.dispositionHistory = "PSFRM";
        //    }
        //    else if (dispositionHistory == 63)
        //    {
        //        typeOfPSF = Convert.ToInt32(id.Split(',')[5]);
        //        ViewBag.typeOfPSF = typeOfPSF;
        //        Session["psfDayType"] = typeOfPSF;
        //        callLog.PsfCreBucketId = 63;
        //        ViewBag.isResolved = true;
        //        ViewBag.dispositionHistory = "PSFDays";
        //    }
        //    else
        //    {
        //        typeOfPSF = Convert.ToInt32(id.Split(',')[5]);
        //        ViewBag.typeOfPSF = typeOfPSF;
        //        Session["psfDayType"] = typeOfPSF;
        //        ViewBag.isResolved = false;
        //        ViewBag.dispositionHistory = "PSFDays";
        //    }

        //    ViewBag.vehiId = vehicle_id;

        //    long UserId = Convert.ToInt32(Session["UserId"].ToString());

        //    PSF_CallLogging psf = new PSF_CallLogging();

        //    callLog.workshopList = new List<workshop>();
        //    callLog.wyzuser = new wyzuser();
        //    callLog.lastPSFAssign = new psfassignedinteraction();
        //    callLog.lastPSFAssignStatus = new psfassignedinteraction();
        //    callLog.LatestInsurance = new insurance();
        //    callLog.psfLastInteraction = new psfinteraction();
        //    callLog.cust = new customer();
        //    callLog.vehi = new vehicle();
        //    callLog.rework = new rework();
        //    callLog.templates = new List<smstemplate>();
        //    callLog.lastService = new service();
        //    callLog.LatestInsurance = new insurance();
        //    callLog.smrCall = new smrforecasteddata();
        //    callLog.listingForm = new ListingForm();
        //    callLog.listingForm.upsellleads = new List<upselllead>();
        //    callLog.psfPullOuts = new List<PsfPullOut>();
        //    callLog.emailtemplates = new List<emailtemplate>();
        //    callLog.Latestservices = new service();
        //    callLog.finaldispostion = new calldispositiondata();
        //    callLog.indusPsfInteraction = new IndusPSFInteraction();
        //    callLog.custPhoneList = new List<phone>();
        //   // callLog.compInteractions = new CompInteraction();
        //   // callLog.rmInteraction = new RMInteraction();

        //    //callLog.listingForm.upsellleads = new List<upselllead>();

        //    try
        //    {
        //        using (var db = new AutoSherDBContext())
        //        {

        //            long assignWyzId = 0;
        //            if (pageFor == "Shw")
        //            {
        //                Session["PageFor"] = "CRE";
        //            }
        //            else if (pageFor == "Hdd")
        //            {
        //                Session["PageFor"] = "Search";
        //            }
        //            else if (pageFor == "CREManager")
        //            {
        //                Session["PageFor"] = "CREManager";
        //            }
        //            else if (pageFor == "CRE")
        //            {
        //                Session["PageFor"] = "CRE";
        //            }

        //            Session["PSFDay"] = "6thDay";

        //            //Session["PageFor"] =
        //            Session["CusId"] = cid;
        //            Session["VehiId"] = vehicle_id;
        //            Session["interactionid"] = interactionid;
        //            Session["typeOfDispo"] = "PSF";
        //            //callLog.workshopList = db.workshops.ToList();

        //            long countOfServicePresent = db.services.Where(m => m.vehicle.vehicle_id == vehicle_id && m.lastServiceDate != null).Count(); //call_int_repo.getCountOfServiceHistoryOfVehicle(vehicleData.getVehicle_id());
        //            ViewBag.countOfServicePresent = countOfServicePresent;


        //            callLog.CustomerId = cid;
        //            callLog.VehicleId = vehicle_id;
        //            callLog.UserId = UserId;
        //            callLog = get360ProfileData(callLog, db, "PSF", Session["DealerCode"].ToString(), Session["OEM"].ToString());
        //            Session["VehiReg"] = callLog.vehi.chassisNo;

        //            //************ Taking UpsellLead from Tagging User***************
        //            callLog.tags = db.Database.SqlQuery<TaggingView>("select distinct(upsellLeadId) from taggingusers where moduleTypeId=4 or moduleTypeId=5;").ToList();
        //            List<TaggingView> allTags = db.Database.SqlQuery<TaggingView>("select upsellLeadId,name,upsellType,wyzUser_id from taggingusers where moduleTypeId=4 or moduleTypeId=5;").ToList();

        //            for (int i = 0; i < callLog.tags.Count(); i++)
        //            {
        //                callLog.tags[i].name = allTags.FirstOrDefault(m => m.upsellLeadId == callLog.tags[i].upsellLeadId).name;
        //                callLog.tags[i].upsellType = allTags.FirstOrDefault(m => m.upsellLeadId == callLog.tags[i].upsellLeadId).upsellType;
        //                callLog.tags[i].wyzUserId = allTags.FirstOrDefault(m => m.upsellLeadId == callLog.tags[i].upsellLeadId).wyzUserId;
        //            }

        //            for (int i = 0; i < callLog.tags.Count(); i++)
        //            {
        //                callLog.listingForm.upsellleads.Add(new upselllead());
        //            }
        //            if (dispositionHistory == 900 || dispositionHistory == 500)
        //            {


        //                //callLog.listingForm.upsellleads = new List<upselllead>();
        //                for (int i = 0; i < callLog.tags.Count(); i++)
        //                {
        //                    callLog.listingForm.upsellleads.Add(new upselllead());
        //                }
        //                //long maxReowrkId = db.reworks.Where(m => m.customer_id == cid).Max(m => m.id);
        //                //if(maxReowrkId!=0)
        //                //{
        //                //    callLog.rework = db.reworks.FirstOrDefault(m => m.id == maxReowrkId);
        //                //}

        //                if (dispositionHistory == 900)
        //                {
        //                    long dispoComplaintId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Exact Nature of Complaint").id;
        //                    var natureOfComplaints = db.calldispositiondatas.Where(m => m.mainDispositionId == dispoComplaintId).ToList();
        //                    ViewBag.natureOfComplaints = natureOfComplaints;


        //                    long RMId = db.roles.FirstOrDefault(m => m.role1 == "RM").id;
        //                    var RMIdList = db.userroles.Where(m => m.roles_id == RMId).Select(m => m.users_id).ToList();
        //                    var RMList = db.wyzusers.Where(m => m.role == "RM").Select(m => new { id = m.id, name = m.userName }).ToList();
        //                    ViewBag.RMList = RMList;
        //                    ViewBag.locationList = db.locations.Select(m => new { id = m.cityId, name = m.name }).ToList();

        //                    //CompInteraction oldCompInter = db.compInteractions.Where(m => m.psfassignedInteraction_id == interactionid).OrderByDescending(m => m.Id).FirstOrDefault();
        //                    //if (oldCompInter != null)
        //                    //{
        //                    //    callLog.compInteractions = oldCompInter;
        //                    //    callLog.compInteractions.bucket_id = Convert.ToInt32(bucket_id);
        //                    //}
        //                }
        //                else if (dispositionHistory == 500)
        //                {
        //                    //RMInteraction oldRmInter = db.rmInteractions.Where(m => m.psfassignedinteraction_id == interactionid).OrderByDescending(m => m.Id).FirstOrDefault();

        //                    //if (oldRmInter != null)
        //                    //{
        //                    //    callLog.rmInteraction = oldRmInter;
        //                    //}
        //                }
        //            }
        //            else
        //            {
        //                if (dispositionHistory != 63)
        //                {
        //                    long techId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Technical").id;
        //                    long nontechId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Non Technical").id;


        //                    callLog.techQuestions = db.calldispositiondatas.Where(m => m.mainDispositionId == techId).ToList();
        //                    callLog.nonTechQuestion = db.calldispositiondatas.Where(m => m.mainDispositionId == nontechId).ToList();

        //                    List<calldispositiondata> callDispo;
        //                    callDispo = new List<calldispositiondata>();

        //                    foreach (var dispo in callLog.techQuestions)
        //                    {
        //                        callDispo.AddRange(db.calldispositiondatas.Where(m => m.mainDispositionId == dispo.id));
        //                    }
        //                    callLog.techQuestions.AddRange(callDispo);
        //                    callDispo.Clear();

        //                    foreach (var dispo in callLog.nonTechQuestion)
        //                    {
        //                        callDispo.AddRange(db.calldispositiondatas.Where(m => m.mainDispositionId == dispo.id));
        //                    }
        //                    callLog.nonTechQuestion.AddRange(callDispo);
        //                    callDispo.Clear();

        //                    long complaintMngId = 0;

        //                    if (db.roles.Any(m => m.role1 == "Complaint Manager"))
        //                    {
        //                        complaintMngId = db.roles.FirstOrDefault(m => m.role1 == "Complaint Manager").id;
        //                    }

        //                    if (complaintMngId != 0)
        //                    {
        //                        lastworkshopId = db.psfassignedinteractions.FirstOrDefault(m => m.id == interactionid).workshop_id ?? default(long);
        //                        //long loginUserWorkshopId = db.wyzusers.FirstOrDefault(m => m.id == UserId).workshop_id ?? default(long);
        //                        var complaintCreId = db.userroles.Where(m => m.roles_id == complaintMngId).Select(m => m.users_id).ToList();
        //                        var complaintCre = db.wyzusers.Where(m => complaintCreId.Contains(m.id) && m.workshop_id == lastworkshopId).Select(m => new { id = m.id, name = m.userName }).ToList();
        //                        ViewBag.complaintCre = complaintCre;
        //                    }
        //                    else
        //                    {
        //                        ViewBag.complaintCre = new List<SelectList>();
        //                    }
        //                }

        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string exception = "";

        //        if (ex.InnerException != null)
        //        {
        //            if (ex.InnerException.InnerException != null)
        //            {
        //                exception = ex.InnerException.InnerException.Message;
        //            }
        //            else
        //            {
        //                exception = ex.InnerException.Message;
        //            }
        //        }
        //        else
        //        {
        //            exception = ex.Message;
        //        }

        //        TempData["Exceptions"] = exception;
        //        TempData["ControllerName"] = "Call Log";
        //        return RedirectToAction("LogOff", "Home");
        //    }

        //    return View("~/Views/CallLogging/Call_Logging.cshtml", callLog);
        //}

        //[HttpPost]
        //public ActionResult addIndusPsfDispo(CallLoggingViewModel callLog)
        //{
        //    long cusId = Convert.ToInt32(Session["CusId"].ToString());
        //    long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
        //    long userId = Convert.ToInt32(Session["UserId"].ToString());
        //    long interactionId = Convert.ToInt32(Session["interactionid"].ToString());
        //    string submissionResult = string.Empty;
        //    IndusPSFInteraction psfinter = callLog.indusPsfInteraction;
        //    ListingForm listingFormData = callLog.listingForm;
        //    psfassignedinteraction psfassignedinteraction = new psfassignedinteraction();
        //    callinteraction callinteraction = new callinteraction();
        //    int currentDispo = 0;
        //    long psfAss_id = 0;
        //    //using (var db = new AutoSherDBContext())
        //    //{
        //    using (var db = new AutoSherDBContext())
        //    {
        //        using (DbContextTransaction dbTras = db.Database.BeginTransaction())
        //        {
        //            try
        //            {

        //                if (db.psfassignedinteractions.Any(m => m.customer_id == cusId && m.vehicle_vehicle_id == vehiId))
        //                {
        //                    if (psfinter.isContacted == "No")
        //                    {
        //                        currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == psfinter.PSFDisposition).dispositionId;
        //                        psfAss_id = recordPSFDisposition(2, db, currentDispo, interactionId);

        //                        callinteraction.callCount = 1;
        //                        callinteraction.callDate = DateTime.Now.ToString("dd-MM-yyyy");
        //                        callinteraction.callMadeDateAndTime = DateTime.Now;
        //                        callinteraction.callTime = DateTime.Now.ToString("HH:mm:ss");
        //                        callinteraction.dealerCode = Session["DealerCode"].ToString();
        //                        callinteraction.dissatPSFintId = 0;
        //                        if (Session["isCallInitiated"] != null)
        //                        {
        //                            callinteraction.isCallinitaited = "initiated";
        //                            callinteraction.makeCallFrom = "PSF";

        //                            if (Session["AndroidUniqueId"] != null)
        //                            {
        //                                callinteraction.uniqueidForCallSync = int.Parse(Session["AndroidUniqueId"].ToString());
        //                            }
        //                            else if (Session["GSMUniqueId"] != null)
        //                            {
        //                                callinteraction.uniqueIdGSM = Session["GSMUniqueId"].ToString().Split(';')[0];
        //                            }
        //                        }
        //                        callinteraction.callStatus = Convert.ToBoolean(Session["NCReason"]);
        //                        if (Session["DialedNumber"] != null)
        //                        {
        //                            callinteraction.dailedNoIs = Session["DialedNumber"].ToString();
        //                        }
        //                        callinteraction.psfAssignedInteraction_id = psfAss_id;
        //                        callinteraction.customer_id = cusId;
        //                        callinteraction.vehicle_vehicle_id = vehiId;
        //                        callinteraction.wyzUser_id = userId;
        //                        callinteraction.chasserCall = false;
        //                        callinteraction.agentName = Session["UserName"].ToString();
        //                        callinteraction.campaign_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).campaign_id ?? default(long);
        //                        callinteraction.service_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).service_id ?? default(long);
        //                        db.callinteractions.Add(callinteraction);
        //                        db.SaveChanges();


        //                        for (int i = 0; i < listingFormData.remarksList.Count; i++)
        //                        {
        //                            if (psfinter.creRemarks == null && listingFormData.commentsList[i] != "")
        //                            {
        //                                psfinter.creRemarks = listingFormData.commentsList[i];
        //                            }

        //                            if (psfinter.customerFeedBack == null && listingFormData.remarksList[i] != "")
        //                            {
        //                                psfinter.customerFeedBack = listingFormData.remarksList[i];
        //                            }
        //                        }

        //                        if (Session["GSMUniqueId"] != null)
        //                        {
        //                            gsmsynchdata gsm = new gsmsynchdata();
        //                            gsm.Callinteraction_id = callinteraction.id;
        //                            gsm.CallMadeDateTime = DateTime.Now;
        //                            gsm.UniqueGsmId = Session["GSMUniqueId"].ToString().Split(';')[0];
        //                            gsm.TenantUrl = Session["GSMUniqueId"].ToString().Split(';')[1];

        //                            db.gsmsynchdata.Add(gsm);
        //                            db.SaveChanges();
        //                        }

        //                        psfinter.callInteraction_id = callinteraction.id;
        //                        psfinter.callDispositionData_id = currentDispo;
        //                        db.indusPSFInteraction.Add(psfinter);
        //                        db.SaveChanges();
        //                    }
        //                    else if (psfinter.isContacted == "Yes")
        //                    {
        //                        string remarks = "";
        //                        if (psfinter.whatCustSaid == "Feedback Given" && psfinter.vehicleAfterService == "Bad" || (psfinter.overallServiceExperience == "Average" || psfinter.overallServiceExperience == "Poor"))
        //                        {

        //                            currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == "Dissatisfied with PSF").dispositionId;
        //                            psfAss_id = recordPSFDisposition(2, db, currentDispo, interactionId);

        //                            if (psfinter.isTechnical == true)
        //                            {
        //                                Dictionary<string, string> techOption = new Dictionary<string, string>();
        //                                techOption = JsonConvert.DeserializeObject<Dictionary<string, string>>(callLog.listingForm.selectedTechnical);

        //                                foreach (var key in techOption)
        //                                {
        //                                    long dispoId = long.Parse(key.Key);
        //                                    string techSuboption = string.Empty;
        //                                    techSuboption = db.calldispositiondatas.FirstOrDefault(m => m.id == dispoId).disposition;

        //                                    if (techSuboption == "Body, Chassis & Upholstery")
        //                                    {
        //                                        psfinter.bodychasis = key.Value;
        //                                    }
        //                                    else if (techSuboption == "Electricals")
        //                                    {
        //                                        psfinter.electricals = key.Value;
        //                                    }
        //                                    else if (techSuboption == "Performance")
        //                                    {
        //                                        psfinter.performance = key.Value;
        //                                    }
        //                                    else if (techSuboption == "Powertrain")
        //                                    {
        //                                        psfinter.powertrain = key.Value;
        //                                    }
        //                                    else if (techSuboption == "Safety")
        //                                    {
        //                                        psfinter.safety = key.Value;
        //                                    }
        //                                    else if (techSuboption == "Steering & Suspension")
        //                                    {
        //                                        psfinter.steeringNsuspention = key.Value;
        //                                    }
        //                                    else if (techSuboption == "Improper Expectation/Usage")
        //                                    {
        //                                        psfinter.improperExpectNUsage = key.Value;
        //                                    }
        //                                }
        //                            }

        //                            if (psfinter.nonTechnincal == true)
        //                            {
        //                                Dictionary<string, string> nonTechOption = new Dictionary<string, string>();
        //                                nonTechOption = JsonConvert.DeserializeObject<Dictionary<string, string>>(callLog.listingForm.selectedNonTechnical);

        //                                foreach (var key in nonTechOption)
        //                                {
        //                                    long dispoId = long.Parse(key.Key);
        //                                    string techSuboption = string.Empty;
        //                                    techSuboption = db.calldispositiondatas.FirstOrDefault(m => m.id == dispoId).disposition;

        //                                    if (techSuboption == "Work Quality")
        //                                    {
        //                                        psfinter.workQuality = key.Value;
        //                                    }
        //                                    else if (techSuboption == "Service Advisor")
        //                                    {
        //                                        psfinter.ServiceAdvisor = key.Value;
        //                                    }
        //                                    else if (techSuboption == "Spare Parts")
        //                                    {
        //                                        psfinter.spareParts = key.Value;
        //                                    }
        //                                    else if (techSuboption == "Billing")
        //                                    {
        //                                        psfinter.billing = key.Value;
        //                                    }
        //                                    else if (techSuboption == "Other")
        //                                    {
        //                                        psfinter.othernonTech = key.Value;
        //                                    }
        //                                }
        //                            }


        //                            //*****************Call Interaction *******************************
        //                            callinteraction.callCount = 1;
        //                            callinteraction.callDate = DateTime.Now.ToString("dd-MM-yyyy");
        //                            callinteraction.callMadeDateAndTime = DateTime.Now;
        //                            callinteraction.callTime = DateTime.Now.ToString("HH:mm:ss");
        //                            callinteraction.dealerCode = Session["DealerCode"].ToString();
        //                            callinteraction.dissatPSFintId = 0;
        //                            if (Session["isCallInitiated"] != null)
        //                            {
        //                                callinteraction.isCallinitaited = "initiated";
        //                                callinteraction.makeCallFrom = "PSF";

        //                                if (Session["AndroidUniqueId"] != null)
        //                                {
        //                                    callinteraction.uniqueidForCallSync = int.Parse(Session["AndroidUniqueId"].ToString());
        //                                }
        //                                else if (Session["GSMUniqueId"] != null)
        //                                {
        //                                    callinteraction.uniqueIdGSM = Session["GSMUniqueId"].ToString().Split(';')[0];
        //                                }
        //                            }
        //                            callinteraction.callStatus = Convert.ToBoolean(Session["NCReason"]);
        //                            if (Session["DialedNumber"] != null)
        //                            {
        //                                callinteraction.dailedNoIs = Session["DialedNumber"].ToString();
        //                            }
        //                            callinteraction.psfAssignedInteraction_id = psfAss_id;
        //                            callinteraction.customer_id = cusId;
        //                            callinteraction.vehicle_vehicle_id = vehiId;
        //                            callinteraction.wyzUser_id = userId;
        //                            callinteraction.agentName = Session["UserName"].ToString();
        //                            callinteraction.campaign_id = long.Parse(Session["psfDayType"].ToString());
        //                            callinteraction.chasserCall = false;
        //                            callinteraction.service_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).service_id ?? default(long);
        //                            db.callinteractions.Add(callinteraction);
        //                            db.SaveChanges();

        //                            if (Session["GSMUniqueId"] != null)
        //                            {
        //                                gsmsynchdata gsm = new gsmsynchdata();
        //                                gsm.Callinteraction_id = callinteraction.id;
        //                                gsm.CallMadeDateTime = DateTime.Now;
        //                                gsm.UniqueGsmId = Session["GSMUniqueId"].ToString().Split(';')[0];
        //                                gsm.TenantUrl = Session["GSMUniqueId"].ToString().Split(';')[1];

        //                                db.gsmsynchdata.Add(gsm);
        //                                db.SaveChanges();
        //                            }
        //                            //******************* rework table**************************
        //                            rework rework = new rework();
        //                            wyzuser user = db.wyzusers.FirstOrDefault(m => m.id == userId);
        //                            rework.Benefits = "No Benefits Applied";
        //                            rework.DissatStatus_id = 44;
        //                            rework.complaint_creid = psfinter.complaintMgr_id ?? default(long);
        //                            rework.campaign_id = long.Parse(Session["psfDayType"].ToString());
        //                            rework.customer_id = cusId;
        //                            rework.vehicle_id = vehiId;
        //                            rework.discount = 0;
        //                            rework.isReworkAvailable = false;
        //                            rework.issuedate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));
        //                            rework.location_id = user.location_cityId ?? default(long);
        //                            rework.psfassignedInteraction_id = psfAss_id;
        //                            rework.workshop_id = user.workshop_id ?? default(long);
        //                            db.reworks.Add(rework);
        //                            db.SaveChanges();
        //                            psfinter.isComplaintRaised = "Yes";

        //                            for (int i = 0; i < listingFormData.remarksList.Count; i++)
        //                            {
        //                                if (psfinter.creRemarks == null && listingFormData.commentsList[i] != "")
        //                                {
        //                                    psfinter.creRemarks = listingFormData.commentsList[i];
        //                                }

        //                                if (psfinter.customerFeedBack == null && listingFormData.remarksList[i] != "")
        //                                {
        //                                    psfinter.customerFeedBack = listingFormData.remarksList[i];
        //                                }
        //                            }

        //                            psfinter.callInteraction_id = callinteraction.id;
        //                            psfinter.callDispositionData_id = currentDispo;
        //                            db.indusPSFInteraction.Add(psfinter);

        //                            db.SaveChanges();
        //                        }
        //                        else if (psfinter.whatCustSaid == "Feedback Given" || psfinter.whatCustSaid == "ConfirmStatus" || psfinter.whatCustSaid == "FollowUp Later" || psfinter.whatCustSaid == "No Feedback" || psfinter.vehicleAfterService == "Good")
        //                        {
        //                            if (psfinter.whatCustSaid == "FollowUp Later")
        //                            {
        //                                currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == "Call Me Later").dispositionId;
        //                                psfAss_id = recordPSFDisposition(2, db, currentDispo, interactionId);
        //                            }
        //                            else if (psfinter.whatCustSaid == "No Feedback")
        //                            {
        //                                currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == "Incomplete Survey").dispositionId;
        //                                psfAss_id = recordPSFDisposition(2, db, currentDispo, interactionId);
        //                            }
        //                            else if (psfinter.whatCustSaid == "ConfirmStatus")
        //                            {
        //                                currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == psfinter.modeOfService).dispositionId;
        //                                psfAss_id = recordPSFDisposition(2, db, currentDispo, interactionId);
        //                            }
        //                            else
        //                            {
        //                                currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == "PSF_Yes").dispositionId;
        //                                psfAss_id = recordPSFDisposition(2, db, currentDispo, interactionId);
        //                            }


        //                            //*****************Call Interaction *******************************
        //                            callinteraction.callCount = 1;
        //                            callinteraction.callDate = DateTime.Now.ToString("dd-MM-yyyy");
        //                            callinteraction.callMadeDateAndTime = DateTime.Now;
        //                            callinteraction.callTime = DateTime.Now.ToString("HH:mm:ss");
        //                            callinteraction.dealerCode = Session["DealerCode"].ToString();
        //                            callinteraction.dissatPSFintId = 0;
        //                            callinteraction.chasserCall = false;
        //                            if (Session["isCallInitiated"] != null)
        //                            {
        //                                callinteraction.isCallinitaited = "initiated";
        //                                callinteraction.makeCallFrom = "PSF";

        //                                if (Session["AndroidUniqueId"] != null)
        //                                {
        //                                    callinteraction.uniqueidForCallSync = int.Parse(Session["AndroidUniqueId"].ToString());
        //                                }
        //                                else if (Session["GSMUniqueId"] != null)
        //                                {
        //                                    callinteraction.uniqueIdGSM = Session["GSMUniqueId"].ToString().Split(';')[0];
        //                                }
        //                            }
        //                            callinteraction.callStatus = Convert.ToBoolean(Session["NCReason"]);
        //                            if (Session["DialedNumber"] != null)
        //                            {
        //                                callinteraction.dailedNoIs = Session["DialedNumber"].ToString();
        //                            }
        //                            callinteraction.psfAssignedInteraction_id = psfAss_id;
        //                            callinteraction.customer_id = cusId;
        //                            callinteraction.vehicle_vehicle_id = vehiId;
        //                            callinteraction.wyzUser_id = userId;
        //                            callinteraction.agentName = Session["UserName"].ToString();
        //                            callinteraction.campaign_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).campaign_id;
        //                            callinteraction.service_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).service_id ?? default(long);
        //                            db.callinteractions.Add(callinteraction);
        //                            db.SaveChanges();


        //                            if (psfinter.modeOfService != null && psfinter.modeOfService == "Dissatisfied with PSF")
        //                            {
        //                                long maxPrvRewordId = 0, complaintMgr_id = 0;

        //                                if (db.reworks.Any(m => m.vehicle_id == vehiId && m.customer_id == cusId))
        //                                {
        //                                    maxPrvRewordId = db.reworks.Where(m => m.vehicle_id == vehiId && m.customer_id == cusId).Max(m => m.id);
        //                                    complaintMgr_id = db.reworks.FirstOrDefault(m => m.id == maxPrvRewordId).complaint_creid;
        //                                }

        //                                psfinter.complaintMgr_id = complaintMgr_id;
        //                                rework rework = new rework();
        //                                wyzuser user = db.wyzusers.FirstOrDefault(m => m.id == userId);
        //                                rework.Benefits = "No Benefits Applied";
        //                                rework.DissatStatus_id = 44;
        //                                rework.complaint_creid = complaintMgr_id;
        //                                rework.campaign_id = long.Parse(Session["psfDayType"].ToString());
        //                                rework.customer_id = cusId;
        //                                rework.vehicle_id = vehiId;
        //                                rework.discount = 0;
        //                                rework.isReworkAvailable = false;
        //                                rework.issuedate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));
        //                                rework.location_id = user.location_cityId ?? default(long);
        //                                rework.psfassignedInteraction_id = psfAss_id;
        //                                rework.workshop_id = user.workshop_id ?? default(long);
        //                                db.reworks.Add(rework);
        //                                db.SaveChanges();
        //                                psfinter.isComplaintRaised = "Yes";
        //                            }


        //                            for (int i = 0; i < listingFormData.remarksList.Count; i++)
        //                            {
        //                                if (psfinter.creRemarks == null && listingFormData.commentsList[i] != "")
        //                                {
        //                                    psfinter.creRemarks = listingFormData.commentsList[i];
        //                                }

        //                                if (psfinter.customerFeedBack == null && listingFormData.remarksList[i] != "")
        //                                {
        //                                    psfinter.customerFeedBack = listingFormData.remarksList[i];
        //                                }
        //                            }

        //                            if (Session["GSMUniqueId"] != null)
        //                            {
        //                                gsmsynchdata gsm = new gsmsynchdata();
        //                                gsm.Callinteraction_id = callinteraction.id;
        //                                gsm.CallMadeDateTime = DateTime.Now;
        //                                gsm.UniqueGsmId = Session["GSMUniqueId"].ToString().Split(';')[0];
        //                                gsm.TenantUrl = Session["GSMUniqueId"].ToString().Split(';')[1];

        //                                db.gsmsynchdata.Add(gsm);
        //                                db.SaveChanges();
        //                            }
        //                            psfinter.callInteraction_id = callinteraction.id;
        //                            psfinter.callinteraction = callinteraction;
        //                            psfinter.callDispositionData_id = currentDispo;
        //                            db.indusPSFInteraction.Add(psfinter);
        //                            db.SaveChanges();
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    TempData["SubmissionResult"] = "No Assignment found";
        //                    return RedirectToAction("ReturnToBucket", new { @id = 6, psfDay = 6 });
        //                }

        //                submissionResult = "True";
        //                dbTras.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                dbTras.Rollback();
        //                if (ex.InnerException != null)
        //                {
        //                    if (ex.InnerException.InnerException != null)
        //                    {
        //                        submissionResult = ex.InnerException.InnerException.Message;
        //                    }
        //                    else
        //                    {
        //                        submissionResult = ex.InnerException.Message;
        //                    }
        //                }
        //                else
        //                {
        //                    submissionResult = ex.Message;
        //                }
        //            }
        //        }
        //    }
        //    //}
        //    TempData["SubmissionResult"] = submissionResult;
        //    return RedirectToAction("ReturnToBucket", new { @id = 6, psfDay = 6 });
        //}

        //[HttpPost]
        //public ActionResult psfComplaintPost(CallLoggingViewModel callLog)
        //{
        //    string submissionResult = "False";
        //    long cusId = Convert.ToInt32(Session["CusId"].ToString());
        //    long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
        //    long userId = Convert.ToInt32(Session["UserId"].ToString());
        //    long interactionId = Convert.ToInt32(Session["interactionid"].ToString());

        //    IndusPSFInteraction psfinter = callLog.indusPsfInteraction;
        //    callinteraction callinteraction = new callinteraction();
        //    ListingForm listingFormData = callLog.listingForm;
        //    rework rework = callLog.rework;
        //    long psfAssgn_id = 0, curDispoId = 0;
        //    using (var db = new AutoSherDBContext())
        //    {
        //        using (DbContextTransaction dbTrans = db.Database.BeginTransaction())
        //        {
        //            //dbTrans.UnderlyingTransaction.IsolationLevel
        //            try
        //            {

        //                long reworkMaxId = 0, preCompManagerId = 0;
        //                if (db.reworks.Any(m => m.customer_id == cusId))
        //                {
        //                    reworkMaxId = db.reworks.Where(m => m.customer_id == cusId).Max(m => m.id);
        //                    preCompManagerId = db.reworks.FirstOrDefault(m => m.id == reworkMaxId).complaint_creid;
        //                }

        //                if (callLog.indusPsfInteraction.isContacted == "No")
        //                {
        //                    curDispoId = db.calldispositiondatas.SingleOrDefault(m => m.disposition == psfinter.PSFDisposition).dispositionId;
        //                    psfAssgn_id = recordPSFDisposition(2, db, Convert.ToInt32(curDispoId), interactionId);

        //                    callinteraction.callCount = 1;
        //                    callinteraction.isComplaint = true;
        //                    callinteraction.callDate = DateTime.Now.ToString("dd-MM-yyyy");
        //                    callinteraction.callMadeDateAndTime = DateTime.Now;
        //                    callinteraction.callTime = DateTime.Now.ToString("HH:mm:ss");
        //                    callinteraction.dealerCode = Session["DealerCode"].ToString();
        //                    callinteraction.dissatPSFintId = 0;
        //                    if (Session["isCallInitiated"] != null)
        //                    {
        //                        callinteraction.isCallinitaited = "initiated";
        //                        callinteraction.makeCallFrom = "PSF";
        //                        if (Session["AndroidUniqueId"] != null)
        //                        {
        //                            callinteraction.uniqueidForCallSync = int.Parse(Session["AndroidUniqueId"].ToString());
        //                        }
        //                        else if (Session["GSMUniqueId"] != null)
        //                        {
        //                            callinteraction.uniqueIdGSM = Session["GSMUniqueId"].ToString().Split(';')[0];
        //                        }
        //                    }
        //                    callinteraction.callStatus = Convert.ToBoolean(Session["NCReason"]);
        //                    if (Session["DialedNumber"] != null)
        //                    {
        //                        callinteraction.dailedNoIs = Session["DialedNumber"].ToString();
        //                    }
        //                    callinteraction.psfAssignedInteraction_id = psfAssgn_id;
        //                    callinteraction.customer_id = cusId;
        //                    callinteraction.vehicle_vehicle_id = vehiId;
        //                    callinteraction.wyzUser_id = userId;
        //                    callinteraction.chasserCall = false;
        //                    callinteraction.agentName = Session["UserName"].ToString();
        //                    callinteraction.campaign_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAssgn_id).campaign_id;
        //                    callinteraction.service_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAssgn_id).service_id ?? default(long);
        //                    db.callinteractions.Add(callinteraction);
        //                    db.SaveChanges();

        //                    if (Session["GSMUniqueId"] != null)
        //                    {
        //                        gsmsynchdata gsm = new gsmsynchdata();
        //                        gsm.Callinteraction_id = callinteraction.id;
        //                        gsm.CallMadeDateTime = DateTime.Now;
        //                        gsm.UniqueGsmId = Session["GSMUniqueId"].ToString().Split(';')[0];
        //                        gsm.TenantUrl = Session["GSMUniqueId"].ToString().Split(';')[1];

        //                        db.gsmsynchdata.Add(gsm);
        //                        db.SaveChanges();
        //                    }

        //                    psfinter.callInteraction_id = callinteraction.id;
        //                    psfinter.callDispositionData_id = curDispoId;

        //                    for (int i = 0; i < listingFormData.remarksList.Count; i++)
        //                    {
        //                        if (psfinter.creRemarks == null && listingFormData.commentsList[i] != "")
        //                        {
        //                            psfinter.creRemarks = listingFormData.commentsList[i];
        //                        }

        //                        if (psfinter.customerFeedBack == null && listingFormData.remarksList[i] != "")
        //                        {
        //                            psfinter.customerFeedBack = listingFormData.remarksList[i];
        //                        }
        //                    }

        //                    db.indusPSFInteraction.Add(psfinter);
        //                    db.SaveChanges();

        //                    if (Session["isPSFRM"] != null && (bool)Session["isPSFRM"] == true)
        //                    {
        //                        rework = db.reworks.FirstOrDefault(m => m.id == reworkMaxId);
        //                        rework.rmAttempts += rework.rmAttempts;
        //                        db.reworks.AddOrUpdate(rework);
        //                        db.SaveChanges();
        //                    }
        //                }
        //                else if (callLog.indusPsfInteraction.isContacted == "Yes")
        //                {
        //                    if (psfinter.whatCustSaid == "FollowUp Later")
        //                    {
        //                        curDispoId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Call Me Later").id;
        //                        psfAssgn_id = recordPSFDisposition(2, db, Convert.ToInt32(curDispoId), interactionId);
        //                        //if (rework.RMResolutionStatus != null && rework.RMResolutionStatus != "")
        //                        //{
        //                        //    rework = db.reworks.FirstOrDefault(m => m.id == reworkMaxId);
        //                        //}
        //                    }
        //                    else if (rework.resolutionMode != null && rework.resolutionMode != "")
        //                    {
        //                        if (rework.resolutionMode == "Complaint not valid/Customer educated")
        //                        {
        //                            curDispoId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Resolved").id;
        //                            psfAssgn_id = recordPSFDisposition(2, db, Convert.ToInt32(curDispoId), interactionId);

        //                            rework = db.reworks.FirstOrDefault(m => m.id == reworkMaxId);
        //                            rework.id = reworkMaxId;
        //                            rework.DissatStatus_id = curDispoId;
        //                            rework.reworkAddress = "";
        //                            rework.reworkStatus_id = curDispoId;
        //                            rework.resolutionDateAndTime = DateTime.Now;
        //                            rework.psfassignedInteraction_id = psfAssgn_id;
        //                            db.reworks.AddOrUpdate(rework);
        //                            db.SaveChanges();

        //                        }
        //                        else if (rework.resolutionMode == "Schedule a re-visit")
        //                        {
        //                            curDispoId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Rework").id;
        //                            psfAssgn_id = recordPSFDisposition(2, db, Convert.ToInt32(curDispoId), interactionId);
        //                            bool isNewInsert = false;
        //                            rework existingRework = db.reworks.SingleOrDefault(m => m.id == reworkMaxId);

        //                            if (existingRework.reworkStatus_id != 0)
        //                            {

        //                                rework.Benefits = "No Benefits Applied";
        //                                rework.complaint_creid = existingRework.complaint_creid;
        //                                rework.campaign_id = existingRework.campaign_id;
        //                                rework.customer_id = existingRework.customer_id;
        //                                rework.DissatStatus_id = existingRework.DissatStatus_id;
        //                                rework.issuedate = existingRework.issuedate;
        //                                rework.vehicle_id = existingRework.vehicle_id;
        //                                rework.psfassignedInteraction_id = psfAssgn_id;

        //                                rework.reworkStatus_id = curDispoId;
        //                                rework.resolutionDateAndTime = DateTime.Now;
        //                                if (rework.reworkMode == "Self")
        //                                {
        //                                    rework.reworkAddress = "";
        //                                }
        //                                db.reworks.Add(rework);
        //                                db.SaveChanges();

        //                                existingRework.reworkStatus_id = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Cancelled").id;
        //                                db.reworks.AddOrUpdate(existingRework);
        //                                db.SaveChanges();
        //                            }
        //                            else
        //                            {
        //                                existingRework.reworkStatus_id = curDispoId;
        //                                //existingRework.resolutionDateAndTime = DateTime.Now;
        //                                existingRework.reworkMode = rework.reworkMode;
        //                                if (existingRework.reworkMode == "Self")
        //                                {
        //                                    existingRework.reworkAddress = "";
        //                                }
        //                                existingRework.psfassignedInteraction_id = psfAssgn_id;
        //                                existingRework.workshop_id = rework.workshop_id;
        //                                existingRework.reworkDateAndTime = rework.reworkDateAndTime;
        //                                existingRework.resolutionMode = "Schedule a re-visit";
        //                                db.reworks.AddOrUpdate(existingRework);
        //                                db.SaveChanges();
        //                            }
        //                        }
        //                        else if (rework.resolutionMode == "Resolved")
        //                        {
        //                            curDispoId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Resolved").id;
        //                            psfAssgn_id = recordPSFDisposition(2, db, Convert.ToInt32(curDispoId), interactionId);

        //                            rework existingRework = db.reworks.FirstOrDefault(m => m.id == reworkMaxId);

        //                            existingRework.DissatStatus_id = curDispoId;
        //                            existingRework.reworkStatus_id = curDispoId;
        //                            existingRework.resolutionDateAndTime = DateTime.Now;
        //                            existingRework.resolvedOn = rework.resolvedOn;
        //                            existingRework.attendedBy = rework.attendedBy;
        //                            existingRework.resolvedBy = rework.resolvedBy;
        //                            existingRework.natureOfComplaint = rework.natureOfComplaint;
        //                            existingRework.psfassignedInteraction_id = psfAssgn_id;
        //                            existingRework.discount = rework.discount;
        //                            existingRework.resolutionMode = "Resolved";


        //                            db.reworks.AddOrUpdate(existingRework);
        //                            db.SaveChanges();

        //                        }
        //                        else if (rework.resolutionMode == "Escalate to RM")
        //                        {
        //                            curDispoId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Dissatisfied with PSF").id;

        //                            psfAssgn_id = recordPSFDisposition(2, db, Convert.ToInt32(curDispoId), interactionId);

        //                            string voc = rework.VOC;
        //                            long rm_creid = rework.rm_creid;
        //                            rework = db.reworks.FirstOrDefault(m => m.id == reworkMaxId);
        //                            rework.VOC = voc;
        //                            rework.rm_creid = rm_creid;
        //                            rework.isRMComplaintRaised = true;
        //                            rework.resolutionMode = "Escalate to RM";

        //                            db.reworks.AddOrUpdate(rework);
        //                            db.SaveChanges();
        //                        }
        //                    }
        //                    else if (rework.RMResolutionStatus != null && rework.RMResolutionStatus != "")
        //                    {
        //                        string rmRemarks = rework.RMRemarks;
        //                        if (rework.RMResolutionStatus == "Resolved")
        //                        {
        //                            curDispoId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Resolved").id;
        //                            psfAssgn_id = recordPSFDisposition(2, db, Convert.ToInt32(curDispoId), interactionId);


        //                            rework = db.reworks.FirstOrDefault(m => m.id == reworkMaxId);
        //                            rework.DissatStatus_id = curDispoId;
        //                            rework.reworkStatus_id = curDispoId;
        //                            rework.resolutionDateAndTime = DateTime.Now;
        //                            rework.psfassignedInteraction_id = psfAssgn_id;
        //                            rework.RMRemarks = rmRemarks;
        //                            rework.RMResolutionStatus = "Resolved";

        //                        }
        //                        else if (rework.RMResolutionStatus == "Pending")
        //                        {
        //                            curDispoId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Pending").id;
        //                            psfAssgn_id = recordPSFDisposition(2, db, Convert.ToInt32(curDispoId), interactionId);

        //                            rework = db.reworks.FirstOrDefault(m => m.id == reworkMaxId);
        //                            rework.reworkStatus_id = curDispoId;
        //                            rework.resolutionDateAndTime = DateTime.Now;
        //                            rework.psfassignedInteraction_id = psfAssgn_id;
        //                            rework.RMRemarks = rmRemarks;
        //                            rework.RMResolutionStatus = "Pending";
        //                        }
        //                        else if (rework.RMResolutionStatus == "Closed")
        //                        {
        //                            curDispoId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Closed").id;
        //                            psfAssgn_id = recordPSFDisposition(2, db, Convert.ToInt32(curDispoId), interactionId);

        //                            rework = db.reworks.FirstOrDefault(m => m.id == reworkMaxId);
        //                            rework.DissatStatus_id = curDispoId;
        //                            rework.reworkStatus_id = curDispoId;
        //                            rework.resolutionDateAndTime = DateTime.Now;
        //                            rework.psfassignedInteraction_id = psfAssgn_id;
        //                            rework.RMRemarks = rmRemarks;
        //                            rework.RMResolutionStatus = "Closed";
        //                        }
        //                        db.reworks.AddOrUpdate(rework);
        //                        db.SaveChanges();
        //                    }


        //                    if (Session["isPSFRM"] != null && (bool)Session["isPSFRM"] == true)
        //                    {
        //                        rework = db.reworks.FirstOrDefault(m => m.id == reworkMaxId);
        //                        rework.rmAttempts += rework.rmAttempts;
        //                        db.reworks.AddOrUpdate(rework);
        //                        db.SaveChanges();
        //                    }

        //                    callinteraction.isComplaint = true;
        //                    callinteraction.callCount = 1;
        //                    callinteraction.callDate = DateTime.Now.ToString("dd-MM-yyyy");
        //                    callinteraction.callMadeDateAndTime = DateTime.Now;
        //                    callinteraction.callTime = DateTime.Now.ToString("HH:mm:ss");
        //                    callinteraction.dealerCode = Session["DealerCode"].ToString();
        //                    callinteraction.dissatPSFintId = 0;
        //                    if (Session["isCallInitiated"] != null)
        //                    {
        //                        callinteraction.isCallinitaited = "initiated";
        //                        callinteraction.makeCallFrom = "PSF";

        //                        if (Session["AndroidUniqueId"] != null)
        //                        {
        //                            callinteraction.uniqueidForCallSync = int.Parse(Session["AndroidUniqueId"].ToString());
        //                        }
        //                        else if (Session["GSMUniqueId"] != null)
        //                        {
        //                            callinteraction.uniqueIdGSM = Session["GSMUniqueId"].ToString().Split(';')[0];
        //                        }
        //                    }
        //                    callinteraction.callStatus = Convert.ToBoolean(Session["NCReason"]);
        //                    if (Session["DialedNumber"] != null)
        //                    {
        //                        callinteraction.dailedNoIs = Session["DialedNumber"].ToString();
        //                    }
        //                    callinteraction.psfAssignedInteraction_id = psfAssgn_id;
        //                    callinteraction.customer_id = cusId;
        //                    callinteraction.vehicle_vehicle_id = vehiId;
        //                    callinteraction.wyzUser_id = userId;
        //                    callinteraction.agentName = Session["UserName"].ToString();
        //                    callinteraction.chasserCall = false;
        //                    callinteraction.campaign_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAssgn_id).campaign_id;
        //                    callinteraction.service_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAssgn_id).service_id ?? default(long);
        //                    db.callinteractions.Add(callinteraction);
        //                    db.SaveChanges();
        //                    for (int i = 0; i < listingFormData.remarksList.Count; i++)
        //                    {
        //                        if (psfinter.creRemarks == null && listingFormData.commentsList[i] != "")
        //                        {
        //                            psfinter.creRemarks = listingFormData.commentsList[i];
        //                        }

        //                        if (psfinter.customerFeedBack == null && listingFormData.remarksList[i] != "")
        //                        {
        //                            psfinter.customerFeedBack = listingFormData.remarksList[i];
        //                        }
        //                    }

        //                    if (Session["GSMUniqueId"] != null)
        //                    {
        //                        gsmsynchdata gsm = new gsmsynchdata();
        //                        gsm.Callinteraction_id = callinteraction.id;
        //                        gsm.CallMadeDateTime = DateTime.Now;
        //                        gsm.UniqueGsmId = Session["GSMUniqueId"].ToString().Split(';')[0];
        //                        gsm.TenantUrl = Session["GSMUniqueId"].ToString().Split(';')[1];

        //                        db.gsmsynchdata.Add(gsm);
        //                        db.SaveChanges();
        //                    }
        //                    psfinter.callInteraction_id = callinteraction.id;
        //                    psfinter.callinteraction = callinteraction;
        //                    psfinter.callDispositionData_id = curDispoId;
        //                    db.indusPSFInteraction.Add(psfinter);
        //                    db.SaveChanges();

        //                    long upselCount = 0;
        //                    if (listingFormData.LeadYes == "Capture Lead Yes")
        //                    {
        //                        foreach (var upsel in listingFormData.upsellleads)
        //                        {
        //                            if (upsel.taggedTo != null)
        //                            {
        //                                upsel.vehicle_vehicle_id = vehiId;
        //                                //upsel.induspsfinteraction_id = psfinter.Id;
        //                                //upsel.srDisposition_id = sr_disposition.id;
        //                                db.upsellleads.Add(upsel);
        //                                db.SaveChanges();
        //                                upselCount++;
        //                            }
        //                        }
        //                    }


        //                    psfinter.upsellCount = upselCount;
        //                    db.SaveChanges();
        //                    submissionResult = "True";

        //                }
        //                dbTrans.Commit();
        //                submissionResult = "True";
        //            }

        //            catch (Exception ex)
        //            {
        //                dbTrans.Rollback();

        //                if (ex.InnerException != null)
        //                {
        //                    if (ex.InnerException.InnerException != null)
        //                    {
        //                        submissionResult = ex.InnerException.InnerException.Message;
        //                    }
        //                    else
        //                    {
        //                        submissionResult = ex.InnerException.Message;
        //                    }
        //                }
        //                else
        //                {
        //                    submissionResult = ex.Message;
        //                }

        //                //if (ex.Message.Contains("inner except"))
        //                //{
        //                //    submissionResult = ex.InnerException.Message;
        //                //}
        //                //submissionResult = ex.Message;
        //            }
        //        }

        //    }

        //    TempData["SubmissionResult"] = submissionResult;
        //    if (Session["isPSFRM"] != null && Convert.ToBoolean(Session["isPSFRM"]) == true)
        //    {
        //        return RedirectToAction("ReturnToBucket", new { @id = 500 });
        //    }
        //    else
        //    {
        //        return RedirectToAction("ReturnToBucket", new { @id = 900 });

        //    }
        //}
        #endregion

        public string getLatestServiceBookedType(long cid, long vehicle_id, service service)
        {
            try
            {

                using (var db = new AutoSherDBContext())
                {
                    string modeType = "";
                    long countOfServiceBooked = db.servicebookeds.Where(m => m.customer != null && m.vehicle != null && m.customer.id == cid && m.vehicle.vehicle_id == vehicle_id).Count();

                    if (countOfServiceBooked != 0)
                    {

                        long service_booked = db.servicebookeds.Where(m => m.customer != null && m.vehicle != null && m.customer.id == cid && m.vehicle.vehicle_id == vehicle_id).Max(m => m.serviceBookedId);
                        servicebooked service_bookedData = db.servicebookeds.FirstOrDefault(m => m.serviceBookedId == service_booked);

                        if (service_bookedData.typeOfPickup == "true")
                        {
                            modeType = "Pick-Up";
                            return modeType;
                        }
                        else if (service_bookedData.typeOfPickup == "Customer Drive-In")
                        {
                            modeType = "Self Drive-in";
                            return modeType;

                        }
                        else if (service_bookedData.typeOfPickup == "Maruthi Mobile Support")
                        {

                            modeType = "MMS";
                            return modeType;

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return "";
        }

        public psfassignedinteraction getLastPSFAssignStatusOfVehicle(int vehi_id)
        {
            psfassignedinteraction psf = new psfassignedinteraction();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    long maxId = 0;
                    if (db.psfassignedinteractions.Any(m => m.vehicle_vehicle_id == vehi_id))
                    {
                        maxId = db.psfassignedinteractions.Where(m => m.vehicle_vehicle_id == vehi_id).Max(m => m.id);
                        psf = db.psfassignedinteractions.Include("campaign").Include("service").Include("wyzuser").FirstOrDefault(m => m.id == maxId);
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return psf;
        }

        public psfassignedinteraction getLastPSFAssignOfVehicle(int vehi_id)
        {
            psfassignedinteraction lastPSFAssigned = new psfassignedinteraction();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    long psfStatusExist = db.psfassignedinteractions.Where(m => m.vehicle != null && m.vehicle.vehicle_id == vehi_id && m.service != null).Count();

                    if (psfStatusExist > 0)
                    {

                        long idIs = db.psfassignedinteractions.Where(m => m.vehicle != null && m.vehicle.vehicle_id == vehi_id && m.service != null).Max(m => m.id);

                        lastPSFAssigned = db.psfassignedinteractions.Include("service").Include("campaign").FirstOrDefault(m => m.id == idIs);
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return lastPSFAssigned;
        }

        public List<smstemplate> getMessageTemplatesForDispoType(string typeDispo)
        {
            List<smstemplate> filterdSMS = new List<smstemplate>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string complainttype = SMSTemplateCode.COMPLIANT;
                    string servicebookedtype = SMSTemplateCode.SERVICE_BOOKED;
                    string serviceduetype = SMSTemplateCode.SERVICE_DUE;
                    string noncontacttype = SMSTemplateCode.NONCONTACT;
                    string drivertype = SMSTemplateCode.DRIVER;

                    List<smstemplate> listSMS = new List<smstemplate>();
                    if (typeDispo == "insurance" || typeDispo == "insuranceSearch")
                    {
                        listSMS = db.smstemplates.Where(m => m.moduletype == 2 || m.moduletype == 3 && m.deliveryType == "Auto").ToList();
                    }
                    else if (typeDispo == "PSF")
                    {
                        listSMS = db.smstemplates.Where(m => m.moduletype == 4 || m.deliveryType == "Auto").ToList();

                    }
                    else
                    {
                        listSMS = db.smstemplates.Where(m => m.moduletype == 1 || m.moduletype == 3 && m.deliveryType == "Auto").ToList();
                    }
                    filterdSMS = listSMS.Where(m => m.smsType != null && m.smsType == complainttype && m.smsType == servicebookedtype && m.smsType == serviceduetype && m.smsType == noncontacttype && m.smsType == drivertype && m.inActive == false).ToList();

                    return filterdSMS;
                }
            }
            catch (Exception ex)
            {
                return filterdSMS;
            }
            //return filterdSMS;
        }


        #region AutoSMS
        /// <summary>
        /// This function for sending sms on disposition for service,insurance and psf, then call scheduler for asych
        /// </summary>
        /// <param name="wyzId">Wyzuser id for procedure</param>
        /// <param name="vehicleId"></param>
        /// <param name="custId"></param>
        /// <param name="smsType">smsteplate name</param>
        /// <param name="dispoType">disposition type</param>
        /// <param name="taggingid">if tagging user then that id</param>
        /// <param name="driverId">if smr->driverId, if ins->insAgentId, if psf->saId</param>
        /// <param name="departmentId"></param>
        /// <param name="customMsg">custom message if required</param>
        /// <param name="workshopId"></param>
        public void autosmsday(long wyzId, long vehicleId, long custId, string smsType, string dispoType, int taggingid, long driverId, long departmentId, string customMsg, long workshopId, string phoneNumbers = null, string DealerCode = null)
        {

            IScheduler autosmsScheduler = StdSchedulerFactory.GetDefaultScheduler().Result;

            autosmsScheduler.Start();
            IJobDetail jobDetail = JobBuilder.Create<AutoSMSJob>().Build();

            string trigername = "DispoAutoSMSTrigger" + DateTime.Now.Millisecond + vehicleId;
            string trigerGroupName = "DispoAutoSMSGroup" + DateTime.Now.Millisecond + vehicleId;

            ITrigger trigger = TriggerBuilder.Create().WithIdentity(trigername, trigerGroupName).StartNow().WithSimpleSchedule().Build();

            jobDetail.JobDataMap["WyzId"] = wyzId;
            jobDetail.JobDataMap["vehicleId"] = vehicleId;
            jobDetail.JobDataMap["custId"] = custId;
            jobDetail.JobDataMap["dispoType"] = dispoType;
            jobDetail.JobDataMap["smsType"] = smsType;
            jobDetail.JobDataMap["taggingid"] = taggingid;
            jobDetail.JobDataMap["driverId"] = driverId;
            jobDetail.JobDataMap["departmentId"] = departmentId;
            jobDetail.JobDataMap["customMsg"] = customMsg;
            jobDetail.JobDataMap["workshopId"] = workshopId;
            jobDetail.JobDataMap["phoneNumbers"] = phoneNumbers;
            if (string.IsNullOrEmpty(DealerCode))
            {
                jobDetail.JobDataMap["DealerCode"] = Session["DealerCode"].ToString();

            }
            else
            {
                jobDetail.JobDataMap["DealerCode"] = DealerCode;

            }

            //autosmsScheduler.Context.Put("WyzId", wyzId);
            //autosmsScheduler.Context.Put("vehicleId", vehicleId);
            //autosmsScheduler.Context.Put("custId", custId);
            //autosmsScheduler.Context.Put("smsType", smsType);
            //autosmsScheduler.Context.Put("dispoType", dispoType);
            //autosmsScheduler.Context.Put("taggingid", taggingid);
            //autosmsScheduler.Context.Put("driverId", driverId);
            //autosmsScheduler.Context.Put("departmentId", departmentId);
            //autosmsScheduler.Context.Put("customMsg", customMsg);
            //autosmsScheduler.Context.Put("workshopId", workshopId);
            //autosmsScheduler.Context.Put("phoneNumbers", phoneNumbers);
            //if (string.IsNullOrEmpty(DealerCode))
            //{
            //    autosmsScheduler.Context.Put("DealerCode", Session["DealerCode"].ToString());
            //}
            //else
            //{
            //    autosmsScheduler.Context.Put("DealerCode", DealerCode);
            //}

            autosmsScheduler.ScheduleJob(jobDetail, trigger);

        }

        #endregion

        #region Kataria Auto SMS
        public void autosmsKataria(long wyzId, long vehicleId, long custId, long dispositionId, string notRequiredReason, string innerRequiredReason, int moduleType, int taggingid, long driverId, long departmentId, string customMsg, long workshopId, string phoneNumbers = null, string DealerCode = null)
        {

            IScheduler autosmsScheduler = StdSchedulerFactory.GetDefaultScheduler().Result;

            autosmsScheduler.Start();
            IJobDetail jobDetail = JobBuilder.Create<KatariaAutoSMS>().Build();

            string trigername = "DispoAutoSMSKatariaTrigger" + DateTime.Now.Millisecond + vehicleId;
            string trigerGroupName = "DispoAutoSMSKatriaGroup" + DateTime.Now.Millisecond + vehicleId;

            ITrigger trigger = TriggerBuilder.Create().WithIdentity(trigername, trigerGroupName).StartNow().WithSimpleSchedule().Build();

            jobDetail.JobDataMap["WyzId"] = wyzId;
            jobDetail.JobDataMap["vehicleId"] = vehicleId;
            jobDetail.JobDataMap["custId"] = custId;
            jobDetail.JobDataMap["moduleType"] = moduleType;
            jobDetail.JobDataMap["dispositionId"] = dispositionId;
            jobDetail.JobDataMap["taggingid"] = taggingid;
            jobDetail.JobDataMap["driverId"] = driverId;
            jobDetail.JobDataMap["departmentId"] = departmentId;
            jobDetail.JobDataMap["customMsg"] = customMsg;
            jobDetail.JobDataMap["workshopId"] = workshopId;
            jobDetail.JobDataMap["phoneNumbers"] = phoneNumbers;
            jobDetail.JobDataMap["notRequiredReason"] = notRequiredReason;
            jobDetail.JobDataMap["innerRequiredReason"] = innerRequiredReason;
            if (string.IsNullOrEmpty(DealerCode))
            {
                jobDetail.JobDataMap["DealerCode"] = Session["DealerCode"].ToString();
            }
            else
            {
                jobDetail.JobDataMap["DealerCode"] = DealerCode;
            }

            autosmsScheduler.ScheduleJob(jobDetail, trigger);

        }

        #endregion

        #region autoEmail

        public void autoEmailDay(long cusID, long userId, long vehID, string emailType, string fromEmailId, string password, string toEmailId, string DealerCode, string toCCemail, string emailSubject, string emailBody)
        {
            //IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;

            IScheduler autoEmailScheduler = StdSchedulerFactory.GetDefaultScheduler().Result;

            autoEmailScheduler.Start();
            IJobDetail jobDetail = JobBuilder.Create<AutoEmail>().Build();


            string trigername = "DispoAutoEmailTrigger" + DateTime.Now.Millisecond + vehID;
            string trigerGroupName = "DispoAutoEmailGroup" + DateTime.Now.Millisecond + vehID;

            ITrigger trigger = TriggerBuilder.Create().WithIdentity(trigername, trigerGroupName)
                .StartNow().WithSimpleSchedule().Build();


            //autoEmailScheduler.Context.Put("WyzId", userId);
            //autoEmailScheduler.Context.Put("vehicleId", vehID);
            //autoEmailScheduler.Context.Put("custId", cusID);
            //autoEmailScheduler.Context.Put("emailType", emailType);
            //autoEmailScheduler.Context.Put("fromEmailId", fromEmailId);
            //autoEmailScheduler.Context.Put("password", password);
            //autoEmailScheduler.Context.Put("toEmailId", toEmailId);


            //if (string.IsNullOrEmpty(DealerCode))
            //{
            //    autoEmailScheduler.Context.Put("DealerCode", Session["DealerCode"].ToString());
            //}
            //else
            //{
            //    autoEmailScheduler.Context.Put("DealerCode", DealerCode);
            //}

            jobDetail.JobDataMap["WyzId"] = userId;
            jobDetail.JobDataMap["vehicleId"] = vehID;
            jobDetail.JobDataMap["custId"] = cusID;
            jobDetail.JobDataMap["emailType"] = emailType;
            jobDetail.JobDataMap["fromEmailId"] = fromEmailId;
            jobDetail.JobDataMap["password"] = password;
            jobDetail.JobDataMap["toEmailId"] = toEmailId;
            jobDetail.JobDataMap["toCCemail"] = toCCemail;
            jobDetail.JobDataMap["toemailSubject"] = emailSubject;
            jobDetail.JobDataMap["toemailBody"] = emailBody;

            if (string.IsNullOrEmpty(DealerCode))
            {
                jobDetail.JobDataMap["DealerCode"] = Session["DealerCode"].ToString();
            }
            else
            {
                jobDetail.JobDataMap["DealerCode"] = DealerCode;
            }
            autoEmailScheduler.ScheduleJob(jobDetail, trigger);
        }

        #region KatariaAutoEmail
        public void autoKatariaEmailDay(long cusID, long userId, long vehID, long calldispositionid, string notrequiredreason, string innerRequiredReason, string DealerCode, string fromEmail, int role)
        {
            IScheduler autoEmailScheduler = StdSchedulerFactory.GetDefaultScheduler().Result;

            autoEmailScheduler.Start();
            IJobDetail jobDetail = JobBuilder.Create<KatariaAutoEmail>().Build();


            string trigername = "DispoKatariaAutoEmailTrigger" + calldispositionid + DateTime.Now.Millisecond + vehID;
            string trigerGroupName = "DispoKatariaAutoEmailGroup" + calldispositionid + DateTime.Now.Millisecond + vehID;

            ITrigger trigger = TriggerBuilder.Create().WithIdentity(trigername, trigerGroupName)
                .StartNow().WithSimpleSchedule().Build();

            jobDetail.JobDataMap["WyzId"] = userId;
            jobDetail.JobDataMap["vehicleId"] = vehID;
            jobDetail.JobDataMap["custId"] = cusID;
            jobDetail.JobDataMap["calldispositionid"] = calldispositionid;
            jobDetail.JobDataMap["notrequiredreason"] = notrequiredreason;
            jobDetail.JobDataMap["innerRequiredReason"] = innerRequiredReason;
            jobDetail.JobDataMap["fromEmail"] = fromEmail;
            jobDetail.JobDataMap["role"] = role;

            if (string.IsNullOrEmpty(DealerCode))
            {
                jobDetail.JobDataMap["DealerCode"] = Session["DealerCode"].ToString();
            }
            else
            {
                jobDetail.JobDataMap["DealerCode"] = DealerCode;
            }
            autoEmailScheduler.ScheduleJob(jobDetail, trigger);
        }

        #endregion
        public ActionResult loadEmail(int moduleType, string dataFor)
        {
            int wyzUserId = Convert.ToInt32(Session["UserId"].ToString());
            List<emailcredential> emailList = new List<emailcredential>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (dataFor == "Dissat")
                    {
                        emailList = db.emailcredentials.Where(m => (m.moduleType == 3 || m.moduleType == moduleType) && (m.wyzuser_id == wyzUserId || m.common == true) && m.isinternal == true).ToList();
                    }
                    else
                    {
                        emailList = db.emailcredentials.Where(m => (m.moduleType == 3 || m.moduleType == moduleType) && (m.wyzuser_id == wyzUserId || m.common == true)).ToList();
                    }

                    for (int i = 0; i < emailList.Count(); i++)
                    {
                        if (string.IsNullOrEmpty(emailList[i].userPassword))
                        {
                            emailList[i].userPassword = "No";
                        }
                        else
                        {
                            emailList[i].userPassword = "Yes";
                        }
                    }

                    return Json(new { success = true, emails = emailList.Select(m => new { emailId = m.userEmail, password = m.userPassword }).ToList() });
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        return Json(new { success = true, error = ex.InnerException.InnerException.Message });
                    }
                    else
                    {
                        return Json(new { success = true, error = ex.InnerException.Message });
                    }
                }
                else
                {
                    return Json(new { success = true, error = ex.Message });
                }
            }
        }

        public string getEmailBodyAndSub(string wyzUserID_var, string vehID_var, string locID_var, string smsID_var,
                                         string insID_var, long docID_var, string procedureType_var)
        {
            string returnString = string.Empty;
            try
            {
                using (AutoSherDBContext dBContext = new AutoSherDBContext())
                {
                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("inwyzuser_id",wyzUserID_var),
                        new MySqlParameter("invehicle_id",vehID_var),
                        new MySqlParameter("inlocid",locID_var),
                        new MySqlParameter("insmsid",smsID_var),
                        new MySqlParameter("ininsid",insID_var),
                        new MySqlParameter("indocid",docID_var)
                    };
                    if (procedureType_var == "1")
                    {
                        string dbName = dBContext.Database.Connection.Database;
                      returnString = dBContext.Database.SqlQuery<string>(@"call sendemailsubject(@inwyzuser_id,@invehicle_id,@inlocid,@insmsid,@ininsid,@indocid);", param).FirstOrDefault();
                        //returnString = dBContext.Database.SqlQuery<string>(@"call sendemailsubject('2', '24053', '0', '3', '1', 0);").FirstOrDefault();
                    }
                    else if (procedureType_var == "2")
                    {
                      returnString = dBContext.Database.SqlQuery<string>(@"call sendemailbody(@inwyzuser_id,@invehicle_id,@inlocid,@insmsid,@ininsid,@indocid);", param).FirstOrDefault();
                       //returnString = dBContext.Database.SqlQuery<string>(@"call sendemailbody('2', '24053', '0', '3', '1', 0);").FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return returnString;
        }
        #endregion


        #region side Address

        //By Nisarga
        public ActionResult getStateCityByPincode(string values)
        {
            try
            {
                using (AutoSherDBContext dBContext = new AutoSherDBContext())
                {
                    var ListOfCity = dBContext.citystates.Where(x => x.pincode == values).Select(m => m.city).ToList();
                    var state = dBContext.citystates.Where(x => x.pincode == values).Select(m => m.state).Distinct().ToList();
                    return Json(new { success = true, ListOfCity = ListOfCity, state = state });
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }

        //By Nisarga
        public ActionResult getDataForAddressTable()
        {
            long? customerId = long.Parse(Session["CusId"].ToString());
            List<CallLoggingCustomerAddressVM> listOfAddr = new List<CallLoggingCustomerAddressVM>();
            List<address> customerAddress = new List<address>();
            List<upload> customerUpload = new List<upload>();
            List<uploadtype> customerUploadType = new List<uploadtype>();
            using (AutoSherDBContext dBContext = new AutoSherDBContext())
            {
                customerAddress = dBContext.addresses.Where(m => m.customer_Id == customerId).ToList();

                foreach (var item in customerAddress)
                {
                    CallLoggingCustomerAddressVM addrData = new CallLoggingCustomerAddressVM();
                    addrData.addressId = item.id;
                    addrData.address = item.concatenatedAdress;
                    addrData.city = item.city;
                    addrData.pincode = item.pincode;
                    addrData.isPrimary = item.isPreferred;
                    var upoadId = item.upload_id;
                    if (upoadId != null)
                    {
                        var uploadLong = long.Parse(upoadId);
                        addrData.updateOn = dBContext.uploads.Single(m => m.id == uploadLong).uploadedDateTime;
                        var uploadTypeId = dBContext.uploads.Single(m => m.id == uploadLong).uploadType_id;
                        addrData.updatedBy = dBContext.uploadtypes.Single(m => m.id == uploadTypeId).uploadTypeName;
                    }
                    else
                    {
                        addrData.updateOn = item.updatedDateTime;
                        addrData.updatedBy = item.wyzUserName;
                    }

                    listOfAddr.Add(addrData);
                }

            }
            return Json(new { success = true, listOfAddr = listOfAddr }, JsonRequestBehavior.AllowGet);
        }


        //By Nisarga
        public ActionResult AddNewAddress(string values)
        {
            var listOfAddr = JsonConvert.DeserializeObject<CallLoggingCustomerAddressVM>(values);
            try
            {
                long custId = long.Parse(Session["CusId"].ToString());
                using (AutoSherDBContext dBContext = new AutoSherDBContext())
                {
                    if (listOfAddr != null)
                    {
                        address address = new address();
                        address.customer_Id = custId;
                        address.concatenatedAdress = listOfAddr.address;
                        address.city = listOfAddr.city;
                        address.state = listOfAddr.state;
                        address.pincode = listOfAddr.pincode;
                        address.addressType = 1;
                        if (listOfAddr.isPrimary == true)
                        {
                            var listOfIsPrefered = dBContext.addresses.Where(x => x.customer_Id == custId).ToList();
                            foreach (var item in listOfIsPrefered)
                            {
                                item.isPreferred = false;
                            }
                        }
                        address.wyzUserName = Session["UserName"].ToString();
                        address.updatedDateTime = DateTime.Now;
                        address.isPreferred = listOfAddr.isPrimary;
                        dBContext.addresses.Add(address);
                        dBContext.SaveChanges();
                        listOfAddr.addressId = address.id;
                        listOfAddr.updatedBy = Session["UserName"].ToString(); ;
                        listOfAddr.updateOn = DateTime.Now;
                    }
                    return Json(new { success = true, address = listOfAddr.address, city = listOfAddr.city, pincode = listOfAddr.pincode, isPrimary = listOfAddr.isPrimary });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }

        }

        public ActionResult deleteRowFromPopUp(long value)
        {
            string addres = string.Empty;
            try
            {
                using (AutoSherDBContext dBContext = new AutoSherDBContext())
                {
                    address delRow = dBContext.addresses.Single(m => m.id == value);

                    addres = delRow.concatenatedAdress;
                    dBContext.addresses.Remove(delRow);
                    dBContext.SaveChanges();

                    return Json(new { success = true, address = addres });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }

        public ActionResult isPrimaryChange(long value)
        {
            try
            {
                using (AutoSherDBContext dBContext = new AutoSherDBContext())
                {
                    long custId = long.Parse(Session["CusId"].ToString());
                    var listOfData = dBContext.addresses.Where(x => x.customer_Id == custId).ToList();
                    string prefferedAddress = string.Empty;
                    foreach (var item in listOfData)
                    {
                        if (item.id == value)
                        {
                            item.isPreferred = true;
                            item.updatedDateTime = DateTime.Now;
                            item.wyzUserName = Session["UserName"].ToString();
                            prefferedAddress = item.concatenatedAdress;
                        }
                        else
                        {
                            item.isPreferred = false;
                            item.updatedDateTime = DateTime.Now;
                            item.wyzUserName = Session["UserName"].ToString();
                        }
                    }
                    dBContext.SaveChanges();
                    return Json(new { success = true, prefAddress = prefferedAddress });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }

        #endregion


        #region updating field executive address select box

        public ActionResult updateAddressMSSId()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    long? custId = Convert.ToInt64(Session["CusId"].ToString());
                    var addres = db.addresses.Where(m => m.customer_Id == custId).OrderByDescending(m => m.isPreferred == true).Select(m => m.concatenatedAdress).ToList();
                    return Json(new { success = true, addresses = addres });
                }
            }
            catch (Exception ex)
            {

            }

            return Json(new { success = false });

        }

        #endregion




        #region Sup Cre Kalyani Changes 

        public ActionResult userSuprevisorList(string moduleId)
        {
            List<wyzuser> escalationList = new List<wyzuser>();

            try
            {
                wyzuser wyzuserList = new wyzuser();
                using (var db = new AutoSherDBContext())
                {
                    int UserId = Convert.ToInt32(Session["UserId"].ToString());

                    wyzuserList = db.wyzusers.FirstOrDefault(w => w.id == UserId);

                    long? workshopId = wyzuserList.workshop_id;
                    long? locationId = wyzuserList.location_cityId;

                    long countCRELinkEscCRES = db.Escalationusermappings.Count(u => u.moduleId == "3" || u.moduleId == moduleId && u.creIds != null);
                    long countLocationlinkedEscCRES = db.Escalationusermappings.Count(u => u.moduleId == "3" || u.moduleId == moduleId && u.locationId == locationId);
                    long countWorkshplinkedEscCRES = db.Escalationusermappings.Count(u => u.moduleId == "3" || u.moduleId == moduleId && u.workshopId == workshopId);
                    long countModuleWiselinkedEscCRES = db.Escalationusermappings.Count(u => u.moduleId == "3" || u.moduleId == moduleId && u.workshopId == workshopId);

                    if (countCRELinkEscCRES > 0)
                    {
                        List<String> supIdListExist = db.Escalationusermappings.Where(u => u.moduleId == "3" || u.moduleId == moduleId && u.creIds != null).Select(u => u.creIds).ToList();


                        bool exist = supIdListExist.Any(s => s.Contains(UserId.ToString()));

                        if (exist)
                        {

                            List<long> supIdList = db.Escalationusermappings.Where(u => u.creIds != null && (u.moduleId == moduleId || u.moduleId == "3")).Select(u => u.escalationUserId).ToList();



                            foreach (long sa in supIdList)
                            {
                                wyzuser list = db.wyzusers.FirstOrDefault(u => u.id == sa);
                                escalationList.Add(list);

                            }
                        }
                    }
                    else if (countWorkshplinkedEscCRES > 0)
                    {

                        List<long> supIdList = db.Escalationusermappings.Where(u => u.workshopId == workshopId && (u.moduleId == moduleId || u.moduleId == "3")).Select(u => u.escalationUserId).ToList();

                        foreach (long sa in supIdList)
                        {
                            wyzuser list = db.wyzusers.FirstOrDefault(u => u.id == sa);
                            escalationList.Add(list);

                        }
                    }
                    else if (countLocationlinkedEscCRES > 0)
                    {
                        List<long> supIdList = db.Escalationusermappings.Where(u => u.locationId == locationId && (u.moduleId == moduleId || u.moduleId == "3")).Select(u => u.escalationUserId).ToList();

                        foreach (long sa in supIdList)
                        {
                            wyzuser list = db.wyzusers.FirstOrDefault(u => u.id == sa);
                            escalationList.Add(list);

                        }

                    }
                    else if (countModuleWiselinkedEscCRES > 0)
                    {
                        List<long> supIdList = db.Escalationusermappings.Where(u => u.moduleId == moduleId || u.moduleId == "3").Select(u => u.escalationUserId).ToList();

                        foreach (long sa in supIdList)
                        {
                            wyzuser list = db.wyzusers.FirstOrDefault(u => u.id == sa);
                            escalationList.Add(list);

                        }
                    }



                    List<wyzuser> userJsonList = new List<wyzuser>();
                    foreach (var sa in escalationList)
                    {

                        wyzuser wr = new wyzuser();
                        wr.id = sa.id;
                        wr.userName = sa.userName;
                        userJsonList.Add(wr);

                    }
                    return Json(new { userlist = userJsonList });
                }

            }
            catch (Exception ex)
            {

            }
            return Json(new { });
        }

        #endregion

        public ActionResult getLatestLocationForSMR(long? workshopId)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (workshopId != 0 && workshopId != null)
                    {
                        long locationId = db.workshops.FirstOrDefault(m => m.id == workshopId).location_cityId ?? default(long);
                        string name = db.locations.FirstOrDefault(m => m.cityId == locationId).name;

                        return Json(new { success = true, locId = name });
                    }
                    else
                    {
                        return Json(new { success = true, locId = "" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, exception = ex.Message });
            }


        }

        #region _insuranceOutBound

        public string _insuranceOutBoundSubmit(callinteraction callinteraction, insurancedisposition ir_disposition, ListingForm listingForm, appointmentbooked appointmentbooked, string bookingMode)
        {
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            long userId = Convert.ToInt32(Session["UserId"].ToString());
            long lastinsuraceId = 0;
            bool bookedExist = false;
            long agentId = 0;
            using (var db = new AutoSherDBContext())
            {
                using (DbContextTransaction dbTrans = db.Database.BeginTransaction())
                {
                    try
                    {

                        if (!string.IsNullOrEmpty(listingForm.fieldWalkingLocation) && long.Parse(listingForm.fieldWalkingLocation) != 0)
                        {
                            appointmentbooked.fieldWalkinLocation = long.Parse(listingForm.fieldWalkingLocation);
                        }
                        if (db.insuranceassignedinteractions.Count(m => m.customer_id == cusId && m.vehicle_vehicle_id == vehiId) > 0)
                        {
                            bool isSuperCre = false;
                            bool isINSSuperControl = false;
                            bool isUserControl = db.dealers.FirstOrDefault().userControl ?? default(bool);

                            if (Session["IsSuperCRE"] != null && Convert.ToBoolean(Session["IsSuperCRE"]) == true)
                            {
                                isSuperCre = true;

                                isINSSuperControl = db.dealers.FirstOrDefault().INSsuperControl ?? default(bool);
                            }

                            string callLogFrom = string.Empty;

                            if (Session["MakeCallFrom"] != null)
                            {
                                callLogFrom = Session["MakeCallFrom"].ToString();
                            }
                            else
                            {
                                callLogFrom = "bucket";
                            }

                            if (isINSSuperControl == false && callLogFrom != "bucket")
                            {
                                if (isSuperCre == true)
                                {
                                    userId = db.insuranceassignedinteractions.FirstOrDefault(m => m.customer_id == cusId && m.vehicle_vehicle_id == vehiId).wyzUser_id ?? default(long);
                                }

                            }

                            appointmentbooked appointExist = new appointmentbooked();
                            pickupdrop pickup = new pickupdrop();
                            long? appointMaxId = 0;
                            if (bookingMode == "Book Appointment" || bookingMode == "Renewal Not Required" || bookingMode == "ConfirmedInsu" || bookingMode == "Reschedule" || bookingMode == "Cancel Appointment" || bookingMode == "Paid" || bookingMode == "Call Me Later" || bookingMode == "INS Others")
                            {
                                int currentDisposition = 0, cancelDisposition = 0;
                                long ass_id = 0;
                                if (bookingMode != "INS Others" && bookingMode != "Cancel Appointment" && bookingMode != "ConfirmedInsu")
                                {
                                    currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == bookingMode).dispositionId;
                                }
                                else if (bookingMode == "ConfirmedInsu")
                                {
                                    currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Confirmed").dispositionId;
                                }
                                else if (bookingMode == "Cancel Appointment")
                                {
                                    currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Cancelled").dispositionId;
                                }
                                else if (bookingMode == "INS Others")
                                {
                                    string dispo = listingForm.othersINS;
                                    if (dispo == "")
                                    {
                                        currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "INS Others").dispositionId;
                                    }
                                    else
                                    {
                                        currentDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == dispo).dispositionId;
                                    }
                                }


                                if (bookingMode == "Book Appointment" || bookingMode == "Reschedule")
                                {
                                    long appId = Convert.ToInt64(Session["appointBookId"]);
                                    int appIdCount = db.appointmentbookeds.Count(m => m.appointmentId == appId && m.customer_id == cusId);
                                    if (appIdCount > 0)
                                    {
                                        appointmentbooked rescheduleappointment = new appointmentbooked();

                                        //if (Session["DealerCode"].ToString() != "PAWANHYUNDAI")
                                        //if (db.dealers.FirstOrDefault().isfieldexecutive)
                                        //{
                                        //    int countfield = db.appointmentbookeds.Count(m => m.appointmentId == appId && m.typeOfPickup == "Field");
                                        //    if (countfield > 0)
                                        //    {
                                        //        if (db.appointmentbookeds.Count(m => m.appointmentId == appId) > 0)
                                        //        {
                                        //            //long? id = db.appointmentbookeds.FirstOrDefault(m => m.appointmentId == appId).pickupDrop_id;
                                        //            //if (id != 0 && id != null)
                                        //            //{
                                        //            //    db.pickupdrops.Where(x => x.id == id).ToList().ForEach(x => { x.timeFrom = null; x.timeTo = null; });
                                        //            //}
                                        //        }
                                        //    }
                                        //}

                                        //if (db.Couponinteractions.Count(m => m.appointmentBookedId == appId) > 0)
                                        //{
                                        //    // coupon savings
                                        //    couponinteraction couponinteractions = db.Couponinteractions.FirstOrDefault(m => m.appointmentBookedId == appId);
                                        //    db.Couponinteractions.Remove(couponinteractions);
                                        //    //couponinteractions.issuedate = appointmentbooked.appointmentDate;
                                        //    //var coupondeatails = db.Coupon_Details.FirstOrDefault(m => m.coupon_uniqueid == appointmentbooked.coupon);
                                        //    //couponinteractions.couponexpirydate = appointmentbooked.appointmentDate.Value.AddDays(Convert.ToDouble(coupondeatails.couponExpiryfrom_issuedate));
                                        //    //coupons(db, couponinteractions);
                                        //}
                                        cancelDisposition = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Cancelled").dispositionId;
                                        rescheduleappointment = db.appointmentbookeds.FirstOrDefault(m => m.appointmentId == appId && m.customer_id == cusId);
                                        rescheduleappointment.insuranceBookStatus_id = cancelDisposition;
                                        db.appointmentbookeds.AddOrUpdate(rescheduleappointment);
                                        appointmentbooked.lastinsuranceagentid = rescheduleappointment.insuranceAgent_insuranceAgentId ?? default(long);
                                        db.SaveChanges();
                                        bookedExist = true;
                                    }

                                    appointmentbooked.customer_id = cusId;
                                    appointmentbooked.chaserId = userId;
                                    appointmentbooked.wyzuser_id = userId;
                                    appointmentbooked.vehicle_id = vehiId;
                                    appointmentbooked.insuranceBookStatus_id = currentDisposition;

                                    if (appointmentbooked.typeOfPickup == "Field" && db.dealers.FirstOrDefault().isfieldexecutive)
                                    {
                                        //if (Session["DealerCode"].ToString() != "PAWANHYUNDAI")
                                        {
                                            long from;
                                            long to;
                                            if (listingForm.time_To_insExist != 0 && listingForm.time_To_ins == 0 && listingForm.time_From_insExist != 0 && listingForm.time_To_ins == 0)
                                            {
                                                from = listingForm.time_From_insExist;
                                                to = listingForm.time_To_insExist + 1;

                                            }
                                            else
                                            {
                                                from = listingForm.time_From_ins;
                                                to = listingForm.time_To_ins + 1;
                                            }
                                            agentId = listingForm.insuAgentIdIns;
                                            bookingdatetime bookingFrom = db.bookingdatetimes.FirstOrDefault(x => x.id == from);
                                            bookingdatetime bookingTo = db.bookingdatetimes.FirstOrDefault(x => x.id == to);
                                            pickup.timeFrom = bookingFrom.startTime;
                                            pickup.timeTo = bookingTo.startTime;
                                            pickup.pickupDate = listingForm.appointmentScheduled;
                                            pickup.pickUpAddress = appointmentbooked.addressOfVisit;
                                            db.pickupdrops.AddOrUpdate(pickup);
                                            db.SaveChanges();

                                            appointmentbooked.appointmentDate = listingForm.appointmentScheduled;
                                            appointmentbooked.pickupDrop_id = pickup.id;
                                            appointmentbooked.appointmentFromTime = pickup.timeFrom.ToString() + " to " + pickup.timeTo.ToString();
                                            appointmentbooked.insuranceAgent_insuranceAgentId = agentId;
                                        }
                                        //else if (Session["DealerCode"].ToString() == "PAWANHYUNDAI")
                                        //{
                                        //    appointmentbooked.appointmentDate = listingForm.appointmentDateField;
                                        //    appointmentbooked.appointmentFromTime = listingForm.appointmentFromTimeField;
                                        //}
                                    }
                                    db.appointmentbookeds.Add(appointmentbooked);
                                    db.SaveChanges();
                                    if (appointmentbooked.typeOfPickup == "Field")
                                    {
                                        if (bookedExist)
                                        {
                                            //fieldFirbaseUpdation(db, "Reschedule", appointmentbooked.appointmentId, appId,0, oldPolicyDropMaxId, vehiId,appointmentbooked.lastinsuranceagentid);
                                            //if (Session["DealerCode"].ToString() == "INDUS" || Session["DealerCode"].ToString() == "INDUSFE" || Session["DealerCode"].ToString() == "PODDARCARWORLD")
                                            if (db.dealers.FirstOrDefault().isfieldexecutive)
                                            {
                                                fieldFirbaseUpdation(db, "Reschedule", appointmentbooked.appointmentId, 0, vehiId);
                                            }
                                        }
                                        else
                                        {
                                            //if (Session["DealerCode"].ToString() == "INDUS" || Session["DealerCode"].ToString() == "INDUSFE" || Session["DealerCode"].ToString() == "PODDARCARWORLD")
                                            if (db.dealers.FirstOrDefault().isfieldexecutive)
                                            {
                                                fieldFirbaseUpdation(db, "New Booking", appointmentbooked.appointmentId, 0, vehiId);
                                            }
                                        }
                                    }


                                }
                                else
                                /*when user clicks renewal not required,cancel,confirm,call me later,paid if the appointment is already booked then appointment table updated otherwise no action required in appointment table.*/
                                {
                                    appointMaxId = getInsuranceAppointmentStatus();
                                    if (appointMaxId != 0)
                                    {
                                        appointExist = db.appointmentbookeds.FirstOrDefault(m => m.appointmentId == appointMaxId);

                                        if (bookingMode == "ConfirmedInsu" || bookingMode == "Renewal Not Required" || bookingMode == "Cancel Appointment" || bookingMode == "Paid" || bookingMode == "Call Me Later" || bookingMode == "INS Others")
                                        {

                                            if (bookingMode == "Cancel Appointment" && db.dealers.FirstOrDefault().isfieldexecutive)
                                            {
                                                long appId = Convert.ToInt64(Session["appointBookId"]);

                                                //if appointment type is field  then cancel the appotment time from scheduler(using pickupdrop_id)
                                                // if (Session["DealerCode"].ToString() != "PAWANHYUNDAI")
                                                //{
                                                int countfield = db.appointmentbookeds.Where(m => m.appointmentId == appId && m.typeOfPickup == "Field").Count();
                                                if (countfield > 0)
                                                {
                                                    if (db.appointmentbookeds.Any(m => m.appointmentId == appId))
                                                    {
                                                        //long? id = db.appointmentbookeds.FirstOrDefault(m => m.appointmentId == appId).pickupDrop_id;
                                                        //db.pickupdrops.Where(x => x.id == id).ToList().ForEach(x => { x.timeFrom = null; x.timeTo = null; });

                                                        //fieldFirbaseUpdation(db, "Cancel Appointment", 0,appId,0,oldPolicyDropMaxId, vehiId,0);
                                                        //if (Session["DealerCode"].ToString() == "INDUS" || Session["DealerCode"].ToString() == "INDUSFE" || Session["DealerCode"].ToString() == "PODDARCARWORLD")
                                                        if (db.dealers.FirstOrDefault().isfieldexecutive)
                                                        {
                                                            fieldFirbaseUpdation(db, "Cancel Appointment", appId, 0, vehiId);
                                                        }

                                                    }
                                                }
                                                //}

                                            }
                                            appointExist.customer_id = cusId;
                                            appointExist.vehicle_id = vehiId;
                                            appointExist.wyzuser_id = userId;
                                            appointExist.insuranceBookStatus_id = currentDisposition;
                                            db.appointmentbookeds.AddOrUpdate(appointExist);
                                            db.SaveChanges();
                                        }
                                    }
                                }

                                if (listingForm.othersINS == "Policy Drop")
                                {
                                    //if (db.dealers.FirstOrDefault().ispolicydropexecutive)
                                    //{
                                    //    long from;
                                    //    long to;
                                    //    from = listingForm.policytime_From_ins;
                                    //    to = listingForm.policytime_To_ins + 1;

                                    //    agentId = listingForm.policyinsuAgentIdIns;
                                    //    bookingdatetime bookingFrom = db.bookingdatetimes.FirstOrDefault(x => x.id == from);
                                    //    bookingdatetime bookingTo = db.bookingdatetimes.FirstOrDefault(x => x.id == to);
                                    //    pickup.timeFrom = bookingFrom.startTime;
                                    //    pickup.timeTo = bookingTo.startTime;
                                    //    pickup.pickupDate = listingForm.policydropdate;
                                    //    pickup.pickUpAddress = ir_disposition.policyDropAddress;
                                    //    db.pickupdrops.AddOrUpdate(pickup);
                                    //    db.SaveChanges();

                                    //    ass_id = recordDisposition(2, "insurance", db, currentDisposition, 0, false, false, listingForm.policydroplocation, listingForm.policydropdate.ToString(), agentId, ir_disposition.policyPincode, pickup.id);

                                    //    fieldFirbaseUpdation(db, "PolicyDrop", ass_id, 0, vehiId);

                                    //}
                                    //else
                                    //{
                                    //    ass_id = recordDisposition(2, "insurance", db, currentDisposition, 0, false, false, ir_disposition.locationId, ir_disposition.policayDropDate.ToString());
                                    //}
                                }
                                else
                                {
                                    ass_id = recordDisposition(2, "insurance", db, currentDisposition, 0, isSuperCre);

                                }

                                //***************** CallInteraction starts******************//

                                callinteraction.callCount = 1;
                                callinteraction.callDate = DateTime.Now.ToString("dd-MM-yyyy");
                                callinteraction.callMadeDateAndTime = DateTime.Now;
                                callinteraction.callTime = DateTime.Now.ToString("HH:mm:ss");
                                callinteraction.dealerCode = Session["DealerCode"].ToString();
                                if (Session["isCallInitiated"] != null)
                                {
                                    callinteraction.isCallinitaited = "initiated";
                                    if (Session["MakeCallFrom"] != null)
                                    {
                                        callinteraction.makeCallFrom = Session["MakeCallFrom"].ToString();
                                    }
                                    else
                                    {
                                        callinteraction.makeCallFrom = "insurance";
                                    }

                                    if (Session["AndroidUniqueId"] != null)
                                    {
                                        callinteraction.uniqueidForCallSync = int.Parse(Session["AndroidUniqueId"].ToString());
                                    }
                                    else if (Session["GSMUniqueId"] != null)
                                    {
                                        callinteraction.uniqueIdGSM = Session["GSMUniqueId"].ToString().Split(';')[0];
                                    }
                                }

                                callinteraction.callStatus = Convert.ToBoolean(Session["NCReason"]);
                                if (Session["MakeCallFrom"] != null)
                                {
                                    callinteraction.makeCallFrom = Session["MakeCallFrom"].ToString();
                                }
                                else
                                {
                                    callinteraction.makeCallFrom = "insurance";
                                }

                                if (Session["DialedNumber"] != null)
                                {
                                    callinteraction.dailedNoIs = Session["DialedNumber"].ToString();
                                }
                                callinteraction.insuranceAssignedInteraction_id = ass_id;
                                callinteraction.customer_id = cusId;
                                callinteraction.vehicle_vehicle_id = vehiId;
                                callinteraction.wyzUser_id = userId;

                                if (bookingMode == "Book Appointment" || bookingMode == "Reschedule")
                                {
                                    callinteraction.appointmentBooked_appointmentId = appointmentbooked.appointmentId;
                                }
                                else
                                {
                                    if (appointMaxId != 0)
                                    {
                                        callinteraction.appointmentBooked_appointmentId = appointExist.appointmentId;

                                    }

                                }

                                int tagCount = db.taggedassignments.Where(m => m.vehicle_id == vehiId && m.module_id == 1 && m.active == true).Count();
                                int taggId = 0;
                                string camp = "";
                                if (tagCount > 0)
                                {
                                    if (db.taggedassignments.Count(m => m.vehicle_id == vehiId && m.module_id == 1 && m.active == true) > 0)
                                    {
                                        taggId = Convert.ToInt32(db.taggedassignments.FirstOrDefault(m => m.vehicle_id == vehiId && m.module_id == 1 && m.active == true).tagging_id);

                                        if (db.campaigns.Count(m => m.id == taggId) > 0)
                                        {
                                            camp = db.campaigns.FirstOrDefault(m => m.id == taggId).campaignName;
                                        }
                                    }
                                }



                                callinteraction.agentName = Session["UserName"].ToString();
                                callinteraction.campaign_id = db.insuranceassignedinteractions.FirstOrDefault(m => m.id == ass_id).campaign_id;
                                callinteraction.tagging_id = taggId;
                                callinteraction.tagging_name = camp;
                                callinteraction.chasserCall = false;
                                db.callinteractions.Add(callinteraction);
                                db.SaveChanges();


                                if (Session["GSMUniqueId"] != null)
                                {
                                    gsmsynchdata gsm = new gsmsynchdata();
                                    gsm.Callinteraction_id = callinteraction.id;
                                    gsm.CallMadeDateTime = DateTime.Now;
                                    gsm.UniqueGsmId = Session["GSMUniqueId"].ToString().Split(';')[0];
                                    gsm.TenantUrl = Session["GSMUniqueId"].ToString().Split(';')[1];
                                    gsm.callFor = Convert.ToInt32(Session["UserRole1"]);
                                    db.gsmsynchdata.Add(gsm);
                                    db.SaveChanges();
                                }
                                long callId = callinteraction.id;

                                //***************** CallInteraction End******************

                                string remarks = "", comments = "";

                                for (int i = 0; i < listingForm.remarksList.Count; i++)
                                {
                                    if (remarks == "" && listingForm.remarksList[i] != "")
                                    {
                                        ir_disposition.comments = listingForm.remarksList[i];
                                    }

                                    if (comments == "" && listingForm.commentsList[i] != "")
                                    {
                                        ir_disposition.customerComments = listingForm.commentsList[i];
                                    }
                                }

                                List<string> addonListData = listingForm.addonsList;
                                string addonData = "";
                                if (addonListData != null)
                                {
                                    foreach (string sa in addonListData)
                                    {

                                        if (sa != null)
                                        {

                                            addonData = addonData + sa;
                                        }

                                    }
                                }
                                ir_disposition.addons = addonData + "," + listingForm.otherAddon;
                                int upselCount = 0;
                                //********************** insurance_Disposition Saving part ********************
                                if (ir_disposition.typeOfAutherization == "Unauthorized Dealer")
                                {
                                    ir_disposition.dateOfRenewal = listingForm.dateOfRenewalNonAuth;
                                    ir_disposition.insuranceProvidedBy = listingForm.insuranceProvidedUnAuth;
                                }

                                if (listingForm.VehicleSoldYes == "VehicleSold Yes")
                                {
                                    soldNewCustomerVehicles vehiclesold = new soldNewCustomerVehicles();
                                    vehiclesold.custmerId = cusId;
                                    vehiclesold.wyzuserid = userId;
                                    vehiclesold.customerFName = listingForm.customerFName;
                                    vehiclesold.Callinteraction_id = callinteraction.id;
                                    //vehiclesold.assignedInteractionid = listingForm.customerFName;

                                    vehiclesold.customerLName = listingForm.customerLName;
                                    vehiclesold.state = listingForm.state;
                                    vehiclesold.city = listingForm.city;
                                    vehiclesold.vehicleRegNo = listingForm.vehicleRegNo;
                                    vehiclesold.chassisNo = listingForm.chassisNo;
                                    vehiclesold.variant = listingForm.variant;
                                    vehiclesold.model = listingForm.model;
                                    vehiclesold.dealershipName = listingForm.dealershipName;
                                    vehiclesold.saleDate = listingForm.saleDate;
                                    List<string> phoneList = listingForm.phoneList;
                                    string phoneNo = "";
                                    foreach (var phone in phoneList)
                                    {
                                        phoneNo = phoneNo + "," + phone;
                                    }
                                    vehiclesold.addressLine1 = listingForm.addressLine1;
                                    vehiclesold.addressLine2 = listingForm.addressLine2;
                                    vehiclesold.pincode = listingForm.pincode;
                                    vehiclesold.initial = listingForm.initial;
                                    db.SoldNewCustomerVehicles.Add(vehiclesold);
                                    db.SaveChanges();
                                }

                                if (listingForm.assignedSA != null)
                                {
                                    long asignedSaId = db.taggingusers.FirstOrDefault(m => m.name == listingForm.assignedSA).id;
                                    ir_disposition.assignedToSA = asignedSaId;
                                }

                                if (ir_disposition.renewalNotRequiredReason == "Dissatisfied with Insurance")
                                {
                                    ir_disposition.noServiceReasonTaggedTo = listingForm.noServiceReasonTaggedTo1;
                                    ir_disposition.noServiceReasonTaggedToComments = listingForm.noServiceReasonTaggedToComments1;
                                }
                                // if (bookingMode == "INS Others" && (listingForm.othersINS == "DNM" || listingForm.othersINS == "DNC"))

                                ir_disposition.othersINS = listingForm.othersINS;

                                //if (listingForm.othersINS == "Policy Drop")
                                //{
                                //    fieldFirbaseUpdation(db,"New", 0, ass_id, 0, 0, 0, false, lastinsuraceId);
                                //}


                                ir_disposition.InOutCallName = "OutCall";
                                ir_disposition.callDispositionData_id = currentDisposition;
                                ir_disposition.callInteraction_id = callinteraction.id;
                                ir_disposition.callinteraction = callinteraction;
                                ir_disposition.upsellCount = upselCount;



                                if (isSuperCre == true && isINSSuperControl == false)
                                {
                                    ir_disposition.customerComments = ir_disposition.customerComments + " - By-" + Session["UserName"].ToString();
                                    ir_disposition.comments = ir_disposition.comments + " - By-" + Session["UserName"].ToString();
                                }


                                db.insurancedispositions.Add(ir_disposition);
                                db.SaveChanges();
                                //sr_disposition.

                                if (bookingMode == "Book Appointment" || bookingMode == "Reschedule")

                                {

                                    #region Coupon Savings
                                    if (appointmentbooked.coupon != null)
                                    {
                                        bool isReschedule = false;
                                        long appId = Convert.ToInt64(Session["appointBookId"]);
                                        int appIdCount = db.appointmentbookeds.Count(m => m.appointmentId == appId && m.customer_id == cusId);
                                        isReschedule = appIdCount > 0 ? true : false;

                                        // coupon savings
                                        couponinteraction couponinteractions = new couponinteraction();
                                        if (isReschedule)
                                        {
                                            //long appId = Convert.ToInt64(Session["appointBookId"]);
                                            //int appIdCount = db.appointmentbookeds.Count(m => m.appointmentId == appId && m.customer_id == cusId);
                                            if (appIdCount > 0)
                                                if (db.Couponinteractions.Count(m => m.appointmentBookedId == appId) > 0)
                                                {
                                                    // coupon savings
                                                    couponinteraction oldCouponinteractions = db.Couponinteractions.FirstOrDefault(m => m.appointmentBookedId == appId);

                                                    oldCouponinteractions.issuedate = appointmentbooked.appointmentDate;
                                                    oldCouponinteractions.appointmentBookedId = appointmentbooked.appointmentId;
                                                    oldCouponinteractions.callinteraction_id = callinteraction.id;
                                                    var coupondeatails = db.Coupon_Details.FirstOrDefault(m => m.coupon_uniqueid == appointmentbooked.coupon);
                                                    oldCouponinteractions.couponexpirydate = appointmentbooked.appointmentDate.Value.AddDays(Convert.ToDouble(coupondeatails.couponExpiryfrom_issuedate));
                                                    coupons(db, oldCouponinteractions);

                                                }
                                            else
                                                {

                                                    couponinteractions.status = "PENDING";
                                                    couponinteractions.cre_name = Session["UserName"].ToString();
                                                    couponinteractions.customer_id = cusId;
                                                    couponinteractions.cre_id = userId;
                                                    couponinteractions.vehicleid = vehiId;
                                                    couponinteractions.callinteraction_id = callinteraction.id;
                                                    couponinteractions.assigninteraction_id = ass_id;
                                                    couponinteractions.issuedate = appointmentbooked.appointmentDate;
                                                    couponinteractions.couponcode = appointmentbooked.coupon;
                                                    couponinteractions.appointmentBookedId = appointmentbooked.appointmentId;
                                                    var coupondeatails = db.Coupon_Details.FirstOrDefault(m => m.coupon_uniqueid == appointmentbooked.coupon);
                                                    couponinteractions.coupondeatails = coupondeatails.coupon_name;
                                                    couponinteractions.smstempcode = coupondeatails.smscode;
                                                    couponinteractions.couponexpirydate = appointmentbooked.appointmentDate.Value.AddDays(Convert.ToDouble(coupondeatails.couponExpiryfrom_issuedate));
                                                    coupons(db, couponinteractions);

                                                }


                                        }

                                        else
                                        {

                                            couponinteractions.status = "PENDING";
                                            couponinteractions.cre_name = Session["UserName"].ToString();
                                            couponinteractions.customer_id = cusId;
                                            couponinteractions.cre_id = userId;
                                            couponinteractions.vehicleid = vehiId;
                                            couponinteractions.callinteraction_id = callinteraction.id;
                                            couponinteractions.assigninteraction_id = ass_id;
                                            couponinteractions.issuedate = appointmentbooked.appointmentDate;
                                            couponinteractions.couponcode = appointmentbooked.coupon;
                                            couponinteractions.appointmentBookedId = appointmentbooked.appointmentId;
                                            var coupondeatails = db.Coupon_Details.FirstOrDefault(m => m.coupon_uniqueid == appointmentbooked.coupon);
                                            couponinteractions.coupondeatails = coupondeatails.coupon_name;
                                            couponinteractions.smstempcode = coupondeatails.smscode;
                                            couponinteractions.couponexpirydate = appointmentbooked.appointmentDate.Value.AddDays(Convert.ToDouble(coupondeatails.couponExpiryfrom_issuedate));
                                            coupons(db, couponinteractions);

                                        }

                                    }
                                    #endregion

                                    if (listingForm.LeadYes == "Capture Lead Yes")
                                    {
                                        if (listingForm.upsellleads != null)
                                            foreach (var upsel in listingForm.upsellleads)
                                            {
                                                if (upsel.taggedTo != null)
                                                {
                                                    upselllead upsell = new upselllead();
                                                    upsell = upsel;
                                                    upsell.vehicle_vehicle_id = vehiId;
                                                    upsell.insuranceDisposition_id = ir_disposition.id;

                                                    long upselUserId;
                                                    upselUserId = db.taggingusers.FirstOrDefault(m => m.upsellLeadId == upsel.upsellId).id;

                                                    if (upselUserId != 0)
                                                    {
                                                        upsell.taggingUsers_id = upselUserId;
                                                    }
                                                    upsell.taggedTo = upsel.taggedTo;

                                                    db.upsellleads.Add(upsell);
                                                    db.SaveChanges();
                                                    upselCount++;

                                                }
                                            }
                                    }
                                }
                                if (bookingMode == "Renewal Not Required")
                                {
                                    if (listingForm.LeadYesRNR == "Capture Lead Yes")
                                    {
                                        if (listingForm.upsellleadsRNR != null)
                                            foreach (var upsel in listingForm.upsellleadsRNR)
                                            {
                                                if (upsel.taggedTo != null)
                                                {
                                                    upselllead upsell = new upselllead();
                                                    upsell = upsel;
                                                    upsell.vehicle_vehicle_id = vehiId;
                                                    upsell.insuranceDisposition_id = ir_disposition.id;

                                                    long upselUserId;
                                                    upselUserId = db.taggingusers.FirstOrDefault(m => m.upsellLeadId == upsel.upsellId).id;

                                                    if (upselUserId != 0)
                                                    {
                                                        upsell.taggingUsers_id = upselUserId;
                                                    }
                                                    upsell.taggedTo = upsel.taggedTo;

                                                    db.upsellleads.Add(upsell);
                                                    db.SaveChanges();
                                                    upselCount++;

                                                }
                                            }
                                    }
                                }
                                ir_disposition.upsellCount = upselCount++;
                                db.SaveChanges();

                                db.Database.ExecuteSqlCommand("call Triggerinsertinsurancecallhistrycube(@newid);", new MySqlParameter("@newid", callId));



                            }
                            dbTrans.Commit();

                            //------------------------- Triggering AutoSMS ----------------------------


                            //-----------------------------Triggering Auto SMS ------------------------------
                            if (db.dealers.FirstOrDefault().isfieldexecutive)
                            {

                                if (bookingMode == "Book Appointment" || bookingMode == "Reschedule")
                                {
                                    if (agentId != 0)
                                    {
                                        autosmsday(userId, vehiId, cusId, "Field Executive", "Insurance", 0, agentId, 0, "", 0);
                                    }
                                    autosmsday(userId, vehiId, cusId, "APPOINTMENT", "Insurance", 0, 0, 0, "", 0);
                                }
                            }
                            else
                            {
                                if (bookingMode == "Book Appointment" || bookingMode == "Reschedule")
                                {
                                    autosmsday(userId, vehiId, cusId, "APPOINTMENT", "Insurance", 0, 0, 0, "", 0);
                                }
                            }
                            string customMsg = string.Empty;
                            //Complaints
                            if (ir_disposition.renewalNotRequiredReason != null && (ir_disposition.renewalNotRequiredReason == "Dissatisfied with previous service" || ir_disposition.renewalNotRequiredReason == "Dissatisfied with Sales" || ir_disposition.renewalNotRequiredReason == "Dissatisfied with Insurance"))
                            {
                                string smsTypeva = "COMPLAINT";
                                autosmsday(userId, vehiId, cusId, smsTypeva, "Insurance", 0, 0, 0, "COMPLAINT", 0);

                                // dissat sms
                                int taggingId = 0;
                                if (ir_disposition.renewalNotRequiredReason == "Dissatisfied with Sales")
                                {
                                    taggingId = 26;
                                }
                                else if (ir_disposition.renewalNotRequiredReason == "Dissatisfied with Insurance")
                                {
                                    taggingId = 27;
                                }
                                else
                                {
                                    taggingId = 29;
                                }

                                string smsType1 = "INSDISSAT";

                                customMsg = "INS DISSAT : " + ir_disposition.renewalNotRequiredReason;
                                autosmsday(userId, vehiId, cusId, smsType1, "Insurance", taggingId, 0, 0, customMsg, 0);

                            }

                            // feedback sms
                            if (listingForm.CustomerfeedbackRNR == "Yes")
                            {
                                long departmentId = long.Parse(ir_disposition.departmentForFB);

                                complainttype complainttype = db.complainttypes.FirstOrDefault(m => m.id == departmentId);

                                string dept = complainttype.departmentName;
                                customMsg = "INS Feedback : " + dept;
                                autosmsday(userId, vehiId, cusId, "INSFEEDBACK", "Insurance", 0, 0, departmentId, customMsg, 0);
                            }
                        }
                        else
                        {


                        }
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
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
                            exception += " " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)];
                        }

                        Logger logger = LogManager.GetLogger("apkRegLogger");

                        logger.Info("_insuranceBound Lock: \n" + ex.StackTrace + "\n" + exception);
                        return exception;
                    }

                }

            }
            return "True";
        }

        #endregion


        #region Field Execution New Firebase Adding and Updating 
        /// <summary>
        /// function used to update fieldexecutivefirebaseupdation table to push field disposition data to firebase to reflect in FE app
        /// </summary>
        /// <param name="db"> instance of current db</param>
        /// <param name="action">incoming or disposed as</param>
        /// <param name="newappId">new appointmentId</param>
        /// <param name="newPolicyId">new policudrop id</param>
        /// <param name="vehicle_id">vvehicle id</param>
        /// <returns>true if success or error thrown to parent function catch</returns>
        public bool fieldFirbaseUpdation(AutoSherDBContext db, string action, long newappId, long newPolicyId, long vehicle_id)
        {
            long? lastAgentId = null;
            long? maxFeId = 0;
            bool isOldDataPushed = true;

            string lastFirebaseKey = "";

            //maxFeId = db.Fieldexecutivefirebaseupdations.Where(m => m.vehicle_id == vehicle_id).Max(m => m.id);
            var maxData = db.Fieldexecutivefirebaseupdations.Where(m => m.vehicle_id == vehicle_id).OrderByDescending(m => m.id).FirstOrDefault();
            if (maxData != null)
            {
                maxFeId = maxData.id;

                if (maxData.tobepushed == 0 && string.IsNullOrEmpty(maxData.firebasekey))
                {
                    lastAgentId = maxData.agentId;
                    lastFirebaseKey = maxData.lastfirebase_key;
                    db.Fieldexecutivefirebaseupdations.Remove(maxData);
                    db.SaveChanges();

                    isOldDataPushed = false;
                }

                if (isOldDataPushed)
                {
                    if (maxData.appointmentbookedid != 0)
                    {
                        if (db.appointmentbookeds.Count(m => m.appointmentId == maxData.appointmentbookedid) > 0)
                        {
                            lastAgentId = db.appointmentbookeds.FirstOrDefault(m => m.appointmentId == maxData.appointmentbookedid).insuranceAgent_insuranceAgentId;
                        }
                    }
                    else if (maxData.inspolicydrop_id != 0)
                    {
                        if (db.insPolicyDrop.Count(m => m.id == maxData.inspolicydrop_id) > 0)
                        {
                            lastAgentId = db.insPolicyDrop.FirstOrDefault(m => m.id == maxData.inspolicydrop_id).agent_id;
                        }
                    }
                    lastFirebaseKey = maxData.firebasekey;
                    db.Fieldexecutivefirebaseupdations.Remove(maxData);
                    db.SaveChanges();
                }
            }

            if (action == "PolicyDrop")
            {
                fieldexecutivefirebaseupdation newFEFirebaseData = new fieldexecutivefirebaseupdation();

                newFEFirebaseData.inspolicydrop_id = newPolicyId;
                newFEFirebaseData.appointmentbookedid = 0;
                newFEFirebaseData.vehicle_id = vehicle_id;
                newFEFirebaseData.lastfirebase_key = lastFirebaseKey;
                newFEFirebaseData.agentId = lastAgentId ?? default(long);
                newFEFirebaseData.tobepushed = 0;
                newFEFirebaseData.updatedatetime = DateTime.Now;
                newFEFirebaseData.inspolicydrop_id = newPolicyId;
                db.Fieldexecutivefirebaseupdations.Add(newFEFirebaseData);
            }
            else if (action == "New Booking")
            {
                fieldexecutivefirebaseupdation newFEFirebaseData = new fieldexecutivefirebaseupdation();

                newFEFirebaseData.inspolicydrop_id = 0;
                newFEFirebaseData.appointmentbookedid = newappId;
                newFEFirebaseData.vehicle_id = vehicle_id;
                newFEFirebaseData.lastfirebase_key = lastFirebaseKey;
                newFEFirebaseData.agentId = lastAgentId ?? default(long);
                newFEFirebaseData.tobepushed = 0;
                newFEFirebaseData.updatedatetime = DateTime.Now;
                db.Fieldexecutivefirebaseupdations.Add(newFEFirebaseData);
            }
            else if (action == "Reschedule")
            {
                fieldexecutivefirebaseupdation newFEFirebaseData = new fieldexecutivefirebaseupdation();

                newFEFirebaseData.inspolicydrop_id = 0;
                newFEFirebaseData.appointmentbookedid = newappId;
                newFEFirebaseData.vehicle_id = vehicle_id;
                newFEFirebaseData.lastfirebase_key = lastFirebaseKey;
                newFEFirebaseData.agentId = lastAgentId ?? default(long);
                newFEFirebaseData.tobepushed = 0;
                newFEFirebaseData.updatedatetime = DateTime.Now;
                db.Fieldexecutivefirebaseupdations.Add(newFEFirebaseData);
            }
            else if (action == "Cancel Appointment")
            {
                fieldexecutivefirebaseupdation newFEFirebaseData = new fieldexecutivefirebaseupdation();

                newFEFirebaseData.inspolicydrop_id = 0;
                newFEFirebaseData.appointmentbookedid = newappId;
                newFEFirebaseData.vehicle_id = vehicle_id;
                newFEFirebaseData.lastfirebase_key = lastFirebaseKey;
                newFEFirebaseData.agentId = lastAgentId ?? default(long);
                newFEFirebaseData.tobepushed = 0;
                newFEFirebaseData.updatedatetime = DateTime.Now;
                newFEFirebaseData.isCancelled = true;
                db.Fieldexecutivefirebaseupdations.Add(newFEFirebaseData);
            }

            #region oldCode Commented
            //if (action == "New")
            //{
            //    fieldexecutivefirebaseupdation fieldexecutivefirebaseupdation;

            //    fieldexecutivefirebaseupdation = db.Fieldexecutivefirebaseupdations.FirstOrDefault(m => m.vehicle_id == vehicle_id);

            //    if (fieldexecutivefirebaseupdation != null)
            //    {
            //        fieldexecutivefirebaseupdation.appointmentbookedid = newappId;
            //        fieldexecutivefirebaseupdation.insassignedid = 0;
            //        fieldexecutivefirebaseupdation.tobepushed = 0;
            //        fieldexecutivefirebaseupdation.updatedatetime = DateTime.Now;
            //        fieldexecutivefirebaseupdation.vehicle_id = vehicle_id;

            //        insuranceassignedinteraction FEIds = db.insuranceassignedinteractions.FirstOrDefault(m => m.vehicle_vehicle_id == vehicle_id);

            //        if (FEIds.lastFEID != 0)
            //        {
            //            fieldexecutivefirebaseupdation.agentId = FEIds.lastFEID;
            //        }
            //        else if (FEIds.FEID != 0)
            //        {
            //            fieldexecutivefirebaseupdation.agentId = FEIds.FEID;
            //        }
            //        else
            //        {
            //            fieldexecutivefirebaseupdation.agentId = 0;
            //        }
            //        db.Fieldexecutivefirebaseupdations.AddOrUpdate(fieldexecutivefirebaseupdation);
            //    }
            //    else
            //    {
            //        fieldexecutivefirebaseupdation = new fieldexecutivefirebaseupdation();
            //        fieldexecutivefirebaseupdation.appointmentbookedid = newappId;
            //        fieldexecutivefirebaseupdation.insassignedid = 0;
            //        fieldexecutivefirebaseupdation.tobepushed = 0;
            //        fieldexecutivefirebaseupdation.updatedatetime = DateTime.Now;
            //        fieldexecutivefirebaseupdation.vehicle_id = vehicle_id;

            //        db.Fieldexecutivefirebaseupdations.AddOrUpdate(fieldexecutivefirebaseupdation);
            //    }
            //}
            //else if (action == "Reschedule")
            //{
            //    if (db.appointmentbookeds.Any(m => m.appointmentId == oldappId))
            //    {
            //        fieldexecutivefirebaseupdation feUpdationExisting = new fieldexecutivefirebaseupdation();

            //        feUpdationExisting = db.Fieldexecutivefirebaseupdations.FirstOrDefault(m => m.vehicle_id == vehicle_id);

            //        if (feUpdationExisting != null)
            //        {
            //            feUpdationExisting.appointmentbookedid = newappId;
            //            feUpdationExisting.insassignedid = 0;
            //            feUpdationExisting.tobepushed = 0;
            //            feUpdationExisting.vehicle_id = vehicle_id;
            //            feUpdationExisting.updatedatetime = DateTime.Now;
            //            feUpdationExisting.agentId = 0;
            //            db.Fieldexecutivefirebaseupdations.AddOrUpdate(feUpdationExisting);

            //        }
            //        else
            //        {
            //            feUpdationExisting = new fieldexecutivefirebaseupdation();
            //            feUpdationExisting.appointmentbookedid = newappId;
            //            feUpdationExisting.tobepushed = 0;
            //            feUpdationExisting.insassignedid = 0;
            //            feUpdationExisting.vehicle_id = vehicle_id;
            //            feUpdationExisting.updatedatetime = DateTime.Now;
            //            feUpdationExisting.agentId = 0;
            //            db.Fieldexecutivefirebaseupdations.Add(feUpdationExisting);
            //        }
            //    }
            //}
            //else if (action == "PolicyDrop")
            //{
            //    fieldexecutivefirebaseupdation fieldexecutivefirebaseupdation = new fieldexecutivefirebaseupdation();

            //    fieldexecutivefirebaseupdation = db.Fieldexecutivefirebaseupdations.FirstOrDefault(m => m.vehicle_id == vehicle_id);




            //    if (fieldexecutivefirebaseupdation != null)
            //    {
            //        fieldexecutivefirebaseupdation.insassignedid = newappId;
            //        fieldexecutivefirebaseupdation.appointmentbookedid= 0;
            //        fieldexecutivefirebaseupdation.tobepushed = 0;
            //        fieldexecutivefirebaseupdation.updatedatetime = DateTime.Now;
            //        fieldexecutivefirebaseupdation.vehicle_id = vehicle_id;

            //        db.Fieldexecutivefirebaseupdations.AddOrUpdate(fieldexecutivefirebaseupdation);
            //    }
            //    else
            //    {
            //        fieldexecutivefirebaseupdation = new fieldexecutivefirebaseupdation();
            //        fieldexecutivefirebaseupdation.insassignedid = newappId;
            //        fieldexecutivefirebaseupdation.appointmentbookedid = 0;
            //        fieldexecutivefirebaseupdation.tobepushed = 0;
            //        fieldexecutivefirebaseupdation.updatedatetime = DateTime.Now;
            //        fieldexecutivefirebaseupdation.vehicle_id = vehicle_id;

            //        db.Fieldexecutivefirebaseupdations.Add(fieldexecutivefirebaseupdation);
            //    }
            //}
            //else if (action == "Cancel Appointment")
            //{
            //    long insAgentId = db.appointmentbookeds.FirstOrDefault(m => m.appointmentId == oldappId).insuranceAgent_insuranceAgentId ?? default(long);

            //    if (insAgentId != 0)
            //    {
            //        fieldexecutivefirebaseupdation fieldexecutivefirebaseupdation = new fieldexecutivefirebaseupdation();

            //        fieldexecutivefirebaseupdation = db.Fieldexecutivefirebaseupdations.FirstOrDefault(m => m.vehicle_id == vehicle_id);

            //        if (fieldexecutivefirebaseupdation != null)
            //        {

            //            fieldexecutivefirebaseupdation.agentId = insAgentId;
            //            db.Fieldexecutivefirebaseupdations.AddOrUpdate(fieldexecutivefirebaseupdation);
            //        }
            //    }

            //}
            #endregion
            db.SaveChanges();
            return true;
        }


        #endregion

        /// <summary>
        /// Function used for sending fe app interaction data stored in fehistorydata table
        /// data(s) are taking by vehicle id;
        /// </summary>
        /// <returns>Json array of data</returns>
        public ActionResult getFEInteractionData()
        {
            string exception = "";
            long vehicalId = Convert.ToInt32(Session["VehiId"].ToString());
            List<fefirebasehistorydata> feDataList = new List<fefirebasehistorydata>();
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            int maxCount = 0;
            try
            {
                using (var db = new AutoSherDBContext())
                {

                    maxCount = db.feFirebaseHistoryData.Count(m => m.vehicleId == vehicalId);

                    List<fefirebasehistorydata> feData = db.feFirebaseHistoryData.Where(m => m.vehicleId == vehicalId).OrderBy(m => m.id).Skip(start).Take(length).ToList();

                    if (feData != null && feData.Count > 0)
                    {
                        foreach (var interaction in feData)
                        {
                            fefirebasehistorydata data = new fefirebasehistorydata();

                            if (interaction.insassign_id != 0)
                            {
                                data.InteractionId = "ASSIGN" + interaction.insassign_id.ToString();
                            }
                            else if (interaction.inspolicydrop_id != 0)
                            {
                                data.InteractionId = "INSPD" + interaction.inspolicydrop_id.ToString();
                            }
                            else
                            {
                                data.InteractionId = "ASSIGN" + db.insuranceassignedinteractions.FirstOrDefault(m => m.vehicle_vehicle_id == vehicalId).id.ToString();
                            }

                            data.creName = interaction.creName;
                            data.AgentName = db.wyzusers.FirstOrDefault(m => m.id == interaction.userId).userName;
                            data.actionDate = interaction.actionDate;
                            data.appointmentType = interaction.appointmentType;
                            data.status = interaction.status;


                            if (interaction.disposition == 25)
                            {
                                data.Disposition = "Rescheduled";
                                data.Details = interaction.aptScheduledDate + " | " + interaction.appointmentTime;
                            }
                            else if (interaction.disposition == 23)
                            {
                                data.Disposition = "Not Interested";
                                data.Details = interaction.reason;
                            }
                            else if (interaction.disposition == 6 || interaction.disposition == 8 || interaction.disposition == 9 || interaction.disposition == 10)
                            {
                                data.Disposition = "Not Answered";
                                data.Details = interaction.reason;
                            }
                            else if (interaction.disposition == 35)
                            {
                                data.Disposition = "Cancelled";
                                data.Details = "-";
                            }
                            else if (interaction.disposition == 40)
                            {
                                data.Disposition = "Dropped By FE";
                                data.Details = interaction.reason == null ? "" : interaction.reason;
                            }
                            else if (interaction.disposition == 39)
                            {
                                data.Disposition = "Success";
                                data.Details = interaction.paymentType + " | " + interaction.paymentReference;

                            }
                            else
                            {
                                data.Disposition = "-";
                                data.Details = "-";
                            }
                            data.comments = interaction.comments;

                            feDataList.Add(data);
                        }
                    }
                    else
                    {
                        feDataList = new List<fefirebasehistorydata>();
                    }
                }


                return Json(new { data = feDataList, draw = Request["draw"], recordsTotal = maxCount, recordsFiltered = maxCount, exception = exception }, JsonRequestBehavior.AllowGet);


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
                if (ex.StackTrace.Contains(':'))
                {
                    exception += ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)];
                }
                return Json(new { data = feDataList, draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0, exception = exception }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Function to save PolicyDrop disposition
        /// </summary>
        /// <param name="callLogging">Viewmodel contains required policydrop data</param>
        /// <returns></returns>
        public ActionResult savePolicyDrop(Dictionary<string, string> disctData)
        {
            try
            {
                inspolicydrop insPolicy = new inspolicydrop();

                if (disctData != null)
                {
                    insPolicy.address = disctData["address"];
                    insPolicy.pincode = int.Parse(disctData["pincode"]);
                    insPolicy.appointment_date = Convert.ToDateTime(disctData["fielddate"]);
                    insPolicy.location_id = long.Parse(disctData["locId"]);
                    insPolicy.agent_id = long.Parse(disctData["agentId"]);
                }
                else
                {
                    return Json(new { success = false, exception = "Empty request,couldn't book..." });
                }

                long custId = long.Parse(Session["CusId"].ToString());
                long vehiId = long.Parse(Session["VehiId"].ToString());
                long UserId = long.Parse(Session["UserId"].ToString());
                string username = Session["UserName"].ToString();
                using (var db = new AutoSherDBContext())
                {
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {

                            long to, oldAgentId = 0;

                            to = (long.Parse(disctData["endTime"]));
                            //from = (long.Parse(disctData["startTime"]) - 1);

                            //bookingdatetime bookingFrom = db.bookingdatetimes.FirstOrDefault(x => x.id == from);
                            bookingdatetime bookingTo = db.bookingdatetimes.FirstOrDefault(x => x.id == to);

                            insPolicy.appointment_time = bookingTo.timeRange;

                            insPolicy.wyzuser_id = UserId;
                            insPolicy.vehicle_id = vehiId;
                            insPolicy.lastagent_id = oldAgentId;
                            insPolicy.customer_id = custId;
                            insPolicy.wyzuser_name = username;
                            insPolicy.updated_datetime = DateTime.Now;

                            insPolicy.creRemarks = disctData["creRemarks"];
                            insPolicy.custRemarks = disctData["custRemakrs"];


                            db.insPolicyDrop.Add(insPolicy);
                            db.SaveChanges();

                            if (Session["DealerCode"].ToString() == "INDUS" || Session["DealerCode"].ToString() == "INDUSFE" || Session["DealerCode"].ToString() == "PODDARCARWORLD" || Session["DealerCode"].ToString() == "PLATINUMMOTOCORP")
                            {
                                fieldFirbaseUpdation(db, "PolicyDrop", 0, insPolicy.id, vehiId);
                            }


                            dbTrans.Commit();

                            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                        }
                        catch (Exception ex)
                        {
                            dbTrans.Rollback();
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

                            return Json(new { success = false, exception }, JsonRequestBehavior.AllowGet);
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

                return Json(new { success = false, exception }, JsonRequestBehavior.AllowGet);

            }
        }


        /// <summary>
        /// Used to get policyDrop disposition interaction data based on vehicle_id
        /// </summary>
        /// <returns></returns>
        public ActionResult getPolicyDropInteraction()
        {
            string exception = "";
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            int maxCount = 0;
            List<inspolicydrop> policyDropList = new List<inspolicydrop>();
            try
            {

                long vehiId = long.Parse(Session["VehiId"].ToString());
                using (var db = new AutoSherDBContext())
                {
                    maxCount = db.insPolicyDrop.Count(m => m.vehicle_id == vehiId);

                    if (maxCount < length)
                    {
                        length = maxCount;
                    }

                    policyDropList = db.insPolicyDrop.Where(m => m.vehicle_id == vehiId).OrderByDescending(m => m.id).Skip(start).Take(length).ToList();

                    foreach (var policy in policyDropList)
                    {
                        if (policy.location_id != null && policy.location_id != 0)
                        {
                            policy.LocationName = db.locations.FirstOrDefault(m => m.cityId == policy.location_id).name;
                        }

                        if (policy.agent_id != null && policy.agent_id != 0)
                        {
                            policy.AgentName = db.insuranceagents.FirstOrDefault(m => m.insuranceAgentId == policy.agent_id).insuranceAgentName;
                        }

                    }
                }
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

            }

            if (policyDropList != null)
            {
                return Json(new { data = policyDropList, draw = Request["draw"], recordsTotal = maxCount, recordsFiltered = policyDropList.Count, exception = exception }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { data = policyDropList, draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0, exception = exception }, JsonRequestBehavior.AllowGet);
            }

        }

        #region INFOBIP Whatsapp functions

        /// <summary>
        /// Used to get sms template name based on sms selected
        /// </summary>
        /// <param name="val">sms or infoBip whatsapp templates</param>
        /// <returns></returns>
        public ActionResult getSmsType(string val)
        {
            try
            {
                if (val == "SMS")
                {
                    string dispoType = Session["LoginUser"].ToString();
                    List<smstemplate> smsType = getSMSTemplate(dispoType);
                    return Json(new { success = true, data = smsType, type = "sms" }, JsonRequestBehavior.AllowGet);
                }
                else if (val == "WhatsApp")
                {
                    using (AutoSherDBContext db = new AutoSherDBContext())
                    {
                        long? modtype = Convert.ToInt64(Session["UserRole1"]);
                        var smsType = db.smstemplates.Where(x => x.isWhatsapp == true && x.inActive == false && (x.moduletype == 5 || x.moduletype == modtype)).Select(x => new { smsId = x.smsId, smsType = x.smsType }).ToList();
                        return Json(new { success = true, data = smsType, type = "whatsApp" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                string exception = "";
                if (ex.Message.Contains("inner exception"))
                {
                    exception = ex.InnerException.Message;
                }
                else
                {
                    exception = ex.Message;
                }
                return Json(new { success = false, error = exception }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to get selected infoBip template and template parameters
        /// </summary>
        /// <param name="value">template id</param>
        /// <returns></returns>
        public ActionResult getWhatsappTemplate(string value)
        {
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    long smsId = long.Parse(value);
                    string smsTemp = db.smstemplates.FirstOrDefault(x => x.smsId == smsId).smsTemplate1;
                    whatsappParams parameters = db.WhatsappParams.FirstOrDefault(x => x.smsId == smsId);
                    return Json(new { success = true, smsData = smsTemp, smsParams = parameters }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string exception = "";
                if (ex.Message.Contains("inner exception"))
                {
                    exception = ex.InnerException.Message;
                }
                else
                {
                    exception = ex.Message;
                }
                return Json(new { success = false, error = exception }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Used to send/Call infoBip api with predefined parameters
        /// </summary>
        /// <param name="paraDisct">Disctionary consisting of parameter list and othet required data sent from front end</param>
        /// <returns></returns>
        public ActionResult sendInfoBipWhatsapp(string paraDisct)
        {
            try
            {
                Logger logger = LogManager.GetLogger("apkRegLogger");
                Dictionary<string, string> parameterList = JsonConvert.DeserializeObject<Dictionary<string, string>>(paraDisct);

                long custId = long.Parse(Session["CusId"].ToString());
                long vehiId = long.Parse(Session["VehiId"].ToString());
                long UserId = long.Parse(Session["UserId"].ToString());
                string UserName = Session["UserName"].ToString();

                string phNum = parameterList["custNum"];
                string smsId = parameterList["smsId"];
                string template_name = parameterList["template_name"];
                string template_message = parameterList["message"];
                long maxParaCount = long.Parse(parameterList["maxPara"]);

                using (var db = new AutoSherDBContext())
                {
                    var infoBipData = db.Infobipcredentials.Select(m => new { base_url = m.infoBip_baseurl, info_key = m.infoBip_key, username = m.infoBip_username, password = m.infoBip_password }).FirstOrDefault();

                    if (!string.IsNullOrEmpty(infoBipData.base_url) && !string.IsNullOrEmpty(infoBipData.info_key) && !string.IsNullOrEmpty(infoBipData.username) && !string.IsNullOrEmpty(infoBipData.password))
                    {
                        WhatsAppInfoBip requestData = new WhatsAppInfoBip();

                        requestData.scenarioKey = infoBipData.info_key;

                        phNum = phNum.Trim();

                        if (phNum.Length < 10)
                        {
                            return Json(new { success = false, exception = "Invalid Mobile number" });
                        }

                        if (phNum.Length == 10)
                        {
                            phNum = "91" + phNum;
                        }

                        Destination destination = new Destination();
                        destination.to = new To();
                        destination.to.phoneNumber = phNum;

                        requestData.destinations = new List<Destination>();
                        requestData.destinations.Add(destination);

                        dynamic creDetails = new JObject();

                        string creName = string.Empty;
                        creDetails.CRE = UserName;

                        requestData.callbackData = JsonConvert.SerializeObject(creDetails);
                        requestData.whatsApp = new WhatsApp();
                        requestData.whatsApp.templateName = template_name;

                        List<string> templateData = new List<string>();

                        for (int i = 1; i <= maxParaCount; i++)
                        {
                            templateData.Add(parameterList[i.ToString()]);
                        }

                        requestData.whatsApp.mediaTemplateData.body.placeholders.AddRange(templateData);

                        //requestData.whatsApp.templateData.AddRange(templateData);
                        requestData.whatsApp.language = "en";

                        WebRequest request = WebRequest.Create(infoBipData.base_url);
                        var httprequest = (HttpWebRequest)request;

                        httprequest.PreAuthenticate = true;
                        httprequest.Method = "POST";
                        httprequest.ContentType = "application/json";


                        string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(infoBipData.username + ":" + infoBipData.password));
                        httprequest.Headers.Add("Authorization", "Basic " + encoded);
                        httprequest.Accept = "application/json";

                        using (var streamWriter = new StreamWriter(httprequest.GetRequestStream()))
                        {
                            var bodyContent = JsonConvert.SerializeObject(requestData);
                            streamWriter.Write(bodyContent);

                            streamWriter.Flush();
                            streamWriter.Close();

                            logger.Info("Sending InfoBip: \n" + bodyContent + "\n On " + DateTime.Now);
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

                        InfoBipResponse responseData = new InfoBipResponse();
                        string message;
                        if (response_string.Contains("message"))
                        {
                            responseData = JsonConvert.DeserializeObject<InfoBipResponse>(response_string);
                        }

                        logger.Info("InfoBip whatsApp Response:\n" + response_string + "\n On " + DateTime.Now);
                        if (responseData != null)
                        {
                            message = "(G-" + responseData.messages[0].status.groupId + ",I-" + responseData.messages[0].status.id + ")" + responseData.messages[0].status.description;
                        }
                        else
                        {
                            return Json(new { success = false, exception = "Unrecorded response from api.." });
                        }



                        smsinteraction smsinteraction = new smsinteraction();

                        smsinteraction.interactionDate = DateTime.Now.ToString("dd-MM-yyyy");
                        smsinteraction.interactionDateAndTime = DateTime.Now;
                        smsinteraction.interactionTime = DateTime.Now.ToString("HH:mm:ss");
                        smsinteraction.interactionType = "Whatsapp Msg";
                        smsinteraction.responseFromGateway = response_string;
                        smsinteraction.customer_id = custId;
                        smsinteraction.vehicle_vehicle_id = vehiId;
                        smsinteraction.wyzUser_id = UserId;
                        smsinteraction.mobileNumber = phNum;
                        smsinteraction.smsType = smsId;
                        //smsinteraction.smsMessage = message;
                        smsinteraction.isAutoSMS = false;
                        smsinteraction.smsStatus = true;
                        smsinteraction.reason = message;
                        smsinteraction.smsMessage = template_message;
                        //if (response_string.Contains(parameter.sucessStatus))
                        //{
                        //    smsinteraction.smsStatus = true;
                        //    smsinteraction.reason = "Send Successfully";
                        //}
                        //else
                        //{
                        //    smsstatu status = new smsstatu();

                        //    //response_string = "200";

                        //    status = db.smsstatus.FirstOrDefault(m => response_string.Contains(m.code));
                        //    if (status == null)
                        //    {
                        //        smsinteraction.smsStatus = false;
                        //        smsinteraction.reason = "Sending Failed";
                        //    }
                        //    else if (status != null)
                        //    {
                        //        smsinteraction.smsStatus = false;
                        //        smsinteraction.reason = status.description;
                        //    }
                        //}
                        db.smsinteractions.Add(smsinteraction);
                        db.SaveChanges();
                        return Json(new { success = true, message });
                    }
                    else
                    {
                        return Json(new { success = false, exception = "Base Url/Scenario Key not found" });
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

                return Json(new { success = false, exception });

            }
        }

        #endregion
        //AKASH 
        public ActionResult getpmsdetails(string city, string model, string mileage, string fueltype)
        {
            long citys = Convert.ToInt64(city);
            long models = Convert.ToInt64(model);
            long mileages = Convert.ToInt64(mileage);
            // int fueltypes = Convert.ToInt32(fueltype);

            string exception = "";
            try
            {
                using (var db = new AutoSherDBContext())
                {

                    var pmsdetails = db.Pmslabours.Where(m => m.cityid == citys && m.modelid == models && m.mileageid == mileages && m.fuelType == fueltype).Select(m => new { m.wheelAlignment, m.wheelBalancing, m.engineOil, m.oilFillter, m.brakeFluid, m.coolant, m.sparkPlug, m.airFilter, m.fuelFilter, m.belt, m.basic, m.hygiene, m.gasketDrainPlug, m.oilFilterGasket, m.clutchFluid, m.transmissionOil }).FirstOrDefault();
                    return Json(new { success = true, data = pmsdetails }, JsonRequestBehavior.AllowGet);


                }
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

                return Json(new { success = false, error = exception }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult getpmsMileage(int modelid)
        {
            string exception = "";
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    var Mileage = db.Pmsmileages.Where(m => m.modelid == modelid).Select(m => new { id = m.id, mileage = m.mileage }).ToList();
                    return Json(new { success = true, data = Mileage }, JsonRequestBehavior.AllowGet);

                }
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

                return Json(new { success = false, error = exception }, JsonRequestBehavior.AllowGet);
            }

        }

        #region Driver Allocation and saving
        /// <summary>
        /// This function is get to time and driver details for pickUp and drop driver allocation
        /// </summary>
        /// <param name="date">Date of PickUp</param>
        /// <param name="workshopId">Workshop Selected for booking</param>
        /// <returns>ViewModel which contains booking times,driver details and already allocated driver slot</returns>
        public ActionResult getDriversAndTimmingList(string date, long workshopId)
        {
            try
            {
                long userId = long.Parse(Session["UserId"].ToString());

                using (var db = new AutoSherDBContext())
                {
                    if (workshopId != 0)
                    {
                        List<long> wyzusrIdOfUserWorkshop = db.userworkshops.Where(m => m.workshopList_id == workshopId).Select(m => m.userWorkshop_id).ToList();
                        List<long> driverWyzIdList = db.wyzusers.Where(m => wyzusrIdOfUserWorkshop.Contains(m.id) && m.role == "Driver").Select(m => m.id).ToList();

                        var driverList = db.drivers.Where(m => driverWyzIdList.Contains(m.wyzUser_id ?? default(long)) && m.isactive == true).Select(m => new { id = m.id, name = m.driverName }).ToList();

                        if (driverList != null && driverList.Count() > 0)
                        {
                            //List<PickupDropDataOnTabLoad> bookedDriverSlot = new List<PickupDropDataOnTabLoad>();

                            DateTime reqDate = Convert.ToDateTime(date);

                            //var bookedDetails = db.servicebookeds.Where(m => m.serviceBookStatus_id != 35 && m.driver_id != null && m.servicepickupdate == reqDate && m.pickupDrop_id != null).Select(m => new { driverId = m.driver_id, pickDropid = m.pickupDrop_id }).ToList();

                            //List<pickupdrop> pickUpList = new List<pickupdrop>();

                            //if (bookedDetails != null && bookedDetails.Count() > 0)
                            //{
                            //    List<long> pickIdsList = bookedDetails.Select(m => m.pickDropid ?? default(long)).ToList();
                            //    pickUpList = db.pickupdrops.Where(p => pickIdsList.Contains(p.id)).ToList();

                            //    foreach (var slots in pickUpList)
                            //    {
                            //        PickupDropDataOnTabLoad timetabs = new PickupDropDataOnTabLoad();
                            //        timetabs.DriverId = slots.driver_id ?? default(long);
                            //        timetabs.StartTime = slots.pickupTime;

                            //        bookedDriverSlot.Add(timetabs);
                            //    }

                            //}

                            var bookedDriverSlot = db.driverBookingDetails.Where(m => m.BookingDate == reqDate).Select(m => new { DriverId = m.Driver_Id, StartTime = m.BookingTime }).ToList();

                            var timingList = db.driverbookingdatetimes.Select(m => new { id = m.id, m.timeRange, fromTime = m.startTime.ToString(), endTime = m.endTime.ToString() }).ToList();
                            return Json(new { success = true, driverList, timingList, bookedSlots = bookedDriverSlot });
                        }
                        else
                        {
                            return Json(new { success = false, exception = "No Drivers found for workshop..." });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, exception = "Workshop name not provided..." });
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
                if (ex.StackTrace.Contains(':'))
                {
                    exception = "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)] + " " + exception;
                }
                return Json(new { success = false, exception });
            }
        }

        /// <summary>
        /// Function to save driver scheduling details
        /// </summary>
        /// <param name="BookingDate">Booking Date</param>
        /// <param name="BookingTime">Booking Time</param>
        /// <param name="PickUpDrop">PickUp Drop, 1->PickUp , 2->Drop, 3->PickUpDrop</param>
        /// <param name="ServiceAdvisor">SA Name</param>
        /// <param name="BookingType">Booking Type(pickUp and Drop Only) or Custome Allocation, </param>
        /// <param name="PickUpAddress">PickUp address</param>
        /// <param name="DropAddress">Drop Address</param>
        /// <param name="ScheduledBy">WyzUserId who schedule driver(can be CRE or CREManager)</param>
        /// <param name="Vehicle_Id">Vehicle Id</param>
        /// <param name="Customer_Id">Customer Id</param>
        /// <param name="Driver_Id">Allocating Drivers DriverTable Id</param>
        /// <param name="LastAllcatedDriver_Id">Last Allocated Driver's DriverTable Id</param>
        /// <param name="UniqueKey">Unique Id(Prefferble GUID) if Customer booking else null</param>
        /// <param name="ServiceBookedId">Service Booking Table Id</param>
        /// /// <param name="TimeRange">Booked Time Range</param>
        /// <param name="db">DbContext reference</param>
        /// <returns></returns>
        public long AssignDriverAndBook(DateTime BookingDate, string BookingTime, long PickUpDrop, string BookingType,
            string PickUpAddress, string DropAddress, long ScheduledBy, long Vehicle_Id, long Customer_Id, long? Driver_Id,
            long? LastAllcatedDriver_Id, string UniqueKey, string TimeRange, long? workshopId, long serviceBookedId, AutoSherDBContext db)
        {
            DriverBookingDetails bookingDetails = new DriverBookingDetails();

            List<long?> CustomerId = db.driverBookingDetails.Select(m => m.Customer_Id).ToList();
            if (db.dealers.FirstOrDefault().dealerCode == "POPULARHYUNDAI" && CustomerId.Contains(Customer_Id))
            {
                DriverBookingDetails DriverBookingDetails = db.driverBookingDetails.FirstOrDefault(m => m.Customer_Id == Customer_Id);
                DriverBookingDetails.BookingDate = BookingDate;
                DriverBookingDetails.BookingTime = BookingTime;
                DriverBookingDetails.ScheduledBy = ScheduledBy;
                DriverBookingDetails.LastAllocatedDriver = LastAllcatedDriver_Id;
                DriverBookingDetails.TimeRage = TimeRange;
                return DriverBookingDetails.id;
            }
            else
            {
                bookingDetails.BookingDate = BookingDate;
                bookingDetails.BookingTime = BookingTime;
                bookingDetails.ScheduledBy = ScheduledBy;
                bookingDetails.PickUpAddress = PickUpAddress;
                bookingDetails.Vehicle_Id = Vehicle_Id;
                bookingDetails.Customer_Id = Customer_Id;
                bookingDetails.Driver_Id = Driver_Id;
                bookingDetails.BookingType = BookingType;
                bookingDetails.PickUpDrop = PickUpDrop;
                bookingDetails.DropAddress = DropAddress;
                bookingDetails.LastAllocatedDriver = LastAllcatedDriver_Id;
                bookingDetails.TimeRage = TimeRange;
                bookingDetails.UniqueKey = UniqueKey;
                bookingDetails.workshop_id = workshopId;
                bookingDetails.serviceBookedId = serviceBookedId;
                db.driverBookingDetails.Add(bookingDetails);
                db.SaveChanges();

            }
            return bookingDetails.id;
        }

        /// <summary>
        /// This function used for saving driver details for scheduler to push into firebase db
        /// </summary>
        /// <param name="disposition"></param>
        /// <param name="vehicle_id"></param>
        /// <param name="driver_id"></param>
        /// <param name="db"></param>
        /// <param name="driverBookingdetails_id"></param>
        /// <param name="customer_id"></param>
        public void allocateDriver(string disposition, long vehicle_id, string UniqueKey, long driver_id, AutoSherDBContext db, long driverBookingdetails_id, long customer_id, int PickUpDropType, string SchedulingType, long UserId)
        {
            long? lastDriverId = null;
            string lastFirebaseKey = null;
            driverscheduler maxData = null;

            if (SchedulingType == "Booking")
            {
                maxData = db.driverSchedulers.Where(m => m.vehicle_id == vehicle_id && m.PickUpDrop == PickUpDropType).OrderByDescending(m => m.id).FirstOrDefault();
            }
            else if (SchedulingType == "Custom")
            {
                db.driverSchedulers.Where(m => m.uniquekey == UniqueKey && m.PickUpDrop == PickUpDropType).FirstOrDefault();
            }

            if (maxData != null)
            {

                if (maxData.ispushed == false)
                {

                    DriverBookingDetails LastBookingDetails = db.driverBookingDetails.FirstOrDefault(m => m.id == maxData.driverBookingdetails_id);

                    if (LastBookingDetails != null)
                    {
                        db.driverBookingDetails.Remove(LastBookingDetails);
                        db.SaveChanges();
                    }

                    db.driverSchedulers.Remove(maxData);
                    db.SaveChanges();
                }
                else
                {
                    lastDriverId = maxData.driver_id;
                    lastFirebaseKey = maxData.firebasekey;
                }
            }

            if (disposition == "Book My Service")
            {
                driverscheduler newScheduling = new driverscheduler();

                newScheduling.vehicle_id = vehicle_id;
                newScheduling.customer_id = customer_id;
                newScheduling.driverBookingdetails_id = driverBookingdetails_id;

                newScheduling.driver_id = driver_id;
                newScheduling.lastdriver_id = lastDriverId;
                newScheduling.scheduledBy = UserId;
                newScheduling.updatedOn = DateTime.Now;
                newScheduling.firebasekey = lastFirebaseKey;
                newScheduling.PickUpDrop = PickUpDropType;

                db.driverSchedulers.Add(newScheduling);

            }
            else if (disposition == "Rescheduled")
            {

               // if (db.dealers.FirstOrDefault().dealerCode == "KUNHYUNDAI")
                 if (db.dealers.FirstOrDefault().dealerCode == "POPULARHYUNDAI")
                {
                    driverscheduler newScheduling = new driverscheduler();
                    newScheduling = db.driverSchedulers.FirstOrDefault(m => m.firebasekey == maxData.firebasekey);


                    newScheduling.vehicle_id = vehicle_id;
                    newScheduling.customer_id = customer_id;
                    newScheduling.driverBookingdetails_id = driverBookingdetails_id;

                    newScheduling.driver_id = driver_id;
                    newScheduling.lastdriver_id = lastDriverId;
                    newScheduling.scheduledBy = UserId;
                    newScheduling.updatedOn = DateTime.Now;
                    //newScheduling.firebasekey = lastFirebaseKey;
                    newScheduling.PickUpDrop = PickUpDropType;
                    newScheduling.ispushed = false;

                    db.SaveChanges();
                }

                 else if(db.dealers.FirstOrDefault().dealerCode == "KUNHYUNDAI")
                {
                    maxData.IsCancelled = true;
                db.driverSchedulers.AddOrUpdate(maxData);
                    var newScheduling =  new driverscheduler();
                    newScheduling.vehicle_id = maxData.vehicle_id;
                    newScheduling.customer_id=maxData.customer_id;
                    newScheduling.driverBookingdetails_id = driverBookingdetails_id;

                    newScheduling.driver_id = driver_id;
                    newScheduling.lastdriver_id = lastDriverId;
                    newScheduling.scheduledBy = UserId;
                    newScheduling.updatedOn = DateTime.Now;
                    newScheduling.firebasekey = lastFirebaseKey;
                    newScheduling.PickUpDrop = PickUpDropType;
                    newScheduling.firebasekey = null;
                    newScheduling.IsCancelled = false;
                    newScheduling.ispushed = false;
                    db.driverSchedulers.Add(newScheduling);
                    if (maxData != null)
                    {
                        DriverBookingDetails LastBookingDetails = db.driverBookingDetails.FirstOrDefault(m => m.id == maxData.driverBookingdetails_id);
                        if (LastBookingDetails != null)
                        {
                            db.driverBookingDetails.Remove(LastBookingDetails);
                        }
                    }
                }

                else
                {
                    driverscheduler newScheduling = new driverscheduler();

                    newScheduling.vehicle_id = vehicle_id;
                    newScheduling.customer_id = customer_id;
                    newScheduling.driverBookingdetails_id = driverBookingdetails_id;

                    newScheduling.driver_id = driver_id;
                    newScheduling.lastdriver_id = lastDriverId;
                    newScheduling.scheduledBy = UserId;
                    newScheduling.updatedOn = DateTime.Now;
                    newScheduling.firebasekey = lastFirebaseKey;
                    newScheduling.PickUpDrop = PickUpDropType;


                    db.driverSchedulers.Add(newScheduling);
                    if (maxData != null)
                    {
                        DriverBookingDetails LastBookingDetails = db.driverBookingDetails.FirstOrDefault(m => m.id == maxData.driverBookingdetails_id);
                        if (LastBookingDetails != null)
                        {
                            db.driverBookingDetails.Remove(LastBookingDetails);
                        }
                    }
                }

            }

            /* if (db.dealers.FirstOrDefault().dealerCode == "POPULARHYUNDAI")
               {

                   driverscheduler newScheduling = new driverscheduler();
                   newScheduling = db.driverSchedulers.FirstOrDefault(m => m.firebasekey == maxData.firebasekey);

                   newScheduling.vehicle_id = vehicle_id;
                   newScheduling.customer_id = customer_id;
                   newScheduling.driverBookingdetails_id = driverBookingdetails_id;

                   newScheduling.driver_id = db.driverBookingDetails.where(m => m.Customer_Id == customer_id).Driver_Id;
                   newScheduling.lastdriver_id = lastDriverId;
                   newScheduling.scheduledBy = UserId;
                   newScheduling.updatedOn = DateTime.Now;
                   newScheduling.firebasekey = lastFirebaseKey;
                   newScheduling.ispushed = false;
                   newScheduling.IsCancelled = true;

                   db.SaveChanges();


               }*/
            else if (disposition == "Cancelled")
            {

                //driverscheduler newScheduling = new driverscheduler();

                //newScheduling.vehicle_id = vehicle_id;
                //newScheduling.customer_id = customer_id;
                //newScheduling.driverBookingdetails_id = driverBookingdetails_id;

                //newScheduling.driver_id = driver_id;
                //newScheduling.lastdriver_id = lastDriverId;
                //newScheduling.scheduledBy = UserId;
                //newScheduling.updatedOn = DateTime.Now;
                //newScheduling.firebasekey = lastFirebaseKey;
                //newScheduling.PickUpDrop = PickUpDropType;

                //newScheduling.IsCancelled = true;
                //db.driverSchedulers.Add(newScheduling);
                maxData.IsCancelled = true;
                db.driverSchedulers.AddOrUpdate(maxData);
                    if (maxData != null)
                    {
                        DriverBookingDetails LastBookingDetails = db.driverBookingDetails.FirstOrDefault(m => m.id == maxData.driverBookingdetails_id);
                        if (LastBookingDetails != null)
                        {
                            db.driverBookingDetails.Remove(LastBookingDetails);
                        }
                    }
                
            }

            db.SaveChanges();
        }


        public ActionResult getDriverAppInteraction()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            //string searchPattern = Request["search[value]"];
            string exception = "";
            int maxCount = 0;
            List<DriverAppInteraction> appInteraction = new List<DriverAppInteraction>();
            try
            {
                long vehiId = long.Parse(Session["VehiId"].ToString());
                using (var db = new AutoSherDBContext())
                {


                    maxCount = db.driverAppInteraction.Where(m => m.Vehicle_Id == vehiId).OrderByDescending(m => m.LastUpdatedOn).GroupBy(m => new { m.Vehicle_Id, m.IsDrop, m.IsPickUp }).Count();

                    if (maxCount < length)
                    {
                        length = maxCount;
                    }

                    List<long> appIds = db.driverAppInteraction.Where(m => m.Vehicle_Id == vehiId).GroupBy(m => new { m.Vehicle_Id, m.IsDrop, m.IsPickUp })
                        .Select(m => m.Max(u => u.Id)).OrderByDescending(m => m).Skip(start).Take(length).ToList();

                    if (appIds != null && appIds.Count() > 0)
                    {
                        appInteraction = db.driverAppInteraction.Where(m => appIds.Contains(m.Id)).ToList();

                        for (int i = 0; i < appInteraction.Count(); i++)
                        {
                            long driverId = appInteraction[i].DriverScheduler_Id;

                            appInteraction[i].CREName = db.wyzusers.FirstOrDefault(m => m.id == db.driverSchedulers.FirstOrDefault(u => u.id == driverId).scheduledBy).userName;
                            appInteraction[i].DriverName = db.drivers.FirstOrDefault(m => m.id == db.driverSchedulers.FirstOrDefault(u => u.id == driverId && m.isactive == true).driver_id).driverName;

                            if (!string.IsNullOrEmpty(appInteraction[i].DriverAppFiles_Ids))
                            {
                                List<long> FileIdsList = appInteraction[i].DriverAppFiles_Ids.Split(',').Select(long.Parse).ToList();

                                var appFiles = db.driverAppFileDetails.Where(m => FileIdsList.Contains(m.Id)).Select(m => new { m.FileName, m.FilePath }).ToList();

                                if (appFiles != null)
                                {
                                    appInteraction[i].DictFilesList = new List<Dictionary<string, string>>();
                                    foreach (var file in appFiles)
                                    {
                                        Dictionary<string, string> disctFile = new Dictionary<string, string>();
                                        disctFile.Add(file.FileName, file.FilePath);
                                        appInteraction[i].DictFilesList.Add(disctFile);
                                    }
                                }
                            }
                        }
                    }
                }
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
                if (ex.StackTrace.Contains(':'))
                {
                    exception = "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)] + " " + exception;
                }
            }

            return Json(new { data = appInteraction, draw = Request["draw"], recordsTotal = maxCount, recordsFiltered = maxCount, exception = exception }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region ORAI Whatsapp Integration
        public ActionResult sendORAIWhatsapp(string paraDisct)
        {

            try
            {
                Logger logger = LogManager.GetLogger("apkRegLogger");
                Dictionary<string, string> parameterList = JsonConvert.DeserializeObject<Dictionary<string, string>>(paraDisct);

                long custId = long.Parse(Session["CusId"].ToString());
                long vehiId = long.Parse(Session["VehiId"].ToString());
                long UserId = long.Parse(Session["UserId"].ToString());
                string UserName = Session["UserName"].ToString();

                string phNum = parameterList["custNum"];
                string smsId = parameterList["smsId"];
                string template_name = parameterList["template_name"];
                string template_message = parameterList["message"];
                long maxParaCount = long.Parse(parameterList["maxPara"]);

                using (var db = new AutoSherDBContext())
                {
                    //var infoBipData = db.dealers.Select(m => new { base_url = m.infoBip_baseurl, info_key = m.infoBip_key, username = m.infoBip_username, password = m.infoBip_password }).FirstOrDefault();

                    //if (!string.IsNullOrEmpty(infoBipData.base_url) && !string.IsNullOrEmpty(infoBipData.info_key) && !string.IsNullOrEmpty(infoBipData.username) && !string.IsNullOrEmpty(infoBipData.password))
                    {
                        phNum = phNum.Trim();

                        if (phNum.Length < 10)
                        {
                            return Json(new { success = false, exception = "Invalid Mobile number" });
                        }

                        if (phNum.Length == 10)
                        {
                            phNum = "91" + phNum;
                        }

                        if (Session["DealerCode"].ToString() == "BRIDGEWAYMOTORS")
                        {
                            var prutechResponse = CallPruchtecWhatsapp(parameterList);
                            return Json(new { success = true, prutechResponse });
                        }

                        if (Session["DealerCode"].ToString() == "BHANDARIAUTOMOBILE")
                        {
                            var chat360Response = CallChat360Whatsapp(parameterList);
                            return Json(new { success = true, chat360Response });
                        }


                        ORAIWhatsapp requestData = new ORAIWhatsapp();
                        requestData.From = "whatsapp:+17739853901";
                        // requestData.Body = "Dear chethan,\n Your Honda Test - whtsap   is due for service.  We were unable to reach you. Request you to visit Magnum Honda banglore for the best service Experience.\n To speak with your relationship Manager dial 002154.";
                        requestData.To = "whatsapp:+" + phNum;
                        requestData.AccountSid = "AC9c40252b3f6083bac701e5d32b3b2dc9";
                        requestData.AuthToken = "7c3829eb59e072f8609527580b8bee02";
                        requestData.Body = template_message.Replace("\\n", "\n");

                        WebRequest request = WebRequest.Create("https://e2ewebservice20190528111726.azurewebsites.net/api/ORAIWhatsappNotification");
                        var httprequest = (HttpWebRequest)request;

                        httprequest.PreAuthenticate = true;
                        httprequest.Method = "POST";
                        httprequest.ContentType = "application/json";
                        httprequest.Accept = "application/json";

                        using (var streamWriter = new StreamWriter(httprequest.GetRequestStream()))
                        {
                            var bodyContent = JsonConvert.SerializeObject(requestData);

                            streamWriter.Write(bodyContent);

                            streamWriter.Flush();
                            streamWriter.Close();

                            logger.Info("Sending InfoBip: \n" + bodyContent + "\n On " + DateTime.Now);
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


                        ORAIWhatsapp responseData = new ORAIWhatsapp();
                        string message;
                        if (response_string.Contains("body"))
                        {
                            var jsonResult = JsonConvert.DeserializeObject(response_string).ToString();
                            responseData = JsonConvert.DeserializeObject<ORAIWhatsapp>(jsonResult);
                        }

                        logger.Info("ORAI whatsApp Response:\n" + response_string + "\n On " + DateTime.Now);
                        if (responseData != null)
                        {
                            message = responseData.Body;
                        }
                        else
                        {
                            return Json(new { success = false, exception = "Unrecorded response from api.." });
                        }

                        smsinteraction smsinteraction = new smsinteraction();

                        smsinteraction.interactionDate = DateTime.Now.ToString("dd-MM-yyyy");
                        smsinteraction.interactionDateAndTime = DateTime.Now;
                        smsinteraction.interactionTime = DateTime.Now.ToString("HH:mm:ss");
                        smsinteraction.interactionType = "Whatsapp Msg";
                        smsinteraction.responseFromGateway = response_string;
                        smsinteraction.customer_id = custId;
                        smsinteraction.vehicle_vehicle_id = vehiId;
                        smsinteraction.wyzUser_id = UserId;
                        smsinteraction.mobileNumber = phNum;
                        smsinteraction.smsType = smsId;
                        //smsinteraction.smsMessage = message;
                        smsinteraction.isAutoSMS = false;
                        smsinteraction.smsStatus = true;
                        smsinteraction.reason = message;
                        smsinteraction.smsMessage = template_message;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            smsinteraction.smsStatus = true;
                            smsinteraction.reason = "Send Successfully" + response.StatusDescription;
                        }
                        else
                        {
                            smsinteraction.smsStatus = false;
                            smsinteraction.reason = "Sending Failed" + response.StatusDescription;
                        }
                        db.smsinteractions.Add(smsinteraction);
                        db.SaveChanges();
                        return Json(new { success = true, message });
                    }
                    //else
                    //{
                    //    return Json(new { success = false, exception = "Base Url/Scenario Key not found" });
                    //}
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

                return Json(new { success = false, exception });

            }

        }
        public ActionResult getORAIWhatsappTemplate(string value)
        {
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    long smsId = long.Parse(value);
                    var smstmplates = db.smstemplates.FirstOrDefault(x => x.smsId == smsId);
                    string smsTemp = smstmplates.smsTemplate1;
                    List<long> oraiparamsId = smstmplates.messageparams.Split(',').Select(long.Parse).ToList();

                    List<whatsapporaiparams> parameters = db.Whatsapporaiparams.Where(m => oraiparamsId.Contains(m.parameterid)).ToList();
                    return Json(new { success = true, smsData = smsTemp, smsParams = parameters.OrderBy(m => m.parameterid) }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string exception = "";
                if (ex.Message.Contains("inner exception"))
                {
                    exception = ex.InnerException.Message;
                }
                else
                {
                    exception = ex.Message;
                }
                return Json(new { success = false, error = exception }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getdriverdetails(string servicebookedId)
        {
            long serviceId = Convert.ToInt64(servicebookedId);
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    if (db.servicebookeds.Count(x => x.serviceBookedId == serviceId && x.driver_id != 0 && x.isPickupRequired == true) > 0)

                    {
                        long driverId = db.servicebookeds.FirstOrDefault(x => x.serviceBookedId == serviceId && x.driver_id != 0 && x.isPickupRequired == true).driver_id ?? default(long);
                        if (db.drivers.Count(m => m.id == driverId) > 0)
                        {
                            var driverdetails = db.drivers.Where(m => m.id == driverId && m.isactive == true).Select(m => new { m.driverName, m.driverPhoneNum });
                            return Json(new { success = true, drivername = driverdetails.FirstOrDefault().driverName, driverphonenumber = driverdetails.FirstOrDefault().driverPhoneNum }, JsonRequestBehavior.AllowGet);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string exception = "";
                if (ex.Message.Contains("inner exception"))
                {
                    exception = ex.InnerException.Message;
                }
                else
                {
                    exception = ex.Message;
                }
                return Json(new { success = false, error = exception }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, drivername = "", driverphonenumber = "" }, JsonRequestBehavior.AllowGet);
        }

        //public void autosmsMultipleday(long wyzId, long vehicleId, long custId, string smsType, string dispoType, int taggingid, long driverId, long departmentId, string customMsg, long workshopId,long advisorId, string phoneNumbers = null, string DealerCode = null)
        //{

        //    IScheduler autosmsMultipleScheduler = StdSchedulerFactory.GetDefaultScheduler().Result;

        //    autosmsMultipleScheduler.Start();
        //    IJobDetail jobDetail = JobBuilder.Create<multipleAutoSMSJob>().Build();
        //    ITrigger trigger = TriggerBuilder.Create().WithIdentity("DispoAutoSMSMultipleTrigger", "DispoAutoSMSMultipleGroup")
        //        .StartNow().WithSimpleSchedule().Build();


        //    autosmsMultipleScheduler.Context.Put("WyzId", wyzId);
        //    autosmsMultipleScheduler.Context.Put("vehicleId", vehicleId);
        //    autosmsMultipleScheduler.Context.Put("custId", custId);
        //    autosmsMultipleScheduler.Context.Put("smsType", smsType);
        //    autosmsMultipleScheduler.Context.Put("dispoType", dispoType);
        //    autosmsMultipleScheduler.Context.Put("taggingid", taggingid);
        //    autosmsMultipleScheduler.Context.Put("driverId", driverId);
        //    autosmsMultipleScheduler.Context.Put("advisorId", advisorId);
        //    autosmsMultipleScheduler.Context.Put("departmentId", departmentId);
        //    autosmsMultipleScheduler.Context.Put("customMsg", customMsg);
        //    autosmsMultipleScheduler.Context.Put("workshopId", workshopId);
        //    autosmsMultipleScheduler.Context.Put("phoneNumbers", phoneNumbers);
        //    if (string.IsNullOrEmpty(DealerCode))
        //    {
        //        autosmsMultipleScheduler.Context.Put("DealerCode", Session["DealerCode"].ToString());
        //    }
        //    else
        //    {
        //        autosmsMultipleScheduler.Context.Put("DealerCode", DealerCode);
        //    }

        //    autosmsMultipleScheduler.ScheduleJob(jobDetail, trigger);

        //}

        #endregion
        #region LoadCCandToEmail
        public ActionResult loadEmailToandCC()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    var emailDetails = db.workshops.Where(m => m.isinsurance == false).ToList();
                    var toEmails = emailDetails.Where(m => m.escalationMails != null).Select(m => new { m.id, m.workshopName, m.escalationMails }).OrderBy(m => m.workshopName).ToList();
                    var toCCEmails = emailDetails.Where(m => m.escalationCC != null).Select(m => new { m.id, m.workshopName, m.escalationCC }).OrderBy(m => m.workshopName).ToList();
                    return Json(new { success = true, toEmails = toEmails, toCCEmails = toCCEmails }, JsonRequestBehavior.AllowGet);
                }
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

                return Json(new { success = false, error = exception }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Coupon SAving Flow

        public bool coupons(AutoSherDBContext db, couponinteraction coupon)
        {
            db.Couponinteractions.AddOrUpdate(coupon);
            db.SaveChanges();
            return true;
        }

        public ActionResult getCouponInteraction()
        {
            string exception = "";
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    long cusId = Convert.ToInt64(Session["CusId"]);
                    long vehId = Convert.ToInt64(Session["VehiId"]);
                    string cre_name = Session["UserName"].ToString();

                    if (Session["DealerCode"].ToString() == "INDUS")
                    {
                        List<couponhistory> couponhistories = db.couponhistory.Where(m => m.vehicleid == vehId.ToString()).ToList();
                        return Json(new { success = true, data = couponhistories }, JsonRequestBehavior.AllowGet);
                    }
                    List<couponinteraction> coupons = db.Couponinteractions.Where(m => m.vehicleid == vehId).ToList();
                    return Json(new { success = true, data = coupons }, JsonRequestBehavior.AllowGet);
                }
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

                return Json(new { success = false, error = exception }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion
        #region sendEmailDynamic

        public ActionResult getDynamicEmail(int emailId)
        {
            int UserId = Convert.ToInt32(Session["UserId"].ToString());
            int vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    {
                        string emailSub = "", emailTemplate = "";
                        long? locId = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehiId).vehicleWorkshop_id;

                        var emailDetails = db.workshops.FirstOrDefault(m => m.id == locId);

                        emailSub = getEmailBodyAndSub(UserId.ToString(), vehiId.ToString(), locId.ToString(), emailId.ToString(), "0", 0, "1");
                        emailTemplate = getEmailBodyAndSub(UserId.ToString(), vehiId.ToString(), locId.ToString(), emailId.ToString(), "0", 0, "2");

                        return Json(new { success = true, emailSub, emailTemplate, ccemails = emailDetails.escalationMails });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }


        public async Task<ActionResult> sendEmailDynamic(CallLoggingViewModel callLog, IEnumerable<HttpPostedFileBase> files)
        {
            Email email = callLog.email;
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long userId = Convert.ToInt32(Session["UserId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            emailinteraction emailinter = new emailinteraction();
            string exception = string.Empty;
            string Reason = string.Empty, to = string.Empty, CC = string.Empty, from = string.Empty, baseURL = string.Empty, apiKey = string.Empty;
            bool isSendBlueAPI = false;

            using (var db = new AutoSherDBContext())
            {
                var test = db.emailcredentials.Count(m => m.isdefaultemail == true);
                if (db.emailcredentials.Count(m => m.isdefaultemail == true) > 0)
                {
                    try
                    {
                        emailcredential emailcredentials = new emailcredential();

                        string fileUploadPath = "~/UploadedFiles/" + Session["DealerCode"].ToString() + "/" + Session["UserName"].ToString() + "/EmailAttachements/";

                        emailcredentials = db.emailcredentials.FirstOrDefault(m => m.isdefaultemail == true && m.emailType == "Manual");
                        from = emailcredentials.userEmail; //From address    

                        to = email.EmailTo; //To address    
                        CC = email.EmailCC; //From address    
                        email.EmailFrom = from;

                        MailMessage message = new MailMessage(from, to);
                        if (files != null)
                        {
                            foreach (var file in files)
                            {
                                Directory.CreateDirectory(Server.MapPath(fileUploadPath));
                                string filePath = Server.MapPath(fileUploadPath + file.FileName);
                                file.SaveAs(filePath);
                                message.Attachments.Add(new Attachment(Server.MapPath(fileUploadPath + file.FileName)));
                            }
                        }
                        if (!(string.IsNullOrEmpty(CC)))
                        {
                            string[] CCId = CC.Split(',');
                            foreach (string CCEmail in CCId)
                            {
                                message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id  
                            }
                        }
                        string mailbody = email.Body;
                        message.Subject = email.Subject;
                        message.Body = mailbody;
                        message.BodyEncoding = System.Text.Encoding.UTF8;
                        message.IsBodyHtml = false;

                        SmtpClient client = new SmtpClient(emailcredentials.hostapi, Convert.ToInt32(emailcredentials.portnumber));
                        System.Net.NetworkCredential basicCredential1 = new
                        System.Net.NetworkCredential(emailcredentials.userEmail, emailcredentials.userPassword);
                        client.EnableSsl = true;
                        client.UseDefaultCredentials = false;
                        client.Credentials = basicCredential1;
                        client.Send(message);
                        Reason = "Mail Sent";

                    }


                    catch (Exception ex)
                    {
                        exception = ex.Message;
                        Reason = "Email not sent";

                    }
                    finally
                    {
                        emailinter.wyzUser_id = userId;
                        emailinter.customer_id = cusId;
                        emailinter.vehicle_id = vehiId;
                        emailinter.toEmailAddress = email.EmailTo;
                        emailinter.emailSubject = email.Subject;
                        emailinter.emailContent = email.Body;
                        emailinter.interactionDate = DateTime.Now.ToString("dd-MM-yyyy");
                        emailinter.interactionDateAndTime = DateTime.Now;
                        emailinter.interactionTime = DateTime.Now.ToString("HH:mm:ss");
                        emailinter.exceptionResponse = exception;
                        if(Reason == "Email not sent")
                        {
                            emailinter.emailStatus = false;
                        }
                        else
                        {
                            emailinter.emailStatus = true;
                        }
                       
                        emailinter.fromEmailAddress = email.EmailFrom;
                        emailinter.emailType = "Manual";
                        emailinter.reason = Reason;
                        emailinter.cc = CC;
                        db.emailinteractions.Add(emailinter);
                        db.SaveChanges();
                    }
                    return Json(new { success = true, error = Reason }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, error = "noreply@autosherpas.com Email Not Found in DB" }, JsonRequestBehavior.AllowGet);

                }
            }
        }
        #endregion




        #region sukumani service history

        public JsonResult getLabourserviceHistory(string jobCardNumber, int vehicleId)
        {
            List<servicehistory> labourServiceDetails = new List<servicehistory>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    labourServiceDetails = db.Servicehistories.Where(m => m.jobid == jobCardNumber).OrderByDescending(m => m.id).ToList();

                }

                return Json(new { success = true, data = labourServiceDetails });
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

                return Json(new { success = false, exception });
            }
        }
        #endregion

        #region view more option

        public ActionResult getAssignmentHistoryOfVehicleId_viewmore(string moduleType)
        {
            int UserId = Convert.ToInt32(Session["UserId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            List<long> takenList = new List<long>();
            List<SMSInteractionhistory> sms_int_data = new List<SMSInteractionhistory>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (moduleType == "service")
                    {


                        string campaign = "";
                        string reassign = "";
                        string reassignhistory = "";
                        string Cremangeruser = "";

                        string dealer = db.wyzusers.SingleOrDefault(m => m.id == UserId).dealerName;
                        List<assignedcallsreport_stage1> assign = db.Assignedcallsreport_Stages.Where(m => m.vehicleId == vehiId && m.moduletypeId == 1).ToList();


                        foreach (var assign_li in assign)
                        {
                            SMSInteractionhistory smseachData = new SMSInteractionhistory();

                            if (!takenList.Contains(assign_li.assignInteractionID))
                            {
                                takenList.Add(assign_li.assignInteractionID);
                                wyzuser wyz = new wyzuser();
                                string user = "";
                                if (assign_li.wyzuserId != 0)
                                {
                                    long id = assign_li.wyzuserId;
                                    user = db.wyzusers.FirstOrDefault(m => m.id == id).userName;
                                }
                                smseachData.resonforDrop = db.Database.SqlQuery<string>("select  whendeleted  from deletedstatusreport  where moduletype_id=1 and  assigned_id=@id;", new MySqlParameter("@id", assign_li.assignInteractionID)).FirstOrDefault();
                                if (smseachData.resonforDrop == null)
                                {
                                    smseachData.resonforDrop = "-";
                                }

                                assignedinteraction assignAvail = new assignedinteraction();
                                assignAvail = db.assignedinteractions.SingleOrDefault(m => m.id == assign_li.assignInteractionID && m.vehical_Id == vehiId);

                                string avail = "Removed";
                                if (assignAvail != null)
                                {
                                    avail = "Active";
                                }

                                string managerUser = "Auto";
                                bool autoassigned = db.assignedcallsreports.FirstOrDefault(m => m.assignInteractionID == assign_li.assignInteractionID).isautoassigned;
                                if (autoassigned == false)
                                {
                                    long id = assign_li.wyzuserId;
                                    Cremangeruser = db.wyzusers.FirstOrDefault(m => m.id == id).creManager;
                                    managerUser = Cremangeruser;
                                }
                                //if (assignAvail != null && assignAvail.isautoassigned == false)
                                //{
                                //    long id = assign_li.wyzuserId;
                                //    Cremangeruser = db.wyzusers.FirstOrDefault(m => m.id == id).creManager;
                                //    managerUser = Cremangeruser;
                                //}

                                callhistorycube callhistory = new callhistorycube();


                                int countcalhistory = db.callhistorycubes.Count(m => m.assignedInteraction_id == assign_li.assignInteractionID && m.vehicle_id == vehiId);
                                if (countcalhistory > 0)
                                {
                                    callhistory = db.callhistorycubes.FirstOrDefault(m => m.assignedInteraction_id == assign_li.assignInteractionID && m.vehicle_id == vehiId);
                                }

                                string lastDispo = "";
                                string serviceType = "-";
                                if (callhistory.secondary_dispostion != null)
                                {
                                    lastDispo = callhistory.secondary_dispostion;
                                }
                                if (callhistory.nextServicetype != null)
                                {
                                    serviceType = callhistory.nextServicetype;
                                }
                                long? cid = assign_li.campaignId;
                                if (cid != 0)
                                {
                                    campaign camp = db.campaigns.Where(c => c.id == cid).FirstOrDefault();
                                    campaign = camp.campaignName;
                                }


                                int countassignmentRecorrd = db.change_assignment_records.Count(m => m.assignedinteraction_id == assign_li.assignInteractionID && m.moduletypeIs == 1);

                                if (countassignmentRecorrd > 0)
                                {
                                    List<change_assignment_records> chng = db.change_assignment_records.Where(m => m.assignedinteraction_id == assign_li.assignInteractionID && m.moduletypeIs == 1).ToList();
                                    reassign = "Yes";
                                    foreach (change_assignment_records c in chng)
                                    {
                                        wyzuser w = db.wyzusers.Where(m => m.id == c.new_wyzuserId).FirstOrDefault();

                                        string d = c.updatedDate.ToString("dd/MM/yyyy");
                                        reassignhistory += w.userName + " -> " + d + "<br>";
                                    }
                                }
                                else
                                {
                                    reassign = "No";
                                    reassignhistory = "";
                                }

                                string assignid = "ASSIGNID" + assign_li.assignInteractionID;
                                smseachData.campaign = campaign;
                                smseachData.reassign = reassign;
                                smseachData.reassignhistory = reassignhistory;

                                smseachData.assignDate = assign_li.assignedDate;
                                smseachData.WyzuserName = user;
                                smseachData.reason = managerUser; //db.wyzusers.FirstOrDefault(m => m.id == assign_li.assigned_manager_id).userName;
                                smseachData.assignedId = assignid;
                                smseachData.smsMessage = lastDispo;
                                smseachData.smsType = avail;
                                smseachData.serviceTypes = serviceType;

                                // smseachData.assignedBy = db.wyzusers.FirstOrDefault(m => m.id == assign_li.assigned_manager_id).userName;

                                sms_int_data.Add(smseachData);
                            }
                        }
                    }
                    else if (moduleType == "psf")
                    {


                        string campaign = "";
                        string reassign = "";
                        string reassignhistory = "";
                        string Cremangeruser = "";

                        string dealer = db.wyzusers.SingleOrDefault(m => m.id == UserId).dealerName;
                        List<assignedcallsreport_stage1> assign = db.Assignedcallsreport_Stages.Where(m => m.vehicleId == vehiId && m.moduletypeId == 4).ToList();


                        foreach (var assign_li in assign)
                        {
                            SMSInteractionhistory smseachData = new SMSInteractionhistory();

                            if (!takenList.Contains(assign_li.assignInteractionID))
                            {
                                takenList.Add(assign_li.assignInteractionID);
                                wyzuser wyz = new wyzuser();
                                string user = "";
                                if (assign_li.wyzuserId != 0)
                                {
                                    long id = assign_li.wyzuserId;
                                    user = db.wyzusers.FirstOrDefault(m => m.id == id).userName;
                                }
                                smseachData.resonforDrop = db.Database.SqlQuery<string>("select  whendeleted  from deletedstatusreport  where moduletype_id=4 and  assigned_id=@id;", new MySqlParameter("@id", assign_li.assignInteractionID)).FirstOrDefault();
                                if (smseachData.resonforDrop == null)
                                {
                                    smseachData.resonforDrop = "-";
                                }

                                psfassignedinteraction assignAvail = new psfassignedinteraction();
                                assignAvail = db.psfassignedinteractions.SingleOrDefault(m => m.id == assign_li.assignInteractionID && m.vehicle_vehicle_id == vehiId);

                                string avail = "Removed";
                                if (assignAvail != null)
                                {
                                    avail = "Active";
                                }

                                string managerUser = "Auto";
                                //if (assignAvail != null && assignAvail.isautoassigned == false)
                                //{
                                //    long id = assign_li.wyzuserId;
                                //    Cremangeruser = db.wyzusers.FirstOrDefault(m => m.id == id).creManager;
                                //    managerUser = Cremangeruser;
                                //}
                                bool autoassigned = db.assignedcallsreports.FirstOrDefault(m => m.assignInteractionID == assign_li.assignInteractionID).isautoassigned;
                                if (autoassigned == false)
                                {
                                    long id = assign_li.wyzuserId;
                                    Cremangeruser = db.wyzusers.FirstOrDefault(m => m.id == id).creManager;
                                    managerUser = Cremangeruser;
                                }
                                psfcallhistorycube callhistory = new psfcallhistorycube();


                                int countcalhistory = db.psfcallhistorycubes.Count(m => m.psfAssignedInteraction_id == assign_li.assignInteractionID && m.vehicle_vehicle_id == vehiId);
                                if (countcalhistory > 0)
                                {
                                    callhistory = db.psfcallhistorycubes.FirstOrDefault(m => m.psfAssignedInteraction_id == assign_li.assignInteractionID && m.vehicle_vehicle_id == vehiId);
                                }

                                string lastDispo = "";
                                string serviceType = "-";
                                if (callhistory.SecondaryDisposition != null)
                                {
                                    lastDispo = callhistory.SecondaryDisposition;
                                }
                                if (callhistory.serviceType != null)
                                {
                                    serviceType = callhistory.serviceType;
                                }
                                long? cid = assign_li.campaignId;
                                if (cid != 0)
                                {
                                    campaign camp = db.campaigns.Where(c => c.id == cid).FirstOrDefault();
                                    campaign = camp.campaignName;
                                }


                                int countassignmentRecorrd = db.change_assignment_records.Count(m => m.assignedinteraction_id == assign_li.assignInteractionID && m.moduletypeIs == 4);

                                if (countassignmentRecorrd > 0)
                                {
                                    List<change_assignment_records> chng = db.change_assignment_records.Where(m => m.assignedinteraction_id == assign_li.assignInteractionID && m.moduletypeIs == 4).ToList();
                                    reassign = "Yes";
                                    foreach (change_assignment_records c in chng)
                                    {
                                        wyzuser w = db.wyzusers.Where(m => m.id == c.new_wyzuserId).FirstOrDefault();

                                        string d = c.updatedDate.ToString("dd/MM/yyyy");
                                        reassignhistory += w.userName + " -> " + d + "<br>";
                                    }
                                }
                                else
                                {
                                    reassign = "No";
                                    reassignhistory = "";
                                }

                                string assignid = "ASSIGNID" + assign_li.assignInteractionID;
                                smseachData.campaign = campaign;
                                smseachData.reassign = reassign;
                                smseachData.reassignhistory = reassignhistory;

                                smseachData.assignDate = assign_li.assignedDate;
                                smseachData.WyzuserName = user;
                                smseachData.reason = managerUser; //db.wyzusers.FirstOrDefault(m => m.id == assign_li.assigned_manager_id).userName;
                                smseachData.assignedId = assignid;
                                smseachData.smsMessage = lastDispo;
                                smseachData.smsType = avail;
                                smseachData.serviceTypes = serviceType;

                                // smseachData.assignedBy = db.wyzusers.FirstOrDefault(m => m.id == assign_li.assigned_manager_id).userName;

                                sms_int_data.Add(smseachData);
                            }
                        }
                    }
                    else
                    {
                        string campaign = "";
                        string reassign = "";
                        string reassignhistory = "";
                        string Cremangeruser = "";


                        string dealer = db.wyzusers.SingleOrDefault(m => m.id == UserId).dealerName;
                        List<assignedcallsreport_stage1> assign = db.Assignedcallsreport_Stages.Where(m => m.vehicleId == vehiId && m.moduletypeId == 2).ToList();

                        foreach (var assign_li in assign)
                        {
                            SMSInteractionhistory smseachData = new SMSInteractionhistory();

                            if (!takenList.Contains(assign_li.assignInteractionID))
                            {
                                takenList.Add(assign_li.assignInteractionID);

                                wyzuser wyz = new wyzuser();
                                string user = "";
                                if (assign_li.wyzuserId != 0)
                                {
                                    long id = assign_li.wyzuserId;
                                    user = db.wyzusers.FirstOrDefault(m => m.id == id).userName;
                                }

                                smseachData.resonforDrop = db.Database.SqlQuery<string>("select  whendeleted  from deletedstatusreport  where moduletype_id=2 and assigned_id=@id;", new MySqlParameter("@id", assign_li.assignInteractionID)).FirstOrDefault();
                                if (smseachData.resonforDrop == null)
                                {
                                    smseachData.resonforDrop = "-";
                                }
                                insuranceassignedinteraction assignAvail = new insuranceassignedinteraction();
                                assignAvail = db.insuranceassignedinteractions.SingleOrDefault(m => m.id == assign_li.assignInteractionID && m.vehicle_vehicle_id == vehiId);

                                string avail = "Removed";
                                if (assignAvail != null)
                                {
                                    avail = "Active";
                                }

                                string managerUser = "Auto";
                                bool autoassigned = db.assignedcallsreports.FirstOrDefault(m => m.assignInteractionID == assign_li.assignInteractionID).isautoassigned;
                                if (autoassigned == false)
                                {
                                    long id = assign_li.wyzuserId;
                                    Cremangeruser = db.wyzusers.FirstOrDefault(m => m.id == id).creManager;
                                    managerUser = Cremangeruser;
                                }
                                //if (assignAvail != null && assignAvail.isAutoAssigned == false)
                                //{
                                //    long id = assign_li.wyzuserId;
                                //    Cremangeruser = db.wyzusers.FirstOrDefault(m => m.id == id).creManager;
                                //    managerUser = Cremangeruser;
                                //}

                                insurancecallhistorycube callhistory = new insurancecallhistorycube();


                                int countcalhistory = db.insurancecallhistorycubes.Count(m => m.insuranceAssignedInteraction_id == assign_li.assignInteractionID && m.vehicle_vehicle_id == vehiId);
                                if (countcalhistory > 0)
                                {
                                    callhistory = db.insurancecallhistorycubes.FirstOrDefault(m => m.insuranceAssignedInteraction_id == assign_li.assignInteractionID && m.vehicle_vehicle_id == vehiId);
                                }

                                string lastDispo = "";
                                if (callhistory.SecondaryDisposition != null)
                                {
                                    lastDispo = callhistory.SecondaryDisposition;
                                }
                                long? cid = assign_li.campaignId;
                                if (cid != 0)
                                {
                                    campaign camp = db.campaigns.Where(c => c.id == cid).FirstOrDefault();
                                    campaign = camp.campaignName;
                                }


                                int countassignmentRecorrd = db.change_assignment_records.Count(m => m.assignedinteraction_id == assign_li.assignInteractionID && m.moduletypeIs == 2);

                                if (countassignmentRecorrd > 0)
                                {
                                    List<change_assignment_records> chng = db.change_assignment_records.Where(m => m.assignedinteraction_id == assign_li.assignInteractionID && m.moduletypeIs == 2).ToList();
                                    reassign = "Yes";
                                    foreach (change_assignment_records c in chng)
                                    {
                                        wyzuser w = db.wyzusers.Where(m => m.id == c.new_wyzuserId).FirstOrDefault();

                                        string d = c.updatedDate.ToString("dd/MM/yyyy");
                                        reassignhistory += w.userName + " -> " + d + "<br>";
                                    }
                                }
                                else
                                {
                                    reassign = "No";
                                    reassignhistory = "";
                                }

                                string assignid = "ASSIGNID" + assign_li.assignInteractionID;
                                smseachData.campaign = campaign;
                                smseachData.reassign = reassign;
                                smseachData.reassignhistory = reassignhistory;

                                smseachData.assignDate = assign_li.assignedDate;
                                smseachData.WyzuserName = user;
                                smseachData.reason = managerUser;
                                smseachData.assignedId = assignid;
                                smseachData.smsMessage = lastDispo;
                                smseachData.smsType = avail;

                                //smseachData.assignedBy = db.wyzusers.FirstOrDefault(m => m.id == assign_li.assigned_manager_id).userName;

                                sms_int_data.Add(smseachData);
                            }
                        }
                    }
                }
                return Json(new { data = sms_int_data, draw = Request["draw"], recordsTotal = sms_int_data.Count, recordsFiltered = sms_int_data.Count }, JsonRequestBehavior.AllowGet);
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
                return Json(new { data = "", draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0, exception = exception }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult getCallHistory_viewmore(string typeIs)
        {

            //Logger logger = LogManager.GetLogger("apkRegLogger");
            string gsmAndroid = "";
            long vehicalId = Convert.ToInt32(Session["VehiId"].ToString());

            //Parameters of Paging..........
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            //string searchPattern = Request["search[value]"];

            int maxCount = 0;

            List<interactiondata> interDataList = new List<interactiondata>();

            string exception = "";
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (typeIs == "PSF")
                    {
                        maxCount = db.Psfcallhistorycube_Stages.Where(m => m.vehicle_vehicle_id == vehicalId).Count();
                        List<psfcallhistorycube_stage1> psfCallHistoryByVehicle = db.Psfcallhistorycube_Stages.Where(m => m.vehicle_vehicle_id == vehicalId).OrderByDescending(m => m.callDate).Skip(start).Take(length).ToList();

                        if (psfCallHistoryByVehicle != null && psfCallHistoryByVehicle.Count > 0)
                        {
                            psfCallHistoryByVehicle = psfCallHistoryByVehicle.OrderByDescending(m => m.id).ToList();
                        }

                        foreach (var call_data in psfCallHistoryByVehicle)
                        {

                            interactiondata interData = new interactiondata();

                            interData.CallDate = Convert.ToDateTime(call_data.callDate).ToString("dd-MM-yyyy");
                            interData.Campaign = call_data.psfCallingDayType;
                            interData.CreId = call_data.creName;
                            interData.AssignId = "AASIGNID" + call_data.psfAssignedInteraction_id.ToString();
                            interData.Time = call_data.callTime.ToString();
                            interData.jobCardNumber = call_data.jobCardNumber;
                            if (string.IsNullOrEmpty(call_data.callType))
                            {
                                interData.CallType = call_data.callType;
                            }
                            else
                            {
                                interData.CallType = "-";
                            }
                            //if (call_data.gsm_android != null)
                            //{
                            //    gsmAndroid = "(" + call_data.gsm_android + ")";
                            //}
                            interData.IsCallInitiated = call_data.isCallinitaited;// + gsmAndroid;
                            interData.gsmAndroid = call_data.gsm_android;

                            if (!string.IsNullOrEmpty(call_data.makeCallFrom))
                            {
                                interData.CallMadeType = call_data.makeCallFrom;
                            }
                            else
                            {
                                interData.CallMadeType = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.dailedNumber))
                            {
                                interData.DailedNo = call_data.dailedNumber;
                            }
                            else
                            {
                                interData.DailedNo = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.SecondaryDisposition))
                            {
                                interData.SecondaryDispo = call_data.SecondaryDisposition;
                            }
                            else
                            {
                                interData.SecondaryDispo = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.SecondaryDisposition))
                            {
                                if (call_data.SecondaryDisposition == "Call Me Later")
                                {
                                    interData.Details = Convert.ToDateTime(call_data.psfFollowUpDate).ToString("dd-MM-yyyy") + "/" + call_data.psfFollowUpTime;
                                }
                                else if (!string.IsNullOrEmpty(call_data.Remarks))
                                {
                                    interData.Details = call_data.Remarks;
                                }
                                else
                                {
                                    interData.Details = "-";
                                }
                            }
                            else
                            {
                                interData.Details = "-";
                            }


                            if (!string.IsNullOrEmpty(call_data.Remarks))
                            {
                                interData.CreRemarks = call_data.Remarks;
                            }
                            else
                            {
                                interData.CreRemarks = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.Comments))
                            {
                                interData.Feedback = call_data.Comments;
                            }
                            else
                            {
                                interData.Feedback = "-";
                            }



                            //filepath
                            if (!string.IsNullOrEmpty(call_data.filepath))
                            {
                                interData.FilePath = call_data.filepath;
                            }
                            else
                            {
                                //var callsynchData = db.callSyncDatas.FirstOrDefault(m => m.callinteraction_id == call_data.cicallinteraction_id);
                                //if (callsynchData != null)
                                //{
                                //    interData.FilePath = callsynchData.filepath;
                                //}
                                //else
                                //{
                                var callinteraction = db.callinteractions.FirstOrDefault(m => m.id == call_data.cicallinteraction_id);
                                if (callinteraction != null)
                                {
                                    interData.FilePath = callinteraction.filePath;
                                }
                                else
                                {
                                    interData.FilePath = "-";
                                }
                                //}
                            }
                            interDataList.Add(interData);
                        }
                    }
                    else if (typeIs == "INS")
                    {
                        maxCount = db.Insurancecallhistorycube_Stages.Where(m => m.vehicle_vehicle_id == vehicalId).Count();
                        List<insurancecallhistorycube_stage1> insuranceCallHistoryByVehicle = db.Insurancecallhistorycube_Stages.Where(m => m.vehicle_vehicle_id == vehicalId).OrderByDescending(X => X.callDate).Skip(start).Take(length).ToList();

                        if (insuranceCallHistoryByVehicle != null && insuranceCallHistoryByVehicle.Count() > 0)
                        {
                            insuranceCallHistoryByVehicle = insuranceCallHistoryByVehicle.OrderByDescending(m => m.id).ToList();
                        }

                        foreach (var call_data in insuranceCallHistoryByVehicle)
                        {
                            interactiondata interData = new interactiondata();


                            if (call_data.callDate != null)
                            {
                                interData.CallDate = Convert.ToDateTime(call_data.callDate).ToString("dd-MM-yyyy");
                            }
                            else
                            {
                                interData.CallDate = "-";
                            }

                            interData.Campaign = call_data.campaignTYpe;

                            if (Session["DealerCode"].ToString() == "INDUS")
                            {
                                var wyzUser = db.wyzusers.FirstOrDefault(m => m.id == call_data.wyzUser_id);

                                if (wyzUser != null)
                                {
                                    interData.CreId = wyzUser.firstName + "(" + wyzUser.userName + ")";
                                }
                                else
                                {
                                    interData.CreId = call_data.creName;
                                }
                            }
                            else
                            {
                                interData.CreId = call_data.creName;
                            }

                            interData.AssignId = "AASIGNID" + call_data.insuranceAssignedInteraction_id.ToString();
                            interData.Time = call_data.callTime.ToString();

                            if (string.IsNullOrEmpty(call_data.callType))
                            {
                                interData.CallType = call_data.callType;
                            }
                            else
                            {
                                interData.CallType = "-";
                            }
                            //if (call_data.gsm_android != null)
                            //{
                            //    gsmAndroid = "(" + call_data.gsm_android + ")";
                            //}
                            interData.IsCallInitiated = call_data.isCallinitaited;// + gsmAndroid;
                            interData.gsmAndroid = call_data.gsm_android;

                            if (!string.IsNullOrEmpty(call_data.makeCallFrom))
                            {
                                interData.CallMadeType = call_data.makeCallFrom;
                            }
                            else
                            {
                                interData.CallMadeType = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.dailedNumber))
                            {
                                interData.DailedNo = call_data.dailedNumber;
                            }
                            else
                            {
                                interData.DailedNo = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.SecondaryDisposition))
                            {
                                interData.SecondaryDispo = call_data.SecondaryDisposition;
                            }
                            else
                            {
                                interData.SecondaryDispo = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.SecondaryDisposition))
                            {
                                if (call_data.SecondaryDisposition == "Call Me Later")
                                {
                                    interData.Details = Convert.ToDateTime(call_data.followUpDate).ToString("dd-MM-yyyy") + " " + call_data.followUpTime;
                                }
                                else if (call_data.SecondaryDisposition == "Book Appointment")
                                {
                                    if (!string.IsNullOrEmpty(call_data.Reshedule_Status) && call_data.Reshedule_Status == "Rescheduled")
                                    {
                                        string InsAgentName = "";
                                        if (!string.IsNullOrEmpty(call_data.insuranceAgent_insuranceAgentId))
                                        {
                                            long agentId = long.Parse(call_data.insuranceAgent_insuranceAgentId);

                                            insuranceagent agent = db.insuranceagents.FirstOrDefault(m => m.insuranceAgentId == agentId);
                                            if (agent != null)
                                            {
                                                InsAgentName = "|" + agent.insuranceAgentName;
                                            }
                                        }
                                        else if (!string.IsNullOrEmpty(call_data.insuranceAgentData))
                                        {
                                            long agentId = long.Parse(call_data.insuranceAgentData);

                                            insuranceagent agent = db.insuranceagents.FirstOrDefault(m => m.insuranceAgentId == agentId);
                                            if (agent != null)
                                            {
                                                InsAgentName = "|" + agent.insuranceAgentName;
                                            }
                                        }



                                        interData.Details = call_data.typeOfPickup + "|" + call_data.Tertiary_disposition + "|" + call_data.Reshedule_Status + InsAgentName;
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(call_data.insuranceAgent_insuranceAgentId))
                                        {
                                            long agentId = long.Parse(call_data.insuranceAgent_insuranceAgentId);

                                            insuranceagent agent = db.insuranceagents.FirstOrDefault(m => m.insuranceAgentId == agentId);
                                            if (agent != null)
                                            {
                                                interData.Details = agent.insuranceAgentName + "|" + Convert.ToDateTime(call_data.appointmentDate).ToString("dd-MM-yyyy") + "  " + call_data.appointmentFromTime + "|" + call_data.typeOfPickup + "|" + call_data.insuranceCompany + "|" + call_data.premiumwithdiscount.ToString();
                                            }
                                            else
                                            {
                                                interData.Details = Convert.ToDateTime(call_data.appointmentDate).ToString("dd-MM-yyyy") + "  " + call_data.appointmentFromTime + "|" + call_data.typeOfPickup + "|" + call_data.insuranceCompany + "|" + call_data.premiumwithdiscount.ToString();
                                            }
                                        }
                                        else if (!string.IsNullOrEmpty(call_data.insuranceAgentData))
                                        {
                                            long agentId = long.Parse(call_data.insuranceAgentData);

                                            insuranceagent agent = db.insuranceagents.FirstOrDefault(m => m.insuranceAgentId == agentId);
                                            if (agent != null)
                                            {
                                                interData.Details = agent.insuranceAgentName + "|" + Convert.ToDateTime(call_data.appointmentDate).ToString("dd-MM-yyyy") + "  " + call_data.appointmentFromTime + "|" + call_data.typeOfPickup + "|" + call_data.insuranceCompany + "|" + call_data.premiumwithdiscount.ToString();

                                            }
                                            else
                                            {
                                                interData.Details = Convert.ToDateTime(call_data.appointmentDate).ToString("dd-MM-yyyy") + "  " + call_data.appointmentFromTime + "|" + call_data.typeOfPickup + "|" + call_data.insuranceCompany + "|" + call_data.premiumwithdiscount.ToString();
                                            }
                                        }
                                        else
                                        {
                                            interData.Details = Convert.ToDateTime(call_data.appointmentDate).ToString("dd-MM-yyyy") + "  " + call_data.appointmentFromTime + "|" + call_data.typeOfPickup + "|" + call_data.insuranceCompany + "|" + call_data.premiumwithdiscount.ToString();
                                        }

                                    }
                                }
                                else
                                {
                                    interData.Details = Convert.ToDateTime(call_data.callDate).ToString("dd-MM-yyyy") + "  " + call_data.callTime.ToString().Substring(0, call_data.callTime.ToString().IndexOf(":") + 3);
                                }
                            }
                            else
                            {
                                interData.Details = "-";
                            }


                            if (!string.IsNullOrEmpty(call_data.comments))
                            {
                                interData.CreRemarks = call_data.comments;
                            }
                            else
                            {
                                interData.CreRemarks = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.customerComments))
                            {
                                interData.Feedback = call_data.customerComments;
                            }
                            else
                            {
                                interData.Feedback = "-";
                            }



                            //filepath
                            if (!string.IsNullOrEmpty(call_data.filepath))
                            {
                                interData.FilePath = call_data.filepath;
                            }
                            else
                            {
                                //var callsynchData = db.callSyncDatas.FirstOrDefault(m => m.callinteraction_id == call_data.cicallinteraction_id);
                                //if (callsynchData != null)
                                //{
                                //    interData.FilePath = callsynchData.filepath;
                                //}
                                //else
                                //{
                                var callinteraction = db.callinteractions.FirstOrDefault(m => m.id == call_data.cicallinteraction_id);
                                if (callinteraction != null)
                                {
                                    interData.FilePath = callinteraction.filePath;
                                }
                                else
                                {
                                    interData.FilePath = "-";
                                }
                                //}
                            }


                            interDataList.Add(interData);
                        }
                    }
                    else if (typeIs == "PostSalesFeedback")
                    {
                        maxCount = db.Postsalescallhistory_Stages.Where(m => m.vehicle_vehicle_id == vehicalId).Count();
                        List<postsalescallhistory_stage1> postsalesfeedbackCallHistoryByVehicle = db.Postsalescallhistory_Stages.Where(m => m.vehicle_vehicle_id == vehicalId).OrderByDescending(m => m.callDate).Skip(start).Take(length).ToList();

                        if (postsalesfeedbackCallHistoryByVehicle != null && postsalesfeedbackCallHistoryByVehicle.Count > 0)
                        {
                            postsalesfeedbackCallHistoryByVehicle = postsalesfeedbackCallHistoryByVehicle.OrderByDescending(m => m.id).ToList();
                        }

                        foreach (var call_data in postsalesfeedbackCallHistoryByVehicle)
                        {

                            interactiondata interData = new interactiondata();

                            interData.CallDate = Convert.ToDateTime(call_data.callDate).ToString("dd-MM-yyyy");
                            interData.Campaign = call_data.psfCallingDayType;
                            interData.CreId = call_data.creName;
                            interData.AssignId = "AASIGNID" + call_data.postAssignedInteraction_id.ToString();
                            interData.Time = call_data.callTime.ToString();

                            if (string.IsNullOrEmpty(call_data.callType))
                            {
                                interData.CallType = call_data.callType;
                            }
                            else
                            {
                                interData.CallType = "-";
                            }
                            //if (call_data.gsm_android != null)
                            //{
                            //    gsmAndroid = "(" + call_data.gsm_android + ")";
                            //}
                            interData.IsCallInitiated = call_data.isCallinitaited;// + gsmAndroid;
                            interData.gsmAndroid = call_data.gsm_android;

                            if (!string.IsNullOrEmpty(call_data.makeCallFrom))
                            {
                                interData.CallMadeType = call_data.makeCallFrom;
                            }
                            else
                            {
                                interData.CallMadeType = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.dailedNumber))
                            {
                                interData.DailedNo = call_data.dailedNumber;
                            }
                            else
                            {
                                interData.DailedNo = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.SecondaryDisposition))
                            {
                                interData.SecondaryDispo = call_data.SecondaryDisposition;
                            }
                            else
                            {
                                interData.SecondaryDispo = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.SecondaryDisposition))
                            {
                                if (call_data.SecondaryDisposition == "Call Me Later")
                                {
                                    interData.Details = Convert.ToDateTime(call_data.psfFollowUpDate).ToString("dd-MM-yyyy") + "/" + call_data.psfFollowUpTime;
                                }
                                else if (!string.IsNullOrEmpty(call_data.Remarks))
                                {
                                    interData.Details = call_data.Remarks;
                                }
                                else
                                {
                                    interData.Details = "-";
                                }
                            }
                            else
                            {
                                interData.Details = "-";
                            }


                            if (!string.IsNullOrEmpty(call_data.Remarks))
                            {
                                interData.CreRemarks = call_data.Remarks;
                            }
                            else
                            {
                                interData.CreRemarks = "-";
                            }

                            if (!string.IsNullOrEmpty(call_data.Comments))
                            {
                                interData.Feedback = call_data.Comments;
                            }
                            else
                            {
                                interData.Feedback = "-";
                            }



                            //filepath
                            if (!string.IsNullOrEmpty(call_data.filepath))
                            {
                                interData.FilePath = call_data.filepath;
                            }
                            else
                            {
                                //var callsynchData = db.callSyncDatas.FirstOrDefault(m => m.callinteraction_id == call_data.cicallinteraction_id);
                                //if (callsynchData != null)
                                //{
                                //    interData.FilePath = callsynchData.filepath;
                                //}
                                //else
                                //{
                                var callinteraction = db.callinteractions.FirstOrDefault(m => m.id == call_data.cicallinteraction_id);
                                if (callinteraction != null)
                                {
                                    interData.FilePath = callinteraction.filePath;
                                }
                                else
                                {
                                    interData.FilePath = "-";
                                }
                                //}
                            }
                            interDataList.Add(interData);
                        }
                    }

                    else
                    {
                        //db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                        maxCount = db.Callhistorycube_Stages.Where(m => m.vehicle_id == vehicalId).Count();
                        List<callhistorycube_stage1> serviceCallHistoryByVehicle = db.Callhistorycube_Stages.Where(m => m.vehicle_id == vehicalId).OrderByDescending(m => m.callDate).Skip(start).Take(length).ToList();

                        if (serviceCallHistoryByVehicle != null && serviceCallHistoryByVehicle.Count() > 0)
                        {
                            serviceCallHistoryByVehicle = serviceCallHistoryByVehicle.OrderByDescending(m => m.id).ToList();
                        }

                        foreach (var call_data in serviceCallHistoryByVehicle)
                        {
                            interactiondata interData = new interactiondata();
                            //interactiondata interData = new interactiondata();

                            interData.CallDate = Convert.ToDateTime(call_data.callDate).ToString("dd-MM-yyyy");
                            interData.Campaign = call_data.calling_data_type;
                            interData.CreId = call_data.Cre_Name;
                            interData.AssignId = "AASIGNID" + call_data.assignedInteraction_id.ToString();
                            interData.Time = call_data.callTime.Split('.')[0];

                            if (string.IsNullOrEmpty(call_data.callType))
                            {
                                interData.CallType = "-";
                            }
                            else
                            {
                                interData.CallType = call_data.callType;
                            }

                            if (string.IsNullOrEmpty(call_data.isCallinitaited))
                            {
                                interData.IsCallInitiated = "-";
                            }
                            else
                            {
                                //if (call_data.gsm_android != null)
                                //{
                                //    gsmAndroid = "(" + call_data.gsm_android + ")";
                                //}
                                interData.IsCallInitiated = call_data.isCallinitaited;// + gsmAndroid;
                                interData.gsmAndroid = call_data.gsm_android;
                            }

                            if (string.IsNullOrEmpty(call_data.makeCallFrom))
                            {
                                interData.CallMadeType = "-";
                            }
                            else
                            {
                                interData.CallMadeType = call_data.makeCallFrom;
                            }

                            if (string.IsNullOrEmpty(call_data.dailedNumber))
                            {
                                interData.DailedNo = "-";
                            }
                            else
                            {
                                interData.DailedNo = call_data.dailedNumber;
                            }

                            if (string.IsNullOrEmpty(call_data.secondary_dispostion))
                            {
                                interData.SecondaryDispo = "-";
                            }
                            else
                            {
                                interData.SecondaryDispo = call_data.secondary_dispostion;
                            }

                            if (string.IsNullOrEmpty(call_data.nextServicetype))
                            {
                                interData.ServiveType = "-";
                            }
                            else
                            {
                                interData.ServiveType = call_data.nextServicetype;
                            }

                            if (!string.IsNullOrEmpty(call_data.secondary_dispostion))
                            {
                                if (call_data.secondary_dispostion == "Call Me Later")
                                {
                                    interData.Details = Convert.ToDateTime(call_data.followUpDate).ToString("dd-MM-yyyy") + " " + call_data.followUpTime;
                                }
                                else if (call_data.secondary_dispostion == "Book My Service")
                                {
                                    if (call_data.Reshedule_Status != null && call_data.Reshedule_Status == "Rescheduled")
                                    {
                                        //sr_data.NoServiceReason = Convert.ToDateTime(call_data.scheduledDateTime).ToString("dd-MM-yyyy HH:mm:ss");
                                        interData.Details = call_data.Tertiary_disposition + "|" + call_data.Reshedule_Status + "|" + call_data.typeofpickup;
                                    }
                                    else
                                    {
                                        //sr_data.NoServiceReason = Convert.ToDateTime(call_data.scheduledDateTime).ToString("dd-MM-yyyy HH:mm:ss");
                                        interData.Details = call_data.Tertiary_disposition + "|" + call_data.typeofpickup;
                                    }
                                }
                                else if (!string.IsNullOrEmpty(call_data.currentMileage) && call_data.noServiceReason == "Kms Not Covered")
                                {
                                    interData.Details = call_data.noServiceReason + "/" + call_data.currentMileage + " Km";
                                }
                                else if (!string.IsNullOrEmpty(call_data.transferedCity) && call_data.noServiceReason == "Distance from Dealer Location")
                                {
                                    interData.Details = call_data.noServiceReason + "/" + call_data.transferedCity;
                                }
                                else if (!string.IsNullOrEmpty(call_data.Reason) && call_data.noServiceReason == "Other Service")
                                {
                                    interData.Details = call_data.noServiceReason + "/" + call_data.Reason;
                                }
                                else if (call_data.secondary_dispostion == "Service Not Required")
                                {
                                    interData.Details = call_data.noServiceReason + "/" + call_data.authorisedOrNot + "/" + call_data.alredyServicedDealerName;
                                }
                                else
                                {
                                    interData.Details = call_data.noServiceReason;
                                }
                            }
                            else
                            {
                                interData.Details = "-";
                            }

                            if (string.IsNullOrEmpty(call_data.customer_remarks))
                            {
                                interData.CreRemarks = "-";
                            }
                            else
                            {
                                interData.CreRemarks = call_data.customer_remarks;
                            }

                            if (string.IsNullOrEmpty(call_data.customerRemarks))
                            {
                                interData.Feedback = "-";
                            }
                            else
                            {
                                interData.Feedback = call_data.customerRemarks;
                            }


                            //filepath
                            if (!string.IsNullOrEmpty(call_data.filepath))
                            {
                                interData.FilePath = call_data.filepath;
                            }
                            else
                            {
                                // var callsynchData = db.callSyncDatas.FirstOrDefault(m => m.callinteraction_id == call_data.Call_interaction_id);
                                //if(callsynchData!=null)
                                // {
                                //     if(!string.IsNullOrEmpty(callsynchData.filepath))
                                //     {
                                //         interData.FilePath = callsynchData.filepath;
                                //     }
                                //     else
                                //     {
                                //         interData.FilePath = "-";
                                //     }

                                // }
                                // else
                                // {
                                var callinteraction = db.callinteractions.FirstOrDefault(m => m.id == call_data.Call_interaction_id);
                                if (callinteraction != null)
                                {
                                    if (!string.IsNullOrEmpty(callinteraction.filePath))
                                    {
                                        interData.FilePath = callinteraction.filePath;
                                    }
                                    else
                                    {
                                        interData.FilePath = "-";
                                    }

                                }
                                else
                                {
                                    interData.FilePath = "-";
                                }
                                //}
                            }


                            interDataList.Add(interData);
                        }
                    }
                }

                return Json(new { data = interDataList, draw = Request["draw"], recordsTotal = maxCount, recordsFiltered = maxCount, exception = exception }, JsonRequestBehavior.AllowGet);
                //return Json(new { success = true, interaction_list = callhistoryData });
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
                return Json(new { data = "", draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0, exception = exception }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

        #region Kataria Auto Whatsapp
        public void AutoWhatsappKataria(long wyzId, long vehicleId, long custId, long dispositionId, string notRequiredReason, string innerRequiredReason, int moduleType, long workshopId, string DealerCode = null)
        {
            try
            {
                IScheduler autoWhatsappScheduler = StdSchedulerFactory.GetDefaultScheduler().Result;

                autoWhatsappScheduler.Start();
                IJobDetail jobDetail = JobBuilder.Create<TwilioWhatsapp>().Build();

                string trigername = "DispoAutoWhatsappKatariaTrigger" + DateTime.Now.Millisecond + vehicleId;
                string trigerGroupName = "DispoAutoWhatsappKatriaGroup" + DateTime.Now.Millisecond + vehicleId;

                ITrigger trigger = TriggerBuilder.Create().WithIdentity(trigername, trigerGroupName).StartNow().WithSimpleSchedule().Build();

                jobDetail.JobDataMap["WyzId"] = wyzId;
                jobDetail.JobDataMap["vehicleId"] = vehicleId;
                jobDetail.JobDataMap["custId"] = custId;
                jobDetail.JobDataMap["dispositionId"] = dispositionId;
                jobDetail.JobDataMap["notRequiredReason"] = notRequiredReason;
                jobDetail.JobDataMap["innerRequiredReason"] = innerRequiredReason;
                jobDetail.JobDataMap["moduleType"] = moduleType;
                jobDetail.JobDataMap["workshopId"] = workshopId;


                if (string.IsNullOrEmpty(DealerCode))
                {
                    jobDetail.JobDataMap["DealerCode"] = Session["DealerCode"].ToString();

                }
                else
                {
                    jobDetail.JobDataMap["DealerCode"] = DealerCode;

                }

                autoWhatsappScheduler.ScheduleJob(jobDetail, trigger);
            }
            catch (Exception ex)
            {
                string exception = "";
                if (ex.Message.Contains("inner exception"))
                {
                    exception = ex.InnerException.Message;
                }
                else
                {
                    exception = ex.Message;
                }
            }

        }

        #endregion

        #region prutech implimentation
        public string GetJsonToken(string username, string password)
        {
            WebRequest request = WebRequest.Create("https://prutech.org/PMMW/api/authentication/generate-token");
            var httprequest = (HttpWebRequest)request;

            httprequest.PreAuthenticate = true;
            httprequest.Method = "POST";
            httprequest.ContentType = "application/json";
            httprequest.Accept = "application/json";
            dynamic requestbody = new JObject();
            requestbody.username = username;
            requestbody.password = password;

            using (var streamWriter = new StreamWriter(httprequest.GetRequestStream()))
            {
                var bodyContent = JsonConvert.SerializeObject(requestbody);

                streamWriter.Write(bodyContent);

                streamWriter.Flush();
                streamWriter.Close();

                //logger.Info("Sending InfoBip: \n" + bodyContent + "\n On " + DateTime.Now);
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
            return response_string;


        }


        public string CallPruchtecWhatsapp(Dictionary<string, string> parameterList)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            using (var db = new AutoSherDBContext())
            {

                long custId = long.Parse(Session["CusId"].ToString());
                long vehiId = long.Parse(Session["VehiId"].ToString());
                long UserId = long.Parse(Session["UserId"].ToString());
                string UserName = Session["UserName"].ToString();

                string phNum = parameterList["custNum"].Trim();
                long smsId = long.Parse(parameterList["smsId"]);
                string template_name = parameterList["template_name"];
                string template_message = parameterList["message"];
                long maxParaCount = long.Parse(parameterList["maxPara"]);
                List<string> smsparam = db.smstemplates.FirstOrDefault(m => m.smsId == smsId).messageparams?.Split(',').ToList();
                var credential = db.businesswhatsappdetails.FirstOrDefault(m => m.isactive == true);

                if (phNum.Length == 10)
                {
                    phNum = "91" + phNum;
                }


                PrutechBusinessApi prutechBusinessApi = new PrutechBusinessApi();
                //Messages msg = new Messages();
                Content cnt = new Content();
                TempData tempData = new TempData();
                PlaceHolders placeHolders = new PlaceHolders();
                TempBody tempBody = new TempBody();
                Random rnd = new Random();

                tempBody.PlaceHolders = new List<PlaceHolders>();
                foreach (var param in smsparam)
                {
                    PlaceHolders ph = new PlaceHolders();
                    ph.Type = "text";
                    ph.Text = parameterList[param];
                    tempBody.PlaceHolders.Add(ph);
                }


                tempData.Body = new TempBody();
                tempData.Body = tempBody;


                cnt.TemplateData = new TempData();
                cnt.TemplateData = tempData;
                cnt.TemplateName = template_name;
                cnt.Language = "en";

                //msg.CallBackData = "Callback data";
                //msg.Content = new Content();
                //msg.From = "918139000158";
                //msg.To = phNum;
                //msg.MessageId = rnd.Next().ToString();
                //msg.Content = cnt;

                prutechBusinessApi.Messages = new List<Messages>();
                Messages msg = new Messages();
                msg.CallBackData = "Callback data";
                msg.Content = new Content();
                msg.From = credential.fromnumber;
                msg.To = phNum;
                msg.MessageId = rnd.Next().ToString();
                msg.Content = cnt;
                prutechBusinessApi.Messages.Add(msg);
                prutechBusinessApi.BulkId = string.Concat("bulk_", rnd.Next().ToString());
                //prutechBusinessApi.Messages = msg;
                var timeDiff = credential.expiry.HasValue ? DateTime.Now - credential.expiry : null;

                PrutechToken tokenDetails = new PrutechToken();
                if (string.IsNullOrEmpty(credential.bearertoken) || !timeDiff.HasValue || timeDiff.Value.Hours < 0 || (timeDiff.HasValue && timeDiff.Value.Hours > 2))
                {
                    var token = GetJsonToken(credential.username, credential.password);
                    tokenDetails = JsonConvert.DeserializeObject<PrutechToken>(token);
                    credential.bearertoken = tokenDetails.idToken;
                    credential.expiry = DateTime.Now;
                    db.businesswhatsappdetails.AddOrUpdate(credential);
                }

                WebRequest request = WebRequest.Create(credential.baseurl);
                var httprequest = (HttpWebRequest)request;

                httprequest.PreAuthenticate = true;
                httprequest.Method = "POST";
                httprequest.Headers.Add("Authorization", string.Concat("Bearer ", credential.bearertoken));
                httprequest.ContentType = "application/json";
                httprequest.Accept = "application/json";


                using (var streamWriter = new StreamWriter(httprequest.GetRequestStream()))
                {
                    var bodyContent = JsonConvert.SerializeObject(prutechBusinessApi);

                    streamWriter.Write(bodyContent);

                    streamWriter.Flush();
                    streamWriter.Close();

                    //logger.Info("Sending InfoBip: \n" + bodyContent + "\n On " + DateTime.Now);
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

                PrutechResponse responseData = new PrutechResponse();
                string message;
                if (!string.IsNullOrEmpty(response_string))
                {
                    var jsonResult = JsonConvert.DeserializeObject(response_string).ToString();
                    responseData = JsonConvert.DeserializeObject<PrutechResponse>(jsonResult);
                }

                logger.Info("ORAI whatsApp Response:\n" + response_string + "\n On " + DateTime.Now);


                smsinteraction smsinteraction = new smsinteraction();

                smsinteraction.interactionDate = DateTime.Now.ToString("dd-MM-yyyy");
                smsinteraction.interactionDateAndTime = DateTime.Now;
                smsinteraction.interactionTime = DateTime.Now.ToString("HH:mm:ss");
                smsinteraction.interactionType = "Whatsapp Msg";
                smsinteraction.responseFromGateway = response_string;
                smsinteraction.customer_id = custId;
                smsinteraction.vehicle_vehicle_id = vehiId;
                smsinteraction.wyzUser_id = UserId;
                smsinteraction.mobileNumber = phNum;
                smsinteraction.smsType = smsId.ToString();
                //smsinteraction.smsMessage = message;
                smsinteraction.isAutoSMS = false;
                smsinteraction.smsStatus = true;
                smsinteraction.reason = responseData.message;
                smsinteraction.smsMessage = template_message;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    smsinteraction.smsStatus = true;
                    smsinteraction.reason = "Send Successfully" + response.StatusDescription;
                }
                else
                {
                    smsinteraction.smsStatus = false;
                    smsinteraction.reason = "Sending Failed" + response.StatusDescription;
                }
                db.smsinteractions.Add(smsinteraction);
                db.SaveChanges();
                return responseData.message;

            }

        }
        #endregion

        #region chat360 Business whtasApp implimentation


        public string CallChat360Whatsapp(Dictionary<string, string> parameterList)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            using (var db = new AutoSherDBContext())
            {

                long custId = long.Parse(Session["CusId"].ToString());
                long vehiId = long.Parse(Session["VehiId"].ToString());
                long UserId = long.Parse(Session["UserId"].ToString());
                string UserName = Session["UserName"].ToString();

                string phNum = parameterList["custNum"].Trim();
                long smsId = long.Parse(parameterList["smsId"]);
                string template_name = parameterList["template_name"];
                string template_message = parameterList["message"];
                long maxParaCount = long.Parse(parameterList["maxPara"]);
                var smsDetails = db.smstemplates.FirstOrDefault(m => m.smsId == smsId);
                List<string> smsparam = smsDetails.messageparams?.Split(',').ToList();
                var credential = db.businesswhatsappdetails.FirstOrDefault(m => m.isactive == true);
                var originalParamNames = db.Whatsapporaiparams.Where(m => smsparam.Contains(m.id.ToString())).ToList();

                if (phNum.Length == 10)
                {
                    phNum = "91" + phNum;
                }


                CallChatRequest callChatRequest = new CallChatRequest();
                
                //Messages msg = new Messages();
                TaskBody taskBody = new TaskBody();
                TemplateData templateData = new TemplateData();
                templateData.template_id = smsDetails.templateId;
                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                foreach(var param in smsparam)
                {
                    keyValuePairs.Add(originalParamNames.FirstOrDefault(m => m.id == Convert.ToInt32(param)).originalparam, parameterList[param]);
                }

                templateData.param_data = keyValuePairs;

                taskBody.client_number = credential.fromnumber;
                taskBody.receiver_number = phNum;
                taskBody.template_data = templateData;

                callChatRequest.task_name = "whatsapp_push_notification";
                callChatRequest.extra = String.Empty;
                callChatRequest.task_body = new List<TaskBody>();
                callChatRequest.task_body.Add(taskBody);
                

               
                WebRequest request = WebRequest.Create(credential.baseurl);
                var httprequest = (HttpWebRequest)request;

                httprequest.PreAuthenticate = true;
                httprequest.Method = "POST";
                httprequest.Headers.Add("Authorization", string.Concat("Api-Key ", credential.bearertoken));
                httprequest.ContentType = "application/json";
                httprequest.Accept = "application/json";


                using (var streamWriter = new StreamWriter(httprequest.GetRequestStream()))
                {
                    var bodyContent = JsonConvert.SerializeObject(callChatRequest);

                    streamWriter.Write(bodyContent);

                    streamWriter.Flush();
                    streamWriter.Close();

                    //logger.Info("Sending InfoBip: \n" + bodyContent + "\n On " + DateTime.Now);
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

                
               
                string message = string.Empty;
                if (!string.IsNullOrEmpty(response_string))
                {
                    var jsonResult = JsonConvert.DeserializeObject(response_string).ToString();
                    message = jsonResult.Contains("successful") ? "Send Successfully" : "Sending Failed";
                }                

                logger.Info("ORAI whatsApp Response:\n" + response_string + "\n On " + DateTime.Now);


                smsinteraction smsinteraction = new smsinteraction();

                smsinteraction.interactionDate = DateTime.Now.ToString("dd-MM-yyyy");
                smsinteraction.interactionDateAndTime = DateTime.Now;
                smsinteraction.interactionTime = DateTime.Now.ToString("HH:mm:ss");
                smsinteraction.interactionType = "Whatsapp Msg";
                smsinteraction.responseFromGateway = response_string;
                smsinteraction.customer_id = custId;
                smsinteraction.vehicle_vehicle_id = vehiId;
                smsinteraction.wyzUser_id = UserId;
                smsinteraction.mobileNumber = phNum;
                smsinteraction.smsType = smsId.ToString();
                
                smsinteraction.isAutoSMS = false;
                smsinteraction.smsStatus = true;
                smsinteraction.reason = message;
                smsinteraction.smsMessage = template_message;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    smsinteraction.smsStatus = true;
                    smsinteraction.reason = "Send Successfully" + response.StatusDescription;
                }
                else
                {
                    smsinteraction.smsStatus = false;
                    smsinteraction.reason = "Sending Failed" + response.StatusDescription;
                }
                db.smsinteractions.Add(smsinteraction);
                db.SaveChanges();
                return message;

            }

        }
        #endregion
        public string GetPhoneNumberByLocation(int workshopId)
        {
            using (var db = new AutoSherDBContext())
            {
                var phoneNumber = db.workshops.FirstOrDefault(m => m.id == workshopId)?.workshopPhone;
                return phoneNumber;
            }
        }

        #region TgsGroup Sms API
        public string TgsSms(Dictionary<string, string> parameterList, long smsId , string phNum, string templateMessage)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            using (var db = new AutoSherDBContext())
            {

                long custId = long.Parse(Session["CusId"].ToString());
                long vehiId = long.Parse(Session["VehiId"].ToString());
                long UserId = long.Parse(Session["UserId"].ToString());
                string UserName = Session["UserName"].ToString();
                phNum = phNum.Trim();

                var smsDetails = db.smstemplates.FirstOrDefault(m => m.smsId == smsId);
                List<string> smsparam = smsDetails.messageparams?.Split(',').ToList();
                var credential = db.businesswhatsappdetails.FirstOrDefault(m => m.isactive == true);
                var originalParamNames = db.Whatsapporaiparams.Where(m => smsparam.Contains(m.id.ToString())).ToList();

                if (phNum.Length == 10)
                {
                    phNum = "91" + phNum;
                }
                parameterList.Add("mobiles", phNum);


                TGSRequest tgsRequest = new TGSRequest();
                tgsRequest.template_id = smsDetails.templateId;
                tgsRequest.sender = smsDetails.dealer;
                tgsRequest.recipients = new List<IDictionary<string, string>>();
                tgsRequest.recipients.Add(parameterList);
                WebRequest request = WebRequest.Create(credential.baseurl);
                var httprequest = (HttpWebRequest)request;

                httprequest.PreAuthenticate = true;
                httprequest.Method = "POST";
                httprequest.Headers.Add("authkey", credential.bearertoken);
                httprequest.ContentType = "application/json";
                httprequest.Accept = "application/json";


                using (var streamWriter = new StreamWriter(httprequest.GetRequestStream()))
                {
                    var bodyContent = JsonConvert.SerializeObject(tgsRequest);

                    streamWriter.Write(bodyContent);

                    streamWriter.Flush();
                    streamWriter.Close();

                    //logger.Info("Sending InfoBip: \n" + bodyContent + "\n On " + DateTime.Now);
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



                string message = string.Empty;
               
                    var jsonResult = JsonConvert.DeserializeObject<TGSRespons>(response_string);
                    message = jsonResult.type;
                

                logger.Info("ORAI whatsApp Response:\n" + response_string + "\n On " + DateTime.Now);


                smsinteraction smsinteraction = new smsinteraction();

                smsinteraction.interactionDate = DateTime.Now.ToString("dd-MM-yyyy");
                smsinteraction.interactionDateAndTime = DateTime.Now;
                smsinteraction.interactionTime = DateTime.Now.ToString("HH:mm:ss");
                smsinteraction.interactionType = "Text Msg";
                smsinteraction.responseFromGateway = response_string;
                smsinteraction.customer_id = custId;
                smsinteraction.vehicle_vehicle_id = vehiId;
                smsinteraction.wyzUser_id = UserId;
                smsinteraction.mobileNumber = phNum;
                smsinteraction.smsType = smsId.ToString();

                smsinteraction.isAutoSMS = false;
                smsinteraction.smsStatus = true;
                smsinteraction.reason = message;
                smsinteraction.smsMessage = templateMessage;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    smsinteraction.smsStatus = true;
                    smsinteraction.reason = "Send Successfully" + response.StatusDescription;
                }
                else
                {
                    smsinteraction.smsStatus = false;
                    smsinteraction.reason = "Sending Failed" + response.StatusDescription;
                }
                db.smsinteractions.Add(smsinteraction);
                db.SaveChanges();
                return message;

            }

        }

        public ActionResult getTgsSmsTemplate(long smsId)
        {
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    var smstmplates = db.smstemplates.FirstOrDefault(x => x.smsId == smsId);
                    List<long> oraiparamsId = smstmplates.messageparams.Split(',').Select(long.Parse).ToList();

                    List<whatsapporaiparams> parameters = db.Whatsapporaiparams.Where(m => oraiparamsId.Contains(m.parameterid)).ToList();
                    return Json(new { success = true, smsParams = parameters.OrderBy(m => m.parameterid) }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string exception = "";
                if (ex.Message.Contains("inner exception"))
                {
                    exception = ex.InnerException.Message;
                }
                else
                {
                    exception = ex.Message;
                }
                return Json(new { success = false, error = exception }, JsonRequestBehavior.AllowGet);
            }
        }


        #endregion

    }
}
