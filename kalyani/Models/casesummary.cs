namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("casesummary")]
    public partial class casesummary
    {
        [Key]
        public long serialNoCS { get; set; }

        [StringLength(255)]
        public string addChangeDetails { get; set; }

        [StringLength(255)]
        public string additionalRemarks { get; set; }

        [StringLength(255)]
        public string additionalRemarksBusiness { get; set; }

        [StringLength(30)]
        public string addressConfirmed { get; set; }

        [StringLength(30)]
        public string addressTraceble { get; set; }

        [StringLength(255)]
        public string allocated_to { get; set; }

        [StringLength(30)]
        public string antiSocial { get; set; }

        [StringLength(50)]
        public string antiSocialBusiness { get; set; }

        [StringLength(255)]
        public string approxAreaofoffice { get; set; }

        [StringLength(30)]
        public string areaOfHouseInYards { get; set; }

        [StringLength(100)]
        public string businessBoard { get; set; }

        [StringLength(255)]
        public string businessBoard_others { get; set; }

        [StringLength(255)]
        public string checkedAtLocalityRadd { get; set; }

        [StringLength(255)]
        public string checkedAtLocalityRadd_Others { get; set; }

        [StringLength(50)]
        public string checkedatLocalityBus { get; set; }

        [StringLength(255)]
        public string comments { get; set; }

        [StringLength(255)]
        public string confimedByWhom { get; set; }

        [StringLength(20)]
        public string confirmation { get; set; }

        [StringLength(255)]
        public string confirmedByWhomBus { get; set; }

        [StringLength(255)]
        public string designationApplicant { get; set; }

        [StringLength(255)]
        public string designationPersonmet { get; set; }

        [StringLength(50)]
        public string doorLockedOrNot { get; set; }

        [StringLength(255)]
        public string durationOfStayCurrentAddress { get; set; }

        public long earningMembers { get; set; }

        [StringLength(30)]
        public string easeOfLocation { get; set; }

        [StringLength(30)]
        public string easeoflocatingOffice { get; set; }

        [StringLength(255)]
        public string educationQualification { get; set; }

        [StringLength(255)]
        public string educationQualification_Others { get; set; }

        [StringLength(30)]
        public string familyType { get; set; }

        [StringLength(30)]
        public string howCOperitiveCustomerIs { get; set; }

        [StringLength(30)]
        public string isAddressOfPersonTraced { get; set; }

        [StringLength(30)]
        public string isAppicatedStayInHome { get; set; }

        [StringLength(30)]
        public string isHouseLocked { get; set; }

        [StringLength(30)]
        public string lastMileAcheived { get; set; }

        [StringLength(255)]
        public string lastmileachievedBus { get; set; }

        [StringLength(30)]
        public string livingStandardOfApplicant { get; set; }

        [StringLength(30)]
        public string localityOfResidence { get; set; }

        [StringLength(255)]
        public string localityOfResidence_Others { get; set; }

        [StringLength(255)]
        public string nameOfContactPerson { get; set; }

        [StringLength(100)]
        public string natureOfBusiness { get; set; }

        [StringLength(255)]
        public string natureOfBusiness_others { get; set; }

        [StringLength(255)]
        public string neighbour1 { get; set; }

        [StringLength(255)]
        public string neighbour1DetailsIfNeg { get; set; }

        [StringLength(30)]
        public string neighbour1FeedBack { get; set; }

        [StringLength(255)]
        public string neighbour2 { get; set; }

        [StringLength(255)]
        public string neighbour2DetailsIfNeg { get; set; }

        [StringLength(30)]
        public string neighbour2FeedBack { get; set; }

        [StringLength(255)]
        public string noOfEmployeesighted { get; set; }

        public long noOfFamilyMembers { get; set; }

        [StringLength(100)]
        public string noofEmployeesWorking { get; set; }

        [StringLength(255)]
        public string numOfyearsInPresent { get; set; }

        [StringLength(100)]
        public string officeOwnership { get; set; }

        [StringLength(255)]
        public string officeOwnership_others { get; set; }

        [StringLength(255)]
        public string otherCheckedatLocalityBus { get; set; }

        [StringLength(255)]
        public string otherEarningMembers { get; set; }

        [StringLength(255)]
        public string othersWasCustomercontactableBus { get; set; }

        [StringLength(255)]
        public string personMet { get; set; }

        [StringLength(255)]
        public string personalIdSeen { get; set; }

        [StringLength(255)]
        public string personalIdSeen_Others { get; set; }

        [StringLength(30)]
        public string police { get; set; }

        [StringLength(50)]
        public string policeBusiness { get; set; }

        [StringLength(30)]
        public string politicalParty { get; set; }

        [StringLength(50)]
        public string politicalPartyBusiness { get; set; }

        [StringLength(255)]
        public string previousAddressDetails { get; set; }

        [StringLength(255)]
        public string productDetails { get; set; }

        [StringLength(30)]
        public string realtionShipWithApplicant { get; set; }

        [StringLength(255)]
        public string realtionShipWithApplicant_Others { get; set; }

        [StringLength(255)]
        public string remarks { get; set; }

        [StringLength(255)]
        public string remarksBus { get; set; }

        [StringLength(30)]
        public string residentialStatus { get; set; }

        [StringLength(255)]
        public string residentialStatus_Others { get; set; }

        public long serialNoOfPendingCase { get; set; }

        [StringLength(255)]
        public string signature { get; set; }

        [StringLength(255)]
        public string status { get; set; }

        public DateTime? submitedTimeStamp { get; set; }

        public long totalMembers { get; set; }

        [StringLength(100)]
        public string typeOfBusiness { get; set; }

        [StringLength(255)]
        public string typeOfBusiness_others { get; set; }

        [StringLength(100)]
        public string typeOfOffice { get; set; }

        [StringLength(255)]
        public string typeOfOffice_others { get; set; }

        [StringLength(50)]
        public string typeOfResidence { get; set; }

        [StringLength(255)]
        public string userSignBusiness { get; set; }

        [StringLength(30)]
        public string wasCustomerContactable { get; set; }

        [StringLength(255)]
        public string wasCustomerContactable_Others { get; set; }

        [StringLength(50)]
        public string wasCustomercontactableBus { get; set; }

        public long? image_Id { get; set; }

        public long? case_Id { get; set; }

        public virtual caselist caselist { get; set; }

        public virtual imagestorage imagestorage { get; set; }
    }
}
