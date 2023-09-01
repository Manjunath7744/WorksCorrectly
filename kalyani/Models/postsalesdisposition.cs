using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("postsalesdisposition")]

    public class postsalesdisposition
    {
        public long id { get; set; }
        public string isContacted { get; set; }
        public string PSFDispositon { get; set; }
        public string whatcustsays { get; set; }
        public long? callDispositionData_id { get; set; }

        public long? callInteraction_id { get; set; }


        public string psfquestions_id { get; set; }
        public string OtherComments { get; set; }
        public string remarks { get; set; }
        public string insurancelocation { get; set; }
        public string postsalesFollowUpTime { get; set; }
        public DateTime? postsalesFollowUpDate { get; set; }
        public string q1 { get; set; }
        public string q2 { get; set; }
        public string q3 { get; set; }
        public string q4 { get; set; }
        public string q5 { get; set; }
        public string q6 { get; set; }
        public string q7 { get; set; }
        public string q8 { get; set; }
        public string q9 { get; set; }
        public string q10 { get; set; }
        public string q11 { get; set; }
        public string q12 { get; set; }
        public string q13 { get; set; }
        public string q14 { get; set; }
        public string q15 { get; set; }
        public string q16 { get; set; }
        public string q17 { get; set; }
        public string q18 { get; set; }
        public string q19 { get; set; }
        public string q20 { get; set; }
        public string q21 { get; set; }
        public string q22 { get; set; }
        public string q23 { get; set; }
        public string q24 { get; set; }
        public string q25 { get; set; }
        public string q26 { get; set; }
        public string q27 { get; set; }
        public string q28 { get; set; }
        public string q29 { get; set; }
        public string q30 { get; set; }
        public string q31 { get; set; }
        [NotMapped]
        public string workshop { get; set; }

        public string isComplaintRaised { get; set; }
        public string dispositionFrom { get; set; }
        public long? complaintMgr_id { get; set; }
        public DateTime? complaintDate { get; set; }
        public DateTime? complaintResolvedDate { get; set; }
        [NotMapped]
        public string isresolvedorpending { get; set; }
        public string CREOtherComments { get; set; }
        
        public long campaignid { get; set; }
        public string psfformat  { get; set; }
    }
}