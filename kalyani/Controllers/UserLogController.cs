using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;
using AutoSherpa_project.Models;
using AutoSherpa_project.Models.Schedulers;
using System.IO;
using Newtonsoft.Json;
using System.Data.Entity.Migrations;
using Quartz;
using Quartz.Impl;
using AutoSherpa_project.Models.Scheduler;
using NLog;

namespace AutoSherpa_project.Controllers
{
    public class UserLogController : Controller
    {

        //public List<firebaseData> fireDataList = new List<firebaseData>();
        public static bool synchStop = false;
        public static List<long> vehicleIdList = new List<long>();
        public static string curUser { get; set; }
        [HttpGet]
        [ActionName("ViewLogs")]
        public ActionResult ViewLogs(int id)
        {
            try
            {
                if (id != 420)
                {
                    return RedirectToAction("LogOff", "Home");
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public ActionResult loadUserData(string from, string to, string filterOn)
        {
            List<userlogs> users = new List<userlogs>();
            int start = Convert.ToInt32(Request["start"]);
            int toIndex = Convert.ToInt32(Request["length"]), totalCount = 0;
            DateTime fromDate, toDate;
            string exception = "";
            try
            {
                using (var db = new AutoSherDBContext())
                {

                    totalCount = db.userlogs.Count();

                    if (toIndex < 0)
                    {
                        toIndex = 10;
                    }

                    if (toIndex > totalCount)
                    {
                        toIndex = totalCount;
                    }

                    if (filterOn != "All" && filterOn != "inspect")
                    {
                        fromDate = Convert.ToDateTime(from);
                        toDate = Convert.ToDateTime(to);

                        users = db.userlogs.Where(m => m.loginDateTime >= fromDate && m.loginDateTime <= toDate).OrderBy(m => m.id).Skip(start).Take(toIndex).ToList();
                    }
                    else if (filterOn == "inspect")
                    {
                        users = db.userlogs.Where(m => m.managername.Contains("Inspector")).OrderBy(m => m.id).Skip(start).Take(toIndex).ToList();
                    }
                    else
                    {
                        users = db.userlogs.OrderBy(m => m.id).Skip(start).Take(toIndex).ToList();
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
            }

            return Json(new { data = users, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount, exception = exception }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult renameAndroFiles()
        {
            List<string> fileRenames = new List<string>();
            try
            {
                //DirectoryInfo d = new DirectoryInfo(@"C:\DirectoryToAccess");
                //FileInfo[] infos = d.GetFiles();
                //foreach (FileInfo f in infos)
                //{
                //    File.Move(f.FullName, f.FullName.Replace("abc_",""));
                //}
                string DirectoryPaths = Server.MapPath(@"~/wyzAudioData/INDUS/");
                DirectoryInfo d = new DirectoryInfo(DirectoryPaths);
                foreach (var file in d.GetFiles())
                {
                    string newFile = (file.FullName.Replace('+', '_')).Replace('-', '_');
                    Directory.Move(file.FullName, newFile);
                    fileRenames.Add(newFile);
                    //i++;
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }

            return Json(new { success = true, filesRenamed = string.Join(",", fileRenames) });
        }

        public ActionResult deleteBody(int length)
        {
            List<string> fileRemoved = new List<string>();
            long rmFileSize = 0;
            try
            {
                string DealerCode = "";
                using (var db = new AutoSherDBContext())
                {
                    DealerCode = db.dealers.FirstOrDefault().dealerCode;
                }


                string DirectoryPaths = Server.MapPath(@"~/wyzAudioData/" + DealerCode + "/");
                //var filesList = Path.GetFileNameWithoutExtension(DirectoryPaths);

                //foreach(var file in filesList)
                //{
                //    //new File().Delete(file.)
                //}

                DirectoryInfo d = new DirectoryInfo(DirectoryPaths);
                FileInfo[] infos = d.GetFiles();

                foreach (FileInfo f in infos)
                {
                    if (f.Extension == "")
                    {
                        rmFileSize = rmFileSize + f.Length;
                        fileRemoved.Add(f.FullName);
                        System.IO.File.Delete(f.FullName);
                    }
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }

            return Json(new { success = true, filesremoved = string.Join(",", fileRemoved), rmCount = fileRemoved.Count(), rmFileSize = rmFileSize });
        }

        public ActionResult getAuthenticate(string pass)
        {
            if (pass == "mnj@123$")
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        public ActionResult getDetails(string id)
        {
            string regNo = string.Empty;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    long? vehicle_id = db.callinteractions.FirstOrDefault(m => m.uniqueIdGSM == id).vehicle_vehicle_id;

                    if (vehicle_id != 0)
                    {
                        regNo = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehicle_id).chassisNo;

                        var datas = db.callhistorycubes.Where(m => m.vehicle_id == vehicle_id).Select(m => new { callDate = m.callDate, filepath = m.filepath, creName = m.Cre_Name }).ToList();
                        string data = JsonConvert.SerializeObject(datas);
                        return Json(new { success = true, data = data, regNo = regNo });
                    }
                    else
                    {
                        return Json(new { success = true, error = "UniqueId Doesn't matched......." });
                    }

                }
            }
            catch (Exception ex)
            {
                return Json(new { success = true, error = ex.Message });
            }
        }

        #region old firebaseCRMManualkSync

        //public async Task<ActionResult> synchFireBaseData(string fireBaseLink)
        //{
        //    try
        //    {

        //        using (var db = new AutoSherDBContext())
        //        {

        //            string dealerName = db.dealers.FirstOrDefault().dealerCode;


        //            string baseURL = string.Empty;

        //            if (string.IsNullOrEmpty(fireBaseLink))
        //            {
        //                return Json(new { success = false, error = fireBaseLink + "Not found" });
        //            }

        //            baseURL = fireBaseLink;
        //            //if (HomeController.wyzCRM1.Contains(dealerName))
        //            //{
        //            //    baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_Wyzcrm1"];
        //            //}
        //            //else if (HomeController.wyzCRM.Contains(dealerName))
        //            //{
        //            //    baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_Wyzcrm"];
        //            //}
        //            //else if (HomeController.autosherpa1.Contains(dealerName))
        //            //{
        //            //    baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_autosherpa1"];
        //            //}

        //            var firebaseClient = new FirebaseClient(baseURL);


        //            var crenames = db.wyzusers.Where(m => m.role == "CRE").Select(m => m.userName).OrderBy(m => m).ToList();
        //            foreach (var cre in crenames)
        //            {
        //                string urlLink = dealerName;

        //                urlLink += "/CRE/" + cre + "/WebHistory/CallInfo/";

        //                var fireInst = await firebaseClient
        //                    .Child(urlLink).OnceAsync<firebaseData>();

        //                try
        //                {
        //                    if (fireInst.Count != 0 || fireInst.Count > 0)
        //                    {
        //                        //using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
        //                        //{
        //                        //try
        //                        //{
        //                        foreach (var data in fireInst)
        //                        {

        //                            if (synchStop == false)
        //                            {
        //                                try
        //                                {



        //                                    firebaseData fireData = new firebaseData();
        //                                    fireData = data.Object;
        //                                    string filePath = string.Empty;
        //                                    if (!string.IsNullOrEmpty(fireData.filePath))
        //                                    {
        //                                        filePath = (fireData.filePath.Replace('+', '_')).Replace('-', '_');
        //                                        filePath = "/wyzAudioData/" + filePath;
        //                                    }

        //                                    if (fireData.uniqueidForCallSync == 0 && string.IsNullOrEmpty(fireData.callDate) && string.IsNullOrEmpty(filePath) && string.IsNullOrEmpty(fireData.callTime))
        //                                    {
        //                                        //await firebaseClient
        //                                        //     .Child(urlLink + data.Key).DeleteAsync();
        //                                    }
        //                                    else
        //                                    {
        //                                        callinteraction callInter = new callinteraction();

        //                                        if (fireData.uniqueidForCallSync != 0)
        //                                        {
        //                                            callInter = db.callinteractions.FirstOrDefault(m => m.uniqueidForCallSync == fireData.uniqueidForCallSync);
        //                                        }
        //                                        string fireBaseKey = data.Key;
        //                                        if (callInter != null && fireData.uniqueidForCallSync != 0)
        //                                        {
        //                                            //callinteraction callInter = db.callinteractions.Include("customer").Include("customer.phones").FirstOrDefault(m => m.uniqueidForCallSync == fireData.uniqueidForCallSync);
        //                                            callInter.callDate = fireData.callDate.Replace('/', '-');
        //                                            callInter.callDuration = fireData.callDuration;
        //                                            callInter.callTime = fireData.callTime;
        //                                            callInter.callType = fireData.callType;
        //                                            callInter.ringTime = fireData.ringTime;
        //                                            callInter.mediaFileLob = fireData.mediaFileLob;
        //                                            callInter.chasserCall = false;
        //                                            callInter.latitude = fireData.latitude;
        //                                            callInter.longitude = fireData.logitude;
        //                                            callInter.filePath = filePath;
        //                                            callInter.fileSize = fireData.fileSize;
        //                                            callInter.firebaseKey = data.Key;

        //                                            db.callinteractions.AddOrUpdate(callInter);
        //                                            db.SaveChanges();
        //                                            long filesize = 0;
        //                                            string size = string.Empty;
        //                                            if (!string.IsNullOrEmpty(fireData.fileSize))
        //                                            {
        //                                                fireData.fileSize = fireData.fileSize.Replace(".00", "");
        //                                                for (int i = 0; i < fireData.fileSize.Length; i++)
        //                                                {
        //                                                    if (Char.IsDigit(fireData.fileSize[i]))
        //                                                        size += fireData.fileSize[i];
        //                                                }

        //                                                //filesize = long.Parse(fireData.fileSize.Replace("[^0-9]", ""));
        //                                                filesize = long.Parse(size);
        //                                            }

        //                                            callhistorycube callHistory = db.callhistorycubes.FirstOrDefault(m => m.Call_interaction_id == callInter.id);
        //                                            if (callHistory != null)
        //                                            {
        //                                                //callhistorycube callHistory = db.callhistorycubes.FirstOrDefault(m => m.Call_interaction_id == callInter.id);
        //                                                callHistory.callDuration = fireData.callDuration;
        //                                                callHistory.ringtime = fireData.ringTime;
        //                                                callHistory.isCallDurationUpdated = true;
        //                                                callHistory.updatedDate = DateTime.Now.Subtract(new TimeSpan(0, 20, 0));
        //                                                callHistory.filepath = filePath;
        //                                                callHistory.callType = fireData.callType;
        //                                                callHistory.fileSize = filesize;
        //                                                callHistory.dailedNumber = callInter.customer.phones.FirstOrDefault(m => m.isPreferredPhone == true).phoneNumber;
        //                                                db.callhistorycubes.AddOrUpdate(callHistory);
        //                                                db.SaveChanges();
        //                                            }
        //                                            else
        //                                            {
        //                                                insurancecallhistorycube callHistoryIns = db.insurancecallhistorycubes.FirstOrDefault(m => m.cicallinteraction_id == callInter.id);

        //                                                if (callHistoryIns != null)
        //                                                {
        //                                                    callHistoryIns.callDuration = fireData.callDuration;
        //                                                    callHistoryIns.ringTime = fireData.ringTime;
        //                                                    callHistoryIns.isCallDurationUpdated = true;
        //                                                    callHistoryIns.updatedDate = DateTime.Now.Subtract(new TimeSpan(0, 20, 0));
        //                                                    callHistoryIns.filepath = filePath;
        //                                                    callHistoryIns.callType = fireData.callType;
        //                                                    callHistoryIns.fileSize = filesize;
        //                                                    callHistoryIns.dailedNumber = callInter.customer.phones.FirstOrDefault(m => m.isPreferredPhone == true).phoneNumber;
        //                                                    db.insurancecallhistorycubes.AddOrUpdate(callHistoryIns);
        //                                                    db.SaveChanges();
        //                                                }
        //                                                else
        //                                                {
        //                                                    //psfcallhistorycube callHistoryPSF = db.psfcallhistorycubes.FirstOrDefault(m => m.cicallinteraction_id == callInter.id);

        //                                                    //if (callHistoryPSF != null)
        //                                                    //{
        //                                                    //    callHistoryPSF.callDuration = fireData.callDuration;
        //                                                    //    callHistoryPSF.ringTime = fireData.ringTime;
        //                                                    //    callHistoryPSF.updatedDate = DateTime.Now.Subtract(new TimeSpan(0, 20, 0));
        //                                                    //    callHistoryPSF.filepath = filePath;
        //                                                    //    callHistoryPSF.callType = fireData.callType;
        //                                                    //    callHistoryPSF.fileSize = filesize;
        //                                                    //    callHistoryPSF.dailedNumber = callInter.customer.phones.FirstOrDefault(m => m.isPreferredPhone == true).phoneNumber;
        //                                                    //    db.psfcallhistorycubes.AddOrUpdate(callHistoryPSF);
        //                                                    //    db.SaveChanges();
        //                                                    //}
        //                                                }
        //                                            }

        //                                            callsyncdata callsyncdata = new callsyncdata();
        //                                            string callMadeDateTime = fireData.callDate + " " + fireData.callTime;

        //                                            callsyncdata.uniqueidForCallSync = fireData.uniqueidForCallSync.ToString();
        //                                            callsyncdata.callDuration = fireData.callDuration;
        //                                            callsyncdata.callDurationUpdated = true;
        //                                            callsyncdata.callinteraction_id = callInter.id;
        //                                            callsyncdata.callMadeDateAndTime = Convert.ToDateTime(callMadeDateTime);
        //                                            callsyncdata.callType = fireData.callType;
        //                                            //callsyncdata.csidmap=
        //                                            callsyncdata.dailedNumber = fireData.customerPhone;
        //                                            callsyncdata.filepath = filePath;
        //                                            callsyncdata.isComplaintCall = false;
        //                                            callsyncdata.moduletype = 1;
        //                                            callsyncdata.ringTime = fireData.ringTime;
        //                                            callsyncdata.updatedDate = DateTime.Now;
        //                                            callsyncdata.wyzuser_id = callInter.wyzUser_id;
        //                                            callsyncdata.isgsmsdata = false;
        //                                            db.callSyncDatas.Add(callsyncdata);
        //                                            db.SaveChanges();

        //                                            await firebaseClient
        //                                           .Child(urlLink + data.Key).DeleteAsync();
        //                                        }
        //                                        else
        //                                        {
        //                                            callinteraction callInterNew = new callinteraction();

        //                                            //if(db.phones.Any(m=>m.phoneNumber==fireData.customerPhone))
        //                                            //{
        //                                            //    callInter.
        //                                            //}
        //                                            callInterNew.dailedNoIs = fireData.customerPhone;
        //                                            callInterNew.callDate = fireData.callDate.Replace('/', '-');
        //                                            callInterNew.callDuration = fireData.callDuration;
        //                                            callInterNew.callTime = fireData.callTime;
        //                                            callInterNew.callType = fireData.callType;
        //                                            callInterNew.ringTime = fireData.ringTime;
        //                                            callInterNew.mediaFileLob = fireData.mediaFileLob;
        //                                            callInterNew.latitude = fireData.latitude;
        //                                            callInterNew.longitude = fireData.logitude;
        //                                            callInterNew.filePath = filePath;
        //                                            callInterNew.fileSize = fireData.fileSize;
        //                                            callInterNew.firebaseKey = data.Key;
        //                                            callInterNew.chasserCall = false;
        //                                            callInterNew.dealerCode = fireData.dealerCode;
        //                                            callInterNew.wyzUser_id = db.wyzusers.FirstOrDefault(m => m.userName == cre).id;
        //                                            callInterNew.isCallinitaited = fireData.callType;

        //                                            db.callinteractions.Add(callInterNew);
        //                                            db.SaveChanges();

        //                                            //callsyncdata callsyncdata = new callsyncdata();

        //                                            //string callMadeDateTime = fireData.callDate + " " + fireData.callTime;

        //                                            //callsyncdata.uniqueidForCallSync = fireData.uniqueidForCallSync.ToString();
        //                                            //callsyncdata.callDuration = fireData.callDuration;
        //                                            //callsyncdata.callDurationUpdated = true;
        //                                            //callsyncdata.callinteraction_id = callInter.id;
        //                                            //callsyncdata.callMadeDateAndTime = Convert.ToDateTime(callMadeDateTime);
        //                                            //callsyncdata.callType = fireData.callType;
        //                                            ////callsyncdata.csidmap=
        //                                            //callsyncdata.dailedNumber = fireData.customerPhone;
        //                                            //callsyncdata.filepath = filePath;
        //                                            //callsyncdata.isComplaintCall = false;
        //                                            //callsyncdata.moduletype = 1;
        //                                            //callsyncdata.ringTime = fireData.ringTime;
        //                                            //callsyncdata.updatedDate = DateTime.Now;
        //                                            //callsyncdata.wyzuser_id = callInter.wyzUser_id;
        //                                            //db.callSyncDatas.Add(callsyncdata);
        //                                            //db.SaveChanges();
        //                                            await firebaseClient
        //                                            .Child(urlLink + data.Key).DeleteAsync();
        //                                        }
        //                                        logger.Info("Synchronized Data: \n", JsonConvert.SerializeObject(fireData));
        //                                        //fireDataList.Add(fireData);
        //                                        synchDataCount = synchDataCount + 1;
        //                                        curUser = cre + "_____FireBase Key: " + fireBaseKey;
        //                                    }
        //                                }
        //                                catch (Exception ex)
        //                                {
        //                                    if (ex.Message.Contains("inner exception"))
        //                                    {
        //                                        if (ex.InnerException.Message.Contains("inner exception"))
        //                                        {
        //                                            logger.Error("Error" + ex.InnerException.InnerException.Message);
        //                                        }
        //                                        else
        //                                        {
        //                                            logger.Error("Error" + ex.InnerException.Message);
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        logger.Error("Error" + ex.Message);
        //                                    }
        //                                    logger.Info("\n Received Firebase Data Cause Exception: \n" + JsonConvert.SerializeObject(data.Object));
        //                                }
        //                            }
        //                            else
        //                            {
        //                                return Json(new { success = "Stoped" });
        //                            }


        //                            //dbTransaction.Commit();
        //                        }
        //                        //}
        //                        //catch (Exception ex)
        //                        //{
        //                        //    dbTransaction.Rollback();
        //                        //}
        //                        //dbTransaction.Commit();
        //                        //}


        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    if (ex.Message.Contains("inner exception"))
        //                    {
        //                        if (ex.InnerException.Message.Contains("inner exception"))
        //                        {
        //                            logger.Error("Error" + ex.InnerException.InnerException.Message);
        //                        }
        //                        else
        //                        {
        //                            logger.Error("Error" + ex.InnerException.Message);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        logger.Error("Error" + ex.Message);
        //                    }
        //                }

        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.Contains("inner exception"))
        //        {
        //            if (ex.InnerException.Message.Contains("inner exception"))
        //            {
        //                logger.Error("Error" + ex.InnerException.InnerException.Message);
        //            }
        //            else
        //            {
        //                logger.Error("Error" + ex.InnerException.Message);
        //            }
        //        }
        //        else
        //        {
        //            logger.Error("Error" + ex.Message);
        //        }
        //    }

        //    return Json(new { success = true, DataImported = "" });
        //}

        #endregion



        //public ActionResult getSynchDetails(string syndhStatus)
        //{
        //    try
        //    {
        //        if(syndhStatus== "STOP")
        //        {
        //            synchStop = true;
        //            return Json(new { count = synchDataCount, data = "Synch Stoped" });
        //        }
        //        else if(syndhStatus=="START")
        //        {
        //            synchStop = false;
        //            return Json(new { count = synchDataCount, data = "Synch Stoped" });
        //        }
        //        else
        //        {
        //            return Json(new { count = synchDataCount.ToString()+"____Current--"+ curUser, data = "" });
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false });
        //    }
        //}

        public ActionResult captureInspectors(string sessionUser, string inspectedPage)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    userlogs inspUser = new userlogs();

                    inspUser.username = sessionUser;
                    inspUser.managername = "Inspector - " + inspectedPage;
                    inspUser.loginDateTime = DateTime.Now;
                    inspUser.sessionid = "Inspected on the Page:" + inspectedPage;
                    inspUser.hostaddress = Request.UserHostAddress;
                    db.userlogs.Add(inspUser);
                    db.SaveChanges();
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
                return Json(new { success = exception });
            }
            return Json(new { success = true });
        }


        /// <summary>
        /// To manage quartz scheduler running...
        /// </summary>
        /// <param name="id">for user restriction</param>
        /// <returns>List of scheduler for accessing user..</returns>
        [ActionName("ManageScheduler"), HttpGet]
        public ActionResult manageScheduler(int id)
        {
            List<SelectListItem> listItem = new List<SelectListItem>();

            if (id == 03) // all scheduler
            {
                listItem.Add(new SelectListItem { Value = "clickToCall", Text = "ClickToCall Scheduler" });
                listItem.Add(new SelectListItem { Value = "adminChangeAssign", Text = "Admin ChangeAssign Scheduler" });
                listItem.Add(new SelectListItem { Value = "fePushing", Text = "FE DataPushing Scheduler" });
                listItem.Add(new SelectListItem { Value = "fePulling", Text = "FE DataPulling(History) Scheduler" });
            }
            else if (id == 0111) // except
            {
                listItem.Add(new SelectListItem { Value = "adminChangeAssign", Text = "Admin ChangeAssign Scheduler" });
                listItem.Add(new SelectListItem { Value = "fePushing", Text = "FE DataPushing Scheduler" });
                listItem.Add(new SelectListItem { Value = "fePulling", Text = "FE DataPulling(History) Scheduler" });
            }

            ViewBag.schedulerList = listItem;

            return View();
        }

        public ActionResult doSchedulerJob(string opr, string scheduler, int min=3)
        {

            //var triggerBykeyGroup = FirebaseJob.scheduler.GetTrigger(new Quartz.TriggerKey("clickToCallTrigger", "clickToCallGroup"));
            //TimeSpan newmin = new TimeSpan(0, min, 0);
            //((Quartz.Impl.Triggers.SimpleTriggerImpl)triggerBykeyGroup.Result).RepeatInterval = newmin;
            //TaskStatus task = triggerBykeyGroup.Status;
            //var iTrigger = triggerBykeyGroup.Result;

            //TriggerKey triggerkey = iTrigger.Key;

            //JobKey job = new JobKey(triggerkey.Name, triggerkey.Group);
            //var scheduleJob = FirebaseJob.scheduler.GetJobDetail(job);
            //FirebaseJob.scheduler.Start();
            //FirebaseJob.scheduler.RescheduleJob(triggerkey, iTrigger);
            try
            {
                if (scheduler == "clickToCall")
                {

                    if (opr == "start")
                    {
                        FirebaseJob.scheduler.Start();
                        return Json(new { success = true, opr });
                    }
                    else if (opr == "stop")
                    {
                        FirebaseJob.scheduler.Shutdown();
                        return Json(new { success = true, opr });
                    }
                    else if(opr =="status")
                    {
                        var triggerBykeyGroup = FirebaseJob.scheduler.GetTrigger(new Quartz.TriggerKey("clickToCallTrigger", "clickToCallGroup"));
                        TaskStatus task = triggerBykeyGroup.Status;
                        JobKey jobfg = triggerBykeyGroup.Result.JobKey;
                        var jobStatus = FirebaseJob.scheduler.GetJobDetail(jobfg);
                        var Firebase = FirebaseJob.scheduler.GetCurrentlyExecutingJobs();
                        //FirebaseJob.scheduler.Shutdown();
                        return Json(new { success = true, status = task,opr });
                    }
                    else if(opr== "reschedule")
                    {
                        var triggerBykeyGroup = FirebaseJob.scheduler.GetTrigger(new Quartz.TriggerKey("trigger1", "group1"));

                        TimeSpan newmin = new TimeSpan(0, min, 0);

                        ((Quartz.Impl.Triggers.SimpleTriggerImpl)triggerBykeyGroup.Result).RepeatInterval = newmin;
                        var iTrigger = triggerBykeyGroup.Result;
                        TriggerKey triggerkey = iTrigger.Key;
                        FirebaseJob.scheduler.Start();
                        FirebaseJob.scheduler.RescheduleJob(triggerkey, iTrigger);

                        return Json(new { success = true, opr });
                    }
                }
                else if (scheduler == "adminChangeAssign")
                {
                    if (opr == "start")
                    {
                        AdminChangeAssigScheduler.adminChangeAssignscheduler.Start();
                        return Json(new { success = true, opr });
                    }
                    else if (opr == "stop")
                    {
                        AdminChangeAssigScheduler.adminChangeAssignscheduler.Shutdown();
                        return Json(new { success = true, opr });
                    }
                    else if (opr == "status")
                    {
                        var triggerBykeyGroup = AdminChangeAssigScheduler.adminChangeAssignscheduler.GetTrigger(new Quartz.TriggerKey("adminChangeAssignTrigger", "adminChangeAssignGroup"));
                        TaskStatus task = triggerBykeyGroup.Status;

                        return Json(new { success = true, status = task, opr });
                    }
                    else if (opr == "reschedule")
                    {
                        var triggerBykeyGroup = AdminChangeAssigScheduler.adminChangeAssignscheduler.GetTrigger(new Quartz.TriggerKey("adminChangeAssignTrigger", "adminChangeAssignGroup"));

                        TimeSpan newmin = new TimeSpan(0, min, 0);

                        ((Quartz.Impl.Triggers.SimpleTriggerImpl)triggerBykeyGroup.Result).RepeatInterval = newmin;
                        var iTrigger = triggerBykeyGroup.Result;
                        TriggerKey triggerkey = iTrigger.Key;
                        AdminChangeAssigScheduler.adminChangeAssignscheduler.Start();
                        AdminChangeAssigScheduler.adminChangeAssignscheduler.RescheduleJob(triggerkey, iTrigger);

                        return Json(new { success = true, opr });
                    }
                }
                else if (scheduler == "fePushing")
                {
                    if (opr == "start")
                    {
                        if (min == 09)
                        {
                            var triggerBykeyGroup = FirebaseJob.scheduler.GetTrigger(new Quartz.TriggerKey("FEPushingTrigger", "FEPushingGroup"));

                            AppScheduler.appSchedulers.Start();
                            var iTrigger = triggerBykeyGroup.Result;
                            AppScheduler.appSchedulers.ScheduleJob(iTrigger);

                            return Json(new { success = true, opr });
                        }
                        else
                        {
                            AppScheduler.appSchedulers.Start();
                            return Json(new { success = true, opr });
                        }
                    }
                    else if (opr == "stop")
                    {
                        AppScheduler.appSchedulers.Shutdown();
                        return Json(new { success = true, opr });
                    }
                    else if (opr == "status")
                    {
                        var triggerBykeyGroup = AppScheduler.appSchedulers.GetTrigger(new Quartz.TriggerKey("FEPushingTrigger", "FEPushingGroup"));
                        TaskStatus task = triggerBykeyGroup.Status;
                        string taskStatus = "";

                        if(triggerBykeyGroup.Result!=null)
                        {
                            taskStatus = task.ToString();
                        }
                        else
                        {
                            taskStatus = "Scheduler Not Configured";
                        }

                        //JobKey jobfg = triggerBykeyGroup.Result.JobKey;
                        //var jobStatus = FirebaseJob.scheduler.GetJobDetail(jobfg);
                        var Firebase = FirebaseJob.scheduler.GetCurrentlyExecutingJobs();

                        return Json(new { success = true, status = taskStatus, opr });
                    }
                    else if (opr == "reschedule")
                    {
                        var triggerBykeyGroup = AppScheduler.appSchedulers.GetTrigger(new Quartz.TriggerKey("FEPushingTrigger", "FEPushingTrigger"));

                        TimeSpan newmin = new TimeSpan(0, min, 0);

                        ((Quartz.Impl.Triggers.SimpleTriggerImpl)triggerBykeyGroup.Result).RepeatInterval = newmin;
                        var iTrigger = triggerBykeyGroup.Result;
                        TriggerKey triggerkey = iTrigger.Key;
                        AppScheduler.appSchedulers.Start();
                        AppScheduler.appSchedulers.RescheduleJob(triggerkey, iTrigger);

                        return Json(new { success = true, opr });
                    }
                }
                else if (scheduler == "fePulling")
                {
                    if (opr == "start")
                    {
                        if(min==999)
                        {
                            var triggerBykeyGroup = FirebaseJob.scheduler.GetTrigger(new Quartz.TriggerKey("clickToCallTrigger", "clickToCallGroup"));

                            AppScheduler.appSchedulers.Start();
                            var iTrigger = triggerBykeyGroup.Result;
                            AppScheduler.appSchedulers.ScheduleJob(iTrigger);

                            return Json(new { success = true, opr });
                        }
                        else
                        {
                            AppScheduler.appSchedulers.Start();
                            return Json(new { success = true, opr });
                        }
                    }
                    else if (opr == "stop")
                    {
                        AppScheduler.appSchedulers.Shutdown();
                        return Json(new { success = true, opr });
                    }
                    else if (opr == "status")
                    {
                        var triggerBykeyGroup = AppScheduler.appSchedulers.GetTrigger(new Quartz.TriggerKey("FEPullingTrigger", "FEPullingGroup"));
                        TaskStatus task = triggerBykeyGroup.Status;
                        string taskStatus = "";
                        if (triggerBykeyGroup.Result != null)
                        {
                            taskStatus = task.ToString();
                        }
                        else
                        {
                            taskStatus = "Scheduler Not Configured";
                        }
                        return Json(new { success = true, status = taskStatus, opr });
                    }
                    else if (opr == "reschedule")
                    {
                        var triggerBykeyGroup = AppScheduler.appSchedulers.GetTrigger(new Quartz.TriggerKey("FEPullingTrigger", "FEPullingGroup"));

                        TimeSpan newmin = new TimeSpan(0, min, 0);

                        ((Quartz.Impl.Triggers.SimpleTriggerImpl)triggerBykeyGroup.Result).RepeatInterval = newmin;
                        var iTrigger = triggerBykeyGroup.Result;
                        TriggerKey triggerkey = iTrigger.Key;
                        AppScheduler.appSchedulers.Start();
                        AppScheduler.appSchedulers.RescheduleJob(triggerkey, iTrigger);

                        return Json(new { success = true, opr });
                    }
                }
                else
                {
                    return Json(new { success = false, exception = "Incorrect Scheduler name" });
                }

            }
            catch (Exception ex)
            {
                string exception = "";
                if (ex.InnerException != null)
                {
                    exception = ex.InnerException.Message;
                }
                else
                {
                    exception = ex.Message;
                }

                return Json(new { success = false, exception });
            }

            return Json(new { success = true, opr = opr }); ;
        }

        public ActionResult runJob(string jobName)
        {
            try
            {
                IScheduler autosynchScheduler = StdSchedulerFactory.GetDefaultScheduler().Result;

                if(jobName=="FE-Push")
                {
                    
                        autosynchScheduler.Start();
                    IJobDetail jobDetail = JobBuilder.Create<FieldSchedulerJob>().Build();
                    ITrigger trigger = TriggerBuilder.Create().WithIdentity("ManulFEPushingTrigger", "ManulFEPushingGroup")
                        .StartNow().WithSimpleSchedule().Build();

                    autosynchScheduler.ScheduleJob(jobDetail, trigger);
                    return Json(new { success = true });
                }
                else if(jobName=="FE-Pull")
                {
                    autosynchScheduler.Start();
                    IJobDetail jobDetail = JobBuilder.Create<FESchedulerPulling>().Build();
                    ITrigger trigger = TriggerBuilder.Create().WithIdentity("ManulFEPullTrigger", "ManulFEPullGroup")
                        .StartNow().WithSimpleSchedule().Build();

                    autosynchScheduler.ScheduleJob(jobDetail, trigger);
                    return Json(new { success = true });
                }
                else if(jobName== "CallSynch")
                {
                    //    IJobDetail jobDetail = JobBuilder.Create<FirebaseScheduler>().Build();
                    //    ITrigger trigger = TriggerBuilder.Create().WithIdentity("trigger1", "group1")
                    //        .StartNow().WithSimpleSchedule(s => s.WithIntervalInMinutes(min).RepeatForever()).Build();

                    autosynchScheduler.Start();
                    IJobDetail jobDetail = JobBuilder.Create<FirebaseScheduler>().Build();
                    //ITrigger trigger = TriggerBuilder.Create().WithIdentity("FireCallsychtrigger1", "FireCallsychgroup1")
                    //    .StartNow().WithSimpleSchedule().Build();

                    ITrigger trigger = TriggerBuilder.Create().WithIdentity("CallSynchtrigger1", "CallSynchgroup1")
                        .StartNow().WithSimpleSchedule().Build();



                    autosynchScheduler.ScheduleJob(jobDetail, trigger);
                    return Json(new { success = true });
                }
                else if(jobName== "SMR-Push")
                {
                    //    IJobDetail jobDetail = JobBuilder.Create<FirebaseScheduler>().Build();
                    //    ITrigger trigger = TriggerBuilder.Create().WithIdentity("trigger1", "group1")
                    //        .StartNow().WithSimpleSchedule(s => s.WithIntervalInMinutes(min).RepeatForever()).Build();

                    autosynchScheduler.Start();
                    IJobDetail jobDetail = JobBuilder.Create<servicePushJob>().Build();
                    ITrigger trigger = TriggerBuilder.Create().WithIdentity("CallSynchtrigger1", "CallSynchgroup1")
                        .StartNow().WithSimpleSchedule().Build();

                    autosynchScheduler.ScheduleJob(jobDetail, trigger);
                    return Json(new { success = true });
                }
                else if(jobName== "AutoSMSCron")
                {
                    //    IJobDetail jobDetail = JobBuilder.Create<FirebaseScheduler>().Build();
                    //    ITrigger trigger = TriggerBuilder.Create().WithIdentity("trigger1", "group1")
                    //        .StartNow().WithSimpleSchedule(s => s.WithIntervalInMinutes(min).RepeatForever()).Build();

                    autosynchScheduler.Start();
                    IJobDetail jobDetail = JobBuilder.Create<AutoSMSCronJob>().Build();
                    ITrigger trigger = TriggerBuilder.Create().WithIdentity("trigger1AutoSMSCron", "group1AutoSMSCron")
                        .StartNow().WithSimpleSchedule().Build();

                    autosynchScheduler.ScheduleJob(jobDetail, trigger);
                    return Json(new { success = true });
                }
                else if (jobName == "Events")
                {
                    //    IJobDetail jobDetail = JobBuilder.Create<FirebaseScheduler>().Build();
                    //    ITrigger trigger = TriggerBuilder.Create().WithIdentity("trigger1", "group1")
                    //        .StartNow().WithSimpleSchedule(s => s.WithIntervalInMinutes(min).RepeatForever()).Build();

                    autosynchScheduler.Start();
                    IJobDetail jobDetail = JobBuilder.Create<EventsJob>().Build();
                    ITrigger trigger = TriggerBuilder.Create().WithIdentity("triggerEvent", "triggerGroup")
                        .StartNow().WithSimpleSchedule().Build();

                    autosynchScheduler.ScheduleJob(jobDetail, trigger);
                    return Json(new { success = true });
                }
                else if (jobName == "Driver-Push")
                {

                    autosynchScheduler.Start();
                    IJobDetail jobDetail = JobBuilder.Create<DriverPushingJob>().Build();
                    ITrigger trigger = TriggerBuilder.Create().WithIdentity("ManulDriverPushingTrigger", "ManulDriverPushingGroup")
                        .StartNow().WithSimpleSchedule().Build();

                    autosynchScheduler.ScheduleJob(jobDetail, trigger);
                    return Json(new { success = true });
                }

                return Json(new { success = false,exception="Could not able to start the job..." });
            }
            catch(Exception ex)
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
                return Json(new { success = false, exception });
            }
        }

        public async Task<ActionResult> deleteDuplicate(string fireBaseLink)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {
                using (var db = new AutoSherDBContext())
                {

                    string dealerName = db.dealers.FirstOrDefault().dealerCode;


                    string baseURL = string.Empty;

                    if (string.IsNullOrEmpty(fireBaseLink))
                    {
                        return Json(new { success = false, exception = fireBaseLink + "Not found" });
                    }

                    baseURL = fireBaseLink;

                    var firebaseClient = new FirebaseClient(baseURL);


                    var crenames = db.wyzusers.Where(m => m.role == "InsuranceAgent").Select(m => m.userName).OrderBy(m => m).ToList();
                    foreach (var cre in crenames)
                    {
                        string urlLink = dealerName;

                        urlLink += "/InsuranceAgent/" + cre + "/ScheduledCalls/" + "CallInfo/";

                       
                        try
                        {
                            var fireInst = await firebaseClient
                           .Child(urlLink).OnceAsync<FEFirebaseData>();

                            if (fireInst.Count != 0 || fireInst.Count > 0)
                            {
                                //using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
                                //{
                                //try
                                //{
                                foreach (var data in fireInst)
                                {

                                    if (synchStop == false)
                                    {
                                        try
                                        {
                                            FEFirebaseData fireData = new FEFirebaseData();
                                            fireData = data.Object;
                                            string firebaseKey = data.Key;

                                            long pr_vehicleId = 0;

                                            if (!string.IsNullOrEmpty(fireData.vehicleId))
                                            {
                                                pr_vehicleId = long.Parse(fireData.vehicleId);
                                            }
                                            

                                            if (!vehicleIdList.Contains(pr_vehicleId))
                                            {
                                                if (db.Fieldexecutivefirebaseupdations.Count(m => m.firebasekey == firebaseKey) > 0)
                                                {
                                                    fieldexecutivefirebaseupdation firebase = db.Fieldexecutivefirebaseupdations.FirstOrDefault(m => m.firebasekey == firebaseKey);

                                                    if (!string.IsNullOrEmpty(fireData.vehicleId))
                                                    {
                                                        firebase.vehicle_id = long.Parse(fireData.vehicleId);
                                                    }
                                                    else
                                                    {
                                                        firebase.vehicle_id = 0420;
                                                    }
                                                    firebase.updatedatetime = DateTime.Now;

                                                    db.Fieldexecutivefirebaseupdations.AddOrUpdate(firebase);
                                                    db.SaveChanges();

                                                    vehicleIdList.Add(pr_vehicleId);
                                                }
                                                else
                                                {
                                                    await firebaseClient
                                                        .Child(urlLink + firebaseKey).DeleteAsync();
                                                    vehicleIdList.Add(pr_vehicleId);
                                                }
                                            }
                                            else
                                            {
                                                await firebaseClient
                                                   .Child(urlLink + firebaseKey).DeleteAsync();
                                            }
                                            
                                            curUser += "_*"+firebaseKey + " __" + cre;
                                        }
                                        catch (Exception ex)
                                        {
                                            if (ex.Message.Contains("inner exception"))
                                            {
                                                if (ex.InnerException.Message.Contains("inner exception"))
                                                {
                                                    logger.Error("DataLoop FE-Duplicate Error" + ex.InnerException.InnerException.Message);
                                                }
                                                else
                                                {
                                                    logger.Error("DataLoop FE-Duplicate Error" + ex.InnerException.Message);
                                                }
                                            }
                                            else
                                            {
                                                logger.Error("DataLoop FE-Duplicate Error" + ex.Message);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        return Json(new { success = "Stoped" });
                                    }


                                    //dbTransaction.Commit();
                                }
                                //}
                                //catch (Exception ex)
                                //{
                                //    dbTransaction.Rollback();
                                //}
                                //dbTransaction.Commit();
                                //}


                            }
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("inner exception"))
                            {
                                if (ex.InnerException.Message.Contains("inner exception"))
                                {
                                    logger.Error("FE Manual Schedule Duplicate Delete Error" + ex.InnerException.InnerException.Message);
                                }
                                else
                                {
                                    logger.Error("FE Manual Schedule Duplicate Delete Error" + ex.InnerException.Message);
                                }
                            }
                            else
                            {
                                logger.Error(" FE Manual Schedule Duplicate Delete Error" + ex.Message);
                            }
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                return Json(new { success = false, exception=ex.Message });
            }

            return Json(new { success = true });
        }

        public ActionResult getSynchDetails(string syndhStatus)
        {
            try
            {
                if (syndhStatus == "STOP")
                {
                    synchStop = true;
                    return Json(new { success = true,  data = "Synch Stoped" });
                }
                else if (syndhStatus == "START")
                {
                    synchStop = false;
                    return Json(new { success = true, data = "Synch Stoped" });
                }
                else
                {
                    return Json(new { success = true, data = "____Current--" + curUser });
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false,msg = ex.Message });
            }
        }
    }
}