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
    public class SAROLogController : Controller
    {
        #region ROSA Logs
        // GET: SAROLog
        public ActionResult SAROLog()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {

                    ViewBag.workType = db.Saservicetypes.Select(m => new { workType = m.serviceTypeName , id = m.id }).Distinct().ToList();
                    ViewBag.pendingReason = db.roReasons.Select(m => new { pendingReason = m.reason }).Distinct().ToList();
                   // ViewBag.saservicetypes = db.Saservicetypes.Select(m => new { saservicetypes = m.serviceTypeName }).Distinct().ToList();



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
                TempData["ControllerName"] = "Repair Order";

                return RedirectToAction("LogOff", "Home");
            }
            return View();
        }

        public ActionResult GetBucketData(string repairorderData)
        {
            List<repairOrderVM> rodetails = new List<repairOrderVM>();
            int fromIndex = Convert.ToInt32(Request["start"]);
            int toIndex = Convert.ToInt32(Request["length"]);
            string searchPattern = Request["search[value]"];

            string roStatus, roworkTypes,PRU, roDate,PDT;
            string exception = "";


            int wyzUserId = Convert.ToInt32(Session["UserId"].ToString());

            repairorderFilter filter = new repairorderFilter();
            if (repairorderData != null)
            {
                filter = JsonConvert.DeserializeObject<repairorderFilter>(repairorderData);
            }

            int totalCount = 0;
            long patternCount = 0;

            if (searchPattern != "")
            {
                filter.isFiltered = true;
            }
            int roworkType;
            
            roStatus = filter.roStatus == null ? "" : filter.roStatus;
            roworkType = Convert.ToInt32(filter.roworkType);
           // roworkType = filter.roworkType == null ? "" : filter.roworkType;
            PRU = filter.PRU == null ? "" : filter.PRU;
            PDT = filter.PDT == null ? "" : Convert.ToDateTime(filter.PDT.ToString()).ToString("yyyy-MM-dd");
            roDate = filter.roDate == null ? "" : Convert.ToDateTime(filter.roDate.ToString()).ToString("yyyy-MM-dd");


            using (var db = new AutoSherDBContext())
            {
                try
                {
                    if (filter.getDataFor == 1)//All RO
                    {

                        totalCount = getROcallLogCounts(wyzUserId, "", roworkType, "","","","");
                        if (roworkType == 0 && roStatus == "" && PDT=="" && roDate=="")
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
                        rodetails = getROcallLog(wyzUserId, searchPattern, fromIndex, toIndex, roworkType, roStatus,roDate,PDT,PRU);
                        if (filter.isFiltered == true)
                        {
                            patternCount = getROcallLogCounts(wyzUserId, roStatus, roworkType, searchPattern, roDate, PDT, PRU);
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
            if (rodetails != null)
            {
                if (filter.isFiltered == true)
                {
                    var JsonData = Json(new { data = rodetails, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = patternCount, exception = exception }, JsonRequestBehavior.AllowGet);
                    JsonData.MaxJsonLength = int.MaxValue;
                    return JsonData;
                }
                else if (filter.isFiltered == false)
                {
                    var JsonData = Json(new { data = rodetails, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount, exception = exception }, JsonRequestBehavior.AllowGet);
                    JsonData.MaxJsonLength = int.MaxValue;
                    return JsonData;
                }
            }
            return Json(new { data = "", draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0, exception = exception }, JsonRequestBehavior.AllowGet);
        }
        public int getROcallLogCounts(long wyzuserId, string roStatus, int roWorkType, string pattern,string roDate,string PDT,string PRU)
        {
            int totalCount = 0;
            using (var db = new AutoSherDBContext())
            {
                totalCount = db.Database.SqlQuery<int>("call sa_calllogcount(@userid,@servicetypes,@rostatusin,@pattern,@roDate,@PDT,@PRU)",
                      new MySqlParameter[] {
                            new MySqlParameter("@userid", wyzuserId),
                            new MySqlParameter("@servicetypes",roWorkType),
                            new MySqlParameter("@rostatusin",roStatus),
                            new MySqlParameter("@pattern",pattern),
                            new MySqlParameter("@roDate",roDate),
                            new MySqlParameter("@PDT",PDT),
                            new MySqlParameter("@PRU",PRU)
                      }).FirstOrDefault();

                return totalCount;

            }

        }
        public List<repairOrderVM> getROcallLog(int wyzUserId, string searchPattern, int fromIndex, int toIndex, int roworkType, string roStatus, string roDate, string PDT, string PRU)
        {
            List<repairOrderVM> callLogDispositions = null;
            using (var db = new AutoSherDBContext())
            {

                string str = @"CALL sa_calllog(@userid,@pattern,@start_with,@length,@servicetypes,@rostatusin,@roDate,@PDT,@PRU)";

                MySqlParameter[] param = new MySqlParameter[]
                {
                        new MySqlParameter("@userid", wyzUserId),
                        new MySqlParameter("@pattern", searchPattern),
                        new MySqlParameter("@start_with", fromIndex),
                        new MySqlParameter("@length", toIndex),
                        new MySqlParameter("@servicetypes", roworkType),
                        new MySqlParameter("@rostatusin", roStatus),
                        new MySqlParameter("@roDate",roDate),
                            new MySqlParameter("@PDT",PDT),
                            new MySqlParameter("@PRU",PRU)
                };

                callLogDispositions = db.Database.SqlQuery<repairOrderVM>(str, param).ToList();
            }
            return callLogDispositions;
        }
        #endregion

        #region ROSA Reports
        public ActionResult SAROReports()
        {

            return View();
        }

        public ActionResult getSAROReports(string reportId)
        {
            string exception = "";
            long userId = Convert.ToInt64(Session["UserId"]);
            DataTable ROReports = new DataTable();

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string conStr = ConfigurationManager.ConnectionStrings["AutoSherContext"].ConnectionString;

                    using (MySqlConnection connection = new MySqlConnection(conStr))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("live_reports_detailedRO", connection))
                        {
                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@reportid", reportId));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@wyzuserid", userId));
                            adapter.Fill(ROReports);
                        }
                    }
                    var results = JsonConvert.SerializeObject(ROReports, Formatting.Indented,
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
                var results = JsonConvert.SerializeObject(ROReports, Formatting.Indented,
                     new JsonSerializerSettings
                     {
                         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                     });
                return Json(new { data = results, exception = exception }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region  Report Download
        public ActionResult downloadSAReports()
        {
            try
            {
                string role = Session["UserRole"].ToString();
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                string userName = Session["UserName"].ToString();

                using (var db = new AutoSherDBContext())
                {

                    var workshopLists = db.workshops.Select(m => new { m.id, m.workshopName }).ToList();
                    if(role== "Service Advisor")
                    {
                        var wyuserLists = db.wyzusers.Where(m => m.id == UserId).Select(m => new { id = m.id, name = m.userName + "(" + m.firstName + ")" }).ToList();
                        ViewBag.wyuserLists = wyuserLists.OrderBy(m => m.name);
                    }
                    else if(role == "CREManager")
                    {
                        var wyuserLists = db.wyzusers.Where(m => m.role == "Service Advisor" && m.creManager==userName).Select(m => new { id = m.id, name = m.userName + "(" + m.firstName + ")" }).ToList();
                        ViewBag.wyuserLists = wyuserLists.OrderBy(m => m.name);
                    }
                    else
                    {
                        var wyuserLists = db.wyzusers.Where(m => m.role == "Service Advisor").Select(m => new { id = m.id, name = m.userName + "(" + m.firstName + ")" }).ToList();
                        ViewBag.wyuserLists = wyuserLists.OrderBy(m => m.name);
                    }
                    var worktypeLists = db.Saservicetypes.Select(m => new { name = m.serviceTypeName , id = m.id} ).Distinct().ToList();
                    ViewBag.workshopLists = workshopLists.OrderBy(m => m.workshopName);
                    ViewBag.worktypeLists = worktypeLists.OrderBy(m=>m.name);
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

        public ActionResult SADownloadFilter(string SAFilterLists)
        {
            DataTable downloadExceltable = new DataTable();
            int reportId;
            string connectionId,callfromDate,calltoDate, reportName, workshopLists, creLists,missesfromdate,missedtodate,worktypes, pending, roStatusLists, assigndateTo, assigndateFrom;
            downloadReportFilter filter = JsonConvert.DeserializeObject<downloadReportFilter>(SAFilterLists);
            connectionId = filter.connectionId;
            int UserId = Convert.ToInt32(Session["UserId"].ToString());
            string role = Session["UserRole"].ToString();
            string userName = Session["UserName"].ToString();
            string wyuserListIds = string.Empty;
            try
            {
              
                reportId = Convert.ToInt32(filter.reportId);
                callfromDate = filter.callfromDate == null || filter.callfromDate == "" ? "" : Convert.ToDateTime(filter.callfromDate.ToString()).ToString("yyyy-MM-dd");
                calltoDate = filter.calltoDate == null || filter.calltoDate == "" ? "" : Convert.ToDateTime(filter.calltoDate.ToString()).ToString("yyyy-MM-dd");
                missesfromdate = filter.billdateFrom == null || filter.billdateFrom == "" ? "" : Convert.ToDateTime(filter.billdateFrom.ToString()).ToString("yyyy-MM-dd");
                missedtodate = filter.billdateTo == null || filter.billdateTo == "" ? "" : Convert.ToDateTime(filter.billdateTo.ToString()).ToString("yyyy-MM-dd");
                assigndateFrom = filter.assigndateFrom == null || filter.assigndateFrom == "" ? "" : Convert.ToDateTime(filter.assigndateFrom.ToString()).ToString("yyyy-MM-dd");
                assigndateTo = filter.assigndateTo == null || filter.assigndateTo == "" ? "" : Convert.ToDateTime(filter.assigndateTo.ToString()).ToString("yyyy-MM-dd");
                reportName = filter.reportName;
                workshopLists = filter.workshopLists == null ? "" : filter.workshopLists;
                roStatusLists = filter.roStatus == null ? "" : filter.roStatus;
                worktypes = filter.worktypelists == null ? "" : filter.worktypelists;
                creLists = filter.creLists == null ? "" : filter.creLists;
                pending = filter.ispending == null ? "" :filter.ispending;

                using (var db = new AutoSherDBContext())
                {

                    if (role == "Service Advisor")
                    {
                        wyuserListIds = UserId.ToString();
                    }
                    else if (role == "CREManager")
                    {
                        var wyuserLists = db.wyzusers.Where(m => m.role == "Service Advisor" && m.creManager == userName).Select(m => m.id).ToList();
                        wyuserListIds = string.Join(",", wyuserLists);
                    }
                    else
                    {
                        var wyuserLists = db.wyzusers.Where(m => m.role == "Service Advisor").Select(m => m.id).ToList();
                        wyuserListIds = string.Join(",", wyuserLists);
                    }

                    downloadExceltable = getSAReportProcedureData(reportId, callfromDate, calltoDate, wyuserListIds, creLists,workshopLists,missesfromdate,missedtodate,worktypes,pending,roStatusLists,assigndateFrom,assigndateTo);
                    if(downloadExceltable.Rows.Count>0)
                    {
                        Response.Clear();

                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("SA.xlsx", System.Text.Encoding.UTF8));
                        using (ExcelPackage pck = new ExcelPackage())
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("SA");
                            ws.Cells["A1"].LoadFromDataTable(downloadExceltable, true);
                            Session["downloadSAReports"] = pck.GetAsByteArray();
                        }

                        // new liveProgressbar().SendProgress(connectionId, "Processing....(" + totalCount + "/" + totalCount + ")", totalCount, totalCount);
                        return Json(new { success = true });
                    }
                    else
                    {
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
             //   new liveProgressbar().SendProgress(connectionId, "Processing....", 100, 100);
                return Json(new { success = false, error = exception }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SADownloadALL(string reportName)
        {
            try
            {

                if (Session["downloadSAReports"] != null)
                {
                    byte[] data = Session["downloadSAReports"] as byte[];
                    Session["downloadSAReports"] = null;
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


        public DataTable getSAReportProcedureData(int reportId,string callfromDate,string calltoDate,string UserIds,string serviceadvID,string workshopids,string missesfromdate, string missedtodate, string worktypes, string pending,string roStatusLists,string assigndateFrom,string assigndateTo)
        {
            DataTable downloadExceltable = new DataTable();
            //try
            //{
                using (var db = new AutoSherDBContext())
                {
                    string conStr = ConfigurationManager.ConnectionStrings["AutoSherContext"].ConnectionString;

                    using (MySqlConnection connection = new MySqlConnection(conStr))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("live_hyundaiSA_report", connection))
                        {
                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@bucket", reportId));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@incalldatefrom", callfromDate));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@incalldateto", calltoDate));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@wyzid", UserIds));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@inserviceadvisorlistId", serviceadvID));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@inworkshoplistid", workshopids));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@inmissedrodatefrom", missesfromdate));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@inmissedrodateto", missedtodate));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@inworktype", worktypes));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@inpendinglists", pending));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@inroStatusLists", roStatusLists));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@inassigndatefrom", assigndateFrom));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("@inassigndatedateto", assigndateTo));
                            adapter.Fill(downloadExceltable);
                        }
                    }
                    

                }
            //}
            //catch(Exception ex)
            //{

            //}
            return downloadExceltable;
        }


        public ActionResult getLiveSAReports(string reportId)
        {
            string exception = "";
            DataTable SAReports = new DataTable();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string role = Session["UserRole"].ToString();
                    string userName = Session["UserName"].ToString();
                    string wyuserListIds = string.Empty;
                    int userId = Convert.ToInt32(Session["UserId"]);
                    int bucketId = Convert.ToInt32(reportId);
                    if (role == "Service Advisor")
                    {
                        wyuserListIds = userId.ToString();
                    }
                    else if (role == "CREManager")
                    {
                        var wyuserLists = db.wyzusers.Where(m => m.role == "Service Advisor" && m.creManager == userName).Select(m => m.id).ToList();
                        wyuserListIds = string.Join(",", wyuserLists);
                    }
                    else
                    {
                        var wyuserLists = db.wyzusers.Where(m => m.role == "Service Advisor").Select(m => m.id).ToList();
                        wyuserListIds = string.Join(",", wyuserLists);
                    }
                    SAReports = getSAReportProcedureData(bucketId, "", "", wyuserListIds, "","","","","","","","","");


                    var results = JsonConvert.SerializeObject(SAReports, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
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
                var results = JsonConvert.SerializeObject(SAReports, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                return Json(new { data = results, exception = exception }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region SA Customer Search
        public ActionResult saCustomerSearch()
        {
            return View();
        }
        public ActionResult getSASearchDetails(string pattern)
        {
            string exception = "";
            List<SAcustomerSearch> searchDetails = new List<SAcustomerSearch>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    db.Database.CommandTimeout = 900;
                    string str = @"CALL customer_search_sa(@pattern);";

                    MySqlParameter[] sqlParameter = new MySqlParameter[]
                    {
                            new MySqlParameter("pattern", pattern)

                    };
                    searchDetails = db.Database.SqlQuery<SAcustomerSearch>(str, sqlParameter).ToList();
                }
                return Json(new { data = searchDetails, exception = exception }, JsonRequestBehavior.AllowGet);

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
                return Json(new { data = "", exception = exception }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

    }
}