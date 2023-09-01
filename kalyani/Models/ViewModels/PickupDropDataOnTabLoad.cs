using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
      public class PickupDropDataOnTabLoad 
  {

		public long id { get; set; }
		public string userName { get; set; }
		public List<TimeSpan> listTime { get; set; }
		public TimeSpan blockedTime { get; set; }
		public List<string> listChassis { get; set; }
		public bool sameUser { get; set; }

		//for driver
		public string StartTime { get; set; }
		public long DriverId { get; set; }
	}
 }