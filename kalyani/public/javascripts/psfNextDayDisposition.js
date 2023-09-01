$(document).ready(function(){
	$(".showOnlyFutureDate").datepicker({
		changeMonth: true,
		changeYear: true,
		maxDate: "+30d",
		minDate: '0',
		showAnim: 'slideDown',
		dateFormat: 'yy-mm-dd'
	  });
 $("input[name$='isContacted']").click(function() {
        var vYesPSFM = $(this).val();
		if(vYesPSFM=="Yes")
		{
			$("#MaruthiGoodM").show();
			$("#MaruthiNotConnect").hide();
			$("#MaruthiFirstQ").hide();
			
		}
		if(vYesPSFM=="No")
		{
			$("#MaruthiNotConnect").show();
			$("#MaruthiGoodM").hide();
			$("#MaruthiFirstQ").hide();
			
		}
		
});
$("input[name$='qM4_confirmingCustomer']").click(function() {
        var vSmahiPsf = $(this).val();

		if(vSmahiPsf=="Yes")
		{
			$("#ManhindarYesC").show();
			
			$("#Ques2FollowUp").hide();
			$("#onlyFollowUpDiv").hide();
			$("#FollowUpDtBtn1").hide();
			$("#MaruthiGoodM").hide();
		}
			if(vSmahiPsf=="No")
		{
			/* $("#Ques2FollowUp").show();
			$("#onlyFollowUpDiv").show();
			$("#FollowUpDtBtn1").show(); */
			$("#ManhindarYesC").hide();
			$("#MaruthiGoodM").hide();
		}
});
$("#NextToQuest3").click(function(){
if(document.getElementById('goodYes').checked)
{
	$("#ManhindarYesC").show();
	$("#FollowUpDtBtn1").hide();
	$("#MaruthiGoodM").hide();
}
else if(document.getElementById('goodNo').checked) 
{
	 $("#ManhindarYesC").show();
	$("#FollowUpDtBtn1").show(); 

	$("#MaruthiGoodM").hide();
}
else{
	Lobibox.notify('warning', {
		continueDelayOnInactiveTab: true,
		msg: 'Please Select Option.'
	});
	return false;
}
});
$("input[name$='qM4_confirmingCustomerRightTime']").click(function() {
        var vrighttm = $(this).val();
		if(vrighttm=="Yes")
		{
			$("#MahindraYesLastQ").show();
			$("#FalloUpDtBtn2").hide();
			$("#ManhindarYesC").hide();
			$("#Ques3FollowUp").hide();
			$("#onlyFollowUpDiv").hide();
		}
			if(vrighttm=="No")
		{
		
			$("#Ques3FollowUp").show();
			$("#onlyFollowUpDiv").show();
			$("#FalloUpDtBtn2").show();
			$("#MahindraYesLastQ").hide();
			$("#ManhindarYesC").hide();
			
			
		}
});

$("#NextToQuest4").click(function(){
if(document.getElementById('righttimeYes').checked)
{
			$("#MahindraYesLastQ").show();
			
			$("#FalloUpDtBtn2").hide();
			$("#ManhindarYesC").hide();
}
else if(document.getElementById('righttimeNo').checked) 
{				$("#FalloUpDtBtn2").show();
		
			$("#MahindraYesLastQ").hide();
			$("#ManhindarYesC").hide();
}
else{
	Lobibox.notify('warning', {
		continueDelayOnInactiveTab: true,
		msg: 'Please Select option.'
	});
	return false;
}
});
$("input[name$='qM1_SatisfiedWithQualityOfServ']").click(function() {
        var vSatisFyMahi = $(this).val();
		if(vSatisFyMahi=="Yes")
		{
			$("#SatisfiedYesMa").show();
			$("#MahindraNoLastQ").hide();
			$("#MahindraYesLastQ").hide();
		}
			if(vSatisFyMahi=="No")
		{
			$("#MahindraNoLastQ").show();
			$("#SatisfiedYesMa").hide();
			$("#MahindraYesLastQ").hide();
		}
});
$("#NextTOMahi5thQ").click(function(){
if(document.getElementById('satisfiedSID').checked)
{
			$("#SatisfiedYesMa").show();
			$("#MahindraNoLastQ").hide();
			$("#MahindraYesLastQ").hide();
}
else if(document.getElementById('satisfiedNID').checked) 
{
			$("#MahindraNoLastQ").show();
			$("#SatisfiedYesMa").hide();
			$("#MahindraYesLastQ").hide();
}
else{
	Lobibox.notify('warning', {
		continueDelayOnInactiveTab: true,
		msg: 'Please Select option.'
	});
	return false;
}
});


$("input[name$='qM2_ReasonOfAreaOfImprovement']").click(function() {
        var vQualityMahi = $(this).val();
		if(vQualityMahi=="Quality Of Serive")
		{
			$("#qualityDiv1").show();
			$("#ProductDiv2").hide();
			$("#GeneralDiv3").hide();
			$("#MahindraNoLastQ").hide();
			$("#GeneralPlaceDiv").hide();
		
		}
			if(vQualityMahi=="Product")
		{
			$("#ProductDiv2").show();
			$("#qualityDiv1").hide();
			$("#GeneralDiv3").hide();
			$("#MahindraNoLastQ").hide();
			$("#GeneralPlaceDiv").hide();
			
		}
			if(vQualityMahi=="General")
		{
			$("#GeneralPlaceDiv").show();
			$("#OnlyAppoDateAndTm").show();
			$("#ProductDiv2").hide();
			$("#qualityDiv1").hide();
			$("#MahindraNoLastQ").hide();
			
		}
});
$("#NextToSubQMAhai").click(function(){
if(document.getElementById('serviceQualityID').checked)
{
			$("#qualityDiv1").show();
			$("#ProductDiv2").hide();
			$("#GeneralDiv3").hide();
			$("#MahindraNoLastQ").hide();
			$("#GeneralPlaceDiv").hide();
}
else if(document.getElementById('ProductIDMa').checked) 
{
			$("#ProductDiv2").show();
			$("#qualityDiv1").hide();
			$("#GeneralDiv3").hide();
			$("#MahindraNoLastQ").hide();
			$("#GeneralPlaceDiv").hide();
}
else if(document.getElementById('generalIdMa').checked) 
{
			$("#GeneralPlaceDiv").show();
			$("#GeneralDiv3").show();
			$("#ProductDiv2").hide();
			$("#qualityDiv1").hide();
			$("#MahindraNoLastQ").hide();
}
else{
	Lobibox.notify('warning', {
		continueDelayOnInactiveTab: true,
		msg: 'Please Select option.'
	});
	return false;
}
});


$("input[name$='qM3_SubReasonOfAreaOfImprovement1']").click(function() {
        var vQualServ = $(this).val();
		if(vQualServ=="Minor Issues")
		{
			$("#minorSubDiv1").show();
			$("#OnlyAppoDateAndTm").show();
			$("#OnlyPicUpAddDiv").hide();
			
			$("#minorSubDiv2").hide();
			$("#minorSubDiv3").hide();
			$("#qualityDiv1").hide();
			
		
		}
			if(vQualServ=="Car is required at workshop")
		{
			$("#minorSubDiv2").show();
			$("#OnlyPicUpAddDiv").show();
			$("#OnlyAppoDateAndTm").hide();
			$("#minorSubDiv1").hide();
			$("#minorSubDiv3").hide();
			$("#qualityDiv1").hide();
			
			
		}
			if(vQualServ=="Quality of Serive")
		{
			$("#minorSubDiv3").show();
			$("#OnlyAppoDateAndTm").show();
			$("#OnlyPicUpAddDiv").hide();
			$("#minorSubDiv1").hide();
			$("#minorSubDiv2").hide();
			$("#qualityDiv1").hide();
			
			
		}
});
$("#nextTosubQSubMa").click(function(){
if(document.getElementById('MinorIssuesID').checked)
{
			$("#minorSubDiv1").show();
			$("#minorSubDiv2").hide();
			$("#minorSubDiv3").hide();
			$("#qualityDiv1").hide();
}
else if(document.getElementById('workhoIDMahi').checked) 
{
			$("#minorSubDiv2").show();
			$("#minorSubDiv1").hide();
			$("#minorSubDiv3").hide();
			$("#qualityDiv1").hide();
}
else if(document.getElementById('QualitySubMahi').checked) 
{
			$("#minorSubDiv3").show();
			$("#minorSubDiv1").hide();
			$("#minorSubDiv2").hide();
			$("#qualityDiv1").hide();
}
else{
	Lobibox.notify('warning', {
		continueDelayOnInactiveTab: true,
		msg: 'Please Select Option'
	});
	return false;
}
});


$("input[name$='qM3_SubReasonOfAreaOfImprovement2']").click(function() {
        var vProductNm = $(this).val();
		if(vProductNm=="Minor Issues")
		{
			$("#productSubDiv1").show();
			$("#OnlyAppoDateAndTm").show();
			$("#OnlyPicUpAddDiv").hide();
			$("#productSubDiv2").hide();
			$("#ProductDiv2").hide();
			
		
		}
			if(vProductNm=="Car is required at workshop")
		{
			$("#productSubDiv2").show();
			$("#OnlyPicUpAddDiv").show();
			$("#OnlyAppoDateAndTm").hide();
			$("#productSubDiv1").hide();
			$("#ProductDiv2").hide();
			
			
			
		}
});
$("#nextToSubMahiQ").click(function(){
if(document.getElementById('ProduMinorIDMa').checked)
{
			$("#productSubDiv1").show();
			$("#productSubDiv2").hide();
			$("#ProductDiv2").hide();
}
else if(document.getElementById('ProduworkSpIDMa').checked) 
{
		$("#productSubDiv2").show();
		$("#productSubDiv1").hide();
		$("#ProductDiv2").hide();
}

else{
	Lobibox.notify('warning', {
		continueDelayOnInactiveTab: true,
		msg: 'Please Select option.'
	});
	return false;
}
});

$("#BackToMaruti1stQ").click(function(){
	$("#MaruthiFirstQ").show();
	$("#MaruthiNotConnect").hide();
	$('input[name=isContacted]').prop('checked',false);
});

$("#BackTo1stMQ").click(function(){
	$("#MaruthiFirstQ").show();
	$("#MaruthiGoodM").hide();
	$('input[name=isContacted]').prop('checked',false);
});
$("#BackTo2ndMahiQ").click(function(){
	$("#MaruthiGoodM").show();
	$("#ManhindarYesC").hide();
	
});

$("#BackTo2ndQClLat").click(function(){
	$("#MaruthiGoodM").show();
	$("#Ques2FollowUp").hide();
	$("#onlyFollowUpDiv").hide();
	
});

$("#BackTo3rdQuest").click(function(){
	$("#MahindraYesLastQ").show();
	$("#SatisfiedYesMa").hide();
	
});
$("#BackTo3rdQuesMa").click(function(){
	$("#ManhindarYesC").show();
	$("#MahindraYesLastQ").hide();
	

});
$("#BackTo3rdQMAhis").click(function(){
	$("#ManhindarYesC").show();
	$("#Ques3FollowUp").hide();
	$("#onlyFollowUpDiv").hide();

	

});

$("#BackTo4QMahindra").click(function(){
	$("#MahindraYesLastQ").show();
	$("#MahindraNoLastQ").hide();
	

});

$("#BackTo2workShop").click(function(){
	$("#MahindraNoLastQ").show();
	$("#GeneralDiv3").hide();
	

});
$("#BackTo2minor").click(function(){
	$("#MahindraNoLastQ").show();
	$("#ProductDiv2").hide();

});
$("#BackTo1stAppolagy").click(function(){
	$("#MahindraNoLastQ").show();
	$("#qualityDiv1").hide();

});
$("#BackToMinorIss1").click(function(){
	$("#qualityDiv1").show();
	$("#minorSubDiv1").hide();
	$("#OnlyAppoDateAndTm").hide();

});

$("#BackToMinorIss2").click(function(){
	$("#qualityDiv1").show();
	$("#minorSubDiv2").hide();
	$("#OnlyPicUpAddDiv").hide();

});
$("#BackToMinorIss3").click(function(){
	$("#qualityDiv1").show();
	$("#minorSubDiv3").hide();
	$("#OnlyAppoDateAndTm").hide();

});
$("#BackToMhindraQSub").click(function(){

	$("#ProductDiv2").show();
	$("#productSubDiv1").hide();
	$("#OnlyAppoDateAndTm").hide();

});

$("#BackToMhinQsu2").click(function(){
	$("#ProductDiv2").show();
	$("#productSubDiv2").hide();
	$("#OnlyPicUpAddDiv").hide();

});

$("#BackToQuestPlaceBk").click(function(){
	$("#MahindraNoLastQ").show();
	$("#GeneralPlaceDiv").hide();
	$("#OnlyAppoDateAndTm").hide();

});

$("#subNextDay1PSF").click(function(){
	var v4thDayDate=$("#followupDTNextDAYID").val();	
	var v4thDayTime=$("#followupTMNextDAYID").val();	
	//alert(v4thDayTime);
	
	
	if(v4thDayDate==""){
		Lobibox.notify('warning', {
						continueDelayOnInactiveTab: true,
						msg: 'Please Enter FollowUp Date.'
					});
					return false;
		
	}
	else if(v4thDayTime==""){
		Lobibox.notify('warning', {
						continueDelayOnInactiveTab: true,
						msg: 'Please Enter FollowUp Time.'
					});
					return false;
		
	}
	else{
		
		return true;
	}
	
});

$("#submitNextDayFalloup2").click(function(){
	var vNextDayDate=$("#followupDTNextDAYID").val();	
	var vNextDayTime=$("#followupTMNextDAYID").val();	
	
	
	if(vNextDayDate==""){
		Lobibox.notify('warning', {
						continueDelayOnInactiveTab: true,
						msg: 'Please Enter FollowUp Date.'
					});
					return false;
		
	}
	else if(vNextDayTime==""){
		Lobibox.notify('warning', {
						continueDelayOnInactiveTab: true,
						msg: 'Please Enter FollowUp Time.'
					});
					return false;
		
	}
	else{
		
		return true;
	}
	
});
$('.single-input').timepicker({
					showPeriodLabels: false,
				});
	$( ".datepicker" ).datepicker({
       dateFormat: 'yy-mm-dd',
       });

});