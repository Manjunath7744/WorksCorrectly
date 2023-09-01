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
    public class callLogReminderController : Controller
    {
        // GET: callLogReminder

        #region  Insurance Reminder Starts
        public ActionResult insuranceReminder()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    int UserId = Convert.ToInt32(Session["UserId"].ToString());

                    var ddlCampaign = db.campaigns.Where(m => m.campaignType == "Insurance" && m.isactive == true).Select(m => new { id = m.id, campaignName = m.campaignName }).ToList();
                    ViewBag.ddlCampaign = ddlCampaign;


                    var ddlCRE = db.wyzusers.Where(m => m.insuranceRole == true &&  m.role == "CRE" && m.unAvailable ==false ).Select(m => new { id = m.id, userName = m.userName }).ToList();
                    ViewBag.ddlCRE = ddlCRE;


                    var ddlmodelList = db.modelslists.Select(m => new { id = m.id, model = m.model }).ToList();
                    ViewBag.ddlmodelList = ddlmodelList;


                    var ddlsisposition = db.calldispositiondatas.Select(m => new { id = m.id, dispo = m.disposition }).ToList();
                    ViewBag.ddlsisposition = ddlsisposition;

                    var locationids = db.insuranceassignedinteractions.Where(m => m.assigned_manager_id == UserId).Select(m => m.location_id).ToList();
                    var ddllocation = db.locations.Where(m => locationids.Contains(m.cityId)).Select(m => new { id = m.cityId, location = m.name }).OrderBy(m => m.location).ToList();
                    ViewBag.ddllocation = ddllocation;


                    var renewalType = db.renewaltypes.Select(m => new { id = m.id, name = m.renewalTypeName }).OrderBy(m => m.name).ToList();
                    ViewBag.renewalType = renewalType;

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

        public ActionResult loadInsuranceDashboardCounts()
        {
            int FreshCallsInsCount, followupInsCount, nonContactsIns, overDueInsCount, totalRedFlag, attemptsIns, pendingIns, appointment, totalContactsInsCREperDay, totalCallsperDay;
            try
            {
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                string managerUsername = Session["UserName"].ToString();

                using (var db = new AutoSherDBContext())
                {
                    db.Database.CommandTimeout = 900;

                    FreshCallsInsCount = db.Database.SqlQuery<int>("call insuranceDashboardCountsCREManager(@incremanager,@indashboardId);", new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername), new MySqlParameter("@indashboardId", 1) }).FirstOrDefault();

                    followupInsCount = db.Database.SqlQuery<int>("call insuranceDashboardCountsCREManager(@incremanager,@indashboardId);", new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername), new MySqlParameter("@indashboardId", 2) }).FirstOrDefault();

                    nonContactsIns = db.Database.SqlQuery<int>("call insuranceDashboardCountsCREManager(@incremanager,@indashboardId);", new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername), new MySqlParameter("@indashboardId", 3) }).FirstOrDefault();

                    overDueInsCount = db.Database.SqlQuery<int>("call insuranceDashboardCountsCREManager(@incremanager,@indashboardId);", new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername), new MySqlParameter("@indashboardId", 4) }).FirstOrDefault();

                    totalRedFlag = db.Database.SqlQuery<int>("call insuranceDashboardCountsCREManager(@incremanager,@indashboardId);", new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername), new MySqlParameter("@indashboardId", 5) }).FirstOrDefault();

                    attemptsIns = db.Database.SqlQuery<int>("call insuranceDashboardCountsCREManager(@incremanager,@indashboardId);", new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername), new MySqlParameter("@indashboardId", 6) }).FirstOrDefault();

                    pendingIns = db.Database.SqlQuery<int>("call insuranceDashboardCountsCREManager(@incremanager,@indashboardId);", new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername), new MySqlParameter("@indashboardId", 7) }).FirstOrDefault();

                    appointment = db.Database.SqlQuery<int>("call insuranceDashboardCountsCREManager(@incremanager,@indashboardId);", new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername), new MySqlParameter("@indashboardId", 8) }).FirstOrDefault();

                    totalContactsInsCREperDay = db.Database.SqlQuery<int>("call insuranceDashboardCountsCREManager(@incremanager,@indashboardId);", new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername), new MySqlParameter("@indashboardId", 9) }).FirstOrDefault();

                    totalCallsperDay = db.Database.SqlQuery<int>("call insuranceDashboardCountsCREManager(@incremanager,@indashboardId);", new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername), new MySqlParameter("@indashboardId", 10) }).FirstOrDefault();
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

            return Json(new { success = true, FreshCallsInsCount, followupInsCount, nonContactsIns, overDueInsCount, totalRedFlag, attemptsIns, pendingIns, appointment, totalContactsInsCREperDay, totalCallsperDay });
        }

        public ActionResult getInsuranceBucket(string insurancereminderData)
        {
            List<CallLogDispositionLoadReminderMR> Insurancedetails = null;
            List<CallLogDispositionLoadReminderMR> InsurancedetailsCounts = null;



            string expfromDate, exptoDate, campaignName, CREIds, appfromDate, apptoDate, flag, callfromDate, calltoDate, attempt, models, reason, appType, location;


            //Parameters of Paging..........
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchPattern = Request["search[value]"];
            int UserId = Convert.ToInt32(Session["UserId"].ToString());
            string managerUsername = Session["UserName"].ToString();
            string exception = "";

            insuranceFilter filter = new insuranceFilter();
            if (insurancereminderData != null)
            {
                filter = JsonConvert.DeserializeObject<insuranceFilter>(insurancereminderData);
            }
            if (searchPattern != "")
            {
                filter.isFiltered = true;
            }

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
            location = filter.LocationId == null ? "" : filter.LocationId;

            long fromIndex, toIndex;
            long totalCount = 0;
            long totalFilteredCount = 0;
            long patternCount = 0;

            using (var db = new AutoSherDBContext())
            {
                try
                {

                    if (filter.getDataFor == 1)
                    {
                        if(campaignName=="" && expfromDate=="" && exptoDate=="" && flag=="" && CREIds=="" && searchPattern=="")
                        {
                            filter.isFiltered = false;
                        }

                        totalCount = InsuranceassignedListOfUserMRCount(managerUsername, CREIds, campaignName, expfromDate, exptoDate, flag, models, searchPattern);
                        if (filter.isFiltered == true)
                        {
                            totalFilteredCount = InsuranceassignedListOfUserMRCount(managerUsername, "", "", "", "", "", "", "");
                        }

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
                        Insurancedetails = InsuranceassignedListOfUserMR(managerUsername, CREIds, campaignName, expfromDate, exptoDate, flag, models, searchPattern, fromIndex, toIndex);

                    }
                    else if (filter.getDataFor == 2)
                    {
                        if (campaignName == "" && expfromDate == "" && exptoDate == "" && flag == "" && CREIds == "" && searchPattern == "")
                        {
                            filter.isFiltered = false;
                        }
                        long dispoType = 4;

                        totalCount = getInsurancefollowUpCallLogTableDataMRCount(managerUsername, CREIds, expfromDate, exptoDate, campaignName, appfromDate, apptoDate, callfromDate, calltoDate, appType, models, flag, reason, searchPattern, dispoType);
                        if (filter.isFiltered == true)
                        {
                            totalFilteredCount = getInsurancefollowUpCallLogTableDataMRCount(managerUsername, "", "", "", "", "", "", "", "", "", "", "", "", "", dispoType);
                        }
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
                        Insurancedetails = getInsurancefollowUpCallLogTableDataMR(managerUsername, CREIds, expfromDate, exptoDate, campaignName, appfromDate, apptoDate, callfromDate, calltoDate, appType, models, flag, reason, searchPattern, dispoType, fromIndex, toIndex);


                    }
                    else if (filter.getDataFor == 3)
                    {
                        if (expfromDate == "" && exptoDate == "" && flag == "" && CREIds == "" && searchPattern == "" && appType=="" && appfromDate=="" && apptoDate=="")
                        {
                            filter.isFiltered = false;
                        }

                        long dispoType = 25;

                        totalCount = getInsurancefollowUpCallLogTableDataMRCount(managerUsername, CREIds, expfromDate, exptoDate, campaignName, appfromDate, apptoDate, callfromDate, calltoDate, appType, models, flag, reason, searchPattern, dispoType);
                        if (filter.isFiltered == true)
                        {
                            totalFilteredCount = getInsurancefollowUpCallLogTableDataMRCount(managerUsername, "", "", "", "", "", "", "", "", "", "", "", "", "", dispoType);
                        }
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
                        Insurancedetails = getInsurancefollowUpCallLogTableDataMR(managerUsername, CREIds, expfromDate, exptoDate, campaignName, appfromDate, apptoDate, callfromDate, calltoDate, appType, models, flag, reason, searchPattern, dispoType, fromIndex, toIndex);
                    }
                    else if (filter.getDataFor == 4)
                    {
                        if (campaignName == "" && expfromDate == "" && exptoDate == "" && flag == "" && reason == "" && searchPattern == "")
                        {
                            filter.isFiltered = false;
                        }
                        long dispoType = 26;

                        totalCount = getInsurancefollowUpCallLogTableDataMRCount(managerUsername, CREIds, expfromDate, exptoDate, campaignName, appfromDate, apptoDate, callfromDate, calltoDate, appType, models, flag, reason, searchPattern, dispoType);
                        if (filter.isFiltered == true)
                        {
                            totalFilteredCount = getInsurancefollowUpCallLogTableDataMRCount(managerUsername, "", "", "", "", "", "", "", "", "", "", "", "", "", dispoType);
                        }
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
                        Insurancedetails = getInsurancefollowUpCallLogTableDataMR(managerUsername, CREIds, expfromDate, exptoDate, campaignName, appfromDate, apptoDate, callfromDate, calltoDate, appType, models, flag, reason, searchPattern, dispoType, fromIndex, toIndex);

                    }
                    else if (filter.getDataFor == 5)
                    {
                        if (campaignName == "" && expfromDate == "" && exptoDate == "" && flag == "" && CREIds == "" && searchPattern == "" && callfromDate=="" && calltoDate=="" && attempt=="")
                        {
                            filter.isFiltered = false;
                        }
                        long dispoType = 1;

                        totalCount = getInsurancenonContactsServerDataTableMRCount(managerUsername, searchPattern, expfromDate, exptoDate, campaignName, callfromDate, calltoDate, flag, attempt, CREIds, dispoType);
                        if (filter.isFiltered == true)
                        {
                            totalFilteredCount = getInsurancenonContactsServerDataTableMRCount(managerUsername, "", "", "", "", "", "", "", "", "", dispoType);
                        }

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
                        Insurancedetails = getInsurancenonContactsServerDataTableMR(managerUsername, CREIds, searchPattern, expfromDate, exptoDate, campaignName, callfromDate, calltoDate, flag, attempt, dispoType, fromIndex, toIndex);

                    }
                    else if (filter.getDataFor == 6)
                    {

                        if ( expfromDate == "" && exptoDate == "" && flag == "" && CREIds == "" && searchPattern == "" && appType=="")
                        {
                            filter.isFiltered = false;
                        }

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
                        InsurancedetailsCounts = getInsurancePreviousDayServerDataTableMR(managerUsername, "1", CREIds, searchPattern, expfromDate, exptoDate, appType, flag, fromIndex, toIndex);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getInsurancePreviousDayServerDataTableMR(managerUsername, "1", CREIds, searchPattern, expfromDate, exptoDate, appType, flag, 0, totalCount).Count;
                        }
                    }
                    else if (filter.getDataFor == 7)
                    {
                        if (expfromDate == "" && exptoDate == "" && flag == "" && CREIds == "" && searchPattern == "" && appType == "")
                        {
                            filter.isFiltered = false;
                        }

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
                        InsurancedetailsCounts = getInsurancePreviousDayServerDataTableMR(managerUsername, "2", CREIds, searchPattern, expfromDate, exptoDate, appType, flag, fromIndex, toIndex);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getInsurancePreviousDayServerDataTableMR(managerUsername, "2", CREIds, searchPattern, expfromDate, exptoDate, appType, flag, 0, totalCount).Count;
                        }
                    }
                    else if (filter.getDataFor == 8)
                    {

                        if (campaignName=="" && expfromDate == "" && exptoDate == "" && flag == "" && CREIds == "" && searchPattern == "" && appType == "")
                        {
                            filter.isFiltered = false;
                        }

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
                        InsurancedetailsCounts = getInsuranceFuturefollowUpDataMR(managerUsername, CREIds, expfromDate, exptoDate, campaignName, models, flag, searchPattern, fromIndex, toIndex);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getInsuranceFuturefollowUpDataMR(managerUsername, CREIds, expfromDate, exptoDate, campaignName, models, flag, searchPattern, 0, totalCount).Count;
                        }
                    }
                    else if (filter.getDataFor == 9)
                    {

                        if (campaignName == "" && expfromDate == "" && exptoDate == "" && flag == "" && CREIds == "" && searchPattern == "" && appType == "" && location=="" && callfromDate=="" && calltoDate=="" && attempt=="")
                        {
                            filter.isFiltered = false;
                        }

                        totalCount = getpaidBucketCount("", "", "", "", "", "", "", "", managerUsername, "", "", "");
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
                        InsurancedetailsCounts = getPaid(searchPattern, expfromDate, exptoDate, campaignName, callfromDate, calltoDate, flag, attempt, managerUsername, CREIds, 1, location, "", fromIndex, toIndex);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getPaid(searchPattern, expfromDate, exptoDate, campaignName, callfromDate, calltoDate, flag, attempt, managerUsername, CREIds, 1, location,"", 0,totalCount).Count;
                        }
                    }
                    else if (filter.getDataFor == 10)
                    {

                        if (campaignName == "" && expfromDate == "" && exptoDate == "" && flag == "" && CREIds == "" && searchPattern == "" && appType == "" && location == "" && callfromDate == "" && calltoDate == "" && attempt == "")
                        {
                            filter.isFiltered = false;
                        }

                        totalCount = getotherBucketCount("", "", "", "", "", "", "", "", managerUsername, "", "", "");

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
                        InsurancedetailsCounts = getOthers(searchPattern, expfromDate, exptoDate, campaignName, callfromDate, calltoDate, flag, attempt, managerUsername, CREIds,1,location, "",fromIndex,toIndex);
                            getInsuranceFuturefollowUpDataMR(managerUsername, CREIds, expfromDate, exptoDate, campaignName, models, flag, searchPattern, fromIndex, toIndex);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getOthers(searchPattern, expfromDate, exptoDate, campaignName, callfromDate, calltoDate, flag, attempt, managerUsername, CREIds,1, location, "", 0, totalCount).Count;
                        }
                    }
                    else if (filter.getDataFor == 11)
                    {

                        if (campaignName == "" && expfromDate == "" && exptoDate == "" && flag == "" && CREIds == "" && searchPattern == "" && appType == "" && location == "" )
                        {
                            filter.isFiltered = false;
                        }

                        totalCount = getreceiptdBucketCount(managerUsername,"");

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
                        InsurancedetailsCounts = getreceiptCall(managerUsername,CREIds,campaignName,expfromDate,exptoDate,flag,"",searchPattern, location, "", fromIndex,toIndex);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getreceiptCall(managerUsername, CREIds, campaignName, expfromDate, exptoDate, flag, "", searchPattern, location, "", 0, totalCount).Count;
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
            if (Insurancedetails != null)
            {
                if (filter.isFiltered == true)
                {

                    var JsonData = Json(new { data = Insurancedetails, draw = Request["draw"], recordsTotal = totalFilteredCount, recordsFiltered = totalCount, exception = exception });
                    JsonData.MaxJsonLength = int.MaxValue;
                    return JsonData;
                }
                else
                {
                    var JsonData = Json(new { data = Insurancedetails, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount, exception = exception });
                    JsonData.MaxJsonLength = int.MaxValue;
                    return JsonData;
                }

            }
            if(InsurancedetailsCounts!=null)
            {
                if (filter.isFiltered == true)
                {
                    var JsonData = Json(new { data = InsurancedetailsCounts, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = patternCount, exception = exception }, JsonRequestBehavior.AllowGet);
                    JsonData.MaxJsonLength = int.MaxValue;
                    return JsonData;
                }
                else if (filter.isFiltered == false)
                {
                    var JsonData = Json(new { data = InsurancedetailsCounts, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount, exception = exception }, JsonRequestBehavior.AllowGet);
                    JsonData.MaxJsonLength = int.MaxValue;
                    return JsonData;
                }
            }

            return Json(new { data = "", draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0, exception = exception });
        }


        public long InsuranceassignedListOfUserMRCount(string managerUsername, string CREIds, string campaignName, string expfromDate, string exptoDate, string flag, string models, string searchPattern)
        {
            long totalCount = 0;
            //try
            //{
            using (var db = new AutoSherDBContext())
            {
                totalCount = db.Database.SqlQuery<int>("call cremanagerinsuranceScheduledCallsCount(@cremanagerid,@wyzuserid,@in_campaign_id,@instartdate,@inenddate,@in_flag,@in_model,@pattern)",
                          new MySqlParameter("@cremanagerid", managerUsername),
                          new MySqlParameter("@wyzuserid", CREIds),
                          new MySqlParameter("@in_campaign_id", campaignName),
                          new MySqlParameter("@instartdate", expfromDate),
                          new MySqlParameter("@inenddate", exptoDate),
                          new MySqlParameter("@in_flag", flag),
                          new MySqlParameter("@in_model", models),
                      new MySqlParameter("@pattern", searchPattern)).FirstOrDefault();

                return totalCount;
            }
            //}
            //catch (Exception ex)
            //{

            //}
            return totalCount;
        }
        public List<CallLogDispositionLoadReminderMR> InsuranceassignedListOfUserMR(string userLoginName, string crename, string campaignName, string fromexpirydaterange, string toexpirydaterange, string flag, string modelName, string searchPattern, long fromIndex, long toIndex)
        {
            List<CallLogDispositionLoadReminderMR> dispositionLoads = null;
            //try
            //{
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
            // }
            //catch (Exception ex)
            //{

            //}

            return dispositionLoads;
        }

        public long getInsurancefollowUpCallLogTableDataMRCount(string username, string crename, string fromexpirydaterange, string toexpirydaterange, string campaignName, string appointmentFromDate, string appointmentToDate, string callFromDate, string callToDate, string appointmentType, string modelName, string flag, string reasons, string searchPattern, long buckettype)
        {
            long totalCount = 0;
            //try
            //{
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
            //}
            //catch (Exception ex)
            //{

            //}
            return totalCount;
        }

        public List<CallLogDispositionLoadReminderMR> getInsurancefollowUpCallLogTableDataMR(string username, string crename, string fromexpirydaterange, string toexpirydaterange, string campaignName, string appointmentFromDate, string appointmentToDate, string callFromDate, string callToDate, string appointmentType, string modelName, string flag, string reasons, string searchPattern, long buckettype, long fromIndex, long toIndex)
        {
            List<CallLogDispositionLoadReminderMR> dispositionLoads = null;
            //try
            //{
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
            //}
            //catch (Exception ex)
            //{

            //}

            return dispositionLoads;


        }


        public long getInsurancenonContactsServerDataTableMRCount(string managerUsername, string searchPattern, string expfromDate, string exptoDate, string campaignName, string callfromDate, string calltoDate, string flag, string attempt, string CREIds, long dispoType)
        {

            long totalCount = 0;
            using (var db = new AutoSherDBContext())
            {
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

                return totalCount;
            }

        }

        public List<CallLogDispositionLoadReminderMR> getInsurancenonContactsServerDataTableMR(string username, string crename, string searchPattern, string fromexpirydaterange, string toexpirydaterange, string campaignName, string callFromDate, string callToDate, string flag, string attempts, long buckettype, long fromIndex, long toIndex)
        {
            List<CallLogDispositionLoadReminderMR> dispositionLoads = null;
            //try
            //{
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
            // }
            //catch (Exception ex)
            //{

            //}

            return dispositionLoads;

        }

        public long getInsurancePreviousDayServerDataTableMRCount(string incremanager, string dispo)
        {
            long totalCount = 0;
            //try
            //{
            using (var db = new AutoSherDBContext())
            {
                totalCount = db.Database.SqlQuery<int>("call cremanagerInsurancePreviousDayCount(@sbday,@incremanager)",
                      new MySqlParameter("@sbday", dispo),
                  new MySqlParameter("@incremanager", incremanager)).FirstOrDefault();

                return totalCount;
            }
            //}
            //catch (Exception ex)
            //{

            //}
            return totalCount;

        }
        public List<CallLogDispositionLoadReminderMR> getInsurancePreviousDayServerDataTableMR(string incremanager, string dispo, string crename, string searchPattern, string fromexpirydaterange, string toexpirydaterange, string appointmentType, string flag, long fromIndex, long toIndex)
        {
            List<CallLogDispositionLoadReminderMR> dispositionLoads = null;
            //try
            //{
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
            //}
            //catch (Exception ex)
            //{

            //}

            return dispositionLoads;

        }

        public long getInsuranceFuturefollowUpDataMRCount(string incremanager)
        {
            long totalCount = 0;
            //try
            //{
            using (var db = new AutoSherDBContext())
            {
                totalCount = db.Database.SqlQuery<int>("call cremanagerInsuranceFutureFollowupCount(@incremanager)",
                  new MySqlParameter("@incremanager", incremanager)).FirstOrDefault();

                return totalCount;
            }
            //}
            //catch (Exception ex)
            //{

            //}
            return totalCount;


        }

        public List<CallLogDispositionLoadReminderMR> getInsuranceFuturefollowUpDataMR(string incremanager, string crename, string fromexpirydaterange, string toexpirydaterange, string campaignName, string modelName, string flag, string searchPattern, long fromIndex, long toIndex)
        {
            List<CallLogDispositionLoadReminderMR> dispositionLoads = null;
            //try
            //{
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
            //}
            //catch (Exception ex)
            //{

            //}

            return dispositionLoads;


        }

        #region Paid Bucket Details
        public int getpaidBucketCount(string searchPattern, string ExpiryfromDate, string ExpirytoDate, string campaignName, string callFromDate, string callToDate, string flag, string attempts,string cremanagerid, string UserId, string locId, string renewaltype)
        {
            int totalCount = 0;
            using (var db = new AutoSherDBContext())
            {
                totalCount = db.Database.SqlQuery<int>("call cremanagerInsurancefilterforPaidCount(@pattern,@instartdate,@inenddate,@incampaignname,@incallFromDate,@incallToDate,@inflag,@inattempts,@cremanagerid,@wyzuserid,@dispositiontype,@locId,@inrenType)",
                            new MySqlParameter("@pattern", searchPattern),
                            new MySqlParameter("@instartdate", ExpiryfromDate),
                            new MySqlParameter("@inenddate", ExpirytoDate),
                            new MySqlParameter("@incampaignname", campaignName),
                            new MySqlParameter("@incallFromDate", callFromDate),
                            new MySqlParameter("@incallToDate", callToDate),
                            new MySqlParameter("@inflag", flag),
                            new MySqlParameter("@inattempts", attempts),
                            new MySqlParameter("@cremanagerid", cremanagerid),
                            new MySqlParameter("@wyzuserid", UserId),
                            new MySqlParameter("@dispositiontype", 1),
                            new MySqlParameter("@locId", locId),
                            new MySqlParameter("@inrenType", renewaltype)).FirstOrDefault();
                return totalCount;
            }
        }

        public List<CallLogDispositionLoadReminderMR> getPaid(string pattern, string instartdate, string inenddate, string incampaignname, string incallFromDate, string incallToDate, string inflag, string inattempts, string cremanagerid, string wyzuserid, long dispositiontype, string locId, string inrenType, long start_with, long length)
        {
            List<CallLogDispositionLoadReminderMR> dispositionLoadInsurances = new List<CallLogDispositionLoadReminderMR>();
            using (var db = new AutoSherDBContext())
            {
                string str = @"CALL cremanagerInsurancefilterforPaid(@pattern,@instartdate, @inenddate,@incampaignname,@incallFromDate,@incallToDate,@inflag,@inattempts,@cremanagerid,@wyzuserid, @dispositiontype,@locId,@inrenType,@start_with,@length);";

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
                        new MySqlParameter("@cremanagerid", cremanagerid),
                        new MySqlParameter("wyzuserid", wyzuserid),
                        new MySqlParameter("dispositiontype", dispositiontype),
                        new MySqlParameter("locId", locId),
                        new MySqlParameter("inrenType", inrenType),
                        new MySqlParameter("start_with", start_with),
                        new MySqlParameter("length", length)
            };
                dispositionLoadInsurances = db.Database.SqlQuery<CallLogDispositionLoadReminderMR>(str, sqlParameter).ToList();
            }

            return dispositionLoadInsurances;
        }
        #endregion

        #region OtherBucket Details

        public int getotherBucketCount(string searchPattern, string ExpiryfromDate, string ExpirytoDate, string campaignName, string callFromDate, string callToDate, string flag, string attempts, string incremanager, string crename, string locId, string renewaltype)
        {
            int totalCount = 0;
            using (var db = new AutoSherDBContext())
            {
                totalCount = db.Database.SqlQuery<int>("call cremanagerInsurancefilterforOthersCount(@pattern,@instartdate,@inenddate,@incampaignname,@incallFromDate,@incallToDate,@inflag,@inattempts,@incremanager,@wyzuserid,@dispositiontype,@locId,@inrenType)",
                           new MySqlParameter("@pattern", searchPattern),
                           new MySqlParameter("@instartdate", ExpiryfromDate),
                           new MySqlParameter("@inenddate", ExpirytoDate),
                           new MySqlParameter("@incampaignname", campaignName),
                           new MySqlParameter("@incallFromDate", callFromDate),
                           new MySqlParameter("@incallToDate", callToDate),
                           new MySqlParameter("@inflag", flag),
                           new MySqlParameter("@inattempts", attempts),
                           new MySqlParameter("@incremanager", incremanager),
                           new MySqlParameter("@wyzuserid", crename),
                           new MySqlParameter("@dispositiontype", 1),
                           new MySqlParameter("@locId", locId),
                           new MySqlParameter("@inrenType", renewaltype)).FirstOrDefault();
                return totalCount;
            }
        }

        public List<CallLogDispositionLoadReminderMR> getOthers(string pattern, string instartdate, string inenddate, string incampaignname, string incallFromDate, string incallToDate, string inflag, string inattempts, string incremanager, string crename, long dispositiontype, string locId, string inrenType, long start_with, long length)
        {
            List<CallLogDispositionLoadReminderMR> dispositionLoadInsurances = new List<CallLogDispositionLoadReminderMR>();
            using (var db = new AutoSherDBContext())
            {
                string str = @"CALL cremanagerInsurancefilterforOthers(@pattern,@instartdate, @inenddate,@incampaignname,@incallFromDate,@incallToDate,@inflag,@inattempts,@incremanager,@wyzuserid, @dispositiontype,@locId,@inrenType,@start_with,@length);";

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
                        new MySqlParameter("incremanager", incremanager),
                        new MySqlParameter("wyzuserid", crename),
                        new MySqlParameter("dispositiontype", dispositiontype),
                        new MySqlParameter("locId", locId),
                        new MySqlParameter("inrenType", inrenType),
                        new MySqlParameter("start_with", start_with),
                        new MySqlParameter("length", length)
            };
                dispositionLoadInsurances = db.Database.SqlQuery<CallLogDispositionLoadReminderMR>(str, sqlParameter).ToList();
            }

            return dispositionLoadInsurances;
        }
        #endregion

        #region Receipt Bucket Details
        public int getreceiptdBucketCount(string cremanagerid, string creName)
        {
            int totalCount = 0;
            using (var db = new AutoSherDBContext())
            {
                totalCount = db.Database.SqlQuery<int>("call cremanagerInsurancefilterReceiptCount(@cremanagerid,@wyzuserid)",
                            new MySqlParameter("@cremanagerid", cremanagerid),new MySqlParameter("@wyzuserid", creName)).FirstOrDefault();
                return totalCount;
            }
        }

        public List<CallLogDispositionLoadReminderMR> getreceiptCall(string cremanagerid,string creName, string campaignName, string fromexpirydaterange, string toexpirydaterange, string flag, string modelName, string searchPattern, string locId, string renewalType, long fromIndex, long toIndex)
        {
            List<CallLogDispositionLoadReminderMR> callLogsInsurence = null;

            using (var db = new AutoSherDBContext())
            {
                string str = @"CALL cremanagerInsurancefilterReceipt(@incremanager,@wyzuserid,@in_campaign_id,@instartdate,@inenddate,@in_flag,@in_model,@pattern,@locId,@inrenType,@start_with,@length);";
                MySqlParameter[] sqlParameter = new MySqlParameter[]
                {
                        new MySqlParameter("incremanager", cremanagerid),
                        new MySqlParameter("wyzuserid", creName),
                        new MySqlParameter("in_campaign_id", campaignName),
                        new MySqlParameter("instartdate", fromexpirydaterange),
                        new MySqlParameter("inenddate", toexpirydaterange),
                        new MySqlParameter("in_flag", flag),
                        new MySqlParameter("in_model", modelName),
                        new MySqlParameter("pattern", searchPattern),
                        new MySqlParameter("locId", locId),
                        new MySqlParameter("inrenType", renewalType),
                        new MySqlParameter("start_with", fromIndex),
                        new MySqlParameter("length", toIndex)
                };
                callLogsInsurence = db.Database.SqlQuery<CallLogDispositionLoadReminderMR>(str, sqlParameter).ToList();
            }
            return callLogsInsurence;
        }
        #endregion

        #endregion Insurance Reminder Ends

        #region Service Reminder Starts
        [HttpGet, ActionName("serviceReminder")]
        public ActionResult serviceReminder()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {

                    var ddllocation = db.locations.Select(m => new { id = m.cityId, location = m.name }).OrderBy(m => m.location).ToList();
                    ViewBag.ddllocation = ddllocation;


                    var ddlCampaign = db.campaigns.Where(m => m.campaignType != "Insurance" && m.isactive==true).Select(m => new { id = m.id, campaignName = m.campaignName }).ToList();
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
        public ActionResult loadServiceDashboardCounts()
        {
            int countCREMFreshCalls, countCREMFollowups, countCREMNonContacts, countCREMoverDueBookings, countCREMtotalCalls, countCREMbookings, countCREMcontacts;
            try
            {
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                string managerUsername = Session["UserName"].ToString();

                using (var db = new AutoSherDBContext())
                {
                    db.Database.CommandTimeout = 900;
                    countCREMFreshCalls = db.Database.SqlQuery<int>("call CreMtotalFreshCalls(@wyzUserId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", managerUsername) }).FirstOrDefault();
                    countCREMFollowups = db.Database.SqlQuery<int>("call CreMTotalfollowups(@mgrUsername);", new MySqlParameter[] { new MySqlParameter("@mgrUsername", managerUsername) }).FirstOrDefault();
                    countCREMNonContacts = db.Database.SqlQuery<int>("call CreMTotalNoncontacts(@mgrUsername);", new MySqlParameter[] { new MySqlParameter("@mgrUsername", managerUsername) }).FirstOrDefault();
                    countCREMoverDueBookings = db.Database.SqlQuery<int>("call CreMTotaloverDueBookings(@mgrUsername);", new MySqlParameter[] { new MySqlParameter("@mgrUsername", managerUsername) }).FirstOrDefault();
                    countCREMtotalCalls = db.Database.SqlQuery<int>("call CreMTotalCallstoday(@mgrUsername);", new MySqlParameter[] { new MySqlParameter("@mgrUsername", managerUsername) }).FirstOrDefault();
                    countCREMbookings = db.Database.SqlQuery<int>("call CreMTotalBookings(@mgrUsername);", new MySqlParameter[] { new MySqlParameter("@mgrUsername", managerUsername) }).FirstOrDefault();
                    countCREMcontacts = db.Database.SqlQuery<int>("call CreMTotalContactstoday(@mgrUsername);", new MySqlParameter[] { new MySqlParameter("@mgrUsername", managerUsername) }).FirstOrDefault();

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

            //Parameters of Paging..........
            int fromIndex = Convert.ToInt32(Request["start"]);
            int toIndex = Convert.ToInt32(Request["length"]);
            string searchPattern = Request["search[value]"];
            int UserId = Convert.ToInt32(Session["UserId"].ToString());
            string UserName = Session["UserName"].ToString();
            string fromDateNew, toDateNew, campaignName, serviceTypeName, CREIds, fromLSDDate, toLSDDate, city, workshopid, bookingstatus, reasons, lastDipo, droppedCount, typeOfPickup, instatus, serviceBookedType;

            string exception = "";

            serviceFilter filter = new serviceFilter();
            if (reminderData != null)
            {
                filter = JsonConvert.DeserializeObject<serviceFilter>(reminderData);
            }
            if (searchPattern != "")
            {
                filter.isFiltered = true;
            }

            campaignName = filter.campaign == null ? "" : filter.campaign;
            serviceTypeName = filter.serviceType == null ? "" : filter.serviceType;
            serviceBookedType = filter.serviceBookedType == null ? "" : filter.serviceBookedType;
            CREIds = filter.CRES == null ? "" : filter.CRES;
            fromDateNew = filter.fromDate == null ? "" : Convert.ToDateTime(filter.fromDate.ToString()).ToString("yyyy-MM-dd");
            toDateNew = filter.toDate == null ? "" : Convert.ToDateTime(filter.toDate.ToString()).ToString("yyyy-MM-dd");
            fromLSDDate = filter.fromSLDate == null ? "" : Convert.ToDateTime(filter.fromSLDate.ToString()).ToString("yyyy-MM-dd");
            toLSDDate = filter.toSLDate == null ? "" : Convert.ToDateTime(filter.toSLDate.ToString()).ToString("yyyy-MM-dd");
            city = filter.city == null ? "" : filter.city;
            workshopid = filter.workshopLoc == null ? "" : filter.workshopLoc;

            bookingstatus = filter.bookingstatus == null ? "" : filter.bookingstatus;
            reasons = filter.reasons == null ? "" : filter.reasons;
            lastDipo = filter.lastDipo == null ? "" : filter.lastDipo;
            droppedCount = filter.droppedCount == null ? "" : filter.droppedCount;

            typeOfPickup = filter.typeOfPickup == null ? "" : filter.typeOfPickup;
            instatus = filter.bookingstatus == null ? "" : filter.bookingstatus;

            long patternCount = 0;
            int totalCount = 0;

            
            using (var db = new AutoSherDBContext())
            {
                db.Database.CommandTimeout = 900;
                try
                {
                    string user = Session["UserId"].ToString();

                    string managerUsername = Session["UserName"].ToString();

                    if (filter.getDataFor == 1)
                    {
                        if(campaignName=="" && CREIds=="" &&serviceTypeName=="" && fromDateNew=="" && toDateNew=="" && searchPattern=="")
                        {
                            filter.isFiltered = false;
                        }
                        totalCount = db.Database.SqlQuery<int>("call cremanagerServiceReminderCount(@pattern,@mgr_user_id,@users)",new MySqlParameter("@pattern", ""),new MySqlParameter("@mgr_user_id", managerUsername),new MySqlParameter("@users", "")).FirstOrDefault();

                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }
                        service = assignedListOfUserMR(fromDateNew, toDateNew, campaignName, serviceTypeName, searchPattern, managerUsername, CREIds, fromIndex, toIndex);

                        if (filter.isFiltered == true)
                        {
                            patternCount = assignedListOfUserMR(fromDateNew, toDateNew, campaignName, serviceTypeName, searchPattern, managerUsername, CREIds, 0, totalCount).Count;
                        }
                    }
                    else if (filter.getDataFor == 2)
                    {
                        if (campaignName == "" && CREIds == "" && serviceTypeName == "" && fromDateNew == "" && toDateNew == "" && searchPattern == "")
                        {
                            filter.isFiltered = false;
                        }
                        long typeOfDispo = 4;
                        int orderDirection = 1;

                        totalCount = serviceBucketDataCount(managerUsername, "", "", typeOfDispo);

                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }

                        service = followUp(fromDateNew, toDateNew, campaignName, serviceTypeName, searchPattern, managerUsername, user, CREIds, typeOfDispo, fromIndex, toIndex, orderDirection, bookingstatus, reasons, fromLSDDate, toLSDDate);

                        if (filter.isFiltered == true)
                        {
                            patternCount = followUp(fromDateNew, toDateNew, campaignName, serviceTypeName, searchPattern, managerUsername, user, CREIds, typeOfDispo, 0, totalCount, orderDirection, bookingstatus, reasons, fromLSDDate, toLSDDate).Count();
                        }
                    }
                    else if (filter.getDataFor == 3)
                    {
                        if (campaignName == "" && CREIds == "" && serviceTypeName == "" && fromDateNew == "" && toDateNew == "" && searchPattern == "" && serviceBookedType=="" && bookingstatus=="")
                        {
                            filter.isFiltered = false;
                        }
                        long typeOfDispo = 3;
                        int orderDirection = 1;
                        totalCount = serviceBucketDataCount(managerUsername, "", "", typeOfDispo);
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }

                        service = followUp(fromDateNew, toDateNew, campaignName, serviceBookedType, searchPattern, managerUsername, user, CREIds, typeOfDispo, fromIndex, toIndex, orderDirection, bookingstatus, reasons, fromLSDDate, toLSDDate);

                        if (filter.isFiltered == true)
                        {
                            patternCount = followUp(fromDateNew, toDateNew, campaignName, serviceBookedType, searchPattern, managerUsername, user, CREIds, typeOfDispo, 0, totalCount, orderDirection, bookingstatus, reasons, fromLSDDate, toLSDDate).Count();
                        }
                    }
                    else if (filter.getDataFor == 4)
                    {
                        if (campaignName == "" && CREIds == "" && reasons == "" && searchPattern == "")
                        {
                            filter.isFiltered = false;
                        }

                        long typeOfDispo = 5;
                        int orderDirection = 1;

                        totalCount = serviceBucketDataCount(managerUsername, "", "", typeOfDispo);
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }

                        service = followUp(fromDateNew, toDateNew, campaignName, serviceTypeName, searchPattern, managerUsername, user, CREIds, typeOfDispo, fromIndex, toIndex, orderDirection, bookingstatus, reasons, fromLSDDate, toLSDDate);

                        if (filter.isFiltered == true)
                        {
                            patternCount = followUp(fromDateNew, toDateNew, campaignName, serviceTypeName, searchPattern, managerUsername, user, CREIds, typeOfDispo, 0, totalCount, orderDirection, bookingstatus, reasons, fromLSDDate, toLSDDate).Count();
                        }
                    }
                    else if (filter.getDataFor == 5)
                    {
                        if (campaignName == "" && CREIds == "" && serviceTypeName == "" && fromDateNew == "" && toDateNew == "" && searchPattern == "" && lastDipo == "" && droppedCount == "")
                        {
                            filter.isFiltered = false;
                        }
                        totalCount = db.Database.SqlQuery<int>("call cremanagerNonContactsSearchCount(@pattern,@mgr_user_id,@users)",
                            new MySqlParameter("@pattern", ""),
                            new MySqlParameter("@mgr_user_id", managerUsername),
                        new MySqlParameter("@users", "")).FirstOrDefault();

                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }


                        service = getnonContactsListOfUserMR(fromDateNew, toDateNew, campaignName, lastDipo, droppedCount, searchPattern, managerUsername, CREIds, fromIndex, toIndex, fromLSDDate, toLSDDate);

                        if (filter.isFiltered == true)
                        {
                            patternCount = getnonContactsListOfUserMR(fromDateNew, toDateNew, campaignName, lastDipo, droppedCount, searchPattern, managerUsername, CREIds, 0, totalCount, fromLSDDate, toLSDDate).Count();
                        }
                    }
                    else if (filter.getDataFor == 6)
                    {
                        if (fromDateNew == "" && toDateNew == "" && searchPattern == "" )
                        {
                            filter.isFiltered = false;
                        }
                        totalCount = db.Database.SqlQuery<int>("call nonFS_nonPMS_vehiclereportedListsCremanagerCount(@mgr_user_id)",
                            new MySqlParameter("@mgr_user_id", managerUsername)).FirstOrDefault();

                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }

                        service = getNonFsPms(managerUsername, fromDateNew, toDateNew, searchPattern, fromIndex, toIndex);

                        if (filter.isFiltered == true)
                        {
                            patternCount = getNonFsPms(managerUsername, fromDateNew, toDateNew, searchPattern, 0, totalCount).Count();
                        }
                    }
                    else if (filter.getDataFor == 7)
                    {
                        if ( CREIds == "" && serviceBookedType == "" && fromDateNew == "" && toDateNew == "" && searchPattern == "" && bookingstatus=="")
                        {
                            filter.isFiltered = false;
                        }
                        totalCount = getSBNthDataCount(managerUsername,1);


                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }

                        service = getSBNthData(managerUsername, 1, CREIds, serviceBookedType, typeOfPickup, searchPattern, fromIndex, toIndex, instatus, fromLSDDate, toLSDDate);

                        if (filter.isFiltered == true)
                        {
                            patternCount = getSBNthData(managerUsername, 1, CREIds, serviceBookedType, typeOfPickup, searchPattern, 0, totalCount, instatus, fromLSDDate, toLSDDate).Count();
                        }
                    }
                    else if (filter.getDataFor == 8)
                    {
                        if (CREIds == "" && serviceBookedType == "" && fromDateNew == "" && toDateNew == "" && searchPattern == "" && bookingstatus == "")
                        {
                            filter.isFiltered = false;
                        }
                        totalCount = getSBNthDataCount(managerUsername, 2);

                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }


                        service = getSBNthData(managerUsername, 2, CREIds, serviceBookedType, typeOfPickup, searchPattern, fromIndex, toIndex, instatus, fromLSDDate, toLSDDate);

                        if (filter.isFiltered == true)
                        {
                            patternCount = getSBNthData(managerUsername, 2, CREIds, serviceBookedType, typeOfPickup, searchPattern, 0, totalCount, instatus, fromLSDDate, toLSDDate).Count();
                        }
                    }
                    else if (filter.getDataFor == 9)
                    {
                        if (CREIds == "" && campaignName == "" && fromDateNew == "" && toDateNew == "" && searchPattern == "" && serviceTypeName == "")
                        {
                            filter.isFiltered = false;
                        }
                        totalCount = db.Database.SqlQuery<int>("call cremanagerServiceFutureFollowupCount(@increManager)",
 new MySqlParameter[] { new MySqlParameter("@increManager", managerUsername) }).FirstOrDefault();

                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }


                        service = getFutureFollowUp(managerUsername, CREIds, fromDateNew, toDateNew, campaignName, serviceTypeName, searchPattern, fromIndex, toIndex, fromLSDDate, toLSDDate);

                        if (filter.isFiltered == true)
                        {
                            patternCount = getFutureFollowUp(managerUsername, CREIds, fromDateNew, toDateNew, campaignName, serviceTypeName, searchPattern, 0, totalCount, fromLSDDate, toLSDDate).Count();
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
            if (service != null)
            {
                if (filter.isFiltered == true)
                {
                    var JsonData = Json(new { data = service, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = patternCount, exception = exception }, JsonRequestBehavior.AllowGet);
                    JsonData.MaxJsonLength = int.MaxValue;
                    return JsonData;
                }
                else if (filter.isFiltered == false)
                {
                    var JsonData = Json(new { data = service, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount, exception = exception }, JsonRequestBehavior.AllowGet);
                    JsonData.MaxJsonLength = int.MaxValue;
                    return JsonData;
                }

            }
            return Json(new { data = "", draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0, exception = exception }, JsonRequestBehavior.AllowGet);
        }
        /*************************** Calling Stored Procedure ***********************/

        public List<CallLogDispositionLoadReminderMR> assignedListOfUserMR(string fromDateNew, string toDateNew, string campaignName, string serviceTypeName, string searchPattern, string managerUsername, string CREIds, long fromIndex, long toIndex)
        {
            List<CallLogDispositionLoadReminderMR> serviceData = new List<CallLogDispositionLoadReminderMR>();

            //try
            //{
            using (var db = new AutoSherDBContext())
            {
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
            //}
            //catch (Exception ex)
            //{

            //}
            return serviceData;
        }

        public int serviceBucketDataCount(string managerUsername,string pattern, string users,long dispotype)
        {
            int totalCount = 0;

            using (var db = new AutoSherDBContext())
            {
                totalCount = db.Database.SqlQuery<int>("call cremanagerdispositionfilterCount(@pattern,@mgr_user_id,@users,@dispositiontype)",
                            new MySqlParameter("@pattern", ""),
                            new MySqlParameter("@mgr_user_id", managerUsername),
                        new MySqlParameter("@users", ""),
                        new MySqlParameter("@dispositiontype", dispotype)).FirstOrDefault();

            }
            return totalCount;

        }

        public int getSBNthDataCount(string managerUsername, long sbday)
{
            int totalCount = 0;

            using (var db = new AutoSherDBContext())
            {

                totalCount = db.Database.SqlQuery<int>("call ServiceBookingPreviousDayCallsCremanagerCount(@increManager,@sbday)",
 new MySqlParameter[] { new MySqlParameter("@increManager", managerUsername), new MySqlParameter("@sbday", 2) }).FirstOrDefault();

            }
            return totalCount;
        }

        public List<CallLogDispositionLoadReminderMR> followUp(string fromDateNew, string toDateNew, string campaignName, string serviceTypeName, string pattern, string managerName, string userId, string CREIds, long typeOfDispo, long fromIndex, long toIndex, int orderDirection, string bookingstatus, string reasons, string fromLSDDate, string toLSDDate)
        {
            List<CallLogDispositionLoadReminderMR> serviceData = new List<CallLogDispositionLoadReminderMR>();

            //try
            //{
            using (var db = new AutoSherDBContext())
            {
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
            //}
            //catch (Exception ex)
            //{

            //}
            return serviceData;
        }

        public List<CallLogDispositionLoadReminderMR> getnonContactsListOfUserMR(string fromDateNew, string toDateNew, string campaignName, string lastDispo, string droppedCount, string pattern, string managerName, string CREIds, long fromIndex, long toIndex, string fromLSDDate, string toLSDDate)
        {
            List<CallLogDispositionLoadReminderMR> serviceData = new List<CallLogDispositionLoadReminderMR>();

            //try
            //{
            using (var db = new AutoSherDBContext())
            {
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
            //}
            //catch (Exception ex)
            //{

            //}
            return serviceData;
        }
        public List<CallLogDispositionLoadReminderMR> getSBNthData(string increManager, long sbday, string inChasers_id, string serviceBookedType, string intypeofpickup,
            string pattern, long fromIndex, long toIndex, string instatus, string fromLSDDate, string toLSDDate)
        {
            List<CallLogDispositionLoadReminderMR> dispositionLoads = null;
            //try
            //{
            using (var db = new AutoSherDBContext())
            {
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
            //}
            //catch (Exception ex)
            //{

            //}

            return dispositionLoads;
        }

        public List<CallLogDispositionLoadReminderMR> getNonFsPms(string increManager, string increstartdate, string inenddate, string pattern, long fromIndex, long toIndex)
        {
            List<CallLogDispositionLoadReminderMR> dispositionLoads = null;
            //try
            //{
            using (var db = new AutoSherDBContext())
            {
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
            //}
            //catch (Exception ex)
            //{

            //}

            return dispositionLoads;
        }


        public List<CallLogDispositionLoadReminderMR> getFutureFollowUp(string increManager, string inwyzuser, string instartdate, string inenddate, string incampaignname, string induetype,
         string pattern, long fromIndex, long toIndex, string fromLSDDate, string toLSDDate)
        {
            List<CallLogDispositionLoadReminderMR> dispositionLoads = null;
            //try
            //{
            using (var db = new AutoSherDBContext())
            {

                string str = @"CALL cremanagerServiceFutureFollowup(@increManager,@inwyzuser,@instartdate,@inenddate,@incampaignname,@induetype
,@pattern,@startwith,@length)";
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
            //}
            //catch (Exception ex)
            //{

            //}

            return dispositionLoads;
        }

        #endregion

        #region Other Reminder starts

        public ActionResult otherReminder()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    int UserId = Convert.ToInt32(Session["UserId"].ToString());
                    var ddllocation = db.locations.Select(m => new { id = m.cityId, location = m.name }).OrderBy(m => m.location).ToList();
                    ViewBag.ddllocation = ddllocation;

                }
            }
            catch (Exception ex)
            {

            }

            return View();
        }

        public ActionResult otherReminderDashboardCounts()
        {
            int outgoing, missed, incoming;
            try
            {
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                string managerUsername = Session["UserName"].ToString();
   
                using (var db = new AutoSherDBContext())
                {
                    db.Database.CommandTimeout = 900;
                    outgoing = getDataMissedIncominOutgoingCount(managerUsername, "OUTGOING", "", "", "", "");
                    missed = getDataMissedIncominOutgoingCount(managerUsername, "MISSED", "", "", "", "");
                    incoming = getDataMissedIncominOutgoingCount(managerUsername, "INCOMING", "", "", "", "");
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

            return Json(new { success = true, outgoing, missed, incoming  });
        }

        public ActionResult getOtherBucket(string otherreminderData)
        {
            List<CallLogDispositionLoadReminderMR> otherCalls = null;

            //Parameters of Paging..........
            int fromIndex = Convert.ToInt32(Request["start"]);
            int toIndex = Convert.ToInt32(Request["length"]);
            string searchPattern = Request["search[value]"];
            int UserId = Convert.ToInt32(Session["UserId"].ToString());
            string managerUsername = Session["UserName"].ToString();

            string fromDateNew, toDateNew, CREIds, city, workshopid;
            string exception = "";

            otherFilter filter = new otherFilter();
            if (otherreminderData != null)
            {
                filter = JsonConvert.DeserializeObject<otherFilter>(otherreminderData);
            }
            CREIds = filter.CRES == null ? "" : filter.CRES;
            fromDateNew = filter.fromDate == null ? "" : Convert.ToDateTime(filter.fromDate.ToString()).ToString("yyyy-MM-dd");
            toDateNew = filter.toDate == null ? "" : Convert.ToDateTime(filter.toDate.ToString()).ToString("yyyy-MM-dd");
            city = filter.city == null ? "" : filter.city;
            workshopid = filter.workshopLoc == null ? "" : filter.workshopLoc;

            int totalCount = 0;
            int totalFilteredCount = 0;

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
                        
                        string callType = "MISSED";
                        if (CREIds == "" && fromDateNew == "" && toDateNew == "" && searchPattern == "")
                        {
                            filter.isFiltered = false;
                        }
                        totalCount = getDataMissedIncominOutgoingCount(managerUsername, callType, CREIds, fromDateNew, toDateNew, searchPattern);
                        if (filter.isFiltered == true)
                        {
                            totalFilteredCount = getDataMissedIncominOutgoingCount(managerUsername, callType, "", "", "", "");
                        }
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }
                        otherCalls = getDataMissedIncominOutgoing(managerUsername, callType, CREIds, fromDateNew, toDateNew, searchPattern, fromIndex, toIndex);
                    }
                    else if (filter.getDataFor == 2)
                    {
                        string callType = "INCOMING";
                        if (CREIds == "" && fromDateNew == "" && toDateNew == "" && searchPattern=="")
                        {
                            filter.isFiltered = false;
                        }
                        totalCount = getDataMissedIncominOutgoingCount(managerUsername, callType, CREIds, fromDateNew, toDateNew, searchPattern);
                        if (filter.isFiltered == true)
                        {
                            totalFilteredCount = getDataMissedIncominOutgoingCount(managerUsername, callType, "", "", "", "");
                        }
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }

                        otherCalls = getDataMissedIncominOutgoing(managerUsername, callType, CREIds, fromDateNew, toDateNew, searchPattern, fromIndex, toIndex);

                    }
                    else if (filter.getDataFor == 3)
                    {
                        string callType = "OUTGOING";
                        if (CREIds == "" && fromDateNew == "" && toDateNew == "" && searchPattern == "")
                        {
                            filter.isFiltered = false;
                        }
                        totalCount = getDataMissedIncominOutgoingCount(managerUsername, callType, CREIds, fromDateNew, toDateNew, searchPattern);
                        if (filter.isFiltered == true)
                        {
                            totalFilteredCount = getDataMissedIncominOutgoingCount(managerUsername, callType, "", "", "", "");
                        }
                        if (toIndex < 0)
                        {
                            toIndex = 10;
                        }
                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }

                        otherCalls = getDataMissedIncominOutgoing(managerUsername, callType, CREIds, fromDateNew, toDateNew, searchPattern, fromIndex, toIndex);

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
            if (otherCalls != null)
            {
                if (filter.isFiltered == true)
                {

                    var JsonData = Json(new { data = otherCalls, draw = Request["draw"], recordsTotal = totalFilteredCount, recordsFiltered = totalCount, exception = exception });
                    JsonData.MaxJsonLength = int.MaxValue;
                    return JsonData;
                }
                else
                {
                    var JsonData = Json(new { data = otherCalls, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount, exception = exception });
                    JsonData.MaxJsonLength = int.MaxValue;
                    return JsonData;
                }

            }
            return Json(new { data = "", draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0 }, JsonRequestBehavior.AllowGet);
        }

        public List<CallLogDispositionLoadReminderMR> getDataMissedIncominOutgoing(string managerName, string callType, string CREIds, string fromCallDate, string toCallDate, string searchPattern, long fromIndex, long toIndex)
        {
            List<CallLogDispositionLoadReminderMR> dispositionLoads = null;
            try
            {
                using (var db = new AutoSherDBContext())
                {
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


        #endregion

        #region PSF Reminder Starts

        public ActionResult PSFReminder()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {

                    var ddllocation = db.locations.Select(m => new { id = m.cityId, location = m.name }).OrderBy(m => m.location).ToList();
                    ViewBag.ddllocation = ddllocation;
                    var ddlCampaign = db.campaigns.Where(m => m.campaignType == "psf" && m.isactive).Select(m => new { id = m.id, campaignName = m.campaignName }).ToList();
                    ViewBag.ddlCampaign = ddlCampaign;

                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public ActionResult loadPSFDashboardCounts(string psfID)
        {
            int fourthPSFFreshCallsCountCREMgr, fourthPSFfollowupcountCREMgr, fourthPSFnonContactsCREMgr, fourthPSFincompleteSurveyCountCREMgr, fourthPSFSurveysCREperDayMgr, fourthPSFContactsCREperDayMgr, fourthPSFTotalCallsCREperDayMgr;
            try
            {
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                string managerUsername = Session["UserName"].ToString();
                if (psfID == "")
                {
                    psfID = "0";
                }

                using (var db = new AutoSherDBContext())
                {
                    db.Database.CommandTimeout = 900;
                    fourthPSFFreshCallsCountCREMgr = db.Database.SqlQuery<int>("call PSFCREMgrFreshCallCount(@wyzUserId,@inpsfdayid);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", managerUsername), new MySqlParameter("@inpsfdayid", psfID) }).FirstOrDefault();
                    fourthPSFfollowupcountCREMgr = db.Database.SqlQuery<int>("call PSFCREMgrpendingFollowUpCount(@wyzUserId,@inpsfdayid);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", managerUsername), new MySqlParameter("@inpsfdayid", psfID) }).FirstOrDefault();
                    fourthPSFnonContactsCREMgr = db.Database.SqlQuery<int>("call PSFCREMgrNonContactCount(@wyzUserId,@inpsfdayid);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", managerUsername), new MySqlParameter("@inpsfdayid", psfID) }).FirstOrDefault();
                    fourthPSFincompleteSurveyCountCREMgr = db.Database.SqlQuery<int>("call PSFCREincompleteCount(@wyzUserId,@inpsfdayid);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", managerUsername), new MySqlParameter("@inpsfdayid", psfID) }).FirstOrDefault();
                    fourthPSFSurveysCREperDayMgr = db.Database.SqlQuery<int>("call PSFCREMgrSatisfiedCountToday(@wyzUserId,@inpsfdayid);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", managerUsername), new MySqlParameter("@inpsfdayid", psfID) }).FirstOrDefault();
                    fourthPSFContactsCREperDayMgr = db.Database.SqlQuery<int>("call PSFCREMgrContactCount(@wyzUserId,@inpsfdayid);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", managerUsername), new MySqlParameter("@inpsfdayid", psfID) }).FirstOrDefault();
                    fourthPSFTotalCallsCREperDayMgr = db.Database.SqlQuery<int>("call PSFCREMgrTotalCallsCountToday(@wyzUserId,@inpsfdayid);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", managerUsername), new MySqlParameter("@inpsfdayid", psfID) }).FirstOrDefault();

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

            return Json(new { success = true, fourthPSFFreshCallsCountCREMgr, fourthPSFfollowupcountCREMgr, fourthPSFnonContactsCREMgr, fourthPSFincompleteSurveyCountCREMgr, fourthPSFSurveysCREperDayMgr, fourthPSFContactsCREperDayMgr, fourthPSFTotalCallsCREperDayMgr });

        }


        public ActionResult getPSFBucket(string psfreminderData)
        {
            List<psfreminder> psfdetails = null;

            long fromIndex, toIndex;
            long totalCount = 0;
            long totalFilteredCount = 0;
            string campaignName, CREIds, fromassigndate, toassigndate;
            string exception = "";

            //Parameters of Paging..........
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchPattern = Request["search[value]"];
            int UserId = Convert.ToInt32(Session["UserId"].ToString());
            string managerUsername = Session["UserName"].ToString();

            psfFilter filter = new psfFilter();
            if (psfreminderData != null)
            {
                filter = JsonConvert.DeserializeObject<psfFilter>(psfreminderData);
            }
            if (searchPattern != "")
            {
                filter.isFiltered = true;
            }
            campaignName = filter.campaign == null ? "" : filter.campaign;
            CREIds = filter.CRES == null ? "" : filter.CRES;
            fromassigndate = filter.fromassigndate == null ? "" : Convert.ToDateTime(filter.fromassigndate.ToString()).ToString("yyyy-MM-dd");
            toassigndate = filter.toassigndate == null ? "" : Convert.ToDateTime(filter.toassigndate.ToString()).ToString("yyyy-MM-dd");

            using (var db = new AutoSherDBContext())
            {
                try
                {

                    if (filter.getDataFor == 1)
                    {

                        if(campaignName=="" && CREIds=="" && fromassigndate=="" && toassigndate=="")
                        {
                            filter.isFiltered = false;
                        }

                        totalCount = getPSFScheduledBucketDataCount(managerUsername, fromassigndate, toassigndate, campaignName, searchPattern, CREIds);


                        if (filter.isFiltered == true)
                        {
                            totalFilteredCount = getPSFScheduledBucketDataCount(managerUsername, "", "", "", "", "");
                        }


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
                        psfdetails = getPSFScheduledBucketData(managerUsername, fromassigndate, toassigndate, campaignName, searchPattern, CREIds, fromIndex, toIndex);

                    }
                    if (filter.getDataFor == 2)
                    {
                        if (campaignName == "" && CREIds == "" && fromassigndate == "" && toassigndate == "")
                        {
                            filter.isFiltered = false;
                        }
                        totalCount = Bucket2345Count(managerUsername, fromassigndate, toassigndate, campaignName, searchPattern, CREIds, 4);
                        if (filter.isFiltered == true)
                        {
                            totalFilteredCount = Bucket2345Count(managerUsername, "", "", "", "", "", 4);
                        }
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
                        psfdetails = Bucket2345(managerUsername, fromassigndate, toassigndate, campaignName, searchPattern, CREIds, 4, fromIndex, toIndex);

                    }
                    if (filter.getDataFor == 3)
                    {
                        if (campaignName == "" && CREIds == "" && fromassigndate == "" && toassigndate == "")
                        {
                            filter.isFiltered = false;
                        }
                        if (Session["DealerCode"].ToString() == "SUKHMANI")
                        {
                            totalCount = Bucket2345Count(managerUsername, fromassigndate, toassigndate, campaignName, searchPattern, CREIds, 44);
                        }
                        else
                        {
                            totalCount = Bucket2345Count(managerUsername, fromassigndate, toassigndate, campaignName, searchPattern, CREIds, 36);
                        }
                        if (filter.isFiltered == true)
                        {
                            totalFilteredCount = Bucket2345Count(managerUsername, "", "", "", "", "", 36);
                        }

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
                        if (Session["DealerCode"].ToString() == "SUKHMANI")
                        {
                            psfdetails = Bucket2345(managerUsername, fromassigndate, toassigndate, campaignName, searchPattern, CREIds, 44, fromIndex, toIndex);
                        }
                        else
                        {
                            psfdetails = Bucket2345(managerUsername, fromassigndate, toassigndate, campaignName, searchPattern, CREIds, 36, fromIndex, toIndex);
                        }
                    }
                    if (filter.getDataFor == 4)
                    {
                        if (campaignName == "" && CREIds == "" && fromassigndate == "" && toassigndate == "")
                        {
                            filter.isFiltered = false;
                        }

                        totalCount = Bucket2345Count(managerUsername, fromassigndate, toassigndate, campaignName, searchPattern, CREIds, 22);
                        if (filter.isFiltered == true)
                        {
                            totalFilteredCount = Bucket2345Count(managerUsername, "", "", "", "", "", 22);
                        }

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
                        psfdetails = Bucket2345(managerUsername, fromassigndate, toassigndate, campaignName, searchPattern, CREIds, 22, fromIndex, toIndex);

                    }
                    if (filter.getDataFor == 5)
                    {
                        if (campaignName == "" && CREIds == "" && fromassigndate == "" && toassigndate == "")
                        {
                            filter.isFiltered = false;
                        }
                        totalCount = Bucket2345Count(managerUsername, fromassigndate, toassigndate, campaignName, searchPattern, CREIds, 25);
                        if (filter.isFiltered == true)
                        {
                            totalFilteredCount = Bucket2345Count(managerUsername, "", "", "", "", "", 25);
                        }

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
                        psfdetails = Bucket2345(managerUsername, fromassigndate, toassigndate, campaignName, searchPattern, CREIds, 25, fromIndex, toIndex);

                    }
                    if (filter.getDataFor == 6)
                    {
                        if (campaignName == "" && CREIds == "" && fromassigndate == "" && toassigndate == "")
                        {
                            filter.isFiltered = false;
                        }
                        totalCount = Bucket67Count(managerUsername, fromassigndate, toassigndate, campaignName, searchPattern, CREIds, 1);
                        if (filter.isFiltered == true)
                        {
                            totalFilteredCount = Bucket67Count(managerUsername, "", "", "", "", "", 1);
                        }

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
                        psfdetails = Bucket67(managerUsername, fromassigndate, toassigndate, campaignName, searchPattern, CREIds, 1, fromIndex, toIndex);

                    }
                    if (filter.getDataFor == 7)
                    {
                        if (campaignName == "" && CREIds == "" && fromassigndate == "" && toassigndate == "")
                        {
                            filter.isFiltered = false;
                        }
                        totalCount = Bucket67Count(managerUsername, fromassigndate, toassigndate, campaignName, searchPattern, CREIds, 2);
                        if (filter.isFiltered == true)
                        {
                            totalFilteredCount = Bucket67Count(managerUsername, "", "", "", "", "", 1);
                        }

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
                        psfdetails = Bucket67(managerUsername, fromassigndate, toassigndate, campaignName, searchPattern, CREIds, 2, fromIndex, toIndex);

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
            if (psfdetails != null)
            {
                if (filter.isFiltered == true)
                {

                    var JsonData = Json(new { data = psfdetails, draw = Request["draw"], recordsTotal = totalFilteredCount, recordsFiltered = totalCount, exception = exception });
                    JsonData.MaxJsonLength = int.MaxValue;
                    return JsonData;
                }
                else
                {
                    var JsonData = Json(new { data = psfdetails, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount, exception = exception });
                    JsonData.MaxJsonLength = int.MaxValue;
                    return JsonData;
                }

            }
            return Json(new { data = "", draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0, exception = exception });
        }

        public long getPSFScheduledBucketDataCount(string managerUsername, string instartdate, string inenddate, string psfcampaignid, string pattern, string users)
        {
            long totalCount = 0;

            using (var db = new AutoSherDBContext())
            {
                totalCount = db.Database.SqlQuery<int>("call CREManagerPSFScheduledCallsCount(@mgrUserName ,@instartdate,@inenddate,@psfcampaignid,@pattern,@users)",
                           new MySqlParameter("@mgrUserName", managerUsername),
                           new MySqlParameter("@instartdate", instartdate),
                           new MySqlParameter("@inenddate", inenddate),
                           new MySqlParameter("@psfcampaignid", psfcampaignid),
                           new MySqlParameter("@pattern", pattern),
                           new MySqlParameter("@users", users)).FirstOrDefault();
            }
            return totalCount;

        }

        public List<psfreminder> getPSFScheduledBucketData(string mgrUserName, string instartdate, string inenddate, string psfcampaignid, string pattern, string users, long start_with, long length)
        {
            List<psfreminder> scheduledCalls = null;
            using (var db = new AutoSherDBContext())
            {
                string str = @"CALL CREManagerPSFScheduledCalls(@mgrUserName,@instartdate,@inenddate,@psfcampaignid,@pattern,@users,@start_with,@length)";
                MySqlParameter[] param = new MySqlParameter[]
                {
                         new MySqlParameter("@mgrUserName", mgrUserName),
                           new MySqlParameter("@instartdate", instartdate),
                           new MySqlParameter("@inenddate", inenddate),
                           new MySqlParameter("@psfcampaignid", psfcampaignid),
                           new MySqlParameter("@pattern", pattern),
                           new MySqlParameter("@users", users),
                           new MySqlParameter("@start_with", start_with),
                           new MySqlParameter("@length", length)
                };
                scheduledCalls = db.Database.SqlQuery<psfreminder>(str, param).ToList();
            }
            return scheduledCalls;
        }

        public long Bucket2345Count(string managerUsername, string instartdate, string inenddate, string psfcampaignid, string pattern, string users, long dispositiontype)
        {
            long totalCount = 0;

            using (var db = new AutoSherDBContext())
            {
                totalCount = db.Database.SqlQuery<int>("call CreManagerPSFfileterForContactsCount(@mgrUserName ,@instartdate,@inenddate,@psfcampaignid,@pattern,@users,@dispositiontype)",
                            new MySqlParameter("@mgrUserName", managerUsername),
                            new MySqlParameter("@instartdate", instartdate),
                            new MySqlParameter("@inenddate", inenddate),
                            new MySqlParameter("@psfcampaignid", psfcampaignid),
                            new MySqlParameter("@pattern", pattern),
                            new MySqlParameter("@users", users),
                            new MySqlParameter("@dispositiontype", dispositiontype)).FirstOrDefault();
            }
            return totalCount;

        }

        public List<psfreminder> Bucket2345(string mgrUserName, string instartdate, string inenddate, string psfcampaignid, string pattern, string users, long dispositiontype, long start_with, long length)
        {
            List<psfreminder> calldetails = null;
            using (var db = new AutoSherDBContext())
            {
                string str = @"CALL CreManagerPSFfileterForContacts(@mgrUserName,@instartdate,@inenddate,@psfcampaignid,@pattern,@users,@dispositiontype,@start_with,@length)";
                MySqlParameter[] param = new MySqlParameter[]
                {
                         new MySqlParameter("@mgrUserName", mgrUserName),
                           new MySqlParameter("@instartdate", instartdate),
                           new MySqlParameter("@inenddate", inenddate),
                           new MySqlParameter("@psfcampaignid", psfcampaignid),
                           new MySqlParameter("@pattern", pattern),
                           new MySqlParameter("@users", users),
                           new MySqlParameter("@dispositiontype", dispositiontype),
                           new MySqlParameter("@start_with", start_with),
                           new MySqlParameter("@length", length)
                };
                calldetails = db.Database.SqlQuery<psfreminder>(str, param).ToList();
            }
            return calldetails;
        }

        public long Bucket67Count(string managerUsername, string instartdate, string inenddate, string psfcampaignid, string pattern, string users, long dispositiontype)
        {
            long totalCount = 0;

            using (var db = new AutoSherDBContext())
            {
                totalCount = db.Database.SqlQuery<int>("call creManagerPSFfileterForNonContactsCount(@mgrUserName ,@instartdate,@inenddate,@psfcampaignid,@pattern,@users,@dispositiontype)",
                            new MySqlParameter("@mgrUserName", managerUsername),
                            new MySqlParameter("@instartdate", instartdate),
                            new MySqlParameter("@inenddate", inenddate),
                            new MySqlParameter("@psfcampaignid", psfcampaignid),
                            new MySqlParameter("@pattern", pattern),
                            new MySqlParameter("@users", users),
                            new MySqlParameter("@dispositiontype", dispositiontype)).FirstOrDefault();
                return totalCount;
            }
            return totalCount;

        }

        public List<psfreminder> Bucket67(string mgrUserName, string instartdate, string inenddate, string psfcampaignid, string pattern, string users, long dispositiontype, long start_with, long length)
        {
            List<psfreminder> calldetails = null;
            using (var db = new AutoSherDBContext())
            {
                string str = @"CALL creManagerPSFfileterForNonContacts(@mgrUserName,@instartdate,@inenddate,@psfcampaignid,@pattern,@users,@dispositiontype,@start_with,@length)";
                MySqlParameter[] param = new MySqlParameter[]
                {
                         new MySqlParameter("@mgrUserName", mgrUserName),
                           new MySqlParameter("@instartdate", instartdate),
                           new MySqlParameter("@inenddate", inenddate),
                           new MySqlParameter("@psfcampaignid", psfcampaignid),
                           new MySqlParameter("@pattern", pattern),
                           new MySqlParameter("@users", users),
                           new MySqlParameter("@dispositiontype", dispositiontype),
                           new MySqlParameter("@start_with", start_with),
                           new MySqlParameter("@length", length)
                };
                calldetails = db.Database.SqlQuery<psfreminder>(str, param).ToList();
            }
            return calldetails;
        }
        #endregion


        #region Ajax Calls
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

        public ActionResult getCRESListBasedOnWorkshop(long? workshopId, string crefor)
        {

            try
            {
                using (var db = new AutoSherDBContext())
                {

                    var creIds = db.userworkshops.Where(r => r.workshopList_id == workshopId).Select(r => r.userWorkshop_id).ToList();

                    //var listOfRoleId = db.userworkshops.Where(r => r.us == workshopId).Select(r => r.workshopList_id).ToList();
                    //var workshopsList = db.workshops.Where(r => listOfRoleId.Contains(r.id)).Select(m => new { id = m.id, name = m.workshopName }).ToList();

                    //var  workshopListId = db.userworkshops.Where(x => x.workshopList_id == workshopId).Select(m=>m.userWorkshop_id).ToList();

                    //foreach(var workshopids in workshopListId)
                    //{
                    if (crefor == "Service")
                    {
                        var creList = db.wyzusers.Where(u => creIds.Contains(u.id) && u.role == "CRE" && u.unAvailable == false && u.role1 == "1").Select(m => new { id = m.id, creName = m.firstName + "(" + m.userName + ")" }).ToList();

                        //var creList = db.wyzusers.Where(m => m.workshop_id == workshopId && m.insuranceRole == false && m.role == "CRE" && m.unAvailable == false && m.role1 == "1").Select(m => new { id = m.id, creName = m.userName }).OrderBy(m => m.creName).ToList();
                        return Json(creList);
                    }
                    else if (crefor == "PSF")
                    {
                        var creList = db.wyzusers.Where(u => creIds.Contains(u.id) && u.role == "CRE" && u.unAvailable == false && u.role1 == "4").Select(m => new { id = m.id, creName = m.firstName + "(" + m.userName + ")" }).ToList();

                        //var creList = db.wyzusers.Where(m => m.workshop_id == workshopId && m.role == "CRE" && m.unAvailable == false && m.role1 == "4").Select(m => new { id = m.id, creName = m.userName }).OrderBy(m => m.creName).ToList();
                        return Json(creList);
                    }
                    else if (crefor == "Other")
                    {
                        var creList = db.wyzusers.Where(u => creIds.Contains(u.id) && u.role == "CRE" && u.unAvailable == false).Select(m => new { id = m.id, creName = m.firstName + "(" + m.userName + ")" }).ToList();

                        //var creList = db.wyzusers.Where(m => m.workshop_id == workshopId && m.role == "CRE" && m.unAvailable == false).Select(m => new { id = m.id, creName = m.userName }).OrderBy(m => m.creName).ToList();
                        return Json(creList);
                    }

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
        #endregion

        #region PSFComplaint CRE manager
        [HttpGet,ActionName("PSFCREComplaintMgr_CallLog")]
        public ActionResult psfComplaintCreLog()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string managerusername = Session["UserName"].ToString();
                    long? locId = db.wyzusers.FirstOrDefault(m => m.userName == managerusername).location_cityId;
                    var workshops = db.workshops.Where(m => m.location_cityId == locId).Select(m => new { id = m.id, workshopname = m.workshopName }).ToList();
                    ViewBag.workshopList = workshops;

                    long ComCreRoleId = 0;
                    if (db.roles.Any(m => m.role1 == "Complaint Manager"))
                    {
                        ComCreRoleId = db.roles.FirstOrDefault(m => m.role1 == "Complaint Manager").id;
                    }

                    var comCreIds = db.userroles.Where(m => m.roles_id == ComCreRoleId).Select(m => m.users_id).ToList();

                    if (comCreIds != null)
                    {
                        ViewBag.complaintCreList = db.wyzusers.Where(m => comCreIds.Contains(m.id)).Select(m => new { id = m.id, name = m.userName }).ToList();
                    }
                }
            }
            catch(Exception ex)
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


        public ActionResult loadPSFCRECompMgrDashboardCounts()
        {
            int FreshComplaint, PendingFollowUps, NonContact,Rework,Contacts,TotalCalls;
            try
            {
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                string managerUsername = Session["UserName"].ToString();
                //if (psfID == "")
                //{
                //    psfID = "0";
                //}

                using (var db = new AutoSherDBContext())
                {
                    long ComCreRoleId = 0;
                    string wyzUser_id = "";
                    if(db.roles.Any(m=>m.role1=="Complaint Manager"))
                    {
                        ComCreRoleId = db.roles.FirstOrDefault(m => m.role1 == "Complaint Manager").id;
                    }

                    var comCreIds = db.userroles.Where(m => m.roles_id == ComCreRoleId).Select(m => m.users_id).ToList();

                    if (comCreIds != null)
                    {
                        wyzUser_id = string.Join(",", comCreIds);
                    }

                    db.Database.CommandTimeout = 900;
                    FreshComplaint = db.Database.SqlQuery<int>("call complaintCREManagerCalllogsDashboard(@incremanager,@bucket_id,@inwyzuser_id);", new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername), new MySqlParameter("@bucket_id", 1), new MySqlParameter("@inwyzuser_id", wyzUser_id) }).FirstOrDefault();
                    PendingFollowUps = db.Database.SqlQuery<int>("call complaintCREManagerCalllogsDashboard(@incremanager,@bucket_id,@inwyzuser_id);", new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername), new MySqlParameter("@bucket_id", 2), new MySqlParameter("@inwyzuser_id", wyzUser_id) }).FirstOrDefault();
                    NonContact = db.Database.SqlQuery<int>("call complaintCREManagerCalllogsDashboard(@incremanager,@bucket_id,@inwyzuser_id);", new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername), new MySqlParameter("@bucket_id", 3), new MySqlParameter("@inwyzuser_id", wyzUser_id) }).FirstOrDefault();
                    Rework = db.Database.SqlQuery<int>("call complaintCREManagerCalllogsDashboard(@incremanager,@bucket_id,@inwyzuser_id);", new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername), new MySqlParameter("@bucket_id", 4), new MySqlParameter("@inwyzuser_id", wyzUser_id) }).FirstOrDefault();
                    //Resolved = db.Database.SqlQuery<int>("call complaintCREManagerCalllogsDashboard(@incremanager,@bucket_id,@inwyzuser_id);", new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername), new MySqlParameter("@bucket_id", 5), new MySqlParameter("@inwyzuser_id", wyzUser_id) }).FirstOrDefault();
                    Contacts = db.Database.SqlQuery<int>("call complaintCREManagerCalllogsDashboard(@incremanager,@bucket_id,@inwyzuser_id);", new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername), new MySqlParameter("@bucket_id", 6), new MySqlParameter("@inwyzuser_id", wyzUser_id) }).FirstOrDefault();
                    TotalCalls = db.Database.SqlQuery<int>("call complaintCREManagerCalllogsDashboard(@incremanager,@bucket_id,@inwyzuser_id);", new MySqlParameter[] { new MySqlParameter("@incremanager", managerUsername), new MySqlParameter("@bucket_id", 7), new MySqlParameter("@inwyzuser_id", wyzUser_id) }).FirstOrDefault();

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

            return Json(new { success = true, FreshComplaint, PendingFollowUps, NonContact,Rework,Contacts,TotalCalls });

        }

        public ActionResult getBucketData(string filterData)
        {
            string exception = "";
            psfCompMgrFilter filter = new psfCompMgrFilter();

            List<psfCompMgrCallLog> psfdetails = new List<psfCompMgrCallLog>();

            long totalCount = 0, filterCount = 0;
            
            //DataTable Elements
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchPattern = Request["search[value]"];

            try
            {
               

                int UserId = Convert.ToInt32(Session["UserId"].ToString());

                string managerUsername = Session["UserName"].ToString();

               
                if (filterData != null)
                {
                    filter = JsonConvert.DeserializeObject<psfCompMgrFilter>(filterData);
                }

                if (searchPattern != "")
                {
                    filter.isFiltered = true;
                }


                //for sending data to procedures
                string inwyzuser_id, instartdt, inenddt, inreworkMode, inworkshopid;
                int  inaging, inattempt;

                inwyzuser_id = filter.compMgrWyzuserid == null? "" : filter.compMgrWyzuserid;
                instartdt = filter.startdate == null ? "" : Convert.ToDateTime(filter.startdate).ToString("yyyy-MM-dd");
                inenddt = filter.enddate == null ? "" : Convert.ToDateTime(filter.enddate).ToString("yyyy-MM-dd");
                inreworkMode = filter.reworkmode == null ? "" : filter.reworkmode;
                inworkshopid = filter.workshopid == null ? "0" : filter.workshopid;

                inaging = filter.aging;
                inattempt = filter.attempts;

                using (var db = new AutoSherDBContext())
                {
                    long ComCreRoleId = 0;
                    string wyzUser_id = "";
                    if (db.roles.Any(m => m.role1 == "Complaint Manager"))
                    {
                        ComCreRoleId = db.roles.FirstOrDefault(m => m.role1 == "Complaint Manager").id;
                    }

                    var comCreIds = db.userroles.Where(m => m.roles_id == ComCreRoleId).Select(m => m.users_id).ToList();

                    if (comCreIds != null)
                    {
                        wyzUser_id = string.Join(",", comCreIds);
                    }
                    
                    if(inwyzuser_id=="")
                    {
                        inwyzuser_id = wyzUser_id;
                    }
                    if(filter.getDataFor==0)
                    {
                        filter.getDataFor = 1;
                    }
                    totalCount = getPsfCompMgrCount(managerUsername, filter.getDataFor, inwyzuser_id, inaging, instartdt, inenddt, inattempt, inreworkMode);
                    psfdetails = getPsfCompMgrData(managerUsername, filter.getDataFor, inwyzuser_id, inaging, instartdt, inenddt, inattempt, inreworkMode, searchPattern, start, length, inworkshopid);

                    if(filter.isFiltered==true)
                    {
                        filterCount = getPsfCompMgrData(managerUsername, filter.getDataFor, inwyzuser_id, inaging, instartdt, inenddt, inattempt, inreworkMode, searchPattern, start, length, inworkshopid).Count();
                    }
                }
            }
            catch(Exception ex)
            {
                if (ex.InnerException!=null)
                {
                    if(ex.InnerException.InnerException!=null)
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

            if(filter.isFiltered==true)
            {
                var JsonData = Json(new { data = psfdetails, draw = Request["draw"], recordsTotal = filterCount, recordsFiltered = psfdetails.Count(), exception = exception });
                JsonData.MaxJsonLength = int.MaxValue;
                return JsonData;
            }
            else
            {
                var JsonData = Json(new { data = psfdetails, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = psfdetails.Count(), exception = exception });
                JsonData.MaxJsonLength = int.MaxValue;
                return JsonData;
            }
        }

        public long getPsfCompMgrCount(string managername, int bucketId, string wyzuserId, int ageing, string startDate, string endData, int attempts, string reworkmode)
        {
            long totalCount = 0;
            using (var db = new AutoSherDBContext())
            {
               totalCount =  db.Database.SqlQuery<long>("call complaintCREManagerCalllogsCount(@incremanager,@bucket_id,@inwyzuser_id,@inaging,@instartdt,@inenddt,@inattempt,@inreworkMode)",
                           new MySqlParameter("@incremanager", managername),
                           new MySqlParameter("@bucket_id", bucketId),
                           new MySqlParameter("@inwyzuser_id", wyzuserId),
                           new MySqlParameter("@inaging", ageing),
                           new MySqlParameter("@instartdt", startDate),
                           new MySqlParameter("@inenddt", endData),
                           new MySqlParameter("@inattempt", attempts),
                           new MySqlParameter("@inreworkMode", reworkmode)).FirstOrDefault();
            }
            return totalCount;
        }


        public List<psfCompMgrCallLog> getPsfCompMgrData(string incremanager, int bucket_id, string inwyzuser_id, int inaging, string instartdt, string inenddt, int inattempt, string inreworkMode, string pattern, int startwith, int length, string inworkshopid)
        {
            List<psfCompMgrCallLog> psfCompData = new List<psfCompMgrCallLog>();
            using (var db = new AutoSherDBContext())
            {
                psfCompData= db.Database.SqlQuery<psfCompMgrCallLog>("call complaintCREManagerCalllogs(@incremanager,@bucket_id,@inwyzuser_id,@inaging,@instartdt,@inenddt,@inattempt,@inreworkMode,@pattern,@startwith,@length,@inworkshopid)",
                             new MySqlParameter("@incremanager", incremanager),
                             new MySqlParameter("@bucket_id", bucket_id),
                             new MySqlParameter("@inwyzuser_id", inwyzuser_id),
                             new MySqlParameter("@inaging", inaging),
                             new MySqlParameter("@instartdt", instartdt),
                             new MySqlParameter("@inenddt", inenddt),
                             new MySqlParameter("@inattempt", inattempt),
                             new MySqlParameter("@inreworkMode", inreworkMode),
                             new MySqlParameter("@pattern", pattern),
                             new MySqlParameter("@startwith", startwith),
                             new MySqlParameter("@length", length),
                             new MySqlParameter("@inworkshopid", inworkshopid)).ToList();
            }
            return psfCompData;
        }
        #endregion


        #region View Manager Home
        public ActionResult managerHomePage()
        {
            return View();
        }
        #endregion

    }
}