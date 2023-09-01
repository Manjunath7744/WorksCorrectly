using AutoSherpa_project.Models;
using AutoSherpa_project.Models.ViewModels;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
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

    public class postSalesDetailsController : Controller
    {
        // GET: postSalesDetails
        #region for Post Sales Call Log
        public ActionResult postSalesLogs(int? id)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (id == 2)
                    {
                        Session["PostSalesFeedbackDayType"] = db.campaigns.FirstOrDefault(m => m.campaignName == "PostSales 2nd Day").id;
                        Session["PostSalesFeedbackCampaign"] = "PostSales 2nd Day";
                    }
                    else if (id == 15)
                    {
                        Session["PostSalesFeedbackDayType"] = db.campaigns.FirstOrDefault(m => m.campaignName == "PostSales 15th Day").id;
                        Session["PostSalesFeedbackCampaign"] = "PostSales 15th Day";
                    }
                    else if (id == 30)
                    {
                        Session["PostSalesFeedbackDayType"] = db.campaigns.FirstOrDefault(m => m.campaignName == "PostSales 30th Day").id;
                        Session["PostSalesFeedbackCampaign"] = "PostSales 30th Day";
                    }
                    else if (id == 45)
                    {
                        Session["PostSalesFeedbackDayType"] = db.campaigns.FirstOrDefault(m => m.campaignName == "PostSales 45th Day").id;
                        Session["PostSalesFeedbackCampaign"] = "PostSales 45th Day";
                    }

                    //var ddlWorkshop = db.workshops.Where(m => m.isinsurance == false).Select(model => new { workshopId = model.id, workshopName = model.workshopName }).OrderBy(m => m.workshopName).ToList();
                    //ViewBag.ddlWorkshop = ddlWorkshop;
                    var vehiclelists = db.vehicles.Select(m => new { m.model, m.dealershipName, m.vipDealer }).Distinct().ToList();
                    ViewBag.ddlmodel = vehiclelists.Where(m => m.model != null && !(string.IsNullOrEmpty(m.model))).Select(m => new { model = m.model }).Distinct().ToList();
                    ViewBag.ddldealerName = vehiclelists.Where(m => m.dealershipName != null && !(string.IsNullOrEmpty(m.dealershipName))).Select(m => new { dealershipName = m.dealershipName }).Distinct().ToList();
                    ViewBag.ddloutlet = vehiclelists.Where(m => m.vipDealer != null).Select(m => new { vipDealer = m.vipDealer }).Distinct().ToList();
                    List<long?> dispositiondataId = db.Postsalesdispositions.Select(m => m.callDispositionData_id).Distinct().ToList();
                    var ddllastdisposition = db.calldispositiondatas.Where(m => dispositiondataId.Contains(m.id)).Select(m => new { m.id, m.disposition }).ToList();
                    ViewBag.ddllastdisposition = ddllastdisposition;
                }

                if (TempData["SubmissionResult"] != null)
                {
                    if (TempData["SubmissionResult"].ToString() != "True")
                    {
                        ViewBag.dispoError = true;
                        ViewBag.dispositionResult = TempData["SubmissionResult"].ToString();
                    }
                    else
                    {
                        ViewBag.dispoError = false;
                        ViewBag.dispositionResult = "Disposition submitted";
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
                TempData["ControllerName"] = "PSF Day";

                return RedirectToAction("LogOff", "Home");
            }
            return View();
        }


        public ActionResult loadDashboardCounts()
        {
            int freshPSF, pendingFollow, nonContacts, incompleteSurvey, surveys, contacts, totalCalls;
            try
            {
                string UserId = string.Empty;

                //int UserId = Convert.ToInt32(Session["UserId"].ToString());
                
                using (var db = new AutoSherDBContext())
                {
                    if (Session["UserRole"].ToString() == "CRE")
                    {
                        UserId = Session["UserId"].ToString();
                    }
                    else
                    {
                        string userName = Session["UserName"].ToString();
                        var userListIds = db.wyzusers.Where(m => m.creManager == userName && m.role == "CRE" && m.unAvailable == false).Select(m => m.id).ToList();
                        UserId = string.Join(",", userListIds);

                    }

                    int campnId = int.Parse(Session["PostSalesFeedbackDayType"].ToString());

                    freshPSF = db.Database.SqlQuery<int>("call postsalesdhashboard(@dispotype,@inwyzuserid,@psfcampaignid);", new MySqlParameter[] { new MySqlParameter("@dispotype", 1), new MySqlParameter("@inwyzuserid", UserId), new MySqlParameter("@psfcampaignid", campnId) }).FirstOrDefault();
                    pendingFollow = db.Database.SqlQuery<int>("call postsalesdhashboard(@dispotype,@inwyzuserid,@psfcampaignid);", new MySqlParameter[] { new MySqlParameter("@dispotype", 2), new MySqlParameter("@inwyzuserid", UserId), new MySqlParameter("@psfcampaignid", campnId) }).FirstOrDefault();
                    nonContacts = db.Database.SqlQuery<int>("call postsalesdhashboard(@dispotype,@inwyzuserid,@psfcampaignid);", new MySqlParameter[] { new MySqlParameter("@dispotype", 3), new MySqlParameter("@inwyzuserid", UserId), new MySqlParameter("@psfcampaignid", campnId) }).FirstOrDefault();
                    incompleteSurvey = db.Database.SqlQuery<int>("call postsalesdhashboard(@dispotype,@inwyzuserid,@psfcampaignid);", new MySqlParameter[] { new MySqlParameter("@dispotype", 4), new MySqlParameter("@inwyzuserid", UserId), new MySqlParameter("@psfcampaignid", campnId) }).FirstOrDefault();

                    surveys = db.Database.SqlQuery<int>("call postsalesdhashboard(@dispotype,@inwyzuserid,@psfcampaignid);", new MySqlParameter[] { new MySqlParameter("@dispotype", 5), new MySqlParameter("@inwyzuserid", UserId), new MySqlParameter("@psfcampaignid", campnId) }).FirstOrDefault();
                    contacts = db.Database.SqlQuery<int>("call postsalesdhashboard(@dispotype,@inwyzuserid,@psfcampaignid);", new MySqlParameter[] { new MySqlParameter("@dispotype", 6), new MySqlParameter("@inwyzuserid", UserId), new MySqlParameter("@psfcampaignid", campnId) }).FirstOrDefault();
                    totalCalls = db.Database.SqlQuery<int>("call postsalesdhashboard(@dispotype,@inwyzuserid,@psfcampaignid);", new MySqlParameter[] { new MySqlParameter("@dispotype", 7), new MySqlParameter("@inwyzuserid", UserId), new MySqlParameter("@psfcampaignid", campnId) }).FirstOrDefault();

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

            return Json(new { success = true, freshPSF, pendingFollow, nonContacts, incompleteSurvey, surveys, contacts, totalCalls });
        }
        public ActionResult GetBucketData(string psfData)
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string Pattern = Request["search[value]"];
            string exception = "";
            string UserId = string.Empty;
            List<postsalesBucketDAtaVM> postsalesVM = new List<postsalesBucketDAtaVM>();

            string FromBillDate, ToBillDate, workshopId, modelcategory, model, serviceoutlet, delaership, ddllastdisposition;
           
            long fromIndex, toIndex, dispoType;
            int totalCount = 0;
            int postsalesVMCount = 0;
            FiltertpostSalesVM filter = new FiltertpostSalesVM();
            if (psfData != null)
            {
                filter = JsonConvert.DeserializeObject<FiltertpostSalesVM>(psfData);
            }
            ddllastdisposition = filter.ddllastdisposition == null || filter.ddllastdisposition == "" ? "0" : filter.ddllastdisposition;
            FromBillDate = filter.FromBillDate == null ? "" : Convert.ToDateTime(filter.FromBillDate.ToString()).ToString("yyyy-MM-dd");
            ToBillDate = filter.ToBillDate == null ? "" : Convert.ToDateTime(filter.ToBillDate.ToString()).ToString("yyyy-MM-dd");
            workshopId = filter.Workshop == null || filter.Workshop == "" ? "0" : filter.Workshop;
            model = filter.model == null ? "" : filter.model;
            delaership = filter.ddldealername == null ? "" : filter.ddldealername;
            serviceoutlet = filter.ddlserviceoutlet == null ? "" : filter.ddlserviceoutlet;
            modelcategory = filter.ddlmodelCategory == null ? "" : filter.ddlmodelCategory;
            if (Pattern != "")
            {
                filter.isFiltered = true;
            }

            using (var db = new AutoSherDBContext())
            {
                if (Session["UserRole"].ToString() == "CRE")
                {
                    UserId = Session["UserId"].ToString();
                }
                else
                {
                    string userName= Session["UserName"].ToString();
                    var userListIds = db.wyzusers.Where(m => m.creManager == userName && m.role == "CRE" && m.unAvailable == false).Select(m => m.id).ToList();
                    UserId = string.Join(",", userListIds);
                     
                }
                db.Database.CommandTimeout = 900;
                try
                {
                    if (filter.getDataFor == 1)//Days
                    {

                        totalCount = getPostSalesCreAllBucketcount(1, "", "", "", "", Session["PostSalesFeedbackDayType"].ToString(), "", UserId,"","","0");

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


                        postsalesVM = getPostSalesCreAllBucket(1, FromBillDate, ToBillDate, model, modelcategory, Session["PostSalesFeedbackDayType"].ToString(), Pattern, UserId, fromIndex, toIndex, serviceoutlet, delaership,ddllastdisposition);
                        {
                            postsalesVMCount = getPostSalesCreAllBucketcount(1, FromBillDate, ToBillDate, model, modelcategory, Session["PostSalesFeedbackDayType"].ToString(), Pattern, UserId, serviceoutlet, delaership, ddllastdisposition);
                        }
                    }
                    else if (filter.getDataFor == 2)//FollowUp
                    {
                        dispoType = 4;
                        totalCount = getPostSalesCreAllBucketcount(4, "", "", "", "", Session["PostSalesFeedbackDayType"].ToString(), "", UserId,"","","0");
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

                        postsalesVM = getPostSalesCreAllBucket(4, FromBillDate, ToBillDate, model, modelcategory, Session["PostSalesFeedbackDayType"].ToString(), Pattern, UserId, fromIndex, toIndex, serviceoutlet, delaership, ddllastdisposition);
                        if (filter.isFiltered == true)
                        {
                            postsalesVMCount = getPostSalesCreAllBucketcount(4, FromBillDate, ToBillDate, model, modelcategory, Session["PostSalesFeedbackDayType"].ToString(), Pattern, UserId, serviceoutlet, delaership, ddllastdisposition);

                        }
                    }
                    //else if (filter.getDataFor == 3)//Completed Survey
                    //{
                    //    dispoType = 22;
                    //    totalCount = getPSFBucketCount("", "", Session["PostSalesFeedbackDayType"].ToString(), "", UserId, dispoType, 1);


                    //    fromIndex = start;
                    //    toIndex = length;
                    //    if (toIndex < 0)
                    //    {
                    //        toIndex = 10;
                    //    }

                    //    if (toIndex > totalCount)
                    //    {
                    //        toIndex = totalCount;
                    //    }




                    //    postsalesVM = getFollowUpAndCompletedSurvey(FromBillDate, ToBillDate, Session["PostSalesFeedbackDayType"].ToString(), Pattern, UserId, dispoType, fromIndex, toIndex, workshopId, "", "");
                    //    if (filter.isFiltered == true)
                    //    {
                    //        postsalesVM = getFollowUpAndCompletedSurvey(FromBillDate, ToBillDate, Session["PostSalesFeedbackDayType"].ToString(), Pattern, UserId, dispoType, 0, totalCount, workshopId, "", "");
                    //    }

                    //}
                    else if (filter.getDataFor == 4)//Dissatisfied
                    {
                        dispoType = 44;
                        totalCount = getPostSalesCreAllBucketcount(3, "", "", "", "", Session["PostSalesFeedbackDayType"].ToString(), "", UserId,"","","0");
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


                        postsalesVM = getPostSalesCreAllBucket(3, FromBillDate, ToBillDate, model, modelcategory, Session["PostSalesFeedbackDayType"].ToString(), Pattern, UserId, fromIndex, toIndex, serviceoutlet, delaership, ddllastdisposition);
                        if (filter.isFiltered == true)
                        {
                            postsalesVMCount = getPostSalesCreAllBucketcount(3, FromBillDate, ToBillDate, model, modelcategory, Session["PostSalesFeedbackDayType"].ToString(), Pattern, UserId, serviceoutlet, delaership, ddllastdisposition);

                        }
                    }
                    else if (filter.getDataFor == 5)//Non Contacts
                    {
                        dispoType = 1;
                        totalCount = getPostSalesCreAllBucketcount(6, "", "", "", "", Session["PostSalesFeedbackDayType"].ToString(), "", UserId,"","","0");


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
                        postsalesVM = getPostSalesCreAllBucket(6,FromBillDate, ToBillDate,model,modelcategory, Session["PostSalesFeedbackDayType"].ToString(), Pattern, UserId, fromIndex, toIndex, serviceoutlet, delaership, ddllastdisposition);
                        if (filter.isFiltered == true)
                        {
                            postsalesVMCount = getPostSalesCreAllBucketcount(6,FromBillDate, ToBillDate,model,modelcategory ,Session["PostSalesFeedbackDayType"].ToString(), Pattern, UserId, serviceoutlet, delaership, ddllastdisposition);
                        }


                    }
                    //else if (filter.getDataFor == 6)//Dropped
                    //{

                    //    dispoType = 2;
                    //    totalCount = getPSFBucketCount("", "", Session["PostSalesFeedbackDayType"].ToString(), "", UserId, dispoType, 2);


                    //    fromIndex = start;
                    //    toIndex = length;
                    //    if (toIndex < 0)
                    //    {
                    //        toIndex = 10;
                    //    }

                    //    if (toIndex > totalCount)
                    //    {
                    //        toIndex = totalCount;
                    //    }



                    //    postsalesVM = getnonContacts(FromBillDate, ToBillDate, Session["PostSalesFeedbackDayType"].ToString(), Pattern, UserId, dispoType, fromIndex, toIndex, workshopId, "", "");
                    //    if (filter.isFiltered == true)
                    //    {
                    //        postsalesVM = getnonContacts(FromBillDate, ToBillDate, Session["PostSalesFeedbackDayType"].ToString(), Pattern, UserId, dispoType, 0, totalCount, workshopId, "", "");
                    //    }

                    //}
                    else if (filter.getDataFor == 7)//Resolved
                    {

                        totalCount = getPostSalesCreAllBucketcount(2, "", "", "", "", Session["PostSalesFeedbackDayType"].ToString(), "", UserId,"","","0");


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

                        postsalesVM = getPostSalesCreAllBucket(2, FromBillDate, ToBillDate, model, modelcategory, Session["PostSalesFeedbackDayType"].ToString(), Pattern, UserId, fromIndex, toIndex, serviceoutlet, delaership, ddllastdisposition);

                        if (filter.isFiltered == true)
                        {
                            postsalesVMCount = getPostSalesCreAllBucketcount(2, FromBillDate, ToBillDate, model, modelcategory, Session["PostSalesFeedbackDayType"].ToString(), Pattern, UserId, serviceoutlet, delaership, ddllastdisposition);
                        }

                    }
                    else if (filter.getDataFor == 8)//FuTureFollowUp
                    {
                        totalCount = getPostSalesCreAllBucketcount(5, "", "", "", "", Session["PostSalesFeedbackDayType"].ToString(), "", UserId,"","","0");

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

                        postsalesVM = getPostSalesCreAllBucket(5, FromBillDate, ToBillDate, model, modelcategory, Session["PostSalesFeedbackDayType"].ToString(), Pattern, UserId, fromIndex, toIndex, serviceoutlet, delaership, ddllastdisposition);
                        if (filter.isFiltered == true)
                        {
                            postsalesVMCount = getPostSalesCreAllBucketcount(5, FromBillDate, ToBillDate, model, modelcategory, Session["PostSalesFeedbackDayType"].ToString(), Pattern, UserId, serviceoutlet, delaership, ddllastdisposition);
                        }
                    }

                }
                catch (Exception ex)
                {

                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException != null)
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
            }


            if (postsalesVM != null)
            {
                if (filter.isFiltered == true)
                {
                    return Json(new { exception = exception, data = postsalesVM, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = postsalesVMCount }, JsonRequestBehavior.AllowGet);
                }
                else if (filter.isFiltered == false)
                {
                    return Json(new { exception = exception, data = postsalesVM, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount }, JsonRequestBehavior.AllowGet);
                }

            }

            return Json(new { exception = exception, data = "", draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0 }, JsonRequestBehavior.AllowGet);
        }

        #region post sales buckets
        public int getPostSalesCreAllBucketcount(int dispotype, string instartdate, string inenddate, string inmodel, string inmodelcategory, string incampaignid, string pattern, string wyzuserid, string inserviceoutlet, string indealer,string lastdisposition)
        {
            using (var db = new AutoSherDBContext())
            {
                return db.Database.SqlQuery<int>("call PostSalesCreAllBucketcount(@dispotype,@instartdate,@inenddate,@inmodel,@inmodelcategory,@incampaignid,@pattern,@wyzuserid,@inserviceoutlet,@indealer,@insecondarydisposition)",
                            new MySqlParameter[] {
                                    new MySqlParameter("@dispotype",dispotype),
                                    new MySqlParameter("@instartdate",instartdate),
                                    new MySqlParameter("@inenddate", inenddate),
                                    new MySqlParameter("@inmodel", inmodel),
                                    new MySqlParameter("@inmodelcategory", inmodelcategory),
                                    new MySqlParameter("@incampaignid", incampaignid),
                                    new MySqlParameter("@pattern", pattern),
                                                                                new MySqlParameter("@wyzuserid", wyzuserid),
                new MySqlParameter("@inserviceoutlet", inserviceoutlet),
                                new MySqlParameter("@indealer", indealer),
                                new MySqlParameter("@insecondarydisposition", lastdisposition)
                            }).FirstOrDefault();

            }

            return 0;
        }
        public List<postsalesBucketDAtaVM> getPostSalesCreAllBucket(int dispotype, string instartdate, string inenddate, string inmodel, string inmodelcategory, string incampaignid, string pattern, string wyzuserid, long start_with, long length,string inserviceoutlet,string indealer,string lastdisposition)
        {
            List<postsalesBucketDAtaVM> psfDetails = new List<postsalesBucketDAtaVM>();

            using (AutoSherDBContext dBContext = new AutoSherDBContext())
            {
                string str = @"call PostSalesCreAllBucket(@dispotype,@instartdate,@inenddate,@inmodel,@inmodelcategory,@incampaignid,@pattern,@wyzuserid,@start_with,@length,@inserviceoutlet,@indealer,@insecondarydisposition)";
                MySqlParameter[] param = new MySqlParameter[]
                {
                        new MySqlParameter("@dispotype",dispotype),
                                    new MySqlParameter("@instartdate",instartdate),
                                    new MySqlParameter("@inenddate", inenddate),
                                    new MySqlParameter("@inmodel", inmodel),
                                    new MySqlParameter("@inmodelcategory", inmodelcategory),
                                    new MySqlParameter("@incampaignid", incampaignid),
                                    new MySqlParameter("@pattern", pattern),
                                    new MySqlParameter("@wyzuserid", wyzuserid),
                                    new MySqlParameter("@start_with", start_with),
                                    new MySqlParameter("@length",length),
                                    new MySqlParameter("@inserviceoutlet",inserviceoutlet),
                                    new MySqlParameter("@indealer",indealer),
                                                                    new MySqlParameter("@insecondarydisposition", lastdisposition)

                };
                psfDetails = dBContext.Database.SqlQuery<postsalesBucketDAtaVM>(str, param).ToList();
            }
            return psfDetails;
        }


        #endregion
        #endregion


        #region post sales Complaint
        public ActionResult postsalesComplaintLogs()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    //var ddlWorkshop = db.workshops.Where(m => m.isinsurance == false).Select(model => new { workshopId = model.id, workshopName = model.workshopName }).OrderBy(m => m.workshopName).ToList();
                    //ViewBag.ddlWorkshop = ddlWorkshop;
                    //ViewBag.ddlWorkshop = ddlWorkshop;
                    var vehiclelists = db.vehicles.Select(m => new { m.model, m.dealershipName, m.vipDealer }).Distinct().ToList();
                    ViewBag.ddlmodel = vehiclelists.Where(m => m.model != null).Select(m => new { model = m.model }).Distinct().ToList();
                    ViewBag.ddldealerName = vehiclelists.Where(m => m.dealershipName != null).Select(m => new { dealershipName = m.dealershipName }).Distinct().ToList();
                    ViewBag.ddloutlet = vehiclelists.Where(m => m.vipDealer != null).Select(m => new { vipDealer = m.vipDealer }).Distinct().ToList();
                    if (TempData["SubmissionResult"] != null)
                    {
                        if (TempData["SubmissionResult"].ToString() != "True")
                        {
                            ViewBag.dispoError = true;
                            ViewBag.dispositionResult = TempData["SubmissionResult"].ToString();
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
                TempData["ControllerName"] = "PSFComplaints";

                return RedirectToAction("LogOff", "Home");
            }
            return View();
        }


        public ActionResult loadComplaintDashboardCounts()
        {
            int freshPSF, pendingFollow, nonContacts, Rework, surveys, contacts, totalCalls;
            try
            {
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                using (var dBContext = new AutoSherDBContext())
                {

                    freshPSF = dBContext.Database.SqlQuery<int>("call postsalescompdashboardcount(@bucket_id,@inwyzuser_id);", new MySqlParameter[] { new MySqlParameter("@bucket_id", 1), new MySqlParameter("@inwyzuser_id", UserId) }).FirstOrDefault();
                    pendingFollow = dBContext.Database.SqlQuery<int>("call postsalescompdashboardcount(@bucket_id,@inwyzuser_id);", new MySqlParameter[] { new MySqlParameter("@bucket_id", 2), new MySqlParameter("@inwyzuser_id", UserId) }).FirstOrDefault();
                    nonContacts = dBContext.Database.SqlQuery<int>("call postsalescompdashboardcount(@bucket_id,@inwyzuser_id);", new MySqlParameter[] { new MySqlParameter("@bucket_id", 3), new MySqlParameter("@inwyzuser_id", UserId) }).FirstOrDefault();
                    Rework = dBContext.Database.SqlQuery<int>("call postsalescompdashboardcount(@bucket_id,@inwyzuser_id);", new MySqlParameter[] { new MySqlParameter("@bucket_id", 4), new MySqlParameter("@inwyzuser_id", UserId) }).FirstOrDefault();
                    surveys = dBContext.Database.SqlQuery<int>("call postsalescompdashboardcount(@bucket_id,@inwyzuser_id);", new MySqlParameter[] { new MySqlParameter("@bucket_id", 5), new MySqlParameter("@inwyzuser_id", UserId) }).FirstOrDefault();
                    contacts = dBContext.Database.SqlQuery<int>("call postsalescompdashboardcount(@bucket_id,@inwyzuser_id);", new MySqlParameter[] { new MySqlParameter("@bucket_id", 6), new MySqlParameter("@inwyzuser_id", UserId) }).FirstOrDefault();
                    totalCalls = dBContext.Database.SqlQuery<int>("call postsalescompdashboardcount(@bucket_id,@inwyzuser_id);", new MySqlParameter[] { new MySqlParameter("@bucket_id", 7), new MySqlParameter("@inwyzuser_id", UserId) }).FirstOrDefault();

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

            return Json(new { success = true, freshPSF, pendingFollow, nonContacts, Rework, surveys, contacts, totalCalls });
        }


        public ActionResult getpostSalesBuckets(string psfValues)
        {
            int totalCount = 0, postsalesCount = 0;
            string exception = "";
            string FromBillDate, ToBillDate, workshopId, modelcategory, model, delaership, serviceoutlet;

            long dispotype, inwyzuserid, inworkshopId, startwith, length;
            inwyzuserid = Convert.ToInt32(Session["UserId"].ToString());
            startwith = Convert.ToInt32(Request["start"]);
            length = Convert.ToInt32(Request["length"]);
            string inPattern = Request["search[value]"];
            List<postCCMSalesVM> postSalesDetailsVM = null;

            FiltertpostSalesVM complaintfilter = new FiltertpostSalesVM();
            if (psfValues != null)
            {
                complaintfilter = JsonConvert.DeserializeObject<FiltertpostSalesVM>(psfValues);
            }
            if (inPattern != "")
            {
                complaintfilter.isFiltered = true;
            }

            if (length < 0)
            {
                length = 10;
            }
            using (AutoSherDBContext dBContext = new AutoSherDBContext())
            {
                try
                {

                    FromBillDate = complaintfilter.FromBillDate == null ? "" : Convert.ToDateTime(complaintfilter.FromBillDate.ToString()).ToString("yyyy-MM-dd");
                    ToBillDate = complaintfilter.ToBillDate == null ? "" : Convert.ToDateTime(complaintfilter.ToBillDate.ToString()).ToString("yyyy-MM-dd");
                    workshopId = complaintfilter.Workshop == null || complaintfilter.Workshop == "" ? "0" : complaintfilter.Workshop;
                    model = complaintfilter.model == null ? "" : complaintfilter.model;
                    modelcategory = complaintfilter.ddlmodelCategory == null ? "" : complaintfilter.ddlmodelCategory;
                    inworkshopId = complaintfilter.Workshop == null || complaintfilter.Workshop == "" ? 0 : Convert.ToInt64(complaintfilter.Workshop);
                    dispotype = Convert.ToInt64(complaintfilter.bucketId);
                    delaership = complaintfilter.ddldealername == null ? "" : complaintfilter.ddldealername;
                    serviceoutlet = complaintfilter.ddlserviceoutlet == null ? "" : complaintfilter.ddlserviceoutlet;

                    totalCount = getpostsalesComplaintCount(dispotype, "", "", "", "", "", inwyzuserid,"","");
                    if (length > totalCount)
                    {
                        length = totalCount;
                    }
                    postSalesDetailsVM = getpostsalesComplaints(dispotype, FromBillDate, ToBillDate, model, modelcategory, inPattern, inwyzuserid, startwith, length,serviceoutlet,delaership);
                    if (complaintfilter.isFiltered == true)
                    {
                        postsalesCount = getpostsalesComplaintCount(dispotype, FromBillDate, ToBillDate, model,modelcategory,inPattern,inwyzuserid, serviceoutlet, delaership);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException != null)
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

                if (postSalesDetailsVM != null)
                {
                    if (complaintfilter.isFiltered == true)
                    {
                        return Json(new { exception = exception, data = postSalesDetailsVM, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = postsalesCount }, JsonRequestBehavior.AllowGet);
                    }
                    else if (complaintfilter.isFiltered == false)
                    {
                        return Json(new { exception = exception, data = postSalesDetailsVM, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount }, JsonRequestBehavior.AllowGet);
                    }

                }
            }
            return Json(new { exception = exception, data = "", draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0 }, JsonRequestBehavior.AllowGet);
        }


    public int getpostsalesComplaintCount(long dispotype, string instartdate, string inenddate, string inmodel, string inmodelcategory, string pattern, long wyzuserid, string inserviceoutlet, string indealer)
    {
        using (var db = new AutoSherDBContext())
        {
            return db.Database.SqlQuery<int>("call PostSalescompcount(@dispotype,@instartdate,@inenddate,@inmodel,@inmodelcategory,@pattern,@wyzuserid,@inserviceoutlet,@indealer)",
                        new MySqlParameter[] {
                                    new MySqlParameter("@dispotype",dispotype),
                                    new MySqlParameter("@instartdate",instartdate),
                                    new MySqlParameter("@inenddate", inenddate),
                                    new MySqlParameter("@inmodel", inmodel),
                                    new MySqlParameter("@inmodelcategory", inmodelcategory),
                                    new MySqlParameter("@pattern", pattern),
                                new MySqlParameter("@wyzuserid", wyzuserid),
                                new MySqlParameter("@inserviceoutlet", inserviceoutlet),
                                new MySqlParameter("@indealer", indealer)}).FirstOrDefault();
        }

        return 0;
    }

    public List<postCCMSalesVM> getpostsalesComplaints(long dispotype, string instartdate, string inenddate, string inmodel, string inmodelcategory, string pattern, long wyzuserid, long start_with, long length, string inserviceoutlet, string indealer)
        {
            List<postCCMSalesVM> psfDetails = new List<postCCMSalesVM>();
            //try
            //{
            using (AutoSherDBContext dBContext = new AutoSherDBContext())
            {
                string str = @"call postsalescompcalllog(@dispotype,@instartdate,@inenddate,@inmodel,@inmodelcategory,@pattern,@wyzuserid,@start_with,@length,@inserviceoutlet,@indealer)";
                MySqlParameter[] param = new MySqlParameter[]
                {
                        new MySqlParameter("dispotype",dispotype),
                        new MySqlParameter("instartdate",instartdate),
                        new MySqlParameter("inenddate",inenddate),
                        new MySqlParameter("inmodel",inmodel),
                        new MySqlParameter("inmodelcategory",inmodelcategory),
                        new MySqlParameter("pattern",pattern),
                        new MySqlParameter("wyzuserid",wyzuserid),
                        new MySqlParameter("start_with",start_with),
                        new MySqlParameter("length",length),
                          new MySqlParameter("@inserviceoutlet",inserviceoutlet),
                                    new MySqlParameter("@indealer",indealer)
                };
                psfDetails = dBContext.Database.SqlQuery<postCCMSalesVM>(str, param).ToList();
            }
            //}
            //catch (Exception ex)
            //{

            //}
            return psfDetails;
        }


        #endregion

        // GET: PostSalesLive
        #region  PostSalesLive

        public ActionResult postsalesLive()
        {
            string role = Session["UserRole"].ToString();
            int userId = Convert.ToInt32(Session["UserId"]);

            try
            {
                string userName = Session["UserName"].ToString();

                using (var db = new AutoSherDBContext())
                {
                    var campaignList = db.campaigns.Where(m => m.campaignType == "Post Sales" && m.isactive).Select(m => new { id = m.id, campaignName = m.campaignName }).OrderBy(m => m.campaignName).ToList();
                    var workshopList = db.workshops.Select(m => new { workshopName = m.workshopName, id = m.id }).ToList();


                    if (role == "CREManager")
                    {
                        //var ddlmanager = db.wyzusers.Where(m => m.userName == userName).Select(model => new { id = model.id, creName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.creName).ToList();
                        //ViewBag.ddlmanager = ddlmanager;

                        var ddlcres = db.wyzusers.Where(m => m.creManager == userName && m.role == "CRE" && m.role1 == "5" && m.unAvailable == false).Select(model => new { id = model.id, userName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.userName).ToList();
                        ViewBag.ddlcreList = ddlcres;

                    }
                    else if (role == "Admin")
                    {
                        var ddlcres = db.wyzusers.Where(m => m.role1 == "5" && m.role == "CRE" && m.unAvailable == false).Select(model => new { id = model.id, userName = model.firstName + "(" + model.userName + ")" }).OrderBy(m => m.userName).ToList();
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
                TempData["ControllerName"] = "PostSalesReports";

                return RedirectToAction("LogOff", "Home");

            }
            return View();
        }
        public ActionResult PostSalesLiveloadDashboardCounts()
        {
            int BoxPostSalesLiveCRE, BoxPostSalesLiveCalls, BoxPostSalesLiveCRECalls, BoxPostSalesLiveContact, BoxPostSalesLiveContactPercent, BoxPostSalesLiveComplete, BoxPostSalesLiveSatisfied, BoxPostSalesLiveDisSatisfied;
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

                    BoxPostSalesLiveCRE = db.Database.SqlQuery<int>("call live_reports_postsales(@reportid,@increname,@incremanager,@inworkshop,@increid,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 1), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshop", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxPostSalesLiveCalls = db.Database.SqlQuery<int>("call live_reports_postsales(@reportid,@increname,@incremanager,@inworkshop,@increid,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 2), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshop", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxPostSalesLiveCRECalls = db.Database.SqlQuery<int>("call live_reports_postsales(@reportid,@increname,@incremanager,@inworkshop,@increid,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 3), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshop", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxPostSalesLiveContact = db.Database.SqlQuery<int>("call live_reports_postsales(@reportid,@increname,@incremanager,@inworkshop,@increid,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 4), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshop", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxPostSalesLiveContactPercent = db.Database.SqlQuery<int>("call live_reports_postsales(@reportid,@increname,@incremanager,@inworkshop,@increid,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 5), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshop", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxPostSalesLiveComplete = db.Database.SqlQuery<int>("call live_reports_postsales(@reportid,@increname,@incremanager,@inworkshop,@increid,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 6), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshop", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxPostSalesLiveSatisfied = db.Database.SqlQuery<int>("call live_reports_postsales(@reportid,@increname,@incremanager,@inworkshop,@increid,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 7), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshop", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();
                    BoxPostSalesLiveDisSatisfied = db.Database.SqlQuery<int>("call live_reports_postsales(@reportid,@increname,@incremanager,@inworkshop,@increid,@incampaign);", new MySqlParameter[] { new MySqlParameter("@reportid", 8), new MySqlParameter("@increname", creName), new MySqlParameter("@incremanager", managerName), new MySqlParameter("@inworkshop", ""), new MySqlParameter("@increid", ""), new MySqlParameter("@incampaign", "") }).FirstOrDefault();

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

            return Json(new { success = true, BoxPostSalesLiveCRE, BoxPostSalesLiveCalls, BoxPostSalesLiveCRECalls, BoxPostSalesLiveContact, BoxPostSalesLiveContactPercent, BoxPostSalesLiveComplete, BoxPostSalesLiveSatisfied, BoxPostSalesLiveDisSatisfied });
        }
        public ActionResult getPostSales(string PostSalesData)
        {
            string exception = "";
            DataTable PostSalesReports = new DataTable();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string role = Session["UserRole"].ToString();
                    long userId = Convert.ToInt64(Session["UserId"]);

                    reportFilter filter = new reportFilter();

                    if (PostSalesData != null)
                    {
                        filter = JsonConvert.DeserializeObject<reportFilter>(PostSalesData);
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
                        using (MySqlCommand cmd = new MySqlCommand("live_reports_postsales", connection))
                        {
                            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("reportid", reportId));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("increname", CREName));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("incremanager", manageruserName));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("inworkshop", workshopList));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("increid", creList));
                            adapter.SelectCommand.Parameters.Add(new MySqlParameter("incampaign", CampaignList));
                            adapter.Fill(PostSalesReports);
                        }
                    }

                    var results = JsonConvert.SerializeObject(PostSalesReports, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
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
                var results = JsonConvert.SerializeObject(PostSalesReports, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                return Json(new { data = results, exception = exception }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion




        #region view Models
        public class FiltertpostSalesVM
        {
            public string FromBillDate { get; set; }
            public string ToBillDate { get; set; }
            public string Workshop { get; set; }
            public string ddlmodelCategory { get; set; }
            public string model { get; set; }
            public string ddldealername { get; set; }
            public string ddlserviceoutlet { get; set; }
            public string ddllastdisposition { get; set; }
            public long getDataFor { get; set; }
            public long bucketId { get; set; }
            public bool isFiltered { get; set; }

        }
        public class postCCMSalesVM
        {

            public string psfassignedInteraction_id { get; set; }
            public string customer_id { get; set; }
            public string vehicle_id { get; set; }
            public string customer_name { get; set; }
            public string Mobile_number { get; set; }
            public string chassis { get; set; }
            public string vehicleReg { get; set; }
            public string modelcat { get; set; }
            public string workshopname { get; set; }
            public string campaignName { get; set; }

            public string complaintDate { get; set; }
            public string model { get; set; }
            public string saledate { get; set; }

            public string dealershipName { get; set; }
            public string dlryoutlet { get; set; }
            public string dlryinvdate { get; set; }

        }

        public class postsalesBucketDAtaVM
        {
            //public string customer_name { get; set; }
            //public string vehicle_RegNo { get; set; }
            //public DateTime? BillDate { get; set; }
            //public string callinteraction_id { get; set; }
            //public string followupdate { get; set; }
            ////public string followUpTime { get; set; }
            //public string Last_disposition_Conflict { get; set; }
            //public string scheduled_date { get; set; }
            //public string scheduled_time { get; set; }
            //public string reason { get; set; }
            //public string vehicle_id { get; set; }
            //public string customer_id { get; set; }
            //public string duedate { get; set; }
            //public string campaignName { get; set; }
            //public string campaign { get; set; }
            //public DateTime? calldate { get; set; }

            //public DateTime? call_date { get; set; }
            //public string RONumber { get; set; }
            //public DateTime? ROdate { get; set; }
            //public DateTime? BillDate_Conflict { get; set; }
            //public string model { get; set; }
            //public string category { get; set; }
            ////public string followUpDate { get; set; }
            //public string psfAppointmentDate { get; set; }
            //public string psfAppointmentTime { get; set; }
            //public string veh_model { get; set; }
            //public string attempts { get; set; }
            //public string workshop { get; set; }
            //public DateTime? lastServiceDate { get; set; }
            //public string lastServiceType { get; set; }
            //public string saname { get; set; }
            //public string crename { get; set; }
            //public string followuptime { get; set; }
            //public string duetype { get; set; }
            //public string assigned_intercation_id { get; set; }
            //public string chassisno { get; set; }
            //public string feedbackrating { get; set; }
            //public string resolvedBy { get; set; }
            //public DateTime? resolvedDate { get; set; }
            //public string Compliant_Category_Conflict { get; set; }
            //public string Compliant_Category { get; set; }
            //public DateTime? issuedate { get; set; }
            //public int ageing { get; set; }
            //public string resolutionMode { get; set; }
            ////public DateTime? psffollowupdate { get; set; }
            ////public string psffollowuptime { get; set; }
            //public string calldispositiondata_id { get; set; }
            //public DateTime? aptdate { get; set; }
            //public string reworkMode { get; set; }
            //public string apttime { get; set; }
            //public string complaint_creid { get; set; }
            //public string reworkStatus_id { get; set; }
            //public string aptstatus { get; set; }
            //public string DissatStatus_id_Conflict { get; set; }
            //public string last_serviceDate { get; set; }
            //public string tagging { get; set; }
            //public DateTime? Servey_date { get; set; }
            //public string Last_disposition { get; set; }
            //public string Mobile_number { get; set; }
            //public string chassiNo { get; set; }
            //public string serviceBookedType { get; set; }
            //public string serviceadvisor { get; set; }
            //public string typeOfPickup { get; set; }
            //public string booking_status { get; set; }
            ////public string lastServiceType { get; set; }
            ////public string lastServiceDate { get; set; }
            //public string servicetype { get; set; }
            //public string customercategory { get; set; }
            //public string modelcat { get; set; }

            //Fresh Call
            public string customer_id { get; set; }
            public string customer_name { get; set; }
            public string Mobile_number { get; set; }
            public string vehicle_RegNo { get; set; }
            public string model { get; set; }
            public string workshop { get; set; }
            public string vehicle_id { get; set; }
            public string category { get; set; }
            public string assigned_intercation_id { get; set; }
            public string chassisno { get; set; }
            public string modelcat { get; set; }
            public string dse { get; set; }

            //Follow Up
            public string campaign_id { get; set; }
            public string salesdate { get; set; }
            public string psfdate { get; set; }
            public string calldate { get; set; }
            public string followUpDate { get; set; }
            public string followUpTime { get; set; }
            public string attempts { get; set; }

            //Resolved

            public string creName { get; set; }
            public string cicallinteraction_id { get; set; }
            public string psfCallingDayType { get; set; }
            public string saleDate { get; set; }
            public string customerName { get; set; }
            public string vehicleRegNo { get; set; }
            public string prefferedPhoneNumber { get; set; }
            public string wyzUser_id { get; set; }
            public string workshop_id { get; set; }
            public string complaint_crename { get; set; }

            //Non contact
            public string Last_disposition { get; set; }

            // Dissatisfied
            public string callinteraction_id { get; set; }
            public string dealershipName { get; set; }
            public string dlryoutlet { get; set; }
            public string dlryinvdate { get; set; }
            public string SecondaryDisposition { get; set; }


        }

        #endregion
    }
}