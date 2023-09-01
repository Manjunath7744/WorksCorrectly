using AutoSherpa_project.Models;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Newtonsoft.Json;
using NLog;
using Renci.SshNet.Messages.Authentication;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AutoSherpa_project.Controllers
{
    [AuthorizeFilter]
    public class NavigationTabController : Controller
    {
        // GET: Navigation
        [HttpGet]
        [ActionName("ManageNavigation")]
        public ActionResult NavigationGet(int? id)
        {

            if(id==null || id!=03)
            {
                ViewBag.Grant = "No";
            }
            else if(id==03)
            {
                ViewBag.Grant = "Yes";
            }

            return View();
        }


        public ActionResult getCreNames(string moduleType, string roleType)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    var creList = db.wyzusers.Where(m => m.role == roleType && m.role1 == moduleType).Select(m => new { id = m.id, name = m.userName }).ToList();
                    return Json(new { success = true, data = creList.OrderBy(m => m.name) });
                }

            }
            catch (Exception ex)
            {
                string exception="";

                if(ex.InnerException!=null)
                {
                    if(ex.InnerException.InnerException!=null)
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

        public ActionResult getNavigationDetails(string creId, string roleType, string moduleType, bool isCkAll)
        {
            try
            {
                long id = Convert.ToInt64(creId);

                using (var db = new AutoSherDBContext())
                {
                    navigationaccess userNavs = db.navaccess.FirstOrDefault(m => m.wyzuser_id == id);
                    string userforms = ""; 
                    bool isActive = false;

                    if(userNavs!=null)
                    {
                        userforms = userNavs.form_id;
                        isActive = userNavs.isactive;
                    }
                    
                    if (isCkAll==true)
                    {
                        List<formmapping> forms = getNavForms(moduleType, roleType, true,null,false);


                        return Json(new { success = true, forms, userforms,isActive });
                    }
                    else
                    {
                        List<formmapping> forms = new List<formmapping>();

                        if(string.IsNullOrEmpty(userforms))
                        {
                            forms = getNavForms(moduleType, roleType, false, null,false);
                        }
                        else
                        {
                            forms = getNavForms(moduleType, roleType, false, userforms, true);
                        }

                        

                        return Json(new { success = true, forms, userforms,isActive });
                    }
                }
            }
            catch (Exception ex)
            {
                string exception="";

                if(ex.InnerException!=null)
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

        public List<formmapping> getNavForms(string moduleType,string roleType,bool isCkAll,string form_ids,bool needExtraforms)
        {
            List<formmapping> forms =new List<formmapping>(); ;
            bool sendSelectedForms = false;

            using (var db = new AutoSherDBContext())
            {
                if (isCkAll == true)
                {
                    forms = db.form_mapping.ToList();
                }
                else
                {
                    if(string.IsNullOrEmpty(form_ids) || needExtraforms == true)
                    {
                        if (roleType == "CRE")
                        {
                            if (moduleType == "1")
                            {
                                forms = db.form_mapping.Where(m => m.user_role.Contains("CRE") && (m.module_type == "1" || m.module_type == "5" || m.module_type == "1,4" || m.module_type == "1,2") && m.isactive==true).OrderBy(m => m.displaying_order).ToList();
                            }
                            else if (moduleType == "2")
                            {
                                forms = db.form_mapping.Where(m => m.user_role.Contains("CRE") && (m.module_type == "2" || m.module_type == "5" || m.module_type == "1,2") && m.isactive == true).OrderBy(m => m.displaying_order).ToList();
                            }
                            else if (moduleType == "4")
                            {
                                forms = db.form_mapping.Where(m => m.user_role.Contains("CRE") && (m.module_type == "4" || m.module_type == "5" || m.module_type == "1,4") && m.isactive == true).OrderBy(m => m.displaying_order).ToList();
                            }
                            else if (moduleType == "5")
                            {
                                forms = db.form_mapping.Where(m => m.user_role.Contains("CRE") && (m.module_type == "5" || m.module_type == "6") && m.isactive == true).OrderBy(m => m.displaying_order).ToList();
                            }
                        }
                        else if (roleType == "CREManager")
                        {
                            if (moduleType == "1" || moduleType == "4")
                            {
                                forms = db.form_mapping.Where(m => (m.user_role.Contains("Manager") && m.user_role != "Driver_Manager" && m.user_role != "FE_Manager") && (m.module_type == "1" || m.module_type == "5" || m.module_type == "1,4") && m.isactive == true).OrderBy(m => m.displaying_order).ToList();
                            }
                            else if (moduleType == "2")
                            {
                                forms = db.form_mapping.Where(m => (m.user_role.Contains("Manager") && m.user_role != "Driver_Manager" && m.user_role != "FE_Manager") && (m.module_type == "2" || m.module_type == "5" || m.module_type == "1,2") && m.isactive == true).OrderBy(m => m.displaying_order).ToList();
                            }
                        }
                        else if (roleType == "RM")
                        {
                            if (moduleType == "1" || moduleType == "4")
                            {
                                forms = db.form_mapping.Where(m => m.user_role.Contains("RM") && (m.module_type.Contains("1") || m.module_type == "5") && m.isactive == true).OrderBy(m => m.displaying_order).ToList();
                            }
                            else if (moduleType == "2")
                            {
                                forms = db.form_mapping.Where(m => m.user_role.Contains("RM") && (m.module_type.Contains("2") || m.module_type == "5") && m.isactive == true).OrderBy(m => m.displaying_order).ToList();
                            }
                        }
                        else if (roleType == "WM")
                        {
                            if (moduleType == "1" || moduleType == "4")
                            {
                                forms = db.form_mapping.Where(m => m.user_role.Contains("WM") && (m.module_type.Contains("1") || m.module_type == "5") && m.isactive == true).OrderBy(m => m.displaying_order).ToList();
                            }
                            else if (moduleType == "2")
                            {
                                forms = db.form_mapping.Where(m => m.user_role.Contains("WM") && (m.module_type.Contains("2") || m.module_type == "5") && m.isactive == true).OrderBy(m => m.displaying_order).ToList();
                            }
                        }
                        else if (roleType == "Admin")
                        {
                            if (moduleType == "1" || moduleType == "4")
                            {
                                forms = db.form_mapping.Where(m => m.user_role.StartsWith("Admin") && (m.module_type.Contains("1") || m.module_type == "5") && m.isactive == true).OrderBy(m => m.displaying_order).ToList();
                            }
                            else if (moduleType == "2")
                            {
                                forms = db.form_mapping.Where(m => m.user_role.StartsWith("Admin") && (m.module_type.Contains("2") || m.module_type == "5") && m.isactive == true).OrderBy(m => m.displaying_order).ToList();
                            }
                        }
                        else if (roleType == "SuperAdmin")
                        {
                            if (moduleType == "1" || moduleType == "4")
                            {
                                forms = db.form_mapping.Where(m => m.user_role.Contains("SuperAdmin") && (m.module_type.Contains("1") || m.module_type == "5") && m.isactive == true).OrderBy(m => m.displaying_order).ToList();
                            }
                            else if (moduleType == "2")
                            {
                                forms = db.form_mapping.Where(m => m.user_role.Contains("SuperAdmin") && (m.module_type.Contains("2") || m.module_type == "5") && m.isactive == true).OrderBy(m => m.displaying_order).ToList();
                            }
                        }

                        if (roleType == "Service Advisor")
                        {
                            if (moduleType == "1")
                            {
                                forms = db.form_mapping.Where(m => m.user_role.Contains("Service Advisor") && (m.module_type == "1" || m.module_type == "5" || m.module_type == "1,4" || m.module_type == "1,2") && m.isactive == true).OrderBy(m => m.displaying_order).ToList();
                            }
                        }
                    }
                    else
                    {
                        sendSelectedForms = true;
                    }


                    if(sendSelectedForms == true || needExtraforms == true)
                    {
                        List<int> formIds = form_ids.Split(',').Select(int.Parse).ToList();
                        List<formmapping> additionForms = new List<formmapping>();
                       
                            additionForms = db.form_mapping.Where(m => formIds.Contains(m.id)).ToList();

                            List<int> mainForm_id = additionForms.Select(x => x.mainform_id).ToList();
                            List<formmapping> formsHeader = db.form_mapping.Where(m => mainForm_id.Contains(m.id)).ToList();
                            if (formsHeader != null)
                            {
                                additionForms.AddRange(formsHeader);
                            }
                            if (additionForms != null)
                            {
                                foreach (var nav in additionForms)
                                {
                                    if (forms.Count(m => m.id == nav.id) == 0)
                                    {
                                        forms.Add(nav);
                                    }
                                }

                                //forms.AddRange(additionForms);
                                forms = forms.OrderBy(m => m.displaying_order).ToList();
                            }
                    }
                }
            }

            return forms;
        }

        public ActionResult manageNavigationDetails(string creId, string roleType, string selected_navs,bool active)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {
                int userId = Convert.ToInt32(creId);
                //string HostMachineName = string.Empty;
                //try
                //{
                //    string IP = Request.UserHostName;
                //    logger.Info("IP Address is: " + IP);
                //    IPAddress myIP = IPAddress.Parse(IP);
                //    //logger.Info("IP Address is: " + JsonConvert.SerializeObject(myIP) );
                //    IPHostEntry GetIPHost = Dns.GetHostEntry(IP);
                //    //List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
                //    string HostAddress = JsonConvert.SerializeObject(GetIPHost.AddressList);
                //    HostMachineName = GetIPHost.HostName + IP + HostAddress;
                //}
                //catch (Exception ex)
                //{
                //    HostMachineName = ex.Message;

                //    if(ex.StackTrace.Contains(":"))
                //    {
                //        HostMachineName = HostMachineName + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)];
                //    }
                //}

                using (var db = new AutoSherDBContext())
                {
                    if (db.navaccess.Any(m => m.wyzuser_id == userId))
                    {
                        navigationaccess navs = db.navaccess.FirstOrDefault(m => m.wyzuser_id == userId);
                        navs.form_id = selected_navs;
                        navs.isactive = active;
                        navs.UpdatedBy = Session["UserName"].ToString()+"-"+Request.UserHostAddress;
                        navs.UpdatedOn = DateTime.Now;
                        db.navaccess.AddOrUpdate(navs);
                        db.SaveChanges();
                    }
                    else
                    {
                        navigationaccess navs = new navigationaccess();
                        navs.wyzuser_id = userId;
                        navs.role = roleType;
                        navs.form_id = selected_navs;
                        navs.isactive = true;
                        navs.UpdatedBy = Session["UserName"].ToString() + "-"+ Request.UserHostAddress;
                        navs.UpdatedOn = DateTime.Now;
                        db.navaccess.Add(navs);
                        db.SaveChanges();
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

        [HttpGet,ActionName("ManageAppForms")]
        public ActionResult ManageAppForms()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    ViewBag.formsname = db.form_mapping.Select(m => new {id=m.id, name = m.form_name }).Distinct().ToList();
                }
            }
            catch(Exception ex)
            {

            }

            return View();
        }

        public ActionResult getFontByForm(int formId)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string fontIcons = db.form_mapping.FirstOrDefault(m => m.id == formId).font_icon;

                    return Json(new { success = true, fontIcons });
                }
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

        public ActionResult updateFontIcons(string newFont, int id)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    long userId = long.Parse(Session["UserId"].ToString());

                    formmapping forms = db.form_mapping.FirstOrDefault(m => m.id==id);
                    if(forms!=null)
                    {
                        forms.font_icon = newFont;
                        forms.updatedby = userId;
                        forms.updatedon = DateTime.Now;
                        db.form_mapping.AddOrUpdate(forms);
                        db.SaveChanges();

                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, exception="No forms found" });
                    }
                }
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
    }
}
