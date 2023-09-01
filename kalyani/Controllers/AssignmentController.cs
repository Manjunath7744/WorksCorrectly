using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using AutoSherpa_project.Models;
using AutoSherpa_project.Models.ViewModels;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace AutoSherpa_project.Controllers
{
    [AuthorizeFilter]
    public class AssignmentController : Controller
    {
        [ActionName("SMRPSFAssignment")]
        public ActionResult SMRAssignment(long id)
        {
            AssignmentViewModal assign = new AssignmentViewModal();

            //assign.GetType().GetProperty().
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    List<campaign> campaign = new List<campaign>();
                    if (Session["LoginUser"].ToString() == "Service")
                    {
                        campaign = db.campaigns.Where(m => m.campaignType == "Campaign" || m.campaignType == "PSF").ToList();
                    }
                    else
                    {
                        campaign = db.campaigns.Where(m => m.campaignType == "Insurance").OrderBy(m => m.campaignType).ToList();
                    }

                    List<campaign> campAllList = new List<campaign>();



                    foreach (var list in campaign)
                    {
                        if (!campAllList.Any(m => m.campaignType == list.campaignType))
                        {
                            campAllList.Add(list);
                        }
                    }

                    campAllList = campAllList.OrderBy(m => m.campaignType).ToList();

                    string creManeger = Session["UserName"].ToString();

                    var creList = db.wyzusers.Where(m => m.role == "CRE" && m.unAvailable==false && m.creManager == creManeger).Select(m => new { usernameWithFirstname = m.firstName + "-" + m.userName, creName = m.userName }).OrderBy(m => m.creName).ToList();
                    var campTypeList = db.campaigns.Where(m => m.campaignType == "Campaign" && m.isactive == true).Select(m => new { id = m.id, campaignName = m.campaignName }).ToList();
                    var campListPSF = db.campaigns.Where(m => m.campaignType == "PSF" && m.isactive == true).Select(m => new { id = m.id, campaignName = m.campaignName }).ToList();
                    var locations = db.locations.Select(m => new { id = m.cityId, name = m.name }).OrderBy(m => m.name).ToList();
                    var serviceType = db.servicetypes.Where(m=>m.isActive).ToList();

                    ViewBag.creList = creList;
                    ViewBag.campAllList = campAllList;
                    ViewBag.campTypeList = campTypeList;
                    ViewBag.campListPSF = campListPSF;
                    ViewBag.locations = locations;
                    ViewBag.serviceType = serviceType;

                    if (id > 0)
                    {
                        assign.changeAssignDetails = db.Database.SqlQuery<ChangeAssignment>("call uploadAssignedDetails(@inuploadid);", new MySqlParameter("@inuploadid", id)).ToList();

                    }
                    assign.uploadId = id;
                }
            }
            catch (Exception ex)
            {

            }
            return View(assign);
        }

        public ActionResult getUploadDetails(long uploadId)
        {
            List<ChangeAssignment> changeUploadDetails = new List<ChangeAssignment>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    changeUploadDetails = db.Database.SqlQuery<ChangeAssignment>("call uploadAssignedStatusDetails(@inuploadid);", new MySqlParameter("@inuploadid", uploadId)).ToList();

                }

                return Json(new { success = true, data = changeUploadDetails });
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = false });
        }

        public ActionResult getUploadedAssignedCallDetailsCrewise(long uploadId)
        {
            List<ChangeAssignment> changeUploadDetails = new List<ChangeAssignment>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    changeUploadDetails = db.Database.SqlQuery<ChangeAssignment>("call uploadAssignedCallDetailsCrewise(@inuploadid);", new MySqlParameter("@inuploadid", uploadId)).ToList();
                }

                return Json(new { success = true, data = changeUploadDetails });
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = false });
        }

        public ActionResult getAssignList(string filterData)
        {
            int fromIndex = Convert.ToInt32(Request["start"]);
            int toIndex = Convert.ToInt32(Request["length"]);
            string searchPattern = Request["search[value]"];
            int totalCount = 0;
            List<AssignedListForCRE> assignList = new List<AssignedListForCRE>();
            SMRAssignmentFilter filter = new SMRAssignmentFilter();
            DateTime fromDate, toDate;
            string customerCat, modelname, cityName, cityId, workshopname, serviceCat = "All";
            long campaignTypeCat, campaignNameCat, campaignNamePSF = 0;
            string selectedCampaignType = "";
            if (filterData != null)
            {
                filter = JsonConvert.DeserializeObject<SMRAssignmentFilter>(filterData);
            }
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    campaignTypeCat = long.Parse(filter.AllCampaign);
                    if (filter.PSFCampaign != "")
                    {
                        campaignNamePSF = long.Parse(filter.PSFCampaign);
                    }

                    cityName = filter.location;
                    cityId = db.locations.FirstOrDefault(m => m.name == cityName).cityId.ToString();
                    selectedCampaignType = db.campaigns.FirstOrDefault(m => m.id == campaignTypeCat).campaignType;
                    fromDate = Convert.ToDateTime(Convert.ToDateTime(filter.from_date).ToString("yyyy-MM-dd"));
                    toDate = Convert.ToDateTime(Convert.ToDateTime(filter.to_date).ToString("yyyy-MM-dd"));
                    workshopname = filter.workshop == null ? "" : filter.workshop;
                    campaignNameCat = filter.CampaignName == null ? 0 : long.Parse(filter.CampaignName);
                    modelname = filter.vehicle_model == null ? "" : filter.vehicle_model;
                    if (selectedCampaignType == "PSF")
                    {
                        assignList = getAssignListAll(fromDate, toDate, modelname, serviceCat, campaignTypeCat, campaignNameCat, campaignNamePSF, modelname, fromIndex, toIndex, cityId, workshopname, 1);
                        totalCount = getAssignListCount(fromDate, toDate, modelname, serviceCat, campaignNamePSF, modelname, cityId, workshopname, 1);
                    }
                    else
                    {
                        if (selectedCampaignType != "Campaign")
                        {
                            campaignNameCat = 0;
                        }
                        assignList = getAssignListAll(fromDate, toDate, modelname, serviceCat, campaignTypeCat, campaignNameCat, campaignNamePSF, modelname, fromIndex, toIndex, cityId, workshopname, 2);
                        totalCount = getAssignListCount(fromDate, toDate, modelname, serviceCat, campaignNameCat, modelname, cityId, workshopname, 2);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { data = assignList, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount }, JsonRequestBehavior.AllowGet);
        }

        public List<AssignedListForCRE> getAssignListAll(DateTime fromDateNew, DateTime toDateNew,
            string customerCat, string serviceCat, long campaignTypeCat, long campaignNameCat, long campaignNamePSF,
            string modelname, long fromIndex, long toIndex, string cityId, string workshopname, int getListFor)
        {
            //getListfor --------->1=PSF,2=Campagin & Others

            List<AssignedListForCRE> allAssignment = new List<AssignedListForCRE>();
            try
            {
                if (customerCat == "All")
                {

                    customerCat = "";
                }
                if (serviceCat == "All")
                {

                    serviceCat = "";
                }

                using (var db = new AutoSherDBContext())
                {
                    if (getListFor == 1)//psf;
                    {
                        string str = @"CALL psfassignlist(@campaignNamePSF,@fromDateNew,@toDateNew,@customer_cat, @service_cat,@inmodel,@inlocation, @inworkshop_id, @startwith,@length)";

                        MySqlParameter[] parameter = new MySqlParameter[] {
                        new MySqlParameter("campaignNamePSF", campaignNamePSF),
                        new MySqlParameter("fromDateNew", fromDateNew),
                        new MySqlParameter("toDateNew", toDateNew),
                        new MySqlParameter("customer_cat", customerCat),
                        new MySqlParameter("service_cat", serviceCat),
                        new MySqlParameter("inmodel", modelname),
                        new MySqlParameter("inlocation", cityId),
                        new MySqlParameter("inworkshop_id", workshopname),
                        new MySqlParameter("startwith", (int)fromIndex),
                        new MySqlParameter("length", (int)toIndex),
                        };

                        allAssignment = db.Database.SqlQuery<AssignedListForCRE>(str, parameter).ToList();
                    }
                    else //Campaign & Others
                    {
                        string str = @"CALL smrcampaign_assign_list(@incampaign_id,@start_date,@end_date,@incategory,@inservicetype, @inlocation, @startwith,@length)";

                        MySqlParameter[] parameter = new MySqlParameter[] {
                            new MySqlParameter("incampaign_id", campaignNameCat),
                            new MySqlParameter("start_date", fromDateNew),
                            new MySqlParameter("end_date", toDateNew),
                            new MySqlParameter("incategory", customerCat),
                            new MySqlParameter("inservicetype", serviceCat),
                            new MySqlParameter("inlocation", cityId),
                            new MySqlParameter("startwith", (int)fromIndex),
                            new MySqlParameter("length", (int)toIndex)
                        };
                        allAssignment = db.Database.SqlQuery<AssignedListForCRE>(str, parameter).ToList();
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return allAssignment;
        }

        public int getAssignListCount(DateTime fromDateNew, DateTime toDateNew, string customerCat,
            string serviceCat, long campaignNameType, string modelname, string cityId, string workshopname, int countFor)
        {
            ///countFor == if--->countFor=1(i.e for ---->PSF) else other and campaign
            int totalCount = 0;
            string customer_cat = customerCat;
            string service_cat = serviceCat;

            if (customer_cat == "All")
            {

                customer_cat = "";
            }
            if (service_cat == "All")
            {

                service_cat = "";
            }

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (countFor == 1)//PSF
                    {
                        string str = @"call psfassignlist_count(@campaignNamePSF,@fromDateNew,@toDateNew,@customer_cat,@service_cat,@inmodel,@inlocation,@inworkshop_id)";
                        MySqlParameter[] parameter = new MySqlParameter[] {
                            new MySqlParameter("campaignNamePSF", campaignNameType),
                            new MySqlParameter("fromDateNew", fromDateNew),
                            new MySqlParameter("toDateNew", toDateNew),
                            new MySqlParameter("customer_cat", customer_cat),
                            new MySqlParameter("service_cat", service_cat),
                            new MySqlParameter("inmodel", modelname),
                            new MySqlParameter("inlocation", cityId),
                            new MySqlParameter("inworkshop_id", workshopname)
                        };

                        totalCount = db.Database.SqlQuery<int>(str, parameter).FirstOrDefault();
                    }
                    else
                    {
                        string str = @"call smrcampaign_assign_list_count(@incampaign_id,@start_date,@end_date,@incategory,@inservicetype,@inlocation)";
                        MySqlParameter[] parameter = new MySqlParameter[] {
                            new MySqlParameter("incampaign_id", campaignNameType),
                            new MySqlParameter("start_date", fromDateNew),
                            new MySqlParameter("toDateNew", fromDateNew),
                            new MySqlParameter("end_date", toDateNew),
                            new MySqlParameter("incategory", customer_cat),
                            new MySqlParameter("inservicetype", service_cat),
                            new MySqlParameter("inlocation", cityId)
                        };

                        totalCount = db.Database.SqlQuery<int>(str, parameter).FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return totalCount;
        }


        public ActionResult assignCallToUser(string city, string workshop, string fromDate, string toDate, long campaignTypeIs, string campName, string psfName, string selected_cres, string modelname)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string selectedCampaignType = db.campaigns.FirstOrDefault(m => m.id == campaignTypeIs).campaignType;
                    DateTime from_Date = Convert.ToDateTime(Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd"));
                    DateTime to_Date = Convert.ToDateTime(Convert.ToDateTime(toDate).ToString("yyyy-MM-dd"));
                    string str = string.Empty;

                    List<string> creList = new List<string>();
                    creList = selected_cres.Split(',').ToList();

                    foreach (var name in creList)
                    {
                        string wyzname = name;
                        fmanualuser user = new fmanualuser();
                        user.upload_id = DateTime.Now.Day + DateTime.Now.Month + DateTime.Today.Year + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond;
                        var wyzId = db.wyzusers.FirstOrDefault(m => m.userName == wyzname).id;
                        user.wyzuser_id = wyzId;
                        db.fmanualusers.AddOrUpdate(user);
                        db.SaveChanges();
                        //fmanualuser 
                    }

                    if (selectedCampaignType == "PSF")
                    {
                        str = @"CALL psfWyzuserAssignmentInsertion(@fromDate,@toDate,@workshopId,@isCommerical, @psftype)";

                        MySqlParameter[] parameter = new MySqlParameter[]
                        {
                            new MySqlParameter("fromDate", from_Date),
                            new MySqlParameter("toDate", to_Date),
                            new MySqlParameter("workshopId", workshop),
                            new MySqlParameter("isCommerical", modelname),
                            new MySqlParameter("psftype", psfName)
                        };

                        int res = db.Database.ExecuteSqlCommand(str, parameter);
                        if (res == 0)
                        {
                            return Json(new { success = true });
                        }
                        else
                        {
                            return Json(new { success = false });
                        }
                    }
                    else
                    {
                        str = @"CALL smrWyzuserAssignmentInsertion(@in_campaign_id,@start_date,@end_date,@in_location)";

                        MySqlParameter[] parameter = new MySqlParameter[]
                        {
                            new MySqlParameter("in_campaign_id", campName),
                            new MySqlParameter("start_date", from_Date),
                            new MySqlParameter("end_date", to_Date),
                            new MySqlParameter("in_location", workshop)
                        };

                        int res = db.Database.ExecuteSqlCommand(str, parameter);
                        if (res == 0)
                        {
                            return Json(new { success = true });
                        }
                        else
                        {
                            return Json(new { success = false });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return View();
        }

        #region Fresh call deleteion

        //************************** Fresh call deletion **********************************

        [ActionName("Assignment_Deletion")]
        public ActionResult assignmentDeletion()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {

                    var locationList = db.locations.Select(m => new { id = m.cityId, name = m.name }).OrderBy(m => m.name).ToList();

                    ViewBag.locationList = locationList;
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public ActionResult listCampaignAndCRE(string moduleType)
        {
            List<campaign> campList = new List<campaign>();
            List<wyzuser> creList = new List<wyzuser>();
            try
            {
                using (var db = new AutoSherDBContext())
                {

                    string creManeger = Session["UserName"].ToString();
                    campList = db.campaigns.Where(m => m.campaignType == moduleType || m.campaignType == "Forecast" && m.isactive == true).ToList();
                    if (Session["UserRole"].ToString() == "Admin")
                    {
                        if (moduleType == "Insurance")
                        {
                            creList = db.wyzusers.Where(m => m.role == "CRE" && m.unAvailable==false && m.insuranceRole == true && m.role1 != "1").ToList();
                        }
                        else if (moduleType == "Campaign")
                        {
                            creList = db.wyzusers.Where(m => m.role == "CRE" && m.unAvailable == false && m.insuranceRole == false && m.role1 == "1").ToList();
                        }
                        else if (moduleType == "PSF")
                        {
                            creList = db.wyzusers.Where(m => m.role == "CRE" && m.unAvailable == false && m.insuranceRole == false && m.role1 == "4").ToList();
                        }
                    }
                    else
                    {
                        if (moduleType == "Insurance")
                        {
                            if (Session["DealerName"].ToString() == "INDUS MOTORS")
                            {
                                creList = db.wyzusers.Where(m => m.role == "CRE" && m.unAvailable == false && m.insuranceRole == true && m.role1 != "1").ToList();
                            }
                            else
                            {
                                creList = db.wyzusers.Where(m => m.creManager == creManeger && m.role == "CRE" && m.unAvailable == false && m.insuranceRole == true && m.role1 != "1").ToList();
                            }

                        }
                        else if (moduleType == "Campaign")
                        {
                            creList = db.wyzusers.Where(m => m.creManager == creManeger && m.role == "CRE" && m.unAvailable == false && m.insuranceRole == false && m.role1 == "1").ToList();
                        }
                        else if (moduleType == "PSF")
                        {
                            creList = db.wyzusers.Where(m => m.role == "CRE" && m.unAvailable == false && m.insuranceRole == false && m.role1 == "4").ToList();
                        }
                    }
                }
                var camp = campList.Select(m => new { campaignName = m.campaignName, id = m.id }).OrderBy(m => m.campaignName).ToList();
                var cre = creList.Select(m => new { id = m.id, userName = m.firstName + "-" + m.userName }).OrderBy(m => m.userName).ToList();
                return Json(new { success = true, campaign = camp, cres = cre });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message.ToString() });
            }
        }

        //After clicking View 
        public ActionResult assignedInteractionData(string wyzuserId, string location, string moduleName, string campaignName, string fromDate, string toDate, int callPurpose)
        {
            List<AssignedIntreactionNoCall> nocall = new List<AssignedIntreactionNoCall>();
            try
            {

                if (callPurpose == 1)//for displaying
                {
                    //Parameters of Paging..........
                    int start = Convert.ToInt32(Request["start"]);
                    int toIndex = Convert.ToInt32(Request["length"]);
                    string searchPattern = Request["search[value]"];
                    int totalSize = 0;
                    long cityId;
                    if (location != "")
                    {
                        cityId = new AutoSherDBContext().locations.FirstOrDefault(m => m.name == location).cityId;
                    }
                    else
                    {
                        cityId = 0;
                    }


                    nocall = getAssignedInteractionListData(wyzuserId, cityId, moduleName, campaignName, fromDate, toDate);
                    totalSize = nocall.Count();
                    if (toIndex < 0)
                    {
                        toIndex = 10;
                    }
                    if (toIndex > totalSize)
                    {
                        toIndex = totalSize;
                    }
                    var result = Json(new { data = nocall, draw = Request["draw"], recordsTotal = totalSize, recordsFiltered = totalSize }, JsonRequestBehavior.AllowGet);
                    result.MaxJsonLength = int.MaxValue;
                    return result;


                    // return Json(new { data = nocall, draw = Request["draw"], recordsTotal = totalSize, recordsFiltered = totalSize }, JsonRequestBehavior.AllowGet);
                }
                else if (callPurpose == 2)//for deletion
                {
                    using (var db = new AutoSherDBContext())
                    {
                        string date1 = "0";
                        string date2 = "0";

                        string str = string.Empty;

                        str = @"call FreshCallDeletion(@wyzuserId, @locId, @moduleName, @campaignid, @fromDate, @toDate)";

                        MySqlParameter[] param = new MySqlParameter[]
                        {
                        new MySqlParameter("wyzuserId", wyzuserId),
                        new MySqlParameter("locId", location),
                        new MySqlParameter("moduleName", moduleName),
                        new MySqlParameter("campaignid", campaignName),
                        new MySqlParameter("fromDate", date1),
                        new MySqlParameter("toDate", date2)
                        };

                        int res = db.Database.ExecuteSqlCommand(str, param);

                        if (res > 0)
                        {
                            return Json(new { success = true });
                        }
                        else
                        {
                            return Json(new { success = false });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return Json(new { success = false });

        }

        //Count and data of assignment for deletion
        public List<AssignedIntreactionNoCall> getAssignedInteractionListData(string wyzuserId, long location,
            string moduleName, string campaignName, string fromDate, string toDate)
        {
            List<AssignedIntreactionNoCall> nocall = new List<AssignedIntreactionNoCall>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string date1 = "0";
                    string date2 = "0";

                    string str = string.Empty;

                    str = @"call cre_assignment_selection_within_date_range(@wyzuserId, @locId, @moduleName, @campaignid, @fromDate, @toDate)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("wyzuserId", wyzuserId),
                        new MySqlParameter("locId", location),
                        new MySqlParameter("moduleName", moduleName),
                        new MySqlParameter("campaignid", campaignName),
                        new MySqlParameter("fromDate", date1),
                        new MySqlParameter("toDate", date2)
                    };

                    nocall = db.Database.SqlQuery<AssignedIntreactionNoCall>(str, param).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return nocall;
        }

        #endregion


        #region Insurance Assigned Interaction
        //*********************************** Insurance Assigned Interaction ***********************************
        [ActionName("InsuranceAssignment")]
        public ActionResult getInsuranceAssignment(long id)
        {
            AssignmentViewModal assign = new AssignmentViewModal();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string creManager = Session["UserName"].ToString();
                    var creList = db.wyzusers.Where(m => m.creManager == creManager && m.role == "CRE" && m.unAvailable == false).Select(m => new { id = m.id, name = m.firstName + "-" + m.userName }).ToList();
                    var campList = db.campaigns.Where(m => m.campaignType == "Insurance" && m.isactive == true).Select(m => new { id = m.id, type = m.campaignName }).ToList();
                    var campListFiltered = db.campaigns.Where(m => m.campaignType == "Insurance" || m.campaignType == "Forecast" && m.isactive == true).Select(m => new { id = m.id, name = m.campaignName }).ToList();
                    var location = db.locations.OrderBy(m => m.name).ToList();

                    ViewBag.creList = creList;
                    ViewBag.campList = campList;
                    ViewBag.campNameList = campListFiltered;
                    ViewBag.location = location;

                    ViewBag.isRangeSelected = false;
                    ViewBag.toAssigncall = true;
                    ViewBag.uploadId = id;
                    if (id > 0)
                    {
                        assign.changeAssignDetails = db.Database.SqlQuery<ChangeAssignment>("call uploadAssignedDetails(@inuploadid);", new MySqlParameter("@inuploadid", id)).ToList();

                    }
                    assign.uploadId = id;
                }

            }
            catch (Exception ex)
            {

            }

            return View(assign);
        }

        public ActionResult doInsuranceAssignment(string creList, long campType, int campName, string from_date, string to_date, string location, int callFor)
        {
            List<CallLogAjaxLoadInsurance> scheduleData = new List<CallLogAjaxLoadInsurance>();
            string str = string.Empty;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    //string selectedCampaign = db.campaigns.FirstOrDefault(m => m.id == campType).campaignType;
                    DateTime fromDate = Convert.ToDateTime(Convert.ToDateTime(from_date).ToString("yyyy-MM-dd"));
                    DateTime toDate = Convert.ToDateTime(Convert.ToDateTime(to_date).ToString("yyyy-MM-dd"));
                    long locId = db.locations.FirstOrDefault(m => m.name == location).cityId;
                    if (callFor == 1)//get list
                    {
                        str = @"CALL insurance_assign_list(@in_campaign_id,@start_date, @end_date,@in_location, @in_workshop_id)";
                        MySqlParameter[] paramter = new MySqlParameter[]
                        {
                            new MySqlParameter("in_campaign_id", campName),
                            new MySqlParameter("start_date", fromDate),
                            new MySqlParameter("end_date", toDate),
                            new MySqlParameter("in_location", locId),
                            new MySqlParameter("in_workshop_id", int.Parse(locId.ToString()))
                        };

                        scheduleData = db.Database.SqlQuery<CallLogAjaxLoadInsurance>(str, paramter).ToList();
                        return Json(new { data = scheduleData, draw = Request["draw"], recordsTotal = scheduleData.Count(), recordsFiltered = scheduleData.Count() }, JsonRequestBehavior.AllowGet);
                    }
                    else if (callFor == 2)//assign list
                    {

                        List<string> cre_List = creList.Split(',').ToList();

                        foreach (var id in cre_List)
                        {
                            //string wyzname = name;
                            fmanualuser user = new fmanualuser();
                            user.upload_id = DateTime.Now.Day + DateTime.Now.Month + DateTime.Today.Year + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond;
                            var wyzId = long.Parse(id);
                            user.wyzuser_id = wyzId;
                            db.fmanualusers.AddOrUpdate(user);
                            db.SaveChanges();
                            //fmanualuser 
                        }

                        str = @"CALL insWyzuserAssignmentInsertion(@in_campaign_id,@start_date, @end_date,@in_location, @inmanagerid)";
                        MySqlParameter[] paramter = new MySqlParameter[]
                        {
                            new MySqlParameter("in_campaign_id", campName),
                            new MySqlParameter("start_date", fromDate),
                            new MySqlParameter("end_date", toDate),
                            new MySqlParameter("in_location", locId),
                            new MySqlParameter("inmanagerid", long.Parse(Session["UserId"].ToString()))
                        };

                        var res = db.Database.ExecuteSqlCommand(str, paramter);

                        if (res != null)
                        {
                            return Json(new { success = true });
                        }
                        else
                        {
                            return Json(new { success = false });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return Json(new { success = false });
        }


        #endregion

        #region ChnageAssignment
        //********************************************** Change assignment ******************************************
        [ActionName("Change_assignCalls")]
        public ActionResult changeAssignmentGet()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    //if (Session["UserRole"].ToString() == "Admin")
                    //{
                    //    var creList = db.wyzusers.Where(m => m.role == "CRE").Select(m => new { id = m.id, name = m.userName }).OrderBy(m => m.name).ToList();
                    //    ViewBag.ddlCreList = creList;
                    //}
                    //else
                    //{

                    //    ViewBag.ddlCreList = creList;

                    //}

                    string creManager = Session["UserName"].ToString();

                    if (Session["DealerName"].ToString() == "INDUS MOTORS")
                    {
                        string role = db.wyzusers.FirstOrDefault(m => m.userName == creManager).role1;
                        var creList = db.wyzusers.Where(m => m.role1 == role && m.role == "CRE" && m.unAvailable == false).Select(m => new { id = m.id, name = m.firstName + "-" + m.userName }).OrderBy(m => m.name).ToList();
                        ViewBag.ddlCreList = creList;
                        var location = db.workshops.Where(m => m.isinsurance == false).Select(m => new { id = m.id, name = m.workshopName }).OrderBy(m => m.name).ToList();
                        ViewBag.location = location;


                    }
                    else
                    {
                        var creList = db.wyzusers.Where(m => m.creManager == creManager && m.role == "CRE" && m.unAvailable == false).Select(m => new { id = m.id, name = m.firstName + "-" + m.userName }).OrderBy(m => m.name).ToList();
                        ViewBag.ddlCreList = creList;
                        var location = db.locations.Select(m => new { id = m.cityId, name = m.name }).OrderBy(m => m.name).ToList();
                        ViewBag.location = location;

                    }

                    var serviceType = db.servicetypes.Where(m => m.isActive).Select(m => new { id = m.id, name = m.serviceTypeName }).OrderBy(m => m.name).ToList();
                    ViewBag.serviceType = serviceType;
                    var renewalType = db.renewaltypes.Select(m => new { id = m.id, name = m.renewalTypeName }).OrderBy(m => m.name).ToList();
                    ViewBag.renewalType = renewalType;
                    //ViewBag.campList = campAllList;
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        //function to get disposition name in jquery datatable 
        //public ActionResult getQueryData(long id,int callFor)
        //{
        //    string name = string.Empty;
        //    try
        //    {
        //        if(callFor==1)
        //        {
        //            name= new AutoSherDBContext().calldispositiondatas.FirstOrDefault(m => m.id == id).disposition;
        //        }
        //        else if(callFor==2)
        //        {
        //            name = new AutoSherDBContext().campaigns.FirstOrDefault(m => m.id == id).campaignName;
        //        }
        //        else
        //        {
        //            return Json(new { success = false });
        //        }

        //        return Json(new { success = true, data = name });
        //    }
        //    catch(Exception ex)
        //    {
        //        return Json(new { success = false });
        //    }

        //    //return Json(new { success = false });
        //}

        public ActionResult getWorkshop(string value)
        {
            try
            {
                List<long> wyzuser_ids = value.Split(',').Select(long.Parse).ToList();
                List<workshop> workshops = new List<workshop>();
                
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                    if(Session["LoginUser"].ToString()!= "Insurance") 
                    {
                        List<long> listOfWrkshopId = db.userworkshops.Where(x => wyzuser_ids.Contains(x.userWorkshop_id)).Select(x => x.workshopList_id).Distinct().ToList();
                        workshops = db.workshops.Where(x => listOfWrkshopId.Contains(x.id)).ToList();
                    }
                    else
                    {
                        workshops = db.Database.SqlQuery<workshop>("select * from workshop where id in (select location_id from insuranceassignedinteraction where wyzUser_id in (@wyzuser_ids) group by location_id);", new MySqlParameter("@wyzuser_ids", value)).ToList();
                    }
                   
                   

                    //var workshopIdList = db.Database.SqlQuery<long>("select distinct(location_id) from insuranceassignedinteraction where wyzUser_id in (@wyzuser_ids) group by location_id;", new MySqlParameter("@wyzuser_ids", value)).ToList();

                    //var workshopList = db.workshops.Where(m => workshopIdList.Contains(m.id)).Select(m => new { id = m.id, name = m.workshopName }).ToList();

                     

                    return Json(new { data = workshops.Select(m => new { id = m.id, name = m.workshopName }) }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
        
      
       public ActionResult getworkshop2(string location)
        {
            try
            {

            }
            catch(Exception ex)
            {

            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult changeAssignmentAction(string campSelecType, string campSelec, DateTime? fromdate, DateTime? todate, string selected_cres, string selected_dispositions, string workshopNames, string selected_servicetypelist, string folowupfromdate, string followuptodateTo,string bookingFromdate, string bookingTodate,string isEditedDate="")
        {
            string from_date, to_date;
            //Parameters of Paging..........
            long fromIndex = Convert.ToInt32(Request["start"]);
            long toIndex = Convert.ToInt32(Request["length"]);
            string searchPattern = Request["search[value]"];
            long totalSize = 0;
            string str = string.Empty, exception = "";

            List<ChangeAssignment> assign = new List<ChangeAssignment>();
            if (fromdate == null && todate == null)
            {
                DateTime todayDate = DateTime.Now;
                from_date = todayDate.AddMonths(-5).ToString("yyyy-MM-dd");
                to_date = todayDate.ToString("yyyy-MM-dd");
            }
            else
            {
                from_date = Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd");
                to_date = Convert.ToDateTime(todate).ToString("yyyy-MM-dd");
            }
            try
            {

                using (var db = new AutoSherDBContext())
                {
                    db.Database.CommandTimeout = 900;
                    str = @"call change_assignment_list_count(@users,@dispostion,@fromdate,@todate,@campaign_type,@campaign_name,@inloc,@inservicetype,@fromfollowupdate,@tofollowupdate,@bookingFromdate,@bookingTodate,@isEditedDate)"; 

                    MySqlParameter[] paramter;
                    paramter = new MySqlParameter[]
                    {
                            new MySqlParameter("users", selected_cres),
                            new MySqlParameter("dispostion", selected_dispositions),
                            new MySqlParameter("fromdate", from_date),
                            new MySqlParameter("todate", to_date),
                            new MySqlParameter("campaign_type", campSelecType),
                            new MySqlParameter("campaign_name", campSelec),
                            new MySqlParameter("inloc", workshopNames),
                            new MySqlParameter("inservicetype", selected_servicetypelist),
                            new MySqlParameter("fromfollowupdate", folowupfromdate),
                            new MySqlParameter("tofollowupdate", followuptodateTo),
                            new MySqlParameter("bookingFromdate", bookingFromdate),
                            new MySqlParameter("bookingTodate", bookingTodate),
                            new MySqlParameter("isEditedDate", isEditedDate)
                    };

                    totalSize = db.Database.SqlQuery<long>(str, paramter).FirstOrDefault();
                    if (toIndex < 0)
                    {
                        toIndex = 10;
                    }
                    if (toIndex > totalSize)
                    {
                        toIndex = totalSize;
                    }

                    assign = getCallListToChange(campSelecType, campSelec, from_date, to_date, selected_cres, selected_dispositions, workshopNames, fromIndex, toIndex, selected_servicetypelist, folowupfromdate, followuptodateTo, bookingFromdate, bookingTodate,isEditedDate);
                    return Json(new { data = assign, draw = Request["draw"], recordsTotal = totalSize, recordsFiltered = totalSize, exception = exception }, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                exception = ex.Message.ToString();
                if (ex.Message.Contains("inner exception"))
                {
                    exception = ex.InnerException.Message;
                }
            }

            return Json(new { data = assign, draw = Request["draw"], recordsTotal = totalSize, recordsFiltered = totalSize, exception = exception }, JsonRequestBehavior.AllowGet);
        }

        public List<ChangeAssignment> getCallListToChange(string campSelecType, string campSelec, string fromdate, string todate, string selected_cres, string selected_dispositions, string loc, long fromIndex, long toIndex, string selected_servicetypelist, string folowupfromdate, string followuptodateTo, string bookingFromdate, string bookingTodate, string isEditedDate="")
        {
            string str = string.Empty;
            List<ChangeAssignment> assign = new List<ChangeAssignment>();
            fromdate = Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd");
            todate = Convert.ToDateTime(todate).ToString("yyyy-MM-dd");
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    db.Database.CommandTimeout = 900;
                    str = @"call change_assignment_list(@users, @dispostion, @fromdate, @todate, @campaign_type,@campaign_name, @inloc,@inservicetype, @fromfollowupdate, @tofollowupdate,@start_with,@length,@bookingFromdate,@bookingTodate,@isEditedDate)";
                    MySqlParameter[] paramter = new MySqlParameter[]
                    {
                            new MySqlParameter("users", selected_cres),
                            new MySqlParameter("dispostion", selected_dispositions),
                            new MySqlParameter("fromdate", fromdate),
                            new MySqlParameter("todate", todate),
                            new MySqlParameter("campaign_type", campSelecType),
                            new MySqlParameter("campaign_name", campSelec),
                            new MySqlParameter("inloc", loc),
                            new MySqlParameter("inservicetype", selected_servicetypelist),
                            new MySqlParameter("fromfollowupdate",folowupfromdate),
                            new MySqlParameter("tofollowupdate",followuptodateTo),
                            new MySqlParameter("start_with",fromIndex),
                            new MySqlParameter("length",toIndex),
                            new MySqlParameter("bookingFromdate", bookingFromdate),
                            new MySqlParameter("bookingTodate", bookingTodate),
                            new MySqlParameter("isEditedDate",isEditedDate)
                    };

                    assign = db.Database.SqlQuery<ChangeAssignment>(str, paramter).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return assign;
        }

        public ActionResult changeCallAssignment(string campSelecType, string filtData, string selected_cre_list, string selected_camp, bool isAllSelected, string numOfAssign, string selected_servicetypelist, string folowupfromdate, string followuptodateTo,string bookingFromdate, string bookingTodate,string isEditedDate = "")
        {
            //List<long> assignCampList = new List<long>();
            List<long> serviceBookedIdList = new List<long>();
            List<long> InteractionCallId = new List<long>();
            List<string> campIdList = new List<string>();
            List<long> newWyzId = new List<long>();
            long AssignCalls = 0;
            if (numOfAssign != "")
            {
                AssignCalls = long.Parse(numOfAssign);
            }

            if (selected_cre_list != "" && selected_cre_list != null)
            {
                newWyzId = selected_cre_list.Split(',').Select(long.Parse).ToList();
            }
            int creCount = newWyzId.Count;
            bool isAssignDone = false;
            List<change_assignment_records> change_Assign = new List<change_assignment_records>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    db.Database.CommandTimeout = 600;

                    if (isAllSelected == true || AssignCalls != 0)
                    {
                        dynamic data = JsonConvert.DeserializeObject(filtData);
                        string from_date = Convert.ToDateTime(data.fromdate).ToString("yyyy-MM-dd");
                        string to_date = Convert.ToDateTime(data.todate).ToString("yyyy-MM-dd");

                        long totalSize = 0;

                        if (AssignCalls == 0)
                        {
                            string str = @"call change_assignment_list_count(@users,@dispostion,@fromdate,@todate,@campaign_type,@campaign_name,@inloc,@inservicetype,@fromfollowupdate,@tofollowupdate,@bookingFromdate,@bookingTodate,@isEditedDate)";


                            MySqlParameter[] paramter = new MySqlParameter[]
                            {
                            new MySqlParameter("users", data.selected_cres),
                            new MySqlParameter("dispostion", data.selected_dispositions),
                            new MySqlParameter("fromdate", from_date),
                            new MySqlParameter("todate", to_date),
                            new MySqlParameter("campaign_type", campSelecType),
                            new MySqlParameter("campaign_name", data.campSelec),
                            new MySqlParameter("inloc", data.workshopNames),
                            new MySqlParameter("inservicetype", data.selected_servicetypelist),
                            new MySqlParameter("fromfollowupdate", folowupfromdate),
                            new MySqlParameter("tofollowupdate", followuptodateTo),
                            new MySqlParameter("bookingFromdate", bookingFromdate),
                            new MySqlParameter("bookingTodate", bookingTodate),
                             new MySqlParameter("isEditedDate",isEditedDate)
                            };

                            totalSize = db.Database.SqlQuery<long>(str, paramter).FirstOrDefault();
                        }
                        else
                        {
                            totalSize = AssignCalls;
                        }

                        List<ChangeAssignment> assignments = new List<ChangeAssignment>();
                        string campSelec = data.campSelec, selected_cres = data.selected_cres, selected_dispositions = data.selected_dispositions, loc = data.workshopNames;

                        assignments = getCallListToChange(campSelecType, campSelec, from_date, to_date, selected_cres, selected_dispositions, loc, 0, totalSize, selected_servicetypelist, folowupfromdate, followuptodateTo, bookingFromdate, bookingTodate);
                        InteractionCallId.AddRange(assignments.Select(m => m.id));
                        serviceBookedIdList.AddRange(assignments.Select(m => m.servicebookedId));

                        //assignCampList.AddRange(assignments.Select(m=>m.campaign_id));
                    }
                    else
                    {
                        campIdList = selected_camp.Split(',').ToList();

                        for (int i = 0; i < campIdList.Count(); i += 3)
                        {
                            InteractionCallId.Add(long.Parse(campIdList[i]));

                            if ((i + 2) < campIdList.Count())
                            {
                                serviceBookedIdList.Add(long.Parse(campIdList[(i + 2)]));
                            }

                            //if (i != 0)
                            //{
                            //    serviceBookedIdList.Add(long.Parse(campIdList[i - 1]));
                            //}
                            //else
                            //{

                            //}
                            //assignCampList.Add(long.Parse(campIdList[i + 1]));
                        }
                    }

                    if (InteractionCallId.Count > 0)
                    {
                        if (campSelecType == "Campaign")//Service
                        {
                            for (int i = 0; i < InteractionCallId.Count(); i++)
                            {
                                long id = InteractionCallId[i];

                                assignedinteraction assignInter = db.assignedinteractions.FirstOrDefault(m => m.id == id);

                                change_assignment_records changeAssignRecord = new change_assignment_records();
                                changeAssignRecord.assignedinteraction_id = assignInter.id;
                                changeAssignRecord.campaign_id = assignInter.campaign_id ?? default(long);
                                changeAssignRecord.last_wyzuserId = assignInter.wyzUser_id ?? default(long);

                                if (i == 0)
                                {
                                    long wyzuserId = newWyzId[i];
                                    changeAssignRecord.new_wyzuserId = wyzuserId;
                                    long wyzId = wyzuserId;
                                    //wyzuser wyzuser = db.wyzusers.FirstOrDefault(m => m.id == wyzId);

                                    assignInter.wyzUser_id = wyzuserId; //updation
                                    if (serviceBookedIdList[i] != 0)
                                    {
                                        long serviceId = serviceBookedIdList[i];
                                        servicebooked service = db.servicebookeds.FirstOrDefault(m => m.serviceBookedId == serviceId);
                                        service.chaser_id = wyzuserId;
                                        db.servicebookeds.AddOrUpdate(service);
                                        db.SaveChanges();
                                    }
                                    //assignInter.wyzuser = wyzuser;
                                    //assignInter.assigned_wyzuser_id= wyzuserId; 
                                    //assignInter.assigned_manager_id = long.Parse(Session["UserId"].ToString());
                                }
                                else
                                {
                                    int wyzPos = i % creCount;

                                    long wyzuserId = newWyzId[wyzPos];

                                    changeAssignRecord.new_wyzuserId = wyzuserId;
                                    assignInter.wyzUser_id = wyzuserId;
                                    if (serviceBookedIdList[i] != 0)
                                    {
                                        long serviceId = serviceBookedIdList[i];
                                        servicebooked service = db.servicebookeds.FirstOrDefault(m => m.serviceBookedId == serviceId);
                                        service.chaser_id = wyzuserId;
                                        db.servicebookeds.AddOrUpdate(service);
                                        db.SaveChanges();
                                    }
                                    //long wyzId = wyzuserId;
                                    //wyzuser wyzuser = db.wyzusers.FirstOrDefault(m => m.id == wyzuserId);
                                    //assignInter.wyzuser = wyzuser;
                                    //assignInter.assigned_wyzuser_id = wyzuserId;
                                    //assignInter.assigned_manager_id = long.Parse(Session["UserId"].ToString());
                                }
                                changeAssignRecord.updatedDate = DateTime.Now;
                                changeAssignRecord.moduletypeIs = 1;
                                changeAssignRecord.updatedType = Session["UserName"].ToString();
                                //db.change_assignment_records.Add(changeAssignRecord);
                                change_Assign.Add(changeAssignRecord);
                                db.assignedinteractions.AddOrUpdate(assignInter);
                                db.SaveChanges();
                                isAssignDone = true;
                            }
                        }//-------->Service If End
                        else if (campSelecType == "Insurance")
                        {
                            for (int i = 0; i < InteractionCallId.Count(); i++)
                            {
                                long id = InteractionCallId[i];
                                insuranceassignedinteraction assignInter = db.insuranceassignedinteractions.FirstOrDefault(m => m.id == id);
                                change_assignment_records changeAssignRecord = new change_assignment_records();
                                changeAssignRecord.assignedinteraction_id = assignInter.id;
                                changeAssignRecord.campaign_id = assignInter.campaign_id ?? default(long);
                                changeAssignRecord.last_wyzuserId = assignInter.wyzUser_id ?? default(long);

                                if (i == 0)
                                {
                                    long wyzuserId = newWyzId[i];
                                    changeAssignRecord.new_wyzuserId = wyzuserId;

                                    //wyzuser wyzuser = db.wyzusers.FirstOrDefault(m => m.id == wyzuserId);

                                    assignInter.wyzUser_id = wyzuserId;
                                    //if (serviceBookedIdList[i] != 0)
                                    //{
                                    //    long insId = serviceBookedIdList[i];
                                    //    appointmentbooked appointment = db.appointmentbookeds.FirstOrDefault(m => m.appointmentId == insId);
                                    //    appointment.chaserId = wyzuserId;
                                    //    db.appointmentbookeds.AddOrUpdate(appointment);
                                    //    db.SaveChanges();
                                    //}
                                    ////assignInter.wyzuser = wyzuser;
                                    //assignInter.assigned_wyzuser_id = wyzuserId;
                                    //assignInter.assigned_manager_id = long.Parse(Session["UserId"].ToString());
                                }
                                else
                                {
                                    int wyzPos = i % creCount;
                                    long wyzId = newWyzId[wyzPos];
                                    changeAssignRecord.new_wyzuserId = wyzId;
                                    assignInter.wyzUser_id = wyzId;

                                    //if (serviceBookedIdList[i] != 0)
                                    //{
                                    //    long insId = serviceBookedIdList[i];
                                    //    appointmentbooked appointment = db.appointmentbookeds.FirstOrDefault(m => m.appointmentId == insId);
                                    //    appointment.chaserId = wyzId;
                                    //    db.appointmentbookeds.AddOrUpdate(appointment);
                                    //    db.SaveChanges();
                                    //}
                                    //wyzuser wyzuser = db.wyzusers.FirstOrDefault(m => m.id == wyzId);
                                    //assignInter.wyzuser = wyzuser;
                                    //assignInter.assigned_wyzuser_id = wyzId;
                                    //assignInter.assigned_manager_id = long.Parse(Session["UserId"].ToString());
                                }
                                changeAssignRecord.updatedDate = DateTime.Now;
                                changeAssignRecord.moduletypeIs = 2;
                                changeAssignRecord.updatedType = Session["UserName"].ToString();
                                //db.change_assignment_records.Add(changeAssignRecord);
                                change_Assign.Add(changeAssignRecord);
                                db.insuranceassignedinteractions.AddOrUpdate(assignInter);
                                db.SaveChanges();
                                isAssignDone = true;
                            }
                        }
                        else if (campSelecType == "PSF")
                        {
                            for (int i = 0; i < InteractionCallId.Count(); i++)
                            {
                                long id = InteractionCallId[i];
                                psfassignedinteraction assignInter = db.psfassignedinteractions.FirstOrDefault(m => m.id == id);
                                change_assignment_records changeAssignRecord = new change_assignment_records();
                                changeAssignRecord.assignedinteraction_id = assignInter.id;
                                changeAssignRecord.campaign_id = assignInter.campaign_id ?? default(long);
                                changeAssignRecord.last_wyzuserId = assignInter.wyzUser_id ?? default(long);

                                if (i == 0)
                                {
                                    changeAssignRecord.new_wyzuserId = newWyzId[i];
                                    long wyzId = newWyzId[i];
                                    //wyzuser wyzuser = db.wyzusers.FirstOrDefault(m => m.id == wyzId);

                                    assignInter.wyzUser_id = wyzId;

                                    //if (serviceBookedIdList[i] != 0)
                                    //{
                                    //    servicebooked service = db.servicebookeds.FirstOrDefault(m => m.serviceBookedId == serviceBookedIdList[i]);
                                    //    service.chaser_id = wyzId;
                                    //    db.servicebookeds.AddOrUpdate(service);
                                    //    db.SaveChanges();
                                    //}

                                    //assignInter.wyzuser = wyzuser;
                                    //assignInter.id = newWyzId[i];
                                    //assignInter.assigned_manager_id = long.Parse(Session["UserId"].ToString());      
                                    //assignInter.id = newWyzId[i];
                                    //assignInter.assigned_manager_id = long.Parse(Session["UserId"].ToString());
                                    //assignInter.assigned_manager_id = long.Parse(Session["UserId"].ToString());
                                }
                                else
                                {
                                    int wyzPos = i % creCount;
                                    long wyzId = newWyzId[wyzPos];
                                    changeAssignRecord.new_wyzuserId = wyzId;
                                    assignInter.wyzUser_id = wyzId;

                                    //if (serviceBookedIdList[i] != 0)
                                    //{
                                    //    servicebooked service = db.servicebookeds.FirstOrDefault(m => m.serviceBookedId == serviceBookedIdList[i]);
                                    //    service.chaser_id = wyzId;
                                    //    db.servicebookeds.AddOrUpdate(service);
                                    //    db.SaveChanges();
                                    //}
                                    //wyzuser wyzuser = db.wyzusers.FirstOrDefault(m => m.id == wyzId);
                                    //assignInter.wyzuser = wyzuser;
                                }
                                changeAssignRecord.updatedDate = DateTime.Now;
                                changeAssignRecord.moduletypeIs = 4;
                                changeAssignRecord.updatedType = Session["UserName"].ToString();
                                db.change_assignment_records.Add(changeAssignRecord);
                                db.psfassignedinteractions.AddOrUpdate(assignInter);
                                db.SaveChanges();
                                isAssignDone = true;
                            }
                        }
                        else
                        {
                            return Json(new { success = false, error = "Invalid Campaign Selected" });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, error = "No CRE Selected" });
                    }

                    isAssignDone = doChnage_assignInsertion(change_Assign);
                }

                return Json(new { success = isAssignDone });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message.ToString() });
            }
            //return Json(new { success = false,error="Something went wrong" });
        }

        public bool doChnage_assignInsertion(List<change_assignment_records> change_assign)
        {
            using (var db = new AutoSherDBContext())
            {
                //foreach (var changeAssign in change_assign)
                //{
                //    db.change_assignment_records.Add(changeAssign);
                //    db.SaveChanges();
                //}

                db.change_assignment_records.AddRange(change_assign);
                db.SaveChanges();
            }

            return true;

        }

        #endregion

        #region AssignmentPlan

        //*********************************** Assignment Plan ***************************
        public List<ChangeAssignment> getAllAssignmentPlan(string managerLogin, string module, string campaignIds, string workshopId)
        {
            List<ChangeAssignment> resultLists = new List<ChangeAssignment>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string str = @"CALL all_call_assignment_plan(@managerLogin,@inmodule,@campaignIds,@workshopId)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("managerLogin", managerLogin),
                        new MySqlParameter("inmodule", module),
                        new MySqlParameter("campaignIds", campaignIds),
                        new MySqlParameter("workshopId", workshopId)

                    };
                    resultLists = db.Database.SqlQuery<ChangeAssignment>(str, param).ToList();

                }
            }
            catch (Exception)
            {

            }
            return resultLists;
        }


        [ActionName("AssignmentPlan")]
        public ActionResult assignmentPlan()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    var locationList = db.locations.Select(m => new { locationName = m.name, id = m.cityId }).OrderBy(m => m.locationName).ToList();
                    ViewBag.locationList = locationList;

                }
            }
            catch (Exception)
            {

            }

            return View();
        }

        public ActionResult getListCampaign(string campaign)
        {
            List<campaign> campaignType = new List<campaign>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    campaignType = db.campaigns.Where(m => m.campaignType == campaign && m.isactive == true).ToList();
                    return Json(campaignType, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception)
            {

            }
            return Json(campaignType, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getWorkshopListByLoca(int? selectedCity)
        {
            List<workshop> work_list = new List<workshop>();
            try
            {

                using (var db = new AutoSherDBContext())
                {
                    db.Configuration.LazyLoadingEnabled = false;

                    work_list = db.workshops.Where(x => x.location_cityId == selectedCity).ToList();
                    if (work_list == null)
                    {
                        work_list = db.workshops.ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return Json(work_list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getAllAutoAssignmentPlans(string module)
        {
            string managerUsername = Session["UserName"].ToString();

            try
            {
                using (var db = new AutoSherDBContext())
                {


                    List<ChangeAssignment> assignPlanList = getAllAssignmentPlan(managerUsername, module, "", "");

                    return Json(new { data = assignPlanList, draw = Request["draw"], recordsTotal = assignPlanList.Count(), recordsFiltered = assignPlanList.Count() }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        public List<wyzuser> getWyzuserByLocation(long? cityId, string moduleName)
        {
            List<wyzuser> wyzusersList = new List<wyzuser>();

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    // long loc = db.locations.FirstOrDefault(u => u.name == cityName).cityId;


                    if (moduleName == "Insurance")
                    {
                        wyzusersList = db.wyzusers.Where(w => w.location_cityId == cityId && w.insuranceRole == true && w.role == "CRE" && w.unAvailable == false).ToList();
                    }
                    else
                    {
                        wyzusersList = db.wyzusers.Where(w => w.location_cityId == cityId && w.insuranceRole == false && w.role == "CRE" && w.unAvailable == false).ToList();

                    }

                }
            }
            catch (Exception ex)
            {

            }

            return wyzusersList;
        }

        public List<wyzuser> getlistAssignedPlanWyzusers(string moduleName, long? cityId, long? campaignId)
        {
            List<wyzuser> creList = new List<wyzuser>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    // long locId = db.locations.FirstOrDefault(u => u.name == cityName).cityId;

                    //   long campaignId = Convert.ToInt64(campaignName);
                    List<assigmentplan> assignedPlan = new List<assigmentplan>();

                    if (moduleName == "Campaign")
                    {
                        assignedPlan = db.assigmentplans.Where(u => u.campaign_id == campaignId && u.locationId == cityId && u.smr == true).ToList();

                    }
                    else if (moduleName == "PSF")
                    {
                        assignedPlan = db.assigmentplans.Where(u => u.campaign_id == campaignId && u.locationId == cityId && u.psf == true).ToList();
                    }
                    else if (moduleName == "Insurance")
                    {
                        assignedPlan = db.assigmentplans.Where(u => u.campaign_id == campaignId && u.locationId == cityId && u.insurance == true).ToList();
                    }
                    foreach (var wyzusers in assignedPlan)
                    {
                        creList = db.wyzusers.Where(u => u.id == wyzusers.wyzuserId).ToList();
                    }

                    return creList;
                }
            }
            catch (Exception ex)
            {

            }
            return creList;
        }

        public ActionResult getWyzuserListWithPlannedByLocation(long? cityId, string moduleName, long? campaignId)
        {

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    List<wyzuser> wyzuserList = getWyzuserByLocation(cityId, moduleName);
                    List<wyzuser> assignedWyzusers = getlistAssignedPlanWyzusers(moduleName, cityId, campaignId);
                    return Json(new { CREList = wyzuserList.Select(m => new { id = m.id, userName = m.userName }).ToList(), DriverList = assignedWyzusers.Select(m => new { id = m.id, userName = m.userName, JsonRequestBehavior.AllowGet }) });
                }
            }
            catch (Exception ex)
            {
            }
            return Json(new { });

        }


        public ActionResult insertNewAssignmentPlans(string module, long? workshopid, long? campaignIds, string creList, string cityName)
        {
            List<long> wyzUser = new List<long>();
            if (creList.Contains(','))
            {
                wyzUser.AddRange(creList.Split(',').Select(long.Parse).ToList());
            }
            else
            {
                wyzUser.Add(long.Parse(creList));
            }
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (module == "Campaign")
                    {
                        if (creList.Length > 0)
                        {
                            List<assigmentplan> itemToRemove = db.assigmentplans.Where(a => a.smr == true && a.campaign_id != null && a.campaign_id == campaignIds && a.locationId == workshopid && !wyzUser.Contains(a.wyzuserId ?? default(long))).ToList();

                            if (itemToRemove != null)
                            {
                                db.assigmentplans.RemoveRange(itemToRemove);
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            List<assigmentplan> itemToRemove = db.assigmentplans.Where(a => a.smr == true && a.campaign_id != null && a.campaign_id == campaignIds && a.locationId == workshopid).ToList();

                            if (itemToRemove != null)
                            {
                                db.assigmentplans.RemoveRange(itemToRemove);
                                db.SaveChanges();
                            }
                        }
                    }
                    else if (module == "PSF")
                    {
                        if (creList.Length > 0)
                        {
                            List<assigmentplan> itemToRemove = db.assigmentplans.Where(a => a.psf == true && a.campaign_id != null && a.campaign_id == campaignIds && a.locationId == workshopid && !wyzUser.Contains(a.wyzuserId ?? default(long))).ToList();

                            if (itemToRemove != null)
                            {
                                db.assigmentplans.RemoveRange(itemToRemove);
                                db.SaveChanges();

                            }
                        }
                        else
                        {
                            List<assigmentplan> itemToRemove = db.assigmentplans.Where(a => a.smr == true && a.campaign_id != null && a.campaign_id == campaignIds && a.locationId == workshopid).ToList();

                            if (itemToRemove != null)
                            {
                                db.assigmentplans.RemoveRange(itemToRemove);
                                db.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        if (creList.Length > 0)
                        {
                            List<assigmentplan> itemToRemove = db.assigmentplans.Where(a => a.insurance == true && a.campaign_id != null && a.campaign_id == campaignIds && a.locationId == workshopid && !wyzUser.Contains(a.wyzuserId ?? default(long))).ToList();

                            if (itemToRemove != null)
                            {
                                db.assigmentplans.RemoveRange(itemToRemove);

                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            List<assigmentplan> itemToRemove = db.assigmentplans.Where(a => a.insurance == true && a.campaign_id != null && a.campaign_id == campaignIds && a.locationId == workshopid).ToList();

                            if (itemToRemove != null)
                            {
                                db.assigmentplans.RemoveRange(itemToRemove);
                                db.SaveChanges();

                            }
                        }
                    }

                    string campaignName = campaignIds.ToString();
                    List<wyzuser> assignedPlanWyzusers = getlistAssignedPlanWyzusers(module, workshopid, campaignIds);
                    List<long> assignedPlanWyzIds = assignedPlanWyzusers.Select(m => m.id).ToList();
                    foreach (var id in assignedPlanWyzIds)
                    {
                        wyzUser.Remove(id);
                    }

                    //String modifiedDate = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(Calendar.getInstance().getTime());
                    if (wyzUser.Count > 0)
                    {
                        for (int j = 0; j < wyzUser.Count; j++)
                        {
                            assigmentplan assignPlan = new assigmentplan();
                            if (module == "Campaign")
                            {
                                assignPlan.smr = true;
                                assignPlan.psf = false;
                                assignPlan.insurance = false;
                            }
                            else if (module == "PSF")
                            {
                                assignPlan.smr = false;
                                assignPlan.psf = true;
                                assignPlan.insurance = false;
                            }
                            else
                            {
                                assignPlan.smr = false;
                                assignPlan.psf = false;
                                assignPlan.insurance = true;
                            }

                            assignPlan.wyzuserId = wyzUser[j];
                            assignPlan.campaign_id = campaignIds;
                            assignPlan.locationId = workshopid;
                            assignPlan.created_date = DateTime.Now;
                            db.assigmentplans.Add(assignPlan);
                            db.SaveChanges();


                        }
                        return Json(new { success = true, exceptn = "" });
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
                return Json(new { success = false, exceptn = exception });
            }
            return Json(new { success = true, exceptn = "" });
        }

        #endregion
        //By nisarga for assignment for admin login

        [ActionName("changeAssignmentPending")]
        public ActionResult changeAssignmentPendingGet()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string creManager = Session["UserName"].ToString();

                    if (Session["LoginUser"].ToString() == "Insurance")
                    {
                        var creList = db.wyzusers.Where(m => m.role1 == "2" && m.role == "CRE" && m.unAvailable==false && m.insuranceRole == true).Select(m => new { id = m.id, name = m.firstName + "-" + m.userName }).OrderBy(m => m.name).ToList();
                        ViewBag.ddlCreList = creList;
                    }
                    else if (Session["LoginUser"].ToString() == "Service")
                    {
                        var creList = db.wyzusers.Where(m => m.role1 == "1" && m.role == "CRE" && m.unAvailable==false && m.insuranceRole == true).Select(m => new { id = m.id, name = m.firstName + "-" + m.userName }).OrderBy(m => m.name).ToList();
                        ViewBag.ddlCreList = creList;
                    }

                    var location = db.locations.Select(m => new { id = m.cityId, name = m.name }).OrderBy(m => m.name).ToList();
                    var serviceType = db.servicetypes.Where(m => m.isActive).Select(m => new { id = m.id, name = m.serviceTypeName }).OrderBy(m => m.name).ToList();
                    ViewBag.serviceType = serviceType;
                    
                   ViewBag.location = location;
                    
                    

                    var renewalType = db.renewaltypes.Select(m => new { id = m.id, name = m.renewalTypeName }).OrderBy(m => m.name).ToList();
                    ViewBag.renewalType = renewalType;
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }
       
        public ActionResult callAssignmentForAdmin(string campSelecType, string filtData, string selected_cre_list, string selected_camp, bool isAllSelected, string numOfAssign, string selected_servicetypelist, string folowupfromdate, string followuptodateTo, string bookingFromdate, string bookingTodate, string isEditedDate = "")
        {
            //List<long> assignCampList = new List<long>();
            List<long> serviceBookedIdList = new List<long>();
            List<long> InteractionCallId = new List<long>();
            List<string> campIdList = new List<string>();
            List<long> newWyzId = new List<long>();
            List<int> selectedServiceTypeList = new List<int>();
            List<int> serviceTypeList = new List<int>();
            long AssignCalls = 0;
            if (numOfAssign != "")
            {
                AssignCalls = long.Parse(numOfAssign);
            }

            if (selected_cre_list != "" && selected_cre_list != null)
            {
                newWyzId = selected_cre_list.Split(',').Select(long.Parse).ToList();
            }

            if (selected_servicetypelist != "" && selected_servicetypelist != null)
            {
                selectedServiceTypeList = selected_servicetypelist.Split(',').Select(int.Parse).ToList();
            }
            int creCount = newWyzId.Count;
            bool isAssignDone = false;
            List<change_assignment_records> change_Assign = new List<change_assignment_records>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    db.Database.CommandTimeout = 600;

                    if (isAllSelected == true || AssignCalls != 0)
                    {
                        dynamic data = JsonConvert.DeserializeObject(filtData);
                        string from_date = Convert.ToDateTime(data.fromdate).ToString("yyyy-MM-dd");
                        string to_date = Convert.ToDateTime(data.todate).ToString("yyyy-MM-dd");

                        long totalSize = 0;

                        if (AssignCalls == 0)
                        {
                            string str = @"call change_assignment_list_count(@users,@dispostion,@fromdate,@todate,@campaign_type,@campaign_name,@inloc,@inservicetype,@fromfollowupdate,@tofollowupdate,@bookingFromdate,@bookingTodate,@isEditedDate)";


                            MySqlParameter[] paramter = new MySqlParameter[]
                            {
                            new MySqlParameter("users", data.selected_cres),
                            new MySqlParameter("dispostion", data.selected_dispositions),
                            new MySqlParameter("fromdate", from_date),
                            new MySqlParameter("todate", to_date),
                            new MySqlParameter("campaign_type", campSelecType),
                            new MySqlParameter("campaign_name", data.campSelec),
                            new MySqlParameter("inloc", data.workshopNames),
                            new MySqlParameter("inservicetype", data.selected_servicetypelist),
                            new MySqlParameter("fromfollowupdate", folowupfromdate),
                            new MySqlParameter("tofollowupdate", followuptodateTo),
                            new MySqlParameter("bookingFromdate",bookingFromdate),
                            new MySqlParameter("bookingTodate",bookingTodate),
                            new MySqlParameter("isEditedDate",isEditedDate)
                            };

                            totalSize = db.Database.SqlQuery<long>(str, paramter).FirstOrDefault();
                        }
                        else
                        {
                            totalSize = AssignCalls;
                        }




                        List<ChangeAssignment> assignments = new List<ChangeAssignment>();
                        string campSelec = data.campSelec, selected_cres = data.selected_cres, selected_dispositions = data.selected_dispositions, loc = data.workshopNames;

                        assignments = getCallListToChange(campSelecType, campSelec, from_date, to_date, selected_cres, selected_dispositions, loc, 0, totalSize, selected_servicetypelist, folowupfromdate, followuptodateTo, bookingFromdate, bookingTodate);
                        InteractionCallId.AddRange(assignments.Select(m => m.id));
                        serviceBookedIdList.AddRange(assignments.Select(m => m.servicebookedId));

                        if (campSelecType == "Campaign")
                        {
                            //if (!string.IsNullOrEmpty(selected_servicetypelist))
                            //{
                            serviceTypeList.AddRange(assignments.Select(m => int.Parse(m.nextServiceTypeId)));
                            //}
                        }


                        //assignCampList.AddRange(assignments.Select(m=>m.campaign_id));
                    }
                    else
                    {
                        campIdList = selected_camp.Split(',').ToList();

                        for (int i = 0; i < campIdList.Count(); i += 4)
                        {
                            InteractionCallId.Add(long.Parse(campIdList[i]));

                            if ((i + 2) < campIdList.Count())
                            {
                                serviceBookedIdList.Add(long.Parse(campIdList[(i + 2)]));
                            }

                            if (campSelecType == "Campaign")
                            {
                                if ((i + 3) < campIdList.Count())
                                {
                                    serviceTypeList.Add(int.Parse(campIdList[(i + 3)]));
                                }
                            }

                        }
                    }

                    if (InteractionCallId.Count > 0)
                    {
                        if (campSelecType == "Campaign")
                        {
                            List<changeAssignModel> changeModel = new List<changeAssignModel>();

                            for(int i=0; i<InteractionCallId.Count(); i++)
                            {
                                changeAssignModel changeAssign = new changeAssignModel();
                                changeAssign.interactionId = InteractionCallId[i];
                                changeAssign.servicebookedId = serviceBookedIdList[i];
                                changeAssign.serviceTypeId = serviceTypeList[i];
                                changeModel.Add(changeAssign);

                            }

                            for (int j = 0; j < selectedServiceTypeList.Count(); j++)
                            {
                                List<changeAssignModel> changeAssignModel = new List<changeAssignModel>();
                                changeAssignModel = changeModel.Where(x => x.serviceTypeId == selectedServiceTypeList[j]).ToList();
                                for(int i=0;i< changeAssignModel.Count; i++)
                                {
                                    long id = changeAssignModel[i].interactionId;
                                    changeAssignmentPending CAP = new changeAssignmentPending();
                                    CAP.assignedInteractionId = id;
                                    CAP.updatedById = long.Parse(Session["UserId"].ToString());
                                    if (campSelecType == "Campaign")
                                    {
                                        CAP.moduleType = 1;
                                    }
                                    CAP.updatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                                    CAP.updatedStatus = false;
                                    CAP.uploadId = 0;
                                    CAP.serviceBookedId = changeAssignModel[i].servicebookedId;
                                    if (i == 0)
                                    {
                                        CAP.newWyzuserId = newWyzId[i];
                                    }
                                    else
                                    {
                                        int wyzPos = i % creCount;
                                        CAP.newWyzuserId = newWyzId[wyzPos];
                                    }
                                    db.ChangeAssignmentPendings.Add(CAP);
                                    db.SaveChanges();
                                    isAssignDone = true;
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < InteractionCallId.Count(); i++)
                            {
                                long id = InteractionCallId[i];
                                changeAssignmentPending CAP = new changeAssignmentPending();
                                CAP.assignedInteractionId = id;
                                CAP.updatedById = long.Parse(Session["UserId"].ToString());
                                if (campSelecType == "Insurance")
                                {
                                    CAP.moduleType = 2;
                                }
                                else if (campSelecType == "PSF")
                                {
                                    CAP.moduleType = 4;
                                }
                                CAP.updatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                                CAP.updatedStatus = false;
                                CAP.uploadId = 0;
                                CAP.serviceBookedId = serviceBookedIdList[i];
                                if (i == 0)
                                {
                                    CAP.newWyzuserId = newWyzId[i];
                                }
                                else
                                {
                                    int wyzPos = i % creCount;
                                    CAP.newWyzuserId = newWyzId[wyzPos];
                                }
                                db.ChangeAssignmentPendings.Add(CAP);
                                db.SaveChanges();
                                isAssignDone = true;
                            }
                        }

                    }
                    else
                    {
                        return Json(new { success = false, error = "No CRE Selected" });
                    }
                }
                return Json(new { success = isAssignDone });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message.ToString() });
            }
            //return Json(new { success = false,error="Something went wrong" });
        }
        //public ActionResult changeUpload()
        //{
        //    using (AutoSherDBContext db = new AutoSherDBContext())
        //    {
        //        List<change_assignment_records> change_Assign = new List<change_assignment_records>();
        //        List<changeAssignmentPending> cap = db.ChangeAssignmentPendings.Where(x => x.updatedStatus == false).ToList();
        //        foreach (var item in cap)
        //        {
        //            try
        //            {
        //                if (item.moduleType == 1)
        //                {
        //                    assignedinteraction assignInter = db.assignedinteractions.FirstOrDefault(m => m.id == item.assignedInteractionId);
        //                    if (assignInter != null)
        //                    {
        //                        change_assignment_records changeAssignRecord = new change_assignment_records();
        //                        changeAssignRecord.assignedinteraction_id = assignInter.id;
        //                        changeAssignRecord.campaign_id = assignInter.campaign_id ?? default(long);
        //                        changeAssignRecord.last_wyzuserId = assignInter.wyzUser_id ?? default(long);

        //                        long wyzuserId = item.newWyzuserId;
        //                        changeAssignRecord.new_wyzuserId = wyzuserId;
        //                        long wyzId = wyzuserId;
        //                        assignInter.wyzUser_id = wyzuserId; //updation
        //                        if (item.serviceBookedId != 0)
        //                        {
        //                            long serviceId = item.serviceBookedId;
        //                            servicebooked service = db.servicebookeds.FirstOrDefault(m => m.serviceBookedId == serviceId);
        //                            service.chaser_id = wyzuserId;
        //                            db.servicebookeds.AddOrUpdate(service);
        //                        }
        //                        changeAssignRecord.updatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
        //                        changeAssignRecord.moduletypeIs = 1;
        //                        changeAssignRecord.assigned_manager_id = item.updatedById;
        //                        changeAssignRecord.updatedType = item.uploadId != 0 ? "Upload" : "By Admin";
        //                        item.updatedStatus = true;
        //                        db.change_assignment_records.Add(changeAssignRecord);
        //                        db.assignedinteractions.AddOrUpdate(assignInter);
        //                    }
        //                    else
        //                    {
        //                        item.updatedStatus = true;
        //                    }
        //                    db.SaveChanges();
        //                }
        //                else if (item.moduleType == 2)
        //                {
        //                    insuranceassignedinteraction assignInter = db.insuranceassignedinteractions.FirstOrDefault(m => m.id == item.assignedInteractionId);
        //                    if (assignInter != null)
        //                    {
        //                        change_assignment_records changeAssignRecord = new change_assignment_records();
        //                        changeAssignRecord.assignedinteraction_id = assignInter.id;
        //                        changeAssignRecord.campaign_id = assignInter.campaign_id ?? default(long);
        //                        changeAssignRecord.last_wyzuserId = assignInter.wyzUser_id ?? default(long);

        //                        long wyzuserId = item.newWyzuserId;
        //                        changeAssignRecord.new_wyzuserId = wyzuserId;
        //                        assignInter.wyzUser_id = wyzuserId;
        //                        //if (item.serviceBookedId != 0)
        //                        //{
        //                        //    long serviceId = item.serviceBookedId;
        //                        //    servicebooked service = db.servicebookeds.FirstOrDefault(m => m.serviceBookedId == serviceId);
        //                        //    service.chaser_id = wyzuserId;
        //                        //    db.servicebookeds.AddOrUpdate(service);
        //                        //    db.SaveChanges();
        //                        //}

        //                        changeAssignRecord.updatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
        //                        changeAssignRecord.moduletypeIs = 2;
        //                        changeAssignRecord.assigned_manager_id = item.updatedById;
        //                        changeAssignRecord.updatedType = item.uploadId != 0 ? "Upload" : "By Admin";
        //                        item.updatedStatus = true;
        //                        db.change_assignment_records.Add(changeAssignRecord);
        //                        db.insuranceassignedinteractions.AddOrUpdate(assignInter);
        //                    }
        //                    else
        //                    {
        //                        item.updatedStatus = true;
        //                    }
        //                    db.SaveChanges();
        //                }
        //                else if (item.moduleType == 4)
        //                {
        //                    psfassignedinteraction assignInter = db.psfassignedinteractions.FirstOrDefault(m => m.id == item.assignedInteractionId);
        //                    if (assignInter != null)
        //                    {
        //                        change_assignment_records changeAssignRecord = new change_assignment_records();
        //                        changeAssignRecord.assignedinteraction_id = assignInter.id;
        //                        changeAssignRecord.campaign_id = assignInter.campaign_id ?? default(long);
        //                        changeAssignRecord.last_wyzuserId = assignInter.wyzUser_id ?? default(long);

        //                        changeAssignRecord.new_wyzuserId = item.newWyzuserId;
        //                        long wyzId = item.newWyzuserId;

        //                        assignInter.wyzUser_id = wyzId;

        //                        //if (item.serviceBookedId != 0)
        //                        //{
        //                        //    long serviceId = item.serviceBookedId;
        //                        //    servicebooked service = db.servicebookeds.FirstOrDefault(m => m.serviceBookedId == serviceId);
        //                        //    service.chaser_id = wyzId;
        //                        //    db.servicebookeds.AddOrUpdate(service);
        //                        //    db.SaveChanges();
        //                        //}

        //                        changeAssignRecord.updatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
        //                        changeAssignRecord.moduletypeIs = 4;
        //                        changeAssignRecord.assigned_manager_id = item.updatedById;
        //                        changeAssignRecord.updatedType = item.uploadId != 0 ? "Upload" : "By Admin";
        //                        item.updatedStatus = true;
        //                        db.change_assignment_records.Add(changeAssignRecord);
        //                        db.psfassignedinteractions.AddOrUpdate(assignInter);
        //                    }
        //                    else
        //                    {
        //                        item.updatedStatus = true;
        //                    }
        //                    db.SaveChanges();
        //                }
        //                //item.updatedStatus = true;
        //            }
        //            catch (Exception ex)
        //            {

        //            }
        //        }
        //    }
        //    return View();
        //}
    }

    class changeAssignModel
    {
        public long interactionId { get; set; }
        public long servicebookedId { get; set; }
        public int serviceTypeId { get; set; }
    }
}