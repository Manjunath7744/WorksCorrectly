using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class CallDispositionDataCode
    {
        public static int STATUS_CONTACT = 1;
        public static int STATUS_NON_CONTACT = 2;
        public static int STATUS_BOOK_MY_SERVICE = 3;
        public static int STATUS_CALL_ME_LATER = 4;
        public static int STATUS_SERVICE_NOT_REQUIRED = 5;
        public static int STATUS_RINGING_NO_RESPONSE = 6;
        public static int STATUS_BUSY = 7;
        public static int STATUS_SWITCHED_OFF_UNREACHABLE = 8;
        public static int STATUS_INVALID_NUMBER = 9;
        public static int STATUS_OTHERS = 10;

        public static int STATUS_ALREADY_SERVED = 11;
        public static int STATUS_VEHICLE_SOLD = 12;
        public static int STATUS_DISSATIFIED_WITH_PREVIOUS_SERVICE = 13;
        public static int STATUS_DISTANCE_FROM_DEALER_LOCATION = 14;
        public static int STATUS_DISSATIFIED_WITH_SALES = 15;
        public static int STATUS_DISSATIFIED_WITH_INSURANCE = 16;
        public static int STATUS_OTHER_SERVICE = 17;
        public static int STATUS_SERVICED_AT_MY_DEALER = 18;
        public static int STATUS_SERVICED_AT_OTHER_DEALER = 19;
        public static int STATUS_AUTHORIZED_WORKSHOP = 20;
        public static int STATUS_NON_AUTHORIZED_WORKSHOP = 21;
        public static int STATUS_PSF_YES = 22;
        public static int STATUS_NOT_INTERESTED = 23;
        public static int STATUS_SERVICE_CANCELLED = 24;

        public static int STATUS_BOOK_APPOINTMENT = 25;
        public static int STATUS_RENEWAL_NOT_REQUIRED = 26;
        public static int STATUS_ALREADY_RENEWED = 27;
        public static int STATUS_RENEWED_AT_MY_DEALER = 28;
        public static int STATUS_RENEWED_AT_OTHER_DEALER = 29;
        public static int STATUS_OEM_AUTHORIZED_DEALER = 30;
        public static int STATUS_UNAUTHORIZED_DEALER = 31;
        public static int STATUS_KMS_NOT_REQUIRED = 32;

        public static int STATUS_OVER_DUE = 33;
        public static int STATUS_CLOSED = 34;
        public static int STATUS_CANCELLED = 35;
        public static int STATUS_INCOMPLETE_SURVEY = 36;
        public static int STATUS_SERVICERESCHEDULED = 37;
        public static int STATUS_VEHICLERECIEVED = 38;
        public static int STATUS_CONVERTED = 39;
        public static int STATUS_DROPPED_BY_FSE = 40;
        public static int STATUS_CONFIRMED = 41;
        public static int STATUS_PICK_UP_CANCEL = 42;
    }
}