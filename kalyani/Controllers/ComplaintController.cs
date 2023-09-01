using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoSherpa_project.Models;
using AutoSherpa_project.Models.ViewModels;
using Newtonsoft.Json;

namespace AutoSherpa_project.Controllers
{
    [AuthorizeFilter]
    public class ComplaintController : Controller
    {
        // GET: Complaint
        [HttpGet, ActionName("addComplaint")]
        public ActionResult addComplaint()
        {
            ComplaintViewModel funRole = new ComplaintViewModel();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    var workshopList = db.workshops.Select(work => new { id = work.id, name = work.workshopName }).ToList();
                    ViewBag.workshopList = workshopList;
                    var complaintSourceList = db.complaintsources.Select(source => new { name = source.complaintSource1 }).ToList();
                    ViewBag.complaintSourceList = complaintSourceList;
                    var functionRole = db.roles.Select(r => new { id=r.id,name=r.role1 }).ToList();
                    ViewBag.functionRole = functionRole;
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }



        //[HttpPost, ActionName("addComplaint")]

        public ActionResult saveCustomerComplaints(ComplaintViewModel complaint)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string complaintNumber = "C" + DateTime.Now.ToString("yyyymmddhhmmss");
                    complaint cmplnt = new complaint();
                    cmplnt.complaintStatus = "New";
                    cmplnt.isAssigned = "false";
                    cmplnt.callMade = "no";

                    cmplnt.RaisedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    cmplnt.issueDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    if (complaint.isComplaintExist)
                    {
                        cmplnt.complaintNumber = complaintNumber;
                        cmplnt.vehicleRegNo = complaint.vehicleRegNo;
                        cmplnt.customerName = complaint.customerName;

                        int count = db.phones.Count(x => x.customer_id == complaint.customerId);//Checking Whether Customer Has any phone number
                        if (count > 0)
                        {
                            var result = db.phones.FirstOrDefault(b => b.phoneNumber == complaint.customerMobileNo && b.isPreferredPhone == true && b.customer_id == complaint.customerId);//if he enters same number with already active no action needed
                            if (result == null)//if he adds ne phone i.e record does not exists
                            {
                                var checkifPhoneWxist = db.phones.SingleOrDefault(b => b.phoneNumber == complaint.customerMobileNo && b.customer_id == complaint.customerId);//checks whether enter phone number matches any of his previous phone number
                                if (checkifPhoneWxist != null) //if exists
                                {
                                    db.phones.Where(x => x.customer_id == complaint.customerId).ToList().ForEach(x =>{x.isPreferredPhone = false;});
                                    checkifPhoneWxist.isPreferredPhone = true;//make that phone number as active
                                    db.SaveChanges();
                                }
                                else
                                {
                                    db.phones.Where(x => x.customer_id == complaint.customerId).ToList().ForEach(x =>{x.isPreferredPhone = false;});
                                    //create new phone number
                                    phone ph = new phone();
                                    ph.customer_id = complaint.customerId;
                                    ph.phoneNumber = complaint.customerMobileNo;
                                    ph.isPreferredPhone = true;
                                    db.phones.Add(ph);
                                }
                            }
                        }
                        else  //if not found any phone number add one
                        {
                            phone ph = new phone();
                            ph.customer_id = complaint.customerId;
                            ph.phoneNumber = complaint.customerMobileNo;
                            ph.isPreferredPhone = true;
                            db.phones.Add(ph);
                        }

                        //For Email

                        int countEmail = db.emails.Count(x => x.customer_id == complaint.customerId);//Checking Whether Customer Has any Email
                        if (countEmail > 0)
                        {
                            var result = db.emails.FirstOrDefault(b => b.emailAddress == complaint.customerEmail && b.isPreferredEmail == true && b.customer_id==complaint.customerId);//if he enters same number with already active no action needed
                            if (result == null)//if he adds ne phone i.e record does not exists
                            {
                                var checkifEmailWxist = db.emails.SingleOrDefault(b => b.emailAddress == complaint.customerEmail && b.customer_id == complaint.customerId);//checks whether enter phone number matches any of his previous phone number
                                if (checkifEmailWxist != null) //if exists
                                {
                                    db.emails.Where(x => x.customer_id == complaint.customerId).ToList().ForEach(x =>{x.isPreferredEmail = false;});
                                    checkifEmailWxist.isPreferredEmail = true;//make that phone number as active
                                    db.SaveChanges();
                                }
                                else
                                {
                                    db.emails.Where(x => x.customer_id == complaint.customerId).ToList().ForEach(x =>{ x.isPreferredEmail = false;});
                                    //create new phone number
                                    email em = new email();
                                    em.customer_id = complaint.customerId;
                                    em.emailAddress = complaint.customerEmail;
                                    em.isPreferredEmail = true;
                                    db.emails.Add(em);
                                }
                            }
                        }
                        else  //if not found any phone number add one
                        {
                            email em = new email();
                            em.customer_id = complaint.customerId;
                            em.emailAddress = complaint.customerEmail;
                            em.isPreferredEmail = true;
                            db.emails.Add(em);

                        }

                        //For Address
                        int countAdd = db.addresses.Count(x => x.customer_Id == complaint.customerId);//Checking Whether Customer Has any Email
                        if (countAdd > 0)
                        {
                            var result = db.addresses.FirstOrDefault(b => b.concatenatedAdress == complaint.concatenatedAdress && b.isPreferred == true);//if he enters same number with already active no action needed
                            if (result == null)//if he adds ne phone i.e record does not exists
                            {
                                var checkifAddressExist = db.addresses.SingleOrDefault(b => b.concatenatedAdress == complaint.concatenatedAdress);//checks whether enter phone number matches any of his previous phone number
                                if (checkifAddressExist != null) //if exists
                                {
                                    db.addresses.Where(x => x.customer_Id == complaint.customerId).ToList().ForEach(x =>
                                    {
                                        x.isPreferred = false;
                                    });
                                    checkifAddressExist.isPreferred = true;//make that phone number as active
                                    db.SaveChanges();
                                }
                                else
                                {

                                    db.addresses.Where(x => x.customer_Id == complaint.customerId).ToList().ForEach(x =>
                                    {
                                        x.isPreferred = false;
                                    });
                                    //create new phone number
                                    address add = new address();
                                    add.customer_Id = complaint.customerId;
                                    add.concatenatedAdress = complaint.concatenatedAdress;
                                    add.isPreferred = true;
                                    db.addresses.Add(add);
                                }
                            }
                        }
                        else  //if not found any phone number add one
                        {
                            address add = new address();
                            add.customer_Id = complaint.customerId;
                            add.concatenatedAdress = complaint.concatenatedAdress;
                            add.isPreferred = true;
                            db.addresses.Add(add);

                        }
                        cmplnt.vehicle_vehicle_id = complaint.vehicleId;
                        cmplnt.customerPhone = complaint.customerMobileNo;
                        cmplnt.lastServiceDate = complaint.lastServiceDate;
                        cmplnt.serviceadvisor = complaint.serviceadvisor;
                        cmplnt.workshop = complaint.workshop;
                        cmplnt.sourceName = complaint.sourceName;
                        cmplnt.functionName = complaint.functionName;
                        cmplnt.description = complaint.reasonFor;
                        cmplnt.wyzUser_id = complaint.wyzUser;

                        var cityid = db.workshops.FirstOrDefault(x => x.id.ToString() == cmplnt.workshop);


                        cmplnt.location_cityId = cityid.id;
                        cmplnt.customer_id = complaint.customerId;
                        cmplnt.complaintType = complaint.complaintType;
                        cmplnt.subcomplaintType = complaint.subcomplaintType;


                        db.complaints.Add(cmplnt);
                        db.SaveChanges();
                        return Json(new { success = true });
                    }
                    else if (!complaint.isComplaintExist)
                    {
                        using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
                        {
                            try
                            {
                                int count = db.vehicles.Where(x => x.vehicleRegNo == complaint.vehicleRegNo).Count();
                                if (count > 0)
                                {
                                    return Json(new { success = false, Message = "Vehicle already registered! please hit Search Icon" });
                                }
                                customer cust = new customer();
                                email em = new email();
                                address addr = new address();
                                phone ph = new phone();
                                vehicle vh = new vehicle();



                                cust.customerName = complaint.customerName;
                                cust.dbvehregno = complaint.vehicleRegNo;
                                cust.createdDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                db.customers.Add(cust);
                                db.SaveChanges();

                                vh.customerId = cust.id.ToString();
                                vh.model = complaint.model;
                                vh.variant = complaint.variant;
                                vh.saleDate = complaint.saleDate;
                                vh.lastServiceDate = complaint.lastServiceDate;
                                vh.vehicleWorkshop_id = Convert.ToInt64(complaint.workshop);
                                vh.chassisNo = complaint.chassisNo;
                                db.vehicles.Add(vh);

                                ph.customer_id = cust.id;
                                ph.phoneNumber = complaint.customerMobileNo;
                                ph.isPreferredPhone = true;
                                db.phones.Add(ph);

                                addr.customer_Id = cust.id; ;
                                addr.concatenatedAdress = complaint.concatenatedAdress;
                                addr.isPreferred = true;
                                db.addresses.Add(addr);

                                em.customer_id = cust.id;
                                em.emailAddress = complaint.customerEmail;
                                em.isPreferredEmail = true;
                                db.emails.Add(em);


                                cmplnt.complaintNumber = complaintNumber;
                                cmplnt.vehicle_vehicle_id = vh.vehicle_id;
                                cmplnt.vehicleRegNo = complaint.vehicleRegNo;
                                cmplnt.customerName = complaint.customerName;
                                cmplnt.customerPhone = complaint.customerMobileNo;
                                cmplnt.lastServiceDate = complaint.lastServiceDate;
                                cmplnt.serviceadvisor = complaint.serviceadvisor;
                                cmplnt.workshop = complaint.workshop;
                                cmplnt.sourceName = complaint.sourceName;
                                cmplnt.functionName = complaint.functionName;
                                cmplnt.description = complaint.reasonFor;
                                cmplnt.wyzUser_id = Convert.ToInt32(Session["UserId"].ToString());
                                cmplnt.complaintType = complaint.complaintType;
                                cmplnt.subcomplaintType = complaint.subcomplaintType;

                                long workshop = Convert.ToInt64(cmplnt.workshop);
                                var cityid = db.workshops.FirstOrDefault(x => x.id == workshop);

                                cmplnt.location_cityId = Convert.ToInt64(cityid.id);
                                cmplnt.customer_id = cust.id;
                                db.complaints.Add(cmplnt);

                                db.SaveChanges();
                                dbTransaction.Commit();

                                return Json(new { success = true });
                            }
                            catch (Exception ex)
                            {
                                dbTransaction.Rollback();
                                return Json(new { success = false, Message = "Error Insering Reccords" });
                            }
                        }
                    }

                }

            }
            catch (Exception e)
            {

            }
            return View();
        }

        public ActionResult fetchComplaint(string vehicleRegNo)
        {
            try
            {
                ComplaintViewModel complaints = new ComplaintViewModel();
                // List<complaint>  = new List<complaint>();
                using (var db = new AutoSherDBContext())
                {

                    var vehicles = (from v in db.vehicles where v.vehicleRegNo == vehicleRegNo select v).FirstOrDefault();
                    int count = db.vehicles.Where(x => x.vehicleRegNo == vehicleRegNo).Count();
                    if (count > 0)
                    {
                        complaints.model = vehicles.model;
                        complaints.variant = vehicles.variant;
                        complaints.saleDate = vehicles.saleDate;
                        complaints.lastServiceDate = vehicles.lastServiceDate;
                        complaints.chassisNo = vehicles.chassisNo;
                        complaints.vehicleId = vehicles.vehicle_id;

                        //complaints.workshop = (from w in db.workshops where w.id == vehicles.vehicleWorkshop_id select w.id).FirstOrDefault();
                        complaints.workshop = vehicles.vehicleWorkshop_id.ToString();

                        //    ViewBag.workshopList = db.workshops.ToList().Select(x => new SelectListItem{Value = x.id.ToString(),Text = x.workshopName,Selected = (x.id == vehicles.vehicleWorkshop_id)});

                        //    complaints.workshop = db.workshops.FirstOrDefault(e=>e.id==vehicles.vehicleWorkshop_id).ToString();
                        complaints.customerId = Convert.ToInt64(vehicles.customer_id);
                        complaints.location = Convert.ToInt64(vehicles.location_cityId);
                        complaints.wyzUser = Convert.ToInt32(Session["UserId"].ToString());
                        complaints.isComplaintExist = true;


                        long vehcleId = Convert.ToInt64(vehicles.vehicle_id);
                        long custId = Convert.ToInt64(vehicles.customer_id);

                        var customers = (from c in db.customers where c.id == custId select c).FirstOrDefault();

                        complaints.customerName = customers.customerName;

                        var phone = (from p in db.phones where p.customer_id == custId && p.isPreferredPhone select p).FirstOrDefault();
                        if (phone != null)
                        {
                            complaints.customerMobileNo = phone.phoneNumber;
                        }
                        var email = (from e in db.emails where e.customer_id == custId && e.isPreferredEmail select e).FirstOrDefault();
                        if (email != null)
                        {
                            complaints.customerEmail = email.emailAddress;
                        }
                        var addres = (from a in db.addresses where a.customer_Id == custId && a.isPreferred select a).FirstOrDefault();
                        if (addres != null)
                        {
                            complaints.concatenatedAdress = addres.concatenatedAdress;
                        }
                            return Json(new { success = true, data = complaints });

                    }
                    else
                    {

                        return Json(new { success = false });

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = false });

        }

        public ActionResult viewallComplaints()
        {
            ComplaintViewModel complaintViewModel = new ComplaintViewModel();

            using (var db = new AutoSherDBContext())
            {
                try
                {
                    var locationList = db.workshops.Select(work => new { id = work.id, name = work.workshopName }).ToList();
                    ViewBag.locationList = locationList;
                    var functionList = db.complaints.Select(com => new { id = com.functionName, name = com.functionName }).ToList();
                    ViewBag.functionList = functionList.Distinct();

                    complaintViewModel.Roles = db.roles.Distinct().ToList();


                }
                catch (Exception ex)
                {

                }

            }
            return View(complaintViewModel);
        }

        public ActionResult getComplaintsFilterDataByStatus(string filterData, long? loc, string func, string raisedDateIs, string endDate)
        {
            try
            {

                using (var db = new AutoSherDBContext())
                {
                    DateTime startdate = Convert.ToDateTime(raisedDateIs);
                    DateTime enddate = Convert.ToDateTime(endDate);
                    //var results = db.complaints.ToList();

                    //var stocks = db.complaints.AsQueryable();
                    //if (func != null && func != "" && func != "0") stocks = stocks.Where(s => s.functionName == func);
                    //if (loc != null && loc != 0) stocks = stocks.Where(s => s.location_cityId == loc);
                    //if (filterData != null && filterData != "" && filterData != "0") stocks = stocks.Where(s => s.complaintStatus == filterData);
                    //if (raisedDateIs != null && raisedDateIs!="") stocks = stocks.Where(s => s.RaisedDate.ToString() == raisedDateIs);
                    //if (endDate != null && endDate!="") stocks = stocks.Where(s => s.issueDate.ToString() == endDate);
                    //var result = stocks.ToList();
                    var result = db.complaints.Where(m => m.functionName == func && m.location_cityId == loc && m.complaintStatus == filterData && m.RaisedDate >= startdate && m.RaisedDate <= enddate).Select(m=>new {m.complaintNumber,m.issueDate,m.ageOfComplaint,m.wyzUser_id,m.sourceName,m.functionName,m.vehicleRegNo }).ToList();
                    return Json(new { success = true, data = result });
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult getComplaints(string complaintNum, string VehicleRegNo)
        {
            try
            {

                using (var db = new AutoSherDBContext())

                {

                    var Customer_id = (from c in db.complaints
                                       where c.complaintNumber.Equals(complaintNum)
                                       select c.customer_id).SingleOrDefault();

                    int userId = Convert.ToInt32(Session["UserId"].ToString());

                    var results = (from c in db.complaints.Where(x => x.complaintNumber == complaintNum)
                                   from wo in db.workshops.Where(x => x.id.ToString() == c.workshop).DefaultIfEmpty()
                                   from e in db.emails.Where(x => x.isPreferredEmail == true && x.customer_id == Customer_id).DefaultIfEmpty()
                                   from a in db.addresses.Where(x => x.isPreferred == true && x.customer_Id == Customer_id).DefaultIfEmpty()

                                   from p in db.phones.Where(x => x.isPreferredPhone == true && x.customer_id == Customer_id).DefaultIfEmpty()
                                   from v in db.vehicles.Where(x => x.vehicleRegNo == VehicleRegNo && x.customer_id == Customer_id).DefaultIfEmpty()
                                   select new
                                   {
                                       //    assignedUser = ci.assignedUser_id,
                                       concatenatedAdress = (a != null ? a.concatenatedAdress : null),
                                       phone = (p != null ? p.phoneNumber : null),
                                       email = (e != null ? e.emailAddress : null),
                                       vehicleNumber = c.vehicleRegNo,
                                       customerNames = c.customerName,
                                       mobileNum = c.customerPhone,
                                       chasiNum = v.chassisNo,
                                       models = v.model,
                                       variants = v.variant,
                                       saleDates = v.saleDate,
                                       lastService = v.lastServiceDate,
                                       serviceAdvisor = c.serviceadvisor,
                                       workshops = wo.workshopName,
                                       compNum = c.complaintNumber,
                                       compStatus = c.complaintStatus,
                                       function = c.functionName,
                                       source = c.sourceName,
                                       descriptions = c.description,
                                       subcat = c.subcomplaintType,
                                       category=c.complaintType
                                   }).ToList();

                           if (results == null)
                    {
                        // Do something if the id doesn't exist...
                    }
                    else
                    {
                        return Json(new { success = true, data = results });
                    }


                    return Json(new { success = true, data = results });
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public ActionResult getComplaintsHistory(string complaintNumber, string vehregnumber)
        {
            try
            {
                using (var db = new AutoSherDBContext())

                {
                    long count = db.complaintinteractions.Count(x => x.complaintNumber == complaintNumber);
                    if (count > 0)
                    {
                        db.Configuration.LazyLoadingEnabled = false;
                        var results = (db.complaintinteractions.Where(x => x.complaintNumber == complaintNumber).ToList());
                        return Json(new { success = true, data = results });
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = false });
        }


        public ActionResult ajaxCallToLoadCity()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    return Json(db.locations.Select(x => new { cityID = x.cityId, cityName = x.name }).OrderBy(m=>m.cityName).ToList(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        public ActionResult WorkShopByCityComplaints(long? city)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    return Json(db.workshops.Select(x => new{ID = x.id,Name = x.workshopName}).OrderBy(m=>m.Name).ToList(), JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {

            }

            return View();
        }
        public ActionResult getOpenComplaints(string complaintType)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    int wyzUser = Convert.ToInt32(Session["UserId"].ToString());
                    int count = db.complaints.Count(x => x.wyzUser_id == wyzUser);
                    if (count > 0)
                    {             
                        if(complaintType== "Open")
                        {
                            var opencomplntList = db.complaints.Where(s => (s.complaintStatus == "New" || s.complaintStatus == "Open") && s.wyzUser_id == wyzUser).Select(m => new { m.complaintNumber, m.complaintStatus, m.issueDate, m.ageOfComplaint, m.wyzUser_id, m.functionName, m.sourceName, m.subcomplaintType, m.vehicleRegNo }).ToList();
                            return Json(new { data = opencomplntList }, JsonRequestBehavior.AllowGet);

                        }
                        else if(complaintType== "Closed")
                        {
                            var closedcomplntList = db.complaints.Where(s => (s.complaintStatus == "Closed") && s.wyzUser_id == wyzUser).Select(m => new { m.complaintNumber, m.complaintStatus, m.issueDate, m.ageOfComplaint, m.wyzUser_id, m.functionName, m.sourceName, m.subcomplaintType, m.vehicleRegNo }).ToList();
                            return Json(new { data =closedcomplntList }, JsonRequestBehavior.AllowGet);

                        }



                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View();

        }

        public ActionResult resolveComplaint()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
            return View();
        }


        public ActionResult getOpenClosedComplaints(string complaintNum, string vehicleRegNum)
        {
            try
            {
                using (var db = new AutoSherDBContext())

                {
                    if(db.complaints.Count(m=>m.complaintNumber==complaintNum)>0)
                    { 
                        long? Customer_id = db.complaints.FirstOrDefault(m => m.complaintNumber == complaintNum).customer_id;

                    if (Customer_id != 0)
                    {
                        int userId = Convert.ToInt32(Session["UserId"].ToString());

                        var results = (from c in db.complaints.Where(x => x.complaintNumber == complaintNum)
                                       from loc in db.locations.Where(x => x.cityId == c.location_cityId).DefaultIfEmpty()
                                       from e in db.emails.Where(x => x.isPreferredEmail == true && x.customer_id == Customer_id).DefaultIfEmpty()
                                       from a in db.addresses.Where(x => x.isPreferred == true && x.customer_Id == Customer_id).DefaultIfEmpty()
                                       from assign in db.complaintassignedinteractions.Where(x => x.complaint_id.ToString() == complaintNum).DefaultIfEmpty()
                                       from p in db.phones.Where(x => x.isPreferredPhone == true && x.customer_id == Customer_id).DefaultIfEmpty()
                                       from v in db.vehicles.Where(x => x.vehicleRegNo == vehicleRegNum && x.customer_id == Customer_id).DefaultIfEmpty()
                                       from ci in db.complaintinteractions.Where(x => x.complaintNumber == complaintNum).DefaultIfEmpty()
                                       select new
                                       {
                                           compllaintStatus = c.complaintStatus,
                                           reasonfor = ci.reasonFor,
                                           actionTakens = ci.actionTaken,
                                           status = ci.customerstatus,
                                           desc = ci.description,
                                           comstat = ci.complaintStatus,
                                           city = loc.name,
                                           concatenatedAdress = (a != null ? a.concatenatedAdress : null),
                                           phone = (p != null ? p.phoneNumber : null),
                                           email = (e != null ? e.emailAddress : null),
                                           vehicleNumber = c.vehicleRegNo,
                                           customerNames = c.customerName,
                                           mobileNum = c.customerPhone,
                                           chasiNum = v.chassisNo,
                                           models = v.model,
                                           variants = v.variant,
                                           saleDates = v.saleDate,
                                           lastService = v.lastServiceDate,
                                           serviceAdvisor = c.serviceadvisor,
                                           workshops = c.workshop,
                                           compNum = c.complaintNumber,
                                           issueDates = c.issueDate,
                                           compStatus = c.complaintStatus,
                                           function = c.functionName,
                                           source = c.sourceName,
                                           descriptions = c.description,
                                           subcat = c.subcomplaintType,
                                           cat = c.complaintType
                                       }).ToList();

                        return Json(new { success = true, data = results });

                    }

                }
                }
            }
            catch (Exception ex)
            {
                string inner = ex.InnerException.Message;
            }
            return View();
        }

        public ActionResult UpdateComplaintResolution(string jsonData,string complaintNum)
        {
            try

            {
                complaintinteraction resolve = JsonConvert.DeserializeObject<complaintinteraction>(jsonData);

                using (var db = new AutoSherDBContext())
                {
                    var updateComplainStatus = db.complaints.SingleOrDefault(b => b.complaintNumber == complaintNum);
                    if (updateComplainStatus != null) //if exists
                    {
                        updateComplainStatus.complaintStatus = resolve.complaintStatus;
                        updateComplainStatus.customerstatus = resolve.customerstatus;
                        complaintinteraction inrctnt= new complaintinteraction();
                        inrctnt.UpdatedDate = DateTime.Now;
                        inrctnt.customer_id = updateComplainStatus.customer_id;
                        inrctnt.complaintStatus = resolve.complaintStatus;
                        inrctnt.reasonFor = resolve.reasonFor;
                        inrctnt.complaintNumber = complaintNum;
                        inrctnt.actionTaken = resolve.actionTaken;
                        db.complaintinteractions.Add(inrctnt);
                        db.SaveChanges();
                        return Json(new { success = true });


                    }
                }
            }
            catch (Exception ex)
            {

            }

return View();
 }
        [HttpPost]
        public ActionResult assignCre()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    
                }
            }
            catch(Exception ex)
            {

            }
            return View();
        }

        public ActionResult listOwnership(string func)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    //var wyzUser = db.wyzusers.Where(s => s.role1 == func).ToList();
                    var wyzUser = db.wyzusers.Where(s => s.role1 == func).Select(m=>new { name = m.userName, id =m.id}).ToList();
                    return Json(new { wyzUser });

                }
            }
            catch(Exception ex)
            {

            }
            return View();
        }



    }
}
