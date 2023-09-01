	$(document).ready(function(){
			$(".showOnlyFutureDate").datepicker({
		changeMonth: true,
		changeYear: true,
		maxDate: "+30d",
		minDate: '0',
		showAnim: 'slideDown',
		dateFormat: 'yy-mm-dd'
	  });
	$('#FollowUpTime').keypress(function(e) {
		var regex = ["[0-2]",
		"[0-4]",
		":",
		"[0-6]",
		"[0-9]",
		"(A|P)",
		"M"],
		string = $(this).val() + String.fromCharCode(e.which),
		b = true;
		for (var i = 0; i < string.length; i++) {
			if (!new RegExp("^" + regex[i] + "$").test(string[i])) {
				b = false;
				alert('Incorrect Format!');
			}
		}
		return b;
	});
	$('#editRegistrationno').click(function(){
		$('.vehicalRegNo').removeAttr('readonly', true);
		$('#vehicalRegNo').focus();
		$("#editRegistrationno").css("display","none");
		$("#updateRegistrationno").css("display","block");
	});

	$('#editEngineno').click(function(){
		$('#engineNo').removeAttr('readonly', true);
		$("#editEngineno").css("display","none");
		$("#updateEngineno").css("display","block");
		$('#engineNo').focus();
	});

	$('#editChassisno').click(function(){
		$('#chassisNo').removeAttr('readonly', true);
		$("#editChassisno").css("display","none");
		$("#updateChassisno").css("display","block");
		$('#chassisNo').focus();
	});

	var preFlag=$('#pFlag').val();
	if(preFlag==1){
		$( "#checkbox3" ).prop( "checked", true );
	}
	else if(preFlag==2){
		$( "#checkbox2" ).prop( "checked", true );
	}
	else if(preFlag==3){
		$( "#checkbox1" ).prop( "checked", true );
	}
	$(".peditAddressmodal").click(function () {
		var mystring = $('#permanentAddress').val();
		var elements = mystring.split('_');
		$('.paddr_line1').val(elements[0]);
		$('.paddr_line2').val(elements[1]);
		$('.paddr_line3').val(elements[2]);
	});


	$(".paddr_submit").click(function () {
	   var paddressline1 = $('.paddr_line1').val();
	   var paddressline2 = $('.paddr_line2').val();
	   var paddressline3 = $('.paddr_line3').val();
	   var paddressline4 = $('.paddr_line4').val();
	   var paddressline5 = $('.paddr_line5').val();
	   var paddressline6 = $('.paddr_line6').val();
	   var paddressline7 = $('.paddr_line7').val();
	   var joinData = [paddressline1+'_'+paddressline2+'_'+paddressline3+'_'+paddressline4+'_'+paddressline5+'_'+paddressline6+'_'+paddressline7].join('_ ');
	   $('.permanentAddress').val(joinData);
	});


	$(".reditAddressmodal").click(function () {
		var mystring1 = $('#residenceAddress').val();
		var elements1 = mystring1.split('_');
		$('.raddr_line1').val(elements1[0]);
		$('.raddr_line2').val(elements1[1]);
		$('.raddr_line3').val(elements1[2]);
	});
	$(".raddr_submit").click(function () {
	   var readdressline1 = $('.raddr_line1').val();  
	   var readdressline2 = $('.raddr_line2').val(); 
	   var readdressline3 = $('.raddr_line3').val();
	   var readdressline4 = $('.raddr_line4').val();
	   var readdressline5 = $('.raddr_line5').val();
	   var readdressline6 = $('.raddr_line6').val();
	   var readdressline7 = $('.raddr_line7').val();
	   var joinData1 = [readdressline1+'_'+readdressline2+'_'+readdressline3+'_'+readdressline4+'_'+readdressline5+'_'+readdressline6+'_'+readdressline7].join('_ ');
	 $('#residenceAddress').val(joinData1);
	});

	$(".oeditAddressmodal").click(function () {
		var mystring2 = $('#officeAddress').val();
		var elements2 = mystring2.split('_');
		$('.oaddr_line1').val(elements2[0]);
		$('.oaddr_line2').val(elements2[1]);
		$('.oaddr_line3').val(elements2[2]);
	});

	$(".oaddr_submit").click(function () {
	//alert("hi");
	   var oaddressline1 = $('.oaddr_line1').val();
	   var oaddressline2 = $('.oaddr_line2').val();
	   var oaddressline3 = $('.oaddr_line3').val();
	   var oaddressline4 = $('.oaddr_line7').val();
	   var oaddressline5 = $('.oaddr_line4').val();
	   var oaddressline6 = $('.oaddr_line5').val();
	   var oaddressline7 = $('.oaddr_line6').val();
	   //console.log(oaddressline4);
	   //alert(oaddressline7);
	   var joinData2 = [oaddressline1+'_'+oaddressline2+'_'+oaddressline3+'_'+oaddressline4+'_'+oaddressline5+'_'+oaddressline6+'_'+oaddressline7].join('_ ');
	   $('#officeAddress').val(joinData2);
	   
	});

	

	$('#tpreffered_mode_contact_edit').click(function(){
		$("#modeOfCon").css("display","block");
		$("#tpreffered_mode_contact").css("display","none");
		$("#tpreffered_mode_contact_edit").css("display","none");
	});

	$('#tpreffered_day_contact_edit').click(function(){
		$("#daysWeek").css("display","block");
		$("#tpreffered_day_contact").css("display","none");
		$("#tpreffered_day_contact_edit").css("display","none");
	});
	$("#preffered_contact_num").on('change',function(){
		var getValue=$(this).val();
		$('#tanniversary_date1').val(getValue);
	});
	$(".box").click(function(){
	   $(this).box().toggleClass("circle");
	});


	var test="";
	var varWhatdidSay ="";
	var varPickupaddress ="";
	var varAlreadyServiced ="";
	var varAlreadyServicedR="";
	var varAddaddress="";
	var varTransferTootherCity="";
	var varAlreadyservicedadio="";
	var varServicedAtOtherDealer="";
	var mAlreadyServiced="";
	var mLeadYes="";
	var mcallInOut="";
	var varPickupaddressIn="";
	var mRandam="";
	var mVehicleSold="";
	var mPurchase="";
	var mLastQuestion="";
	//Inound variable
	var mLeadYesIn="";
	var mfeedbackYesIn="";

	//OutGoing Call Back to Next------------------------------------------->

	$("#NextToPurchaseCar").click(function(){
		
		var vcustname = $("#customerFNameConfirm").val();
		var vcustMobile1 = $("#Mobile1").val();
		var vcustPinInput = $("#PinInput").val();
					
		if(vcustname=='' && vcustMobile1==''){
			Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Enter The New Owner Details.'
						});
						return false;
			
		}
		
		else if(vcustname==''){
			Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Enter Name'
						});
						return false;
			
		}
		else if(vcustMobile1==''){
			
		
			Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Enter Phone No.'
						});
						
						return false;
						
						
			
		}
		else if(vcustMobile1.length < 10 ){
			
		
			Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Invalid Phone No.'
						});
						
						return false;
						
						
			
		}
		
		
		
	else{
		$("#VehicleSoldClickNo").show();
		$("#VehicelSoldYesRNo").hide();
		$("#whatDidCustSayDiv").hide();
		$(".VehicleSold").hide();
		$("#VehicleSoldClickYes").hide();
		}
		
	});

	$("#BackToPleaseComfNewOwnre").click(function(){
			$("#VehicelSoldYesRNo").show();
		$(".VehicleSold").show();
		$('.VehicleSold').removeAttr('checked',false);
		//$("#WhatdidtheCustomersayDIV").show();
		$("#whatDidCustSayDiv").show();
		$("#VehicleSoldClickNo").hide();
		$("#VehicleSoldClickYes").hide();
		$('#VehicleSoldYesbtn').attr('checked',false);
		$('#VehicleSoldNobtn').attr('checked',false);
		
	});

               
	$("#NextToLastQuestion").click(function(){
					var selectValDrop=$('#selected_department1').val();
			
			var selectValRemarks = $('#commentsOfFB').val();
		
			 var userfeedbackOutbound = 0;
				$('[name="listingForm.userfeedback"]').each(function (){
					if ($(this).is(':checked')) userfeedbackOutbound++;
				});
				if (userfeedbackOutbound == 0) {
							Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one of these.'
						});
						return false;
				}else{
					
					if($("#feedbackYes").prop('checked')){
						
					if(selectValDrop ==0 ){
							Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Select Department.'
						});
						return false;
						}
						
						if(selectValRemarks =='' ){
							Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Remarks Shouldnot empty.'
						});
						return false;
						}
						else{
					$("#LastQuestion").show();
					$("#CustFeedBack").hide();
					}						
					}
					$("#LastQuestion").show();
					$("#CustFeedBack").hide();
					
				}
					
	});

	$("#BackToCustomerFeedback").click(function(){
		$("#CustFeedBack").show();
		$("#LastQuestion").hide();
		
	});



	$("#backtoMain").click(function(){
					$("#SMRInteractionFirst").show();
					$("#DidYouTalkDiv").show();
					$("#whatDidCustSayDiv").hide();
					$("#serviceBookDiv").hide();
					$('#SpeakYes').attr('checked',false);
					$('#SpeakNo').attr('checked',false);
					
	});


	$("#CallaterBack").click(function(){
					$("#SMRInteractionFirst").show();
					$("#DidYouTalkDiv").show();
					$("#whatDidCustSayDiv").hide();
					$("#callMeLattteDiv").hide();
					$('#SpeakYes').attr('checked',false);
					$('#SpeakNo').attr('checked',false);
					
	});

	$("#DidUTalkNO").click(function(){
					$("#SMRInteractionFirst").show();
					$("#DidYouTalkDiv").show();
					$("#NotSpeachDiv").hide();
					$("#NoComments").hide();
					$('#SpeakYes').attr('checked',false);
					$('#SpeakNo').attr('checked',false);
					
	});

	$("#backToSNR").click(function(){
					$("#SMRInteractionFirst").hide();
					$("#DidYouTalkDiv").hide();
					$("#alreadyserviceDIV").show(); 
					$("#whatDidCustSayDiv").show();
					$('#SpeakYes').attr('checked',false);
					$('#SpeakNo').attr('checked',false);
					$("#VehicleSoldClickYes").hide();
					$("#WhatdidtheCustomersayDIV").show(); 
					$("#VehicelSoldYesRNo").show(); 
					$(".VehicleSold").show(); 
					$('#VehicleSoldYesbtn').attr('checked',false);
			
	});



	$("#nextToFinalEditInfoVS").click(function(){
		//$("#LastQuestion").show();
		$("#VehicelSoldQuestion").show();
		$("#VehicleSoldClickNo").hide();
	});
	$("#BackToVSHVNewCar").click(function(){
			//$("#LastQuestion").show();
			$("#VehicleSoldClickNo").show();
			$("#VehicelSoldQuestion").hide();
	});
	$("#NextDistanceForPopup").click(function(){
		$("#DistanceFoRRQuestion").show();
		$("#DistancefromDealerLocationDIV").hide();
	});
			
			
			$("#nextToCustomerDrive").click(function(){
				
				var FollowupDateSbooked=$("#date12345").val();
				var FollowupTimeSbooked=$("#CommittedTimes").val();
				var FollowupTypeSbooked=$("#serviceBookedTypeDisposition").val();
				var mSerAdvi=$("#serviceAdvisor").val();
				var mWorksId=$("#workshop").val();
					if( mWorksId !="" && mWorksId !="select" && mWorksId !="Select" && mWorksId !="0" && FollowupDateSbooked !="" && FollowupTimeSbooked !="" && FollowupTypeSbooked !="0"){
						$("#CustomerDriveInDiv").show();
						$("#serviceBookDiv").hide();
						$("#SMRInteractionFirst").hide();
						$("#DidYouTalkDiv").hide();
						$("#whatDidCustSayDiv").hide();
						
						var pickFlag=$('#selectedModeOfCont').val();
						if(pickFlag=="Customer Drive-In"){
							$( "#CustomerDriveInID" ).prop( "checked", true );
						}else if(pickFlag=="true"){
							$( "#PickupDropRequired" ).prop( "checked", true );
						}else if(pickFlag=="Maruthi Mobile Support"){
							$( "#MaruthiMobileSupport" ).prop( "checked", true );
						}
						
					}else{
						

						if(FollowupTypeSbooked ==  'select' ||  FollowupTypeSbooked == '0' ){
														Lobibox.notify('warning', {
														continueDelayOnInactiveTab: true,
														msg: 'Please select Service Booked Type.'
														});	
														return false;
												}
						
					if(mWorksId ==  'select' || mWorksId == ''  || mWorksId =='Select' ||  mWorksId == '0' ){
								Lobibox.notify('warning', {
								continueDelayOnInactiveTab: true,
								msg: 'Please select Workshop.'
								});	
								return false;
						}
					if(FollowupDateSbooked == ""){ 
									Lobibox.notify('warning', {
									continueDelayOnInactiveTab: true,
									msg: 'Please select date.'
										});										
								return false;
								}
					if(FollowupTimeSbooked == ""){ 
									Lobibox.notify('warning', {
									continueDelayOnInactiveTab: true,
									msg: 'Please select time.'
								});										
								return false;								
								}
					if(mSerAdvi ==null){
									Lobibox.notify('warning', {
									continueDelayOnInactiveTab: true,
									msg: 'Please select Service Advisor.'
								});	
								return false;

						}
						} 
				
			});
			
				$("#BackToCunstomerDrive").click(function(){
					$("#serviceBookDiv").show();
					$("#CustomerDriveInDiv").hide();
					$("#SMRInteractionFirst").hide();
					$("#DidYouTalkDiv").hide();
					$("#whatDidCustSayDiv").show();
					
					
					
				});
			
			
			
			
			$("#BackToLead").click(function(){
					$("#CustomerDriveInDiv").show();
					$("#finalDiv1").hide();
			});
//			
				$("#NextToCustFeedBack").click(function(){
			
				var atLeastOneIsChecked = 0;
				$('[name="listingForm.LeadYes"]').each(function (){
					if ($(this).is(':checked')) atLeastOneIsChecked++;
				});
				if (atLeastOneIsChecked == 0) {
					Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please check one.'
						});
				
				}else{ 
				if($("#LeadYesID").prop('checked')){
					 var checkeds = $('.myOutCheckbox').is(':checked');
					
					if(checkeds){
						
					}else{
						
						Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please check one of these.'
							
						});
						return false;
					}
				}else if($("#LeadNoID").prop('checked')){
					
				}
				
				
					$("#CustFeedBack").show();
					$("#finalDiv1").hide();
				} 
				
				var complFB=$('#selectedFBComp').val();
				
				if(complFB == "0" || complFB == null ||complFB == ""){
					$( "#feedbackNo" ).prop( "checked", true );
				}else{
					$( "#feedbackYes" ).prop( "checked", true );
					$("#feedbackDIV").show();
					loadLeadBasedOnLocationDepartment();
					var rem=$('#enteredRMFB').val();
					$('#commentsOfFB').val(rem);

				}
				
			});
				
				$("#NextToPurchaseNewcarNO").click(function(){
					var chkVehSold = 0;
				$('[name="listingForm.VehicleSoldYes"]').each(function (){
					if ($(this).is(':checked')) chkVehSold++;
				});
				if (chkVehSold == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please choose one.'
						});
						return false;
					
				}else{
					$("#VehicleSoldClickNo").show();
					$("#VehicelSoldYesRNo").hide();
					$("#whatDidCustSayDiv").hide();
					$(".VehicleSold").hide();
				}
					
				});
				
				
				$("#NextToAlreadyServicePopup").click(function(){
					$("#AlreadySerivePopup").show();
					$("#alreadyservicedDiv1").hide();
				});
				
		//added on 02/01/2017		
	$("#nextToAlreadySrviceUpsell").click(function(){
					
				var chknoReq = 0;
				$('[name="srdisposition.reasonForHTML"]').each(function (){
					if ($(this).is(':checked')) chknoReq++;
				});
				
				var chknoReqChi = 0;
				$('[name="srdisposition.ServicedAtOtherDealerRadio"]').each(function (){
					if ($(this).is(':checked')) chknoReqChi++;
				});
				
				
				if (chknoReq == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please choose one.'
						});
						return false;
					
				}else{
					if($('#ServicedOtherDealer').prop('checked')){
						if (chknoReqChi == 0) {
						Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please choose one.'
						});
						return false;
					
				}
				if($('#Autorizedworkshopid').prop('checked')){
					
					if($('#CheckedwithDMS').prop('checked') !=true){
						Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Checked Verified with DMS.'
						});
						return false;
					}
					
				}
					}else{
						
					}
					$("#AlreadyServiceUpsellOpp").show();
						$("#alreadyservicedDiv1").hide();
						$(".AlreadyServiced").hide();
						$(".AlreadyServiced").attr('checked',true);
						$("#WhatdidtheCustomersayDIV").hide();
				}
				});
					
			$("#NextDisatisfiedPrPopup").click(function(){
					var AssignedToSA= $("#assignedToSA").val();
					if( AssignedToSA !='0' && AssignedToSA !='Select' && AssignedToSA !='select' ){
						$("#DisatisfiedPreQuestion").show();
						$("#txtDissatisfiedwithpreviousservice").hide();
					}else{
					if( AssignedToSA =='0'|| AssignedToSA =='' || AssignedToSA =='Select' || AssignedToSA =='select' ){
						alert('Please Assign To Service Advisor');
						}
					}
			});
				$("#NextDisSatisSalePopup").click(function()
				{
					$("#DisstisFiedSaleRQuestion").show();
					$("#DissatisfactionwithSalesREmarksDiv").hide();
						
				});
				$("#NextDisSatInsurancPopup").click(function()
				{
					$("#DisstisInsurancQuestion").show();
					$("#DissatisfactionwithInsuranceREmarksDiv").hide();
						
				});
					$("#NextOthersPopup").click(function()
				{
					var commentRemarks =$('#commentsOtherRemarks').val();
					if (commentRemarks == '') {
							Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Remarks sholud not Blank.'
						});
				}else{
					$("#OthersLastQuestion").show();
					$("#OtherSeriveRemarks").hide();
				}
						
				});





				
				
	//Incomming Call Back to next---------------------------------------->
			$("#NextToLead").click(function(){
				
					 var inc = 0;
				$('[name="servicebooked.typeOfPickup"]').each(function (){
					if ($(this).is(':checked')) inc++;
				});
				if (inc == 0) {
							Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please check one.'
						});
				}else{
					if($('#PickupDropRequired').prop('checked')){
						var selectDriverVal = $('#driverIdSelect').val();
						var selectTimeFromVal = $('#time_FromDriver').val();
						var selectTimeToVal = $('#time_ToDriver').val();
						
						if(selectDriverVal == 0){
							Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Select from Drop down.'
						});
						return false;
						}
						if(selectTimeFromVal == ''){
							Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'From Time Is Required.'
						});
						return false;
						}
						if(selectTimeToVal == ''){
							Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'To Time Is Required.'
						});
						return false;
						}
					}
				$("#finalDiv1").show();
				$("#CustomerDriveInDiv").hide();
				}
				
				var upselFlag=$('#selectedUpselOpp').val();				
				
				if(upselFlag == "0" || upselFlag == null || upselFlag == "()"){
					$( "#LeadNoID" ).prop( "checked", true );
				}else{
					$( "#LeadYesID" ).prop( "checked", true );
					$("#LeadDiv").show();
				}
				loadLeadBasedOnLocation();
				var sr_int_id=$('#srdispo_id').val();
				//alert("sr_int_id : "+sr_int_id);		
				
				var urlDisposition = "/CRE/upsellSelectedLastSB/"+sr_int_id+"";
				$.ajax({
					
			        url: urlDisposition

			    }).done(function (upselData) {
			    	
			        console.log(upselData);
			        
			        for (var i = 0; i < upselData.length; i++) {
			        	
			       if(upselData[i].upSellType == "Insurance"){ 
			        	
			        $('#InsuranceIDCheck').prop('checked',true);			    		
			    	$('#InsuranceSelect').show();
			    	$('#comments1').val(upselData[i].upsellComments);
			    		
			    
			       }else if(upselData[i].upSellType == "Warranty / EW"){
			    	   $('#WARRANTYID').prop('checked',true);			    		
				    	$('#WARRANTYSelect').show();
				    	$('#comments2').val(upselData[i].upsellComments);
			    	
			       }else if(upselData[i].upSellType == "Re-Finance / New Car Finance"){

			    	   $('#ReFinanceIDCheck').prop('checked',true);			    		
				    	$('#ReFinanceSelect').show();
				    	$('#comments3').val(upselData[i].upsellComments);
				    	
			    	
			       }else if(upselData[i].upSellType == "VAS"){

			    	   $('#VASID').prop('checked',true);			    		
				    	$('#VASTagToSelect').show();
				    	$('#comments4').val(upselData[i].upsellComments);
			    	
			       }else if(upselData[i].upSellType == "Sell Old Car"){

			    	   $('#LoanID').prop('checked',true);				    		
				    	$('#LoanSelect').show();
				    	$('#comments5').val(upselData[i].upsellComments);
			    	
			        }else if(upselData[i].upSellType == "Buy New Car / Exchange"){
			        
			        	 $('#EXCHANGEID').prop('checked',true);			    		
					    	$('#EXCHANGEIDSelect').show();
					    	$('#comments6').val(upselData[i].upsellComments);
			    	
			        }else if(upselData[i].upSellType == "UsedCar"){

			        	 $('#UsedCarID').prop('checked',true);				    		
					    	$('#UsedCarSelect').show();
					    	$('#comments7').val(upselData[i].upsellComments);
					    	
			    	
			  	  
			        }
			        }
			    });
				
				
				
			});
				
			$("#nextToCustomerDriveIn").click(function(){
				
				var FollowupDateinboundData=$("#date123456").val();
				var FollowupTimeinboundData=$("#CommittedTimesIn").val();
				var mSerAdvis=$("#serviceAdvisorIdIn").val();
				var mWorksIds=$("#workshopIn").val();
					if( mWorksIds !="" && mWorksIds !="select" && mWorksIds !="Select" && mWorksIds !="0" && FollowupDateinboundData !="" && FollowupTimeinboundData !=""  && mSerAdvis !="select" && mSerAdvis !="select" && mSerAdvis !="" && mSerAdvis !="0" ){
					$("#CustomerDriveInDivIn").show();
					$("#InCallserviceBookDiv").hide();
					$("#SMRInteractionFirst").hide();
					$("#BookMyServiceIn").hide();
					$("#LeadSourceHideIn").hide();
					
					
					var pickFlag=$('#selectedModeOfCont').val();
					if(pickFlag=="Customer Drive-In"){
						$( "#CustomerDriveInIDIn" ).prop( "checked", true );
					}else if(pickFlag=="true"){
						$( "#PickupDropRequiredIn" ).prop( "checked", true );
					}else if(pickFlag=="Maruthi Mobile Support"){
						$( "#MaruthiMobileSupportIn" ).prop( "checked", true );
					}
						
					}else{
						
					if(mWorksIds ==  'select' || mWorksIds == ''  || mWorksIds =='Select' ||  mWorksIds == '0' ){
								Lobibox.notify('warning', {
								continueDelayOnInactiveTab: true,
								msg: 'Please select Workshop.'
								});	
								return false;
						}
					if(FollowupDateinboundData == ""){ 
									Lobibox.notify('warning', {
									continueDelayOnInactiveTab: true,
									msg: 'Please select date.'
										});										
								return false;
								}
					if(FollowupTimeinboundData == ""){ 
									Lobibox.notify('warning', {
									continueDelayOnInactiveTab: true,
									msg: 'Please select time.'
								});										
								return false;								
								}
					if(mSerAdvis == 'select' || mSerAdvis == '' || mSerAdvis == 'Select' ||  mSerAdvis == '0' ){
									Lobibox.notify('warning', {
									continueDelayOnInactiveTab: true,
									msg: 'Please select Service Advisor.'
								});	
								return false;

						}
						} 
				
			});
			

			
			$("#BackToCunstomerDriveIn").click(function(){
				$("#LeadSourceHideIn").hide();
				$("#SMRInteractionFirst").hide();
					$("#InCallserviceBookDiv").show();
					$("#CustomerDriveInDivIn").hide();
					$("#BookMyServiceIn").show();
			});
			
				
				
				
		$("#BackToCustomerMainInBound").click(function(){
				$("#LeadSourceHideIn").show();
				$("#SMRInteractionFirst").show();
				$("#InCallServiceBook").show();
				//$('#InCallServiceBook').attr('checked',false);
				$('#BookMyServiceIn').show();
				$('#InCallserviceBookDiv').hide();
				//$('#BookMyServiceIn').removeAttr('checked');
				$('#InCallServiceBook').removeAttr('checked');
	});
			
			
					$("#BackToLeadIn").click(function()
						{
							$("#CustomerDriveInDivIn").show();
							$("#finalDiv1Inbound").hide();
							
							
					});

					
					$("#VehicleSoldYesbtn").click(function(){
						$("#WhatdidtheCustomersayDIV").hide();
						$("#VehicelSoldYesRNo").hide();
						$(".VehicleSold").hide();
						$(".backToAllSNR").hide();
						$(".backToYesSNR").show();
						
					});
				$("#VehicleSoldNobtn").click(function(){
						$(".backToYesSNR").hide();
						$(".backToAllSNR").show();
				});

					
					$("#backToAlreadyServicediv").click(function(){
						$("#WhatdidtheCustomersayDIV").show();	
						$(".VehicleSold").show();
						
						$("#VehicleSoldYesbtn").attr("checked",false);
						$("#VehicleSoldNobtn").attr("checked",false);
						$("#alreadyserviceDIV").show();
						
						if($("#alreadyserviceDIV").attr("checked",false)){
							$("#VehicelSoldYesRNo").hide();
							$("#VehicleSold").attr("checked",false);
							$("#alreadyservicedDiv1").hide();
							$(".Dissatisfiedwithpreviousservice").show();
							$(".Distancefrom").show();
							$(".DissatisfiedwithSalesID").show();
							$(".DissatisfiedwithInsuranceId").show();
							$(".Stolen").show();
							$(".Totalloss").show();
							$(".OtherLast").show();
							$(".AlreadyServiced").show();
							$("#VehicleSoldClickYes").hide();
							$("#PurchaseClickYes").hide();
							$("#VehicleSoldClickNo").hide();
															

							}
						});
				$("#NextToCustFeedBackIn").click(function(){
				
				var chkincl = 0;
				$('[name="LeadYesIn"]').each(function (){
					if ($(this).is(':checked')) chkincl++;
				});
				if (chkincl == 0) {
					Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please check one.'
						});
				
				}else{ 
				if($("#LeadYesIDIn").prop('checked')){
					 var checked = $('.myCheckbox').is(':checked');
					
					if(checked){
						
					}else{
						
						Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please check one of these.'
							
						});
						return false;
					}
				}else if($("#LeadNoIDIn").prop('checked')){
					
				}
				
				
					$("#CustFeedBackIn").show();
					$("#CustomerDriveInDiv").hide();
					$("#finalDiv1Inbound").hide();
				} 
				
				
				var complFB=$('#selectedFBComp').val();
				
				if(complFB == "0" || complFB == null ||complFB == ""){
					$( "#feedbackNoIn" ).prop( "checked", true );
				}else{
					$( "#feedbackYesIn" ).prop( "checked", true );
					$("#feedbackDIVIn").show();
					loadLeadBasedOnLocationDepartment();
					var rem=$('#enteredRMFB').val();
					$('#commentsDSA').val(rem);

				}
				
				
				});
			
			$("#BackToUpsellIn").click(function(){
				$(".myCheckbox").prop('checked', false); 
				$("#CustFeedBackIn").hide();
				$("#InsuranceSelectIn").hide();
				$("#WARRANTYSelectIn").hide();
				$("#VASTagToSelectIn").hide();
				$("#ReFinanceSelectIn").hide();
				$("#LoanSelectIn").hide();
				$("#EXCHANGEIDSelectIn").hide();
				$("#UsedCarSelectIn").hide();
				$("#finalDiv1Inbound").show();
				});
				
				$("#BackToUpsell").click(function(){
				 $("input[type=checkbox]"). prop("checked",false);
				 
				 $("#InsuranceSelect").hide();
				 $("#WARRANTYSelect").hide();
				 $("#VASTagToSelect").hide();
				 $("#ReFinanceSelect").hide();
				 $("#LoanSelect").hide();
				 $("#EXCHANGEIDSelect").hide();
				 $("#UsedCarSelect").hide();
				$("#CustFeedBack").hide();
				$("#finalDiv1").show();
			});
				$("#OutBoundDiv").show();
				
	 $("input[name$='InOutCallName']").click(function(){
			var mcallInOut = $(this).val();
			if(mcallInOut=="OutCall")
			{
				//$('#OutGoingID').attr('checked',true);
				$("#OutBoundDiv").show();
				$("#InBoundDiv").hide();
				
			}
			else
			{
			$("#InBoundDiv").show();
			$("#OutBoundDiv").hide();
			//$('#InCallServiceBook').attr('checked',true);

			$("#BookMyServiceIn").show();
			}
	});


	 
		//PickUp Required Singl CheckBox InBound Call
			$('.InCallServiceBook').click(function() {
			if( $(this).is(':checked')) {
				$("#InCallserviceBookDiv").show();
				$("#LeadSourceHideIn").hide();
				$("#SMRInteractionFirst").hide();
				
				
			} else {
				$("#InCallserviceBookDiv").hide();
				$("#LeadSourceHideIn").show();
				$("#SMRInteractionFirst").show();
			}
		});

	$("input[name$='typeOfDisposition']").click(function() {
		test = $(this).val();
		if(test=="Contact")
		{
			$('#BtnNoSubmit').hide();
			$('#NoComments').hide();
			$('#alreadyserviceDIV').hide();
			$("#NotSpeachDiv").hide();
			
			
			$("#WhatdidtheCustomersayDIV").show();
			$('#whatDidCustSayDiv').show();
			$('#NextToModdelDiv').show();
		
			
			
			
			
			validateCheck();
			//alreadySericedCheck();
		}
		if(test=="NonContact")
		{
			validateCheck();
			//alreadySericedCheck();
			$("#DidYouTalkDiv").hide();
			$("#SMRInteractionFirst").hide();
			
			$("#NotSpeachDiv").show();
			$("#WhatdidtheCustomersayDIV").hide();
			$('#whatDidCustSayDiv').hide();
			$("#alreadyserviceDIV").hide();
			$('#callMeLattteDiv').hide();
			$('#serviceBookDiv').hide();
			$('#alreadyservicedDiv1').hide();
			$('#MiddelDiv').hide();
			$('#finalDiv').hide();
			
			$('#BtnNoSubmit').show();
			$('#NoComments').show();
			$('#NextToModdelDiv').hide();
			$('#CustomerDriveInDiv').hide();
			
			
			
			
			
		}	
	});

				//Randam Access CRE List

		 $("input[name$='Random']").click(function() {
			var mRandam = $(this).val();
			if(mRandam=="ChoseCRE List")
			{
				$("#CREListSelect").show();
				
			}
			else
			{
			$("#CREListSelect").hide();
			}
	});

	function validateCheck()
	{
		if(varWhatdidSay!="")
		{
			if(varWhatdidSay == "Book My Service")
			{
				$("#serviceBookDiv").show();
				$("#callMeLattteDiv").hide();
				$("#alreadyserviceDIV").hide();
				$("#SMRInteractionFirst").hide();
				$("#DidYouTalkDiv").hide();
				
			}
			if(varWhatdidSay == "Call Me Later")
			{
				//$("#callMeLattteDiv").show();
				$('#callMeLattteDiv').show();
					

				$("#serviceBookDiv").hide();				
				$("#alreadyserviceDIV").hide();
				$("#alreadyservicedDiv1").hide();
				$("#SMRInteractionFirst").hide();
				$("#DidYouTalkDiv").hide();
				
				
					
				
			}
			if(varWhatdidSay == "Service Not Required")
			{
			//console.log(varWhatdidSay);
				$("#serviceBookDiv").hide();
				$("#callMeLattteDiv").hide();
				$("#alreadyserviceDIV").show();
				$("#SMRInteractionFirst").hide();
				$("#DidYouTalkDiv").hide();
				
			}
		}
		
	}


		

	$("input[name$='typeOfPickup']").click(function(){
		varPickupaddress = $(this).val();

		if(varPickupaddress== "true")
		{
		 $("#pickupDiv").show();

		 $("#MSSSelectDiv").hide();
		 
		}
		else if(varPickupaddress== "Maruthi Mobile Support")
		{

			$("#pickupDiv").hide();
			$("#MSSSelectDiv").show();
		}

		else
		{
			$("#pickupDiv").hide();
			
			$("#MSSSelectDiv").hide();
			
			
		}
	});

		//Inbound ServiceBook
		$("input[name$='typeOfPickupIn']").click(function(){
		varPickupaddressIn = $(this).val();

		if(varPickupaddressIn== "true")
		{
		 $("#pickupDivIn").show();
		 
		 $("#MSSSelectDivIn").hide();
		 
		}
		else if(varPickupaddressIn== "Maruthi Mobile SupportIn")
		{
			
			$("#pickupDivIn").hide();
			$("#MSSSelectDivIn").show();
		}

		else
		{
			$("#pickupDivIn").hide();
			
			$("#MSSSelectDivIn").hide();
			
			
		}
	});




			//PickUp Required Singl CheckBox
		$('.AddnewAddressClass').click(function() {
		if( $(this).is(':checked')) {
			$("#AddAddressDiv").show();
		  
		} else {
			$("#AddAddressDiv").hide();
		}
	});



	$('#AlreadyServiced').click(function(){
		if($(this).prop('checked')){
		   $(this).parent().addClass("redBackground"); 
		$("#alreadyservicedDiv1").show(); 
		$("#txtDissatisfiedwithpreviousservice").hide();
		$("#DistancefromDealerLocationDIV").hide();
		$("#DissatisfactionwithInsuranceREmarksDiv").hide();
		$("#DissatisfactionwithSalesREmarksDiv").hide();
		$("#OtherSeriveRemarks").hide();
		$(".VehicleSold").hide();
		$(".Dissatisfiedwithpreviousservice").hide();
		$(".Distancefrom").hide();
		$(".DissatisfiedwithSalesID").hide();
		$(".DissatisfiedwithInsuranceId").hide();
		$(".Stolen").hide();
		$(".Totalloss").hide();
		$(".OtherLast").hide();
		}
		else
		{
		$("#alreadyservicedDiv1").hide();
		$(".VehicleSold").show();
		$(".Dissatisfiedwithpreviousservice").show();
		$(".Distancefrom").show();
		$(".DissatisfiedwithSalesID").show();
		$(".DissatisfiedwithInsuranceId").show();
		$(".Stolen").show();
		$(".Totalloss").show();
		$(".OtherLast").show();
		$("#AlreadySerivePopup").hide();
		}
	});

		
	$('#VehicleSold').click(function(){
		
		if($(this).prop('checked'))
		{
		$("#VehicelSoldYesRNo").show(); 
		$("#alreadyservicedDiv1").hide(); 
		$("#txtDissatisfiedwithpreviousservice").hide();
		$("#DistancefromDealerLocationDIV").hide();
		$("#DissatisfactionwithInsuranceREmarksDiv").hide();
		$("#DissatisfactionwithSalesREmarksDiv").hide();
		$("#OtherSeriveRemarks").hide();
		$(".Dissatisfiedwithpreviousservice").hide();
		$(".Distancefrom").hide();
		$(".DissatisfiedwithSalesID").hide();
		$(".DissatisfiedwithInsuranceId").hide();
		$(".Stolen").hide();
		$(".Totalloss").hide();
		$(".OtherLast").hide();
		$(".AlreadyServiced").hide();
		}
		else
		{
		$("#VehicelSoldYesRNo").hide();
		$("#alreadyservicedDiv1").hide();
		$(".Dissatisfiedwithpreviousservice").show();
		$(".Distancefrom").show();
		$(".DissatisfiedwithSalesID").show();
		$(".DissatisfiedwithInsuranceId").show();
		$(".Stolen").show();
		$(".Totalloss").show();
		$(".OtherLast").show();
		$(".AlreadyServiced").show();
		$("#VehicleSoldClickYes").hide();
		$("#PurchaseClickYes").hide();
		$("#VehicleSoldClickNo").hide();
		}
	});
	$('#Dissatisfiedwithpreviousservice').click(function(){
		if($(this).prop('checked')){
		$("#txtDissatisfiedwithpreviousservice").show(); 
		$("#alreadyservicedDiv1").hide();
		$("#DistancefromDealerLocationDIV").hide();
		$("#DissatisfactionwithInsuranceREmarksDiv").hide();
		$("#DissatisfactionwithSalesREmarksDiv").hide();
		$("#OtherSeriveRemarks").hide();
		$(".Distancefrom").hide();
		$(".DissatisfiedwithSalesID").hide();
		$(".DissatisfiedwithInsuranceId").hide();
		$(".Stolen").hide();
		$(".Totalloss").hide();
		$(".OtherLast").hide();
		$(".AlreadyServiced").hide();
		$(".VehicleSold").hide();
		$("#DisatisfiedPreQuestion").hide();
		}
		else
		{
		$("#txtDissatisfiedwithpreviousservice").hide();
		$(".Distancefrom").show();
		$(".DissatisfiedwithSalesID").show();
		$(".DissatisfiedwithInsuranceId").show();
		$(".Stolen").show();
		$(".Totalloss").show();
		$(".OtherLast").show();
		$(".AlreadyServiced").show();
		$(".VehicleSold").show();
		$("#DisatisfiedPreQuestion").hide();
		}
	});	
	$('#Distancefrom').click(function(){	 
		if($(this).prop('checked')){
		$("#DistancefromDealerLocationDIV").show();
		$("#alreadyservicedDiv1").hide();
		$("#DissatisfactionwithInsuranceREmarksDiv").hide();
		$("#txtDissatisfiedwithpreviousservice").hide();
		$("#DissatisfactionwithSalesREmarksDiv").hide();
		$("#OtherSeriveRemarks").hide();
		$(".Dissatisfiedwithpreviousservice").hide();
		$(".DissatisfiedwithSalesID").hide();
		$(".DissatisfiedwithInsuranceId").hide();
		$(".Stolen").hide();
		$(".Totalloss").hide();
		$(".OtherLast").hide();
		$(".AlreadyServiced").hide();
		$(".VehicleSold").hide();
		$("#DistanceFoRRQuestion").hide();
		}else{
		$("#DistancefromDealerLocationDIV").hide();
		$(".Dissatisfiedwithpreviousservice").show();
		$(".DissatisfiedwithSalesID").show();
		$(".DissatisfiedwithInsuranceId").show();
		$(".Stolen").show();
		$(".Totalloss").show();
		$(".OtherLast").show();
		$(".AlreadyServiced").show();
		$(".VehicleSold").show();
		$("#DistanceFoRRQuestion").hide();
		}
		
	});	

	$('#DissatisfiedwithSalesID').click(function(){
		if($(this).prop('checked')){
		$("#DissatisfactionwithSalesREmarksDiv").show();
		$("#alreadyservicedDiv1").hide();
		$("#DistancefromDealerLocationDIV").hide();	
		$("#DissatisfactionwithInsuranceREmarksDiv").hide();
		$("#txtDissatisfiedwithpreviousservice").hide();
		$("#OtherSeriveRemarks").hide();
		$(".Dissatisfiedwithpreviousservice").hide();
		$(".Distancefrom").hide();
		$(".DissatisfiedwithInsuranceId").hide();
		$(".Stolen").hide();
		$(".Totalloss").hide();
		$(".OtherLast").hide();
		$(".AlreadyServiced").hide();
		$(".VehicleSold").hide();
		}else{
		$("#DissatisfactionwithSalesREmarksDiv").hide();
		$(".Dissatisfiedwithpreviousservice").show();
		$(".Distancefrom").show();
		$(".DissatisfiedwithInsuranceId").show();
		$(".Stolen").show();
		$(".Totalloss").show();
		$(".OtherLast").show();
		$(".AlreadyServiced").show();
		$(".VehicleSold").show();
		$("#DisstisFiedSaleRQuestion").hide();
		}	
	});
	$('#DissatisfiedwithInsuranceId').click(function(){
		if($(this).prop('checked')){
		$("#DissatisfactionwithInsuranceREmarksDiv").show(); 
		$("#alreadyservicedDiv1").hide();
		$("#DistancefromDealerLocationDIV").hide();
		$("#txtDissatisfiedwithpreviousservice").hide();
		$("#DissatisfactionwithSalesREmarksDiv").hide();
		$("#OtherSeriveRemarks").hide();
		$(".Dissatisfiedwithpreviousservice").hide();
		$(".Distancefrom").hide();
		$(".DissatisfiedwithSalesID").hide();
		$(".Stolen").hide();
		$(".Totalloss").hide();
		$(".OtherLast").hide();
		$(".AlreadyServiced").hide();
		$(".VehicleSold").hide();
		}else{
		$("#DissatisfactionwithInsuranceREmarksDiv").hide();
		$(".Dissatisfiedwithpreviousservice").show();
		$(".Distancefrom").show();
		$(".DissatisfiedwithSalesID").show();
		$(".Stolen").show();
		$(".Totalloss").show();
		$(".OtherLast").show();
		$(".AlreadyServiced").show();
		$(".VehicleSold").show();
		$("#DisstisInsurancQuestion").hide();
		}
	});

	$('#Stolen').click(function(){	 
		if($(this).prop('checked'))	{
		$("#alreadyservicedDiv1").hide(); 
		$("#txtDissatisfiedwithpreviousservice").hide();
		$("#DistancefromDealerLocationDIV").hide();
		$("#DissatisfactionwithInsuranceREmarksDiv").hide();
		$("#DissatisfactionwithSalesREmarksDiv").hide();
		$("#OtherSeriveRemarks").hide();
		$(".Dissatisfiedwithpreviousservice").hide();
		$(".Distancefrom").hide();
		$(".DissatisfiedwithSalesID").hide();
		$(".DissatisfiedwithInsuranceId").hide();
		$(".Totalloss").hide();
		$(".OtherLast").hide();
		$(".AlreadyServiced").hide();
		$(".VehicleSold").hide();
		$("#stolenHideShowSubmit").show();
		}else{
		$("#alreadyservicedDiv1").hide();
		$(".Dissatisfiedwithpreviousservice").show();
		$(".Distancefrom").show();
		$(".DissatisfiedwithSalesID").show();
		$(".DissatisfiedwithInsuranceId").show();
		$(".Totalloss").show();
		$(".OtherLast").show();
		$(".AlreadyServiced").show();
		$(".VehicleSold").show();
		$("#stolenHideShowSubmit").hide();
		}
	});
	$('#Totalloss').click(function(){
		if($(this).prop('checked')){
		$("#alreadyservicedDiv1").hide(); 
		$("#txtDissatisfiedwithpreviousservice").hide();
		$("#DistancefromDealerLocationDIV").hide();
		$("#DissatisfactionwithInsuranceREmarksDiv").hide();
		$("#DissatisfactionwithSalesREmarksDiv").hide();
		$("#OtherSeriveRemarks").hide();
		$(".Dissatisfiedwithpreviousservice").hide();
		$(".Distancefrom").hide();
		$(".DissatisfiedwithSalesID").hide();
		$(".DissatisfiedwithInsuranceId").hide();
		$(".Stolen").hide();
		$(".OtherLast").hide();
		$(".AlreadyServiced").hide();
		$(".VehicleSold").hide();
		$("#stolenHideShowSubmit").show();
		}else{
		$("#alreadyservicedDiv1").hide();
		$(".Dissatisfiedwithpreviousservice").show();
		$(".Distancefrom").show();
		$(".DissatisfiedwithSalesID").show();
		$(".DissatisfiedwithInsuranceId").show();
		$(".Stolen").show();
		$(".OtherLast").show();
		$(".AlreadyServiced").show();
		$(".VehicleSold").show();
		$("#stolenHideShowSubmit").hide();
		}
	});

	$('#Other').click(function(){
		if($(this).prop('checked')){
		$("#OtherSeriveRemarks").show(); 
		$("#alreadyservicedDiv1").hide();
		$("#DistancefromDealerLocationDIV").hide();
		$("#DissatisfactionwithInsuranceREmarksDiv").hide();
		$("#txtDissatisfiedwithpreviousservice").hide();
		$("#DissatisfactionwithSalesREmarksDiv").hide();
		$(".Dissatisfiedwithpreviousservice").hide();
		$(".Distancefrom").hide();
		$(".DissatisfiedwithSalesID").hide();
		$(".DissatisfiedwithInsuranceId").hide();
		$(".Totalloss").hide();
		$(".Stolen").hide();
		$(".AlreadyServiced").hide();
		$(".VehicleSold").hide();
		}else{
		$("#OtherSeriveRemarks").hide();
		$(".Dissatisfiedwithpreviousservice").show();
		$(".Distancefrom").show();
		$(".DissatisfiedwithSalesID").show();
		$(".DissatisfiedwithInsuranceId").show();
		$(".Stolen").show();
		$(".Totalloss").show();
		$(".OtherLast").show();
		$(".AlreadyServiced").show();
		$(".VehicleSold").show();
		$("#OthersLastQuestion").hide();
		}
	});

	//Vehicel Sold Info
	$("input[name$='VehicleSoldYes']").click(function(){
		mVehicleSold = $(this).val();
		if(mVehicleSold== "VehicleSold Yes")
		{
		$("#VehicleSoldClickYes").show();
		}else{
		$("#VehicleSoldClickYes").hide();
		}
		});

	//Purchase
	$("input[name$='PurchaseYes']").click(function(){
		mPurchase = $(this).val();
		if(mPurchase== "Purchase Yes"){
		$("#PurchaseClickYes").show();
		}else{
		$("#PurchaseClickYes").hide();
		}
		});		
	//Last Question
	$("input[name$='CustomerFeedBackYes']").click(function(){
		mLastQuestion = $(this).val();
		if(mLastQuestion== "Customer Yes"){
		$('#addBtn').modal('show');
		}else{
		$('#addBtn').modal('hide');
		}
		});
	//VEhicelSold LastQuestion
	//Last Question.
	$("input[name$='DistancefromDealerLocationRadio']").click(function(){
		varTransferTootherCity = $(this).val();
		if(varTransferTootherCity== "Transfer to other city"){
		 $("#txtTransfertoothercity").show();
		 $("#txtToofar").hide();
		}else{
		$("#txtTransfertoothercity").hide();
		$("#txtToofar").show();
		}
	});
	
	$("input[name$='disposition']").click(function(){
		varWhatdidSay = $(this).val();
		if(varWhatdidSay == "Book My Service" || varWhatdidSay == "Call Me Later" || varWhatdidSay == "Service Not Required" )
		{
		$('#callMeLaterA').removeAttr("disabled", true);
		$('#alreadyServiced').removeAttr("disabled", true);
		$('#vehicleSold').removeAttr("disabled", true);
		$('#dissatifiedwithPreviousService').removeAttr("disabled", true);
		$('#dissatifiedwithPreviousServices').removeAttr("disabled", true);
		$('#dissatifiedwithPService').removeAttr("disabled", true);
		$('#dissatifiedwithService').removeAttr("disabled", true);
		$('#dissatifiedwithStolen').removeAttr("disabled", true);
		$('#vehicleSoldStolen').removeAttr("disabled", true);
		$('#stolenHideShowSubmit').removeAttr("disabled", true);
		
			validateCheck();
		$('#finalDiv1').hide();	
		$('#CustomerDriveInDiv').hide();	
		$('#feedbackDIV').hide();	
		$('#CustFeedBack').hide();
		}else{
		$('#alreadyservicedDiv1').hide();
		}
	});


	$("input[name$='reasonForHTML']").click(function(){
			varAlreadyservicedadio = $(this).val();

		if(varAlreadyservicedadio == "Serviced At My Dealer"){
			$('#ServicedMyDealerDiv').show();
			$('#ServicedAtOtherDealerDiv').hide();
		}else{
			$('#ServicedAtOtherDealerDiv').show();
			$('#ServicedMyDealerDiv').hide();
		}
	});

	$("input[name$='ServicedAtOtherDealerRadio']").click(function(){
			varServicedAtOtherDealer = $(this).val();

		if(varServicedAtOtherDealer == "Autorized workshop"){
			$('#AutorizedworkshopDIV').show();
			$('.CheckedwithDMS').show();
		}
		else if(varServicedAtOtherDealer == "Non Autorized workshop"){
			$('#NonAutorizedworkshopDiv').show();
			$('.CheckedwithDMS').hide();
		}else{
			$('#AutorizedworkshopDIV').hide();
			$('#NonAutorizedworkshopDiv').hide();
			$('.CheckedwithDMS').hide();
		}
	});
	$('.NoService').click(function(){
			$('.NoService').not(this).prop('checked', false);
			varAlreadyServicedR = $(this).val();
			if(varAlreadyServicedR== "Already Serviced" || varAlreadyServicedR== "Vehicle Sold" || varAlreadyServicedR== "Dissatisfied with previous service" || varAlreadyServicedR== "Distance from Dealer Location" || varAlreadyServicedR== "Dissatisfied with Sales" || varAlreadyServicedR== "Dissatisfied with Insurance" || varAlreadyServicedR== "Stolen" || varAlreadyServicedR== "Total loss" || varAlreadyServicedR== "Other Serive")
			{
				alreadySericedCheck();
			}
		});
	$("input[name$='reasonForHTML']").click(function(){
		varAlreadyServiced = $(this).val();
	}); 

	//OutBound Upsell Opportunity--------->
	$('#InsuranceIDCheck').click(function(){
		if($(this).prop('checked')){
			$('#InsuranceSelect').show();
		}else{
			$('#InsuranceSelect').hide();
		}
	});

	$('#WARRANTYID').click(function(){
		if($(this).prop('checked')){
		$('#WARRANTYSelect').show();
		}else{
		$('#WARRANTYSelect').hide();
		}
	});

	$('#ReFinanceIDCheck').click(function(){
		if($(this).prop('checked')){
			$('#ReFinanceSelect').show();
		}else{
			$('#ReFinanceSelect').hide();
		}
	});

	$('#VASID').click(function(){
		if($(this).prop('checked')){
				$('#VASTagToSelect').show();
		}else{
				$('#VASTagToSelect').hide();
		}
	});

	$('#LoanID').click(function(){
		if($(this).prop('checked')){
				$('#LoanSelect').show();
		}else{
				$('#LoanSelect').hide();
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


	//Inbound Upsell Opportunity--------->
	$('#InsuranceIDCheckIn').click(function(){
		if($(this).prop('checked')){
				$('#InsuranceSelectIn').show();
		}else{
				$('#InsuranceSelectIn').hide();
		}
	});

	$('#WARRANTYIDIn').click(function(){
		if($(this).prop('checked')){
		$('#WARRANTYSelectIn').show();
		}else{
		$('#WARRANTYSelectIn').hide();
		}
	});

	$('#ReFinanceIDCheckIn').click(function(){
		if($(this).prop('checked')){
			$('#ReFinanceSelectIn').show();
		}else{
			$('#ReFinanceSelectIn').hide();
		}
	});

	$('#VASIDIn').click(function(){
		if($(this).prop('checked')){
				$('#VASTagToSelectIn').show();
		}else{
				$('#VASTagToSelectIn').hide();
		}
	});

	$('#LoanIDIn').click(function(){
		if($(this).prop('checked')){
				$('#LoanSelectIn').show();
		}else{
				$('#LoanSelectIn').hide();
		}
	});

	$('#EXCHANGEIDIn').click(function(){
		if($(this).prop('checked')){
				$('#EXCHANGEIDSelectIn').show();
		}else{
				$('#EXCHANGEIDSelectIn').hide();
		}
	});

	$('#UsedCarIDIn').click(function(){
		if($(this).prop('checked')){
				$('#UsedCarSelectIn').show();
		}else{
				$('#UsedCarSelectIn').hide();
		}
	});


	$("input[name$='ServicedAtOtherDealerRadio']").click(function(){
		varServicedAtOtherDealer = $(this).val();
		if(varServicedAtOtherDealer == "Autorized workshop"){
			$('#AutorizedworkshopDIV').show();
			$('#NonAutorizedworkshopDiv').hide();
		}else{
			$('#NonAutorizedworkshopDiv').show();
			$('#AutorizedworkshopDIV').hide();
		}
	});


	$("input[name$='LeadYes']").click(function() {
		   mLeadYes = $(this).val();
			if(mLeadYes=="Capture Lead Yes"){
				$("#LeadDiv").show();
			}else{
				$("#LeadDiv").hide();
			}	
		});
		
		//Inbound Call Upsel opportunity -->
	$("input[name$='LeadYesIn']").click(function() {
		   mLeadYesIn = $(this).val();
			if(mLeadYesIn=="Capture Lead YesIn"){
				$("#LeadDivIn").show();
			}else{
				$("#LeadDivIn").hide();
			}	
		});
		
		//Inbound Complaints----->
	$("input[name$='userfeedbackIn']").click(function() {
		   mfeedbackYesIn = $(this).val();
			if(mfeedbackYesIn=="feedback YesIn"){
				$("#feedbackDIVIn").show();
			}else{
				$("#feedbackDIVIn").hide();
			}	
		});

	$("input[name='listingForm.userfeedback']").click(function() {
		   mfeedbackYes = $(this).val();
			if(mfeedbackYes=="feedback Yes"){
				$("#feedbackDIV").show();
			}else{
				$("#feedbackDIV").hide();
			}	
		});

	$("#NextToModdelDiv").on("click", function () {
			$(this).next("#CustomerDriveInID").focus();
		 });

	$("#NextToFinalDiv").on("click", function () {
			$(this).next("#LeadYesID").focus();
		 });
		 
	$("input[name='listingForm.dispoNotTalk']").click(function() {
		var mNoOthre = $(this).val();
        if (mNoOthre =="NOOthersCheck"){
			$("#NoOthers").show();
		}else{
			$("#NoOthers").hide();
		}	
	});
	$('#LandLineNumberCheck').click(function(){
			if($(this).prop('checked')){
					$('#AddLandLineNumber').show()
				}else{
				$('#AddLandLineNumber').hide()
				}
		});

	$('#MobileNumberCheck').click(function(){
			if($(this).prop('checked'))
				{
					$('#AddMobileNumber').show()
				}
			else
				{
				$('#AddMobileNumber').hide()
				}
		});
	
		///////////////////////////////////////////02/01/2017///////////////////////////////////////
		$('#NextAlreadySerFeedBack').click(function(){
			
			
			var chknoReqUpLead = 0;
				$('[name="listingForm.LeadYesAlradyService"]').each(function (){
					if ($(this).is(':checked')) chknoReqUpLead++;
				});
				if (chknoReqUpLead == 0) {
					Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please check one of these.'
						});
				
				}else{ 
				if($("#LeadYesAlradyServiceIdYes").prop('checked')){
					 var checkeds = $('.notRequiredChk ').is(':checked');
					
					if(checkeds){
						
					}else{
						
						Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please check one of these.'
							
						});
						return false;
					}
				}else if($("#LeadYesAlradyServiceIdNo").prop('checked')){
					$('#AlreadyServiceFeedBAck').show();
				$('#AlreadyServiceUpsellOpp').hide();
				}
				$('#AlreadyServiceFeedBAck').show();
				$('#AlreadyServiceUpsellOpp').hide();
				}
			
		});
		
		$('#BackToAlreadySerUpsel').click(function(){
			$('.whatDidCustSayDiv').show();
			$('#AlreadyServiced').attr("checked",true);
			$('.AlreadyServiced').show();
			$('#AlreadyServiceUpsellOpp').hide();
			$('.alreadyservicedDiv1').show();
		});
		
		
		$('#BackToAlreadyServUpsell').click(function(){
			
			$("input[type=checkbox]"). prop("checked",false);
			$('#AlreadyServiceUpsellOpp').show();
			$('#AlreadyServiceFeedBAck').hide();
		});
		
		$('#NextToLastAlreadySerQuestion').click(function(){
			
			var selectValDropNotReq=$('#selected_department').val();
			
			var selectValRemarksnotReq = $('#commentsNotReq').val();
		
			 var userfeedbackNotReq = 0;
				$('[name="listingForm.userfeedbackAlreadyService"]').each(function (){
					if ($(this).is(':checked')) userfeedbackNotReq++;
				});
				if (userfeedbackNotReq == 0) {
							Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one of theses.'
						});
						return false;
				}else{
					
					if($("#userfeedbackAlreadyService").prop('checked')){
						
					if(selectValDropNotReq ==0 ){
							Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Select Department.'
						});
						return false;
						}
						
						if(selectValRemarksnotReq =='' ){
							Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Remarks Should not empty.'
						});
						return false;
						}
						else{
							$('#LastQuestionAlreadyServ').show();
							$('#AlreadyServiceFeedBAck').hide();
						}						
				}
					$('#LastQuestionAlreadyServ').show();
					$('#AlreadyServiceFeedBAck').hide();
					
				}
			
		});
		$('.BackToCustomerFeedback').click(function(){
			$('#AlreadyServiceFeedBAck').show();
			$('#LastQuestionAlreadyServ').hide();
		});
///////doday//////////////////////////////////////////
		   $("input[name$='LeadYesAlradyService']").click(function() {
			   mLeadYesAlradyService = $(this).val();
				if(mLeadYesAlradyService=="Capture Lead Yes AlreadyService")
				{
					$("#LeadDivAlreadyService").show();
				}
				else
				{
					$("#LeadDivAlreadyService").hide();
				}	
			});
			
		$('#InsuranceIDCheckAlreadyServiced').click(function(){
			if($(this).prop('checked'))
			{
					$('#InsuranceSelectAlreadyService').show();
			}else{
					$('#InsuranceSelectAlreadyService').hide();
			}
		});

		$('#WARRANTYIDAlreadyService').click(function(){
			if($(this).prop('checked'))
			{
			$('#WARRANTYSelectAlreadyService').show();
			}
			else
			{
			$('#WARRANTYSelectAlreadyService').hide();
			}
		});

		$('#ReFinanceIDCheckAlreadyService').click(function(){
			if($(this).prop('checked'))
			{
				$('#ReFinanceSelectAlreadyService').show();
			}
			else
			{
				$('#ReFinanceSelectAlreadyService').hide();
			}
		});

		$('#VASIDAlreadyService').click(function(){
			if($(this).prop('checked'))
			{
					$('#VASTagToSelectAlreadyService').show();
			}
			else
			{
					$('#VASTagToSelectAlreadyService').hide();
			}
		});

		$('#LoanIDAlreadyService').click(function(){
			if($(this).prop('checked'))
			{
					$('#LoanSelectAlreadyService').show();
			}
			else
			{
					$('#LoanSelectAlreadyService').hide();
			}
		});

		$('#EXCHANGEIDAlreadyService').click(function(){
			if($(this).prop('checked'))
			{
					$('#EXCHANGEIDSelectAlreadyService').show();
			}
			else
			{
					$('#EXCHANGEIDSelectAlreadyService').hide();
			}
		});

		$('#UsedCarIDAlreadyService').click(function(){
			if($(this).prop('checked'))
			{
					$('#UsedCarSelectAlreadyService').show();
			}
			else
			{
					$('#UsedCarSelectAlreadyService').hide();
			}
		});
			
			
		$('input[type="radio"]').click(function(){
			if($(this).attr("value")=="feedback Yes AlreadyService"){
		
			   $("#feedbackDIVAlreadyService").show();
			}
			if($(this).attr("value")=="feedback No AlreadyService"){
			
				$("#feedbackDIVAlreadyService").hide();
			}
			
		});
		
		
		$('#BackToAlreadyServiceFeedBack').click(function(){
			
			$('#AlreadyServiceFeedBAck').show()
			$('#LastQuestionAlreadyServ').hide()
			
		});
			
		$('#NextToInBoundLastQuest').click(function(){
		
			var selectValDrop1=$('#selected_department2').val();
			
			var selectRemarks = $('#commentsDSA').val();
		
			 var userfeedbackInbound = 0;
				$('[name="userfeedbackIn"]').each(function (){
					if ($(this).is(':checked')) userfeedbackInbound++;
				});
				if (userfeedbackInbound == 0) {
							Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Choose one of these.'
						});
						return false;
				}else{
					
					if($("#feedbackYesIn").prop('checked')){
						
					if(selectValDrop1 ==0 ){
							Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Select Department.'
						});
						return false;
						}
						
						if(selectRemarks =='' ){
							Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Remarks Shouldnot empty.'
						});
						return false;
						}
						else{
					$('#LastQuestionInbound').show();
					$('#CustFeedBackIn').hide();
					}						
					}
					$('#LastQuestionInbound').show();
					$('#CustFeedBackIn').hide();
					
				}
		});
		
		
		$('#ToInboindComplaintSubmit').click(function(){
				var radioBtnInboundLastQ = 0;
				$('[name="radio7"]').each(function (){
			if ($(this).is(':checked')) radioBtnInboundLastQ++;
			});	
			if(radioBtnInboundLastQ == 0 ) {
			Lobibox.notify('warning', {
			continueDelayOnInactiveTab: true,
			msg: 'Please Choose.'
			});
			return false;
			}else{
			  $.blockUI(); 
			
			}
		});
		
		
		$('#BackToToInboindComplaint').click(function(){
			
			$('#CustFeedBackIn').show()
			$('#LastQuestionInbound').hide()
			
		});

			

	   //sashi ends//

	//subhendu offer function
			  $('#applyOffers').click(function(){
				  
				  var offerid=$("input[name='case[]']:checked").val();
				 
				  
			 var values = new Array();
				   $.each($("input[name='case[]']:checked").closest("td").siblings("td"),
						  function () {
							   values.push($(this).text());
						  });	
							$('#selectChkVal').html(values.join (", "));
							
							
							 $('.saveOffer').click(function(){
								 
								 $('#offerSelectedId').val(offerid);
									
								});	
							
			 });
			 
			 $('.removeOffer').click(function(){
				$("input[name='case[]']:checked").removeAttr("checked");
				$('#selectChkVal').text(' ');
			});
			 
					 
			 
			 
	
	$('.abc').on('click',function(){
		if($(this).hasClass('open')){
		$('.abc').animate({right:'-600px'},  300).removeClass('open');
		//$(this).animate({right:'-3px'},  300).addClass('open');
		}else{
		$('.abc').animate({right:'-600px'},  300).removeClass('open');
		$(this).animate({right:'-3px'},  300).addClass('open');
		}
	});

	$('#example700').on('click',function(e){
		e.stopPropagation();
	});
	
	$('#example7889').on('click',function(e){
		e.stopPropagation();
	});
	
	 $('input[type="radio"]').click(function(){
		var x = $(this).val();
		$('#submitDndBtn').on("click",function(){
		if(x === "makeDND"){
		console.log("clicked maske dnd");
			   $("#btnDnd").addClass("btn btn-danger");
			}
			if(x ==="removeDND"){
		   
			console.log("clicked remove dnd");	
				$("#btnDnd").removeClass("btn btn-danger");
				$("#btnDnd").addClass("btn btn-success");
			}
		});
			
	});


	
	//validation//
	
	$('#callMeLaterSubmit').click(function(){
			var dateVal=$('#FollowUpDate').val();
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
		
		
		 $(".oeditAddressmodal").click(function() {
			 if($('.addrPin').val() == '()'){
				 $('.addrPin').val('');
			 }
		 });
		
		
 $(".addrPin").keydown(function (e) {
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||(e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) || (e.keyCode >= 35 && e.keyCode <= 40)) {
                 return;
        }
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
	$('.peditAddressmodal').click(function(){
		if($('.paddr_line6').val()=='0' || $('.paddr_line6').val()=='()'){
			$('.paddr_line6').val('');
		}
	});
	$('.reditAddressmodal').click(function(){
		if($('.raddr_line6').val()=='0' || $('.raddr_line6').val()=='()'){
			$('.raddr_line6').val('');
		}
	});
	$('.oeditAddressmodal').click(function(){
		if($('.paddr_line5').val()=='0' || $('.paddr_line5').val()=='()'){
			$('.paddr_line5').val('');
		}
	});
	
	var dateToday = new Date();
var dates = $("#date12345").datepicker({
    //defaultDate: "+1w",
dateFormat: 'yy-mm-dd',
maxDate: "+30d",
    minDate:0,
    onSelect: function(selectedDate) {
        var option = this.id == "date12345" ? "minDate" : "maxDate",
            instance = $(this).data("datepicker"),
            date = $.datepicker.parseDate(instance.settings.dateFormat || $.datepicker._defaults.dateFormat, selectedDate, instance.settings);
        dates.not(this).datepicker("option", option, date);
    }
});
var dateTodayext = new Date();
var dates = $("#date123456").datepicker({
    //defaultDate: "+1w",
dateFormat: 'yy-mm-dd',
    minDate:0,
    onSelect: function(selectedDate) {
        var option = this.id == "date123456" ? "minDate" : "maxDate",
            instance = $(this).data("datepicker"),
            date = $.datepicker.parseDate(instance.settings.dateFormat || $.datepicker._defaults.dateFormat, selectedDate, instance.settings);
     //dates.not(this).datepicker("option", option, date);
    }
});	
var dateToday1 = new Date();
var dates1 = $("#FollowUpDate").datepicker({
    //defaultDate: "+1w",
dateFormat: 'yy-mm-dd',
maxDate: "+30d",
    minDate:0,
    onSelect: function(selectedDate) {
        var option = this.id == "FollowUpDate" ? "minDate" : "maxDate",
            instance = $(this).data("datepicker"),
            date = $.datepicker.parseDate(instance.settings.dateFormat || $.datepicker._defaults.dateFormat, selectedDate, instance.settings);
       // dates.not(this).datepicker("option", option, date);
    }
});
	
	
	
	
	
	
        
        ///validation for book my service
        
        $('#bookMyserviceSubmit').click(function(){
		 var chkincSubmit = 0;
            $('[name="listingForm.CustomerFeedBackYes"]').each(function (){
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
	$('#nonContactValidation').click(function(){
		 var chkincSubmit = 0;
            $('[name="listingForm.dispoNotTalk"]').each(function (){
					if ($(this).is(':checked')) chkincSubmit++;
				});
				if (chkincSubmit == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please check one.'
						});
						return false;
						
					
				} else{
					if($("#NOOthersCheck").prop('checked')){
						 
						var textNoOthers = $('.NoOthersText1').val();
						if(textNoOthers ==''){
							Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Remarks Shouldnot Empty.'
						});
						return false;
							
						}
					}
                                    
                        $.blockUI(); 
                                    
                                }
	}); 
        
	
			//added function 22/12/2016
			$('#inBoundLeadSourceSelectVal,#serviceTypeAltIdDataVal').on('change', function() {
                     if($('#inBoundLeadSourceSelectVal').val() != 0 && $('#serviceTypeAltIdDataVal').val() != 0 ) {
			  $('#booksMyServiceDisplayBlock').css('display','block');
			} 
			else {
			   $('#booksMyServiceDisplayBlock').css('display','none');
			}
			});
			
			  $('#alreadyServiced').click(function(){
					var chknoReqSubmit = 0;
				$('[name="radio7"]').each(function (){
					if ($(this).is(':checked')) chknoReqSubmit++;
				});
				if (chknoReqSubmit == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please choose one.'
						});
						return false;
					
				} else{
                                    
                                    $.blockUI(); 
                                    
                                }
	});
	 $('#vehicleSoldStolen').click(function(){
					var chknoReqOthers = 0;
				$('[name="radio8"]').each(function (){
					if ($(this).is(':checked')) chknoReqOthers++;
				});
				if (chknoReqOthers == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please choose one.'
						});
						return false;
					
				} else{
                                    
                                    $.blockUI(); 
                                    
                                }
	});
	$('#dissatifiedwithService').click(function(){
					var chknoReqInsu = 0;
				$('[name="radio6"]').each(function (){
					if ($(this).is(':checked')) chknoReqInsu++;
				});
				if (chknoReqInsu == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please choose one.'
						});
						return false;
					
				} else{
                                    
                                  $.blockUI(); 
                                    
                                }
	});
	$('#dissatifiedwithPService').click(function(){
					var chknoReqsales = 0;
				$('[name="radio5"]').each(function (){
					if ($(this).is(':checked')) chknoReqsales++;
				});
				if (chknoReqsales == 0) {
				Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please choose one.'
						});
						return false;
					
				} else{
                                    
                     $.blockUI(); 
                                    
                                }
	});
	
	//shashi added on 4th jan 17
	//Incomming Call Back to next---------------------------------------->
				$("#NextToLeadInbound").click(function(){
					 var inboundInc = 0;
				$('[name="typeOfPickupIn"]').each(function (){
					if ($(this).is(':checked')) inboundInc++;
				});
				if (inboundInc == 0) {
							Lobibox.notify('warning', {
							continueDelayOnInactiveTab: true,
							msg: 'Please Select one of these.'
						});
				}else{
					$("#finalDiv1Inbound").show();
					$("#CustomerDriveInDivIn").hide();
				}
				
				var upselFlag=$('#selectedUpselOpp').val();	
				
				alert(upselFlag);
				
				if(upselFlag == "0" || upselFlag == null || upselFlag == "()"){
					$( "#LeadNoIDIn" ).prop( "checked", true );
				}else{
					$( "#LeadYesIDIn" ).prop( "checked", true );
					$("#LeadDivIn").show();
				}
				loadLeadBasedOnLocation();
				var sr_int_id=$('#srdispo_id').val();
				//alert("sr_int_id : "+sr_int_id);		
				
				var urlDisposition = "/CRE/upsellSelectedLastSB/"+sr_int_id+"";
				$.ajax({
					
			        url: urlDisposition

			    }).done(function (upselData) {
			    	
			        console.log(upselData);
			        
			        for (var i = 0; i < upselData.length; i++) {
			        	
			       if(upselData[i].upSellType == "Insurance"){ 
			        	
			        $('#InsuranceIDCheckIn').prop('checked',true);			    		
			    	$('#InsuranceSelectIn').show();
			    	$('#comments8').val(upselData[i].upsellComments);
			    		
			    
			       }else if(upselData[i].upSellType == "Warranty / EW"){
			    	   $('#WARRANTYIDIn').prop('checked',true);			    		
				    	$('#WARRANTYSelectIn').show();
				    	$('#comments9').val(upselData[i].upsellComments);
			    	
			       }else if(upselData[i].upSellType == "Re-Finance / New Car Finance"){

			    	   $('#ReFinanceIDCheckIn').prop('checked',true);			    		
				    	$('#ReFinanceSelectIn').show();
				    	$('#comments11').val(upselData[i].upsellComments);
				    	
			    	
			       }else if(upselData[i].upSellType == "VAS"){

			    	   $('#VASIDIn').prop('checked',true);			    		
				    	$('#VASTagToSelectIn').show();
				    	$('#comments10').val(upselData[i].upsellComments);
			    	
			       }else if(upselData[i].upSellType == "Sell Old Car"){

			    	   $('#LoanIDIn').prop('checked',true);				    		
				    	$('#LoanSelectIn').show();
				    	$('#comments12').val(upselData[i].upsellComments);
			    	
			        }else if(upselData[i].upSellType == "Buy New Car / Exchange"){
			        
			        	 $('#EXCHANGEIDIn').prop('checked',true);			    		
					    	$('#EXCHANGEIDSelectIn').show();
					    	$('#comments13').val(upselData[i].upsellComments);
			    	
			        }else if(upselData[i].upSellType == "UsedCar"){

			        	 $('#UsedCarIDIn').prop('checked',true);				    		
					    	$('#UsedCarSelectIn').show();
					    	$('#comments14').val(upselData[i].upsellComments);
					    	
			    	
			  	  
			        }
			        }
			    });
				
				
					
				});
				
				
	$('#example7889').DataTable( {
        "paging":   false,
        "ordering": false,
        "info":     false,
		 "searching": false
    });
	
	 $('#example700').DataTable( {
        "paging":   false,
        "ordering": false,
        "info":     false,
		 "searching": false
    });
	 $('#example800').DataTable( {
        "paging":   false,
        "ordering": false,
        "info":     false,
		 "searching": false
    });
	

//Added by Shashidhar 25 jan 2017 
$(".onlyAlphabetOnly").keypress(function(a) {
        var b = a.charCode;
        b >= 65 && b <= 120 || 32 == b || 0 == b || a.preventDefault()
    }), $("#LeadNoID").click(function() {
        $("#InsuranceIDCheck").attr("checked", !1),
		$("#WARRANTYID").attr("checked", !1),
		$("#VASID").attr("checked", !1), 
		$("#ReFinanceIDCheck").attr("checked", !1),
		$("#LoanID").attr("checked", !1),
		$("#EXCHANGEID").attr("checked", !1), 
		$("#UsedCarID").attr("checked", !1),
		$("#InsuranceSelect").hide(), 
		$("#WARRANTYSelect").hide(), 
		$("#VASTagToSelect").hide(),
		$("#ReFinanceSelect").hide(),
		$("#LoanSelect").hide(), 
		$("#EXCHANGEIDSelect").hide(),
		$("#UsedCarSelect").hide(),
		$("#sellOldCarLead1").val('0'),
		$("#commentsSellOld").val('')
		
    }), $("#LeadNoIDIn").click(function() {
        $("#InsuranceIDCheckIn").attr("checked", !1), $("#WARRANTYIDIn").attr("checked", !1), $("#VASIDIn").attr("checked", !1), $("#ReFinanceIDCheckIn").attr("checked", !1), $("#LoanIDIn").attr("checked", !1), $("#EXCHANGEIDIn").attr("checked", !1), $("#UsedCarIDIn").attr("checked", !1), $("#InsuranceSelectIn").hide(), $("#WARRANTYSelectIn").hide(), $("#VASTagToSelectIn").hide(), $("#ReFinanceSelectIn").hide(), $("#LoanSelectIn").hide(), $("#EXCHANGEIDSelectIn").hide(), $("#UsedCarSelectIn").hide()
    }), $("#CustomerDriveInID").click(function() {
        $("#time_FromDriver").val(""), $("#time_ToDriver").val(""), $("#driverIdSelect").val("0")
		
    }), $("#BookMyService").click(function() {
    	
    	var servBookExist=$('#servicebook_id').val();
    	
    	//alert(servBookExist);
    	if(servBookExist!="()"){
    	Lobibox.confirm({ msg: "This service is already booked do you want to modify?",
            callback: function ($this, type) {
                if (type === 'yes') {
                	
                	 $("#FollowUpDate").val(""), $("#FollowUpTime").val(""), $("#AlreadyServiced").attr("checked", !1), $("#VehicleSold").attr("checked", !1), $("#Dissatisfiedwithpreviousservice").attr("checked", !1), $("#Distancefrom").attr("checked", !1), $("#DissatisfiedwithSalesID").attr("checked", !1), $("#DissatisfiedwithInsuranceId").attr("checked", !1), $("#Stolen").attr("checked", !1), $("#Totalloss").attr("checked", !1),
             		$("#OtherReason").attr("checked", !1),
             		$("#alreadyservicedDiv1").hide(),
             		$("#VehicelSoldYesRNo").hide(),
             		$("#txtDissatisfiedwithpreviousservice").hide(),
             		$("#DistancefromDealerLocationDIV").hide(),
             		$("#DissatisfactionwithSalesREmarksDiv").hide(),
             		$("#DissatisfactionwithInsuranceREmarksDiv").hide(),
             		$("#OtherSeriveRemarks").hide(),
             		$("#stolenHideShowSubmit").hide(),
             		$("#stolenDamageSubmit").hide()
                   
                } else if (type === 'no') {
                	$("#SMRInteractionFirst").show();
					$("#DidYouTalkDiv").show();
					$("#whatDidCustSayDiv").hide();
					$("#serviceBookDiv").hide();
					$('#SpeakYes').attr('checked',false);
					$('#SpeakNo').attr('checked',false);
                	
                }
           
            }
            });
    	
    	}else{
    		$("#FollowUpDate").val(""), $("#FollowUpTime").val(""), $("#AlreadyServiced").attr("checked", !1), $("#VehicleSold").attr("checked", !1), $("#Dissatisfiedwithpreviousservice").attr("checked", !1), $("#Distancefrom").attr("checked", !1), $("#DissatisfiedwithSalesID").attr("checked", !1), $("#DissatisfiedwithInsuranceId").attr("checked", !1), $("#Stolen").attr("checked", !1), $("#Totalloss").attr("checked", !1),
     		$("#OtherReason").attr("checked", !1),
     		$("#alreadyservicedDiv1").hide(),
     		$("#VehicelSoldYesRNo").hide(),
     		$("#txtDissatisfiedwithpreviousservice").hide(),
     		$("#DistancefromDealerLocationDIV").hide(),
     		$("#DissatisfactionwithSalesREmarksDiv").hide(),
     		$("#DissatisfactionwithInsuranceREmarksDiv").hide(),
     		$("#OtherSeriveRemarks").hide(),
     		$("#stolenHideShowSubmit").hide(),
     		$("#stolenDamageSubmit").hide()
    	}
    	
		
		
		
		
    }), $("#CallMeLatter").click(function() {
        $("#serviceBookedTypeSelect").val("0"),
		$("#workshop").val("0"),
		$("#date12345").val(""),
		$("#CommittedTimes").val(""),
		$("#serviceAdvisor").val("0"),
		$("#AlreadyServiced").attr("checked", !1), 
		$("#VehicleSold").attr("checked", !1),
		$("#Dissatisfiedwithpreviousservice").attr("checked", !1),
		$("#Distancefrom").attr("checked", !1),
		$("#DissatisfiedwithSalesID").attr("checked", !1),
		$("#DissatisfiedwithInsuranceId").attr("checked", !1),
		$("#Stolen").attr("checked", !1),
		$("#Totalloss").attr("checked", !1), 
		$("input:radio[name='typeOfPickup']").each(function(){
			this.checked=false;			
		}),
		$("input:radio[name='LeadYes']").each(function(){
			this.checked=false;
		}),
		$("input:radio[name='userfeedback']").each(function(){
			this.checked=false;
		}),
		$("input:radio[name='CustomerFeedBackYes']").each(function(){
			this.checked=false;
			
		}),
		$("#LeadDiv").hide(),
		
			$("input[type=checkbox]"). prop("checked",false);
				$("#InsuranceSelect").hide(),
				 $("#WARRANTYSelect").hide(),
				 $("#VASTagToSelect").hide(),
				 $("#ReFinanceSelect").hide(),
				 $("#LoanSelect").hide(),
				 $("#EXCHANGEIDSelect").hide(),
				 $("#UsedCarSelect").hide(),
				 		$("#alreadyservicedDiv1").hide(),
		$("#VehicelSoldYesRNo").hide(),
		$("#txtDissatisfiedwithpreviousservice").hide(),
		$("#DistancefromDealerLocationDIV").hide(),
		$("#DissatisfactionwithSalesREmarksDiv").hide(),
		$("#DissatisfactionwithInsuranceREmarksDiv").hide(),
		$("#OtherSeriveRemarks").hide(),
		$("#stolenHideShowSubmit").hide(),
		$("#stolenDamageSubmit").hide(),
		$("#OtherReason").attr("checked", !1)
    }),

	$("#ServiceNotRequired").click(function() {
        $("#serviceBookedTypeSelect").val("0"), 
		$("#workshop").val("0"),
		$("#date12345").val(""),
		$("#CommittedTimes").val(""),
		$("#serviceAdvisor").val("0"),
		$("#FollowUpDate").val(""),
		$("#FollowUpTime").val(""),
		$(".AlreadyServiced").show(),
		$(".VehicleSold").show(), 
		$(".Dissatisfiedwithpreviousservice").show(),
		$(".Distancefrom").show(),
		$(".DissatisfiedwithSalesID").show(), 
		$(".DissatisfiedwithInsuranceId").show(),
		$(".Stolen").show(),
		$(".Totalloss").show(), 
			$("input:radio[name='typeOfPickup']").each(function(){
			this.checked=false;			
		}),
		$("input:radio[name='LeadYes']").each(function(){
			this.checked=false;
		}),
		$("input:radio[name='userfeedback']").each(function(){
			this.checked=false;
		}),
		$("input:radio[name='CustomerFeedBackYes']").each(function(){
			this.checked=false;
			
		}),
		$("#LeadDiv").hide(),
		
			$("input[type=checkbox]"). prop("checked",false);
				$("#InsuranceSelect").hide(),
				 $("#WARRANTYSelect").hide(),
				 $("#VASTagToSelect").hide(),
				 $("#ReFinanceSelect").hide(),
				 $("#LoanSelect").hide(),
				 $("#EXCHANGEIDSelect").hide(),
				 $("#UsedCarSelect").hide(),
		$(".OtherLast").show()
    })
	
$("#AddAddrMMSSave").click(function(){
	var Addr1=$("#AddAddress1MMS").val();
	var Addr2=$("#AddAddress2MMS").val();
	var CityMMS=$("#cityInputPopup3").val();
	var StateMMS=$("#AddAddrMMSState").val();
	var PinMMS=$("#PinCode1MMS").val();
	
	var ResultMMS = Addr1.concat(Addr2+" , "+CityMMS+" , "+StateMMS+" , "+PinMMS);

$("#AddrMMSPOPUP").prepend($("<option></option>").html(ResultMMS));

});


$("#PickUpAddrSave1").click(function(){
var PicUpAddr1= $("#AddAddrPicUp1").val();
var PicUpAddr2= $("#AddAddrPicUp2").val();
var PicUpcity3= $("#cityAddr1").val();
var PicUpSelect4= $("#selectAddreState1").val();
var PicUppin5= $("#addrPinPicup1").val();

var ResultPickup=PicUpAddr1.concat(PicUpAddr2+" , "+PicUpcity3+" , "+PicUpSelect4+" , "+PicUppin5);

$("#pickUpAddPOPUP1").prepend($("<option></option>").html(ResultPickup));

				
});


    
    //Add phone by subendhu
				
    $('.numberOnly').keypress(function (e) {
		
        if (isNaN(this.value + "" + "." + String.fromCharCode(e.charCode))) {
					return false;
        }

    });
	
	$(".textOnlyAccepted").keypress(function (e) {
            var code = e.keyCode || e.which;
            if ((code < 65 || code > 90) && (code < 97 || code > 122) && code != 32 && code != 46)
            {
                 return false;
            }
        });
    
    
    
    $("#savePhoneCust1").on('click', function(){
    	
    	//alert("ad new phone");
    	 var lastValue = parseFloat($('#preffered_contact_num').val()) + 1;
    	 var conLenthSave= $('#preffered_contact_num').children('option').length;
      	if(conLenthSave >=1){
      	 $("#deleteNum").show()
      	 }
    	 
    	 var remarks = $("#myPhoneRemark1").val();
    	// alert("savePhoneCust : " +remarks.length);
    	  var custMobNo =$("#myPhoneNum").val();
    		if(custMobNo.length <10){
    			
    			Lobibox.notify('warning', {
    								continueDelayOnInactiveTab: true,
    								msg: 'Mobile Number Is Invalid.'
    							});
    							return false;
    			
    		} 
    		if(remarks.length <5){
    			
    			Lobibox.notify('warning', {
    								continueDelayOnInactiveTab: true,
    								msg: 'Please Enter the Remark.'
    							});
    			var conLenth= $('#preffered_contact_num').children('option').length;
    			if(conLenth ==1){
    			 $("#deleteNum").hide();
    			}
    							return false;
    			
    		}
    		
    	    var wyzUser_id = document.getElementById('wyzUser_Id').value;
    	    var customer_Id = document.getElementById('customer_Id').value;
    	    var urlDisposition = "/CRE/saveaddcustomermobno/" + custMobNo + "/" + wyzUser_id + "/" + customer_Id +"";
    		
    	    $.ajax({
    	    	'type': 'POST',
		        'url': urlDisposition,
		        'data': {    		        	
		        	remarks:''+remarks,
		        }

    	    }).done(function (data) {
    	    	//alert(data);
    	    	if(data!=0){
    	     $('#myPhoneRemark1').val('');
    	     $('#ddl_phone_no').val(custMobNo);
    	     $('#pref_number_callini').val(custMobNo);
    	     
    	     
    	     $("#preffered_contact_num").prepend($("<option></option>").text(lastValue).html(custMobNo).val(data));
             
	    	 $("#preffered_contact_num option:first").prop('selected', true);
    	    	}else{
    	    		
    	    		Lobibox.notify('warning', {
						continueDelayOnInactiveTab: true,
						msg: 'Mobile Number Is Aready Exisiting.'
					});
    	    	}
    	     
    	    });

    	});
	///////////////////Shashi 25th march 2017//////////////////////
		$( ".datepicMYDropDown" ).datepicker({
      changeMonth: true,
      changeYear: true,
	  maxDate: '0',
	  dateFormat: 'yy-mm-dd',
	  yearRange: "-100:+0"
	  
    });
	
	$( ".datepickerPlus30Days" ).datepicker({
       maxDate: "+30d",
       dateFormat: 'yy-mm-dd',
	   minDate:0
    });
	
	/* $( "#date12345" ).datepicker({
       maxDate: "+30d",
	   minDate:0
    }); */
	
	

	$("#AddAddrMMSSave").click(function(){
		var vAddr1= $("#AddAddress1MMS").val();
		var vAddr2= $("#AddAddress2MMS").val();
		var vState4= $("#AddAddrMMSState").val();
		var vcity3= $("#cityInputPopup3").val();
		var vpin5= $("#PinCode1MMS").val();

		var ResultAll=vAddr1+" , "+vAddr2+" , "+vcity3+" , "+vState4+" , "+vpin5;

		$(".AddressMSSId").prepend($("<option></option>").html(ResultAll));
		  	 $(".AddressMSSId option:first").prop('selected', true);

		});
//time picker

            //dates
		
});	
	



    function tableText(tableCell) {
	//alert('clicked two inside'+tableCell.id);
	vtempValue = parseInt(document.getElementById("tempValue").value);
	
	
				if(vtempValue==tableCell.cellIndex || vtempValue==0){
			vtempValue= tableCell.cellIndex//    table.rows[0].cells[j].innerText;
			
	}
	else{return;}
	console.log("tablecel id : "+tableCell.id);
	var className = tableCell.className;
	
	if(className==""){
	//alert('clicked 4');
		 $.confirm({
                                        title: 'Confirm!',
										closeIcon: true,
                                        content: 'Do you want Assign' +"<br>"+ tableCell.id,
                                        buttons: {
                                            Yes: function () {
											vstartValue = parseInt(document.getElementById("startValue").value);
											vendValue = parseInt(document.getElementById("endValue").value);
											
												
												if(vstartValue===0){
												vstartValue=tableCell.parentElement.rowIndex;
												document.getElementById("startValue").value=tableCell.parentElement.rowIndex;
												}
												else if(vendValue===0){
													vendValue=tableCell.parentElement.rowIndex;	
													document.getElementById("endValue").value=vendValue;
															
												}
                                              
											   $(tableCell).addClass('ColorGreen');
											   if(vendValue === 0){
											   vendValue = tableCell.parentElement.rowIndex;
											   }
											   if(vstartValue!=0 && vendValue!=0){
													if(vstartValue < tableCell.parentElement.rowIndex){
															if(vendValue < tableCell.parentElement.rowIndex){
																vendValue =tableCell.parentElement.rowIndex;
															}
													}else{
														vstartValue =tableCell.parentElement.rowIndex;
													}
													if(vstartValue > vendValue){
														minValue=vendValue;
														maxValue=vstartValue;
													}else{
														minValue=vstartValue;
														maxValue=vendValue;
													}
													
													 var table = document.getElementById("tableID");
													 var tableCellheader=	table.rows[0].cells[tableCell.cellIndex];
													
													 //console.log("tableCellheader : "+tableCellheader.id);
													 //console.log("tableCellheader innerText: "+tableCellheader.innerText);

													 document.getElementById("driverValue").value=tableCellheader.id;
													 $('#newDriver').html(tableCellheader.innerText);
													 
													 console.log("min and max value : "+minValue,maxValue);
														
														for (var i = minValue; i < maxValue; i++) {
														var tableCell1=	table.rows[i].cells[tableCell.cellIndex];
														$(tableCell1).addClass('ColorGreen');
														}
																
																document.getElementById("startValue").value=minValue;
																document.getElementById("endValue").value=maxValue;
																minValue='';
																maxValue='';
														
											   }
									
											   
											   document.getElementById("tempIncreValue").value=$('#tableID .ColorGreen').length;
											   document.getElementById("tempValue").value=tableCell.cellIndex;
											   if ($('#tableID .ColorGreen').length === 0){
												document.getElementById("startValue").value=0;
												document.getElementById("endValue").value=0;
												document.getElementById("tempValue").value=0;
											}
											   
                                            },
                                            No: function () {
											 $(tableCell).removeClass('ColorGreen');
	                                         }
                                        }
                                    });
                              }
							  else
							  {
								 $.confirm({
                                        title: 'Confirm!',
										closeIcon: true,
                                        content: 'Do you want UnAssign'  +"<br>"+ tableCell.id,
                                        buttons: {
                                            Yes: function () {
                                                
											vstartValue = parseInt(document.getElementById("startValue").value);
											vendValue = parseInt(document.getElementById("endValue").value);
											var table1 = document.getElementById('tableID');
											$(tableCell).removeClass('ColorGreen');
											if(vstartValue !== tableCell.parentElement.rowIndex && vendValue === tableCell.parentElement.rowIndex){
											for	(i=vendValue-1;i>=vstartValue;i--){
											var tableCell2=	table1.rows[i].cells[tableCell.cellIndex];
												if($(tableCell2).hasClass('ColorGreen')){
													vendValue= i;
													document.getElementById("endValue").value=i;
													break;
												}
												continue;
											}
											}else if(vstartValue === tableCell.parentElement.rowIndex && vendValue !== tableCell.parentElement.rowIndex ){
												for	(i=vstartValue+1;i<=vendValue;i++){
												var tableCell2=	table1.rows[i].cells[tableCell.cellIndex];
												if($(tableCell2).hasClass('ColorGreen')){
													vstartValue= i;
													document.getElementById("startValue").value=i;
													break;
												}
												continue;
											}
											}else if(vstartValue === tableCell.parentElement.rowIndex && vendValue === tableCell.parentElement.rowIndex){
												document.getElementById("startValue").value=0;
												document.getElementById("endValue").value=0;
												document.getElementById("tempValue").value=0;
											}
											
										
											   document.getElementById("tempIncreValue").value=$('#tableID .ColorGreen').length;
											   document.getElementById("tempValue").value=tableCell.cellIndex;
											   if ($('#tableID .ColorGreen').length === 0){
												document.getElementById("startValue").value=0;
												document.getElementById("endValue").value=0;
												document.getElementById("tempValue").value=0;
											}
											   
                                            },
                                            No: function () {
											 $(tableCell).addClass('ColorGreen');
                                            
                                            }
                                        }
                                    });
							  }
							  
							  
		
		
    }
	
	
	function AssignBtnBkreviewScript(){
		//alert("AssignBtnBkreview");
			$.blockUI();
			//var newdates = $('#changeserviceBookingDate').val();
		var scheduleDate = document.getElementById('changeserviceBookingDate').value;
		var workshopId = document.getElementById('workshop').value;
		 
		var urlPath = "/driverListSchedule/"+workshopId+"/"+scheduleDate;
	    $.ajax({
	        url: urlPath
	    }).done(function (dataDriver) {
	        var data = dataDriver.driverListData;
	        var timeslot = dataDriver.timeSlotData;

			$('#tableID').empty();
	        	
	       	var table = document.getElementById("tableID");
	       	var header = table.createTHead();
        	var row = header.insertRow(0);	

		            	        	       
        	for (var i = data.length-1; i >= 0; i--) {	        	
	        	var cell = row.insertCell(0);		        			        	
			    cell.outerHTML  = "<th id="+data[i].id+">"+data[i].userName+"</th>";
	        		        }

        	var cell = row.insertCell(0);		        			        	
		    cell.outerHTML  = "<th>Time slot</th>";

		    for (var j = 0; j < timeslot.length; j++) {	
		    tr = $('<tr/>');
            tr.append('<td id='+timeslot[0].timeRange+' At '+timeslot[j].timeRange+'>'+timeslot[j].timeRange+'</td>');
            for(var k = 0; k < data.length; k++){
			   
               var blockedTime= data[k].listTime;
               var result=inArray(timeslot[j].startTime,blockedTime)
               //console.log("Time is result: "+result);  
           if(result){     
            tr.append('<td id='+timeslot[j].timeRange+' class="ColorRed"></td>');
           }else{

        	   tr.append('<td id='+timeslot[j].timeRange+'></td>');
               }
            
            }       
			
            $('#tableID').append(tr);
			 $.fn.dataTable.ext.errMode = 'none';
			  $($.fn.dataTable.tables(true)).DataTable()
     .columns.adjust();
		$('#tableID').dataTable({

 "fixedHeader": true,
    "scrollX": true,
	"scrollY": 200,
	
       "paging": false,
    "searching":false,
    "ordering":false,
       "bInfo" : false
});	
            		      
		    }

		    var firsftSelect;
		      if (table != null) {
		          for (var i = 1; i < table.rows.length; i++) {
		              for (var j = 1; j < table.rows[i].cells.length; j++){
		  			//table.rows[i].cells[j].id= table.rows[0].cells[j].innerText +" At " + table.rows[i].cells[0].innerText;
		  			//console.log("class name : "+table.rows[i].cells[j].className )
		  			var blockedclass=table.rows[i].cells[j].className;	

		  			if(blockedclass == "ColorRed"){}else{		  				  			
		  			table.rows[i].cells[j].onclick = function () {
		  			//alert('clicked one ');
		  				tableText(this);
		  				
		              
		  			};
		              }
		              
		  			}
		          }
		      }
		      $.unblockUI();
	   
	    });

		}
	

	$('#allcateSubmit').on('click',function(){

		  $("#SubmitBtnBkreview").show();
		});

	function inArray(needle,haystack)
	{
	    var count=haystack.length;
	    for(var i=0;i<count;i++)
	    {
	        if(haystack[i]===needle){return true;}
	    }
	    return false;
	}