using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using System.Windows.Interop;
using AutoSherpa_project.Models;
using AutoSherpa_project.Models.Schedulers;
using AutoSherpa_project.Models.ViewModels;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using Org.BouncyCastle.Asn1.Ocsp;
using Quartz;
using Quartz.Impl;
namespace AutoSherpa_project.Controllers
{
    public class TestController : Controller
    {
        public ActionResult ShowLoginPost(wyzuser user)
        {

            try
            {
                if (user.userName != null && user.password != null)
                {
                    using (var db = new AutoSherDBContext())
                    {

                        if (db.wyzusers.Any(m => m.userName == user.userName && m.password == user.password && m.unAvailable == false))
                        {
                            //ViewBag.Version = version + " / " + YlaGeneralUtilities.GetBuildDateTime(version);
                            //string selectedDb = user.role;

                            //if (selectedDb == "Indus")
                            //{
                            //    AutoSherDBContext.setContextName("indusContext");
                            //}
                            //else if (selectedDb == "Kalyani")
                            //{
                            //    AutoSherDBContext.setContextName("kalyaniContext");
                            //}
                            //else if (selectedDb == "Hyundai")
                            //{
                            //    AutoSherDBContext.setContextName("advaithContext");
                            //}
                            //else if (selectedDb == "Indus FE")
                            //{
                            //    AutoSherDBContext.setContextName("indusFEContext");
                            //}
                            //else
                            //{
                            //    TempData["IsRoleError"] = "Invalid Db Selection...";
                            //    return RedirectToAction("LogInPage", "Home", null);
                            //}

                            if (Session["UserName"] == null && Session["UserId"] == null && Session["UserRole"] == null)
                            {
                                //var isAuthenticAcces = Task.Run(async () => await getAuthenticateUserLogin(db.dealers.FirstOrDefault().dealerCode)).Result;
                                //if (isAuthenticAcces == true)
                                bool allowIP = true;
                                //string incomingIp = Request.UserHostAddress;
                                bool ipChecking = db.dealers.FirstOrDefault().ipAuthorization;
                                var userData = db.wyzusers.SingleOrDefault(m => m.userName == user.userName && m.password == user.password);
                                var forRole = userData.role;
                                Session["RoleFor"] = forRole;
                                var dealerLogoPath = db.dealers.FirstOrDefault();
                                if (!(string.IsNullOrEmpty(dealerLogoPath.logopath)))
                                {
                                    Session["pathlog"] = dealerLogoPath.logopath;
                                }
                                //if (userData.role != "Admin" && userData.dealerCode == "INDUS" || userData.dealerCode == "POPULAR")
                                //{
                                //    var PasswordupdatedDate = userData.LatestPasswordUpdatedDate;
                                //    DateTime TodaysDate = DateTime.Now;
                                //    var DifferenceDays = (TodaysDate - PasswordupdatedDate).TotalDays;
                                //    var roundOffDays = Math.Round(DifferenceDays);
                                //    if (roundOffDays > 30)
                                //    {
                                //        TempData["IsRoleError"] = "Password Expired!! Contact Admin";
                                //        return RedirectToAction("LogInPage", "Home", null);
                                //    }
                                //    Session["DifferenceDays"] = roundOffDays;

                                //}

                                string incomingIp = Request.UserHostAddress;
                                //var userData = db.wyzusers.SingleOrDefault(m => m.userName == user.userName && m.password == user.password);
                                // Session["UserId"] = userData.id;
                                login_history loginhistoty = new login_history();

                                loginhistoty.wyzuser_id = Convert.ToInt32(userData.id);
                                loginhistoty.loginDateTime = DateTime.Now;
                                loginhistoty.ipAddress = incomingIp;
                                db.loginhistory.AddOrUpdate(loginhistoty);
                                db.SaveChanges();

                                if (ipChecking)
                                {

                                    if (db.authorizedips.Any(m => m.ip_address == incomingIp))
                                    {
                                        allowIP = true;
                                    }
                                    else
                                    {
                                        allowIP = false;
                                    }

                                    if (allowIP == false)
                                    {
                                        if (!string.IsNullOrEmpty(userData.ipAddress))
                                        {
                                            foreach (var ips in userData.ipAddress.Split(','))
                                            {
                                                if (ips == incomingIp)
                                                {
                                                    allowIP = true;
                                                    break;
                                                }

                                            }
                                        }
                                        else
                                        {
                                            allowIP = true;
                                        }
                                    }
                                }

                                if (allowIP == true)
                                {
                                    if (Session["RoleFor"].ToString().ToLower() == userData.role.ToLower() || userData.role == "Admin" || userData.role == "SuperAdmin")
                                    {
                                        Session["UserName"] = userData.userName;
                                        Session["UserId"] = userData.id;
                                        Session["firstName"] = userData.firstName;
                                        //Session["InsurenceRole"] = userData.insuranceRole;
                                        //Session["TenantId"] = userData.tenant_id;
                                        Session["UserRole"] = userData.role;
                                        Session["UserRole1"] = userData.role1;
                                        Session["DealerCode"] = userData.dealerCode;
                                        Session["DealerName"] = userData.dealerName;
                                        //Session["uniquesupcre"] = userData.uniquesupcre;

                                        //Session["DealerCode"] = userData.dealerCode;
                                        Session["ISPSFENABLED"] = db.dealers.FirstOrDefault(m => m.id == userData.dealer_id).isPsfDynamic;
                                        Session["OEM"] = db.dealers.FirstOrDefault(m => m.id == userData.dealer_id).OEM;
                                        //Session["ISFIELDENABLED"] = db.dealers.FirstOrDefault(m => m.id == userData.dealer_id).isfieldexecutive;

                                        if (db.dealers.FirstOrDefault(m => m.id == userData.dealer_id).opsguruUrl != null)
                                        {
                                            Session["OpsGuruLink"] = db.dealers.FirstOrDefault(m => m.id == userData.dealer_id).opsguruUrl;
                                        }

                                        navigationaccess navAccess = db.navaccess.FirstOrDefault(m => m.wyzuser_id == userData.id && m.isactive == true);



                                        List<formmapping> navForms = new NavigationTabController().getNavForms(userData.role1, userData.role, false, navAccess == null ? "" : navAccess.form_id, false);
                                        //Session["navigations"] = JsonConvert.SerializeObject(navForms);
                                        Session["navigations"] = JsonConvert.SerializeObject(navForms.Select(m => new { id = m.id, mainform_id = m.mainform_id, font_icon = m.font_icon, form_name = m.form_name, href_link = m.href_link }).ToList());

                                        //Session["navigations"] = "";
                                        #region AutoSave
                                        //recordLogDetails(1, userData.userName, userData.creManager, userData.dealerCode, userData.id, HttpContext.Session.SessionID);
                                        #endregion

                                        var followUpday = db.dealers.Where(m => m.id == userData.dealer_id).Select(x => new { x.followupdaylimit, x.bookingDayLimit, x.insbookingDayLimit, x.insfollowupdaylimit }).ToArray();
                                        string followUpMaxdate = "";
                                        string bookingMaxDate = "";
                                        string INSfollowUpMaxdate = "";
                                        string INSbookingMaxDate = "";
                                        if (followUpday[0].followupdaylimit == 0)
                                        {
                                            followUpMaxdate = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
                                        }
                                        else
                                        {
                                            followUpMaxdate = DateTime.Now.AddDays(followUpday[0].followupdaylimit).ToString("yyyy-MM-dd");
                                        }
                                        if (followUpday[0].bookingDayLimit == 0)
                                        {
                                            bookingMaxDate = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
                                        }
                                        else
                                        {
                                            bookingMaxDate = DateTime.Now.AddDays(followUpday[0].bookingDayLimit).ToString("yyyy-MM-dd");
                                        }
                                        if (followUpday[0].insbookingDayLimit == 0)
                                        {
                                            INSbookingMaxDate = "30";
                                        }
                                        else
                                        {
                                            INSbookingMaxDate = followUpday[0].insbookingDayLimit.ToString();
                                        }
                                        if (followUpday[0].insfollowupdaylimit == 0)
                                        {
                                            INSfollowUpMaxdate = "30";
                                        }
                                        else
                                        {
                                            INSfollowUpMaxdate = followUpday[0].insfollowupdaylimit.ToString();
                                        }

                                        Session["followUpDayLimit"] = followUpMaxdate + "," + bookingMaxDate;
                                        Session["INSfollowUpDayLimit"] = INSfollowUpMaxdate + "," + INSbookingMaxDate;

                                        long roleID = 0;

                                        if (userData.role == "RM")
                                        {

                                            formmapping firstNav;

                                            if (navForms.Count > 0)
                                            {
                                                firstNav = navForms.FirstOrDefault(m => m.mainform_id != 0);
                                                if (!string.IsNullOrEmpty(firstNav.href_link) && firstNav.href_link.Contains("/"))
                                                {
                                                    if (userData.insuranceRole == true)
                                                    {
                                                        Session["LoginUser"] = "Insurance";
                                                    }
                                                    else
                                                    {
                                                        Session["LoginUser"] = "Service";
                                                    }

                                                    if (firstNav.href_link.Split('/').Count() == 3)
                                                    {
                                                        return RedirectToAction(firstNav.href_link.Split('/')[1], firstNav.href_link.Split('/')[0], new { @id = firstNav.href_link.Split('/')[2] });
                                                    }
                                                    else
                                                    {
                                                        return RedirectToAction(firstNav.href_link.Split('/')[1], firstNav.href_link.Split('/')[0]);
                                                    }

                                                }

                                            }

                                            if (userData.insuranceRole == true)
                                            {
                                                Session["LoginUser"] = "Insurance";
                                                return RedirectToAction("searchCustomer", "Customer");
                                            }
                                            else
                                            {
                                                Session["LoginUser"] = "Service";
                                                return RedirectToAction("RM_serviceReminder", "RMCallLogReminder");
                                            }

                                        }

                                        if (userData.role == "Admin")
                                        {

                                            formmapping firstNav;

                                            if (navForms.Count > 0)
                                            {
                                                firstNav = navForms.FirstOrDefault(m => m.mainform_id != 0);
                                                if (!string.IsNullOrEmpty(firstNav.href_link) && firstNav.href_link.Contains("/"))
                                                {
                                                    if (userData.insuranceRole == true)
                                                    {
                                                        Session["LoginUser"] = "Insurance";
                                                    }
                                                    else
                                                    {
                                                        Session["LoginUser"] = "Service";
                                                    }


                                                    if (firstNav.href_link.Split('/').Count() == 3)
                                                    {
                                                        return RedirectToAction(firstNav.href_link.Split('/')[1], firstNav.href_link.Split('/')[0], new { @id = firstNav.href_link.Split('/')[2] });
                                                    }
                                                    else
                                                    {
                                                        return RedirectToAction(firstNav.href_link.Split('/')[1], firstNav.href_link.Split('/')[0]);
                                                    }
                                                }
                                            }


                                            if (userData.insuranceRole == true)
                                            {
                                                Session["LoginUser"] = "Insurance";
                                                //return RedirectToAction("Change_assignCalls", "Assignment");
                                                return RedirectToAction("searchCustomer", "Customer");
                                            }
                                            else
                                            {
                                                Session["LoginUser"] = "Service";
                                                // return RedirectToAction("Change_assignCalls", "Assignment");
                                                return RedirectToAction("searchCustomer", "Customer");
                                            }

                                        }

                                        if (userData.role == "SuperAdmin")
                                        {

                                            formmapping firstNav;

                                            if (navForms.Count > 0)
                                            {
                                                firstNav = navForms.FirstOrDefault(m => m.mainform_id != 0);
                                                if (!string.IsNullOrEmpty(firstNav.href_link) && firstNav.href_link.Contains("/"))
                                                {
                                                    if (userData.insuranceRole == true)
                                                    {
                                                        Session["LoginUser"] = "Insurance";
                                                    }
                                                    else
                                                    {
                                                        Session["LoginUser"] = "Service";
                                                    }


                                                    if (firstNav.href_link.Split('/').Count() == 3)
                                                    {
                                                        return RedirectToAction(firstNav.href_link.Split('/')[1], firstNav.href_link.Split('/')[0], new { @id = firstNav.href_link.Split('/')[2] });
                                                    }
                                                    else
                                                    {
                                                        return RedirectToAction(firstNav.href_link.Split('/')[1], firstNav.href_link.Split('/')[0]);
                                                    }
                                                }
                                            }


                                            if (userData.insuranceRole == true)
                                            {
                                                Session["LoginUser"] = "Insurance";
                                                //return RedirectToAction("Change_assignCalls", "Assignment");
                                                return RedirectToAction("ManageNavigation", "NavigationTab");
                                            }
                                            else
                                            {
                                                Session["LoginUser"] = "Service";
                                                // return RedirectToAction("Change_assignCalls", "Assignment");
                                                return RedirectToAction("ManageNavigation", "NavigationTab");
                                            }

                                        }

                                        if (userData.role == "WM")
                                        {

                                            formmapping firstNav;

                                            if (navForms.Count > 0)
                                            {
                                                firstNav = navForms.FirstOrDefault(m => m.mainform_id != 0);
                                                if (!string.IsNullOrEmpty(firstNav.href_link) && firstNav.href_link.Contains("/"))
                                                {
                                                    if (userData.insuranceRole == true)
                                                    {
                                                        Session["LoginUser"] = "Insurance";
                                                    }
                                                    else
                                                    {
                                                        Session["LoginUser"] = "Service";
                                                    }

                                                    if (firstNav.href_link.Split('/').Count() == 3)
                                                    {
                                                        return RedirectToAction(firstNav.href_link.Split('/')[1], firstNav.href_link.Split('/')[0], new { @id = firstNav.href_link.Split('/')[2] });
                                                    }
                                                    else
                                                    {
                                                        return RedirectToAction(firstNav.href_link.Split('/')[1], firstNav.href_link.Split('/')[0]);
                                                    }

                                                }

                                            }

                                            if (userData.insuranceRole == true)
                                            {
                                                Session["LoginUser"] = "Insurance";
                                                return RedirectToAction("searchCustomer", "Customer");
                                            }
                                            else
                                            {
                                                Session["LoginUser"] = "Service";
                                                return RedirectToAction("searchCustomer", "Customer");
                                            }

                                        }

                                        if (userData.role == "Service Advisor")
                                        {

                                            formmapping firstNav;

                                            if (navForms.Count > 0)
                                            {
                                                firstNav = navForms.FirstOrDefault(m => m.mainform_id != 0);
                                                if (!string.IsNullOrEmpty(firstNav.href_link) && firstNav.href_link.Contains("/"))
                                                {
                                                    if (userData.insuranceRole == true)
                                                    {
                                                        Session["LoginUser"] = "Insurance";
                                                    }
                                                    else
                                                    {
                                                        Session["LoginUser"] = "Service";
                                                    }

                                                    if (firstNav.href_link.Split('/').Count() == 3)
                                                    {
                                                        return RedirectToAction(firstNav.href_link.Split('/')[1], firstNav.href_link.Split('/')[0], new { @id = firstNav.href_link.Split('/')[2] });
                                                    }
                                                    else
                                                    {
                                                        return RedirectToAction(firstNav.href_link.Split('/')[1], firstNav.href_link.Split('/')[0]);
                                                    }

                                                }

                                            }

                                            if (userData.insuranceRole == true)
                                            {
                                                Session["LoginUser"] = "Insurance";
                                                return RedirectToAction("SAROLog", "SAROLog");
                                            }
                                            else
                                            {
                                                Session["LoginUser"] = "Service";
                                                return RedirectToAction("SAROLog", "SAROLog");
                                            }

                                        }


                                        if (userData.role == "CREManager")
                                        {
                                            if (db.roles.Any(m => m.role1 == "FEManager"))
                                            {
                                                roleID = db.roles.FirstOrDefault(m => m.role1 == "FEManager").id;
                                                var userRole = db.userroles.Where(m => m.users_id == userData.id).ToList();
                                                if (userRole.Any(m => m.roles_id == roleID))
                                                {
                                                    Session["LoginUser"] = "FEManager";
                                                    return RedirectToAction("FieldSchedulerView", "FieldScheduler");
                                                }
                                            }


                                            if (db.roles.Any(m => m.role1 == "DriverManager"))
                                            {
                                                var roleIDriver = db.roles.FirstOrDefault(m => m.role1 == "DriverManager").id;
                                                var userRoleDriver = db.userroles.Where(m => m.users_id == userData.id).ToList();

                                                if (userRoleDriver.Any(m => m.roles_id == roleIDriver))
                                                {
                                                    Session["LoginUser"] = "DriverManager";
                                                    return RedirectToAction("DriverScheduler", "fieldDriverScheduler");
                                                }
                                            }

                                            if (userData.insuranceRole == true)
                                            {
                                                Session["LoginUser"] = "Insurance";
                                                //return RedirectToAction("insuranceReminder", "callLogReminder");
                                                return RedirectToAction("managerHomePage", "callLogReminder");
                                            }
                                            else
                                            {
                                                Session["LoginUser"] = "Service";
                                                return RedirectToAction("managerHomePage", "callLogReminder");

                                                //if (Session["UserRole1"].ToString() == "4")
                                                //{
                                                //    return RedirectToAction("PSFReminder", "callLogReminder");
                                                //}
                                                //else
                                                //{
                                                //    return RedirectToAction("serviceReminder", "callLogReminder");
                                                //}
                                            }

                                        }
                                        else
                                        {
                                            var rol = db.roles.ToList();
                                            if (db.roles.Count(m => m.role1 == "SuperCRE") > 0)
                                            {
                                                roleID = db.roles.FirstOrDefault(m => m.role1 == "SuperCRE").id;
                                                var userRole = db.userroles.Where(m => m.users_id == userData.id).ToList();
                                                if (userRole.Any(m => m.roles_id == roleID))
                                                {
                                                    Session["IsSuperCRE"] = true;
                                                }
                                            }

                                            if (userData.insuranceRole == true)
                                            {
                                                //.....Moving to Insurance Page First;
                                                Session["LoginUser"] = "Insurance";
                                                return RedirectToAction("Insurance", "CREInsurance", null);
                                            }
                                            else
                                            {
                                                if (userData.role1 == "4")
                                                {
                                                    int psfday = 0;
                                                    bool isComplaintMgr = false;
                                                    if (db.roles.Count(m => m.role1 == "Complaint Manager") > 0)
                                                    {
                                                        if (db.userroles.Any(m => m.roles_id == db.roles.FirstOrDefault(x => x.role1 == "Complaint Manager").id && m.users_id == userData.id))
                                                        {
                                                            isComplaintMgr = true;
                                                        }
                                                    }

                                                    if (isComplaintMgr != true)
                                                    {
                                                        if (Session["OEM"].ToString() == "HYUNDAI" || Session["OEM"].ToString() == "BENZ" || Session["DealerCode"].ToString() == "SUKHMANI")
                                                        {
                                                            psfday = 3;
                                                        }
                                                        else if (Session["OEM"].ToString() == "TATA MOTORS")
                                                        {
                                                            psfday = 2;
                                                        }
                                                        else
                                                        {
                                                            psfday = 6;
                                                        }
                                                        Session["LoginUser"] = "PSF";
                                                        return RedirectToAction("PSFDetails", "PSF", new { id = psfday });
                                                    }
                                                    else
                                                    {
                                                        Session["LoginUser"] = "PSFComMgr";
                                                        return RedirectToAction("psfComplaintDetails", "PSF");
                                                    }

                                                }

                                                else if (userData.role1 == "5")
                                                {
                                                    int psfday = 2;
                                                    bool isComplaintMgr = false;
                                                    if (db.roles.Count(m => m.role1 == "Complaint Manager") > 0)
                                                    {
                                                        if (db.userroles.Any(m => m.roles_id == db.roles.FirstOrDefault(x => x.role1 == "Complaint Manager").id && m.users_id == userData.id))
                                                        {
                                                            isComplaintMgr = true;
                                                        }
                                                    }

                                                    if (isComplaintMgr != true)
                                                    {
                                                        Session["LoginUser"] = "POSTSALES";
                                                        return RedirectToAction("postSalesLogs", "postSalesDetails", new { id = psfday });
                                                    }
                                                    else
                                                    {
                                                        Session["LoginUser"] = "POSTSALESComMgr";
                                                        return RedirectToAction("postsalesComplaintLogs", "postSalesDetails");
                                                    }

                                                }
                                                else
                                                {
                                                    //.............Moving to Service Page First;
                                                    Session["LoginUser"] = "Service";
                                                    return RedirectToAction("Service", "CREServiceLog", null);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        TempData["IsRoleError"] = "Invalid Login Attempt!...";
                                        return RedirectToAction("LogInPage", "Home", null);
                                    }
                                }
                                else
                                {
                                    TempData["IsRoleError"] = "Invalid IP Address!..";
                                    return RedirectToAction("LogInPage", "Home", null);
                                }
                            

                            //else


                                {
                                    TempData["IsRoleError"] = "Please Contact Admin to Login!..";
                                    return RedirectToAction("LogInPage", "Home", null);
                                }


                            }
                            else
                            {
                                if (Session["RoleFor"].ToString().ToLower() == "cremanager")
                                {
                                    return RedirectToAction("managerHomePage", "callLogReminder");

                                    //if (Session["LoginUser"].ToString() == "Service")
                                    //{
                                    //    return RedirectToAction("serviceReminder", "callLogReminder", null);
                                    //}
                                    //else
                                    //{
                                    //    return RedirectToAction("insuranceReminder", "callLogReminder");
                                    //}

                                }
                                else
                                {
                                    if (Session["LoginUser"].ToString() == "Service")
                                    {
                                        return RedirectToAction("Service", "CREServiceLog", null);
                                    }
                                    else if (Session["LoginUser"].ToString() == "POSTSALES")
                                    {
                                        return RedirectToAction("postSalesLogs", "postSalesDetails", new { id = 2 });
                                    }
                                    else if (Session["LoginUser"].ToString() == "POSTSALESComMgr")
                                    {
                                        return RedirectToAction("postsalesComplaintLogs", "postSalesDetails");
                                    }
                                    else if (Session["LoginUser"].ToString() == "Insurance")
                                    {
                                        return RedirectToAction("Insurance", "CREInsurance", null);
                                    }
                                    else if (Session["LoginUser"].ToString() == "PSF")
                                    {
                                        int psfday = 0;
                                        if (Session["OEM"].ToString() == "HYUNDAI")
                                        {
                                            psfday = 3;
                                        }
                                        else if (Session["OEM"].ToString() == "TATA MOTORS")
                                        {
                                            psfday = 2;
                                        }
                                        else
                                        {
                                            psfday = 6;
                                        }
                                        return RedirectToAction("PSFDetails", "PSF", new { id = psfday });
                                    }
                                    else if (Session["LoginUser"].ToString() == "PSFComMgr")
                                    {
                                        return RedirectToAction("psfComplaintDetails", "PSF");
                                    }
                                    else
                                    {
                                        return RedirectToAction("Insurance", "CREInsurance", null);
                                    }
                                }

                            }   
                        }
                        else
                        {
                            Session["errors"] = "CredentialsException1";
                            return RedirectToAction("LogInPage", "Home", new { });
                        }
                    }
                }
                else
                {


                    Session["errors"] = "CredentialsException2";
                    return RedirectToAction("LoginPage", "Home", new { @id = 1, @message = Session["errors"] });
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("inner exception"))
                {
                    TempData["Exceptions"] = ex.InnerException.Message.ToString();
                }
                else
                {
                    TempData["Exceptions"] = ex.Message.ToString();
                }

                return RedirectToAction("LogOff", "Home");
            }
            return View();
        }

    }   
}
