using AutoSherpa_project.Models;
using AutoSherpa_project.Models.Schedulers;
using AutoSherpa_project.Models.ViewModels;
using CrystalDecisions.CrystalReports.Engine;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Internal;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AutoSherpa_project.Controllers
{
    [AuthorizeFilter]
    public class SuperAdminController : Controller
    {
        // GET: EventStatus
        public ActionResult Event()
        {
            return View();
        }

        public ActionResult getEventDetails()
        {

            List<eventstaus> listofevent = new List<eventstaus>();
            string exception = "";
            int maxCount = 0;
            try
            {
                //Parameters of Paging..........
                int start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchPattern = Request["search[value]"];
                string role = Session["UserRole"].ToString();




                using (var db = new AutoSherDBContext())
                {
                    if (role == "SuperAdmin")
                    {

                        if (!string.IsNullOrEmpty(searchPattern))
                        {
                            maxCount = db.eventStaus.Count(m => m.Eventname.Contains(searchPattern));

                            if (maxCount < length)
                            {
                                length = maxCount;
                            }

                            listofevent = db.eventStaus.Where(m => m.Eventname.Contains(searchPattern)).OrderBy(m => m.id).Skip(start).Take(length).ToList();
                        }
                        else
                        {
                            maxCount = db.eventStaus.Count();

                            if (maxCount < length)
                            {
                                length = maxCount;
                            }
                            listofevent = db.eventStaus.OrderBy(m => m.id).Skip(start).Take(length).ToList();
                        }
                    }
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
                if (ex.StackTrace.Contains(':'))
                {
                    exception = "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)] + " " + exception;
                }

            }

            return Json(new { data = listofevent, recordsTotal = maxCount, recordsFiltered = maxCount, exception });
            // return Json(new { data = "", recordsTotal = 0, recordsFiltered = 0 });

        }

        //by nisarga
        public ActionResult changeAssignAccess()
        {
            try
            {
            }
            catch (Exception ex)
            {
                string exception = " ";
                if (ex.Message.Contains("inner exception"))
                {
                    exception = ex.InnerException.ToString();
                }
                else
                {
                    exception = ex.Message;
                }
            }
            return View();
        }

        //public ActionResult getSearchAssignTable()
        //{
        //    List<searchandassignaccess> data = new List<searchandassignaccess>();
        //    try
        //    {

        //        using (AutoSherDBContext db=new AutoSherDBContext())
        //        {
        //            data = db.searchandassignaccesses.ToList();
        //            return Json(new { data = data, draw = Request["draw"], recordsTotal = data.Count(), recordsFiltered = data.Count() },JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string exception = "";
        //        if (ex.Message.Contains("inner exception"))
        //        {
        //            exception = ex.InnerException.Message;
        //        }
        //        else
        //        {
        //            exception = ex.Message;
        //        }
        //    }
        //    return Json(new { data = data, draw = Request["draw"], recordsTotal = data.Count(), recordsFiltered = data.Count() }, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult getWyzuserData(string value)
        {
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    if (Session["LoginUser"].ToString() == "Insurance")
                    {
                        var data = db.wyzusers.Where(x => x.role == value && x.unAvailable == false && x.insuranceRole == true).Select(x => new { x.id, x.userName, x.role, x.insuranceRole, x.searchAssignAccess }).ToList();
                        return Json(new { data, draw = Request["draw"], recordsTotal = data.Count(), recordsFiltered = data.Count() }, JsonRequestBehavior.AllowGet);
                    }
                    else if (Session["LoginUser"].ToString() == "Service")
                    {
                        var data = db.wyzusers.Where(x => x.role == value && x.unAvailable == false && x.insuranceRole == false).Select(x => new { x.id, x.userName, x.role, x.insuranceRole, x.searchAssignAccess }).ToList();
                        return Json(new { data, draw = Request["draw"], recordsTotal = data.Count(), recordsFiltered = data.Count() }, JsonRequestBehavior.AllowGet);
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
                return Json(new { success = false, exception }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { });
        }

        public ActionResult setSearchAssignment(string id, string value)
        {
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    long wyzId = long.Parse(id);
                    wyzuser wyzData = db.wyzusers.FirstOrDefault(x => x.id == wyzId);
                    if (value == "on")
                    {
                        wyzData.searchAssignAccess = false;
                    }
                    else
                    {
                        wyzData.searchAssignAccess = true;
                    }
                    //= System.Convert.ToBoolean(value);

                    //string userId = Session["UserId"].ToString();
                    //searchandassignaccess data = db.searchandassignaccesses.FirstOrDefault(x => x.role == role);
                    //data.givenAccess =System.Convert.ToBoolean(givenAccess);
                    //data.updatedBy =long.Parse(userId);
                    //data.updatedOn = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                    db.SaveChanges();
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
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
                return Json(new { success = false, exception }, JsonRequestBehavior.AllowGet);
            }
        }

        #region IPDetails
        //By Akash
        public ActionResult getIPDetails()
        {

            return View();
        }
        public ActionResult getCreName(string roleType)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {

                    if (Session["LoginUser"].ToString() == "Service")
                    {
                        var creList = db.wyzusers.Where(m => m.role == roleType && (m.role1 == "1" || m.role1 == "4")).Select(m => new { id = m.id, name = m.firstName, }).ToList();
                        return Json(new { success = true, data = creList.OrderBy(m => m.name) });
                    }
                    if (Session["LoginUser"].ToString() == "Insurance")
                    {
                        var creList = db.wyzusers.Where(m => m.role == roleType && (m.role1 == "2")).Select(m => new { id = m.id, name = m.firstName, }).ToList();
                        return Json(new { success = true, data = creList.OrderBy(m => m.name) });
                    }

                    return Json(new { success = true });
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

                return Json(new { success = false, exception });
            }
        }


        public ActionResult getDetails()
        {

            string exception = "";
            int maxCount = 0;
            try
            {
                //Parameters of Paging.......
                int start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchPattern = Request["search[value]"];
                string role = Session["UserRole"].ToString();


                using (var db = new AutoSherDBContext())
                {

                    if (role == "SuperAdmin")
                    {

                        if (!string.IsNullOrEmpty(searchPattern))
                        {
                            if (Session["LoginUser"].ToString() == "Service")
                            {
                                maxCount = db.wyzusers.Count(m => m.firstName.Contains(searchPattern) && m.role1 == "1" || m.role1 == "4");
                            }
                            if (Session["LoginUser"].ToString() == "Insurance")
                            {
                                maxCount = db.wyzusers.Count(m => m.firstName.Contains(searchPattern) && m.role1 == "2");
                            }

                            if (maxCount < length)
                            {
                                length = maxCount;
                            }

                            if (Session["LoginUser"].ToString() == "Service")
                            {
                                var listofdata = db.wyzusers.Where(m => m.firstName.Contains(searchPattern) && (m.role1 == "1" || m.role1 == "4")).Select(m => new { role = m.role, userName = m.firstName, ipAddress = m.ipAddress, id = m.id }).OrderBy(m => m.id).Skip(start).Take(length).ToList();
                                return Json(new { data = listofdata, recordsTotal = maxCount, recordsFiltered = maxCount, exception });
                            }
                            if (Session["LoginUser"].ToString() == "Insurance")
                            {
                                var listofdata = db.wyzusers.Where(m => m.firstName.Contains(searchPattern) && (m.role1 == "2")).Select(m => new { role = m.role, userName = m.firstName, ipAddress = m.ipAddress, id = m.id }).OrderBy(m => m.id).Skip(start).Take(length).ToList();
                                return Json(new { data = listofdata, recordsTotal = maxCount, recordsFiltered = maxCount, exception });
                            }
                        }
                        else
                        {
                            if (Session["LoginUser"].ToString() == "Service")
                            {
                                maxCount = db.wyzusers.Count(m => ((m.role == "CRE" && m.unAvailable == false) || m.role == "CREManager" || m.role == "RM" || m.role == "Admin" || m.role == "WM") && (m.role1 == "1" || m.role1 == "4"));
                            }
                            if (Session["LoginUser"].ToString() == "Insurance")
                            {
                                maxCount = db.wyzusers.Count(m => ((m.role == "CRE" && m.unAvailable == false) || m.role == "CREManager" || m.role == "RM" || m.role == "Admin" || m.role == "WM") && (m.role1 == "2"));
                            }

                            if (maxCount < length)
                            {
                                length = maxCount;
                            }
                            if (Session["LoginUser"].ToString() == "Service")
                            {
                                var listofdata = db.wyzusers.Where(m => ((m.role == "CRE" && m.unAvailable == false) || m.role == "CREManager" || m.role == "RM" || m.role == "Admin" || m.role == "WM") && (m.role1 == "1" || m.role1 == "4")).Select(m => new { role = m.role, userName = m.firstName, ipAddress = m.ipAddress, id = m.id }).OrderBy(m => m.id).Skip(start).Take(length).ToList();
                                return Json(new { data = listofdata, recordsTotal = maxCount, recordsFiltered = maxCount, exception });
                            }
                            if (Session["LoginUser"].ToString() == "Insurance")
                            {
                                var listofdata = db.wyzusers.Where(m => ((m.role == "CRE" && m.unAvailable == false) || m.role == "CREManager" || m.role == "RM" || m.role == "Admin" || m.role == "WM") && (m.role1 == "2")).Select(m => new { role = m.role, userName = m.firstName, ipAddress = m.ipAddress, id = m.id }).OrderBy(m => m.id).Skip(start).Take(length).ToList();
                                return Json(new { data = listofdata, recordsTotal = maxCount, recordsFiltered = maxCount, exception });
                            }
                        }
                    }
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
                if (ex.StackTrace.Contains(':'))
                {
                    exception = "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)] + " " + exception;
                }
                return Json(new { data = "", recordsTotal = 0, recordsFiltered = 0, exception });

            }

            return Json(new { data = "", recordsTotal = maxCount, recordsFiltered = maxCount, exception });


        }

        public ActionResult saveDetails(long? id, string ipAddress)
        {

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    wyzuser data = db.wyzusers.Where(a => a.id == id).FirstOrDefault();
                    if (data != null)
                    {
                        data.ipAddress = ipAddress;
                        db.wyzusers.AddOrUpdate(data);
                        db.SaveChanges();
                    }

                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
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

                return Json(new { success = false, exception }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult creMultipleUserIp(string creIds, string ipAddress)
        {
            List<long> cres = creIds.Split(',').Select(long.Parse).ToList();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    List<wyzuser> data = db.wyzusers.Where(a => cres.Contains(a.id)).ToList();
                    foreach (var wyzdata in data)
                    {
                        wyzdata.ipAddress = ipAddress;
                        db.wyzusers.AddOrUpdate(wyzdata);
                    }
                    db.SaveChanges();

                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
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

                return Json(new { success = false, exception }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult getIpAddress(long? creId)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string ipAddress = db.wyzusers.FirstOrDefault(m => m.id == creId).ipAddress;
                    return Json(new { success = true, ipAddress });
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

                return Json(new { success = false, exception }, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult deleteDetails(long? id, string ipAddress)
        {

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    wyzuser data = db.wyzusers.Where(a => a.id == id).FirstOrDefault();
                    if (data != null)
                    {
                        data.ipAddress = ipAddress;
                        db.wyzusers.AddOrUpdate(data);
                        db.SaveChanges();
                    }
                    string ipaddress = data.ipAddress;
                    return Json(new { success = true, ipaddress = ipaddress }, JsonRequestBehavior.AllowGet);
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

                return Json(new { success = false, exception }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Manage Dealer table Fields

        public ActionResult manageFlagField()
        {
            dealer dealerdetails = new dealer();
            string dealercode = (Session["DealerCode"]).ToString();
            try
            {
                using (var db = new AutoSherDBContext())
                {

                    dealerdetails = db.dealers.Where(m => m.dealerCode == dealercode).FirstOrDefault();

                    return View(dealerdetails);
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

                return Json(new { success = false, exception }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult updateDetails(string colName, string colValues, HttpPostedFileBase[] httpPostedFileBase)
        {
            colName = JsonConvert.DeserializeObject<string>(colName);
            colValues = JsonConvert.DeserializeObject<string>(colValues);
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    dealer data = db.dealers.FirstOrDefault();


                    if (data != null)
                    {
                        if (colName == "usercontrol")
                        {
                            data.userControl = Convert.ToBoolean(colValues);

                        }
                        else if (colName == "superControl")
                        {
                            data.superControl = Convert.ToBoolean(colValues);

                        }
                        if (colName == "INSusercontrol")
                        {
                            data.userControl = Convert.ToBoolean(colValues);

                        }
                        else if (colName == "INSsuperControl")
                        {
                            data.superControl = Convert.ToBoolean(colValues);

                        }
                        else if (colName == "followupdaylimit")
                        {
                            data.followupdaylimit = Convert.ToInt32(colValues);

                        }
                        else if (colName == "opsguruurl")
                        {
                            data.opsguruUrl = colValues;

                        }
                        else if (colName == "assignmentinterval")
                        {
                            data.assignmentinterval = Convert.ToInt32(colValues);

                        }
                        else if (colName == "isfieldexecutive")
                        {
                            data.isfieldexecutive = Convert.ToBoolean(colValues);

                        }
                        else if (colName == "ispolicydropexecutive")
                        {
                            data.ispolicydropexecutive = Convert.ToBoolean(colValues);

                        }
                        else if (colName == "indusserviceusername")
                        {
                            data.indusServiceUserName = colValues;

                        }
                        else if (colName == "indusservicepassword")
                        {
                            data.indusServicePassword = colValues;

                        }
                        else if (colName == "indusbaseurl")
                        {
                            data.indusBaseURL = colValues;

                        }
                        else if (colName == "psftriggerstatus")
                        {
                            data.PSFTriggerStatus = Convert.ToBoolean(colValues);

                        }

                        else if (colName == "bookingdaylimit")
                        {
                            data.bookingDayLimit = Convert.ToInt32(colValues);

                        }
                        else if (colName == "insfollowupdaylimit")
                        {
                            data.insfollowupdaylimit = Convert.ToInt32(colValues);
                        }
                        else if (colName == "insbookingdaylimit")
                        {
                            data.insbookingDayLimit = Convert.ToInt32(colValues);
                        }
                        else if (colName == "userLogo")
                        {
                            if (Request.Files.Count > 0)
                            {
                                string ext = System.IO.Path.GetExtension(Request.Files[0].FileName);
                                string fileName = "autosherpaLogo" + ext;
                                string path = Path.Combine(Server.MapPath("~/public/img/"), fileName);
                                Request.Files[0].SaveAs(path);
                                data.logopath = "~/public/img/" + fileName;

                            }
                        }
                        else if (colName == "authorizationkey")
                        {
                            data.authorizationkey = colValues;
                        }

                        db.dealers.AddOrUpdate(data);
                        db.SaveChanges();
                    }
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
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

                return Json(new { success = false, exception }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Adding SMS Template
        public ActionResult addSMSTemplate()
        {
            smstemplate smstemplates = new smstemplate();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    var smsparms = db.messageparameters.Select(m => new { m.detail, m.messageParameter1 }).ToList();

                    ViewBag.msgParameteres = smsparms.OrderBy(m => m.messageParameter1).ToList();
                    ViewBag.moduleTypes = db.moduletypes.OrderBy(m => m.moduleName).ToList();
                    ViewBag.workshopLists = db.workshops.Select(m => new { m.id, m.workshopName }).OrderBy(m => m.workshopName).ToList();
                    if (TempData["SubmissionResult"] != null)
                    {
                        if (TempData["SubmissionResult"].ToString() != "True")
                        {
                            ViewBag.saveResult = TempData["SubmissionResult"].ToString();
                            ViewBag.saveError = true;
                        }
                        else
                        {
                            ViewBag.saveError = false;
                            ViewBag.saveResult = "SMS Template Saved";
                        }
                    }
                }
                return View(smstemplates);
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

                return View(smstemplates);
            }

        }
        [HttpPost, ValidateInput(false)]
        public ActionResult saveSMSTemplate(smstemplate smstemplates)
        {
            List<String> msgparams = new List<String>();
            string submissionResult = "";

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    var smsparms = db.messageparameters.Select(m => new { m.detail, m.messageParameter1, m.id }).ToList();

                    for (int j = 0; j < smsparms.Count; j++)
                    {
                        if (smstemplates.smsTemplate1.Contains(smsparms[j].messageParameter1.ToString() + "({" + smsparms[j].detail.ToString() + "})"))
                        {
                            smstemplates.smsTemplate1 = smstemplates.smsTemplate1.Replace(smsparms[j].messageParameter1.ToString() + "({" + smsparms[j].detail.ToString() + "})", smsparms[j].messageParameter1.ToString() + " ");
                            msgparams.Add(smsparms[j].id.ToString());
                        }
                    }
                    smstemplates.messageparams = String.Join(",", msgparams);
                    smstemplates.isWhatsapp = false;
                    smstemplates.inActive = false;
                    db.smstemplates.AddOrUpdate(smstemplates);
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

            return RedirectToAction("addSMSTemplate");
        }

        public ActionResult displaySMSTemplate(string moduleType)
        {
            string exception = "";
            long? moduleTypeID = null;
            if (!string.IsNullOrEmpty(moduleType))
            {
                moduleTypeID = Convert.ToInt64(moduleType);
            }
            List<smstemplate> smstemplates = new List<smstemplate>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (moduleTypeID != null)
                    {
                        smstemplates = db.smstemplates.Where(m => m.moduletype == moduleTypeID).ToList();
                    }
                    else
                    {
                        smstemplates = db.smstemplates.ToList();

                    }
                    var smsparms = db.messageparameters.Select(m => new { m.detail, m.messageParameter1 }).ToList();
                    for (int i = 0; i < smstemplates.Count; i++)
                    {
                        for (int j = 0; j < smsparms.Count; j++)
                        {
                            if (smstemplates[i].smsTemplate1.Contains(smsparms[j].messageParameter1.ToString()))
                            {
                                smstemplates[i].smsTemplate1 = smstemplates[i].smsTemplate1.Replace(smsparms[j].messageParameter1.ToString(), "<b style='background-color: white;' >" + smsparms[j].detail.ToString() + "</b>");
                            }
                        }
                    }

                }
                return Json(new { data = smstemplates, exception }, JsonRequestBehavior.AllowGet);
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

                return Json(new { data = "", exception }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult actionSMSTemplate(string smsID, string actionType)
        {
            string exception = "";
            int smsIds = Int32.Parse(smsID);
            smstemplate smstemplates = new smstemplate();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (actionType == "deletetemplate")
                    {
                        smstemplates = db.smstemplates.FirstOrDefault(x => x.smsId == smsIds);
                        db.smstemplates.Remove(smstemplates);
                        db.SaveChanges();
                        return Json(new { success = true, message = "Template Deleted Successfully." }, JsonRequestBehavior.AllowGet);

                    }
                    else if (actionType == "edittemplate")
                    {
                        if (smsIds != 0)
                        {
                            var smsparms = db.messageparameters.Select(m => new { m.detail, m.messageParameter1 }).ToList();

                            smstemplates = db.smstemplates.FirstOrDefault(x => x.smsId == smsIds);

                            for (int j = 0; j < smsparms.Count; j++)
                            {
                                if (smstemplates.smsTemplate1.Contains(smsparms[j].messageParameter1.ToString()))
                                {
                                    smstemplates.smsTemplate1 = smstemplates.smsTemplate1.Replace(smsparms[j].messageParameter1.ToString(), smsparms[j].messageParameter1.ToString() + "({" + smsparms[j].detail.ToString() + "})");
                                }
                            }
                        }
                        return Json(new { success = true, message = smstemplates }, JsonRequestBehavior.AllowGet);


                        // return RedirectToAction("addSMSTemplate", smstemplates);
                    }
                    else if (actionType == "activatesmstemplate")
                    {
                        smstemplates = db.smstemplates.FirstOrDefault(x => x.smsId == smsIds);
                        smstemplates.inActive = false;
                        db.smstemplates.AddOrUpdate(smstemplates);
                        db.SaveChanges();
                        return Json(new { success = true, message = "Template Activated Successfully." }, JsonRequestBehavior.AllowGet);
                    }
                    else if (actionType == "deactivatesmstemplate")
                    {
                        smstemplates = db.smstemplates.FirstOrDefault(x => x.smsId == smsIds);
                        smstemplates.inActive = true;
                        db.smstemplates.AddOrUpdate(smstemplates);
                        db.SaveChanges();
                        return Json(new { success = true, message = "Template Deactivated Successfully." }, JsonRequestBehavior.AllowGet);
                    }
                    else if (actionType == "activatewtstemplate")
                    {
                        smstemplates = db.smstemplates.FirstOrDefault(x => x.smsId == smsIds);
                        smstemplates.isWhatsapp = true;
                        db.smstemplates.AddOrUpdate(smstemplates);
                        db.SaveChanges();
                        return Json(new { success = true, message = "Whatsapp Activated Successfully." }, JsonRequestBehavior.AllowGet);
                    }
                    else if (actionType == "deactivatewtstemplate")
                    {
                        smstemplates = db.smstemplates.FirstOrDefault(x => x.smsId == smsIds);
                        smstemplates.isWhatsapp = false;
                        db.smstemplates.AddOrUpdate(smstemplates);
                        db.SaveChanges();
                        return Json(new { success = true, message = "Whatsapp Deactivated Successfully." }, JsonRequestBehavior.AllowGet);
                    }

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

                return Json(new { success = false, message = exception }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, message = "Something Went Wrong." }, JsonRequestBehavior.AllowGet);

        }


        public ActionResult getSMSTemplate(int smsID)
        {
            string exception = "";

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string str = @"CALL sendsms(@inwyzuser_id,@invehicle_id,@inlocid,@insmsid,@ininsid);";

                    MySqlParameter[] sqlParameter = new MySqlParameter[]
                    {
                       new MySqlParameter("@inwyzuser_id",1),
                       new MySqlParameter("@invehicle_id",1),
                       new MySqlParameter("@inlocid","0"),
                       new MySqlParameter("@insmsid",smsID.ToString()),
                       new MySqlParameter("@ininsid","0"),
                    };

                    //string template = db.Database.SqlQuery<string>(str, sqlParameter).FirstOrDefault().ToString();
                    var template = db.Database.SqlQuery<string>(str, sqlParameter).ToList();

                    return Json(new { success = true, message = template });
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

                return Json(new { success = false, message = exception }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Add PSF Questions
        public ActionResult addPSFquestions(int? id)
        {
            using (AutoSherDBContext db = new AutoSherDBContext())
            {
                var campaign = db.campaigns.Where(m => m.campaignType == "PSF" && m.isactive == true).Select(m => new { m.id, m.campaignName }).ToList();
                ViewBag.campaignList = campaign.OrderBy(m => m.campaignName);

                if (id != null)
                {
                    psfquestions psfqustn = db.psfquestions.FirstOrDefault(m => m.id == id);
                    return View(psfqustn);
                }
            }
            return View();
        }
        public ActionResult getPSFquestions()
        {
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    var PSFquestions = db.psfquestions.OrderBy(a => a.id).ToList();
                    return Json(new { data = PSFquestions }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("inner exception"))
                {
                    TempData["Exceptions"] = ex.InnerException.Message;
                }
                return View();
            }
        }
        [HttpGet]
        public ActionResult Save(int id)
        {
            using (AutoSherDBContext db = new AutoSherDBContext())
            {
                var data = db.psfquestions.Where(a => a.id == id).FirstOrDefault();
                return View(data);
            }
        }
        [HttpPost]
        public ActionResult Save(psfquestions psfquestions)
        {
            bool status = false;

            using (AutoSherDBContext db = new AutoSherDBContext())

            {
                if (psfquestions.id > 0)
                {
                    //edit
                    var data = db.psfquestions.Where(a => a.id == psfquestions.id).FirstOrDefault();
                    if (data != null)
                    {

                        data.question_no = psfquestions.question_no;
                        data.question = psfquestions.question;
                        data.isActive = psfquestions.isActive;
                        data.display_type = psfquestions.display_type;
                        data.ddl_range = psfquestions.ddl_range;
                        data.ddl_options = psfquestions.ddl_options;
                        data.radio_options = psfquestions.radio_options;
                        data.campaignid = psfquestions.campaignid;
                        data.binding_var = psfquestions.binding_var;
                        data.qs_mandatory = psfquestions.qs_mandatory;
                        data.visited_cust_cat = psfquestions.visited_cust_cat;
                        data.pickup_cust_cat = psfquestions.pickup_cust_cat;

                    }
                }
                else
                {

                    //save
                    db.psfquestions.Add(psfquestions);
                }
                db.SaveChanges();
                status = true;
            }

            // return View();
            return Json(status, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (AutoSherDBContext db = new AutoSherDBContext())
            {
                var data = db.psfquestions.Where(a => a.id == id).FirstOrDefault();
                if (data != null)
                {
                    return View(data);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        public ActionResult Deletedata(int id)
        {
            string message = "";
            bool status = false;
            using (AutoSherDBContext db = new AutoSherDBContext())
            {
                var data = db.psfquestions.Where(a => a.id == id).FirstOrDefault();
                if (data != null)
                {
                    //delete
                    db.psfquestions.Remove(data);
                    db.SaveChanges();
                    status = true;
                }
            }
            if (status == true)
            {
                message = "Deleted Successfully";
            }
            else
            {
                message = "Error";
            }
            return new JsonResult { Data = message };
        }

        public ActionResult checkbindingvar(string bindingVar)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {


                    if (db.psfquestions.Count(m => m.binding_var == bindingVar && m.isActive == true) > 0)
                    {
                        return Json(new { success = true, exist = true });
                    }
                    else
                    {
                        return Json(new { success = true, exist = false });

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

                return Json(new { success = false, exception }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region scheduler
        public ActionResult schedulerStatus()
        {
            return View();
        }
        public ActionResult getSchedulerDetails()
        {

            List<schedulers> listofschedulers = new List<schedulers>();
            string exception = "";
            int maxCount = 0;
            try
            {

                using (var db = new AutoSherDBContext())
                {
                    listofschedulers = db.schedulers.OrderBy(m => m.scheduler_name).ToList();
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
                if (ex.StackTrace.Contains(':'))
                {
                    exception = "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)] + " " + exception;
                }

            }

            return Json(new { data = listofschedulers, recordsTotal = maxCount, recordsFiltered = maxCount, exception });


        }
        public ActionResult scheduleractionChanges(string action, long schedulerId, string schedulerName, string filterFromDate, string filterToDate, string ispushed)
        {
            try
            {

                using (var db = new AutoSherDBContext())
                {
                    if (action == "refresh" || action == "activate" || action == "deactivate")
                    {
                        schedulers schedulerDetails = db.schedulers.Where(a => a.id == schedulerId).FirstOrDefault();

                        if (schedulerDetails != null)
                        {

                            if (action == "refresh")
                            {
                                //if (schedulerDetails.schedulername == "fe-push")
                                //{
                                //    var triggerBykeyGroup = FieldExecutiveScheduler.fieldExecutiveScheduler.GetTrigger(new Quartz.TriggerKey("FEPushingTrigger", "FEPushingGroup"));
                                //    TaskStatus task = triggerBykeyGroup.Status;

                                //    TimeSpan newmin = new TimeSpan(0, 5, 0);

                                //    ((Quartz.Impl.Triggers.SimpleTriggerImpl)triggerBykeyGroup.Result).RepeatInterval = newmin;
                                //    var iTrigger = triggerBykeyGroup.Result;
                                //    TriggerKey triggerkey = iTrigger.Key;
                                //    AppScheduler.appSchedulers.Start();
                                //    AppScheduler.appSchedulers.RescheduleJob(triggerkey, iTrigger);
                                //}
                                schedulerDetails.IsItRunning = false;
                            }

                            else if (action == "activate")
                            {
                                schedulerDetails.isActive = true;
                            }
                            else if (action == "deactivate")
                            {
                                schedulerDetails.isActive = false;
                            }

                            db.schedulers.AddOrUpdate(schedulerDetails);
                            db.SaveChanges();
                        }
                    }

                    else if (action == "save")
                    {
                        schedulers savescheduler = new schedulers();
                        savescheduler.scheduler_name = schedulerName;
                        savescheduler.isActive = true;
                        savescheduler.IsItRunning = false;
                        savescheduler.datalenght = 0;
                        savescheduler.intervalInMin = 0;
                        savescheduler.dealerid = 1;
                        db.schedulers.Add(savescheduler);
                        db.SaveChanges();

                    }

                    else if (action == "downloadfereport")
                    {
                        string fromDate = (Convert.ToDateTime(filterFromDate)).ToString("yyyy-MM-dd");
                        string toDate = (Convert.ToDateTime(filterToDate)).ToString("yyyy-MM-dd");
                        bool synched = Convert.ToBoolean(ispushed);
                        DataTable fesynchReports = new DataTable();
                        string reportQuery = "select v.vehicleRegNo,v.chassisNo,f.firebasekey,f.lastfirebase_key,if(f.inspolicydrop_id=0,'-','Policy Drop') as 'Policy Drop Status',if(f.isCancelled=0,'-','Canceled') as 'Cancelled Status',f.updatedatetime from fieldexecutivefirebaseupdation f left join vehicle v on v.vehicle_id=f.vehicle_id where date(f.updatedatetime) between date('" + fromDate + "') and date('" + toDate + "') and f.tobepushed=" + synched + ";";

                        string conStr = ConfigurationManager.ConnectionStrings["AutoSherContext"].ConnectionString;
                        using (MySqlConnection connection = new MySqlConnection(conStr))
                        {
                            using (MySqlCommand cmd = new MySqlCommand(reportQuery, connection))
                            {
                                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                                adapter.Fill(fesynchReports);
                            }
                            if (fesynchReports.Rows.Count > 0)
                            {
                                Response.Clear();

                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("InsuranceDue.xlsx", System.Text.Encoding.UTF8));
                                using (ExcelPackage pck = new ExcelPackage())
                                {
                                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("FE Synch Report");
                                    ws.Cells["A1"].LoadFromDataTable(fesynchReports, true);
                                    Session["downloadPSFReports"] = pck.GetAsByteArray();
                                }
                            }
                            return Json(new { success = true, totalReports = fesynchReports.Rows.Count }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    return Json(new { success = true, }, JsonRequestBehavior.AllowGet);
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

                return Json(new { success = false, exception }, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion

        #region isactiveuploadtype

        public ActionResult uploadType()
        {
            return View();
        }
        public ActionResult GetuploadType()
        {
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    var activeList = db.uploadtypes.Select(m => new { m.id, m.uploadDisplayName, m.uploadTypeName, m.isActive }).OrderBy(m => m.uploadTypeName).ToList();
                    return Json(new { data = activeList }, JsonRequestBehavior.AllowGet);
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
                return Json(new { data = "", exception = exception });
            }

        }

        public ActionResult UpdateActivate(int? id, string value)
        {
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    {
                        var active = db.uploadtypes.FirstOrDefault(m => m.id == id);
                        if (value == "isactive")
                        {
                            if (active.isActive == false)
                            {
                                active.isActive = true;
                            }
                            else
                            {
                                active.isActive = false;
                            }
                        }

                        else
                        {

                        }
                        db.uploadtypes.AddOrUpdate(active);
                        db.SaveChanges();
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
                return Json(new { data = "", exception = exception });
            }
        }


        #endregion



        #region CRE Details
        public ActionResult creDetails()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {

                    if (TempData["exception"] != null)
                    {
                        ViewBag.exception = TempData["exception"].ToString();
                        ViewBag.isexception = true;
                    }
                    else if (TempData["uploadId"] != null)
                    {

                        int uploadId = Convert.ToInt32(TempData["uploadId"]);
                        var uploadDetails = db.useruploads.Where(m => m.id == uploadId).Select(m => new { m.fileName, m.rawfilePath, m.uploadedPath, m.discardedPath, m.duplicateCREPath, m.duplicatephoneNumber, m.duplicateUser, m.totalUploaded, m.totalDiscarded, m.totalRecords, m.uploadedDate, m.uploadedTime, m.nomanagerpath, m.duplicateExtensionCREPath }).FirstOrDefault();

                        ViewBag.outputtable = JsonConvert.SerializeObject(uploadDetails);
                        ViewBag.issuccess = true;
                        ViewBag.totaluploaded = "Total " + uploadDetails.totalUploaded + "  Records Uploaded";

                    }


                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }



        public ActionResult creGetUploads()
        {
            try
            {
                int fromIndex = Convert.ToInt32(Request["start"]);
                int toIndex = Convert.ToInt32(Request["length"]);
                string searchPattern = Request["search[value]"].Trim();
                long totalCount = 0;
                using (AutoSherDBContext db = new AutoSherDBContext())
                {

                    totalCount = db.useruploads.Count();




                    if (searchPattern != null && searchPattern != "")
                    {

                        var user = db.useruploads.Where(m => (m.fileName.Contains(searchPattern) || m.fileName.Contains(searchPattern))).Select(m => new { id = m.id, fileName = m.fileName, uploadedPath = m.uploadedPath, discardedPath = m.discardedPath, duplicateCREPath = m.duplicateCREPath, totalUploaded = m.totalUploaded, totalDiscarded = m.totalDiscarded, totalRecords = m.totalRecords, m.uploadedDate, m.uploadedTime, m.nomanagerpath, m.duplicateExtensionCREPath }).OrderByDescending(x => x.id).Skip(fromIndex).Take(toIndex).ToList();
                        var JsonData = Json(new { data = user, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount, exception = "" }, JsonRequestBehavior.AllowGet);
                        JsonData.MaxJsonLength = int.MaxValue;
                        return JsonData;
                    }
                    else
                    {
                        var user = db.useruploads.Select(m => new { id = m.id, fileName = m.fileName, uploadedPath = m.uploadedPath, discardedPath = m.discardedPath, duplicateCREPath = m.duplicateCREPath, totalUploaded = m.totalUploaded, totalDiscarded = m.totalDiscarded, totalRecords = m.totalRecords, m.uploadedDate, m.uploadedTime, m.nomanagerpath, m.duplicateExtensionCREPath }).OrderByDescending(x => x.id).Skip(fromIndex).Take(toIndex).ToList();
                        var JsonData = Json(new { data = user, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount, exception = "" }, JsonRequestBehavior.AllowGet);
                        JsonData.MaxJsonLength = int.MaxValue;
                        return JsonData;
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
                return Json(new { data = "", exception = exception });
            }
        }

        #endregion



        #region CRE Details Upload



        public ActionResult ExcelExport()
        {
            try
            {

                DataTable Dt = new DataTable();
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode("wyzuser_SampleExcel", System.Text.Encoding.UTF8));
                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");

                    ws.Cells[1, 1].Value = "creManager";


                    ws.Cells[1, 2].Value = "firstName";
                    ws.Cells[1, 3].Value = "lastName";
                    ws.Cells[1, 4].Value = "password";

                    ws.Cells[1, 5].Value = "phoneIMEINo";
                    ws.Cells[1, 6].Value = "phoneNumber";

                    ws.Cells[1, 7].Value = "role";

                    ws.Cells[1, 8].Value = "userName";

                    ws.Cells[1, 9].Value = "extensionId";

                    ws.Cells[1, 10].Value = "gsmRegistrationId";
                    ws.Cells[1, 11].Value = "workshopCode";
                    ws.Cells[1, 12].Value = "moduletype";

                    string fileName = "wyzuser_SampleExcel.xlsx";
                    Session["FileName"] = fileName;
                    Session["DownloadExcel_FileManager"] = pck.GetAsByteArray();
                }
            }
            catch (Exception ex)
            {
            }
            return Json(new { JsonRequestBehavior.AllowGet });
        }


        public ActionResult Download()
        {
            try
            {
                string fileName = Session["FileName"].ToString();

                if (Session["DownloadExcel_FileManager"] != null)
                {
                    byte[] data = Session["DownloadExcel_FileManager"] as byte[];
                    return File(data, "application/octet-stream", fileName);
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



        public ActionResult uploadWyzuserFile()
        {
            try
            {
                string fileNameAppend = "_" + DateTime.Now.ToString("yyyyMMddhhmmss");
                DataTable originalColumn = new DataTable();
                DataTable excelColumn = new DataTable();
                long uploadId = 0;
                bool isError = false;
                string customErrors = string.Empty;
                var request = Request;
                for (int i = 0; i < request.Files.Count; i++)
                {
                    var file = request.Files[i];
                    string fileUploadPath = Server.MapPath("~/UploadedFiles/" + Session["DealerCode"].ToString() + "/CREUPLOADS/" + Session["UserName"].ToString() + "/AllRecords/");
                    if (!(Directory.Exists(fileUploadPath)))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(fileUploadPath);
                    }
                    string filename = file.FileName.Split('.')[0];
                    filename = Regex.Replace(filename, "[^a-zA-Z0-9_.]+", "");
                    string ext = System.IO.Path.GetExtension(file.FileName);
                    string final_name = filename + fileNameAppend + ext;
                    var fullPath = Path.Combine(fileUploadPath, final_name);
                    string[] imageArray = file.FileName.Split('.');
                    if (imageArray.Length != 0)
                    {
                        string extansion = imageArray[imageArray.Length - 1].ToLower();
                        file.SaveAs(fullPath);
                        FileInfo existingFile = new FileInfo(fullPath);
                        using (ExcelPackage package = new ExcelPackage(existingFile))
                        {
                            originalColumn.Columns.Add(("creManager").Trim(), typeof(string));

                            originalColumn.Columns.Add(("firstName").Trim(), typeof(string));
                            originalColumn.Columns.Add(("lastName").Trim(), typeof(string));
                            originalColumn.Columns.Add(("password").Trim(), typeof(string));
                            originalColumn.Columns.Add(("phoneIMEINo").Trim(), typeof(string));
                            originalColumn.Columns.Add(("phoneNumber").Trim(), typeof(string));

                            originalColumn.Columns.Add(("role").Trim(), typeof(string));

                            originalColumn.Columns.Add(("userName").Trim(), typeof(string));

                            originalColumn.Columns.Add(("extensionId").Trim(), typeof(string));


                            originalColumn.Columns.Add(("gsmRegistrationId").Trim(), typeof(string));

                            originalColumn.Columns.Add(("workshopCode").Trim(), typeof(string));
                            originalColumn.Columns.Add(("moduletype").Trim(), typeof(string));

                            ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                            int colCount = worksheet.Dimension.End.Column;
                            int maxRows = worksheet.Dimension.End.Row;
                            int rowCount = 1;
                            for (int row = 1; row <= rowCount; row++)
                            {

                                for (int col = 1; col <= colCount; col++)
                                {
                                    if (worksheet.Cells[row, col].Value != null && worksheet.Cells[row, col].Value.ToString().Trim().Length > 0)
                                    {
                                        if (excelColumn.Columns.Contains(worksheet.Cells[row, col].Value.ToString().Trim()))
                                        {

                                        }
                                        else
                                        {
                                            excelColumn.Columns.Add(worksheet.Cells[row, col].Value?.ToString().Trim());
                                        }
                                    }
                                }
                            }

                            if (excelColumn.Columns.Count != originalColumn.Columns.Count)
                            {
                                customErrors = "Columns in the uploaded files are Changed";
                                TempData["exception"] = customErrors;
                                isError = true;
                            }
                            else if (maxRows <= 1)
                            {
                                customErrors = "You cannot Upload Empty file..";
                                TempData["exception"] = customErrors;
                                isError = true;

                            }
                            DataTable d = CompareTwoDataTable(originalColumn, excelColumn);
                            if (d.Columns.Count > 0)
                            {
                                customErrors = "Columns in the uploaded files are changed, expected colum -  " + d.Columns[0].ColumnName;
                                TempData["exception"] = customErrors;
                                isError = true;
                            }
                            if (isError == false)
                            {
                                uploadId = insertData(fullPath);
                                TempData["uploadId"] = uploadId;
                            }
                        }

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

                TempData["exception"] = exception;
                TempData["noexception"] = false; ;

            }
            return RedirectToAction("creDetails");

        }




        public static DataTable CompareTwoDataTable(DataTable orgialCol, DataTable excelCol)
        {
            DataTable d3 = new DataTable();

            try
            {
                for (int i = 0; i < orgialCol.Columns.Count; i++)
                {
                    for (int j = 0; j < excelCol.Columns.Count; j++)
                    {
                        if (orgialCol.Columns[i].ColumnName == excelCol.Columns[j].ColumnName)
                        {
                            break;
                        }
                        else if (excelCol.Columns.Count - 1 == j)
                        {
                            d3.Columns.Add(orgialCol.Columns[i].ColumnName);
                            return d3;
                        }
                    }
                }
                return d3;

            }
            catch (Exception ex)
            {

            }
            return d3;
        }



        public static DataSet GetDataTableFromExcel(string path, bool hasHeader = true)
        {
            DataSet FinalRecords = new DataSet();
            DataTable errorTable = new DataTable();
            string errorcolumn = string.Empty;
            string keyRowNo = string.Empty;
            bool errorinRow = false;
            using (var db = new AutoSherDBContext())
            {
                using (var pck = new OfficeOpenXml.ExcelPackage())
                {
                    using (var stream = System.IO.File.OpenRead(path))
                    {
                        pck.Load(stream);
                    }
                    var ws = TrimEmptyRows(pck.Workbook.Worksheets.First());


                    DataTable wyzuserDatatable = new DataTable();
                    foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                    {
                        wyzuserDatatable.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                        errorTable.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                    }
                    wyzuserDatatable.Columns["creManager"].DataType = typeof(string);
                    errorTable.Columns["creManager"].DataType = typeof(string);

                    wyzuserDatatable.Columns["firstName"].DataType = typeof(string);
                    errorTable.Columns["firstName"].DataType = typeof(string);

                    wyzuserDatatable.Columns["lastName"].DataType = typeof(string);
                    errorTable.Columns["lastName"].DataType = typeof(string);

                    wyzuserDatatable.Columns["password"].DataType = typeof(string);
                    errorTable.Columns["password"].DataType = typeof(string);

                    wyzuserDatatable.Columns["phoneIMEINo"].DataType = typeof(string);
                    errorTable.Columns["phoneIMEINo"].DataType = typeof(string);

                    wyzuserDatatable.Columns["phoneNumber"].DataType = typeof(string);
                    errorTable.Columns["phoneNumber"].DataType = typeof(string);

                    wyzuserDatatable.Columns["role"].DataType = typeof(string);
                    errorTable.Columns["role"].DataType = typeof(string);

                    wyzuserDatatable.Columns["userName"].DataType = typeof(string);
                    errorTable.Columns["userName"].DataType = typeof(string);

                    wyzuserDatatable.Columns["extensionId"].DataType = typeof(string);
                    errorTable.Columns["extensionId"].DataType = typeof(string);

                    wyzuserDatatable.Columns["gsmRegistrationId"].DataType = typeof(string);
                    errorTable.Columns["gsmRegistrationId"].DataType = typeof(string);

                    wyzuserDatatable.Columns["workshopCode"].DataType = typeof(string);
                    errorTable.Columns["workshopCode"].DataType = typeof(string);

                    wyzuserDatatable.Columns["moduletype"].DataType = typeof(string);
                    errorTable.Columns["moduletype"].DataType = typeof(string);

                    DataColumn errocol = new DataColumn("Error", typeof(string));
                    errocol.AllowDBNull = true;
                    errorTable.Columns.Add(errocol);

                    var startRow = hasHeader ? 2 : 1;
                    for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                        if (wsRow.Value != null)
                        {
                            DataRow row = wyzuserDatatable.Rows.Add();
                            foreach (var cell in wsRow)
                            {
                                try
                                {
                                    if (wyzuserDatatable.Columns[cell.Start.Column - 1].DataType == typeof(decimal))
                                    {
                                        try
                                        {
                                            if (cell.Text.ToString().Trim().Length > 0)
                                            {
                                                decimal val = Convert.ToDecimal(cell.Text);
                                                row[cell.Start.Column - 1] = val;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            if (errorinRow)
                                            {
                                                errorcolumn = errorcolumn + " ,  Error in Column Name  " + wyzuserDatatable.Columns[cell.Start.Column - 1].ColumnName + " - values -" + cell.Text.ToString();

                                            }
                                            else
                                            {
                                                errorcolumn = "Error in Column Name  " + wyzuserDatatable.Columns[cell.Start.Column - 1].ColumnName + " - values -" + cell.Text.ToString();

                                            }
                                            keyRowNo = rowNum.ToString();
                                            errorinRow = true;
                                        }
                                    }
                                    else if (wyzuserDatatable.Columns[cell.Start.Column - 1].DataType == typeof(string))
                                    {
                                        try
                                        {
                                            if (cell.Text.ToString().Trim().Length > 0)
                                            {
                                                string val = cell.Text.ToString();
                                                row[cell.Start.Column - 1] = val;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            if (errorinRow)
                                            {
                                                errorcolumn = errorcolumn + " ,  Error in Column Name  " + wyzuserDatatable.Columns[cell.Start.Column - 1].ColumnName + " - values -" + cell.Text.ToString();

                                            }
                                            else
                                            {
                                                errorcolumn = "Error in Column Name  " + wyzuserDatatable.Columns[cell.Start.Column - 1].ColumnName + " - values -" + cell.Text.ToString();

                                            }
                                            keyRowNo = rowNum.ToString();
                                            errorinRow = true;
                                        }
                                    }

                                }
                                catch (Exception ex)
                                {

                                }
                            }
                            if (errorinRow)
                            {
                                errorTable.ImportRow(row);
                                errorTable.AcceptChanges();
                                row.Delete();
                                wyzuserDatatable.AcceptChanges();
                                errorinRow = false;
                                int no = errorTable.Rows.Count - 1;
                                errorTable.Rows[no]["Error"] = errorcolumn;
                                errorcolumn = string.Empty;
                                errorTable.AcceptChanges();
                            }
                        }


                    }
                    wyzuserDatatable.AcceptChanges();
                    FinalRecords.Tables.Add(wyzuserDatatable);
                    FinalRecords.Tables.Add(errorTable);
                    return FinalRecords;
                }
            }
        }


        private static ExcelWorksheet TrimEmptyRows(ExcelWorksheet worksheet)
        {
            //loop all rows in a file
            for (int i = worksheet.Dimension.Start.Row; i <=
           worksheet.Dimension.End.Row; i++)
            {
                bool isRowEmpty = true;
                //loop all columns in a row
                for (int j = worksheet.Dimension.Start.Column; j <= worksheet.Dimension.End.Column; j++)
                {
                    if (worksheet.Cells[i, j].Value != null)
                    {
                        isRowEmpty = false;
                        break;
                    }
                }
                if (isRowEmpty)
                {
                    worksheet.DeleteRow(i);
                }
            }
            return worksheet;
        }



        public long insertData(string path)
        {
            DataSet excelDataTables = new DataSet();
            DataTable finalTable = new DataTable();
            DataTable errorTable = new DataTable();
            int duplicatecount = 0;
            int duplicateextensioncount = 0;
            int duplicatephonecount = 0;
            int totaluploaded = 0;
            bool isinvalidphone = false;
            int notexistmanagerCount = 0;
            List<wyzuser> allduplicatewyzusers = new List<wyzuser>();
            List<wyzuser> allduplicateextension = new List<wyzuser>();
            List<wyzuser> allnotexistmangercres = new List<wyzuser>();
            List<wyzuser> wyzuserDetails = new List<wyzuser>();
            string allduplicatewyzusersphonenumbers = string.Empty;
            string JSONString = string.Empty;

            using (var db = new AutoSherDBContext())
            {
                db.Database.CommandTimeout = 600;
                excelDataTables = GetDataTableFromExcel(path);
                finalTable = excelDataTables.Tables[0];
                errorTable = excelDataTables.Tables[1];

                if (finalTable.Rows.Count > 0)
                {

                    finalTable = finalTable.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is DBNull || string.IsNullOrWhiteSpace(field as string))).CopyToDataTable();
                    JSONString = JsonConvert.SerializeObject(finalTable);

                    wyzuserDetails = JsonConvert.DeserializeObject<List<wyzuser>>(JSONString);
                    wyzuserDetails = wyzuserDetails.OrderByDescending(m => m.role).ToList();

                    //foreach(var item in wyzuserDetails)
                    //{
                    //    item.extensionId = Convert.ToString(Convert.ToInt32(Convert.ToDecimal(item.extensionId)));
                    //}



                    foreach (wyzuser wyzuser_single in wyzuserDetails)
                    {
                        //wyzuser_single.extensionId =wyzuser_single.extensionId;
                        wyzuser_single.tenant_id = db.tenants.FirstOrDefault().id;

                        //wyzuser_single.dealerName = db.dealers.FirstOrDefault().dealerName;

                        if (wyzuser_single.phoneNumber != null)
                        {
                            wyzuser_single.phoneNumber = wyzuser_single.phoneNumber.Trim();
                            if (wyzuser_single.phoneNumber.Length > 13)
                            {
                                isinvalidphone = true;
                            }

                            else if (wyzuser_single.phoneNumber.Length == 12)
                            {
                                string firstcharacter = wyzuser_single.phoneNumber.Substring(0, 2);
                                if (firstcharacter == "91")
                                {
                                    wyzuser_single.phoneNumber = "+" + wyzuser_single.phoneNumber;
                                }
                                else
                                {
                                    isinvalidphone = true;
                                }

                            }
                            else if (wyzuser_single.phoneNumber.Contains("+91"))
                            {
                                wyzuser_single.phoneNumber = wyzuser_single.phoneNumber;
                            }
                            else
                            {

                                if (wyzuser_single.phoneNumber.Length == 10)
                                {
                                    wyzuser_single.phoneNumber = "+91" + wyzuser_single.phoneNumber;
                                }
                                else
                                {
                                    isinvalidphone = true;
                                }
                            }
                        }

                        if (wyzuser_single.userName != null && wyzuser_single.userName != "")
                        {


                            int cremangaerExist = wyzuser_single.creManager == null ? 0 : db.wyzusers.Count(m => m.userName == wyzuser_single.creManager);
                            wyzuser_single.role = wyzuser_single.role == null ? null : wyzuser_single.role.Trim().ToLower();
                            wyzuser_single.userName = wyzuser_single.userName.Trim();
                            if (!(string.IsNullOrEmpty(wyzuser_single.creManager)))
                            {
                                wyzuser_single.creManager = wyzuser_single.creManager.Trim();
                            }
                            if (db.wyzusers.Any(m => m.userName.Trim() == wyzuser_single.userName))
                            {
                                duplicatecount++;
                                allduplicatewyzusers.Add(wyzuser_single);
                            }

                            else if (wyzuser_single.role == "cre" && cremangaerExist == 0)
                            {
                                notexistmanagerCount++;
                                allnotexistmangercres.Add(wyzuser_single);
                            }

                            else if (db.wyzusers.Any(M => M.extensionId != null && M.extensionId == wyzuser_single.extensionId.Trim()))
                            {
                                //wyzuser_single.extensionId = "";
                                duplicateextensioncount++;
                                allduplicateextension.Add(wyzuser_single);
                            }
                            else
                            {
                                if (isinvalidphone == true || db.wyzusers.Any(m => m.phoneNumber.Trim() == wyzuser_single.phoneNumber.Trim()))
                                {
                                    duplicatephonecount++;
                                    allduplicatewyzusersphonenumbers = allduplicatewyzusersphonenumbers + "," + wyzuser_single.userName;
                                    wyzuser_single.phoneNumber = "";
                                    wyzuser_single.phoneIMEINo = "";

                                }

                                wyzuser addwyzuser = new wyzuser();
                                addwyzuser = wyzuser_single;




                                if (!string.IsNullOrWhiteSpace(wyzuser_single.workshopCode))
                                {
                                    if (db.workshops.Count(m => m.workshopCode == wyzuser_single.workshopCode) > 0)
                                    {
                                        var workshopdetails = db.workshops.FirstOrDefault(m => m.workshopCode == wyzuser_single.workshopCode);
                                        addwyzuser.workshop_id = workshopdetails.id;
                                        addwyzuser.location_cityId = workshopdetails.location_cityId;
                                    }


                                    var dealerdetails = db.dealers.FirstOrDefault();
                                    addwyzuser.dealer_id = dealerdetails.id;
                                    addwyzuser.dealerCode = dealerdetails.dealerCode;
                                    addwyzuser.dealerId = dealerdetails.dealerId;
                                    addwyzuser.dealerName = dealerdetails.dealerName;

                                    addwyzuser.updatedDate = DateTime.Now;
                                    addwyzuser.updatedBy = Convert.ToInt64(Session["UserId"]);
                                    //addwyzuser.insuranceRole = true;
                                    addwyzuser.role = addwyzuser.role.Trim();
                                    if ((!string.IsNullOrEmpty(addwyzuser.role)))
                                    {
                                        addwyzuser.role = addwyzuser.role.Trim();

                                        if (addwyzuser.role.ToLower() == "admin")
                                        {
                                            addwyzuser.role = "Admin";
                                        }
                                        if (addwyzuser.role.ToLower() == "cre")
                                        {
                                            addwyzuser.role = "CRE";
                                        }
                                        else if (addwyzuser.role.ToLower() == "cremanager")
                                        {
                                            addwyzuser.role = "CREManager";
                                        }
                                        else if (addwyzuser.role.ToLower() == "superadmin")
                                        {
                                            addwyzuser.role = "SuperAdmin";
                                        }
                                    }
                                    if (addwyzuser.phoneIMEINo != null && addwyzuser.phoneIMEINo != "")
                                    {
                                        addwyzuser.phoneIMEINo = addwyzuser.phoneIMEINo.Trim();
                                    }


                                    if (addwyzuser.moduletype.ToUpper() == "SMR" || addwyzuser.moduletype.ToUpper() == "SMRSUPERCRE")
                                    {
                                        addwyzuser.role1 = "1";
                                        addwyzuser.insuranceRole = false;
                                    }

                                    if (addwyzuser.moduletype.ToUpper() == "INS" || addwyzuser.moduletype.ToUpper() == "INSSUPERCRE")
                                    {
                                        addwyzuser.insuranceRole = true;
                                        addwyzuser.role1 = "2";
                                    }
                                    if (addwyzuser.moduletype.ToUpper() == "PSF" || addwyzuser.moduletype.ToUpper() == "PSFCOMPLAINTMANAGER")
                                    {
                                        addwyzuser.role1 = "4";
                                        addwyzuser.insuranceRole = false;
                                    }
                                    if (addwyzuser.moduletype.ToUpper() == "POSTSALES" || addwyzuser.moduletype.ToUpper() == "POSTSALESCOMPLAINTMANAGER")
                                    {
                                        addwyzuser.role1 = "5";
                                        addwyzuser.insuranceRole = false;
                                    }
                                    db.wyzusers.Add(addwyzuser);
                                    db.SaveChanges();
                                    long role_id = 0;

                                    if (addwyzuser.moduletype != null && (addwyzuser.moduletype.ToUpper() == "PSFCOMPLAINTMANAGER" || addwyzuser.moduletype.ToUpper() == "POSTSALESCOMPLAINTMANAGER" || addwyzuser.moduletype.ToUpper() == "SUPERADMIN" || addwyzuser.moduletype.ToUpper() == "SUPERCRE" || addwyzuser.moduletype.ToUpper() == "INSSUPERCRE" || addwyzuser.moduletype.ToUpper() == "SMRSUPERCRE"))
                                    {
                                        userrole userroles = new userrole();
                                        if (addwyzuser.moduletype.ToUpper() == "PSFCOMPLAINTMANAGER" || addwyzuser.moduletype.ToUpper() == "POSTSALESCOMPLAINTMANAGER")
                                        {

                                            role_id = db.roles.FirstOrDefault(m => m.role1 == "Complaint Manager").id;

                                        }

                                        //else if (addwyzuser.moduletype.ToUpper() == "SUPERADMIN")
                                        //{

                                        //    role_id = db.roles.FirstOrDefault(m => m.role1 == "SuperAdmin").id;

                                        //}
                                        else if (addwyzuser.moduletype.ToUpper() == "INSSUPERCRE" || (addwyzuser.moduletype.ToUpper() == "SMRSUPERCRE"))
                                        {

                                            role_id = db.roles.FirstOrDefault(m => m.role1 == "SuperCRE").id;

                                        }
                                        //else if (addwyzuser.moduletype.ToUpper() == "CRE")
                                        //{

                                        //    role_id = db.roles.FirstOrDefault(m => m.role1 == "CRE").id;

                                        //}
                                        userroles.roles_id = role_id; ;

                                        userroles.users_id = wyzuser_single.id;
                                        db.userroles.Add(userroles);
                                        db.SaveChanges();

                                    }

                                    totaluploaded++;
                                }
                            }



                            isinvalidphone = false;
                        }
                    }


                }

                string Fullfilename = Path.GetFileName(path);
                string filename = Fullfilename.Split('.')[0];
                string ext = Fullfilename.Split('.')[1];

                useruploads UserUploads = new useruploads();
                UserUploads.fileName = filename;
                UserUploads.rawfilePath = path;
                UserUploads.duplicatephoneNumber = "(" + duplicatephonecount + ")" + allduplicatewyzusersphonenumbers;
                UserUploads.duplicateUser = "(" + duplicatecount + ")" + string.Join(",", allduplicatewyzusers);
                UserUploads.totalRecords = finalTable.Rows.Count + errorTable.Rows.Count;
                UserUploads.totalDiscarded = errorTable.Rows.Count + duplicatecount + notexistmanagerCount + duplicateextensioncount;
                if (errorTable.Rows.Count > 0)
                {
                    string DiscardUploadPath = Server.MapPath("~/UploadedFiles/" + Session["DealerCode"].ToString() + "/CREUPLOADS/" + Session["UserName"].ToString() + "/DiscardedRecords/");
                    if (!(Directory.Exists(DiscardUploadPath)))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(DiscardUploadPath);
                    }
                    string destinationFile = System.IO.Path.Combine(DiscardUploadPath, filename + "_Discarded." + ext);
                    DataSetToExcel(errorTable, destinationFile, "Discarded CRE");
                    UserUploads.discardedPath = destinationFile;

                }
                if (totaluploaded > 0)
                {
                    string uploadedFilePath = Server.MapPath("~/UploadedFiles/" + Session["DealerCode"].ToString() + "/CREUPLOADS/" + Session["UserName"].ToString() + "/CompletedFiles/");

                    if (!(Directory.Exists(uploadedFilePath)))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(uploadedFilePath);
                    }
                    string destinationFile = System.IO.Path.Combine(uploadedFilePath, filename + "_Uploaded." + ext);
                    DataSetToExcel(finalTable, destinationFile, "Uploaded");
                    UserUploads.uploadedPath = destinationFile;
                    UserUploads.totalUploaded = Convert.ToInt32(totaluploaded);

                }
                if (allduplicatewyzusers.Count > 0)
                {

                    string DeletefileUploadPath = Server.MapPath("~/UploadedFiles/" + Session["DealerCode"].ToString() + "/CREUPLOADS/" + Session["UserName"].ToString() + "/DuplicareCRE/");

                    if (!(Directory.Exists(DeletefileUploadPath)))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(DeletefileUploadPath);
                    }
                    string destinationFile = System.IO.Path.Combine(DeletefileUploadPath, filename + "_Duplicates." + ext);
                    using (ExcelPackage pck = new ExcelPackage())
                    {
                        pck.Workbook.Worksheets.Add("Duplicate UserName");
                        pck.Workbook.Worksheets.MoveToStart("Duplicate UserName");
                        ExcelWorksheet ws = pck.Workbook.Worksheets[1];
                        var excelExport = allduplicatewyzusers.Select(m => new { m.creManager, m.firstName, m.lastName, m.password, m.userName, m.phoneNumber, m.role, m.phoneIMEINo, m.extensionId, m.workshopCode, m.gsmRegistrationId, m.moduletype }).ToList();
                        ws.Cells["A1"].LoadFromCollection(Collection: excelExport, PrintHeaders: true);
                        pck.SaveAs(new FileInfo(destinationFile));
                    }
                    UserUploads.duplicateCREPath = destinationFile;
                }
                if (allduplicateextension.Count > 0)
                {


                    string DeletefileUploadPath = Server.MapPath("~/UploadedFiles/" + Session["DealerCode"].ToString() + "/CREUPLOADS/" + Session["UserName"].ToString() + "/DuplicateExtensionCRE/");

                    if (!(Directory.Exists(DeletefileUploadPath)))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(DeletefileUploadPath);
                    }
                    string destinationFile = System.IO.Path.Combine(DeletefileUploadPath, filename + "_DuplicatesExtension." + ext);
                    using (ExcelPackage pck = new ExcelPackage())
                    {
                        pck.Workbook.Worksheets.Add("Duplicate Extension");
                        pck.Workbook.Worksheets.MoveToStart("Duplicate Extension");
                        ExcelWorksheet ws = pck.Workbook.Worksheets[1];
                        var excelExport = allduplicateextension.Select(m => new { m.creManager, m.firstName, m.lastName, m.password, m.userName, m.phoneNumber, m.role, m.phoneIMEINo, m.extensionId, m.workshopCode, m.moduletype, m.gsmRegistrationId }).ToList();
                        ws.Cells["A1"].LoadFromCollection(Collection: excelExport, PrintHeaders: true);
                        pck.SaveAs(new FileInfo(destinationFile));
                    }
                    UserUploads.duplicateExtensionCREPath = destinationFile;
                }
                if (allnotexistmangercres.Count > 0)
                {

                    string DeletefileUploadPath = Server.MapPath("~/UploadedFiles/" + Session["DealerCode"].ToString() + "/CREUPLOADS/" + Session["UserName"].ToString() + "/NOTEXISTMANAGERCRE/");

                    if (!(Directory.Exists(DeletefileUploadPath)))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(DeletefileUploadPath);
                    }
                    string destinationFile = System.IO.Path.Combine(DeletefileUploadPath, filename + "_MANAGERNOTFOUND." + ext);
                    using (ExcelPackage pck = new ExcelPackage())
                    {
                        pck.Workbook.Worksheets.Add("Managernotfound");
                        pck.Workbook.Worksheets.MoveToStart("Managernotfound");
                        ExcelWorksheet ws = pck.Workbook.Worksheets[1];
                        var excelExport = allnotexistmangercres.Select(m => new { m.creManager, m.firstName, m.lastName, m.password, m.userName, m.phoneNumber, m.role, m.phoneIMEINo, m.extensionId, m.workshopCode, m.moduletype, m.gsmRegistrationId }).ToList();
                        ws.Cells["A1"].LoadFromCollection(Collection: excelExport, PrintHeaders: true);
                        pck.SaveAs(new FileInfo(destinationFile));
                    }
                    UserUploads.nomanagerpath = destinationFile;
                }

                UserUploads.uploadedDate = DateTime.Now;
                UserUploads.uploadedTime = DateTime.Now.ToShortTimeString();
                db.useruploads.Add(UserUploads);
                db.SaveChanges();
                return UserUploads.id;
            }
        }




        private static void DataSetToExcel(DataTable table, string filePath, string sheetName)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                {
                    pck.Workbook.Worksheets.Add(sheetName);
                    pck.Workbook.Worksheets.MoveToStart(sheetName);
                    ExcelWorksheet ws = pck.Workbook.Worksheets[1];
                    ws.Cells["A1"].LoadFromDataTable(table, true);
                    int numCol = ws.Dimension.Columns;
                    if (sheetName == "Discarded CRE")
                    {
                        ws.Column(numCol).Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FF0000"));
                    }
                }

                pck.SaveAs(new FileInfo(filePath));
            }
        }


        #endregion

        #region infobip credentials

        public ActionResult infobipCredentials()
        {
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    ViewBag.addInfoBip = db.Infobipcredentials.Count();
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
                return Json(new { data = "", exception = exception });
            }
            return View();
        }

        public ActionResult GetInfobipType()
        {
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    var infobip = db.Infobipcredentials.ToList();
                    return Json(new { data = infobip }, JsonRequestBehavior.AllowGet);
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
                return Json(new { data = "", exception = exception });
            }

        }
        [HttpPost]
        public ActionResult saveInfobip(infobipcredentials infocred)
        {
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    db.Infobipcredentials.AddOrUpdate(infocred);
                    db.SaveChanges();
                }
                ModelState.Clear();
                return Json(new { success = true, message = "Submitted successfully!" });


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
            return View("infobipCredentials");

        }

        #endregion

        #region coupon Redemption

        public ActionResult couponRedemption()
        {
            using (AutoSherDBContext db = new AutoSherDBContext())
            {
                var ddlWorkshop = db.workshops.Select(model => new { workshopId = model.id, workshopName = model.workshopName }).OrderBy(m => m.workshopName).ToList();
                ViewBag.ddlWorkshop = ddlWorkshop;

            }
            return View();
        }

        public ActionResult GetCouponRedemption(string couponValue)
        {

            List<couponRedemption> coupondeatails = new List<couponRedemption>();
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {

                    string str = @"CALL customer_search_coupon(@pattern);";

                    MySqlParameter[] sqlParameter = new MySqlParameter[]
                    {
                            new MySqlParameter("pattern", couponValue)

                    };
                    coupondeatails = db.Database.SqlQuery<couponRedemption>(str, sqlParameter).ToList();

                    return Json(new { data = coupondeatails }, JsonRequestBehavior.AllowGet);
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
                return Json(new { data = "", exception = exception });
            }
        }
        private string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
        {

            string sOTP = String.Empty;

            string sTempChars = String.Empty;

            Random rand = new Random();

            for (int i = 0; i < iOTPLength; i++)

            {

                int p = rand.Next(0, saAllowedCharacters.Length);

                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                sOTP += sTempChars;

            }

            return sOTP;

        }
        public ActionResult couponStatusRedeem(int id, string couponstatus, string enteredOTP, string JCNumber, string coupon_Workshop)
        {
            string phoneNumber = string.Empty;
            int userId = Convert.ToInt32(Session["UserId"]);
            string DealerCode = Session["DealerCode"].ToString();
            string response_string = string.Empty;
            string message = string.Empty;
            string APIURL = string.Empty;
            string smsBody = string.Empty;
            long cusomerId = 0;
            long vehicleid = 0;
            smsparameter parameter = new smsparameter();
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    smstemplate template = new smstemplate();
                    couponinteraction coupon = db.Couponinteractions.FirstOrDefault(m => m.id == id);
                    if (couponstatus == "OTP" || couponstatus == "SENDSMS")
                    {
                        parameter = db.smsparameters.FirstOrDefault();
                        if (db.phones.Count(m => m.customer_id == coupon.customer_id && m.isPreferredPhone == true) > 0)
                        {
                            phoneNumber = db.phones.FirstOrDefault(m => m.customer_id == coupon.customer_id && m.isPreferredPhone == true).phoneNumber;
                            try
                            {
                                cusomerId = coupon.customer_id;
                                vehicleid = coupon.vehicleid;

                                if (couponstatus == "OTP")
                                {
                                    string sRandomOTP = GenerateRandomOTP(4, saAllowedCharacters);
                                    couponredemptionotp otpDetails = new couponredemptionotp();
                                    if (db.Couponredemptionotps.Count(m => m.customerid == coupon.customer_id) > 0)
                                    {
                                        var removeOtpDetails = db.Couponredemptionotps.FirstOrDefault(m => m.customerid == coupon.customer_id);
                                        db.Couponredemptionotps.Remove(removeOtpDetails);
                                        db.SaveChanges();
                                    }

                                    otpDetails.customerid = coupon.customer_id;
                                    otpDetails.otp = sRandomOTP;
                                    //otpDetails.JCNumber = JCNumber;
                                    otpDetails.timestamp = DateTime.Now.AddMinutes(5);
                                    db.Couponredemptionotps.Add(otpDetails);
                                    db.SaveChanges();
                                    template = db.smstemplates.FirstOrDefault(m => m.inActive == false && m.smsType == "COUPONREDEMPTIONOTP" && m.inActive == false);
                                    //smsBody = template.smsTemplate1.Replace("", );
                                    //smsBody = .Replace("{{OTP}}", sRandomOTP);
                                    smsBody = template.smsTemplate1.Replace("{{OTP}}",sRandomOTP).Replace("{{VOUCHERNUMBER}}", coupon.couponcode);
                                    message = "OTP Sent successfully.";

                                }
                                else if (couponstatus == "SENDSMS")
                                {
                                    string customerName = db.customers.FirstOrDefault(m => m.id == coupon.customer_id).customerName;
                                    string vehRegNo = db.vehicles.FirstOrDefault(m => m.vehicle_id == coupon.vehicleid).vehicleRegNo;
                                    template = db.smstemplates.FirstOrDefault(m => m.inActive == false && m.smsType == "COUPONREDEMPTIONOFFER" && m.inActive == false);

                                    smsBody = template.smsTemplate1.Replace("{{CUSTOMERNAME}}", customerName).Replace("{{REGNO}}", vehRegNo).Replace("{{VOUCHERNUMBER}}", coupon.couponcode).Replace("{{EXPIRYDATE}}", coupon.couponexpirydate.ToString());
                                    message = "Message Sent successfully.";


                                }
                                APIURL = template.smsAPI+ parameter.senderid + "=" + template.dealerName+ "&" + parameter.phone + "=" + phoneNumber.Trim() + "&" + parameter.message + "=" + smsBody;

                                WebRequest request = WebRequest.Create(APIURL);
                                HttpWebRequest httpWebRequest = (HttpWebRequest)request;
                                httpWebRequest.Method = "GET";
                                httpWebRequest.Accept = "application/json";
                                HttpWebResponse response = null;
                                response = (HttpWebResponse)httpWebRequest.GetResponse();
                                response_string = string.Empty;
                                using (Stream strem = response.GetResponseStream())
                                {
                                    StreamReader sr = new StreamReader(strem);
                                    response_string = sr.ReadToEnd();
                                    sr.Close();
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
                                return Json(new { success = false, message = exception });
                            }
                            finally
                            {
                                smsinteraction smsinteraction = new smsinteraction();

                                smsinteraction.interactionDate = DateTime.Now.ToString("dd-MM-yyyy");
                                smsinteraction.interactionDateAndTime = DateTime.Now;
                                smsinteraction.interactionTime = DateTime.Now.ToString("HH:mm:ss");
                                smsinteraction.interactionType = "Text Msg";
                                smsinteraction.responseFromGateway = response_string;
                                smsinteraction.customer_id = cusomerId;
                                smsinteraction.vehicle_vehicle_id = vehicleid;
                                smsinteraction.wyzUser_id = userId;
                                smsinteraction.mobileNumber = phoneNumber;
                                smsinteraction.smsType = template.smsId.ToString();
                                smsinteraction.smsMessage = smsBody;
                                smsinteraction.isAutoSMS = true;

                                if (DealerCode == "INDUS")
                                {
                                    Regex r = new Regex(@"^\d+$");
                                    if (r.IsMatch(response_string))
                                    {
                                        smsinteraction.smsStatus = true;
                                        smsinteraction.reason = "Send Successfully";
                                    }
                                    else
                                    {
                                        smsinteraction.smsStatus = false;
                                        smsinteraction.reason = "Sending Failed";
                                    }
                                }
                                else
                                {
                                    if (response_string.Contains(parameter.sucessStatus))
                                    {
                                        smsinteraction.smsStatus = true;
                                        smsinteraction.reason = "Send Successfully";
                                    }
                                    else
                                    {
                                        smsstatu status = new smsstatu();
                                        status = db.smsstatus.FirstOrDefault(m => response_string.Contains(m.code));
                                        if (status == null)
                                        {
                                            smsinteraction.smsStatus = false;
                                            smsinteraction.reason = "Sending Failed";
                                        }
                                        else if (status != null)
                                        {
                                            smsinteraction.smsStatus = false;
                                            smsinteraction.reason = status.description;
                                        }
                                    }

                                }
                                db.smsinteractions.Add(smsinteraction);
                                db.SaveChanges();
                            }
                        }
                        else
                        {

                            message = "Please Enter customer Phone Number";
                        }

                    }
                    else if (couponstatus == "REDEEMED")
                    {
                        enteredOTP = enteredOTP.Trim();
                        couponredemptionotp confirmOtp = db.Couponredemptionotps.FirstOrDefault(m => m.customerid == coupon.customer_id);
                        if (confirmOtp.timestamp <= DateTime.Now)
                        {
                            message = "OTP Expired.. Please Resend OTP";
                        }
                        else if ((JCNumber == "" || JCNumber == null) || (coupon_Workshop == "" || coupon_Workshop == null))
                        {
                            message = "Please enter all the above";
                        }
                        else if (confirmOtp.otp == enteredOTP)
                        {
                            if (db.Couponredemptionotps.Count(m => m.customerid == coupon.customer_id) > 0)
                            {
                                var removeOtpDetails = db.Couponredemptionotps.FirstOrDefault(m => m.customerid == coupon.customer_id);
                                db.Couponredemptionotps.Remove(removeOtpDetails);
                                db.SaveChanges();
                            }
                            coupon.status = "REDEEMED";
                            coupon.redemptionDate = DateTime.Now;
                            coupon.JCNumber = JCNumber;
                            coupon.coupon_Workshop = coupon_Workshop;
                            db.Couponinteractions.AddOrUpdate(coupon);

                            db.SaveChanges();
                            serviceCouponVM serviceCoupon = new serviceCouponVM();
                            serviceCoupon.PolicyNo = coupon.couponcode;
                            var veh = db.vehicles.FirstOrDefault(m => m.vehicle_id == coupon.vehicleid);
                            serviceCoupon.ChassisNo = veh.chassis;
                            serviceCoupon.EngineNo = veh.engineNo;
                            if (coupon.status == "REDEEMED")
                            {
                                serviceCoupon.CouponStatus = coupon.status.Replace("REDEEMED", "Y");
                            }
                            serviceCoupon.PolicyIssueDate = Convert.ToDateTime(coupon.issuedate).ToString("MM/dd/yyyy");
                            serviceCoupon.LoyaltyType = coupon.coupondeatails;
                            serviceCouponForInsurance(serviceCoupon);
                            message = "Reedemed successful";
                        }
                        else
                        {
                            message = "Please Enter Valid OTP.";
                        }
                    }
                    else if (couponstatus == "REJECT")
                    {
                        coupon.status = "REJECTED";
                        db.Couponinteractions.AddOrUpdate(coupon);
                        db.SaveChanges();
                        message = "Denied successful";
                    }
                    else
                    {
                        message = "No Action Found.";
                    }
                    return Json(new { success = true, message = message, phoneNumber }, JsonRequestBehavior.AllowGet);

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
                return Json(new { success = false, message = exception });
            }
        }


        #endregion

        #region service api for indus

        public void serviceCouponForInsurance(serviceCouponVM serviceCoupon)
        {
            IScheduler serviceCouponScheduler = StdSchedulerFactory.GetDefaultScheduler().Result;

            serviceCouponScheduler.Start();
            IJobDetail jobDetail = JobBuilder.Create<serviceCouponForInsuranceJob>().Build();

            string trigername = "ServiceCouponSavingTrigger" + DateTime.Now.Millisecond + serviceCoupon.EngineNo;
            string trigerGroupName = "ServiceCouponSavingTriggerGroup" + DateTime.Now.Millisecond + serviceCoupon.EngineNo;

            ITrigger trigger = TriggerBuilder.Create().WithIdentity(trigername, trigerGroupName).StartNow().WithSimpleSchedule().Build();

            jobDetail.JobDataMap["ServiceCouponSaving"] = JsonConvert.SerializeObject(serviceCoupon);


            serviceCouponScheduler.ScheduleJob(jobDetail, trigger);

        }

        #endregion


        #region service type

        public ActionResult Servicetype()
        {

            return View();

        }

        public ActionResult getSeviceType()
        {
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    var activeList = db.servicetypes.Select(m => new { m.id, m.serviceId, m.serviceTypeName, m.isActive }).OrderBy(m => m.serviceTypeName).ToList();
                    return Json(new { data = activeList }, JsonRequestBehavior.AllowGet);
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
                return Json(new { data = "", exception = exception });
            }
        }

        public ActionResult updateactivateservicetype(int? id, string value)
        {
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    {
                        var active = db.servicetypes.FirstOrDefault(m => m.id == id);
                        if (value == "isactive")
                        {
                            if (active.isActive == false)
                            {
                                active.isActive = true;
                            }
                            else
                            {
                                active.isActive = false;
                            }
                        }

                        else
                        {

                        }
                        db.servicetypes.AddOrUpdate(active);
                        db.SaveChanges();
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
                return Json(new { data = "", exception = exception });
            }
        }

        #endregion

        #region PickUpSlip
        public ActionResult PickUpSlip()
        {
            using (var db = new AutoSherDBContext())
            {
                ViewBag.workshopNames = db.workshops.Select(model => new { workshopId = model.id, workshopName = model.workshopName }).OrderBy(m => m.workshopName).ToList();
            }
            return View();
        }
        [HttpPost]
        public ActionResult pickupslipDownload(List<String> workshopId, DateTime? fromDate)
        {
            using (var db = new AutoSherDBContext())
            {
                string PickupfromDate = fromDate?.ToString("yyyy-MM-dd");

                string manageruserName = string.Empty;

                manageruserName = Session["UserName"].ToString();

                string UserRole = Session["UserRole"].ToString();

                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/"), "DriverReport.rpt"));
                List<DriverPickUpModel> driverpickupmodels = new List<DriverPickUpModel>();

                if (UserRole == "Admin")
                {
                    driverpickupmodels = db.Database.SqlQuery<DriverPickUpModel>("call event_pickupdropquery(@inmanager,@inworkshop,@instrassigndate,@inendassigndate);",
                 new MySqlParameter("@inmanager", manageruserName),
                 new MySqlParameter("@inworkshop", workshopId),
                 new MySqlParameter("@instrassigndate", PickupfromDate),
                 new MySqlParameter("@inendassigndate", "")).ToList();
                }
                else
                {
                    driverpickupmodels = db.Database.SqlQuery<DriverPickUpModel>("call event_pickupdropquery(@inmanager,@inworkshop,@instrassigndate,@inendassigndate);",
                new MySqlParameter("@inmanager", manageruserName),
                new MySqlParameter("@inworkshop", "0"),
                new MySqlParameter("@instrassigndate", PickupfromDate),
                new MySqlParameter("@inendassigndate", "")).ToList();
                }

                if (driverpickupmodels != null)
                {
                        rd.SetDataSource(driverpickupmodels);
                }

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return File(stream, "application/pdf", "customerlist.pdf");

            }

        }
        #endregion

        #region Appointment Report
        public ActionResult FieldAppointment()
        {
            using (var db = new AutoSherDBContext())
            {
                List<string> strinsuranceAgentIds = db.insurancecallhistorycubes.Where(m => m.insuranceAgent_insuranceAgentId != null).Select(m => m.insuranceAgent_insuranceAgentId).Distinct().ToList();
                List<long> longinsuranceAgentIds = strinsuranceAgentIds.Select(long.Parse).ToList();
                ViewBag.insuranceAgentNames = db.insuranceagents.Where(m => longinsuranceAgentIds.Contains(m.insuranceAgentId)).Select(m => new { m.insuranceAgentId, m.insuranceAgentName }).OrderBy(m => m.insuranceAgentName).ToList();
            }
            return View();
        }
        [HttpPost]
        public ActionResult DownloadFieldAppointment(List<String> insuranceAgentId, DateTime? fromDate, DateTime? toDate)
        {
            using (var db = new AutoSherDBContext())
            {
                
                string reportfromDate = fromDate?.ToString("yyyy-MM-dd");
                string reporttoDate = toDate?.ToString("yyyy-MM-dd");
               
                string userName = string.Empty;
                string manageruserName = string.Empty;

                userName = Session["UserName"].ToString();
                manageruserName = db.wyzusers.Where(m => m.userName == userName).FirstOrDefault().creManager;

                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/"), "FieldAppointmentReport.rpt"));
                List<FieldAppointment> FieldAppointments = new List<FieldAppointment>();

                if (insuranceAgentId == null)
                {
                    FieldAppointments = db.Database.SqlQuery<FieldAppointment>("call event_appointmentquery(@fename,@inapstartdate,@inapenddate,@incremanager);",
                new MySqlParameter("@fename", ""),
                new MySqlParameter("@inapstartdate", reportfromDate),
                new MySqlParameter("@inapenddate", reporttoDate),
                new MySqlParameter("@incremanager", manageruserName)).ToList();
                }
                else
                {
                    string insuranceAgentIds = string.Join(",", insuranceAgentId);
                    FieldAppointments = db.Database.SqlQuery<FieldAppointment>("call event_appointmentquery(@fename,@inapstartdate,@inapenddate,@incremanager);",
                 new MySqlParameter("@fename", insuranceAgentIds),
                 new MySqlParameter("@inapstartdate", reportfromDate),
                 new MySqlParameter("@inapenddate", reporttoDate),
                 new MySqlParameter("@incremanager", manageruserName)).ToList();
                }
                
                if (FieldAppointments != null)
                {
                    rd.SetDataSource(FieldAppointments);
                }

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();
                return File(stream, "application/pdf", "FieldAppointmentReport.pdf");
            }
        }
        #endregion


    }
    public class couponRedemption
    {
        public int id { get; set; }
        public long couponintraction_id { get; set; }
        public DateTime? issuedate { get; set; }
        public string vehicleRegNo { get; set; }
        public string phonenumber { get; set; }
        public DateTime? redemptiondate { get; set; }
        public long callintraction { get; set; }
        public string Crename { get; set; }
        public string status { get; set; }
        public DateTime? couponexpirydate { get; set; }
        public string ChassisNo { get; set; }
        public string customerName { get; set; }
        public string couponcode { get; set; }
        public long assignintraction { get; set; }
    }
}