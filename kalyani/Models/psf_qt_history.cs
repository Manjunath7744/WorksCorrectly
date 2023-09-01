namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("psf_qt_history")]
    public partial class psf_qt_history
    {
        public long id { get; set; }

        [StringLength(1000)]
        public string AgentRemarks { get; set; }

        [StringLength(100)]
        public string AppointmentonDesiredDayTime { get; set; }

        [StringLength(100)]
        public string DealerFacility { get; set; }

        [StringLength(100)]
        public string DealerHAClosingDate { get; set; }

        [StringLength(2000)]
        public string DealerRemarkHA { get; set; }

        [StringLength(100)]
        public string Dealershipcleanliness { get; set; }

        [StringLength(100)]
        public string EngineType { get; set; }

        [StringLength(100)]
        public string FIRFT { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(1000)]
        public string HAAgentremarks { get; set; }

        [Column(TypeName = "date")]
        public DateTime? HotAlertOpendate { get; set; }

        [StringLength(100)]
        public string Hotalertageing { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Model { get; set; }

        [StringLength(100)]
        public string OverallDelieveryExperience { get; set; }

        [StringLength(100)]
        public string PhoneNo { get; set; }

        [StringLength(100)]
        public string ReadyWithinPromisedTime { get; set; }

        [StringLength(100)]
        public string ReviewDone { get; set; }

        [StringLength(100)]
        public string RootCause { get; set; }

        [StringLength(100)]
        public string RootCause1 { get; set; }

        [StringLength(100)]
        public string Rootcause2 { get; set; }

        [StringLength(100)]
        public string ServiceAdvisorName { get; set; }

        [StringLength(100)]
        public string ServiceAdvisorPerformance { get; set; }

        [StringLength(100)]
        public string ServiceExperience { get; set; }

        [StringLength(100)]
        public string ServiceInitiation { get; set; }

        [StringLength(100)]
        public string ServiceTechnicianName { get; set; }

        [StringLength(100)]
        public string Status { get; set; }

        [StringLength(100)]
        public string SubDisposition { get; set; }

        [StringLength(100)]
        public string VehicleCleanliness { get; set; }

        [StringLength(100)]
        public string VehicleReadinessStatus { get; set; }

        [StringLength(100)]
        public string VehicleSaleDate { get; set; }

        public long? vehicle_id { get; set; }

        [StringLength(45)]
        public string upload_id { get; set; }

        public virtual vehicle vehicle { get; set; }
    }
}
