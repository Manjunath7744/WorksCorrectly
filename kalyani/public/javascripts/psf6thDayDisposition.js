$(document).ready(function(){
	
	
	var dateToday1 = new Date();
	var dates1 = $("#selDate").datepicker({
	    //defaultDate: "+1w",
	dateFormat: 'yy-mm-dd',
	maxDate: "+180d",
	    minDate:0,
	    onSelect: function(selectedDate) {
	        var option = this.id == "selDate" ? "minDate" : "maxDate",
	            instance = $(this).data("datepicker"),
	            date = $.datepicker.parseDate(instance.settings.dateFormat || $.datepicker._defaults.dateFormat, selectedDate, instance.settings);
	       // dates.not(this).datepicker("option", option, date);
	    }
	});
	
	
	 $('.timePickRange7to20').timepicker({
         showPeriod: false, // 24 hours formate
         onHourShow: timepick,
        	showPeriodLabels: false,
		
			});

 function timepick(hour) {
     if ((hour > 20) || (hour < 7)) {
         return false;
     }
     return true;
 }
	
	$('#BackToLastQuestion9Submit').click(function(){
				var inputRadioSubmit = 0;
					$('[name="isSatisfiedWithServiceProvided"]').each(function (){
						if ($(this).is(':checked')) inputRadioSubmit++;
					});
					
					if (inputRadioSubmit == 0) {
					Lobibox.notify('warning', {
								continueDelayOnInactiveTab: true,
								msg: 'Please check one.'
							});
							return false;
						
					}else{
						
						if($('#11thNameSatis').prop('checked')){
							var selectedValSel=$('#11thSelectId').val();
						if(selectedValSel == '0'){
							Lobibox.notify('warning', {
								continueDelayOnInactiveTab: true,
								msg: 'Please Select From List.'
							});
							return false;
							
						}if(selectedValSel >=8){
							
							
						}else if(selectedValSel < 8 || selectedValSel >0){
							var inputRadiorating = 0;
					$('[name="remarks"]').each(function (){
						if ($(this).is(':checked')) inputRadiorating++;
					});
					if (inputRadiorating == 0) {
					Lobibox.notify('warning', {
								continueDelayOnInactiveTab: true,
								msg: 'Please check one.'
							});
							return false;
						
					}
							if($('#ratingRadio1').prop('checked')){
						var selectedValDate=$('#RattingQuestion1date').val();
						var selectedValTime=$('#RattingQuestion1time').val();
						if(selectedValDate == '' || selectedValTime =='' ){
							Lobibox.notify('warning', {
								continueDelayOnInactiveTab: true,
								msg: 'Please Select Date And time'
							});
							return false;
							
						}
					}
					if($('#ratingRadio2').prop('checked')){
						
						var selectedValDate2=$('#RattingQuestiondate2').val();
						var selectedValTime2=$('#RattingQuestiontime2').val();
						
						
						if(selectedValDate2 == '' || selectedValTime2 =='' ){
							Lobibox.notify('warning', {
								continueDelayOnInactiveTab: true,
								msg: 'Please Select Date And time'
							});
							return false;
							
						}
					}else if($('#ratingRadio3').prop('checked')){
						
					}
							
						}
						}else if($('#11thNameDesatis').prop('checked')){
							var inputRadiorating2 = 0;
					$('[name="remarksDissatisfied"]').each(function (){
						if ($(this).is(':checked')) inputRadiorating2++;
					});
					if (inputRadiorating2 == 0) {
					Lobibox.notify('warning', {
								continueDelayOnInactiveTab: true,
								msg: 'Please check one.'
							});
							return false;
						
					}
						}
						if($('#ratingRadio1New').prop('checked')){
						var selectedValDate1New=$('#RattingQuestion1dateNew').val();
						var selectedValTime1New=$('#RattingQuestion1timeNew').val();
						if(selectedValDate1New == '' || selectedValTime1New =='' ){
							Lobibox.notify('warning', {
								continueDelayOnInactiveTab: true,
								msg: 'Please Select Date And time'
							});
							return false;
							
						}
					}
					if($('#ratingRadio2New').prop('checked')){
						
						var selectedValDate2New=$('#RattingQuestiondate2New').val();
						var selectedValTime2New=$('#RattingQuestiontime2New').val();
						
						
						if(selectedValDate2New == '' || selectedValTime2New =='' ){
							Lobibox.notify('warning', {
								continueDelayOnInactiveTab: true,
								msg: 'Please Select Date And time'
							});
							return false;
							
						}
					}
						
						$.blockUI();
						
					}
				
	});
	
	

	$('#pickupSubmitVal').click(function(){
		
							var selectedValSelPick=$('#PickUpRatingSelect').val();
						if(selectedValSelPick == '0'){
							Lobibox.notify('warning', {
								continueDelayOnInactiveTab: true,
								msg: 'Please Select From List.'
							});
							return false;
							
						}if(selectedValSelPick >=8){
							
							
						}else if(selectedValSelPick < 8 || selectedValSelPick >0){
							var inputRadioratingPick = 0;
					$('[name="remarksPickUp"]').each(function (){
						if ($(this).is(':checked')) inputRadioratingPick++;
					});
					if (inputRadioratingPick == 0) {
					Lobibox.notify('warning', {
								continueDelayOnInactiveTab: true,
								msg: 'Please check one.'
							});
							return false;
						
					}
							if($("#pickUpRadio1").prop('checked')){
						var selectedValDate1Pick=$('#pickupDate1').val();
						var selectedValTime1Pick=$('#pickupTime1').val();
						if(selectedValDate1Pick == '' || selectedValTime1Pick =='' ){
							Lobibox.notify('warning', {
								continueDelayOnInactiveTab: true,
								msg: 'Please Select Date And time'
							});
							return false;
							
						}
					}
					if($('#pickUpRadio2').prop('checked')){
						
						var selectedValDate2Pick=$('#pickupDate2').val();
						var selectedValTime2Pick=$('#pickupTime2').val();
						
						
						if(selectedValDate2Pick == '' || selectedValTime2Pick =='' ){
							Lobibox.notify('warning', {
								continueDelayOnInactiveTab: true,
								msg: 'Please Select Date And time'
							});
							return false;
							
						}
						}	}		
						$.blockUI();
									
	});

$('#ratingRadio1').click(function(){
$('#RattingQuestiondate2').val('');
$('#RattingQuestiontime2').val('');
});
$('#ratingRadio2').click(function(){
$('#RattingQuestion1date').val('');
$('#RattingQuestion1time').val('');
});

$('#talkCustomerSubmit').click(function(){
	 var inputRadioSel = 0;
				$('[name="PSFDispositon"]').each(function (){
					if ($(this).is(':checked')) inputRadioSel++;
				});
				if (inputRadioSel == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please check one.'
						});
						return false;
					
				} else{
				$.blockUI();
                                    
                                }
	
});


 



  $("select[name='ratinglast']").change(function(){
  
					var ratting=$("#11thSelectId").val();
			
					    if(ratting >= 8)
					    {
					
					        $("#IfNo11thqueDissatisfiedNew").hide();
					        $("#rattingAbove8").show();
					    }
					    if(ratting < 8)
					    {
					        $("#IfNo11thqueDissatisfiedNew").show();
                            $("#rattingAbove8").hide();
                          
					    }
				});

               
 $("input[name$='remarks']").click(function() {
        var vratting1 = $(this).val();
		if(vratting1=="bring the vehicle")
		{
			$("#RattingQuestion1").show();
			$("#RattingQuestion2").hide();
			
		}
			else if(vratting1=="customer's premise")
		{
			$("#RattingQuestion2").show();
			$("#RattingQuestion1").hide();
			
		}
		else
		{
			$("#RattingQuestion2").hide();
			$("#RattingQuestion1").hide();
		}
		
});
 $("input[name$='remarksDissatisfied']").click(function() {
			var vratting2 = $(this).val();
			if(vratting2=="bring the vehicleNew")
			{
				
				$("#RattingQuestion2New").show();
				$("#RattingQuestion3New").hide();
				
			}
				else if(vratting2=="customer's premiseNew")
			{
				$("#RattingQuestion2New").hide();
				$("#RattingQuestion3New").show();
				
				
			}
			else
			{
				$("#RattingQuestion2New").hide();
				$("#RattingQuestion3New").hide();
			}
			
	});

////////pickup ratting

 $("select[name='pickupRatingName']").change(function(){
  
					var rattingPickUp=$("#PickUpRatingSelect").val();
			
					    if(rattingPickUp >= 8)
					    {
					
					        $("#IfNo11thqueDissatisfiedPickUp").hide();
					        $("#rattingAbove8PickUp").show();
					    }
					    if(rattingPickUp < 8)
					    {
					        $("#IfNo11thqueDissatisfiedPickUp").show();
                            $("#rattingAbove8PickUp").hide();
                          
					    }
				});
				
				$("input[name='PSF6thDayPSF']").click(function(){
  
					var var6thdayPSF=$(this).val();
			
					    if(var6thdayPSF == "Yes workshop")
					    {
							$("#RattingQuestion2New6thdayPSF").show();
					        $("#RattingQuestion3New6thDayPSF").hide();
					      
					    }
					   else if(var6thdayPSF == "customer premise")
					    {
					        $("#RattingQuestion3New6thDayPSF").show();
                            $("#RattingQuestion2New6thdayPSF").hide();
                          
					    }
						else{
							 $("#RattingQuestion3New6thDayPSF").hide();
                            $("#RattingQuestion2New6thdayPSF").hide();
						}
				});

               
 $("input[name$='remarksPickUp']").click(function() {
        var vratting1PickUp = $(this).val();
		if(vratting1PickUp=="bring the vehiclePickUp")
		{
			$("#RattingQuestion1PickUp").show();
			$("#RattingQuestion2PickUp").hide();
			
		}
			else if(vratting1PickUp=="customer's premisePickUp")
		{
			$("#RattingQuestion2PickUp").show();
			$("#RattingQuestion1PickUp").hide();
			
		}
		else
		{
			$("#RattingQuestion2PickUp").hide();
			$("#RattingQuestion1PickUp").hide();
		}
		
});



////////pickup


 $("select[name='MMSRatingName']").change(function(){
  
					var rattingMMS=$("#MMSRatingSelect").val();
			
					    if(rattingMMS >= 8)
					    {
					
					        $("#IfNo11thqueDissatisfiedMMS").hide();
							 $("#IfNo11thqMMS").hide();
					        $("#rattingAbove8MMS").show();
					    }
					    if(rattingMMS < 8)
					    {
					        $("#IfNo11thqMMS").show();
					        $("#IfNo11thqueDissatisfiedMMS").show();
                           	$("#rattingAbove8MMS").hide();
							
                          
					    }
				});

               
 $("input[name$='ratting1MMS']").click(function() {
        var vratting1MMS = $(this).val();
		if(vratting1MMS=="bring the vehicleMMS")
		{
			$("#RattingQuestion1MMS").show();
			$("#RattingQuestion2MMS").hide();
			
		}
			else if(vratting1MMS=="customer's premiseMMS")
		{
			$("#RattingQuestion2MMS").show();
			$("#RattingQuestion1MMS").hide();
			
		}
		else
		{
			$("#RattingQuestion2MMS").hide();
			$("#RattingQuestion1MMS").hide();
		}
		
});





$('#nextToLastQuestion').click(function(){
		//first set radio button checked ir ot
			var inputRadioLastQuest = 0;
			$('[name="isVisitedWorkshopBeforeAppoint"]').each(function (){
					if ($(this).is(':checked'))inputRadioLastQuest++; 	
						
				});
				if (inputRadioLastQuest == 0) {
						
						
					Lobibox.notify('warning', {
									continueDelayOnInactiveTab: true,
									msg: 'Please choose Any One.'
								});
								return false;
								
						}
				
			//if yes is chekced firsst yes	
			if($('#8thNameYes').prop('checked')){
				//seconf set is checked or no
				var inputRadioLastQuest1 = 0;
				$('[name="isAppointmentGotAsDesired"]').each(function (){
					if ($(this).is(':checked')) inputRadioLastQuest1++;
					
											
				});
				if (inputRadioLastQuest1 == 0) {

					Lobibox.notify('warning', {
									continueDelayOnInactiveTab: true,
									msg: 'Please choose Any One.'
								});
								return false;
											}
				//secon yes
				if($('#Sub9ThQuestNo').prop('checked')){
					
						var appointmentHour = $('#appointmentHour').val();
						var appointmentMin = $('#appointmentMins').val();
						if (appointmentHour == '') {
						Lobibox.notify('warning', {
									continueDelayOnInactiveTab: true,
									msg: 'Please choose Appointment Hour.'
								});
								return false;
						}if (appointmentMin == '') {
						Lobibox.notify('warning', {
									continueDelayOnInactiveTab: true,
									msg: 'Please choose Appointment Time.'
								});
								return false;
						}else{
							
						$("#8thquestionDiv").hide();
						$("#9questionSatisfiedDiv").show();
						}
				}else if($('#Sub9ThQuestYes').prop('checked')){// second no
					
					$("#8thquestionDiv").hide();
						$("#9questionSatisfiedDiv").show();
				}
				
			}else if($('#8thNameNo').prop('checked')){ // first no
				$("#8thquestionDiv").hide();
				$("#9questionSatisfiedDiv").show();
				
			}
			$("#SaveInCompleteServeyDiv").hide();
				
		
});
$('#BackToLastQuestion9').click(function(){
//$('[name="isVisitedWorkshopBeforeAppoint"]').prop('checked',false);
//$('[name="isAppointmentGotAsDesired"]').prop('checked',false);
$("#8thquestionDiv").show();
$("#8thQuestListYes").show();
$("#IfNo9thquestion").show();
//$("#appointmentHour").val('');
//$("#appointmentMins").val('');
$("#9questionSatisfiedDiv").hide();
$("#SaveInCompleteServeyDiv").show();


});



$('#BackTospeak').on('click',function(){
	//$( "input[name='modeOfService']" ).prop( "checked", false );
	
	
$("#PSFconnectCall1").show();
$("#PsfSelfDriveINYes").hide();
$("#PSFYesTalk").show();
$("#PSFYesNamaskarYesDiv").hide();
$("#remarksDivDisplay").hide();


$("#SaveInCompleteServeyDiv").hide();
 $( "#psfYesNamaskarbtn" ).prop( "checked", false );
});

$('#BackTospeakResolution').on('click',function(){
	
$("#PSFconnectCall1").show();
$("#PSFYesTalk").show();
$("#PSFYesNamaskarYesDiv").hide();
$("#NoResolutionClosedSelId").hide();
$("#remarksDivDisplay").hide();
 $( "#psfYesNamaskarbtn" ).prop( "checked", false );
});
$('#BackToRework').on('click',function(){
	
	$("#PSFconnectCall1").show();
	$("#PSFYesTalk").show();
	$("#PSFYesNamaskarYesDiv").hide();
	$("#ReworkDivId").hide();
	$("#remarksDivDisplay").hide();
	 $( "#psfYesNamaskarbtn" ).prop( "checked", false );
	});

$('#BackToResolved').on('click',function(){
	
	$("#PSFconnectCall1").show();
	$("#PSFYesTalk").show();
	$("#PSFYesNamaskarYesDiv").hide();
	$("#ResolvedDivId").hide();
	$("#remarksDivDisplay").hide();
	 $( "#psfYesNamaskarbtn" ).prop( "checked", false );
	});


$('#SatisfiedNoBtn').on('click',function(){
if($('#SatisfiedNoBtn').attr('checked',true)){
$('#disatisfiedBtns').hide();
$('#disatisfiedyesnoBtns').show();
}
});



var varPSFYes="";
var varPSF1Yes='';
var PSFtest='';
var varPSFSatisfied='';
var varPSFWere3='';
var var4tQuest='';
var var8thQuest='';
var varSub9ThQuest='';
var var11thName='';
var varPickup1QuestName='';
var varpickupRepair1='';
var var5thPickUpName='';
var varmodeOfService=null;


 $("input[name$='PSFDispositon']").click(function() {
         var PSFOthers = $(this).val();
		if(PSFOthers=="NoOther")
		{
			$("#PSFOtherDivShow").show();			
			
		}
			else
		{
			
			$("#PSFOtherDivShow").hide();
			
		}
		
});

$("input[name$='satisfiedWithWashingSelfD']").click(function() {
         var V5thName = $(this).val();
		if(V5thName=="No")
		{
			$("#5thQuestList").show();
			
			
		}
			else
		{
			
			$("#5thQuestList").hide();
			
		}
		
});

 $("input[name='isContacted']").click(function() {
         PSFtest = $(this).val();
		if(PSFtest=="Yes")
		{
			
			$("#PSFYesTalk").show();
			$("#PSFNotSpeachDiv").hide();
			$("#SaveInCompleteServeyDiv").hide();
			$("#remarksDivDisplay").hide();
		$("input:radio[name='PSFDispositon']").each(function(i) {
		this.checked = false;
});
			
		}
			if(PSFtest=="No")
		{
			$("#PSFNotSpeachDiv").show();
			$("#PSFYesTalk").hide();
			$("#SaveInCompleteServeyDiv").hide();
			$("#remarksDivDisplay").show();
		$("input:radio[name='disposition']").each(function(i) {
		this.checked = false;
});
			
		}
		
});

	$("input[name$='disposition']").click(function(){
		varPSFYes = $(this).val();
		if(varPSFYes == "PSF_Yes" || varPSFYes == "Call Me Later" || varPSFYes == "Not Interested" || varPSFYes == "No Resolution - Closed"|| varPSFYes == "Re-Work"|| varPSFYes == "Resolved"|| varPSFYes =="ConfirmStatus")
		{
			psfNamaskar();
		}
});

function psfNamaskar(){
	//alert(" varPSFYes "+varPSFYes);
	if(varPSFYes!="")
		{
			if(varPSFYes == "PSF_Yes")
				
			{
				$("#PSFYesNamaskarYesDiv").show();
				$("#PSFYesNamaskarNoDiv").hide();
				$("#PSFconnectCall1").hide();
				$("#PSFYesTalk").hide();
				$("#NotInterestedBtns").hide();
				$("#SaveInCompleteServeyDiv").show();
				$("#NoResolutionClosedSelId").hide();
				$("#ReworkDivId").hide();
				$("#ResolvedDivId").hide();
				$("#ConfirmStatusDivId").hide();
				$("#remarksDivDisplay").show();
				 $("#complaintCategorySelect").show();
				
					
			}
			if(varPSFYes == "Call Me Later")
			{
				$("#PSFYesNamaskarNoDiv").show();
				$("#PSFYesNamaskarYesDiv").hide();
				$("#PSFconnectCall1").hide();
				$("#PSFYesTalk").hide();
				$("#NotInterestedBtns").hide();
				$("#SaveInCompleteServeyDiv").hide();
				$("#NoResolutionClosedSelId").hide();
				$("#ReworkDivId").hide();
				$("#ResolvedDivId").hide();
				$("#ConfirmStatusDivId").hide();
				$("#remarksDivDisplay").show();
				 $("#complaintCategorySelect").hide();
			
				
			}
			if(varPSFYes == "Not Interested")
			{
				$("#PSFYesNamaskarNoDiv").hide();
				$("#PSFYesNamaskarYesDiv").hide();
				$("#PSFconnectCall1").hide();
				$("#PSFYesTalk").hide();
				$("#NotInterestedBtns").show();
				$("#SaveInCompleteServeyDiv").hide();
				$("#NoResolutionClosedSelId").hide();
				$("#ReworkDivId").hide();
				$("#ResolvedDivId").hide();
				$("#ConfirmStatusDivId").hide();
				$("#remarksDivDisplay").show();
				 $("#complaintCategorySelect").hide();
			
				
			}
			if(varPSFYes =="No Resolution - Closed"){
				
				$("#PSFYesNamaskarNoDiv").hide();
				$("#PSFYesNamaskarYesDiv").hide();
				$("#PSFconnectCall1").hide();
				$("#PSFYesTalk").hide();
				$("#NotInterestedBtns").hide();
				$("#SaveInCompleteServeyDiv").hide();
				$("#NoResolutionClosedSelId").show();
				$("#ReworkDivId").hide();
				$("#ResolvedDivId").hide();
				$("#ConfirmStatusDivId").hide();
				$("#remarksDivDisplay").show();
				 $("#complaintCategorySelect").hide();
			
			}
			if(varPSFYes =="Re-Work"){
				
				$("#PSFYesNamaskarNoDiv").hide();
				$("#PSFYesNamaskarYesDiv").hide();
				$("#PSFconnectCall1").hide();
				$("#PSFYesTalk").hide();
				$("#NotInterestedBtns").hide();
				$("#SaveInCompleteServeyDiv").hide();
				$("#NoResolutionClosedSelId").hide();
				$("#ReworkDivId").show();
				$("#ResolvedDivId").hide();
				$("#ConfirmStatusDivId").hide();
				$("#remarksDivDisplay").show();
				 $("#complaintCategorySelect").hide();
				ajaxCallToLoadWorkShop();
			
			}
			if(varPSFYes =="Resolved"){
				
				$("#PSFYesNamaskarNoDiv").hide();
				$("#PSFYesNamaskarYesDiv").hide();
				$("#PSFconnectCall1").hide();
				$("#PSFYesTalk").hide();
				$("#NotInterestedBtns").hide();
				$("#SaveInCompleteServeyDiv").hide();
				$("#NoResolutionClosedSelId").hide();
				$("#ReworkDivId").hide();
				$("#ConfirmStatusDivId").hide();	
				$("#ResolvedDivId").show();
				$("#remarksDivDisplay").show();
				 $("#complaintCategorySelect").show();
			
			}
			if(varPSFYes =="ConfirmStatus"){
				
				$("#PSFYesNamaskarNoDiv").hide();
				$("#PSFYesNamaskarYesDiv").hide();
				$("#PSFconnectCall1").hide();
				$("#PSFYesTalk").hide();
				$("#NotInterestedBtns").hide();
				$("#SaveInCompleteServeyDiv").hide();
				$("#NoResolutionClosedSelId").hide();
				$("#ReworkDivId").hide();
				$("#ResolvedDivId").hide();
				$("#ConfirmStatusDivId").show();				
				$("#remarksDivDisplay").show();
				 $("#complaintCategorySelect").hide();
			}
			
		}

}
 

	

		function dropDownCheck(){
			if(varChecked!="")
			{
				if(varChecked =='Self Drive-in')
				{
					$('#6thDayPSF').show();
					$('.6thDayPSF').show();
					$('#PickUp1Div').hide();
					$('#MMS1Div').hide();
					//$('#BackTospeak').hide();
					$('#PSFYesNamaskarYesDiv').hide();
					
				}
				if(varChecked =='Pick-Up')
				{
					$('#PickUp1Div').show();
					$('#SelfDriveIn1Div').hide();
					$('.6thDayPSF').hide();
					$('#MMS1Div').hide();
					//$('#BackTospeak').hide();
					$('#PSFYesNamaskarYesDiv').hide();
				}
				if(varChecked =='MMS')
				{
					$('#MMS1Div').show();
					$('#PickUp1Div').hide();
					$('#SelfDriveIn1Div').hide();
					$('.6thDayPSF').hide();
					//$('#BackTospeak').hide();
					$('#PSFYesNamaskarYesDiv').hide();
				}
				else{
				
				}
			}
		}

	 $("input[name$='selfDriveInFeedBack']").click(function() {
         varPSFSatisfied = $(this).val();
		if(varPSFSatisfied=="Satisfied")
		{
		
			$("#DisSatisfiedID").hide();
			$("#SatisfiedBacktoBextBtn").show();
			
			
		}
			if(varPSFSatisfied=="Dissatisfied")
		{
			$("#DisSatisfiedID").show();
			$("#SatisfiedBacktoBextBtn").show();
			
			
		}
	});
	
	 $("input[name$='demandedRequestDone']").click(function() {
         varPSFDemand2 = $(this).val();
		if(varPSFDemand2=="Yes")
		{
		
			$("#IfNodemandedDiv").hide();
			$("#DemandbackNextBtn").show();
			
		}
			if(varPSFDemand2=="No")
		{
			$("#IfNodemandedDiv").show();
			$("#DemandbackNextBtn").show();
			
			
		}
	});
	
	 $("input[name$='isProblemFixedInFirstVisit']").click(function() {
         varPSFWere3 = $(this).val();
		if(varPSFWere3=="Yes")
		{
		
			$("#3problemPSFListDiv").hide();
			//$("#DemandbackNextBtn").show();
			
		}
			if(varPSFWere3=="No")
		{
			$("#3problemPSFListDiv").show();
			//$("#DemandbackNextBtn").show();
			
			
		}
	});
		 $("input[name$='isDeliveredAsPromisedTime']").click(function() {
         var4tQuest = $(this).val();
		if(var4tQuest=="Yes")
		{
		
			$("#4thQuestList ").hide();
			
		}
			if(var4tQuest=="No")
		{
			$("#4thQuestList ").show();
			
		}
	});
	
	 $("input[name$='vehiclePerformancePickUp']").click(function() {
         varPickup1QuestName = $(this).val();
		if(varPickup1QuestName=="Satisfied")
			
		{
		
			$("#PickUpSatisfiedYes").show();
			$("#PickUpDisSatisfiedNo").hide();
			$("#pickupRepareNoDiv").hide();
			$("#pickupRepareYesDiv").hide();
			$("#pickupTextRemark").val('');
			
		$("input:radio[name='isDemandOfSerDoneInLVisitpickup']").each(function() {
       this.checked = false;
});
			
		}
			if(varPickup1QuestName=="Dissatisfied")
		{
			$("#PickUpSatisfiedYes").hide();
			$("#PickUpDisSatisfiedNo").show();
			$("#pickupRepareNoDiv").hide();
			$("#pickupRepareYesDiv").hide();
			
		}
	});
	
	 $("input[name$='isVisitedWorkshopBeforeAppoint']").click(function() {
         var8thQuest = $(this).val();
		if(var8thQuest=="Yes")
		{
		
			$("#8thQuestListYes").show();
			$("#8thQuestListNo").hide();
			$("#IfYes11thqueSatisfied").hide();
			$("#IfNo11thqueDissatisfied").hide();
			$("#IfNo9thquestion").hide();
			
		}
			if(var8thQuest=="No")
			
		{
			$("#8thQuestListNo").show();
			$("#8thQuestListYes").hide();
			$("#IfYes11thqueSatisfied").hide();
			$("#IfNo11thqueDissatisfied").hide();
			$("#IfNo9thquestion").hide();
			
		}
	});
	
	 $("input[name$='isAppointmentGotAsDesired']").click(function() {
         varSub9ThQuest = $(this).val();
		if(varSub9ThQuest=="Yes")
		{
		
			$("#IfNo9thquestion").hide();
			$("#8thQuestListNo").show();
			
			
		}
			if(varSub9ThQuest=="No")
		{
			$("#IfNo9thquestion").show();
			$("#8thQuestListNo").hide();
			
		}
	});
	
	$("input[name$='isSatisfiedWithServiceProvided']").click(function() {
 var11thName = $(this).val();
if(var11thName=="Satisfied")
{

$("#IfYes11thqueSatisfied").show();
$("#IfNo11thqueDissatisfied").hide();
$("#rattingAbove8").hide();
$("#IfNo11thqueDissatisfiedNew").hide();

}
if(var11thName=="Dissatisfied")

{
$("#IfNo11thqueDissatisfied").hide();
$("#IfNo11thqueDissatisfiedNew").show();
$("#IfYes11thqueSatisfied").hide();
$("#rattingAbove8").hide();


}
});
	
	 $("input[name$='isDemandOfSerDoneInLVisitpickup']").click(function() {
         varpickupRepair1 = $(this).val();
		if(varpickupRepair1=="Yes")
		{
		
			$("#pickupRepareYesDiv").show();
			$("#pickupRepareNoDiv").hide();
			$("#pickupTextRemark").val('');
			
		}
			if(varpickupRepair1=="No")
			
		{
			$("#pickupRepareNoDiv").show();
			$("#pickupRepareYesDiv").hide();
			
		}
	});
	
	$("input[name$='isVehicleReadyAsPromisedDate']").click(function() {
         var5thPickUpName = $(this).val();
		if(var5thPickUpName=="Yes")
		{
		
			$("#5thPickUpDivNo").hide();
			$("#PickUpDivDate").val('');
			$("#PickUpDivMin").val('');
			$("#PickUpDivText").val('');
			
		}
			if(var5thPickUpName=="No")
		{
			$("#5thPickUpDivNo").show();
			
		}
	});
$("#NextTodemanded2DivShow").click(function(){
	
	if($('#SatisfiedNoBtn').prop('checked')){
		var reasonDis = $('#dissatisfactionReason').val();
		var dateDissatified = $('#followUpDateDissatisfied').val();
		var timeDissatified = $('#followUpDateDissatisfiedTime').val();
		
		if(reasonDis =='0'){
			Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please select'
						});
						return false;
			
		}else if(dateDissatified == ''){
			Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Select Date.'
						});
						return false;
		}else if(timeDissatified == ''){
			Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Select Time.'
						});
						return false;
		}else{
			
				$('#2PSFDemanded').show();
	$('.2PSFDemanded').show();
	
	$('#PSFYesNamaskarYesDiv').hide();
	$('.6thDayPSF').hide();
	$('#SelfDriveIn1Div').hide();
	$('#DisSatisfiedID').hide();
	$('#PsfSelfDriveInNo1').hide();
	$('#SatisfiedBacktoBextBtn').hide();
		}
	
	}
	
	
	
	 var inputRadioDissatisfied = 0;
				$('[name="selfDriveInFeedBack"]').each(function (){
					if ($(this).is(':checked')) inputRadioDissatisfied++;
				});
				if (inputRadioDissatisfied == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please check one.'
						});
						return false;
					
				} 
	$('#2PSFDemanded').show();
	$('.2PSFDemanded').show();
	
	$('#PSFYesNamaskarYesDiv').hide();
	$('.6thDayPSF').hide();
	$('#SelfDriveIn1Div').hide();
	$('#DisSatisfiedID').hide();
	$('#PsfSelfDriveInNo1').hide();
	$('#SatisfiedBacktoBextBtn').hide();
	
	
	
});


$("#BackTo1stQuestion").click(function(){

 $('#dissatisfactionReason').val('0');
 $('#followUpDateDissatisfied').val('');
$('#followUpDateDissatisfiedTime').val('');
		$(".2PSFDemanded").hide();
		$(".6thDayPSF").show();

});


$("#BackToDidUtlakPSF").click(function(){
		$('#time_ToIn').val('');
		$('#time_FromIn').val('');
		$("#PSFYesTalk").show();
		$("#PSFconnectCall1").show();
		$("#PSFYesNamaskarNoDiv").hide();
		$("#remarksDivDisplay").hide();
		
});

$("#BackToDidUtlakPSF1").click(function(){
		$('#remarksValueForNoIntrst').val('');
		$("#PSFYesTalk").show();
		$("#PSFconnectCall1").show();
		$("#NotInterestedBtns").hide();
		$("#remarksDivDisplay").hide();
		

});

$("#BackToAfterresolution").click(function(){
	$('#remarksValueForNoIntrst').val('');
	$("#PSFYesTalk").show();
	$("#PSFconnectCall1").show();
	$("#ConfirmStatusDivId").hide();
	$("#remarksDivDisplay").hide();
	

});


$("#NextToPSFDriveIN").click(function(){

$("#PsfSelfDriveInNo1").show();
//$("#PSFYesNamaskarNoDiv").hide();
});
$("#NextTo3Question").click(function(){
		
	if($('#demanded2Nobtn').prop('checked')){
		var remarksval = $('#repairTextVal').val();
		
		
		if(remarksval ==''){
			Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please enter the text'
						});
						return false;
			
		}else{

	
				   $("#3rdPSFDemanded").show();
					$(".2PSFDemanded").hide();

		}
	
	}
	
	
	
				var inputRadioRemarks = 0;
				$('[name="demandedRequestDone"]').each(function (){
					if ($(this).is(':checked')) inputRadioRemarks++;
				});
				if (inputRadioRemarks == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please choose any one.'
						});
						return false;
					
				}else{
					
				   $("#3rdPSFDemanded").show();
					$(".2PSFDemanded").hide();
				} 
	
});
$("#BackTo2Question").click(function(){

$('#repairTextVal').val('');
$("#IfNodemandedDiv").hide();
$("#3rdPSFDemanded").hide();
$(".2PSFDemanded").show();

});

$("#BackTo1question").click(function(){

$("#PSFYesNamaskarYesDiv").show();
//$("#PsfSelfDriveInNo1").show();
$(".6thDayPSF").hide();



});
$("#BackTo1stQPickUp").click(function(){
$("#PickUp1Div").hide();
$("#PSFYesNamaskarYesDiv").show();


});
$("#NextTo4Question").click(function(){
		
	if($('#3WereNoBtn').prop('checked')){
		var probval = $('#problemTxt').val();
		
		
		if(probval ==''){
			Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please enter the text'
						});
						return false;
			
		}else{

	
				   $("#4thquestionDiv").show();
					$("#3rdPSFDemanded").hide();

		}
	
	}
	
	
	
				var inputRadioProb = 0;
				$('[name="isProblemFixedInFirstVisit"]').each(function (){
					if ($(this).is(':checked')) inputRadioProb++;
				});
				if (inputRadioProb == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please choose any one.'
						});
						return false;
					
				}else{
					
				  $("#4thquestionDiv").show();
					$("#3rdPSFDemanded").hide();
				} 
	
});
$("#BackTo3rdQuestion").click(function(){

$("#4thquestionDiv").hide();
$("#problemTxt").val('');
$("#3problemPSFListDiv").hide();
$("#3rdPSFDemanded").show();

});
$("#NextTo5thQuestion").click(function(){
if($('#4thNameNoBtn').prop('checked')){
		var hoursval = $('#hoursVal').val();
		var minsval = $('#MinuteVal').val();
		
		
if(hoursval =='' || minsval ==''){
			Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please enter the text'
						});
						return false;
			
		}else{

					$("#4thquestionDiv").hide();
					$("#5thquestionDiv").show();

		}
	
	}
	
	
	
				var inputRadioR4thName = 0;
				$('[name="isDeliveredAsPromisedTime"]').each(function (){
					if ($(this).is(':checked')) inputRadioR4thName++;
				});
				if (inputRadioR4thName == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please choose any one.'
						});
						return false;
					
				}else{
					
				   $("#4thquestionDiv").hide();
					$("#5thquestionDiv").show();
				} 
	

});

$("#BackTo4thQuestion").click(function(){
//$('[name="isDeliveredAsPromisedTime"]').prop('checked',false);
$("#4thquestionDiv").show();
$("#4thQuestList ").hide();
$("#hoursVal").val('');
$("#MinuteVal").val('');
$("#5thquestionDiv").hide();

});

$("#NextTo6thQuestion").click(function(){

if($('#5thNameNoBtn').prop('checked')){
		var fifthNameval = $('#specificTxt').val();
		
		
		if(fifthNameval ==''){
			Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please enter the text'
						});
						return false;
			
		}else{

	
				   $("#5thquestionDiv").hide();
					$("#6thquestionDiv").show();

		}
	
	}
	
	
	
				var inputRadio5thName = 0;
				$('[name="satisfiedWithWashingSelfD"]').each(function (){
					if ($(this).is(':checked')) inputRadio5thName++;
				});
				if (inputRadio5thName == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please choose any one.'
						});
						return false;
					
				}else{
					
				   $("#5thquestionDiv").hide();
					$("#6thquestionDiv").show();
				} 
	

});

$("#BackTo5thQuestion").click(function(){
//$('[name="satisfiedWithWashingSelfD"]').prop('checked',false);
$("#5thquestionDiv").show();
$("#6thquestionDiv").hide();
$("#5thQuestList").hide();

});

	$("#NextTo7thQuestion").click(function(){
	
			var inputRadio6thName = 0;
			$('[name="workDoneOnVehiandServExpAtTimeOfDeli"]').each(function (){
				if ($(this).is(':checked')) inputRadio6thName++;
			});
			if (inputRadio6thName == 0) {
			Lobibox.notify('warning', {
						continueDelayOnInactiveTab: true,
						msg: 'Please choose any one.'
					});
					return false;
				
			}else{
				$("#6thquestionDiv").hide();
				$("#7thquestionDiv").show();
			}
	

	});

	$("#BackTo6thQuestion").click(function(){
//$('[name="workDoneOnVehiandServExpAtTimeOfDeli"]').prop('checked',false);
		$("#6thquestionDiv").show();
		$("#7thquestionDiv").hide();


	});


$("#NextTo8thQuestion").click(function(){
		var inputRadio7thName = 0;
			$('[name="ratePerformanceOfSA"]').each(function (){
				if ($(this).is(':checked')) inputRadio7thName++;
			});
			if (inputRadio7thName == 0) {
			Lobibox.notify('warning', {
						continueDelayOnInactiveTab: true,
						msg: 'Please choose any one.'
					});
					return false;
				
			}else{
				$("#7thquestionDiv").hide();
				$("#8thquestionDiv").show();
			}

});

$("#BackTo7thQuestion").click(function(){
//$('[name="ratePerformanceOfSA"]').prop('checked',false);
$("#7thquestionDiv").show();
$("#8thquestionDiv").hide();


});
<!----------------------------------Picku UP Jquery--------------->
$("#NextToPickup2ndQuest").click(function(){


			var inputRadiopickup = 0;
				$('[name="vehiclePerformancePickUp"]').each(function (){
					if ($(this).is(':checked')) inputRadiopickup++;
				});
				var inputRadiopickupOther = 0;
				$('[name="isDemandOfSerDoneInLVisitpickup"]').each(function (){
					if ($(this).is(':checked')) inputRadiopickupOther++;
				});
				
				if (inputRadiopickup == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one.'
						});
						return false;
					
				}if($('#Pickup1QuestNameDesatis').prop('checked')){
					
					if (inputRadiopickupOther == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one.'
						});
						return false;
					
				}if($('#pickupRepair1No').prop('checked')){
					var piktext=$('#pickupTextRemark').val();
					if(piktext ==''){
						Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Enter Text.'
						});
						$('#pickupTextRemark').focus();
						return false;
						
					}else{
						
						$("#pickup2ndQuestDiv").show();	
						$("#PickUp1Div").hide();
					}
				}else{
					$("#pickup2ndQuestDiv").show();
				$("#PickUp1Div").hide();
				}
					
				}else{
				$("#pickup2ndQuestDiv").show();
				$("#PickUp1Div").hide();	
				}



});

$("#NextTo3rdQuestionPickup").click(function(){
	
		var QpickUp = 0;
				$('[name="iscallMadeToFixPickUptime"]').each(function (){
					if ($(this).is(':checked')) QpickUp++;
				});
				
				if (QpickUp == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one.'
						});
						return false;
					
				}else{

$("#pickup3rdQuestPickUpDiv").show();
$("#pickup2ndQuestDiv").hide();
				}

});
$("#NextTo4thQuestionPickup").click(function(){

			var QpickUp3 = 0;
				$('[name="isCallRecievedBeforeVehWorkshop"]').each(function (){
					if ($(this).is(':checked')) QpickUp3++;
				});
				
				if (QpickUp3 == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one.'
						});
						return false;
					
				}else{


$("#pickup4thQuestPickUpDiv").show();
$("#pickup3rdQuestPickUpDiv").hide();
				}


});
$("#NextTo5thQuestionPickup").click(function(){
	
	var QpickUp4 = 0;
				$('[name="chargesInfoExplainedBeforeServiceMMS"]').each(function (){
					if ($(this).is(':checked')) QpickUp4++;
				});
				
				if (QpickUp4 == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one.'
						});
						return false;
					
				}else{

$("#pickup5thQuestPickUpDiv").show();
$("#pickup4thQuestPickUpDiv").hide();
				}
});
$("#NextTo6thQuestionPickup").click(function(){
	var QpickUp5 = 0;
				$('[name="isVehicleReadyAsPromisedDate"]').each(function (){
					if ($(this).is(':checked')) QpickUp5++;
				});
				
				if (QpickUp5 == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one.'
						});
						return false;
					
				}if($('#5thPickUpNameNo').prop('checked')){
					var pickDates = $('#PickUpDivDate').val();
					var pickTimes = $('#PickUpDivMin').val();
					var pickTexts = $('#PickUpDivText').val();
					if(pickDates =='' || pickTimes ==''|| pickTexts ==''){
						Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Select Date,Time And Enter Text.'
						});
						return false;
					}else{
						$("#pickup6thQuestPickUpDiv").show();
						$("#pickup5thQuestPickUpDiv").hide();
					}
				}else{
					
				$("#pickup6thQuestPickUpDiv").show();
				$("#pickup5thQuestPickUpDiv").hide();
				}
});

$("#NextTo7thQuestionPickup").click(function(){
	
	var QpickUp6 = 0;
				$('[name="isChargesAsEstimatedpickUp"]').each(function (){
					if ($(this).is(':checked')) QpickUp6++;
				});
				
				if (QpickUp6 == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one.'
						});
						return false;
					
				}else{
					
					$("#pickup7thQuestPickUpDiv").show();
					$("#pickup6thQuestPickUpDiv").hide();
				}
	

});

$("#NextTo8thQuestionPickup").click(function(){
	
	var QpickUp7 = 0;
				$('[name="isChargesAndRepairAsMentionedPickup"]').each(function (){
					if ($(this).is(':checked')) QpickUp7++;
				});
				
				if (QpickUp7 == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one.'
						});
						return false;
					
				}else{
$("#pickup8thQuestPickUpDiv").show();
$("#pickup7thQuestPickUpDiv").hide();
				}
});

$("#NextTo9thQuestionPickup").click(function(){
	var QpickUp8 = 0;
				$('[name="satisfiedWithWashingPickup"]').each(function (){
					if ($(this).is(':checked')) QpickUp8++;
				});
				
				if (QpickUp8 == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one.'
						});
						return false;
					
				}else{
$("#pickup9thQuestPickUpDiv").show();
$("#pickup8thQuestPickUpDiv").hide();
				}
});

$("#NextTo10thQuestionPickup").click(function(){
	
	var QpickUp9 = 0;
				$('[name="isPickUpDropStaffCourteous"]').each(function (){
					if ($(this).is(':checked')) QpickUp9++;
				});
				
				if (QpickUp9 == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one.'
						});
						return false;
					
				}else{
$("#pickup10thQuestPickUpDiv").show();
$("#pickup9thQuestPickUpDiv").hide();
				}
});

$("#NextTo11thQuestionPickup").click(function(){
	var QpickUp10 = 0;
				$('[name="isQualityOfPickUpDone"]').each(function (){
					if ($(this).is(':checked')) QpickUp10++;
				});
				
				if (QpickUp10 == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one.'
						});
						return false;
					
				}else{
$("#pickup11thQuestPickUpDiv").show();
$("#pickup10thQuestPickUpDiv").hide();
				}
});


$("#BackTo1QuestionPickup").click(function(){
//$('[name="vehiclePerformancePickUp"]').prop('checked',false);
//$('[name="isDemandOfSerDoneInLVisitpickup"]').prop('checked',false);
$("#PickUp1Div").show();
$("#pickupTextRemark").val('');
$("#pickup2ndQuestDiv").hide();
$("#PickUpDisSatisfiedNo").hide();
$("#pickupRepareNoDiv").hide();
$("#pickupRepareYesDiv").hide();


});



$("#BackTo2ndQuestionPickup").click(function(){
//$('[name="iscallMadeToFixPickUptime"]').prop('checked',false);
$("#pickup2ndQuestDiv").show();
$("#pickup3rdQuestPickUpDiv").hide();

});

$("#BackTo3rdQuestionPickup").click(function(){
//$('[name="isCallRecievedBeforeVehWorkshop"]').prop('checked',false);
$("#pickup3rdQuestPickUpDiv").show();
$("#pickup4thQuestPickUpDiv").hide();

});

$("#BackTo4thQuestionPickup").click(function(){
//$('[name="chargesInfoExplainedBeforeServiceMMS"]').prop('checked',false);
$("#pickup4thQuestPickUpDiv").show();
$("#pickup5thQuestPickUpDiv").hide();
});

$("#BackTo5thQuestionPickup").click(function(){
//$('[name="isVehicleReadyAsPromisedDate"]').prop('checked',false);
$("#PickUpDivDate").val('');
$("#PickUpDivMin").val('');
$("#PickUpDivText").val('');
$("#pickup5thQuestPickUpDiv").show();
$("#pickup6thQuestPickUpDiv").hide();
$("#5thPickUpDivNo").hide();
});

$("#BackTo6thQuestionPickup").click(function(){
//$('[name="isChargesAsEstimatedpickUp"]').prop('checked',false);
$("#pickup6thQuestPickUpDiv").show();
$("#pickup7thQuestPickUpDiv").hide();
});

$("#BackTo7thQuestionPickup").click(function(){
//$('[name="isChargesAndRepairAsMentionedPickup"]').prop('checked',false);
$("#pickup7thQuestPickUpDiv").show();
$("#pickup8thQuestPickUpDiv").hide();
});
$("#BackTo8thQuestionPickup").click(function(){
//$('[name="satisfiedWithWashingPickup"]').prop('checked',false);
$("#pickup8thQuestPickUpDiv").show();
$("#pickup9thQuestPickUpDiv").hide();
});

$("#BackTo9thQuestionPickup").click(function(){
//$('[name="isPickUpDropStaffCourteous"]').prop('checked',false);
$("#pickup9thQuestPickUpDiv").show();
$("#pickup10thQuestPickUpDiv").hide();
});
$("#BackTo10thQuestionPickup").click(function(){
//$('[name="isQualityOfPickUpDone"]').prop('checked',false);
$("#pickup10thQuestPickUpDiv").show();
$("#pickup11thQuestPickUpDiv").hide();
});
<!---------------------MMS jquery------------------------------------------------------>
 $("input[name$='vehiclePerformance']").click(function() {
        var varMMS1QuestName = $(this).val();
		if(varMMS1QuestName=="Satisfied")
		{
		
			$("#MMSSatisfiedYes").show();
			$("#MMSDisSatisfiedNo").hide();
			$("#MMSRepareNoDiv").hide();
			$("#MMSRepareYesDiv").hide();
			
		}
			if(varMMS1QuestName=="Dissatisfied")
		{
			$("#MMSSatisfiedYes").hide();
			$("#MMSDisSatisfiedNo").show();
			$("#MMSRepareNoDiv").hide();
			$("#MMSRepareYesDiv").hide();
			
		}
	});
 $("input[name$='isDemandOfSeriviceDoneInLastVisit']").click(function() {
         var varMMSRepair1 = $(this).val();
		if(varMMSRepair1=="Yes")
		{
		
			$("#MMSRepareYesDiv").show();
			$("#MMSRepareNoDiv").hide();
			
		}
			if(varMMSRepair1=="No")
			
		{
			$("#MMSRepareNoDiv").show();
			$("#MMSRepareYesDiv").hide();
			
		}
	});
	$("input[name$='isTimeTakenForServiceReasonable']").click(function() {
         var var5thMMSName = $(this).val();
		if(var5thMMSName=="Yes")
		{
		
			$("#5thMMSDivNo").hide();
			
		}
			if(var5thMMSName=="No")
		{
			$("#5thMMSDivNo").show();
			
		}
	});
	$("#BackTo1stQMMS").click(function(){
		$("#MMS1Div").hide();
		$("#PSFYesNamaskarYesDiv").show();
	

	});
	
$("#NextToMMS2ndQuest").click(function(){


var inputRadiomms = 0;
				$('[name="vehiclePerformance"]').each(function (){
					if ($(this).is(':checked')) inputRadiomms++;
				});
				var inputRadiommsOther = 0;
				$('[name="isDemandOfSeriviceDoneInLastVisit"]').each(function (){
					if ($(this).is(':checked')) inputRadiommsOther++;
				});
				
				if (inputRadiomms == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one.'
						});
						return false;
					
				}if($('#MMS1QuestNameDisatis').prop('checked')){
					
					if (inputRadiommsOther == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one.'
						});
						return false;
					
				}if($('#MMSRepair1No').prop('checked')){
					var piktextMms=$('#mmsTextVal').val();
					if(piktextMms ==''){
						Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Enter Text.'
						});
						return false;
						
					}else{
						
						$("#MMS2ndQuestDiv").show();
						$("#MMS1Div").hide();
					}
				}else{
				$("#MMS2ndQuestDiv").show();
				$("#MMS1Div").hide();
				}
					
				}else{
				$("#MMS2ndQuestDiv").show();
				$("#MMS1Div").hide();	
				}

});

$("#NextTo3rdQuestionMMS").click(function(){


			var MMS2QuestName1 = 0;
				$('[name="isAppointmentReceviedAsReq"]').each(function (){
					if ($(this).is(':checked')) MMS2QuestName1++;
				});
				
				if (MMS2QuestName1 == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one.'
						});
						return false;
					
				}else{
$("#MMS3rdQuestPickUpDiv").show();
$("#MMS2ndQuestDiv").hide();
				}

});
$("#NextTo4thQuestionMMS").click(function(){

var MMS2QuestName2 = 0;
				$('[name="iscallMadeOneDayBeforeService"]').each(function (){
					if ($(this).is(':checked')) MMS2QuestName2++;
				});
				
				if (MMS2QuestName2 == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one.'
						});
						return false;
					
				}else{
$("#MMS4thQuestPickUpDiv").show();
$("#MMS3rdQuestPickUpDiv").hide();
				}


});
$("#NextTo5thQuestionMMS").click(function(){
	 var MMS2QuestName3 = 0;
				$('[name="chargesInfoExplainedBeforeService"]').each(function (){
					if ($(this).is(':checked')) MMS2QuestName3++;
				});
				
				if (MMS2QuestName3 == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one.'
						});
						return false;
					
				}else{
$("#MMS5thQuestPickUpDiv").show();
$("#MMS4thQuestPickUpDiv").hide();
				}
});
$("#NextTo6thQuestionMMS").click(function(){
	 var MMS2QuestName3 = 0;
				$('[name="isTimeTakenForServiceReasonable"]').each(function (){
					if ($(this).is(':checked')) MMS2QuestName3++;
				});
				
				if (MMS2QuestName3 == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one.'
						});
						return false;
					
				}if($('#5thMMSNameNoData').prop('checked')){
					var mmsDates = $('#5thMMSNameHourData').val();
					var mmsTimes = $('#5thMMSNameMinData').val();
					var mmsTexts = $('#5thMMSNameTextData').val();
					if(mmsDates =='' || mmsTimes ==''|| mmsTexts ==''){
						Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Select Date,Time And Enter Text.'
						});
						return false;
					}else{
						$("#MMS6thQuestPickUpDiv").show();
						$("#MMS5thQuestPickUpDiv").hide();
					}
				}else{
	
						$("#MMS6thQuestPickUpDiv").show();
						$("#MMS5thQuestPickUpDiv").hide();
				}
							
});

$("#NextTo7thQuestionMMS").click(function(){
	var MMS2QuestName4 = 0;
				$('[name="isChargesAsEstimated"]').each(function (){
					if ($(this).is(':checked')) MMS2QuestName4++;
				});
				
				if (MMS2QuestName4 == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one.'
						});
						return false;
					
				}else{
$("#MMS7thQuestPickUpDiv").show();
$("#MMS6thQuestPickUpDiv").hide();
				}
});

$("#NextTo8thQuestionMMS").click(function(){
	
	 var MMS2QuestName5 = 0;
				$('[name="isChargesAndRepairAsMentioned"]').each(function (){
					if ($(this).is(':checked')) MMS2QuestName5++;
				});
				
				if (MMS2QuestName5 == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one.'
						});
						return false;
					
				}else{
	
$("#MMS8thQuestPickUpDiv").show();
$("#MMS7thQuestPickUpDiv").hide();
				}
});

$("#NextTo9thQuestionMMS").click(function(){
	 var MMS2QuestName6 = 0;
				$('[name="satisfiedWithWashing"]').each(function (){
					if ($(this).is(':checked')) MMS2QuestName6++;
				});
				
				if (MMS2QuestName6 == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one.'
						});
						return false;
					
				}else{
$("#MMS9thQuestPickUpDiv").show();
$("#MMS8thQuestPickUpDiv").hide();
				}
});

$("#NextTo10thQuestionMMS").click(function(){
	 var MMS2QuestName7 = 0;
				$('[name="isworkshopStaffCourteous"]').each(function (){
					if ($(this).is(':checked')) MMS2QuestName7++;
				});
				
				if (MMS2QuestName7 == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one.'
						});
						return false;
					
				}else{
	
$("#MMS10thQuestPickUpDiv").show();
$("#MMS9thQuestPickUpDiv").hide();
				}
});

$("#NextTo11thQuestionMMS").click(function(){
	 var MMS2QuestName8 = 0;
				$('[name="satisfiedWithQualityOfDoorService"]').each(function (){
					if ($(this).is(':checked')) MMS2QuestName8++;
				});
				
				if (MMS2QuestName8 == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one.'
						});
						return false;
					
				}else{
	
$("#MMS11thQuestPickUpDiv").show();
$("#MMS10thQuestPickUpDiv").hide();
				}
});


$("#BackTo1QuestionMMS").click(function(){


$("#MMS1Div").show();
$("#MMS2ndQuestDiv").hide();
$("#MMSDisSatisfiedNo").hide();
$("#MMSRepareNoDiv").hide();
$("#mmsTextVal").val('');

});



$("#BackTo2ndQuestionMMS").click(function(){

$("#MMS2ndQuestDiv").show();
$("#MMS3rdQuestPickUpDiv").hide();

});

$("#BackTo3rdQuestionMMS").click(function(){

$("#MMS3rdQuestPickUpDiv").show();
$("#MMS4thQuestPickUpDiv").hide();

});

$("#BackTo4thQuestionMMS").click(function(){

$("#MMS4thQuestPickUpDiv").show();
$("#MMS5thQuestPickUpDiv").hide();
});

$("#BackTo5thQuestionMMS").click(function(){

$("#MMS5thQuestPickUpDiv").show();
$("#MMS6thQuestPickUpDiv").hide();
$("#5thMMSDivNo").hide();
$("#5thMMSNameHourData").val('');
$("#5thMMSNameMinData").val('');
$("#5thMMSNameTextData").val('');

});

$("#BackTo6thQuestionMMS").click(function(){

$("#MMS6thQuestPickUpDiv").show();
$("#MMS7thQuestPickUpDiv").hide();
});

$("#BackTo7thQuestionMMS").click(function(){

$("#MMS7thQuestPickUpDiv").show();
$("#MMS8thQuestPickUpDiv").hide();
});
$("#BackTo8thQuestionMMS").click(function(){

$("#MMS8thQuestPickUpDiv").show();
$("#MMS9thQuestPickUpDiv").hide();
});

$("#BackTo9thQuestionMMS").click(function(){

$("#MMS9thQuestPickUpDiv").show();
$("#MMS10thQuestPickUpDiv").hide();
});
$("#BackTo10thQuestionMMS").click(function(){

$("#MMS10thQuestPickUpDiv").show();
$("#MMS11thQuestPickUpDiv").hide();
});

$("#SaveInCompleteServey").click(function(){
	
	Lobibox.confirm({
	msg: "Are you sure you want Save as a Incomplete Survey?",
});

});


$("#6thdayUpselAppo").click(function(){
		
			
			var selectedValSelMms=$('#MMSRatingSelect').val();
						if(selectedValSelMms == '0'){
							Lobibox.notify('warning', {
								continueDelayOnInactiveTab: true,
								msg: 'Please Select From List.'
							});
							return false;
							
						}
						else{
						
			$("#UpsellPSF6ththday").show();
			$("#BackTo6thSatisfed").show();
			$("#MMS11thQuestPickUpDiv").hide();
			$("#BackToPickUpsel").hide();
			$("#BackToSelfDriveINUpsel").hide();
						}
	});
		$("#BackTo6thSatisfed").click(function(){
		
			$("#UpsellPSF6ththday").hide();
			$("#MMS11thQuestPickUpDiv").show();
		
	});
  
    $("#PSFUpsellPickup").click(function(){
		var selValPickup=$('#PickUpRatingSelect').val();
						if(selValPickup == '0'){
							Lobibox.notify('warning', {
								continueDelayOnInactiveTab: true,
								msg: 'Please Select From List.'
							});
							return false;
							
						}
						else{	
		
			$("#UpsellPSF6ththday").show();
			$("#BackToPickUpsel").show();
			$("#pickup11thQuestPickUpDiv").hide();
			$("#BackToSelfDriveINUpsel").hide();
		}
	});
	$("#NextUpsellSelfDriveIN").click(function(){
			if(document.getElementById('11thNameSatis').checked) {
			
		$("#UpsellPSF6ththday").show();
			$("#BackToSelfDriveINUpsel").show();
			$("#9questionSatisfiedDiv").hide();
		}
		else if(document.getElementById('11thNameDesatis').checked) {
		$("#UpsellPSF6ththday").show();
			$("#BackToSelfDriveINUpsel").show();
			$("#9questionSatisfiedDiv").hide();
		}
		
		else{
				Lobibox.notify('warning', {
						continueDelayOnInactiveTab: true,
						msg: 'Please check one.'
					});
					return false;
			}
			
		
	});
	
	$("#BackToSelfDriveINUpsel").click(function(){
		
			$("#9questionSatisfiedDiv").show();
			$("#UpsellPSF6ththday").hide();
		
	});
	$("#BackToPickUpsel").click(function(){
		
			$("#pickup11thQuestPickUpDiv").show();
			$("#UpsellPSF6ththday").hide();
		
	});
	
	
	
/*   if(document.getElementById('CaptureLeadPSFYES').checked) {
	  $("#LeadDivAlreadyService").show();
	  
  }
  else if(document.getElementById('CaptureLeadPSFNO').checked) {
	  $("#LeadDivAlreadyService").show();
	  $('.PSFUpsell :checked').removeAttr('checked');
	  
	  
  } */

  $("input[name='CaptureLeadPSF']").click(function(){
	var6thPSF =$(this).val();
	if(var6thPSF=="Capture Lead Yes PSF"){
		$("#LeadDivAlreadyService").show();
		
	}
	else if(var6thPSF=="Capture Lead No PSF"){
		$("#LeadDivAlreadyService").hide();
		$("#InsuranceSelectAlreadyService").hide();
		$("#WARRANTYSelectAlreadyService").hide();
		$("#EXCHANGEIDSelectAlreadyService").hide();
		$("#VASTagToSelectAlreadyService").hide();
		$("#ReFinanceSelectAlreadyService").hide();
		$("#UsedCarSelectAlreadyService").hide();
		$("#LoanSelectAlreadyService").hide();
		$('.PSFUpsell').prop('checked',false);
		
				
	}
	else{}
	
});

   
  var urlPath = "/complaintCategoryList/63";
  $.ajax({
      url: urlPath
  }).done(function (leadlist) {
  	
  	  $('#Compliant_Category_id').empty();
          var dropdown = document.getElementById("Compliant_Category_id");
          dropdown[0] = new Option('--Select--', '0');
          for(var i=0;i<leadlist.length;i++){
         
                  dropdown[dropdown.length] = new Option(leadlist[i].disposition,leadlist[i].id);
               
          }
          
          $('#cre_Compliant_Category_id').empty();
          var dropdown1 = document.getElementById("cre_Compliant_Category_id");
          dropdown1[0] = new Option('--Select--', '0');
          for(var i=0;i<leadlist.length;i++){
         
                  dropdown1[dropdown1.length] = new Option(leadlist[i].disposition,leadlist[i].id);
               
          }
          
          
  	
  	
  });
  
  


});


//Validation for submit

//Feedback Provided


$('#BackTo10thQuestionMMSSubmit0').click(function(){
	var vFB=$("#dropDownCheckedSelect1").val();
	
			if (vFB == 0) {
			Lobibox.notify('warning', {
						continueDelayOnInactiveTab: true,
						msg: 'Please select feedback rating.'
					});
					return false;
				
			} else{
                    $.blockUI(); 
                            
                        }
});
//Not interested

$('#notInterestedSubmit').click(function(){
	
	var custFBD=$("#custFB").val();
	var cretFB=$("#creRemarks").val();
			if (custFBD =='') {
			Lobibox.notify('warning', {
						continueDelayOnInactiveTab: true,
						msg: 'Please Enter Customer Feedback.'
					});
					return false;
				
			}
			if (cretFB =='') {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Enter CRE Remark.'
						});
						return false;
					
				}
			$.blockUI(); 
                
                            
                        
});

$('#noresolutionSubmit').click(function(){
	
	
	var chkincSubmit = 0;
	$('[name="reasonOfDissatification"]').each(function (){
		if ($(this).is(':checked')) chkincSubmit++;
	});
	if (chkincSubmit == 0) {
	Lobibox.notify('warning', {
				continueDelayOnInactiveTab: true,
				msg: 'Please check one.'
			});
			return false;
			
		
	}
	 $.blockUI(); 
	
});




$('#notCallMeLaterSubmit').click(function(){
	var dateVal=$('#selDate').val();
	var timeVal=$('#FollowUpTime').val();
	if(dateVal ==''){
	Lobibox.notify('warning', {
	msg: 'Please select the date.'
});				
		return false;
	}
	if(timeVal ==''){
		Lobibox.notify('warning', {
		msg: 'Please select the time.'
});	
		return false;
	}
		  $.blockUI(); 
});

//Rework

$('#reWorkSubmit').click(function(){
	
	
	var workVal=$('#workshop_id_Data').val();
	var rewMoVal=$('#reworkmodeId').val();
	var rewAddVal=$('#reworkAddId').val();
	var schDVal=$('#schDateId').val();
	var schTVal=$('#schTimeId').val();
	if(workVal =='' || rewMoVal == '' ||rewAddVal==''||schDVal==''||schTVal==''){
		Lobibox.notify('warning', {
		msg: 'Please enter all Fields.'
	});				
			return false;
		}
	 $.blockUI(); 
	
});


//reSolvedSubmit
$('#reSolvedSubmit').click(function(){
	
	
	var dtVal=$('#dateandtimepicker').val();
	var resByVal=$('#resolvedById').val();
	var resModVal=$('#resolutionModeId').val();
	var DVal=$('#discID').val();
	var benVal=$('#benefId').val();
	var comVal=$('#Compliant_Category_id').val();
	
	if(dtVal =='' || resByVal == '' ||resModVal==''||DVal==''||benVal=='' ||comVal==''){
		Lobibox.notify('warning', {
		msg: 'Please enter all Fields.'
	});				
			return false;
		}
	
	 $.blockUI(); 
});




