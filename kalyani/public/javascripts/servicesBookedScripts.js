/*  
Pop up Workshop Service Booked
*/

function callWorkshop()
{
	$.blockUI();
	var workId=document.getElementById('workshop').value;
	var userId=document.getElementById('wyzUser_Id').value;
	
	//alert("workshop id is:"+workId+" "+user id is:"+userId);
		
	 var urlDisposition = "/CRE/getWorkshopServices/" + workId + "/" + userId + "";
	 
    $.ajax({
        url: urlDisposition

    }).done(function (data) {
        if(data!=null){	
			
			//alert("success");
			
			var tableHeaderRowCount = 1;
			var table = document.getElementById('dataTables-example52');
			var rowCount = table.rows.length;
			for (var i = tableHeaderRowCount; i < rowCount; i++) {
			table.deleteRow(tableHeaderRowCount);
			}
			
                        var workshop_list=data.workshopList;                       
                        var serviceBooked_list=data.serviceBookedList;
                                               
                       
			for(i=0;i<serviceBooked_list.length;i++){
				
				tr = $('<tr/>');
                            
			     tr.append("<td>" + workshop_list[i].workshopName + "</td>");                            
			     tr.append("<td>" + serviceBooked_list[i].serviceScheduledDate + "</td>");
			     
  
			      $('#dataTables-example52').append(tr);	
                              
				
                                    


			
			}
                         $.unblockUI();
	
}
 });

}

/*  
Pop up Service Advisor Service Booked
*/

function callServiceAdvisor()
{
    $.blockUI();
	//alert("sa");
	  var serviceAdvisorId=document.getElementById('serviceAdvisor').value;
	  var userId=document.getElementById('wyzUser_Id').value;
	  
	  //alert(serviceAdvisorId+userId);
	

		
	 var urlDisposition = "/CRE/getServiceAdvisorServices/" + serviceAdvisorId + "/" + userId + "";
	 
    $.ajax({
        url: urlDisposition

    }).done(function (data) {
		
		console.log(data);
        if(data!=null){	
			
			//alert("success");
			
			
			var tableHeaderRowCount = 1;
			var table = document.getElementById('dataTables-example53');
			var rowCount = table.rows.length;
			for (var i = tableHeaderRowCount; i < rowCount; i++) {
			table.deleteRow(tableHeaderRowCount);
			}
			
                        var workshop_list=data.workshopList;                       
                        var serviceBooked_list=data.serviceBookedList;
                        var serviceAdvisor_list=data.serviceAdvisorList; 
						var userlist=data.wyzUserList;
                       
			for(i=0;i<userlist.length;i++)
			{
				
				tr = $('<tr/>');
                    
				
			     tr.append("<td>" + userlist[i].userName  + "</td>");
				  tr.append("<td>" + serviceAdvisor_list[i].AdvisorName  + "</td>");
				  tr.append("<td>" + workshop_list[i].workshopName  + "</td>");
			     tr.append("<td>" + serviceBooked_list[i].serviceScheduledDate  + "</td>");
				
                 
			     
  
			      $('#dataTables-example53').append(tr);	
                              
				



			
			}
                         $.unblockUI();
	
}
 });

}

/*  
Pop up Driver Service Booked
*/




function callDriver()
{
	 $.blockUI();
	var driverId=document.getElementById('drivers').value;
	var userId=document.getElementById('wyzUser_Id').value;
	
	
		
	 var urlDisposition = "/CRE/getDriverServices/" + driverId + "/" + userId + "";
	 
    $.ajax({
        url: urlDisposition

    }).done(function (data) {
        if(data!=null){	
			
			
			
			var tableHeaderRowCount = 1;
			var table = document.getElementById('dataTables-example54');
			var rowCount = table.rows.length;
			for (var i = tableHeaderRowCount; i < rowCount; i++) {
			table.deleteRow(tableHeaderRowCount);
			}
			
                        var driver_list=data.driverList;                    
                       var serviceBooked_list=data.serviceBookedList;
		        
                      
                                               
                       
			for(i=0;i<serviceBooked_list.length;i++){
				
				tr = $('<tr/>');
                            
			     tr.append("<td>" + driver_list[i].driverName + "</td>");                            
			     tr.append("<td>" + serviceBooked_list[i].serviceScheduledDate + "</td>");
			     
  
			      $('#dataTables-example54').append(tr);	
                              
				



			
			}
                         $.unblockUI();
	
}
 });

}