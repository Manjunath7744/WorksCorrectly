using AutoSherpa_project.Controllers;
using Firebase.Database;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AutoSherpa_project.Models.Schedulers
{
    [DisallowConcurrentExecution]
    public class FESchedulerPulling : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            string siteRoot = HttpRuntime.AppDomainAppVirtualPath;
            Logger logger = LogManager.GetLogger("apkRegLogger");

            logger.Info("\n FE History Data Pulling Running DateTime: " + DateTime.Now);
            if (siteRoot != "/")
            {
                var fireBaseClient = new FirebaseClient("https://wyzcrm-2feff.firebaseio.com/");
                try
                {
                    string dealerCode = string.Empty;
                    using (var db = new AutoSherDBContext())
                    {
                        dealer dealerData = db.dealers.FirstOrDefault();

                        dealerCode = dealerData.dealerCode;
                        if (dealerCode != "INDUS")
                        {
                            string baseURL = string.Empty;
                            if (HomeController.wyzCRM1.Contains(dealerCode))
                            {
                                baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_Wyzcrm1"];
                            }
                            else if (HomeController.wyzCRM.Contains(dealerCode))
                            {
                                baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_Wyzcrm"];
                            }
                            else if (HomeController.autosherpa1.Contains(dealerCode))
                            {
                                baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_autosherpa1"];
                            }
                            else if (HomeController.WyzCrmNew.Contains(dealerCode))
                            {
                                baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_wyzNew"];
                            }
                            else if (HomeController.autosherpa3.Contains(dealerCode))
                            {
                                baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_autosherpa3"];
                            }

                            fireBaseClient = new FirebaseClient(baseURL);
                        }

                        schedulers schedulerDetails = db.schedulers.FirstOrDefault(m => m.dealerid == dealerData.id && m.scheduler_name == "fe-pull");

                        if(schedulerDetails.isActive==true)
                        {
                            List<string> insAgentList = db.wyzusers.Where(m => m.role == "InsuranceAgent").OrderBy(m => m.userName).Select(m => m.userName).ToList();

                            foreach (var cre in insAgentList)
                            {
                                //string cre1 = "Sudheer J(IND7083)";
                                string FireCrePath_History = dealerCode + "/InsuranceAgent/" + cre + "/History/CallInfo/";
                                try
                                {
                                    var FEHistoryData = await fireBaseClient.Child(FireCrePath_History).OnceAsync<FEFirebaseData>();

                                    if (FEHistoryData != null && FEHistoryData.Count > 0)
                                    {
                                        foreach (var data in FEHistoryData)
                                        {
                                            using (DbContextTransaction dbTrans = db.Database.BeginTransaction())
                                            {
                                                try
                                                {
                                                    FEFirebaseData FireData = new FEFirebaseData();
                                                    string FireBaseKey = string.Empty;
                                                    FireData = data.Object;
                                                    FireBaseKey = data.Key;

                                                    #region Previous Code now commented

                                                    //long isKeyExist = db.callinteractions.Where(m => m.firebaseKey == FireBaseKey).Count();

                                                    //if (isKeyExist == 0)
                                                    //{
                                                    //    callinteraction callInter = new callinteraction();
                                                    //    long dispoid = 0, insId = 0, vehId = 0, userId = 0, custId = 0;
                                                    //    callInter.callMadeDateAndTime = Convert.ToDateTime(FireData.actionDate);

                                                    //    callInter.callDate = Convert.ToDateTime(FireData.actionDate).ToString("dd-MM-yyyy");
                                                    //    callInter.callTime = Convert.ToDateTime(FireData.actionDate).ToString("HH:mm:ss");
                                                    //    callInter.firebaseKey = FireBaseKey;
                                                    //    callInter.ringTime = FireData.ringingTime.ToString();
                                                    //    callInter.makeCallFrom = FireData.makeCallFrom;
                                                    //    callInter.dailedNoIs = FireData.customerPhone;
                                                    //    callInter.dealerCode = dealerCode;


                                                    //    #region Checking and getting Id;s from Data

                                                    //    if (!string.IsNullOrEmpty(FireData.disposition))
                                                    //    {
                                                    //        dispoid = long.Parse(FireData.disposition);
                                                    //    }
                                                    //    else
                                                    //    {
                                                    //        logger.Info("\n Disposition Not Found\n ------- For The Data ------- \n" + JsonConvert.SerializeObject(data));
                                                    //        continue;
                                                    //    }

                                                    //    if (!string.IsNullOrEmpty(FireData.appointmentbookedId))
                                                    //    {
                                                    //        if(FireData.appointmentbookedId.Contains("PolicyDrop_"))
                                                    //        {
                                                    //            insId = long.Parse(FireData.appointmentbookedId.Split('_')[1]);
                                                    //        }
                                                    //        else
                                                    //        {
                                                    //            insId = long.Parse(FireData.appointmentbookedId);
                                                    //        }
                                                    //    }
                                                    //    else
                                                    //    {
                                                    //        logger.Info("\n Appointment-Id Not Found\n\n ------- For The Data ------- \n" + JsonConvert.SerializeObject(data));
                                                    //        continue;
                                                    //    }

                                                    //    if (!string.IsNullOrEmpty(FireData.vehicleId))
                                                    //    {
                                                    //        vehId = long.Parse(FireData.vehicleId);
                                                    //    }
                                                    //    else
                                                    //    {
                                                    //        logger.Info("\n Vehicle-Id Not Found\n\n ------- For The Data ------- \n" + JsonConvert.SerializeObject(data));
                                                    //        continue;
                                                    //    }

                                                    //    if (!string.IsNullOrEmpty(FireData.custId))
                                                    //    {
                                                    //        custId = long.Parse(FireData.custId);
                                                    //    }
                                                    //    else
                                                    //    {
                                                    //        logger.Info("\n Customer-Id Not Found\n\n ------- For The Data ------- \n" + JsonConvert.SerializeObject(data));
                                                    //        continue;
                                                    //    }

                                                    //    if (!string.IsNullOrEmpty(FireData.userId))
                                                    //    {
                                                    //        userId = long.Parse(FireData.userId);
                                                    //    }
                                                    //    else
                                                    //    {
                                                    //        logger.Info("\n User" +
                                                    //            "-Id Not Found\n\n ------- For The Data ------- \n" + JsonConvert.SerializeObject(data));
                                                    //        continue;
                                                    //    }
                                                    //    #endregion

                                                    //    if (FireData.appointmentbookedId.Contains("PolicyDrop_"))
                                                    //    {
                                                    //        long insAssignCount = db.insuranceassignedinteractions.Where(m => m.vehicle_vehicle_id == vehId).Count();
                                                    //        if (insAssignCount > 0 && insAssignCount <= 1)
                                                    //        {
                                                    //            string lastDispo = "";
                                                    //            insuranceassignedinteraction insAssign = db.insuranceassignedinteractions.FirstOrDefault(m => m.vehicle_vehicle_id == vehId);

                                                    //            if (insAssign.finalDisposition_id != null)
                                                    //            {
                                                    //                lastDispo = db.calldispositiondatas.FirstOrDefault(m => m.dispositionId == insAssign.finalDisposition_id).disposition;
                                                    //                insAssign.lastDisposition = lastDispo;
                                                    //            }

                                                    //            insAssign.finalDisposition_id = dispoid;
                                                    //            db.insuranceassignedinteractions.AddOrUpdate(insAssign);
                                                    //            db.SaveChanges();


                                                    //            callInter.insuranceAssignedInteraction_id = insAssign.id;
                                                    //            callInter.campaign_id = insAssign.campaign_id;
                                                    //        }
                                                    //        else
                                                    //        {
                                                    //            logger.Info("\n More Than One Data Dounf in InsuAssignInteraction \n\n ------- For The Data ------- \n" + JsonConvert.SerializeObject(data));
                                                    //            continue;
                                                    //        }
                                                    //    }
                                                    //    else // If id is Appointment Id
                                                    //    {
                                                    //        if (db.appointmentbookeds.Any(m => m.appointmentId == insId))
                                                    //        {
                                                    //            appointmentbooked existingApt = db.appointmentbookeds.FirstOrDefault(m => m.appointmentId == insId);
                                                    //            long insAssignedId = 0;

                                                    //            if(db.callinteractions.Where(m => m.appointmentBooked_appointmentId == insId).Count()>0)
                                                    //            {
                                                    //                long maxCallInterId = db.callinteractions.Where(m => m.appointmentBooked_appointmentId == insId).Max(m=>m.id);
                                                    //                insAssignedId = db.callinteractions.FirstOrDefault(m => m.id == maxCallInterId).insuranceAssignedInteraction_id ?? default(long);

                                                    //                if(insAssignedId!=0)
                                                    //                {
                                                    //                    string lastDispo = "";
                                                    //                    insuranceassignedinteraction insAssign = db.insuranceassignedinteractions.FirstOrDefault(m => m.id== insAssignedId);

                                                    //                    if (insAssign.finalDisposition_id != 0)
                                                    //                    {
                                                    //                        lastDispo = db.calldispositiondatas.FirstOrDefault(m => m.dispositionId == insAssign.finalDisposition_id).disposition;
                                                    //                        insAssign.lastDisposition = lastDispo;
                                                    //                    }

                                                    //                    insAssign.finalDisposition_id = dispoid;
                                                    //                    db.insuranceassignedinteractions.AddOrUpdate(insAssign);
                                                    //                    db.SaveChanges();

                                                    //                    callInter.insuranceAssignedInteraction_id = insAssign.id;
                                                    //                    callInter.campaign_id = insAssign.campaign_id;

                                                    //                    if(dispoid==25)
                                                    //                    {
                                                    //                        appointmentbooked newApt = new appointmentbooked();

                                                    //                        newApt.insuranceagent = existingApt.insuranceagent;
                                                    //                        newApt.customer_id = existingApt.customer_id;
                                                    //                        newApt.appointmentFromTime = FireData.appointmentTime;//FireData.aptScheduledDate;
                                                    //                        newApt.addressOfVisit = existingApt.addressOfVisit;
                                                    //                        newApt.renewalMode = existingApt.renewalMode;
                                                    //                        newApt.renewalType = existingApt.renewalType;
                                                    //                        newApt.insuranceBookStatus_id = dispoid;
                                                    //                        newApt.typeOfPickup = existingApt.typeOfPickup;
                                                    //                        newApt.insuranceAgentData = existingApt.insuranceAgentData;
                                                    //                        newApt.insuranceCompany = existingApt.insuranceCompany;
                                                    //                        newApt.fieldWalkinLocation = existingApt.fieldWalkinLocation;
                                                    //                        newApt.vehicle_id = existingApt.vehicle_id;
                                                    //                        newApt.wyzuser_id = existingApt.wyzuser_id;
                                                    //                        newApt.discountValue = existingApt.discountValue;
                                                    //                        newApt.feFireBaseKey = existingApt.feFireBaseKey;
                                                    //                        newApt.chaserId = existingApt.chaserId;
                                                    //                        newApt.pincode = existingApt.pincode;
                                                    //                        newApt.purpose = existingApt.purpose;
                                                    //                        DateTime appointmentDateTime = Convert.ToDateTime(FireData.aptScheduledDate + " " + FireData.appointmentTime);
                                                    //                        newApt.appointmentDate = appointmentDateTime;


                                                    //                        int hrs = appointmentDateTime.TimeOfDay.Hours;
                                                    //                        int min = appointmentDateTime.TimeOfDay.Minutes;
                                                    //                        TimeSpan startTime;
                                                    //                        if(min>=30)
                                                    //                        {
                                                    //                            min = 30;
                                                    //                        }
                                                    //                        else
                                                    //                        {
                                                    //                            min = 0;
                                                    //                        }

                                                    //                        startTime = new TimeSpan(hrs, min, 0);
                                                    //                        bookingdatetime bookingTime = db.bookingdatetimes.FirstOrDefault(m => m.startTime == startTime);

                                                    //                        pickupdrop pickUp = new pickupdrop();
                                                    //                        pickUp.pickupDate = appointmentDateTime;
                                                    //                        pickUp.timeFrom = bookingTime.startTime;
                                                    //                        pickUp.timeTo = bookingTime.endTime;

                                                    //                        db.pickupdrops.Add(pickUp);
                                                    //                        db.SaveChanges();

                                                    //                        newApt.pickupDrop_id = pickUp.id;

                                                    //                        existingApt.insuranceBookStatus_id = 35;
                                                    //                        db.appointmentbookeds.AddOrUpdate(existingApt);
                                                    //                        db.SaveChanges();

                                                    //                        db.appointmentbookeds.Add(newApt);
                                                    //                        db.SaveChanges();

                                                    //                        callInter.appointmentBooked_appointmentId = newApt.appointmentId;
                                                    //                        callInter.insuranceAssignedInteraction_id = insAssignedId;

                                                    //                        // Changinf Appointment Id in Firebase
                                                    //                        string fireBaseNewPatchUrl = dealerCode + "/InsuranceAgent/" + cre + "/ScheduledCalls/CallInfo/"+newApt.feFireBaseKey+ "/appointmentbookedId";
                                                    //                        //string fireBaseNewPatchUrl = dealerCode + "/InsuranceAgent/" + cre1 + "/ScheduledCalls/CallInfo/" + newApt.feFireBaseKey + "/appointmentbookedId";

                                                    //                        await fireBaseURL.Child(fireBaseNewPatchUrl).PutAsync(JsonConvert.SerializeObject("" +newApt.appointmentId+""));
                                                    //                    }
                                                    //                    else if(dispoid==39 || dispoid==23 || dispoid==35) // 39->Converted, 23->Not interested, 35->Cancelled
                                                    //                    {
                                                    //                        long pickUpDropId = existingApt.pickupDrop_id ?? default(long);

                                                    //                        if(pickUpDropId!=0)
                                                    //                        {
                                                    //                            pickupdrop delPickup = db.pickupdrops.FirstOrDefault(m => m.id == pickUpDropId);
                                                    //                            if(delPickup!=null)
                                                    //                            {
                                                    //                                db.pickupdrops.Remove(delPickup);
                                                    //                                db.SaveChanges();
                                                    //                            }
                                                    //                        }

                                                    //                        if(dispoid==35)
                                                    //                        {
                                                    //                            existingApt.insuranceBookStatus_id = 35;
                                                    //                        }

                                                    //                        existingApt.pickupDrop_id = null;
                                                    //                        db.appointmentbookeds.AddOrUpdate(existingApt);
                                                    //                        db.SaveChanges();
                                                    //                    }
                                                    //                    else
                                                    //                    {
                                                    //                        callInter.appointmentBooked_appointmentId = existingApt.appointmentId;
                                                    //                        callInter.insuranceAssignedInteraction_id = insAssignedId;
                                                    //                    }
                                                    //                }
                                                    //                else
                                                    //                {
                                                    //                    logger.Info("\n InsAssigedId not Found from appointment->callinteraction \n\n ------- For The Data ------- \n" + JsonConvert.SerializeObject(data));
                                                    //                    continue;
                                                    //                }
                                                    //            }

                                                    //        }
                                                    //        else
                                                    //        {
                                                    //            logger.Info("\n Appointment Not Found \n\n ------- For The Data ------- \n" + JsonConvert.SerializeObject(data));
                                                    //            continue;
                                                    //        }
                                                    //    }


                                                    //    if(FireData.documentUploaded=="Yes")
                                                    //    {
                                                    //        callInter.insDocuUploaded = true;
                                                    //    }

                                                    //    if(!string.IsNullOrEmpty(FireData.startLocation))
                                                    //    {
                                                    //        callInter.startLocation = FireData.startLocation;
                                                    //    }

                                                    //    if (!string.IsNullOrEmpty(FireData.stopLocation))
                                                    //    {
                                                    //        callInter.stopLocation = FireData.stopLocation;
                                                    //    }

                                                    //    if (!string.IsNullOrEmpty(FireData.kmTravelled))
                                                    //    {
                                                    //        callInter.kmTravelled = FireData.kmTravelled;
                                                    //    }

                                                    //    callInter.isCallinitaited = "initiated";
                                                    //    callInter.wyzUser_id = userId;
                                                    //    callInter.vehicle_vehicle_id = vehId;
                                                    //    callInter.customer_id = custId;
                                                    //    callInter.chasserCall = false;
                                                    //    db.callinteractions.Add(callInter);
                                                    //    db.SaveChanges();

                                                    //    if(FireData.documentUploaded=="Yes")
                                                    //    {
                                                    //        foreach(var doc in FireData.documentsLink)
                                                    //        {
                                                    //            string key = string.Empty;
                                                    //            key = doc.Keys.FirstOrDefault();
                                                    //            if (!string.IsNullOrEmpty(key))
                                                    //            {
                                                    //                documentuploadhistory docHistory = db.documentuploadhistories.FirstOrDefault(m => m.documentName == key && m.customerId==custId && m.userId==userId);

                                                    //                if(docHistory!=null)
                                                    //                {
                                                    //                    docHistory.insCallInterId = callInter.id;
                                                    //                    db.documentuploadhistories.AddOrUpdate(docHistory);
                                                    //                    db.SaveChanges();
                                                    //                }
                                                    //            }
                                                    //        }
                                                    //    }

                                                    //    insurancedisposition insDispo = new insurancedisposition();
                                                    //    if (!string.IsNullOrEmpty(FireData.comments))
                                                    //    {
                                                    //        insDispo.comments = FireData.comments;
                                                    //    }

                                                    //    if (!string.IsNullOrEmpty(FireData.paymentReference))
                                                    //    {
                                                    //        insDispo.paymentReference = FireData.paymentReference;
                                                    //    }

                                                    //    if (!string.IsNullOrEmpty(FireData.paymentType))
                                                    //    {
                                                    //        insDispo.paymentType = FireData.paymentType;
                                                    //    }

                                                    //    if (!string.IsNullOrEmpty(FireData.reason))
                                                    //    {
                                                    //        insDispo.reason = FireData.reason;
                                                    //    }


                                                    //    insDispo.callInteraction_id = callInter.id;
                                                    //    insDispo.callDispositionData_id = dispoid;
                                                    //    db.insurancedispositions.Add(insDispo);
                                                    //    db.SaveChanges();

                                                    //    dataremoval dataRm = new dataremoval();

                                                    //    dataRm.firebasekey = FireBaseKey;
                                                    //    dataRm.insAssign_id = callInter.insuranceAssignedInteraction_id ?? default(long);
                                                    //    dataRm.updatedByApp = true;
                                                    //    dataRm.isSynched = true;
                                                    //    dataRm.vehicle_id = vehId;
                                                    //    dataRm.customer_id = custId;
                                                    //    dataRm.updatedDateTime = DateTime.Now;
                                                    //    db.Dataremovals.Add(dataRm);
                                                    //    db.SaveChanges();

                                                    //    db.Database.ExecuteSqlCommand("call Triggerinsertinsurancecallhistrycube(@newid);", new MySqlParameter("@newid", callInter.id));
                                                    //    dbTrans.Commit();

                                                    //    logger.Info("\n\n Below Data has been Synchronized\n\n ---------- Data ------ \n"+JsonConvert.SerializeObject(data));
                                                    //}

                                                    #endregion

                                                    fefirebasehistorydata feHistory = new fefirebasehistorydata();

                                                    if (db.feFirebaseHistoryData.Count(m => m.firebasekey == FireBaseKey) == 0)
                                                    {
                                                        feHistory.actionDate = FireData.actionDate;
                                                        feHistory.ageOfVehicle = FireData.ageOfVehicle;
                                                        feHistory.appointmentType = FireData.appointmentType;
                                                        feHistory.callTypePicId = FireData.callTypePicId;
                                                        feHistory.chassisNo = FireData.chassisNo;
                                                        feHistory.comments = FireData.comments;
                                                        feHistory.creName = FireData.creName;
                                                        feHistory.currentAddress = FireData.currentAddress;
                                                        if (!string.IsNullOrEmpty(FireData.custId))
                                                        {
                                                            feHistory.custId = long.Parse(FireData.custId);
                                                        }

                                                        feHistory.customerName = FireData.customerName;
                                                        feHistory.customerPhone = FireData.customerPhone;
                                                        feHistory.daysBetweenVisit = FireData.daysBetweenVisit;
                                                        feHistory.dealerCode = FireData.dealerCode;
                                                        feHistory.discountValue = FireData.discountValue;
                                                        feHistory.engineNo = FireData.engineNo;
                                                        feHistory.insuranceCompany = FireData.insuranceCompany;
                                                        feHistory.lastDisposition = FireData.lastDisposition;
                                                        if (!string.IsNullOrEmpty(FireData.disposition) && FireData.disposition.Length == 2)
                                                        {
                                                            feHistory.disposition = long.Parse(FireData.disposition);
                                                        }

                                                        feHistory.lastPolicyDate = FireData.lastPolicyDate;
                                                        feHistory.lastPremium = FireData.lastPremium;
                                                        feHistory.makeCallFrom = FireData.makeCallFrom;
                                                        feHistory.model = FireData.model;
                                                        feHistory.nextRenewalDate = FireData.nextRenewalDate;
                                                        feHistory.paymentReference = FireData.paymentReference;
                                                        feHistory.paymentType = FireData.paymentType;
                                                        feHistory.pincode = FireData.pincode;
                                                        feHistory.policyNo = FireData.policyNo;
                                                        feHistory.renewalMode = FireData.renewalMode;
                                                        feHistory.renewalType = FireData.renewalType;
                                                        feHistory.ringingTime = FireData.ringingTime;
                                                        feHistory.saleDate = FireData.saleDate;

                                                        if (FireData.appointmentbookedId.Contains("PolicyDrop_"))
                                                        {
                                                            feHistory.inspolicydrop_id = long.Parse(FireData.appointmentbookedId.Split('_')[1]);
                                                        }
                                                        else
                                                        {
                                                            if(FireData.appointmentbookedId.Contains("InsInter_"))
                                                            {
                                                                feHistory.insassign_id  = long.Parse(FireData.appointmentbookedId.Split('_')[1]);
                                                            }
                                                            else
                                                            {
                                                                feHistory.appointmentbookedId = long.Parse(FireData.appointmentbookedId);
                                                            }

                                                        }


                                                        feHistory.aptScheduledDate = FireData.aptScheduledDate;
                                                        feHistory.appointmentTime = FireData.appointmentTime;
                                                        if (!string.IsNullOrEmpty(FireData.userId))
                                                        {
                                                            feHistory.userId = long.Parse(FireData.userId);
                                                        }

                                                        feHistory.vehicleRegNo = FireData.vehicleRegNo;
                                                        if (!string.IsNullOrEmpty(FireData.vehicleId))
                                                        {
                                                            feHistory.vehicleId = long.Parse(FireData.vehicleId);
                                                        }
                                                        feHistory.vehicalRegNo = FireData.vehicalRegNo;
                                                        feHistory.customerAddress = FireData.customerAddress;
                                                        feHistory.interactionDate = FireData.interactionDate;
                                                        feHistory.lastPolicyNo = FireData.lastPolicyNo;
                                                        feHistory.lastIDV = FireData.lastIDV;
                                                        feHistory.premiumWithValue = FireData.premiumWithValue;
                                                        feHistory.coupon = FireData.coupon;
                                                        feHistory.lastCoverage = FireData.lastCoverage;
                                                        feHistory.premium = FireData.premium;
                                                        feHistory.uploadedDate = FireData.uploadedDate;
                                                        feHistory.status = FireData.status;
                                                        feHistory.documentUploaded = FireData.documentUploaded;
                                                        feHistory.kmTravelled = FireData.kmTravelled;
                                                        feHistory.startLocation = FireData.startLocation;
                                                        feHistory.stopLocation = FireData.stopLocation;
                                                        feHistory.reason = FireData.reason;
                                                        feHistory.crePhoneNumber = FireData.crePhoneNumber;
                                                        feHistory.firebasekey = FireBaseKey;
                                                        feHistory.updateddatetime = DateTime.Now;

                                                        db.feFirebaseHistoryData.Add(feHistory);
                                                        db.SaveChanges();


                                                        dataremoval dataRm = new dataremoval();

                                                        dataRm.firebasekey = FireBaseKey;
                                                        if (FireData.appointmentbookedId.Contains("PolicyDrop_"))
                                                        {
                                                            dataRm.inspolicydrop_id = long.Parse(FireData.appointmentbookedId.Split('_')[1]);
                                                        }
                                                        else
                                                        {
                                                            if (FireData.appointmentbookedId.Contains("InsInter_"))
                                                            {
                                                                dataRm.insAssign_id = long.Parse(FireData.appointmentbookedId.Split('_')[1]);
                                                            }
                                                            else
                                                            {
                                                                dataRm.appointmentId = long.Parse(FireData.appointmentbookedId);
                                                            }
                                                        }
                                                        dataRm.updatedByApp = true;
                                                        dataRm.isSynched = true;

                                                        if (!string.IsNullOrEmpty(FireData.custId))
                                                        {
                                                            dataRm.customer_id = long.Parse(FireData.custId);
                                                        }

                                                        if (!string.IsNullOrEmpty(FireData.vehicleId))
                                                        {
                                                            dataRm.vehicle_id = long.Parse(FireData.vehicleId);
                                                        }

                                                        dataRm.updatedDateTime = DateTime.Now;
                                                        db.Dataremovals.Add(dataRm);
                                                        db.SaveChanges();
                                                        dbTrans.Commit();

                                                        string historyURL = FireCrePath_History + FireBaseKey;
                                                        await fireBaseClient.Child(historyURL).DeleteAsync();//delete

                                                        if (FireData.disposition == "23" || FireData.disposition == "35" || FireData.disposition == "39")
                                                        {
                                                            long vehicleId = feHistory.vehicleId ?? default(long);

                                                            if (vehicleId != 0)
                                                            {
                                                                fieldexecutivefirebaseupdation feUpdation = db.Fieldexecutivefirebaseupdations.Where(m => m.vehicle_id == vehicleId).OrderByDescending(m => m.id).FirstOrDefault();
                                                                if (feUpdation != null)
                                                                {
                                                                    string firebasekey = feUpdation.firebasekey;

                                                                    if (!string.IsNullOrEmpty(firebasekey))
                                                                    {
                                                                        string agentName = db.wyzusers.FirstOrDefault(m => m.id == feHistory.userId).userName;

                                                                        await fireBaseClient.Child(dealerCode + "/InsuranceAgent/" + agentName + "/" +
                                                                                                 "ScheduledCalls" + "/" + "CallInfo/" + firebasekey).DeleteAsync();

                                                                        db.Fieldexecutivefirebaseupdations.Remove(feUpdation);
                                                                        db.SaveChanges();
                                                                    }
                                                                    else
                                                                    {
                                                                        logger.Error("\nFE History Pulling - Firebase key is empty in FeUpation Table for vehicle: " + vehicleId);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    logger.Error("\nFE History Pulling - data not found in FEUpation Table for vehicle: " + vehicleId);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                logger.Error("\nFE History Pulling - Null vehicle_id in firebasehistory data");
                                                            }

                                                        }

                                                        logger.Info("\n\n Below Data has been Synchronized\n\n ---------- Data ------ \n" + JsonConvert.SerializeObject(data));
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    dbTrans.Rollback();
                                                    string exception = string.Empty;
                                                    if (ex.Message.Contains("inner exception"))
                                                    {
                                                        if (ex.InnerException.Message.Contains("inner exception"))
                                                        {
                                                            exception = "\nFE History Pulling Scheduler Error(DataLoop): " + ex.InnerException.InnerException.Message;// +"\n----- From the line:"+lineNum);
                                                        }
                                                        else
                                                        {
                                                            exception="\nFE History Pulling Scheduler Error(DataLoop):" + ex.InnerException.Message; // + "\n----- From the line:" + lineNum);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        exception = "\nFE History Pulling Scheduler Error(DataLoop):" + ex.Message;// + "\n----- From the line:" + lineNum);
                                                    }


                                                    if (ex.StackTrace.Contains(':'))
                                                    {
                                                        exception = exception + "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)];
                                                    }
                                                    logger.Info(exception);
                                                    logger.Info("\n ------- For The Data ------- \n" + JsonConvert.SerializeObject(data));
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    //var stack = new StackTrace(ex, true);
                                    //var topFrame = stack.GetFrame(0);
                                    //int lineNum = topFrame.GetFileLineNumber();
                                    string exception = string.Empty;
                                    if (ex.Message.Contains("inner exception"))
                                    {
                                        if (ex.InnerException.Message.Contains("inner exception"))
                                        {
                                            exception="\nFE History Pulling Scheduler Error(CreLoop):" + ex.InnerException.InnerException.Message;// + "\n----- From the line:" + lineNum);
                                        }
                                        else
                                        {
                                            exception = "\nFE History Pulling Scheduler Error(CreLoop):" + ex.InnerException.Message;// + "\n----- From the line:" + lineNum);
                                        }
                                    }
                                    else
                                    {
                                        exception = "\nFE History Pulling Scheduler Error(CreLoop):" + ex.Message;// + "\n----- From the line:" + lineNum);
                                    }

                                    if (ex.StackTrace.Contains(':'))
                                    {
                                        exception = exception + "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)];
                                    }
                                    logger.Info(exception);
                                }
                            }
                        }


                       

                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("inner exception"))
                    {
                        if (ex.InnerException.Message.Contains("inner exception"))
                        {
                            logger.Error("\nFE History Pulling Scheduler Error(OuterLoop):" + ex.InnerException.InnerException.Message);
                        }
                        else
                        {
                            logger.Error("\nFE History Pulling Scheduler Error(OuterLoop):" + ex.InnerException.Message);
                        }
                    }
                    else
                    {
                        logger.Error("\nFE History Pulling Scheduler Error(OuterLoop):" + ex.Message);
                    }
                }
            }

            logger.Info("\n FE History Data Pulling Completed DateTime: " + DateTime.Now);
        }
    }
}