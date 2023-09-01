
//const { Session } = require("node:inspector");

//const { Session } = require("node:inspector");

var emailList = {};
var pvrCubeId = 0;
var dict = {};
var UserName = $('#PkUsername').val();
var location_cityId = $('#location_Id').val();
var UserRole = $('#userrole').val();
var postsalesCampaignId = 0;
$(document).ready(function () {

    var oem = $('#PkOEM').val();

    var fieldenabled = $('#fieldenabled').val();


    var custDND = $('#PkDnd').val();

    if (custDND == 'True' || custDND == true) {
        $('#btnDND').css({ 'display': 'block' });
    }

    //$('#dispositionHistory').addClass('nowrap');
    $('#tblDocument').addClass('nowrap');
    //$('#dispositionHistory').DataTable({
    //    responsive: true,
    //    "destroy": true,
    //    "scrollX": "true",
    //    "scrollY": "300",
    //    "paging": "false",
    //    "searching": false,
    //    columnDefs: [
    //        { targets: [-1, -3], className: 'dt-body-right' }
    //    ],

    //});

    $('#example700').DataTable({
        responsive: true,
        "destroy": true,
        //"scrollX": "true",
        //"scrollY": "300",
        "paging": "true",
        "searching": false,
        columnDefs: [
            { targets: [-1, -3], className: 'dt-body-right' }
        ],

    });

    $('#pullOutIns').DataTable({
        responsive: true,
        "destroy": true,
        //"scrollX": "true",
        //"scrollY": "300",
        "paging": "true",
        "searching": false,
        columnDefs: [
            { targets: [-1, -3], className: 'dt-body-right' }
        ],
        "order": []

    });

    $('.datepicker').datepicker({

        autoclose: true,
        dateFormat: 'yy-mm-dd',

        onSelect: function (date) {
            $('.range1datepicker').datepicker("destroy");
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

$('form').on('submit', function () {
    $('#mainLoader').css({ 'display': 'block' });
});

$(function () {
    $(".datepickerAfter").datepicker({
        autoclose: true,
        dateFormat: 'yy-mm-dd',
        minDate: new Date()
    });
});
$(function () {
    $(".datepickerPrevious").datepicker({
        autoclose: true,
        dateFormat: 'yy-mm-dd',
        maxDate: new Date(),
        onSelect: function (date) {
            $('.rangedatepicker').datepicker("destroy");
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

//Field Executive Scheduler Related Function

$('#dateis').on('change', function () {
    $('#startValueInsExist').val('0');
    $('#endValueInsExist').val('0');
    $('#driverValueIns').val('0');
});


$('#fieldLocation').on('change', function () {

    $('#startValueInsExist').val('0');
    $('#endValueInsExist').val('0');
    $('#driverValueIns').val('0');
});



//Loyality Card AjaxActionLink Support Functions Start --------------------------
function loyalityDone(res) {
    if (res.success == true) {
        $('#cardNo').html(res.cardNo);
        $('#loyalityPoint').html(res.loyalityPoint);
    }
    else {
        Lobibox.notify('warning', {
            msg: res.error
        });
    }
}
function adddeletecontact(value) {
    if (value == "add") {
        $('#addphoneno').show();
        $("#savePhoneCust").prop("value", "Save");
        $('#lblremarks').text("Reason for adding");
        $('#ButtonName').removeAttr('onclick');
        $('#savePhoneCust').attr('onClick', 'savecustomerDetails("SavePhone");');

    }
    else if (value == "delete") {
        $('#addphoneno').hide();
        $("#savePhoneCust").prop("value", "Delete");

        $('#lblremarks').text("Reason for Deleting");
        $('#ButtonName').removeAttr('onclick');
        $('#savePhoneCust').attr('onClick', 'savecustomerDetails("dltPhoneCust");');

    }
    $('#adddeletephonenumberDiv').show();

}
function loyalityFailure(error) {
    Lobibox.notify('warning', {
        msg: "Api responded with 500 server error"
    });
}
//Loyality Card AjaxActionLink Support Functions -------------------------- End *****************

//------------------------------- Customer Profile Edit PopUp ActionBeginForm Supporting Function ---------------- Start
function custSaveSuccess(res) {
    if (res.success == true) {
        $('#mainLoader').fadeOut('slow');
        //console.log(res);

        if (res.details[0].dnd == true) {
            $('#btnDND').css({ 'display': 'block' });
        }
        else {
            $('#btnDND').css({ 'display': 'none' });
        }
        $('#driverIdUpdate').val(res.details[0].driver);

        $('#addBtn').modal('hide');
        Lobibox.notify('info', {
            continueDelayOnInactiveTab: true,
            msg: 'Saved Successfully'
        });

        var prefer = res.prefer;
        var PrEmail = prefer["Email"];
        var PrPhone = prefer["Phone"];
        var isdueDate = prefer["dueDate"];
        if (PrEmail != "None") {
            $('#ddl_email').text(PrEmail);
            $('#email option[value="' + PrEmail + '"]').prop('selected', true);
        }

        if (PrPhone != "None") {
            $('#ddl_phone_no option:selected').removeAttr("selected");
            $('#preffered_contact_num option:selected').removeAttr("selected");

            $('#ddl_phone_no option[value="' + PrPhone + '"]').prop('selected', true);
            $('#preffered_contact_num option[value="' + PrPhone + '"]').prop('selected', true);
        }
        if (isdueDate != "None") {
            $('#policueditDuedate').val(isdueDate);
            $('#policyduedateINS').text(isdueDate);
        }

    }
    else {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'something went wrong...'
        });
        $('#addBtn').modal('hide');
    }

}

//Phone Number Check Function
$("#myPhoneNum").keyup(function () {
    $('#phErro').text('');
    if ($(this).val().length == 10) {
        if ($('#preffered_contact_num option').length > 0) {
            $("#preffered_contact_num option").each(function () {
                if ($(this).text() == $("#myPhoneNum").val()) {
                    $("#myPhoneNum").val('');
                    $('#phErro').text('Phone num already exists');
                }
            })

        }
    }
});

function multiSelectInit() {
    $('.multiselect-ui').multiselect({
        onChange: function (option, checked) {
            // Get selected options.
            var selectedOptions = $('.multiselect-ui option:selected');

            if (selectedOptions.length >= 3) {
                // Disable all other checkboxes.
                var nonSelectedOptions = $('.multiselect-ui option').filter(function () {
                    return !$(this).is(':selected');
                });

                nonSelectedOptions.each(function () {
                    var input = $('input[value="' + $(this).val() + '"]');
                    input.prop('disabled', true);
                    input.parent('li').addClass('disabled');
                });
            }
            else {
                // Enable all checkboxes.
                $('.multiselect-ui option').each(function () {
                    var input = $('input[value="' + $(this).val() + '"]');
                    input.prop('disabled', false);
                    input.parent('li').addClass('disabled');
                });
            }
        }
    });

}

setTimeout(multiSelectInit, 0);

function leadtaglimit() {
   
    data = $('#customer_lead_tag option:selected').toArray().map(item => item.value).join();
    $("#hdn_customer_lead_tag").val(data);
};


function onPopUPBegin() {
    //var data_field = $('#AddAddrMMSSaveMe').attr('data-value');
    //$('#AddAddrMMSSaveMe').val(data_field);
}

function onPopopSucces(res) {
    if (res.successPhone == true) {
        $('#myPhoneNum').val('');
        $('#myPhoneRemark').val('');
        $('#myModalAddPhone').modal("hide");

        $('#ddl_phone_no').empty();
        $("#preffered_contact_num").empty();
        var appenddata1 = "";
        var appenddata2 = "";
        var Phone = "";

        for (var i = 0; i < res.phnum.length; i++) {
            Phone = Phone + res.phnum[i].phoneNo + ",";
            appenddata1 += "<option value = '" + res.phnum[i].id + " '>" + res.phnum[i].phoneNo + " </option>";
            appenddata2 += "<option value = '" + res.phnum[i].id + " '>" + res.phnum[i].phoneNo + " </option>";

        }

        $("#preffered_contact_num").append(appenddata1);
        $("#ddl_phone_no").append(appenddata2);
        $("#prefPhone").text(Phone);


        Lobibox.notify('info', {
            msg: 'Phone Number Saved successfully!'
        });

    }
    else if (res.successEmail == true) {
        $('#myEmailNum').val('');
        $('#emailId').val('');

        $("#driver_modal4").modal("hide");
        $("#email").empty();
        var appenddata1 = "";
        var EMAILS = "";

        for (var i = 0; i < res.emailAddress.length; i++) {
            if (res.emailAddress[i].pref == true) {
                EMAILS = res.emailAddress[i].email;

            }
            appenddata1 += "<option value = '" + res.emailAddress[i].id + " '>" + res.emailAddress[i].email + " </option>";


        }
        $("#email").append(appenddata1);
        $("#ddl_email").text(EMAILS);
        Lobibox.notify('info', {
            msg: 'Email Saved successfully!'
        });
    }
    else if (res.successAddress == true) {




        $('#AddAddress1All').val('');
        $('#AddAddress2All').val('');
        $('#policyDropAddressId').val('');
        $('#PinCode1MMS').val('');

        console.log(res.address);

        var address = "";
        $("#AddNewAddressPopup").modal("hide");

        for (var i = 0; i < res.address.length; i++) {
            address = res.address[i].address1;
            $("#prefAdressUpdate").text(res.address[i].address1);
            $("#city").text(res.address[i].cty);
            $("#pin").text(res.address[i].pin);

        }
        $('#permanentAddress').val(address);
        $('#AddressMSSId option:selected').removeAttr("selected");
        $('#AddressMSSId').append(`<option value='${address}' selected>${address}</option>`);
        $('#smrDropAddress option:selected').removeAttr("selected");
        $('#smrDropAddress').append(`<option value='${address}' selected>${address}</option>`);
        $('#policyDropAddressId option:selected').removeAttr("selected");
        $('#policyDropAddressId').append(`<option value='${address}' selected>${address}</option>`);
        $('#prefAdressUpdate').text(address);
        getAddressForPopUp();


        Lobibox.notify('info', {
            msg: 'Address Saved successfully!'
        });

    }
    else if (res.successDelete == true) {
        $("#myModalUpdatePhone").modal("hide");
        Lobibox.notify('warning', {
            msg: 'Phone Number Deleted  successfully!'
        });

    }
    else {
        Lobibox.notify('warning', {
            msg: 'something went Wrong'
        });
    }
    $('#mainLoader').fadeOut('slow');
    $('#phErro').text('');
}

//On Success of each Tabs Call- Ajax
function onPopopFailure(res) {
    Lobibox.notify('warning', {
        continueDelayOnInactiveTab: true,
        msg: 'Something Went Wrong'
    });
    $('#mainLoader').fadeOut('slow');
    $('#phErro').text('');
}
//------------------------------- Customer Profile Edit PopUp ActionBeginForm Supporting Function ---------------- End


//------------------------------- Click to Call, GSM Call, SMS and Whatsapp Sending Supporting Function --------------------- Start
function doCall() {

    Lobibox.confirm({
        msg: "Do you want to make call",
        callback: function ($this, type) {
            if (type === 'yes') {
                var phNum = $('#ddl_phone_no option:selected').text();
                if (phNum === "-" || phNum === "") {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please select phone number'
                    });
                    return false;
                }
                
                $.ajax({
                    type: 'POST',
                    url: siteRoot + "/CallLogging/getUniqueID/",
                    datatype: 'json',
                    async: false,
                    cache: false,
                    data: {},
                    success: function (res) {
                        if (res.success == true) {

                            $.ajax({
                                type: 'POST',
                                url: siteRoot + "/CallLogging/initializeCall/",
                                datatype: 'json',
                                cache: false,
                                data: { phNum: phNum, uniqueid: res.unqId },
                                success: function (res) {
                                    if (res.success == true) {
                                        Lobibox.notify('info', {
                                            continueDelayOnInactiveTab: true,
                                            msg: 'Call made successfully'
                                        });
                                    }
                                    else {
                                        Lobibox.notify('warning', {
                                            continueDelayOnInactiveTab: true,
                                            msg: res.error
                                        });
                                    }
                                },
                                error: function (ex) {
                                    //alert(ex);
                                }
                            });
                        }
                        else {
                            alert('something went wrong');
                        }
                    },
                    error: function (ex) {
                        //alert(ex);
                    }
                });
            }
            else if (type === 'no') {
                return false;
            }
        }
    });
}

function doCalltatateleservices() {

    Lobibox.confirm({
        msg: "Do you want to make call",
        callback: function ($this, type) {
            if (type === 'yes') {
                var phNum = $('#ddl_phone_no option:selected').text();
                if (phNum === "-" || phNum === "") {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please select phone number'
                    });
                    return false;
                }
                $.ajax({
                    type: 'POST',
                    url: siteRoot + "/CallLogging/initializetatcloudPhone/",
                    datatype: 'json',
                    cache: false,
                    data: { phNum: phNum },
                    success: function (res) {
                        if (res.success == true) {
                            Lobibox.notify('info', {
                                continueDelayOnInactiveTab: true,
                                msg: 'Call made successfully'
                            });
                        }
                        else {
                            Lobibox.notify('warning', {
                                continueDelayOnInactiveTab: true,
                                msg: res.error
                            });
                        }
                    },
                    error: function (ex) {
                        //alert(ex);
                    }
                });

            }
            else if (type === 'no') {
                return false;
            }
        }
    });
}

function doCallknowlarity() {

    Lobibox.confirm({
        msg: "Do you want to make call",
        callback: function ($this, type) {
            if (type === 'yes') {
                var phNum = $('#ddl_phone_no option:selected').text();
                if (phNum === "-" || phNum === "") {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please select phone number'
                    });
                    return false;
                }
                $.ajax({
                    type: 'POST',
                    url: siteRoot + "/CallLogging/initializetatknowlarityPhone/",
                    datatype: 'json',
                    cache: false,
                    data: { phNum: phNum },
                    success: function (res) {
                        if (res.success == true) {
                            Lobibox.notify('info', {
                                continueDelayOnInactiveTab: true,
                                msg: res.Message
                            });
                        }
                        else {
                            Lobibox.notify('warning', {
                                continueDelayOnInactiveTab: true,
                                msg: res.Message
                            });
                        }
                        //console.log("Knowlarity Request", res.request);
                        //console.log("Knowlarity Response",res.response);

                    },
                    error: function (ex) {
                        //alert(ex);
                    }
                });

            }
            else if (type === 'no') {
                return false;
            }
        }
    });
}


function doGSM() {
   /* var dealerCode = $('#PkDealercode').val();*/
    Lobibox.confirm({
        msg: "Do you want to make call",
        callback: function ($this, type) {
            if (type === 'yes') {
                var phNum = $('#ddl_phone_no option:selected').text();
                if (phNum === "-" || phNum === "") {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please select phone number'
                    });
                    return false;
                }
                $.ajax({
                    type: 'POST',
                    url: siteRoot + "/CallLogging/initializeGSMCall/",
                    datatype: 'json',
                    cache: false,
                    data: { phNum: phNum },
                    success: function (res) {
                        if (res.success == true) {
                            Lobibox.notify('info', {
                                continueDelayOnInactiveTab: true,
                                msg: 'Call made successfully'
                            });
                        }
                        else {
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
            else if (type === 'no') {
                return false;
            }
        }
    });



}

function ABtMaruthidoGSM() {
    Lobibox.confirm({
        msg: "Do you want to make call",
        callback: function ($this, type) {
            if (type === 'yes') {
                var phNum = $('#ddl_phone_no option:selected').text();
                if (phNum === "-" || phNum === "") {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please select phone number'
                    });
                    return false;
                }
                 makeCall();
            }
            else if (type === 'no') {
                return false;
            }
        }
    });
}



function login() {
    console.log("login called");
    var extensionId = $('#extensionId').val();
    console.log("login extensionId: " + extensionId);
    var loggedin = false;
    console.log("login loggedin: " + loggedin);
    $.ajax({
        url: "https://abt.intalk.io/cc/api/v1/agent/login",
        type: "POST",
        data: '{"username":"' + extensionId + '","token":"CNn6iE2xTqB86hQRFfMmI4sRQZw7K32nQpt83vGXWi62zXTfcF"}',
        dataType: 'json',
        contentType: 'application/json',
        success: function (data, textStatus, jqXHR) {
            console.log("login data" + data);
            var obj = JSON.parse(JSON.stringify(data))
            console.log("login obj" + obj);
            console.log("obj.status" + obj.status);
            console.log("obj.data.user_token" + obj.data.user_token);
            if (obj.status == 1) {
                localStorage.setItem("usertoken", obj.data.user_token);
                loggedin = true;
                ctiAgentLogin(obj.data);
                setTimeout(function () {
                    makeCall();
                }, 3000);

            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log("login error" + errorThrown);
        }
    });
    console.log("return loggedin");
    return loggedin;
}

function makeCall() {
    console.log("makeCall called");
    //	ctiMakeCall($("#phonenumber").val(),"");
    var phNum = $('#ddl_phone_no option:selected').text();
    var token = localStorage.getItem("usertoken");
    if (token == "" || token == null) {
        login();
    }
    else {
        $("#hdnintalkDialNumber").val(phNum);

        $("#hdnintalkNCReason").val(true);
        var json = '{"token":"' + localStorage.getItem("usertoken") + '","phone_number": "' + phNum + '"}';
        $.ajax({
            url: "https://abt.intalk.io/cc/api/v1/make-outbound-call",
            type: "POST",
            data: json,
            dataType: 'json',
            contentType: 'application/json',
            success: function (data, textStatus, jqXHR) {

                var obj = JSON.parse(JSON.stringify(data))
                console.log("Call ID : " + obj.data.callid);
                $("#hdnintalkGSMUniqueId").val(obj.data.callid + "; intalk");
                $("#hdnintalkIsCallInitiated").val("initiated");
                Lobibox.notify('info', {
                    continueDelayOnInactiveTab: true,
                    msg: obj.message
                });
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(errorThrown);
            }
        });
    }

}

function logout(value) {

    if (localStorage.getItem("usertoken") != "") {
        var json = '{"token":"' + localStorage.getItem("usertoken") + '","accountStatus":"logout"}';
        $.ajax({
            url: "https://abt.intalk.io/cc/api/v1/status/edit",
            type: "POST",
            data: json,
            dataType: 'json',
            contentType: 'application/json',
            success: function (data, textStatus, jqXHR) {
                var obj = JSON.parse(JSON.stringify(data))
                console.log(obj.message);
                localStorage.setItem("usertoken", "")
                if (value == "logout") {
                    window.localStorage.clear();
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(errorThrown);
            }
        });
    } else {
        $("#message").html($("#message").html() + '<br>' + "Already Logged out");
    }
}


function smsTypeChanged(ele, msgType) {
    //var phNum = $('#ddl_phone_no option:selected').val();

    if (msgType == "sms") {
        var selectedIdSMSType = $('#ddl_SMSType option:selected').val();
        var selectedLocId = $('#ddl_workshop option:selected').val();


        if (selectedIdSMSType == "") {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please select SMS Type'
            });
            $('#smstemplate').val('');
            return false;
        }

        if (selectedLocId == "") {
            selectedLocId = 0;
        }
        getTemplate(selectedIdSMSType, selectedLocId, 'sms');
    }
    else if (msgType == "email") {
        var selectedIdSMSType = $('#ddl_Email_Type option:selected').val();
        var selectedLocId = $('#ddl_Email_Workshop option:selected').val();


        if (selectedIdSMSType == "") {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please select Email Type'
            });
            $('#smstemplate').val('');
            return false;
        }

        if (selectedLocId == "") {
            selectedLocId = 0;
        }
        getTemplate(selectedIdSMSType, selectedLocId, 'email');
    }
}

function locationChnaged(ele, msgType) {
    //var phNum = $('#ddl_phone_no option:selected').val();

    if (msgType == "sms") {
        var selectedLocId = $('#' + ele + ' option:selected').val();
        var selectedIdSMSType = $('#ddl_SMSType option:selected').val();
        if (selectedIdSMSType == "") {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please select SMS Type'
            });
            $('#smstemplate').val('');
            return false;
        }

        if (selectedLocId == "") {
            selectedLocId = 0;
        }
        getTemplate(selectedIdSMSType, selectedLocId, 'sms');
    }
    else if (msgType == "email") {
        var selectedIdSMSType = $('#ddl_Email_Type option:selected').val();
        var selectedLocId = $('#ddl_Email_Workshop option:selected').val();
        if (selectedIdSMSType == "") {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please select Email Type'
            });
            $('#smstemplate').val('');
            return false;
        }

        if (selectedLocId == "") {
            selectedLocId = 0;
        }
        getTemplate(selectedIdSMSType, selectedLocId, 'email');
    }
}
function getSMSTemplateINS(selectedIdSMSType, selectedLocId, msgType, phNum) {
    $('#smstemplate').empty();

    if (selectedLocId == undefined) {
        selectedLocId = 0;
    }
        
    $('#mainLoader').fadeIn('slow');
    $.ajax({
        type: 'POST',
        url: siteRoot + "/CallLogging/getTemplateMessage/",
        datatype: 'json',
        //async: false,
        cache: false,
        data: { smsId: selectedIdSMSType, locId: selectedLocId, msgType: msgType },
        success: function (res) {
            if (res.success == true) {

                if (msgType == "sms") {
                    $('#smstemplate').val(res.sms);
                    var smstemplate = $('#smstemplate').val();
                    if ($('#smstemplate').val() == "") {
                        Lobibox.notify('warning', {
                            continueDelayOnInactiveTab: true,
                            msg: 'Make sure SMS Template is selected'
                        });
                    }
                    else {
                        $.ajax({
                            type: 'POST',
                            url: siteRoot + "/CallLogging/sendSMS/",
                            datatype: 'json',
                            cache: false,
                            data: { phNum: phNum, smstemplate: res.sms },
                            success: function (res) {
                                if (res.success == true) {
                                    Lobibox.notify('info', {
                                        continueDelayOnInactiveTab: true,
                                        msg: 'Response Recorded Successfully'
                                    });
                                    $('#smstemplate').val('');
                                }

                                else {
                                    Lobibox.notify('info', {
                                        continueDelayOnInactiveTab: true,
                                        msg: res.error
                                    });
                                    $('#smstemplate').val('');
                                }
                            },
                            error: function (ex) {
                                alert(ex);
                            }
                        });
                    }
                }
                else if (msgType == "email") {
                    $('#emailSubject').val(res.emailSub);
                    $('#emailtemplate').val(res.emailTemplate);
                }
            }
            else {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: res.error
                });
            }
            $('#mainLoader').fadeOut('slow');
        },
        error: function (ex) {
            alert(ex);
            $('#mainLoader').fadeOut('slow');
        }
    });
}
function getTemplate(selectedIdSMSType, selectedLocId, msgType) {
    $('#smstemplate').empty();

    if (selectedLocId == undefined) {
        selectedLocId = 0;
    }

    $('#mainLoader').fadeIn('slow');
    $.ajax({
        type: 'POST',
        url: siteRoot + "/CallLogging/getTemplateMessage/",
        datatype: 'json',
        //async: false,
        cache: false,
        data: { smsId: selectedIdSMSType, locId: selectedLocId, msgType: msgType },
        success: function (res) {
            if (res.success == true) {

                if (msgType == "sms") {
                    $('#smstemplate').val(res.sms);
                }
                else if (msgType == "email") {
                    $('#emailSubject').val(res.emailSub);
                    $('#emailtemplate').val(res.emailTemplate);
                }
            }
            else {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: res.error
                });
            }
            $('#mainLoader').fadeOut('slow');
        },
        error: function (ex) {
            alert(ex);
            $('#mainLoader').fadeOut('slow');
        }
    });
}

function sendSMS() {
    var phNum = $('#ddl_phone_no option:selected').text();

    var smsType = $('#ddl_SMSType option:selected').val();
    var workshopselected = $('#ddl_workshop option:selected').val();
    var smstemplate = $('#smstemplate').val();

    smstemplate = smstemplate.replace(/[\\\#()$~'"*<>{}]/g, ' ');
    if ($('#PkDealercode').val() == "INDUS") {
        if (smsType == "29" || smsType == "30") {
            if (workshopselected == "" || workshopselected == null) {
                workshopselected = "0";
            }
        }

    }

    if ($('#smstemplate').val() !== "" && smsType !== "" && workshopselected !== "" && phNum !== "") {

        try {
            $.ajax({
                type: 'POST',
                url: siteRoot + "/CallLogging/sendSMS/",
                datatype: 'json',
                cache: false,
                data: { phNum: phNum, smstemplate: smstemplate },
                success: function (res) {
                    if (res.success == true) {
                        Lobibox.notify('info', {
                            continueDelayOnInactiveTab: true,
                            msg: 'Response Recorded Successfully'
                        });
                    }
                    else {
                        Lobibox.notify('Warning', {
                            continueDelayOnInactiveTab: true,
                            msg: res.error
                        });
                        //alert(res.error);
                    }
                },
                error: function (ex) {
                    alert("Sesrervver Error");
                }
            });
        }
        catch (err) {
            alert(err);
        }


    }
    else {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Make sure phone no, SMS Type and Workshop is selected'
        });
    }

}

function sendSMSInsuranceQuotation() {
    if (phNum == "") {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Make sure phone no is selected'
        });
    } else {
        var phNum = $('#ddl_phone_no option:selected').text();
        getSMSTemplateINS(21, location_cityId, 'sms', phNum);
    }
   }

//quotation template
function getINSquotation(selectedIdSMSType, selectedLocId, msgType) {
    $('#smstemplate').empty();
    if (selectedLocId == undefined) {
        selectedLocId = 0;
    }

    $('#mainLoader').fadeIn('slow');

    return $.ajax({
        "url": siteRoot + "/CallLogging/getTemplateMessage/",
        "type": "POST",
        "data": { smsId: selectedIdSMSType, locId: selectedLocId, msgType: msgType },
        "datatype": "json"
    });
}

function OpenWhatsApp(ele) {
    //debugger;
    var phNum = "91" + $('#ddl_phone_no option:selected').text();
    //var msg;
    //if (Session["DealerCode"].ToString() == "BRIDGEWAYMOTORS") {
     //   msg = $('#ORAIsmstemplate').val();
   // }
   // else {
      //  msg = $('#smstemplate').val()
    //}


    var dealerCode = $('#PkDealercode').val();

    if (dealerCode == "BRIDGEWAYMOTORS") {
        var msg = $('#ORAIsmstemplate').val();
        if (msg == "") {
            var msg = $('#ORAIsmstemplate').val();
        }
    }
    else {
        var msg = $('#smstemplate').val();
        if (msg == "") {
            var msg = $('#smstemplate').val();
        }
    }

    var docName = $(ele).attr("data-file");
    var href1 = "";
    var doclink = $(ele).attr("data-dwnload");

    if (docName != undefined && docName !== "INS QUOTATION") {
        href1 = "https://api.whatsapp.com/send?phone=" + phNum + "&text=Please Refer Attached File Name: " + docName + " " + encodeURI(doclink)
        $('#whatsAppBtn').attr("data-from", "doc");
        $('#whatsAppBtn').attr('data-file', "Refer attached file:" + docName);
        window.open(href1, '_blank');
        $('#whatsAppModal').modal({
            backdrop: 'static',
            keyboard: false
        });
        $('#smsPopuId').modal('hide');
    }
    else if (docName == "INS QUOTATION") {
        getINSquotation(21, location_cityId, 'sms').done(function (res) {
            if (res.success == true) {

                $('#smstemplate').val(res.sms);
                msg = $('#smstemplate').val();
                //$('#smstemplate').val('');
                href1 = "https://api.whatsapp.com/send?phone=" + phNum + "&text=" + msg;
                window.open(href1, '_blank');
                $('#whatsAppModal').modal({
                    backdrop: 'static',
                    keyboard: false
                });
                $('#smsPopuId').modal('hide');
            }
            else {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: res.error,

                });
            }
            $('#mainLoader').fadeOut('slow');
       
        })
        //getTemplate(21, location_cityId, 'sms');
      //  msg = $('#smstemplate').val();
      ////$('#smstemplate').val('');
      //  href1 = "https://api.whatsapp.com/send?phone=" + phNum + "&text=" + msg;
    }
    else {
        href1 = "https://api.whatsapp.com/send?phone=" + phNum + "&text=" + msg;
        window.open(href1, '_blank');
        $('#whatsAppModal').modal({
            backdrop: 'static',
            keyboard: false
        });
        $('#smsPopuId').modal('hide');

    }
    //$(this).attr('href', href1);
    //$(this).trigger('click');
    //window.open(href1, '_blank');
    //$('#whatsAppModal').modal({
    //    backdrop: 'static',
    //    keyboard: false
    //});
    //$('#smsPopuId').modal('hide');
}

function putWhatsappResponse(ele) {
    //debugger;
    var dealerCode = $('#PkDealercode').val();
    var value = $("#whatsAppStatus option:selected").val();
    var phNum = "91" + $('#ddl_phone_no option:selected').text();
    var msg = "";
    var responseFrom = "";
    var datafrom = $(ele).attr('data-from');
    if (datafrom == "doc") {
        msg = $(ele).attr('data-file');
        responseFrom = "doc";
    }
    else {
        if (dealerCode == "BRIDGEWAYMOTORS") {
            msg = $('#ORAIsmstemplate').val();
            msg = msg.replace(/[&\/\\#,+()$~%.'":*?<>{}]/g, '');

            responseFrom = "message";
        }
        else {
            msg = $('#smstemplate').val();
            msg = msg.replace(/[&\/\\#,+()$~%.'":*?<>{}]/g, '');

            responseFrom = "message";
        }

    }
    try {
        if (value != "2") {
            $.ajax({
                type: 'POST',
                url: siteRoot + '/CallLogging/sendWhatsappMsg/',
                datatype: 'json',
                cache: false,
                data: { phNum: phNum, msg: msg, reason: value, responseFrom: responseFrom },
                success: function (res) {
                    if (res.success == true) {
                        Lobibox.notify('info', {
                            continueDelayOnInactiveTab: true,
                            msg: 'Response Recorded Successfully'
                        });
                        $('#smstemplate').val('');
                    }
                    else {
                        Lobibox.notify('warning', {
                            continueDelayOnInactiveTab: true,
                            msg: res.error
                        });
                        //alert(res.error);
                    }
                },
                error: function (ex) {
                    alert("Server Error");
                }
            });
        }
        else {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please select sent status.'
            });
            $('#whatsAppModal').modal('show');
            $('#whatsAppModal').css({ 'display': 'block' });

        }
    }
    catch (err) {
        alert(err);
    }
}

//------------------------------- Click to Call, GSM Call, SMS and Whatsapp Sending Supporting Function --------------------- End


//-------------------------------- Document Upload and Document Handling Related Support Function --------------------------- Start
function NumOfFile(ele) {
    if (ele.files.length > 3) {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'you cant upload files more than 3'
        });
        $(ele).val("");
    }
}

function getDocuments() {
    $.ajax({
        type: 'POST',
        url: siteRoot + "/CallLogging/getDocuments/",
        datatype: 'json',
        cache: false,
        data: {},
        success: function (res) {
            // if (res.success == true) {
            if (res.success == true && res.files != "") {
                if (res.files.includes('%')) {
                    var files = res.files.split('%');
                    var docName = res.fileName.split(',');
                    var deptname = res.deptName.split(',');
                    var uploadedDatetime = res.uploadedDateTime.split(',');
                    var docFileName = res.docFileName.split('%');
                    var docIds = res.docId.split(',');
                    var creNames = res.usernames.split(',');

                    $('#tblDocument tbody tr').remove();
                    if ($.fn.DataTable.isDataTable("#tblDocument")) {
                        var table = $("#tblDocument").DataTable();
                        table.clear();
                        table.destroy();
                    }
                    for (var i = 0; i < files.length; i++) {
                        bindFiles(files[i], deptname[i], docName[i], uploadedDatetime[i], creNames[i], docIds[i], docFileName[i]);
                    }
                }
                else {
                    $('#tblDocument tbody tr').remove();
                    if ($.fn.DataTable.isDataTable("#tblDocument")) {
                        var table = $("#tblDocument").DataTable();
                        table.clear();
                        table.destroy();
                    }
                    bindFiles(res.files, res.deptName, res.fileName, res.uploadedDateTime, res.usernames.split(',')[0], res.docId, res.docFileName);
                }
                $("#tblDocument").dataTable({
                    //destroy: true
                    responsive: true,
                    columnDefs: [
                        { targets: [-7, -3], className: 'dt-body-right' }
                    ]
                });

            }
            else {
                if (res.success == false) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: res.error
                    });

                }

            }
            //}
            //else {
            //    alert('something went wrong');
            //}
        },
        error: function (ex) {
            alert(ex);
        }
    });
}

function bindFiles(files, deptname, docName, Datetime, creNames, docId, docFileName) {
    //debugger;
    //alert("File Uploaded successfully");
    var DataTable = $('#tblDocument tbody');
    //$(DataTable).empty();

    var DataRow = "";
    var codePath = window.location.href;
    var callIndex = codePath.indexOf('CallLogging');
    var callPath = codePath.substr(callIndex, (codePath.length - 1));
    codePath = codePath.replace(callPath, '');
    var docUploadedName = docFileName
    var allPaths = "";

    var file1 = "", file2 = "", file3 = "", file1Name = "", file2Name = "", file3Name = "";
    if (files.includes(';')) {
        var fileBase = files.split('#')[1];
        files = files.split('#')[0];

        if (files.split(';')[0] != null || files.split(';')[0] != undefined) {
            file1 = '<a href="' + codePath + fileBase + files.split(';')[0] + '" download><i class="fa fa-download">' + docUploadedName.split(';')[0] + '</a>';
            file1Name = docUploadedName.split(';')[0];
            allPaths = (codePath + fileBase + files.split(';')[0]).replace(/ /g, "%20");

        }

        if (files.split(';')[1] != null || files.split(';')[1] != undefined) {
            file2 = '<a href="' + codePath + fileBase + files.split(';')[1] + '" download><i class="fa fa-download">' + docUploadedName.split(';')[1] + '</a>';
            file2Name = docUploadedName.split(';')[1];
            allPaths = " , " + (codePath + fileBase + files.split(';')[1]).replace(/ /g, "%20");

        }

        if (files.split(';')[2] != null || files.split(';')[2] != undefined) {
            file2 = '<a href="' + codePath + fileBase + files.split(';')[2] + '" download><i class="fa fa-download">' + docUploadedName.split(';')[2] + '</a>';
            file3Name = docUploadedName.split(';')[2];
            allPaths = " , " + (codePath + fileBase + files.split(';')[2]).replace(/ /g, "%20");

        }
    }
    else {
        var fileBase = files.split('#')[1];
        files = files.split('#')[0];

        file1 = '<a href="' + codePath + fileBase + files + '" download><i class="fa fa-download">' + docFileName + '</i></a>';
        file1Name = files;
        allPaths = (codePath + fileBase + files.split(';')[0]).replace(/ /g, "%20");

    }

    var deptName = deptname;
    var documentName = docName;
    $('#deptName').val('');
    $('#documentName').val('');
    $('#fileList').val();
    if (creNames == null && creNames == undefined) {
        creNames = UserName;
    }

    //for (var i = 0; i<res.doc.length; i++) {
    var row = `<tr>
                                <td>${deptName}</td>
                                <td>${creNames}</td>
                                <td>${Datetime}</td>
                                <td>${documentName}</td>
                                <td>${file1}${file2}${file3}</td>
                                <td><button type="button" id="whatsappDoc" onclick="OpenWhatsApp(this)" data-file="${documentName}" data-dwnload="${allPaths}" class="btn btn-default whatsapp"><img src="${codePath}/public/images/whatsapp_logo.png" width='30px'/></button></td>
                                <td><button type="button" id="btnEmail" data-docId="${docId}" data-deptname="${deptName}" data-docName="${documentName}" data-EmailFor="doc" class="btn btn-default" onclick="openEmailpopUp(this)"><img src="${codePath}/public/images/email_logo.jpg" width='30px'/></button></td>
                            </tr>`;
    DataRow = DataRow + row;
    //}
    $(DataTable).append(DataRow);
}
//file upload success function
function fileSuccess(res) {
    //debugger;
    if (res.success == true) {
        $('#mainLoader').fadeOut('slow');
        getDocuments();

    }
    else {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: res.error
        });

        //  alert("Could not upload the file.. try after sometime?");
        $('#mainLoader').fadeOut('slow');
    }
}

function fileFailure(error) {
    alert("Internal Server error couldn't upload file.");
    $('#mainLoader').fadeOut('slow');

}

//-------------------------------- Document Upload and Document Handling Related Support Function --------------------------- End


//------------------------------------- Interaction History Supporting Functions ---------------------------------------- Start
function callHistoryByVehicle(typeIs) {
    $('#hdndispohisttype').val(typeIs);
    var codePath = window.location.href;
    var callIndex = codePath.indexOf('CallLogging');
    var callPath = codePath.substr(callIndex, (codePath.length - 1))
    codePath = codePath.replace(callPath, '');

    var dataTableCallDispo = $('#dispositionHistory').DataTable({
        "destroy": true,
        "ajax": {
            "url": siteRoot + "/CallLogging/getCallHistory/",
            "type": "POST",
            "datatype": "json",
            "data": { typeIs: typeIs },
        },
        "columns": [
            { "data": "AssignId", "name": "AssignId" },
            { "data": "jobCardNumber", "name": "jobCardNumber" },

            { "data": "CallDate", "name": "CallDate" },
            { "data": "Time", "name": "Time" },
            { "data": "CreId", "name": "CreId" },

            { "data": "Campaign", "name": "Campaign" },
            { "data": "ServiveType", "name": "ServiveType" },
            { "data": "SecondaryDispo", "name": "SecondaryDispo" },
            { "data": "Details", "name": "Details" },
            { "data": "CreRemarks", "name": "CreRemarks" },
            { "data": "Feedback", "name": "Feedback" },
            { "data": "CallMadeType", "name": "CallMadeType" },
            {
                "data": "IsCallInitiated", render: function (data, type, row) {
                    if (data == "") {
                        return "Not Initiated";
                    }
                    else {
                        return data;
                    }
                }
            },
            { "data": "gsmAndroid", "name": "gsmAndroid" },
            { "data": "DailedNo", "name": "DailedNo" },
            {
                "data": "FilePath", render: function (data, type, row) {
                    var filePath = "";
                    if (data != null && data != undefined && data.includes('http:')) {
                        var startIndex = data.indexOf('http');
                        filePath = data.substring(startIndex, data.length);
                    }
                    else if (data != null && data != undefined && data.includes('https:')) {
                        var startIndex = data.indexOf('https');
                        filePath = data.substring(startIndex, data.length);
                    }
                    else {
                        filePath = siteRoot + "/" + data;
                    }

                    return `<audio controls="" src="${filePath}"></audio><br/>`;
                }
            },
            {
                "data": "FilePath", render: function (data, type, row) {
                    var filePath = "";
                    if (data != null && data != undefined && data.includes('http:')) {
                        var startIndex = data.indexOf('http');
                        filePath = data.substring(startIndex, data.length);
                    }
                    else if (data != null && data != undefined && data.includes('https:')) {
                        var startIndex = data.indexOf('https');
                        filePath = data.substring(startIndex, data.length);
                    }
                    else {
                        filePath = siteRoot + "/" + data;
                    }

                    return `<a href="${filePath}" download><i class="fa fa-download" data-toggle="tooltip"></i></a>`;
                }
            }
            //{
            //    "data": "Action", render: function (data, type, row) {

            //        if (typeIs == "SMR") {
            //            return `<button text="viewmore" class='btn btn-success' data-toggle='modal' data-target='#smrviewmoreModal' onclick="callHistoryByVehicle_viewmore('SMR')">viewmore</button>`
            //        }
            //        else if (typeIs == "INS") {
            //            return `<button text="viewmore" class='btn btn-success' data-toggle='modal' data-target='#smrviewmoreModal' onclick="callHistoryByVehicle_viewmore('INS')">viewmore</button>`

            //        }
            //        else if (typeIs == "PSF") {
            //            return `<button text="viewmore" class='btn btn-success' data-toggle='modal' data-target='#smrviewmoreModal' onclick="callHistoryByVehicle_viewmore('PSF')">viewmore</button>`

            //        }
            //        else if (typeIs == "PostSalesFeedback") {
            //            return `<button text="viewmore" class='btn btn-success' data-toggle='modal' data-target='#smrviewmoreModal' onclick="callHistoryByVehicle_viewmore('PostSalesFeedback')">viewmore</button>`

            //        }
            //        else {
            //            return `<button text="viewmore" class='btn btn-success' data-toggle='modal' data-target='#smrviewmoreModal' onclick="callHistoryByVehicle_viewmore(this.view)">viewmore</button>`

            //        }
            //    }
            //}

        ],
        responsive: true,
        "initComplete": function (data) {
            $('#tblException').text(data.json.exception);
            // filtercountDisplay();
            if (typeIs == "PSF") {
                dataTableCallDispo.columns(1).visible(true)
            }
            else {
                dataTableCallDispo.columns(1).visible(false)

            }
            if (pkDealer == "KATARIA")
            {
                dataTableCallDispo.columns(16).visible(false);
            }
            else
            {
                dataTableCallDispo.columns(16).visible(true);
            }
            if (typeIs == 'PSF' || typeIs == 'INS') {
                dataTableCallDispo.columns(6).visible(false)
            }
            $('.dataTables_length').hide();
            $('.dataTables_filter').hide();

            //$('.dataTable').wrap('<div class="dataTables_scroll" />');
        },
        "serverSide": "true",
        "processing": "true",
        "serverSide": "true",
        "sorting": "false",
        //"scrollX": "true",
        //"scrollY": "300",
        "paging": "true",
        //"searching": "true",
        "language": {
            "processing": "Loading Please Wait.....!"
        },
        order: [],
        pageLength: 5
    });
}


function smsHistoryOfUser(typeIs) {

    var dataTableCallDispo = $('#sms_history').DataTable({
        "destroy": true,
        "ajax": {
            url: siteRoot + "/CallLogging/getSMSHistoryOfCustomer/",
            "type": "POST",
            "datatype": "json",
            data: {},
        },
        "columns": [

            { "data": "interactionDate", "name": "interactionDate" },
            { "data": "interactionTime", "name": "interactionTime" },
            { "data": "WyzuserName", "name": "WyzuserName" },
            { "data": "reason", "name": "reason" },
            { "data": "mobileNumber", "name": "mobileNumber" },
            { "data": "smsType", "name": "smsType" },
            { "data": "smsMessage", "name": "smsMessage" },
            { "data": "smsStatus", "name": "smsStatus" }
        ],
        columnDefs: [{
            targets: "_all",
            orderable: false
        }],
        responsive: true,
        "initComplete": function (data) {
            $('#tblsmsHstry').text(data.json.exception);
        },
        "processing": "true",
        "bFilter": false,
        "bLengthChange": false,
        "sorting": "false",
        "paging": "true",
        "language": {
            "processing": "Loading Please Wait.....!"
        },
        order: [],
        pageLength: 5
    });
}

function complaintsOFVehicle() {

    var dataTableCallDispo = $('#complaint_history').DataTable({
        "destroy": true,
        "ajax": {
            url: siteRoot + "/CallLogging/getComplaintHistoryVeh/",
            "type": "POST",
            "datatype": "json",
            data: {},
        },
        "columns": [
            { "data": "complaintNumber", "name": "complaintNumber" },
            {
                "data": "saleDate", render: function (data, type, row) {
                    return parseJsonDateTime(data)
                }
            },
            { "data": "sourceName", "name": "sourceName" },
            { "data": "functionName", "name": "functionName" },
            { "data": "subcomplaintType", "name": "subcomplaintType" },
            { "data": "Description", "name": "Description" }
        ],
        columnDefs: [{
            targets: "_all",
            orderable: false
        }],
        responsive: true,
        "initComplete": function (data) {
            $('#tblcomplaintHstry').text(data.json.exception);
        },
        "processing": "true",
        "bFilter": false,
        "bLengthChange": false,
        "sorting": "false",
        "paging": "true",
        "language": {
            "processing": "Loading Please Wait.....!"
        },
        order: [],
        pageLength: 5
    });
}

function emailHistoryOfVehicle(typeIs) {



    var dataTableCallDispo = $('#email_history').DataTable({
        "destroy": true,
        "ajax": {
            url: siteRoot + "/CallLogging/getEmailHistoryOfCustomerId/",
            "type": "POST",
            "datatype": "json",
            data: {},
        },
        "columns": [
            { "data": "interactionDate", "name": "interactionDate" },
            { "data": "interactionTime", "name": "interactionTime" },
            { "data": "WyzuserName", "name": "WyzuserName" },
            { "data": "fromEmailAddress", "name": "fromEmailAddress" },
            { "data": "toEmailAddress", "name": "toEmailAddress" },
            { "data": "ccEmailAddress", "name": "ccEmailAddress" },
            { "data": "smsType", "name": "smsType" },
            { "data": "emailsubject", "name": "emailsubject" },
            { "data": "smsMessage", "name": "smsMessage" },
            { "data": "reason", "name": "reason" }
        ],
        columnDefs: [{
            targets: "_all",
            orderable: false
        }],
        responsive: true,
        "initComplete": function (data) {
            $('#tblemailHstry').text(data.json.exception);
        },
        "processing": "true",
        "bFilter": false,
        "bLengthChange": false,
        "sorting": "false",
        "paging": "true",
        "language": {
            "processing": "Loading Please Wait.....!"
        },
        order: [],
        pageLength: 5
    });


}

function insuranceHistoryOfCustomer() {
    var codePath = window.location.href;
    var callIndex = codePath.indexOf('CallLogging');
    var callPath = codePath.substr(callIndex, (codePath.length - 1));
    codePath = codePath.replace(callPath, '');

    var dataTableINSQuotation = $('#insurance_history').DataTable({
        "destroy": true,
        "ajax": {
            url: siteRoot + "/CallLogging/insuranceHistoryOfCustomerId/",
            "type": "POST",
            "datatype": "json",
            data: {},
        },
        "columns": [
            {
                "data": "Action", render: function (data, type, row) {
                    return `<button type='button' id='smsSendbtn' class='btn btn-default smsSendType' data-dismiss='modal' onclick='sendSMSInsuranceQuotation();'><img src= '${codePath}/public/images/sms_logo.jpg' style = 'width: 20px;' >&nbsp; Text</button >`
                }
            },
            {
                "data": "Action", render: function (data, type, row) {
                    return `<button type'button' id='whatsapp' class='btn btn-default whatsapp' onclick='OpenWhatsApp(this)' data-file='INS QUOTATION' data-dismiss='modal'><img src='${codePath}/public/images/whatsapp_logo.png' style='width: 20px;'>&nbsp;Message</button>`
                }
            },
            {
                "data": "createdDate", render: function (data, type, row) {
                    return parseJsonDateTime(data)
                }
            },
            { "data": "createdCRE", "name": "createdCRE" },
            { "data": "insuranceCompanyQuotated", "name": "insuranceCompanyQuotated" },
            { "data": "totalPremiumWithTax", "name": "totalPremiumWithTax" },
            { "data": "status", "name": "status" },
            { "data": "response_transaction_id", "name": "response_transaction_id" },
            { "data": "idv", "name": "idv" },
            { "data": "odPercentage", "name": "odPercentage" },
            { "data": "ncbPercentage", "name": "ncbPercentage" },
            { "data": "ncbRupees", "name": "ncbRupees" },
            { "data": "discountPercentage", "name": "discountPercentage" },
            { "data": "discountValue", "name": "discountValue" },
            { "data": "oDPremium", "name": "oDPremium" },
            { "data": "addOn", "name": "addOn" },
            { "data": "liabilityPremium", "name": "liabilityPremium" },
            { "data": "otherAmt", "name": "otherAmt" },
            { "data": "totalPremiumWithOutTax", "name": "totalPremiumWithOutTax" },
            { "data": "commentIQ", "name": "commentIQ" }
        ],
        columnDefs: [{
            targets: "_all",
            orderable: true 
        }],
        responsive: true,
        "initComplete": function (data) {
            $('#tblINSQutnhstryException').text(data.json.exception);
        },
        "processing": "true",
        "bFilter": false,
        "bLengthChange": false,
        "sorting": "false",
        "paging": "true",
        "language": {
            "processing": "Loading Please Wait.....!"
        },
        order: [2, "desc"],
        pageLength: 5
    });
}

function AssignmentHistoryOfCustomer(typeIs) {

    var dataTableAssgnmntHstry = $('#assignment_smr_history').DataTable({
        "destroy": true,
        "ajax": {
            url: siteRoot + "/CallLogging/getAssignmentHistoryOfVehicleId/",
            "type": "POST",
            "datatype": "json",
            data: { moduleType: typeIs },
        },
        "columns": [
            { "data": "assignedId", "name": "assignedId" },
            {
                "data": "assignDate", render: function (data, type, row) {
                    return parseJsonDateTime(data)
                }
            },
            { "data": "reason", "name": "reason" },
            { "data": "WyzuserName", "name": "WyzuserName" },
            { "data": "smsType", "name": "smsType" },
            { "data": "serviceTypes", "name": "serviceTypes" },
            { "data": "smsMessage", "name": "smsMessage" },
            { "data": "reassign", "name": "reassign" },
            { "data": "reassignhistory", "name": "reassignhistory" },
            { "data": "resonforDrop", "name": "resonforDrop" },
            { "data": "dateofdrop", "name": "dateofdrop" },
            { "data": "campaign", "name": "campaign" }
            //{
            //    "data": "Action", "name": "Coupon Action", render: function (data, type, row) {

            //        if (typeIs == "insurance") {
            //            return `<button text="viewmore" class='btn btn-success' data-toggle='modal' data-target='#insassignviewmoreModal' onclick="AssignmentHistoryOfCustomer_viewmore('insurance')">viewmore</button>`
            //        }
            //        else if (typeIs == "service") {
            //            return `<button text="viewmore" class='btn btn-success' data-toggle='modal' data-target='#insassignviewmoreModal' onclick="AssignmentHistoryOfCustomer_viewmore('service')">viewmore</button>`

            //        }
            //        else if (typeIs == "psf") {
            //            return `<button text="viewmore" class='btn btn-success' data-toggle='modal' data-target='#insassignviewmoreModal' onclick="AssignmentHistoryOfCustomer_viewmore('psf')">viewmore</button>`

            //        }
            //        else {
            //            return `<button text="viewmore" class='btn btn-success' data-toggle='modal' data-target='#insassignviewmoreModal' onclick="AssignmentHistoryOfCustomer_viewmore(this.value)">viewmore</button>`

            //        }
            //    }
            //},
        ],
        columnDefs: [{
            targets: "_all",
            orderable: false
        }],
        responsive: true,
        "initComplete": function (data) {
            $('#tbsmrhstryException').text(data.json.exception);

            if (typeIs == 'insurance') {
                dataTableAssgnmntHstry.columns(5).visible(false)
            }
        },
        "processing": "true",
        "bFilter": false,
        "bLengthChange": false,
        "sorting": "false",
        "paging": "true",
        "language": {
            "processing": "Loading Please Wait.....!"
        },
        order: [],
        pageLength: 5
    });
}


function getFEInteractionData() {
    var dataTableCallDispo = $('#tbl_fe_interactions').DataTable({
        "destroy": true,
        "ajax": {
            "url": siteRoot + "/CallLogging/getFEInteractionData/",
            "type": "POST",
            "datatype": "json",
            "data": {},
        },
        "columns": [
            { "data": "InteractionId", "name": "InteractionId" },
            { "data": "creName", "name": "creName" },
            { "data": "AgentName", "name": "AgentName" },
            { "data": "actionDate", "name": "actionDate" },
            { "data": "appointmentType", "name": "appointmentType" },
            { "data": "status", "name": "status" },
            { "data": "Disposition", "name": "Disposition" },
            { "data": "Details", "name": "Details" },
            { "data": "comments", "name": "comments" },
        ],
        responsive: true,
        "initComplete": function (data) {
            $('#tblExceptionFE').text(data.json.exception);
            // filtercountDisplay();

            //$('.dataTable').wrap('<div class="dataTables_scroll" />');
        },
        "serverSide": "true",
        "processing": "true",
        "serverSide": "true",
        "sorting": "false",
        //"scrollX": "true",
        //"scrollY": "300",
        "paging": "true",
        //"searching": "true",
        "language": {
            "processing": "Loading Please Wait.....!"
        },
        order: [],
        pageLength: 5
    });
}
function getDriverAppInteraction() {
    var codePath = window.location.href;
    var callIndex = codePath.indexOf('CallLogging');
    var callPath = codePath.substr(callIndex, (codePath.length - 1));
    codePath = codePath.replace(callPath, '');

    var dataTableCallDispo = $('#tblDriverAppInter').DataTable({
        "destroy": true,
        "ajax": {
            "url": siteRoot + "/CallLogging/getDriverAppInteraction/",
            "type": "POST",
            "datatype": "json",
            "data": {},
        },
        "columns": [
            {
                "data": "Id", render: function (data, type, row) {
                    return "DRV-" + data;
                }
            },
            { "data": "CREName", "name": "CREName" },
            { "data": "DriverName", "name": "DriverName" },
            {
                "data": "LastUpdatedOn", render: function (data, type, row) {
                    return parseJsonDateTime(data);
                }
            },
            {
                "data": "PickUpDropType", render: function (data, type, row) {
                    if (row.IsPickUp == true) {
                        return "PickUp-" + row.InteractionType;
                    }
                    else {
                        return "Drop-" + row.InteractionType;
                    }
                }
            },
            { "data": "PaymentCollected", "name": "PaymentCollected" },
            {
                "data": "PaymentCollected", render: function (data, type, row) {
                    if (data == "Yes" || data == "YES") {
                        return row.PaymentMode + "|" + row.PaymentReason + "|" + row.Amount + "|" + row.ReferenceNo;
                    }
                    else {
                        return "-";
                    }
                }
            },
            { "data": "PaymentRemarks", "name": "PaymentRemarks" },
            { "data": "Disposition", "name": "Disposition" },
            {
                "data": "RescheduledDateTime", render: function (data, type, row) {
                    return parseJsonDateTime(data) + "-" + row.Disposition;
                }
            },
            { "data": "Reasons", "name": "Reasons" },
            {
                "data": "DictFilesList", render: function (data, type, row) {
                    if (data != null && data != undefined && data.length > 0) {
                        var MainContainer = "<div class='img-container'>";

                        for (var file in data) {
                            MainContainer = MainContainer + `<div class="img-Subcontainer"><center><a href="${codePath + Object.values(data[file])}" target="_black"><i style="font-size:1.9em" class="fa fa-picture-o" aria-hidden="true"></i></a>
                                        <span>${Object.keys(data[file])}</span><center></div>`;
                        }
                        return MainContainer + "</div>";
                    }
                    else {
                        return "-";
                    }
                }
            },
        ],
        //responsive: screen.width < 500 ? true : false,
        responsive: true,
        "initComplete": function (data) {
            $('#tblExceptionDriver').text(data.json.exception);
            // filtercountDisplay();

            //$('.dataTable').wrap('<div class="dataTables_scroll" />');
        },
        "serverSide": "true",
        "processing": "true",
        "serverSide": "true",
        "sorting": "false",
        //"scrollX": "true",
        //"scrollY": "300",
        "paging": "true",
        //"searching": "true",
        "language": {
            "processing": "Loading Please Wait.....!"
        },
        order: [],
        pageLength: 5
    });
}

function getPolicyDropInteraction() {
    var dataTableCallDispo = $('#tbl_policydrop_interactions').DataTable({
        "destroy": true,
        "ajax": {
            "url": siteRoot + "/CallLogging/getPolicyDropInteraction/",
            "type": "POST",
            "datatype": "json",
            "data": {},
        },
        "columns": [
            {
                "data": "id", render: function (data, type, row) {
                    return "PD" + data;
                }
            },
            { "data": "wyzuser_name", "name": "wyzuser_name" },
            { "data": "AgentName", "name": "AgentName" },
            {
                "data": "updated_datetime", render: function (data, type, row) {
                    return parseJsonDateTime(data);
                }
            },
            {
                "data": "appointment_date", render: function (data, type, row) {
                    return parseJsonDateTime(data);
                }
            },
            { "data": "appointment_time", "name": "appointment_time" },
            { "data": "LocationName", "name": "LocationName" },
            { "data": "creRemarks", "name": "creRemarks" },
            { "data": "custRemarks", "name": "custRemarks" },
        ],
        responsive: true,
        "initComplete": function (data) {
            $('#tblExceptionPolicyDrop').text(data.json.exception);
            // filtercountDisplay();

            //$('.dataTable').wrap('<div class="dataTables_scroll" />');
        },
        "serverSide": "true",
        "processing": "true",
        "serverSide": "true",
        "sorting": "false",
        //"scrollX": "true",
        //"scrollY": "300",
        "paging": "true",
        //"searching": "true",
        "language": {
            "processing": "Loading Please Wait.....!"
        },
        order: [],
        pageLength: 5
    });
}

function Assigncoupons() {
    $('#coupon_history').DataTable({
        "destroy": true,
        "ajax": {
            "url": siteRoot + "/CallLogging/getCouponInteraction/",
            "type": "POST",
            "datatype": "json",
            "data": {},
        },
        "columns": [

            { "data": "couponcode", "name": "couponcode" },
            { "data": "coupondeatails", "name": "coupondeatails" },
            {
                "data": "issuedate", render: function (data, type, row) {
                   return parseJsonDateTime(data);
                }
            },
            {
                "data": "couponexpirydate", render: function (data, type, row) {
                    return parseJsonDateTime(data);
                }
            },
            { "data": "cre_name", "name": "cre_name" },
            { "data": "status", "name": "status" },
            {
                "data": "redemptiondate" , render: function (data, type, row) {
                   
                    return parseJsonDateTime(data);
                }
            },

        ],
        responsive: true,
        "initComplete": function (data) {
            $('#tblExceptionCoupons').text(data.json.exception);
        },
        "serverSide": false,
        "processing": "true",
        "sorting": "false",
        "paging": "true",
        "language": {
            "processing": "Loading Please Wait.....!"
        },
        order: [],
        pageLength: 5
    });
}

//------------------------------------- Interaction History Supporting Functions ---------------------------------------- End

//-------------------------------------PulOut Related Supporting Functions -----------------------------------------------Start
function ajaxCallServiceLoadOfCustomer() {
        var dealerCode = $('#PkDealercode').val();

    $.ajax({
        type: 'POST',
        url: siteRoot + "/CallLogging/getServiceDataOfCustomer/",
        datatype: 'json',
        data: {},
        cache: false,
        success: function (res) {
            if (res.serviceLoadData != null) {


                var tableHeaderRowCount = 1;
                var table = document.getElementById('example800');


                if ($.fn.DataTable.isDataTable("#example800")) {
                    var table = $("#example800").DataTable();
                    table.clear();
                    table.destroy();
                }

                var rowCount = table.rows.length;
                for (var i = tableHeaderRowCount; i < rowCount; i++) {
                    table.deleteRow(tableHeaderRowCount);
                }
                var serviceData = res.serviceLoadData.serviceLoadList;
                var vehicleData = res.serviceLoadData.vehicleLoadList;
                
                for (var j = 0; j < serviceData.length; j++) {
                    console.log("menu code description:" + serviceData[j]);

                }
                for (i = 0; i < vehicleData.length; i++) {
                    var variant, vehcolor, serviceDate, uploadDate, serviceType, lastServiceMeterReading, serviceAdvisor, billAmt, serviceLocations;
                    if (vehicleData[i].variant != null) {

                        variant = vehicleData[i].variant;
                    } else {
                        variant = '-';
                    }
                    if (vehicleData[i].color != null) {

                        var vehcolor = vehicleData[i].color;
                    } else {
                        vehcolor = '-';
                    }
                    if (serviceData[i].serviceDate != null) {

                        serviceDate = serviceData[i].serviceDate;
                    } else {
                        serviceDate = '-';
                    }

                    if (serviceData[i].uploadDateuploadDate != null) {

                        uploadDate = parseJsonDateTime(serviceData[i].uploadDateuploadDate);
                    } else {
                        uploadDate = '-';
                    }

                    if (serviceData[i].serviceType != null) {

                        serviceType = serviceData[i].serviceType;
                    } else {
                        serviceType = '-';
                    }
                    if (serviceData[i].lastServiceMeterReading != null) {

                        lastServiceMeterReading = serviceData[i].lastServiceMeterReading;
                    } else {
                        lastServiceMeterReading = '-';
                    }




                    if (serviceData[i].serviceAdvisor != null) {
                        serviceAdvisor = serviceData[i].serviceAdvisor;

                    } else {
                        serviceAdvisor = '-';

                    }
                    if (serviceData[i].technician != null) {
                        technician = serviceData[i].technician;

                    } else {
                        technician = '-';

                    }

                    if (serviceData[i].billAmt != null) {
                        billAmt = serviceData[i].billAmt;

                    } else {
                        billAmt = '-';

                    }


                    if (serviceData[i].serviceLocaton != null) {
                        serviceLocations = serviceData[i].serviceLocaton;
                    } else {
                        serviceLocations = '-';
                    }

                    if (serviceData[i].jobCardDate != null) {
                        var jobCardDate = serviceData[i].jobCardDate;
                    } else {
                        jobCardDate = '-';
                    }

                    if (serviceData[i].jobCardDate != null) {
                        var jobCardDate = serviceData[i].jobCardDate;
                    } else {
                        jobCardDate = '-';
                    }

                    if (serviceData[i].billDate != null) {
                        var billDate = serviceData[i].billDate;
                    } else {
                        billDate = '-';
                    }


                    if (serviceData[i].kilometer != null) {
                        var kilometer = serviceData[i].kilometer;
                    } else {
                        kilometer = '-';
                    }

                    if (serviceData[i].custName != null) {
                        var custName = serviceData[i].custName;
                    } else {
                        custName = '-';
                    }
                    if (serviceData[i].phonenumber != null) {
                        var phonenumber = serviceData[i].phonenumber;
                    } else {
                        phonenumber = '-';
                    }
                    if (vehicleData[i].category != null) {
                        var category = vehicleData[i].category;
                    } else {
                        category = '-';
                    }
                    Number.prototype.round = function (decimals) {
                        return Number((Math.round(this + "e" + decimals) + "e-" + decimals));
                    }

                    var partAmt;
                    var labourAmt;
                    var totalAmt;
                    if (serviceData[i].mfgPartsTotal != null || serviceData[i].localPartsTotal != null || serviceData[i].maximileTotal != null || serviceData[i].oilConsumablesTotal != null || serviceData[i].maxiCareTotal != null || serviceData[i].mfgAccessoriesTotal != null || serviceData[i].localAccessoriesTotal != null) {
                        var mfgPartsTotal = serviceData[i].mfgPartsTotal;
                        var localPartsTotal = serviceData[i].localPartsTotal;
                        var maximileTotal = serviceData[i].maximileTotal;
                        var oilConsumablesTotal = serviceData[i].oilConsumablesTotal;
                        var maxiCareTotal = serviceData[i].maxiCareTotal;
                        var mfgAccessoriesTotal = serviceData[i].mfgAccessoriesTotal;
                        var localAccessoriesTotal = serviceData[i].localAccessoriesTotal;
                        partAmt = mfgPartsTotal + localPartsTotal + maximileTotal + oilConsumablesTotal + maxiCareTotal + mfgAccessoriesTotal + localAccessoriesTotal;
                        partAmt = partAmt.round(2);
                    } else {
                        partAmt = '-';
                    }

                    if (serviceData[i].inhouseLabourTotal != null || serviceData[i].outLabourTotal != null) {
                        var inhouseLabourTotal = serviceData[i].inhouseLabourTotal;
                        var outLabourTotal = serviceData[i].outLabourTotal;
                        labourAmt = inhouseLabourTotal + outLabourTotal;
                        abourAmt = labourAmt.round(2);
                    } else {
                        labourAmt = '-';
                    }

                    if (partAmt != null || labourAmt != null) {
                        var totalAmt = partAmt + labourAmt;
                        totalAmt = totalAmt.round(2);
                    } else {
                        totalAmt = '-';
                    }

                    var workshop = serviceData[i].workshop;
                    var laborDetail = '';
                    var defectDetail = '';
                    var jobcardDetail = '';
                    if (serviceData[i].laborDetails != null) {
                        laborDetail = serviceData[i].laborDetails;
                    }
                    if (serviceData[i].defectDetails != null) {
                        defectDetail = serviceData[i].defectDetails;
                    }
                    if (serviceData[i].jobCardRemarks != null) {
                        jobcardDetail = serviceData[i].jobCardRemarks;
                    }

                    var jobcardloc = serviceData[i].jobcardlocation;
                    var menudesc = '';
                    var menudescrip = '';
                    if (serviceData[i].menuCodeDesc != null) {
                        menudesc = serviceData[i].menuCodeDesc;
                        menudescrip = menudesc.substring(0, 30) + "..";
                    }
                    if (serviceData[i].kminhours != null) {
                        kminhours = serviceData[i].kminhours;

                    } else {
                        kminhours = '-';

                    }



                    tr = $('<tr/>');
                    if (dealerCode == "SUKHMANI") {
                        tr.append("<td><a data-toggle='modal' data-target='#marutiServiceModal' onclick=getserviceHistoryDetails('" + serviceData[i].jobCardNumber + "','" + serviceData[i].vehicle_vehicle_id + "');><i class='fa fa-info-circle' data-toggel='tooltip' title='Other Details' style='font-size:30px;color:red;' id='modaltest'></i></a></td>");
                                        }
                    else {
                        tr.append("<td><a data-toggle='modal' data-target='#marutiServiceModal' onclick=marutiServiceModal('" + jobcardloc + "');><i class='fa fa-info-circle' data-toggel='tooltip' title='Other Details' style='font-size:30px;color:red;' id='modaltest'></i></a></td>");

                    }
                    tr.append("<td>" + uploadDate + "</td>");
                    //tr.append("<td>" + serviceLocations + "</td>");
                    tr.append("<td>" + jobCardDate + "</td>");
                    tr.append("<td>" + serviceData[i].jobCardNumber + "</td>");
                    tr.append("<td>" + workshop + "</td>");
                    tr.append("<td>" + billDate + "</td>");
                    tr.append("<td>" + phonenumber + "</td>");
                    tr.append("<td>" + kilometer + "</td>");
                    //tr.append("<td>" + vehicleData[i].model + "</td>");
                    //tr.append("<td>" + vehicleData[i].vehicleRegNo + "</td>");
                    tr.append("<td>" + serviceType + "</td>");
                    tr.append("<td title='" + menudesc + "'>" + menudescrip + "</td>");
                    tr.append("<td>" + serviceAdvisor + "</td>");
                    tr.append("<td>" + technician + "</td>");
                    //tr.append("<td>" + custName + "</td>");
                    //tr.append("<td>" + category + "</td>");
                    tr.append("<td>" + partAmt + "</td>");
                    tr.append("<td>" + labourAmt + "</td>");
                    tr.append("<td>" + billAmt + "</td>");
                    tr.append("<td>" + kminhours + "</td>");
                   
                    $('#example800').append(tr);
                }

                $("#example800").dataTable({
                    //destroy: true
                    responsive: true,
                    columnDefs: [
                        { targets: [-7, -3], className: 'dt-body-right' }
                    ],
                    "order": []
                });
                //$('#example800').DataTable();
            }
        }, error: function (error) {

        }
    });
}

function marutiServiceModal(jobcardloc) {
    var defectDetail = '';
    var laborDetail = '';
    var jobcardDetail = '';
    $.ajax({
        type: 'POST',
        url: siteRoot + "/CallLogging/getServiceByJobcardlocation/",
        datatype: 'json',
        data: { jocardloc: jobcardloc },
        cache: false,
        success: function (res) {
            var details = res.defectDetails;
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
        }, error: function (error) {

        }
    });
}
//-------------------------------------PulOut Related Supporting Functions -----------------------------------------------End

//------------------------------------ Email Related Supporting Functions ------------------------------------------ Start
function openEmailpopUp(ele) {
    var docName = $(ele).attr('data-docName');
    var deptName = $(ele).attr('data-deptname');
    var getCredentialFor = $(ele).attr('data-EmailFor');
    var docId = $(ele).attr('data-docId');

    if (getCredentialFor != "doc") {
        docId = 0;
    }
    var toEmail = $.trim($('#ddl_email').text());
    if (toEmail == "") {
        //$('#sendManualEmailUpload').css({ 'display': 'none' });
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Sorry! there is no preferred email-id.'
        });
        return false;
    }

    bindEmailIds('emailCredentialIds', 3, 'Email');
    $('#hdEmail_To').val($.trim(toEmail));
    $('#emailPasswordDiv').hide();
    $('#myModalEmailPopUp').modal('show');
    $('#emailSubject').val('');

    $('#ddl_Email_Type').val('');

    $('#ddl_Email_Location').val('');

    $('#ddl_Email_Workshop').val('');


    if (getCredentialFor == "doc") {
        $('#custSelectionDiv').hide();
        $('#attachPanel').show();
        $.ajax({
            type: 'POST',
            url: siteRoot + "/CallLogging/getEmailCredentials/",
            datatype: 'json',
            data: { docId: docId, getCredentialFor: getCredentialFor },
            cache: false,
            success: function (res) {
                if (res.success == true) {
                    //debugger;
                    if (res.getCredentialFor == "doc" && res.files == "") {
                        //alert('No Attachments Found');
                        Lobibox.notify('warning', {
                            continueDelayOnInactiveTab: true,
                            msg: 'No Attachments Found'
                        });
                    }
                    else {


                        //if (toEmail == "") {
                        //    $('#sendManualEmailUpload').css({ 'display': 'none' });
                        //    Lobibox.notify('warning', {
                        //        continueDelayOnInactiveTab: true,
                        //        msg: 'Sorry! there is no preferred email-id.'
                        //    });
                        //    return false;
                        //}
                        //else
                        //{
                        $('#sendManualEmailUpload').css({ 'display': 'block' });
                        //}

                        var vehi = $.trim($('#vehRegNO').text());
                        var customer = $.trim($('#custName').text());
                        //$('#emailCredentialIds').empty();
                        //$('#emailCredentialIds').append('<option>--Select--</option>')
                        $('#attachedDocs').empty();
                        //---------Adding to,Subject,Body------Ex...



                        //----------Finish-------------
                        if (res.getCredentialFor == "doc") {
                            $('#myModalEmailPopUp').modal('show');
                            $('#emailSubject').val('Document For Vehicle' + vehi);
                            $('#emailtemplate').val('Dear Mr. ' + customer);
                            var filesList = res.files.split(';');
                            var fileNames = res.fileNames.split(';');
                            for (var i = 0; i < filesList.length; i++) {
                                $('#attachedDocs').append(`<div class="col-md-3"><input type="checkbox" id="emailCk${i}" onclick="fileUpdated(this)" class="emailCk" value="${filesList[i]}" checked/>${fileNames[i]}</div>`)
                                emailList[filesList[i]] = filesList[i];
                            }
                            $('#hEmailFor').val('doc');
                            //$('#emailForm').attr('action', '/CallLogging/sendEmail?id=doc');
                            $('#hduploadedUser').val(res.uploadedUser);
                            $('#attachPanel').show();
                            $('#emailPasswords').val('');
                            //$('#emailSubject').val('');
                        }
                        else {
                            //$('#emailForm').attr('action', '/CallLogging/sendEmail?id=custom');
                            $('#hEmailFor').val('custom')
                            $('#attachPanel').hide();
                            $('#emailPasswords').val('');
                            //$('#emailSubject').val('');
                        }
                        //var emails = res.emails.split(';');
                        //for (var i = 0; i < emails.length; i++) {
                        //    $('#emailCredentialIds').append(`<option value="${emails[i]}">${emails[i]}</option>`);
                        //}
                    }

                    $('#myModalEmailPopUp').modal('show');
                }
                else {
                    alert('Something Went Wrong');
                }

            }, error: function (error) {
                alert(error);
            }
        });
    }
    else {
        $('#custSelectionDiv').show();
        $('#attachPanel').hide();
        $('#emailtemplate').val('');
    }

}

$("#emailtemplate").keypress(function (e) {
    var key = e.keyCode || e.which;
    console.log(key + "");

    //not allowing(<,>,~,-,;)
    if (key == 60 || key == 62 || key == 126 || key == 45 || key == 59) {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'This symbol is not allowed'
        });

        return false;
    }
});

$('#btnEmailSubmit').click(function () {

    var emilSub = $('#emailSubject').val().replace(/[\\\#()$~'"*<>{}]/g, ' ');
    var emailBody = $('#emailtemplate').val().replace(/[\\\#()$~'"*<>{}]/g, ' ');
    $('#emailSubject').val(emilSub);
    $('#emailtemplate').val(emailBody);

    var attachments;
    for (var key in emailList) {
        attachments = emailList[key];
        var preValue = $('#attachmentHidden').val();
        if (preValue == undefined) {
            $('#attachmentHidden').val(emailList[key] + ";");
            $('#attachmentHidden').attr('value', emailList[key] + ";");
        }
        else {
            $('#attachmentHidden').val(preValue + emailList[key] + ";");
        }

    }
});

//email success
function emailSuccess(res) {
    if (res.success == true) {
        Lobibox.notify('info', {
            msg: 'Email sent successfully'
        });
    }
    else if (res.success == false) {
        Lobibox.notify('warning', {
            msg: res.error
        });
    }
    $('#attachmentHidden').val('');
    $('#myModalEmailPopUp').modal("hide");
    $('#mainLoader').fadeOut('slow');

}

//email failure
function emailFailure(error) {
    $('#attachmentHidden').val('');

    $('#myModalEmailPopUp').modal("hide");
    Lobibox.notify('warning', {
        msg: "Server error, failed to sent email."
    });
    $('#mainLoader').fadeOut('slow');
}

//adding user selected value to dictionary.........user options
function fileUpdated(ele) {

    //debugger;
    if ($(ele).prop("checked")) {
        var value = $(ele).val();
        if (!emailList[value]) {
            emailList[value] = value;
        }
    }
    else {
        var value = $(ele).val();
        delete emailList[value];
    }
}

//supporting function to add updated address
function addSelfId(Addressddl) {
    var presentValue = $('#AddAddrMMSSaveMe').val();
    $('#AddAddrMMSSaveMe').val(presentValue + '_' + Addressddl);
}


$(":submit").on('click', function () {
    $("#custSubBtn").val($(this).val());
})

//------------------------------------ Email Related Supporting Functions ------------------------------------------ End

//-------------------------------------- PopUp Address Modification Supporting Function -----------------------------Start
$('#ppincode').keyup(function () {
    var val = $(this).val();
    var citySelected = $('#paddrrCity').val();
    var stateSelected = $('#pstateId').val();
    if (val.length === 6 && (stateSelected === "--SELECT--" || stateSelected === "" || stateSelected === null) && (citySelected === "--SELECT--" || citySelected === null || citySelected === "")) {
        $.ajax({
            url: siteRoot + "/CallLogging/getStateCityByPincode/",
            cache: false,
            dataType: "json",
            type: "post",
            data: { values: val },
            success: function (result) {
                if (result.success) {
                    //$('#paddrrCity').prop('disabled', false);
                    var cityDDL = "";
                    var stateDDL = "";
                    $.each(result.ListOfCity, function (item) {
                        cityDDL += "<option value='" + result.ListOfCity[item] + "'>" + result.ListOfCity[item] + "</option>"
                    });
                    $('#paddrrCity').append(cityDDL);
                    $('#pstateId').val(result.state[0]).change();
                }
                else {
                    alert('failure');
                }
            }
        });
    }
    //}
});

function reset() {
    $('#newAddress').val('');
    $('#ppincode').val('');
    //$('#paddrrCity').prop('selectedIndex', 0);
    $('#paddrrCity').empty();
    //$('#paddrrCity').attr('disabled', true);
    $('#pstateId option:first').prop('selected', true);
    $('#IsPrimary').prop('checked', false);
}

function getAddressForPopUp() {
    $.ajax({
        url: siteRoot + '/CallLogging/getDataForAddressTable/',
        type: 'get',
        success: function (result) {
            if (result.success) {
                $('#popUpBody').empty();
                var userRole = UserRole;
                var resultLength = result.listOfAddr.length;
                for (var i = (resultLength - 1); i >= 0; i--) {

                    if (result.listOfAddr[i].isPrimary != true) {
                        $(`<tr><td><input id="CK_${result.listOfAddr[i].addressId}" type="radio" class="isPrimaryCheck" onclick="isPrimaryCheck(this);" name="prefered" value="${result.listOfAddr[i].isPrimary}" /></td>
                                                        <td>${result.listOfAddr[i].address}</td>
                                                        <td> ${result.listOfAddr[i].city}</td>
                                                        <td>${result.listOfAddr[i].pincode}</td>
                                                        <td>${parseJsonDateTime(result.listOfAddr[i].updateOn)}</td>
                                                        <td>${result.listOfAddr[i].updatedBy}</td>
                                                        ${userRole == "CREManager" ? `<td><button id="btn_${result.listOfAddr[i].addressId}" onclick="deleteAddress(this)">Delete</button></td></tr>` : `<td></td>`}`).appendTo('#popUpBody');
                    }
                    else {
                        $(`<tr><td><input id="CK_${result.listOfAddr[i].addressId}" type="radio" class="isPrimaryCheck" onclick="isPrimaryCheck(this);" name="prefered" value="${result.listOfAddr[i].isPrimary}" checked/></td>
                                                        <td>${result.listOfAddr[i].address}</td>
                                                        <td> ${result.listOfAddr[i].city}</td>
                                                        <td>${result.listOfAddr[i].pincode}</td>
                                                        <td>${parseJsonDateTime(result.listOfAddr[i].updateOn)}</td>
                                                        <td>${result.listOfAddr[i].updatedBy}</td>
                                                        ${userRole == "CREManager" ? `<td><button id="btn_${result.listOfAddr[i].addressId}" onclick="deleteAddress(this)">Delete</button></td></tr>` : `<td></td>`}`).appendTo('#popUpBody');
                    }

                    //if (result.listOfAddr[i].isPrimary != true) {
                    //    $('<tr><td><input id="CK_' + result.listOfAddr[i].addressId + '" type="radio" class="isPrimaryCheck" onclick="isPrimaryCheck(this);" name="prefered" value="' + result.listOfAddr[i].isPrimary + '" /></td>\
                    //                                <td>' + result.listOfAddr[i].address == null ? "" : result.listOfAddr[i].address + '</td>\
                    //                                <td> '+ result.listOfAddr[i].city == null ? "" : result.listOfAddr[i].city + '</td>\
                    //                                <td>'+ result.listOfAddr[i].pincode == null ? "" : result.listOfAddr[i].pincode + '</td>\
                    //                                <td>'+ parseJsonDateTime(result.listOfAddr[i].updateOn) + '</td>\
                    //                                <td>'+ result.listOfAddr[i].updatedBy == null ? "" : result.listOfAddr[i].address + '</td>\
                    //                                <td><button id="btn_'+ result.listOfAddr[i].addressId + '" onclick="deleteAddress(this)">Delete</button></td></tr> ').appendTo('#popUpBody');
                    //}
                    //else {
                    //    $('<tr><td><input id="CK_' + result.listOfAddr[i].addressId + '" type="radio" class="isPrimaryCheck" onclick="isPrimaryCheck(this);" name="prefered" value="' + result.listOfAddr[i].isPrimary + '" checked/></td>\
                    //                                <td>' + result.listOfAddr[i].address == null ? "" : result.listOfAddr[i].address + '</td>\
                    //                                <td> '+ result.listOfAddr[i].city == null ? "" : result.listOfAddr[i].city + '</td>\
                    //                                <td>'+ result.listOfAddr[i].pincode == null ? "" : result.listOfAddr[i].pincode + '</td>\
                    //                                <td>'+ parseJsonDateTime(result.listOfAddr[i].updateOn) + '</td>\
                    //                                <td>'+ result.listOfAddr[i].updatedBy == null ? "" : result.listOfAddr[i].updatedBy + '</td>\
                    //                                <td><button id="btn_'+ result.listOfAddr[i].addressId + '" onclick="deleteAddress(this)">Delete</button></td></tr> ').appendTo('#popUpBody');
                    //}
                }
                reset();
            }
        }
    });

}


function updateFieldExecutiveSelect() {
    $.ajax({
        url: siteRoot + '/CallLogging/updateAddressMSSId/',
        type: 'post',
        async: false,
        dataType: 'json',
        data: {},
        success: function (res) {
            if (res.success) {
                $("#AddressMSSId").empty();
                for (var i = 0; i < res.addresses.length; i++) {
                    var opt = new Option(res.addresses[i], res.addresses[i]);
                    $('#AddressMSSId').append(opt);
                }
            }
        }
    });
}

function deleteAddress(data) {
    var id = data.id.split('_')[1];

    if ($('#CK_' + id).is(':checked')) {
        Lobibox.notify('warning', {
            msg: 'Please select another preffered address before deleting preffered one!'
        });

        return false;
    }


    $.ajax({
        url: siteRoot + '/CallLogging/deleteRowFromPopUp/',
        type: 'post',
        dataType: 'json',
        async: false,
        data: { value: id },
        success: function (res) {
            if (res.success) {
                $(data).closest('tr').remove();
                updateFieldExecutiveSelect();
                //$('#AddressMSSId option[value="' + res.address + '"]').remove();
                Lobibox.notify('info', {
                    msg: 'Deleted Successfuly'
                });
                $('#AddressUpdatePopup').modal('hide');
            }
        }
    });
}

function updateNewAddress() {
    var validated = false;
    var checkboxVal = $('#IsPrimary').is(":checked");
    var addrVal = $('#newAddress').val();
    var stateVal = $('#pstateId').val();
    var cityVal = $('#paddrrCity').val();
    var pinVal = $('#ppincode').val();
    if (pinVal == "") {
        pinVal = 0;
    }
    var dealerName = $('#PkDealercode').val();
    if (dealerName == "INDUS" || dealerName == "ADVAITHHYUNDAI") {
        if (addrVal == "") {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Enter Enter AddressLine1'
            });
            return false;
        }

    }

    if (addrVal == "" && stateVal == "--SELECT--" && cityVal == "" && pinVal == 0) {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please enter all required fields'
        });
        return false;
    }
    else {
        var addressObject = {
            address: addrVal,
            state: stateVal,
            city: cityVal,
            pincode: pinVal,
            isPrimary: checkboxVal
        }
        $.ajax({
            url: siteRoot + '/CallLogging/AddNewAddress/',
            dataType: 'json',
            type: 'post',
            async: false,

            data: { values: JSON.stringify(addressObject) },
            success: function (result) {
                if (result.success) {
                    if (result.isPrimary == true) {
                        $('#prefAdressUpdate').text(result.address);
                        //  $('#AddressMSSId').append('<option value="' + result.address + '">' + result.address + '</option>');
                        $('#city').text(result.city);
                        $('#pin').text(result.pincode);
                    }
                    updateFieldExecutiveSelect();
                    Lobibox.notify('info', {
                        msg: 'Address added successfully'
                    });
                    getAddressForPopUp();
                    //$('<tr><td><input id="' + result.listOfAddr.addressId + '" type="radio" class="isPrimaryCheck" onclick="isPrimaryCheck(this);" name="prefered" value="' + result.listOfAddr.isPrimary + '" checked/></td>\
                    //            <td>' + result.listOfAddr.address + '</td>\
                    //            <td> '+ result.listOfAddr.city + '</td>\
                    //            <td>'+ result.listOfAddr.pincode + '</td>\
                    //            <td>'+ parseJsonDateTime(result.listOfAddr[i].updateOn) + '</td>\
                    //            <td>'+ result.listOfAddr.updatedBy + '</td>\
                    //            <td><button id="'+ result.listOfAddr.addressId + '" onclick="deleteAddress(this)">Delete</button></td></tr>').appendTo('#popUpBody');
                    reset();
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
}

function isPrimaryCheck(data) {
    var id = data.id.split('_')[1];
    $.ajax({
        url: siteRoot + "/CallLogging/isPrimaryChange/",
        type: 'post',
        async: false,
        dataType: 'json',
        data: { value: id },
        success: function (res) {
            if (res.success) {
                updateFieldExecutiveSelect();
                Lobibox.notify('info', {
                    msg: 'Preferred address changed successfully.'
                });
                $('#prefAdressUpdate').text(res.prefAddress);
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}

//-------------------------------------- PopUp Address Modification Supporting Function -----------------------------End


//---------------------------------PSF PullOut related supporting function -------------------------------------- Start
function getPsfDetails(cubeId) {
    $('.DispoContainer').hide();
    $('#psfloader').css({ 'display': 'block' });

    if (pvrCubeId != 0) {
        $('#jobCard_' + pvrCubeId).addClass('main-content');
        $('#jobCard_' + pvrCubeId).removeClass('main-content-selected');
    }

    $('.pcontent').hide();
    $('#detailsDiv').hide();
    $('#jobCard_' + cubeId).removeClass('main-content');
    $('#jobCard_' + cubeId).addClass('main-content-selected');

    pvrCubeId = cubeId;

    if ($.fn.DataTable.isDataTable("#tblpsfpull")) {
        var table = $("#tblpsfpull").DataTable();
        table.clear();
        table.destroy();
    }

    $.ajax({
        url: siteRoot + "/CallLoggingPSF/getPSFPullOuts/",
        cache: false,
        type: 'post',
        dataType: 'json',
        data: { cubeId: cubeId },
        success: function (res) {
            if ($.fn.DataTable.isDataTable("#tblpsfpull")) {
                var table = $("#tblpsfpull").DataTable();
                table.clear();
                table.destroy();
            }
            if (res.success == true) {

                $('#pcustName').text(res.data.CustName);
                $('#pchassis').text(res.data.ChassisNo);
                $('#pbilldate').text(parseJsonDateTime(res.data.BillDate));
                $('#psaledate').text(parseJsonDateTime(res.data.SaleDate));
                $('#pservicetype').text(res.data.ServiceType);
                $('#pservicelocation').text(res.data.ServiceLoc);
                $('#plastdisposition').text(res.data.FinalDispo);
                $('#feedbackpsfstatcust').text(res.data.psfstatus);
                $('.pcontent').show();
                $('#detailsDiv').show();

                if (res.calldisoIds.includes('22') || res.calldisoIds.includes('44')) {

                    if (res.calldisoIds.includes('22')) {
                        var index = (res.calldisoIds.split(',')).indexOf('22');
                        var cubeid = (res.cubeIds.split(','))[index];

                        $('#btnViewDetails').attr('onclick', 'getPSFDispoDetails(' + cubeid + ')');
                    }

                    if (res.calldisoIds.includes('44')) {
                        var index = (res.calldisoIds.split(',')).indexOf('44');
                        var cubeid = (res.cubeIds.split(','))[index];

                        $('#btnViewDetails').attr('onclick', 'getPSFDispoDetails(' + cubeid + ')');
                    }
                    $('#psfloader').css({ 'display': 'none' });

                }
                else {
                    $('#btnViewDetails').attr('onclick', 'getPSFDispoDetails(' + 0 + ')');
                    $('#btnViewDetails').css({ 'cursor': 'no-drop' });
                    $('.DispoContainer').hide();
                    $('#psfloader').css({ 'display': 'none' });

                }
            }
            else {
                $('.DispoContainer').hide();

                Lobibox.notify('warning', function () {
                    msg: res.exception
                });
            }
            //$("#tblpsfpull").dataTable({
            //    //destroy: true
            //    responsive: true,
            //    columnDefs: [
            //        { targets: [-7, -3], className: 'dt-body-right' }
            //    ],
            //    "order": []
            //});
        },
        error: function (ex) {
            alert(ex);
            console.log(ex);
        }
    });
}

function getPSFDispoDetails(cubeId) {
    var dealcrCode = document.getElementById("PkDealercode").value;

    var OEM = document.getElementById("PkOEM").value;
    //var dealcrCode = $('#PkDealercode').val();
    if (cubeId != 0) {
        $('#psfloader').css({ 'display': 'block' });

        $.ajax({
            url: siteRoot + "/CallLoggingPSF/getPSFDispoDetails/",
            type: 'post',
            dataType: 'json',
            cache: false,
            data: { cubeId: cubeId },
            success: function (res) {
                if (res.success == true) {
                    $('.DispoContainer').show();
                    $('.DispoContainer15Day').hide();
                    if (dealcrCode == "INDUS" || dealcrCode == "KATARIA" || dealcrCode == "SPEEDAUTO") {
                        if (res.campaign_id == "5") {
                            var bindData = "";
                            var disposition = "";
                            $('.DispoContainer').hide();

                            if (res.data.calldispositiondata_id == 22) {
                                disposition = "Satisfied";
                            }
                            else if (res.data.calldispositiondata_id == 44) {
                                disposition = "Dissatisfied";
                            }
                            bindData = '<div class="row" style="margin-top: 15px; display: block; color: black" id="psf15daycomments"><div class="col-lg-12">'
                                + '<div class="col-md-6 col-sm-6 col-xs-6"><label>Overall Feedback:</label>'
                                + '<span id="psf15thdayoverallfeedSatps" style="padding:5px 8px 5px 8px;border-radius: 5px;"></span><span style="font-size:14pt;background-color: #b4efb3;" >' + disposition + '</span></div>'
                                + '<div class="col-md-6 col-sm-6 col-xl-12"><label>CRE Remarks:</label><span style="background-color:#d9e60b;">' + res.data.Remarks + '</span></div><div class="col-md-6 col-sm-6 col-xs-6"></div><div class="col-md-6 col-sm-6 col-xl-12"><label>Customer Remarks:</label><span style="background-color:#d9e60b;">' + res.data.Comments + '</span></div></div>'

                            $(".DispoContainer15Day").append(bindData);
                            var dict = {};

                            for (i = 0; i < res.psf_questions.length; i++) {
                                var answer = res.data[res.psf_questions[i].binding_var];

                                if (res.psf_questions[i].display_type == "radio-button") {
                                    var displayAnswers = res.psf_questions[i].radio_values.split(",");
                                    var displayOptions = res.psf_questions[i].radio_options.split(",");
                                }
                                for (j = 0; j < displayAnswers.length; j++) {
                                    dict[displayAnswers[j]] = displayOptions[j];
                                }
                                var cls;
                                var value;
                                if (dict[answer] != null && dict[answer] != "" && dict[answer] != undefined) {
                                    value = dict[answer].toUpperCase();

                                }
                                else {
                                    value = "-";
                                }


                                if (value.includes("POOR") || value.includes("FAIR") || value.includes("GOOD") || value.includes("V.GOOD") || value.includes("NO")) {
                                    cls = "background-color:#e8c6c6;";
                                }
                                else {
                                    cls = "background-color:#c6e8c6;";
                                }

                                $(".DispoContainer15Day").append('<br/><div style="background-color:grey;margin-top:15px;margin-bottom:15px;"><div  class="col-md-8 col-sm-8-col-xs-12 psques" >' + res.psf_questions[i].question + '</div><div class="col-md-4 col-sm-4 col-xs-12 "style=' + cls + '>' + value
                                    + '</div></div><br/>');

                            }
                            $('.DispoContainer15Day').show();
                        }
                        else {
                            //---------code for indus display
                            $('#serviceadv').text(res.data.escalation);
                            if (res.data.escalation == "Yes") {
                                $('#div_serviceadv').css({ 'background-color': '#c6e8c6' });
                            }
                            else {
                                $('#div_serviceadv').css({ 'background-color': '#e8c6c6' });
                            }

                            //$('#safeed').text(res.data.instfeed);
                            //if (res.data.instfeed == "Yes") {
                            //    $('#div_safeed').css({ 'background-color': '#c6e8c6' });
                            //}
                            //else {
                            //    $('#div_safeed').css({ 'background-color': '#e8c6c6' });
                            //}

                            $('#demandedrepairsCompleted').text(res.data.demandedrepair);
                            if (res.data.demandedrepair == "Yes") {
                                $('#div_demandedrepairsCompleted').css({ 'background-color': '#c6e8c6' });
                            }
                            else {
                                $('#div_demandedrepairsCompleted').css({ 'background-color': '#e8c6c6' });
                            }

                            $('#promiseddeliveryTime').text(res.data.promiseddlvrytime);
                            if (res.data.promiseddlvrytime == "Yes") {
                                $('#div_promiseddeliveryTime').css({ 'background-color': '#c6e8c6' });
                            }
                            else {
                                $('#div_promiseddeliveryTime').css({ 'background-color': '#e8c6c6' });
                            }

                            $('#washingQuality').text(res.data.washQualty);
                            if (res.data.washQualty == "Yes") {
                                $('#div_washingQuality').css({ 'background-color': '#c6e8c6' });
                            }
                            else {
                                $('#div_washingQuality').css({ 'background-color': '#e8c6c6' });
                            }


                            $('#vehicleserviceCharges').text(res.data.servicecharges);
                            if (res.data.servicecharges == "Yes") {
                                $('#div_vehicleserviceCharges').css({ 'background-color': '#c6e8c6' });
                            }
                            else {
                                $('#div_vehicleserviceCharges').css({ 'background-color': '#e8c6c6' });
                            }




                            $('#qos').text(res.data.qos);
                            if (res.data.qos == "Yes") {
                                $('#div_qos').css({ 'background-color': '#c6e8c6' });
                            }
                            else {
                                $('#div_qos').css({ 'background-color': '#e8c6c6' });
                            }

                            $('#overallExpo').text(res.data.overallexpo);
                            if (res.data.overallexpo != 'Average' && res.data.overallexpo != 'Poor') {
                                $('#div_overallExpo').css({ 'background-color': '#c6e8c6' });
                            }
                            else {
                                $('#div_overallExpo').css({ 'background-color': '#e8c6c6' });
                            }

                            $('#rateSA').text(res.data.ratesa);

                            if (res.data.overallexpo != 'Average' && res.data.overallexpo != 'Poor') {
                                $('#overallfeedSat').text('Satisfied');
                                $('#overallfeedSat').css({ 'background-color': '#c6e8c6' });
                                $('#satIcon').show();
                                $('#disSatIcon').hide();
                                $('#overallfeedDisSat').hide();
                                $('#overallfeedSat').show();
                            }
                            else {
                                $('#overallfeedDisSat').text('Dissatisfied');
                                $('#overallfeedDisSat').show();

                                $('#overallfeedDisSat').css({ 'background-color': '#e8c6c6' });
                                $('#disSatIcon').show();
                                $('#satIcon').hide();
                                $('#overallfeedSat').hide();
                            }

                            if (res.data.vehicleafter == "Good") {
                                $('#vehiPerform').text(res.data.vehicleafter);
                                $('#vehiPerform').css({ 'color': 'green' });
                            }
                            else {
                                $('#vehiPerform').text(res.data.vehicleafter);
                                $('#vehiPerform').css({ 'color': 'red' });
                            }

                            if (res.natureOfComplaints != null && res.natureOfComplaints != "") {
                                $('#natureOfComplaints').text(res.natureOfComplaints);
                                $('#divnatureOfComplaint').show();
                            }
                            else {
                                $('#divnatureOfComplaint').hide();
                            }

                            if (res.data.istechnical == true || res.data.isnontechnical == true) {
                                $('#divFeedBad').show();

                                //------------------Technical Section --------------------------
                                if (res.data.istechnical == true) {
                                    $('#techans').text('Yes');
                                    $('#div_techans').css({ 'background': '#c6e8c6' });

                                    var technicalquestions = JSON.parse(res.techincaldata);
                                    var techdiv_contents = "", contents = "";
                                    for (var i = 0; i < technicalquestions.length; i++) {
                                        if (technicalquestions[0].id == technicalquestions[i].mainDispositionId) {
                                            break;
                                        }
                                        else {
                                            var suboptions = technicalquestions.filter(m => m.mainDispositionId == technicalquestions[i].id);
                                            var main_qs_start = "<div style='position:relative;' id='" + technicalquestions[i].id + "'>";
                                            var main_qs_end = "</div>";
                                            var main_qs = `<div class="col-md-8 col-sm-8 col-xs-12 qusansbad" onclick="collapseHelper('tech_sub_qs_${technicalquestions[i].id}');" style="cursor: pointer;"><span id="main_qs_${technicalquestions[i].id}" class="main-qs">${technicalquestions[i].disposition}</span></div>`;
                                            var main_qs_ans = "";
                                            var sub_qs_start = "<div style='display:none;' class='animated bounceInLeft' id='tech_sub_qs_" + technicalquestions[i].id + "'>";
                                            var sub_qs_end = "</div>";
                                            var sub_qs_container = "";
                                            if (technicalquestions[i].disposition == "Body, Chassis & Upholstery" && res.data.bodychasis != null) {
                                                main_qs_ans = "<div class='col-md-4 col-sm-4 col-xs-12 qusans' style='background-color:#c6e8c6'>Yes</div>";
                                            }
                                            else if (technicalquestions[i].disposition == "Electricals" && res.data.electricals != null) {
                                                main_qs_ans = "<div class='col-md-4 col-sm-4 col-xs-12 qusans' style='background-color:#c6e8c6'>Yes</div>";
                                                //.electricals = key.Value;
                                            }
                                            else if (technicalquestions[i].disposition == "Performance" && res.data.performance != null) {
                                                main_qs_ans = "<div class='col-md-4 col-sm-4 col-xs-12 qusans' style='background-color:#c6e8c6'>Yes</div>";
                                                //.performance = key.Value;
                                            }
                                            else if (technicalquestions[i].disposition == "Powertrain" && res.data.powertrain != null) {
                                                main_qs_ans = "<div class='col-md-4 col-sm-4 col-xs-12 qusans' style='background-color:#c6e8c6'>Yes</div>";
                                                //.powertrain = key.Value;
                                            }
                                            else if (technicalquestions[i].disposition == "Safety" && res.data.safety != null) {
                                                main_qs_ans = "<div class='col-md-4 col-sm-4 col-xs-12 qusans' style='background-color:#c6e8c6'>Yes</div>";
                                                //.safety = key.Value;
                                            }
                                            else if (technicalquestions[i].disposition == "Steering & Suspension" && res.data.steeringNsuspention != null) {
                                                main_qs_ans = "<div class='col-md-4 col-sm-4 col-xs-12 qusans' style='background-color:#c6e8c6'>Yes</div>";
                                                //.steeringNsuspention = key.Value;
                                            }
                                            else if (technicalquestions[i].disposition == "Improper Expectation/Usage" && res.data.improperExpectNUsage != null) {
                                                main_qs_ans = "<div class='col-md-4 col-sm-4 col-xs-12 qusans' style='background-color:#c6e8c6'>Yes</div>";
                                                //.improperExpectNUsage = key.Value;
                                            }
                                            else {
                                                main_qs_ans = "<div class='col-md-4 col-sm-4 col-xs-12 qusans' style='background-color:#e8c6c6'>No</div>";
                                            }

                                            suboptions.forEach(function (qs) {
                                                if (res.data.bodychasis != null && res.data.bodychasis.includes(qs.id)) {
                                                    sub_qs_container = sub_qs_container + "<div style='padding-left:45px' class='col-md-8 col-sm-8 col-xs-12 qusans-sub'>" + qs.disposition + "</div><div class='col-md-4 col-sm-4 col-xs-12 qusans-sub' style='background-color:#e8c6c6'>Yes</div>";
                                                }
                                                else if (res.data.electricals != null && res.data.electricals.includes(qs.id)) {
                                                    sub_qs_container = sub_qs_container + "<div style='padding-left:45px' class='col-md-8 col-sm-8 col-xs-12 qusans-sub'>" + qs.disposition + "</div><div class='col-md-4 col-sm-4 col-xs-12 qusans-sub' style='background-color:#e8c6c6'>Yes</div>";
                                                }
                                                else if (res.data.performance != null && res.data.performance.includes(qs.id)) {
                                                    sub_qs_container = sub_qs_container + "<div style='padding-left:45px' class='col-md-8 col-sm-8 col-xs-12 qusans-sub'>" + qs.disposition + "</div><div class='col-md-4 col-sm-4 col-xs-12 qusans-sub' style='background-color:#e8c6c6'>Yes</div>";
                                                }
                                                else if (res.data.powertrain != null && res.data.powertrain.includes(qs.id)) {
                                                    sub_qs_container = sub_qs_container + "<div style='padding-left:45px' class='col-md-8 col-sm-8 col-xs-12 qusans-sub'>" + qs.disposition + "</div><div class='col-md-4 col-sm-4 col-xs-12 qusans-sub' style='background-color:#e8c6c6'>Yes</div>";
                                                }
                                                else if (res.data.safety != null && res.data.safety.includes(qs.id)) {
                                                    sub_qs_container = sub_qs_container + "<div style='padding-left:45px' class='col-md-8 col-sm-8 col-xs-12 qusans-sub'>" + qs.disposition + "</div><div class='col-md-4 col-sm-4 col-xs-12 qusans-sub' style='background-color:#e8c6c6'>Yes</div>";
                                                }
                                                else if (res.data.steeringNsuspention != null && res.data.steeringNsuspention.includes(qs.id)) {
                                                    sub_qs_container = sub_qs_container + "<div style='padding-left:45px' class='col-md-8 col-sm-8 col-xs-12 qusans-sub'>" + qs.disposition + "</div><div class='col-md-4 col-sm-4 col-xs-12 qusans-sub' style='background-color:#e8c6c6'>Yes</div>";
                                                }
                                                else if (res.data.improperExpectNUsage != null && res.data.improperExpectNUsage.includes(qs.id)) {
                                                    sub_qs_container = sub_qs_container + "<div style='padding-left:45px' class='col-md-8 col-sm-8 col-xs-12 qusans-sub'>" + qs.disposition + "</div><div class='col-md-4 col-sm-4 col-xs-12 qusans-sub' style='background-color:#e8c6c6'>Yes</div>";
                                                }
                                                else {
                                                    sub_qs_container = sub_qs_container + "<div style='padding-left:45px' class='col-md-8 col-sm-8 col-xs-12 qusans-sub'>" + qs.disposition + "</div><div class='col-md-4 col-sm-4 col-xs-12 qusans-sub' style='background-color:#c6e8c6'>NA</div>";
                                                }
                                            });

                                            techdiv_contents = techdiv_contents + main_qs_start + main_qs + main_qs_ans + sub_qs_start + sub_qs_container + sub_qs_end + main_qs_end;
                                        }
                                    }
                                    $('#div_techSubqus').html(techdiv_contents);
                                }
                                else {
                                    $('#techans').text('No');
                                    $('#div_techans').css({ 'background': '#c6e8c6' });
                                }

                                //--------------------------------Non Technical Section ------------------------
                                if (res.data.isnontechnical == true) {
                                    $('#nontechans').text('Yes');
                                    $('#div_nontechans').css({ 'background': '#c6e8c6' });

                                    var nontechnicalquestions = JSON.parse(res.nontechnicaldata);
                                    var nontechdiv_contents = "", contents = "";
                                    for (var i = 0; i < nontechnicalquestions.length; i++) {
                                        if (nontechnicalquestions[0].id == nontechnicalquestions[i].mainDispositionId) {
                                            break;
                                        }
                                        else {
                                            var suboptions = nontechnicalquestions.filter(m => m.mainDispositionId == nontechnicalquestions[i].id);
                                            var main_qs_start = "<div style='position:relative;' id='" + nontechnicalquestions[i].id + "'>";
                                            var main_qs_end = "</div>";
                                            var main_qs = `<div class="col-md-8 col-sm-8 col-xs-12 qusansbad" onclick="collapseHelper('nontech_sub_qs_${nontechnicalquestions[i].id}');" style="cursor: pointer;"><span id="main_qs_${nontechnicalquestions[i].id}" class="main-qs">${nontechnicalquestions[i].disposition}</span></div>`;
                                            var main_qs_ans = "";
                                            var sub_qs_start = "<div style='display:none;' class='animated bounceInLeft' id='nontech_sub_qs_" + nontechnicalquestions[i].id + "'>";
                                            var sub_qs_end = "</div>";
                                            var sub_qs_container = "";
                                            if (nontechnicalquestions[i].disposition == "Work Quality" && res.data.workQuality != null) {
                                                main_qs_ans = "<div class='col-md-4 col-sm-4 col-xs-12 qusans' style='background-color:#c6e8c6'>Yes</div>";
                                            }
                                            else if (nontechnicalquestions[i].disposition == "Service Advisor" && res.data.ServiceAdvisor != null) {
                                                main_qs_ans = "<div class='col-md-4 col-sm-4 col-xs-12 qusans' style='background-color:#c6e8c6'>Yes</div>";
                                                //.electricals = key.Value;
                                            }
                                            else if (nontechnicalquestions[i].disposition == "Spare Parts" && res.data.spareParts != null) {
                                                main_qs_ans = "<div class='col-md-4 col-sm-4 col-xs-12 qusans' style='background-color:#c6e8c6'>Yes</div>";
                                                //.performance = key.Value;
                                            }
                                            else if (nontechnicalquestions[i].disposition == "Billing" && res.data.billing != null) {
                                                main_qs_ans = "<div class='col-md-4 col-sm-4 col-xs-12 qusans' style='background-color:#c6e8c6'>Yes</div>";
                                                //.powertrain = key.Value;
                                            }
                                            else if (nontechnicalquestions[i].disposition == "Other" && res.data.othernonTech != null) {
                                                main_qs_ans = "<div class='col-md-4 col-sm-4 col-xs-12 qusans' style='background-color:#c6e8c6'>Yes</div>";
                                                //.safety = key.Value;
                                            }
                                            else {
                                                main_qs_ans = "<div class='col-md-4 col-sm-4 col-xs-12 qusans' style='background-color:#e8c6c6'>No</div>";
                                            }

                                            suboptions.forEach(function (qs) {
                                                if (res.data.workQuality != null && res.data.workQuality.includes(qs.id)) {
                                                    sub_qs_container = sub_qs_container + "<div style='padding-left:45px' class='col-md-8 col-sm-8 col-xs-12 qusans-sub'>" + qs.disposition + "</div><div class='col-md-4 col-sm-4 col-xs-12 qusans-sub' style='background-color:#e8c6c6'>Yes</div>";
                                                }
                                                else if (res.data.ServiceAdvisor != null && res.data.ServiceAdvisor.includes(qs.id)) {
                                                    sub_qs_container = sub_qs_container + "<div style='padding-left:45px' class='col-md-8 col-sm-8 col-xs-12 qusans-sub'>" + qs.disposition + "</div><div class='col-md-4 col-sm-4 col-xs-12 qusans-sub' style='background-color:#e8c6c6'>Yes</div>";
                                                }
                                                else if (res.data.spareParts != null && res.data.spareParts.includes(qs.id)) {
                                                    sub_qs_container = sub_qs_container + "<div style='padding-left:45px' class='col-md-8 col-sm-8 col-xs-12 qusans-sub'>" + qs.disposition + "</div><div class='col-md-4 col-sm-4 col-xs-12 qusans-sub' style='background-color:#e8c6c6'>Yes</div>";
                                                }
                                                else if (res.data.billing != null && res.data.billing.includes(qs.id)) {
                                                    sub_qs_container = sub_qs_container + "<div style='padding-left:45px' class='col-md-8 col-sm-8 col-xs-12 qusans-sub'>" + qs.disposition + "</div><div class='col-md-4 col-sm-4 col-xs-12 qusans-sub' style='background-color:#e8c6c6'>Yes</div>";
                                                }
                                                else if (res.data.otherYesnTech != null && res.data.otherYesnTech.includes(qs.id)) {
                                                    sub_qs_container = sub_qs_container + "<div style='padding-left:45px' class='col-md-8 col-sm-8 col-xs-12 qusans-sub'>" + qs.disposition + "</div><div class='col-md-4 col-sm-4 col-xs-12 qusans-sub' style='background-color:#e8c6c6'>Yes</div>";
                                                }
                                                else {
                                                    sub_qs_container = sub_qs_container + "<div style='padding-left:45px' class='col-md-8 col-sm-8 col-xs-12 qusans-sub'>" + qs.disposition + "</div><div class='col-md-4 col-sm-4 col-xs-12 qusans-sub' style='background-color:#c6e8c6'>NA</div>";
                                                }
                                            });

                                            nontechdiv_contents = nontechdiv_contents + main_qs_start + main_qs + main_qs_ans + sub_qs_start + sub_qs_container + sub_qs_end + main_qs_end;
                                        }
                                    }

                                    $('#div_nontechSubqus').html(nontechdiv_contents);
                                }
                                else {
                                    $('#nontechans').text('No');
                                    $('#div_nontechans').css({ 'background': '#c6e8c6' });
                                }
                            }
                            else {
                                $('#divFeedBad').hide();
                            }
                        }
                    }
                    else if (dealcrCode == "KALYANIMOTORS") {
                        if (res.data != null && res.data != "") {
                            var customerCat = document.getElementById("customerCat").value;

                            getReviews(res.data.modeOfServiceDone, "modeOfServiceDone")
                            if (customerCat != "BODYSHOP") {
                                getReviews(res.data.qFordQ1, "qFordQ1");
                                getReviews(res.data.qFordQ3, "qFordQ3");
                                getReviews(res.data.qFordQ5, "qFordQ5");
                                getReviews(res.data.qFordQ6, "qFordQ6");
                            }

                            if (customerCat == "BODYSHOP") {
                                getReviews(res.data.bodyrepairthrough, "bodyrepairthrough");
                                getReviews(res.data.experienceOnIns, "experienceOnIns");
                                getReviews(res.data.qualityofwork, "qualityofwork");
                            }

                            getReviews(res.data.qFordQ2, "qFordQ2")
                            getReviews(res.data.qFordQ7, "qFordQ7")
                            if (res.complaintCategory != null && res.complaintCategory != "") {
                                $('#complaintCategory').text(res.complaintCategory);

                            }
                            if (res.data.afterServiceComments != null && res.data.afterServiceComments != "") {
                                $('#qosafterServiceComments').text(res.data.afterServiceComments);
                                $('#div_afterServiceComments').css("color", "#006600");

                            }
                            else {
                                $('#qosafterServiceComments').text("NA");
                            }

                            if (res.data.qM2_ReasonOfAreaOfImprovement != null && res.data.qM2_ReasonOfAreaOfImprovement != "") {
                                $('#qosqM2_ReasonOfAreaOfImprovement').text(res.data.qM2_ReasonOfAreaOfImprovement);
                                $('#div_qM2_ReasonOfAreaOfImprovement').css("color", "#006600");

                            }
                            else {
                                $('#qosqM2_ReasonOfAreaOfImprovement').text("NA");
                            }
                            if (res.data.qFordQ9 != null && res.data.qFordQ9 != "") {
                                $('#qosqFordQ9').text(res.data.qFordQ9);
                                $('#div_qFordQ9').css("color", "#006600");
                            }
                            else {
                                $('#qosqFordQ9').text("NA");
                            }
                        }
                        else {
                            Lobibox.notify('warning', function () {
                                msg: res.exception
                            });
                        }
                    }
                    else if (OEM == "HYUNDAI" || dealcrCode == "JDAUTONATION") {
                        $('#questionDiv_hyundai').html('');
                        var divData = "";
                        if (res.data.length > 0) {
                            for (var i = 0; i < res.data.length; i++) {
                                var eachRow = `<div class="row">
                                        <div class="colo-md-8 col-sm-8 col-xs-12 qusans">
                                            <span>
                                               ${(i + 1)}. ${res.data[i].question}
                                            </span>
                                        </div>
                                        <div class="col-md-4 col-sm-8 col-xs-12 qusans" style="border-bottom:none;">
                                            <span>${res.data[i].ans == null ? "NA" : res.data[i].ans}</span>
                                        </div>
                                    </div>`;
                                divData = divData + eachRow;
                            }

                            if (res.selectedRadio == "Visited") {
                                $('#feedformSelecetd').text("Went yourself 🚗");
                            }
                            else if (res.selectedRadio == "pickup") {
                                $('#feedformSelecetd').text("Pickup Drop🕴🏻");
                            }

                            $('#questionDiv_hyundai').html(divData);

                        }
                    }

                    $('#psfloader').css({ 'display': 'none' });

                }
                else {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: res.exception
                    });
                    $('#psfloader').css({ 'display': 'none' });

                }
            },
            error: function (ex) {
                $('#psfloader').css({ 'display': 'none' });
                console.log(ex);
            }
        });
    }
    else {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'No Vehicle Disposition Found'
        });

    }
}

function getReviews(grade, type) {
    if (grade == "150") {
        $('#qos' + type).text("EXCELLENT 😀");
        //$('#div_' + type).css("background", "#006500");

    }
    else if (grade == "100") {
        $('#qos' + type).text("VERY GOOD 😊");
        //$('#div_' + type).css("background", "#80FF00");

    }
    else if (grade == "50") {
        $('#qos' + type).text("GOOD  😊");
        // $('#div_' + type).css("background", "#99FF33");

    }
    else if (grade == "0") {
        $('#qos' + type).text("FAIR 😐");
        //$('#div_' + type).css("background", "#FFFF33");

    }
    else if (grade == "-50") {
        $('#qos' + type).text("POOR 😠");
        //$('#div_' + type).css("background", "#FF3333");

    }

    else {
        $('#qos' + type).text("NA");
    }
}

function collapseHelper(div) {
    //if (dict[div]) {
    var status = dict[div];
    //if it is first click Open div and add to dict
    if (status == undefined || status == null) {
        dict[div] = 1;
        //$('#' + div).slideDown('normal');
        $('#' + div).show();
        for (var list in dict) {
            if (list != div) {
                //$('#' + list).slideUp('normal');
                $('#' + list).hide();
                dict[list] = 0;
            }
        }
        //$('html, body').animate({
        //    scrollTop: $('#' + div).offset().top
        //}, 500);
    }
    else {
        //Opening a Div
        if (status == 0) {
            //$('#' + div).slideDown('normal');
            $('#' + div).show();
            //$('#' + div).fadeIn(100);

            //$('#' + div).css({ "display": "block" });
            dict[div] = 1;
            for (var list in dict) {
                if (list != div) {
                    //$('#' + list).slideUp('normal');
                    $('#' + list).hide();
                    dict[list] = 0;
                }
            }
            //$('html, body').animate({
            //    scrollTop: $('#' + div).offset().top
            //}, 1500);
        }//To Close a Div
        else if (status == 1) {
            //$('#' + div).slideUp('normal');
            $('#' + div).hide();
            //$('#' + div).css({ "display": "none" });
            dict[div] = 0;
        }
    }
}
//---------------------------------PSF PullOut related supporting function -------------------------------------- End

// ---------------------------------- Info BIP Whatsapp integration code ---------------------------------------- Start

function typeOfMsg(ele) {
    $("#sendMsg").removeAttr("disabled");
    var typeOfMsg = ele.value;
    if (typeOfMsg == "SMS" || typeOfMsg == "WhatsAppNormal") {
        clearInfoBipTags();
        $("#dateTimeDiv").css("display", "none");
        $("#div_campSpecial").css("display", "none");
        $('#btn_whatsapp').hide();
        if (typeOfMsg == "WhatsAppNormal") {
            typeOfMsg = "SMS";
            $('#btn_whatsapp').show();
        }

        $('#infoBip_btn').hide();
    }
    else if (typeOfMsg == "WhatsApp") {
        clearInfoBipTags();
        $("#dateTimeDiv").css("display", "block");
        $('#div_date').show();
        $('#btn_whatsapp').hide();
        $('#infoBip_btn').show();
    }

    $('#ddl_workshop').empty();
    $('#ddl_workshop').append('<option value="">--Select--</option>');

    $.ajax({
        url: siteRoot + "/CallLogging/getSmsType/",
        type: 'get',
        data: { val: typeOfMsg },
        dataType: 'json',
        success: function (res) {
            if (res.success) {
                if (res.type == "sms") {
                    $("#ddl_SMSType").empty();
                    $("#ddl_SMSType").append('<option value="">' + "--Select--" + '</option>');
                    if (res.data != null) {
                        for (var i = 0; i < res.data.length; i++) {
                            $("#ddl_SMSType").append('<option value="' + res.data[i].smsId + '">' + res.data[i].smsType + '</option>');
                        }
                    }
                }
                else if (res.type == "whatsApp") {
                    $("#ddl_SMSType").empty();
                    $("#ddl_SMSType").append('<option value="">' + "--Select--" + '</option>');
                    if (res.data != null) {
                        for (var i = 0; i < res.data.length; i++) {
                            $("#ddl_SMSType").append('<option value="' + res.data[i].smsId + '">' + res.data[i].smsType + '</option>')
                        }
                    }
                }
                smsEnableDisableButton();
            }
            else {
                Lobibox.notify("warning", {
                    msg: res.error
                });
            }
        }
    });
}

function getSMSTemplate(id) {
    var typeOfMsg = $("input[name='TypeOfMsg']:checked").val();
    if (typeOfMsg == "SMS" || typeOfMsg == "WhatsAppNormal") {
        smsTypeChanged(id, 'sms');
    }
    else if (typeOfMsg == "WhatsApp") {
        var dealerCode = document.getElementById('PkDealercode').value;

        var selType = $('#' + id + " option:selected").text();
        $('#smstemplate').val('');
        $('#infoBipParams').val('');

        if (dealerCode == "HANSHYUNDAI") {
            if (selType == "camp_message" || selType == "special_service") {

                $('#div_campSpecial,#div_offerEndDate,#bookingDateFrom,#div_dateFrom').show();

                $('.offerCntr').val('');
                if (selType == "camp_message") {
                    $('.offerCntr').attr('placeholder', 'Camp Offer');
                    $('#div_campName').show();
                    $('#div_amount').hide();
                }
                else if (selType == "special_service") {
                    $('.offerCntr').attr('placeholder', 'Special Offer');
                    $('#div_campName').hide();
                    $('#div_amount').show();
                }

                $('#div_bTime,#div_date').hide();
            }
            else {
                if (selType == "not_contacted1" || selType == "n_visit_confirmation1") {
                    $('#div_bTime,#div_date').hide();
                }
                else if (selType == "n_1_booking_confirmation") {
                    $('#div_date').hide();
                    $('#div_bTime').show();
                }
                else {
                    $('#div_bTime,#div_date').show();
                }
                $('#div_campSpecial,#div_campName,#div_amount,#div_offerEndDate,#div_dateFrom').hide();


            }
        }
        else if (dealerCode == "HARPREETFORD") {
            $('#div_campSpecial').hide();

            if (selType == "fresh_booking_01_hf") {
                $('#div_date').show();
                $('#div_bTime').show();
                $('#div_dateFrom').hide();
            }
            else if (selType == "scheduled_service_confirmation_06_hf" || selType == "service_overdue_template_03_hf" || selType == "service_servicedue_template_03_hf_2" || selType == "service_servicedue_ggn" || selType == "service_servicedue_ssi" || selType == "service_servicedue_sbd1" || selType == "service_servicedue_okhla2")
            {
                $('#div_date,#div_bTime,#div_dateFrom').hide();
            }
            else if (selType == "scheduled_service_confirmation_06_hf" || selType == "service_overdue_template_03_hf" || selType == "service_servicedue_template_03_hf_2") {
                $('#div_date,#div_bTime,#div_dateFrom').hide();
            }
            else if (selType == "One_day_before_scheduled_service_02_hf") {
                $('#div_date,#div_dateFrom').hide();
                $('#div_bTime').show();
            }
            else if (selType == "follow_up_on_booking_confirmation_04_hf" || selType == "post_service_feedback_05_hf") {
                $('#div_date,#div_bTime').hide();
                $('#div_dateFrom').show();
            }
        }
        else {
            return false;
        }
    }
}

function workshopChanged(id) {
    var typeOfMsg = $("input[name='TypeOfMsg']:checked").val();
    if (typeOfMsg == "SMS" || typeOfMsg == "WhatsAppNormal") {
        locationChnaged(id, 'sms');
    }
    else if (typeOfMsg == "WhatsApp") {
        return false;
    }

}

function sendMessage() {
    var typeOfMsg = $("input[name='TypeOfMsg']:checked").val();
    var dealerCode = document.getElementById('PkDealercode').value;

    if (typeOfMsg == "SMS") {
        if (dealerCode == "HARPREETFORD") {
            sendTgsSMS();
        }
        else {
            sendSMS();
        }
    }
    else if (typeOfMsg == "WhatsApp") {
        var paraDisct = $('#infoBipParams').val();

        if (paraDisct != "") {

            $.ajax({
                url: siteRoot + "/CallLogging/sendInfoBipWhatsapp/",
                type: 'post',
                data: { paraDisct: paraDisct },
                dataType: 'json',
                success: function (res) {
                    if (res.success) {
                        Lobibox.notify("success", {
                            msg: 'Message sent successfully.'
                        });

                        console.log(res.message);
                        clearInfoBipTags();
                    }
                    else {
                        Lobibox.notify("warning", {
                            msg: res.exception
                        });
                    }
                }
            });
        }
        else {
            Lobibox.notify("warning", {
                msg: 'Message body is empty, cannot sent message.'
            });
        }
    }
}

function clearInfoBipTags() {
    //$("input[name='TypeOfMsg']:checked").prop("checked", false);
    $("#ddl_SMSType").prop("selectedIndex", 0);
    $("#ddl_location").prop("selectedIndex", 0);
    $("#ddl_workshop").prop("selectedIndex", 0);
    $("#smstemplate").val("");
    $("#bookingDate").val("");
    $("#bookingTime").val("");
}

function getTemplateForWhatsapp() {
    var userRole1 = document.getElementById('role1').value;
    var smsTypeId = $("#ddl_SMSType").val();
    var smsTypeText = $("#ddl_SMSType option:selected").text();
    if (smsTypeId != "") {
        var dealerCode = document.getElementById('PkDealercode').value;
        if (dealerCode == "HANSHYUNDAI")
        {
            if (userRole1 == "1") {
                if (smsTypeText == "not_contacted") {
                    if ($('#ddl_location').val() == "") {
                        Lobibox.notify("warning", {
                            msg: 'Please select location'
                        });

                        return false;
                    }
                }
                else if (smsTypeText == "special_service" || smsTypeText == "camp_message") {
                    var offerValid = true;
                    if ($('#bookingDatFrom').val() == "") {
                        Lobibox.notify("warning", {
                            msg: 'Please enter offer start date.'
                        });

                        return false;
                    }

                    if ($('#ddl_location').val() == "") {
                        Lobibox.notify("warning", {
                            msg: 'Please select location'
                        });

                        return false;
                    }

                    if ($('#bookingDateTo').val() == "") {
                        Lobibox.notify("warning", {
                            msg: 'Please enter offer end date'
                        });

                        return false;
                    }

                    $('.offerCntr').each(function () {
                        if ($(this).val() == "") {
                            Lobibox.notify("warning", {
                                msg: 'Please select ' + $(this).attr('id')
                            });

                            if (offerValid == true) {
                                offerValid = false;
                            }
                            return false;
                        }
                    });

                    if (offerValid == false) {
                        Lobibox.notify("warning", {
                            msg: 'Please select offers'
                        });
                    }

                    if (smsTypeText == "special_service") {
                        if ($('#txtAmount').val() == "") {
                            Lobibox.notify("warning", {
                                msg: 'Please enter amount.'
                            });

                            return false;
                        }
                    }
                    else if (smsTypeText == "camp_message") {
                        if ($('#txtCampName').val() == "") {
                            Lobibox.notify("warning", {
                                msg: 'Please enter campaign name.'
                            });

                            return false;
                        }
                    }
                }
                else {

                    if (smsTypeText == "n_visit_confirmation1" || smsTypeText == "not_contacted1") {

                    }
                    else if (smsTypeText == "n_1_booking_confirmation") {
                        if ($('#bookingTime').val() == "") {
                            Lobibox.notify("warning", {
                                msg: 'Please select booking time.'
                            });

                            return false;
                        }
                    }
                    else {
                        if ($('#bookingDate').val() == "") {
                            Lobibox.notify("warning", {
                                msg: 'Please select booking date.'
                            });

                            return false;
                        }

                        if ($('#bookingTime').val() == "") {
                            Lobibox.notify("warning", {
                                msg: 'Please select booking time.'
                            });

                            return false;
                        }
                    }
                    if ($('#ddl_location').val() == "") {
                        Lobibox.notify("warning", {
                            msg: 'Please select location'
                        });

                        return false;
                    }
                }
            }
        }
        else if (dealerCode == "HARPREETFORD") {
            if (userRole1 == "1") {

                if (smsTypeText == "fresh_booking_01_hf") {

                    if ($('#bookingDate').val() == "") {
                        Lobibox.notify("warning", {
                            msg: 'Please select booking date.'
                        });

                        return false;
                    }

                    if ($('#bookingTime').val() == "") {
                        Lobibox.notify("warning", {
                            msg: 'Please select booking time.'
                        });

                        return false;
                    }
                }
                else if (smsTypeText == "One_day_before_scheduled_service_02_hf") {
                    if ($('#bookingTime').val() == "") {
                        Lobibox.notify("warning", {
                            msg: 'Please select booking time.'
                        });

                        return false;
                    }
                }
                else if (smsTypeText == "follow_up_on_booking_confirmation_04_hf" || smsTypeText == "post_service_feedback_05_hf") {
                    if ($('#bookingDatFrom').val() == "") {
                        Lobibox.notify("warning", {
                            msg: 'Please select date.'
                        });

                        return false;
                    }
                }
                if ($('#ddl_location').val() == "") {
                    Lobibox.notify("warning", {
                        msg: 'Please select location'
                    });

                    return false;
                }
            }
        }
        else {
            return false;
        }


        $('#smstemplate').val('');
        $('#infoBipParams').val('');
        $.ajax({
            url: siteRoot + "/CallLogging/getWhatsappTemplate/",
            type: 'post',
            dataType: 'json',
            data: { value: smsTypeId },
            success: function (res) {
                if (res.success) {
                    if (res.smsData != "" && res.smsParams != "") {
                        var template = res.smsData;
                        var params = res.smsParams;

                        var dictParaValue = getParamValue(params);
                        var result = template;
                        for (var key in dictParaValue) {
                            result = result.replace("{{" + key + "}}", dictParaValue[key]);

                            dictParaValue["smsId"] = smsTypeId;
                            dictParaValue["maxPara"] = params.paramCount;
                            dictParaValue["template_name"] = smsTypeText;
                            if ($('#ddl_phone_no option:selected').text() != "") {
                                dictParaValue["custNum"] = $('#ddl_phone_no option:selected').text();
                            }
                            else {
                                Lobibox.notify("warning", {
                                    msg: "No Preffered customer number found"
                                });
                                return false;
                            }
                            dictParaValue["message"] = result;
                            $('#infoBipParams').val(JSON.stringify(dictParaValue));
                        }

                        $('#smstemplate').val(result);
                    }
                }
                else {
                    Lobibox.notify("warning", {
                        msg: res.error
                    });
                }
            }
        });
    }
    else {
        Lobibox.notify("warning", {
            msg: 'Please select sms type.'
        });
    }
}

function getParamValue(params) {
    var dicstPara = {};
    var maxPara = params.paramCount;
    var j = 1;

    for (var i = 1; i <= maxPara; i++) {
        var paraName = params["param" + i];

        if (paraName == "CONTACT_NO") {
            if ($('#ddl_phone_no option:selected').text() != "") {
                dicstPara[i] = $('#ddl_phone_no option:selected').text();
            }
            else {
                dicstPara[i] = "--";
            }
        }
        else if (paraName == "LOCATION_NAME") {
            if ($('#ddl_location').val() != "") {
                dicstPara[i] = $('#ddl_location').val();
            }
            else {
                dicstPara[i] = "--";
            }
        }
        else if (paraName == "MODEL") {
            if ($('#modelNo').text() != "") {
                dicstPara[i] = $('#modelNo').text();
            }
            else {
                dicstPara[i] = "--";
            }
        }
        else if (paraName == "VEHICLE_NO") {
            if ($('#vehRegNO').text() != "") {
                dicstPara[i] = $('#vehRegNO').text();
            }
            else {
                dicstPara[i] = "--";
            }
        }
        else if (paraName == "DATE") {
            if ($('#bookingDate').val() != "") {
                dicstPara[i] = $('#bookingDate').val();
            }
            else {
                dicstPara[i] = "--";
            }
        }
        else if (paraName == "CAMP_STARTING_DATE" || paraName == "STARTING_DATE") {
            if ($('#bookingDatFrom').val() != "") {
                dicstPara[i] = $('#bookingDatFrom').val();
            }
            else {
                dicstPara[i] = "--";
            }
        }
        else if (paraName == "TIME") {
            if ($('#bookingTime').val() != "") {
                dicstPara[i] = $('#bookingTime').val();
            }
            else {
                dicstPara[i] = "--";
            }
        }
        else if (paraName == "CAMPAIGN_NAME") {
            if ($('#txtCampName').val() != "") {
                dicstPara[i] = $('#txtCampName').val();
            }
            else {
                dicstPara[i] = "--";
            }
        }
        else if (paraName == "FOLLOWUP_DATE" || paraName == "FEEDBACK_DATE") {
            if ($('#bookingDatFrom').val() != "") {
                dicstPara[i] = $('#bookingDatFrom').val();
            }
            else {
                dicstPara[i] = "--";
            }
        }
        else if (paraName == "CAMP_ENDING_DATE" || paraName == "ENDING_DATE") {
            if ($('#bookingDateTo').val() != "") {
                dicstPara[i] = $('#bookingDateTo').val();
            }
            else {
                dicstPara[i] = "--";
            }
        }
        else if (paraName.includes('CAMPOFFER')) {
            if ($('#Offer' + j).val() != "") {
                dicstPara[i] = $('#Offer' + j).val();
            }
            else {
                dicstPara[i] = "--";
            }
            j = j + 1;
        }


    }
    return dicstPara;
}

//-----------------------------WhatsApp InfoBip Integration function ----------------------------------- End


//------------- ORAI Whatsapp Integration -----------------------------//

function clearORAIControls() {
    $('#ddl_ORAISMSType').prop('selectedIndex', 0);
    clearfilters();
}
function clearfilters() {
    $('#ORAIsmstemplate').val('');
    $('#txtoraibookingTime').val('');
    $('#txtoraibookingDateTo').val('');
    $('#ORAIddl_workshop').empty();
    $('#ORAIddl_location').prop('selectedIndex', 0);
    $('#ORAIParams').val('');
    dicstPara = {};
    $('#ORAIddl_workshop').append('<option value="">--Select--</option>');

}
function typeOfMsgORAI(ele) {
    clearORAIControls();
    $("#sendMsgORAI").removeAttr("disabled");
    var typeOfMsg = ele.value;
    if (typeOfMsg == "SMS" || typeOfMsg == "WhatsAppNormal") {

        $('#btn_whatsapporai').hide();
        $('#orai_btn').hide();
        $('#ORAIdateTimeDiv').hide();
        if (typeOfMsg == "WhatsAppNormal") {
            typeOfMsg = "SMS";
            $('#btn_whatsapporai').show();
        }

    }
    else if (typeOfMsg == "WhatsApp") {
        $('#orai_btn').show();
        $('#ORAIdateTimeDiv').show();
        $('#btn_whatsapporai').hide();
    }

    $('#ddl_workshop').empty();
    $('#ddl_workshop').append('<option value="">--Select--</option>');

    $.ajax({
        url: siteRoot + "/CallLogging/getSmsType/",
        type: 'get',
        data: { val: typeOfMsg },
        dataType: 'json',
        success: function (res) {
            if (res.success) {
                if (res.type == "sms") {
                    $("#ddl_ORAISMSType").empty();
                    $("#ddl_ORAISMSType").append('<option value="">' + "--Select--" + '</option>');
                    if (res.data != null) {
                        for (var i = 0; i < res.data.length; i++) {
                            $("#ddl_ORAISMSType").append('<option value="' + res.data[i].smsId + '">' + res.data[i].smsType + '</option>');
                        }
                    }
                }
                else if (res.type == "whatsApp") {
                    $("#ddl_ORAISMSType").empty();
                    $("#ddl_ORAISMSType").append('<option value="">' + "--Select--" + '</option>');
                    if (res.data != null) {
                        for (var i = 0; i < res.data.length; i++) {
                            $("#ddl_ORAISMSType").append('<option value="' + res.data[i].smsId + '">' + res.data[i].smsType + '</option>')
                        }
                    }
                }


                var smstypelength = $('#ddl_ORAISMSType > option').length;
                if (smstypelength > 1) {
                    $("#btn_whatsapporai").removeAttr('disabled');
                    $("#sendMsgORAI").removeAttr('disabled');
                }
                else {
                    $("#sendMsgORAI").prop("disabled", true);
                    $("#btn_whatsapporai").prop("disabled", true);

                }

            }
            else {
                Lobibox.notify("warning", {
                    msg: res.error
                });
            }
        }
    });
}


function sendMessageORAI() {
    var typeOfMsg = $("input[name='TypeOfMsgORAI']:checked").val();
    if (typeOfMsg == "SMS") {

        ORAIsendSMS();
    }
    else if (typeOfMsg == "WhatsApp") {
        var paraDisct = $('#ORAIParams').val();

        if (paraDisct != "") {

            $.ajax({
                url: siteRoot + "/CallLogging/sendORAIWhatsapp/",
                type: 'post',
                data: { paraDisct: paraDisct },
                dataType: 'json',
                success: function (res) {
                    if (res.success) {
                        Lobibox.notify("success", {
                            msg: 'Message sent successfully.'
                        });

                        console.log(res.message);
                    }
                    else {
                        Lobibox.notify("warning", {
                            msg: res.exception
                        });
                    }
                    clearORAIControls();

                }
            });
        }
        else {
            Lobibox.notify("warning", {
                msg: 'Message body is empty, cannot sent message.'
            });
        }
    }
}


function workshopChangedORAI(id) {
    $('#ORAIsmstemplate').val('');
    var typeOfMsg = $("input[name='TypeOfMsgORAI']:checked").val();
    if (typeOfMsg == "SMS" || typeOfMsg == "WhatsAppNormal") {
        ORAIlocationChnaged(id, 'sms');
    }
    else if (typeOfMsg == "WhatsApp") {

        var smsTypeText = $("#ddl_ORAISMSType option:selected").text();
        if (smsTypeText.trim() == "service_app_confir") {
            $('#bookingdaeORAIDiv').show();
            $('#oraitimeDiv').hide();
        }
        if (smsTypeText.trim() == "non_contactable") {
            $('#bookingdaeORAIDiv').hide();
            $('#oraitimeDiv').hide();
        }
        if (smsTypeText.trim() == "service_due_reminder") {
            $('#bookingdaeORAIDiv').hide();
            $('#oraitimeDiv').hide();
        }
        if (smsTypeText.trim() == "app_one_day_before") {
            $('#bookingdaeORAIDiv').hide();
            $('#oraitimeDiv').show();
        }
        if (smsTypeText.trim() == "over_due_reminder") {
            $('#bookingdaeORAIDiv').hide();
            $('#oraitimeDiv').hide();
        }
        if (smsTypeText.trim() == "thankyou_sms_after_service") {
            $('#bookingdaeORAIDiv').hide();
            $('#oraitimeDiv').hide();
        }
        if (smsTypeText.trim() == "non_conatact2") {
            $('#bookingdaeORAIDiv').hide();
            $('#oraitimeDiv').hide();
        }
        if (smsTypeText.trim() == "n_day_appointment") {
            $('#bookingdaeORAIDiv').show();
            $('#oraitimeDiv').hide();
        }
        if (smsTypeText.trim() == "for_no_show_category") {
            $('#bookingdaeORAIDiv').hide();
            $('#oraitimeDiv').hide();
        }
        if (smsTypeText.trim() == "service_reminder") {
            $('#bookingdaeORAIDiv').hide();
            $('#oraitimeDiv').hide();
        }
        if (smsTypeText.trim() == "picup_drop") {
            $('#bookingdaeORAIDiv').show();
            $('#oraitimeDiv').show();
        }
        if (smsTypeText.trim() == "non_contact") {
            $('#bookingdaeORAIDiv').hide();
            $('#oraitimeDiv').hide();
        }
        if (smsTypeText.trim() == "insurance_renewable") {
            $('#bookingdaeORAIDiv').hide();
            $('#oraitimeDiv').hide();
        }
        if (smsTypeText.trim() == "due_notif") {
            $('#bookingdaeORAIDiv').hide();
            $('#oraitimeDiv').hide();
        }
        if (smsTypeText.trim() == "camp_sms") {
            $('#bookingdaeORAIDiv').hide();
            $('#oraitimeDiv').hide();
        }
        if (smsTypeText.trim() == "for_picup_drop") {
            $('#bookingdaeORAIDiv').show();
            $('#oraitimeDiv').show();
        }

        return false;
        //getTemplateForWhatsappORAI();
    }

}

//ORAI Template

function ORAIlocationChnaged(ele, msgType) {
    //var phNum = $('#ddl_phone_no option:selected').val();

    if (msgType == "sms") {
        var selectedLocId = $('#' + ele + ' option:selected').val();
        var selectedIdSMSType = $('#ddl_ORAISMSType option:selected').val();
        if (selectedIdSMSType == "") {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please select SMS Type'
            });
            $('#smstemplate').val('');
            return false;
        }

        if (selectedLocId == "") {
            selectedLocId = 0;
        }
        ORAIgetTemplate(selectedIdSMSType, selectedLocId, 'sms');
    }
}

function ORAIgetTemplate(selectedIdSMSType, selectedLocId, msgType) {
    $('#ORAIsmstemplate').empty();
    if (selectedLocId == undefined) {
        selectedLocId = 0;
    }

    $('#mainLoader').fadeIn('slow');
    $.ajax({
        type: 'POST',
        url: siteRoot + "/CallLogging/getTemplateMessage/",
        datatype: 'json',
        //async: false,
        cache: false,
        data: { smsId: selectedIdSMSType, locId: selectedLocId, msgType: msgType },
        success: function (res) {
            if (res.success == true) {

                if (msgType == "sms") {
                    $('#ORAIsmstemplate').val(res.sms);
                }
                else if (msgType == "email") {
                    $('#emailSubject').val(res.emailSub);
                    $('#emailtemplate').val(res.emailTemplate);
                }
            }
            else {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: res.error
                });
            }
            $('#mainLoader').fadeOut('slow');
        },
        error: function (ex) {
            alert(ex);
            $('#mainLoader').fadeOut('slow');
        }
    });
}

function ORAIsendSMS() {
    var phNum = $('#ddl_phone_no option:selected').text();

    var smsType = $('#ddl_ORAISMSType option:selected').val();
    var workshopselected = $('#ORAIddl_workshop option:selected').val();
    var smstemplate = $('#ORAIsmstemplate').val();

    smstemplate = smstemplate.replace(/[\\\#()$~'"*<>{}]/g, ' ');
    if ($('#PkDealercode').val() == "INDUS") {
        if (smsType == "29" || smsType == "30") {
            if (workshopselected == "" || workshopselected == null) {
                workshopselected = "0";
            }
        }

    }

    if ($('#ORAIsmstemplate').val() !== "" && smsType !== "" && workshopselected !== "" && phNum !== "") {

        try {
            $.ajax({
                type: 'POST',
                url: siteRoot + "/CallLogging/sendSMS/",
                datatype: 'json',
                cache: false,
                data: { phNum: phNum, smstemplate: smstemplate },
                success: function (res) {
                    if (res.success == true) {
                        Lobibox.notify('info', {
                            continueDelayOnInactiveTab: true,
                            msg: 'Response Recorded Successfully'
                        });
                    }
                    else {
                        Lobibox.notify('Warning', {
                            continueDelayOnInactiveTab: true,
                            msg: res.error
                        });
                        //alert(res.error);
                    }
                },
                error: function (ex) {
                    alert("Sesrervver Error");
                }
            });
        }
        catch (err) {
            alert(err);
        }


    }
    else {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Make sure phone no, SMS Type and Workshop is selected'
        });
    }

}

function getTemplateForWhatsappORAI() {
    var userRole1 = document.getElementById('role1').value;
    var smsTypeId = $("#ddl_ORAISMSType").val();
    var smsTypeText = $("#ddl_ORAISMSType option:selected").text();

    var workshop = $('#ORAIddl_workshop').val();
    var bookingdate = $('#txtoraibookingDateTo').val();
    var bookingtime = $('#txtoraibookingTime').val();
    if (smsTypeId == "") {
        Lobibox.notify("warning", {
            msg: 'Please select sms type.'
        });
        return false;
    }
    if (smsTypeText.trim() == "service_app_confir" && (workshop == "" || bookingdate == "")) {
        Lobibox.notify("warning", {
            msg: 'Please select Workshop and Booking Date.'
        });
        return false;
    }
    if (smsTypeText.trim() == "non_contactable" && workshop == "") {
        Lobibox.notify("warning", {
            msg: 'Please select Workshop.'
        });
        return false;
    }
    if (smsTypeText.trim() == "service_due_reminder" && workshop == "") {
        Lobibox.notify("warning", {
            msg: 'Please select Workshop.'
        });
        return false;
    }
    if (smsTypeText.trim() == "thankyou_sms_after_service" && workshop == "") {
        Lobibox.notify("warning", {
            msg: 'Please select Workshop.'
        });
        return false;
    }
    if (smsTypeText.trim() == "non_conatact2" && workshop == "") {
        Lobibox.notify("warning", {
            msg: 'Please select Workshop.'
        });
        return false;
    }
    if (smsTypeText.trim() == "service_reminder" && workshop == "") {
        Lobibox.notify("warning", {
            msg: 'Please select Workshop.'
        });
        return false;
    }
    if (smsTypeText.trim() == "app_one_day_before" && (workshop == "" || bookingtime == "")) {
        Lobibox.notify("warning", {
            msg: 'Please select Workshop and Appointment Time.'
        });
        return false;
    }
    if (smsTypeText.trim() == "n_day_appointment" && (bookingdate == "")) {
        Lobibox.notify("warning", {
            msg: 'Please select  Booking Date.'
        });
        return false;
    }
    if (smsTypeText.trim() == "picup_drop" && (bookingdate == "" || bookingtime == "" || workshop == "")) {
        Lobibox.notify("warning", {
            msg: 'Please select workshop ,  Booking Date and Booking Time.'
        });
        return false;
    }
    if (smsTypeText.trim() == "for_picup_drop" && (bookingdate == "" || bookingtime == "" || workshop == "")) {
        Lobibox.notify("warning", {
            msg: 'Please select workshop ,  Booking Date and Booking Time.'
        });
        return false;
    }
    if (smsTypeText.trim() == "due_notif" && (workshop == "")) {
        Lobibox.notify("warning", {
            msg: 'Please select workshop .'
        });
        return false;
    }
    if (smsTypeText.trim() == "camp_sms" && (workshop == "")) {
        Lobibox.notify("warning", {
            msg: 'Please select workshop .'
        });
        return false;
    }

    if (smsTypeId != "") {
        var dealerCode = document.getElementById('PkDealercode').value;

        $('#ORAIsmstemplate').val('');
        $('#infoBipParams').val('');
        $.ajax({
            url: siteRoot + "/CallLogging/getORAIWhatsappTemplate/",
            type: 'post',
            dataType: 'json',
            data: { value: smsTypeId },
            success: function (res) {
                if (res.success) {
                    if (res.smsData != "" && res.smsParams != "") {
                        var template = res.smsData;
                        var params = res.smsParams;

                        var dictParaValue = getORAIParamValue(params);
                        var result = template;
                        for (var key in dictParaValue) {
                            result = result.replace("{{" + key + "}}", dictParaValue[key]);

                            dictParaValue["smsId"] = smsTypeId;
                            dictParaValue["maxPara"] = params.length;
                            dictParaValue["template_name"] = smsTypeText;
                            if ($('#ddl_phone_no option:selected').text() != "") {
                                dictParaValue["custNum"] = $('#ddl_phone_no option:selected').text();
                            }
                            else {
                                Lobibox.notify("warning", {
                                    msg: "No Preffered customer number found"
                                });
                                return false;
                            }
                            dictParaValue["message"] = result;
                            $('#ORAIParams').val(JSON.stringify(dictParaValue));
                        }

                        $('#ORAIsmstemplate').val(result);
                    }
                }
                else {
                    Lobibox.notify("warning", {
                        msg: res.error
                    });
                }
            }
        });
    }
}

function getORAIParamValue(params) {
    var dicstPara = {};
    var maxPara = params.length;
    var j = 1;

    for (var i = 0; i < maxPara; i++) {
        var paraName = params[i].parametername;
        var paramId = params[i].parameterid;
        let splitDateAndTime = $('#hdnscheduleDateTime').val();
        let splittedDateTime = splitDateAndTime.split(" ");
        let splittedDate = splittedDateTime[0];
        let splittedTime = splittedDateTime[1] + ' ' + splittedDateTime[2];


        if (paraName == "CONTACT_NO") {
            if ($('#ddl_phone_no option:selected').text() != "") {
                dicstPara[paramId] = $('#ddl_phone_no option:selected').text();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "CUSTOMER_NO") {
            if ($('#ddl_phone_no option:selected').text() != "") {
                dicstPara[paramId] = $('#ddl_phone_no option:selected').text();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "DRIVER_PHONE_NO") {
            if ($('#driverCal option:selected').text() != "") {
                dicstPara[paramId] = $('#driverCal option:selected').text();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "CUSTOMER_NAME") {
            if ($('#custName').text() != "") {
                dicstPara[paramId] = $('#custName').text().trim();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "DRIVER_NAME") {
            if ($('#driverNameEdit').text() != "") {
                dicstPara[paramId] = $('#driverNameEdit').text().trim();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "MODEL") {
            if ($('#modelNo').text() != "") {
                dicstPara[paramId] = $('#modelNo').text();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "CUSTOMER_ADDRESS") {
            if ($('#hdnCustmoerAddress').val() != "") {
                dicstPara[paramId] = $('#hdnCustmoerAddress').val();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }

        else if (paraName == "VEHICLE_REG_NO" || paraName == "VEHICLE_REG_NO1") {
            if ($('#vehRegNO').text() != "") {
                dicstPara[paramId] = $('#vehRegNO').text();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "BRANCH_NAME") {
            if ($('#PkDealercode').val() != "") {
                dicstPara[paramId] = $('#PkDealercode').val();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "OEM" || paraName == "OEM1") {
            if ($('#PkOEM').val() != "") {
                dicstPara[paramId] = $('#PkOEM').val();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "R_M_CONTACT") {
            if ($('#txtwyzuserphonenum').val() != "") {
                dicstPara[paramId] = $('#txtwyzuserphonenum').val();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "DUE_DATE" || paraName == "SERVICEDUE") {
            if ($('#serviceDueDate').val() != "") {
                dicstPara[paramId] = $('#serviceDueDate').val();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        //else if (paraName == "SERVICEDUE") {
        //    if ($('#nextServDataId').text().trim() != "") {
        //        dicstPara[paramId] = $('#nextServDataId').text().trim();
        //    }
        //    else {
        //        dicstPara[paramId] = "--";
        //    }
        //}

        else if (paraName == "LOCATION_NAME") {
            if (($('#ORAIddl_workshop option:selected').text() != "") && ($('#ORAIddl_workshop option:selected').text() != "--Select--")) {
                dicstPara[paramId] = $('#ORAIddl_workshop option:selected').text();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "SERVICE_TYPE") {
            if ($('#servicetypeorai').text().trim() != "") {
                dicstPara[paramId] = $('#servicetypeorai').text().trim();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "APPOINTMENT_DATE") {
            let splitDate = $('#appointmentDate').val();
            let splittedDate = splitDate.split(" ");
            let splitted = splittedDate[0];
            if (splitted != "") {
                dicstPara[paramId] = splitted;
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "APPOINTMENT_TIME") {
            if ($('#appointmentTimes').val() != "") {
                dicstPara[paramId] = $('#appointmentTimes').val();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "SCHEDULE_DATE") {
            if ($('#hdnscheduleDateTime').val() != "") {
                dicstPara[paramId] = splittedDate;
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "SCHEDULE_TIME") {
            if ($('#hdnscheduleDateTime').val() != "") {
                dicstPara[paramId] = splittedTime;
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "POLICY_EXPR_DATE") {
            let splitDate = $('#hdnPolicyExprDate').val();
            let splittedDate = splitDate.split(" ");
            let splitted = splittedDate[0];
            if (splitted != "") {
                dicstPara[paramId] = splitted;
            }
            else {
                dicstPara[paramId] = "--";
            }
        }


        else if (paraName == "PDR_NAME") {
            if ($('#custName').text() != "") {
                dicstPara[i + 1] = $('#custName').text().trim();
            }
            else {
                dicstPara[i + 1] = "--";
            }
        }
        else if (paraName == "DEALER") {
            if ($('#PkDealercode').val() != "") {
                dicstPara[paramId] = $('#PkDealercode').val();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "CRM_NAME") {
            if ($('#txtwyzuserfirstname').val() != "") {
                dicstPara[paramId] = $('#txtwyzuserfirstname').val();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "CRE_NO" || paraName == "CRE_NO1" || paraName == "CRE_NO2") {
            if ($('#txtwyzuserphonenum').val() != "") {
                dicstPara[paramId] = $('#txtwyzuserphonenum').val();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "CRE_MANAGER_NO" || paraName == "CRE_MANAGER_NO1") {
            if ($('#txtwyzuserManagerphonenum').val() != "") {
                dicstPara[paramId] = $('#txtwyzuserManagerphonenum').val();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }

        else if (paraName == "APP_DATE_TIME") {
            if ($('#txtoraibookingDateTo').val() != "" && $('#txtoraibookingTime').val() != "") {
                var dateapp = $('#txtoraibookingDateTo').val();
                var timeapp = $('#txtoraibookingTime').val();
                dicstPara[paramId] = dateapp + "," + timeapp;
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "DRIVER_DETAILS") {
            var servicebookedId = $('#servicebook_id').val();

            $.when(getpickupdriverdetails(servicebookedId)).then
                (function successHandler(res) {
                    if (res.success == true) {
                        dicstPara[paramId] = res.drivername + "," + res.driverphonenumber;

                    }
                    else if (res.success == false) {
                        dicstPara[paramId] = "--";

                    }
                })

        }

        else if (paraName == "POLICY_DUE_DATE") {
            if ($('#policyduedateINS').text().trim() != "") {
                dicstPara[paramId] = $('#policyduedateINS').text().trim();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "POLICY_DUE_MONTH") {
            if ($('#policyduedateINS').text().trim() != "") {
                var datemonth = $('#policyduedateINS').text().trim()

                var date = datemonth.split("-");;
                var date1 = new Date(date[2] + " " + date[1] + " " + date[0]);
                date1.toLocaleDateString("en-GB");

                var objDate = new Date(date1);

                var strDate = objDate.toLocaleString("en", { month: "long" });
                dicstPara[paramId] = strDate;
            }
            else {
                dicstPara[paramId] = "--";
            }
        }

        else if (paraName == "online_link") {

            dicstPara[paramId] = "https://www.mercedes-benz.co.in/passengercars/being-an-owner/service-booking/oab.module.html/oab.html";

        }


    }
    return dicstPara;
}

function getpickupdriverdetails(servicebookedId) {
    return $.ajax({
        url: siteRoot + '/CallLogging/getdriverdetails/',
        type: "post",
        async: false,
        data: { servicebookedId: servicebookedId },
    });
}

//Post SAles Pullouts
function getPostsalesDetails(cubeId) {
    if (postsalesCampaignId != 0) {
        $('#postsalesday_' + postsalesCampaignId).addClass('main-content');
        $('#postsalesday_' + postsalesCampaignId).removeClass('main-content-selected');
    }

    $('.pcontent').hide();
    $("#postsalesfeedbackicon").hide();

    $('#postsalesday_' + cubeId).removeClass('main-content');
    $('#postsalesday_' + cubeId).addClass('main-content-selected');

    postsalesCampaignId = cubeId;
    $('.pcontent').show();
    $("#postsalesfeedbackicon").show();

    $.ajax({
        url: siteRoot + "/CallLoggingPSF/getPostsalesPullOuts/",
        cache: false,
        type: 'post',
        dataType: 'json',
        data: { cubeId: cubeId },
        success: function (res) {

            if (res.success == true) {

                $('#psoutlet').text(res.data.salesoutlet);
                $('#pschassis').text(res.data.chassisNo);
                $('#psdeliverydate').text(parseJsonDateTime(res.data.saleDate));
                $('#psdsename').text(res.data.DSE);
                $('#pscrename').text(res.data.assignedCRE);
                $('#psldd').text(parseJsonDateTime(res.data.updatedDate));
                $('#creps').text(res.data.Remarks);
                $('#cusps').text(res.data.Comments);
                $('.PostSalesDispoContainer').empty();
                $('.PostSalesDispoContainer').show();

                if (res.data.calldispositiondata_id == 22) {
                    $('#overallfeedSatps').text('Satisfied');
                    $('#overallfeedSatps').css({ 'background-color': '#c6e8c6' });
                    $('#satIconps').show();
                    $('#disSatIconps').hide();
                    $('#overallfeedDisSatps').hide();
                    $('#overallfeedSatps').show();
                }
                else if (res.data.calldispositiondata_id == 44) {
                    $('#overallfeedDisSatps').text('Dissatisfied');
                    $('#overallfeedDisSatps').show();

                    $('#overallfeedDisSatps').css({ 'background-color': '#e8c6c6' });
                    $('#disSatIconps').show();
                    $('#satIconps').hide();
                    $('#overallfeedSatps').hide();
                }
                else {

                }
                var dict = {};

                for (i = 0; i < res.psf_questions.length; i++) {
                    var answer = res.data[res.psf_questions[i].binding_var];

                    if (res.psf_questions[i].display_type == "radio-button") {
                        var displayAnswers = res.psf_questions[i].radio_values.split(",");
                        var displayOptions = res.psf_questions[i].radio_options.split(",");
                    }
                    for (j = 0; j < displayAnswers.length; j++) {
                        dict[displayAnswers[j]] = displayOptions[j];
                    }
                    var cls;
                    var value;
                    if (dict[answer] != null && dict[answer] != "" && dict[answer] != undefined) {
                        value = dict[answer].toUpperCase();

                    }
                    else {
                        value = "-";
                    }

                    var questno = 'q' + res.psf_questions[i].question_no;
                    if (questno == 'q25' || questno == 'q16') {
                        value = answer;
                    }

                    if (value.includes("POOR") || value.includes("FAIR") || value.includes("GOOD") || value.includes("V.GOOD") || value.includes("NO")) {
                        cls = "background-color:#e8c6c6;";
                    }
                    else {
                        cls = "background-color:#c6e8c6;";
                    }

                    $(".PostSalesDispoContainer").append('<br/><div  class="col-md-8 col-sm-8-col-xs-12 psques">' + res.psf_questions[i].question + '</div><div class="col-md-4 col-sm-4 col-xs-12 "style=' + cls + '>' + value
                        + '</div></br>');

                }




            }
            else {
                $('.PostSalesDispoContainer').hide();

                Lobibox.notify('warning', function () {
                    msg: res.exception
                });
            }

        },
        error: function (ex) {
            alert(ex);
            console.log(ex);
        }
    });
}


function opendynamicEmailDetails() {


    var toEmail = $.trim($('#ddl_email').text());


    $('#txtdynamicemailto').val(toEmail);

    var emailId = $('#ddlemailTypedynamic').val();
    if (emailId != "") {
        $('#mainLoader').fadeIn('slow');

        $.ajax({
            type: 'POST',
            url: siteRoot + "/CallLogging/getDynamicEmail/",
            datatype: 'json',
            cache: false,
            data: { emailId: emailId },
            success: function (res) {
                if (res.success == true) {

                    $('#txtdynamicemailCC').val(res.ccemails);
                    $('#txtdynamicemailSubject').val(res.emailSub);
                    $('#txtdynamicemailtemplate').val(res.emailTemplate);
                    $('#mainLoader').fadeOut('slow');

                }
                else {
                    $('#mainLoader').fadeOut('slow');

                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: res.error
                    });
                }
            },
            error: function (ex) {
                $('#mainLoader').fadeOut('slow');
            }
        });
    }
}

$('#btndynamicEmailSubmit').click(function () {

    var toEmail = $('#txtdynamicemailto').val();
    var emailSub = $('#txtdynamicemailSubject').val();
    var emailTemplate = $('#txtdynamicemailtemplate').val();
    if (toEmail == "") {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please Enter Customer Email'
        });
        return false;
    }
    if (emailSub == "") {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please Enter Email Subject'
        });
        return false;
    }
    if (emailTemplate == "") {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please Enter Email Content'
        });
        return false;
    }
})

//email success
function dynamicemailSuccess(res) {
    if (res.success == true) {
        Lobibox.notify('info', {
            msg: res.error
        });
    }
    else if (res.success == false) {
        Lobibox.notify('warning', {
            msg: res.error
        });
    }
    $('#txtdynamicemailSubject').val('');
    $('#txtdynamicemailCC').val('');
    $('#txtdynamicemailtemplate').val('');
    $('#fileUploaderControl').val('');
    $('#dynamicemailDetails').modal("hide");
    $('#mainLoader').fadeOut('slow');
    $("#ddlemailTypedynamic").prop("selectedIndex", 0);
}

//email failure
function dynamicemailFailure(error) {

    $('#myModalEmailPopUp').modal("hide");
    Lobibox.notify('warning', {
        msg: "Server error, failed to sent email."
    });
    $('#mainLoader').fadeOut('slow');
}

function smsEnableDisableButton() {
    var smstypelength = $('#ddl_SMSType > option').length;
    if (smstypelength > 1) {

        $("#btn_whatsapporai").removeAttr('disabled');
        $("#sendMsgORAI").removeAttr('disabled');
        $("#whatsapp").removeAttr('disabled');
        $("#sendMsg").removeAttr('disabled');
        $("#smsSendbtn").removeAttr('disabled');
        $("#btn_whatsapp").removeAttr('disabled');

    }
    else {

        $("#sendMsg").prop("disabled", true);
        $("#btn_whatsapp").prop("disabled", true);
        $("#smsSendbtn").prop("disabled", true);
        $("#whatsapp").prop("disabled", true);
        $("#sendMsgORAI").prop("disabled", true);
        $("#btn_whatsapporai").prop("disabled", true);

    }
}

//sukumani service history
function getserviceHistoryDetails(jobCardNumber, vehicleId) {
    var dataTableserviceDetails = $('#tbllabourDetailsService').DataTable({
        "destroy": true,
        "ajax": {
            url: siteRoot + "/CallLogging/getLabourserviceHistory/",
            "type": "POST",
            "datatype": "json",
            data: { jobCardNumber: jobCardNumber, vehicleId: vehicleId},
        },
        "columns": [
            { "data": "itemgroupname", "name": "itemgroupname" },
            { "data": "partcodelabourcode", "name": "partcodelabourcode" },
            { "data": "partdescriptionlabourdescription", "name": "partdescriptionlabourdescription" }
        ],
        columnDefs: [{
            targets: "_all",
            orderable: false
        }],
        responsive: true,
        "initComplete": function (data) {
            $('#tblservicehistoryerror').text(data.json.exception);
        },
        "processing": "true",
        "bFilter": false,
        "bLengthChange": false,
        "sorting": "false",
        "paging": "true",
        "language": {
            "processing": "Loading Please Wait.....!"
        },
        order: [],
        pageLength: 5
    });
}

// interaction view more

function AssignmentHistoryOfCustomer_viewmore(typeIs) {

    var dataTableAssgnmntHstry = $('#tblinsassignviewmore').DataTable({
        "destroy": true,
        "ajax": {
            url: siteRoot + "/CallLogging/getAssignmentHistoryOfVehicleId_viewmore/",
            "type": "POST",
            "datatype": "json",
            data: { moduleType: typeIs },
        },
        "columns": [
            { "data": "assignedId", "name": "assignedId" },
            {
                "data": "assignDate", render: function (data, type, row) {
                    return parseJsonDateTime(data)
                }
            },
            { "data": "reason", "name": "reason" },
            { "data": "WyzuserName", "name": "WyzuserName" },
            { "data": "smsType", "name": "smsType" },
            { "data": "serviceTypes", "name": "serviceTypes" },
            { "data": "smsMessage", "name": "smsMessage" },
            { "data": "reassign", "name": "reassign" },
            { "data": "reassignhistory", "name": "reassignhistory" },
            { "data": "resonforDrop", "name": "resonforDrop" },

        ],
        columnDefs: [{
            targets: "_all",
            orderable: false
        }],
        responsive: true,
        "initComplete": function (data) {
            $('#tblinsassignerror').text(data.json.exception);

            if (typeIs == 'insurance') {
                dataTableAssgnmntHstry.columns(5).visible(false)
            }
        },
        "processing": "true",
        "bFilter": false,
        "bLengthChange": false,
        "sorting": "false",
        "paging": "true",
        "language": {
            "processing": "Loading Please Wait.....!"
        },
        order: [],
        pageLength: 5
    });
}


function callHistoryByVehicle_viewmore() {
    typeIs = $('#hdndispohisttype').val();
    var codePath = window.location.href;
    var callIndex = codePath.indexOf('CallLogging');
    var callPath = codePath.substr(callIndex, (codePath.length - 1))
    codePath = codePath.replace(callPath, '');

    var dataTableCallDispo = $('#tblsmrviewmore').DataTable({
        "destroy": true,
        "ajax": {
            "url": siteRoot + "/CallLogging/getCallHistory_viewmore/",
            "type": "POST",
            "datatype": "json",
            "data": { typeIs: typeIs },
        },
        "columns": [
            { "data": "AssignId", "name": "AssignId" },
            { "data": "jobCardNumber", "name": "jobCardNumber" },

            { "data": "CallDate", "name": "CallDate" },
            { "data": "Time", "name": "Time" },
            { "data": "CreId", "name": "CreId" },

            { "data": "Campaign", "name": "Campaign" },
            { "data": "ServiveType", "name": "ServiveType" },
            { "data": "SecondaryDispo", "name": "SecondaryDispo" },
            { "data": "Details", "name": "Details" },
            { "data": "CreRemarks", "name": "CreRemarks" },
            { "data": "Feedback", "name": "Feedback" },
            { "data": "CallMadeType", "name": "CallMadeType" },
            {
                "data": "IsCallInitiated", render: function (data, type, row) {
                    if (data == "") {
                        return "Not Initiated";
                    }
                    else {
                        return data;
                    }
                }
            },
            { "data": "gsmAndroid", "name": "gsmAndroid" },
            { "data": "DailedNo", "name": "DailedNo" },
            {
                "data": "FilePath", render: function (data, type, row) {
                    var filePath = "";
                    if (data != null && data != undefined && data.includes('http:')) {
                        var startIndex = data.indexOf('http');
                        filePath = data.substring(startIndex, data.length);
                    }
                    else if (data != null && data != undefined && data.includes('https:')) {
                        var startIndex = data.indexOf('https');
                        filePath = data.substring(startIndex, data.length);
                    }
                    else {
                        filePath = siteRoot + "/" + data;
                    }

                    return `<audio controls="" src="${filePath}"></audio><br/>`;
                }
            },
            {
                "data": "FilePath", render: function (data, type, row) {
                    var filePath = "";
                    if (data != null && data != undefined && data.includes('http:')) {
                        var startIndex = data.indexOf('http');
                        filePath = data.substring(startIndex, data.length);
                    }
                    else if (data != null && data != undefined && data.includes('https:')) {
                        var startIndex = data.indexOf('https');
                        filePath = data.substring(startIndex, data.length);
                    }
                    else {
                        filePath = siteRoot + "/" + data;
                    }

                    return `<a href="${filePath}" download><i class="fa fa-download" data-toggle="tooltip"></i></a>`;
                }
            }
            //{ "data": "CallType", "name": "CallType" }

        ],
        responsive: true,
        "initComplete": function (data) {
            $('#tblsmrviewmoreerror').text(data.json.exception);
            // filtercountDisplay();
            if (typeIs == "PSF") {
                dataTableCallDispo.columns(1).visible(true)
            }
            else {
                dataTableCallDispo.columns(1).visible(false)

            }

            if (typeIs == 'PSF' || typeIs == 'INS') {
                dataTableCallDispo.columns(6).visible(false)
            }
            $('.dataTables_length').hide();
            $('.dataTables_filter').hide();

            //$('.dataTable').wrap('<div class="dataTables_scroll" />');
        },
        "serverSide": "true",
        "processing": "true",
        "serverSide": "true",
        "sorting": "false",
        //"scrollX": "true",
        //"scrollY": "300",
        "paging": "true",
        //"searching": "true",
        "language": {
            "processing": "Loading Please Wait.....!"
        },
        order: [],
        pageLength: 5
    });
}

function clearaddInfoDetails() {
    $("#customer_lead_tag").multiselect("clearSelection");
    $("#dob").val('');
    $("#anniversary_date").val('');
    $("#dateStarttime").val('');
    $("#dateEndtime").val('');
}

$("#addBtnID").click(function () {
    clearaddInfoDetails();
})

function blockSpecialChar(e) {
    var k = e.keyCode;
    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || (k >= 48 && k <= 57));
}

$("#btneditckyc").click(function () {
    $("#ddleditkyc").focus();
    $("#ddleditkyc").select();
    $("#ddleditkyc").prop("readonly", false);
})
$("#btneditpan").click(function () {
    $("#ddleditpan").focus();
    $("#ddleditpan").select();
    $("#ddleditpan").prop("readonly", false);
})
$("#pn_save").click(function () {
    $("#ddleditpan").prop("readonly", true);
    $("#ddleditkyc").prop("readonly", true);
})

//TGS
function getSMSParamValue(params) {
    var dicstPara = {};
    var maxPara = params.length;
    var j = 1;

    for (var i = 0; i < maxPara; i++) {
        var paraName = params[i].parametername;
        var paramId = params[i].originalparam;
        let splitDateAndTime = $('#hdnscheduleDateTime').val();
        let splittedDateTime = splitDateAndTime.split(" ");
        let splittedDate = splittedDateTime[0];
        let splittedTime = splittedDateTime[1] + ' ' + splittedDateTime[2];


        if (paraName == "CONTACT_NO") {
            if ($('#ddl_phone_no option:selected').text() != "") {
                dicstPara[paramId] = $('#ddl_phone_no option:selected').text();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "CUSTOMER_NO") {
            if ($('#ddl_phone_no option:selected').text() != "") {
                dicstPara[paramId] = $('#ddl_phone_no option:selected').text();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "DRIVER_PHONE_NO") {
            if ($('#driverCal option:selected').text() != "") {
                dicstPara[paramId] = $('#driverCal option:selected').text();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "CUSTOMER_NAME") {
            if ($('#custName').text() != "") {
                dicstPara[paramId] = $('#custName').text().trim();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "DRIVER_NAME") {
            if ($('#driverNameEdit').text() != "") {
                dicstPara[paramId] = $('#driverNameEdit').text().trim();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "MODEL") {
            if ($('#modelNo').text() != "") {
                dicstPara[paramId] = $('#modelNo').text();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "CUSTOMER_ADDRESS") {
            if ($('#hdnCustmoerAddress').val() != "") {
                dicstPara[paramId] = $('#hdnCustmoerAddress').val();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }

        else if (paraName == "VEHICLE_REG_NO" || paraName == "VEHICLE_REG_NO1") {
            if ($('#vehRegNO').text() != "") {
                dicstPara[paramId] = $('#vehRegNO').text();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "BRANCH_NAME") {
            if ($('#PkDealercode').val() != "") {
                dicstPara[paramId] = $('#PkDealercode').val();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "OEM" || paraName == "OEM1") {
            if ($('#PkOEM').val() != "") {
                dicstPara[paramId] = $('#PkOEM').val();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "R_M_CONTACT") {
            if ($('#txtwyzuserphonenum').val() != "") {
                dicstPara[paramId] = $('#txtwyzuserphonenum').val();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "DUE_DATE" || paraName == "SERVICEDUE") {
            if ($('#serviceDueDate').val() != "") {
                dicstPara[paramId] = $('#serviceDueDate').val();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        //else if (paraName == "SERVICEDUE") {
        //    if ($('#nextServDataId').text().trim() != "") {
        //        dicstPara[paramId] = $('#nextServDataId').text().trim();
        //    }
        //    else {
        //        dicstPara[paramId] = "--";
        //    }
        //}

        else if (paraName == "LOCATION_NAME") {
            if (($('#ORAIddl_workshop option:selected').text() != "") && ($('#ORAIddl_workshop option:selected').text() != "--Select--")) {
                dicstPara[paramId] = $('#ORAIddl_workshop option:selected').text();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "SERVICE_TYPE") {
            if ($('#servicetypeorai').text().trim() != "") {
                dicstPara[paramId] = $('#servicetypeorai').text().trim();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "APPOINTMENT_DATE") {
            let splitDate = $('#appointmentDate').val();
            let splittedDate = splitDate.split(" ");
            let splitted = splittedDate[0];
            if (splitted != "") {
                dicstPara[paramId] = splitted;
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "APPOINTMENT_TIME") {
            if ($('#appointmentTimes').val() != "") {
                dicstPara[paramId] = $('#appointmentTimes').val();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "SCHEDULE_DATE") {
            if ($('#hdnscheduleDateTime').val() != "") {
                dicstPara[paramId] = splittedDate;
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "SCHEDULE_TIME") {
            if ($('#hdnscheduleDateTime').val() != "") {
                dicstPara[paramId] = splittedTime;
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "POLICY_EXPR_DATE") {
            let splitDate = $('#hdnPolicyExprDate').val();
            let splittedDate = splitDate.split(" ");
            let splitted = splittedDate[0];
            if (splitted != "") {
                dicstPara[paramId] = splitted;
            }
            else {
                dicstPara[paramId] = "--";
            }
        }


        else if (paraName == "PDR_NAME") {
            if ($('#custName').text() != "") {
                dicstPara[i + 1] = $('#custName').text().trim();
            }
            else {
                dicstPara[i + 1] = "--";
            }
        }
        else if (paraName == "DEALER") {
            if ($('#PkDealercode').val() != "") {
                dicstPara[paramId] = $('#PkDealercode').val();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "CRM_NAME") {
            if ($('#txtwyzuserfirstname').val() != "") {
                dicstPara[paramId] = $('#txtwyzuserfirstname').val();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "CRE_NO" || paraName == "CRE_NO1" || paraName == "CRE_NO2") {
            if ($('#txtwyzuserphonenum').val() != "") {
                dicstPara[paramId] = $('#txtwyzuserphonenum').val();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "CRE_MANAGER_NO" || paraName == "CRE_MANAGER_NO1") {
            if ($('#txtwyzuserManagerphonenum').val() != "") {
                dicstPara[paramId] = $('#txtwyzuserManagerphonenum').val();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }

        else if (paraName == "APP_DATE_TIME") {
            if ($('#txtoraibookingDateTo').val() != "" && $('#txtoraibookingTime').val() != "") {
                var dateapp = $('#txtoraibookingDateTo').val();
                var timeapp = $('#txtoraibookingTime').val();
                dicstPara[paramId] = dateapp + "," + timeapp;
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "DRIVER_DETAILS") {
            var servicebookedId = $('#servicebook_id').val();

            $.when(getpickupdriverdetails(servicebookedId)).then
                (function successHandler(res) {
                    if (res.success == true) {
                        dicstPara[paramId] = res.drivername + "," + res.driverphonenumber;

                    }
                    else if (res.success == false) {
                        dicstPara[paramId] = "--";

                    }
                })

        }

        else if (paraName == "POLICY_DUE_DATE") {
            if ($('#policyduedateINS').text().trim() != "") {
                dicstPara[paramId] = $('#policyduedateINS').text().trim();
            }
            else {
                dicstPara[paramId] = "--";
            }
        }
        else if (paraName == "POLICY_DUE_MONTH") {
            if ($('#policyduedateINS').text().trim() != "") {
                var datemonth = $('#policyduedateINS').text().trim()

                var date = datemonth.split("-");;
                var date1 = new Date(date[2] + " " + date[1] + " " + date[0]);
                date1.toLocaleDateString("en-GB");

                var objDate = new Date(date1);

                var strDate = objDate.toLocaleString("en", { month: "long" });
                dicstPara[paramId] = strDate;
            }
            else {
                dicstPara[paramId] = "--";
            }
        }

        else if (paraName == "online_link") {

            dicstPara[paramId] = "https://www.mercedes-benz.co.in/passengercars/being-an-owner/service-booking/oab.module.html/oab.html";

        }


    }
    return dicstPara;
}

function sendTgsSMS() {
    var phNum = $('#ddl_phone_no option:selected').text();

    var smsType = $('#ddl_SMSType option:selected').val();
    var workshopselected = $('#ddl_workshop option:selected').val();
    var smstemplate = $('#smstemplate').val();

    smstemplate = smstemplate.replace(/[\\\#()$~'"*<>{}]/g, ' ');
    if ($('#smstemplate').val() !== "" && smsType !== "" && workshopselected !== "" && phNum !== "") {
        $.when(getTgsSmsTemplate(smsType)).then
            (function successHandler(res) {
                if (res.success == true) {
                    let result = getSMSParamValue(res.smsParams);
                    CallTgsSmsAPI(result, smsType, phNum, smstemplate);
                }

            })


    }
    else {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Make sure phone no, SMS Type and Workshop is selected'
        });
    }

}

function CallTgsSmsAPI(parameterList, smsId, phNum, templateMessage) {

    try {
        $.ajax({
            type: 'POST',
            url: siteRoot + "/CallLogging/TgsSms/",
            datatype: 'json',
            cache: false,
            data: { parameterList, smsId, phNum, templateMessage },
            success: function (res) {
                if (res.success == true) {
                    Lobibox.notify('info', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Response Recorded Successfully'
                    });
                }
                else {
                    Lobibox.notify('Warning', {
                        continueDelayOnInactiveTab: true,
                        msg: res.error
                    });
                    //alert(res.error);
                }
            },
            error: function (ex) {
                alert("Sesrervver Error");
            }
        });
    }
    catch (err) {
        alert(err);
    }

}

function getTgsSmsTemplate(smsId) {
    return $.ajax({
        url: siteRoot + '/CallLogging/getTgsSmsTemplate/',
        type: "post",
        async: false,
        data: { smsId: smsId },
    });
}
