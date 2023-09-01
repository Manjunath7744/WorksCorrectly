//Upload Report Excel 

 function ajaxCallForUploadExcelReport(){
	 
     //alert("server side data load");
	 var uploadReportId=document.getElementById('reportUploadId').value;
	// alert(uploadReportId);
	 
     var typeIds="",i;
     myOption = document.getElementById('dataFileType');
     
 for (i=0;i<myOption.options.length;i++){
     if(myOption.options[i].selected){
         if(myOption.options[i].value != "--Select--"){
        	 typeIds = typeIds + myOption.options[i].value + ",";
         }
     }
 }
 	if(typeIds.length > 0){
 		typeIds = typeIds.substring(0, typeIds.length - 1);
 	}
 	else{
 		typeIds="Select";
 	}

 	var fromDate=document.getElementById("fromDate").value;
 	var toDate=document.getElementById("toDate").value;

 	/* alert(" fromDate "+fromDate);
 	alert(" toDate "+toDate);
 	alert(" typeIds "+typeIds); */
     
    $('#uploadReport').dataTable( {
        "bDestroy": true,
        "processing": true,
        "serverSide": true,
        "scrollY": 300,
        "paging": true,
        "searching":false,
        "ordering":false,			
        "ajax": "/uploadReport/"+typeIds+"/"+fromDate+"/"+toDate+"/"+uploadReportId+""
    } );
    
    $('a[data-toggle="tab"]').on('shown.bs.tab', function(e){
      $($.fn.dataTable.tables(true)).DataTable()
         .columns.adjust();
   });
    
    $("#reportUploadId").val("0");
    
    }