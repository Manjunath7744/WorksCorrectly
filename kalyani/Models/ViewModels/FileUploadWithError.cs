using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class FileUploadWithError
    {
        public  long id { get; set; }
        public string name { get; set; }
        public long size { get; set; }
        public string error { get; set; }
    }
}