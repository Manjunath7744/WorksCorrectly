namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("smrforecasteddata")]
    public partial class smrforecasteddata
    {
        public long id { get; set; }

        [StringLength(100)]
        public string chassisnumber { get; set; }

        [StringLength(100)]
        public string customername { get; set; }

        [Column(TypeName = "date")]
        public DateTime? maxfsdate { get; set; }

        [StringLength(100)]
        public string locationname { get; set; }

        public long? location_id { get; set; }

        [StringLength(100)]
        public string workshopname { get; set; }

        public long? workshop_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? nextServiceDate { get; set; }

        [StringLength(100)]
        public string nextservicetype { get; set; }

        [Column(TypeName = "date")]
        public DateTime? maxpsdate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? saledate { get; set; }

        public float? averagerunning { get; set; }

        public long? roAging { get; set; }

        [Column(TypeName = "date")]
        public DateTime? nextServiceDateMilage { get; set; }

        [Column(TypeName = "date")]
        public DateTime? nextservicedatetenure { get; set; }

        [StringLength(100)]
        public string forecastlogic { get; set; }

        [StringLength(100)]
        public string model { get; set; }

        [StringLength(250)]
        public string loststatus { get; set; }

        public bool? activestatus { get; set; }

        public long? customer_id { get; set; }

        public long? vehicle_id { get; set; }

        [StringLength(100)]
        public string phonenumber { get; set; }

        public long? lastMileage { get; set; }

        [Column(TypeName = "date")]
        public DateTime? lastvisitdate { get; set; }

        [StringLength(100)]
        public string vehicleregnum { get; set; }

        [Column(TypeName = "date")]
        public DateTime? createddate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? updateddate { get; set; }

        public int? noShowPeriod { get; set; }

        public int? ServiceTypeId { get; set; }

        [StringLength(100)]
        public string lastServicetype { get; set; }

        [StringLength(250)]
        public string address { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Lastservicedate { get; set; }

        public bool? assigned { get; set; }

        public long? campaign_id { get; set; }

        public long? cremanagerid { get; set; }

        public long? uploadid { get; set; }

        [Column(TypeName = "bit")]
        public bool? dnd { get; set; }

        public int? nowshowPeriod { get; set; }
        public string saledealercode { get; set; }
        public long modeltype { get; set; }
        public string todaymillage { get; set; }
    }
}
