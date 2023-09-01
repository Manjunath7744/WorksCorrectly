using AutoSherpa_project.Models;
using AutoSherpa_project.Models.ViewModels;
using MySql.Data.MySqlClient;
using NLog;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoSherpa_project.Controllers
{
    [AuthorizeFilter]
    public class CallLoggingPostSalesFeedbackController : Controller
    {
        // GET: CallLoggingPostServiceFeedback
        public ActionResult CallLoggingPostSalesFeedback(string id)
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
            Session["PostSalesFeedbackDayType"] = null;
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
            ViewBag.typeOfDispo = "PostSalesFeedback";

            long cid, vehicle_id, interactionid, dispositionHistory, typeOfPSF, lastworkshopId = 0, bucket_id = 0;
            string pageFor, typeofQuestions = "";

            pageFor = id.Split(',')[0];//For CRE
            // Just for redirecting

            cid = Convert.ToInt32(id.Split(',')[2]);//CustomerId
            vehicle_id = Convert.ToInt32(id.Split(',')[3]);//Vehicle Id
            interactionid = Convert.ToInt32(id.Split(',')[4]); //AssignedInteraction Id
            dispositionHistory = Convert.ToInt32(id.Split(',')[5]);// 0 for cre normal call flow , 900 for complaint, 63 for Welcome call
            typeOfPSF = Convert.ToInt32(id.Split(',')[6]); // 2 and 15 day PSF
            callLog.postsalespsfQuestionType = id.Split(',')[7]; // Category Arena , Nexa
            ViewBag.typeOfPSF = typeOfPSF;
            ViewBag.interactionid = interactionid;

            if (dispositionHistory == 9000)
            {
                bucket_id = Convert.ToInt32(id.Split(',')[5]);
                ViewBag.dispositionHistory = "PostSalesFeedbackComplaints";
                ViewBag.typeOfPSF = 9000;
            }

            else if (dispositionHistory == 63)
            {
                typeOfPSF = Convert.ToInt32(id.Split(',')[6]);
                ViewBag.typeOfPSF = typeOfPSF;
                Session["PostSalesFeedbackDayType"] = typeOfPSF;
                callLog.PsfCreBucketId = 63;
                ViewBag.isResolved = true;
                ViewBag.dispositionHistory = "PostSalesFeedbackDays";
            }
            else
            {
                typeOfPSF = Convert.ToInt32(id.Split(',')[6]);
                ViewBag.typeOfPSF = typeOfPSF;
                Session["PostSalesFeedbackDayType"] = typeOfPSF;
                ViewBag.isResolved = false;
                ViewBag.dispositionHistory = "PostSalesFeedbackDays";
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
            callLog.postsalesassignedinteraction = new postsalesassignedinteraction();
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
                    else if (pageFor == "CREHdd")
                    {
                        Session["PageFor"] = "CREHdd";
                    }
                    Session["CusId"] = cid;
                    Session["VehiId"] = vehicle_id;
                    Session["interactionid"] = interactionid;
                    Session["typeOfDispo"] = "PostSalesFeedback";

                    callLog.CustomerId = cid;
                    callLog.VehicleId = vehicle_id;
                    callLog.UserId = UserId;
                    callLog.postsalesinteractionId = interactionid;

                    callLog = new CallLoggingController().get360ProfileData(callLog, db, "PostSalesFeedback", Session["DealerCode"].ToString(), Session["OEM"].ToString());




                    Session["VehiReg"] = callLog.vehi.chassisNo;
                    string dealername = callLog.wyzuser.dealerName;


                    var ddlWorkshop = db.workshops.Where(m => m.isinsurance == false).Select(model => new { workshopId = model.id, workshopName = model.workshopName }).OrderBy(m => m.workshopName).ToList();
                    ViewBag.ddlWorkshop = ddlWorkshop;

                    if (typeOfPSF != 0)
                    {
                        Session["PostSalesFeedbackDay"] = typeOfPSF.ToString() + "Day";
                    }
                    List<postsalesfollowupquestions> qsHelper = new List<postsalesfollowupquestions>();
                    callLog.postsalesfollowupquestionLists = new List<postsalesfollowupquestions>();

                    callLog.postsalesfollowupquestionLists = db.Postsalesfollowupquestions.Where(m => m.campaignid == typeOfPSF && m.isActive == true && m.psf_format == callLog.postsalespsfQuestionType).ToList();
                    ViewBag.totalMandatoryQustn = db.Postsalesfollowupquestions.Count(m => m.campaignid == typeOfPSF && m.isActive == true && m.psf_format == callLog.postsalespsfQuestionType && m.qs_mandatory == true);


                    for (int i = 0; i < callLog.postsalesfollowupquestionLists.Count(); i++)
                    {
                        if (callLog.postsalesfollowupquestionLists[i].display_type == "drop-down")
                        {
                            if ((!string.IsNullOrEmpty(callLog.postsalesfollowupquestionLists[i].ddl_text)) && (!(string.IsNullOrEmpty(callLog.postsalesfollowupquestionLists[i].ddl_values))))
                            {
                                callLog.postsalesfollowupquestionLists[i].DDLOptionTextList = callLog.postsalesfollowupquestionLists[i].ddl_text.Split(',').ToList();
                                callLog.postsalesfollowupquestionLists[i].DDLOptionValueList = callLog.postsalesfollowupquestionLists[i].ddl_values.Split(',').ToList();
                                callLog.postsalesfollowupquestionLists[i].dictionaryDDLQuestionList = callLog.postsalesfollowupquestionLists[i].DDLOptionTextList.ToDictionary(x => x, x => callLog.postsalesfollowupquestionLists[i].DDLOptionValueList[callLog.postsalesfollowupquestionLists[i].DDLOptionTextList.IndexOf(x)]);
                            }
                        }
                        else if (callLog.postsalesfollowupquestionLists[i].display_type == "radio-button")
                        {
                            callLog.postsalesfollowupquestionLists[i].RadioTextList = callLog.postsalesfollowupquestionLists[i].radio_options.Split(',').ToList();
                            callLog.postsalesfollowupquestionLists[i].RadioValueList = callLog.postsalesfollowupquestionLists[i].radio_values.Split(',').ToList();
                            callLog.postsalesfollowupquestionLists[i].dictionaryRDOQuestionList = callLog.postsalesfollowupquestionLists[i].RadioTextList.ToDictionary(x => x, x => callLog.postsalesfollowupquestionLists[i].RadioValueList[callLog.postsalesfollowupquestionLists[i].RadioTextList.IndexOf(x)]);

                        }
                    }

                    var workshopList = db.workshops.Where(m => m.isinsurance == true).Select(work => new { id = work.id, name = work.workshopName }).ToList();
                    ViewBag.workShop = workshopList;
                    //callLog.postsalesfollowupquestionLists.Add(workshopList1);



                    if (dispositionHistory == 9000)
                    {
                        callLog.postsalescompinteraction = db.Postsalescompinteractions.Where(m => m.psfassignedInteraction_id == interactionid).OrderByDescending(m => m.Id).FirstOrDefault();
                    }
                    if (db.Postsalescompinteractions.Count(m => m.psfassignedInteraction_id == interactionid) > 0)
                    {
                        long lastId = db.Postsalescompinteractions.Where(m => m.psfassignedInteraction_id == interactionid).Max(m => m.Id);
                        if (lastId > 0)
                        {
                            long findispoId = db.Postsalescompinteractions.FirstOrDefault(m => m.Id == lastId).calldisposition_id;
                            if (findispoId == 63)
                            {
                                ViewBag.isResolved = true;
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

        public long recordPostssalesfeedbackDisposition(int stage, AutoSherDBContext db, int? finalDispoId = 0, long? postsales_assigninter = 0, string distdispo = "")
        {
            //try
            //{
            //using (var db = new AutoSherDBContext())
            //{
            if (stage == 1)
            {
                postsalesassignedinteraction postssalesAssInter = new postsalesassignedinteraction();
                if (db.Postsalesassignedinteractions.Any(m => m.id == postsales_assigninter))
                {
                    string lastDispo = "";
                    postssalesAssInter = db.Postsalesassignedinteractions.FirstOrDefault(m => m.id == postsales_assigninter);
                    postssalesAssInter.callMade = "Yes";
                    if (postssalesAssInter.finalDisposition_id != null)
                    {
                        lastDispo = db.calldispositiondatas.FirstOrDefault(m => m.id == postssalesAssInter.finalDisposition_id).disposition;
                        postssalesAssInter.lastDisposition = lastDispo;
                    }

                    if (finalDispoId == 63)
                    {
                        postssalesAssInter.isResolved = true;
                    }

                    postssalesAssInter.finalDisposition_id = finalDispoId;
                    db.Postsalesassignedinteractions.AddOrUpdate(postssalesAssInter);
                    db.SaveChanges();

                    return postssalesAssInter.id;
                }
            }
            //}

            //}
            //catch(Exception ex)
            //{

            //}

            return 0;
        }




        #region New Post Sales
        public long recordPsfDisposition(int stage, AutoSherDBContext db, int? finalDispoId = 0, long? psf_assigninter = 0)
        {
            if (stage == 1)
            {
                postsalesassignedinteraction psfAssInter = new postsalesassignedinteraction();
                if (db.Postsalesassignedinteractions.Any(m => m.id == psf_assigninter))
                {
                    string lastDispo = "";
                    psfAssInter = db.Postsalesassignedinteractions.FirstOrDefault(m => m.id == psf_assigninter);
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
                    db.Postsalesassignedinteractions.AddOrUpdate(psfAssInter);
                    db.SaveChanges();

                    return psfAssInter.id;
                }
            }
            return 0;
        }

        [HttpPost]
        public ActionResult addpostsalesfeedback(CallLoggingViewModel callLog)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            string dissatemailBody = "\n Ratings : \n", emailSubject = "Dissatisfied Customer : " + callLog.postsalesChasis;
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            long userId = Convert.ToInt32(Session["UserId"].ToString());
            long interactionId = Convert.ToInt32(Session["interactionid"].ToString());
            string toemail = string.Empty, ccemail = string.Empty;
            string submissionResult = string.Empty;

            List<string> DDLOptionValueList = new List<string>();
            List<string> DDLOptionTextList = new List<string>();
            List<string> RadioTextList = new List<string>();
            List<string> RadioValueList = new List<string>();
            int indexofList = 0;
            string dissatAnswer = string.Empty;


            postsalesassignedinteraction psfassignedinteraction = new postsalesassignedinteraction();
            postsalescallinteraction callinteraction = new postsalescallinteraction();
            postsalesdisposition psfinter = callLog.postsalesdispositions;

            int currentDispo = 0;
            long psfAss_id = 0;
            // List<string> mandQsValue = new List<string>();
            bool isdissatisfied = false;
            long campId = Convert.ToInt32(Session["PostSalesFeedbackDayType"]);

            using (var db = new AutoSherDBContext())
            {
                using (DbContextTransaction dbTras = db.Database.BeginTransaction())
                {
                    try
                    {
                        logger.Info("\n Post Sales Days Incoming disposition: CustId:" + cusId + " VehiId:" + vehiId + " User:" + Session["UserName"].ToString());

                        if (callLog.CustomerId != cusId && callLog.VehicleId != vehiId)
                        {
                            TempData["Exceptions"] = "Invalid Disposition Found...";
                            return RedirectToAction("LogOff", "Home");
                        }
                        if (psfinter != null)
                        {
                            //var psfQs = db.Postsalesfollowupquestions.Where(m => m.qs_mandatory == true && m.campaignid == campId && m.isActive==true).ToList();
                            var psfQs = db.Postsalesfollowupquestions.Where((m => m.campaignid == campId && m.isActive == true && m.psf_format == callLog.postsalesdispositions.psfformat && m.qs_mandatory == true)).ToList();

                            foreach (var pQs in psfQs)
                            {
                                if (psfinter.GetType().GetProperty(pQs.binding_var).GetValue(psfinter, null) != null)
                                {
                                    string answeredValue = (psfinter.GetType().GetProperty(pQs.binding_var).GetValue(psfinter, null).ToString().ToUpper());
                                    //mandQsValue.Add(psfinter.GetType().GetProperty(pQs.binding_var).GetValue(psfinter, null).ToString().ToUpper());
                                    var dissatisfiyanswers = pQs.dissatisfiedvalue.Split(',').Select(x => x.Trim()).Select(x => (x).ToUpper()).ToList();

                                    if (dissatisfiyanswers.Contains(answeredValue))
                                    {

                                        if (pQs.display_type == "drop-down")
                                        {
                                            if ((!string.IsNullOrEmpty(pQs.ddl_text)) && (!(string.IsNullOrEmpty(pQs.ddl_values))))
                                            {
                                                DDLOptionTextList = pQs.ddl_text.Split(',').ToList();
                                                DDLOptionValueList = pQs.ddl_values.Split(',').ToList();
                                                indexofList = DDLOptionValueList.IndexOf(answeredValue);
                                                dissatAnswer = DDLOptionTextList.ElementAt(indexofList);
                                            }
                                        }
                                        else if (pQs.display_type == "radio-button")
                                        {
                                            RadioTextList = pQs.radio_options.Split(',').ToList();
                                            RadioValueList = pQs.radio_values.Split(',').ToList();
                                            indexofList = RadioValueList.IndexOf(answeredValue);
                                           dissatAnswer = RadioTextList.ElementAt(indexofList);

                                        }
                                        else
                                        {
                                            dissatAnswer = answeredValue;
                                        }

                                        dissatemailBody = dissatemailBody + "\n" + pQs.question + " :\t" + dissatAnswer ;
                                        isdissatisfied = true;
                                        //break;
                                    }

                                }
                            }
                        }

                        if (db.Postsalesassignedinteractions.Count(m => m.customer_id == cusId && m.vehicle_vehicle_id == vehiId) > 0)
                        {
                            if (psfinter.isContacted == "No")
                            {
                                currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == psfinter.PSFDispositon).dispositionId;
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
                                    callinteraction.makeCallFrom = "POSTSALEFEEDBACK";

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

                                callinteraction.postsalesassignedInteraction_id = psfAss_id;
                                callinteraction.customer_id = cusId;
                                callinteraction.vehicle_vehicle_id = vehiId;
                                callinteraction.wyzUser_id = userId;
                                callinteraction.chasserCall = false;
                                callinteraction.agentName = Session["UserName"].ToString();
                                callinteraction.campaign_id = db.Postsalesassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).campaign_id ?? default(long);
                                // callinteraction.service_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).service_id ?? default(long);
                                db.Postsalescallinteractions.Add(callinteraction);
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

                                psfinter.dispositionFrom = "PostSalesCRE";
                                psfinter.callInteraction_id = callinteraction.id;
                                psfinter.callDispositionData_id = currentDispo;
                                db.Postsalesdispositions.Add(psfinter);
                                db.SaveChanges();

                                if (db.smstemplates.Count(m => m.smsType == "PostsalesNoncontact" && m.inActive == false) > 0)
                                {
                                    new CallLoggingController().autosmsday(userId, vehiId, cusId, "postsalesnoncontact", "postsales", 0, 0, 0, "", 0, null, Session["DealerCode"].ToString());
                                }
                            }
                            else if (psfinter.isContacted == "Yes")
                            {

                                if ((isdissatisfied == true) || psfinter.isresolvedorpending == "Pending")
                                {

                                    if (db.Postsalesassignedinteractions.Count(m => m.id == interactionId) > 0)
                                    {
                                        var salesworkshopIds = db.Postsalesassignedinteractions.FirstOrDefault(m => m.id == interactionId).salesworkshop_id;
                                        if (db.Postsalescomplaintcres.Count(m => m.workshop_id == salesworkshopIds) > 0)
                                        {
                                            psfinter.complaintMgr_id = db.Postsalescomplaintcres.FirstOrDefault(m => m.workshop_id == salesworkshopIds).Complaintcre_id;
                                            if (db.Salesworkshops.Count(m => m.workshop_id == salesworkshopIds) > 0)
                                            {
                                                var escalationDetails = db.Salesworkshops.FirstOrDefault(m => m.workshop_id == salesworkshopIds);
                                                toemail = escalationDetails.escalationMails;
                                                ccemail = escalationDetails.escalationCC;
                                            }
                                        }
                                        else
                                        {
                                            TempData["SubmissionResult"] = "No Complaint Manager found";
                                            if (Session["PostSalesFeedbackCampaign"].ToString() == "PostSales 2nd Day")
                                            {
                                                return RedirectToAction("returnToCallLog", new { @psfType = "PostSalesFeedbackDays", @psfDay = 2 });
                                            }
                                            else if (Session["PostSalesFeedbackCampaign"].ToString() == "PostSales 15th Day")
                                            {
                                                return RedirectToAction("returnToCallLog", new { @psfType = "PostSalesFeedbackDays", @psfDay = 15 });

                                            }
                                            else if (Session["PostSalesFeedbackCampaign"].ToString() == "PostSales 30th Day")
                                            {
                                                return RedirectToAction("returnToCallLog", new { @psfType = "PostSalesFeedbackDays", @psfDay = 30 });

                                            }
                                            else if (Session["PostSalesFeedbackCampaign"].ToString() == "PostSales 45th Day")
                                            {
                                                return RedirectToAction("returnToCallLog", new { @psfType = "PostSalesFeedbackDays", @psfDay = 45 });

                                            }
                                            else
                                            {
                                                return RedirectToAction("returnToCallLog", new { @psfType = "PostSalesFeedbackComplaints", @psfDay = 0 });

                                            }
                                        }
                                    }

                                    psfinter.complaintDate = DateTime.Now;
                                    currentDispo = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Dissatisfied with PSF").dispositionId;
                                    callinteraction.isComplaint = true;
                                    callinteraction.isResolved = false;

                                    psfAss_id = recordPsfDisposition(1, db, currentDispo, interactionId);

                                    //*****************Call Interaction *******************************
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
                                        callinteraction.makeCallFrom = "POSTSALEFEEDBACK";

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
                                    callinteraction.postsalesassignedInteraction_id = psfAss_id;
                                    callinteraction.customer_id = cusId;
                                    callinteraction.vehicle_vehicle_id = vehiId;
                                    callinteraction.wyzUser_id = userId;
                                    callinteraction.agentName = Session["UserName"].ToString();
                                    callinteraction.campaign_id = long.Parse(Session["PostSalesFeedbackDayType"].ToString());
                                    callinteraction.chasserCall = false;
                                    // callinteraction.service_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).service_id ?? default(long);
                                    db.Postsalescallinteractions.Add(callinteraction);
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
                                    wyzuser user = db.wyzusers.FirstOrDefault(m => m.id == userId);

                                    postsalescompinteraction compInter = new postsalescompinteraction();
                                    compInter.Benefits = "No Benefits Applied";
                                    compInter.DissatStatus_id = currentDispo;
                                    compInter.complaint_creid = psfinter.complaintMgr_id ?? default(long);
                                    compInter.campaign_id = long.Parse(Session["PostSalesFeedbackDayType"].ToString());
                                    compInter.customer_id = cusId;
                                    compInter.vehicle_id = vehiId;
                                    compInter.issueDateTime = DateTime.Now;
                                    compInter.location_id = user.location_cityId ?? default(long);
                                    compInter.psfassignedInteraction_id = psfAss_id;
                                    compInter.callinteraction_id = callinteraction.id;
                                    compInter.workshop_id = user.workshop_id ?? default(long);
                                    compInter.bucket_id = 1;
                                    compInter.compRaisedCreId = long.Parse(Session["UserId"].ToString());
                                    compInter.compRaisedCreName = Session["UserName"].ToString();
                                    compInter.calldisposition_id = currentDispo;
                                    db.Postsalescompinteractions.Add(compInter);
                                    db.SaveChanges();

                                    psfinter.isComplaintRaised = "Yes";



                                    psfinter.dispositionFrom = "PSFCRE";
                                    psfinter.callInteraction_id = callinteraction.id;
                                    psfinter.callDispositionData_id = currentDispo;
                                    db.Postsalesdispositions.Add(psfinter);

                                    db.SaveChanges();
                                }

                                else if (psfinter.whatcustsays == "Call Me Later" || psfinter.whatcustsays == "FEEDBACK" || psfinter.whatcustsays == "NO FEEDBACK" || psfinter.whatcustsays == "Complaint Resolution Status")
                                {
                                    if (psfinter.whatcustsays == "Call Me Later")
                                    {
                                        currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == "Call Me Later").dispositionId;
                                        psfAss_id = recordPsfDisposition(1, db, currentDispo, interactionId);
                                        if (callLog.PsfCreBucketId == 63 && currentDispo != 44)
                                        {
                                            callinteraction.isResolved = true;
                                        }
                                    }
                                    else if (psfinter.whatcustsays == "NO FEEDBACK")
                                    {
                                        currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == "Incomplete Survey").dispositionId;
                                        psfAss_id = recordPsfDisposition(1, db, currentDispo, interactionId);
                                    }
                                    else if (psfinter.isresolvedorpending == "Resolved")
                                    {
                                        currentDispo = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "ResolutionConfirmed").dispositionId;
                                        psfAss_id = recordPsfDisposition(1, db, currentDispo, interactionId);
                                        callinteraction.isResolved = true;
                                    }
                                    else
                                    {
                                        currentDispo = db.calldispositiondatas.SingleOrDefault(m => m.disposition == "PSF_Yes").dispositionId;
                                        psfAss_id = recordPsfDisposition(1, db, currentDispo, interactionId);
                                    }


                                    //*****************Call Interaction *******************************

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
                                        callinteraction.makeCallFrom = "POSTSALEFEEDBACK";

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
                                    callinteraction.postsalesassignedInteraction_id = psfAss_id;
                                    callinteraction.customer_id = cusId;
                                    callinteraction.vehicle_vehicle_id = vehiId;
                                    callinteraction.wyzUser_id = userId;
                                    callinteraction.agentName = Session["UserName"].ToString();
                                    callinteraction.campaign_id = db.Postsalesassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).campaign_id;
                                    //  callinteraction.service_id = db.psfassignedinteractions.FirstOrDefault(m => m.id == psfAss_id).service_id ?? default(long);
                                    db.Postsalescallinteractions.Add(callinteraction);
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

                                    psfinter.dispositionFrom = "POSTSALEFEEDBACK";
                                    psfinter.callInteraction_id = callinteraction.id;
                                    // psfinter.callinteraction = callinteraction;
                                    psfinter.callDispositionData_id = currentDispo;
                                    db.Postsalesdispositions.Add(psfinter);
                                    db.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            TempData["SubmissionResult"] = "No Assignment found";
                            // return RedirectToAction("ReturnToBucket", new { @id = 6, psfDay = 6 });
                            if (Session["PostSalesFeedbackCampaign"].ToString() == "PostSales 2nd Day")
                            {
                                return RedirectToAction("returnToCallLog", new { @psfType = "PostSalesFeedbackDays", @psfDay = 2 });
                            }
                            else if (Session["PostSalesFeedbackCampaign"].ToString() == "PostSales 15th Day")
                            {
                                return RedirectToAction("returnToCallLog", new { @psfType = "PostSalesFeedbackDays", @psfDay = 15 });

                            }
                            else if (Session["PostSalesFeedbackCampaign"].ToString() == "PostSales 30th Day")
                            {
                                return RedirectToAction("returnToCallLog", new { @psfType = "PostSalesFeedbackDays", @psfDay = 30 });

                            }
                            else if (Session["PostSalesFeedbackCampaign"].ToString() == "PostSales 45th Day")
                            {
                                return RedirectToAction("returnToCallLog", new { @psfType = "PostSalesFeedbackDays", @psfDay = 45 });

                            }
                            else
                            {
                                return RedirectToAction("returnToCallLog", new { @psfType = "PostSalesFeedbackComplaints", @psfDay = 0 });

                            }
                        }

                        db.Database.ExecuteSqlCommand("call TriggerPostsalesCallhistoryDataInsertion(@innewid,@invehicleid);", new MySqlParameter[] { new MySqlParameter("@innewid", callinteraction.id), new MySqlParameter("@invehicleid", vehiId) });

                        dbTras.Commit();

                        submissionResult = "True";


                        if (currentDispo == 22 && (db.smstemplates.Count(m => m.smsType == "PostsalesSatisfied" && m.inActive == false) > 0))
                        {
                            new CallLoggingController().autosmsday(userId, vehiId, cusId, "PostsalesSatisfied", "postsales", 0, 0, 0, "", 0, null, Session["DealerCode"].ToString());
                        }
                        else if (currentDispo == 44)
                        {
                            if ((db.smstemplates.Count(m => m.smsType == "PostsalesDissatisfied" && m.inActive == false) > 0))
                            {
                                new CallLoggingController().autosmsday(userId, vehiId, cusId, "PostsalesDissatisfied", "postsales", 0, 0, 0, "", 0, null, Session["DealerCode"].ToString());
                            }
                            if (db.emailtemplates.Count(m => m.inActive == false && m.emailType == "DISSATPOSTSALES") > 0)
                            {
                                //var emailCredentials = db.emailcredentials.Where(m => m.userEmail == "noreply@autosherpas.com" && m.inActive == false).Select(m => new { m.userEmail, m.userPassword }).FirstOrDefault();
                                var emailCredentials = db.emailcredentials.Where(m => m.isdefaultemail == true).Select(m => new { m.userEmail, m.userPassword }).FirstOrDefault();//newly added
                                new CallLoggingController().autoEmailDay(cusId, userId, vehiId, "DISSATPOSTSALES", emailCredentials.userEmail, emailCredentials.userPassword, toemail, Session["DealerCode"].ToString(), ccemail, emailSubject, dissatemailBody);
                            }
                        }

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
            if (Session["PostSalesFeedbackCampaign"].ToString() == "PostSales 2nd Day")
            {
                return RedirectToAction("returnToCallLog", new { @psfType = "PostSalesFeedbackDays", @psfDay = 2 });
            }
            else if (Session["PostSalesFeedbackCampaign"].ToString() == "PostSales 15th Day")
            {
                return RedirectToAction("returnToCallLog", new { @psfType = "PostSalesFeedbackDays", @psfDay = 15 });

            }
            else if (Session["PostSalesFeedbackCampaign"].ToString() == "PostSales 30th Day")
            {
                return RedirectToAction("returnToCallLog", new { @psfType = "PostSalesFeedbackDays", @psfDay = 30 });

            }
            else if (Session["PostSalesFeedbackCampaign"].ToString() == "PostSales 45th Day")
            {
                return RedirectToAction("returnToCallLog", new { @psfType = "PostSalesFeedbackDays", @psfDay = 45 });

            }
            else
            {
                return RedirectToAction("returnToCallLog", new { @psfType = "PostSalesFeedbackComplaints", @psfDay = 0 });

            }
        }
        public ActionResult returnToCallLog(string psfType, int psfDay)
        {
            if (TempData["SubmissionResult"] != null)
            {
                TempData["SubmissionResult"] = TempData["SubmissionResult"];
            }

            try
            {
                if (psfType == "PostSalesFeedbackDays")
                {
                    return RedirectToAction("postSalesLogs", "postSalesDetails", new { @id = psfDay });
                }
                else if (psfType == "PostSalesFeedbackComplaints")
                {
                    return RedirectToAction("psfComplaintDetails", "postSalesDetails", null);
                }
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("postSalesLogs", "postSalesDetails", new { @id = 2 });
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

            postsalesdisposition psfinter = callLog.postsalesdispositions;
            postsalescompinteraction newCompInter = callLog.postsalescompinteraction;
            postsalescallinteraction callinteraction = new postsalescallinteraction();
            long psfAssgn_id = 0;
            int curDispoId = 0;
            using (var db = new AutoSherDBContext())
            {
                using (DbContextTransaction dbTrans = db.Database.BeginTransaction())
                {
                    try
                    {
                        logger.Info("\n Post sales ComplaintPpost Incoming disposition: PsfMrg - CustId:" + cusId + " VehiId:" + vehiId + " User:" + Session["UserName"].ToString());
                        if (callLog.CustomerId != cusId && callLog.VehicleId != vehiId)
                        {
                            TempData["Exceptions"] = "Invalid Disposition Found...";
                            return RedirectToAction("LogOff", "Home");
                        }

                        if (callLog.postsalesdispositions.isContacted == "No")
                        {
                            curDispoId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == psfinter.PSFDispositon).dispositionId;

                            psfinter.dispositionFrom = "PSFCompMgr";
                            psfAssgn_id = callLog.postsalescompinteraction.psfassignedInteraction_id;

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
                                callinteraction.makeCallFrom = "POSTSALEFEEDBACK";
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
                            callinteraction.postsalesassignedInteraction_id = psfAssgn_id;
                            callinteraction.customer_id = cusId;
                            callinteraction.vehicle_vehicle_id = vehiId;
                            callinteraction.wyzUser_id = userId;
                            callinteraction.chasserCall = false;
                            callinteraction.agentName = Session["UserName"].ToString();
                            callinteraction.campaign_id = db.Postsalesassignedinteractions.FirstOrDefault(m => m.id == psfAssgn_id).campaign_id;
                            db.Postsalescallinteractions.Add(callinteraction);
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
                            db.Postsalesdispositions.Add(psfinter);
                            db.SaveChanges();

                            newCompInter.callinteraction_id = callinteraction.id;
                            newCompInter.calldisposition_id = curDispoId;
                            newCompInter.isDailed = true;
                            db.Postsalescompinteractions.Add(newCompInter);
                            db.SaveChanges();
                        }
                        else if (callLog.postsalesdispositions.isContacted == "Yes")
                        {
                            if (psfinter.whatcustsays == "Call Me Later")
                            {
                                curDispoId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Call Me Later").dispositionId;
                                {
                                    newCompInter.calldisposition_id = curDispoId;

                                }
                            }
                            else if (newCompInter != null && newCompInter.resolutionMode != null && newCompInter.resolutionMode != "")
                            {
                                if (newCompInter.resolutionMode == "Resolved")
                                {
                                    curDispoId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Resolved").dispositionId;
                                    newCompInter.calldisposition_id = curDispoId;
                                    newCompInter.resolutionMode = "Resolved";
                                    newCompInter.resolvedOn = DateTime.Now;
                                    newCompInter.resolvedBy = Session["UserName"].ToString();
                                    callinteraction.isResolved = true;

                                }
                                else if (newCompInter.resolutionMode == "Escalate to Dealer Head")
                                {
                                    curDispoId = db.calldispositiondatas.FirstOrDefault(m => m.disposition == "Dissatisfied with PSF").dispositionId;
                                    newCompInter.calldisposition_id = curDispoId;

                                }
                            }
                            psfAssgn_id = newCompInter.psfassignedInteraction_id;
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
                                callinteraction.makeCallFrom = "POSTSALEFEEDBACK";

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
                            callinteraction.postsalesassignedInteraction_id = psfAssgn_id;
                            callinteraction.customer_id = cusId;
                            callinteraction.vehicle_vehicle_id = vehiId;
                            callinteraction.wyzUser_id = userId;
                            callinteraction.agentName = Session["UserName"].ToString();
                            callinteraction.chasserCall = false;
                            callinteraction.campaign_id = db.Postsalesassignedinteractions.FirstOrDefault(m => m.id == psfAssgn_id).campaign_id;
                            db.Postsalescallinteractions.Add(callinteraction);
                            db.SaveChanges();


                            newCompInter.callinteraction_id = callinteraction.id;
                            db.Postsalescompinteractions.Add(newCompInter);
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
                            db.Postsalesdispositions.Add(psfinter);
                            db.SaveChanges();
                            submissionResult = "True";
                        }
                        db.Database.ExecuteSqlCommand("call TriggerPostsalesCallhistoryDataInsertion(@innewid,@invehicleid);", new MySqlParameter[] { new MySqlParameter("@innewid", callinteraction.id), new MySqlParameter("@invehicleid", vehiId) });

                        dbTrans.Commit();

                        submissionResult = "True";

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
                        logger.Info("\n Exception:PSFCompMgr-Disposition:- " + submissionResult + "\n StackTrace:- " + stackTrace);
                    }
                }

            }
            TempData["SubmissionResult"] = submissionResult;

            return RedirectToAction("ReturnToBucket", "CallLogging", new { @id = 9000 });
        }
        #endregion

        //Drop down Populate workshop  based on District


        public JsonResult GetWorkshopDropdown(int? districtValue)
        {
            try
            {
                //int districtId = Convert.ToInt32(districtValue.distId);
                using (var db = new AutoSherDBContext())
                {
                    var workshopList = db.workshops.Where(x => x.districtid == districtValue).Select(m => new { id = m.id, workshopName = m.workshopName }).ToList();

                    return Json(new { workshopList = workshopList, JsonRequestBehavior.AllowGet });

                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { workshopList = "", JsonRequestBehavior.AllowGet });

        }

    }

}