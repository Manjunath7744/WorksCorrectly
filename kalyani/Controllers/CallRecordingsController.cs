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
    public class CallRecordingsController : Controller
    {
        //public int //recordFiltered=0;
        // GET: CallRecordings
        [ActionName("Call_Recordings")]
        public ActionResult CallRecordings(int id)
        {
            CallRecordingViewModel callRecords = new CallRecordingViewModel();
            callRecords.campaignList = new List<campaign>();
            try
            {
                using(AutoSherDBContext db=new AutoSherDBContext())
                {
                    if (id == 1)
                    {
                        ViewBag.recordingFor = "SMR";
                        callRecords.dispoList = getDispositionList(1);
                        callRecords.campaignList.AddRange(getCampainTypeByName("Campaign"));
                        callRecords.campaignList.AddRange(getCampainTypeByName("Service Reminder"));
                        callRecords.reasonsList = getDispositionList(4);
                        callRecords.creList = getCREList(Session["UserName"].ToString(), "smr");
                    }
                    else if (id == 2)
                    {
                        ViewBag.recordingFor = "IR";
                        callRecords.dispoList = getDispositionList(2);
                        callRecords.campaignList.AddRange(getCampainTypeByName("Insurance"));
                        callRecords.reasonsList = new List<calldispositiondata>();
                        callRecords.creList = getCREList(Session["UserName"].ToString(), "ir");
                    }
                    else if (id == 3)
                    {
                        ViewBag.recordingFor = "PSF";
                        callRecords.dispoList = getDispositionList(3);
                        callRecords.campaignList.AddRange(getCampainTypeByName("PSF"));
                        callRecords.reasonsList = new List<calldispositiondata>();
                        callRecords.creList = getCREList(Session["UserName"].ToString(), "psf");
                    }
                    else if (id == 4)
                    {
                        ViewBag.recordingFor = "Other";
                        callRecords.dispoList = new List<calldispositiondata>();
                        callRecords.campaignList = new List<campaign>();
                        callRecords.reasonsList = new List<calldispositiondata>();
                        callRecords.creList = getCREList(Session["UserName"].ToString(), "oth");
                    }
                    else if (id == 5)
                    {
                        ViewBag.recordingFor = "postsales";
                        callRecords.dispoList = getDispositionList(5);
                        callRecords.campaignList.AddRange(getCampainTypeByName("Campaign"));
                        callRecords.campaignList.AddRange(getCampainTypeByName("Service Reminder"));
                        callRecords.reasonsList = getDispositionList(5);
                        callRecords.creList = getCREList(Session["UserName"].ToString(), "postsales");
                    }
                    else
                    {
                        return RedirectToAction("LogOff", "Home");
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("LogOff", "Home");
            }
            return View(callRecords);
        }

        public List<calldispositiondata> getDispositionList(int disFor)
        {
            List<calldispositiondata> dispoList = new List<calldispositiondata>();
            List<int> dispoIds = new List<int>();

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (disFor == 1)//Service.dispo
                    {
                        dispoList = db.calldispositiondatas.Where(m => m.dispositionId == 3 || m.dispositionId == 4 || m.dispositionId == 5 ||
                        m.dispositionId == 6 || m.dispositionId == 7 || m.dispositionId == 8 || m.dispositionId == 9 || m.dispositionId == 10).ToList();
                    }
                    else if (disFor == 2)//IR
                    {
                        dispoList = db.calldispositiondatas.Where(m => m.dispositionId == 25 || m.dispositionId == 4 || m.dispositionId == 6 ||
                        m.dispositionId == 7 || m.dispositionId == 8 || m.dispositionId == 9 || m.dispositionId == 10 || m.dispositionId == 26).ToList();
                    }
                    else if (disFor == 3)//PSF
                    {
                        dispoList = db.calldispositiondatas.Where(m => m.dispositionId == 25 || m.dispositionId == 22 || m.dispositionId == 36 ||
                        m.dispositionId == 4 || m.dispositionId == 6 || m.dispositionId == 7 || m.dispositionId == 8 || m.dispositionId == 9 || m.dispositionId == 10).ToList();
                    }
                    else if (disFor == 4)
                    {
                        dispoList = db.calldispositiondatas.Where(m => m.mainDispositionId == 5).ToList();
                    }
                    else if (disFor == 5)//postsales
                    {
                        dispoList = db.calldispositiondatas.Where(m => m.dispositionId == 3 || m.dispositionId == 4 || m.dispositionId == 5 ||
                        m.dispositionId == 6 || m.dispositionId == 7 || m.dispositionId == 8 || m.dispositionId == 9 || m.dispositionId == 10).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return dispoList;
        }

        public List<campaign> getCampainTypeByName(string name)
        {
            List<campaign> campaignList = new List<campaign>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    campaignList = db.campaigns.Where(m => m.campaignType == "Forecast" || m.campaignType == name && m.isactive == true).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return campaignList;
        }

        public List<wyzuser> getCREList(string creManager, string moduleType)
        {
            List<wyzuser> cres = new List<wyzuser>();
            try
            {
                var rmid = long.Parse(Session["UserId"].ToString());

                using (var db = new AutoSherDBContext())
                {
                    List<long> creManagerIdList = db.AccessLevels.Where(x => x.rmId == rmid).Select(x => x.creManagerId).Distinct().ToList();
                    List<string> creManagerNameList = db.wyzusers.Where(x => creManagerIdList.Contains(x.id)).Select(x => x.userName).ToList();
                    var managerList = string.Join(",", creManagerNameList);
                    if (Session["UserRole"].ToString() == "RM" || Session["UserRole"].ToString() == "WM")
                    {
                        if (moduleType == "smr")
                        {
                            cres = db.wyzusers.Where(m => managerList.Contains(m.creManager) && m.role == "CRE" && m.unAvailable==false && m.insuranceRole != true && m.role1 != "4").ToList();
                        }
                        else if (moduleType == "ir")
                        {
                            cres = db.wyzusers.Where(m => managerList.Contains(m.creManager) && m.role == "CRE" && m.unAvailable == false && m.insuranceRole == true).ToList();
                        }
                        else if (moduleType == "psf")
                        {
                     
                            cres = db.wyzusers.Where(m => managerList.Contains(m.creManager) && m.role == "CRE" && m.unAvailable == false  && m.insuranceRole != true && m.role1 == "4").ToList();
                        }
                        else if (moduleType == "oth")
                        {
                            cres = db.wyzusers.Where(m => managerList.Contains(m.creManager) && m.role == "CRE" && m.unAvailable == false).ToList();
                        }
                        else if (moduleType == "postsales")
                        {

                            cres = db.wyzusers.Where(m => managerList.Contains(m.creManager) && m.role == "CRE" && m.unAvailable == false && m.insuranceRole != true && m.role1 == "5").ToList();
                        }
                    }
                    else if (Session["UserRole"].ToString() == "Admin")
                    {
                        if (moduleType == "smr")
                        {
                            cres = db.wyzusers.Where(m => m.role == "CRE" && m.unAvailable == false  && m.insuranceRole != true && m.role1 != "4").ToList();
                        }
                        else if (moduleType == "ir")
                        {
                            cres = db.wyzusers.Where(m => m.role == "CRE" && m.unAvailable == false && m.insuranceRole == true).ToList();
                        }
                        else if (moduleType == "psf")
                        {
                            cres = db.wyzusers.Where(m => m.role == "CRE" && m.unAvailable == false && m.insuranceRole != true && m.role1 == "4").ToList();
                        }
                        else if (moduleType == "oth")
                        {
                            cres = db.wyzusers.Where(m => m.role == "CRE" && m.unAvailable == false).ToList();
                        }
                        else if (moduleType == "postsales")
                        {
                            cres = db.wyzusers.Where(m => m.role == "CRE" && m.unAvailable == false && m.insuranceRole != true && m.role1 == "5").ToList();
                        }
                    }

                    else
                    {
                        if (moduleType == "smr")
                        {
                            cres = db.wyzusers.Where(m => m.creManager == creManager && m.role == "CRE" && m.unAvailable == false && m.insuranceRole != true && m.role1 != "4").ToList();
                        }
                        else if (moduleType == "ir")
                        {
                            cres = db.wyzusers.Where(m => m.creManager == creManager && m.role == "CRE" && m.unAvailable == false && m.insuranceRole == true).ToList();
                        }
                        else if (moduleType == "psf")
                        {
                            cres = db.wyzusers.Where(m => m.creManager == creManager && m.role == "CRE" && m.unAvailable == false && m.insuranceRole != true && m.role1 == "4").ToList();
                        }
                        else if (moduleType == "oth")
                        {
                            cres = db.wyzusers.Where(m => m.creManager == creManager && m.role == "CRE" && m.unAvailable == false).ToList();
                        }
                        else if (moduleType == "postsales")
                        {
                            cres = db.wyzusers.Where(m => m.creManager == creManager && m.role == "CRE" && m.unAvailable == false && m.insuranceRole != true && m.role1 == "5").ToList();
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return cres;
        }


        public ActionResult getCallData(string jsonData)
        {
            //Ajax Table Options
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            int loginId = Convert.ToInt32(Session["UserId"]);
            string searchPattern = Request["search[value]"];
            string exception = "";
            /*
            Note
             DataFor and filter.FilterFor is used to know recording for SMR,IR,PSF,Other,postsales
            DataFor=1-----Service(SMR)
            DataFor=2-----IR, DataFor=3------PSF, DataFor=4---------Other,DataFor=5-----postsales
            */

            RecordingFilters filter = new RecordingFilters();
            List<CallRecordingDataTable> CallDt = new List<CallRecordingDataTable>();
            int moduletype = 0;
            string userName = " ";
            var a = Session["UserName"].ToString().ToList();
            long fromIndex = 0, toIndex = 0;
            int totalCount = 0, filteredCount = 0;

            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    if (Session["UserRole"].ToString() == "RM" || Session["UserRole"].ToString() == "WM")
                    {
                        List<long> managerList = db.AccessLevels.Where(x => x.rmId == loginId).Select(x => x.creManagerId).Distinct().ToList();
                        List<string> userNameStr = db.wyzusers.Where(x => managerList.Contains(x.id)).Select(x => x.userName).ToList();
                        userName = string.Join(",", userNameStr);
                    }
                    else if (Session["UserRole"].ToString()=="Admin")
                    {
                        if (Session["LoginUser"].ToString() == "Insurance")
                        {
                            List<string> managerList = db.wyzusers.Where(x => x.role == "creManager" && x.insuranceRole == true).Select(x => x.userName).ToList();
                            userName = string.Join(",", managerList);
                        }
                        else if (Session["LoginUser"].ToString() == "Service")
                        {
                            List<string> managerList = db.wyzusers.Where(x => x.role == "creManager" && x.insuranceRole == false).Select(x => x.userName).ToList();
                            userName = string.Join(",", managerList);
                        }
                    }
                    else
                    {
                        userName = Session["UserName"].ToString();
                    }
                    if (jsonData != "")
                    {
                        filter = JsonConvert.DeserializeObject<RecordingFilters>(jsonData);
                    }
                    else
                    {
                        filter = new RecordingFilters();
                    }
                    string fromDateNew, toDateNew, callType, campaignName, creInitiated, disposition, usernameslist, reasonslist = "";
                    int callstatus = 3;

                    fromDateNew = filter.Call_Date_from == null ? "" : Convert.ToDateTime(filter.Call_Date_from.ToString()).ToString("yyyy-MM-dd");
                    toDateNew = filter.Call_Date_to == null ? "" : Convert.ToDateTime(filter.Call_Date_to.ToString()).ToString("yyyy-MM-dd");
                    callType = filter.calltype == null ? "" : filter.calltype;
                    campaignName = filter.campaign == null ? "" : filter.campaign;
                    creInitiated = filter.CRMCallType == null ? "" : filter.CRMCallType;
                    disposition = filter.disposition == null ? "" : filter.disposition;
                    //searchPattern = filter.search == null ? "" : filter.search;
                    usernameslist = filter.cresName == null ? "" : filter.cresName;
                    reasonslist = filter.SecondReason == null ? "" : filter.SecondReason;
                    callstatus = string.IsNullOrEmpty(filter.callStatus) ? 3 : int.Parse(filter.callStatus);
                    

                    if (filter.FilterFor == 1)
                    {
                        moduletype = 1;
                        if (filter.FilterFor == 1)
                        {
                           
                    
                           


                            totalCount = getAllRecordsCount(userName, "", "", "", "", "", "", "", "", "", moduletype, callstatus);

                            fromIndex = start;
                            toIndex = length;
                            
                            if(totalCount<toIndex)
                            {
                                toIndex = totalCount;
                            }
                            
                            CallDt = getAllCallRecording(userName, fromDateNew, toDateNew, callType, campaignName, creInitiated, disposition, searchPattern, usernameslist, moduletype, fromIndex, toIndex, reasonslist, callstatus);
                            
                            if (filter.isFiltered == true)
                            {
                                filteredCount = getAllRecordsCount(userName, fromDateNew, toDateNew, callType, campaignName, creInitiated, disposition, usernameslist, searchPattern, reasonslist, moduletype, callstatus);
                            }
                            else
                            {
                                filteredCount = totalCount;
                            }
                        }
                    }
                    else if (filter.FilterFor == 2)//Insurance
                    {
                        moduletype = 3;
                        if (filter.FilterFor == 2)
                        {
                            
                            totalCount = getAllRecordsCount(userName, "", "", "", "", "", "", "", "", "", moduletype, callstatus);

                            fromIndex = start;
                            toIndex = length;

                            if (totalCount < toIndex)
                            {
                                toIndex = totalCount;
                            }

                            //fromDateNew = filter.Call_Date_from == null ? "" : Convert.ToDateTime(filter.Call_Date_from.ToString()).ToString("yyyy-MM-dd");
                            //toDateNew = filter.Call_Date_to == null ? "" : Convert.ToDateTime(filter.Call_Date_to.ToString()).ToString("yyyy-MM-dd");
                            //callType = filter.calltype == null ? "" : filter.calltype;
                            //campaignName = filter.campaign == null ? "" : filter.campaign;
                            //creInitiated = filter.CRMCallType == null ? "" : filter.CRMCallType;
                            //disposition = filter.disposition == null ? "" : filter.disposition;
                            ////searchPattern = filter.search == null ? "" : filter.search;
                            //usernameslist = filter.cresName == null ? "" : filter.cresName;

                            CallDt = getAllCallRecording(userName, fromDateNew, toDateNew, callType, campaignName, creInitiated, disposition, searchPattern, usernameslist, moduletype, fromIndex, toIndex, reasonslist, callstatus);
                            if (filter.isFiltered == true)
                            {
                                filteredCount = getAllRecordsCount(userName, fromDateNew, toDateNew, callType, campaignName, creInitiated, disposition, usernameslist, searchPattern, reasonslist, moduletype, callstatus);
                            }
                            else
                            {
                                filteredCount = totalCount;
                            }
                        }
                    }
                    else if (filter.FilterFor == 3)//PSF
                    {
                        moduletype = 2;
                        if (filter.FilterFor == 3)
                        {
                            totalCount = getAllRecordsCount(userName, "", "", "", "", "", "", "", "", "", moduletype, callstatus);

                            fromIndex = start;
                            toIndex = length;
                            if (totalCount < toIndex)
                            {
                                toIndex = totalCount;
                            }

                            //fromDateNew = filter.Call_Date_from == null ? "" : Convert.ToDateTime(filter.Call_Date_from.ToString()).ToString("yyyy-MM-dd");
                            //toDateNew = filter.Call_Date_to == null ? "" : Convert.ToDateTime(filter.Call_Date_to.ToString()).ToString("yyyy-MM-dd");
                            //callType = filter.calltype == null ? "" : filter.calltype;
                            //campaignName = filter.campaign == null ? "" : filter.campaign;
                            //creInitiated = filter.CRMCallType == null ? "" : filter.CRMCallType;
                            //disposition = filter.disposition == null ? "" : filter.disposition;
                            ////searchPattern = filter.search == null ? "" : filter.search;
                            //usernameslist = filter.cresName == null ? "" : filter.cresName;

                            CallDt = getAllCallRecording(userName, fromDateNew, toDateNew, callType, campaignName, creInitiated, disposition, searchPattern, usernameslist, moduletype, fromIndex, toIndex, reasonslist, callstatus);
                            if (filter.isFiltered == true)
                            {
                                filteredCount = getAllRecordsCount(userName, fromDateNew, toDateNew, callType, campaignName, creInitiated, disposition, usernameslist, searchPattern, reasonslist, moduletype, callstatus);
                            }
                            else
                            {
                                filteredCount = totalCount;
                            }
                        }
                    }
                    else if (filter.FilterFor == 4)//Other
                    {
                        moduletype = 2;
                        if (filter.FilterFor == 4)
                        {
                            //long fromIndex=0,toIndex = 20;

                            totalCount = getRecordingsCount(userName, "", "", "", "", "");
                            fromIndex = start;
                            toIndex = length;
                            if (totalCount < toIndex)
                            {
                                toIndex = totalCount;
                            }

                            fromDateNew = filter.Call_Date_from == null ? "" : Convert.ToDateTime(filter.Call_Date_from.ToString()).ToString("yyyy-MM-dd");
                            toDateNew = filter.Call_Date_to == null ? "" : Convert.ToDateTime(filter.Call_Date_to.ToString()).ToString("yyyy-MM-dd");
                            callType = filter.calltype == null ? "" : filter.calltype;
                            campaignName = filter.campaign == null ? "" : filter.campaign;
                            creInitiated = filter.CRMCallType == null ? "" : filter.CRMCallType;
                            disposition = filter.disposition == null ? "" : filter.disposition;
                            //searchPattern = filter.search == null ? "" : filter.search;
                            usernameslist = filter.cresName == null ? "" : filter.cresName;

                            CallDt = getOtherCallRecords(userName, fromDateNew, toDateNew, usernameslist, callType, searchPattern, fromIndex, toIndex);
                            if (filter.isFiltered == true)
                            {
                                filteredCount = getRecordingsCount(userName, "", "", "", "", "");
                            }
                            else
                            {
                                filteredCount = totalCount;
                            }
                        }
                    }

                    else if (filter.FilterFor == 5)    //post Sales
                    {
                        moduletype = 5;
                        if (filter.FilterFor == 5)
                        {
                            totalCount = getAllRecordsCount(userName, "", "", "", "", "", "", "", "", "", moduletype, callstatus);

                            fromIndex = start;
                            toIndex = length;

                            if (totalCount < toIndex)
                            {
                                toIndex = totalCount;
                            }

                            CallDt = getAllCallRecording(userName, fromDateNew, toDateNew, callType, campaignName, creInitiated, disposition, searchPattern, usernameslist, moduletype, fromIndex, toIndex, reasonslist, callstatus);

                            if (filter.isFiltered == true)
                            {
                                filteredCount = getAllRecordsCount(userName, fromDateNew, toDateNew, callType, campaignName, creInitiated, disposition, usernameslist, searchPattern, reasonslist, moduletype, callstatus);
                            }
                            else
                            {
                                filteredCount = totalCount;
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
            }

            var jsonReturnData = Json(new { data = CallDt, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = filteredCount, exception= exception }, JsonRequestBehavior.AllowGet);
            jsonReturnData.MaxJsonLength = Int32.MaxValue;
            return jsonReturnData;
        }

        public List<CallRecordingDataTable> getAllCallRecording(string userLoginName, string fromDateNew,
            string toDateNew, string callType, string campaignName, string creInitiated, string disposition,
            string searchPattern, string usernameslist, int modelType, long fromIndex, long toIndex,
            string reasonslist, int callstatus)
        {
            List<CallRecordingDataTable> Calldt = new List<CallRecordingDataTable>();
            //try
            //{
                using (var db = new AutoSherDBContext())
                {
                    if(Session["UserRole"].ToString() == "WM" /*&& Session["OEM"].ToString().ToLower() == "hyundai"*/)
                {
                        var userNameWM = Session["UserName"].ToString();
                        var workId = db.wyzusers.FirstOrDefault(x => x.userName == userNameWM).workshop_id;
                        int workshopId = Convert.ToInt32(workId);
                        string str = @"CALL callRecordingHistoryforwm(@managerUserId,@instartDate, @inEndDate,@incallType, @inCampaign,@calledStatus,@dipsoType,@users,@pattern,@modeltype,@reasonslist,@startfrom,@length,@Incallstatus,@inworkshop_id)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("managerUserId", userLoginName),
                        new MySqlParameter("instartDate", fromDateNew),
                        new MySqlParameter("inEndDate", toDateNew),
                        new MySqlParameter("incallType", callType),
                        new MySqlParameter("inCampaign", campaignName),
                        new MySqlParameter("calledStatus", creInitiated),
                        new MySqlParameter("dipsoType", disposition),
                        new MySqlParameter("users", usernameslist),
                        new MySqlParameter("pattern", searchPattern),
                        new MySqlParameter("modeltype",modelType.ToString()),
                        new MySqlParameter("startfrom", fromIndex),
                        new MySqlParameter("length", toIndex),
                        new MySqlParameter("reasonslist", reasonslist),
                        new MySqlParameter("Incallstatus", callstatus),
                        new MySqlParameter("inworkshop_id", workshopId)
                    };
                    Calldt = db.Database.SqlQuery<CallRecordingDataTable>(str, param).ToList();
                }
                else
                {
                    string str = @"CALL callRecordingHistory(@managerUserId,@instartDate, @inEndDate,@incallType, @inCampaign,@calledStatus,@dipsoType,@users,@pattern,@modeltype,@reasonslist,@startfrom,@length,@Incallstatus)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("managerUserId", userLoginName),
                        new MySqlParameter("instartDate", fromDateNew),
                        new MySqlParameter("inEndDate", toDateNew),
                        new MySqlParameter("incallType", callType),
                        new MySqlParameter("inCampaign", campaignName),
                        new MySqlParameter("calledStatus", creInitiated),
                        new MySqlParameter("dipsoType", disposition),
                        new MySqlParameter("users", usernameslist),
                        new MySqlParameter("pattern", searchPattern),
                        new MySqlParameter("modeltype",modelType.ToString()),
                        new MySqlParameter("startfrom", fromIndex),
                        new MySqlParameter("length", toIndex),
                        new MySqlParameter("reasonslist", reasonslist),
                        new MySqlParameter("Incallstatus", callstatus)
                    };
                    Calldt = db.Database.SqlQuery<CallRecordingDataTable>(str, param).ToList();
                }

                
                    //recordFiltered = Calldt.Count();
                }
            //}
            //catch (Exception ex)
            //{

            //}

            return Calldt;
        }

        public List<CallRecordingDataTable> getOtherCallRecords(string userLoginName, string fromDateNew,
            string toDateNew, string usernameslist, string callType, string searchPattern, long fromIndex,
            long toIndex)
        {
            List<CallRecordingDataTable> callDt = new List<CallRecordingDataTable>();
            //try
            //{
                using (var db = new AutoSherDBContext())
                {
                    string str = @"CALL otherCallRecordingHistory(@managerUserId,@instartDate, @inEndDate, @users,@in_calltype,@pattern,@startfrom,@length)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("managerUserId", userLoginName),
                        new MySqlParameter("instartDate", fromDateNew),
                        new MySqlParameter("inEndDate", toDateNew),
                        new MySqlParameter("in_calltype", callType),
                        new MySqlParameter("users", usernameslist),
                        new MySqlParameter("pattern", searchPattern),
                        new MySqlParameter("startfrom", fromIndex),
                        new MySqlParameter("length", toIndex),
                    };

                    callDt = db.Database.SqlQuery<CallRecordingDataTable>(str, param).ToList();

                    //recordFiltered = callDt.Count();
                }
            //}
            //catch (Exception ex)
            //{

            //}

            return callDt;
        }


        public int getAllRecordsCount(string userLoginName, string startDate, string endDate, string calltype,
            string campainName, string calledStatus, string dispoType, string users, string pattern, string reasonslist,
            int modelType,int callstatus)
        {
            int count = 0;
            //try
            //{
                using (var db = new AutoSherDBContext())
                {
                if (Session["UserRole"].ToString() == "WM")
                {
                    var userNameWM = Session["UserName"].ToString();
                    var workId = db.wyzusers.FirstOrDefault(x => x.userName == userNameWM).workshop_id;
                    int workshopId = Convert.ToInt32(workId);
                    string str = @"CALL callRecordingHistorycountforwm(@managerUserId,@instartDate, @inEndDate,@incallType,@inCampaign,@calledStatus,@dipsoType,@users,@pattern,@modeltype,@reasonslist,@Incallstatus,@inworkshop_id)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("managerUserId", userLoginName),
                        new MySqlParameter("instartDate", startDate),
                        new MySqlParameter("inEndDate", endDate),
                        new MySqlParameter("incallType", calltype),
                        new MySqlParameter("inCampaign", campainName),
                        new MySqlParameter("calledStatus", calledStatus),
                        new MySqlParameter("dipsoType", dispoType),
                        new MySqlParameter("users", users),
                        new MySqlParameter("pattern", pattern),
                        new MySqlParameter("modeltype", modelType.ToString()),
                        new MySqlParameter("reasonslist", reasonslist),
                        new MySqlParameter("Incallstatus", callstatus),
                        new MySqlParameter("inworkshop_id", workshopId)

                    };

                    count = db.Database.SqlQuery<int>(str, param).FirstOrDefault();
                }
                else
                {
                    string str = @"CALL callRecordingHistorycount(@managerUserId,@instartDate, @inEndDate,@incallType,@inCampaign,@calledStatus,@dipsoType,@users,@pattern,@modeltype,@reasonslist,@Incallstatus)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("managerUserId", userLoginName),
                        new MySqlParameter("instartDate", startDate),
                        new MySqlParameter("inEndDate", endDate),
                        new MySqlParameter("incallType", calltype),
                        new MySqlParameter("inCampaign", campainName),
                        new MySqlParameter("calledStatus", calledStatus),
                        new MySqlParameter("dipsoType", dispoType),
                        new MySqlParameter("users", users),
                        new MySqlParameter("pattern", pattern),
                        new MySqlParameter("modeltype", modelType.ToString()),
                        new MySqlParameter("reasonslist", reasonslist),
                        new MySqlParameter("Incallstatus", callstatus),

                    };

                    count = db.Database.SqlQuery<int>(str, param).FirstOrDefault();
                }
                    

                    ////recordFiltered = callDt.Count();
                }
            //}
            //catch (Exception ex)
            //{

            //}

            return count;
        }


        public int getRecordingsCount(string userLoginName, string fromDateNew,
            string toDateNew, string usernameslist, string callType, string searchPattern)
        {
            int totalRecordsCount = 0;
            //try
            //{
                using (var db = new AutoSherDBContext())
                {
                    string str = @"CALL otherCallRecordingHistoryCount(@managerUserId,@instartDate, @inEndDate, @users,@in_calltype,@pattern)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("managerUserId", userLoginName),
                        new MySqlParameter("instartDate", fromDateNew),
                        new MySqlParameter("inEndDate", toDateNew),
                        new MySqlParameter("in_calltype", callType),
                        new MySqlParameter("users", usernameslist),
                        new MySqlParameter("pattern", searchPattern),
                    };

                    totalRecordsCount = db.Database.SqlQuery<int>(str, param).FirstOrDefault();
                }
            //}
            //catch (Exception ex)
            //{

            //}

            return totalRecordsCount;
        }
    }
}