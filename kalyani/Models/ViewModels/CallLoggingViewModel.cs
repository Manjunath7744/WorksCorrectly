using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoSherpa_project.Models.ViewModels
{
    public class CallLoggingViewModel
    {
        public wyzuser wyzuser { get; set; }
        public customer cust { get; set; }
        public List<string> selectedTagList { get; set; }
        public customer custoAdd { get; set; }
        public vehicle vehi { get; set; }
        public callinteraction callinteraction { get; set; }
        public List<workshop> workshopList { get; set; }
        public List<workshop> allworkshopList { get; set; }
        public List<location> locationList { get; set; }
        public List<servicetype> servicetypeList { get; set; }
        
        public List<fieldwalkinlocation> walkinlocationList { get; set; }
        public List<addoncover> addOnCoversList { get; set; }
        public List<renewaltype> renewaltypes { get; set; }
        public List<insurancecompany> companiesList { get; set; }
        public service Latestservices { get; set; }
        public insurance LatestInsurance { get; set; }
        public List<smstemplate> smstemplates { get; set; }
        public List<specialoffermaster> offerList { get; set; }
        public List<insuranceagent> insuranceagents { get; set; }
        public List<TaggingView> tags { get; set; }
        public List<tagginguser> tagList { get; set; }
        public smrforecasteddata smrCall { get; set; }
        public psfassignedinteraction lastPSFAssignStatus { get; set; }
        public calldispositiondata finaldispostion { get; set; }
        public documentuploadhistory docHistory { get; set; }
        public citystate citystates { get; set; }
        public address addressesAdd { get; set; }
        public email emailAdd { get; set; }
        public List<psf_qt_history> psf_Qt_History { get; set; }
       
        //Supporting Module for Service Bound
        public ListingForm listingForm { get; set; }
        public srdisposition srdisposition { get; set; }
        public callinteraction callinterService { get; set; }
        public servicebooked servicebooked { get; set; }
        public  List<phone> phonesAdd { get; set; }
        public NewCar newCar { get; set; }

        //Supporting for Insurance
        public insurancedisposition insudisposition { get; set; }
        public appointmentbooked appointbooked { get; set; }

        public phone phoneAdd { get; set; }

        public service lastService { get; set; }
        public workshop workshop { get; set; }
        public psfassignedinteraction lastPSFAssign { get; set; }
        public psfinteraction psfLastInteraction { get; set; }
        public insuranceexceldata insuranceexceldata { get; set; } //added
        public List<smstemplate> templates { get; set; }
        public policyrenewallist policyrenewallist { get; set; }
        public List<emailtemplate> emailtemplates { get; set; }

        //for psf
        public DispositionPage dispositionPage { get; set; }
        public psfinteraction psfinteraction { get; set; }
        public pickupdrop pickupdrop { get; set; }

        //for email
        public Email email { get; set; }

        //for Customer UpdateForm
        public string submittedBtnName { get; set; }

        public List<string> modeOfContact { get; set; }
        public List<string> modeOfDay { get; set; }

        //Chethan Added for Insurance 
        public List<fieldwalkinlocation> walkinlocationLists { get; set; }
        public List<fieldwalkinlocation> FieldwalkinlocationList { get; set; }
        //public List<coupons> coupons { get; set; }
        public List<followupReason> followupReasons { get; set; }
        /****chethan added*/

        public insuranceassignedinteraction insuranceassignedinteraction { get; set; }

        //for Indus PSF Flow
        public IndusPSFInteraction indusPsfInteraction { get; set; }
        public List<calldispositiondata> techQuestions { get; set; }
        public List<calldispositiondata> nonTechQuestion { get; set; }
        public rework rework { get; set; }
        public List<PsfPullOut> psfPullOuts { get; set; }

        public CompInteraction compInteractions { get; set; }

        public RMInteraction rmInteraction { get; set; }

        //Customer Update Supporting varialbles
        public long prefferedPhone { get; set; }
        public long prefferedEmail { get; set; }

        //for psf last box in 360DegreeMNJ

        public List<psfBoxView> psfboxview { get; set; }
        public List<psfquestions> psfquestions { get; set; }

        public List<phone> custPhoneList { get; set; }

        //for new policydrop flowMNJ
        public inspolicydrop insPolicyDrop { get; set; }

        public long CustomerId { get; set; }

        public long VehicleId { get; set; }

        public long UserId { get; set; }

        public string DealerCode { get; set; }

        public string OEM { get; set; }

        //Viewbag replcement
        public string ceicustCat { get; set; }

        public List<SelectListItem> citystatesList { get; set; }

        public string CustCategory { get; set; }

        public string complaintOFCust { get; set; }

        public List<SelectListItem> stateList { get; set; }

        public string noShowCall { get; set; }

        public string AssignCRE { get; set; }

        public string UploadedDate { get; set; }

        public string AssignWorkShop { get; set; }

        public string InsAssignCRE { get; set; }

        public string InsUploadedDate { get; set; }

        public string InsAssignWorkShop { get; set; }

        public string NextServiceType { get; set; }

        public string NextServiceDate { get; set; }

        public string AssignCampaign { get; set; }

        public string InsAssignCampaign { get; set; }

        public string AssignTaggingName { get; set; }

        public string InsAssignTaggingName { get; set; }

        public int PsfCreBucketId { get; set; }

        public string PolicyDueDate { get; set; }
     

        public string PolicyEditedDueDate { get; set; }
        //AKASH ADDED

        public pmslabour pmslabour { get; set; }
        public List<pmscity> pmscitylist { get; set; }
        public List<pmsmodel> pmsmodels { get; set; }

        //postsalesfollowupquestions
        public List<postsalesfollowupquestions> postsalesfollowupquestionLists { get; set; }
        public postsalesdisposition postsalesdispositions { get; set; }
        public postsalescallinteraction postsalescallinteraction { get; set; }
        public postsalescompinteraction postsalescompinteraction { get; set; }
        public postsalesassignedinteraction postsalesassignedinteraction { get; set; }
        public string postsalespsfQuestionType { get; set; }

        public string postsalesAssignedCRE { get; set; }
        public string postsalesAssignedDate { get; set; }
        public string postsalesDayName { get; set; }
        public string postsalesAssignedCampaign { get; set; }
        public string postsalesAssignedworkshop { get; set; }
        public string postsalesChasis { get; set; }


        //indus cei
        public List<induspostservicefollowupquestions> induspostservicefollowupquestions { get; set; }
        public bool? isCEIPSF { get; set; }
        public List<coupon_viewdetails> coupons { get; set; }
        public bool ispsfDynamic { get; set; }
        public string offers { get; set; }
        public long postsalesinteractionId { get; set; }

        //Coupon details
        public string coupondetails { get; set; }
        //maruti std remarks
        public List<marutistdremarks> marutiremarkslist { get; set; }

        //intalk
        public string intalkDialNumber { get; set; }
        public string intalkIsCallInitiated { get; set; }
        public string intalkGSMUniqueId { get; set; }
        public string intalkNCReason { get; set; }
        public string wyzUserMngPhn { get; set; }

    }

    public class psfBoxView
    {
        public string wyzUser { get; set; }

        public string updateedDate { get; set; }

        public string workshopName { get; set; }

        public string campaignName { get; set; }
    }


}