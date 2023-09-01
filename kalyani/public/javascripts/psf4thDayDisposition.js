$(document).ready(function(){
	$( ".showOnlyFutureDate" ).datepicker({
    changeMonth: true,
    changeYear: true,
    maxDate: "+30d",
minDate: '0',
showAnim: 'slideDown',
dateFormat: 'yy-mm-dd'


  });





	$("#nextTo4thDayRating").click(function(){
	$("#fedBack4thDayDispo").show();
	$("#upsellPsf4thDayageing").hide();
	
});
$("#BackToFeedBack4thDay").click(function(){
	$("#MahiYesLastQAging").show();
	$("#ageingAbove2Year").show();
	$("#commercialVehicle").show();
	$("#upsellPsf4thDayageing").hide();
	
	
});

$("#BackToupsell4thDayPsd").click(function(){
	$("#upsellPsf4thDayageing").show();
	$("#fedBack4thDayDispo").hide();
	
	
});

$("input[name$='LeadYesH']").click(function() {
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
 
    $("#nextToupsellAbove2").click(function(){
	
	  $("#upsellPsf4thDayageing").show();
	  $("#ageingAbove2Year").hide();
  });

 $("input[name$='isContacted']").click(function() {
        var vYesPSFM = $(this).val();
		if(vYesPSFM=="Yes")
		{
			$("#MahiiAng1GoodM").show();
			$("#mahinAngNotConnect").hide();
			$("#mahindraAgeing1").hide();
			
		}
			if(vYesPSFM=="No")
		{
			$("#mahinAngNotConnect").show();
			$("#MahiiAng1GoodM").hide();
			$("#mahindraAgeing1").hide();
			
		}
		
});
$("input[name$='qM4_confirmingCustomer']").click(function() {
        var vSmahiPsf = $(this).val();
		if(vSmahiPsf=="Yes")
		{
			$("#ManhinYesAgeing").show();
			$("#FollowUpDateDiv").hide();
			$("#MahiiAng1GoodM").hide();
			$("#ManhinNotAgeing").hide();
		}
			if(vSmahiPsf=="No")
		{
			/* $("#ManhinNotAgeing").show();
			$("#FollowUpDateDiv").show();
			$("#ManhinYesAgeing").hide();
			$("#MahiiAng1GoodM").hide(); */
		}
});
$("#NextToQuest3Age").click(function(){
if(document.getElementById('goodYesAgeing').checked)
{
	$("#ManhinYesAgeing").show();
	$("#ManhinNotAgeing").hide();
	$("#MahiiAng1GoodM").hide();
}
else if(document.getElementById('goodNoAgeing').checked) 
{
	$("#ManhinYesAgeing").show();
	$("#ManhinNotAgeing").hide();
	$("#MahiiAng1GoodM").hide();
}
else{
	Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Select Option.'
						});
						return false;
	

}
});
$("input[name$='qM4_RightTimeToEnquireOrderbillDate']").click(function() {
        var vrighttm = $(this).val();
		if(vrighttm=="Yes")
		{
			var ageis=document.getElementById('ageOfVehicleIS').value;
			var isCommecial	=document.getElementById('commercialVeh').value;
			console.log("isCommecial : "+isCommecial);
			if(isCommecial == "true"){
				$("#commercialVehicle").show(); 
			}else if(ageis > 730){
			
				$("#ageingAbove2Year").show(); 
				
			}else{
				
				$("#MahiYesLastQAging").show();
				
				
			}
			
			$("#FollowUpDateDiv").hide();
			$("#agingPSF3rdQ").hide();
			$("#ManhinYesAgeing").hide();
			
		}
			if(vrighttm=="No")
		{
			$("#agingPSF3rdQ").show();
			$("#FollowUpDateDiv").show();
			$("#MahiYesLastQAging").hide();
			$("#ManhinYesAgeing").hide();
			
		}
});

$("#NextToQuest4Age").click(function(){
if(document.getElementById('righttimeAgeingYes').checked)
{
			$("#MahiYesLastQAging").show();
			$("#ageingAbove2Year").show();
			$("#commercialVehicle").show();
			
			$("#agingPSF3rdQ").hide();
			$("#ManhinYesAgeing").hide();
}
else if(document.getElementById('righttimeAgeingNo').checked) 
{
			$("#agingPSF3rdQ").show();
			$("#FollowUpDateDiv").show();
			$("#ageingAbove2Year").hide();
			$("#commercialVehicle").hide();
			
			$("#MahiYesLastQAging").hide();
			$("#ManhinYesAgeing").hide();
}
else{
Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Select Option.'
						});
						return false;
}
});

$("#BackToAgeing1stQ").click(function(){
	$("#mahindraAgeing1").show();
	$("#mahinAngNotConnect").hide();
	$('input[name=isContacted]').prop('checked',false);
});

$("#BackTo1stMQAge").click(function(){
	$("#mahindraAgeing1").show();
	$("#MahiiAng1GoodM").hide();
	$('input[name=isContacted]').prop('checked',false);
});
$("#BackTo2ndMahiQAge").click(function(){
	$("#MahiiAng1GoodM").show();
	$("#ManhinYesAgeing").hide();
	
});

$("#BackTo2ndQClLatAgeing").click(function(){
	$("#MahiiAng1GoodM").show();
	$("#ManhinNotAgeing").hide();
	$("#FollowUpDateDiv").hide();
	
});

$("#BackTo3rdQuest").click(function(){
	$("#MahiYesLastQAging").show();
	$("#SatisfiedYesMa").hide();
	
});
$("#BackTo3rdQuesMaAgeing").click(function(){
	$("#ManhinYesAgeing").show();
	$("#MahiYesLastQAging").hide();
	$("#ageingAbove2Year").hide();
	$("#commercialVehicle").hide();

});

$("#BackToaboverdQuesMaAgeing").click(function(){
	$("#ManhinYesAgeing").show();
	$("#ageingAbove2Year").hide();

});


$("#BackTo3rdQMAhisAgeing").click(function(){
	$("#ManhinYesAgeing").show();
	$("#agingPSF3rdQ").hide();
	$("#FollowUpDateDiv").hide();

});

/* $("#ratingSubmit3rdDayM").click(function(){
var rating12 =$('.ratingValidate').val();
alert(rating12);
	if(rating12=="0"){
	alert('Please Select Rating');
	return false;
		
	}else{
	alert('Submited Successfully');
		return true;
	}
});*/

$('input[name="PSFDispositon"]').click(function(){
		var vpsf4tDay=$(this).val();
		if(vpsf4tDay=="NoOther"){
			$("#psf4DayOthers").show();
			
		}else{
			$("#psf4DayOthers").hide();
			
		}
	
});

	$('#psf4dayNoContact').click(function(){
		 var v4thdaySubmit = 0;
				$('[name="PSFDispositon"]').each(function (){
					if ($(this).is(':checked')) v4thdaySubmit++;
				});
				if (v4thdaySubmit == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please check one.'
						});
						return false;
						
					
				} else{
					if($("#MaruthiOthersCheck").prop('checked')){
						 
						var remarkNoOthers = $('.Psf4thdayRemarks').val();
						if(remarkNoOthers ==''){
							Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Remarks Should not Empty.'
						});
						return false;
							
						}
					}
                                    
                   }
	});


	$("#psf4thDay2ndsubmit").click(function(){
		var v4thDayDate=$("#followUpDT4thDay").val();	
		var v4thDayTime=$("#followTM4thDay").val();	
		
		
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
	
	
	
	$("#Samepsf4thDay2ndsubmit").click(function(){
		var v4thDayDate=$("#followUpDT4thDay").val();	
		var v4thDayTime=$("#followTM4thDay").val();	
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
	
	$('.single-input').timepicker({
						showPeriodLabels: false,
					});
	
	
	//Age Question validation
	
	/*$("#ratingSubmit3rdDayM").click(function(){
		
		});*/

	 $("#nextToupsellBelow2").click(function(){
		 var rating1 =$('#1stQSelect').val();
		 		var rating2 =$('#2ndQSelect').val();
		 		var rating3 =$('#3rdQSelect').val();
		 		var rating4 =$('#4thQSelect').val();
		 		var rating5 =$('#5thQSelect').val();
		 		var rating6 =$('#6thQSelect').val();
		 		var rating7 =$('#7thQSelect').val();
		 		var rating8 =$('#8thQSelect').val();
		 		var rating9 =$('#9thQSelect').val();
		 		var rating10 =$('#10thQSelect').val();
		 		var rating11 =$('#11thQSelect').val();
		 		var rating12 =$('#12thQSelect').val();
		 		var incompleteData=$('#incompleteDate').val();
		 		
		 		if(incompleteData ==null | incompleteData == ""){
		 			
		 			if(rating1=="0" | rating2=="0" | rating3=="0" | rating4=="0" | rating5=="0" | rating6=="0" | rating7=="0" | rating8=="0" | rating9=="0" |rating10=="0" |rating11=="0" |rating12=="0" )
		 			{
		 			Lobibox.notify('warning', {
		 			continueDelayOnInactiveTab: true,
		 			msg: 'Please Select All Rating2.'
		 			});
		 			return false;

		 			}else{
		 				$("#upsellPsf4thDayageing").show();
		 	 			 $("#MahiYesLastQAging").hide();
		 					}	
		 		}else{
		 		$("#upsellPsf4thDayageing").show();
		 	 			 $("#MahiYesLastQAging").hide();

		 		
		 		}
		   });

		  $("#nextToPASageAbove2").click(function(){
		 				var rating1Below2 =$('#1stQSelectAbove2').val();
		 		var rating2Below2 =$('#2ndQSelectAbove2').val();
		 		var rating3Below2 =$('#3rdQSelectAbove2').val();
		 		var rating4Below2 =$('#4thQSelectAbove2').val();
		 		var rating5Below2 =$('#5thQSelectAbove2').val();
		 		var rating6Below2 =$('#6thQSelectAbove2').val();
		 		var rating7Below2 =$('#7thQSelectAbove2').val();
		 		var rating8Below2 =$('#8thQSelectAbove2').val();
		 		var rating9Below2 =$('#9thQSelectAbove2').val();
		 		
		 		var incompleteData=$('#incompleteDate').val();
		 		
		 		if(incompleteData ==null | incompleteData == ""){
		 			
		 			if(rating1Below2=="0" | rating2Below2=="0" | rating3Below2=="0" | rating4Below2=="0" | rating5Below2=="0" | rating6Below2=="0" | rating7Below2=="0" | rating8Below2=="0" | rating9Below2=="0" )
		 			{
		 			Lobibox.notify('warning', {
		 			continueDelayOnInactiveTab: true,
		 			msg: 'Please Select All Rating1.'
		 			});
		 			return false;

		 			}else{
		 			$("#upsellPsf4thDayageing").show();
		 			$("#ageingAbove2Year").hide();

		 			
		 			}
		 		}else{
		 			$("#upsellPsf4thDayageing").show();
		 			$("#ageingAbove2Year").hide();

		 				
		 				}
		 	
		 		});
		  
		  $("#nextToCommercial").click(function(){
			  
			    var rating1Below2 =$('#qMC1_OverallServiceExp').val();
		 		var rating2Below2 =$('#qMC2_HappyWithRepairJob').val();
		 		var rating3Below2 =$('#qMC3_CoutnessHelpfulnessOfSA').val();
		 		var rating4Below2 =$('#qMC4_TimeTakenCompleteJob').val();
		 		var rating5Below2 =$('#qMC5_CostEstimateAdherance').val();
		 		var rating6Below2 =$('#qMC6_ServiceConvinenantTime').val();
		 		var rating7Below2 =$('#qMC7_LikeToRevistDealer').val();
		 				 		
		 		var incompleteData=$('#incompleteDate').val();
		 		
		 		if(incompleteData ==null | incompleteData == ""){
		 			
		 			if(rating1Below2=="0" | rating2Below2=="0" | rating3Below2=="0" | rating4Below2=="0" | rating5Below2=="0" | rating6Below2=="0" | rating7Below2=="0" )
		 			{
		 			Lobibox.notify('warning', {
		 			continueDelayOnInactiveTab: true,
		 			msg: 'Please Select All Ratings.'
		 			});
		 			return false;

		 			}else{
		 				$("#upsellPsf4thDayageing").show();
		 				$("#commercialVehicle").hide();	

		 			
		 			}
		 		}else{
		 			$("#upsellPsf4thDayageing").show();
					$("#commercialVehicle").hide();	

		 				
		 				}
				
				
	
		});
		$('.single-input').timepicker({
			showPeriodLabels: false,
			});

	
});