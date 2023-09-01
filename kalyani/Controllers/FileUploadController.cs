using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using AutoSherpa_project.Models;
using AutoSherpa_project.Models.ViewModels;
using MySql.Data.MySqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace AutoSherpa_project.Controllers
{
    [AuthorizeFilter]
    public class FileUploadController : Controller
    {

        public static string uploadTable;
        public bool same = true;

        // GET: FileUpload
        public ActionResult fileUpload()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    var uploadList = db.uploadtypes.Select(m => new { id = m.id, uploadtypes = m.uploadTypeName }).ToList();
                    ViewBag.uploadList = uploadList;
                    var locationList = db.locations.Select(m => new { id = m.cityId, locationName = m.name }).ToList();
                    ViewBag.locationList = locationList;
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public ActionResult startUpload()
        {

            try
            {
                var resultList = new List<FileUploadData>();

                var CurrentContext = HttpContext;

                var httpRequest = CurrentContext.Request;
                foreach (string inputTagName in httpRequest.Files)
                {

                    var headers = httpRequest.Headers;

                    var file = httpRequest.Files[inputTagName];
                    System.Diagnostics.Debug.WriteLine(file.FileName);

                    if (string.IsNullOrEmpty(headers["X-File-Name"]))
                    {
                        UploadWholeFile(CurrentContext, resultList);
                    }
                }
                JsonFiles files = new JsonFiles(resultList);

                bool isEmpty = !resultList.Any();

                if (isEmpty)
                {

                }
                else
                {
                    return Json(files);
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);

            }
            return Json(new { });
        }

        public ActionResult UploadWholeFile(HttpContextBase requestContext, List<FileUploadData> statuses)
        {
            //try
            //{
            string fileNameAppend = "_" + DateTime.Now.ToString("yyyyMMddhhmmss");
            long uploadId = Convert.ToInt64(requestContext.Request.Params["uploadType"]);
            DataTable originalColumn = new DataTable();
            DataTable excelColumn = new DataTable();

            var request = requestContext.Request;
            for (int i = 0; i < request.Files.Count; i++)
            {
                var file = request.Files[i];
                string fileUploadPath = Server.MapPath("~/UploadedFiles/" + Session["DealerCode"].ToString() + "/" + Session["UserName"].ToString() + "/");
                if (!(Directory.Exists(fileUploadPath)))
                {
                    DirectoryInfo di = Directory.CreateDirectory(fileUploadPath);
                }
                string filename = file.FileName.Split('.')[0];
                filename = Regex.Replace(filename, "[^a-zA-Z0-9_.]+", "");
                // string filename = file.FileName;
                string ext = System.IO.Path.GetExtension(file.FileName);
                //string ext = file.FileName.Split('.')[1];
                string final_name = filename + fileNameAppend + ext;
                string savingName = Path.GetFileName(file.FileName) + fileNameAppend;
                var fullPath = Path.Combine(fileUploadPath, final_name);
                string[] imageArray = file.FileName.Split('.');
                if (imageArray.Length != 0)
                {
                    string extansion = imageArray[imageArray.Length - 1].ToLower();
                    file.SaveAs(fullPath);
                    FileInfo existingFile = new FileInfo(fullPath);
                    using (ExcelPackage package = new ExcelPackage(existingFile))
                    {
                        using (var db = new AutoSherDBContext())
                        {
                            long id = db.uploadtypes.FirstOrDefault(x => x.id == uploadId).id;
                            List<long> columnDef = db.uploadtype_columndefinition.Where(m => m.UploadType_id == id).Select(m => m.definitions_id).ToList();
                            var sourceColoumns = db.columndefinitions.Where(m => columnDef.Contains(m.id) && m.additionalFormData == false).Select(m => new { dataType = m.columnDataType, name = m.sourceColumnName, meta = m.metaColumnName }).ToList();
                            foreach (var items in sourceColoumns)
                            {
                                originalColumn.Columns.Add((items.name).Trim(), typeof(string));
                            }
                        }
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        int colCount = worksheet.Dimension.End.Column;  //get Column Count
                        int maxRows = worksheet.Dimension.End.Row;
                        int rowCount = 1;     //get row count
                        for (int row = 1; row <= rowCount; row++)
                        {
                            for (int col = 1; col <= colCount; col++)
                            {
                                if (worksheet.Cells[row, col].Value != null && worksheet.Cells[row, col].Value.ToString().Trim().Length > 0)
                                {
                                    if (excelColumn.Columns.Contains(worksheet.Cells[row, col].Value.ToString().Trim()))
                                    {

                                    }
                                    else
                                    {
                                        excelColumn.Columns.Add(worksheet.Cells[row, col].Value?.ToString().Trim());
                                    }
                                }
                            }
                        }

                        if (excelColumn.Columns.Count != originalColumn.Columns.Count)
                        {
                            same = false;
                            var customErrors = "Columns in the uploaded files are lesser than expected";
                            statuses.Add(UploadResult(file.FileName, file.ContentLength, file.FileName, customErrors, 0));
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, customErrors);
                        }
                        else if (maxRows <= 1)
                        {
                            same = false;
                            var customErrors = "You cannot Upload Empty file..";
                            upload f = new upload();
                            f.error = customErrors;
                            statuses.Add(UploadResult(file.FileName, file.ContentLength, file.FileName, customErrors, 0));

                        }
                        DataTable d = CompareTwoDataTable(originalColumn, excelColumn);
                        if (d.Columns.Count > 0)
                        {
                            same = false;
                            var customErrors = "Columns in the uploaded files are changed, expected colum -  " + d.Columns[0].ColumnName;
                            upload f = new upload();
                            f.error = customErrors;
                            statuses.Add(UploadResult(file.FileName, file.ContentLength, file.FileName, customErrors, 0));
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, customErrors);
                        }
                        if (same)
                        {
                            using (var db = new AutoSherDBContext())
                            {
                                long userId = Convert.ToInt64(Session["UserId"]);

                                upload uploads = new upload();
                                uploads.fileName = file.FileName;
                                uploads.processingStartedDT = DateTime.Now;
                                uploads.processingStatus = "STARTED";
                                uploads.size = file.ContentLength;
                                uploads.uploadedDateTime = DateTime.Now;
                                uploads.uploadPath = fullPath;
                                uploads.uploadType_id = uploadId;
                                uploads.uploadedBy_id = userId;
                                db.uploads.Add(uploads);
                                db.SaveChanges();
                                long uId = uploads.id;
                                string[] keys = Request.Form.AllKeys;
                                for (int k = 0; k < keys.Length; k++)
                                {
                                    formdata formsData = new formdata();
                                    formsData.name = Request.Form.Keys[k];
                                    formsData.value = Request.Form[keys[k]];
                                    formsData.upload_id = uId;
                                    db.formdatas.Add(formsData);

                                }
                                db.SaveChanges();
                                statuses.Add(UploadResult(file.FileName, file.ContentLength, file.FileName, null, uId));
                            }
                        }
                    }

                }
            }
            //}
            //catch (Exception ex)
            //{

            //}
            return Json(new { });
        }

        #region Codes for uploading Excel file

        public static DataTable CompareTwoDataTable(DataTable orgialCol, DataTable excelCol)
        {
            DataTable d3 = new DataTable();

            try
            {

                for (int i = 0; i < orgialCol.Columns.Count; i++)
                {
                    for (int j = 0; j < excelCol.Columns.Count; j++)
                    {
                        if (orgialCol.Columns[i].ColumnName == excelCol.Columns[j].ColumnName)
                        {
                            break;
                        }
                        else if (excelCol.Columns.Count - 1 == j)
                        {
                            d3.Columns.Add(orgialCol.Columns[i].ColumnName);
                            return d3;
                        }
                    }
                }
                return d3;

            }
            catch (Exception ex)
            {

            }
            return d3;
        }

        public ActionResult getCampaignNamesByUpload(string uploadType)
        {
            List<CampaignAjaxLoad> campAjaxList = new List<CampaignAjaxLoad>();

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    List<campaign> campList = getCampaignListUploadType(uploadType);


                    if (campList.Count != 0)
                    {

                        foreach (campaign sa in campList)
                        {
                            CampaignAjaxLoad camp = new CampaignAjaxLoad();
                            camp.campId = sa.id;
                            camp.campName = sa.campaignName;
                            campAjaxList.Add(camp);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return Json(new { campAjaxList });
        }
        public List<campaign> getCampaignListUploadType(string uploadType)
        {
            List<campaign> camplist = new List<campaign>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    long CampaTypecount = db.uploadtypes.Count(m => m.uploadTypeName == uploadType);
                    if (CampaTypecount != 0)
                    {
                        string CampaType = db.uploadtypes.FirstOrDefault(m => m.uploadTypeName == uploadType).uploadDisplayName;
                        camplist = db.campaigns.Where(u => u.campaignType == CampaType && u.isactive == true).ToList();
                        return camplist;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return camplist;
        }
        public ActionResult getAssignmentPlanList(long? uploadTypeId, string campaignId, string workshopId, string campaignName)
        {
            string managerUsername = Session["UserName"].ToString();

            try
            {
                string moduleName = "";
                string workshopName = "";
                if (uploadTypeId == 4)
                {
                    moduleName = "SMR";
                }
                else if (uploadTypeId == 5)
                {
                    moduleName = "INS";
                }
                else
                {
                    workshopName = workshopId;
                    // campaignId = "";
                    moduleName = "PSF";
                }
                if (workshopId == "0")
                {
                    workshopId = "";
                }
                List<ChangeAssignment> assignPlanList = getAllAssignmentPlan(managerUsername, moduleName, campaignId, workshopId);
                long totalSize = assignPlanList.Count();
                List<ChangeAssignment> chAll = new List<ChangeAssignment>();

                foreach (ChangeAssignment c in assignPlanList)
                {
                    string campaign = "-";
                    if (c.campaignName != "" || c.campaignName != null)
                    {
                        campaign = moduleName + " - " + c.campaignName;
                    }
                    ChangeAssignment ch = new ChangeAssignment();
                    ch.campaignName = campaign;
                    ch.locationName = c.locationName;
                    ch.CreName = c.CreName;
                    chAll.Add(ch);
                }

                return Json(new { data = chAll, draw = Request["draw"], recordsTotal = chAll.Count(), recordsFiltered = chAll.Count() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }

            return null;
        }
        public ActionResult loadWorkshops()
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    var workshoList = db.workshops.Select(m => new { code = m.workshopCode, workshopName = m.workshopName }).ToList();
                    return Json(new { data = workshoList.OrderBy(m => m.workshopName) }, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { });
        }

        public List<ChangeAssignment> getAllAssignmentPlan(string managerLogin, string module, string campaignIds, string workshopId)
        {
            List<ChangeAssignment> resultLists = new List<ChangeAssignment>();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string str = @"CALL all_call_assignment_plan(@managerLogin,@inmodule,@campaignIds,@workshopId)";

                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("managerLogin", managerLogin),
                        new MySqlParameter("inmodule", module),
                        new MySqlParameter("campaignIds", campaignIds),
                        new MySqlParameter("workshopId", workshopId)

                    };
                    resultLists = db.Database.SqlQuery<ChangeAssignment>(str, param).ToList();

                }
            }
            catch (Exception)
            {

            }
            return resultLists;
        }
        public ActionResult listWorkshopsByIdAndType(long cityId, long locType)
        {

            try
            {
                using (var db = new AutoSherDBContext())
                {

                    if (locType == 1)
                    {
                        var workshopList = db.workshops.Where(user => user.location_cityId == cityId && user.issales == true).Select(user => new { id = user.id, name = user.workshopName }).ToList();
                        return Json(new { workshops = workshopList });

                    }
                    else if (locType == 2)
                    {
                        var workshopList = db.workshops.Where(user => user.location_cityId == cityId && user.issales == false).Select(user => new { id = user.id, name = user.workshopName }).ToList();
                        return Json(new { workshops = workshopList });

                    }
                }
            }
            catch (Exception ex)
            {

            }

            return Json(new { });
        }


        #region for downloading Sample Excel Sheet

        public ActionResult ExcelExport(long? utype, string name)
        {
            try
            {

                DataTable Dt = new DataTable();
                Dt = getColums(utype, name);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode(name + "_SampleExcel", System.Text.Encoding.UTF8));
                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");
                    ws.Cells["A1"].LoadFromDataTable(Dt, true);
                    //var ms = new System.IO.MemoryStream();
                    //pck.SaveAs(ms);
                    //  ms.WriteTo(Response.OutputStream);
                    string fileName = name + "_SampleExcel.xlsx";
                    Session["FileName"] = fileName;
                    Session["DownloadExcel_FileManager"] = pck.GetAsByteArray();
                }
            }
            catch (Exception ex)
            {
            }
            return Json(new { JsonRequestBehavior.AllowGet });
        }

        public DataTable getColums(long? uploadtypeID, string tblName)
        {
            List<string> content = new List<string>();

            DataTable dt = new DataTable();

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string isTableExit = db.Database.SqlQuery<string>("SHOW TABLES LIKE '" + tblName + "';").FirstOrDefault();
                    //Session["tblName"] = tblName;
                    List<long> columnDef = db.uploadtype_columndefinition.Where(m => m.UploadType_id == uploadtypeID).Select(m => m.definitions_id).ToList();
                    var sourceColoumns = db.columndefinitions.Where(m => columnDef.Contains(m.id) && m.additionalFormData == false).Select(m => new { dataType = m.columnDataType, name = m.sourceColumnName, pattern = m.dataPattern, destinationColumn = m.destinationColumnName, size = m.size }).ToList();
                    //var sourceColoumns = db.columndefinitions.Where(m => columnDef.Contains(m.id) && m.additionalFormData == false && m.id != 11186).Select(m => new { dataType = m.columnDataType, name = m.sourceColumnName, pattern = m.dataPattern, destinationColumn = m.destinationColumnName, size = m.size }).ToList();
                    DataRow dr = dt.NewRow();

                    if (isTableExit == null)
                    {
                        string tblCreationStart = string.Format("create table {0}(", tblName);
                        string tblColums = "id bigint(20) NOT NULL AUTO_INCREMENT,uploadid bigint(20) DEFAULT NULL, ";
                        foreach (var items in sourceColoumns)
                        {
                            if (items.dataType.ToLower() == "date" || items.dataType.ToLower() == "double")
                            {
                                tblColums = tblColums + string.Format("{0} {1} DEFAULT NULL,", "`" + items.destinationColumn + "`", items.dataType.ToLower(), items.size);
                            }
                            else
                            {
                                tblColums = tblColums + string.Format("{0} {1}({2}) DEFAULT NULL,", "`" + items.destinationColumn + "`", items.dataType.ToLower(), items.size);
                            }
                        }


                        //if (tblColums.Contains("ChassisNo"))
                        //{
                        //    tblColums = tblColums + " PRIMARY KEY (`id`),";
                        //    tblColums = tblColums + "KEY `ChassisNo` (`ChassisNo`)";
                        //}
                        //else if (tblColums.Contains("chassisno"))
                        //{
                        //    tblColums = tblColums + " PRIMARY KEY (`id`),";
                        //    tblColums = tblColums + "KEY `chassisno` (`chassisno`)";
                        //}
                        //else
                        //{
                        tblColums = tblColums + " PRIMARY KEY (`id`)";
                        //}
                        string finalSql = string.Empty;
                        finalSql = tblCreationStart + tblColums + ")ENGINE=InnoDB AUTO_INCREMENT=124823 DEFAULT CHARSET=utf8;";

                        int k = db.Database.ExecuteSqlCommand(finalSql);
                    }
                    foreach (var items in sourceColoumns)
                    {

                        dt.Columns.Add(items.name, typeof(string));
                        dr[items.name] = items.pattern;

                    }
                    dt.Rows.Add(dr.ItemArray);
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }
        public ActionResult Download()
        {
            try
            {
                string fileName = Session["FileName"].ToString();

                if (Session["DownloadExcel_FileManager"] != null)
                {
                    byte[] data = Session["DownloadExcel_FileManager"] as byte[];
                    return File(data, "application/octet-stream", fileName);
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {

            }
            return new EmptyResult();
        }

        #endregion

        #region Display Existing File by upload type
        public ActionResult getExistingFiles(string upType)
        {
            try
            {
                FileUploadWithError uploadError = new FileUploadWithError();
                List<FileUploadData> uploadresponse = new List<FileUploadData>();

                DateTime currentDate = DateTime.Now;
                string userName = Session["userId"].ToString();
                if (userName == null)
                { }
                List<upload> uploadList = getLast3DaysUploads(userName, currentDate, upType);
                if (uploadList == null)
                {
                }
                ArrayList files = new ArrayList();// Recommended
                uploadList.ForEach(up => { populateResponseArrayList(up, files); });
                FileUploadResponse response = new FileUploadResponse();
                return Json(new { files }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        private void populateResponseArrayList(upload upload, ArrayList files)
        {

            if (upload.uploadError)
            {
                FileUploadWithError uploadError = new FileUploadWithError();
                uploadError.id = upload.id;
                uploadError.name = upload.fileName;
                uploadError.size = upload.size;
                uploadError.error = upload.error;
                files.Add(uploadError);
            }
            else if (!upload.processed)
            {
                string siteRoot = string.Empty;

                if (Request.ApplicationPath != "/")
                {
                    siteRoot = Request.ApplicationPath;
                }

                FileUploadData uploadresponse = new FileUploadData();
                uploadresponse.id = upload.id;
                uploadresponse.name = upload.fileName;
                uploadresponse.size = upload.size;
                uploadresponse.deleteType = "DELETE";
                string deleteUrl = siteRoot + "/FileUpload/deleteFile/" + upload.id;
                uploadresponse.deleteUrl = deleteUrl;
                string downloadUrl = siteRoot + "/FileUpload/downloadFile/" + upload.uploadPath;
                uploadresponse.url = downloadUrl;
                string processUrl = siteRoot + "/FileUpload/wyzprocessFile/" + upload.id;
                uploadresponse.processUrl = processUrl;

                files.Add(uploadresponse);
            }
        }

        public List<upload> getLast3DaysUploads(string userName, DateTime currentDate, string uploadType)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    DateTime currenteDate = DateTime.UtcNow.Date.AddDays(-3);
                    var person = db.uploads.Where(m => m.uploadtype.uploadTypeName == uploadType && m.uploadedBy_id == (db.wyzusers.FirstOrDefault(w => w.userName == userName).id) && m.uploadedDateTime > currenteDate && m.processingStatus == "STARTED").ToList();
                    return person;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        #endregion

        public FileUploadData UploadResult(string FileName, int fileSize, string FileFullPath, string customErors, long Uid)
        {
            string getType = System.Web.MimeMapping.GetMimeMapping(FileFullPath);

            string siteRoot = string.Empty;
            if (Request.ApplicationPath != "/")
            {
                siteRoot = Request.ApplicationPath;
            }
            var result = new FileUploadData()
            {
                name = FileName,
                size = fileSize,
                url = siteRoot + "/FileUpload/downloadFile/" + Uid,
                deleteType = "DELETE",
                deleteUrl = siteRoot + "/FileUpload/deleteFile/" + Uid,
                processUrl = siteRoot + "/FileUpload/wyzprocessFile/" + Uid,
                error = customErors,

                id = Uid,
            };
            return result;
        }

        #endregion

        #region Saving File Uploads 


        public String BulkInsert(ref DataTable table, String table_name)
        {
            try
            {
                StringBuilder queryBuilder = new StringBuilder();
                DateTime dt;

                queryBuilder.AppendFormat("INSERT INTO `{0}` (", table_name);

                // more than 1 column required and 1 or more rows
                if (table.Columns.Count > 1 && table.Rows.Count > 0)
                {
                    // build all columns
                    queryBuilder.AppendFormat("`{0}`", table.Columns[0].ColumnName);

                    if (table.Columns.Count > 1)
                    {
                        for (int i = 1; i < table.Columns.Count; i++)
                        {
                            queryBuilder.AppendFormat(", `{0}` ", table.Columns[i].ColumnName);
                        }
                    }

                    queryBuilder.AppendFormat(") VALUES (", table_name);

                    // build all values for the first row
                    // escape String & Datetime values!
                    if (table.Columns[0].DataType == typeof(String))
                    {
                        queryBuilder.AppendFormat("'{0}'", MySqlHelper.EscapeString(table.Rows[0][table.Columns[0].ColumnName].ToString()));
                    }
                    else if (table.Columns[0].DataType == typeof(DateTime))
                    {
                        if (table.Rows[0][table.Columns[0].ColumnName].ToString() != "" && table.Rows[0][table.Columns[0].ColumnName] != DBNull.Value)
                        {
                            dt = (DateTime)table.Rows[0][table.Columns[0].ColumnName];
                            queryBuilder.AppendFormat("'{0}'", dt.ToString("yyyy-MM-dd"));
                        }
                        else
                        {
                            queryBuilder.AppendFormat(" null");

                        }
                    }
                    else if (table.Columns[0].DataType == typeof(decimal))
                    {
                        queryBuilder.AppendFormat("{0}", table.Rows[0].Field<decimal?>(table.Columns[0].ColumnName) ?? 0);
                    }
                    else
                    {
                        queryBuilder.AppendFormat(", {0}", table.Rows[0][table.Columns[0].ColumnName].ToString());
                    }

                    for (int i = 1; i < table.Columns.Count; i++)
                    {
                        // escape String & Datetime values!
                        if (table.Columns[i].DataType == typeof(String))
                        {
                            queryBuilder.AppendFormat(", '{0}'", MySqlHelper.EscapeString(table.Rows[0][table.Columns[i].ColumnName].ToString()));
                        }
                        else if (table.Columns[i].DataType == typeof(DateTime))
                        {
                            if (table.Rows[0][table.Columns[i].ColumnName].ToString() != "" && table.Rows[0][table.Columns[i].ColumnName] != DBNull.Value)
                            {
                                dt = (DateTime)table.Rows[0][table.Columns[i].ColumnName];
                                queryBuilder.AppendFormat(", '{0}'", dt.ToString("yyyy-MM-dd"));
                            }
                            else
                            {
                                queryBuilder.AppendFormat(", null");
                            }

                        }
                        else if (table.Columns[i].DataType == typeof(decimal))
                        {
                            queryBuilder.AppendFormat(", {0}", table.Rows[0].Field<decimal?>(table.Columns[i].ColumnName) ?? 0);
                        }
                        else
                        {
                            queryBuilder.AppendFormat(", {0}", table.Rows[0][table.Columns[i].ColumnName].ToString());
                        }
                    }

                    queryBuilder.Append(")");
                    queryBuilder.AppendLine();

                    // build all values all remaining rows
                    if (table.Rows.Count > 1)
                    {
                        // iterate over the rows
                        for (int row = 1; row < table.Rows.Count; row++)
                        {
                            // open value block
                            queryBuilder.Append(", (");

                            // escape String & Datetime values!
                            if (table.Columns[0].DataType == typeof(String))
                            {
                                queryBuilder.AppendFormat("'{0}'", MySqlHelper.EscapeString(table.Rows[row][table.Columns[0].ColumnName].ToString()));
                            }
                            else if (table.Columns[0].DataType == typeof(DateTime))
                            {
                                if (table.Rows[row][table.Columns[0].ColumnName].ToString() != "" && table.Rows[row][table.Columns[0].ColumnName] != DBNull.Value)
                                {
                                    dt = (DateTime)table.Rows[row][table.Columns[0].ColumnName];
                                    queryBuilder.AppendFormat("'{0}'", dt.ToString("yyyy-MM-dd"));
                                }
                                else
                                {
                                    queryBuilder.AppendFormat(" null");
                                }
                            }
                            else if (table.Columns[0].DataType == typeof(decimal))
                            {
                                queryBuilder.AppendFormat("{0}", table.Rows[row].Field<decimal?>(table.Columns[0].ColumnName) ?? 0);
                            }
                            else
                            {
                                queryBuilder.AppendFormat(", {0}", table.Rows[row][table.Columns[0].ColumnName].ToString());
                            }

                            for (int col = 1; col < table.Columns.Count; col++)
                            {
                                // escape String & Datetime values!
                                if (table.Columns[col].DataType == typeof(String))
                                {
                                    queryBuilder.AppendFormat(", '{0}'", MySqlHelper.EscapeString(table.Rows[row][table.Columns[col].ColumnName].ToString()));
                                }
                                else if (table.Columns[col].DataType == typeof(DateTime))
                                {
                                    if (table.Rows[row][table.Columns[col].ColumnName].ToString() != "" && table.Rows[row][table.Columns[col].ColumnName] != DBNull.Value)
                                    {
                                        dt = (DateTime)table.Rows[row][table.Columns[col].ColumnName];
                                        queryBuilder.AppendFormat(", '{0}'", dt.ToString("yyyy-MM-dd"));
                                    }
                                    else
                                    {
                                        queryBuilder.AppendFormat(", null");

                                    }
                                }
                                else if (table.Columns[col].DataType == typeof(decimal))
                                {
                                    queryBuilder.AppendFormat(", {0}", table.Rows[row].Field<decimal?>(table.Columns[col].ColumnName) ?? 0);
                                }
                                else
                                {
                                    queryBuilder.AppendFormat(", {0}", table.Rows[row][table.Columns[col].ColumnName].ToString());
                                }
                            } // end for (int i = 1; i < table.Columns.Count; i++)

                            // close value block
                            queryBuilder.Append(")");
                            queryBuilder.AppendLine();

                        } // end for (int r = 1; r < table.Rows.Count; r++)

                        // sql delimiter =)
                        queryBuilder.Append(";");

                    } // end if (table.Rows.Count > 1)

                    return queryBuilder.ToString();
                }
                else
                {
                    return "";
                } // end if(table.Columns.Count > 1 && table.Rows.Count > 0)
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }



        public ActionResult wyzprocessFile(string id)
        {
            DataTable finalTable = new DataTable();
            DataTable errorTable = new DataTable();
            upload upload = new upload();
            long Id = Convert.ToInt64(id);
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    db.Database.CommandTimeout = 600;
                    upload = db.uploads.FirstOrDefault(x => x.id == Id);
                    DataSet excelDataTables = new DataSet();
                    excelDataTables = GetDataTableFromExcel(upload.uploadPath, Id);
                    finalTable = excelDataTables.Tables[0];
                    errorTable = excelDataTables.Tables[1];

                    //DataColumn newCol = new DataColumn("uploadid", typeof(decimal));
                    //newCol.AllowDBNull = true;
                    //finalTable.Columns.Add(newCol);
                    //foreach (DataRow uprows in finalTable.Rows)
                    //{
                    //    uprows["uploadid"] = Id;
                    //}

                    uploadTable = db.uploadtypes.FirstOrDefault(x => x.id == upload.uploadType_id).uploadTypeName.ToLower();

                    #region oldSavings

                    //string columns = string.Join("`,`", finalTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName));

                    //if (finalTable.Rows.Count > 0)
                    //{
                    //    string connectionString = ConfigurationManager.ConnectionStrings["AutoSherContext"].ConnectionString;
                    //    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    //    {
                    //        connection.Open();
                    //        using (MySqlTransaction tran = connection.BeginTransaction())
                    //        {
                    //            using (MySqlCommand cmd = new MySqlCommand())
                    //            {
                    //                cmd.Connection = connection;
                    //                cmd.Transaction = tran;
                    //                cmd.CommandText = $"SELECT `" + columns + "` FROM " + uploadTable;

                    //                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    //                {
                    //                    adapter.UpdateBatchSize = 10000;
                    //                    using (MySqlCommandBuilder cb = new MySqlCommandBuilder(adapter))
                    //                    {
                    //                        cb.SetAllValues = true;
                    //                        adapter.Update(finalTable);
                    //                        tran.Commit();
                    //                    }
                    //                };
                    //            }
                    //        }
                    //    }
                    //}


                    //string columns = string.Join("','", finalTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName));
                    //string values = string.Join(",", finalTable.Columns.Cast<DataColumn>().Select(c => string.Format("@{0}", c.ColumnName)));
                    //string sqlCommandInsert = string.Format("INSERT INTO " + uploadTable + "({0}) VALUES ({1})", columns, values);

                    //int colCount = finalTable.Columns.Count;
                    //string conStr = ConfigurationManager.ConnectionStrings["AutoSherContext"].ConnectionString;
                    //MySqlConnection con = new MySqlConnection(conStr);
                    //con.Open();
                    //foreach (DataRow row in finalTable.Rows)
                    //{

                    //    string startingQuery = "Insert into " + uploadTable + " (";
                    //    string ColName = "", ParamName = "(";
                    //    int m = 0;
                    //    MySqlParameter[] pra = new MySqlParameter[(finalTable.Columns.Count + 1)];
                    //    MySqlCommand Mycmd = new MySqlCommand(sqlCommandInsert, con);
                    //    foreach (DataColumn col in finalTable.Columns)
                    //    {
                    //        if (!string.IsNullOrEmpty(row[col].ToString()) && !string.IsNullOrWhiteSpace(row[col].ToString()))
                    //        {
                    //            ColName = ColName + "`" + col.ColumnName + "`,";
                    //            ParamName = ParamName + "@" + col.ColumnName + ",";
                    //            MySqlParameter parameter = new MySqlParameter("@" + col.ColumnName, row[col]);
                    //            pra[m] = parameter;
                    //            m++;
                    //            Mycmd.Parameters.Add(parameter);
                    //        }
                    //    }
                    //    ColName = ColName.Substring(0, ColName.Length - 1) + ")";
                    //    ParamName = " Values " + ParamName.Substring(0, ParamName.Length - 1) + ")";
                    //    string finalQuery = startingQuery + ColName + ParamName;
                    //    Mycmd.CommandText = finalQuery;
                    //    int k = Mycmd.ExecuteNonQuery();

                    //}
                    #endregion


                    if (finalTable.Rows.Count > 0)
                    {
                        string rows = BulkInsert(ref finalTable, uploadTable);
                        string conStr = ConfigurationManager.ConnectionStrings["AutoSherContext"].ConnectionString;
                        //string conStr = ConfigurationManager.ConnectionStrings[AutoSherDBContext.getContextName()].ConnectionString;

                        using (MySqlConnection connection = new MySqlConnection(conStr))
                        {
                            connection.Open();
                            // MySqlTransaction transaction = connection.BeginTransaction();
                            //try
                            //{
                            MySqlCommand myCmd = new MySqlCommand(rows, connection);
                            myCmd.ExecuteNonQuery();
                            //   transaction.Commit();
                            //} // Here the execution is committed to the DB
                            //catch (Exception ex)
                            //{
                            //    transaction.Rollback();
                            //    throw;
                            //}
                            connection.Close();
                        }
                    }
                    int count = db.uploads.Count(c => c.id == Id);
                    if (errorTable.Rows.Count > 0)
                    {
                        string DeletefileUploadPath = Server.MapPath("~/UploadedFiles/" + Session["DealerCode"].ToString() + "/" + Session["UserName"].ToString() + "/error");

                        if (!(Directory.Exists(DeletefileUploadPath)))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(DeletefileUploadPath);
                        }
                        string sourceFile = upload.uploadPath;
                        string filename = Path.GetFileName(sourceFile);
                        string destinationFile = System.IO.Path.Combine(DeletefileUploadPath, filename);
                        DataSetToExcel(errorTable, destinationFile);

                        if (count > 0)
                        {
                            upload updateUpload = db.uploads.FirstOrDefault(d => d.id == Id);
                            updateUpload.processingEndedDT = DateTime.Now;
                            updateUpload.processed = false;
                            updateUpload.processingError = true;
                            if (finalTable.Rows.Count > 0)
                            {
                                updateUpload.processingStatus = "COMPLETE";
                            }
                            else
                            {
                                updateUpload.processingStatus = "Processing Error";
                            }
                            updateUpload.uploadError = true;
                            updateUpload.numberProcessed = finalTable.Rows.Count;
                            updateUpload.numberDiscarded = errorTable.Rows.Count;
                            updateUpload.discardedEntriesPath = destinationFile;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        if (count > 0)
                        {
                            upload updateUpload = db.uploads.FirstOrDefault(d => d.id == Id);
                            updateUpload.processingEndedDT = DateTime.Now;
                            updateUpload.processed = true;
                            updateUpload.processingError = false;
                            updateUpload.processingStatus = "COMPLETE";
                            updateUpload.uploadError = false;
                            updateUpload.numberProcessed = finalTable.Rows.Count;
                            updateUpload.numberDiscarded = errorTable.Rows.Count;
                            db.SaveChanges();

                        }
                    }
                    return Json(new { id = Id, errorMessage = "" });
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "";
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        errorMessage = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        errorMessage = ex.InnerException.Message;
                    }

                }
                else
                {
                    errorMessage = ex.Message;
                }



                using (var db = new AutoSherDBContext())
                {
                    string errorMesg = string.Empty;
                    long totalrecord = db.Database.SqlQuery<int>("SELECT COUNT(*) FROM " + uploadTable + " WHERE (uploadid =" + Id + ")").First();
                    long totaldump = finalTable.Rows.Count;
                    if (totalrecord == totaldump)
                    {
                        errorMesg = "Processing Error(T)";
                    }
                    else
                    {
                        errorMesg = "Processing error";
                    }

                    upload updateUpload = db.uploads.FirstOrDefault(d => d.id == Id);
                    updateUpload.processingEndedDT = DateTime.Now;
                    updateUpload.processed = false;
                    updateUpload.processingError = true;
                    updateUpload.error = errorMessage;
                    updateUpload.processingStatus = errorMesg;
                    updateUpload.uploadError = true;
                    updateUpload.numberProcessed = finalTable.Rows.Count;
                    updateUpload.numberDiscarded = errorTable.Rows.Count;
                    db.SaveChanges();
                }

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        return Json(new { id = Id, errorMessage = "Error While Uploading File." + ex.InnerException.InnerException.Message });
                    }
                    else
                    {
                        return Json(new { id = Id, errorMessage = "Error While Uploading File." + ex.InnerException.Message });
                    }

                }
                else
                {
                    return Json(new { id = Id, errorMessage = "Error While Uploading File." + ex.Message });
                }

            }

        }


        /** Extracting Data From Excel **/
        public static DataSet GetDataTableFromExcel(string path, long uploadId, bool hasHeader = true)
        {
            DataSet FinalRecords = new DataSet();
            DataTable patterntable = new DataTable();
            IDictionary<string, string> datepattern = new Dictionary<string, string>();
            Dictionary<int, int> rowNo = new Dictionary<int, int>();
            Dictionary<string, string> ErrorColRows = new Dictionary<string, string>();
            DataTable errorTable = new DataTable();
            string name = string.Empty;
            string pattern = string.Empty;
            string keyRowNo = string.Empty;
            string errorcolumn = string.Empty;
            bool errorinRow = false;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    long? uploadTypeId = db.uploads.FirstOrDefault(x => x.id == uploadId).uploadType_id;
                    string tableName = db.uploadtypes.FirstOrDefault(x => x.id == uploadTypeId).uploadTypeName;

                    using (var pck = new OfficeOpenXml.ExcelPackage())
                    {
                        using (var stream = System.IO.File.OpenRead(path))
                        {
                            pck.Load(stream);
                        }
                        var ws = TrimEmptyRows(pck.Workbook.Worksheets.First());


                        DataTable tbl = new DataTable();
                        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                        {
                            //tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                            if (!(string.IsNullOrWhiteSpace(firstRowCell.Value.ToString())))
                            {
                                tbl.Columns.Add(hasHeader ? firstRowCell.Value.ToString() : string.Format("Column {0}", firstRowCell.Start.Column));
                            }
                        }


                        List<long> columnDef = db.uploadtype_columndefinition.Where(m => m.UploadType_id == uploadTypeId).Select(m => m.definitions_id).ToList();
                        var sourceColoumns = db.columndefinitions.Where(m => columnDef.Contains(m.id) && m.additionalFormData == false).Select(m => new { dataType = m.columnDataType, name = m.sourceColumnName, destinationColumnName = m.destinationColumnName, pattern = m.dataPattern }).ToList();
                        foreach (var items in sourceColoumns)
                        {
                            for (int i = 0; i < tbl.Columns.Count; i++)
                            {
                                if (items.name == tbl.Columns[i].ColumnName)
                                {
                                    tbl.Columns[i].ColumnName = items.destinationColumnName.Trim();
                                    errorTable.Columns.Add(items.destinationColumnName);
                                    name = items.destinationColumnName;
                                    pattern = items.pattern;
                                    datepattern.Add(name, pattern);
                                    if ((items.dataType.ToLower() == "DATE".ToLower()) || (items.dataType.ToLower() == "TIME".ToLower()) || (items.dataType.ToLower() == "DATETIME".ToLower()) || (items.dataType.ToLower() == "TIMESTAMP".ToLower()) || (items.dataType.ToLower() == "YEAR".ToLower()))
                                    {
                                        tbl.Columns[i].ColumnName = items.destinationColumnName;
                                        tbl.Columns[i].DataType = typeof(DateTime);
                                    }
                                    else if ((items.dataType.ToLower() == "VARCHAR".ToLower()) || (items.dataType.ToLower() == "CHAR".ToLower()) || (items.dataType.ToLower() == "BINARY".ToLower()) || (items.dataType.ToLower() == "VARBINARY".ToLower()) || (items.dataType.ToLower() == "LONGTEXT".ToLower()) || (items.dataType.ToLower() == "MEDIUMTEXT".ToLower()) || (items.dataType.ToLower() == "TINYTEXT".ToLower()) || (items.dataType.ToLower() == "TEXT".ToLower()))
                                    {
                                        tbl.Columns[i].ColumnName = items.destinationColumnName;
                                        tbl.Columns[i].DataType = typeof(string);
                                    }
                                    else if ((items.dataType.ToLower() == "TINYINT".ToLower()) || (items.dataType.ToLower() == "SMALLINT".ToLower()) || (items.dataType.ToLower() == "MEDIUMINT".ToLower()) || (items.dataType.ToLower() == "INT".ToLower()) || (items.dataType.ToLower() == "BIGINT".ToLower()) || (items.dataType.ToLower() == "DECIMAL".ToLower()) || (items.dataType.ToLower() == "FLOAT".ToLower()) || (items.dataType.ToLower() == "DOUBLE".ToLower()) || (items.dataType.ToLower() == "BIT".ToLower()))
                                    {
                                        tbl.Columns[i].ColumnName = items.destinationColumnName;
                                        tbl.Columns[i].DataType = typeof(decimal);
                                    }
                                    break;
                                }
                            }
                        }
                        DataColumn newCol = new DataColumn("uploadid", typeof(decimal));
                        newCol.AllowDBNull = true;
                        tbl.Columns.Add(newCol);




                        DataColumn errocol = new DataColumn("Error", typeof(string));
                        errocol.AllowDBNull = true;
                        errorTable.Columns.Add(errocol);

                        var startRow = hasHeader ? 2 : 1;
                        for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                        {
                            var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                            if (wsRow.Value != null)
                            {
                                DataRow row = tbl.Rows.Add();
                                foreach (var cell in wsRow)
                                {
                                    try
                                    {
                                        if (tbl.Columns[cell.Start.Column - 1].DataType == typeof(DateTime))
                                        {
                                            try
                                            {
                                                if (cell.Text.ToString().Trim().Length > 0)
                                                {
                                                    string dateString = cell.Text.ToString();

                                                    string format = datepattern[tbl.Columns[cell.Start.Column - 1].ColumnName].ToString();

                                                    //if (format.Equals("ddMMyyyy"))
                                                    //{
                                                    //    var newDate = DateTime.ParseExact(dateString, "ddMMyyyy", CultureInfo.InvariantCulture);
                                                    //    row[cell.Start.Column - 1] = newDate.ToString("yyyy/MM/dd");
                                                    //}
                                                    //else if (format.Equals("yyyyMMdd"))
                                                    //{
                                                    //    var newDate = DateTime.ParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture);
                                                    //    row[cell.Start.Column - 1] = newDate.ToString("yyyy/MM/dd");

                                                    //}
                                                    //else
                                                    //{
                                                    //    DateTime dateTime = Convert.ToDateTime(dateString);
                                                    //    row[cell.Start.Column - 1] = dateTime.ToString("yyyy/MM/dd");
                                                    //}

                                                    row[cell.Start.Column - 1] = returnDate(dateString, format);
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                if (errorinRow)
                                                {
                                                    //errorcolumn=errorcolumn+ " , Error in Column Name  " + tbl.Columns[cell.Start.Column - 1].ColumnName  + ex.Message;
                                                    errorcolumn = errorcolumn + " ,  Error in Column Name  " + tbl.Columns[cell.Start.Column - 1].ColumnName + " - values -" + cell.Text.ToString() + "Invalid date . format should be -  " + datepattern[tbl.Columns[cell.Start.Column - 1].ColumnName].ToString();
                                                }
                                                else
                                                {
                                                    //errorcolumn = "Error in Column Name  " + tbl.Columns[cell.Start.Column - 1].ColumnName + ex.Message;
                                                    errorcolumn = "Error in Column Name  " + tbl.Columns[cell.Start.Column - 1].ColumnName + " - values -  " + cell.Text.ToString() + "Invalid date . format should be - " + datepattern[tbl.Columns[cell.Start.Column - 1].ColumnName].ToString();

                                                }


                                                keyRowNo = rowNum.ToString();
                                                errorinRow = true;
                                            }

                                        }
                                        else if (tbl.Columns[cell.Start.Column - 1].DataType == typeof(decimal))
                                        {
                                            try
                                            {
                                                if (cell.Text.ToString().Trim().Length > 0)
                                                {
                                                    decimal val = Convert.ToDecimal(cell.Text);
                                                    row[cell.Start.Column - 1] = val;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                if (errorinRow)
                                                {
                                                    //errorcolumn = errorcolumn + "  Error in Column Name  " + tbl.Columns[cell.Start.Column - 1].ColumnName + ex.Message;
                                                    errorcolumn = errorcolumn + " ,  Error in Column Name  " + tbl.Columns[cell.Start.Column - 1].ColumnName + " - values -" + cell.Text.ToString();

                                                }
                                                else
                                                {
                                                    //errorcolumn = "Error in Column Name  " + tbl.Columns[cell.Start.Column - 1].ColumnName + ex.Message;
                                                    errorcolumn = "Error in Column Name  " + tbl.Columns[cell.Start.Column - 1].ColumnName + " - values -" + cell.Text.ToString();

                                                }
                                                keyRowNo = rowNum.ToString();
                                                errorinRow = true;
                                            }
                                        }
                                        else if (tbl.Columns[cell.Start.Column - 1].DataType == typeof(string))
                                        {
                                            try
                                            {
                                                if (cell.Text.ToString().Trim().Length > 0)
                                                {
                                                    string val = cell.Text.ToString();
                                                    row[cell.Start.Column - 1] = val;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                if (errorinRow)
                                                {
                                                    //errorcolumn = errorcolumn + "  Error in Column Name  " + tbl.Columns[cell.Start.Column - 1].ColumnName + ex.Message;
                                                    errorcolumn = errorcolumn + " ,  Error in Column Name  " + tbl.Columns[cell.Start.Column - 1].ColumnName + " - values -" + cell.Text.ToString();

                                                }
                                                else
                                                {
                                                    //errorcolumn = "Error in Column Name  " + tbl.Columns[cell.Start.Column - 1].ColumnName + ex.Message;
                                                    errorcolumn = "Error in Column Name  " + tbl.Columns[cell.Start.Column - 1].ColumnName + " - values -" + cell.Text.ToString();

                                                }
                                                keyRowNo = rowNum.ToString();
                                                errorinRow = true;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                                if (errorinRow)
                                {
                                    errorTable.ImportRow(row);
                                    errorTable.AcceptChanges();
                                    row.Delete();
                                    tbl.AcceptChanges();
                                    errorinRow = false;
                                    int no = errorTable.Rows.Count - 1;
                                    errorTable.Rows[no]["Error"] = errorcolumn;
                                    errorcolumn = string.Empty;
                                    errorTable.AcceptChanges();
                                }
                                if (tbl.Rows.Count > 0)
                                {
                                    int no1 = tbl.Rows.Count - 1;
                                    tbl.Rows[no1]["uploadid"] = uploadId;
                                }
                            }


                        }
                        tbl.AcceptChanges();
                        FinalRecords.Tables.Add(tbl);
                        FinalRecords.Tables.Add(errorTable);

                        return FinalRecords;
                    }
                }
            }
            catch (Exception ex)
            {
                string excptn = "";
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        excptn = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        excptn = ex.InnerException.Message;
                    }

                }
                else
                {
                    excptn = ex.Message;
                }
                using (var db = new AutoSherDBContext())
                {
                    upload updateUpload = db.uploads.FirstOrDefault(d => d.id == uploadId);
                    updateUpload.processingEndedDT = DateTime.Now;
                    updateUpload.processed = false;
                    updateUpload.processingError = true;
                    updateUpload.error = excptn;
                    updateUpload.processingStatus = "Processing error";
                    updateUpload.uploadError = true;
                    updateUpload.numberProcessed = 0;
                    updateUpload.numberDiscarded = 0;
                    db.SaveChanges();
                }
            }
            return null;
        }

        public static DateTime returnDate(string dateString, string format)
        {
            DateTime convertedDate = new DateTime();


            if (!format.Equals("ddMMyyyy") && !format.Equals("yyyyMMdd"))
            {
                if (dateString.Contains("-") || dateString.Contains("/"))
                {
                    convertedDate = Convert.ToDateTime(dateString);
                }
                else
                {
                    long dateNum = long.Parse(dateString);
                    convertedDate = DateTime.FromOADate(dateNum);
                }
            }
            else if (format.Equals("ddMMyyyy"))
            {
                if (dateString.Contains("-") || dateString.Contains("/"))
                {
                    convertedDate = Convert.ToDateTime(dateString);
                }
                else
                {
                    convertedDate = DateTime.ParseExact(dateString, "ddMMyyyy", CultureInfo.InvariantCulture);
                }
            }
            else if (format.Equals("yyyyMMdd"))
            {
                if (dateString.Contains("-") || dateString.Contains("/"))
                {
                    convertedDate = Convert.ToDateTime(dateString);
                }
                else
                {
                    convertedDate = DateTime.ParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture);
                }

            }
            return convertedDate;

        }

        /** End **/

        /**  Deleting Empty Rows in Excel **/
        private static ExcelWorksheet TrimEmptyRows(ExcelWorksheet worksheet)
        {
            //loop all rows in a file
            for (int i = worksheet.Dimension.Start.Row; i <=
           worksheet.Dimension.End.Row; i++)
            {
                bool isRowEmpty = true;
                //loop all columns in a row
                for (int j = worksheet.Dimension.Start.Column; j <= worksheet.Dimension.End.Column; j++)
                {
                    if (worksheet.Cells[i, j].Value != null)
                    {
                        isRowEmpty = false;
                        break;
                    }
                }
                if (isRowEmpty)
                {
                    worksheet.DeleteRow(i);
                }
            }
            return worksheet;
        }

        /** END **/

        //Displayig Errors in Excel . while appying validation to records
        private static void DataSetToExcel(DataTable table, string filePath)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                {
                    pck.Workbook.Worksheets.Add("sheet1");
                    pck.Workbook.Worksheets.MoveToStart("sheet1");
                    ExcelWorksheet ws = pck.Workbook.Worksheets[1];
                    ws.Cells["A1"].LoadFromDataTable(table, true);
                    int numCol = ws.Dimension.Columns;
                    //ws.Column(numCol).Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Column(numCol).Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FF0000"));
                }

                pck.SaveAs(new FileInfo(filePath));
            }
        }


        /** Once the updatiuon is completed displaying uploading details in span Popup based on current uploadId **/
        public ActionResult getUploadsListById(long? upId)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    upload uploadList = db.uploads.Where(u => u.id == upId).FirstOrDefault();
                    // var uploadList = db.uploads.Where(u => u.id == upId).Select(u => new { procesStartDate = u.processingStartedDT, processEndDate = u.processingEndedDT, processError = u.processingError, fileNme = u.fileName, status = u.processingStatus, uplodDateTime = u.uploadedDateTime, numProcessed = u.numberProcessed, err = u.error, erPath = u.discardedEntriesPath, uploadTypeId = u.uploadType_id, path = u.uploadPath }).ToList();


                    string fileName = "<a href=" + @Url.Action("downloadFile", "FileUpload", new { path = uploadList.uploadPath }) + "><i class=\"fa fa-download\"><span style=\"text-overflow: clip;\">" + uploadList.fileName
                    + "</span></i></a>";
                    dict.Add("FileName", fileName);
                    dict.Add("Status", uploadList.processingStatus);
                    DateTime? date = uploadList.uploadedDateTime;
                    string fromatedDate = date.ToString();
                    dict.Add("Uploaded Time", fromatedDate);
                    string error = "";
                    string errorPath = "";
                    string invDatapath = "";
                    dict.Add("Accepted/Rejected", uploadList.numberProcessed + "/" + uploadList.numberDiscarded);

                    if (uploadList.processingError)
                    {
                        error = uploadList.error;
                        errorPath = "<a href=" + @Url.Action("downloadFile", "FileUpload", new { path = uploadList.discardedEntriesPath }) + "><i class=\"fa fa-download\"><span style=\"text-overflow: clip;\">" + uploadList.fileName
                    + "</span></i></a>";
                        dict.Add("Error File", errorPath);
                    }

                    DateTime? pstart = uploadList.processingStartedDT;
                    string pstartF = "";
                    if (pstart != null)
                        pstartF = pstart.ToString();

                    DateTime? pedate = uploadList.processingEndedDT;
                    string pedateF = "";
                    if (pedate != null)
                        pedateF = pedate.ToString();

                    if (uploadList.processingError)
                    {
                        if (uploadList.error != null)
                        {
                            dict.Add("Action", uploadList.error);
                        }
                    }
                    else
                    {
                        dict.Add("Action", "NO ERRORS");
                    }

                    if (uploadList.uploadType_id == 4)
                    {
                        long checkId = db.invalidworkshopdatas.Count(u => u.uploadid == uploadList.id.ToString());
                        if (checkId > 0)
                        {
                            invDatapath = "<a href=" + @Url.Action("getInvalidData", "FileUpload", new { uploadId = uploadList.id }) + "><i class=\"fa fa-download\"><span style=\"text-overflow: clip;\"> InvalidData </span></i></a>";
                            dict.Add("InavalidData", invDatapath);
                        }
                    }
                    //IDictionary<string, string> totalErrors = (IDictionary<string, string>)Session["ErrorExcel"];
                    //if (totalErrors.Count > 0)
                    //{
                    //    string errShow = "<span onclick='showErrors()' style='color:red;'>View Error</span>";
                    //    dict.Add("View Error", errShow);
                    //}

                    return Json(new { success = true, strUploadData = dict, JsonRequestBehavior.AllowGet });
                    //return Json(new { success = true, strUploadData = dict, errorExcel = totalErrors, JsonRequestBehavior.AllowGet });
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = false });

        }

        /** END **/

        public void uploadedStatusDetailsUpdation(string uploadId, List<ChangeAssignment> uploadStatusList)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    long upId = Convert.ToInt64(uploadId);
                    uploaddetail uploaddetail = new uploaddetail();
                    //  uploaddetail up = db.uploaddetails.FirstOrDefault(u => u.uploadid == upId);
                    uploaddetail.totalUploaded = uploadStatusList[0].totalUploaded;
                    uploaddetail.totalAssigned = uploadStatusList[0].totalAssigned;
                    uploaddetail.totalNotAssigned = uploadStatusList[0].totalAssigned;
                    uploaddetail.alreadyAvailable = uploadStatusList[0].alreadyInBucket;
                    uploaddetail.totalReject = uploadStatusList[0].totalRejected;
                    uploaddetail.excelReject = uploadStatusList[0].excelError;
                    uploaddetail.invalidWorkshopReject = uploadStatusList[0].rejectedInvalidWorkshopId;
                    uploaddetail.uploadid = upId;
                    long? uploadTId = db.uploads.FirstOrDefault(u => u.id == upId).uploadType_id;

                    if (uploadTId == 4 || uploadTId == 5)
                    {
                        string typeId = db.formdatas.FirstOrDefault(u => u.name == "campaignName" && u.upload_id == upId).value;
                        uploaddetail.campaign_id = Convert.ToInt64(typeId);
                    }
                    else
                    {
                        string typeId = db.formdatas.FirstOrDefault(u => u.name == "workshop_id" && u.upload_id == upId).value;
                        uploaddetail.campaign_id = Convert.ToInt64(typeId);
                    }
                    db.uploaddetails.AddOrUpdate(uploaddetail);
                    db.SaveChanges();
                    List<ChangeAssignment> uploadCreWiseList = getUploadedAssignedCallDetailsCrewise(uploadId);
                    uploadedAssignedCallDetailsCrewiseUpdation(uploadId, uploadCreWiseList);

                }
            }
            catch (Exception ex)
            {

            }
        }

        public List<ChangeAssignment> getUploadedStatusDetails(string uploadId)
        {
            List<ChangeAssignment> changeAssignment = null;
            try
            {
                using (var db = new AutoSherDBContext())
                {

                    string str = @"CALL uploadAssignedStatusDetails(@inuploadid)";
                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("@inuploadid", uploadId)
                    };

                    changeAssignment = db.Database.SqlQuery<ChangeAssignment>(str, param).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            uploadedStatusDetailsUpdation(uploadId, changeAssignment);
            return changeAssignment;
        }

        public List<ChangeAssignment> getUploadedAssignedCallDetailsCrewise(string uploadId)
        {
            List<ChangeAssignment> changeAssignment = null;
            try
            {
                using (var db = new AutoSherDBContext())
                {

                    string str = @"CALL uploadAssignedCallDetailsCrewise(@inuploadid)";
                    MySqlParameter[] param = new MySqlParameter[]
                    {
                        new MySqlParameter("@inuploadid", uploadId)
                    };

                    changeAssignment = db.Database.SqlQuery<ChangeAssignment>(str, param).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return changeAssignment;
        }
        public void uploadedAssignedCallDetailsCrewiseUpdation(string uploadId, List<ChangeAssignment> uploadCreWiseList)
        {

            try
            {
                using (var db = new AutoSherDBContext())
                {
                    long upId = Convert.ToInt64(uploadId);
                    long campaignId;
                    long? uploadTypeId = db.uploads.FirstOrDefault(u => u.id == upId).uploadType_id;

                    if (uploadTypeId == 4 || uploadTypeId == 5)
                    {
                        string typeId = db.formdatas.FirstOrDefault(u => u.upload_id == upId && u.name == "campaignName").value;
                        campaignId = Convert.ToInt64(typeId);
                        for (int i = 0; i < uploadCreWiseList.Count; i++)
                        {
                            uploadsummaryuserwise uploadSummary = new uploadsummaryuserwise();
                            string locName = uploadCreWiseList[i].locationName;
                            string creName = uploadCreWiseList[i].CreName;
                            long locId = db.locations.FirstOrDefault(u => u.name == locName).cityId;
                            long wyzId = db.wyzusers.FirstOrDefault(u => u.userName == creName).id;
                            uploadSummary.uploadid = wyzId;
                            uploadSummary.wyzuser_id = wyzId;
                            uploadSummary.isAutoAssigned = true;
                            uploadSummary.campaign_id = campaignId;
                            uploadSummary.location_id = locId;
                            uploadSummary.module = 1;
                            uploadSummary.totalAssignedCalls = uploadCreWiseList[i].totalAssigned;
                            db.uploadsummaryuserwises.AddOrUpdate(uploadSummary);
                        }
                    }
                    //else if (uploadTypeId == 5)
                    //{
                    //    string typeId = db.formdatas.FirstOrDefault(u => u.upload_id == upId && u.name == "campaignName").value;
                    //    campaignId = Convert.ToInt64(typeId);
                    //    for (int i = 0; i < uploadCreWiseList.Count; i++)
                    //    {
                    //        uploadsummaryuserwise uploadSummary = new uploadsummaryuserwise();
                    //        string locName = uploadCreWiseList[i].locationName;
                    //        string creName = uploadCreWiseList[i].CreName;
                    //        long locId = db.locations.FirstOrDefault(u => u.name == locName).cityId;
                    //        long wyzId = db.wyzusers.FirstOrDefault(u => u.userName == creName).id;
                    //        uploadSummary.uploadid = wyzId;
                    //        uploadSummary.wyzuser_id = wyzId;
                    //        uploadSummary.isAutoAssigned = true;
                    //        uploadSummary.campaign_id = campaignId;
                    //        uploadSummary.location_id = locId;
                    //        uploadSummary.module = 1;
                    //        uploadSummary.totalAssignedCalls = uploadCreWiseList[i].totalAssigned;
                    //        db.uploadsummaryuserwises.AddOrUpdate(uploadSummary);
                    //    }
                    //}
                    else
                    {
                        string typeId = db.formdatas.FirstOrDefault(u => u.upload_id == upId && u.name == "workshop_id").value;
                        campaignId = Convert.ToInt64(typeId);
                        for (int i = 0; i < uploadCreWiseList.Count; i++)
                        {
                            uploadsummaryuserwise uploadSummary = new uploadsummaryuserwise();
                            string locName = uploadCreWiseList[i].locationName;
                            string creName = uploadCreWiseList[i].CreName;
                            long locId = db.locations.FirstOrDefault(u => u.name == locName).cityId;
                            long wyzId = db.wyzusers.FirstOrDefault(u => u.userName == creName).id;
                            uploadSummary.uploadid = wyzId;
                            uploadSummary.wyzuser_id = wyzId;
                            uploadSummary.isAutoAssigned = true;
                            uploadSummary.campaign_id = campaignId;
                            uploadSummary.location_id = locId;
                            uploadSummary.module = 1;
                            uploadSummary.totalAssignedCalls = uploadCreWiseList[i].totalAssigned;
                            db.uploadsummaryuserwises.AddOrUpdate(uploadSummary);
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }


        }
        #endregion

        [HttpGet]
        public ActionResult downloadFile(string path)
        {
            try
            {
                var filePath = path;
                var fileName = "ExcellData.xlsx";
                //Path.GetFileName(filePath);
                var mimeType = "application/octet-stream";
                return File(new FileStream(filePath, FileMode.Open), mimeType, fileName);
            }
            catch (Exception ex)
            {

            }
            return View("Excel");
        }
        public ActionResult deleteFile(long? id)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    int count = db.uploads.Count(m => m.id == id);
                    if (count > 0)
                    {

                        string deletePath = db.uploads.FirstOrDefault(m => m.id == id).uploadPath;
                        if (System.IO.File.Exists(deletePath))
                        {
                            System.IO.File.Delete(deletePath);
                        }
                        upload up = db.uploads.FirstOrDefault(m => m.id == id);
                        up.processingStatus = "DELETED";
                        db.uploads.AddOrUpdate(up);
                        db.SaveChanges();
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public ActionResult getInvalidData(string uploadId)
        {
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    string filename = "InvalidData";
                    List<invalidworkshopdata> dataInvalid = db.invalidworkshopdatas.Where(u => u.uploadid == uploadId).ToList();
                    DataTable invalidDataTable = getInvalidcolumnstoDT(dataInvalid);
                    using (ExcelPackage pck = new ExcelPackage())
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("InvalidData");
                        ws.Cells["A1"].LoadFromDataTable(invalidDataTable, true);

                        var FileBytesArray = pck.GetAsByteArray();
                        return File(FileBytesArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename + ".xlsx");

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public DataTable getInvalidcolumnstoDT<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties  
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table   
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows  
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable  
            return dataTable;
        }
    }
}