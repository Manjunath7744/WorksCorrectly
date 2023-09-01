using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using AutoSherpa_project.Models;
using AutoSherpa_project.Models.ViewModels;
using MySql.Data.MySqlClient;
using System.Reflection;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Net;
using Firebase.Database;
using Newtonsoft.Json.Linq;

namespace AutoSherpa_project.Controllers
{

    public class HomeController : Controller
    {
        public static List<string> wyzCRM1 = new List<string>(
             new string[] {"ATULSALES","AudiKarnal","AUDIMOTORS","BHAGATHFORD","BHAGATVOLKSWAGON","GALAXYTOYOTA","HANSHYUNDAI",
                "JDTATAUTONATION","BLUEHYUNDAI","SOMANIHYUNDAI","KPRVOLKSWAGON","SSHYUNDAI","STARHYUNDAI","STARSUZUKI",
                "TSG","VIBRANTFORD","VARRSHAFORD","WASANTOYOTA","YASHODHAHYUNDAI","PRIDEHONDA","BULLMENN","MAGNUM","RAJASKODA","SPEEDAUTO", "MAVERICKMOTORS"  });

        public static List<string> wyzCRM = new List<string>(
            new string[] { "BALAJIMOTORS", "BERKELEYHYUNDAI", "JDTATAUTONATION", "KALYANIMOTORS", "MAHANTHMOTORS", "SIRISHAUTO", "SIRISHSALES", "TATAUTONATION",
                "INDUS", "INDUSFE", "POTHENSHYUNDAI" });

        public static List<string> autosherpa1 = new List<string>(new string[] { "HARPREETFORD", "KAUSHALYA", "SAMATA", "SAWANHYUNDAI", "PAWANHYUNDAI", "ATULAUTOMOTIVES",
            "AANHERO", "MADHAVSUZUKI", "MADHAVTVS", "JDAUTONATION","DEEPHONDA","LAMBAHYUNDAI","MADHUBANTOYOTA","SHIVAMHYUNDAI","ADVAITHHYUNDAI" });

        public static List<string> WyzCrmNew = new List<string>(new string[] { "INDUSFE", "INDUS", "testdb", "CAUVERYFORD", "AANHONDA", "POPULARHYUNDAI", "SMARTWHEELS","KUNHYUNDAI",
            "BRIDGEWAYMOTORS","FRIENDLYMOTORS","KATARIA", "CHITTORHYUNDAI","PLATINUMMOTOCORP", "CHAVANMOTORS", "BHARATHHYUNDAI", "ARKARENAULTHYUNDAI",
            "LAKSHMIHYUNDAIVSP", "KATARIASA", "SPEEDAUTO", "CMAUTOSALES", "ADITYACARCARE", "ABTMARUTHI", "AMMOTORS",  "BHANDARIAUTOMOBILE",
             "BULLMENN","TRICITYAUTO" });
        public static List<string> autosherpa3 = new List<string>(new string[] { "POPULAR", "SUKHMANI", "VBSRSHYUNDAI", "DREAMMACHINE", "RAJALAKSHMICARS", "HANSHYUNDAI", "ANIKAHYUNDAI", "SMMARUTICARS", "ATULMOTORS", "BULLMENMOTORS", "NAVNEETMOTORS", "RISHABHFOURWHEELS", "SALESCRM", "ACEKUDALECARS", "JAMMUMOTORS", "MAHALAXMIAUTOMOTIVE", "RDMOTORS", "JAYABHERIAUTO", "NAVDESH", "FORTPOINTAUTOMOTIVES", "TOYOTADEMO", "FORTUNETOYOTAHYD", "FORTUNETOYOTAKERALA", "LAKSHMIHYUNDAIHYD", "HYUNDAIDEMO3", "MARUTHIDEMO1", "HYUNDAIDEMO2", "MARUTHIDEMO3", "HYUNDAIDEMO1", "MARUTHIDEMO2", "PODDARCARWORLD", "KATARIATRUCKING", "SUSEECARSANDTRUCKS" , "BADRIKAHYUNDAI", "ALLIEDMOTORS", "CHUNGATHHYUNDAI", "ADITHYAHONDA", "SHYMASHONDA", "SSMAHINDRA", "SAIAUTOHYUNDAI", "SANSAIHYUNDAI" , "UNIRIDEHONDA" });

        [HttpGet]
        public ActionResult Index(int? id, string message)
        {
            if (TempData["IsRoleError"] != null)
            {
                ViewBag.error = TempData["IsRoleError"];
            }

            if (message != null || TempData["ControllerName"] != null)
            {
                ViewBag.Exception = message;
                //Session["Exceptions"] = null;
                //ViewBag.contro = TempData["ControllerName"];
            }

            if (id == 200 || id != 0)
            {
                Session["RoleFor"] = null;
            }
            using (var db = new AutoSherDBContext())
            {
                try
                {
                    var dealerLogoPath = db.dealers.FirstOrDefault();
                    if (!(string.IsNullOrEmpty(dealerLogoPath.logopath)))
                    {
                        Session["pathlog"] = dealerLogoPath.logopath;
                    }
                }
                catch (Exception ex)
                {
                   
                }
            }
            //string response = "";
            //response = "106";

            //serviceFilter service = new serviceFilter();

            //Type servicetype = typeof(serviceFilter);
            //string cityColumn = "city";
            //PropertyInfo serviceProp = servicetype.GetProperty(cityColumn);
            //serviceProp.SetValue(service, "Bangalore");
            //Console.Write(service.city);
            //var obj = service.GetType().GetProperty("city");
            //service.GetType().GetProperty(cityColumn).SetValue(service, "Mangalore");

            return View();
        }

        [ActionName("LogInPage"), HttpGet]
        public ActionResult ShowLogin(int? id,string message)
        {
         
            
            return View();
        }

        [ActionName("LogInPage"), HttpPost]
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
                                                    /*
                                                    if (firstNav.href_link.Split('/').Count() == 3)
                                                    {
                                                        return RedirectToAction(firstNav.href_link.Split('/')[1], firstNav.href_link.Split('/')[0], new { @id = firstNav.href_link.Split('/')[2] });
                                                    }
                                                    else
                                                    {
                                                        return RedirectToAction(firstNav.href_link.Split('/')[1], firstNav.href_link.Split('/')[0]);
                                                    }
                                                    */
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
                            Session["errors"] = "CredentialsException";
                            return RedirectToAction("LogInPage", "Home", new { @id = 1, @message = Session["errors"] });
                        }
                    }
                }
                else
                {


                    Session["errors"] = "CredentialsException";
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

        public ActionResult LogOff()
        {
            string error = string.Empty;


            try
            {
                //if (HttpContext.Session != null)
                //{
                //    recordLogDetails(2, "", "", "", 0, HttpContext.Session.SessionID);
                //}


                using (var db = new AutoSherDBContext())
                {

                    var userid = Convert.ToInt32(Session["UserId"].ToString());

                    var loginhis = db.loginhistory.Where(m => m.wyzuser_id == userid).OrderByDescending(m => m.id).FirstOrDefault();

                    loginhis.logout_time = DateTime.Now;
                    db.loginhistory.AddOrUpdate(loginhis);
                    db.SaveChanges();



                    if (db.Database.Exists())
                    {


                        db.Database.Connection.Close();
                    }
                }

                FormsAuthentication.SignOut();
                Session.Abandon();
                Session.Clear();
                Session.RemoveAll();
               
                if (TempData["Exceptions"] != null)
                {
                    error = TempData["Exceptions"].ToString();
                }

                TempData["ControllerName"] = JsonConvert.SerializeObject(TempData["ControllerName"]);
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("LoginPage", "Home");
        }

        [AuthorizeFilter]
        [ActionName("FollowUpToday")]
        public ActionResult getFollowUpTableDataOfCRE(string id)
        {
            string typeOfDipo = id;
            List<callinteraction> followUpdata = new List<callinteraction>();//repo.getListOfFollowUpOfTodayCRE(folowupDataId);
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string cre = Session["UserName"].ToString();
                    int userId = Convert.ToInt32(Session["UserId"].ToString());
                    wyzuser userdata = new wyzuser();
                    userdata = db.wyzusers.SingleOrDefault(m => m.id == userId);
                    string dealerCode = userdata.dealerCode;
                    List<long> folowupDataId = new List<long>();

                    List<FollowUpNotificationModel> followupdata = new List<FollowUpNotificationModel>();

                    bool role = true;

                    if (typeOfDipo == "insurance")
                    {
                        followupdata = db.Database.SqlQuery<FollowUpNotificationModel>("call insurance_followUp_notifications(@id,@dealerCode);", new MySqlParameter[] { new MySqlParameter("@id", userId), new MySqlParameter("@dealerCode", dealerCode) }).ToList();
                        //followupdata = insurRepo.getFollowupNotificationOfInsu(userdata.getId(), getUserLogindealerCode());
                        role = true;
                        ViewBag.role = role;
                    }
                    else
                    {
                        followupdata = db.Database.SqlQuery<FollowUpNotificationModel>("call FollowUpFilter(@id,@dealerCode);", new MySqlParameter[] { new MySqlParameter("@id", userId), new MySqlParameter("@dealerCode", dealerCode) }).ToList();
                        role = false;
                        ViewBag.role = role;
                    }

                    foreach (FollowUpNotificationModel sa in followupdata)
                    {

                        folowupDataId.Add(sa.callInteraction_id);
                        ViewBag.role = role;

                    }


                    if (folowupDataId != null)
                    {
                        followUpdata = db.callinteractions.Include("vehicle").Include("insurancedispositions").Include("customer").Include("customer.phones").Include("appointmentbooked").Include("servicebooked").Include("srdispositions").Where(m => folowupDataId.Contains(m.id)).ToList();

                    }
                }

                return View(followUpdata);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message.ToString();
                if (ex.Message.Contains("inner exception"))
                {
                    ViewBag.error = ex.InnerException.Message;
                }
            }
            return View(followUpdata);
        }

        [HttpGet]
        [ActionName("changepasswordcre")]
        public ActionResult changePassowrd()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
            return View();
            //return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("changepasswordcre")]
        public ActionResult changePassowrdPost(changePassword passwords)
        {
            int userId = Convert.ToInt32(Session["UserId"].ToString());
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (db.wyzusers.Any(m => m.password == passwords.curPassword && m.id == userId))
                    {
                        wyzuser user = db.wyzusers.SingleOrDefault(m => m.password == passwords.curPassword && m.id == userId);
                        user.password = passwords.newPass;
                        //if (user.role != "Admin" && user.dealerCode == "INDUS" || user.dealerCode == "POPULAR")
                        //{
                        //    user.LatestPasswordUpdatedDate = DateTime.Now;
                        //}
                        db.wyzusers.AddOrUpdate(user);
                        db.SaveChanges();
                    }
                    else
                    {
                        TempData["NotMatch"] = true;
                        ViewBag.error = true;
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("LogOff");
        }


        //Service Notifocation
        [AuthorizeFilter]
        public ActionResult getServiceNotificationToday()
        {
            List<FollowUpNotificationModel> followUps = new List<FollowUpNotificationModel>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    int userId = Convert.ToInt32(Session["UserId"].ToString());
                    wyzuser userdata = new wyzuser();
                    userdata = db.wyzusers.SingleOrDefault(m => m.id == userId);
                    string dealerCode = userdata.dealerCode;

                    followUps = db.Database.SqlQuery<FollowUpNotificationModel>("call FollowUpFilter(@id,@dealercode);", new MySqlParameter[] { new MySqlParameter("@id", userId), new MySqlParameter("@dealercode", dealerCode) }).ToList();

                    return Json(new { success = true, data = followUps });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message.ToString() });
            }
        }
        //Insurance Notifocation
        [AuthorizeFilter]
        public ActionResult getInsuranceNotificationToday()
        {
            List<FollowUpNotificationModel> followUps = new List<FollowUpNotificationModel>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    int userId = Convert.ToInt32(Session["UserId"].ToString());
                    wyzuser userdata = new wyzuser();
                    userdata = db.wyzusers.SingleOrDefault(m => m.id == userId);
                    string dealerCode = userdata.dealerCode;

                    followUps = db.Database.SqlQuery<FollowUpNotificationModel>("call insurance_followUp_notifications(@id,@dealercode);", new MySqlParameter[] { new MySqlParameter("@id", userId), new MySqlParameter("@dealercode", dealerCode) }).ToList();

                    return Json(new { success = true, data = followUps });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message.ToString() });
            }
        }
        //PSF Notification
        [AuthorizeFilter]
        public ActionResult getPSFFollowUpNotificationToday()
        {
            List<PSFFollowupNotificationModel> followUps = new List<PSFFollowupNotificationModel>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    int userId = Convert.ToInt32(Session["UserId"].ToString());
                    string procedureName = "";
                    if (Session["OEM"].ToString() == "MARUTI SUZUKI")
                    {
                        procedureName = "psffollowupnotificationmaruti";
                    }
                    else
                    {
                        procedureName = "psffollowupnotification";
                    }

                    followUps = db.Database.SqlQuery<PSFFollowupNotificationModel>("call " + procedureName + "(@id);", new MySqlParameter("@id", userId)).ToList();
                    return Json(new { success = true, data = followUps });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message.ToString() });
            }
        }

        public ActionResult psfFutureFollowUpToday()
        {
            List<PSFFollowupNotificationModel> followupdata = new List<PSFFollowupNotificationModel>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    int userId = Convert.ToInt32(Session["UserId"].ToString());

                    if (Session["OEM"].ToString() == "MARUTI SUZUKI")
                    {
                        followupdata = db.Database.SqlQuery<PSFFollowupNotificationModel>("call psffollowupnotificationmaruti(@id);", new MySqlParameter[] { new MySqlParameter("@id", userId) }).ToList();
                    }
                    else
                    {
                        followupdata = db.Database.SqlQuery<PSFFollowupNotificationModel>("call psffollowupnotification(@id);", new MySqlParameter[] { new MySqlParameter("@id", userId) }).ToList();

                    }
                }

                return View(followupdata);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message.ToString();
                if (ex.Message.Contains("inner exception"))
                {
                    ViewBag.error = ex.InnerException.Message;
                }
            }
            return View(followupdata);
        }

        [AuthorizeFilter]
        public ActionResult redirectToCallLogging(string id)
        {

            try
            {
                if (id.Contains(','))
                {
                    string pageRequestFor = id.Split(',')[1];
                    Session["inComingParameter"] = null;

                    string userParameter = string.Empty;
                    if (pageRequestFor == "S" || pageRequestFor == "I")
                    {
                        userParameter = id.Split(',')[2];
                        userParameter = userParameter + "," + id.Split(',')[3];

                        Session["inComingParameter"] = id;
                        return RedirectToAction("Call_Logging", "CallLogging", new { id = userParameter });
                    }

                    else if (pageRequestFor == "salesfeedback")
                    {
                        userParameter = id.Split(',')[2];
                        userParameter = userParameter + "," + id.Split(',')[3];

                        Session["inComingParameter"] = id;
                        return RedirectToAction("CallLoggingPostSalesFeedback", "CallLoggingPostSalesFeedback", new { @id = userParameter });

                    }
                    else
                    {
                        Session["inComingParameter"] = id;
                        string dealerCode = Session["DealerCode"].ToString();

                        userParameter = id.Split(',')[1];
                        userParameter = userParameter + "," + id.Split(',')[2];

                        if (dealerCode == "INDUS" || dealerCode == "KATARIA" || dealerCode == "SPEEDAUTO" || dealerCode == "PLATINUMMOTOCORP" || dealerCode == "JAYABHERIAUTO")
                        {
                            return RedirectToAction("CallLogging_IndusPSF", "CallLoggingPSF", new { @id = userParameter });
                            //return RedirectToAction("CallLoggingIndus_PSF", "CallLogging", new { @id= userParameter });
                        }
                        else if (dealerCode == "KALYANIMOTORS" || Session["OEM"].ToString().ToLower() == "hyundai" || dealerCode == "JDAUTONATION" || Session["ISPSFENABLED"].ToString() == "True")
                        {
                            return RedirectToAction("CallLogging_PSF", "CallLoggingPSF", new { @id = userParameter });
                        }
                        else
                        {
                            return RedirectToAction("CallLogging_PSF", "CallLogging", new { @id = userParameter });
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
                return RedirectToAction("LogOff", "Home");
            }
            TempData["Exceptions"] = "Invalid Url Operation....";
            return RedirectToAction("LogOff");
        }
        #region Login firebase
        public async Task<bool> getAuthenticateUserLogin(string DealerCode)
        {
            try
            {
                //dynamic data = new JObject();
                using (var db = new AutoSherDBContext())
                {
                    string baseURL = string.Empty;
                    if (HomeController.wyzCRM1.Contains(DealerCode))
                    {
                        baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_Wyzcrm1"];
                    }
                    else if (HomeController.wyzCRM.Contains(DealerCode))
                    {
                        baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_Wyzcrm"];
                    }
                    else if (HomeController.autosherpa1.Contains(DealerCode))
                    {
                        baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_autosherpa1"];
                    }
                    else if (HomeController.WyzCrmNew.Contains(DealerCode))
                    {
                        baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_wyzNew"];
                    }
                    else if (HomeController.autosherpa3.Contains(DealerCode))
                    {
                        baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_autosherpa3"];
                    }
                    var firebaseBaseURL = new FirebaseClient(baseURL);
                    string Url = DealerCode + "_Authentication/";

                    var firebasevalue = await firebaseBaseURL.Child(Url).OnceAsync<FireBaseAuthentication>();
                    FireBaseAuthentication authentic = (from x in firebasevalue select x.Object).FirstOrDefault();
                    if (authentic != null && authentic.userLogin == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                string exception;

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
                // return Json(new { success = false, exception });
                return false;
            }

        }

        #endregion

        #region Driver App details
        [ActionName("driverlocation")]
        public ActionResult driverlocation(string id)
        {
            try
            {

                long customerId = 0;
                long vehicleId = 0;

                if (!string.IsNullOrEmpty(id))
                {
                    vehicleId = long.Parse(id.Split('_')[0]);
                    customerId = long.Parse(id.Split('_')[1]);
                }
                using (var db = new AutoSherDBContext())
                {
                    DriversDeliveryNotes deliveryNote = db.driversDeliveryNotes.Where(m => m.VehicleId == vehicleId && m.CustomerId == customerId && m.IsMap == true).OrderByDescending(m => m.Id).FirstOrDefault();

                    if (deliveryNote != null)
                    {
                        string vehicleRegNo = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehicleId).vehicleRegNo;
                        string driverName = db.drivers.FirstOrDefault(m => m.id == db.driverSchedulers.FirstOrDefault(u => u.id == deliveryNote.DriverSchedulerId && m.isactive == true).driver_id).driverName;
                        string dealerName = db.dealers.FirstOrDefault().dealerCode;
                        ViewBag.VehRegNo = vehicleRegNo;
                        ViewBag.DriverName = driverName;
                        ViewBag.DealerCode = dealerName;
                        ViewBag.DeliveryNoteId = deliveryNote.Id;
                        ViewBag.Allow = "Allow";
                    }
                    else
                    {
                        ViewBag.Result = "Invalid Request or Session/Link Expired";
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
                if (ex.StackTrace.Contains(':'))
                {
                    exception = "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)] + " " + exception;
                }
                ViewBag.error = exception;
            }

            return View();
        }
        [ActionName("driverdeliveryImage")]
        public ActionResult driverdeliveryImage(string id)
        {
            driverImageVM fileDetails = new driverImageVM();
            try
            {

                long driverschedulerId = 0;

                if (!string.IsNullOrEmpty(id))
                {
                    driverschedulerId = long.Parse(id);
                }
                using (var db = new AutoSherDBContext())
                {
                    var driverscheulerdetails = db.driverSchedulers.Where(m => m.id == driverschedulerId).Select(m => new { m.driverBookingdetails_id, m.driver_id, m.vehicle_id, m.customer_id }).FirstOrDefault();
                    if (driverscheulerdetails != null)
                    {
                        fileDetails.driverAppFileDetails = db.driverAppFileDetails.Where(m => m.DriverScheduler_Id == driverschedulerId).OrderByDescending(m => m.Id).Distinct().ToList();

                        fileDetails.vehicleRegNo = db.vehicles.FirstOrDefault(m => m.vehicle_id == driverscheulerdetails.vehicle_id).vehicleRegNo;

                        var appInteractions = db.driverAppInteraction.Where(m => m.DriverScheduler_Id == driverschedulerId).ToList();
                        if (appInteractions.Count(m => m.IsPickUp && m.InteractionType == "PickUp Reached") > 0)
                        {
                            fileDetails.PickupTime = appInteractions.Where(m => m.IsPickUp && m.InteractionType == "PickUp Reached").OrderByDescending(m => m.Id).FirstOrDefault().PickupDateTime;
                        }
                        if (appInteractions.Count(m => m.IsPickUp && m.InteractionType == "PickUp Completed") > 0)
                        {
                            fileDetails.pickupstartedlocation = appInteractions.Where(m => m.IsPickUp && m.InteractionType == "PickUp Completed").OrderByDescending(m => m.Id).FirstOrDefault().startlocation;
                            fileDetails.pickupstopedlocation = appInteractions.Where(m => m.IsPickUp && m.InteractionType == "PickUp Completed").OrderByDescending(m => m.Id).FirstOrDefault().stoplocation;
                            fileDetails.pickupkmtravelled = appInteractions.Where(m => m.IsPickUp && m.InteractionType == "PickUp Completed").OrderByDescending(m => m.Id).FirstOrDefault().kmtravelled;
                        }
                        if (appInteractions.Count(m => m.IsDrop && m.InteractionType == "Drop Completed") > 0)
                        {
                            fileDetails.dropstartedlocation = appInteractions.Where(m => m.IsDrop && m.InteractionType == "Drop Completed").OrderByDescending(m => m.Id).FirstOrDefault().startlocation;
                            fileDetails.dropstopedlocation = appInteractions.Where(m => m.IsDrop && m.InteractionType == "Drop Completed").OrderByDescending(m => m.Id).FirstOrDefault().stoplocation;
                            fileDetails.DeliveryTime = appInteractions.Where(m => m.IsDrop && m.InteractionType == "Drop Completed").OrderByDescending(m => m.Id).FirstOrDefault().DeliveryDateTime;
                            fileDetails.dropkmtravelled = appInteractions.Where(m => m.IsDrop && m.InteractionType == "Drop Completed").OrderByDescending(m => m.Id).FirstOrDefault().kmtravelled;
                        }
                        if (fileDetails.PickupTime == null && fileDetails.pickupstartedlocation == null && fileDetails.pickupstopedlocation == null && fileDetails.pickupkmtravelled == null)
                        {
                            ViewBag.pickupdrop = "Pickuphide";
                        }
                        if (fileDetails.dropstartedlocation == null && fileDetails.dropstopedlocation == null && fileDetails.DeliveryTime == null && fileDetails.dropkmtravelled == null)
                        {
                            ViewBag.pickupdrop = "Drophide";
                        }
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
                if (ex.StackTrace.Contains(':'))
                {
                    exception = "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)] + " " + exception;
                }
                ViewBag.error = exception;
            }

            return View(fileDetails);
        }


        public async Task<ActionResult> getLoc(string DriverName, string DealerCode, string VehRegNo, long NoteId)
        {
            try
            {
                dynamic data = new JObject();
                using (var db = new AutoSherDBContext())
                {
                    if (db.driversDeliveryNotes.Count(m => m.Id == NoteId) == 1)
                    {
                        var firebaseBaseURL = new FirebaseClient("https://wyznew-30c55.firebaseio.com/");
                        string Url = DealerCode + "/Driver/ScheduledCalls/" + DriverName + "/LocationInfo/" + VehRegNo + "/";

                        var firebasevalue = await firebaseBaseURL.Child(Url).OnceAsync<string>();
                        string LocationInfo = (from x in firebasevalue select x.Object).FirstOrDefault();

                        if (LocationInfo != null && LocationInfo.ToString().Contains(","))
                        {
                            return Json(new { success = true, lnt = LocationInfo.ToString().Split(',')[0], lng = LocationInfo.ToString().Split(',')[1] });
                        }
                        else
                        {
                            return Json(new { success = false, exception = "Location Not Found..." });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, exception = "Link Expired...." });
                    }

                }
            }
            catch (Exception ex)
            {
                string exception;

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
                return Json(new { success = false, exception });
            }
        }

        #endregion
        #region user log book
        public async Task recordLogDetails(int operation, string username, string managername, string dealercode, long wyzuserId, string curSession)
        {
            try
            {

                if (operation == 1)//Login
                {
                    userlogs user = new userlogs();

                    user.wyzuserId = wyzuserId;
                    user.username = username;
                    user.managername = managername;
                    user.dealerCode = dealercode;
                    user.loginDateTime = DateTime.Now;
                    user.sessionid = curSession;
                    user.hostaddress = Request.UserHostAddress;
                    using (var db = new AutoSherDBContext())
                    {
                        db.userlogs.Add(user);
                        db.SaveChanges();
                    }
                }
                else if (operation == 2)//LogOut
                {
                    using (var db = new AutoSherDBContext())
                    {
                        userlogs user = new userlogs();
                        if (db.userlogs.Any(m => m.sessionid == curSession && m.logoutDateTime == null))
                        {
                            user = db.userlogs.FirstOrDefault(m => m.sessionid == curSession && m.logoutDateTime == null);
                            user.logoutDateTime = DateTime.Now;
                            db.userlogs.AddOrUpdate(user);
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }

    public class changePassword
    {
        [Required]
        public string curPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string newPass { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string confirmPass { get; set; }
    }
    public class FireBaseAuthentication
    {
        public int userLogin { get; set; }
    }

    //public class driverImageVM
    //{
    //    public List<DriverAppFileDetails> driverAppFileDetails { get; set; }
    //    public string paths { get; set; }
    //    public string vehicleRegNo { get; set; }
    //    public string PickupTime { get; set; }
    //    public string DeliveryTime { get; set; }
    //    public string pickupstartedlocation { get; set; }
    //    public string dropstartedlocation { get; set; }
    //    public string pickupstopedlocation { get; set; }
    //    public string dropstopedlocation { get; set; }
    //    public string dropkmtravelled { get; set; }
    //    public string pickupkmtravelled { get; set; }
    //}
}
