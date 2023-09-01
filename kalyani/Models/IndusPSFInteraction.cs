using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("indusPsfInteraction")]
    public class IndusPSFInteraction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IndusPSFInteraction()
        {
            upsellleads = new HashSet<upselllead>();
        }

        [Key]
        public int Id { get; set; }

        public string isContacted { get; set; }
        public string PSFDisposition { get; set; }
        public string whatCustSaid { get; set; }
        public string vehicleAfterService { get; set; }
        public bool isTechnical { get; set; }
        public bool nonTechnincal { get; set; }
        public string bodychasis { get; set; }
        public string electricals { get; set; }
        public string performance { get; set; }
        public string powertrain { get; set; }
        public string steeringNsuspention { get; set; }
        public string safety { get; set; }
        public string improperExpectNUsage { get; set; }
        public string workQuality { get; set; }
        public string ServiceAdvisor { get; set; }
        public string spareParts { get; set; }
        public string billing { get; set; }
        public string delivery { get; set; }
        public string othernonTech { get; set; }
        public string SAescalationSticker { get; set; }
        public string SAInstantFeedBack { get; set; }
        public string qos { get; set; }
        public string overallServiceExperience { get; set; }
        public string rateSA { get; set; }
        public string customerFeedBack { get; set; }
        public string creRemarks { get; set; }

        public DateTime? FollowUpdate { get; set; }

        public string FollowUptime { get; set; }
        public string nonContactOtherReason { get; set; }
        public string isComplaintRaised { get; set; }
        public long? complaintMgr_id { get; set; }

        public string OtherComments { get; set; }
        public string modeOfServiceDone { get; set; }
        public string qFordQ1 { get; set; }
        public string qFordQ2 { get; set; }
        public string qFordQ3 { get; set; }
        public string afterServiceComments { get; set; }
        public string qFordQ5 { get; set; }
        public string qFordQ6 { get; set; }
        public string qFordQ7 { get; set; }
        public string qM2_ReasonOfAreaOfImprovement { get; set; }
        public string qFordQ9 { get; set; }
        public string qFordQ10 { get; set; }
        public string reasonOfDissatification { get; set; }
        public string modeOfService { get; set; }

        public string finalrating { get; set; }


        //Extra Questions
        public string demandedrepairsCompleted { get; set; }
        public string promiseddeliveryTime { get; set; }
        public string washingQuality { get; set; }
        public string vehicleserviceCharges { get; set; }

        //kalyanibodyshop
        public string bodyrepairthrough { get; set; }
        public string experienceOnIns { get; set; }
        public string qualityofwork { get; set; }

        public string dispositionFrom { get; set; }

        //*********** Complaint End *******

        public long upsellCount { get; set; }

        public long? callDispositionData_id { get; set; }

        public long? callInteraction_id { get; set; }
        [NotMapped]
        public string psfFormat { get; set; }
        [NotMapped]
        public int psfCampaign { get; set; }
        public string qFordQ11 { get; set; }
        public string qFordQ12 { get; set; }
        public string qFordQ13 { get; set; }
        public string qFordQ14 { get; set; }
        public string qFordQ15 { get; set; }
        public string qFordQ16 { get; set; }
        public string qFordQ17 { get; set; }
        public string qFordQ18 { get; set; }
        public string qFordQ19 { get; set; }
        public string qFordQ20 { get; set; }
        public string qFordQ21 { get; set; }
        public string qFordQ22 { get; set; }
        public string qFordQ23 { get; set; }
        public string qFordQ24 { get; set; }
        public string qFordQ25 { get; set; }
        public string qFordQ26 { get; set; }
        //for kataria
        public string overallcustsatisfaction { get; set; }
        public string VisitType { get; set; }
        

        public virtual calldispositiondata calldispositiondata { get; set; }

        public virtual callinteraction callinteraction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<upselllead> upsellleads { get; set; }
    }
}