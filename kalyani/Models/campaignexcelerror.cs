namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("campaignexcelerror")]
    public partial class campaignexcelerror
    {
        public long id { get; set; }

        [StringLength(255)]
        public string address1 { get; set; }

        [StringLength(255)]
        public string address2 { get; set; }

        [StringLength(255)]
        public string address3 { get; set; }

        [StringLength(255)]
        public string chassis { get; set; }

        [StringLength(255)]
        public string city { get; set; }

        [StringLength(255)]
        public string colorDesc { get; set; }

        [StringLength(255)]
        public string customerName { get; set; }

        [StringLength(255)]
        public string dataUploadLocation { get; set; }

        [StringLength(255)]
        public string email_id { get; set; }

        [StringLength(255)]
        public string endDate { get; set; }

        [StringLength(255)]
        public string engine { get; set; }

        [StringLength(1073741823)]
        public string errorInformation { get; set; }

        [Column(TypeName = "bit")]
        public bool isError { get; set; }

        [StringLength(255)]
        public string land_line { get; set; }

        [StringLength(255)]
        public string mobile1 { get; set; }

        [StringLength(255)]
        public string mobile2 { get; set; }

        [StringLength(255)]
        public string pincode { get; set; }

        public int rowNumber { get; set; }

        [StringLength(255)]
        public string salutation { get; set; }

        [StringLength(255)]
        public string source { get; set; }

        [StringLength(255)]
        public string startDate { get; set; }

        [StringLength(255)]
        public string std_code { get; set; }

        [StringLength(255)]
        public string upload_id { get; set; }

        [StringLength(255)]
        public string varaintDesc { get; set; }

        [StringLength(255)]
        public string veh_reg_no { get; set; }

        [StringLength(255)]
        public string vehicle_types { get; set; }
    }
}
