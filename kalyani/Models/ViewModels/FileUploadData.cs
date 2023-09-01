using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class FileUploadData
    {
		public long id { get; set; }
		public string name { get; set; }
		public long size{get;set;}
		public string url{get;set;}
		public string thumbnailUrl{get;set;}
		public string deleteUrl{get;set;}
		public string deleteType{get;set;}
		public string processUrl{get;set;}
		public string error { get;set;}
		public bool processed{get;set;}


	}
}