using AutoSherpa_project.Models;
using AutoSherpa_project.Models.ViewModels;
using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoSherpa_project.Controllers
{
    [AuthorizeFilter]
    public class SACallLoggingController : Controller
    {
        // GET: SACallLogging
        [ActionName("SACallLogging"), HttpGet]

        public ActionResult SACallLogging(string id)
        {
            SACalllogingVM sacallLog = new SACalllogingVM();

            try
            {
                Session["CusId"] = null;
                Session["VehiId"] = null;

                int UserId = Convert.ToInt32(Session["UserId"].ToString());

                sacallLog.wyzuser = new wyzuser();
                sacallLog.cust = new customer();
                sacallLog.vehi = new vehicle();
                sacallLog.phonesAdd = new List<phone>();
                sacallLog.rosaassignment = new rosaassignment();
                sacallLog.LatestInsurance = new insurance();
                sacallLog.allworkshopList = new List<workshop>();
                sacallLog.citystatesList = new List<SelectListItem>();
                sacallLog.stateList = new List<SelectListItem>();
                sacallLog.custPhoneList = new List<phone>();
                sacallLog.companiesList = new List<insurancecompany>();
                sacallLog.emailtemplates = new List<emailtemplate>();
                sacallLog.locationList = new List<location>();
                sacallLog.smstemplates = new List<smstemplate>();
                sacallLog.rsacoverageLists = new List<rsacoverage>();
                sacallLog.ewcoverageLists = new List<ewcoverage>();

                for (int i = 0; i < 4; i++)
                {
                    sacallLog.phonesAdd.Add(new phone());
                }

                if (id.Contains(','))
                {
                    Session["CusId"] = Convert.ToInt32(id.Split(',')[2]);
                    Session["VehiId"] = Convert.ToInt32(id.Split(',')[3]);
                    int cusid = Convert.ToInt32(id.Split(',')[2]);
                    int vehId = Convert.ToInt32(id.Split(',')[3]);
                    int roassignmentId = Convert.ToInt32(id.Split(',')[4]);
                    //long roassignmentId = 0;
                    string DealerCode = Session["DealerCode"].ToString();
                    string OEM = Session["OEM"].ToString();
                    ViewBag.vehiId = vehId;
                    sacallLog.CustomerId = cusid;
                    sacallLog.VehicleId = vehId;
                    sacallLog.UserId = UserId;
                    sacallLog.insurance = "No";
                    sacallLog.newcar = "No";
                    sacallLog.rsa = "No";
                    sacallLog.ew = "No";
                    //sacallLog.rosaassignedId = roassignmentId;
                    double dateDiff=0;
                    using (var db = new AutoSherDBContext())
                    {
                        if(roassignmentId!=0)
                        {
                            sacallLog.rosaassignedId = roassignmentId;
                            sacallLog.enableDispositionForm = true;
                        }
                        else
                        {
                            sacallLog.enableDispositionForm = false;
                        }
                        //else
                        //{
                        //    rosaassignment rosaassignments = new rosaassignment();
                        //    rosaassignments.assigneddatetime = DateTime.Now;
                        //    rosaassignments.repairorderdetails_id = 0;
                        //    rosaassignments.upload_id = 0;
                        //    rosaassignments.wyzuser_id = UserId;
                        //    if(db.vehicles.Count(m=>m.vehicle_id==vehId)>0)
                        //    {
                        //        var vehicleDetails = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehId);
                        //        rosaassignments.vehicle_id = Convert.ToInt64(vehId);
                        //        rosaassignments.vehiclereg_no = vehicleDetails.vehicleRegNo;
                        //        rosaassignments.chassisno = vehicleDetails.chassisNo;
                        //        rosaassignments.saledate = vehicleDetails.saleDate;
                        //        rosaassignments.mileage = Convert.ToInt64(vehicleDetails.odometerReading);
                        //        rosaassignments.workshop_id = vehicleDetails.vehicleWorkshop_id;
                        //        rosaassignments.servicetype =vehicleDetails.nextServicetype ;

                        //        rosaassignments.jobcardlocation =  vehicleDetails.chassisNo;
                        //    }
                        //    if(db.services.Count(m=>m.vehicle_vehicle_id==vehId)>0)
                        //    {
                        //        var serviceDetails = db.services.OrderByDescending(m => m.id).FirstOrDefault(m => m.vehicle_vehicle_id == vehId);
                        //        rosaassignments.visittypes = serviceDetails.lastVisitType;
                        //        rosaassignments.serviceadvisor_name = serviceDetails.saName ;
                        //        rosaassignments.technician = serviceDetails.technician;

                        //    }
                        //    if (db.customers.Count(m=>m.id==cusid)>0)
                        //    {
                        //        var customerDetails = db.customers.FirstOrDefault(m => m.id == cusid);
                        //        rosaassignments.customer_id = cusid;
                        //        rosaassignments.customername = customerDetails.customerName;
                        //    }
                        //    //rosaassignments.rodate = ;
                        //    rosaassignments.rostatus = "OPEN";
                        //    rosaassignments.ismanuallyCreated = true;
                        //    //rosaassignments.ronumber = ;

                        //    db.rosaAssignments.Add(rosaassignments);
                        //    db.SaveChanges();
                        //    sacallLog.rosaassignedId = rosaassignments.id;
                        //    sacallLog.enableDispositionForm = rosaassignments.ismanuallyCreated;
                        //}
                        sacallLog.wyzuser = db.wyzusers.Include("location").Include("workshop ").SingleOrDefault(m => m.id == UserId);
                        sacallLog.cust = db.customers.Include("emails").Include("addresses").Include("vehicles").Include("insurances").FirstOrDefault(m => m.id == cusid);
                        sacallLog.vehi = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehId);
                        sacallLog.ceicustCat = db.Database.SqlQuery<string>("select if(saledate between date_sub(curdate(), interval 3 year)  and date_sub(curdate(), interval 1 year),'CEI','NONCEI') as custcat   from vehicle where  vehicle_id=@id;", new MySqlParameter("@id", vehId)).FirstOrDefault();
                        sacallLog.allworkshopList = db.workshops.Where(m => m.isinsurance == false).ToList();
                        sacallLog.custPhoneList = db.Database.SqlQuery<phone>("select * from phone p join(select max(phone_Id)phid from phone where customer_id=@custId group by phoneNumber)Ph on ph.phid=p.phone_Id and p.customer_id=@custId order by phone_Id desc Limit 0,5", new MySqlParameter("@custId", cusid)).ToList();
                        sacallLog.emailtemplates = new CallLoggingController().getEmailTemplateList("Service");
                        sacallLog.locationList = new CallLoggingController().getLocationByDispoType("Service");
                        sacallLog.rsacoverageLists = db.Rsacoverages.OrderBy(m=>m.coverage).ToList();
                        sacallLog.ewcoverageLists = db.Ewcoverages.OrderBy(m=>m.coverage).ToList();
                        if (DealerCode != "HARPREETFORD" && DealerCode != "HANSHYUNDAI" && DealerCode != "GALAXYTOYOTA" && DealerCode != "TOYOTADEMO")
                        {
                            sacallLog.smstemplates = new CallLoggingController().getSMSTemplate("Service");
                        }
                        if (db.insurances.Where(m => m.vehicle != null && m.vehicle.vehicle_id == vehId).Count() != 0)
                        {
                            var lastInsurence = db.insurances.Where(m => m.vehicle != null && m.vehicle.vehicle_id == vehId).OrderByDescending(k => k.policyIssueDate).FirstOrDefault();

                            if (lastInsurence != null)
                            {
                                sacallLog.LatestInsurance = lastInsurence;
                            }
                        }

                        var Viewcitystates = db.citystates.Select(h => new { state = h.state }).OrderBy(m => m.state).Distinct().ToList();
                        var newList = Viewcitystates.OrderBy(x => x.state).ToList(); // ToList optional

                        foreach (var city in newList)
                        {
                            sacallLog.citystatesList.Add(new SelectListItem { Text = city.state, Value = city.state });
                            sacallLog.stateList.Add(new SelectListItem { Text = city.state, Value = city.state });
                        }
                        sacallLog.companiesList = db.insurancecompanies.ToList();
                        if(db.saInteractions.Count(m=>m.roassignid==roassignmentId)>0)
                        {
                            sacallLog.Sainteraction = db.saInteractions.OrderByDescending(m => m.id).FirstOrDefault(m=>m.roassignid == roassignmentId);
                        }
                        if (db.rosaAssignments.Count(m => m.id == sacallLog.rosaassignedId) >0)
                        {
                            sacallLog.rosaassignment = db.rosaAssignments.FirstOrDefault(m => m.id == sacallLog.rosaassignedId);
                            if (sacallLog.rosaassignment.servicetype == "Accidental Repair" || sacallLog.rosaassignment.servicetype == "RF Accidental")
                            {
                                sacallLog.roreasons = db.Database.SqlQuery<roreasons>("select * from roreasons where mainreasonId in(select id from roreasons where reason like '%ACCIDENTAL_REPAIR%');").ToList();

                            }
                            else
                            {
                                sacallLog.roreasons = db.Database.SqlQuery<roreasons>("select * from roreasons where mainreasonId in(select id from roreasons where reason like '%NOTACCIDENTAL_NOTREPAIR_RF_ACCIDENTAL%');").ToList();
                            }
                        }
                        sacallLog.roModels = db.modelslists.OrderBy(m=>m.model).ToList();
                        DateTime todayDate = DateTime.Now.AddDays(90);

                        //if(db.insuranceforecasteddatas.Count(m=>m.vehicle_id==vehId)>0)
                        //{
                        //    DateTime dueDate = db.insuranceforecasteddatas.FirstOrDefault(m => m.vehicle_id == vehId).policyexpirydate??(default(DateTime));
                        //     dateDiff = (todayDate.Date - dueDate.Date).TotalDays;
                        //    if (dateDiff > 0 && dateDiff <= 90)
                        //    {
                        //        sacallLog.insurance = "Yes";
                        //    }
                        //}

                        sacallLog.insurance = db.Database.SqlQuery<string>("select if(month((saledate))  between   month(curdate()) and    month(date_add(curdate(), interval 90 day)),'Yes','No') from vehicle  where vehicle_id =@id;", new MySqlParameter("@id", vehId)).FirstOrDefault();


                        if (sacallLog.rosaassignment.saledate!=null)
                        {
                            DateTime saledate = sacallLog.rosaassignment.saledate ?? (default(DateTime));
                             dateDiff = (todayDate.Date - saledate.Date).TotalDays;
                          
                            
                            //string s1 = todayDate.Day + "/" + todayDate.Month;
                            //string s2 = saledate.Day + "/" + saledate.Month;
                            //DateTime d1 = DateTime.Parse(s1 + "/2014");
                            //DateTime d2 = DateTime.Parse(s2 + "/2014");
                           
                            //double insdatediff= (d1.Date - d2.Date).TotalDays;

                            // if(sacallLog.insurance=="No")
                            //{
                                //if(insdatediff <= 90)
                                //{
                                //    sacallLog.insurance = "Yes";
                                //}
                            //}
                            if(dateDiff>2555)
                            {
                                sacallLog.newcar = "Yes";
                            }
                            if(dateDiff<=1095)
                            {
                                sacallLog.ew = "Yes";
                            }
                            if(dateDiff>1095 && dateDiff<1825)
                            {
                                sacallLog.rsa = "Yes";
                            }
                        }                     
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Exceptions"] = ex.Message.ToString();
                if (ex.Message.Contains("inner exception"))
                {
                    TempData["Exceptions"] = ex.InnerException.Message;
                }
                TempData["ControllerName"] = "RO Call Log";
                return RedirectToAction("LogOff", "Home");
            }

            return View(sacallLog);
        }

        public ActionResult SACallLoggingPost(SACalllogingVM callLogging)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");

            string submissionResult = string.Empty;
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());

            if (cusId != callLogging.CustomerId && vehiId != callLogging.VehicleId)
            {
                TempData["Exceptions"] = "Invalid disposition...";

                logger.Info("\n\n------- SA Invalid Submition: " + DateTime.Now + "\n Disposing CustId: " + cusId + "Disposing VehId: " + vehiId + " User: " + Session["UserName"].ToString() + "\n Prev-Open Cust: " + callLogging.CustomerId + " Prev-Open VehiId" + callLogging.VehicleId);
                return RedirectToAction("LogOff", "Home");
            }

            logger.Info("\n\n-------  SA Submition started : " + DateTime.Now + "\n CustId: " + cusId + " VehId: " + vehiId + " User: " + Session["UserName"].ToString());
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    callLogging.Sainteraction.vehicle_id = vehiId;
                    callLogging.Sainteraction.customer_id = cusId;
                    callLogging.Sainteraction.calldate = DateTime.Now.Date;
                    callLogging.Sainteraction.calltime = DateTime.Now.ToString("HH:mm:ss");
                    callLogging.Sainteraction.callmadeon = DateTime.Now;
                    if(callLogging.Sainteraction.reasonforpendingro!= "Parts not available" && callLogging.Sainteraction.reasonforpendingro != "Additional Work - Parts awaited")
                    {
                        callLogging.Sainteraction.partname = "";
                        callLogging.Sainteraction.partnumber = "";
                        callLogging.Sainteraction.etd = null;
                        callLogging.Sainteraction.requestdate = null;
                        callLogging.Sainteraction.orderdate = null;
                    }
                    callLogging.Sainteraction.roassignid = callLogging.rosaassignedId;
                    callLogging.Sainteraction.saname = Session["UserName"].ToString();
                    callLogging.Sainteraction.wyzuser_id = Convert.ToInt64(Session["UserId"]);

                    db.saInteractions.Add(callLogging.Sainteraction);
                    db.SaveChanges();
                    submissionResult = "True";
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        submissionResult = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        submissionResult = ex.InnerException.Message;
                    }
                }
                else
                {
                    submissionResult = ex.Message;
                }


            }

            TempData["SubmissionResult"] = submissionResult;
            logger.Info("\n\n-------  SA  Submition Ended : " + DateTime.Now);
          
                return RedirectToAction("sAROLog", "sAROLog");
          

        }


        
    public ActionResult saReferalCallLog(SACalllogingVM callLogging)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            string exception = "";
            long cusId = Convert.ToInt32(Session["CusId"].ToString());
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());

            if (cusId != callLogging.CustomerId && vehiId != callLogging.VehicleId)
            {
                TempData["Exceptions"] = "Invalid disposition...";

                logger.Info("\n\n------- SA Invalid Submition: " + DateTime.Now + "\n Disposing CustId: " + cusId + "Disposing VehId: " + vehiId + " User: " + Session["UserName"].ToString() + "\n Prev-Open Cust: " + callLogging.CustomerId + " Prev-Open VehiId" + callLogging.VehicleId);
                return RedirectToAction("LogOff", "Home");
            }

            logger.Info("\n\n------- SA Refferals Submition started : " + DateTime.Now + "\n CustId: " + cusId + " VehId: " + vehiId + " User: " + Session["UserName"].ToString());
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if(callLogging.Sareferrals.insurance==false)
                    {
                        callLogging.Sareferrals.policyexpirydate = null;
                        callLogging.Sareferrals.lastdiv = 0;
                        callLogging.Sareferrals.lastdiv = 0;
                        callLogging.Sareferrals.inurancecompany = "";
                        callLogging.Sareferrals.lastncb = 0;
                        callLogging.Sareferrals.comprehensive = "";
                        callLogging.Sareferrals.nilldip = "";
                        callLogging.Sareferrals.haanonhaa = "";
                    } 
                    if(callLogging.Sareferrals.referred_other==false)
                    {
                        callLogging.Sareferrals.customer_name = "";
                        callLogging.Sareferrals.mobile_number = "";
                    }
                    if(callLogging.Sareferrals.rsa==false)
                    {
                        callLogging.Sareferrals.rsacoverage = "";
                        callLogging.Sareferrals.rsaconverted = "";
                    }
                    if(callLogging.Sareferrals.ew==false)
                    {
                        callLogging.Sareferrals.ewcoverage = "";
                        callLogging.Sareferrals.ewconverted = "";
                    }
                    if(callLogging.Sareferrals.new_car==false)
                    {
                        callLogging.Sareferrals.intrestedmodel = "";
                        callLogging.Sareferrals.intrestedvariant = "";
                        callLogging.Sareferrals.fueltype = "";
                        callLogging.Sareferrals.intrestedcolor = "";
                        callLogging.Sareferrals.exchange = "";
                        callLogging.Sareferrals.preferedoutlet = "";
                    }
                    
                    if(callLogging.Sareferrals.otherservice==false)
                    {
                        callLogging.Sareferrals.otherservicetype = "";
                        callLogging.Sareferrals.othercurrentmileage = "";
                       
                    }

                    callLogging.Sareferrals.vehicle_id = vehiId;
                    callLogging.Sareferrals.customer_id = cusId;
                    callLogging.Sareferrals.roassignid = callLogging.rosaassignedId;
                    callLogging.Sareferrals.calldate = DateTime.Now.Date;
                    callLogging.Sareferrals.calltime = DateTime.Now.ToString("HH:mm:ss");
                    callLogging.Sareferrals.callmadeon = DateTime.Now;
                    callLogging.Sareferrals.saname = Session["UserName"].ToString();
                    callLogging.Sareferrals.wyzuser_id = Convert.ToInt64(Session["UserId"]);
                    db.saReferrals.Add(callLogging.Sareferrals);
                    db.SaveChanges();
                    return Json(new { success = true ,error=exception});

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

                return Json(new { success = false, error = exception });

            }

            logger.Info("\n\n------- SA Refferals Submition Ended : " + DateTime.Now);
        }

        public ActionResult getReferaltHistoryofVehicle()
        {
            long vehiId = Convert.ToInt32(Session["VehiId"].ToString());
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    var saReferals = db.saReferrals.Where(m => m.vehicle_id == vehiId).OrderByDescending(m=>m.calldate).Select(m=>new {m.id,m.calldate,m.calltime,m.saname,m.insurance,m.new_car,m.ew,m.rsa,m.referred_other,m.referral_remarks,m.otherservice }).ToList();
                    return Json(new { data = saReferals.OrderByDescending(m=>m.id), draw = Request["draw"], recordsTotal = saReferals.Count, recordsFiltered = saReferals.Count }, JsonRequestBehavior.AllowGet);

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
                return Json(new { data = "", draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0, exception = exception }, JsonRequestBehavior.AllowGet);

            }
        } 
        public ActionResult updateRoassignmentsattusandPDT(int roId,string roStatus,string isStatus,DateTime? PDT)
        {
            string exception = "";
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    rosaassignment updaterosaassignment = new rosaassignment();
                    updaterosaassignment = db.rosaAssignments.FirstOrDefault(m => m.id == roId);
                    if(isStatus=="true")
                    {
                        updaterosaassignment.rostatus = roStatus;
                        updaterosaassignment.rostatusupdatedate = DateTime.Now.Date;
                    }
                    if(isStatus=="false")
                    {
                        updaterosaassignment.promisedeliverytime = PDT;
                    }
                    db.rosaAssignments.AddOrUpdate(updaterosaassignment);
                    db.SaveChanges();
                    return Json(new { success = true ,exception="Updated Successfully" }, JsonRequestBehavior.AllowGet);

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
                return Json(new { success = false, exception = exception }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult getmodelvariantandfueltype(long modelID)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    var variants = db.Variants.Where(m => m.model_id==modelID).OrderByDescending(m => m.variant).Select(m => new { m.variant, m.fueltype }).ToList();
                    return Json(new { success=true, variants = variants,fueltype=variants.Select(m=>m.fueltype).Distinct() }, JsonRequestBehavior.AllowGet);

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
                return Json(new { success = false, variants = "" }, JsonRequestBehavior.AllowGet);

            }
        }


    }
}