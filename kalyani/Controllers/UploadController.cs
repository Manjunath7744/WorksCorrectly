using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using AutoSherpa_project.Models;
using AutoSherpa_project.Models.ViewModels;
using MySql.Data.MySqlClient;
using OfficeOpenXml;

namespace AutoSherpa_project.Controllers
{
    [AuthorizeFilter]
    public class UploadController : Controller
    {
        public ActionResult uploadReport()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    var ddluploadType = db.uploadtypes.Select(m => new { id = m.id, uploadName = m.uploadTypeName }).ToList();
                    ViewBag.ddluploadType = ddluploadType;
                }
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        public ActionResult getAllUploads(string uploadType, DataTablesParam param)
        {
            long userId = Convert.ToInt64(Session["UserId"]);
            int pageNo = 1;
            int totalCount = 0;

            List<uploadExcel> list_uploads = new List<uploadExcel>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (Session["UserRole"].ToString()=="CREManager")
                    {
                        if (param.iDisplayStart >= param.iDisplayLength)
                        {
                            pageNo = (param.iDisplayStart / param.iDisplayLength) + 1;
                        }


                        if (param.sSearch != null)
                        {
                            if (uploadType == "--Select--")
                            {
                                totalCount = db.uploads.Where(p => p.uploadedBy_id == userId && (p.fileName.Contains(param.sSearch) || p.processingStatus.Contains(param.sSearch))).Count();
                                list_uploads = db.uploads.Where(p => p.uploadedBy_id == userId && (p.fileName.Contains(param.sSearch) || p.processingStatus.Contains(param.sSearch))).OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new uploadExcel
                                {
                                    fileName = p.fileName,
                                    uploadPath = p.uploadPath,
                                    uploadedBy_id = p.uploadedBy_id,
                                    processingStatus = p.processingStatus,
                                    uploadedDateTime = p.uploadedDateTime,
                                    numberProcessed = p.numberProcessed,
                                    numberDiscarded = p.numberDiscarded,
                                    error = p.discardedEntriesPath,
                                    processingStartedDT = p.processingStartedDT,
                                    processingEndedDT = p.processingEndedDT
                                }).OrderByDescending(p => p.uploadedDateTime).ToList();
                                for (int i = 0; i < list_uploads.Count(); i++)
                                {
                                    long mangerId = (long)list_uploads[i].uploadedBy_id;
                                    list_uploads[i].managerName = db.wyzusers.FirstOrDefault(m => m.id == mangerId).userName;
                                    list_uploads[i].uploadPath = list_uploads[i].uploadPath.Substring(list_uploads[i].uploadPath.IndexOf("UploadedFiles"));
                                    if (list_uploads[i].error != null)
                                    {
                                        list_uploads[i].error = list_uploads[i].error.Substring(list_uploads[i].error.IndexOf("UploadedFiles"));
                                    }
                                }
                            }
                            else
                            {
                                long? ids = db.uploadtypes.FirstOrDefault(x => x.uploadTypeName == uploadType).id;
                                totalCount = db.uploads.Where(x => (x.uploadType_id == ids && x.uploadedBy_id == userId) && (x.fileName.Contains(param.sSearch) || x.processingStatus.Contains(param.sSearch))).Count();
                                list_uploads = db.uploads.Where(x => (x.uploadType_id == ids && x.uploadedBy_id == userId) && (x.fileName.Contains(param.sSearch) || x.processingStatus.Contains(param.sSearch))).OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new uploadExcel
                                {
                                    fileName = p.fileName,
                                    uploadPath = p.uploadPath,
                                    uploadedBy_id = p.uploadedBy_id,
                                    processingStatus = p.processingStatus,
                                    uploadedDateTime = p.uploadedDateTime,
                                    numberProcessed = p.numberProcessed,
                                    numberDiscarded = p.numberDiscarded,
                                    processingError = p.processingError,
                                    error = p.discardedEntriesPath,
                                    processingStartedDT = p.processingStartedDT,
                                    processingEndedDT = p.processingEndedDT
                                }).OrderByDescending(p => p.uploadedDateTime).ToList();

                                for (int i = 0; i < list_uploads.Count(); i++)
                                {
                                    long mangerId = (long)list_uploads[i].uploadedBy_id;
                                    list_uploads[i].managerName = db.wyzusers.FirstOrDefault(m => m.id == mangerId).userName;
                                    list_uploads[i].uploadPath = list_uploads[i].uploadPath.Substring(list_uploads[i].uploadPath.IndexOf("UploadedFiles"));
                                    if (list_uploads[i].error != null)
                                    {
                                        list_uploads[i].error = list_uploads[i].error.Substring(list_uploads[i].error.IndexOf("UploadedFiles"));
                                    }
                                }

                            }

                        }

                        else
                        {
                            if (uploadType == "--Select--")
                            {
                                totalCount = db.uploads.Where(m => m.uploadedBy_id == userId).Count();
                                list_uploads = db.uploads.Where(m => m.uploadedBy_id == userId).OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new uploadExcel
                                {
                                    fileName = p.fileName,
                                    uploadPath = p.uploadPath,
                                    uploadedBy_id = p.uploadedBy_id,
                                    processingStatus = p.processingStatus,
                                    uploadedDateTime = p.uploadedDateTime,
                                    numberProcessed = p.numberProcessed,
                                    numberDiscarded = p.numberDiscarded,
                                    error = p.discardedEntriesPath,
                                    processingStartedDT = p.processingStartedDT,
                                    processingEndedDT = p.processingEndedDT
                                }).OrderByDescending(p => p.uploadedDateTime).ToList();
                                for (int i = 0; i < list_uploads.Count(); i++)
                                {
                                    long mangerId = (long)list_uploads[i].uploadedBy_id;
                                    list_uploads[i].managerName = db.wyzusers.FirstOrDefault(m => m.id == mangerId).userName;
                                    list_uploads[i].uploadPath = list_uploads[i].uploadPath.Substring(list_uploads[i].uploadPath.IndexOf("UploadedFiles"));
                                    if (list_uploads[i].error != null)
                                    {
                                        list_uploads[i].error = list_uploads[i].error.Substring(list_uploads[i].error.IndexOf("UploadedFiles"));
                                    }
                                }
                            }
                            else
                            {
                                long? ids = db.uploadtypes.FirstOrDefault(x => x.uploadTypeName == uploadType).id;
                                totalCount = db.uploads.Where(x => x.uploadType_id == ids && x.uploadedBy_id == userId).Count();
                                list_uploads = db.uploads.Where(x => x.uploadType_id == ids && x.uploadedBy_id == userId).OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new uploadExcel
                                {
                                    fileName = p.fileName,
                                    uploadPath = p.uploadPath,
                                    uploadedBy_id = p.uploadedBy_id,
                                    processingStatus = p.processingStatus,
                                    uploadedDateTime = p.uploadedDateTime,
                                    numberProcessed = p.numberProcessed,
                                    numberDiscarded = p.numberDiscarded,
                                    processingError = p.processingError,
                                    error = p.discardedEntriesPath,
                                    processingStartedDT = p.processingStartedDT,
                                    processingEndedDT = p.processingEndedDT
                                }).OrderByDescending(p => p.uploadedDateTime).ToList();

                                for (int i = 0; i < list_uploads.Count(); i++)
                                {
                                    long mangerId = (long)list_uploads[i].uploadedBy_id;
                                    list_uploads[i].managerName = db.wyzusers.FirstOrDefault(m => m.id == mangerId).userName;
                                    list_uploads[i].uploadPath = list_uploads[i].uploadPath.Substring(list_uploads[i].uploadPath.IndexOf("UploadedFiles"));
                                    if (list_uploads[i].error != null)
                                    {
                                        list_uploads[i].error = list_uploads[i].error.Substring(list_uploads[i].error.IndexOf("UploadedFiles"));
                                    }
                                }

                            }
                        }

                        return Json(new { aaData = list_uploads, sEcho = param.sEcho, iTotalDisplayRecords = totalCount, iTotalRecords = totalCount }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        if (param.iDisplayStart >= param.iDisplayLength)
                        {
                            pageNo = (param.iDisplayStart / param.iDisplayLength) + 1;
                        }


                        if (param.sSearch != null)
                        {
                            if (uploadType == "--Select--")
                            {
                                totalCount = db.uploads.Where(p => p.fileName.Contains(param.sSearch) || p.processingStatus.Contains(param.sSearch)).Count();
                                list_uploads = db.uploads.Where(p => p.fileName.Contains(param.sSearch) || p.processingStatus.Contains(param.sSearch)).OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new uploadExcel
                                {
                                    fileName = p.fileName,
                                    uploadPath = p.uploadPath,
                                    uploadedBy_id = p.uploadedBy_id,
                                    processingStatus = p.processingStatus,
                                    uploadedDateTime = p.uploadedDateTime,
                                    numberProcessed = p.numberProcessed,
                                    numberDiscarded = p.numberDiscarded,
                                    error = p.discardedEntriesPath,
                                    processingStartedDT = p.processingStartedDT,
                                    processingEndedDT = p.processingEndedDT
                                }).OrderByDescending(p => p.uploadedDateTime).ToList();
                                for (int i = 0; i < list_uploads.Count(); i++)
                                {
                                    long mangerId = (long)list_uploads[i].uploadedBy_id;
                                    list_uploads[i].managerName = db.wyzusers.FirstOrDefault(m => m.id == mangerId).userName;
                                    list_uploads[i].uploadPath = list_uploads[i].uploadPath.Substring(list_uploads[i].uploadPath.IndexOf("UploadedFiles"));
                                    if (list_uploads[i].error != null)
                                    {
                                        list_uploads[i].error = list_uploads[i].error.Substring(list_uploads[i].error.IndexOf("UploadedFiles"));
                                    }
                                }
                            }
                            else
                            {
                                long? ids = db.uploadtypes.FirstOrDefault(x => x.uploadTypeName == uploadType).id;
                                totalCount = db.uploads.Where(x => x.uploadType_id == ids && (x.fileName.Contains(param.sSearch) || x.processingStatus.Contains(param.sSearch))).Count();
                                list_uploads = db.uploads.Where(x => x.uploadType_id == ids && (x.fileName.Contains(param.sSearch) || x.processingStatus.Contains(param.sSearch))).OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new uploadExcel
                                {
                                    fileName = p.fileName,
                                    uploadPath = p.uploadPath,
                                    uploadedBy_id = p.uploadedBy_id,
                                    processingStatus = p.processingStatus,
                                    uploadedDateTime = p.uploadedDateTime,
                                    numberProcessed = p.numberProcessed,
                                    numberDiscarded = p.numberDiscarded,
                                    processingError = p.processingError,
                                    error = p.discardedEntriesPath,
                                    processingStartedDT = p.processingStartedDT,
                                    processingEndedDT = p.processingEndedDT
                                }).OrderByDescending(p => p.uploadedDateTime).ToList();

                                for (int i = 0; i < list_uploads.Count(); i++)
                                {
                                    long mangerId = (long)list_uploads[i].uploadedBy_id;
                                    list_uploads[i].managerName = db.wyzusers.FirstOrDefault(m => m.id == mangerId).userName;
                                    list_uploads[i].uploadPath = list_uploads[i].uploadPath.Substring(list_uploads[i].uploadPath.IndexOf("UploadedFiles"));
                                    if (list_uploads[i].error != null)
                                    {
                                        list_uploads[i].error = list_uploads[i].error.Substring(list_uploads[i].error.IndexOf("UploadedFiles"));
                                    }
                                }

                            }

                        }

                        else
                        {
                            if (uploadType == "--Select--")
                            {
                                totalCount = db.uploads.Count();
                                list_uploads = db.uploads.OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new uploadExcel
                                {
                                    fileName = p.fileName,
                                    uploadPath = p.uploadPath,
                                    uploadedBy_id = p.uploadedBy_id,
                                    processingStatus = p.processingStatus,
                                    uploadedDateTime = p.uploadedDateTime,
                                    numberProcessed = p.numberProcessed,
                                    numberDiscarded = p.numberDiscarded,
                                    error = p.discardedEntriesPath,
                                    processingStartedDT = p.processingStartedDT,
                                    processingEndedDT = p.processingEndedDT
                                }).OrderByDescending(p => p.uploadedDateTime).ToList();
                                for (int i = 0; i < list_uploads.Count(); i++)
                                {
                                    long mangerId = (long)list_uploads[i].uploadedBy_id;
                                    list_uploads[i].managerName = db.wyzusers.FirstOrDefault(m => m.id == mangerId).userName;
                                    list_uploads[i].uploadPath = list_uploads[i].uploadPath.Substring(list_uploads[i].uploadPath.IndexOf("UploadedFiles"));
                                    if (list_uploads[i].error != null)
                                    {
                                        list_uploads[i].error = list_uploads[i].error.Substring(list_uploads[i].error.IndexOf("UploadedFiles"));
                                    }
                                }
                            }
                            else
                            {
                                long? ids = db.uploadtypes.FirstOrDefault(x => x.uploadTypeName == uploadType).id;
                                totalCount = db.uploads.Where(x => x.uploadType_id == ids).Count();
                                list_uploads = db.uploads.Where(x => x.uploadType_id == ids).OrderByDescending(x => x.id).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).Select(p => new uploadExcel
                                {
                                    fileName = p.fileName,
                                    uploadPath = p.uploadPath,
                                    uploadedBy_id = p.uploadedBy_id,
                                    processingStatus = p.processingStatus,
                                    uploadedDateTime = p.uploadedDateTime,
                                    numberProcessed = p.numberProcessed,
                                    numberDiscarded = p.numberDiscarded,
                                    processingError = p.processingError,
                                    error = p.discardedEntriesPath,
                                    processingStartedDT = p.processingStartedDT,
                                    processingEndedDT = p.processingEndedDT
                                }).OrderByDescending(p => p.uploadedDateTime).ToList();

                                for (int i = 0; i < list_uploads.Count(); i++)
                                {
                                    long mangerId = (long)list_uploads[i].uploadedBy_id;
                                    list_uploads[i].managerName = db.wyzusers.FirstOrDefault(m => m.id == mangerId).userName;
                                    list_uploads[i].uploadPath = list_uploads[i].uploadPath.Substring(list_uploads[i].uploadPath.IndexOf("UploadedFiles"));
                                    if (list_uploads[i].error != null)
                                    {
                                        list_uploads[i].error = list_uploads[i].error.Substring(list_uploads[i].error.IndexOf("UploadedFiles"));
                                    }
                                }

                            }
                        }

                        return Json(new { aaData = list_uploads, sEcho = param.sEcho, iTotalDisplayRecords = totalCount, iTotalRecords = totalCount }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { aaData = list_uploads, sEcho = param.sEcho, iTotalDisplayRecords = totalCount, iTotalRecords = totalCount }, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public ActionResult downloadFile(string path)
        //{
        //    try
        //    {


        //        var filePath = path;
        //        var fileName = "ExcellData.xlsx";
        //        //Path.GetFileName(filePath);
        //        var mimeType = "application/octet-stream";
        //        return File(new FileStream(filePath, FileMode.Open), mimeType, fileName);


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return Json(new { }, JsonRequestBehavior.AllowGet);
        //    //return File(new FileStream(path, FileMode.OpenOrCreate), "application/octet-stream", "Nodata.xlsx");
        //}
    }

    public class DataTablesParam
    {
        public int iDisplayStart { get; set; }
        public int iDisplayLength { get; set; }
        public int iColumns { get; set; }
        public string sSearch { get; set; }
        public bool bEscapeRegex { get; set; }
        public int iSortingCols { get; set; }
        public int sEcho { get; set; }
        public List<string> sColumnNames { get; set; }
        public List<bool> bSortable { get; set; }
        public List<bool> bSearchable { get; set; }
        public List<string> sSearchValues { get; set; }
        public List<int> iSortCol { get; set; }
        public List<string> sSortDir { get; set; }
        public List<bool> bEscapeRegexColumns { get; set; }

        public DataTablesParam()
        {
            sColumnNames = new List<string>();
            bSortable = new List<bool>();
            bSearchable = new List<bool>();
            sSearchValues = new List<string>();
            iSortCol = new List<int>();
            sSortDir = new List<string>();
            bEscapeRegexColumns = new List<bool>();
        }

        public DataTablesParam(int iColumns)
        {
            this.iColumns = iColumns;
            sColumnNames = new List<string>(iColumns);
            bSortable = new List<bool>(iColumns);
            bSearchable = new List<bool>(iColumns);
            sSearchValues = new List<string>(iColumns);
            iSortCol = new List<int>(iColumns);
            sSortDir = new List<string>(iColumns);
            bEscapeRegexColumns = new List<bool>(iColumns);
        }


    }
}