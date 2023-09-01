using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class WhatsAppInfoBip
    {
        public string callbackData { get; set; }
        public string scenarioKey { get; set; }
        public List<Destination> destinations { get; set; }
        public WhatsApp whatsApp { get; set; }
    }

    public class To
    {
        public string phoneNumber { get; set; }
    }

    public class Destination
    {
        public To to { get; set; }
    }

    public class Body
    {
        public Body()
        {
            placeholders = new List<string>();
        }
        public List<string> placeholders { get; set; }
    }

    public class MediaTemplateData
    {
        public MediaTemplateData()
        {
            body = new Body();
            buttons = new List<string>();
        }
        public Body body { get; set; }
        public List<string> buttons { get; set; }
    }

    public class WhatsApp
    {
        public WhatsApp()
        {
            mediaTemplateData = new MediaTemplateData();
        }
        public string templateName { get; set; }
        public MediaTemplateData mediaTemplateData { get; set; }
        public string language { get; set; }
    }

    //Classes for Response data capturing 

    public class InfoBipResponse
    {
        public InfoBipResponse()
        {
            messages = new List<Message>();
        }
        public List<Message> messages { get; set; }
    }

    public class Status
    {
        public int groupId { get; set; }
        public string groupName { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class Message
    {
        Message()
        {
            to = new To();
            status =new Status();
        }
        public To to { get; set; }
        public Status status { get; set; }
        public string messageId { get; set; }
    }

   
}