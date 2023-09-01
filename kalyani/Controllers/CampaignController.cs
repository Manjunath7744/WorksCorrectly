using AutoSherpa_project.Models;
using AutoSherpa_project.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AutoSherpa_project.Controllers
{
    [AuthorizeFilter]
    public class CampaignController : Controller
    {
        // GET: Campaign
        [HttpGet]
        [ActionName("Campaign")]
        public ActionResult CampaignGet()
        {
            return View();
        }
        
        public ActionResult CampaignPost(campaign campaignMod)
        {
            try
            {
                using (AutoSherDBContext dBContext = new AutoSherDBContext())
                {
                    if (dBContext.campaigns.Where(m => m.campaignName.ToLower() == campaignMod.campaignName.Trim().ToLower()).Count() == 0)
                    {
                        campaignMod.isactive = true;
                        campaignMod.isValid = true;
                        campaignMod.campaignName=campaignMod.campaignName.Trim();
                        dBContext.campaigns.Add(campaignMod);
                        dBContext.SaveChanges();
                    }
                    else
                    {
                        return Json(new { success = true, message = "This campaign name already exists in the database,please provide some other campaign name." });
                    }
                    return Json(new { success = true, message= "Submitted successfully!" });
                }
            }
            catch(Exception ex)
            {
                return Json(new { success = false,error=ex.Message });
            }
        }

        public ActionResult GetCampaign()
        {
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    if(Session["UserRole"].ToString() == "SuperAdmin" )
                    {
                        var campaignList = db.campaigns.Select(m => new {m.id, m.campaignName, m.campaignType, m.isactive }).ToList();
                        return Json(new { data = campaignList }, JsonRequestBehavior.AllowGet);
                    }
                    else if(Session["UserRole"].ToString() == "CREManager" || Session["UserRole"].ToString() == "Admin")
                    {
                        if (Session["UserRole1"].ToString() == "2")
                        {
                            var campaignList = db.campaigns.Where(m => m.campaignType == "Insurance" || m.campaignType == "TaggingINS").Select(m => new {m.id, m.campaignName, m.campaignType, m.isactive }).ToList();
                            return Json(new { data = campaignList }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            var campaignList = db.campaigns.Where(m => m.campaignType == "PSF" || m.campaignType == "TaggingSMR" || m.campaignType == "Campaign").Select(m => new {m.id, m.campaignName, m.campaignType, m.isactive }).ToList();
                            return Json(new { data = campaignList }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    else
                    {
                        return Json(new { data = "" }, JsonRequestBehavior.AllowGet);

                    }

                }
            }
            catch(Exception ex)
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

        public ActionResult UpdateActivatedeactivate(int? id, string value)
        {
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                    {
                       var active = db.campaigns.FirstOrDefault(m => m.id == id);
                        if (value == "is_active")
                        {
                            if (active.isactive == false)
                            {
                                active.isactive = true;
                            }
                            else
                            {
                                active.isactive = false;
                            }
                        }
                        
                        else
                        {

                        }
                        db.campaigns.AddOrUpdate(active);
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
        }

    
}