using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoSherpa_project.Models;
using Newtonsoft.Json;
using AutoSherpa_project.Models.ViewModels;
using System.Text.RegularExpressions;
using System.Data;
using OfficeOpenXml;
using LiteDB;
using NLog;
using System.Data.Entity.Migrations;

namespace AutoSherpa_project.Controllers
{
    [AuthorizeFilter]
    public class AddCREController : Controller
    {
        // GET: AddCRE
        public ActionResult AddCRE()
        {
            using (AutoSherDBContext db = new AutoSherDBContext())
            {
                ViewBag.Locations = db.locations.Select(x => new { id = x.cityId, name = x.name }).ToList();
                ViewBag.Workshop = db.workshops.Select(x => new { id = x.id, name = x.workshopName }).ToList();
                if (Session["LoginUser"].ToString() == "Insurance")
                {
                    ViewBag.CreManagers = db.wyzusers.Where(x => x.insuranceRole == true && x.role == "CREManager").Select(x => new { name = x.userName }).Distinct().ToList();
                }
                else if (Session["LoginUser"].ToString() == "Service")
                {
                    ViewBag.CreManagers = db.wyzusers.Where(x => x.insuranceRole == false && x.role == "CREManager").Select(x => new { name = x.userName }).Distinct().ToList();
                    ViewBag.PSFCreManagers = db.wyzusers.Where(x => x.role1 == "4" && x.role == "CREManager").Select(x => new { name = x.userName }).Distinct().ToList();

                }

                ViewBag.CRECampaigns = db.campaigns.Where(x => x.isactive == true && x.campaignType == "Campaign").Select(x => new { id = x.id, name = x.campaignName }).ToList();
                ViewBag.CREServiceTypes = db.forecastlogicservicetypes.Where(s=> !s.inactive).Select(x => new { id = x.id, name = x.servicetype }).ToList();
                ViewBag.CREROAgeingList = db.ROAge.Select(x => new { id = x.ROAgeValue, name = x.ROAgeName }).OrderBy(s => s.id).ToList();
                ViewBag.CRECategories = db.Modelcategories.Select(x => new { id = x.modelcatid, name = x.modelcat }).OrderBy(s => s.id).ToList();
                ViewBag.CREFieldOneList = db.CREFieldOne.Select(x => new { id = x.FieldOneId, name = x.FieldOneName }).ToList();
                ViewBag.CREFieldTwoList = db.CREFieldTwo.Select(x => new { id = x.FieldTwoId, name = x.FieldTwoName }).ToList();
                ViewBag.GSMIP = db.tenants.Select(x => new { id = x.id, name = x.gsmip }).ToList();
            }

            return View();
        }
        public ActionResult submitData(string values)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<addCreVM>(values);
                string phoneNum = "";
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    if (data != null)
                    {
                        long? tenantId = null;
                        string creManagerName = Session["UserName"].ToString();
                        string dealerCode = Session["DealerCode"].ToString();
                        string userId = Session["UserId"].ToString();
                        if (data.callingType == "gsm")
                        {
                            if (data.gsmip != "")
                            {
                                tenantId = long.Parse(data.gsmip);
                            }
                        }
                        else if (data.callingType == "android")
                        {
                            tenantId = 1;
                        }
                        var dealerData = db.dealers.FirstOrDefault(x => x.dealerCode == dealerCode);

                        //updating to wyzuser table

                        wyzuser wyzuser = new wyzuser();

                        if (data.role == "Admin" || data.role == "RM" || data.role == "WM")
                        {
                            wyzuser.creManager = null;
                        }
                        else if (data.role == "Service Advisor" || data.role == "CRE")
                        {
                            wyzuser.creManager = data.creManager;
                        }
                        else if (data.role == "CREManager")
                        {
                            wyzuser.creManager = null;
                        }
                        wyzuser.firstName = data.creName;
                        wyzuser.userName = data.userName;
                        wyzuser.password = data.password;
                        wyzuser.location_cityId = long.Parse(data.firstLocation);
                        wyzuser.workshop_id = long.Parse(data.firstWorkshop);
                        wyzuser.role = data.role;
                        if (data.role == "Service Advisor")
                        {
                            wyzuser.role1 = "1";
                        }
                        else
                        {
                            wyzuser.role1 = data.moduleType;
                        }

                        if (data.phoneNumber != "")
                        {
                            if (data.phoneNumber.StartsWith("+91"))
                            {
                                if (data.callingType == "gsm")
                                {
                                    wyzuser.gsmPhonenumber = data.phoneNumber;
                                    wyzuser.phoneNumber = "XXXXXXXXXX";
                                }
                                else if (data.callingType == "android")
                                {
                                    wyzuser.phoneNumber = data.phoneNumber;
                                    wyzuser.gsmPhonenumber = "XXXXXXXXXX";
                                }

                            }
                            else
                            {
                                if (data.callingType == "gsm")
                                {
                                    wyzuser.gsmPhonenumber = "+91" + data.phoneNumber;
                                    wyzuser.phoneNumber = "XXXXXXXXXX";
                                }
                                else if (data.callingType == "android")
                                {
                                    wyzuser.phoneNumber = "+91" + data.phoneNumber;
                                    wyzuser.gsmPhonenumber = "XXXXXXXXXX";
                                }
                            }
                        }
                        else
                        {
                            wyzuser.phoneNumber = "XXXXXXXXXX";
                            wyzuser.gsmPhonenumber = "XXXXXXXXXX";
                        }
                        wyzuser.phoneIMEINo = data.IMEI;
                        wyzuser.dealerCode = dealerData.dealerCode;
                        wyzuser.dealer_id = dealerData.id;
                        wyzuser.dealerId = dealerData.dealerId;
                        wyzuser.dealerName = dealerData.dealerName;
                        wyzuser.tenant_id = tenantId;
                        if (data.sipExtensionId != "" && data.sipID != "")
                        {
                            var gsmRedId = db.Sipregistrationids.FirstOrDefault(x => x.tenant_id == tenantId && x.sipextension == data.sipExtensionId).gsmregistrationid;
                            wyzuser.gsmRegistrationId = gsmRedId; /*data.sipExtensionId;*/
                            wyzuser.extensionId = data.sipID;
                        }
                        if (data.moduleType == "1" || data.moduleType == "4")
                        {
                            wyzuser.insuranceRole = false;
                        }
                        else if (data.moduleType == "2")
                        {
                            wyzuser.insuranceRole = true;
                        }
                        wyzuser.updatedBy = long.Parse(userId);
                        wyzuser.updatedOn = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                        db.wyzusers.Add(wyzuser);
                        db.SaveChanges();
                        var id = wyzuser.id;
                        // comment changes has been done by rohan on 22042023 to avoid insersen for insurance 
                        //if (data.role == "CRE"){
                        if (data.role == "CRE" && data.moduleType == "1")
                        {
                            CREUserAdditionalFieldsMapper userAdditionalFieldMapper = new CREUserAdditionalFieldsMapper();
                            userAdditionalFieldMapper.CREUserId = Convert.ToInt32(id);
                            userAdditionalFieldMapper.CampaignId = data.CRECampaignId;
                            userAdditionalFieldMapper.ServiceTypeId = data.CREServiceTypeId;
                            userAdditionalFieldMapper.ROAgeingId = data.CREROAgeingId;
                            userAdditionalFieldMapper.CategoryId = data.CRECategoryId;
                            userAdditionalFieldMapper.FieldOneId = data.CREFieldOneId;
                            userAdditionalFieldMapper.FieldTwoId = data.CREFieldTwoId;
                            db.CREUserAdditionalFieldsMapper.Add(userAdditionalFieldMapper);
                            db.SaveChanges();
                        }


                        //updating to user location table creManager

                        List<userlocation> loc = new List<userlocation>();
                        int[] locationList = Array.ConvertAll(data.locations.Split(','), int.Parse);
                        for (int i = 1; i < locationList.Length; i++)
                        {
                            userlocation userlocation = new userlocation();
                            userlocation.userLocation_id = id;
                            userlocation.locationList_cityId = locationList[i];
                            loc.Add(userlocation);
                        }
                        db.userlocations.AddRange(loc);

                        //updating to user roles workshop table

                        List<userworkshop> work = new List<userworkshop>();
                        int[] workshopList = Array.ConvertAll(data.workshops.Split(','), int.Parse);
                        for (int i = 1; i < workshopList.Length; i++)
                        {
                            userworkshop userworkshop = new userworkshop();
                            userworkshop.userWorkshop_id = id;
                            userworkshop.workshopList_id = workshopList[i];
                            work.Add(userworkshop);
                        }
                        db.userworkshops.AddRange(work);

                        //updating to Service Advisory table

                        if (data.role == "Service Advisor")
                        {
                            serviceadvisor SA = new serviceadvisor();
                            SA.advisorName = data.userName;
                            SA.advisorNumber = "XXXXXXXXXX";   //?????????????????????????????????????
                            SA.capacityPerDay = 250;  //????????????????????????????????????????????????
                            SA.isActive = true;
                            // SA.workshop_id=       ????????????????????????????????????????????????????
                            SA.wyzUser_id = id;
                            SA.priority = 1;
                            SA.upload_id = null;
                        }

                        //updating in role table[it is happening due to trigger, when we save changes in wyzuser table]
                        //List<userrole> userrole = new List<userrole>();
                        // int[] roleId = { 2, 17 };
                        // foreach(var item in roleId)
                        // {
                        //     userrole user = new userrole();
                        //     user.users_id = id;
                        //     user.roles_id = item;
                        //     userrole.Add(user);
                        // }
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                string exception = "";
                if (ex.Message.Contains("inner exception"))
                {
                    exception = ex.InnerException.Message;
                }
                else
                {
                    exception = ex.Message;
                }
                return Json(new { success = false, error = exception });
            }
            return Json(new { success = true });
        }

        #region Display CRE
        public ActionResult getcreDetails(string roleType, string managertUsername, string isavailable, DataTablesParam param)
        {
            string listworkshopNames = string.Empty;
            string listlocationnames = string.Empty;
            string managerName = string.Empty;
            string managerPassword = string.Empty;
            string managerPhone = string.Empty;
            List<creTableVM> addCreList = new List<creTableVM>();
            List<wyzuser> CreList = new List<wyzuser>();
            List<workshop> workshopLists = new List<workshop>();
            List<location> locationLists = new List<location>();
            List<userworkshop> userworkshopLists = new List<userworkshop>();
            List<userlocation> userlocationLists = new List<userlocation>();
            string username = Session["UserName"].ToString();
            string userRole = Session["UserRole"].ToString();
            string userRole1 = Session["UserRole1"].ToString();
            long userId = Convert.ToInt64(Session["UserId"]);
            bool unavailable = Convert.ToBoolean(isavailable);
            int pageNo = 1;
            int totalCount = 0;
            if (param.iDisplayStart >= param.iDisplayLength)
            {
                pageNo = (param.iDisplayStart / param.iDisplayLength) + 1;
            }
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    userlocationLists = db.userlocations.ToList();
                    locationLists = db.locations.ToList();
                    userworkshopLists = db.userworkshops.ToList();
                    workshopLists = db.workshops.ToList();


                    if (param.sSearch != null)
                    {
                        if (roleType == "CRE")
                        {
                            if (userRole == "CREManager")
                            {
                                addCreList = db.wyzusers.Where(m => m.creManager == username && m.unAvailable == unavailable && m.role1 == userRole1 && m.role == "CRE" && (m.userName.Contains(param.sSearch) || m.phoneNumber.Contains(param.sSearch) || m.extensionId.Contains(param.sSearch))).OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new creTableVM
                                {
                                    creName = p.firstName,
                                    phoneNumber = p.phoneNumber,
                                    IMEI = p.phoneIMEINo,
                                    userName = p.userName,
                                    passWord = p.password,
                                    wyzId = p.id,
                                    location_cityId = p.location_cityId,
                                    workshop_id = p.workshop_id,
                                    creManager = p.creManager,
                                    extensionId = p.extensionId
                                }).OrderByDescending(p => p.creName).ToList();
                                totalCount = db.wyzusers.Count(m => m.creManager == username && m.unAvailable == unavailable && m.role1 == userRole1 && m.role == "CRE" && (m.userName.Contains(param.sSearch) || m.phoneNumber.Contains(param.sSearch) || m.extensionId.Contains(param.sSearch)));
                            }
                            else
                            {
                                addCreList = db.wyzusers.Where(m => m.unAvailable == unavailable && m.role1 == userRole1 && m.role == "CRE" && (m.userName.Contains(param.sSearch) || m.phoneNumber.Contains(param.sSearch) || m.extensionId.Contains(param.sSearch))).OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength)
                                    .Join(db.CREUserAdditionalFieldsMapper, p => (int)(p.id), ut => ut.CREUserId, (p, ut) =>
                                     new creTableVM
                                     {
                                         creName = p.firstName,
                                         phoneNumber = p.phoneNumber,
                                         IMEI = p.phoneIMEINo,
                                         userName = p.userName,
                                         passWord = p.password,
                                         wyzId = p.id,
                                         location_cityId = p.location_cityId,
                                         workshop_id = p.workshop_id,
                                         creManager = p.creManager,
                                         extensionId = p.extensionId,
                                         CRECampaignId = ut.CampaignId,
                                         CREServiceTypeId = ut.ServiceTypeId,
                                         CREROAgeingId = ut.ROAgeingId,
                                         CRECategoryId = ut.CategoryId,
                                         CREFieldOneId = ut.FieldOneId,
                                         CREFieldTwoId = ut.FieldTwoId
                                     }).OrderByDescending(p => p.creName).ToList();

                                totalCount = db.wyzusers.Count(m => m.unAvailable == unavailable && m.role == "CRE" && (m.userName.Contains(param.sSearch) || m.phoneNumber.Contains(param.sSearch) || m.extensionId.Contains(param.sSearch)));
                            }

                        }
                        else if (roleType == "CREManager")
                        {
                            var crePassDetails = db.wyzusers.Where(m => m.userName == managertUsername).Select(m => new { m.userName, m.password, m.phoneNumber }).FirstOrDefault();
                            managerName = crePassDetails.userName;
                            managerPassword = crePassDetails.password;
                            managerPhone = crePassDetails.phoneNumber;

                            addCreList = db.wyzusers.Where(m => m.creManager == managertUsername && m.unAvailable == unavailable && m.role == "CRE" && m.role1 == userRole1 && (m.userName.Contains(param.sSearch) || m.phoneNumber.Contains(param.sSearch) || m.extensionId.Contains(param.sSearch))).OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new creTableVM
                            {
                                creName = p.firstName,
                                phoneNumber = p.phoneNumber,
                                IMEI = p.phoneIMEINo,
                                userName = p.userName,
                                passWord = p.password,
                                wyzId = p.id,
                                location_cityId = p.location_cityId,
                                workshop_id = p.workshop_id,
                                creManager = p.creManager,
                                extensionId = p.extensionId
                            }).OrderByDescending(p => p.creName).ToList();
                            totalCount = db.wyzusers.Count(m => m.creManager == managertUsername && m.role == "CRE" && m.unAvailable == unavailable && m.role1 == userRole1 && (m.userName.Contains(param.sSearch) || m.phoneNumber.Contains(param.sSearch) || m.extensionId.Contains(param.sSearch)));

                        }
                        else if (roleType == "ServiceAdvisor")
                        {
                            if (userRole == "CREManager")
                            {
                                addCreList = db.wyzusers.Where(m => m.creManager == username && m.unAvailable == unavailable && m.role1 == userRole1 && m.role == "Service Advisor" && (m.userName.Contains(param.sSearch) || m.phoneNumber.Contains(param.sSearch) || m.extensionId.Contains(param.sSearch))).OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new creTableVM
                                {
                                    creName = p.firstName,
                                    phoneNumber = p.phoneNumber,
                                    IMEI = p.phoneIMEINo,
                                    userName = p.userName,
                                    passWord = p.password,
                                    wyzId = p.id,
                                    location_cityId = p.location_cityId,
                                    workshop_id = p.workshop_id,
                                    creManager = p.creManager,
                                    extensionId = p.extensionId
                                }).OrderByDescending(p => p.creName).ToList();
                                totalCount = db.wyzusers.Count(m => m.creManager == username && m.unAvailable == unavailable && m.role1 == userRole1 && m.role == "Service Advisor" && (m.userName.Contains(param.sSearch) || m.phoneNumber.Contains(param.sSearch) || m.extensionId.Contains(param.sSearch)));
                            }
                            else
                            {
                                addCreList = db.wyzusers.Where(m => m.unAvailable == unavailable && m.role1 == userRole1 && m.role == "Service Advisor" && (m.userName.Contains(param.sSearch) || m.phoneNumber.Contains(param.sSearch) || m.extensionId.Contains(param.sSearch))).OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new creTableVM
                                {
                                    creName = p.firstName,
                                    phoneNumber = p.phoneNumber,
                                    IMEI = p.phoneIMEINo,
                                    userName = p.userName,
                                    passWord = p.password,
                                    wyzId = p.id,
                                    location_cityId = p.location_cityId,
                                    workshop_id = p.workshop_id,
                                    creManager = p.creManager,
                                    extensionId = p.extensionId
                                }).OrderByDescending(p => p.creName).ToList();
                                totalCount = db.wyzusers.Count(m => m.unAvailable == unavailable && m.role1 == userRole1 && m.role == "Service Advisor" && (m.userName.Contains(param.sSearch) || m.phoneNumber.Contains(param.sSearch) || m.extensionId.Contains(param.sSearch)));
                            }

                        }

                        if (roleType == "PSFCRE")
                        {
                            if (userRole == "PSFCREManager")
                            {

                                addCreList = db.wyzusers.Where(m => m.creManager == username && m.unAvailable == unavailable && m.role1 == "4" && m.role == "CRE" && (m.userName.Contains(param.sSearch) || m.phoneNumber.Contains(param.sSearch) || m.extensionId.Contains(param.sSearch))).OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new creTableVM
                                {
                                    creName = p.firstName,
                                    phoneNumber = p.phoneNumber,
                                    IMEI = p.phoneIMEINo,
                                    userName = p.userName,
                                    passWord = p.password,
                                    wyzId = p.id,
                                    location_cityId = p.location_cityId,
                                    workshop_id = p.workshop_id,
                                    creManager = p.creManager,
                                    extensionId = p.extensionId
                                }).OrderByDescending(p => p.creName).ToList();
                                totalCount = db.wyzusers.Count(m => m.creManager == username && m.unAvailable == unavailable && m.role1 == "4" && m.role == "CRE" && (m.userName.Contains(param.sSearch) || m.phoneNumber.Contains(param.sSearch) || m.extensionId.Contains(param.sSearch)));
                            }
                            else
                            {
                                List<long> complaintmanagerIds = db.userroles.Where(m => m.roles_id == 40).Select(m => m.users_id).ToList();

                                addCreList = db.wyzusers.Where(m => m.unAvailable == unavailable && m.role1 == "4" && m.role == "CRE" && (!complaintmanagerIds.Contains(m.id)) && (m.userName.Contains(param.sSearch) || m.phoneNumber.Contains(param.sSearch) || m.extensionId.Contains(param.sSearch))).OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new creTableVM
                                {
                                    creName = p.firstName,
                                    phoneNumber = p.phoneNumber,
                                    IMEI = p.phoneIMEINo,
                                    userName = p.userName,
                                    passWord = p.password,
                                    wyzId = p.id,
                                    location_cityId = p.location_cityId,
                                    workshop_id = p.workshop_id,
                                    creManager = p.creManager,
                                    extensionId = p.extensionId
                                }).OrderByDescending(p => p.creName).ToList();
                                totalCount = db.wyzusers.Count(m => m.unAvailable == unavailable && m.role1 == "4" && m.role == "CRE" && (!complaintmanagerIds.Contains(m.id)) && (m.userName.Contains(param.sSearch) || m.phoneNumber.Contains(param.sSearch) || m.extensionId.Contains(param.sSearch)));
                            }

                        }
                        else if (roleType == "PSFCREManager")
                        {
                            var crePassDetails = db.wyzusers.Where(m => m.userName == managertUsername).Select(m => new { m.userName, m.password, m.phoneNumber }).FirstOrDefault();
                            managerName = crePassDetails.userName;
                            managerPassword = crePassDetails.password;
                            managerPhone = crePassDetails.phoneNumber;

                            addCreList = db.wyzusers.Where(m => m.creManager == managertUsername && m.unAvailable == unavailable && m.role == "CRE" && m.role1 == "4" && (m.userName.Contains(param.sSearch) || m.phoneNumber.Contains(param.sSearch) || m.extensionId.Contains(param.sSearch))).OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new creTableVM
                            {
                                creName = p.firstName,
                                phoneNumber = p.phoneNumber,
                                IMEI = p.phoneIMEINo,
                                userName = p.userName,
                                passWord = p.password,
                                wyzId = p.id,
                                location_cityId = p.location_cityId,
                                workshop_id = p.workshop_id,
                                creManager = p.creManager,
                                extensionId = p.extensionId
                            }).OrderByDescending(p => p.creName).ToList();
                            totalCount = db.wyzusers.Count(m => m.creManager == managertUsername && m.role == "CRE" && m.unAvailable == unavailable && m.role1 == "4" && (m.userName.Contains(param.sSearch) || m.phoneNumber.Contains(param.sSearch) || m.extensionId.Contains(param.sSearch)));

                        }
                        else if (roleType == "PSFComplaintCre")
                        {
                            List<long> complaintmanagerIds = db.userroles.Where(m => m.roles_id == 40).Select(m => m.users_id).ToList();

                            if (userRole == "PSFCREManager")
                            {
                                //    select id, userName from wyzuser where id in(select users_id from userroles where roles_id = 40);



                                addCreList = db.wyzusers.Where(m => m.creManager == username && m.unAvailable == unavailable && complaintmanagerIds.Contains(m.id) && (m.userName.Contains(param.sSearch) || m.phoneNumber.Contains(param.sSearch) || m.extensionId.Contains(param.sSearch))).OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new creTableVM
                                {
                                    creName = p.firstName,
                                    phoneNumber = p.phoneNumber,
                                    IMEI = p.phoneIMEINo,
                                    userName = p.userName,
                                    passWord = p.password,
                                    wyzId = p.id,
                                    location_cityId = p.location_cityId,
                                    workshop_id = p.workshop_id,
                                    creManager = p.creManager,
                                    extensionId = p.extensionId
                                }).OrderByDescending(p => p.creName).ToList();
                                totalCount = db.wyzusers.Count(m => m.creManager == username && m.unAvailable == unavailable && complaintmanagerIds.Contains(m.id) && (m.userName.Contains(param.sSearch) || m.phoneNumber.Contains(param.sSearch) || m.extensionId.Contains(param.sSearch)));
                            }
                            else
                            {
                                addCreList = db.wyzusers.Where(m => m.unAvailable == unavailable && complaintmanagerIds.Contains(m.id) && (m.userName.Contains(param.sSearch) || m.phoneNumber.Contains(param.sSearch) || m.extensionId.Contains(param.sSearch))).OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new creTableVM
                                {
                                    creName = p.firstName,
                                    phoneNumber = p.phoneNumber,
                                    IMEI = p.phoneIMEINo,
                                    userName = p.userName,
                                    passWord = p.password,
                                    wyzId = p.id,
                                    location_cityId = p.location_cityId,
                                    workshop_id = p.workshop_id,
                                    creManager = p.creManager,
                                    extensionId = p.extensionId
                                }).OrderByDescending(p => p.creName).ToList();
                                totalCount = db.wyzusers.Count(m => m.unAvailable == unavailable && complaintmanagerIds.Contains(m.id) && (m.userName.Contains(param.sSearch) || m.phoneNumber.Contains(param.sSearch) || m.extensionId.Contains(param.sSearch)));
                            }

                        }
                    }
                    else
                    {
                        if (roleType == "CRE")
                        {
                            if (userRole == "CREManager")
                            {
                                addCreList = db.wyzusers.Where(m => m.creManager == username && m.unAvailable == unavailable && m.role1 == userRole1 && m.role == "CRE").OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new creTableVM
                                {
                                    creName = p.firstName,
                                    phoneNumber = p.phoneNumber,
                                    IMEI = p.phoneIMEINo,
                                    userName = p.userName,
                                    passWord = p.password,
                                    wyzId = p.id,
                                    location_cityId = p.location_cityId,
                                    workshop_id = p.workshop_id,
                                    creManager = p.creManager,
                                    extensionId = p.extensionId
                                }).OrderByDescending(p => p.creName).ToList();
                                totalCount = db.wyzusers.Count(m => m.creManager == username && m.unAvailable == unavailable && m.role1 == userRole1 && m.role == "CRE");
                            }
                            else
                            {

                                addCreList = db.wyzusers.Where(m => m.unAvailable == unavailable && m.role1 == userRole1 && m.role == "CRE").OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength)
                                .GroupJoin(db.CREUserAdditionalFieldsMapper, p => p.id, ut => (long)ut.CREUserId, (p, ut) => new { user = p, mappings = ut })
                                .SelectMany(o => o.mappings.DefaultIfEmpty(), (p, ut) => new creTableVM
                                {
                                    creName = p.user.firstName,
                                    phoneNumber = p.user.phoneNumber,
                                    IMEI = p.user.phoneIMEINo,
                                    userName = p.user.userName,
                                    passWord = p.user.password,
                                    wyzId = p.user.id,
                                    location_cityId = p.user.location_cityId,
                                    workshop_id = p.user.workshop_id,
                                    creManager = p.user.creManager,
                                    extensionId = p.user.extensionId,
                                    CRECampaignId = ut.CampaignId,
                                    CREServiceTypeId = ut.ServiceTypeId,
                                    CREROAgeingId = ut.ROAgeingId,
                                    CRECategoryId = ut.CategoryId,
                                    CREFieldOneId = ut.FieldOneId,
                                    CREFieldTwoId = ut.FieldTwoId

                                }).OrderByDescending(p => p.creName).ToList();
                                totalCount = db.wyzusers.Count(m => m.unAvailable == unavailable && m.role1 == userRole1 && m.role == "CRE");
                            }

                        }
                        else if (roleType == "CREManager")
                        {
                            var crePassDetails = db.wyzusers.Where(m => m.userName == managertUsername).Select(m => new { m.userName, m.password, m.phoneNumber }).FirstOrDefault();
                            managerName = crePassDetails.userName;
                            managerPassword = crePassDetails.password;
                            managerPhone = crePassDetails.phoneNumber;

                            addCreList = db.wyzusers.Where(m => m.creManager == managertUsername && m.unAvailable == unavailable && m.role == "CRE" && m.role1 == userRole1).OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new creTableVM
                            {
                                creName = p.firstName,
                                phoneNumber = p.phoneNumber,
                                IMEI = p.phoneIMEINo,
                                userName = p.userName,
                                passWord = p.password,
                                wyzId = p.id,
                                location_cityId = p.location_cityId,
                                workshop_id = p.workshop_id,
                                creManager = p.creManager,
                                extensionId = p.extensionId
                            }).OrderByDescending(p => p.creName).ToList();
                            totalCount = db.wyzusers.Count(m => m.creManager == managertUsername && m.role == "CRE" && m.unAvailable == unavailable && m.role1 == userRole1);

                        }
                        else if (roleType == "ServiceAdvisor")
                        {
                            if (userRole == "CREManager")
                            {
                                addCreList = db.wyzusers.Where(m => m.creManager == username && m.unAvailable == unavailable && m.role1 == userRole1 && m.role == "Service Advisor").OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new creTableVM
                                {
                                    creName = p.firstName,
                                    phoneNumber = p.phoneNumber,
                                    IMEI = p.phoneIMEINo,
                                    userName = p.userName,
                                    passWord = p.password,
                                    wyzId = p.id,
                                    location_cityId = p.location_cityId,
                                    workshop_id = p.workshop_id,
                                    creManager = p.creManager,
                                    extensionId = p.extensionId
                                }).OrderByDescending(p => p.creName).ToList();
                                totalCount = db.wyzusers.Count(m => m.creManager == username && m.unAvailable == unavailable && m.role1 == userRole1 && m.role == "Service Advisor");
                            }
                            else
                            {
                                addCreList = db.wyzusers.Where(m => m.unAvailable == unavailable && m.role1 == userRole1 && m.role == "Service Advisor").OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new creTableVM
                                {
                                    creName = p.firstName,
                                    phoneNumber = p.phoneNumber,
                                    IMEI = p.phoneIMEINo,
                                    userName = p.userName,
                                    passWord = p.password,
                                    wyzId = p.id,
                                    location_cityId = p.location_cityId,
                                    workshop_id = p.workshop_id,
                                    creManager = p.creManager,
                                    extensionId = p.extensionId
                                }).OrderByDescending(p => p.creName).ToList();
                                totalCount = db.wyzusers.Count(m => m.unAvailable == unavailable && m.role1 == userRole1 && m.role == "Service Advisor");
                            }

                        }
                        if (roleType == "PSFCRE")
                        {
                            if (userRole == "PSFCREManager")
                            {
                                addCreList = db.wyzusers.Where(m => m.creManager == username && m.unAvailable == unavailable && m.role1 == "4" && m.role == "CRE").OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new creTableVM
                                {
                                    creName = p.firstName,
                                    phoneNumber = p.phoneNumber,
                                    IMEI = p.phoneIMEINo,
                                    userName = p.userName,
                                    passWord = p.password,
                                    wyzId = p.id,
                                    location_cityId = p.location_cityId,
                                    workshop_id = p.workshop_id,
                                    creManager = p.creManager,
                                    extensionId = p.extensionId
                                }).OrderByDescending(p => p.creName).ToList();
                                totalCount = db.wyzusers.Count(m => m.creManager == username && m.unAvailable == unavailable && m.role1 == userRole1 && m.role == "CRE");
                            }
                            else
                            {
                                List<long> complaintmanagerIds = db.userroles.Where(m => m.roles_id == 40).Select(m => m.users_id).ToList();


                                addCreList = db.wyzusers.Where(m => m.unAvailable == unavailable && m.role1 == "4" && m.role == "CRE" && (!complaintmanagerIds.Contains(m.id))).OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new creTableVM
                                {
                                    creName = p.firstName,
                                    phoneNumber = p.phoneNumber,
                                    IMEI = p.phoneIMEINo,
                                    userName = p.userName,
                                    passWord = p.password,
                                    wyzId = p.id,
                                    location_cityId = p.location_cityId,
                                    workshop_id = p.workshop_id,
                                    creManager = p.creManager,
                                    extensionId = p.extensionId
                                }).OrderByDescending(p => p.creName).ToList();
                                totalCount = db.wyzusers.Count(m => m.unAvailable == unavailable && m.role1 == "4" && m.role == "CRE" && (!complaintmanagerIds.Contains(m.id)));
                            }

                        }
                        else if (roleType == "PSFCREManager")
                        {
                            var crePassDetails = db.wyzusers.Where(m => m.userName == managertUsername).Select(m => new { m.userName, m.password, m.phoneNumber }).FirstOrDefault();
                            managerName = crePassDetails.userName;
                            managerPassword = crePassDetails.password;
                            managerPhone = crePassDetails.phoneNumber;

                            addCreList = db.wyzusers.Where(m => m.creManager == managertUsername && m.unAvailable == unavailable && m.role == "CRE" && m.role1 == "4").OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new creTableVM
                            {
                                creName = p.firstName,
                                phoneNumber = p.phoneNumber,
                                IMEI = p.phoneIMEINo,
                                userName = p.userName,
                                passWord = p.password,
                                wyzId = p.id,
                                location_cityId = p.location_cityId,
                                workshop_id = p.workshop_id,
                                creManager = p.creManager,
                                extensionId = p.extensionId
                            }).OrderByDescending(p => p.creName).ToList();
                            totalCount = db.wyzusers.Count(m => m.creManager == managertUsername && m.role == "CRE" && m.unAvailable == unavailable && m.role1 == "4");

                        }
                        else if (roleType == "PSFComplaintCre")
                        {
                            List<long> complaintmanagerIds = db.userroles.Where(m => m.roles_id == 40).Select(m => m.users_id).ToList();

                            if (userRole == "PSFCREManager")
                            {
                                addCreList = db.wyzusers.Where(m => m.creManager == username && m.unAvailable == unavailable && complaintmanagerIds.Contains(m.id)).OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new creTableVM
                                {
                                    creName = p.firstName,
                                    phoneNumber = p.phoneNumber,
                                    IMEI = p.phoneIMEINo,
                                    userName = p.userName,
                                    passWord = p.password,
                                    wyzId = p.id,
                                    location_cityId = p.location_cityId,
                                    workshop_id = p.workshop_id,
                                    creManager = p.creManager,
                                    extensionId = p.extensionId
                                }).OrderByDescending(p => p.creName).ToList();
                                totalCount = db.wyzusers.Count(m => m.creManager == username && m.unAvailable == unavailable && complaintmanagerIds.Contains(m.id));
                            }
                            else
                            {
                                addCreList = db.wyzusers.Where(m => m.unAvailable == unavailable && complaintmanagerIds.Contains(m.id)).OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new creTableVM
                                {
                                    creName = p.firstName,
                                    phoneNumber = p.phoneNumber,
                                    IMEI = p.phoneIMEINo,
                                    userName = p.userName,
                                    passWord = p.password,
                                    wyzId = p.id,
                                    location_cityId = p.location_cityId,
                                    workshop_id = p.workshop_id,
                                    creManager = p.creManager,
                                    extensionId = p.extensionId
                                }).OrderByDescending(p => p.creName).ToList();
                                totalCount = db.wyzusers.Count(m => m.unAvailable == unavailable && complaintmanagerIds.Contains(m.id));
                            }

                        }



                    }

                    if (addCreList.Count != 0)
                    {
                        foreach (var item in addCreList)
                        {
                            List<long> locationIds = userlocationLists.Where(x => x.userLocation_id == item.wyzId).Select(x => x.locationList_cityId).ToList();
                            List<string> locationNames = locationLists.Where(x => locationIds.Contains(x.cityId)).Select(x => x.name).ToList();
                            listlocationnames = string.Join(",", locationNames);
                            item.location = listlocationnames;
                            List<long> workshopIds = db.userworkshops.Where(x => x.userWorkshop_id == item.wyzId).Select(x => x.workshopList_id).ToList();
                            List<string> workshopNames = db.workshops.Where(x => workshopIds.Contains(x.id)).Select(x => x.workshopName).ToList();
                            listworkshopNames = string.Join(",", workshopNames);

                            item.workshop = listworkshopNames;
                            item.workshopIds = string.Join(",", workshopIds); ;

                        }
                    }
                    return Json(new { aaData = addCreList, sEcho = param.sEcho, iTotalDisplayRecords = totalCount, iTotalRecords = totalCount, managerName, managerPassword, managerPhone }, JsonRequestBehavior.AllowGet);

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
                return Json(new { data = "", exception }, JsonRequestBehavior.AllowGet);

            }
        }
        #endregion

        public ActionResult getSIPExtension(string value)
        {
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    long tenantId = long.Parse(value);
                    var data = db.Sipregistrationids.Where(x => x.tenant_id == tenantId).Select(x => new { id = x.tenant_id, name = x.sipextension }).ToList();
                    return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SIPduplication(string value, string tenantId)
        {
            try
            {
                long? tenantIdLong = long.Parse(tenantId);
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    if (value != "" && tenantId != "")
                    {
                        var extensionID = db.wyzusers.Any(x => x.tenant_id == tenantIdLong && x.extensionId == value);
                        if (extensionID)
                        {
                            string username = db.wyzusers.FirstOrDefault(x => x.tenant_id == tenantIdLong && x.extensionId == value).userName;
                            return Json(new { success = false, data = username }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = false, JsonRequestBehavior.AllowGet });
        }

        public ActionResult usernameDuplication(string value)
        {
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    var user = db.wyzusers.Any(x => x.userName == value);
                    if (user)
                    {
                        return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult phoneAndImeiDuplication(string phone, string imei)
        {
            try
            {

                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    bool phoneAndImei;
                    var username = "";

                    if (phone != "XXXXXXXXXX")
                    {
                        if (!phone.StartsWith("+91"))
                        {
                            phone = "+91" + phone;
                        }
                    }


                    if (phone != "" && imei != "" /*&& phone== "XXXXXXXXXX"*/)
                    {
                        phoneAndImei = db.wyzusers.Any(x => x.phoneNumber == phone && x.phoneIMEINo == imei);
                        if (phoneAndImei)
                        {
                            username = db.wyzusers.FirstOrDefault(x => x.phoneNumber == phone && x.phoneIMEINo == imei).userName;
                            return Json(new { success = false, data = username }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    //else if(phone != "" && imei == "")
                    //{
                    //    phoneAndImei = db.wyzusers.Any(x => x.phoneNumber == phone);
                    //}
                    //else if(phone == "" && imei != "")
                    //{
                    //    phoneAndImei = db.wyzusers.Any(x => x.phoneIMEINo == imei);
                    //}



                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { });
        }


        public ActionResult editPhoneAndIMEI(string id, string newPhone, string newImei, string newPass, string extnId, string sipExtension, string gsmipPop, string creManager, string workShop, string CRECampaignId, string CREServiceTypeId, string CREROAgeingId, string CRECategoryId, string CREFieldOneId, string CREFieldTwoId)
        {
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    string userId = Session["UserId"].ToString();
                    var wyzId = long.Parse(id);
                    var wyzData = db.wyzusers.FirstOrDefault(x => x.id == wyzId);

                    if (newPhone == "")
                    {
                        wyzData.phoneNumber = "XXXXXXXXXX";
                    }
                    else
                    {
                        if (newPhone.StartsWith("+91"))
                        {
                            wyzData.phoneNumber = newPhone;
                        }
                        else
                        {
                            wyzData.phoneNumber = "+91" + newPhone;
                        }
                    }
                    wyzData.phoneIMEINo = newImei;
                    wyzData.password = newPass;
                    if (extnId == "" && sipExtension == "")
                    {
                        wyzData.extensionId = null;
                    }
                    else
                    {
                        if (gsmipPop != "" && sipExtension != "")
                        {
                            var tenantId = long.Parse(gsmipPop);
                            wyzData.tenant_id = tenantId;
                            var gsmRedId = db.Sipregistrationids.FirstOrDefault(x => x.tenant_id == tenantId && x.sipextension == sipExtension).gsmregistrationid;
                            wyzData.gsmRegistrationId = gsmRedId;
                            wyzData.extensionId = extnId;
                        }
                    }
                    if (!string.IsNullOrEmpty(creManager))
                    {
                        wyzData.creManager = creManager;
                    }
                    else
                    {
                        //??????????????????????????????
                    }
                    wyzData.updatedBy = long.Parse(userId);
                    wyzData.updatedOn = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));


                    // var work = db.userworkshops.FirstOrDefault(x => x.userWorkshop_id == wyzId);
                    int[] workshopList = Array.ConvertAll(workShop.Split(','), int.Parse);
                    wyzData.workshop_id = workshopList[0];


                    if (workshopList.Length > 0)
                    {
                        List<userworkshop> userworkshopLists = db.userworkshops.Where(m => m.userWorkshop_id == wyzId).ToList();
                        db.userworkshops.RemoveRange(userworkshopLists);

                        for (int i = 0; i < workshopList.Length; i++)
                        {

                            userworkshop userworkshop = new userworkshop();
                            userworkshop.userWorkshop_id = wyzId;
                            userworkshop.workshopList_id = workshopList[i];
                            db.userworkshops.Add(userworkshop);
                        }
                    }


                    //db.userworkshops.AddRange(work);
                    db.SaveChanges();


                    var selectedUserId = Convert.ToInt32(id);
                    CREUserAdditionalFieldsMapper userMapper = new CREUserAdditionalFieldsMapper();
                    if (db.CREUserAdditionalFieldsMapper.Where(u => u.CREUserId == selectedUserId)?.FirstOrDefault() is var tMapper && tMapper != null)
                    {
                        userMapper = tMapper;
                    }
                   
                    if (userMapper != null)
                    {
                        userMapper.CREUserId = Convert.ToInt32(id);
                        if (!String.IsNullOrEmpty(CRECampaignId)) userMapper.CampaignId = CRECampaignId;
                        if (!String.IsNullOrEmpty(CREServiceTypeId)) userMapper.ServiceTypeId = CREServiceTypeId;
                        if (!String.IsNullOrEmpty(CRECategoryId)) userMapper.CategoryId = CRECategoryId;
                        if (!String.IsNullOrEmpty(CREROAgeingId)) userMapper.ROAgeingId = CREROAgeingId;
                        if (!String.IsNullOrEmpty(CREFieldOneId)) userMapper.FieldOneId = Convert.ToInt32(CREFieldOneId);
                        if (!String.IsNullOrEmpty(CREFieldTwoId)) userMapper.FieldTwoId = Convert.ToInt32(CREFieldTwoId);
                        db.CREUserAdditionalFieldsMapper.AddOrUpdate(userMapper);
                        db.SaveChanges();
                    }





                }
                return Json(new { success = true, exception = "" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                string exception = "";
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
                return Json(new { success = false, exception = exception }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult deactivateCreAndSA(string value)
        {
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    bool deactivateCRE = true;
                    string userId = Session["UserId"].ToString();
                    long id = long.Parse(value);
                    if (db.assignedinteractions.Count(m => m.wyzUser_id == id) > 0)
                    {
                        deactivateCRE = false;
                    }
                    if (db.insuranceassignedinteractions.Count(m => m.wyzUser_id == id) > 0)
                    {
                        deactivateCRE = false;
                    }
                    if (db.psfassignedinteractions.Count(m => m.wyzUser_id == id) > 0)
                    {
                        deactivateCRE = false;
                    }
                    if (db.rosaAssignments.Count(m => m.wyzuser_id == id) > 0)
                    {
                        deactivateCRE = false;
                    }
                    if (deactivateCRE == false)
                    {
                        return Json(new { success = false, exception = "Please Clear the Assigned calls." });
                    }
                    wyzuser wyzuserData = db.wyzusers.FirstOrDefault(x => x.id == id);
                    wyzuserData.unAvailable = true;
                    assigmentplan assignmentPlanData = db.assigmentplans.FirstOrDefault(x => x.wyzuserId == id);//only 1 wyzuser will be tagged
                    if (assignmentPlanData != null)//necessary???????
                    {
                        db.assigmentplans.Remove(assignmentPlanData);
                    }
                    if (wyzuserData.role == "Service Advisor")
                    {
                        serviceadvisor SAData = db.serviceadvisors.FirstOrDefault(x => x.wyzUser_id == id);//only 1 wyzuser will be tagged
                        SAData.isActive = false;
                    }
                    wyzuserData.extensionId = null;
                    wyzuserData.updatedBy = long.Parse(userId);
                    wyzuserData.updatedOn = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    db.SaveChanges();
                    return Json(new { success = true, exception = "CRE Deactivated Successfully." });

                }
            }
            catch (Exception ex)
            {
                string exception = "";
                if (ex.Message.Contains("inner exception"))
                {
                    exception = ex.InnerException.Message;
                }
                else
                {
                    exception = ex.Message;
                }
                return Json(new { success = false, exception = exception });
            }
        }

        public ActionResult downloadAllWyzuser(string mangername, string unavailable, string rls)
        {
            string username = Session["UserName"].ToString();
            string userRole = Session["UserRole"].ToString();
            string userRole1 = Session["UserRole1"].ToString();
            long userId = Convert.ToInt64(Session["UserId"]);
            bool isunavailable = Convert.ToBoolean(unavailable);
            DataTable downloadExceltable = new DataTable();
            try
            {

                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    if (userRole == "CREManager")
                    {
                        if (rls == "")
                        {
                            var data = db.wyzusers.Where(m => m.unAvailable == isunavailable && m.creManager == username).Select(x => new { x.creManager, x.firstName, x.userName, x.password, x.phoneNumber, x.phoneIMEINo, x.extensionId, x.role }).ToList();
                            downloadExceltable = new SMRLiveController().ToDataTable(data);
                        }
                        else
                        {
                            var data = db.wyzusers.Where(m => m.role == rls && m.creManager == username && m.unAvailable == isunavailable).Select(x => new { x.creManager, x.firstName, x.userName, x.password, x.phoneNumber, x.phoneIMEINo, x.extensionId, x.role }).ToList();
                            downloadExceltable = new SMRLiveController().ToDataTable(data);
                        }
                    }
                    else
                    {
                        if (rls == "" || (rls == "CREManager" && mangername == ""))
                        {
                            var data = db.wyzusers.Where(m => m.unAvailable == isunavailable).Select(x => new { x.creManager, x.firstName, x.userName, x.password, x.phoneNumber, x.phoneIMEINo, x.extensionId, x.role }).ToList();
                            downloadExceltable = new SMRLiveController().ToDataTable(data);
                        }
                        else if (rls == "CREManager" && mangername != "")
                        {
                            var data = db.wyzusers.Where(m => m.role == "CRE" && m.creManager == mangername && m.unAvailable == isunavailable).Select(x => new { x.creManager, x.firstName, x.userName, x.password, x.phoneNumber, x.phoneIMEINo, x.extensionId, x.role }).ToList();
                            downloadExceltable = new SMRLiveController().ToDataTable(data);
                        }
                        else
                        {
                            var data = db.wyzusers.Where(m => m.role == rls && m.unAvailable == isunavailable).Select(x => new { x.creManager, x.firstName, x.userName, x.password, x.phoneNumber, x.phoneIMEINo, x.extensionId, x.role }).ToList();
                            downloadExceltable = new SMRLiveController().ToDataTable(data);
                        }
                    }

                    if (downloadExceltable.Rows.Count <= 0)
                    {
                        return Json(new { success = false, exception = "No Records Found" }, JsonRequestBehavior.AllowGet);

                    }

                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("CRE_Details", System.Text.Encoding.ASCII));
                    using (ExcelPackage pck = new ExcelPackage())
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("CRE_Details");
                        ws.Cells["A1"].LoadFromDataTable(downloadExceltable, true);
                        Session["DownloadExcel_FileManager"] = pck.GetAsByteArray();
                    }
                }
            }
            catch (Exception ex)
            {
                string exception = "";
                if (ex.Message.Contains("inner exception"))
                {
                    exception = ex.InnerException.Message;
                }
                else
                {
                    exception = ex.Message;
                }
                return Json(new { success = false, exception = exception }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, exception = "" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DownloadALLAddCre()
        {
            // Logger logger = LogManager.GetLogger("apkRegLogger");

            try
            {

                if (Session["DownloadExcel_FileManager"] != null)
                {
                    //logger.Info("Forecast Download Started");

                    byte[] data = Session["DownloadExcel_FileManager"] as byte[];
                    Session["DownloadExcel_FileManager"] = null;
                    //logger.Info("Forecast Download Ended");

                    return File(data, "application/octet-stream", "CRE_Details" + DateTime.Now.ToShortDateString() + DateTime.Now.TimeOfDay + ".xlsx");
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
            return new EmptyResult();
        }
        public ActionResult DownloadForCreManager()
        {
            // Logger logger = LogManager.GetLogger("apkRegLogger");

            try
            {

                if (Session["DownloadExcel_FileManager"] != null)
                {
                    //logger.Info("Forecast Download Started");

                    byte[] data = Session["DownloadExcel_FileManager"] as byte[];
                    Session["DownloadExcel_FileManager"] = null;
                    //logger.Info("Forecast Download Ended");

                    return File(data, "application/octet-stream", "Download For CreManager" + DateTime.Now.ToShortDateString() + DateTime.Now.TimeOfDay + ".xlsx");
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
            return new EmptyResult();
        }

        #region Re-Activate CRE

        public ActionResult reactivareCREId(long wyzuserId, string sipId)
        {
            string exception = string.Empty;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (db.wyzusers.Count(m => m.id == wyzuserId) > 0)
                    {
                        wyzuser addCre = db.wyzusers.Where(m => m.id == wyzuserId).FirstOrDefault();
                        addCre.unAvailable = false;
                        addCre.extensionId = sipId;
                        db.wyzusers.AddOrUpdate(addCre);
                        db.SaveChanges();
                        return Json(new { success = true, exception = exception }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, exception = "User Not Exists" }, JsonRequestBehavior.AllowGet);

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
                return Json(new { success = false, exception = exception }, JsonRequestBehavior.AllowGet);

            }
            return View();
        }
        #endregion



    }
}