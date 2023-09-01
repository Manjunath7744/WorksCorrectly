using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoSherpa_project.Models;
using AutoSherpa_project.Models.ViewModels;
using MySql.Data.MySqlClient;

namespace AutoSherpa_project.Controllers
{
    [AuthorizeFilter]
    public class customerUpdateController : Controller
    {
        [HttpPost]
        public ActionResult Saves(CallLoggingViewModel CVM, string submit)
        {
            long cusId = Convert.ToInt32(Session["CusId"].ToString());

            string ddlAddress = string.Empty;


            try
            {

                using (var db = new AutoSherDBContext())
                {
                    submit = CVM.submittedBtnName;
                    if (submit.Contains('_'))
                    {
                        ddlAddress = submit.Split('_')[1];
                        submit = submit.Split('_')[0];

                    }

                    if (submit == "SavePhone")
                    {
                        bool result = phoneAdd(CVM);
                        if (result)
                        {
                            //var phoneList = db.phones.Where(e => e.customer_id == cusId).Select(e => new { phoneNo = e.phoneNumber, id = e.phone_Id, pref = e.isPreferredPhone }).ToList().OrderByDescending(m => m.pref == true);
                            List<phone> phoneList = db.Database.SqlQuery<phone>("select distinct(phoneNumber),phone_Id,isPreferredPhone,phoneTye,updatedBy,customer_id,upload_id,remarks from phone where customer_id=@custId group by phone_Id order by phone_Id desc limit 0,5;", new MySqlParameter("@custId", cusId)).ToList();
                            var newphoneList = phoneList.Select(e => new { phoneNo = e.phoneNumber, id = e.phone_Id, pref = e.isPreferredPhone }).ToList();
                            return Json(new { successPhone = true, phnum = newphoneList, id = CVM.phoneAdd.ddlId });

                        }
                        else
                        {
                            return Json(new { success = false });
                        }
                    }
                    else if (submit == "SaveEmail")
                    {

                        bool result = emailAdd(CVM);
                        if (result)
                        {
                            var emailList = db.emails.Where(e => e.customer_id == cusId).Select(e => new { email = e.emailAddress, id = e.email_Id, pref = e.isPreferredEmail }).ToList().OrderByDescending(m => m.pref == true);

                            return Json(new { successEmail = true, emailAddress = emailList, id = CVM.emailAdd.ddlId });
                        }

                        else
                        {
                            return Json(new { success = false });
                        }
                    }
                    else if (submit == "SaveAddress")
                    {
                        string result = addressAdd(CVM);
                        if (result != "false")
                        {
                            var address = db.addresses.Where(e => e.customer_Id == cusId && e.isPreferred == true).Select(e => new { address1 = e.concatenatedAdress, cty = e.city, pin = e.pincode }).ToList();

                            return Json(new { successAddress = true, address = address, id = CVM.addressesAdd.ddlId });
                        }
                        else
                        {
                            return Json(new { success = false });
                        }

                    }
                    else if (submit == "DeletePhone")
                    {
                        //var phNum = CVM.phoneAdd
                        //    bool result = deletePhone(CVM);
                        //if (result)
                        //{
                        //    return Json(new { successDelete = true });
                        //}
                        //else
                        //{
                        //    return Json(new { success = false });
                        //}
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }


        public string addressAdd(CallLoggingViewModel CVM)
        {
            int custId = Convert.ToInt32(Session["CusId"].ToString());
            int vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            int UserId = Convert.ToInt32(Session["UserId"].ToString());
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string addrs = CVM.addressesAdd.addressLine1 + "," + CVM.addressesAdd.addressLine2 + " " + CVM.addressesAdd.city; ;
                    int count = db.addresses.Count(c => c.customer_Id == custId);
                    if (count > 0)
                    {
                        db.addresses.Where(x => x.customer_Id == custId ).ToList().ForEach(x => { x.isPreferred = false; });
                        address add = new address();
                        add.customer_Id = custId;
                        add.concatenatedAdress = addrs;
                        add.city = CVM.addressesAdd.city;
                        add.state = CVM.addressesAdd.state;
                        add.pincode = CVM.addressesAdd.pincode;
                        add.addressType = 1;
                        add.isPreferred = true;
                        add.wyzUserName = Session["UserName"].ToString();
                        add.updatedDateTime = DateTime.Now;
                        db.addresses.Add(add);

                    }
                    else
                    {
                        address add = new address();
                        add.customer_Id = custId;
                        add.concatenatedAdress = addrs;
                        add.city = CVM.addressesAdd.city;
                        add.state = CVM.addressesAdd.state;
                        add.pincode = CVM.addressesAdd.pincode;
                        add.addressType = 1;
                        add.isPreferred = true;
                        add.wyzUserName = Session["UserName"].ToString();
                        add.updatedDateTime = DateTime.Now;
                        db.addresses.Add(add);
                    }
                    db.SaveChanges();
                    return addrs;
                }
            }
            catch (Exception ex)
            {

            }
            return "false";
        }

        public bool emailAdd(CallLoggingViewModel CVM)
        {
            int custId = Convert.ToInt32(Session["CusId"].ToString());
            int vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            int UserId = Convert.ToInt32(Session["UserId"].ToString());
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    int countEmail = db.emails.Count(x => x.customer_id == custId);
                    if (countEmail > 0)
                    {
                        var result = db.emails.FirstOrDefault(b => b.emailAddress == CVM.emailAdd.emailAddress && b.isPreferredEmail == true && b.customer_id == custId);//if he enters same number with already active no action needed
                        if (result == null)//if he adds ne phone i.e record does not exists
                        {
                            var checkifEmailWxist = db.emails.SingleOrDefault(b => b.emailAddress == CVM.emailAdd.emailAddress && b.customer_id == custId);//checks whether enter phone number matches any of his previous phone number
                            if (checkifEmailWxist != null) //if exists
                            {
                                db.emails.Where(x => x.customer_id == custId && x.isPreferredEmail==true).ToList().ForEach(x =>
                                {
                                    x.isPreferredEmail = false;
                                });
                                checkifEmailWxist.isPreferredEmail = true;//make that phone number as active
                                db.SaveChanges();
                                return true;
                            }
                            else
                            {

                                db.emails.Where(x => x.customer_id == custId && x.isPreferredEmail==true).ToList().ForEach(x =>
                                 {
                                     x.isPreferredEmail = false;
                                 });
                                //create new phone number
                                email em = new email();
                                em.customer_id = custId;
                                em.emailAddress = CVM.emailAdd.emailAddress;
                                em.updatedBy = Session["UserName"].ToString();
                                em.isPreferredEmail = true;
                                db.emails.Add(em);
                                db.SaveChanges();
                                return true;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else  //if not found any phone number add one
                    {
                        email em = new email();
                        em.customer_id = custId;
                        em.emailAddress = CVM.emailAdd.emailAddress;
                        em.updatedBy = Session["UserName"].ToString();
                        em.isPreferredEmail = true;
                        db.emails.Add(em);
                        db.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public ActionResult deletePhone(long phnId, string remarks)
        {
            int custId = Convert.ToInt32(Session["CusId"].ToString());
            int vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            int UserId = Convert.ToInt32(Session["UserId"].ToString());
            try
            {

                using (var db = new AutoSherDBContext())
                {
                    var phNum = phnId;

                    var checkifPhonexist = db.phones.SingleOrDefault(b => b.phone_Id == phNum && b.customer_id == custId);
                    if (checkifPhonexist != null)
                    {
                        if (checkifPhonexist.isPreferredPhone == true)
                        {
                            //return Json(new { success = false });
                            phonenodeletion phonenodeletion = new phonenodeletion();
                            phonenodeletion.customerId = custId;
                            phonenodeletion.userId = UserId;
                            phonenodeletion.remarks = remarks;
                            phonenodeletion.phoneNumber = checkifPhonexist.phoneNumber;
                            phonenodeletion.deletedDate = DateTime.Now;
                            db.phonenodeletions.Add(phonenodeletion);

                            db.phones.Remove(checkifPhonexist);
                            db.SaveChanges();

                            db.phones.Where(m => m.customer_id == custId).ToList().ForEach(k => k.isPreferredPhone = false);
                            db.SaveChanges();

                            var firstPh = db.phones.FirstOrDefault(m => m.customer_id == custId);
                            firstPh.isPreferredPhone = true;
                            db.SaveChanges();

                            var phoneList = db.phones.Where(e => e.customer_id == custId).Select(e => new { phoneNo = e.phoneNumber, id = e.phone_Id, pref = e.isPreferredPhone }).ToList().OrderByDescending(m => m.pref == true);
                            return Json(new { success = true, phnum = phoneList });

                        }
                        else
                        {
                            phonenodeletion phonenodeletion = new phonenodeletion();
                            phonenodeletion.customerId = custId;
                            phonenodeletion.userId = UserId;
                            phonenodeletion.remarks = remarks;
                            phonenodeletion.phoneNumber = checkifPhonexist.phoneNumber;
                            phonenodeletion.deletedDate = DateTime.Now;
                            db.phonenodeletions.Add(phonenodeletion);

                            db.phones.Remove(checkifPhonexist);
                            db.SaveChanges();


                            var phoneList = db.phones.Where(e => e.customer_id == custId).Select(e => new { phoneNo = e.phoneNumber, id = e.phone_Id, pref = e.isPreferredPhone }).ToList().OrderByDescending(m => m.pref == true);
                            return Json(new { success = true, phnum = phoneList });

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = false });
        }

        public bool phoneAdd(CallLoggingViewModel CVM)
        {
            int custId = Convert.ToInt32(Session["CusId"].ToString());
            int vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            int UserId = Convert.ToInt32(Session["UserId"].ToString());
            try
            {

                using (var db = new AutoSherDBContext())
                {
                    int count = db.phones.Count(x => x.customer_id == custId);
                    if (count > 0)
                    {
                        var result = db.phones.FirstOrDefault(b => b.phoneNumber == CVM.phoneAdd.phoneNumber && b.isPreferredPhone == true && b.customer_id == custId);//if he adds ne phone i.e record does not exists
                        if (result == null)
                        {
                            var checkifPhoneWxist = db.phones.SingleOrDefault(b => b.phoneNumber == CVM.phoneAdd.phoneNumber && b.customer_id == custId);//checks whether enter phone number matches any of his previous phone number
                            if (checkifPhoneWxist != null) //if exists
                            {
                                db.phones.Where(x => x.customer_id == custId && x.isPreferredPhone == true).ToList().ForEach(x =>
                                {
                                    x.isPreferredPhone = false;
                                });
                                checkifPhoneWxist.isPreferredPhone = true;//make that phone number as active
                                checkifPhoneWxist.remarks = CVM.phoneAdd.remarks;
                                db.SaveChanges();
                                return true;
                            }
                            else
                            {

                                db.phones.Where(x => x.customer_id == custId && x.isPreferredPhone == true).ToList().ForEach(x =>
                                {
                                    x.isPreferredPhone = false;
                                });
                                //create new phone number
                                phone ph = new phone();
                                ph.customer_id = custId;
                                ph.updatedBy = Session["UserName"].ToString();
                                ph.phoneNumber = CVM.phoneAdd.phoneNumber;
                                ph.isPreferredPhone = true;
                                ph.remarks = CVM.phoneAdd.remarks;
                                db.phones.Add(ph);
                                db.SaveChanges();

                            }

                            //phone ph = new phone();
                            //ph.customer_id = custId;
                            //ph.updatedBy = Session["UserName"].ToString();
                            //ph.phoneNumber = CVM.phoneAdd.phoneNumber;
                            //ph.isPreferredPhone = true;
                            //ph.remarks = CVM.phoneAdd.remarks;
                            //db.phones.Add(ph);
                            //db.SaveChanges();
                            return true;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else  //if not found any phone number add one
                    {
                        phone ph = new phone();
                        ph.customer_id = custId;
                        ph.updatedBy = Session["UserName"].ToString();
                        ph.phoneNumber = CVM.phoneAdd.phoneNumber;
                        ph.remarks = CVM.phoneAdd.remarks;
                        ph.isPreferredPhone = true;
                        db.phones.Add(ph);
                        db.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public ActionResult customerSave(CallLoggingViewModel CVM)
        {
            Dictionary<string, string> prefferences = new Dictionary<string, string>();
            CVM.cust.leadtag = CVM.selectedTagList?.Count() > 0 ? String.Join(",", CVM.selectedTagList) : CVM.cust.leadtag;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    int custId = Convert.ToInt32(Session["CusId"].ToString());
                    int vehId = Convert.ToInt32(Session["VehiId"].ToString());
                    customer updateCustomer = db.customers.FirstOrDefault(b => b.id == custId);

                    if (updateCustomer != null)
                    {
                        updateCustomer.anniversary_date = CVM.cust.anniversary_date;
                        updateCustomer.userDriver = CVM.cust.userDriver;
                        updateCustomer.customerName = CVM.cust.customerName;
                        updateCustomer.doNotDisturb = CVM.cust.doNotDisturb;
                        updateCustomer.dob = CVM.cust.dob;
                        updateCustomer.mode_of_contact = CVM.cust.mode_of_contact;
                        updateCustomer.preferred_day = CVM.cust.preferred_day;
                        updateCustomer.preferred_time_start = CVM.cust.preferred_time_start;
                        updateCustomer.preferred_time_end = CVM.cust.preferred_time_end;
                        updateCustomer.leadtag = CVM.cust.leadtag;
                        updateCustomer.pancardno = CVM.cust.pancardno;
                        updateCustomer.ckycno = CVM.cust.ckycno;
                        // db.customers.AddOrUpdate(updateCustomer);
                        //updateCustomer.id = custId;
                        //updateCustomer.id = custId;
                        db.customers.AddOrUpdate(updateCustomer);
                        db.SaveChanges();

                    }
                    if ((Session["DealerCode"].ToString() == "INDUS"))
                    {
                        if (CVM.callinteraction != null && CVM.callinteraction.insuranceassignedinteraction.policyDueDate != null)
                        {
                            int countpolicyduedate = db.insuranceassignedinteractions.Count(m => (m.campaign_id == 31 || m.campaign_id == 32) && m.vehicle_vehicle_id == vehId && m.policyDueDate != CVM.callinteraction.insuranceassignedinteraction.policyDueDate);
                            if (countpolicyduedate > 0)
                            {
                                insuranceassignedinteraction assInter = db.insuranceassignedinteractions.FirstOrDefault(m => (m.campaign_id == 31 || m.campaign_id == 32) && m.vehicle_vehicle_id == vehId);
                                assInter.policyDueDate = CVM.callinteraction.insuranceassignedinteraction.policyDueDate;
                                db.insuranceassignedinteractions.AddOrUpdate(assInter);

                                vehicle veh = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehId);
                                veh.policy_updateddate = DateTime.Now;
                                veh.policyDueDate = CVM.callinteraction.insuranceassignedinteraction.policyDueDate;
                                veh.policy_updatedby = Convert.ToInt64(Session["UserId"]);
                                db.vehicles.AddOrUpdate(veh);
                                db.SaveChanges();
                                prefferences.Add("dueDate", Convert.ToDateTime(assInter.policyDueDate).ToString("dd-MM-yyyy"));

                            }
                            else
                            {
                                prefferences.Add("dueDate", "None");

                            }
                        }
                        else
                        {
                            prefferences.Add("dueDate", "None");

                        }
                    }
                    long prefferedPhoneId = 0, prefferedEmailId = 0;
                    string prefferdEmailAddress = "";

                    if (CVM.prefferedEmail != 0)
                    {
                        if (db.emails.Any(m => m.customer_id == custId && m.isPreferredEmail == true))
                        {
                            email emai = new email();
                            prefferdEmailAddress = db.emails.FirstOrDefault(m => m.customer_id == custId && m.isPreferredEmail == true).emailAddress;
                            prefferedEmailId = db.emails.FirstOrDefault(m => m.customer_id == custId && m.isPreferredEmail == true).email_Id;
                            if (prefferedEmailId != CVM.prefferedEmail)
                            {
                                //emai = db.emails.FirstOrDefault(m => m.customer_id == custId && m.email_Id == prefferedEmailId);
                                //emai.isPreferredEmail = false;
                                //db.emails.AddOrUpdate(emai);
                                //db.SaveChanges();
                                db.emails.Where(x => x.customer_id == custId && x.isPreferredEmail == true).ToList().ForEach(x =>
                                {
                                    x.isPreferredEmail = false;
                                });

                                emai = db.emails.FirstOrDefault(m => m.email_Id == CVM.prefferedEmail);
                                emai.isPreferredEmail = true;
                                db.emails.AddOrUpdate(emai);
                                db.SaveChanges();
                                prefferences.Add("Email", emai.emailAddress);
                            }
                            else
                            {
                                prefferences.Add("Email", "None");
                            }
                        }
                        else
                        {
                            email email = new email();
                            email = db.emails.FirstOrDefault(m => m.customer_id == custId && m.email_Id == CVM.prefferedEmail);
                            email.isPreferredEmail = true;
                            db.emails.AddOrUpdate(email);
                            db.SaveChanges();

                            prefferences.Add("Email", email.emailAddress);
                        }
                    }
                    if (CVM.prefferedPhone != 0)
                    {
                        if (db.phones.Any(m => m.customer_id == custId && m.isPreferredPhone == true))
                        {
                            phone ph = new phone();
                            prefferedPhoneId = db.phones.FirstOrDefault(m => m.customer_id == custId && m.isPreferredPhone == true).phone_Id;

                            if (prefferedPhoneId != CVM.prefferedPhone)
                            {
                                //ph = db.phones.FirstOrDefault(m => m.phone_Id == prefferedPhoneId);
                                //ph.isPreferredPhone = false;
                                //db.phones.AddOrUpdate(ph);
                                //db.SaveChanges();
                                db.phones.Where(x => x.customer_id == custId && x.isPreferredPhone==true).ToList().ForEach(x =>
                                {
                                    x.isPreferredPhone = false;
                                });

                                ph = db.phones.FirstOrDefault(m => m.phone_Id == CVM.prefferedPhone);
                                ph.isPreferredPhone = true;
                                db.phones.AddOrUpdate(ph);
                                db.SaveChanges();
                                prefferences.Add("Phone", CVM.prefferedPhone.ToString());
                            }
                            else
                            {
                                prefferences.Add("Phone", "None");
                            }
                        }
                        else
                        {
                            phone ph = new phone();
                            ph = db.phones.FirstOrDefault(m => m.phone_Id == CVM.prefferedPhone);
                            ph.isPreferredPhone = true;
                            db.phones.AddOrUpdate(ph);
                            db.SaveChanges();

                            prefferences.Add("Phone", CVM.prefferedPhone.ToString());
                        }
                    }
                    var res = db.customers.Where(m => m.id == custId).Select(m => new { owner = m.customerName, driver = m.userDriver, dobi = m.dob, aniver = m.anniversary_date, timeStart = m.preferred_time_start, timeEnd = m.preferred_time_end, dnd = m.doNotDisturb, dyCon = m.preferred_day, dytype = m.mode_of_contact }).ToList();

                    return Json(new { success = true, details = res, prefer = prefferences });

                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message.ToString() });
            }

        }

        public ActionResult getAddressDetails(long? custId)
        {
            try
            {


                using (var db = new AutoSherDBContext())
                {
                    db.Configuration.ProxyCreationEnabled = true;
                    db.Configuration.LazyLoadingEnabled = false;
                    var customerList = db.customers.FirstOrDefault(x => x.id == custId);
                    var phoneList = db.phones.Where(x => x.customer_id == custId).ToList();
                    List<address> addList = db.addresses.Where(x => x.customer_Id == custId && x.isPreferred == true).ToList();
                    return Json(new { success = true, cust = customerList }, JsonRequestBehavior.AllowGet);



                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }
    }
}