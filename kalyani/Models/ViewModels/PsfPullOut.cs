using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class PsfPullOut
    {
        public string JobCardNo { get; set; }

        public string PSFDay { get; set; }

        public long CubeId { get; set; }
        public int roboStatus { get; set; }
        public int isrobocall  { get; set; }
    }
}