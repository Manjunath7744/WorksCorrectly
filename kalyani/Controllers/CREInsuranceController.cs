using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoSherpa_project.Models;
using AutoSherpa_project.Models.ViewModels;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace AutoSherpa_project.Controllers
{
    [AuthorizeFilter]
    public class CREInsuranceController : Controller
    {
        [ActionName("Insurance"), HttpGet]
        //[AuthorizeFilter]
        public ActionResult CallLog()
        {
            InsurenceViewModel insurenceLogVM = new InsurenceViewModel();

            Session["RoleFor"] = null;
            Session["isCallInitiated"] = null;
            Session["AndroidUniqueId"] = null;
            Session["GSMUniqueId"] = null;
            Session["PageFor"] = null;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    int UserId = Convert.ToInt32(Session["UserId"].ToString());
                    var ddlcampaignList = db.campaigns.Where(m => m.campaignType == "Forecast" || m.campaignType == "Insurance" && m.isactive == true).ToList();
                    var ddlreason = db.calldispositiondatas.Where(m => m.insurance == 1).ToList();
                    var ddlNewListDispo = db.calldispositiondatas.Where(u => u.dispositionId == 6 || u.dispositionId == 7 || u.dispositionId == 8 || u.dispositionId == 9 || u.dispositionId == 10 || u.dispositionId == 43).ToList();
                    var followupreasons = db.FollowupReasons.ToList();
                    var modelsList = db.modelslists.ToList();
                    var modelcategorieslist = db.Modelcategories.ToList();
                    
                    var insCompany = db.insurancecompanies.ToList();
                    
                    var ddltagging = db.campaigns.Where(m => m.campaignType == "TaggingINS" && m.isactive == true && m.campaignName != "ISP" && m.campaignName != "ISP/MCP/EW" && m.campaignName != "ISP/MCP" && m.campaignName != "ISP/EW").Select(model => new { id = model.id, TaggingCamp = model.campaignName }).ToList();

                    var ddllocation = db.workshops.Select(m => new { id = m.id, location = m.workshopName }).ToList();
                    ViewBag.ddllocation = ddllocation;
                  /* if (Session["DealerCode"].ToString() == "INDUS TV"|| Session["DealerCode"].ToString() == "PAWANHYUNDAI")
                   {
                        var ddllocation = db.workshops.Select(m => new { id = m.id, location = m.workshopName }).ToList();
                        ViewBag.ddllocation = ddllocation;
                    }
                    else
                    {
                        var locationids = db.insuranceassignedinteractions.Where(m => m.wyzUser_id == UserId).Select(m => m.location_id).ToList();
                        var ddllocation = db.locations.Where(m => locationids.Contains(m.cityId)).Select(m => new { id = m.cityId, location = m.name }).OrderBy(m => m.location).ToList();
                        ViewBag.ddllocation = ddllocation;
                    }*/
                    var renewalType = db.renewaltypes.Select(m => new { id = m.id, name = m.renewalTypeName }).OrderBy(m => m.name).ToList();

                    ViewBag.ddlcampaignList = ddlcampaignList;
                    ViewBag.ddlreason = ddlreason;
                    ViewBag.modelsList = modelsList;
                    ViewBag.insCompany = insCompany;
                    ViewBag.renewalType = renewalType;
                    ViewBag.ddlNewListDispo = ddlNewListDispo;
                    ViewBag.ddltagging = ddltagging;
                    ViewBag.modelcategorieslist = modelcategorieslist;
                    ViewBag.followupreasons = followupreasons;
                    if (TempData["SubmissionResult"] != null)
                    {
                        if (TempData["SubmissionResult"].ToString() != "True")
                        {
                            ViewBag.dispositionResult = TempData["SubmissionResult"].ToString();
                            ViewBag.dispoError = true;
                        }
                        else
                        {
                            ViewBag.dispoError = false;
                            ViewBag.dispositionResult = "Disposition submitted";
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

                if (ex.StackTrace.Contains(":"))
                {
                    exception += ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)];
                }

                TempData["Exceptions"] = exception;
                TempData["ControllerName"] = "Insurance";

                return RedirectToAction("LogOff", "Home");
            }
            return View(insurenceLogVM);
        }

        #region Dashboard Count

        public ActionResult loadDashCounts()
        {
            int FreeCalls, pendingFollow, overDue, totalContacts, NonContacts, totalCall, totalAttemptedPerDay, totalRedFlag, attemptsIns, pending, totalAppointments,duetodaycount;
            try
            {
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                using (var db = new AutoSherDBContext())
                {
                    FreeCalls = db.Database.SqlQuery<int>("call insuranceDashboardCounts(@wyzUserId,@boardId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId), new MySqlParameter("@boardId", 1) }).FirstOrDefault();
                    pendingFollow = db.Database.SqlQuery<int>("call insuranceDashboardCounts(@wyzUserId,@boardId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId), new MySqlParameter("@boardId", 2) }).FirstOrDefault();
                    NonContacts = db.Database.SqlQuery<int>("call insuranceDashboardCounts(@wyzUserId,@boardId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId), new MySqlParameter("@boardId", 3) }).FirstOrDefault();
                    overDue = db.Database.SqlQuery<int>("call insuranceDashboardCounts(@wyzUserId,@boardId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId), new MySqlParameter("@boardId", 4) }).FirstOrDefault();
                    totalRedFlag = db.Database.SqlQuery<int>("call insuranceDashboardCounts(@wyzUserId,@boardId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId), new MySqlParameter("@boardId", 5) }).FirstOrDefault();
                    
                    attemptsIns = db.Database.SqlQuery<int>("call insuranceDashboardCounts(@wyzUserId,@boardId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId), new MySqlParameter("@boardId", 6) }).FirstOrDefault();
                    pending = db.Database.SqlQuery<int>("call insuranceDashboardCounts(@wyzUserId,@boardId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId), new MySqlParameter("@boardId", 7) }).FirstOrDefault();
                    totalAppointments = db.Database.SqlQuery<int>("call insuranceDashboardCounts(@wyzUserId,@boardId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId), new MySqlParameter("@boardId", 8) }).FirstOrDefault();
                    totalContacts = db.Database.SqlQuery<int>("call insuranceDashboardCounts(@wyzUserId,@boardId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId), new MySqlParameter("@boardId", 9) }).FirstOrDefault();
                    totalCall = db.Database.SqlQuery<int>("call insuranceDashboardCounts(@wyzUserId,@boardId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId), new MySqlParameter("@boardId", 10) }).FirstOrDefault();
                    totalAttemptedPerDay = db.Database.SqlQuery<int>("call insuranceDashboardCounts(@wyzUserId,@boardId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId), new MySqlParameter("@boardId", 11) }).FirstOrDefault();
                    duetodaycount = db.Database.SqlQuery<int>("call insuranceDashboardCounts(@wyzUserId,@boardId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId), new MySqlParameter("@boardId", 12) }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                string exception;
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

                return Json(new { success = false, error = exception });
            }

            return Json(new { success = true, FreeCalls, pendingFollow, NonContacts, overDue, totalRedFlag, attemptsIns, pending, totalAppointments, totalContacts, totalCall, totalAttemptedPerDay,duetodaycount });
        }
        #endregion

        #region Getting Bucket Data
        public ActionResult GetBucketData(string insurenceData)
        {
            string campaignName, status, attempts, ExpiryfromDate, renewalType, ExpirytoDate, flag, locId, insuranceCompany, modelName,modelCategory, appointmentFromDate, appointmentToDate, callFromDate, callToDate, appointmentType, reasons, fromexpirydaterange, toexpirydaterange, lastServiceFromDate, lastServiceToDate, lastDispo, tag, orderFilter,fromsaledate,tosaledate,followupfromdate,followuptodate,followupreason;
            string exception = "";
            string dueDateEdited = "";
            List<insuranceBuketData> insuracecallLogs = null;

            int fromIndex = Convert.ToInt32(Request["start"]);
            int toIndex = Convert.ToInt32(Request["length"]);
            string searchPattern = Request["search[value]"];

            //Find Order Column
            var insortfilter = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortOrder = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            orderFilter = insortfilter + "_" + sortOrder;

            int UserId = Convert.ToInt32(Session["UserId"].ToString());

            InsurenceFilter filter = new InsurenceFilter();
            if (insurenceData != null)
            {
                filter = JsonConvert.DeserializeObject<InsurenceFilter>(insurenceData);
            }

            int totalCount = 0;
            long patternCount = 0;

            if (searchPattern != "")
            {
                filter.isFiltered = true;
            }
            campaignName = filter.Campaign == null ? "" : filter.Campaign;
            ExpiryfromDate = filter.ExFromDate == null ? "" : Convert.ToDateTime(filter.ExFromDate.ToString()).ToString("yyyy-MM-dd");
            ExpirytoDate = filter.ExToDate == null ? "" : Convert.ToDateTime(filter.ExToDate.ToString()).ToString("yyyy-MM-dd");
            flag = filter.Flag == null ? "" : filter.Flag;
            locId = filter.LocationId == null ? "" : filter.LocationId;
            insuranceCompany = filter.InsuCmp == null ? "" : filter.InsuCmp;
            modelName = filter.model == null ? "" : filter.model;
            modelCategory = filter.modelcategory == null ? "" : filter.modelcategory;
            appointmentFromDate = filter.fromDate == null ? "" : Convert.ToDateTime(filter.fromDate.ToString()).ToString("yyyy-MM-dd");
            appointmentToDate = filter.toDate == null ? "" : Convert.ToDateTime(filter.toDate.ToString()).ToString("yyyy-MM-dd");
            callFromDate = filter.fromDate == null ? "" : Convert.ToDateTime(filter.fromDate.ToString()).ToString("yyyy-MM-dd");
            callToDate = filter.toDate == null ? "" : Convert.ToDateTime(filter.toDate.ToString()).ToString("yyyy-MM-dd");
            appointmentType = filter.AppointmentType == null ? "" : filter.AppointmentType;
            reasons = filter.reasons == null ? "" : filter.reasons;
            fromexpirydaterange = filter.ExFromDate == null ? "" : Convert.ToDateTime(filter.ExFromDate.ToString()).ToString("yyyy-MM-dd");
            toexpirydaterange = filter.ExToDate == null ? "" : Convert.ToDateTime(filter.ExToDate.ToString()).ToString("yyyy-MM-dd");
            attempts = filter.attempts == null ? "" : filter.attempts;
            status = filter.BookingStatus == null ? "" : filter.BookingStatus;
            renewalType = filter.renewalTypelist == null ? "" : filter.renewalTypelist;
            dueDateEdited = filter.DueDateEdited == null ? "" : filter.DueDateEdited;
            lastServiceFromDate = filter.LastserviceFromDate == null ? "" : Convert.ToDateTime(filter.LastserviceFromDate).ToString("yyyy-MM-dd");
            lastServiceToDate = filter.LastserviceToDate == null ? "" : Convert.ToDateTime(filter.LastserviceToDate).ToString("yyyy-MM-dd");
            fromsaledate = filter.fromsaleDate == null ? "" : Convert.ToDateTime(filter.fromsaleDate).ToString("yyyy-MM-dd");
            tosaledate = filter.tosaleDate == null ? "" : Convert.ToDateTime(filter.tosaleDate).ToString("yyyy-MM-dd");
            lastDispo = filter.LastDispostion == null || filter.LastDispostion == "" ? "0" : filter.LastDispostion;
            followupfromdate = filter.Followupfromdate == null ? "" : Convert.ToDateTime(filter.Followupfromdate).ToString("yyyy-MM-dd");
            followuptodate = filter.Followupfromdate == null ? "" : Convert.ToDateTime(filter.Followupfromdate).ToString("yyyy-MM-dd");
            followupreason = filter.FollowupReason == null ? "" : filter.FollowupReason;
            tag = filter.Tagging == null || filter.Tagging == "" ? "0" : filter.Tagging;
            long tagIId = Convert.ToInt64(tag);

            using (var db = new AutoSherDBContext())
            {
                try
                {
                    if (filter.getDataFor == 1) //Scheduled Calls
                    {
                        totalCount = getScheduledBucketCount(UserId, "", "", "", "", "", "", "", "", dueDateEdited);
                        if (campaignName == "" && ExpiryfromDate == "" && ExpirytoDate == "" && flag == "" && locId == "" && renewalType == "" && searchPattern == "" && tag == "0")
                        {
                            filter.isFiltered = false;
                        }
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }
                        insuracecallLogs = getScheduledCall(UserId, campaignName, ExpiryfromDate, ExpirytoDate, flag, modelName, searchPattern, locId, renewalType, fromIndex, toIndex, lastServiceFromDate, lastServiceToDate , tagIId, orderFilter, dueDateEdited);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getScheduledCall(UserId, campaignName, ExpiryfromDate, ExpirytoDate, flag, modelName, searchPattern, locId, renewalType, 0, totalCount, lastServiceFromDate, lastServiceToDate , tagIId, orderFilter, dueDateEdited).Count;
                        }
                    }
                    else if (filter.getDataFor == 2) //Pending FollowUp
                    {
                        totalCount = getBucket234Count("", "", "", "", "", "", "", "", "", "", "", "", UserId, 4, "", dueDateEdited);
                        if (campaignName == "" && fromexpirydaterange == "" && toexpirydaterange == "" && appointmentFromDate == "" && appointmentToDate == "" && callFromDate == "" && callToDate == "" && appointmentType == "" && modelName == "" && reasons == "" && flag == "" && locId == "" && searchPattern == "" && tag == "0")
                        {
                            filter.isFiltered = false;
                        }
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }

                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }
                        insuracecallLogs = getBucket234(fromexpirydaterange, toexpirydaterange, campaignName, appointmentFromDate, appointmentToDate, callFromDate, callToDate, appointmentType, modelName, flag, reasons, searchPattern, UserId, 4, locId, fromIndex, toIndex, lastServiceFromDate, lastServiceToDate, tagIId, orderFilter, dueDateEdited);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getBucket234(fromexpirydaterange, toexpirydaterange, campaignName, appointmentFromDate, appointmentToDate, callFromDate, callToDate, appointmentType, modelName, flag, reasons, searchPattern, UserId, 4, locId, 0, totalCount, lastServiceFromDate, lastServiceToDate, tagIId,  dueDateEdited).Count;
                        }

                    }
                    else if (filter.getDataFor == 3)//Appointments
                    {
                        totalCount = getBucket234Count("", "", "", "", "", "", "", "", "", "", "", "", UserId, 25, "", dueDateEdited);
                        if (campaignName == "" && fromexpirydaterange == "" && toexpirydaterange == "" && appointmentFromDate == "" && appointmentToDate == "" && callFromDate == "" && callToDate == "" && appointmentType == "" && modelName == "" && reasons == "" && flag == "" && locId == "" && searchPattern == "" && tag == "0")
                        {
                            filter.isFiltered = false;
                        }
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }

                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }

                        insuracecallLogs = getBucket234(fromexpirydaterange, toexpirydaterange, campaignName, appointmentFromDate, appointmentToDate, callFromDate, callToDate, appointmentType, modelName, flag, reasons, searchPattern, UserId, 25, locId, fromIndex, toIndex, lastServiceFromDate, lastServiceToDate, tagIId, orderFilter, dueDateEdited);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getBucket234(fromexpirydaterange, toexpirydaterange, campaignName, appointmentFromDate, appointmentToDate, callFromDate, callToDate, appointmentType, modelName, flag, reasons, searchPattern, UserId, 25, locId, 0, totalCount, lastServiceFromDate, lastServiceToDate, tagIId, dueDateEdited).Count;
                        }
                    }
                    else if (filter.getDataFor == 4) //Renewal not required
                    {

                        totalCount = getBucket234Count("", "", "", "", "", "", "", "", "", "", "", "", UserId,  26, "", dueDateEdited);
                        if (campaignName == "" && fromexpirydaterange == "" && toexpirydaterange == "" && appointmentFromDate == "" && appointmentToDate == "" && callFromDate == "" && callToDate == "" && appointmentType == "" && modelName == "" && reasons == "" && flag == "" && locId == "" && searchPattern == "" && tag == "0")
                        {
                            filter.isFiltered = false;
                        }
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }
                        insuracecallLogs = getBucket234(fromexpirydaterange, toexpirydaterange, campaignName, appointmentFromDate, appointmentToDate, callFromDate, callToDate, appointmentType, modelName, flag, reasons, searchPattern, UserId, 26, locId, fromIndex, toIndex, lastServiceFromDate, lastServiceToDate, tagIId, orderFilter, dueDateEdited);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getBucket234(fromexpirydaterange, toexpirydaterange, campaignName, appointmentFromDate, appointmentToDate, callFromDate, callToDate, appointmentType, modelName, flag, reasons, searchPattern, UserId, 26, locId, 0, totalCount,lastServiceFromDate, lastServiceToDate, tagIId, dueDateEdited).Count;
                        }
                    }
                    else if (filter.getDataFor == 5) //NonContacts
                    {
                        totalCount = getnoncontactBucketCount("", "", "", "", "", "", "", "", UserId, "", dueDateEdited,lastDispo);
                        if (campaignName == "" && fromexpirydaterange == "" && toexpirydaterange == "" && callFromDate == "" && callToDate == "" && attempts == "" && flag == "" && locId == "" && searchPattern == "" && lastDispo == "0" && tag == "0")
                        {
                            filter.isFiltered = false;
                        }
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }
                        insuracecallLogs = getNonContacts(searchPattern, fromexpirydaterange, toexpirydaterange, campaignName, callFromDate, callToDate, flag, attempts, UserId, 1, locId, fromIndex, toIndex, lastServiceFromDate, lastServiceToDate, lastDispo, tagIId, orderFilter, dueDateEdited);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getNonContacts(searchPattern, fromexpirydaterange, toexpirydaterange, campaignName, callFromDate, callToDate, flag, attempts, UserId, 1, locId, 0, totalCount, lastServiceFromDate, lastServiceToDate , lastDispo, tagIId, dueDateEdited).Count;
                        }

                    }
                    else if (filter.getDataFor == 6) //N+1 Day
                    {
                        totalCount = getNthand1Count(1, UserId, "", "", "", "", "", "", "", "", dueDateEdited);
                        if (campaignName == "" && ExpiryfromDate == "" && ExpirytoDate == "" && appointmentType == "" && insuranceCompany == "" && status == "" && flag == "" && locId == "" && searchPattern == "" && tag == "0")
                        {
                            filter.isFiltered = false;
                        }
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }

                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }
                        insuracecallLogs = getNthand1(searchPattern, ExpiryfromDate, ExpirytoDate, flag, appointmentType, insuranceCompany, UserId, 1, locId, status, fromIndex, toIndex, lastServiceFromDate, lastServiceToDate, tagIId, orderFilter, dueDateEdited);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getNthand1(searchPattern, ExpiryfromDate, ExpirytoDate, flag, appointmentType, insuranceCompany, UserId, 1, locId, status, 0, totalCount, lastServiceFromDate, lastServiceToDate, tagIId, dueDateEdited).Count;
                        }
                    }
                    else if (filter.getDataFor == 7)//Nth Day
                    {
                        totalCount = getNthand1Count(2, UserId, "", "", "", "", "", "", "", "", dueDateEdited);
                        if (campaignName == "" && ExpiryfromDate == "" && ExpirytoDate == "" && appointmentType == "" && insuranceCompany == "" && status == "" && flag == "" && locId == "" && searchPattern == "" && tag == "0")
                        {
                            filter.isFiltered = false;
                        }
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }

                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }
                        insuracecallLogs = getNthand1(searchPattern, ExpiryfromDate, ExpirytoDate, flag, appointmentType, insuranceCompany, UserId, 2, locId, status, fromIndex, toIndex, lastServiceFromDate, lastServiceToDate, tagIId, orderFilter, dueDateEdited);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getNthand1(searchPattern, ExpiryfromDate, ExpirytoDate, flag, appointmentType, insuranceCompany, UserId, 2, locId, status, 0, totalCount, lastServiceFromDate, lastServiceToDate, tagIId, dueDateEdited).Count;
                        }
                    }
                    else if (filter.getDataFor == 8) //Future Followup
                    {
                        totalCount = getfuturefollowupBucketCount("", "", "", "", "", UserId, "", "", dueDateEdited);
                        if (campaignName == "" && fromexpirydaterange == "" && toexpirydaterange == "" && modelName == "" && flag == "" && locId == "" && searchPattern == "" && tag == "0")
                        {
                            filter.isFiltered = false;
                        }
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }

                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }

                        insuracecallLogs = getFutureFollow(fromexpirydaterange, toexpirydaterange, campaignName, modelName, flag, searchPattern, UserId, locId, fromIndex, toIndex, lastServiceFromDate, lastServiceToDate, tagIId, orderFilter, dueDateEdited);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getFutureFollow(fromexpirydaterange, toexpirydaterange, campaignName, modelName, flag, searchPattern, UserId, locId, 0, totalCount, lastServiceFromDate, lastServiceToDate, tagIId, dueDateEdited).Count;
                        }
                    }
                    else if (filter.getDataFor == 9)  //Paid 
                    {
                        totalCount = getpaidBucketCount("", "", "", "", "", "", "", "", UserId, "", "", dueDateEdited);
                        if (campaignName == "" && ExpiryfromDate == "" && ExpirytoDate == "" && flag == "" && locId == "" && renewalType == "" && searchPattern == "" && tag == "0")
                        {
                            filter.isFiltered = false;
                        }
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }

                        insuracecallLogs = getPaid(searchPattern, ExpiryfromDate, ExpirytoDate, campaignName, "", "", flag, "", UserId, 1, locId, renewalType, fromIndex, toIndex, lastServiceFromDate, lastServiceToDate, tagIId, orderFilter, dueDateEdited);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getPaid(searchPattern, ExpiryfromDate, ExpirytoDate, campaignName, "", "", flag, "", UserId, 1, locId, renewalType, 0, totalCount, lastServiceFromDate, lastServiceToDate, tagIId, dueDateEdited).Count;
                        }
                    }
                    else if (filter.getDataFor == 10) //Other
                    {
                        totalCount = getotherBucketCount("", "", "", "", "", "", "", "", UserId, "", "", dueDateEdited);
                        if (campaignName == "" && ExpiryfromDate == "" && ExpirytoDate == "" && flag == "" && locId == "" && renewalType == "" && searchPattern == "" && tag == "0")
                        {
                            filter.isFiltered = false;
                        }
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }

                        insuracecallLogs = getOthers(searchPattern, ExpiryfromDate, ExpirytoDate, campaignName, "", "", flag, "", UserId, 1, locId, renewalType, fromIndex, toIndex, lastServiceFromDate, lastServiceToDate, tagIId, orderFilter, dueDateEdited);
                        if (filter.isFiltered == true)

                        {
                            patternCount = getOthers(searchPattern, ExpiryfromDate, ExpirytoDate, campaignName, "", "", flag, "", UserId, 1, locId, renewalType, 0, totalCount, lastServiceFromDate, lastServiceToDate, tagIId, dueDateEdited).Count;
                        }
                    }
                   else if (filter.getDataFor == 11) //Due Today
                    {

                        totalCount = getdueTodayBucketCount(UserId, "", "", "", "", "", "", "", "", "","","");
                        if ( ExpiryfromDate == "" && ExpirytoDate == "" && renewalType == "" && searchPattern == "" && fromsaledate == ""  &&  tosaledate== ""  && modelName == "")
                        {
                            filter.isFiltered = false;
                        }
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }

                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }

                        insuracecallLogs = getdueToday(UserId, campaignName,ExpiryfromDate,ExpirytoDate,searchPattern, renewalType,locId,flag,  fromsaledate,tosaledate,modelName,dueDateEdited);
                        if(filter.isFiltered==true)
                        {
                            patternCount = getdueToday(UserId, campaignName, ExpiryfromDate, ExpirytoDate, searchPattern, renewalType,locId, flag, fromsaledate, tosaledate, modelName, dueDateEdited).Count;
                        }
                    }
                    /* else if (filter.getDataFor == 11) //Receipt
                     {
                         totalCount = getreceiptdBucketCount(UserId, "", "", "", "", "", "", "", "", dueDateEdited);
                         if (campaignName == "" && fromexpirydaterange == "" && toexpirydaterange == "" && flag == "" && modelName == "" && locId == "" && renewalType == "" && searchPattern == "" && tag == "0")
                         {
                             filter.isFiltered = false;
                         }
                         if (toIndex < 0)
                         {
                             toIndex = 10;
                         }
                         if (toIndex > totalCount)
                         {
                             toIndex = totalCount;
                         }
                         insuracecallLogs = getreceiptCall(UserId, campaignName, fromexpirydaterange, toexpirydaterange, flag, modelName, searchPattern, locId, renewalType, fromIndex, toIndex ,lastServiceFromDate, lastServiceToDate, tagIId, orderFilter, dueDateEdited);
                         if (filter.isFiltered == true)
                         {
                             patternCount = getreceiptCall(UserId, campaignName, fromexpirydaterange, toexpirydaterange, flag, modelName, searchPattern, locId, renewalType, 0, totalCount, lastServiceFromDate, lastServiceToDate, tagIId, dueDateEdited).Count;
                         }
                     }*/

                }
                catch (Exception ex)
                {

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

            if (insuracecallLogs != null)
            {
                if (filter.isFiltered == true)
                {
                    var JsonData = Json(new { data = insuracecallLogs, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = patternCount, exception = exception }, JsonRequestBehavior.AllowGet);
                    JsonData.MaxJsonLength = int.MaxValue;
                    return JsonData;
                }
                else if (filter.isFiltered == false)
                {
                    var JsonData = Json(new { data = insuracecallLogs, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount, exception = exception }, JsonRequestBehavior.AllowGet);
                    JsonData.MaxJsonLength = int.MaxValue;
                    return JsonData;
                }
            }

            return Json(new { data = "", draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0, exception = exception }, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region Scheduled Call Bucket
        public int getScheduledBucketCount(long UserId, string campaignName, string ExpiryfromDate, string ExpirytoDate, string flag, string modelName, string searchPattern, string locId, string renewaltype, string isEditedDate = "")
        {
            int totalCount = 0;
            using (var db = new AutoSherDBContext())
            {
                totalCount = db.Database.SqlQuery<int>("call insuranceScheduledCallsCount(@wyzUserId,@in_campaign_id,@instartdate,@inenddate,@in_flag,@in_model,@pattern,@locId,@inrenType,@isEditedDate)",
                            new MySqlParameter("@wyzUserId", UserId),
                            new MySqlParameter("@in_campaign_id", campaignName),
                            new MySqlParameter("@instartdate", ExpiryfromDate),
                            new MySqlParameter("@inenddate", ExpirytoDate),
                            new MySqlParameter("@in_flag", flag),
                            new MySqlParameter("@in_model", modelName),
                            new MySqlParameter("@pattern", searchPattern),
                            new MySqlParameter("@locId", locId),
                            new MySqlParameter("@inrenType", renewaltype),
                            new MySqlParameter("@isEditedDate", isEditedDate)).FirstOrDefault();

                return totalCount;
            }
        }
        public List<insuranceBuketData> getScheduledCall(long id, string campaignName,
       string fromexpirydaterange, string toexpirydaterange, string flag, string modelName, string searchPattern,
       string locId, string renewaltype, long fromIndex, long toIndex, string lastServiceFromDate,string lastServiceToDate, long tag,string orderFilter,string isEditedDate = "")
        {
            List<insuranceBuketData> callLogsInsurence = null;
            using (var db = new AutoSherDBContext())
            {
                string str = @"CALL insuranceScheduledCalls(@wyzuserid,@in_campaign_id,@instartdate,@inenddate,@in_flag,@in_model,@pattern,@locId,@inrenType,@start_with,@length,@isEditedDate,@inLastservicedatefrom,@inLastservicedateto,@intag,@insortfilter);";

                MySqlParameter[] sqlParameter = new MySqlParameter[]
                {
                        new MySqlParameter("wyzuserid", id),
                        new MySqlParameter("in_campaign_id", campaignName),
                        new MySqlParameter("instartdate", fromexpirydaterange),
                        new MySqlParameter("inenddate", toexpirydaterange),
                        new MySqlParameter("in_flag", flag),
                        new MySqlParameter("in_model", modelName),
                        new MySqlParameter("pattern", searchPattern),
                        new MySqlParameter("locId", locId),
                        new MySqlParameter("inrenType", renewaltype),
                        new MySqlParameter("start_with", fromIndex),
                        new MySqlParameter("length", toIndex),
                        new MySqlParameter("isEditedDate", isEditedDate),
                        new MySqlParameter("inLastservicedatefrom", lastServiceFromDate),
                        new MySqlParameter("inLastservicedateto", lastServiceToDate),
                        new MySqlParameter("@intag", tag),
                        new MySqlParameter("@insortfilter", orderFilter)

                };
                callLogsInsurence = db.Database.SqlQuery<insuranceBuketData>(str, sqlParameter).ToList();
            }
            return callLogsInsurence;
        }
        #endregion

        #region Pending Followup,Appointments,Renewal Not Required Bucket 
        public int getBucket234Count(string instartdate, string inenddate, string incampaignname, string inflag, string infromApdate, string intoApdate, string infromCalldate, string intoCalldate, string inAptype, string inmodel, string inreasons, string pattern, long wyzuserid, long dispositiontype, string locId, string isEditedDate = "")
        {
            int totalCount = 0;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    totalCount = db.Database.SqlQuery<int>("call insurancefilterforDispositionContactsCount" +
                          "(@instartdate,@inenddate,@incampaignname,@inflag,@infromApdate,@intoApdate,@infromCalldate,@intoCalldate ,@inAptype,@inmodel,@inreasons,@pattern,@wyzuserid,@dispositiontype,@locId,@isEditedDate)",
                          new MySqlParameter[] {
                            new MySqlParameter("@instartdate", instartdate),
                            new MySqlParameter("@inenddate",inenddate),
                            new MySqlParameter("@incampaignname", incampaignname),
                            new MySqlParameter("@inflag",inflag),
                            new MySqlParameter("@infromApdate",infromApdate),
                            new MySqlParameter("@intoApdate", intoApdate),
                            new MySqlParameter("@infromCalldate",infromCalldate),
                            new MySqlParameter("@intoCalldate", intoCalldate),
                            new MySqlParameter("@inAptype",inAptype),
                            new MySqlParameter("@inmodel", inmodel),
                            new MySqlParameter("@inreasons", inreasons),
                            new MySqlParameter("@pattern",pattern),
                            new MySqlParameter("@wyzuserid", wyzuserid),
                            new MySqlParameter("@dispositiontype",dispositiontype),
                            new MySqlParameter("@locId",locId),
                            new MySqlParameter("@isEditedDate",isEditedDate)
                          }).FirstOrDefault();

                    return totalCount;

                }
            }
            catch (Exception ex)
            {

            }
            return totalCount;

        }
        public List<insuranceBuketData> getBucket234(string fromexpirydaterange, string toexpirydaterange, string campaignName, string appointmentFromDate, string appointmentToDate, string callFromDate, string callToDate, string appointmentType, string modelName, string flag, string reasons, string searchPattern, long id, long typeOfdispo, string locId, long fromIndex, long toIndex, string lastServiceFromDate, string lastServiceToDate , long tag, string orderFilter, string isEditedDate = "")
        {
            List<insuranceBuketData> dispositionLoadInsurances = new List<insuranceBuketData>();
            using (var db = new AutoSherDBContext())
            {
                string str = @"CALL insurancefilterforDispositionContacts(@instartdate,@inenddate,@incampaignname,@inflag,@infromApdate,@intoApdate,@infromCalldate,@intoCalldate,@inAptype,@inmodel,@inreasons,@pattern,@wyzuserid,@dispositiontype,@locId,@start_with,@length,@isEditedDate,@inLastservicedatefrom,@inLastservicedateto,@intag,@insortfilter);";

                MySqlParameter[] sqlParameter = new MySqlParameter[]
                {
                        new MySqlParameter("instartdate", fromexpirydaterange),
                        new MySqlParameter("inenddate", toexpirydaterange),
                        new MySqlParameter("incampaignname", campaignName),
                        new MySqlParameter("inflag", flag),
                        new MySqlParameter("infromApdate", appointmentFromDate),
                        new MySqlParameter("intoApdate", appointmentToDate),
                        new MySqlParameter("infromCalldate", callFromDate),
                        new MySqlParameter("intoCalldate", callToDate),
                        new MySqlParameter("inAptype", appointmentType),
                        new MySqlParameter("inmodel", modelName),
                        new MySqlParameter("inreasons", reasons),
                        new MySqlParameter("pattern", searchPattern),
                        new MySqlParameter("wyzuserid", id),
                        new MySqlParameter("dispositiontype", typeOfdispo),
                        new MySqlParameter("locId", locId),
                        new MySqlParameter("start_with", fromIndex),
                        new MySqlParameter("length", toIndex),
                        new MySqlParameter("isEditedDate", isEditedDate),
                        new MySqlParameter("inLastservicedatefrom", lastServiceFromDate),
                        new MySqlParameter("inLastservicedateto", lastServiceToDate),
                        new MySqlParameter("@intag", tag),
                         new MySqlParameter("@insortfilter", orderFilter)

                };
                dispositionLoadInsurances = db.Database.SqlQuery<insuranceBuketData>(str, sqlParameter).ToList();
            }

            return dispositionLoadInsurances;
        }
        #endregion

        #region NonContact Bucket Details
        public int getnoncontactBucketCount(string searchPattern, string fromexpirydaterange, string toexpirydaterange, string campaignName, string callFromDate, string callToDate, string flag, string attempts, long UserId, string locId, string lastDispo, string isEditedDate = "" )
        {
            int totalCount = 0;
            using (var db = new AutoSherDBContext())
            {
                totalCount = db.Database.SqlQuery<int>("call insurancefilterforNonContactsCount( @pattern,@startdate,@enddate,@campaignname,@callFromDate,@callToDate,@flag,@attempts,@wyzuserid,@dispositiontype,@locId,@isEditedDate,@lastdispo)",
                       new MySqlParameter("@pattern", searchPattern),
                       new MySqlParameter("@startdate", fromexpirydaterange),
                       new MySqlParameter("@enddate", toexpirydaterange),
                       new MySqlParameter("@campaignname", campaignName),
                       new MySqlParameter("@callFromDate", callFromDate),
                       new MySqlParameter("@callToDate", callToDate),
                       new MySqlParameter("@flag", flag),
                       new MySqlParameter("@attempts", attempts),
                       new MySqlParameter("@wyzuserid", UserId),
                       new MySqlParameter("@dispositiontype", 1),
                       new MySqlParameter("@locId", locId),
                       new MySqlParameter("@isEditedDate", isEditedDate),
                        new MySqlParameter("@lastdispo", lastDispo)

                       ).FirstOrDefault();
                return totalCount;
            }
        }

        public List<insuranceBuketData> getNonContacts(string searchPattern,
    string fromexpirydaterange, string toexpirydaterange, string campaignName, string callFromDate,
    string callToDate, string flag, string attempts, long id, long typeOfdispo, string locId, long fromIndex,
    long toIndex, string lastServiceFromDate, string lastServiceToDate , string lastDispo, long tag,string orderFilter, string isEditedDate = "")
        {
            List<insuranceBuketData> dispositionLoadInsurances = new List<insuranceBuketData>();

            using (var db = new AutoSherDBContext())
            {
                if (Session["DealerCode"].ToString() == "DREAMMACHINE")
                { 
                    string str = @"CALL insurancefilterforNonContacts(@pattern,@instartdate,@inenddate,@incampaignname,@incallFromDate,@incallToDate,@inflag,@inattempts,@wyzuserid,@dispositiontype,@locId,@start_with,@length,@isEditedDate,@inLastservicedatefrom,@inLastservicedateto,@lastdispo,@insortfilter);";

                MySqlParameter[] sqlParameter = new MySqlParameter[]
                {
                        new MySqlParameter("instartdate", fromexpirydaterange),
                        new MySqlParameter("inenddate", toexpirydaterange),
                        new MySqlParameter("incampaignname", campaignName),
                        new MySqlParameter("wyzuserid", id),
                        new MySqlParameter("incallFromDate", callFromDate),
                        new MySqlParameter("incallToDate", callToDate),
                        new MySqlParameter("inflag", flag),
                        new MySqlParameter("inattempts", attempts),
                        new MySqlParameter("dispositiontype", typeOfdispo),
                        new MySqlParameter("pattern", searchPattern),
                        new MySqlParameter("locId", locId),
                        new MySqlParameter("start_with", fromIndex),
                        new MySqlParameter("length", toIndex),
                        new MySqlParameter("isEditedDate", isEditedDate),
                        new MySqlParameter("inLastservicedatefrom", lastServiceFromDate),
                        new MySqlParameter("inLastservicedateto", lastServiceToDate),
                        new MySqlParameter("lastdispo", lastDispo),
                        new MySqlParameter("@insortfilter", orderFilter)


                };
                dispositionLoadInsurances = db.Database.SqlQuery<insuranceBuketData>(str, sqlParameter).ToList();
            }

                else
                {

                  
                        string str = @"CALL insurancefilterforNonContacts(@pattern,@instartdate,@inenddate,@incampaignname,@incallFromDate,@incallToDate,@inflag,@inattempts,@wyzuserid,@dispositiontype,@locId,@start_with,@length,@isEditedDate,@inLastservicedatefrom,@inLastservicedateto,@intag,@insortfilter);";

                        MySqlParameter[] sqlParameter = new MySqlParameter[]
                        {
                        new MySqlParameter("instartdate", fromexpirydaterange),
                        new MySqlParameter("inenddate", toexpirydaterange),
                        new MySqlParameter("incampaignname", campaignName),
                        new MySqlParameter("wyzuserid", id),
                        new MySqlParameter("incallFromDate", callFromDate),
                        new MySqlParameter("incallToDate", callToDate),
                        new MySqlParameter("inflag", flag),
                        new MySqlParameter("inattempts", attempts),
                        new MySqlParameter("dispositiontype", typeOfdispo),
                        new MySqlParameter("pattern", searchPattern),
                        new MySqlParameter("locId", locId),
                        new MySqlParameter("start_with", fromIndex),
                        new MySqlParameter("length", toIndex),
                        new MySqlParameter("isEditedDate", isEditedDate),
                        new MySqlParameter("inLastservicedatefrom", lastServiceFromDate),
                        new MySqlParameter("inLastservicedateto", lastServiceToDate),
                        new MySqlParameter("@intag", tag),
                        new MySqlParameter("@insortfilter", orderFilter)



                        };
                        dispositionLoadInsurances = db.Database.SqlQuery<insuranceBuketData>(str, sqlParameter).ToList();
                    }
                }

            return dispositionLoadInsurances;
        }
        #endregion

        #region N+1 day and Nth Day Bucket Details
        public int getNthand1Count(long sbday, long inChasers_id, string inworkshop, string instatus, string ininsurancecompany, string intypeofpickup, string inflag, string instartdate, string inenddate, string pattern, string isEditedDate = "")
        {
            int totalCount = 0;
            using (var db = new AutoSherDBContext())
            {
                totalCount = db.Database.SqlQuery<int>("call InsuranceAppointmentPreviousDayCallsCount" +
                    "(@sbday,@inChasers_id,@inworkshop,@instatus,@ininsurancecompany,@intypeofpickup,@inflag,@instartdate,@inenddate,@pattern,@isEditedDate)",
                         new MySqlParameter[] {
                                new MySqlParameter("@sbday",sbday),
                                new MySqlParameter("@inChasers_id", inChasers_id),
                                new MySqlParameter("@inworkshop", inworkshop),
                                new MySqlParameter("@instatus", instatus),
                                new MySqlParameter("@ininsurancecompany",ininsurancecompany),
                                new MySqlParameter("@intypeofpickup",intypeofpickup),
                                new MySqlParameter("@inflag",inflag),
                                new MySqlParameter("@instartdate", instartdate),
                                new MySqlParameter("@inenddate",inenddate),
                                new MySqlParameter("@pattern",pattern),
                                new MySqlParameter("@isEditedDate",isEditedDate)

                         }).FirstOrDefault();

                return totalCount;

            }
        }

        public List<insuranceBuketData> getNthand1(string searchpattern, string fromexpirydaterange, string toexpirydaterange, string flag, string appointmenttype, string inscompany, long chasser_id, long typeofdispo, string locid, string status, long fromindex, long toindex,string lastServiceFromDate, string lastServiceToDate, long tag, string orderFilter, string isEditedDate = "")
        {
            List<insuranceBuketData> calllogajaxloads = new List<insuranceBuketData>();
            using (var db = new AutoSherDBContext())
            {
                string str = @"call insuranceappointmentpreviousdaycalls(@sbday,@inchasers_id,@inworkshop,@instatus,@ininsurancecompany,@intypeofpickup,@inflag,@instartdate,@inenddate,@pattern,@startwith,@length,@isEditedDate,@inLastservicedatefrom,@inLastservicedateto,@intag,@insortfilter);";

                MySqlParameter[] sqlparameter = new MySqlParameter[]
                {
                        new MySqlParameter("sbday", (int) typeofdispo),
                        new MySqlParameter("inchasers_id", (int)chasser_id),
                        new MySqlParameter("inworkshop", locid),
                        new MySqlParameter("instatus", status),
                        new MySqlParameter("ininsurancecompany", inscompany),
                        new MySqlParameter("intypeofpickup", appointmenttype),
                        new MySqlParameter("inflag", flag),
                        new MySqlParameter("instartdate", fromexpirydaterange),
                        new MySqlParameter("inenddate", toexpirydaterange),
                        new MySqlParameter("pattern", searchpattern),
                        new MySqlParameter("startwith", fromindex),
                        new MySqlParameter("length", toindex),
                        new MySqlParameter("isEditedDate", isEditedDate),
                        new MySqlParameter("inLastservicedatefrom", lastServiceFromDate),
                        new MySqlParameter("inLastservicedateto", lastServiceToDate),
                        new MySqlParameter("@intag", tag),
                        new MySqlParameter("@insortfilter", orderFilter)


                };
                calllogajaxloads = db.Database.SqlQuery<insuranceBuketData>(str, sqlparameter).ToList();
            }

            return calllogajaxloads;
        }
        #endregion

        #region Future FollowUp Bucket Details
        public int getfuturefollowupBucketCount(string fromexpirydaterange, string toexpirydaterange, string campaignName, string flag, string searchPattern, int UserId, string locId, string renewaltype, string isEditedDate = "" )
        {
            int totalCount = 0;
            using (var db = new AutoSherDBContext())
            {
                totalCount = db.Database.SqlQuery<int>("call insurancefilterforFutureFollowupCount(@instartdate,@inenddate,@incampaignname,@inflag,@pattern,@wyzuserid,@locId,@inrenType,@isEditedDate)",
                          new MySqlParameter[]
                          { new MySqlParameter("@instartdate",fromexpirydaterange),
                                new MySqlParameter("@inenddate",toexpirydaterange),
                                new MySqlParameter("@incampaignname",campaignName),
                                new MySqlParameter("@inflag",flag),
                                new MySqlParameter("@pattern",searchPattern),
                                new MySqlParameter("@wyzuserid",UserId),
                                new MySqlParameter("@locId",locId),
                                new MySqlParameter("@inrenType",renewaltype),
                                new MySqlParameter("@isEditedDate",isEditedDate)
                          }).FirstOrDefault();
                return totalCount;
            }
        }

        public List<insuranceBuketData> getFutureFollow(string fromexpirydaterange, string toexpirydaterange, string campaignName, string modelName, string flag, string searchPattern, long id, string locId, long fromIndex, long toIndex, string lastServiceFromDate,string lastServiceToDate, long tag, string orderFilter, string isEditedDate = "")
        {
            List<insuranceBuketData> dispositionLoadInsurances = new List<insuranceBuketData>();
            using (var db = new AutoSherDBContext())
            {
                string str = @"CALL insurancefilterforFutureFollowup(@instartdate,@inenddate, @incampaignname,@inflag,@inmodel,@pattern,@wyzuserid,@locId,@start_with, @length,@isEditedDate,@inLastservicedatefrom,@inLastservicedateto,@intag,@insortfilter);";

                MySqlParameter[] sqlParameter = new MySqlParameter[]
                {
                        new MySqlParameter("instartdate", fromexpirydaterange),
                        new MySqlParameter("inenddate", toexpirydaterange),
                        new MySqlParameter("incampaignname", campaignName),
                        new MySqlParameter("inflag", flag),
                        new MySqlParameter("inmodel", modelName),
                        new MySqlParameter("pattern", searchPattern),
                        new MySqlParameter("wyzuserid", id),
                        new MySqlParameter("locId", locId),
                        new MySqlParameter("start_with", fromIndex),
                        new MySqlParameter("length", toIndex),
                        new MySqlParameter("isEditedDate", isEditedDate),
                        new MySqlParameter("inLastservicedatefrom", lastServiceFromDate),
                        new MySqlParameter("inLastservicedateto", lastServiceToDate),
                        new MySqlParameter("@intag", tag),
                        new MySqlParameter("@insortfilter", orderFilter)

            };
                dispositionLoadInsurances = db.Database.SqlQuery<insuranceBuketData>(str, sqlParameter).ToList();
            }
            return dispositionLoadInsurances;
        }
        #endregion

        #region Paid Bucket Details
        public int getpaidBucketCount(string searchPattern, string ExpiryfromDate, string ExpirytoDate, string campaignName, string callFromDate, string callToDate, string flag, string attempts, long UserId, string locId, string renewaltype, string isEditedDate = "")
        {
            int totalCount = 0;
            using (var db = new AutoSherDBContext())
            {
                totalCount = db.Database.SqlQuery<int>("call insurancefilterforPaidCount(@pattern,@instartdate,@inenddate,@incampaignname,@incallFromDate,@incallToDate,@inflag,@inattempts,@wyzuserid,@dispositiontype,@locId,@inrenType,@isEditedDate)",
                            new MySqlParameter("@pattern", searchPattern),
                            new MySqlParameter("@instartdate", ExpiryfromDate),
                            new MySqlParameter("@inenddate", ExpirytoDate),
                            new MySqlParameter("@incampaignname", campaignName),
                            new MySqlParameter("@incallFromDate", callFromDate),
                            new MySqlParameter("@incallToDate", callToDate),
                            new MySqlParameter("@inflag", flag),
                            new MySqlParameter("@inattempts", attempts),
                            new MySqlParameter("@wyzuserid", UserId),
                            new MySqlParameter("@dispositiontype", 1),
                            new MySqlParameter("@locId", locId),
                            new MySqlParameter("@inrenType", renewaltype),
                            new MySqlParameter("@isEditedDate", isEditedDate)).FirstOrDefault();
                return totalCount;
            }
        }

        public List<insuranceBuketData> getPaid(string pattern, string instartdate, string inenddate, string incampaignname, string incallFromDate, string incallToDate, string inflag, string inattempts, long wyzuserid, long dispositiontype, string locId, string inrenType, long start_with, long length,string lastServiceFromDate,string lastServiceToDate, long tag,string orderFilter, string isEditedDate = "")
        {
            List<insuranceBuketData> dispositionLoadInsurances = new List<insuranceBuketData>();
            using (var db = new AutoSherDBContext())
            {
                string str = @"CALL insurancefilterforPaid(@pattern,@instartdate, @inenddate,@incampaignname,@incallFromDate,@incallToDate,@inflag,@inattempts,@wyzuserid, @dispositiontype,@locId,@inrenType,@start_with,@length,@isEditedDate,@inLastservicedatefrom,@inLastservicedateto,@intag,@insortfilter);";

                MySqlParameter[] sqlParameter = new MySqlParameter[]
                {
                        new MySqlParameter("pattern", pattern),
                        new MySqlParameter("instartdate", instartdate),
                        new MySqlParameter("inenddate", inenddate),
                        new MySqlParameter("incampaignname", incampaignname),
                        new MySqlParameter("incallFromDate", incallFromDate),
                        new MySqlParameter("incallToDate", incallToDate),
                        new MySqlParameter("inflag", inflag),
                        new MySqlParameter("inattempts", inattempts),
                        new MySqlParameter("wyzuserid", wyzuserid),
                        new MySqlParameter("dispositiontype", dispositiontype),
                        new MySqlParameter("locId", locId),
                        new MySqlParameter("inrenType", inrenType),
                        new MySqlParameter("start_with", start_with),
                        new MySqlParameter("length", length),
                        new MySqlParameter("isEditedDate", isEditedDate),
                        new MySqlParameter("inLastservicedatefrom", lastServiceFromDate),
                        new MySqlParameter("inLastservicedateto", lastServiceToDate),
                        new MySqlParameter("@intag", tag),
                        new MySqlParameter("@insortfilter", orderFilter)

            };
                dispositionLoadInsurances = db.Database.SqlQuery<insuranceBuketData>(str, sqlParameter).ToList();
            }

            return dispositionLoadInsurances;
        }
        #endregion

        #region OtherBucket Details

        public int getotherBucketCount(string searchPattern, string ExpiryfromDate, string ExpirytoDate, string campaignName, string callFromDate, string callToDate, string flag, string attempts, long UserId, string locId, string renewaltype, string isEditedDate = "")
        {
            int totalCount = 0;
            using (var db = new AutoSherDBContext())
            {
                totalCount = db.Database.SqlQuery<int>("call insurancefilterforOthersCount(@pattern,@instartdate,@inenddate,@incampaignname,@incallFromDate,@incallToDate,@inflag,@inattempts,@wyzuserid,@dispositiontype,@locId,@inrenType,@isEditedDate)",
                           new MySqlParameter("@pattern", searchPattern),
                           new MySqlParameter("@instartdate", ExpiryfromDate),
                           new MySqlParameter("@inenddate", ExpirytoDate),
                           new MySqlParameter("@incampaignname", campaignName),
                           new MySqlParameter("@incallFromDate", callFromDate),
                           new MySqlParameter("@incallToDate", callToDate),
                           new MySqlParameter("@inflag", flag),
                           new MySqlParameter("@inattempts", attempts),
                           new MySqlParameter("@wyzuserid", UserId),
                           new MySqlParameter("@dispositiontype", 1),
                           new MySqlParameter("@locId", locId),
                           new MySqlParameter("@inrenType", renewaltype),
                           new MySqlParameter("@isEditedDate", isEditedDate)).FirstOrDefault();
                return totalCount;
            }
        }

        public List<insuranceBuketData> getOthers(string pattern, string instartdate, string inenddate, string incampaignname, string incallFromDate, string incallToDate, string inflag, string inattempts, long wyzuserid, long dispositiontype, string locId, string inrenType, long start_with, long length, string lastServiceFromDate, string lastServiceToDate, long tag, string orderFilter, string isEditedDate = "")
        {
            List<insuranceBuketData> dispositionLoadInsurances = new List<insuranceBuketData>();
            using (var db = new AutoSherDBContext())
            {
                string str = @"CALL insurancefilterforOthers(@pattern,@instartdate, @inenddate,@incampaignname,@incallFromDate,@incallToDate,@inflag,@inattempts,@wyzuserid, @dispositiontype,@locId,@inrenType,@start_with,@length,@isEditedDate,@inLastservicedatefrom,@inLastservicedateto,@intag,@insortfilter);";

                MySqlParameter[] sqlParameter = new MySqlParameter[]
                {
                        new MySqlParameter("pattern", pattern),
                        new MySqlParameter("instartdate", instartdate),
                        new MySqlParameter("inenddate", inenddate),
                        new MySqlParameter("incampaignname", incampaignname),
                        new MySqlParameter("incallFromDate", incallFromDate),
                        new MySqlParameter("incallToDate", incallToDate),
                        new MySqlParameter("inflag", inflag),
                        new MySqlParameter("inattempts", inattempts),
                        new MySqlParameter("wyzuserid", wyzuserid),
                        new MySqlParameter("dispositiontype", dispositiontype),
                        new MySqlParameter("locId", locId),
                        new MySqlParameter("inrenType", inrenType),
                        new MySqlParameter("start_with", start_with),
                        new MySqlParameter("length", length),
                        new MySqlParameter("isEditedDate", isEditedDate),
                        new MySqlParameter("inLastservicedatefrom", lastServiceFromDate),
                        new MySqlParameter("inLastservicedateto", lastServiceToDate),
                        new MySqlParameter("@intag", tag),
                        new MySqlParameter("@insortfilter", orderFilter)

            };
                dispositionLoadInsurances = db.Database.SqlQuery<insuranceBuketData>(str, sqlParameter).ToList();
            }

            return dispositionLoadInsurances;
        }
        #endregion
        #region Due Today Bucket Details
        public int getdueTodayBucketCount(long UserId, string campaignName, string fromexpiryDate, string toexpiryDate, string searchPattern, string renewalType,string locId, string flag,  string fromSaledate, string toSalesDate, string modelName, string isEditedDate = "")
        {
            int totalCount = 0;
            using (var db = new AutoSherDBContext())
            {
                totalCount = db.Database.SqlQuery<int>("call insurancepolicyexprytodaycallscount(@wyzuserid,@incampaignname,@instartdate,@inenddate,@pattern,@inrenType,@locId,@inflag,@infromSaledate,@intoSaledate,@in_model,@isEditedDate);",
                          new MySqlParameter[]
                          {
                                new MySqlParameter("@wyzuserid",UserId),
                                new MySqlParameter("@incampaignname", campaignName),
                                new MySqlParameter("@instartdate",fromexpiryDate),
                                new MySqlParameter("@inenddate",toexpiryDate),
                                new MySqlParameter("@pattern",searchPattern),
                                new MySqlParameter("@inrenType",renewalType),
                                new MySqlParameter("@inflag", flag),
                                new MySqlParameter("locId", locId),
                                new MySqlParameter("@infromSaledate", fromSaledate),
                                new MySqlParameter("@intoSaledate", toSalesDate),
                                new MySqlParameter("@in_model", modelName),
                                new MySqlParameter("@isEditedDate", isEditedDate)

                          }).FirstOrDefault();
                return totalCount;
            }
        }

        public List<insuranceBuketData> getdueToday(long id, string campaignName, string fromexpiryDate, string toexpiryDate, string searchPattern, string renewalType,string locId, string flag,  string fromSaledate, string toSalesDate, string modelName, string isEditedDate = "")
        {
            List<insuranceBuketData> dispositionLoadInsurances = new List<insuranceBuketData>();
            using (var db = new AutoSherDBContext())
            {
                string str = @"CALL insurancepolicyexprytodaycalls( @wyzuserid,@incampaignname,@instartdate,@inenddate,@pattern, @inrenType,@locId,@inflag,@infromSaledate,@intoSaledate,@in_model,@isEditedDate);";

                MySqlParameter[] sqlParameter = new MySqlParameter[]
                {

                        new MySqlParameter("@wyzuserid", id),
                         new MySqlParameter("@incampaignname", campaignName),
                        new MySqlParameter("@instartdate", fromexpiryDate),
                        new MySqlParameter("@inenddate", toexpiryDate),
                        new MySqlParameter("@pattern", searchPattern),
                        new MySqlParameter("@inrenType", renewalType),
                        new MySqlParameter("@inflag", flag),
                        new MySqlParameter("@locId", locId),
                      
                        new MySqlParameter("@infromSaledate", fromSaledate),
                        new MySqlParameter("@intoSaledate", toSalesDate),
                        new MySqlParameter("@in_model", modelName),
                        new MySqlParameter("@isEditedDate", isEditedDate)

            };
                dispositionLoadInsurances = db.Database.SqlQuery<insuranceBuketData>(str, sqlParameter).ToList();
            }
            return dispositionLoadInsurances;
        }
        #endregion

        /*   #region Receipt Bucket Details
           public int getreceiptdBucketCount(long UserId, string campaignName, string ExpiryfromDate, string ExpirytoDate, string flag, string modelName, string searchPattern, string locId, string insuranceCompanyd, string isEditedDate = "")
           {
               int totalCount = 0;
               using (var db = new AutoSherDBContext())
               {
                   totalCount = db.Database.SqlQuery<int>("call insurancefilterReceiptCount(@wyzuserid,@isEditedDate)",
                               new MySqlParameter("@wyzuserid", UserId), new MySqlParameter("@isEditedDate", isEditedDate)).FirstOrDefault();
                   return totalCount;
               }
           }

           public List<insuranceBuketData> getreceiptCall(long id, string campaignName, string fromexpirydaterange, string toexpirydaterange, string flag, string modelName, string searchPattern, string locId, string renewalType, long fromIndex, long toIndex,string lastServiceFromDate,string  lastServiceToDate, long tag,string orderFilter, string isEdited = "")
           {
               List<insuranceBuketData> callLogsInsurence = null;

               using (var db = new AutoSherDBContext())
               {
                   string str = @"CALL insurancefilterReceipt(@wyzuserid,@in_campaign_id,@instartdate,@inenddate,@in_flag,@in_model,@pattern,@locId,@inrenType,@start_with,@length,@isEditedDate,@inLastservicedatefrom,@inLastservicedateto ,@intag,@insortfilter);";
                   MySqlParameter[] sqlParameter = new MySqlParameter[]
                   {
                           new MySqlParameter("wyzuserid", id),
                           new MySqlParameter("in_campaign_id", campaignName),
                           new MySqlParameter("instartdate", fromexpirydaterange),
                           new MySqlParameter("inenddate", toexpirydaterange),
                           new MySqlParameter("in_flag", flag),
                           new MySqlParameter("in_model", modelName),
                           new MySqlParameter("pattern", searchPattern),
                           new MySqlParameter("locId", locId),
                           new MySqlParameter("inrenType", renewalType),
                           new MySqlParameter("start_with", fromIndex),
                           new MySqlParameter("length", toIndex),
                           new MySqlParameter("isEditedDate", isEdited),
                           new MySqlParameter("inLastservicedatefrom", lastServiceFromDate),
                           new MySqlParameter("inLastservicedateto", lastServiceToDate),
                           new MySqlParameter("@intag", tag),
                           new MySqlParameter("@insortfilter", orderFilter)

                   };
                   callLogsInsurence = db.Database.SqlQuery<insuranceBuketData>(str, sqlParameter).ToList();
               }
               return callLogsInsurence;
           }
           #endregion*/

        #region FilterCount
        public ActionResult getFilterCount(string insurenceData)
        {
            List<insurenceFilterCount> dispositionLoadInsurances = new List<insurenceFilterCount>();

            int UserId = Convert.ToInt32(Session["UserId"].ToString());

            InsurenceFilter filter = new InsurenceFilter();
            if (insurenceData != null)
            {
                filter = JsonConvert.DeserializeObject<InsurenceFilter>(insurenceData);
            }

            if (filter.isFiltered == true)
            {

                string camp, ExpiryfromDate, attempts, ExpirytoDate, flag, locId, Insu, modelName, appointmentFromDate, appointmentToDate, callFromDate, callToDate, appointmentType, reasons, isEditedDate;

                camp = filter.Campaign == null ? "" : filter.Campaign;
                ExpiryfromDate = filter.ExFromDate == null ? "" : Convert.ToDateTime(filter.ExFromDate.ToString()).ToString("yyyy-MM-dd");
                ExpirytoDate = filter.ExToDate == null ? "" : Convert.ToDateTime(filter.ExToDate.ToString()).ToString("yyyy-MM-dd");
                flag = filter.Flag == null ? "" : filter.Flag;
                locId = filter.LocationId == null ? "" : filter.LocationId;
                Insu = filter.InsuCmp == null ? "" : filter.InsuCmp;
                modelName = filter.model == null ? "" : filter.model;
                appointmentFromDate = filter.fromDate == null ? "" : Convert.ToDateTime(filter.fromDate.ToString()).ToString("yyyy-MM-dd");
                appointmentToDate = filter.toDate == null ? "" : Convert.ToDateTime(filter.toDate.ToString()).ToString("yyyy-MM-dd");
                callFromDate = filter.fromDate == null ? "" : Convert.ToDateTime(filter.fromDate.ToString()).ToString("yyyy-MM-dd");
                callToDate = filter.toDate == null ? "" : Convert.ToDateTime(filter.toDate.ToString()).ToString("yyyy-MM-dd");
                appointmentType = filter.AppointmentType == null ? "" : filter.AppointmentType;
                reasons = filter.reasons == null ? "" : filter.reasons;
                attempts = filter.attempts == null ? "" : filter.attempts;
                isEditedDate = filter.DueDateEdited == null ? "" : filter.DueDateEdited;
               


                //  if ((campId != 0 || tagIId != 0 || duetypeId != 0 || locId != 0 || modelIds != 0 || fromLSDDate != "" || toLSDDate != "" || fromDateNew != "" || toDateNew != "") && (filter.followUP_From_date == null && filter.followUP_To_Date == null && filter.Booked_From_date == null && filter.Booked_To_Date == null && filter.ServiceBooked_type == null && filter.BookingStatus == null && filter.LastDispostion == null && filter.lastcall_From_date == null && filter.lastcall_To_Date == null && filter.reasons == null && filter.droppedCount == null && filter.visit_Type == null))
                {
                    try
                    {
                        using (var db = new AutoSherDBContext())
                        {
                            string str = @"CALL insurance_filtered_counts(@inwyzuser_id,@incamp_id,@pfrmdate,@ptodate,@inflag,@inloc,@inrentype,@aptfrmdate,@apttodate,@apttype,@inreasons,@incaldatefrm,@incaldateto,@inattempts,@infollowdatefrm,@infollowdateto,@isEditedDate );";

                            MySqlParameter[] sqlParameter = new MySqlParameter[]
                            {
                        new MySqlParameter("inwyzuser_id", UserId),
                        new MySqlParameter("incamp_id", camp),
                        new MySqlParameter("pfrmdate", ExpiryfromDate),
                        new MySqlParameter("ptodate", ExpirytoDate),
                        new MySqlParameter("inflag", flag),
                        new MySqlParameter("inloc", locId),
                        new MySqlParameter("inrentype", ""),
                        new MySqlParameter("aptfrmdate", appointmentFromDate),
                        new MySqlParameter("apttodate", appointmentToDate),
                        new MySqlParameter("apttype", appointmentType),
                        new MySqlParameter("inreasons", reasons),
                        new MySqlParameter("incaldatefrm", callFromDate),
                        new MySqlParameter("incaldateto", callToDate),
                        new MySqlParameter("inattempts",attempts),
                        new MySqlParameter("infollowdatefrm", ""),
                        new MySqlParameter("infollowdateto", ""),
                        new MySqlParameter("isEditedDate", isEditedDate)

                        };
                            dispositionLoadInsurances = db.Database.SqlQuery<insurenceFilterCount>(str, sqlParameter).ToList();
                        }
                        return Json(new { success = true, filterls = dispositionLoadInsurances });
                    }
                    catch (Exception ex)
                    {
                        string counterexception;
                        if (ex.Message.Contains("inner exception"))
                        {
                            if (ex.InnerException.Message.Contains("inner exception"))
                            {
                                counterexception = ex.InnerException.InnerException.Message;
                            }
                            else
                            {
                                counterexception = ex.InnerException.Message;
                            }

                        }
                        else
                        {
                            counterexception = ex.Message;
                        }

                        return Json(new { success = false, error = counterexception });
                    }
                }
            }
            return Json(new { success = false, filterls = dispositionLoadInsurances });
        }
        #endregion
    }
}