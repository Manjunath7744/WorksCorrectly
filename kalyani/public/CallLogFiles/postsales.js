
//For 2/15 Day  Workflow Changes

$("input[name='postsalesdispositions.isContacted']").click(function () {
    testPSF = $(this).val();
    if (testPSF == "Yes") {
        $("#postsalesyesDiv").show();
        $("#postsalesinterdptmntleadfeedbckDiv").show();
        $("#postsalesnospeechDiv").hide();
        clearallnodivControl();
    }
    if (testPSF == "No") {
        $("#postsalesnospeechDiv").show();
        $("#postsalesyesDiv").hide();
        $("#postsalesfeedbackyesDiv").hide();
        $("#postsalescalmelaterDiv").hide();
        $("#postsalesRemarksDiv").show();
        $("#postsalesnofeedbackDiv").hide();
        $("#postsalesinterdptmntleadfeedbckDiv").hide();

        clearallyesdivcontrol();
        clearthankcallDiv();
    }
});

$("input[name='postsalesdispositions.whatcustsays']").click(function () {
    testPSF = $(this).val();
    if (testPSF == "FEEDBACK") {
        $("#postsalesfeedbackyesDiv").show();
        $("#postsalescalmelaterDiv").hide();
        $("#postsalesnofeedbackDiv").hide();
        $("#postsalesRemarksDiv").show();

        clearallfolowuplater();
        clearallnofeedback();
    }
    if (testPSF == "NO FEEDBACK") {
        $("#postsalesfeedbackyesDiv").hide();
        $("#postsalescalmelaterDiv").hide();
        $("#postsalesRemarksDiv").show();
        $("#postsalesnofeedbackDiv").show();
        clearallfeedback();
        clearallfolowuplater();
    }
    if (testPSF == "Call Me Later") {
        $("#postsalesfeedbackyesDiv").hide();
        $("#postsalescalmelaterDiv").show();
        $("#postsalesRemarksDiv").show();
        $("#postsalesnofeedbackDiv").hide();

        clearallfeedback();
        clearallnofeedback();
        clearcomplaintresolvecontrol();
        clearthankcallDiv();
    }
    if (testPSF == "Complaint Resolution Status") {
        $("#postsalesthankcallDiv").show();
        $("#postsalescalmelaterDiv").hide();
        $("#postsalesfeedbackyesDiv").hide();
        $("#postsalescalmelaterDiv").hide();
        $("#postsalesnofeedbackDiv").hide();
        $("#postsalesRemarksDiv").show();
        clearallfolowuplater();
        clearallnofeedback();
    }
});


$("input[name$='postsalesdispositions.PSFDispositon']").click(function () {
    var datais = $(this).val();

    if (datais == "NoOther") {
        $("#postservicefeedbackOtherDivShow").show();
    } else {
        $("#postservicefeedbackOtherDivShow").hide();
    }
});


function checkintdepfeedback() {
    if (!$("#chkinterfedback").is(":checked")) {
        $("#postsalesinsurancelocDiv").hide();
    }
    else {
        $("#postsalesinsurancelocDiv").show();

    }
}
//clearing all the input fields

function clearallqstmddls() {
    $(postsalesfeedbackyesDiv).find("select").prop("selectedIndex", 0);

}
function clearallqstnrdo() {
    $("input:radio.cntrl").each(function (i) {
        $(this).attr('checked', false);
    });
}
function clearallfolowuplater() {
    $('#txtpostsalescallbackdate').val('');
    $('#txtpostsalecallbacktime').val('');
    $('#remarks_for_followUpLater').val('');
}
function clearallnofeedback() {
    $('#remarks_for_followUpLater').val('');

}
function clearallfeedback() {
    clearallqstnrdo();
    clearallqstnrdo();
}

function clearallyesdivcontrol() {
    clearallfeedback();
    clearallnofeedback();
    clearallfolowuplater();
    const chbx = document.getElementsByName("postsalesdispositions.whatcustsays");

    for (let i = 0; i < chbx.length; i++) {
        chbx[i].checked = false;
    }
}

function clearallnodivControl() {
    const chbx = document.getElementsByName("postsalesdispositions.PSFDispositon");

    for (let i = 0; i < chbx.length; i++) {
        chbx[i].checked = false;
    }
    $("#postservicefeedbackOtherDivShow").hide();
    $('#remarks_for_followUpLater').val('');
    $('#ClmOther').val('');

}

function clearcomplaintresolvecontrol() {
    const chbx = document.getElementsByName("postsalesdispositions.isresolvedorpending");

    for (let i = 0; i < chbx.length; i++) {
        chbx[i].checked = false;
    }
}

function clearthankcallDiv() {
    clearcomplaintresolvecontrol();
    $("#postsalesthankcallDiv").hide();

}

//submit validations
$("#btnsubmitpostsalesfollowuplater").click(function () {
    var callbackdate = $("#txtpostsalescallbackdate").val();
    var callbacktime = $("#txtpostsalecallbacktime").val();
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

$("#btnnoncontactpostsalesbmt").click(function () {

    var chkincSubmit = 0;
    $('[name="postsalesdispositions.PSFDispositon"]').each(function () {
        if ($(this).is(':checked')) chkincSubmit++;
    });
    if (chkincSubmit == 0) {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please Select any one Reason.'
        });
        return false;
    }
    else
    {
        var selected = $("input[name='postsalesdispositions.PSFDispositon']:checked").val();
        if (selected == "NoOther") {
            var textNoOthers = $('#txtnoncontactotherreason').val();
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

//$("#btnfeedbacksubmit").click(function () {

//    var postsalesDetails = Model.postsalesfollowupquestionLists

//    var totalmndtryQustn = $('#txtmndqustn').val();
//    var length = $('.rdomandatory_True:checked').length;
//    if (length < totalmndtryQustn)
//    {
//        Lobibox.notify('warning', {
//            continueDelayOnInactiveTab: true,
//            msg: 'Please select mandatory question(s)*.'

//        });
//        return false;
//    }

//});
$("#btnpostsalescomplaintresolvedsubmit").click(function () {

    var chkincSubmit = 0;
    $('[name="postsalesdispositions.isresolvedorpending"]').each(function () {
        if ($(this).is(':checked')) chkincSubmit++;
    });
    if (chkincSubmit == 0) {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please Select any one Reason.'
        });
        return false;
    }
});
