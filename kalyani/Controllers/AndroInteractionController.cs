using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoSherpa_project.Models;
using AutoSherpa_project.Models.Schedulers;
using Newtonsoft.Json.Linq;
using NLog;
using Newtonsoft.Json;
using AutoSherpa_project.Models.API_Model;
using System.Text.RegularExpressions;
using System.Collections.Specialized;


namespace AutoSherpa_project.Controllers
{
    public class AndroInteractionController : ApiController
    {
        // GET: api/AndroInteraction
        [HttpGet, Route("api/AndroInteraction")]
        public IHttpActionResult Get()
        {
            dynamic data = new JObject();
            string JsonString = string.Empty;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    var wyzAppData = db.tenants.FirstOrDefault(m => m.isDeactivated == true);

                    data.latestAppVersion = wyzAppData.appVersion;
                    data.urlLink = wyzAppData.apkURLPath;

                    //JsonString = JsonConvert.SerializeObject(data);
                    //JsonString = data;
                }
            }
            catch (Exception ex)
            {

                string exception = "";

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        exception = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        exception = ex.InnerException.Message;
                    }
                }
                else
                {
                    exception = ex.Message;
                }


                //dynamic data = new JObject();
                data.Exception = exception;
                //JsonString = JsonConvert.SerializeObject(data); 
            }
            return Ok<JObject>(data);
        }

        public IHttpActionResult Get(string id)
        {
            dynamic data = new JObject();
            string JsonString = string.Empty;
            try
            {
                using (var db = new AutoSherDBContext())
                {


                    var wyzAppData = db.tenants.FirstOrDefault(m => m.isDeactivated == true);

                    if (id.ToLower() == "fe")
                    {
                        data.latestAppVersion = wyzAppData.feAppVersion;
                        data.urlLink = wyzAppData.apkURLPath;
                    }
                    else if (id.ToLower() == "driver")
                    {
                        data.latestAppVersion = wyzAppData.driverAppVersion;
                        data.urlLink = wyzAppData.apkURLPath;
                    }
                    else
                    {
                        data.Exception = "Wrong Parameter";
                    }
                    //JsonString = JsonConvert.SerializeObject(data);
                    //JsonString = data;
                }
            }
            catch (Exception ex)
            {

                string exception = "";

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        exception = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        exception = ex.InnerException.Message;
                    }
                }
                else
                {
                    exception = ex.Message;
                }


                //dynamic data = new JObject();
                data.Exception = exception;
                //JsonString = JsonConvert.SerializeObject(data); 
            }
            return Ok<JObject>(data);
        }

        // GET: api/AndroInteraction/5
        //public string Get(string id)
        //{
        //    return "value";
        //}

        // POST: api/AndroInteraction
        [HttpPost, Route("api/AndroInteraction")]
        public IHttpActionResult Post([FromBody] AppLogCheck app)
        {
            dynamic data = new JObject();
            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {
                //AppLogCheck app = new AppLogCheck();


                logger.Info("Request Body:\n" + JsonConvert.SerializeObject(app));

                using (var db = new AutoSherDBContext())
                {
                    string phIM = string.Empty;
                    string phNum = string.Empty;

                    if (app.phoneNumber != null)
                    {
                        phNum = app.phoneNumber.Trim();
                    }
                    if (!String.IsNullOrEmpty(app.phoneIMEINo))
                    {
                        phIM = app.phoneIMEINo.Trim();
                    }
                    //if(value==null)
                    //{
                    //    return JsonString = "Request is Empty";
                    //}

                    wyzuser user = new wyzuser();

                    if (!string.IsNullOrEmpty(app.feRequest) && app.feRequest == "yes")
                    {
                        user = db.wyzusers.FirstOrDefault(m => m.phoneNumber == phNum && m.role == "InsuranceAgent");
                    }
                    else if (!string.IsNullOrEmpty(app.feRequest) && app.feRequest == "driver")
                    {
                        user = db.wyzusers.FirstOrDefault(m => m.phoneNumber == phNum && m.role == "Driver");
                    }

                    else if (!string.IsNullOrEmpty(app.feRequest) && app.feRequest == "FEManager")
                    {
                        user = db.wyzusers.FirstOrDefault(m => m.phoneNumber == phNum && m.role == "FEManager");
                    } 
                    else
                    {
                        if (db.wyzusers.Where(m => m.phoneNumber.Trim() == phNum && m.phoneIMEINo.Trim() == phIM).Count() > 1)
                        {
                            data.Exception = "Duplicate Phone Number";
                            return Ok<JObject>(data);
                        }
                        else
                        {
                            user = db.wyzusers.FirstOrDefault(m => m.phoneNumber.Trim() == phNum && m.phoneIMEINo.Trim() == phIM);
                        }
                    }

                    //app = JsonConvert.DeserializeObject<AppLogCheck>(value);

                    if (user != null && !string.IsNullOrEmpty(app.registrationId))
                    {


                        user.registrationId = app.registrationId;
                        db.wyzusers.AddOrUpdate(user);
                        db.SaveChanges();

                        data.authenticationStatus = true;
                        data.userId = user.userName;
                        data.userRole = user.role;
                        data.userFName = user.firstName;
                        data.userLName = user.lastName == null ? "0" : user.lastName;
                        data.dealerId = user.dealerCode;
                        data.dealerName = user.dealerName;
                        data.userEmail = null;
                        data.jwtToken = app.registrationId;

                        string baseURL = string.Empty;
                        if (HomeController.wyzCRM1.Contains(user.dealerCode))
                        {
                            baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_Wyzcrm1"];
                        }
                        else if (HomeController.wyzCRM.Contains(user.dealerCode))
                        {
                            baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_Wyzcrm"];
                        }
                        else if (HomeController.autosherpa1.Contains(user.dealerCode))
                        {
                            baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_autosherpa1"];
                        }
                        else if (HomeController.WyzCrmNew.Contains(user.dealerCode))
                        {
                            baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_wyzNew"];
                        }
                        else if (HomeController.autosherpa3.Contains(user.dealerCode))
                        {
                            baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_autosherpa3"];
                        }
                        if (app.feRequest == "driver")
                        {
                            baseURL = System.Configuration.ConfigurationManager.AppSettings["FireBase_wyzNew"];
                            data.dealerName = user.dealerCode;
                        }
                        data.fireBaseUrl = baseURL;
                    }
                    else
                    {
                        data.authenticationStatus = false;
                        data.userId = null;
                        data.userRole = null;
                        data.userFName = null;
                        data.userLName = null;
                        data.dealerId = null;
                        data.dealerName = null;
                        data.userEmail = null;
                        data.jwtToken = null;
                        data.fireBaseUrl = null;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        data.Exception = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        data.Exception = ex.InnerException.Message;
                    }
                }
                else
                {
                    data.Exception = ex.Message.ToString();
                }
                //dynamic data = new JObject();

            }
            logger.Info("Response Body: \n" + JsonConvert.SerializeObject(data));
            return Ok<JObject>(data);
        }

        // PUT: api/AndroInteraction/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/AndroInteraction/5
        public void Delete(int id)
        {
        }


        [Route("api/AndroInteraction/processAudio")]
        [HttpPost()]
        public async Task<IHttpActionResult> processAudioFile()
        {
            List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
            Dictionary<string, string> returnData = new Dictionary<string, string>();
            var context = HttpContext.Current;

            try
            {
                string dealerCode = string.Empty;
                using (var db = new AutoSherDBContext())
                {
                    dealerCode = db.dealers.FirstOrDefault().dealerCode;
                }

                var root = context.Server.MapPath("~/wyzAudioData/" + dealerCode + "/");
                //var root = "C:/AS_DotNet/WyzAudioFiles/"+ dealerCode+"/";
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }

                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    string name = "";
                    var docfiles = new List<string>();
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        name = postedFile.FileName;
                        name = name.Trim('"');
                        name = (name.Replace('+', '_')).Replace('-', '_');
                        var filePath = HttpContext.Current.Server.MapPath("~/wyzAudioData/" + dealerCode + "/" + name);
                        postedFile.SaveAs(filePath);
                        docfiles.Add(filePath);
                    }

                    returnData["fileName"] = name;
                    returnData["status"] = "1";
                    data.Add(returnData);
                    //result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
                }
                else
                {
                    returnData["fileName"] = "";
                    returnData["status"] = "1";
                    data.Add(returnData);
                }
            }
            catch (Exception ex)
            {
                returnData["fileName"] = "";
                returnData["status"] = "1";
                data.Add(returnData);
                return Ok<List<Dictionary<string, string>>>(data);
            }
            return Ok<List<Dictionary<string, string>>>(data);
        }

        [Route("api/AndroInteraction/downloadApk")]
        [HttpGet]
        public IHttpActionResult downloadApk(string appfor, string appver)
        {
            dynamic data = new JObject();
            try
            {
                appfor = appfor.Trim().ToUpper();
                appver = appver.Trim();
                string dealerCode;
                using (var db = new AutoSherDBContext())
                {
                    dealerCode = db.dealers.FirstOrDefault().dealerCode;
                }

                string root = @"C:\AndroidApps\" + dealerCode + "\\" + appfor + "\\";
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                DirectoryInfo dirInfo = new DirectoryInfo(root);

                string path = string.Empty;
                string fileName = appfor + "_" + appver + ".apk";
                string filePath = @"C:\AndroidApps\" + dealerCode + "\\" + appfor + "\\" + appfor + "_" + appver + ".apk";
                if (File.Exists(filePath))
                {
                    //FileInfo[] file = dirInfo.GetFiles("CRM_1.4.apk");
                    FileInfo[] file = dirInfo.GetFiles(fileName);

                    foreach (FileInfo fInfo in file)
                    {
                        path = root + fInfo.Name;
                        fileName = fInfo.Name;
                        break;
                    }

                    if (!string.IsNullOrEmpty(path))
                    {
                        var dataBytes = File.ReadAllBytes(path);
                        var dataStream = new MemoryStream(dataBytes);
                        return new returnApk(dataStream, Request, fileName);
                    }
                    else
                    {
                        data.result = "No file found...";
                        return Ok<JObject>(data);
                    }

                    //if (file.Count()>0)
                    //{
                    //    var dataBytes = File.ReadAllBytes(file[0].Name);
                    //    var dataStream = new MemoryStream(dataBytes);

                    //    return new returnApk(dataStream, Request, fileName);
                    //}
                    //else
                    //{
                    //    return Ok<string>("No file found...");
                    //}
                }
                else
                {
                    data.result = "No file found...";
                    return Ok<JObject>(data);
                }

            }
            catch (Exception ex)
            {
                string exception = "";

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        exception = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        exception = ex.InnerException.Message;
                    }
                }
                else
                {
                    exception = ex.Message;
                }

                data.exception = exception;
                return Ok<JObject>(data);
            }
        }

        [Route("api/AndroInteraction/getAppFilesName")]
        [HttpGet]
        public IHttpActionResult getAppFilesName(string appfor)
        {
            dynamic data = new JObject();
            List<string> filesList = new List<string>();
            try
            {
                appfor = appfor.Trim().ToUpper();
                string dealerCode;
                using (var db = new AutoSherDBContext())
                {
                    dealerCode = db.dealers.FirstOrDefault().dealerCode;
                }

                string root = @"C:\AndroidApps\" + dealerCode + "\\" + appfor + "\\";
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                DirectoryInfo dirInfo = new DirectoryInfo(root);

                string filePath = @"C:\AndroidApps\" + dealerCode + "\\" + appfor + "\\";
                foreach (FileInfo fInfo in dirInfo.GetFiles())
                {
                    filesList.Add(fInfo.Name);
                }

                if (filesList.Count() > 0)
                {
                    data.result = string.Join(",", filesList);
                    return Ok<JObject>(data);
                }
                else
                {
                    data.result = "~/" + dealerCode + "/" + appfor + "/" + ".. Directory is Empty...";
                    return Ok<JObject>(data);
                }
            }
            catch (Exception ex)
            {
                string exception = "";

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        exception = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        exception = ex.InnerException.Message;
                    }
                }
                else
                {
                    exception = ex.Message;
                }

                data.exception = exception;
                return Ok<JObject>(data);
            }
        }

        [Route("api/AndroInteraction/updateCustomerInfo")]
        [HttpPost]
        public IHttpActionResult saveCustAdress([FromBody] FEAppAddressUpdate addres)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            dynamic data = new JObject();
            string firebaseKey = "";
            try
            {
                if (addres != null)
                {
                    logger.Info("Incoming CustInfo/Address Updation is: " + JsonConvert.SerializeObject(addres));
                    long cust_id = 0;
                    long id = 0;
                    using (var db = new AutoSherDBContext())
                    {

                        if (!string.IsNullOrEmpty(addres.appointmentbookedId))
                        {
                            if (addres.appointmentbookedId.Contains("PolicyDrop_"))
                            {
                                id = long.Parse(addres.appointmentbookedId.Split('_')[1]);
                                cust_id = db.insPolicyDrop.FirstOrDefault(m => m.id == id).customer_id ?? default(long);
                                if (id > 0)
                                {
                                    firebaseKey = db.Fieldexecutivefirebaseupdations.FirstOrDefault(m => m.inspolicydrop_id == id).firebasekey;
                                }
                            }
                            else
                            {
                                id = long.Parse(addres.appointmentbookedId);
                                cust_id = db.appointmentbookeds.FirstOrDefault(m => m.appointmentId == id).customer_id ?? default(long);
                                if (id > 0)
                                {
                                    firebaseKey = db.Fieldexecutivefirebaseupdations.FirstOrDefault(m => m.appointmentbookedid == id).firebasekey;
                                }
                            }
                        }
                        else if (addres.DriverScheduler_id != 0)
                        {
                            driverscheduler schedulerData = db.driverSchedulers.FirstOrDefault(m => m.id == addres.DriverScheduler_id);

                            if (schedulerData != null)
                            {
                                firebaseKey = schedulerData.firebasekey;
                                cust_id = schedulerData.customer_id ?? default(long);
                            }
                            else
                            {
                                data.status = "failure";
                                data.exception = "Invalid Request";
                                return Ok<JObject>(data);
                            }
                        }
                        else
                        {
                            data.status = "failure";
                            data.exception = "Invalid Request";
                            return Ok<JObject>(data);
                        }


                        if (cust_id > 0)
                        {
                            address adr = db.addresses.FirstOrDefault(m => m.customer_Id == cust_id && m.isPreferred == true);
                            if (adr != null)
                            {
                                adr.isPreferred = false;
                                db.addresses.AddOrUpdate(adr);
                                db.SaveChanges();
                            }

                            address newAddress = new address();
                            newAddress.concatenatedAdress = addres.address;
                            newAddress.isPreferred = true;
                            newAddress.pincode = addres.pincode;
                            newAddress.customer_Id = cust_id;
                            db.addresses.Add(newAddress);
                            db.SaveChanges();

                        }
                        else
                        {
                            logger.Error("\n\n----------FF/Driver App Address updation Custome Id not found customer id is zero");
                            data.status = "failure";
                            return Ok<JObject>(data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string exception = "";

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        exception = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        exception = ex.InnerException.Message;
                    }
                }
                else
                {
                    exception = ex.Message;
                }
                logger.Error("\n\n------------ FE/Driver App adrress update error------------\n" + exception);
                data.status = "failure";
                return Ok<JObject>(data);
            }
            data.status = "success";
            data.firebasekey = firebaseKey;
            return Ok<JObject>(data);
        }

        [Route("api/AndroInteraction/uploadFiles")]
        [HttpPost()]
        public async Task<IHttpActionResult> processFile()
        {
            var context = HttpContext.Current;
            FEDocumentsReturn docReturn = new FEDocumentsReturn();
            docReturn.filePaths = new List<Dictionary<string, string>>();
            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {
                string dealerCode = string.Empty, username = string.Empty, documentType = string.Empty, documentName = string.Empty, fileString = string.Empty, uploadedFileName = string.Empty;
                long custId = 0, userId = 0, insid = 0;
                string fileUploadPath = "", fireBaseKey = string.Empty;// context.Server.MapPath("~/UploadedFiles/" + Session["DealerCode"].ToString() + "/" + Session["UserName"].ToString() + "/");
                var httpRequest = HttpContext.Current.Request;
                using (var db = new AutoSherDBContext())
                {
                    #region Assigning Form Data to Local Variables
                    if (!string.IsNullOrEmpty(httpRequest.Form["userId"]))
                    {
                        userId = long.Parse(httpRequest.Form["userId"].ToString());
                    }

                    if (!string.IsNullOrEmpty(httpRequest.Form["custId"]))
                    {
                        custId = long.Parse(httpRequest.Form["custId"]);
                    }
                    string FormDataString = httpRequest.Form.ToString();

                    if (!string.IsNullOrEmpty(httpRequest.Form["appointmentId"]))
                    {
                        if (httpRequest.Form["appointmentId"].Contains("PolicyDrop_"))
                        {
                            insid = long.Parse(httpRequest.Form["appointmentId"].ToString().Split('_')[1]);
                            if (db.Fieldexecutivefirebaseupdations.Count(m => m.inspolicydrop_id == insid) > 0)
                            {
                                fireBaseKey = db.Fieldexecutivefirebaseupdations.FirstOrDefault(m => m.inspolicydrop_id == insid).firebasekey;
                            }
                            else
                            {
                                fireBaseKey = "NotFound";
                            }
                        }
                        else
                        {
                            insid = long.Parse(httpRequest.Form["appointmentId"]);
                            if (db.Fieldexecutivefirebaseupdations.Count(m => m.appointmentbookedid == insid) > 0)
                            {
                                fireBaseKey = db.Fieldexecutivefirebaseupdations.FirstOrDefault(m => m.appointmentbookedid == insid).firebasekey;
                            }
                            else
                            {
                                fireBaseKey = "NotFound";
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(httpRequest.Form["documentType"]))
                    {
                        documentType = httpRequest.Form["documentType"];
                    }

                    if (!string.IsNullOrEmpty(httpRequest.Form["documentName"]))
                    {
                        documentName = httpRequest.Form["documentName"];
                    }

                    logger.Info(string.Format("\n Incoming doc details:\n documentType:{0}\n documentName:{1}\n userid:{2}| custId:{3}| aptId:{4}", documentType, documentName, userId, custId, insid));
                    #endregion


                    dealerCode = db.dealers.FirstOrDefault().dealerCode;

                    if (db.wyzusers.Any(m => m.id == userId))
                    {
                        username = db.wyzusers.FirstOrDefault(m => m.id == userId).userName;
                    }
                    else
                    {
                        docReturn.fileName = "";
                        docReturn.status = "0";
                        logger.Info("No WyzUser Id Found");
                        return Ok<FEDocumentsReturn>(docReturn);
                    }

                    fileUploadPath = context.Server.MapPath("~/UploadedFiles/" + dealerCode + "/" + username + "/");

                    if (!Directory.Exists(fileUploadPath))
                    {
                        Directory.CreateDirectory(fileUploadPath);
                    }


                    if (httpRequest.Files.Count > 0)
                    {
                        documentuploadhistory doc = new documentuploadhistory();
                        doc.customerId = custId;
                        doc.user = username;
                        doc.userId = userId;
                        doc.deptName = documentType;
                        doc.documentName = documentName;

                        //var read1 = HttpContext.Current.Request.conte
                        var docfiles = new List<string>();
                        foreach (string file in httpRequest.Files)
                        {
                            string extension = string.Empty, name = string.Empty;

                            var postedFile = httpRequest.Files[file];
                            name = postedFile.FileName;
                            extension = Path.GetExtension(name);

                            string savingFileName = name.Split('.')[0] + "_" + custId + "_" + DateTime.Now.ToString("dd-MM-yyyy").Replace("-", "") + "_" + DateTime.Now.ToString("HH:mm:ss").Replace(":", "") + extension;
                            postedFile.SaveAs(fileUploadPath + savingFileName);


                            //for saving multiple files and fileName with comma(,) separate
                            fileString = fileString + fileUploadPath + savingFileName + ";";
                            uploadedFileName = uploadedFileName + name + ";";
                            Dictionary<string, string> filePath = new Dictionary<string, string>();
                            filePath["ImgURL"] = "/UploadedFiles/" + dealerCode + "/" + username + "/" + savingFileName;
                            docReturn.filePaths.Add(filePath);
                        }

                        uploadedFileName = uploadedFileName.Remove((uploadedFileName.Length - 1));
                        fileString = fileString.Remove((fileString.Length - 1));

                        doc.filePath = fileString;
                        doc.uploadFileName = uploadedFileName;
                        doc.uploadDateTime = DateTime.Now;

                        db.documentuploadhistories.Add(doc);
                        db.SaveChanges();

                        docReturn.status = "1";
                        docReturn.fileName = documentName;
                        docReturn.fireBaseKey = fireBaseKey;
                    }
                    else
                    {
                        docReturn.fileName = "";
                        docReturn.status = "0";
                        logger.Info("No File Id Found");
                    }
                }
            }
            catch (Exception ex)
            {
                string exception = string.Empty;

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        exception = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        exception = ex.InnerException.Message;
                    }
                }
                else
                {
                    exception = ex.Message;
                }

                logger.Error("FE app file upload api error: \n" + exception);

                docReturn.fileName = "";
                docReturn.status = "0";
                return Ok<FEDocumentsReturn>(docReturn);
            }
            return Ok<FEDocumentsReturn>(docReturn);
        }
        [Route("api/AndroInteraction/uploaddriverappfiles")]
        [HttpPost()]
        public async Task<IHttpActionResult> uploaddriverappfiles()
        {
            var context = HttpContext.Current;
            FEDocumentsReturn docReturn = new FEDocumentsReturn();
            docReturn.filePaths = new List<Dictionary<string, string>>();
            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {
                string dealerCode = string.Empty, documentType = string.Empty, documentName = string.Empty, fileString = string.Empty, uploadedFileName = string.Empty;
                long custId = 0, userId = 0, driverscheduler_id = 0;
                string fileUploadPath = "", driver_name = string.Empty, fireBaseKey = string.Empty;// context.Server.MapPath("~/UploadedFiles/" + Session["DealerCode"].ToString() + "/" + Session["UserName"].ToString() + "/");
                var httpRequest = HttpContext.Current.Request;

                using (var db = new AutoSherDBContext())
                {
                    if (!string.IsNullOrEmpty(httpRequest.Form["driver_id"]))
                    {
                        userId = long.Parse(httpRequest.Form["driver_id"].ToString());
                    }

                    if (!string.IsNullOrEmpty(httpRequest.Form["custId"]))
                    {
                        custId = long.Parse(httpRequest.Form["custId"]);
                    }

                    if (!string.IsNullOrEmpty(httpRequest.Form["driverscheduler_id"]))
                    {
                        driverscheduler_id = long.Parse(httpRequest.Form["driverscheduler_id"]);
                    }



                    if (!string.IsNullOrEmpty(httpRequest.Form["documentType"]))
                    {
                        documentType = httpRequest.Form["documentType"];
                    }

                    if (!string.IsNullOrEmpty(httpRequest.Form["driver_name"]))
                    {
                        driver_name = httpRequest.Form["driver_name"];
                    }

                    if (!string.IsNullOrEmpty(httpRequest.Form["documentName"]))
                    {
                        documentName = httpRequest.Form["documentName"];
                    }

                    logger.Info(string.Format("\n Incoming/DriverApp doc details:\n documentType:{0}\n documentName:{1}\n Driver_userid:{2}| custId:{3}| driverScheduler_id:{4}", documentType, documentName, userId, custId, driverscheduler_id));



                    dealerCode = db.dealers.FirstOrDefault().dealerCode;



                    fileUploadPath = context.Server.MapPath("~/UploadedFiles/" + dealerCode + "/" + driver_name + "/");

                    if (!Directory.Exists(fileUploadPath))
                    {
                        Directory.CreateDirectory(fileUploadPath);
                    }


                    if (httpRequest.Files.Count > 0)
                    {
                        documentuploadhistory doc = new documentuploadhistory();
                        doc.customerId = custId;
                        doc.user = driver_name;
                        doc.userId = userId;
                        doc.deptName = documentType;
                        doc.documentName = documentName;

                        //var read1 = HttpContext.Current.Request.conte
                        var docfiles = new List<string>();
                        foreach (string file in httpRequest.Files)
                        {
                            string extension = string.Empty, name = string.Empty;

                            var postedFile = httpRequest.Files[file];
                            name = postedFile.FileName;
                            extension = Path.GetExtension(name);

                            string savingFileName = name.Split('.')[0] + "_" + custId + "_" + DateTime.Now.ToString("dd-MM-yyyy").Replace("-", "") + "_" + DateTime.Now.ToString("HH:mm:ss").Replace(":", "") + extension;
                            postedFile.SaveAs(fileUploadPath + savingFileName);


                            //for saving multiple files and fileName with comma(,) separate
                            fileString = fileString + fileUploadPath + savingFileName + ";";
                            uploadedFileName = uploadedFileName + name + ";";
                            Dictionary<string, string> filePath = new Dictionary<string, string>();
                            filePath["ImgURL"] = "/UploadedFiles/" + dealerCode + "/" + driver_name + "/" + savingFileName;
                            docReturn.filePaths.Add(filePath);
                        }

                        uploadedFileName = uploadedFileName.Remove((uploadedFileName.Length - 1));
                        fileString = fileString.Remove((fileString.Length - 1));

                        doc.filePath = fileString;
                        doc.uploadFileName = uploadedFileName;
                        doc.uploadDateTime = DateTime.Now;


                        fireBaseKey = db.driverSchedulers.FirstOrDefault(m => m.id == driverscheduler_id).firebasekey;

                        db.documentuploadhistories.Add(doc);
                        db.SaveChanges();

                        docReturn.status = "1";
                        docReturn.fileName = documentName;
                        docReturn.fireBaseKey = fireBaseKey;
                    }
                    else
                    {
                        docReturn.fileName = "";
                        docReturn.status = "0";
                        logger.Info("No File Id Found");
                    }
                }
            }
            catch (Exception ex)
            {
                string exception = string.Empty;

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        exception = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        exception = ex.InnerException.Message;
                    }
                }
                else
                {
                    exception = ex.Message;
                }

                logger.Error("FE app file upload api error: \n" + exception);

                docReturn.fileName = "";
                docReturn.status = "0";
                return Ok<FEDocumentsReturn>(docReturn);
            }
            return Ok<FEDocumentsReturn>(docReturn);
        }

        [Route("api/AndroInteraction/savefedetails")]
        [HttpPost]
        public IHttpActionResult SaveFeDetails([FromBody] FETracking fedata)
        {
            Logger logger = LogManager.GetLogger("apkRegLogger");
            dynamic data = new JObject();
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (fedata != null)
                    {
                        logger.Info("FE Login Details" + JsonConvert.SerializeObject(fedata));

                        if (db.wyzusers.Count(m => m.role == "InsuranceAgent" && m.phoneNumber == fedata.PhoneNumber) > 0)
                        {
                            fedata.WyzUserId = db.wyzusers.FirstOrDefault(m => m.role == "InsuranceAgent" && m.phoneNumber == fedata.PhoneNumber).id;
                            if (db.feTracking.Count(m => m.UniqueID == fedata.UniqueID && m.Flag == "login") > 0)
                            {
                                var updatefetrackdetails = db.feTracking.FirstOrDefault(m => m.UniqueID == fedata.UniqueID && m.Flag == "login");
                                updatefetrackdetails.Flag = fedata.Flag;
                                updatefetrackdetails.LogoutLocation = fedata.Location;
                                updatefetrackdetails.LogoutDate = fedata.LoginDate;
                                updatefetrackdetails.LogoutTime = fedata.LoginTime;
                                db.feTracking.AddOrUpdate(updatefetrackdetails);
                            }
                            else
                            {
                                db.feTracking.Add(fedata);
                            }
                            db.SaveChanges();

                            data.status = "Success";
                            return Ok<JObject>(data);
                        }
                        else
                        {
                            logger.Info("FE login Captured Failed: No User Found for PhoneNumber" + fedata.PhoneNumber);
                            data.status = "Failed";
                            data.exception = "No User data found";
                            return Ok<JObject>(data);
                        }
                    }
                    else
                    {
                        data.status = "Failed";
                        data.exception = "Incoming body is null";
                        return Ok<JObject>(data);

                    }
                }
            }
            catch (Exception ex)
            {
                string exception = "";

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        exception = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        exception = ex.InnerException.Message;
                    }
                }
                else
                {
                    exception = ex.Message;
                }
                if (ex.StackTrace.Contains(':'))
                {
                    exception = "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)] + " " + exception;
                }
                data.status = "Failed";
                data.exception = exception;
                return Ok<JObject>(data);
            }

        }


        #region Driver App APIs

        [Route("api/AndroInteraction/saveReachedPickUp")]
        [HttpPost]
        public IHttpActionResult saveReachedPickUp()
        {
            dynamic data = new JObject();
            DriverAppInteraction newIncomingData = new DriverAppInteraction();
            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {
                var httpRequest = HttpContext.Current.Request;

                //logger.Info("saveReachedPickUp-- Incoming Data:\n" + JsonConvert.SerializeObject(newIncomingData));
                using (var db = new AutoSherDBContext())
                {
                    DriverAppInteraction MaxDriverAppData = new DriverAppInteraction();
                    long vehicle_id = 0;
                    newIncomingData = bindFormdataModel(httpRequest.Form);


                    if (!string.IsNullOrEmpty(newIncomingData.VehicleId))
                    {
                        vehicle_id = long.Parse(newIncomingData.VehicleId);
                    }

                    if (string.IsNullOrEmpty(newIncomingData.UniqueKey))
                    {
                        MaxDriverAppData = db.driverAppInteraction.Where(m => m.IsPickUp == true && m.Vehicle_Id == vehicle_id).OrderByDescending(m => m.Id).FirstOrDefault();
                    }
                    else
                    {
                        MaxDriverAppData = db.driverAppInteraction.Where(m => m.IsPickUp == true && m.UniqueKey == newIncomingData.UniqueKey).OrderByDescending(m => m.Id).FirstOrDefault();
                    }

                    if (MaxDriverAppData == null)
                    {
                        MaxDriverAppData = new DriverAppInteraction();

                        MaxDriverAppData.Vehicle_Id = vehicle_id;
                        MaxDriverAppData.Customer_Id = newIncomingData.Customer_Id;
                        MaxDriverAppData.DriverScheduler_Id = newIncomingData.DriverScheduler_Id;
                        MaxDriverAppData.UniqueKey = newIncomingData.UniqueKey;
                        MaxDriverAppData.DriverId = newIncomingData.DriverId;
                        MaxDriverAppData.DriverName = newIncomingData.DriverName;
                    }

                    MaxDriverAppData.DemandedRepair = newIncomingData.DemandedRepair;
                    MaxDriverAppData.Mileage = newIncomingData.Mileage;
                    MaxDriverAppData.IsSameDayDelivery = newIncomingData.IsSameDayDelivery;
                    MaxDriverAppData.PaymentCollected = newIncomingData.PaymentCollected;
                    if (newIncomingData.PaymentCollected == "Yes")
                    {
                        MaxDriverAppData.PaymentReason = newIncomingData.PaymentReason;
                        MaxDriverAppData.PaymentMode = newIncomingData.PaymentMode;
                        MaxDriverAppData.Amount = newIncomingData.Amount;
                        MaxDriverAppData.ReferenceNo = newIncomingData.ReferenceNo;
                        MaxDriverAppData.PaymentRemarks = newIncomingData.PaymentRemarks;
                    }
                    else if (newIncomingData.PaymentCollected == "No")
                    {
                        MaxDriverAppData.PaymentReason = null;
                        MaxDriverAppData.PaymentMode = null;
                        MaxDriverAppData.Amount = null;
                        MaxDriverAppData.ReferenceNo = null;
                        MaxDriverAppData.PaymentRemarks = null;
                    }

                    MaxDriverAppData.Inventories = newIncomingData.Inventories;
                    MaxDriverAppData.IsPickUp = true;


                    string DealerCode = db.dealers.FirstOrDefault().dealerCode;
                    //File Processing part and saving
                    string fileUploadPath = HttpContext.Current.Server.MapPath("~/UploadedFiles/" + DealerCode + "/DriverAppFiles/" + newIncomingData.DriverName + "/");

                    if (!Directory.Exists(fileUploadPath))
                    {
                        Directory.CreateDirectory(fileUploadPath);
                    }

                    logger.Info("Files Count:" + httpRequest.Files != null ? httpRequest.Files.Count : 0);
                    List<DriverAppFileDetails> driverAppFilesList = new List<DriverAppFileDetails>();
                    if (httpRequest.Files.Count > 0)
                    {
                        foreach (string file in httpRequest.Files)
                        {
                            logger.Info("Files Name:" + file + " entered");
                            DriverAppFileDetails appFiles = new DriverAppFileDetails();

                            string extension = string.Empty, name = string.Empty;

                            var postedFile = httpRequest.Files[file];
                            name = postedFile.FileName;
                            extension = Path.GetExtension(name);

                            string savingFileName = name.Split('.')[0] + "_" + newIncomingData.DriverScheduler_Id.ToString() + "_" + DateTime.Now.ToString("dd-MM-yyyy").Replace("-", "") + "_" + DateTime.Now.ToString("HH:mm:ss").Replace(":", "") + extension;
                            postedFile.SaveAs(fileUploadPath + savingFileName);

                            appFiles.FileName = file;
                            appFiles.FilePath = ("/UploadedFiles/" + DealerCode + "/DriverAppFiles/" + newIncomingData.DriverName + "/" + savingFileName);
                            appFiles.PictureType = "PickUp Reached";
                            appFiles.PckUpDropType = 1;
                            appFiles.Vehicle_Id = MaxDriverAppData.Vehicle_Id;
                            appFiles.DriverScheduler_Id = MaxDriverAppData.DriverScheduler_Id;

                            driverAppFilesList.Add(appFiles);
                        }

                        db.driverAppFileDetails.AddRange(driverAppFilesList);
                        db.SaveChanges();
                    }

                    if (driverAppFilesList.Count() > 0)
                    {
                        MaxDriverAppData.DriverAppFiles_Ids = MaxDriverAppData.DriverAppFiles_Ids + string.Join(",", driverAppFilesList.Select(m => m.Id.ToString()).ToList());
                    }

                    MaxDriverAppData.InteractionType = "PickUp Reached";
                    MaxDriverAppData.LastUpdatedOn = DateTime.Now;
                    //MaxDriverAppData.Invoice_Amt = newIncomingData.Invoice_Amt;
                    MaxDriverAppData.PickupDateTime = newIncomingData.PickupDateTime;
                    // MaxDriverAppData.DeliveryDateTime = newIncomingData.DeliveryDateTime;
                    db.driverAppInteraction.Add(MaxDriverAppData);
                    db.SaveChanges();

                    data.status = "Success";
                    return Ok<JObject>(data);
                }
            }
            catch (Exception ex)
            {
                string exception = "";

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        exception = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        exception = ex.InnerException.Message;
                    }
                }
                else
                {
                    exception = ex.Message;
                }
                if (ex.StackTrace.Contains(':'))
                {
                    exception = "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)] + " " + exception;
                }
                data.status = "Failed";
                data.exception = exception;
                return Ok<JObject>(data);
            }
        }

        [Route("api/AndroInteraction/saveReachedWorkshop")]
        [HttpPost]
        public IHttpActionResult saveReachedWorkshop()
        {
            dynamic data = new JObject();
            DriverAppInteraction newIncomingData = new DriverAppInteraction();
            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {
                var httpRequest = HttpContext.Current.Request;
                newIncomingData = bindFormdataModel(httpRequest.Form);

                using (var db = new AutoSherDBContext())
                {
                    //logger.Info("saveReachedPickUp-- Incoming Data:\n" + JsonConvert.SerializeObject(newIncomingData));
                    DriverAppInteraction MaxDriverAppData = null;
                    long vehicle_id = 0;
                    if (!string.IsNullOrEmpty(newIncomingData.VehicleId))
                    {
                        vehicle_id = long.Parse(newIncomingData.VehicleId);
                    }
                    if (string.IsNullOrEmpty(newIncomingData.UniqueKey))
                    {
                        MaxDriverAppData = db.driverAppInteraction.Where(m => m.IsPickUp == true && m.Vehicle_Id == vehicle_id).OrderByDescending(m => m.Id).FirstOrDefault();
                    }
                    else
                    {
                        MaxDriverAppData = db.driverAppInteraction.Where(m => m.IsPickUp == true && m.UniqueKey == newIncomingData.UniqueKey).OrderByDescending(m => m.Id).FirstOrDefault();
                    }

                    if (MaxDriverAppData == null)
                    {
                        MaxDriverAppData = new DriverAppInteraction();

                        MaxDriverAppData.Vehicle_Id = vehicle_id;
                        MaxDriverAppData.Customer_Id = newIncomingData.Customer_Id;
                        MaxDriverAppData.DriverScheduler_Id = newIncomingData.DriverScheduler_Id;
                        MaxDriverAppData.UniqueKey = newIncomingData.UniqueKey;
                        MaxDriverAppData.DriverId = newIncomingData.DriverId;
                        MaxDriverAppData.DriverName = newIncomingData.DriverName;
                    }

                    string DealerCode = db.dealers.FirstOrDefault().dealerCode;
                    //File Processing part and saving
                    string fileUploadPath = HttpContext.Current.Server.MapPath("~/UploadedFiles/" + DealerCode + "/DriverAppFiles/" + newIncomingData.DriverName + "/");

                    if (!Directory.Exists(fileUploadPath))
                    {
                        Directory.CreateDirectory(fileUploadPath);
                    }


                    logger.Info("Files Count:" + httpRequest.Files != null ? httpRequest.Files.Count : 0);
                    List<DriverAppFileDetails> driverAppFilesList = new List<DriverAppFileDetails>();
                    if (httpRequest.Files.Count > 0)
                    {
                        foreach (string file in httpRequest.Files)
                        {
                            logger.Info("Files Name:" + file + " entered");
                            DriverAppFileDetails appFiles = new DriverAppFileDetails();

                            string extension = string.Empty, name = string.Empty;

                            var postedFile = httpRequest.Files[file];
                            name = postedFile.FileName;
                            extension = Path.GetExtension(name);

                            string savingFileName = name.Split('.')[0] + "_" + newIncomingData.DriverScheduler_Id.ToString() + "_" + DateTime.Now.ToString("dd-MM-yyyy").Replace("-", "") + "_" + DateTime.Now.ToString("HH:mm:ss").Replace(":", "") + extension;
                            postedFile.SaveAs(fileUploadPath + savingFileName);

                            appFiles.FileName = file;
                            // appFiles.FilePath = savingFileName;
                            appFiles.FilePath = ("/UploadedFiles/" + DealerCode + "/DriverAppFiles/" + newIncomingData.DriverName + "/" + savingFileName);
                            appFiles.PictureType = "PickUp Completed";
                            appFiles.PckUpDropType = 1;
                            appFiles.Vehicle_Id = MaxDriverAppData.Vehicle_Id;
                            appFiles.DriverScheduler_Id = MaxDriverAppData.DriverScheduler_Id;

                            driverAppFilesList.Add(appFiles);
                        }

                        db.driverAppFileDetails.AddRange(driverAppFilesList);
                        db.SaveChanges();
                    }

                    if (driverAppFilesList.Count() > 0)
                    {
                        MaxDriverAppData.DriverAppFiles_Ids = MaxDriverAppData.DriverAppFiles_Ids + string.Join(",", driverAppFilesList.Select(m => m.Id.ToString()).ToList());
                    }

                    MaxDriverAppData.InteractionType = "PickUp Completed";
                    MaxDriverAppData.WorkshopGateIntime = newIncomingData.WorkshopGateIntime;
                    MaxDriverAppData.MileageAfterGateIn = newIncomingData.MileageAfterGateIn;
                    MaxDriverAppData.IsPickUp = true;
                    MaxDriverAppData.LastUpdatedOn = DateTime.Now;
                    MaxDriverAppData.kmtravelled = newIncomingData.kmtravelled;
                    MaxDriverAppData.startlocation = newIncomingData.startlocation;
                    MaxDriverAppData.stoplocation = newIncomingData.stoplocation;
                    db.driverAppInteraction.Add(MaxDriverAppData);
                    db.SaveChanges();

                    data.status = "Success";
                    return Ok<JObject>(data);
                }
            }
            catch (Exception ex)
            {
                string exception = "";

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        exception = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        exception = ex.InnerException.Message;
                    }
                }
                else
                {
                    exception = ex.Message;
                }
                if (ex.StackTrace.Contains(':'))
                {
                    exception = "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)] + " " + exception;
                }
                data.status = "Failed";
                data.exception = exception;
                return Ok<JObject>(data);
            }
        }


        [Route("api/AndroInteraction/saveDeliveryCheck")]
        [HttpPost]
        public IHttpActionResult saveDeliveryCheck()
        {
            dynamic data = new JObject();
            DriverAppInteraction newIncomingData = new DriverAppInteraction();

            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {
                var httpRequest = HttpContext.Current.Request;
                newIncomingData = bindFormdataModel(httpRequest.Form);
                using (var db = new AutoSherDBContext())
                {
                    //logger.Info("saveReachedPickUp-- Incoming Data:\n" + JsonConvert.SerializeObject(newIncomingData));
                    DriverAppInteraction MaxDriverAppData = null;
                    long vehicle_id = 0;
                    if (!string.IsNullOrEmpty(newIncomingData.VehicleId))
                    {
                        vehicle_id = long.Parse(newIncomingData.VehicleId);
                    }
                    if (string.IsNullOrEmpty(newIncomingData.UniqueKey))
                    {
                        MaxDriverAppData = db.driverAppInteraction.Where(m => m.IsDrop == true && m.Vehicle_Id == vehicle_id).OrderByDescending(m => m.Id).FirstOrDefault();
                    }
                    else
                    {
                        MaxDriverAppData = db.driverAppInteraction.Where(m => m.IsDrop == true && m.UniqueKey == newIncomingData.UniqueKey).OrderByDescending(m => m.Id).FirstOrDefault();
                    }

                    if (MaxDriverAppData == null)
                    {
                        MaxDriverAppData = new DriverAppInteraction();

                        MaxDriverAppData.Vehicle_Id = vehicle_id;
                        MaxDriverAppData.Customer_Id = newIncomingData.Customer_Id;
                        MaxDriverAppData.DriverScheduler_Id = newIncomingData.DriverScheduler_Id;
                        MaxDriverAppData.UniqueKey = newIncomingData.UniqueKey;
                        MaxDriverAppData.DriverId = newIncomingData.DriverId;
                        MaxDriverAppData.DriverName = newIncomingData.DriverName;
                    }

                    MaxDriverAppData.Mileage = newIncomingData.Mileage;
                    MaxDriverAppData.Inventories = newIncomingData.Inventories;
                    MaxDriverAppData.IsDrop = true;

                    string DealerCode = db.dealers.FirstOrDefault().dealerCode;
                    //File Processing part and saving
                    string fileUploadPath = HttpContext.Current.Server.MapPath("~/UploadedFiles/" + DealerCode + "/DriverAppFiles/" + newIncomingData.DriverName + "/");

                    if (!Directory.Exists(fileUploadPath))
                    {
                        Directory.CreateDirectory(fileUploadPath);
                    }

                    List<DriverAppFileDetails> driverAppFilesList = new List<DriverAppFileDetails>();
                    logger.Info("Files Count:" + httpRequest.Files != null ? httpRequest.Files.Count : 0);
                    if (httpRequest.Files.Count > 0)
                    {
                        foreach (string file in httpRequest.Files)
                        {
                            logger.Info("Files Name:" + file + " entered");
                            DriverAppFileDetails appFiles = new DriverAppFileDetails();

                            string extension = string.Empty, name = string.Empty;

                            var postedFile = httpRequest.Files[file];
                            name = postedFile.FileName;
                            extension = Path.GetExtension(name);

                            string savingFileName = name.Split('.')[0] + "_" + newIncomingData.DriverScheduler_Id.ToString() + "_" + DateTime.Now.ToString("dd-MM-yyyy").Replace("-", "") + "_" + DateTime.Now.ToString("HH:mm:ss").Replace(":", "") + extension;
                            postedFile.SaveAs(fileUploadPath + savingFileName);

                            appFiles.FileName = file;
                            appFiles.FilePath = ("/UploadedFiles/" + DealerCode + "/DriverAppFiles/" + newIncomingData.DriverName + "/" + savingFileName);
                            // appFiles.FilePath = savingFileName;
                            appFiles.PictureType = "Drop Started";
                            appFiles.PckUpDropType = 1;
                            appFiles.Vehicle_Id = MaxDriverAppData.Vehicle_Id;
                            appFiles.DriverScheduler_Id = MaxDriverAppData.DriverScheduler_Id;

                            driverAppFilesList.Add(appFiles);
                        }

                        db.driverAppFileDetails.AddRange(driverAppFilesList);
                        db.SaveChanges();
                    }

                    if (driverAppFilesList.Count() > 0)
                    {
                        MaxDriverAppData.DriverAppFiles_Ids = MaxDriverAppData.DriverAppFiles_Ids + string.Join(",", driverAppFilesList.Select(m => m.Id.ToString()).ToList());
                    }
                    MaxDriverAppData.InteractionType = "Drop Started";
                    MaxDriverAppData.LastUpdatedOn = DateTime.Now;
                    MaxDriverAppData.Invoice_Amt = newIncomingData.Invoice_Amt;
                    // MaxDriverAppData.PickupDateTime = newIncomingData.PickupDateTime;
                    // MaxDriverAppData.DeliveryDateTime = newIncomingData.DeliveryDateTime;
                    db.driverAppInteraction.Add(MaxDriverAppData);
                    db.SaveChanges();

                    data.status = "Success";
                    return Ok<JObject>(data);
                }
            }
            catch (Exception ex)
            {
                string exception = "";

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        exception = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        exception = ex.InnerException.Message;
                    }
                }
                else
                {
                    exception = ex.Message;
                }
                if (ex.StackTrace.Contains(':'))
                {
                    exception = "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)] + " " + exception;
                }
                data.status = "Failed";
                data.exception = exception;
                return Ok<JObject>(data);
            }
        }

        [Route("api/AndroInteraction/saveReachedLocation")]
        [HttpPost]
        public IHttpActionResult saveReachedLocation()
        {
            dynamic data = new JObject();
            DriverAppInteraction newIncomingData = new DriverAppInteraction();
            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {
                var httpRequest = HttpContext.Current.Request;
                newIncomingData = bindFormdataModel(httpRequest.Form);

                using (var db = new AutoSherDBContext())
                {
                    DriverAppInteraction MaxDriverAppData = null;
                    long vehicle_id = 0;
                    if (!string.IsNullOrEmpty(newIncomingData.VehicleId))
                    {
                        vehicle_id = long.Parse(newIncomingData.VehicleId);
                    }
                    if (string.IsNullOrEmpty(newIncomingData.UniqueKey))
                    {
                        MaxDriverAppData = db.driverAppInteraction.Where(m => m.IsDrop == true && m.Vehicle_Id == vehicle_id).OrderByDescending(m => m.Id).FirstOrDefault();
                    }
                    else
                    {
                        MaxDriverAppData = db.driverAppInteraction.Where(m => m.IsDrop == true && m.UniqueKey == newIncomingData.UniqueKey).OrderByDescending(m => m.Id).FirstOrDefault();
                    }

                    if (MaxDriverAppData == null)
                    {
                        MaxDriverAppData = new DriverAppInteraction();

                        MaxDriverAppData.Vehicle_Id = vehicle_id;
                        MaxDriverAppData.Customer_Id = newIncomingData.Customer_Id;
                        MaxDriverAppData.DriverScheduler_Id = newIncomingData.DriverScheduler_Id;
                        MaxDriverAppData.UniqueKey = newIncomingData.UniqueKey;
                        MaxDriverAppData.DriverId = newIncomingData.DriverId;
                        MaxDriverAppData.DriverName = newIncomingData.DriverName;
                    }


                    MaxDriverAppData.PaymentCollected = newIncomingData.PaymentCollected;
                    if (newIncomingData.PaymentCollected == "Yes")
                    {
                        MaxDriverAppData.PaymentReason = newIncomingData.PaymentReason;
                        MaxDriverAppData.PaymentMode = newIncomingData.PaymentMode;
                        MaxDriverAppData.Amount = newIncomingData.Amount;
                        MaxDriverAppData.ReferenceNo = newIncomingData.ReferenceNo;
                        MaxDriverAppData.PaymentRemarks = newIncomingData.PaymentRemarks;
                    }

                    MaxDriverAppData.Inventories = newIncomingData.Inventories;
                    MaxDriverAppData.IsDrop = true;

                    string DealerCode = db.dealers.FirstOrDefault().dealerCode;
                    //File Processing part and saving
                    string fileUploadPath = HttpContext.Current.Server.MapPath("~/UploadedFiles/" + DealerCode + "/DriverAppFiles/" + newIncomingData.DriverName + "/");

                    if (!Directory.Exists(fileUploadPath))
                    {
                        Directory.CreateDirectory(fileUploadPath);
                    }

                    logger.Info("Files Count:" + httpRequest.Files != null ? httpRequest.Files.Count : 0);
                    List<DriverAppFileDetails> driverAppFilesList = new List<DriverAppFileDetails>();
                    if (httpRequest.Files.Count > 0)
                    {
                        foreach (string file in httpRequest.Files)
                        {
                            DriverAppFileDetails appFiles = new DriverAppFileDetails();
                            logger.Info("Files Name:" + file + " entered");
                            string extension = string.Empty, name = string.Empty;

                            var postedFile = httpRequest.Files[file];
                            name = postedFile.FileName;
                            extension = Path.GetExtension(name);

                            string savingFileName = name.Split('.')[0] + "_" + newIncomingData.DriverScheduler_Id.ToString() + "_" + DateTime.Now.ToString("dd-MM-yyyy").Replace("-", "") + "_" + DateTime.Now.ToString("HH:mm:ss").Replace(":", "") + extension;
                            postedFile.SaveAs(fileUploadPath + savingFileName);

                            appFiles.FileName = file;
                            //appFiles.FilePath = savingFileName;
                            appFiles.FilePath = ("/UploadedFiles/" + DealerCode + "/DriverAppFiles/" + newIncomingData.DriverName + "/" + savingFileName);
                            appFiles.PictureType = "Drop Completed";
                            appFiles.PckUpDropType = 1;
                            appFiles.Vehicle_Id = MaxDriverAppData.Vehicle_Id;
                            appFiles.DriverScheduler_Id = MaxDriverAppData.DriverScheduler_Id;

                            driverAppFilesList.Add(appFiles);
                        }

                        db.driverAppFileDetails.AddRange(driverAppFilesList);
                        db.SaveChanges();
                    }

                    if (driverAppFilesList.Count() > 0)
                    {
                        MaxDriverAppData.DriverAppFiles_Ids = MaxDriverAppData.DriverAppFiles_Ids + string.Join(",", driverAppFilesList.Select(m => m.Id.ToString()).ToList());
                    }
                    MaxDriverAppData.InteractionType = "Drop Completed";
                    MaxDriverAppData.LastUpdatedOn = DateTime.Now;
                    // MaxDriverAppData.Invoice_Amt = newIncomingData.Invoice_Amt;
                    //MaxDriverAppData.PickupDateTime = newIncomingData.PickupDateTime;
                    MaxDriverAppData.DeliveryDateTime = newIncomingData.DeliveryDateTime;

                    MaxDriverAppData.kmtravelled = newIncomingData.kmtravelled;
                    MaxDriverAppData.startlocation = newIncomingData.startlocation;
                    MaxDriverAppData.stoplocation = newIncomingData.stoplocation;
                    db.driverAppInteraction.Add(MaxDriverAppData);
                    db.SaveChanges();

                    data.status = "Success";
                    return Ok<JObject>(data);
                }
            }
            catch (Exception ex)
            {
                string exception = "";

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        exception = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        exception = ex.InnerException.Message;
                    }
                }
                else
                {
                    exception = ex.Message;
                }
                if (ex.StackTrace.Contains(':'))
                {
                    exception = "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)] + " " + exception;
                }
                data.status = "Failed";
                data.exception = exception;
                return Ok<JObject>(data);
            }
        }

        [Route("api/AndroInteraction/saveDriverDisposition")]
        [HttpPost]
        public IHttpActionResult saveDriverDisposition([FromBody] DriverAppInteraction newIncomingData)
        {
            dynamic data = new JObject();
            Logger logger = LogManager.GetLogger("apkRegLogger");

            try
            {
                using (var db = new AutoSherDBContext())
                {

                    logger.Info("saveReachedPickUp-- Incoming Data:\n" + JsonConvert.SerializeObject(newIncomingData));
                    DriverAppInteraction MaxDriverAppData = null;
                    long vehicle_id = 0;
                    if (!string.IsNullOrEmpty(newIncomingData.VehicleId))
                    {
                        vehicle_id = long.Parse(newIncomingData.VehicleId);
                    }
                    if (string.IsNullOrEmpty(newIncomingData.UniqueKey))
                    {
                        if (newIncomingData.PickUpDropType == 1)
                        {
                            MaxDriverAppData = db.driverAppInteraction.Where(m => m.IsPickUp == true && m.Vehicle_Id == vehicle_id).OrderByDescending(m => m.Id).FirstOrDefault();
                        }
                        else if (newIncomingData.PickUpDropType == 2)
                        {
                            MaxDriverAppData = db.driverAppInteraction.Where(m => m.IsDrop == true && m.Vehicle_Id == vehicle_id).OrderByDescending(m => m.Id).FirstOrDefault();
                        }
                    }
                    else
                    {
                        if (newIncomingData.PickUpDropType == 1)
                        {
                            MaxDriverAppData = db.driverAppInteraction.Where(m => m.IsPickUp == true && m.UniqueKey == newIncomingData.UniqueKey).OrderByDescending(m => m.Id).FirstOrDefault();
                        }
                        else if (newIncomingData.PickUpDropType == 2)
                        {
                            MaxDriverAppData = db.driverAppInteraction.Where(m => m.IsDrop == true && m.UniqueKey == newIncomingData.UniqueKey).OrderByDescending(m => m.Id).FirstOrDefault();
                        }
                    }

                    if (MaxDriverAppData == null)
                    {
                        MaxDriverAppData = new DriverAppInteraction();

                        MaxDriverAppData.Vehicle_Id = vehicle_id;
                        MaxDriverAppData.Customer_Id = newIncomingData.Customer_Id;
                        MaxDriverAppData.DriverScheduler_Id = newIncomingData.DriverScheduler_Id;
                        MaxDriverAppData.UniqueKey = newIncomingData.UniqueKey;
                        MaxDriverAppData.DriverId = newIncomingData.DriverId;
                        MaxDriverAppData.DriverName = newIncomingData.DriverName;
                    }


                    MaxDriverAppData.Disposition = newIncomingData.Disposition;
                    MaxDriverAppData.Reasons = newIncomingData.Reasons;

                    if (newIncomingData.Disposition == "Rescheduled")
                    {
                        if (newIncomingData.RescheduledDateTime != null)
                        {
                            //--------------Need to Change Saving Logic
                            //MaxDriverAppData.RescheduledDateTime = Convert.ToDateTime(newIncomingData.RescheduledDateTime);

                            //long DriverBooking_id = db.driverSchedulers.FirstOrDefault(m => m.id == newIncomingData.DriverScheduler_Id).driverBookingdetails_id;

                            //if (DriverBooking_id != 0)
                            //{
                            //    DriverBookingDetails driverBookDetails = db.driverBookingDetails.FirstOrDefault(m => m.id == DriverBooking_id);

                            //    driverBookDetails.BookingDate = null;
                            //    driverBookDetails.Driver_Id = 0;

                            //    db.driverBookingDetails.AddOrUpdate(driverBookDetails);
                            //    db.SaveChanges();
                            //}
                        }
                    }
                    else if (newIncomingData.Disposition == "NotInterested")
                    {
                        driverscheduler driverschedulerData = db.driverSchedulers.FirstOrDefault(m => m.id == newIncomingData.DriverScheduler_Id);

                        if (driverschedulerData != null)
                        {
                            driverschedulerData.ispushed = false;
                            driverschedulerData.IsCancelled = true;

                            db.driverSchedulers.AddOrUpdate(driverschedulerData);
                            db.SaveChanges();
                        }
                    }
                    else if (newIncomingData.Disposition == "ReachedPickUp")
                    {
                        DriversDeliveryNotes note = db.driversDeliveryNotes.Where(m => m.DriverSchedulerId == newIncomingData.DriverScheduler_Id && m.IsMap == true).OrderByDescending(m => m.Id).FirstOrDefault();

                        if (note != null)
                        {
                            db.driversDeliveryNotes.Remove(note);
                            db.SaveChanges();
                        }
                    }
                    else if (newIncomingData.Disposition == "DropComplete")
                    {
                        DriversDeliveryNotes note = db.driversDeliveryNotes.Where(m => m.DriverSchedulerId == newIncomingData.DriverScheduler_Id && m.IsMap == true).OrderByDescending(m => m.Id).FirstOrDefault();

                        if (note != null)
                        {
                            db.driversDeliveryNotes.Remove(note);
                            db.SaveChanges();
                        }
                    }


                    if (newIncomingData.PickUpDropType == 1)
                    {
                        MaxDriverAppData.IsPickUp = true;
                    }
                    else if (newIncomingData.PickUpDropType == 2)
                    {
                        MaxDriverAppData.IsDrop = true;
                    }
                    MaxDriverAppData.LastUpdatedOn = DateTime.Now;

                    db.driverAppInteraction.Add(MaxDriverAppData);
                    db.SaveChanges();

                    data.status = "Success";
                    return Ok<JObject>(data);
                }
            }
            catch (Exception ex)
            {
                string exception = "";

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        exception = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        exception = ex.InnerException.Message;
                    }
                }
                else
                {
                    exception = ex.Message;
                }
                if (ex.StackTrace.Contains(':'))
                {
                    exception = "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)] + " " + exception;
                }
                data.status = "Failed";
                data.exception = exception;
                return Ok<JObject>(data);
            }
        }

        [Route("api/AndroInteraction/saveSMSInteraction"), HttpPost, System.Web.Mvc.ValidateInput(false)]
        public IHttpActionResult saveSMSInteraction([FromBody] smsSaving smsRepsonse)
        {
            dynamic data = new JObject();
            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {
                logger.Info("Driver SMS API Saving");
                var httpRequest = HttpContext.Current.Request;

                if (smsRepsonse != null)
                {
                    using (var db = new AutoSherDBContext())
                    {
                        smsinteraction smsinteraction = new smsinteraction();

                        smsinteraction.interactionDate = DateTime.Now.ToString("dd-MM-yyyy");
                        smsinteraction.interactionDateAndTime = DateTime.Now;
                        smsinteraction.interactionTime = DateTime.Now.ToString("HH:mm:ss");
                        smsinteraction.interactionType = "Text Msg";
                        smsinteraction.responseFromGateway = smsRepsonse.response_string;
                        smsinteraction.customer_id = smsRepsonse.custId;
                        smsinteraction.vehicle_vehicle_id = smsRepsonse.vehiId;
                        smsinteraction.wyzUser_id = smsRepsonse.driver_id;
                        smsinteraction.mobileNumber = smsRepsonse.phNum;
                        smsinteraction.smsType = "004";
                        smsinteraction.smsMessage = smsRepsonse.message;
                        smsinteraction.isAutoSMS = false;

                        Regex r = new Regex(@"^\d+$");
                        if (r.IsMatch(smsRepsonse.response_string))
                        {
                            //only number
                            smsinteraction.smsStatus = true;
                            smsinteraction.reason = "Send Successfully";
                        }
                        else
                        {
                            smsstatu status = new smsstatu();

                            //response_string = "200";

                            status = db.smsstatus.FirstOrDefault(m => smsRepsonse.response_string.Contains(m.code));
                            if (status == null)
                            {
                                smsinteraction.smsStatus = false;
                                smsinteraction.reason = "Sending Failed";
                            }
                            else if (status != null)
                            {
                                smsinteraction.smsStatus = false;
                                smsinteraction.reason = status.description;
                            }
                        }

                        db.smsinteractions.Add(smsinteraction);
                        db.SaveChanges();
                    }
                }
                else
                {
                    data.status = "Failed";
                    data.exception = "Invalid Request";
                    return Ok<JObject>(data);
                }

            }
            catch (Exception ex)
            {
                string exception = "";

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        exception = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        exception = ex.InnerException.Message;
                    }
                }
                else
                {
                    exception = ex.Message;
                }
                if (ex.StackTrace.Contains(':'))
                {
                    exception = "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)] + " " + exception;
                }
                data.status = "Failed";
                data.exception = exception;
                return Ok<JObject>(data);
            }

            data.status = "Success";
            return Ok<JObject>(data);
        }

        [Route("api/AndroInteraction/getMapUrl"), HttpGet]
        public IHttpActionResult getMapUrl(int id)
        {
            dynamic data = new JObject();
            Logger logger = LogManager.GetLogger("apkRegLogger");
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    if (db.driverSchedulers.Count(m => m.id == id) == 1)
                    {
                        logger.Info("Getting Map URL started");

                        driverscheduler schedulerDetails = db.driverSchedulers.FirstOrDefault(m => m.id == id);
                        var httpRequest = HttpContext.Current.Request;

                        DriversDeliveryNotes deliveryNotes = new DriversDeliveryNotes();

                        deliveryNotes.VehicleId = schedulerDetails.vehicle_id;
                        deliveryNotes.CustomerId = schedulerDetails.customer_id;

                        string DeliveryLink = httpRequest.Url.Host + httpRequest.ApplicationPath + "/Home/driverlocation/" + schedulerDetails.vehicle_id.ToString() + "_" + schedulerDetails.customer_id.ToString();

                        //DeliveryLink =  HttpUtility.UrlEncode(DeliveryLink);
                        logger.Info("Map Link Delicry Link: " + DeliveryLink);
                        deliveryNotes.DriverSchedulerId = id;
                        deliveryNotes.IsMap = true;

                        db.driversDeliveryNotes.Add(deliveryNotes);
                        db.SaveChanges();

                        data.status = "Success";
                        data.MapLink = DeliveryLink;
                        return Ok<JObject>(data);
                    }
                    else
                    {
                        data.status = "Failed";
                        data.exception = "Invalid request id";
                        return Ok<JObject>(data);
                    }
                }
            }
            catch (Exception ex)
            {
                string exception = "";

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        exception = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        exception = ex.InnerException.Message;
                    }
                }
                else
                {
                    exception = ex.Message;
                }
                if (ex.StackTrace.Contains(':'))
                {
                    exception = "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)] + " " + exception;
                }
                data.status = "Failed";
                data.exception = exception;
                return Ok<JObject>(data);
            }
        }


        [Route("api/AndroInteraction/driverApproval")]
        [HttpGet]
        public IHttpActionResult driverApproval(int id)
        {
            dynamic data = new JObject();
            Logger logger = LogManager.GetLogger("apkRegLogger");
            //  driverapprovalDocuments driverapprovalDocuments = new driverapprovalDocuments();
            // driverapprovalDocuments.filePaths = new List<Dictionary<string, string>>();
            string DeliveryLink = string.Empty;
            try
            {
                using (var db = new AutoSherDBContext())
                {
                    var driverscheulerdetails = db.driverSchedulers.Where(m => m.id == id).Select(m => new { m.driverBookingdetails_id, m.driver_id }).FirstOrDefault();
                    var bookingdetails = db.driverBookingDetails.Where(m => m.id == driverscheulerdetails.driverBookingdetails_id).Select(m => new { m.BookingDate, m.BookingTime }).FirstOrDefault();
                    string driverName = db.drivers.FirstOrDefault(m => m.id == driverscheulerdetails.driver_id && m.isactive == true).driverName;
                    string DealerCode = db.dealers.FirstOrDefault().dealerCode;
                    //string fileUploadPath = HttpContext.Current.Server.MapPath("~/UploadedFiles/" + DealerCode + "/DriverAppFiles/" + driverName + "/");
                    var httpRequest = HttpContext.Current.Request;

                    DeliveryLink = httpRequest.Url.Host + httpRequest.ApplicationPath + "/Home/driverdeliveryImage/" + id;

                    //driverapprovalDocuments.appointmentDate = bookingdetails.BookingDate;
                    //driverapprovalDocuments.appointmentTime = bookingdetails.BookingTime;
                    // driverapprovalDocuments.imageLink = DeliveryLink;
                    //var driverfiles = db.driverAppFileDetails.Where(m => m.DriverScheduler_Id == id).ToList();
                    //if(driverfiles.Count>0)
                    //{
                    //    foreach (var files in driverfiles)
                    //    {
                    //        Dictionary<string, string> filePath = new Dictionary<string, string>();
                    //        filePath["fileName"] = files.FileName;
                    //        filePath["path"] =fileUploadPath+files.FilePath;
                    //        driverapprovalDocuments.filePaths.Add(filePath);
                    //    }
                    //}

                }
            }
            catch (Exception ex)
            {
                string exception = "";

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        exception = ex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        exception = ex.InnerException.Message;
                    }
                }
                else
                {
                    exception = ex.Message;
                }
                if (ex.StackTrace.Contains(':'))
                {
                    exception = "Line: " + ex.StackTrace.Split(':')[(ex.StackTrace.Split(':').Count() - 1)] + " " + exception;
                }
                data.status = "Failed";
                data.exception = exception;
                return Ok<JObject>(data);
            }
            data.status = "success";
            return Ok<string>(DeliveryLink);
        }


        #endregion


        public DriverAppInteraction bindFormdataModel(NameValueCollection Form)
        {
            DriverAppInteraction newIncomingData = new DriverAppInteraction();

            if (!string.IsNullOrEmpty(Form["Mileage"]))
            {
                newIncomingData.Mileage = Form["Mileage"];
            }
            if (!string.IsNullOrEmpty(Form["DemandedRepair"]))
            {
                newIncomingData.DemandedRepair = Form["DemandedRepair"];
            }
            if (!string.IsNullOrEmpty(Form["IsSameDayDelivery"]))
            {
                newIncomingData.IsSameDayDelivery = bool.Parse(Form["IsSameDayDelivery"]);
            }
            if (!string.IsNullOrEmpty(Form["PaymentCollected"]))
            {
                newIncomingData.PaymentCollected = Form["PaymentCollected"];
            }
            if (!string.IsNullOrEmpty(Form["WorkshopGateIntime"]))
            {
                newIncomingData.WorkshopGateIntime = Form["WorkshopGateIntime"];
            }
            if (!string.IsNullOrEmpty(Form["MileageAfterGateIn"]))
            {
                newIncomingData.MileageAfterGateIn = Form["MileageAfterGateIn"];
            }
            if (!string.IsNullOrEmpty(Form["PaymentReason"]))
            {
                newIncomingData.PaymentReason = Form["PaymentReason"];
            }
            if (!string.IsNullOrEmpty(Form["PaymentMode"]))
            {
                newIncomingData.PaymentMode = Form["PaymentMode"];
            }
            if (!string.IsNullOrEmpty(Form["Amount"]))
            {
                newIncomingData.Amount = Form["Amount"];
            }
            if (!string.IsNullOrEmpty(Form["ReferenceNo"]))
            {
                newIncomingData.ReferenceNo = Form["ReferenceNo"];
            }
            if (!string.IsNullOrEmpty(Form["PaymentRemarks"]))
            {
                newIncomingData.PaymentRemarks = Form["PaymentRemarks"];
            }
            if (!string.IsNullOrEmpty(Form["DriverScheduler_Id"]))
            {
                newIncomingData.DriverScheduler_Id = long.Parse(Form["DriverScheduler_Id"]);
            }
            if (!string.IsNullOrEmpty(Form["Vehicle_Id"]))
            {
                newIncomingData.Vehicle_Id = long.Parse(Form["Vehicle_Id"]);
            }
            if (!string.IsNullOrEmpty(Form["UniqueKey"]))
            {
                newIncomingData.UniqueKey = Form["UniqueKey"];
            }
            if (!string.IsNullOrEmpty(Form["IsPickUp"]))
            {
                newIncomingData.IsPickUp = bool.Parse(Form["IsPickUp"]);
            }
            if (!string.IsNullOrEmpty(Form["IsDrop"]))
            {
                newIncomingData.IsDrop = bool.Parse(Form["IsDrop"]);
            }
            if (!string.IsNullOrEmpty(Form["Inventories"]))
            {
                newIncomingData.Inventories = Form["Inventories"];
            }
            if (!string.IsNullOrEmpty(Form["Disposition"]))
            {
                newIncomingData.Disposition = Form["Disposition"];
            }
            if (!string.IsNullOrEmpty(Form["Reasons"]))
            {
                newIncomingData.Reasons = Form["Reasons"];
            }
            if (!string.IsNullOrEmpty(Form["RescheduledDateTime"]))
            {
                newIncomingData.RescheduledDateTime = Convert.ToDateTime(Form["RescheduledDateTime"]);
            }

            if (!string.IsNullOrEmpty(Form["InteractionType"]))
            {
                newIncomingData.InteractionType = Form["InteractionType"];
            }

            if (!string.IsNullOrEmpty(Form["DriverAppFiles_Ids"]))
            {
                newIncomingData.DriverAppFiles_Ids = Form["DriverAppFiles_Ids"];
            }
            if (!string.IsNullOrEmpty(Form["PickUpDropType"]))
            {
                newIncomingData.PickUpDropType = int.Parse(Form["PickUpDropType"]);
            }
            if (!string.IsNullOrEmpty(Form["VehicleId"]))
            {
                newIncomingData.VehicleId = Form["VehicleId"];
            }
            if (!string.IsNullOrEmpty(Form["DriverName"]))
            {
                newIncomingData.DriverName = Form["DriverName"];
            }
            if (!string.IsNullOrEmpty(Form["DriverId"]))
            {
                newIncomingData.DriverId = long.Parse(Form["DriverId"]);
            }
            if (!string.IsNullOrEmpty(Form["Driver_Id"]))
            {
                newIncomingData.DriverId = long.Parse(Form["Driver_Id"]);
            }
            if (!string.IsNullOrEmpty(Form["LastUpdatedOn"]))
            {
                newIncomingData.LastUpdatedOn = Convert.ToDateTime(Form["LastUpdatedOn"]);
            }
            if (!string.IsNullOrEmpty(Form["CREName"]))
            {
                newIncomingData.CREName = Form["CREName"];
            }
            if (!string.IsNullOrEmpty(Form["Details"]))
            {
                newIncomingData.Details = Form["Details"];
            }
            if (!string.IsNullOrEmpty(Form["Invoice_Amt"]))
            {
                newIncomingData.Invoice_Amt = Form["Invoice_Amt"];
            }
            if (!string.IsNullOrEmpty(Form["PickupDateTime"]))
            {
                newIncomingData.PickupDateTime = Form["PickupDateTime"];
            }
            if (!string.IsNullOrEmpty(Form["DeliveryDateTime"]))
            {
                newIncomingData.DeliveryDateTime = Form["DeliveryDateTime"];
            }
            if (!string.IsNullOrEmpty(Form["kmtravelled"]))
            {
                newIncomingData.kmtravelled = Form["kmtravelled"];
            }
            if (!string.IsNullOrEmpty(Form["startlocation"]))
            {
                newIncomingData.startlocation = Form["startlocation"];
            }
            if (!string.IsNullOrEmpty(Form["stoplocation"]))
            {
                newIncomingData.stoplocation = Form["stoplocation"];
            }
            return newIncomingData;
        }
    }

    public class AppLogCheck
    {
        public string phoneNumber { get; set; }
        public string phoneIMEINo { get; set; }
        public string registrationId { get; set; }
        public string latestAppVersion { get; set; }

        public string fileName { get; set; }
        public string dealerCode { get; set; }
        public string feRequest { get; set; }
        public string FEName { get; set; }
    }

    public class FEAppAddressUpdate
    {
        public string dealerCode { get; set; }
        public string address { get; set; }
        public string appointmentbookedId { get; set; }
        public long pincode { get; set; }
        public long DriverScheduler_id { get; set; }

    }

    public class smsSaving
    {
        public long custId { get; set; }
        public long vehiId { get; set; }
        public long driver_id { get; set; }

        [System.Web.Mvc.AllowHtml]
        public string response_string { get; set; }
        public string phNum { get; set; }
        public string message { get; set; }
    }

    public class FEDocumentsReturn
    {
        public string status { get; set; }
        public string fileName { get; set; }
        public List<Dictionary<string, string>> filePaths { get; set; }
        public string fireBaseKey { get; set; }
    }

    public class returnApk : IHttpActionResult
    {
        MemoryStream bookStuff;
        string PdfFileName;
        HttpRequestMessage httpRequestMessage;
        HttpResponseMessage httpResponseMessage;
        public returnApk(MemoryStream data, HttpRequestMessage request, string filename)
        {
            bookStuff = data;
            httpRequestMessage = request;
            PdfFileName = filename;
        }
        public System.Threading.Tasks.Task<HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            httpResponseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.OK);
            httpResponseMessage.Content = new StreamContent(bookStuff);
            //httpResponseMessage.Content = new ByteArrayContent(bookStuff.ToArray());  
            httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            httpResponseMessage.Content.Headers.ContentDisposition.FileName = PdfFileName;
            httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            return System.Threading.Tasks.Task.FromResult(httpResponseMessage);
        }
    }

    public class serviceDueDetails
    {
        public string vehicleRegNo { get; set; }
        public string phoneNumber { get; set; }
    }
}
