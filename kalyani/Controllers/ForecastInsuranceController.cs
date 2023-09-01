using AutoSherpa_project.Models;
using AutoSherpa_project.Models.ViewModels;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoSherpa_project.Controllers
{
    [AuthorizeFilter]
    public class ForecastInsuranceController : Controller
    {
        // GET: ForecastInsurance
        public ActionResult ForecastInsuranceGET()
        {
            ForecastInsuranceVM forecastInsuranceVM = new ForecastInsuranceVM();
            using (AutoSherDBContext dBContext = new AutoSherDBContext())
            {
                try
                {
                    int wyzUserId = Convert.ToInt32(Session["UserId"].ToString());

                    forecastInsuranceVM.campaigns = dBContext.campaigns.Where(x => x.isactive == true && x.campaignType == "Insurance").ToList();
                    ViewBag.campaign = forecastInsuranceVM.campaigns.Select(m => new { id = m.campaignName });
                    forecastInsuranceVM.renewaltypes = dBContext.renewaltypes.ToList();
                    if (Session["DealerCode"].ToString() == "POPULARHYUNDAI")
                    {
                        List<long> listOfWrkshopId = dBContext.userlocations.Where(x => x.userLocation_id == wyzUserId).Select(x => x.locationList_cityId).Distinct().ToList();
                        forecastInsuranceVM.locations = dBContext.locations.Where(x => listOfWrkshopId.Contains(x.cityId)).ToList();
                    }
                    else
                    {

                        forecastInsuranceVM.locations = dBContext.locations.ToList();
                        ViewBag.location = forecastInsuranceVM.locations.Select(m => new { id = m.cityId, name = m.name });
                    }
                    forecastInsuranceVM.wyzusers = dBContext.wyzusers.Where(x => x.role == "CRE" && x.unAvailable == false).ToList();
                    ViewBag.countOfData = dBContext.insuranceforecasteddatas.Count();
                    ViewBag.ModelType = dBContext.Modelcategories.Select(x => new { id = x.modelcatid, name = x.modelcat }).ToList();
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


                    ViewBag.exception = ex.ToString();
                }

            }
            return View(forecastInsuranceVM);
        }

        public ActionResult getWorkshopValues(string value)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    List<long> locIDs = value.Split(',').Select(long.Parse).ToList();
                    db.Configuration.ProxyCreationEnabled = false;
                    var workshoplist = db.workshops.Where(m => locIDs.Contains(m.location_cityId ?? default(long))).Select(m => new { id = m.id, workshopName = m.workshopName }).ToList();

                    return Json(workshoplist);
                }
                //using(AutoSherDBContext db=new AutoSherDBContext())
                //{
                //    List<long> locIDs = value.Split(',').Select(long.Parse).ToList();
                //    var workshopNames = db.workshops.Where(x => locIDs.Contains(x.location_cityId ?? default(long))).Select(x=>new { id=x.id,name=x.workshopName}).ToList();
                //    return Json(new { success = true, data = workshopNames }, JsonRequestBehavior.AllowGet);
                //}
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getCreList(string value)
        {
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    if (Session["DealerCode"].ToString() == "CAUVERYFORD")
                    {
                        if (Session["LoginUser"].ToString() == "Insurance")
                        {
                            var listOfCREs = db.wyzusers.Where(x => x.insuranceRole == true && x.role == "CRE" && x.unAvailable == false).Select(x => new { id = x.id, name = x.userName }).ToList();
                            return Json(new { success = true, data = listOfCREs }, JsonRequestBehavior.AllowGet);
                        }
                        else if (Session["LoginUser"].ToString() == "Service")
                        {
                            var listOfCREs = db.wyzusers.Where(x => x.insuranceRole == true && x.role == "CRE" && x.unAvailable == false).Select(x => new { id = x.id, name = x.userName }).ToList();
                            return Json(new { success = true, data = listOfCREs }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        List<long> worksopList = value.Split(',').Select(long.Parse).ToList();
                        List<long> listOfUsers = db.userworkshops.Where(x => worksopList.Contains(x.workshopList_id)).Distinct().Select(x => x.userWorkshop_id).ToList();
                        var listOfCREs = db.wyzusers.Where(x => listOfUsers.Contains(x.id) && x.role == "CRE" && x.unAvailable == false).Select(x => new { id = x.id, name = x.userName }).ToList();
                        return Json(new { success = true, data = listOfCREs }, JsonRequestBehavior.AllowGet);
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
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult getDDLData(string ddlValue)
        //{
        //    List<string> workshopData = new List<string>();
        //    using (AutoSherDBContext dBContext = new AutoSherDBContext())
        //    {
        //        if (ddlValue.Contains(','))
        //        {
        //            var locIDs1 = ddlValue.Split(',').Select(int.Parse).ToList();
        //            List<long> locIDs = ddlValue.Split(',').Select(long.Parse).ToList();
        //            workshopData = dBContext.workshops.Where(x => (locIDs).Contains(x.location_cityId ?? default(long))).Select(x => x.workshopName).ToList();
        //        }
        //        else
        //        {
        //            long id = long.Parse(ddlValue);
        //            workshopData = dBContext.workshops.Where(x => x.location_cityId == id).Select(x => x.workshopName).ToList();
        //        }
        //    }
        //    return Json(new { success = true, workshopData = workshopData });
        //}

        //public List<insuranceforecasteddata> getTableData(string values)
        //{
        //    List<insuranceforecasteddata> assignedData = new List<insuranceforecasteddata>();
        //    //isactive n isvalue checks
        //    using (AutoSherDBContext dBContext = new AutoSherDBContext())
        //    {
        //        var selectedData = JsonConvert.DeserializeObject<ForecastInsuranceFilter>(values);
        //        List<int> campaignIDs = new List<int>();
        //        List<int> renewalIDs = new List<int>();
        //        List<long> wrkshopIDs = new List<long>();
        //        string wrkshopIDs1 = "";
        //        DateTime fromDate = new DateTime();
        //        DateTime toDate = new DateTime();
        //        if (selectedData.cmpaignObj != null)
        //        {
        //            campaignIDs = selectedData.cmpaignObj.Split(',').Select(int.Parse).ToList();
        //        }
        //        if (selectedData.renewaltypeObj != null)
        //        {
        //            renewalIDs = selectedData.renewaltypeObj.Split(',').Select(int.Parse).ToList();
        //        }
        //        if (selectedData.workshopLocationObj != null)
        //        {
        //            wrkshopIDs = selectedData.workshopLocationObj.Split(',').Select(long.Parse).ToList();
        //            wrkshopIDs1 = wrkshopIDs.ToString();
        //        }
        //        if (selectedData.fromPolicyExpDataObj != null && selectedData.toPolicyExpDataObj != null)
        //        {
        //            var fromDateString = Convert.ToDateTime(selectedData.fromPolicyExpDataObj).ToString("yyyy-MM-dd");
        //            fromDate = DateTime.Parse(fromDateString);
        //            var toDateString = Convert.ToDateTime(selectedData.toPolicyExpDataObj).ToString("yyyy-MM-dd");
        //            toDate = DateTime.Parse(toDateString);
        //        }
        //        if (selectedData.isAssignedObj != null)
        //        {
        //            assignedData = null;
        //            var vehIdFromInteraction = dBContext.insuranceassignedinteractions.Select(x => x.vehicle_vehicle_id).ToList();
        //            DateTime? uploadCurDate = new DateTime?();
        //            if (selectedData.isAssignedObj == "1")//no
        //            {
        //                assignedData = dBContext.insuranceforecasteddatas.Where(x => (x.policyexpirydate >= fromDate
        //                && x.policyexpirydate <= toDate)
        //                && campaignIDs.Contains(x.Campaign ?? default(int))
        //                && renewalIDs.Contains(x.renewaltype ?? default(int))
        //                && wrkshopIDs.Contains(x.location_id)
        //                && !vehIdFromInteraction.Contains(x.vehicle_id)).ToList();//on same  date
        //                foreach (var item in assignedData)
        //                {
        //                    item.updated_date = null;
        //                    item.crename = null;
        //                }
        //            }
        //            else if (selectedData.isAssignedObj == "0")//yes
        //            {
        //                assignedData = dBContext.insuranceforecasteddatas.Where(x => (x.policyexpirydate >= fromDate
        //                && x.policyexpirydate <= toDate)
        //                && campaignIDs.Contains(x.Campaign ?? default(int))
        //                && renewalIDs.Contains(x.renewaltype ?? default(int))
        //                && wrkshopIDs.Contains(x.location_id)
        //                && vehIdFromInteraction.Contains(x.vehicle_id)).ToList();//on same  date
        //                //uploadCurDate = dBContext.insuranceassignedinteractions.FirstOrDefault(m => assignedData.Any(x => x.vehicle_id == m.vehicle_vehicle_id)).uplodedCurrentDate ?? default(DateTime);
        //                foreach (var item in assignedData)
        //                {
        //                    item.updated_date = dBContext.insuranceassignedinteractions.Single(x => x.vehicle_vehicle_id == item.vehicle_id).uplodedCurrentDate;
        //                    long? wyzId = dBContext.insuranceassignedinteractions.Single(x => x.vehicle_vehicle_id == item.vehicle_id).wyzUser_id;
        //                    item.crename = dBContext.wyzusers.Single(x => x.id == wyzId).userName;
        //                }
        //            }
        //        }
        //    }

        //    return assignedData;
        //}

        public Dictionary<int, string> doJoint(string values)
        {
            Dictionary<int, string> returnData = new Dictionary<int, string>();
            //try
            //{
            var selectedData = JsonConvert.DeserializeObject<ForecastInsuranceFilter>(values);
            List<int> campaignIDs = new List<int>();
            List<int> renewalIDs = new List<int>();
            List<long> wrkshopIDs = new List<long>();
            List<long> sublocations = new List<long>();

            long wrkshopIDs1 = 0;
            long sublocations1 = 0;
            DateTime fromDate = new DateTime();
            DateTime toDate = new DateTime();
            string modelType = "";
            if (values.Contains("modelTypeObj"))
            {
                modelType = selectedData.modelTypeObj;
            }


            if (selectedData.cmpaignObj != null)
            {
                campaignIDs = selectedData.cmpaignObj.Split(',').Select(int.Parse).ToList();
            }
            
            if (selectedData.renewaltypeObj != null)
            {
                renewalIDs = selectedData.renewaltypeObj.Split(',').Select(int.Parse).ToList();
            }

            //if (selectedData.workshopLocationObj != null)
            //{
            //    if (selectedData.workshopLocationObj.Contains(','))
            //    {
            //        wrkshopIDs = selectedData.workshopLocationObj.Split(',').Select(long.Parse).ToList();
            //        // wrkshopIDs1 = wrkshopIDs.ToString();
            //    }
            //    else
            //    {
            //        wrkshopIDs1 = long.Parse(selectedData.workshopLocationObj);
            //    }

            //}

            //if (selectedData.locationObj != null)
            //{
            //    if (selectedData.locationObj.Contains(','))
            //    {
            //        sublocations = selectedData.locationObj.Split(',').Select(long.Parse).ToList();
            //        // wrkshopIDs1 = wrkshopIDs.ToString();
            //    }
            //    else
            //    {
            //        sublocations1 = long.Parse(selectedData.locationObj);
            //    }

            //}
            if (selectedData.workshopLocationObj != null)
            {
                if (selectedData.workshopLocationObj.Contains(','))
                {
                    wrkshopIDs = selectedData.workshopLocationObj.Split(',').Select(long.Parse).ToList();
                    // wrkshopIDs1 = wrkshopIDs.ToString();
                }
                else
                {
                    wrkshopIDs1 = long.Parse(selectedData.workshopLocationObj);
                }

            }   




            if (selectedData.fromPolicyExpDataObj != null && selectedData.toPolicyExpDataObj != null)
            {
                var fromDateString = Convert.ToDateTime(selectedData.fromPolicyExpDataObj).ToString("yyyy-MM-dd");

                fromDate = DateTime.Parse(fromDateString);


                var toDateString = Convert.ToDateTime(selectedData.toPolicyExpDataObj).ToString("yyyy-MM-dd");
                toDate = DateTime.Parse(toDateString);
            }

            List<CallLogAjaxLoadInsurance> callLogsInsurance = new List<CallLogAjaxLoadInsurance>();
            using (var db = new AutoSherDBContext())
            {
                var count = db.insuranceforecasteddatas.Count();
                var vehIdFromInteraction = db.insuranceassignedinteractions.Select(x => x.vehicle_vehicle_id).ToList();

                if (Session["OEM"].ToString() == "MARUTI SUZUKI")
                {
                    if (modelType == "")
                    {
                        if (selectedData.isAssignedObj == "1")//no
                        {
                            var assign = (from ins in db.insuranceforecasteddatas
                                          join cmp in db.campaigns on (long)ins.Campaign equals cmp.id
                                          join loc in db.workshops on ins.location_id equals loc.id
                                          join ren in db.renewaltypes on (long)ins.renewaltype equals ren.id
                                          join veh in db.vehicles on (long)ins.vehicle_id equals veh.vehicle_id
                                          where (ins.policyexpirydate >= fromDate && ins.policyexpirydate <= toDate) && campaignIDs.Contains(ins.Campaign ?? default(int))
                                          && renewalIDs.Contains(ins.renewaltype ?? default(int)) && (wrkshopIDs.Contains(ins.location_id) || wrkshopIDs1 == ins.location_id) &&
                                          !vehIdFromInteraction.Contains(ins.vehicle_id) && ((selectedData.duedateEdited == "1" && ins.duedateEdited.HasValue) || (selectedData.duedateEdited == "0" && !ins.duedateEdited.HasValue) || String.IsNullOrEmpty(selectedData.duedateEdited))
                                          select
                                                new
                                                {
                                                    campaign = cmp.campaignName,
                                                    customerName = ins.customerName,
                                                    chassisNo = ins.chassisNo,
                                                    vehicle_RegNo = ins.vehicleRegNo,
                                                    phoneNUmber = ins.phonenumber,
                                                    NextRenewalType = ren.renewalTypeName,
                                                    policyDueDate = ins.policyexpirydate,
                                                    last_insurancecompanyname = ins.insurancecompanyname,
                                                    location = loc.workshopName,
                                                    assigned = ins.IsAssigned,
                                                    assignedDate = ins.assigneddate,
                                                    creName = ins.crename,
                                                    lastDispo = ins.lastdisposition,
                                                    vehicle_vehicle_id = ins.vehicle_id,
                                                    customerId = ins.customer_id,
                                                    last_insuirancecompanyname = ins.insurancecompanyname,
                                                    locationID = ins.location_id,
                                                    campaignID = cmp.id,
                                                    nextRenewalTypeID = ren.id,
                                                    modelCategory = ""
                                                }).ToList();

                            string JsonData = JsonConvert.SerializeObject(assign);
                            returnData[assign.Count()] = JsonData;
                            return returnData;
                        }
                        else if (selectedData.isAssignedObj == "2")
                        {
                            //// var abc= from ins in db.insuranceforecasteddatas
                            //    where vehIdFromInteraction.Contains(ins.vehicle_id).ToString


                            var assigneddate = db.insuranceassignedinteractions.Where(x => vehIdFromInteraction.Contains(x.vehicle_vehicle_id)).Select(x => x.uplodedCurrentDate).ToList();
                            //List<long?> wyzId = db.insuranceassignedinteractions.Where(x => vehIdFromInteraction.Contains(x.vehicle_vehicle_id)).Select(x=>x.wyzUser_id).ToList();
                            //var crename = db.wyzusers.Where(x => wyzId.Contains(x.id)).Select(x=>x.userName);
                            var assign = (from ins in db.insuranceforecasteddatas
                                          join cmp in db.campaigns on (long)ins.Campaign equals cmp.id
                                          join loc in db.workshops on ins.location_id equals loc.id
                                          join ren in db.renewaltypes on (long)ins.renewaltype equals ren.id
                                          join veh in db.vehicles on (long)ins.vehicle_id equals veh.vehicle_id
                                          join insassignInter in db.insuranceassignedinteractions on ins.vehicle_id equals insassignInter.vehicle_vehicle_id
                                          join wyz in db.wyzusers on insassignInter.wyzUser_id equals wyz.id
                                          where (/*EntityFunctions.TruncateTime(*/ins.policyexpirydate >= /*EntityFunctions.TruncateTime(*/fromDate && /*EntityFunctions.TruncateTime(*/ins.policyexpirydate <= /*EntityFunctions.TruncateTime(*/toDate && campaignIDs.Contains(ins.Campaign ?? default(int))
                                          && renewalIDs.Contains(ins.renewaltype ?? default(int)) && (wrkshopIDs.Contains(ins.location_id)) &&
                                          vehIdFromInteraction.Contains(ins.vehicle_id)) && ((selectedData.duedateEdited == "1" && ins.duedateEdited.HasValue) || (selectedData.duedateEdited == "0" && !ins.duedateEdited.HasValue) || String.IsNullOrEmpty(selectedData.duedateEdited))
                                          select
                                                new
                                                {
                                                    campaign = cmp.campaignName,
                                                    customerName = ins.customerName,
                                                    chassisNo = ins.chassisNo,
                                                    vehicle_RegNo = ins.vehicleRegNo,
                                                    phoneNUmber = ins.phonenumber,
                                                    NextRenewalType = ren.renewalTypeName,
                                                    policyDueDate = ins.policyexpirydate,
                                                    last_insurancecompanyname = ins.insurancecompanyname,
                                                    location = loc.workshopName,
                                                    assigned = ins.IsAssigned,
                                                    assignedDate = insassignInter.uplodedCurrentDate,
                                                    creName = wyz.userName,
                                                    lastDispo = ins.lastdisposition,
                                                    vehicle_vehicle_id = ins.vehicle_id,
                                                    customerId = ins.customer_id,
                                                    last_insuirancecompanyname = ins.insurancecompanyname,
                                                    locationID = ins.location_id,
                                                    campaignID = cmp.id,
                                                    nextRenewalTypeID = ren.id,
                                                    modelCategory = ""

                                                }).ToList();

                            string JsonData = JsonConvert.SerializeObject(assign);
                            returnData[assign.Count()] = JsonData;
                            return returnData;
                        }
                    }
                    else if (modelType != "")
                    {
                        List<long> modelTypeLong = modelType.Split(',').Select(long.Parse).ToList();
                        if (selectedData.isAssignedObj == "1")//no
                        {
                            var assign = (from ins in db.insuranceforecasteddatas
                                          join cmp in db.campaigns on (long)ins.Campaign equals cmp.id
                                          join loc in db.workshops on ins.location_id equals loc.id
                                          join ren in db.renewaltypes on (long)ins.renewaltype equals ren.id
                                          join veh in db.vehicles on (long)ins.vehicle_id equals veh.vehicle_id
                                          join mod in db.Modelcategories on veh.Modelcat equals mod.modelcatid
                                          where ((ins.policyexpirydate >= fromDate && ins.policyexpirydate <= toDate) || (ins.duedateEdited >= fromDate && ins.duedateEdited <= toDate)) && campaignIDs.Contains(ins.Campaign ?? default(int))
                                          && renewalIDs.Contains(ins.renewaltype ?? default(int)) && (wrkshopIDs.Contains(ins.location_id) || wrkshopIDs1 == ins.location_id) &&
                                          !vehIdFromInteraction.Contains(ins.vehicle_id) && modelTypeLong.Contains(veh.Modelcat) && ((selectedData.duedateEdited == "1" && ins.duedateEdited.HasValue) || (selectedData.duedateEdited == "0" && !ins.duedateEdited.HasValue) || String.IsNullOrEmpty(selectedData.duedateEdited))
                                          select
                                                new
                                                {
                                                    campaign = cmp.campaignName,
                                                    customerName = ins.customerName,
                                                    chassisNo = ins.chassisNo,
                                                    vehicle_RegNo = ins.vehicleRegNo,
                                                    phoneNUmber = ins.phonenumber,
                                                    NextRenewalType = ren.renewalTypeName,
                                                    policyDueDate = ins.policyexpirydate,
                                                    last_insurancecompanyname = ins.insurancecompanyname,
                                                    location = loc.workshopName,
                                                    assigned = ins.IsAssigned,
                                                    assignedDate = ins.assigneddate,
                                                    creName = ins.crename,
                                                    lastDispo = ins.lastdisposition,
                                                    vehicle_vehicle_id = ins.vehicle_id,
                                                    customerId = ins.customer_id,
                                                    last_insuirancecompanyname = ins.insurancecompanyname,
                                                    locationID = ins.location_id,
                                                    campaignID = cmp.id,
                                                    nextRenewalTypeID = ren.id,
                                                    modelCategory = mod.modelcat,

                                                    //workshopId = wksp.id
                                                }).ToList();

                            string JsonData = JsonConvert.SerializeObject(assign);
                            returnData[assign.Count()] = JsonData;
                            return returnData;
                        }
                        else if (selectedData.isAssignedObj == "2")
                        {
                            //// var abc= from ins in db.insuranceforecasteddatas
                            //    where vehIdFromInteraction.Contains(ins.vehicle_id).ToString


                            var assigneddate = db.insuranceassignedinteractions.Where(x => vehIdFromInteraction.Contains(x.vehicle_vehicle_id)).Select(x => x.uplodedCurrentDate).ToList();
                            //List<long?> wyzId = db.insuranceassignedinteractions.Where(x => vehIdFromInteraction.Contains(x.vehicle_vehicle_id)).Select(x=>x.wyzUser_id).ToList();
                            //var crename = db.wyzusers.Where(x => wyzId.Contains(x.id)).Select(x=>x.userName);
                            var assign = (from ins in db.insuranceforecasteddatas
                                          join cmp in db.campaigns on (long)ins.Campaign equals cmp.id
                                          join loc in db.workshops on ins.location_id equals loc.id
                                          join ren in db.renewaltypes on (long)ins.renewaltype equals ren.id
                                          join veh in db.vehicles on (long)ins.vehicle_id equals veh.vehicle_id
                                          join mod in db.Modelcategories on veh.Modelcat equals mod.modelcatid
                                          join insassignInter in db.insuranceassignedinteractions on ins.vehicle_id equals insassignInter.vehicle_vehicle_id
                                          join wyz in db.wyzusers on insassignInter.wyzUser_id equals wyz.id
                                          where ((ins.policyexpirydate >= fromDate && ins.policyexpirydate <= toDate) || (ins.duedateEdited >= fromDate && ins.duedateEdited <= toDate)) && campaignIDs.Contains(ins.Campaign ?? default(int))
                                          && renewalIDs.Contains(ins.renewaltype ?? default(int)) && (wrkshopIDs.Contains(ins.location_id)) && ((selectedData.duedateEdited == "1" && ins.duedateEdited.HasValue) || (selectedData.duedateEdited == "0" && !ins.duedateEdited.HasValue) || String.IsNullOrEmpty(selectedData.duedateEdited))
                                          select
                                                new
                                                {
                                                    campaign = cmp.campaignName,
                                                    customerName = ins.customerName,
                                                    chassisNo = ins.chassisNo,
                                                    vehicle_RegNo = ins.vehicleRegNo,
                                                    phoneNUmber = ins.phonenumber,
                                                    NextRenewalType = ren.renewalTypeName,
                                                    policyDueDate = ins.policyexpirydate,
                                                    last_insurancecompanyname = ins.insurancecompanyname,
                                                    location = loc.workshopName,
                                                    assigned = ins.IsAssigned,
                                                    assignedDate = insassignInter.uplodedCurrentDate,
                                                    creName = wyz.userName,
                                                    lastDispo = ins.lastdisposition,
                                                    vehicle_vehicle_id = ins.vehicle_id,
                                                    customerId = ins.customer_id,
                                                    last_insuirancecompanyname = ins.insurancecompanyname,
                                                    locationID = ins.location_id,
                                                    campaignID = cmp.id,
                                                    nextRenewalTypeID = ren.id,
                                                    modelCategory = mod.modelcat

                                                }).ToList();

                            string JsonData = JsonConvert.SerializeObject(assign);
                            returnData[assign.Count()] = JsonData;
                            return returnData;
                        }
                    }

                }
                else
                {
                    if (selectedData.isAssignedObj == "1")//no
                    {
                        var assign = (from ins in db.insuranceforecasteddatas
                                      join cmp in db.campaigns on (long)ins.Campaign equals cmp.id
                                      join loc in db.workshops on ins.location_id equals loc.id

                                      join ren in db.renewaltypes on (long)ins.renewaltype equals ren.id
                                      where ((ins.policyexpirydate >= fromDate && ins.policyexpirydate <= toDate) || (ins.duedateEdited >= fromDate && ins.duedateEdited <= toDate)) && campaignIDs.Contains(ins.Campaign ?? default(int))
                                      && renewalIDs.Contains(ins.renewaltype ?? default(int)) && (wrkshopIDs.Contains(ins.location_id) || wrkshopIDs1 == ins.location_id) &&
                                      !vehIdFromInteraction.Contains(ins.vehicle_id) && ((selectedData.duedateEdited == "1" && ins.duedateEdited.HasValue) || (selectedData.duedateEdited == "0" && !ins.duedateEdited.HasValue) || String.IsNullOrEmpty(selectedData.duedateEdited))
                                      select
                                            new
                                            {
                                                campaign = cmp.campaignName,
                                                customerName = ins.customerName,
                                                chassisNo = ins.chassisNo,
                                                vehicle_RegNo = ins.vehicleRegNo,
                                                phoneNUmber = ins.phonenumber,
                                                NextRenewalType = ren.renewalTypeName,
                                                policyDueDate = ins.policyexpirydate,
                                                last_insurancecompanyname = ins.insurancecompanyname,
                                                location = loc.workshopName,
                                                assigned = ins.IsAssigned,
                                                assignedDate = ins.assigneddate,
                                                creName = ins.crename,
                                                lastDispo = ins.lastdisposition,
                                                vehicle_vehicle_id = ins.vehicle_id,
                                                customerId = ins.customer_id,
                                                last_insuirancecompanyname = ins.insurancecompanyname,
                                                locationID = ins.location_id,
                                                campaignID = cmp.id,
                                                nextRenewalTypeID = ren.id
                                            }).ToList();

                        string JsonData = JsonConvert.SerializeObject(assign);
                        returnData[assign.Count()] = JsonData;
                        return returnData;
                    }
                    else if (selectedData.isAssignedObj == "2")//yes
                    {
                        //// var abc= from ins in db.insuranceforecasteddatas
                        //    where vehIdFromInteraction.Contains(ins.vehicle_id).ToString


                        var assigneddate = db.insuranceassignedinteractions.Where(x => vehIdFromInteraction.Contains(x.vehicle_vehicle_id)).Select(x => x.uplodedCurrentDate).ToList();

                        // List<long?> wyzId = db.insuranceassignedinteractions.Where(x => vehIdFromInteraction.Contains(x.vehicle_vehicle_id)).Select(x=>x.wyzUser_id).ToList();
                        // var crename = db.wyzusers.Where(x => wyzId.Contains(x.id)).Select(x=>x.userName);

                        var assign = (from ins in db.insuranceforecasteddatas
                                      join cmp in db.campaigns on (long)ins.Campaign equals cmp.id
                                      join loc in db.workshops on ins.location_id equals loc.id
                                      join ren in db.renewaltypes on (long)ins.renewaltype equals ren.id
                                      join insassignInter in db.insuranceassignedinteractions on ins.vehicle_id equals insassignInter.vehicle_vehicle_id
                                      join wyz in db.wyzusers on insassignInter.wyzUser_id equals wyz.id
                                      where ((ins.policyexpirydate >= fromDate && ins.policyexpirydate <= toDate) || (ins.duedateEdited >= fromDate && ins.duedateEdited <= toDate) && campaignIDs.Contains(ins.Campaign ?? default(int))
                                      && renewalIDs.Contains(ins.renewaltype ?? default(int)) && (wrkshopIDs.Contains(ins.location_id) || wrkshopIDs1 == ins.location_id) &&
                                      vehIdFromInteraction.Contains(ins.vehicle_id)) && ((selectedData.duedateEdited == "1" && ins.duedateEdited.HasValue) || (selectedData.duedateEdited == "0" && !ins.duedateEdited.HasValue) || String.IsNullOrEmpty(selectedData.duedateEdited))
                                      select
                                            new
                                            {
                                                campaign = cmp.campaignName,
                                                customerName = ins.customerName,
                                                chassisNo = ins.chassisNo,
                                                vehicle_RegNo = ins.vehicleRegNo,
                                                phoneNUmber = ins.phonenumber,
                                                NextRenewalType = ren.renewalTypeName,
                                                policyDueDate = ins.policyexpirydate,
                                                last_insurancecompanyname = ins.insurancecompanyname,
                                                location = loc.workshopName,
                                                assigned = ins.IsAssigned,
                                                assignedDate = insassignInter.uplodedCurrentDate,
                                                // assignedDate = ins.assigneddate,

                                                creName = ins.crename,
                                                //creName = wyz.userName,
                                                lastDispo = ins.lastdisposition,
                                                vehicle_vehicle_id = ins.vehicle_id,
                                                customerId = ins.customer_id,
                                                last_insuirancecompanyname = ins.insurancecompanyname,
                                                locationID = ins.location_id,
                                                campaignID = cmp.id,
                                                nextRenewalTypeID = ren.id,


                                            }).ToList();

                        string JsonData = JsonConvert.SerializeObject(assign);
                        returnData[assign.Count()] = JsonData;
                        return returnData;
                    }
                }

            }

            return returnData;
        }
        public ActionResult FilterForecastData(string values)
        {
            //List<insuranceforecasteddata> assignedData = new List<insuranceforecasteddata>();
            //assignedData = getTableData(values);
            int totalCount = 0;
            List<CallLogAjaxLoadInsurance> insuData = new List<CallLogAjaxLoadInsurance>();
            try
            {

                Dictionary<int, string> data = new Dictionary<int, string>();
                data = doJoint(values);

                string asignmentData = data.ElementAt(0).Value;
                totalCount = data.ElementAt(0).Key;
                insuData = JsonConvert.DeserializeObject<List<CallLogAjaxLoadInsurance>>(asignmentData);
            }
            catch (Exception ex)
            {
                string inner;
                if (ex.InnerException != null)
                {

                    if (ex.InnerException.InnerException != null)
                    {
                        inner = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        inner = ex.InnerException.Message;
                    }
                }
                else
                {
                    inner = ex.Message;
                }
                return Json(new { data = "", draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount, exception = inner });
            }


            var jsondata = Json(new { data = insuData, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount, exception = "" });
            jsondata.MaxJsonLength = Int32.MaxValue;
            return jsondata;
        }
        public ActionResult submitModal(string values)
        {
            try
            {
                var selectedData = JsonConvert.DeserializeObject<ForecastInsuranceFilter>(values);
                string campaignData = selectedData.ModalCampaign;
                string campaignId = "";
                long uploadId = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                campaign campaigns = new campaign();
                insuranceassignedinteraction insuranceassignedinteraction = new insuranceassignedinteraction();
                long incremamangeridvar = long.Parse(Session["UserId"].ToString());

                List<CallLogAjaxLoadInsurance> insuData = new List<CallLogAjaxLoadInsurance>();
                Dictionary<int, string> data = new Dictionary<int, string>();
                data = doJoint(values);
                insuData = JsonConvert.DeserializeObject<List<CallLogAjaxLoadInsurance>>((data?.FirstOrDefault().Value != "[]" ? data?.FirstOrDefault().Value : String.Empty));


                using (AutoSherDBContext dbContext = new AutoSherDBContext())
                {

                    if (campaignData.Contains(","))
                    {
                        if (campaignData.Split(',')[1] == "ddl")
                        {
                            campaignId = campaignData.Split(',')[0];
                        }
                        else if (campaignData.Split(',')[1] == "txt")
                        {
                            string campName = campaignData.Split(',')[0];
                            campaigns.campaignName = campName;
                            campaigns.campaignType = "Insurance";
                            campaigns.createdDate = DateTime.Now;
                            campaigns.isValid = true;
                            campaigns.isactive = true;
                            dbContext.campaigns.Add(campaigns);
                            dbContext.SaveChanges();
                            //campaignId = dBContext.campaigns.Single(x => x.campaignName.Contains(campName)).id.ToString();
                            campaignId = campaigns.id.ToString();
                        }
                    }
                    List<int> wyzuserId = new List<int>();
                    wyzuserId = selectedData.creList.Split(',').Select(int.Parse).ToList();
                    int wyzID = 0;
                    List<long?> vehicleIDs = dbContext.insuranceassignedinteractions.Select(x => x.vehicle_vehicle_id).ToList();
                    List<long> workshopList = selectedData.workshopLocationObj.Split(',').Select(long.Parse).ToList();

                    for (int j = 0; j < workshopList.Count(); j++)
                    {
                        //dividing cre's based on selected workshop

                        var wrk = workshopList[j];
                        List<long> listOfWyzuserId = dbContext.userworkshops.Where(x => x.workshopList_id == wrk).Select(x => x.userWorkshop_id).ToList();
                        List<int> listOfIntWyzuserId = listOfWyzuserId.ConvertAll(i => (int)i);
                        List<int> wyzIdforSelectedWorkshop = new List<int>();

                        for (int k = 0; k < listOfIntWyzuserId.Count(); k++)
                        {
                            if (wyzuserId.Contains(listOfIntWyzuserId[k]))
                            {
                                wyzIdforSelectedWorkshop.Add(listOfIntWyzuserId[k]);
                            }
                        }

                        //diving data based on selected workshop
                        //var locID = dbContext.workshops.Where(x => workshopList.Contains(x.id)).Select(x=>x.location_cityId).Distinct().ToList();
                        //var locIdFiltered = locID[j];
                        var filteredData = insuData?.Where(x => x.locationID == wrk).ToList();

                        for (int i = 0; i < filteredData?.Count(); i++)
                        {
                            long vehId = filteredData[i].vehicle_vehicle_id;
                            //var serviceTypeID = dbContext.smrforecasteddatas.Single(x => x.vehicle_id == vehId).ServiceTypeId;
                            if (!vehicleIDs.Contains(vehId))
                            {
                                if (i == 0)
                                {
                                    wyzID = wyzuserId[i];
                                    //wyzID = wyzIdforSelectedWorkshop[i];
                                }
                                else
                                {
                                    int position = i % wyzuserId.Count;
                                    wyzID = wyzuserId[position];
                                    //int position = i % wyzIdforSelectedWorkshop.Count;
                                    //wyzID = wyzIdforSelectedWorkshop[position];
                                }

                                insuranceassignedinteraction.callMade = "No";
                                insuranceassignedinteraction.interactionType = null;
                                insuranceassignedinteraction.lastDisposition = null;
                                insuranceassignedinteraction.uplodedCurrentDate = DateTime.Now;
                                insuranceassignedinteraction.customer_id = long.Parse(filteredData[i].customerId);
                                insuranceassignedinteraction.finalDisposition_id = null;
                                insuranceassignedinteraction.vehicle_vehicle_id = filteredData[i].vehicle_vehicle_id;
                                insuranceassignedinteraction.wyzUser_id = long.Parse(wyzID.ToString());//to whom u hv assigned
                                insuranceassignedinteraction.displayFlag = false;
                                insuranceassignedinteraction.campaign_id = long.Parse(campaignId);
                                insuranceassignedinteraction.insurance_id = null;
                                insuranceassignedinteraction.policyDueDate = filteredData[i].policyDueDate;
                                insuranceassignedinteraction.policyDueType = filteredData[i].nextRenewalTypeID.ToString();
                                insuranceassignedinteraction.insuranceCompanyName = filteredData[i].last_insuirancecompanyname;
                                insuranceassignedinteraction.location_id = filteredData[i].locationID;
                                insuranceassignedinteraction.upload_id = uploadId;
                                insuranceassignedinteraction.isAutoAssigned = false;
                                insuranceassignedinteraction.assigned_wyzuser_id = wyzID;//
                                insuranceassignedinteraction.assigned_manager_id = incremamangeridvar;

                                dbContext.insuranceassignedinteractions.Add(insuranceassignedinteraction);
                                dbContext.SaveChanges();
                            }
                        }

                    }
                }
                return Json(new { success = true });
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

        //for dashboard count

        public ActionResult insDashboardCount(string values)
        {
            List<CallLogDispositionLoadInsurance> selectedVeh = new List<CallLogDispositionLoadInsurance>();
            List<CallLogDispositionLoadInsurance> selectedAssigned = new List<CallLogDispositionLoadInsurance>();
            List<CallLogDispositionLoadInsurance> selectedBalanced = new List<CallLogDispositionLoadInsurance>();
            List<CallLogDispositionLoadInsurance> filteredVeh = new List<CallLogDispositionLoadInsurance>();
            try
            {
                var filters = JsonConvert.DeserializeObject<ForecastInsuranceFilter>(values);

                string policyFromDate, policyToDate, workshopId, campaignId, renewalTypeId, dueDateEdited;
                int dashboardID;
                policyFromDate = filters.fromPolicyExpDataObj == null ? "0" : filters.fromPolicyExpDataObj;
                policyToDate = filters.toPolicyExpDataObj == null ? "0" : filters.toPolicyExpDataObj;
                workshopId = filters.workshopLocationObj == null ? "0" : filters.workshopLocationObj;
                campaignId = filters.cmpaignObj == null ? "0" : filters.cmpaignObj;
                renewalTypeId = filters.renewaltypeObj == null ? "0" : filters.renewaltypeObj;
                dueDateEdited = filters.duedateEdited == null ? "" : filters.duedateEdited;
                selectedVeh = getDashCountOnIDs(policyFromDate, policyToDate, workshopId, campaignId, renewalTypeId, 1, dueDateEdited);
                selectedAssigned = getDashCountOnIDs(policyFromDate, policyToDate, workshopId, campaignId, renewalTypeId, 2, dueDateEdited);
                selectedBalanced = getDashCountOnIDs(policyFromDate, policyToDate, workshopId, campaignId, renewalTypeId, 3, dueDateEdited);
                filteredVeh = getDashCountOnIDs(policyFromDate, policyToDate, workshopId, campaignId, renewalTypeId, 4, dueDateEdited);
            }
            catch (Exception ex)
            {
                ViewBag.exception = ex.ToString().Substring(0, 20);
            }

            return Json(new { success = true, selectedVeh = selectedVeh, selectedAssigned = selectedAssigned, selectedBalanced = selectedBalanced, filteredVeh = filteredVeh, draw = Request["draw"], JsonRequestBehavior.AllowGet });
        }
        public List<CallLogDispositionLoadInsurance> getDashCountOnIDs(string policyFromDate, string policyToDate, string workshopId, string campaignId, string renewalTypeId, int dashboardID,string dueDateEdited="")
        {
            List<CallLogDispositionLoadInsurance> CallDashboard = new List<CallLogDispositionLoadInsurance>();
            using (AutoSherDBContext dBContext = new AutoSherDBContext())
            {
                var str = @"CALL insForecastDashboardCounts(@instartdate,@inenddate, @inworkshopId,@incampaign,@inrenewaltype,@indashboardId,@isEditedDate)";

                MySqlParameter[] param = new MySqlParameter[]
                {
                                    new MySqlParameter("instartdate",policyFromDate),
                                    new MySqlParameter("inenddate",policyToDate),
                                    new MySqlParameter("inworkshopId",workshopId),
                                    new MySqlParameter("incampaign",campaignId),
                                    new MySqlParameter("inrenewaltype",renewalTypeId),
                                    new MySqlParameter("indashboardId",dashboardID),
                                    new MySqlParameter("isEditedDate",dueDateEdited)
                };
                CallDashboard = dBContext.Database.SqlQuery<CallLogDispositionLoadInsurance>(str, param).ToList();
            }
            return CallDashboard;
        }

        //For InsuranceDue
        public ActionResult downloadInsuranceForecastInsDueFormat(string values)
        {
            List<CallLogAjaxLoadInsurance> CallInsuranceDue = new List<CallLogAjaxLoadInsurance>();
            try
            {

                string policyFromDate, policyToDate, workshopId, campaignId, renewalTypeId, assigned, pattern;
                long fromIndex, toIndex;
                var filters = JsonConvert.DeserializeObject<ForecastInsuranceFilter>(values);
                policyFromDate = filters.fromPolicyExpDataObj == null ? "0" : filters.fromPolicyExpDataObj;
                policyToDate = filters.toPolicyExpDataObj == null ? "0" : filters.toPolicyExpDataObj;
                workshopId = filters.workshopLocationObj == null ? "0" : filters.workshopLocationObj;
                campaignId = filters.cmpaignObj == null ? "0" : filters.cmpaignObj;
                renewalTypeId = filters.renewaltypeObj == null ? "0" : filters.renewaltypeObj;
                assigned = filters.isAssignedObj == null ? "0" : filters.isAssignedObj;
                pattern = filters.searchPatternObj;
                fromIndex = 0;
                toIndex = 0;

                //renewalTypeId = "0";
                //assigned = "0";

                using (AutoSherDBContext dBContext = new AutoSherDBContext())
                {
                    var str = @"CALL insuranceForecastDataListCount(@instartdate,@inenddate,@inworkshopId,@incampaign,@inrenewaltype,@inassigned,@pattern)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("instartdate",policyFromDate),
                        new MySqlParameter("inenddate",policyToDate),
                        new MySqlParameter("inworkshopId",workshopId),
                        new MySqlParameter("incampaign", campaignId),
                        new MySqlParameter("inrenewaltype", renewalTypeId),
                        new MySqlParameter("inassigned", assigned),
                        new MySqlParameter("pattern", pattern)
                    };

                    toIndex = dBContext.Database.SqlQuery<long>(str, param).FirstOrDefault();
                }

                CallInsuranceDue = getInsuranceForecastInsDueFormat(policyFromDate, policyToDate, workshopId, campaignId, renewalTypeId, assigned, fromIndex, toIndex);

                //datatable
                DataTable tbl = new DataTable("Insurance Forecast Table");

                tbl.Columns.Add("Customer Name", typeof(string));
                if (Session["DealerCode"].ToString() != "SHIVAMHYUNDAI")
                {
                    tbl.Columns.Add("Mobile No.", typeof(string));
                }
                tbl.Columns.Add("Vehicle Reg. No.", typeof(string));
                tbl.Columns.Add("Chassis No.", typeof(string));
                tbl.Columns.Add("Sale Date", typeof(DateTime));
                tbl.Columns.Add("Policy Due Date", typeof(DateTime));
                tbl.Columns.Add("Policy Due Month", typeof(string));
                tbl.Columns.Add("Next Renewal Type", typeof(string));
                tbl.Columns.Add("Last insurance Company", typeof(string));
                tbl.Columns.Add("workshopName", typeof(string));
                for (var i = 0; i < CallInsuranceDue.Count; i++)
                {
                    if (Session["DealerCode"].ToString() != "SHIVAMHYUNDAI")
                    {
                        tbl.Rows.Add(CallInsuranceDue[i].customerName, CallInsuranceDue[i].Phone, CallInsuranceDue[i].RegNo, CallInsuranceDue[i].chassisNo,
                                     CallInsuranceDue[i].saledate, CallInsuranceDue[i].policyDueDate, CallInsuranceDue[i].policyDueMonth, CallInsuranceDue[i].NextRenewalType,
                                     CallInsuranceDue[i].last_insuirancecompanyname, CallInsuranceDue[i].workshopname);
                    }
                    else
                    {
                        tbl.Rows.Add(CallInsuranceDue[i].customerName, CallInsuranceDue[i].RegNo, CallInsuranceDue[i].chassisNo,
                                     CallInsuranceDue[i].saledate, CallInsuranceDue[i].policyDueDate, CallInsuranceDue[i].policyDueMonth, CallInsuranceDue[i].NextRenewalType,
                                     CallInsuranceDue[i].last_insuirancecompanyname, CallInsuranceDue[i].workshopname);
                    }

                }


                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("InsuranceDue.xlsx", System.Text.Encoding.UTF8));
                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Insurance_Due");
                    ws.Cells["A1"].LoadFromDataTable(tbl, true);
                    //var ms = new System.IO.MemoryStream();
                    //pck.SaveAs(ms);
                    //ms.WriteTo(Response.OutputStream);
                    Session["DownloadExcel_FileManager"] = pck.GetAsByteArray();
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
            return Json(new { success = true });
        }

        public ActionResult Download()
        {
            try
            {
                //string fileName = Session["FileName"].ToString();

                if (Session["DownloadExcel_FileManager"] != null)
                {
                    //var data = new System.IO.MemoryStream();
                    //data = Session["DownloadExcel_FileManager"] as System.IO.MemoryStream;
                    //Session["DownloadExcel_FileManager"] = null;
                    byte[] data = Session["DownloadExcel_FileManager"] as byte[];
                    return File(data, "application/octet-stream", "InsuranceDue_" + DateTime.Now.ToShortDateString() + ".xlsx");
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {

            }
            return new EmptyResult();
        }

        public List<CallLogAjaxLoadInsurance> getInsuranceForecastInsDueFormat(string policyFromDate, string policyToDate,
            string workshopId, string campaignId, string renewalTypeId, string assigned, long fromIndex, long toIndex)
        {
            List<CallLogAjaxLoadInsurance> CallInsuranceDue = new List<CallLogAjaxLoadInsurance>();
            try
            {
                using (AutoSherDBContext dBContext = new AutoSherDBContext())
                {
                    var str = @"CALL insuranceForecastInsuranceDueFormat(@instartdate,@inenddate,@inworkshopId,@incampaign,@inrenewaltype,@inassigned,@start_with,@length)";
                    // ,"InsuranceForecastInsDue");
                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("instartdate",policyFromDate),
                        new MySqlParameter("inenddate",policyToDate),
                        new MySqlParameter("inworkshopId",workshopId),
                        new MySqlParameter("incampaign", campaignId),
                        new MySqlParameter("inrenewaltype", renewalTypeId),
                        new MySqlParameter("inassigned", assigned),
                        new MySqlParameter("start_with", fromIndex),
                        new MySqlParameter("length", toIndex)
                    };
                    CallInsuranceDue = dBContext.Database.SqlQuery<CallLogAjaxLoadInsurance>(str, param).ToList();

                }
            }
            catch (Exception ex)
            {
                ViewBag.exception = ex.ToString().Substring(0, 20);
            }
            return CallInsuranceDue;
        }


        #region Download due ins

        public ActionResult downloadInsuranceForecastLimit(string forecastData)
        {
            List<downloadforecast> CallInsuranceDue = new List<downloadforecast>();

            long count = 0;
            ForecastInsuranceFilter forecastDataFilter = JsonConvert.DeserializeObject<ForecastInsuranceFilter>(forecastData);
            List<int> campaignIDs = new List<int>();
            List<int> renewalIDs = new List<int>();
            List<long> wrkshopIDs = new List<long>();
            long wrkshopIDs1 = 0;
            DateTime fromDate = new DateTime();
            DateTime toDate = new DateTime();
            string modelType = forecastDataFilter.modelTypeObj;


            if (forecastDataFilter.cmpaignObj != null)
            {
                campaignIDs = forecastDataFilter.cmpaignObj.Split(',').Select(int.Parse).ToList();
            }
            if (forecastDataFilter.renewaltypeObj != null)
            {
                renewalIDs = forecastDataFilter.renewaltypeObj.Split(',').Select(int.Parse).ToList();
            }
            if (forecastDataFilter.workshopLocationObj != null)
            {
                if (forecastDataFilter.workshopLocationObj.Contains(','))
                {
                    wrkshopIDs = forecastDataFilter.workshopLocationObj.Split(',').Select(long.Parse).ToList();
                }
                else
                {
                    wrkshopIDs1 = long.Parse(forecastDataFilter.workshopLocationObj);
                }

            }
            if (forecastDataFilter.fromPolicyExpDataObj != null && forecastDataFilter.toPolicyExpDataObj != null)
            {
                var fromDateString = Convert.ToDateTime(forecastDataFilter.fromPolicyExpDataObj).ToString("yyyy-MM-dd");
                fromDate = DateTime.Parse(fromDateString);
                var toDateString = Convert.ToDateTime(forecastDataFilter.toPolicyExpDataObj).ToString("yyyy-MM-dd");
                toDate = DateTime.Parse(toDateString);
            }
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    var vehIdFromInteraction = db.insuranceassignedinteractions.Select(x => x.vehicle_vehicle_id).ToList();

                    if (Session["OEM"].ToString() == "MARUTI SUZUKI")
                    {
                        if (modelType == "")
                        {
                            if (forecastDataFilter.isAssignedObj == "1")//no
                            {
                                count = (from ins in db.insuranceforecasteddatas
                                         join cmp in db.campaigns on (long)ins.Campaign equals cmp.id
                                         join loc in db.workshops on ins.location_id equals loc.id
                                         join ren in db.renewaltypes on (long)ins.renewaltype equals ren.id
                                         join veh in db.vehicles on (long)ins.vehicle_id equals veh.vehicle_id
                                         where (ins.policyexpirydate >= fromDate && ins.policyexpirydate <= toDate) && campaignIDs.Contains(ins.Campaign ?? default(int))
                                         && renewalIDs.Contains(ins.renewaltype ?? default(int)) && (wrkshopIDs.Contains(ins.location_id) || wrkshopIDs1 == ins.location_id) &&
                                         !vehIdFromInteraction.Contains(ins.vehicle_id)
                                         select new { id = ins.id }).ToList().Count();
                                for (long i = 0; i < count; i += 500)
                                {
                                    List<downloadforecast> CallInsuranceDue1 = null;

                                    CallInsuranceDue1 = (from ins in db.insuranceforecasteddatas
                                                         join cmp in db.campaigns on (long)ins.Campaign equals cmp.id
                                                         join loc in db.workshops on ins.location_id equals loc.id
                                                         join ren in db.renewaltypes on (long)ins.renewaltype equals ren.id
                                                         join veh in db.vehicles on (long)ins.vehicle_id equals veh.vehicle_id
                                                         where (ins.policyexpirydate >= fromDate && ins.policyexpirydate <= toDate) && campaignIDs.Contains(ins.Campaign ?? default(int))
                                                         && renewalIDs.Contains(ins.renewaltype ?? default(int)) && (wrkshopIDs.Contains(ins.location_id) || wrkshopIDs1 == ins.location_id) &&
                                                         !vehIdFromInteraction.Contains(ins.vehicle_id)
                                                         select
                                                               new downloadforecast
                                                               {
                                                                   id = ins.id,
                                                                   campaign = cmp.campaignName,
                                                                   customerName = ins.customerName,
                                                                   chassisNo = ins.chassisNo,
                                                                   vehicle_RegNo = ins.vehicleRegNo,
                                                                   phoneNUmber = ins.phonenumber,
                                                                   NextRenewalType = ren.renewalTypeName,
                                                                   policyDueDate = ins.policyexpirydate,
                                                                   last_insurancecompanyname = ins.insurancecompanyname,
                                                                   location = loc.workshopName,
                                                                   assigned = ins.IsAssigned,
                                                                   assignedDate = ins.assigneddate,
                                                                   creName = ins.crename,
                                                                   lastDispo = ins.lastdisposition,
                                                                   vehicle_vehicle_id = ins.vehicle_id,
                                                                   customerId = ins.customer_id,
                                                                   last_insuirancecompanyname = ins.insurancecompanyname,
                                                                   locationID = ins.location_id,
                                                                   campaignID = cmp.id,
                                                                   nextRenewalTypeID = ren.id,
                                                                   modelCategory = ""
                                                               }).OrderByDescending(m => m.id).Skip(Convert.ToInt32(i)).Take(500).ToList().ToList();

                                    CallInsuranceDue.AddRange(CallInsuranceDue1);
                                }
                            }
                            else if (forecastDataFilter.isAssignedObj == "2")
                            {

                                count = (from ins in db.insuranceforecasteddatas
                                         join cmp in db.campaigns on (long)ins.Campaign equals cmp.id
                                         join loc in db.workshops on ins.location_id equals loc.id
                                         join ren in db.renewaltypes on (long)ins.renewaltype equals ren.id
                                         join veh in db.vehicles on (long)ins.vehicle_id equals veh.vehicle_id
                                         join insassignInter in db.insuranceassignedinteractions on ins.vehicle_id equals insassignInter.vehicle_vehicle_id
                                         join wyz in db.wyzusers on insassignInter.wyzUser_id equals wyz.id
                                         where (ins.policyexpirydate >= fromDate && ins.policyexpirydate <= toDate && campaignIDs.Contains(ins.Campaign ?? default(int))
                                         && renewalIDs.Contains(ins.renewaltype ?? default(int)) && (wrkshopIDs.Contains(ins.location_id)) &&
                                         vehIdFromInteraction.Contains(ins.vehicle_id))
                                         select new { id = ins.id }).ToList().Count();
                                for (long i = 0; i < count; i += 500)
                                {
                                    List<downloadforecast> CallInsuranceDue1 = null;

                                    CallInsuranceDue1 = (from ins in db.insuranceforecasteddatas
                                                         join cmp in db.campaigns on (long)ins.Campaign equals cmp.id
                                                         join loc in db.workshops on ins.location_id equals loc.id
                                                         join ren in db.renewaltypes on (long)ins.renewaltype equals ren.id
                                                         join veh in db.vehicles on (long)ins.vehicle_id equals veh.vehicle_id
                                                         join insassignInter in db.insuranceassignedinteractions on ins.vehicle_id equals insassignInter.vehicle_vehicle_id
                                                         join wyz in db.wyzusers on insassignInter.wyzUser_id equals wyz.id
                                                         where (/*EntityFunctions.TruncateTime(*/ins.policyexpirydate >= /*EntityFunctions.TruncateTime(*/fromDate && /*EntityFunctions.TruncateTime(*/ins.policyexpirydate <= /*EntityFunctions.TruncateTime(*/toDate && campaignIDs.Contains(ins.Campaign ?? default(int))
                                                         && renewalIDs.Contains(ins.renewaltype ?? default(int)) && (wrkshopIDs.Contains(ins.location_id)) &&
                                                         vehIdFromInteraction.Contains(ins.vehicle_id)) /*&&  modelType.Contains(veh.Modelcat)*/
                                                         select
                                                               new downloadforecast
                                                               {
                                                                   id = ins.id,
                                                                   campaign = cmp.campaignName,
                                                                   customerName = ins.customerName,
                                                                   chassisNo = ins.chassisNo,
                                                                   vehicle_RegNo = ins.vehicleRegNo,
                                                                   phoneNUmber = ins.phonenumber,
                                                                   NextRenewalType = ren.renewalTypeName,
                                                                   policyDueDate = ins.policyexpirydate,
                                                                   last_insurancecompanyname = ins.insurancecompanyname,
                                                                   location = loc.workshopName,
                                                                   assigned = ins.IsAssigned,
                                                                   assignedDate = insassignInter.uplodedCurrentDate,
                                                                   creName = wyz.userName,
                                                                   lastDispo = ins.lastdisposition,
                                                                   vehicle_vehicle_id = ins.vehicle_id,
                                                                   customerId = ins.customer_id,
                                                                   last_insuirancecompanyname = ins.insurancecompanyname,
                                                                   locationID = ins.location_id,
                                                                   campaignID = cmp.id,
                                                                   nextRenewalTypeID = ren.id,
                                                                   modelCategory = ""

                                                               }).OrderByDescending(m => m.id).Skip(Convert.ToInt32(i)).Take(500).ToList().ToList();

                                    CallInsuranceDue.AddRange(CallInsuranceDue1);
                                }
                            }
                        }
                        else if (modelType != "")
                        {
                            List<long> modelTypeLong = modelType.Split(',').Select(long.Parse).ToList();
                            if (forecastDataFilter.isAssignedObj == "1")//no
                            {

                                count = (from ins in db.insuranceforecasteddatas
                                         join cmp in db.campaigns on (long)ins.Campaign equals cmp.id
                                         join loc in db.workshops on ins.location_id equals loc.id
                                         join ren in db.renewaltypes on (long)ins.renewaltype equals ren.id
                                         join veh in db.vehicles on (long)ins.vehicle_id equals veh.vehicle_id
                                         join mod in db.Modelcategories on veh.Modelcat equals mod.id
                                         where (ins.policyexpirydate >= fromDate && ins.policyexpirydate <= toDate) && campaignIDs.Contains(ins.Campaign ?? default(int))
                                         && renewalIDs.Contains(ins.renewaltype ?? default(int)) && (wrkshopIDs.Contains(ins.location_id) || wrkshopIDs1 == ins.location_id) &&
                                         !vehIdFromInteraction.Contains(ins.vehicle_id) && modelTypeLong.Contains(veh.Modelcat)
                                         select
                                               new
                                               {
                                                   id = ins.id
                                               }).ToList().Count();
                                for (long i = 0; i < count; i += 500)
                                {
                                    List<downloadforecast> CallInsuranceDue1 = null;

                                    CallInsuranceDue1 = (from ins in db.insuranceforecasteddatas
                                                         join cmp in db.campaigns on (long)ins.Campaign equals cmp.id
                                                         join loc in db.workshops on ins.location_id equals loc.id
                                                         join ren in db.renewaltypes on (long)ins.renewaltype equals ren.id
                                                         join veh in db.vehicles on (long)ins.vehicle_id equals veh.vehicle_id
                                                         join mod in db.Modelcategories on veh.Modelcat equals mod.id
                                                         where (ins.policyexpirydate >= fromDate && ins.policyexpirydate <= toDate) && campaignIDs.Contains(ins.Campaign ?? default(int))
                                                         && renewalIDs.Contains(ins.renewaltype ?? default(int)) && (wrkshopIDs.Contains(ins.location_id) || wrkshopIDs1 == ins.location_id) &&
                                                         !vehIdFromInteraction.Contains(ins.vehicle_id) && modelTypeLong.Contains(veh.Modelcat)
                                                         select
                                                               new downloadforecast
                                                               {
                                                                   id = ins.id,
                                                                   campaign = cmp.campaignName,
                                                                   customerName = ins.customerName,
                                                                   chassisNo = ins.chassisNo,
                                                                   vehicle_RegNo = ins.vehicleRegNo,
                                                                   phoneNUmber = ins.phonenumber,
                                                                   NextRenewalType = ren.renewalTypeName,
                                                                   policyDueDate = ins.policyexpirydate,
                                                                   last_insurancecompanyname = ins.insurancecompanyname,
                                                                   location = loc.workshopName,
                                                                   assigned = ins.IsAssigned,
                                                                   assignedDate = ins.assigneddate,
                                                                   creName = ins.crename,
                                                                   lastDispo = ins.lastdisposition,
                                                                   vehicle_vehicle_id = ins.vehicle_id,
                                                                   customerId = ins.customer_id,
                                                                   last_insuirancecompanyname = ins.insurancecompanyname,
                                                                   locationID = ins.location_id,
                                                                   campaignID = cmp.id,
                                                                   nextRenewalTypeID = ren.id,
                                                                   modelCategory = mod.modelcat,
                                                                   //workshopId = wksp.id
                                                               }).OrderByDescending(m => m.id).Skip(Convert.ToInt32(i)).Take(500).ToList().ToList();

                                    CallInsuranceDue.AddRange(CallInsuranceDue1);
                                }
                            }
                            else if (forecastDataFilter.isAssignedObj == "2")
                            {
                                count = (from ins in db.insuranceforecasteddatas
                                         join cmp in db.campaigns on (long)ins.Campaign equals cmp.id
                                         join loc in db.workshops on ins.location_id equals loc.id
                                         join ren in db.renewaltypes on (long)ins.renewaltype equals ren.id
                                         join veh in db.vehicles on (long)ins.vehicle_id equals veh.vehicle_id
                                         join mod in db.Modelcategories on veh.Modelcat equals mod.id
                                         join insassignInter in db.insuranceassignedinteractions on ins.vehicle_id equals insassignInter.vehicle_vehicle_id
                                         join wyz in db.wyzusers on insassignInter.wyzUser_id equals wyz.id
                                         where (/*EntityFunctions.TruncateTime(*/ins.policyexpirydate >= /*EntityFunctions.TruncateTime(*/fromDate && /*EntityFunctions.TruncateTime(*/ins.policyexpirydate <= /*EntityFunctions.TruncateTime(*/toDate && campaignIDs.Contains(ins.Campaign ?? default(int))
                                         && renewalIDs.Contains(ins.renewaltype ?? default(int)) && (wrkshopIDs.Contains(ins.location_id)) &&
                                         vehIdFromInteraction.Contains(ins.vehicle_id)) && modelTypeLong.Contains(veh.Modelcat)
                                         select
                                               new
                                               {
                                                   id = ins.id
                                               }).ToList().Count();
                                for (long i = 0; i < count; i += 500)
                                {
                                    List<downloadforecast> CallInsuranceDue1 = null;

                                    CallInsuranceDue1 = (from ins in db.insuranceforecasteddatas
                                                         join cmp in db.campaigns on (long)ins.Campaign equals cmp.id
                                                         join loc in db.workshops on ins.location_id equals loc.id
                                                         join ren in db.renewaltypes on (long)ins.renewaltype equals ren.id
                                                         join veh in db.vehicles on (long)ins.vehicle_id equals veh.vehicle_id
                                                         join mod in db.Modelcategories on veh.Modelcat equals mod.id
                                                         join insassignInter in db.insuranceassignedinteractions on ins.vehicle_id equals insassignInter.vehicle_vehicle_id
                                                         join wyz in db.wyzusers on insassignInter.wyzUser_id equals wyz.id
                                                         where (/*EntityFunctions.TruncateTime(*/ins.policyexpirydate >= /*EntityFunctions.TruncateTime(*/fromDate && /*EntityFunctions.TruncateTime(*/ins.policyexpirydate <= /*EntityFunctions.TruncateTime(*/toDate && campaignIDs.Contains(ins.Campaign ?? default(int))
                                                         && renewalIDs.Contains(ins.renewaltype ?? default(int)) && (wrkshopIDs.Contains(ins.location_id)) &&
                                                         vehIdFromInteraction.Contains(ins.vehicle_id)) && modelTypeLong.Contains(veh.Modelcat)
                                                         select
                                                               new downloadforecast
                                                               {
                                                                   id = ins.id,
                                                                   campaign = cmp.campaignName,
                                                                   customerName = ins.customerName,
                                                                   chassisNo = ins.chassisNo,
                                                                   vehicle_RegNo = ins.vehicleRegNo,
                                                                   phoneNUmber = ins.phonenumber,
                                                                   NextRenewalType = ren.renewalTypeName,
                                                                   policyDueDate = ins.policyexpirydate,
                                                                   last_insurancecompanyname = ins.insurancecompanyname,
                                                                   location = loc.workshopName,
                                                                   assigned = ins.IsAssigned,
                                                                   assignedDate = insassignInter.uplodedCurrentDate,
                                                                   creName = wyz.userName,
                                                                   lastDispo = ins.lastdisposition,
                                                                   vehicle_vehicle_id = ins.vehicle_id,
                                                                   customerId = ins.customer_id,
                                                                   last_insuirancecompanyname = ins.insurancecompanyname,
                                                                   locationID = ins.location_id,
                                                                   campaignID = cmp.id,
                                                                   nextRenewalTypeID = ren.id,
                                                                   modelCategory = mod.modelcat

                                                               }).OrderByDescending(m => m.id).Skip(Convert.ToInt32(i)).Take(500).ToList().ToList();

                                    CallInsuranceDue.AddRange(CallInsuranceDue1);
                                }

                            }
                        }

                    }
                    else
                    {
                        if (forecastDataFilter.isAssignedObj == "1")//no
                        {
                            count = (from ins in db.insuranceforecasteddatas
                                     join cmp in db.campaigns on (long)ins.Campaign equals cmp.id
                                     join loc in db.workshops on ins.location_id equals loc.id
                                     join ren in db.renewaltypes on (long)ins.renewaltype equals ren.id
                                     where (ins.policyexpirydate >= fromDate && ins.policyexpirydate <= toDate) && campaignIDs.Contains(ins.Campaign ?? default(int))
                                     && renewalIDs.Contains(ins.renewaltype ?? default(int)) && (wrkshopIDs.Contains(ins.location_id) || wrkshopIDs1 == ins.location_id) &&
                                     !vehIdFromInteraction.Contains(ins.vehicle_id)
                                     select
                                           new
                                           {
                                               id = ins.id
                                           }).ToList().Count();
                            for (long i = 0; i < count; i += 500)
                            {
                                List<downloadforecast> CallInsuranceDue1 = null;

                                CallInsuranceDue1 = (from ins in db.insuranceforecasteddatas
                                                     join cmp in db.campaigns on (long)ins.Campaign equals cmp.id
                                                     join loc in db.workshops on ins.location_id equals loc.id
                                                     join ren in db.renewaltypes on (long)ins.renewaltype equals ren.id
                                                     where (ins.policyexpirydate >= fromDate && ins.policyexpirydate <= toDate) && campaignIDs.Contains(ins.Campaign ?? default(int))
                                                     && renewalIDs.Contains(ins.renewaltype ?? default(int)) && (wrkshopIDs.Contains(ins.location_id) || wrkshopIDs1 == ins.location_id) &&
                                                     !vehIdFromInteraction.Contains(ins.vehicle_id)
                                                     select
                                                           new downloadforecast
                                                           {
                                                               id = ins.id,
                                                               campaign = cmp.campaignName,
                                                               customerName = ins.customerName,
                                                               chassisNo = ins.chassisNo,
                                                               vehicle_RegNo = ins.vehicleRegNo,
                                                               phoneNUmber = ins.phonenumber,
                                                               NextRenewalType = ren.renewalTypeName,
                                                               policyDueDate = ins.policyexpirydate,
                                                               last_insurancecompanyname = ins.insurancecompanyname,
                                                               location = loc.workshopName,
                                                               assigned = ins.IsAssigned,
                                                               assignedDate = ins.assigneddate,
                                                               creName = ins.crename,
                                                               lastDispo = ins.lastdisposition,
                                                               vehicle_vehicle_id = ins.vehicle_id,
                                                               customerId = ins.customer_id,
                                                               last_insuirancecompanyname = ins.insurancecompanyname,
                                                               locationID = ins.location_id,
                                                               campaignID = cmp.id,
                                                               nextRenewalTypeID = ren.id
                                                           }).OrderByDescending(m => m.id).Skip(Convert.ToInt32(i)).Take(500).ToList().ToList();

                                CallInsuranceDue.AddRange(CallInsuranceDue1);
                            }
                        }
                        else if (forecastDataFilter.isAssignedObj == "2")//yes
                        {
                            count = (from ins in db.insuranceforecasteddatas
                                     join cmp in db.campaigns on (long)ins.Campaign equals cmp.id
                                     join loc in db.workshops on ins.location_id equals loc.id
                                     join ren in db.renewaltypes on (long)ins.renewaltype equals ren.id
                                     join insassignInter in db.insuranceassignedinteractions on ins.vehicle_id equals insassignInter.vehicle_vehicle_id
                                     join wyz in db.wyzusers on insassignInter.wyzUser_id equals wyz.id
                                     where (/*EntityFunctions.TruncateTime(*/ins.policyexpirydate >= /*EntityFunctions.TruncateTime(*/fromDate && /*EntityFunctions.TruncateTime(*/ins.policyexpirydate <= /*EntityFunctions.TruncateTime(*/toDate && campaignIDs.Contains(ins.Campaign ?? default(int))
                                     && renewalIDs.Contains(ins.renewaltype ?? default(int)) && (wrkshopIDs.Contains(ins.location_id)) &&
                                     vehIdFromInteraction.Contains(ins.vehicle_id))
                                     select
                                           new
                                           {
                                               id = ins.id
                                           }).ToList().Count();
                            for (long i = 0; i < count; i += 500)
                            {
                                List<downloadforecast> CallInsuranceDue1 = null;

                                CallInsuranceDue1 = (from ins in db.insuranceforecasteddatas
                                                     join cmp in db.campaigns on (long)ins.Campaign equals cmp.id
                                                     join loc in db.workshops on ins.location_id equals loc.id
                                                     join ren in db.renewaltypes on (long)ins.renewaltype equals ren.id
                                                     join insassignInter in db.insuranceassignedinteractions on ins.vehicle_id equals insassignInter.vehicle_vehicle_id
                                                     join wyz in db.wyzusers on insassignInter.wyzUser_id equals wyz.id
                                                     where (/*EntityFunctions.TruncateTime(*/ins.policyexpirydate >= /*EntityFunctions.TruncateTime(*/fromDate && /*EntityFunctions.TruncateTime(*/ins.policyexpirydate <= /*EntityFunctions.TruncateTime(*/toDate && campaignIDs.Contains(ins.Campaign ?? default(int))
                                                     && renewalIDs.Contains(ins.renewaltype ?? default(int)) && (wrkshopIDs.Contains(ins.location_id)) &&
                                                     vehIdFromInteraction.Contains(ins.vehicle_id))
                                                     select
                                                           new downloadforecast
                                                           {
                                                               id = ins.id,
                                                               campaign = cmp.campaignName,
                                                               customerName = ins.customerName,
                                                               chassisNo = ins.chassisNo,
                                                               vehicle_RegNo = ins.vehicleRegNo,
                                                               phoneNUmber = ins.phonenumber,
                                                               NextRenewalType = ren.renewalTypeName,
                                                               policyDueDate = ins.policyexpirydate,
                                                               last_insurancecompanyname = ins.insurancecompanyname,
                                                               location = loc.workshopName,
                                                               assigned = ins.IsAssigned,
                                                               assignedDate = insassignInter.uplodedCurrentDate,
                                                               creName = wyz.userName,
                                                               lastDispo = ins.lastdisposition,
                                                               vehicle_vehicle_id = ins.vehicle_id,
                                                               customerId = ins.customer_id,
                                                               last_insuirancecompanyname = ins.insurancecompanyname,
                                                               locationID = ins.location_id,
                                                               campaignID = cmp.id,
                                                               nextRenewalTypeID = ren.id,

                                                           }).OrderByDescending(m => m.id).Skip(Convert.ToInt32(i)).Take(500).ToList().ToList();

                                CallInsuranceDue.AddRange(CallInsuranceDue1);
                            }
                        }

                    }
                    DataTable tbl = new DataTable("Insurance Forecast Table");
                    tbl.Columns.Add("Customer Name", typeof(string));
                    if (Session["DealerCode"].ToString() != "SHIVAMHYUNDAI")
                    {
                        tbl.Columns.Add("Mobile No.", typeof(string));
                    }
                    tbl.Columns.Add("Vehicle Reg. No.", typeof(string));
                    tbl.Columns.Add("Chassis No.", typeof(string));
                    tbl.Columns.Add("Policy Due Date", typeof(DateTime));
                    tbl.Columns.Add("Next Renewal Type", typeof(string));
                    tbl.Columns.Add("Last insurance Company", typeof(string));
                    tbl.Columns.Add("workshopName", typeof(string));

                    for (var i = 0; i < CallInsuranceDue.Count; i++)
                    {
                        if (Session["DealerCode"].ToString() != "SHIVAMHYUNDAI")
                        {
                            tbl.Rows.Add(CallInsuranceDue[i].customerName, CallInsuranceDue[i].phoneNUmber, CallInsuranceDue[i].vehicle_RegNo, CallInsuranceDue[i].chassisNo
                                         , CallInsuranceDue[i].policyDueDate, CallInsuranceDue[i].NextRenewalType,
                                         CallInsuranceDue[i].last_insuirancecompanyname, CallInsuranceDue[i].location);
                        }
                        else
                        {
                            tbl.Rows.Add(CallInsuranceDue[i].customerName, CallInsuranceDue[i].vehicle_RegNo, CallInsuranceDue[i].chassisNo
                                         , CallInsuranceDue[i].policyDueDate, CallInsuranceDue[i].NextRenewalType,
                                         CallInsuranceDue[i].last_insuirancecompanyname, CallInsuranceDue[i].location);
                        }
                    }
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("InsuranceDue.xlsx", System.Text.Encoding.UTF8));
                    using (ExcelPackage pck = new ExcelPackage())
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Insurance_Due");
                        ws.Cells["A1"].LoadFromDataTable(tbl, true);
                        Session["DownloadExcel_FileManager"] = pck.GetAsByteArray();
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

                return Json(new { success = false, error = exception });
            }
            return Json(new { success = true });
        }
        #endregion

        //public ActionResult InsuranceDue()
        //{
        //    List<insuranceforecasteddata> assignedData = new List<insuranceforecasteddata>();
        //    assignedData = getTableData(values);
        //    try
        //    {
        //        DataTable tbl = new DataTable("Insurance Forecast Table");

        //        tbl.Columns.Add("customerName", typeof(string));
        //        tbl.Columns.Add("phonenumber", typeof(string));
        //        tbl.Columns.Add("vehicleRegNo", typeof(string));
        //        tbl.Columns.Add("chassisNo", typeof(string));
        //        tbl.Columns.Add("saledate", typeof(string));
        //        tbl.Columns.Add("policyexpirydate", typeof(DateTime?));
        //        tbl.Columns.Add("policy due month", typeof(DateTime?));
        //        tbl.Columns.Add("renewaltype", typeof(int?));
        //        tbl.Columns.Add("insurancecompanyname", typeof(string));
        //        tbl.Columns.Add("location_id", typeof(string));

        //        int i, j;
        //        for (i = 0; i < POParts.Count; i++)
        //        {
        //            var poc = POHead.FirstOrDefault(x => x.PONumber == POParts[i].PONumber && x.ModifiedStatus == false);
        //            tbl.Rows.Add(poc.PONumber, poc.PODate, poc.ProcurementTypeName, poc.VendorName, poc.VendorId, poc.QuotationReferenceNumber,
        //                poc.QuotationDated, poc.DeliverTerms, poc.PaymentTerms, poc.ShippingMode, poc.ShipTo, poc.RetentionPeriod, poc.RevNum,
        //                poc.TermsAndConditions, poc.CreatedBy, poc.CreatedOn, poc.ModifiedBy, poc.ModifiedOn, poc.POStatus,
        //                POParts[i].ProjectNumber, POParts[i].PartNumber, POParts[i].MftrPartNumber, POParts[i].Description, POParts[i].UOM,
        //                POParts[i].RequiredQuantity, POParts[i].UnitPrice, POParts[i].UnitPricePerQty, POParts[i].Currency, POParts[i].DeliverySchedule,
        //                POParts[i].SGST, POParts[i].CGST, POParts[i].IGST, POParts[i].UTGST, POParts[i].CreatedBy, POParts[i].CreatedOn,
        //                POParts[i].ModifiedBy, POParts[i].ModifiedOn);
        //        }

        //        Response.Clear();
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("PO-Report_" + DateTime.Now + ".xlsx", System.Text.Encoding.UTF8));
        //        using (ExcelPackage pck = new ExcelPackage())
        //        {
        //            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Purchase Order");
        //            ws.Cells["A1"].LoadFromDataTable(tbl, true);
        //            var ms = new System.IO.MemoryStream();
        //            pck.SaveAs(ms);
        //            ms.WriteTo(Response.OutputStream);
        //        }
        //        //ViewBag.cust = preRFQ.Select(c => new { CustomerName = c.CustomerName }).Distinct().ToList();
        //        //return View("rfqReport", preRFQ);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return View();
        //}
    }

    public class ForecastInsuranceFilter
    {
        public string workshopLocationObj { get; set; }
        public string cmpaignObj { get; set; }
        public string renewaltypeObj { get; set; }
        public string isAssignedObj { get; set; }


        public string fromPolicyExpDataObj { get; set; }


        public string toPolicyExpDataObj { get; set; }
        public string ModalCampaign { get; set; }
        public string creList { get; set; }
        public string searchPatternObj { get; set; }
        public string modelTypeObj { get; set; }
        public string locationObj { get; set; }
        public string duedateEdited { get; set; }
    }

    public class downloadforecast
    {
        public long? id { get; set; }
        public string campaign { get; set; }
        public string customerName { get; set; }
        public string chassisNo { get; set; }
        public string vehicle_RegNo { get; set; }
        public string phoneNUmber { get; set; }
        public string NextRenewalType { get; set; }
        public DateTime? policyDueDate { get; set; }
        public string last_insurancecompanyname { get; set; }
        public string location { get; set; }
        public string assigned { get; set; }
        public DateTime? assignedDate { get; set; }
        public string creName { get; set; }
        public string lastDispo { get; set; }
        public long? vehicle_vehicle_id { get; set; }
        public string customerId { get; set; }
        public string last_insuirancecompanyname { get; set; }
        public long? locationID { get; set; }
        public long campaignID { get; set; }
        public long? nextRenewalTypeID { get; set; }
        public string modelCategory { get; set; }
    }

}
