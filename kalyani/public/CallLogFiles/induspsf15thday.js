
//For 2/15 Day  Workflow Changes

$("input[name='indusPsfInteraction.isContacted']").click(function () {
    testPSF = $(this).val();
    if (testPSF == "Yes") {
        $("#postserviceYesDiv").show();
        $("#postservicenoSpeechDiv").hide();
        clearallnodivControl();
    }
    if (testPSF == "No") {
        $("#postservicenoSpeechDiv").show();
        $("#postserviceYesDiv").hide();
        $("#postservicefeedbackyesDiv").hide();
        $("#postservicecalmelaterDiv").hide();
        $("#postservicesRemarksDiv").show();
        $("#postservicenoFeedbacks").hide();

        clearallyesdivcontrol();
    }
});

$("input[name='indusPsfInteraction.whatCustSaid']").click(function () {
    testPSF = $(this).val();
    if (testPSF == "FEEDBACK") {
        $("#postservicefeedbackyesDiv").show();
        $("#postservicecalmelaterDiv").hide();
        $("#postservicenoFeedbacks").hide();
        $("#postservicesRemarksDiv").show();

        clearallfolowuplater();
        clearallnofeedback();
    }
    if (testPSF == "NO FEEDBACK") {
        $("#postservicefeedbackyesDiv").hide();
        $("#postservicecalmelaterDiv").hide();
        $("#postservicesRemarksDiv").show();
        $("#postservicenoFeedbacks").show();
        clearallfeedback();
        clearallfolowuplater();
    }
    if (testPSF == "Call Me Later") {
        $("#postservicefeedbackyesDiv").hide();
        $("#postservicecalmelaterDiv").show();
        $("#postservicesRemarksDiv").show();
        $("#postservicenoFeedbacks").hide();

        clearallfeedback();
        clearallnofeedback();
    }
});


$("input[name$='indusPsfInteraction.PSFDisposition']").click(function () {
    var datais = $(this).val();

    if (datais == "NoOther") {
        $("#postservicefeedbackOtherDivShow").show();
    } else {
        $("#postservicefeedbackOtherDivShow").hide();
    }
});


//clearing all the input fields

function clearallqstmddls() {
    $(postservicefeedbackyesDiv).find("select").prop("selectedIndex", 0);

}
function clearallqstnrdo() {
    $("input:radio.cntrl").each(function (i) {
        $(this).attr('checked', false);
    });
}
function clearallfolowuplater() {
    $('#txtpostservicecallbackdate').val('');
    $('#txtpostservcecallbacktime').val('');
    $('#txtspsfcreRemarks').val('');
    $('#txtspsfCustremarks').val('');
}
function clearallnofeedback() {
    $('#txtspsfcreRemarks').val('');
    $('#txtspsfCustremarks').val('');
}
function clearallfeedback() {
    clearallqstnrdo();
    clearallqstnrdo();
}

function clearallyesdivcontrol() {
    clearallfeedback();
    clearallnofeedback();
    clearallfolowuplater();
    const chbx = document.getElementsByName("indusPsfInteraction.whatCustSaid");

    for (let i = 0; i < chbx.length; i++) {
        chbx[i].checked = false;
    }
}

function clearallnodivControl() {
    const chbx = document.getElementsByName("indusPsfInteraction.PSFDisposition");

    for (let i = 0; i < chbx.length; i++) {
        chbx[i].checked = false;
    }
    $("#postservicefeedbackOtherDivShow").hide();
    $('#txtspsfcreRemarks').val('');
    $('#txtspsfCustremarks').val('');
}



//submit validations
$("#btnsubmitpostservicefollowuplater").click(function () {
    var callbackdate = $("#txtpostservicecallbackdate").val();
    var callbacktime = $("#txtpostservcecallbacktime").val();
    if (callbackdate == "") {
        Lobibox.notify('warning', {
            msg: 'Please provide Call Back Date.'
        });
        return false;
    }
    if (callbacktime == "") {
        Lobibox.notify('warning', {
            msg: 'Please provide Call Back Time'
        });
        return false;
    }
});

$("#btnnoncontactpostservicesubmits").click(function () {

    var chkincSubmit = 0;
    $('[name="indusPsfInteraction.PSFDisposition"]').each(function () {
        if ($(this).is(':checked')) chkincSubmit++;
    });
    if (chkincSubmit == 0) {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please Select any one Reason.'
        });
        return false;
    }
    else {
        var selected = $("input[name='indusPsfInteraction.PSFDisposition']:checked").val();
        if (selected == "NoOther") {
            var textNoOthers = $('#txtpsfservicenoncontactotherreason').val();
            if (textNoOthers == '' || textNoOthers == undefined) {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Pleasy Enter Other Reason.'
                });
                return false;
            }
        }
    }

});

$("#btnpostservicefeedbackSubmit").click(function () {
    var totalmndtryQustn = $('#txttotalpostservicemandatroryQuestions').val();
    var length = $('.rdomandatory_True:checked').length;
    if (length < totalmndtryQustn) {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please select mandatory question(s)*.'

        });
        return false;
    }

});