
//var dates = $("#insdate1234567").datepicker({

//    dateFormat: 'yy-mm-dd',
//    maxDate: "+30d",
//    minDate: 0,
//    onSelect: function (selectedDate) {
//        var option = this.id == "insdate1234567" ? "minDate" : "maxDate",
//            instance = $(this).data("datepicker"),
//            date = $.datepicker.parseDate(instance.settings.dateFormat || $.datepicker._defaults.dateFormat, selectedDate, instance.settings);
//        dates.not(this).datepicker("option", option, date);
//    }

//});


//$("#nextToCustomerDrivekalyani").click(function () {

//    var vVhicle = $("#vehicle").val();
//    var vRenewalTypeID = $("#renewalTypeID").val();
//    var vRenewalModeID = $("#renewalModeID").val();
//    var vPaymentTypeID = $("#paymentTypeID").val();
//    var vTypeOfPickup = $("input[name='appointbooked.typeOfPickup']:checked").val();
//    var vAddressMSSId = $("#AddressMSSId").val();
//    var vdiscountID = $("#discount").val();
//    var vInsdate12345 = $("#insdate12345").val();
//    var onlineTime = $('#appointmentTime').val();
//    var purposeId = $('#PurposeID').val();
//    var picodeId = $('#pincodeId').val();
//    var fieldloc = $('#OtherLocation').val();

//    var vaddressLenght = 0;

//    if (vAddressMSSId != null) {
//        vaddressLenght = vAddressMSSId.length;
//    }
//    var vSchedule = false;

//    if (vTypeOfPickup == "Online") {
//        vSchedule = true;

//        if (vInsdate12345 == "" || vInsdate12345 == null) {
//            Lobibox.notify('warning', {
//                continueDelayOnInactiveTab: true,
//                msg: 'Please Provide Appointment Date.'
//            });
//            return false;
//        }
//        if (onlineTime == "" || onlineTime == null) {
//            Lobibox.notify('warning', {
//                continueDelayOnInactiveTab: true,
//                msg: 'Please Provide Appointment Time.'
//            });
//            return false;
//        }

//    }
//    else if (vTypeOfPickup == "Field") {

//        if (purposeId == 0 || purposeId == null) {
//            Lobibox.notify('warning', {
//                continueDelayOnInactiveTab: true,
//                msg: 'Please Provide Purpose of Visit.'
//            });
//            return false
//        }
//        else if (vaddressLenght == 0) {
//            Lobibox.notify('warning', {
//                continueDelayOnInactiveTab: true,
//                msg: 'Please Select Address.'
//            });
//            return false
//        }
//        else if (picodeId == "" || purposeId == null) {
//            Lobibox.notify('warning', {
//                continueDelayOnInactiveTab: true,
//                msg: 'Please Provide Pincode.'
//            });
//            return false
//        }

//        else if (fieldloc == "" || fieldloc == null) {
//            Lobibox.notify('warning', {
//                continueDelayOnInactiveTab: true,
//                msg: 'Please Provide Field Location.'
//            });
//            return false
//        }

//        else if (vInsdate12345 == "") {
//            Lobibox.notify('warning', {
//                continueDelayOnInactiveTab: true,
//                msg: 'Please Provide Appointment Date.'
//            });
//            return false

//        }
//        else if (onlineTime == "") {
//            Lobibox.notify('warning', {
//                continueDelayOnInactiveTab: true,
//                msg: 'Please Provide Appointment Time.'
//            });
//            return false
//        }
//        else {
//            vSchedule = true;
//        }

//    }

//    else if (vTypeOfPickup == "Walk-in") {
//        var vWalkinLocation = $("#walkinLocation").val();
//        if (vWalkinLocation != "") {
//            var vInsdate12345 = $("#insdate12345").val();
//            if (vInsdate12345 != "") {
//                var vAppointmentTime = $("#appointmentTime").val();
//                if (vAppointmentTime != "") {
//                    var vInsuranceAgentDataId = $("#insuranceAgentDataId").val();
//                    if (vInsuranceAgentDataId != 0) {
//                        vSchedule = true;
//                    }
//                }
//            }
//        }
//    }
//    if (vInsdate12345 != '' && vSchedule && vVhicle != "" && vRenewalTypeID != "" && vRenewalModeID != "0" && vPaymentTypeID != "0" && vdiscountID != "" && vTypeOfPickup) {
//        $("#CustomerDriveInDiv").show();
//        $("#serviceBookDiv").hide();
//        $("#SMRInteractionFirst").hide();
//        $("#DidYouTalkDiv").hide();
//        $("#whatDidCustSayDiv").hide();

//    }

//    else {

//        if (vVhicle == '0' || vVhicle == 'null') {
//            Lobibox.notify('warning', {
//                continueDelayOnInactiveTab: true,
//                msg: 'Please Select Vehicle.'
//            });
//            return false;
//        }

//        if (vRenewalTypeID == "" || vRenewalTypeID == null) {
//            Lobibox.notify('warning', {
//                continueDelayOnInactiveTab: true,
//                msg: 'Please Select Renewal Type.'
//            });
//            return false;
//        }
//        if (vRenewalModeID == "0" || vRenewalModeID == null) {
//            Lobibox.notify('warning', {
//                continueDelayOnInactiveTab: true,
//                msg: 'Please Select Renewal Mode.'
//            });
//            return false;
//        }
//        if (vPaymentTypeID == "0" || vPaymentTypeID == null) {
//            Lobibox.notify('warning', {
//                continueDelayOnInactiveTab: true,
//                msg: 'Please Select Payment Mode.'
//            });
//            return false;
//        }
//        if (vdiscountID == "" || vdiscountID == null) {
//            Lobibox.notify('warning', {
//                continueDelayOnInactiveTab: true,
//                msg: 'Please Enter Discount Value.'
//            });
//            return false;
//        }


//        if (!vTypeOfPickup) {
//            Lobibox.notify('warning', {
//                continueDelayOnInactiveTab: true,
//                msg: 'Please Select Appt Mode.'
//            });
//            return false
//        }

//        if (vTypeOfPickup == "Field" && vaddressLenght <= 10) {
//            Lobibox.notify('warning', {
//                continueDelayOnInactiveTab: true,
//                msg: 'Please Provide Valid Address.'
//            });
//            return false
//        }

//        if (vTypeOfPickup == "Walk-in" && !vSchedule) {
//            var vWalkinLocation = $("#walkinLocation").val();
//            var vInsdate12345 = $("#insdate12345").val();
//            var vAppointmentTime = $("#appointmentTime").val();
//            var vInsuranceAgentDataId = $("#insuranceAgentDataId").val();
//            if (vWalkinLocation = "" || vWalkinLocation == null || vWalkinLocation == "--Select--") {
//                Lobibox.notify('warning', {
//                    continueDelayOnInactiveTab: true,
//                    msg: 'Please Provide Walk-in Location.'
//                });
//                return false

//            }
//            else if (vInsdate12345 == "") {
//                Lobibox.notify('warning', {
//                    continueDelayOnInactiveTab: true,
//                    msg: 'Please Provide Appointment Date.'
//                });
//                return false
//            }
//            else if (vAppointmentTime == "") {
//                Lobibox.notify('warning', {
//                    continueDelayOnInactiveTab: true,
//                    msg: 'Please Provide Appointment Time.'
//                });
//                return false
//            }
//            else if (vInsuranceAgentDataId == 0) {
//                Lobibox.notify('warning', {
//                    continueDelayOnInactiveTab: true,
//                    msg: 'Please Provide FSE.'
//                });
//                return false
//            }
//        }
//    }

//});
//$("input[name$='listingForm.dispoCustAns']").click(function () {
//    varWhatdidSay = $(this).val();
//    if (varWhatdidSay == "INS Others" || varWhatdidSay == "Book Appointment" || varWhatdidSay == "Book My Service" || varWhatdidSay == "Call Me Later" || varWhatdidSay == "Service Not Required" || varWhatdidSay == "Renewal Not Required" || varWhatdidSay == "Confirmed" || varWhatdidSay == "ConfirmedInsu" || varWhatdidSay == "Cancel Appointment" || varWhatdidSay == "Cancelled" || varWhatdidSay == "Paid") {
//        $('#callMeLaterA').removeAttr("disabled", true);
//        $('#alreadyServiced').removeAttr("disabled", true);
//        $('#vehicleSold').removeAttr("disabled", true);
//        $('#dissatifiedwithPreviousService').removeAttr("disabled", true);
//        $('#dissatifiedwithPreviousServices').removeAttr("disabled", true);
//        $('#dissatifiedwithPService').removeAttr("disabled", true);
//        $('#dissatifiedwithService').removeAttr("disabled", true);
//        $('#dissatifiedwithStolen').removeAttr("disabled", true);
//        $('#vehicleSoldStolen').removeAttr("disabled", true);
//        $('#stolenHideShowSubmit').removeAttr("disabled", true);

//        validateCheck();
//        $('#finalDiv1').hide();
//        $('#CustomerDriveInDiv').hide();
//        $('#feedbackDIV').hide();
//        $('#CustFeedBack').hide();
//    } else {
//        $('#alreadyservicedDiv1').hide();
//    }
//});

//function validateCheck() {
//    if (varWhatdidSay != "") {
//        if (varWhatdidSay == "Book My Service") {
//            $("#InsuOthersDiv").hide();
//            $("#serviceBookDiv").show();
//            $("#callMeLattteDiv").hide();
//            $("#alreadyserviceDIV").hide();
//            $("#SMRInteractionFirst").hide();
//            $("#DidYouTalkDiv").hide();
//            $("#ConfirmedSubmit").hide();
//            $("#CancelServiceBk").hide();
//            $("#confirmInsuComments").hide();
//            $("#CancelInsuAppo").hide();

//        }
//        if (varWhatdidSay == "Book Appointment") {

//            $("#InsuOthersDiv").hide();
//            $("#serviceBookDiv").show();
//            $("#callMeLattteDiv").hide();
//            $("#alreadyserviceDIV").hide();
//            $("#SMRInteractionFirst").hide();
//            $("#DidYouTalkDiv").hide();
//            $("#ConfirmedSubmit").hide();
//            $("#CancelServiceBk").hide();
//            $("#confirmInsuComments").hide();
//            $("#CancelInsuAppo").hide();

//        }
//        if (varWhatdidSay == "Call Me Later") {
//            //$("#callMeLattteDiv").show();
//            $('#callMeLattteDiv').show();


//            $("#InsuOthersDiv").hide();
//            $("#serviceBookDiv").hide();
//            $("#alreadyserviceDIV").hide();
//            $("#alreadyservicedDiv1").hide();
//            $("#SMRInteractionFirst").hide();
//            $("#DidYouTalkDiv").hide();
//            $("#ConfirmedSubmit").hide();
//            $("#CancelServiceBk").hide();
//            $("#confirmInsuComments").hide();
//            $("#CancelInsuAppo").hide();




//        }
//        if (varWhatdidSay == "Service Not Required") {
//            //console.log(varWhatdidSay);
//            $("#serviceBookDiv").hide();
//            $("#callMeLattteDiv").hide();
//            $("#alreadyserviceDIV").show();
//            var dealer = $('#dealercode').val();
//            if (dealer == 'KALYANIMOTORS') {
//                $(".KmsNotCov").hide();
//                $(".callDisconnected").hide();
//            }
//            else {
//                $(".KmsNotCov").show();
//                $(".callDisconnected").show();
//            }
//            $("#SMRInteractionFirst").hide();
//            $("#DidYouTalkDiv").hide();
//            $("#ConfirmedSubmit").hide();
//            $("#CancelServiceBk").hide();
//            $("#confirmInsuComments").hide();
//            $("#CancelInsuAppo").hide();

//            $("#InsuOthersDiv").hide();
//        }

//        if (varWhatdidSay == "Renewal Not Required") {
//            //console.log(varWhatdidSay);
//            $("#serviceBookDiv").hide();
//            $("#callMeLattteDiv").hide();
//            $("#alreadyserviceDIV").show();
//            $("#SMRInteractionFirst").hide();
//            $("#DidYouTalkDiv").hide();
//            $("#ConfirmedSubmit").hide();
//            $("#CancelServiceBk").hide();
//            $("#confirmInsuComments").hide();
//            $("#CancelInsuAppo").hide();

//            $("#InsuOthersDiv").hide();

//        }

//        if (varWhatdidSay == "Confirmed") {
//            //console.log(varWhatdidSay);
//            $("#ConfirmedSubmit").show();
//            $("#serviceBookDiv").hide();
//            $("#callMeLattteDiv").hide();
//            $("#alreadyserviceDIV").hide();
//            $("#SMRInteractionFirst").hide();
//            $("#DidYouTalkDiv").hide();
//            $("#CancelServiceBk").hide();
//            $("#confirmInsuComments").hide();
//            $("#CancelInsuAppo").hide();

//            $("#InsuOthersDiv").hide();

//        }
//        if (varWhatdidSay == "ConfirmedInsu") {

//            //console.log(varWhatdidSay);
//            $("#confirmInsuComments").show();
//            $("#serviceBookDiv").hide();
//            $("#callMeLattteDiv").hide();
//            $("#alreadyserviceDIV").hide();
//            $("#SMRInteractionFirst").hide();
//            $("#DidYouTalkDiv").hide();
//            $("#CancelServiceBk").hide();
//            $("#CancelInsuAppo").hide();
//            $("#InsuOthersDiv").hide();

//        }
//        if (varWhatdidSay == "Cancel Appointment") {
//            $("#policyDropConfAdd").hide();
//            $("#InsuOthersDiv").hide();
//            $("#CancelInsuAppo").show();
//            $("#confirmInsuComments").hide();
//            $("#serviceBookDiv").hide();
//            $("#callMeLattteDiv").hide();
//            $("#alreadyserviceDIV").hide();
//            $("#DidYouTalkDiv").hide();
//            $("#CancelServiceBk").hide();

//        }
//        if (varWhatdidSay == "Cancelled") {
//            console.log("service booking " + varWhatdidSay);

//            $("#CancelServiceBk").show();
//            $("#SMRInteractionFirst").show();
//            $("#ConfirmedSubmit").hide();
//            $("#serviceBookDiv").hide();
//            $("#callMeLattteDiv").hide();
//            $("#alreadyserviceDIV").hide();
//            $("#DidYouTalkDiv").hide();
//            $("#confirmInsuComments").hide();
//            $("#CancelInsuAppo").hide();
//            $("#InsuOthersDiv").hide();

//        }

//        if (varWhatdidSay == "Paid") {

//            //console.log(varWhatdidSay);
//            $("#confirmInsuComments").show();
//            $("#serviceBookDiv").hide();
//            $("#callMeLattteDiv").hide();
//            $("#alreadyserviceDIV").hide();
//            $("#SMRInteractionFirst").hide();
//            $("#DidYouTalkDiv").hide();
//            $("#CancelServiceBk").hide();
//            $("#CancelInsuAppo").hide();
//            $("#InsuOthersDiv").hide();

//        }

//        if (varWhatdidSay == "INS Others") {


//            $("#policyDropConfAdd").hide();
//            $("#InsuOthersDiv").show();
//            $("#confirmInsuComments").hide();
//            $("#serviceBookDiv").hide();
//            $("#callMeLattteDiv").hide();
//            $("#alreadyserviceDIV").hide();
//            $("#DidYouTalkDiv").hide();
//            $("#CancelServiceBk").hide();
//            $("#CancelInsuAppo").hide();

//        }
//    }

//}

//$("input[name$='listingForm.othersINS']").click(function () {
//    var datais = $(this).val();

//    if (datais == "Policy Drop") {
//        $("#policyDropConfAdd").show();
//    } else {
//        $("#policyDropConfAdd").hide();
//    }

//    if (datais == "Escalation") {
//        var urlPath = siteRoot + "/CallLogging/userSuprevisorList/";

//        $.ajax({
//            type: 'POST',
//            url: urlPath,
//            datatype: 'json',
//            data: { moduleId: 2 },
//            cache: false,
//            success: function (res) {
//                console.log("userlist " + res.userlist.length);

//                var dropdown = document.getElementById("supCREList");
//                if (res.userlist.length > 0) {
//                    $('#supCREList').empty();

//                    for (var i = 0; i < res.userlist.length; i++) {

//                        dropdown[dropdown.length] = new Option(res.userlist[i].userName, res.userlist[i].id);

//                    }
//                }
//                else {
//                    Lobibox.notify('warning', {
//                        msg: 'No SUP CRE present for this Location!'
//                    });
//                }


//            }, error(error) {

//            }
//        });


//        $.ajax({



//        })

//        $("#escaltionDrop").show();

//    } else if (datais == "EscalationSMR") {


//        var urlPath = "/suprevisorList/1";
//        $.ajax({
//            url: urlPath
//        }).done(function (userlist) {
//            console.log("userlist " + userlist.length);

//            $('#supCREList').empty();
//            var dropdown = document.getElementById("supCREList");

//            for (var i = 0; i < userlist.length; i++) {

//                dropdown[dropdown.length] = new Option(userlist[i].userName, userlist[i].id);

//            }


//        });
//        $("#escaltionDrop").show();



//    } else {
//        $("#escaltionDrop").hide();
//    }

//});


//// New Insurance Design Chnages on 20th june 2018 //
//$("input[name$='appointbooked.typeOfPickup']").click(function () {
//    var oem = $("#PkOEM").val();
//    if (oem == "MARUTI SUZUKI") {
//        var FieldVar = $(this).val();
//        if (FieldVar == "Field") {
//            $("#NEWWalkinID").hide();
//            $("#NEWFieldID").show();
//            $("#NEWOnlineID").hide();

//        }
//        else if (FieldVar == "Walk-in") {
//            $("#NEWFieldID").show();
//            $("#NEWWalkinID").hide();
//            $("#NEWOnlineID").show();
//            setTimeout(function () {
//                setTomorrowDate();
//            }
//                , 800);

//        }
//        else {
//            $("#NEWFieldID").hide();
//            $("#NEWWalkinID").hide();
//            $("#NEWOnlineID").show();
//            setTimeout(function () {
//                setTomorrowDateAndTime();
//            }
//                , 800);
//        }
//    }
//});
//$("#otherSubmit").click(function () {

//    var other = $("input[name='listingForm.othersINS']:checked").val();
//    var checkedCount = $('input[name="listingForm.othersINS"]:checked').length;

//    var pin = $('#pincodeIdpolicy').val();
//    var policydate = $('#insdate1234567').val();
//    var policydroptime = $('#policayDropTime').val();
//    var policydropaddress = $('#policyDropAddressId').val();
//    var policydroplocation = $('#dropLocation').val();
//    if (checkedCount > 0) {
//        if (other == "Policy Drop") {
//            if (pin == "" || pin == null) {
//                Lobibox.notify('warning', {
//                    continueDelayOnInactiveTab: true,
//                    msg: 'Please Enter Pincode Value.'
//                });
//                return false;
//            }
//            if (policydate == "" || policydate == null) {
//                Lobibox.notify('warning', {
//                    continueDelayOnInactiveTab: true,
//                    msg: 'Please Enter Policy Date Value.'
//                });
//                return false;
//            }
//            if (policydroptime == "" || policydroptime == null) {
//                Lobibox.notify('warning', {
//                    continueDelayOnInactiveTab: true,
//                    msg: 'Please Enter Policy Drop Time.'
//                });
//                return false;
//            }
//            if (policydroplocation == "" || policydroptime == null || policydroplocation == "--Select--" || policydroplocation == "0") {
//                Lobibox.notify('warning', {
//                    continueDelayOnInactiveTab: true,
//                    msg: 'Please Enter Plicy Drop Location.'
//                });
//                return false;
//            }
//            if (policydropaddress == "" || policydropaddress == null || policydropaddress == "--Select--" || policydropaddress == "0") {
//                Lobibox.notify('warning', {
//                    continueDelayOnInactiveTab: true,
//                    msg: 'Please Enter Valid Addres.'
//                });
//                return false;
//            }
//        }
//        else if (other == "Escalation") {
//            var supCRE = $('#supCREList').val();
//            if (supCRE == "" || supCRE == null || supCRE == "--Select--" || supCRE == "0") {
//                Lobibox.notify('warning', {
//                    continueDelayOnInactiveTab: true,
//                    msg: 'Please select CRE.'
//                });
//                return false;
//            }

//        }
//    }
//    else {
//        Lobibox.notify('warning', {
//            continueDelayOnInactiveTab: true,
//            msg: 'Please select Any options below.'
//        });
//        return false;

//    }
//});




