
$(document).ready(function () {
    $(".pressesYesOrNo").click(function () {
        //var isContacted=$()
        if ($(this).val() == "Yes") {
            $("#notContacted").hide();
            $("#YesContactedDiv").show();
            $("input[name='indusPsfInteraction.PSFDisposition']").prop("checked", false);
        }
        else if ($(this).val() == "No") {
            $("#notContacted").show();
            $("#YesContactedDiv").hide();
            $("#compResolveDiv").hide();
            $("#followUpDiv").hide();
            $("input[name='indusPsfInteraction.whatCustSaid']").prop("checked", false);
            clearFollowUpLater();
            clearComplaintResStatus();
        }
    })
});

//txtarea is displayed when others is clicked
$("#clickedOthers").click(function () {
    $("#NoOthers").css('display','block');
});

//txt area is cleared and is hidden when other radio btns are clicked
$(".notOthersClicked").click(function () {
    $("#NoOthersText1").val("");
    $("#NoOthers").css('display', 'none');
});



$("input[name='indusPsfInteraction.whatCustSaid']").click(function () {

    var selected = $(this).val();

    if (selected == "Complaint Resolution Status") {
        $("#compResolveDiv").show();
        $("#followUpDiv").hide();
        clearFollowUpLater();
    }
    else {
        clearComplaintResStatus();
        $("#compResolveDiv").hide();
        $("#followUpDiv").show();
    }

});

var isClicked = false;
$("input[name='rmInteraction.RMResolutionStatus']").click(function () {
    isClicked = true;
});

function getInterDeptLead() {
    if (isClicked == true) {
        $("#isContacted").hide();
        $("#YesContactedDiv").css("display","none");
        $("#interDptFeedDiv").show();
        $("#compResolveDiv").hide();
    }
    else {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please select anyone of each'
        });
    }
}

function clearFollowUpLater() {
    $(".followLater").val("");
}
function clearComplaintResStatus() {
    $("input[name='rmInteraction.RMResolutionStatus']").prop("checked", false);
    $("#VOC").val("");
}

function backToMain() {
    $("#isContacted").show();
    $("#YesContactedDiv").css("display", "block");
    $("#compResolveDiv").show();
    $("#interDptFeedDiv").hide();
    clearIDLYes();
    $("#IDLCreRemarks").val("");
    $("#IDLCusRemarks").val("");
}

$("#LeadNoID").click(function () {
    clearIDLYes();
});

function clearIDLYes() {
    $(".myOutCheckbox").prop("checked", false);
    $(".IDLYesRemarksCls").val("");
    $('.upsellLeadSelectDivSB').each(function () {
        $(this).prop('checked', false);
        $('#upsellLeadSelectSB_' + $(this).attr('data-upsellId')).hide();
    });
    $('.upsellLeadSelectDivSB').prop('checked', false);
}