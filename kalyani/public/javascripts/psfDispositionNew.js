$(document).ready(function(){

var varPSFYes15thDay15thDay="";
var varPSFYes15thDay='';
var PSFtest15thDay='';
var remarkOthers='';
var var15thDaySat='';
var varOther15th='';
var var15thdatetime='';
var var15thtimeDate='';
var varSatisfiedRadio =null;

$("input[name='vehiclePerformanceAfterService']").click(function(){
	var15thDaySat =$(this).val();
	if(var15thDaySat=="Satisfied"){
		$("#15thDayRatting").val('10');
		
	}
	else{
		$("#15thDayRatting").val('0');
		
	}
	
});


 $("input[name='isContacted']").click(function() {

         PSFtest15thDay = $(this).val();
		if(PSFtest15thDay=="Yes")
		{
			$("#15thDayPSFYesTalk").show();
			$("#15thDayPSFNotSpeachDiv").hide();
						$("input:radio[name='PSFDispositon']").each(function(i) {
       this.checked = false;
});
			
		}
			if(PSFtest15thDay=="No")
		{
			$("#15thDayPSFNotSpeachDiv").show();
			$("#15thDayPSFYesTalk").hide();
			$("#15thDayFormPSF").hide();
			$("input:radio[name='disposition']").each(function(i) {
       this.checked = false;
});
		}
		
});
$("input[name='PSFDispositon']").click(function() {

         remarkOthers = $(this).val();
		if(remarkOthers=="NoOther")
		{
			$("#15thDayPSFOtherDiv").show();
					
		}
		else{
			
				$("#15thDayPSFOtherDiv").hide();
		}
		
});


 $("input[name='disposition']").click(function() {

         varPSFYes15thDay = $(this).val();
		if(varPSFYes15thDay=="PSF_Yes")
		{
			$("#15thDayPSFYesNamaskarNoDiv").hide();
				$("#15thDayNotInterestedBtns").hide();
				$("#15thDayFormPSF").show();
				$("#15thDayPSFYesTalk").hide();
				$("#PSFconnectCall15th").hide();
				$("#time_FromIn15thDay").val('');
				$("#time_ToIn15thDay").val('');
				$("#15thDayremarksValueForNoIntrst").val('');
				
				
				
				
			
		}
			if(varPSFYes15thDay=="Call Me Later")
		{
			$("#15thDayPSFYesNamaskarNoDiv").show();
				$("#15thDayNotInterestedBtns").hide();
				$("#15thDayFormPSF").hide();
				$("#PSFconnectCall15th").hide();
				$("#15thDayPSFYesTalk").hide();
				$("#15thDayPSFYesTalk").val('');
				$("#15thDayremarksValueForNoIntrst").val('');
				$("#remarks15thDay").val('');
				$("#15thDayRatting").val('0');
		$("input:radio[name='vehiclePerformanceAfterService']").each(function(i) {
       this.checked = false;
});
			
		}
	
			if(varPSFYes15thDay=="Not Interested")
		{
			$("#15thDayPSFYesNamaskarNoDiv").hide();
				$("#15thDayNotInterestedBtns").show();
				$("#15thDayFormPSF").hide();
				$("#PSFconnectCall15th").hide();
				$("#15thDayPSFYesTalk").hide();
				$("#time_FromIn15thDay").val('');
				$("#time_ToIn15thDay").val('');
				$("#remarks15thDay").val('');
				$("#15thDayRatting").val('0');
				
	$("input:radio[name='vehiclePerformanceAfterService']").each(function(i) {
       this.checked = false;
});
		}
		
});
$("#15thDayPSFBack").click(function(){
	$("#PSFconnectCall15th").show();
	$("#15thDayPSFYesTalk").show();
	$("#15thDayFormPSF").hide();
	$("input:radio[name='disposition']").each(function(i) {
       this.checked = false;
});
	
});
$("#15thDayBackToDidUtlakPSF").click(function(){
	$("#PSFconnectCall15th").show();
	$("#15thDayPSFYesTalk").show();
	$("#15thDayPSFYesNamaskarNoDiv").hide();
		$("input:radio[name='disposition']").each(function(i) {
       this.checked = false;
});
	
});

$("#15thDayBackToDidUtlakPSF1").click(function(){
	$("#PSFconnectCall15th").show();
	$("#15thDayPSFYesTalk").show();
	$("#15thDayNotInterestedBtns").hide();
		$("input:radio[name='disposition']").each(function(i) {
       this.checked = false;
});
	
});

$("#15thDaynotCallMeLaterSubmit").click(function(){

	var15thdatetime=$("#time_FromIn15thDay").val();
	var15thtimeDate=$("#time_ToIn15thDay").val();
	if(var15thdatetime!='' && var15thtimeDate!='')
	{
		$.blockUI();
		
	}
	else{
		alert('Please Select Date and Time');
	}
	
});


	$('#15thDaytalkCustomerSubmit').click(function(){
	 var chkincSubmit = 0;
	$('[name="PSFDispositon"]').each(function (){
	if ($(this).is(':checked')) chkincSubmit++;
	});
	if (chkincSubmit == 0) {
	// Lobibox.notify('warning', {
	// continueDelayOnInactiveTab: true,
	// msg: 'Please check one.'
	// });
	alert('Please check one.');
	return false;


	} else{
	if($("#15thDayOther1").prop('checked')){
	 
	var textNoOthers = $('#15thDayOtherPSF').val();
	if(textNoOthers ==''){
	// Lobibox.notify('warning', {
	// continueDelayOnInactiveTab: true,
	// msg: 'Remarks Shouldnot Empty.'
	// });
	alert('Remarks Shouldnot Empty.');
	return false;

	}
	}
										
			$.blockUI(); 
										
									}
	});

	
	
	$("#BackTo15thSatisfed").click(function(){
		
			$("#UpsellPSF15thday").hide();
			$("#15thDayFormPSF").show();
		
	});
	
	$("#15thDayPSFSatisfiedUpsell").click(function(){
		
		if(document.getElementById('SatisfiedPSF15').checked) {
			$("#15thDayFormPSF").hide();
		$("#UpsellPSF15thday").show();
		}
		else if(document.getElementById('DissatisfiedPSF15').checked) {
			$("#15thDayFormPSF").hide();
		$("#UpsellPSF15thday").show();
		}
		else{
				Lobibox.notify('warning', {
						continueDelayOnInactiveTab: true,
						msg: 'Please check one.'
					});
					return false;
			}
				
		
	});
	$("#BackTo30thSatisfed").click(function(){
		
			$("#UpsellPSF30thday").hide();
			$("#15thDayFormPSF").show();
		
	});
	
	$("#30dthDayPSFSatisfiedUpsell").click(function(){
		
		if(document.getElementById('SatisfiedYES1').checked) {
			$("#15thDayFormPSF").hide();
		$("#UpsellPSF30thday").show();
		}
		else if(document.getElementById('SatisfiedNo1').checked) {
			$("#15thDayFormPSF").hide();
		$("#UpsellPSF30thday").show();
		}
		else{
				Lobibox.notify('warning', {
						continueDelayOnInactiveTab: true,
						msg: 'Please check one.'
					});
					return false;
			}
				
		
	});
	$("#30thDayPSFSatisfied").click(function(){
		
		if(document.getElementById('UpsellPSFyes').checked) {
		
		}
		else if(document.getElementById('UpsellPSFyes').checked) {
		
		}
		
		else{
				Lobibox.notify('warning', {
						continueDelayOnInactiveTab: true,
						msg: 'Please check one.'
					});
					return false;
			}
	});		
	
	
	$("input[name='LeadYesPSF30thDay']").click(function(){
	var var30thPSF =$(this).val();
	if(var30thPSF=="Yes"){
		$("#LeadDiv30ThDayPSF").show();
		
	}
	else if(var30thPSF=="No"){
		$("#LeadDiv30ThDayPSF").hide();
		$("#InsuranceSelectAlreadyService").hide();
		$("#WARRANTYSelectAlreadyService").hide();
		$("#EXCHANGEIDSelectAlreadyService").hide();
		$("#VASTagToSelectAlreadyService").hide();
		$("#ReFinanceSelectAlreadyService").hide();
		$("#UsedCarSelectAlreadyService").hide();
		$("#LoanSelectAlreadyService").hide();
		$('.PSFUpsell30th').prop('checked',false);
		
				
	}
	else{
		$("#LeadDiv30ThDayPSF").hide();
	}
	
});
	$("input[name='LeadYes15thDayPSF']").click(function(){
	var var15thPSF =$(this).val();
	if(var15thPSF=="Yes"){
		$("#LeadDiv15thDayPSF").show();
		
	}
	else if(var15thPSF=="No"){
		$("#LeadDiv15thDayPSF").hide();
		$("#InsuranceSelectAlreadyService").hide();
		$("#WARRANTYSelectAlreadyService").hide();
		$("#EXCHANGEIDSelectAlreadyService").hide();
		$("#VASTagToSelectAlreadyService").hide();
		$("#ReFinanceSelectAlreadyService").hide();
		$("#UsedCarSelectAlreadyService").hide();
		$("#LoanSelectAlreadyService").hide();
		$('.Upsell15thPSF').prop('checked',false);
		
				
	}
	else{
		$("#LeadDiv15thDayPSF").hide();
	}
	
});
$("#BackTo10thQuestionMMSSubmit0").click(function(){
		
		if(document.getElementById('CaptureLeadPSFYES').checked) {
			
		
		}
		else if(document.getElementById('CaptureLeadPSFNO').checked) {
		
		}
		
		else{
				Lobibox.notify('warning', {
						continueDelayOnInactiveTab: true,
						msg: 'Please check one.'
					});
					return false;
			}
	});

$("#15thDayPSFSubmit").click(function(){
		
		if(document.getElementById('15thDayUpsellYes').checked) {
			
		
		}
		else if(document.getElementById('15thDayUpsellNo').checked) {
		
		}
		
		else{
				Lobibox.notify('warning', {
						continueDelayOnInactiveTab: true,
						msg: 'Please check one.'
					});
					return false;
			}
	});		

	$("#30thDayPSFSatisfied").click(function(){
		
		if(document.getElementById('Upsell30thDayYes').checked) {
			
		
		}
		else if(document.getElementById('Upsell30thDayNo').checked) {
		
		}
		
		else{
				Lobibox.notify('warning', {
						continueDelayOnInactiveTab: true,
						msg: 'Please check one.'
					});
					return false;
			}
	});
/////////////////////////////////30th Day PSF //////////////////////////////////////
	$('.single-input').timepicker({
		showPeriodLabels: false,
		});

});

