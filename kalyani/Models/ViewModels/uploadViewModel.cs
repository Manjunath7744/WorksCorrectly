using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class uploadViewModel
    {
      public  fileUploads FileUploads = new fileUploads();
     //   public ViewDataUploadFilesResult[] Files { get; set; }

        public List<columndefinition> columndefinitions { get; set; }
        public List<uploadtype_columndefinition> uploadtype_Columndefinitions { get; set; }
    }
    //public class ViewDataUploadFilesResult
    //{
    //    public string name { get; set; }
    //    public int size { get; set; }
    //    public string type { get; set; }
    //    public string url { get; set; }
    //    public string deleteUrl { get; set; }
    //    public string processUrl { get; set; }
    //    public string thumbnailUrl { get; set; }
    //    public string deleteType { get; set; }
    //    public string setError { get; set; }
    //}
    public class JsonFiles
    {
        public FileUploadData[] files;
        public string TempFolder { get; set; }
        public string displayError { get; set; }
        public JsonFiles(List<FileUploadData> filesList)
        {
            files = new FileUploadData[filesList.Count];
            for (int i = 0; i < filesList.Count; i++)
            {
                files[i] = filesList.ElementAt(i);
            }

        }
    }
    public class getWorkshops
    {
        public string workshopCode{get;set;}
        public string workshopName{get;set;}
    }
    public partial class uploadExcel
    {

        public long id { get; set; }

        public string discardedEntriesPath { get; set; }

        public string error { get; set; }

        public string fileName { get; set; }

        public long numberDiscarded { get; set; }

        public long numberProcessed { get; set; }

        public bool processed { get; set; }

        public DateTime? processingEndedDT { get; set; }

        public bool processingError { get; set; }

        public bool processingStarted { get; set; }

        public DateTime? processingStartedDT { get; set; }

        public string processingStatus { get; set; }

        public long size { get; set; }

        public bool uploadError { get; set; }

        public string uploadPath { get; set; }

        public DateTime? uploadedDateTime { get; set; }

        public long? uploadType_id { get; set; }

        public long? uploadedBy_id { get; set; }

        public DateTime? validFrom { get; set; }

        public DateTime? validTo { get; set; }

        public long assignedRecords { get; set; }

        public long unAssignedRecords { get; set; }
        public string managerName { get; set; }


    }
    public class fileUploads
    {
        public string uploadType { get; set; }
        public string locType_id { get; set; }
        public string location_id { get; set; }
        public string workshop_id { get; set; }
        public string campaignName { get; set; }
        public string campaignFromDate { get; set; }
        public string campaignToDate { get; set; }

    }
    public class getStructure
    {
        public string COLUMN_NAME { get;set; }
        public string DATA_TYPE { get;set; }
    }

    //public class ViewDataUploadFilesResult
    //{
    //    public string name { get; set; }
    //    public int size { get; set; }
    //    public string type { get; set; }
    //    public string url { get; set; }
    //    public string deleteUrl { get; set; }
    //    public string thumbnailUrl { get; set; }
    //    public string deleteType { get; set; }
    //}
}