function tableText(tableCell) {
	//alert('clicked two inside'+tableCell.id);
	vtempValue = parseInt(document.getElementById("tempValue").value);


	if (vtempValue == tableCell.cellIndex || vtempValue == 0) {
		vtempValue = tableCell.cellIndex//    table.rows[0].cells[j].innerText;

	}
	else { return; }
	console.log("tablecel id : " + tableCell.id);
	var className = tableCell.className;

	if (className == "") {
		//alert('clicked 4');
		$.confirm({
			title: 'Confirm!',
			closeIcon: true,
			content: 'Do you want Assign' + "<br>" + tableCell.id,
			buttons: {
				Yes: function () {
					vstartValue = parseInt(document.getElementById("startValue").value);
					vendValue = parseInt(document.getElementById("endValue").value);


					if (vstartValue === 0) {
						vstartValue = tableCell.parentElement.rowIndex;
						document.getElementById("startValue").value = tableCell.parentElement.rowIndex;
					}
					else if (vendValue === 0) {
						vendValue = tableCell.parentElement.rowIndex;
						document.getElementById("endValue").value = vendValue;

					}

					$(tableCell).addClass('ColorGreen');
					if (vendValue === 0) {
						vendValue = tableCell.parentElement.rowIndex;
					}
					if (vstartValue != 0 && vendValue != 0) {
						if (vstartValue < tableCell.parentElement.rowIndex) {
							if (vendValue < tableCell.parentElement.rowIndex) {
								vendValue = tableCell.parentElement.rowIndex;
							}
						} else {
							vstartValue = tableCell.parentElement.rowIndex;
						}
						if (vstartValue > vendValue) {
							minValue = vendValue;
							maxValue = vstartValue;
						} else {
							minValue = vstartValue;
							maxValue = vendValue;
						}

						var table = document.getElementById("tableID");
						var tableCellheader = table.rows[0].cells[tableCell.cellIndex];

						//console.log("tableCellheader : "+tableCellheader.id);
						//console.log("tableCellheader innerText: "+tableCellheader.innerText);

						document.getElementById("driverValue").value = tableCellheader.id;
						$('#newDriver').html(tableCellheader.innerText);

						console.log("min and max value : " + minValue, maxValue);

						for (var i = minValue; i < maxValue; i++) {
							var tableCell1 = table.rows[i].cells[tableCell.cellIndex];
							$(tableCell1).addClass('ColorGreen');
						}

						document.getElementById("startValue").value = minValue;
						document.getElementById("endValue").value = maxValue;
						minValue = '';
						maxValue = '';

					}


					document.getElementById("tempIncreValue").value = $('#tableID .ColorGreen').length;
					document.getElementById("tempValue").value = tableCell.cellIndex;
					if ($('#tableID .ColorGreen').length === 0) {
						document.getElementById("startValue").value = 0;
						document.getElementById("endValue").value = 0;
						document.getElementById("tempValue").value = 0;
					}

				},
				No: function () {
					$(tableCell).removeClass('ColorGreen');
				}
			}
		});
	}
	else {
		$.confirm({
			title: 'Confirm!',
			closeIcon: true,
			content: 'Do you want UnAssign' + "<br>" + tableCell.id,
			buttons: {
				Yes: function () {

					vstartValue = parseInt(document.getElementById("startValue").value);
					vendValue = parseInt(document.getElementById("endValue").value);
					var table1 = document.getElementById('tableID');
					$(tableCell).removeClass('ColorGreen');
					if (vstartValue !== tableCell.parentElement.rowIndex && vendValue === tableCell.parentElement.rowIndex) {
						for (i = vendValue - 1; i >= vstartValue; i--) {
							var tableCell2 = table1.rows[i].cells[tableCell.cellIndex];
							if ($(tableCell2).hasClass('ColorGreen')) {
								vendValue = i;
								document.getElementById("endValue").value = i;
								break;
							}
							continue;
						}
					} else if (vstartValue === tableCell.parentElement.rowIndex && vendValue !== tableCell.parentElement.rowIndex) {
						for (i = vstartValue + 1; i <= vendValue; i++) {
							var tableCell2 = table1.rows[i].cells[tableCell.cellIndex];
							if ($(tableCell2).hasClass('ColorGreen')) {
								vstartValue = i;
								document.getElementById("startValue").value = i;
								break;
							}
							continue;
						}
					} else if (vstartValue === tableCell.parentElement.rowIndex && vendValue === tableCell.parentElement.rowIndex) {
						document.getElementById("startValue").value = 0;
						document.getElementById("endValue").value = 0;
						document.getElementById("tempValue").value = 0;
					}


					document.getElementById("tempIncreValue").value = $('#tableID .ColorGreen').length;
					document.getElementById("tempValue").value = tableCell.cellIndex;
					if ($('#tableID .ColorGreen').length === 0) {
						document.getElementById("startValue").value = 0;
						document.getElementById("endValue").value = 0;
						document.getElementById("tempValue").value = 0;
					}

				},
				No: function () {
					$(tableCell).addClass('ColorGreen');

				}
			}
		});
	}




}

//	$(document).ready(function(){	

//				$("#AddAddrMMSSaveId").click(function(){
//					var vAddr1= $("#AddAddress1All").val();
//					var vAddr2= $("#AddAddress2All").val();
//					var vState4= $("#AddAddrMMSState").val();
//					var vcity3= $("#cityInputPopup3").val();
//					var vpin5= $("#PinCode1MMS").val();

//					var ResultAll=vAddr1.concat(vAddr2+" , "+vcity3+" , "+vState4+","+vpin5);
//					//alert(ResultAll);
//					$("#AddressMSSId").prepend($("<option></option>").html(ResultAll));
//					  	 $("#AddressMSSId option:first").prop('selected', true);

//					});
////time picker

//            //dates

//            $( ".datepicMYDropDown" ).datepicker({

//                changeMonth: true,
//                changeYear: true,
//          	  dateFormat: 'yy-mm-dd',
//          	  yearRange: "-100:+0"

//              });

//	});


function AssignBtnBkreview() {
	//alert("AssignBtnBkreview");
	$.blockUI();
	//var newdates = $('#changeserviceBookingDate').val();
	var scheduleDate = document.getElementById('scheduleDate').value;
	var workshopId = document.getElementById('workshop').value;

	var urlPath = "/driverListSchedule/" + workshopId + "/" + scheduleDate;
	$.ajax({
		url: urlPath
	}).done(function (dataDriver) {
		var data = dataDriver.driverListData;
		var timeslot = dataDriver.timeSlotData;

		$('#tableID').empty();

		var table = document.getElementById("tableID");
		var header = table.createTHead();
		var row = header.insertRow(0);


		for (var i = data.length - 1; i >= 0; i--) {
			var cell = row.insertCell(0);
			cell.outerHTML = "<th id=" + data[i].id + ">" + data[i].userName + "</th>";
		}

		var cell = row.insertCell(0);
		cell.outerHTML = "<th>Time slot</th>";

		for (var j = 0; j < timeslot.length; j++) {
			tr = $('<tr/>');
			tr.append('<td id=' + timeslot[0].timeRange + ' At ' + timeslot[j].timeRange + '>' + timeslot[j].timeRange + '</td>');
			for (var k = 0; k < data.length; k++) {

				var blockedTime = data[k].listTime;
				var result = inArray(timeslot[j].startTime, blockedTime)
				//console.log("Time is result: "+result);  
				if (result) {
					tr.append('<td id=' + timeslot[j].timeRange + ' class="ColorRed"></td>');
				} else {

					tr.append('<td id=' + timeslot[j].timeRange + '></td>');
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
				"searching": false,
				"ordering": false,
				"bInfo": false
			});

		}

		var firsftSelect;
		if (table != null) {
			for (var i = 1; i < table.rows.length; i++) {
				for (var j = 1; j < table.rows[i].cells.length; j++) {
					//table.rows[i].cells[j].id= table.rows[0].cells[j].innerText +" At " + table.rows[i].cells[0].innerText;
					//console.log("class name : "+table.rows[i].cells[j].className )
					var blockedclass = table.rows[i].cells[j].className;

					if (blockedclass == "ColorRed") { } else {
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


//$('#allcateSubmit').on('click',function(){

//	  $("#SubmitBtnBkreview").show();
//	});

function inArray(needle, haystack) {
	var count = haystack.length;
	for (var i = 0; i < count; i++) {
		if (haystack[i] === needle) { return true; }
	}
	return false;
}

