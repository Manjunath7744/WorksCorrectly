//Insurance compaliants

/*const { Session } = require("node:inspector");*/

function ajaxLeadTagByDepartmentInsurance() {

    var departmentName = document.getElementById('selected_department_ins').value;
    var urlPath = siteRoot + "/complaintTaggingList/" + departmentName;
    $.ajax({
        url: urlPath
    }).done(function (leadlist) {

        $('#LeadTagsByLocationIns').empty();
        var dropdown = document.getElementById("LeadTagsByLocationIns");

        for (var i = 0; i < leadlist.length; i++) {
            dropdown[dropdown.length] = new Option(leadlist[i], leadlist[i]);
        }


    });

}

//New added For Email Global - 30/07/2020(Manoj) ****************************************
function bindEmailIds(ddlId, moduleType,dataFor) {

    $.ajax({
        type: 'POST',
        url: siteRoot + '/CallLogging/loadEmail/',
        datatype: 'json',
        data: { moduleType: moduleType, dataFor: dataFor },
        cache: false,
        success: function (res) {

            if (res.success == true) {
                var ddlFromMail = $('#' + ddlId);

                $(ddlFromMail).empty();

                $(ddlFromMail).append(`<option value=''>--Select--</option>`)
                if (res.emails.length > 0) {
                    for (var i = 0; i < res.emails.length; i++) {
                        $(ddlFromMail).append(`<option value='${res.emails[i].emailId}' data-pwd='${res.emails[i].password}'>${res.emails[i].emailId}</option>`)
                    }

                    var isPassword = $('#fromEmailId').find(':selected').attr('data-pwd');

                    if (isPassword == "No") {
                        $('#disEmailPass').show();
                        $('#fromPassword').val('');
                    }
                    else {
                        $('#disEmailPass').hide();
                        $('#fromPassword').val('');
                    }
                }
                else {

                    if (dataFor != 'Dissat') {
                        Lobibox.notify('warning', {
                            msg: 'No Emails Found....'
                        });
                    }
                }
            }
            else {
                Lobibox.notify('warning', {
                    msg: res.error
                });
            }

        }, error(error) {

        }
    });
}

function manageEmailIds(ddl_emailId, txt_passwordId, pwd_divId){
    
    var isPassword = $('#' + ddl_emailId).find(':selected').attr('data-pwd');

    if (isPassword == "No") {
        $('#' + pwd_divId).show();
        $('#' + txt_passwordId).val('');
    }
    else {
        $('#' + pwd_divId).hide();
        $('#' + txt_passwordId).val('');
    }
}

// **************************** Email Function End ***********************************








function ajaxLeadTagByDepartments() {
    var exist = 0;
    var moduleId = 1;
    var userLocation = document.getElementById('location_Id').value;
    var departmentName = $('#selected_department option:selected').text();
    // var departmentNameOthers = document.getElementById('selected_department1').value;
    //var departmentNameOthers = $('#selected_department1').text();
    var departmentNameOthers = $("#selected_department1 option:selected").text();
    var moduletype = document.getElementById('typeOfDispoPageView').value;
    if (moduletype == "insurance" || moduletype == "insuranceSearch") {
        moduleId = 2;
    }

    if (departmentName !== "") {
        var departmentNameOther = departmentName
    }
    
    else if (departmentNameOthers !== "") {
        var departmentNameOther = departmentNameOthers
         exist = 1;
    }

    if (exist === 1) {
        // alert("departmentName");
        var urlLink = siteRoot+"/CallLogging/getLeadTagByDepartment/";
        $.ajax({
            type: 'POST',
            url: urlLink,
            datatype: 'json',
            data: { locId: userLocation, deptName: departmentNameOther, moduleId: moduleId },
            cache: false,
            success: function (res) {
                if (res.success == true) {
                    if (res.leadlist != null) {
                        $('#LeadTagsByLocation1').empty();
                        var dropdown = document.getElementById("LeadTagsByLocation1");

                        for (var i = 0; i < res.leadlist.length; i++) {

                            dropdown[dropdown.length] = new Option(res.leadlist[i], res.leadlist[i]);

                        }

                    }
                }
                else {
                    alert(res.error);
                }
            }, error(error) {

            }
        });


    } else {
        // alert("departmentNameOther");
        var urlLink = siteRoot+ "/CallLogging/getLeadTagByDepartment/";


        $.ajax({
            type: 'POST',
            url: urlLink,
            datatype: 'json',
            data: { locId: userLocation, deptName: departmentNameOther, moduleId: moduleId },
            cache: false,
            success: function (res) {
                if (res.success == true) {
                    if (res.leadlist != null) {
                        $('#LeadTagsByLocation').empty();
                        var dropdown = document.getElementById("LeadTagsByLocation");
                        for (var i = 0; i < res.leadlist.length; i++) {

                            dropdown[dropdown.length] = new Option(res.leadlist[i], res.leadlist[i]);

                        }
                    }
                }
                else {
                    alert(res.error);
                }
            }, error(error) {

            }
        });


    }

}



//Schedular function call of insurance


//Schedular function call of insurance

//Schedular function call of insurance
function ajaxCallForSchedular() {
    //debugger;


    var scheduleDate = document.getElementById("dateis").value;
    var locaId = document.getElementById("fieldLocation").value;
    var needle = [];
    var blocktme = [];
    if (scheduleDate == "" || locaId == "") {

        Lobibox.notify('warning', {
            msg: 'scheduled Date or Location is not selected'
        });

        return false;
    }

    console.log("ajaxCallForSchedular " + locaId + " scheduleDate : " + scheduleDate);
    var urlPath = siteRoot+ "/InsuranceOutBound/filedExecutivesListToSchedule/";
    $.ajax({
        type: 'POST',
        url: urlPath,
        datatype: 'json',
        data: { scheduleDate: scheduleDate, locaId: locaId },
        cache: false,
        success: function (dataDriver) {
            var result = false;
            var data = dataDriver.UsersList;
            var timeslot = dataDriver.timeSlots;
            console.log(timeslot.startTime);

            if ($.fn.DataTable.isDataTable("#tableschID")) {
                var table = $("#tableschID").DataTable();
                table.clear();
                table.destroy();
            }
            $("#tableschID thead").remove();


            var table = document.getElementById("tableschID");
            var header = table.createTHead();
            var row = header.insertRow(0);
            /*
            for (var i = 0; i < timeslot.length; i++) {

                needle[i] = timeslot[i].startTime.Hours + ":" + timeslot[i].startTime.Minutes
            }
            */


            for (var i = data.length - 1; i >= 0; i--) {

                console.log("id on click : " + data[i].id + "username : " + data[i].userName);
                var cell = row.insertCell(0);
                cell.outerHTML = "<th id=" + data[i].id + ">" + data[i].userName + "</th>";
            }

            var cell = row.insertCell(0);
            cell.outerHTML = "<th>Timeslot</th>";

            console.log("chethan" + timeslot.length);

            for (var j = 0; j < timeslot.length-1; j++) {
                tr = $('<tr/>');
                tr.append('<td id=' + timeslot[0].timeRange + ' At ' + timeslot[j].timeRange + '>' + timeslot[j].timeRange + '</td>');
                for (var k = 0; k < data.length; k++) {
                    var blockedTime = data[k].listTime;
                    console.log(" User Block Time" + blockedTime);
                    if (blockedTime != null || blockedTime > 0) {
                        result = inArray(timeslot[j].startTime, blockedTime)
                        if (result) {
                            result = true
                            console.log("Time is result: " + result);

                        }
                    }
                    //else {
                    //    result = false
                    //}

                    if (result) {
                        console.log("Booked", timeslot[j].timeRange);
                        tr.append('<td id=' + timeslot[j].timeRange + ' class="ColorRed"></td>');
                        result = false;
                    }
                    else {
                        tr.append('<td id=' + timeslot[j].timeRange + '></td>');
                    }

                }
                $('#tableschID').append(tr);

                $.fn.dataTable.ext.errMode = 'none';
                $($.fn.dataTable.tables(true)).DataTable()
                    .columns.adjust();

            }

            var firsftSelect;
            if (table != null) {
                for (var i = 1; i < table.rows.length; i++) {
                    for (var j = 1; j < table.rows[i].cells.length; j++) {
                        //table.rows[i].cells[j].id= table.rows[0].cells[j].innerText +" At " + table.rows[i].cells[0].innerText;
                        console.log("class name : " + table.rows[i].cells[j].className)
                        var blockedclass = table.rows[i].cells[j].className;

                        if (blockedclass == "ColorRed") { } else {
                            table.rows[i].cells[j].onclick = function () {
                                //alert('clicked one ');
                                tableTextInsurance(this);


                            };
                        }

                    }
                }
            }


            $('#tableschID').dataTable({

                "fixedHeader": true,
                "scrollX": true,
                "scrollY": 320,
                "paging": false,
                "searching": false,
                "ordering": false,
                "bInfo": false,
                fixedColumns: {
                    leftColumns: 1
                }

            });


        }, error(error) {

        }
    });
    $('#schButton').show();
}


//Ajax Executives based on location
function executiveBasedOnWalkinLocation() {

    var sel = document.getElementById('walkinLocation').value;
    var urlLink = siteRoot + "/InsuranceOutBound/WalkinExecutivesByLocation/";
    $.ajax({
        type: 'POST',
        url: urlLink,
        datatype: 'json',
        data: { locaId: sel },
        cache: false,
        success: function (res) {
            //console.log(res.insuranceAgentList.length);

            if (res.insuranceAgentList.length>0) {
                $('#insuranceAgentDataId').empty();

                var dropdown = document.getElementById("insuranceAgentDataId");
                dropdown[0] = new Option('--Select--', '0');
                for (var i = 0; i < res.insuranceAgentList.length; i++) {

                    dropdown[dropdown.length] = new Option(res.insuranceAgentList[i].insuranceAgentName, res.insuranceAgentList[i].insuranceAgentId);
                }
            }
            else {
                Lobibox.notify('warning', {
                    msg: 'No Walkin-Executives present for this Location!'
                });
            }


        }, error(error) {

        }
    });
}





//Ajax chages of insurance model

function ajaxCallPSFOfVehicle() {
    var vehicleId = document.getElementById('vehical_Id').value;
    var urlLink = "/PSFforVehicle";

    $('#example901').dataTable({
        "bDestroy": true,
        "processing": true,
        "serverSide": false,
        "scrollY": 300,
        "scrollX": true,
        "paging": false,
        "searching": false,
        "ordering": false,
        "ajax": {
            'type': 'POST',
            'url': urlLink,
            'data': {
                vehicleId: '' + vehicleId,
            }
        },
        "fnRowCallback": function (nRow, aData, iDisplayIndex) {
            $('td', nRow).attr('nowrap', 'nowrap');
            return nRow;
        }
    });
    return false;
}

function ajaxCampaignNameBasedOnType() {
    var campaignTypeId = document.getElementById('campaignTypeSelected').value;
    //console.log(campaignTypeId);
    var urlLink = "/CRE/getCampaignNameBasedOnType/" + campaignTypeId + "";
    $.ajax({
        url: urlLink

    }).done(function (listCampaignNames) {

        console.log(listCampaignNames);

        $('#campaignNameSelected').empty();
        var dropdown = document.getElementById("campaignNameSelected");
        // dropdown[0]= new Option('Select All','Select');
        for (var i = 0; i < listCampaignNames.length; i++) {
            console.log(listCampaignNames[i].id);

            dropdown[dropdown.length] = new Option(listCampaignNames[i].campaignName, listCampaignNames[i].id);
            $("#campaignNameSelected").addClass('selectpicker');
            $('#campaignNameSelected').multiselect('rebuild');

        }
    });


}

function ajaxCampaignByModule() {

    var sel = document.getElementById('moduleAssign').value;
    if (sel == "PSF") {
        document.getElementById("campaignFromAssignDiv").style.display = "none";
        document.getElementById("campaignToAssignDiv").style.display = "none";
        document.getElementById("billFromAssignDiv").style.display = "block";
        document.getElementById("billToAssignDiv").style.display = "block";
    }
    else {
        document.getElementById("campaignFromAssignDiv").style.display = "block";
        document.getElementById("campaignToAssignDiv").style.display = "block";
        document.getElementById("billFromAssignDiv").style.display = "none";
        document.getElementById("billToAssignDiv").style.display = "none";
    }
    var urlLink = "/CRE/listCampaignAndCRE/" + sel + "";
    $.ajax({
        url: urlLink

    }).done(function (campaignAndCRElist) {
        //alert(campaignlist.length);
        console.log("campaignAndCRElist length " + campaignAndCRElist.length);
        console.log("campaign list length " + campaignAndCRElist[0].length);
        console.log("CRE list length " + campaignAndCRElist[1].length);
        if (campaignAndCRElist != null) {
            $('#usernameAssiged').empty();
            $('#campaignNameAssign').empty();
        }
        console.log("campaignAndCRElist length " + campaignAndCRElist.length);
        var dropdown = document.getElementById("campaignNameAssign");
        dropdown[0] = new Option('--Select--', '0');
        for (var i = 0; i < campaignAndCRElist[0].length; i++) {
            console.log(campaignAndCRElist[0][i]);
            dropdown[dropdown.length] = new Option(campaignAndCRElist[0][i].campaignName, campaignAndCRElist[0][i].id);
        }

        var dropdown1 = document.getElementById("usernameAssiged");
        dropdown1[0] = new Option('--Select--', '0');
        for (var i = 0; i < campaignAndCRElist[1].length; i++) {
            console.log(campaignAndCRElist[1][i].id);
            console.log(campaignAndCRElist[1][i].userName);
            dropdown1[dropdown1.length] = new Option(campaignAndCRElist[1][i].userName, campaignAndCRElist[1][i].id);
        }

    });
}

function ajaxWorkShopByCity() {


    var selectedCity = document.getElementById('locationAssiged').value;
    var urlLink = "/CRE/listWorkshops/" + selectedCity + "";

    $.ajax({
        url: urlLink

    }).done(function (work_list) {
        console.log("work_list " + work_list.length);
        if (work_list != null) {
            $('#workshopAssiged').empty();
            var dropdown = document.getElementById("workshopAssiged");
            dropdown[0] = new Option('--Select--', '0');
            for (var i = 0; i < work_list.length; i++) {

                dropdown[dropdown.length] = new Option(work_list[i].workshopName, work_list[i].id);
            }
        }
    });
}

//ajax for call history
function ajaxCallHistoryReports() {
    var workshopId = document.getElementById('workshopname').value;

    // alert(workshopId);

    var urlLink = "/CRE/listCREsByWorkshopcallhistory/" + workshopId + "";


    $.ajax({
        url: urlLink

    }).done(function (crelist) {

        console.log(crelist);

        $('#crename').empty();
        var dropdown = document.getElementById("crename");
        // dropdown[0]= new Option('Select All','Select');
        for (var i = 0; i < crelist.length; i++) {

            dropdown[dropdown.length] = new Option(crelist[i].username, crelist[i].id);
            $("#crename").addClass('selectpicker');
            $('#crename').multiselect('rebuild');




        }
    });
}

function ajaxSMSHistoryReports() {
    var workshopId = document.getElementById('workshopname').value;

    // alert(workshopId);

    var urlLink = "/CRE/listAllUsersByWorkshopsmshistory/" + workshopId + "";


    $.ajax({
        url: urlLink

    }).done(function (crelist) {

        console.log(crelist);
        $('#crename').empty();
        var dropdown = document.getElementById("crename");
        // dropdown[0]= new Option('Select All','Select');
        for (var i = 0; i < crelist.length; i++) {

            dropdown[dropdown.length] = new Option(crelist[i].username, crelist[i].id);
            $("#crename").addClass('selectpicker');
            $('#crename').multiselect('rebuild');

        }
    });

}





function ajaxCampaignByModuleAutoAssgPlan() {
    var sel = document.getElementById('moduleName').value;
    if (sel == "SMR") {
        sel = "Campaign";
    }
    var urlLink = "/CRE/listCampaignAndCRE/" + sel + "";
    $.ajax({
        url: urlLink
    }).done(function (campaignAndCRElist) {

        if (campaignAndCRElist != null) {
            $('#CampaignTypesDiv').empty();
        }
        var dropdown = document.getElementById("CampaignTypesDiv");
        for (var i = 0; i < campaignAndCRElist[0].length; i++) {
            dropdown[i] = new Option(campaignAndCRElist[0][i].campaignName, campaignAndCRElist[0][i].id);
        }
    });
}







// workhsop for call history
function ajaxCallToLoadWorkShopByCityCallhistory() {


    var selectedCity = document.getElementById('location').value;

    //console.log("selectedCity : "+selectedCity);

    var urlLink = siteRoot + "/CallLogging/getListWorkshopByLocation/";
    // alert(urlLink);

    $.ajax({
        type: 'POST',
        url: urlLink,
        datatype: 'json',
        data: { selectedCity: selectedCity },
        cache: false,
        success: function (res) {
            if (res.success == true) {
                if (res.workshoplist != null) {
                    $('#workshopname').empty();
                    var dropdown = document.getElementById("workshopname");
                    dropdown[0] = new Option('--Select--', '0');
                    for (var i = 0; i < res.workshoplist.length; i++) {
                        console.log("works : " + res.workshoplist[i].workshopName);

                        dropdown[dropdown.length] = new Option(res.workshoplist[i].workshopName, res.workshoplist[i].id);
                        $("#workshopname").addClass('selectpicker');
                        $('#workshopname').multiselect('rebuild');
                    }



                }
            }
            else {
                alert(res.error);
            }
        }, error(error) {

        }
    });
}






//workshop load by city and type in upload
function workshopsBasedOnLocAndType(sel) {
    var cityId = document.getElementById(sel).value;
    var locType = document.getElementById('locType_id').value;
    if (cityId == 0) {
        $('#workshop_id').empty();
        var loc = '<option value="0">--SELECT--</option>';
        $('#workshop_id').append(loc);
    }
    else {
        //var urlLink="/CRE/listWorkshopsByID/"+cityId+"";
        var urlLink = "/CRE/listWorkshopsByIdAndType/" + cityId + "/" + locType;

        $.ajax({
            url: urlLink

        }).done(function (workshops) {

            if (workshops != null) {
                $('#workshop_id').empty();
                var dropdown = document.getElementById("workshop_id");
                dropdown[dropdown.length] = new Option("--SELECT--", "0");
                for (var i = 0; i < workshops.length; i++) {
                    dropdown[dropdown.length] = new Option(workshops[i].workshopName, workshops[i].id);
                }
            }

        });
    }
}
function workshopsBasedOnLoca(sel) {

    var cityId = document.getElementById(sel).value;

    var urlLink = "/CRE/listWorkshopsByID/" + cityId + "";

    $.ajax({
        url: urlLink

    }).done(function (workshops) {

        if (workshops != null) {
            $('#workshop_id').empty();
            var dropdown = document.getElementById("workshop_id");
            dropdown[dropdown.length] = new Option("--SELECT--", "0");
            for (var i = 0; i < workshops.length; i++) {
                dropdown[dropdown.length] = new Option(workshops[i].workshopName, workshops[i].id);
            }
        }

    });

}




//get service advisor by workshop

function ajaxGetServiceAdvisorByWorkshop() {
    var workshopid = $('#workshop_id').val();
    var urlLink = "/serviceAdvisorListByWorkshop/" + workshopid + "";
    $.ajax({
        url: urlLink


    }).done(function (userList) {
        if (userList.SAList != null) {
            $('#selectSAID').empty();
            var dropdown = document.getElementById("selectSAID");
            //dropdown[0]= new Option('--Select--','0');
            for (var i = 0; i < userList.SAList.length; i++) {
                dropdown[dropdown.length] = new Option(userList.SAList[i].userName, userList.SAList[i].id);
                $("#selectSAID").addClass('selectpicker');
                $('#selectSAID').multiselect('rebuild');

            }
        }

        if (userList.DriverList != null) {
            $('#selectDriverID').empty();
            var dropdown = document.getElementById("selectDriverID");
            //dropdown[0]= new Option('--Select--','0');
            for (var i = 0; i < userList.DriverList.length; i++) {
                dropdown[dropdown.length] = new Option(userList.DriverList[i].userName, userList.DriverList[i].id);
                $("#selectDriverID").addClass('selectpicker');
                $('#selectDriverID').multiselect('rebuild');
            }
        }
        if (userList.CREList != null) {
            $('#userName').empty();
            var dropdown = document.getElementById("userName");
            //dropdown[0]= new Option('--Select--','0');
            for (var i = 0; i < userList.CREList.length; i++) {
                dropdown[dropdown.length] = new Option(userList.CREList[i].userName, userList.CREList[i].id);
                $("#userName").addClass('selectpicker');
                $('#userName').multiselect('rebuild');
            }
        }
    });



}


//checking to duplicated 


$('#vehicleRegNoId').on("keyup", function () {

    var regNoIs = document.getElementById('vehicleRegNoId').value;
    //alert(regNoIs);
    var urlDisposition = "/CRE/checkingVehicleRegNo/" + regNoIs + "";

    $.ajax({
        url: urlDisposition

    }).done(function (data) {

        if (data > 0) {

            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Vehicle Reg No is Already Existed.'
            });

        }


    });

});

function getAudioPLay(playerId) {

    //var audioplayer= $('#'+playerId).val();

    var blobURL = $("#128");
    //alert(audioplayer);

    var xhr = new XMLHttpRequest();

    xhr.addEventListener('load', function (blob) {
        if (xhr.readyState == 4 && xhr.status == 200) {
            console.log("src :" + window.URL.createObjectURL(xhr.response));
            blobURL.src = window.URL.createObjectURL(xhr.response);

            //audioplayer.play();
            //document.getElementById('128').play();
        }
    });
    xhr.open('GET', '/CREManager/downloadMediaFile/128');
    xhr.responseType = 'blob';
    xhr.send(null);



	/*var urlDisposition="/CREManager/downloadMediaFile/128";
	$.ajax({
	    url: urlDisposition

	}).done(function (data) {		
		console.log("src :"+data);
		audioplayer.src = "http://connect.autosherpas.com:9014/"+data;
		
	});*/
}

//Add address
/*


$("#AddAddrMMSSave").on('click', function(){

var customer_Id = document.getElementById('customer_Id').value;
var urlDisposition = "/CRE/saveaddcustomerAddress/" + wyzUser_id + "/" + customer_Id +"";

$.ajax({
    url: urlDisposition

}).done(function (data) {
	
	
});

});*/
// Upload Campaign List



function ajaxCampaignList() {

    var uploadType = document.getElementById("uploadTypeHidden").value;
    $("#sampleExcel").attr("href", "/downloadSampleExcel/" + uploadType + "");
    var urlLink = "/getCampaignList/" + uploadType + "";


    $.ajax({
        url: urlLink

    }).done(function (camp) {

        if (camp.length != 0) {

            document.getElementById("CampIDDis").style.display = "block";
            document.getElementById("campaignFromDate").style.display = "none";
            document.getElementById("campaignToDate").style.display = "none";
            document.getElementById("loc_id").style.display = "none";
            document.getElementById("workS_id").style.display = "none";
            document.getElementById("sales_id").style.display = "none";


            $('#CampaignId').empty();
            var dropdown = document.getElementById("CampaignId");
            dropdown[0] = new Option('--SELECT--', '0');
            for (var i = 0; i < camp.length; i++) {

                dropdown[dropdown.length] = new Option(camp[i].campName, camp[i].campId);
            }


        } else {
            document.getElementById("CampIDDis").style.display = "none";
            document.getElementById("campaignFromDate").style.display = "none";
            document.getElementById("campaignToDate").style.display = "none";
            document.getElementById("loc_id").style.display = "block";
            document.getElementById("workS_id").style.display = "block";
            document.getElementById("sales_id").style.display = "block";


        }


    });




}

// CIty By states


function getCityByStateSelection(sel, selCity) {

    var stateSel = document.getElementById(sel).value;
    // alert(stateSel);

    var urlLink = siteRoot + "/CallLogging/getCityNames/";

    $.ajax({
        type: 'POST',
        url: urlLink,
        datatype: 'json',
        data: { state: stateSel },
        cache: false,
        success: function (res) {
            if (res.success == true) {
                if (res.cities != null) {
                    $('#' + selCity).empty();
                    var dropdown = document.getElementById(selCity);
                    dropdown[0] = new Option('--SELECT--', '--SELECT--');
                    for (var i = 0; i < res.cities.length; i++) {
                        dropdown[dropdown.length] = new Option(res.cities[i], res.cities[i]);
                    }
                }
            }
            else {
                alert(res.error);
            }
        }, error(error) {

        }
    });
}




// CRES Based On Locations

function cresBasedOnLocations(sel) {
    var opt;
    var j = 0;
    for (var i = 0; i < sel.options.length; i++) {
        if (sel.options[i].selected == true) {
            if (j == 0)
                opt = sel.options[i].value;
            else
                opt = opt + "," + sel.options[i].value;

            // alert(opt);
            j++;
        }
    }


    var urlLink = "/getCRESByWorkshop/" + opt + "";
    console.log("location multi is : " + opt);

    $.ajax({
        url: urlLink

    }).done(function (cres) {

        if (cres != null) {
            $('#crename').empty();
            var dropdown = document.getElementById("crename");
            // dropdown[0]= new Option('Select','0');
            for (var i = 0; i < cres.length; i++) {

                dropdown[dropdown.length] = new Option(cres[i], cres[i]);
                $("#crename").addClass('selectpicker');
                $('#crename').multiselect('rebuild');
            }


        }


    });

}
// workshop based on multiselect locations

function workshopsBasedOnLocations(sel) {

    var opt;
    var j = 0;
    for (var i = 0; i < sel.options.length; i++) {
        if (sel.options[i].selected == true) {
            if (j == 0)
                opt = sel.options[i].value;
            else
                opt = opt + "," + sel.options[i].value;

            // alert(opt);
            j++;
        }
    }


    var urlLink = "/getworkshopByLocation/" + opt + "";

    $.ajax({
        url: urlLink


    }).done(function (workshops) {

        if (workshops != null) {
            $('#workshopname').empty();
            var dropdown = document.getElementById("workshopname");
            // dropdown[0]= new Option('Select','0');
            for (var i = 0; i < workshops.length; i++) {
                console.log("workshop multi : " + workshops[i]);

                dropdown[dropdown.length] = new Option(workshops[i], workshops[i]);
                $("#workshopname").addClass('selectpicker');
                $('#workshopname').multiselect('rebuild');
                //$("#")
            }


        }


    });

    cresBasedOnLocations(sel);


}

//var dates = $("#insdate12345").datepicker({

//    dateFormat: 'yy-mm-dd',
//    maxDate: "+30d",
//    minDate: 0,
//    onSelect: function (selectedDate) {
//        var option = this.id == "insdate12345" ? "minDate" : "maxDate",
//            instance = $(this).data("datepicker"),
//            date = $.datepicker.parseDate(instance.settings.dateFormat || $.datepicker._defaults.dateFormat, selectedDate, instance.settings);
//        dates.not(this).datepicker("option", option, date);

//        // ajaxAutoSASelectionListInsurance("insdate12345","serviceAdvisor");
//    }

//});

function ajaxAutoSASelectionListInsurance(selectedDate, serviceAdv) {

    // alert("ajaxAutoSASelectionListIns");

    var date = document.getElementById(selectedDate).value;

    var preSaDetails = document.getElementById('preSaDetailsIns');
    var newSaDetails = document.getElementById('newSaDetailsIns');
    $('#' + serviceAdv + ' option').remove();
    $('#serviceAdvisorTempIns option').remove();
    $.ajax({
        url: "/CRE/callDispositionPageIns/" + preSaDetails.value
    }).done(function (data) {
        preSaDetails.value = "NA";
        newSaDetails.value = "NA";
    });
    return false;
}

// Final Premium value

function ajaxgetDealerOEM() {
    var urlLink = "/CRE/getOEMOfDealer";

    $.ajax({
        url: urlLink

    }).done(function (oem) {

        var oemIs = oem + " Mobile Support";

        // alert("ajaxgetDealerOEM");
        $('#oemvalue').html(oemIs);
        $('#oemvalue').val(oemIs);
    });


}

// Add show room on load

function ajaxAddShowRoom() {

    var urlLink = "/CRE/showRoomList";
    // alert("ajaxAddShowRoom");
    $.ajax({
        url: urlLink

    }).done(function (showRoomsData) {

        if (showRoomsData != null) {
            $('#ShowroomsSelectId').empty();
            var dropdown = document.getElementById("ShowroomsSelectId");
            dropdown[0] = new Option('Select', '0');
            for (var i = 0; i < showRoomsData.length; i++) {

                dropdown[dropdown.length] = new Option(showRoomsData[i].showroomName, showRoomsData[i].showRoom_id);
            }


        }

    });
}

function ajaxAddShowRoomIn() {

    var urlLink = "/CRE/showRoomList";
    // alert("ajaxAddShowRoom");
    $.ajax({
        url: urlLink

    }).done(function (showRoomsData) {

        if (showRoomsData != null) {
            $('#ShowroomsSelectIdInB').empty();
            var dropdown = document.getElementById("ShowroomsSelectIdInB");
            dropdown[0] = new Option('Select', '0');
            for (var i = 0; i < showRoomsData.length; i++) {

                dropdown[dropdown.length] = new Option(showRoomsData[i].showroomName, showRoomsData[i].showRoom_id);
            }


        }

    });
}







// onload get phone

function ajaxReqForCustomerPhone() {


    var customerid = document.getElementById('get_preffered_contact_num').value;
    // alert(customerid);


}




// ajax load of workshop summary based on dropdown selection

function ajaxCallToLoadWorkshopSummaryIn() {

    var selectedWorkshop = document.getElementById('workshopIn').value;

    var schDate = document.getElementById('date123456').value;

    if (selectedWorkshop != '0' && schDate != '') {
        $.blockUI();
    }

    var urlLink = "/CRE/listWorkshopSummary/" + selectedWorkshop + "/" + schDate + "";

    $.ajax({
        url: urlLink

    }).done(function (workshopSummaryList) {

        // if (workshopSummaryList != null) {

        var tableHeaderRowCount = 1;
        var table = document.getElementById('dataTables-example60');
        var rowCount = table.rows.length;
        for (var i = tableHeaderRowCount; i < rowCount; i++) {
            table.deleteRow(tableHeaderRowCount);
        }
        $('#dataTables-example60').dataTable().fnAddData([
            schDate,
            workshopSummaryList,
            '60'

        ]);

        $.unblockUI();

        // }
    });


}

function driverbasedOnWorkshopSelectionIn() {


    var workshopId = document.getElementById('workshopIn').value;
    var urlLink = "/CRE/listDrivers/" + workshopId + "";

    $.ajax({
        url: urlLink

    }).done(function (driverlist) {

        if (driverlist != null) {
            $('#driverIdSelectIn').empty();
            var dropdown = document.getElementById("driverIdSelectIn");
            dropdown[0] = new Option('Select', '0');
            for (var i = 0; i < driverlist.length; i++) {

                dropdown[dropdown.length] = new Option(driverlist[i].driverName, driverlist[i].id);
            }

        }
    });


}


function ajaxCallToLoadWorkshopSummary() {



    var selectedWorkshop = document.getElementById('workshop').value;
    var schDate = document.getElementById('date12345').value;

    if (selectedWorkshop != '0' && schDate != '') {
        $.blockUI();
    }

    var urlLink = "/CRE/listWorkshopSummary/" + selectedWorkshop + "/" + schDate + "";

    $.ajax({
        url: urlLink

    }).done(function (workshopSummaryList) {

        var tableHeaderRowCount = 1;
        var table = document.getElementById('dataTables-example60');
        var rowCount = table.rows.length;
        for (var i = tableHeaderRowCount; i < rowCount; i++) {
            table.deleteRow(tableHeaderRowCount);
        }

        $('#dataTables-example60').dataTable().fnAddData([
            schDate,
            workshopSummaryList,
            '60'

        ]);

        $.unblockUI();
    });

}
// //ajax load of workshop based on location selecttion for inbound

function ajaxCallToLoadWorkShopByCityIn() {

    var selectedCity = document.getElementById('cityIn').value;

    // alert(selectedCity);

    var urlLink = "/CRE/listWorkshops/" + selectedCity + "";

    $.ajax({
        url: urlLink

    }).done(function (workshoplist) {

        if (workshoplist != null) {
            $('#workshopIn').empty();
            var dropdown = document.getElementById("workshopIn");
            dropdown[0] = new Option('select', '0');
            for (var i = 0; i < workshoplist.length; i++) {

                dropdown[dropdown.length] = new Option(workshoplist[i].workshopName, workshoplist[i].id);
            }



        }
    });





}
// ajax to load CRE's by workshop


function ajaxCallToLoadCRESByWorkshop() {
    var workshopId = document.getElementById('workshop').value;

    // alert(workshopId);

    var urlLink = "/CRE/listCREsByWorkshop/" + workshopId + "";


    $.ajax({
        url: urlLink

    }).done(function (crelist) {

        console.log(crelist);

        $('#ddlCreIds').empty();
        var dropdown = document.getElementById("ddlCreIds");
        // dropdown[0]= new Option('Select All','Select');
        for (var i = 0; i < crelist.length; i++) {

            dropdown[dropdown.length] = new Option(crelist[i].username, crelist[i].id);
            $("#ddlCreIds").addClass('selectpicker');
            $('#ddlCreIds').multiselect('rebuild');




        }
    });
}

// ajax load of workshop based on location selecttion
// ajax load of workshop based on location selecttion


function ajaxCallToLoadWorkShopByCity(ThisDDL, BindingddlId) {


    var selectedCity = document.getElementById(ThisDDL).value;

    //alert(selectedCity);

    var urlLink = siteRoot + "/CallLogging/listWorkshops/";

    $.ajax({
        type: 'POST',
        url: urlLink,
        datatype: 'json',
        data: { selectedCity: selectedCity },
        cache: false,
        success: function (workshoplist) {

            if (workshoplist != null) {
                $('#' + BindingddlId).empty();
                var dropdown = document.getElementById(BindingddlId);
                dropdown[0] = new Option('--Select--', '');

                for (var i = 0; i < workshoplist.length; i++) {
                    $(dropdown).append('<option value="' + workshoplist[i].id + '">' + workshoplist[i].workshopName + '</option>');
                }

                if (BindingddlId == "SB_workshop") {
                    $('#' + BindingddlId).multiselect('rebuild');
                }
                
            }
        }, error(error) {
            console.log(error);
        }
    });



    //// alert(urlLink);
    // $.ajax({
    //     url: urlLink

    // }).done(function (workshoplist) {

    //     if (workshoplist != null) {
    //         $('#workshop').empty();
    //         var dropdown = document.getElementById("workshop"); 
    //   dropdown[0]= new Option('select','0');
    //    for(var i=0;i<workshoplist.length;i++){

    //     dropdown[dropdown.length] = new Option(workshoplist[i].workshopName,workshoplist[i].id);
    //    }



    //         }
    // });



}


function ajaxCallToLoadWorkShopByCitySB() {


    var selectedCity = document.getElementById('city_id').value;

    // alert(selectedCity);

    var urlLink = "/CRE/listWorkshops/" + selectedCity + "";
    // alert(urlLink);
    $.ajax({
        url: urlLink

    }).done(function (workshoplist) {

        if (workshoplist != null) {
            $('#workshop_id').empty();
            var dropdown = document.getElementById("workshop_id");
            dropdown[0] = new Option('select', '0');
            for (var i = 0; i < workshoplist.length; i++) {

                dropdown[dropdown.length] = new Option(workshoplist[i].workshopName, workshoplist[i].id);
            }



        }
    });



}

// In complaints workshop by city selection

function ajaxCallToLoadWorkShopByCityComplaints() {
    var selectedCity = document.getElementById('city').value;

    // alert(selectedCity);

    var urlLink = "/CRE/listWorkshops/" + selectedCity + "";
    // alert(urlLink);
    $.ajax({
        url: urlLink

    }).done(function (workshoplist) {

        if (workshoplist != null) {
            $('#workshopSelected').empty();
            var dropdown = document.getElementById("workshopSelected");
            dropdown[0] = new Option('--Select--', 'select');
            for (var i = 0; i < workshoplist.length; i++) {

                dropdown[dropdown.length] = new Option(workshoplist[i].workshopName, workshoplist[i].workshopName);
            }



        }
    });

}

function ajaxCallToLoadFunctionsByLocation() {

    var selectedCity = document.getElementById('city').value;


    var urlLink = "/CRE/listFunctionsByLocation/" + selectedCity + "";

    $.ajax({
        url: urlLink

    }).done(function (funclist) {

        if (funclist != null) {
            $('#FUNCTION').empty();
            var dropdown = document.getElementById("FUNCTION");
            dropdown[0] = new Option('--Select--', '0');
            for (var i = 0; i < funclist.length; i++) {

                dropdown[dropdown.length] = new Option(funclist[i], funclist[i]);
            }



        }
    });
}

//function ajaxLoadUserByFunc(){
//	var city=document.getElementById('city').value;
//	var func=document.getElementById('FUNCTION').value;

//	var urlLink="/CRE/listUsersByFuncandLoc/" + city + "/"+ func +"";

//    $.ajax({
//        url: urlLink

//    }).done(function (userlist) {

//        if (userlist != null) {
//            $('#OWNERSHIP').empty();
//            var dropdown = document.getElementById("OWNERSHIP"); 

//       for(var i=0;i<userlist.length;i++){

//        dropdown[dropdown.length] = new Option(userlist[i],userlist[i]);
//       }



//            }
//    });


//}

function loadLeadBasedOnLocationPSF() {

    var userLocation = document.getElementById('location_Id').value;
    var urlLink = "/CRE/leadBasedOnLocation/" + userLocation + "";
    // alert(userLocation);

    $.ajax({
        url: urlLink

    }).done(function (leadlist) {

        if (leadlist != null) {

            $('#insuranceLead1').empty();
            var dropdown = document.getElementById("insuranceLead1");
            dropdown[0] = new Option(leadlist[0].name, leadlist[0].name);

            $('#warrantyLead1').empty();
            var dropdown = document.getElementById("warrantyLead1");
            dropdown[0] = new Option(leadlist[1].name, leadlist[1].name);

            $('#vASLead1').empty();
            var dropdown = document.getElementById("vASLead1");
            dropdown[0] = new Option(leadlist[2].name, leadlist[2].name);

            $('#reFinanceLead1').empty();
            var dropdown = document.getElementById("reFinanceLead1");
            dropdown[0] = new Option(leadlist[3].name, leadlist[3].name);


            $('#sellOldCarLead1').empty();
            var dropdown = document.getElementById("sellOldCarLead1");
            dropdown[0] = new Option(leadlist[4].name, leadlist[4].name);

            $('#buyNewCarLead1').empty();
            var dropdown = document.getElementById("buyNewCarLead1");
            dropdown[0] = new Option(leadlist[5].name, leadlist[5].name);

            $('#usedCarLead1').empty();
            var dropdown = document.getElementById("usedCarLead1");
            dropdown[0] = new Option(leadlist[6].name, leadlist[6].name);


        }

    });


}


// old location lead



function loadLeadBasedOnLocation() {

    var moduletype = document.getElementById('typeOfDispoPageView').value;
    var urlLink = siteRoot + "/CallLogging/getLeadByUserLocation/";

    $.ajax({
        type: 'POST',
        url: urlLink,
        datatype: 'json',
        data: { moduleType: moduletype },
        cache: false,
        success: function (res) {
            if (res.success == true) {
                if (res.leadlist.length > 0) {
                    $('#insuranceLead').empty();
                    var dropdown = document.getElementById("insuranceLead");
                    dropdown[0] = new Option(res.leadlist[0].name, res.leadlist[0].name);

                    $('#warrantyLead').empty();
                    var dropdown = document.getElementById("warrantyLead");
                    dropdown[0] = new Option(res.leadlist[1].name, res.leadlist[1].name);

                    $('#vASLead').empty();
                    var dropdown = document.getElementById("vASLead");
                    dropdown[0] = new Option(res.leadlist[2].name, res.leadlist[2].name);

                    $('#reFinanceLead').empty();
                    var dropdown = document.getElementById("reFinanceLead");
                    dropdown[0] = new Option(res.leadlist[3].name, res.leadlist[3].name);


                    $('#sellOldCarLead').empty();
                    var dropdown = document.getElementById("sellOldCarLead");
                    dropdown[0] = new Option(res.leadlist[4].name, res.leadlist[4].name);

                    $('#buyNewCarLead').empty();
                    var dropdown = document.getElementById("buyNewCarLead");
                    dropdown[0] = new Option(res.leadlist[5].name, res.leadlist[5].name);

                    $('#usedCarLead').empty();
                    var dropdown = document.getElementById("usedCarLead");
                    dropdown[0] = new Option(res.leadlist[6].name, res.leadlist[6].name);

                    $('#insuranceLead1').empty();
                    var dropdown = document.getElementById("insuranceLead1");
                    dropdown[0] = new Option(res.leadlist[0].name, res.leadlist[0].name);

                    $('#warrantyLead1').empty();
                    var dropdown = document.getElementById("warrantyLead1");
                    dropdown[0] = new Option(res.leadlist[1].name, res.leadlist[1].name);

                    $('#vASLead1').empty();
                    var dropdown = document.getElementById("vASLead1");
                    dropdown[0] = new Option(res.leadlist[2].name, res.leadlist[2].name);

                    $('#reFinanceLead1').empty();
                    var dropdown = document.getElementById("reFinanceLead1");
                    dropdown[0] = new Option(res.leadlist[3].name, res.leadlist[3].name);


                    $('#sellOldCarLead1').empty();
                    var dropdown = document.getElementById("sellOldCarLead1");
                    dropdown[0] = new Option(res.leadlist[4].name, res.leadlist[4].name);

                    $('#buyNewCarLead1').empty();
                    var dropdown = document.getElementById("buyNewCarLead1");
                    dropdown[0] = new Option(res.leadlist[5].name, res.leadlist[5].name);

                    $('#usedCarLead1').empty();
                    var dropdown = document.getElementById("usedCarLead1");
                    dropdown[0] = new Option(res.leadlist[6].name, res.leadlist[6].name);

                    $('#insuranceLead2').empty();
                    var dropdown = document.getElementById("insuranceLead2");
                    dropdown[0] = new Option(res.leadlist[0].name, res.leadlist[0].name);

                    $('#warrantyLead2').empty();
                    var dropdown = document.getElementById("warrantyLead2");
                    dropdown[0] = new Option(res.leadlist[1].name, res.leadlist[1].name);

                    $('#vASLead2').empty();
                    var dropdown = document.getElementById("vASLead2");
                    dropdown[0] = new Option(res.leadlist[2].name, res.leadlist[2].name);

                    $('#reFinanceLead2').empty();
                    var dropdown = document.getElementById("reFinanceLead2");
                    dropdown[0] = new Option(res.leadlist[3].name, res.leadlist[3].name);


                    $('#sellOldCarLead2').empty();
                    var dropdown = document.getElementById("sellOldCarLead2");
                    dropdown[0] = new Option(res.leadlist[4].name, res.leadlist[4].name);

                    $('#buyNewCarLead2').empty();
                    var dropdown = document.getElementById("buyNewCarLead2");
                    dropdown[0] = new Option(res.leadlist[5].name, res.leadlist[5].name);

                    $('#usedCarLead2').empty();
                    var dropdown = document.getElementById("usedCarLead2");
                    dropdown[0] = new Option(res.leadlist[6].name, res.leadlist[6].name);
                }
            }
            else {
                alert(res.error);
            }

        }, error(error) {

        }
    });
}

function loadLeadBasedOnLocationDepartment(ddl) {
    // alert("hi");
    var j = 0;
    var selectDept = $('#depForFB').val();
    // alert("old selected department : "+selectDept);
    var moduletype = document.getElementById('typeOfDispoPageView').value;

    var dropdown = '';
    //ck added
    var dropdownid;

    if (ddl == null || ddl == undefined) {
        //ck Added
        dropdownid = "selected_department1";
        dropdown = document.getElementById("selected_department1");
    }
    else {
        dropdown = document.getElementById("selected_department");
    }

    var urlLink = siteRoot + "/CallLogging/getLeadByUserLocation/";

    $.ajax({
        type: 'POST',
        url: urlLink,
        datatype: 'json',
        async: false,
        data: { moduleType: moduletype },
        cache: false,
        success: function (res) {
            if (res.success == true) {
                if (res.leadlist.length != 0) {
                    //ck added
                    if (dropdown == "selected_department1") {
                        $('#selected_department1').removeClass('selectpicker');

                    }

                    $('.selected_department').empty();

                    dropdown[0] = new Option('select', '0');
                    for (var i = 0; i < res.leadlist.length; i++) {

                        // alert("coming res.leadlist[i].upsellLeadId :
                        // "+res.leadlist[i].upsellLeadId);
                        if (selectDept == res.leadlist[i].upsellLeadId) {
                            // alert("Matching");
                            dropdown[dropdown.length] = new Option(res.leadlist[i].departmentName, res.leadlist[i].id);
                            j = i;


                        } else {

                            dropdown[dropdown.length] = new Option(res.leadlist[i].departmentName, res.leadlist[i].id);
                        }

                    }

                    dropdown.options[j].selected = true;
                    //ck added
                    if (dropdownid == "selected_department1") {

                        $('#selected_department1').addClass('selectpicker');
                        $('#selected_department1').multiselect('rebuild');
                    }
                    //var dropdown1 = document.getElementById("selected_department");
                    var dropdown1 = $('#selected_department');
                    dropdown1[0] = new Option('select', '0');

                    for (var i = 0; i < res.leadlist.length; i++) {
                        dropdown1[dropdown1.length] = new Option(res.leadlist[i].departmentName, res.leadlist[i].id);
                    }

                    //var dropdown2 = document.getElementById("selected_department2");
                    ////dropdown2[0] = new Option('select', '0');
                    //for (var i = 0; i < res.leadlist.length; i++) {
                    //    dropdown2[dropdown2.length] = new Option(res.leadlist[i].departmentName, res.leadlist[i].id);
                    //}




                }
            }
            else {
                alert(res.error);
            }
        }, error(error) {

        }
    });
}


function ajaxLeadTagByDepartmentInbound() {
    var userLocation = document.getElementById('location_Id').value;
    var departmentName = document.getElementById('selected_department').value;
    var urlLink = siteRoot + "/CallLogging/getLeadTagByDepartment/";

    $.ajax({
        type: 'POST',
        url: urlLink,
        datatype: 'json',
        data: { locId: userLocation, deptId: departmentName },
        cache: false,
        success: function (res) {
            if (res.success == true) {
                if (res.leadlist != null) {
                    $('#LeadTagsByLocation2').empty();
                    var dropdown = document.getElementById("LeadTagsByLocation2");

                    for (var i = 0; i < res.leadlist.length; i++) {

                        dropdown[dropdown.length] = new Option(res.leadlist[i], res.leadlist[i]);

                    }



                }
            }
            else {
                alert(res.error);
            }
        }, error(error) {

        }
    });
}

function ajaxLeadOnDissatisfiedWithSales(upselIDTag) {

    var userLocation = document.getElementById('location_Id').value;
    // alert(upselIDTag);

    var urlLink = siteRoot + "/CallLogging/getTagNameByUpselLeadType/";
    $.ajax({
        type: 'POST',
        url: urlLink,
        datatype: 'json',
        data: { locId: userLocation, upselId: upselIDTag },
        cache: false,
        success: function (res) {
            if (res.success == true) {
                if (res.leadlist != null) {

                    if (upselIDTag == 26) {
                        // alert("noServiceReasonTaggedTo sales");

                        $('#noServiceReasonTaggedTo').empty();
                        var dropdown = document.getElementById("noServiceReasonTaggedTo");

                        for (var i = 0; i < res.leadlist.length; i++) {

                            dropdown[dropdown.length] = new Option(res.leadlist[i], res.leadlist[i]);

                        }
                    }

                    if (upselIDTag == 27) {

                        // alert("noServiceReasonTaggedTo insurance");

                        $('#noServiceReasonTaggedTo1').empty();
                        var dropdown = document.getElementById("noServiceReasonTaggedTo1");

                        for (var i = 0; i < res.leadlist.length; i++) {

                            dropdown[dropdown.length] = new Option(res.leadlist[i], res.leadlist[i]);

                        }



                    }
                    if (upselIDTag == 28) {

                        // alert("noServiceReasonTaggedTo insurance");

                        $('#noServiceReasonTaggedToClaims').empty();
                        var dropdown = document.getElementById("noServiceReasonTaggedToClaims");

                        for (var i = 0; i < res.leadlist.length; i++) {

                            dropdown[dropdown.length] = new Option(res.leadlist[i], res.leadlist[i]);

                        }



                    }
                    if (upselIDTag == 29) {

                        // alert("noServiceReasonTaggedTo insurance");
                        //Dissat with previous service
                        $('#assignedToSA').empty();
                        var dropdown = document.getElementById("assignedToSA");

                        for (var i = 0; i < res.leadlist.length; i++) {

                            dropdown[dropdown.length] = new Option(res.leadlist[i], res.leadlist[i]);

                        }
                    }
                }
            }
            else {
                alert(res.error);
            }
        }, error(error) {

        }
    });
}
//ajaxLeadTagByDepartment();
function ajaxLeadTagByDepartment(ddl, ddlSelf) {


    var userLocation = document.getElementById('location_Id').value;
    var departmentName = document.getElementById('selected_department').value;
    var departmentNameOther = "";

    if (ddlSelf != null || ddlSelf != undefined) {
        departmentNameOther = document.getElementById(ddlSelf).value;
    }
    else {
        departmentNameOther = document.getElementById("selected_department1").value;
    }
    var dropdown = "";

    if (ddl == null || ddl == undefined) {
        dropdown = document.getElementById("LeadTagsByLocation1");
    }
    else {
        dropdown = document.getElementById(ddl);
    }
    // alert(departmentName);
    // alert(departmentNameOther);
    if (departmentNameOther === '0') {
        // alert("departmentName");
        var urlLink = siteRoot + "/CallLogging/getLeadTagByDepartment/";
        //AlreadytagsByLocation


        $.ajax({
            type: 'POST',
            url: urlLink,
            datatype: 'json',
            data: { locId: userLocation, deptId: departmentNameOther },
            cache: false,
            success: function (res) {
                if (res.success == true) {
                    if (res.leadlist != null) {
                        $('#LeadTagsByLocation').empty();


                        for (var i = 0; i < res.leadlist.length; i++) {

                            dropdown[dropdown.length] = new Option(res.leadlist[i], res.leadlist[i]);

                        }
                    }
                }
                else {
                    alert(res.error);
                }
            }, error(error) {

            }
        });
    } else {
        // alert("departmentNameOther");
        var urlLink = siteRoot + "/CallLogging/getLeadTagByDepartment/";

        $.ajax({
            type: 'POST',
            url: urlLink,
            datatype: 'json',
            data: { locId: userLocation, deptId: departmentNameOther },
            cache: false,
            success: function (res) {
                if (res.success == true) {
                    if (res.leadlist != null) {
                        $('#LeadTagsByLocation1').empty();
                        //var dropdown = document.getElementById(dropdown);

                        for (var i = 0; i < res.leadlist.length; i++) {

                            dropdown[dropdown.length] = new Option(res.leadlist[i], res.leadlist[i]);

                        }



                    }
                }
                else {
                    alert(res.error);
                }
            }, error(error) {

            }
        });
    }
}



function ajaxsearchVehicle() {
    var veh_number = document.getElementById('vehicleRegNo').value;
    var urlDisposition = "/CRE/searchvehRegNo/" + veh_number + "";
    $.ajax({
        url: urlDisposition

    }).done(function (data) {
        if (data != null) {
            console.log(data);
            $("#model").val(data.model);
            $("#variant").val(data.variant);
            $("#saledate").val(data.saleDate);
            $("#lastservice").val(data.lastServiceDate);
            $("#serviceadvisor").val(data.serviceadvisor);
            // $("#workshop").val(data.serviceadvisor);
            $("#customerName").val(data.customerName);
            $("#email").val(data.customerEmail);
            $("#customerPhone").val(data.customerMobileNo);
            $("#chassisno").val(data.chassisNo);
            $("#address").val(data.preferredAdress);
        }
    });
}

// Add the phone number
function ajaxCallForAddPhoneNo() {
    // alert("add phone number");
    var mobile_phone_number = document.getElementById('newNumberData').value;
    var land_phone_number = document.getElementById('newNumber').value;
    var customer_id = document.getElementById('customer_Id').value;

    // alert(customer_id);
    // alert("selectAgent :"+selectAgent);
    if (mobile_phone_number == '') {

        var urlDisposition = "/CRE/addphone/" + land_phone_number + "/" + customer_id + "";
    } else {

        var urlDisposition = "/CRE/addphone/+91" + mobile_phone_number + "/" + customer_id + "";
    }
    $.ajax({
        url: urlDisposition

    }).done(function (data) {

        if (data != null) {

            // alert("success");

            Lobibox.alert('info', {
                msg: "Added Phone Number"
            });
            var ddl_phone = document.getElementById('ddl_phone_no');
            if (mobile_phone_number == '') {
                ddl_phone.options[ddl_phone.options.length] = new Option(land_phone_number, land_phone_number);
            } else {
                ddl_phone.options[ddl_phone.options.length] = new Option(mobile_phone_number, mobile_phone_number);
            }
        }
    });

}


function ajaxAddChassisno() {
    $.blockUI();
    var chassisNo = document.getElementById('chassisNo').value;
    // alert(chassisNo);
    var customer_id = document.getElementById('vehical_Id').value;
    // alert(customer_id);
    var urlDisposition = "/CRE/saveaddcustomerchassisno/" + chassisNo + "/" + customer_id + "";
    $.ajax({
        url: urlDisposition

    }).done(function (data) {

        if (data != null) {

            $('#chassisNo').attr('readonly', true);
            $("#editChassisno").css("display", "block");
            $("#updateChassisno").css("display", "none");
            $.unblockUI();

        }
    });

}

function ajaxAddEngineno() {
    $.blockUI();
    var engineNo = document.getElementById('engineNo').value;
    var customer_id = document.getElementById('vehical_Id').value;
    var urlDisposition = "/CRE/saveaddcustomerengineno/" + engineNo + "/" + customer_id + "";
    $.ajax({
        url: urlDisposition

    }).done(function (data) {

        if (data != null) {
            $('#engineNo').attr('readonly', true);
            $("#editEngineno").css("display", "block");
            $("#updateEngineno").css("display", "none");
            $.unblockUI();


        }
    });

}

function ajaxAddRegistrationno() {
    $.blockUI();
    var vehicleRegNo = $('.vehicalRegNo').val();
    var customer_id = document.getElementById('vehical_Id').value;
    var urlDisposition = "/CRE/saveaddcustomervregistrationno/" + vehicleRegNo + "/" + customer_id + "";
    $.ajax({
        url: urlDisposition

    }).done(function (data) {

        if (data != null) {
            $('.vehicalRegNo').attr('readonly', true);
            $("#editRegistrationno").css("display", "block");
            $("#updateRegistrationno").css("display", "none");
            $.unblockUI();
        }
    });

}

$('.phoneinfobtn').click(function () {
    $('#tpreffered_mode_contact').css('display', 'block');
    $('#tpreffered_mode_contact_edit').css('display', 'block');
    $('#modeOfCon').css('display', 'none');
    $('#daysWeek').css('display', 'none');
    $('#tpreffered_day_contact').css('display', 'block');
    $('#tpreffered_day_contact_edit').css('display', 'block');

    var lengthOfPadd = $(".permanentAddress").val().length;
    //var lengthOfRadd=$("#residenceAddress").val().length;
    //var lengthOfOadd=$("#officeAddress").val().length;
    if (lengthOfPadd <= 10) {
        $(".permanentAddress").val('');
    }
    //if(lengthOfRadd<= 10){
    //$("#residenceAddress").val('');
    //} 
    //if(lengthOfOadd<= 10){
    //$("#officeAddress").val('');
    //}

});


function ajaxCallForAddcustomerinfo() {
    //alert("customerinfo update");

    var y = $("#preffered_contact_num option:selected").text();
    var zx = $("#preffered_contact_num option:selected").val();

    // alert(zx);

    // $('#ddl_phone_no').val(y);
    $('#pref_number_callini').val(zx);

    $("#ddl_phone_no option[value='" + y + "']").remove();
    $("#ddl_phone_no").prepend($("<option></option>").text(y).html(y).val(y));
    $("#ddl_phone_no option:first").prop('selected', true);



    var prefEmail = $("#email option:selected").text();
    $('#ddl_email').val(prefEmail);

    // alert(prefEmail);


    var selected_days;
    var x = document.getElementById("preffered_day_contact");

    var j = 0;
    for (var i = 0; i < x.options.length; i++) {
        if (x.options[i].selected == true) {
            if (j == 0)
                selected_days = x.options[i].value;
            else
                selected_days = selected_days + "," + x.options[i].value;

            console.log(selected_days);
            j++;
        }
    }


    var y = document.getElementById("preffered_contact_num");

    var email = document.getElementById("email");

    var selected_cont;
    var z = document.getElementById("preffered_mode_contact");
    var j = 0;
    for (var i = 0; i < z.options.length; i++) {
        if (z.options[i].selected == true) {

            if (j == 0)
                selected_cont = z.options[i].value;
            else
                selected_cont = selected_cont + "," + z.options[i].value;

            j++;
        }
    }

    var preffed_flag = $("input[name=ADDRESS]:checked").val();
    var dnd_flag = $("input[name=doNotDisturb]:checked").val();
    // alert(dnd_flag);
    // alert(preffed_flag);
    if (preffed_flag == 0) {
        var preferredcomAddress = document.getElementById('permanentAddress').value;


    } else if (preffed_flag == 2) {
        var preferredcomAddress = document.getElementById('residenceAddress').value;
    } else if (preffed_flag == 1) {
        var preferredcomAddress = document.getElementById('officeAddress').value;
    }

    var customer_id = document.getElementById('customer_Id').value;

    var email = document.getElementById('email').value;

    var wyzuser_id = document.getElementById('wyzUser_Id').value;
    var customerNameEdit = document.getElementById('customerNameEdit').value;
    var driverNameEdit = document.getElementById('driverNameEdit').value;
    var dob = document.getElementById('dob').value;
    var anniversary_day = document.getElementById('anniversary_date').value;
    var preferred_mobile_no_value = y.options[y.selectedIndex].value;

    var pref_day_contact_value = selected_days;
    var preffered_mode_contact_value = selected_cont;

    if (pref_day_contact_value == undefined) {
        pref_day_contact_value = $("#tpreffered_day_contact").val();
    }
    if (preffered_mode_contact_value == undefined) {
        preffered_mode_contact_value = $("#tpreffered_mode_contact").val();
    }
    var dateStarttime = document.getElementById('dateStarttime').value;
    var dateEndtime = document.getElementById('dateEndtime').value;
    var permanentAddress = document.getElementById('permanentAddress').value;
    var residenceAddress = document.getElementById('residenceAddress').value;
    var officeAddress = document.getElementById('officeAddress').value;
    var custComments = document.getElementById('custEditComment').value;

    var preff_workshop = document.getElementById('preff_workshop').value;
    // alert(custComments);

    var cusInfo = { "dob": dob, "wyzuser_id": wyzuser_id, "customerNameEdit": customerNameEdit, "driverNameEdit": driverNameEdit, "preffed_flag": preffed_flag, "preferredcomAddress": preferredcomAddress, "preferred_mobile_no": preferred_mobile_no_value, "permanentAddress": permanentAddress, "residenceAddress": residenceAddress, "officeAddress": officeAddress, "customerId": customer_id, "email": prefEmail, "anniversary_date": anniversary_day, "preferred_day": pref_day_contact_value, "mode_of_contact": preffered_mode_contact_value, "preferred_time_start": dateStarttime, "preferred_time_end": dateEndtime, "customerComments": custComments, "dnd_flag": dnd_flag, "preff_workshop": preff_workshop };
    var cust_info = [];
    cust_info.push(cusInfo);
    jsonData = JSON.stringify(cusInfo);
    var urlPath = "/CRE/saveaddcustomerinfo";
    $.ajax({
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: jsonData,
        url: urlPath,
        success: function (json) {
            console.log("add json :" + json);
            $('#tpreffered_mode_contact').val(preffered_mode_contact_value);

            $('#tpreffered_day_contact').val(pref_day_contact_value);

            $('#driverIdUpdate').val(driverNameEdit);


            // console.log("prefEmail json :"+prefEmail);

            $('#ddl_email').html(prefEmail);

            $('#doa_id').html(dob);

            $('#dob_id').html(anniversary_day);

            if (permanentAddress != "") {
                console.log("Per");
                $('#pFlag').val(3);
                $('#prefAdressUpdate').html(permanentAddress);

            } else if (residenceAddress != "") {
                console.log("res");
                $('#pFlag').val(2);

                $('#prefAdressUpdate').html(residenceAddress);

            } else if (officeAddress != "") {
                console.log("off");
                $('#pFlag').val(1);
                $('#prefAdressUpdate').html(officeAddress);

            }
            Lobibox.notify('success', {
                continueDelayOnInactiveTab: true,
                msg: 'Data Update successfully.'
            });

            // window.location.reload();
        },
        error: function () {


        }
    });

}

// Add Email in customer edit

//$("#saveEmailCust").on('click', function(){
//	 var lastValue = parseFloat($('.email').val()) + 1;


//	  var custEmail =$(".email").val();
//	// alert(custEmail.length);
//		if(custEmail.length <10){

//			Lobibox.notify('warning', {
//								continueDelayOnInactiveTab: true,
//								msg: 'Email Id Is Invalid.'
//							});
//							return false;

//		}else{
//			$("#email").prepend($("<option ></option>").text(lastValue).html(custEmail));

//	    	 $("#email option:first").prop('selected', true);

//		}


//	    var wyzUser_id = document.getElementById('wyzUser_Id').value;
//	    var customer_Id = document.getElementById('customer_Id').value;
//	    var urlDisposition = "/CRE/saveaddcustomerEmail/" + custEmail + "/" + wyzUser_id + "/" + customer_Id +"";

//	    $.ajax({
//	        url: urlDisposition

//	    }).done(function (data) {

//	     $('#ddl_email').val(custEmail);
//	    });

//	});


// CAll Dispositions Tabs
// function ajaxCallForFollowUpRequired() {
// $.blockUI();
// var tables0 = $('#dataTables-example0').DataTable();
// tables0.clear().draw();
// var selectAgent = document.getElementById('user').value;
//
// //alert("selectAgent :"+selectAgent);
// var urlDisposition = "/CRE/getFollowUpRequiredData/" + selectAgent + "";
// $.ajax({
// url: urlDisposition
//
// }).done(function (data) {
//
// if (data != null) {
//
// $.unblockUI();
// var tableHeaderRowCount = 1;
// var table = document.getElementById('dataTables-example0');
// var rowCount = table.rows.length;
// for (var i = tableHeaderRowCount; i < rowCount; i++) {
// table.deleteRow(tableHeaderRowCount);
// }
// var customer_list = data.customerLoadList;
// var vehical_list = data.vehicleLoadList;
// var srdisposition_list = data.srdispositionLoadList;
// var interaction_list = data.callInteractionLoadList;
// var service_list = data.serviceLoadList;
//            
//           
//            
//            
// for (i = 0; i < interaction_list.length; i++) {
// var html0 = '<a href="/' + interaction_list[i].dealerCode +
// '/CRE/toeditcallLog/' + interaction_list[i].id + '"><i class="fa
// fa-pencil-square-o" data-toggle="tooltip" title="edit"></i></a>';
// var span0 = "<span class=\"label label-primary\">" +
// srdisposition_list[i].callDisposition + "</span>";
//
// var tools0 = '<a href="/' + interaction_list[i].dealerCode +
// '/CRE/getFollowUpCallDispositionPage/' + interaction_list[i].id + '"><i
// class="fa fa-pencil-square" data-toggel="tooltip" title="Disposition"
// style="font-size:30px;color:#DD4B39;"></i></a>';
//
// $('#dataTables-example0').dataTable().fnAddData([
// html0,
// customer_list[i].customerName,
// customer_list[i].customerPhone,
// vehical_list[i].vehicleRegNo,
// vehical_list[i].nextServicedate,
// srdisposition_list[i].followUpDate,
// srdisposition_list[i].followUpTime,
// span0,
// tools0
// ]);
// }
// }
// });
// }
// function ajaxCallForServiceBooked() {
// $.blockUI();
// var tables3 = $('#dataTables-example3').DataTable();
// tables3.clear().draw();
// var selectAgent = document.getElementById('user').value;
//
// //alert("selectAgent for servicebooked:"+selectAgent);
// var urlDisposition = "/CRE/getServiceBookedData/" + selectAgent + "";
// $.ajax({
// url: urlDisposition
//
// }).done(function (data) {
//
// if (data != null) {
// $.unblockUI();
//
// var tableHeaderRowCount = 1;
// var table = document.getElementById('dataTables-example3');
// var rowCount = table.rows.length;
// for (var i = tableHeaderRowCount; i < rowCount; i++) {
// table.deleteRow(tableHeaderRowCount);
// }
//
// var customer_list = data.customerLoadList;
// var vehical_list = data.vehicleLoadList;
// var srdisposition_list = data.srdispositionLoadList;
// var interaction_list = data.callInteractionLoadList;
// var servicebooked_List = data.servicebookedLoadList;
// var service_list = data.serviceLoadList;
//
// for (i = 0; i < interaction_list.length; i++) {
//
//
// var html3 = '<a href="/' + interaction_list[i].dealerCode +
// '/CRE/toeditcallLog/' + interaction_list[i].id + '"><i class="fa
// fa-pencil-square-o" data-toggle="tooltip" title="edit"></i></a>';
//
// var span3 = "<span class=\"label label-primary\">" +
// srdisposition_list[i].callDisposition + "</span>";
//
// var tools3 = '<a href="/' + interaction_list[i].dealerCode +
// '/CRE/getFollowUpCallDispositionPage/' + interaction_list[i].id + '"><i
// class="fa fa-pencil-square" data-toggel="tooltip" title="Disposition"
// style="font-size:30px;color:#DD4B39;"></i></a>';
//
// $('#dataTables-example3').dataTable().fnAddData([
// html3,
// customer_list[i].customerName,
// customer_list[i].customerPhone,
// vehical_list[i].vehicleRegNo,
// vehical_list[i].nextServicedate,
// servicebooked_List[i].serviceScheduledDate,
// servicebooked_List[i].serviceScheduledTime,
// span3,
// tools3
// ]);
//
// }
// } else {
//
// alert("Data is null");
// }
// });
//
// }

// function ajaxCallForServiceNotRequired() {
// $.blockUI();
// var tables2 = $('#dataTables-example2').DataTable();
// tables2.clear().draw();
// var selectAgent = document.getElementById('user').value;
//
// // alert("selectAgent for ServiceNotRequired:"+selectAgent);
// var urlDisposition = "/CRE/getServiceNotRequiredData/" + selectAgent + "";
// $.ajax({
// url: urlDisposition
//
// }).done(function (data) {
//
// if (data != null) {
// $.unblockUI();
// //alert("success");
//
// var tableHeaderRowCount = 1;
// var table = document.getElementById('dataTables-example2');
// var rowCount = table.rows.length;
// for (var i = tableHeaderRowCount; i < rowCount; i++) {
// table.deleteRow(tableHeaderRowCount);
// }
//
// var customer_list = data.customerLoadList;
// var vehical_list = data.vehicleLoadList;
// var srdisposition_list = data.srdispositionLoadList;
// var interaction_list = data.callInteractionLoadList;
// var service_list = data.serviceLoadList;
//
// for (i = 0; i < interaction_list.length; i++) {
//
//
// var html1 = '<a href="/' + interaction_list[i].dealerCode +
// '/CRE/toeditcallLog/' + interaction_list[i].id + '"><i class="fa
// fa-pencil-square-o" data-toggle="tooltip" title="edit"></i></a>';
//
// var span1 = "<span class=\"label label-primary\">" +
// srdisposition_list[i].callDisposition + "</span>";
//
// var tools1 = '<a href="/' + interaction_list[i].dealerCode +
// '/CRE/getFollowUpCallDispositionPage/' + interaction_list[i].id + '"><i
// class="fa fa-pencil-square" data-toggel="tooltip" title="Disposition"
// style="font-size:30px;color:#DD4B39;"></i></a>';
//
// $('#dataTables-example2').dataTable().fnAddData([
// html1,
// customer_list[i].customerName,
// customer_list[i].customerPhone,
// vehical_list[i].vehicleRegNo,
// vehical_list[i].nextServicedate,
// span1,
// srdisposition_list[i].noServiceReason,
// tools1
// ]);
//
//
//
//
// }
//
// } else {
//
// alert("Data is null");
// }
// });
//
// }

// function ajaxCallForNonContacts() {
// $.blockUI();
// var tables1 = $('#dataTables-example1').DataTable();
// tables1.clear().draw();
//
// var selectAgent = document.getElementById('user').value;
//
// //alert("selectAgent for NonContacts:"+selectAgent);
// var urlDisposition = "/CRE/getNonContactsData/" + selectAgent + "";
// $.ajax({
// url: urlDisposition
//
// }).done(function (data) {
//
// if (data != null) {
//
// //alert("success");
// $.unblockUI();
// var tableHeaderRowCount = 1;
// var table = document.getElementById('dataTables-example1');
// var rowCount = table.rows.length;
// for (var i = tableHeaderRowCount; i < rowCount; i++) {
// table.deleteRow(tableHeaderRowCount);
// }
//            
// var customer_list = data.customerLoadList;
// var vehical_list = data.vehicleLoadList;
// var srdisposition_list = data.srdispositionLoadList;
// var interaction_list = data.callInteractionLoadList;
// var service_list = data.serviceLoadList;
//            
//            
//
// for (i = 0; i < interaction_list.length; i++) {
// var html = '<a href="/' + interaction_list[i].dealerCode +
// '/CRE/toeditcallLog/' + interaction_list[i].id + '"><i class="fa
// fa-pencil-square-o" data-toggle="tooltip" title="edit"></i></a>';
//
// var span = "<span class=\"label label-primary\">" +
// srdisposition_list[i].callDisposition + "</span>";
//
// var tools = '<a href="/' + interaction_list[i].dealerCode +
// '/CRE/getFollowUpCallDispositionPage/' + interaction_list[i].id + '"><i
// class="fa fa-pencil-square" data-toggel="tooltip" title="Disposition"
// style="font-size:30px;color:#DD4B39;"></i></a>';
//
// $('#dataTables-example1').dataTable().fnAddData([
// html,
// customer_list[i].customerName,
// customer_list[i].customerPhone,
// vehical_list[i].vehicleRegNo,
// vehical_list[i].nextServicedate,
// span,
// tools
// ]);
// }
// } else {
//
// alert("Data is null");
// }
// });
//
// }

// function ajaxCallForDroppedCalls() {
// $.blockUI();
//
// var tables4 = $('#dataTables-example4').DataTable();
// tables4.clear().draw();
// var selectAgent = document.getElementById('user').value;
//
// //alert("selectAgent for DroppedCalls:"+selectAgent);
// var urlDisposition = "/CRE/getDroppedCallsData/" + selectAgent + "";
// $.ajax({
// url: urlDisposition
//
// }).done(function (data) {
//
// if (data != null) {
//
//
// var tableHeaderRowCount = 1;
// var table = document.getElementById('dataTables-example4');
// var rowCount = table.rows.length;
// for (var i = tableHeaderRowCount; i < rowCount; i++) {
// table.deleteRow(tableHeaderRowCount);
// }
//
// var customer_list = data.customerLoadList;
// var vehical_list = data.vehicleLoadList;
// var srdisposition_list = data.srdispositionLoadList;
// var interaction_list = data.callInteractionLoadList;
// var service_list = data.serviceLoadList;
//
// for (i = 0; i < interaction_list.length; i++) {
//              	
//
//
// var span4 = "<span class=\"label label-primary\">" +
// srdisposition_list[i].callDisposition + "</span>";
//
//
// $('#dataTables-example4').dataTable().fnAddData([
//                    
// customer_list[i].customerName,
// customer_list[i].customerPhone,
// vehical_list[i].vehicleRegNo,
// vehical_list[i].nextServicedate,
// span4
// ]);
//
//
// }
// } else {
//
// alert("Data is null");
// }
// });
// $.unblockUI();
// }
//


// function ajaxCallForAssignedInt() {
// $.blockUI();
// var tables5 = $('#dataTables-example5').DataTable();
// tables5.clear().draw();
// var selectAgent = document.getElementById('user').value;
//
// //alert("selectAgent for DroppedCalls:"+selectAgent);
// var urlDisposition = "/CRE/getAssignedCalls/" + selectAgent + "";
// $.ajax({
// url: urlDisposition
//
// }).done(function (data) {
//
// if (data != null) {
// $.unblockUI();
// //alert("success");
//
// var tableHeaderRowCount = 1;
// var table = document.getElementById('dataTables-example5');
// var rowCount = table.rows.length;
// for (var i = tableHeaderRowCount; i < rowCount; i++) {
// table.deleteRow(tableHeaderRowCount);
// }
//
// var customer_list = data.customerLoadList;
// var vehical_list = data.vehicleLoadList;
// var wyzUser_list = data.wyzUserLoadList;
// var assign_list = data.assignedInteractionLoadList;
// var service_list = data.serviceLoadList;
//
//
// for (i = 0; i < assign_list.length; i++) {
//
// var tools5 = '<a href="/CRE/getCallDispositionPage/' + assign_list[i].id +
// '"><i class="fa fa-pencil-square" data-toggel="tooltip" title="Disposition"
// style="font-size:30px;color:#DD4B39;"></i></a>';
//
// $('#dataTables-example5').dataTable().fnAddData([
//                    
// customer_list[i].customerName,
// vehical_list[i].vehicleRegNo,
// vehical_list[i].model,
// customer_list[i].customerCategory,
// '-', //loyality
// vehical_list[i].nextServicedate, //duedate
// vehical_list[i].nextServicetype, //type
// vehical_list[i].forecastLogic, //Forecast
// '-', //PSFStatus
// '-', //DND
// '-', //Complaint
// tools5
// ]);
//
// }
// } else {
//
// alert("Data is null");
// }
// });
//
//
//
// }
//


function assignCallToCRE() {

    var selectUser = document.getElementById('data');
    var selectAgent = "";

    for (var i = 0; i < selectUser.length; i++) {

        if (i == (selectUser.length - 1)) {

            selectAgent = selectAgent + selectUser.options[i].value;
        } else {

            selectAgent = selectAgent + selectUser.options[i].value + ",";
        }
    }
    var fromData = document.getElementById('singleData').value;
    var toDate = document.getElementById('tranferData').value;


    var urlDisposition = "/CRE/assignCalls/" + selectAgent + "/" + fromData + "/" + toDate + "";
    // var urlString="/"+dealercode+"/CRE/getDispositionPageOfTab";
    // alert("hi");
    Lobibox.confirm({
        msg: "Are you sure you want to assign this Calls?",
        callback: function ($this, type) {
            if (type === 'yes') {

                $.ajax({
                    url: urlDisposition
                }).done(function (data) {

                    // window.location = urlString;
                    Lobibox.alert('info', {
                        msg: "Call Interactions are assigned"
                    });

                });

            } else if (type === 'no') {


            }

        }
    });

}
function getChangeAssignmentCalls() {
    $.blockUI();
    var selectUser = document.getElementById('selectedValues');
    var selectAgent = "";

    for (var i = 0; i < selectUser.length; i++) {
        if (selectUser.options[i].selected) {
            if (selectAgent === "") {

                selectAgent = selectUser.options[i].value;
            } else {

                selectAgent = selectAgent + "," + selectUser.options[i].value;
            }
        }
    }

    var urlDisposition = "/CREManager/getReAssignmentCalls/" + selectAgent + "";

    $.ajax({
        url: urlDisposition

    }).done(function (data) {

        if (data != null) {
            $.unblockUI();
            // alert("success");

            var tableHeaderRowCount = 1;
            var table = document.getElementById('dataTables-example51');
            var rowCount = table.rows.length;
            for (var i = tableHeaderRowCount; i < rowCount; i++) {
                table.deleteRow(tableHeaderRowCount);
            }
            var customer_list = data.customerLoadList;
            var vehical_list = data.vehicleLoadList;
            var wyzUser_list = data.wyzUserLoadList;

            var service_list = data.serviceLoadList;



            for (i = 0; i < wyzUser_list.length; i++) {

                $('#dataTables-example51').dataTable().fnAddData([
                    wyzUser_list[i].username,
                    customer_list[i].customerName,
                    customer_list[i].customerPhone,
                    vehical_list[i].vehicleRegNo,
                    vehical_list[i].model,
                    service_list[i].saleDate,
                    vehical_list[i].nextServicedate

                ]);


            }
        } else {

            alert("Data is null");
        }
    });



}
function ajaxRequest_getForm() {
    var uploadForm = document.getElementById('upload_format_name');
    var selected_part = uploadForm.options[uploadForm.selectedIndex].value;
    var urlPath = "/uploadFormat/getFormElement/" + selected_part;

    var x = document.getElementById('upload_format_name');
    var createform = document.createElement('form');
    x.appendChild(createform);

    $.ajax({
        url: urlPath
    }).done(function (data) {
        var name = data[0];
        var type = data[1];
        $("#tableFormat").find("tr").remove();
        th = $('<tr>');
        th.append('<th>Required Column</th>');
        th.append('<th >Data Type</th>');
        th.append('<th>Matching Excel Column Name</th>');

        $('#tableFormat').append(th);

        for (i = 0; i < name.length; i++) {

            tr = $('<tr/>');
            tr.append('<td><input  readonly style="border:none" id="upload_sheet_data[' + i + '].model_column" name="upload_sheet_data[' + i + '].model_column" value="' + name[i].replace(/_/g, ' ') + '">')
            tr.append('<td><input readonly style="border:none" id="upload_sheet_data[' + i + '].data_type" name="upload_sheet_data[' + i + '].data_type" value="' + type[i].toString().split('.').pop() + '">')
            tr.append('<td><input type="text" id="upload_sheet_data[' + i + '].excel_column" name="upload_sheet_data[' + i + '].excel_column"></td>');
            $("#tableFormat").append(tr);

        }

    });
    return false;
}


function ajaxMap_MasterDataFormat(table_name, upload_function) {
    var sheet_name = document.getElementById('txt_sheetname').value;

    var uploadForm = document.getElementById('upload_form_name');
    if (uploadForm != null) {
        var selected_part = uploadForm.options[uploadForm.selectedIndex].value;
    } else {
        var selected_part = document.getElementById('upload_transaction_name').value;
    }
    // var json_uploaded = document.getElementById('out').innerText;

    var urlpath = "/uploadFormat/MapMasterFormat/" + selected_part;
    $.ajax({
        url: urlpath
    }).done(function (json) {
        json_excel_uploaded = output_excel_data;
        output_excel_data = '';
        if ($.fn.DataTable.isDataTable('#' + table_name)) {
            var table = $('#' + table_name).DataTable();
            table.clear();
            table.destroy();
        }
        $('#' + table_name + ' thead tr').empty();
        th = $('<tr />');
        var array_header = [];
        for (var i = 0; i < json.length; i++) {
            th.append('<th data-hide="phone,tablet" style="background-color :#4DB6AC;text-align:center">' + json[i].excel_column + '</th>');
            json_headers = json[i].model_column.replace(/ /g, '_');
            array_header.push({ "data": json_headers });
            json_excel_uploaded = json_excel_uploaded.replace(new RegExp("\"" + json[i].excel_column + "\":", "g"), "\"" + json_headers + "\":");

            // json_ex_uploaded = json_excel_uploaded.replace(/[\[\]']+/g,'');
        }

        var jsonobj = JSON.parse(json_excel_uploaded);
        $('#' + table_name + ' thead').append(th);

        $('#' + table_name).dataTable({
            "data": jsonobj,
            "columns": array_header,
            "scrollY": "350px",
            "scrollX": "auto",
            fixedColumns: {
                leftColumns: 2
            },
            dom: '<"html5buttons"B>lTfgitp',
            buttons: [
                {
                    text: 'Upload',
                    action: function (e, dt, node, config) {
                        window[upload_function]();
                    }
                },
                { extend: 'copy' },
                { extend: 'csv' },
                { extend: 'excel', title: 'uploaded Data' },
            ]
        });

        // json_uploaded='';
    });

    return false;
}

function ajax_master_data_upload() {



    var uploadForm = document.getElementById('upload_form_name');
    var selected_format = uploadForm.options[uploadForm.selectedIndex].value;
    var urlPath = "/master/excelUpload/" + selected_format;

    $.ajax({
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: json_excel_uploaded,
        url: urlPath,
        success: function (json) {

            alert("File uploaded successfully");
            console.log('/sayHello POST was successful.');
            console.log(json);

        },
        error: function () {
            alert("There was an error. Try again please!");
        }
    });

}


function ajaxCallForAssignedIntCREManager() {


    // $.blockUI();
    var tables5 = $('#dataTables-example6').DataTable();
    tables5.clear().draw();



    // oTable = $('#dataTables-example6').dataTable({
    // "bStateSave": true
    // }).yadcf([{
    // column_number: 0,
    // filter_type: "multi_select",
    // select_type: 'chosen'
    // }]);
    //    

    var selectUser = document.getElementById('dataList');
    var selectAgent = "";

    for (var i = 0; i < selectUser.length; i++) {

        if (i == (selectUser.length - 1)) {

            selectAgent = selectAgent + selectUser.options[i].value;
        } else {

            selectAgent = selectAgent + selectUser.options[i].value + ",";
        }
    }

    // var selectAgent = document.getElementById('user').value;

    // alert("selectAgent for DroppedCalls:"+selectAgent);
    var urlDisposition = "/CREManager/getAssignedCallsOfCREManager/" + selectAgent + "";
    $.ajax({
        url: urlDisposition

    }).done(function (data) {

        if (data != null) {
            // $.unblockUI();
            // alert("success");

            var tableHeaderRowCount = 1;
            var table = document.getElementById('dataTables-example6');
            var rowCount = table.rows.length;
            for (var i = tableHeaderRowCount; i < rowCount; i++) {
                table.deleteRow(tableHeaderRowCount);
            }

            var customer_list = data.customerLoadList;
            var vehical_list = data.vehicleLoadList;
            var wyzUser_list = data.wyzUserLoadList;
            var assign_list = data.assignedInteractionLoadList;
            var service_list = data.serviceLoadList;

            $('#dataList').on('change', function () {
                var selectedValue = $(this).val();
                $('#dataTables-example6').dataTable().fnFilter("^" + selectedValue + "$", 0, true); // Exact
                // value,
                // column,
                // reg
            });


            // $('#data').on( 'keyup', function () {
            // tables5
            // .columns( 0 )
            // .search( this.value )
            // .draw();
            // } );



            for (i = 0; i < assign_list.length; i++) {


                $('#dataTables-example6').dataTable().fnAddData([
                    wyzUser_list[i].username,
                    customer_list[i].customerName,
                    vehical_list[i].vehicleRegNo,
                    vehical_list[i].model,
                    customer_list[i].customerCategory,
                    customer_list[i].customerPhone,
                    vehical_list[i].nextServicedate,
                ]);

            }
        } else {

            alert("Data is null");
        }
    });


}


function ajaxCallForFollowUpRequiredCREManager() {

    $.blockUI();
    var tables0 = $('#dataTables-example7').DataTable();
    tables0.clear().draw();

    var selectUser = document.getElementById('dataList');
    var selectAgent = "";

    for (var i = 0; i < selectUser.length; i++) {

        if (i == (selectUser.length - 1)) {

            selectAgent = selectAgent + selectUser.options[i].value;
        } else {

            selectAgent = selectAgent + selectUser.options[i].value + ",";
        }
    }

    var urlDisposition = "/CREManager/getFollowUpRequiredDataCREMan/" + selectAgent + "";
    $.ajax({
        url: urlDisposition

    }).done(function (data) {

        if (data != null) {

            $.unblockUI();
            var tableHeaderRowCount = 1;
            var table = document.getElementById('dataTables-example7');
            var rowCount = table.rows.length;
            for (var i = tableHeaderRowCount; i < rowCount; i++) {
                table.deleteRow(tableHeaderRowCount);


            }
            var wyzUser_list = data.wyzUserLoadList;
            var customer_list = data.customerLoadList;
            var vehical_list = data.vehicleLoadList;
            var srdisposition_list = data.srdispositionLoadList;
            var interaction_list = data.callInteractionLoadList;
            var service_list = data.serviceLoadList;

            $('#dataList').on('change', function () {
                var selectedValue = $(this).val();
                $('#dataTables-example7').dataTable().fnFilter("^" + selectedValue + "$", 0, true); // Exact
                // value,
                // column,
                // reg
            });


            for (i = 0; i < interaction_list.length; i++) {
                var span1 = "<span class=\"label label-primary\">" + srdisposition_list[i].callDisposition + "</span>";
                $('#dataTables-example7').dataTable().fnAddData([
                    wyzUser_list[i].username,
                    customer_list[i].customerName,
                    customer_list[i].customerPhone,
                    vehical_list[i].vehicleRegNo,
                    vehical_list[i].nextServicedate,
                    srdisposition_list[i].followUpDate,
                    srdisposition_list[i].followUpTime,
                    span1]);
            }
        }
    });




}

function ajaxCallForServiceBookedCREManager() {


    $.blockUI();
    var tables3 = $('#dataTables-example8').DataTable();
    tables3.clear().draw();

    var selectUser = document.getElementById('dataList');
    var selectAgent = "";

    for (var i = 0; i < selectUser.length; i++) {

        if (i == (selectUser.length - 1)) {

            selectAgent = selectAgent + selectUser.options[i].value;
        } else {

            selectAgent = selectAgent + selectUser.options[i].value + ",";
        }
    }



    // alert("selectAgent for servicebooked:"+selectAgent);
    var urlDisposition = "/CREManager/getServiceBookedDataCREMan/" + selectAgent + "";
    $.ajax({
        url: urlDisposition

    }).done(function (data) {

        if (data != null) {
            $.unblockUI();

            var tableHeaderRowCount = 1;
            var table = document.getElementById('dataTables-example8');
            var rowCount = table.rows.length;
            for (var i = tableHeaderRowCount; i < rowCount; i++) {
                table.deleteRow(tableHeaderRowCount);
            }

            var wyzUser_list = data.wyzUserLoadList;
            var customer_list = data.customerLoadList;
            var vehical_list = data.vehicleLoadList;
            var srdisposition_list = data.srdispositionLoadList;
            var interaction_list = data.callInteractionLoadList;
            var servicebooked_List = data.servicebookedLoadList;
            var service_list = data.serviceLoadList;
            $('#dataList').on('change', function () {
                var selectedValue = $(this).val();
                $('#dataTables-example8').dataTable().fnFilter("^" + selectedValue + "$", 0, true); // Exact
                // value,
                // column,
                // reg
            });

            for (i = 0; i < interaction_list.length; i++) {




                var span3 = "<span class=\"label label-primary\">" + srdisposition_list[i].callDisposition + "</span>";



                $('#dataTables-example8').dataTable().fnAddData([
                    wyzUser_list[i].username,
                    customer_list[i].customerName,
                    customer_list[i].customerPhone,
                    vehical_list[i].vehicleRegNo,
                    vehical_list[i].nextServicedate,
                    servicebooked_List[i].serviceScheduledDate,
                    servicebooked_List[i].serviceScheduledTime,
                    span3
                ]);

            }
        } else {

            alert("Data is null");
        }
    });



}

function ajaxCallForServiceNotRequiredCREManager() {

    $.blockUI();
    var tables2 = $('#dataTables-example9').DataTable();
    tables2.clear().draw();
    var selectUser = document.getElementById('dataList');
    var selectAgent = "";

    for (var i = 0; i < selectUser.length; i++) {

        if (i == (selectUser.length - 1)) {

            selectAgent = selectAgent + selectUser.options[i].value;
        } else {

            selectAgent = selectAgent + selectUser.options[i].value + ",";
        }
    }

    // alert("selectAgent for ServiceNotRequired:"+selectAgent);
    var urlDisposition = "/CREManager/getServiceNotRequiredDataCREMan/" + selectAgent + "";
    $.ajax({
        url: urlDisposition

    }).done(function (data) {

        if (data != null) {
            $.unblockUI();
            // alert("success");

            var tableHeaderRowCount = 1;
            var table = document.getElementById('dataTables-example9');
            var rowCount = table.rows.length;
            for (var i = tableHeaderRowCount; i < rowCount; i++) {
                table.deleteRow(tableHeaderRowCount);
            }
            var wyzUser_list = data.wyzUserLoadList;
            var customer_list = data.customerLoadList;
            var vehical_list = data.vehicleLoadList;
            var srdisposition_list = data.srdispositionLoadList;
            var interaction_list = data.callInteractionLoadList;
            var service_list = data.serviceLoadList;



            $('#dataList').on('change', function () {
                var selectedValue = $(this).val();
                $('#dataTables-example9').dataTable().fnFilter("^" + selectedValue + "$", 0, true); // Exact
                // value,
                // column,
                // reg
            });


            for (i = 0; i < interaction_list.length; i++) {




                var span1 = "<span class=\"label label-primary\">" + srdisposition_list[i].callDisposition + "</span>";



                $('#dataTables-example9').dataTable().fnAddData([
                    wyzUser_list[i].username,
                    customer_list[i].customerName,
                    customer_list[i].customerPhone,
                    vehical_list[i].vehicleRegNo,
                    vehical_list[i].nextServicedate,
                    span1,
                    srdisposition_list[i].noServiceReason

                ]);

            }

        } else {

            alert("Data is null");
        }
    });



}

function ajaxCallForNonContactsCREManager() {

    $.blockUI();
    var tables1 = $('#dataTables-example10').DataTable();
    tables1.clear().draw();

    var selectUser = document.getElementById('dataList');
    var selectAgent = "";

    for (var i = 0; i < selectUser.length; i++) {

        if (i == (selectUser.length - 1)) {

            selectAgent = selectAgent + selectUser.options[i].value;
        } else {

            selectAgent = selectAgent + selectUser.options[i].value + ",";
        }
    }

    // alert("selectAgent for NonContacts:"+selectAgent);
    var urlDisposition = "/CREManager/getNonContactsDataCREMan/" + selectAgent + "";
    $.ajax({
        url: urlDisposition

    }).done(function (data) {

        if (data != null) {

            // alert("success");
            $.unblockUI();
            var tableHeaderRowCount = 1;
            var table = document.getElementById('dataTables-example10');
            var rowCount = table.rows.length;
            for (var i = tableHeaderRowCount; i < rowCount; i++) {
                table.deleteRow(tableHeaderRowCount);
            }


            var customer_list = data.customerLoadList;
            var vehical_list = data.vehicleLoadList;
            var wyzUser_list = data.wyzUserLoadList;
            var srdisposition_list = data.srdispositionLoadList;
            var interaction_list = data.callInteractionLoadList;
            var service_list = data.serviceLoadList;

            $('#dataList').on('change', function () {
                var selectedValue = $(this).val();
                $('#dataTables-example10').dataTable().fnFilter("^" + selectedValue + "$", 0, true); // Exact
                // value,
                // column,
                // reg
            });

            for (i = 0; i < interaction_list.length; i++) {


                var span = "<span class=\"label label-primary\">" + srdisposition_list[i].callDisposition + "</span>";



                $('#dataTables-example10').dataTable().fnAddData([
                    wyzUser_list[i].username,
                    customer_list[i].customerName,
                    customer_list[i].customerPhone,
                    vehical_list[i].vehicleRegNo,
                    vehical_list[i].nextServicedate,
                    span
                ]);
            }
        } else {

            alert("Data is null");
        }
    });





}

function ajaxCallForDroppedCallsCREManager() {

    $.blockUI();

    var tables4 = $('#dataTables-example11').DataTable();
    tables4.clear().draw();
    var selectUser = document.getElementById('dataList');
    var selectAgent = "";

    for (var i = 0; i < selectUser.length; i++) {

        if (i == (selectUser.length - 1)) {

            selectAgent = selectAgent + selectUser.options[i].value;
        } else {

            selectAgent = selectAgent + selectUser.options[i].value + ",";
        }
    }


    // alert("selectAgent for DroppedCalls:"+selectAgent);
    var urlDisposition = "/CREManager/getDroppedCallsDataCREMan/" + selectAgent + "";
    $.ajax({
        url: urlDisposition

    }).done(function (data) {

        if (data != null) {


            var tableHeaderRowCount = 1;
            var table = document.getElementById('dataTables-example11');
            var rowCount = table.rows.length;
            for (var i = tableHeaderRowCount; i < rowCount; i++) {
                table.deleteRow(tableHeaderRowCount);
            }
            var wyzUser_list = data.wyzUserLoadList;
            var customer_list = data.customerLoadList;
            var vehical_list = data.vehicleLoadList;
            var srdisposition_list = data.srdispositionLoadList;
            var interaction_list = data.callInteractionLoadList;
            var service_list = data.serviceLoadList;

            $('#dataList').on('change', function () {
                var selectedValue = $(this).val();
                $('#dataTables-example11').dataTable().fnFilter("^" + selectedValue + "$", 0, true); // Exact
                // value,
                // column,
                // reg
            });

            for (i = 0; i < interaction_list.length; i++) {

                var span4 = "<span class=\"label label-primary\">" + srdisposition_list[i].callDisposition + "</span>";


                $('#dataTables-example11').dataTable().fnAddData([
                    wyzUser_list[i].username,
                    customer_list[i].customerName,
                    customer_list[i].customerPhone,
                    vehical_list[i].vehicleRegNo,
                    vehical_list[i].nextServicedate,
                    span4
                ]);


            }
        } else {

            alert("Data is null");
        }
    });
    $.unblockUI();




}
// ROSTER
function loadRosterTable() {
    var selectAgent = document.getElementById('selectedCREis').value;
    var urlRosterData = "/CREManager/loadRosterData/" + selectAgent + "";

    $.ajax({

        url: urlRosterData

    }).done(function (rosterData) {
        $("#loadRoaster").find("tr").remove();
        for (i = 0; i < rosterData.length; i++) {
            console.log(rosterData[i].id);
            var x = rosterData[i].fromDate;
            var y = rosterData[i].toDate;
            var markup1 = "<tr><td>" + x + "</td><td>" + y + "</td><td><button class='btn btn-sm btn-danger' onclick='deleteTheRoster(" + rosterData[i].id + ");'>Delete</button></td></tr>";
            $("#loadRoaster").append(markup1);

        }

    });


}
function deleteTheRoster(id) {


    // alert("delete id is : "+id);

    var deleteRosterById = "/CREManager/deleteUnavialbility/" + id + "";

    Lobibox.confirm({
        msg: "Are u sure want to delete it?",
        callback: function ($this, type) {
            if (type === 'yes') {


                console.log(deleteRosterById);
                $.ajax({

                    url: deleteRosterById


                }).done(function (data) {

                    // $("#loadRoaster").find("tr").remove();
                    // $("#loadRoaster1").find("tr").remove();
                    // $("#loadRoaster2").find("tr").remove();
                    // loadRosterTable();
                    // loadRosterTables();
                    // loadRosterforDriver();
                    Lobibox.alert('info', {
                        msg: "Deleted Successfuly"
                    });

                });

            } else if (type === 'no') {


            }

        }
    });


}
function rosterUnavialbiltyByUser() {

    loadRosterTable();
    $("#creTableRosters").find("tr").remove();
    var d = new Date();
    var year = d.getFullYear();
    var day = d.getDate();
    var month = d.getMonth() + 1;

    if (month < 10) month = "0" + month;
    if (day < 10) day = "0" + day;

    var picker = $("<input/>", {
        type: 'text',
        id: 'numFrom',
        name: 'numFrom',
        style: 'margin-right: 2.5cm; ',
        required: 'required',
        readonly: 'true'


    }).datepicker({

        autoclose: true,
        dateFormat: 'yy-mm-dd'
    });

    var picker1 = $("<input/>", {
        type: 'text',
        id: 'numTo',
        name: 'numTo',
        required: 'required',
        readonly: 'true'

    }).datepicker({

        autoclose: true,
        dateFormat: 'yy-mm-dd'
    });



    var selectAgent = document.getElementById('selectedCREis').value;

    $("#creTableRoster").find("tr").remove();


    th = $('<tr>');
    th.append('<th>User Name</th>');
    th.append('<th>UnAvailable From and To Date</th>');
    th.append('<th>Add</th>');
    $('#creTableRoster').append(th);

    tr = $('<tr/>');
    tr.append('<td><input type="text" style="border:none" value="' + selectAgent + '" readOnly></td>');
    tr.append(picker);
    tr.append(picker1);
    tr.append('<td><button class="btn btn-sm btn-danger" onclick="\addTheRosterForUser();\">ADD</button></td>');

    $('#creTableRoster').append(tr);







}

function addTheRosterForUser() {
    $.blockUI();
    var selectAgent = document.getElementById('selectedCREis').value;
    var fromDate = document.getElementById('numFrom').value;
    var toDate = document.getElementById('numTo').value;

    if (fromDate != "" && toDate != "") {


        var urlDisposition = "/CREManager/addRosterOfUserByAjax/" + selectAgent + "/" + fromDate + "/" + toDate + "";
        $.ajax({

            url: urlDisposition

        }).done(function (data) {

            if (data == "success") {

                var markup = "<tr><td>" + fromDate + "</td><td>" + toDate + "</td><td><button class='btn btn-sm btn-danger' >Delete</button></td></tr>";
                $("#creTableRosters").append(markup);
                $.unblockUI();
            } else if (data == "failure") {

                Lobibox.alert('error', {
                    msg: "Error in adding Unavialabilty"
                });
            } else {
                $.unblockUI();

                for (i = 0; i < data.length; i++) {
                    var From_Date = data[i].fromDate;
                    var To_Date = data[i].toDate;
                    var Id = data[i].id;
                }


                Lobibox.confirm({
                    msg: " From <b>" + fromDate + '</b> to <b>' + toDate + "</b> already Exists !</br> <i>Do You Want To Update?</i>",
                    callback: function ($this, type) {
                        if (type === 'yes') {
                            $.blockUI();
                            var urlDisposition = "/CREManager/updateRosterOfUserByAjaxData/" + selectAgent + "/" + fromDate + "/" + toDate + "";
                            console.log(urlDisposition);
                            $.ajax({

                                url: urlDisposition
                            }).done(function (data1) {

                                $.unblockUI();
                                // $("#creTableRoster").find("tr").remove();
                                $("#loadRoaster").find("tr").remove();
                                loadRosterTable();

                                // $("#creTableRosters").append(markup);

                            });

                        } else if (type === 'no') {


                        }

                    }
                });

            }


        });


    } else {

        Lobibox.alert('error', {
            msg: "Unavialabilty Date's Are Missing"
        });


    }
}
//Disposition page data load
//function ajaxCallServiceLoadOfCustomer() {

//    var customerId = document.getElementById('customer_Id').value;
//    var dealerCode = $('#PkDealercode').val();
//    // alert("customerId : "+customerId);
//    var urlDisposition = "/CRE/serviceDataOfCustomer/" + customerId + "";
//    $.ajax({

//        url: urlDisposition

//    }).done(function (serviceLoadData) {

//        if (serviceLoadData != null) {


//            var tableHeaderRowCount = 1;
//            var table = document.getElementById('example800');
//            var rowCount = table.rows.length;
//            for (var i = tableHeaderRowCount; i < rowCount; i++) {
//                table.deleteRow(tableHeaderRowCount);
//            }
//            var serviceData = serviceLoadData.serviceLoadList;
//            var vehicleData = serviceLoadData.vehicleLoadList;

//            for (var j = 0; j < serviceData.length; j++) {
//                console.log("menu code description:" + serviceData[j]);

//            }
//            for (i = 0; i < vehicleData.length; i++) {
//                if (vehicleData[i].variant != null) {

//                    var variant = vehicleData[i].variant;
//                } else {
//                    variant = '-';
//                }
//                if (vehicleData[i].color != null) {

//                    var vehcolor = vehicleData[i].color;
//                } else {
//                    vehcolor = '-';
//                }
//                if (serviceData[i].serviceDate != null) {

//                    var serviceDate = serviceData[i].serviceDate;
//                } else {
//                    serviceDate = '-';
//                }

//                if (serviceData[i].uploadDate != null) {

//                    var uploadDate = serviceData[i].uploadDate;
//                } else {
//                    uploadDate = '-';
//                }

//                if (serviceData[i].serviceType != null) {

//                    var serviceType = serviceData[i].serviceType;
//                } else {
//                    serviceType = '-';
//                }
//                if (serviceData[i].lastServiceMeterReading != null) {

//                    var lastServiceMeterReading = serviceData[i].lastServiceMeterReading;
//                } else {
//                    lastServiceMeterReading = '-';
//                }




//                if (serviceData[i].serviceAdvisor != null) {
//                    var serviceAdvisor = serviceData[i].serviceAdvisor;

//                } else {
//                    serviceAdvisor = '-';

//                }

//                if (serviceData[i].billAmt != null) {
//                    var billAmt = serviceData[i].billAmt;

//                } else {
//                    billAmt = '-';

//                }


//                if (serviceData[i].serviceLocaton != null) {
//                    var serviceLocations = serviceData[i].serviceLocaton;
//                } else {
//                    serviceLocations = '-';
//                }

//                if (serviceData[i].jobCardDate != null) {
//                    var jobCardDate = serviceData[i].jobCardDate;
//                } else {
//                    jobCardDate = '-';
//                }

//                if (serviceData[i].jobCardDate != null) {
//                    var jobCardDate = serviceData[i].jobCardDate;
//                } else {
//                    jobCardDate = '-';
//                }

//                if (serviceData[i].billDate != null) {
//                    var billDate = serviceData[i].billDate;
//                } else {
//                    billDate = '-';
//                }


//                if (serviceData[i].kilometer != null) {
//                    var kilometer = serviceData[i].kilometer;
//                } else {
//                    kilometer = '-';
//                }

//                if (serviceData[i].custName != null) {
//                    var custName = serviceData[i].custName;
//                } else {
//                    custName = '-';
//                }
//                if (serviceData[i].phonenumber != null) {
//                    var phonenumber = serviceData[i].phonenumber;
//                } else {
//                    phonenumber = '-';
//                }
//                if (vehicleData[i].category != null) {
//                    var category = vehicleData[i].category;
//                } else {
//                    category = '-';
//                }
//                Number.prototype.round = function (decimals) {
//                    return Number((Math.round(this + "e" + decimals) + "e-" + decimals));
//                }

//                var partAmt;
//                var labourAmt;
//                var totalAmt;
//                if (serviceData[i].mfgPartsTotal != null || serviceData[i].localPartsTotal != null || serviceData[i].maximileTotal != null || serviceData[i].oilConsumablesTotal != null || serviceData[i].maxiCareTotal != null || serviceData[i].mfgAccessoriesTotal != null || serviceData[i].localAccessoriesTotal != null) {
//                    var mfgPartsTotal = serviceData[i].mfgPartsTotal;
//                    var localPartsTotal = serviceData[i].localPartsTotal;
//                    var maximileTotal = serviceData[i].maximileTotal;
//                    var oilConsumablesTotal = serviceData[i].oilConsumablesTotal;
//                    var maxiCareTotal = serviceData[i].maxiCareTotal;
//                    var mfgAccessoriesTotal = serviceData[i].mfgAccessoriesTotal;
//                    var localAccessoriesTotal = serviceData[i].localAccessoriesTotal;
//                    partAmt = mfgPartsTotal + localPartsTotal + maximileTotal + oilConsumablesTotal + maxiCareTotal + mfgAccessoriesTotal + localAccessoriesTotal;
//                    partAmt = partAmt.round(2);
//                } else {
//                    partAmt = '-';
//                }

//                if (serviceData[i].inhouseLabourTotal != null || serviceData[i].outLabourTotal != null) {
//                    var inhouseLabourTotal = serviceData[i].inhouseLabourTotal;
//                    var outLabourTotal = serviceData[i].outLabourTotal;
//                    labourAmt = inhouseLabourTotal + outLabourTotal;
//                    abourAmt = labourAmt.round(2);
//                } else {
//                    labourAmt = '-';
//                }

//                if (partAmt != null || labourAmt != null) {
//                    var totalAmt = partAmt + labourAmt;
//                    totalAmt = totalAmt.round(2);
//                } else {
//                    totalAmt = '-';
//                }

//                var workshop = serviceData[i].workshop;
//                var laborDetail = '';
//                var defectDetail = '';
//                var jobcardDetail = '';
//                if (serviceData[i].laborDetails != null) {
//                    laborDetail = serviceData[i].laborDetails;
//                }
//                if (serviceData[i].defectDetails != null) {
//                    defectDetail = serviceData[i].defectDetails;
//                }
//                if (serviceData[i].jobCardRemarks != null) {
//                    jobcardDetail = serviceData[i].jobCardRemarks;
//                }

//                var jobcardloc = serviceData[i].jobcardlocation;
//                var menudesc = '';
//                var menudescrip = '';
//                if (serviceData[i].menuCodeDesc != null) {
//                    menudesc = serviceData[i].menuCodeDesc;
//                    menudescrip = menudesc.substring(0, 30) + "..";
//                }



//                tr = $('<tr/>');
//                if (dealerCode == "SUKHMANI") {
//                    tr.append("<td><a data-toggle='modal' data-target='#marutiServiceModal' onclick=servicehistory('" + jobcardloc + "');><i class='fa fa-info-circle' data-toggel='tooltip' title='Other Details' style='font-size:30px;color:red;' id='modaltest'></i></a></td>");
//                }
//                else {
//                    tr.append("<td><a data-toggle='modal' data-target='#marutiServiceModal' onclick=marutiServiceModal('" + jobcardloc + "');><i class='fa fa-info-circle' data-toggel='tooltip' title='Other Details' style='font-size:30px;color:red;' id='modaltest'></i></a></td>");
//                }
//                tr.append("<td>" + uploadDate + "</td>");
//                //tr.append("<td>" + serviceLocations + "</td>");
//                tr.append("<td>" + jobCardDate + "</td>");
//                tr.append("<td>" + serviceData[i].jobCardNumber + "</td>");
//                tr.append("<td>" + workshop + "</td>");
//                tr.append("<td>" + billDate + "</td>");
//                tr.append("<td>" + phonenumber + "</td>");
//                tr.append("<td>" + kilometer + "</td>");
//                //tr.append("<td>" + vehicleData[i].model + "</td>");
//                //tr.append("<td>" + vehicleData[i].vehicleRegNo + "</td>");
//                tr.append("<td>" + serviceType + "</td>");
//                tr.append("<td title='" + menudesc + "'>" + menudescrip + "</td>");
//                tr.append("<td>" + serviceAdvisor + "</td>");
//                //tr.append("<td>" + custName + "</td>");
//                //tr.append("<td>" + category + "</td>");
//                tr.append("<td>" + partAmt + "</td>");
//                tr.append("<td>" + labourAmt + "</td>");
//                tr.append("<td>" + billAmt + "</td>");

//                $('#example800').append(tr);
//                $($.fn.dataTable.tables(true)).DataTable()
//                    .columns.adjust();
//            }




//        }

//    });
//}

//function callHistoryByVehicle(typeIs){


//    // alert("Call history loading");
//    var vehicalId = document.getElementById('vehical_Id').value;

//    var urlDisposition = "/CRE/getCallHistoryOfvehicalId/" + vehicalId + "/"+typeIs;
//    $.ajax({
//        url: urlDisposition

//    }).done(function (interaction_list) {    	
//        if (interaction_list != null) {


//            //var tableHeaderRowCount = 1;
//			var tableHeaderRowCount1 = 1;
//           // var table = document.getElementById('previousCommentTable');
//			var table1 = document.getElementById('dispositionHistory');
//          //  var rowCount = table.rows.length;
//			var rowCount1 = table1.rows.length;
//            /*for (var i = tableHeaderRowCount; i < rowCount; i++) {
//                table.deleteRow(tableHeaderRowCount);

//            }*/

//			 for (var i = tableHeaderRowCount1; i < rowCount1; i++) {
//                table1.deleteRow(tableHeaderRowCount1);

//            }


//        var wyzUser_list = interaction_list.wyzUserLoadList;
//           var srdisposition_list = interaction_list.srdispositionLoadList;
//            var interac_list = interaction_list.callInteractionLoadList;
//           // console.log(srdisposition_list);
//            var lengthIs=wyzUser_list.length;
//            console.log("wyzUser_list.length : "+wyzUser_list.length);
//          /*  for (i = lengthIs-1; i >= 0; i--) {

//				tr = $('<tr/>');
//                tr.append("<td>" + interac_list[i].callDate + '/' + interac_list[i].callTime +"</td>");

//                if(srdisposition_list[i] !=null){
//				tr.append("<td>" + srdisposition_list[i].comments + "</td>");
//                }else{
//                	tr.append("<td></td>");               	
//                }

//                $('#previousCommentTable').append(tr);


//            }*/

//            for (i = lengthIs-1; i >= 0; i--) {

//				tr = $('<tr/>');

//				if(srdisposition_list[i] !=null){
//					tr.append("<td>" + srdisposition_list[i].assignId + "</td>");
//					tr.append("<td>" + interac_list[i].callDate + "</td>");
//					tr.append("<td>" + interac_list[i].callTime + "</td>");
//					tr.append("<td>" + wyzUser_list[i].username + "</td>");
//					tr.append("<td>" + interac_list[i].callType + "</td>");
//					tr.append("<td>" + interac_list[i].callisInitiated + "</td>");
//					tr.append("<td>" + interac_list[i].makeCallFrom + "</td>");
//					tr.append("<td>" + interac_list[i].campName + "</td>");
//					tr.append("<td>" + interac_list[i].dailedNumber + "</td>");
//					tr.append("<td>" + srdisposition_list[i].callDisposition + "</td>");
//					if(srdisposition_list[i].noServiceReason == "Kms Not Covered") {
//						tr.append("<td>" + srdisposition_list[i].noServiceReason + " / " + srdisposition_list[i].kms + "KM" + "</td>");

//					} else {
//						tr.append("<td>" + srdisposition_list[i].noServiceReason + "</td>");
//					}
//	    			tr.append("<td>" + srdisposition_list[i].comments + "</td>");

//	    			if(srdisposition_list[i].typeOfCall == "SMR"){

//	    				tr.append("<td><audio controls><source src='http://connect.autosherpas.com:9014/CRE/audiostreamFile/"
//	    						+ interac_list[i].id + "/SMR' type='audio/mp3'></audio><td>");

//	    				tr.append("<td><a href='/CRE/audiostreamFile/"+ interac_list[i].id+ "/SMR'><i class=\"fa fa-download\" data-toggle=\"tooltip\"></i></a><td>");	    				

//	    			}else if(srdisposition_list[i].typeOfCall == "PSF"){

//	    				tr.append("<td><audio controls><source src='http://connect.autosherpas.com:9014/CRE/audiostreamFile/"
//	    						+ interac_list[i].id + "/PSF' type='audio/mp3'></audio><td>");

//	    				tr.append("<td><a href='/CRE/audiostreamFile/"+ interac_list[i].id+ "/PSF'><i class=\"fa fa-download\" data-toggle=\"tooltip\"></i></a><td>");

//	    			}else if(srdisposition_list[i].typeOfCall == "INS"){

//	    				tr.append("<td><audio controls><source src='http://connect.autosherpas.com:9014/CRE/audiostreamFile/"
//	    						+ interac_list[i].id + "/INS' type='audio/mp3'></audio><td>");

//	    				tr.append("<td><a href='/CRE/audiostreamFile/"+ interac_list[i].id+ "/INS'><i class=\"fa fa-download\" data-toggle=\"tooltip\"></i></a><td>");

//	    			}else{

//					tr.append("<td><audio controls><source src='http://connect.autosherpas.com:9014/CRE/audiostreamFile/"
//						+ interac_list[i].id + "/others' type='audio/mp3'></audio><td>");

//					tr.append("<td><a href='/CRE/audiostreamFile/"+ interac_list[i].id+ "/others'><i class=\"fa fa-download\" data-toggle=\"tooltip\"></i></a><td>");

//	    			}
//				}else{
//					tr.append("<td></td>");
//					tr.append("<td></td>");
//	    			tr.append("<td></td>");
//					tr.append("<td></td>");
//					tr.append("<td></td>");
//					tr.append("<td></td>");
//					tr.append("<td></td>");	
//					tr.append("<td></td>");
//					tr.append("<td></td>");
//	    			tr.append("<td></td>");
//					tr.append("<td></td>");	
//					tr.append("<td></td>");
//				}



//                $('#dispositionHistory').append(tr);


//            }



//        } else {

//            alert("Data is null");
//        }
//    });

//}
//function complaintsOFVehicle(){

//	// alert("Call history loading");
//    var vehicalId = document.getElementById('vehical_Id').value;


//    var urlDisposition = "/CRE/getComplaintHistoryOfvehicalId/" + vehicalId + "";
//    $.ajax({
//        url: urlDisposition

//    }).done(function (complaint_list) {


//    	var tableHeaderRowCount = 1;
//		var table = document.getElementById('complaint_history');
//		var rowCount = table.rows.length;
//		for (var i = tableHeaderRowCount; i < rowCount; i++) {
//		table.deleteRow(tableHeaderRowCount);
//		}


//    	 for (i = 0; i < complaint_list.length; i++) {

//				tr = $('<tr/>');
//				tr.append("<td>" + complaint_list[i].complaintNumber + "</td>");
//				tr.append("<td>" + complaint_list[i].saleDate + "</td>");
//				tr.append("<td>" + complaint_list[i].sourceName + "</td>");
//				tr.append("<td>" + complaint_list[i].functionName + "</td>");
//				tr.append("<td>" + complaint_list[i].subcomplaintType + "</td>");
//				tr.append("<td>" + complaint_list[i].description + "</td>");


//             $('#complaint_history').append(tr);


//         }

//    });


//}
//function AssignmentHistoryOfCustomer() {
//	// alert("insuranceHistoryOfCustomer");
//	var vehicleId = document.getElementById('vehical_Id').value;


//	    var urlDisposition = "/CRE/getAssignmentHistoryOfVehicleId/" + vehicleId + "";
//	    $.ajax({
//	        url: urlDisposition

//	    }).done(function (assign_list) {


//	    	var tableHeaderRowCount = 1;
//			var table = document.getElementById('assignment_smr_history');
//			var rowCount = table.rows.length;
//			for (var i = tableHeaderRowCount; i < rowCount; i++) {
//			table.deleteRow(tableHeaderRowCount);
//			} 

//			 for (i = 0; i < assign_list.length; i++) {
//				 var d="";
//				 var date = "";
//					if(assign_list[i].assignDate!=null){ 
//					  d = new Date(assign_list[i].assignDate);
//					  var month = (d.getMonth() + 1).toString();
//			           month = month.length > 1 ? month : '0' + month;
//			           var day = d.getDate().toString();
//			           day = day.length > 1 ? day : '0' + day;
//			           date= day + '/' + month + '/' + d.getFullYear();
//					}

//					tr = $('<tr/>');
//					tr.append("<td>" + assign_list[i].assignedId + "</td>");	 
//					tr.append("<td>" + date + "</td>");
//					tr.append("<td>" + assign_list[i].reason + "</td>");
//					tr.append("<td>" + assign_list[i].WyzuserName + "</td>");
//					tr.append("<td>" + assign_list[i].smsType + "</td>");
//					tr.append("<td>" + assign_list[i].smsMessage + "</td>");

//	          $('#assignment_smr_history').append(tr);


//	      }


//	    });

//}

//function insuranceHistoryOfCustomer(){

//	// alert("insuranceHistoryOfCustomer");
//	var customerId = document.getElementById('customer_Id').value;


//	    var urlDisposition = "/CRE/getInsuranceHistoryOfCustomerId/" + customerId + "";
//	    $.ajax({
//	        url: urlDisposition

//	    }).done(function (insurance_list) {


//	    	var tableHeaderRowCount = 1;
//			var table = document.getElementById('insurance_history');
//			var rowCount = table.rows.length;
//			for (var i = tableHeaderRowCount; i < rowCount; i++) {
//			table.deleteRow(tableHeaderRowCount);
//			} 

//			 for (i = 0; i < insurance_list.length; i++) {
//				 var d="";
//				 var date = "";
//					if(insurance_list[i].createdDate!=null){ 
//					  d = new Date(insurance_list[i].createdDate);
//					  var month = (d.getMonth() + 1).toString();
//			           month = month.length > 1 ? month : '0' + month;
//			           var day = d.getDate().toString();
//			           day = day.length > 1 ? day : '0' + day;
//			           date= day + '/' + month + '/' + d.getFullYear();
//					}

//					tr = $('<tr/>');
//					tr.append("<td>" + insurance_list[i].msgURL + "</td>");
//					tr.append("<td>" + insurance_list[i].emailURL + "</td>");	 
//					tr.append("<td>" + date + "</td>");
//					tr.append("<td>" + insurance_list[i].createdCRE + "</td>");
//					tr.append("<td>" + insurance_list[i].insuranceCompanyQuotated + "</td>");
//					tr.append("<td>" + insurance_list[i].idv + "</td>");
//					tr.append("<td>" + insurance_list[i].odPercentage + "</td>");
//					tr.append("<td>" + insurance_list[i].ncbPercentage + "</td>");
//					tr.append("<td>" + insurance_list[i].ncbRupees + "</td>");
//					tr.append("<td>" + insurance_list[i].discountPercentage + "</td>");
//					tr.append("<td>" + insurance_list[i].discountValue + "</td>");
//					tr.append("<td>" + insurance_list[i].oDPremium + "</td>");
//					tr.append("<td>" + insurance_list[i].addOn + "</td>");
//					tr.append("<td>" + insurance_list[i].liabilityPremium + "</td>");
//					tr.append("<td>" + insurance_list[i].otherAmt + "</td>");					
//					tr.append("<td>" + insurance_list[i].totalPremiumWithOutTax + "</td>");
//					tr.append("<td>" + insurance_list[i].totalPremiumWithTax + "</td>");
//					tr.append("<td>" + insurance_list[i].commentIQ + "</td>");



//	          $('#insurance_history').append(tr);


//	      }


//	    });

//	}

//function smsHistoryOfUser(){
//	// alert("sms history loading");
//    var customerId = document.getElementById('customer_Id').value;

//    //alert(customerId);
//    var urlDisposition = "/CRE/getSMSHistoryOfCustomerId/" + customerId + "";
//    $.ajax({
//        url: urlDisposition

//    }).done(function (sms_list) {


//    	var tableHeaderRowCount = 1;
//		var table = document.getElementById('sms_history');
//		var rowCount = table.rows.length;
//		for (var i = tableHeaderRowCount; i < rowCount; i++) {
//		table.deleteRow(tableHeaderRowCount);
//		} 

//		 for (i = 0; i < sms_list.length; i++) {

//				tr = $('<tr/>');
//				tr.append("<td>" + sms_list[i].interactionDate + "</td>");
//				tr.append("<td>" + sms_list[i].interactionTime + "</td>");
//				tr.append("<td>" + sms_list[i].wyzuserName + "</td>");
//				tr.append("<td>" + sms_list[i].reason + "</td>");
//				tr.append("<td>" + sms_list[i].mobileNumber + "</td>");
//				tr.append("<td>" + sms_list[i].smsType + "</td>");
//				tr.append("<td>" + sms_list[i].smsMessage + "</td>");
//				tr.append("<td>" + sms_list[i].smsStatus + "</td>");


//          $('#sms_history').append(tr);


//      }


//    });


//}

function workshopNameList() {

    var workshopId = document.getElementById('workshopIdis').value;
    if (workshopId == "") {
        workshopId = 0;

    }
    urlLink = siteRoot + "/CallLogging/getWorkshopListName/";

    // alert("loading advisors based on workshop : "+workshopId);

    $.ajax({
        type: 'POST',
        url: urlLink,
        datatype: 'json',
        data: { workshopId: workshopId },
        cache: false,
        success: function (res) {
            if (res.success == true) {
                if (res.workshoplist != null) {
                  //  $('#lastServiceWorkshopList').empty();
                    var dropdown = document.getElementById("lastServiceWorkshopList");
                    // dropdown[0]= new Option('Select','0');
                    for (var i = 0; i < res.workshoplist.length; i++) {

                        dropdown[dropdown.length] = new Option(res.workshoplist[i], res.workshoplist[i]);

                        $("#lastServiceWorkshopList").addClass("selectpicker");
                        $('#lastServiceWorkshopList').multiselect('rebuild');
                    }

                    if (res.selectedName != "")
                    {
                        $("#lastServiceWorkshopList").val(res.selectedName);
                        $("#lastServiceWorkshopList").addClass("selectpicker");
                        $('#lastServiceWorkshopList').multiselect('rebuild');
                    }

                }
            }
            else {
                alert(res.error);
            }
        }, error(error) {

        }
    });
}



function insuracelastrenewedLocation() {

    var workshopId = document.getElementById('hddnltstserviceloc').value;
    if (workshopId == "") {
        workshopId = 0;

    }
    urlLink = siteRoot + "/CallLogging/getlatestlocationName/";

    // alert("loading advisors based on workshop : "+workshopId);

    $.ajax({
        type: 'POST',
        url: urlLink,
        datatype: 'json',
        data: { workshopId: workshopId },
        cache: false,
        success: function (res) {
            if (res.success == true) {
                if (res.workshoplist != null) {
                    $('#lastServiceWorkshopList').empty();
                    $('#lastServiceWorkshopList').append(`<option value="">--Select--</option>`);
                    var dropdown = document.getElementById("lastServiceWorkshopList");
                    // dropdown[0]= new Option('Select','0');
                    for (var i = 0; i < res.workshoplist.length; i++) {

                        dropdown[dropdown.length] = new Option(res.workshoplist[i], res.workshoplist[i]);
                        $("#lastServiceWorkshopList").addClass("selectpicker");
                        $('#lastServiceWorkshopList').multiselect('rebuild');
                    }

                }
                if (res.selectedName != "") {
                    $("#lastServiceWorkshopList").val(res.selectedName);
                    $("#lastServiceWorkshopList").addClass("selectpicker");
                    $('#lastServiceWorkshopList').multiselect('rebuild');
                   // $("#lastServiceWorkshopList").val(res.selectedName);

                }


            }
            else {
                alert(res.error);
            }
        }, error(error) {

        }
    });
}

function advisorBasedOnWorkshop() {
    var workshopId = document.getElementById('workshopIdis').value;
    if (workshopId == "") {
        workshopId = 0

    }
    // alert("loading advisors based on workshop : "+workshopId);
    var urlLink = siteRoot + "/CallLogging/getServiceAdvisorOfWorkshop/";

    $.ajax({
        type: 'POST',
        url: urlLink,
        datatype: 'json',
        data: { workshopId: workshopId },
        cache: false,
        success: function (res) {
            if (res.success == true) {
                if (res.advisorlist != null) {
                    $('#assignedToSA').empty();
                    var dropdown = document.getElementById("assignedToSA");
                    dropdown[0] = new Option('Select', '0');
                    for (var i = 0; i < res.advisorlist.length; i++) {

                        dropdown[dropdown.length] = new Option(res.advisorlist[i].advisorName, res.advisorlist[i].advisorId);
                    }



                }
            }
            else {
                alert(res.error);
            }
        }, error(error) {

        }
    });
}

function advisorBasedOnWorkshopSelection() {

    var workshopId = document.getElementById('workshop').value;
    var urlLink = siteRoot + "/CallLogging/getServiceAdvisorOfWorkshop/";

    $.ajax({
        type: 'POST',
        url: urlLink,
        datatype: 'json',
        data: { workshopId: workshopId },
        cache: false,
        success: function (res) {
            if (res.success == true) {
                if (res.advisorlist != null) {
                    $('#serviceAdvisorIdIn').empty();
                    var dropdown = document.getElementById("serviceAdvisorIdIn");
                    dropdown[0] = new Option('Select', '0');
                    for (var i = 0; i < res.advisorlist.length; i++) {

                        dropdown[dropdown.length] = new Option(res.advisorlist[i].advisorName, res.advisorlist[i].advisorId);
                    }
                }
            }
            else {
                alert(res.error);
            }
        }, error(error) {

        }
    });
    //$.ajax({
    //    url: urlLink

    //}).done(function (advisorlist) {

    //    if (advisorlist != null) {
    //        $('#serviceAdvisor').empty();
    //        var dropdown = document.getElementById("serviceAdvisor"); 
    //  dropdown[0]= new Option('Select','0');
    //   for(var i=0;i<advisorlist.length;i++){

    //    dropdown[dropdown.length] = new Option(advisorlist[i].advisorName,advisorlist[i].advisorId);
    //   }



    //        }
    //});


}
function advisorBasedOnWorkshopSelectionIn() {
    var workshopId = document.getElementById('workshopIn').value;
    var urlLink = siteRoot + "/CallLogging/getServiceAdvisorOfWorkshop/";

    $.ajax({
        type: 'POST',
        url: urlLink,
        datatype: 'json',
        data: { workshopId: workshopId },
        cache: false,
        success: function (res) {
            if (res.success == true) {
                if (res.advisorlist != null) {
                    $('#serviceAdvisorIdIn').empty();
                    var dropdown = document.getElementById("serviceAdvisorIdIn");
                    dropdown[0] = new Option('Select', '0');
                    for (var i = 0; i < res.advisorlist.length; i++) {

                        dropdown[dropdown.length] = new Option(res.advisorlist[i].advisorName, res.advisorlist[i].advisorId);
                    }
                }
            }
            else {
                alert(res.error);
            }
        }, error(error) {

        }
    });
}

function loadRosterTables() {
    var selectAgent1 = document.getElementById('selectedServiceAdvisoris').value;
    var urlRosterData = "/CREManager/loadRosterData/" + selectAgent1 + "";

    $.ajax({

        url: urlRosterData

    }).done(function (rosterData) {
        $("#loadRoaster1").find("tr").remove();
        for (i = 0; i < rosterData.length; i++) {
            var x = rosterData[i].fromDate;
            var y = rosterData[i].toDate;
            var markup1 = "<tr><td>" + x + "</td><td>" + y + "</td><td><button class='btn btn-sm btn-danger' onclick='deleteTheRoster(" + rosterData[i].id + ");'>Delete</button></td></tr>";
            $("#loadRoaster1").append(markup1);
            // $("#loadRoaster").append(markup1);

        }

    });


}


function rosterUnavialbiltyByServiceAdvisor() {
    loadRosterTables();
    $("#creTableRosterByServiceData").find("tr").remove();
    var d = new Date();
    var year = d.getFullYear();
    var day = d.getDate();
    var month = d.getMonth() + 1;

    if (month < 10) month = "0" + month;
    if (day < 10) day = "0" + day;

    var picker2 = $("<input/>", {
        type: 'text',
        id: 'numFrom1',
        name: 'numFrom',
        style: 'margin-right: 2.5cm; ',
        required: 'required',
        readonly: 'true'


    }).datepicker({

        autoclose: true,
        dateFormat: 'yy-mm-dd'
    });

    var picker3 = $("<input/>", {
        type: 'text',
        id: 'numTo1',
        name: 'numTo',
        required: 'required',
        readonly: 'true'

    }).datepicker({

        autoclose: true,
        dateFormat: 'yy-mm-dd'
    });



    var selectAgents = document.getElementById('selectedServiceAdvisoris').value;

    $("#creTableRosterByService").find("tr").remove();


    th = $('<tr>');
    th.append('<th>User Name</th>');
    th.append('<th>UnAvailable From and To Date</th>');
    th.append('<th>Add</th>');
    $('#creTableRosterByService').append(th);

    tr = $('<tr/>');
    tr.append('<td><input type="text" style="border:none" value="' + selectAgents + '" readOnly></td>');
    tr.append(picker2);
    tr.append(picker3);
    tr.append('<td><button class="btn btn-sm btn-danger" onclick="\addTheRosterForService();\">ADD</button></td>');

    $('#creTableRosterByService').append(tr);
}

function addTheRosterForService() {
    $.blockUI();
    // loadRosterTables();
    var selectAgents = document.getElementById('selectedServiceAdvisoris').value;
    var fromDate = document.getElementById('numFrom1').value;
    var toDate = document.getElementById('numTo1').value;

    if (fromDate != "" && toDate != "") {


        var urlDisposition = "/CREManager/addRosterOfUserByAjax/" + selectAgents + "/" + fromDate + "/" + toDate + "";
        $.ajax({

            url: urlDisposition

        }).done(function (data) {

            if (data == "success") {

                var markup = "<tr><td>" + fromDate + "</td><td>" + toDate + "</td><td><button class='btn btn-sm btn-danger' >Delete</button></td></tr>";
                $("#creTableRosterByServiceData").append(markup);
                $.unblockUI();
            } else if (data == "failure") {

                Lobibox.alert('error', {
                    msg: "Error in adding Unavialabilty"
                });
            } else {
                $.unblockUI();

                for (i = 0; i < data.length; i++) {
                    var From_Date = data[i].fromDate;
                    var To_Date = data[i].toDate;
                    var Id = data[i].id;
                }


                Lobibox.confirm({
                    msg: " From <b>" + fromDate + '</b> to <b>' + toDate + "</b> already Exists !</br> <i>Do You Want To Update?</i>",
                    callback: function ($this, type) {
                        if (type === 'yes') {
                            $.blockUI();
                            var urlDisposition = "/CREManager/updateRosterOfUserByAjaxData/" + selectAgents + "/" + fromDate + "/" + toDate + "";
                            // console.log(urlDisposition);
                            $.ajax({

                                url: urlDisposition
                            }).done(function (data1) {

                                $.unblockUI();


                                $("#loadRoaster1").find("tr").remove();
                                loadRosterTables();

                            });

                        } else if (type === 'no') {


                        }

                    }
                });

            }


        });


    } else {

        Lobibox.alert('error', {
            msg: "Unavialabilty Date's Are Missing"
        });


    }
}

// driver//

// -----------------------ROSTER for
// driver----------------------------------------------------//


function loadRosterforDriver() {
    var selectAgent = document.getElementById('selectedDriver').value;
    var urlRosterData = "/CREManager/loadRosterData/" + selectAgent + "";

    $.ajax({

        url: urlRosterData

    }).done(function (rosterData) {
        $("#loadRoaster2").find("tr").remove();
        for (i = 0; i < rosterData.length; i++) {
            var x = rosterData[i].fromDate;
            var y = rosterData[i].toDate;
            var markup1 = "<tr><td>" + x + "</td><td>" + y + "</td><td><button class='btn btn-sm btn-danger' onclick='deleteTheRoster(" + rosterData[i].id + ");'>Delete</button></td></tr>";
            $("#loadRoaster2").append(markup1);

        }

    });


}
function rosterUnavialbiltyByDriver() {

    loadRosterforDriver();
    $("#creTableRosterByDriverData").find("tr").remove();
    var d = new Date();
    var year = d.getFullYear();
    var day = d.getDate();
    var month = d.getMonth() + 1;

    if (month < 10) month = "0" + month;
    if (day < 10) day = "0" + day;

    var picker4 = $("<input/>", {
        type: 'text',
        id: 'numFrom2',
        name: 'numFrom',
        style: 'margin-right: 2.5cm; ',
        required: 'required',
        readonly: 'true'


    }).datepicker({

        autoclose: true,
        dateFormat: 'yy-mm-dd'
    });

    var picker5 = $("<input/>", {
        type: 'text',
        id: 'numTo2',
        name: 'numTo',
        required: 'required',
        readonly: 'true'

    }).datepicker({

        autoclose: true,
        dateFormat: 'yy-mm-dd'
    });



    var selectAgent = document.getElementById('selectedDriver').value;

    $("#creTableRosterByDriver").find("tr").remove();


    th = $('<tr>');
    th.append('<th>User Name</th>');
    th.append('<th>UnAvailable From and To Date</th>');
    th.append('<th>Add</th>');
    $('#creTableRosterByDriver').append(th);

    tr = $('<tr/>');
    tr.append('<td><input type="text" style="border:none" value="' + selectAgent + '" readOnly></td>');
    tr.append(picker4);
    tr.append(picker5);
    tr.append('<td><button class="btn btn-sm btn-danger" onclick="\addTheRosterForDriver();\">ADD</button></td>');

    $('#creTableRosterByDriver').append(tr);






}

function addTheRosterForDriver() {
    $.blockUI();
    var selectAgent = document.getElementById('selectedDriver').value;
    var fromDate = document.getElementById('numFrom2').value;
    var toDate = document.getElementById('numTo2').value;

    if (fromDate != "" && toDate != "") {


        var urlDisposition = "/CREManager/addRosterOfUserByAjax/" + selectAgent + "/" + fromDate + "/" + toDate + "";
        $.ajax({

            url: urlDisposition

        }).done(function (data) {

            if (data == "success") {

                var markup = "<tr><td>" + fromDate + "</td><td>" + toDate + "</td><td><button class='btn btn-sm btn-danger' >Delete</button></td></tr>";
                $("#creTableRosterByDriverData").append(markup);
                $.unblockUI();
            } else if (data == "failure") {

                Lobibox.alert('error', {
                    msg: "Error in adding Unavialabilty"
                });
            } else {
                $.unblockUI();

                for (i = 0; i < data.length; i++) {
                    var From_Date = data[i].fromDate;
                    var To_Date = data[i].toDate;
                    var Id = data[i].id;
                }


                Lobibox.confirm({
                    msg: " From <b>" + fromDate + '</b> to <b>' + toDate + "</b> already Exists !</br> <i>Do You Want To Update?</i>",
                    callback: function ($this, type) {
                        if (type === 'yes') {
                            $.blockUI();
                            var urlDisposition = "/CREManager/updateRosterOfUserByAjaxData/" + selectAgent + "/" + fromDate + "/" + toDate + "";
                            // console.log(urlDisposition);
                            $.ajax({

                                url: urlDisposition
                            }).done(function (data1) {

                                $.unblockUI();


                                $("#loadRoaster2").find("tr").remove();
                                loadRosterforDriver();


                            });

                        } else if (type === 'no') {


                        }

                    }
                });

            }


        });


    } else {

        Lobibox.alert('error', {
            msg: "Unavialabilty Date's Are Missing"
        });


    }
}

$('#upload_form_name').on('change', function () {
    var dropDownTypeData = $('#upload_form_name').val();

    if (dropDownTypeData == 'campaign') {
        $('.campaignExpiry').css('display', 'block');
        $('.campaignStartdate').css('display', 'block');
        $('.campaignNameInsert').css('display', 'block');
    } else {
        $('.campaignExpiry').css('display', 'none');
        $('.campaignStartdate').css('display', 'none');
        $('.campaignNameInsert').css('display', 'none');
    }
});
$('#campaignTypeData').on('change', function () {
    $('#assignCallsDiv').css('display', 'none');
    $('#selectCREDiv').css('display', 'none');
    $("#campaignNamePSF option:selected").each(function () {
        $(this).removeAttr("selected");
    });
    $("#campaignName option:selected").each(function () {
        $(this).removeAttr("selected");
    });
    var fromDate = document.getElementById('singleData');
    var todate = document.getElementById('tranferData');
    fromDate.value = fromDate.defaultValue;
    todate.value = todate.defaultValue;
    $("#model option:selected").each(function () {
        $(this).removeAttr("selected");
    });
    $("#city option:selected").each(function () {
        $(this).removeAttr("selected");
    });
    $("#workshop option:selected").each(function () {
        $(this).removeAttr("selected");
    });

    $('#data').multiselect("deselectAll", false).multiselect('refresh');
    var dropDownType = $('#campaignTypeData').val();

    if (dropDownType == '2') {
        $('.selectcampaign').css("display", "block");
    } else {
        $('.selectcampaign').css("display", "none");
    }
});

$('#campaignTypeData').on('change', function () {

    $('#assignCallsDiv').css('display', 'none');
    $('#selectCREDiv').css('display', 'none');
    $("#campaignNamePSF option:selected").each(function () {
        $(this).removeAttr("selected");
    });
    $("#campaignName option:selected").each(function () {
        $(this).removeAttr("selected");
    });
    var fromDate = document.getElementById('singleData');
    var todate = document.getElementById('tranferData');
    fromDate.value = fromDate.defaultValue;
    todate.value = todate.defaultValue;
    $("#model option:selected").each(function () {
        $(this).removeAttr("selected");
    });
    $("#city option:selected").each(function () {
        $(this).removeAttr("selected");
    });
    $("#workshop option:selected").each(function () {
        $(this).removeAttr("selected");
    });

    $('#data').multiselect("deselectAll", false).multiselect('refresh');
    var dropDownTypes = $('#campaignTypeData').val();

    if (dropDownTypes == '4' || dropDownTypes == '5' || dropDownTypes == '6' || dropDownTypes == '7') {
        $('.campaignTypePSF').css("display", "block");
        $('.model').css("display", "block");
    } else {
        $('.campaignTypePSF').css("display", "none");
        $('.model').css("display", "none");
    }
});




$(".customerNameComplain").keypress(function (e) {
    var code = e.keyCode || e.which;
    if ((code < 65 || code > 90) && (code < 97 || code > 122) && code != 32 && code != 46) {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Only alphabates are allowed.'
        });

        return false;
    }
});

$('#email').blur(function () {
    var customerEmail = $(this).val();
    var re = /^(([^<>()[\]\\.,;:\s@@\"]+(\.[^<>()[\]\\.,;:\s@@\"]+)*)|(\".+\"))@@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,3}))$/igm;
    if (re.test(customerEmail)) {

    } else {
        return false;
        $('.customerEmail').val('');
    }
});

$('.customerEmail').keypress(function (e) {
    if (e.which === 32)
        return false;
});
$('#functame').change(function () {

    var funcVal = $('#functame').val();

    if (funcVal == 'AFTERSALES') {

        $('.complainCategory').css('display', 'block');
        $('.complainSubCategory').css('display', 'block');


    } else {
        $('.complainCategory').css('display', 'none');
        $('.complainSubCategory').css('display', 'none');

    }

});
$('.number-only').keypress(function (e) {
    if (isNaN(this.value + "" + "." + String.fromCharCode(e.charCode))) {

        return false;
    }

})
$('#complaintSubmit').click(function () {

    var vehicleRegNo = $('#vehicleRegNo').val();
    var model = $('#model').val();
    var customerName = $('#customerName').val();
    var variant = $('#variant').val();
    var customerPhone = $('#customerPhone').val();
    var saledate = $('#saledate').val();
    var email = $('#email').val();
    var lastservice = $('#lastservice').val();
    var serviceadvisor = $('#serviceadvisor').val();
    var address = $('#address').val();
    var chassisno = $('#chassisno').val();
    var workshop = $('#workshop').val();
    var sname = $('#sname').val();
    var functame = $('#functame').val();
    var description = $('#description').val();
    var complaintType = $('#complaintType').val();
    var subcomplaintType = $('#subcomplaintType').val();
    if (vehicleRegNo == '') {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Vehicle Number is Required'
        });

        return false;
    }
    if (model == '') {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Model Number is Required'
        });

        return false;
    }
    if (customerName == '') {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Customer Name is Required'
        });

        return false;
    }
    if (variant == '') {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Variant Number is Required'
        });

        return false;
    }
    if (customerPhone == '') {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Customer Phone Number is Required'
        });

        return false;
    }
    if (saledate == '') {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Sale Date is Required'
        });

        return false;
    }
    if (email == '') {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Email is Required'
        });

        return false;
    }
    if (lastservice == '') {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Lastservice is Required'
        });

        return false;
    }
    if (address == '') {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Address is Required'
        });

        return false;
    }
    if (serviceadvisor == '') {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Service Advisor is Required'
        });

        return false;
    }
    if (chassisno == '') {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Chassis  Number is Required'
        });

        return false;
    }
    if (workshop == '') {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Workshop is Required'
        });

        return false;
    }
    if (sname == '0') {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Select Source Name'
        });

        return false;
    }
    if (functame == '0') {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Select Function Name'
        });

        return false;

        if (functame == 'AFTERSALES') {
            if (complaintType == '0') {

                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Select Complaint Category'
                });

                return false;
            } else if (subcomplaintType == '0') {

                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Select Complaint Sub Category'
                });

                return false;
            }

        }
    }
    if (description == '') {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Description is Required'
        });

        return false;
    } else {
        Lobibox.notify('success', {
            msg: 'Submited successfully....'
        });


    }

});


$(".viewComplaints").on("click", function () {

    // alert();
    var $row = $(this).closest("tr");    // Find the row
    var complaintNum = $row.find("#compNum").text(); // Find the text
    // alert(complaintNum);
    var $text2 = $row.find("#issueDate").text();
    var $text3 = $row.find("#userName").text();
    var veh_num = $row.find("#vREgNO").text();
    var status = $row.find("#status").text();
    var $text5 = $row.find("#x").text();
    var $text6 = $row.find("#y").text();
    var $text7 = $row.find("#z").text();
    var $text8 = $row.find("#a").text();
    var $text9 = $row.find("#issueDate").text();
    var $text10 = $row.find("#custPhone").text();
    alert($(".resolve_save").val(complaintNum));
    alert($(".resolve_savebymanager").val(complaintNum));

    var urlDisposition = "/CRE/searchcomplaintNum/" + complaintNum + "/" + veh_num + "";
    $.ajax({
        url: urlDisposition

    }).done(function (data) {
        if (data != null) {
            console.log(data);
            console.log("on clikc in the list" + complaintNum);


            $("#veh_num_pop").html(data.vehicleRegNo);
            $("#cust_phone_pop").html(data.customerMobileNo);
            $("#cust_name_pop").html(data.customerName);
            $("#cust_email").html(data.customerEmail);
            $("#serviceadvisor").html(data.serviceadvisor);
            $("#addressName").html(data.preferredAdress);
            $("#chassisno").html(data.chassisNo);
            $("#saledate").html(data.saleDate);
            $("#variant").html(data.variant);
            $("#model").html(data.model);
            $("#function").html(data.functionName);

            $("#serviceadvisor").html(data.serviceadvisor);
            $("#source").html(data.sourceName);
            $("#description").html(data.description);
            $("#subcomplaintType").html(data.subcomplaintType);
            $("#complaintType").html(data.complaintType);
            $("#function").html(data.functionName);
            $("#assignOwnership").html(data.assignedUser);
            $("#prorityName").html(data.priority);
            $("#cityName").html(data.location);


        }
    });
});

// $(".resolve_save").on('click',function(){
//            
// var complaintNum = $(".resolve_save").val();
//        
// var reasonFor = $("#reasonfor").val();
//		  
// var complaintStatus = $("#complaintStatus").val();
// var actionTaken = $("#actionTaken").val();
// var resolutionBy = $("#resolutionBy").val();
// var customerStatus =$("#customerStatus").val();
// 
// if(reasonFor ==''){
// Lobibox.notify('warning', {
// continueDelayOnInactiveTab: true,
// msg: 'Reason for is Required'
// });
//
// return false;
//		
// }if(actionTaken ==''){
// Lobibox.notify('warning', {
// continueDelayOnInactiveTab: true,
// msg: 'Action Taken is Required'
// });
//
// return false;
// }else{
// var
// urlDisposition="/CRE/updateComplaintsResolution/"+complaintNum+"/"+reasonFor+"/"+complaintStatus+"/"+customerStatus+"/"+actionTaken+"/"+resolutionBy;
// $.ajax({
// url:urlDisposition
// }).done(function(data){
// if(data!=null){
// Lobibox.alert('success', {
// msg: 'Complaint Has been Resolved Successfully....'
// });
//	
// }
// window.setTimeout(function(){location.reload()},4000)
// 
//		 
// });
// 
// }
//          
// });

$(".resolve_savebymanager").on('click', function () {
    var $row = $(this).closest("tr");
    var complaintNum = $("#compnum").text();
    var reasonFor = $("#reasonfor").val();
    var complaintStatus = $("#complaintStatus").val();
    var actionTaken = $("#actionTaken").val();
    var resolutionBy = $("#resolutionBy").val();
    var customerStatus = $("#customerStatus").val();

    // alert(complaintNum);

    if (reasonFor == '') {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Reason for is Required'
        });

        return false;

    } if (actionTaken == '') {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Action Taken is Required'
        });

        return false;
    } else {
        var urlDisposition = "/CRE/updateComplaintsResolutionByManager/" + complaintNum + "/" + reasonFor + "/" + complaintStatus + "/" + customerStatus + "/" + actionTaken + "/" + resolutionBy;
        // alert(urlDisposition);
        $.ajax({
            url: urlDisposition
        }).done(function (data) {
            if (data != null) {
                Lobibox.alert('success', {
                    msg: 'Complaint Has been Resolved Successfully....'
                });

            }
            // window.setTimeout(function(){location.reload()},4000)
            window.setTimeout(function () { location.reload() }, 1000)

        });

    }

});

// $(".getModal").on("click",function(){
// alert("inside new getCompliantInfo/");
// var $row = $(this).closest("tr"); // Find the row
// var complaintNum = $row.find("#compNum").text();
// var veh_num = $row.find("#vREgNO").text();
// console.log(complaintNum);
// console.log(veh_num);
// var urlDisposition = "/CRE/getCompliantInfo/" + complaintNum
// + "/" + veh_num +"";
// console.log(urlDisposition);
// $.ajax({
// url: urlDisposition

// }).done(function(data) {
// console.log(data);
// if (data != null) {
// $("#vNumber").html(data.vehicleRegNo);
// $("#mNumber").html(data.customerMobileNo);
// $("#cName").html(data.customerName);
// $("#eMail").html(data.customerEmail);
// $("#aDdress").html(data.preferredAdress);
// $("#chassisNumber").html(data.chassisNo);
// $("#saleDate").html(data.saleDate);
// $("#vAriant").html(data.variant);
// $("#mOdel").html(data.model);
// $("#serviceAdvisor").html(data.serviceadvisor);
// $("#descripTion1").html(data.wyzUser_id);
// $("#funcTion").html(data.functionName);
// var cattype = data.complaintType;
// var subcattype = data.subcomplaintType;
// alert(cattype);
// if(cattype === '0'){
// $("#cateGory").html("--");


// }else{

// $("#cateGory").html(data.complaintType);
// }


// if(subcattype === '0'){
// $("#subCategory1").html("--");

// }else{
// $("#subCategory1").html(data.subcomplaintType);
// }
// $("#descripTion").html(data.description);
// $("#souRce").html(data.sourceName);

// }
// });
// });
// $(".getModel").on("click",function(){
// alert("inside complaintAssignment/");
// var $row = $(this).closest("tr"); // Find the row
// var complaintNum = $row.find("#compNum").text();
// var veh_num = $row.find("#vREgNO").text();
// console.log(complaintNum);
// console.log(veh_num);
// var urlDisposition = "/CRE/complaintAssignment/" +
// complaintNum + "/" + veh_num +"";
// console.log(urlDisposition);
// $.ajax({
// url: urlDisposition

// }).done(function(data) {
// console.log(data);
// if (data != null) {
// $("#vNumber").html(data.vehicleRegNo);
// $("#mNumber").html(data.customerMobileNo);
// $("#cName").html(data.customerName);
// $("#eMail").html(data.customerEmail);
// $("#aDdress").html(data.preferredAdress);
// $("#chassisNumber").html(data.chassisNo);
// $("#saleDate").html(data.saleDate);
// $("#vAriant").html(data.variant);
// $("#mOdel").html(data.model);
// $("#serviceAdvisor").html(data.serviceadvisor);
// $("#descripTion1").html(data.wyzUser_id);
// $("#funcTion").html(data.functionName);
// var cattype = data.complaintType;
// var subcattype = data.subcomplaintType;
// alert(cattype);
// if(cattype === '0'){
// $("#cateGory").html("--");


// }else{

// $("#cateGory").html(data.complaintType);
// }


// if(subcattype === '0'){
// $("#subCategory1").html("--");

// }else{
// $("#subCategory1").html(data.subcomplaintType);
// }
// $("#descripTion").html(data.description);
// $("#souRce").html(data.sourceName);

// }
// });
// });
$('.AddComplaintResolution').click(function () {
    // var wyzUser_id = document.getElementById('#sdsds').value;
    var $row = $(this).closest("tr");
    var complaintNum = $("#compnum").text();
    var city = $("#city").val();
    var workshop = $("#workshopSelected option:selected").text();
    var functions = $("#FUNCTION").val();
    var ownership = $("#OWNERSHIP").val();
    var priority = $("#PRIORITY option:selected").text();
    var esclation1 = document.getElementById('ESCALATION1').value;
    var esclation2 = document.getElementById('ESCALATION2').value;

    // alert(workshop);
    if (esclation1 == '') {
        esclation1 = "empty";

    }
    if (esclation2 == '') {
        esclation2 = "empty";
    }
    if (city == '0') {
        Lobibox.notify('warning', {
            msg: 'Please select city'
        });
        return false;
    } if (workshop == '0') {
        Lobibox.notify('warning', {
            msg: 'Please select Location'
        });
        return false;
    }
    if (functions == '0') {
        Lobibox.notify('warning', {
            msg: 'Please select function name'
        });
        return false;
    }
    if (ownership == '0') {
        Lobibox.notify('warning', {
            msg: 'Please select an Agent to assign'
        });
        return false;
    } else {
        // alert();

        var urlDisposition = "/CRE/saveaddComplaintAssignModile/" + complaintNum + "/" + city + "/" + workshop + "/" + functions + "/" + ownership + "/" + priority + "/" + esclation1 + "/" + esclation2 + "";
        console.log(urlDisposition);
        $.ajax({
            url: urlDisposition

        }).done(function (data) {
            if (data != null) {
                Lobibox.notify('success', {
                    msg: 'Complaint Has been Assigned Successfully....'
                });

            }
            window.setTimeout(function () { location.reload() }, 3000)
        });
    }
});


$(document).ready(function () {

    $("#editFunction1").click(function () {
        $("#OFFEROne").prop("disabled", false).focus();
    });



    $("#editFunction2").click(function () {
        $("#OFFERTwo").prop("disabled", false).focus();
    });



    $("#editFunction3").click(function () {
        $("#OFFERThree").prop("disabled", false).focus();
    });



    $("#editFunction4").click(function () {
        $("#OFFERFore").prop("disabled", false).focus();
    });



    $("#editFunction5").click(function () {
        $("#OFFERFive").prop("disabled", false).focus();
    });





});

// SMS send methods in CRE call disposition page

$(document).ready(function () {


    $(".clssTemplType").click(function () {
        var type = $(this).text();
        //console.log(type);

        var id = $(this).attr('id');
        var idexact = id.substr(8);
        var idtxt = "typ_" + idexact;
        $('#' + idtxt).val(type);
    });

    $(".cmmnbtnclass").click(function () {
        var id = $(this).attr('id');
        var idexact = id.substr(3);
        var idtxt = "txt_" + idexact;
        $('#' + idtxt).prop("disabled", false).focus();
    });



    //$('.smsSendType').click( function() {

    //	var sms = $('#smstemplate').val();
    //	var smsType = $('#smstype').val();
    //	var smsInteractionType;
    //	var smsStatus;
    //	if(this.id == 'smsSendbtn') {
    //		smsInteractionType = 'Mobile SMS';
    //		smsStatus = 'true';
    //	}
    //	else if(this.id == 'whatsAppBtn') {
    //		if(submitWhatsAppStatus()){
    //			smsInteractionType = 'WhatsApp Msg';
    //			var Status = document.getElementById('whatsAppStatus');
    //			smsStatus = Status.value;
    //		}
    //		else {
    //			return false;
    //		}
    //	}

    //	// ins quotation
    //	var insQu = $('#insQuotation').val();
    //	if(insQu == 'INS QUOTATION') {
    //		var smsType = insQu;
    //		var sms = $('#insQuotationMsg').val();
    //		sms = sms.replace(/%20/g, " ");
    //		sms = sms.replace(/%0A/g, " ");
    //	}
    //	//upload file msg
    //	var insQu = $('#insQuotation').val();
    //	if(insQu == 'UPLOAD_DOC') {
    //		var smsType = insQu;
    //		var sms = $('#insQuotationMsg').val();
    //		sms = sms.replace(/%20/g, " ");
    //	}
    //	var smsreference = $('#smsrequestReference').val();

    //	var urlSendSMS = "/CRE/sendSMS/"+smsreference;

    //	var smsmessage = {'message':'','type':'','interactiontype':'','status':''};
    //	smsmessage.message = sms;
    //	smsmessage.type = smsType;
    //	smsmessage.interactiontype = smsInteractionType;
    //	smsmessage.status = smsStatus;
    //	console.log(smsmessage);

    //    $.ajax({
    //		type :  "POST",
    //		dataType: 'json',
    //		data: JSON.stringify(smsmessage),
    //		contentType: "application/json; charset=utf-8",
    //		url:urlSendSMS 
    //	}).done(function(data){
    //		$('#insQuotation').val('0');
    //		console.log("dta"+data);

    //		$.unblockUI();
    //	if(data!=null){


    //			Lobibox.alert('success', {
    //            msg: data
    //			});

    //	}
    //	}
    //	).fail(function( jqXHR, textStatus ) {
    //		//$.unblockUI();
    //			Lobibox.alert('error', {
    //            msg: 'SMS could not be sent please try later or contact support'
    //			});

    //	}
    //	);

    //});


});

function submitWhatsAppStatus() {
    var status = document.getElementById("whatsAppStatus").value;
    console.log("status of whatsapp " + status);
    if (status == 2) {
        Lobibox.notify('warning', {
            msg: "Please enter the status"
        });
        return false;
    }
    else {
        return true;
    }
}


// Driver list


function driverbasedOnWorkshopSelection() {


    var workshopId = document.getElementById('workshop').value;
    var urlLink = "/CRE/listDrivers/" + workshopId + "";

    $.ajax({
        url: urlLink

    }).done(function (driverlist) {

        if (driverlist != null) {
            $('#driverIdSelect').empty();
            var dropdown = document.getElementById("driverIdSelect");
            dropdown[0] = new Option('Select', '0');
            for (var i = 0; i < driverlist.length; i++) {

                dropdown[dropdown.length] = new Option(driverlist[i].driverName, driverlist[i].id);
            }

        }
    });


}

$(document).ready(function () {
    $('.example4321').DataTable({
        responsive: true
    });
});






function showWhatsappPopup(insId) {
    //alert("showWhatsappPopup insId: " +insId);
    var custId = $('#customer_Id').val();
    var urlLink = "/getUpdatedSMSforInsQuotation/" + custId + "/" + insId;
    $.ajax({
        url: urlLink

    }).done(function (sms) {

        console.log('smsURL ' + sms[1]);
        window.open(sms[1], '_blank');
        $('#insQuotation').val('INS QUOTATION');
        $('#insQuotationMsg').val(sms[0]);
        $('#myModalWhatsApp').modal('show');
        $('#myModalWhatsApp').modal({
            backdrop: 'static',
            keyboard: false
        });
    });

}


//Tagging user changes


//New Lead Of user locations

function loadLeadBasedOnUserLocation(lead, tag) {
    console.log("lead is " + lead);
    console.log("tag is " + tag);


    if ($("#" + lead).is(":checked")) {
        var leadId = document.getElementById(lead).value;
        var wyzuserId = document.getElementById("wyzUser_Id").value;
        var moduletype = document.getElementById("typeOfDispoPageView").value;

        var urlLink = siteRoot + "/CallLogging/getLeadNamesbyLeadId/";
        console.log("lead urlLink : " + urlLink);

        $.ajax({
            type: 'POST',
            url: urlLink,
            datatype: 'json',
            data: { leadId: leadId, userId: wyzuserId, moduletype: moduletype },
            cache: false,
            success: function (res) {
                if (res.success == true) {
                    if (res.leadName.length != 0) {
                        $('#' + tag).empty();
                        var dropdown = document.getElementById(tag);

                        for (var i = 0; i < res.leadName.length; i++) {

                            dropdown[dropdown.length] = new Option(res.leadName[i].leadName, res.leadName[i].leadName);

                        }

                    } else {

                        $('#' + tag).empty();
                        var dropdown = document.getElementById(tag);

                        for (var i = 0; i < res.leadName.length; i++) {

                            dropdown[dropdown.length] = new Option(res.leadName[i].leadName, res.leadName[i].leadName);

                        }
                        Lobibox.notify('warning', {
                            continueDelayOnInactiveTab: true,
                            msg: 'No tagging present for the location'
                        });

                    }
                }
                else {
                    alert(res.error);
                }
            }, error(error) {

            }
        });
    }
    else {
        $('#' + tag).empty();
    }

   

    
}


//lead change based on location
function loadLeadBasedOnUserLocationSelected(locais, lead, tag) {
    console.log("lead is " + lead);
    console.log("tag is " + tag);

    var locaisName = document.getElementById(locais).value;
    var leadId = document.getElementById(lead).value;
    var wyzuserId = document.getElementById("wyzUser_Id").value;
    var moduletype = document.getElementById("typeOfDispoPageView").value;

    var urlLink = siteRoot + "/CallLogging/getLeadNamesbyLeadLocationData/";
    //console.log("lead urlLink : "+urlLink);

    $.ajax({
        type: 'POST',
        url: urlLink,
        datatype: 'json',
        data: { leadId: leadId, userId: wyzuserId, moduletype: moduletype, locaisName: locaisName },
        cache: false,
        success: function (res) {
            if (res.success == true) {
                if (res.leadName.length != 0) {

                    $('#' + tag).empty();
                    var dropdown = document.getElementById(tag);

                    for (var i = 0; i < res.leadName.length; i++) {

                        dropdown[dropdown.length] = new Option(res.leadName[i].name, res.leadName[i].id);

                    }
                } else {

                    $('#' + tag).empty();
                    var dropdown = document.getElementById(tag);

                    for (var i = 0; i < res.leadName.length; i++) {

                        dropdown[dropdown.length] = new Option(res.leadName[i].name, res.leadName[i].id);

                    }
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'No tagging present for the location'
                    });

                }
            }
            else {
                alert(res.error);
            }
        }, error(error) {

        }
    });
}


function sendEmail(moduleId, wyzId, quotId, custId, vehId) {
    $("#emailCredentialId option:selected").each(function () {
        $(this).removeAttr("selected");
    });
    $('#emailPassword').val('');

    var urlDisposition = "/getEmailCredentialList/" + moduleId + "/" + wyzId;
    $.ajax({

        url: urlDisposition
    }).done(function (data1) {
        console.log("data1 " + data1.length);
        if (data1 != null) {
            $('#emailCredentialId').empty();
            var dropdown = document.getElementById("emailCredentialId");
            dropdown[0] = new Option('--Select--', '0');
            for (var i = 0; i < data1.length; i++) {

                dropdown[dropdown.length] = new Option(data1[i].userEmail, data1[i].id);
            }
        }
    });
    $('#myModalEmail').modal({
        backdrop: 'static',
        backdrop: 'static',
        keyboard: false
    });

    $('#myModalEmail').modal('show');

    $('#insQuotationId').val(quotId);
}

$('#sendQuotationEmail').click(function () {
    var credentialId = $("#emailCredentialId").val();
    var quotId = $("#insQuotationId").val();
    var vehId = $("#vehical_Id").val();
    var custId = $("#customer_Id").val();
    var quotType = "INSQUOTATION";
    var module = "2";
    var password = $("#emailPassword").val();

    if (credentialId == '0') {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: "Select Email Id"
        });
        return false;
    }
    $.blockUI();
    var urlLink = "/sendEmail";

    $.ajax({
        type: 'POST',
        data: {
            quotType: quotType,
            credId: credentialId,
            module: module,
            custId: custId,
            vehId: vehId,
            quotId: quotId,
            password: password
        },
        url: urlLink,
        success: function (data) {
            $.unblockUI();
            if (data == 'Email Sent') {
                Lobibox.notify('success', {
                    continueDelayOnInactiveTab: true,
                    msg: data
                });
            }
            else {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: data
                });
            }

        },
        error: function () {
            $.unblockUI();

        }


    });

});


$('#emailCredentialId').change(function () {
    $('#emailPassword').val('');
    var credId = $(this).val();
    var urlLink = "/emailLoginPasswordAvailability/" + credId;
    console.log(urlLink);
    $.ajax({
        url: urlLink

    }).done(function (data) {
        console.log("emailCredentialId " + data);
        if (data == '1') {
            document.getElementById('emailPasswordDiv').style.display = "none";
        }
        else {
            document.getElementById('emailPasswordDiv').style.display = "block";
        }
    });

});

//function emailHistoryOfVehicle() {
//	 var customerId = document.getElementById('customer_Id').value;

//	    //alert(customerId);
//	    var urlDisposition = "/CRE/getEmailHistoryOfCustomerId/" + customerId + "";
//	    $.ajax({
//	        url: urlDisposition

//	    }).done(function (email_list) {


//	    	var tableHeaderRowCount = 1;
//			var table = document.getElementById('email_history');
//			var rowCount = table.rows.length;
//			for (var i = tableHeaderRowCount; i < rowCount; i++) {
//			table.deleteRow(tableHeaderRowCount);
//			} 

//			 for (i = 0; i < email_list.length; i++) {
//	             	var cc = email_list[i].emailCC;
//					var str = email_list[i].smsMessage;
//					var to = email_list[i].toEmailAddress;
//					var from = email_list[i].fromEmailAddress;

//	             	tr = $('<tr/>');
//					tr.append("<td>" + email_list[i].interactionDate + "</td>");
//					tr.append("<td>" + email_list[i].interactionTime + "</td>");
//					tr.append("<td>" + email_list[i].wyzuserName + "</td>");
//					tr.append("<td title='"+email_list[i].fromEmailAddress+"'>" + from.substring(0,15) + "</td>");
//					tr.append("<td title='"+email_list[i].toEmailAddress+"'>" + to.substring(0,15) + "</td>");
//					tr.append("<td>" + email_list[i].smsType + "</td>");
//					tr.append("<td title='"+email_list[i].smsMessage+"'>" + str.substring(0,30) + "</td>");
//					tr.append("<td>" + email_list[i].reason + "</td>");


//	          $('#email_history').append(tr);


//	      }


//	    });
//}  

function showWhatsappPopupForUpload(msg) {
    $('#insQuotation').val('UPLOAD_DOC');
    $('#insQuotationMsg').val(msg);
    $('#myModalWhatsApp').modal('show');
    $('#myModalWhatsApp').modal({
        backdrop: 'static',
        keyboard: false
    });

}

function showEmailPopup(type) {
    $('#emailSubject').html('');
    $('#emailtemplate').html('');
    $('#emailSubject').val('');
    $('#emailtemplate').val('');
    $('#emailPasswords').html('');
    $('#emailPasswords').val('');
    $('#attachedDocs').empty();
    emailArr = type.split(',');

    wyzId = $('#wyzUser_Id').val();
    var urlDisposition = "/getEmailCredentialList/2/" + wyzId;
    $.ajax({
        url: urlDisposition
    }).done(function (data1) {
        console.log("data1 " + data1.length);
        if (data1 != null) {
            $('#emailCredentialIds').empty();
            var dropdown = document.getElementById("emailCredentialIds");
            dropdown[0] = new Option('--Select--', '0');
            for (var i = 0; i < data1.length; i++) {
                dropdown[dropdown.length] = new Option(data1[i].userEmail, data1[i].id);
            }
        }
    });
    //alert(emailArr[0]);
    $('#docUploadId').val(emailArr[0]);
    var template = emailArr[1];
    $('#docUploadTypeTemplate').val(template);

    var docId = $('#docUploadId').val();
    var vehId = $('#vehical_Id').val();
    var custId = $('#customer_Id').val();

    var urlDisposition = "/getEmailSubjectAndBody/3/" + template + "/" + vehId + "/" + custId + "/" + docId;
    $.ajax({
        url: urlDisposition
    }).done(function (data2) {
        console.log("data2 " + data2.length);
        if (data2 != null) {
            console.log("uploadFileName " + data2[0].uploadFileName);
            console.log("uploadFilePath " + data2[0].uploadFilePath);
            $('#emailSubject').val(data2[0].uploadFileName);
            $('#emailtemplate').val(data2[0].uploadFilePath);
            $('#emailSubject').html(data2[0].uploadFileName);
            $('#emailtemplate').html(data2[0].uploadFilePath);
        }
        if (data2.length > 1) {

            filelist = '<div class="col-sm-3"> <label style="font-weight:normal;"> <input type="checkbox" name="fileList" value="' + data2[1].uploadFilePath + '" checked>&nbsp;' + data2[1].uploadFileName + '</label> </div>';
            for (var i = 2; i < data2.length; i++) {
                filelist += '<div class="col-sm-3"> <label style="font-weight:normal;"> <input type="checkbox" name="fileList" value="' + data2[i].uploadFilePath + '" checked>&nbsp;' + data2[i].uploadFileName + '</label> </div>';
            }
            if (typeof (filelist) !== "undefined") {
                document.getElementById('attachedDocs').innerHTML = filelist;
                $('#attachedDocs').css('border', 'thin solid black');
            }
        }

    });
    $('#myModalEmailPopUp').modal('show');
}

//$('#sendManualEmailUpload').click(function() {
//	id = $('#docUploadId').val();
//	var selected_docs = new Array();
//	$("input:checkbox[name=fileList]:checked").each(function(){
//		selected_docs.push($(this).val());
//	});
//	var selecteddocs = selected_docs.join(",");
//	//alert(selecteddocs);
//	 var credentialId = $("#emailCredentialIds").val();
//	  var docId = $("#docUploadId").val();
//	  var vehId =  $("#vehical_Id").val();
//	  var custId = $("#customer_Id").val();
//	  var password =  $("#emailPasswords").val(); 
//	  var body = $("#emailtemplate").val(); 
//	  var subject = $("#emailSubject").val();  
//	  var template = $('#docUploadTypeTemplate').val();
//	  if(credentialId == '0') {
//		  Lobibox.notify('warning', {
//	            continueDelayOnInactiveTab: true,
//	            msg: "Select Sender Email Id"
//	        });
//		  return false;
//	  }
//	  $.blockUI();
//	  var urlLink="/sendManualEmailUpload";

//		$.ajax({
//			type: 'POST',
//	        data: {
//	        	docId:docId,
//	        	template:template,
//		    	credId: credentialId,
//		    	custId:custId,
//		    	vehId:vehId,
//		    	subject:subject,
//		    	body:body,
//		    	password:password,
//		    	selecteddocs:selecteddocs
//			},
//	        url: urlLink,
//	        success: function (data) {

//	        	$.unblockUI();
//	        	if(data=='Email Sent') {
//	        		Lobibox.notify('success', {
//			            continueDelayOnInactiveTab: true,
//			            msg: data
//			        });
//	        	}
//	        	else {
//	        		Lobibox.notify('warning', {
//			            continueDelayOnInactiveTab: true,
//			            msg: data
//			        });
//	        	}

//	        },
//	        error: function () {
//	        	$.unblockUI();

//	        }

//		});
//		 $('#docUploadId').val('0');
//	        $('#docUploadTypeTemplate').val('0');
//	        $('#emailSubject').html('');
//	        $('#emailtemplate').html('');
//	        $('#emailSubject').val('');
//	        $('#emailtemplate').val('');
//	        $('#emailPasswords').html('');
//	        $('#emailPasswords').val('');
//	        $('#attachedDocs').empty();
//});

function ajaxLoadDealerPanelList() {

    var dealercode = $('#PkDealercode').val();
    var urlLink = siteRoot + "/CallLogging/getDealer";

    $.ajax({
        type: 'POST',
        url: urlLink,
        datatype: 'json',
        data: {},
       // cache: false,
        success: function (res) {
            if (res.success == true) {
                if (res.dealerPanellist != null) {
                    $('#dealerNameId').empty();
                    var dropdown = document.getElementById("dealerNameId");
                    dropdown[0] = new Option('Select', '0');
                    if (dealercode == "HARPREETFORD" || dealercode =="HANSHYUNDAI") {
                        for (var i = 0; i < res.dealerPanellist.length; i++) {
                            dropdown[dropdown.length] = new Option(res.dealerPanellist[i].dealerName, res.dealerPanellist[i].dealerName);
                         
                        }
                    }
                    else {
                        for (var i = 0; i < res.dealerPanellist.length; i++) {
                            //console.log("res.dealerPanellist[i].dealerName size : "+res.dealerPanellist[i].dealerName);
                            dropdown[dropdown.length] = new Option(res.dealerPanellist[i].workshopCode + "-" + res.dealerPanellist[i].dealerName, res.dealerPanellist[i].workshopCode + "-" + res.dealerPanellist[i].dealerName);
                            
                        }
                    }
                    $("#dealerNameId").addClass("selectpicker");
                    $('#dealerNameId').multiselect('rebuild');
                }
            }
            else {
                alert(res.error);
            }
        }, error(error) {

        }
    });
}

function marutiServiceModal(jobcardloc) {
    //alert("laborDetail: "+laborDetail+ " defectDetail:" +defectDetail);
    var urlDisposition = "/getServiceByJobcardlocation/" + jobcardloc;
    var defectDetail = '';
    var laborDetail = '';
    var jobcardDetail = '';


    $.ajax({
        url: urlDisposition

    }).done(function (defectDetails) {

        var details = defectDetails;
        defectDetail = details[0];
        laborDetail = details[1];
        jobcardDetail = details[2];
        if (defectDetail == '' && laborDetail == '' && jobcardDetail == '') {
            var divdetails = '<div><center><p>No Data Available</p></center></div>';
            $('#showServiceInfo').empty();
            $('#showServiceInfo').append(divdetails);
        }
        else {
            //alert("defectDetail "+defectDetail+ " laborDetail " +laborDetail);
            var divdetails = '<div><label>Labour Details :</label><h5>' + laborDetail + '</h5><br><label>Defect Details :</label><h5>' + defectDetail + '</h5><br><label>JobCard Details :</label><h5>' + jobcardDetail + '</h5></div>';
            $('#showServiceInfo').empty();
            $('#showServiceInfo').append(divdetails);
        }
    });

}

function assignManually(assignId) {
    //alert(assignId);
    $('#assignId').val(assignId);
    $('#manualAssignmentDiv').modal('show');
}


$('#manualAssign').click(function () {
    id = $('#assignId').val();
    var credentialId = $("#manualCREId").val();
    if (credentialId == '0') {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: "Select CRE"
        });
        return false;
    }
    $.blockUI();
    var urlLink = "/assignCallManually";

    $.ajax({
        type: 'POST',
        data: {
            id: id,
            credentialId: credentialId
        },
        url: urlLink,
        success: function (data) {

            $("#manualCREId option:selected").each(function () {
                $(this).removeAttr("selected");
            });
            $.unblockUI();
            if (data == 'done') {
                Lobibox.notify('success', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Assignment Done'
                });
            }
            window.setTimeout(function () { location.reload() }, 1000);

        },
        error: function () {
            $.unblockUI();
            window.setTimeout(function () { location.reload() }, 1000);
        }

    });
});


//AKASH
function pmsMileage() {

    $('#pmsLabourMileage').attr('disabled', false);


    var modelid = $('#pmsLabourModel').val();

    $.ajax({
        type: 'POST',
        url: siteRoot + "/CallLogging/getpmsMileage/",
        datatype: 'json',
        cache: false,
        data: { modelid: modelid },
        success: function (res) {
            if (res.success == true) {
                var pmsLabourMileage = $('#pmsLabourMileage');
                $(pmsLabourMileage).empty();
                $(pmsLabourMileage).append(`<option value=''>--Select--</option>`);

                if (res.data.length > 0) {

                    for (var i = 0; i < res.data.length; i++) {
                        $(pmsLabourMileage).append(`<option value='${res.data[i].id}'>${res.data[i].mileage}</option>`)
                    }


                }
                else {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'No Data Found'
                    });
                }
            }
            else if (res.success == false) {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: res.error
                });
            }
        },
        error: function (ex) {
            alert(ex);
        }


    });

}

//function pmsLabourSubmitKataria() {
//    $('#pmsLabourMileage').attr('disabled', true);
//    if ($('#pmsLabourCity').val() == "") {

//        Lobibox.notify('warning', {
//            msg: 'please select city.'
//        });

//        return false;
//    }
//    if ($('#pmsLabourMileage').val() == "") {
//        Lobibox.notify('warning', {
//            msg: 'Please select milage.'
//        });

//        return false;
//    }
//    if ($('#pmsLabourModel').val() == "") {
//        Lobibox.notify('warning', {
//            msg: 'Please select model.'
//        });

//        return false;
//    }
//    if ($('#pmsLabourfuelType').val() == "") {
//        Lobibox.notify('warning', {
//            msg: 'Please select fueltype.'
//        });

//        return false;
//    }
//    else {
//        $('#pmsLabour').modal('hide');
//        $('#pmsLabour1').modal('show');
//    }

//    var city = $('#pmsLabourCity').val();
//    var model = $('#pmsLabourModel').val();
//    var mileage = $('#pmsLabourMileage').val();
//    var fueltype = $('#pmsLabourfuelType').val();

//    $.ajax({
//        type: 'POST',
//        url: siteRoot + "/CallLogging/getpmsdetails/",
//        datatype: 'json',
//        cache: false,
//        data: { city: city, model: model, mileage: mileage, fueltype: fueltype },
//        success: function (res) {
//            if (res.success == true) {
//                $('#labourAmount').val(res.data.labourAmount);
//                $('#wheelAlignment').val(res.data.wheelAlignment);
//                $('#wheelBalancing').val(res.data.wheelBalancing);
//                $('#basic').val(res.data.basic);
//                $('#hygiene').val(res.data.hygiene);
//                $('#engineOil').val(res.data.engineOil);
//                $('#oilFilter').val(res.data.oilFillter);
//                $('#brakeFluid').val(res.data.brakeFluid);
//                $('#coolant').val(res.data.coolant);
//                $('#sparkPlug').val(res.data.sparkPlug);
//                $('#airFilter').val(res.data.airFilter);
//                $('#fuelFilter').val(res.data.fuelFilter);
//                $('#belt').val(res.data.belt);
//                $('#gasketDrainPlug').val(res.data.gasketDrainPlug);
//                $('#oilFilterGasket').val(res.data.oilFilterGasket);
//                $('#clutchFluid').val(res.data.clutchFluid);
//                $('#transmissionOil').val(res.data.transmissionOil);
//                $('#other1').val(res.data.other1);
//                $('#other2').val(res.data.other2);
//                pmsLabourReset();
//                getFinalADDONN();
//                getFinalADDONKataria();
//                getFinalLiability();
//            }
//        },
//        error: function (ex) {
//            alert(ex);
//        }
//    });
//}

function pmsLabourSubmit() {
    var dealercode = $('#PkDealercode').val();
    if (dealercode == "KATARIA") {
        $('#katariaServiceEstDiv').show();
    }
    else {
        $('#katariaServiceEstDiv').hide();
    }
  
   
    if ($('#pmsLabourCity').val() == "") {

        Lobibox.notify('warning', {
            msg: 'please select city.'
        });

        return false;
    }
    if ($('#pmsLabourMileage').val() == "") {
        Lobibox.notify('warning', {
            msg: 'Please select milage.'
        });

        return false;
    }
    if ($('#pmsLabourModel').val() == "") {
        Lobibox.notify('warning', {
            msg: 'Please select model.'
        });

        return false;
    }
    if ($('#pmsLabourfuelType').val() == "") {
        Lobibox.notify('warning', {
            msg: 'Please select fueltype.'
        });

        return false;
    }
    else {
        $('#pmsLabour').modal('hide');
        $('#pmsLabour1').modal('show');


    }

    var city = $('#pmsLabourCity').val();
    var model = $('#pmsLabourModel').val();
    var mileage = $('#pmsLabourMileage').val();
    var fueltype = $('#pmsLabourfuelType').val();

    $.ajax({
        type: 'POST',
        url: siteRoot + "/CallLogging/getpmsdetails/",
        datatype: 'json',
        cache: false,
        data: { city: city, model: model, mileage: mileage, fueltype: fueltype },
        success: function (res) {
            if (res.success == true) {

                //$('#labourAmount').val(res.data.labourAmount);
                //$('#labourAmount').val(res.data.labourAmount);
                $('#labourAmount').val(res.data.labourAmount);
                $('#wheelAlignment').val(res.data.wheelAlignment);
                $('#wheelBalancing').val(res.data.wheelBalancing);
                $('#basic').val(res.data.basic);
                $('#hygiene').val(res.data.hygiene);
                $('#engineOil').val(res.data.engineOil);
                $('#oilFilter').val(res.data.oilFillter);
                $('#brakeFluid').val(res.data.brakeFluid);
                $('#coolant').val(res.data.coolant);
                $('#sparkPlug').val(res.data.sparkPlug);
                $('#airFilter').val(res.data.airFilter);
                $('#fuelFilter').val(res.data.fuelFilter);
                $('#belt').val(res.data.belt);
                $('#gasketDrainPlug').val(res.data.gasketDrainPlug);
                $('#oilFilterGasket').val(res.data.oilFilterGasket);
                $('#clutchFluid').val(res.data.clutchFluid);
                $('#transmissionOil').val(res.data.transmissionOil);
                $('#other1').val(res.data.other1);
                $('#other2').val(res.data.other2);
                pmsLabourReset();

                //getFinalADDONN();
                //getFinalADDON();
                calculateLabour();
                calculateParts();
                getFinalLiability();
            }
        },
        error: function (ex) {
            alert(ex);
        }


    });
}
function pmsLabourReset() {
    $('#pmsLabourCity').val('');
    $('#pmsLabourMileage').empty();
    $('#pmsLabourMileage').append(`<option value="">--Select--</option>`);
    $('#pmsLabourModel').val('');
    $('#pmsLabourfuelType').val('');
    $('#other1').val('');
    $('#other2').val('');
}

//ADD ON


//$('#engineOil').on("keyup", function () {

//    getFinalADDON();
//    getFinalLiability();

//});
//$('#oilFilter').on("keyup", function () {

//    getFinalADDON();
//    getFinalLiability();

//});
// $('#brakeFluid').on("keyup", function () {

//    getFinalADDON();
//    getFinalLiability();

//});
//$('#coolant').on("keyup", function () {

//    getFinalADDON();
//    getFinalLiability();

//});
//$('#sparkPlug').on("keyup", function () {

//    getFinalADDON();
//    getFinalLiability();

//});
//$('#airFilter').on("keyup", function () {

//    getFinalADDON();
//    getFinalLiability();

//});
//$('#fuelFilter').on("keyup", function () {

//    getFinalADDON();
//    getFinalLiability();

//});
//$('#belt').on("keyup", function () {

//    getFinalADDON();
//    getFinalLiability();

//});
//$('#other1').on("keyup", function () {

//    getFinalADDONNN();
//    getFinalLiability();

//});
//$('#other2').on("keyup", function () {

//    getFinalADDONNNN();
//    getFinalLiability();

//});
//$('#wheelAlignment').on("keyup", function () {

//    getFinalADDONN();
//    getfinalliability();

//});
//$('#wheelBalancing').on("keyup", function () {

//    getFinalADDONN();
//    getfinalliability();

//});
//$('#basic').on("keyup", function () {

//    getFinalADDONN();
//    getfinalliability();

//});
//$('#hygiene').on("keyup", function () {

//    getFinalADDONN();
//    getfinalliability();

//});

//$('#gasketDrainPlug').on("keyup", function () {

//    getFinalADDONKataria();
//    getFinalLiability();

//});
//$('#oilFilterGasket').on("keyup", function () {

//    getFinalADDONKataria();
//    getFinalLiability();

//});
//$('#clutchFluid').on("keyup", function () {

//    getFinalADDONKataria();
//    getFinalLiability();

//});
//$('#transmissionOil').on("keyup", function () {

//    getFinalADDONKataria();
//    getFinalLiability();

//});

function calculateLabour() {
    var WheelAlignment = document.getElementById('wheelAlignment').value;
    var WheelBalancing = document.getElementById('wheelBalancing').value;
    var Basic = document.getElementById('basic').value;
    var Hygiene = document.getElementById('hygiene').value;

    finalLabourAmount = (+Basic) + (+WheelAlignment) + (+WheelBalancing) + (+Hygiene);

    $('#labourAmount').val(finalLabourAmount);

    getFinalLiability();
}
function calculateParts() {
    var EngineOil = document.getElementById('engineOil').value;
    var OilFilter = document.getElementById('oilFilter').value;
    var BrakeFluid = document.getElementById('brakeFluid').value;
    var Coolant = document.getElementById('coolant').value;
    var SparkPlug = document.getElementById('sparkPlug').value;
    var AirFilter = document.getElementById('airFilter').value;
    var FuelFilter = document.getElementById('fuelFilter').value;
    var Belt = document.getElementById('belt').value;
    var GasketDrainPlug = document.getElementById('gasketDrainPlug').value;
    var OilFilterGasket = document.getElementById('oilFilterGasket').value;
    var ClutchFluid = document.getElementById('clutchFluid').value;
    var TransmissionOil = document.getElementById('transmissionOil').value;

    finalPartsAmount = (+EngineOil) + (+OilFilter) + (+BrakeFluid) + (+Coolant) + (+SparkPlug) + (+AirFilter) + (+FuelFilter) + (+Belt) + (+GasketDrainPlug) + (+OilFilterGasket) + (+ClutchFluid) + (+TransmissionOil);

    $('#partsAmount').val(finalPartsAmount);
    getFinalLiability();
}
//function getFinalADDON() {
//    console.log("addon");
//    var EngineOil = document.getElementById('engineOil').value;
//    var OilFilter = document.getElementById('oilFilter').value; 
//    var BrakeFluid = document.getElementById('brakeFluid').value;
//    var Coolant = document.getElementById('coolant').value;
//    var SparkPlug = document.getElementById('sparkPlug').value;
//    var AirFilter = document.getElementById('airFilter').value;
//    var FuelFilter = document.getElementById('fuelFilter').value;
//    var Belt = document.getElementById('belt').value;
//    var GasketDrainPlug = document.getElementById('gasketDrainPlug').value;
//    var OilFilterGasket = document.getElementById('oilFilterGasket').value;
//    var ClutchFluid = document.getElementById('clutchFluid').value;
//    var TransmissionOil = document.getElementById('transmissionOil').value;
//    var finaladdOnis = 0;
//    var dealercode = $('#PkDealercode').val();
//    if (dealercode == "KATARIA") {
//         finaladdOnis = (+EngineOil) + (+OilFilter) + (+BrakeFluid) + (+Coolant) + (+SparkPlug) + (+AirFilter) + (+FuelFilter) + (+Belt) + (+GasketDrainPlug) + (+OilFilterGasket) + (+ClutchFluid) + (+TransmissionOil);
//    }
//    else {
//         finaladdOnis = (+EngineOil) + (+OilFilter) + (+BrakeFluid) + (+Coolant) + (+SparkPlug) + (+AirFilter) + (+FuelFilter) + (+Belt)/* + (+Other1) + (+Other2)*/;
//    }
    
//    //var finaladdOnis = (+EngineOil) + (+OilFilter) + (+BrakeFluid) + (+Coolant) + (+SparkPlug) + (+AirFilter) + (+FuelFilter) + (+Belt)/* + (+Other1) + (+Other2)*/;

//    $('#partsAmount').html(Math.abs(finaladdOnis));
//    $('#partsAmount').val(Math.abs(finaladdOnis));
//    //$('#partsAmount').html(Math.round(finaladdOnis).toFixed(2));
//    //$('#partsAmount').val(Math.round(finaladdOnis).toFixed(2));

//    getFinalLiability();

//}

//function getFinalADDONN() {
//    console.log("addon");
//    var WheelAlignment = document.getElementById('wheelAlignment').value;
//    var WheelBalancing = document.getElementById('wheelBalancing').value;
//    var Basic = document.getElementById('basic').value;
//    var Hygiene = document.getElementById('hygiene').value;

//     finaladdOnisss = (+Basic) + (+WheelAlignment) + (+WheelBalancing) + (+Hygiene);

//    $('#labourAmount').html(Math.abs(finaladdOnisss));
//    $('#labourAmount').val(Math.abs(finaladdOnisss));

//    getFinalLiability();

//}

//function getFinalADDONNN() {
//    console.log("addon");
    
//    var Other1 = document.getElementById('other1').value;

//    //var finaladdOnissss = (+Other1);

//    $('#other1').html(Math.abs(Other1));
//    $('#other1').val(Math.abs(Other1));

//    getFinalLiability();

//}

//function getFinalADDONNNN() {
//    console.log("addon");

//    var Other2 = document.getElementById('other2').value;

//    //var finaladdOnisssss = (+Other2);

//    $('#other2').html(Math.abs(Other2));
//    $('#other2').val(Math.abs(Other2));

//    getFinalLiability();

//}

function getFinalLiability() {

    console.log("addon");
    
    var LabourAmount = document.getElementById('labourAmount').value;
    var PartsAmount = document.getElementById('partsAmount').value;
    var Other1 = document.getElementById('other1').value;
    var Other2 = document.getElementById('other2').value;

    var finaladdOniss = (+LabourAmount) + (+PartsAmount) + (+Other1) + (+Other2);

    $('#total').html(Math.abs(finaladdOniss));
    $('#total').val(Math.abs(finaladdOniss));

}

$('.numberOnly').keypress(function (e) {

    if (isNaN(this.value + "" + "." + String.fromCharCode(e.charCode))) {
        return false;
    }

});

function ajaxCallToLoadPhoneNumberByWorkshop(ThisDDL, BindingText) {


    var workshopId = document.getElementById(ThisDDL).value;

    //alert(selectedCity);

    var urlLink = siteRoot + "/CallLogging/GetPhoneNumberByLocation/";

    $.ajax({
        type: 'POST',
        url: urlLink,
        datatype: 'json',
        data: { workshopId: workshopId },
        cache: false,
        success: function (phoneNumber) {

            $('#' + BindingText).val(phoneNumber);
        }, error(error) {
            console.log(error);
        }
    });
}