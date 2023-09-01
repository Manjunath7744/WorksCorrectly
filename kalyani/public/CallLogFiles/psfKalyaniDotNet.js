$("input[name='indusPsfInteraction.isContacted']").click(function () {
    PSFtest = $(this).val();
    if (PSFtest == "Yes") {

        $("#PSFYesTalk").show();
        $("#PSFNotSpeachDiv").hide();
        $("#SaveInCompleteServeyDiv").hide();
        $("#remarksDivDisplay").hide();
        $("input:radio[name='indusPsfInteraction.PSFDispositon']").each(function (i) {
            this.checked = false;
        });

    }
    if (PSFtest == "No") {
        $("#PSFNotSpeachDiv").show();
        $("#PSFYesTalk").hide();
        $("#SaveInCompleteServeyDiv").hide();
        $("#remarksDivDisplay").show();
        $("input:radio[name='indusPsfInteraction.whatCustSaid']").each(function (i) {
            this.checked = false;
        });

    }

});

$('.stRd').click(function () {
    if ($(this).val() == "0" || $(this).val() == "-50" || $(this).val() == "50") {
        $('#complaintCRESelect').show();
    }
    else {
        $('#creComplaintCRE').val('');
        $('#complaintCRESelect').hide();
    }
});

$("input[name='indusPsfInteraction.PSFDispositon']").click(function () {
    if ($(this).val() == "NoOther") {
        $('#PSFNoOthers').show();
    }
    else {
        $('#PSFNoOthers').hide();
        $("input[name='indusPsfInteraction.OtherComments']").val('');
    }
});

$("input[name='indusPsfInteraction.whatCustSaid']").click(function () {
    varPSFYes = $(this).val();
    if (varPSFYes == "PSF_Yes" || varPSFYes == "Call Me Later" || varPSFYes == "Not Interested" || varPSFYes == "No Resolution - Closed" || varPSFYes == "Re-Work" || varPSFYes == "Resolved" || varPSFYes == "ConfirmStatus") {
        psfNamaskar();
    }
});

function psfNamaskar() {
    //alert(" varPSFYes "+varPSFYes);
    if (varPSFYes != "") {
        if (varPSFYes == "PSF_Yes") {
            $("#PSFYesNamaskarYesDiv").show();
            $("#PSFYesNamaskarNoDiv").hide();
            $("#PSFconnectCall1").hide();
            $("#PSFYesTalk").hide();
            $("#NotInterestedBtns").hide();
            $("#SaveInCompleteServeyDiv").show();
            $("#NoResolutionClosedSelId").hide();
            $("#ReworkDivId").hide();
            $("#ResolvedDivId").hide();
            $("#ConfirmStatusDivId").hide();
            $("#remarksDivDisplay").show();


        }
        else if (varPSFYes == "Call Me Later") {
            $("#PSFYesNamaskarNoDiv").show();
            $("#PSFYesNamaskarYesDiv").hide();
            $("#PSFconnectCall1").hide();
            $("#PSFYesTalk").hide();
            $("#NotInterestedBtns").hide();
            $("#SaveInCompleteServeyDiv").hide();
            $("#NoResolutionClosedSelId").hide();
            $("#ReworkDivId").hide();
            $("#ResolvedDivId").hide();
            $("#ConfirmStatusDivId").hide();
            $("#remarksDivDisplay").show();


        }
        else if (varPSFYes == "Not Interested") {
            $("#PSFYesNamaskarNoDiv").hide();
            $("#PSFYesNamaskarYesDiv").hide();
            $("#PSFconnectCall1").hide();
            $("#PSFYesTalk").hide();
            $("#NotInterestedBtns").show();
            $("#SaveInCompleteServeyDiv").hide();
            $("#NoResolutionClosedSelId").hide();
            $("#ReworkDivId").hide();
            $("#ResolvedDivId").hide();
            $("#ConfirmStatusDivId").hide();
            $("#remarksDivDisplay").show();
        }
        else if (varPSFYes == "No Resolution - Closed") {

            $("#PSFYesNamaskarNoDiv").hide();
            $("#PSFYesNamaskarYesDiv").hide();
            $("#PSFconnectCall1").hide();
            $("#PSFYesTalk").hide();
            $("#NotInterestedBtns").hide();
            $("#SaveInCompleteServeyDiv").hide();
            $("#NoResolutionClosedSelId").show();
            $("#ReworkDivId").hide();
            $("#ResolvedDivId").hide();
            $("#ConfirmStatusDivId").hide();
            $("#remarksDivDisplay").show();

        }
        else if (varPSFYes == "Re-Work") {

            $("#PSFYesNamaskarNoDiv").hide();
            $("#PSFYesNamaskarYesDiv").hide();
            $("#PSFconnectCall1").hide();
            $("#PSFYesTalk").hide();
            $("#NotInterestedBtns").hide();
            $("#SaveInCompleteServeyDiv").hide();
            $("#NoResolutionClosedSelId").hide();
            $("#ReworkDivId").show();
            $("#ResolvedDivId").hide();
            $("#ConfirmStatusDivId").hide();
            $("#remarksDivDisplay").show();
            ajaxCallToLoadWorkShop();

        }
        else if (varPSFYes == "Resolved") {

            $("#PSFYesNamaskarNoDiv").hide();
            $("#PSFYesNamaskarYesDiv").hide();
            $("#PSFconnectCall1").hide();
            $("#PSFYesTalk").hide();
            $("#NotInterestedBtns").hide();
            $("#SaveInCompleteServeyDiv").hide();
            $("#NoResolutionClosedSelId").hide();
            $("#ReworkDivId").hide();
            $("#ConfirmStatusDivId").hide();
            $("#ResolvedDivId").show();
            $("#remarksDivDisplay").show();

        }
        else if (varPSFYes == "ConfirmStatus") {

            $("#PSFYesNamaskarNoDiv").hide();
            $("#PSFYesNamaskarYesDiv").hide();
            $("#PSFconnectCall1").hide();
            $("#PSFYesTalk").hide();
            $("#NotInterestedBtns").hide();
            $("#SaveInCompleteServeyDiv").hide();
            $("#NoResolutionClosedSelId").hide();
            $("#ReworkDivId").hide();
            $("#ResolvedDivId").hide();
            $("#divResolveAction").show();
        }
    }
}

$('#BackTospeak').on('click', function () {
    //$( "input[name='modeOfService']" ).prop( "checked", false );

    $("#PSFconnectCall1").show();
    $("#PsfSelfDriveINYes").hide();
    $("#PSFYesTalk").show();
    $("#PSFYesNamaskarYesDiv").hide();
    $("#remarksDivDisplay").hide();


    $("#SaveInCompleteServeyDiv").hide();
    $("#psfYesNamaskarbtn").prop("checked", false);

    $('.controlBtn').prop("checked", false);
    $('.clsImprove').prop("checked", false);
    $('#hdImprovement').prop("checked", false);
    $('.upsellLeadTags').val('');
    if ($('#interlead').is(":checked")) {
        $('#interlead').trigger('click');
    }

    $('.remarks1').val('');
    $('#complaintCRESelect').hide();

    $('.upsellLeadSelectDivSB').each(function () {
        if ($(this).is(":checked")) {
            $(this).trigger('click');
        }
    });
});


$('#interlead').click(function () {
    if ($(this).is(":checked")) {
        $('#leadFeedback').show();
        $('#hdUpsell').val('Yes');
    }
    else {
        $('#leadFeedback').hide();
        $('#hdUpsell').val('No');
    }
});


$('#BackTo10thQuestionMMSSubmitKalyani').click(function () {
    var customerCat = document.getElementById("customerCat").value;

    var v1FB = $("input[name='indusPsfInteraction.modeOfServiceDone']:checked").val();
    var v8FB = $("input[name='indusPsfInteraction.qFordQ9']:checked").val();

    //New Validtions
    if ($("input[name='indusPsfInteraction.modeOfServiceDone']").is(":checked")) {
        if (v1FB == "0" || v1FB == "-50" || v1FB == "50") {
            if ($('#creComplaintCRE').val() == '' || $('#creComplaintCRE').val() == null) {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please rate Complaint Manager.'
                });
                return false;
            }
        }
    }
    else {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please rate interactions and experiences at our Service Center.'
        });
        return false;
    }

    if (customerCat != "BODYSHOP") {

        if (!$("input[name='indusPsfInteraction.qFordQ1']").is(":checked")) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please rate experience and appointment process.'
            });
            return false;
        }

        if (!$("input[name='indusPsfInteraction.qFordQ3']").is(":checked")) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please rate quality of the work performed.'
            });
            return false;
        }

        if (!$("input[name='indusPsfInteraction.qFordQ5']").is(":checked")) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please rate washing & cleanliness.'
            });
            return false;
        }

        if (!$("input[name='indusPsfInteraction.qFordQ6']").is(":checked")) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please rate "Value for Money" of our service.'
            });
            return false;
        }
    }

    if (customerCat == "BODYSHOP") {
        if (!$("input[name='indusPsfInteraction.bodyrepairthrough']").is(":checked")) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please selectu recommend Kalyani Motors to a friends. vehicle body repair done through.'
            });
            return false;
        }

        if (!$("input[name='indusPsfInteraction.experienceOnIns']").is(":checked")) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please rate overall experience on insurance claim.'
            });
            return false;
        }

        if (!$("input[name='indusPsfInteraction.qualityofwork']").is(":checked")) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please rate quality of the painting & denting work.'
            });
            return false;
        }
    }

    if (!$("input[name='indusPsfInteraction.qFordQ2']").is(":checked")) {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please rate service advisor based on SA(s) ability.'
        });
        return false;
    }

    if (!$("input[name='indusPsfInteraction.qFordQ7']").is(":checked")) {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please rate you recommend Kalyani Motors to a friends.'
        });
        return false;
    }

    if (!$("input[name='indusPsfInteraction.qFordQ9']").is(":checked")) {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please select Complaint attributed to?.'
        });
        return false;
    }
    else {
        if (v8FB == 'Service Advisor and Technician' || v8FB == 'Only Service Advisor' || v8FB == 'Workshop' || v8FB == 'Technical Advisor' || v8FB == 'None') {
            var improArray = [];
            var data = "";
            
            $.blockUI();
        }
        else {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please select complaint attributed to.'
            });
            return false;
        }
    }

    $('.clsImprove').each(function () {
        if ($(this).is(":checked")) {
            improArray.push($(this).val());
        }
    });

    if (improArray.length > 0) {
        data = improArray.join(',');
        $('#hdImprovement').val(data);
    }
    else {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please select improvement in our service center.'
        });
        return false;
    }
    //Old Validations
    //var customerCat = document.getElementById("customerCat").value;
    //if (v1FB == '150' || v1FB == '100' || v1FB == '50' || v1FB == '0' || v1FB == '-50') {
    //    if (v1FB == "0" || v1FB == "-50" || v1FB == "50") {
    //        if ($('#creComplaintCRE').val() == '' || $('#creComplaintCRE').val() == null) {
    //            Lobibox.notify('warning', {
    //                continueDelayOnInactiveTab: true,
    //                msg: 'Please select Complaint Manager.'
    //            });
    //            return false;
    //        }
    //    }
    //    if (v2FB == '150' || v2FB == '100' || v2FB == '50' || v2FB == '0' || v2FB == '-50') {
    //        if (v3FB == '150' || v3FB == '100' || v3FB == '50' || v3FB == '0' || v3FB == '-50') {
    //            if (v4FB == '150' || v4FB == '100' || v4FB == '50' || v4FB == '0' || v4FB == '-50') {
    //                if (v5FB == '150' || v5FB == '100' || v5FB == '50' || v5FB == '0' || v5FB == '-50') {
    //                    if (v6FB == '150' || v6FB == '100' || v6FB == '50' || v6FB == '0' || v6FB == '-50') {
    //                        if (v7FB == '150' || v7FB == '100' || v7FB == '50' || v7FB == '0' || v7FB == '-50') {
    //                            if (v8FB == 'Service Advisor and Technician' || v8FB == 'Only Service Advisor' || v8FB == 'Workshop' || v8FB == 'Technical Advisor' || v8FB == 'None') {
    //                                var improArray = [];
    //                                var data = "";
    //                                $('.clsImprove').each(function () {
    //                                    if ($(this).is(":checked")) {
    //                                        improArray.push($(this).val());
    //                                    }
    //                                });

    //                                if (improArray.length > 0) {
    //                                    data = improArray.join(',');
    //                                    $('#hdImprovement').val(data);
    //                                }
    //                                $.blockUI();
    //                            }
    //                            else {
    //                                Lobibox.notify('warning', {
    //                                    continueDelayOnInactiveTab: true,
    //                                    msg: 'Please select question 10 for feedback rating.'
    //                                });
    //                                return false;
    //                            }
    //                        }
    //                        else {
    //                            Lobibox.notify('warning', {
    //                                continueDelayOnInactiveTab: true,
    //                                msg: 'Please select question 8 for feedback rating.'
    //                            });
    //                            return false;
    //                        }
    //                    }
    //                    else {
    //                        Lobibox.notify('warning', {
    //                            continueDelayOnInactiveTab: true,
    //                            msg: 'Please select question 7 for feedback rating.'
    //                        });
    //                        return false;
    //                    }
    //                }
    //                else {
    //                    Lobibox.notify('warning', {
    //                        continueDelayOnInactiveTab: true,
    //                        msg: 'Please select question 6 for feedback rating.'
    //                    });
    //                    return false;
    //                }
    //            }
    //            else {
    //                Lobibox.notify('warning', {
    //                    continueDelayOnInactiveTab: true,
    //                    msg: 'Please select question 4 for feedback rating.'
    //                });
    //                return false;
    //            }
    //        }
    //        else {
    //            Lobibox.notify('warning', {
    //                continueDelayOnInactiveTab: true,
    //                msg: 'Please select question 3 for feedback rating.'
    //            });
    //            return false;
    //        }
    //    }
    //    else {
    //        Lobibox.notify('warning', {
    //            continueDelayOnInactiveTab: true,
    //            msg: 'Please select question 2 for feedback rating.'
    //        });
    //        return false;
    //    }
    //} else {
    //    Lobibox.notify('warning', {
    //        continueDelayOnInactiveTab: true,
    //        msg: 'Please select question 1 for feedback rating.'
    //    });
    //    return false;
    //}
});


$("#BackToDidUtlakPSF1").click(function () {
    $('#remarksValueForNoIntrst').val('');
    $("#PSFYesTalk").show();
    $("#PSFconnectCall1").show();
    $("#NotInterestedBtns").hide();
    $("#remarksDivDisplay").hide();

    $('.remarks1').val('');
    $('#indusPsfInteraction.afterServiceComments').val('');

    $('#custFB').val('');
    $('#creRemarks').val('');
});

$('#notInterestedSubmit').click(function () {

    var custFBD = $("#custFB").val();
    var cretFB = $("#creRemarks").val();
    if (custFBD == '') {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please Enter Customer Feedback.'
        });
        return false;

    }
    if (cretFB == '') {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please Enter CRE Remark.'
        });
        return false;

    }
    $.blockUI();
});

//------------------CallMeLater -----------------

$('#notCallMeLaterSubmit').click(function () {
    var dateVal = $('#selDate').val();
    var timeVal = $('#FollowUpTime').val();
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
    $.blockUI();
});

$("#BackToDidUtlakPSF").click(function () {
    $('#time_ToIn').val('');
    $('#time_FromIn').val('');
    $("#PSFYesTalk").show();
    $("#PSFconnectCall1").show();
    $("#PSFYesNamaskarNoDiv").hide();
    $("#remarksDivDisplay").hide();
    $('#selDate').val('');
    $('#FollowUpTime').val('');

    $('#custFB').val('');
    $('#creRemarks').val('');

    $('input[name="indusPsfInteraction.whatCustSaid"]').prop('checked', false);
});


//Psf Complaint Else(not in psf Days*6th) Part Dtart

$('#BackTospeakResolution').on('click', function () {


    $('input[name="indusPsfInteraction.reasonOfDissatification"]').prop('checked', false);
    $('.remarks1').val('');
    $("#PSFconnectCall1").show();
    $("#PSFYesTalk").show();
    $("#PSFYesNamaskarYesDiv").hide();
    $("#NoResolutionClosedSelId").hide();
    $("#remarksDivDisplay").hide();
    $("#psfYesNamaskarbtn").prop("checked", false);
});

$('#noresolutionSubmit').click(function () {
    var chkincSubmit = 0;
    $('[name="indusPsfInteraction.reasonOfDissatification"]').each(function () {
        if ($(this).is(':checked')) chkincSubmit++;
    });
    if (chkincSubmit == 0) {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please check one.'
        });
        return false;


    }
    $.blockUI();

});


$("#BackToAfterresolution").click(function () {
    $('#remarksValueForNoIntrst').val('');
    $("#PSFYesTalk").show();
    $("#PSFconnectCall1").show();
    $("#ConfirmStatusDivId").hide();
    $("#remarksDivDisplay").hide();


});

$('#BackToRework').on('click', function () {

    $('#reworkmodeId').val('');
    $('#schDateTimeId').val('');
    $('#workshop_id_Data').val('');
    $('.remarks1').val('');

    $("#PSFconnectCall1").show();
    $("#PSFYesTalk").show();
    $("#PSFYesNamaskarYesDiv").hide();
    $("#ReworkDivId").hide();
    $("#remarksDivDisplay").hide();
    $("#psfYesNamaskarbtn").prop("checked", false);
});

$('#reWorkSubmit').click(function () {


    var workVal = $('#workshop_id_Data').val();
    var rewMoVal = $('#reworkmodeId').val();
    var rewAddVal = $('#reworkAddId').val();
    var schDVal = $('#schDateId').val();
    var schTVal = $('#schTimeId').val();
    if (workVal == '' || rewMoVal == '' || rewAddVal == '' || schDVal == '' || schTVal == '') {
        Lobibox.notify('warning', {
            msg: 'Please enter all Fields.'
        });
        return false;
    }
    $.blockUI();

});

$('#BackToResolved').on('click', function () {

    $('#resolutionDateTime').val('');
    $('#resolvedById').val('');
    $('#resolutionModeId').val('');
    $('#discID').val('0');
    $('#benefId').val('No Benefits Applied');
    $('#Compliant_Category_id').val('');
    $('.remarks1').val('');

    $("#PSFconnectCall1").show();
    $("#PSFYesTalk").show();
    $("#PSFYesNamaskarYesDiv").hide();
    $("#ResolvedDivId").hide();
    $("#remarksDivDisplay").hide();
    $("#psfYesNamaskarbtn").prop("checked", false);
});

$('#reSolvedSubmit').click(function () {


    var dtVal = $('#dateandtimepicker').val();
    var resByVal = $('#resolvedById').val();
    var resModVal = $('#resolutionModeId').val();
    var DVal = $('#discID').val();
    var benVal = $('#benefId').val();
    var comVal = $('#Compliant_Category_id').val();

    if (dtVal == '' || resByVal == '' || resModVal == '' || DVal == '' || benVal == '' || comVal == '') {
        Lobibox.notify('warning', {
            msg: 'Please enter all Fields.'
        });
        return false;
    }

    $.blockUI();
});