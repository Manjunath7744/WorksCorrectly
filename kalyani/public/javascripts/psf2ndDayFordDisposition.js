
$(document).ready(function(){

$('#BackTospeak').on('click',function(){
$("#PSFYesTalk").show();
$("#PSFconnectCall1").hide();
$("#PsfSelfDriveINYes").hide();
$("#PSFYesNamaskarYesDiv").hide();
 $( "#psfYesNamaskarbtn" ).prop( "checked", false );
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



 $("input[name$='isContacted']").click(function() {
         PSFtest = $(this).val();
		if(PSFtest=="Yes")
		{
			$("#PSFYesTalk").show();
			$("#PSFNotSpeachDiv").hide();
			
		}
			if(PSFtest=="No")
		{
			$("#PSFNotSpeachDiv").show();
			$("#PSFYesTalk").hide();
			
		}
		
});

	$("input[name$='PSFYesNamaskar']").click(function(){
		varPSFYes = $(this).val();
		
		if(varPSFYes == "Yes" || varPSFYes == "No" || varPSFYes == "NotInterested" )
		{
			psfNamaskar();
		}
});

function psfNamaskar(){
	if(varPSFYes!="")
		{
			if(varPSFYes == "Yes")
			{
				$("#PSFYesNamaskarYesDiv").show();
				$("#PSFYesNamaskarNoDiv").hide();
				$("#PSFconnectCall1").hide();
				$("#PSFYesTalk").hide();
				$("#NotInterestedBtns").hide();
						
			}
			if(varPSFYes == "No")
			{
				
				$("#fordPSFYesNamaskarYesDiv").show();
				//$("#fordPSFYesNamaskarNoDiv").show();
				
				//$("#PSFconnectCall1").hide();
				//$("#PSFYesTalk").hide();
				//$("#NotInterestedBtns").hide();
							
			}
			if(varPSFYes == "NotInterested")
			{
				$("#PSFYesNamaskarNoDiv").hide();
				$("#PSFYesNamaskarYesDiv").hide();
				$("#PSFconnectCall1").hide();
				$("#PSFYesTalk").hide();
				$("#NotInterestedBtns").show();
			
				
			}
		}

}
 $("input[name$='disposition']").click(function() {
        var varPSF1Yes = $(this).val();
		if(varPSF1Yes=="PSF_Yes")
		{
			$("#PsfSelfDriveINYes").show();
			//$("#PsfSelfDriveInNo1").hide();
			$("#foardCallMelater").hide();
			//$("#PSFYesNamaskarYesDiv").hide();
			//$("#PsfSelfDriveInNo1").hide();	
			
		}
			if(varPSF1Yes=="Call Me Later")
		{
			$("#foardCallMelater").show();
			//$("#PsfSelfDriveInNo1").show();
			$("#PsfSelfDriveINYes").hide();
			//$("#PSFYesNamaskarYesDiv").hide();
			//$("#PsfSelfDriveInNo1").hide();
			
			
		}
		
});

 $("input[name$='LeadYesH']").click(function() {
        var varUpselLead = $(this).val();
		if(varUpselLead=="Yes")
		{
			$("#PsfLeadFoard").show();
		
		}
			if(varUpselLead=="No")
		{
			$("#PsfLeadFoard").hide();
			
			
			
		}
		
});


$("#BackToDidUtlakPSF").click(function(){
$("#PSFYesTalk").show();
$("#PSFconnectCall1").show();
$("#PSFYesNamaskarNoDiv").hide();
$("#PSFNoNamaskarbtn" ).prop( "checked", false );
});



		
$("#BackToDidUtlakPSFH").click(function(){
	$("#PSFYesTalk").show();
	$("#PSFconnectCall1").hide();
	$("#foardCallMelater").hide();
	$("#PSFYesNamaskarYesDiv").hide();
});
$("#nextToPASageAbove2").click(function(){
	
	var rating1 =$('#1stQSelectAbove2').val();
	
	

var incompleteData=$('#incompleteDate').val();
	
	if(incompleteData ==null | incompleteData == ""){

if(rating1=="0"){
	Lobibox.notify('warning', {
			continueDelayOnInactiveTab: true,
			msg: 'Please Select Overall Service Experience'
			});
			return false;;
}else{
	$("#upsellFoard").show();
	$("#PSFYesNamaskarYesDiv").hide();
	$("#PsfSelfDriveINYes").hide();
}}else{
	$("#upsellFoard").show();
	$("#PSFYesNamaskarYesDiv").hide();
	$("#PsfSelfDriveINYes").hide();
	
}

});

$("#BackToUpselFoard").click(function(){

$("#PSFYesNamaskarYesDiv").show();
$("#PsfSelfDriveINYes").show();
$("#upsellFoard").hide();
});

$("#NextToUpselFoard").click(function(){

$("#psffeedBackFoard").show();
$("#upsellFoard").hide();
});

$("#BackToSirMamDiv").click(function(){

$("#upsellFoard").show();
$("#psffeedBackFoard").hide();
});



$("#BackTo1stMQAge").click(function(){
	$("#PSFconnectCall1").show();
	$("#PSFYesTalk").hide();
	$('input[name=isContacted]').prop('checked',false);
});



<!-----Upsell Validation------->
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