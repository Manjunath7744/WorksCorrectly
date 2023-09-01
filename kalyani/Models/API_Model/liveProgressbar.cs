using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.API_Model
{
    public class liveProgressbar
    {
        public  void SendProgress(string connectionId,string progressMessage, int progressCount, int totalItems)
        {
            //IN ORDER TO INVOKE SIGNALR FUNCTIONALITY DIRECTLY FROM SERVER SIDE WE MUST USE THIS
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ProgressHub>();
            long percentage=0;
            //CALCULATING PERCENTAGE BASED ON THE PARAMETERS SENT

            if (totalItems==0)
            {
                 percentage = 0;
            }
            else
            {
                 percentage = (progressCount * 100) / totalItems;

            }

            //PUSHING DATA TO ALL CLIENTS
            //hubContext.Clients.All.AddProgress(progressMessage, percentage + "%");

            hubContext.Clients.Client(connectionId).AddProgress(progressMessage, percentage + "%");


        }
    }
}