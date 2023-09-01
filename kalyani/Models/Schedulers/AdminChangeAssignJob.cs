using NLog;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AutoSherpa_project.Models.Schedulers
{
    [DisallowConcurrentExecution]
    public class AdminChangeAssignJob : schedulerCommonFunction, IJob
    {
        //Write Scheduler code here
        public async Task Execute(IJobExecutionContext context)
        {

            string siteRoot = HttpRuntime.AppDomainAppVirtualPath;
            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {

                logger.Info("\n Admin Change Assignment : " + DateTime.Now);
                if (siteRoot != "/")
                {
                    using (AutoSherDBContext db = new AutoSherDBContext())
                    {

                        schedulers schedulerDetails = db.schedulers.FirstOrDefault(m => m.scheduler_name == "admin-changeassignment");

                        if (schedulerDetails != null && schedulerDetails.isActive == true/* && schedulerDetails.IsItRunning == false*/)
                        {
                            startScheduler("admin-changeassignment");
                            int maxLength = 500;

                            if (schedulerDetails.datalenght != 0)
                            {
                                maxLength = schedulerDetails.datalenght;
                            }

                            List<change_assignment_records> change_Assign = new List<change_assignment_records>();
                            List<changeAssignmentPending> cap = db.ChangeAssignmentPendings.Where(x => x.updatedStatus == false).OrderBy(m => m.id).Skip(0).Take(maxLength).ToList();
                            foreach (var item in cap)
                            {
                                try
                                {
                                    if (item.newWyzuserId != 0)
                                    {
                                        if (item.moduleType == 1)
                                        {
                                            assignedinteraction assignInter = db.assignedinteractions.FirstOrDefault(m => m.id == item.assignedInteractionId);
                                            if (assignInter != null)
                                            {
                                                change_assignment_records changeAssignRecord = new change_assignment_records();
                                                changeAssignRecord.assignedinteraction_id = assignInter.id;
                                                changeAssignRecord.campaign_id = assignInter.campaign_id ?? default(long);
                                                changeAssignRecord.last_wyzuserId = assignInter.wyzUser_id ?? default(long);

                                                long wyzuserId = item.newWyzuserId;
                                                changeAssignRecord.new_wyzuserId = wyzuserId;
                                                long wyzId = wyzuserId;
                                                assignInter.wyzUser_id = wyzuserId; //updation
                                                if (item.serviceBookedId != 0)
                                                {
                                                    long serviceId = item.serviceBookedId;
                                                    servicebooked service = db.servicebookeds.FirstOrDefault(m => m.serviceBookedId == serviceId);
                                                    service.chaser_id = wyzuserId;
                                                    db.servicebookeds.AddOrUpdate(service);
                                                }
                                                changeAssignRecord.updatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                                                changeAssignRecord.moduletypeIs = 1;
                                                changeAssignRecord.assigned_manager_id = item.updatedById;
                                                changeAssignRecord.updatedType = item.uploadId != 0 ? "Upload" : "By Admin";
                                                item.updatedStatus = true;
                                                db.change_assignment_records.Add(changeAssignRecord);
                                                db.assignedinteractions.AddOrUpdate(assignInter);
                                            }
                                            else
                                            {
                                                item.updatedStatus = true;
                                            }
                                            db.SaveChanges();
                                        }
                                        else if (item.moduleType == 2)
                                        {
                                            insuranceassignedinteraction assignInter = db.insuranceassignedinteractions.FirstOrDefault(m => m.id == item.assignedInteractionId);
                                            if (assignInter != null)
                                            {
                                                change_assignment_records changeAssignRecord = new change_assignment_records();
                                                changeAssignRecord.assignedinteraction_id = assignInter.id;
                                                changeAssignRecord.campaign_id = assignInter.campaign_id ?? default(long);
                                                changeAssignRecord.last_wyzuserId = assignInter.wyzUser_id ?? default(long);

                                                long wyzuserId = item.newWyzuserId;
                                                changeAssignRecord.new_wyzuserId = wyzuserId;
                                                assignInter.wyzUser_id = wyzuserId;
                                                //if (item.serviceBookedId != 0)
                                                //{
                                                //    long serviceId = item.serviceBookedId;
                                                //    servicebooked service = db.servicebookeds.FirstOrDefault(m => m.serviceBookedId == serviceId);
                                                //    service.chaser_id = wyzuserId;
                                                //    db.servicebookeds.AddOrUpdate(service);
                                                //    db.SaveChanges();
                                                //}

                                                changeAssignRecord.updatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                                                changeAssignRecord.moduletypeIs = 2;
                                                changeAssignRecord.assigned_manager_id = item.updatedById;
                                                changeAssignRecord.updatedType = item.uploadId != 0 ? "Upload" : "By Admin";
                                                item.updatedStatus = true;
                                                db.change_assignment_records.Add(changeAssignRecord);
                                                db.insuranceassignedinteractions.AddOrUpdate(assignInter);
                                            }
                                            else
                                            {
                                                item.updatedStatus = true;
                                            }
                                            db.SaveChanges();
                                        }
                                        else if (item.moduleType == 4)
                                        {
                                            psfassignedinteraction assignInter = db.psfassignedinteractions.FirstOrDefault(m => m.id == item.assignedInteractionId);
                                            if (assignInter != null)
                                            {
                                                change_assignment_records changeAssignRecord = new change_assignment_records();
                                                changeAssignRecord.assignedinteraction_id = assignInter.id;
                                                changeAssignRecord.campaign_id = assignInter.campaign_id ?? default(long);
                                                changeAssignRecord.last_wyzuserId = assignInter.wyzUser_id ?? default(long);

                                                changeAssignRecord.new_wyzuserId = item.newWyzuserId;
                                                long wyzId = item.newWyzuserId;

                                                assignInter.wyzUser_id = wyzId;

                                                //if (item.serviceBookedId != 0)
                                                //{
                                                //    long serviceId = item.serviceBookedId;
                                                //    servicebooked service = db.servicebookeds.FirstOrDefault(m => m.serviceBookedId == serviceId);
                                                //    service.chaser_id = wyzId;
                                                //    db.servicebookeds.AddOrUpdate(service);
                                                //    db.SaveChanges();
                                                //}

                                                changeAssignRecord.updatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                                                changeAssignRecord.moduletypeIs = 4;
                                                changeAssignRecord.assigned_manager_id = item.updatedById;
                                                changeAssignRecord.updatedType = item.uploadId != 0 ? "Upload" : "By Admin";
                                                item.updatedStatus = true;
                                                db.change_assignment_records.Add(changeAssignRecord);
                                                db.psfassignedinteractions.AddOrUpdate(assignInter);
                                            }
                                            else
                                            {
                                                item.updatedStatus = true;
                                            }
                                            db.SaveChanges();
                                        }
                                        item.updatedStatus = true;
                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                            }

                            stopScheduler("admin-changeassignment");

                        }
                        else
                        {
                            logger.Info("\nadmin changeassignment Inactive / Not Exist / Already Running");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                stopScheduler("admin-changeassignment");
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
                logger.Error("\admin-changeassignment(Outer Loop): " + exception);
            }


        }
    }
}