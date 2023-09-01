using AutoSherpa_project.Models;
using AutoSherpa_project.Models.API_Model;
using AutoSherpa_project.Models.ViewModels;
using DocumentFormat.OpenXml.Wordprocessing;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace AutoSherpa_project.Controllers
{
    [AuthorizeFilter]

    public class SMRLiveController : Controller
    {
        // GET: SMRLive

        #region SMRLive Reports

        public ActionResult SMRLiveForm()
        {
            try
            {
                string userName = Session["UserName"].ToString();
                int userId = Convert.ToInt32(Session["UserId"]);
                string role = Session["UserRole"].ToString();
                using (var db = new AutoSherDBContext())
                {
                    var ddlcampaignList = db.campaigns.Where(m => (m.campaignType == "Campaign" || m.campaignType == "Service Reminder" || m.campaignType == "Forecast") && m.isactive == true).Select(model => new { id = model.id, CampaignName = model.campaignName }).ToList();

                    var ddlWorkshop = db.workshops.Select(model => new { workshopId = model.id, workshopName = model.workshopName }).OrderBy(m => m.workshopName).ToList();

                    if (role == "CREManager")
                    {
                        var ddlmanager = db.wyzusers.Where(m => m.userName == userName && m.unAvailable == false).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        ViewBag.ddlmanager = ddlmanager;

                        var ddlcres = db.wyzusers.Where(m => m.creManager == userName && m.role == "CRE" && m.unAvailable == false).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        ViewBag.ddlcres = ddlcres;

                    }
                    else if (role == "Admin")
                    {
                        var ddlcres = db.wyzusers.Where(m => m.role1 == "1" && m.role == "CRE" && m.unAvailable == false).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        var ddlmanager = db.wyzusers.Where(m => m.role1 == "1" && m.role == "CREManager" && m.unAvailable == false).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        ViewBag.ddlcres = ddlcres;
                        ViewBag.ddlmanager = ddlmanager;

                    }
                    else if (role == "RM" || role == "WM")
                    {
                        var creManagerIdList = db.AccessLevels.Where(m => m.rmId == userId).Select(m => new { m.creManagerId, m.creId }).Distinct().ToList();
                        var cresList = creManagerIdList.Select(m => m.creId).ToList();
                        var managerList = creManagerIdList.Select(m => m.creManagerId).ToList();


                        var ddlcres = db.wyzusers.Where(x => cresList.Contains(x.id) && x.role1 == "1" && x.role == "CRE" && x.unAvailable == false).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        var ddlmanager = db.wyzusers.Where(x => managerList.ToList().Contains(x.id) && x.role1 == "1" && x.role == "CREManager" && x.unAvailable == false).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        ViewBag.ddlcres = ddlcres;
                        ViewBag.ddlmanager = ddlmanager;
                    }
                    ViewBag.ddlcampaignList = ddlcampaignList;
                    ViewBag.ddlWorkshop = ddlWorkshop;


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
                TempData["ControllerName"] = "SMRLive";

                return RedirectToAction("LogOff", "Home");
            }
            return View();
        }

        public ActionResult SMRLiveloadDashboardCounts()
        {
            int BoxLiveCRE, BoxLiveDataAvail, BoxLiveCalls, BoxLiveCRECalls, BoxLiveContact, BoxLiveContactPercent, BoxLiveFreshBook, BoxLiveCancelled, BoxLiveReschedule;
            string userName = Session["UserName"].ToString();
            string role = Session["UserRole"].ToString();
            string creName = "";
            string managerName = "";
            try
            {
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                using (var db = new AutoSherDBContext())
                {
                    if (role == "CRE")
                    {
                        creName = userName;;
                        managerName = "";
                    }
                    else if (role == "CREManager")
                    {
                        creName = "";
                        managerName = userName;
                    }
                    else if (role == "Admin")
                    {
                        creName = "";
                        var mangeruserNames = db.wyzusers.Where(m => m.role == "CREManager" && m.role1 == "2" && m.unAvailable == false).Select(m => m.userName).ToList();
                        managerName = string.Join(",", mangeruserNames);
                        //Session["UserName"].ToString();
                    }
                    else if (role == "RM" || role == "WM")
                    {
                        creName = "";
                        var creManagerIdList = db.AccessLevels.Where(m => m.rmId == UserId).Select(m => m.creManagerId).Distinct().ToList();
                        var creManagerName = db.wyzusers.Where(x => creManagerIdList.Contains(x.id) && x.unAvailable == false).Select(x => x.userName).ToList();
                        managerName = string.Join(",", creManagerName);
                    }

                    BoxLiveCRE = db.Database.SqlQuery<int>("call live_reports_smr(@reportid,@increname,@incremanager,@inworkshop,@increid,@indatefrom,@indateto,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 2), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshop", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@indatefrom", ""), new MySqlParameter("@indateto", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxLiveDataAvail = db.Database.SqlQuery<int>("call live_reports_smr(@reportid,@increname,@incremanager,@inworkshop,@increid,@indatefrom,@indateto,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 1), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshop", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@indatefrom", ""), new MySqlParameter("@indateto", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxLiveCalls = db.Database.SqlQuery<int>("call live_reports_smr(@reportid,@increname,@incremanager,@inworkshop,@increid,@indatefrom,@indateto,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 4), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshop", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@indatefrom", ""), new MySqlParameter("@indateto", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxLiveCRECalls = db.Database.SqlQuery<int>("call live_reports_smr(@reportid,@increname,@incremanager,@inworkshop,@increid,@indatefrom,@indateto,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 3), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshop", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@indatefrom", ""), new MySqlParameter("@indateto", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxLiveContact = db.Database.SqlQuery<int>("call live_reports_smr(@reportid,@increname,@incremanager,@inworkshop,@increid,@indatefrom,@indateto,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 5), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshop", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@indatefrom", ""), new MySqlParameter("@indateto", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxLiveContactPercent = db.Database.SqlQuery<int>("call live_reports_smr(@reportid,@increname,@incremanager,@inworkshop,@increid,@indatefrom,@indateto,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 6), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshop", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@indatefrom", ""), new MySqlParameter("@indateto", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxLiveFreshBook = db.Database.SqlQuery<int>("call live_reports_smr(@reportid,@increname,@incremanager,@inworkshop,@increid,@indatefrom,@indateto,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 7), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshop", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@indatefrom", ""), new MySqlParameter("@indateto", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxLiveCancelled = db.Database.SqlQuery<int>("call live_reports_smr(@reportid,@increname,@incremanager,@inworkshop,@increid,@indatefrom,@indateto,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 8), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshop", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@indatefrom", ""), new MySqlParameter("@indateto", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxLiveReschedule = db.Database.SqlQuery<int>("call live_reports_smr(@reportid,@increname,@incremanager,@inworkshop,@increid,@indatefrom,@indateto,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 9), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshop", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@indatefrom", ""), new MySqlParameter("@indateto", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();

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

            return Json(new { success = true, BoxLiveCRE, BoxLiveDataAvail, BoxLiveCalls, BoxLiveCRECalls, BoxLiveContact, BoxLiveContactPercent, BoxLiveFreshBook, BoxLiveCancelled, BoxLiveReschedule });
        }

        public ActionResult getSMRReports(string smrData)
        {
            string exception = "";
            long userId = Convert.ToInt64(Session["UserId"]);
            DataTable smrReports = new DataTable();

            try
            {
                using (var db = new AutoSherDBContext())
                {
                     string role = Session["UserRole"].ToString();

                    reportFilter filter = new reportFilter();
                    if (smrData != null)
                    {
                        filter = JsonConvert.DeserializeObject<reportFilter>(smrData);
                    }

                    string CREName = string.Empty;
                    string manageruserName = string.Empty;

                    string workshopList = filter.selected_Workshop == null ? "" : filter.selected_Workshop;
                    string creList = filter.selected_CRE == null ? "" : filter.selected_CRE;
                    string CampaignList = filter.selected_Campaign == null ? "" : filter.selected_Campaign;
                    int reportId = Convert.ToInt32(filter.reportId);
                    string fromDate = (filter.fromDate == "" || filter.fromDate == null) ? "" : Convert.ToDateTime(filter.fromDate.ToString()).ToString("yyyy-MM-dd");
                    string toDate = (filter.toDate == null || filter.fromDate == "") ? "" : Convert.ToDateTime(filter.toDate.ToString()).ToString("yyyy-MM-dd");

                    if (role == "CRE")
                    {
                        CREName = Session["UserName"].ToString();
                        manageruserName = "";
                    }
                    else if (role == "CREManager")
                    {
                        CREName = "";
                        manageruserName = Session["UserName"].ToString();
                    }
                    else if (role == "Admin")
                    {
                        CREName = "";
                        var mangeruserNames = db.wyzusers.Where(m => m.role == "CREManager" && m.role1 == "2" && m.unAvailable == false).Select(m => m.userName).ToList();
                        manageruserName = string.Join(",", mangeruserNames);
                    }
                    else if (role == "RM" || role == "WM")
                    {
                        CREName = "";
                        var creManagerIdList = db.AccessLevels.Where(m => m.rmId == userId).Select(m => m.creManagerId).Distinct().ToList();
                        var creManagerName = db.wyzusers.Where(x => creManagerIdList.Contains(x.id) && x.unAvailable == false).Select(x => x.userName).ToList();
                        manageruserName = string.Join(",", creManagerName);
                    }

               
                    string conStr = ConfigurationManager.ConnectionStrings["AutoSherContext"].ConnectionString;

                    using (MySqlConnection connection = new MySqlConnection(conStr))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("live_reports_smr", connection))
                        {
                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@reportid", reportId));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@increname", CREName));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@incremanager", manageruserName));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@inworkshop", workshopList));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@increid", creList));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@indatefrom", fromDate));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@indateto", toDate));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@incampaign", CampaignList));
                            adapter.Fill(smrReports);
                        }
                    }
                    var results = JsonConvert.SerializeObject(smrReports, Formatting.Indented,
                       new JsonSerializerSettings
                       {
                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                       });

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
                var results = JsonConvert.SerializeObject(smrReports, Formatting.Indented,
                     new JsonSerializerSettings
                     {
                         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                     });
                return Json(new {data=results, exception=exception }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
        public ActionResult SMRMTDScoreCard()
        {

            return View();
        }
        public ActionResult SMRDataSummaryLiveForm()
        {

            return View();
        }

        #region SMR Download Reports
        public ActionResult SMRDataLogsForm()
        {

            try
            {
                string role = Session["UserRole"].ToString();
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                string userName = Session["UserName"].ToString();

                using (var db = new AutoSherDBContext())
                {

                    var ddlcampaignList = db.campaigns.Where(m => (m.campaignType == "Campaign" || m.campaignType == "Service Reminder" || m.campaignType == "Forecast") && m.isactive == true).Select(model => new { id = model.id, CampaignName = model.campaignName }).ToList();

                    var ddlWorkshop = db.workshops.Select(model => new { workshopId = model.id, workshopName = model.workshopName }).OrderBy(m => m.workshopName).ToList();


                    if (role == "CREManager")
                    {
                        var ddlmanager = db.wyzusers.Where(m => m.userName == userName).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        ViewBag.ddlmanager = ddlmanager;

                        var ddlcres = db.wyzusers.Where(m => m.creManager == userName && m.role == "CRE" && m.unAvailable == false).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        ViewBag.ddlcres = ddlcres;

                    }
                    else if (role == "Admin")
                    {
                        var ddlcres = db.wyzusers.Where(m => m.role1 == "1" && m.role == "CRE" && m.unAvailable == false).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        var ddlmanager = db.wyzusers.Where(m => m.role1 == "1" && m.role == "CREManager" && m.unAvailable == false).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        ViewBag.ddlcres = ddlcres;
                        ViewBag.ddlmanager = ddlmanager;

                    }
                    else if (role == "RM" || role == "WM")
                    {
                        var creManagerIdList = db.AccessLevels.Where(m => m.rmId == UserId).Select(m => new { m.creManagerId, m.creId }).Distinct().ToList();
                        var cresList = creManagerIdList.Select(m => m.creId).ToList();
                        var managerList = creManagerIdList.Select(m => m.creManagerId).ToList();


                        var ddlcres = db.wyzusers.Where(x => cresList.Contains(x.id) && x.role1 == "1" && x.role == "CRE" && x.unAvailable == false).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        var ddlmanager = db.wyzusers.Where(x => managerList.ToList().Contains(x.id) && x.role1 == "1" && x.role == "CREManager" && x.unAvailable == false).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        ViewBag.ddlcres = ddlcres;
                        ViewBag.ddlmanager = ddlmanager;
                    }

          
                    ViewBag.ddlcampaignList = ddlcampaignList;
                    ViewBag.ddlWorkshop = ddlWorkshop;



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
                TempData["ControllerName"] = "SMRLive";

                return RedirectToAction("LogOff", "Home");
            }
            return View();
        }

        public ActionResult smrDownloadFilter(string SMRdownloadFilters)
        {
            DataTable downloadExceltable = new DataTable();
            int totalCount = 0, toIndex = 10000;
            int reportId;
            string workshopLists, smstypeLists, smsstatusLists, campaignLists, creLists, callfromDate, calltoDate, reportName, connectionId;
            downloadReportFilter filter = JsonConvert.DeserializeObject<downloadReportFilter>(SMRdownloadFilters);
            connectionId = filter.connectionId;

            try
            {
                
                reportId = Convert.ToInt32(filter.reportId);
                workshopLists = filter.workshopLists == null ? "" : filter.workshopLists;
                smstypeLists = filter.smstypeLists == null ? "" : filter.smstypeLists;
                smsstatusLists = filter.smsstatusLists == null ? "" : filter.smsstatusLists;
                campaignLists = filter.campaignLists == null ? "" : filter.campaignLists;
                creLists = filter.creLists == null ? "" : filter.creLists;
                callfromDate = filter.callfromDate == null || filter.callfromDate == "" ? "" : Convert.ToDateTime(filter.callfromDate.ToString()).ToString("yyyy-MM-dd");
                calltoDate = filter.calltoDate == null || filter.calltoDate == "" ? "" : Convert.ToDateTime(filter.calltoDate.ToString()).ToString("yyyy-MM-dd");
                reportName = filter.reportName;

                using (var db = new AutoSherDBContext())
                {

                    new liveProgressbar().SendProgress(connectionId, "Processing....(" + 0 +" /"+ totalCount + ")", 0, 0);
                    string conStr = ConfigurationManager.ConnectionStrings["AutoSherContext"].ConnectionString;
                    totalCount = getdownloadCount(reportId, workshopLists, smstypeLists, smsstatusLists, campaignLists, creLists, callfromDate, calltoDate, reportName);
                    if(totalCount>0)
                    { 
                    for (int i = 0; i < totalCount; i += 10000)
                    {
                            using (MySqlConnection connection = new MySqlConnection(conStr))
                            {
                                using (MySqlCommand cmd = new MySqlCommand("live_ServicereportsQuerys", connection))
                                {
                                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@reportid", reportId));
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@fromcalldate", callfromDate));
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@tocalldate", calltoDate));
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@inwyzuser_id", creLists));
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@inworkshop_id", workshopLists));
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@incampaign_id", campaignLists));
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@insmstype", smstypeLists));
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@insmsstatustype", smsstatusLists));
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@infromIndex", i));
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@intoIndex", toIndex));
                                    adapter.Fill(downloadExceltable);
                                }
                            }
                        new liveProgressbar().SendProgress(connectionId, "Processing....("+i+"/"+totalCount+")", i, totalCount);
                        }
                   
                        Response.Clear();

                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("service.xlsx", System.Text.Encoding.UTF8));
                        using (ExcelPackage pck = new ExcelPackage())
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("service");
                            ws.Cells["A1"].LoadFromDataTable(downloadExceltable, true);
                            Session["downloadSMRReports"] = pck.GetAsByteArray();
                        }
                        new liveProgressbar().SendProgress(connectionId, "Processing....(" + totalCount + "/" + totalCount + ")", totalCount, totalCount);
                        return Json(new { success = true });
                    }
                    else
                    {
                        new liveProgressbar().SendProgress(connectionId, "Processing....", 100, 100);
                        return Json(new { success = false, error = "No Records Found." }, JsonRequestBehavior.AllowGet);
                    }
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
                new liveProgressbar().SendProgress(connectionId, "Processing....", 100, 100);
                return Json(new { success = false, error = exception }, JsonRequestBehavior.AllowGet);
            }
        }

        public int getdownloadCount(int reportId, string workshopLists, string smstypeLists, string smsstatusLists, string campaignLists, string creLists, string callfromDate, string calltoDate, string reportName)
        {
            int totalCount = 0;


            using (var db = new AutoSherDBContext())
            {

                totalCount = db.Database.SqlQuery<int>("call live_ServicereportsQuerys_Counts(@reportid,@fromcalldate,@tocalldate,@inwyzuser_id,@inworkshop_id,@incampaign_id,@insmstype,@insmsstatustype)",
                            new MySqlParameter("@reportid", reportId),
                            new MySqlParameter("@fromcalldate", callfromDate),
                            new MySqlParameter("@tocalldate", calltoDate),
                            new MySqlParameter("@inwyzuser_id", creLists),
                            new MySqlParameter("@inworkshop_id", workshopLists),
                            new MySqlParameter("@incampaign_id", campaignLists),
                            new MySqlParameter("@insmstype", smstypeLists),
                            new MySqlParameter("@insmsstatustype", smsstatusLists)).FirstOrDefault();
            }
            return totalCount;
        }
        public  DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public ActionResult todayCrecallReport(string workshops,int moduletype)
        {
            FileContentResult robj;
            List<creTodaysLiveReport> todaysCreReport = new List<creTodaysLiveReport>();
            try
            {
                int userId = Convert.ToInt32(Session["UserId"]);

                DataTable downloadExceltable = new DataTable();
                using (var db = new AutoSherDBContext())
                 {
                    if (moduletype == 1)
                    {
                        todaysCreReport = db.Database.SqlQuery<creTodaysLiveReport>("select  CallTime,chassisNo as VinNo,Veh_Reg_no as VehRegno ,secondary_dispostion as Dispostion, Compliant_Remark as comment   from   callhistorycube    where  calldate =CURDATE() and   FIND_IN_SET(wyzuser_id,@Id) and FIND_IN_SET(workshop_id,@inworkshop_id)   order by CallTime ;", new MySqlParameter("@Id", userId), new MySqlParameter("@inworkshop_id", workshops)).ToList();
                    }
                    if (moduletype == 2)
                    {
                        todaysCreReport = db.Database.SqlQuery<creTodaysLiveReport>("select  CallTime,chassisNo as VinNo,vehicleRegNo as VehRegno ,SecondaryDisposition as Dispostion,comments   from   insurancecallhistorycube    where  calldate =CURDATE() and   FIND_IN_SET(wyzuser_id,@Id) and FIND_IN_SET(workshop_id,@inworkshop_id)   order by CallTime ;", new MySqlParameter("@Id", userId), new MySqlParameter("@inworkshop_id", workshops)).ToList();
                    }
                    if (todaysCreReport.Count == 0)
                    {
                        return Json(new { success = false, error = "Your Call History is Empty." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        downloadExceltable = ToDataTable(todaysCreReport);

                        Response.Clear();

                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("todayscallReport.xlsx", System.Text.Encoding.UTF8));
                        using (ExcelPackage pck = new ExcelPackage())
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("todayscallReport");
                            ws.Cells["A1"].LoadFromDataTable(downloadExceltable, true);
                            robj = File(pck.GetAsByteArray(), System.Net.Mime.MediaTypeNames.Application.Octet, "todayscallReport.xlsx");
                        }
                        return Json(new { robj, success = true }, JsonRequestBehavior.AllowGet);
                    }
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

                return Json(new { success = false, error = exception }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        public ActionResult SMRInterDayAnalysis()
        {

            return View();
        }
        public ActionResult SMRVinLiveForm()
        {

            return View();
        }
        public ActionResult DownloadSMRALL(string reportName)
        {
            try
            {

                if (Session["downloadSMRReports"] != null)
                {

                    byte[] data = Session["downloadSMRReports"] as byte[];
                    Session["downloadSMRReports"] = null;
                    return File(data, "application/octet-stream", reportName + DateTime.Now.ToShortDateString() + DateTime.Now.TimeOfDay + ".xlsx");
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                return new EmptyResult();
            }
        }

        #region barchat

        [HttpPost]
        public JsonResult getSMRChart(string smrbarData)
        {
            string exception;
            List<object> iData = new List<object>();
            string wyzuserId = "";
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string role = Session["UserRole"].ToString();
                    int userId = Convert.ToInt32(Session["UserId"]);
                    string userName = Session["UserName"].ToString();


                    reportFilter filter = new reportFilter();
                    if (smrbarData != null)
                    {
                        filter = JsonConvert.DeserializeObject<reportFilter>(smrbarData);
                    }

                    string workshopList = filter.selected_Workshop == null ? "" : filter.selected_Workshop;
                    string creList = filter.selected_CRE == null ? "" : filter.selected_CRE;
                    string CampaignList = filter.selected_Campaign == null ? "" : filter.selected_Campaign;
                    int reportId = Convert.ToInt32(filter.reportId);
                   
                    if (role == "CRE")
                    {
                        creList = userId.ToString();
                    }
                    else
                    {
                        creList = filter.selected_CRE == null ? "" : filter.selected_CRE;
                    }

                    string str = @"CALL live_reports_smr(@reportid,@increname,@incremanager,@inworkshop,@increid,@indatefrom,@indateto,@incampaign);";
                    MySqlParameter[] sqlParameter = new MySqlParameter[]
                    {
                        new MySqlParameter("reportid",reportId),
                        new MySqlParameter("increname", ""),
                        new MySqlParameter("incremanager", ""),
                        new MySqlParameter("inworkshop", ""),
                        new MySqlParameter("increid", creList),
                        new MySqlParameter("indatefrom", ""),
                        new MySqlParameter("indateto", ""),
                        new MySqlParameter("incampaign", "")

                    };

                    List<barchartVM> times = db.Database.SqlQuery<barchartVM>(str, sqlParameter).ToList();

                    if (times.Count > 0)
                    {
                        iData.Add(times.Select(m => m.TimeInterval).ToList());
                        iData.Add(times.Select(m => m.Calls).ToList());

                        return Json(new { success = true, data = iData, exception = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, data = iData, exception = "Intra Day Record Not Found" }, JsonRequestBehavior.AllowGet);
                    }
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
                return Json(new { success = false, data = iData, exception = exception }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        public class smrFilter
        {
            public long reportId { get; set; }
            public string Cres { get; set; }
            public string workshops { get; set; }
        }

    }

}