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
    public class PSFController : Controller
    {
        [ActionName("PSFDetails"), HttpGet]
        public ActionResult callLogAjax(int? id)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    //id represents days
                    if (id == 3)//"psf3rdd ay"
                    {
                        Session["psfDayType"] = 7;
                    }
                    else if (id == 10|| id==9)
                    {
                        Session["psfDayType"] = 5;
                    }
                    else if (id == 6)
                    {
                        Session["psfDayType"] = 4;
                    }
                    else if (id == 30)
                    {
                        Session["psfDayType"] = 6;
                    }
                    else if(id==2)
                    {
                        Session["psfDayType"] = 10;
                    }
                    else if(id==15)
                    {
                        Session["psfDayType"] = 5;
                    }
                   
                    if (Session["DealerCode"].ToString() == "KATARIA")
                    {
                        string userName = Session["UserName"].ToString();
                        var userId = db.wyzusers.Where(m => m.userName == userName).Select(m => m.id).FirstOrDefault();
                        List<long> workshopIds = db.userworkshops.Where(m => m.userWorkshop_id == userId).Select(m => m.workshopList_id).ToList();
                        var ddlWorkshop = db.workshops.Where(m => workshopIds.Contains(m.id)  && m.isinsurance == false).Select(model => new { workshopId = model.id, workshopName = model.workshopName }).OrderBy(m => m.workshopName).ToList();
                        ViewBag.ddlWorkshop = ddlWorkshop;
                    }
                    else
                    {
                        var ddlWorkshop = db.workshops.Where(m => m.isinsurance == false).Select(model => new { workshopId = model.id, workshopName = model.workshopName }).OrderBy(m => m.workshopName).ToList();
                        ViewBag.ddlWorkshop = ddlWorkshop;
                    }
                        

                    
                    var ddlserviceType = db.servicetypes.Where(m => m.isActive).Select(model => new { serviceTypeId = model.id, serviceTypeName = model.serviceTypeName }).OrderBy(m => m.serviceTypeName).ToList();
                    //if (Session["DealerCode"].ToString() == "KATARIA")
                    //{
                    //    var ddlservicetType = db.psfservicetypes.Where(m => m.isActive).Select(model => new { serviceTypeId = model.id, serviceTypeName = model.serviceTypeName }).OrderBy(m => m.serviceTypeName).ToList();
                    //    ViewBag.servicetypes = ddlservicetType;
                    //}
                    //else
                    //{
                    //    var ddlserviceType = db.servicetypes.Where(m => m.isActive).Select(model => new { serviceTypeId = model.id, serviceTypeName = model.serviceTypeName }).OrderBy(m => m.serviceTypeName).ToList();
                    //    ViewBag.servicetypes = ddlserviceType;
                    //}
                    ViewBag.servicetypes = ddlserviceType;
                   


                    List<long?> dispositiondataId = db.indusPSFInteraction.Select(m => m.callDispositionData_id).Distinct().ToList();
                    var ddllastdisposition = db.calldispositiondatas.Where(m => dispositiondataId.Contains(m.id)).Select(m => new { m.id, m.disposition }).ToList();
                    ViewBag.ddllastdisposition = ddllastdisposition;
                    //int campnId = int.Parse(Session["psfDayType"].ToString());
                    //int UserId = Convert.ToInt32(Session["UserId"].ToString());

                    //ViewBag.freshPSF = db.Database.SqlQuery<int>("call PSFCREFreshCallCount(@inwyzuser_id,@inpsfdayid);", new MySqlParameter[] { new MySqlParameter("@inwyzuser_id", UserId.ToString()), new MySqlParameter("@inpsfdayid", campnId.ToString()) }).FirstOrDefault();
                    //ViewBag.pendingFollow = db.Database.SqlQuery<int>("call PSFCREpendingFollowUpCount(@wyzUserId,@campnId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId.ToString()), new MySqlParameter("@campnId", campnId.ToString()) }).FirstOrDefault();
                    //ViewBag.nonContacts = db.Database.SqlQuery<int>("call PSFCRENonContactCount(@wyzUserId,@campnId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId.ToString()), new MySqlParameter("@campnId", campnId.ToString()) }).FirstOrDefault();
                    //ViewBag.incompleteSurvey = db.Database.SqlQuery<int>("call PSFCREincompleteCount(@wyzUserId,@campnId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId.ToString()), new MySqlParameter("@campnId", campnId.ToString()) }).FirstOrDefault();

                    //ViewBag.surveys = db.Database.SqlQuery<int>("call PSFCRESatisfiedCountToday(@wyzUserId,@campnId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId.ToString()), new MySqlParameter("@campnId", campnId.ToString()) }).FirstOrDefault();
                    //ViewBag.contacts = db.Database.SqlQuery<int>("call PSFCREContactCount(@wyzUserId,@campnId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId.ToString()), new MySqlParameter("@campnId", campnId.ToString()) }).FirstOrDefault();
                    //ViewBag.totalCalls = db.Database.SqlQuery<int>("call PSFCRETotalCallsCountToday(@wyzUserId,@campnId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId.ToString()), new MySqlParameter("@campnId", campnId.ToString()) }).FirstOrDefault();

                    //  ViewBag.date = db.Database.SqlQuery<int>("call PSFCRENonContactCount(@campnId,@wyzUserId);", new MySqlParameter[] { new MySqlParameter("@campnId", campnId), new MySqlParameter("@wyzUserId", UserId) }).FirstOrDefault();
                    //long PSFCREResolvedCount = searchRepo.getPSFDashBoardCountCRE(id, userdata.getId(), "PSFCREResolvedCount");
                    //long PSFCREDissatCountPerDay = searchRepo.getPSFDashBoardCountCRE(id, userdata.getId(), "PSFCREDisSatisfiedCountToday");
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
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
                using (var db = new AutoSherDBContext())
                {

                    int campnId = int.Parse(Session["psfDayType"].ToString());

                    freshPSF = db.Database.SqlQuery<int>("call PSFCREFreshCallCount(@inwyzuser_id,@inpsfdayid);", new MySqlParameter[] { new MySqlParameter("@inwyzuser_id", UserId.ToString()), new MySqlParameter("@inpsfdayid", campnId.ToString()) }).FirstOrDefault();
                    pendingFollow = db.Database.SqlQuery<int>("call PSFCREpendingFollowUpCount(@wyzUserId,@campnId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId.ToString()), new MySqlParameter("@campnId", campnId.ToString()) }).FirstOrDefault();
                    nonContacts = db.Database.SqlQuery<int>("call PSFCRENonContactCount(@wyzUserId,@campnId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId.ToString()), new MySqlParameter("@campnId", campnId.ToString()) }).FirstOrDefault();
                    incompleteSurvey = db.Database.SqlQuery<int>("call PSFCREDissatisfiedCountToday(@wyzUserId,@campnId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId.ToString()), new MySqlParameter("@campnId", campnId.ToString()) }).FirstOrDefault();

                    surveys = db.Database.SqlQuery<int>("call PSFCRESatisfiedCountToday(@wyzUserId,@campnId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId.ToString()), new MySqlParameter("@campnId", campnId.ToString()) }).FirstOrDefault();
                    contacts = db.Database.SqlQuery<int>("call PSFCREContactCount(@wyzUserId,@campnId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId.ToString()), new MySqlParameter("@campnId", campnId.ToString()) }).FirstOrDefault();
                    totalCalls = db.Database.SqlQuery<int>("call PSFCRETotalCallsCountToday(@wyzUserId,@campnId);", new MySqlParameter[] { new MySqlParameter("@wyzUserId", UserId.ToString()), new MySqlParameter("@campnId", campnId.ToString()) }).FirstOrDefault();

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
            //For Pagination
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string Pattern = Request["search[value]"];
            string exception = "", serviceId, cusCategory;

            List<CallLogAjaxLoad> callLogAjax = null;
            List<CallLogAjaxLoad> callLogAjaxCount = new List<CallLogAjaxLoad>();
            List<CallLogDispositionLoad> dispositionLoad = null;
            List<CallLogDispositionLoad> dispositionLoadCount = new List<CallLogDispositionLoad>();

            string FromDateBill, ToDateBill, ExFromCallDate, ExToCallDate, workshopId, ddllastdisposition;
            int UserId = Convert.ToInt32(Session["UserId"].ToString());

            long fromIndex, toIndex, dispoType;
            int totalCount = 0;
            PSFFilter filter = new PSFFilter();
            if (psfData != null)
            {
                filter = JsonConvert.DeserializeObject<PSFFilter>(psfData);
            }

            int ageing = filter.ageing;
            FromDateBill = filter.ExFromBillDate == null ? "" : Convert.ToDateTime(filter.ExFromBillDate.ToString()).ToString("yyyy-MM-dd");
            ToDateBill = filter.ExToBillDate == null ? "" : Convert.ToDateTime(filter.ExToBillDate.ToString()).ToString("yyyy-MM-dd");
            // ExFromCallDate = filter.ExFromCallDate == null ? "" : Convert.ToDateTime(filter.ExFromCallDate.ToString()).ToString("yyyy-MM-dd");
            //ExToCallDate = filter.ExToCallDate == null ? "" : Convert.ToDateTime(filter.ExToCallDate.ToString()).ToString("yyyy-MM-dd");

          

            workshopId = filter.workshop == null || filter.workshop == "" ? "0" : filter.workshop;
            ddllastdisposition = filter.ddllastdisposition == null || filter.ddllastdisposition == "" ? "0" : filter.ddllastdisposition;
            serviceId = filter.serviceType == null ? "" : filter.serviceType;
            cusCategory = filter.customerCategory == null ? "" : filter.customerCategory;

            if (Pattern != "")
            {
                filter.isFiltered = true;
            }

            using (var db = new AutoSherDBContext())
            {
                db.Database.CommandTimeout = 900;
                try
                {
                    if (filter.getDataFor == 1)//Days
                    {

                        totalCount = db.Database.SqlQuery<int>("call PSFScheduledCallsCount(@instartdate,@inenddate,@psfcampaignid,@pattern,@inwyzuser_id,@start_with,@length)",
                                new MySqlParameter[] { new MySqlParameter("@instartdate", ""),new MySqlParameter("@inenddate", ""), new MySqlParameter("@psfcampaignid", Session["psfDayType"].ToString()), new MySqlParameter("@pattern", ""),
                                new MySqlParameter("@inwyzuser_id", UserId), new MySqlParameter("@start_with", 0), new MySqlParameter("@length", 10000000) }).FirstOrDefault(); ;

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


                        callLogAjax = get3rdDayPSF(FromDateBill, ToDateBill, Session["psfDayType"].ToString(), Pattern, UserId, fromIndex, toIndex, workshopId, serviceId, cusCategory);
                        if (filter.isFiltered == true)
                        {
                            callLogAjaxCount = get3rdDayPSF(FromDateBill, ToDateBill, Session["psfDayType"].ToString(), Pattern, UserId, 0, totalCount, workshopId, serviceId, cusCategory);
                        }
                    }
                    else if (filter.getDataFor == 2)//FollowUp
                    {
                        dispoType = 4;
                        totalCount = getPSFBucketCount("", "", Session["psfDayType"].ToString(), "", UserId, dispoType, 1);

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



                        dispositionLoad = getFollowUpAndCompletedSurvey(FromDateBill, ToDateBill, Session["psfDayType"].ToString(), Pattern, UserId, dispoType, fromIndex, toIndex, workshopId, serviceId, cusCategory);
                        if (filter.isFiltered == true)
                        {
                            dispositionLoadCount = getFollowUpAndCompletedSurvey(FromDateBill, ToDateBill, Session["psfDayType"].ToString(), Pattern, UserId, dispoType, 0, totalCount, workshopId, serviceId, cusCategory);
                        }
                    }
                    else if (filter.getDataFor == 3)//Completed Survey
                    {
                        dispoType = 22;
                        totalCount = getPSFBucketCount("", "", Session["psfDayType"].ToString(), "", UserId, dispoType, 1);


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




                        dispositionLoad = getFollowUpAndCompletedSurvey(FromDateBill, ToDateBill, Session["psfDayType"].ToString(), Pattern, UserId, dispoType, fromIndex, toIndex, workshopId, serviceId, cusCategory);
                        if (filter.isFiltered == true)
                        {
                            dispositionLoadCount = getFollowUpAndCompletedSurvey(FromDateBill, ToDateBill, Session["psfDayType"].ToString(), Pattern, UserId, dispoType, 0, totalCount, workshopId, serviceId, cusCategory);
                        }

                    }
                    else if (filter.getDataFor == 4)//Dissatisfied
                    {
                        dispoType = 44;
                        totalCount = getPSFBucketCount("", "", Session["psfDayType"].ToString(), "", UserId, dispoType, 1);


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



                        dispositionLoad = getFollowUpAndCompletedSurvey(FromDateBill, ToDateBill, Session["psfDayType"].ToString(), Pattern, UserId, dispoType, fromIndex, toIndex, workshopId, serviceId, cusCategory);
                        if (filter.isFiltered == true)
                        {
                            dispositionLoadCount = getFollowUpAndCompletedSurvey(FromDateBill, ToDateBill, Session["psfDayType"].ToString(), Pattern, UserId, dispoType, 0, totalCount, workshopId, serviceId, cusCategory);
                        }
                    }
                    else if (filter.getDataFor == 5)//Non Contacts
                    {
                        dispoType = 1;
                        totalCount = getPSFBucketCount("", "", Session["psfDayType"].ToString(), "", UserId, dispoType, 2);


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



                        dispositionLoad = getnonContacts(FromDateBill, ToDateBill, Session["psfDayType"].ToString(), Pattern, UserId, dispoType, fromIndex, toIndex, workshopId, serviceId, cusCategory);
                        if (filter.isFiltered == true)
                        {
                            dispositionLoadCount = getnonContacts(FromDateBill, ToDateBill, Session["psfDayType"].ToString(), Pattern, UserId, dispoType, 0, totalCount, workshopId, serviceId, cusCategory);
                        }


                    }
                    else if (filter.getDataFor == 6)//Dropped
                    {

                        dispoType = 2;
                        totalCount = getPSFBucketCount("", "", Session["psfDayType"].ToString(), "", UserId, dispoType, 2);


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



                        dispositionLoad = getnonContacts(FromDateBill, ToDateBill, Session["psfDayType"].ToString(), Pattern, UserId, dispoType, fromIndex, toIndex, workshopId, serviceId, cusCategory);
                        if (filter.isFiltered == true)
                        {
                            dispositionLoadCount = getnonContacts(FromDateBill, ToDateBill, Session["psfDayType"].ToString(), Pattern, UserId, dispoType, 0, totalCount, workshopId, serviceId, cusCategory);
                        }

                    }
                    else if (filter.getDataFor == 7)//Resolved
                    {

                        dispoType = 9;
                        totalCount = db.Database.SqlQuery<int>("call PSFfilterResolvedSatisfiedCount(@psfcampaignid,@wyzuserid,@bucketid)",
                                new MySqlParameter[] { new MySqlParameter("@psfcampaignid", Session["psfDayType"].ToString()), new MySqlParameter("@wyzuserid", UserId), new MySqlParameter("@bucketid", dispoType) }).FirstOrDefault();


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


                        dispositionLoad = getresolved(FromDateBill, ToDateBill, Session["psfDayType"].ToString(), Pattern, UserId, ageing, dispoType, fromIndex, toIndex, workshopId, serviceId, cusCategory,ddllastdisposition);
                        if (filter.isFiltered == true)
                        {
                            dispositionLoadCount = getresolved(FromDateBill, ToDateBill, Session["psfDayType"].ToString(), Pattern, UserId, ageing, dispoType, 0, totalCount, workshopId, serviceId, cusCategory,ddllastdisposition);
                        }

                    }
                    else if (filter.getDataFor == 8)//FuTureFollowUp
                    {
                        dispoType = 0420;
                        totalCount = getPSFBucketCount("", "", Session["psfDayType"].ToString(), "", UserId, dispoType, 1);

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

                        dispositionLoad = getFollowUpAndCompletedSurvey(FromDateBill, ToDateBill, Session["psfDayType"].ToString(), Pattern, UserId, dispoType, fromIndex, toIndex, workshopId, serviceId, cusCategory);
                        if (filter.isFiltered == true)
                        {
                            dispositionLoadCount = getFollowUpAndCompletedSurvey(FromDateBill, ToDateBill, Session["psfDayType"].ToString(), Pattern, UserId, dispoType, 0, totalCount, workshopId, serviceId, cusCategory);
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


            if (callLogAjax != null)
            {
                if (filter.isFiltered == true)
                {
                    return Json(new { exception = exception, data = callLogAjax, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = callLogAjaxCount.Count() }, JsonRequestBehavior.AllowGet);
                }
                else if (filter.isFiltered == false)
                {
                    return Json(new { exception = exception, data = callLogAjax, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount }, JsonRequestBehavior.AllowGet);
                }

            }
            else if (dispositionLoad != null)
            {
                if (filter.isFiltered == true)
                {
                    return Json(new { exception = exception, data = dispositionLoad, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = dispositionLoadCount.Count() }, JsonRequestBehavior.AllowGet);
                }
                else if (filter.isFiltered == false)
                {
                    return Json(new { exception = exception, data = dispositionLoad, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount }, JsonRequestBehavior.AllowGet);
                }

            }

            return Json(new { exception = exception, data = "", draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0 }, JsonRequestBehavior.AllowGet);
        }


        //Retrieving Day Wise PFS Data 
        public List<CallLogAjaxLoad> get3rdDayPSF(string fromBillDate, string toBillDate, string typeOfPSF,
            string searchPattern, long id, long fromIndex, long toIndex, string workshopId, string serviceId, string cusCategory)
        {
            List<CallLogAjaxLoad> PSFDetails = new List<CallLogAjaxLoad>();

            //try
            //{
            using (var db = new AutoSherDBContext())
            {
                db.Database.CommandTimeout = 900;
                string str = @"CALL PSFScheduledCalls(@fromBillDate,@toBillDate, @typeOfPSF,@searchPattern,@id,@fromIndex,@toIndex,@workshopId,@servicetype,@customercategory);";

                MySqlParameter[] sqlParameter = new MySqlParameter[]
                {
                        new MySqlParameter("fromBillDate", fromBillDate),
                        new MySqlParameter("toBillDate", toBillDate),
                        new MySqlParameter("typeOfPSF", typeOfPSF),
                        new MySqlParameter("searchPattern", searchPattern),
                        new MySqlParameter("id", id),
                        new MySqlParameter("fromIndex", fromIndex),
                        new MySqlParameter("toIndex", toIndex),
                        new MySqlParameter("workshopId", workshopId),
                          new MySqlParameter("servicetype", serviceId),
                        new MySqlParameter("customercategory", cusCategory)

                };
                PSFDetails = db.Database.SqlQuery<CallLogAjaxLoad>(str, sqlParameter).ToList();
            }

            return PSFDetails;
            //}
            //catch (Exception ex)
            //{

            //}

            //return PSFDetails;
        }

        public List<CallLogDispositionLoad> getFollowUpAndCompletedSurvey(string fromBillDate,
            string toBillDate, string typeOfPSF, string searchPattern, long id, long dispoType, long fromIndex,
            long toIndex, string workshopId, string serviceId, string cusCategory)
        {
            List<CallLogDispositionLoad> followUpRequired = new List<CallLogDispositionLoad>();

            //try
            //{
            using (var db = new AutoSherDBContext())
            {
                string str = @"CALL PSFfileterForContacts(@fromBillDate,@toBillDate,@typeOfPSF,@searchPattern,@id,@dispoType,@fromIndex,@toIndex,@workshopId,@servicetype,@customercategory);";

                MySqlParameter[] sqlParameter = new MySqlParameter[]
                {
                        new MySqlParameter("fromBillDate", fromBillDate),
                        new MySqlParameter("toBillDate", toBillDate),
                        new MySqlParameter("typeOfPSF", typeOfPSF),
                        new MySqlParameter("searchPattern", searchPattern),
                        new MySqlParameter("id", id),
                        new MySqlParameter("dispoType", dispoType),
                        new MySqlParameter("fromIndex", fromIndex),
                        new MySqlParameter("toIndex", toIndex),
                        new MySqlParameter("workshopId", workshopId),
                        new MySqlParameter("servicetype", serviceId),
                        new MySqlParameter("customercategory", cusCategory)

                };
                followUpRequired = db.Database.SqlQuery<CallLogDispositionLoad>(str, sqlParameter).ToList();
            }

            return followUpRequired;
            //}
            //catch (Exception ex)
            //{

            //}
            //return followUpRequired;
        }

        public List<CallLogDispositionLoad> getnonContacts(string fromBillDate, string toBillDate, string typeOfPSF,
          string searchPattern, long id, long dispoType, long fromIndex, long toIndex, string workshopId, string serviceId, string cusCategory)
        {
            List<CallLogDispositionLoad> nonContactsList = new List<CallLogDispositionLoad>();

            //try
            //{
            using (var db = new AutoSherDBContext())
            {
                string str = @"CALL PSFfileterForNonContacts(@fromBillDate,@toBillDate, @typeOfPSF,@searchPattern,@id,@dispoTypeid,@fromIndex,@toIndex,@workshopId,@servicetype,@customercategory);";

                MySqlParameter[] sqlParameter = new MySqlParameter[]
                {
                        new MySqlParameter("fromBillDate", fromBillDate),
                        new MySqlParameter("toBillDate", toBillDate),
                        new MySqlParameter("typeOfPSF", typeOfPSF),
                        new MySqlParameter("searchPattern", searchPattern),
                        new MySqlParameter("id", id),
                        new MySqlParameter("dispoTypeid", dispoType),
                        new MySqlParameter("fromIndex", fromIndex),
                        new MySqlParameter("toIndex", toIndex),
                        new MySqlParameter("workshopId", workshopId),
                          new MySqlParameter("servicetype", serviceId),
                        new MySqlParameter("customercategory", cusCategory)

                };
                nonContactsList = db.Database.SqlQuery<CallLogDispositionLoad>(str, sqlParameter).ToList();
            }

            return nonContactsList;
            //}
            //catch (Exception ex)
            //{

            //}

            //return nonContactsList;
        }
        public List<CallLogDispositionLoad> getresolved(string fromBillDate, string toBillDate, string typeOfPSF,
         string searchPattern, long id, int ageng, long dispoType, long fromIndex, long toIndex, string workshopId, string serviceId, string cusCategory,string insecondarydisposition)
        {
            List<CallLogDispositionLoad> resolvedList = new List<CallLogDispositionLoad>();
            if(workshopId=="0")
            {
                workshopId = "";
            }
            if(Session["DealerCode"].ToString() == "BRIDGEWAYMOTORS")
            {
                if (workshopId == "")
                {
                    workshopId = "0";
                }
            }
            //try
            //{
            using (var db = new AutoSherDBContext())
            {
                string str = @"CALL PSFfilterResolvedSatisfied(@instartdate,@inenddate, @psfcampaignid,@pattern,@wyzuserid,@inAge,@bucketid,@start_with,@length,@inworkshopid,@servicetype,@customercategory,@insecondarydisposition);";

                MySqlParameter[] sqlParameter = new MySqlParameter[]
                {
                        new MySqlParameter("instartdate", fromBillDate),
                        new MySqlParameter("inenddate", toBillDate),
                        new MySqlParameter("psfcampaignid", typeOfPSF),
                        new MySqlParameter("pattern", searchPattern),
                        new MySqlParameter("wyzuserid", id),
                        new MySqlParameter("inAge", ageng),
                        new MySqlParameter("bucketid", dispoType),
                        new MySqlParameter("start_with", fromIndex),
                        new MySqlParameter("length", toIndex),
                        new MySqlParameter("inworkshopid", workshopId),
                          new MySqlParameter("servicetype", serviceId),
                        new MySqlParameter("customercategory", cusCategory),
                        new MySqlParameter("insecondarydisposition", insecondarydisposition)

                };
                resolvedList = db.Database.SqlQuery<CallLogDispositionLoad>(str, sqlParameter).ToList();
            }

            return resolvedList;
            //}
            //catch (Exception ex)
            //{

            //}

            //return nonContactsList;
        }


        public int getPSFBucketCount(string instartdate, string inenddate, string psfcampaignid, string pattern, long UserId, long dispoType, int procedure)
        {
            using (var db = new AutoSherDBContext())
            {
                if (procedure == 1)
                {
                    return db.Database.SqlQuery<int>("call PSFfileterForContactsCount(@instartdate,@inenddate,@psfcampaignid,@pattern,@inwyzuser_id,@dispositiontype)",
                                new MySqlParameter[] { new MySqlParameter("@instartdate", ""),new MySqlParameter("@inenddate", ""), new MySqlParameter("@psfcampaignid", Session["psfDayType"].ToString()), new MySqlParameter("@pattern", ""),
                                new MySqlParameter("@inwyzuser_id", UserId), new MySqlParameter("@dispositiontype", dispoType) }).FirstOrDefault();
                }
                else if (procedure == 2)
                {
                    return db.Database.SqlQuery<int>("call PSFfileterForNonContactsCount(@instartdate,@inenddate,@psfcampaignid,@pattern,@inwyzuser_id,@dispositiontype)",
                                new MySqlParameter[] { new MySqlParameter("@instartdate", ""),new MySqlParameter("@inenddate", ""), new MySqlParameter("@psfcampaignid", Session["psfDayType"].ToString()), new MySqlParameter("@pattern", ""),
                                new MySqlParameter("@inwyzuser_id", UserId), new MySqlParameter("@dispositiontype", dispoType) }).FirstOrDefault();
                }
            }

            return 0;

        }

        #region PSF Complaint Region Indus
        public ActionResult psfComplaintDetails()
        {
            int UserId = Convert.ToInt32(Session["UserId"].ToString());
            try
            {
                using (AutoSherDBContext dBContext = new AutoSherDBContext())
                {

                    ViewBag.workshop = dBContext.workshops.Select(m => new { id = m.id, name = m.workshopName }).OrderBy(m => m.name).ToList();
                    var ddlserviceType = dBContext.servicetypes.Where(m => m.isActive).Select(model => new { serviceTypeId = model.serviceId, serviceTypeName = model.serviceTypeName }).OrderBy(m => m.serviceTypeName).ToList();
                    ViewBag.servicetypes = ddlserviceType;
                    var ddlcampaigntypeType = dBContext.campaigns.Where(m => m.campaignType=="PSF").Select(model => new { id = model.id, campaigmName = model.campaignName }).OrderBy(m => m.campaigmName).ToList();
                    ViewBag.campaigntypeType = ddlcampaigntypeType;

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

                    freshPSF = dBContext.Database.SqlQuery<int>("call complaintCRECalllogsDashboard(@bucket_id,@inwyzuser_id);", new MySqlParameter[] { new MySqlParameter("@bucket_id", 1), new MySqlParameter("@inwyzuser_id", UserId) }).FirstOrDefault();
                    pendingFollow = dBContext.Database.SqlQuery<int>("call complaintCRECalllogsDashboard(@bucket_id,@inwyzuser_id);", new MySqlParameter[] { new MySqlParameter("@bucket_id", 2), new MySqlParameter("@inwyzuser_id", UserId) }).FirstOrDefault();
                    nonContacts = dBContext.Database.SqlQuery<int>("call complaintCRECalllogsDashboard(@bucket_id,@inwyzuser_id);", new MySqlParameter[] { new MySqlParameter("@bucket_id", 3), new MySqlParameter("@inwyzuser_id", UserId) }).FirstOrDefault();
                    Rework = dBContext.Database.SqlQuery<int>("call complaintCRECalllogsDashboard(@bucket_id,@inwyzuser_id);", new MySqlParameter[] { new MySqlParameter("@bucket_id", 4), new MySqlParameter("@inwyzuser_id", UserId) }).FirstOrDefault();
                    surveys = dBContext.Database.SqlQuery<int>("call complaintCRECalllogsDashboard(@bucket_id,@inwyzuser_id);", new MySqlParameter[] { new MySqlParameter("@bucket_id", 5), new MySqlParameter("@inwyzuser_id", UserId) }).FirstOrDefault();
                    contacts = dBContext.Database.SqlQuery<int>("call complaintCRECalllogsDashboard(@bucket_id,@inwyzuser_id);", new MySqlParameter[] { new MySqlParameter("@bucket_id", 6), new MySqlParameter("@inwyzuser_id", UserId) }).FirstOrDefault();
                    totalCalls = dBContext.Database.SqlQuery<int>("call complaintCRECalllogsDashboard(@bucket_id,@inwyzuser_id);", new MySqlParameter[] { new MySqlParameter("@bucket_id", 7), new MySqlParameter("@inwyzuser_id", UserId) }).FirstOrDefault();

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


        public ActionResult getDataforBuckets(string psfValues)
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string Pattern = Request["search[value]"];
            int userID = Convert.ToInt32(Session["UserId"].ToString());
            List<PSFIndusVM> psfVM = null;
            List<PSFIndusVM> psfCount = null;
            string fromBillDate_var, toBillDate_var, reworkMode_var, workshop_var, exception = "", serviceId_var, cusCategory_var;
            int ageing_var, bucketId_var, attempts_var;
            int fromIndex, toIndex;
            int totalCount = 0,campaignID;
            PSFFilterVM filter = new PSFFilterVM();
            if (psfValues != null)
            {
                filter = JsonConvert.DeserializeObject<PSFFilterVM>(psfValues);
            }
            if (Pattern != "")
            {
                filter.isFiltered = true;
            }
            fromIndex = start;
            toIndex = length;
            if (toIndex < 0)
            {
                toIndex = 10;
            }

            using (AutoSherDBContext dBContext = new AutoSherDBContext())
            {
                try
                {
                    bucketId_var = Convert.ToInt32(filter.bucketId);
                    fromBillDate_var = filter.ExFromBillDate == null ? "" : Convert.ToDateTime(filter.ExFromBillDate.ToString()).ToString("yyyy-MM-dd");
                    toBillDate_var = filter.ExToBillDate == null ? "" : Convert.ToDateTime(filter.ExToBillDate.ToString()).ToString("yyyy-MM-dd");
                    ageing_var = filter.Ageing == null ? 0 : Convert.ToInt32(filter.Ageing);
                    attempts_var = filter.Attempts == null ? 0 : Int32.Parse(filter.Attempts);
                    reworkMode_var = filter.ReworkMode == null || filter.ReworkMode == "0" ? "" : filter.ReworkMode;
                    workshop_var = filter.Workshop == null || filter.Workshop == "" ? "0" : filter.Workshop;
                    serviceId_var = filter.serviceType == null ? "" : filter.serviceType;
                    cusCategory_var = filter.customerCategory == null ? "" : filter.customerCategory;
                    campaignID = ((filter.campaigntypeTypeDDL == null || filter.campaigntypeTypeDDL=="")) ? 0 : Convert.ToInt32(filter.campaigntypeTypeDDL);


                    totalCount = getPSFCount(bucketId_var, userID, ageing_var, fromBillDate_var, toBillDate_var,
                                     attempts_var, reworkMode_var, campaignID);
                    if (toIndex > totalCount)
                    {
                        toIndex = totalCount;
                    }

                    psfVM = getPSFComplaints(bucketId_var, userID, ageing_var, fromBillDate_var, toBillDate_var,
                                     attempts_var, reworkMode_var, Pattern, fromIndex, toIndex, workshop_var, serviceId_var, cusCategory_var, campaignID);
                    if (filter.isFiltered == true)
                    {
                        psfCount = getPSFComplaints(bucketId_var, userID, ageing_var, fromBillDate_var, toBillDate_var,
                                     attempts_var, reworkMode_var, Pattern, 0, totalCount, workshop_var, serviceId_var, cusCategory_var, campaignID);
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

                if (psfVM != null)
                {
                    if (filter.isFiltered == true)
                    {
                        return Json(new { exception = exception, data = psfVM, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = psfCount.Count() }, JsonRequestBehavior.AllowGet);
                    }
                    else if (filter.isFiltered == false)
                    {
                        return Json(new { exception = exception, data = psfVM, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount }, JsonRequestBehavior.AllowGet);
                    }

                }
            }


            return Json(new { exception = exception, data = "", draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0 }, JsonRequestBehavior.AllowGet);
        }
        public List<PSFIndusVM> getPSFComplaints(int bucketId_var, int userID, int ageing_var, string fromBillDate_var, string toBillDate_var,
                                                int attempts_var, string reworkMode_var, string Pattern, int start, int length, string workshop_var, string serviceId_var, string cusCategory_var,int campaignID)
        {
            List<PSFIndusVM> psfDetails = new List<PSFIndusVM>();
            //try
            //{
            using (AutoSherDBContext dBContext = new AutoSherDBContext())
            {
                string str = @"call complaintCRECalllogs(@bucket_id,@inwyzuser_id,@inaging,@instartdt,@inenddt,@inattempt
,@inreworkMode,@pattern,@startwith,@length,@inworkshopid,@servicetype,@customercategory,@incampaignid)";
                MySqlParameter[] param = new MySqlParameter[]
                {
                        new MySqlParameter("bucket_id",bucketId_var),
                        new MySqlParameter("inwyzuser_id",userID),
                        new MySqlParameter("inaging",ageing_var),
                        new MySqlParameter("instartdt",fromBillDate_var),
                        new MySqlParameter("inenddt",toBillDate_var),
                        new MySqlParameter("inattempt",attempts_var),
                        new MySqlParameter("inreworkMode",reworkMode_var),
                        new MySqlParameter("pattern",Pattern),
                        new MySqlParameter("startwith",start),
                        new MySqlParameter("length",length),
                        new MySqlParameter("inworkshopid",workshop_var),
                        new MySqlParameter("servicetype", serviceId_var),
                        new MySqlParameter("customercategory", cusCategory_var),
                        new MySqlParameter("incampaignid", campaignID)
                };
                psfDetails = dBContext.Database.SqlQuery<PSFIndusVM>(str, param).ToList();
            }
            //}
            //catch (Exception ex)
            //{

            //}
            return psfDetails;
        }
        public int getPSFCount(long bucketId_var, int userID, int ageing_var, string fromBillDate_var, string toBillDate_var,
                                                int attempts_var, string reworkMode_var,int campaignID)
        {
            int psfCount = 0;
            //try
            //{
            using (AutoSherDBContext dBContext = new AutoSherDBContext())
            {
                var str = @"call complaintCRECalllogsCount(@bucket_id,@inwyzuser_id,@inaging,@instartdt,@inenddt,@inattempt,@inreworkMode,@incampaignid)";
                MySqlParameter[] param = new MySqlParameter[]
                 {
                        new MySqlParameter("bucket_id",bucketId_var),
                        new MySqlParameter("inwyzuser_id",userID),
                        new MySqlParameter("inaging",ageing_var),
                        new MySqlParameter("instartdt",fromBillDate_var),
                        new MySqlParameter("inenddt",toBillDate_var),
                        new MySqlParameter("inattempt",attempts_var),
                        new MySqlParameter("inreworkMode",reworkMode_var),
                        new MySqlParameter("incampaignid", campaignID)
                 };
                psfCount = dBContext.Database.SqlQuery<int>(str, param).FirstOrDefault();
            }
            //}
            //catch (Exception ex)
            //{

            //}
            return psfCount;
        }

        #endregion

        #region PSFRM Indus
        public ActionResult PSFRM()
        {
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    ViewBag.workshops = db.workshops.Where(x => x.isinsurance == false).Select(x => new { id = x.id, name = x.workshopName }).ToList();
                    //long complaintManagerId = db.roles.FirstOrDefault(x => x.role1 == "Complaint Manager").id;
                    //    List<long> userIdList = db.userroles.Where(x => x.roles_id == complaintManagerId).Select(x => x.users_id).ToList();
                    //    ViewBag.creComplaintList = db.wyzusers.Where(x => userIdList.Contains(x.id)).Select(x => new { id = x.id, name = x.userName }).ToList();


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
                TempData["ControllerName"] = "PSFFRM";

                return RedirectToAction("LogOff", "Home");
            }
            return View();
        }
        public ActionResult PSFRMDataTable(string values)
        {
            List<PSFRMIndusVM> psf = new List<PSFRMIndusVM>();
            PSFRMfilters getData = new PSFRMfilters();
            string exception = "";
            int totalCount = 0;
            int psfCount = 0;


            if (values != null)
            {
                getData = JsonConvert.DeserializeObject<PSFRMfilters>(values);
            }


            try
            {
                using (AutoSherDBContext dBContext = new AutoSherDBContext())
                {

                    int UserId = Convert.ToInt32(Session["UserId"].ToString());
                    string fromDate_var, toDate_var, workshop_var, creId_var;
                    int lastDispo_var;
                    long fromIndex, toIndex;
                    int start = Convert.ToInt32(Request["start"]);
                    int length = Convert.ToInt32(Request["length"]);
                    string searchPattern = Request["search[value]"];

                    if (searchPattern != "")
                    {
                        getData.isFiltered = true;
                    }

                    //List<long> listOfCreManId = dBContext.AccessLevels.Where(x => x.rmId == UserId).Select(x => x.creManagerId).Distinct().ToList();
                    //List<string> listOfCreManName = dBContext.wyzusers.Where(x => listOfCreManId.Contains(x.id)).Select(x => x.userName).ToList();
                    //var incremanager = string.Join(",", listOfCreManName);
                    lastDispo_var = getData.lastDisposition == 0 ? 0 : getData.lastDisposition;
                    fromDate_var = getData.fromIssueDate == null ? "" : Convert.ToDateTime(getData.fromIssueDate.ToString()).ToString("yyyy-MM-dd");
                    toDate_var = getData.toIssueDate == null ? "" : Convert.ToDateTime(getData.toIssueDate.ToString()).ToString("yyyy-MM-dd");
                    workshop_var = getData.workshop == null ? "" : getData.workshop;
                    //creId_var = getData.complaintCreId == null ? "" : getData.complaintCreId;

                    fromIndex = start;
                    toIndex = length;

                    //var str1 = @"call complaintRMCalllogsCount(@bucket_id,@incremanager,@inwyzuser_id,@instartdt,@inenddt,@pattern,@inworkshopid);";
                    //MySqlParameter[] param1 = new MySqlParameter[]
                    //{
                    //    new MySqlParameter("bucket_id",lastDispo_var),
                    //    new MySqlParameter("incremanager",incremanager),
                    //    new MySqlParameter("inwyzuser_id",creId_var),
                    //    new MySqlParameter("instartdt",fromDate_var),
                    //    new MySqlParameter("inenddt",toDate_var),
                    //    new MySqlParameter("pattern",searchPattern),
                    //    new MySqlParameter("inworkshopid",workshop_var)
                    //};
                    //totalCount = dBContext.Database.SqlQuery<int>(str1, param1).FirstOrDefault();

                    if (toIndex < 0)
                    {
                        toIndex = 10;
                    }
                    if (toIndex > totalCount)
                    {
                        toIndex = 1000;
                        //psfCount = 1000;
                    }

                    //var str = @"call complaintRMCalllogs(@bucket_id,@incremanager,@inwyzuser_id,@instartdt,@inenddt,@pattern,@startwith,@length,@inworkshopid);";
                    //MySqlParameter[] param = new MySqlParameter[]
                    //{
                    //    new MySqlParameter("bucket_id",lastDispo_var),
                    //    new MySqlParameter("incremanager",incremanager),
                    //    new MySqlParameter("inwyzuser_id",creId_var),
                    //    new MySqlParameter("instartdt",fromDate_var),
                    //    new MySqlParameter("inenddt",toDate_var),
                    //    new MySqlParameter("pattern",searchPattern),
                    //    new MySqlParameter("startwith",0),
                    //    new MySqlParameter("length",totalCount),
                    //    new MySqlParameter("inworkshopid",workshop_var)
                    //};
                    //psf = dBContext.Database.SqlQuery<PSFRMIndusVM>(str, param).ToList();
                    //if (getData.isFiltered == true)
                    //{
                    //    psfCount = psf.Count();
                    //}

                    var str = @"call rm_calllog(@inrmid,@fromissuedate,@toissuedate,@inlastdispo,@inworkshopId,@pattern);";
                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("inrmid",UserId),
                        new MySqlParameter("fromissuedate",fromDate_var),
                        new MySqlParameter("toissuedate",toDate_var),
                        new MySqlParameter("inlastdispo",lastDispo_var),
                        new MySqlParameter("inworkshopId",workshop_var),
                        new MySqlParameter("pattern",searchPattern)
                    };
                    psf = dBContext.Database.SqlQuery<PSFRMIndusVM>(str, param).ToList();
                    psfCount = psf.Count();

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


            if (getData.isFiltered == true)
            {
                return Json(new { data = psf, draw = Request["draw"], recordsTotal = psfCount, recordsFiltered = psfCount, exception= exception }, JsonRequestBehavior.AllowGet);
            }
            else if (getData.isFiltered == false)
            {
                return Json(new { data = psf, draw = Request["draw"], recordsTotal = psfCount, recordsFiltered = psfCount, exception = exception }, JsonRequestBehavior.AllowGet);
                //helpful when paging is done
            }
            else
            {
                return Json(new { data = "", draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = psfCount, exception = exception }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

    }
}