using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    public class PrutechBusinessApi
    {
      public List<Messages> Messages { get; set; }
      public string BulkId { get; set; }


    }

    public class Messages
    {
        public string From { get; set; }
        public string To { get; set; }
        public string MessageId { get; set; }
        public Content Content { get; set; }
        public string CallBackData { get; set; }
    }

    public class Content
    {
        public string TemplateName { get; set; }
        public TempData TemplateData { get; set; }
        public string Language { get; set; }
        
        

    }
    public class TempData
    {
        public TempBody Body { get; set; }
        
    }
    public class TempBody
    {
        public List<PlaceHolders> PlaceHolders { get; set; }
    }
    public class PlaceHolders
    {
        public string Type { get; set; }
        public string Text { get; set; }
    }
    

    public class PrutechToken
    {
        public string idToken { get; set; }
        public string expiresIn { get; set; }
    }
    public class PrutechResponse
    {
        public string status { get; set; }
        public string message { get; set; }
    }
   
   
    
}