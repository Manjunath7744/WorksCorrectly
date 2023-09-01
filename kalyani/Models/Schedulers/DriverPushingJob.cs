using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Quartz;
using NLog;
using Newtonsoft.Json;
using AutoSherpa_project.Models.Schedulers;

namespace AutoSherpa_project.Models.Schedulers
{
    [DisallowConcurrentExecution]
    public class DriverPushingJob :schedulerCommonFunction, IJob
    {
        public async Task Execute(IJobExecutionContext context)
          {

            string siteRoot = HttpRuntime.AppDomainAppVirtualPath;
            Logger logger = LogManager.GetLogger("apkRegLogger");
            string demandedrepairlists = "";
            logger.Info("\n Driver Pushing Running DateTime: " + DateTime.Now);
            
                
            if (siteRoot != "/")
            {
                try
                {
                    //var firebaseBaseURL = new FirebaseClient("https://wyzcrm-2feff.firebaseio.com/");
                    var firebaseBaseURL = new FirebaseClient("https://wyznew-30c55.firebaseio.com/");
                    using (AutoSherDBContext db = new AutoSherDBContext())
                    {
                        dealer dealerData = db.dealers.FirstOrDefault();

                         schedulers schedulerDetails = db.schedulers.FirstOrDefault(m => m.dealerid == dealerData.id && m.scheduler_name == "driver-push");

                        if (schedulerDetails != null && schedulerDetails.isActive == true)
                        {
                            logger.Info("\n Driver History Data Pushing Running Started --> DateTime: " + DateTime.Now);
                            int maxlength = 10000;
                            startScheduler("driver-push");

                            if (schedulerDetails.datalenght != 0)
                            {
                                maxlength = schedulerDetails.datalenght;
                            }

                            List<driverscheduler> driverdata = new List<driverscheduler>();

                            driverdata = db.driverSchedulers.Where(m => m.ispushed == false && m.driver_id != 0).OrderByDescending(m => m.id).ToList();

                            deletecandcelleddriverappfirebasedata(dealerData);

                            List<driverscheduler> assigneddriverdata = driverdata.Where(m => m.ispushed == false && m.driver_id!=0 && m.IsCancelled==false).OrderByDescending(m => m.id).ToList();
                            
                            logger.Info("Current going data\n Driver Data:{0}", JsonConvert.SerializeObject(driverdata));

                            if(driverdata!=null && driverdata.Count()>0)
                            {

                                foreach(var data in assigneddriverdata)
                                {
                                    try
                                    {
                                        long servicebookedId = db.driverBookingDetails.FirstOrDefault(m => m.id == data.driverBookingdetails_id).serviceBookedId;
                                        long pickupdropTypeId = db.servicebookeds.FirstOrDefault(m => m.serviceBookedId == servicebookedId).pickupdroptype;
                                        if (pickupdropTypeId == 3 && data.PickUpDrop == 2)
                                        {
                                            if (db.driverBookingDetails.Count(m => m.serviceBookedId == servicebookedId && m.PickUpDrop == 1) > 0)
                                            {
                                                long driverbookingdetailspickupId = db.driverBookingDetails.FirstOrDefault(m => m.serviceBookedId == servicebookedId && m.PickUpDrop == 1).id;
                                                long driverschedulerID = db.driverSchedulers.FirstOrDefault(m => m.driverBookingdetails_id == driverbookingdetailspickupId).id;
                                                if (db.driverAppInteraction.Count(m => m.DriverScheduler_Id == driverschedulerID && m.IsPickUp && (m.Disposition == "PickUpComplete" || m.Disposition == "NonContact" || m.Disposition == "NotInterested")) <= 0)
                                                {
                                                    continue;
                                                }
                                                if ((db.driverAppInteraction.Count(m => m.DriverScheduler_Id == driverschedulerID && m.InteractionType == "PickUp Reached") > 0))
                                                {
                                                    demandedrepairlists = db.driverAppInteraction.FirstOrDefault(m => m.DriverScheduler_Id == driverschedulerID && m.InteractionType == "PickUp Reached").DemandedRepair;
                                                }
                                            }
                                        }

                                        //Fetching data from required tables from database
                                        driver driversdata = db.drivers.Include("wyzuser").FirstOrDefault(m => m.id == data.driver_id && m.isactive == true);
                                        DriverBookingDetails bookedData = db.driverBookingDetails.FirstOrDefault(m => m.id == data.driverBookingdetails_id);
                                        wyzuser CreDetails = db.wyzusers.FirstOrDefault(m => m.id == data.scheduledBy);
                                        vehicle vehicledata = db.vehicles.FirstOrDefault(m => m.vehicle_id == data.vehicle_id);
                                        callhistorycube callhistory = db.callhistorycubes.Where(m => m.vehicle_id == data.vehicle_id).OrderByDescending(m => m.id).FirstOrDefault();

                                        DriverFirebaseData DriverAppData = new DriverFirebaseData();

                                        DriverAppData.CustomerName = validateString(callhistory.Customer_name);

                                        List<string> phoneNumberList = db.phones.Where(m => m.customer_id == data.customer_id).Select(m => m.phoneNumber).ToList();

                                        if (phoneNumberList.Count() > 0)
                                        {
                                            DriverAppData.CustomerPhoneNumber = string.Join(",", phoneNumberList);
                                        }
                                        else
                                        {
                                            DriverAppData.CustomerPhoneNumber = "-";
                                        }

                                        DriverAppData.RegistrationNumber = validateString(vehicledata.vehicleRegNo);
                                        DriverAppData.ChassisNumber = validateString(vehicledata.chassisNo);
                                        DriverAppData.Model = validateString(vehicledata.model);

                                        if (vehicledata.saleDate != null)
                                        {
                                            DriverAppData.SaleDate = Convert.ToDateTime(vehicledata.saleDate).ToString("dd-MM-yyyy");
                                        }
                                        else
                                        {
                                            DriverAppData.SaleDate = "-";
                                        }

                                        DriverAppData.SaleLocation = "-";

                                        DriverAppData.Mileage = getLastMileage(data.vehicle_id ?? default(long), db, dealerData.dealerCode);

                                        if (bookedData.BookingDate != null)
                                        {
                                            DriverAppData.AptDateTime = Convert.ToDateTime(bookedData.BookingDate).ToString("dd-MM-yyyy") + " " + bookedData.BookingTime;
                                        }

                                        DriverAppData.CRETeleCaller = validateString(CreDetails.phoneNumber);
                                        DriverAppData.CREPhoneNumber = validateString(CreDetails.phoneNumber);
                                        DriverAppData.ServiceBookedType = validateString(callhistory.serviceType);
                                        DriverAppData.WorkshopName = validateString(callhistory.Booked_workshop);
                                        DriverAppData.ServiceAdvisor = validateString(callhistory.service_advisor);
                                        DriverAppData.AptMode = validateString(bookedData.BookingType);
                                        DriverAppData.CRERemarks = validateString(callhistory.customer_remarks);
                                        DriverAppData.PickUpAddress = validateString(bookedData.PickUpAddress);
                                        DriverAppData.InteractionDate = Convert.ToDateTime(callhistory.callDate).ToString("dd-MM-yyyy");
                                        DriverAppData.DriverName = validateString(driversdata.driverName);
                                        DriverAppData.LastDisposition = validateString(callhistory.secondary_dispostion);
                                        DriverAppData.Remarks = validateString(callhistory.customerRemarks);
                                        DriverAppData.PickUpDropType = data.PickUpDrop;
                                        DriverAppData.UniqueKey = validateString(data.uniquekey);
                                        DriverAppData.DriverPhoneNumber = validateString(driversdata.driverPhoneNum);


                                        DriverAppData.DriverId = data.driver_id ?? default(long);
                                        DriverAppData.DriverScheduler_Id = data.id;


                                        DriverAppData.CREName = validateString(CreDetails.userName);
                                        DriverAppData.Disposition = "NONE";
                                        DriverAppData.Reasons = "NONE";
                                        DriverAppData.DeliveryNoteStatus = "NONE";
                                        DriverAppData.DemandedRepair = demandedrepairlists;

                                        if (data.vehicle_id != null)
                                        {
                                            if (DriverAppData.PickUpDropType == 1)
                                            {
                                                DriverAppData.VehicleId = (data.vehicle_id ?? default(long)).ToString() + "_PickUp";
                                            }
                                            else
                                            {
                                                DriverAppData.VehicleId = (data.vehicle_id ?? default(long)).ToString() + "_Drop";
                                            }

                                        }

                                        DriverAppData.Status = "Not Attempted";
                                        DriverAppData.Customer_Id = data.customer_id ?? default(long);
                                        //Pickup Completed in 'PickUpDropRequired'
                                        DriverAppData.PickUpCount = data.PickUpDrop;
                                        DriverAppData.PickupDispositon = string.Empty;
                                        DriverAppData.DropDisposition = string.Empty;

                                        string URLPath1;

                                        List<long?> CustomerId = db.driverSchedulers.Select(m => m.customer_id).ToList();
                                        
                                        if (dealerData.dealerCode == "POPULARHYUNDAI"  && CustomerId.Contains(DriverAppData.Customer_Id) && db.driverSchedulers.FirstOrDefault(m => m.customer_id == DriverAppData.Customer_Id).firebasekey != null)
                                        {
                                            var firebasekey = db.driverSchedulers.FirstOrDefault(m => m.customer_id == DriverAppData.Customer_Id).firebasekey;
                                            URLPath1 = dealerData.dealerCode + "/Driver/ScheduledCalls/" + driversdata.wyzuser.userName + "/CallInfo/" + firebasekey;
                                             await firebaseBaseURL.Child(URLPath1).PutAsync(DriverAppData);
                                            data.ispushed = true;
                                            db.SaveChanges();

                                            logger.Info("\n Below Data got pushed in Driver:\n" + JsonConvert.SerializeObject(DriverAppData));
                                        }
                                        else
                                        {
                                             URLPath1 = dealerData.dealerCode + "/Driver/ScheduledCalls/" + driversdata.wyzuser.userName + "/CallInfo/";
                                            var firebaseURL1 = await firebaseBaseURL.Child(URLPath1).PostAsync(DriverAppData);
                                            
                                            string referenceKey = firebaseURL1.Key;

                                            data.firebasekey = referenceKey;
                                            data.ispushed = true;
                                            db.SaveChanges();

                                            logger.Info("\n Below Data got pushed in Driver:\n" + JsonConvert.SerializeObject(DriverAppData));
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string exception = string.Empty;

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
                                            exception = exception + "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)];
                                        }

                                        logger.Error("Driver app file push scheduler error(Data Loop block): \n" + exception);
                                        logger.Error("For the booked driver data: \n" + JsonConvert.SerializeObject(data));

                                    }
                                }
                               
                            }

                            stopScheduler("driver-push");
                        }
                        else
                        {
                            logger.Info("\n driver-push Inactive / Not Exist / Already Running");
                        }

                    }
                }
                catch (Exception ex)
                {
                    string exception = string.Empty;

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
                        exception = exception + "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)];
                    }

                    logger.Error("Driver app file push scheduler error(outer block): \n" + exception);
                    stopScheduler("driver-push");
                }
                //ChangeRunningStatus();
            }
            logger.Info("\n Driver History Data Pushing Completed DateTime: " + DateTime.Now);
        }

        public string getLastMileage(long vehicleId,AutoSherDBContext db,string OEM)
        {
            string Mileage = "";

            long countOfServicePresent = db.services.Where(m => m.vehicle_vehicle_id == vehicleId && m.lastServiceDate != null).Count();

            //latest Service Getting
            if (countOfServicePresent != 0)
            {
                long lastId = 0;

                if (OEM == "MARUTI SUZUKI")
                {
                    var latestService = db.services.Where(m => m.vehicle_vehicle_id == vehicleId && m.lastServiceDate != null).OrderByDescending(x => x.billDate).FirstOrDefault();
                    Mileage = latestService.Lastservicemeterreading;
                }
                else
                {
                    var latestService = db.services.Where(m => m.vehicle_vehicle_id == vehicleId && m.lastServiceDate != null).OrderByDescending(x => x.jobCardDate).FirstOrDefault();
                    Mileage = latestService.Lastservicemeterreading;
                }
            }

            return Mileage;
        }

        public string validateString(string stringVar)
        {
            if (string.IsNullOrEmpty(stringVar))
            {
                return "-";
            }
            else
            {
                return stringVar;
            }
        }

        public async void deletecandcelleddriverappfirebasedata( dealer dealerData)
        {
            List<driverscheduler> driverdata = new List<driverscheduler>();

            string siteRoot = HttpRuntime.AppDomainAppVirtualPath;
            Logger logger = LogManager.GetLogger("apkRegLogger");

            logger.Info("\n Driver Removing Cancelled Data From Firebase Running DateTime: " + DateTime.Now);
            using (var db=new AutoSherDBContext())
            { driverdata = db.driverSchedulers.Where(m => m.IsCancelled == true && m.iscanceldeleted==false && m.firebasekey != null).ToList();
            
            if (driverdata != null && driverdata.Count() > 0)
            {

                foreach (var data in driverdata)
                {
                    try
                    {
                        var firebaseBaseURL = new FirebaseClient("https://wyznew-30c55.firebaseio.com/");
                       
                            if ((data.firebasekey != null && data.driver_id != null))
                            {
                                driver LastDriverData = db.drivers.Include("wyzuser").FirstOrDefault(m => m.id == data.driver_id && m.isactive == true);

                                
                                    string URLPath = dealerData.dealerCode + "/Driver/ScheduledCalls/" + LastDriverData.wyzuser.userName + "/CallInfo/" + data.firebasekey;
                                    await firebaseBaseURL.Child(URLPath).DeleteAsync();
                                

                                if (data.IsCancelled == true)
                                {
                                    data.ispushed = true;
                                    data.iscanceldeleted = true;
                                    db.driverSchedulers.AddOrUpdate(data);
                                    db.SaveChanges();
                                    continue;
                                }
                            }
                        
                    }
                    catch (Exception ex)
                    {
                        string exception = string.Empty;

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
                            exception = exception + "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)];
                        }

                        logger.Error(" Removing Cancelled Data From Firebase Running Error:\n" + exception);
                    }
                }
            }
            }
        }
    }
}