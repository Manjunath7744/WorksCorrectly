using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class serviceDashboardCountVewModel
    {
        public long? FreeCalls { get; set; }
        public long? FreshCalls { get; set; }
        public long? FreshCallsAttempts { get; set; }
        public long? TotalpendingCalls { get; set; }
        public long? NonFsPms { get; set; }
        public long? NonFsPmsattempt { get; set; }
        public long? pendingattempts { get; set; }
        public long? TotalNonContact { get; set; }
        public long? NonConattempts { get; set; }
        public long? TodayNonContact { get; set; }
        public long? Nthbooking { get; set; }
        public long? NthbookingAttempt { get; set; }
        public long? NPlusebooking { get; set; }
        public long? NPlusebookingAttempt { get; set; }
        public long? OverdueBooking { get; set; }
        public long? OverdueBookingAttempt { get; set; }
        public long? Total { get; set; }
        public long? Pickup { get; set; }
        public long? WalkIn { get; set; }
        public long? TodayTotalCalls { get; set; }
        public long? Contacts { get; set; }
        public long? Bookings { get; set; }

    }
}