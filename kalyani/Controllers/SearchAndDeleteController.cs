using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoSherpa_project.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace AutoSherpa_project.Controllers
{
    public class SearchAndDeleteController : Controller
    {
        // GET: SearchAndDelete
        public ActionResult SearchAndDelete()
        {
            return View();
        }
        public ActionResult searchData(string value)
        {
            try
            {
                using(AutoSherDBContext db=new AutoSherDBContext())
                {
                    db.Database.CommandTimeout = 900;
                    
                    var data = (from veh in db.vehicles where (veh.chassisNo == value || veh.vehicleRegNo== value)
                                join cus in db.customers on veh.customer_id equals cus.id
                                join phone in db.phones on veh.customer_id equals phone.customer_id
                                where phone.isPreferredPhone == true
                                join addr in db.addresses on veh.customer_id equals addr.customer_Id where addr.isPreferred==true
                                
                                select new
                                {
                                    customerName = cus.customerName,
                                    phone = phone.phoneNumber,
                                    address = addr.concatenatedAdress,
                                    vehRegNo = veh.vehicleRegNo,
                                    chassisNo = veh.chassisNo
                                }
                              ).Take(1).ToList();
                    return Json(new { data = data, draw = Request["draw"] });

                }
            }catch(Exception ex)
            {

            }
            return View();
        }
        public ActionResult deleteCustomer(string value)
        {
            try
            {
                using (AutoSherDBContext db = new AutoSherDBContext())
                {
                      db.Database.CommandTimeout = 200;
                    //  string str = @"call deletevehiclefromdatabase(@inchassis);";
                    //int i= db.Database.ExecuteSqlCommand(str, new MySqlParameter("@inchassis", value));

                    string conStr = ConfigurationManager.ConnectionStrings["AutoSherContext"].ConnectionString;
                    string str = @"call deletevehiclefromdatabase(@inchassis);";
                    MySqlConnection con = new MySqlConnection(conStr);
                    con.Open();
              //      con.ConnectionTimeout = 900;
                    MySqlCommand cmd = new MySqlCommand(str,con);
                    cmd.Parameters.AddWithValue("@inchassis", value);
                    cmd.CommandTimeout = 900;
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }
    }
    
}