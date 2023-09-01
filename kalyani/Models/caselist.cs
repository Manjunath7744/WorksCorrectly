namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("caselist")]
    public partial class caselist
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public caselist()
        {
            casesummaries = new HashSet<casesummary>();
        }

        [Key]
        public long serialNo { get; set; }

        [StringLength(255)]
        public string allocated_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? allocated_on { get; set; }

        [StringLength(255)]
        public string allocated_to { get; set; }

        [Column(TypeName = "bit")]
        public bool? allocation_status { get; set; }

        [StringLength(255)]
        public string applicantAddress { get; set; }

        [StringLength(255)]
        public string applicantId { get; set; }

        [StringLength(255)]
        public string applicantName { get; set; }

        [StringLength(255)]
        public string applicantType { get; set; }

        [StringLength(255)]
        public string bankName { get; set; }

        [StringLength(255)]
        public string category { get; set; }

        [StringLength(255)]
        public string companyName { get; set; }

        [StringLength(255)]
        public string created_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? created_on { get; set; }

        [StringLength(255)]
        public string department { get; set; }

        [StringLength(255)]
        public string designation { get; set; }

        [StringLength(255)]
        public string dob { get; set; }

        [StringLength(255)]
        public string dsaName { get; set; }

        [StringLength(255)]
        public string fI_Date { get; set; }

        [StringLength(255)]
        public string fI_Flag { get; set; }

        [StringLength(255)]
        public string fI_Time { get; set; }

        [StringLength(255)]
        public string fI_ToBeConducted { get; set; }

        [StringLength(255)]
        public string loanAmount { get; set; }

        [StringLength(255)]
        public string location { get; set; }

        [StringLength(255)]
        public string mobileNumber { get; set; }

        [StringLength(255)]
        public string modified_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? modified_on { get; set; }

        [Column(TypeName = "bit")]
        public bool modified_status { get; set; }

        [StringLength(255)]
        public string officeAddress { get; set; }

        [StringLength(255)]
        public string officeNumber { get; set; }

        [StringLength(255)]
        public string office_pincode { get; set; }

        [StringLength(255)]
        public string permanentAddress { get; set; }

        [StringLength(255)]
        public string permanentNumber { get; set; }

        [StringLength(255)]
        public string productName { get; set; }

        [StringLength(255)]
        public string productTypeCustomer { get; set; }

        [StringLength(255)]
        public string residenceAddress { get; set; }

        [StringLength(255)]
        public string residenceNumber { get; set; }

        [StringLength(255)]
        public string residence_pincode { get; set; }

        [StringLength(255)]
        public string specialInstruction { get; set; }

        [StringLength(255)]
        public string status { get; set; }

        [StringLength(255)]
        public string subCategory { get; set; }

        [StringLength(255)]
        public string tenure { get; set; }

        [StringLength(255)]
        public string verificationId { get; set; }

        [StringLength(255)]
        public string verificationType { get; set; }

        [Column(TypeName = "bit")]
        public bool? verified_Status { get; set; }

        public long? user_Id { get; set; }

        public virtual wyzuser wyzuser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<casesummary> casesummaries { get; set; }
    }
}
