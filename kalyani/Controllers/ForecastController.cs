using AutoSherpa_project.Models;
using AutoSherpa_project.Models.API_Model;
using AutoSherpa_project.Models.ViewModels;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using NLog;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoSherpa_project.Controllers
{
    [AuthorizeFilter]
    public class ForecastController : Controller
    {
        // GET: Forecast

        [HttpGet]
        [ActionName("Forecast")]
        public ActionResult ForecastGet()
        {
            ForecastVM forecastVM = new ForecastVM();
            using (AutoSherDBContext dbContext = new AutoSherDBContext())
            {
                try
                {
                    //For Dashboards
                    ViewBag.TotalVINCount = dbContext.smrforecasteddatas.Count();
                    var latestDate = dbContext.services.Max(n => n.jobCardDate);
                    ViewBag.LastROUploaded = Convert.ToDateTime(latestDate).ToShortDateString();
                    ViewBag.LastSalesUploaded = Convert.ToDateTime(dbContext.vehicles.Max(m => m.saleDate)).ToShortDateString();
                    ViewBag.LastSalesUploaded = dbContext.Database.SqlQuery<DateTime>("select date(processingEndedDT) from upload  where uploadType_id in (select id from uploadtype where uploadTypeName regexp 'sales' order by id desc) and processingStatus = 'COMPLETE' order by id desc limit 0,1;").FirstOrDefault();

                    ViewBag.CurrentDate = Convert.ToDateTime(DateTime.Now).ToShortDateString();

                    //For DropDowns
                    forecastVM.locations = dbContext.locations.OrderBy(m=>m.name).ToList();
                    forecastVM.forecastlogicservicetypes = dbContext.forecastlogicservicetypes.Where(x => x.inactive == false).ToList();

                    //for modal pop up
                    ViewBag.ddlWorkshop = dbContext.workshops.Where(x => x.issales == false && x.isinsurance == false).Select(model => new { workshopId = model.id, workshopName = model.workshopName }).OrderBy(m => m.workshopName).ToList();
                    forecastVM.campaigns = dbContext.campaigns.Where(x => x.isactive == true && x.campaignType == "Campaign").ToList();
                    ViewBag.CampDDL = forecastVM.campaigns.Select(m => new { id = m.id });
                    forecastVM.wyzusers = dbContext.wyzusers.Where(x => x.role == "CRE" && x.unAvailable==false).ToList();
                    ViewBag.ModelCategories = dbContext.Modelcategories.Select(x => new { id = x.modelcatid, name = x.modelcat }).ToList();

                    ViewBag.ExcludeNegativeDiposition = dbContext.ExcludeNegativeDiposition.Select(x => new { id = x.ExcludeNegativeDipositionID, name = x.ExcludeNegativeDipositionName }).ToList();
                }
                catch (Exception ex)
                {
                    ViewBag.exception = ex.ToString().Substring(0, 20);
                }

            }
            return View(forecastVM);
        }

        //getting table data
        public ActionResult ForecastPost(string values)
        {
            List<CallLogDispositionLoadMRForcast> CallMR = new List<CallLogDispositionLoadMRForcast>();
            int recordsTotalCount = 0;
            JsonResult jsonOutputData = new JsonResult();
            try
            {
                long incremamangeridvar = long.Parse(Session["UserId"].ToString());
                long fromIndex = 0, toIndex = 0;
                int start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchPattern = Request["search[value]"];

                using (AutoSherDBContext dbContext = new AutoSherDBContext())
                {
                    try
                    {
                        if (values != "")
                        {
                            int totalCount = dbContext.smrforecasteddatas.Count();

                            var getData = JsonConvert.DeserializeObject<forecastFilter>(values);
                            getCallMR(out CallMR, incremamangeridvar, getData,out recordsTotalCount);

                            jsonOutputData = Json(new { data = CallMR.Take(100), draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = recordsTotalCount  }, JsonRequestBehavior.AllowGet);
                            jsonOutputData.MaxJsonLength = Int32.MaxValue;
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.exception = ex.ToString().Substring(0, 20);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.exception = ex.ToString().Substring(0, 20);
            }
            return jsonOutputData;
        }

        public ActionResult getDataWorkshopWiseCounts(string values)
        {
            List<DataWorkshopWiseCounts> CallMR = new List<DataWorkshopWiseCounts>();
            int recordsTotalCount = 0;
            JsonResult jsonOutputData = new JsonResult();
            try
            {
                long incremamangeridvar = long.Parse(Session["UserId"].ToString());
                long fromIndex = 0, toIndex = 0;
                int start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchPattern = Request["search[value]"];

                using (AutoSherDBContext dbContext = new AutoSherDBContext())
                {
                    try
                    {
                        if (values != "")
                        {
                            int totalCount = dbContext.smrforecasteddatas.Count();

                            var getData = JsonConvert.DeserializeObject<forecastFilter>(values);
                            //getCallMR(out CallMR, incremamangeridvar, getData, out recordsTotalCount);
                            getCallWWC(out CallMR, incremamangeridvar, getData, out recordsTotalCount);
                            jsonOutputData = Json(new { data = CallMR.Take(100), draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = recordsTotalCount }, JsonRequestBehavior.AllowGet);
                            jsonOutputData.MaxJsonLength = Int32.MaxValue;
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.exception = ex.ToString().Substring(0, 20);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.exception = ex.ToString().Substring(0, 20);
            }
            return jsonOutputData;
        }

        [HttpPost]
        public JsonResult GetFilteredCREUsers(string workshopId, string campaignId, string serviceTypeId, string roAgeId, string categoryId)
        {

            var assignUserList = new List<AssignUserList>();

            using (AutoSherDBContext dbContext = new AutoSherDBContext())
            {
                string str = @"CALL SMR_ReturnUserListBasedOnfilter(@InWorkshop_id ,@inCampaignId ,@inServiceTypeId ,@inROAgeingId,@inCategoryId)";
                //@inserviceType,@inpsYear//,@inpsMonth ,@inROAge
                MySqlParameter[] param = new MySqlParameter[]
               {
                            new MySqlParameter("InWorkshop_id", workshopId ?? ""),
                            new MySqlParameter("inCampaignId", campaignId ?? ""),
                            new MySqlParameter("inROAgeingId", roAgeId ?? ""),
                            new MySqlParameter("inServiceTypeId", serviceTypeId ?? ""),
                            new MySqlParameter("inCategoryId", categoryId ?? "")
               };
                assignUserList = dbContext.Database.SqlQuery<AssignUserList>(str, param).ToList();

            }

            return Json(assignUserList?.GroupBy(g=>g.workshopName)?.Select(s=>new { worshopName = s.Key, creUsers = s.ToList()  }));

        }
        private void getCallWWC(out List<DataWorkshopWiseCounts> CallMR, long incremamangeridvar, forecastFilter getData, out int recordsTotalCount)
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            long fromIndex, toIndex;
            int totalCount = 0;
            string inlocvar, fromservicedatevar
                               , toservicedatevar, inserviceTypevar, inpsYearvar, fromsalevar, tosalevar
                               , inpsMonth, inROAge, inlostPeriod, inforecastLogic, isassigned
                               , fromlastServicevar, tolastServicevar, fromEWExpiryFrom, fromEWExpiryTo, workshopId, fromMCPExpiryFrom, fromMCPExpiryTo, modelCategories, excludeNegativeDeposition, excludeNegativeDepositionCallDateFrom, excludeNegativeDepositionCallDateTo;
            long start_withvar, lengthvar;
            inlocvar = getData.locationObj == null ? "0" : getData.locationObj;
            fromservicedatevar = getData.FromDueYearObj == null ? "0" : Convert.ToDateTime(getData.FromDueYearObj.ToString()).ToString("yyyy-MM-dd"); //from nxt ser date
            toservicedatevar = getData.ToDueYearObj == null ? "0" : Convert.ToDateTime(getData.ToDueYearObj.ToString()).ToString("yyyy-MM-dd"); //from nxt ser date
            inserviceTypevar = getData.selectedDueTypeObj == null ? "0" : getData.selectedDueTypeObj;//ser type id
            inpsYearvar = "0";
            fromsalevar = getData.FromSaleDateYearObj == null ? "0" : Convert.ToDateTime(getData.FromSaleDateYearObj.ToString()).ToString("yyyy-MM-dd"); //from sale date
            tosalevar = getData.ToSaleDateYearObj == null ? "0" : Convert.ToDateTime(getData.ToSaleDateYearObj.ToString()).ToString("yyyy-MM-dd");
            inpsMonth = "0";
            inROAge = getData.roObj == null ? "0" : getData.roObj;
            inlostPeriod = getData.noShowObj == null ? "0" : getData.noShowObj;
            inforecastLogic = getData.selectedForecastLogicObj == null ? "0" : getData.selectedForecastLogicObj;
            isassigned = getData.selectedIsAssignedObj == null ? "0" : getData.selectedIsAssignedObj;
            fromlastServicevar = getData.fromLastSerDateObj == null ? "0" : Convert.ToDateTime(getData.fromLastSerDateObj.ToString()).ToString("yyyy-MM-dd"); //from last ser date
            tolastServicevar = getData.toLastSerDateObj == null ? "0" : Convert.ToDateTime(getData.toLastSerDateObj.ToString()).ToString("yyyy-MM-dd"); //from last ser date
            fromEWExpiryFrom = getData.FromEWExpiryFrom == null ? "0" : Convert.ToDateTime(getData.FromEWExpiryFrom.ToString()).ToString("yyyy-MM-dd"); //from nxt ser date
            fromEWExpiryTo = getData.FromEWExpiryTo == null ? "0" : Convert.ToDateTime(getData.FromEWExpiryTo.ToString()).ToString("yyyy-MM-dd"); //from nxt ser date
            fromMCPExpiryFrom = getData.FromMCPExpiryFrom == null ? "0" : Convert.ToDateTime(getData.FromMCPExpiryFrom.ToString()).ToString("yyyy-MM-dd"); //from nxt ser date
            fromMCPExpiryTo = getData.FromMCPExpiryTo == null ? "0" : Convert.ToDateTime(getData.FromMCPExpiryTo.ToString()).ToString("yyyy-MM-dd"); //from nxt ser date
            modelCategories = getData.ModelCategories == null ? "0" : getData.ModelCategories;
            excludeNegativeDeposition = getData.ExcludeNegativeDeposition == null ? "0" : getData.ExcludeNegativeDeposition;
            excludeNegativeDepositionCallDateFrom = getData.ExcludeNegativeDepositionCallDateFrom == null ? "0" : Convert.ToDateTime(getData.ExcludeNegativeDepositionCallDateFrom.ToString()).ToString("yyyy-MM-dd");
            excludeNegativeDepositionCallDateTo = getData.ExcludeNegativeDepositionCallDateTo == null ? "0" : Convert.ToDateTime(getData.ExcludeNegativeDepositionCallDateTo.ToString()).ToString("yyyy-MM-dd"); ///from nxt ser date//start_withvar = 0;
            workshopId = getData.workshopId == null ? "0" : getData.workshopId;

            using (AutoSherDBContext dBContext = new AutoSherDBContext())
            {
                totalCount = dBContext.smrforecasteddatas.Count();
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



            // below logic was not necessary - too many calls to db

            //recordsTotalCount = getAllForecastData(incremamangeridvar, inlocvar, fromservicedatevar
            //       , toservicedatevar, inserviceTypevar, inpsYearvar, fromsalevar, tosalevar
            //       , inpsMonth, inROAge, inlostPeriod, inforecastLogic, isassigned
            //       , fromlastServicevar, tolastServicevar, 0, totalCount,workshopId,fromEWExpiryFrom,fromEWExpiryTo,fromMCPExpiryFrom,fromMCPExpiryTo,modelCategories,excludeNegativeDeposition,excludeNegativeDepositionCallDate).Count;

            //if (getData.isFiltered == true)
            //{
            CallMR = getAllWWC(incremamangeridvar, inlocvar, fromservicedatevar
               , toservicedatevar, inserviceTypevar, inpsYearvar, fromsalevar, tosalevar
               , inpsMonth, inROAge, inlostPeriod, inforecastLogic, isassigned
               , fromlastServicevar, tolastServicevar, fromIndex, totalCount, workshopId, fromEWExpiryFrom, fromEWExpiryTo, fromMCPExpiryFrom, fromMCPExpiryTo, modelCategories, excludeNegativeDeposition, excludeNegativeDepositionCallDateFrom, excludeNegativeDepositionCallDateTo);
            recordsTotalCount = CallMR.Count();


        }
        public List<DataWorkshopWiseCounts> getAllWWC(long incremamangeridvar, string inlocvar, string fromservicedatevar,
        string toservicedatevar, string inserviceTypevar, string inpsYearvar, string fromsalevar, string tosalevar, string inpsMonthvar,
        string inROAgevar, string inlostPeriodvar, string inforecastLogicvar, string isassignedvar, string fromlastServicevar,
        string tolastServicevar, long start_withvar, long lengthvar, string workshopId, string fromEWExpiryFrom = "", string fromEWExpiryTo = "", string fromMCPExpiryFrom = "", string fromMCPExpiryTo = "", string modelCategories = "", string excludeNegativeDeposition = "", string excludeNegativeDepositionCallDateFrom = "", string excludeNegativeDepositionCallDateTo = "")
        {

            List<DataWorkshopWiseCounts> CallMr = new List<DataWorkshopWiseCounts>();
            try
            {
                using (AutoSherDBContext dbContext = new AutoSherDBContext())
                {
                    string str = @"CALL smrforecastdataWorkshopWiseCounts(@incremamangerid ,@inloc ,@fromservicedate 
                                    ,@toservicedate ,@inserviceType,@inpsYear ,@fromsale ,@tosale 
                                    ,@inpsMonth ,@inROAge,@inlostPeriod ,@inforecastLogic,@isassigned
                                    ,@fromlastService,@tolastService, @start_with, @length,@workshopidd,@ewexpiryfrom,@ewexpiryto,@mcpfrom,@mcpto,@inmodelcat,@negativedispose,@negativecalldatefrom,@negativecalldateto)";
                    //@inserviceType,@inpsYear//,@inpsMonth ,@inROAge
                    MySqlParameter[] param = new MySqlParameter[]
                   {
                            new MySqlParameter("incremamangerid", incremamangeridvar),
                            new MySqlParameter("inloc", inlocvar),
                            new MySqlParameter("fromservicedate", fromservicedatevar),
                            new MySqlParameter("toservicedate", toservicedatevar),
                            new MySqlParameter("inserviceType", inserviceTypevar),
                            new MySqlParameter("inpsYear", inpsYearvar),
                            new MySqlParameter("fromsale", fromsalevar),
                            new MySqlParameter("tosale", tosalevar),
                            new MySqlParameter("inpsMonth", inpsMonthvar),
                            new MySqlParameter("inROAge",inROAgevar),
                            new MySqlParameter("inlostPeriod", inlostPeriodvar),
                            new MySqlParameter("inforecastLogic", inforecastLogicvar),
                            new MySqlParameter("isassigned", isassignedvar),
                            new MySqlParameter("fromlastService",fromlastServicevar),
                            new MySqlParameter("tolastService", tolastServicevar),
                            new MySqlParameter("start_with", start_withvar),
                            new MySqlParameter("length", lengthvar),
                            new MySqlParameter("workshopidd", workshopId),
                            new MySqlParameter("ewexpiryfrom", fromEWExpiryFrom),
                            new MySqlParameter("ewexpiryto", fromEWExpiryTo),
                            new MySqlParameter("mcpfrom", fromMCPExpiryFrom),
                            new MySqlParameter("mcpto", fromMCPExpiryTo),
                            new MySqlParameter("inmodelcat", modelCategories),
                            new MySqlParameter("negativedispose", excludeNegativeDeposition),
                            new MySqlParameter("negativecalldatefrom", excludeNegativeDepositionCallDateFrom),
                            new MySqlParameter("negativecalldateto", excludeNegativeDepositionCallDateTo)

                   };
                    CallMr = dbContext.Database.SqlQuery<DataWorkshopWiseCounts>(str, param).ToList();
                    //string jsonString = JsonConvert.SerializeObject(result);

                }
            }
            catch (Exception ex)
            {
                ViewBag.exception = ex.ToString().Substring(0, 20);
            }
            return CallMr;
        }

        private void getCallMR(out List<CallLogDispositionLoadMRForcast> CallMR, long incremamangeridvar, forecastFilter getData,out int recordsTotalCount)
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            long fromIndex, toIndex;
            int totalCount = 0;
            string inlocvar, fromservicedatevar
                               , toservicedatevar, inserviceTypevar, inpsYearvar, fromsalevar, tosalevar
                               , inpsMonth, inROAge, inlostPeriod, inforecastLogic, isassigned
                               , fromlastServicevar, tolastServicevar, fromEWExpiryFrom, fromEWExpiryTo,workshopId, fromMCPExpiryFrom, fromMCPExpiryTo, modelCategories, excludeNegativeDeposition, excludeNegativeDepositionCallDateFrom, excludeNegativeDepositionCallDateTo;
            long start_withvar, lengthvar;
            inlocvar = getData.locationObj == null ? "0" : getData.locationObj;
            fromservicedatevar = getData.FromDueYearObj == null ? "0" : Convert.ToDateTime(getData.FromDueYearObj.ToString()).ToString("yyyy-MM-dd"); //from nxt ser date
            toservicedatevar = getData.ToDueYearObj == null ? "0" : Convert.ToDateTime(getData.ToDueYearObj.ToString()).ToString("yyyy-MM-dd"); //from nxt ser date
            inserviceTypevar = getData.selectedDueTypeObj == null ? "0" : getData.selectedDueTypeObj;//ser type id
            inpsYearvar = "0";
            fromsalevar = getData.FromSaleDateYearObj == null ? "0" : Convert.ToDateTime(getData.FromSaleDateYearObj.ToString()).ToString("yyyy-MM-dd"); //from sale date
            tosalevar = getData.ToSaleDateYearObj == null ? "0" : Convert.ToDateTime(getData.ToSaleDateYearObj.ToString()).ToString("yyyy-MM-dd");
            inpsMonth = "0";
            inROAge = getData.roObj == null ? "0" : getData.roObj;
            inlostPeriod = getData.noShowObj == null ? "0" : getData.noShowObj;
            inforecastLogic = getData.selectedForecastLogicObj == null ? "0" : getData.selectedForecastLogicObj;
            isassigned = getData.selectedIsAssignedObj == null ? "0" : getData.selectedIsAssignedObj;
            fromlastServicevar = getData.fromLastSerDateObj == null ? "0" : Convert.ToDateTime(getData.fromLastSerDateObj.ToString()).ToString("yyyy-MM-dd"); //from last ser date
            tolastServicevar = getData.toLastSerDateObj == null ? "0" : Convert.ToDateTime(getData.toLastSerDateObj.ToString()).ToString("yyyy-MM-dd"); //from last ser date
            fromEWExpiryFrom = getData.FromEWExpiryFrom == null ? "0" : Convert.ToDateTime(getData.FromEWExpiryFrom.ToString()).ToString("yyyy-MM-dd"); //from nxt ser date
            fromEWExpiryTo = getData.FromEWExpiryTo == null ? "0" : Convert.ToDateTime(getData.FromEWExpiryTo.ToString()).ToString("yyyy-MM-dd"); //from nxt ser date
            fromMCPExpiryFrom = getData.FromMCPExpiryFrom == null ? "0" : Convert.ToDateTime(getData.FromMCPExpiryFrom.ToString()).ToString("yyyy-MM-dd"); //from nxt ser date
            fromMCPExpiryTo = getData.FromMCPExpiryTo == null ? "0" : Convert.ToDateTime(getData.FromMCPExpiryTo.ToString()).ToString("yyyy-MM-dd"); //from nxt ser date
            modelCategories = getData.ModelCategories == null ? "0" : getData.ModelCategories;
            excludeNegativeDeposition = getData.ExcludeNegativeDeposition == null ? "0" : getData.ExcludeNegativeDeposition;
            excludeNegativeDepositionCallDateFrom = getData.ExcludeNegativeDepositionCallDateFrom == null ? "0" : Convert.ToDateTime(getData.ExcludeNegativeDepositionCallDateFrom.ToString()).ToString("yyyy-MM-dd");
            excludeNegativeDepositionCallDateTo = getData.ExcludeNegativeDepositionCallDateTo == null ? "0" : Convert.ToDateTime(getData.ExcludeNegativeDepositionCallDateTo.ToString()).ToString("yyyy-MM-dd"); ///from nxt ser date//start_withvar = 0;
            workshopId = getData.workshopId == null ? "0" : getData.workshopId;

            using (AutoSherDBContext dBContext = new AutoSherDBContext())
            {
                totalCount = dBContext.smrforecasteddatas.Count();
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



            // below logic was not necessary - too many calls to db

            //recordsTotalCount = getAllForecastData(incremamangeridvar, inlocvar, fromservicedatevar
            //       , toservicedatevar, inserviceTypevar, inpsYearvar, fromsalevar, tosalevar
            //       , inpsMonth, inROAge, inlostPeriod, inforecastLogic, isassigned
            //       , fromlastServicevar, tolastServicevar, 0, totalCount,workshopId,fromEWExpiryFrom,fromEWExpiryTo,fromMCPExpiryFrom,fromMCPExpiryTo,modelCategories,excludeNegativeDeposition,excludeNegativeDepositionCallDate).Count;

            //if (getData.isFiltered == true)
            //{
            CallMR = getAllForecastData(incremamangeridvar, inlocvar, fromservicedatevar
               , toservicedatevar, inserviceTypevar, inpsYearvar, fromsalevar, tosalevar
               , inpsMonth, inROAge, inlostPeriod, inforecastLogic, isassigned
               , fromlastServicevar, tolastServicevar, fromIndex, totalCount, workshopId, fromEWExpiryFrom, fromEWExpiryTo, fromMCPExpiryFrom, fromMCPExpiryTo, modelCategories, excludeNegativeDeposition, excludeNegativeDepositionCallDateFrom, excludeNegativeDepositionCallDateTo);
            recordsTotalCount = CallMR.Count();


        }

        public List<CallLogDispositionLoadMRForcast> getAllForecastData(long incremamangeridvar, string inlocvar, string fromservicedatevar,
            string toservicedatevar, string inserviceTypevar, string inpsYearvar, string fromsalevar, string tosalevar, string inpsMonthvar,
            string inROAgevar, string inlostPeriodvar, string inforecastLogicvar, string isassignedvar, string fromlastServicevar,
            string tolastServicevar, long start_withvar, long lengthvar,string workshopId,string fromEWExpiryFrom="",string fromEWExpiryTo ="",string fromMCPExpiryFrom = "",string fromMCPExpiryTo="",string modelCategories="",string excludeNegativeDeposition="",string excludeNegativeDepositionCallDateFrom = "", string excludeNegativeDepositionCallDateTo = "")
        {

            List<CallLogDispositionLoadMRForcast> CallMr = new List<CallLogDispositionLoadMRForcast>();
            try
            {
                using (AutoSherDBContext dbContext = new AutoSherDBContext())
                {
                    string str = @"CALL smrforecastdataview(@incremamangerid ,@inloc ,@fromservicedate 
                                    ,@toservicedate ,@inserviceType,@inpsYear ,@fromsale ,@tosale 
                                    ,@inpsMonth ,@inROAge,@inlostPeriod ,@inforecastLogic,@isassigned
                                    ,@fromlastService,@tolastService, @start_with, @length,@workshopidd,@ewexpiryfrom,@ewexpiryto,@mcpfrom,@mcpto,@inmodelcat,@negativedispose,@negativecalldatefrom,@negativecalldateto)";
                    //@inserviceType,@inpsYear//,@inpsMonth ,@inROAge
                    MySqlParameter[] param = new MySqlParameter[]
                   {
                            new MySqlParameter("incremamangerid", incremamangeridvar),
                            new MySqlParameter("inloc", inlocvar),
                            new MySqlParameter("fromservicedate", fromservicedatevar),
                            new MySqlParameter("toservicedate", toservicedatevar),
                            new MySqlParameter("inserviceType", inserviceTypevar),
                            new MySqlParameter("inpsYear", inpsYearvar),
                            new MySqlParameter("fromsale", fromsalevar),
                            new MySqlParameter("tosale", tosalevar),
                            new MySqlParameter("inpsMonth", inpsMonthvar),
                            new MySqlParameter("inROAge",inROAgevar),
                            new MySqlParameter("inlostPeriod", inlostPeriodvar),
                            new MySqlParameter("inforecastLogic", inforecastLogicvar),
                            new MySqlParameter("isassigned", isassignedvar),
                            new MySqlParameter("fromlastService",fromlastServicevar),
                            new MySqlParameter("tolastService", tolastServicevar),
                            new MySqlParameter("start_with", start_withvar),
                            new MySqlParameter("length", lengthvar),
                            new MySqlParameter("workshopidd", workshopId),
                            new MySqlParameter("ewexpiryfrom", fromEWExpiryFrom),
                            new MySqlParameter("ewexpiryto", fromEWExpiryTo),
                            new MySqlParameter("mcpfrom", fromMCPExpiryFrom),
                            new MySqlParameter("mcpto", fromMCPExpiryTo),
                            new MySqlParameter("inmodelcat", modelCategories),
                            new MySqlParameter("negativedispose", excludeNegativeDeposition),
                            new MySqlParameter("negativecalldatefrom", excludeNegativeDepositionCallDateFrom),
                            new MySqlParameter("negativecalldateto", excludeNegativeDepositionCallDateTo)

                   };
                    CallMr = dbContext.Database.SqlQuery<CallLogDispositionLoadMRForcast>(str, param).ToList();
                    //string jsonString = JsonConvert.SerializeObject(result);

                }
            }
            catch (Exception ex)
            {
                ViewBag.exception = ex.ToString().Substring(0, 20);
            }
            return CallMr;
        }

        //getting dashboard count
        //public ActionResult cardDetails(string values)
        //{
        //    int totalCount = 0;
        //    int assignedCount = 0;
        //    int balanceCount = 0;
        //    List<CallLogDispositionLoadMRForcast> CallMR = new List<CallLogDispositionLoadMRForcast>();
        //    var getData = JsonConvert.DeserializeObject<forecastFilter>(values);
        //    long incremamangeridvar = long.Parse(Session["UserId"].ToString());
        //    getCallMR(out CallMR, incremamangeridvar, getData);
        //    totalCount = CallMR.Count();
        //    if (getData.selectedIsAssignedObj == "1")//no
        //    {
        //        assignedCount = 0;
        //        balanceCount = totalCount;
        //    }
        //    else if (getData.selectedIsAssignedObj == "2")
        //    {
        //        assignedCount = totalCount;
        //        balanceCount = 0;
        //    }
        //    else if (getData.selectedIsAssignedObj == "3")
        //    {
        //        using (AutoSherDBContext dbContext = new AutoSherDBContext())
        //        {
        //            foreach (var item in CallMR)
        //            {
        //                long id = long.Parse(item.vehicle_id);
        //                var checkIfAssigned = dbContext.assignedinteractions.Where(x => x.vehical_Id == id).Select(x => x.id);
        //                assignedCount = checkIfAssigned.Count();
        //            }
        //        }
        //        balanceCount = totalCount - assignedCount;
        //    }

        //    return Json(new { totalCount = totalCount, assignedCount = assignedCount, balanceCount = balanceCount }, JsonRequestBehavior.AllowGet);
        //}

        //assigning data after modal submission
        public ActionResult submitModal(string values)
        {
            try
            {


                fmanualuser fmanualusers = new fmanualuser();
                campaign campaigns = new campaign();

                IDictionary<int, string> userAssingedinteractions = new Dictionary<int, string>();
                List<CallLogDispositionLoadMRForcast> CallMR = new List<CallLogDispositionLoadMRForcast>();
                //List<CallLogDispositionLoadMRForcast> recordsTotalCount = null;         
                int recordsTotalCount = 0;
                var getData = JsonConvert.DeserializeObject<forecastFilter>(values);
                long incremamangeridvar = long.Parse(Session["UserId"].ToString());
                getCallMR(out CallMR, incremamangeridvar, getData, out recordsTotalCount);

                string campaignData = getData.campaignControl;
                string campaignId = "";
                var listOfModalRes = new List<KeyValuePair<string, int>>();
                long uploadId = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                //var wrkShopUsrGrp = getData.selectedWrkshpUsrList.GroupBy(s => s.workshop).Select( g => new{ workshop = g.Key, userList = g.Select(s=> s.userId).Distinct() });

                //var wrkShopUsrGrp2 = getData.selectedWrkshpUsrList.Distinct().SelectMany(s => (s.serviceTypeId ?? "").Split(',').Select(c => new WorkshopUserMapper{ serviceTypeId = c,workshop =s.workshop, ROAgeingId = s.ROAgeingId,userId = s.userId })).SelectMany(s => (s.ROAgeingId ?? "").Split(',').Select(c => new WorkshopUserMapper { serviceTypeId = s.serviceTypeId, workshop = s.workshop, ROAgeingId = c, userId = s.userId }));

                //CallMR = (from a in CallMR
                //          join b in wrkShopUsrGrp on Convert.ToInt32(a.location_id) equals b.workshop
                //          select a).ToList();

                using (AutoSherDBContext dbContext = new AutoSherDBContext())
                {
                    if (campaignData.Contains(","))
                    {
                        if (campaignData.Split(',')[1] == "ddl")
                        {
                            campaignId = campaignData.Split(',')[0];
                        }
                        else if (campaignData.Split(',')[1] == "txt")
                        {
                            string campName = campaignData.Split(',')[0];
                            campaigns.campaignName = campName;
                            campaigns.campaignType = "Campaign";
                            campaigns.createdDate = DateTime.Now;
                            campaigns.isValid = true;
                            campaigns.isactive = true;
                            dbContext.campaigns.Add(campaigns);
                            dbContext.SaveChanges();
                            //campaignId = dBContext.campaigns.Single(x => x.campaignName.Contains(campName)).id.ToString();
                            campaignId = campaigns.id.ToString();
                        }
                    }

                    List<long?> vehicleIDs = dbContext.assignedinteractions.Select(x => x.vehical_Id).ToList();


                    for (int i = 0; i < CallMR.Count(); i++)
                    {
                        var row = CallMR[i];
                        row.CallLogDispositionLoadMRForcastID = i;

                        var matchingUserIds = String.Join(",", getData.selectedWrkshpUsrList.Where(u => (u.workshop == Convert.ToInt32(CallMR[i].location_id))
                         && ((u.serviceTypeId ?? "").Contains(row.ServiceTypeId ?? " "))
                         && ((u.categoryId ?? "").Contains(row.CategoryId ?? " "))
                         && ((u.ROAgeingId ?? "").Contains(row.RoId ?? " "))).Select(s => s.userId));
                        if (!String.IsNullOrEmpty(matchingUserIds))
                        {
                            userAssingedinteractions.Add(row.CallLogDispositionLoadMRForcastID, matchingUserIds);
                        }

                    }

                    var usrGroups = userAssingedinteractions.GroupBy(s => s.Value).Select(s => new { usrGrp = s.Key, interactionList = s.ToList() }).OrderByDescending(o => o.usrGrp.Length);

                    var assingedInteractionList = new List<assignedinteraction>();
                    foreach (var usrGrp in usrGroups)
                    {
                        var usrGrpList = usrGrp.usrGrp.Split(',');
                        if (usrGrpList.Any())
                        {
                            int usersCount = usrGrpList.Count();
                            int index = 0;

                            foreach (var item in usrGrp.interactionList)
                            {
                                assignedinteraction assignedinteractions = new assignedinteraction();
                                var assingedInteraction = CallMR[item.Key];
                                long? vehId = long.Parse(assingedInteraction.vehicle_id);
                                var serviceTypeID = dbContext.smrforecasteddatas.Single(x => x.vehicle_id == vehId).ServiceTypeId;
                                if (!vehicleIDs.Contains(vehId))
                                {
                                    assignedinteractions.callMade = "No";
                                    assignedinteractions.displayFlag = false;
                                    assignedinteractions.uplodedCurrentDate = DateTime.Now;
                                    assignedinteractions.campaign_id = long.Parse(campaignId);
                                    assignedinteractions.customer_id = long.Parse(assingedInteraction.customer_id);
                                    assignedinteractions.vehical_Id = long.Parse(assingedInteraction.vehicle_id);
                                    assignedinteractions.wyzUser_id = long.Parse(usrGrpList[index]);
                                    assignedinteractions.location_id = long.Parse(assingedInteraction.location_id);
                                    assignedinteractions.upload_id = uploadId;
                                    assignedinteractions.nextServiceType = serviceTypeID.ToString();
                                    assignedinteractions.nextServiceDate = assingedInteraction.nextServicedate;
                                    assignedinteractions.assigned_manager_id = incremamangeridvar;
                                    assignedinteractions.isautoassigned = false;
                                    assingedInteractionList.Add(assignedinteractions);

                                    index++;
                                }
                                if (index > (usersCount - 1)) index = 0;
                            }

                        }
                    }

                    dbContext.assignedinteractions.AddRange(assingedInteractionList);
                    dbContext.SaveChanges();

                    //foreach (var wrkShp in wrkShopUsrGrp)
                    //{
                    //    List<int> lclwyzuserId = new List<int>();
                    //    lclwyzuserId = wrkShp.userList.ToList();
                    //    int usersCount = lclwyzuserId.Count();
                    //    int index = 0;

                    //    foreach (var item in CallMR.Where(w => Convert.ToInt32(w.location_id) == wrkShp.workshop))
                    //    {
                    //        long? vehId = long.Parse(item.vehicle_id);
                    //        var serviceTypeID = dbContext.smrforecasteddatas.Single(x => x.vehicle_id == vehId).ServiceTypeId;
                    //        if (!vehicleIDs.Contains(vehId) && Convert.ToInt32(item.location_id) == wrkShp.workshop)
                    //        {
                    //            assignedinteractions.callMade = "No";
                    //            assignedinteractions.displayFlag = false;
                    //            assignedinteractions.uplodedCurrentDate = DateTime.Now;
                    //            assignedinteractions.campaign_id = long.Parse(campaignId);
                    //            assignedinteractions.customer_id = long.Parse(item.customer_id);
                    //            assignedinteractions.vehical_Id = long.Parse(item.vehicle_id);
                    //            assignedinteractions.wyzUser_id = lclwyzuserId[index];
                    //            assignedinteractions.location_id = long.Parse(item.location_id);
                    //            assignedinteractions.upload_id = uploadId;
                    //            assignedinteractions.nextServiceType = serviceTypeID.ToString();
                    //            assignedinteractions.nextServiceDate = item.nextServicedate;
                    //            assignedinteractions.assigned_manager_id = incremamangeridvar;
                    //            assignedinteractions.isautoassigned = false;//
                    //            dbContext.assignedinteractions.Add(assignedinteractions);
                    //            dbContext.SaveChanges();

                    //            index++;
                    //        }

                    //        if (index > (usersCount - 1)) index = 0;
                    //    }
                    //}


                    var str = "Insert into assignedcallsreport(assignInteractionID, assignedDate, assignmentType, moduletypeId, dueType, dueDate, uploadId, vehicleId, wyzuserId, campaignId, assigned_manager_id, workshop_id, service_id, isautoassigned) select id, uplodedCurrentDate,'SMR',1,nextServiceType,nextServiceDate,upload_id,vehical_Id,wyzUser_id,campaign_id,assigned_manager_id,location_id,0,1 from assignedinteraction where upload_id = @uploadId";
                    MySqlParameter[] param = new MySqlParameter[]
                      {
                            new MySqlParameter("uploadId", uploadId)
                      };

                    var result = dbContext.Database.ExecuteSqlCommand(str, param);

                    List<int> wyzuserId = new List<int>();
                    wyzuserId = assingedInteractionList.Select(s => Convert.ToInt32(s.wyzUser_id)).Distinct().ToList();

                    //if (getData.creList.Contains(','))
                    //{

                    foreach (var item in wyzuserId)
                    {
                        fmanualusers.upload_id = uploadId;
                        fmanualusers.wyzuser_id = item;
                        dbContext.fmanualusers.Add(fmanualusers);
                        dbContext.SaveChanges();
                    }
                    //}
                    //else
                    //{
                    //    //wyzuserId = getData.creList.ToList();
                    //    fmanualusers.upload_id = uploadId;
                    //    fmanualusers.wyzuser_id = long.Parse(getData.creList);
                    //    dbContext.fmanualusers.Add(fmanualusers);
                    //    dbContext.SaveChanges();
                    //}
                    // int res = dbContext.Database.ExecuteSqlCommand("call SmrManualAssignmentProcess(@upload_id)", new MySqlParameter("@upload_id", uploadId));

                    foreach (var item in wyzuserId)
                    {
                        var distinctCount = dbContext.assignedinteractions.Where(x => x.upload_id == uploadId && x.wyzUser_id == item).Count();
                        var userName = dbContext.wyzusers.Single(x => x.id == item).userName;
                        listOfModalRes.Add(new KeyValuePair<string, int>(userName, distinctCount));
                    }
                }
                return Json(new { success = true, modalResponse = listOfModalRes, totalAssigned = userAssingedinteractions.Count() });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            
            }
        }

        //getting data for summary table

        public ActionResult getDataForSummaryTable(string values)
        {
            List<CallLogDispositionLoadMRForcast> CallDis = new List<CallLogDispositionLoadMRForcast>();
            try
            {
                long incremamangerid = long.Parse(Session["UserId"].ToString());
                var filter = JsonConvert.DeserializeObject<forecastFilter>(values);
                string loc, serviceYear, serviceMonth;
                loc = filter.locationObj == null ? "" : filter.locationObj;
                serviceYear = filter.FromDueYearObj == null ? "" : Convert.ToDateTime(filter.FromDueYearObj.ToString()).ToString("yyyy-MM-dd");
                serviceMonth = filter.ToDueYearObj == null ? "" : Convert.ToDateTime(filter.ToDueYearObj.ToString()).ToString("yyyy-MM-dd");
                CallDis = getPsYearData(incremamangerid, loc, serviceYear, serviceMonth);
            }
            catch (Exception ex)
            {

            }
            return Json(new { data = CallDis, draw = Request["draw"], recordsTotal = CallDis.Count(), recordsFiltered = CallDis.Count() }, JsonRequestBehavior.AllowGet);
        }

        public List<CallLogDispositionLoadMRForcast> getPsYearData(long wyzid, string loc, string serviceYear, string serviceMonth)
        {
            List<CallLogDispositionLoadMRForcast> CallDis = new List<CallLogDispositionLoadMRForcast>();
            try
            {
                using (AutoSherDBContext dBContext = new AutoSherDBContext())
                {
                    string str = @"CALL smrForecastPSYearData(@incremamangerid ,@inloc, @inserviceYear, @inserviceMonth)";
                    //,"smrForecastPSYearDataView"); "
                    MySqlParameter[] param = new MySqlParameter[]
                    {
                                new MySqlParameter("incremamangerid",wyzid),
                                new MySqlParameter("inloc",loc),
                                new MySqlParameter("inserviceYear",serviceYear),
                                new MySqlParameter("inserviceMonth",serviceMonth)
                    };
                    CallDis = dBContext.Database.SqlQuery<CallLogDispositionLoadMRForcast>(str, param).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return CallDis;
        }

        //to download service reminder

        //public ActionResult downloadServiceReminder(string values)
        //{

        //    try
        //    {
        //        Session["filterValues"] = values;
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, error = ex.Message });
        //    }
        //    return Json(new { success = true });
        //}

        //public ActionResult Download()
        //{
        //    List<CallLogDispositionLoadMRForcast> CallService = new List<CallLogDispositionLoadMRForcast>();
        //    try
        //    {
        //        //string fileName = Session["FileName"].ToString();
        //        string values = Session["filterValues"].ToString();

        //        long incremamangeridvar = long.Parse(Session["UserId"].ToString());
        //        List<CallLogDispositionLoadMRForcast> CallMR = new List<CallLogDispositionLoadMRForcast>();
        //        var getData = JsonConvert.DeserializeObject<forecastFilter>(values);

        //        string inlocvar, fromservicedatevar
        //                       , toservicedatevar, inserviceTypevar, inpsYearvar, fromsalevar, tosalevar
        //                       , inpsMonth, inROAge, inlostPeriod, inforecastLogic, isassigned
        //                       , fromlastServicevar, tolastServicevar;

        //        inlocvar = getData.locationObj == null ? "0" : getData.locationObj;
        //        fromservicedatevar = getData.FromDueYearObj == null ? "0" : Convert.ToDateTime(getData.FromDueYearObj.ToString()).ToString("yyyy-MM-dd"); //from nxt ser date
        //        toservicedatevar = getData.ToDueYearObj == null ? "0" : Convert.ToDateTime(getData.ToDueYearObj.ToString()).ToString("yyyy-MM-dd"); //from nxt ser date
        //        inserviceTypevar = getData.selectedDueTypeObj == null ? "0" : getData.selectedDueTypeObj;//ser type id
        //        inpsYearvar = "0";
        //        fromsalevar = getData.FromSaleDateYearObj == null ? "0" : Convert.ToDateTime(getData.FromSaleDateYearObj.ToString()).ToString("yyyy-MM-dd"); //from sale date
        //        tosalevar = getData.ToSaleDateYearObj == null ? "0" : Convert.ToDateTime(getData.ToSaleDateYearObj.ToString()).ToString("yyyy-MM-dd");
        //        inpsMonth = "0";
        //        inROAge = getData.roObj == null ? "0" : getData.roObj;
        //        inlostPeriod = getData.noShowObj == null ? "0" : getData.noShowObj;
        //        inforecastLogic = getData.selectedForecastLogicObj == null ? "0" : getData.selectedForecastLogicObj;
        //        isassigned = getData.selectedIsAssignedObj == null ? "0" : getData.selectedIsAssignedObj;
        //        fromlastServicevar = getData.fromLastSerDateObj == null ? "0" : Convert.ToDateTime(getData.fromLastSerDateObj.ToString()).ToString("yyyy-MM-dd"); //from last ser date
        //        tolastServicevar = getData.toLastSerDateObj == null ? "0" : Convert.ToDateTime(getData.toLastSerDateObj.ToString()).ToString("yyyy-MM-dd"); //from last ser date

        //        using (AutoSherDBContext dBContext = new AutoSherDBContext())
        //        {
        //            string str = @"CALL smrforecastdataduedownload(@incremamangerid ,@inloc ,@inserviceYear ,@inserviceMonth ,@inserviceType ,@inpsYear ,@insaleYear 
        //                            ,@insaleMonth ,@inpsMonth ,@inROAge ,@inlostPeriod ,@inforecastLogic,@isassigned,@fromlastService,@tolastService)";

        //            MySqlParameter[] param = new MySqlParameter[]
        //           {
        //                    new MySqlParameter("incremamangerid", incremamangeridvar),
        //                    new MySqlParameter("inloc", inlocvar),
        //                    new MySqlParameter("inserviceYear", fromservicedatevar),
        //                    new MySqlParameter("inserviceMonth", toservicedatevar),
        //                    new MySqlParameter("inserviceType", inserviceTypevar),
        //                    new MySqlParameter("inpsYear", inpsYearvar),
        //                    new MySqlParameter("insaleYear", fromsalevar),
        //                    new MySqlParameter("insaleMonth", tosalevar),
        //                    new MySqlParameter("inpsMonth", inpsMonth),
        //                    new MySqlParameter("inROAge",inROAge),
        //                    new MySqlParameter("inlostPeriod", inlostPeriod),
        //                    new MySqlParameter("inforecastLogic", inforecastLogic),
        //                    new MySqlParameter("isassigned", isassigned),
        //                    new MySqlParameter("fromlastService",fromlastServicevar),
        //                    new MySqlParameter("tolastService", tolastServicevar)
        //           };
        //            CallMR = dBContext.Database.SqlQuery<CallLogDispositionLoadMRForcast>(str, param).ToList();
        //        }
        //        DataTable tbl = new DataTable("Service Reminder");
        //        tbl.Columns.Add("customerName", typeof(string));
        //        tbl.Columns.Add("customerPhone", typeof(string));
        //        tbl.Columns.Add("customerAddress", typeof(string));
        //        tbl.Columns.Add("vehicleNumber", typeof(string));
        //        tbl.Columns.Add("vehicalRegNo", typeof(string));
        //        tbl.Columns.Add("model", typeof(string));
        //        tbl.Columns.Add("saleDate", typeof(DateTime));
        //        tbl.Columns.Add("serviceDueBasedonTenure", typeof(DateTime));
        //        tbl.Columns.Add("averageRunning", typeof(string));
        //        tbl.Columns.Add("serviceDueBasedonMileage", typeof(DateTime));
        //        tbl.Columns.Add("nextServiceDue", typeof(DateTime));
        //        tbl.Columns.Add("forecastLogic", typeof(string));
        //        tbl.Columns.Add("serviceType", typeof(string));
        //        tbl.Columns.Add("lastVisitDate", typeof(DateTime));
        //        tbl.Columns.Add("lastVisitType", typeof(string));
        //        tbl.Columns.Add("lastVisitMileage", typeof(string));
        //        tbl.Columns.Add("lastServiceDate", typeof(DateTime));
        //        tbl.Columns.Add("lastServiceType", typeof(string));
        //        tbl.Columns.Add("lastServiceMileage", typeof(string));
        //        tbl.Columns.Add("averageMileage", typeof(string));
        //        tbl.Columns.Add("milege", typeof(string));
        //        tbl.Columns.Add("visitSuccessRate", typeof(string));
        //        tbl.Columns.Add("serviceNoShowPeriod", typeof(string));
        //        tbl.Columns.Add("nextServiceType", typeof(string));
        //        tbl.Columns.Add("WorkshopName", typeof(string));
        //        tbl.Columns.Add("EmailID", typeof(string));
        //        for (var i = 0; i < CallMR.Count; i++)
        //        {
        //            tbl.Rows.Add(CallMR[i].customerName, CallMR[i].phoneNumber, CallMR[i].address, CallMR[i].chassisnumber, CallMR[i].vehicleRegNum,
        //                         CallMR[i].model, CallMR[i].saledate, CallMR[i].nextServiceDateTenure, CallMR[i].averageRunning,
        //                         CallMR[i].nextServiceDateMilage, CallMR[i].nextServicedate, CallMR[i].forecastLogic, CallMR[i].serviceType,
        //                         CallMR[i].lastVisitDate, CallMR[i].lastVisitType, CallMR[i].lastServiceMileage, CallMR[i].lastServiceDate,
        //                         CallMR[i].lastServiceType, CallMR[i].lastServiceMileage, CallMR[i].averageMileage, CallMR[i].milege,
        //                         CallMR[i].visitSuccessRate, CallMR[i].serviceNoShowPeriod, CallMR[i].nextServiceType, CallMR[i].workshopName, CallMR[i].EmailID);
        //        }
        //        string fileName = "Service_Remainder_" + Guid.NewGuid().ToString() + ".xlsx";
        //        Response.Clear();
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
        //        //string handle = Guid.NewGuid().ToString();

        //        using (ExcelPackage pck = new ExcelPackage())
        //        {
        //            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Service_Reminder");
        //            ws.Cells["A1"].LoadFromDataTable(tbl, true);
        //            //var ms = new System.IO.MemoryStream();
        //            //pck.SaveAs(ms);

        //            //TempData[handle] = pck.GetAsByteArray();
        //            byte[] data = pck.GetAsByteArray() as byte[];

        //            //ms.WriteTo(Response.OutputStream);
        //            //return File(ms.ToArray(), "application/octet-stream", "Service_Remainder" + DateTime.Now.ToShortDateString() + DateTime.Now.TimeOfDay + ".xlsx");
        //            return File(data, "application/octet-stream", fileName);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return new EmptyResult();
        //}

        //to download service reminder
        [HttpPost]
        public ActionResult downloadServiceReminder(string values)
        {
            long totalCount = 0, toIndex = 500;
            //List<CallLogDispositionLoadMRForcast> CallService = new List<CallLogDispositionLoadMRForcast>();
            List<smrforecastDownloads> downloadServiceForecastAll = new List<smrforecastDownloads>();
            long j = 0;
            try
            {
                long incremamangeridvar = long.Parse(Session["UserId"].ToString());
                List<CallLogDispositionLoadMRForcast> CallMR = new List<CallLogDispositionLoadMRForcast>();
                var getData = JsonConvert.DeserializeObject<forecastFilter>(values);

                string inlocvar, fromservicedatevar
                               , toservicedatevar, inserviceTypevar, inpsYearvar, fromsalevar, tosalevar
                               , inpsMonth, inROAge, inlostPeriod, inforecastLogic, isassigned
                               , fromlastServicevar, tolastServicevar
                               , fromEWExpiryFrom, fromEWExpiryTo,workshopId, fromMCPExpiryFrom, fromMCPExpiryTo, modelCategories, excludeNegativeDeposition, excludeNegativeDepositionCallDateFrom, excludeNegativeDepositionCallDateTo;
                long fromIndex;
                inlocvar = getData.locationObj == null ? "0" : getData.locationObj;
                fromservicedatevar = getData.FromDueYearObj == null ? "0" : Convert.ToDateTime(getData.FromDueYearObj.ToString()).ToString("yyyy-MM-dd"); //from nxt ser date
                toservicedatevar = getData.ToDueYearObj == null ? "0" : Convert.ToDateTime(getData.ToDueYearObj.ToString()).ToString("yyyy-MM-dd"); //from nxt ser date
                inserviceTypevar = getData.selectedDueTypeObj == null ? "0" : getData.selectedDueTypeObj;//ser type id
                inpsYearvar = "0";
                fromsalevar = getData.FromSaleDateYearObj == null ? "0" : Convert.ToDateTime(getData.FromSaleDateYearObj.ToString()).ToString("yyyy-MM-dd"); //from sale date
                tosalevar = getData.ToSaleDateYearObj == null ? "0" : Convert.ToDateTime(getData.ToSaleDateYearObj.ToString()).ToString("yyyy-MM-dd");
                inpsMonth = "0";
                inROAge = getData.roObj == null ? "0" : getData.roObj;
                inlostPeriod = getData.noShowObj == null ? "0" : getData.noShowObj;
                inforecastLogic = getData.selectedForecastLogicObj == null ? "0" : getData.selectedForecastLogicObj;
                isassigned = getData.selectedIsAssignedObj == null ? "0" : getData.selectedIsAssignedObj;
                fromlastServicevar = getData.fromLastSerDateObj == null ? "0" : Convert.ToDateTime(getData.fromLastSerDateObj.ToString()).ToString("yyyy-MM-dd"); //from last ser date
                tolastServicevar = getData.toLastSerDateObj == null ? "0" : Convert.ToDateTime(getData.toLastSerDateObj.ToString()).ToString("yyyy-MM-dd"); //from last ser date
                fromEWExpiryFrom = getData.FromEWExpiryFrom == null ? "0" : Convert.ToDateTime(getData.FromEWExpiryFrom.ToString()).ToString("yyyy-MM-dd"); //from nxt ser date
                fromEWExpiryTo = getData.FromEWExpiryTo == null ? "0" : Convert.ToDateTime(getData.FromEWExpiryTo.ToString()).ToString("yyyy-MM-dd"); //from nxt ser date
                fromMCPExpiryFrom = getData.FromMCPExpiryFrom == null ? "0" : Convert.ToDateTime(getData.FromMCPExpiryFrom.ToString()).ToString("yyyy-MM-dd"); //from nxt ser date
                fromMCPExpiryTo = getData.FromMCPExpiryTo == null ? "0" : Convert.ToDateTime(getData.FromMCPExpiryTo.ToString()).ToString("yyyy-MM-dd"); //from nxt ser date
                modelCategories = getData.ModelCategories == null ? "0" : getData.ModelCategories;
                excludeNegativeDeposition = getData.ExcludeNegativeDeposition == null ? "0" : getData.ExcludeNegativeDeposition;
                excludeNegativeDepositionCallDateFrom = getData.ExcludeNegativeDepositionCallDateFrom == null ? "0" : Convert.ToDateTime(getData.ExcludeNegativeDepositionCallDateFrom.ToString()).ToString("yyyy-MM-dd");
                excludeNegativeDepositionCallDateTo = getData.ExcludeNegativeDepositionCallDateTo == null ? "0" : Convert.ToDateTime(getData.ExcludeNegativeDepositionCallDateTo.ToString()).ToString("yyyy-MM-dd"); ///from nxt ser date//start_withvar = 0;
                workshopId = getData.workshopId == null ? "0" : getData.workshopId;
            
                using (AutoSherDBContext dBContext = new AutoSherDBContext())
                {

                    totalCount = dBContext.Database.SqlQuery<int>("call smrforecastdataduedownloadCount(@incremamangerid ,@inloc ,@inserviceYear ,@inserviceMonth ,@inserviceType ,@inpsYear ,@insaleYear, @insaleMonth, @inpsMonth, @inROAge, @inlostPeriod, @inforecastLogic, @isassigned, @fromlastService, @tolastService,@workshopidd,@ewexpiryfrom,@ewexpiryto,@mcpfrom,@mcpto,@inmodelcat,@negativedispose,@negativecalldatefrom,@negativecalldateto)",
                    new MySqlParameter("incremamangerid", incremamangeridvar),
                            new MySqlParameter("inloc", inlocvar),
                            new MySqlParameter("inserviceYear", fromservicedatevar),
                            new MySqlParameter("inserviceMonth", toservicedatevar),
                            new MySqlParameter("inserviceType", inserviceTypevar),
                            new MySqlParameter("inpsYear", inpsYearvar),
                            new MySqlParameter("insaleYear", fromsalevar),
                            new MySqlParameter("insaleMonth", tosalevar),
                            new MySqlParameter("inpsMonth", inpsMonth),
                            new MySqlParameter("inROAge", inROAge),
                            new MySqlParameter("inlostPeriod", inlostPeriod),
                            new MySqlParameter("inforecastLogic", inforecastLogic),
                            new MySqlParameter("isassigned", isassigned),
                            new MySqlParameter("fromlastService", fromlastServicevar),
                            new MySqlParameter("tolastService", tolastServicevar),
                            new MySqlParameter("workshopidd", workshopId),
                            new MySqlParameter("ewexpiryfrom", fromEWExpiryFrom),
                            new MySqlParameter("ewexpiryto", fromEWExpiryTo),
                            new MySqlParameter("mcpfrom", fromMCPExpiryFrom),
                            new MySqlParameter("mcpto", fromMCPExpiryTo),
                            new MySqlParameter("inmodelcat", modelCategories),
                            new MySqlParameter("negativedispose", excludeNegativeDeposition),
                            new MySqlParameter("negativecalldatefrom", excludeNegativeDepositionCallDateFrom),
                            new MySqlParameter("negativecalldateto", excludeNegativeDepositionCallDateTo)).FirstOrDefault();


                    for (long i = 0; i < totalCount; i += 500)
                    {
                        List<smrforecastDownloads> downloadServiceForecast = new List<smrforecastDownloads>();
                        j = i;
                        string str = @"CALL smrforecastdataduedownload(@incremamangerid ,@inloc ,@inserviceYear ,@inserviceMonth ,@inserviceType ,@inpsYear ,@insaleYear 
                                    ,@insaleMonth ,@inpsMonth ,@inROAge ,@inlostPeriod ,@inforecastLogic,@isassigned,@fromlastService,@tolastService,@start_with,@length,@workshopidd,@ewexpiryfrom,@ewexpiryto,@mcpfrom,@mcpto,@inmodelcat,@negativedispose,@negativecalldatefrom,@negativecalldateto)";
                        MySqlParameter[] param = new MySqlParameter[]
                       {
                            new MySqlParameter("incremamangerid", incremamangeridvar),
                            new MySqlParameter("inloc", inlocvar),
                            new MySqlParameter("inserviceYear", fromservicedatevar),
                            new MySqlParameter("inserviceMonth", toservicedatevar),
                            new MySqlParameter("inserviceType", inserviceTypevar),
                            new MySqlParameter("inpsYear", inpsYearvar),
                            new MySqlParameter("insaleYear", fromsalevar),
                            new MySqlParameter("insaleMonth", tosalevar),
                            new MySqlParameter("inpsMonth", inpsMonth),
                            new MySqlParameter("inROAge",inROAge),
                            new MySqlParameter("inlostPeriod", inlostPeriod),
                            new MySqlParameter("inforecastLogic", inforecastLogic),
                            new MySqlParameter("isassigned", isassigned),
                            new MySqlParameter("fromlastService",fromlastServicevar),
                            new MySqlParameter("tolastService", tolastServicevar),
                            new MySqlParameter("start_with", i),
                            new MySqlParameter("length", toIndex),
                            new MySqlParameter("workshopidd", workshopId),
                            new MySqlParameter("ewexpiryfrom", fromEWExpiryFrom),
                            new MySqlParameter("ewexpiryto", fromEWExpiryTo),
                            new MySqlParameter("mcpfrom", fromMCPExpiryFrom),
                            new MySqlParameter("mcpto", fromMCPExpiryTo),
                            new MySqlParameter("inmodelcat", modelCategories),
                            new MySqlParameter("negativedispose", excludeNegativeDeposition),
                            new MySqlParameter("negativecalldatefrom", excludeNegativeDepositionCallDateFrom),
                            new MySqlParameter("negativecalldateto", excludeNegativeDepositionCallDateTo)
                       };
                        downloadServiceForecast = dBContext.Database.SqlQuery<smrforecastDownloads>(str, param).ToList();
                        downloadServiceForecastAll.AddRange(downloadServiceForecast);
                    }
                }
                DataTable downloadExceltable = new DataTable();
                if(downloadServiceForecastAll.Count>0)
                {
                    if (Session["DealerCode"].ToString() == "SHIVAMHYUNDAI")
                    {
                        var downloads = downloadServiceForecastAll.Select(m => new { m.customerName, m.chassisnumber, m.dnd, m.vehicleRegNum, m.model, m.saledate, m.nextServicedate, m.nextServiceType, m.nextServiceDateTenure, m.averageRunning, m.lastMileage, m.nextServiceDateMilage, m.forecastLogic, m.NoShowPeriod, m.LastServiceDate, m.lastServiceType }).ToList();
                        downloadExceltable = new SMRLiveController().ToDataTable(downloads);

                    }
                    else
                    {

                        var downloads = downloadServiceForecastAll.Select(m => new { m.customerName, m.phoneNumber, m.chassisnumber, m.dnd, m.vehicleRegNum, m.model, m.saledate, m.nextServicedate, m.nextServiceType, m.nextServiceDateTenure, m.averageRunning, m.lastMileage, m.nextServiceDateMilage, m.forecastLogic, m.NoShowPeriod, m.LastServiceDate, m.lastServiceType }).ToList();
                        downloadExceltable = new SMRLiveController().ToDataTable(downloads);

                    }
                }
                else
                {
                    return Json(new { success = false, error = "No Forecast Records Found." }, JsonRequestBehavior.AllowGet);

                }
                
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("Service Reminder", System.Text.Encoding.ASCII));
                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Service_Reminder");
                    ws.Cells["A1"].LoadFromDataTable(downloadExceltable, true);
                    Session["DownloadExcel_FileManager"] = pck.GetAsByteArray();
                }
            }
            catch (Exception ex)
            {
                long  k = j;
                return Json(new { success = false, error = ex.Message });
            }
            return Json(new { success = true });
        }

        public ActionResult Download()
        {
            try
            {
                //string fileName = Session["FileName"].ToString();

                if (Session["DownloadExcel_FileManager"] != null)
                {
                    //var data = new System.IO.MemoryStream();
                    //data = Session["DownloadExcel_FileManager"] as System.IO.MemoryStream;
                    //Session["DownloadExcel_FileManager"] = null;
                    byte[] data = Session["DownloadExcel_FileManager"] as byte[];
                    return File(data, "application/octet-stream", "Service_Remainder" + DateTime.Now.ToShortDateString() + DateTime.Now.TimeOfDay + ".xlsx");
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
        public ActionResult DownloadALL()
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");

            try
            {

                if (Session["DownloadExcel_FileManager"] != null)
                {
                    logger.Info("Forecast Download Started");

                    byte[] data = Session["DownloadExcel_FileManager"] as byte[];
                    Session["DownloadExcel_FileManager"] = null;
                    logger.Info("Forecast Download Ended");

                    return File(data, "application/octet-stream", "Service_Remainder" + DateTime.Now.ToShortDateString() + DateTime.Now.TimeOfDay + ".xlsx");
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

        [HttpPost]
        public JsonResult GetWorkshopListByLocationId(string locationIds )
        {
            if(!String.IsNullOrEmpty(locationIds))
            {
                //using (AutoSherDBContext db = new AutoSherDBContext())
                //{
                //    var WorkshopList = db.workshops.Where(m => m.location_cityId == locationId).Select(m => ).ToList();
                //    return Json(WorkshopList);
                //}


                var locations = locationIds.Split(',').Select(s => Convert.ToInt16(s));
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    var WorkshopList = db.workshops.Join(locations, w => w.location_cityId, e => e, (wrkshp, city) => new { id = wrkshp.id, name = wrkshp.workshopName }).ToList();
                    return Json(WorkshopList);
                }
            }

            return Json(null);
            }


        #region Service Forecast Limit Download
        public ActionResult downloadServiceForecastLimit(string connectionId)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {
                int toIndex = 1000;
                //List<CallLogDispositionLoadMRForcast> CallService = new List<CallLogDispositionLoadMRForcast>();
                List<smrforecasteddata> downloadServiceForecastAll = new List<smrforecasteddata>();
                using (var db = new AutoSherDBContext())
                {
                        int itemsCount = db.smrforecasteddatas.Count();
                    if (itemsCount > 0)
                    {
                        logger.Info("Forecast Started" + itemsCount);

                        for (int i = 0; i <= itemsCount; i += 1000)
                        {
                            // Thread.Sleep(500);
                            List<smrforecasteddata> downloadServiceForecast = new List<smrforecasteddata>();

                            downloadServiceForecast = db.smrforecasteddatas.OrderBy(m => m.id).Skip(i).Take(toIndex).ToList();
                            downloadServiceForecastAll.AddRange(downloadServiceForecast);
                            new liveProgressbar().SendProgress(connectionId,"Processing....", i, itemsCount);
                        }
                        DataTable downloadExceltable = new DataTable();
                        if (Session["DealerCode"].ToString() == "SHIVAMHYUNDAI")
                        {
                            var downloads = downloadServiceForecastAll.Select(m => new { m.customername, m.chassisnumber, m.dnd, m.vehicleregnum, m.model, m.saledate, m.nextServiceDate, m.nextservicetype, m.nextservicedatetenure, m.averagerunning, m.lastMileage, m.nextServiceDateMilage, m.forecastlogic, m.noShowPeriod, m.Lastservicedate, m.lastServicetype, m.maxfsdate, m.maxpsdate, m.workshopname, m.roAging }).ToList();
                            downloadExceltable = new SMRLiveController().ToDataTable(downloads);

                        }
                        else
                        {
                            var downloads = downloadServiceForecastAll.Select(m => new { m.customername, m.phonenumber, m.chassisnumber, m.dnd, m.vehicleregnum, m.model, m.saledate, m.nextServiceDate, m.nextservicetype, m.nextservicedatetenure, m.averagerunning, m.lastMileage, m.nextServiceDateMilage, m.forecastlogic, m.noShowPeriod, m.Lastservicedate, m.lastServicetype, m.maxfsdate, m.maxpsdate, m.workshopname, m.roAging }).ToList();
                            downloadExceltable = new SMRLiveController().ToDataTable(downloads);

                        }
                       
                        //FileContentResult robj;

                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("Service Reminder", System.Text.Encoding.ASCII));
                        using (ExcelPackage pck = new ExcelPackage())
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Service_Reminder");
                            ws.Cells["A1"].LoadFromDataTable(downloadExceltable, true);
                            //var ms = new System.IO.MemoryStream();
                            //pck.SaveAs(ms);
                            //ms.WriteTo(Response.OutputStream);
                            Session["DownloadExcel_FileManager"] = pck.GetAsByteArray();
                        }

                        new liveProgressbar().SendProgress(connectionId, "Processing....", itemsCount, itemsCount);
                        logger.Info("Forecast ended" + itemsCount);

                        return Json(new { success = true });

                        //Response.Clear();
                        //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        //Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("Service Reminder", System.Text.Encoding.ASCII));
                        //using (ExcelPackage pck = new ExcelPackage())
                        //{
                        //    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Service_Reminder");
                        //    ws.Cells["A1"].LoadFromDataTable(downloadExceltable, true);
                        //    robj = File(pck.GetAsByteArray(), System.Net.Mime.MediaTypeNames.Application.Octet, "serviceForecastData.xlsx");

                        //}
                        // new liveProgressbar().SendProgress(connectionId,"Downloading....", itemsCount, itemsCount);

                        //var jsondata = Json(new { robj, success = true }, JsonRequestBehavior.AllowGet);
                        //Json(new { robj, success = true }, JsonRequestBehavior.AllowGet);
                        //jsondata.MaxJsonLength = Int32.MaxValue;
                        //logger.Info("Forecast ended"+itemsCount);
                        //    return jsondata;
                    }
                    else
                    {
                            return Json(new { success = false, error = "No Forecast Records Found." }, JsonRequestBehavior.AllowGet);
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
                if (ex.StackTrace.Contains(':'))
                {
                    exception = "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)] + " " + exception;
                }

                logger.Info("Forecast Exception:\n"+exception);

                return Json(new { success = false, error = exception });
            }
        }

        #endregion
    }
    public class forecastFilter
        {
            public string locationObj { get; set; }
            public string noShowObj { get; set; }
            public DateTime? FromDueYearObj { get; set; }
            public DateTime? ToDueYearObj { get; set; }
            public DateTime? toLastSerDateObj { get; set; }
            public DateTime? fromLastSerDateObj { get; set; }
            public DateTime? FromSaleDateYearObj { get; set; }
            public DateTime? ToSaleDateYearObj { get; set; }
            public string roObj { get; set; }
            public string selectedForecastLogicObj { get; set; }
            public string selectedIsAssignedObj { get; set; }
            public string selectedDueTypeObj { get; set; }
            public string campaignControl { get; set; }
            public string creList { get; set; }
        public List<WorkshopUserMapper> selectedWrkshpUsrList { get; set; }
        public bool isFiltered { get; set; }
        //============
        public string locationName { get; set; }
        public string NoShowPeriod { get; set; }
        public DateTime? FromDueYear { get; set; }
        public DateTime? ToDueYear { get; set; }
        public DateTime? toLastSerDate { get; set; }
        public DateTime? fromLastSerDate { get; set; }
        public DateTime? FromSaleDateYear { get; set; }
        public DateTime? ToSaleDateYear { get; set; }
        public string selectedROAge { get; set; }
        public string selectedForecastLogic { get; set; }
        public string selectedIsAssigned { get; set; }
        public string selectedDueType { get; set; }
        public string workshopId { get; set; }
        public DateTime? FromEWExpiryFrom { get; set; }
        public DateTime? FromEWExpiryTo { get; set; }
        public DateTime? FromMCPExpiryFrom { get; set; }
        public DateTime? FromMCPExpiryTo { get; set; }
        public string ModelCategories { get; set; }
        public string ExcludeNegativeDeposition { get; set; }
        public DateTime? ExcludeNegativeDepositionCallDateFrom { get; set; }
        public DateTime? ExcludeNegativeDepositionCallDateTo { get; set; }

    }

    public class WorkshopUserMapper
    {
        public int workshop { get; set; }
        public int userId { get; set; }
        public string serviceTypeId { get; set; }
        public string categoryId { get; set; }
        public string ROAgeingId { get; set; }
    }
    
}
