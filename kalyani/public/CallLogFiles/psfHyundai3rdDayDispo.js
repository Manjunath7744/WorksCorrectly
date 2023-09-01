$(document).ready(function () {
    var varYesPSF = "";
    var varYesPSF1 = '';
    var testPSF = '';
    var varSatisfiedPSF = '';
    var varPSFfeedback = '';
    var varPSFleadS = '';
    $("input[name='psfinteraction.isContacted']").click(function () {
        testPSF = $(this).val();
        if (testPSF == "PSF Yes") {
            $("#PSFYesTalkH").show();
            $("#PSFNotSpeachDiv").hide();
            $("#PSFconnectCall1H").hide();

        }
        if (testPSF == "PSF No") {
            $("#PSFNotSpeachDiv").show();
            $("#PSFYesTalkH").hide();
            $("#PSFconnectCall1H").hide();

        }

    });

    //********nisarga start*********
    $("#SubmitNoPsf").click(function () {
        //if (!$(".reason_for_not_talking").is(":checked")) {
        //    Lobibox.notify('warning', {
        //        msg: 'Please select one of the options given'
        //    });
        //    return false;
        //}

        var chkincSubmit = 0;
        $('[name="psfinteraction.PSFDispositon"]').each(function () {
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
            var selected = $("input[name='psfinteraction.PSFDispositon']:checked").val();
            if (selected == "NoOther") {
                var textNoOthers = $('#ClmOther').val();
                if (textNoOthers == '' || textNoOthers == undefined) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Pleasy Enter Other Reason.'
                    });
                    return false;

                }
            }
            blockUI();
        }


    });
    $("#SubmitToDidUtlakPSFH").click(function () {
        var callbackdate = $("#dp1592642950195").val();
        var callbacktime = $("#tp1592642950235").val();
        if (callbackdate == "")
        {
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

    $("#NextToHowRate").click(function () {

        //else {
        //    alert('Please Select Any Option');
        //}
        //var arr = $("#sel1,#sel2,#sel3,#sel4,#sel5,#sel6,#sel7,#sel8,#sel9,#sel10,#sel11,#sel12,#sel13");
        //arr.change(function () {
        //    if (arr.filter('option[value!=""]:selected').length != arr.length) {
        //        Lobibox.notify('warning', {
        //            msg: 'Please select all the dropdowns'
        //        });
        //    }
        //    else {

        var feedSelected = "";
        var mandQsVal = "";
        $('input[name="psfinteraction.q1_CompleteSatisfication"]').each(function () {

            if ($(this).is(":checked")) {
                feedSelected = $(this).val();
            }
        });

        var selectionCount = 0;
        if (feedSelected == "Visited") {
            $(".mand_qs_visited").each(function () {
                var qsno = $(this).attr('data-qsNo');

                var Element = $('.qs_' + qsno)[0].tagName;
                if (Element == "SELECT") {
                    if ($('.qs_' + qsno).val() != "") {
                        selectionCount = selectionCount + 1;
                        mandQsVal = $('.qs_' + qsno).val();
                    }
                }
                else if (Element == "INPUT") {
                    var Type = $('.qs_' + qsno)[0].type;

                    if (Type == "radio") {
                        var selCount = 0;
                        $('.qs_' + qsno).each(function () {
                            if ($(this).is(":checked")) {
                                selCount = selCount + 1;
                            }
                        });

                        if (selCount > 0) {
                            selectionCount = selectionCount + 1;
                            mandQsVal = $('.qs_' + qsno).val();
                        }
                    }
                }
            });

            $(".mand_qs_both").each(function () {
                var qsno = $(this).attr('data-qsNo');

                var Element = $('.qs_' + qsno)[0].tagName;
                if (Element == "SELECT") {
                    if ($('.qs_' + qsno).val() != "") {
                        selectionCount = selectionCount + 1;
                        mandQsVal = $('.qs_' + qsno).val();
                    }
                }
                else if (Element == "INPUT") {
                    var Type = $('.qs_' + qsno)[0].type;

                    if (Type == "radio") {
                        var selCount = 0;
                        $('.qs_' + qsno).each(function () {
                            if ($(this).is(":checked")) {
                                selCount = selCount + 1;
                            }
                        });

                        if (selCount > 0) {
                            selectionCount = selectionCount + 1;
                            mandQsVal = $('.qs_' + qsno).val();
                        }
                    }
                }
            });

            if (selectionCount != $(".mand_qs_pickup").length || selectionCount != $(".mand_qs_both").length) {
                if (selectionCount != $(".mand_qs_both").length) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please select mandatory question(s)*.'

                    });
                    return false;
                }
                else if ($(".mand_qs_pickup").length > 0) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please select mandatory question(s)*.'

                    });
                    return false;
                }

            }

            if ($('#complaintReceived').val() == "Yes" && ($('#creComplaintCRE').val() == "" || $('#creComplaintCRE').val() == null)) {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please select complaint manager.'

                });
                return false;
            }
        }
        else if (feedSelected == "pickup") {
            $(".mand_qs_pickup").each(function () {
                var qsno = $(this).attr('data-qsNo');

                var Element = $('.qs_' + qsno)[0].tagName;
                if (Element == "SELECT") {
                    if ($('.qs_' + qsno).val() != "") {
                        selectionCount = selectionCount + 1;
                        mandQsVal = $('.qs_' + qsno).val();
                    }
                }
                else if (Element == "INPUT") {
                    var Type = $('.qs_' + qsno)[0].type;

                    if (Type == "radio") {
                        var selCount = 0;
                        $('.qs_' + qsno).each(function () {
                            if ($(this).is(":checked")) {
                                selCount = selCount + 1;
                            }
                        });

                        if (selCount > 0) {
                            selectionCount = selectionCount + 1;
                            mandQsVal = $('.qs_' + qsno).val();
                        }
                    }
                }
            });

            $(".mand_qs_both").each(function () {
                var qsno = $(this).attr('data-qsNo');

                var Element = $('.qs_' + qsno)[0].tagName;
                if (Element == "SELECT") {
                    if ($('.qs_' + qsno).val() != "") {
                        selectionCount = selectionCount + 1;
                        mandQsVal = $('.qs_' + qsno).val();
                    }
                }
                else if (Element == "INPUT") {
                    var Type = $('.qs_' + qsno)[0].type;

                    if (Type == "radio") {
                        var selCount = 0;
                        $('.qs_' + qsno).each(function () {
                            if ($(this).is(":checked")) {
                                selCount = selCount + 1;
                            }
                        });

                        if (selCount > 0) {
                            selectionCount = selectionCount + 1;
                            mandQsVal = $('.qs_' + qsno).val();
                        }
                    }
                }
            });

            if (selectionCount != $(".mand_qs_pickup").length || selectionCount != $(".mand_qs_both").length) {
                if (selectionCount != $(".mand_qs_both").length) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please select mandatory question(s)*.'

                    });
                    return false;
                }
                else if ($(".mand_qs_pickup").length > 0) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please select mandatory question(s)*.'

                    });
                    return false;
                }
            }

            if ($('#complaintReceived').val() == "Yes" && ($('#creComplaintCRE').val() == "" || $('#creComplaintCRE').val() == null)) {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please select complaint manager.'

                });
                return false;
            }
        }


        $("#PsfSelfDriveINYesH").hide();
        $("#complaintDiv").hide();
                if (document.getElementById('PSF2YesId').checked) {
                    $("#upsell3rdDayH").show();
                    //$("#PsfSelfDriveINYesH").hide();
                    $("#PSFYesNamaskarYesDivH").hide();
                }
                else if (document.getElementById('PSF2NoId').checked) {
                    $("#upsell3rdDayH").show();
                    //$("#PsfSelfDriveINYesH").hide();
                    $("#PSFYesNamaskarYesDivH").hide();
                }
        //    }
        //});
    });

    $("#BackToSirMam").click(function () {
        $("#PSFYesTalkH").show();
        //$("#PSFconnectCall1H").show();
        $("#PSFYesNamaskarYesDivH").hide();
        $("input[name='psfinteraction.PSFDispositon']").prop('checked', false);

        $("#PsfSelfDriveINYesH").hide();
        $("#complaintDiv").hide();
        $("#comment2").val("");//clear comment
        $('.cntrl').val('');//clear all ddl's

        $('.characterLimit').val('');//clear all ddl's
        $('.cntrl').prop('checked', false);
        $('#complaintReceived').val('');
        $('#creComplaintCRE').val('');
        $('#complaintCRESelect').hide();
        $("input[name='psfinteraction.q1_CompleteSatisfication']").prop('checked', false);
        $('#psfMainQsDiv').hide();
    });

    $("#BackTo3rdDayRate").click(function () {

        var feedSelected = "";
        //$('input[name="psfinteraction.q1_CompleteSatisfication"]').each(function () {

        //    if ($(this).is(":checked")) {
        //        feedSelected = $(this).val();
        //    }
        //});

        //if (feedSelected == "PSFSelf Yes") {
        //    $("#PSFYesNamaskarYesDivH").show();
        //    $("#PsfSelfDriveINYesH").show();
        //}
        //else if (feedSelected == "PSFSelf No") {
        //    $("#PSFYesNamaskarYesDivH").show();
        //    $("#PsfSelfDriveInNo1H").show();
        //}

        $('#PSFYesNamaskarYesDivH').show();
        $("#upsell3rdDayH").hide();
       
        $("#complaintDiv").show();
        //clearCaptureLead();
    });

    function clearCaptureLead() {
        $('#LeadNoID3Hyndai').prop('checked', true);
        $('.myOutCheckbox').each(function () {
            if ($(this).is(':checked')) {
                $(this).trigger('click');
            }
        });
        $('.capture-lead').val('');
    }


    //********nisarga end*********

    $("input[name='psfinteraction.PSFDispositon']").click(function () {
        varYesPSF = $(this).val();
        if (varYesPSF == "PSF_Yes" || varYesPSF == "Call Me Later") {
            if (varYesPSF == "PSF_Yes") {
                hideforMaverick();
                $("#PSFYesNamaskarYesDivH").show();
                $("#PSFYesNamaskarNoDivH").hide();
                $("#PSFconnectCall1H").hide();
                $("#PSFYesTalkH").hide();
                //$("#PSF2YesId").trigger('click');
                $("#PsfSelfDriveINYesH").show();
                $("#PsfSelfDriveInNo1H").hide();
                //$("#PSF2YesId").prop("checked", true);
                $("#complaintDiv").show();
                $('#PSFconnectCall1H').hide();

                //For 10th Day only Question show for hanshyundai

                var psfDay = $('#psfType').val();
                var dealer = $('#PkDealercode').val();

                if ((dealer == "HANSHYUNDAI" && psfDay == "5") || (dealer == "BRIDGEWAYMOTORS")) {
                    $('#psfOptionDiv').hide();
                    $("#PSF2YesId").trigger('click');
                }
            }
            if (varYesPSF == "Call Me Later") {
                $("#PSFYesNamaskarNoDivH").show();
                $("#PSFYesTalkH").hide();
                $("#PSFconnectCall1H").hide();

                $("#PsfSelfDriveInNo1H").show();
                $("#PsfSelfDriveINYesH").hide();
                $("#complaintDiv").hide();
                


            }
        }
    });

    
    $("input[name='psfinteraction.q1_CompleteSatisfication']").click(function () {
        var varYesPSF1 = $(this).val();
        if (varYesPSF1 == "Visited") {//went yourself
            $("#PsfSelfDriveINYesH").show();
            $("#PsfSelfDriveInNo1H").hide();
            $("#complaintDiv").show();
            $("#comment2div").css("display:block")
            $("#complaintReceived,#comment1").val("");//clear comment
            $(".visited").show();
            $(".pickup").hide();

            $(".both").show();
            $(".cntrl_pickup").val('');
            $(".cntrl_pickup").prop('checked', false);
        }
        if (varYesPSF1 == "pickup") {//pickup drop
            $("#PsfSelfDriveInNo1H").show();
            $("#PsfSelfDriveINYesH").hide();
            $("#complaintDiv").show();
            $("#comment2").val("");//clear comment
            
            $(".visited").hide();
            $(".pickup").show();
            $("#comment1Div").css("display:block");

            $(".cntrl_visited").val('');
            $(".cntrl_visited").prop('checked', false);
            $(".both").show();
        }

        $('#complaintCRESelect').hide();
        $('#psfMainQsDiv').show();

    });
    $("#BackToTaltkDivH").click(function () {
        $("#PSFYesTalkH").show();
        $("#PSFconnectCall1H").show();
        $("#PSFYesNamaskarYesDivH").hide();
    });

    $("#BackToDidUtlakPSFH").click(function () {
        $("#PSFYesTalkH").show();
        //$("#PSFconnectCall1H").show();
        $("#PSFYesNamaskarNoDivH").hide();
        $("input[name='psfinteraction.PSFDispositon']").prop('checked', false);
        $("#dp1592642950195").val(" ")
        $("#tp1592642950235").val("");
        $(".characterLimit").val(" ")
    });

    


   




    $("#NextToUpsellInsu").click(function () {


        if (document.getElementById('LeadYesID3Hyndai').checked) {

            var checkeds = $('.myOutCheckbox').is(':checked');

            if (checkeds) {

            } else {

                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please check one of these.'

                });
                return false;
            }

            $("#upsell3rdDayH").hide();
            $("#PSFFeedbackQ").show();


        }
        else if (document.getElementById('LeadNoID3Hyndai').checked) {
            $("#upsell3rdDayH").hide();
            $("#PSFFeedbackQ").show();

        }
        else {
            alert('Please Select Any Option');
        }

    });



   

    $("#BackToSirMamDiv").click(function () {
        //$("#PSFYesNamaskarYesDivH").show();
        $("#upsell3rdDayH").show();
        $("#PSFFeedbackQ").hide();
        $("input[name='psfinteraction.q1_CompleteSatisfication']").prop('checked', false);
    });

    $("#NextToPSFLastDiv").click(function () {
        $("#PsfSelfDriveINYesH").show();
        //$("#PSFYesNamaskarYesDivH").hide();

    });
    $("#BackTo1stQ").click(function () {

        $("#PSFconnectCall1H").show();
        $("#PSFYesTalkH").hide();
        $("input[name='psfinteraction.isContacted']").prop('checked', false);

    });
    $("#BackTolatkNoPsf").click(function () {
        $("#PSFconnectCall1H").show();
        $("#PSFNotSpeachDiv").hide();
        $("input[name='psfinteraction.isContacted']").prop('checked', false);
        $(".reason_for_not_talking").prop('checked', false);
        $(".OtherComments,.nonContactRemarks").val('');
    });


    $("#NextTO2ndQ").click(function () {
        if (document.getElementById('GoodMorningYes').checked) {
            $("#PSFYesNamaskarYesDivH").show();
            $("#PSFYesTalkH").hide();
        }
        else if (document.getElementById('GoodMorningNo').checked) {
            $("#PSFYesNamaskarYesDivH").show();
            $("#PSFYesTalkH").hide();
        }
        else {
            Lobibox.notify('warning', {
                msg: 'Please select one of the options given'
            })
        }


    });



    $("input[name='psfinteraction.q12_FeedbackTaken']").click(function () {
        varPSFfeedback = $(this).val();
        if (varPSFfeedback == "Yes") {
            $("#feedbackPSFYes").show();
            $("#feedbackPSFNo").hide();

        }
        if (varPSFfeedback == "No") {
            $("#feedbackPSFNo").show();
            $("#feedbackPSFYes").hide();

        }

    });//--

    $("input[name='listingForm.LeadYesH']").click(function () {
        varPSFleadS = $(this).val();
        if (varPSFleadS == "Yes") {
            $("#LeadHyndai3rdDay").show();


        }
        if (varPSFleadS == "No") {
            clearCaptureLead();
            $("#LeadHyndai3rdDay").hide();

        }

    });

//< !------ Upsell Validation------->
        //OutBound Upsell Opportunity--------->
        $('#InsuranceIDCheck').click(function () {
            if ($(this).prop('checked')) {
                $('#InsuranceSelect').show();
            } else {
                $('#InsuranceSelect').hide();
            }
        });

    $('#MaxicareIDCheck').click(function () {
        if ($(this).prop('checked')) {
            $('#MaxicareSelect').show();
        } else {
            $('#MaxicareSelect').hide();
        }
    });

    $('#ShieldID').click(function () {
        if ($(this).prop('checked')) {
            $('#ShieldSelect').show();
        } else {
            $('#ShieldSelect').hide();
        }
    });

    $('#VASID').click(function () {
        if ($(this).prop('checked')) {
            $('#VASTagToSelect').show();
        } else {
            $('#VASTagToSelect').hide();
        }
    });

    $('#RoadSideAsstID').click(function () {
        if ($(this).prop('checked')) {
            $('#RoadSideAssiSelect').show();
        } else {
            $('#RoadSideAssiSelect').hide();
        }
    });

    $('#EXCHANGEID').click(function () {
        if ($(this).prop('checked')) {
            $('#EXCHANGEIDSelect').show();
        } else {
            $('#EXCHANGEIDSelect').hide();
        }
    });

    $('#UsedCarID').click(function () {
        if ($(this).prop('checked')) {
            $('#UsedCarSelect').show();
        } else {
            $('#UsedCarSelect').hide();
        }
    });


    $('.single-input').timepicker({
        showPeriodLabels: false,
    });

});


//validation//

$('#followUpValidation').click(function () {
    var dateVal = $('#psfFollowUpDate').val();
    var timeVal = $('#psfFollowUpTime').val();
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



$('#SubmitDivMamDivsubmit').click(function () {
    var chkincSubmit = 0;
    $('[name="q12_FeedbackTaken"]').each(function () {
        if ($(this).is(':checked')) chkincSubmit++;
    });
    if (chkincSubmit == 0) {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please check one.'
        });
        return false;

    } else {
        $.blockUI();

    }
});

$('#noncontactsValid').click(function () {
    var chkincSubmit = 0;
    $('[name="PSFDispositon"]').each(function () {
        if ($(this).is(':checked')) chkincSubmit++;
    });
    if (chkincSubmit == 0) {
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please check one.'
        });
        return false;

    } else {
        $.blockUI();

    }
});


function psfRatings() {
    var PsfQ1Rating = $('#PsfRatingQ_1').val();
    var PsfQ2Rating = $('#PsfRatingQ_2').val();
    var PsfQ3Rating = $('#PsfRatingQ_3').val();
    var PsfQ4Rating = $('#PsfRatingQ_4').val();
    var PsfQ5Rating = $('#PsfRatingQ_5').val();
    var PsfQ6Rating = $('#PsfRatingQ_6').val();
    var PsfQ7Rating = $('#PsfRatingQ_7').val();
    if (PsfQ1Rating == "" || PsfQ2Rating == "" || PsfQ3Rating == "" || PsfQ4Rating == "" || PsfQ5Rating == "" || PsfQ6Rating == "" || PsfQ7Rating == "" ) {
    }
    else if (PsfQ1Rating < 4 || PsfQ2Rating < 4 || PsfQ3Rating < 4 || PsfQ4Rating < 4 || PsfQ5Rating < 4 || PsfQ6Rating < 4 || PsfQ7Rating < 9) {
        document.getElementById("complaintReceived").value = "Yes";
        loadCampaignUser();
    } else {
        document.getElementById("complaintReceived").value = "No";
        loadCampaignUser();
    }
}


function loadCampaignUser() {

    var selectedRating = document.getElementById('complaintReceived').value;

        if (selectedRating == "Yes") {
            $("#complaintCRESelect").show();

            //var psfassignid = document.getElementById('assignIntId').value;
            //var loca_id = document.getElementById('comp_workshop_id').value;
            //var urlPath = "/complaintCREList/" + psfassignid + "/" + loca_id;

            //$.ajax({
            //    type: 'POST',
            //    url: urlPath,
            //    datatype: 'json',
            //    data: { psfassignid: psfassignid, loca_id: loca_id },
            //    cache: false,
            //    success: function (crelist) {
            //        $("#complaintCRESelect").show();

            //        $('#creComplaintCRE').empty();
            //        var dropdown = document.getElementById("creComplaintCRE");

            //        for (var i = 0; i < crelist.length; i++) {
            //            dropdown[dropdown.length] = new Option(crelist[i][1], crelist[i][0]);

            //        }

            //    }, error(error) {

            //    }
            //});
        }
        else {
            $("#complaintCRESelect").hide();
        }
    }



$("input[name$='PSFDispositon']").click(function () {
    var PSFOthers = $(this).val();
    if (PSFOthers == "NoOther") {
        $("#PSFOtherDivShow").show();

    }
    else {

        $("#PSFOtherDivShow").hide();

    }

});


function hideforMaverick() {
    var delerName = $('#PkDealercode').val();
    if (delerName == "MAVERICKMOTORS") {
        $("#PSF2YesId").prop("checked", true);
        $("#PSF2NoId").prop("checked", false);
        $("#psfOptionDiv").hide();
        $("#PSF2YesId").attr('checked', true).trigger('click');
    }
}