using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    public class wyzLoop
    {
        public  DateTime ConvertDateTime_Format(string Date)
        {
            string[] formats = new[]
                            {
                                "dd.MM.yyyy HH:mm:ss",
                                "dd-MM-yyyy HH:mm:ss",
                                "dd/MM/yyyy HH:mm:ss",
                                "MM/dd/yyyy HH:mm:ss",
                                "MM-dd-yyyy HH:mm:ss",
                                "MM.dd.yyyy HH:mm:ss",
                                "yyyy/MM/dd HH:mm:ss",
                                "yyyy-MM-dd HH:mm:ss",
                                "yyyy.MM.dd HH:mm:ss",
                                "yyyy/dd/MM HH:mm:ss",
                                "yyyy-dd-MM HH:mm:ss",
                                "yyyy.dd.MM HH:mm:ss",
                                "d.M.yyyy HH:mm:ss",
                                "d-M-yyyy HH:mm:ss",
                                "d/M/yyyy HH:mm:ss",
                                "M/d/yyyy HH:mm:ss",
                                "M-d-yyyy HH:mm:ss",
                                "M.d.yyyy HH:mm:ss",
                                "yyyy/M/d HH:mm:ss",
                                "yyyy-M-d HH:mm:ss",
                                "yyyy.M.d HH:mm:ss",
                                "yyyy/d/M HH:mm:ss",
                                "yyyy-d-M HH:mm:ss",
                                "yyyy.d.M HH:mm:ss",
                                "dd.MM.yyyy hh:mm:ss tt",
                                "dd-MM-yyyy hh:mm:ss tt",
                                "dd/MM/yyyy hh:mm:ss tt",
                                "dd/MM/yyyy h:m:s tt",
                                "d/M/yyyy hh:mm:ss tt",
                                "d/M/yyyy h:m:s tt",
                                "MM/dd/yyyy hh:mm:ss tt",
                                "M/d/yyyy hh:mm:ss tt",
                                "M/d/yyyy h:m:s tt",
                                "MM/dd/yyyy h:m:s tt",
                                "MM-dd-yyyy hh:mm:ss tt",
                                "MM.dd.yyyy hh:mm:ss tt",
                                "yyyy/MM/dd hh:mm:ss tt",
                                "yyyy-MM-dd hh:mm:ss tt",
                                "yyyy.MM.dd hh:mm:ss tt",
                                "yyyy/dd/MM hh:mm:ss tt",
                                "yyyy-dd-MM hh:mm:ss tt",
                                "yyyy.dd.MM hh:mm:ss tt",
                                "d.M.yyyy hh:mm:ss tt",
                                "d-M-yyyy hh:mm:ss tt",
                                "d/M/yyyy hh:mm:ss tt",
                                "M/d/yyyy hh:mm:ss tt",
                                "M-d-yyyy hh:mm:ss tt",
                                "M.d.yyyy hh:mm:ss tt",
                                "yyyy/M/d hh:mm:ss tt",
                                "yyyy-M-d hh:mm:ss tt",
                                "yyyy.M.d hh:mm:ss tt",
                                "yyyy/d/M hh:mm:ss tt",
                                "yyyy-d-M hh:mm:ss tt",
                                "yyyy.d.M hh:mm:ss tt",
                                "dd.MM.yyyy HH:mm:ss",
                                "dd-MM-yyyy HH:mm:ss",
                                "dd/MM/yyyy HH:mm:ss",
                                "MM/dd/yyyy HH:mm:ss",
                                "MM-dd-yyyy HH:mm:ss",
                                "MM.dd.yyyy HH:mm:ss",
                                "yyyy/MM/dd HH:mm:ss",
                                "yyyy-MM-dd HH:mm:ss",
                                "yyyy.MM.dd HH:mm:ss",
                                "yyyy/dd/MM HH:mm:ss",
                                "yyyy-dd-MM HH:mm:ss",
                                "yyyy.dd.MM HH:mm:ss",
                                "d.M.yyyy HH:mm",
                                "d-M-yyyy HH:mm",
                                "d/M/yyyy HH:mm",
                                "M/d/yyyy HH:mm",
                                "M-d-yyyy HH:mm",
                                "M.d.yyyy HH:mm",
                                "yyyy/M/d HH:mm",
                                "yyyy-M-d HH:mm",
                                "yyyy.M.d HH:mm",
                                "yyyy/d/M HH:mm",
                                "yyyy-d-M HH:mm",
                                "yyyy.d.M HH:mm",
                                "dd/MM/yyyy",
                                "MM-dd-yyyy"
                            };
            DateTime FinalDate = DateTime.ParseExact(Date, formats, System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None);
            return FinalDate;
        }
    }
}