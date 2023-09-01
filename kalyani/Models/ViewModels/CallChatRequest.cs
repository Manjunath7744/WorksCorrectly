using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class CallChatRequest
    {
        public string task_name { get; set; }
        public string extra { get; set; }
        public  List<TaskBody> task_body { get; set; }
        
        
    }
    public class TaskBody
    {
            public string client_number { get; set; }
            public string receiver_number { get; set; }
            public TemplateData template_data { get; set; }
        
    }
    public class TemplateData
    {
        public string template_id { get; set; }
        public IDictionary<string, string> param_data { get; set; }
    }
   
}