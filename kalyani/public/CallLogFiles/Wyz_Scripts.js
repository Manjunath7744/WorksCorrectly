$(window, document, undefined)
		.ready(
				function() {

					
					$('input').blur(function() {
						var $this = $(this);
						if ($this.val())
							$this.addClass('used');
						else
							$this.removeClass('used');
					});

					var $ripples = $('.ripples');

					$ripples.on('click.Ripples', function(e) {

						var $this = $(this);
						var $offset = $this.parent().offset();
						var $circle = $this.find('.ripplesCircle');

						var x = e.pageX - $offset.left;
						var y = e.pageY - $offset.top;

						$circle.css({
							top : y + 'px',
							left : x + 'px'
						});

						$this.addClass('is-active');

					});

					$ripples
							.on(
									'animationend webkitAnimationEnd mozAnimationEnd oanimationend MSAnimationEnd',
									function(e) {
										$(this).removeClass('is-active');
									});
					
					
					

				});


//Clear browser

function ClearHistory()
{
     var backlen = history.length;
     history.go(-backlen);
     window.location.href = loggedOutPageUrl
}

function displayTimeandDate(){
	var follow=document.getElementById('status_feedBack');
	var selected=follow.options[follow.selectedIndex].value;
	
	
	if(follow.options[follow.selectedIndex].value === "Follow Up Required"){
	document.getElementById("followUp").style.display = "block";
	document.getElementById("callback").style.display = "none";
	}else if(follow.options[follow.selectedIndex].value === "CallBack later"){
		document.getElementById("followUp").style.display = "none";
		document.getElementById("callback").style.display = "block";
	}else{
		document.getElementById("followUp").style.display = "none";
		document.getElementById("callback").style.display = "none";
		
	}
	
	
}
//Dropdown select according to cre and cre Manger

function callStatusTypeSelection1(){
	
	
	
	var calltypeSelect=document.getElementById('callstatus_feedback');
	
	if(calltypeSelect.options[calltypeSelect.selectedIndex].value==="Contact"){
		document.getElementById("status_feedBack2").style.display = "block";
		document.getElementById("status_feedBack1").style.display = "none";		
	}else{
		document.getElementById('status_feedBack2').style.display = "none";
		document.getElementById('status_feedBack1').style.display = "block";
		document.getElementById("followUp").style.display = "none";
		document.getElementById("callback").style.display = "none";
	}
}

function dropdownChage() {
	var roleselect = document.getElementById('role');

	if (roleselect.options[roleselect.selectedIndex].value === "CRE") {
		document.getElementById('creManagerId').style.display = "block";
		document.getElementById('dealerCodeId').style.display = "none";
		document.getElementById('salesManagerId').style.display = "none";
	} else if (roleselect.options[roleselect.selectedIndex].value === "CREManager"){
		document.getElementById('creManagerId').style.display = "none";
		document.getElementById('dealerCodeId').style.display = "block";
		document.getElementById('salesManagerId').style.display = "none";
	}else if (roleselect.options[roleselect.selectedIndex].value === "SalesExecutive"){
		document.getElementById('creManagerId').style.display = "none";
		document.getElementById('dealerCodeId').style.display = "none";
		document.getElementById('salesManagerId').style.display = "block";
	}
	
	
	else {		
		document.getElementById('creManagerId').style.display = "none";
		document.getElementById('dealerCodeId').style.display = "block";
		document.getElementById('salesManagerId').style.display = "none";
		
		
	}
}

//validation of duplicate entry of Username

function validatingDuplicateUserName(){

	var userData =document.forms["formSub"]["userName"].value;
	var ddlArray= new Array();
	var ddl = document.getElementById("checkUserName");
	
	for (i = 0; i < ddl.options.length; i++) {
	   ddlArray[i] = ddl .options[i].value;
	   if(userData.toLowerCase()==ddlArray[i].toLowerCase()){

			alert("the Username is already existing,Please modify it");
			return false
		   }
	}
		
	return true;		
}

function callDelete(id,dealercode){

	var urlDisposition="/"+dealercode+"/CRE/deleteCall/"+id+"";
	var urlString="/"+dealercode+"/CRE/getDispositionPageOfTab";
	
	Lobibox.confirm({		msg: "Are you sure you want to delete this CallInfo?",
        callback: function ($this, type) {
            if (type === 'yes') {     	 
            	
            	$.ajax({
            		
            		url:urlDisposition 
            	}).done(function(data){
            		
            		window.location = urlString;
            		Lobibox.alert('info', {
                        msg: "Call Deleted"
                    });
            		
            	});
               
            } else if (type === 'no') {
                
            	
            }
       
        }
        });
}
//callScheduledDelete
function callScheduledDelete(id,dealercode){

	var urlDisposition="/"+dealercode+"/CRE/deleteSchCall/"+id+"";
	var urlString="/"+dealercode+"/CRE/getDispositionPageOfTab";
	
	Lobibox.confirm({msg: "Are you sure you want to delete this ScheduledCall?",
        callback: function ($this, type) {
            if (type === 'yes') {     	 
            	
            	$.ajax({
            		
            		url:urlDisposition 
            	}).done(function(data){           		
            		
            		window.location = urlString;
            		Lobibox.alert('info', {
                        msg: "ScheduledCall Deleted"
                    });
            		
            	});
               
            } else if (type === 'no') {
                
            	
            }
       
        }
        });
}





//CallInitiation

function callfunction(phonenumber,id,dealercode){	
	 var uniqueid=1;
	var urlString="/CRE/ajax/initiateCall/"+phonenumber+"/"+uniqueid+"";
	var urlDisposition="/"+dealercode+"/CRE/getCallDispositionPage/"+id+"";
	$.ajax({
		
		url:urlString 
	}).done(function(data){
		if(data=="success"){
			window.location = urlDisposition;
			
		}else{
			
			Lobibox.alert('error', {
                msg: "Not Authorized to Call from Web"
            });
			
			
		}
		
	});
	
}




//callInitiating for disposition

function callfunctionDisposition(phonenumber,id,dealercode){
	 var uniqueid=1;
	
	var urlString="/CRE/ajax/initiateCall/"+phonenumber+"/"+uniqueid+"";
	var urlDisposition="/"+dealercode+"/CRE/getFollowUpCallDispositionPage/"+id+"";
	$.ajax({
		
		url:urlString 
	}).done(function(data){
		if(data=="success"){
			window.location = urlDisposition;
			
		}else{
			
			Lobibox.alert('error', {
                msg: "Not Authorized to Call from Web"
            });
			
			
		}
		
	});
	
}


//initiate from page

function callfunctionFromPage(phonenumberdata,uniqueid,customerId){
	
	
var vehicleId=document.getElementById('vehical_Id').value;
	
	
    var urlLink="/uniqueIdForCallSyncData"; 
    var phonenumber= $("#ddl_phone_no").val();
    
    $.ajax({
        url: urlLink

    }).done(function (uniqueIdIs) {
 	   
    	uniqueid=uniqueIdIs;
    	console.log("unique id is : "+uniqueid);
    	document.getElementById('uniqueidForCallSync').value=uniqueid;
    	

    	if(customerId==""){
    		
    		var urlString="/CRE/ajax/initiateCall/"+phonenumber+"/"+uniqueid+"/0/0";
    		
    	}else{		
    		
    		var urlString="/CRE/ajax/initiateCall/"+phonenumber+"/"+uniqueid+"/"+customerId+"/"+vehicleId;
    	}   	
    	
    	
    	Lobibox.confirm({msg: "Are you sure you want to Call?",
            callback: function ($this, type) {
                if (type === 'yes') {     	 
                	
                	$.ajax({
                		
                		url:urlString 
                	}).done(function(data){           		
                		
                		
                		if(data=="success"){
                			
                			Lobibox.alert('info', {
                                msg: "Call Initiated from Web"
                            });
                			
                			document.getElementById('isCallinitaited').value='Initiated';
                			var btn=$("#callIdBtn");
                			btn.prop('disabled', true);
                		    setTimeout(function(){
                		        btn.prop('disabled', false);
                		    }, 20*1000);
                		    
                			
                		}else if(data=="Failure"){
                			
                			Lobibox.alert('error', {
                                msg: "Call Initiated Failed"
                            });
                			
                			document.getElementById('isCallinitaited').value='Failed';
                			
                		}
                		
                		else{
                			
                			Lobibox.alert('error', {
                                msg: "User Not Authenticated"
                            });
                			
                			document.getElementById('isCallinitaited').value='NotAuthenticated';
                			
                		}
                		
                		
                	});
                   
                } else if (type === 'no') {
                	
                	document.getElementById('isCallinitaited').value='NO';
                    
                	
                }
           
            }
            });
    });
	
		
	
}

//GSM CALL INITIATION HYUNDAI


function callfunctionFromPageGSM(){
	
	var phonenumber= $("#ddl_phone_no").val();
	
	var urlDisposition="/CRE/ajax/initiateCallGSM/"+phonenumber+"";
	$.ajax({
		
		url:urlDisposition 
	}).done(function(data){
		
		console.log("data is : "+data);
		
		//var jsondata=$.parseJSON(data);
		
		console.log(data.number);
		console.log(data.originate_status)
		var resultOfCall=data.originate_status;
		
		if(resultOfCall=="1"){
			
			Lobibox.alert('info', {
                msg: "Call Initiated from GSM"
            });
			
			document.getElementById('isCallinitaited').value='Initiated';
			document.getElementById('makeCallFromGSM').value='GSM';
			
			var gsmid=data.uniqueid;
			console.log(" gsmid.parseInt() "+parseInt(gsmid));
			
			document.getElementById('uniqueidForCallSync').value=parseInt(gsmid);
			document.getElementById('uniqueIdGSM').value=gsmid;
			
		}else if(resultOfCall=="0"){
			
			Lobibox.alert('error', {
                msg: "Call Initiated Failed From GSM "+data.message 	});
			
			document.getElementById('isCallinitaited').value='Failed';
			document.getElementById('makeCallFromGSM').value='GSM';
			document.getElementById('uniqueidForCallSync').value=0;
			document.getElementById('uniqueIdGSM').value=0;
			
		}
		
	});
	
	
	
}




//GSM CALL INITIATION CAUVERY


function callfunctionFromPageGSMCAUVERY(phonenumberdata){
	
	 var phonenumber= $("#ddl_phone_no").val();
	
	var urlDisposition="/CRE/ajax/initiateCallGSMCauvery/"+phonenumber+"";
	$.ajax({
		
		url:urlDisposition 
	}).done(function(data){
		
		console.log("data is : "+data);
		
		var jsondata=$.parseJSON(data);
		
		console.log(jsondata['calls']['number']);
		console.log(jsondata['calls']['originate_status'])
		var resultOfCall=jsondata['calls']['originate_status'];
		
		if(resultOfCall=="Success"){
			
			Lobibox.alert('info', {
               msg: "Call Initiated from GSM"
           });
			
			document.getElementById('isCallinitaited').value='Initiated';
			document.getElementById('makeCallFromGSM').value='GSM';
			
			var gsmid=jsondata['calls']['uniqueid'];
			console.log(" gsmid.parseInt() "+parseInt(gsmid));
			
			document.getElementById('uniqueidForCallSync').value=parseInt(gsmid);
			document.getElementById('uniqueIdGSM').value=gsmid;
			
		}else if(resultOfCall=="Failed"){
			
			Lobibox.alert('error', {
               msg: "Call Initiated Failed From GSM" 
               	});
			
			document.getElementById('isCallinitaited').value='Failed';
			document.getElementById('makeCallFromGSM').value='GSM';
			document.getElementById('uniqueidForCallSync').value=0;
			document.getElementById('uniqueIdGSM').value=0;
			
		}
		
	});
	
	
	
}

//Download Recording


function downloadCallRecording(dealer,id){
	
	var urlString="/"+dealer+"/CREManager/downloadMediaFile/"+id+"";
	var downloadUrl ="/"+dealer+"/CREManager/download/"+id+"";
	$.ajax({		
			url:urlString 
		}).done(function(data){
			if(data=="No access"){				

				Lobibox.alert('error', {
	                msg: "Not Authorized to Download Call from Web"
	            });
				
				
			}else if(data=="CallDuration is Zero"){
				
				Lobibox.alert('info', {
	                msg: "CallDuartion is Zero,Audio File Cannot be Downloaded!!"
	            });
				
				
			}else{
				
				window.location = downloadUrl;
			}			
		});
	
	
}


//Map Display

function loadMap(lat, lon) {
	
	if(!lat==''){
	
	var latlang = new google.maps.LatLng(lat, lon);
	$("#dialog").dialog({
		modal : true,
		title : "Google Map",
		width : 'auto',
		hright : 'auto',
		top : '0px!important',
		left : 'auto',

		buttons : {
			Close : function() {
				$(this).dialog('close');
			}
		},
		open : function() {
			var mapOptions = {

				center : latlang,
				zoom : 15,				
				mapTypeId : google.maps.MapTypeId.ROADMAP
			}
			var map = new google.maps.Map($("#dvMap")[0], mapOptions);
			var marker = new google.maps.Marker({
				position : latlang,
				my : 'center',
				at : 'center',
				map : map,
				title : "Call location"
			});
		}
	});
	
	}else{
		
		Lobibox.alert('error', {
            msg: "Not Authorized to View Map from Web"
        });
	}
	

}

function tab_hide() {
	if($("#tabShow").length){
		$("#tabShow").hide();
	}
}
function div_detail(tab_id) {
	tab_hide();
	if($("#" + tab_id).length){
		$("#" + tab_id).show();
	}
	return false;
}

$(document).ready(function() {
	
	if($("table[id^='dataTables-example']").length){
		$("table[id^='dataTables-example']").DataTable({
			responsive : true,
	
		});
	}
	
	
	
	
	if($('#dataTables-example').length){
		$('#dataTables-example').DataTable({
			responsive : true,
	
		});
	}
	
	tab_hide();
	
    $("body").on("click", "#js-upload-submit", function () {
        
        var allowedFiles = [".csv", ".CSV"];
        var fileUpload = $("#js-upload-files");
        var lblError = $("#lblError");
        var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
        if (!regex.test(fileUpload.val().toLowerCase())) {
           
            lblError.html("Please upload files having extensions: <b>" + allowedFiles.join(', ') + "</b> only.");
            return false;
        }
        lblError.html('');
        return true;
    });
    
    $(document).on('click', '#assignCallsBtn', function() {
    	   $.blockUI();
    });
    
    $(document).on('click', '#js-upload-submit', function() {
 	   $.blockUI();
 	   
 	  
        
 });
    
    $(document).on('click', '.upload-submitExcel', function() {
  	   $.blockUI();         
  });
    
    
    
	
});







function goBack() {
	history.back();
}


$(document).ready(function() {
	
	$("body").on("click", "#js-upload-submit1", function () {
	        
	        var allowedFiles = [".csv", ".CSV"];
	        var fileUpload = $("#js-upload-files");
	        var lblError = $("#lblError");
	        var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
	        if (!regex.test(fileUpload.val().toLowerCase())) {
	           
	            lblError.html("Please upload files having extensions: <b>" + allowedFiles.join(', ') + "</b> only.");
	            return false;
	        }
	        lblError.html('');
	        return true;
	    });
	
	
	$(document).on('click', '#js-upload-submit1', function() {
		
		var options = document.getElementById('data[]').options, count = 0;
		
		for (var i=0; i < options.length; i++) {
		  if (options[i].selected) count++;		 
		}
		
		if(count>0){
			
	 	   $.blockUI();
		}		
	 });
$(document).on('click', '#lodingOption', function() {
	
	 	   $.blockUI();
				
	 });
		
		
		
	});
//function to ajax for Sales Manager

function ajaxRequestSalesScheduledCallCountForSalesManager(){
	
	$.ajax({
		
		url:"/SalesManager/ajax/getTtlSalesSchCallsOFSalesMan"  
	}).done(function(data){
		$("#salesSchCountSalesMan").html(data);	
		
	});
	
}
function ajaxRequestSalesSchCallsMadeCountForSalesManager(){
	
$.ajax({
		
		url:"/SalesManager/ajax/getTtlSalesSchCallsPendingSalesMan"
	}).done(function(data){
		$("#salesSchCountPendingSalesMan").html(data);	
		
	});
}

function ajaxRequestSalesPercConversionOfTestDriveOfSalesMan(){
	
$.ajax({
		
		url:"/SalesManager/ajax/getTtlSalesConversionTestDriveOfSalesMan"
	}).done(function(data){
		$("#salesPerConvTDSalesMan").html(data);	
		
	});
	
}


//function to ajax request for Sales Executives

function ajaxRequestSalesScheduledCallCountForSE(){
	
	$.ajax({
		
		url:"/SalesExecutive/ajax/getTtlSalesSchCallsOFSE"
	}).done(function(data){
		$("#salesSchCountSE").html(data);	
		
	});
	
	
}

function ajaxRequestSalesSchCallsMadeCountForSE(){
	$.ajax({		
		url:"/SalesExecutive/ajax/getTtlSalesSchCallsPendingSE"
	}).done(function(data){
		$("#salesSchSEPending").html(data);	
		
	});
}

function ajaxRequestSalesPercConversionOfTestDrive(){
	
	$.ajax({		
		url:"/SalesExecutive/ajax/getTtlSalesConversionTestDriveOfSE"
	}).done(function(data){
		$("#salesSEConversionTD").html(data);	
		
	});
	
	
	
}



//function to ajax request for CRE


/*function ajaxRequestScheduledCallCountForCRE() { 
    $.ajax({
        url: "/CRE/ajax/getTtlSchCallsCRE"
    }).done(function(data) {
        $("#schCRECount").html(data);
    });
}

function ajaxRequestSchCallsMadeCountForCRE(){
	$.ajax({
        url: "/CRE/ajax/getTtlSchCallsPendingCRE"
    }).done(function(data) {
        $("#schCREPending").html(data);
    });
}

function ajaxRequestServiceBookedCountForCRE(){
	$.ajax({
        url: "/CRE/ajax/getTtlServiceBookedForCRE"
    }).done(function(data) {
        $("#serviceCREBookd").html(data);
    });
}

function ajaxRequestServiceBookedPercentageForCRE(){
	$.ajax({
        url: "/CRE/ajax/getPercServiceBookedForsCRE"
    }).done(function(data) {
        $("#serviceBookdCREPer").html(data);
    });
}
*/
function ajaxRequestDataOFCRE(){
	
	$.ajax({
        url: "/CRE/ajax/getAjaxDataCompleteData"
    }).done(function(data) {
    	//alert(data);
    	
        $("#schCRECount").html(data[0]);
        $("#schCREPending").html(data[1]);
        $("#serviceCREBookd").html(data[2]);
        $("#serviceBookdCREPer").html(data[3]);
       
        
    });
	
}

function ajaxRequestDataOFCREIndexPage(){
	
	var dataIndex1 = document.getElementById("indexBox1").value;	
	var myJsonString = JSON.parse(dataIndex1);
	 $("#schCRECount").html(myJsonString[0]);
     $("#schCREPending").html(myJsonString[1]);
     $("#serviceCREBookd").html(myJsonString[2]);
     $("#serviceBookdCREPer").html(myJsonString[3]);
     
	
}



//function to ajax request for CRE Manager
/*function ajaxRequestScheduledCallCount() { 
    $.ajax({
        url: "/ajax/getTtlSchCallsCREManager"
    }).done(function(data) {
        $("#schCount").html(data);
    });
}

function ajaxRequestSchCallsMadeCount(){
	$.ajax({
        url: "/ajax/getTtlSchCallsPendingCREManager"
    }).done(function(data) {
        $("#schPending").html(data);
    });
}

function ajaxRequestServiceBookedCount(){
	$.ajax({
        url: "/ajax/getTtlServiceBookedCREManager"
    }).done(function(data) {
        $("#serviceBookd").html(data);
    });
}
*/
//function ajaxRequestData(){
//	
//	$.ajax({
//        url: "/ajax/getAjaxData"
//    }).done(function(data) {
//    	
//    	
//        $("#schCount").html(data[0]);
//        $("#schPending").html(data[1]);
//        $("#serviceBookd").html(data[2]);
//        $("#serviceBookdPer").html(data[3]);
//        
//    });
//	
//}

//function ajaxRequestDataIndexPageCREMAn(){
//	
//	var dataIndex1 = document.getElementById("indexBox1").value;	
//	var myJsonString = JSON.parse(dataIndex1);
//	 $("#schCount").html(myJsonString[0]);
//     $("#schPending").html(myJsonString[1]);
//     $("#serviceBookd").html(myJsonString[2]);
//     $("#serviceBookdPer").html(myJsonString[3]);    
//	
//	
//}

/*function ajaxRequestServiceBookedPercentage(){
	$.ajax({
        url: "/ajax/getPercServiceBookedCREManager"
    }).done(function(data) {
        $("#serviceBookdPer").html(data);
    });
}*/



function ajaxRequestAreaChartCREMan(){
	
	$.ajax({
        url: "/ajax/getBookedListByTime"
    }).done(function(data) {
    	
    
       	
    	var bookedData = data.bookedList;
    	var calldata = data.callList;
       	
    	
    	var areaChartCanvas = $("#areaChart").get(0).getContext("2d");
    	// This will get the first returned node in the jQuery collection.
    	var areaChart = new Chart(areaChartCanvas);

    	var areaChartData = {
    		labels : [ "9 to 11 AM", "11 to 1 PM", "1 to 3 PM", "3 to 5 PM", "5 to 7 PM"],
    		datasets : [ {
    			label : "Call made",
    			fillColor : "rgba(210, 214, 222, 1)",
    			strokeColor : "rgba(210, 214, 222, 1)",
    			pointColor : "rgba(210, 214, 222, 1)",
    			pointStrokeColor : "#c1c7d1",
    			pointHighlightFill : "#fff",
    			pointHighlightStroke : "rgba(220,220,220,1)",
    			data : [ 65, 59, 80, 81, 56, 55, 40 ]
    		}, {
    			label : "Service Booked",
    			fillColor : "rgba(60,141,188,0.9)",
    			strokeColor : "rgba(60,141,188,0.8)",
    			pointColor : "#3b8bba",
    			pointStrokeColor : "rgba(60,141,188,1)",
    			pointHighlightFill : "#fff",
    			pointHighlightStroke : "rgba(60,141,188,1)",
    			data : [ 28, 48, 40, 19, 86, 27, 90 ]
    		} ]
    	};
   
    	areaChartData.datasets[0].data = calldata;
    	
    	areaChartData.datasets[1].data = bookedData;
    	

    	var areaChartOptions = {
    		//Boolean - If we should show the scale at all
    		showScale : true,
    		//Boolean - Whether grid lines are shown across the chart
    		scaleShowGridLines : false,
    		//String - Colour of the grid lines
    		scaleGridLineColor : "rgba(0,0,0,.05)",
    		//Number - Width of the grid lines
    		scaleGridLineWidth : 1,
    		//Boolean - Whether to show horizontal lines (except X axis)
    		scaleShowHorizontalLines : true,
    		//Boolean - Whether to show vertical lines (except Y axis)
    		scaleShowVerticalLines : true,
    		//Boolean - Whether the line is curved between points
    		bezierCurve : true,
    		//Number - Tension of the bezier curve between points
    		bezierCurveTension : 0.3,
    		//Boolean - Whether to show a dot for each point
    		pointDot : true,
    		//Number - Radius of each point dot in pixels
    		pointDotRadius : 4,
    		//Number - Pixel width of point dot stroke
    		pointDotStrokeWidth : 1,
    		//Number - amount extra to add to the radius to cater for hit detection outside the drawn point
    		pointHitDetectionRadius : 20,
    		//Boolean - Whether to show a stroke for datasets
    		datasetStroke : true,
    		//Number - Pixel width of dataset stroke
    		datasetStrokeWidth : 2,
    		//Boolean - Whether to fill the dataset with a color
    		datasetFill : true,
    		//String - A legend template
    		legendTemplate : "<ul ><li><Services Booked</li><li><Calls Made</li></ul>",
    		//Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
    		maintainAspectRatio : true,
    		//Boolean - whether to make the chart responsive to window resizing
    		responsive : true,
    		animation: true,
 		   barValueSpacing : 5,
 		   barDatasetSpacing : 1,
 		   tooltipFillColor: "rgba(0,0,0,0.8)",
 		   multiTooltipTemplate: "<%= datasetLabel %> - <%= value %>"
    		
    		
    	};

    	//Create the line chart
    	areaChart.Line(areaChartData, areaChartOptions);
  
        

    	
    	
    });


	
}

function ajaxRequstPieChartCallType(){
	
	
	$.ajax({
        url: "/ajax/getCallTypePie"
    }).done(function(data) {
        
    	var missedCount = data.missedCallCount;
    	var outgoingCount = data.outgoingCallCount;
    	var incommingCount = data.incomingCallCount;
    	
     	
    	var pieChartCanvas = $("#pieChart").get(0).getContext("2d");
    	var pieChart = new Chart(pieChartCanvas);
    	var PieData = [ {
    		value : 700,
    		color : "#f56954",
    		highlight : "#f56954",
    		label : "Missed"
    	}, {
    		value : 500,
    		color : "#00a65a",
    		highlight : "#00a65a",
    		label : "Outgoing"
    	}, {
    		value : 400,
    		color : "#f39c12",
    		highlight : "#f39c12",
    		label : "In-Comming"
    	}];
    	
    	PieData[0].value = missedCount;
    	PieData[1].value = outgoingCount;
    	PieData[2].value = incommingCount;
    	
    	var pieOptions = {
    		//Boolean - Whether we should show a stroke on each segment
    		segmentShowStroke : true,
    		//String - The colour of each segment stroke
    		segmentStrokeColor : "#fff",
    		//Number - The width of each segment stroke
    		segmentStrokeWidth : 2,
    		//Number - The percentage of the chart that we cut out of the middle
    		percentageInnerCutout : 50, // This is 0 for Pie charts
    		//Number - Amount of animation steps
    		animationSteps : 130,
    		//String - Animation easing effect
    		animationEasing : "easeOutBounce",
    		//Boolean - Whether we animate the rotation of the Doughnut
    		animateRotate : true,
    		//Boolean - Whether we animate scaling the Doughnut from the centre
    		animateScale : false,
    		//Boolean - whether to make the chart responsive to window resizing
    		responsive : true,
    		// Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
    		maintainAspectRatio : true,
    		//String - A legend template
    		legendTemplate : "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>"
    	};
    	//Create pie or douhnut chart
    	// You can switch between pie and douhnut using the method below.
    	pieChart.Doughnut(PieData, pieOptions);


    	
    });
	
}
function ajaxRequestPieChartForCRE(){
	//Dashboard
	
	var dataIndex1 = document.getElementById("indexBox1").value;	
	var myJsonString = JSON.parse(dataIndex1);
	 $("#schCRECount").html(myJsonString[0]);
     $("#schCREPending").html(myJsonString[1]);
     $("#serviceCREBookd").html(myJsonString[2]);
     $("#serviceBookdCREPer").html(myJsonString[3]);
     
	//areachart
	
	var data1area = document.getElementById('areachartdata1').value;
	var data2area = document.getElementById('areachartdata2').value;
	
	
	var calldata = data1area;
	var bookedData = data2area;
	
   	
	
	var areaChartCanvas = $("#areaChartForCRE").get(0).getContext("2d");
	// This will get the first returned node in the jQuery collection.
	var areaChart = new Chart(areaChartCanvas);

	var areaChartData = {
		labels : [ "9 to 11 AM", "11 to 1 PM", "1 to 3 PM", "3 to 5 PM", "5 to 7 PM"],
		datasets : [ {
			label : "Call made",
			fillColor : "rgba(210, 214, 222, 1)",
			strokeColor : "rgba(210, 214, 222, 1)",
			pointColor : "rgba(210, 214, 222, 1)",
			pointStrokeColor : "#c1c7d1",
			pointHighlightFill : "#fff",
			pointHighlightStroke : "rgba(220,220,220,1)",
			data : [ 65, 59, 80, 81, 56, 55, 40 ]
		}, {
			label : "Service Booked",
			fillColor : "rgba(60,141,188,0.9)",
			strokeColor : "rgba(60,141,188,0.8)",
			pointColor : "#3b8bba",
			pointStrokeColor : "rgba(60,141,188,1)",
			pointHighlightFill : "#fff",
			pointHighlightStroke : "rgba(60,141,188,1)",
			data : [ 28, 48, 40, 19, 86, 27, 90 ]
		} ]
	};

	areaChartData.datasets[0].data = JSON.parse(calldata);
	
	areaChartData.datasets[1].data = JSON.parse(bookedData);
	

	var areaChartOptions = {
		//Boolean - If we should show the scale at all
		showScale : true,
		//Boolean - Whether grid lines are shown across the chart
		scaleShowGridLines : false,
		//String - Colour of the grid lines
		scaleGridLineColor : "rgba(0,0,0,.05)",
		//Number - Width of the grid lines
		scaleGridLineWidth : 1,
		//Boolean - Whether to show horizontal lines (except X axis)
		scaleShowHorizontalLines : true,
		//Boolean - Whether to show vertical lines (except Y axis)
		scaleShowVerticalLines : true,
		//Boolean - Whether the line is curved between points
		bezierCurve : true,
		//Number - Tension of the bezier curve between points
		bezierCurveTension : 0.3,
		//Boolean - Whether to show a dot for each point
		pointDot : true,
		//Number - Radius of each point dot in pixels
		pointDotRadius : 4,
		//Number - Pixel width of point dot stroke
		pointDotStrokeWidth : 1,
		//Number - amount extra to add to the radius to cater for hit detection outside the drawn point
		pointHitDetectionRadius : 20,
		//Boolean - Whether to show a stroke for datasets
		datasetStroke : true,
		//Number - Pixel width of dataset stroke
		datasetStrokeWidth : 2,
		//Boolean - Whether to fill the dataset with a color
		datasetFill : true,
		//String - A legend template
		legendTemplate : "<ul ><li><Services Booked</li><li><Calls Made</li></ul>",
		//Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
		maintainAspectRatio : true,
		//Boolean - whether to make the chart responsive to window resizing
		responsive : true,    		
		animation: true,
		   barValueSpacing : 5,
		   barDatasetSpacing : 1,
		   tooltipFillColor: "rgba(0,0,0,0.8)",
		multiTooltipTemplate: "<%= datasetLabel %> - <%= value %>"
		
		
	};

	//Create the line chart
	areaChart.Line(areaChartData, areaChartOptions);
	
	
	
	
	//piechart
	var data1 = document.getElementById("piechartdata").value;
	var data2 = document.getElementById("piechartdata1").value;
	var data3 = document.getElementById("piechartdata2").value;
	
	 
	var missedCount = data1;
	var outgoingCount = data2;
	var incommingCount = data3;
	
 	
	var pieChartCanvas = $("#pieChartForCRE").get(0).getContext("2d");
	var pieChart = new Chart(pieChartCanvas);
	var PieData = [ {
		value : 700,
		color : "#f56954",
		highlight : "#f56954",
		label : "Missed"
	}, {
		value : 500,
		color : "#00a65a",
		highlight : "#00a65a",
		label : "Outgoing"
	}, {
		value : 400,
		color : "#f39c12",
		highlight : "#f39c12",
		label : "In-Comming"
	}];
	
	PieData[0].value = missedCount;
	PieData[1].value = outgoingCount;
	PieData[2].value = incommingCount;
	
	var pieOptions = {
		//Boolean - Whether we should show a stroke on each segment
		segmentShowStroke : true,
		//String - The colour of each segment stroke
		segmentStrokeColor : "#fff",
		//Number - The width of each segment stroke
		segmentStrokeWidth : 2,
		//Number - The percentage of the chart that we cut out of the middle
		percentageInnerCutout : 50, // This is 0 for Pie charts
		//Number - Amount of animation steps
		animationSteps : 130,
		//String - Animation easing effect
		animationEasing : "easeOutBounce",
		//Boolean - Whether we animate the rotation of the Doughnut
		animateRotate : true,
		//Boolean - Whether we animate scaling the Doughnut from the centre
		animateScale : false,
		//Boolean - whether to make the chart responsive to window resizing
		responsive : true,
		// Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
		maintainAspectRatio : true,
		//String - A legend template
		legendTemplate : "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>"
	};
	//Create pie or douhnut chart
	// You can switch between pie and douhnut using the method below.
	pieChart.Doughnut(PieData, pieOptions);
	
	
	
	
}
function ajaxRequstPieChartCallTypeForCRE(){
	
	
	$.ajax({
        url: "/CRE/ajax/getCallTypePieForCRE"
    }).done(function(data) {
        
    	var missedCount = data.missedCallCount;
    	var outgoingCount = data.outgoingCallCount;
    	var incommingCount = data.incomingCallCount;
    	
     	
    	var pieChartCanvas = $("#pieChartForCRE").get(0).getContext("2d");
    	var pieChart = new Chart(pieChartCanvas);
    	var PieData = [ {
    		value : 700,
    		color : "#f56954",
    		highlight : "#f56954",
    		label : "Missed"
    	}, {
    		value : 500,
    		color : "#00a65a",
    		highlight : "#00a65a",
    		label : "Outgoing"
    	}, {
    		value : 400,
    		color : "#f39c12",
    		highlight : "#f39c12",
    		label : "In-Comming"
    	}];
    	
    	PieData[0].value = missedCount;
    	PieData[1].value = outgoingCount;
    	PieData[2].value = incommingCount;
    	
    	var pieOptions = {
    		//Boolean - Whether we should show a stroke on each segment
    		segmentShowStroke : true,
    		//String - The colour of each segment stroke
    		segmentStrokeColor : "#fff",
    		//Number - The width of each segment stroke
    		segmentStrokeWidth : 2,
    		//Number - The percentage of the chart that we cut out of the middle
    		percentageInnerCutout : 50, // This is 0 for Pie charts
    		//Number - Amount of animation steps
    		animationSteps : 130,
    		//String - Animation easing effect
    		animationEasing : "easeOutBounce",
    		//Boolean - Whether we animate the rotation of the Doughnut
    		animateRotate : true,
    		//Boolean - Whether we animate scaling the Doughnut from the centre
    		animateScale : false,
    		//Boolean - whether to make the chart responsive to window resizing
    		responsive : true,
    		// Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
    		maintainAspectRatio : true,
    		//String - A legend template
    		legendTemplate : "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>"
    	};
    	//Create pie or douhnut chart
    	// You can switch between pie and douhnut using the method below.
    	pieChart.Doughnut(PieData, pieOptions);


    	
    });
	
}

function ajaxRequestAreaChartForCRE(){
	
	$.ajax({
        url: "/CRE/ajax/getBookedListByTime"
    }).done(function(data) {
    	
    
       	
    	var bookedData = data.bookedList;
    	var calldata = data.callList;
       	
    	
    	var areaChartCanvas = $("#areaChartForCRE").get(0).getContext("2d");
    	// This will get the first returned node in the jQuery collection.
    	var areaChart = new Chart(areaChartCanvas);

    	var areaChartData = {
    		labels : [ "9 to 11 AM", "11 to 1 PM", "1 to 3 PM", "3 to 5 PM", "5 to 7 PM"],
    		datasets : [ {
    			label : "Call made",
    			fillColor : "rgba(210, 214, 222, 1)",
    			strokeColor : "rgba(210, 214, 222, 1)",
    			pointColor : "rgba(210, 214, 222, 1)",
    			pointStrokeColor : "#c1c7d1",
    			pointHighlightFill : "#fff",
    			pointHighlightStroke : "rgba(220,220,220,1)",
    			data : [ 65, 59, 80, 81, 56, 55, 40 ]
    		}, {
    			label : "Service Booked",
    			fillColor : "rgba(60,141,188,0.9)",
    			strokeColor : "rgba(60,141,188,0.8)",
    			pointColor : "#3b8bba",
    			pointStrokeColor : "rgba(60,141,188,1)",
    			pointHighlightFill : "#fff",
    			pointHighlightStroke : "rgba(60,141,188,1)",
    			data : [ 28, 48, 40, 19, 86, 27, 90 ]
    		} ]
    	};
   
    	areaChartData.datasets[0].data = calldata;
    	
    	areaChartData.datasets[1].data = bookedData;
    	

    	var areaChartOptions = {
    		//Boolean - If we should show the scale at all
    		showScale : true,
    		//Boolean - Whether grid lines are shown across the chart
    		scaleShowGridLines : false,
    		//String - Colour of the grid lines
    		scaleGridLineColor : "rgba(0,0,0,.05)",
    		//Number - Width of the grid lines
    		scaleGridLineWidth : 1,
    		//Boolean - Whether to show horizontal lines (except X axis)
    		scaleShowHorizontalLines : true,
    		//Boolean - Whether to show vertical lines (except Y axis)
    		scaleShowVerticalLines : true,
    		//Boolean - Whether the line is curved between points
    		bezierCurve : true,
    		//Number - Tension of the bezier curve between points
    		bezierCurveTension : 0.3,
    		//Boolean - Whether to show a dot for each point
    		pointDot : true,
    		//Number - Radius of each point dot in pixels
    		pointDotRadius : 4,
    		//Number - Pixel width of point dot stroke
    		pointDotStrokeWidth : 1,
    		//Number - amount extra to add to the radius to cater for hit detection outside the drawn point
    		pointHitDetectionRadius : 20,
    		//Boolean - Whether to show a stroke for datasets
    		datasetStroke : true,
    		//Number - Pixel width of dataset stroke
    		datasetStrokeWidth : 2,
    		//Boolean - Whether to fill the dataset with a color
    		datasetFill : true,
    		//String - A legend template
    		legendTemplate : "<ul ><li><Services Booked</li><li><Calls Made</li></ul>",
    		//Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
    		maintainAspectRatio : true,
    		//Boolean - whether to make the chart responsive to window resizing
    		responsive : true,    		
    		animation: true,
    		   barValueSpacing : 5,
    		   barDatasetSpacing : 1,
    		   tooltipFillColor: "rgba(0,0,0,0.8)",
    		multiTooltipTemplate: "<%= datasetLabel %> - <%= value %>"
    		
    		
    	};

    	//Create the line chart
    	areaChart.Line(areaChartData, areaChartOptions);
  
        

    	
    	
    });


	
}

function ajaxRequstAreaChartCallTypeForSalesExec(){
	
	$.ajax({
        url: "/SalesExecutive/ajax/getTestDriveListByTimeofSE"
    }).done(function(data) {
    	
    
       	
    	var bookedData = data.bookedList;
    	var calldata = data.callList;
       	
    	
    	var areaChartCanvas = $("#areaChartForSE").get(0).getContext("2d");
    	// This will get the first returned node in the jQuery collection.
    	var areaChart = new Chart(areaChartCanvas);

    	var areaChartData = {
    		labels : [ "9 to 11 AM", "11 to 1 PM", "1 to 3 PM", "3 to 5 PM", "5 to 7 PM"],
    		datasets : [ {
    			label : "Calls made",
    			fillColor : "rgba(210, 214, 222, 1)",
    			strokeColor : "rgba(210, 214, 222, 1)",
    			pointColor : "rgba(210, 214, 222, 1)",
    			pointStrokeColor : "#c1c7d1",
    			pointHighlightFill : "#fff",
    			pointHighlightStroke : "rgba(220,220,220,1)",
    			data : [ 65, 59, 80, 81, 56, 55, 40 ]
    		}, {
    			label : "Test Drive",
    			fillColor : "rgba(60,141,188,0.9)",
    			strokeColor : "rgba(60,141,188,0.8)",
    			pointColor : "#3b8bba",
    			pointStrokeColor : "rgba(60,141,188,1)",
    			pointHighlightFill : "#fff",
    			pointHighlightStroke : "rgba(60,141,188,1)",
    			data : [ 28, 48, 40, 19, 86, 27, 90 ]
    		} ]
    	};
   
    	areaChartData.datasets[0].data = calldata;
    	
    	areaChartData.datasets[1].data = bookedData;
    	

    	var areaChartOptions = {
    		//Boolean - If we should show the scale at all
    		showScale : true,
    		//Boolean - Whether grid lines are shown across the chart
    		scaleShowGridLines : false,
    		//String - Colour of the grid lines
    		scaleGridLineColor : "rgba(0,0,0,.05)",
    		//Number - Width of the grid lines
    		scaleGridLineWidth : 1,
    		//Boolean - Whether to show horizontal lines (except X axis)
    		scaleShowHorizontalLines : true,
    		//Boolean - Whether to show vertical lines (except Y axis)
    		scaleShowVerticalLines : true,
    		//Boolean - Whether the line is curved between points
    		bezierCurve : true,
    		//Number - Tension of the bezier curve between points
    		bezierCurveTension : 0.3,
    		//Boolean - Whether to show a dot for each point
    		pointDot : true,
    		//Number - Radius of each point dot in pixels
    		pointDotRadius : 4,
    		//Number - Pixel width of point dot stroke
    		pointDotStrokeWidth : 1,
    		//Number - amount extra to add to the radius to cater for hit detection outside the drawn point
    		pointHitDetectionRadius : 20,
    		//Boolean - Whether to show a stroke for datasets
    		datasetStroke : true,
    		//Number - Pixel width of dataset stroke
    		datasetStrokeWidth : 2,
    		//Boolean - Whether to fill the dataset with a color
    		datasetFill : true,
    		//String - A legend template
    		legendTemplate : "<ul ><li><Services Booked</li><li><Calls Made</li></ul>",
    		//Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
    		maintainAspectRatio : true,
    		//Boolean - whether to make the chart responsive to window resizing
    		responsive : true,    		
    		animation: true,
    		   barValueSpacing : 5,
    		   barDatasetSpacing : 1,
    		   tooltipFillColor: "rgba(0,0,0,0.8)",
    		multiTooltipTemplate: "<%= datasetLabel %> - <%= value %>"
    		
    		
    	};

    	//Create the line chart
    	areaChart.Line(areaChartData, areaChartOptions);
  
        

    	
    	
    });
	
	
}

function ajaxRequstAreaChartCallTypeForSalesMan(){
	
	$.ajax({
        url: "/SalesManager/ajax/getTestDriveListByTimeofSalesManager"
    }).done(function(data) {
    	
    
       	
    	var bookedData = data.bookedList;
    	var calldata = data.callList;
       	
    	
    	var areaChartCanvas = $("#areaChartForSalesMan").get(0).getContext("2d");
    	// This will get the first returned node in the jQuery collection.
    	var areaChart = new Chart(areaChartCanvas);

    	var areaChartData = {
    		labels : [ "9 to 11 AM", "11 to 1 PM", "1 to 3 PM", "3 to 5 PM", "5 to 7 PM"],
    		datasets : [ {
    			label : "Calls made",
    			fillColor : "rgba(210, 214, 222, 1)",
    			strokeColor : "rgba(210, 214, 222, 1)",
    			pointColor : "rgba(210, 214, 222, 1)",
    			pointStrokeColor : "#c1c7d1",
    			pointHighlightFill : "#fff",
    			pointHighlightStroke : "rgba(220,220,220,1)",
    			data : [ 65, 59, 80, 81, 56, 55, 40 ]
    		}, {
    			label : "Test Drive",
    			fillColor : "rgba(60,141,188,0.9)",
    			strokeColor : "rgba(60,141,188,0.8)",
    			pointColor : "#3b8bba",
    			pointStrokeColor : "rgba(60,141,188,1)",
    			pointHighlightFill : "#fff",
    			pointHighlightStroke : "rgba(60,141,188,1)",
    			data : [ 28, 48, 40, 19, 86, 27, 90 ]
    		} ]
    	};
   
    	areaChartData.datasets[0].data = calldata;
    	
    	areaChartData.datasets[1].data = bookedData;
    	

    	var areaChartOptions = {
    		//Boolean - If we should show the scale at all
    		showScale : true,
    		//Boolean - Whether grid lines are shown across the chart
    		scaleShowGridLines : false,
    		//String - Colour of the grid lines
    		scaleGridLineColor : "rgba(0,0,0,.05)",
    		//Number - Width of the grid lines
    		scaleGridLineWidth : 1,
    		//Boolean - Whether to show horizontal lines (except X axis)
    		scaleShowHorizontalLines : true,
    		//Boolean - Whether to show vertical lines (except Y axis)
    		scaleShowVerticalLines : true,
    		//Boolean - Whether the line is curved between points
    		bezierCurve : true,
    		//Number - Tension of the bezier curve between points
    		bezierCurveTension : 0.3,
    		//Boolean - Whether to show a dot for each point
    		pointDot : true,
    		//Number - Radius of each point dot in pixels
    		pointDotRadius : 4,
    		//Number - Pixel width of point dot stroke
    		pointDotStrokeWidth : 1,
    		//Number - amount extra to add to the radius to cater for hit detection outside the drawn point
    		pointHitDetectionRadius : 20,
    		//Boolean - Whether to show a stroke for datasets
    		datasetStroke : true,
    		//Number - Pixel width of dataset stroke
    		datasetStrokeWidth : 2,
    		//Boolean - Whether to fill the dataset with a color
    		datasetFill : true,
    		//String - A legend template
    		legendTemplate : "<ul ><li><Services Booked</li><li><Calls Made</li></ul>",
    		//Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
    		maintainAspectRatio : true,
    		//Boolean - whether to make the chart responsive to window resizing
    		responsive : true,    		
    		animation: true,
    		   barValueSpacing : 5,
    		   barDatasetSpacing : 1,
    		   tooltipFillColor: "rgba(0,0,0,0.8)",
    		multiTooltipTemplate: "<%= datasetLabel %> - <%= value %>"
    		
    		
    	};

    	//Create the line chart
    	areaChart.Line(areaChartData, areaChartOptions);
    	
    	
    });
	
	
	
	
}


function ajaxRequstPieChartCallTypeForSalesExec(){
	
	
	$.ajax({
        url: "/SalesExecutive/ajax/getCallTypePieForSalesExecutive"
    }).done(function(data) {
        
    	var missedCount = data.missedCallCount;
    	var outgoingCount = data.outgoingCallCount;
    	var incommingCount = data.incomingCallCount;
    	
     	
    	var pieChartCanvas = $("#pieChartForSE").get(0).getContext("2d");
    	var pieChart = new Chart(pieChartCanvas);
    	var PieData = [ {
    		value : 700,
    		color : "#f56954",
    		highlight : "#f56954",
    		label : "Missed"
    	}, {
    		value : 500,
    		color : "#00a65a",
    		highlight : "#00a65a",
    		label : "Outgoing"
    	}, {
    		value : 400,
    		color : "#f39c12",
    		highlight : "#f39c12",
    		label : "In-Comming"
    	}];
    	
    	PieData[0].value = missedCount;
    	PieData[1].value = outgoingCount;
    	PieData[2].value = incommingCount;
    	
    	var pieOptions = {
    		//Boolean - Whether we should show a stroke on each segment
    		segmentShowStroke : true,
    		//String - The colour of each segment stroke
    		segmentStrokeColor : "#fff",
    		//Number - The width of each segment stroke
    		segmentStrokeWidth : 2,
    		//Number - The percentage of the chart that we cut out of the middle
    		percentageInnerCutout : 50, // This is 0 for Pie charts
    		//Number - Amount of animation steps
    		animationSteps : 130,
    		//String - Animation easing effect
    		animationEasing : "easeOutBounce",
    		//Boolean - Whether we animate the rotation of the Doughnut
    		animateRotate : true,
    		//Boolean - Whether we animate scaling the Doughnut from the centre
    		animateScale : false,
    		//Boolean - whether to make the chart responsive to window resizing
    		responsive : true,
    		// Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
    		maintainAspectRatio : true,
    		//String - A legend template
    		legendTemplate : "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>"
    	};
    	//Create pie or douhnut chart
    	// You can switch between pie and douhnut using the method below.
    	pieChart.Doughnut(PieData, pieOptions);


    	
    });
	
}

function ajaxRequstPieChartCallTypeForSalesManager(){
	
	
	$.ajax({
        url: "/SalesManager/ajax/getCallTypePieForSalesManager"
    }).done(function(data) {
        
    	var missedCount = data.missedCallCount;
    	var outgoingCount = data.outgoingCallCount;
    	var incommingCount = data.incomingCallCount;
    	
     	
    	var pieChartCanvas = $("#pieChartForSalesMan").get(0).getContext("2d");
    	var pieChart = new Chart(pieChartCanvas);
    	var PieData = [ {
    		value : 700,
    		color : "#f56954",
    		highlight : "#f56954",
    		label : "Missed"
    	}, {
    		value : 500,
    		color : "#00a65a",
    		highlight : "#00a65a",
    		label : "Outgoing"
    	}, {
    		value : 400,
    		color : "#f39c12",
    		highlight : "#f39c12",
    		label : "In-Comming"
    	}];
    	
    	PieData[0].value = missedCount;
    	PieData[1].value = outgoingCount;
    	PieData[2].value = incommingCount;
    	
    	var pieOptions = {
    		//Boolean - Whether we should show a stroke on each segment
    		segmentShowStroke : true,
    		//String - The colour of each segment stroke
    		segmentStrokeColor : "#fff",
    		//Number - The width of each segment stroke
    		segmentStrokeWidth : 2,
    		//Number - The percentage of the chart that we cut out of the middle
    		percentageInnerCutout : 50, // This is 0 for Pie charts
    		//Number - Amount of animation steps
    		animationSteps : 130,
    		//String - Animation easing effect
    		animationEasing : "easeOutBounce",
    		//Boolean - Whether we animate the rotation of the Doughnut
    		animateRotate : true,
    		//Boolean - Whether we animate scaling the Doughnut from the centre
    		animateScale : false,
    		//Boolean - whether to make the chart responsive to window resizing
    		responsive : true,
    		// Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
    		maintainAspectRatio : true,
    		//String - A legend template
    		legendTemplate : "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>"
    	};
    	//Create pie or douhnut chart
    	// You can switch between pie and douhnut using the method below.
    	pieChart.Doughnut(PieData, pieOptions);


    	
    });
	
}

function ajaxInitaiateCall(){
	
	var psfid=document.getElementById("psfQueueId").value;
	var urlpath="/CRE/postRequestFeedBack/"+psfid+""
	$.ajax({
        url: urlpath
    }).done(function(data) {
    	alert("success");
    	
    });
}

function ajaxRequestBarChartForCallsMadeCREMan(){
	//document.getElementById('showThebarChartCalls').style.display = "block";
	//var data=document.getElementById('callchartdata');
	
	var data1 = document.getElementById("callchartdata").value;
	var data2 = document.getElementById("callchartdata1").value;
	//alert("data1 :"+data1+"data2 : "+data2);
	//var data1Array=new Array();
	//data1Array=data.value.split(",")
	
	// var dataCall=data1.callCountList;
	 //var dataService=data2.serviceBookedCountList;
	var selectedDataUser=document.getElementById('selectedData').value;
	 var selectUser = document.getElementById('data[]');
	 var selectAgent ="";
		
		for (var i = 0; i < selectUser.length; i++) {
        if(selectUser.options[i].selected){
        if (selectAgent === "") {

            selectAgent = selectUser.options[i].value;
        } else {
            
            selectAgent = selectAgent + ","+ selectUser.options[i].value ;
        }
    }
    }
	 var temp = new Array();    	
 	temp = selectedDataUser.split(",");  
	
	var barChartCanvas = $("#barChartOfCalls").get(0).getContext("2d");
	var barChart = new Chart(barChartCanvas);
	var barChartData ={	
			
			labels:temp,
		    datasets: [
		        {
		        	label: "Call Made",
		            strokeColor: "#003300",
		            fillColor: "#33cc33",
		            pointColor:"#cc3399",
		            data: JSON.parse(data1)
		        },
		        {
		        	label: "Service Booked",
		            strokeColor: "#003300",
		            fillColor: "#990033",
		            pointColor:"#cc3399",
		            data: JSON.parse(data2)
		        }			        
		    ]
	};
	
	 	

	var barChartOptions = {
		//Boolean - Whether the scale should start at zero, or an order of magnitude down from the lowest value
		scaleBeginAtZero : true,
		//Boolean - Whether grid lines are shown across the chart
		scaleShowGridLines : true,
		//String - Colour of the grid lines
		scaleGridLineColor : "rgba(0,0,0,.05)",
		//Number - Width of the grid lines
		scaleGridLineWidth : 1,
		//Boolean - Whether to show horizontal lines (except X axis)
		scaleShowHorizontalLines : true,
		//Boolean - Whether to show vertical lines (except Y axis)
		scaleShowVerticalLines : true,
		//Boolean - If there is a stroke on each bar
		barShowStroke : true,
		//Number - Pixel width of the bar stroke
		barStrokeWidth : 2,
		//Number - Spacing between each of the X value sets
		barValueSpacing : 2,
		//Number - Spacing between data sets within X values
		barDatasetSpacing : 5,
		//String - A legend template
		legendTemplate : "<%if (label){%><%=label%>: <%}%><%= value %>kb",
		//Boolean - whether to make the chart responsive
		responsive : true,
		maintainAspectRatio : true
	};
	
	barChartOptions.datasetFill = false;
	barChart.Bar(barChartData, barChartOptions); 
	
}
//Pending Calls chart

function pendingCallByUserChartFunction(){
	
	var dataIndex1 = document.getElementById("indexBox1").value;	
	var myJsonString = JSON.parse(dataIndex1);
	 $("#schCount").html(myJsonString[0]);
     $("#schPending").html(myJsonString[1]);
     $("#serviceBookd").html(myJsonString[2]);
     $("#serviceBookdPer").html(myJsonString[3]);  
	
	
	
	var data1 = document.getElementById("callMadeCountPendingChart").value;
	var data2 = document.getElementById("pendingCountPendingChart").value;
	
	var selectedDataUser=document.getElementById('selectedData').value;	
	 var temp = new Array();    	
 	temp = selectedDataUser.split(",");  
	
	var barChartCanvas = $("#pendingCallsBarChartCREMan").get(0).getContext("2d");
	var barChart = new Chart(barChartCanvas);
	var barChartData ={	
			
			labels:temp,
		    datasets: [
		        {
		        	label: "Calls Made",
		            strokeColor: "#003300",
		            fillColor: "#33cc33",
		            pointColor:"#cc3399",
		            data: JSON.parse(data1)
		        },
		        {
		        	label: "Pending Calls",
		            strokeColor: "#003300",
		            fillColor: "#990033",
		            pointColor:"#cc3399",
		            data: JSON.parse(data2)
		        }			        
		    ]
	};
	
	 	

	var barChartOptions = {
		//Boolean - Whether the scale should start at zero, or an order of magnitude down from the lowest value
		scaleBeginAtZero : true,
		//Boolean - Whether grid lines are shown across the chart
		scaleShowGridLines : true,
		//String - Colour of the grid lines
		scaleGridLineColor : "rgba(0,0,0,.05)",
		//Number - Width of the grid lines
		scaleGridLineWidth : 1,
		//Boolean - Whether to show horizontal lines (except X axis)
		scaleShowHorizontalLines : true,
		//Boolean - Whether to show vertical lines (except Y axis)
		scaleShowVerticalLines : true,
		//Boolean - If there is a stroke on each bar
		barShowStroke : true,
		//Number - Pixel width of the bar stroke
		barStrokeWidth : 2,
		//Number - Spacing between each of the X value sets
		barValueSpacing : 2,
		//Number - Spacing between data sets within X values
		barDatasetSpacing : 5,
		//String - A legend template
		legendTemplate : "<ul ><li><Pending Calls</li><li><Calls Made</li></ul>",
		//Boolean - whether to make the chart responsive
		responsive : true,
		maintainAspectRatio : true,
		responsive : true,    		
		animation: true,
		   barValueSpacing : 5,
		   barDatasetSpacing : 1,
		   tooltipFillColor: "rgba(0,0,0,0.8)",
		multiTooltipTemplate: "<%= datasetLabel %> - <%= value %>"
	};
	
	barChartOptions.datasetFill = false;
	barChart.Bar(barChartData, barChartOptions); 
	
	
	
}


function followUpCallByUserChartFunction(){
	
	var dataIndex1 = document.getElementById("indexBox1").value;	
	var myJsonString = JSON.parse(dataIndex1);
	 $("#schCount").html(myJsonString[0]);
     $("#schPending").html(myJsonString[1]);
     $("#serviceBookd").html(myJsonString[2]);
     $("#serviceBookdPer").html(myJsonString[3]); 
     
	
	var data1 = document.getElementById("followupMadeCountChart").value;
	var data2 = document.getElementById("followUpDoneCountChart").value;
	
	var selectedDataUser=document.getElementById('selectedData').value;	
	 var temp = new Array();    	
 	temp = selectedDataUser.split(",");  
	
	var barChartCanvas = $("#followupCallsBarChartCREMan").get(0).getContext("2d");
	var barChart = new Chart(barChartCanvas);
	var barChartData ={	
			
			labels:temp,
		    datasets: [
		        {
		        	label: "FollowUp Required",
		            strokeColor: "#003300",
		            fillColor: "#33cc33",
		            pointColor:"#cc3399",
		            data: JSON.parse(data1)
		        },
		        {
		        	label: "FollowUp Done",
		            strokeColor: "#003300",
		            fillColor: "#990033",
		            pointColor:"#cc3399",
		            data: JSON.parse(data2)
		        }			        
		    ]
	};
	
	 	

	var barChartOptions = {
		//Boolean - Whether the scale should start at zero, or an order of magnitude down from the lowest value
		scaleBeginAtZero : true,
		//Boolean - Whether grid lines are shown across the chart
		scaleShowGridLines : true,
		//String - Colour of the grid lines
		scaleGridLineColor : "rgba(0,0,0,.05)",
		//Number - Width of the grid lines
		scaleGridLineWidth : 1,
		//Boolean - Whether to show horizontal lines (except X axis)
		scaleShowHorizontalLines : true,
		//Boolean - Whether to show vertical lines (except Y axis)
		scaleShowVerticalLines : true,
		//Boolean - If there is a stroke on each bar
		barShowStroke : true,
		//Number - Pixel width of the bar stroke
		barStrokeWidth : 2,
		//Number - Spacing between each of the X value sets
		barValueSpacing : 2,
		//Number - Spacing between data sets within X values
		barDatasetSpacing : 5,
		//String - A legend template
		legendTemplate : "<ul ><li><FollowUp Done</li><li><FollowUp Required</li></ul>",
		//Boolean - whether to make the chart responsive
		responsive : true,
		maintainAspectRatio : true,
		responsive : true,    		
		animation: true,
		   barValueSpacing : 5,
		   barDatasetSpacing : 1,
		   tooltipFillColor: "rgba(0,0,0,0.8)",
		multiTooltipTemplate: "<%= datasetLabel %> - <%= value %>"
	};
	
	barChartOptions.datasetFill = false;
	barChart.Bar(barChartData, barChartOptions); 
	
	
	
}


/*function ajaxRequestBarChartForCallsMadeCREMan(){
	document.getElementById('showThebarChartCalls').style.display = "block";
	var selectUser = document.getElementById('data[]');
	var selectAgent ="";
	
	for (var i=0; i < selectUser.length; i++) {
	  
	  if(i==(selectUser.length-1)){
			
		  selectAgent=selectAgent+selectUser.options[i].value;
		}
		else{
			
			selectAgent=selectAgent+selectUser.options[i].value+",";
		}
	}
	
	var a = document.getElementById("singleData");
	var selectType = a.options[a.selectedIndex].value;
	var urlPath="/CREManager/showChartOfCallsMAde/"+selectAgent+"/"+selectType+"";
	$.ajax({
        url: urlPath
    }).done(function(data) {
    	
    	var temp = new Array();    	
    	temp = selectAgent.split(",");    	
    	
    	 
    	 
    	 var dataCall=data.callCountList;
    	 var dataService=data.serviceBookedCountList;
    	
    
    	
    	var barChartCanvas = $("#barChartOfCalls").get(0).getContext("2d");
		var barChart = new Chart(barChartCanvas);
		var barChartData ={	
				
				labels:temp,
			    datasets: [
			        {
			        	label: "Call Made",
			            strokeColor: "#003300",
			            fillColor: "#33cc33",
			            pointColor:"#cc3399",
			            data: dataCall
			        },
			        {
			        	label: "Service Booked",
			            strokeColor: "#003300",
			            fillColor: "#990033",
			            pointColor:"#cc3399",
			            data: dataService
			        }			        
			    ]
		};
		
		 	
    
		var barChartOptions = {
			//Boolean - Whether the scale should start at zero, or an order of magnitude down from the lowest value
			scaleBeginAtZero : true,
			//Boolean - Whether grid lines are shown across the chart
			scaleShowGridLines : true,
			//String - Colour of the grid lines
			scaleGridLineColor : "rgba(0,0,0,.05)",
			//Number - Width of the grid lines
			scaleGridLineWidth : 1,
			//Boolean - Whether to show horizontal lines (except X axis)
			scaleShowHorizontalLines : true,
			//Boolean - Whether to show vertical lines (except Y axis)
			scaleShowVerticalLines : true,
			//Boolean - If there is a stroke on each bar
			barShowStroke : true,
			//Number - Pixel width of the bar stroke
			barStrokeWidth : 2,
			//Number - Spacing between each of the X value sets
			barValueSpacing : 2,
			//Number - Spacing between data sets within X values
			barDatasetSpacing : 5,
			//String - A legend template
			legendTemplate : "<%if (label){%><%=label%>: <%}%><%= value %>kb",
			//Boolean - whether to make the chart responsive
			responsive : true,
			maintainAspectRatio : true
		};
		
		barChartOptions.datasetFill = false;
		barChart.Bar(barChartData, barChartOptions); 
		
		
    });
	var legendHolder = document.createElement('div');
	legendHolder.innerHTML = bar.generateLegend();
	document.getElementById('legend').appendChild(legendHolder.firstChild);
	
}	
	*/
function ajaxRequestBarChartCREMan(){
	
	var data1 = document.getElementById("callTypechartdata").value;
	var data2 = document.getElementById("callTypechartdata1").value;
	var data3 = document.getElementById("callTypechartdata2").value;
	
	var selectedDataUser=document.getElementById('selectedData').value;
	 var selectUser = document.getElementById('data[]');
	 var selectAgent ="";
		
		for (var i=0; i < selectUser.length; i++) {
		  
		  if(i==(selectUser.length-1)){
				
			  selectAgent=selectAgent+selectUser.options[i].value;
			}
			else{
				
				selectAgent=selectAgent+selectUser.options[i].value+",";
			}
		}
	var temp = new Array();    	
	temp = selectedDataUser.split(",");
	
	var barChartCanvas = $("#barChart").get(0).getContext("2d");
	var barChart = new Chart(barChartCanvas);
	var barChartData ={	
			
			labels:temp,
		    datasets: [
		        {
		        	label:"Missed",
		            strokeColor: "#003300",
		            fillColor: "#33cc33",
		            pointColor:"#cc3399",
		            data: JSON.parse(data1)
		        },
		        {
		        	label:"OutGoing",
		            strokeColor: "#003300",
		            fillColor: "#ff0000",
		            pointColor:"#cc3399",
		            data: JSON.parse(data2)
		        },
		        {
		        	label:"Incoming",
		            strokeColor: "#003300",
		            fillColor: "#990033",
		            pointColor:"#cc3399",
		            data: JSON.parse(data3)
		        }
		    ]
	};
	
	 	

	var barChartOptions = {
		//Boolean - Whether the scale should start at zero, or an order of magnitude down from the lowest value
		scaleBeginAtZero : true,
		//Boolean - Whether grid lines are shown across the chart
		scaleShowGridLines : true,
		//String - Colour of the grid lines
		scaleGridLineColor : "rgba(0,0,0,.05)",
		//Number - Width of the grid lines
		scaleGridLineWidth : 1,
		//Boolean - Whether to show horizontal lines (except X axis)
		scaleShowHorizontalLines : true,
		//Boolean - Whether to show vertical lines (except Y axis)
		scaleShowVerticalLines : true,
		//Boolean - If there is a stroke on each bar
		barShowStroke : true,
		//Number - Pixel width of the bar stroke
		barStrokeWidth : 2,
		//Number - Spacing between each of the X value sets
		barValueSpacing : 2,
		//Number - Spacing between data sets within X values
		barDatasetSpacing : 5,
		//String - A legend template
		legendTemplate : "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].fillColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
		//Boolean - whether to make the chart responsive
		responsive : true,
		maintainAspectRatio : true
	};
	
	barChartOptions.datasetFill = false;
	barChart.Bar(barChartData, barChartOptions); 
	
	
}
	

/*function ajaxRequestBarChartCREMan(){
	document.getElementById('showThebarChart').style.display = "block";
	var selectUser = document.getElementById('data[]');
	var selectAgent ="";
	
	for (var i=0; i < selectUser.length; i++) {
	  
	  if(i==(selectUser.length-1)){
			
		  selectAgent=selectAgent+selectUser.options[i].value;
		}
		else{
			
			selectAgent=selectAgent+selectUser.options[i].value+",";
		}
	}
	
	var a = document.getElementById("singleData");
	var selectType = a.options[a.selectedIndex].value;
	var urlPath="/CREManager/showChart/"+selectAgent+"/"+selectType+"";
	$.ajax({
        url: urlPath
    }).done(function(data) {
    	
    	var temp = new Array();    	
    	temp = selectAgent.split(",");
    	var missedCount = new Array();
    	var outgoingCount = new Array();   
    	var incommingCount = new Array();     	
    	 
    	 
    	 var dataMiss=data.missedList;
    	 var dataOut=data.outGoingList;
    	 var dataInc=data.incomingList;
    
    	
    	var barChartCanvas = $("#barChart").get(0).getContext("2d");
		var barChart = new Chart(barChartCanvas);
		var barChartData ={	
				
				labels:temp,
			    datasets: [
			        {
			        	label:"Missed",
			            strokeColor: "#003300",
			            fillColor: "#33cc33",
			            pointColor:"#cc3399",
			            data: dataMiss
			        },
			        {
			        	label:"OutGoing",
			            strokeColor: "#003300",
			            fillColor: "#ff0000",
			            pointColor:"#cc3399",
			            data: dataOut
			        },
			        {
			        	label:"Incoming",
			            strokeColor: "#003300",
			            fillColor: "#990033",
			            pointColor:"#cc3399",
			            data: dataInc
			        }
			    ]
		};
		
		 	
    
		var barChartOptions = {
			//Boolean - Whether the scale should start at zero, or an order of magnitude down from the lowest value
			scaleBeginAtZero : true,
			//Boolean - Whether grid lines are shown across the chart
			scaleShowGridLines : true,
			//String - Colour of the grid lines
			scaleGridLineColor : "rgba(0,0,0,.05)",
			//Number - Width of the grid lines
			scaleGridLineWidth : 1,
			//Boolean - Whether to show horizontal lines (except X axis)
			scaleShowHorizontalLines : true,
			//Boolean - Whether to show vertical lines (except Y axis)
			scaleShowVerticalLines : true,
			//Boolean - If there is a stroke on each bar
			barShowStroke : true,
			//Number - Pixel width of the bar stroke
			barStrokeWidth : 2,
			//Number - Spacing between each of the X value sets
			barValueSpacing : 2,
			//Number - Spacing between data sets within X values
			barDatasetSpacing : 5,
			//String - A legend template
			legendTemplate : "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].fillColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
			//Boolean - whether to make the chart responsive
			responsive : true,
			maintainAspectRatio : true
		};
		
		barChartOptions.datasetFill = false;
		barChart.Bar(barChartData, barChartOptions);   	
		
    });
	
	
}*/

//validations



$(document).ready(function() {
	
	
	ajaxRequestForPopUpNotificationCRE();
    ajaxRequestForPsfPopupNotificationCRE();
    ajaxRequestForInsurancePopupNotificationCRE();
	
	//-------------
	//- PIE CHART -
	//-------------
	// Get context with jQuery - using jQuery's .get() method. for Sales Manager
	
	if($("#pieChartForSalesMan").length){
		setInterval(ajaxRequstPieChartCallTypeForSalesManager, 10000);
	}
	
	if($("#areaChartForSalesMan").length){
		setInterval(ajaxRequstAreaChartCallTypeForSalesMan, 10000);
	}
	
	//-------------
	//- PIE CHART -
	//-------------
	// Get context with jQuery - using jQuery's .get() method. for Sales Executive
	
	if($("#pieChartForSE").length){
		setInterval(ajaxRequstPieChartCallTypeForSalesExec, 10000);
	}
	
	if($("#areaChartForSE").length){
		setInterval(ajaxRequstAreaChartCallTypeForSalesExec, 10000);
	}
	
	
	
	//-------------
	//- PIE CHART -
	//-------------
	// Get context with jQuery - using jQuery's .get() method. for CRE
	
	if($("#pieChartForCRE").length){
		setInterval(ajaxRequstPieChartCallTypeForCRE, 10000);
	}
	
	if($("#areaChartForCRE").length){
		setInterval(ajaxRequestAreaChartForCRE, 10000);
		
	}
	
	
	//-------------
	//- PIE CHART -
	//-------------
	// Get context with jQuery - using jQuery's .get() method.
	if($("#pieChart").length){
		setInterval(ajaxRequstPieChartCallType, 10000);
	}		
	

	if($("#areaChart").length){
		setInterval(ajaxRequestAreaChartCREMan, 10000);
		
	}
	
	
	//-------------
	//- LINE CHART -
	//--------------
	
	if($("#lineChart").length){
		var lineChartCanvas = $("#lineChart").get(0).getContext("2d");
		var lineChart = new Chart(lineChartCanvas);
		var lineChartOptions = areaChartOptions;
		lineChartOptions.datasetFill = false;
		lineChart.Line(areaChartData, lineChartOptions);
	}
	
    $('.input-group input[required], .input-group textarea[required], .input-group select[required]').on('keyup change', function() {
		var $form = $(this).closest('form'),
            $group = $(this).closest('.input-group'),
			$addon = $group.find('.input-group-addon'),
			$icon = $addon.find('span'),
			state = false;
            
    	if (!$group.data('validate')) {
			state = $(this).val() ? true : false;
		}else if ($group.data('validate') == 'emailId') {;
			state = /^(([a-zA-Z]|[0-9])|([-]|[_]|[.]))+[@@](([a-zA-Z0-9])|([-])){2,63}[.](([a-zA-Z0-9]){2,63})+$/.test($(this).val());
		}else if($group.data('validate') == 'phoneNumber') {
			state = /^[(]{0,1}[0-9]{3}[)]{0,1}[-\s\.]{0,1}[0-9]{3}[-\s\.]{0,1}[0-9]{4}$/.test($(this).val());
		}else if($group.data('validate') == 'phoneIMEINo') {
			state = /^([0-9]*)(^\S+)$/.test($(this).val());
		}else if ($group.data('validate') == 'userName') {
			state = /^[_a-zA-Z0-9]+$/.test($(this).val());
		}else if ($group.data('validate') == 'password') {
			state = /^((?=\S*?[A-Z])(?=\S*?[a-z])(?=\S*?[0-9]).{6,})\S$/.test($(this).val());
		}

		if (state) {
				$addon.removeClass('danger');
				$addon.addClass('success');
				$icon.attr('class', 'glyphicon glyphicon-ok');
		}else{
				$addon.removeClass('success');
				$addon.addClass('danger');
				$icon.attr('class', 'glyphicon glyphicon-remove');
		}
        
        if ($form.find('.input-group-addon.danger').length == 0) {
            $form.find('[type="submit"]').prop('disabled', false);
        }else{
            $form.find('[type="submit"]').prop('disabled', true);
        }
	});
    
    $('.input-group input[required], .input-group textarea[required], .input-group select[required]').trigger('change');
    
    if($("#myModal").length){
    	$("#myModal").modal('show');
    }
   
    
    
});

//follow up notification if the follow up is missed

function ajaxRequestRemainderOfMissedFollowUps(){
    
     $.ajax({
		 url: "/CRE/ajax/getFollowUpRemainderOfMissedSchedules"
    }).done(function(data) {    	
    	
    }); 
    
    
}

//follow up notification before 5 min of follow up time

function ajaxRequestForPopUpNotificationBefore(){
    
   $.ajax({
		 url: "/CRE/ajax/getFollowUpNotificationBeforeTime"
    }).done(function(data) {    	
    	var follow=data.followUpTime;   	   	
    	if(data.followUpTime!=null){ 
    		
    		Lobibox.notify('info',{
       		 delay: false,    		
       		msg: 'Follow Up is scheduled After 5 min for : '+data.customerName,
       		
       		 
       		 
       	});	
    		
    	    	
    	}
    }); 
    
    
}


//follow up pop notification for disposition

function ajaxRequestForPopUpNotificationCRE(){	
	
$.ajax({
 url: "/CRE/ajax/getFollowUpNotificationOfToday"
    }).done(function(data) {
    	
    	$("#followUpCountOfToday").html(data.length);
    	 var existDate='';
    	 for (i = 0; i < data.length; i++) {
                tr = $('<tr/>');
                tr.append("<td>" + data[i].callInteraction_id + "</td>");
                tr.append("<td>" + data[i].dealerCode + "</td>");
                tr.append("<td>" + data[i].customerName + "</td>");
                tr.append("<td>" + data[i].followUpTime + "</td>");
                tr.append("<td>" + data[i].vehicle_vehicle_id + "</td>");
                tr.append("<td>" + data[i].customer_id + "</td>");
                $('#tblfubNote').append(tr);
            
    	 if(data[i].followUpTime!=null){
    	console.log("existDate :"+existDate+" data[i].followUpTime "+data[i].followUpTime);
    	if(existDate != data[i].followUpTime ){
    		 
    	 var now = new Date();
    	 
    	 console.log("not same both times"+data[i].followUpTime);
    	 var fupTime = toDate(data[i].followUpTime,'H:MM')
    	 var millisTill10 = new Date(now.getFullYear(), now.getMonth(), now.getDate(), fupTime.getHours(), fupTime.getMinutes(), 0, 0) - now;
    	 console.log(" millisTill10 "+millisTill10+" i is "+i);
    	 if(millisTill10 > 0){
    		
    	 setTimeout(function(){showNotifyshowNotify();}, millisTill10);

    	 }
    	 
    	 }else{    		
    		 console.log("same as previous  both times");
    		 
    		 var now = new Date();
    		 now.setMinutes(now.getMinutes()+3);    		 
    		 console.log("the +3 time is :"+now+" data[i].followUpTime is "+data[i].followUpTime);
    		 
        	 var fupTime = toDate(data[i].followUpTime,'H:MM')
        	 var millisTill10 = new Date(now.getFullYear(), now.getMonth(), now.getDate(), fupTime.getHours(), fupTime.getMinutes(), 0, 0) - now;
        	 
        	 console.log(" millisTill10 "+millisTill10+" i is "+i);
        	 
        	 if(millisTill10 > 0){
        	 setTimeout(function(){showNotify();},millisTill10);
        	 } 	 
        	 
    		 
    	 }
    	
    	existDate=data[i].followUpTime;
    }
    	} 	
    	});
}


//PSF Notify

function ajaxRequestForPsfPopupNotificationCRE(){
	//alert("ajaxRequestForPsfPopupNotificationCRE");

$.ajax({
	url:"/CRE/ajax/getPSFFollowupNotificationOfToday"
}).done(function(data){
	console.log(" data.length :" +data.length);
	 var existDate='';
	for(i=0;i<data.length;i++){
		tr = $('<tr/>');
		tr.append("<td>" +data[i].customer_id+"</td>");
		tr.append("<td>" +data[i].customer_name+"</td>");
		tr.append("<td>" +data[i].vehicle_id+"</td>");
		tr.append("<td>" +data[i].callinteraction_id+"</td>");
		tr.append("<td>" +data[i].psfFollowUpTime+"</td>");
		tr.append("<td>"+data[i].campaign_id+"<td>");
		$('#tblforfollowuptemp').append(tr);
		
	if(data[i].psfFollowUpTime!=null){
		 
		console.log("existDate :"+existDate+" data[i].psfFollowUpTime "+data[i].psfFollowUpTime);
    	if(existDate != data[i].psfFollowUpTime ){
    		 
    	 var now = new Date();
    	 
    	 console.log("not same both times"+data[i].psfFollowUpTime);
    	 var fupTime = toDate(data[i].psfFollowUpTime,'H:MM')
    	 var millisTill10 = new Date(now.getFullYear(), now.getMonth(), now.getDate(), fupTime.getHours(), fupTime.getMinutes(), 0, 0) - now;
    	 console.log(" millisTill10 "+millisTill10+" i is "+i);
    	 if(millisTill10 > 0){
    		
    	 setTimeout(function(){showPSFNotify();}, millisTill10);

    	 }
    	 
    	 }else{    		
    		 console.log("same as previous  both times");
    		 
    		 var now = new Date();
    		 now.setMinutes(now.getMinutes()+3);    		 
    		 console.log("the +3 time is :"+now+" data[i].psfFollowUpTime is "+data[i].psfFollowUpTime);
    		 
        	 var fupTime = toDate(data[i].psfFollowUpTime,'H:MM')
        	 var millisTill10 = new Date(now.getFullYear(), now.getMonth(), now.getDate(), fupTime.getHours(), fupTime.getMinutes(), 0, 0) - now;
        	 
        	 console.log(" millisTill10 "+millisTill10+" i is "+i);
        	 
        	 if(millisTill10 > 0){
        	 setTimeout(function(){showPSFNotify();},millisTill10);
        	 } 	 
        	 
    		 
    	 }
    	
    	existDate=data[i].psfFollowUpTime;
	}
	}
});
}

function showPSFNotify(){
	//alert("showPSFNotify");
	
	var d = new Date();
	    var h = addZero(d.getHours());
	    var m = addZero(d.getMinutes());
	 
	    var fupTime = h + ":" + m ;
	   
	    var table = document.getElementById("tblforfollowuptemp");
	    for (var i = 0, row; row = table.rows[i]; i++) {
	    	//alert("psf"+row.cells[3].innerHTML);
	    	//alert("psf"+fupTime);
	    	
	    	var	onClkUrl;
	    	if(row.cells[5].innerHTML === '4'){
       	 		onClkUrl= "/CRE/psfDispo/"+row.cells[0].innerHTML+"/"+row.cells[2].innerHTML+"/"+row.cells[3].innerHTML+"/1/4";
       	 	}else if(row.cells[5].innerHTML === "5"){
       	 		onClkUrl=	"/CRE/psfDispo/"+row.cells[0].innerHTML+"/"+row.cells[2].innerHTML+"/"+row.cells[3].innerHTML+"/1/5";
       	 	}else if(row.cells[5].innerHTML === "6"){
       	 		onClkUrl=	"/CRE/psfDispo/"+row.cells[0].innerHTML+"/"+row.cells[2].innerHTML+"/"+row.cells[3].innerHTML+"/1/6";
       	 	}else if(row.cells[5].innerHTML === "7"){
       	 		onClkUrl=	"/CRE/psfDispo/"+row.cells[0].innerHTML+"/"+row.cells[2].innerHTML+"/"+row.cells[3].innerHTML+"/1/7";
       	 	}
	    	
	    	 if(fupTime === row.cells[4].innerHTML){
	    	 Lobibox_notify('info',{
	    	 delay: 24*5000,    	
	       	 msg: 'PSF Follow Up is required for '+row.cells[1].innerHTML+"",
	    	 onClickUrl: onClkUrl
	       	 	});	
	    	}
	    	} 
	    }


//SERviceRemainder notify
function showNotify(){
var d = new Date();
    var h = addZero(d.getHours());
    var m = addZero(d.getMinutes());
 
    var fupTime = h + ":" + m ;
    
    var table = document.getElementById("tblfubNote");
    for (var i = 0, row; row = table.rows[i]; i++) {
    	
    	//alert("service : "+fupTime+" followuptime"+row.cells[3].innerHTML)
    	 if(fupTime === row.cells[3].innerHTML){
    	 Lobibox_notify('info',{
    	 delay: 24*5000,      	
       	 msg: 'Service Follow Up is required for '+row.cells[2].innerHTML,
       	 onClickUrl: "/CRE/getDispositionFormPage/2/"+row.cells[5].innerHTML+"/"+row.cells[4].innerHTML+"/service"
    	});	
    	} 
    }
    
//    
//	for(i=0;i<FeedbackNotify.length;i++){
//	 if(fupTime === FeedbackNotify[i].followUpTime){
//	 Lobibox_notify('info',{
//	 delay: false,    	
//   	 msg: 'Follow Up is required for'+FeedbackNotify[i].customerName,
//   	 onClickUrl: "/"+FeedbackNotify[i].dealerCode+"/CRE/getFollowUpCallDispositionPage/"+FeedbackNotify[i].id+""
//	});
//	
//	}
//	}
}
	function addZero(i) {
	    if (i < 10) {
	        i = "0" + i;
	    }
	    return i;
	}
	function toDate(dStr,format) {
	 var now = new Date();
	 if (format == "H:MM") {
	   now.setHours(dStr.substr(0,dStr.indexOf(":")));
	   now.setMinutes(dStr.substr(dStr.indexOf(":")+1));
	   now.setSeconds(0);
	   return now;
	 }else 
	  return "Invalid Format";
	}

function changeFunction(){
	
	alert("Password Changed Successfully");
	
}

$(function() {
	  $( ".datepickerPrevious" ).datepicker({
	  autoclose: true,
	  dateFormat: 'yy-mm-dd',
	  maxDate: new Date(),
	  onSelect: function (date) {
	  $('.rangedatepicker').datepicker( "destroy" );
	  $('.rangedatepicker').datepicker({
	  autoclose: true,
	  dateFormat: 'yy-mm-dd',
	  maxDate: new Date(),
	  minDate: $('.datepickerPrevious').datepicker('getDate')
	  });
	  	var dt1 = $('.datepickerPrevious').datepicker('getDate');
	            var dt2 = $('.rangedatepicker').datepicker('getDate');
	            if (dt2 <= dt1) {
	                var minDate = $('#dt2').datepicker('option', 'minDate');
	                $('.rangedatepicker').datepicker('setDate', null);
	            }
	  }
	  });
});

	$(function() {
	  $( ".rangedatepicker" ).datepicker({
	  autoclose: true,
	  dateFormat: 'yy-mm-dd',
	  maxDate: new Date()
	  });
	 });
	
	$(function() {
		  $( ".datepickerAfter" ).datepicker({
		  autoclose: true,
		  dateFormat: 'yy-mm-dd',
		  minDate: new Date()
		  });
		 });

jQuery(document).ready(function($) {
	  $('.datepicker').datepicker({
		  
	     autoclose: true,
	     dateFormat: 'yy-mm-dd',
	     
	     onSelect: function (date) {
	   	  $('.range1datepicker').datepicker( "destroy" );
	   	  $('.range1datepicker').datepicker({
	   	  autoclose: true,
	   	  dateFormat: 'yy-mm-dd',	   	  
	   	  minDate: $('.datepicker').datepicker('getDate')
	   	  });
	   	  	var dt1 = $('.datepicker').datepicker('getDate');
	   	            var dt2 = $('.range1datepicker').datepicker('getDate');
	   	            if (dt2 <= dt1) {
	   	                var minDate = $('#dt2').datepicker('option', 'minDate');
	   	                $('.range1datepicker').datepicker('setDate', null);
	   	            }
	   	  }
	    	 
	    	 
	    	 
	  });
});


jQuery(document).ready(function($) {
	  $('.datepickerFilter').datepicker({
		  
	     autoclose: true,
	     dateFormat: 'yy-mm-dd',
	     
	   	  autoclose: true,   	  
	    	 
	    	 
	    	 
	  });
});

jQuery(document).ready(function($) {
	  $('.datepickerFilterDefault').datepicker({
		  
	     autoclose: true,
	     dateFormat: 'yy-mm-dd',
	     
	   	  autoclose: true,   	  
	    	 
	    	 
	    	 
	  }).datepicker("setDate", new Date());
});

function GetFormattedDate() {
    var todayTime = new Date();
   // console.log("todayTime"+todayTime);
    var month = todayTime.getMonth();
    
    var monthis=month+1;
    
    var day = todayTime.getDate();
    var year = todayTime .getFullYear();
   // console.log("day"+day)
   // console.log("monthis"+monthis)
    return day + "/" + monthis + "/" + year;
}

/*jQuery(document).ready(function($) {
var input = $('.single-input').clockpicker({
placement: 'top',
align: 'left',
autoclose: true,
'default': 'now'
});

});*/



  
function filterCallLogBasedOnStatus(){


	var selectedValue=document.getElementById("statusId1");
	var selected=selectedValue.options[selectedValue.selectedIndex].value;
	
	if(selected=="Already Serviced - Others"){
		
		document.getElementById("servedOthers").style.display = "block";
		
	}else{
		
		document.getElementById("servedOthers").style.display = "none";
	}
	
	
	
	var urlDisposition="/CRE/getFilterDataCallDispositionPageForSNR/"+selected+"";
		$.ajax({
			
			url:urlDisposition		
			
		}).done(function(data) {    	
	    	 
			//alert("success");
			
			if(data!=null){	
				
				//alert("success");
				
				var tableHeaderRowCount = 1;
				var table = document.getElementById('dataTables-example2');
				var rowCount = table.rows.length;
				for (var i = tableHeaderRowCount; i < rowCount; i++) {
	    		table.deleteRow(tableHeaderRowCount);
				}
				

				for(i=0;i<data.length;i++){

					tr = $('<tr/>');
					 tr.append('<td><a href="/' + data[i].dealerCode + '/CRE/toeditserviceNotRequiredcalls/'  + data[i].id +'"><i class="fa fa-pencil-square-o" data-toggle="tooltip" title="edit"></i></a></td>');
					 tr.append("<td>" + data[i].customerName + "</td>");
	 		        tr.append("<td>" + data[i].customerPhone + "</td>");
	 		       // tr.append('<td><i class="fa fa-phone-square" onclick="callfunctionDisposition(' + data[i].customerPhone + ',' + data[i].id + ',' + data[i].dealerCode + ')"  aria-hidden="true" data-toggle="tooltip"  title="Make Call" style="font-size:30px;color:green;"></i></td>');
	 		       
	 		      // tr.append('<td><a href="#" onclick="callfunctionDisposition('+data[i].customerPhone.trim() +','+ data[i].id +','+ data[i].dealerCode +')"><i class="fa fa-phone-square"  aria-hidden="true" data-toggle="tooltip"  title="Make Call" style="font-size:30px;color:green;"></i></a></td>');
	 		       tr.append('<td><a href=# onclick=\"callfunctionDisposition(\''+ data[i].customerPhone + '\',\''+ data[i].id + '\',\'' + data[i].dealerCode + '\')\"><i class="fa fa-phone-square"  aria-hidden="true" data-toggle="tooltip"  title="Make Call" style="font-size:30px;color:green;"></i></a></td>');
	 		       
	 		        tr.append("<td>" + data[i].vehicalRegNo + "</td>");
	 		        tr.append("<td>" + data[i].nextServiceDue + "</td>");
	 		       //tr.append("<td>" + data[i].status + "</td>");
	 		        
	 		       tr.append("<td><span class=\"label label-warning\">" 
			                 + data[i].status + "</span></td>");
	 		       
	 		      tr.append("<td>" + data[i].noServiceReason + "</td>");

	 		      tr.append('<td><a href="/' + data[i].dealerCode + '/CRE/getFollowUpCallDispositionPage/'  + data[i].id +'"><i class="fa fa-pencil-square" data-toggel="tooltip" title="Disposition" style="font-size:30px;color:#DD4B39;"></i></a></td>');
	 		     
	 		      $('#dataTables-example2').append(tr);	

					}



				
				}
		
                
                  
			
	    	
    	  	
	    	
	    	
	    });





	}


function filterCAllLogBasedOnAlreadyServed(){
	
	var selectedValue=document.getElementById("statusId2");
	var selected=selectedValue.options[selectedValue.selectedIndex].value;
	
	var urlDisposition="/CRE/getFilterDataCallDispositionPageAlreadyServedSNR/"+selected+"";
	
	$.ajax({
		
		url:urlDisposition		
		
	}).done(function(data) {    	
    	 
		//alert("success");
		
		if(data!=null){	
			
			//alert("success");
			
			var tableHeaderRowCount = 1;
			var table = document.getElementById('dataTables-example2');
			var rowCount = table.rows.length;
			for (var i = tableHeaderRowCount; i < rowCount; i++) {
    		table.deleteRow(tableHeaderRowCount);
			}
			

			for(i=0;i<data.length;i++){

				tr = $('<tr/>');
				 tr.append('<td><a href="/' + data[i].dealerCode + '/CRE/toeditserviceNotRequiredcalls/'  + data[i].id +'"><i class="fa fa-pencil-square-o" data-toggle="tooltip" title="edit"></i></a></td>');
				 tr.append("<td>" + data[i].customerName + "</td>");
 		        tr.append("<td>" + data[i].customerPhone + "</td>");
 		       // tr.append('<td><i class="fa fa-phone-square" onclick="callfunctionDisposition(' + data[i].customerPhone + ',' + data[i].id + ',' + data[i].dealerCode + ')"  aria-hidden="true" data-toggle="tooltip"  title="Make Call" style="font-size:30px;color:green;"></i></td>');
 		       
 		      // tr.append('<td><a href="#" onclick="callfunctionDisposition(' + data[i].customerPhone + ',' + data[i].id + ',' + data[i].dealerCode + ')"><i class="fa fa-phone-square"  aria-hidden="true" data-toggle="tooltip"  title="Make Call" style="font-size:30px;color:green;"></i></a></td>');
 		       tr.append('<td><a href=# onclick=\"callfunctionDisposition(\''+ data[i].customerPhone + '\',\''+ data[i].id + '\',\'' + data[i].dealerCode + '\')\"><i class="fa fa-phone-square"  aria-hidden="true" data-toggle="tooltip"  title="Make Call" style="font-size:30px;color:green;"></i></a></td>');
 		        tr.append("<td>" + data[i].vehicalRegNo + "</td>");
 		        tr.append("<td>" + data[i].nextServiceDue + "</td>");
 		      // tr.append("<td>" + data[i].status + "</td>");
 		       tr.append("<td><span class=\"label label-warning\">" 
		                 + data[i].status + "</span></td>");
 		       
 		      tr.append("<td>" + data[i].noServiceReason + "</td>");

 		      tr.append('<td><a href="/' + data[i].dealerCode + '/CRE/getFollowUpCallDispositionPage/'  + data[i].id +'"><i class="fa fa-pencil-square" data-toggel="tooltip" title="Disposition" style="font-size:30px;color:#DD4B39;"></i></a></td>');
 		     
 		      $('#dataTables-example2').append(tr);	

				}



			
			}
		
		
    	
	  	
    	
    	
    });
	
}

function filterTableBasedOnSelection(){

	var selectedValue=document.getElementById("singleDataNonContacts");
	var selected=selectedValue.options[selectedValue.selectedIndex].value;
	
	//alert(selected);

	var urlDisposition="/CRE/getFilterDataCallDispositionPage/"+selected+"";
	$.ajax({
		
		url:urlDisposition 
		
	}).done(function(data){

		if(data!=null){	
								
		//alert("success");
		
		var tableHeaderRowCount = 1;
		var table = document.getElementById('dataTables-example1');
		var rowCount = table.rows.length;
		for (var i = tableHeaderRowCount; i < rowCount; i++) {
		table.deleteRow(tableHeaderRowCount);
		}
		

		for(i=0;i<data.length;i++){

			tr = $('<tr/>');

			 tr.append('<td><a href="/' + data[i].dealerCode + '/CRE/toeditcallLog/'  + data[i].id +'"><i class="fa fa-pencil-square-o" data-toggle="tooltip" title="edit"></i></a></td>');
		     
			  tr.append("<td>" + data[i].customerName + "</td>");
		        tr.append("<td>" + data[i].customerPhone + "</td>");
		       // tr.append('<td><i class="fa fa-phone-square" onclick="callfunctionDisposition(' + data[i].customerPhone + ',' + data[i].id + ',' + data[i].dealerCode + ')"  aria-hidden="true" data-toggle="tooltip"  title="Make Call" style="font-size:30px;color:green;"></i></td>');
		       
		       tr.append('<td><a href=# onclick=\"callfunctionDisposition(\''+ data[i].customerPhone + '\',\''+ data[i].id + '\',\'' + data[i].dealerCode + '\')\"><i class="fa fa-phone-square"  aria-hidden="true" data-toggle="tooltip"  title="Make Call" style="font-size:30px;color:green;"></i></a></td>');
		        
		        //tr.append('<td><span onclick="callfunctionDisposition(' + data[i].customerPhone + ',' + data[i].id + ',' + data[i].dealerCode + ')"><i class="fa fa-phone-square"  aria-hidden="true" data-toggle="tooltip"  title="Make Call" style="font-size:30px;color:green;"></i></span></td>');
		       
		        tr.append("<td>" + data[i].vehicalRegNo + "</td>");
		        tr.append("<td>" + data[i].nextServiceDue + "</td>");
		       //tr.append("<td>" + data[i].status + "</td>");
		      		        
		        tr.append("<td><span class=\"label label-warning\">" 
		                 + data[i].status + "</span></td>");


		      tr.append('<td><a href="/' + data[i].dealerCode + '/CRE/getFollowUpCallDispositionPage/'  + data[i].id +'"><i class="fa fa-pencil-square" data-toggel="tooltip" title="Disposition" style="font-size:30px;color:#DD4B39;"></i></a></td>');
		     
		      $('#dataTables-example1').append(tr);	

			}



		
		}
	});

	}




//CRE Man Filters


function filterTableBasedOnSelectionForCREMan(){
	
	//var selectCREValue=document.getElementById("selectedCRES");
	//var selectedData=selectCREValue.options[selectCREValue.selectedIndex].value;
	
	var selectAgent = document.getElementById('selectedCRES').value;	
	var selectedValue=document.getElementById("nonContactCRESelection");
	var selected=selectedValue.options[selectedValue.selectedIndex].value;
	
	if(selectAgent==""){
		
		selectAgent="selectedAll";
		
	}
	
	
	//alert(selected);

	var urlDisposition="/CREManger/getFilterDataCallDispositionPage/"+selected+"/"+selectAgent+"";
	$.ajax({
		
		url:urlDisposition 
		
	}).done(function(data){

		if(data!=null){	
			
			//alert("success");
			
			var tableHeaderRowCount = 1;
			var table = document.getElementById('dataTables-example10');
			var rowCount = table.rows.length;
			for (var i = tableHeaderRowCount; i < rowCount; i++) {
			table.deleteRow(tableHeaderRowCount);
			}
			

			for(i=0;i<data.length;i++){

				tr = $('<tr/>');
				tr.append("<td>" + data[i].agentName + "</td>");
				 tr.append("<td>" + data[i].customerName + "</td>");
			     tr.append("<td>" + data[i].customerPhone + "</td>");
			     tr.append("<td>" + data[i].vehicalRegNo + "</td>");
			     tr.append("<td>" + data[i].nextServiceDue + "</td>");
			     tr.append("<td><span class=\"label label-warning\">" 
			                 + data[i].status + "</span></td>");
  
			      $('#dataTables-example10').append(tr);	

				}



			
			}
	});
	
	
}


//Service booked N-1 th dayand Nth day

function tabDisplayBasedOnRole(){
	
	 var username=document.getElementById('user').value;
	 
	  	var urlLink="/CRE/chasserExist/" + username + ""; 
	    $.ajax({
	        url: urlLink

	    }).done(function (data) {
	    	
	    	if(data[0] == true && data[1] == true){

	    		document.getElementById("sch").style.display="block";
	    		document.getElementById("followUp").style.display="block";
	    		document.getElementById("SB").style.display="block";
	    		document.getElementById("SNR").style.display="block";
	    		document.getElementById("NC").style.display="block";
	    		document.getElementById("DB").style.display="none"; 
	    		document.getElementById("sbn-1").style.display="block";
	    		document.getElementById("sbn").style.display="block";
	    		
	    	}
	    	if(data[1] == true && data[0] == false){
	    		  
	    		document.getElementById("sch").style.display="block";
	    		document.getElementById("followUp").style.display="block";
	    		document.getElementById("SB").style.display="block";
	    		document.getElementById("SNR").style.display="block";
	    		document.getElementById("NC").style.display="block";
	    		document.getElementById("DB").style.display="none";
	    		document.getElementById("sbn-1").style.display="none";
	    		document.getElementById("sbn").style.display="none";
	    		
	    
		    	
	    	}
	    	if(data[1] == false && data[0] == true){
	    		
	    		
	    		document.getElementById("sch").style.display="none";
	    		document.getElementById("followUp").style.display="none";
	    		document.getElementById("SB").style.display="none";
	    		document.getElementById("SNR").style.display="none";
	    		document.getElementById("NC").style.display="none";
	    		document.getElementById("DB").style.display="none";
	    		document.getElementById("sbn-1").style.display="block";
	    		document.getElementById("sbn").style.display="block";
	    			
		    	
	    	}
	 	   
	    });
	
}
function ajaxRequestDataOFCREIndexPageWithTab(){
	tabDisplayBasedOnRole();
	//assignedInteractionData();
    var tab_a=document.getElementById('toActivateTab1').value;
    var tab_b=document.getElementById('toActivateTab2').value;
    var tab_c=document.getElementById('toActivateTab3').value;
    var tab_d=document.getElementById('toActivateTab4').value;
    var tab_e=document.getElementById('toActivateTab5').value;
    var tab_f=document.getElementById('toActivateTab6').value;
    var tab_g=document.getElementById('toActivateTab7').value;
    var tab_h=document.getElementById('toActivateTab8').value;
    var tab_i=document.getElementById('toActivateTab9').value;
	//alert(tab_a.slice(0,2));
    if(tab_a.slice(0,2)=="in"){

	if($("#Tab1").length){
    
    var url = "#home";
    $('.nav a[href="'+url+'"]').parent().addClass('active');
    assignedInteractionData();
	}
	
    }else if(tab_b.slice(0,2)=="in"){

    	if($("#Tab2").length){
            
            var url = "#profile";
            $('.nav a[href="'+url+'"]').parent().addClass('active');
            ajaxCallForFollowUpRequiredServer();
        	}


        }else if(tab_c.slice(0,2)=="in"){

        	if($("#Tab3").length){
                
                var url = "#messages";
                $('.nav a[href="'+url+'"]').parent().addClass('active');
                ajaxCallForServiceBookedServer();
            	}
        	

            }else if(tab_d.slice(0,2)=="in"){

            	if($("#Tab4").length){
                    
                    var url = "#settings";
                    $('.nav a[href="'+url+'"]').parent().addClass('active');
                    ajaxCallForServiceNotRequiredServer();
                }

                }else if(tab_e.slice(0,2)=="in"){

                	if($("#Tab5").length){
                        
                        var url = "#nonContacts";
                        $('.nav a[href="'+url+'"]').parent().addClass('active');
                        ajaxCallForNonContactsServer();
                        
                    	}

                    }else if(tab_f.slice(0,2)=="in"){

                    	if($("#Tab7").length){
                            
                            var url = "#sbn-1day";
                            $('.nav a[href="'+url+'"]').parent().addClass('active');
                            ajaxCallForServiceBookedServerNminusOne();
                            
                        	}

                        }
                    else if(tab_g.slice(0,2)=="in"){

                    	if($("#Tab8").length){
                            
                            var url = "#sbnthday";
                            $('.nav a[href="'+url+'"]').parent().addClass('active');
                            ajaxCallForserviceBookedServerDataNthDay();
                            
                        	}

                        }
                    else if(tab_h.slice(0,2)=="in"){

                    	if($("#Tab9").length){
                            
                            var url = "#nfspms";
                            $('.nav a[href="'+url+'"]').parent().addClass('active');
                            ajaxCallForserviceBookedNonFSPMS();
                            
                        	}

                        }
                    else if(tab_i.slice(0,2)=="in"){

                    	if($("#Tab10").length){
                            
                            var url = "#futureFollowup";
                            $('.nav a[href="'+url+'"]').parent().addClass('active');
                            ajaxCallForFutureFollowupData();
                            
                        	}

                        }
    
}





function ajaxRequestDataOFInsuranceIndexPageWithTab(){
	ajaxInsuranceRemainder();
	
    var tab_a=document.getElementById('toActivateInsTab1').value;
    var tab_b=document.getElementById('toActivateInsTab2').value;
    var tab_c=document.getElementById('toActivateInsTab3').value;
    var tab_d=document.getElementById('toActivateInsTab4').value;
    var tab_e=document.getElementById('toActivateInsTab5').value;
	//alert(tab_a.slice(0,2));
    var tab_f=document.getElementById('toActivateInsTab6').value;
    var tab_g=document.getElementById('toActivateInsTab7').value;
    var tab_h=document.getElementById('toActivateInsTab8').value;
    if(tab_a.slice(0,2)=="in"){

	if($("#TabIns1").length){
    
    var url = "#homeIns";
    $('.nav a[href="'+url+'"]').parent().addClass('active');
	}
	
    }else if(tab_b.slice(0,2)=="in"){

    	if($("#TabIns2").length){
            
            var url = "#profileIns";
            $('.nav a[href="'+url+'"]').parent().addClass('active');
            ajaxInsuranceDisposition('4');
        	}


        }else if(tab_c.slice(0,2)=="in"){

        	if($("#TabIns3").length){
                
                var url = "#messagesIns";
                $('.nav a[href="'+url+'"]').parent().addClass('active');
                ajaxInsuranceDisposition('25');
            	}
        	

            }else if(tab_d.slice(0,2)=="in"){

            	if($("#TabIns4").length){
                    
                    var url = "#settingsIns";
                    $('.nav a[href="'+url+'"]').parent().addClass('active');
                    ajaxInsuranceDisposition('26');
                }

                }else if(tab_e.slice(0,2)=="in"){

                	if($("#TabIns5").length){
                        
                        var url = "#nonContactsIns";
                        $('.nav a[href="'+url+'"]').parent().addClass('active');
                        ajaxInsuranceNonContactDispo('1');
                        
                    	}

                    }
                else if(tab_f.slice(0,2)=="in"){

                	if($("#TabIns6").length){
                        
                        var url = "#sbn-1day";
                        $('.nav a[href="'+url+'"]').parent().addClass('active');
                        ajaxCallForInsNminusOne();
                        
                    	}

                    }
                else if(tab_g.slice(0,2)=="in"){

                	if($("#TabIns7").length){
                        
                        var url = "#sbnthday";
                        $('.nav a[href="'+url+'"]').parent().addClass('active');
                        ajaxCallForInsNthDay();
                        
                    	}

                    }
                else if(tab_h.slice(0,2)=="in"){

                	if($("#TabIns8").length){
                        
                        var url = "#futureIns";
                        $('.nav a[href="'+url+'"]').parent().addClass('active');
                        ajaxFutureFollowupDispo();
                        
                    	}

                    }
}

function ajaxRequestPSFDataPageWithTab(){
		
	assignedInteractionPSFData("psf6thday");
	
}

function ajaxRequestPSFDataPageWithTabNextDay(){
	
	assignedInteractionPSFData("psf1stday");
}

function ajaxRequestPSFDataPageWithTab4thDay(){
	
	assignedInteractionPSFData("psf4thday");
}

function ajaxRequestPSFDataPageWithTabFor15th(){
	

	assignedInteractionPSFData("psf15thday");
}

function ajaxRequestPSFDataPageWithTabFor30th(){
	

	assignedInteractionPSFData("psf30thday");
	
}

function ajaxRequestPSFDataPageWithTabFor3rd(){
	
	
		assignedInteractionPSFData("psf3rdday");
		
	}
//Insurance Agent


function ajaxAutoSASelectionIn(selectedDate,insurAgent){
	//alert("ajaxAutoSASelectionIn");
	
	var date = document.getElementById(selectedDate).value;
	
	var preSaDetails = document.getElementById('preSaDetailsIns');
	var newSaDetails = document.getElementById('newSaDetailsIns');
	$('#'+insurAgent+' option').remove();
	$('#serviceAdvisorTempIns option').remove();
	$.ajax({
		url:"/CRE/callDispositionPageIns/"+date
	}).done(function(data){
		var saDetails="";
		var i=0;
		$.each(data, function(arrayId,sa) {
			saDetails= sa.advisorId + "," + sa.advisorName + "," + sa.priority + "," + sa.date;
		            $("#"+insurAgent+"").append('<option value=\'' + sa.advisorId + '\'>' + sa.advisorName + '</option>');
		            $("#serviceAdvisorTempIns").append('<option value=\'' + saDetails + '\'>' + saDetails + '</option>');
		            if(i===0)
		            	{
		            preSaDetails.value=saDetails;
		            newSaDetails.value=saDetails;
		            	}
		            i++;
		});
	});
	return false;
	
}



function ajaxAutoSAManualchangeIns(selectedDate,serviceAdv){
	//alert("ajaxAutoSAManualchangeIns");
	var serviceAdvData = document.getElementById(serviceAdv).value;
	
	var date = document.getElementById(selectedDate).value;
	
	var preSaDetails = document.getElementById('preSaDetailsIns');
	var newSaDetails = document.getElementById('newSaDetailsIns');
	if(preSaDetails.value === "" || preSaDetails.value === ''){
		preSaDetails.value = "NA";
	}
	console.log("1");
	newSaDetails.value = $('#serviceAdvisorTempIns option').eq($("#"+serviceAdv+"").prop('selectedIndex')).val()
	$.ajax({
	
		url:"/CRE/callDispositionPage/"+date+"/"+preSaDetails.value+"/"+newSaDetails.value
	}).done(function(data){
		preSaDetails.value = newSaDetails.value;
		newSaDetails.value = newSaDetails.value;
	});
}

//Service Advisor

function ajaxAutoSASelection(workshopIdSelect,selectedDate,serviceAdv){
	
	
	var date = document.getElementById(selectedDate).value;
	var workshopId = document.getElementById(workshopIdSelect).value;
	var preSaDetails = document.getElementById('preSaDetails');
	var newSaDetails = document.getElementById('newSaDetails');
	$('#'+serviceAdv+' option').remove();
	$('#serviceAdvisorTemp option').remove();
	console.log("2");
	$.ajax({
		
		url:"/CRE/callDispositionPage/" + workshopId +"/"+date
	}).done(function(data){
		var saDetails="";
		var i=0;
		$.each(data, function(arrayId,sa) {
			saDetails= sa.advisorId + "," + sa.advisorName + "," + sa.priority + "," + sa.date + "," + sa.workshopId;
		            $("#"+serviceAdv+"").append('<option value=\'' + sa.advisorId + '\'>' + sa.advisorName + '</option>');
		            $("#serviceAdvisorTemp").append('<option value=\'' + saDetails + '\'>' + saDetails + '</option>');
		            if(i===0)
		            	{
		            preSaDetails.value=saDetails;
		            newSaDetails.value=saDetails;
		            	}
		            i++;
		});
	});
	return false;
}

function ajaxAutoSASelectionList(workshopIdSelect,selectedDate,serviceAdv){
	
	var date = document.getElementById(selectedDate).value;
	var workshopId = document.getElementById(workshopIdSelect).value;
	var preSaDetails = document.getElementById('preSaDetails');
	var newSaDetails = document.getElementById('newSaDetails');
	$('#'+serviceAdv+' option').remove();
	$('#serviceAdvisorTemp option').remove();
	console.log("3");
	if(preSaDetails.value!=""){
	$.ajax({
		
		url:"/CRE/callDispositionPage/"+preSaDetails.value
	}).done(function(data){
		            preSaDetails.value="NA";
		            newSaDetails.value ="NA";
	});
	}
	return false;
}

function ajaxAutoSAManualchange(workshopIdSelect,selectedDate,serviceAdv){
	
	var serviceAdvData = document.getElementById(serviceAdv).value;
	//alert(serviceAdvData);
	
	var date = document.getElementById('date12345').value;
	var workshopId = document.getElementById('workshop').value;
	var preSaDetails = document.getElementById('preSaDetails');
	var newSaDetails = document.getElementById('newSaDetails');
	if(preSaDetails.value === "" || preSaDetails.value === ''){
		preSaDetails.value = "NA";
	}
	console.log("4");
	newSaDetails.value = $('#serviceAdvisorTemp option').eq($("#"+serviceAdv+"").prop('selectedIndex')).val()
	$.ajax({
		
		url:"/CRE/callDispositionPage/" + workshopId +"/"+date+"/"+preSaDetails.value+"/"+newSaDetails.value
	}).done(function(data){
		preSaDetails.value = newSaDetails.value;
		newSaDetails.value = newSaDetails.value;
	});
}

function onChangeofDateorWorkshop(){
	
	$('#serviceAdvisor option').remove();
	$('#serviceAdvisorTemp option').remove();
	
	
}



//date picker of month

$( ".datepicMandYDropDown" ).datepicker({
    changeMonth: true,
    changeYear: true,	  
	  dateFormat: 'yy-mm-dd',
	  yearRange: "-10:+1"
	  
  });

$( ".datepicLSDDropDown" ).datepicker({
    changeMonth: true,
    changeYear: true,	
    maxDate: new Date(),
	  dateFormat: 'yy-mm-dd',
	  yearRange: "-10:+1"
	  
  });

$('.characterLimit').keypress(function() {
	$('.characterLimit').attr('maxlength','500');
	var str = this.value;
	if(str!= null && str.length>499) {
		Lobibox.notify('warning', {
			msg: 'Character limit exceed of 500..'
		});
	}
});


function ajaxRequestPSFDataPageWithTabForMaruti6thDay(){
	assignedInteractionPSFDataMaruti();
    var tab_a=document.getElementById('toActivateInsTab1').value;
    var tab_b=document.getElementById('toActivateInsTab2').value;
    var tab_c=document.getElementById('toActivateInsTab3').value;
    var tab_d=document.getElementById('toActivateInsTab4').value;
    var tab_e=document.getElementById('toActivateInsTab5').value;
	//alert(tab_a.slice(0,2));
    if(tab_a.slice(0,2)=="in"){

	if($("#Tab1").length){
    
    var url = "#home";
    $('.nav a[href="'+url+'"]').parent().addClass('active');
    assignedInteractionPSFDataMaruti();
	}
	
    }else if(tab_b.slice(0,2)=="in"){

    	if($("#Tab2").length){
            
            var url = "#profile";
            $('.nav a[href="'+url+'"]').parent().addClass('active');
            ajaxCallForFollowUpRequiredPSFDataMaruti();
        	}


        }else if(tab_c.slice(0,2)=="in"){

        	if($("#Tab3").length){
                
                var url = "#satisfied";
                $('.nav a[href="'+url+'"]').parent().addClass('active');
                ajaxCallForSatisfiedPSFDataMaruti();
            	}
        	

            }else if(tab_d.slice(0,2)=="in"){

            	if($("#Tab4").length){
                    
                    var url = "#nonContacts";
                    $('.nav a[href="'+url+'"]').parent().addClass('active');
                    ajaxCallForNonContactsPSFDataMaruti();
                }

                }else if(tab_e.slice(0,2)=="in"){

                	if($("#Tab5").length){
                        
                        var url = "#resolved";
                        $('.nav a[href="'+url+'"]').parent().addClass('active');
                        ajaxCallForResolvedPSFDataMaruti();
                        
                    	}

                    }
}



function ajaxRequestComplaintDataPageWithTabForMaruti(){
	assignedInteractionComplaintDataMaruti();
    var tab_a=document.getElementById('toActivateInsTab1').value;
    var tab_b=document.getElementById('toActivateInsTab2').value;
    var tab_c=document.getElementById('toActivateInsTab3').value;
    var tab_d=document.getElementById('toActivateInsTab4').value;
	//alert(tab_a.slice(0,2));
    if(tab_a.slice(0,2)=="in"){

	if($("#Tab1").length){
    
    var url = "#home";
    $('.nav a[href="'+url+'"]').parent().addClass('active');
    assignedInteractionComplaintDataMaruti();
	}
	
    }else if(tab_b.slice(0,2)=="in"){

    	if($("#Tab2").length){
            
            var url = "#profile";
            $('.nav a[href="'+url+'"]').parent().addClass('active');
            ajaxCallForFollowUpRequiredComplaintDataMaruti();
        	}


        }else if(tab_c.slice(0,2)=="in"){

        	if($("#Tab4").length){
                
                var url = "#rework";
                $('.nav a[href="'+url+'"]').parent().addClass('active');
                ajaxCallForReworkComplaintDataMaruti();
            	}
        	

            }else if(tab_d.slice(0,2)=="in"){

            	if($("#Tab3").length){
                    
                    var url = "#nonContacts";
                    $('.nav a[href="'+url+'"]').parent().addClass('active');
                    ajaxCallForNonContactsComplaintDataMaruti();
                }

                }
}



function CalDate(date1,date2) {
    var diff = Math.floor(date1.getTime() - date2.getTime());
    var secs = Math.floor(diff/1000);
    var mins = Math.floor(secs/60);
    var hours = Math.floor(mins/60);
    var days = Math.floor(hours/24);
    var months = Math.floor(days/30);
    var years = Math.floor(months/12);
    months=Math.floor(months%12);
    days = Math.floor(days%31);
    hours = Math.floor(hours%24);
    mins = Math.floor(mins%60);
    secs = Math.floor(secs%60); 
    var message = ""; 
    if(days<=0){
    message += secs + " sec "; 
    message += mins + " min "; 
    message += hours + " hours "; 
    }else{
        message += days + " days "; 
        if(months>0 || years>0){
            message += months + " months ";
        }
        if(years>0){
            message += years + " years ago";    
        }
    }
    return message
}

$(function() {
	  $( ".salesdatepicker" ).datepicker({
	  autoclose: true,
	  dateFormat: 'yy-mm-dd',
	  maxDate: new Date()
	  });
	 });

function ajaxRequestDataOFCREManagerIndexPageWithTab(){
	//assignedInteractionData();
    var tab_a=document.getElementById('toActivateTab1').value;
    var tab_b=document.getElementById('toActivateTab2').value;
    var tab_c=document.getElementById('toActivateTab3').value;
    var tab_d=document.getElementById('toActivateTab4').value;
    var tab_e=document.getElementById('toActivateTab5').value;
    var tab_f=document.getElementById('toActivateTab6').value;
    var tab_g=document.getElementById('toActivateTab7').value;
    var tab_h=document.getElementById('toActivateTab8').value;
    var tab_i=document.getElementById('toActivateTab9').value;
	//alert(tab_a.slice(0,2));
    if(tab_a.slice(0,2)=="in"){

	if($("#Tab1").length){
    
    var url = "#home";
    $('.nav a[href="'+url+'"]').parent().addClass('active');
    assignedInteractionDataMR();
	}
	
    }else if(tab_b.slice(0,2)=="in"){

    	if($("#Tab2").length){
            
            var url = "#profile";
            $('.nav a[href="'+url+'"]').parent().addClass('active');
            ajaxCallForFollowUpRequiredServerMR();
        	}


        }else if(tab_c.slice(0,2)=="in"){

        	if($("#Tab3").length){
                
                var url = "#messages";
                $('.nav a[href="'+url+'"]').parent().addClass('active');
                ajaxCallForserviceBookedServerDataMR();
            	}
        	

            }else if(tab_d.slice(0,2)=="in"){

            	if($("#Tab4").length){
                    
                    var url = "#settings";
                    $('.nav a[href="'+url+'"]').parent().addClass('active');
                    ajaxCallForServiceNotRequiredServerMR();
                }

                }else if(tab_e.slice(0,2)=="in"){

                	if($("#Tab5").length){
                        
                        var url = "#nonContacts";
                        $('.nav a[href="'+url+'"]').parent().addClass('active');
                        ajaxCallForNonContactsServerMR();
                        
                    	}

                    }else if(tab_f.slice(0,2)=="in"){

                    	if($("#Tab7").length){
                            
                            var url = "#sbn-1day";
                            $('.nav a[href="'+url+'"]').parent().addClass('active');
                            ajaxCallCRMNonFSPMSBucket();
                            
                        	}

                        }
                    else if(tab_g.slice(0,2)=="in"){

                    	if($("#Tab8").length){
                            
                            var url = "#sbnthday";
                            $('.nav a[href="'+url+'"]').parent().addClass('active');
                            ajaxCallCRMNminuOneDay();
                            
                        	}

                        }
                    else if(tab_h.slice(0,2)=="in"){

                    	if($("#Tab9").length){
                            
                            var url = "#nfspms";
                            $('.nav a[href="'+url+'"]').parent().addClass('active');
                            ajaxCallCRMNthDay();
                            
                        	}

                        }
                    else if(tab_i.slice(0,2)=="in"){

                    	if($("#Tab10").length){
                            
                            var url = "#futureFollowup";
                            $('.nav a[href="'+url+'"]').parent().addClass('active');
                            ajaxCallCRMFutureFollowup();
                            
                        	}

                        }
    
}

function ajaxRequestDataOFInsCREManagerPageWithTab(){

    var tab_a=document.getElementById('toActivateTab1').value;
    var tab_b=document.getElementById('toActivateTab2').value;
    var tab_c=document.getElementById('toActivateTab3').value;
    var tab_d=document.getElementById('toActivateTab4').value;
    var tab_e=document.getElementById('toActivateTab5').value;
    var tab_f=document.getElementById('toActivateTab6').value;
    var tab_g=document.getElementById('toActivateTab7').value;
    var tab_h=document.getElementById('toActivateTab8').value;
	//alert(tab_a.slice(0,2));
    if(tab_a.slice(0,2)=="in"){

	if($("#Tab1").length){
    
    var url = "#home";
    $('.nav a[href="'+url+'"]').parent().addClass('active');
    InsuranceassignedInteractionDataMR();
	}
	
    }else if(tab_b.slice(0,2)=="in"){

    	if($("#Tab2").length){
            
            var url = "#profile";
            $('.nav a[href="'+url+'"]').parent().addClass('active');
            ajaxInsuranceCallForFollowUpRequiredServerMR(4);
        	}


        }else if(tab_c.slice(0,2)=="in"){

        	if($("#Tab3").length){
                
                var url = "#messages";
                $('.nav a[href="'+url+'"]').parent().addClass('active');
                ajaxInsuranceCallForFollowUpRequiredServerMR(25);
            	}
        	

            }else if(tab_d.slice(0,2)=="in"){

            	if($("#Tab4").length){
                    
                    var url = "#settings";
                    $('.nav a[href="'+url+'"]').parent().addClass('active');
                    ajaxInsuranceCallForFollowUpRequiredServerMR(26);
                }

                }else if(tab_e.slice(0,2)=="in"){

                	if($("#Tab5").length){
                        
                        var url = "#nonContacts";
                        $('.nav a[href="'+url+'"]').parent().addClass('active');
                        ajaxInsuranceCallForNonContactsServerMR(1);
                        
                    	}

                    }else if(tab_f.slice(0,2)=="in"){

                    	if($("#Tab6").length){
                            
                            var url = "#insn1day";
                            $('.nav a[href="'+url+'"]').parent().addClass('active');
                            ajaxCallForInsNminusOne();
                            
                        	}

                        }
                    else if(tab_g.slice(0,2)=="in"){

                    	if($("#Tab7").length){
                            
                            var url = "#insnthday";
                            $('.nav a[href="'+url+'"]').parent().addClass('active');
                            ajaxCallForInsNthDay();
                            
                        	}

                        }
                    else if(tab_h.slice(0,2)=="in"){

                    	if($("#Tab8").length){
                            
                            var url = "#futureIns";
                            $('.nav a[href="'+url+'"]').parent().addClass('active');
                            ajaxFutureFollowup();
                            
                        	}

                        }
    
}

