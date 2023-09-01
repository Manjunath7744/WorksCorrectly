using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("ROAge")]
    public class ROAge
    {
        [Key]
        public int ROAgeValue { get; set; }
        public string ROAgeName { get; set; }
    }
    [Table("CRECategory")]
    public class CRECategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
    [Table("CREFieldOne")]
    public class CREFieldOne
    {
        [Key]
        public int FieldOneId { get; set; }
        public string FieldOneName { get; set; }

    }
    [Table("CREFieldTwo")]
    public class CREFieldTwo
    {
        [Key]
        public int FieldTwoId { get; set; }
        public string FieldTwoName { get; set; }

    }
    [Table("CREUserAdditionalFieldsMapper")]

    public class CREUserAdditionalFieldsMapper
    {
        [Key]
        public int UserAdditionalFieldsMapperId { get; set; }
        public int? CREUserId { get; set; }
        public string CampaignId { get; set; }
        public string ServiceTypeId { get; set; }
        public string ROAgeingId { get; set; }
        public string CategoryId { get; set; }
        public int? FieldOneId { get; set; }
        public int? FieldTwoId { get; set; }
    }

    [Table("ExcludeNegativeDiposition")]
    public class ExcludeNegativeDiposition
    {
        [Key]
        public int ExcludeNegativeDipositionID { get; set; }
        public string ExcludeNegativeDipositionName { get; set; }
        public string ExcludeNegativeDipositionValue { get; set; }

    }
}