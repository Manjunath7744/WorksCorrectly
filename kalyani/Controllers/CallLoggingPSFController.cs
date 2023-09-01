using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoSherpa_project.Models.ViewModels;
using AutoSherpa_project.Models;
using System.Data.Entity.Migrations;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using System.Data.Entity;
using NLog;

namespace AutoSherpa_project.Controllers
{
    [AuthorizeFilter]
    public class CallLoggingPSFController : Controller
    {

        [HttpGet, ActionName("CallLogging_PSF")]
        public ActionResult CallLoggingKalyani_PSF(string id)
        {
            //new CallLoggingController().clear360ProfilePrevSession();
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
            Session["NCReason"] = null;

            if (Session["inComingParameter"] != null)
            {
                id = string.Empty;
                id = Session["inComingParameter"].ToString();
            }
            else
            {
                TempData["Exceptions"] = "Invalid Url Operation..[CallLogging]....";
                if (Session["UserRole"].ToString() != "CREManager" && Session["UserRole"].ToString() != "RegionalManager")
                {
                    return RedirectToAction("LogOff", "Home");
                }
            }

            CallLoggingViewModel callLog = new CallLoggingViewModel();
            ViewBag.typeOfDispo = "PSF";

            long cid, vehicle_id, interactionid, dispositionHistory, typeOfPSF, lastworkshopId = 0, bucket_id = 0;
            string pageFor, customer_category = "";
            pageFor = id.Split(',')[0];
            cid = Convert.ToInt32(id.Split(',')[1]);
            vehicle_id = Convert.ToInt32(id.Split(',')[2]);
            interactionid = Convert.ToInt32(id.Split(',')[3]);
            dispositionHistory = Convert.ToInt32(id.Split(',')[4]);
            typeOfPSF = Convert.ToInt32(id.Split(',')[5]);
            ViewBag.typeOfPSF = typeOfPSF;
            ViewBag.interactionid = interactionid;

            Session["psfDayType"] = dispositionHistory;

            if (dispositionHistory == 900)
            {
                bucket_id = Convert.ToInt32(id.Split(',')[5]);
                ViewBag.dispositionHistory = "PSFComplaints";
                ViewBag.typeOfPSF = 900;
            }
            else if (dispositionHistory == 500)
            {
                Session["isPSFRM"] = true;
                ViewBag.dispositionHistory = "PSFRM";
            }
            else if (dispositionHistory == 63)
            {
                typeOfPSF = Convert.ToInt32(id.Split(',')[5]);
                ViewBag.typeOfPSF = typeOfPSF;
                Session["psfDayType"] = typeOfPSF;
                callLog.PsfCreBucketId = 63;
                ViewBag.isResolved = true;
                ViewBag.dispositionHistory = "PSFDays";
            }
            else
            {
                typeOfPSF = Convert.ToInt32(id.Split(',')[5]);
                ViewBag.typeOfPSF = typeOfPSF;
                Session["psfDayType"] = typeOfPSF;
                ViewBag.isResolved = false;
                ViewBag.dispositionHistory = "PSFDays";
            }

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
            callLog.rework = new rework();
            callLog.templates = new List<smstemplate>();
            callLog.lastService = new service();
            callLog.LatestInsurance = new insurance();
            callLog.listingForm = new ListingForm();
            callLog.listingForm.upsellleads = new List<upselllead>();
            callLog.emailtemplates = new List<emailtemplate>();
            callLog.Latestservices = new service();
            callLog.finaldispostion = new calldispositiondata();
            callLog.indusPsfInteraction = new IndusPSFInteraction();
            callLog.psfPullOuts = new List<PsfPullOut>();
            callLog.custPhoneList = new List<phone>();
            callLog.compInteractions = new CompInteraction();
            //callLog.listingForm.upsellleads = new List<upselllead>();

            try
            {
                using (var db = new AutoSherDBContext())
                {

                    long assignWyzId = 0;

                    if (pageFor == "Shw")
                    {
                        Session["PageFor"] = "CRE";
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
                    Session["CusId"] = cid;
                    Session["VehiId"] = vehicle_id;
                    Session["interactionid"] = interactionid;
                    Session["typeOfDispo"] = "PSF";

                    callLog.CustomerId = cid;
                    callLog.VehicleId = vehicle_id;
                    callLog.UserId = UserId;
                    callLog = new CallLoggingController().get360ProfileData(callLog, db, "PSF", Session["DealerCode"].ToString(), Session["OEM"].ToString());
                    Session["VehiReg"] = callLog.vehi.chassisNo;
                    string dealername = callLog.wyzuser.dealerName;


                    if (typeOfPSF != 0)
                    {
                        Session["PSFDay"] = typeOfPSF.ToString() + "Day";
                    }

                    //Customer Category for hyundau dealers
                    if (Session["OEM"].ToString() == "HYUNDAI" || Session["DealerCode"].ToString() == "JDAUTONATION" || Session["ISPSFENABLED"].ToString() == "True")
                    {
                        if (db.psfassignedinteractions.Any(m => m.id == interactionid))
                        {
                            long serviceId = db.psfassignedinteractions.FirstOrDefault(m => m.id == interactionid).service_id ?? default(long);

                            if (serviceId == 0)
                            {
                                if (callLog.vehi.saleDate != null)
                                {
                                    customer_category = getCustomerCategory(callLog.vehi.saleDate ?? default(DateTime));
                                }
                                else
                                {
                                    customer_category = "nosaledate";
                                }
                            }
                            else
                            {
                                string serviceType = db.services.FirstOrDefault(m => m.id == serviceId).lastServiceType;
                                if(serviceType != null) { 
                                if (serviceType.ToUpper() == "BODYSHOP" || serviceType == "Accidental Repair")
                                {
                                    customer_category = "bodyshop";
                                }
                                else
                                {
                                    if (callLog.vehi.saleDate != null)
                                    {
                                        customer_category = getCustomerCategory(callLog.vehi.saleDate ?? default(DateTime));
                                    }
                                    else
                                    {
                                        customer_category = "nosaledate";
                                    }
                                }
                                }
                                else
                                {
                                    serviceType = "noserviceType";
                                }
                            }
                        }
                        else if (callLog.vehi.saleDate != null)
                        {
                            customer_category = getCustomerCategory(callLog.vehi.saleDate ?? default(DateTime));
                        }
                        else
                        {
                            customer_category = "nosaledate";
                        }

                        ViewBag.CustCategory = customer_category.ToUpper();
                    }
                    else if (Session["DealerCode"].ToString() == "KALYANIMOTORS")
                    {
                        if (db.psfassignedinteractions.Any(m => m.id == interactionid))
                        {
                            long serviceId = db.psfassignedinteractions.FirstOrDefault(m => m.id == interactionid).service_id ?? default(long);

                            if (serviceId == 0)
                            {
                                customer_category = "NoServiceType";
                            }
                            else
                            {
                                string serviceType = db.services.FirstOrDefault(m => m.id == serviceId).lastServiceType;

                                if (serviceType != null && serviceType.ToUpper() == "BODYSHOP" || serviceType.ToUpper() == "BANDP")
                                {
                                    customer_category = "bodyshop";
                                }
                                else
                                {
                                    customer_category = serviceType;
                                }
                            }
                        }
                        ViewBag.CustCategory = customer_category.ToUpper();
                    }

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
                    if (dispositionHistory == 900)
                    {

                        if (Session["DealerCode"].ToString() == "KALYANIMOTORS")
                        {
                            CompInteraction oldCompInter = db.compInteractions.Where(m => m.psfassignedInteraction_id == interactionid).OrderByDescending(m => m.Id).FirstOrDefault();
                            if (oldCompInter != null)
                            {
                                callLog.compInteractions = oldCompInter;
                                callLog.compInteractions.bucket_id = Convert.ToInt32(bucket_id);
                            }
                        }

                        long userLocId = db.wyzusers.FirstOrDefault(m => m.id == UserId).location_cityId ?? default(long);

                        if (userLocId != 0)
                        {
                            var WorkshopList = db.workshops.Where(m => m.location_cityId == userLocId).Select(m => new { id = m.id, name = m.workshopName }).ToList();
                            ViewBag.workshops = WorkshopList;
                        }

                        long? ComplaintCatMainId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Resolved").dispositionId;

                        if (ComplaintCatMainId != 0)
                        {
                            var compCat = db.calldispositiondatas.Where(m => m.mainDispositionId == ComplaintCatMainId).Select(m => new { id = m.id, catName = m.disposition }).ToList();
                            ViewBag.ComplaintCategory = compCat;
                        }


                        if (Session["OEM"].ToString() == "HYUNDAI" || Session["DealerCode"].ToString() == "JDAUTONATION" || Session["ISPSFENABLED"].ToString() == "True")
                        {
                            //int? rcissueId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "RC Issue Types").dispositionId;
                            int? rcissueId = db.calldispositiondatas.Where(m => m.disposition.Contains("RC Issue Types")).Select(m => m.dispositionId).FirstOrDefault();
                            if (rcissueId != 0)
                            {
                                var rcIssue = db.calldispositiondatas.Where(m => m.mainDispositionId == rcissueId).Select(m => new { name = m.disposition }).ToList();
                                ViewBag.rcIssueType = rcIssue;
                            }
                            
                            else
                            {
                                ViewBag.rcIssueType = new List<SelectList>();
                            }
                        }


                    }
                    else if (dispositionHistory == 500)
                    {

                    }
                    else
                    {
                        if (dispositionHistory != 63)
                        {



                            long complaintMngId = 0;

                            if (db.roles.Any(m => m.role1 == "Complaint Manager"))
                            {
                                complaintMngId = db.roles.FirstOrDefault(m => m.role1 == "Complaint Manager").id;
                            }

                            if (complaintMngId != 0)
                            {
                                //long loginUserWorkshopId = db.wyzusers.FirstOrDefault(m => m.id == UserId).workshop_id ?? default(long);

                                var userworkshopIds = db.userworkshops.Where(r => r.userWorkshop_id == UserId).Select(r => r.workshopList_id).ToList();

                                //lastworkshopId = db.psfassignedinteractions.FirstOrDefault(m => m.id == interactionid).workshop_id ?? default(long);
                                var complaintCreId = db.userroles.Where(m => m.roles_id == complaintMngId).Select(m => m.users_id).ToList();
                                var complaintCre = db.wyzusers.Where(m => complaintCreId.Contains(m.id) && userworkshopIds.Contains(m.workshop_id??default(long))).Select(m => new { id = m.id, name = m.userName }).ToList();
                                ViewBag.complaintCre = complaintCre;
                            }
                            else
                            {
                                ViewBag.complaintCre = new List<SelectList>();
                            }

                            if ((Session["OEM"].ToString() == "HYUNDAI" || Session["DealerCode"].ToString() == "JDAUTONATION" || Session["ISPSFENABLED"].ToString() == "True") && !string.IsNullOrEmpty(customer_category))
                            {
                                List<psfquestions> psfqsList = new List<psfquestions>();
                                customer_category = customer_category.ToLower();

                                psfqsList = db.psfquestions.Where(m => (m.visited_cust_cat.Contains(customer_category) || m.pickup_cust_cat.Contains(customer_category)) && m.campaignid == typeOfPSF && m.isActive == true).ToList();

                                //Formating Questions
                                callLog.psfquestions = new List<psfquestions>();
                                List<psfquestions> qsHelper = new List<psfquestions>();

                                //Questions in both sections
                                qsHelper = psfqsList.Where(m => m.visited_cust_cat.Contains(customer_category) && m.pickup_cust_cat.Contains(customer_category)).ToList();
                                qsHelper.ForEach(m => m.SectionName = "both");
                                callLog.psfquestions.AddRange(qsHelper);
                                qsHelper.Clear();

                                //Questions only in Visited
                                qsHelper = psfqsList.Where(m => m.visited_cust_cat.Contains(customer_category) && !m.pickup_cust_cat.Contains(customer_category)).ToList();
                                qsHelper.ForEach(m => m.SectionName = "visited");
                                callLog.psfquestions.AddRange(qsHelper);
                                qsHelper.Clear();

                                //Questions only in pickup
                                qsHelper = psfqsList.Where(m => !m.visited_cust_cat.Contains(customer_category) && m.pickup_cust_cat.Contains(customer_category)).ToList();
                                qsHelper.ForEach(m => m.SectionName = "pickup");
                                callLog.psfquestions.AddRange(qsHelper);
                                qsHelper.Clear();

                                for (int i = 0; i < callLog.psfquestions.Count(); i++)
                                {
                                    if (callLog.psfquestions[i].display_type == "drop-down")
                                    {
                                        if (!string.IsNullOrEmpty(callLog.psfquestions[i].ddl_range) && string.IsNullOrEmpty(callLog.psfquestions[i].ddl_options))
                                        {
                                            callLog.psfquestions[i].IsDDLNumeric = true;
                                            callLog.psfquestions[i].ddlmin = int.Parse(callLog.psfquestions[i].ddl_range.Split(',')[0]);
                                            callLog.psfquestions[i].ddlmax = int.Parse(callLog.psfquestions[i].ddl_range.Split(',')[1]);
                                        }
                                        else if (string.IsNullOrEmpty(callLog.psfquestions[i].ddl_range) && !string.IsNullOrEmpty(callLog.psfquestions[i].ddl_options))
                                        {
                                            callLog.psfquestions[i].IsDDLNumeric = false;
                                            callLog.psfquestions[i].DDLOptionList = callLog.psfquestions[i].ddl_options.Split(',').ToList();
                                        }
                                    }
                                    else if (callLog.psfquestions[i].display_type == "radio-button")
                                    {
                                        callLog.psfquestions[i].RadioList = callLog.psfquestions[i].radio_options.Split(',').ToList();
                                    }
                                    else if (callLog.psfquestions[i].display_type == "checkbox")
                                    {
                                        callLog.psfquestions[i].CheckBoxOptions = callLog.psfquestions[i].checkbox_options.Split(',').ToList();
                                    }
                                }

                                callLog.psfquestions = callLog.psfquestions.OrderBy(m => m.question_no).ToList();
                                callLog.psfinteraction = new psfinteraction();
                                callLog.psfinteraction.psfquestions_id = string.Join(",", callLog.psfquestions.Select(m => m.id.ToString()).ToList());

                            }
                        }
                    }
                    var ddlTagging = db.campaigns.Where(m => m.campaignType == "TaggingSMR" && m.isactive == true && m.campaignName != "ISP" && m.campaignName != "ISP/MCP/EW" && m.campaignName != "ISP/MCP" && m.campaignName != "ISP/EW").Select(model => new { id = model.id, TaggingCamp = model.campaignName }).ToList();
                    ViewBag.ddltagging = ddlTagging;
                    callLog.selectedTagList = callLog.cust.leadtag?.Split(',')?.ToList();
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

            return View("~/Views/CallLogging/Call_Logging.cshtml", callLog);
        }

        public string getCustomerCategory(DateTime sateDate)
        {
            DateTime salesDate = Convert.ToDateTime(sateDate);
            double totalDays = (DateTime.Now.Date - salesDate.Date).TotalDays;
            if (totalDays <= 60)
            {
                return "CX1";
            }
            else if (totalDays >= 61 && totalDays <= 1095)
            {
                return "GOLD";
            }
            else if (totalDays >= 1096 && totalDays <= 1825)
            {
                return "PLATINUM";
            }
            else if (totalDays >= 1826 && totalDays <= 2555)
            {
                return "DIAMOND";
            }
            else if (totalDays > 2556)
            {
                return "Above 7 YRS";
            }
            else
            {
                return "";
            }
        }

        public long recordPsfDisposition(int stage, AutoSherDBContext db, int? finalDispoId = 0, long? psf_assigninter = 0)
        {
            //try
            //{
            //using (var db = new AutoSherDBContext())
            //{
            if (stage == 1)
            {
                psfassignedinteraction psfAssInter = new psfassignedinteraction();
                if (db.psfassignedinteractions.Any(m => m.id == psf_assigninter))
                {
                    string lastDispo = "";
                    psfAssInter = db.psfassignedinteractions.FirstOrDefault(m => m.id == psf_assigninter);
                    psfAssInter.callMade = "Yes";
                    if (psfAssInter.finalDisposition_id != null)
                    {
                        lastDispo = db.calldispositiondatas.FirstOrDefault(m => m.id == psfAssInter.finalDisposition_id).disposition;
                        psfAssInter.lastDisposition = lastDispo;
                    }

                    if (finalDispoId == 63)
                    {
                        psfAssInter.isResolved = true;
                    }

                    psfAssInter.finalDisposition_id = finalDispoId;
                    db.psfassignedinteractions.AddOrUpdate(psfAssInter);
                    db.SaveChanges();

                    return psfAssInter.id;
                }
            }
            //}

            //}
            //catch(Exception ex)
            //{

            //}

            return 0;
        }

        public ActionResult returnToCallLog(string psfType, int psfDay)
        {
            if (TempData["SubmissionResult"] != null)
            {
                TempData["SubmissionResult"] = TempData["SubmissionResult"];
            }

            try
            {
                if (psfType == "PSF")
                {
                    return RedirectToAction("PSFDetails", "PSF", new { @id = psfDay });
                }
                else if (psfType == "PSFComplaint")
                {
                    return RedirectToAction("psfComplaintDetails", "PSF", null);
                }
                else
                {
                    return RedirectToAction("PSFRM", "PSF", null);
                }
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("PSFDetails", "PSF", new { @id = 6 });
        }

        [HttpPost]
        public ActionResult addKalynipsf(CallLoggingViewModel callLog)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");

            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            long userId = Convert.ToInt32(Session["UserId"].ToString());
            long interactionId = Convert.ToInt32(Session["interactionid"].ToString());
            int currentDispo = 0;
            long psfAss_id = 0;
            callinteraction callinteraction = new callinteraction();
            IndusPSFInteraction induspsfinter = callLog.indusPsfInteraction;

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string contactDispo = callLog.indusPsfInteraction.whatCustSaid;
                    using (var dbTrans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            logger.Info("\n PSF Days Incoming disposition: CustId:" + cusId + " VehiId:" + vehiId + " User:" + Session["UserName"].ToString());

                            if (callLog.CustomerId != cusId && callLog.VehicleId != vehiId)
                            {
                                TempData["Exceptions"] = "Invalid Disposition Found...";
                                return RedirectToAction("LogOff", "Home");
                            }

                            if (db.psfassignedinteractions.Any(m => m.customer_id == cusId && m.vehicle_vehicle_id == vehiId))
                            {
                                if (induspsfinter.isContacted == "No")
                                {
                                    currentDispo = db.calldispositiondatas.FirstOrDefault(m => m.disposition == induspsfinter.PSFDisposition).dispositionId;
                                    psfAss_id = recordPsfDisposition(1, db, currentDispo, interactionId);

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
                                    callinteraction.makeCallFrom = "PSF";

                                    if (Session["DialedNumber"] != null)
                                    {
                                        callinteraction.dailedNoIs = Session["DialedNumber"].ToString();
                                    }

                                    if (Session["LoginUser"].ToString() == "PSFComMgr")
                                    {
                                        callinteraction.isComplaint = true;
                                    }
                                    if (callLog.PsfCreBucketId == 63)
                                    {
                                        callinteraction.isResolved = true;
                                    }
                                    callinteraction.psfAssignedInteraction_id = psfAss_id;
                                    callinteraction.customer_id = cusId;
                                    callinteraction.vehicle_vehicle_id = vehiId;
                                    callinteraction.wyzUser_id = userId;
                                    callinteraction.agentName = Session["UserName"].ToString();
                                    callinteraction.campaign_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).campaign_id ?? default(long);
                                    callinteraction.service_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).service_id ?? default(long);
                                    callinteraction.chasserCall = false;
                                    db.callinteractions.Add(callinteraction);
                                    db.SaveChanges();

                                    if (Session["LoginUser"].ToString() == "PSFComMgr")
                                    {
                                        callLog.compInteractions.calldisposition_id = currentDispo;
                                        callLog.compInteractions.callinteraction_id = callinteraction.id;
                                        db.compInteractions.Add(callLog.compInteractions);
                                        db.SaveChanges();

                                        induspsfinter.dispositionFrom = "PSFCompMgr";
                                    }
                                    else
                                    {
                                        induspsfinter.dispositionFrom = "PSFCRE";
                                    }

                                    if (Session["GSMUniqueId"] != null)
                                    {
                                        gsmsynchdata gsm = new gsmsynchdata();
                                        gsm.callFor = Convert.ToInt32(Session["UserRole1"]);
                                        gsm.Callinteraction_id = callinteraction.id;
                                        gsm.CallMadeDateTime = DateTime.Now;
                                        gsm.UniqueGsmId = Session["GSMUniqueId"].ToString();
                                        gsm.TenantUrl = Session["GSMUniqueId"].ToString().Split(';')[1];
                                        db.gsmsynchdata.Add(gsm);
                                        db.SaveChanges();
                                    }


                                    induspsfinter.callInteraction_id = callinteraction.id;
                                    induspsfinter.callDispositionData_id = currentDispo;
                                    db.indusPSFInteraction.Add(induspsfinter);
                                    db.SaveChanges();
                                }
                                else if (induspsfinter.isContacted == "Yes")
                                {
                                    if (contactDispo == "PSF_Yes" || contactDispo == "Not Interested" || contactDispo == "Call Me Later" || contactDispo == "ConfirmStatus")
                                    {
                                        if (induspsfinter.modeOfServiceDone == "0" || induspsfinter.modeOfServiceDone == "-50" || induspsfinter.modeOfServiceDone == "50")
                                        {
                                            currentDispo = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Dissatisfied with PSF").dispositionId;
                                        }
                                        else if (contactDispo == "ConfirmStatus")
                                        {
                                            currentDispo = db.calldispositiondatas.FirstOrDefault(m => m.disposition == induspsfinter.modeOfService).dispositionId;
                                        }
                                        else
                                        {
                                            currentDispo = db.calldispositiondatas.FirstOrDefault(m => m.disposition == contactDispo).dispositionId;
                                        }


                                        psfAss_id = recordPsfDisposition(1, db, currentDispo, interactionId);

                                        if (Session["LoginUser"].ToString() == "PSFComMgr")
                                        {
                                            callinteraction.isComplaint = true;
                                        }

                                        if (callLog.PsfCreBucketId == 63 && currentDispo != 44)
                                        {
                                            callinteraction.isResolved = true;
                                        }

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

                                        callinteraction.makeCallFrom = "PSF";

                                        if (Session["DialedNumber"] != null)
                                        {
                                            callinteraction.dailedNoIs = Session["DialedNumber"].ToString();
                                        }

                                        callinteraction.chasserCall = false;
                                        callinteraction.psfAssignedInteraction_id = psfAss_id;
                                        callinteraction.customer_id = cusId;
                                        callinteraction.vehicle_vehicle_id = vehiId;
                                        callinteraction.wyzUser_id = userId;
                                        callinteraction.agentName = Session["UserName"].ToString();
                                        callinteraction.campaign_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).campaign_id ?? default(long);
                                        callinteraction.service_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).service_id ?? default(long);
                                        db.callinteractions.Add(callinteraction);
                                        db.SaveChanges();

                                        if (Session["LoginUser"].ToString() == "PSFComMgr")
                                        {
                                            callLog.compInteractions.calldisposition_id = currentDispo;
                                            callLog.compInteractions.callinteraction_id = callinteraction.id;
                                            db.compInteractions.Add(callLog.compInteractions);
                                            db.SaveChanges();

                                            induspsfinter.dispositionFrom = "PSFCompMgr";
                                        }
                                        else
                                        {
                                            induspsfinter.dispositionFrom = "PSFCRE";
                                        }



                                        if (Session["GSMUniqueId"] != null)
                                        {
                                            gsmsynchdata gsm = new gsmsynchdata();

                                            gsm.Callinteraction_id = callinteraction.id;
                                            gsm.CallMadeDateTime = DateTime.Now;
                                            gsm.UniqueGsmId = Session["GSMUniqueId"].ToString();
                                            gsm.TenantUrl = Session["GSMUniqueId"].ToString().Split(';')[1];
                                            gsm.callFor = Convert.ToInt32(Session["UserRole1"]);
                                            db.gsmsynchdata.Add(gsm);
                                            db.SaveChanges();
                                        }

                                        //if (contactDispo == "Call Me Later")
                                        //{
                                        //    induspsfinter.isFollowUpDone = "No";
                                        //    psfinter.isFollowupRequired = "Yes";
                                        //}

                                        induspsfinter.upsellCount = 0;
                                        induspsfinter.callInteraction_id = callinteraction.id;
                                        induspsfinter.callDispositionData_id = currentDispo;
                                        db.indusPSFInteraction.Add(induspsfinter);
                                        db.SaveChanges();

                                        long upselCount = 0;
                                        if (callLog.listingForm != null)
                                        {
                                            if (callLog.listingForm.LeadYes == "Yes")
                                            {
                                                foreach (var upsel in callLog.listingForm.upsellleads)
                                                {
                                                    if (upsel.taggedTo != null)
                                                    {
                                                        upsel.vehicle_vehicle_id = vehiId;
                                                        upsel.induspsfinteraction_Id = induspsfinter.Id;
                                                        //upsel.induspsfinteraction_id = psfinter.Id;
                                                        //upsel.srDisposition_id = sr_disposition.id;
                                                        db.upsellleads.Add(upsel);
                                                        db.SaveChanges();
                                                        upselCount++;
                                                    }
                                                }
                                            }
                                        }


                                        if (upselCount != 0)
                                        {
                                            induspsfinter.upsellCount = upselCount;
                                            db.indusPSFInteraction.AddOrUpdate(induspsfinter);
                                            db.SaveChanges();
                                        }

                                        if (induspsfinter.modeOfServiceDone == "0" || induspsfinter.modeOfServiceDone == "-50" || induspsfinter.modeOfServiceDone == "50" || induspsfinter.modeOfServiceDone == "Dissatisfied with PSF" || induspsfinter.modeOfService == "Dissatisfied with PSF")
                                        {
                                            //long complaintMgr_id = 0;
                                            //if (induspsfinter.modeOfService == "Dissatisfied with PSF")
                                            //{
                                            //    long maxRwId = db.reworks.Where(m => m.customer_id == cusId && m.vehicle_id == vehiId).Max(m => m.id);
                                            //    complaintMgr_id = db.reworks.FirstOrDefault(m => m.id == maxRwId).complaint_creid;
                                            //}

                                            //rework rework = new rework();

                                            wyzuser user = db.wyzusers.FirstOrDefault(m => m.id == userId);
                                            //rework.Benefits = "No Benefits Applied";
                                            //rework.DissatStatus_id = 44;
                                            //if (callLog.rework != null)
                                            //{
                                            //    rework.complaint_creid = callLog.rework.complaint_creid;
                                            //}
                                            //else
                                            //{
                                            //    rework.complaint_creid = complaintMgr_id;
                                            //}
                                            //rework.campaign_id = long.Parse(Session["psfDayType"].ToString());
                                            //rework.customer_id = cusId;
                                            //rework.vehicle_id = vehiId;
                                            //rework.discount = 0;
                                            //rework.isReworkAvailable = false;
                                            //rework.issuedate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));
                                            //rework.location_id = user.location_cityId ?? default(long);
                                            //rework.psfassignedInteraction_id = psfAss_id;
                                            //rework.workshop_id = user.workshop_id ?? default(long);
                                            //db.reworks.Add(rework);
                                            //db.SaveChanges();
                                            CompInteraction compInter = new CompInteraction();
                                            if (callLog.compInteractions != null)
                                            {
                                                compInter = callLog.compInteractions;
                                            }
                                            else
                                            {
                                                CompInteraction oldCompInter = db.compInteractions.Where(m => m.psfassignedInteraction_id == psfAss_id).OrderByDescending(m => m.Id).FirstOrDefault();
                                                if (oldCompInter != null)
                                                {
                                                    compInter.complaint_creid = oldCompInter.complaint_creid;
                                                }

                                            }
                                            compInter.Benefits = "No Benefits Applied";
                                            compInter.DissatStatus_id = 44;
                                            compInter.campaign_id = long.Parse(Session["psfDayType"].ToString());
                                            compInter.customer_id = cusId;
                                            compInter.vehicle_id = vehiId;
                                            compInter.discount = 0;
                                            compInter.isReworkAvailable = false;
                                            compInter.issueDateTime = DateTime.Now;
                                            compInter.location_id = user.location_cityId ?? default(long);
                                            compInter.psfassignedInteraction_id = psfAss_id;
                                            compInter.callinteraction_id = callinteraction.id;
                                            compInter.workshop_id = user.workshop_id ?? default(long);
                                            compInter.bucket_id = 1;
                                            compInter.compRaisedCreId = long.Parse(Session["UserId"].ToString());
                                            compInter.compRaisedCreName = Session["UserName"].ToString();

                                            db.compInteractions.Add(compInter);
                                            db.SaveChanges();
                                        }
                                    }
                                    else if (contactDispo == "No Resolution - Closed" || contactDispo == "Re-Work" || contactDispo == "Resolved")
                                    {
                                        CompInteraction newCompInter = callLog.compInteractions;
                                        //rework rework = callLog.rework;
                                        induspsfinter.dispositionFrom = "PSFCompMgr";
                                        //long reworkMaxId = 0, preCompManagerId = 0;
                                        //if (db.reworks.Any(m => m.customer_id == cusId))
                                        //{
                                        //    reworkMaxId = db.reworks.Where(m => m.customer_id == cusId).Max(m => m.id);
                                        //    preCompManagerId = db.reworks.FirstOrDefault(m => m.id == reworkMaxId).complaint_creid;
                                        //}

                                        if (contactDispo == "No Resolution - Closed")
                                        {
                                            currentDispo = db.calldispositiondatas.FirstOrDefault(m => m.disposition == contactDispo).dispositionId;

                                            // psfAss_id = recordPsfDisposition(1, db, currentDispo, interactionId);

                                            //rework = db.reworks.FirstOrDefault(m => m.id == reworkMaxId);
                                            //rework.id = reworkMaxId;
                                            //rework.DissatStatus_id = currentDispo;
                                            //rework.reworkAddress = "";
                                            //rework.reworkStatus_id = currentDispo;
                                            //rework.resolutionDateAndTime = DateTime.Now;
                                            //rework.psfassignedInteraction_id = psfAss_id;
                                            //db.reworks.AddOrUpdate(rework);
                                            //db.SaveChanges();


                                            newCompInter.reworkAddress = "";
                                            newCompInter.calldisposition_id = currentDispo;


                                        }
                                        else if (contactDispo == "Re-Work")
                                        {
                                            currentDispo = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Rework").dispositionId;

                                            // psfAss_id = recordPsfDisposition(1, db, currentDispo, interactionId);
                                            // bool isNewInsert = false;
                                            //rework existingRework = db.reworks.SingleOrDefault(m => m.id == reworkMaxId);

                                            //if (existingRework.reworkStatus_id != 0)
                                            //{

                                            //    rework.Benefits = "No Benefits Applied";
                                            //    rework.complaint_creid = existingRework.complaint_creid;
                                            //    rework.campaign_id = existingRework.campaign_id;
                                            //    rework.customer_id = existingRework.customer_id;
                                            //    rework.DissatStatus_id = existingRework.DissatStatus_id;
                                            //    rework.issuedate = existingRework.issuedate;
                                            //    rework.vehicle_id = existingRework.vehicle_id;
                                            //    rework.psfassignedInteraction_id = psfAss_id;

                                            //    rework.reworkStatus_id = currentDispo;
                                            //    rework.resolutionDateAndTime = DateTime.Now;
                                            //    if (rework.reworkMode == "Self")
                                            //    {
                                            //        rework.reworkAddress = "";
                                            //    }

                                            //    db.reworks.Add(rework);
                                            //    db.SaveChanges();

                                            //    existingRework.reworkStatus_id = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Cancelled").id;
                                            //    db.reworks.AddOrUpdate(existingRework);
                                            //    db.SaveChanges();
                                            //}
                                            //else
                                            //{
                                            //    existingRework.reworkStatus_id = currentDispo;
                                            //    //existingRework.resolutionDateAndTime = DateTime.Now;
                                            //    existingRework.reworkMode = rework.reworkMode;
                                            //    if (existingRework.reworkMode == "Self")
                                            //    {
                                            //        existingRework.reworkAddress = "";
                                            //    }
                                            //    existingRework.psfassignedInteraction_id = psfAss_id;
                                            //    existingRework.workshop_id = rework.workshop_id;
                                            //    existingRework.reworkDateAndTime = rework.reworkDateAndTime;
                                            //    existingRework.resolutionMode = "Schedule a re-visit";
                                            //    //existingRework.visittype = rework.visittype;
                                            //    db.reworks.AddOrUpdate(existingRework);
                                            //    db.SaveChanges();
                                            //}

                                            newCompInter.calldisposition_id = currentDispo;
                                            //existingRework.resolutionDateAndTime = DateTime.Now;
                                            if (newCompInter.reworkMode == "Self")
                                            {
                                                newCompInter.reworkAddress = "";
                                            }
                                            newCompInter.resolutionMode = "Schedule a re-visit";


                                        }
                                        else if (contactDispo == "Resolved")
                                        {
                                            currentDispo = db.calldispositiondatas.FirstOrDefault(m => m.disposition == contactDispo).dispositionId;

                                            //   psfAss_id = recordPsfDisposition(1, db, currentDispo, interactionId);

                                            //rework existingRework = db.reworks.FirstOrDefault(m => m.id == reworkMaxId);

                                            //existingRework.DissatStatus_id = currentDispo;
                                            //existingRework.reworkStatus_id = currentDispo;
                                            //existingRework.resolutionDateAndTime = DateTime.Now;
                                            //existingRework.resolvedOn = rework.resolvedOn;
                                            //existingRework.attendedBy = rework.attendedBy;
                                            //existingRework.resolvedBy = rework.resolvedBy;
                                            //existingRework.natureOfComplaint = rework.natureOfComplaint;
                                            //existingRework.psfassignedInteraction_id = psfAss_id;
                                            //existingRework.discount = rework.discount;
                                            //existingRework.resolutionMode = "Resolved";


                                            //db.reworks.AddOrUpdate(existingRework);
                                            //db.SaveChanges();

                                            newCompInter.reworkAddress = "";
                                            newCompInter.calldisposition_id = currentDispo;
                                            newCompInter.resolutionMode = "Resolved";
                                            callinteraction.isResolved = true;
                                        }

                                        if (Session["LoginUser"].ToString() == "PSFComMgr")
                                        {
                                            callinteraction.isComplaint = true;
                                        }

                                        psfAss_id = newCompInter.psfassignedInteraction_id;
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

                                        callinteraction.makeCallFrom = "PSF";
                                        if (Session["DialedNumber"] != null)
                                        {
                                            callinteraction.dailedNoIs = Session["DialedNumber"].ToString();
                                        }
                                        callinteraction.psfAssignedInteraction_id = psfAss_id;
                                        callinteraction.customer_id = cusId;
                                        callinteraction.vehicle_vehicle_id = vehiId;
                                        callinteraction.wyzUser_id = userId;
                                        callinteraction.agentName = Session["UserName"].ToString();
                                        callinteraction.chasserCall = false;
                                        callinteraction.campaign_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).campaign_id;
                                        callinteraction.service_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).service_id ?? default(long);
                                        db.callinteractions.Add(callinteraction);
                                        db.SaveChanges();
                                        //for (int i = 0; i < listingFormData.remarksList.Count; i++)
                                        //{
                                        //    if (psfinter.creRemarks == null && listingFormData.commentsList[i] != "")
                                        //    {
                                        //        psfinter.creRemarks = listingFormData.commentsList[i];
                                        //    }

                                        //    if (psfinter.customerFeedBack == null && listingFormData.remarksList[i] != "")
                                        //    {
                                        //        psfinter.customerFeedBack = listingFormData.remarksList[i];
                                        //    }
                                        //}

                                        newCompInter.callinteraction_id = callinteraction.id;
                                        db.compInteractions.Add(newCompInter);
                                        db.SaveChanges();
                                        if (Session["GSMUniqueId"] != null)
                                        {
                                            gsmsynchdata gsm = new gsmsynchdata();
                                            gsm.Callinteraction_id = callinteraction.id;
                                            gsm.CallMadeDateTime = DateTime.Now;
                                            gsm.UniqueGsmId = Session["GSMUniqueId"].ToString();
                                            gsm.TenantUrl = Session["GSMUniqueId"].ToString().Split(';')[1];
                                            gsm.callFor = Convert.ToInt32(Session["UserRole1"]);
                                            db.gsmsynchdata.Add(gsm);
                                            db.SaveChanges();
                                        }
                                        induspsfinter.callInteraction_id = callinteraction.id;
                                        induspsfinter.callinteraction = callinteraction;
                                        induspsfinter.callDispositionData_id = currentDispo;
                                        db.indusPSFInteraction.Add(induspsfinter);
                                        db.SaveChanges();
                                    }
                                }

                                dbTrans.Commit();
                                TempData["SubmissionResult"] = "True";
                            }
                        }
                        catch (Exception ex)
                        {
                            dbTrans.Rollback();
                            if (ex.InnerException != null)
                            {
                                if (ex.InnerException.InnerException != null)
                                {
                                    TempData["SubmissionResult"] = ex.InnerException.InnerException.Message;
                                }
                                else
                                {
                                    TempData["SubmissionResult"] = ex.InnerException.Message;
                                }
                            }
                            else
                            {
                                TempData["SubmissionResult"] = ex.Message;
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
                        TempData["SubmissionResult"] = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        TempData["SubmissionResult"] = ex.InnerException.Message;
                    }
                }
                else
                {
                    TempData["SubmissionResult"] = ex.Message;
                }
            }

            if (Session["psfDayType"].ToString() == "0")
            {
                return RedirectToAction("returnToCallLog", new { @psfType = "PSF", @psfDay = 6 });
            }
            else if (Session["psfDayType"].ToString() == "900")
            {
                return RedirectToAction("returnToCallLog", new { @psfType = "PSFComplaint", @psfDay = 0 });
            }
            else
            {
                return RedirectToAction("returnToCallLog", new { @psfType = "PSF", @psfDay = 6 });
            }
        }

        [HttpPost]
        public ActionResult addHyundaipsf(CallLoggingViewModel callLog)
        {
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            long userId = Convert.ToInt32(Session["UserId"].ToString());
            long interactionId = Convert.ToInt32(Session["interactionid"].ToString());
            int currentDispo = 0;
            bool isdissatisfied = false;

            long psfAss_id = 0, mandQsValue = 0;
            callinteraction callinteraction = new callinteraction();
            psfinteraction psfinter = callLog.psfinteraction;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                   
                    if (psfinter != null)
                    {
                        var psfQs = db.psfquestions.Where(m => m.qs_mandatory == true).ToList();
                        if (Session["DealerCode"].ToString() == "BRIDGEWAYMOTORS")
                        {
                            foreach (var pQs in psfQs)
                            {
                                if (psfinter.GetType().GetProperty(pQs.binding_var).GetValue(psfinter, null) != null)
                                {
                                    string answeredValue = (psfinter.GetType().GetProperty(pQs.binding_var).GetValue(psfinter, null).ToString().ToUpper());
                                    var dissatisfiyanswers = pQs.dissatisfiedvalue.Split(',').Select(x => x.Trim()).Select(x => (x).ToUpper()).ToList();

                                    if (dissatisfiyanswers.Contains(answeredValue))
                                    {
                                        isdissatisfied = true;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var pQs in psfQs)
                            {
                                if (pQs.display_type == "drop-down" && string.IsNullOrEmpty(pQs.ddl_options) && psfinter.GetType().GetProperty(pQs.binding_var).GetValue(psfinter, null) != null)
                                {
                                    mandQsValue = long.Parse(psfinter.GetType().GetProperty(pQs.binding_var).GetValue(psfinter, null).ToString());
                                }
                            }
                        }

                        
                    }

                    string contactDispo = callLog.psfinteraction.PSFDispositon;
                    if (callLog.listingForm != null && callLog.listingForm.maindisposition != null && contactDispo == "Call Me Later")
                    {
                        contactDispo = callLog.listingForm.maindisposition;
                    }
                    using (var dbTrans = db.Database.BeginTransaction())
                    {

                        db.Database.CommandTimeout = 900;
                        try
                        {
                            if (db.psfassignedinteractions.Any(m => m.customer_id == cusId && m.vehicle_vehicle_id == vehiId))
                            {
                                if (psfinter.isContacted == "PSF No" || psfinter.isContacted == "No")
                                {
                                    currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == psfinter.PSFDispositon).dispositionId;
                                    psfAss_id = recordPsfDisposition(1, db, currentDispo, interactionId);

                                    if (Session["LoginUser"].ToString() == "PSFComMgr")
                                    {
                                        callinteraction.isComplaint = true;
                                    }
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

                                    callinteraction.makeCallFrom = "PSF";
                                    if (Session["DialedNumber"] != null)
                                    {
                                        callinteraction.dailedNoIs = Session["DialedNumber"].ToString();
                                    }

                                    callinteraction.psfAssignedInteraction_id = psfAss_id;
                                    callinteraction.customer_id = cusId;
                                    callinteraction.vehicle_vehicle_id = vehiId;
                                    callinteraction.wyzUser_id = userId;
                                    callinteraction.agentName = Session["UserName"].ToString();
                                    callinteraction.campaign_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).campaign_id ?? default(long);
                                    callinteraction.service_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).service_id ?? default(long);
                                    callinteraction.chasserCall = false;
                                    db.callinteractions.Add(callinteraction);
                                    db.SaveChanges();

                                    if (Session["GSMUniqueId"] != null)
                                    {
                                        gsmsynchdata gsm = new gsmsynchdata();

                                        gsm.Callinteraction_id = callinteraction.id;
                                        gsm.CallMadeDateTime = DateTime.Now;
                                        gsm.UniqueGsmId = Session["GSMUniqueId"].ToString();
                                        gsm.TenantUrl = Session["GSMUniqueId"].ToString().Split(';')[0];
                                        gsm.callFor = Convert.ToInt32(Session["UserRole1"]);
                                        db.gsmsynchdata.Add(gsm);
                                        db.SaveChanges();
                                    }


                                    if (callLog.listingForm != null && callLog.listingForm.remarksList != null)
                                    {
                                        for (int i = 0; i < callLog.listingForm.remarksList.Count; i++)
                                        {
                                            if (psfinter.remarks == null && callLog.listingForm.commentsList[i] != "")
                                            {
                                                psfinter.remarks = callLog.listingForm.commentsList[i];
                                            }

                                            if (psfinter.comments == null && callLog.listingForm.remarksList[i] != "")
                                            {
                                                psfinter.comments = callLog.listingForm.remarksList[i];
                                            }
                                        }
                                    }




                                    psfinter.callInteraction_id = callinteraction.id;
                                    psfinter.callDispositionData_id = currentDispo;
                                    db.psfinteractions.Add(psfinter);
                                    db.SaveChanges();
                                }
                                else if (psfinter.isContacted == "PSF Yes" || psfinter.isContacted == "Yes")
                                {
                                    if (psfinter.qM4_confirmingCustomer == "Yes" || contactDispo == "Call Me Later" || contactDispo == "Escaltion to CEO" || contactDispo == "PSF_Yes" || callLog.listingForm.maindisposition == "Call Me Later" || callLog.listingForm.maindisposition == "ConfirmStatus")
                                    {
                                            
                                        if (Session["DealerCode"].ToString() == "SUKHMANI" && mandQsValue > 3 && mandQsValue <= 5)
                                        {
                                            currentDispo = db.calldispositiondatas.FirstOrDefault(m => m.disposition == contactDispo).dispositionId;
                                        }
                                        
                                        else if(Session["DealerCode"].ToString() != "PAWANHYUNDAI" && Session["DealerCode"].ToString() != "JDAUTONATION" && Session["DealerCode"].ToString() != "RAJASKODA" && Session["DealerCode"].ToString() != "MAVERICKMOTORS" && Session["DealerCode"].ToString() != "BRIDGEWAYMOTORS" &&  (psfinter.qM4_confirmingCustomer == "Yes" || (mandQsValue <= 9 && mandQsValue > 0)))
                                        {
                                            currentDispo = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Dissatisfied with PSF").dispositionId;
                                        }

                                        else if ((Session["DealerCode"].ToString() == "JDAUTONATION" || Session["DealerCode"].ToString() == "RAJASKODA" || Session["DealerCode"].ToString() == "MAVERICKMOTORS" ) && (psfinter.qM4_confirmingCustomer == "Yes" || (mandQsValue < 9 && mandQsValue > 0) || (mandQsValue < 4 && mandQsValue > 0)))
                                        {
                                            currentDispo = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Dissatisfied with PSF").dispositionId;
                                        }
                                        else if (Session["DealerCode"].ToString() == "BRIDGEWAYMOTORS" && isdissatisfied == true) 
                                        {
                                                currentDispo = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Dissatisfied with PSF").dispositionId;                                           
                                        }
                                        
                                        else
                                        {
                                            if (contactDispo != null && callLog.listingForm.maindisposition != "ConfirmStatus")
                                            {
                                                if (psfinter.qM4_confirmingCustomer == "Yes")
                                                {
                                                    currentDispo = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Dissatisfied with PSF").dispositionId;
                                                }
                                                else
                                                {
                                                    currentDispo = db.calldispositiondatas.FirstOrDefault(m => m.disposition == contactDispo).dispositionId;
                                                }
                                            }
                                            else if (callLog.listingForm.maindisposition == "ConfirmStatus")
                                            {
                                                currentDispo = db.calldispositiondatas.FirstOrDefault(m => m.disposition == psfinter.modeOfService).dispositionId;
                                            }
                                            else
                                            {
                                                currentDispo = db.calldispositiondatas.FirstOrDefault(m => m.disposition == callLog.listingForm.maindisposition).dispositionId;
                                            }

                                        }


                                        psfAss_id = recordPsfDisposition(1, db, currentDispo, interactionId);

                                        if (Session["LoginUser"].ToString() == "PSFComMgr")
                                        {
                                            callinteraction.isComplaint = true;
                                        }
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

                                        callinteraction.makeCallFrom = "PSF";

                                        if (Session["DialedNumber"] != null)
                                        {
                                            callinteraction.dailedNoIs = Session["DialedNumber"].ToString();
                                        }

                                        callinteraction.chasserCall = false;
                                        callinteraction.psfAssignedInteraction_id = psfAss_id;
                                        callinteraction.customer_id = cusId;
                                        callinteraction.vehicle_vehicle_id = vehiId;
                                        callinteraction.wyzUser_id = userId;
                                        callinteraction.agentName = Session["UserName"].ToString();
                                        callinteraction.campaign_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).campaign_id ?? default(long);
                                        callinteraction.service_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).service_id ?? default(long);
                                        db.callinteractions.Add(callinteraction);
                                        db.SaveChanges();

                                        if (Session["GSMUniqueId"] != null)
                                        {
                                            gsmsynchdata gsm = new gsmsynchdata();

                                            gsm.Callinteraction_id = callinteraction.id;
                                            gsm.CallMadeDateTime = DateTime.Now;
                                            gsm.UniqueGsmId = Session["GSMUniqueId"].ToString();
                                            gsm.TenantUrl = Session["GSMUniqueId"].ToString().Split(';')[1];
                                            gsm.callFor = Convert.ToInt32(Session["UserRole1"]);
                                            db.gsmsynchdata.Add(gsm);
                                            db.SaveChanges();
                                        }

                                        //if (contactDispo == "Call Me Later")
                                        //{
                                        //    psfinter.isFollowUpDone = "No";
                                        //    psfinter.isFollowupRequired = "Yes";
                                        //}

                                        if (callLog.listingForm != null && callLog.listingForm.remarksList != null)
                                        {
                                            for (int i = 0; i < callLog.listingForm.remarksList.Count; i++)
                                            {
                                                if (psfinter.remarks == null && callLog.listingForm.commentsList[i] != "")
                                                {
                                                    psfinter.remarks = callLog.listingForm.commentsList[i];
                                                }

                                                if (psfinter.comments == null && callLog.listingForm.remarksList[i] != "")
                                                {
                                                    psfinter.comments = callLog.listingForm.remarksList[i];
                                                }
                                            }
                                        }



                                        psfinter.upsellCount = 0;
                                        psfinter.callInteraction_id = callinteraction.id;
                                        psfinter.callDispositionData_id = currentDispo;
                                        db.psfinteractions.Add(psfinter);
                                        db.SaveChanges();

                                        long upselCount = 0;
                                        if (callLog.listingForm != null)
                                        {
                                            if (callLog.listingForm.LeadYes == "Yes")
                                            {
                                                foreach (var upsel in callLog.listingForm.upsellleads)
                                                {
                                                    if (upsel.taggedTo != null)
                                                    {
                                                        upsel.vehicle_vehicle_id = vehiId;
                                                        upsel.psfInteraction_id = psfinter.id;
                                                        //upsel.psfinteraction_id = psfinter.Id;
                                                        //upsel.srDisposition_id = sr_disposition.id;
                                                        db.upsellleads.Add(upsel);
                                                        db.SaveChanges();
                                                        upselCount++;
                                                    }
                                                }
                                            }
                                        }


                                        if (upselCount != 0)
                                        {
                                            psfinter.upsellCount = upselCount;
                                            db.psfinteractions.AddOrUpdate(psfinter);
                                            db.SaveChanges();
                                        }

                                        if (psfinter.qM4_confirmingCustomer == "Yes" || psfinter.modeOfService == "Dissatisfied with PSF")
                                        {
                                            long complaintMgr_id = 0;
                                            //if (psfinter.modeOfService == "Dissatisfied with PSF")
                                            //{
                                            //    //long maxRwId = db.reworks.Where(m => m.customer_id == cusId && m.vehicle_id == vehiId).Max(m => m.id);
                                            //    //complaintMgr_id = db.reworks.FirstOrDefault(m => m.id == maxRwId).complaint_creid;
                                            //    long maxRwId = db.userworkshops.FirstOrDefault(m => m.userWorkshop_id == userId).workshopList_id;
                                            //    //complaintMgr_id = db.workshops.FirstOrDefault(m => m.id == maxRwId);

                                            //}
                                            if (psfinter.modeOfService == "Dissatisfied with PSF")
                                            {
                                                long maxRwId = db.reworks.Where(m => m.customer_id == cusId && m.vehicle_id == vehiId).Max(m => m.id);
                                                complaintMgr_id = db.reworks.FirstOrDefault(m => m.id == maxRwId).complaint_creid;
                                            }

                                            rework rework = new rework();
                                            wyzuser user = db.wyzusers.FirstOrDefault(m => m.id == userId);
                                            rework.Benefits = "No Benefits Applied";
                                            rework.DissatStatus_id = 44;
                                            if (callLog.rework != null)
                                            {
                                                rework.complaint_creid = callLog.rework.complaint_creid;
                                            }
                                            else
                                            {
                                                rework.complaint_creid = complaintMgr_id;
                                            }

                                            rework.campaign_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).campaign_id ?? default(long);
                                            rework.customer_id = cusId;
                                            rework.vehicle_id = vehiId;
                                            rework.discount = 0;
                                            rework.isReworkAvailable = false;
                                            rework.issuedate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));
                                            rework.location_id = user.location_cityId ?? default(long);
                                            rework.psfassignedInteraction_id = psfAss_id;
                                            rework.workshop_id = user.workshop_id ?? default(long);
                                            db.reworks.Add(rework);
                                            db.SaveChanges();
                                        }
                                    }
                                    else if (contactDispo == "No Resolution - Closed" || contactDispo == "Re-Work" || contactDispo == "Resolved" || contactDispo == "ConfirmStatus")
                                    {
                                        rework rework = callLog.rework;

                                        long reworkMaxId = 0, preCompManagerId = 0;
                                        if (db.reworks.Any(m => m.customer_id == cusId))
                                        {
                                            reworkMaxId = db.reworks.Where(m => m.customer_id == cusId).Max(m => m.id);
                                            preCompManagerId = db.reworks.FirstOrDefault(m => m.id == reworkMaxId).complaint_creid;
                                        }

                                        if (contactDispo == "No Resolution - Closed")
                                        {
                                            currentDispo = db.calldispositiondatas.FirstOrDefault(m => m.disposition == contactDispo).dispositionId;

                                            psfAss_id = recordPsfDisposition(1, db, currentDispo, interactionId);

                                            rework = db.reworks.FirstOrDefault(m => m.id == reworkMaxId);
                                            rework.id = reworkMaxId;
                                            rework.DissatStatus_id = currentDispo;
                                            rework.reworkAddress = "";
                                            rework.reworkStatus_id = currentDispo;
                                            rework.resolutionDateAndTime = DateTime.Now;
                                            rework.psfassignedInteraction_id = psfAss_id;
                                            db.reworks.AddOrUpdate(rework);
                                            db.SaveChanges();
                                        }
                                        else if (contactDispo == "Re-Work")
                                        {
                                            currentDispo = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Rework").dispositionId;

                                            psfAss_id = recordPsfDisposition(1, db, currentDispo, interactionId);
                                            bool isNewInsert = false;
                                            rework existingRework = db.reworks.SingleOrDefault(m => m.id == reworkMaxId);

                                            if (existingRework.reworkStatus_id != 0)
                                            {

                                                rework.Benefits = "No Benefits Applied";
                                                rework.complaint_creid = existingRework.complaint_creid;
                                                rework.campaign_id = existingRework.campaign_id;
                                                rework.customer_id = existingRework.customer_id;
                                                rework.DissatStatus_id = existingRework.DissatStatus_id;
                                                rework.issuedate = existingRework.issuedate;
                                                rework.vehicle_id = existingRework.vehicle_id;
                                                rework.psfassignedInteraction_id = psfAss_id;

                                                rework.reworkStatus_id = currentDispo;
                                                rework.resolutionDateAndTime = DateTime.Now;
                                                if (rework.reworkMode == "Self")
                                                {
                                                    rework.reworkAddress = "";
                                                }
                                                db.reworks.Add(rework);
                                                db.SaveChanges();

                                                existingRework.reworkStatus_id = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Cancelled").id;
                                                db.reworks.AddOrUpdate(existingRework);
                                                db.SaveChanges();
                                            }
                                            else
                                            {
                                                existingRework.reworkStatus_id = currentDispo;
                                                //existingRework.resolutionDateAndTime = DateTime.Now;
                                                existingRework.reworkMode = rework.reworkMode;
                                                if (existingRework.reworkMode == "Self")
                                                {
                                                    existingRework.reworkAddress = "";
                                                }
                                                existingRework.visittype = rework.visittype;
                                                existingRework.psfassignedInteraction_id = psfAss_id;
                                                existingRework.workshop_id = rework.workshop_id;
                                                existingRework.reworkDateAndTime = rework.reworkDateAndTime;
                                                existingRework.resolutionMode = "Schedule a re-visit";
                                                db.reworks.AddOrUpdate(existingRework);
                                                db.SaveChanges();
                                            }
                                        }
                                        else if (contactDispo == "Resolved" || contactDispo == "ConfirmStatus")
                                        {
                                            currentDispo = db.calldispositiondatas.FirstOrDefault(m => m.disposition == contactDispo).dispositionId;

                                            psfAss_id = recordPsfDisposition(1, db, currentDispo, interactionId);

                                            rework existingRework = db.reworks.FirstOrDefault(m => m.id == reworkMaxId);

                                            existingRework.DissatStatus_id = currentDispo;
                                            existingRework.reworkStatus_id = currentDispo;
                                            existingRework.resolutionDateAndTime = DateTime.Now;
                                            existingRework.resolvedOn = rework.resolvedOn;
                                            existingRework.attendedBy = rework.attendedBy;
                                            existingRework.resolvedBy = rework.resolvedBy;
                                            existingRework.natureOfComplaint = rework.natureOfComplaint;
                                            existingRework.psfassignedInteraction_id = psfAss_id;
                                            existingRework.discount = rework.discount;
                                            existingRework.resolutionMode = "Resolved";


                                            db.reworks.AddOrUpdate(existingRework);
                                            db.SaveChanges();
                                        }

                                        if (Session["LoginUser"].ToString() == "PSFComMgr")
                                        {
                                            callinteraction.isComplaint = true;
                                        }

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

                                        callinteraction.makeCallFrom = "PSF";
                                        if (Session["DialedNumber"] != null)
                                        {
                                            callinteraction.dailedNoIs = Session["DialedNumber"].ToString();
                                        }
                                        callinteraction.psfAssignedInteraction_id = psfAss_id;
                                        callinteraction.customer_id = cusId;
                                        callinteraction.vehicle_vehicle_id = vehiId;
                                        callinteraction.wyzUser_id = userId;
                                        callinteraction.agentName = Session["UserName"].ToString();
                                        callinteraction.chasserCall = false;
                                        callinteraction.campaign_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).campaign_id;
                                        callinteraction.service_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).service_id ?? default(long);
                                        db.callinteractions.Add(callinteraction);
                                        db.SaveChanges();

                                        //for (int i = 0; i < callLog.listingForm.remarksList.Count; i++)
                                        //{
                                        //    if (psfinter.remarks == null && callLog.listingForm.commentsList[i] != "")
                                        //    {
                                        //        psfinter.remarks = callLog.listingForm.commentsList[i];
                                        //    }

                                        //    if (psfinter.comments == null && callLog.listingForm.remarksList[i] != "")
                                        //    {
                                        //        psfinter.comments = callLog.listingForm.remarksList[i];
                                        //    }
                                        //}


                                        if (Session["GSMUniqueId"] != null)
                                        {
                                            gsmsynchdata gsm = new gsmsynchdata();
                                            gsm.Callinteraction_id = callinteraction.id;
                                            gsm.CallMadeDateTime = DateTime.Now;
                                            gsm.UniqueGsmId = Session["GSMUniqueId"].ToString();
                                            gsm.TenantUrl = Session["GSMUniqueId"].ToString().Split(';')[1];
                                            gsm.callFor = Convert.ToInt32(Session["UserRole1"]);
                                            db.gsmsynchdata.Add(gsm);
                                            db.SaveChanges();
                                        }
                                        psfinter.callInteraction_id = callinteraction.id;
                                        psfinter.callinteraction = callinteraction;
                                        psfinter.callDispositionData_id = currentDispo;
                                        db.psfinteractions.Add(psfinter);
                                        db.SaveChanges();
                                    }
                                }

                                dbTrans.Commit();
                                TempData["SubmissionResult"] = "True";
                            }
                        }
                        catch (Exception ex)
                        {
                            dbTrans.Rollback();
                            if (ex.InnerException != null)
                            {
                                if (ex.InnerException.InnerException != null)
                                {
                                    TempData["SubmissionResult"] = ex.InnerException.InnerException.Message;
                                }
                                else
                                {
                                    TempData["SubmissionResult"] = ex.InnerException.Message;
                                }
                            }
                            else
                            {
                                TempData["SubmissionResult"] = ex.Message;
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
                        TempData["SubmissionResult"] = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        TempData["SubmissionResult"] = ex.InnerException.Message;
                    }
                }
                else
                {
                    TempData["SubmissionResult"] = ex.Message;
                }
            }

            if (Session["psfDayType"].ToString() == "0")
            {
                return RedirectToAction("returnToCallLog", new { @psfType = "PSF", @psfDay = 3 });
            }
            else if (Session["psfDayType"].ToString() == "900")
            {
                return RedirectToAction("returnToCallLog", new { @psfType = "PSFComplaint", @psfDay = 0 });
            }
            else if (Session["psfDayType"].ToString() == "5")
            {
                return RedirectToAction("returnToCallLog", new { @psfType = "PSF", @psfDay = 10 });
            }
            else if (Session["psfDayType"].ToString() == "10")
            {
                return RedirectToAction("returnToCallLog", new { @psfType = "PSF", @psfDay = 2 });
            }
            else if (Session["psfDayType"].ToString() == "4")
            {
                return RedirectToAction("returnToCallLog", new { @psfType = "PSF", @psfDay = 6 });
            }
            else
            {
                return RedirectToAction("returnToCallLog", new { @psfType = "PSF", @psfDay = 3 });
            }
        }

        public ActionResult getPSFPullOuts(long cubeId)
        {
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string calldisoIds = "", cubeIds = "";
                    var dispoCubeIds = db.psfcallhistorycubes.Where(m => m.vehicle_vehicle_id == vehiId).Select(m => new { cubeId = m.id, dispoId = m.calldispositiondata_id }).ToList();
                    if (dispoCubeIds != null)
                    {
                        calldisoIds = string.Join(",", dispoCubeIds.Select(m => m.dispoId.ToString()).ToList());
                        cubeIds = string.Join(",", dispoCubeIds.Select(m => m.cubeId.ToString()).ToList());
                    }

                    if (Session["DealerCode"].ToString() == "INDUS")
                    {

                        var psfDatarobocall = db.Psfassignmentrecords.Where(m => m.psfassignedInteraction_id == cubeId && m.psfstatus == "Satisfied(RoboCall)").Select(m => new
                        {
                            BillDate = m.BillDate,
                            CustName = m.Customername,
                            ChassisNo = m.ChassisNo,
                            SaleDate = m.SaleDate,
                            ServiceType = m.ServiceType,
                            ServiceLoc = m.workshopname,
                            FinalDispo = m.lastDisposition,
                            FinalDispoId = m.calllDisposition_id,
                            psfstatus = m.psfstatus
                        }).FirstOrDefault();

                        if (psfDatarobocall != null)
                        {
                            return Json(new { success = true, data = psfDatarobocall, calldisoIds, cubeIds });
                        }
                    }
                        var psfData = db.psfcallhistorycubes.Where(m => m.id == cubeId).Select(m =>
                        new
                        {
                            BillDate = m.billDate,
                            CustName = m.customerName,
                            ChassisNo = m.chassisNo,
                            SaleDate = m.saleDate,
                            ServiceType = m.serviceType,
                            ServiceLoc = m.Serviced_location,
                            VisitType = m.IsSatisfied,
                            FinalDispo = m.SecondaryDisposition,
                            FinalDispoId = m.calldispositiondata_id
                        }).FirstOrDefault();
                        if (psfData != null)
                        {
                            return Json(new { success = true, data = psfData, calldisoIds, cubeIds });
                        }
                        else
                        {
                            return Json(new { success = true, exception = "No Data Found" });
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

                return Json(new { success = false, exception = exception });
            }
        }

        public ActionResult getPSFDispoDetails(long cubeId)
        {
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            try
            {
                List<calldispositiondata> technical = new List<calldispositiondata>();
                List<calldispositiondata> nontechnical = new List<calldispositiondata>();

                using (var db = new AutoSherDBContext())
                {
                    if (Session["DealerCode"].ToString() == "INDUS" || Session["DealerCode"].ToString() == "KATARIA" || Session["DealerCode"].ToString() == "SPEEDAUTO" || Session["DealerCode"].ToString() == "JAYABHERIAUTO")
                    {
                        var psfHistory = db.psfcallhistorycubes.Where(m => m.id == cubeId).Select(m => new
                        {
                            escalation = m.SAescalationSticker,
                            instfeed = m.SAInstantFeedBack,
                            qos = m.qos,
                            overallexpo = m.overallServiceExperience,
                            ratesa = m.rateSA,
                            istechnical = m.isTechnical,
                            isnontechnical = m.nonTechnincal,
                            vehicleafter = m.vehicleAfterService,
                            electricals = m.electricals,
                            bodychasis = m.bodychasis,
                            performance = m.performance,
                            powertrain = m.powertrain,
                            steeringNsuspention = m.steeringNsuspention,
                            safety = m.safety,
                            improperExpectNUsage = m.improperExpectNUsage,
                            workQuality = m.workQuality,
                            ServiceAdvisor = m.ServiceAdvisor,
                            spareParts = m.spareParts,
                            billing = m.billing,
                            delivery = m.delivery,
                            othernonTech = m.othernonTech,
                            finaldispo = m.calldispositiondata_id,
                            natureofcomplaints = m.natureofcomplaints,
                            demandedrepair = m.demandedrepairsCompleted,
                            promiseddlvrytime = m.promiseddeliveryTime,
                            washQualty = m.washingQuality,
                            servicecharges = m.vehicleserviceCharges,m.campaign_id,
                            m.qFordQ1,m.qFordQ2,m.qFordQ3,m.qFordQ4,m.qFordQ5,m.qFordQ6,m.qFordQ7,m.qFordQ8,m.qFordQ9,m.qFordQ10,m.qFordQ11,m.qFordQ12,m.qFordQ13,m.qFordQ14,m.qFordQ15,m.qFordQ16,
                            m.qFordQ17,m.qFordQ18,m.qFordQ19,m.qFordQ20,m.qFordQ21,m.qFordQ22,m.qFordQ23,m.qFordQ24,m.qFordQ25,m.qFordQ26,m.qFordQ27,m.isbodyshop,m.iscei,m.calldispositiondata_id,m.Remarks,m.Comments
                        }).FirstOrDefault();

                        if (psfHistory != null)
                        {

                            if (psfHistory.campaign_id == 5)
                            {
                                string psf_format = string.Empty;
                                if (psfHistory.isbodyshop==true)
                                {
                                    psf_format = "BCEI";
                                }
                                else
                                {
                                    psf_format = "CEI";
                                }
                                var psf_questions = db.Induspostservicefollowupquestions.Where(m => m.campaignid == psfHistory.campaign_id && m.isActive == true && m.psf_format == psf_format).ToList();
                                if (psf_questions != null)
                                {
                                    return Json(new { success = true, data = psfHistory, psf_questions, campaign_id=psfHistory.campaign_id });
                                }
                                else
                                {
                                    return Json(new { success = true, exception = "No Data Found" });
                                }
                            }
                            else
                            {

                                long techId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Technical").id;
                                long nontechId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Non Technical").id;

                                technical = db.calldispositiondatas.Where(m => m.mainDispositionId == techId).ToList();
                                nontechnical = db.calldispositiondatas.Where(m => m.mainDispositionId == nontechId).ToList();

                                List<calldispositiondata> callDispo;
                                callDispo = new List<calldispositiondata>();

                                foreach (var dispo in technical)
                                {
                                    callDispo.AddRange(db.calldispositiondatas.Where(m => m.mainDispositionId == dispo.id));
                                }
                                technical.AddRange(callDispo);
                                callDispo.Clear();

                                foreach (var dispo in nontechnical)
                                {
                                    callDispo.AddRange(db.calldispositiondatas.Where(m => m.mainDispositionId == dispo.id));
                                }
                                nontechnical.AddRange(callDispo);
                                callDispo.Clear();
                                string natureOfComplaints = "";
                            var tech = JsonConvert.SerializeObject(technical.Select(m => new { id = m.dispositionId, disposition = m.disposition, mainDispositionId = m.mainDispositionId }).ToList());
                            var nontech = JsonConvert.SerializeObject(nontechnical.Select(m => new { id = m.dispositionId, disposition = m.disposition, mainDispositionId = m.mainDispositionId }).ToList());

                            var dispoCubeIds = db.psfcallhistorycubes.Where(m => m.vehicle_vehicle_id == vehiId).Select(m => new { cubeId = m.id, dispoId = m.calldispositiondata_id }).ToList();

                            if (dispoCubeIds.Select(m => m.dispoId).ToList().Contains(63))
                            {
                                int cubeid = dispoCubeIds.FirstOrDefault(m => m.dispoId == 63).cubeId;
                                natureOfComplaints = db.psfcallhistorycubes.FirstOrDefault(m => m.id == cubeid).natureofcomplaints;
                                if (!string.IsNullOrEmpty(natureOfComplaints))
                                {
                                    List<int> natureOfComplaintList = natureOfComplaints.Split(',').Select(int.Parse).ToList();
                                    natureOfComplaints = string.Join(",", db.calldispositiondatas.Where(m => natureOfComplaintList.Contains(m.dispositionId)).Select(m => m.disposition).ToList());
                                }

                            }
                            //if (!string.IsNullOrEmpty(psfHistory.natureofcomplaints))
                            //{
                            //    List<int> natureOfComplaintList = psfHistory.natureofcomplaints.Split(',').Select(int.Parse).ToList();
                            //    natureOfComplaints = string.Join(",",db.calldispositiondatas.Where(m => natureOfComplaintList.Contains(m.dispositionId)).Select(m => m.disposition).ToList());
                            //}

                            var data = JsonConvert.SerializeObject(psfHistory);
                            return Json(new { success = true, data = psfHistory, techincaldata = tech, nontechnicaldata = nontech, natureOfComplaints = natureOfComplaints, campaign_id=0 }, JsonRequestBehavior.AllowGet);
                        }
                        }
                    }
                    else if (Session["DealerCode"].ToString() == "KALYANIMOTORS")
                    {
                        string complaintCategory = "";
                        var psfHistory = db.psfcallhistorycubes.Where(m => m.id == cubeId).Select(m => new
                        {
                            modeOfServiceDone = m.modeOfServiceDone,
                            qFordQ1 = m.qFordQ1,
                            qFordQ2 = m.qFordQ2,
                            qFordQ3 = m.qFordQ3,
                            afterServiceComments = m.afterServiceComments,
                            qFordQ5 = m.qFordQ5,
                            qFordQ6 = m.qFordQ6,
                            qFordQ7 = m.qFordQ7,
                            qM2_ReasonOfAreaOfImprovement = m.qM2_ReasonOfAreaOfImprovement,
                            qFordQ9 = m.qFordQ9,
                            bodyrepairthrough = m.bodyrepairthrough,
                            experienceOnIns = m.experienceOnIns,
                            qualityofwork = m.qualityofwork
                        }).FirstOrDefault();

                        if (psfHistory != null)
                        {
                            long? dispoCubeIds = db.psfcallhistorycubes.FirstOrDefault(m => m.id == cubeId).Compliant_Category_id;
                            if (dispoCubeIds != 0 && dispoCubeIds != null)
                            {
                                complaintCategory = db.calldispositiondatas.FirstOrDefault(m => m.id == dispoCubeIds).disposition;
                            }
                        }
                        var data = JsonConvert.SerializeObject(psfHistory);
                        return Json(new { success = true, data = psfHistory, complaintCategory = complaintCategory }, JsonRequestBehavior.AllowGet);
                    }
                    else if (Session["OEM"].ToString() == "HYUNDAI" || Session["DealerCode"].ToString() == "JDAUTONATION")
                    {
                        psfcallhistorycube psf = new psfcallhistorycube();
                        psf = db.psfcallhistorycubes.FirstOrDefault(m => m.id == cubeId);
                        List<int> psfQsIdList = new List<int>();
                        List<psfquestions> psfQs = new List<psfquestions>();
                        if (psf.psfquestions_id != null)
                        {
                            psfQsIdList = psf.psfquestions_id.Split(',').Select(int.Parse).ToList();
                            psfQs = db.psfquestions.Where(m => psfQsIdList.Contains(m.id)).ToList();

                            for (int i = 0; i < psfQsIdList.Count(); i++)
                            {
                                string binding_var = psfQs[i].binding_var;

                                if (psf.GetType().GetProperty(psfQs[i].binding_var).GetValue(psf, null) != null)
                                {
                                    psfQs[i].ans = psf.GetType().GetProperty(psfQs[i].binding_var).GetValue(psf, null).ToString();
                                }

                            }

                            string selectedRadio = psf.IsSatisfied;

                            return Json(new { success = true, data = psfQs.Select(m => new { question = m.question, ans = m.ans }), selectedRadio });
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

                return Json(new { success = false, exception = exception });
            }

            return Json(new { success = false, exception = "No Data Found" });
        }


        public ActionResult getRCSub(string rc)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    long DispoId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == rc).dispositionId;

                    if (DispoId != 0)
                    {
                        var data = db.calldispositiondatas.Where(m => m.mainDispositionId == DispoId).Select(m => new { name = m.disposition }).ToList();
                        return Json(new { success = true, data = data });
                    }
                    else
                    {
                        return Json(new { success = false, exception = "No RC Subtypes found" });
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

                return Json(new { success = false, exception = exception });
            }
        }

        #region NewPSF SavingFlow Changed
        //PSF HTTP Get Method
        [HttpGet, ActionName("CallLogging_IndusPSF")]
        public ActionResult CallLoggingIndus_PSF(string id)
        {

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

            if (Session["inComingParameter"] != null)
            {
                id = string.Empty;
                id = Session["inComingParameter"].ToString();
            }
            else
            {
                TempData["Exceptions"] = "Invalid Url Operation..[CallLogging]....";
                if (Session["UserRole"].ToString() != "CREManager" && Session["UserRole"].ToString() != "RM")
                {
                    return RedirectToAction("LogOff", "Home");
                }
                //return RedirectToAction("LogOff", "Home");
            }

            CallLoggingViewModel callLog = new CallLoggingViewModel();
            ViewBag.typeOfDispo = "PSF";

            long cid, vehicle_id, interactionid, dispositionHistory, typeOfPSF = 0, lastworkshopId = 0, bucket_id = 0;
            string pageFor;
            pageFor = id.Split(',')[0];
            cid = Convert.ToInt32(id.Split(',')[1]);
            vehicle_id = Convert.ToInt32(id.Split(',')[2]);
            interactionid = Convert.ToInt32(id.Split(',')[3]);
            dispositionHistory = Convert.ToInt32(id.Split(',')[4]);


            ViewBag.interactionid = interactionid;


            if (dispositionHistory == 900)
            {
                bucket_id = Convert.ToInt32(id.Split(',')[5]);
                ViewBag.dispositionHistory = "PSFComplaints";
                ViewBag.typeOfPSF = 900;
            }
            else if (dispositionHistory == 500)
            {
                Session["isPSFRM"] = true;
                ViewBag.dispositionHistory = "PSFRM";
            }
            else if (dispositionHistory == 63)
            {
                typeOfPSF = Convert.ToInt32(id.Split(',')[5]);
                ViewBag.typeOfPSF = typeOfPSF;
                Session["psfDayType"] = typeOfPSF;
                callLog.PsfCreBucketId = 63;
                ViewBag.isResolved = true;
                ViewBag.dispositionHistory = "PSFDays";
            }
            else
            {
                typeOfPSF = Convert.ToInt32(id.Split(',')[5]);
                ViewBag.typeOfPSF = typeOfPSF;
                Session["psfDayType"] = typeOfPSF;
                ViewBag.isResolved = false;
                ViewBag.dispositionHistory = "PSFDays";
            }

            ViewBag.vehiId = vehicle_id;

            long UserId = Convert.ToInt32(Session["UserId"].ToString());

            PSF_CallLogging psf = new PSF_CallLogging();

            callLog.workshopList = new List<workshop>();
            callLog.wyzuser = new wyzuser();
            callLog.lastPSFAssign = new psfassignedinteraction();
            callLog.lastPSFAssignStatus = new psfassignedinteraction();
            callLog.LatestInsurance = new insurance();
            callLog.psfLastInteraction = new psfinteraction();
            callLog.cust = new customer();
            callLog.vehi = new vehicle();
            callLog.rework = new rework();
            callLog.templates = new List<smstemplate>();
            callLog.lastService = new service();
            callLog.LatestInsurance = new insurance();
            callLog.smrCall = new smrforecasteddata();
            callLog.listingForm = new ListingForm();
            callLog.listingForm.upsellleads = new List<upselllead>();
            callLog.psfPullOuts = new List<PsfPullOut>();
            callLog.emailtemplates = new List<emailtemplate>();
            callLog.Latestservices = new service();
            callLog.finaldispostion = new calldispositiondata();
            callLog.indusPsfInteraction = new IndusPSFInteraction();
            callLog.custPhoneList = new List<phone>();
            callLog.compInteractions = new CompInteraction();
            callLog.rmInteraction = new RMInteraction();

            //callLog.listingForm.upsellleads = new List<upselllead>();

            try
            {
                using (var db = new AutoSherDBContext())
                {

                    long assignWyzId = 0;
                    if (pageFor == "Shw")
                    {
                        Session["PageFor"] = "CRE";
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

                    Session["PSFDay"] = "6thDay";

                    //Session["PageFor"] =
                    Session["CusId"] = cid;
                    Session["VehiId"] = vehicle_id;
                    Session["interactionid"] = interactionid;
                    Session["typeOfDispo"] = "PSF";
                    //callLog.workshopList = db.workshops.ToList();

                    long countOfServicePresent = db.services.Where(m => m.vehicle.vehicle_id == vehicle_id && m.lastServiceDate != null).Count(); //call_int_repo.getCountOfServiceHistoryOfVehicle(vehicleData.getVehicle_id());
                    ViewBag.countOfServicePresent = countOfServicePresent;


                    callLog.CustomerId = cid;
                    callLog.VehicleId = vehicle_id;
                    callLog.UserId = UserId;
                    callLog = new CallLoggingController().get360ProfileData(callLog, db, "PSF", Session["DealerCode"].ToString(), Session["OEM"].ToString());
                    Session["VehiReg"] = callLog.vehi.chassisNo;

                    //************ Taking UpsellLead from Tagging User***************
                    //newly added start
                    List<TaggingView> allTags = new List<TaggingView>();
                    if (Session["DealerCode"].ToString() == "KATARIA")
                    {
                        callLog.tags = db.Database.SqlQuery<TaggingView>("select distinct(upsellLeadId) from taggingusers").ToList();
                        allTags = db.Database.SqlQuery<TaggingView>("select upsellLeadId,name,upsellType,wyzUser_id from taggingusers").ToList();
                    }
                    else { 
                    callLog.tags = db.Database.SqlQuery<TaggingView>("select distinct(upsellLeadId) from taggingusers where moduleTypeId=4 or moduleTypeId=5;").ToList();
                        //List<TaggingView> allTags = db.Database.SqlQuery<TaggingView>("select upsellLeadId,name,upsellType,wyzUser_id from taggingusers where moduleTypeId=4 or moduleTypeId=5;").ToList();
                        allTags = db.Database.SqlQuery<TaggingView>("select upsellLeadId,name,upsellType,wyzUser_id from taggingusers where moduleTypeId=4 or moduleTypeId=5;").ToList();
                    }
                    //newly added end

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
                    if (dispositionHistory == 900 || dispositionHistory == 500)
                    {


                        //callLog.listingForm.upsellleads = new List<upselllead>();
                        for (int i = 0; i < callLog.tags.Count(); i++)
                        {
                            callLog.listingForm.upsellleads.Add(new upselllead());
                        }
                        //long maxReowrkId = db.reworks.Where(m => m.customer_id == cid).Max(m => m.id);
                        //if(maxReowrkId!=0)
                        //{
                        //    callLog.rework = db.reworks.FirstOrDefault(m => m.id == maxReowrkId);
                        //}

                        if (dispositionHistory == 900)
                        {
                            long dispoComplaintId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Exact Nature of Complaint").id;
                            var natureOfComplaints = db.calldispositiondatas.Where(m => m.mainDispositionId == dispoComplaintId).ToList();
                            ViewBag.natureOfComplaints = natureOfComplaints;


                            long RMId = db.roles.FirstOrDefault(m => m.role1 == "RM").id;
                            var RMIdList = db.userroles.Where(m => m.roles_id == RMId).Select(m => m.users_id).ToList();
                            var RMList = db.wyzusers.Where(m => m.role == "RM").Select(m => new { id = m.id, name = m.userName }).ToList();
                            //var RMList = db.wyzusers.Where(m => m.role == "RM" && m.workshop_id == callLog.wyzuser.workshop_id).Select(m => new { id = m.id, name = m.userName }).ToList();
                            ViewBag.RMList = RMList;
                            ViewBag.locationList = db.locations.Select(m => new { id = m.cityId, name = m.name }).ToList();

                            CompInteraction oldCompInter = db.compInteractions.Where(m => m.psfassignedInteraction_id == interactionid).OrderByDescending(m => m.Id).FirstOrDefault();
                            if (oldCompInter != null)
                            {
                                callLog.compInteractions = oldCompInter;
                                callLog.compInteractions.bucket_id = Convert.ToInt32(bucket_id);
                            }
                        }
                        else if (dispositionHistory == 500)
                        {
                            RMInteraction oldRmInter = db.rmInteractions.Where(m => m.psfassignedinteraction_id == interactionid).OrderByDescending(m => m.Id).FirstOrDefault();

                            if (oldRmInter != null)
                            {
                                callLog.rmInteraction = oldRmInter;
                            }
                        }
                    }
                    else
                    {
                        if (dispositionHistory != 63)
                        {
                            long techId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Technical").id;
                            long nontechId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Non Technical").id;


                            callLog.techQuestions = db.calldispositiondatas.Where(m => m.mainDispositionId == techId).ToList();
                            callLog.nonTechQuestion = db.calldispositiondatas.Where(m => m.mainDispositionId == nontechId).ToList();

                            List<calldispositiondata> callDispo;
                            callDispo = new List<calldispositiondata>();

                            foreach (var dispo in callLog.techQuestions)
                            {
                                callDispo.AddRange(db.calldispositiondatas.Where(m => m.mainDispositionId == dispo.id));
                            }
                            callLog.techQuestions.AddRange(callDispo);
                            callDispo.Clear();

                            foreach (var dispo in callLog.nonTechQuestion)
                            {
                                callDispo.AddRange(db.calldispositiondatas.Where(m => m.mainDispositionId == dispo.id));
                            }
                            callLog.nonTechQuestion.AddRange(callDispo);
                            callDispo.Clear();

                            long complaintMngId = 0;

                            if (db.roles.Any(m => m.role1 == "Complaint Manager"))
                            {
                                complaintMngId = db.roles.FirstOrDefault(m => m.role1 == "Complaint Manager").id;
                            }

                            if (complaintMngId != 0)
                            {
                                lastworkshopId = db.psfassignedinteractions.FirstOrDefault(m => m.id == interactionid).workshop_id ?? default(long);
                                //long loginUserWorkshopId = db.wyzusers.FirstOrDefault(m => m.id == UserId).workshop_id ?? default(long);
                                var complaintCreId = db.userroles.Where(m => m.roles_id == complaintMngId).Select(m => m.users_id).ToList();
                                var complaintCre = db.wyzusers.Where(m => complaintCreId.Contains(m.id) && m.workshop_id == lastworkshopId).Select(m => new { id = m.id, name = m.userName }).ToList();
                                ViewBag.complaintCre = complaintCre;
                            }
                            else
                            {
                                ViewBag.complaintCre = new List<SelectList>();
                            }
                        }
                        else
                        {
                            callLog.PsfCreBucketId = 63;
                        }

                        if (db.psfassignedinteractions.Count(m => m.id == interactionid) > 0)
                        {
                            bool? isbodyshop = db.psfassignedinteractions.FirstOrDefault(m => m.id == interactionid).isbodyshop;
                            if (isbodyshop == true)
                            {
                                callLog.postsalespsfQuestionType = "BCEI";
                            }
                            else
                            {
                                callLog.postsalespsfQuestionType = "CEI";
                            }
                        }

                        if (Session["psfDayType"].ToString() == "5" && Session["DealerCode"].ToString() == "INDUS")
                        {
                            callLog.isCEIPSF = true;

                            callLog.induspostservicefollowupquestions = db.Induspostservicefollowupquestions.Where(m => m.campaignid == typeOfPSF && m.isActive == true && m.psf_format == callLog.postsalespsfQuestionType).ToList();
                            ViewBag.totalMandatoryQustn = db.Induspostservicefollowupquestions.Count(m => m.campaignid == typeOfPSF && m.isActive == true && m.psf_format == callLog.postsalespsfQuestionType && m.qs_mandatory == true);


                            for (int i = 0; i < callLog.induspostservicefollowupquestions.Count(); i++)
                            {
                                if (callLog.induspostservicefollowupquestions[i].display_type == "drop-down")
                                {
                                    if ((!string.IsNullOrEmpty(callLog.induspostservicefollowupquestions[i].ddl_text)) && (!(string.IsNullOrEmpty(callLog.induspostservicefollowupquestions[i].ddl_values))))
                                    {
                                        callLog.induspostservicefollowupquestions[i].DDLOptionTextList = callLog.induspostservicefollowupquestions[i].ddl_text.Split(',').ToList();
                                        callLog.induspostservicefollowupquestions[i].DDLOptionValueList = callLog.induspostservicefollowupquestions[i].ddl_values.Split(',').ToList();
                                        callLog.induspostservicefollowupquestions[i].dictionaryDDLQuestionList = callLog.induspostservicefollowupquestions[i].DDLOptionTextList.ToDictionary(x => x, x => callLog.induspostservicefollowupquestions[i].DDLOptionValueList[callLog.induspostservicefollowupquestions[i].DDLOptionTextList.IndexOf(x)]);
                                    }
                                }
                                else if (callLog.induspostservicefollowupquestions[i].display_type == "radio-button")
                                {
                                    callLog.induspostservicefollowupquestions[i].RadioTextList = callLog.induspostservicefollowupquestions[i].radio_options.Split(',').ToList();
                                    callLog.induspostservicefollowupquestions[i].RadioValueList = callLog.induspostservicefollowupquestions[i].radio_values.Split(',').ToList();
                                    callLog.induspostservicefollowupquestions[i].dictionaryRDOQuestionList = callLog.induspostservicefollowupquestions[i].RadioTextList.ToDictionary(x => x, x => callLog.induspostservicefollowupquestions[i].RadioValueList[callLog.induspostservicefollowupquestions[i].RadioTextList.IndexOf(x)]);

                                }
                            }
                        }

                    }

                    var ddlTagging = db.campaigns.Where(m => m.campaignType == "TaggingSMR" && m.isactive == true && m.campaignName != "ISP" && m.campaignName != "ISP/MCP/EW" && m.campaignName != "ISP/MCP" && m.campaignName != "ISP/EW").Select(model => new { id = model.id, TaggingCamp = model.campaignName }).ToList();
                    ViewBag.ddltagging = ddlTagging;
                    callLog.selectedTagList = callLog.cust.leadtag?.Split(',')?.ToList();


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

                TempData["Exceptions"] = exception;
                TempData["ControllerName"] = "Call Log";
                return RedirectToAction("LogOff", "Home");
            }

            return View("~/Views/CallLogging/Call_Logging.cshtml", callLog);
        }

        [HttpPost]
        public ActionResult addIndusPsfDispo(CallLoggingViewModel callLog)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");

            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            long userId = Convert.ToInt32(Session["UserId"].ToString());
            long interactionId = Convert.ToInt32(Session["interactionid"].ToString());
            string submissionResult = string.Empty;
            IndusPSFInteraction psfinter = callLog.indusPsfInteraction;
            ListingForm listingFormData = callLog.listingForm;
            psfassignedinteraction psfassignedinteraction = new psfassignedinteraction();
            callinteraction callinteraction = new callinteraction();
            int currentDispo = 0;
            long psfAss_id = 0;
            long complaintMngrId = 0;
            //using (var db = new AutoSherDBContext())
            //{
            using (var db = new AutoSherDBContext())
            {
                using (DbContextTransaction dbTras = db.Database.BeginTransaction())
                {
                    try
                    {
                        logger.Info("\n PSF Days Incoming disposition: CustId:" + cusId + " VehiId:" + vehiId + " User:" + Session["UserName"].ToString());

                        if (callLog.CustomerId != cusId && callLog.VehicleId != vehiId)
                        {
                            TempData["Exceptions"] = "Invalid Disposition Found...";
                            return RedirectToAction("LogOff", "Home");
                        }


                        if (db.psfassignedinteractions.Count(m => m.customer_id == cusId && m.vehicle_vehicle_id == vehiId) > 0)
                        {
                            if (psfinter.isContacted == "No")
                            {
                                currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == psfinter.PSFDisposition).dispositionId;
                                //psfAss_id = recordPSFDisposition(2, db, currentDispo, interactionId);
                                psfAss_id = recordPsfDisposition(1, db, currentDispo, interactionId);

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

                                if (callLog.PsfCreBucketId == 63)
                                {
                                    callinteraction.isResolved = true;
                                }

                                callinteraction.psfAssignedInteraction_id = psfAss_id;
                                callinteraction.customer_id = cusId;
                                callinteraction.vehicle_vehicle_id = vehiId;
                                callinteraction.wyzUser_id = userId;
                                callinteraction.chasserCall = false;
                                callinteraction.agentName = Session["UserName"].ToString();
                                callinteraction.campaign_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).campaign_id ?? default(long);
                                callinteraction.service_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).service_id ?? default(long);
                                db.callinteractions.Add(callinteraction);
                                db.SaveChanges();

                                if (listingFormData != null)
                                {
                                    for (int i = 0; i < listingFormData.remarksList.Count; i++)
                                    {
                                        if (psfinter.creRemarks == null && listingFormData.commentsList[i] != "")
                                        {
                                            psfinter.creRemarks = listingFormData.commentsList[i];
                                        }

                                        if (psfinter.customerFeedBack == null && listingFormData.remarksList[i] != "")
                                        {
                                            psfinter.customerFeedBack = listingFormData.remarksList[i];
                                        }
                                    }
                                }
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

                                psfinter.dispositionFrom = "PSFCRE";
                                psfinter.callInteraction_id = callinteraction.id;
                                psfinter.callDispositionData_id = currentDispo;
                                psfinter.demandedrepairsCompleted = null;
                                psfinter.SAescalationSticker = null;
                                psfinter.promiseddeliveryTime = null;
                                psfinter.washingQuality = null;
                                psfinter.vehicleserviceCharges = null;
                                psfinter.qos = null;
                                db.indusPSFInteraction.Add(psfinter);
                                db.SaveChanges();
                            }
                            else if (psfinter.isContacted == "Yes")
                            {


                                if (callLog.isCEIPSF == true)
                                {
                                    bool isdissatisfied = false;

                                    //List<string> mandQsValue = new List<string>();

                                    if (psfinter != null)
                                    {
                                        var psfQs = db.Induspostservicefollowupquestions.Where((m => m.campaignid == psfinter.psfCampaign && m.isActive == true && m.psf_format == callLog.indusPsfInteraction.psfFormat && m.qs_mandatory == true)).ToList();

                                        foreach (var pQs in psfQs)
                                        {
                                            if (psfinter.GetType().GetProperty(pQs.binding_var).GetValue(psfinter, null) != null)
                                            {
                                                //mandQsValue.Add(psfinter.GetType().GetProperty(pQs.binding_var).GetValue(psfinter, null).ToString().ToUpper());
                                                string answeredValue = (psfinter.GetType().GetProperty(pQs.binding_var).GetValue(psfinter, null).ToString().ToUpper());
                                                var dissatisfiyanswers = pQs.dissatisfiedvalue.Split(',').Select(x => x.Trim()).Select(x => (x).ToUpper()).ToList();

                                                if (dissatisfiyanswers.Contains(answeredValue))
                                                {
                                                    isdissatisfied = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    if (isdissatisfied == true)
                                    // if ((mandQsValue.Contains("POOR") || mandQsValue.Contains("FAIR") || mandQsValue.Contains("GOOD") || mandQsValue.Contains("0") || mandQsValue.Contains("50") || mandQsValue.Contains("-50") || mandQsValue.Contains("NO")))
                                    {
                                        currentDispo = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Dissatisfied with PSF").dispositionId;
                                    }
                                    else if (psfinter.whatCustSaid == "Call Me Later" || psfinter.whatCustSaid == "FEEDBACK" || psfinter.whatCustSaid == "NO FEEDBACK")
                                    {
                                        if (psfinter.whatCustSaid == "Call Me Later")
                                        {
                                            currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == "Call Me Later").dispositionId;
                                        }
                                        else if (psfinter.whatCustSaid == "NO FEEDBACK")
                                        {
                                            currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == "Incomplete Survey").dispositionId;
                                        }
                                        else
                                        {
                                            currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == "PSF_Yes").dispositionId;
                                        }
                                    }
                                    psfAss_id = recordPsfDisposition(1, db, currentDispo, interactionId);

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
                                    callinteraction.campaign_id = long.Parse(Session["psfDayType"].ToString());
                                    callinteraction.chasserCall = false;
                                    callinteraction.service_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).service_id ?? default(long);
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


                                    psfinter.dispositionFrom = "PSFCRE";
                                    psfinter.callInteraction_id = callinteraction.id;
                                    psfinter.callDispositionData_id = currentDispo;
                                    psfinter.demandedrepairsCompleted = null;
                                    psfinter.SAescalationSticker = null;
                                    psfinter.promiseddeliveryTime = null;
                                    psfinter.washingQuality = null;
                                    psfinter.vehicleserviceCharges = null;
                                    psfinter.qos = null;
                                    db.indusPSFInteraction.Add(psfinter);
                                    db.SaveChanges();

                                }
                                else
                                {
                                    string remarks = "";
                                    if (psfinter.whatCustSaid == "Feedback Given" && psfinter.vehicleAfterService == "Bad" || (psfinter.overallServiceExperience == "Average" || psfinter.overallServiceExperience == "Poor") || psfinter.overallcustsatisfaction == "DissatisfiedKAT")
                                    {

                                        currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == "Dissatisfied with PSF").dispositionId;
                                        psfAss_id = recordPsfDisposition(1, db, currentDispo, interactionId);

                                        if (psfinter.isTechnical == true)
                                        {
                                            Dictionary<string, string> techOption = new Dictionary<string, string>();
                                            techOption = JsonConvert.DeserializeObject<Dictionary<string, string>>(callLog.listingForm.selectedTechnical);

                                            foreach (var key in techOption)
                                            {
                                                long dispoId = long.Parse(key.Key);
                                                string techSuboption = string.Empty;
                                                techSuboption = db.calldispositiondatas.FirstOrDefault(m => m.id == dispoId).disposition;

                                                if (techSuboption == "Body, Chassis & Upholstery")
                                                {
                                                    psfinter.bodychasis = key.Value;
                                                }
                                                else if (techSuboption == "Electricals")
                                                {
                                                    psfinter.electricals = key.Value;
                                                }
                                                else if (techSuboption == "Performance")
                                                {
                                                    psfinter.performance = key.Value;
                                                }
                                                else if (techSuboption == "Powertrain")
                                                {
                                                    psfinter.powertrain = key.Value;
                                                }
                                                else if (techSuboption == "Safety")
                                                {
                                                    psfinter.safety = key.Value;
                                                }
                                                else if (techSuboption == "Steering & Suspension")
                                                {
                                                    psfinter.steeringNsuspention = key.Value;
                                                }
                                                else if (techSuboption == "Improper Expectation/Usage")
                                                {
                                                    psfinter.improperExpectNUsage = key.Value;
                                                }
                                            }
                                        }

                                        if (psfinter.nonTechnincal == true)
                                        {
                                            Dictionary<string, string> nonTechOption = new Dictionary<string, string>();
                                            nonTechOption = JsonConvert.DeserializeObject<Dictionary<string, string>>(callLog.listingForm.selectedNonTechnical);

                                            foreach (var key in nonTechOption)
                                            {
                                                long dispoId = long.Parse(key.Key);
                                                string techSuboption = string.Empty;
                                                techSuboption = db.calldispositiondatas.FirstOrDefault(m => m.id == dispoId).disposition;

                                                if (techSuboption == "Work Quality")
                                                {
                                                    psfinter.workQuality = key.Value;
                                                }
                                                else if (techSuboption == "Service Advisor")
                                                {
                                                    psfinter.ServiceAdvisor = key.Value;
                                                }
                                                else if (techSuboption == "Spare Parts")
                                                {
                                                    psfinter.spareParts = key.Value;
                                                }
                                                else if (techSuboption == "Billing")
                                                {
                                                    psfinter.billing = key.Value;
                                                }
                                                else if (techSuboption == "Delivery")
                                                {
                                                    psfinter.delivery = key.Value;
                                                }
                                                else if (techSuboption == "Others")
                                                {
                                                    psfinter.othernonTech = key.Value;
                                                }
                                            }
                                        }


                                        //*****************Call Interaction *******************************
                                        if (callLog.PsfCreBucketId == 63)
                                        {
                                            callinteraction.isResolved = true;
                                        }
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
                                        callinteraction.campaign_id = long.Parse(Session["psfDayType"].ToString());
                                        callinteraction.chasserCall = false;
                                        callinteraction.service_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).service_id ?? default(long);
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
                                        //******************* rework table**************************
                                        //rework rework = new rework();
                                        wyzuser user = db.wyzusers.FirstOrDefault(m => m.id == userId);
                                        //rework.Benefits = "No Benefits Applied";
                                        //rework.DissatStatus_id = 44;
                                        //rework.complaint_creid = psfinter.complaintMgr_id ?? default(long);
                                        //rework.campaign_id = long.Parse(Session["psfDayType"].ToString());
                                        //rework.customer_id = cusId;
                                        //rework.vehicle_id = vehiId;
                                        //rework.discount = 0;
                                        //rework.isReworkAvailable = false;
                                        //rework.issuedate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));
                                        //rework.location_id = user.location_cityId ?? default(long);
                                        //rework.psfassignedInteraction_id = psfAss_id;
                                        //rework.workshop_id = user.workshop_id ?? default(long);
                                        //db.reworks.Add(rework);
                                        //db.SaveChanges();

                                        CompInteraction compInter = new CompInteraction();
                                        compInter.Benefits = "No Benefits Applied";
                                        compInter.DissatStatus_id = 44;
                                        compInter.complaint_creid = psfinter.complaintMgr_id ?? default(long);
                                        complaintMngrId = compInter.complaint_creid;
                                        compInter.campaign_id = long.Parse(Session["psfDayType"].ToString());
                                        compInter.customer_id = cusId;
                                        compInter.vehicle_id = vehiId;
                                        compInter.discount = 0;
                                        compInter.isReworkAvailable = false;
                                        compInter.issueDateTime = DateTime.Now;
                                        compInter.location_id = user.location_cityId ?? default(long);
                                        compInter.psfassignedInteraction_id = psfAss_id;
                                        compInter.callinteraction_id = callinteraction.id;
                                        compInter.workshop_id = user.workshop_id ?? default(long);
                                        compInter.bucket_id = 1;
                                        compInter.compRaisedCreId = long.Parse(Session["UserId"].ToString());
                                        compInter.compRaisedCreName = Session["UserName"].ToString();

                                        db.compInteractions.Add(compInter);
                                        db.SaveChanges();

                                        psfinter.isComplaintRaised = "Yes";

                                        for (int i = 0; i < listingFormData.remarksList.Count; i++)
                                        {
                                            if (psfinter.creRemarks == null && listingFormData.commentsList[i] != "")
                                            {
                                                psfinter.creRemarks = listingFormData.commentsList[i];
                                            }

                                            if (psfinter.customerFeedBack == null && listingFormData.remarksList[i] != "")
                                            {
                                                psfinter.customerFeedBack = listingFormData.remarksList[i];
                                            }
                                        }

                                        psfinter.dispositionFrom = "PSFCRE";
                                        psfinter.callInteraction_id = callinteraction.id;
                                        psfinter.callDispositionData_id = currentDispo;
                                        db.indusPSFInteraction.Add(psfinter);

                                        db.SaveChanges();
                                    }
                                    else if (psfinter.whatCustSaid == "Feedback Given" || psfinter.whatCustSaid == "ConfirmStatus" || psfinter.whatCustSaid == "FollowUp Later" || psfinter.whatCustSaid == "No Feedback" || psfinter.vehicleAfterService == "Good")
                                    {
                                        if (psfinter.whatCustSaid == "FollowUp Later")
                                        {
                                            currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == "Call Me Later").dispositionId;
                                            psfAss_id = recordPsfDisposition(1, db, currentDispo, interactionId);
                                        }
                                        else if (psfinter.whatCustSaid == "No Feedback")
                                        {
                                            currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == "Incomplete Survey").dispositionId;
                                            psfAss_id = recordPsfDisposition(1, db, currentDispo, interactionId);
                                        }
                                        else if (psfinter.whatCustSaid == "ConfirmStatus")
                                        {
                                            currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == psfinter.modeOfService).dispositionId;
                                            psfAss_id = recordPsfDisposition(1, db, currentDispo, interactionId);
                                        }
                                        else
                                        {
                                            currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == "PSF_Yes").dispositionId;
                                            psfAss_id = recordPsfDisposition(1, db, currentDispo, interactionId);
                                        }


                                        //*****************Call Interaction *******************************
                                        //if (callLog.PsfCreBucketId == 63)
                                        //{
                                        //    callinteraction.isResolved = true;
                                        //}

                                        callinteraction.callCount = 1;
                                        callinteraction.callDate = DateTime.Now.ToString("dd-MM-yyyy");
                                        callinteraction.callMadeDateAndTime = DateTime.Now;
                                        callinteraction.callTime = DateTime.Now.ToString("HH:mm:ss");
                                        callinteraction.dealerCode = Session["DealerCode"].ToString();
                                        callinteraction.dissatPSFintId = 0;
                                        callinteraction.chasserCall = false;
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
                                        callinteraction.service_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).service_id ?? default(long);
                                        db.callinteractions.Add(callinteraction);
                                        db.SaveChanges();


                                        if (psfinter.modeOfService != null && psfinter.modeOfService == "Dissatisfied with PSF")
                                        {
                                            long maxPrvRewordId = 0, complaintMgr_id = 0;

                                            if (db.compInteractions.Any(m => m.vehicle_id == vehiId && m.customer_id == cusId))
                                            {
                                                maxPrvRewordId = db.compInteractions.Where(m => m.vehicle_id == vehiId && m.customer_id == cusId).Max(m => m.Id);
                                                complaintMgr_id = db.compInteractions.FirstOrDefault(m => m.Id == maxPrvRewordId).complaint_creid;
                                            }

                                            psfinter.complaintMgr_id = complaintMgr_id;
                                            rework rework = new rework();
                                            CompInteraction compInter = new CompInteraction();
                                            wyzuser user = db.wyzusers.FirstOrDefault(m => m.id == userId);
                                            //rework.Benefits = "No Benefits Applied";
                                            //rework.DissatStatus_id = 44;
                                            //rework.complaint_creid = complaintMgr_id;
                                            //rework.campaign_id = long.Parse(Session["psfDayType"].ToString());
                                            //rework.customer_id = cusId;
                                            //rework.vehicle_id = vehiId;
                                            //rework.discount = 0;
                                            //rework.isReworkAvailable = false;
                                            //rework.issuedate = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));
                                            //rework.location_id = user.location_cityId ?? default(long);
                                            //rework.psfassignedInteraction_id = psfAss_id;
                                            //rework.workshop_id = user.workshop_id ?? default(long);
                                            //db.reworks.Add(rework);
                                            //db.SaveChanges();


                                            compInter.Benefits = "No Benefits Applied";
                                            compInter.DissatStatus_id = 44;
                                            compInter.complaint_creid = complaintMgr_id;
                                            complaintMngrId = complaintMgr_id;
                                            compInter.campaign_id = long.Parse(Session["psfDayType"].ToString());
                                            compInter.customer_id = cusId;
                                            compInter.vehicle_id = vehiId;
                                            compInter.discount = 0;
                                            compInter.isReworkAvailable = false;
                                            compInter.issueDateTime = DateTime.Now;
                                            compInter.location_id = user.location_cityId ?? default(long);
                                            compInter.psfassignedInteraction_id = psfAss_id;
                                            compInter.callinteraction_id = callinteraction.id;
                                            compInter.workshop_id = user.workshop_id ?? default(long);
                                            compInter.compRaisedCreId = long.Parse(Session["UserId"].ToString());
                                            compInter.compRaisedCreName = Session["UserName"].ToString();
                                            db.compInteractions.Add(compInter);
                                            db.SaveChanges();
                                            psfinter.isComplaintRaised = "Yes";
                                        }


                                        for (int i = 0; i < listingFormData.remarksList.Count; i++)
                                        {
                                            if (psfinter.creRemarks == null && listingFormData.commentsList[i] != "")
                                            {
                                                psfinter.creRemarks = listingFormData.commentsList[i];
                                            }

                                            if (psfinter.customerFeedBack == null && listingFormData.remarksList[i] != "")
                                            {
                                                psfinter.customerFeedBack = listingFormData.remarksList[i];
                                            }
                                        }

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

                                        psfinter.dispositionFrom = "PSFCRE";
                                        psfinter.callInteraction_id = callinteraction.id;
                                        psfinter.callinteraction = callinteraction;
                                        psfinter.callDispositionData_id = currentDispo;
                                        db.indusPSFInteraction.Add(psfinter);
                                        db.SaveChanges();
                                    }
                                }
                            }
                        }
                        else
                        {
                            TempData["SubmissionResult"] = "No Assignment found";
                            return RedirectToAction("ReturnToBucket", new { @id = 6, psfDay = 6 });
                        }

                        bool PSFtriggerStatus = db.dealers.FirstOrDefault().PSFTriggerStatus;

                        if (PSFtriggerStatus == true)
                        {
                            db.Database.ExecuteSqlCommand("call TriggerPSFCallhistoryCubeDataInsertion(@innewid,@invehicleid);", new MySqlParameter[] { new MySqlParameter("@innewid", callinteraction.id), new MySqlParameter("@invehicleid", vehiId) });
                        }
                        dbTras.Commit();

                        #region calling auto sms and email for dissat
                        if (Session["DealerCode"].ToString() == "KATARIA")
                        {
                            if (db.dealers.Count(m => m.issmsenabled) > 0)
                            {
                                new CallLoggingController().autosmsKataria(userId, vehiId, cusId, currentDispo, string.Empty, string.Empty, Convert.ToInt32(Session["UserRole1"]), 0, 0, 0, "", 0, string.Empty ,Session["DealerCode"].ToString());
                            }
                            if (db.dealers.Count(m => m.isemailenabled) > 0)
                            {
                                new CallLoggingController().autoKatariaEmailDay(cusId, userId, vehiId, currentDispo, string.Empty, string.Empty, Session["DealerCode"].ToString(), string.Empty, Convert.ToInt32(Session["UserRole1"]));
                                new CallLoggingController().autoKatariaEmailDay(cusId, userId, vehiId, 224, string.Empty, string.Empty, Session["DealerCode"].ToString(), string.Empty, Convert.ToInt32(Session["UserRole1"]));

                            }

                        }
                        else
                        { 
                        //PSF Dissat(sms &email)
                        if (currentDispo == 44 && psfinter.whatCustSaid != "ConfirmStatus")
                        {
                            List<string> phNumList = new List<string>();
                            List<string> emailList = new List<string>();
                            string concatEmail = string.Empty, concatPhonne = string.Empty;
                            //string managerName = db.wyzusers.FirstOrDefault(m => m.id == userId).creManager;
                            //var Managerdetails = db.wyzusers.Where(m => m.userName == managerName).Select(m => new { m.emailId, m.phoneNumber, m.Wm }).FirstOrDefault();
                            if (callLog.isCEIPSF == true)
                            {
                                complaintMngrId = Convert.ToInt64(Session["UserId"]);
                            }

                            var Managerdetails = db.wyzusers.Where(m => m.id == complaintMngrId).Select(m => new { m.emailId, m.phoneNumber, m.Wm }).FirstOrDefault();

                            if (Managerdetails.phoneNumber != null)
                            {
                                phNumList.Add(Managerdetails.phoneNumber);
                            }
                            if (Managerdetails.emailId != null)
                            {
                                emailList.Add(Managerdetails.emailId);
                            }

                            if (Managerdetails.Wm != null)
                            {
                                var WmDetails = db.wyzusers.Where(m => m.id == Managerdetails.Wm).Select(m => new { m.emailId, m.phoneNumber }).FirstOrDefault();
                                if (WmDetails != null && WmDetails.phoneNumber != null)
                                {
                                    phNumList.Add(WmDetails.phoneNumber);
                                }
                                if (WmDetails != null && WmDetails.emailId != null)
                                {
                                    emailList.Add(WmDetails.emailId);
                                }

                            }

                            logger.Info("PSF autosms started for user:" + Session["UserName"].ToString() + "\n Vehicle_Id: " + vehiId.ToString() + " Customer_Id: " + cusId);

                            //call sms sending function for ccm and wm
                            if (phNumList != null && phNumList.Count() > 0)
                            {
                                new CallLoggingController().autosmsday(userId, vehiId, cusId, "DISSATISFIED", "PSF", 0, 0, 0, "", 0, string.Join(",", phNumList), Session["DealerCode"].ToString());
                            }

                            //calling sms sending function for service advisor
                            long? serviceId = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).service_id;
                            if (serviceId != null)
                            {

                                long? advisorId = db.services.FirstOrDefault(m => m.id == serviceId).serviceAdvisor_advisorId;
                                if (advisorId != null)
                                {
                                    new CallLoggingController().autosmsday(userId, vehiId, cusId, "DISSATISFIED", "PSF", 0, advisorId ?? default(long), 0, "", 0, "", Session["DealerCode"].ToString());

                                }
                            }

                            //email for Wm
                            if (emailList != null && emailList.Count() > 0)
                            {
                                var emailCredentials = db.emailcredentials.Where(m => m.userEmail == "noreply@autosherpas.com" && m.inActive == false).Select(m => new { m.userEmail, m.userPassword }).FirstOrDefault();
                                if ((db.emailtemplates.Count(m => m.inActive == false && m.emailType == "DISSATPREVIOUS") > 0) && emailCredentials != null)
                                {
                                    new CallLoggingController().autoEmailDay(cusId, userId, vehiId, "DISSATPREVIOUS", emailCredentials.userEmail, emailCredentials.userPassword, string.Join(",", emailList), Session["DealerCode"].ToString(), null, null, null);
                                }
                            }
                        }
                    }
                        #endregion
                        submissionResult = "True";
                    }
                    catch (Exception ex)
                    {
                        dbTras.Rollback();
                        string stackTrace = "";
                        if (ex.InnerException != null)
                        {
                            if (ex.InnerException.InnerException != null)
                            {
                                submissionResult = ex.InnerException.InnerException.Message;
                                stackTrace = ex.InnerException.InnerException.StackTrace;
                            }
                            else
                            {
                                submissionResult = ex.InnerException.Message;
                                stackTrace = ex.InnerException.StackTrace;
                            }
                        }
                        else
                        {
                            submissionResult = ex.Message;
                            //stackTrace = ex.InnerException.StackTrace;
                        }

                        if (ex.StackTrace.Contains(":"))
                        {
                            submissionResult = submissionResult + " | " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)];
                        }

                        logger.Info("\n Exception:PSF Days:- " + submissionResult);
                    }
                }
            }
            //}
            TempData["SubmissionResult"] = submissionResult;
            if (Session["psfDayType"].ToString() == "5")
            {
                return RedirectToAction("ReturnToBucket", "CallLogging", new { @id = 6, psfDay = 15 });
            }
            else
            {
                return RedirectToAction("ReturnToBucket", "CallLogging", new { @id = 6, psfDay = 6 });
            }
        }

        [HttpPost]
        public ActionResult psfComplaintPost(CallLoggingViewModel callLog)
        {
            string submissionResult = "False";
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            long userId = Convert.ToInt32(Session["UserId"].ToString());
            long interactionId = Convert.ToInt32(Session["interactionid"].ToString());
            Logger logger = LogManager.GetLogger("apkRegLogger");

            IndusPSFInteraction psfinter = callLog.indusPsfInteraction;
            callinteraction callinteraction = new callinteraction();
            ListingForm listingFormData = callLog.listingForm;
            rework rework = callLog.rework;
            long psfAssgn_id = 0;
            int curDispoId = 0;
            using (var db = new AutoSherDBContext())
            {
                using (DbContextTransaction dbTrans = db.Database.BeginTransaction())
                {
                    try
                    {
                        CompInteraction newCompInter = callLog.compInteractions;
                        RMInteraction newRmInter = callLog.rmInteraction;
                        if (Session["isPSFRM"] != null && Convert.ToBoolean(Session["isPSFRM"]) == true)
                        {
                            logger.Info("\n PsfComplaintPpost Incoming disposition: RM - CustId:" + cusId + " VehiId:" + vehiId + " User:" + Session["UserName"].ToString());
                        }
                        else
                        {
                            logger.Info("\n PsfComplaintPpost Incoming disposition: PsfMrg - CustId:" + cusId + " VehiId:" + vehiId + " User:" + Session["UserName"].ToString());
                        }

                        if (callLog.CustomerId != cusId && callLog.VehicleId != vehiId)
                        {
                            TempData["Exceptions"] = "Invalid Disposition Found...";
                            return RedirectToAction("LogOff", "Home");
                        }

                        if (callLog.indusPsfInteraction.isContacted == "No")
                        {
                            curDispoId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == psfinter.PSFDisposition).dispositionId;
                            //psfAssgn_id = recordPsfDisposition(1, db, curDispoId, interactionId);

                            if (Session["isPSFRM"] != null && (bool)Session["isPSFRM"] == true)
                            {
                                psfinter.dispositionFrom = "RM";
                                psfAssgn_id = callLog.rmInteraction.psfassignedinteraction_id;
                            }
                            else
                            {
                                psfinter.dispositionFrom = "PSFCompMgr";
                                psfAssgn_id = callLog.compInteractions.psfassignedInteraction_id;
                            }


                            callinteraction.callCount = 1;
                            callinteraction.isComplaint = true;
                            callinteraction.callDate = DateTime.Now.ToString("dd-MM-yyyy");
                            callinteraction.callMadeDateAndTime = DateTime.Now;
                            callinteraction.callTime = DateTime.Now.ToString("HH:mm:ss");
                            callinteraction.dealerCode = Session["DealerCode"].ToString();
                            callinteraction.dissatPSFintId = 0;
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
                            callinteraction.psfAssignedInteraction_id = psfAssgn_id;
                            callinteraction.customer_id = cusId;
                            callinteraction.vehicle_vehicle_id = vehiId;
                            callinteraction.wyzUser_id = userId;
                            callinteraction.chasserCall = false;
                            callinteraction.agentName = Session["UserName"].ToString();
                            callinteraction.campaign_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAssgn_id).campaign_id;
                            callinteraction.service_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAssgn_id).service_id ?? default(long);
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
                            psfinter.callDispositionData_id = curDispoId;

                            for (int i = 0; i < listingFormData.remarksList.Count; i++)
                            {
                                if (psfinter.creRemarks == null && listingFormData.commentsList[i] != "")
                                {
                                    psfinter.creRemarks = listingFormData.commentsList[i];
                                }

                                if (psfinter.customerFeedBack == null && listingFormData.remarksList[i] != "")
                                {
                                    psfinter.customerFeedBack = listingFormData.remarksList[i];
                                }
                            }

                            db.indusPSFInteraction.Add(psfinter);
                            db.SaveChanges();

                            if (Session["isPSFRM"] != null && (bool)Session["isPSFRM"] == true)
                            {
                                newRmInter.callinteraction_id = callinteraction.id;
                                newRmInter.calldisposition_id = curDispoId;
                                newRmInter.rmAttempts = newRmInter.rmAttempts + 1;
                                db.rmInteractions.Add(newRmInter);
                                db.SaveChanges();
                            }
                            else
                            {
                                newCompInter.callinteraction_id = callinteraction.id;
                                newCompInter.calldisposition_id = curDispoId;
                                newCompInter.isDailed = true;
                                db.compInteractions.Add(newCompInter);
                                db.SaveChanges();
                            }

                        }
                        else if (callLog.indusPsfInteraction.isContacted == "Yes")
                        {


                            if (psfinter.whatCustSaid == "FollowUp Later")
                            {
                                curDispoId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Call Me Later").dispositionId;

                                //psfAssgn_id = recordPsfDisposition(1, db, curDispoId, interactionId);
                                //if (rework.RMResolutionStatus != null && rework.RMResolutionStatus != "")
                                //{
                                //    rework = db.reworks.FirstOrDefault(m => m.id == reworkMaxId);
                                //}

                                if (Session["isPSFRM"] != null && Convert.ToBoolean(Session["isPSFRM"]) == true) // If RM
                                {
                                    newRmInter.calldisposition_id = curDispoId;
                                    newRmInter.FollowUpDate = psfinter.FollowUpdate;
                                    newRmInter.FollowUpTime = psfinter.FollowUptime;
                                }
                                else // IF ComplaintMgr
                                {
                                    newCompInter.calldisposition_id = curDispoId;
                                }
                            }
                            else if (newCompInter != null && newCompInter.resolutionMode != null && newCompInter.resolutionMode != "")
                            {
                                if (newCompInter.resolutionMode == "Complaint not valid/Customer educated")
                                {
                                    curDispoId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Resolved").dispositionId;
                                    //psfAssgn_id = recordPsfDisposition(1, db, curDispoId, interactionId);
                                    newCompInter.reworkAddress = "";
                                    //rework = db.reworks.FirstOrDefault(m => m.id == reworkMaxId);
                                    //rework.id = reworkMaxId;
                                    //rework.DissatStatus_id = curDispoId;
                                    //rework.reworkAddress = "";
                                    //rework.reworkStatus_id = curDispoId;
                                    //rework.resolutionDateAndTime = DateTime.Now;
                                    //rework.psfassignedInteraction_id = psfAssgn_id;
                                    //db.reworks.AddOrUpdate(rework);
                                    //db.SaveChanges();
                                    newCompInter.calldisposition_id = curDispoId;

                                }
                                else if (newCompInter.resolutionMode == "Schedule a re-visit")
                                {
                                    curDispoId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Rework").dispositionId;

                                    newCompInter.calldisposition_id = curDispoId;
                                    newCompInter.resolutionMode = "Schedule a re-visit";
                                    if (newCompInter.reworkMode == "Self")
                                    {
                                        newCompInter.reworkAddress = "";
                                    }
                                    //psfAssgn_id = recordPsfDisposition(1, db, curDispoId, interactionId);
                                    //bool isNewInsert = false;
                                    //rework existingRework = db.reworks.SingleOrDefault(m => m.id == reworkMaxId);

                                    //if (existingRework.reworkStatus_id != 0)
                                    //{

                                    //    rework.Benefits = "No Benefits Applied";
                                    //    rework.complaint_creid = existingRework.complaint_creid;
                                    //    rework.campaign_id = existingRework.campaign_id;
                                    //    rework.customer_id = existingRework.customer_id;
                                    //    rework.DissatStatus_id = existingRework.DissatStatus_id;
                                    //    rework.issuedate = existingRework.issuedate;
                                    //    rework.vehicle_id = existingRework.vehicle_id;
                                    //    rework.psfassignedInteraction_id = psfAssgn_id;

                                    //    rework.reworkStatus_id = curDispoId;
                                    //    rework.resolutionDateAndTime = DateTime.Now;
                                    //    if (rework.reworkMode == "Self")
                                    //    {
                                    //        rework.reworkAddress = "";
                                    //    }
                                    //    db.reworks.Add(rework);
                                    //    db.SaveChanges();

                                    //    existingRework.reworkStatus_id = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Cancelled").id;
                                    //    db.reworks.AddOrUpdate(existingRework);
                                    //    db.SaveChanges();
                                    //}
                                    //else
                                    //{
                                    //    existingRework.reworkStatus_id = curDispoId;
                                    //    //existingRework.resolutionDateAndTime = DateTime.Now;
                                    //    existingRework.reworkMode = rework.reworkMode;
                                    //    if (existingRework.reworkMode == "Self")
                                    //    {
                                    //        existingRework.reworkAddress = "";
                                    //    }
                                    //    existingRework.psfassignedInteraction_id = psfAssgn_id;
                                    //    existingRework.workshop_id = rework.workshop_id;
                                    //    existingRework.reworkDateAndTime = rework.reworkDateAndTime;
                                    //    existingRework.resolutionMode = "Schedule a re-visit";
                                    //    db.reworks.AddOrUpdate(existingRework);
                                    //    db.SaveChanges();
                                    //}
                                }
                                else if (newCompInter.resolutionMode == "Resolved")
                                {
                                    curDispoId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Resolved").dispositionId;
                                    newCompInter.reworkAddress = "";
                                    newCompInter.calldisposition_id = curDispoId;
                                    newCompInter.resolutionMode = "Resolved";
                                    callinteraction.isResolved = true;
                                    //callinteraction.isResolved = true;
                                    //psfAssgn_id = recordPsfDisposition(1, db, curDispoId, interactionId);

                                    //rework existingRework = db.reworks.FirstOrDefault(m => m.id == reworkMaxId);

                                    //existingRework.DissatStatus_id = curDispoId;
                                    //existingRework.reworkStatus_id = curDispoId;
                                    //existingRework.resolutionDateAndTime = DateTime.Now;
                                    //existingRework.resolvedOn = rework.resolvedOn;
                                    //existingRework.attendedBy = rework.attendedBy;
                                    //existingRework.resolvedBy = rework.resolvedBy;
                                    //existingRework.natureOfComplaint = rework.natureOfComplaint;
                                    //existingRework.psfassignedInteraction_id = psfAssgn_id;
                                    //existingRework.discount = rework.discount;
                                    //existingRework.resolutionMode = "Resolved";


                                    //db.reworks.AddOrUpdate(existingRework);
                                    //db.SaveChanges();

                                }
                                else if (newCompInter.resolutionMode == "Escalate to RM")
                                {
                                    curDispoId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Dissatisfied with PSF").dispositionId;
                                    newCompInter.reworkAddress = "";
                                    newCompInter.calldisposition_id = curDispoId;
                                    newCompInter.isRMComplaintRaised = true;

                                    //psfAssgn_id = recordPsfDisposition(1, db, curDispoId, interactionId);

                                    //string voc = rework.VOC;
                                    //long rm_creid = rework.rm_creid;
                                    //rework = db.reworks.FirstOrDefault(m => m.id == reworkMaxId);
                                    //rework.VOC = voc;
                                    //rework.rm_creid = rm_creid;
                                    //rework.isRMComplaintRaised = true;
                                    //rework.resolutionMode = "Escalate to RM";

                                    //db.reworks.AddOrUpdate(rework);
                                    //db.SaveChanges();
                                }
                            }
                            else if (newRmInter != null && newRmInter.RMResolutionStatus != null && newRmInter.RMResolutionStatus != "")
                            {
                                //string rmRemarks = rework.RMRemarks;
                                if (newRmInter.RMResolutionStatus == "Resolved")
                                {
                                    curDispoId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Resolved").dispositionId;
                                    newRmInter.calldisposition_id = curDispoId;
                                    newRmInter.RMResolutionStatus = "Resolved";
                                    newRmInter.resolved_datetime = DateTime.Now;
                                    callinteraction.isResolved = true;
                                    //psfAssgn_id = recordPsfDisposition(1, db, curDispoId, interactionId);
                                    //callinteraction.isResolved = true;

                                    //rework = db.reworks.FirstOrDefault(m => m.id == reworkMaxId);
                                    //rework.DissatStatus_id = curDispoId;
                                    //rework.reworkStatus_id = curDispoId;
                                    //rework.resolutionDateAndTime = DateTime.Now;
                                    //rework.psfassignedInteraction_id = psfAssgn_id;
                                    //rework.RMRemarks = rmRemarks;
                                    //rework.RMResolutionStatus = "Resolved";

                                }
                                else if (newRmInter.RMResolutionStatus == "Pending")
                                {
                                    curDispoId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Pending").dispositionId;
                                    newRmInter.calldisposition_id = curDispoId;
                                    newRmInter.RMResolutionStatus = "Pending";


                                    //psfAssgn_id = recordPsfDisposition(1, db, curDispoId, interactionId);

                                    //rework = db.reworks.FirstOrDefault(m => m.id == reworkMaxId);
                                    //rework.reworkStatus_id = curDispoId;
                                    //rework.resolutionDateAndTime = DateTime.Now;
                                    //rework.psfassignedInteraction_id = psfAssgn_id;
                                    //rework.RMRemarks = rmRemarks;
                                    //rework.RMResolutionStatus = "Pending";
                                }
                                else if (newRmInter.RMResolutionStatus == "Closed")
                                {
                                    curDispoId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Closed").dispositionId;
                                    newRmInter.calldisposition_id = curDispoId;
                                    newRmInter.RMResolutionStatus = "Closed";

                                    //psfAssgn_id = recordPsfDisposition(1, db, curDispoId, interactionId);

                                    //rework = db.reworks.FirstOrDefault(m => m.id == reworkMaxId);
                                    //rework.DissatStatus_id = curDispoId;
                                    //rework.reworkStatus_id = curDispoId;
                                    //rework.resolutionDateAndTime = DateTime.Now;
                                    //rework.psfassignedInteraction_id = psfAssgn_id;
                                    //rework.RMRemarks = rmRemarks;
                                    //rework.RMResolutionStatus = "Closed";
                                }
                                //db.reworks.AddOrUpdate(rework);
                                //db.SaveChanges();
                            }

                            if (newCompInter != null && newCompInter.psfassignedInteraction_id != 0)
                            {
                                psfAssgn_id = newCompInter.psfassignedInteraction_id;
                            }

                            if (newRmInter != null && newRmInter.psfassignedinteraction_id != 0)
                            {
                                psfAssgn_id = newRmInter.psfassignedinteraction_id;
                            }

                            callinteraction.isComplaint = true;
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
                            callinteraction.psfAssignedInteraction_id = psfAssgn_id;
                            callinteraction.customer_id = cusId;
                            callinteraction.vehicle_vehicle_id = vehiId;
                            callinteraction.wyzUser_id = userId;
                            callinteraction.agentName = Session["UserName"].ToString();
                            callinteraction.chasserCall = false;
                            callinteraction.campaign_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAssgn_id).campaign_id;
                            callinteraction.service_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAssgn_id).service_id ?? default(long);
                            db.callinteractions.Add(callinteraction);
                            db.SaveChanges();


                            if (Session["isPSFRM"] != null && Convert.ToBoolean(Session["isPSFRM"]) == true) // If RM
                            {

                                if (newRmInter.RMResolutionStatus == "Pending")
                                {
                                    CompInteraction compInter = new CompInteraction();
                                    compInter.Benefits = "No Benefits Applied";
                                    compInter.DissatStatus_id = 44;
                                    compInter.complaint_creid = newRmInter.rmRaisedCompMgrId;
                                    compInter.campaign_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == newRmInter.psfassignedinteraction_id).campaign_id ?? default(long);
                                    compInter.customer_id = cusId;
                                    compInter.vehicle_id = vehiId;
                                    compInter.discount = 0;
                                    compInter.isReworkAvailable = false;
                                    compInter.issueDateTime = DateTime.Now;
                                    compInter.location_id = db.wyzusers.FirstOrDefault(m => m.id == userId).workshop_id ?? default(long);
                                    compInter.psfassignedInteraction_id = newRmInter.psfassignedinteraction_id;
                                    compInter.callinteraction_id = callinteraction.id;
                                    compInter.workshop_id = newRmInter.workshopId;
                                    compInter.compRaisedCreId = long.Parse(Session["UserId"].ToString());
                                    compInter.compRaisedCreName = Session["UserName"].ToString();
                                    db.compInteractions.Add(compInter);
                                    db.SaveChanges();
                                }

                                psfinter.dispositionFrom = "RM";
                                newRmInter.rmAttempts = newRmInter.rmAttempts + 1;
                                newRmInter.callinteraction_id = callinteraction.id;
                                db.rmInteractions.Add(newRmInter);
                                db.SaveChanges();
                            }
                            else // IF ComplaintMgr
                            {
                                psfinter.dispositionFrom = "PSFCompMgr";

                                newCompInter.callinteraction_id = callinteraction.id;
                                newCompInter.isDailed = true;
                                db.compInteractions.Add(newCompInter);
                                db.SaveChanges();

                                if (newCompInter.resolutionMode == "Escalate to RM")
                                {
                                    newRmInter.rmRaisedCompMgrId = long.Parse(Session["UserId"].ToString());
                                    newRmInter.rmRaisedCompMgrName = Session["UserName"].ToString();
                                    newRmInter.CompInteraction_id = newCompInter.Id;
                                    newRmInter.psfassignedinteraction_id = newCompInter.psfassignedInteraction_id;
                                    newRmInter.callinteraction_id = callinteraction.id;
                                    newRmInter.rmRaisedDateTime = DateTime.Now;
                                    newRmInter.workshopId = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAssgn_id).workshop_id ?? default(long);
                                    newRmInter.vehicle_id = vehiId;
                                    newRmInter.customer_id = cusId;
                                    db.rmInteractions.Add(newRmInter);
                                    db.SaveChanges();
                                }

                            }

                            for (int i = 0; i < listingFormData.remarksList.Count; i++)
                            {
                                if (psfinter.creRemarks == null && listingFormData.commentsList[i] != "")
                                {
                                    psfinter.creRemarks = listingFormData.commentsList[i];
                                }

                                if (psfinter.customerFeedBack == null && listingFormData.remarksList[i] != "")
                                {
                                    psfinter.customerFeedBack = listingFormData.remarksList[i];
                                }
                            }

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
                            psfinter.callinteraction = callinteraction;
                            psfinter.callDispositionData_id = curDispoId;
                            db.indusPSFInteraction.Add(psfinter);
                            db.SaveChanges();

                            long upselCount = 0;
                            if (listingFormData != null && listingFormData.LeadYes == "Capture Lead Yes")
                            {
                                if (listingFormData.upsellleads != null)
                                {
                                    foreach (var upsel in listingFormData.upsellleads)
                                    {
                                        if (upsel.taggedTo != null)
                                        {
                                            upsel.vehicle_vehicle_id = vehiId;
                                            //upsel.induspsfinteraction_id = psfinter.Id;
                                            //upsel.srDisposition_id = sr_disposition.id;
                                            db.upsellleads.Add(upsel);
                                            db.SaveChanges();
                                            upselCount++;
                                        }
                                    }
                                }
                            }


                            psfinter.upsellCount = upselCount;
                            db.SaveChanges();
                            submissionResult = "True";

                        }

                        bool PSFtriggerStatus = db.dealers.FirstOrDefault().PSFTriggerStatus;

                        if (PSFtriggerStatus == true)
                        {
                            db.Database.ExecuteSqlCommand("call TriggerPSFCallhistoryCubeDataInsertion(@innewid,@invehicleid);", new MySqlParameter[] { new MySqlParameter("@innewid", callinteraction.id), new MySqlParameter("@invehicleid", vehiId) });
                        }
                        dbTrans.Commit();

                        submissionResult = "True";

                        #region autoSMSEmail
                        if (newCompInter != null && newCompInter.resolutionMode == "Escalate to RM")
                        {
                            List<string> emailList = new List<string>();
                            string concatEmail = string.Empty, concatPhonne = string.Empty;
                            string managerName = db.wyzusers.FirstOrDefault(m => m.id == userId).creManager;
                            var Managerdetails = db.wyzusers.Where(m => m.userName == managerName).Select(m => new { m.emailId, m.Wm }).FirstOrDefault();
                            var Rmdetails = db.wyzusers.Where(m => m.id == newRmInter.regionalmgr_id).Select(m => new { m.emailId }).FirstOrDefault();

                            if (Managerdetails.emailId != null)
                            {
                                emailList.Add(Managerdetails.emailId);
                            }


                            if (Rmdetails.emailId != null)
                            {
                                emailList.Add(Rmdetails.emailId);
                            }

                            if (Managerdetails.Wm != null)
                            {
                                var WmDetails = db.wyzusers.Where(m => m.id == Managerdetails.Wm).Select(m => new { m.emailId }).FirstOrDefault();

                                if (WmDetails != null && WmDetails.emailId != null)
                                {
                                    emailList.Add(WmDetails.emailId);
                                }
                            }
                            logger.Info("PSF autosms started for user:" + Session["UserName"].ToString() + "\n Vehicle_Id: " + vehiId.ToString() + " Customer_Id: " + cusId);
                            //email for Wm
                            if (emailList != null && emailList.Count() > 0)
                            {
                                var emailCredentials = db.emailcredentials.Where(m => m.userEmail == "noreply@autosherpas.com" && m.inActive == false).Select(m => new { m.userEmail, m.userPassword }).FirstOrDefault();
                                if ((db.emailtemplates.Count(m => m.inActive == false && m.emailType == "ESCALATE TO RM") > 0) && emailCredentials != null)
                                {
                                    new CallLoggingController().autoEmailDay(cusId, userId, vehiId, "ESCALATE TO RM", emailCredentials.userEmail, emailCredentials.userPassword, string.Join(",", emailList), Session["DealerCode"].ToString(),null, null, null);
                                }
                            }

                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        string stackTrace = "";
                        if (ex.InnerException != null)
                        {
                            if (ex.InnerException.InnerException != null)
                            {
                                submissionResult = ex.InnerException.InnerException.Message;
                                stackTrace = ex.InnerException.InnerException.StackTrace;
                            }
                            else
                            {
                                submissionResult = ex.InnerException.Message;
                                stackTrace = ex.InnerException.StackTrace;
                            }
                        }
                        else
                        {
                            submissionResult = ex.Message;
                            stackTrace = ex.InnerException.StackTrace;
                        }

                        if (ex.StackTrace.Contains(":"))
                        {
                            submissionResult = submissionResult + " | " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)];
                        }

                        if (Session["isPSFRM"] != null && Convert.ToBoolean(Session["isPSFRM"]) == true)
                        {
                            logger.Info("\n Exception:RM-Disposition:- " + submissionResult + "\n StackTrace:- " + stackTrace);
                        }
                        else
                        {
                            logger.Info("\n Exception:PSFCompMgr-Disposition:- " + submissionResult + "\n StackTrace:- " + stackTrace);
                        }
                    }
                }

            }

            TempData["SubmissionResult"] = submissionResult;
            if (Session["isPSFRM"] != null && Convert.ToBoolean(Session["isPSFRM"]) == true)
            {
                return RedirectToAction("ReturnToBucket", "CallLogging", new { @id = 500 });
            }
            else
            {
                return RedirectToAction("ReturnToBucket", "CallLogging", new { @id = 900 });

            }
        }

        #endregion


        #region post sales pullouts
        public ActionResult getPostsalesPullOuts(string cubeId)
        {
            long postsalesCubeId = Convert.ToInt64(cubeId);
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            try
            {
                using (var db = new AutoSherDBContext())
                {


                    var psfData = db.Postsalescallhistories.Where(m => m.id == postsalesCubeId).FirstOrDefault();

                    if (psfData.salesworkshop_id != 0)
                    {
                        psfData.salesoutlet = db.Salesworkshops.FirstOrDefault(m => m.workshop_id == psfData.salesworkshop_id).salesWorkshopname;
                    }
                    psfData.DSE = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehiId).dse;
                    int campId = Convert.ToInt32(psfData.campaign_id);
                    var psf_questions = db.Postsalesfollowupquestions.Where(m => m.campaignid == campId && m.isActive == true && m.psf_format == psfData.modelcat).ToList();
                    if (psfData != null)
                    {
                        return Json(new { success = true, data = psfData, psf_questions });
                    }
                    else
                    {
                        return Json(new { success = true, exception = "No Data Found" });
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

                return Json(new { success = false, exception = exception });
            }
        }
        #endregion

    }
}