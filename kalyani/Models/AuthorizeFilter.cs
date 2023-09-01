using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoSherpa_project.Models
{
    public class AuthorizeFilter : AuthorizeAttribute
    {

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                if (HttpContext.Current.Session["UserName"] != null && HttpContext.Current.Session["UserId"] != null)
                {
                    //base.OnAuthorization(filterContext);
                }
                else
                {
                    string siteRoot = HttpRuntime.AppDomainAppVirtualPath;
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new JsonResult { Data = new { status = "401" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                        filterContext.HttpContext.Response.StatusCode = 401;
                        return;
                    }
                    if (siteRoot == "/")
                    {
                        filterContext.Result = new RedirectResult("/Home/LogInPage");
                    }
                    else
                    {
                        filterContext.Result = new RedirectResult(siteRoot + "/Home/LogInPage");
                    }

                }
            }
            catch
            {

            }

        }
        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    try
        //    {
        //        if (HttpContext.Current.Session["UserName"] != null && HttpContext.Current.Session["UserId"] != null)
        //        {
        //            //base.OnAuthorization(filterContext);
        //        }
        //        else
        //        {
        //            string siteRoot = HttpRuntime.AppDomainAppVirtualPath;
        //            if (siteRoot == "/")
        //            {
        //                filterContext.Result = new RedirectResult("/Home/Index");
        //            }
        //            else
        //            {
        //                filterContext.Result = new RedirectResult(siteRoot + "/Home/Index");
        //            }

        //        }
        //    }
        //    catch
        //    {

        //    }

        //}
    }
}