using AutoSherpa_project.Models;
using AutoSherpa_project.Models.API_Model;
using AutoSherpa_project.Models.ViewModels;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
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
    public class InsuranceLiveFormController : Controller
    {
        // GET: InsuranceLiveForm
        public ActionResult InsuranceLive()
        {
            try
            {
                string userName = Session["UserName"].ToString();
                string role = Session["UserRole"].ToString();
                int userId = Convert.ToInt32(Session["UserId"]);

                using (var db = new AutoSherDBContext())
                {
                    var campaignList = db.campaigns.Where(m => m.campaignType == "Insurance").Select(m => new { id = m.id, campaignName = m.campaignName }).ToList();
                    var workshopList = db.workshops.Select(m => new { workshopName = m.workshopName, id = m.id }).ToList();
                    if (role == "CREManager")
                    {
                        //var ddlmanager = db.wyzusers.Where(m => m.userName == userName).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        //ViewBag.ddlmanager = ddlmanager;

                        var ddlcres = db.wyzusers.Where(m => m.creManager == userName && m.role == "CRE" && m.unAvailable==false).Select(model => new { id = model.id, userName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.userName).ToList();
                        ViewBag.ddlcreList = ddlcres;

                    }
                    else if (role == "Admin")
                    {
                        var ddlcres = db.wyzusers.Where(m => m.role1 == "2" && m.role == "CRE" && m.unAvailable == false).Select(model => new { id = model.id, userName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.userName).ToList();
                        //var ddlmanager = db.wyzusers.Where(m => m.role1 == "1" && m.role == "CREManager").Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        ViewBag.ddlcreList = ddlcres;
                        //ViewBag.ddlmanager = ddlmanager;

                    }
                    else if (role == "RM" || role == "WM")
                    {
                        var creManagerIdList = db.AccessLevels.Where(m => m.rmId == userId).Select(m => new { m.creManagerId, m.creId }).Distinct().ToList();
                        var cresList = creManagerIdList.Select(m => m.creId).ToList();
                        var managerList = creManagerIdList.Select(m => m.creManagerId).ToList();


                        var ddlcres = db.wyzusers.Where(x => cresList.Contains(x.id) && x.role1 == "2" && x.role == "CRE" && x.unAvailable == false).Select(model => new { id = model.id, userName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.userName).ToList();
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
                TempData["ControllerName"] = "InsuranceLiveForm";

                return RedirectToAction("LogOff", "Home");
            }
            return View();
        }


        public ActionResult InsuranceLiveloadDashboardCounts()
        {
            int BoxInsuranceLiveCRE, BoxInsuranceLiveDataAvail, BoxInsuranceLiveCalls, BoxInsuranceLiveCRECalls, BoxInsuranceLiveCalls1, BoxInsuranceLiveNotLoggedCRECalls, BoxInsuranceLiveCRECalls1, BoxInsuranceLiveContact, BoxInsuranceLiveContactPercent, BoxInsuranceLiveFreshBook, BoxInsuranceLiveCancelled, BoxInsuranceLiveReschedule;
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

                    BoxInsuranceLiveCRE = db.Database.SqlQuery<int>("call live_reports_ins(@reportid,@increname,@incremanager,@inworkshopid,@increid,@indatefrom,@indateto,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 1), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshopid", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@indatefrom", ""), new MySqlParameter("@indateto", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxInsuranceLiveFreshBook = db.Database.SqlQuery<int>("call live_reports_ins(@reportid,@increname,@incremanager,@inworkshopid,@increid,@indatefrom,@indateto,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 2), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshopid", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@indatefrom", ""), new MySqlParameter("@indateto", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxInsuranceLiveCRECalls = db.Database.SqlQuery<int>("call live_reports_ins(@reportid,@increname,@incremanager,@inworkshopid,@increid,@indatefrom,@indateto,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 3), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshopid", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@indatefrom", ""), new MySqlParameter("@indateto", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxInsuranceLiveCalls = db.Database.SqlQuery<int>("call live_reports_ins(@reportid,@increname,@incremanager,@inworkshopid,@increid,@indatefrom,@indateto,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 4), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshopid", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@indatefrom", ""), new MySqlParameter("@indateto", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxInsuranceLiveContactPercent = db.Database.SqlQuery<int>("call live_reports_ins(@reportid,@increname,@incremanager,@inworkshopid,@increid,@indatefrom,@indateto,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 5), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshopid", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@indatefrom", ""), new MySqlParameter("@indateto", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxInsuranceLiveContact = db.Database.SqlQuery<int>("call live_reports_ins(@reportid,@increname,@incremanager,@inworkshopid,@increid,@indatefrom,@indateto,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 6), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshopid", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@indatefrom", ""), new MySqlParameter("@indateto", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxInsuranceLiveReschedule = db.Database.SqlQuery<int>("call live_reports_ins(@reportid,@increname,@incremanager,@inworkshopid,@increid,@indatefrom,@indateto,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 7), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshopid", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@indatefrom", ""), new MySqlParameter("@indateto", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxInsuranceLiveCancelled = db.Database.SqlQuery<int>("call live_reports_ins(@reportid,@increname,@incremanager,@inworkshopid,@increid,@indatefrom,@indateto,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 8), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshopid", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@indatefrom", ""), new MySqlParameter("@indateto", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxInsuranceLiveDataAvail = db.Database.SqlQuery<int>("call live_reports_ins(@reportid,@increname,@incremanager,@inworkshopid,@increid,@indatefrom,@indateto,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 9), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshopid", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@indatefrom", ""), new MySqlParameter("@indateto", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxInsuranceLiveNotLoggedCRECalls = db.Database.SqlQuery<int>("call live_reports_ins(@reportid,@increname,@incremanager,@inworkshopid,@increid,@indatefrom,@indateto,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 10), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshopid", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@indatefrom", ""), new MySqlParameter("@indateto", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxInsuranceLiveCalls1 = db.Database.SqlQuery<int>("call live_reports_ins(@reportid,@increname,@incremanager,@inworkshopid,@increid,@indatefrom,@indateto,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 11), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshopid", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@indatefrom", ""), new MySqlParameter("@indateto", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxInsuranceLiveCRECalls1 = db.Database.SqlQuery<int>("call live_reports_ins(@reportid,@increname,@incremanager,@inworkshopid,@increid,@indatefrom,@indateto,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 12), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshopid", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@indatefrom", ""), new MySqlParameter("@indateto", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
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

            return Json(new { success = true, BoxInsuranceLiveCRE, BoxInsuranceLiveDataAvail, BoxInsuranceLiveCalls, BoxInsuranceLiveCRECalls, BoxInsuranceLiveCalls1, BoxInsuranceLiveNotLoggedCRECalls, BoxInsuranceLiveCRECalls1, BoxInsuranceLiveContact, BoxInsuranceLiveContactPercent, BoxInsuranceLiveFreshBook, BoxInsuranceLiveCancelled, BoxInsuranceLiveReschedule });
        }
        public ActionResult getINSReports(string INSData)
        {
            string exception = "";
            DataTable INSReports = new DataTable();

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    
                    string role = Session["UserRole"].ToString();
                    long userId = Convert.ToInt64(Session["UserId"]);

                    reportFilter filter = new reportFilter();
                    if (INSData != null)
                    {
                        filter = JsonConvert.DeserializeObject<reportFilter>(INSData);
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
                        //Session["UserName"].ToString();
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
                        using (MySqlCommand cmd = new MySqlCommand("live_reports_ins", connection))
                        {
                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("reportid", reportId));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("increname", CREName));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("incremanager", manageruserName));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("inworkshop", workshopList));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("increid", creList));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("indatefrom", fromDate));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("indateto", toDate));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("incampaign", CampaignList));
                            adapter.Fill(INSReports);
                        }
                    }
                    
                    var results = JsonConvert.SerializeObject(INSReports, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
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
                var results = JsonConvert.SerializeObject(INSReports, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                return Json(new { data = results, exception = exception }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult downloadNotDialedCRE()
        {
            return View();
        }


        #region Insurance Report Download

        public ActionResult INSDataLogsForm()
        {
            try
            {
                string role = Session["UserRole"].ToString();
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                string userName = Session["UserName"].ToString();

                using (var db = new AutoSherDBContext())
                {

                    var ddlcampaignList = db.campaigns.Where(m => m.campaignType == "Forecast" || m.campaignType == "Insurance" && m.isactive == true).ToList();
                    var ddlWorkshop = db.workshops.Select(model => new { workshopId = model.id, workshopName = model.workshopName }).OrderBy(m => m.workshopName).ToList();



                    if (role == "CREManager")
                    {
                        var ddlmanager = db.wyzusers.Where(m => m.userName== userName).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        ViewBag.ddlmanager = ddlmanager;

                        var ddlcres = db.wyzusers.Where(m => m.creManager == userName && m.role == "CRE" && m.unAvailable == false).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        ViewBag.ddlcres = ddlcres;

                    }
                    else if (role == "Admin")
                    {
                        var ddlcres = db.wyzusers.Where(m => m.role1 == "2" && m.role == "CRE" && m.unAvailable == false).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        var ddlmanager = db.wyzusers.Where(m => m.role1 == "2" && m.role == "CREManager" && m.unAvailable == false).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        ViewBag.ddlcres = ddlcres;
                        ViewBag.ddlmanager = ddlmanager;

                    }
                    else if (role == "RM" || role == "WM")
                    {
                        var creManagerIdList = db.AccessLevels.Where(m => m.rmId == UserId).Select(m => new { m.creManagerId, m.creId }).Distinct().ToList();
                        var cresList = creManagerIdList.Select(m => m.creId).ToList();
                        var managerList = creManagerIdList.Select(m => m.creManagerId).ToList();


                        var ddlcres = db.wyzusers.Where(x => cresList.Contains(x.id) && x.role1=="2" && x.role == "CRE" && x.unAvailable == false).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        var ddlmanager = db.wyzusers.Where(x => managerList.ToList().Contains(x.id) && x.role1 == "2" && x.role == "CREManager" && x.unAvailable == false).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
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
                TempData["ControllerName"] = "InsuranceLiveForm";

                return RedirectToAction("LogOff", "Home");
            }
            return View();
        }
        public ActionResult INSDownloadFilter(string INSdownloadFilters)
        {
            int totalCount = 0, toIndex = 20000;
            int reportId, smsType;
            string workshopLists, smstypeLists, smsstatusLists, campaignLists, creLists, callfromDate, calltoDate, reportName, connectionId;

            downloadReportFilter filter = JsonConvert.DeserializeObject<downloadReportFilter>(INSdownloadFilters);
            connectionId = filter.connectionId;

            try
            {
                DataTable downloadExceltable = new DataTable();
                reportId = Convert.ToInt32(filter.reportId);
                workshopLists = filter.workshopLists == null ? "" : filter.workshopLists;
                smstypeLists = filter.smstypeLists == null ? "" : filter.smstypeLists;
                smsType = filter.smstype == null || filter.smstype == "" ? 0 : Convert.ToInt32(filter.smstype);
                smsstatusLists = filter.smsstatusLists == null ? "" : filter.smsstatusLists;
                campaignLists = filter.campaignLists == null ? "" : filter.campaignLists;
                creLists = filter.creLists == null ? "" : filter.creLists;
                callfromDate = filter.callfromDate == null || filter.callfromDate == "" ? "" : Convert.ToDateTime(filter.callfromDate.ToString()).ToString("yyyy-MM-dd");
                calltoDate = filter.calltoDate == null || filter.calltoDate == "" ? "" : Convert.ToDateTime(filter.calltoDate.ToString()).ToString("yyyy-MM-dd");
                reportName = filter.reportName;
                string conStr = ConfigurationManager.ConnectionStrings["AutoSherContext"].ConnectionString;
                if (reportId == 1)
                {
                    creLists = Session["UserId"].ToString();
                }

                using (var db = new AutoSherDBContext())
                {
                    new liveProgressbar().SendProgress(connectionId, "Processing....(" + 0 + " /" + totalCount + ")", 0, 0);
                    totalCount = getdownloadCount(reportId, workshopLists, smstypeLists, smsstatusLists, campaignLists, creLists, callfromDate, calltoDate, smsType);

                    if (totalCount > 0)
                    {
                        for (int i = 0; i < totalCount; i += 20000)
                        {
                            using (MySqlConnection connection = new MySqlConnection(conStr))
                            {
                                using (MySqlCommand cmd = new MySqlCommand("live_insurancereportsQuerys", connection))
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
                        Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("Insurance.xlsx", System.Text.Encoding.UTF8));
                        using (ExcelPackage pck = new ExcelPackage())
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Insurance");
                            ws.Cells["A1"].LoadFromDataTable(downloadExceltable, true);
                            Session["downloadINSReports"] = pck.GetAsByteArray();
                        }

                        new liveProgressbar().SendProgress(connectionId, "Processing....(" + totalCount + "/" + totalCount + ")", totalCount, totalCount);
                        return Json(new { success = true });


                    }
                    else
                    {
                        new liveProgressbar().SendProgress(connectionId, "Processing....", 100, 100);
                        return Json(new { success = false, error = "No Data Found." }, JsonRequestBehavior.AllowGet);

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

        public int getdownloadCount(int reportId, string workshopLists, string smstypeLists, string smsstatusLists, string campaignLists, string creLists, string callfromDate, string calltoDate, int smstype)
        {
            int totalCount = 0;


            using (var db = new AutoSherDBContext())
            {

                totalCount = db.Database.SqlQuery<int>("call live_INsurancereportsQuerys_Counts(@reportid,@fromcalldate,@tocalldate,@inwyzuser_id,@inworkshop_id,@incampaign_id,@insmstype, @insmsstatustype, @smstype)",
                            new MySqlParameter("@reportid", reportId),
                            new MySqlParameter("@fromcalldate", callfromDate),
                            new MySqlParameter("@tocalldate", calltoDate),
                            new MySqlParameter("@inwyzuser_id", creLists),
                            new MySqlParameter("@inworkshop_id", workshopLists),
                            new MySqlParameter("@incampaign_id", campaignLists),
                            new MySqlParameter("@insmstype", smstypeLists),
                            new MySqlParameter("@insmsstatustype", smsstatusLists),
                            new MySqlParameter("@smstype", smstype)).FirstOrDefault();
            }
            return totalCount;
        }

        #endregion

        public ActionResult getCRES(string managerIds)
        {
            try
            {
                string role = Session["UserRole"].ToString();
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                string userName = Session["UserName"].ToString();

                using (var db = new AutoSherDBContext())
                {
                    var cres = db.Database.SqlQuery<creLists>("select id,CONCAT(firstName, '(', userName, ')') as UName from wyzuser where      role='CRE' and unAvailable=false and creManager in(select userName from wyzuser where   FIND_IN_SET(id,@Id))  order by userName ;", new MySqlParameter("@Id", managerIds)).ToList();
                    return Json(new { success = true, cres = cres }, JsonRequestBehavior.AllowGet);

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

        public ActionResult DownloadINSALL(string reportName)
        {
            try
            {

                if (Session["downloadINSReports"] != null)
                {

                    byte[] data = Session["downloadINSReports"] as byte[];
                    Session["downloadINSReports"] = null;
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
        public JsonResult getINSChart(string INSbarData)
        {
            string exception;
            List<object> iData = new List<object>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string role = Session["UserRole"].ToString();
                    int userId = Convert.ToInt32(Session["UserId"]);
                    string userName = Session["UserName"].ToString();

                    reportFilter filter = new reportFilter();
                    if (INSbarData != null)
                    {
                        filter = JsonConvert.DeserializeObject<reportFilter>(INSbarData);
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


                    string str = @"CALL live_reports_ins(@reportid,@increname,@incremanager,@inworkshopid,@increid,@indatefrom,@indateto,@incampaign);";
                    MySqlParameter[] sqlParameter = new MySqlParameter[]
                    {
                        new MySqlParameter("reportid",reportId),
                        new MySqlParameter("increname", ""),
                        new MySqlParameter("incremanager", ""),
                        new MySqlParameter("inworkshopid", ""),
                        new MySqlParameter("increid", creList),
                        new MySqlParameter("indatefrom", ""),
                        new MySqlParameter("indateto", ""),
                        new MySqlParameter("incampaign", "")

                    };

                    List<barchartVM> times  = db.Database.SqlQuery<barchartVM>(str, sqlParameter).ToList();


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
    }
    public class creLists
        {
        public long id { get; set; }
        public string UName { get; set; }
        }
}