var cntrContacted = $("input[name='indusPsfInteraction.isContacted']");
var cntrCustSay = $("input[name='indusPsfInteraction.whatCustSaid']")
var divIsContacted = $('#isContacted');
var cmpNature = "",leadSelected;
var pickUpStatus = "";
var isContacted = "", selectedNonContact = "";
var complaint = "", rm_creId="";

$(document).ready(function () {
    $("input[name='indusPsfInteraction.isContacted']").click(function () {
        isContacted = $(this).val();
        $('#isContacted').hide();
        if (isContacted == "Yes") {
            $('#compResolveDiv').show();

        }
        else {
            $('#NotSpeachDiv').show();
        }
        $('.characterLimit').val('');
    });

    //No Other
    $('input[name="indusPsfInteraction.PSFDisposition"]').click(function () {
        selectedNonContact = $(this).val();

        if (selectedNonContact == "Other") {
            $('#NoOthers').show();
        }
        else {
            $('#NoOthers').hide();
            $('#NoOthersText1').val('');
        }
    });

    $("input[name='indusPsfInteraction.whatCustSaid']").click(function () {
        var custRespo = $(this).val();

        if (custRespo == "Complaint Resolution Status") {
            //$(cntrContacted).hide();
            $('#compResolveDiv').show();
            $('#followUpDiv').hide();
            $('#noFeedDiv').hide();
            $('#isContacted').hide();
            $('#custSay').show();
            $('.noFeedText').val('');
            $('.followLater').val('');
            

        }
        else if (custRespo == "FollowUp Later") {
            //$(cntrContacted).hide();
            $('#compResolveDiv').hide();
            $('#interDptFeedDiv').hide();
            $('#isContacted').hide();
            $('#followUpDiv').show();
            $('#LeadDiv').hide();
            $('#YesContactedDiv').hide();
            $('#complaintResoluation').prop("checked", false);
            $('input[name="compInteractions.resolutionMode"]').prop('checked', false);
            $('input[name="listingForm.LeadYes"]').prop('checked', false);
            $('.upsellLeadSelectDivSB').prop('checked', false);

            //rework, resolve disabling
            $('#reScheduleDiv').hide();
            $('input[name="compInteractions.reworkMode"]').prop('checked', false);
            $('.reworkCntrl').val('');
            $('.reworkCntrlRadio').prop('checked', false);

            $('#resolvedDiv').hide();
            $('.resolvedCntr').val('');
            $('.resolvedCntrRadio').prop('checked', false);
            $('#cmpNature').multiselect('refresh');
            $('#cmpNaturehidden').val('');
        }
    });

    $("input[name='compInteractions.resolutionMode']").click(function () {
        complaint = $(this).val();
        $('#compResolveDiv').hide();
        $('#YesContactedDiv').hide();
        $('#btncustSayBack').show();
        $('#isContacted').hide();
        $('#rdFollowLater').prop('checked', false);
        $('#complaintResoluation').prop("checked", true);

        if (complaint == "Complaint not valid/Customer educated") {
            $('#interDptFeedDiv').show();
            $('#reScheduleDiv').hide();
            $('#resolvedDiv').hide();
            $('.EscRM').hide();
            clearCustSay('upsell');
        }
        else if (complaint == "Schedule a re-visit") {
            $('#reScheduleDiv').show();
            $('#interDptFeedDiv').hide();
            $('#resolvedDiv').hide();
            $('.EscRM').hide();
            clearCustSay('all', 'reSchedule');
        }
        else if (complaint == "Resolved") {
            $('#resolvedDiv').show();
            $('#reScheduleDiv').hide();
            $('#interDptFeedDiv').hide();
            $('.EscRM').hide();
            clearCustSay('all', 'resolve');
        }
        else if (complaint == "Escalate to RM") {
            $('#interDptFeedDiv').show();
            $('.EscRM').show();
            $('#reScheduleDiv').hide();
            $('#resolvedDiv').hide();
            clearCustSay('rm');
        }
    });
});

function notAnsback() {
    $("input[name='indusPsfInteraction.PSFDisposition']").prop("checked", false);
    $(".NoOthersText1").val('');
    $("#NotSpeachDiv").hide();
    $("#NoOthers").hide();
    $('#isContacted').show();
    $('.characterLimit').val('');
}

function backToMain() {
    //$('#feedbackDiv').hide();
    $('#compResolveDiv').hide();
    $('#isContacted').show();
    $('input[name="compInteractions.resolutionMode"]').prop('checked', false);
    $(cntrCustSay).prop("checked", false);
    $(cntrContacted).prop("checked", false);
}

function backFromCallMeLater() {
    $('.followLater').val('');
    $('#isContacted').hide();
    $('#followUpDiv').hide();
    $('#compResolveDiv').show();

    //$(cntrCustSay).prop("checked", false);
    //$(cntrContacted).prop("checked", false);
}

$('#RMList').change(function () {
    var id = $(this).attr('id');
    rm_creId = $('#' + id + " option:selected").val();

});

$('#discountGiven').click(function () {

    if ($(this).is(':checked')) {

        $('#hdDisCount').val('1');
        $("#discDiv").show();

    }
    else {
        $("#discDiv").hide();

        $('#hdDisCount').val('');
    }

});

function clearCustSay(partName, exceptPart) {
    if (partName == 'all') {
        
        if (exceptPart == "reSchedule") {
            $('input[name="listingForm.LeadYes"]').prop('checked', false);
            $('.upsellLeadSelectDivSB').prop('checked', false);
            $('.upsellLeadSelectDivSB').each(function () {
                $(this).prop('checked', false);
                $('#upsellLeadSelectSB_' + $(this).attr('data-upsellId')).hide();
            });

            $('.resolvedCntr').val('');
            $('.resolvedCntrRadio').prop('checked', false);
            $('#cmpNature').multiselect('refresh');
        }
        else if (exceptPart == "resolve") {
            $('input[name="listingForm.LeadYes"]').prop('checked', false);
            $('.upsellLeadSelectDivSB').prop('checked', false);
            $('.upsellLeadSelectDivSB').each(function () {
                $(this).prop('checked', false);
                $('#upsellLeadSelectSB_' + $(this).attr('data-upsellId')).hide();
            });

            $('input[name="compInteractions.reworkMode"]').prop('checked', false);
            $('.reworkCntrl').val('');
            $('.reworkCntrlRadio').prop('checked', false);
            $('#txtVOC').val('');
            $('#pickUpDiv').hide();
        }
        $('#cmpNaturehidden').val('');
    }
    else if (partName == "upsell" || partName=="rm") {
        $('input[name="listingForm.LeadYes"]').prop('checked', false);
        $('.upsellLeadSelectDivSB').prop('checked', false);
        $('.upsellLeadSelectDivSB').each(function () {
            $(this).prop('checked', false);
            $('#upsellLeadSelectSB_' + $(this).attr('data-upsellId')).hide();
        });
        $('input[name="compInteractions.reworkMode"]').prop('checked', false);
        $('.reworkCntrl').val('');
        $('.reworkCntrlRadio').prop('checked', false);
        $('#txtVOC').val('');
        $('#pickUpDiv').hide();

        $('.resolvedCntr').val('');
        $('.resolvedCntrRadio').prop('checked', false);
        $('#cmpNature').multiselect('refresh');
        $('#cmpNaturehidden').val('');
        if (partName == "rm") {
            $('.EscRM').show();
        } else {
            $('.EscRM').hide();
        }
        $('#LeadDiv').hide();
        $('#cmpNaturehidden').val('');
    }
}

function manageVisit(pickUp) {
    pickUpStatus = pickUp;
    if (pickUp == "Self") {
        $('#pickUpDiv').hide();
    }
    else {
        $('#pickUpDiv').show();
    }
}



function custBack(divName) {

    //$('#compResolveDiv').hide();
    //$('#interDptFeedDiv').hide();
    //$('#isContacted').show();
    //$('#followUpDiv').hide();
    //$('#LeadDiv').hide();
    //$('#YesContactedDiv').hide();

    $('#compResolveDiv').show();
    $('#YesContactedDiv').show();


    //$(cntrCustSay).prop('checked', false);
    //$(cntrContacted).prop('checked', false);
    //$('input[name="rework.resolutionMode"]').prop('checked', false);

    if (divName == "rework") {
        $('#reScheduleDiv').hide();
        $('input[name="compInteractions.reworkMode"]').prop('checked', false);
        $('.reworkCntrl').val('');
        $('.reworkCntrlRadio').prop('checked', false);
    }
    else if (divName == "resolved") {
        $('#resolvedDiv').hide();
        $('.resolvedCntr').val('');
        $('.resolvedCntrRadio').prop('checked', false);
        $('#cmpNature').multiselect('refresh');
    }
    else if (divName == "upsell") {
        $('input[name="listingForm.LeadYes"]').prop('checked', false);
        $('.upsellLeadSelectDivSB').each(function () {
            $(this).prop('checked', false);
            $('#upsellLeadSelectSB_' + $(this).attr('data-upsellId')).hide();
        });
        $('.upsellLeadSelectDivSB').prop('checked', false);
        $('#LeadDiv').hide();
        $('#interDptFeedDiv').hide();
        $("#finalRemarks").val('');
    }
}

function custNext(divName) {
    var emptyCheck = false;
        
    if (divName == 'rework') {

        $('.reworkCntrl').each(function () {
            var value = "";

            if ($(this).prop('type') == "radio") {
                value = 0;
            }
            else {
                value = $(this).val();
            }
            
            if (value == "") {
                var data_field = $(this).attr('data-field');
                if (data_field == null || data_field == undefined) {
                    
                }
                else {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please enter data for ' + data_field
                    });
                    emptyCheck = true;
                    return false;
                }
            }
        });


        if (pickUpStatus == "") {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Select Visit Status'
            });
            emptyCheck = true;
            return false;
        }

        if (emptyCheck == false) {
            $('#reScheduleDiv').hide();
            $('#interDptFeedDiv').show();

            $('#btncustSayBack').hide();
            $('#btnreScheduleBack').show();
            $('#btnresolvedback').hide();

        }
    }
    else if('resolved') {
        $('.resolvedCntrl').each(function () {
            if ($(this).val() == "") {
                var data_field = $(this).attr('data-field');
                if (data_field == null || data_field == undefined) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Ensure all data to be selected'
                    });
                }
                else {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please enter data for ' + data_field
                    });
                }
                emptyCheck = true;
                return false;
            }
        });

        if (emptyCheck == false) {
            $('#resolvedDiv').hide();
            $('#interDptFeedDiv').show();
            $('#btncustSayBack').hide();
            $('#btnreScheduleBack').hide();
            $('#btnresolvedback').show();
        }
    }
}


function custSayBack(divName) {

    $('input[name="listingForm.LeadYes"]').prop('checked', false);
    $('.upsellLeadSelectDivSB').each(function () {
        $(this).prop('checked', false);
        $('#upsellLeadSelectSB_' + $(this).attr('data-upsellId')).hide();
    });
    $('#LeadDiv').hide();
    $('#interDptFeedDiv').hide();
    if (divName == "reSchedule") {
        $('#reScheduleDiv').show();
    }
    else if (divName == "resolved") {
        $('#resolvedDiv').show();
    }
}

$("input[name='listingForm.LeadYes']").click(function () {
    mLeadYes = $(this).val();
    if (mLeadYes == "Capture Lead Yes") {
        $("#LeadDiv").show();
        leadSelected = "Yes";
    } else {
        $("#LeadDiv").hide();
        leadSelected = "No";
    }
});

function natureChange(id) {
    cmpNature = $('#' + id +' option:selected').toArray().map(item => item.value).join();
    $('#cmpNaturehidden').val(cmpNature);
}

$('#custKnowledge').click(function (e) {

    if (isContacted == "Yes") {
        var atLeastOneIsChecked = 0;
        $('[name="listingForm.LeadYes"]').each(function () {
            if ($(this).is(':checked')) atLeastOneIsChecked++;
        });

        if (atLeastOneIsChecked == 0) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please select anyone of the option from Inter Department Lead'
            });
            e.preventDefault();
            return false;
        }

        if (complaint == "Escalate to RM") {
            //if ($('#txtVOC').val() == '' || $('#txtVOC').val() == null) {
            //    Lobibox.notify('warning', {
            //        continueDelayOnInactiveTab: true,
            //        msg: 'Please enter VOC.'
            //    });
            //    e.preventDefault();
            //    return false;
            //}

            if (rm_creId == '' || rm_creId == null) {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please select RM.'
                });
                e.preventDefault();
                return false;
            }
        }
    }
});

$('#nonContactValidationPSF').click(function (e) {


    var chkincSubmit = 0;
    $('[name="indusPsfInteraction.PSFDisposition"]').each(function () {
        if ($(this).is(':checked')) chkincSubmit++;
    });
    if (chkincSubmit == 0) {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please Select Anyone of the option.'
        });
        return false;

    }

    //if (isContacted == "Yes") {
    //    if (selectedNonContact == "") {
    //        Lobibox.notify('warning', {
    //            continueDelayOnInactiveTab: true,
    //            msg: 'Please Select Anyone of the option'
    //        });
    //        e.preventDefault();
    //        return false;
    //    }
    //}

});
