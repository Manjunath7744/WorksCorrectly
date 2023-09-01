using AutoSherpa_project.Models;
using AutoSherpa_project.Models.ViewModels;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoSherpa_project.Controllers
{
    public class mcpispewController : Controller
    {
        // GET: mcpispew
        public ActionResult mcpispew()
        {
            return View();
        }
        public ActionResult getmcpispewDetails(string servicemcpispewData)
        {
            long buckedId=0,isp=0,mcp=0,ew=0,loyalty=0,bsc=0;
            string exception = "", fueltype=string.Empty,micoupon=string.Empty;
            string ispfrom, ispto, mcpfrom, mcpto, ewfrom, ewto,loyaltyfrom,loyaltyto;
            List<mcapispewcallLogs> mcapispewLogs = null;

            int fromIndex = Convert.ToInt32(Request["start"]);
            int toIndex = Convert.ToInt32(Request["length"]);
            string searchPattern = Request["search[value]"];



            long UserId = Convert.ToInt64(Session["UserId"].ToString());

            mcpispewfilter filter = new mcpispewfilter();
            if (servicemcpispewData != null)
            {
                filter = JsonConvert.DeserializeObject<mcpispewfilter>(servicemcpispewData);
            }
            filter.isFiltered = true;

            int totalCount = 0;
            int patternCount = 0;
            buckedId = filter.bucketcalls == null || filter.bucketcalls == "" ? 0 : Convert.ToInt64(filter.bucketcalls);
            if(filter.typeofbucket==null || filter.typeofbucket=="" || filter.typeofbucket=="All")
            {
                isp = mcp = ew =loyalty=bsc= 0;
            }
            else if(filter.typeofbucket== "MCP") 
            {
                mcp = 1;
                isp =  ew =bsc= loyalty = 0;
            }
            else if(filter.typeofbucket== "ISP") 
            {
                isp = 1;
                 mcp = ew =loyalty=bsc=0;
            }
            else if(filter.typeofbucket== "EW") 
            {
                ew = 1;
                isp = mcp =loyalty= bsc= 0;
            }
            else if(filter.typeofbucket== "Loyalty") 
            {
                loyalty = 1;
                isp = mcp =ew= bsc= 0;
            }
            else if(filter.typeofbucket== "BSC") 
            {
                bsc = 1;
                isp = mcp =ew=loyalty= 0;
            }
            else if(filter.typeofbucket== "Petrol" || filter.typeofbucket== "Diesel")
            {
                bsc =isp = mcp = ew = loyalty = 0;
                fueltype = filter.typeofbucket;
            }
            else if(filter.typeofbucket== "micoupon")
            {
                bsc =isp = mcp = ew = loyalty = 0;
                micoupon = filter.typeofbucket;
            }
            ispfrom = filter.ispfrom == null ? "" : Convert.ToDateTime(filter.ispfrom.ToString()).ToString("yyyy-MM-dd");
            ispto = filter.ispto == null ? "" : Convert.ToDateTime(filter.ispto.ToString()).ToString("yyyy-MM-dd");
            mcpfrom = filter.mcpfrom == null ? "" : Convert.ToDateTime(filter.mcpfrom.ToString()).ToString("yyyy-MM-dd");
            mcpto = filter.mcpto == null ? "" : Convert.ToDateTime(filter.mcpto.ToString()).ToString("yyyy-MM-dd");
            ewfrom = filter.ewfrom == null ? "" : Convert.ToDateTime(filter.ewfrom.ToString()).ToString("yyyy-MM-dd");
            ewto = filter.ewto == null ? "" : Convert.ToDateTime(filter.ewto.ToString()).ToString("yyyy-MM-dd");
            loyaltyfrom = filter.loyaltyfrom == null ? "" : Convert.ToDateTime(filter.loyaltyfrom.ToString()).ToString("yyyy-MM-dd");
            loyaltyto = filter.loyaltyto == null ? "" : Convert.ToDateTime(filter.loyaltyto.ToString()).ToString("yyyy-MM-dd");
            if (searchPattern != "")
            {
                filter.isFiltered = true;
            }

            using (var db = new AutoSherDBContext())
            {
                try
                {
                    totalCount = mcpispewDetails(buckedId, UserId, isp, mcp, ew,loyalty,bsc,micoupon, fueltype, "", "", "", "", "", "","","","",0, 10000000).Count;


                    if (toIndex < 0)
                        {
                            toIndex = 10;
                        }

                        if (toIndex > totalCount)
                        {
                            toIndex = totalCount;
                        }
                    mcapispewLogs = mcpispewDetails(buckedId, UserId, isp, mcp, ew,loyalty,bsc, micoupon, fueltype, ispfrom, ispto, mcpfrom, mcpto, ewfrom, ewto,loyaltyfrom,loyaltyto,searchPattern, fromIndex, toIndex);
                    if (filter.isFiltered == true)
                    {
                        patternCount = mcpispewDetails(buckedId, UserId, isp, mcp, ew,loyalty,bsc, micoupon,fueltype, ispfrom, ispto, mcpfrom, mcpto, ewfrom, ewto, loyaltyfrom, loyaltyto, searchPattern, fromIndex, totalCount).Count;
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
            }

            if (mcapispewLogs != null)
            {
                if (filter.isFiltered == true)
                {
                    var JsonData = Json(new { data = mcapispewLogs, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = patternCount, exception = exception }, JsonRequestBehavior.AllowGet);
                    JsonData.MaxJsonLength = int.MaxValue;
                    return JsonData;
                }
                else if (filter.isFiltered == false)
                {
                    var JsonData = Json(new { data = mcapispewLogs, draw = Request["draw"], recordsTotal = totalCount, recordsFiltered = totalCount, exception = exception }, JsonRequestBehavior.AllowGet);
                    JsonData.MaxJsonLength = int.MaxValue;
                    return JsonData;
                }
            }
            return Json(new { data = "", draw = Request["draw"], recordsTotal = 0, recordsFiltered = 0, exception = exception }, JsonRequestBehavior.AllowGet);
        }


        #region Fresh Bucket

        public List<mcapispewcallLogs> mcpispewDetails(long bucketid, long inwyzuserid,long isp, long mcp, long ew,long loyalty,long bsc,string micoupon,string fueltype, string inispfrom, string inispto, string inmcpfrom, string inmcpto, string inewfrom, string inewto,string loyaltyfrom,string loyaltyto, string pattern, long start_with, long length)
        {
            List<mcapispewcallLogs> serviceRemainers = null;

            using (var db = new AutoSherDBContext())
            {
                string str = @"call mcp_isp_ew_procedure(@bucketid,@inwyzuserid,@isp,@mcp,@ew,@loayalty,@bsc,@inmicoupon,@infueltype,@inispfrom,@inispto,@inmcpfrom,@inmcpto,@inewfrom,@inewto,@loyaltyfrom,@loyaltyto,@pattern,@start_with,@length);";

                MySqlParameter[] sqlParameter = new MySqlParameter[]
                {
                        new MySqlParameter("@bucketid", bucketid),
                        new MySqlParameter("@inwyzuserid", inwyzuserid),
                        new MySqlParameter("@isp", isp),
                        new MySqlParameter("@mcp", mcp),
                        new MySqlParameter("@ew", ew),
                        new MySqlParameter("@loayalty",loyalty),
                        new MySqlParameter("@bsc", bsc),
                        new MySqlParameter("@inmicoupon", micoupon),
                        new MySqlParameter("@infueltype", fueltype),
                        new MySqlParameter("@inispfrom", inispfrom),
                        new MySqlParameter("@inispto", inispto),
                        new MySqlParameter("@inmcpfrom", inmcpfrom),
                        new MySqlParameter("@inmcpto", inmcpto),
                        new MySqlParameter("@inewfrom", inewfrom),
                        new MySqlParameter("@inewto", inewto),
                        new MySqlParameter("@loyaltyfrom", loyaltyfrom),
                        new MySqlParameter("@loyaltyto", loyaltyto),
                        new MySqlParameter("@pattern", pattern),
                        new MySqlParameter("@start_with", start_with),
                        new MySqlParameter("@length", length)
                };
                serviceRemainers = db.Database.SqlQuery<mcapispewcallLogs>(str, sqlParameter).ToList();
            }
            return serviceRemainers;
        }

        #endregion
    }

    public class mcpispewfilter
    {
        public string bucketcalls { get; set; }
        public string typeofbucket { get; set; }
        public string ispfrom { get; set; }
        public string ispto { get; set; }
        public string mcpfrom { get; set; }
        public string mcpto { get; set; }
        public string ewfrom { get; set; }
        public string ewto { get; set; }
        public string loyaltyfrom { get; set; }
        public string loyaltyto { get; set; }
        public bool isFiltered { get; set; }
    }
    public class mcapispewcallLogs
    {
        public DateTime? ispDate { get; set; }
        public DateTime? mcpdate { get; set; }
        public DateTime? OEMWarrentyDate { get; set; }
        public string vehicle_id { get; set; }
        public string customer_id { get; set; }
        public string assignid { get; set; }
        public DateTime? nextServiceDate { get; set; }
        public string nextServiceTypeId { get; set; }
        public string nextServiceType { get; set; }
        public string customername { get; set; }
        public string chassisno { get; set; }
        public string vehicleRegno { get; set; }
        public string lastdispo { get; set; }
        public string bucket { get; set; }
        public string followUpDate { get; set; }
        public string followUpTime { get; set; }
        public string scheduleDate { get; set; }
        public string scheduleTime { get; set; }
    }
}