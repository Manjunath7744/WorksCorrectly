using System;
using System.Data.Entity;
using MySql.Data.EntityFramework;
using System.Web;
using AutoSherpa_project.Models.ViewModels;

namespace AutoSherpa_project.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public partial class AutoSherDBContext : DbContext
    {
        public static string contextName = "";
        public AutoSherDBContext()
            //: base("name="+AutoSherDBContext.getContextName())
            : base("name=AutoSherContext")
        {
        }

        //public AutoSherDBContext(string contextName)
        //    //: base("name="+AutoSherDBContext.getContextName())
        //    : base("name=" + contextName)
        //{
        //}

        public static void setContextName(string context)
        {
            try
            {
                HttpContext.Current.Session.Add("DbContextName", context);
            }
            catch (Exception ex)
            {
                //string siteRoot = HttpRuntime.AppDomainAppVirtualPath;
                //RedirectResult redirect = new RedirectResult();
                //redirect.Url = siteRoot + "/Home/Index";
                //return RedirectResult
            }

        }

        public static string getContextName()
        {
            try
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    return HttpContext.Current.Session["DbContextName"].ToString();
                }
                else
                {
                    return contextName;
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public virtual DbSet<addoncover> addoncovers { get; set; }
        public virtual DbSet<address> addresses { get; set; }
        public virtual DbSet<agency> agencies { get; set; }
        public virtual DbSet<agent_day_wise_data> agent_day_wise_data { get; set; }
        public virtual DbSet<agent_roaster> agent_roaster { get; set; }
        public virtual DbSet<agent_wise_mtd> agent_wise_mtd { get; set; }
        public virtual DbSet<agentdata> agentdatas { get; set; }
        public virtual DbSet<agentdatablob> agentdatablobs { get; set; }
        public virtual DbSet<agentdigicalldata> agentdigicalldatas { get; set; }
        public virtual DbSet<appointmentbooked> appointmentbookeds { get; set; }
        public virtual DbSet<assigmentplan> assigmentplans { get; set; }
        public virtual DbSet<assignedcallsreport> assignedcallsreports { get; set; }
        public virtual DbSet<assignedinteraction> assignedinteractions { get; set; }
        public virtual DbSet<assignmentreport_nthday> assignmentreport_nthday { get; set; }
        public virtual DbSet<authorizedip> authorizedips { get; set; }
        public virtual DbSet<automessageparameter> automessageparameters { get; set; }
        public virtual DbSet<autosmsday> autosmsdays { get; set; }
        public virtual DbSet<autosmstemplate> autosmstemplates { get; set; }
        public virtual DbSet<availability> availabilities { get; set; }
        public virtual DbSet<axisdetail> axisdetails { get; set; }
        public virtual DbSet<bay> bays { get; set; }
        public virtual DbSet<bookeddataremainder> bookeddataremainders { get; set; }
        public virtual DbSet<bookingdatetime> bookingdatetimes { get; set; }
        public virtual DbSet<callassignmentstatu> callassignmentstatus { get; set; }
        public virtual DbSet<calldispositiondata> calldispositiondatas { get; set; }
        public virtual DbSet<callhistorycube> callhistorycubes { get; set; }
        public virtual DbSet<callinteraction> callinteractions { get; set; }
        public virtual DbSet<campaign> campaigns { get; set; }
        public virtual DbSet<campaignexcelerror> campaignexcelerrors { get; set; }
        public virtual DbSet<caselist> caselists { get; set; }
        public virtual DbSet<casesummary> casesummaries { get; set; }
        public virtual DbSet<change_assignment_records> change_assignment_records { get; set; }
        public virtual DbSet<chartdata> chartdatas { get; set; }
        public virtual DbSet<chaserassignementhistory> chaserassignementhistories { get; set; }
        public virtual DbSet<checkupdatecount> checkupdatecounts { get; set; }
        public virtual DbSet<citystate> citystates { get; set; }
        public virtual DbSet<columndefinition> columndefinitions { get; set; }
        public virtual DbSet<complaint> complaints { get; set; }
        public virtual DbSet<complaintassignedinteraction> complaintassignedinteractions { get; set; }
        public virtual DbSet<complaintinteraction> complaintinteractions { get; set; }
        public virtual DbSet<complaintsource> complaintsources { get; set; }
        public virtual DbSet<complainttype> complainttypes { get; set; }
        public virtual DbSet<conversiondecription> conversiondecriptions { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<customersearchresult> customersearchresults { get; set; }
        public virtual DbSet<datarejectioncause> datarejectioncauses { get; set; }
        public virtual DbSet<dealer> dealers { get; set; }
        public virtual DbSet<dealerpanel> dealerpanels { get; set; }
        public virtual DbSet<deletedinteractionsforcancelledandclosed> deletedinteractionsforcancelledandcloseds { get; set; }
        public virtual DbSet<deletedstatusreport> deletedstatusreports { get; set; }
        public virtual DbSet<documentuploadhistory> documentuploadhistories { get; set; }
        public virtual DbSet<driver> drivers { get; set; }
        public virtual DbSet<driverinteraction> driverinteractions { get; set; }
        public virtual DbSet<driverinteractionstatu> driverinteractionstatus { get; set; }
        public virtual DbSet<dynamicdashformat> dynamicdashformats { get; set; }
        public virtual DbSet<email> emails { get; set; }
        public virtual DbSet<emailcredential> emailcredentials { get; set; }
        public virtual DbSet<emailinteraction> emailinteractions { get; set; }
        public virtual DbSet<emailtemplate> emailtemplates { get; set; }
        public virtual DbSet<feedbackallocation> feedbackallocations { get; set; }
        public virtual DbSet<feedbackrating> feedbackratings { get; set; }
        public virtual DbSet<fieldwalkinlocation> fieldwalkinlocations { get; set; }
        public virtual DbSet<fileuploadformat> fileuploadformats { get; set; }
        public virtual DbSet<fmanualuser> fmanualusers { get; set; }
        public virtual DbSet<forecastlogicservicetype> forecastlogicservicetypes { get; set; }
        public virtual DbSet<forecastservicecategory> forecastservicecategories { get; set; }
        public virtual DbSet<forecastservicetype> forecastservicetypes { get; set; }
        public virtual DbSet<forex> forexes { get; set; }
        public virtual DbSet<formdata> formdatas { get; set; }
        public virtual DbSet<imagestorage> imagestorages { get; set; }
        public virtual DbSet<insautoassignmentsummary> insautoassignmentsummaries { get; set; }
        public virtual DbSet<insforecastassignmenthistory> insforecastassignmenthistories { get; set; }
        public virtual DbSet<insurance> insurances { get; set; }
        public virtual DbSet<insurance_history> insurance_history { get; set; }
        public virtual DbSet<insuranceagent> insuranceagents { get; set; }
        public virtual DbSet<insuranceassignedinteraction> insuranceassignedinteractions { get; set; }
        public virtual DbSet<insuranceassignmentreport_nthday> insuranceassignmentreport_nthday { get; set; }
        public virtual DbSet<insurancecallhistorycube> insurancecallhistorycubes { get; set; }
        public virtual DbSet<insurancecompany> insurancecompanies { get; set; }
        public virtual DbSet<insurancedisposition> insurancedispositions { get; set; }
        public virtual DbSet<insuranceexceldata> insuranceexceldatas { get; set; }
        public virtual DbSet<insuranceexcelerror> insuranceexcelerrors { get; set; }
        public virtual DbSet<insuranceforecasteddata> insuranceforecasteddatas { get; set; }
        public virtual DbSet<insurancequotation> insurancequotations { get; set; }
        public virtual DbSet<insurance1> insurances1 { get; set; }
        public virtual DbSet<invalidworkshopdata> invalidworkshopdatas { get; set; }
        public virtual DbSet<irda_od_premium> irda_od_premium { get; set; }
        public virtual DbSet<jobcard_report> jobcard_report { get; set; }
        public virtual DbSet<jobcardstatuserror> jobcardstatuserrors { get; set; }
        public virtual DbSet<location> locations { get; set; }
        public virtual DbSet<logininfo> logininfoes { get; set; }
        public virtual DbSet<logininformation> logininformations { get; set; }
        public virtual DbSet<messageparameter> messageparameters { get; set; }
        public virtual DbSet<messagetemplete> messagetempletes { get; set; }
        public virtual DbSet<modelcache> modelcaches { get; set; }
        public virtual DbSet<modelslist> modelslists { get; set; }
        public virtual DbSet<moduletype> moduletypes { get; set; }
        public virtual DbSet<movie> movies { get; set; }
        public virtual DbSet<outlet> outlets { get; set; }
        public virtual DbSet<phone> phones { get; set; }
        public virtual DbSet<phonenodeletion> phonenodeletions { get; set; }
        public virtual DbSet<pickupdrop> pickupdrops { get; set; }
        public virtual DbSet<postsalescallhistorycube> postsalescallhistorycubes { get; set; }
        public virtual DbSet<postservicefeedback> postservicefeedbacks { get; set; }
        public virtual DbSet<postupdate> postupdates { get; set; }
        public virtual DbSet<postupdatemapping> postupdatemappings { get; set; }
        public virtual DbSet<primaryservicedata> primaryservicedatas { get; set; }
        public virtual DbSet<processdefinition> processdefinitions { get; set; }
        public virtual DbSet<processdetail> processdetails { get; set; }
        public virtual DbSet<processfeedback> processfeedbacks { get; set; }
        public virtual DbSet<psf_qt_history> psf_qt_history { get; set; }
        public virtual DbSet<psfassignedinteraction> psfassignedinteractions { get; set; }
        public virtual DbSet<psfcallhistorycube> psfcallhistorycubes { get; set; }
        public virtual DbSet<psfinteraction> psfinteractions { get; set; }
        public virtual DbSet<renewaltype> renewaltypes { get; set; }
        public virtual DbSet<repair_order_report> repair_order_report { get; set; }
        public virtual DbSet<rework> reworks { get; set; }
        public virtual DbSet<robillscube> robillscubes { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<saleregisterexcelerror> saleregisterexcelerrors { get; set; }
        public virtual DbSet<savedsearchresult> savedsearchresults { get; set; }
        public virtual DbSet<segment> segments { get; set; }
        public virtual DbSet<service> services { get; set; }
        public virtual DbSet<serviceadvisor> serviceadvisors { get; set; }
        public virtual DbSet<serviceadvisorinteraction> serviceadvisorinteractions { get; set; }
        public virtual DbSet<serviceadvisorinteractionstatu> serviceadvisorinteractionstatus { get; set; }
        public virtual DbSet<servicebooked> servicebookeds { get; set; }
        public virtual DbSet<servicebookedsynced> servicebookedsynceds { get; set; }
        public virtual DbSet<servicedoneschcall> servicedoneschcalls { get; set; }
        public virtual DbSet<servicess> servicesses { get; set; }
        public virtual DbSet<servicetype> servicetypes { get; set; }
        public virtual DbSet<sfotemp> sfotemps { get; set; }
        public virtual DbSet<showroom> showrooms { get; set; }
        public virtual DbSet<smrautoassignmentsummary> smrautoassignmentsummaries { get; set; }
        public virtual DbSet<smrdataerror> smrdataerrors { get; set; }
        public virtual DbSet<smrforecastassignmenthistory> smrforecastassignmenthistories { get; set; }
        public virtual DbSet<sms_upload> sms_upload { get; set; }
        public virtual DbSet<smsinteraction> smsinteractions { get; set; }
        public virtual DbSet<smsparameter> smsparameters { get; set; }
        public virtual DbSet<smsstatu> smsstatus { get; set; }
        public virtual DbSet<smstemplate> smstemplates { get; set; }
        public virtual DbSet<specialoffermaster> specialoffermasters { get; set; }
        public virtual DbSet<srdisposition> srdispositions { get; set; }
        public virtual DbSet<stakeholdemail> stakeholdemails { get; set; }
        public virtual DbSet<storeobjtoblob> storeobjtoblobs { get; set; }
        public virtual DbSet<synchedkey> synchedkeys { get; set; }
        public virtual DbSet<synchedkeyschcall> synchedkeyschcalls { get; set; }
        public virtual DbSet<taggedassignment> taggedassignments { get; set; }
        public virtual DbSet<tagginguser> taggingusers { get; set; }
        public virtual DbSet<tenant> tenants { get; set; }
        public virtual DbSet<testformcreation> testformcreations { get; set; }
        public virtual DbSet<testscore> testscores { get; set; }
        public virtual DbSet<tl_day_wise_data> tl_day_wise_data { get; set; }
        public virtual DbSet<tl_monthwise_data> tl_monthwise_data { get; set; }
        public virtual DbSet<unavailability> unavailabilities { get; set; }
        public virtual DbSet<uniqueidforcall> uniqueidforcalls { get; set; }
        public virtual DbSet<upload> uploads { get; set; }
        public virtual DbSet<uploaddata> uploaddatas { get; set; }
        public virtual DbSet<uploaddetail> uploaddetails { get; set; }
        public virtual DbSet<uploadmasterformat> uploadmasterformats { get; set; }
        public virtual DbSet<uploadreportfile> uploadreportfiles { get; set; }
        public virtual DbSet<uploadreportname> uploadreportnames { get; set; }
        public virtual DbSet<uploadsummaryuserwise> uploadsummaryuserwises { get; set; }
        public virtual DbSet<uploadtype> uploadtypes { get; set; }
        public virtual DbSet<uploadtype_columndefinition> uploadtype_columndefinition { get; set; }
        public virtual DbSet<upselllead> upsellleads { get; set; }
        public virtual DbSet<usercreation> usercreations { get; set; }
        public virtual DbSet<userlocation> userlocations { get; set; }
        public virtual DbSet<userrole> userroles { get; set; }
        public virtual DbSet<usersession> usersessions { get; set; }
        public virtual DbSet<userworkshop> userworkshops { get; set; }
        public virtual DbSet<vehicle> vehicles { get; set; }
        public virtual DbSet<vehiclesummarycube> vehiclesummarycubes { get; set; }
        public virtual DbSet<workbillregistererror> workbillregistererrors { get; set; }
        public virtual DbSet<workshop> workshops { get; set; }
        public virtual DbSet<workshopsummary> workshopsummaries { get; set; }
        public virtual DbSet<wyzuser> wyzusers { get; set; }
        public virtual DbSet<userfieldwalkinlocation> userfieldwalkinlocations { get; set; }
        //public virtual DbSet<insurancecompanies> InsurancecompaniesList { get; set; } insuranceCompany is -> insuranceCompanies
        public virtual DbSet<soldNewCustomerVehicles> SoldNewCustomerVehicles { get; set; }
        public virtual DbSet<callsyncdata> callSyncDatas { get; set; }

        public virtual DbSet<smrforecasteddata> smrforecasteddatas { get; set; }

        public virtual DbSet<coupons> Coupons { get; set; }
        public virtual DbSet<IndusPSFInteraction> indusPSFInteraction { get; set; }
        
        public virtual DbSet<policyrenewallist> Policyrenewallists { get; set; }
        public virtual DbSet<salesreport> Salesreports { get; set; }
        public virtual DbSet<servicereportinformationbp> Servicereportinformationbps { get; set; }
        public virtual DbSet<servicereportinformationgs> Servicereportinformationgs { get; set; }
        public virtual DbSet<uio_report> Uio_Reports { get; set; }
        public virtual DbSet<escalationusermapping> Escalationusermappings { get; set; }
        public virtual DbSet<fieldexecutivefirebaseupdation> Fieldexecutivefirebaseupdations { get; set; }

        public virtual DbSet<psfquestions> psfquestions { get; set; }

        public virtual DbSet<modelcategory> Modelcategories { get; set; }
        public virtual DbSet<dataremoval> Dataremovals { get; set; }
        public virtual DbSet<formmapping> form_mapping { get; set; }


        public virtual DbSet<CompInteraction> compInteractions { get; set; }

        public virtual DbSet<RMInteraction> rmInteractions { get; set; }
        public virtual DbSet<saservicetypes> Saservicetypes { get; set; }

        /******************** Kalyani File Upload Models Aded**********************/

        public virtual DbSet<userlogs> userlogs { get; set; }
        public virtual DbSet<accessLevel> AccessLevels { get; set; }

        public virtual DbSet<gsmcallsynchdetails> gsmcallsynch { get; set; }
        public virtual DbSet<gsmsynchdata> gsmsynchdata { get; set; }
        public virtual DbSet<changeAssignmentPending> ChangeAssignmentPendings { get; set; }
       
        public virtual DbSet<navigationaccess> navaccess { get; set; }
        public virtual DbSet<sipregistrationid> Sipregistrationids { get; set; }


        public virtual DbSet<smscrondata> Smscrondatas { get; set; }
        public virtual DbSet<fefirebasehistorydata> feFirebaseHistoryData { get; set; }
        public virtual DbSet<whatsappParams> WhatsappParams { get; set; }
        public virtual DbSet<servicePush> servicePush { get; set; }
        public virtual DbSet<inspolicydrop> insPolicyDrop { get; set; }

        public virtual DbSet<schedulers> schedulers { get; set; }
        public virtual DbSet<searchandassignaccess> searchandassignaccesses { get; set; }
        public virtual DbSet<eventstaus> eventStaus { get; set; }
        public virtual DbSet<FETracking> feTracking { get; set; }
        public virtual DbSet<EmailSender> emailSender { get; set; }
        public virtual DbSet<EmailReceipient> emailReceipient { get; set; }
        public virtual DbSet<EventEmailInteraction> eventEmailInteraction { get; set; }
        public virtual DbSet<rosaassignment> rosaAssignments { get; set; }
        public virtual DbSet<sainteraction> saInteractions { get; set; }
        public virtual DbSet<roreasons> roReasons { get; set; }
        public virtual DbSet<sareferrals> saReferrals { get; set; }
        public virtual DbSet<variants> Variants { get; set; }
        public virtual DbSet<rsacoverage> Rsacoverages { get; set; }
        public virtual DbSet<ewcoverage> Ewcoverages { get; set; }
        //AKASH
        public virtual DbSet<pmslabour> Pmslabours { get; set; }
        public virtual DbSet<pmscity> Pmscity { get; set; }
        public virtual DbSet<pmsmileage> Pmsmileages { get; set; }
        public virtual DbSet<pmsmodel> Pmsmodels { get; set; }

        public virtual DbSet<psfassignmentrecords> Psfassignmentrecords { get; set; }

        public virtual DbSet<driverscheduler> driverSchedulers { get; set; }

        public virtual DbSet<driverbookingdatetime> driverbookingdatetimes { get; set; }

        public virtual DbSet<DriverBookingDetails> driverBookingDetails { get; set; }

        public virtual DbSet<DriverAppInteraction> driverAppInteraction { get; set; }

        public virtual DbSet<DriverAppFileDetails> driverAppFileDetails { get; set; }

        public virtual DbSet<DriversDeliveryNotes> driversDeliveryNotes { get; set; }
        public virtual DbSet<whatsapporaiparams> Whatsapporaiparams { get; set; }
        public virtual DbSet<postsalesdisposition> Postsalesdispositions { get; set; }
        public virtual DbSet<postsalesassignedinteraction> Postsalesassignedinteractions { get; set; }
        public virtual DbSet<postsalescallinteraction> Postsalescallinteractions { get; set; }
        public virtual DbSet<postsalescompinteraction> Postsalescompinteractions { get; set; }
        public virtual DbSet<postsalescallhistory> Postsalescallhistories { get; set; }
        public virtual DbSet<postsalesfollowupquestions> Postsalesfollowupquestions { get; set; }
        public virtual DbSet <followupReason> FollowupReasons { get; set; }
        public virtual DbSet <postsalescomplaintcre> Postsalescomplaintcres { get; set; }
        public virtual DbSet <useruploads> useruploads { get; set; }
        public virtual DbSet <naudabaoCredintials> NaudabaoCredintials { get; set; }
        public virtual DbSet <masterservicedetails> Masterservicedetails { get; set; }
        public virtual DbSet <induspostservicefollowupquestions> Induspostservicefollowupquestions { get; set; }
        public virtual DbSet<infobipcredentials> Infobipcredentials { get; set; }
        public virtual DbSet<couponinteraction> Couponinteractions { get; set; }
        public virtual DbSet<coupon_details> Coupon_Details { get; set; }
        public virtual DbSet<couponhistory> couponhistory { get; set; }
        public virtual DbSet<tatateleservicescallrecords> Tatateleservicescallrecords { get; set; }
        public virtual DbSet<knowlaritycredintials> Knowlaritycredintials { get; set; }
        public virtual DbSet<salesworkshop> Salesworkshops { get; set; }
        public virtual DbSet<tatateleservicescredintials> Tatateleservicescredintials { get; set; }
        public virtual DbSet<knowlaritycallrecords> Knowlaritycallrecords { get; set; }
        public virtual DbSet<couponredemptionotp> Couponredemptionotps { get; set; }
        public virtual DbSet<servicehistory> Servicehistories { get; set; }
        public virtual DbSet<individualreportsmscrondata> Individualreportsmscrondatas { get; set; }
        public virtual DbSet<assignedcallsreport_stage1> Assignedcallsreport_Stages { get; set; }
        public virtual DbSet<psfcallhistorycube_stage1> Psfcallhistorycube_Stages { get; set; }
        public virtual DbSet<insurancecallhistorycube_stage1> Insurancecallhistorycube_Stages { get; set; }
        public virtual DbSet<postsalescallhistory_stage1> Postsalescallhistory_Stages { get; set; }
        public virtual DbSet<callhistorycube_stage1> Callhistorycube_Stages { get; set; }
        public virtual DbSet<payUTransaction> PayUTransactions { get; set; }
        public virtual DbSet<marutistdremarks> GetMarutistdremarks { get; set; }
        public virtual DbSet<calldispotitionautotemplates> calldispotitionautotemplates { get; set; }
        public virtual DbSet<psfescalationemails> psfescalationemails { get; set; }
        public virtual DbSet<psfservicetypes> psfservicetypes { get; set; }
        public virtual DbSet<whatsupscrondata> whatsupscrondata { get; set; }
        public virtual DbSet<authenticationcredentials> authenticationcredentials { get; set; }
        public virtual DbSet<afterpaymenttransaction> afterpaymenttransaction { get; set; }
        public virtual DbSet<businesswhatsappdetails> businesswhatsappdetails { get; set; }
        //loginhistory
        public virtual DbSet<login_history> loginhistory { get; set; }
        public virtual DbSet<CREUserAdditionalFieldsMapper> CREUserAdditionalFieldsMapper { get; set; }
        public virtual DbSet<ROAge> ROAge { get; set; }
        public virtual DbSet<CRECategory> CRECategory { get; set; }
        public virtual DbSet<CREFieldOne> CREFieldOne { get; set; }
        public virtual DbSet<CREFieldTwo> CREFieldTwo { get; set; }

        public virtual DbSet<ExcludeNegativeDiposition> ExcludeNegativeDiposition { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //**************** For Indus *********************************
            modelBuilder.Entity<calldispositiondata>()
                .HasMany(e => e.induspsfinteractions)
                .WithOptional(e => e.calldispositiondata)
                .HasForeignKey(e => e.callDispositionData_id);

            modelBuilder.Entity<callinteraction>()
                .HasMany(e => e.induspsfinteractions)
                .WithOptional(e => e.callinteraction)
                .HasForeignKey(e => e.callInteraction_id);

            modelBuilder.Entity<IndusPSFInteraction>()
               .HasMany(e => e.upsellleads)
               .WithOptional(e => e.induspsfinteraction)
               .HasForeignKey(e => e.induspsfinteraction_Id);

            //********************* End **********************************


            modelBuilder.Entity<callsyncdata>()
                .Property(e => e.callType)
                .IsUnicode(false);

            modelBuilder.Entity<callsyncdata>()
                .Property(e => e.dailedNumber)
                .IsUnicode(false);

            modelBuilder.Entity<callsyncdata>()
                .Property(e => e.filepath)
                .IsUnicode(false);

            modelBuilder.Entity<callsyncdata>()
                .Property(e => e.ringTime)
                .IsUnicode(false);

            modelBuilder.Entity<callsyncdata>()
                .Property(e => e.uniqueidForCallSync)
                .IsUnicode(false);

            modelBuilder.Entity<addoncover>()
                .Property(e => e.coverName)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.addressLine1)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.addressLine2)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.addressLine3)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.concatenatedAdress)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.country)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.state)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.district)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.tehsil)
                .IsUnicode(false);

            modelBuilder.Entity<agency>()
                .Property(e => e.agencyAddress)
                .IsUnicode(false);

            modelBuilder.Entity<agency>()
                .Property(e => e.agencyName)
                .IsUnicode(false);

            modelBuilder.Entity<agent_day_wise_data>()
                .Property(e => e.aspect_id)
                .IsUnicode(false);

            modelBuilder.Entity<agent_day_wise_data>()
                .Property(e => e.assistant_manager)
                .IsUnicode(false);

            modelBuilder.Entity<agent_day_wise_data>()
                .Property(e => e.attendance)
                .IsUnicode(false);

            modelBuilder.Entity<agent_day_wise_data>()
                .Property(e => e.designation)
                .IsUnicode(false);

            modelBuilder.Entity<agent_day_wise_data>()
                .Property(e => e.employee_id)
                .IsUnicode(false);

            modelBuilder.Entity<agent_day_wise_data>()
                .Property(e => e.employee_name)
                .IsUnicode(false);

            modelBuilder.Entity<agent_day_wise_data>()
                .Property(e => e.full_time_or_part_time)
                .IsUnicode(false);

            modelBuilder.Entity<agent_day_wise_data>()
                .Property(e => e.location)
                .IsUnicode(false);

            modelBuilder.Entity<agent_day_wise_data>()
                .Property(e => e.process)
                .IsUnicode(false);

            modelBuilder.Entity<agent_day_wise_data>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<agent_day_wise_data>()
                .Property(e => e.team_leader)
                .IsUnicode(false);

            modelBuilder.Entity<agent_day_wise_data>()
                .Property(e => e.tenure)
                .IsUnicode(false);

            modelBuilder.Entity<agent_day_wise_data>()
                .Property(e => e.week_bucket)
                .IsUnicode(false);

            modelBuilder.Entity<agent_day_wise_data>()
                .Property(e => e.zt_ids)
                .IsUnicode(false);

            modelBuilder.Entity<agent_day_wise_data>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<agent_day_wise_data>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<agent_day_wise_data>()
                .Property(e => e.process_name)
                .IsUnicode(false);

            modelBuilder.Entity<agent_roaster>()
                .Property(e => e.agent_name)
                .IsUnicode(false);

            modelBuilder.Entity<agent_roaster>()
                .Property(e => e.assistant_manager)
                .IsUnicode(false);

            modelBuilder.Entity<agent_roaster>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<agent_roaster>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<agent_roaster>()
                .Property(e => e.team_leader)
                .IsUnicode(false);

            modelBuilder.Entity<agent_wise_mtd>()
                .Property(e => e.Aspect_ID)
                .IsUnicode(false);

            modelBuilder.Entity<agent_wise_mtd>()
                .Property(e => e.Average_Login_Hrs)
                .IsUnicode(false);

            modelBuilder.Entity<agent_wise_mtd>()
                .Property(e => e.Employee_ID)
                .IsUnicode(false);

            modelBuilder.Entity<agent_wise_mtd>()
                .Property(e => e.Employee_Name)
                .IsUnicode(false);

            modelBuilder.Entity<agentdata>()
                .Property(e => e.Agent_name)
                .IsUnicode(false);

            modelBuilder.Entity<agentdata>()
                .Property(e => e.Aspect_id)
                .IsUnicode(false);

            modelBuilder.Entity<agentdata>()
                .Property(e => e.assistant_manager)
                .IsUnicode(false);

            modelBuilder.Entity<agentdata>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<agentdata>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<agentdata>()
                .Property(e => e.process_name)
                .IsUnicode(false);

            modelBuilder.Entity<agentdata>()
                .Property(e => e.team_leader)
                .IsUnicode(false);

            modelBuilder.Entity<agentdatablob>()
                .Property(e => e.agentId)
                .IsUnicode(false);

            modelBuilder.Entity<agentdatablob>()
                .Property(e => e.columnName)
                .IsUnicode(false);

            modelBuilder.Entity<agentdatablob>()
                .Property(e => e.createdBy)
                .IsUnicode(false);

            modelBuilder.Entity<agentdatablob>()
                .Property(e => e.modifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<agentdigicalldata>()
                .Property(e => e.Aspect_ID)
                .IsUnicode(false);

            modelBuilder.Entity<agentdigicalldata>()
                .Property(e => e.Assistant_Manager)
                .IsUnicode(false);

            modelBuilder.Entity<agentdigicalldata>()
                .Property(e => e.Attendance)
                .IsUnicode(false);

            modelBuilder.Entity<agentdigicalldata>()
                .Property(e => e.Emp_Id)
                .IsUnicode(false);

            modelBuilder.Entity<agentdigicalldata>()
                .Property(e => e.Employee_Name)
                .IsUnicode(false);

            modelBuilder.Entity<agentdigicalldata>()
                .Property(e => e.FT_PT)
                .IsUnicode(false);

            modelBuilder.Entity<agentdigicalldata>()
                .Property(e => e.LOB)
                .IsUnicode(false);

            modelBuilder.Entity<agentdigicalldata>()
                .Property(e => e.Man_Daysfor_Login_Hrs)
                .IsUnicode(false);

            modelBuilder.Entity<agentdigicalldata>()
                .Property(e => e.Team_Leader)
                .IsUnicode(false);

            modelBuilder.Entity<agentdigicalldata>()
                .Property(e => e.Temp_ID)
                .IsUnicode(false);

            modelBuilder.Entity<agentdigicalldata>()
                .Property(e => e.Tenure_Bucket)
                .IsUnicode(false);

            modelBuilder.Entity<agentdigicalldata>()
                .Property(e => e.ZT_ID)
                .IsUnicode(false);

            modelBuilder.Entity<agentdigicalldata>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<agentdigicalldata>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<agentdigicalldata>()
                .Property(e => e.process_name)
                .IsUnicode(false);

            modelBuilder.Entity<agentdigicalldata>()
                .Property(e => e.uploadTable_name)
                .IsUnicode(false);

            modelBuilder.Entity<appointmentbooked>()
                .Property(e => e.addressOfVisit)
                .IsUnicode(false);

            modelBuilder.Entity<appointmentbooked>()
                .Property(e => e.appointeeName)
                .IsUnicode(false);

            modelBuilder.Entity<appointmentbooked>()
                .Property(e => e.appointmentFromTime)
                .IsUnicode(false);

            modelBuilder.Entity<appointmentbooked>()
                .Property(e => e.appointmentToTime)
                .IsUnicode(false);

            modelBuilder.Entity<appointmentbooked>()
                .Property(e => e.dsa)
                .IsUnicode(false);

            modelBuilder.Entity<appointmentbooked>()
                .Property(e => e.insuranceAgentData)
                .IsUnicode(false);

            modelBuilder.Entity<appointmentbooked>()
                .Property(e => e.insuranceCompany)
                .IsUnicode(false);

            modelBuilder.Entity<appointmentbooked>()
                .Property(e => e.nomineeAge)
                .IsUnicode(false);

            modelBuilder.Entity<appointmentbooked>()
                .Property(e => e.nomineeName)
                .IsUnicode(false);

            modelBuilder.Entity<appointmentbooked>()
                .Property(e => e.nomineeRelationWithOwner)
                .IsUnicode(false);

            modelBuilder.Entity<appointmentbooked>()
                .Property(e => e.renewalMode)
                .IsUnicode(false);

            modelBuilder.Entity<appointmentbooked>()
                .Property(e => e.renewalType)
                .IsUnicode(false);

            modelBuilder.Entity<appointmentbooked>()
                .Property(e => e.typeOfPickup)
                .IsUnicode(false);

            modelBuilder.Entity<appointmentbooked>()
                .Property(e => e.PremiumYes)
                .IsUnicode(false);

            modelBuilder.Entity<appointmentbooked>()
                .Property(e => e.closed_insurance_id)
                .IsUnicode(false);

            modelBuilder.Entity<appointmentbooked>()
                .Property(e => e.purpose)
                .IsUnicode(false);

            modelBuilder.Entity<appointmentbooked>()
                .Property(e => e.discountValue)
                .IsUnicode(false);

            modelBuilder.Entity<appointmentbooked>()
                .HasMany(e => e.callinteractions)
                .WithOptional(e => e.appointmentbooked)
                .HasForeignKey(e => e.appointmentBooked_appointmentId);

            modelBuilder.Entity<assignedcallsreport>()
                .Property(e => e.assignmentType)
                .IsUnicode(false);

            modelBuilder.Entity<assignedcallsreport>()
                .Property(e => e.uploadId)
                .IsUnicode(false);

            modelBuilder.Entity<assignedinteraction>()
                .Property(e => e.callMade)
                .IsUnicode(false);

            modelBuilder.Entity<assignedinteraction>()
                .Property(e => e.interactionType)
                .IsUnicode(false);

            modelBuilder.Entity<assignedinteraction>()
                .Property(e => e.lastDisposition)
                .IsUnicode(false);

            modelBuilder.Entity<assignedinteraction>()
                .Property(e => e.nextServiceType)
                .IsUnicode(false);

            modelBuilder.Entity<assignedinteraction>()
                .HasMany(e => e.callinteractions)
                .WithOptional(e => e.assignedinteraction)
                .HasForeignKey(e => e.assignedInteraction_id);

            modelBuilder.Entity<assignmentreport_nthday>()
                .Property(e => e.booking_status)
                .IsUnicode(false);

            modelBuilder.Entity<authorizedip>()
                .Property(e => e.ip_address)
                .IsUnicode(false);

            modelBuilder.Entity<automessageparameter>()
                .Property(e => e.modeltable)
                .IsUnicode(false);

            modelBuilder.Entity<automessageparameter>()
                .Property(e => e.query)
                .IsUnicode(false);

            modelBuilder.Entity<automessageparameter>()
                .Property(e => e.parameter)
                .IsUnicode(false);

            modelBuilder.Entity<autosmstemplate>()
                .Property(e => e.smsAPI)
                .IsUnicode(false);

            modelBuilder.Entity<autosmstemplate>()
                .Property(e => e.smsTemplate)
                .IsUnicode(false);

            modelBuilder.Entity<autosmstemplate>()
                .Property(e => e.smsType)
                .IsUnicode(false);

            modelBuilder.Entity<availability>()
                .Property(e => e.year)
                .IsUnicode(false);

            modelBuilder.Entity<axisdetail>()
                .Property(e => e.aggreagation)
                .IsUnicode(false);

            modelBuilder.Entity<axisdetail>()
                .Property(e => e.chartName)
                .IsUnicode(false);

            modelBuilder.Entity<axisdetail>()
                .Property(e => e.chartType)
                .IsUnicode(false);

            modelBuilder.Entity<axisdetail>()
                .Property(e => e.xaxisFieldName)
                .IsUnicode(false);

            modelBuilder.Entity<axisdetail>()
                .Property(e => e.xaxisLabelName)
                .IsUnicode(false);

            modelBuilder.Entity<axisdetail>()
                .Property(e => e.yaxisLabelName)
                .IsUnicode(false);

            modelBuilder.Entity<axisdetail>()
                .Property(e => e.yaxisName)
                .IsUnicode(false);

            modelBuilder.Entity<bay>()
                .Property(e => e.workshopName)
                .IsUnicode(false);

            modelBuilder.Entity<bookingdatetime>()
                .Property(e => e.timeRange)
                .IsUnicode(false);

            modelBuilder.Entity<callassignmentstatu>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<callassignmentstatu>()
                .HasMany(e => e.assignedinteractions)
                .WithOptional(e => e.callassignmentstatu)
                .HasForeignKey(e => e.assignmentStatus_id);

            modelBuilder.Entity<calldispositiondata>()
                .Property(e => e.disposition)
                .IsUnicode(false);

            modelBuilder.Entity<calldispositiondata>()
                .HasMany(e => e.appointmentbookeds)
                .WithOptional(e => e.calldispositiondata)
                .HasForeignKey(e => e.insuranceBookStatus_id);

            modelBuilder.Entity<calldispositiondata>()
                .HasMany(e => e.assignedinteractions)
                .WithOptional(e => e.calldispositiondata)
                .HasForeignKey(e => e.finalDisposition_id);

            modelBuilder.Entity<calldispositiondata>()
                .HasMany(e => e.srdispositions)
                .WithOptional(e => e.calldispositiondata)
                .HasForeignKey(e => e.callDispositionData_id);

            modelBuilder.Entity<calldispositiondata>()
                .HasMany(e => e.psfinteractions)
                .WithOptional(e => e.calldispositiondata)
                .HasForeignKey(e => e.callDispositionData_id);

            modelBuilder.Entity<calldispositiondata>()
                .HasMany(e => e.psfassignedinteractions)
                .WithOptional(e => e.calldispositiondata)
                .HasForeignKey(e => e.finalDisposition_id);

            modelBuilder.Entity<calldispositiondata>()
                .HasMany(e => e.insurancedispositions)
                .WithOptional(e => e.calldispositiondata)
                .HasForeignKey(e => e.callDispositionData_id);

            modelBuilder.Entity<calldispositiondata>()
                .HasMany(e => e.servicebookeds)
                .WithOptional(e => e.calldispositiondata)
                .HasForeignKey(e => e.serviceBookStatus_id);

            modelBuilder.Entity<calldispositiondata>()
                .HasMany(e => e.insuranceassignedinteractions)
                .WithOptional(e => e.calldispositiondata)
                .HasForeignKey(e => e.finalDisposition_id);

            modelBuilder.Entity<calldispositiondata>()
                .HasMany(e => e.servicebookeds1)
                .WithOptional(e => e.calldispositiondata1)
                .HasForeignKey(e => e.serviceBookStatus_id);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.Cre_Name)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.Customer_name)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.DOb)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.Email_id)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.Model)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.Reason)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.Tertiary_disposition)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.Variant)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.Veh_Reg_no)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.booking_status)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.callDuration)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.callTime)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.callType)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.calling_data_type)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.chassisNo)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.customerRemarks)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.customer_category)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.customer_city)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.customer_remarks)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.driver)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.followUpTime)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.forecastLogic)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.fromtimeofpick)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.fueltype)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.isCallinitaited)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.lastServiceType)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.nextServicetype)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.nonContactsReason)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.office_address)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.permenant_address)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.preffered_Contact_number)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.previous_disposition)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.primary_disposition)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.psf_status)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.residential_address)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.ringtime)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.ronumber)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.scheduledDateTime)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.secondary_dispostion)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.serviceType)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.service_advisor)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.typeofpickup)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.upselltype)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.upto)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.cremanager)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.lastsaname)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.currentMileage)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.expectedVisitDate)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.pickUpAddress)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.Booked_workshop)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.SpecielofferValidity)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.SpecielofferDetails)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.SpecielofferCode)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.SpecielofferBenefit)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.SpecielofferConditions)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.BookingStatus)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.Reshedule_Status)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.Is_Feedback_orCompliant)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.Complaint_For_Dept)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.Compliant_Remark)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.noServiceReason)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.subdispositoinForSNR)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.AlreadyServicedateOfService)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.verifiedWithDMS)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.authorisedOrNot)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.alreadyServicedType)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.mileageAsOnDate)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.mileageAtService)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.alredyServicedDealerName)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.transferedCity)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.pinCodeOfCity)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.inBoundLeadSource)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.filepath)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.dailedNumber)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.makeCallFrom)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.isChaserCall)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.assigned_wyzuser)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.assigned_crename)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.reported)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.reported_date)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.reported_condition)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.reported_workshop)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.reported_serviceType)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.complaintTaggedTo)
                .IsUnicode(false);

            modelBuilder.Entity<callhistorycube>()
                .Property(e => e.tagging_name)
                .IsUnicode(false);

            modelBuilder.Entity<callinteraction>()
                .Property(e => e.agentName)
                .IsUnicode(false);

            modelBuilder.Entity<callinteraction>()
                .Property(e => e.callDate)
                .IsUnicode(false);

            modelBuilder.Entity<callinteraction>()
                .Property(e => e.callDuration)
                .IsUnicode(false);

            modelBuilder.Entity<callinteraction>()
                .Property(e => e.callTime)
                .IsUnicode(false);

            modelBuilder.Entity<callinteraction>()
                .Property(e => e.callType)
                .IsUnicode(false);

            modelBuilder.Entity<callinteraction>()
                .Property(e => e.dealerCode)
                .IsUnicode(false);

            modelBuilder.Entity<callinteraction>()
                .Property(e => e.filePath)
                .IsUnicode(false);

            modelBuilder.Entity<callinteraction>()
                .Property(e => e.firebaseKey)
                .IsUnicode(false);

            modelBuilder.Entity<callinteraction>()
                .Property(e => e.isCallinitaited)
                .IsUnicode(false);

            modelBuilder.Entity<callinteraction>()
                .Property(e => e.latitude)
                .IsUnicode(false);

            modelBuilder.Entity<callinteraction>()
                .Property(e => e.longitude)
                .IsUnicode(false);

            modelBuilder.Entity<callinteraction>()
                .Property(e => e.makeCallFrom)
                .IsUnicode(false);

            modelBuilder.Entity<callinteraction>()
                .Property(e => e.mediaFile)
                .IsUnicode(false);

            modelBuilder.Entity<callinteraction>()
                .Property(e => e.serviceAdvisorfirebaseKey)
                .IsUnicode(false);

            modelBuilder.Entity<callinteraction>()
                .Property(e => e.synchedToFirebase)
                .IsUnicode(false);

            modelBuilder.Entity<callinteraction>()
                .Property(e => e.dailedNoIs)
                .IsUnicode(false);

            modelBuilder.Entity<callinteraction>()
                .Property(e => e.ringTime)
                .IsUnicode(false);

            modelBuilder.Entity<callinteraction>()
                .Property(e => e.ronumber)
                .IsUnicode(false);

            modelBuilder.Entity<callinteraction>()
                .Property(e => e.uniqueAndroidId)
                .IsUnicode(false);

            modelBuilder.Entity<callinteraction>()
                .Property(e => e.fileSize)
                .IsUnicode(false);

            modelBuilder.Entity<callinteraction>()
                .Property(e => e.tagging_name)
                .IsUnicode(false);

            modelBuilder.Entity<callinteraction>()
                .HasMany(e => e.psfinteractions)
                .WithOptional(e => e.callinteraction)
                .HasForeignKey(e => e.callInteraction_id);

            modelBuilder.Entity<callinteraction>()
                .HasMany(e => e.complaints)
                .WithOptional(e => e.callinteraction)
                .HasForeignKey(e => e.callInteraction_id);

            modelBuilder.Entity<callinteraction>()
                .HasMany(e => e.srdispositions)
                .WithOptional(e => e.callinteraction)
                .HasForeignKey(e => e.callInteraction_id);

            modelBuilder.Entity<callinteraction>()
                .HasMany(e => e.insurancedispositions)
                .WithOptional(e => e.callinteraction)
                .HasForeignKey(e => e.callInteraction_id);

            modelBuilder.Entity<callinteraction>()
                .HasMany(e => e.servicebookeds)
                .WithOptional(e => e.callinteraction)
                .HasForeignKey(e => e.callInteraction_id);

            modelBuilder.Entity<campaign>()
                .Property(e => e.campaignName)
                .IsUnicode(false);

            modelBuilder.Entity<campaign>()
                .Property(e => e.campaignType)
                .IsUnicode(false);

            modelBuilder.Entity<campaign>()
                .Property(e => e.campaignTypeIdValue)
                .IsUnicode(false);

            modelBuilder.Entity<campaign>()
                .Property(e => e.customizedName)
                .IsUnicode(false);

            modelBuilder.Entity<campaign>()
                .HasMany(e => e.assignedinteractions)
                .WithOptional(e => e.campaign)
                .HasForeignKey(e => e.campaign_id);

            modelBuilder.Entity<campaign>()
                .HasMany(e => e.callinteractions)
                .WithOptional(e => e.campaign)
                .HasForeignKey(e => e.campaign_id);

            modelBuilder.Entity<campaign>()
                .HasMany(e => e.insuranceassignedinteractions)
                .WithOptional(e => e.campaign)
                .HasForeignKey(e => e.campaign_id);

            modelBuilder.Entity<campaign>()
                .HasMany(e => e.complaints)
                .WithOptional(e => e.campaign)
                .HasForeignKey(e => e.campaign_id);

            modelBuilder.Entity<campaign>()
                .HasMany(e => e.workshops)
                .WithOptional(e => e.campaign)
                .HasForeignKey(e => e.campaign_id);

            modelBuilder.Entity<campaign>()
                .HasMany(e => e.psfassignedinteractions)
                .WithOptional(e => e.campaign)
                .HasForeignKey(e => e.campaign_id);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.address1)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.address2)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.address3)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.chassis)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.colorDesc)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.customerName)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.dataUploadLocation)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.email_id)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.endDate)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.engine)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.errorInformation)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.land_line)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.mobile1)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.mobile2)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.pincode)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.salutation)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.source)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.startDate)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.std_code)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.varaintDesc)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.veh_reg_no)
                .IsUnicode(false);

            modelBuilder.Entity<campaignexcelerror>()
                .Property(e => e.vehicle_types)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.allocated_by)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.allocated_to)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.applicantAddress)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.applicantId)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.applicantName)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.applicantType)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.bankName)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.category)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.companyName)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.department)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.designation)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.dob)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.dsaName)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.fI_Date)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.fI_Flag)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.fI_Time)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.fI_ToBeConducted)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.loanAmount)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.location)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.mobileNumber)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.officeAddress)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.officeNumber)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.office_pincode)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.permanentAddress)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.permanentNumber)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.productName)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.productTypeCustomer)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.residenceAddress)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.residenceNumber)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.residence_pincode)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.specialInstruction)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.subCategory)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.tenure)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.verificationId)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .Property(e => e.verificationType)
                .IsUnicode(false);

            modelBuilder.Entity<caselist>()
                .HasMany(e => e.casesummaries)
                .WithOptional(e => e.caselist)
                .HasForeignKey(e => e.case_Id);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.addChangeDetails)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.additionalRemarks)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.additionalRemarksBusiness)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.addressConfirmed)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.addressTraceble)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.allocated_to)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.antiSocial)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.antiSocialBusiness)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.approxAreaofoffice)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.areaOfHouseInYards)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.businessBoard)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.businessBoard_others)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.checkedAtLocalityRadd)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.checkedAtLocalityRadd_Others)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.checkedatLocalityBus)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.confimedByWhom)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.confirmation)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.confirmedByWhomBus)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.designationApplicant)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.designationPersonmet)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.doorLockedOrNot)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.durationOfStayCurrentAddress)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.easeOfLocation)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.easeoflocatingOffice)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.educationQualification)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.educationQualification_Others)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.familyType)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.howCOperitiveCustomerIs)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.isAddressOfPersonTraced)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.isAppicatedStayInHome)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.isHouseLocked)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.lastMileAcheived)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.lastmileachievedBus)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.livingStandardOfApplicant)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.localityOfResidence)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.localityOfResidence_Others)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.nameOfContactPerson)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.natureOfBusiness)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.natureOfBusiness_others)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.neighbour1)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.neighbour1DetailsIfNeg)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.neighbour1FeedBack)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.neighbour2)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.neighbour2DetailsIfNeg)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.neighbour2FeedBack)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.noOfEmployeesighted)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.noofEmployeesWorking)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.numOfyearsInPresent)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.officeOwnership)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.officeOwnership_others)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.otherCheckedatLocalityBus)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.otherEarningMembers)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.othersWasCustomercontactableBus)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.personMet)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.personalIdSeen)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.personalIdSeen_Others)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.police)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.policeBusiness)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.politicalParty)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.politicalPartyBusiness)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.previousAddressDetails)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.productDetails)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.realtionShipWithApplicant)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.realtionShipWithApplicant_Others)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.remarks)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.remarksBus)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.residentialStatus)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.residentialStatus_Others)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.signature)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.typeOfBusiness)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.typeOfBusiness_others)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.typeOfOffice)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.typeOfOffice_others)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.typeOfResidence)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.userSignBusiness)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.wasCustomerContactable)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.wasCustomerContactable_Others)
                .IsUnicode(false);

            modelBuilder.Entity<casesummary>()
                .Property(e => e.wasCustomercontactableBus)
                .IsUnicode(false);

            modelBuilder.Entity<change_assignment_records>()
                .Property(e => e.updatedType)
                .IsUnicode(false);

            modelBuilder.Entity<chartdata>()
                .Property(e => e.chart_name)
                .IsUnicode(false);

            modelBuilder.Entity<chartdata>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<chartdata>()
                .Property(e => e.dashboard_name)
                .IsUnicode(false);

            modelBuilder.Entity<chartdata>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<chartdata>()
                .Property(e => e.process_name)
                .IsUnicode(false);

            modelBuilder.Entity<checkupdatecount>()
                .Property(e => e.checkedBy)
                .IsUnicode(false);

            modelBuilder.Entity<checkupdatecount>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<citystate>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<citystate>()
                .Property(e => e.state)
                .IsUnicode(false);

            modelBuilder.Entity<citystate>()
                .Property(e => e.pincode)
                .IsUnicode(false);

            modelBuilder.Entity<columndefinition>()
                .Property(e => e.columnCategory)
                .IsUnicode(false);

            modelBuilder.Entity<columndefinition>()
                .Property(e => e.columnDataType)
                .IsUnicode(false);

            modelBuilder.Entity<columndefinition>()
                .Property(e => e.dataPattern)
                .IsUnicode(false);

            modelBuilder.Entity<columndefinition>()
                .Property(e => e.destinationColumnName)
                .IsUnicode(false);

            modelBuilder.Entity<columndefinition>()
                .Property(e => e.measureType)
                .IsUnicode(false);

            modelBuilder.Entity<columndefinition>()
                .Property(e => e.metaColumnName)
                .IsUnicode(false);

            modelBuilder.Entity<columndefinition>()
                .Property(e => e.sourceColumnName)
                .IsUnicode(false);

            modelBuilder.Entity<columndefinition>()
                .HasMany(e => e.uploadtype_columndefinition)
                .WithRequired(e => e.columndefinition)
                .HasForeignKey(e => e.definitions_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<complaint>()
                .Property(e => e.CallType)
                .IsUnicode(false);

            modelBuilder.Entity<complaint>()
                .Property(e => e.callMade)
                .IsUnicode(false);

            modelBuilder.Entity<complaint>()
                .Property(e => e.complaintNumber)
                .IsUnicode(false);

            modelBuilder.Entity<complaint>()
                .Property(e => e.complaintSource)
                .IsUnicode(false);

            modelBuilder.Entity<complaint>()
                .Property(e => e.complaintStatus)
                .IsUnicode(false);

            modelBuilder.Entity<complaint>()
                .Property(e => e.complaintType)
                .IsUnicode(false);

            modelBuilder.Entity<complaint>()
                .Property(e => e.customerName)
                .IsUnicode(false);

            modelBuilder.Entity<complaint>()
                .Property(e => e.customerPhone)
                .IsUnicode(false);

            modelBuilder.Entity<complaint>()
                .Property(e => e.customerstatus)
                .IsUnicode(false);

            modelBuilder.Entity<complaint>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<complaint>()
                .Property(e => e.functionName)
                .IsUnicode(false);

            modelBuilder.Entity<complaint>()
                .Property(e => e.isAssigned)
                .IsUnicode(false);

            modelBuilder.Entity<complaint>()
                .Property(e => e.priority)
                .IsUnicode(false);

            modelBuilder.Entity<complaint>()
                .Property(e => e.sourceName)
                .IsUnicode(false);

            modelBuilder.Entity<complaint>()
                .Property(e => e.subcomplaintType)
                .IsUnicode(false);

            modelBuilder.Entity<complaint>()
                .Property(e => e.vehicleRegNo)
                .IsUnicode(false);

            modelBuilder.Entity<complaint>()
                .Property(e => e.serviceadvisor)
                .IsUnicode(false);

            modelBuilder.Entity<complaint>()
                .Property(e => e.workshop)
                .IsUnicode(false);

            modelBuilder.Entity<complaint>()
                .HasMany(e => e.complaintinteractions)
                .WithOptional(e => e.complaint)
                .HasForeignKey(e => e.complaint_id);

            modelBuilder.Entity<complaint>()
                .HasMany(e => e.complaintassignedinteractions)
                .WithOptional(e => e.complaint)
                .HasForeignKey(e => e.complaint_id);

            modelBuilder.Entity<complaintassignedinteraction>()
                .Property(e => e.callMade)
                .IsUnicode(false);

            modelBuilder.Entity<complaintassignedinteraction>()
                .Property(e => e.complaintStatus)
                .IsUnicode(false);

            modelBuilder.Entity<complaintassignedinteraction>()
                .Property(e => e.isAssigned)
                .IsUnicode(false);

            modelBuilder.Entity<complaintinteraction>()
                .Property(e => e.actionTaken)
                .IsUnicode(false);

            modelBuilder.Entity<complaintinteraction>()
                .Property(e => e.complaintNumber)
                .IsUnicode(false);

            modelBuilder.Entity<complaintinteraction>()
                .Property(e => e.complaintStatus)
                .IsUnicode(false);

            modelBuilder.Entity<complaintinteraction>()
                .Property(e => e.customerstatus)
                .IsUnicode(false);

            modelBuilder.Entity<complaintinteraction>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<complaintinteraction>()
                .Property(e => e.functionName)
                .IsUnicode(false);

            modelBuilder.Entity<complaintinteraction>()
                .Property(e => e.priority)
                .IsUnicode(false);

            modelBuilder.Entity<complaintinteraction>()
                .Property(e => e.reasonFor)
                .IsUnicode(false);

            modelBuilder.Entity<complaintinteraction>()
                .Property(e => e.resolutionBy)
                .IsUnicode(false);

            modelBuilder.Entity<complaintinteraction>()
                .Property(e => e.sourceName)
                .IsUnicode(false);

            modelBuilder.Entity<complaintinteraction>()
                .Property(e => e.subcomplaintType)
                .IsUnicode(false);

            modelBuilder.Entity<complaintsource>()
                .Property(e => e.complaintSource1)
                .IsUnicode(false);

            modelBuilder.Entity<complainttype>()
                .Property(e => e.departmentName)
                .IsUnicode(false);

            modelBuilder.Entity<complainttype>()
                .Property(e => e.taggedUserName)
                .IsUnicode(false);

            modelBuilder.Entity<complainttype>()
                .Property(e => e.taggedUserNumber)
                .IsUnicode(false);

            modelBuilder.Entity<conversiondecription>()
                .Property(e => e.legend)
                .IsUnicode(false);

            modelBuilder.Entity<conversiondecription>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<conversiondecription>()
                .Property(e => e.moduleTypeid)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.anniversary_date)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.createdBy)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.customerCategory)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.customerEmail)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.customerFName)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.customerId)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.customerLName)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.customerName)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.dob)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.mode_of_contact)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.preferred_day)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.preferred_time_end)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.preferred_time_start)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.salutation)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.sourceType)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.userDriver)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.dbvehregno)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.regnoSMR)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.preff_workshop)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.regno_SMR)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.addresses)
                .WithOptional(e => e.customer)
                .HasForeignKey(e => e.customer_Id);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.appointmentbookeds)
                .WithOptional(e => e.customer)
                .HasForeignKey(e => e.customer_id);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.assignedinteractions)
                .WithOptional(e => e.customer)
                .HasForeignKey(e => e.customer_id);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.callinteractions)
                .WithOptional(e => e.customer)
                .HasForeignKey(e => e.customer_id);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.complaints)
                .WithOptional(e => e.customer)
                .HasForeignKey(e => e.customer_id);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.complaintinteractions)
                .WithOptional(e => e.customer)
                .HasForeignKey(e => e.customer_id);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.segments)
                .WithOptional(e => e.customer)
                .HasForeignKey(e => e.customer_id);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.servicebookeds)
                .WithOptional(e => e.customer)
                .HasForeignKey(e => e.customer_id);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.driverinteractions)
                .WithOptional(e => e.customer)
                .HasForeignKey(e => e.customer_id);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.insurances)
                .WithOptional(e => e.customer)
                .HasForeignKey(e => e.customer_id);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.insuranceassignedinteractions)
                .WithOptional(e => e.customer)
                .HasForeignKey(e => e.customer_id);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.serviceadvisorinteractions)
                .WithOptional(e => e.customer)
                .HasForeignKey(e => e.customer_id);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.vehicles)
                .WithOptional(e => e.customer)
                .HasForeignKey(e => e.customer_id);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.psfassignedinteractions)
                .WithOptional(e => e.customer)
                .HasForeignKey(e => e.customer_id);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.phones)
                .WithOptional(e => e.customer)
                .HasForeignKey(e => e.customer_id);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.emails)
                .WithOptional(e => e.customer)
                .HasForeignKey(e => e.customer_id);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.smsinteractions)
                .WithOptional(e => e.customer)
                .HasForeignKey(e => e.customer_id);

            modelBuilder.Entity<customersearchresult>()
                .Property(e => e.cid)
                .IsUnicode(false);

            modelBuilder.Entity<customersearchresult>()
                .Property(e => e.customerCategory)
                .IsUnicode(false);

            modelBuilder.Entity<customersearchresult>()
                .Property(e => e.customerName)
                .IsUnicode(false);

            modelBuilder.Entity<customersearchresult>()
                .Property(e => e.lastdisposition)
                .IsUnicode(false);

            modelBuilder.Entity<customersearchresult>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<customersearchresult>()
                .Property(e => e.nextServicedate)
                .IsUnicode(false);

            modelBuilder.Entity<customersearchresult>()
                .Property(e => e.phoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<customersearchresult>()
                .Property(e => e.variant)
                .IsUnicode(false);

            modelBuilder.Entity<customersearchresult>()
                .Property(e => e.vehicleRegNo)
                .IsUnicode(false);

            modelBuilder.Entity<customersearchresult>()
                .Property(e => e.vehicle_id)
                .IsUnicode(false);

            modelBuilder.Entity<customersearchresult>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<customersearchresult>()
                .Property(e => e.chassisno)
                .IsUnicode(false);

            modelBuilder.Entity<customersearchresult>()
                .Property(e => e.ins_assignedcre)
                .IsUnicode(false);

            modelBuilder.Entity<customersearchresult>()
                .Property(e => e.policyDueDate)
                .IsUnicode(false);

            modelBuilder.Entity<customersearchresult>()
                .Property(e => e.smr_assignedcre)
                .IsUnicode(false);

            modelBuilder.Entity<customersearchresult>()
                .HasMany(e => e.savedsearchresults)
                .WithMany(e => e.customersearchresults)
                .Map(m => m.ToTable("saved_search_results").MapLeftKey("searchResults_id").MapRightKey("SavedSearchResult_id"));

            modelBuilder.Entity<datarejectioncause>()
                .Property(e => e.rejectionCause)
                .IsUnicode(false);

            modelBuilder.Entity<dealer>()
                .Property(e => e.dealerAddress)
                .IsUnicode(false);

            modelBuilder.Entity<dealer>()
                .Property(e => e.dealerCode)
                .IsUnicode(false);

            modelBuilder.Entity<dealer>()
                .Property(e => e.dealerId)
                .IsUnicode(false);

            modelBuilder.Entity<dealer>()
                .Property(e => e.dealerName)
                .IsUnicode(false);

            modelBuilder.Entity<dealer>()
                .Property(e => e.noOfUsers)
                .IsUnicode(false);

            modelBuilder.Entity<dealer>()
                .Property(e => e.timeZoneOfDealer)
                .IsUnicode(false);

            modelBuilder.Entity<dealer>()
                .Property(e => e.OEM)
                .IsUnicode(false);

            modelBuilder.Entity<dealer>()
                .Property(e => e.maindb)
                .IsUnicode(false);

            modelBuilder.Entity<dealer>()
                .HasMany(e => e.wyzusers)
                .WithOptional(e => e.dealer)
                .HasForeignKey(e => e.dealer_id);

            modelBuilder.Entity<dealer>()
                .HasMany(e => e.locations)
                .WithOptional(e => e.dealer)
                .HasForeignKey(e => e.dealer_id);

            modelBuilder.Entity<dealer>()
                .HasMany(e => e.locations1)
                .WithOptional(e => e.dealer1)
                .HasForeignKey(e => e.dealer_id);

            modelBuilder.Entity<dealer>()
                .HasMany(e => e.wyzusers1)
                .WithOptional(e => e.dealer1)
                .HasForeignKey(e => e.dealer_id);

            modelBuilder.Entity<dealerpanel>()
                .Property(e => e.region)
                .IsUnicode(false);

            modelBuilder.Entity<dealerpanel>()
                .Property(e => e.workshopCode)
                .IsUnicode(false);

            modelBuilder.Entity<dealerpanel>()
                .Property(e => e.dealerName)
                .IsUnicode(false);

            modelBuilder.Entity<dealerpanel>()
                .Property(e => e.workshopType)
                .IsUnicode(false);

            modelBuilder.Entity<dealerpanel>()
                .Property(e => e.jDP_Non_JDP)
                .IsUnicode(false);

            modelBuilder.Entity<dealerpanel>()
                .Property(e => e.workshopGroup)
                .IsUnicode(false);

            modelBuilder.Entity<dealerpanel>()
                .Property(e => e.location)
                .IsUnicode(false);

            modelBuilder.Entity<dealerpanel>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<dealerpanel>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<dealerpanel>()
                .Property(e => e.state)
                .IsUnicode(false);

            modelBuilder.Entity<dealerpanel>()
                .Property(e => e.pincode)
                .IsUnicode(false);

            modelBuilder.Entity<deletedstatusreport>()
                .Property(e => e.final_disposition)
                .IsUnicode(false);

            modelBuilder.Entity<deletedstatusreport>()
                .Property(e => e.called_status)
                .IsUnicode(false);

            modelBuilder.Entity<deletedstatusreport>()
                .Property(e => e.whendeleted)
                .IsUnicode(false);

            modelBuilder.Entity<deletedstatusreport>()
                .Property(e => e.assignType)
                .IsUnicode(false);

            modelBuilder.Entity<deletedstatusreport>()
                .Property(e => e.moduletype_id)
                .IsUnicode(false);

            modelBuilder.Entity<deletedstatusreport>()
                .Property(e => e.ronumber)
                .IsUnicode(false);

            modelBuilder.Entity<deletedstatusreport>()
                .Property(e => e.Reported_Status)
                .IsUnicode(false);

            modelBuilder.Entity<deletedstatusreport>()
                .Property(e => e.renewedPolicyNumber)
                .IsUnicode(false);

            modelBuilder.Entity<deletedstatusreport>()
                .Property(e => e.AppointmentStatus)
                .IsUnicode(false);

            modelBuilder.Entity<documentuploadhistory>()
                .Property(e => e.deptName)
                .IsUnicode(false);

            modelBuilder.Entity<documentuploadhistory>()
                .Property(e => e.documentName)
                .IsUnicode(false);

            modelBuilder.Entity<documentuploadhistory>()
                .Property(e => e.filePath)
                .IsUnicode(false);

            modelBuilder.Entity<documentuploadhistory>()
                .Property(e => e.user)
                .IsUnicode(false);

            modelBuilder.Entity<documentuploadhistory>()
                .Property(e => e.uploadFileName)
                .IsUnicode(false);

            modelBuilder.Entity<driver>()
                .Property(e => e.driverAltPhoneNum)
                .IsUnicode(false);

            modelBuilder.Entity<driver>()
                .Property(e => e.driverName)
                .IsUnicode(false);

            modelBuilder.Entity<driver>()
                .Property(e => e.driverPhoneNum)
                .IsUnicode(false);

            modelBuilder.Entity<driver>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<driver>()
                .HasMany(e => e.pickupdrops)
                .WithOptional(e => e.driver)
                .HasForeignKey(e => e.driver_id);

            modelBuilder.Entity<driver>()
                .HasMany(e => e.servicebookeds)
                .WithOptional(e => e.driver)
                .HasForeignKey(e => e.driver_id);

            modelBuilder.Entity<driverinteraction>()
                .Property(e => e.firebaseKey)
                .IsUnicode(false);

            modelBuilder.Entity<driverinteraction>()
                .Property(e => e.interactionDate)
                .IsUnicode(false);

            modelBuilder.Entity<driverinteraction>()
                .Property(e => e.interactionTime)
                .IsUnicode(false);

            modelBuilder.Entity<driverinteraction>()
                .Property(e => e.mediaFile)
                .IsUnicode(false);

            modelBuilder.Entity<driverinteraction>()
                .Property(e => e.pickupOrDrop)
                .IsUnicode(false);

            modelBuilder.Entity<driverinteractionstatu>()
                .Property(e => e.interactionStatus)
                .IsUnicode(false);

            modelBuilder.Entity<driverinteractionstatu>()
                .HasMany(e => e.driverinteractions)
                .WithOptional(e => e.driverinteractionstatu)
                .HasForeignKey(e => e.status_id);

            modelBuilder.Entity<dynamicdashformat>()
                .Property(e => e.chartName)
                .IsUnicode(false);

            modelBuilder.Entity<dynamicdashformat>()
                .Property(e => e.sqlQuery)
                .IsUnicode(false);

            modelBuilder.Entity<dynamicdashformat>()
                .HasMany(e => e.axisdetails)
                .WithRequired(e => e.dynamicdashformat)
                .HasForeignKey(e => e.dashboardId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<email>()
                .Property(e => e.emailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<email>()
                .Property(e => e.updatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<email>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<emailcredential>()
                .Property(e => e.hostapi)
                .IsUnicode(false);

            modelBuilder.Entity<emailcredential>()
                .Property(e => e.userEmail)
                .IsUnicode(false);

            modelBuilder.Entity<emailcredential>()
                .Property(e => e.userPassword)
                .IsUnicode(false);

            modelBuilder.Entity<emailinteraction>()
                .Property(e => e.interactionDate)
                .IsUnicode(false);

            modelBuilder.Entity<emailinteraction>()
                .Property(e => e.interactionTime)
                .IsUnicode(false);

            modelBuilder.Entity<emailinteraction>()
                .Property(e => e.emailSubject)
                .IsUnicode(false);

            modelBuilder.Entity<emailinteraction>()
                .Property(e => e.emailContent)
                .IsUnicode(false);

            modelBuilder.Entity<emailinteraction>()
                .Property(e => e.toEmailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<emailinteraction>()
                .Property(e => e.fromEmailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<emailinteraction>()
                .Property(e => e.cc)
                .IsUnicode(false);

            modelBuilder.Entity<emailinteraction>()
                .Property(e => e.reason)
                .IsUnicode(false);

            modelBuilder.Entity<emailinteraction>()
                .Property(e => e.emailType)
                .IsUnicode(false);

            modelBuilder.Entity<emailinteraction>()
                .Property(e => e.exceptionResponse)
                .IsUnicode(false);

            modelBuilder.Entity<emailinteraction>()
                .Property(e => e.emailGreetings)
                .IsUnicode(false);

            modelBuilder.Entity<emailinteraction>()
                .Property(e => e.emailId)
                .IsUnicode(false);

            modelBuilder.Entity<emailinteraction>()
                .Property(e => e.emailSign)
                .IsUnicode(false);

            modelBuilder.Entity<emailinteraction>()
                .Property(e => e.emailTemplate)
                .IsUnicode(false);

            modelBuilder.Entity<emailtemplate>()
                .Property(e => e.emailType)
                .IsUnicode(false);

            modelBuilder.Entity<emailtemplate>()
                .Property(e => e.cc)
                .IsUnicode(false);

            modelBuilder.Entity<emailtemplate>()
                .Property(e => e.subjectTemplate)
                .IsUnicode(false);

            modelBuilder.Entity<emailtemplate>()
                .Property(e => e.headerTemplate)
                .IsUnicode(false);

            modelBuilder.Entity<emailtemplate>()
                .Property(e => e.bodyTemplate)
                .IsUnicode(false);

            modelBuilder.Entity<emailtemplate>()
                .Property(e => e.footerTemplate)
                .IsUnicode(false);

            modelBuilder.Entity<emailtemplate>()
                .Property(e => e.sendingType)
                .IsUnicode(false);

            modelBuilder.Entity<emailtemplate>()
                .Property(e => e.emailGreetings)
                .IsUnicode(false);

            modelBuilder.Entity<emailtemplate>()
                .Property(e => e.emailId)
                .IsUnicode(false);

            modelBuilder.Entity<emailtemplate>()
                .Property(e => e.emailSign)
                .IsUnicode(false);

            modelBuilder.Entity<emailtemplate>()
                .Property(e => e.emailSubject)
                .IsUnicode(false);

            modelBuilder.Entity<emailtemplate>()
                .Property(e => e.emailTemplate1)
                .IsUnicode(false);

            modelBuilder.Entity<feedbackallocation>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<feedbackallocation>()
                .Property(e => e.form_name)
                .IsUnicode(false);

            modelBuilder.Entity<feedbackallocation>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<feedbackallocation>()
                .Property(e => e.process_name)
                .IsUnicode(false);

            modelBuilder.Entity<feedbackallocation>()
                .Property(e => e.userRole)
                .IsUnicode(false);

            modelBuilder.Entity<feedbackallocation>()
                .Property(e => e.viewBy)
                .IsUnicode(false);

            modelBuilder.Entity<feedbackrating>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<feedbackrating>()
                .Property(e => e.feedbackRatings)
                .IsUnicode(false);

            modelBuilder.Entity<feedbackrating>()
                .Property(e => e.form_name)
                .IsUnicode(false);

            modelBuilder.Entity<feedbackrating>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<feedbackrating>()
                .Property(e => e.process_name)
                .IsUnicode(false);

            modelBuilder.Entity<feedbackrating>()
                .Property(e => e.questions)
                .IsUnicode(false);

            modelBuilder.Entity<fieldwalkinlocation>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<fieldwalkinlocation>()
                .Property(e => e.typeOfLocation)
                .IsUnicode(false);

            modelBuilder.Entity<fileuploadformat>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<fileuploadformat>()
                .Property(e => e.data_type)
                .IsUnicode(false);

            modelBuilder.Entity<fileuploadformat>()
                .Property(e => e.excel_column)
                .IsUnicode(false);

            modelBuilder.Entity<fileuploadformat>()
                .Property(e => e.model_column)
                .IsUnicode(false);

            modelBuilder.Entity<fileuploadformat>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<fileuploadformat>()
                .Property(e => e.process_name)
                .IsUnicode(false);

            modelBuilder.Entity<fileuploadformat>()
                .Property(e => e.uploadTable_name)
                .IsUnicode(false);

            modelBuilder.Entity<forecastlogicservicetype>()
                .Property(e => e.servicetype)
                .IsUnicode(false);

            modelBuilder.Entity<forecastservicecategory>()
                .Property(e => e.servicecategory)
                .IsUnicode(false);

            modelBuilder.Entity<forecastservicetype>()
                .Property(e => e.mainType)
                .IsUnicode(false);

            modelBuilder.Entity<forecastservicetype>()
                .Property(e => e.serviceType)
                .IsUnicode(false);

            modelBuilder.Entity<forex>()
                .Property(e => e.currency)
                .IsUnicode(false);

            modelBuilder.Entity<forex>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<formdata>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<formdata>()
                .Property(e => e.value)
                .IsUnicode(false);

           
           
            modelBuilder.Entity<imagestorage>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<imagestorage>()
                .Property(e => e.imageId)
                .IsUnicode(false);

            modelBuilder.Entity<imagestorage>()
                .Property(e => e.latitude)
                .IsUnicode(false);

            modelBuilder.Entity<imagestorage>()
                .Property(e => e.latitude1)
                .IsUnicode(false);

            modelBuilder.Entity<imagestorage>()
                .Property(e => e.latitude2)
                .IsUnicode(false);

            modelBuilder.Entity<imagestorage>()
                .Property(e => e.latitude3)
                .IsUnicode(false);

            modelBuilder.Entity<imagestorage>()
                .Property(e => e.latitude4)
                .IsUnicode(false);

            modelBuilder.Entity<imagestorage>()
                .Property(e => e.latitude5)
                .IsUnicode(false);

            modelBuilder.Entity<imagestorage>()
                .Property(e => e.longitude)
                .IsUnicode(false);

            modelBuilder.Entity<imagestorage>()
                .Property(e => e.longitude1)
                .IsUnicode(false);

            modelBuilder.Entity<imagestorage>()
                .Property(e => e.longitude2)
                .IsUnicode(false);

            modelBuilder.Entity<imagestorage>()
                .Property(e => e.longitude3)
                .IsUnicode(false);

            modelBuilder.Entity<imagestorage>()
                .Property(e => e.longitude4)
                .IsUnicode(false);

            modelBuilder.Entity<imagestorage>()
                .Property(e => e.longitude5)
                .IsUnicode(false);

            modelBuilder.Entity<imagestorage>()
                .Property(e => e.mode)
                .IsUnicode(false);

            modelBuilder.Entity<imagestorage>()
                .Property(e => e.signature)
                .IsUnicode(false);

            modelBuilder.Entity<imagestorage>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<imagestorage>()
                .HasMany(e => e.casesummaries)
                .WithOptional(e => e.imagestorage)
                .HasForeignKey(e => e.image_Id);

            modelBuilder.Entity<insautoassignmentsummary>()
                .Property(e => e.filename)
                .IsUnicode(false);

            modelBuilder.Entity<insforecastassignmenthistory>()
                .Property(e => e.renewaltype)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.add_ON_Premium)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.coverageFromTo)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.coveragePeriod)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.discountPercentage)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.insuranceCompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.lastCoverNoteNumber)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.lastRenewalBy)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.ncBPercentage)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.odPercentage)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.policyNo)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.renewalType)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.serviceTax)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.add_ON_CoverPercentage)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.cubicCapacity)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.discValue)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.typeCode)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.vehicleAge)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.vehicleCity)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.vehicleZone)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.policyType)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.classType)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.aPPremium)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.pAPremium)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.legalLiability)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.netLiability)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.addOn)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.otherPremium)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.netPremium)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.grossPremium)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.dealer)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.zone)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.reportedlocation)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.paymentType)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.lastServiceType)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.paymentReference)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.AntitheftDeviceInstalled)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.phonenumber)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .Property(e => e.reportedlocname)
                .IsUnicode(false);

            modelBuilder.Entity<insurance>()
                .HasMany(e => e.appointmentbookeds)
                .WithOptional(e => e.insurance)
                .HasForeignKey(e => e.insurance_id);

            modelBuilder.Entity<insurance>()
                .HasMany(e => e.insuranceassignedinteractions)
                .WithOptional(e => e.insurance)
                .HasForeignKey(e => e.insurance_id);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.insuredName)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.campaignToDate)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.campaignFromDate)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.grossPremium)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.tax)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.netPremium)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.otherPremium)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.addOnPremium)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.addOn)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.netLiability)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.legalLiability)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.paPremium)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.tpPremium)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.oDPremium)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.discount)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.nCBValue)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.nCBpercentage)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.basicOD)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.oDPercentage)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.iDV)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e._class)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.policytype)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.insuranceCompany)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.policyNumber)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.color)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.vehicleRegNo)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.saleDate)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.fuelType)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.vehicleType)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.variant)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.engineNo)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.chassisNo)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.dob)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.doa)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.eMailID)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.landlineNo)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.sTDCode)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.mobileNo)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.state)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.pincode)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.dealer)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.location_id)
                .IsUnicode(false);

            modelBuilder.Entity<insurance_history>()
                .Property(e => e.workshop_id)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceagent>()
                .Property(e => e.insuranceAgentName)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceagent>()
                .Property(e => e.insuranceAgentNumber)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceagent>()
                .HasMany(e => e.appointmentbookeds)
                .WithOptional(e => e.insuranceagent)
                .HasForeignKey(e => e.insuranceAgent_insuranceAgentId);

            modelBuilder.Entity<insuranceassignedinteraction>()
                .Property(e => e.callMade)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceassignedinteraction>()
                .Property(e => e.interactionType)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceassignedinteraction>()
                .Property(e => e.lastDisposition)
                .IsUnicode(false);

            //modelBuilder.Entity<insuranceassignedinteraction>()
            //    .Property(e => e.policyDueDate)
            //    .IsUnicode(false);

            modelBuilder.Entity<insuranceassignedinteraction>()
                .Property(e => e.policyDueType)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceassignedinteraction>()
                .Property(e => e.insuranceCompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceassignedinteraction>()
                .HasMany(e => e.callinteractions)
                .WithOptional(e => e.insuranceassignedinteraction)
                .HasForeignKey(e => e.insuranceAssignedInteraction_id);

            modelBuilder.Entity<insuranceassignmentreport_nthday>()
                .Property(e => e.appointment_status)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.creName)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.isCallinitaited)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.callType)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.callDuration)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.ringTime)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.campaignTYpe)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.chassisNo)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.variant)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.saleDate)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.vehicleRegNo)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.customerName)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.preffered_address)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.prefferedPhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.PrimaryDisposition)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.SecondaryDisposition)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.coverNoteNo)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.lastRenewedLocation)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.lastRenewalDate)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.premimum)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.renewalDoneBy)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.Tertiary_disposition)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.followUpTime)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.addOnsPrefered_OtherOptionsData)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.addOnsPrefered_PopularOptionsData)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.remarksOfFB)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.departmentForFB)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.complaintOrFB_TagTo)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.appointmentId)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.addressOfVisit)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.appointeeName)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.appointmentFromTime)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.dsa)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.insuranceAgentData)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.insuranceCompany)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.nomineeName)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.nomineeRelationWithOwner)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.premiumwithTax)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.renewalMode)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.renewalType)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.typeOfPickup)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.insuranceAgent_insuranceAgentId)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.PremiumYes)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.reason)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.cremanager)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.location)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.filepath)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.makeCallFrom)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.dailedNumber)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.addons)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.addOn_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.addOnPercentage_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.commentIQ_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.createdCRE_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.customer_id_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.discountPercentage_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.discountValue_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.idv_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.insuranceCompany_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.liabilityPremium_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.ncbPercentage_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.ncbRupees_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.oDPremium_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.odPercentage_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.odRupees_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.paPremium_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.qt_Date_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.serviceTax_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.thirdPartyPremium_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.totalODPremium_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.totalPremiumWithOutTax_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.totalPremiumWithTax_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.vehicle_id_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.createdDate_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.insuranceCompanyQuotated_qi)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.renewalNotRequiredReason)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.paymentReference)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.paymentType)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.other)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.transferedCity)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.typeOfInsurance)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.pinCodeOfCity)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.noServiceReasonTaggedToComments)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.noServiceReasonTaggedTo)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.insuranceProvidedBy)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.inBoundLeadSource)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.appointment_status)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.policyNumber)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.assigned_ins_company)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.assigned_campaignid)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.assi_policyduetype)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.assi_policyduedate)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.assigned_wyzuserid)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.assigned_crename)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.assignedDate)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.upselltypes)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.upselltaggedto)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.upselcomments)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.appointment_closed)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.closed_condition)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.closed_reneweddate)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.closed_policynumber)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.closed_renewal_type)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.Reshedule_Status)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.appointment_status_id)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.booked_workshop)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.renewed_location)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.closed_paymentMode)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecallhistorycube>()
                .Property(e => e.customerComments)
                .IsUnicode(false);

            modelBuilder.Entity<insurancecompany>()
                .Property(e => e.companyName)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.DistancefromDealerLocationRadio)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.InOutCallName)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.cityName)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.complaintOrFB_TagTo)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.coverNoteNo)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.dealerName)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.departmentForFB)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.followUpTime)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.inBoundLeadSource)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.insuranceProvidedBy)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.isFollowUpDone)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.isFollowupRequired)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.lastRenewedLocation)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.noServiceReasonTaggedTo)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.noServiceReasonTaggedToComments)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.optforMMS)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.pinCodeOfCity)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.premimum)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.reason)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.remarksOfFB)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.renewalDoneBy)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.renewalNotRequiredReason)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.serviceAdvisorID)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.transferedCity)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.typeOfAutherization)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.typeOfDisposition)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.typeOfInsurance)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.addOnsPrefered_OtherOptionsData)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.addOnsPrefered_PopularOptionsData)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.other)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.lastServiceType)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.paymentReference)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.paymentType)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.addons)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.appointeeName)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.nomineeAge)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.nomineeName)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.nomineeRelationWithOwner)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.othersINS)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.policyDropAddress)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .Property(e => e.policyPincode)
                .IsUnicode(false);

            modelBuilder.Entity<insurancedisposition>()
                .HasMany(e => e.upsellleads)
                .WithOptional(e => e.insurancedisposition)
                .HasForeignKey(e => e.insuranceDisposition_id);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.add_ON_Premium)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.chassisNo)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.color)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.contactNo)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.coveragePeriod)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.customerName)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.discountPercentage)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.doa)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.dob)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.engineNo)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.insuranceCompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.lastMileage)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.lastRenewalBy)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.ncBPercentage)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.odPercentage)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.policyNo)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.renewalType)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.serviceTax)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.state)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.variant)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.vehicleRegNo)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.location)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.campaignName)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.workshop_id)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.campaignToDate)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.campaignFromDate)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexceldata>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.ODpremiumStr)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.add_ON_Premium)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.chassisNo)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.color)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.contactNo)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.coveragePeriod)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.customerName)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.discountPercentage)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.doa)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.dob)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.engineNo)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.errorInformation)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.idvStr)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.insuranceCompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.lastMileage)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.lastRenewalBy)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.liabilityPremiumStr)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.ncBAmountStr)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.ncBPercentage)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.odAmountStr)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.odPercentage)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.policyDueDateStr)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.policyNo)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.premiumAmountAfterTaxStr)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.premiumAmountBeforeTaxStr)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.renewalType)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.saleDateStr)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.serviceTax)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.state)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.variant)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.vehicleRegNo)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceexcelerror>()
                .Property(e => e.dobStr)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceforecasteddata>()
                .Property(e => e.vehicleRegNo)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceforecasteddata>()
                .Property(e => e.customerName)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceforecasteddata>()
                .Property(e => e.phonenumber)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceforecasteddata>()
                .Property(e => e.customer_id)
                .IsUnicode(false);

            //modelBuilder.Entity<insuranceforecasteddata>()
            //    .Property(e => e.location_id)
            //    .IsUnicode(false);

            modelBuilder.Entity<insuranceforecasteddata>()
                .Property(e => e.chassisNo)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceforecasteddata>()
                .Property(e => e.saledate)
                .IsUnicode(false);

            //modelBuilder.Entity<insuranceforecasteddata>()
            //    .Property(e => e.policyexpirydate)
            //    .IsUnicode(false);

            modelBuilder.Entity<insuranceforecasteddata>()
                .Property(e => e.IsAssigned)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceforecasteddata>()
                .Property(e => e.crename)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceforecasteddata>()
                .Property(e => e.lastdisposition)
                .IsUnicode(false);

            modelBuilder.Entity<insuranceforecasteddata>()
                .Property(e => e.insurancecompanyname)
                .IsUnicode(false);

            modelBuilder.Entity<insurancequotation>()
                .Property(e => e.commentIQ)
                .IsUnicode(false);

            modelBuilder.Entity<insurancequotation>()
                .Property(e => e.createdCRE)
                .IsUnicode(false);

            modelBuilder.Entity<insurancequotation>()
                .Property(e => e.insuranceCompany)
                .IsUnicode(false);

            modelBuilder.Entity<insurancequotation>()
                .Property(e => e.insuranceCompanyQuotated)
                .IsUnicode(false);

            modelBuilder.Entity<insurancequotation>()
                .Property(e => e.msgURL)
                .IsUnicode(false);

            modelBuilder.Entity<insurancequotation>()
                .Property(e => e.emailURL)
                .IsUnicode(false);

            modelBuilder.Entity<insurance1>()
                .Property(e => e.insuranceCompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<insurance1>()
                .Property(e => e.policyNo)
                .IsUnicode(false);

            modelBuilder.Entity<insurance1>()
                .Property(e => e.renewalType)
                .IsUnicode(false);

            modelBuilder.Entity<insurance1>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<insurance1>()
                .Property(e => e.policyType)
                .IsUnicode(false);

            modelBuilder.Entity<insurance1>()
                .Property(e => e.reportedlocation)
                .IsUnicode(false);

            modelBuilder.Entity<insurance1>()
                .Property(e => e.phonenumber)
                .IsUnicode(false);

            modelBuilder.Entity<insurance1>()
                .Property(e => e.classType)
                .IsUnicode(false);

            modelBuilder.Entity<invalidworkshopdata>()
                .Property(e => e.chassisNo)
                .IsUnicode(false);

            modelBuilder.Entity<invalidworkshopdata>()
                .Property(e => e.customerName)
                .IsUnicode(false);

            modelBuilder.Entity<invalidworkshopdata>()
                .Property(e => e.uploaded_Date)
                .IsUnicode(false);

            modelBuilder.Entity<invalidworkshopdata>()
                .Property(e => e.uploadid)
                .IsUnicode(false);

            modelBuilder.Entity<invalidworkshopdata>()
                .Property(e => e.workshopCode)
                .IsUnicode(false);

            modelBuilder.Entity<invalidworkshopdata>()
                .Property(e => e.smrId)
                .IsUnicode(false);

            modelBuilder.Entity<invalidworkshopdata>()
                .Property(e => e.moduletypeid)
                .IsUnicode(false);

            modelBuilder.Entity<irda_od_premium>()
                .Property(e => e.cubicCapacity)
                .IsUnicode(false);

            modelBuilder.Entity<irda_od_premium>()
                .Property(e => e.vehicleAge)
                .IsUnicode(false);

            modelBuilder.Entity<irda_od_premium>()
                .Property(e => e.zone)
                .IsUnicode(false);

            modelBuilder.Entity<irda_od_premium>()
                .Property(e => e.vehicleType)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.addressLine1)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.addressLine2)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.addressLine3)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.advisorName)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.anniversary_date)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.billAmt)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.billDate)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.billNo)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.category_name)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.chassisNo)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.circularNo)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.color)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.colorCode)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.country)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.customer_id)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.customerName)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.dob)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.engineNo)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.enquiryNo)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.estLabAmt)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.estPartAmt)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.fuelType)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.groupData)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.jobCardDate)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.jobCardNumber)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.jobCardStatus)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.labAmt)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.lastServiceDate)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.lastServiceType)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.mileage)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.partsAmt)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.phoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.pickupDate)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.pincode)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.promiseDate)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.psfStatus)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.readyDate)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.repeat_revisit)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.revisedPromiseDate)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.saleDate)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.serviceAdvisor_advisorId)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.state)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.technician)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.variant)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.Variant_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.variantCode)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.veh_customer_id)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.vehicle_id)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.vehicle_vehicle_id)
                .IsUnicode(false);

            modelBuilder.Entity<jobcard_report>()
                .Property(e => e.vehicleRegNo)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.address1)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.address2)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.address3)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.billAmtStr)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.billDateStr)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.billNo)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.centre)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.chassis)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.circularNo)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.color)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.custCat)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.custName)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.doaStr)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.dobStr)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.engineNo)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.errorInformation)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.estLabAmtStr)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.estPartAmtStr)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.groupData)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.jobCardDateStr)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.jobCardNo)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.labAmtStr)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.mi)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.mileageStr)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.mobile)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.partsAmtStr)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.pickupDateStr)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.pickupLoc)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.pickupRequired)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.pincodeStr)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.promiseDtStr)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.psfStatus)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.readyDateandTimeStr)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.regNo)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.repeatRevisit)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.revPromisedDtStr)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.saleDateStr)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.serviceAdvisor)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.serviceType)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.slNo)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.technician)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.variant)
                .IsUnicode(false);

            modelBuilder.Entity<jobcardstatuserror>()
                .Property(e => e.yearStr)
                .IsUnicode(false);

            modelBuilder.Entity<location>()
                .Property(e => e.locCode)
                .IsUnicode(false);

            modelBuilder.Entity<location>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<location>()
                .Property(e => e.pinCode)
                .IsUnicode(false);

            modelBuilder.Entity<location>()
                .Property(e => e.state)
                .IsUnicode(false);

            modelBuilder.Entity<location>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<location>()
                .Property(e => e.locAddress)
                .IsUnicode(false);

            modelBuilder.Entity<location>()
                .Property(e => e.locPhone)
                .IsUnicode(false);

            modelBuilder.Entity<location>()
                .HasMany(e => e.campaigns)
                .WithOptional(e => e.location)
                .HasForeignKey(e => e.location_cityId);

            modelBuilder.Entity<location>()
                .HasMany(e => e.complaints)
                .WithOptional(e => e.location)
                .HasForeignKey(e => e.location_cityId);

            modelBuilder.Entity<location>()
                .HasMany(e => e.complaintinteractions)
                .WithOptional(e => e.location)
                .HasForeignKey(e => e.location_cityId);

            modelBuilder.Entity<location>()
                .HasMany(e => e.workshops)
                .WithOptional(e => e.location)
                .HasForeignKey(e => e.location_cityId);

            modelBuilder.Entity<location>()
                .HasMany(e => e.userlocations)
                .WithRequired(e => e.location)
                .HasForeignKey(e => e.locationList_cityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<location>()
                .HasMany(e => e.vehicles)
                .WithOptional(e => e.location)
                .HasForeignKey(e => e.location_cityId);

            modelBuilder.Entity<location>()
                .HasMany(e => e.uploaddatas)
                .WithOptional(e => e.location)
                .HasForeignKey(e => e.location_cityId);

            modelBuilder.Entity<location>()
                .HasMany(e => e.taggingusers)
                .WithOptional(e => e.location)
                .HasForeignKey(e => e.location_cityId);

            modelBuilder.Entity<location>()
                .HasMany(e => e.wyzusers)
                .WithOptional(e => e.location)
                .HasForeignKey(e => e.location_cityId);

            modelBuilder.Entity<logininfo>()
                .Property(e => e.ip_address)
                .IsUnicode(false);

            modelBuilder.Entity<logininfo>()
                .Property(e => e.oem)
                .IsUnicode(false);

            modelBuilder.Entity<logininfo>()
                .Property(e => e.tenantCode)
                .IsUnicode(false);

            modelBuilder.Entity<logininfo>()
                .Property(e => e.tenantName)
                .IsUnicode(false);

            modelBuilder.Entity<logininfo>()
                .Property(e => e.user_role)
                .IsUnicode(false);

            modelBuilder.Entity<logininfo>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<logininformation>()
                .Property(e => e.ip_address)
                .IsUnicode(false);

            modelBuilder.Entity<logininformation>()
                .Property(e => e.user_role)
                .IsUnicode(false);

            modelBuilder.Entity<logininformation>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<messageparameter>()
                .Property(e => e.modeltable)
                .IsUnicode(false);

            modelBuilder.Entity<messageparameter>()
                .Property(e => e.query)
                .IsUnicode(false);

            modelBuilder.Entity<messageparameter>()
                .Property(e => e.detail)
                .IsUnicode(false);

            modelBuilder.Entity<messagetemplete>()
                .Property(e => e.dealerCode)
                .IsUnicode(false);

            modelBuilder.Entity<messagetemplete>()
                .Property(e => e.messageBody)
                .IsUnicode(false);

            modelBuilder.Entity<messagetemplete>()
                .Property(e => e.messageHeader)
                .IsUnicode(false);

            modelBuilder.Entity<messagetemplete>()
                .Property(e => e.messageType)
                .IsUnicode(false);

            modelBuilder.Entity<modelcache>()
                .Property(e => e.updateContent)
                .IsUnicode(false);

            modelBuilder.Entity<modelslist>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<modelslist>()
                .Property(e => e.modelCode)
                .IsUnicode(false);

            modelBuilder.Entity<moduletype>()
                .Property(e => e.moduleName)
                .IsUnicode(false);

            modelBuilder.Entity<movie>()
                .Property(e => e.CustomerMobile)
                .IsUnicode(false);

            modelBuilder.Entity<movie>()
                .Property(e => e.adrress)
                .IsUnicode(false);

            modelBuilder.Entity<movie>()
                .Property(e => e.customerName)
                .IsUnicode(false);

            modelBuilder.Entity<movie>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<movie>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<movie>()
                .Property(e => e.number)
                .IsUnicode(false);

            modelBuilder.Entity<movie>()
                .Property(e => e.teliphone)
                .IsUnicode(false);

            modelBuilder.Entity<outlet>()
                .Property(e => e.outletCode)
                .IsUnicode(false);

            modelBuilder.Entity<outlet>()
                .HasMany(e => e.vehicles)
                .WithOptional(e => e.outlet)
                .HasForeignKey(e => e.outlets_id);

            modelBuilder.Entity<phone>()
                .Property(e => e.phoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<phone>()
                .Property(e => e.updatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<phone>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<phone>()
                .Property(e => e.remarks)
                .IsUnicode(false);

            modelBuilder.Entity<phonenodeletion>()
                .Property(e => e.phoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<phonenodeletion>()
                .Property(e => e.remarks)
                .IsUnicode(false);

            modelBuilder.Entity<pickupdrop>()
                .Property(e => e.CRN)
                .IsUnicode(false);

            modelBuilder.Entity<pickupdrop>()
                .Property(e => e.pickUpAddress)
                .IsUnicode(false);

            modelBuilder.Entity<pickupdrop>()
                .Property(e => e.pickupTime)
                .IsUnicode(false);

            modelBuilder.Entity<pickupdrop>()
                .HasMany(e => e.appointmentbookeds)
                .WithOptional(e => e.pickupdrop)
                .HasForeignKey(e => e.pickupDrop_id);

            modelBuilder.Entity<pickupdrop>()
                .HasMany(e => e.servicebookeds)
                .WithOptional(e => e.pickupdrop)
                .HasForeignKey(e => e.pickupDrop_id);

            modelBuilder.Entity<pickupdrop>()
                .HasMany(e => e.psfinteractions)
                .WithOptional(e => e.pickupdrop)
                .HasForeignKey(e => e.pickupDrop_id);

          
            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.ChassisNo)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.PrimaryDisposition)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.SecondaryDisposition)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.assignedcre)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.callDuration)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.callType)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.cremanager)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.customerName)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.dailedCre)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.dailedNumber)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.filePath)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.fileSize)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.isCallinitaited)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.isContacted)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.isFollowUpDone)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.isFollowupRequired)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.prefferedPhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.preffered_address)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.psfAppointmentTime)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.psfCallingDayType)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.psfFollowUpTime)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.psfQ1)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.psfQ10)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.psfQ11)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.psfQ12)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.psfQ13)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.psfQ14)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.psfQ15)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.psfQ16)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.psfQ17)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.psfQ2)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.psfQ3)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.psfQ4)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.psfQ5)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.psfQ6)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.psfQ7)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.psfQ8)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.psfQ9)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.remarks)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.ringTime)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.variant)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.vehicleRegNo)
                .IsUnicode(false);

            modelBuilder.Entity<postsalescallhistorycube>()
                .Property(e => e.workshop)
                .IsUnicode(false);

            modelBuilder.Entity<postservicefeedback>()
                .Property(e => e.specialMessage)
                .IsUnicode(false);

            modelBuilder.Entity<postservicefeedback>()
                .HasMany(e => e.psfinteractions)
                .WithOptional(e => e.postservicefeedback)
                .HasForeignKey(e => e.postServiceFeedBack_id);

            modelBuilder.Entity<postupdate>()
                .Property(e => e.contentType)
                .IsUnicode(false);

            modelBuilder.Entity<postupdate>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<postupdate>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<postupdate>()
                .Property(e => e.testFormName)
                .IsUnicode(false);

            modelBuilder.Entity<postupdate>()
                .Property(e => e.updateContent)
                .IsUnicode(false);

            modelBuilder.Entity<postupdate>()
                .Property(e => e.updateFilePath)
                .IsUnicode(false);

            modelBuilder.Entity<postupdate>()
                .Property(e => e.updateSubType)
                .IsUnicode(false);

            modelBuilder.Entity<postupdate>()
                .Property(e => e.updateTitle)
                .IsUnicode(false);

            modelBuilder.Entity<postupdate>()
                .Property(e => e.updateType)
                .IsUnicode(false);

            modelBuilder.Entity<postupdate>()
                .Property(e => e.viewUpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<postupdatemapping>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<postupdatemapping>()
                .Property(e => e.postUpdateId)
                .IsUnicode(false);

            modelBuilder.Entity<postupdatemapping>()
                .Property(e => e.updateCreated_by)
                .IsUnicode(false);

            modelBuilder.Entity<postupdatemapping>()
                .Property(e => e.user_id)
                .IsUnicode(false);

            modelBuilder.Entity<primaryservicedata>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<primaryservicedata>()
                .Property(e => e.vehicleRegNo)
                .IsUnicode(false);

            modelBuilder.Entity<primaryservicedata>()
                .Property(e => e.chassisNo)
                .IsUnicode(false);

            modelBuilder.Entity<primaryservicedata>()
                .Property(e => e.fstype)
                .IsUnicode(false);

            modelBuilder.Entity<primaryservicedata>()
                .Property(e => e.pstype)
                .IsUnicode(false);

            modelBuilder.Entity<primaryservicedata>()
                .Property(e => e.otherstype)
                .IsUnicode(false);

            modelBuilder.Entity<processdefinition>()
                .Property(e => e.chart_name)
                .IsUnicode(false);

            modelBuilder.Entity<processdefinition>()
                .Property(e => e.chart_type)
                .IsUnicode(false);

            modelBuilder.Entity<processdefinition>()
                .Property(e => e.dashboard_name)
                .IsUnicode(false);

            modelBuilder.Entity<processdefinition>()
                .Property(e => e.process_name)
                .IsUnicode(false);

            modelBuilder.Entity<processdefinition>()
                .Property(e => e.table_name)
                .IsUnicode(false);

            modelBuilder.Entity<processdefinition>()
                .Property(e => e.x_axis)
                .IsUnicode(false);

            modelBuilder.Entity<processdefinition>()
                .Property(e => e.x_axis_fields)
                .IsUnicode(false);

            modelBuilder.Entity<processdefinition>()
                .Property(e => e.y_axis)
                .IsUnicode(false);

            modelBuilder.Entity<processdefinition>()
                .Property(e => e.y_axis_fields)
                .IsUnicode(false);

            modelBuilder.Entity<processdetail>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<processdetail>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<processdetail>()
                .Property(e => e.process_name)
                .IsUnicode(false);

            modelBuilder.Entity<processfeedback>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<processfeedback>()
                .Property(e => e.form_name)
                .IsUnicode(false);

            modelBuilder.Entity<processfeedback>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<processfeedback>()
                .Property(e => e.process_name)
                .IsUnicode(false);

            modelBuilder.Entity<processfeedback>()
                .Property(e => e.questions)
                .IsUnicode(false);

            modelBuilder.Entity<processfeedback>()
                .Property(e => e.ratings)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.AgentRemarks)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.AppointmentonDesiredDayTime)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.DealerFacility)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.DealerHAClosingDate)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.DealerRemarkHA)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.Dealershipcleanliness)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.EngineType)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.FIRFT)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.HAAgentremarks)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.Hotalertageing)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.Model)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.OverallDelieveryExperience)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.PhoneNo)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.ReadyWithinPromisedTime)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.ReviewDone)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.RootCause)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.RootCause1)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.Rootcause2)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.ServiceAdvisorName)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.ServiceAdvisorPerformance)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.ServiceExperience)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.ServiceInitiation)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.ServiceTechnicianName)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.SubDisposition)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.VehicleCleanliness)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.VehicleReadinessStatus)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.VehicleSaleDate)
                .IsUnicode(false);

            modelBuilder.Entity<psf_qt_history>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<psfassignedinteraction>()
                .Property(e => e.callMade)
                .IsUnicode(false);

            modelBuilder.Entity<psfassignedinteraction>()
                .Property(e => e.interactionType)
                .IsUnicode(false);

            modelBuilder.Entity<psfassignedinteraction>()
                .Property(e => e.lastDisposition)
                .IsUnicode(false);

            modelBuilder.Entity<psfassignedinteraction>()
                .HasMany(e => e.callinteractions)
                .WithOptional(e => e.psfassignedinteraction)
                .HasForeignKey(e => e.psfAssignedInteraction_id);

            modelBuilder.Entity<psfassignedinteraction>()
                .HasMany(e => e.serviceadvisorinteractions)
                .WithOptional(e => e.psfassignedinteraction)
                .HasForeignKey(e => e.psfAssignedInteraction_id);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.assignedCRE)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.rate_dissatisfied_followup)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.rate_ready_promise_time)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.rate_fixing_concern)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.creName)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.callTime)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.isCallinitaited)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.callType)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.callDuration)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.ringTime)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.psfCallingDayType)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.chassisNo)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.variant)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.vehicleRegNo)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.commercialVehicle)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.customerName)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.preffered_address)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.prefferedPhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.Workshop)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.serviceType)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.lastServiceMeterReading)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.saName)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.jobCardNumber)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.Serviced_location)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.PrimaryDisposition)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.SecondaryDisposition)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.isContacted)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.psfFollowUpTime)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.IsSatisfied)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.HasCreExplainedReferAndEarnPRGM)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.ratingsForRepairAndMaintainance)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.ratingsForCleanlinessofCar)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.ratingsExplanationBySa)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.ratingsForPickUpOfCar)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.RatingsForCleanlinessOfDealerFacility)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.ratingsForAllinteraction)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.availedPickUpGivenByDealerfrom)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.IsFeedbackTaken)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.remarksForDissatisFaction)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.DissatisfactionAssignedTo)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.dissatisFiedFunction)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.IsCarPerformingWell)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.psfAppointmentTime)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.driverName)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.psfAddressForVisit)
                .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.mahindra_satisfied_with_QOS)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.mahindra_areaOfImprovementNeededIn)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.mahindra_SubReasonAreaOfImprovementNeededIn)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.mahindra_SubReasonOfAreaOfImprovement1)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.mahindra_SubReasonOfAreaOfImprovement2)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.mahindra_SubReasonOfAreaOfImprovement3)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.mahindra_isThatRightTimeToTalk)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.mahindra_isThatRightTimeToEnquireOrderbillDate)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.mahindra_ratingsForOverAllAppointmentProcess)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.mahindra_ratingsForTimeTakenVehiclehandOver)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.mahindra_ratingsForServicAdvCourtesyResponse)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.mahindra_ratingsForServiceAdvJobExpl)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.mahindra_ratingsForCleanlinessOfDealer)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.mahindra_ratingsForTimelinessVehicleDelivary)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.mahindra_ratingsForFairnessOfCharge)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.mahindra_ratingsForHelpFulnessOfStaff)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.mahindra_ratingsForTotalTimeRequired)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.mahindra_ratingsForQualityOfMaintenance)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.mahindra_ratingsForCondAndCleanlinessOfVeh)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.mahindra_ratingsForOverAllServExp)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.commercial_OverallServiceExp)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.commercial_HappyWithRepairJob)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.commercial_CoutnessHelpfulnessOfSA)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.commercial_TimeTakenCompleteJob)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.commercial_CostEstimateAdherance)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.commercial_ServiceConvinenantTime)
            //    .IsUnicode(false);

            //modelBuilder.Entity<psfcallhistorycube>()
            //    .Property(e => e.commercial_LikeToRevistDealer)
            //    .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.Fq1_Overall_Service_Experience)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.Fq2_Rate_Service_Advisor)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.Fq3_Rate_Vehicle_Clean_And_Tidy)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.Fq4_Rate_Dealer_Facility)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.Fq5_Rate_Dealer_Clean_And_Tidy)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.Fq6_Vehicle_Is_Ready_Ontime)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.Fq7_Rate_Overall_delivery_Experience)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.Fq8_First_Right_First_Time)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.Fq9_Did_you_Got_Service_Appinrment_Ontime)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.Fq10_Rate_Service_Initiation)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.Fq11_Updated_On_vehicleReadiness_Status)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.Fq12_Was_Review_Done_For_Your_Vehicle)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.Fq13_Rate_Overall_Timetaken_For_Service)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.Fq14_Informed_About_Your_Next_Appointment)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.Fq15_Rate_Quality_of_work)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.Fq16_Job_explanation_Provided)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.is_Upsell_available)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.filepath)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.makeCallFrom)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.dailedNumber)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.Fq20_free_pickup_drop)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.rate_free_pickup_drop)
                .IsUnicode(false);

            modelBuilder.Entity<psfcallhistorycube>()
                .Property(e => e.cremanager)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.OtherComments)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.PSFDispositon)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.afterServiceComments)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.afterServiceSatisfication)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.chargesInfoExplainedBeforeService)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.demandedRequestDone)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.detailsOfProblemsInPSFList)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.feedbackrating)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.isAppointmentGotAsDesired)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.isAppointmentReceviedAsReq)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.isCallRecievedBeforeVehWorkshop)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.isChargesAndRepairAsMentioned)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.isChargesAsEstimated)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.isContacted)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.isDeliveredAsPromisedTime)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.isDemandOfSeriviceDoneInLastVisit)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.isFollowUpDone)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.isFollowupRequired)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.isPickUpDropStaffCourteous)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.isProblemFixedInFirstVisit)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.isQualityOfPickUpDone)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.isSatisfiedWithServiceProvided)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.isTimeTakenForServiceReasonable)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.isVehicleReadyAsPromisedDate)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.isVisitedWorkshopBeforeAppoint)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.iscallMadeOneDayBeforeService)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.iscallMadeToFixPickUptime)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.isworkshopStaffCourteous)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.modeOfService)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.modeOfServiceDone)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.psfFollowUpTime)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.ratePerformanceOfSA)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.reasonOfDissatification)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.recentServiceComments)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.recentServiceSatisfication)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.remarks)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.satisfiedWithQualityOfDoorService)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.satisfiedWithWashing)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.selfDriveInFeedBack)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.specificDetailsOfComplaint)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.specificDetailsOfVisitNotCompleted)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.timeOfVisit)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.vehiclePerformance)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.vehiclePerformanceAfterService)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.workDoneOnVehiandServExpAtTimeOfDeli)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.q15_explainedReferAndEarnPRGM)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.q14_availedPickUpGivenByDealer)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.q16_RateCleanlinessOfDealerFacility)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.q17_RateMaintenanceAndRepair)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.psfAddressForVisit)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.psfAppointmentTime)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.psfPickUpAddressVisit)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.psfPickUpFromTime)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.psfPickUpToTime)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qM1_SatisfiedWithQualityOfServ)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qM2_ReasonOfAreaOfImprovement)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qM3_SubReasonOfAreaOfImprovement)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qM4_confirmingCustomer)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.psfDriverName)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qM3_SubReasonOfAreaOfImprovement1)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qM3_SubReasonOfAreaOfImprovement2)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qM3_SubReasonOfAreaOfImprovement3)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qM4_confirmingCustomerRightTime)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qM4_RightTimeToEnquireOrderbillDate)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qM10RatingQualityOfMaintenance)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qM11RatingCondAndCleanlinessOfVeh)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qM12RatingOverAllServExp)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qM1RatingOverAllAppointProcess)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qM2RatingTimeTakenVehiclehandOver)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qM3RatingServicAdvCourtesyResponse)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qM4RatingServiceAdvJobExpl)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qM5RatingCleanlinessOfDealer)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qM6RatingTimelinessVehicleDelivary)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qM7RatingFairnessOfCharge)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qM8RatingHelpFulnessOfStaff)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qM9RatingTotalTimeRequired)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qMC1_OverallServiceExp)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qMC2_HappyWithRepairJob)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qMC3_CoutnessHelpfulnessOfSA)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qMC4_TimeTakenCompleteJob)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qMC5_CostEstimateAdherance)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qMC6_ServiceConvinenantTime)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qMC7_LikeToRevistDealer)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.q2_InconvinenceDSRFunction)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.q1_CompleteSatisfication)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.q3_InconvinenceDSRAssignedTo)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.q4_InconvinenceDSRRemarks)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.q10_FeedbackAssignedTo)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.q11_FeedbackRemarks)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.q5_RateExplanationOfSA)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.q6_RatePickUpProcessOfCar)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.q7_RateCleanlinessOfCar)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.q8_RateOverAllInteraction)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.q9_FeedbackFunction)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.q12_FeedbackTaken)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.q13_IsCarPerformingWell)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qFordQ1)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qFordQ10)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qFordQ11)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qFordQ12)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qFordQ13)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qFordQ14)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qFordQ15)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qFordQ16)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qFordQ2)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qFordQ3)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qFordQ4)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qFordQ5)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qFordQ6)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qFordQ7)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qFordQ8)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qFordQ9)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qFordQ17)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qFordQ18)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qFordQ19)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qFordQ20)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .Property(e => e.qFordQ21)
                .IsUnicode(false);

            modelBuilder.Entity<psfinteraction>()
                .HasMany(e => e.upsellleads)
                .WithOptional(e => e.psfinteraction)
                .HasForeignKey(e => e.psfInteraction_id);

            modelBuilder.Entity<renewaltype>()
                .Property(e => e.renewalTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.rono)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.regno)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.worktype)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.serviceadv)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.maintech)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.specialmsg)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.visittype)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.estimatno)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.quickservice)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.mobileservice)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.smsstatus)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.rodatetime)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.gatepasstime)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.inscompname)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.location)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.workshop_id)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.campaignFromDate)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.campaignToDate)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.PreRoadTest)
                .IsUnicode(false);

            modelBuilder.Entity<repair_order_report>()
                .Property(e => e.doorstep)
                .IsUnicode(false);

           
            modelBuilder.Entity<rework>()
                .Property(e => e.Benefits)
                .IsUnicode(false);

            modelBuilder.Entity<rework>()
                .Property(e => e.resolutionMode)
                .IsUnicode(false);

            modelBuilder.Entity<rework>()
                .Property(e => e.resolvedBy)
                .IsUnicode(false);

            modelBuilder.Entity<rework>()
                .Property(e => e.reworkAddress)
                .IsUnicode(false);

            modelBuilder.Entity<rework>()
                .Property(e => e.reworkMode)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.billNumber)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.billStatus)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.chassisNo)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.color)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.customerName)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.customerPhone)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.engineNo)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.jobCardNumber)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.jobCardStatus)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.jobcardLocation)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.lastServiceMeterReading)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.lastServiceType)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.location_name)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.menuCodeDesc)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.modelGroup)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.saName)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.serviceCategory)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.shieldexpiryDate)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.taxiNonTaxi)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.technician)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.variant)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.vehicleRegNo)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.warrantyyn)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.workshopName)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.uploadType1)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.uploadType2)
                .IsUnicode(false);

            modelBuilder.Entity<robillscube>()
                .Property(e => e.uploadType3)
                .IsUnicode(false);

            modelBuilder.Entity<role>()
                .Property(e => e.role1)
                .IsUnicode(false);

            modelBuilder.Entity<role>()
                .HasMany(e => e.userroles)
                .WithRequired(e => e.role)
                .HasForeignKey(e => e.roles_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.additionalTaxStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.address1)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.address2)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.address3)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.allotmentStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.area)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.areaType)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.basicPriceStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.buyerType)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.ceName)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.chassis)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.colorDesc)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.colorcode)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.corporate)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.csdExShowRoomStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.customerId)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.customerName)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.delDateStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.discountStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.district)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.dsa)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.dse)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.emailId)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.engine)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.enquirSubSource)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.enquiryNo)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.enquirySources)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.errorInformation)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.evaluatorMSPIN)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.exWarrantyType)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.exchCancReason)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.exchangeCancDateStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.extWarrantyStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.fuelType)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.hypAmtStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.hyp_address)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.hypthecation)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.indivScheme)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.instiCustStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.insuranceStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.invCancelDateStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.invCancelNo)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.invDateStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.invNo)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.ivnAmtStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.locCode)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.loyaltyBonusDiscStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.mgaSoldAmtStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.miDateStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.miFlag)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.miname)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.mobile1)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.mobile2)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.mulInvDateStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.mulInvNo)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.offPhone)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.oldCarMfg)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.oldCarModelCode)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.oldCarOwner)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.oldCarRegNo)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.oldCarRelation)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.oldCarStatus)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.orderDateStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.orderNum)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.outlet)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.panNoStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.picDesc)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.pincodeStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.promisedDelvDateStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.purchasePriceStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.refMobileNo)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.refNo)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.refRegNo)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.refType)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.refby)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.regNo)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.resCode)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.roadTaxStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.roundOffStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.saleType)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.salutation)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.schemeOfGoiStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.stateDesc)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.stdCode)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.stdCodeComp)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.sumStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.teamLead)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.tehsilDesc)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.tradeIn)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.varaintDesc)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.variantCode)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.vatStr)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.villageDesc)
                .IsUnicode(false);

            modelBuilder.Entity<saleregisterexcelerror>()
                .Property(e => e.year)
                .IsUnicode(false);

          
            modelBuilder.Entity<savedsearchresult>()
                .Property(e => e.savedName)
                .IsUnicode(false);

            modelBuilder.Entity<savedsearchresult>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<segment>()
                .Property(e => e.dataUploadLocation)
                .IsUnicode(false);

            modelBuilder.Entity<segment>()
                .Property(e => e.dataUploadType)
                .IsUnicode(false);

            modelBuilder.Entity<segment>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<segment>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<segment>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.billNo)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.billNumber)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.circularNo)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.createdBy)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.custCat)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.customerName)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.groupData)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.isAssigned)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.jobCardNumber)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.jobCardStatus)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.lastPSFStatus)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.lastServiceMeterReading)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.lastServiceType)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.lastVisitMeterReading)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.lastVisitMileage)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.lastVisitType)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.mcpNo)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.messageSentStatus)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.nextServiceType)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.psfStatus)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.repeat_revisit)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.selling)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.serviceOdometerReading)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.serviceType)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.technician)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.uploadDataFiletype)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.visitSuccessRate)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.billCreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.billType)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.jobcardLocation)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.saName)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.serviceCategory)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.suprevisor)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.OEMprivilageCust)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.RoSource)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.billStatus)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.campaignCode)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.dropType)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.expressService)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.loyaltyCardNo)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.menuCodeDesc)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.originalWarrantystartDate)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.uploadType1)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.uploadType2)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.uploadType3)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.pickUpType)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.purpleCouponNo)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.purpleCouponYesNo)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.rsaSchemeCode)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.shieldschemeCode)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.taxiNonTaxi)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.udayNo)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.originalWarrantyDate)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.phonenumber)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.serviceDealerName)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.defectDetails)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.laborDetails)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .Property(e => e.jobCardRemarks)
                .IsUnicode(false);

            modelBuilder.Entity<service>()
                .HasMany(e => e.psfassignedinteractions)
                .WithOptional(e => e.service)
                .HasForeignKey(e => e.service_id);

           
            modelBuilder.Entity<serviceadvisor>()
                .Property(e => e.advisorName)
                .IsUnicode(false);

            modelBuilder.Entity<serviceadvisor>()
                .Property(e => e.advisorNumber)
                .IsUnicode(false);

            modelBuilder.Entity<serviceadvisor>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<serviceadvisor>()
                .HasMany(e => e.services)
                .WithOptional(e => e.serviceadvisor)
                .HasForeignKey(e => e.serviceAdvisor_advisorId);

            modelBuilder.Entity<serviceadvisor>()
                .HasMany(e => e.servicebookeds)
                .WithOptional(e => e.serviceadvisor)
                .HasForeignKey(e => e.serviceAdvisor_advisorId);

            modelBuilder.Entity<serviceadvisorinteraction>()
                .Property(e => e.filePath)
                .IsUnicode(false);

            modelBuilder.Entity<serviceadvisorinteraction>()
                .Property(e => e.firebaseKey)
                .IsUnicode(false);

            modelBuilder.Entity<serviceadvisorinteraction>()
                .Property(e => e.interactionDate)
                .IsUnicode(false);

            modelBuilder.Entity<serviceadvisorinteraction>()
                .Property(e => e.interactionTime)
                .IsUnicode(false);

            modelBuilder.Entity<serviceadvisorinteraction>()
                .Property(e => e.latitude)
                .IsUnicode(false);

            modelBuilder.Entity<serviceadvisorinteraction>()
                .Property(e => e.longitude)
                .IsUnicode(false);

            modelBuilder.Entity<serviceadvisorinteraction>()
                .Property(e => e.mediaFile)
                .IsUnicode(false);

            modelBuilder.Entity<serviceadvisorinteraction>()
                .Property(e => e.typeOfInteraction)
                .IsUnicode(false);

            modelBuilder.Entity<serviceadvisorinteractionstatu>()
                .Property(e => e.interactionStatus)
                .IsUnicode(false);

            modelBuilder.Entity<serviceadvisorinteractionstatu>()
                .HasMany(e => e.serviceadvisorinteractions)
                .WithOptional(e => e.serviceadvisorinteractionstatu)
                .HasForeignKey(e => e.status_id);

            modelBuilder.Entity<servicebooked>()
                .Property(e => e.serviceBookedType)
                .IsUnicode(false);

            modelBuilder.Entity<servicebooked>()
                .Property(e => e.statusOfSB)
                .IsUnicode(false);

            modelBuilder.Entity<servicebooked>()
                .Property(e => e.typeOfPickup)
                .IsUnicode(false);

            modelBuilder.Entity<servicebooked>()
                .Property(e => e.serviceBookingAddress)
                .IsUnicode(false);

            modelBuilder.Entity<servicebooked>()
                .Property(e => e.pickUpAmount)
                .IsUnicode(false);

            modelBuilder.Entity<servicebooked>()
                .HasMany(e => e.callinteractions)
                .WithOptional(e => e.servicebooked)
                .HasForeignKey(e => e.serviceBooked_serviceBookedId);

            modelBuilder.Entity<servicebooked>()
                .HasMany(e => e.serviceadvisorinteractions)
                .WithOptional(e => e.servicebooked)
                .HasForeignKey(e => e.serviceBooked_serviceBookedId);

            modelBuilder.Entity<servicebookedsynced>()
                .Property(e => e.customerPhone)
                .IsUnicode(false);

            modelBuilder.Entity<servicebookedsynced>()
                .Property(e => e.smsType)
                .IsUnicode(false);

            modelBuilder.Entity<servicedoneschcall>()
                .Property(e => e.agentId)
                .IsUnicode(false);

            modelBuilder.Entity<servicedoneschcall>()
                .Property(e => e.agentName)
                .IsUnicode(false);

            modelBuilder.Entity<servicedoneschcall>()
                .Property(e => e.customerName)
                .IsUnicode(false);

            modelBuilder.Entity<servicedoneschcall>()
                .Property(e => e.customerPhone)
                .IsUnicode(false);

            modelBuilder.Entity<servicedoneschcall>()
                .Property(e => e.dealerCode)
                .IsUnicode(false);

            modelBuilder.Entity<servicedoneschcall>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<servicedoneschcall>()
                .Property(e => e.syncedToFireBaseServiceBooked)
                .IsUnicode(false);

            modelBuilder.Entity<servicedoneschcall>()
                .Property(e => e.vehicalRegNo)
                .IsUnicode(false);

            modelBuilder.Entity<servicedoneschcall>()
                .Property(e => e.vehicleNumber)
                .IsUnicode(false);

            modelBuilder.Entity<servicess>()
                .Property(e => e.customerName)
                .IsUnicode(false);

            modelBuilder.Entity<servicess>()
                .Property(e => e.jobCardNumber)
                .IsUnicode(false);

            modelBuilder.Entity<servicess>()
                .Property(e => e.lastServiceMeterReading)
                .IsUnicode(false);

            modelBuilder.Entity<servicess>()
                .Property(e => e.serviceOdometerReading)
                .IsUnicode(false);

            modelBuilder.Entity<servicess>()
                .Property(e => e.serviceType)
                .IsUnicode(false);

            modelBuilder.Entity<servicess>()
                .Property(e => e.jobcardLocation)
                .IsUnicode(false);

            modelBuilder.Entity<servicess>()
                .Property(e => e.saName)
                .IsUnicode(false);

            modelBuilder.Entity<servicess>()
                .Property(e => e.serviceCategory)
                .IsUnicode(false);

            modelBuilder.Entity<servicess>()
                .Property(e => e.menuCodeDesc)
                .IsUnicode(false);

            modelBuilder.Entity<servicetype>()
                .Property(e => e.serviceTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<servicetype>()
                .HasMany(e => e.servicebookeds)
                .WithOptional(e => e.servicetype)
                .HasForeignKey(e => e.serviceBookType_id);

            modelBuilder.Entity<servicetype>()
                .HasMany(e => e.servicebookeds1)
                .WithOptional(e => e.servicetype1)
                .HasForeignKey(e => e.serviceBookType_id);

            modelBuilder.Entity<sfotemp>()
                .Property(e => e.bankName)
                .IsUnicode(false);

            modelBuilder.Entity<sfotemp>()
                .Property(e => e.category)
                .IsUnicode(false);

            modelBuilder.Entity<sfotemp>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<sfotemp>()
                .Property(e => e.data_type)
                .IsUnicode(false);

            modelBuilder.Entity<sfotemp>()
                .Property(e => e.excel_column)
                .IsUnicode(false);

            modelBuilder.Entity<sfotemp>()
                .Property(e => e.model_column)
                .IsUnicode(false);

            modelBuilder.Entity<sfotemp>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<sfotemp>()
                .Property(e => e.subCategory)
                .IsUnicode(false);

            modelBuilder.Entity<showroom>()
                .Property(e => e.showroomName)
                .IsUnicode(false);

            modelBuilder.Entity<showroom>()
                .HasMany(e => e.appointmentbookeds)
                .WithOptional(e => e.showroom)
                .HasForeignKey(e => e.showRooms_showRoom_id);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.chassis)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.currentJC)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.custCat)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.custContactStatus)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.custEmailIdStatus)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.custName)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.custPickUpType)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.deliveryDate)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.dueDate)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.dueService)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.errorInformation)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.extWarranty)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.followUpNew)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.followUpNum)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.followUpOld)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.followUpRemarks)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.followUpResponse)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.followUpType)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.fuelType)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.lastServiceDate)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.lastServiceType)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.lastfollowUpRemarks)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.mileage)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.mobile)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.nextFollowupDate)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.pickupDrop)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.regNo)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.saleDate)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.serviceType)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.telephone)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.deliveryDateStr)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.dueDateStr)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.followUpNewStr)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.followUpOldStr)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.lastServiceDateStr)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.mileageStr)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.saleDateStr)
                .IsUnicode(false);

            modelBuilder.Entity<smrdataerror>()
                .Property(e => e.upload_id)
                .IsUnicode(false);


            modelBuilder.Entity<smrforecasteddata>()
                .Property(e => e.chassisnumber)
                .IsUnicode(false);

            modelBuilder.Entity<smrforecasteddata>()
                .Property(e => e.customername)
                .IsUnicode(false);

            modelBuilder.Entity<smrforecasteddata>()
                .Property(e => e.locationname)
                .IsUnicode(false);

            modelBuilder.Entity<smrforecasteddata>()
                .Property(e => e.workshopname)
                .IsUnicode(false);

            modelBuilder.Entity<smrforecasteddata>()
                .Property(e => e.nextservicetype)
                .IsUnicode(false);

            modelBuilder.Entity<smrforecasteddata>()
                .Property(e => e.forecastlogic)
                .IsUnicode(false);

            modelBuilder.Entity<smrforecasteddata>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<smrforecasteddata>()
                .Property(e => e.loststatus)
                .IsUnicode(false);

            modelBuilder.Entity<smrforecasteddata>()
                .Property(e => e.phonenumber)
                .IsUnicode(false);

            modelBuilder.Entity<smrforecasteddata>()
                .Property(e => e.vehicleregnum)
                .IsUnicode(false);

            modelBuilder.Entity<smrforecasteddata>()
                .Property(e => e.lastServicetype)
                .IsUnicode(false);

            modelBuilder.Entity<smrforecasteddata>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<sms_upload>()
                .Property(e => e.customerCategory)
                .IsUnicode(false);

            modelBuilder.Entity<sms_upload>()
                .Property(e => e.customerName)
                .IsUnicode(false);

            modelBuilder.Entity<sms_upload>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<sms_upload>()
                .Property(e => e.phoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<sms_upload>()
                .Property(e => e.variant)
                .IsUnicode(false);

            modelBuilder.Entity<sms_upload>()
                .Property(e => e.vehicleRegNo)
                .IsUnicode(false);

            modelBuilder.Entity<sms_upload>()
                .Property(e => e.campaignName)
                .IsUnicode(false);

            modelBuilder.Entity<sms_upload>()
                .Property(e => e.workshop_id)
                .IsUnicode(false);

            modelBuilder.Entity<sms_upload>()
                .Property(e => e.location)
                .IsUnicode(false);

            modelBuilder.Entity<sms_upload>()
                .Property(e => e.campaignFromDate)
                .IsUnicode(false);

            modelBuilder.Entity<sms_upload>()
                .Property(e => e.campaignToDate)
                .IsUnicode(false);

            modelBuilder.Entity<sms_upload>()
                .Property(e => e.chassisno)
                .IsUnicode(false);

            modelBuilder.Entity<smsinteraction>()
                .Property(e => e.interactionDate)
                .IsUnicode(false);

            modelBuilder.Entity<smsinteraction>()
                .Property(e => e.interactionTime)
                .IsUnicode(false);

            modelBuilder.Entity<smsinteraction>()
                .Property(e => e.interactionType)
                .IsUnicode(false);

            modelBuilder.Entity<smsinteraction>()
                .Property(e => e.responseFromGateway)
                .IsUnicode(false);

            modelBuilder.Entity<smsinteraction>()
                .Property(e => e.smsHeader)
                .IsUnicode(false);

            modelBuilder.Entity<smsinteraction>()
                .Property(e => e.smsMessage)
                .IsUnicode(false);

            modelBuilder.Entity<smsinteraction>()
                .Property(e => e.mobileNumber)
                .IsUnicode(false);

            modelBuilder.Entity<smsinteraction>()
                .Property(e => e.reason)
                .IsUnicode(false);

            modelBuilder.Entity<smsinteraction>()
                .Property(e => e.smsType)
                .IsUnicode(false);

            modelBuilder.Entity<smsparameter>()
                .Property(e => e.message)
                .IsUnicode(false);

            modelBuilder.Entity<smsparameter>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<smsparameter>()
                .Property(e => e.senderid)
                .IsUnicode(false);

            modelBuilder.Entity<smsparameter>()
                .Property(e => e.sucessStatus)
                .IsUnicode(false);

            modelBuilder.Entity<smsstatu>()
                .Property(e => e.code)
                .IsUnicode(false);

            modelBuilder.Entity<smsstatu>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<smstemplate>()
                .Property(e => e.smsAPI)
                .IsUnicode(false);

            modelBuilder.Entity<smstemplate>()
                .Property(e => e.smsTemplate1)
                .IsUnicode(false);

            modelBuilder.Entity<smstemplate>()
                .Property(e => e.smsType)
                .IsUnicode(false);

            modelBuilder.Entity<smstemplate>()
                .Property(e => e.dealerName)
                .IsUnicode(false);

            modelBuilder.Entity<smstemplate>()
                .Property(e => e.dealer)
                .IsUnicode(false);

            modelBuilder.Entity<smstemplate>()
                .Property(e => e.locationId)
                .IsUnicode(false);

            modelBuilder.Entity<smstemplate>()
                .Property(e => e.deliveryType)
                .IsUnicode(false);

            modelBuilder.Entity<specialoffermaster>()
                .Property(e => e.offerBenefit)
                .IsUnicode(false);

            modelBuilder.Entity<specialoffermaster>()
                .Property(e => e.offerCode)
                .IsUnicode(false);

            modelBuilder.Entity<specialoffermaster>()
                .Property(e => e.offerConditions)
                .IsUnicode(false);

            modelBuilder.Entity<specialoffermaster>()
                .Property(e => e.offerDetails)
                .IsUnicode(false);

            modelBuilder.Entity<specialoffermaster>()
                .HasMany(e => e.servicebookeds)
                .WithOptional(e => e.specialoffermaster)
                .HasForeignKey(e => e.specialOfferMaster_id);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.DistancefromDealerLocationRadio)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.InOutCallName)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.ServicedAtOtherDealerRadio)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.assignedToSA)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.checkedwithDMS)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.cityName)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.complaintOrFB_TagTo)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.dateOfService)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.dealerName)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.departmentForFB)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.followUpTime)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.inBoundLeadSource)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.isFollowUpDone)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.isFollowupRequired)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.mileageAsOnDate)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.mileageAtService)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.noServiceReason)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.noServiceReasonTaggedTo)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.noServiceReasonTaggedToComments)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.optforMMS)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.other)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.pinCodeOfCity)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.reason)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.reasonForHTML)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.remarksOfFB)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.serviceAdvisorID)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.serviceLocation)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.serviceType)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.transferedCity)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.typeOfDisposition)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.remarks)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .Property(e => e.cityOfDistanceFromDealerLocation)
                .IsUnicode(false);

            modelBuilder.Entity<srdisposition>()
                .HasMany(e => e.upsellleads)
                .WithOptional(e => e.srdisposition)
                .HasForeignKey(e => e.srDisposition_id);

            modelBuilder.Entity<stakeholdemail>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<stakeholdemail>()
                .Property(e => e.attachmentName)
                .IsUnicode(false);

            modelBuilder.Entity<stakeholdemail>()
                .Property(e => e.ToEmail_id)
                .IsUnicode(false);

            modelBuilder.Entity<stakeholdemail>()
                .Property(e => e.Cc)
                .IsUnicode(false);

            modelBuilder.Entity<stakeholdemail>()
                .Property(e => e.Subject)
                .IsUnicode(false);

            modelBuilder.Entity<stakeholdemail>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<stakeholdemail>()
                .Property(e => e.EmailTime)
                .IsUnicode(false);

            modelBuilder.Entity<storeobjtoblob>()
                .Property(e => e.modelName)
                .IsUnicode(false);

            modelBuilder.Entity<synchedkey>()
                .Property(e => e.firebaseKey)
                .IsUnicode(false);

            modelBuilder.Entity<synchedkey>()
                .Property(e => e.onMorekey)
                .IsUnicode(false);

            modelBuilder.Entity<synchedkeyschcall>()
                .Property(e => e.firebaseKey)
                .IsUnicode(false);

            modelBuilder.Entity<synchedkeyschcall>()
                .Property(e => e.onMorekey)
                .IsUnicode(false);

            modelBuilder.Entity<tagginguser>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<tagginguser>()
                .Property(e => e.phoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<tagginguser>()
                .Property(e => e.upsellType)
                .IsUnicode(false);

            modelBuilder.Entity<tagginguser>()
                .HasMany(e => e.upsellleads)
                .WithOptional(e => e.tagginguser)
                .HasForeignKey(e => e.taggingUsers_id);

            modelBuilder.Entity<tenant>()
                .Property(e => e.tenantCode)
                .IsUnicode(false);

            modelBuilder.Entity<tenant>()
                .Property(e => e.tenantPhone)
                .IsUnicode(false);

            modelBuilder.Entity<tenant>()
                .Property(e => e.tenentAddress)
                .IsUnicode(false);

            modelBuilder.Entity<tenant>()
                .Property(e => e.apkURLPath)
                .IsUnicode(false);

            modelBuilder.Entity<tenant>()
                .Property(e => e.appVersion)
                .IsUnicode(false);

            modelBuilder.Entity<tenant>()
                .HasMany(e => e.wyzusers)
                .WithOptional(e => e.tenant)
                .HasForeignKey(e => e.tenant_id);

            modelBuilder.Entity<testformcreation>()
                .Property(e => e.answers1)
                .IsUnicode(false);

            modelBuilder.Entity<testformcreation>()
                .Property(e => e.answers2)
                .IsUnicode(false);

            modelBuilder.Entity<testformcreation>()
                .Property(e => e.answers3)
                .IsUnicode(false);

            modelBuilder.Entity<testformcreation>()
                .Property(e => e.answers4)
                .IsUnicode(false);

            modelBuilder.Entity<testformcreation>()
                .Property(e => e.answers5)
                .IsUnicode(false);

            modelBuilder.Entity<testformcreation>()
                .Property(e => e.correct_answer)
                .IsUnicode(false);

            modelBuilder.Entity<testformcreation>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<testformcreation>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<testformcreation>()
                .Property(e => e.process_name)
                .IsUnicode(false);

            modelBuilder.Entity<testformcreation>()
                .Property(e => e.questions)
                .IsUnicode(false);

            modelBuilder.Entity<testformcreation>()
                .Property(e => e.test_form_name)
                .IsUnicode(false);

            modelBuilder.Entity<testscore>()
                .Property(e => e.answer_selected)
                .IsUnicode(false);

            modelBuilder.Entity<testscore>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<testscore>()
                .Property(e => e.fromDate)
                .IsUnicode(false);

            modelBuilder.Entity<testscore>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<testscore>()
                .Property(e => e.process_name)
                .IsUnicode(false);

            modelBuilder.Entity<testscore>()
                .Property(e => e.questions)
                .IsUnicode(false);

            modelBuilder.Entity<testscore>()
                .Property(e => e.testTakenBy)
                .IsUnicode(false);

            modelBuilder.Entity<testscore>()
                .Property(e => e.test_form_name)
                .IsUnicode(false);

            modelBuilder.Entity<testscore>()
                .Property(e => e.tested_by)
                .IsUnicode(false);

            modelBuilder.Entity<testscore>()
                .Property(e => e.toDate)
                .IsUnicode(false);

            modelBuilder.Entity<tl_day_wise_data>()
                .Property(e => e.team_leader)
                .IsUnicode(false);

            modelBuilder.Entity<tl_day_wise_data>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<tl_day_wise_data>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<tl_monthwise_data>()
                .Property(e => e.Aspect_ID)
                .IsUnicode(false);

            modelBuilder.Entity<tl_monthwise_data>()
                .Property(e => e.Team_Leader)
                .IsUnicode(false);

            modelBuilder.Entity<tl_monthwise_data>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<tl_monthwise_data>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<unavailability>()
                .Property(e => e.year)
                .IsUnicode(false);

           
            modelBuilder.Entity<upload>()
                .Property(e => e.discardedEntriesPath)
                .IsUnicode(false);

            modelBuilder.Entity<upload>()
                .Property(e => e.error)
                .IsUnicode(false);

            modelBuilder.Entity<upload>()
                .Property(e => e.fileName)
                .IsUnicode(false);

            modelBuilder.Entity<upload>()
                .Property(e => e.processingStatus)
                .IsUnicode(false);

            modelBuilder.Entity<upload>()
                .Property(e => e.uploadPath)
                .IsUnicode(false);

            modelBuilder.Entity<upload>()
                .HasMany(e => e.formdatas)
                .WithOptional(e => e.upload)
                .HasForeignKey(e => e.upload_id);

            modelBuilder.Entity<uploaddata>()
                .Property(e => e.campaignExpiry)
                .IsUnicode(false);

            modelBuilder.Entity<uploaddata>()
                .Property(e => e.campaignName)
                .IsUnicode(false);

            modelBuilder.Entity<uploaddata>()
                .Property(e => e.campaignStartdate)
                .IsUnicode(false);

            modelBuilder.Entity<uploaddata>()
                .Property(e => e.dataType)
                .IsUnicode(false);

            modelBuilder.Entity<uploaddata>()
                .Property(e => e.fileNametimeStamp)
                .IsUnicode(false);

            modelBuilder.Entity<uploaddata>()
                .Property(e => e.sheetName)
                .IsUnicode(false);

            modelBuilder.Entity<uploaddata>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<uploadmasterformat>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<uploadmasterformat>()
                .Property(e => e.data_type)
                .IsUnicode(false);

            modelBuilder.Entity<uploadmasterformat>()
                .Property(e => e.excel_column)
                .IsUnicode(false);

            modelBuilder.Entity<uploadmasterformat>()
                .Property(e => e.model_column)
                .IsUnicode(false);

            modelBuilder.Entity<uploadmasterformat>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<uploadmasterformat>()
                .Property(e => e.upload_format_name)
                .IsUnicode(false);

            modelBuilder.Entity<uploadreportfile>()
                .Property(e => e.fileId)
                .IsUnicode(false);

            modelBuilder.Entity<uploadreportfile>()
                .Property(e => e.fileName)
                .IsUnicode(false);

            modelBuilder.Entity<uploadreportname>()
                .Property(e => e.ReportName)
                .IsUnicode(false);

            modelBuilder.Entity<uploadreportname>()
                .Property(e => e.ReportFilterName)
                .IsUnicode(false);

            modelBuilder.Entity<uploadreportname>()
                .Property(e => e.OEM)
                .IsUnicode(false);

            modelBuilder.Entity<uploadreportname>()
                .Property(e => e.UploadType)
                .IsUnicode(false);

            modelBuilder.Entity<uploadtype>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<uploadtype>()
                .Property(e => e.rowsToignore)
                .IsUnicode(false);

            modelBuilder.Entity<uploadtype>()
                .Property(e => e.uploadDisplayName)
                .IsUnicode(false);

            modelBuilder.Entity<uploadtype>()
                .Property(e => e.uploadTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<uploadtype>()
                .Property(e => e.uploadextensions)
                .IsUnicode(false);

            modelBuilder.Entity<uploadtype>()
                .HasMany(e => e.uploads)
                .WithOptional(e => e.uploadtype)
                .HasForeignKey(e => e.uploadType_id);

            modelBuilder.Entity<upselllead>()
                .Property(e => e.taggedTo)
                .IsUnicode(false);

            modelBuilder.Entity<upselllead>()
                .Property(e => e.upSellType)
                .IsUnicode(false);

            modelBuilder.Entity<upselllead>()
                .Property(e => e.upsellComments)
                .IsUnicode(false);

            modelBuilder.Entity<usercreation>()
                .Property(e => e.udealerName)
                .IsUnicode(false);

            modelBuilder.Entity<usercreation>()
                .Property(e => e.uemail)
                .IsUnicode(false);

            modelBuilder.Entity<usercreation>()
                .Property(e => e.ufirstName)
                .IsUnicode(false);

            modelBuilder.Entity<usercreation>()
                .Property(e => e.ulastName)
                .IsUnicode(false);

            modelBuilder.Entity<usercreation>()
                .Property(e => e.upassword)
                .IsUnicode(false);

            modelBuilder.Entity<usercreation>()
                .Property(e => e.urole)
                .IsUnicode(false);

            modelBuilder.Entity<usersession>()
                .Property(e => e.ipAddress)
                .IsUnicode(false);

            modelBuilder.Entity<usersession>()
                .Property(e => e.userLogined)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.Variant_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.cancel_No)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.chassisNo)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.clr_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.color)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.colorCode)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.customerId)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.dealershipName)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.district)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.dsa)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.dse)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.email_Id)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.engineNo)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.enquiryNo)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.enquirySource)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.evaluator_name_MSPIN)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.ewType)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.exch_Canc_Reason)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.exchange)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.extWarrantyType)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.extendedWarentyDue)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.forecastLogic)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.fuelType)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.hyp_Address)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.hypo)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.isAssigned)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.jobCardNumber)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.lastServiceType)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.miFlag)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.miName)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.mobile1)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.mobile2)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.mul_Inv_No)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.nextServicetype)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.odometerReading)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.off_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.oldCar_regNum)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.old_Car_Relation)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.oldcar_Manufacturer)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.oldcar_Owner)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.oldcar_Status)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.oldcar_modelCode)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.pin_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.pincode)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.refVehicle_regnNo)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.ref_MobileNo)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.referenceType)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.referredBy)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.refferenceNo)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.res_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.salesChannel)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.stateDesc)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.stdCode_comp)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.std_code)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.teamLead)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.tehsilDesc)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.variant)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.variantCode)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.vehicleNumber)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.vehicleRegNo)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.villageDesc)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.forecasted_duetype)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.system_forecast_logic)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.currentPriority)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.delivaryNoteNumber)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.finanaceBranch)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.financier)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.initialPriority)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.kitNo)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.modelFamilyCode)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.modelGroup)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.oTFNo)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.oemInvoiceNo)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.priceType)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.nextRenewalType)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.insuranceCompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.branchName)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.corporateType)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.typeOfProgram)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.regno_chassisNo)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.averageRunning)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.brand)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.insfcupload_id)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.hotAlertOpenDate)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.contractEndDate)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.esbProduct)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.isSalesDone)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.systemDueType)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .Property(e => e.vipDealer)
                .IsUnicode(false);

            modelBuilder.Entity<vehicle>()
                .HasMany(e => e.assignedinteractions)
                .WithOptional(e => e.vehicle)
                .HasForeignKey(e => e.vehical_Id);

            modelBuilder.Entity<vehicle>()
                .HasMany(e => e.callinteractions)
                .WithOptional(e => e.vehicle)
                .HasForeignKey(e => e.vehicle_vehicle_id);

            modelBuilder.Entity<vehicle>()
                .HasMany(e => e.complaints)
                .WithOptional(e => e.vehicle)
                .HasForeignKey(e => e.vehicle_vehicle_id);

            modelBuilder.Entity<vehicle>()
                .HasMany(e => e.complaintinteractions)
                .WithOptional(e => e.vehicle)
                .HasForeignKey(e => e.vehicle_vehicle_id);

            modelBuilder.Entity<vehicle>()
                .HasMany(e => e.driverinteractions)
                .WithOptional(e => e.vehicle)
                .HasForeignKey(e => e.vehicle_vehicle_id);

            modelBuilder.Entity<vehicle>()
                .HasMany(e => e.insuranceassignedinteractions)
                .WithOptional(e => e.vehicle)
                .HasForeignKey(e => e.vehicle_vehicle_id);

            modelBuilder.Entity<vehicle>()
                .HasMany(e => e.psfassignedinteractions)
                .WithOptional(e => e.vehicle)
                .HasForeignKey(e => e.vehicle_vehicle_id);

            modelBuilder.Entity<vehicle>()
                .HasMany(e => e.services)
                .WithOptional(e => e.vehicle)
                .HasForeignKey(e => e.vehicle_vehicle_id);

            modelBuilder.Entity<vehicle>()
                .HasMany(e => e.serviceadvisorinteractions)
                .WithOptional(e => e.vehicle)
                .HasForeignKey(e => e.vehicle_vehicle_id);

            modelBuilder.Entity<vehicle>()
                .HasMany(e => e.servicebookeds)
                .WithOptional(e => e.vehicle)
                .HasForeignKey(e => e.vehicle_vehicle_id);

            modelBuilder.Entity<vehicle>()
                .HasMany(e => e.smsinteractions)
                .WithOptional(e => e.vehicle)
                .HasForeignKey(e => e.vehicle_vehicle_id);

            modelBuilder.Entity<vehicle>()
                .HasMany(e => e.upsellleads)
                .WithOptional(e => e.vehicle)
                .HasForeignKey(e => e.vehicle_vehicle_id);

            modelBuilder.Entity<vehiclesummarycube>()
                .Property(e => e.ChassisNo)
                .IsUnicode(false);

            modelBuilder.Entity<vehiclesummarycube>()
                .Property(e => e.assignedCre)
                .IsUnicode(false);

            modelBuilder.Entity<vehiclesummarycube>()
                .Property(e => e.attempts)
                .IsUnicode(false);

            modelBuilder.Entity<vehiclesummarycube>()
                .Property(e => e.bookingStatus)
                .IsUnicode(false);

            modelBuilder.Entity<vehiclesummarycube>()
                .Property(e => e.bookingType)
                .IsUnicode(false);

            modelBuilder.Entity<vehiclesummarycube>()
                .Property(e => e.callMade)
                .IsUnicode(false);

            modelBuilder.Entity<vehiclesummarycube>()
                .Property(e => e.callStatus)
                .IsUnicode(false);

            modelBuilder.Entity<vehiclesummarycube>()
                .Property(e => e.campaign)
                .IsUnicode(false);

            modelBuilder.Entity<vehiclesummarycube>()
                .Property(e => e.customerName)
                .IsUnicode(false);

            modelBuilder.Entity<vehiclesummarycube>()
                .Property(e => e.firstCallDipso)
                .IsUnicode(false);

            modelBuilder.Entity<vehiclesummarycube>()
                .Property(e => e.isBookingDone)
                .IsUnicode(false);

            modelBuilder.Entity<vehiclesummarycube>()
                .Property(e => e.isRemoval)
                .IsUnicode(false);

            modelBuilder.Entity<vehiclesummarycube>()
                .Property(e => e.lastCallCre)
                .IsUnicode(false);

            modelBuilder.Entity<vehiclesummarycube>()
                .Property(e => e.lastCallDispoition)
                .IsUnicode(false);

            modelBuilder.Entity<vehiclesummarycube>()
                .Property(e => e.phoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<vehiclesummarycube>()
                .Property(e => e.repairOrderNo)
                .IsUnicode(false);

            modelBuilder.Entity<vehiclesummarycube>()
                .Property(e => e.reportedStatus)
                .IsUnicode(false);

            modelBuilder.Entity<vehiclesummarycube>()
                .Property(e => e.serviceDueType)
                .IsUnicode(false);

            modelBuilder.Entity<vehiclesummarycube>()
                .Property(e => e.updated_date)
                .IsUnicode(false);

            modelBuilder.Entity<vehiclesummarycube>()
                .Property(e => e.vehicleRegNo)
                .IsUnicode(false);

            modelBuilder.Entity<vehiclesummarycube>()
                .Property(e => e.whenRemoved)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.beforematchingStr)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.billAmtStr)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.billDateStr)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.billDateoldStr)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.billNo)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.custId)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.errorInformation)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.jobCardNo)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.labourBasicStr)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.labourChargeStr)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.labourDiscStr)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.location)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.mcpNo)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.monthStr)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.partBasicStr)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.partChargesStr)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.partDiscStr)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.partyName)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.regno)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.roundOffAmtStr)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.servicetype)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.slNo)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<workbillregistererror>()
                .Property(e => e.yearStr)
                .IsUnicode(false);

            modelBuilder.Entity<workshop>()
                .Property(e => e.pincode)
                .IsUnicode(false);

            modelBuilder.Entity<workshop>()
                .Property(e => e.workshopAddress)
                .IsUnicode(false);

            modelBuilder.Entity<workshop>()
                .Property(e => e.workshopCode)
                .IsUnicode(false);

            modelBuilder.Entity<workshop>()
                .Property(e => e.workshopName)
                .IsUnicode(false);

            modelBuilder.Entity<workshop>()
                .Property(e => e.upload_id)
                .IsUnicode(false);

            modelBuilder.Entity<workshop>()
                .HasMany(e => e.bays)
                .WithOptional(e => e.workshop)
                .HasForeignKey(e => e.workshop_id);

            modelBuilder.Entity<workshop>()
                .HasMany(e => e.campaigns)
                .WithOptional(e => e.workshop)
                .HasForeignKey(e => e.workshop_id);

            modelBuilder.Entity<workshop>()
                .HasMany(e => e.complaintinteractions)
                .WithOptional(e => e.workshop)
                .HasForeignKey(e => e.workshop_id);

            modelBuilder.Entity<workshop>()
                .HasMany(e => e.drivers)
                .WithOptional(e => e.workshop)
                .HasForeignKey(e => e.workshop_id);

            modelBuilder.Entity<workshop>()
                .HasMany(e => e.services)
                .WithOptional(e => e.workshop)
                .HasForeignKey(e => e.workshop_id);

            modelBuilder.Entity<workshop>()
                .HasMany(e => e.serviceadvisors)
                .WithOptional(e => e.workshop)
                .HasForeignKey(e => e.workshop_id);

            modelBuilder.Entity<workshop>()
                .HasMany(e => e.servicebookeds)
                .WithOptional(e => e.workshop)
                .HasForeignKey(e => e.workshop_id);

            modelBuilder.Entity<workshop>()
                .HasMany(e => e.uploaddatas)
                .WithOptional(e => e.workshop)
                .HasForeignKey(e => e.workshop_id);

            modelBuilder.Entity<workshop>()
                .HasMany(e => e.userworkshops)
                .WithRequired(e => e.workshop)
                .HasForeignKey(e => e.workshopList_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<workshop>()
                .HasMany(e => e.workshopsummaries)
                .WithOptional(e => e.workshop)
                .HasForeignKey(e => e.workshop_id);

            modelBuilder.Entity<workshop>()
                .HasMany(e => e.wyzusers)
                .WithOptional(e => e.workshop)
                .HasForeignKey(e => e.workshop_id);

            modelBuilder.Entity<wyzuser>()
                .Property(e => e.creManager)
                .IsUnicode(false);

            modelBuilder.Entity<wyzuser>()
                .Property(e => e.dealerCode)
                .IsUnicode(false);

            modelBuilder.Entity<wyzuser>()
                .Property(e => e.dealerId)
                .IsUnicode(false);

            modelBuilder.Entity<wyzuser>()
                .Property(e => e.dealerName)
                .IsUnicode(false);

            modelBuilder.Entity<wyzuser>()
                .Property(e => e.emailId)
                .IsUnicode(false);

            modelBuilder.Entity<wyzuser>()
                .Property(e => e.firstName)
                .IsUnicode(false);

            modelBuilder.Entity<wyzuser>()
                .Property(e => e.lastName)
                .IsUnicode(false);

            modelBuilder.Entity<wyzuser>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<wyzuser>()
                .Property(e => e.phoneIMEINo)
                .IsUnicode(false);

            modelBuilder.Entity<wyzuser>()
                .Property(e => e.phoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<wyzuser>()
                .Property(e => e.registrationId)
                .IsUnicode(false);

            modelBuilder.Entity<wyzuser>()
                .Property(e => e.role)
                .IsUnicode(false);

            modelBuilder.Entity<wyzuser>()
                .Property(e => e.role1)
                .IsUnicode(false);

            modelBuilder.Entity<wyzuser>()
                .Property(e => e.userName)
                .IsUnicode(false);

            modelBuilder.Entity<wyzuser>()
                .Property(e => e.extensionId)
                .IsUnicode(false);

            modelBuilder.Entity<wyzuser>()
                .Property(e => e.appVersion)
                .IsUnicode(false);

            modelBuilder.Entity<wyzuser>()
                .Property(e => e.osversion)
                .IsUnicode(false);

            modelBuilder.Entity<wyzuser>()
                .Property(e => e.phoneModel)
                .IsUnicode(false);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.appointmentbookeds)
                .WithOptional(e => e.wyzuser)
                .HasForeignKey(e => e.wyzuser_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.assignedinteractions)
                .WithOptional(e => e.wyzuser)
                .HasForeignKey(e => e.wyzUser_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.availabilities)
                .WithOptional(e => e.wyzuser)
                .HasForeignKey(e => e.wyzUser_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.callinteractions)
                .WithOptional(e => e.wyzuser)
                .HasForeignKey(e => e.wyzUser_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.caselists)
                .WithOptional(e => e.wyzuser)
                .HasForeignKey(e => e.user_Id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.complaints)
                .WithOptional(e => e.wyzuser)
                .HasForeignKey(e => e.wyzUser_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.complaints1)
                .WithOptional(e => e.wyzuser1)
                .HasForeignKey(e => e.assignedUserTo_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.complaintassignedinteractions)
                .WithOptional(e => e.wyzuser)
                .HasForeignKey(e => e.assignedTo_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.complaintinteractions)
                .WithOptional(e => e.wyzuser)
                .HasForeignKey(e => e.assignedUser_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.drivers)
                .WithOptional(e => e.wyzuser)
                .HasForeignKey(e => e.wyzUser_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.driverinteractions)
                .WithOptional(e => e.wyzuser)
                .HasForeignKey(e => e.wyzUser_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.insuranceagents)
                .WithOptional(e => e.wyzuser)
                .HasForeignKey(e => e.wyzUser_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.insuranceassignedinteractions)
                .WithOptional(e => e.wyzuser)
                .HasForeignKey(e => e.wyzUser_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.psfassignedinteractions)
                .WithOptional(e => e.wyzuser)
                .HasForeignKey(e => e.wyzUser_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.serviceadvisors)
                .WithOptional(e => e.wyzuser)
                .HasForeignKey(e => e.wyzUser_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.serviceadvisorinteractions)
                .WithOptional(e => e.wyzuser)
                .HasForeignKey(e => e.wyzUser_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.servicebookeds)
                .WithOptional(e => e.wyzuser)
                .HasForeignKey(e => e.chaser_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.servicebookeds1)
                .WithOptional(e => e.wyzuser1)
                .HasForeignKey(e => e.wyzUser_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.smsinteractions)
                .WithOptional(e => e.wyzuser)
                .HasForeignKey(e => e.wyzUser_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.taggingusers)
                .WithOptional(e => e.wyzuser)
                .HasForeignKey(e => e.wyzUser_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.taggingusers1)
                .WithOptional(e => e.wyzuser1)
                .HasForeignKey(e => e.wyzUser_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.unavailabilities)
                .WithOptional(e => e.wyzuser)
                .HasForeignKey(e => e.wyzUser_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.uploads)
                .WithOptional(e => e.wyzuser)
                .HasForeignKey(e => e.uploadedBy_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.uploaddatas)
                .WithOptional(e => e.wyzuser)
                .HasForeignKey(e => e.wyzUser_id);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.userlocations)
                .WithRequired(e => e.wyzuser)
                .HasForeignKey(e => e.userLocation_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.userroles)
                .WithRequired(e => e.wyzuser)
                .HasForeignKey(e => e.users_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<wyzuser>()
                .HasMany(e => e.userworkshops)
                .WithRequired(e => e.wyzuser)
                .HasForeignKey(e => e.userWorkshop_id)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<wyzuser>()
            //    .HasMany(e => e.fieldwalkinlocations)
            //    .WithMany(e => e.wyzusers)
            //    .Map(m => m.ToTable("userfieldwalkinlocation").MapLeftKey("userFieldWalkinLocation_id").MapRightKey("fieldWalkinLocationList_id"));
        }
    }
}
