using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoSherpa_project.Models;
using MySql.Data.MySqlClient;
using AutoSherpa_project.Models.ViewModels;
using Newtonsoft.Json;

namespace AutoSherpa_project.Controllers
{
    [AuthorizeFilter]
    public class RMCallLogReminderController : Controller
    {
        // GET: callLogReminder


        #region  Insurance Reminder Starts
        public ActionResult RM_insuranceReminder()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    var ddlCampaign = db.campaigns.Where(m => m.campaignType == "Insurance").Select(m => new { id = m.id, campaignName = m.campaignName }).ToList();
                    ViewBag.ddlCampaign = ddlCampaign;


                    var ddlCRE = db.wyzusers.Where(m => m.insuranceRole == true).Select(m => new { id = m.id, userName = m.userName }).ToList();
                    ViewBag.ddlCRE = ddlCRE;


                    var ddlmodelList = db.modelslists.Select(m => new { id = m.id, model = m.model }).ToList();
                    ViewBag.ddlmodelList = ddlmodelList;


                    var ddlsisposition = db.calldispositiondatas.Select(m => new { id = m.id, dispo = m.disposition }).ToList();
                    ViewBag.ddlsisposition = ddlsisposition;

                    string managerUsername = Session["UserName"].ToString();
                    ViewBag.FreshCallsInsCount = db.Database.SqlQuery<int>("call insuranceDashboardCountsCREManager(@incremanager,@indashboardId);",
                        new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername) ,
                         new MySqlParameter("@indashboardId", 1) }).FirstOrDefault();

                    ViewBag.followupInsCount = db.Database.SqlQuery<int>("call insuranceDashboardCountsCREManager(@incremanager,@indashboardId);",
                        new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername) ,
                         new MySqlParameter("@indashboardId", 2) }).FirstOrDefault();

                    ViewBag.nonContactsIns = db.Database.SqlQuery<int>("call insuranceDashboardCountsCREManager(@incremanager,@indashboardId);",
                        new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername) ,
                          new MySqlParameter("@indashboardId", 3) }).FirstOrDefault();

                    ViewBag.overDueInsCount = db.Database.SqlQuery<int>("call insuranceDashboardCountsCREManager(@incremanager,@indashboardId);",
                        new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername) ,
                          new MySqlParameter("@indashboardId", 4) }).FirstOrDefault();

                    ViewBag.totalRedFlag = db.Database.SqlQuery<int>("call insuranceDashboardCountsCREManager(@incremanager,@indashboardId);",
                        new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername) ,
                          new MySqlParameter("@indashboardId", 5) }).FirstOrDefault();

                    ViewBag.attemptsIns = db.Database.SqlQuery<int>("call insuranceDashboardCountsCREManager(@incremanager,@indashboardId);",
                        new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername) ,
                          new MySqlParameter("@indashboardId", 6) }).FirstOrDefault();

                    ViewBag.pendingIns = db.Database.SqlQuery<int>("call insuranceDashboardCountsCREManager(@incremanager,@indashboardId);",
                        new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername) ,
                          new MySqlParameter("@indashboardId", 7) }).FirstOrDefault();

                    ViewBag.followupInsCount = db.Database.SqlQuery<int>("call insuranceDashboardCountsCREManager(@incremanager,@indashboardId);",
                        new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername) ,
                          new MySqlParameter("@indashboardId", 8) }).FirstOrDefault();

                    ViewBag.totalContactsInsCREperDay = db.Database.SqlQuery<int>("call insuranceDashboardCountsCREManager(@incremanager,@indashboardId);",
                        new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername) ,
                          new MySqlParameter("@indashboardId", 9) }).FirstOrDefault();

                    ViewBag.totalCallsperDay = db.Database.SqlQuery<int>("call insuranceDashboardCountsCREManager(@incremanager,@indashboardId);",
                        new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername) ,
                          new MySqlParameter("@indashboardId", 10) }).FirstOrDefault();

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
            return View();
        }
        public ActionResult getInsuranceBucket(string insurancereminderData)
        {
            List<CallLogDispositionLoadReminderMR> service = null;
            List<CallLogDispositionLoadReminderMR> serviceCount = null;

            //Parameters of Paging..........
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchPattern = Request["search[value]"];
            int UserId = Convert.ToInt32(Session["UserId"].ToString());
            string managerUsername = Session["UserName"].ToString();

            string expiryFromDate = "", expiryToDate = "";
            insuranceFilter filter = new insuranceFilter();
            if (insurancereminderData != null)
            {
                filter = JsonConvert.DeserializeObject<insuranceFilter>(insurancereminderData);
            }

            long fromIndex, toIndex;
            long totalCount = 0;

            if (searchPattern != "")
            {
                filter.isFiltered = true;
            }
            using (var db = new AutoSherDBContext())
            {
                try
                {

                    if (filter.getDataFor == 1)
                    {

                        string expfromDate, exptoDate, campaignName, CREIds, appfromDate, apptoDate, flag, callfromDate, calltoDate, attempt, models, reason, appType;

                        campaignName = filter.campaign == null ? "" : filter.campaign;
                        CREIds = filter.CRES == null ? "" : filter.CRES;
                        expfromDate = filter.expiryfromDate == null ? "" : Convert.ToDateTime(filter.expiryfromDate.ToString()).ToString("yyyy-MM-dd");
                        exptoDate = filter.expirytoDate == null ? "" : Convert.ToDateTime(filter.expirytoDate.ToString()).ToString("yyyy-MM-dd");
                        appfromDate = filter.appointmentFromDate == null ? "" : Convert.ToDateTime(filter.appointmentFromDate.ToString()).ToString("yyyy-MM-dd");
                        apptoDate = filter.appointmentToDate == null ? "" : Convert.ToDateTime(filter.appointmentToDate.ToString()).ToString("yyyy-MM-dd");
                        callfromDate = filter.callfromDate == null ? "" : Convert.ToDateTime(filter.callfromDate.ToString()).ToString("yyyy-MM-dd");
                        calltoDate = filter.calltoDate == null ? "" : Convert.ToDateTime(filter.calltoDate.ToString()).ToString("yyyy-MM-dd");
                        flag = filter.flag == null ? "" : filter.flag;
                        attempt = filter.attempts == null ? "" : filter.attempts;
                        reason = filter.reasons == null ? "" : filter.reasons;
                        models = filter.model == null ? "" : filter.model;
                        appType = filter.appointmentType == null ? "" : filter.appointmentType;

                        expiryFromDate = expfromDate;
                        expiryToDate = exptoDate;
                        totalCount = db.Database.SqlQuery<int>("call cremanagerinsuranceScheduledCallsCount(@cremanagerid,@wyzuserid,@in_campaign_id,@instartdate,@inenddate,@in_flag,@in_model,@pattern)",
                           new MySqlParameter("@cremanagerid", managerUsername),
                           new MySqlParameter("@wyzuserid", CREIds),
                           new MySqlParameter("@in_campaign_id", campaignName),
                           new MySqlParameter("@instartdate", expfromDate),
                           new MySqlParameter("@inenddate", exptoDate),
                           new MySqlParameter("@in_flag", flag),
                           new MySqlParameter("@in_model", models),
                       new MySqlParameter("@pattern", searchPattern)).FirstOrDefault();

                        fromIndex = start;
                        toIndex = length;
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }
                        service = InsuranceassignedListOfUserMR(managerUsername, CREIds, campaignName, expfromDate, exptoDate, flag, models, searchPattern, fromIndex, toIndex);

                        if (filter.isFiltered == true)
                        {
                            serviceCount = InsuranceassignedListOfUserMR(managerUsername, CREIds, campaignName, expfromDate, exptoDate, flag, models, searchPattern, fromIndex, totalCount);
                        }
                    }
                    else if (filter.getDataFor == 2)
                    {
                        string expfromDate, exptoDate, campaignName, CREIds, appfromDate, apptoDate, flag, callfromDate, calltoDate, attempt, models, reason, appType;

                        campaignName = filter.campaign == null ? "" : filter.campaign;
                        CREIds = filter.CRES == null ? "" : filter.CRES;
                        expfromDate = filter.expiryfromDate == null ? "" : Convert.ToDateTime(filter.expiryfromDate.ToString()).ToString("yyyy-MM-dd");
                        exptoDate = filter.expirytoDate == null ? "" : Convert.ToDateTime(filter.expirytoDate.ToString()).ToString("yyyy-MM-dd");
                        appfromDate = filter.appointmentFromDate == null ? "" : Convert.ToDateTime(filter.appointmentFromDate.ToString()).ToString("yyyy-MM-dd");
                        apptoDate = filter.appointmentToDate == null ? "" : Convert.ToDateTime(filter.appointmentToDate.ToString()).ToString("yyyy-MM-dd");
                        callfromDate = filter.callfromDate == null ? "" : Convert.ToDateTime(filter.callfromDate.ToString()).ToString("yyyy-MM-dd");
                        calltoDate = filter.calltoDate == null ? "" : Convert.ToDateTime(filter.calltoDate.ToString()).ToString("yyyy-MM-dd");
                        flag = filter.flag == null ? "" : filter.flag;
                        attempt = filter.attempts == null ? "" : filter.attempts;
                        reason = filter.reasons == null ? "" : filter.reasons;
                        models = filter.model == null ? "" : filter.model;
                        appType = filter.appointmentType == null ? "" : filter.appointmentType;
                        long dispoType = 4;

                        totalCount = getInsurancefollowUpCallLogTableDataMRCount(managerUsername, CREIds, expfromDate, exptoDate, campaignName, appfromDate, apptoDate, callfromDate, calltoDate, appType, models, flag, reason, searchPattern, dispoType);

                        fromIndex = start;
                        toIndex = length;
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }
                        service = getInsurancefollowUpCallLogTableDataMR(managerUsername, CREIds, expfromDate, exptoDate, campaignName, appfromDate, apptoDate, callfromDate, calltoDate, appType, models, flag, reason, searchPattern, dispoType, fromIndex, toIndex);

                        if (filter.isFiltered == true)
                        {
                            serviceCount = getInsurancefollowUpCallLogTableDataMR(managerUsername, CREIds, expfromDate, exptoDate, campaignName, appfromDate, apptoDate, callfromDate, calltoDate, appType, models, flag, reason, searchPattern, dispoType, fromIndex, totalCount);
                        }


                    }
                    else if (filter.getDataFor == 3)
                    {
                        string expfromDate, exptoDate, campaignName, CREIds, appfromDate, apptoDate, flag, callfromDate, calltoDate, attempt, models, reason, appType;

                        campaignName = filter.campaign == null ? "" : filter.campaign;
                        CREIds = filter.CRES == null ? "" : filter.CRES;
                        expfromDate = filter.expiryfromDate == null ? "" : Convert.ToDateTime(filter.expiryfromDate.ToString()).ToString("yyyy-MM-dd");
                        exptoDate = filter.expirytoDate == null ? "" : Convert.ToDateTime(filter.expirytoDate.ToString()).ToString("yyyy-MM-dd");
                        appfromDate = filter.appointmentFromDate == null ? "" : Convert.ToDateTime(filter.appointmentFromDate.ToString()).ToString("yyyy-MM-dd");
                        apptoDate = filter.appointmentToDate == null ? "" : Convert.ToDateTime(filter.appointmentToDate.ToString()).ToString("yyyy-MM-dd");
                        callfromDate = filter.callfromDate == null ? "" : Convert.ToDateTime(filter.callfromDate.ToString()).ToString("yyyy-MM-dd");
                        calltoDate = filter.calltoDate == null ? "" : Convert.ToDateTime(filter.calltoDate.ToString()).ToString("yyyy-MM-dd");
                        flag = filter.flag == null ? "" : filter.flag;
                        attempt = filter.attempts == null ? "" : filter.attempts;
                        reason = filter.reasons == null ? "" : filter.reasons;
                        models = filter.model == null ? "" : filter.model;
                        appType = filter.appointmentType == null ? "" : filter.appointmentType;
                        long dispoType = 25;

                        totalCount = getInsurancefollowUpCallLogTableDataMRCount(managerUsername, CREIds, expfromDate, exptoDate, campaignName, appfromDate, apptoDate, callfromDate, calltoDate, appType, models, flag, reason, searchPattern, dispoType);

                        fromIndex = start;
                        toIndex = length;
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }
                        service = getInsurancefollowUpCallLogTableDataMR(managerUsername, CREIds, expfromDate, exptoDate, campaignName, appfromDate, apptoDate, callfromDate, calltoDate, appType, models, flag, reason, searchPattern, dispoType, fromIndex, toIndex);

                        if (filter.isFiltered == true)
                        {
                            serviceCount = getInsurancefollowUpCallLogTableDataMR(managerUsername, CREIds, expfromDate, exptoDate, campaignName, appfromDate, apptoDate, callfromDate, calltoDate, appType, models, flag, reason, searchPattern, dispoType, fromIndex, totalCount);
                        }

                    }
                    else if (filter.getDataFor == 4)
                    {
                        string expfromDate, exptoDate, campaignName, CREIds, appfromDate, apptoDate, flag, callfromDate, calltoDate, attempt, models, reason, appType;

                        campaignName = filter.campaign == null ? "" : filter.campaign;
                        CREIds = filter.CRES == null ? "" : filter.CRES;
                        expfromDate = filter.expiryfromDate == null ? "" : Convert.ToDateTime(filter.expiryfromDate.ToString()).ToString("yyyy-MM-dd");
                        exptoDate = filter.expirytoDate == null ? "" : Convert.ToDateTime(filter.expirytoDate.ToString()).ToString("yyyy-MM-dd");
                        appfromDate = filter.appointmentFromDate == null ? "" : Convert.ToDateTime(filter.appointmentFromDate.ToString()).ToString("yyyy-MM-dd");
                        apptoDate = filter.appointmentToDate == null ? "" : Convert.ToDateTime(filter.appointmentToDate.ToString()).ToString("yyyy-MM-dd");
                        callfromDate = filter.callfromDate == null ? "" : Convert.ToDateTime(filter.callfromDate.ToString()).ToString("yyyy-MM-dd");
                        calltoDate = filter.calltoDate == null ? "" : Convert.ToDateTime(filter.calltoDate.ToString()).ToString("yyyy-MM-dd");
                        flag = filter.flag == null ? "" : filter.flag;
                        attempt = filter.attempts == null ? "" : filter.attempts;
                        reason = filter.reasons == null ? "" : filter.reasons;
                        models = filter.model == null ? "" : filter.model;
                        appType = filter.appointmentType == null ? "" : filter.appointmentType;
                        long dispoType = 26;

                        totalCount = getInsurancefollowUpCallLogTableDataMRCount(managerUsername, CREIds, expfromDate, exptoDate, campaignName, appfromDate, apptoDate, callfromDate, calltoDate, appType, models, flag, reason, searchPattern, dispoType);

                        fromIndex = start;
                        toIndex = length;
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }
                        service = getInsurancefollowUpCallLogTableDataMR(managerUsername, CREIds, expfromDate, exptoDate, campaignName, appfromDate, apptoDate, callfromDate, calltoDate, appType, models, flag, reason, searchPattern, dispoType, fromIndex, toIndex);

                        if (filter.isFiltered == true)
                        {
                            serviceCount = getInsurancefollowUpCallLogTableDataMR(managerUsername, CREIds, expfromDate, exptoDate, campaignName, appfromDate, apptoDate, callfromDate, calltoDate, appType, models, flag, reason, searchPattern, dispoType, fromIndex, totalCount);
                        }

                    }
                    else if (filter.getDataFor == 5)
                    {
                        string expfromDate, exptoDate, campaignName, CREIds, appfromDate, apptoDate, flag, callfromDate, calltoDate, attempt, models, reason, appType;

                        campaignName = filter.campaign == null ? "" : filter.campaign;
                        CREIds = filter.CRES == null ? "" : filter.CRES;
                        expfromDate = filter.expiryfromDate == null ? "" : Convert.ToDateTime(filter.expiryfromDate.ToString()).ToString("yyyy-MM-dd");
                        exptoDate = filter.expirytoDate == null ? "" : Convert.ToDateTime(filter.expirytoDate.ToString()).ToString("yyyy-MM-dd");
                        appfromDate = filter.appointmentFromDate == null ? "" : Convert.ToDateTime(filter.appointmentFromDate.ToString()).ToString("yyyy-MM-dd");
                        apptoDate = filter.appointmentToDate == null ? "" : Convert.ToDateTime(filter.appointmentToDate.ToString()).ToString("yyyy-MM-dd");
                        callfromDate = filter.callfromDate == null ? "" : Convert.ToDateTime(filter.callfromDate.ToString()).ToString("yyyy-MM-dd");
                        calltoDate = filter.calltoDate == null ? "" : Convert.ToDateTime(filter.calltoDate.ToString()).ToString("yyyy-MM-dd");
                        flag = filter.flag == null ? "" : filter.flag;
                        attempt = filter.attempts == null ? "" : filter.attempts;
                        reason = filter.reasons == null ? "" : filter.reasons;
                        models = filter.model == null ? "" : filter.model;
                        appType = filter.appointmentType == null ? "" : filter.appointmentType;
                        long dispoType = 1;


                        totalCount = db.Database.SqlQuery<int>("call cremanagerinsurancefilterforNonContactsCount(@cremanagerid ,@pattern,@instartdate,@inenddate,@incampaignname," +
                            "@incallFromDate,@incallToDate,@inflag,@inattempts,@wyzuserid,@dispositiontype)",
                           new MySqlParameter("@cremanagerid", managerUsername),
                           new MySqlParameter("@pattern", searchPattern),
                           new MySqlParameter("@instartdate", expfromDate),
                           new MySqlParameter("@inenddate", exptoDate),
                           new MySqlParameter("@incampaignname", campaignName),
                           new MySqlParameter("@incallFromDate", callfromDate),
                           new MySqlParameter("@incallToDate", calltoDate),
                           new MySqlParameter("@inflag", flag),
                           new MySqlParameter("@inattempts", attempt),
                           new MySqlParameter("@wyzuserid", CREIds),
                           new MySqlParameter("@dispositiontype", dispoType)).FirstOrDefault();

                        fromIndex = start;
                        toIndex = length;
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }
                        service = getInsurancenonContactsServerDataTableMR(managerUsername, CREIds, searchPattern, expfromDate, exptoDate, campaignName, callfromDate, calltoDate, flag, attempt, dispoType, fromIndex, toIndex);

                        if (filter.isFiltered == true)
                        {
                            serviceCount = getInsurancenonContactsServerDataTableMR(managerUsername, CREIds, searchPattern, expfromDate, exptoDate, campaignName, callfromDate, calltoDate, flag, attempt, dispoType, fromIndex, totalCount);
                        }
                    }
                    else if (filter.getDataFor == 6)
                    {
                        string expfromDate, exptoDate, campaignName, CREIds, appfromDate, apptoDate, flag, callfromDate, calltoDate, attempt, models, reason, appType;

                        campaignName = filter.campaign == null ? "" : filter.campaign;
                        CREIds = filter.CRES == null ? "" : filter.CRES;
                        expfromDate = filter.expiryfromDate == null ? "" : Convert.ToDateTime(filter.expiryfromDate.ToString()).ToString("yyyy-MM-dd");
                        exptoDate = filter.expirytoDate == null ? "" : Convert.ToDateTime(filter.expirytoDate.ToString()).ToString("yyyy-MM-dd");
                        appfromDate = filter.appointmentFromDate == null ? "" : Convert.ToDateTime(filter.appointmentFromDate.ToString()).ToString("yyyy-MM-dd");
                        apptoDate = filter.appointmentToDate == null ? "" : Convert.ToDateTime(filter.appointmentToDate.ToString()).ToString("yyyy-MM-dd");
                        callfromDate = filter.callfromDate == null ? "" : Convert.ToDateTime(filter.callfromDate.ToString()).ToString("yyyy-MM-dd");
                        calltoDate = filter.calltoDate == null ? "" : Convert.ToDateTime(filter.calltoDate.ToString()).ToString("yyyy-MM-dd");
                        flag = filter.flag == null ? "" : filter.flag;
                        attempt = filter.attempts == null ? "" : filter.attempts;
                        reason = filter.reasons == null ? "" : filter.reasons;
                        models = filter.model == null ? "" : filter.model;
                        appType = filter.appointmentType == null ? "" : filter.appointmentType;

                        totalCount = getInsurancePreviousDayServerDataTableMRCount(managerUsername, "1");

                        fromIndex = start;
                        toIndex = length;
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }
                        service = getInsurancePreviousDayServerDataTableMR(managerUsername, "1", CREIds, searchPattern, expfromDate, exptoDate, appType, flag, fromIndex, toIndex);

                        if (filter.isFiltered == true)
                        {
                            serviceCount = getInsurancePreviousDayServerDataTableMR(managerUsername, "1", CREIds, searchPattern, expfromDate, exptoDate, appType, flag, fromIndex, totalCount);
                        }


                    }
                    else if (filter.getDataFor == 7)
                    {
                        string expfromDate, exptoDate, campaignName, CREIds, appfromDate, apptoDate, flag, callfromDate, calltoDate, attempt, models, reason, appType;

                        campaignName = filter.campaign == null ? "" : filter.campaign;
                        CREIds = filter.CRES == null ? "" : filter.CRES;
                        expfromDate = filter.expiryfromDate == null ? "" : Convert.ToDateTime(filter.expiryfromDate.ToString()).ToString("yyyy-MM-dd");
                        exptoDate = filter.expirytoDate == null ? "" : Convert.ToDateTime(filter.expirytoDate.ToString()).ToString("yyyy-MM-dd");
                        appfromDate = filter.appointmentFromDate == null ? "" : Convert.ToDateTime(filter.appointmentFromDate.ToString()).ToString("yyyy-MM-dd");
                        apptoDate = filter.appointmentToDate == null ? "" : Convert.ToDateTime(filter.appointmentToDate.ToString()).ToString("yyyy-MM-dd");
                        callfromDate = filter.callfromDate == null ? "" : Convert.ToDateTime(filter.callfromDate.ToString()).ToString("yyyy-MM-dd");
                        calltoDate = filter.calltoDate == null ? "" : Convert.ToDateTime(filter.calltoDate.ToString()).ToString("yyyy-MM-dd");
                        flag = filter.flag == null ? "" : filter.flag;
                        attempt = filter.attempts == null ? "" : filter.attempts;
                        reason = filter.reasons == null ? "" : filter.reasons;
                        models = filter.model == null ? "" : filter.model;
                        appType = filter.appointmentType == null ? "" : filter.appointmentType;

                        totalCount = getInsurancePreviousDayServerDataTableMRCount(managerUsername, "2");

                        fromIndex = start;
                        toIndex = length;
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }
                        service = getInsurancePreviousDayServerDataTableMR(managerUsername, "2", CREIds, searchPattern, expfromDate, exptoDate, appType, flag, fromIndex, toIndex);

                        if (filter.isFiltered == true)
                        {
                            serviceCount = getInsurancePreviousDayServerDataTableMR(managerUsername, "2", CREIds, searchPattern, expfromDate, exptoDate, appType, flag, fromIndex, totalCount);
                        }

                    }
                    else if (filter.getDataFor == 8)
                    {
                        string expfromDate, exptoDate, campaignName, CREIds, appfromDate, apptoDate, flag, callfromDate, calltoDate, attempt, models, reason, appType;

                        campaignName = filter.campaign == null ? "" : filter.campaign;
                        CREIds = filter.CRES == null ? "" : filter.CRES;
                        expfromDate = filter.expiryfromDate == null ? "" : Convert.ToDateTime(filter.expiryfromDate.ToString()).ToString("yyyy-MM-dd");
                        exptoDate = filter.expirytoDate == null ? "" : Convert.ToDateTime(filter.expirytoDate.ToString()).ToString("yyyy-MM-dd");
                        appfromDate = filter.appointmentFromDate == null ? "" : Convert.ToDateTime(filter.appointmentFromDate.ToString()).ToString("yyyy-MM-dd");
                        apptoDate = filter.appointmentToDate == null ? "" : Convert.ToDateTime(filter.appointmentToDate.ToString()).ToString("yyyy-MM-dd");
                        callfromDate = filter.callfromDate == null ? "" : Convert.ToDateTime(filter.callfromDate.ToString()).ToString("yyyy-MM-dd");
                        calltoDate = filter.calltoDate == null ? "" : Convert.ToDateTime(filter.calltoDate.ToString()).ToString("yyyy-MM-dd");
                        flag = filter.flag == null ? "" : filter.flag;
                        attempt = filter.attempts == null ? "" : filter.attempts;
                        reason = filter.reasons == null ? "" : filter.reasons;
                        models = filter.model == null ? "" : filter.model;
                        appType = filter.appointmentType == null ? "" : filter.appointmentType;

                        totalCount = getInsuranceFuturefollowUpDataMRCount(managerUsername);

                        fromIndex = start;
                        toIndex = length;
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }
                        service = getInsuranceFuturefollowUpDataMR(managerUsername, CREIds, expfromDate, exptoDate, campaignName, models, flag, searchPattern, fromIndex, toIndex);

                        if (filter.isFiltered == true)
                        {
                            serviceCount = getInsuranceFuturefollowUpDataMR(managerUsername, CREIds, expfromDate, exptoDate, campaignName, models, flag, searchPattern, fromIndex, totalCount);
                        }

                    }
                }
                catch (Exception ex)
                {

                }
            }
            if (service != null)
            {
                if (filter.isFiltered == true)
                {
                    return Json(new { data = service, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = serviceCount.Count(), fromDate = expiryFromDate, toDate = expiryToDate });
                }
                else if (filter.isFiltered == false)
                {
                    return Json(new { data = service, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount });
                }

            }
            else if (serviceCount != null)
            {
                if (filter.isFiltered == true)
                {
                    return Json(new { data = serviceCount, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = serviceCount.Count(), fromDate = expiryFromDate, toDate = expiryToDate }, JsonRequestBehavior.AllowGet);
                }
                else if (filter.isFiltered == false)
                {
                    return Json(new { data = serviceCount, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount }, JsonRequestBehavior.AllowGet);
                }

            }

            return Json(new { data = "", draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0 });
        }
        public List<CallLogDispositionLoadReminderMR> InsuranceassignedListOfUserMR(string userLoginName, string crename, string campaignName, string fromexpirydaterange, string toexpirydaterange, string flag, string modelName, string searchPattern, long fromIndex, long toIndex)
        {
            List<CallLogDispositionLoadReminderMR> dispositionLoads = null;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string str = @"CALL cremanagerinsuranceScheduledCalls(@cremanagerid,@wyzuserid,@in_campaign_id,@instartdate,@inenddate,@in_flag,@in_model,@pattern,@start_with,@length)";
                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("@cremanagerid", userLoginName),
                        new MySqlParameter("@wyzuserid", crename),
                        new MySqlParameter("@in_campaign_id", campaignName),
                        new MySqlParameter("@instartdate", fromexpirydaterange),
                        new MySqlParameter("@inenddate", toexpirydaterange),
                        new MySqlParameter("@in_flag", flag),
                        new MySqlParameter("@in_model", modelName),
                        new MySqlParameter("@pattern", searchPattern),
                        new MySqlParameter("@start_with", fromIndex),
                        new MySqlParameter("@length", toIndex)
                    };

                    dispositionLoads = db.Database.SqlQuery<CallLogDispositionLoadReminderMR>(str, param).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return dispositionLoads;
        }

        public long getInsurancefollowUpCallLogTableDataMRCount(string username, string crename, string fromexpirydaterange,
            string toexpirydaterange, string campaignName, string appointmentFromDate, string appointmentToDate,
            string callFromDate, string callToDate, string appointmentType, string modelName, string flag,
            string reasons, string searchPattern, long buckettype)
        {
            long totalCount = 0;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    totalCount = db.Database.SqlQuery<int>("call cremanagerinsurancefilterforDispositionContactsCount( @cremanagerid,@instartdate,@inenddate,@incampaignname,@inflag,@infromApdate,@intoApdate,@infromCalldate,@intoCalldate,@inAptype,@inmodel, @inreasons,@pattern, @wyzuserid, @dispositiontype)",
                          new MySqlParameter("@cremanagerid", username),
                          new MySqlParameter("@instartdate", fromexpirydaterange),
                          new MySqlParameter("@inenddate", toexpirydaterange),
                          new MySqlParameter("@incampaignname", campaignName),
                          new MySqlParameter("@inflag", flag),
                          new MySqlParameter("@infromApdate", appointmentFromDate),
                          new MySqlParameter("@intoApdate", appointmentToDate),
                          new MySqlParameter("@infromCalldate", callFromDate),
                          new MySqlParameter("@intoCalldate", callToDate),
                          new MySqlParameter("@inAptype", appointmentType),
                          new MySqlParameter("@inmodel", modelName),
                          new MySqlParameter("@inreasons", reasons),
                          new MySqlParameter("@pattern", searchPattern),
                          new MySqlParameter("@wyzuserid", crename),
                      new MySqlParameter("@dispositiontype", buckettype)).FirstOrDefault();

                    return totalCount;
                }
            }
            catch (Exception ex)
            {

            }
            return totalCount;
        }



        public List<CallLogDispositionLoadReminderMR> getInsurancefollowUpCallLogTableDataMR(string username, string crename,
                string fromexpirydaterange, string toexpirydaterange, string campaignName, string appointmentFromDate,
                string appointmentToDate, string callFromDate, string callToDate, string appointmentType, string modelName,
                string flag, string reasons, string searchPattern, long buckettype, long fromIndex, long toIndex)
        {
            List<CallLogDispositionLoadReminderMR> dispositionLoads = null;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string str = @"CALL cremanagerinsurancefilterforDispositionContacts(@cremanagerid,@instartdate,@inenddate,@incampaignname,
@inflag,@infromApdate,@intoApdate,@infromCalldate,@intoCalldate,@inAptype,@inmodel
 ,@inreasons,@pattern,@wyzuserid,@dispositiontype,@start_with,@length )";
                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("@cremanagerid", username),
                        new MySqlParameter("@instartdate", fromexpirydaterange),
                        new MySqlParameter("@inenddate", toexpirydaterange),
                        new MySqlParameter("@incampaignname", campaignName),
                        new MySqlParameter("@inflag", flag),
                        new MySqlParameter("@infromApdate",appointmentFromDate),
                        new MySqlParameter("@intoApdate", appointmentToDate),
                        new MySqlParameter("@infromCalldate", callFromDate),
                        new MySqlParameter("@intoCalldate", callToDate),
                        new MySqlParameter("@inAptype", appointmentType),
                        new MySqlParameter("@inmodel", modelName),
                        new MySqlParameter("@inreasons", reasons),
                        new MySqlParameter("@pattern", searchPattern),
                        new MySqlParameter("@wyzuserid", crename),
                        new MySqlParameter("@dispositiontype", buckettype),
                        new MySqlParameter("@start_with", fromIndex),
                        new MySqlParameter("@length", toIndex)
                    };
                    dispositionLoads = db.Database.SqlQuery<CallLogDispositionLoadReminderMR>(str, param).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return dispositionLoads;


        }
        public List<CallLogDispositionLoadReminderMR> getInsurancenonContactsServerDataTableMR(string username, string crename,
        string searchPattern, string fromexpirydaterange, string toexpirydaterange, string campaignName,
        string callFromDate, string callToDate, string flag, string attempts, long buckettype, long fromIndex,
        long toIndex)
        {
            List<CallLogDispositionLoadReminderMR> dispositionLoads = null;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string str = @"CALL cremanagerinsurancefilterforNonContacts(@cremanagerid,@pattern,@instartdate,@inenddate,@incampaignname,@incallFromDate," +
                            "@incallToDate,@inflag,@inattempts,@wyzuserid,@dispositiontype,@start_with,@length)";
                    MySqlParameter[] param = new MySqlParameter[]
                    {

                         new MySqlParameter("@cremanagerid", username),
                           new MySqlParameter("@pattern", searchPattern),
                           new MySqlParameter("@instartdate", fromexpirydaterange),
                           new MySqlParameter("@inenddate", toexpirydaterange),
                           new MySqlParameter("@incampaignname", campaignName),
                           new MySqlParameter("@incallFromDate", callFromDate),
                           new MySqlParameter("@incallToDate", callToDate),
                           new MySqlParameter("@inflag", flag),
                           new MySqlParameter("@inattempts", attempts),
                           new MySqlParameter("@wyzuserid", crename),
                           new MySqlParameter("@dispositiontype", buckettype),
                           new MySqlParameter("@start_with",fromIndex),
                       new MySqlParameter("@length", toIndex)
                    };
                    dispositionLoads = db.Database.SqlQuery<CallLogDispositionLoadReminderMR>(str, param).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return dispositionLoads;

        }





        public long getInsurancePreviousDayServerDataTableMRCount(string incremanager, string dispo)
        {
            long totalCount = 0;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    totalCount = db.Database.SqlQuery<int>("call cremanagerInsurancePreviousDayCount(@sbday,@incremanager)",
                          new MySqlParameter("@sbday", dispo),
                      new MySqlParameter("@incremanager", incremanager)).FirstOrDefault();

                    return totalCount;
                }
            }
            catch (Exception ex)
            {

            }
            return totalCount;

        }
        public List<CallLogDispositionLoadReminderMR> getInsurancePreviousDayServerDataTableMR(string incremanager, string dispo,
            string crename, string searchPattern, string fromexpirydaterange, string toexpirydaterange,
            string appointmentType, string flag, long fromIndex, long toIndex)
        {
            List<CallLogDispositionLoadReminderMR> dispositionLoads = null;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string str = @"CALL cremanagerInsurancePreviousDay(@sbday,@incremanager,@inChasers_id,@instatus,@inflag,@instartdate,@inenddate,@pattern,@startwith,@length)";
                    MySqlParameter[] param = new MySqlParameter[]
                    {
                         new MySqlParameter("@sbday", dispo),
                           new MySqlParameter("@incremanager", incremanager),
                           new MySqlParameter("@inChasers_id", crename),
                           new MySqlParameter("@instatus", appointmentType),
                           new MySqlParameter("@inflag", flag),
                           new MySqlParameter("@instartdate", fromexpirydaterange),
                           new MySqlParameter("@inenddate", toexpirydaterange),
                           new MySqlParameter("@pattern", searchPattern),
                           new MySqlParameter("@startwith", fromIndex),
                       new MySqlParameter("@length", 900)
                    };
                    dispositionLoads = db.Database.SqlQuery<CallLogDispositionLoadReminderMR>(str, param).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return dispositionLoads;

        }

        public long getInsuranceFuturefollowUpDataMRCount(string incremanager)
        {
            long totalCount = 0;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    totalCount = db.Database.SqlQuery<int>("call cremanagerInsuranceFutureFollowupCount(@incremanager)",
                      new MySqlParameter("@incremanager", incremanager)).FirstOrDefault();

                    return totalCount;
                }
            }
            catch (Exception ex)
            {

            }
            return totalCount;


        }

        public List<CallLogDispositionLoadReminderMR> getInsuranceFuturefollowUpDataMR(string incremanager, string crename,
            string fromexpirydaterange, string toexpirydaterange, string campaignName, string modelName, string flag,
            string searchPattern, long fromIndex, long toIndex)
        {
            List<CallLogDispositionLoadReminderMR> dispositionLoads = null;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string str = @"CALL cremanagerInsuranceFutureFollowup(@increManager,@inwyzuserid,@instartdate,@inenddate,@incampaignname,@inflag,@inmodel,@pattern,@start_with,@length)";
                    MySqlParameter[] param = new MySqlParameter[]
                    {
                         new MySqlParameter("@increManager", incremanager),
                           new MySqlParameter("@inwyzuserid", crename),
                           new MySqlParameter("@instartdate", fromexpirydaterange),
                           new MySqlParameter("@inenddate", toexpirydaterange),
                           new MySqlParameter("@incampaignname", campaignName),
                           new MySqlParameter("@inflag", flag),
                           new MySqlParameter("@inmodel", modelName),
                           new MySqlParameter("@pattern", searchPattern),
                           new MySqlParameter("@start_with", fromIndex),
                       new MySqlParameter("@length", toIndex)
                    };
                    dispositionLoads = db.Database.SqlQuery<CallLogDispositionLoadReminderMR>(str, param).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return dispositionLoads;


        }

        #endregion Insurance Reminder Ends
        public ActionResult RMOtherReminder()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    int UserId = Convert.ToInt32(Session["UserId"].ToString());
                    string managerUsername = Session["UserName"].ToString();
                    ViewBag.outgoing = getDataMissedIncominOutgoingCount(managerUsername, "OUTGOING", "", "", "", "");
                    ViewBag.missed = getDataMissedIncominOutgoingCount(managerUsername, "MISSED", "", "", "", "");
                    ViewBag.incoming = getDataMissedIncominOutgoingCount(managerUsername, "INCOMING", "", "", "", "");


                    var ddllocation = db.locations.Select(m => new { id = m.cityId, location = m.name }).OrderBy(m => m.location).ToList();
                    ViewBag.ddllocation = ddllocation;

                }
            }
            catch (Exception ex)
            {

            }

            return View();
        }
        public ActionResult RM_PSFReminder()
        {

            return View();
        }

        [HttpGet, ActionName("RM_serviceReminder")]
        public ActionResult RM_serviceReminder()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    db.Database.CommandTimeout = 900;
                    int UserId = Convert.ToInt32(Session["UserId"].ToString());
                    string managerUsername = Session["UserName"].ToString();

                    string user = Session["UserId"].ToString();
                    

                   
                    if(Session["LoginUser"].ToString()== "Insurance")
                    {
                        var ddllocation = db.locations.Where(x=>x.moduleType=="2" || x.moduleType=="3").Select(m => new { id = m.cityId, location = m.name }).OrderBy(m => m.location).ToList();
                        ViewBag.ddllocation = ddllocation;
                    }else if (Session["LoginUser"].ToString() == "Service")
                    {
                        var ddllocation = db.locations.Where(x => x.moduleType == "1" || x.moduleType == "3").Select(m => new { id = m.cityId, location = m.name }).OrderBy(m => m.location).ToList();
                        ViewBag.ddllocation = ddllocation;
                    }
                    


                    var ddlCampaign = db.campaigns.Where(m => m.campaignType != "Insurance").Select(m => new { id = m.id, campaignName = m.campaignName }).ToList();
                    ViewBag.ddlCampaign = ddlCampaign;


                    var ddlserviceType = db.servicetypes.Where(m => m.isActive).Select(m => new { id = m.id, serviceTypeName = m.serviceTypeName }).ToList();
                    ViewBag.ddlserviceType = ddlserviceType;


                    var ddlDispo = db.calldispositiondatas.Where(m => m.dispositionId == 6 || m.dispositionId == 7 || m.dispositionId == 8 || m.dispositionId == 9 || m.dispositionId == 10 || m.dispositionId == 43).Select(m => new { id = m.id, disposition = m.disposition }).ToList();
                    ViewBag.ddlDispo = ddlDispo;
                    //List<string> categories = db.servicebookeds.Select(m => m.serviceBookedType).Distinct().ToList();

                    var ddlBookedserviceType = db.servicebookeds.Select(m => new { serviceTypeName = m.serviceBookedType }).Distinct().ToList();
                    ViewBag.ddlBookedserviceType = ddlBookedserviceType;

                    var ddlreason = db.calldispositiondatas.Where(m => m.mainDispositionId == 5).ToList();
                    ViewBag.ddlreason = ddlreason;

                }
            }
            catch (Exception ex)
            {

            }


            return View();
        }

        public ActionResult loadRMServiceDashboardCounts()
        {
            int countCREMFreshCalls, countCREMFollowups, countCREMNonContacts, countCREMoverDueBookings, countCREMtotalCalls, countCREMbookings, countCREMcontacts;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    
                    long rmId = long.Parse(Session["UserId"].ToString());
                    List<long> creManagerIdList = db.AccessLevels.Where(m => m.rmId == rmId).Select(m => m.creManagerId).Distinct().ToList();
                    List<string> creManagerName = db.wyzusers.Where(x => creManagerIdList.Contains(x.id)).Select(x => x.userName).ToList();
                    string managerList = string.Join(",", creManagerName);

                        db.Database.CommandTimeout = 900;
                       countCREMFreshCalls = db.Database.SqlQuery<int>("call CreMtotalFreshCalls(@wyzUserId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", managerList) }).FirstOrDefault();
                        countCREMFollowups = db.Database.SqlQuery<int>("call CreMTotalfollowups(@mgrUsername);", new MySqlParameter[] { new MySqlParameter("@mgrUsername", managerList) }).FirstOrDefault();
                        countCREMNonContacts = db.Database.SqlQuery<int>("call CreMTotalNoncontacts(@mgrUsername);", new MySqlParameter[] { new MySqlParameter("@mgrUsername", managerList) }).FirstOrDefault();
                        countCREMoverDueBookings = db.Database.SqlQuery<int>("call CreMTotaloverDueBookings(@mgrUsername);", new MySqlParameter[] { new MySqlParameter("@mgrUsername", managerList) }).FirstOrDefault();
                       countCREMtotalCalls = db.Database.SqlQuery<int>("call CreMTotalCallstoday(@mgrUsername);", new MySqlParameter[] { new MySqlParameter("@mgrUsername", managerList) }).FirstOrDefault();
                       countCREMbookings = db.Database.SqlQuery<int>("call CreMTotalBookings(@mgrUsername);", new MySqlParameter[] { new MySqlParameter("@mgrUsername", managerList) }).FirstOrDefault();
                       countCREMcontacts = db.Database.SqlQuery<int>("call CreMTotalContactstoday(@mgrUsername);", new MySqlParameter[] { new MySqlParameter("@mgrUsername", managerList) }).FirstOrDefault();
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

            return Json(new { success = true, countCREMFreshCalls, countCREMFollowups, countCREMNonContacts, countCREMoverDueBookings, countCREMtotalCalls, countCREMbookings, countCREMcontacts });
        }
        public ActionResult getServiceBucket(string reminderData)
        {
            List<CallLogDispositionLoadReminderMR> service = null;
            List<CallLogDispositionLoadReminderMR> serviceCount = null;

            //Parameters of Paging..........
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchPattern = Request["search[value]"];
            int UserId = Convert.ToInt32(Session["UserId"].ToString());
            string UserName = Session["UserName"].ToString();


            serviceFilter filter = new serviceFilter();
            if (reminderData != null)
            {
                filter = JsonConvert.DeserializeObject<serviceFilter>(reminderData);
            }

            long fromIndex, toIndex;
            int totalCount = 0;

            if (searchPattern != "")
            {
                filter.isFiltered = true;
            }

            using (var db = new AutoSherDBContext())
            {
                db.Database.CommandTimeout = 900;
                try
                {
                    string  user = Session["UserId"].ToString();
                    long rmId = long.Parse(Session["UserId"].ToString());

                    //string managerUsername = Session["UserName"].ToString();
                    List<long> creManagerIdList = db.AccessLevels.Where(m => m.rmId == rmId).Select(m => m.creManagerId).Distinct().ToList();
                    List<string> creManagerName = db.wyzusers.Where(x => creManagerIdList.Contains(x.id)).Select(x => x.userName).ToList();
                    string managerList = string.Join(",", creManagerName);
                    if (filter.getDataFor == 1)
                    {
                        string fromDateNew, toDateNew, campaignName, serviceTypeName, CREIds, fromLSDDate, toLSDDate, city, workshopid;

                        campaignName = filter.campaign == null ? "" : filter.campaign;
                        serviceTypeName = filter.serviceType == null ? "" : filter.serviceType;
                        CREIds = filter.CRES == null ? "" : filter.CRES;
                        fromDateNew = filter.fromDate == null ? "" : Convert.ToDateTime(filter.fromDate.ToString()).ToString("yyyy-MM-dd");
                        toDateNew = filter.toDate == null ? "" : Convert.ToDateTime(filter.toDate.ToString()).ToString("yyyy-MM-dd");
                        fromLSDDate = filter.fromSLDate == null ? "" : Convert.ToDateTime(filter.fromSLDate.ToString()).ToString("yyyy-MM-dd");
                        toLSDDate = filter.toSLDate == null ? "" : Convert.ToDateTime(filter.toSLDate.ToString()).ToString("yyyy-MM-dd");
                        city = filter.city == null ? "" : filter.city;
                        workshopid = filter.workshopLoc == null ? "" : filter.workshopLoc;


                        totalCount = db.Database.SqlQuery<int>("call cremanagerServiceReminderCount(@pattern,@mgr_user_id,@users)",
                            new MySqlParameter("@pattern", searchPattern),
                            new MySqlParameter("@mgr_user_id", managerList),
                        new MySqlParameter("@users", CREIds)).FirstOrDefault();
                        fromIndex = start;
                        toIndex = length;
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }

                        service = assignedListOfUserMR(fromDateNew, toDateNew, campaignName, serviceTypeName, searchPattern, managerList, CREIds, fromIndex, toIndex);

                        if (filter.isFiltered == true)
                        {
                            serviceCount = assignedListOfUserMR(fromDateNew, toDateNew, campaignName, serviceTypeName, searchPattern, managerList, CREIds, 0, totalCount);
                        }
                    }
                    else if (filter.getDataFor == 2)
                    {

                        string fromDateNew, toDateNew, campaignName, serviceTypeName, CREIds, fromLSDDate, toLSDDate, city, workshopid, bookingstatus, reasons;

                        campaignName = filter.campaign == null ? "" : filter.campaign;
                        serviceTypeName = filter.serviceType == null ? "" : filter.serviceType;
                        CREIds = filter.CRES == null ? "" : filter.CRES;
                        fromDateNew = filter.fromDate == null ? "" : Convert.ToDateTime(filter.fromDate.ToString()).ToString("yyyy-MM-dd");
                        toDateNew = filter.toDate == null ? "" : Convert.ToDateTime(filter.toDate.ToString()).ToString("yyyy-MM-dd");
                        fromLSDDate = filter.fromSLDate == null ? "" : Convert.ToDateTime(filter.fromSLDate.ToString()).ToString("yyyy-MM-dd");
                        toLSDDate = filter.toSLDate == null ? "" : Convert.ToDateTime(filter.toSLDate.ToString()).ToString("yyyy-MM-dd");
                        city = filter.city == null ? "" : filter.city;
                        workshopid = filter.workshopLoc == null ? "" : filter.workshopLoc;
                        bookingstatus = filter.bookingstatus == null ? "" : filter.bookingstatus;
                        reasons = filter.reasons == null ? "" : filter.reasons;
                        long typeOfDispo = 4;
                        int orderDirection = 1;

                        totalCount = db.Database.SqlQuery<int>("call cremanagerdispositionfilterCount(@pattern,@mgr_user_id,@users,@dispositiontype)",
                            new MySqlParameter("@pattern", searchPattern),
                            new MySqlParameter("@mgr_user_id", managerList),
                        new MySqlParameter("@users", CREIds),
                        new MySqlParameter("@dispositiontype", 4)).FirstOrDefault();
                        fromIndex = start;
                        toIndex = length;
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }

                        service = followUp(fromDateNew, toDateNew, campaignName, serviceTypeName, searchPattern, managerList, user, CREIds, typeOfDispo, fromIndex, toIndex, orderDirection, bookingstatus, reasons, fromLSDDate, toLSDDate);

                        if (filter.isFiltered == true)
                        {
                            serviceCount = followUp(fromDateNew, toDateNew, campaignName, serviceTypeName, searchPattern, managerList, user, CREIds, typeOfDispo, 0, totalCount, orderDirection, bookingstatus, reasons, fromLSDDate, toLSDDate);
                        }
                    }
                    else if (filter.getDataFor == 3)
                    {
                        string fromDateNew, toDateNew, campaignName, serviceTypeName, CREIds, fromLSDDate, toLSDDate, city, workshopid, bookingstatus, reasons;

                        campaignName = filter.campaign == null ? "" : filter.campaign;
                        serviceTypeName = filter.serviceType == null ? "" : filter.serviceType;
                        CREIds = filter.CRES == null ? "" : filter.CRES;
                        fromDateNew = filter.fromDate == null ? "" : Convert.ToDateTime(filter.fromDate.ToString()).ToString("yyyy-MM-dd");
                        toDateNew = filter.toDate == null ? "" : Convert.ToDateTime(filter.toDate.ToString()).ToString("yyyy-MM-dd");
                        fromLSDDate = filter.fromSLDate == null ? "" : Convert.ToDateTime(filter.fromSLDate.ToString()).ToString("yyyy-MM-dd");
                        toLSDDate = filter.toSLDate == null ? "" : Convert.ToDateTime(filter.toSLDate.ToString()).ToString("yyyy-MM-dd");
                        city = filter.city == null ? "" : filter.city;
                        workshopid = filter.workshopLoc == null ? "" : filter.workshopLoc;
                        bookingstatus = filter.bookingstatus == null ? "" : filter.bookingstatus;
                        reasons = filter.reasons == null ? "" : filter.reasons;
                        long typeOfDispo = 3;
                        int orderDirection = 1;

                        totalCount = db.Database.SqlQuery<int>("call cremanagerdispositionfilterCount(@pattern,@mgr_user_id,@users,@dispositiontype)",
                            new MySqlParameter("@pattern", searchPattern),
                            new MySqlParameter("@mgr_user_id", managerList),
                        new MySqlParameter("@users", CREIds),
                        new MySqlParameter("@dispositiontype", 3)).FirstOrDefault();
                        fromIndex = start;
                        toIndex = length;
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }

                        service = followUp(fromDateNew, toDateNew, campaignName, serviceTypeName, searchPattern, managerList, user, CREIds, typeOfDispo, fromIndex, toIndex, orderDirection, bookingstatus, reasons, fromLSDDate, toLSDDate);

                        if (filter.isFiltered == true)
                        {
                            serviceCount = followUp(fromDateNew, toDateNew, campaignName, serviceTypeName, searchPattern, managerList, user, CREIds, typeOfDispo, 0, totalCount, orderDirection, bookingstatus, reasons, fromLSDDate, toLSDDate);
                        }
                    }
                    else if (filter.getDataFor == 4)
                    {
                        string fromDateNew, toDateNew, campaignName, serviceTypeName, CREIds, fromLSDDate, toLSDDate, city, workshopid, bookingstatus, reasons;

                        campaignName = filter.campaign == null ? "" : filter.campaign;
                        serviceTypeName = filter.serviceType == null ? "" : filter.serviceType;
                        CREIds = filter.CRES == null ? "" : filter.CRES;
                        fromDateNew = filter.fromDate == null ? "" : Convert.ToDateTime(filter.fromDate.ToString()).ToString("yyyy-MM-dd");
                        toDateNew = filter.toDate == null ? "" : Convert.ToDateTime(filter.toDate.ToString()).ToString("yyyy-MM-dd");
                        fromLSDDate = filter.fromSLDate == null ? "" : Convert.ToDateTime(filter.fromSLDate.ToString()).ToString("yyyy-MM-dd");
                        toLSDDate = filter.toSLDate == null ? "" : Convert.ToDateTime(filter.toSLDate.ToString()).ToString("yyyy-MM-dd");
                        city = filter.city == null ? "" : filter.city;
                        workshopid = filter.workshopLoc == null ? "" : filter.workshopLoc;
                        bookingstatus = filter.bookingstatus == null ? "" : filter.bookingstatus;
                        reasons = filter.reasons == null ? "" : filter.reasons;
                        long typeOfDispo = 5;
                        int orderDirection = 1;

                        totalCount = db.Database.SqlQuery<int>("call cremanagerdispositionfilterCount(@pattern,@mgr_user_id,@users,@dispositiontype)",
                            new MySqlParameter("@pattern", searchPattern),
                            new MySqlParameter("@mgr_user_id", managerList),
                        new MySqlParameter("@users", CREIds),
                        new MySqlParameter("@dispositiontype", 5)).FirstOrDefault();
                        fromIndex = start;
                        toIndex = length;
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }

                        service = followUp(fromDateNew, toDateNew, campaignName, serviceTypeName, searchPattern, managerList, user, CREIds, typeOfDispo, fromIndex, toIndex, orderDirection, bookingstatus, reasons, fromLSDDate, toLSDDate);

                        if (filter.isFiltered == true)
                        {
                            serviceCount = followUp(fromDateNew, toDateNew, campaignName, serviceTypeName, searchPattern, managerList, user, CREIds, typeOfDispo, 0, totalCount, orderDirection, bookingstatus, reasons, fromLSDDate, toLSDDate);
                        }
                    }
                    else if (filter.getDataFor == 5)
                    {
                        string fromDateNew, toDateNew, campaignName, serviceTypeName, CREIds, fromLSDDate, toLSDDate, city, workshopid, lastDipo, droppedCount;

                        campaignName = filter.campaign == null ? "" : filter.campaign;
                        serviceTypeName = filter.serviceType == null ? "" : filter.serviceType;
                        CREIds = filter.CRES == null ? "" : filter.CRES;
                        fromDateNew = filter.fromDate == null ? "" : Convert.ToDateTime(filter.fromDate.ToString()).ToString("yyyy-MM-dd");
                        toDateNew = filter.toDate == null ? "" : Convert.ToDateTime(filter.toDate.ToString()).ToString("yyyy-MM-dd");
                        fromLSDDate = filter.fromSLDate == null ? "" : Convert.ToDateTime(filter.fromSLDate.ToString()).ToString("yyyy-MM-dd");
                        toLSDDate = filter.toSLDate == null ? "" : Convert.ToDateTime(filter.toSLDate.ToString()).ToString("yyyy-MM-dd");
                        city = filter.city == null ? "" : filter.city;
                        workshopid = filter.workshopLoc == null ? "" : filter.workshopLoc;
                        lastDipo = filter.lastDipo == null ? "" : filter.lastDipo;
                        droppedCount = filter.droppedCount == null ? "" : filter.droppedCount;

                        totalCount = db.Database.SqlQuery<int>("call cremanagerNonContactsSearchCount(@pattern,@mgr_user_id,@users)",
                            new MySqlParameter("@pattern", searchPattern),
                            new MySqlParameter("@mgr_user_id", managerList),
                        new MySqlParameter("@users", CREIds)).FirstOrDefault();
                        fromIndex = start;
                        toIndex = length;
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }


                        service = getnonContactsListOfUserMR(fromDateNew, toDateNew, campaignName, lastDipo, droppedCount, searchPattern, managerList, CREIds, fromIndex, toIndex, fromLSDDate, toLSDDate);

                        if (filter.isFiltered == true)
                        {
                            serviceCount = getnonContactsListOfUserMR(fromDateNew, toDateNew, campaignName, lastDipo, droppedCount, searchPattern, managerList, CREIds, 0, totalCount, fromLSDDate, toLSDDate);
                        }
                    }
                    else if (filter.getDataFor == 6)
                    {
                        string fromDateNew, toDateNew, campaignName, serviceTypeName, CREIds, fromLSDDate, toLSDDate, city, workshopid, lastDipo, droppedCount;

                        campaignName = filter.campaign == null ? "" : filter.campaign;
                        serviceTypeName = filter.serviceType == null ? "" : filter.serviceType;
                        CREIds = filter.CRES == null ? "" : filter.CRES;
                        fromDateNew = filter.fromDate == null ? "" : Convert.ToDateTime(filter.fromDate.ToString()).ToString("yyyy-MM-dd");
                        toDateNew = filter.toDate == null ? "" : Convert.ToDateTime(filter.toDate.ToString()).ToString("yyyy-MM-dd");
                        fromLSDDate = filter.fromSLDate == null ? "" : Convert.ToDateTime(filter.fromSLDate.ToString()).ToString("yyyy-MM-dd");
                        toLSDDate = filter.toSLDate == null ? "" : Convert.ToDateTime(filter.toSLDate.ToString()).ToString("yyyy-MM-dd");
                        city = filter.city == null ? "" : filter.city;
                        workshopid = filter.workshopLoc == null ? "" : filter.workshopLoc;
                        lastDipo = filter.lastDipo == null ? "" : filter.lastDipo;
                        droppedCount = filter.droppedCount == null ? "" : filter.droppedCount;

                        totalCount = db.Database.SqlQuery<int>("call nonFS_nonPMS_vehiclereportedListsCremanagerCount(@mgr_user_id)",
                            new MySqlParameter("@mgr_user_id", managerList)).FirstOrDefault();
                        fromIndex = start;
                        toIndex = length;
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }

                        service = getNonFsPms(managerList, fromDateNew, toDateNew, searchPattern, fromIndex, toIndex);

                        if (filter.isFiltered == true)
                        {
                            serviceCount = getNonFsPms(managerList, fromDateNew, toDateNew, searchPattern, 0, totalCount);
                        }
                    }
                    else if (filter.getDataFor == 7)
                    {
                        string fromDateNew, toDateNew, campaignName, serviceTypeName, CREIds, fromLSDDate, toLSDDate, city, workshopid, lastDipo, droppedCount, typeOfPickup, instatus;

                        campaignName = filter.campaign == null ? "" : filter.campaign;
                        serviceTypeName = filter.serviceBookedType == null ? "" : filter.serviceBookedType;
                        CREIds = filter.CRES == null ? "" : filter.CRES;
                        fromDateNew = filter.fromDate == null ? "" : Convert.ToDateTime(filter.fromDate.ToString()).ToString("yyyy-MM-dd");
                        toDateNew = filter.toDate == null ? "" : Convert.ToDateTime(filter.toDate.ToString()).ToString("yyyy-MM-dd");
                        fromLSDDate = filter.fromSLDate == null ? "" : Convert.ToDateTime(filter.fromSLDate.ToString()).ToString("yyyy-MM-dd");
                        toLSDDate = filter.toSLDate == null ? "" : Convert.ToDateTime(filter.toSLDate.ToString()).ToString("yyyy-MM-dd");
                        city = filter.city == null ? "" : filter.city;
                        workshopid = filter.workshopLoc == null ? "" : filter.workshopLoc;
                        lastDipo = filter.lastDipo == null ? "" : filter.lastDipo;
                        droppedCount = filter.droppedCount == null ? "" : filter.droppedCount;
                        typeOfPickup = filter.typeOfPickup == null ? "" : filter.typeOfPickup;
                        instatus = filter.bookingstatus == null ? "" : filter.bookingstatus;

                        totalCount = db.Database.SqlQuery<int>("call ServiceBookingPreviousDayCallsCremanagerCount(@increManager,@sbday)",
 new MySqlParameter[] { new MySqlParameter("@increManager", managerList), new MySqlParameter("@sbday", 1) }).FirstOrDefault();
                        fromIndex = start;
                        toIndex = length;
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }



                        service = getSBNthData(managerList, 1, CREIds, serviceTypeName, typeOfPickup, searchPattern, fromIndex, toIndex, instatus, fromLSDDate, toLSDDate);

                        if (filter.isFiltered == true)
                        {
                            serviceCount = getSBNthData(managerList, 1, CREIds, serviceTypeName, typeOfPickup, searchPattern, 0, totalCount, instatus, fromLSDDate, toLSDDate);
                        }
                    }
                    else if (filter.getDataFor == 8)
                    {
                        string fromDateNew, toDateNew, campaignName, serviceTypeName, CREIds, fromLSDDate, toLSDDate, city, workshopid, lastDipo, droppedCount, typeOfPickup, instatus;

                        campaignName = filter.campaign == null ? "" : filter.campaign;
                        serviceTypeName = filter.serviceBookedType == null ? "" : filter.serviceBookedType;
                        CREIds = filter.CRES == null ? "" : filter.CRES;
                        fromDateNew = filter.fromDate == null ? "" : Convert.ToDateTime(filter.fromDate.ToString()).ToString("yyyy-MM-dd");
                        toDateNew = filter.toDate == null ? "" : Convert.ToDateTime(filter.toDate.ToString()).ToString("yyyy-MM-dd");
                        fromLSDDate = filter.fromSLDate == null ? "" : Convert.ToDateTime(filter.fromSLDate.ToString()).ToString("yyyy-MM-dd");
                        toLSDDate = filter.toSLDate == null ? "" : Convert.ToDateTime(filter.toSLDate.ToString()).ToString("yyyy-MM-dd");
                        city = filter.city == null ? "" : filter.city;
                        workshopid = filter.workshopLoc == null ? "" : filter.workshopLoc;
                        lastDipo = filter.lastDipo == null ? "" : filter.lastDipo;
                        droppedCount = filter.droppedCount == null ? "" : filter.droppedCount;
                        typeOfPickup = filter.typeOfPickup == null ? "" : filter.typeOfPickup;
                        instatus = filter.bookingstatus == null ? "" : filter.bookingstatus;


                        totalCount = db.Database.SqlQuery<int>("call ServiceBookingPreviousDayCallsCremanagerCount(@increManager,@sbday)",new MySqlParameter[] { new MySqlParameter("@increManager", managerList), new MySqlParameter("@sbday", 2) }).FirstOrDefault();
                        fromIndex = start;
                        toIndex = length;
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }


                        service = getSBNthData(managerList, 2, CREIds, serviceTypeName, typeOfPickup, searchPattern, fromIndex, toIndex, instatus, fromLSDDate, toLSDDate);

                        if (filter.isFiltered == true)
                        {
                            serviceCount = getSBNthData(managerList, 2, CREIds, serviceTypeName, typeOfPickup, searchPattern, 0, totalCount, instatus, fromLSDDate, toLSDDate);
                        }
                    }
                    else if (filter.getDataFor == 9)
                    {
                        string fromDateNew, toDateNew, campaignName, serviceTypeName, CREIds, fromLSDDate, toLSDDate, city, workshopid, lastDipo, droppedCount, typeOfPickup;

                        campaignName = filter.campaign == null ? "" : filter.campaign;
                        serviceTypeName = filter.serviceType == null ? "" : filter.serviceType;
                        CREIds = filter.CRES == null ? "" : filter.CRES;
                        fromDateNew = filter.fromDate == null ? "" : Convert.ToDateTime(filter.fromDate.ToString()).ToString("yyyy-MM-dd");
                        toDateNew = filter.toDate == null ? "" : Convert.ToDateTime(filter.toDate.ToString()).ToString("yyyy-MM-dd");
                        fromLSDDate = filter.fromSLDate == null ? "" : Convert.ToDateTime(filter.fromSLDate.ToString()).ToString("yyyy-MM-dd");
                        toLSDDate = filter.toSLDate == null ? "" : Convert.ToDateTime(filter.toSLDate.ToString()).ToString("yyyy-MM-dd");
                        city = filter.city == null ? "" : filter.city;
                        workshopid = filter.workshopLoc == null ? "" : filter.workshopLoc;
                        lastDipo = filter.lastDipo == null ? "" : filter.lastDipo;
                        droppedCount = filter.droppedCount == null ? "" : filter.droppedCount;
                        typeOfPickup = filter.typeOfPickup == null ? "" : filter.typeOfPickup;

                        totalCount = db.Database.SqlQuery<int>("call cremanagerServiceFutureFollowupCount(@increManager)",
 new MySqlParameter[] { new MySqlParameter("@increManager", managerList) }).FirstOrDefault();
                        fromIndex = start;
                        toIndex = length;
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }


                        service = getFutureFollowUp(managerList, CREIds, fromDateNew, toDateNew, campaignName, serviceTypeName, searchPattern, fromIndex, toIndex, fromLSDDate, toLSDDate);

                        if (filter.isFiltered == true)
                        {
                            serviceCount = getFutureFollowUp(managerList, CREIds, fromDateNew, toDateNew, campaignName, serviceTypeName, searchPattern, 0, totalCount, fromLSDDate, toLSDDate);
                        }
                    }

                }
                catch (Exception ex)
                {

                }
            }
            if (service != null)
            {
                if (filter.isFiltered == true)
                {
                    return Json(new { data = service, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = serviceCount.Count() }, JsonRequestBehavior.AllowGet);
                }
                else if (filter.isFiltered == false)
                {
                    return Json(new { data = service, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount }, JsonRequestBehavior.AllowGet);
                }

            }
            else if (serviceCount != null)
            {
                if (filter.isFiltered == true)
                {
                    return Json(new { data = serviceCount, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = serviceCount.Count() }, JsonRequestBehavior.AllowGet);
                }
                else if (filter.isFiltered == false)
                {
                    return Json(new { data = serviceCount, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount }, JsonRequestBehavior.AllowGet);
                }

            }

            return Json(new { data = "", draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0 }, JsonRequestBehavior.AllowGet);
        }
        /*************************** Calling Stored Procedure ***********************/

        public List<CallLogDispositionLoadReminderMR> assignedListOfUserMR(string fromDateNew, string toDateNew, string campaignName, string serviceTypeName, string searchPattern, string managerUsername, string CREIds, long fromIndex, long toIndex)
        {
            List<CallLogDispositionLoadReminderMR> serviceData = new List<CallLogDispositionLoadReminderMR>();

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    db.Database.CommandTimeout = 900;
                    string str = @"CALL cremanagerServiceReminderwithSearch(@instartdate,@inenddate, @incampaignname,@induetype,@pattern,@managerName,@CREIds,@start_with,@length)";
                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("instartdate", fromDateNew),
                        new MySqlParameter("inenddate", toDateNew),
                        new MySqlParameter("incampaignname", campaignName),
                        new MySqlParameter("induetype", serviceTypeName),
                        new MySqlParameter("pattern", searchPattern),
                        new MySqlParameter("managerName", managerUsername),
                        new MySqlParameter("CREIds", CREIds),
                        new MySqlParameter("start_with", fromIndex),
                        new MySqlParameter("length", toIndex),

                    };
                    serviceData = db.Database.SqlQuery<CallLogDispositionLoadReminderMR>(str, param).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return serviceData;
        }
        public List<CallLogDispositionLoadReminderMR> followUp(string fromDateNew, string toDateNew, string campaignName, string serviceTypeName, string pattern, string managerName, string userId, string CREIds, long typeOfDispo, long fromIndex, long toIndex, int orderDirection,string bookingstatus,string reasons, string fromLSDDate, string toLSDDate)
        {
            List<CallLogDispositionLoadReminderMR> serviceData = new List<CallLogDispositionLoadReminderMR>();

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    db.Database.CommandTimeout = 900;
                    string str = @"CALL cremanagerdispositionfilter(@instartdate,@inenddate,@incampaignname,@induetype,@pattern,@managerName,@CREIds,@dispositiontype,@start_with,@length,@orderDirection,@bookingstatus, @reasons)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("instartdate", fromDateNew),
                        new MySqlParameter("inenddate", toDateNew),
                        new MySqlParameter("incampaignname", campaignName),
                        new MySqlParameter("induetype", serviceTypeName),
                        new MySqlParameter("pattern", pattern),
                        new MySqlParameter("managerName", managerName),
                        new MySqlParameter("CREIds", CREIds),
                        new MySqlParameter("dispositiontype", typeOfDispo),
                        new MySqlParameter("start_with", fromIndex),
                        new MySqlParameter("length", toIndex),
                        new MySqlParameter("orderDirection", orderDirection),
                        new MySqlParameter("bookingstatus", bookingstatus),
                        new MySqlParameter("reasons", reasons)

                    };
                    serviceData = db.Database.SqlQuery<CallLogDispositionLoadReminderMR>(str, param).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return serviceData;
        }

        public List<CallLogDispositionLoadReminderMR> getnonContactsListOfUserMR(string fromDateNew, string toDateNew, string campaignName, string lastDispo, string droppedCount, string pattern, string managerName, string CREIds, long fromIndex, long toIndex, string fromLSDDate, string toLSDDate)
        {
            List<CallLogDispositionLoadReminderMR> serviceData = new List<CallLogDispositionLoadReminderMR>();

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    db.Database.CommandTimeout = 900;
                    string str = @"CALL cremanagerNonContactsSearch(@instartdate,@inenddate,@incampaignname,@inlastdispo,@droppedCount,@pattern,@managerName,@CREIds,@start_with,@length)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("instartdate", fromDateNew),
                        new MySqlParameter("inenddate", toDateNew),
                        new MySqlParameter("incampaignname", campaignName),
                        new MySqlParameter("inlastdispo", lastDispo),
                        new MySqlParameter("droppedCount", droppedCount),
                        new MySqlParameter("pattern", pattern),
                        new MySqlParameter("managerName", managerName),
                        new MySqlParameter("CREIds", CREIds),
                        new MySqlParameter("start_with", fromIndex),
                        new MySqlParameter("length", toIndex)

                    };
                    serviceData = db.Database.SqlQuery<CallLogDispositionLoadReminderMR>(str, param).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return serviceData;
        }
        public List<CallLogDispositionLoadReminderMR> getSBNthData(string increManager, long sbday, string inChasers_id, string serviceBookedType, string intypeofpickup,
            string pattern, long fromIndex, long toIndex,string instatus, string fromLSDDate, string toLSDDate)
        {
            List<CallLogDispositionLoadReminderMR> dispositionLoads = null;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    db.Database.CommandTimeout = 900;
                    string str = @"CALL ServiceBookingPreviousDayCallsCREManager(@increManager,@sbday,@inChasers_id,@inserviceBookedType,@intypeofpickup,@pattern,@startwith,@length,@fromlsddate,@tolsddate,@instatus)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("@increManager", increManager),
                        new MySqlParameter("@sbday", sbday),
                        new MySqlParameter("@inChasers_id", inChasers_id),
                        new MySqlParameter("@inserviceBookedType", serviceBookedType),
                        new MySqlParameter("@intypeofpickup", intypeofpickup),
                        new MySqlParameter("@pattern", pattern),
                        new MySqlParameter("@startwith", fromIndex),
                        new MySqlParameter("@length", toIndex),
                        new MySqlParameter("@fromlsddate", fromLSDDate),
                        new MySqlParameter("@tolsddate", toLSDDate),
                        new MySqlParameter("@instatus", instatus)
                    };

                    dispositionLoads = db.Database.SqlQuery<CallLogDispositionLoadReminderMR>(str, param).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return dispositionLoads;
        }

        public List<CallLogDispositionLoadReminderMR> getNonFsPms(string increManager, string increstartdate, string inenddate, string pattern, long fromIndex, long toIndex)
        {
            List<CallLogDispositionLoadReminderMR> dispositionLoads = null;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    db.Database.CommandTimeout = 900;
                    string str = @"CALL nonFS_nonPMS_vehiclereportedListsCremanager(@increManager,@increstartdate,@inenddate,@pattern,@startwith,@length)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("@increManager", increManager),
                        new MySqlParameter("@increstartdate", increstartdate),
                        new MySqlParameter("@inenddate", inenddate),
                        new MySqlParameter("@pattern", pattern),
                        new MySqlParameter("@startwith", fromIndex),
                        new MySqlParameter("@length", toIndex)
                    };

                    dispositionLoads = db.Database.SqlQuery<CallLogDispositionLoadReminderMR>(str, param).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return dispositionLoads;
        }


        public List<CallLogDispositionLoadReminderMR> getFutureFollowUp(string increManager, string inwyzuser, string instartdate, string inenddate, string incampaignname, string induetype,
         string pattern, long fromIndex, long toIndex, string fromLSDDate, string toLSDDate)
        {
            List<CallLogDispositionLoadReminderMR> dispositionLoads = null;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    db.Database.CommandTimeout = 900;
                    string str = @"CALL cremanagerServiceFutureFollowup(@increManager,@inwyzuser,@instartdate,@inenddate,@incampaignname,@induetype,@pattern,@startwith,@length)";
                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("@increManager", increManager),
                        new MySqlParameter("@inwyzuser", inwyzuser),
                        new MySqlParameter("@instartdate", instartdate),
                        new MySqlParameter("@inenddate", inenddate),
                        new MySqlParameter("@incampaignname", incampaignname),
                        new MySqlParameter("@induetype", induetype),
                        new MySqlParameter("@pattern", pattern),
                        new MySqlParameter("@startwith", fromIndex),
                        new MySqlParameter("@length", toIndex),
                    };

                    dispositionLoads = db.Database.SqlQuery<CallLogDispositionLoadReminderMR>(str, param).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return dispositionLoads;
        }

        /*********Others******************/
        public List<CallLogDispositionLoadReminderMR> getDataMissedIncominOutgoing(string managerName, string callType, string CREIds, string fromCallDate, string toCallDate, string searchPattern, long fromIndex, long toIndex)
        {
            List<CallLogDispositionLoadReminderMR> dispositionLoads = null;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    db.Database.CommandTimeout = 900;
                    string str = @"CALL callogMissedIncoming(@manager_username,@in_calltype,@inwyzuser_id,@fromcalldate,@tocalldate,@pattern,@startwith,@length)";
                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("@manager_username", managerName),
                        new MySqlParameter("@in_calltype", callType),
                        new MySqlParameter("@inwyzuser_id", CREIds),
                        new MySqlParameter("@fromcalldate", fromCallDate),
                        new MySqlParameter("@tocalldate", toCallDate),
                        new MySqlParameter("@pattern", searchPattern),
                        new MySqlParameter("@startwith", fromIndex),
                        new MySqlParameter("@length", toIndex)
                    };
                    dispositionLoads = db.Database.SqlQuery<CallLogDispositionLoadReminderMR>(str, param).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return dispositionLoads;

        }
        public int getDataMissedIncominOutgoingCount(string managerName, string callType, string UserId, string fromCallDate, string toCallDate, string searchPattern)
        {
            int totalCount = 0;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    db.Database.CommandTimeout = 900;
                    totalCount = db.Database.SqlQuery<int>("call callogMissedIncomingcount(@manager_username,@in_calltype,@inwyzuser_id,@fromcalldate,@tocalldate,@pattern)",
                             new MySqlParameter("@manager_username", managerName),
                             new MySqlParameter("@in_calltype", callType),
                             new MySqlParameter("@inwyzuser_id", UserId),
                             new MySqlParameter("@fromcalldate", fromCallDate),
                             new MySqlParameter("@tocalldate", toCallDate),
                             new MySqlParameter("@pattern", searchPattern)).FirstOrDefault();
                    return totalCount;

                }
            }
            catch (Exception ex)
            {

            }

            return totalCount;

        }




        /***********************************************************************/


        /*********************Ajax Calls***********************************/
        public ActionResult listWorkshops(int selectedCity)
        {
            //List<workshop> workshoplist = new List<workshop>();
            AutoSherDBContext dba = new AutoSherDBContext();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    var workshoplist = dba.workshops.Where(m => m.location_cityId == selectedCity).Select(m => new { id = m.id, workshopName = m.workshopName }).ToList();
                    //var data = JsonConvert.SerializeObject(workshoplist);

                    return Json(workshoplist);
                }

            }
            catch (Exception ex)
            {

            }
            return Json(new { workshoplist = new List<workshop>() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getCRESListBasedOnWorkshop(long? workshopId)
        {

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    //var  workshopListId = db.userworkshops.Where(x => x.workshopList_id == workshopId).Select(m=>m.userWorkshop_id).ToList();

                    //foreach(var workshopids in workshopListId)
                    //{
                    var creList = db.wyzusers.Where(m => m.workshop_id == workshopId && m.insuranceRole == false && m.role == "CRE" && m.unAvailable==false && m.role1 == "1").Select(m => new { id = m.id, creName = m.userName }).OrderBy(m => m.creName).ToList();
                    return Json(creList);

                    //}
                    // var creList = (from w in db.wyzusers join uw in db.userworkshops on w.workshop_id equals uw.userWorkshop_id  where uw.workshopList_id== workshopId && w.role == "CRE" && uw.userWorkshop_id==w.workshop_id select new { id = w.id,creName = w.userName}).ToList();

                    //var creList = db.wyzusers.Where(m => m.workshop_id == workshopListId && m.insuranceRole==false && m.role == "CRE" && m.role1=="1").Select(m => new { id = m.id, creName = m.userName }).OrderBy(m=>m.creName).ToList();
                    // var creList = db.wyzusers.Where(m => workshopListId.Contains(m.workshop_id)&&  m.role == "CRE" ).Select(m => new {  }).OrderBy(m=>m.creName).ToList();

                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { creList = new List<wyzuser>() }, JsonRequestBehavior.AllowGet);

        }
        /******************************************************************/
        public ActionResult getOtherBucket(string otherreminderData)
        {
            List<CallLogDispositionLoadReminderMR> service = null;
            List<CallLogDispositionLoadReminderMR> serviceCount = null;

            //Parameters of Paging..........
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchPattern = Request["search[value]"];
            int UserId = Convert.ToInt32(Session["UserId"].ToString());
            string managerUsername = Session["UserName"].ToString();


            otherFilter filter = new otherFilter();
            if (otherreminderData != null)
            {
                filter = JsonConvert.DeserializeObject<otherFilter>(otherreminderData);
            }

            long fromIndex, toIndex;
            int totalCount = 0;

            if (searchPattern != "")
            {
                filter.isFiltered = true;
            }
            using (var db = new AutoSherDBContext())
            {
                try
                {

                    if (filter.getDataFor == 1)
                    {
                        string fromDateNew, toDateNew, CREIds, city, workshopid;
                        CREIds = filter.CRES == null ? "" : filter.CRES;
                        fromDateNew = filter.fromDate == null ? "" : Convert.ToDateTime(filter.fromDate.ToString()).ToString("yyyy-MM-dd");
                        toDateNew = filter.toDate == null ? "" : Convert.ToDateTime(filter.toDate.ToString()).ToString("yyyy-MM-dd");
                        city = filter.city == null ? "" : filter.city;
                        workshopid = filter.workshopLoc == null ? "" : filter.workshopLoc;
                        string callType = "MISSED";

                        totalCount = getDataMissedIncominOutgoingCount(managerUsername, callType, CREIds, fromDateNew, toDateNew, searchPattern);

                        fromIndex = start;
                        toIndex = length;
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }

                        service = getDataMissedIncominOutgoing(managerUsername, callType, CREIds, fromDateNew, toDateNew, searchPattern, fromIndex, toIndex);

                        if (filter.isFiltered == true)
                        {
                            serviceCount = getDataMissedIncominOutgoing(managerUsername, callType, CREIds, fromDateNew, toDateNew, searchPattern, fromIndex, toIndex);
                        }
                    }
                    else if (filter.getDataFor == 2)
                    {
                        string fromDateNew, toDateNew, CREIds, city, workshopid;
                        CREIds = filter.CRES == null ? "" : filter.CRES;
                        fromDateNew = filter.fromDate == null ? "" : Convert.ToDateTime(filter.fromDate.ToString()).ToString("yyyy-MM-dd");
                        toDateNew = filter.toDate == null ? "" : Convert.ToDateTime(filter.toDate.ToString()).ToString("yyyy-MM-dd");
                        city = filter.city == null ? "" : filter.city;
                        workshopid = filter.workshopLoc == null ? "" : filter.workshopLoc;
                        string callType = "INCOMING";
                        fromDateNew = filter.fromDate == null ? "" : Convert.ToDateTime(filter.fromDate.ToString()).ToString("yyyy-MM-dd");
                        toDateNew = filter.toDate == null ? "" : Convert.ToDateTime(filter.toDate.ToString()).ToString("yyyy-MM-dd");

                        totalCount = getDataMissedIncominOutgoingCount(managerUsername, callType, CREIds, fromDateNew, toDateNew, searchPattern);

                        fromIndex = start;
                        toIndex = length;
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }

                        service = getDataMissedIncominOutgoing(managerUsername, callType, CREIds, fromDateNew, toDateNew, searchPattern, fromIndex, toIndex);

                        if (filter.isFiltered == true)
                        {
                            serviceCount = getDataMissedIncominOutgoing(managerUsername, callType, CREIds, fromDateNew, toDateNew, searchPattern, fromIndex, toIndex);
                        }
                    }
                    else if (filter.getDataFor == 3)
                    {
                        string fromDateNew, toDateNew, CREIds, city, workshopid;
                        CREIds = filter.CRES == null ? "" : filter.CRES;
                        fromDateNew = filter.fromDate == null ? "" : Convert.ToDateTime(filter.fromDate.ToString()).ToString("yyyy-MM-dd");
                        toDateNew = filter.toDate == null ? "" : Convert.ToDateTime(filter.toDate.ToString()).ToString("yyyy-MM-dd");
                        city = filter.city == null ? "" : filter.city;
                        workshopid = filter.workshopLoc == null ? "" : filter.workshopLoc;
                        string callType = "OUTGOING";
                        fromDateNew = filter.fromDate == null ? "" : Convert.ToDateTime(filter.fromDate.ToString()).ToString("yyyy-MM-dd");
                        toDateNew = filter.toDate == null ? "" : Convert.ToDateTime(filter.toDate.ToString()).ToString("yyyy-MM-dd");

                        totalCount = getDataMissedIncominOutgoingCount(managerUsername, callType, CREIds, fromDateNew, toDateNew, searchPattern);

                        fromIndex = start;
                        toIndex = length;
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }


                        service = getDataMissedIncominOutgoing(managerUsername, callType, CREIds, fromDateNew, toDateNew, searchPattern, fromIndex, toIndex);

                        if (filter.isFiltered == true)
                        {
                            serviceCount = getDataMissedIncominOutgoing(managerUsername, callType, CREIds, fromDateNew, toDateNew, searchPattern, fromIndex, toIndex);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            if (service != null)
            {
                if (filter.isFiltered == true)
                {
                    return Json(new { data = service, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = serviceCount.Count() }, JsonRequestBehavior.AllowGet);
                }
                else if (filter.isFiltered == false)
                {
                    return Json(new { data = service, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount }, JsonRequestBehavior.AllowGet);
                }

            }
            else if (serviceCount != null)
            {
                if (filter.isFiltered == true)
                {
                    return Json(new { data = serviceCount, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = serviceCount.Count() }, JsonRequestBehavior.AllowGet);
                }
                else if (filter.isFiltered == false)
                {
                    return Json(new { data = serviceCount, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { data = "", draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0 }, JsonRequestBehavior.AllowGet);
        }

    }
}