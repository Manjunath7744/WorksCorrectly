using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class addCreVM
    {
        public string creName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string confirmPass { get; set; }
        public string firstLocation { get; set; }
        public string firstWorkshop { get; set; }
        public string role { get; set; }
        public string moduleType { get; set; }
        public string phoneNumber { get; set; }
        public string IMEI { get; set; }
        public string locations { get; set; }
        public string workshops { get; set; }
        public string creManager { get; set; }
        public string sipExtensionId { get; set; }
        public string sipID { get; set; }
        public string gsmip { get; set; }
        public string callingType { get; set; }
        public string CRECampaignId { get; set; }
        public string CREServiceTypeId { get; set; }
        public string CREROAgeingId { get; set; }
        public string CRECategoryId { get; set; }
        public int CREFieldOneId { get; set; }
        public int CREFieldTwoId { get; set; }

    }
    public class creTableVM
    {
        public string creName { get; set; }
        public string location { get; set; }
        public long? location_cityId { get; set; }
        public long? workshop_id { get; set; }
        public string workshop { get; set; }
        public string phoneNumber { get; set; }
        public string IMEI { get; set; }
        public string userName { get; set; }
        public string passWord { get; set; }
        public string creManager { get; set; }
        public long wyzId { get; set; }
        public string extensionId { get; set; }
        public string workshopIds { get; set; }
        public string CRECampaignId { get; set; }
        public string CREServiceTypeId { get; set; }
        public string CREROAgeingId { get; set; }
        public string CRECategoryId { get; set; }
        public int? CREFieldOneId { get; set; }
        public int? CREFieldTwoId { get; set; }
    }


    public class AssignUserList
    {
        public string CREUserId { get; set; }
        public string username { get; set; }
        public string workshopName { get; set; }
        public int workshopid { get; set; }
        public string serviceTypeId { get; set; }
        public string categoryId { get; set; }
        public string RoAgeingId { get; set; }
    }
}