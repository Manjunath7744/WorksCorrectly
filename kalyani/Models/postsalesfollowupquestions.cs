using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("postsalesfollowupquestions")]
    public class postsalesfollowupquestions
    {
        [Key]
        //public int id { get; set; }

        //public int question_no { get; set; }

        //public string question { get; set; }

        //public string display_type { get; set; } // front-end tags(radio,ddl,check,text)

        //public string ddl_range { get; set; } // for range 0,3 or 1,10

        //public string ddl_options { get; set; } // other than numeric range

        //public string radio_options { get; set; }

        //public string checkbox_options { get; set; }

        //public int campaignid { get; set; }

        //public string binding_var { get; set; }

        //public string visited_cust_cat { get; set; }

        //public string pickup_cust_cat { get; set; }
        //public string psf_format { get; set; }

        //public bool qs_mandatory { get; set; }

        //public bool isActive { get; set; }

        //[NotMapped]
        //public bool IsDDLNumeric { get; set; }

        //[NotMapped]
        //public List<string> DDLOptionList { get; set; }

        //[NotMapped]
        //public List<string> RadioList { get; set; }

        //[NotMapped]
        //public List<string> CheckBoxOptions { get; set; }

        //[NotMapped]
        //public string SectionName { get; set; }

        //[NotMapped]
        //public int ddlmin { get; set; }

        //[NotMapped]
        //public int ddlmax { get; set; }

        //[NotMapped]
        //public string ans { get; set; }
        //public string defaultVaues { get; set; }


        public int id { get; set; }
        public int question_no { get; set; }

        public string question { get; set; }

        public string display_type { get; set; }

        public string ddl_values { get; set; }

        public string ddl_text { get; set; }

        public string radio_options { get; set; }
        public string radio_values { get; set; }

        public string binding_var { get; set; }

        public bool qs_mandatory { get; set; }

        public bool isActive { get; set; }
        public string defaultvalues { get; set; }
        public int campaignid { get; set; }
        public string psf_format { get; set; }
        public string dissatisfiedvalue { get; set; }


        [NotMapped]
        public List<string> DDLOptionValueList { get; set; }
        [NotMapped]

        public List<string> DDLOptionTextList { get; set; }
        [NotMapped]
        public Dictionary<string, string> dictionaryDDLQuestionList { get; set; }


        [NotMapped]
        public List<string> RadioTextList { get; set; }
        public List<string> RadioValueList { get; set; }
        public Dictionary<string, string> dictionaryRDOQuestionList { get; set; }


    }
}