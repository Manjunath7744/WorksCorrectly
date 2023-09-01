using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoSherpa_project.Models;
using MySql.Data.MySqlClient;
using AutoSherpa_project.Models.ViewModels;
using PagedList;
using Newtonsoft;
using System.Data.Entity;
using Newtonsoft.Json;

namespace AutoSherpa_project.Controllers
{
    [AuthorizeFilter]
    public class CREServiceLogController : Controller
    {
        // GET: ServiceLog

        [ActionName("Service"), HttpGet]
        [AuthorizeFilter]
        public ActionResult CallLog()
        {
            ServiceLogViewModel serviceLogVM = new ServiceLogViewModel();
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

                    var ddlcampaignList = db.campaigns.Where(m =>( m.campaignType == "Campaign" || m.campaignType == "Service Reminder" ||  m.campaignType == "Forecast") && m.isactive == true).Select(model => new { id = model.id, CampaignName = model.campaignName }).ToList();

                    //var ddlcampRemainder= db.campaigns.Where(m => m.campaignType == "Service Reminder" && m.campaignType == "Forecast" && m.isactive == true).Select(model => new { TaggingCamp = model.campaignName }).ToList();
                    var ddlreason = db.calldispositiondatas.Where(m => m.mainDispositionId == 5).ToList();
                    var ddlserviceType = db.servicetypes.Where(m => m.isActive).ToList();
                    var ServiceBookedList = db.servicebookeds.Where(r => r.serviceBookedType!= null).Select(r => r.serviceBookedType).Distinct().ToList();
                    var servicebookeds = db.servicetypes.Where(r => ServiceBookedList.Contains(r.serviceTypeName)).Select(m => new { id = m.id, name = m.serviceTypeName }).ToList().OrderBy(m=>m.name);
                   
                    
                    var ddlNewListDispo = db.calldispositiondatas.Where(u => u.dispositionId == 6 || u.dispositionId == 7 || u.dispositionId == 8 || u.dispositionId == 9 || u.dispositionId == 10 || u.dispositionId == 43).ToList();
                    var ddlWorkshop = db.workshops.Select(model => new { workshopId = model.id, workshopName = model.workshopName }).OrderBy(m => m.workshopName).ToList();
                    var ddltagging = db.campaigns.Where(m => m.campaignType == "TaggingSMR" && m.isactive == true && m.campaignName != "ISP" && m.campaignName != "ISP/MCP/EW" && m.campaignName != "ISP/MCP" && m.campaignName != "ISP/EW").Select(model => new { id = model.id, TaggingCamp = model.campaignName }).ToList();
                    // var models = db.modelslists.Select(m => new { @id = m.id, model = m.model }).ToList();

                    ViewBag.ddltagging = ddltagging;
                    ViewBag.ddlcampaignList = ddlcampaignList;
                    //ViewBag.ddlcampRemainder = ddlcampRemainder;
                    ViewBag.ddlreason = ddlreason;
                    ViewBag.ddlserviceType = ddlserviceType;
                    ViewBag.bookedService = servicebookeds;
                    ViewBag.ddlNewListDispo = ddlNewListDispo;
                    ViewBag.ddlWorkshop = ddlWorkshop;
                    ViewBag.ddlreason = ddlreason;
                    // ViewBag.models = models;

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

                if(ex.StackTrace.Contains(":"))
                {
                    exception += ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)];
                }

                TempData["Exceptions"] = exception;
                TempData["ControllerName"] = "Service";

                return RedirectToAction("LogOff", "Home");
            }
            return View(serviceLogVM);
        }
        public ActionResult loadDashCounts()
        {
            int FreeCalls, pendingFollow, overDue, totalBooking, totalContacts, NonContacts, totalCalls, totalAttemptedPerDay;
            try
            {
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                using (var db = new AutoSherDBContext())
                {
                    FreeCalls = db.Database.SqlQuery<int>("call pending_calls_crewise(@wyzUserId)", new MySqlParameter("@wyzUserId", UserId)).FirstOrDefault();
                    pendingFollow = db.Database.SqlQuery<int>("call totalFollowupCountCRE(@wyzUserId)", new MySqlParameter("@wyzUserId", UserId)).FirstOrDefault();
                    NonContacts = db.Database.SqlQuery<int>("call totalNoncontactsCountCRE(@wyzUserId)", new MySqlParameter("@wyzUserId", UserId)).FirstOrDefault();
                    overDue = db.Database.SqlQuery<int>("call totaloverDueCountCRE(@wyzUserId)", new MySqlParameter("@wyzUserId", UserId)).FirstOrDefault();
                    totalBooking = db.Database.SqlQuery<int>("call totalBookingPerday(@wyzUserId)", new MySqlParameter("@wyzUserId", UserId)).FirstOrDefault();
                    totalContacts = db.Database.SqlQuery<int>("call totalContactsPerDayCRE(@wyzUserId)", new MySqlParameter("@wyzUserId", UserId)).FirstOrDefault();
                    totalCalls = db.Database.SqlQuery<int>("call totalCallsCountCRE(@wyzUserId)", new MySqlParameter("@wyzUserId", UserId)).FirstOrDefault();
                    totalAttemptedPerDay = db.Database.SqlQuery<int>("call totalNonContactsAttemptedPerDayCRE(@wyzUserId)", new MySqlParameter("@wyzUserId", UserId)).FirstOrDefault();
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

            return Json(new { success = true, FreeCalls, pendingFollow, overDue, totalBooking, totalContacts, NonContacts, totalCalls, totalAttemptedPerDay });
        }

        
        //Ajax-Tab Call->Schedule
        public ActionResult GetBucketData(string serviceData)
        {
            int fromIndex = Convert.ToInt32(Request["start"]);
            int toIndex = Convert.ToInt32(Request["length"]);
            string searchPattern = Request["search[value]"];
            string workshopId, fromDateNew, toDateNew, fromLSDDate, toLSDDate, modelId, tag, serviceTypeName, campaignName,IncomingCallType, orderFilter;
            string bookingstatus, lastDispo, followupfromdate, followuptodate, bookedfromdate, bookedtodate, bookedservicetype, lastcallfromdate, lastcalltodate, reasons, dropCount,  FromSaledate , toSalesDate, lastvisittype, typeOFSB = "";

            List<serviceBuckets> serviceCallLogs = null;
            string exception = "";

            //Find Order Column
            var insortfilter = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortOrder = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            orderFilter = insortfilter + "_" + sortOrder;

            int UserId = Convert.ToInt32(Session["UserId"].ToString());

            ServiceLogBucketFilter filter = new ServiceLogBucketFilter();
            if (serviceData != null)
            {
                filter = JsonConvert.DeserializeObject<ServiceLogBucketFilter>(serviceData);
            }

            int totalCount = 0;
            long patternCount = 0;

            if (searchPattern != "")
            {
                filter.isFiltered = true;
            }
            /** Global Filters**/
            campaignName = filter.Campaign == null || filter.Campaign == "" ? "0" : filter.Campaign;
            serviceTypeName = filter.Service_type == null || filter.Service_type == "" ? "0" : filter.Service_type;
            tag = filter.Tagging == null || filter.Tagging == "" ? "0" : filter.Tagging;

            long campId = Convert.ToInt64(campaignName);
            long tagIId = Convert.ToInt64(tag);
            long duetypeId = Convert.ToInt64(serviceTypeName);

            workshopId = filter.workshop_Id == null ? "" : filter.workshop_Id;
            modelId = filter.modelId == null ? "" : filter.modelId;
            fromLSDDate = filter.LSD_From_Date == null ? "" : Convert.ToDateTime(filter.LSD_From_Date.ToString()).ToString("yyyy-MM-dd");
            toLSDDate = filter.LSD_To_Date == null ? "" : Convert.ToDateTime(filter.LSD_To_Date.ToString()).ToString("yyyy-MM-dd");
            fromDateNew = filter.From_date == null ? "" : Convert.ToDateTime(filter.From_date.ToString()).ToString("yyyy-MM-dd");
            toDateNew = filter.To_Date == null ? "" : Convert.ToDateTime(filter.To_Date.ToString()).ToString("yyyy-MM-dd");
            /** End Global Filters **/

            /** Local Filters **/
            
            bookingstatus = filter.BookingStatus == null || filter.BookingStatus == "" ? "0" : filter.BookingStatus;
            long bookstatusId = Convert.ToInt64(bookingstatus);
            followupfromdate = filter.followUP_From_date == null ? "" : Convert.ToDateTime(filter.followUP_From_date.ToString()).ToString("yyyy-MM-dd");
            followuptodate = filter.followUP_To_Date == null ? "" : Convert.ToDateTime(filter.followUP_To_Date.ToString()).ToString("yyyy-MM-dd");
            bookedfromdate = filter.Booked_From_date == null ? "" : Convert.ToDateTime(filter.Booked_From_date.ToString()).ToString("yyyy-MM-dd");
            bookedtodate = filter.Booked_To_Date == null ? "" : Convert.ToDateTime(filter.Booked_To_Date.ToString()).ToString("yyyy-MM-dd");
            lastcallfromdate = filter.lastcall_From_date == null ? "" : Convert.ToDateTime(filter.lastcall_From_date.ToString()).ToString("yyyy-MM-dd");
            lastcalltodate = filter.lastcall_To_Date == null ? "" : Convert.ToDateTime(filter.lastcall_To_Date.ToString()).ToString("yyyy-MM-dd");
            bookedservicetype = filter.ServiceBooked_type == null ? "" : filter.ServiceBooked_type;
            lastDispo = filter.LastDispostion == null || filter.LastDispostion == "" ? "0" : filter.LastDispostion;
            long lastdispoId = Convert.ToInt64(lastDispo);
            reasons = filter.reasons == null ? "" : filter.reasons;
            dropCount = filter.droppedCount == null || filter.droppedCount == "" ? "" : filter.droppedCount;
            lastvisittype = filter.visit_Type == null ? "" : filter.visit_Type;
            IncomingCallType = filter.IncomingCallType == null ? "" : filter.IncomingCallType;
            FromSaledate= filter.From_Saledate == null ? "" : Convert.ToDateTime(filter.From_Saledate.ToString()).ToString("yyyy-MM-dd");
            toSalesDate = filter.To_SaleDate == null ? "" : Convert.ToDateTime(filter.To_SaleDate.ToString()).ToString("yyyy-MM-dd");
            /** End Local Filter **/

            using (var db = new AutoSherDBContext())
            {
                try
                {
                    if (filter.getDataFor == 1)//schedule
                    {

                        totalCount = db.Database.SqlQuery<int>("call assigned_intercation_count(@wyzUserId)", new MySqlParameter("@wyzUserId", UserId)).FirstOrDefault();
                        if (fromLSDDate=="" && toLSDDate=="" && fromDateNew=="" && toDateNew=="" &&  campaignName == "0" && serviceTypeName == "0" && tag == "0" && workshopId == "" && modelId == "" && searchPattern == "" && IncomingCallType=="" && FromSaledate=="" && toSalesDate=="")
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
                        serviceCallLogs = getServiceLogs(fromDateNew, toDateNew, campId, duetypeId, searchPattern, UserId, fromIndex, toIndex, fromLSDDate, toLSDDate, workshopId, tagIId, modelId, IncomingCallType, FromSaledate, toSalesDate);
                
                        if (filter.isFiltered == true)
                        {
                            patternCount = getServiceLogs(fromDateNew, toDateNew, campId, duetypeId, searchPattern, UserId, 0, totalCount, fromLSDDate, toLSDDate, workshopId, tagIId, modelId, IncomingCallType,FromSaledate, toSalesDate).Count;
                        }
                    }
                    else if (filter.getDataFor == 2)//follow up
                    {
                        totalCount = getPendingServBookedServnotReqCount(UserId, 4, "");
                        if (fromLSDDate == "" && toLSDDate == "" && fromDateNew == "" && toDateNew == "" && campaignName == "0" && serviceTypeName == "0" && tag == "0" && workshopId == "" && modelId == "" && searchPattern == "" && followupfromdate=="" && followuptodate==""&& FromSaledate == "" && toSalesDate == "")
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
                        serviceCallLogs = getPendingServBookedServnotReq(fromDateNew, toDateNew, campId, duetypeId, searchPattern, UserId, 4, fromIndex, toIndex, 0, "", fromLSDDate, toLSDDate, tagIId, workshopId, modelId, followupfromdate, followuptodate, bookedservicetype, bookstatusId, lastdispoId, lastcallfromdate, lastcalltodate, orderFilter, FromSaledate, toSalesDate);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getPendingServBookedServnotReq(fromDateNew, toDateNew, campId, duetypeId, searchPattern, UserId, 4, 0, totalCount, 0, "", fromLSDDate, toLSDDate, tagIId, workshopId, modelId, followupfromdate, followuptodate, bookedservicetype, bookstatusId, lastdispoId, lastcallfromdate, lastcalltodate, orderFilter, FromSaledate, toSalesDate).Count;

                        }
                    }
                    else if (filter.getDataFor == 3)//book my service
                    {
                        totalCount = getPendingServBookedServnotReqCount(UserId, 3, "");
                        if (fromLSDDate == "" && toLSDDate == "" && fromDateNew == "" && toDateNew == "" && campaignName == "0" && serviceTypeName == "0" && tag == "0" && workshopId == "" && modelId == "" && searchPattern == "" && bookedfromdate == "" && bookedtodate == "" && lastDispo=="0" && bookingstatus=="0" && bookedservicetype=="" && FromSaledate == "" && toSalesDate == "")
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

                        serviceCallLogs = getPendingServBookedServnotReq(fromDateNew, toDateNew, campId, duetypeId, searchPattern, UserId, 3, fromIndex, toIndex, 0, "", fromLSDDate, toLSDDate, tagIId, workshopId, modelId, bookedfromdate, bookedtodate, bookedservicetype, bookstatusId, lastdispoId, lastcallfromdate, lastcalltodate, orderFilter, FromSaledate, toSalesDate);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getPendingServBookedServnotReq(fromDateNew, toDateNew, campId, duetypeId, searchPattern, UserId, 3, 0, totalCount, 0, "", fromLSDDate, toLSDDate, tagIId, workshopId, modelId, bookedfromdate, bookedtodate, bookedservicetype, bookstatusId, lastdispoId, lastcallfromdate, lastcalltodate, orderFilter, FromSaledate, toSalesDate).Count;

                        }
                    }
                    else if (filter.getDataFor == 4)//service not required
                    {
                        totalCount = getPendingServBookedServnotReqCount(UserId, 5, "");
                        if (fromLSDDate == "" && toLSDDate == "" && fromDateNew == "" && toDateNew == "" && campaignName == "0" && serviceTypeName == "0" && tag == "0" && workshopId == "" && modelId == "" && searchPattern == "" && lastcallfromdate == "" && lastcalltodate == "" && reasons == "" && FromSaledate == "" && toSalesDate == "")
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
                        serviceCallLogs = getPendingServBookedServnotReq(fromDateNew, toDateNew, campId, duetypeId, searchPattern, UserId, 5, fromIndex, toIndex, 0, reasons, fromLSDDate, toLSDDate, tagIId, workshopId, modelId, bookedfromdate, bookedtodate, bookedservicetype, bookstatusId, lastdispoId, lastcallfromdate, lastcalltodate, orderFilter, FromSaledate, toSalesDate);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getPendingServBookedServnotReq(fromDateNew, toDateNew, campId, duetypeId, searchPattern, UserId, 5, 0, totalCount, 0, reasons, fromLSDDate, toLSDDate, tagIId, workshopId, modelId, bookedfromdate, bookedtodate, bookedservicetype, bookstatusId, lastdispoId, lastcallfromdate, lastcalltodate, orderFilter, FromSaledate, toSalesDate).Count;

                        }
                    }
                    else if (filter.getDataFor == 5)//noncontacts
                    {

                        totalCount = db.Database.SqlQuery<int>("call NonContactsCount(@wyzUserId)", new MySqlParameter("@wyzUserId", UserId)).FirstOrDefault();
                        if (fromLSDDate == "" && toLSDDate == "" && fromDateNew == "" && toDateNew == "" && campaignName == "0" && serviceTypeName == "0" && tag == "0" && workshopId == "" && modelId == "" && searchPattern == "" && lastcallfromdate == "" && lastcalltodate == "" && lastDispo == "0" && dropCount=="" && FromSaledate == "" && toSalesDate == "")
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

                        serviceCallLogs = getNonContacts(fromDateNew, toDateNew, campId, lastdispoId, dropCount, searchPattern, UserId, fromIndex, toIndex, fromLSDDate, toLSDDate, tagIId, workshopId, duetypeId, modelId, lastcallfromdate, lastcalltodate, FromSaledate, toSalesDate);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getNonContacts(fromDateNew, toDateNew, campId, lastdispoId, dropCount, searchPattern, UserId, 0, totalCount, fromLSDDate, toLSDDate, tagIId, workshopId, duetypeId, modelId, lastcallfromdate, lastcalltodate, FromSaledate, toSalesDate).Count;

                        }

                    }
                    else if (filter.getDataFor == 6)//sbn-1
                    {

                        totalCount = getNthN1DayCount(1, UserId, "", "", "", "", "");
                        if (fromLSDDate == "" && toLSDDate == "" && fromDateNew == "" && toDateNew == "" && campaignName == "0" && serviceTypeName == "0" && tag == "0" && workshopId == "" && modelId == "" && searchPattern == "" && lastcallfromdate == "" && lastcalltodate == "" && bookedservicetype == "" && bookingstatus == "0" && FromSaledate == "" && toSalesDate == "")
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

                        serviceCallLogs = getNthN1Day(1, UserId, workshopId, bookedservicetype, duetypeId, typeOFSB, searchPattern, fromIndex, toIndex, fromLSDDate, toLSDDate, tagIId, modelId, campId, lastcallfromdate, lastcalltodate, bookstatusId, fromDateNew, toDateNew, FromSaledate, toSalesDate);
                        if (filter.isFiltered)
                        {
                            patternCount = getNthN1Day(1, UserId, workshopId, bookedservicetype, duetypeId, typeOFSB, searchPattern, 0, totalCount, fromLSDDate, toLSDDate, tagIId, modelId, campId, lastcallfromdate, lastcalltodate, bookstatusId, fromDateNew, toDateNew, FromSaledate, toSalesDate).Count;

                        }
                    }
                    else if (filter.getDataFor == 7)//sbNth
                    {
                        if (fromLSDDate == "" && toLSDDate == "" && fromDateNew == "" && toDateNew == "" && campaignName == "0" && serviceTypeName == "0" && tag == "0" && workshopId == "" && modelId == "" && searchPattern == "" && lastcallfromdate == "" && lastcalltodate == "" && bookedservicetype == "" && bookingstatus == "0" && FromSaledate == "" && toSalesDate == "")
                        {
                            filter.isFiltered = false;
                        }
                        totalCount = getNthN1DayCount(2, UserId, "", "", "", "", "");
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }

                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }
                        serviceCallLogs = getNthN1Day(2, UserId, workshopId, bookedservicetype, duetypeId, typeOFSB, searchPattern, fromIndex, toIndex, fromLSDDate, toLSDDate, tagIId, modelId, campId, lastcallfromdate, lastcalltodate, bookstatusId, fromDateNew, toDateNew, FromSaledate, toSalesDate);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getNthN1Day(1, UserId, workshopId, bookedservicetype, duetypeId, typeOFSB, searchPattern, 0, totalCount, fromLSDDate, toLSDDate, tagIId, modelId, campId, lastcallfromdate, lastcalltodate, bookstatusId, fromDateNew, toDateNew, FromSaledate, toSalesDate).Count;

                        }
                    }
                    else if (filter.getDataFor == 8)//non-sf-pms
                    {


                        totalCount = db.Database.SqlQuery<int>("call nonFS_nonPMS_vehiclereportedListsCount(@instartdate,@inenddate,@pattern,@wyzuserid)",
                            new MySqlParameter[] { new MySqlParameter("@instartdate", ""), new MySqlParameter("@inenddate", ""), new MySqlParameter("@pattern", ""), new MySqlParameter("@wyzuserid", UserId) }).FirstOrDefault();
                        if (fromLSDDate == "" && toLSDDate == "" && fromDateNew == "" && toDateNew == "" && campaignName == "0" && serviceTypeName == "0" && tag == "0" && workshopId == "" && modelId == "" && searchPattern == "" && FromSaledate == "" && toSalesDate == "")
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
                        serviceCallLogs = getNonFS_PMS(fromLSDDate, toLSDDate, searchPattern, UserId, fromIndex, toIndex, modelId, campId, tagIId, duetypeId, workshopId, lastvisittype, fromDateNew, toDateNew, FromSaledate, toSalesDate);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getNonFS_PMS(fromLSDDate, toLSDDate, searchPattern, UserId, 0, totalCount, modelId, campId, tagIId, duetypeId, workshopId, lastvisittype, fromDateNew, toDateNew, FromSaledate, toSalesDate).Count;

                        }
                    }
                    else if (filter.getDataFor == 9)//future followup
                    {
                        totalCount = db.Database.SqlQuery<int>("call serviceFutureFollowupCount(@wyzUserId)", new MySqlParameter("@wyzUserId", UserId)).FirstOrDefault();
                        if (fromLSDDate == "" && toLSDDate == "" && fromDateNew == "" && toDateNew == "" && campaignName == "0" && serviceTypeName == "0" && tag == "0" && workshopId == "" && modelId == "" && searchPattern == "" && followupfromdate == "" && followuptodate == "" && FromSaledate == "" && toSalesDate == "")
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
                        serviceCallLogs = getFuturefollowup(followupfromdate, followuptodate, campId, duetypeId, searchPattern, UserId, fromIndex, toIndex, fromLSDDate, toLSDDate, tagIId, workshopId, modelId, fromDateNew, toDateNew, FromSaledate, toSalesDate);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getFuturefollowup(followupfromdate, followuptodate, campId, duetypeId, searchPattern, UserId, 0, totalCount, fromLSDDate, toLSDDate, tagIId, workshopId, modelId, fromDateNew, toDateNew, FromSaledate, toSalesDate).Count;

                        }
                    }
                    else if (filter.getDataFor == 10)
                    {

                        totalCount = db.Database.SqlQuery<int>("call otherSMRCount(@wyzUserId)", new MySqlParameter("@wyzUserId", UserId)).FirstOrDefault();

                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }

                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }

                        serviceCallLogs = getOthersData(duetypeId, workshopId, tagIId, fromDateNew, toDateNew, campId, typeOFSB, dropCount, searchPattern, UserId, fromIndex, toIndex, fromLSDDate, toLSDDate, modelId, FromSaledate, toSalesDate);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getOthersData(duetypeId, workshopId, tagIId, fromDateNew, toDateNew, campId, typeOFSB, dropCount, searchPattern, UserId, 0, totalCount, fromLSDDate, toLSDDate, modelId, FromSaledate, toSalesDate).Count;
                        }
                    }
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
            if (serviceCallLogs != null)
            {
                if (filter.isFiltered == true)
                {
                    var JsonData = Json(new { data = serviceCallLogs, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = patternCount, exception = exception }, JsonRequestBehavior.AllowGet);
                    JsonData.MaxJsonLength = int.MaxValue;
                    return JsonData;
                }
                else if (filter.isFiltered == false)
                {
                    var JsonData = Json(new { data = serviceCallLogs, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount, exception = exception }, JsonRequestBehavior.AllowGet);
                    JsonData.MaxJsonLength = int.MaxValue;
                    return JsonData;
                }
            }
            return Json(new { data = "", draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0, exception = exception }, JsonRequestBehavior.AllowGet);
        }

        #region Fresh Bucket
        public List<serviceBuckets> getServiceLogs(string fromDateNew, string toDateNew, long campaignName, long serviceTypeName, string pattern, long id, long fromIndex, long toIndex, string fromLSDDate, string toLSDDate, string workshopId, long tag, string modelId,string IncomingCallType,string FromSaledate,string ToSaledate)
        {
            List<serviceBuckets> serviceRemainers = null;
            long locId = workshopId == "" ? 0 : Convert.ToInt64(workshopId);

            using (var db = new AutoSherDBContext())
            {
                if(Session["DealerCode"].ToString()== "ADVAITHHYUNDAI" || Session["DealerCode"].ToString() == "POPULARHYUNDAI")
                {
                    string str = @"call ServiceReminderwithSearch(@instartdate,@inenddate,@incampaignname,@serviceTypeName,@pattern,@wyzuserid,@start_with,@length,@inlsdstartdate,@inlsdenddate,@intag,@inloc,@inmodelId,@incomingcalltype);";

                    MySqlParameter[] sqlParameter = new MySqlParameter[]
                    {
                        new MySqlParameter("@instartdate", fromDateNew),
                        new MySqlParameter("@inenddate", toDateNew),
                        new MySqlParameter("@incampaignname", campaignName),
                        new MySqlParameter("@serviceTypeName", serviceTypeName),
                        new MySqlParameter("@pattern", pattern),
                        new MySqlParameter("@wyzuserid", id),
                        new MySqlParameter("@start_with", fromIndex),
                        new MySqlParameter("@length", toIndex),
                        new MySqlParameter("@inlsdstartdate", fromLSDDate),
                        new MySqlParameter("@inlsdenddate", toLSDDate),
                        new MySqlParameter("@intag", tag),
                        new MySqlParameter("@inloc", locId),
                        new MySqlParameter("@inmodelId", modelId),
                        new MySqlParameter("@incomingcalltype", IncomingCallType),
                    };
                    serviceRemainers = db.Database.SqlQuery<serviceBuckets>(str, sqlParameter).ToList();
                }
                else if(Session["DealerCode"].ToString() == "KATARIA")
                {
                    string str = @"call ServiceReminderwithSearch(@instartdate,@inenddate,@incampaignname,@serviceTypeName,@pattern,@wyzuserid,@start_with,@length,@inlsdstartdate,@inlsdenddate,@intag,@inloc,@inmodelId,@infromSaledate,@intoSaledate);";

                    MySqlParameter[] sqlParameter = new MySqlParameter[]
                    {
                        new MySqlParameter("@instartdate", fromDateNew),
                        new MySqlParameter("@inenddate", toDateNew),
                        new MySqlParameter("@incampaignname", campaignName),
                        new MySqlParameter("@serviceTypeName", serviceTypeName),
                        new MySqlParameter("@pattern", pattern),
                        new MySqlParameter("@wyzuserid", id),
                        new MySqlParameter("@start_with", fromIndex),
                        new MySqlParameter("@length", toIndex),
                        new MySqlParameter("@inlsdstartdate", fromLSDDate),
                        new MySqlParameter("@inlsdenddate", toLSDDate),
                        new MySqlParameter("@intag", tag),
                        new MySqlParameter("@inloc", locId),
                        new MySqlParameter("@inmodelId", modelId),
                        new MySqlParameter("@infromSaledate", FromSaledate),
                        new MySqlParameter("@intoSaledate", ToSaledate)
                    };
                    serviceRemainers = db.Database.SqlQuery<serviceBuckets>(str, sqlParameter).ToList();
                }

                else
                {
                    string str = @"call ServiceReminderwithSearch(@instartdate,@inenddate,@incampaignname,@serviceTypeName,@pattern,@wyzuserid,@start_with,@length,@inlsdstartdate,@inlsdenddate,@intag,@inloc,@inmodelId);";

                    MySqlParameter[] sqlParameter = new MySqlParameter[]
                    {
                        new MySqlParameter("@instartdate", fromDateNew),
                        new MySqlParameter("@inenddate", toDateNew),
                        new MySqlParameter("@incampaignname", campaignName),
                        new MySqlParameter("@serviceTypeName", serviceTypeName),
                        new MySqlParameter("@pattern", pattern),
                        new MySqlParameter("@wyzuserid", id),
                        new MySqlParameter("@start_with", fromIndex),
                        new MySqlParameter("@length", toIndex),
                        new MySqlParameter("@inlsdstartdate", fromLSDDate),
                        new MySqlParameter("@inlsdenddate", toLSDDate),
                        new MySqlParameter("@intag", tag),
                        new MySqlParameter("@inloc", locId),
                        new MySqlParameter("@inmodelId", modelId),
                    };
                    serviceRemainers = db.Database.SqlQuery<serviceBuckets>(str, sqlParameter).ToList();
                }
            }
            return serviceRemainers;
        }

        #endregion

        #region pending foloup,service Booked,Service not required- Bucket-2,Bucket-3,Bucket-4

        public int getPendingServBookedServnotReqCount(long wyzuserId, long dispotype, string reason)
        {
            int totalCount = 0;
            using (var db = new AutoSherDBContext())
            {
                totalCount = db.Database.SqlQuery<int>("call DispositionContactsCount(@wyzUserId,@dispositiontype,@reasons)",
                      new MySqlParameter[] {
                            new MySqlParameter("@wyzUserId", wyzuserId),
                            new MySqlParameter("@dispositiontype",dispotype),
                            new MySqlParameter("@reasons",reason)
                      }).FirstOrDefault();

                return totalCount;

            }

        }

        public List<serviceBuckets> getPendingServBookedServnotReq(string fromDateNew, string toDateNew, long campaignName, long serviceTypeName, string pattern, long id, long typeOfDispo, long fromIndex, long toIndex, int orderDirection, string reasons, string fromLSDDate, string toLSDDate, long tag, string workshopId, string modelId, string followupbookedfromdate, string followupbookedtodate, string bookedservicetype, long bookingstatus, long lastDispo, string lastcallfromdate, string lastcalltodate, string orderFilter, string FromSaledate, string ToSaledate)
        {
            List<serviceBuckets> callLogDispositions = null;
            using (var db = new AutoSherDBContext())
            {
                long locationId = workshopId == "" ? 0 : Convert.ToInt64(workshopId);
                long modelsId = modelId == "" ? 0 : Convert.ToInt64(modelId);

                if (Session["DealerCode"].ToString() == "INDUS")
                {
                    string str = @"CALL 1dispositionFilter(@instartdate,@inenddate,@incampaignname,@induetype,@pattern,@wyzuserid,@dispositiontype,@start_with, @length,@orderDirection,@reasons, @inlsdstartdate, @inlsdenddate, @intag, @inloc,@inmodelId,@infbstartdate,@infbenddate,@inlastcallstartdate,@inlatcallenddate,@bookingstatus,@bookedservicetype,@lastdispo,@insortfilter)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("@instartdate", fromDateNew),
                        new MySqlParameter("@inenddate", toDateNew),
                        new MySqlParameter("@incampaignname", campaignName),
                        new MySqlParameter("@induetype", serviceTypeName),
                        new MySqlParameter("@pattern", pattern),
                        new MySqlParameter("@wyzuserid", id),
                        new MySqlParameter("@dispositiontype", typeOfDispo),
                        new MySqlParameter("@start_with", fromIndex),
                        new MySqlParameter("@length", toIndex),
                        new MySqlParameter("@orderDirection", orderDirection),
                        new MySqlParameter("@reasons", reasons),
                        new MySqlParameter("@inlsdstartdate", fromLSDDate),
                        new MySqlParameter("@inlsdenddate", toLSDDate),
                        new MySqlParameter("@intag", tag),
                        new MySqlParameter("@inloc", locationId),
                        new MySqlParameter("@inmodelId", modelsId),
                        new MySqlParameter("@infbstartdate", followupbookedfromdate),
                        new MySqlParameter("@infbenddate",followupbookedtodate),
                        new MySqlParameter("@inlastcallstartdate",lastcallfromdate),
                        new MySqlParameter("@inlatcallenddate", lastcalltodate),
                        new MySqlParameter("@bookingstatus", bookingstatus),
                        new MySqlParameter("@bookedservicetype", bookedservicetype),
                        new MySqlParameter("@lastdispo", lastDispo),
                        new MySqlParameter("@insortfilter", orderFilter)
                    };

                    callLogDispositions = db.Database.SqlQuery<serviceBuckets>(str, param).ToList();
                }
                else if(Session["DealerCode"].ToString() == "KATARIA")
                {
                    string str = @"CALL 1dispositionFilter(@instartdate,@inenddate,@incampaignname,@induetype,@pattern,@wyzuserid,@dispositiontype,@start_with, @length,@orderDirection,@reasons, @inlsdstartdate, @inlsdenddate, @intag, @inloc,@inmodelId,@infbstartdate,@infbenddate,@inlastcallstartdate,@inlatcallenddate,@bookingstatus,@bookedservicetype,@lastdispo,@infromSaledate,@intoSaledate)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("@instartdate", fromDateNew),
                        new MySqlParameter("@inenddate", toDateNew),
                        new MySqlParameter("@incampaignname", campaignName),
                        new MySqlParameter("@induetype", serviceTypeName),
                        new MySqlParameter("@pattern", pattern),
                        new MySqlParameter("@wyzuserid", id),
                        new MySqlParameter("@dispositiontype", typeOfDispo),
                        new MySqlParameter("@start_with", fromIndex),
                        new MySqlParameter("@length", toIndex),
                        new MySqlParameter("@orderDirection", orderDirection),
                        new MySqlParameter("@reasons", reasons),
                        new MySqlParameter("@inlsdstartdate", fromLSDDate),
                        new MySqlParameter("@inlsdenddate", toLSDDate),
                        new MySqlParameter("@intag", tag),
                        new MySqlParameter("@inloc", locationId),
                        new MySqlParameter("@inmodelId", modelsId),
                        new MySqlParameter("@infbstartdate", followupbookedfromdate),
                        new MySqlParameter("@infbenddate",followupbookedtodate),
                        new MySqlParameter("@inlastcallstartdate",lastcallfromdate),
                        new MySqlParameter("@inlatcallenddate", lastcalltodate),
                        new MySqlParameter("@bookingstatus", bookingstatus),
                        new MySqlParameter("@bookedservicetype", bookedservicetype),
                        new MySqlParameter("@lastdispo", lastDispo),
                        new MySqlParameter("@infromSaledate", FromSaledate),
                        new MySqlParameter("@intoSaledate", ToSaledate)
                    };

                    callLogDispositions = db.Database.SqlQuery<serviceBuckets>(str, param).ToList();
                }
                else 
                {
                    string str = @"CALL 1dispositionFilter(@instartdate,@inenddate,@incampaignname,@induetype,@pattern,@wyzuserid,@dispositiontype,@start_with, @length,@orderDirection,@reasons, @inlsdstartdate, @inlsdenddate, @intag, @inloc,@inmodelId,@infbstartdate,@infbenddate,@inlastcallstartdate,@inlatcallenddate,@bookingstatus,@bookedservicetype,@lastdispo)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("@instartdate", fromDateNew),
                        new MySqlParameter("@inenddate", toDateNew),
                        new MySqlParameter("@incampaignname", campaignName),
                        new MySqlParameter("@induetype", serviceTypeName),
                        new MySqlParameter("@pattern", pattern),
                        new MySqlParameter("@wyzuserid", id),
                        new MySqlParameter("@dispositiontype", typeOfDispo),
                        new MySqlParameter("@start_with", fromIndex),
                        new MySqlParameter("@length", toIndex),
                        new MySqlParameter("@orderDirection", orderDirection),
                        new MySqlParameter("@reasons", reasons),
                        new MySqlParameter("@inlsdstartdate", fromLSDDate),
                        new MySqlParameter("@inlsdenddate", toLSDDate),
                        new MySqlParameter("@intag", tag),
                        new MySqlParameter("@inloc", locationId),
                        new MySqlParameter("@inmodelId", modelsId),
                        new MySqlParameter("@infbstartdate", followupbookedfromdate),
                        new MySqlParameter("@infbenddate",followupbookedtodate),
                        new MySqlParameter("@inlastcallstartdate",lastcallfromdate),
                        new MySqlParameter("@inlatcallenddate", lastcalltodate),
                        new MySqlParameter("@bookingstatus", bookingstatus),
                        new MySqlParameter("@bookedservicetype", bookedservicetype),
                        new MySqlParameter("@lastdispo", lastDispo)
                    };

                    callLogDispositions = db.Database.SqlQuery<serviceBuckets>(str, param).ToList();
                }

            }
            return callLogDispositions;
        }

        #endregion

        #region Non Contacts Filter with all data Non Contacts data fetch function - Bucket-5

        public List<serviceBuckets> getNonContacts(string fromDateNew, string toDateNew,
            long campaignName, long typeOfDispo, string droppedCount, string searchPattern, long id, long fromIndex,
            long toIndex, string fromLSDDate, string toLSDDate, long tag, string workshopId, long induetype, string inmodelId, string lastcallfromdate, string lastcalltodate, string FromSaledate, string ToSaledate)
        {
            List<serviceBuckets> dispositionLoads = null;
            //{
            using (var db = new AutoSherDBContext())
            {

                if (Session["DealerCode"].ToString() == "KATARIA")
                {
                    string str = @"CALL 1NonContactsSearch(@instartdate,@inenddate,@incampaignname,@typeOfDispo,@droppedCount,@pattern,@wyzuserid,@start_with,@length,@inlsdstartdate,@inlsdenddate,@intag,@inloc,@induetype,@inmodelId,@inlastcallstartdate,@inlatcallenddate,@infromSaledate,@intoSaledate)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("@instartdate", fromDateNew),
                        new MySqlParameter("@inenddate", toDateNew),
                        new MySqlParameter("@incampaignname", campaignName),
                        new MySqlParameter("@typeOfDispo", typeOfDispo),
                        new MySqlParameter("@droppedCount", droppedCount),
                        new MySqlParameter("@pattern", searchPattern),
                        new MySqlParameter("@wyzuserid", id),
                        new MySqlParameter("@start_with", fromIndex),
                        new MySqlParameter("@length", toIndex),
                        new MySqlParameter("@inlsdstartdate", fromLSDDate),
                        new MySqlParameter("@inlsdenddate", toLSDDate),
                        new MySqlParameter("@intag", tag),
                        new MySqlParameter("@inloc", workshopId),
                        new MySqlParameter("@induetype", induetype),
                        new MySqlParameter("@inmodelId", inmodelId),
                        new MySqlParameter("@inlastcallstartdate", lastcallfromdate),
                        new MySqlParameter("@inlatcallenddate", lastcalltodate),
                        new MySqlParameter("@infromSaledate", FromSaledate),
                        new MySqlParameter("@intoSaledate", ToSaledate)
                    };

                    dispositionLoads = db.Database.SqlQuery<serviceBuckets>(str, param).ToList();

                }
                else
                {
                    string str = @"CALL 1NonContactsSearch(@instartdate,@inenddate,@incampaignname,@typeOfDispo,@droppedCount,@pattern,@wyzuserid,@start_with,@length,@inlsdstartdate,@inlsdenddate,@intag,@inloc,@induetype,@inmodelId,@inlastcallstartdate,@inlatcallenddate)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("@instartdate", fromDateNew),
                        new MySqlParameter("@inenddate", toDateNew),
                        new MySqlParameter("@incampaignname", campaignName),
                        new MySqlParameter("@typeOfDispo", typeOfDispo),
                        new MySqlParameter("@droppedCount", droppedCount),
                        new MySqlParameter("@pattern", searchPattern),
                        new MySqlParameter("@wyzuserid", id),
                        new MySqlParameter("@start_with", fromIndex),
                        new MySqlParameter("@length", toIndex),
                        new MySqlParameter("@inlsdstartdate", fromLSDDate),
                        new MySqlParameter("@inlsdenddate", toLSDDate),
                        new MySqlParameter("@intag", tag),
                        new MySqlParameter("@inloc", workshopId),
                        new MySqlParameter("@induetype", induetype),
                        new MySqlParameter("@inmodelId", inmodelId),
                        new MySqlParameter("@inlastcallstartdate", lastcallfromdate),
                        new MySqlParameter("@inlatcallenddate", lastcalltodate)
                    };

                    dispositionLoads = db.Database.SqlQuery<serviceBuckets>(str, param).ToList();

                }
            }
            return dispositionLoads;
        }
        #endregion

        #region  //SB-1 Filter with all data SB-1 data fetch function



        public int getNthN1DayCount(long sbday, long inChasers_id, string pattern, string inworkshop, string instatus, string intypeofpickup, string inserviceBookedType)
        {
            int totalCount = 0;
            using (var db = new AutoSherDBContext())
            {
                totalCount = db.Database.SqlQuery<int>("call ServiceBookingPreviousDayCallsCount(@sbday,@inChasers_id,@pattern,@inworkshop,@instatus,@intypeofpickup,@inserviceBookedType)",
                      new MySqlParameter[] {
                            new MySqlParameter("@sbday", sbday),
                            new MySqlParameter("@inChasers_id",inChasers_id),
                            new MySqlParameter("@pattern",pattern),
                            new MySqlParameter("@inworkshop",inworkshop),
                            new MySqlParameter("@instatus",instatus),
                            new MySqlParameter("@intypeofpickup",intypeofpickup),
                            new MySqlParameter("@inserviceBookedType",inserviceBookedType)
                      }).FirstOrDefault();

                return totalCount;

            }

        }

        public List<serviceBuckets> getNthN1Day(long sbday, long id, string workshopId, string bookedservicetype, long duetype, string typeOfSB, string pattern, long fromIndex, long toIndex, string fromLSDDate, string toLSDDate, long tag, string inmodelId, long incampaign, string lastcallfromdate, string lastcalltodate, long bookingStatus, string induefromdate, string induetodate, string FromSaledate, string ToSaledate)
        {
            List<serviceBuckets> dispositionLoads = null;
            long locId = workshopId == "" ? 0 : Convert.ToInt64(workshopId);
            long bookedserviceId = bookedservicetype == "" ? 0 : Convert.ToInt64(bookedservicetype);
            string statusId = bookingStatus == 0 ? "" : bookingStatus.ToString();
            using (var db = new AutoSherDBContext())
            {
                if (Session["DealerCode"].ToString() == "KATARIA")
                {
                    string str = @"CALL 1ServiceBookingPreviousDayCalls(@sbday,@inChasers_id,@inworkshop,@instatus,@inserviceBookedType,@intypeofpickup,@pattern,@startwith,@length,@inlsdstartdate,@inlsdenddate,@intag,@inmodelId,@incampaign,@inlastcallstartdate,@inlatcallenddate,@induetype,@induefromdate,@induetodate,@infromSaledate,@intoSaledate)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("@sbday", sbday),
                        new MySqlParameter("@inChasers_id", id),
                        new MySqlParameter("@inworkshop", locId),
                        new MySqlParameter("@instatus", statusId),
                        new MySqlParameter("@inserviceBookedType", bookedserviceId),
                        new MySqlParameter("@intypeofpickup", typeOfSB),
                        new MySqlParameter("@pattern", pattern),
                        new MySqlParameter("@startwith", fromIndex),
                        new MySqlParameter("@length", toIndex),
                        new MySqlParameter("@inlsdstartdate", fromLSDDate),
                        new MySqlParameter("@inlsdenddate", toLSDDate),
                        new MySqlParameter("@intag", tag),
                        new MySqlParameter("@inmodelId", inmodelId),
                        new MySqlParameter("@incampaign", incampaign),
                        new MySqlParameter("@inlastcallstartdate", lastcallfromdate),
                        new MySqlParameter("@inlatcallenddate", lastcalltodate),
                        new MySqlParameter("@induetype", duetype),
                        new MySqlParameter("@induefromdate", induefromdate),
                        new MySqlParameter("@induetodate", induetodate),
                        new MySqlParameter("@infromSaledate", FromSaledate),
                        new MySqlParameter("@intoSaledate", ToSaledate)
                    };

                    dispositionLoads = db.Database.SqlQuery<serviceBuckets>(str, param).ToList();
                }


                else {

                    string str = @"CALL 1ServiceBookingPreviousDayCalls(@sbday,@inChasers_id,@inworkshop,@instatus,@inserviceBookedType,@intypeofpickup,@pattern,@startwith,@length,@inlsdstartdate,@inlsdenddate,@intag,@inmodelId,@incampaign,@inlastcallstartdate,@inlatcallenddate,@induetype,@induefromdate,@induetodate)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("@sbday", sbday),
                        new MySqlParameter("@inChasers_id", id),
                        new MySqlParameter("@inworkshop", locId),
                        new MySqlParameter("@instatus", statusId),
                        new MySqlParameter("@inserviceBookedType", bookedserviceId),
                        new MySqlParameter("@intypeofpickup", typeOfSB),
                        new MySqlParameter("@pattern", pattern),
                        new MySqlParameter("@startwith", fromIndex),
                        new MySqlParameter("@length", toIndex),
                        new MySqlParameter("@inlsdstartdate", fromLSDDate),
                        new MySqlParameter("@inlsdenddate", toLSDDate),
                        new MySqlParameter("@intag", tag),
                        new MySqlParameter("@inmodelId", inmodelId),
                        new MySqlParameter("@incampaign", incampaign),
                        new MySqlParameter("@inlastcallstartdate", lastcallfromdate),
                        new MySqlParameter("@inlatcallenddate", lastcalltodate),
                        new MySqlParameter("@induetype", duetype),
                        new MySqlParameter("@induefromdate", induefromdate),
                        new MySqlParameter("@induetodate", induetodate)
                    };

                    dispositionLoads = db.Database.SqlQuery<serviceBuckets>(str, param).ToList();
                }
            }
            return dispositionLoads;
        }
        #endregion

        #region Non-FS Filter with all data SBNth data fetch function
        public List<serviceBuckets> getNonFS_PMS(string fromDateNew, string toDateNew, string searchPattern, long id, long fromIndex, long toIndex, string inmodelId, long incampaignname, long intag, long induetype, string inworkshop, string inlastvisittype, string induefromdate, string induetodate, string FromSaledate, string ToSaledate)
        {
            List<serviceBuckets> dispositionLoads = null;
            long locId = inworkshop == "" ? 0 : Convert.ToInt64(inworkshop);
            using (var db = new AutoSherDBContext())
            {
                if(Session["DealerCode"].ToString() == "KATARIA")
                { 
                string str = @"CALL 1nonFS_nonPMS_vehiclereportedLists(@instartdate,@inenddate,@pattern,@wyzuserid,@startwith,@length,@inmodelId,@incampaignname,@intag,@induetype,@inworkshop,@inlastvisittype,@induefromdate,@induetodate,@infromSaledate,@intoSaledate)";

                MySqlParameter[] param = new MySqlParameter[]
                {
                       new MySqlParameter("@instartdate", fromDateNew),
                       new MySqlParameter("@inenddate", toDateNew),
                       new MySqlParameter("@pattern", searchPattern),
                       new MySqlParameter("@wyzuserid", id),
                       new MySqlParameter("@startwith", fromIndex),
                       new MySqlParameter("@length", toIndex),
                       new MySqlParameter("@inmodelId", inmodelId),
                       new MySqlParameter("@incampaignname", incampaignname),
                       new MySqlParameter("@intag", intag),
                       new MySqlParameter("@induetype", induetype),
                       new MySqlParameter("@inworkshop", locId),
                       new MySqlParameter("@inlastvisittype", inlastvisittype),
                       new MySqlParameter("@induefromdate", induefromdate),
                       new MySqlParameter("@induetodate", induetodate),
                       new MySqlParameter("@infromSaledate", FromSaledate),
                        new MySqlParameter("@intoSaledate", ToSaledate)
                };

                dispositionLoads = db.Database.SqlQuery<serviceBuckets>(str, param).ToList();
            }
                else
                {
                    string str = @"CALL 1nonFS_nonPMS_vehiclereportedLists(@instartdate,@inenddate,@pattern,@wyzuserid,@startwith,@length,@inmodelId,@incampaignname,@intag,@induetype,@inworkshop,@inlastvisittype,@induefromdate,@induetodate)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                       new MySqlParameter("@instartdate", fromDateNew),
                       new MySqlParameter("@inenddate", toDateNew),
                       new MySqlParameter("@pattern", searchPattern),
                       new MySqlParameter("@wyzuserid", id),
                       new MySqlParameter("@startwith", fromIndex),
                       new MySqlParameter("@length", toIndex),
                       new MySqlParameter("@inmodelId", inmodelId),
                       new MySqlParameter("@incampaignname", incampaignname),
                       new MySqlParameter("@intag", intag),
                       new MySqlParameter("@induetype", induetype),
                       new MySqlParameter("@inworkshop", locId),
                       new MySqlParameter("@inlastvisittype", inlastvisittype),
                       new MySqlParameter("@induefromdate", induefromdate),
                       new MySqlParameter("@induetodate", induetodate)
                    };

                    dispositionLoads = db.Database.SqlQuery<serviceBuckets>(str, param).ToList();
                }


            }

            return dispositionLoads;
        }
        #endregion

        #region Futute Follow-ups Filter with all data Futute Follow-ups data fetch function
        public List<serviceBuckets> getFuturefollowup(string followupstartdate, string followupenddate, long campaignName, long serviceTypeName, string pattern, long id, long fromIndex, long toIndex, string fromLSDDate, string toLSDDate, long tag, string workshopId, string inmodelId, string induefromdate, string induetodate, string FromSaledate, string ToSaledate)
        {
            List<serviceBuckets> dispositionLoads = null;
            long locId = workshopId == "" ? 0 : Convert.ToInt64(workshopId);
            using (var db = new AutoSherDBContext())
            {
                if (Session["DealerCode"].ToString() == "KATARIA")

                { 
                    string str = @"CALL 1serviceFutureFollowup(@instartdate,@inenddate,@incampaignname,@induetype,@pattern,@wyzuserid,@start_with,@length,@inlsdstartdate,@inlsdenddate,@intag,@inloc,@inmodelId,@induefromdate,@induetodate,@infromSaledate,@intoSaledate)";

                MySqlParameter[] param = new MySqlParameter[]
                {
                       new MySqlParameter("@instartdate", followupstartdate),
                       new MySqlParameter("@inenddate", followupenddate),
                       new MySqlParameter("@incampaignname", campaignName),
                       new MySqlParameter("@induetype", serviceTypeName),
                       new MySqlParameter("@pattern", pattern),
                       new MySqlParameter("@wyzuserid", id),
                       new MySqlParameter("@start_with", fromIndex),
                       new MySqlParameter("@length", toIndex),
                       new MySqlParameter("@inlsdstartdate", fromLSDDate),
                       new MySqlParameter("@inlsdenddate", toLSDDate),
                       new MySqlParameter("@intag", tag),
                       new MySqlParameter("@inloc", locId),
                       new MySqlParameter("@inmodelId", inmodelId),
                       new MySqlParameter("@induefromdate", induefromdate),
                       new MySqlParameter("@induetodate", induetodate),
                       new MySqlParameter("@infromSaledate", FromSaledate),
                        new MySqlParameter("@intoSaledate", ToSaledate)
                };

                dispositionLoads = db.Database.SqlQuery<serviceBuckets>(str, param).ToList();
            }

                else
                {
                    string str = @"CALL 1serviceFutureFollowup(@instartdate,@inenddate,@incampaignname,@induetype,@pattern,@wyzuserid,@start_with,@length,@inlsdstartdate,@inlsdenddate,@intag,@inloc,@inmodelId,@induefromdate,@induetodate)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                       new MySqlParameter("@instartdate", followupstartdate),
                       new MySqlParameter("@inenddate", followupenddate),
                       new MySqlParameter("@incampaignname", campaignName),
                       new MySqlParameter("@induetype", serviceTypeName),
                       new MySqlParameter("@pattern", pattern),
                       new MySqlParameter("@wyzuserid", id),
                       new MySqlParameter("@start_with", fromIndex),
                       new MySqlParameter("@length", toIndex),
                       new MySqlParameter("@inlsdstartdate", fromLSDDate),
                       new MySqlParameter("@inlsdenddate", toLSDDate),
                       new MySqlParameter("@intag", tag),
                       new MySqlParameter("@inloc", locId),
                       new MySqlParameter("@inmodelId", inmodelId),
                       new MySqlParameter("@induefromdate", induefromdate),
                       new MySqlParameter("@induetodate", induetodate)
                    };

                    dispositionLoads = db.Database.SqlQuery<serviceBuckets>(str, param).ToList();
                }
            }
            return dispositionLoads;
        }
        #endregion

        #region other Bucket
        public List<serviceBuckets> getOthersData(long serviceTypeName, string workshopId, long tag, string fromDateNew, string toDateNew,
            long campaignName, string typeOfDispo, string droppedCount, string searchPattern, long id, long fromIndex,
            long toIndex, string fromLSDDate, string toLSDDate, string modelId, string FromSaledate, string ToSaledate)
        {
            List<serviceBuckets> dispositionLoads = new List<serviceBuckets>();
            using (var db = new AutoSherDBContext())
            {
                if (Session["DealerCode"].ToString() == "BRIDGEWAYMOTORS" && serviceTypeName == 0 && campaignName == 0 && tag == 0)
                {
                    string strSMR = @"CALL otherSMRSearch(@induetype, @instartdate,@inenddate,@incampaignname,@typeOfDispo,@droppedCount,@pattern,@wyzuserid, @start_with, @length, @inlsdstartdate, @inlsdenddate, @intag, @inloc, @inmodelId)";
                    MySqlParameter[] param = new MySqlParameter[]
                {
                        new MySqlParameter("induetype", ""),
                        new MySqlParameter("instartdate", fromDateNew),
                        new MySqlParameter("inenddate", toDateNew),
                        new MySqlParameter("incampaignname", ""),
                        new MySqlParameter("typeOfDispo", typeOfDispo),
                        new MySqlParameter("droppedCount", droppedCount),
                        new MySqlParameter("pattern", searchPattern),
                        new MySqlParameter("wyzuserid", id),
                        new MySqlParameter("start_with", fromIndex),
                        new MySqlParameter("length", toIndex),
                        new MySqlParameter("inlsdstartdate", fromLSDDate),
                        new MySqlParameter("inlsdenddate", toLSDDate),
                        new MySqlParameter("intag", ""),
                        new MySqlParameter("inloc", workshopId),
                        new MySqlParameter("inmodelId", modelId)
                };

                    dispositionLoads = db.Database.SqlQuery<serviceBuckets>(strSMR, param).ToList();
                }
                else if (Session["DealerCode"].ToString() == "KATARIA")
                { 

                string str = @"CALL otherSMRSearch(@induetype, @instartdate,@inenddate,@incampaignname,@typeOfDispo,@droppedCount,@pattern,@wyzuserid, @start_with, @length, @inlsdstartdate, @inlsdenddate, @intag, @inloc, @inmodelId,@infromSaledate,@intoSaledate)";

                MySqlParameter[] param = new MySqlParameter[]
                {
                        new MySqlParameter("induetype", serviceTypeName),
                        new MySqlParameter("instartdate", fromDateNew),
                        new MySqlParameter("inenddate", toDateNew),
                        new MySqlParameter("incampaignname", campaignName),
                        new MySqlParameter("typeOfDispo", typeOfDispo),
                        new MySqlParameter("droppedCount", droppedCount),
                        new MySqlParameter("pattern", searchPattern),
                        new MySqlParameter("wyzuserid", id),
                        new MySqlParameter("start_with", fromIndex),
                        new MySqlParameter("length", toIndex),
                        new MySqlParameter("inlsdstartdate", fromLSDDate),
                        new MySqlParameter("inlsdenddate", toLSDDate),
                        new MySqlParameter("intag", tag),
                        new MySqlParameter("inloc", workshopId),
                        new MySqlParameter("inmodelId", modelId),
                         new MySqlParameter("@infromSaledate", FromSaledate),
                        new MySqlParameter("@intoSaledate", ToSaledate)
                };

                dispositionLoads = db.Database.SqlQuery<serviceBuckets>(str, param).ToList();
                }

                else
                {

                    string str = @"CALL otherSMRSearch(@induetype, @instartdate,@inenddate,@incampaignname,@typeOfDispo,@droppedCount,@pattern,@wyzuserid, @start_with, @length, @inlsdstartdate, @inlsdenddate, @intag, @inloc, @inmodelId)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("induetype", serviceTypeName),
                        new MySqlParameter("instartdate", fromDateNew),
                        new MySqlParameter("inenddate", toDateNew),
                        new MySqlParameter("incampaignname", campaignName),
                        new MySqlParameter("typeOfDispo", typeOfDispo),
                        new MySqlParameter("droppedCount", droppedCount),
                        new MySqlParameter("pattern", searchPattern),
                        new MySqlParameter("wyzuserid", id),
                        new MySqlParameter("start_with", fromIndex),
                        new MySqlParameter("length", toIndex),
                        new MySqlParameter("inlsdstartdate", fromLSDDate),
                        new MySqlParameter("inlsdenddate", toLSDDate),
                        new MySqlParameter("intag", tag),
                        new MySqlParameter("inloc", workshopId),
                        new MySqlParameter("inmodelId", modelId)
                    };

                    dispositionLoads = db.Database.SqlQuery<serviceBuckets>(str, param).ToList();
                }
            }

            return dispositionLoads;
        }
        #endregion

        #region Service Filter Count
        public ActionResult getServiceFilterCount(string serviceData)
        {
            List<serviceFilterCount> dispositionLoadInsurances = new List<serviceFilterCount>();

            string exception = "";
            string workshopId, fromDateNew, toDateNew, fromLSDDate, toLSDDate, modelId, tag, serviceTypeName, campaignName;

            int UserId = Convert.ToInt32(Session["UserId"].ToString());

            ServiceLogBucketFilter filter = new ServiceLogBucketFilter();
            if (serviceData != null)
            {
                filter = JsonConvert.DeserializeObject<ServiceLogBucketFilter>(serviceData);
            }

            if (filter.isFiltered == true)
            {
                /** Global Filters**/
                campaignName = filter.Campaign == null || filter.Campaign == "" ? "0" : filter.Campaign;
                serviceTypeName = filter.Service_type == null || filter.Service_type == "" ? "0" : filter.Service_type;
                tag = filter.Tagging == null || filter.Tagging == "" ? "0" : filter.Tagging;

                long campId = Convert.ToInt64(campaignName);
                long tagIId = Convert.ToInt64(tag);
                long duetypeId = Convert.ToInt64(serviceTypeName);

                workshopId = filter.workshop_Id == null || filter.workshop_Id == "" ? "0" : filter.workshop_Id;
                //if(Session["DealerCode"].ToString() == "KATARIA")
                //{
                    string locId = workshopId;
                //}
                //else
                //{
                //    long locId = Convert.ToInt64(workshopId);
                //}
                modelId = filter.modelId == null || filter.modelId == "" ? "0" : filter.modelId;
                long modelIds = Convert.ToInt64(modelId);
                fromLSDDate = filter.LSD_From_Date == null ? "" : Convert.ToDateTime(filter.LSD_From_Date.ToString()).ToString("yyyy-MM-dd");
                toLSDDate = filter.LSD_To_Date == null ? "" : Convert.ToDateTime(filter.LSD_To_Date.ToString()).ToString("yyyy-MM-dd");
                fromDateNew = filter.From_date == null ? "" : Convert.ToDateTime(filter.From_date.ToString()).ToString("yyyy-MM-dd");
                toDateNew = filter.To_Date == null ? "" : Convert.ToDateTime(filter.To_Date.ToString()).ToString("yyyy-MM-dd");
                /** End Global Filters **/

           //    if ((campId != 0 || tagIId != 0 || duetypeId != 0 || locId != 0 || modelIds != 0 || fromLSDDate != "" || toLSDDate != "" || fromDateNew != "" || toDateNew != "") && (filter.followUP_From_date == null && filter.followUP_To_Date == null && filter.Booked_From_date == null && filter.Booked_To_Date == null && filter.ServiceBooked_type == null && filter.BookingStatus == null && filter.LastDispostion == null && filter.lastcall_From_date == null && filter.lastcall_To_Date == null && filter.reasons == null && filter.droppedCount == null && filter.visit_Type == null))
                {
                    try
                    {
                        using (var db = new AutoSherDBContext())
                        {

                            string str = @"CALL smr_filtered_counts(@inwyzuser_id,@incamp_id,@induetype,@inloc,@nxtserfrmdate,@nxtsertodate,@infromlsddate,@intolsddate ,@intag ,@inmodel);";

                            MySqlParameter[] sqlParameter = new MySqlParameter[]
                            {
                        new MySqlParameter("inwyzuser_id", UserId),
                        new MySqlParameter("incamp_id", campId),
                        new MySqlParameter("induetype", duetypeId),
                        new MySqlParameter("inloc", locId),
                        new MySqlParameter("nxtserfrmdate", fromDateNew),
                        new MySqlParameter("nxtsertodate", toDateNew),
                        new MySqlParameter("infromlsddate", fromLSDDate),
                        new MySqlParameter("intolsddate", toLSDDate),
                        new MySqlParameter("intag", tagIId),
                        new MySqlParameter("inmodel",modelIds )
                        };
                            dispositionLoadInsurances = db.Database.SqlQuery<serviceFilterCount>(str, sqlParameter).ToList();
                        }
                        return Json(new { success = true, filterls = dispositionLoadInsurances });
                        // return dispositionLoadInsurances;
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
            // return dispositionLoadInsurances;
        }
        #endregion

    }
}