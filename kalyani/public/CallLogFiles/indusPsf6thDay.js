var cntrContacted = $("input[name='indusPsfInteraction.isContacted']");
var cntrCustSay = $("input[name='indusPsfInteraction.whatCustSaid']")
var divIsContacted = $('#isContacted');
var custAns = "";
var techPrevSect = 0;
var nontechPrevSect = 0;
var complaintManager = "";
var SAStricker="",SaFeedback="",qos="",rateSA="",SAQuality="";


var dictTech = {};
var dictNonTect = {};

$(document).ready(function () {
    $("input[name='indusPsfInteraction.isContacted']").click(function () {
        var isContacted = $(this).val();
        if (isContacted == "Yes") {
            $('#YesContactedDiv').show();
            $('#NotSpeachDiv').hide();
            $("input[name='indusPsfInteraction.PSFDisposition']").prop('checked', false);
            $('input[name="indusPsfInteraction.whatCustSaid"]').prop('checked', false);
            $('#NoOthersText1').val('');
            $('#NoOthers').hide();
        }
        else {
            $('#YesContactedDiv').hide();
            $('#NotSpeachDiv').show();
            $('#followUpDiv').hide();
            $('input[name="indusPsfInteraction.whatCustSaid"]').prop('checked', false);
            $('#noFeedDiv').hide();
        }
        $('.characterLimit').val('');
    })


    // Chethan Added
    $("#nonContactValidation6PSF").off().on("click", function () {
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
            if (selected == "Other") {
                var textNoOthers = $('.NoOthersText1').val();
                if (textNoOthers == '') {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Remarks Should not Empty.'
                    });
                    return false;

                }
            }
            blockUI();
        }
    });

    //No Other
    $('input[name="indusPsfInteraction.PSFDisposition"]').click(function () {
        var selected = $(this).val();

        if (selected == "Other") {
            $('#NoOthers').show();
        }
        else {
            $('#NoOthers').hide();
            $('#NoOthersText1').val('');
        }
    });

    $("input[name='indusPsfInteraction.whatCustSaid']").click(function () {
        var custRespo = $(this).val();
        $('#YesContactedDiv').hide();
        if (custRespo == "Feedback Given") {
            //$(cntrContacted).hide();
            $('#feedGivenDiv').show();
            $('#followUpDiv').hide();
            $('#noFeedDiv').hide();
            $('#isContacted').hide();
            $('#custSay').show();
            $('.noFeedText').val('');
            $('#feedGivenOptions').show();

            $('input[name="indusPsfInteraction.vehicleAfterService"]').prop("checked", false);
            $('#complaintDDL').val('');
            $('#mainButtonsDiv').show();
            $('#btnFeedBack').attr('onclick', 'backtoFeed("submain")');
        }
        else if (custRespo == "FollowUp Later") {
            //$(cntrContacted).hide();
            $('#followUpDiv').show();
            $('#feedGivenDiv').hide();
            $('#noFeedDiv').hide();
            $('#isContacted').hide();

        }
        else if (custRespo == "No Feedback") {
            //$(cntrContacted).show();
            $('#followUpDiv').hide();
            $('#feedGivenDiv').hide();
            $('#noFeedDiv').show();
            $('#isContacted').hide();

        }
        else if (custRespo == "ConfirmStatus") {

            $('#isContacted').hide();
            $('#divResolveAction').show();
            $('.sa').prop('checked', false);
        }
    });

    $("input[name='indusPsfInteraction.vehicleAfterService']").click(function () {
        var feed = $(this).val();
        if (feed == "Good") {
            $('#afterFeedDiv').show();
            $('#badDiv').hide();
            $('#complaintManagerDiv').hide();
            //tech and non tech play
            custAns = "Good";
            $('input[name="indusPsfInteraction.isTechnical"]').prop("checked", false);
            $('#btnFeedBack').attr('onclick', 'backtoFeed("submain")');
            $('#complaintDDL').val('');
            manageQSCheckbox('close', null);
            managerPlusMinusIcon('close', null);
        }
        else if (feed == "Bad") {
            $('#afterFeedDiv').show();
            $('#feedGivenOptions').hide();
            $('#badDiv').show();
            $('#complaintManagerDiv').show();
            custAns = "Bad";
            $('#btnFeedBack').attr('onclick', 'backtoFeed("submain")');
        }
    });

    $("input:checkbox.badOption").click(function () {
        var selectedOp = $(this).attr("id");
        //techQsDiv
        var isChecked = $(this).prop("checked");
        if (isChecked == true) {
            $(this).val(true);
            if (selectedOp === "hdTech") {
                $('#techQsDiv').show();
                manageQSCheckbox('open', 'tech');
                managerPlusMinusIcon('close', 'tech');
            }
            else if (selectedOp === "hdNonTech") {
                $('#nontechQsDiv').show();
                manageQSCheckbox('open', 'nontech');
                managerPlusMinusIcon('close', 'nontech');
            }
        }
        else {
            if (selectedOp === "hdTech") {
                
                 //$('#techQsDiv').hide();
                manageQSCheckbox('close', 'tech');
                managerPlusMinusIcon('close', 'tech');
                dictTech = {};
            }
             else if (selectedOp === "hdNonTech") {
                 //$('#nontechQsDiv').hide();
                manageQSCheckbox('close', 'nontech');
                managerPlusMinusIcon('close', 'nontech');
                dictNonTect = {};
            }
        }
    });


});

function backtoFeed(toDivName) {


    $('.nonTechForIcon').removeClass("nonTechDivForIcon");

    if (toDivName == "main") {
        //$('#feedGivenOptions').hide();

        //$('#YesContactedDiv').show();
        //$('#feedGivenDiv').show();
        //$('#isContacted').show();
        //$('#mainButtonsDiv').hide();
    }
    else {

        $('#feedGivenOptions').hide();

        $('#YesContactedDiv').show();
        $('#feedGivenDiv').hide();
        $('#isContacted').show();
        $('#mainButtonsDiv').hide();

        $('#btnFeedBack').attr('onclick', 'backtoFeed("submain")');
        $('#feedGivenOptions').show();
        $("input:checkbox.badOption").prop('checked', false);
        manageQSCheckbox('close', null);
        managerPlusMinusIcon('close', null);
        $('#complaintManagerDiv').hide();
        $('#complaintDDL').val('');

        $('.subchildsQs').slideUp();
        $('.subnonchildsQs').slideUp();

        $('.techSubQsCk').prop('disabled', true);
        $('.nontechCK').prop('disabled', true);
        managerPlusMinusIcon('close', null);
        $('#badDiv').hide();
    }
}

$('#followUpSubmit').click(function () {
    var date = $('input[name="indusPsfInteraction.FollowUpdate"]').val();
    var time = $('input[name="indusPsfInteraction.FollowUptime"]').val();

    if (date == "" && time == "") {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please Select Date & Time.'
        });
        return false;
    }
})

function backFromCallMeLater() {
    $('.followLater').val('');
    $('#followUpDiv').hide();
    $('#YesContactedDiv').show();
    $('#isContacted').show();
    $('#noFeedDiv').hide();
}

//function backToMain() {
//    //$('#feedbackDiv').hide();
//    $('input[name="indusPsfInteraction.vehicleAfterService"]').prop("checked", false);
    
//    $(cntrCustSay).prop("checked", false);
//    $(cntrContacted).prop("checked", false);
//    $('#YesContactedDiv').hide();
//    $('#feedGivenDiv').hide();
//    $('#isContacted').show();
//}

function manageQSCheckbox(cmd, part) {
    if (cmd == "close" && part == null) {
        //Tech
        $('input:checkbox.techMainQS').prop("checked", false);
        $('input:checkbox.techSubQsCk').prop("checked", false);
        $('input:checkbox.techMainQS').prop("disabled", true);
        $('input:checkbox.techSubQsCk').prop("disabled", true);
        $('.subchildsQs').slideUp();

        //NonTech
        $('input:checkbox.nontechMainQS').prop("checked", false);
        $('input:checkbox.nontechCK').prop("checked", false);
        $('input:checkbox.nontechMainQS').prop("disabled", true);
        $('input:checkbox.nontechCK').prop("disabled", true);
        $('input[name="indusPsfInteraction.nonTechnincal"]').prop('checked', false);
        $('.subnonchildsQs').slideUp();
       
        dictTech = {};
        dictNonTect = {};
    }
    else if (cmd == "open" && part == null) {
        //Tech
        $('input:checkbox.techMainQS').prop("checked", true);
        $('input:checkbox.techSubQsCk').prop("checked", true);
        $('input:checkbox.techMainQS').prop("disabled", false);
        $('input:checkbox.techSubQsCk').prop("disabled", false);
        $('.subchildsQs').slideDown();

        //NonTech
        $('input:checkbox.nontechMainQS').prop("checked", true);
        $('input:checkbox.nontechCK').prop("checked", true);
        $('input:checkbox.nontechMainQS').prop("disabled", false);
        $('input:checkbox.nontechCK').prop("disabled", false);
        $('.subnonchildsQs').slideDown();
    }
    else if (cmd == "open" && part == "tech") {
        //$('input:checkbox.techMainQS').prop("checked", true);
        //$('input:checkbox.techSubQsCk').prop("checked", true);
        $('input:checkbox.techMainQS').prop("disabled", false);
        $('input:checkbox.techSubQsCk').prop("disabled", false);
        //$('.subchildsQs').slideDown();
    }
    else if (cmd == "open" && part == "nontech") {
        //$('input:checkbox.nontechMainQS').prop("checked", true);
        //$('input:checkbox.nontechCK').prop("checked", true);
        $('input:checkbox.nontechMainQS').prop("disabled", false);
        $('input:checkbox.nontechCK').prop("disabled", false);
        //$('.subnonchildsQs').slideDown();
    }
    else if (cmd == "close" && part == "tech") {
        $('input:checkbox.techMainQS').prop("checked", false);
        $('input:checkbox.techSubQsCk').prop("checked", false);
        $('input:checkbox.techMainQS').prop("disabled", true);
        $('input:checkbox.techSubQsCk').prop("disabled", true);
        $('.subchildsQs').slideUp();
        dictTech = {};
    }
    else if (cmd == "close" && part == "nontech") {
        $('input:checkbox.nontechMainQS').prop("checked", false);
        $('input:checkbox.nontechCK').prop("checked", false);
        $('input:checkbox.nontechMainQS').prop("disabled", true);
        $('input:checkbox.nontechCK').prop("disabled", true);
        $('.subnonchildsQs').slideUp();
        dictNonTect = {};
    }
}
function managerPlusMinusIcon(cmd,part) {
    //cmd:command for all, part: for parts(tech,nonTech)
    if (cmd == "close" && part == null) {
        $('.nontechPlus').hide();
        $('.nontechMinus').hide();
        $('.techPlus').hide();
        $('.techMinus').hide();
    }
    else if (cmd == "open" && part == null) {
        $('.nontechPlus').show();
        $('.nontechMinus').show();
        $('.techPlus').show();
        $('.techMinus').show();
    }
    else if (cmd == "open" && part == "tech") {
        $('.techPlus').show();
        $('.techMinus').show();
    }
    else if (cmd == "close" && part == "tech") {
        $('.techPlus').hide();
        $('.techMinus').hide();
    }
    else if (cmd == "open" && part == "nontech") {
        $('.nontechPlus').show();
        $('.nontechMinus').show();
    }
    else if (cmd == "close" && part == "nontech") {
        $('.nontechPlus').hide();
        $('.nontechMinus').hide();
    }
}

function expandQsIcon(ele, Qs,cmd) {

    if (Qs == "Tech") {
        var divid = $(ele).attr('data-id');
        if (cmd == "open") {
            $('#subTechDiv_' + divid).slideDown();
            $(ele).hide();
            $('#techClose_' + divid).show();
        }
        else if (cmd == "close") {
            $('#subTechDiv_' + divid).slideUp();
            $(ele).hide();
            $('#techOpen_' + divid).show();
        }
    }
    else if (Qs == "nonTech") {
        var divid = $(ele).attr('data-id');
        if (cmd == "open") {
            $('#subnonTechDiv_' + divid).slideDown();
            $(ele).hide();
            $('#nontechClose_' + divid).show();
        }
        else if (cmd == "close") {
            $('#subnonTechDiv_' + divid).slideUp();
            $(ele).hide();
            $('#nontechOpen_' + divid).show();
        }
    }
}

function expandChildQs(ele) {
    $('.subchildsQs').slideUp();
    var userAction = $(ele).prop('checked');
    var curId = $(ele).attr('id').split('_')[1];
    if (userAction == true) {
        $('#subTechDiv_' + curId).slideDown();
        $('#techOpen_' + curId).hide();
        $('#techClose_' + curId).show();

        if ($('#ckTechMain_' + techPrevSect).is(":checked")) {
            $('#techOpen_' + techPrevSect).show();
            $('#techClose_' + techPrevSect).hide();
        }
        else {
            $('#techOpen_' + techPrevSect).hide();
            $('#techClose_' + techPrevSect).hide();
        }
        

        techPrevSect = curId;
    }
    else {
        $('#techOpen_' + curId).hide();
        $('#techClose_' + curId).hide();
        $('input:checkbox.techSubQs_' + curId).prop('checked', false);

        delete dictTech[curId];
    }
}

function expandNonChildQs(ele) {
    $('.subnonchildsQs').slideUp();
    var userAction = $(ele).prop('checked');
    var curId = $(ele).attr('id').split('_')[1];
    if (userAction == true) {
        $('#subnonTechDiv_' + curId).slideDown();
        $('#nontechOpen_' + curId).hide();
        $('#nontechClose_' + curId).show();
        if ($('#cknonTechMain_' + nontechPrevSect).is(":checked")) {
            $('#nontechOpen_' + nontechPrevSect).show();
            $('#nontechClose_' + nontechPrevSect).hide();
        }
        else {
            $('#nontechOpen_' + nontechPrevSect).hide();
            $('#nontechClose_' + nontechPrevSect).hide();
        }
        $("#nontechMainDiv_" + curId).addClass("nonTechDivForIcon");
        nontechPrevSect = curId
    }
    else {
        $("#nontechMainDiv_" + curId).removeClass("nonTechDivForIcon");
        $('input:checkbox.nontechSubQs_' + curId).prop('checked', false);
        $('#nontechOpen_' + curId).hide();
        $('#nontechClose_' + curId).hide();
        delete dictNonTect[curId];
    }
}

//To Record checkbox selected...sub with main QS
function recordTechQS(ele) {
    var mainQSId = $(ele).attr('data-parentQsId');

    if ($(ele).is(":checked")) {
        if (dictTech[mainQSId] == undefined) {
            dictTech[mainQSId] = $(ele).val();
        }
        else {
            var pressentValues = dictTech[mainQSId];
            pressentValues = pressentValues+"," + $(ele).val();
            dictTech[mainQSId] = pressentValues;
        }
    }
    else {
        var unCkValue = $(ele).val();
        var pressentValues = dictTech[mainQSId];
        var valueArray = [];
        if (pressentValues.includes(',')) {
            valueArray = pressentValues.split(',');
            var valIndex = valueArray.indexOf(unCkValue);
            if (valIndex > -1) {
                //valueArray = valueArray.splice(valIndex, 0);
                //valueArray = valueArray.remove(valIndex);
                delete valueArray[valIndex];
                pressentValues = valueArray.join(',');
                dictTech[mainQSId] = pressentValues;
                for (var ele in valueArray) {
                    if (valueArray[ele] != "") {
                        return;
                    }
                }
                delete dictTech[mainQSId]
                $('#ckTechMain_' + mainQSId).click();
            }
        }
        else {
            delete dictTech[mainQSId];
            $('#ckTechMain_' + mainQSId).click();
        }
    }

}

function recordNonTechQS(ele) {
    var mainQSId = $(ele).attr('data-parentQsId');

    if ($(ele).is(":checked")) {
        if (dictNonTect[mainQSId] == undefined) {
            dictNonTect[mainQSId] = $(ele).val();
        }
        else {
            var pressentValues = dictNonTect[mainQSId];
            pressentValues = pressentValues+ "," + $(ele).val();
            dictNonTect[mainQSId] = pressentValues;
        }

        
    }
    else {
        var unCkValue = $(ele).val();
        var pressentValues = dictNonTect[mainQSId];
        var valueArray = [];
        if (pressentValues.includes(',')) {
            valueArray = pressentValues.split(',');
            var valIndex = valueArray.indexOf(unCkValue);
            if (valIndex > -1) {
                //valueArray = valueArray.splice(valIndex, 1);
                delete valueArray[valIndex];
                pressentValues = valueArray.join(',');
                dictNonTect[mainQSId] = pressentValues;

                for (var ele in valueArray) {
                    if (valueArray[ele] != "") {
                        return;
                    }
                }
                delete dictNonTect[mainQSId];
                $('#cknonTechMain_' + mainQSId).click();
                
            }
        }
        else {
            delete dictNonTect[mainQSId];
            $('#cknonTechMain_' + mainQSId).click();
        }
    }
}


function nextToFeedback() {

    var presentSelection = "";

    $('input[name="indusPsfInteraction.vehicleAfterService"]').each(function () {
        if ($(this).is(":checked")) {
            presentSelection = $(this).val();
        }
    });

    if (presentSelection === "Good") {
        $('#YesContactedDiv').hide();
        $('#feedGivenDiv').hide();
        $('#afterVehSay').show();
    }
    else if (presentSelection === "Bad") {
        complaintManager = $('#complaintDDL option:selected').val();
        if (complaintManager == '' || complaintManager == null || complaintManager == undefined) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please select complaint manager'
            });
            return false
        }

        if (Object.keys(dictTech).length === 0 && Object.keys(dictNonTect).length === 0) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please select any options from technical or non technical'
            });
            return false
        }
        $('#txtTechQs').val(JSON.stringify(dictTech));
        $('#txtNonTechQs').val(JSON.stringify(dictNonTect));
        $('#YesContactedDiv').hide();
        $('#feedGivenDiv').hide();
        $('#complaintManager').hide();
        $('#afterVehSay').show();
    }
    else {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please select anyone options(Good/Bad).'
        });
        return false
    }

    
}

$('input[name="indusPsfInteraction.SAescalationSticker"]').click(function () {
    SAStricker = $(this).val();
});

$('input[name="indusPsfInteraction.SAInstantFeedBack"]').click(function () {
    SaFeedback = $(this).val();
});
$('input[name="indusPsfInteraction.qos"]').click(function () {
    qos = $(this).val();
});
$('input[name="indusPsfInteraction.overallServiceExperience"]').click(function () {
    rateSA = $(this).val();

    if (rateSA === "Average" || rateSA === "Poor" || custAns=="Bad") {
        $('#complaintManagerDiv').show();
    }
    else {
        $('#complaintManagerDiv').val('');
        $('#complaintManagerDiv').hide();
    }
});

$('#SAQuality').change(function () {
    SAQuality = $('#SAQuality option:selected').val();
})

$('#feedFinalSubmit').click(function (e) {
    if (rateSA == "" || SAQuality == "") {

        if (SAQuality == "") {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Rate from 1-10.'
            });
            e.preventDefault();
            return;
        }

        if (rateSA == "") {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please select anyone option from Service Experience.'
            });
            e.preventDefault();
            return;
        }

        return false;
    }
   
    else {
        if (rateSA === "Average" || rateSA == "Poor") {
            var complaintManager = $('#complaintDDL').val();
            if (complaintManager === "" || complaintManager === null) {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please select complaint manager'
                });
                return false;
            }
        }
    }
});

$('#goToFeed').click(function () {
    
    $('input[name="indusPsfInteraction.overallServiceExperience"]').prop("checked", false);
    $('#SAQuality').val('');
    $('.finalRemarks').val('');
    
    if (custAns === "Bad" || rateSA === "Average" || rateSA === "Poor") {
        $('#complaintManagerDiv').show();
    }
    else {
        $('#complaintManagerDiv').hide();
        $('#complaintManagerDiv').val('');
    }

    //$('#YesContactedDiv').show();
    $('#feedGivenDiv').show();
    $('#afterVehSay').hide();
});

$("#didUTalk").click(function () {
    $("#SMRInteractionFirst").show();
    $("#isContacted").show();
    $("#NotSpeachDiv").hide();
    $('#psf6thYes').attr('checked', false);
    $('#psf6thNo').attr('checked', false);
    $('input[name="indusPsfInteraction.isContacted"]').prop('checked', false);
    $('.characterLimit').val('');
});

