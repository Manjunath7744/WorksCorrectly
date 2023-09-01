using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoSherpa_project.Models;
using AutoSherpa_project.Models.Schedulers;
using AutoSherpa_project.Models.ViewModels;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Quartz;
using Quartz.Impl;
//using NPOI.SS.Util;

namespace AutoSherpa_project.Controllers
{
	public class InsuranceOutBoundController : Controller
	{
		// GET: InsuranceOutBound
		public ActionResult Index()
		{
			return View();
		}
		/******************* Calculator *********************************/
		public ActionResult insuranceQuote()
		{
			try
			{
				using (var db = new AutoSherDBContext())
				{
					db.Configuration.LazyLoadingEnabled = false;

					var vehTypZone = db.irda_od_premium.Select(m=>m.zone).Distinct().ToList(); 
					return Json(new {  vehTypZone });
				}
			}
			catch (Exception ex)
			{

			}
			return View();
		}
		public ActionResult insuranceCompanyList()
		{
			try
			{
				using (var db = new AutoSherDBContext())
				{
					db.Configuration.LazyLoadingEnabled = false;

					List<string> insCompanyList = db.insurancecompanies.Select(m=>m.companyName).Distinct().ToList();
					return Json(new { success = true, insData = insCompanyList });
				}
			}
			catch(Exception ex)
			{
			}

			return View();
		}
		public ActionResult ccByVehTypeData(string selectedType)
		{
			try
			{
				using (var db = new AutoSherDBContext())
				{
					List<string> ccData = db.irda_od_premium.Where(m => m.vehicleType == selectedType).Select(m => m.cubicCapacity).Distinct().ToList();
					return Json(new { success = true, ccData = ccData });
				}
			}
			catch (Exception ex)
			{

			}

			return Json(new { success = false });
		}

		public ActionResult getBasicODVaue(double odvValue, double idvValue)
		{
			double val=0;

			try
			{
				// logger.info(" odvValue : "+odvValue+" idvValue : "+idvValue);

				//DecimalFormat df2 = new DecimalFormat(".##");

				double OD_Value = (odvValue) / 100;

				double OD_Basic = (OD_Value) * (idvValue);

				val = OD_Basic;
				 //val = Convert.ToDouble(df2.Format(OD_Basic));
			}
			catch(Exception ex)
			{

			}
			
			return Json(new { val });

			//return ok(toJson(df2.format(OD_Basic)));
		}

		public ActionResult vehicleTypeByZoneData(string selectedZone)
		{
				try
			{
				using(var db=new AutoSherDBContext())
				{
					db.Configuration.LazyLoadingEnabled = false;
					List<string> vehTypeData = db.irda_od_premium.Where(m => m.zone == selectedZone).Select(m => m.vehicleType).Distinct().ToList();

					return Json(new { vehTypeData });
				}
			}
			catch(Exception ex)
			{

			}
			return View();

		}
		public ActionResult ageByCCTypeData(string selectedcc)
		{
			try
			{
				using (var db = new AutoSherDBContext())
				{
					List<string> ageData = db.irda_od_premium.Where(m => m.cubicCapacity == selectedcc).Select(m => m.vehicleAge).Distinct().ToList();

					return Json(new { ageData });
				}
			}

			catch (Exception ex)
			{
			}

			return View();

		}

		public ActionResult IRBasedOnFilter(string selectedZone, string selectedType, string selectedcc, string selectedAge)
		{
			try
			{
				using (var db = new AutoSherDBContext())
				{
					db.Configuration.LazyLoadingEnabled = false;
					var irDatas = db.irda_od_premium.Where(u => u.zone == selectedZone && u.vehicleType == selectedType && u.cubicCapacity == selectedcc && u.vehicleAge == selectedAge).ToList();
				     
				if(irDatas.Count>0)
					{
						return Json(new { irData= irDatas });
					}
				else
					{
						return Json(" ");
					}
				}
			}
			catch(Exception ex)
			{

			}
			return View();


		}

		#region save insurancequotaion/payUapi
		public ActionResult saveInsuranceQuotation(string jsonData)
		{
			try
			{
				insurancequotation insurancequotations = JsonConvert.DeserializeObject<insurancequotation>(jsonData);

				
				using (var db = new AutoSherDBContext())
				{
					insurancequotations.createdCRE = Session["UserName"].ToString();
					insurancequotations.createdDate = DateTime.Now;
					db.insurancequotations.Add(insurancequotations);
					db.SaveChanges();
					if (Session["DealerCode"].ToString() == "INDUS")
					{
						long customerID = insurancequotations.customer_id;
						long vehicleID = insurancequotations.vehicle_id;
						customer customerDetails = db.customers.Where(m => m.id == customerID).FirstOrDefault();
						address addressDetails = new address();
						if (db.addresses.Count(m => m.customer_Id == customerID && m.isPreferred == true) > 0)
						{
							addressDetails = db.addresses.Where(m => m.customer_Id == customerID && m.isPreferred == true).FirstOrDefault();
						}
						phone phoneDetails = new phone();
						if (db.phones.Count(m => m.customer_id == customerID && m.isPreferredPhone == true) > 0)
						{
							phoneDetails = db.phones.Where(m => m.customer_id == customerID && m.isPreferredPhone == true).FirstOrDefault();
						}
						email emailDetails = new email();
						if (db.emails.Count(m => m.customer_id == customerID && m.isPreferredEmail == true) > 0)
						{
							emailDetails = db.emails.Where(m => m.customer_id == customerID && m.isPreferredEmail == true).FirstOrDefault();
						}
						var chassisNumber = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehicleID).chassisNo;
						var enginnois = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehicleID).engineNo;
						var chasiss = db.vehicles.FirstOrDefault(m => m.vehicle_id == vehicleID).chassis;
						payUTransaction payUInvoice = new payUTransaction();
						var UserName = Session["UserName"].ToString();
						payUInvoice.wyzUser_ID = db.wyzusers.Where(m => m.userName == UserName).Select(m => m.id).FirstOrDefault();
						payUInvoice.customer_id = customerID;
						payUInvoice.vehicle_id = vehicleID;
						payUInvoice.chassis = chassisNumber;
						payUInvoice.enginno = enginnois;
						payUInvoice.chassisnumbers = chasiss;
						payUInvoice.amount = Convert.ToInt64(insurancequotations.totalPremiumWithTax);
						payUInvoice.productinfo = "insuranceQuotation";
						if (customerDetails.customerName == null || customerDetails.customerName == "")
						{
							payUInvoice.firstname = "Customer";
						}
						else
						{
							payUInvoice.firstname = customerDetails.customerName;
						}
						
                        if (emailDetails.emailAddress == null) 
						{
							payUInvoice.email = "noreply@autosherpas.com";
                        }
                        else {
							payUInvoice.email = emailDetails.emailAddress;
						}
						payUInvoice.phone = phoneDetails.phoneNumber;
						payUInvoice.address1 = addressDetails.concatenatedAdress;
						payUInvoice.city = addressDetails.city;
						payUInvoice.state = addressDetails.state;
						payUInvoice.country = addressDetails.country;
						payUInvoice.zipcode = addressDetails.pincode;
						payUInvoice.validation_period = 7;
						payUInvoice.send_email_now = 1;
						payUInvoice.merchantkey = "iEWnBr";
						payUInvoice.merchanrtsalt = "GTFIWCdXopWiciJtnhdWmVhTlB58bBb8";

						Random r = new Random();
						payUInvoice.transaction_ID = "txnid" + r.Next().ToString() +DateTime.Now.ToString("dd-MM-yyyy").Replace("-", "") + DateTime.Now.ToString("HH:mm:ss").Replace(":", "") + DateTime.Now.Millisecond.ToString();
						payUInvoice.fkquotationid = insurancequotations.id;
						autosendPayUInvoiceCreatePolicy(payUInvoice);
					}
                    return Json(new { success = true });
                }
            }
			catch(Exception ex)
			{

			}
			return View();
		}


        public void autosendPayUInvoiceCreatePolicy(payUTransaction payUtransaction)
        {

            IScheduler createPayUInvoiceScheduler = StdSchedulerFactory.GetDefaultScheduler().Result;

            createPayUInvoiceScheduler.Start();
            IJobDetail jobDetail = JobBuilder.Create<createPayUInvoiceJob>().Build();

            string trigername = "SendPayUInvoiceTrigger" + DateTime.Now.Millisecond + payUtransaction.vehicle_id;
            string trigerGroupName = "SendPayUInvoiceTriggerGroup" + DateTime.Now.Millisecond + payUtransaction.vehicle_id;

            ITrigger trigger = TriggerBuilder.Create().WithIdentity(trigername, trigerGroupName).StartNow().WithSimpleSchedule().Build();

            jobDetail.JobDataMap["SendPayUInvoice"] = JsonConvert.SerializeObject(payUtransaction);

            createPayUInvoiceScheduler.ScheduleJob(jobDetail, trigger);
        }

        #endregion


        /******************** Calculator End **********************/

        /*********************** Scheduling *********************/


        public ActionResult WalkinExecutivesByLocation(long locaId)
		{

			try
			{

				List<insuranceagent> walkinExe = walkinExecutivesByLoca(locaId, "2");
				var data = JsonConvert.SerializeObject(walkinExe);
				return Json(new { success = true, insuranceAgentList = walkinExe }) ;
			}
			catch (Exception ex)
			{
				return Json("false");
			}
			
		}

		public List<insuranceagent> walkinExecutivesByLoca(long locaId, string type)
		{
			List<insuranceagent> newuserList = new List<insuranceagent>();
			try
			{
				using (var db = new AutoSherDBContext())
				{
					db.Configuration.LazyLoadingEnabled = false;
					long walkinLocationExist =db.fieldwalkinlocations.Count(u=>u.id==locaId);
					if (walkinLocationExist > 0)
					{
						List<userfieldwalkinlocation> userList = db.userfieldwalkinlocations.Where(m=>m.fieldWalkinLocationList_id==locaId).ToList();
						foreach (userfieldwalkinlocation sa in  userList)
						{
							Console.WriteLine("sa userlist : " + sa.fieldWalkinLocationList_id);
							long userId = sa.userFieldWalkinLocation_id;
							insuranceagent saData = db.insuranceagents.FirstOrDefault(u => u.wyzuser.id == userId);
							if (saData != null)
							{
								newuserList.Add(saData);
							}
						}
					}
				}
			}
			catch(Exception ex)
			{

			}
			return newuserList;
		}
		public List<string> getFSEScheduleByWyzUser(long userId, string scheduleDate)
		{
			
			try
			{
				using (var db = new AutoSherDBContext())
				{
					insuranceagent ins = db.insuranceagents.Where(u => u.wyzUser_id == userId).FirstOrDefault();

					long id = ins.insuranceAgentId;


					var pickupid = db.appointmentbookeds.FirstOrDefault(u => u.insuranceAgent_insuranceAgentId == userId).pickupDrop_id;

					var pickupListOfTodayFrom = db.pickupdrops.Where(u => u.id == pickupid && u.pickupDate.ToString()==scheduleDate).Select(u => u.timeFrom);
					var pickupListOfTodayTo = db.pickupdrops.Where(u => u.id == pickupid && u.pickupDate.ToString() == scheduleDate).Select(u => u.timeTo).ToList();

					List<appointmentbooked> d = new List<appointmentbooked>();
					
				}
			}
			catch(Exception ex)
			{

			}
			return null;
			}
		public List<bookingdatetime> getTimeSlot()
		{
			try
			{
				using (var db = new AutoSherDBContext())
				{
					return db.bookingdatetimes.ToList();
				}
			}
			catch (Exception ex)
			{

			}
			return null;
			}

		public ActionResult filedExecutivesListToSchedule(string scheduleDate, long? locaId)
		{
			try
			{
				using (var db = new AutoSherDBContext())
				{
					db.Configuration.LazyLoadingEnabled = false;
					long customerId=Convert.ToInt64(Session["CusId"]);
					List<PickupDropDataOnTabLoad> UsersList = new List<PickupDropDataOnTabLoad>();
					List<bookingdatetime> timeSlots =getTimeSlot();
					var fSElist = (from f in db.fieldwalkinlocations.Where(x => x.id == locaId && (x.typeOfLocation == "1"|| x.typeOfLocation=="3"))
								   from u in db.userfieldwalkinlocations.Where(x => x.fieldWalkinLocationList_id == f.id)
								   select new
								   {
									   u.userFieldWalkinLocation_id
								   }).ToList();

					foreach(var lst in fSElist)
					{
						PickupDropDataOnTabLoad pickup = new PickupDropDataOnTabLoad();
						insuranceagent ins = db.insuranceagents.Where(u => u.wyzUser_id == lst.userFieldWalkinLocation_id).FirstOrDefault();
						long id = ins.insuranceAgentId;
						if (db.appointmentbookeds.Any(x => x.insuranceAgent_insuranceAgentId == id))
						{
							List<TimeSpan> datesList = new List<TimeSpan>();
							DateTime sDate = Convert.ToDateTime(scheduleDate);

							var pickupidList = (from app in db.appointmentbookeds where app.insuranceBookStatus_id!=35 && app.insuranceAgent_insuranceAgentId == id && app.appointmentDate == sDate select app.pickupDrop_id).ToList();
							foreach (var pickupid in pickupidList)
							{
								var pickupListOfTodayFrom = db.pickupdrops.FirstOrDefault(u => u.id == pickupid && u.pickupDate == sDate);
								var pickupListOfTodayTo = db.pickupdrops.FirstOrDefault(u => u.id == pickupid && u.pickupDate == sDate);
								if (pickupListOfTodayFrom != null && pickupListOfTodayTo != null)
								{
									TimeSpan t1 = pickupListOfTodayFrom.timeFrom ?? default(TimeSpan);
									TimeSpan t2 = pickupListOfTodayFrom.timeTo ?? default(TimeSpan);

									TimeSpan span = t1 - t2;


									int start = t1.Hours;
									int End = t2.Hours;
									int EntriesCount;
									
									string startTime = t1.Hours + ":" + t1.Minutes;
									string endTime = t2.Hours + ":" + t2.Minutes;

									TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));
									EntriesCount = duration.Hours;
									EntriesCount = EntriesCount + EntriesCount;
									if(duration.Minutes==30)

									{
										EntriesCount = EntriesCount + 1;
									}

									TimeSpan StartTime = TimeSpan.FromHours(start);
									int Difference = 30; //In minutes.
									int j;
									if (t1.Minutes==0)
									{
										j = 0;
										//EntriesCount=EntriesCount + 1;
									}
									else
									{
										j = 1;
										EntriesCount = EntriesCount + 1;
									}

									Dictionary<TimeSpan, TimeSpan> Entries = new Dictionary<TimeSpan, TimeSpan>();
									for (int i = j; i < EntriesCount; i++)
									{
											Entries.Add(StartTime.Add(TimeSpan.FromMinutes(Difference * i)),StartTime.Add(TimeSpan.FromMinutes(Difference * i)));
										
									}
									foreach (var e in Entries)
									{
										datesList.Add(e.Key);
									}
								}
								
								pickup.listTime = datesList;
							}
						}

						pickup.id = ins.insuranceAgentId;
						pickup.userName = ins.insuranceAgentName;
						UsersList.Add(pickup);
					}
					SchedularDataOnTabLoad driverData = new SchedularDataOnTabLoad();
					return Json(new { UsersList, timeSlots , JsonRequestBehavior.AllowGet });
				}
			}
			catch(Exception ex)
			{

			}
			return View();
		}


		#region Appointment Field Excecultive
		public ActionResult filedExecutivesListToSchedulerNEW(string scheduleDate, long? locaId)
		{
			List<string> chasis = new List<string>();
			try
			{
				using (var db = new AutoSherDBContext())
				{
					db.Configuration.LazyLoadingEnabled = false;
					long customerId = Convert.ToInt64(Session["CusId"]);
					List<PickupDropDataOnTabLoad> UsersList = new List<PickupDropDataOnTabLoad>();
					List<bookingdatetime> timeSlots = getTimeSlot();
					var fSElist = (from f in db.fieldwalkinlocations.Where(x => x.id == locaId && (x.typeOfLocation == "1" || x.typeOfLocation == "3"))
								   from u in db.userfieldwalkinlocations.Where(x => x.fieldWalkinLocationList_id == f.id)
								   select new
								   {
									   u.userFieldWalkinLocation_id
								   }).ToList();

					foreach (var lst in fSElist)
					{
						Dictionary<string, string> chasList = new Dictionary<string, string>();
						PickupDropDataOnTabLoad pickup = new PickupDropDataOnTabLoad();
						List<TimeSpan> datesList = new List<TimeSpan>();
						bool sameUser = false;
						List<string> lstchasis = new List<string>();
						DateTime sDate = Convert.ToDateTime(scheduleDate);
						TimeSpan tm = new TimeSpan();
						insuranceagent ins = db.insuranceagents.Where(u => u.wyzUser_id == lst.userFieldWalkinLocation_id).FirstOrDefault();

						long id = ins.insuranceAgentId;
						int appointCount = db.appointmentbookeds.Count(m => m.insuranceBookStatus_id != 35 && m.insuranceAgent_insuranceAgentId == id && m.appointmentDate == sDate);
						if (appointCount > 0)
						{
							var pickupidList = db.appointmentbookeds.Where(m => m.insuranceBookStatus_id != 35 && m.insuranceAgent_insuranceAgentId == id && m.appointmentDate == sDate).Select(m => new { m.vehicle_id, m.pickupDrop_id, m.customer_id }).ToList();
							foreach (var pickupid in pickupidList)
							{
								string chaslst = "",curChassis="";
								var pickupListOfTodayFrom = db.pickupdrops.FirstOrDefault(u => u.id == pickupid.pickupDrop_id && u.pickupDate == sDate);
								if (pickupListOfTodayFrom != null)
								{
									TimeSpan t1 = pickupListOfTodayFrom.timeFrom ?? default(TimeSpan);
									if (!datesList.Any(m => m == t1))
                                    {
										datesList.Add(t1);
                                    }
                                   // datesList.Add(t1);

									var pickupIdlists = db.pickupdrops.Where(m => m.timeFrom == t1 && m.pickupDate == sDate).Select(m => m.id).ToList();
									var vehicleIds = db.appointmentbookeds.Where(m => pickupIdlists.Contains(m.pickupDrop_id ?? default(long))).Select(m => m.vehicle_id).ToList();
									var chassisLists = db.vehicles.Where(m => vehicleIds.Contains(m.vehicle_id)).Select(m => m.chassisNo).Distinct().ToList();

									curChassis = string.Join(",", chassisLists);
									chaslst = t1.TotalHours + "/" + string.Join(",", chassisLists);

								}

							
								
								if(!chasList.ContainsKey(curChassis))
                                {
									chasList[curChassis] = chaslst;
								}
								if (customerId == pickupid.customer_id)
								{
									chaslst = chaslst + "/T";
									if (chasList.ContainsKey(curChassis))
									{
										chasList[curChassis] = chaslst;
									}
									else
                                    {
										chasList[curChassis] = chaslst;
									}
								}


								pickup.listTime = datesList;
								pickup.sameUser = sameUser;
								//pickup.listChassis = chasis;
								pickup.listChassis = chasList.Values.ToList();
							}
						}

						pickup.id = ins.insuranceAgentId;
						pickup.userName = ins.insuranceAgentName;

						UsersList.Add(pickup);
					}
					return Json(new { UsersList, timeSlots, JsonRequestBehavior.AllowGet });
				}
			}
			catch (Exception ex)
			{

			}
			return View();
		}


		#endregion
		#region Policy Drop Field Excecultive

		public ActionResult policydropfiledExecutivesListToScheduler(string scheduleDate, long? locaId)
		{
			List<string> chasis = new List<string>();
			try
			{
				using (var db = new AutoSherDBContext())
				{
					db.Configuration.LazyLoadingEnabled = false;
					long customerId = Convert.ToInt64(Session["CusId"]);
					List<PickupDropDataOnTabLoad> UsersList = new List<PickupDropDataOnTabLoad>();
					List<bookingdatetime> timeSlots = getTimeSlot();
					var fSElist = (from f in db.fieldwalkinlocations.Where(x => x.id == locaId && (x.typeOfLocation == "1" || x.typeOfLocation == "3"))
								   from u in db.userfieldwalkinlocations.Where(x => x.fieldWalkinLocationList_id == f.id)
								   select new
								   {
									   u.userFieldWalkinLocation_id
								   }).ToList();

					foreach (var lst in fSElist)
					{
						string chaslst = "", curChassis = "";
						Dictionary<string, string> chasList = new Dictionary<string, string>();
						PickupDropDataOnTabLoad pickup = new PickupDropDataOnTabLoad();
						List<TimeSpan> datesList = new List<TimeSpan>();
						bool sameUser = false;
						List<string> lstchasis = new List<string>();
						DateTime sDate = Convert.ToDateTime(scheduleDate);
						TimeSpan tm = new TimeSpan();
						insuranceagent ins = db.insuranceagents.Where(u => u.wyzUser_id == lst.userFieldWalkinLocation_id).FirstOrDefault();

						long id = ins.insuranceAgentId;
						int appointCount = db.insuranceassignedinteractions.Count(m => m.FEID == id && m.appointmentDate == sDate);
						if (appointCount > 0)
						{
							var pickupidList = db.insuranceassignedinteractions.Where(m => m.FEID == id && m.appointmentDate == sDate).Select(m => new { m.vehicle_vehicle_id, m.pickUPID, m.customer_id }).ToList();
							foreach (var pickupid in pickupidList)
							{
								var pickupListOfTodayFrom = db.pickupdrops.FirstOrDefault(u => u.id == pickupid.pickUPID && u.pickupDate == sDate);
								if (pickupListOfTodayFrom != null)
								{
									TimeSpan t1 = pickupListOfTodayFrom.timeFrom ?? default(TimeSpan);
									if (!datesList.Any(m => m == t1))
									{
										datesList.Add(t1);
									}

									var pickupIdlists = db.pickupdrops.Where(m => m.timeFrom == t1 && m.pickupDate == sDate).Select(m => m.id).ToList();
									var vehicleIds = db.insuranceassignedinteractions.Where(m => pickupIdlists.Contains(m.pickUPID)).Select(m => m.vehicle_vehicle_id).ToList();
									var chassisLists = db.vehicles.Where(m => vehicleIds.Contains(m.vehicle_id)).Select(m => m.chassisNo).ToList();
									curChassis = string.Join(",", chassisLists);
									chaslst = t1.TotalHours + "/" + string.Join(",", chassisLists);

								}
								chasis.Add(chaslst);

								if (!chasList.ContainsKey(curChassis))
								{
									chasList[curChassis] = chaslst;
								}

								pickup.listTime = datesList;
								pickup.sameUser = sameUser;
								//pickup.listChassis = chasis;
								pickup.listChassis = chasList.Values.ToList();
							}
						}
						pickup.id = ins.insuranceAgentId;
						pickup.userName = ins.insuranceAgentName;

						UsersList.Add(pickup);
					}
					SchedularDataOnTabLoad driverData = new SchedularDataOnTabLoad();
					return Json(new { UsersList, timeSlots, JsonRequestBehavior.AllowGet });
				}
			}
			catch (Exception ex)
			{

			}
			return View();
		}
		#endregion
	}
}