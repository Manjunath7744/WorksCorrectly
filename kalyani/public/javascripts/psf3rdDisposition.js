$(document).ready(function(){
var varYesPSF="";
var varYesPSF1='';
var testPSF='';
var varSatisfiedPSF='';
var varPSFfeedback='';
var varPSFleadS='';
    $("input[name='psfinteraction.isContacted']").click(function() {
         testPSF = $(this).val();
		if(testPSF=="Yes")
		{
			$("#PSFYesTalkH").show();
			$("#PSFNotSpeachDiv").hide();
			$("#PSFconnectCall1H").hide();
			
		}
			if(testPSF=="No")
		{
			$("#PSFNotSpeachDiv").show();
			$("#PSFYesTalkH").hide();
			$("#PSFconnectCall1H").hide();
			
		}
		
});

    $("input[name='listingForm.psfDisposition']").click(function(){
		varYesPSF = $(this).val();
		if(varYesPSF == "PSF_Yes" || varYesPSF == "Call Me Later" )
		{
			if(varYesPSF == "PSF_Yes")
			{
				$("#PSFYesNamaskarYesDivH").show();
				$("#IamBusyDiv").hide();
				$("#PSFconnectCall1H").hide();
				$("#PSFYesTalkH").hide();
						
			}
			if(varYesPSF == "Call Me Later")
			{
                $("#IamBusyDiv").show();
				$("#PSFYesTalkH").hide();
				$("#PSFconnectCall1H").hide();
			}
		}
});


    $("input[name='psfinteraction.q1_CompleteSatisfication']").click(function() {
        var varYesPSF1 = $(this).val();
		if(varYesPSF1=="PSFSelf Yes")
		{
			$("#PsfSelfDriveINYesH").show();
			$("#PsfSelfDriveInNo1H").hide();
			
			
			
		}
			if(varYesPSF1=="PSFSelf No")
		{
			$("#PsfSelfDriveInNo1H").show();
			$("#PsfSelfDriveINYesH").hide();
			
						
		}
		
});
$("#BackToTaltkDivH").click(function(){
	$("#PSFYesTalkH").show();
	$("#PSFconnectCall1H").show();
	$("#PSFYesNamaskarYesDivH").hide();
});

$("#BackToDidUtlakPSFH").click(function(){
	$("#PSFYesTalkH").show();
	//$("#PSFconnectCall1H").show();
	$("#IamBusyDiv").hide();
    $("input[name='psfinteraction.PSFDispositon']").prop('checked',false);
});

$("#BackToSirMam").click(function(){
	$("#PSFYesTalkH").show();
	//$("#PSFconnectCall1H").show();
	$("#PSFYesNamaskarYesDivH").hide();
    $("input[name='psfinteraction.PSFDispositon']").prop('checked',false);
});


$("#NextToHowRate").click(function(){
    if (document.getElementById('PSF2YesId').checked) {
        var selectedCount = 0;
        
        if ($('.rateUs').each(function () {
            if ($(this).val() !== "") {
                selectedCount = selectedCount + 1;
            }
        }));

        if (selectedCount !== $('.rateUs').length) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please select anyone rating from each.'
            });
        }
        else {
            $("#upsell3rdDayH").show();
            //$("#PsfSelfDriveINYesH").hide();
            $("#PSFYesNamaskarYesDivH").hide();
        }
}
else if(document.getElementById('PSF2NoId').checked){
	$("#upsell3rdDayH").show();
	//$("#PsfSelfDriveINYesH").hide();
	$("#PSFYesNamaskarYesDivH").hide();
}
else{
	alert('Please Select Any Option');
}
	
});


    $('.myOutCheckbox').click(function () {
        var checkeds = $(this).is(':checked');
        var divId = $(this).attr('data-childdiv');
        if (checkeds) {
            $('#' + divId).show();
        } else {

            //Lobibox.notify('warning', {
            //    continueDelayOnInactiveTab: true,
            //    msg: 'Please check one of these.'

            //});
            $('#' + divId).hide();
            //return false;
        }
    });
//var checkeds = $('.myOutCheckbox').is(':checked');

//    if (checkeds) {

//    } else {

//        Lobibox.notify('warning', {
//            continueDelayOnInactiveTab: true,
//            msg: 'Please check one of these.'

//        });
//        return false;
//    }


$("#NextToUpsellInsu").click(function(){
	
	
if(document.getElementById('LeadYesID3Hyndai').checked){
	
	 //var checkeds = $('.myOutCheckbox').is(':checked');
		
		//if(checkeds){
			
		//}else{
			
		//	Lobibox.notify('warning', {
		//		continueDelayOnInactiveTab: true,
		//		msg: 'Please check one of these.'
				
		//	});
		//	return false;
		//}
		
    $("#upsell3rdDayH").hide();
	$("#PSFFeedbackQ").show();
	
	
}
else if(document.getElementById('LeadNoID3Hyndai').checked){
	$("#upsell3rdDayH").hide();
	$("#PSFFeedbackQ").show();
	
}
else{
	alert('Please Select Any Option');
}
	
});



$("#BackTo3rdDayRate").click(function(){
	$("#PSFYesNamaskarYesDivH").show();
	$("#upsell3rdDayH").hide();
});

$("#BackToSirMamDiv").click(function(){
	//$("#PSFYesNamaskarYesDivH").show();
	$("#upsell3rdDayH").show();
	$("#PSFFeedbackQ").hide();
});

$("#NextToPSFLastDiv").click(function(){
	$("#PsfSelfDriveINYesH").show();
	//$("#PSFYesNamaskarYesDivH").hide();
	
});
$("#BackTo1stQ").click(function(){

	$("#PSFconnectCall1H").show();
	$("#PSFYesTalkH").hide();
	$('input[name=isContacted]').prop('checked',false);
	
});
$("#BackTolatkNoPsf").click(function(){
	$("#PSFconnectCall1H").show();
	$("#PSFNotSpeachDiv").hide();
	$('input[name=isContacted]').prop('checked',false);
	
});


$("#NextTO2ndQ").click(function(){
if(document.getElementById('GoodMorningYes').checked)
{
	$("#PSFYesNamaskarYesDivH").show();
	$("#PSFYesTalkH").hide();
}
else if(document.getElementById('GoodMorningNo').checked) 
{
	$("#PSFYesNamaskarYesDivH").show();
	$("#PSFYesTalkH").hide();
}

else{
alert('Please Select Option');
}

	
});



    $("input[name='psfinteraction.q12_FeedbackTaken']").click(function() {
         varPSFfeedback = $(this).val();
		if(varPSFfeedback=="Yes")
		{
			$("#feedbackPSFYes").show();
			$("#feedbackPSFNo").hide();
			
		}
			if(varPSFfeedback=="No")
		{
			$("#feedbackPSFNo").show();
			$("#feedbackPSFYes").hide();
			
		}
		
});

    $("input[name$='listingForm.LeadYesH']").click(function() {
         varPSFleadS = $(this).val();
		if(varPSFleadS=="Yes")
		{
			$("#LeadHyndai3rdDay").show();
			
			
		}
			if(varPSFleadS=="No")
		{
			
			$("#LeadHyndai3rdDay").hide();
			
		}
		
});

//---Upsell Validation------->
//OutBound Upsell Opportunity--------->
	$('#InsuranceIDCheck').click(function(){
		if($(this).prop('checked')){
			$('#InsuranceSelect').show();
		}else{
			$('#InsuranceSelect').hide();
		}
	});

	$('#MaxicareIDCheck').click(function(){
		if($(this).prop('checked')){
		$('#MaxicareSelect').show();
		}else{
		$('#MaxicareSelect').hide();
		}
	});

	$('#ShieldID').click(function(){
		if($(this).prop('checked')){
			$('#ShieldSelect').show();
		}else{
			$('#ShieldSelect').hide();
		}
	});

	$('#VASID').click(function(){
		if($(this).prop('checked')){
				$('#VASTagToSelect').show();
		}else{
				$('#VASTagToSelect').hide();
		}
	});

	$('#RoadSideAsstID').click(function(){
		if($(this).prop('checked')){
				$('#RoadSideAssiSelect').show();
		}else{
				$('#RoadSideAssiSelect').hide();
		}
	});

	$('#EXCHANGEID').click(function(){
	if($(this).prop('checked')){
			$('#EXCHANGEIDSelect').show();
	}else{
			$('#EXCHANGEIDSelect').hide();
	}
	});

	$('#UsedCarID').click(function(){
		if($(this).prop('checked')){
				$('#UsedCarSelect').show();
		}else{
				$('#UsedCarSelect').hide();
		}
	});


	$('.single-input').timepicker({
		showPeriodLabels: false,
		});

});


//validation//

$('#followUpValidation').click(function(){
		var dateVal=$('#psfFollowUpDate').val();
		var timeVal=$('#psfFollowUpTime').val();
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



$('#SubmitDivMamDivsubmit').click(function(){
	 var chkincSubmit = 0;
    $('[name="psfinteraction.q12_FeedbackTaken"]').each(function (){
				if ($(this).is(':checked')) chkincSubmit++;
			});
			if (chkincSubmit == 0) {
			Lobibox.notify('warning', {
						continueDelayOnInactiveTab: true,
						msg: 'Please check one.'
					});
					return false;
				
			} else{
                       $.blockUI(); 
                               
                           }
});

$('#noncontactsValid').click(function(){
	 var chkincSubmit = 0;
    $('[name="psfinteraction.PSFDispositon"]').each(function (){
				if ($(this).is(':checked')) chkincSubmit++;
			});
			if (chkincSubmit == 0) {
			Lobibox.notify('warning', {
						continueDelayOnInactiveTab: true,
						msg: 'Please check one.'
					});
					return false;
				
			} else{
                      $.blockUI(); 
                              
                          }
});

