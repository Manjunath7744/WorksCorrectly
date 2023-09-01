using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class ModuleType
    {
        public static long SERVICE = 1;
        public static long INSURANCE = 2;
        public static long SERVICE_INSURANCE = 3;
        public static long PSF = 4;

        public long id { get; set; }

        public long moduleId { get; set; }

        public string moduleName { get; set; }

    }
}