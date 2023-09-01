using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoSherpa_project.Models;
using AutoSherpa_project.Models.ViewModels;
using System.Data.Entity;
using MySql.Data.MySqlClient;
using System.Data.Entity.Migrations;
using Newtonsoft.Json;

namespace AutoSherpa_project.Controllers
{
    [AuthorizeFilter]
    public class CustomerController : Controller
    {
        // GET: Customer
        [HttpGet, ActionName("addCustomer")]
        public ActionResult addCustomer()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    int userrole = Convert.ToInt32(Session["UserRole1"]);
                    var insurancecompanieslist = db.insurancecompanies.Select(com => new { id = com.id, name = com.companyName }).ToList();
                    ViewBag.insurancecompanylist = insurancecompanieslist;


                    var locationList = db.locations.Select(loc => new { id = loc.cityId, name = loc.name }).OrderBy(m => m.name).ToList();
                    ViewBag.locationList = locationList;
                    if (userrole == 2)
                    {
                        var workshopList = db.workshops.Where(m=>m.isinsurance==true).Select(work => new { id = work.id, name = work.workshopName }).ToList();
                        ViewBag.workShop = workshopList;
                    }
                    else
                    {
                        var workshopList = db.workshops.Where(m => m.isinsurance == false).Select(work => new { id = work.id, name = work.workshopName }).ToList();
                        ViewBag.workShop = workshopList;
                    }
                    var serviceTypeLists = db.servicetypes.Where(m => m.isActive == true).ToList();
                    ViewBag.serviceTypeLists = serviceTypeLists;
                   
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        //Drop down Populate workshop  base on city
        public ActionResult FillWorkshopDropdown(int? cityId)
            {
            try
            {

                using (var db = new AutoSherDBContext())
                {
                  var  workshopList = db.workshops.Where(x => x.location_cityId == cityId).Select(m => new { id = m.id, workshopName = m.workshopName }).ToList();

                    //if (workshopList.Count ==0)
                    //{
                    //    workshopList = db.workshops.ToList();
                    //}
                    return Json(new { workshopList = workshopList, JsonRequestBehavior.AllowGet });

                }
            }
            catch (Exception ex)
            {
            }
            return Json(new { workshopList = "", JsonRequestBehavior.AllowGet });

        }

        [HttpPost, ActionName("addCustomer")]
        public ActionResult saveCustomer(CustomerViewModel customerViewModel)
        {
            string logInUser = string.Empty;
            if (Session["LoginUser"].ToString() != null)
            {
                logInUser = Session["LoginUser"].ToString();
            }
            int userId = Convert.ToInt32(Session["UserId"].ToString());
            long managerId = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    using (var db = new AutoSherDBContext())
                    {
                        using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
                        {
                            try
                            {
                                string regNoSMR = null;

                                if (customerViewModel.vehicle.engineNo != null)
                                {
                                    regNoSMR = "E" + customerViewModel.vehicle.engineNo + "C" + customerViewModel.vehicle.chassisNo;
                                }
                                else
                                {
                                    customerViewModel.customer.dbvehregno = customerViewModel.vehicle.chassisNo;

                                }
                                customerViewModel.customer.createdBy = (Session["UserId"].ToString());
                                customerViewModel.customer.createdDate = Convert.ToDateTime(DateTime.Today.ToString());
                                if (regNoSMR != null)
                                {
                                    customerViewModel.customer.dbvehregno = regNoSMR;
                                }
                                db.customers.Add(customerViewModel.customer);
                                db.SaveChanges();

                                customerViewModel.address.customer_Id = customerViewModel.customer.id;
                                customerViewModel.address.isPreferred = true;
                                db.addresses.Add(customerViewModel.address);


                                customerViewModel.email.customer_id = customerViewModel.customer.id;
                                customerViewModel.email.isPreferredEmail = true;
                                db.emails.Add(customerViewModel.email);

                                customerViewModel.vehicle.customerId = customerViewModel.customer.id.ToString();
                                customerViewModel.vehicle.customer_id = customerViewModel.customer.id;
                                if (regNoSMR != null)
                                {
                                    customerViewModel.vehicle.chassisNo = regNoSMR;
                                }

                                if (logInUser == "Service")
                                {
                                    customerViewModel.vehicle.nextServicedate = DateTime.Now.AddDays(2);
                                }
                                else
                                {
                                    customerViewModel.vehicle.policyDueDate = DateTime.Now.AddDays(2);
                                }
                                if (logInUser == "Insurance")
                                {
                                    customerViewModel.vehicle.nextRenewalType = db.renewaltypes.FirstOrDefault(m => m.id == 9).renewalTypeName;
                                }
                                customerViewModel.vehicle.OEMWarrentyDate = Convert.ToDateTime(customerViewModel.vehicle.saleDate).AddYears(2).AddDays(-1);
                                customerViewModel.vehicle.averageRunning = "false";
                                db.vehicles.Add(customerViewModel.vehicle);

                                customerViewModel.phone.customer_id = customerViewModel.customer.id;
                                customerViewModel.phone.isPreferredPhone = true;
                                db.phones.Add(customerViewModel.phone);


                                db.SaveChanges();
                                if(Session["UserRole"].ToString()!="Admin")
                                {
                                    long campId = db.campaigns.FirstOrDefault(m => m.campaignName == "Customer Search").id;
                                    if (Session["UserRole"].ToString() == "CRE")
                                    {
                                        managerId = db.wyzusers.FirstOrDefault(m => m.userName == db.wyzusers.FirstOrDefault(x => x.id == userId).creManager && m.role == "CREManager").id;
                                    }
                                    else
                                    {
                                        managerId = Convert.ToInt64(Session["UserId"].ToString());
                                    }
                                    if (logInUser == "Service" && Session["UserRole"].ToString() != "Service Advisor")
                                    {
                                        assignedinteraction assign = new assignedinteraction();

                                        assign.callMade = "No";
                                        assign.displayFlag = false;
                                        assign.uplodedCurrentDate = DateTime.Now;
                                        assign.campaign_id = campId;
                                        assign.customer_id = customerViewModel.customer.id;
                                        assign.vehical_Id = customerViewModel.vehicle.vehicle_id;
                                        assign.wyzUser_id = userId;
                                        assign.nextServiceDate = DateTime.Now.AddDays(2);
                                        assign.assigned_wyzuser_id = userId;
                                        assign.assigned_manager_id = managerId;
                                        assign.location_id = customerViewModel.vehicle.vehicleWorkshop_id;
                                        assign.isautoassigned = false;
                                        db.assignedinteractions.Add(assign);
                                        db.SaveChanges();

                                    }
                                    else if (logInUser == "Insurance" && Session["UserRole"].ToString() != "Service Advisor")
                                    {

                                        insuranceassignedinteraction insu_assign = new insuranceassignedinteraction();

                                        insu_assign.callMade = "No";
                                        insu_assign.displayFlag = false;
                                        insu_assign.uplodedCurrentDate = DateTime.Now;
                                        insu_assign.customer_id = customerViewModel.customer.id;
                                        insu_assign.vehicle_vehicle_id = customerViewModel.vehicle.vehicle_id;
                                        insu_assign.wyzUser_id = userId;
                                        insu_assign.campaign_id = campId;
                                        insu_assign.policyDueDate = DateTime.Now.AddDays(2);
                                        insu_assign.location_id = customerViewModel.vehicle.vehicleWorkshop_id;
                                        insu_assign.isAutoAssigned = false;
                                        db.insuranceassignedinteractions.Add(insu_assign);
                                        db.SaveChanges();

                                    }
                                    //else if(logInUser == "Service" && Session["UserRole"].ToString() == "Service Advisor")
                                    //{
                                    //    rosaassignment rosaassignments = new rosaassignment();
                                    //    rosaassignments.assigneddatetime = DateTime.Now;
                                    //    rosaassignments.repairorderdetails_id = 0;
                                    //    rosaassignments.upload_id = 0;
                                    //    rosaassignments.wyzuser_id = userId;
                                    //    rosaassignments.customer_id = customerViewModel.customer.id;
                                    //    rosaassignments.vehicle_id = customerViewModel.vehicle.vehicle_id;
                                    //    rosaassignments.vehiclereg_no = customerViewModel.vehicle.vehicleRegNo;
                                    //    rosaassignments.rodate = customerViewModel.roDate;
                                    //    rosaassignments.servicetype =customerViewModel.serviceType ;
                                    //    rosaassignments.customername = customerViewModel.customer.customerName;
                                    //    rosaassignments.chassisno = customerViewModel.vehicle.chassisNo;
                                    //    rosaassignments.rostatus = customerViewModel.roStatus;
                                    //    rosaassignments.saledate = customerViewModel.vehicle.saleDate;
                                    //    rosaassignments.ronumber = customerViewModel.roNumber;
                                    //    rosaassignments.visittypes = customerViewModel.visitType;
                                    //    rosaassignments.serviceadvisor_name =customerViewModel.saName ;
                                    //    rosaassignments.mileage =Convert.ToInt64(customerViewModel.vehicle.odometerReading);
                                    //    rosaassignments.technician = customerViewModel.technician;
                                    //    rosaassignments.workshop_id = customerViewModel.vehicle.vehicleWorkshop_id;
                                    //    if(customerViewModel.roNumber!=null)
                                    //    {
                                    //        rosaassignments.jobcardlocation = customerViewModel.vehicle.chassisNo;
                                    //    }
                                    //    else
                                    //    {
                                    //        rosaassignments.jobcardlocation = customerViewModel.roNumber + customerViewModel.vehicle.chassisNo;
                                    //    }
                                    //    //rosaassignments.rostatusupdatedate =;
                                    //    rosaassignments.ismanuallyCreated =true;
                                    //    db.rosaAssignments.Add(rosaassignments);
                                    //    db.SaveChanges();
                                    //}
                                }
                                dbTransaction.Commit();
                                return Json(new { success = true, custId = customerViewModel.customer.id, vehId = customerViewModel.vehicle.vehicle_id });
                            }
                            catch (Exception ex)
                            {
                                dbTransaction.Rollback();
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

                                return Json(new { success = false, error = exception });
                            }

                        }
                    }
                }
                else
                {
                    return Json(new { success = false, error = "Please Fill Required Fields" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        public ActionResult searchCustomerDetails(int searchtype, string searchpattern)
        {
            long wyzUserId = long.Parse(Session["UserId"].ToString());
            //long heroDealerId = Convert.ToInt64(Session["HeroDealerId"]);
            string loginUser = Session["LoginUser"].ToString(), userRole = Session["UserRole"].ToString();// = "Service";

            List<customerSearchViewModel> searchDetails = new List<customerSearchViewModel>();
            try
            {

                using (var db = new AutoSherDBContext())
                {
                    db.Database.CommandTimeout = 900;
                    string str = @"CALL customer_search_by_individual_params(@pattern,@incampaign_id,@inparams);";

                    MySqlParameter[] sqlParameter = new MySqlParameter[]
                    {
                            new MySqlParameter("pattern",searchpattern),
                            new MySqlParameter("incampaign_id", "0"),
                            new MySqlParameter("inparams", searchtype),
                            
                            //new MySqlParameter("indealerid",heroDealerId),
                            //new MySqlParameter("inuserid",wyzUserId),

                    };
                    searchDetails = db.Database.SqlQuery<customerSearchViewModel>(str, sqlParameter).ToList();

                    foreach (var res in searchDetails)
                    {
                        long ins_id = Convert.ToInt32(res.ins_assignId);

                        long vehID = long.Parse(res.vehicle_id);
                        long workID_Assign = 0;
                        long? workID_wyzuser = db.wyzusers.FirstOrDefault(x => x.id == wyzUserId).workshop_id;


                        if (userRole == "CRE")
                        {
                            if (loginUser == "Insurance")
                            {
                                res.userrole = "Insurance";
                                enablecustomer360profile(res, "Insurance");
                            }

                        }
                        else if (userRole == "CREManager")
                        {
                            if (loginUser == "Insurance")
                            {
                                res.userrole = "Insurance";
                                res.assignSearch = "Yes";
                                enablecustomer360profile(res, "Insurance");
                            }
                        }
                        else if (userRole == "Admin")
                        {
                            if (loginUser == "Insurance")
                            {
                                res.userrole = "Insurance";
                                //res.assignSearch = "Yes";
                                enablecustomer360profile(res, "Insurance");
                            }
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

                return Json(new { success = false, error = exception });
            }
            return Json(new { success = true, data = searchDetails });


        }
        public customerSearchViewModel enablecustomer360profile(customerSearchViewModel custSearch, string typeOfDispo)
        {
            long wyzUserId = long.Parse(Session["UserId"].ToString());
            string userName = Session["UserName"].ToString();
            string loginUser = Session["LoginUser"].ToString();// = "Service";
            string dealerCode = Session["DealerCode"].ToString();
            bool isSuperCre = false;
            try
            {
                if (Session["IsSuperCRE"] != null && Convert.ToBoolean(Session["IsSuperCRE"]) == true)
                {
                    isSuperCre = true;
                }

                using (var db = new AutoSherDBContext())
                {
                    // bool userControl = db.dealers.FirstOrDefault(m => m.dealerCode == dealerCode).userControl ?? default(bool);
                    if (typeOfDispo == "Insurance")
                    {
                        int ins_id = Convert.ToInt32(custSearch.ins_assignId);
                        long? vehicleIdpolicy = Convert.ToInt64(custSearch.vehicle_id);
                        DateTime policyDueDate = DateTime.Now;
                        DateTime fromDate = DateTime.Now.AddDays(-90);
                        DateTime todate = DateTime.Now.AddDays(90);
                        bool isPolicyDueDate = false;

                        if (db.insurances.Count(m => m.vehicle_id == vehicleIdpolicy) > 0)
                        {
                            policyDueDate = db.insurances.Where(m => m.vehicle_id == vehicleIdpolicy).OrderByDescending(m => m.policyDueDate).FirstOrDefault().policyDueDate ?? (default(DateTime));
                            if (policyDueDate != null)
                            {
                                policyDueDate = Convert.ToDateTime(policyDueDate);
                                isPolicyDueDate = true;
                            }
                        }
                        else
                        {
                            isPolicyDueDate = false;
                        }

                        if (loginUser != "Insurance")
                        {
                            custSearch.userControl = "Hdd";
                        }
                        else if (db.insuranceassignedinteractions.Any(m => m.id == ins_id))
                        {
                            insuranceassignedinteraction insuAssign = db.insuranceassignedinteractions.FirstOrDefault(m => m.id == ins_id);
                            long assignWyzId = insuAssign.wyzUser_id ?? default(long);

                            string creManager = db.wyzusers.FirstOrDefault(m => m.id == assignWyzId).creManager;
                            if (string.IsNullOrEmpty(custSearch.ins_assignedcre) && assignWyzId != 0)
                            {
                                custSearch.ins_assignedcre = db.wyzusers.FirstOrDefault(m => m.id == assignWyzId).userName;
                                custSearch.insu_finaldispo = insuAssign.finalDisposition_id == null ? "0" : insuAssign.finalDisposition_id.ToString();
                            }
                            if (creManager == userName)
                            {
                                custSearch.assignSearch = "Yes";
                            }
                            else
                            {
                                custSearch.assignSearch = "No";
                            }
                            if (wyzUserId == assignWyzId && insuAssign.finalDisposition_id != 35)
                            {
                                custSearch.userControl = "Shw";
                            }
                            else if (isSuperCre == true)
                            {
                                custSearch.userControl = "Shw";
                            }
                            else
                            {
                                custSearch.userControl = "Hdd";
                            }
                            //else if (userControl)
                            //{
                            //    string assignCreManager = string.Empty;
                            //    if (assignWyzId != 0)
                            //    {
                            //        assignCreManager = db.wyzusers.FirstOrDefault(m => m.id == assignWyzId).creManager;
                            //        if (isPolicyDueDate == true)
                            //        {
                            //            if (assignCreManager == loggedIdCreManager && db.userroles.Any(m => m.users_id == wyzUserId && m.roles_id == 35) && (policyDueDate.Date > fromDate.Date && policyDueDate.Date < todate.Date))
                            //            {
                            //                custSearch.userControl = "Shw";
                            //            }
                            //            else
                            //            {
                            //                custSearch.userControl = "Hdd";
                            //            }
                            //        }
                            //        else
                            //        {
                            //            custSearch.userControl = "Hdd";
                            //        }
                            //    }
                            //    else if (db.userroles.Any(m => m.users_id == wyzUserId && m.roles_id == 35) && (policyDueDate.Date > fromDate.Date && policyDueDate.Date < todate.Date))
                            //    {
                            //        custSearch.userControl = "Shw";
                            //    }
                            //    else
                            //    {
                            //        custSearch.userControl = "Hdd";
                            //    }
                            //}
                            //else
                            //{
                            //    custSearch.userControl = "Shw";
                            //}
                        }
                        else
                        {
                            if (isSuperCre == true)
                            {
                                custSearch.userControl = "Shw";
                            }
                            else
                            {
                                custSearch.userControl = "Hdd";
                            }
                            //if (userControl)
                            //{
                            //    custSearch.userControl = "Hdd";
                            //}
                            //else
                            //{
                            //    if (custSearch.assignedCre == "Not Assigned" && db.dealers.FirstOrDefault().insunassignedblock == true)
                            //    {
                            //        custSearch.userControl = "Hdd";
                            //    }
                            //    else
                            //    {
                            //        custSearch.userControl = "Shw";
                            //    }
                            //}
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return custSearch;
            }

            return custSearch;
        }
        public ActionResult checkVehicle(string data)
        {
            string id = data.Split(',')[1];
            string regNo = data.Split(',')[0];
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (id == "1")//search for vehicle reg no.
                    {

                        if (db.vehicles.Any(m => m.vehicleRegNo.ToLower() == regNo.ToLower()))
                        {
                            return Json(new { success = true, cameFor = "vehReg" });
                        }
                        else
                        {
                            return Json(new { success = false, cameFor = "vehReg" });
                        }
                    }
                    else if (id == "2")//search for vehicle reg no.
                    {
                        if (db.vehicles.Any(m => m.chassisNo.ToLower() == regNo.ToLower()))
                        {
                            return Json(new { success = true, cameFor = "chassis" });
                        }
                        else
                        {
                            return Json(new { success = false, cameFor = "chassis" });
                        }
                    }

                    else if (id == "3")//search for vehicle reg no.
                    {
                        if (db.vehicles.Any(m => m.chassisNo.ToLower() == regNo.ToLower()))
                        {
                            return Json(new { success = true, cameFor = "engine" });
                        }
                        else
                        {
                            return Json(new { success = false, cameFor = "engine" });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return Json(new { success = false });
        }

        public ActionResult searchCustomer(int id=1)
        {
            try
            {
                long userId = Convert.ToInt64(Session["UserId"]);
                using (AutoSherDBContext db=new AutoSherDBContext())
                {
                    if (Session["DealerCode"].ToString() == "KATARIA" && Session["UserRole1"].ToString() == "4" && (db.wyzusers.Count(m => m.id == userId && m.uniquesupcre == true)) > 0 && id == 1)
                    {
                            ViewBag.SearchPage = "PSF";
                    }
                    else if (id == 1 || (db.wyzusers.Count(m => m.id == userId && m.uniquesupcre == true)) > 0)
                    {
                        ViewBag.SearchPage = "SMRINS";
                        ViewBag.uniquesupcre = db.wyzusers.Count(m => m.id == userId && m.uniquesupcre == true);
                    }
                    else if (id == 4)
                    {
                        ViewBag.SearchPage = "PSF";
                    }
                    else if (id == 5)
                    {
                        ViewBag.SearchPage = "POSTSALES";
                    }
                    else
                    {
                        ViewBag.SearchPage = "SMRINS";
                    }
                    string userRole = Session["UserRole"].ToString();                   
                    if(userRole=="CREManager" || userRole=="RM" || userRole=="Admin" || userRole=="WM")
                    {
                        ViewBag.givenAccess = db.wyzusers.FirstOrDefault(x => x.id == userId).searchAssignAccess;
                    }
                }
            }catch(Exception ex)
            {

            }
            return View();
        }

        public ActionResult Find(string searchData, string forRmPSfSearch,string seachBy, int? isuniquesupercreid)
        {

            searchData = searchData.Trim();
            long wyzUserId = long.Parse(Session["UserId"].ToString());
            string loginUser = Session["LoginUser"].ToString(), userRole = Session["UserRole"].ToString();// = "Service";
            string searchDataFor = "";

            List<customerSearchViewModel> searchDetails = new List<customerSearchViewModel>();
            List<CustomerSearchPSF> searchPSF = new List<CustomerSearchPSF>();
            try
            {
                string searchRes = string.Empty;
                if ((Session["DealerCode"].ToString() == "KATARIA" && isuniquesupercreid == 1) || ((Session["LoginUser"].ToString() != "POSTSALES" && Session["LoginUser"].ToString() != "POSTSALESComMgr" && Session["LoginUser"].ToString() != "PSFComMgr" && Session["LoginUser"].ToString() != "PSF" && forRmPSfSearch != "PSF" && seachBy!="policyNum" &&  Session["UserRole1"].ToString() != "5")) )
                {
                    int paramId = 0;
                    if(seachBy.Contains('_'))
                    {
                        paramId = int.Parse(seachBy.Split('_')[1]);
                    }
                    searchRes = getSearchDetails(searchData, paramId, "SMRINS");
                    searchDetails = searchRes != "" ? JsonConvert.DeserializeObject<List<customerSearchViewModel>>(searchRes) : new List<customerSearchViewModel>();
                    searchDataFor = "SMRINS";
                 }
                else if (Session["UserRole1"] != null && Session["UserRole1"].ToString() == "4" || forRmPSfSearch=="PSF" && seachBy!= "policyNum")
                {
                    searchRes = getSearchDetails(searchData, 0, "PSF");
                    searchPSF = searchRes!=""? JsonConvert.DeserializeObject<List<CustomerSearchPSF>>(searchRes):new List<CustomerSearchPSF>();
                    searchDataFor = "PSF";
                }
                else if (Session["UserRole1"] != null && Session["UserRole1"].ToString() == "5" || forRmPSfSearch== "POSTSALES" && seachBy!= "policyNum")
                {
                    searchRes = getSearchDetails(searchData, 0, "POSTSALES");
                    searchPSF = searchRes!=""? JsonConvert.DeserializeObject<List<CustomerSearchPSF>>(searchRes):new List<CustomerSearchPSF>();
                    searchDataFor = "POSTSALES";
                }
                else if(seachBy== "policyNum")
                {
                    searchRes = getSearchDetails(searchData,0, "policyNum");
                    searchDetails = searchRes != "" ? JsonConvert.DeserializeObject<List<customerSearchViewModel>>(searchRes) : new List<customerSearchViewModel>();
                    searchDataFor = "SMRINS";
                }
                
                using (var db = new AutoSherDBContext())
                {
                    db.Database.CommandTimeout = 900;

                    //checking if given access is 1 or 0
                    
                    if (searchDataFor== "SMRINS")
                    {
                        //short forms -------> Shw->show , Hdd->Hidden
                        foreach (var res in searchDetails)
                        {
                            if (res.chassisNo == null)
                            {
                                res.chassisNo = res.customerCategory;
                            }
                            long smr_id = Convert.ToInt32(res.smr_assignId);
                            long ins_id = Convert.ToInt32(res.ins_assignId);

                            long vehID = long.Parse(res.vehicle_id);
                            //var lastVehID = db.services.Where(x => x.vehicle_vehicle_id == vehID && x.c == res.cid).Max(x => x.vehicle_id);
                            long workID_Assign = 0;
                            long? vehworkId_Assign = 0;
                            long? latestworkId_Assign = 0;
                            long? workID_wyzuser = db.wyzusers.FirstOrDefault(x => x.id == wyzUserId).workshop_id;


                            if (userRole == "CRE")
                            {
                                if ((smr_id != 0 && ins_id != 0) /*|| (Session["DealerCode"].ToString() == "KATARIA" && Session["UserRole1"].ToString() == "4" && Session["uniquesupcre"].ToString() == "1")*/)
                                {
                                    if (loginUser == "Service" || loginUser == "PSFComMgr")
                                    {
                                        res.userrole = "Serive";
                                        authorizeCustprofile(res, "Service");
                                    }
                                    else if (loginUser == "Insurance")
                                    {
                                        res.userrole = "Insurance";
                                        authorizeCustprofile(res, "Insurance");
                                    }
                                }
                                //else if (smr_id != 0)
                                //{
                                //    res.userrole = "Serive";
                                //    authorizeCustprofile(res, "Service");
                                //}
                                //else if (ins_id != 0)
                                //{
                                //    res.userrole = "Insurance";
                                //    authorizeCustprofile(res, "Insurance");
                                //}
                                else
                                {

                                    //string dealerCode = Session["DealerCode"].ToString(); 
                                    //bool userControl = db.dealers.FirstOrDefault(m => m.dealerCode == dealerCode).userControl ?? default(bool);


                                    //if(loginUser=="Service")
                                    //{
                                    //    res.userrole = "Serive";
                                    //}
                                    //else if(loginUser=="Insurance")
                                    //{
                                    //    res.userrole = "Insurance";
                                    //}

                                    //if(!userControl)
                                    //{
                                    //    res.userControl = "Shw";
                                    //}
                                    //else
                                    //{
                                    //    res.userControl = "Hdd";
                                    //}

                                    if (loginUser == "Service")
                                    {
                                        res.userrole = "Serive";
                                        authorizeCustprofile(res, "Service");
                                    }
                                    else if (loginUser == "Insurance")
                                    {
                                        res.userrole = "Insurance";
                                        authorizeCustprofile(res, "Insurance");
                                    }

                                }
                            }
                            else if (userRole == "CREManager" || userRole == "RM"|| userRole == "WM")
                            {
                                if (loginUser == "Service")
                                {
                                    res.userrole = "Serive";

                                    if (db.assignedinteractions.Any(x => x.id == smr_id))
                                    {
                                        List<service> latestService = new List<service>();
                                        workID_Assign = Convert.ToInt64(db.assignedinteractions.FirstOrDefault(x => x.id == smr_id).location_id);
                                        if (workID_Assign != 0)
                                        {
                                            var listOfRoleId = db.userworkshops.Where(r => r.userWorkshop_id == wyzUserId).Select(r => r.workshopList_id).ToList();
                                            if (listOfRoleId.Contains(workID_Assign))
                                            {
                                                res.assignSearch = "Yes";
                                            }
                                            else
                                            {
                                                res.assignSearch = "No";
                                            }

                                        }
                                        else
                                        {
                                            res.assignSearch = "Yes";
                                        }

                                        //latestService = db.services.Where(x => x.vehicle_vehicle_id == vehID).OrderByDescending(m => m.jobCardDate).Skip(0).Take(1).ToList();
                                        //if (latestService.Count > 0)
                                        //{
                                        //    latestworkId_Assign = latestService[0].workshop_id;
                                        //}
                                        // vehworkId_Assign = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehID).vehicleWorkshop_id;

                                        //  if (vehworkId_Assign == 0 && workID_Assign == 0 && latestworkId_Assign == 0)
                                        //if (workID_Assign == 0)
                                        //{
                                        //    res.assignSearch = "Yes";
                                        //}
                                        //else if (workID_Assign == workID_wyzuser)
                                        //{
                                        //    res.assignSearch = "Yes";
                                        //}
                                        //else if (workID_wyzuser == latestworkId_Assign && workID_Assign == 0)
                                        //{
                                        //    res.assignSearch = "Yes";
                                        //}
                                        //else if (workID_wyzuser == vehworkId_Assign && workID_Assign == 0 && latestworkId_Assign == 0)
                                        //{
                                        //    res.assignSearch = "Yes";
                                        //}
                                        //else
                                        //{
                                        //    res.assignSearch = "No";
                                        //}

                                    }
                                    else
                                    {
                                        res.assignSearch = "Yes";
                                    }
                                    //if (db.assignedinteractions.Any(x => x.id==smr_id))
                                    //{
                                    //    workID_Assign = db.assignedinteractions.FirstOrDefault(x => x.id == smr_id).location_id;
                                    //    res.assignSearch = workID_Assign == workID_wyzuser ? "Yes" : "No";
                                    //}
                                    //else
                                    //{
                                    //    List<service> latestService = new List<service>();
                                    //    latestService = db.services.Where(x => x.vehicle_vehicle_id == vehID).OrderByDescending(m => m.jobCardDate).Skip(0).Take(1).ToList();
                                    //    //DateTime? maxJobCardDate = db.services.Where(m => m.vehicle_vehicle_id == vehID).Max(m => m.jobCardDate);
                                    //    if (latestService.Count == 0)
                                    //    {
                                    //        res.assignSearch = "Yes";
                                    //    }
                                    //    else
                                    //    {
                                    //        workID_Assign = latestService[0].workshop_id;
                                    //        res.assignSearch = workID_Assign == workID_wyzuser ? "Yes" : "No";
                                    //    }
                                    //}
                                    authorizeCustprofile(res, "Service");

                                }
                                else if (loginUser == "Insurance")
                                {
                                    res.userrole = "Insurance";
                                    //long? locID = db.insuranceassignedinteractions.FirstOrDefault(x => x.id == ins_id).location_id;
                                    //if (lastVehID != 0)
                                    //{
                                    //    workID = db.services.FirstOrDefault(x => x.id == lastVehID).workshop_id;
                                    //}
                                    //else
                                    //{
                                    //    workID = db.wyzusers.FirstOrDefault(x => x.id == wyzUserId).workshop_id;
                                    //}
                                    res.assignSearch = "Yes";
                                    authorizeCustprofile(res, "Insurance");
                                }
                            }
                            else if (userRole == "Admin")
                            {
                                if (loginUser == "Service")
                                {
                                    res.userrole = "Serive";


                                    res.assignSearch = "Yes";

                                    authorizeCustprofile(res, "Service");

                                }
                                else if (loginUser == "Insurance")
                                {
                                    res.userrole = "Insurance";
                                    res.assignSearch = "Yes";
                                    authorizeCustprofile(res, "Insurance");
                                }
                            }
                        }
                    }
                    else if (Session["UserRole1"] != null && Session["UserRole1"].ToString() == "4" || searchDataFor=="PSF")
                    {
                        long loginUserId = long.Parse(Session["UserId"].ToString());
                        string cmpName = string.Empty,assignCre=string.Empty;
                        string baseUrl = Request.ApplicationPath!="/"? Request.ApplicationPath+"/Home/redirectToCallLogging": "/Home/redirectToCallLogging";
                        foreach (var res in searchPSF)
                        {
                           
                            if(res.psfassignedInteraction_id!=0)
                            {
                                long assignWyzId = db.psfassignedinteractions.FirstOrDefault(m => m.id == res.psfassignedInteraction_id).wyzUser_id ?? default(long);
                                if(assignWyzId!=0)
                                {
                                    res.AssignCre = db.wyzusers.FirstOrDefault(m => m.id == assignWyzId).userName;
                                }
                                else
                                {
                                    res.AssignCre = "Not Assigned";
                                }

                                res.campaignId = db.psfassignedinteractions.FirstOrDefault(m => m.id == res.psfassignedInteraction_id).campaign_id ?? default(long);
                                if(res.campaignId!=0)
                                {
                                    res.campaignName = db.campaigns.FirstOrDefault(m => m.id == res.campaignId).campaignName;

                                    //if(res.campaignId!=5)
                                    //{
                                        if (assignWyzId != 0 && (assignWyzId == loginUserId))
                                        {
                                            if (res.complaint_creid != 0 && res.reworkStatus_id==63)
                                            {
                                                res.routingUrl = "<a href='" + baseUrl + "/Shw," + res.customer_id + "," + res.vehicleid + "," + res.psfassignedInteraction_id + ",63," + res.campaignId+ "'><i class='fa fa-pencil-square' data-toggel='tooltip' title='PSF Dispositon' style='font-size:30px; color:#DD4B39;'></i></a>"; ;
                                            }
                                            else
                                            {
                                                if (res.complaint_creid != 0)
                                                {
                                                    if (loginUserId == res.complaint_creid && res.reworkStatus_id != 63)
                                                    {
                                                        res.routingUrl = "<a href='" + baseUrl + "/Shw," + res.customer_id + "," + res.vehicleid + "," + res.psfassignedInteraction_id + ",900,0'><i class='fa fa-pencil-square' data-toggel='tooltip' title='PSF Dispositon' style='font-size:30px; color:#DD4B39;'></i></a>";
                                                    }
                                                    else
                                                    {
                                                        res.routingUrl = "<a href='" + baseUrl + "/Hdd," + res.customer_id + "," + res.vehicleid + "," + res.psfassignedInteraction_id + ",0,6'><i class='fa fa-id-card-o' data-toggel='tooltip' title='PSF Dispositon' style='font-size:30px; color:#DD4B39;'></i></a>";
                                                    }
                                                    res.escalationTo = db.wyzusers.FirstOrDefault(m => m.id == res.complaint_creid).userName;
                                                }
                                                else
                                                {
                                                    res.routingUrl = "<a href='" + baseUrl + "/Shw," + res.customer_id + "," + res.vehicleid + "," + res.psfassignedInteraction_id + ",0," + res.campaignId + "'><i class='fa fa-pencil-square' data-toggel='tooltip' title='PSF Dispositon' style='font-size:30px; color:#DD4B39;'></i></a>"; ;
                                                }
                                                
                                            }

                                        }
                                        else if (res.complaint_creid != 0)
                                        {
                                            if(loginUserId == res.complaint_creid && res.reworkStatus_id != 63)
                                            {
                                                res.routingUrl = "<a href='" +baseUrl + "/Shw," + res.customer_id + "," + res.vehicleid + "," + res.psfassignedInteraction_id + ",900,0'><i class='fa fa-pencil-square' data-toggel='tooltip' title='PSF Dispositon' style='font-size:30px; color:#DD4B39;'></i></a>";
                                            }
                                            else
                                            {
                                                res.routingUrl = "<a href='"+baseUrl + "/Hdd," + res.customer_id + "," + res.vehicleid + "," + res.psfassignedInteraction_id + ",0,6'><i class='fa fa-id-card-o' data-toggel='tooltip' title='PSF Dispositon' style='font-size:30px; color:#DD4B39;'></i></a>";
                                            }
                                            res.escalationTo = db.wyzusers.FirstOrDefault(m => m.id == res.complaint_creid).userName;
                                        }
                                        else
                                        {
                                            res.routingUrl = "<a href='" + baseUrl + "/Hdd," + res.customer_id + "," + res.vehicleid + "," + res.psfassignedInteraction_id + ",0,6'><i class='fa fa-id-card-o' data-toggel='tooltip' title='PSF Dispositon' style='font-size:30px; color:#DD4B39;'></i></a>";
                                        }
                                    //}
                                    //else
                                    //{
                                    //    res.routingUrl = "<a href='" + baseUrl + "/Hdd," + res.customer_id + "," + res.vehicleid + "," + res.psfassignedInteraction_id + ",0,6'><i class='fa fa-id-card-o' data-toggel='tooltip' title='PSF Dispositon' style='font-size:30px; color:#DD4B39;'></i></a>";
                                    //}

                                    
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(res.escalationTo))
                                {
                                    res.escalationTo = "Not Yet Escalated";
                                }
                                if (string.IsNullOrEmpty(res.lastdisposition))
                                {
                                    res.lastdisposition = "Not Yet Disposed";
                                }

                                if(string.IsNullOrEmpty(res.AssignCre))
                                {
                                    res.AssignCre = "Not Assigned";
                                }

                                if (string.IsNullOrEmpty(res.billdate))
                                {
                                    res.billdate = "-";
                                }



                                if (string.IsNullOrEmpty(res.jobcardnumber))
                                {
                                    res.jobcardnumber = "-";
                                }

                                if (string.IsNullOrEmpty(res.lastdisposition))
                                {
                                    res.lastdisposition = "-";
                                }

                                if (string.IsNullOrEmpty(res.escalationTo))
                                {
                                    res.escalationTo = "-";
                                }

                                if (string.IsNullOrEmpty(res.escallationdispo))
                                {
                                    res.escallationdispo = "-";
                                }

                                res.routingUrl = "<a href='" + baseUrl + "/Hdd," + res.customer_id + "," + res.vehicleid + "," + res.psfassignedInteraction_id + ",0,6'><i class='fa fa-id-card-o' data-toggel='tooltip' title='PSF Dispositon' style='font-size:30px; color:#DD4B39;'></i></a>";
                            }
                        }
                    }
                    else if (Session["UserRole1"] != null && Session["UserRole1"].ToString() == "5" || searchDataFor == "POSTSALES")
                    {
                        long loginUserId = long.Parse(Session["UserId"].ToString());
                        string cmpName = string.Empty, assignCre = string.Empty;
                        string baseUrl = Request.ApplicationPath != "/" ? Request.ApplicationPath + "/Home/redirectToCallLogging" : "/Home/redirectToCallLogging";

                        foreach (var res in searchPSF)
                        {

                            if (res.psfassignedInteraction_id != 0)
                            {
                                long assignWyzId = db.Postsalesassignedinteractions.FirstOrDefault(m => m.id == res.psfassignedInteraction_id).wyzUser_id ?? default(long);
                                if (assignWyzId != 0)
                                {
                                    res.AssignCre = db.wyzusers.FirstOrDefault(m => m.id == assignWyzId).userName;
                                }
                                else
                                {
                                    res.AssignCre = "Not Assigned";
                                }

                                res.campaignId = db.Postsalesassignedinteractions.FirstOrDefault(m => m.id == res.psfassignedInteraction_id).campaign_id ?? default(long);
                                if (res.campaignId != 0)
                                {
                                    res.campaignName = db.campaigns.FirstOrDefault(m => m.id == res.campaignId).campaignName;
                                    string postSaleUser = Session["LoginUser"].ToString();
                                    if (postSaleUser == "POSTSALESComMgr")
                                    {
                                        long dipoid = db.Postsalescompinteractions.Where(m => m.psfassignedInteraction_id == res.psfassignedInteraction_id).OrderByDescending(m=>m.Id).Select(m=>m.calldisposition_id).FirstOrDefault();
                                        if (res.complaint_creid != 0 && (res.complaint_creid == loginUserId) && dipoid != 63 )
                                        {
                                            res.routingUrl = "<a href='" + baseUrl + "/CRE,salesfeedback," + res.customer_id + "," + res.vehicleid + "," + res.psfassignedInteraction_id + ",9000," + res.campaignId + "," + res.modelcat + "'><i class='fa fa-pencil-square' data-toggel='tooltip' title='PSF Dispositon' style='font-size:30px; color:#DD4B39;'></i></a>";
                                        }
                                        else
                                        {
                                            res.routingUrl = "<a href='" + baseUrl + "/CREManager,salesfeedback," + res.customer_id + "," + res.vehicleid + "," + res.psfassignedInteraction_id + ",0," + res.campaignId + "," + res.modelcat + "'><i class='fa fa-id-card-o' data-toggel='tooltip' title='PSF Dispositon' style='font-size:30px; color:#DD4B39;'></i></a>";
                                        }
                                    }
                                    else if (postSaleUser == "POSTSALES")
                                    {
                                        if (assignWyzId != 0 && (assignWyzId == loginUserId) && res.finalDisposition_id != 64 && res.finalDisposition_id != 22 )
                                        {
                                            if(res.finalDisposition_id == 44)
                                            {
                                                var postsalescompInteraction  = db.Postsalescompinteractions.Where(m => m.psfassignedInteraction_id == res.psfassignedInteraction_id).OrderByDescending(m => m.Id).FirstOrDefault();

                                                if (postsalescompInteraction != null)
                                                {

                                                    if (postsalescompInteraction.calldisposition_id == 63)
                                                    {
                                                        res.routingUrl = "<a href='" + baseUrl + "/CRE,salesfeedback," + res.customer_id + "," + res.vehicleid + "," + res.psfassignedInteraction_id + ",0," + res.campaignId + "," + res.modelcat + "'><i class='fa fa-pencil-square' data-toggel='tooltip' title='PSF Dispositon' style='font-size:30px; color:#DD4B39;'></i></a>";
                                                    }
                                                    else
                                                    {
                                                        res.routingUrl = "<a href='" + baseUrl + "/CREManager,salesfeedback," + res.customer_id + "," + res.vehicleid + "," + res.psfassignedInteraction_id + ",0," + res.campaignId + "," + res.modelcat + "'><i class='fa fa-id-card-o' data-toggel='tooltip' title='PSF Dispositon' style='font-size:30px; color:#DD4B39;'></i></a>";
                                                    }
                                                }
                                                else
                                                {
                                                    res.routingUrl = "<a href='" + baseUrl + "/CREManager,salesfeedback," + res.customer_id + "," + res.vehicleid + "," + res.psfassignedInteraction_id + ",0," + res.campaignId + "," + res.modelcat + "'><i class='fa fa-id-card-o' data-toggel='tooltip' title='PSF Dispositon' style='font-size:30px; color:#DD4B39;'></i></a>";
                                                }
                                            }
                                            else
                                            {
                                                res.routingUrl = "<a href='" + baseUrl + "/CRE,salesfeedback," + res.customer_id + "," + res.vehicleid + "," + res.psfassignedInteraction_id + ",0," + res.campaignId + "," + res.modelcat + "'><i class='fa fa-pencil-square' data-toggel='tooltip' title='PSF Dispositon' style='font-size:30px; color:#DD4B39;'></i></a>";
                                            }


                                        }
                                        else
                                        {
                                            res.routingUrl = "<a href='" + baseUrl + "/CREManager,salesfeedback," + res.customer_id + "," + res.vehicleid + "," + res.psfassignedInteraction_id + ",0," + res.campaignId + "," + res.modelcat + "'><i class='fa fa-id-card-o' data-toggel='tooltip' title='PSF Dispositon' style='font-size:30px; color:#DD4B39;'></i></a>";
                                        }
                                    }
                                    else
                                    {
                                        res.routingUrl = "<a href='" + baseUrl + "/CREManager,salesfeedback," + res.customer_id + "," + res.vehicleid + "," + res.psfassignedInteraction_id + ",0," + res.campaignId + "," + res.modelcat + "'><i class='fa fa-id-card-o' data-toggel='tooltip' title='PSF Dispositon' style='font-size:30px; color:#DD4B39;'></i></a>";
                                    }
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(res.escalationTo))
                                {
                                    res.escalationTo = "Not Yet Escalated";
                                }
                                if (string.IsNullOrEmpty(res.lastdisposition))
                                {
                                    res.lastdisposition = "Not Yet Disposed";
                                }

                                if (string.IsNullOrEmpty(res.AssignCre))
                                {
                                    res.AssignCre = "Not Assigned";
                                }

                                if (string.IsNullOrEmpty(res.saleDate))
                                {
                                    res.billdate = "-";
                                }
                                if (string.IsNullOrEmpty(res.lastdisposition))
                                {
                                    res.lastdisposition = "-";
                                }

                                if (string.IsNullOrEmpty(res.escalationTo))
                                {
                                    res.escalationTo = "-";
                                }

                                if (string.IsNullOrEmpty(res.escallationdispo))
                                {
                                    res.escallationdispo = "-";
                                }

                                res.routingUrl = "<a href='" + baseUrl + "/Hdd," + res.customer_id + "," + res.vehicleid + "," + res.psfassignedInteraction_id + ",0,6'><i class='fa fa-id-card-o' data-toggel='tooltip' title='PSF Dispositon' style='font-size:30px; color:#DD4B39;'></i></a>";
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string exception = "";
                
                if(ex.InnerException!=null)
                {
                    if(ex.InnerException.InnerException!=null)
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

            if (searchDataFor=="SMRINS")
            {
                return Json(new { success = true, data = searchDetails });
            }
            else if (Session["UserRole1"] != null && Session["UserRole1"].ToString() == "4" || forRmPSfSearch=="PSF" || Session["UserRole1"].ToString() == "5" || forRmPSfSearch== "POSTSALES")
            {
                return Json(new { success = true, data = searchPSF });
            }

            return Json(new { success = false, error = "Parsing Error" });
            
        }


        public string getSearchDetails(string pattern, int paramId, string searchFor)
        {
            List<customerSearchViewModel> searchDetails = new List<customerSearchViewModel>();
            List<CustomerSearchPSF> searchPSF = new List<CustomerSearchPSF>();
            string returSerializedString = "";
            try
            {
            using (var db = new AutoSherDBContext())
            {
                db.Database.CommandTimeout = 900;
                if (searchFor== "SMRINS")
                {
                    string str = @"CALL customer_search_by_individual_params(@pattern,@campId,@inparams);";

                    MySqlParameter[] sqlParameter = new MySqlParameter[]
                    {
                            new MySqlParameter("pattern", pattern),
                            new MySqlParameter("campId", "0"),
                            new MySqlParameter("inparams", paramId),

                    };
                    searchDetails = db.Database.SqlQuery<customerSearchViewModel>(str, sqlParameter).ToList();
                    returSerializedString = JsonConvert.SerializeObject(searchDetails);
                }
                else if (Session["UserRole1"] != null && Session["UserRole1"].ToString() == "4" || searchFor=="PSF")
                {
                        string str = @"CALL customer_search_psf(@pattern);";

                        MySqlParameter[] sqlParameter = new MySqlParameter[]
                        {
                                    new MySqlParameter("pattern", pattern),

                        };
                        searchPSF = db.Database.SqlQuery<CustomerSearchPSF>(str, sqlParameter).ToList();
                        returSerializedString = JsonConvert.SerializeObject(searchPSF);
                }
                else if (Session["UserRole1"] != null && Session["UserRole1"].ToString() == "5" || searchFor== "POSTSALES")
                {
                    string str = @"CALL customer_search_postsales(@pattern);";

                    MySqlParameter[] sqlParameter = new MySqlParameter[]
                    {
                                new MySqlParameter("pattern", pattern),

                    };
                    searchPSF = db.Database.SqlQuery<CustomerSearchPSF>(str, sqlParameter).ToList();
                    returSerializedString = JsonConvert.SerializeObject(searchPSF);
                }
                else if(searchFor=="policyNum")
                {
                    string str = @"CALL customer_search_policynumber(@pattern);";

                    MySqlParameter[] sqlParameter = new MySqlParameter[]
                    {
                            new MySqlParameter("pattern", pattern)

                    };
                    searchDetails = db.Database.SqlQuery<customerSearchViewModel>(str, sqlParameter).ToList();
                    returSerializedString = JsonConvert.SerializeObject(searchDetails);
                }
            }

            }
            catch (Exception ex)
            {
                
           }

            return returSerializedString;
        }

        public customerSearchViewModel authorizeCustprofile(customerSearchViewModel custSearch, string typeOfDispo)
        {
            long wyzUserId = long.Parse(Session["UserId"].ToString());
            string loginUser = Session["LoginUser"].ToString();// = "Service";
            string dealerCode = Session["DealerCode"].ToString();
            bool isSuperCre = false;
            try
            {
                if (Session["IsSuperCRE"] != null && Convert.ToBoolean(Session["IsSuperCRE"]) == true)
                {
                    isSuperCre = true;
                }

                using (var db = new AutoSherDBContext())
                {
                    if (typeOfDispo == "Service")
                    {
                        bool userControl = db.dealers.FirstOrDefault(m => m.dealerCode == dealerCode).userControl ?? default(bool);

                        int smr_id = Convert.ToInt32(custSearch.smr_assignId);

                        if (isSuperCre == false)
                        {
                            if (loginUser != "Service")
                            {
                                custSearch.userControl = "Hdd";
                                //Session["PageFor"] = "Search";
                            }
                            else if (db.assignedinteractions.Any(m => m.id == smr_id))
                            {
                                assignedinteraction assign = db.assignedinteractions.FirstOrDefault(m => m.id == smr_id);
                                long assignWyzId = assign.wyzUser_id ?? default(long);
                                //long wyzUserId = long.Parse(Session["UserId"].ToString());
                                string loggedIdCreManager = db.wyzusers.FirstOrDefault(m => m.id == wyzUserId).creManager;
                                //bool userControl = db.dealers.FirstOrDefault(m => m.dealerCode == dealerCode).userControl ?? default(bool);

                                if (wyzUserId == assignWyzId && assign.finalDisposition_id != 35)
                                {
                                    custSearch.userControl = "Shw";
                                    //Session["PageFor"] = "CRE";
                                }
                                else if (userControl)
                                {
                                    string assignCreManager = string.Empty;
                                    if (assignWyzId != 0)
                                    {
                                        assignCreManager = db.wyzusers.FirstOrDefault(m => m.id == assignWyzId).creManager;
                                        if (assignCreManager == loggedIdCreManager && db.userroles.Any(m => m.users_id == wyzUserId && m.roles_id == 35))
                                        {
                                            custSearch.userControl = "Shw";
                                            //Session["PageFor"] = "CRE";
                                        }
                                        else
                                        {
                                            custSearch.userControl = "Hdd";
                                        }
                                    }
                                    else if (db.userroles.Any(m => m.users_id == wyzUserId && m.roles_id == 35))
                                    {
                                        custSearch.userControl = "Shw";
                                        //Session["PageFor"] = "CRE";
                                    }
                                    else
                                    {
                                        custSearch.userControl = "Hdd";
                                        //Session["PageFor"] = "Search";
                                    }
                                }
                                else
                                {
                                    custSearch.userControl = "Shw";
                                    //Session["PageFor"] = "Search";
                                }
                            }
                            else
                            {
                                //if (userControl)
                                //{
                                //    custSearch.userControl = "Hdd";
                                //}
                                //else
                                //{
                                    if (custSearch.smr_assignedcre == "Not Assigned" && db.dealers.FirstOrDefault().smrunassignedblock == true)
                                    {
                                        custSearch.userControl = "Hdd";
                                    }
                                    else
                                    {
                                        custSearch.userControl = "Shw";
                                    }
                                //}

                                //Session["PageFor"] = "Search";
                            }
                        }
                        else
                        {
                            if (loginUser != "Service")
                            {
                                custSearch.userControl = "Hdd";
                                //Session["PageFor"] = "Search";
                            }
                            else if (db.assignedinteractions.Any(m => m.id == smr_id))
                            {
                                long vehicle_id = long.Parse(custSearch.vehicle_id);

                                //string loggedIdCreManager = db.wyzusers.FirstOrDefault(m => m.id == wyzUserId).creManager;

                                //long managerWorkshopId = db.wyzusers.FirstOrDefault(m => m.userName == loggedIdCreManager).workshop_id ?? default(long);
                                //long vehicleWorkshopId = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehicle_id).vehicleWorkshop_id ?? default(long);

                                //if(Session["DealerCode"].ToString()!= "ADVAITHHYUNDAI")
                                //{
                                //    bool superCreControl = db.dealers.FirstOrDefault(m => m.dealerCode == dealerCode).superControl ?? default(bool);

                                //    if (vehicleWorkshopId == managerWorkshopId)
                                //    {
                                //        custSearch.userControl = "Shw";
                                //    }
                                //    else
                                //    {
                                //        custSearch.userControl = "Hdd";
                                //    }
                                //}
                                //else
                                {
                                    custSearch.userControl = "Shw";
                                }
                                
                            }
                            else
                            {
                                long vehicle_id = long.Parse(custSearch.vehicle_id);

                                //string loggedIdCreManager = db.wyzusers.FirstOrDefault(m => m.id == wyzUserId).creManager;

                                //long managerWorkshopId = db.wyzusers.FirstOrDefault(m => m.userName == loggedIdCreManager).workshop_id ?? default(long);
                                //long vehicleWorkshopId = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehicle_id).vehicleWorkshop_id ?? default(long);

                                //if (Session["DealerCode"].ToString() != "ADVAITHHYUNDAI")
                                //{
                                //    bool superCreControl = db.dealers.FirstOrDefault(m => m.dealerCode == dealerCode).superControl ?? default(bool);

                                //    if (vehicleWorkshopId == managerWorkshopId)
                                //    {
                                //        custSearch.userControl = "Shw";
                                //    }
                                //    else
                                //    {
                                //        custSearch.userControl = "Hdd";
                                //    }
                                //}
                                //else
                                {
                                    custSearch.userControl = "Shw";
                                }

                            }
                        }
                    }
                    else if (typeOfDispo == "Insurance")
                    {
                        bool userControl = db.dealers.FirstOrDefault(m => m.dealerCode == dealerCode).INSuserControl ?? default(bool);
                        int ins_id = Convert.ToInt32(custSearch.ins_assignId);
                        long? vehicleIdpolicy = Convert.ToInt64(custSearch.vehicle_id);
                        DateTime policyDueDate = DateTime.Now;
                        DateTime fromDate = DateTime.Now.AddDays(-90);
                        DateTime todate = DateTime.Now.AddDays(90);
                        bool isPolicyDueDate=false;

                        if(db.insurances.Count(m=>m.vehicle_id==vehicleIdpolicy)>0)
                        {
                            policyDueDate = db.insurances.Where(m => m.vehicle_id == vehicleIdpolicy).OrderByDescending(m => m.policyDueDate).FirstOrDefault().policyDueDate ?? (default(DateTime));
                            if (policyDueDate != null)
                            {
                                policyDueDate = Convert.ToDateTime(policyDueDate);
                                isPolicyDueDate = true;
                            }
                        }
                        else
                        {
                            isPolicyDueDate = false;
                        }
                       
                        if (loginUser != "Insurance")
                        {
                            custSearch.userControl = "Hdd";
                        }
                        else if (db.insuranceassignedinteractions.Any(m => m.id == ins_id))
                        {
                            insuranceassignedinteraction insuAssign = db.insuranceassignedinteractions.FirstOrDefault(m => m.id == ins_id);
                            long assignWyzId = insuAssign.wyzUser_id ?? default(long);

                            string loggedIdCreManager = db.wyzusers.FirstOrDefault(m => m.id == wyzUserId).creManager;
                            if(string.IsNullOrEmpty(custSearch.ins_assignedcre) && assignWyzId!=0)
                            {
                                custSearch.ins_assignedcre = db.wyzusers.FirstOrDefault(m => m.id == assignWyzId).userName;
                                custSearch.insu_finaldispo = insuAssign.finalDisposition_id==null?"0": insuAssign.finalDisposition_id.ToString();
                            }

                            if (wyzUserId == assignWyzId && insuAssign.finalDisposition_id != 35)
                            {
                                custSearch.userControl = "Shw";
                            }
                            else if(isSuperCre==true)
                            {
                                custSearch.userControl = "Shw";
                            }
                            else if (userControl)
                            {
                                string assignCreManager = string.Empty;
                                if (assignWyzId != 0)
                                {
                                    assignCreManager = db.wyzusers.FirstOrDefault(m => m.id == assignWyzId).creManager;
                                    if (isPolicyDueDate == true)
                                    {
                                        if (assignCreManager == loggedIdCreManager && db.userroles.Any(m => m.users_id == wyzUserId && m.roles_id == 35) && (policyDueDate.Date > fromDate.Date && policyDueDate.Date < todate.Date))
                                        {
                                            custSearch.userControl = "Shw";
                                        }
                                        else
                                        {
                                            custSearch.userControl = "Hdd";
                                        }
                                    }
                                    else
                                    {
                                        custSearch.userControl = "Hdd";
                                    }
                                }
                                else if (db.userroles.Any(m => m.users_id == wyzUserId && m.roles_id == 35) && (policyDueDate.Date > fromDate.Date && policyDueDate.Date < todate.Date))
                                {
                                    custSearch.userControl = "Shw";
                                }
                                else
                                {
                                    custSearch.userControl = "Hdd";
                                }
                            }
                            else
                            {
                                custSearch.userControl = "Shw";
                            }
                        }
                        else
                        {
                            //if (userControl)
                            //{
                            //        custSearch.userControl = "Hdd";
                             
                            //}
                            //else
                            //{
                                if (custSearch.ins_assignedcre == "Not Assigned" && db.dealers.FirstOrDefault().insunassignedblock == true)
                                {
                                    custSearch.userControl = "Hdd";
                                }
                                else
                                {
                                    custSearch.userControl = "Shw";
                                }
                            }

                            //Session["PageFor"] = "Search";
                        //}
                    }
                }

            }
            catch (Exception ex)
            {
                return custSearch;
            }

            return custSearch;
        }

        public ActionResult getWorkshopByCity(int? cityId)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    var locationList = db.locations.ToList();
                    var workshopList = db.workshops.Where(user => user.location_cityId == cityId).Select(user => user.location).SingleOrDefault();
                    return Json(new { success = true, data = workshopList });

                }

            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public ActionResult getCREofCREManager(long? customerId, long? vehicleId, string userrole)
        {

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string user = Session["UserName"].ToString();
                    long userId = Convert.ToInt64(Session["userId"].ToString());
                    long? LocId = db.wyzusers.FirstOrDefault(u => u.id == userId).location_cityId;
                    long? workshp_Id = db.wyzusers.FirstOrDefault(u => u.id == userId).workshop_id;
                    bool isinsurance = db.wyzusers.FirstOrDefault(u => u.id == userId).insuranceRole;

                    if (Session["UserRole"].ToString() == "Admin" )
                    {
                        if (isinsurance)
                        {
                            var ddlcampaignList = db.campaigns.Where(m => (m.campaignType == "Forecast" || m.campaignType == "Insurance") && m.isactive == true).Select(model => new { id = model.id, CampaignName = model.campaignName }).ToList();
                            if (Session["DealerCode"].ToString() == "CAUVERYFORD")
                            {
                                var listOfRoleId = db.userworkshops.Where(r => r.userWorkshop_id == userId).Select(r => r.workshopList_id).ToList();
                                var workshopsList = db.workshops.Where(r => listOfRoleId.Contains(r.id)).Select(m => new { id = m.id, name = m.workshopName }).OrderBy(m=>m.name).ToList();
                                return Json(new { workshopsList, role = "insurance", campaignList = ddlcampaignList }, JsonRequestBehavior.AllowGet);

                            }
                            else
                            {
                                var workshopsList = db.locations.Where(x => x.moduleType == "2" || x.moduleType == "3").Select(x => new { id = x.cityId, name = x.name }).OrderBy(m => m.name).ToList();
                                return Json(new { workshopsList, role = "insurance", campaignList = ddlcampaignList }, JsonRequestBehavior.AllowGet);

                            }
                        }
                        else
                        {
                            var ddlcampaignList = db.campaigns.Where(m => (m.campaignType == "Campaign" || m.campaignType == "Service Reminder" || m.campaignType == "Forecast") && m.isactive == true).Select(model => new { id = model.id, CampaignName = model.campaignName }).ToList();
                            if (Session["DealerCode"].ToString() == "CAUVERYFORD")
                            {
                                var listOfRoleId = db.userworkshops.Where(r => r.userWorkshop_id == userId).Select(r => r.workshopList_id).ToList();
                                var workshopsList = db.workshops.Where(r => listOfRoleId.Contains(r.id)).Select(m => new { id = m.id, name = m.workshopName }).OrderBy(m => m.name).ToList();
                                return Json(new { workshopsList, role = "service", campaignList = ddlcampaignList }, JsonRequestBehavior.AllowGet);

                            }
                            else
                            {
                                var workshopsList = db.workshops.Where(x => x.isinsurance == false).Select(x => new { id = x.id, name = x.workshopName }).OrderBy(m => m.name).ToList();
                            return Json(new { workshopsList, role = "service", campaignList = ddlcampaignList }, JsonRequestBehavior.AllowGet);
                        }
                        }
                    }
                    if (Session["UserRole"].ToString() == "RM" || Session["UserRole"].ToString() == "WM")
                    {
                        var ddlcampaignList = db.campaigns.Where(m => (m.campaignType == "Forecast" || m.campaignType == "Insurance") && m.isactive == true).Select(model => new { id = model.id, CampaignName = model.campaignName }).ToList();

                        var creManagerIdList = db.AccessLevels.Where(m => m.rmId == userId).Select(m => m.creId).Distinct().ToList();

                        var listOfRoleId = db.userworkshops.Where(r =>creManagerIdList.Contains(r.userWorkshop_id)).Select(r => r.workshopList_id).ToList();
                        var workshopsList = db.workshops.Where(r => listOfRoleId.Contains(r.id)).Select(m => new { id = m.id, name = m.workshopName }).OrderBy(m => m.name).ToList();
                        return Json(new { workshopsList, role = "insurance", campaignList = ddlcampaignList }, JsonRequestBehavior.AllowGet);


                        //if (isinsurance)
                        //{
                        //    var ddlcampaignList = db.campaigns.Where(m => (m.campaignType == "Forecast" || m.campaignType == "Insurance") && m.isactive == true).Select(model => new { id = model.id, CampaignName = model.campaignName }).ToList();

                        //    var workshopsList = db.locations.Where(x => x.moduleType == "2").Select(x => new { id = x.cityId, name = x.name }).ToList();
                        //    return Json(new { workshopsList, role = "insurance", campaignList = ddlcampaignList }, JsonRequestBehavior.AllowGet);
                        //}
                        //else
                        //{
                        //    var ddlcampaignList = db.campaigns.Where(m => (m.campaignType == "Campaign" || m.campaignType == "Service Reminder" || m.campaignType == "Forecast") && m.isactive == true).Select(model => new { id = model.id, CampaignName = model.campaignName }).ToList();

                        //    var workshopsList = db.workshops.Where(x => x.isinsurance == false).Select(x => new { id = x.id, name = x.workshopName }).ToList();
                        //    return Json(new { workshopsList, role = "service", campaignList = ddlcampaignList }, JsonRequestBehavior.AllowGet);
                        //}
                    }
                    else
                    {

                        List<campaign> ddlcampaignList = new List<campaign>();
                        if (isinsurance)
                        {
                                ddlcampaignList = db.campaigns.Where(m => (m.campaignType == "Forecast" || m.campaignType == "Insurance") && m.isactive == true).ToList();
                            if (db.insuranceassignedinteractions.Any(m => m.vehicle_vehicle_id == vehicleId && m.location_id != 0))
                            {
                                long? locationId = db.insuranceassignedinteractions.FirstOrDefault(m => m.vehicle_vehicle_id == vehicleId).location_id;
                                long? locationcityId = db.workshops.FirstOrDefault(m => m.id == locationId).location_cityId;
                                var workshopsList = db.locations.Where(m => m.cityId == locationcityId).Select(m => new { id = m.cityId, name = m.name }).OrderBy(m => m.name).ToList();
                                return Json(new { workshopsList, role = "insurance", campaignList = ddlcampaignList.Select(model => new { id = model.id, CampaignName = model.campaignName }).ToList() }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (db.vehicles.Any(m => m.vehicle_id == vehicleId && m.insduelocation_id != 0))
                                {
                                    long? locationId = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehicleId).insduelocation_id;
                                    long? locationcityId = db.workshops.FirstOrDefault(m => m.id == locationId).location_cityId;
                                    var workshopsList = db.locations.Where(m => m.cityId == locationcityId).Select(m => new { id = m.cityId, name = m.name }).OrderBy(m => m.name).ToList();
                                    return Json(new { workshopsList, role = "insurance", campaignList = ddlcampaignList.Select(model => new { id = model.id, CampaignName = model.campaignName }).ToList() }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    var listOfRoleId = db.userlocations.Where(r => r.userLocation_id == userId).Select(r => r.locationList_cityId).ToList();
                                    var workshopsList = db.locations.Where(r => listOfRoleId.Contains(r.cityId)).Select(m => new { id = m.cityId, name = m.name }).OrderBy(m => m.name).ToList();
                                    return Json(new { workshopsList, role = "insurance", campaignList = ddlcampaignList.Select(model => new { id = model.id, CampaignName = model.campaignName }).ToList() }, JsonRequestBehavior.AllowGet);
                                }

                            }
                        }
                        else
                        {
                                ddlcampaignList = db.campaigns.Where(m => (m.campaignType == "Campaign" || m.campaignType == "Service Reminder" || m.campaignType == "Forecast") && m.isactive == true).ToList();
                            var listOfRoleId = db.userworkshops.Where(r => r.userWorkshop_id == userId).Select(r => r.workshopList_id).ToList();
                            var workshopsList = db.workshops.Where(r => listOfRoleId.Contains(r.id)).Select(m => new { id = m.id, name = m.workshopName }).OrderBy(m => m.name).ToList();

                            return Json(new { workshopsList, role = "service", campaignList = ddlcampaignList.Select(model => new { id = model.id, CampaignName = model.campaignName }).ToList() }, JsonRequestBehavior.AllowGet);

                        }
                    }
                    

                    ////select id,username from wyzuser where role='CRE' and creManager='uttarahalli_manager';

                    //var cres=  db.wyzusers.Where(u => u.creManager == user && u.role == "CRE" && u.workshop_id== workshopId).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { });
        }
        
        public ActionResult getCampaignBycreId(long? creid, long? workshpId)
        {

            try
            {
                using (var db = new AutoSherDBContext())
                {

                    var campaignIdsIds = db.assigmentplans.Where(r => r.wyzuserId == creid && r.locationId == workshpId).Select(r => r.campaign_id).ToList();
                    var ddlcampaignList = db.campaigns.Where(m => campaignIdsIds.Contains(m.id) && m.isactive == true).ToList();
                    return Json(new {campaignList = ddlcampaignList.Select(model => new { id = model.id, CampaignName = model.campaignName }).ToList() }, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { });
        }

        public ActionResult getCREManager(long? workshpId)
        {

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string user = Session["UserName"].ToString();
                    long userId = Convert.ToInt64(Session["userId"].ToString());
                    bool isinsurance = db.wyzusers.FirstOrDefault(u => u.id == userId).insuranceRole;

                    if (Session["DealerCode"].ToString() == "INDUS")
                    {
                        if (Session["UserRole"].ToString() == "Admin")
                        {
                            if (isinsurance)
                            {
                                var cres = db.wyzusers.Where(u => u.role == "CRE" && u.unAvailable==false && u.role1 == "2").Select(m => new { creId = m.id, creName = m.firstName }).ToList();
                                return Json(new { cres });
                            }
                            else
                            {
                                var cres = db.wyzusers.Where(u => u.role == "CRE" && u.unAvailable == false && u.workshop_id == workshpId && u.role1 == "1").Select(m => new { creId = m.id, creName = m.firstName }).ToList();
                                return Json(new { cres });
                            }
                        }
                        if (Session["UserRole"].ToString() == "RM" || Session["UserRole"].ToString() == "WM")
                        {
                            var creManagerIdList = db.AccessLevels.Where(m => m.rmId == userId).Select(m => m.creId).Distinct().ToList();
                            var cres = db.wyzusers.Where(x => creManagerIdList.Contains(x.id) && x.workshop_id == workshpId).Select(m => new { creId = m.id, creName = m.firstName }).ToList();
                            return Json(new { cres });

                        }
                        else
                        {
                            if (isinsurance)
                            {
                                var cres = db.wyzusers.Where(u => u.creManager == user && u.role == "CRE" && u.unAvailable == false && u.role1 == "2").Select(m => new { creId = m.id, creName = m.firstName }).ToList();
                                return Json(new { cres });
                            }
                            else
                            {
                                var cres = db.wyzusers.Where(u => u.creManager == user && u.role == "CRE" && u.unAvailable == false && u.workshop_id == workshpId && u.role1 == "1").Select(m => new { creId = m.id, creName = m.firstName }).ToList();
                                return Json(new { cres });
                            }
                        }
                    }
                    if (Session["DealerCode"].ToString() == "HARPREETFORD" || Session["DealerCode"].ToString() == "HANSHYUNDAI" || Session["DealerCode"].ToString() == "PODDARCARWORLD" || Session["DealerCode"].ToString() == "CHAVANMOTORS" || Session["DealerCode"].ToString() == "ADITYACARCAREPVTLTD")
                    {
                        //if (Session["UserRole"].ToString() == "Admin")
                        //{
                        //    if (isinsurance)
                        //    {
                        //        var cres = db.wyzusers.Where(u => u.role == "CRE" && u.unAvailable == false && u.role1 == "2").Select(m => new { creId = m.id, creName = m.firstName + "(" + m.userName + ")" }).ToList();
                        //        return Json(new { cres });
                        //    }
                        //    else
                        //    {
                        //        var cres = db.wyzusers.Where(u => u.role == "CRE" && u.unAvailable == false && u.workshop_id == workshpId && u.role1 == "1").Select(m => new { creId = m.id, creName = m.firstName+"("+m.userName+")" }).ToList();
                        //        return Json(new { cres });
                        //    }
                        //}
                        //if (Session["UserRole"].ToString() == "RM" || Session["UserRole"].ToString() == "WM")
                        //{
                        //    var creManagerIdList = db.AccessLevels.Where(m => m.rmId == userId).Select(m => m.creId).Distinct().ToList();
                        //    var cres = db.wyzusers.Where(x => creManagerIdList.Contains(x.id) && x.workshop_id == workshpId).Select(m => new { creId = m.id, creName = m.firstName + "(" + m.userName + ")" }).ToList();
                        //    return Json(new { cres });

                        //}
                        //else
                        //{
                            var creIds = db.userworkshops.Where(r => r.workshopList_id == workshpId).Select(r => r.userWorkshop_id).ToList();
                            if (isinsurance)
                            {

                                var cres = db.wyzusers.Where(u => creIds.Contains(u.id) && u.role == "CRE" && u.unAvailable == false && u.role1 == "2").Select(m => new { creId = m.id, creName = m.firstName + "(" + m.userName + ")" }).ToList();
                                return Json(new { cres });
                            }
                            else
                            {
                                var cres = db.wyzusers.Where(u => creIds.Contains(u.id) && u.role == "CRE" && u.unAvailable == false && u.role1 == "1" && u.userName != "test_chavan_cre" && u.userName != "test_smr_cre").Select(m => new { creId = m.id, creName = m.firstName + "(" + m.userName + ")" }).ToList();
                                return Json(new { cres });
                            }
                        //}
                    }

                    else
                    {
                        if (Session["UserRole"].ToString() == "Admin")
                        {
                            if (isinsurance)
                            {
                                var cres = db.wyzusers.Where(u => u.role == "CRE" && u.unAvailable == false && u.role1 == "2").Select(m => new { creId = m.id, creName = m.userName }).ToList();
                                return Json(new { cres });
                            }
                            else
                            {
                                var cres = db.wyzusers.Where(u => u.role == "CRE" && u.unAvailable == false && u.workshop_id == workshpId && u.role1 == "1").Select(m => new { creId = m.id, creName = m.userName }).ToList();
                                return Json(new { cres });
                            }
                        }
                        if (Session["UserRole"].ToString() == "RM" || Session["UserRole"].ToString() == "WM")
                        {
                            var creManagerIdList = db.AccessLevels.Where(m => m.rmId == userId).Select(m => m.creId).Distinct().ToList();
                            var cres = db.wyzusers.Where(x => creManagerIdList.Contains(x.id) && x.workshop_id == workshpId).Select(m => new { creId = m.id, creName = m.userName }).ToList();
                            return Json(new { cres });

                        }
                        else
                        {
                            if (isinsurance)
                            {
                                var cres = db.wyzusers.Where(u => u.creManager == user && u.role == "CRE" && u.unAvailable == false && u.role1 == "2").Select(m => new { creId = m.id, creName = m.userName }).ToList();
                                return Json(new { cres });
                            }
                            else
                            {
                                var cres = db.wyzusers.Where(u => u.creManager == user && u.role == "CRE" && u.unAvailable == false && u.workshop_id == workshpId && u.role1 == "1").Select(m => new { creId = m.id, creName = m.userName }).ToList();
                                return Json(new { cres });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { });
        }
        public ActionResult assignCallManually(long? custmerId, long? vehicleId, long? locID, long creId, long assignId, long campId)
        {
            int count = 0;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    long Id = Convert.ToInt64(Session["UserId"]);
                    //long campId = db.campaigns.FirstOrDefault(c => c.id).id;
                    bool role = db.wyzusers.FirstOrDefault(m => m.id == Id).insuranceRole;
                    vehicle veh = db.vehicles.Where(u => u.vehicle_id == vehicleId).FirstOrDefault();

                    if (role)
                    {
                        if (assignId == 0 && db.insuranceassignedinteractions.Count(m => m.vehicle_vehicle_id == vehicleId && m.customer_id == custmerId) == 0)
                        {
                            insuranceassignedinteraction ins = new insuranceassignedinteraction();

                            ins.callMade = "NO";
                            ins.interactionType = "Enquiry";
                            if (campId == 0)
                            {
                                ins.campaign_id = 50;
                            }
                            else
                            {
                                ins.campaign_id = campId;
                            }
                            ins.customer_id = custmerId;
                            ins.vehicle_vehicle_id = vehicleId;
                            ins.wyzUser_id = creId;
                            ins.displayFlag = true;
                            ins.uplodedCurrentDate = DateTime.Now;
                            ins.updatedOnDate = DateTime.Now;
                            ins.location_id = locID;
                            ins.isAutoAssigned = false;

                            int countinsforecastExist = db.insuranceforecasteddatas.Count(m => m.vehicle_id == vehicleId);
                            if (countinsforecastExist > 0)
                            {
                                insuranceforecasteddata insFore = db.insuranceforecasteddatas.Where(u => u.vehicle_id == vehicleId).FirstOrDefault();
                                if (insFore.policyexpirydate != null)
                                {
                                    ins.policyDueDate = insFore.policyexpirydate;
                                }
                                ins.policyDueType = insFore.renewaltype.ToString();
                                ins.insuranceCompanyName = insFore.insurancecompanyname;
                                ins.isAutoAssigned = false;
                                ins.upload_id = 0;
                                ins.assigned_wyzuser_id = creId;
                                ins.assigned_manager_id = Id;
                                db.insuranceassignedinteractions.Add(ins);
                                db.SaveChanges();

                                assignedcallsreport assignReport = new assignedcallsreport();
                                assignReport.assignInteractionID = ins.id;
                                assignReport.assignedDate = DateTime.Now;
                                assignReport.assignmentType = "Insurance";
                                assignReport.moduletypeId = 2;
                                long dueType = Convert.ToInt64(ins.policyDueType);
                                assignReport.dueType = dueType;
                                assignReport.dueDate = veh.policyDueDate;
                                assignReport.vehicleId = Convert.ToInt64(vehicleId);
                                assignReport.wyzuserId = Convert.ToInt64(creId);
                                assignReport.campaignId = ins.campaign_id;
                                assignReport.assigned_manager_id = Id;
                                assignReport.isautoassigned = false;
                                db.assignedcallsreports.AddOrUpdate(assignReport);
                                db.SaveChanges();

                            }
                            else
                            {
                                if (veh.policyDueDate != null)
                                {
                                    ins.policyDueDate = Convert.ToDateTime(veh.policyDueDate);
                                }
                                if (veh.nextRenewalType != null && !veh.nextRenewalType.Equals("") && veh.nextRenewalType!="0")
                                {
                                    string dueTypes = veh.nextRenewalType;
                                    long dueId = 0;                          
                                    dueId = db.renewaltypes.Where(u => u.renewalTypeName == dueTypes).Select(u => u.id).FirstOrDefault();

                                    ins.policyDueType = dueId.ToString();
                                }
                                else
                                {
                                    ins.policyDueType = "0";
                                }

                                ins.insuranceCompanyName = veh.insuranceCompanyName;
                                // ins.location_id = locID;
                                ins.isAutoAssigned = false;
                                ins.upload_id = 0;
                                // ins.uplodedCurrentDate = DateTime.Now;
                                ins.assigned_wyzuser_id = creId;
                                ins.assigned_manager_id = Id;
                                //db.insuranceassignedinteractions.AddOrUpdate(ins);
                                db.insuranceassignedinteractions.Add(ins);
                                db.SaveChanges();

                                assignedcallsreport assignReport = new assignedcallsreport();
                                assignReport.assignInteractionID = ins.id;
                                assignReport.assignedDate = DateTime.Now;
                                assignReport.assignmentType = "Insurance";
                                assignReport.moduletypeId = 2;
                                long dueType = Convert.ToInt64(ins.policyDueType);
                                assignReport.dueType = dueType;
                                assignReport.dueDate = veh.policyDueDate;
                                assignReport.vehicleId = Convert.ToInt64(vehicleId);
                                assignReport.wyzuserId = Convert.ToInt64(creId);
                                assignReport.campaignId = ins.campaign_id;
                                assignReport.assigned_manager_id = Id;
                                assignReport.isautoassigned = false;
                                db.assignedcallsreports.AddOrUpdate(assignReport);
                                db.SaveChanges();
                            }
                        }
                        else if (assignId != 0 && db.insuranceassignedinteractions.Count(m => m.id == assignId && m.customer_id == custmerId && m.vehicle_vehicle_id == vehicleId) > 0)
                        {
                            //count = db.insuranceassignedinteractions.Count(m => m.id==assignId && m.customer_id==custmerId && m.vehicle_vehicle_id==vehicleId);
                            //if (count > 0)
                            // {
                            insuranceassignedinteraction ins = db.insuranceassignedinteractions.FirstOrDefault(m => m.id == assignId);
                            if (ins.wyzUser_id != 0 && ins.wyzUser_id != null)
                            {
                                ins.assigned_manager_id = Id;
                                if (campId != 0)
                                {
                                    ins.campaign_id = campId;
                                }
                                ins.wyzUser_id = creId;
                                ins.isAutoAssigned = false;
                                db.insuranceassignedinteractions.AddOrUpdate(ins);
                                db.SaveChanges();

                                change_assignment_records chng = new change_assignment_records();
                                chng.assignedinteraction_id = ins.id;
                                chng.last_wyzuserId = Convert.ToInt64(ins.wyzUser_id);
                                chng.campaign_id = ins.campaign_id ?? default(long);
                                chng.moduletypeIs = 2;
                                chng.new_wyzuserId = Convert.ToInt64(creId);
                                chng.updatedDate = DateTime.Now;
                                chng.updatedType = "Enquiry";
                                chng.assigned_manager_id = Id;
                                db.change_assignment_records.Add(chng);

                                db.SaveChanges();

                            }
                            else
                            {
                                ins.wyzUser_id = creId;
                                ins.assigned_wyzuser_id = creId;
                                if (campId != 0)
                                {
                                    ins.campaign_id = campId;
                                }
                                ins.assigned_manager_id = Id;
                                ins.isAutoAssigned = false;
                                db.insuranceassignedinteractions.AddOrUpdate(ins);
                                db.SaveChanges();


                                assignedcallsreport assignReport = new assignedcallsreport();
                                assignReport.assignInteractionID = ins.id;
                                assignReport.assignedDate = DateTime.Now;
                                assignReport.assignmentType = "Insurance";
                                assignReport.moduletypeId = 2;
                                long dueType = Convert.ToInt64(ins.policyDueType);
                                assignReport.dueType = dueType;
                                assignReport.dueDate = veh.policyDueDate;
                                assignReport.vehicleId = Convert.ToInt64(vehicleId);
                                assignReport.wyzuserId = Convert.ToInt64(creId);
                                assignReport.campaignId = ins.campaign_id;
                                assignReport.assigned_manager_id = Id;
                                assignReport.isautoassigned = false;
                                db.assignedcallsreports.Add(assignReport);
                                db.SaveChanges();
                            }
                            //}


                        }
                        else
                        {
                            return Json(new { success = false, exception = "Data Duplication Found" });
                        }
                    }
                    else
                    {
                        if (assignId == 0 && db.assignedinteractions.Count(m => m.vehical_Id == vehicleId && m.customer_id == custmerId) == 0)
                        {
                            long? vehicleWorkshop_id= db.vehicles.FirstOrDefault(m => m.vehicle_id == vehicleId).vehicleWorkshop_id;
                            assignedinteraction assgnInt = new assignedinteraction();
                            assgnInt.callMade = "NO";
                            assgnInt.interactionType = "Enquiry";
                            if (campId == 0)
                            {
                                assgnInt.campaign_id = 50;
                            }
                            else
                            {
                                assgnInt.campaign_id = campId;
                            }
                            assgnInt.customer_id = custmerId;
                            assgnInt.vehical_Id = vehicleId;
                            assgnInt.wyzUser_id = creId;
                            assgnInt.displayFlag = true;
                            assgnInt.uplodedCurrentDate = DateTime.Now;


                            long smrForecastCount = db.smrforecasteddatas.Count(u => u.vehicle_id == vehicleId);
                            if (smrForecastCount > 0)
                            {
                                smrforecasteddata smrForecasted = db.smrforecasteddatas.Where(u => u.vehicle_id == vehicleId).FirstOrDefault();
                                assgnInt.nextServiceType = smrForecasted.ServiceTypeId.ToString();
                                if (smrForecasted.nextServiceDate != null)
                                {
                                    assgnInt.nextServiceDate = smrForecasted.nextServiceDate;
                                }

                                if (smrForecasted.location_id != null && smrForecasted.location_id != 0)
                                {
                                    assgnInt.location_id = smrForecasted.location_id;
                                }
                                else if (vehicleWorkshop_id != null && vehicleWorkshop_id != 0)
                                {
                                    assgnInt.location_id = vehicleWorkshop_id;
                                }

                            }
                            else
                            {
                                assgnInt.nextServiceType = "0";
                                //if (veh.nextServicedate != null)
                                //{
                                //    assgnInt.nextServiceDate = veh.nextServicedate;
                                //}
                                assgnInt.location_id = vehicleWorkshop_id;

                            }
                            assgnInt.upload_id = 0;
                            assgnInt.assigned_wyzuser_id = creId;
                            assgnInt.assigned_manager_id = Id;
                            assgnInt.isautoassigned = false;
                            db.assignedinteractions.Add(assgnInt);
                            db.SaveChanges();

                            assignedcallsreport assignReport = new assignedcallsreport();
                            assignReport.assignInteractionID = assgnInt.id;
                            assignReport.assignedDate = DateTime.Now;
                            assignReport.assignmentType = "Service";
                            assignReport.moduletypeId = 1;
                            long dueType = Convert.ToInt64(assgnInt.nextServiceType);
                            assignReport.dueType = dueType;
                            assignReport.dueDate = assgnInt.nextServiceDate;
                            assignReport.vehicleId = Convert.ToInt64(vehicleId);
                            assignReport.wyzuserId = Convert.ToInt64(creId);
                            assignReport.campaignId = assgnInt.campaign_id;
                            assignReport.assigned_manager_id = Id;
                            assignReport.isautoassigned = false;
                            db.assignedcallsreports.Add(assignReport);
                            db.SaveChanges();

                        }
                        else if (assignId != 0 && db.assignedinteractions.Count(m => m.id == assignId && m.customer_id == custmerId && m.vehical_Id == vehicleId) > 0)
                        {
                            //count = db.assignedinteractions.Count(m => m.id == assignId);
                            //if (count > 0)
                            //{
                            bool doAssign = true;
                            assignedinteraction assgnInt = db.assignedinteractions.FirstOrDefault(m => m.id == assignId);

                            if (Session["DealerCode"].ToString() == "KALYANIMOTORS")
                            {
                                if (assgnInt.ishocre == false || assgnInt.isSupCre == false)
                                {
                                    doAssign = true;
                                }
                                else
                                {
                                    doAssign = false;
                                }
                            }

                            if (doAssign == true)
                            {
                                if (assgnInt.wyzUser_id != 0 && assgnInt.wyzUser_id != null)
                                {
                                    assgnInt.assigned_manager_id = Id;
                                    if (campId != 0)
                                    {
                                        assgnInt.campaign_id = campId;
                                    }
                                    assgnInt.wyzUser_id = creId;
                                    assgnInt.isautoassigned = false;
                                    db.assignedinteractions.AddOrUpdate(assgnInt);
                                    db.SaveChanges();

                                    change_assignment_records chng = new change_assignment_records();
                                    chng.assignedinteraction_id = assgnInt.id;
                                    chng.last_wyzuserId = Convert.ToInt64(assgnInt.wyzUser_id);
                                    chng.campaign_id = assgnInt.campaign_id ?? default(long);
                                    chng.moduletypeIs = 1;
                                    chng.new_wyzuserId = Convert.ToInt64(creId);
                                    chng.updatedDate = DateTime.Now;
                                    chng.updatedType = "Enquiry";
                                    chng.assigned_manager_id = Id;
                                    db.change_assignment_records.Add(chng);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    assgnInt.assigned_manager_id = Id;
                                    assgnInt.assigned_wyzuser_id = creId;
                                    assgnInt.wyzUser_id = creId;
                                    assgnInt.isautoassigned = false;
                                    if (campId != 0)
                                    {
                                        assgnInt.campaign_id = campId;
                                    }

                                    db.assignedinteractions.AddOrUpdate(assgnInt);
                                    db.SaveChanges();

                                    assignedcallsreport assignReport = new assignedcallsreport();
                                    assignReport.assignInteractionID = assgnInt.id;
                                    assignReport.assignedDate = DateTime.Now;
                                    assignReport.assignmentType = "Service";
                                    assignReport.moduletypeId = 1;
                                    long dueType = Convert.ToInt64(assgnInt.nextServiceType);
                                    assignReport.dueType = dueType;
                                    assignReport.dueDate = assgnInt.nextServiceDate;
                                    assignReport.vehicleId = Convert.ToInt64(vehicleId);
                                    assignReport.wyzuserId = Convert.ToInt64(creId);
                                    assignReport.campaignId = assgnInt.campaign_id;
                                    assignReport.assigned_manager_id = Id;
                                    assignReport.isautoassigned = false;
                                    db.assignedcallsreports.Add(assignReport);
                                    db.SaveChanges();
                                }
                                if (db.callinteractions.Any(m => m.assignedInteraction_id == assgnInt.id && m.serviceBooked_serviceBookedId != null))
                                {
                                    var serviceBookedIds = db.callinteractions.Where(m => m.assignedInteraction_id == assgnInt.id && m.serviceBooked_serviceBookedId != null).Select(m => m.serviceBooked_serviceBookedId).ToList();

                                    foreach (var id in serviceBookedIds)
                                    {
                                        servicebooked serviceBook = db.servicebookeds.FirstOrDefault(m => m.serviceBookedId == id);
                                        serviceBook.chaser_id = creId;
                                        db.servicebookeds.AddOrUpdate(serviceBook);
                                        db.SaveChanges();
                                    }
                                }
                            }
                            //}
                            //else
                            //{
                            //    return Json(new { success = false, exception = "isHoCre and isSupCre is true, cannot assign call" });
                            //}

                        }
                        else
                        {
                            return Json(new { success = false, exception = "Data Duplication Found" });

                        }
                    }
                    return Json(new { success = true });
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
            return Json(new { success = false });
        }

    }
}