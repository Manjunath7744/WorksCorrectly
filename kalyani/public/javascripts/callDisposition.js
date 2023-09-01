
function callStatusTypeSelectionDownload(){
	
	var roleselect = document.getElementById('SelectContactAndNonContact');

	if (roleselect.options[roleselect.selectedIndex].value === "Contact") {
		document.getElementById("status").style.display = "block";
		document.getElementById("NonContactShow").style.display = "none";
		document.getElementById("serviceNotRequiredOptions").style.display = "none";
		document.getElementById("AlreadyserviceOthers").style.display = "none";
		document.getElementById("submitId").style.display = "block";
	} else if (roleselect.options[roleselect.selectedIndex].value === "NonContact") {
		document.getElementById('status').style.display = "none";
		document.getElementById('NonContactShow').style.display = "block";
		document.getElementById("serviceNotRequiredOptions").style.display = "none";
		document.getElementById("AlreadyserviceOthers").style.display = "none";
		document.getElementById("submitId").style.display = "block";
	}else{
		document.getElementById('status').style.display = "none";
		document.getElementById('NonContactShow').style.display = "none";
		document.getElementById("serviceNotRequiredOptions").style.display = "none";
		document.getElementById("AlreadyserviceOthers").style.display = "none";
		document.getElementById("submitId").style.display = "none";
	
	}
	
	
}

function dispositionTypeSelectionDownload(){
	
	var roleselect = document.getElementById('status');
	if (roleselect.options[roleselect.selectedIndex].value == "FollowUp Required") {
		document.getElementById("status").style.display = "block";
		document.getElementById("NonContactShow").style.display = "none";		
		document.getElementById("serviceNotRequiredOptions").style.display = "none";
		document.getElementById("AlreadyserviceOthers").style.display = "none";
		document.getElementById("submitId").style.display = "block";
		
		
	}else if(roleselect.options[roleselect.selectedIndex].value == "Service booked"){
		
		
		document.getElementById("status").style.display = "block";
		document.getElementById("NonContactShow").style.display = "none";		
		document.getElementById("serviceNotRequiredOptions").style.display = "none";
		document.getElementById("AlreadyserviceOthers").style.display = "none";
		document.getElementById("submitId").style.display = "block";
		
		} else if(roleselect.options[roleselect.selectedIndex].value == "Service Not Required"){
			
		document.getElementById("status").style.display = "block";
		document.getElementById("NonContactShow").style.display = "none";		
		document.getElementById("serviceNotRequiredOptions").style.display = "block";
		document.getElementById("AlreadyserviceOthers").style.display = "none";
		document.getElementById("submitId").style.display = "block";
		}
}
	
	
function dispositionTypeSelectionReasonDownload(){
	var roleselect = document.getElementById('serviceNotRequiredOptions');
	
	if (roleselect.options[roleselect.selectedIndex].value === "Already Serviced - Others") {
		
		document.getElementById("status").style.display = "block";
		document.getElementById("NonContactShow").style.display = "none";		
		document.getElementById("serviceNotRequiredOptions").style.display = "block";
		document.getElementById("AlreadyserviceOthers").style.display = "block";
		document.getElementById("submitId").style.display = "block";
		
	}else{
		
		document.getElementById("status").style.display = "block";
		document.getElementById("NonContactShow").style.display = "none";		
		document.getElementById("serviceNotRequiredOptions").style.display = "block";
		document.getElementById("AlreadyserviceOthers").style.display = "none";
		document.getElementById("submitId").style.display = "block";
	}
	
	
}
	



function callStatusTypeSelectionFilter(){
	
	var roleselect = document.getElementById('SelectType');
	
	if (roleselect.options[roleselect.selectedIndex].value === "Contact") {
		document.getElementById("contactShow").style.display = "block";
		document.getElementById("NonContactShow").style.display = "none";
		document.getElementById("serviceNotRequiredOptionsFilter").style.display = "none";		
		document.getElementById("AlreadyserviceOthersFilter").style.display = "none";
		
		
	}else{
		document.getElementById("contactShow").style.display = "none";
		document.getElementById("NonContactShow").style.display = "block";	
		document.getElementById("serviceNotRequiredOptionsFilter").style.display = "none";		
		document.getElementById("AlreadyserviceOthersFilter").style.display = "none";
	}
	
	
	
}


function dispositionTypeSelectionFilter(){
	
	
	var roleselect = document.getElementById('contactShow');
	
	if (roleselect.options[roleselect.selectedIndex].value == "Service Not Required") {
		
		document.getElementById("contactShow").style.display = "block";
		document.getElementById("serviceNotRequiredOptionsFilter").style.display = "block";	
		document.getElementById("NonContactShow").style.display = "none";	
		document.getElementById("AlreadyserviceOthersFilter").style.display = "none";
		
	}else{
		document.getElementById("contactShow").style.display = "block";		
		document.getElementById("serviceNotRequiredOptionsFilter").style.display = "none";	
		document.getElementById("NonContactShow").style.display = "none";	
		document.getElementById("AlreadyserviceOthersFilter").style.display = "none";
		
	}
	
	
	
}



function dispositionTypeSelectionReasonFilter(){
	var roleselect = document.getElementById('serviceNotRequiredOptionsFilter');
	
	if (roleselect.options[roleselect.selectedIndex].value == "Already Serviced - Others") {
		
		
		document.getElementById("contactShow").style.display = "block";
		document.getElementById("NonContactShow").style.display = "none";
		document.getElementById("AlreadyserviceOthersFilter").style.display = "block";
		document.getElementById("serviceNotRequiredOptionsFilter").style.display = "block";	
		
		
	}else{
		
	document.getElementById("contactShow").style.display = "block";	
	document.getElementById("serviceNotRequiredOptionsFilter").style.display = "none";	
	document.getElementById("NonContactShow").style.display = "none";	
	document.getElementById("AlreadyserviceOthersFilter").style.display = "none";
	}
}





function PickupOnclcik() {
            var pickUpAddressId = document.getElementById("pickUpAddressId");
            pickUpAddressId.style.display = pickUpCheckedId.checked ? "block" : "none";
        }

jQuery(function ($) {
    var $active = $('#accordion .panel-collapse.in').prev().addClass('active');
    $active.find('a').prepend('<i class="glyphicon glyphicon-minus"></i>');
    $('#accordion .panel-heading').not($active).find('a').prepend('<i class="glyphicon glyphicon-plus"></i>');
    $('#accordion').on('show.bs.collapse', function (e) {
        $('#accordion .panel-heading.active').removeClass('active').find('.glyphicon').toggleClass('glyphicon-plus glyphicon-minus');
        $(e.target).prev().addClass('active').find('.glyphicon').toggleClass('glyphicon-plus glyphicon-minus');
    })
});



function callStatusTypeSelection() {
	var roleselect = document.getElementById('SelectContactAndNonContact');

	if (roleselect.options[roleselect.selectedIndex].value === "Contact") {
		document.getElementById("status").style.display = "block";
		document.getElementById("NonContactShow").style.display = "none";
		document.getElementById("FollowUpDateAndTimeSelect").style.display = "none";
		document.getElementById("ServiceDateAndTimeSelect").style.display = "none";
		document.getElementById("serviceNotRequiredOptions").style.display = "none";
		document.getElementById("carDifferentCityId").style.display = "none";
		document.getElementById("alreadyServedMyDEaler").style.display = "none";
		document.getElementById("AlreadyserviceOthers").style.display = "none";	
		document.getElementById("htmlAuthorized").style.display = "none";
		document.getElementById("htmlNotAuthorized").style.display = "none";
		document.getElementById("comments").style.display = "block";
		document.getElementById("lastServiceDateDisplay").style.display = "none";
		document.getElementById("submitId").style.display = "block";
		document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
		document.getElementById('NonContactErrorMessReason').innerHTML = "";
		document.getElementById('NonContactErrorMess').innerHTML = "";
		document.getElementById('followUpdateErrorMess').innerHTML = "";
		document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
		document.getElementById('serviceDateErrorMess').innerHTML = "";
		document.getElementById('serviceTimeErrorMess').innerHTML = "";
	} else{
		document.getElementById('status').style.display = "none";
		document.getElementById('NonContactShow').style.display = "block";
		document.getElementById("FollowUpDateAndTimeSelect").style.display = "none";
		document.getElementById("ServiceDateAndTimeSelect").style.display = "none";
		document.getElementById("serviceNotRequiredOptions").style.display = "none";
		document.getElementById("carDifferentCityId").style.display = "none";
		document.getElementById("alreadyServedMyDEaler").style.display = "none";
		document.getElementById("AlreadyserviceOthers").style.display = "none";	
		document.getElementById("htmlAuthorized").style.display = "none";
		document.getElementById("htmlNotAuthorized").style.display = "none";
		document.getElementById("comments").style.display = "none";
		document.getElementById("lastServiceDateDisplay").style.display = "none";
		document.getElementById("submitId").style.display = "block";
		document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
		document.getElementById('NonContactErrorMessReason').innerHTML = "";
		document.getElementById('NonContactErrorMess').innerHTML = "";
		document.getElementById('followUpdateErrorMess').innerHTML = "";
		document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
		document.getElementById('serviceDateErrorMess').innerHTML = "";
		document.getElementById('serviceTimeErrorMess').innerHTML = "";
	} 	

}


function dispositionTypeSelection(){
	var roleselect = document.getElementById('status');
	if (roleselect.options[roleselect.selectedIndex].value == "FollowUp Required") {
		document.getElementById("status").style.display = "block";
		document.getElementById("NonContactShow").style.display = "none";
		document.getElementById("FollowUpDateAndTimeSelect").style.display = "block";
		document.getElementById("ServiceDateAndTimeSelect").style.display = "none";
		document.getElementById("serviceNotRequiredOptions").style.display = "none";
		document.getElementById("carDifferentCityId").style.display = "none";
		document.getElementById("alreadyServedMyDEaler").style.display = "none";
		document.getElementById("AlreadyserviceOthers").style.display = "none";	
		document.getElementById("htmlAuthorized").style.display = "none";
		document.getElementById("htmlNotAuthorized").style.display = "none";
		document.getElementById("comments").style.display = "block";
		document.getElementById("lastServiceDateDisplay").style.display = "none";
		document.getElementById("submitId").style.display = "block";
		document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
		document.getElementById('NonContactErrorMess').innerHTML = "";
		document.getElementById('NonContactErrorMessReason').innerHTML = "";
		document.getElementById('followUpdateErrorMess').innerHTML = "";
		document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
		document.getElementById('serviceDateErrorMess').innerHTML = "";
		document.getElementById('serviceTimeErrorMess').innerHTML = "";
		
		
	}else if(roleselect.options[roleselect.selectedIndex].value == "Service booked"){
		
		
		document.getElementById("status").style.display = "block";
		document.getElementById("NonContactShow").style.display = "none";
		document.getElementById("FollowUpDateAndTimeSelect").style.display = "none";
		document.getElementById("ServiceDateAndTimeSelect").style.display = "block";
		document.getElementById("serviceNotRequiredOptions").style.display = "none";
		document.getElementById("carDifferentCityId").style.display = "none";
		document.getElementById("alreadyServedMyDEaler").style.display = "none";
		document.getElementById("AlreadyserviceOthers").style.display = "none";	
		document.getElementById("htmlAuthorized").style.display = "none";
		document.getElementById("htmlNotAuthorized").style.display = "none";
		document.getElementById("comments").style.display = "block";
		document.getElementById("lastServiceDateDisplay").style.display = "none";
		document.getElementById("submitId").style.display = "block";
		document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
		document.getElementById('NonContactErrorMess').innerHTML = "";
		document.getElementById('NonContactErrorMessReason').innerHTML = "";
		document.getElementById('followUpdateErrorMess').innerHTML = "";
		document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
		document.getElementById('serviceDateErrorMess').innerHTML = "";
		document.getElementById('serviceTimeErrorMess').innerHTML = "";
		
		} else if(roleselect.options[roleselect.selectedIndex].value == "Service Not Required"){
			
			document.getElementById("status").style.display = "block";
		document.getElementById("NonContactShow").style.display = "none";
		document.getElementById("FollowUpDateAndTimeSelect").style.display = "none";
		document.getElementById("ServiceDateAndTimeSelect").style.display = "none";
		document.getElementById("serviceNotRequiredOptions").style.display = "block";
		document.getElementById("carDifferentCityId").style.display = "none";
		document.getElementById("alreadyServedMyDEaler").style.display = "none";
		document.getElementById("AlreadyserviceOthers").style.display = "none";	
		document.getElementById("htmlAuthorized").style.display = "none";
		document.getElementById("htmlNotAuthorized").style.display = "none";
		document.getElementById("comments").style.display = "block";
		document.getElementById("lastServiceDateDisplay").style.display = "none";
		document.getElementById("submitId").style.display = "block";
		document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
		document.getElementById('NonContactErrorMess').innerHTML = "";
		document.getElementById('NonContactErrorMessReason').innerHTML = "";
		document.getElementById('followUpdateErrorMess').innerHTML = "";
		document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
		document.getElementById('serviceDateErrorMess').innerHTML = "";
		document.getElementById('serviceTimeErrorMess').innerHTML = "";
			}
	
	
	
	}
	
	function dispositionTypeSelectionReason(){
		var roleselect = document.getElementById('serviceNotRequiredOptions');
		if (roleselect.options[roleselect.selectedIndex].value == "Car Runs In Different City") {
			
		document.getElementById("status").style.display = "block";
		document.getElementById("NonContactShow").style.display = "none";
		document.getElementById("FollowUpDateAndTimeSelect").style.display = "none";
		document.getElementById("ServiceDateAndTimeSelect").style.display = "none";
		document.getElementById("serviceNotRequiredOptions").style.display = "block";
		document.getElementById("carDifferentCityId").style.display = "block";
		document.getElementById("alreadyServedMyDEaler").style.display = "none";
		document.getElementById("AlreadyserviceOthers").style.display = "none";	
		document.getElementById("htmlAuthorized").style.display = "none";
		document.getElementById("htmlNotAuthorized").style.display = "none";
		document.getElementById("comments").style.display = "block";
		document.getElementById("lastServiceDateDisplay").style.display = "none";
		document.getElementById("submitId").style.display = "block";
		document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
		document.getElementById('NonContactErrorMess').innerHTML = "";
		document.getElementById('NonContactErrorMessReason').innerHTML = "";
		document.getElementById('followUpdateErrorMess').innerHTML = "";
		document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
		document.getElementById('serviceDateErrorMess').innerHTML = "";
		document.getElementById('serviceTimeErrorMess').innerHTML = "";
		}else if(roleselect.options[roleselect.selectedIndex].value == "Already Serviced - My Dealer"){
			
			document.getElementById("status").style.display = "block";
		document.getElementById("NonContactShow").style.display = "none";
		document.getElementById("FollowUpDateAndTimeSelect").style.display = "none";
		document.getElementById("ServiceDateAndTimeSelect").style.display = "none";
		document.getElementById("serviceNotRequiredOptions").style.display = "block";
		document.getElementById("carDifferentCityId").style.display = "none";
		document.getElementById("alreadyServedMyDEaler").style.display = "block";
		document.getElementById("AlreadyserviceOthers").style.display = "none";	
		document.getElementById("htmlAuthorized").style.display = "none";
		document.getElementById("htmlNotAuthorized").style.display = "none";
		document.getElementById("comments").style.display = "block";
		document.getElementById("lastServiceDateDisplay").style.display = "block";
		document.getElementById("submitId").style.display = "block";
		document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
		document.getElementById('NonContactErrorMess').innerHTML = "";
		document.getElementById('NonContactErrorMessReason').innerHTML = "";
		document.getElementById('followUpdateErrorMess').innerHTML = "";
		document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
		document.getElementById('serviceDateErrorMess').innerHTML = "";
		document.getElementById('serviceTimeErrorMess').innerHTML = "";
			}else if(roleselect.options[roleselect.selectedIndex].value == "Already Serviced - Others"){
				
				document.getElementById("status").style.display = "block";
		document.getElementById("NonContactShow").style.display = "none";
		document.getElementById("FollowUpDateAndTimeSelect").style.display = "none";
		document.getElementById("ServiceDateAndTimeSelect").style.display = "none";
		document.getElementById("serviceNotRequiredOptions").style.display = "block";
		document.getElementById("carDifferentCityId").style.display = "none";
		document.getElementById("alreadyServedMyDEaler").style.display = "none";
		document.getElementById("AlreadyserviceOthers").style.display = "block";	
		document.getElementById("htmlAuthorized").style.display = "none";
		document.getElementById("htmlNotAuthorized").style.display = "none";	
		document.getElementById("comments").style.display = "block";
		document.getElementById("lastServiceDateDisplay").style.display = "block";
		document.getElementById("submitId").style.display = "block";
		document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
		document.getElementById('NonContactErrorMess').innerHTML = "";	
		document.getElementById('NonContactErrorMessReason').innerHTML = "";
		document.getElementById('followUpdateErrorMess').innerHTML = "";
		document.getElementById('followUpdateTimeErrorMess').innerHTML = "";	
		document.getElementById('serviceDateErrorMess').innerHTML = "";
		document.getElementById('serviceTimeErrorMess').innerHTML = "";
		}else if(roleselect.options[roleselect.selectedIndex].value == "Dissatisfied with last service"){
			
			document.getElementById("status").style.display = "block";
			document.getElementById("NonContactShow").style.display = "none";
			document.getElementById("FollowUpDateAndTimeSelect").style.display = "none";
			document.getElementById("ServiceDateAndTimeSelect").style.display = "none";
			document.getElementById("serviceNotRequiredOptions").style.display = "block";
			document.getElementById("carDifferentCityId").style.display = "none";
			document.getElementById("alreadyServedMyDEaler").style.display = "none";
			document.getElementById("AlreadyserviceOthers").style.display = "none";	
			document.getElementById("htmlAuthorized").style.display = "none";
			document.getElementById("htmlNotAuthorized").style.display = "none";
			document.getElementById("comments").style.display = "block";
			document.getElementById("lastServiceDateDisplay").style.display = "none";
			document.getElementById("submitId").style.display = "block";
			document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
			document.getElementById('NonContactErrorMess').innerHTML = "";
			document.getElementById('NonContactErrorMessReason').innerHTML = "";
			document.getElementById('followUpdateErrorMess').innerHTML = "";
			document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
			document.getElementById('serviceDateErrorMess').innerHTML = "";
			document.getElementById('serviceTimeErrorMess').innerHTML = "";
		}else if(roleselect.options[roleselect.selectedIndex].value == "Sold Car"){		
			
			document.getElementById("status").style.display = "block";
			document.getElementById("NonContactShow").style.display = "none";
			document.getElementById("FollowUpDateAndTimeSelect").style.display = "none";
			document.getElementById("ServiceDateAndTimeSelect").style.display = "none";
			document.getElementById("serviceNotRequiredOptions").style.display = "block";
			document.getElementById("carDifferentCityId").style.display = "none";
			document.getElementById("alreadyServedMyDEaler").style.display = "none";
			document.getElementById("AlreadyserviceOthers").style.display = "none";	
			document.getElementById("htmlAuthorized").style.display = "none";
			document.getElementById("htmlNotAuthorized").style.display = "none";
			document.getElementById("comments").style.display = "block";
			document.getElementById("lastServiceDateDisplay").style.display = "none";
			document.getElementById("submitId").style.display = "block";
			document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
			document.getElementById('NonContactErrorMess').innerHTML = "";
			document.getElementById('NonContactErrorMessReason').innerHTML = "";
			document.getElementById('followUpdateErrorMess').innerHTML = "";
			document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
			document.getElementById('serviceDateErrorMess').innerHTML = "";
			document.getElementById('serviceTimeErrorMess').innerHTML = "";
		}else if(roleselect.options[roleselect.selectedIndex].value == "Workshop Location is Far"){
			
			document.getElementById("status").style.display = "block";
			document.getElementById("NonContactShow").style.display = "none";
			document.getElementById("FollowUpDateAndTimeSelect").style.display = "none";
			document.getElementById("ServiceDateAndTimeSelect").style.display = "none";
			document.getElementById("serviceNotRequiredOptions").style.display = "block";
			document.getElementById("carDifferentCityId").style.display = "none";
			document.getElementById("alreadyServedMyDEaler").style.display = "none";
			document.getElementById("AlreadyserviceOthers").style.display = "none";	
			document.getElementById("htmlAuthorized").style.display = "none";
			document.getElementById("htmlNotAuthorized").style.display = "none";
			document.getElementById("comments").style.display = "block";
			document.getElementById("lastServiceDateDisplay").style.display = "none";
			document.getElementById("submitId").style.display = "block";
			document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
			document.getElementById('NonContactErrorMess').innerHTML = "";
			document.getElementById('NonContactErrorMessReason').innerHTML = "";
			document.getElementById('followUpdateErrorMess').innerHTML = "";
			document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
			document.getElementById('serviceDateErrorMess').innerHTML = "";
			document.getElementById('serviceTimeErrorMess').innerHTML = "";
			
		}
		
		
		}
		
		function dispositionTypeSelectionReasonHTML(){
			
			var roleselect = document.getElementById('AlreadyserviceOthers');
			
			if (roleselect.options[roleselect.selectedIndex].value == "MyDealer authorized") {
			
		document.getElementById("status").style.display = "block";
		document.getElementById("NonContactShow").style.display = "none";
		document.getElementById("FollowUpDateAndTimeSelect").style.display = "none";
		document.getElementById("ServiceDateAndTimeSelect").style.display = "none";
		document.getElementById("serviceNotRequiredOptions").style.display = "block";
		document.getElementById("carDifferentCityId").style.display = "none";
		document.getElementById("alreadyServedMyDEaler").style.display = "none";
		document.getElementById("AlreadyserviceOthers").style.display = "block";
		document.getElementById("htmlAuthorized").style.display = "block";
		document.getElementById("htmlNotAuthorized").style.display = "none";
		document.getElementById("comments").style.display = "block";
		document.getElementById("lastServiceDateDisplay").style.display = "block";
		document.getElementById("submitId").style.display = "block";
		
		document.getElementById('NonContactErrorMessReason').innerHTML = "";
		document.getElementById('NonContactErrorMess').innerHTML = "";
		document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
		document.getElementById('followUpdateErrorMess').innerHTML = "";
		document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
		document.getElementById('serviceDateErrorMess').innerHTML = "";
		document.getElementById('serviceTimeErrorMess').innerHTML = "";
		
		}else{
			
			document.getElementById("status").style.display = "block";
		document.getElementById("NonContactShow").style.display = "none";
		document.getElementById("FollowUpDateAndTimeSelect").style.display = "none";
		document.getElementById("ServiceDateAndTimeSelect").style.display = "none";
		document.getElementById("serviceNotRequiredOptions").style.display = "block";
		document.getElementById("carDifferentCityId").style.display = "none";
		document.getElementById("alreadyServedMyDEaler").style.display = "none";
		document.getElementById("AlreadyserviceOthers").style.display = "block";
		document.getElementById("htmlAuthorized").style.display = "none";
		document.getElementById("htmlNotAuthorized").style.display = "block";	
		document.getElementById("comments").style.display = "block";
		document.getElementById("lastServiceDateDisplay").style.display = "block";
		document.getElementById("submitId").style.display = "block";
		
		document.getElementById('NonContactErrorMessReason').innerHTML = "";
		document.getElementById('NonContactErrorMess').innerHTML = "";	
		document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
		document.getElementById('followUpdateErrorMess').innerHTML = "";
		document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
		document.getElementById('serviceDateErrorMess').innerHTML = "";
		document.getElementById('serviceTimeErrorMess').innerHTML = "";
			
			
			
			
			
			}
			
			}

		
function validatingDispositionForm(){
	
	var roleselect = document.getElementById('SelectContactAndNonContact');
	
	if(roleselect.options[roleselect.selectedIndex].value == "NonContact"){
		
	
	var selNon= document.getElementById('NonContactShow');
	
	if(selNon.options[selNon.selectedIndex].value == selNon.options[0].value)
	   {
		document.getElementById('followUpdateErrorMess').innerHTML = "";
		document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
		
			document.getElementById('NonContactErrorMessReason').innerHTML = "";
		   document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
		   document.getElementById('serviceDateErrorMess').innerHTML = "";
			document.getElementById('serviceTimeErrorMess').innerHTML = "";
		   
		  document.getElementById('NonContactErrorMess').innerHTML = "Select Disposition Type";
	      document.getElementById('NonContactShow').focus(); //set focus back to control
	      return false;
	   }else{
		   $.blockUI();	  
		   return true;
	   }
	
	}else if(roleselect.options[roleselect.selectedIndex].value == "Contact"){
		
		 var selCont = document.getElementById('status');
		 
		 if(selCont.options[selCont.selectedIndex].value == selCont.options[0].value){
			 
			 document.getElementById('followUpdateErrorMess').innerHTML = "";
				document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
			 
			 	document.getElementById('NonContactErrorMessReason').innerHTML = "";
			    document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
			    document.getElementById('serviceDateErrorMess').innerHTML = "";
				document.getElementById('serviceTimeErrorMess').innerHTML = "";
			   
			   document.getElementById('NonContactErrorMess').innerHTML = "Select Disposition Type";			   
			   document.getElementById('status').focus(); 
			   return false;
			   
		 }else if(selCont.options[selCont.selectedIndex].value == "Service Not Required"){
			 
			 
			var reasonSNR=document.getElementById('serviceNotRequiredOptions');
			if(reasonSNR.options[reasonSNR.selectedIndex].value == reasonSNR.options[0].value){
						
				
				document.getElementById('followUpdateErrorMess').innerHTML = "";
				document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
					document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
					document.getElementById('NonContactErrorMess').innerHTML = "";
					document.getElementById('serviceDateErrorMess').innerHTML = "";
					document.getElementById('serviceTimeErrorMess').innerHTML = "";
					
				   document.getElementById('NonContactErrorMessReason').innerHTML = "Select Disposition Reason Type";
				   document.getElementById('serviceNotRequiredOptions').focus(); 
				   return false;
				
			}else if(reasonSNR.options[reasonSNR.selectedIndex].value == reasonSNR.options[6].value){
				
				var reasonSNRAuth=document.getElementById('AlreadyserviceOthers');
				
				if(reasonSNRAuth.options[reasonSNRAuth.selectedIndex].value == reasonSNRAuth.options[0].value){
					document.getElementById('followUpdateErrorMess').innerHTML = "";
					document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
					document.getElementById('NonContactErrorMess').innerHTML = "";
					document.getElementById('NonContactErrorMessReason').innerHTML = "";	
					document.getElementById('serviceDateErrorMess').innerHTML = "";
					document.getElementById('serviceTimeErrorMess').innerHTML = "";
				   document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "Select Already Serviced - Others Reason";
				   document.getElementById('AlreadyserviceOthers').focus(); 
				   return false;
				   
				}else{
					
					 $.blockUI();	  
					   return true;	
					
				}   
				   
				   
				   
				
				
			}else if(reasonSNR.options[reasonSNR.selectedIndex].value == reasonSNR.options[1].value){
				document.getElementById('NonContactErrorMess').innerHTML = "";
				document.getElementById('NonContactErrorMessReason').innerHTML = "";
				document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
				document.getElementById('followUpdateErrorMess').innerHTML = "";
				document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
				document.getElementById('serviceDateErrorMess').innerHTML = "";
				document.getElementById('serviceTimeErrorMess').innerHTML = "";
				 return true;	
				
			}else if(reasonSNR.options[reasonSNR.selectedIndex].value == reasonSNR.options[2].value){
				document.getElementById('NonContactErrorMess').innerHTML = "";
				document.getElementById('NonContactErrorMessReason').innerHTML = "";
				document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
				document.getElementById('followUpdateErrorMess').innerHTML = "";
				document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
				document.getElementById('serviceDateErrorMess').innerHTML = "";
				document.getElementById('serviceTimeErrorMess').innerHTML = "";
				 return true;	
				
			}else if(reasonSNR.options[reasonSNR.selectedIndex].value == reasonSNR.options[3].value){
				document.getElementById('NonContactErrorMess').innerHTML = "";
				document.getElementById('NonContactErrorMessReason').innerHTML = "";
				document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
				document.getElementById('followUpdateErrorMess').innerHTML = "";
				document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
				document.getElementById('serviceDateErrorMess').innerHTML = "";
				document.getElementById('serviceTimeErrorMess').innerHTML = "";
				 return true;	
				
			}else if(reasonSNR.options[reasonSNR.selectedIndex].value == reasonSNR.options[4].value){
				document.getElementById('NonContactErrorMess').innerHTML = "";
				document.getElementById('NonContactErrorMessReason').innerHTML = "";
				document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
				document.getElementById('followUpdateErrorMess').innerHTML = "";
				document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
				document.getElementById('serviceDateErrorMess').innerHTML = "";
				document.getElementById('serviceTimeErrorMess').innerHTML = "";
				 return true;	
				
			}else if(reasonSNR.options[reasonSNR.selectedIndex].value == reasonSNR.options[5].value){
				document.getElementById('NonContactErrorMess').innerHTML = "";
				document.getElementById('NonContactErrorMessReason').innerHTML = "";
				document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
				document.getElementById('followUpdateErrorMess').innerHTML = "";
				document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
				document.getElementById('serviceDateErrorMess').innerHTML = "";
				document.getElementById('serviceTimeErrorMess').innerHTML = "";
				 return true;	
				
			}else{
				 $.blockUI();	  
				   return true;				
				
			}
			 
			 
		 }else if(selCont.options[selCont.selectedIndex].value == selCont.options[1].value){
			 
			 var followupD=document.getElementById('followUpDate').value;
			 var followupT=document.getElementById('followUpTime').value;
			 
			 if(followupD=="" && followupT==""){
				
				 	document.getElementById('NonContactErrorMess').innerHTML = "";
					document.getElementById('NonContactErrorMessReason').innerHTML = "";
					document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = ""; 
					document.getElementById('serviceDateErrorMess').innerHTML = "";
					document.getElementById('serviceTimeErrorMess').innerHTML = "";
					document.getElementById('followUpdateErrorMess').innerHTML = "Select Follow Up Date";
					document.getElementById('followUpdateTimeErrorMess').innerHTML = "Select Follow Up Time";
					document.getElementById('followUpDate').focus(); 
					return false;
			 }else if(followupD==""){
				 
				    document.getElementById('NonContactErrorMess').innerHTML = "";
					document.getElementById('NonContactErrorMessReason').innerHTML = "";
					document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = ""; 
					document.getElementById('serviceDateErrorMess').innerHTML = "";
					document.getElementById('serviceTimeErrorMess').innerHTML = "";
					document.getElementById('followUpdateErrorMess').innerHTML = "Select Follow Up Date";					
					document.getElementById('followUpDate').focus(); 
					return false;
			 }else if(followupT==""){
				 
				 	document.getElementById('NonContactErrorMess').innerHTML = "";
					document.getElementById('NonContactErrorMessReason').innerHTML = "";
					document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";	
					document.getElementById('serviceDateErrorMess').innerHTML = "";
					document.getElementById('serviceTimeErrorMess').innerHTML = "";
					document.getElementById('followUpdateTimeErrorMess').innerHTML = "Select Follow Up Date";
					document.getElementById('followUpTime').focus(); 
					return false;
				 
			 }else{
				 
				 $.blockUI();	  
				   return true;				 
			 }
			 
			 
			 
		 }else if(selCont.options[selCont.selectedIndex].value == selCont.options[2].value){
			 
			 var serviceupD=document.getElementById('serviceScheduledDate').value;
			 var serviceupT=document.getElementById('serviceScheduledTime').value;
			 
			 
			 
			 if(serviceupD=="" && serviceupT==""){
					
				 	document.getElementById('NonContactErrorMess').innerHTML = "";
					document.getElementById('NonContactErrorMessReason').innerHTML = "";
					document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
					document.getElementById('followUpdateErrorMess').innerHTML = "";
					document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
					
					document.getElementById('serviceDateErrorMess').innerHTML = "Select Service Date";
					document.getElementById('serviceTimeErrorMess').innerHTML = "Select Service Time";
					document.getElementById('serviceScheduledDate').focus(); 
					return false;
			 }else if(serviceupD==""){
				 
				 document.getElementById('NonContactErrorMess').innerHTML = "";
					document.getElementById('NonContactErrorMessReason').innerHTML = "";
					document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
					document.getElementById('followUpdateErrorMess').innerHTML = "";
					document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
					
					document.getElementById('serviceDateErrorMess').innerHTML = "Select Service Date";				
					document.getElementById('serviceScheduledDate').focus(); 
					return false;
			 }else if(serviceupT==""){
				 
				 document.getElementById('NonContactErrorMess').innerHTML = "";
					document.getElementById('NonContactErrorMessReason').innerHTML = "";
					document.getElementById('NonContactErrorMessAlreadyReason').innerHTML = "";
					document.getElementById('followUpdateErrorMess').innerHTML = "";
					document.getElementById('followUpdateTimeErrorMess').innerHTML = "";
					
					document.getElementById('serviceTimeErrorMess').innerHTML = "Select Service Time";
					document.getElementById('serviceScheduledTime').focus(); 
					return false;
				 
			 }else{
				 
				 $.blockUI();	  
				   return true;				 
			 }
			 
			 
			 
			 
			 
			 
		 }else{
			 
			 $.blockUI();	  
			   return true;	
			 
			 
			 
		 }
		
		
	}
	
}		
