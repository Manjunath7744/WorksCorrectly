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


$("input[name$='typeOfPickup']").click(function () {
    var isfieldexctve = $('#fieldenabled').val();
    var FieldVar = $(this).val();
    if (FieldVar == "Field") {
        $("#NEWWalkinID").hide();
        $("#NEWFieldID").show();
        if (isfieldexctve == "False") {
            $("#NEWOnlineID").show();

        }
        else {
            clearpickupTypes();
            $("#NEWOnlineID").hide();

        }
        clearpickupTypes();
    }
    else if (FieldVar == "Walk-in") {
        $("#NEWFieldID").hide();
        $("#NEWWalkinID").show();
        $("#NEWOnlineID").show();
        clearpickupTypes();
    }
    else {



        $("#NEWFieldID").hide();
        $("#NEWWalkinID").hide();
        $("#NEWOnlineID").show();
        clearpickupTypes();
        setTimeout(function () {
            setTomorrowDateAndTime();
        }
            , 800);
    }

});

$("#nextToCustomerDrivekalyani").click(function () {
    var vRenewalTypeID = $("#renewalTypeID").val();
    var vRenewalModeID = $("#renewalModeID").val();
    var vPaymentTypeID = $("#paymentTypeID").val();
    var vTypeOfPickup = $("input[name='appointbooked.typeOfPickup']:checked").val();
    var vAddressMSSId = $("#AddressMSSId").val();
    var vdiscountID = $("#discount").val();
    var vpremiumdiscountID = $("#premiumdiscount").val();
    var vInsdate12345 = $("#insdate12345").val();
    var onlineTime = $('#appointmentTime').val();
    var purposeId = $('#PurposeID').val();
    var picodeId = $('#pincodeId').val();
    var fieldloc = $('#OtherLocation').val();
    var selectedfiled = $('#driverValueIns').val();
    var isfieldexctve = $('#fieldenabled').val();
    var vaddressLenght = 0;
    var dealerCode = $('#PkDealercode').val();
    var insCompanies = $('#insCompanies').val();

    if (vAddressMSSId != null) {
        vaddressLenght = vAddressMSSId.length;
    }
    var vSchedule = true;

    if (vRenewalTypeID == "" || vRenewalTypeID == null) {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please Select Renewal Type.'
        });
        return false;
    }
    if (vRenewalModeID == "0" || vRenewalModeID == null) {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please Select Renewal Mode.'
        });
        return false;
    }
    if (vPaymentTypeID == "0" || vPaymentTypeID == null) {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please Select Payment Mode.'
        });
        return false;
    }
    if (vdiscountID == "" || vdiscountID == null) {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please Enter Discount Value.'
        });
        return false;
    }
    if (dealerCode == "INDUS") {

        if (vpremiumdiscountID == "" || vpremiumdiscountID == null || vpremiumdiscountID == "0") {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Enter Premium Discount Value.'
            });
            return false;
        }

        if (insCompanies == "" || insCompanies == null || insCompanies == "0") {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Select Insurance Company.'
            });
            return false;
        }
    } else {
        if (vpremiumdiscountID == "" || vpremiumdiscountID == null) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Enter Premium Discount Value.'
            });
            return false;
        }
    }

    if (!vTypeOfPickup) {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please Select Appt Mode.'
        });
        return false
    }

    if (vTypeOfPickup == "Field") {

        if (purposeId == 0 || purposeId == null) {
            vSchedule = false;
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Provide Purpose of Visit.'
            });

            return false
        }
        else if (vaddressLenght == 0) {
            vSchedule = false;
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Select Address.'
            });
            return false
        }
        //else if (picodeId == "" || purposeId == null) {
        //    vSchedule = false;
        //    Lobibox.notify('warning', {
        //        continueDelayOnInactiveTab: true,
        //        msg: 'Please Provide Pincode.'
        //    });
        //    return false
        //}

        else if (isfieldexctve == "False") {
            if (dealerCode != "PAWANHYUNDAI") {
                if (fieldloc == "" || fieldloc == null) {
                    vSchedule = false;
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please Provide Field Location.'
                    });
                    return false
                }
            }
            if (vInsdate12345 == "") {
                vSchedule = false;
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please Provide Appointment Date.'
                });
                return false
            }
            if (onlineTime == "") {
                vSchedule = false;
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please Provide Appointment Time.'
                });
                return false
            }

        }
        else if (isfieldexctve == "True") {
            if (selectedfiled != null) {

                if (selectedfiled == '0') {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please Scheduling Date and Time.'
                    });
                    vSchedule = false;
                }
            }

        }
        else {
            vSchedule = true;
        }
    }
    if (vTypeOfPickup == "Walk-in") {
        var vWalkinLocation = $("#walkinLocation").val();
        var vInsdate12345 = $("#insdate12345").val();
        var vAppointmentTime = $("#appointmentTime").val();
        var vInsuranceAgentDataId = $("#insuranceAgentDataId").val();
        if (vWalkinLocation = "" || vWalkinLocation == null || vWalkinLocation == "--Select--") {
            vSchedule = false;
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Provide Walk-in Location.'
            });
            return false

        }
        else if (vInsdate12345 == "") {
            vSchedule = false;
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Provide Appointment Date.'
            });
            return false
        }
        else if (vAppointmentTime == "") {
            vSchedule = false;
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Provide Appointment Time.'
            });
            return false
        }
        else if (vInsuranceAgentDataId == 0) {
            if (dealerCode != "PAWANHYUNDAI") {
                vSchedule = false;
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please Provide FSE.'
                });
                return false
            }
            else {
                vSchedule = true;

            }
        }
    }
    if (vTypeOfPickup == "Online") {

        if (vInsdate12345 == "" || vInsdate12345 == null) {
            vSchedule = false;
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Provide Appointment Date.'
            });
            return false;
        }
        else if (onlineTime == "" || onlineTime == null) {
            vSchedule = false;
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Provide Appointment Time.'
            });
            return false;
        }
        else {
            vSchedule = true;

        }
    }

    if (vSchedule && vRenewalTypeID != "" && vRenewalModeID != "0" && vPaymentTypeID != "0" && vdiscountID != "" && vpremiumdiscountID != "" && vTypeOfPickup) {
        $("#CustomerDriveInDiv").show();
        $("#serviceBookDiv").hide();
        $("#SMRInteractionFirst").hide();
        $("#DidYouTalkDiv").hide();
        $("#whatDidCustSayDiv").hide();
    }
});


$("#NextToLeadInsurance").click(function () {
    var atLeastOneIsChecked = 0;
    $('[name="listingForm.LeadYes"]').each(function () {
        if ($(this).is(':checked')) atLeastOneIsChecked++;
    });
    if (atLeastOneIsChecked == 0) {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please check one.'
        });

    }
    else {
        if ($("#LeadYesID").prop('checked')) {
            if ($('#checkboxExist').children().length > 0) {

                var checkeds = $('.myOutCheckbox').is(':checked');

                if (checkeds) {

                } else {

                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please check one of these.'

                    });
                    return false;
                }
            }
        }

    }

    var complFB = $('#selected_department1').val();

    if ($("#CustomerfeedbackYes").prop('checked')) {
        if (complFB == "0" || complFB == null || complFB == "") {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please select one of compliant type'

            });
            return false;
        }

    }

    if (document.getElementById('nomineeYesID').checked) {
        var varNomitxtB = $('#NomineeNameID').val();
        var varNomineeAgeID = $('#NomineeAgeID').val();
        var varNomineeRelationID = $('#NomineeRelationID').val();
        var varAppointeeNameID = $('#AppointeeNameID').val();
        if (varNomitxtB == '') {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Enter Nominee Name'

            });
            return false;

        } if (varNomineeAgeID == '') {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Enter Nominee Age'

            });
            return false;
        }
        if (varNomineeRelationID == '') {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Enter Nominee Relation With Owner'

            });
            return false;
        }
        if (varAppointeeNameID == '') {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Enter Appointee Name'

            });
            return false;
        }
    }
    $("#coupon").prop("disabled", false);


});

$("#BackToMainInsu").click(function () {
    clearAllControlls();
    $("#SMRInteractionFirst").show();
    $("#DidYouTalkDiv").show();
    $("#alreadyserviceDIV").hide();
    $("#WhatdidtheCustomersayDIV").hide();
    $("#serviceBookDiv").hide();
    $('#SpeakYes').attr('checked', false);
    $('#SpeakNo').attr('checked', false);


});

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

//        if (varWhatdidSay == "Renewal Not Required") {
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
//        if (varWhatdidSay == "Paid") {
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


$("input[name$='listingForm.othersINS']").click(function () {
    var datais = $(this).val();

    if (datais == "Policy Drop") {
        $("#policyDropConfAdd").show();
    } else {
        $("#policyDropConfAdd").hide();
    }

    if (datais == "Escalation") {
        var urlPath = siteRoot + "/CallLogging/userSuprevisorList/";

        $.ajax({
            type: 'POST',
            url: urlPath,
            datatype: 'json',
            data: { moduleId: 2 },
            cache: false,
            success: function (res) {
                console.log("userlist " + res.userlist.length);

                var dropdown = document.getElementById("supCREList");
                if (res.userlist.length > 0) {
                    $('#supCREList').empty();

                    for (var i = 0; i < res.userlist.length; i++) {

                        dropdown[dropdown.length] = new Option(res.userlist[i].userName, res.userlist[i].id);

                    }
                }
                else {
                    Lobibox.notify('warning', {
                        msg: 'No SUP CRE present for this Location!'
                    });
                }


            }, error(error) {

            }
        });


        $.ajax({



        })

        $("#escaltionDrop").show();

    } else if (datais == "EscalationSMR") {


        var urlPath = "/suprevisorList/1";
        $.ajax({
            url: urlPath
        }).done(function (userlist) {
            console.log("userlist " + userlist.length);

            $('#supCREList').empty();
            var dropdown = document.getElementById("supCREList");

            for (var i = 0; i < userlist.length; i++) {

                dropdown[dropdown.length] = new Option(userlist[i].userName, userlist[i].id);

            }


        });
        $("#escaltionDrop").show();



    } else {
        $("#escaltionDrop").hide();
    }

});
$("#otherSubmit").click(function () {

    var other = $("input[name='listingForm.othersINS']:checked").val();
    var checkedCount = $('input[name="listingForm.othersINS"]:checked').length;

    var policyEnabled = $('#policydropenabled').val();
    var policydropAgent = $('#driverValueIns1').val();

    var pin = $('#pincodeIdpolicy').val();
    var policydate = $('#insdate1234567').val();
    var policydroptime = $('#policayDropTime').val();
    var policydropaddress = $('#policyDropAddressId').val();
    var policydroplocation = $('#dropLocation').val();
    if (checkedCount > 0) {
        if (other == "Policy Drop") {
            if (pin == "" || pin == null) {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please Enter Pincode Value.'
                });
                return false;
            }

            else if (policyEnabled == "True") {
                if (policydropAgent != null) {

                    if (policydropAgent == '0') {
                        Lobibox.notify('warning', {
                            continueDelayOnInactiveTab: true,
                            msg: 'Please Scheduling Date and Time.'
                        });
                        return false;
                    }
                }

            }
            else {
                if (policydate == "" || policydate == null) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please Enter Policy Date Value.'
                    });
                    return false;
                }
                if (policydroptime == "" || policydroptime == null) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please Enter Policy Drop Time.'
                    });
                    return false;
                }
                if (policydroplocation == "" || policydroptime == null || policydroplocation == "--Select--" || policydroplocation == "0") {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please Enter Plicy Drop Location.'
                    });
                    return false;
                }
            }
            if (policydropaddress == "" || policydropaddress == null || policydropaddress == "--Select--" || policydropaddress == "0") {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please Enter Valid Addres.'
                });
                return false;
            }
        }
        else if (other == "Escalation") {
            var supCRE = $('#supCREList').val();
            if (supCRE == "" || supCRE == null || supCRE == "--Select--" || supCRE == "0") {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please select CRE.'
                });
                return false;
            }

        }
    }
    else {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please select Any options below.'
        });
        return false;

    }
});


$('#callMeLaterSubmit').click(function () {
    var dateVal = $('#FollowUpDateinsurance').val();
    var timeVal = $('#FollowUpTimeinsurance').val();
    var reason = $('#followReason').val();
    var dealerCode = $('#PkDealercode').val();
    if (dealerCode == "GALAXYTOYOTA" || dealerCode == "HARPREETFORD" || dealerCode == "HANSHYUNDAI") {
    var followReason = $('#followReason').val();

    if (followReason == '' || followReason == null) {
        Lobibox.notify('warning', {
            msg: 'Please select the Follow Up Type.'
        });
        return false;
        }
    }
    if (dateVal == '') {
        Lobibox.notify('warning', {
            msg: 'Please select the date.'
        });
        return false;
    }
    if (timeVal == '') {
        Lobibox.notify('warning', {
            msg: 'Please select the time.'
        });
        return false;
    }

    if (dealerCode == "ADVAITHHYUNDAI") {
        var advaithfollowupreson = $('#advaithfollowReason').val();
        if (advaithfollowupreson == '' || advaithfollowupreson == '0' || advaithfollowupreson == null) {
            Lobibox.notify('warning', {
                msg: 'Please select followup reason.'
            });
            return false;
        }
    }
    //if (reason == '' || reason == '0' || reason == null) {
    //    Lobibox.notify('warning', {
    //        msg: 'Please select followup reason.'
    //    });
    //    return false;
    //}

});

//20th may changes 
$("input[name$='renewalDoneBy']").click(function () {
    varAlreadyservicedadio = $(this).val();

    if (varAlreadyservicedadio == "Renewed At My Dealer") {
        $('#dateofrnwal').val('');
        $('#dateofrennonauth').val('');
        $('#insuranceProvidedByOEM').prop('selectedIndex', 0);
        $('#inBoundLeadSourceSelectVal').prop('selectedIndex', 0);
        $('#insuranceProvidedUnAuth').prop('selectedIndex', 0);




        $('#ServicedMyDealerDiv').show();
        $('#ServicedAtOtherDealerDiv').hide();
    }
    else if (varAlreadyservicedadio == "Renewed At Other Dealer") {
        $('#insudisposition_premimum').val('');
        $('#insudisposition_coverNoteNo').val('');
        $('#insuranceProvidedBy').prop('selectedIndex', 0);
        $('#ServicedAtOtherDealerDiv').show();
        $('#ServicedMyDealerDiv').hide();
    }
    else {
        $('#insudisposition_premimum').val('');
        $('#insudisposition_coverNoteNo').val('');
        $('#insuranceProvidedBy').prop('selectedIndex', 0);


        $('#dateofrnwal').val('');
        $('#dateofrennonauth').val('');
        $('#insuranceProvidedByOEM').prop('selectedIndex', 0);
        $('#inBoundLeadSourceSelectVal').prop('selectedIndex', 0);
        $('#insuranceProvidedUnAuth').prop('selectedIndex', 0);

        $('#InfoNotAvailable').hide();
        $('#ServicedMyDealerDiv').hide();
        $('#ServicedAtOtherDealerDiv').hide();
    }
});



//Insurance SNR submit validations

$('#renewalId').click(function () {

    var atLeastOneIsChecked = 0;
    $('[name="listingForm.LeadYesRNR"]').each(function () {
        if ($(this).is(':checked')) atLeastOneIsChecked++;
    });


    if ($("#AlreadyServiced").prop('checked') == true) {
        if ((!$('#renewedOtherDealer').is(':checked')) && (!$('#insudisposition_renewalDoneBy').is(':checked')) && (!$('#ServicedMyDealer').is(':checked'))) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Select Any Renewed Option!'
            });
            return false;
        }


        if ($("#ServicedMyDealer").is(":checked")) {

            var lastservDate = $('#insudisposition_lastRenewalDate').val();
            var worklist = $('#lastServiceWorkshopList').val();
            var insPrvded = $('#insuranceProvidedBy').val();
            var premum = $('#insudisposition_premimum').val();
            var cvrNote = $('#insudisposition_coverNoteNo').val();
            if (lastservDate == "" || worklist == "" || insPrvded == "" || premum == "" || cvrNote == "") {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please Enter Renewed Dealer Details'
                });
                return false;
            }


        }
        if ($("#renewedOtherDealer").is(":checked")) {

            if ((!$('#Autorizedworkshopid').is(':checked')) && (!$('#NonAutorizedworkshopid').is(':checked'))) {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please Select Any Other Dealer Option!'
                });
                return false;
            }


            else if ($("#Autorizedworkshopid").is(":checked")) {

                var dlrName = $('#insudisposition_dealerName').val();
                var dtOfRnwl = $('#insudisposition_dateOfRenewal').val();
                var oem = $('#insuranceProvidedByOEM').val();
                var ins = $('#inBoundLeadSourceSelectVal').val();
                if (dlrName == "" || dtOfRnwl == "" || insPrvded == "" || oem == "" || oem == "--Select--" || ins == 0) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please Enter Autherized Dealer Details'
                    });
                    return false;
                }


            }
            else if ($("#NonAutorizedworkshopid").is(":checked")) {

                var dlrName = $('#listingForm_dateOfRenewalNonAuth').val();
                var oem = $('#insuranceProvidedUnAuth').val();
                if (dlrName == "" || oem == "" || oem == "--Select--") {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please Enter Non Autherized Dealer Details'
                    });
                    return false;
                }


            }
        }


    }

    if ($("#Dissatisfiedwithpreviousservice").prop('checked') == true) {
        var lastSerDate = $('#lastServiceDateOfDWPS').val();
        var SA = $('#serviceAdvisorID').val();
        var ST = $('#lastServiceType').val();
        var AS = $('#assignedToSA').val();

        if (lastSerDate == "" || SA == "" || ST == "" || AS == "") {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Enter Previous Service Details'
            });
            return false;
        }
    } if ($("#Distancefrom").prop('checked') == true) {
        var city = $('#insudisposition_cityName').val();


        if (city == "") {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Enter City'
            });
            return false;
        }
    }
    if ($("#DissatisfiedwithSalesID").prop('checked') == true) {
        var Tag = $('#noServiceReasonTaggedTo').val();
        var commnt = $('#noServiceReasonTaggedToComments').val();


        if (Tag == "") {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Enter Tag To'
            });
            return false;
        }
    }
    if ($("#DissatisfiedwithInsuranceId").prop('checked') == true) {
        var Tag = $('#noServiceReasonTaggedTo1').val();
        var commnt = $('#noServiceReasonTaggedToComments1').val();


        if (Tag == "") {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Enter Tag To'
            });
            return false;
        }
    }

    if (atLeastOneIsChecked == 0) {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please check one of upsel.'
        });
        return false;

    } else {
        if ($("#LeadYesRNRID").prop('checked')) {
            var checkeds = $('.myOutCheckbox').is(':checked');

            if (checkeds) {

            } else {

                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please check one of these upsells.'

                });
                return false;
            }
        }
    }
    var complFB = $('#selected_department').val();
    if ($("#CustomerfeedbackRNRIdYes").prop('checked')) {
        if (complFB == "0" || complFB == null || complFB == "") {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please select one of compliant type'

            });
            return false;
        }

    }

});

$("#RenewalNotRequired").click(function () {
    $("#serviceBookedTypeSelect").val("0"),
        $("#workshop").val("0"),
        $("#date12345").val(""),
        $("#CommittedTimes").val(""),
        $("#serviceAdvisor").val("0"),
        $("#FollowUpDate").val(""),
        $("#FollowUpTime").val(""),
        $(".AlreadyServiced").show(),
        $(".3Years").show(); 
        $(".CorporateAgent").show(); 
        $(".HighPremium").show(),
        $(".FundIssue").show(), 
        $(".TransferCase").show(), 
        $(".CustomerRelocate").show(),
        $(".VehicleSold").show(),
        $(".Dissatisfiedwithpreviousservice").show(),
        $(".Distancefrom").show(),
        $(".DissatisfiedwithSalesID").show(),
        $(".DissatisfiedwithInsuranceId").show(),
        $(".Stolen").show(),
        $(".Totalloss").show(),
        $("input:radio[name='servicebooked.typeOfPickup']").each(function () {
            this.checked = false;
        }),
        $("input:radio[name='listingForm.LeadYes']").each(function () {
            this.checked = false;
        }),
        $("input:radio[name='listingForm.userfeedback']").each(function () {
            this.checked = false;
        }),
        $("input:radio[name='listingForm.CustomerFeedBackYes']").each(function () {
            this.checked = false;

        }),
        $("#LeadDiv").hide(),
        $("#renewalId").hide(),

        $("input[type=checkbox]").prop("checked", false);
    $("#InsuranceSelect").hide(),
        $("#WARRANTYSelect").hide(),
        $("#VASTagToSelect").hide(),
        $("#ReFinanceSelect").hide(),
        $("#LoanSelect").hide(),
        $("#EXCHANGEIDSelect").hide(),
        $("#UsedCarSelect").hide(),
        $(".OtherLast").show()
});


$("input[name$='nomineeYes']").click(function () {
    varNomineeYes = $(this).val();

    if (varNomineeYes == "Yes") {
        $("#nomineeDiv").show();

    }
    if (varNomineeYes == "No") {
        $("#nomineeDiv").hide();
        $("#NomineeNameID").val('');
        $("#NomineeAgeID").val('');
        $("#NomineeRelationID").val('');
        $("#AppointeeNameID").val('');
    }
    else {


    }
});


//Field Schheduler

function ajaxCallForSchedularChanged() {
    //var scheduleDate = document.getElementById("dateis").value;
    var scheduleDate = $('#dateis').val();
    //var locaId = document.getElementById("fieldLocation").value;
    var locaId = $("#fieldLocation").val();
    var needle = [];
    var blocktme = [];
    if (scheduleDate == "" || locaId == "") {
        Lobibox.notify('warning', {
            msg: 'scheduled Date or Location is not selected'
        });
        return false;
    }

    if (scheduleDate == undefined || locaId == undefined) {
        //Lobibox.notify('warning', {
        //    msg: 'scheduled Date or Location is not selected'
        //});
        return false;
    }
    $('#fecontent').css({ 'display': 'none' });
    $('#fieldloader').css({ 'display': 'block' });
    var urlPath = siteRoot + "/InsuranceOutBound/filedExecutivesListToSchedulerNEW/";
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

            if ($.fn.DataTable.isDataTable("#tableschID")) {
                var table = $("#tableschID").DataTable();
                table.clear();
                table.destroy();
            }
            $("#tableschID thead").remove();

            var table = document.getElementById("tableschID");
            var header = table.createTHead();
            var row = header.insertRow(0);
            for (var i = data.length - 1; i >= 0; i--) {
                var cell = row.insertCell(0);
                cell.outerHTML = "<th id=" + data[i].id + ">" + data[i].userName + "</th>";
            }
            var cell = row.insertCell(0);
            cell.outerHTML = "<th>Timeslot</th>";

            for (var j = 0; j < timeslot.length - 1; j++) {
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
                    if (result) {
                        var extCount = data[k].listChassis;
                        if (extCount != null && extCount != "" && extCount != undefined) {
                            var s = extCount.length;
                            for (var i = 0; i < extCount.length; i++) {

                                var r = extCount[i].split("/");
                                var a = r[0];
                                var b = r[1];
                                var c = r[2];
                                var h = timeslot[j].startTime.TotalHours;

                                if (h == a && c == "T") {
                                    //$('#startValueIns').val(k);
                                    //$('#endValueIns').val(k);
                                    tr.append('<td id=' + timeslot[j].timeRange + ' class="ColorGreen exist" title=' + b + '></td>');
                                    break;
                                }
                                else if (h == a) {
                                    //$('#startValueIns').val(k);
                                    //$('#endValueIns').val(k);
                                    tr.append('<td id=' + timeslot[j].timeRange + ' class="LightSeaGreen exist" title=' + b + '></td>');
                                    break;
                                }
                            }
                        }

                        result = false;
                    }
                    else {
                        tr.append('<td id=' + timeslot[j].timeRange + '></td>');
                    }

                }
                $('#tableschID').append(tr);

                //$.fn.dataTable.ext.errMode = 'none';
                //$($.fn.dataTable.tables(true)).DataTable()
                //    .columns.adjust();
            }

            if (table != null) {
                for (var i = 1; i < table.rows.length; i++) {
                    for (var j = 1; j < table.rows[i].cells.length; j++) {
                        {
                            table.rows[i].cells[j].onclick = function () {
                                tableInsuranceFieldBooking(this);
                            };
                        }

                    }
                }
            }

            var FeAptTable = $('#tableschID').dataTable({

                //  "fixedHeader": true,
                //  "scrollX": "100%",
                "scrollCollapse": true,
                //     "scrollY": 320,
                "paging": false,
                "searching": false,
                "ordering": false,
                "bInfo": false,
                //fixedColumns: {
                //    leftColumns: 1
                //}
                "initComplete": function (sesstings, json) {
                    //FeAptTable.DataTable().columns.adjust();
                }
            });

            //$($.fn.dataTable.tables(true)).DataTable()
            //    .columns.adjust();

            // $($.fn.dataTable.tables(true)).DataTable()
            //     .columns.adjust();

            //$('#tableschID').append($(this).find("tr:first")).dataTable({ "paging": true, "searching": false, pageLength: 100 });

            //$(document).ready(function () {
            //    $('#MainContent_customer_grd').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable();
            //});
            //FeAptTable.columns.adjust();

            //$($.fn.dataTable.tables(true)).DataTable()
            //    .columns.adjust()
            //    .fixedColumns().relayout();

            //$('#tableschID').dataTable({
            //    "scrollX": true,
            //    "scrollY": 320,
            //    "paging": false,
            //    "searching": false,
            //    "ordering": false,
            //    "bInfo": false
            //});
            $('#fieldloader').css({ 'display': 'none' });
            $('#fecontent').css({ 'display': 'block' });
        }, error(error) {
        }
    });
    $('#schButton').show();
}
function inArray(startTime, blockTime) {
    var count = blockTime.length;
    for (var i = 0; i < count; i++) {
        if (blockTime[i].TotalHours === startTime.TotalHours) {

            return true;
        }
    }
    return false;
}
function tableInsuranceFieldBooking(tableCell) {
    //alert('clicked two inside'+tableCell.id);
    vtempValueIns = parseInt(document.getElementById("tempValueIns").value);

    //if (vtempValueIns == tableCell.cellIndex || vtempValueIns == 0) {
    //    vtempValueIns = tableCell.cellIndex//    table.rows[0].cells[j].innerText;

    //}
    //else
    //{
    //    return;
    //}
    console.log("tablecel id : " + tableCell.id);
    var className = tableCell.className;

    if (className == "" || className == "LightSeaGreen" || className == "LightSeaGreen exist" || className == "exist LightSeaGreen" || className == "exist") {
        //alert('clicked 4');
        $.confirm({
            title: 'Confirm!',
            closeIcon: true,
            content: 'Do you want Assign' + "<br>" + tableCell.id,
            buttons: {
                Yes: function () {
                    vstartValueIns = parseInt(document.getElementById("startValueIns").value);
                    vendValueIns = parseInt(document.getElementById("endValueIns").value);

                    if (vstartValueIns != 0 || vendValueIns != 0) {
                        $('#tableschID').find('.ColorGreen').each(function () {
                            $(this).removeClass("ColorGreen");
                        });
                        $('#tableschID').find('.exist').each(function () {
                            $(this).addClass("LightSeaGreen");
                        });
                        $('#startValueIns').val('0');
                        $('#endValueIns').val('0');
                        vstartValueIns = parseInt(document.getElementById("startValueIns").value);
                        vendValueIns = parseInt(document.getElementById("endValueIns").value);
                    }


                    if (vstartValueIns === 0) {
                        vstartValueIns = tableCell.parentElement.rowIndex;
                        document.getElementById("startValueIns").value = tableCell.parentElement.rowIndex;
                    }
                    else if (vendValueIns === 0) {
                        vendValueIns = tableCell.parentElement.rowIndex;
                        document.getElementById("endValueIns").value = vendValueIns;

                    }
                    $(tableCell).removeClass('LightSeaGreen');
                    $(tableCell).addClass('ColorGreen ');
                    if (vendValueIns === 0) {
                        vendValueIns = tableCell.parentElement.rowIndex;
                    }
                    if (vstartValueIns != 0 && vendValueIns != 0) {
                        //if (vstartValueIns < tableCell.parentElement.rowIndex) {
                        //    if (vendValueIns < tableCell.parentElement.rowIndex) {
                        //        vendValueIns = tableCell.parentElement.rowIndex;
                        //    }
                        //} else {
                        //    vstartValueIns = tableCell.parentElement.rowIndex;
                        //}
                        if (vstartValueIns > vendValueIns) {
                            minValue = vendValueIns;
                            maxValue = vstartValueIns;
                        } else {
                            minValue = vstartValueIns;
                            maxValue = vendValueIns;
                        }

                        var table = document.getElementById("tableschID");
                        var tableCellheader = table.rows[0].cells[tableCell.cellIndex];

                        //console.log("tableCellheader : "+tableCellheader.id);
                        //console.log("tableCellheader innerText: "+tableCellheader.innerText);

                        document.getElementById("driverValueIns").value = tableCellheader.id;
                        $('#newDriver').html(tableCellheader.innerText);

                        console.log("min and max value : " + minValue, maxValue);

                        for (var i = minValue; i < maxValue; i++) {
                            var tableCell1 = table.rows[i].cells[tableCell.cellIndex];
                            $(tableCell1).addClass('ColorGreen');
                        }

                        document.getElementById("startValueIns").value = minValue;
                        document.getElementById("endValueIns").value = maxValue;
                        minValue = '';
                        maxValue = '';

                    }


                    document.getElementById("tempIncreValueIns").value = $('#tableschID .ColorGreen').length;
                    document.getElementById("tempValueIns").value = tableCell.cellIndex;
                    if ($('#tableschID .ColorGreen').length === 0) {
                        document.getElementById("startValueIns").value = 0;
                        document.getElementById("endValueIns").value = 0;
                        document.getElementById("tempValueIns").value = 0;
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

                    vstartValueIns = parseInt(document.getElementById("startValueIns").value);
                    vendValueIns = parseInt(document.getElementById("endValueIns").value);
                    var table1 = document.getElementById('tableschID');
                    $(tableCell).removeClass('ColorGreen');
                    var className = tableCell.className;
                    if (className.includes("exist")) {
                        $(tableCell).addClass('LightSeaGreen');
                    }
                    if (vstartValueIns !== tableCell.parentElement.rowIndex && vendValueIns === tableCell.parentElement.rowIndex) {
                        for (i = vendValueIns - 1; i >= vstartValueIns; i--) {
                            var tableCell2 = table1.rows[i].cells[tableCell.cellIndex];
                            if ($(tableCell2).hasClass('ColorGreen')) {
                                vendValueIns = i;
                                document.getElementById("endValueIns").value = i;
                                break;
                            }
                            continue;
                        }
                    } else if (vstartValueIns === tableCell.parentElement.rowIndex && vendValueIns !== tableCell.parentElement.rowIndex) {
                        for (i = vstartValueIns + 1; i <= vendValueIns; i++) {
                            var tableCell2 = table1.rows[i].cells[tableCell.cellIndex];
                            if ($(tableCell2).hasClass('ColorGreen')) {
                                vstartValueIns = i;
                                document.getElementById("startValueIns").value = i;
                                break;
                            }
                            continue;
                        }
                    } else if (vstartValueIns === tableCell.parentElement.rowIndex && vendValueIns === tableCell.parentElement.rowIndex) {
                        document.getElementById("startValueIns").value = 0;
                        document.getElementById("startValueInsExist").value = 0;
                        document.getElementById("endValueIns").value = 0;
                        document.getElementById("endValueInsExist").value = 0;
                        document.getElementById("tempValueIns").value = 0;
                        document.getElementById("driverValueIns").value = 0;
                    }


                    document.getElementById("tempIncreValueIns").value = $('#tableschID .ColorGreen').length;
                    document.getElementById("tempValueIns").value = tableCell.cellIndex;
                    if ($('#tableschID .ColorGreen').length === 0) {
                        document.getElementById("startValueIns").value = 0;
                        document.getElementById("startValueInsExist").value = 0;
                        document.getElementById("endValueIns").value = 0;
                        document.getElementById("endValueInsExist").value = 0;
                        document.getElementById("tempValueIns").value = 0;
                        document.getElementById("driverValueIns").value = 0;
                    }

                },
                No: function () {
                    $(tableCell).addClass('ColorGreen');

                }
            }
        });
    }




}

//Policy Drop Sceduler

function ajaxCallForpolicydropSchedular() {
    var scheduleDate = document.getElementById("policydropdateis").value;
    var locaId = document.getElementById("policydropfieldLocation").value;
    var needle = [];
    var blocktme = [];
    if (scheduleDate == "" || locaId == "") {
        Lobibox.notify('warning', {
            msg: 'scheduled Date or Location is not selected'
        });
        return false;
    }
    $('#policyContent').css({ 'display': 'none' });
    $('#polcydloader').css({ 'display': 'block' });
    var urlPath = siteRoot + "/InsuranceOutBound/policydropfiledExecutivesListToScheduler/";
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

            if ($.fn.DataTable.isDataTable("#tableschIDpolicy")) {
                var table = $("#tableschIDpolicy").DataTable();
                table.clear();
                table.destroy();
            }
            $("#tableschIDpolicy thead").remove();

            var table = document.getElementById("tableschIDpolicy");
            var header = table.createTHead();
            var row = header.insertRow(0);
            for (var i = data.length - 1; i >= 0; i--) {
                var cell = row.insertCell(0);
                cell.outerHTML = "<th id=" + data[i].id + ">" + data[i].userName + "</th>";
            }
            var cell = row.insertCell(0);
            cell.outerHTML = "<th>Timeslot</th>";

            for (var j = 0; j < timeslot.length - 1; j++) {
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
                    if (result) {
                        var extCount = data[k].listChassis;
                        if (extCount != null && extCount != "" && extCount != undefined) {
                            var s = extCount.length;
                            for (var i = 0; i < extCount.length; i++) {

                                var r = extCount[i].split("/");
                                var a = r[0];
                                var b = r[1];
                                var c = r[2];
                                var h = timeslot[j].startTime.TotalHours;

                                if (h == a && c == "T") {
                                    tr.append('<td id=' + timeslot[j].timeRange + ' class="ColorGreen exist" title=' + b + '></td>');
                                    break;
                                }
                                else if (h == a) {
                                    tr.append('<td id=' + timeslot[j].timeRange + ' class="LightSeaGreen exist" title=' + b + '></td>');
                                    break;
                                }
                            }
                        }

                        result = false;
                    }
                    else {
                        tr.append('<td id=' + timeslot[j].timeRange + '></td>');
                    }

                }
                $('#tableschIDpolicy').append(tr);

                //$.fn.dataTable.ext.errMode = 'none';
                //$($.fn.dataTable.tables(true)).DataTable()
                //    .columns.adjust();
            }

            if (table != null) {
                for (var i = 1; i < table.rows.length; i++) {
                    for (var j = 1; j < table.rows[i].cells.length; j++) {
                        {
                            table.rows[i].cells[j].onclick = function () {
                                tableInsurancePolicydropFieldBooking(this);
                            };
                        }

                    }
                }
            }

            $('#tableschIDpolicy').dataTable({

                //  "fixedHeader": true,
                "scrollX": "100%",
                "scrollY": 320,
                "paging": false,
                "searching": false,
                "ordering": false,
                "bInfo": false
                //fixedColumns: {
                //    leftColumns: 1
                //}
            });
            $('#polcydloader').css({ 'display': 'none' });
            $('#policyContent').css({ 'display': 'block' });


        }, error(error) {
        }
    });
}

function tableInsurancePolicydropFieldBooking(tableCell) {
    vtempValueIns = parseInt(document.getElementById("tempValueIns1").value);

    console.log("tablecel id : " + tableCell.id);
    var className = tableCell.className;

    if (className == "" || className == "LightSeaGreen" || className == "LightSeaGreen exist" || className == "exist LightSeaGreen" || className == "exist") {
        $.confirm({
            title: 'Confirm!',
            closeIcon: true,
            content: 'Do you want Assign' + "<br>" + tableCell.id,
            buttons: {
                Yes: function () {
                    vstartValueIns = parseInt(document.getElementById("startValueIns1").value);
                    vendValueIns = parseInt(document.getElementById("endValueIns1").value);

                    if (vstartValueIns != 0 || vendValueIns != 0) {
                        $('#tableschIDpolicy').find('.ColorGreen').each(function () {
                            $(this).removeClass("ColorGreen");
                        });
                        $('#tableschIDpolicy').find('.exist').each(function () {
                            $(this).addClass("LightSeaGreen");
                        });
                        $('#startValueIns1').val('0');
                        $('#endValueIns1').val('0');
                        vstartValueIns = parseInt(document.getElementById("startValueIns1").value);
                        vendValueIns = parseInt(document.getElementById("endValueIns1").value);
                    }


                    if (vstartValueIns === 0) {
                        vstartValueIns = tableCell.parentElement.rowIndex;
                        document.getElementById("startValueIns1").value = tableCell.parentElement.rowIndex;
                    }
                    else if (vendValueIns === 0) {
                        vendValueIns = tableCell.parentElement.rowIndex;
                        document.getElementById("endValueIns1").value = vendValueIns;

                    }
                    $(tableCell).removeClass('LightSeaGreen');
                    $(tableCell).addClass('ColorGreen ');
                    if (vendValueIns === 0) {
                        vendValueIns = tableCell.parentElement.rowIndex;
                    }
                    if (vstartValueIns != 0 && vendValueIns != 0) {

                        if (vstartValueIns > vendValueIns) {
                            minValue = vendValueIns;
                            maxValue = vstartValueIns;
                        } else {
                            minValue = vstartValueIns;
                            maxValue = vendValueIns;
                        }

                        var table = document.getElementById("tableschIDpolicy");
                        var tableCellheader = table.rows[0].cells[tableCell.cellIndex];



                        document.getElementById("driverValueIns1").value = tableCellheader.id;
                        $('#newDriver').html(tableCellheader.innerText);

                        console.log("min and max value : " + minValue, maxValue);

                        for (var i = minValue; i < maxValue; i++) {
                            var tableCell1 = table.rows[i].cells[tableCell.cellIndex];
                            $(tableCell1).addClass('ColorGreen');
                        }

                        document.getElementById("startValueIns1").value = minValue;
                        document.getElementById("endValueIns1").value = maxValue;
                        minValue = '';
                        maxValue = '';

                    }


                    document.getElementById("tempIncreValueIns1").value = $('#tableschIDpolicy .ColorGreen').length;
                    document.getElementById("tempValueIns1").value = tableCell.cellIndex;
                    if ($('#tableschIDpolicy .ColorGreen').length === 0) {
                        document.getElementById("startValueIns1").value = 0;
                        document.getElementById("endValueIns1").value = 0;
                        document.getElementById("tempValueIns1").value = 0;
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

                    vstartValueIns = parseInt(document.getElementById("startValueIns1").value);
                    vendValueIns = parseInt(document.getElementById("endValueIns1").value);
                    var table1 = document.getElementById('tableschIDpolicy');
                    $(tableCell).removeClass('ColorGreen');
                    var className = tableCell.className;
                    if (className.includes("exist")) {
                        $(tableCell).addClass('LightSeaGreen');
                    }
                    if (vstartValueIns === tableCell.parentElement.rowIndex && vendValueIns === tableCell.parentElement.rowIndex) {
                        document.getElementById("startValueIns1").value = 0;
                        document.getElementById("endValueIns1").value = 0;
                        document.getElementById("tempValueIns1").value = 0;
                        document.getElementById("driverValueIns1").value = 0;
                    }

                    document.getElementById("tempIncreValueIns1").value = $('#tableschIDpolicy .ColorGreen').length;
                    document.getElementById("tempValueIns1").value = tableCell.cellIndex;
                    if ($('#tableschIDpolicy .ColorGreen').length === 0) {
                        document.getElementById("startValueIns1").value = 0;
                        document.getElementById("endValueIns1").value = 0;
                        document.getElementById("tempValueIns1").value = 0;
                        document.getElementById("driverValueIns1").value = 0;
                    }

                },
                No: function () {
                    $(tableCell).addClass('ColorGreen');

                }
            }
        });
    }




}
//Scheduler End

//clear controls
function clearAllControlls() {
    hidecheckboxDiv();
    clearCheckboxesDiv();
    clearcallmeLater();
    clearBookAppointment();
    clearpaid();
    clearOthers();
}

function clearpickupTypes() {
    $('#walkinLocation').prop('selectedIndex', 0);
    $('#insuranceAgentDataId').prop('selectedIndex', 0);
    $('#appointmentTime').val('');
    $('#insdate12345').val('');
    $('#pincodeId').val('');
    $('#fieldsummary').val('');
    $('#remarksList[12]').val('');
    $('#remarksList[13]').val('');
    $('#PurposeID').prop('selectedIndex', 0);
}

function clearBookAppointment() {
    var dealerCode = $('#PkDealercode').val();
    if (dealerCode != "PAWANHYUNDAI") {
        $('#paymentTypeID').prop('selectedIndex', 0);
    }
    //$('#coupon').prop('selectedIndex', 0);
    $('#discount').val('0');
    $('#premiumdiscount').val('0');
    $('#insCompanies').prop('selectedIndex', 0);
    $('#Field').attr('checked', false);
    $('#Walk-in').attr('checked', false);
    $('#Online').attr('checked', false);
    $("#NEWWalkinID").hide();
    $("#NEWFieldID").hide();
    $("#NEWOnlineID").hide();

    clearpickupTypes();
}
function clearpaid() {
    $('#paidCREId').val('');
    $('#paidCUSId').val('');
}
function clearOthers() {
    $('#DNC').attr('checked', false);
    $('#DNM').attr('checked', false);
    $('#creOtherId').val('');
    $('#cusOtherId').val('');
    $('#Escalation').attr('checked', false);
}

function clearcallmeLater() {
    $('#FollowUpDateinsurance').val('');
    $('#FollowUpTimeinsurance').val('');
    $('#listingForm_remarksList_0_').val('');
    $('#listingForm_commentsList_0_').val('');
}

function clearCheckboxesDiv() {
    clearalreadyrenewedDiv();
    clearvehiclesold();
    clearremaingcheckboxDiv();
    $('#listingForm_remarksList_13_').val('');
    $('#listingForm_commentsList_13_').val('');
}

function clearalreadyrenewedDiv() {
    const chbx = document.getElementsByName("insudisposition.renewalDoneBy");

    for (let i = 0; i < chbx.length; i++) {
        chbx[i].checked = false;
    }
    const chbx1 = document.getElementsByName("insudisposition.typeOfAutherization");

    for (let i = 0; i < chbx1.length; i++) {
        chbx1[i].checked = false;
    }
    $('#insudisposition_premimum').val('');
    $('#insudisposition_coverNoteNo').val('');
    $('#insuranceProvidedBy').prop('selectedIndex', 0);
    $('#dateofrnwal').val('');
    $('#dateofrennonauth').val('');
    $('#insuranceProvidedByOEM').prop('selectedIndex', 0);
    $('#inBoundLeadSourceSelectVal').prop('selectedIndex', 0);
    $('#insuranceProvidedUnAuth').prop('selectedIndex', 0);


}

function clearvehiclesold() {

    const chbx = document.getElementsByName("listingForm.PurchaseYes");

    for (let i = 0; i < chbx.length; i++) {
        chbx[i].checked = false;
    }

    const chbx1 = document.getElementsByName("listingForm.VehicleSoldYes");

    for (let i = 0; i < chbx1.length; i++) {
        chbx1[i].checked = false;
    }
    $('#customerFNameConfirm').val('');
    $('#customerLNameConfirm').val('');
    $('#Mobile1').val('');
    $('#Mobile2').val('');
    $('#STDCodeInput').val('');
    $('#LandlineInput').val('');
    $('#addreline1').val('');
    $('#addreline2').val('');
    $('#PinInput').val('');
    $('#vehicleRegNo').val('');
    $('#chassisNo').val('');
    $('#variant').val('');
    $('#model').val('');
    $('#saleDate').val('');
    $('#dealershipName').val('');
    $('#citydistance').val('');
    $('#commentsOtherRemarks').val('');
    $('#stateInput').prop('selectedIndex', 0);
    $('#cityInput').prop('selectedIndex', 0);
}
function clearInsuranceBookingDispo() {
    const chbx = document.getElementsByName("listingForm.dispoCustAns");

    for (let i = 0; i < chbx.length; i++) {
        chbx[i].checked = false;
    }
}

function clearremaingcheckboxDiv() {
    $('#noServiceReasonTaggedToComments').val('');
    $('#listingForm_noServiceReasonTaggedToComments1').val('');
    $('#commentsOtherRemarks').val('');
}

function backtoMain() {
    clearAllControlls();
    $("#DidYouTalkDiv").show();

    clearInsuranceBookingDispo();
    $("#alreadyserviceDIV").hide();
    $("#InsuOthersDiv").hide();// Other DIV hide
    $("#callMeLattteDiv").hide(); // call me later Div Hide
    $("#confirmInsuComments").hide(); // call me later Div Hide
    hidecheckboxDiv();

}
function hidecheckboxDiv() {
    $("#alreadyservicedDiv1").hide();
    $('#ServicedMyDealerDiv').hide();
    $('#ServicedAtOtherDealerDiv').hide();
    $('#VehicelSoldYesRNo').hide();
    $('#VehicleSoldClickYes').hide();
    $('#VehicleSoldClickNo').hide();
    $('#VehicelSoldQuestion').hide();
    $('#txtDissatisfiedwithpreviousservice').hide();
    $('#DisatisfiedPreQuestion').hide();
    $('#DistancefromDealerLocationDIV').hide();
    $('#DistanceFoRRQuestion').hide();
    $('#DissatisfactionwithSalesREmarksDiv').hide();
    $('#DisstisFiedSaleRQuestion').hide();
    $('#DissatisfactionwithInsuranceREmarksDiv').hide();
    $('#DisstisInsurancQuestion').hide();
    $('#OtherSeriveRemarks').hide();

    $('#OthersLastQuestion').hide();

    $('#AutorizedworkshopDIV').hide();
    $('#NonAutorizedworkshopDiv').hide();

    $('#PurchaseClickYes').hide();
}