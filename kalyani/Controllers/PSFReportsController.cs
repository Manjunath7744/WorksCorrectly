using AutoSherpa_project.Models;
using AutoSherpa_project.Models.API_Model;
using AutoSherpa_project.Models.ViewModels;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using NLog;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoSherpa_project.Controllers
{
    [AuthorizeFilter]
    public class PSFReportsController : Controller
    {
        #region  PSFLiveForm
        // GET: PSFLiveForm
        public ActionResult PSFLive()
        {
            string role = Session["UserRole"].ToString();
            int userId = Convert.ToInt32(Session["UserId"]);

            try
            {
                string userName = Session["UserName"].ToString();

                using (var db = new AutoSherDBContext())
                {
                    var campaignList = db.campaigns.Where(m => m.campaignType == "psf" && m.isactive).Select(m => new { id = m.id, campaignName = m.campaignName }).OrderBy(m => m.campaignName).ToList();
                    var workshopList = db.workshops.Select(m => new { workshopName = m.workshopName, id = m.id }).ToList();


                    if (role == "CREManager")
                    {
                        //var ddlmanager = db.wyzusers.Where(m => m.userName == userName).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        //ViewBag.ddlmanager = ddlmanager;

                        var ddlcres = db.wyzusers.Where(m => m.creManager == userName && m.role == "CRE" && m.unAvailable == false).Select(model => new { id = model.id, userName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.userName).ToList();
                        ViewBag.ddlcreList = ddlcres;

                    }
                    else if (role == "Admin")
                    {
                        var ddlcres = db.wyzusers.Where(m => m.role1 == "4" && m.role == "CRE" && m.unAvailable == false).Select(model => new { id = model.id, userName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.userName).ToList();
                        //var ddlmanager = db.wyzusers.Where(m => m.role1 == "1" && m.role == "CREManager").Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        ViewBag.ddlcreList = ddlcres;
                        //ViewBag.ddlmanager = ddlmanager;

                    }
                    else if (role == "RM" || role == "WM")
                    {
                        var creManagerIdList = db.AccessLevels.Where(m => m.rmId == userId).Select(m => new { m.creManagerId, m.creId }).Distinct().ToList();
                        var cresList = creManagerIdList.Select(m => m.creId).ToList();
                        var managerList = creManagerIdList.Select(m => m.creManagerId).ToList();


                        var ddlcres = db.wyzusers.Where(x => cresList.Contains(x.id) && x.role1 == "4" && x.role == "CRE" && x.unAvailable == false).Select(model => new { id = model.id, userName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.userName).ToList();
                        //var ddlmanager = db.wyzusers.Where(x => managerList.ToList().Contains(x.id) && x.role1 == "1" && x.role == "CREManager").Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        ViewBag.ddlcreList = ddlcres;
                        //ViewBag.ddlmanager = ddlmanager;
                    }


                    ViewBag.ddlcampaignList = campaignList.OrderBy(m => m.campaignName);
                    ViewBag.ddlworkshopList = workshopList.OrderBy(m => m.workshopName);
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
                TempData["ControllerName"] = "PSFReports";

                return RedirectToAction("LogOff", "Home");

            }
            return View();
        }
        public ActionResult PSFLiveloadDashboardCounts()
        {
            int BoxPSFLiveCRE, BoxPSFLiveCalls, BoxPSFLiveCRECalls, BoxPSFLiveContact, BoxPSFLiveContactPercent, BoxPSFLiveComplete, BoxPSFLiveSatisfied, BoxPSFLiveDisSatisfied;
            string userName = Session["UserName"].ToString();
            string role = Session["UserRole"].ToString();
            string creName = "";
            string managerName = "";


            try
            {
                
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                using (var db = new AutoSherDBContext())
                {
                    var workshopid = db.wyzusers.FirstOrDefault(m => m.id == UserId).workshop_id;
                    if (role == "CRE")
                    {
                        creName = Session["UserName"].ToString();
                        managerName = "";
                    }
                    else if (role == "CREManager")
                    {
                        creName = "";
                        managerName = Session["UserName"].ToString();
                    }
                    else if (role == "Admin")
                    {
                        creName = "";
                        var mangeruserNames = db.wyzusers.Where(m => m.role == "CREManager" && m.role1 == "4" && m.unAvailable == false).Select(m => m.userName).ToList();
                        managerName = string.Join(",", mangeruserNames);

                    }
                    else if (role == "RM" || role == "WM")
                    {
                        creName = "";
                        var creManagerIdList = db.AccessLevels.Where(m => m.rmId == UserId).Select(m => m.creManagerId).Distinct().ToList();
                        var creManagerName = db.wyzusers.Where(x => creManagerIdList.Contains(x.id) && x.unAvailable == false).Select(x => x.userName).ToList();
                        managerName = string.Join(",", creManagerName);
                    }

                    BoxPSFLiveCRE = db.Database.SqlQuery<int>("call live_reports_psf(@reportid,@increname,@incremanager,@inworkshopid,@increid,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 1), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshopid", workshopid), new MySqlParameter("@increid", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxPSFLiveCalls = db.Database.SqlQuery<int>("call live_reports_psf(@reportid,@increname,@incremanager,@inworkshopid,@increid,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 2), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshopid", workshopid), new MySqlParameter("@increid", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxPSFLiveCRECalls = db.Database.SqlQuery<int>("call live_reports_psf(@reportid,@increname,@incremanager,@inworkshopid,@increid,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 3), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshopid", workshopid), new MySqlParameter("@increid", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxPSFLiveContact = db.Database.SqlQuery<int>("call live_reports_psf(@reportid,@increname,@incremanager,@inworkshopid,@increid,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 4), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshopid", workshopid), new MySqlParameter("@increid", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxPSFLiveContactPercent = db.Database.SqlQuery<int>("call live_reports_psf(@reportid,@increname,@incremanager,@inworkshopid,@increid,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 5), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshopid", workshopid), new MySqlParameter("@increid", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxPSFLiveComplete = db.Database.SqlQuery<int>("call live_reports_psf(@reportid,@increname,@incremanager,@inworkshopid,@increid,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 6), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshopid", workshopid), new MySqlParameter("@increid", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxPSFLiveSatisfied = db.Database.SqlQuery<int>("call live_reports_psf(@reportid,@increname,@incremanager,@inworkshopid,@increid,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 7), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshopid", workshopid), new MySqlParameter("@increid", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxPSFLiveDisSatisfied = db.Database.SqlQuery<int>("call live_reports_psf(@reportid,@increname,@incremanager,@inworkshopid,@increid,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 8), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshopid", workshopid), new MySqlParameter("@increid", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();

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

            return Json(new { success = true, BoxPSFLiveCRE, BoxPSFLiveCalls, BoxPSFLiveCRECalls, BoxPSFLiveContact, BoxPSFLiveContactPercent, BoxPSFLiveComplete, BoxPSFLiveSatisfied, BoxPSFLiveDisSatisfied });
        }
        public ActionResult getPSFReports(string PSFData)
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

                    if (PSFData != null)
                    {
                        filter = JsonConvert.DeserializeObject<reportFilter>(PSFData);
                    }
                    string CREName = string.Empty;
                    string manageruserName = string.Empty;



                    string workshopList = filter.selected_Workshop == null ? "" : filter.selected_Workshop;
                    string creList = filter.selected_CRE == null ? "" : filter.selected_CRE;
                    string CampaignList = filter.selected_Campaign == null ? "" : filter.selected_Campaign;
                    int reportId = Convert.ToInt32(filter.reportId);

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
                        using (MySqlCommand cmd = new MySqlCommand("live_reports_psf", connection))
                        {
                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("reportid", reportId));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("increname", CREName));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("incremanager", manageruserName));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("inworkshop", workshopList));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("increid", creList));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("incampaign", CampaignList));
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

        #region PSF Download starts
        // GET: PSFReports
        public ActionResult downloadPSFReport()
        {
            try
            {
                string role = Session["UserRole"].ToString();
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                string userName = Session["UserName"].ToString();

                using (var db = new AutoSherDBContext())
                {

                    var ddlWorkshop = db.workshops.Select(model => new { workshopId = model.id, workshopName = model.workshopName }).OrderBy(m => m.workshopName).ToList();
                    var ddlcampaignList = db.campaigns.Where(m => m.campaignType == "psf" && m.isactive).Select(m => new { id = m.id, campaignName = m.campaignName }).OrderBy(m => m.campaignName).ToList();


                    if (role == "CREManager")
                    {

                        var ddlcres = db.wyzusers.Where(m => m.creManager == userName && m.role == "CRE" && m.role1 == "4" && m.unAvailable == false).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        ViewBag.ddlcres = ddlcres;

                    }
                    else if (role == "Admin")
                    {
                        var ddlcres = db.wyzusers.Where(m => m.role1 == "4" && m.role == "CRE" && m.unAvailable == false).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        ViewBag.ddlcres = ddlcres;
                    }
                    else if (role == "RM" || role == "WM")
                    {
                        var creManagerIdList = db.AccessLevels.Where(m => m.rmId == UserId).Select(m => new { m.creManagerId, m.creId }).Distinct().ToList();
                        var cresList = creManagerIdList.Select(m => m.creId).ToList();
                        var ddlcres = db.wyzusers.Where(x => cresList.Contains(x.id) && x.role1 == "4" && x.role == "CRE" && x.unAvailable == false).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        ViewBag.ddlcres = ddlcres;
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
                TempData["ControllerName"] = "InsuranceLiveForm";

                return RedirectToAction("LogOff", "Home");
            }
            return View();
        }

        public ActionResult PSFDownloadFilter(string PSFdownloadFilters)
        {
            DataTable downloadExceltable = new DataTable();
            int totalCount = 0, toIndex = 20000;
            int reportId, smsType;
            string workshopLists, connectionId, smstypeLists, smsstatusLists, campaignLists, creLists, callfromDate, smsdateFrom, billfromdate, billtodate, assignedfromdate, assignedtodate, calltoDate, smsdateTo, reportName;
            downloadReportFilter filter = JsonConvert.DeserializeObject<downloadReportFilter>(PSFdownloadFilters);
            connectionId = filter.connectionId;
            try
            {

                reportId = Convert.ToInt32(filter.reportId);
                workshopLists = filter.workshopLists == null ? "" : filter.workshopLists;
                smstypeLists = filter.smstypeLists == null ? "" : filter.smstypeLists;
                smsType = filter.smstype == null || filter.smstype == "" ? 0 : Convert.ToInt32(filter.smstype);
                smsstatusLists = filter.smsstatusLists == null ? "" : filter.smsstatusLists;
                campaignLists = filter.campaignLists == null ? "" : filter.campaignLists;
                creLists = filter.creLists == null ? "" : filter.creLists;
                callfromDate = filter.callfromDate == null || filter.callfromDate == "" ? "" : Convert.ToDateTime(filter.callfromDate.ToString()).ToString("yyyy-MM-dd");
                calltoDate = filter.calltoDate == null || filter.calltoDate == "" ? "" : Convert.ToDateTime(filter.calltoDate.ToString()).ToString("yyyy-MM-dd");
                billfromdate = filter.billdateFrom == null || filter.billdateFrom == "" ? "" : Convert.ToDateTime(filter.billdateFrom.ToString()).ToString("yyyy-MM-dd");
                billtodate = filter.billdateTo == null || filter.billdateTo == "" ? "" : Convert.ToDateTime(filter.billdateTo.ToString()).ToString("yyyy-MM-dd");
                reportName = filter.reportName;

                using (var db = new AutoSherDBContext())
                {
                    new liveProgressbar().SendProgress(connectionId, "Processing....(" + 0 + " /" + totalCount + ")", 0, 0);
                    string conStr = ConfigurationManager.ConnectionStrings["AutoSherContext"].ConnectionString;
                    totalCount = getPSFdownloadCount(reportId, workshopLists, smstypeLists, smsstatusLists, campaignLists, creLists, callfromDate, calltoDate, billfromdate, billtodate, smsType);

                    if (totalCount > 0)
                    {
                        for (int i = 0; i < totalCount; i += 20000)
                        {

                            using (MySqlConnection connection = new MySqlConnection(conStr))
                            {
                                using (MySqlCommand cmd = new MySqlCommand("live_PSFreportsQuerys", connection))
                                {
                                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@reportid", reportId));
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@fromcalldate", callfromDate));
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@tocalldate", calltoDate));
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@frombilldate", billfromdate));
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@tobilldate", billtodate));
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@inwyzuser_id", creLists));
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@inworkshop_id", workshopLists));
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@incampaign_id", campaignLists));
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@insmstype", smstypeLists));
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@insmsstatustype", smsstatusLists));
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@smstype", smsType));
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@infromIndex", i));
                                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@intoIndex", toIndex));
                                    adapter.Fill(downloadExceltable);
                                }
                            }
                            new liveProgressbar().SendProgress(connectionId, "Processing....(" + i + "/" + totalCount + ")", i, totalCount);
                        }
                        Response.Clear();

                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("InsuranceDue.xlsx", System.Text.Encoding.UTF8));
                        using (ExcelPackage pck = new ExcelPackage())
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Service_Reminder");
                            ws.Cells["A1"].LoadFromDataTable(downloadExceltable, true);
                            Session["downloadPSFReports"] = pck.GetAsByteArray();
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
        public int getPSFdownloadCount(int reportId, string workshopLists, string smstypeLists, string smsstatusLists, string campaignLists, string creLists, string callfromDate, string calltoDate, string billfromdate, string billtodate, int smstype)
        {
            int totalCount = 0;


            using (var db = new AutoSherDBContext())
            {

                totalCount = db.Database.SqlQuery<int>("call live_PSFreportsQuerys_Counts(@reportid, @fromcalldate,@tocalldate,@frombilldate,@tobilldate,@inwyzuser_id,@inworkshop_id,@incampaign_id,@insmstype,@insmsstatustype,@smstype)",
                            new MySqlParameter("@reportid", reportId),
                            new MySqlParameter("@fromcalldate", callfromDate),
                            new MySqlParameter("@tocalldate", calltoDate),
                            new MySqlParameter("@frombilldate", billfromdate),
                            new MySqlParameter("@tobilldate", billtodate),
                            new MySqlParameter("@inwyzuser_id", creLists),
                            new MySqlParameter("@inworkshop_id", workshopLists),
                            new MySqlParameter("@incampaign_id", campaignLists),
                            new MySqlParameter("@insmstype", smstypeLists),
                            new MySqlParameter("@insmsstatustype", smsstatusLists),
                            new MySqlParameter("@smstype", smstype)).FirstOrDefault();
            }
            return totalCount;
        }
        public ActionResult DownloadALL(string reportName)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {

                if (Session["downloadPSFReports"] != null)
                {
                    logger.Info("PSF Report Download Started");

                    byte[] data = Session["downloadPSFReports"] as byte[];
                    Session["downloadPSFReports"] = null;
                    logger.Info("Forecast Download Ended");

                    return File(data, "application/octet-stream", reportName + DateTime.Now.ToShortDateString() + DateTime.Now.TimeOfDay + ".xlsx");
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {

            }
            return new EmptyResult();
        }

        #endregion


        #region barchat

        [HttpPost]
        public JsonResult getPSFChart(string PSFbarData)
        {
            string exception;
            List<object> iData = new List<object>();
            string creList = "";
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string role = Session["UserRole"].ToString();
                    int userId = Convert.ToInt32(Session["UserId"]);
                    string userName = Session["UserName"].ToString();


                    reportFilter filter = new reportFilter();
                    if (PSFbarData != null)
                    {
                        filter = JsonConvert.DeserializeObject<reportFilter>(PSFbarData);
                    }

                    string workshopList = filter.selected_Workshop == null ? "" : filter.selected_Workshop;
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

                    string str = @"CALL live_reports_psf(@reportid,@increname,@incremanager,@inworkshopid,@increid,@incampaign);";
                    MySqlParameter[] sqlParameter = new MySqlParameter[]
                    {
                        new MySqlParameter("reportid",reportId),
                        new MySqlParameter("increname", ""),
                        new MySqlParameter("incremanager", ""),
                        new MySqlParameter("inworkshopid", ""),
                        new MySqlParameter("increid", creList),
                        new MySqlParameter("incampaign", "")
                    };

                    List<barchartVM> times = db.Database.SqlQuery<barchartVM>(str, sqlParameter).ToList();


                    if(times.Count>0)
                    {
                        iData.Add(times.Select(m => m.TimeInterval).ToList());
                        iData.Add(times.Select(m => m.Calls).ToList());
                        //DataTable dt = new DataTable();


                        ////Looping and extracting each DataColumn to List<Object>  
                        //foreach (DataColumn dc in dt.Columns)
                        //{
                        //    List<object> x = new List<object>();
                        //    x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                        //    iData.Add(x);
                        //}
                        //Source data returned as JSON  
                        return Json(new {success=true,data=iData,exception="" }, JsonRequestBehavior.AllowGet);
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
    }
}