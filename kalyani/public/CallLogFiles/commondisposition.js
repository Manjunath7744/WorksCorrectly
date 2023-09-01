var pkDealer = $('#PkDealercode').val()

$(document).ready(function () {
    $("#OutBoundDiv").show();
    $('#FollowUpTime').keypress(function (e) {
        var regex = ["[0-2]",
            "[0-4]",
            ":",
            "[0-6]",
            "[0-9]",
            "(A|P)",
            "M"],
            string = $(this).val() + String.fromCharCode(e.which),
            b = true;
        for (var i = 0; i < string.length; i++) {
            if (!new RegExp("^" + regex[i] + "$").test(string[i])) {
                b = false;
                alert('Incorrect Format!');
            }
        }
        return b;
    });
    $('#editRegistrationno').click(function () {
        $('.vehicalRegNo').removeAttr('readonly', true);
        $('#vehicalRegNo').focus();
        $("#editRegistrationno").css("display", "none");
        $("#updateRegistrationno").css("display", "block");
    });

    $('#editEngineno').click(function () {
        $('#engineNo').removeAttr('readonly', true);
        $("#editEngineno").css("display", "none");
        $("#updateEngineno").css("display", "block");
        $('#engineNo').focus();
    });

    $('#editChassisno').click(function () {
        $('#chassisNo').removeAttr('readonly', true);
        $("#editChassisno").css("display", "none");
        $("#updateChassisno").css("display", "block");
        $('#chassisNo').focus();
    });





    var preFlag = $('#pFlag').val();
    if (preFlag == 1) {
        $("#checkbox3").prop("checked", true);
    }
    else if (preFlag == 2) {
        $("#checkbox2").prop("checked", true);
    }
    else if (preFlag == 3) {
        $("#checkbox1").prop("checked", true);
    }
    $(".peditAddressmodal").click(function () {
        var mystring = $('#permanentAddress').val();
        var elements = mystring.split(',');
        $('.paddr_line1').val(elements[0]);
        $('.paddr_line2').val(elements[1]);
        $('.paddr_line3').val(elements[2]);
    });


    $(".paddr_submit").click(function () {
        var paddressline1 = $('.paddr_line1').val();
        var paddressline2 = $('.paddr_line2').val();
        var paddressline3 = $('.paddr_line3').val();
        var paddressline4 = $('.paddr_line4').val();
        var paddressline5 = $('.paddr_line5').val();
        var paddressline6 = $('.paddr_line6').val();
        var paddressline7 = $('.paddr_line7').val();
        var joinData = [paddressline1 + ',' + paddressline2 + ',' + paddressline3 + ',' + paddressline4 + ',' + paddressline5 + ',' + paddressline6 + ',' + paddressline7].join(', ');
        $('.permanentAddress').val(joinData);
    });


    $(".reditAddressmodal").click(function () {
        var mystring1 = $('#residenceAddress').val();
        var elements1 = mystring1.split(',');
        $('.raddr_line1').val(elements1[0]);
        $('.raddr_line2').val(elements1[1]);
        $('.raddr_line3').val(elements1[2]);
    });
    $(".raddr_submit").click(function () {
        var readdressline1 = $('.raddr_line1').val();
        var readdressline2 = $('.raddr_line2').val();
        var readdressline3 = $('.raddr_line3').val();
        var readdressline4 = $('.raddr_line4').val();
        var readdressline5 = $('.raddr_line5').val();
        var readdressline6 = $('.raddr_line6').val();
        var readdressline7 = $('.raddr_line7').val();
        var joinData1 = [readdressline1 + ',' + readdressline2 + ',' + readdressline3 + ',' + readdressline4 + ',' + readdressline5 + ',' + readdressline6 + ',' + readdressline7].join(', ');
        $('#residenceAddress').val(joinData1);
    });

    $(".oeditAddressmodal").click(function () {
        var mystring2 = $('#officeAddress').val();
        var elements2 = mystring2.split(',');
        $('.oaddr_line1').val(elements2[0]);
        $('.oaddr_line2').val(elements2[1]);
        $('.oaddr_line3').val(elements2[2]);
    });

    $(".oaddr_submit").click(function () {
        //alert("hi");
        var oaddressline1 = $('.oaddr_line1').val();
        var oaddressline2 = $('.oaddr_line2').val();
        var oaddressline3 = $('.oaddr_line3').val();
        var oaddressline4 = $('.oaddr_line7').val();
        var oaddressline5 = $('.oaddr_line4').val();
        var oaddressline6 = $('.oaddr_line5').val();
        var oaddressline7 = $('.oaddr_line6').val();
        //console.log(oaddressline4);
        //alert(oaddressline7);
        var joinData2 = [oaddressline1 + ',' + oaddressline2 + ',' + oaddressline3 + ',' + oaddressline4 + ',' + oaddressline5 + ',' + oaddressline6 + ',' + oaddressline7].join(', ');
        $('#officeAddress').val(joinData2);

    });



    $('#tpreffered_mode_contact_edit').click(function () {
        $("#modeOfCon").css("display", "block");
        $("#tpreffered_mode_contact").css("display", "none");
        $("#tpreffered_mode_contact_edit").css("display", "none");
    });

    $('#tpreffered_day_contact_edit').click(function () {
        $("#daysWeek").css("display", "block");
        $("#tpreffered_day_contact").css("display", "none");
        $("#tpreffered_day_contact_edit").css("display", "none");
    });
    $("#preffered_contact_num").on('change', function () {
        var getValue = $(this).val();
        $('#tanniversary_date1').val(getValue);
    });
    $(".box").click(function () {
        $(this).box().toggleClass("circle");
    });


    var test = "";
    var varWhatdidSay = "";
    var varPickupaddress = "";
    var varAlreadyServiced = "";
    var varAlreadyServicedR = "";
    var varAddaddress = "";
    var varTransferTootherCity = "";
    var varAlreadyservicedadio = "";
    var varServicedAtOtherDealer = "";
    var mAlreadyServiced = "";
    var mLeadYes = "";
    var mcallInOut = "";
    var varPickupaddressIn = "";
    var mRandam = "";
    var mVehicleSold = "";
    var mPurchase = "";
    var mLastQuestion = "";
    //Inound variable
    var mLeadYesIn = "";
    var mfeedbackYesIn = "";

    //OutGoing Call Back to Next------------------------------------------->

    $("#NextToPurchaseCar").click(function () {

        var vcustname = $("#customerFNameConfirm").val();
        var vcustMobile1 = $("#Mobile1").val();
        var vcustPinInput = $("#PinInput").val();

        if (vcustname == '' && vcustMobile1 == '') {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Enter The New Owner Details.'
            });
            return false;

        }

        else if (vcustname == '') {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Enter Name'
            });
            return false;

        }
        else if (vcustMobile1 == '') {


            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Enter Phone No.'
            });

            return false;



        }
        else if (vcustMobile1.length < 10) {


            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Invalid Phone No.'
            });

            return false;



        }



        else {
            $("#VehicleSoldClickNo").show();
            $("#VehicelSoldYesRNo").hide();
            $("#whatDidCustSayDiv").hide();
            $(".VehicleSold").hide();
            $("#VehicleSoldClickYes").hide();
        }

    });

    $("#BackToPleaseComfNewOwnre").click(function () {
        $("#VehicelSoldYesRNo").show();
        $(".VehicleSold").show();
        $('.VehicleSold').removeAttr('checked', false);
        //$("#WhatdidtheCustomersayDIV").show();
        $("#whatDidCustSayDiv").show();
        $("#VehicleSoldClickNo").hide();
        $("#VehicleSoldClickYes").hide();
        $('#VehicleSoldYesbtn').attr('checked', false);
        $('#VehicleSoldNobtn').attr('checked', false);

    });


    $("#NextToLastQuestion").click(function () {

        var selectValDrop = $('#selected_department1').val();

        var selectValRemarks = $('#commentsOfFB').val();

        var userfeedbackOutbound = 0;
        $('[name="listingForm.userfeedback"]').each(function () {
            if ($(this).is(':checked')) userfeedbackOutbound++;
        });
        if (userfeedbackOutbound == 0) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Choose one of these.'
            });
            return false;
        } else {

            if ($("#feedbackYes").prop('checked')) {

                if (selectValDrop == 0) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please Select Department.'
                    });
                    return false;
                }

                if (selectValRemarks == '') {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Remarks should not be empty.'
                    });
                    return false;
                }
                else {
                    $("#LastQuestion").show();
                    $("#CustFeedBack").hide();
                }
            }
            $("#LastQuestion").show();
            $("#CustFeedBack").hide();

        }

    });

    $("#BackToCustomerFeedback").click(function () {
        $("#CustFeedBack").show();
        $("#LastQuestion").hide();

    });



    $("#backtoMain").click(function () {
        $("#SMRInteractionFirst").show();
        $("#DidYouTalkDiv").show();
        $("#whatDidCustSayDiv").hide();
        $("#serviceBookDiv").hide();
        $('#SpeakYes').attr('checked', false);
        $('#SpeakNo').attr('checked', false);

    });


    $("#CallaterBack").click(function () {
        $("#SMRInteractionFirst").show();
        $("#DidYouTalkDiv").show();
        $("#whatDidCustSayDiv").hide();
        $("#callMeLattteDiv").hide();
        $('#SpeakYes').attr('checked', false);
        $('#SpeakNo').attr('checked', false);
        clearcallmeLater();
    });

    $("#DidUTalkNO").click(function () {
        $("#SMRInteractionFirst").show();
        $("#DidYouTalkDiv").show();
        $("#NotSpeachDiv").hide();
        $("#NoComments").hide();
        $('#SpeakYes').attr('checked', false);
        $('#SpeakNo').attr('checked', false);

    });

    $("#backToSNR").click(function () {
        $("#SMRInteractionFirst").hide();
        $("#DidYouTalkDiv").hide();
        $("#alreadyserviceDIV").show();
        $("#whatDidCustSayDiv").show();
        $('#SpeakYes').attr('checked', false);
        $('#SpeakNo').attr('checked', false);
        $("#VehicleSoldClickYes").hide();
        $("#WhatdidtheCustomersayDIV").show();
        $("#VehicelSoldYesRNo").show();
        $(".VehicleSold").show();
        $('#VehicleSoldYesbtn').attr('checked', false);

    });



    $("#nextToFinalEditInfoVS").click(function () {
        //$("#LastQuestion").show();
        $("#VehicelSoldQuestion").show();
        $("#VehicleSoldClickNo").hide();
    });
    $("#BackToVSHVNewCar").click(function () {
        //$("#LastQuestion").show();
        $("#VehicleSoldClickNo").show();
        $("#VehicelSoldQuestion").hide();
    });
    $("#NextDistanceForPopup").click(function () {
        $("#DistanceFoRRQuestion").show();
        $("#DistancefromDealerLocationDIV").hide();
    });

    //$("#nextToCustomerDrive1").click(function () {

    //    var vVhicle = $("#vehicle").val();
    //    var vRenewalTypeID = $("#renewalTypeID").val();
    //    var vRenewalModeID = $("#renewalModeID").val();
    //    var vPaymentTypeID = $("#paymentTypeID").val();
    //    var vTypeOfPickup = $("input[name='appointbooked.typeOfPickup']:checked").val();
    //    var vAddressMSSId = $("#AddressMSSId").val();

    //    var vaddressLenght = 0;
    //    //chethan added
    //    var vdiscountID = $("#discount").val();
    //    //    var vCouponID = $("#coupon").val();

    //    var vSchedule = false;

    //    //chethan pawan aded based on dealer code
    //    var dealercode = $('#PkDealercode').val();

    //    if (dealercode == "PAWANHYUNDAI") {
    //        //validations for pawan hyundai chethan added on 14-05-2020

    //        if (vTypeOfPickup == "Online") {
    //            vSchedule = true;

    //            var onlineDate = $('#insdate123').val();
    //            var onlineTime = $('#appointmentTime1').val();

    //            if (onlineDate == "" || onlineDate == null) {
    //                Lobibox.notify('warning', {
    //                    continueDelayOnInactiveTab: true,
    //                    msg: 'Please Enter Date.'
    //                });
    //                return false;
    //            }
    //            if (onlineTime == "" || onlineTime == null) {
    //                Lobibox.notify('warning', {
    //                    continueDelayOnInactiveTab: true,
    //                    msg: 'Please Enter Time.'
    //                });
    //                return false;
    //            }

    //        }
    //        else if (vTypeOfPickup == "Field") {

    //            var fieldapdatepawan = $('#fieldpawandate').val();
    //            var fieldtimepawan = $('#fieldpawanappointmentTime').val();

    //            if (fieldapdatepawan == "" || fieldapdatepawan == null) {
    //                Lobibox.notify('warning', {
    //                    continueDelayOnInactiveTab: true,
    //                    msg: 'Please Enter Date.'
    //                });
    //                return false;
    //            }
    //            else if (fieldtimepawan == "" || fieldtimepawan == null) {
    //                Lobibox.notify('warning', {
    //                    continueDelayOnInactiveTab: true,
    //                    msg: 'Please Enter Time.'
    //                });
    //                return false;
    //            }
    //            else {
    //                vSchedule = true;
    //            }

    //        }
    //        //**************************Chethan **********************************
    //        else if (vTypeOfPickup == "Walk-in") {
    //            var vWalkinLocation = $("#walkinLocation").val();
    //                var vInsdate12345 = $("#insdate12345").val();
    //                if (vInsdate12345 != "") {
    //                    var vAppointmentTime = $("#appointmentTime").val();
    //                    if (vAppointmentTime != "") {
    //                            vSchedule = true;
    //                        }
    //                }
    //        }
    //        if (vSchedule && vVhicle != "" && vRenewalTypeID != "" && vRenewalModeID != "0" && vPaymentTypeID != "" && vdiscountID != "" && vTypeOfPickup) {
    //            $("#CustomerDriveInDiv").show();
    //            $("#serviceBookDiv").hide();
    //            $("#SMRInteractionFirst").hide();
    //            $("#DidYouTalkDiv").hide();
    //            $("#whatDidCustSayDiv").hide();

    //        }

    //        else {

    //            if (vVhicle == '0' || vVhicle == 'null') {
    //                Lobibox.notify('warning', {
    //                    continueDelayOnInactiveTab: true,
    //                    msg: 'Please Select Vehicle.'
    //                });
    //                return false;
    //            }

    //            if (vRenewalTypeID == "" || vRenewalTypeID == null) {
    //                Lobibox.notify('warning', {
    //                    continueDelayOnInactiveTab: true,
    //                    msg: 'Please Select Renewal Type.'
    //                });
    //                return false;
    //            }
    //            if (vRenewalModeID == "0" || vRenewalModeID == null) {
    //                Lobibox.notify('warning', {
    //                    continueDelayOnInactiveTab: true,
    //                    msg: 'Please Select Renewal Mode.'
    //                });
    //                return false;
    //            }
    //            if (vPaymentTypeID == "" || vPaymentTypeID == null) {
    //                Lobibox.notify('warning', {
    //                    continueDelayOnInactiveTab: true,
    //                    msg: 'Please Select Payment Mode.'
    //                });
    //                return false;
    //            }

    //            //Chethan added
    //            if (vdiscountID == "" || vdiscountID == null) {
    //                Lobibox.notify('warning', {
    //                    continueDelayOnInactiveTab: true,
    //                    msg: 'Please Enter Discount Value.'
    //                });
    //                return false;
    //            }

    //            if (!vTypeOfPickup) {
    //                Lobibox.notify('warning', {
    //                    continueDelayOnInactiveTab: true,
    //                    msg: 'Please Select Appt Mode.'
    //                });
    //                return false
    //            }
    //            if (vTypeOfPickup == "Walk-in" && !vSchedule) {
    //                var vWalkinLocation = $("#walkinLocation").val();
    //                var vInsdate12345 = $("#insdate12345").val();
    //                var vAppointmentTime = $("#appointmentTime").val();
    //                var vInsuranceAgentDataId = $("#insuranceAgentDataId").val();

    //                 if (vInsdate12345 == "") {
    //                    Lobibox.notify('warning', {
    //                        continueDelayOnInactiveTab: true,
    //                        msg: 'Please Provide Appointment Date.'
    //                    });
    //                    return false
    //                }
    //                else if (vAppointmentTime == "") {
    //                    Lobibox.notify('warning', {
    //                        continueDelayOnInactiveTab: true,
    //                        msg: 'Please Provide Appointment Time.'
    //                    });
    //                    return false
    //                }
    //            }
    //        }




    //    }
    //    else {
    //    if (vAddressMSSId != null) {
    //        vaddressLenght = vAddressMSSId.length;
    //    }
    //    if (vTypeOfPickup == "Online") {
    //        vSchedule = true;

    //        var onlineDate = $('#insdate123').val();
    //        var onlineTime = $('#appointmentTime1').val();

    //        if (onlineDate == "" || onlineDate == null) {
    //            Lobibox.notify('warning', {
    //                continueDelayOnInactiveTab: true,
    //                msg: 'Please Enter Date.'
    //            });
    //            return false;
    //        }
    //        if (onlineTime == "" || onlineTime == null) {
    //            Lobibox.notify('warning', {
    //                continueDelayOnInactiveTab: true,
    //                msg: 'Please Enter Time.'
    //            });
    //            return false;
    //        }

    //    }
    //    else if (vTypeOfPickup == "Field") {
    //        var DateTime = $('#tableschID').val();

    //        // var selectedfiled = $('#startValueIns').val();
    //        var selectedfiled = $('#driverValueIns').val();
    //        //var selectedfiled2=$('#endValueIns').val();
    //        console.log("startdate : " + selectedfiled);
    //        // console.log("end date :"+selectedfiled2);

    //        if (selectedfiled != null) {

    //            if (selectedfiled == '0' || vVhicle == 'null') {
    //                vSchedule = false;
    //            }
    //            else {
    //                if (vaddressLenght > 0) {
    //                    vSchedule = true;
    //                }
    //                else {
    //                    vSchedule = false;
    //                }
    //            }
    //        }


    //    }
    //    //**************************Chethan **********************************
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
    //    if (vSchedule && vVhicle != "" && vRenewalTypeID != "" && vRenewalModeID != "0" && vPaymentTypeID != "0" && vdiscountID != "" && vTypeOfPickup) {
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

    //        //Chethan added
    //        if (vdiscountID == "" || vdiscountID == null) {
    //            Lobibox.notify('warning', {
    //                continueDelayOnInactiveTab: true,
    //                msg: 'Please Enter Discount Value.'
    //            });
    //            return false;
    //        }
    //        //if (vCouponID == "" || vCouponID == null) {
    //        //    Lobibox.notify('warning', {
    //        //        continueDelayOnInactiveTab: true,
    //        //        msg: 'Please Select Coupons.'
    //        //    });
    //        //    return false;
    //        //}


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
    //        if (vTypeOfPickup == "Field" && !vSchedule) {

    //            Lobibox.notify('warning', {
    //                continueDelayOnInactiveTab: true,
    //                msg: 'Please Provide Schedule Date and Field Location.'
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
    //}

    //});





    $("#nextToCustomerDrive").click(function () {

        var FollowupDateSbooked = $("#date12345").val();
        var FollowupTimeSbooked = $("#CommittedTimes").val();
        var FollowupTypeSbooked = $("#serviceBookedTypeDisposition").val();
        var BookingSource = $("#bookingSource").val();
        var sb_city = $("#SB_city").val();
        var pkDealer = $('#PkDealercode').val()
        //var mSerAdvi=$("#serviceAdvisor").val();
        var mWorksId = $("#SB_workshop").val();
        var minsuAgent = $(".insuranceAgent").val();
        if (mWorksId != "" && FollowupDateSbooked != "" && FollowupTimeSbooked != "" && minsuAgent != "" && FollowupTypeSbooked != "" && sb_city != "") {

            if (pkDealer === "HANSHYUNDAI" || pkDealer === "HARPREETFORD" || pkDealer === "GALAXYTOYOTA" || pkDealer === "TOYOTADEMO" ) {
                if (BookingSource == "" || BookingSource == "--Select--" || BookingSource == "0") {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please select Booking Source.'
                    });
                    return false;
                }
            }


            $("#CustomerDriveInDiv").show();
            $("#serviceBookDiv").hide();
            $("#SMRInteractionFirst").hide();
            $("#DidYouTalkDiv").hide();
            $("#whatDidCustSayDiv").hide();

            var pickFlag = $('#selectedModeOfCont').val();
            if (pickFlag == "Customer Drive-In") {
                $("#CustomerDriveInID").prop("checked", true);


            } else if (pickFlag == "true") {
                $("#PickupDropRequired").prop("checked", true);

            }

            else if (pickFlag == "Mobile Service Support") {
                $("#MaruthiMobileSupport").prop("checked", true);
            }

        } else {

            if (sb_city == "") {
                if (FollowupTypeSbooked == 'select' || FollowupTypeSbooked == '') {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please select Location'
                    });
                    return false;
                }
            }


            if (FollowupTypeSbooked == 'select' || FollowupTypeSbooked == '') {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please select Service Booked Type.'
                });
                return false;
            }

            if (mWorksId == 'select' || mWorksId == '' || mWorksId == 'Select' || mWorksId == '') {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please select Workshop.'
                });
                return false;
            }
            if (FollowupDateSbooked == "") {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please select date.'
                });
                return false;
            }
            if (FollowupTimeSbooked == "") {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please select time.'
                });
                return false;
            }

            if (minsuAgent == null || minsuAgent == '') {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please Select Service Advisor'
                });
                return false;

            }
        }

    });

    $("#BackToCunstomerDrive").click(function () {
        $("#serviceBookDiv").show();
        $("#CustomerDriveInDiv").hide();
        $("#SMRInteractionFirst").hide();
        $("#DidYouTalkDiv").hide();
        $("#whatDidCustSayDiv").show();

        $("#hdDriverId").val('0');
        $('#hdPickUpTime').val('');
        $('#hdPickUpTimeRange').val('');
        $("#tblDriverAllocation thead").remove();
        $("#tblDriverAllocation tbody").remove();

    });




    $("#BackToLead").click(function () {
        /* 	$("#nomineeDetails3").show();
            $("#finalDiv1").hide(); */
        $("#CustomerDriveInDiv").show();
        $("#finalDiv1").hide();
    });

    $("#BackToInsuNomine").click(function () {

        $("#nomineeDetails3").show();
        $("#finalDiv1").hide();
    });

    $("#BackToCunstomerDriveIndu").click(function () {

        $("#whatDidCustSayDiv").show();
        $("#serviceBookDiv").show();
        $("#finalDiv1").hide();
        $("#CustomerDriveInDiv").hide();



    });
    //			
    $("#NextToCustFeedBack").click(function () {

        var atLeastOneIsChecked = 0;
        $('[name="listingForm.LeadYes"]').each(function () {
            if ($(this).is(':checked')) atLeastOneIsChecked++;
        });
        if (atLeastOneIsChecked == 0) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please check one.'
            });

        } else {
            if ($("#LeadYesID").prop('checked')) {


                var checkeds = $('.myOutCheckbox').is(':checked');

                if (checkeds) {

                } else {

                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please check one of these.'

                    });
                    return false;
                }

            } else if ($("#LeadNoID").prop('checked')) {

            }


            $("#CustFeedBack").show();
            $("#finalDiv1").hide();
        }

        var complFB = $('#selectedFBComp').val();

        if (complFB == "0" || complFB == null || complFB == "") {
            //$("#feedbackNo").prop("checked", true);
        } else {
            //$("#feedbackYes").prop("checked", true);
            $("#feedbackDIV").show();
            //loadLeadBasedOnLocationDepartment();
            var rem = $('#enteredRMFB').val();
            $('#commentsOfFB').val(rem);

        }

    });

    $("#NextToPurchaseNewcarNO").click(function () {
        var chkVehSold = 0;
        $('[name="listingForm.VehicleSoldYes"]').each(function () {
            if ($(this).is(':checked')) chkVehSold++;
        });
        if (chkVehSold == 0) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please choose one.'
            });
            return false;

        } else {
            $("#VehicleSoldClickNo").show();
            $("#VehicelSoldYesRNo").hide();
            $("#whatDidCustSayDiv").hide();
            $(".VehicleSold").hide();
        }

    });


    $("#NextToAlreadyServicePopup").click(function () {
        $("#AlreadySerivePopup").show();
        $("#alreadyservicedDiv1").hide();
    });

    //added on 02/01/2017		
    $("#nextToAlreadySrviceUpsell").click(function () {
        //$("input[name$='LeadYesAlradyService']").attr('checked',false);

        //if (document.getElementById("dealerNameId").value == 0) {
        //    Lobibox.notify('warning', {
        //        continueDelayOnInactiveTab: true,
        //        msg: 'Please Select DealerName.'
        //    });
        //    return false;
        //}

        var typeOfpage = $("#typeOfDispoPageView").val();
        //alert(typeOfpage);

        if (typeOfpage == "service" || typeOfpage == "Service" || typeOfpage == "serviceSearch" || typeOfpage == "Service_MCP") {
            //service Part
            var chknoReq = 0;
            $('[name="srdisposition.reasonForHTML"]').each(function () {
                if ($(this).is(':checked')) chknoReq++;
            });

            var chknoReqChi = 0;
            $('[name="srdisposition.ServicedAtOtherDealerRadio"]').each(function () {
                if ($(this).is(':checked')) chknoReqChi++;
            });


            if (chknoReq == 0) {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please choose one.'
                });
                return false;
                //----
                //else if{
                //    if($('#dealerNameId').val() == "" || $('#dealerNameId').val() == "Select") {

                //    Lobibox.notify('warning', {
                //        msg: 'please select Dealer Name.'
                //    });

                //    return false;
                //}
                //}

            } else {
                if ($('#ServicedOtherDealer').prop('checked')) {
                    if (chknoReqChi == 0) {
                        Lobibox.notify('warning', {
                            continueDelayOnInactiveTab: true,
                            msg: 'Please choose one.'
                        });
                        return false;

                    }
                    if ($('#AutorizedworkshopRD').prop('checked')) {
                        var dealerCode = $('#PkDealercode').val();
                        if ($('#CheckedwithDMS').prop('checked') != true) {
                            Lobibox.notify('warning', {
                                continueDelayOnInactiveTab: true,
                                msg: 'Please check "I have verified with MDW."'
                            });
                            return false;
                        }
                        if (dealerCode == "ADVAITHHYUNDAI") {
                            if ($('#dateOfService').val() == "") {

                                Lobibox.notify('warning', {
                                    msg: 'please select Date.'
                                });

                                return false;
                            }
                            if ($('#mileageAtService').val() == "") {

                                Lobibox.notify('warning', {
                                    msg: 'please select mileage.'
                                });
                                
                                return false;
                            }


                            if ($('#serviceType').val() == "" ) {

                                Lobibox.notify('warning', {
                                    msg: 'please select serviceType.'
                                });

                                return false;
                            }
                        }

                       


                    }
                } else {

                }

                $("#AlreadyServiceUpsellOpp").show();
                $("#alreadyservicedDiv1").hide();
                $(".AlreadyServiced").hide();
                $(".AlreadyServiced").attr('checked', true);
                $("#LeadNoUpselOpp").attr('checked', false);

                $("#WhatdidtheCustomersayDIV").hide();
                $(".insuLead").prop("checked", false);
                $("#LeadDivAlreadyService").hide();
                $("#InsuranceSelectAlreadyService").hide();
                $("#WARRANTYSelectAlreadyService").hide();
                $("#VASTagToSelectAlreadyService").hide();
                $("#ReFinanceSelectAlreadyService").hide();
                $("#LoanSelectAlreadyService").hide();
                $("#EXCHANGEIDSelectAlreadyService").hide();
                $("#UsedCarSelectAlreadyService").hide();
            }
        } else if (typeOfpage === "insurance") {

            //insurance part
            var chknoReqInsur = 0;
            $('[name="renewalDoneBy"]').each(function () {
                if ($(this).is(':checked')) chknoReqInsur++;
            });

            var chknoReqChiInsur = 0;
            $('[name="ir_disposition.typeOfAutherization"]').each(function () {
                if ($(this).is(':checked')) chknoReqChiInsur++;
            });

            var chknoReqChiInsur = 0;
            $('[name="insudisposition.typeOfAutherization"]').each(function () {
                if ($(this).is(':checked')) chknoReqChiInsur++;
            });


            if (chknoReqInsur == 0) {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please choose one.'
                });
                return false;

            } else {
                if ($('#renewedOtherDealer').prop('checked')) {
                    if (chknoReqChiInsur == 0) {
                        Lobibox.notify('warning', {
                            continueDelayOnInactiveTab: true,
                            msg: 'Please choose one.'
                        });
                        return false;

                    }
                }

                $("#AlreadyServiceUpsellOpp").show();
                $("#alreadyservicedDiv1").hide();
                $(".AlreadyServiced").hide();
                $(".AlreadyServiced").attr('checked', true);
                $("#WhatdidtheCustomersayDIV").hide();
                $(".insuLead").prop("checked", false);
                $("#LeadDivAlreadyService").hide();
                $("#InsuranceSelectAlreadyService").hide();
                $("#WARRANTYSelectAlreadyService").hide();
                $("#VASTagToSelectAlreadyService").hide();
                $("#ReFinanceSelectAlreadyService").hide();
                $("#LoanSelectAlreadyService").hide();
                $("#EXCHANGEIDSelectAlreadyService").hide();
                $("#UsedCarSelectAlreadyService").hide();

            }
        }
    });

    $("#NextDisatisfiedPrPopup").click(function () {
        var AssignedToSA = $("#assignedToSA").val();
        var fromMail = $('#fromEmailId');
        var needToMove = 0;
        if (AssignedToSA != '0' && AssignedToSA != 'Select' && AssignedToSA != 'select') {
            needToMove = 1;
        } else {
            if (AssignedToSA == '0' || AssignedToSA == '' || AssignedToSA == 'Select' || AssignedToSA == 'select') {
                alert('Please Assign To Service Advisor');
            }
        }

        if (fromMail.length > 0) {
            var isPassword = $(fromMail).find(':selected').attr('data-pwd');

            if (isPassword == "No") {
                if ($('#fromPassword').val() == "") {
                    alert('Please Enter Password...');
                    needToMove = 0;
                }
            }
        }

        if (needToMove == 1) {
            $("#DisatisfiedPreQuestion").show();
            $("#txtDissatisfiedwithpreviousservice").hide();
        }

    });
    $("#NextDisSatisSalePopup").click(function () {
        $("#DisstisFiedSaleRQuestion").show();
        $("#DissatisfactionwithSalesREmarksDiv").hide();

    });
    $("#NextDisSatInsurancPopup").click(function () {
        $("#DisstisInsurancQuestion").show();
        $("#DissatisfactionwithInsuranceREmarksDiv").hide();

    });

    //Heena changes
    $("#NextDisSatisClaimsPopup").click(function () {
        $("#DisstisInsurancQuestion").show();
        $("#DissatisfactionwithclaimsREmarksDiv").hide();

    });

    $("#NextOthersPopup").click(function () {
        var commentRemarks = $('#commentsOtherRemarks').val();
        if (commentRemarks == '') {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Remarks sholud not be Blank.'
            });
        } else {
            $("#OthersLastQuestion").show();
            $("#OtherSeriveRemarks").hide();
        }

    });







    //Incomming Call Back to next---------------------------------------->
    $("#NextToLead").click(function () {

        var Creremarks = $('#creRemarks').val();
        var complFB = $('#selectedFBComp').val();

        if (complFB == "0" || complFB == null || complFB == "") {
            //$("#feedbackNo").prop("checked", true);
        } else {
            //$("#feedbackYes").prop("checked", true);
            //$("#feedbackDIV").show();
            //loadLeadBasedOnLocationDepartment();
            var rem = $('#enteredRMFB').val();
            $('#commentsOfFB').val(rem);

        }

        var inc = 0;
        $('[name="servicebooked.typeOfPickup"]').each(function () {
            if ($(this).is(':checked')) inc++;
        });
        if (inc == 0) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please check one.'
            });
        }
        else {
            if ($('#PickupDropRequired').prop('checked')) {
                var selectDriverVal = $('#driverIdSelect').val();
                var selectTimeFromVal = $('#time_FromDriver').val();
                var selectTimeToVal = $('#time_ToDriver').val();

                var DealerCode = $('#PkDealercode').val();

                if (selectDriverVal == 0) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please Select from Drop down.'
                    });
                    return false;
                }
                if (selectTimeFromVal == '') {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'From Time Is Required.'
                    });
                    return false;
                }
                if (selectTimeToVal == '') {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'To Time Is Required.'
                    });
                    return false;
                }

                var vAddressMss = $("#AddressMSSId").val();

                if (vAddressMss == null) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please Select/Add Drop Address'
                    });
                    return false;

                }

                //if (DealerCode != "KALYANIMOTORS") {
                //    //For Driver Scheduler
                //    var smrDropAddress = $("#smrDropAddress").val();

                //    if (smrDropAddress == null) {
                //        Lobibox.notify('warning', {
                //            continueDelayOnInactiveTab: true,
                //            msg: 'Please Select/Add Drop Address'
                //        });
                //        return false;

                //    }


                //    var hdpickUpdate = $("#hdpickUpdate").val();

                //    if (hdpickUpdate == "") {
                //        Lobibox.notify('warning', {
                //            continueDelayOnInactiveTab: true,
                //            msg: 'Please Select PickUp Date'
                //        });
                //        return false;

                //    }

                //    var hddropdate = $("#hddropdate").val();

                //    if (hddropdate == "") {
                //        Lobibox.notify('warning', {
                //            continueDelayOnInactiveTab: true,
                //            msg: 'Please Select Drop Date'
                //        });
                //        return false;

                //    }

                //    var hdDriverId = $("#hdDriverId ").val();

                //    if (hdDriverId == "0") {
                //        Lobibox.notify('warning', {
                //            continueDelayOnInactiveTab: true,
                //            msg: 'Please Select Driver'
                //        });
                //        return false;

                //    }
                //}
                if (DealerCode == "INDUS") {
                    var hdpickUpdate = $("#hdpickUpdate").val();
                    var hdDriverId = $("#hdDriverId ").val();


                    if (hdDriverId == "0") {
                        Lobibox.notify('info', {
                            continueDelayOnInactiveTab: true,
                            msg: 'Driver Not Allocated.'
                        });
                    }
                }

            }
            else if ($('#DropRequired').prop('checked')) {
                var selectDriverVal = $('#driverIdSelect').val();
                var selectTimeFromVal = $('#time_FromDriver').val();
                var selectTimeToVal = $('#time_ToDriver').val();

                var DealerCode = $('#PkDealercode').val();
                if (selectDriverVal == 0) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please Select from Drop down.'
                    });
                    return false;
                }
                if (selectTimeFromVal == '') {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'From Time Is Required.'
                    });
                    return false;
                }
                if (selectTimeToVal == '') {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'To Time Is Required.'
                    });
                    return false;
                }

                var vAddressMss = $("#AddressMSSId").val();

                if (vAddressMss == null) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please Select/Add PickUp Address'
                    });
                    return false;

                }

                //For Driver Scheduler
                //if (DealerCode != "KALYANIMOTORS") {
                //    var hdpickUpdate = $("#hddropdate").val();

                //    if (hdpickUpdate == "") {
                //        Lobibox.notify('warning', {
                //            continueDelayOnInactiveTab: true,
                //            msg: 'Please Select Drop Date'
                //        });
                //        return false;

                //    }

                //    var hdDriverId = $("#hdDriverId ").val();

                //    if (hdDriverId == "0") {
                //        Lobibox.notify('warning', {
                //            continueDelayOnInactiveTab: true,
                //            msg: 'Please Select Driver'
                //        });
                //        return false;

                //    }
                //}
                if (DealerCode == "INDUS") {
                    var hdpickUpdate = $("#hdpickUpdate").val();
                    var hdDriverId = $("#hdDriverId ").val();


                    if (hdDriverId == "0") {
                        Lobibox.notify('info', {
                            continueDelayOnInactiveTab: true,
                            msg: 'Driver Not Allocated.'
                        });
                    }
                }
            }
            else if ($('#PickupOnly').prop('checked')) {
                var selectDriverVal = $('#driverIdSelect').val();
                var selectTimeFromVal = $('#time_FromDriver').val();
                var selectTimeToVal = $('#time_ToDriver').val();

                var DealerCode = $('#PkDealercode').val();
                if (selectDriverVal == 0) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please Select from Drop down.'
                    });
                    return false;
                }
                if (selectTimeFromVal == '') {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'From Time Is Required.'
                    });
                    return false;
                }
                if (selectTimeToVal == '') {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'To Time Is Required.'
                    });
                    return false;
                }

                var vAddressMss = $("#AddressMSSId").val();

                if (vAddressMss == null) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please Select/Add PickUp Address'
                    });
                    return false;

                }

                //For Driver Scheduler
                //if (DealerCode != "KALYANIMOTORS")
                //{
                //    var hdpickUpdate = $("#hdpickUpdate").val();

                //    if (hdpickUpdate == "") {
                //        Lobibox.notify('warning', {
                //            continueDelayOnInactiveTab: true,
                //            msg: 'Please Select PickUp Date'
                //        });
                //        return false;

                //    }

                //    var hdDriverId = $("#hdDriverId ").val();

                //    if (hdDriverId == "0") {
                //        Lobibox.notify('warning', {
                //            continueDelayOnInactiveTab: true,
                //            msg: 'Please Select Driver'
                //        });
                //        return false;

                //    }
                //}

                //For Driver Scheduler
                if (DealerCode == "INDUS") {
                    var hdpickUpdate = $("#hdpickUpdate").val();
                    var hdDriverId = $("#hdDriverId ").val();


                    if (hdDriverId == "0") {
                        Lobibox.notify('info', {
                            continueDelayOnInactiveTab: true,
                            msg: 'Driver Not Allocated..'
                        });
                    }
                }
            }
            else if ($('#PickupAndMOC').prop('checked')) {
                var selectDriverVal = $('#driverIdSelect').val();
                var selectTimeFromVal = $('#time_FromDriver').val();
                var selectTimeToVal = $('#time_ToDriver').val();
                if (selectDriverVal == 0) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please Select from Drop down.'
                    });
                    return false;
                }
                if (selectTimeFromVal == '') {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'From Time Is Required.'
                    });
                    return false;
                }
                if (selectTimeToVal == '') {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'To Time Is Required.'
                    });
                    return false;
                }

                var vAddressMss = $("#AddressMSSId").val();

                if (vAddressMss == null) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please Add New Address'
                    });
                    return false;

                }

            } else if ($('#RoadSideAssitantIdIs').prop('checked')) {
                var vAddressMss = $("#AddressMSSId").val();
                if (vAddressMss == null) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please Add New Address.'
                    });
                    return false;

                }

            }
            else if ($('#DoorStepServiceID').prop('checked')) {
                var vAddressMss = $("#AddressMSSId").val();
                if (vAddressMss == null) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please Add New Address.'
                    });
                    return false;

                }

            }




            else
                if ($('#MaruthiMobileSupport').prop('checked')) {
                    var vAddressMss = $("#AddressMSSId").val();
                    if (vAddressMss == null) {
                        Lobibox.notify('warning', {
                            continueDelayOnInactiveTab: true,
                            msg: 'Please Add New Address.'
                        });
                        return false;

                    }

                }



            if ((pkDealer == "INDUS") && (Creremarks == '' || Creremarks.length < 10)) {
                Lobibox.notify('warning',
                    {
                        msg: 'CRE Feedback should be minimum 10 characters'
                    });
                return false;
            }

            $("#finalDiv1").show();
            $("#CustomerDriveInDiv").hide();

        }

        
        

        var upselFlag = $('#selectedUpselOpp').val();

        if (upselFlag == "0" || upselFlag == null || upselFlag == "") {
            $("#LeadNoID").prop("checked", true);
        } else {
            $("#LeadYesID").prop("checked", true);
            $("#LeadDiv").show();
        }
        //loadLeadBasedOnLocation();
        var sr_int_id = $('#srdispo_id').val();
        //alert("sr_int_id : "+sr_int_id);		

        var urlDisposition = siteRoot + "/CallLogging/getUpsellLeadsSeletedInLastSB/";

        $.ajax({
            type: 'POST',
            url: urlDisposition,
            datatype: 'json',
            data: { srId: sr_int_id },
            cache: false,
            success: function (res) {
                console.log(res.upselData);
                if (res.success == true) {
                    for (var i = 0; i < res.upselData.length; i++) {

                        if (res.upselData[i].upSellType == "Insurance") {

                            $('#InsuranceIDCheck').prop('checked', true);
                            $('#InsuranceSelect').show();
                            $('#comments1').val(res.upselData[i].upsellComments);


                        } else if (res.upselData[i].upSellType == "Warranty / EW") {
                            $('#WARRANTYID').prop('checked', true);
                            $('#WARRANTYSelect').show();
                            $('#comments2').val(res.upselData[i].upsellComments);

                        } else if (res.upselData[i].upSellType == "Re-Finance / New Car Finance") {

                            $('#ReFinanceIDCheck').prop('checked', true);
                            $('#ReFinanceSelect').show();
                            $('#comments3').val(res.upselData[i].upsellComments);


                        } else if (res.upselData[i].upSellType == "VAS") {

                            $('#VASID').prop('checked', true);
                            $('#VASTagToSelect').show();
                            $('#comments4').val(res.upselData[i].upsellComments);

                        } else if (res.upselData[i].upSellType == "Sell Old Car") {

                            $('#LoanID').prop('checked', true);
                            $('#LoanSelect').show();
                            $('#comments5').val(res.upselData[i].upsellComments);

                        } else if (res.upselData[i].upSellType == "Buy New Car / Exchange") {

                            $('#EXCHANGEID').prop('checked', true);
                            $('#EXCHANGEIDSelect').show();
                            $('#comments6').val(res.upselData[i].upsellComments);

                        } else if (res.upselData[i].upSellType == "UsedCar") {

                            $('#UsedCarID').prop('checked', true);
                            $('#UsedCarSelect').show();
                            $('#comments7').val(res.upselData[i].upsellComments);
                        }
                    }
                }
                else {
                    alert(res.error);
                }

            }, error(error) {
                alert(error);
            }
        });
    });
    ////< !----------------------- New Thing------------------------------>


    //24thaugustchange
    //$("#NextToLeadInsurance").click(function () {

    //    var atLeastOneIsChecked = 0;
    //    $('[name="listingForm.LeadYes"]').each(function () {
    //        if ($(this).is(':checked')) atLeastOneIsChecked++;
    //    });
    //    if (atLeastOneIsChecked == 0) {
    //        Lobibox.notify('warning', {
    //            continueDelayOnInactiveTab: true,
    //            msg: 'Please check one.'
    //        });

    //    } else {
    //        if ($("#LeadYesID").prop('checked')) {
    //            if ($('#checkboxExist').children().length > 0) {

    //            var checkeds = $('.myOutCheckbox').is(':checked');

    //            if (checkeds) {

    //            } else {

    //                Lobibox.notify('warning', {
    //                    continueDelayOnInactiveTab: true,
    //                    msg: 'Please check one of these.'

    //                });
    //                return false;
    //            }
    //        }
    //        }

    //    }

    //    var complFB = $('#selected_department1').val();

    //    if ($("#CustomerfeedbackYes").prop('checked')) {
    //        if (complFB == "0" || complFB == null || complFB == "") {
    //            Lobibox.notify('warning', {
    //                continueDelayOnInactiveTab: true,
    //                msg: 'Please select one of compliant type'

    //            });
    //            return false;
    //        }

    //    } else {
    //        $.blockUI();

    //    }
    //});

    $("#BackToNewInsu1").click(function () {
        $("#CustomerDriveInDiv").show();
        $("#Add_OnsIndurace1").hide();
    });

    $("#NextToNewInsu1").click(function () {

        var addOnChkatleast1 = 0;
        $('[name="AddOnsYes"]').each(function () {
            if ($(this).is(':checked')) addOnChkatleast1++;
        });
        if (addOnChkatleast1 == 0) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please check one.'
            });
        }
        else {

            if ($("#AddOnsYesRId").prop('checked')) {
                var chkisu = $('.Add_Onschk').is(':checked');

                if (chkisu) {

                } else {

                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please check one of these.'

                    });
                    return false;
                }
            }
            $("#PremiumInsu2").show();
            $("#Add_OnsIndurace1").hide();



        }
    });

    $("#BackToNewInsu2").click(function () {
        $("#Add_OnsIndurace1").show();
        $("#PremiumInsu2").hide();

    });

    $("#NextToNewInsu2").click(function () {
        if (document.getElementById('PremiumYesID').checked) {
            var varInsuCompany = $("#InsuCompSelectID").val();
            if (varInsuCompany == '0') {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please Enter Insurance Company Name'

                });
                return false;

            }
            $("#nomineeDetails3").show();
            $("#PremiumInsu2").hide();

        } else if (document.getElementById('PremiumNoID').checked) {
            $("#nomineeDetails3").show();
            $("#PremiumInsu2").hide();

        }
        else {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Select Any one.'
            });
        }

    });
    $("#BackToNewInsu3").click(function () {
        $("#nomineeDetails3").hide();
        $("#PremiumInsu2").show();

    });

    $("#NextToNewInsu3").click(function () {
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
            $("#finalDiv1").show();
            $("#nomineeDetails3").hide();

        } else if (document.getElementById('nomineeNoID').checked) {
            $("#finalDiv1").show();
            $("#nomineeDetails3").hide();
        }
        else {

            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please check one.'
            });
            return false;
        }

    });
    ////< !----------------- Inbound isurance---------------------->

    ////< !----------------------- New Thing------------------------------>


    $("#NextToLeadInsuranceInB").click(function () {

        if (document.getElementById('homeVisitIdInB').checked) {
            //var selectDriverVal = $('#driverIdSelect').val();
            var varappoFmHMID1InB = $('#appoFmHMID1InB').val();
            var varappoToHMID2InB = $('#appoToHMID2InB').val();


            if (varappoFmHMID1InB == '') {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'From Time Is Required.'
                });
                return false;
            }
            if (varappoToHMID2InB == '') {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'To Time Is Required.'
                });
                return false;
            }
            $("#Add_OnsIndurace1InB").show();
            $("#CustomerDriveInDivInB").hide();
            $("#finalDiv1").hide();
        } else if (document.getElementById('showroomVisitIdInB').checked) {
            var varShowroomsSelectIdInB = $('#ShowroomsSelectIdInB').val();
            var varappoFMRoomIDInB = $('#appoFmRoomIDInB').val();
            var varappoToRoomIDInB = $('#appoToRoomIDInB').val();

            if (varShowroomsSelectId == 0) {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please Select Showrooms.'
                });
                return false;
            }
            if (varappoFMRoomIDInB == '') {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'From Time Is Required.'
                });
                return false;
            }
            if (varappoToRoomIDInB == '') {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'To Time Is Required.'
                });
                return false;
            }
            $("#Add_OnsIndurace1InB").show();
            $("#CustomerDriveInDivInB").hide();
            $("#finalDiv1").hide();
        }
        else {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please check one.'
            });
        }


    });

    $("#BackToNewInsu1InB").click(function () {
        $("#CustomerDriveInDivInB").show();
        $("#Add_OnsIndurace1InB").hide();
    });



    $("#BackToNewInsu2InB").click(function () {
        $("#Add_OnsIndurace1InB").show();
        $("#PremiumInsu2InB").hide();

    });

    $("#NextToNewInsu2InB").click(function () {

        if (document.getElementById('PremiumYesIDInB').checked) {
            var vInsuSelect = $("#InsuCompSelectIDInB").val();
            if (vInsuSelect == 0) {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Insurance Company.'
                });
                return false;
            }

            $("#nomineeDetails3InB").show();
            $("#PremiumInsu2InB").hide();

        }
        else if (document.getElementById('PremiumNoIDInB').checked) {

            $("#nomineeDetails3InB").show();
            $("#PremiumInsu2InB").hide();
        }
        else {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please check one.'
            });
            return false;
        }
    });


    $("#BackToNewInsu3").click(function () {
        $("#nomineeDetails3").hide();
        $("#PremiumInsu2").show();

    });








    ////< !----------------- Inbound isurance end---------------------->

    $("#BackToCunstomerDriveIn").click(function () {
        $("#LeadSourceHideIn").hide();
        $("#SMRInteractionFirst").hide();
        $("#InCallserviceBookDiv").show();
        $("#CustomerDriveInDivIn").hide();
        $("#BookMyServiceIn").show();
    });




    $("#BackToCustomerMainInBound").click(function () {
        $("#LeadSourceHideIn").show();
        $("#SMRInteractionFirst").show();
        $("#InCallServiceBook").show();
        //$('#InCallServiceBook').attr('checked',false);
        $('#BookMyServiceIn').show();
        $('#InCallserviceBookDiv').hide();
        //$('#BookMyServiceIn').removeAttr('checked');
        $('#InCallServiceBook').removeAttr('checked');
    });




    $("#VehicleSoldYesbtn").click(function () {
        $("#WhatdidtheCustomersayDIV").hide();
        $("#VehicelSoldYesRNo").hide();
        $(".VehicleSold").hide();
        $(".backToAllSNR").hide();
        $(".backToYesSNR").show();

    });
    $("#VehicleSoldNobtn").click(function () {
        $(".backToYesSNR").hide();
        $(".backToAllSNR").show();
    });


    $("#backToAlreadyServicediv").click(function () {
        $("#WhatdidtheCustomersayDIV").show();
        $(".VehicleSold").show();

        $("#VehicleSoldYesbtn").attr("checked", false);
        $("#VehicleSoldNobtn").attr("checked", false);
        $("#alreadyserviceDIV").show();

        if ($("#alreadyserviceDIV").attr("checked", false)) {
            $("#VehicelSoldYesRNo").hide();
            $("#VehicleSold").attr("checked", false);
            $("#alreadyservicedDiv1").hide();
            $(".Dissatisfiedwithpreviousservice").show();
            $(".Distancefrom").show();
            $(".DissatisfiedwithSalesID").show();
            $(".DissatisfiedwithInsuranceId").show();
            $(".Stolen").show();
            $(".Totalloss").show();
            $(".OtherLast").show();
            $(".AlreadyServiced").show();
            $("#VehicleSoldClickYes").hide();
            $("#PurchaseClickYes").hide();
            $("#VehicleSoldClickNo").hide();


        }
    });
    $("#NextToCustFeedBackIn").click(function () {

        var chkincl = 0;
        $('[name="LeadYesIn"]').each(function () {
            if ($(this).is(':checked')) chkincl++;
        });
        if (chkincl == 0) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please check one.'
            });

        } else {
            if ($("#LeadYesIDIn").prop('checked')) {
                var checked = $('.myCheckbox').is(':checked');

                if (checked) {

                } else {

                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please check one of these.'

                    });
                    return false;
                }
            } else if ($("#LeadNoIDIn").prop('checked')) {

            }


            $("#CustFeedBackIn").show();
            $("#CustomerDriveInDiv").hide();
            $("#finalDiv1Inbound").hide();
        }


        var complFB = $('#selectedFBComp').val();

        if (complFB == "0" || complFB == null || complFB == "") {
            $("#feedbackNoIn").prop("checked", true);
        } else {
            $("#feedbackYesIn").prop("checked", true);
            $("#feedbackDIVIn").show();
            //loadLeadBasedOnLocationDepartment();
            var rem = $('#enteredRMFB').val();
            $('#commentsDSA').val(rem);

        }


    });

    $("#BackToUpsellIn").click(function () {
        $(".myCheckbox").prop('checked', false);
        $("#CustFeedBackIn").hide();
        //$("#InsuranceSelectIn").hide();
        //$("#WARRANTYSelectIn").hide();
        //$("#VASTagToSelectIn").hide();
        //$("#ReFinanceSelectIn").hide();
        //$("#LoanSelectIn").hide();
        //$("#EXCHANGEIDSelectIn").hide();
        //$("#UsedCarSelectIn").hide();
        $("#finalDiv1Inbound").show();
    });

    $("#BackToUpsell").click(function () {
        //$("input[type=checkbox]"). prop("checked",false);

        // $("#InsuranceSelect").hide();
        // $("#WARRANTYSelect").hide();
        // $("#VASTagToSelect").hide();
        // $("#ReFinanceSelect").hide();
        // $("#LoanSelect").hide();
        // $("#EXCHANGEIDSelect").hide();
        // $("#UsedCarSelect").hide();
        $("#CustFeedBack").hide();
        $("#finalDiv1").show();
    });


    $("input[name$='InOutCallName']").click(function () {
        var mcallInOut = $(this).val();
        //alert(mcallInOut);
        if (mcallInOut == "OutCall") {
            //$('#OutGoingID').attr('checked',true);
            $("#OutBoundDiv").show();
            $("#InBoundDiv").hide();

        }
        else {
            $("#InBoundDiv").show();
            $("#OutBoundDiv").hide();
            //$('#InCallServiceBook').attr('checked',true);

            $("#BookMyServiceIn").show();
        }
    });



    //PickUp Required Singl CheckBox InBound Call
    $('.InCallServiceBook').click(function () {
        if ($(this).is(':checked')) {
            $("#InCallserviceBookDiv").show();
            $("#LeadSourceHideIn").hide();
            $("#SMRInteractionFirst").hide();


        } else {
            $("#InCallserviceBookDiv").hide();
            $("#LeadSourceHideIn").show();
            $("#SMRInteractionFirst").show();
        }
    });

    $("input[name='srdisposition.typeOfDisposition']").click(function () {
        test = $(this).val();
        if (test == "Contact") {
            $('#BtnNoSubmit').hide();
            $('#NoComments').hide();
            $('#alreadyserviceDIV').hide();
            $("#NotSpeachDiv").hide();


            $("#WhatdidtheCustomersayDIV").show();
            $('#whatDidCustSayDiv').show();
            $('#NextToModdelDiv').show();





            validateCheck();
            //alreadySericedCheck();
        }
        if (test == "NonContact") {
            validateCheck();
            //alreadySericedCheck();
            $("#DidYouTalkDiv").hide();
            $("#SMRInteractionFirst").hide();

            $("#NotSpeachDiv").show();
            $("#WhatdidtheCustomersayDIV").hide();
            $('#whatDidCustSayDiv').hide();
            $("#alreadyserviceDIV").hide();
            $('#callMeLattteDiv').hide();
            $('#serviceBookDiv').hide();
            $('#alreadyservicedDiv1').hide();
            $('#MiddelDiv').hide();
            $('#finalDiv').hide();

            $('#BtnNoSubmit').show();
            $('#NoComments').show();
            $('#NextToModdelDiv').hide();
            $('#CustomerDriveInDiv').hide();
            $("#InsuOthersDiv").hide();// Other DIV hide
            $("#confirmInsuComments").hide(); // call me later Div Hide






        }
    });


    //Randam Access CRE List

    $("input[name$='Random']").click(function () {
        var mRandam = $(this).val();
        if (mRandam == "ChoseCRE List") {
            $("#CREListSelect").show();

        }
        else {
            $("#CREListSelect").hide();
        }
    });


    //20th may changes 2018

    function validateCheck() {
        if (varWhatdidSay == "Paid") {

            //console.log(varWhatdidSay);
            $("#confirmInsuComments").show();
            $("#serviceBookDiv").hide();
            $("#callMeLattteDiv").hide();
            $("#alreadyserviceDIV").hide();
            $("#SMRInteractionFirst").hide();
            $("#DidYouTalkDiv").hide();
            $("#CancelServiceBk").hide();
            $("#CancelInsuAppo").hide();
            $("#InsuOthersDiv").hide();
            clearAllControlls();
        }
        if (varWhatdidSay == "INS Others" || varWhatdidSay == "Other") {


            $("#policyDropConfAdd").hide();
            $("#InsuOthersDiv").show();
            $("#confirmInsuComments").hide();
            $("#serviceBookDiv").hide();
            $("#callMeLattteDiv").hide();
            $("#alreadyserviceDIV").hide();
            $("#DidYouTalkDiv").hide();
            $("#CancelServiceBk").hide();
            $("#CancelInsuAppo").hide();

            $("input[name='srdisposition.othersSMR']").prop("checked", false);
            $('#escaltionDropService').hide();
            $('#supCREList').val('');
            clearAllControlls();

        }
        if (varWhatdidSay != "") {
            if (varWhatdidSay == "Book My Service" || varWhatdidSay == "Rescheduled") {
                $("#serviceBookDiv").show();
                $("#callMeLattteDiv").hide();
                $("#alreadyserviceDIV").hide();
                $("#SMRInteractionFirst").hide();
                $("#DidYouTalkDiv").hide();
                $("#ConfirmedSubmit").hide();
                $("#CancelServiceBk").hide();
                $("#confirmInsuComments").hide();
                $("#CancelInsuAppo").hide();
                $("#InsuOthersDiv").hide();
                $("#SendSMSDiv").show();


            }
            if (varWhatdidSay == "Book Appointment") {
                $("#serviceBookDiv").show();
                $("#callMeLattteDiv").hide();
                $("#alreadyserviceDIV").hide();
                $("#SMRInteractionFirst").hide();
                $("#DidYouTalkDiv").hide();
                $("#ConfirmedSubmit").hide();
                $("#CancelServiceBk").hide();
                $("#confirmInsuComments").hide();
                $("#CancelInsuAppo").hide();
                $("#InsuOthersDiv").hide();
                clearAllControlls();

            }
            if (varWhatdidSay == "Call Me Later") {
                //$("#callMeLattteDiv").show();
                $('#callMeLattteDiv').show();


                $("#serviceBookDiv").hide();
                $("#alreadyserviceDIV").hide();
                $("#alreadyservicedDiv1").hide();
                $("#SMRInteractionFirst").hide();
                $("#DidYouTalkDiv").hide();
                $("#ConfirmedSubmit").hide();
                $("#CancelServiceBk").hide();
                $("#confirmInsuComments").hide();
                $("#CancelInsuAppo").hide();
                $("#InsuOthersDiv").hide();
                clearAllControlls();
            }
            if (varWhatdidSay == "Service Not Required") {
                //console.log(varWhatdidSay);
                $("#serviceBookDiv").hide();
                $("#callMeLattteDiv").hide();
                $("#alreadyserviceDIV").show();
                $("#SMRInteractionFirst").hide();
                $("#DidYouTalkDiv").hide();
                $("#ConfirmedSubmit").hide();
                $("#CancelServiceBk").hide();
                $("#confirmInsuComments").hide();
                $("#CancelInsuAppo").hide();
                $("#InsuOthersDiv").hide();
            }

            if (varWhatdidSay == "Renewal Not Required") {
                //console.log(varWhatdidSay);
                $("#serviceBookDiv").hide();
                $("#callMeLattteDiv").hide();
                $("#alreadyserviceDIV").show();
                $("#SMRInteractionFirst").hide();
                $("#DidYouTalkDiv").hide();
                $("#ConfirmedSubmit").hide();
                $("#CancelServiceBk").hide();
                $("#confirmInsuComments").hide();
                $("#CancelInsuAppo").hide();
                $("#InsuOthersDiv").hide();
                clearAllControlls();

            }

            if (varWhatdidSay == "Confirmed") {
                //console.log(varWhatdidSay);
                $("#ConfirmedSubmit").show();
                $("#serviceBookDiv").hide();
                $("#callMeLattteDiv").hide();
                $("#alreadyserviceDIV").hide();
                $("#SMRInteractionFirst").hide();
                $("#DidYouTalkDiv").hide();
                $("#CancelServiceBk").hide();
                $("#confirmInsuComments").hide();
                $("#CancelInsuAppo").hide();
                $("#InsuOthersDiv").hide();

            }
            if (varWhatdidSay == "ConfirmedInsu") {

                //console.log(varWhatdidSay);
                $("#confirmInsuComments").show();
                $("#serviceBookDiv").hide();
                $("#callMeLattteDiv").hide();
                $("#alreadyserviceDIV").hide();
                $("#SMRInteractionFirst").hide();
                $("#DidYouTalkDiv").hide();
                $("#CancelServiceBk").hide();
                $("#CancelInsuAppo").hide();
                clearAllControlls();

            }
            if (varWhatdidSay == "Cancel Appointment") {

                $("#CancelInsuAppo").show();
                $("#confirmInsuComments").hide();
                $("#serviceBookDiv").hide();
                $("#callMeLattteDiv").hide();
                $("#alreadyserviceDIV").hide();
                $("#SMRInteractionFirst").hide();
                $("#DidYouTalkDiv").hide();
                $("#CancelServiceBk").hide();

            }
            if (varWhatdidSay == "Cancelled") {
                console.log(varWhatdidSay);
                $("#CancelServiceBk").show();
                $("#ConfirmedSubmit").hide();
                $("#serviceBookDiv").hide();
                $("#callMeLattteDiv").hide();
                $("#alreadyserviceDIV").hide();
                $("#SMRInteractionFirst").show();
                $("#DidYouTalkDiv").hide();
                $("#InsuOthersDiv").hide();
                //$("#CancelServiceBk").show();
                $("#confirmInsuComments").hide();
                $("#CancelInsuAppo").hide();
                $('input[name="listingForm.ServiceBookingCancel"]').prop('checked', false);

                $('input[name="noServiceReason"]').each(function () {
                    var curCkValu = $(this).prop("checked");
                    if (curCkValu == true) {
                        $(this).prop('checked', false);
                        $("input:radio.SNRClear").each(function () {
                            var rdValue = $(this).prop("checked");
                            if (rdValue === true) {
                                $(this).prop("checked", false);
                                $(this).trigger("click");
                                $(this).prop("checked", false);
                            }
                        });

                        $(this).prop("checked", false);
                        $(this).trigger("click");
                        $(this).prop("checked", false);

                        $("input:text.SNRClear").val('');

                        //$("input:radio.SNRClear").removeAttr("checked").trigger("click");
                    }
                });

                //$.confirm({
                //    title: 'Confirm!',
                //    closeIcon: true,
                //    content: 'VEHICLE will be blocked For next 24 HOURS, Do you really want to cancel the Booking?',
                //    buttons: {
                //        Yes: function () {
                //            $("#CancelServiceBk").show();
                //            $("#ConfirmedSubmit").hide();
                //            $("#serviceBookDiv").hide();
                //            $("#callMeLattteDiv").hide();
                //            $("#alreadyserviceDIV").hide();
                //            $("#SMRInteractionFirst").hide();
                //            $("#DidYouTalkDiv").hide();
                //            //$("#CancelServiceBk").show();
                //            $("#confirmInsuComments").hide();
                //            $("#CancelInsuAppo").hide();
                //            $('input[name="listingForm.ServiceBookingCancel"]').prop('checked', false);

                //            $('input[name="noServiceReason"]').each(function () {
                //                var curCkValu = $(this).prop("checked");
                //                if (curCkValu == true) {
                //                    $(this).prop('checked', false);
                //                    $("input:radio.SNRClear").each(function () {
                //                        var rdValue = $(this).prop("checked");
                //                        if (rdValue === true) {
                //                            $(this).prop("checked", false);
                //                            $(this).trigger("click");
                //                            $(this).prop("checked", false);
                //                        }
                //                    });

                //                    $(this).prop("checked", false);
                //                    $(this).trigger("click");
                //                    $(this).prop("checked", false);

                //                    $("input:text.SNRClear").val('');

                //                    //$("input:radio.SNRClear").removeAttr("checked").trigger("click");
                //                }
                //            });


                //            //$('input[name="noServiceReason"]').prop('checked', false);
                //            //$('input[name="noServiceReason"]').trigger("click");
                //            //$('#noserviceForCheckBox').val('');
                //        },
                //        No: function () {
                //            $('input[name="listingForm.ServiceBookingCancel"]').prop('checked', false);
                //            $('input[name="listingForm.dispoCustAns"]').prop('checked', false);
                //            //$("#SMRInteractionFirst").hide();
                //            $("#CancelServiceBk").hide();

                //            //$('input[name="noServiceReason"]').trigger("click");

                //            $('input[name="noServiceReason"]').each(function () {
                //                var curCkValu = $(this).prop("checked");
                //                if (curCkValu == true) {
                //                    $(this).prop('checked', false);
                //                    $("input:radio.SNRClear").each(function () {
                //                        var rdValue = $(this).prop("checked");
                //                        if (rdValue === true) {
                //                            $(this).prop("checked", false);
                //                            $(this).trigger("click");
                //                            $(this).prop("checked", false);
                //                        }
                //                    });

                //                    $("input:text.SNRClear").val('');

                //                    //$("input:radio.SNRClear").removeAttr("checked").trigger("click");
                //                }
                //            });

                //            //$('#noserviceForCheckBox').val('');
                //        }
                //    }
                //});



            }

            if (varWhatdidSay == "Paid") {

                //console.log(varWhatdidSay);
                $("#confirmInsuComments").show();
                $("#serviceBookDiv").hide();
                $("#callMeLattteDiv").hide();
                $("#alreadyserviceDIV").hide();
                $("#SMRInteractionFirst").hide();
                $("#DidYouTalkDiv").hide();
                $("#CancelServiceBk").hide();
                $("#CancelInsuAppo").hide();

            }
        }

    }




    $("input[name$='listingForm.ServiceBookingCancel']").click(function () {
        var varBSCancel = $(this).val();

        if (varBSCancel == "Service Booking") {

            $.confirm({
                title: 'Confirm!',
                closeIcon: true,
                content: 'VEHICLE will be blocked For next 24 HOURS, Do you really want to cancel the Booking?',
                buttons: {
                    Yes: function () {
                        //$("#CancelServiceBk").show();
                        //$("#ConfirmedSubmit").hide();
                        //$("#serviceBookDiv").hide();
                        //$("#callMeLattteDiv").hide();
                        //$("#alreadyserviceDIV").hide();
                        //$("#SMRInteractionFirst").hide();
                        //$("#DidYouTalkDiv").hide();
                        ////$("#CancelServiceBk").show();
                        //$("#confirmInsuComments").hide();
                        //$("#CancelInsuAppo").hide();
                        //$('input[name="listingForm.ServiceBookingCancel"]').prop('checked', false);
                        cancelBooking();
                        $('input[name="noServiceReason"]').each(function () {
                            var curCkValu = $(this).prop("checked");
                            if (curCkValu == true) {
                                $(this).prop('checked', false);
                                $("input:radio.SNRClear").each(function () {
                                    var rdValue = $(this).prop("checked");
                                    if (rdValue === true) {
                                        $(this).prop("checked", false);
                                        $(this).trigger("click");
                                        $(this).prop("checked", false);
                                    }
                                });

                                $(this).prop("checked", false);
                                $(this).trigger("click");
                                $(this).prop("checked", false);

                                $("input:text.SNRClear").val('');

                                //$("input:radio.SNRClear").removeAttr("checked").trigger("click");
                            }
                        });


                        //$('input[name="noServiceReason"]').prop('checked', false);
                        //$('input[name="noServiceReason"]').trigger("click");
                        //$('#noserviceForCheckBox').val('');
                    },
                    No: function () {
                        $('input[name="listingForm.ServiceBookingCancel"]').prop('checked', false);
                        $('input[name="listingForm.dispoCustAns"]').prop('checked', false);
                        //$("#SMRInteractionFirst").hide();
                        $("#CancelServiceBk").hide();

                        //$('input[name="noServiceReason"]').trigger("click");
                        $(this).prop('checked', false);
                        $('input[name="noServiceReason"]').each(function () {
                            var curCkValu = $(this).prop("checked");
                            if (curCkValu == true) {
                                $(this).prop('checked', false);
                                $("input:radio.SNRClear").each(function () {
                                    var rdValue = $(this).prop("checked");
                                    if (rdValue === true) {
                                        $(this).prop("checked", false);
                                        $(this).trigger("click");
                                        $(this).prop("checked", false);
                                    }
                                });

                                $("input:text.SNRClear").val('');

                                //$("input:radio.SNRClear").removeAttr("checked").trigger("click");
                            }
                        });

                        //$('#noserviceForCheckBox').val('');
                    }
                }
            });

        }
        if (varBSCancel == "Pickup Drop") {
            cancelPickup();
            //$.confirm({
            //    title: 'Confirm!',
            //    closeIcon: true,
            //    content: 'VEHICLE will be blocked For next 24 HOURS, Do you really want to cancel the Booking?',
            //    buttons: {
            //        Yes: function () {
            //            //$("#CancelServiceBk").show();
            //            //$("#ConfirmedSubmit").hide();
            //            //$("#serviceBookDiv").hide();
            //            //$("#callMeLattteDiv").hide();
            //            //$("#alreadyserviceDIV").hide();
            //            //$("#SMRInteractionFirst").hide();
            //            //$("#DidYouTalkDiv").hide();
            //            ////$("#CancelServiceBk").show();
            //            //$("#confirmInsuComments").hide();
            //            //$("#CancelInsuAppo").hide();
            //            //$('input[name="listingForm.ServiceBookingCancel"]').prop('checked', false);

            //            $('input[name="noServiceReason"]').each(function () {
            //                var curCkValu = $(this).prop("checked");
            //                if (curCkValu == true) {
            //                    $(this).prop('checked', false);
            //                    $("input:radio.SNRClear").each(function () {
            //                        var rdValue = $(this).prop("checked");
            //                        if (rdValue === true) {
            //                            $(this).prop("checked", false);
            //                            $(this).trigger("click");
            //                            $(this).prop("checked", false);
            //                        }
            //                    });

            //                    $(this).prop("checked", false);
            //                    $(this).trigger("click");
            //                    $(this).prop("checked", false);

            //                    $("input:text.SNRClear").val('');

            //                    //$("input:radio.SNRClear").removeAttr("checked").trigger("click");
            //                }
            //            });


            //            //$('input[name="noServiceReason"]').prop('checked', false);
            //            //$('input[name="noServiceReason"]').trigger("click");
            //            //$('#noserviceForCheckBox').val('');
            //        },
            //        No: function () {
            //            $('input[name="listingForm.ServiceBookingCancel"]').prop('checked', false);
            //            $('input[name="listingForm.dispoCustAns"]').prop('checked', false);
            //            //$("#SMRInteractionFirst").hide();
            //            $("#CancelServiceBk").hide();
            //            $(this).prop('checked', false);
            //            //$('input[name="noServiceReason"]').trigger("click");

            //            $('input[name="noServiceReason"]').each(function () {
            //                var curCkValu = $(this).prop("checked");
            //                if (curCkValu == true) {
            //                    $(this).prop('checked', false);
            //                    $("input:radio.SNRClear").each(function () {
            //                        var rdValue = $(this).prop("checked");
            //                        if (rdValue === true) {
            //                            $(this).prop("checked", false);
            //                            $(this).trigger("click");
            //                            $(this).prop("checked", false);
            //                        }
            //                    });

            //                    $("input:text.SNRClear").val('');

            //                    //$("input:radio.SNRClear").removeAttr("checked").trigger("click");
            //                }
            //            });

            //            //$('#noserviceForCheckBox').val('');
            //        }
            //    }
            //});

        }
    });


    $("input[name='servicebooked.typeOfPickup']").click(function () {
        var varPickupaddress = $(this).val();

        if (varPickupaddress == "true") {
            //$("#pickupDiv").show();
            $("#MSSSelectDiv").show();
            $("#ShowroomListDiv").hide();
            $("#AssignBtnBkreview").show();
            $("#driver_scheduler_div").hide();
            $("#dropAddress_div").hide();
            $("#pickUpAddress").text("Select Address");

        }
        else if (varPickupaddress == "Door Step Service") {
            //$("#pickupDiv").show();
            $("#MSSSelectDiv").show();
            $("#ShowroomListDiv").hide();
            $("#AssignBtnBkreview").show();
            $("#driver_scheduler_div").hide();
            $("#dropAddress_div").hide();
            $("#pickUpAddress").text("Select Address");
            $("#hdDriverId").val('0');
            $('#hdPickUpTime').val('');
            $('#hdPickUpTimeRange').val('');
            $("#tblDriverAllocation thead").remove();
            $("#tblDriverAllocation tbody").remove();
            $("#pickUpAddressDiv").show();

        }
        else if (varPickupaddress == "Mobile Service Support") {

            $("#pickupDiv").hide();
            $("#MSSSelectDiv").show();
            $("#ShowroomListDiv").hide();
            $("#AssignBtnBkreview").show();
            $("#driver_scheduler_div").hide();
            $("#dropAddress_div").hide();
            $("#pickUpAddress").text("Select Address");
            $("#hdDriverId").val('0');
            $('#hdPickUpTime').val('');
            $('#hdPickUpTimeRange').val('');
            $("#tblDriverAllocation thead").remove();
            $("#tblDriverAllocation tbody").remove();
            $("#pickUpAddressDiv").show();


        }
        else if (varPickupaddress == "Customer Drive-In") {

            $("#pickupDiv").hide();
            $("#MSSSelectDiv").hide();
            $("#AssignBtnBkreview").hide();
            $("#driver_scheduler_div").hide();
            $("#dropAddress_div").hide();
            $("#pickUpAddress").text("Select Address");
            $("#hdDriverId").val('0');
            $('#hdPickUpTime').val('');
            $('#hdPickUpTimeRange').val('');
            $("#tblDriverAllocation thead").remove();
            $("#tblDriverAllocation tbody").remove();
        }
        else if (varPickupaddress == "QWIK Service") {

            $("#pickupDiv").hide();
            $("#MSSSelectDiv").hide();
            $("#AssignBtnBkreview").hide();
            $("#driver_scheduler_div").hide();
            $("#dropAddress_div").hide();
            $("#pickUpAddress").text("Select Address");
            $("#hdDriverId").val('0');
            $('#hdPickUpTime').val('');
            $('#hdPickUpTimeRange').val('');
            $("#tblDriverAllocation").empty();
        }

        else if (varPickupaddress == "Home Visit") {
            $("#pickupDivInsurance").show();
            $("#MSSSelectDiv").hide();
            $("#ShowroomListDiv").hide();
            $("#AssignBtnBkreview").hide();
            $("#driver_scheduler_div").hide();
            $("#dropAddress_div").hide();
            $("#pickUpAddress").text("Select Address");
            $("#hdDriverId").val('0');
            $('#hdPickUpTime').val('');
            $('#hdPickUpTimeRange').val('');
            $("#tblDriverAllocation thead").remove();
            $("#tblDriverAllocation tbody").remove();
        }
        else if (varPickupaddress == "Showroom Visit") {

            $("#pickupDivInsurance").hide();
            $("#MSSSelectDiv").show();
            $("#ShowroomListDiv").show();
            $("#AssignBtnBkreview").hide();
            $("#driver_scheduler_div").hide();
            $("#dropAddress_div").hide();
            $("#pickUpAddress").text("Select Address");
            $("#hdDriverId").val('0');
            $('#hdPickUpTime').val('');
            $('#hdPickUpTimeRange').val('');
            $("#tblDriverAllocation thead").remove();
            $("#tblDriverAllocation tbody").remove();

            $("#DrivdropCal_div").hide();
        }
        else if (varPickupaddress == "Road Side Assistance") {
            //$("#pickupDiv").show();
            $("#MSSSelectDiv").show();
            $("#ShowroomListDiv").hide();
            $("#AssignBtnBkreview").show();
            $("#driver_scheduler_div").hide();
            $("#dropAddress_div").hide();
            $("#pickUpAddress").text("Select Address");
            $("#hdDriverId").val('0');
            $('#hdPickUpTime').val('');
            $('#hdPickUpTimeRange').val('');
            $("#tblDriverAllocation thead").remove();
            $("#tblDriverAllocation tbody").remove();
            $("#pickUpAddressDiv").show();

        }
        else if (varPickupaddress == "Pickup Drop Required") {
            $("#MSSSelectDiv").show();
            $("#pickUpAddressDiv").show();
            $("#ShowroomListDiv").hide();
            $("#AssignBtnBkreview").show();
            $("#driver_scheduler_div").show();
            $("#dropAddress_div").show();
            $("#pickUpAddress").text("Select PickUp Address");

            $("#DrivdropCal_div").hide();
            $('#pickuptypedrivercre').empty().append('<option selected="selected" value="Pickup Drop Required">Pickup Drop Required</option>');

        } else if (varPickupaddress == "Pickup & MOS") {
            $("#MSSSelectDiv").show();
            $("#ShowroomListDiv").hide();
            $("#AssignBtnBkreview").show();
            $("#driver_scheduler_div").hide();
            $("#dropAddress_div").hide();
            $("#pickUpAddress").text("Select Address");
            $("#hdDriverId").val('0');
            $('#hdPickUpTime').val('');
            $('#hdPickUpTimeRange').val('');
            $("#tblDriverAllocation thead").remove();
            $("#tblDriverAllocation tbody").remove();
            $("#pickUpAddressDiv").show();

        }
        else if (varPickupaddress == "Pickup Only") {
            $("#MSSSelectDiv").show();
            $("#pickUpAddressDiv").show();
            $("#ShowroomListDiv").hide();
            $("#AssignBtnBkreview").show();
            $("#driver_scheduler_div").show();
            $("#dropAddress_div").hide();
            $("#pickUpAddress").text("Select Pickup Address");
            $("#DrivdropCal_div").hide();
            $('#pickuptypedrivercre').empty().append('<option selected="selected" value="Pickup Only">Pickup Only</option>');
        }
        else if (varPickupaddress == "Drop Only") {
            $("#MSSSelectDiv").show();
            $("#pickUpAddressDiv").hide();
            $("#ShowroomListDiv").hide();
            $("#AssignBtnBkreview").show();
            $("#driver_scheduler_div").show();
            $("#dropAddress_div").show();
            $('#pickuptypedrivercre').empty().append('<option selected="selected" value="Drop Only">Drop Only</option>');
            $("#DrivdropCal_div").hide();
        }
        else {
            $("#pickupDivInsurance").hide();
            $("#driver_scheduler_div").hide();
            $("#MSSSelectDiv").hide();
            $("#ShowroomListDiv").hide();
            $("#AssignBtnBkreview").hide();
            $("#hdDriverId").val('0');
            $('#hdPickUpTime').val('');
            $('#hdPickUpTimeRange').val('');
            $("#tblDriverAllocation thead").remove();
            $("#tblDriverAllocation tbody").remove();
        }
    });



    //Inbound ServiceBook
    $("input[name$='typeOfPickupIn']").click(function () {


        varPickupaddressIn = $(this).val();


        if (varPickupaddressIn == "true") {

            $("#pickupDivIn").hide();
            $("#MSSSelectDivIn").show();
            $("#AssignBtnBkreviewIn").show();

        }
        else if (varPickupaddressIn == "QWIK Service") {

            $("#pickupDivIn").hide();
            $("#MSSSelectDivIn").hide();
            $("#AssignBtnBkreviewIn").hide();

        }
        else if (varPickupaddressIn == "Door Step Service") {

            $("#pickupDivIn").hide();
            $("#MSSSelectDivIn").show();
            $("#AssignBtnBkreviewIn").show();

        }
        else if (varPickupaddressIn == "Road Side Assistance") {

            $("#pickupDivIn").hide();
            $("#MSSSelectDivIn").show();
            $("#AssignBtnBkreviewIn").show();

        }

        else if (varPickupaddressIn == "Maruthi Mobile SupportIn") {


            $("#pickupDivIn").hide();
            $("#MSSSelectDivIn").show();
            $("#AssignBtnBkreviewIn").show();
        }
        else if (varPickupaddressIn == "Customer Drive-In") {


            $("#pickupDivIn").hide();
            $("#MSSSelectDivIn").hide();
            $("#AssignBtnBkreviewIn").hide();

        } else if (varPickupaddressIn == "Home Visit") {


            $("#pickupDivIn").show();
            $("#MSSSelectDivIn").hide();
            $("#AssignBtnBkreviewIn").hide();


        } else if (varPickupaddressIn == "Showroom Visit") {



            $("#pickupDivIn").hide();
            $("#MSSSelectDivIn").show();
            $("#AssignBtnBkreviewIn").hide();
        }
        else {


            $("#pickupDivIn").hide();
            $("#MSSSelectDivIn").hide();
            $("#AssignBtnBkreviewIn").hide();


        }
    });


    //PickUp Required Singl CheckBox
    $('.AddnewAddressClass').click(function () {
        if ($(this).is(':checked')) {
            $("#AddAddressDiv").show();

        } else {
            $("#AddAddressDiv").hide();
        }
    });


    //Heena changes 11-04-2018

    $('#AlreadyServiced').click(function () {
        if ($(this).prop('checked')) {
            $(this).parent().addClass("redBackground");
            $("#alreadyservicedDiv1").show();
            $("#txtDissatisfiedwithpreviousservice").hide();
            $('.DissatisifactionwithClaimId').hide();
            $("#DistancefromDealerLocationDIV").hide();
            $("#DissatisfactionwithInsuranceREmarksDiv").hide();
            $("#DissatisfactionwithSalesREmarksDiv").hide();
            $("#OtherSeriveRemarks").hide();
            $("#kmsCoverdTextDiv").hide();
            $(".VehicleSold").hide();
            $(".Dissatisfiedwithpreviousservice").hide();
            $(".Distancefrom").hide();
            $(".DissatisfiedwithSalesID").hide();
            $(".DissatisfiedwithInsuranceId").hide();
            $(".ExcBillingId").hide();
            $(".Stolen").hide();
            $(".Totalloss").hide();
            $(".OtherLast").hide();
            $(".KmsNotCov").hide();
            $("#SubmitBtnStolen").hide();
            $(".dissatisfiedWithClaims").hide();
            $(".callDisconnected").hide();
            $(".vehicleAtWorkshop").hide();
            $(".cngVehicle").hide();
            $(".outOfStation").hide();
            $(".wantMoreDiscount").hide();
            $("#renewalId").show();
        }
        else {
            $("#alreadyservicedDiv1").hide();
            $(".VehicleSold").show();
            $(".Dissatisfiedwithpreviousservice").show();
            $(".Distancefrom").show();
            $(".DissatisfiedwithSalesID").show();
            $(".DissatisfiedwithInsuranceId").show();
            $(".ExcBillingId").show();
            $(".Stolen").show();
            $('.DissatisifactionwithClaimId').show();
            $(".Totalloss").show();
            $(".OtherLast").show();
            $(".KmsNotCov").show();
            $(".dissatisfiedWithClaims").show();
            $(".callDisconnected").show();
            $(".vehicleAtWorkshop").show();
            $(".cngVehicle").show();
            $(".outOfStation").show();
            $(".wantMoreDiscount").show();
            $("#AlreadySerivePopup").hide();
            $("#renewalId").hide();


            $('input[name="listingForm.VehicleSoldYes"]').attr('checked', false);

        }
    });


    $('#VehicleSold').click(function () {

        if ($(this).prop('checked')) {
            $("#VehicelSoldYesRNo").show();
            $("#alreadyservicedDiv1").hide
            $('.DissatisifactionwithClaimId').hide();
            $("#txtDissatisfiedwithpreviousservice").hide();
            $("#DistancefromDealerLocationDIV").hide();
            $("#DissatisfactionwithInsuranceREmarksDiv").hide();
            $("#DissatisfactionwithSalesREmarksDiv").hide();
            $("#OtherSeriveRemarks").hide();
            $("#kmsCoverdTextDiv").hide();
            $(".Dissatisfiedwithpreviousservice").hide();
            $(".Distancefrom").hide();
            $(".DissatisfiedwithSalesID").hide();
            $(".DissatisfiedwithInsuranceId").hide();
            $(".ExcBillingId").hide();
            $(".Stolen").hide();
            $(".Totalloss").hide();
            $(".OtherLast").hide();
            $(".AlreadyServiced").hide();
            $(".KmsNotCov").hide();
            $("#ServicedMyDealerDiv").hide();
            $("#ServicedAtOtherDealerDiv").hide();
            $('input[name=renewalDoneBy]').attr('checked', false);
            $('input[name="ir_disposition.typeOfAutherization"]').attr('checked', false);
            $('input[name="insudisposition.typeOfAutherization"]').attr('checked', false);
            $("#SubmitBtnStolen").hide();
            $(".dissatisfiedWithClaims").hide();
            $(".callDisconnected").hide();
            $(".vehicleAtWorkshop").hide();
            $(".cngVehicle").hide();
            $(".outOfStation").hide();
            $(".wantMoreDiscount").hide();
            $("#renewalId").show();
        }
        else {
            $("#VehicelSoldYesRNo").hide();
            $("#alreadyservicedDiv1").hide();
            $(".Dissatisfiedwithpreviousservice").show();
            $(".Distancefrom").show();
            $(".DissatisfiedwithSalesID").show();
            $(".DissatisfiedwithInsuranceId").show();
            $(".ExcBillingId").show();
            $(".Stolen").show();
            $(".Totalloss").show();
            $(".OtherLast").show();
            $(".AlreadyServiced").show();
            $(".KmsNotCov").show();
            $('.DissatisifactionwithClaimId').show();
            $("#VehicleSoldClickYes").hide();
            $("#PurchaseClickYes").hide();
            $("#VehicleSoldClickNo").hide();
            $(".dissatisfiedWithClaims").show();
            $(".callDisconnected").show();
            $(".vehicleAtWorkshop").show();
            $(".cngVehicle").show();
            $(".outOfStation").show();
            $(".wantMoreDiscount").show();
            $("#renewalId").hide();

        }
    });
    $('#Dissatisfiedwithpreviousservice').click(function () {
        if ($(this).prop('checked')) {
            $("#txtDissatisfiedwithpreviousservice").show();
            $("#alreadyservicedDiv1").hide();
            $('.DissatisifactionwithClaimId').hide();
            $("#DistancefromDealerLocationDIV").hide();
            $("#DissatisfactionwithInsuranceREmarksDiv").hide();
            $("#DissatisfactionwithSalesREmarksDiv").hide();
            $("#OtherSeriveRemarks").hide();
            $("#kmsCoverdTextDiv").hide();
            $(".Distancefrom").hide();
            $(".DissatisfiedwithSalesID").hide();
            $(".DissatisfiedwithInsuranceId").hide();
            $(".ExcBillingId").hide();
            $(".Stolen").hide();
            $(".Totalloss").hide();
            $(".OtherLast").hide();
            $(".AlreadyServiced").hide();
            $(".VehicleSold").hide();
            $(".KmsNotCov").hide();
            $("#DisatisfiedPreQuestion").hide();
            $("#DisatisfiedPreQuestion").hide();
            $("#SubmitBtnStolen").hide();
            $(".dissatisfiedWithClaims").hide();
            $(".callDisconnected").hide();

            $(".vehicleAtWorkshop").hide();
            $(".cngVehicle").hide();
            $(".outOfStation").hide();
            $(".wantMoreDiscount").hide();
            $("#renewalId").show();

        }
        else {
            $("#txtDissatisfiedwithpreviousservice").hide();
            $(".Distancefrom").show();
            $(".DissatisfiedwithSalesID").show();
            $(".DissatisfiedwithInsuranceId").show();
            $(".ExcBillingId").show();
            $(".Stolen").show();
            $(".Totalloss").show();
            $(".OtherLast").show();
            $(".AlreadyServiced").show();
            $(".VehicleSold").show();
            $(".KmsNotCov").show();
            $("#DisatisfiedPreQuestion").hide();
            $(".DissatisifactionwithClaimId").show();
            $(".callDisconnected").show();
            $(".vehicleAtWorkshop").show();
            $(".cngVehicle").show();
            $('#dissatisfiedWithClaims').show();
            $(".outOfStation").show();
            $(".wantMoreDiscount").show();
            $("#renewalId").hide();
        }
    });
    $('#Distancefrom').click(function () {
        if ($(this).prop('checked')) {
            $("#DistancefromDealerLocationDIV").show();
            $("#alreadyservicedDiv1").hide();
            $("#DissatisfactionwithInsuranceREmarksDiv").hide();
            $("#txtDissatisfiedwithpreviousservice").hide();
            $("#DissatisfactionwithSalesREmarksDiv").hide();
            $("#OtherSeriveRemarks").hide();
            $('.DissatisifactionwithClaimId').hide();
            $("#kmsCoverdTextDiv").hide();
            $(".Dissatisfiedwithpreviousservice").hide();
            $(".DissatisfiedwithSalesID").hide();
            $(".DissatisfiedwithInsuranceId").hide();
            $(".ExcBillingId").hide();
            $(".Stolen").hide();
            $(".Totalloss").hide();
            $(".OtherLast").hide();
            $(".AlreadyServiced").hide();
            $(".VehicleSold").hide();
            $(".KmsNotCov").hide();
            $("#DistanceFoRRQuestion").hide();
            $("#SubmitBtnStolen").hide();
            $(".dissatisfiedWithClaims").hide();
            $(".callDisconnected").hide();
            $(".vehicleAtWorkshop").hide();
            $(".cngVehicle").hide();
            $(".outOfStation").hide();
            $(".wantMoreDiscount").hide();
            $("#renewalId").show();
        } else {
            $("#DistancefromDealerLocationDIV").hide();
            $(".Dissatisfiedwithpreviousservice").show();
            $(".DissatisfiedwithSalesID").show();
            $(".DissatisfiedwithInsuranceId").show();
            $(".ExcBillingId").show();
            $(".Stolen").show();
            $(".Totalloss").show();
            $('.DissatisifactionwithClaimId').show();
            $(".OtherLast").show();
            $(".AlreadyServiced").show();
            $(".VehicleSold").show();
            $(".KmsNotCov").show();
            $("#DistanceFoRRQuestion").hide();
            $(".dissatisfiedWithClaims").show();
            $(".callDisconnected").show();
            $(".vehicleAtWorkshop").show();
            $(".cngVehicle").show();
            $(".outOfStation").show();
            $(".wantMoreDiscount").show();
            $("#renewalId").hide();
        }

    });

    $('#DissatisfiedwithSalesID').click(function () {
        if ($(this).prop('checked')) {
            $("#DissatisfactionwithSalesREmarksDiv").show();
            $("#alreadyservicedDiv1").hide();
            $("#DistancefromDealerLocationDIV").hide();
            $("#DissatisfactionwithInsuranceREmarksDiv").hide();
            $("#txtDissatisfiedwithpreviousservice").hide();
            $('.DissatisifactionwithClaimId').hide();
            $("#OtherSeriveRemarks").hide();
            $("#kmsCoverdTextDiv").hide();
            $(".Dissatisfiedwithpreviousservice").hide();
            $(".Distancefrom").hide();
            $(".DissatisfiedwithInsuranceId").hide();
            $(".ExcBillingId").hide();
            $(".Stolen").hide();
            $(".Totalloss").hide();
            $(".OtherLast").hide();
            $(".AlreadyServiced").hide();
            $(".VehicleSold").hide();
            $(".KmsNotCov").hide();
            $("#SubmitBtnStolen").hide();
            $(".dissatisfiedWithClaims").hide();
            $(".callDisconnected").hide();
            $(".vehicleAtWorkshop").hide();
            $(".cngVehicle").hide();
            $(".outOfStation").hide();
            $(".wantMoreDiscount").hide();
            $("#renewalId").show();
        } else {
            $("#DissatisfactionwithSalesREmarksDiv").hide();
            $(".Dissatisfiedwithpreviousservice").show();
            $(".Distancefrom").show();
            $(".DissatisfiedwithInsuranceId").show();
            $(".ExcBillingId").show();
            $(".Stolen").show();
            $(".Totalloss").show();
            $(".OtherLast").show();
            $('.DissatisifactionwithClaimId').show();
            $(".AlreadyServiced").show();
            $(".VehicleSold").show();
            $(".KmsNotCov").show();
            $("#DisstisFiedSaleRQuestion").hide();
            $(".dissatisfiedWithClaims").show();
            $(".callDisconnected").show();
            $(".vehicleAtWorkshop").show();
            $(".cngVehicle").show();
            $(".outOfStation").show();
            $(".wantMoreDiscount").show();
            $("#renewalId").hide();
        }
    });
    $('#dissatisfiedWithClaims').click(function () {
        if ($(this).prop('checked')) {
            $('.DissatisifactionwithClaimId').show();
            $('#DissatisfactionwithclaimsREmarksDiv').show();
            $('.AlreadyServiced').hide();
            $('#alreadyservicedDiv1').hide();
            $('.VehicleSold').hide();
            $('#VehicelSoldYesRNo').hide();
            $('.Dissatisfiedwithpreviousservice').hide();
            $('.Distancefrom').hide();
            $('.DissatisfiedwithSalesID').hide();
            $('.DissatisfiedwithInsuranceId').hide();
            
            $('.ExcBillingId').hide();
            $('.Stolen').hide();
            $('.Totalloss').hide();
            $('.OtherLast').hide();
            $('.Totalloss').hide();
            $('.Totalloss').hide();
            $("#renewalId").show();
        }
        else {
            $('.DissatisifactionwithClaimId').hide();
            $('#DissatisfactionwithclaimsREmarksDiv').hide();
            $('.AlreadyServiced').show();

            $('.VehicleSold').show();
            $('#VehicelSoldYesRNo').hide();
            $('.Dissatisfiedwithpreviousservice').show();
            $('.Distancefrom').show();
            $('.DissatisfiedwithSalesID').show();
            $('.DissatisfiedwithInsuranceId').show();
            $('#Dissatisifiedwithclaim').hide();
            $('.ExcBillingId').show();
            $('.Stolen').show();
            $('.Totalloss').show();
            $('.OtherLast').show();
            $('.Totalloss').show();
            $('.Totalloss').show();
            $("#renewalId").hide();
        }
    });

    $('#DissatisfiedwithInsuranceId').click(function () {
        if ($(this).prop('checked')) {
            $("#DissatisfactionwithInsuranceREmarksDiv").show();
            $("#alreadyservicedDiv1").hide();
            $('.DissatisifactionwithClaimId').hide();
            $("#DistancefromDealerLocationDIV").hide();
            $("#txtDissatisfiedwithpreviousservice").hide();
            $("#DissatisfactionwithSalesREmarksDiv").hide();
            $("#OtherSeriveRemarks").hide();
            $("#kmsCoverdTextDiv").hide();
            $(".Dissatisfiedwithpreviousservice").hide(); 
            $(".Distancefrom").hide();
            $(".DissatisfiedwithSalesID").hide(); 
            $(".ExcBillingId").hide();
            $(".Stolen").hide();
            $(".Totalloss").hide();
            $(".OtherLast").hide();
            $(".AlreadyServiced").hide();
            $(".VehicleSold").hide();
            $(".KmsNotCov").hide();
            $("#SubmitBtnStolen").hide();
            $(".dissatisfiedWithClaims").hide();
            $(".callDisconnected").hide();
            $(".vehicleAtWorkshop").hide();
            $(".cngVehicle").hide();
            $(".outOfStation").hide();
            $(".wantMoreDiscount").hide();
            $("#renewalId").show();
        } else {
            $("#DissatisfactionwithInsuranceREmarksDiv").hide();
            $(".Dissatisfiedwithpreviousservice").show();
            $(".Distancefrom").show();
            $(".DissatisfiedwithSalesID").show();
            $(".ExcBillingId").show();
            $(".Stolen").show();
            $(".Totalloss").show();
            $(".OtherLast").show();
            $(".AlreadyServiced").show();
            $(".VehicleSold").show();
            $(".KmsNotCov").show();
            $("#DisstisInsurancQuestion").hide();
            $(".dissatisfiedWithClaims").show();
            $('.DissatisifactionwithClaimId').show();
            $(".callDisconnected").show();
            $(".vehicleAtWorkshop").show();
            $(".cngVehicle").show();
            $(".outOfStation").show();
            $(".wantMoreDiscount").show();
            $("#renewalId").hide();
        }
    });
    // New 19th june 2017
    $('#KmsNotCoveredId').click(function () {
        if ($(this).prop('checked')) {
            $("#kmsCoverdTextDiv").show();
            $("#stolenHideShowSubmit").hide();
            $("#alreadyservicedDiv1").hide();
            $("#txtDissatisfiedwithpreviousservice").hide();
            $('.DissatisifactionwithClaimId').hide();
            $("#DistancefromDealerLocationDIV").hide();
            $("#DissatisfactionwithInsuranceREmarksDiv").hide();
            $("#DissatisfactionwithSalesREmarksDiv").hide();
            $("#OtherSeriveRemarks").hide();
            $(".Dissatisfiedwithpreviousservice").hide();
            $(".Distancefrom").hide();
            $(".DissatisfiedwithSalesID").hide();
            $(".DissatisfiedwithInsuranceId").hide();
            $(".ExcBillingId").hide();
            $(".Totalloss").hide();
            $(".OtherLast").hide();
            $(".AlreadyServiced").hide();
            $(".VehicleSold").hide();
            $(".Stolen").hide();
            $("#SubmitBtnStolen").hide();
            $(".dissatisfiedWithClaims").hide();
            $(".callDisconnected").hide();
            $(".vehicleAtWorkshop").hide();
            $(".cngVehicle").hide();
            $(".outOfStation").hide();
            $(".wantMoreDiscount").hide();

        } else {
            $("#kmsCoverdTextDiv").hide();
            $("#alreadyservicedDiv1").hide();
            $(".Dissatisfiedwithpreviousservice").show();
            $(".Distancefrom").show();
            $(".DissatisfiedwithSalesID").show();
            $(".DissatisfiedwithInsuranceId").show();
            $(".ExcBillingId").show();
            $(".Totalloss").show();
            $(".OtherLast").show();
            $(".AlreadyServiced").show();
            $('.DissatisifactionwithClaimId').show();
            $(".VehicleSold").show();
            $(".Stolen").show();
            $(".dissatisfiedWithClaims").show();
            $(".callDisconnected").show();
            $(".vehicleAtWorkshop").show();
            $(".cngVehicle").show();
            $(".outOfStation").show();
            $(".wantMoreDiscount").show();

        }
    });
    //
    $('#ExcBillingId').click(function () {
        if ($(this).prop('checked')) {
            $("#SubmitBtnStolen").show();
            $("#stolenHideShowSubmit").show();
            $("#alreadyservicedDiv1").hide();
            $("#txtDissatisfiedwithpreviousservice").hide();
            $("#DistancefromDealerLocationDIV").hide();
            $('.DissatisifactionwithClaimId').hide();
            $("#DissatisfactionwithInsuranceREmarksDiv").hide();
            $("#DissatisfactionwithSalesREmarksDiv").hide();
            $("#OtherSeriveRemarks").hide();
            $("#kmsCoverdTextDiv").hide();
            $(".Dissatisfiedwithpreviousservice").hide();
            $(".Distancefrom").hide();
            $(".DissatisfiedwithSalesID").hide();
            $(".DissatisfiedwithInsuranceId").hide();
            $(".Stolen").hide();
            $(".Totalloss").hide();
            $(".OtherLast").hide();
            $(".AlreadyServiced").hide();
            $(".VehicleSold").hide();
            $(".KmsNotCov").hide();
            $(".dissatisfiedWithClaims").hide();
            $(".callDisconnected").hide();
            $(".vehicleAtWorkshop").hide();
            $(".cngVehicle").hide();
            $(".outOfStation").hide();
            $(".wantMoreDiscount").hide();
            $("#renewalId").show();


        } else {
            $("#SubmitBtnStolen").hide();
            $("#stolenHideShowSubmit").hide();
            $("#alreadyservicedDiv1").hide();
            $(".Dissatisfiedwithpreviousservice").show();
            $(".Distancefrom").show();
            $(".DissatisfiedwithSalesID").show();
            $(".DissatisfiedwithInsuranceId").show();
            $(".Stolen").show();
            $(".Totalloss").show();
            $('.DissatisifactionwithClaimId').show();
            $(".OtherLast").show();
            $(".AlreadyServiced").show();
            $(".VehicleSold").show();
            $(".KmsNotCov").show();
            $(".dissatisfiedWithClaims").show();
            $(".callDisconnected").show();
            $(".vehicleAtWorkshop").show();
            $(".cngVehicle").show();
            $(".outOfStation").show();
            $(".wantMoreDiscount").show();
            $("#renewalId").hide();

        }
    });
    $('#Stolen').click(function () {
        if ($(this).prop('checked')) {
            $("#SubmitBtnStolen").show();
            $("#stolenHideShowSubmit").show();
            $("#alreadyservicedDiv1").hide();
            $("#txtDissatisfiedwithpreviousservice").hide();
            $('.DissatisifactionwithClaimId').hide();
            $("#DistancefromDealerLocationDIV").hide();
            $("#DissatisfactionwithInsuranceREmarksDiv").hide();
            $("#DissatisfactionwithSalesREmarksDiv").hide();
            $("#OtherSeriveRemarks").hide();
            $("#kmsCoverdTextDiv").hide();
            $(".Dissatisfiedwithpreviousservice").hide();
            $(".Distancefrom").hide();
            $(".DissatisfiedwithSalesID").hide();
            $(".DissatisfiedwithInsuranceId").hide();
            $(".ExcBillingId").hide();
            $(".Totalloss").hide();
            $(".OtherLast").hide();
            $(".AlreadyServiced").hide();
            $(".VehicleSold").hide();
            $(".KmsNotCov").hide();
            $(".dissatisfiedWithClaims").hide();
            $(".callDisconnected").hide();
            $(".vehicleAtWorkshop").hide();
            $(".cngVehicle").hide();
            $(".outOfStation").hide();
            $(".wantMoreDiscount").hide();
            $("#renewalId").show();

        } else {
            $("#SubmitBtnStolen").hide();
            $("#stolenHideShowSubmit").hide();
            $("#alreadyservicedDiv1").hide();
            $(".Dissatisfiedwithpreviousservice").show();
            $(".Distancefrom").show();
            $(".DissatisfiedwithSalesID").show();
            $(".DissatisfiedwithInsuranceId").show();
            $(".ExcBillingId").show();
            $(".Totalloss").show();
            $('.DissatisifactionwithClaimId').show();
            $(".OtherLast").show();
            $(".AlreadyServiced").show();
            $(".VehicleSold").show();
            $(".KmsNotCov").show();
            $(".dissatisfiedWithClaims").show();
            $(".callDisconnected").show();
            $(".vehicleAtWorkshop").show();
            $(".cngVehicle").show();
            $(".outOfStation").show();
            $(".wantMoreDiscount").show();
            $("#renewalId").hide();

        }
    });
    $('#Totalloss').click(function () {
        if ($(this).prop('checked')) {
            $("#SubmitBtnStolen").show();
            $("#stolenHideShowSubmit").show();
            $("#alreadyservicedDiv1").hide();
            $("#txtDissatisfiedwithpreviousservice").hide();
            $("#DistancefromDealerLocationDIV").hide();
            $("#DissatisfactionwithInsuranceREmarksDiv").hide();
            $("#DissatisfactionwithSalesREmarksDiv").hide();
            $('.DissatisifactionwithClaimId').hide();
            $("#kmsCoverdTextDiv").hide();
            $("#OtherSeriveRemarks").hide();
            $(".Dissatisfiedwithpreviousservice").hide();
            $(".Distancefrom").hide();
            $(".DissatisfiedwithSalesID").hide();
            $(".DissatisfiedwithInsuranceId").hide();
            $(".ExcBillingId").hide();
            $(".Stolen").hide();
            $(".OtherLast").hide();
            $(".AlreadyServiced").hide();
            $(".VehicleSold").hide();
            $(".KmsNotCov").hide();
            $(".dissatisfiedWithClaims").hide();
            $(".callDisconnected").hide();
            $(".vehicleAtWorkshop").hide();
            $(".cngVehicle").hide();
            $(".outOfStation").hide();
            $(".wantMoreDiscount").hide();
            $("#renewalId").show();
        } else {
            $("#SubmitBtnStolen").hide();
            $("#alreadyservicedDiv1").hide();
            $(".Dissatisfiedwithpreviousservice").show();
            $(".Distancefrom").show();
            $(".DissatisfiedwithSalesID").show();
            $(".DissatisfiedwithInsuranceId").show();
            $(".ExcBillingId").show();
            $(".Stolen").show();
            $(".OtherLast").show();
            $(".AlreadyServiced").show();
            $('.DissatisifactionwithClaimId').show();
            $(".VehicleSold").show();
            $(".KmsNotCov").show();
            $("#stolenHideShowSubmit").hide();
            $(".dissatisfiedWithClaims").show();
            $(".callDisconnected").show();
            $(".vehicleAtWorkshop").show();
            $(".cngVehicle").show();
            $(".outOfStation").show();
            $(".wantMoreDiscount").show();
            $("#renewalId").hide();
        }
    });

    $('#Other').click(function () {
        if ($(this).prop('checked')) {
            $("#OtherSeriveRemarks").show();
            $("#alreadyservicedDiv1").hide();
            $("#DistancefromDealerLocationDIV").hide();
            $("#DissatisfactionwithInsuranceREmarksDiv").hide();
            $("#txtDissatisfiedwithpreviousservice").hide();
            $("#DissatisfactionwithSalesREmarksDiv").hide();
            $("#kmsCoverdTextDiv").hide();
            $(".Dissatisfiedwithpreviousservice").hide();
            $(".Distancefrom").hide();
            $(".DissatisfiedwithSalesID").hide();
            $(".DissatisfiedwithInsuranceId").hide();
            $('.DissatisifactionwithClaimId').hide();
            $(".ExcBillingId").hide();
            $(".Totalloss").hide();
            $(".Stolen").hide();
            $(".AlreadyServiced").hide();
            $(".VehicleSold").hide();
            $(".KmsNotCov").hide();
            $("#SubmitBtnStolen").hide();
            $(".dissatisfiedWithClaims").hide();
            $(".callDisconnected").hide();
            $(".vehicleAtWorkshop").hide();
            $(".cngVehicle").hide();
            $(".outOfStation").hide();
            $(".wantMoreDiscount").hide();
            $("#renewalId").show();
        } else {
            $("#OtherSeriveRemarks").hide();
            $(".Dissatisfiedwithpreviousservice").show();
            $(".Distancefrom").show();
            $(".DissatisfiedwithSalesID").show();
            $(".DissatisfiedwithInsuranceId").show();
            $('.DissatisifactionwithClaimId').show();
            $(".ExcBillingId").show();
            $(".Stolen").show();
            $(".Totalloss").show();
            $(".OtherLast").show();
            $(".AlreadyServiced").show();
            $(".VehicleSold").show();
            $(".KmsNotCov").show();
            $("#OthersLastQuestion").hide();
            $(".dissatisfiedWithClaims").show();
            $(".callDisconnected").show();
            $(".vehicleAtWorkshop").show();
            $(".cngVehicle").show();
            $(".outOfStation").show();
            $(".wantMoreDiscount").show();
            $("#renewalId").hide();
        }
    });

    $('#callDisconnected').click(function () {
        if ($(this).prop('checked')) {
            $("#SubmitBtnStolen").show();
            $("#stolenHideShowSubmit").show();
            $("#alreadyservicedDiv1").hide();
            $("#txtDissatisfiedwithpreviousservice").hide();
            $("#DistancefromDealerLocationDIV").hide();
            $("#DissatisfactionwithInsuranceREmarksDiv").hide();
            $("#DissatisfactionwithSalesREmarksDiv").hide();
            $("#kmsCoverdTextDiv").hide();
            $("#OtherSeriveRemarks").hide();
            $(".Dissatisfiedwithpreviousservice").hide();
            $(".Distancefrom").hide();
            $(".DissatisfiedwithSalesID").hide();
            $(".DissatisfiedwithInsuranceId").hide();
            $(".ExcBillingId").hide();
            $(".Stolen").hide();
            $(".OtherLast").hide();
            $(".AlreadyServiced").hide();
            $(".VehicleSold").hide();
            $(".KmsNotCov").hide();
            $(".dissatisfiedWithClaims").hide();
            $(".Totalloss").hide();
            $(".vehicleAtWorkshop").hide();
            $(".cngVehicle").hide();
            $(".outOfStation").hide();
            $(".wantMoreDiscount").hide();
        } else {
            $("#SubmitBtnStolen").hide();
            $("#alreadyservicedDiv1").hide();
            $(".Dissatisfiedwithpreviousservice").show();
            $(".Distancefrom").show();
            $(".DissatisfiedwithSalesID").show();
            $(".DissatisfiedwithInsuranceId").show();
            $(".ExcBillingId").show();
            $(".Stolen").show();
            $(".OtherLast").show();
            $(".AlreadyServiced").show();
            $(".VehicleSold").show();
            $(".KmsNotCov").show();
            $("#stolenHideShowSubmit").hide();
            $(".dissatisfiedWithClaims").show();
            $(".callDisconnected").show();
            $(".vehicleAtWorkshop").show();
            $(".cngVehicle").show();
            $(".outOfStation").show();
            $(".wantMoreDiscount").show();
        }
    });

    $('#vehicleAtWorkshop').click(function () {
        if ($(this).prop('checked')) {
            $("#SubmitBtnStolen").show();
            $("#stolenHideShowSubmit").show();
            $("#alreadyservicedDiv1").hide();
            $("#txtDissatisfiedwithpreviousservice").hide();
            $("#DistancefromDealerLocationDIV").hide();
            $("#DissatisfactionwithInsuranceREmarksDiv").hide();
            $("#DissatisfactionwithSalesREmarksDiv").hide();
            $("#kmsCoverdTextDiv").hide();
            $("#OtherSeriveRemarks").hide();
            $(".Dissatisfiedwithpreviousservice").hide();
            $(".Distancefrom").hide();
            $(".DissatisfiedwithSalesID").hide();
            $(".DissatisfiedwithInsuranceId").hide();
            $(".ExcBillingId").hide();
            $(".Stolen").hide();
            $(".OtherLast").hide();
            $(".AlreadyServiced").hide();
            $(".VehicleSold").hide();
            $(".KmsNotCov").hide();
            $(".dissatisfiedWithClaims").hide();
            $(".Totalloss").hide();
            $(".callDisconnected").hide();
            $(".cngVehicle").hide();
            $(".outOfStation").hide();
            $(".wantMoreDiscount").hide();
        } else {
            $("#SubmitBtnStolen").hide();
            $("#alreadyservicedDiv1").hide();
            $(".Dissatisfiedwithpreviousservice").show();
            $(".Distancefrom").show();
            $(".DissatisfiedwithSalesID").show();
            $(".DissatisfiedwithInsuranceId").show();
            $(".ExcBillingId").show();
            $(".Stolen").show();
            $(".OtherLast").show();
            $(".AlreadyServiced").show();
            $(".VehicleSold").show();
            $(".KmsNotCov").show();
            $("#stolenHideShowSubmit").hide();
            $(".dissatisfiedWithClaims").show();
            $(".callDisconnected").show();
            $(".vehicleAtWorkshop").show();
            $(".cngVehicle").show();
            $(".outOfStation").show();
            $(".wantMoreDiscount").show();
        }
    });

    $('#cngVehicle').click(function () {
        if ($(this).prop('checked')) {
            $("#SubmitBtnStolen").show();
            $("#stolenHideShowSubmit").show();
            $("#alreadyservicedDiv1").hide();
            $("#txtDissatisfiedwithpreviousservice").hide();
            $("#DistancefromDealerLocationDIV").hide();
            $("#DissatisfactionwithInsuranceREmarksDiv").hide();
            $("#DissatisfactionwithSalesREmarksDiv").hide();
            $("#kmsCoverdTextDiv").hide();
            $("#OtherSeriveRemarks").hide();
            $(".Dissatisfiedwithpreviousservice").hide();
            $(".Distancefrom").hide();
            $(".DissatisfiedwithSalesID").hide();
            $(".DissatisfiedwithInsuranceId").hide();
            $(".ExcBillingId").hide();
            $(".Stolen").hide();
            $(".OtherLast").hide();
            $(".AlreadyServiced").hide();
            $(".VehicleSold").hide();
            $(".KmsNotCov").hide();
            $(".dissatisfiedWithClaims").hide();
            $(".Totalloss").hide();
            $(".callDisconnected").hide();
            $(".vehicleAtWorkshop").hide();
            $(".outOfStation").hide();
            $(".wantMoreDiscount").hide();
        } else {
            $("#SubmitBtnStolen").hide();
            $("#alreadyservicedDiv1").hide();
            $(".Dissatisfiedwithpreviousservice").show();
            $(".Distancefrom").show();
            $(".DissatisfiedwithSalesID").show();
            $(".DissatisfiedwithInsuranceId").show();
            $(".ExcBillingId").show();
            $(".Stolen").show();
            $(".OtherLast").show();
            $(".AlreadyServiced").show();
            $(".VehicleSold").show();
            $(".KmsNotCov").show();
            $("#stolenHideShowSubmit").hide();
            $(".dissatisfiedWithClaims").show();
            $(".callDisconnected").show();
            $(".vehicleAtWorkshop").show();
            $(".cngVehicle").show();
            $(".outOfStation").show();
            $(".wantMoreDiscount").show();
        }
    });

    $('#outOfStation').click(function () {
        if ($(this).prop('checked')) {
            $("#SubmitBtnStolen").show();
            $("#stolenHideShowSubmit").show();
            $("#alreadyservicedDiv1").hide();
            $("#txtDissatisfiedwithpreviousservice").hide();
            $("#DistancefromDealerLocationDIV").hide();
            $("#DissatisfactionwithInsuranceREmarksDiv").hide();
            $("#DissatisfactionwithSalesREmarksDiv").hide();
            $("#kmsCoverdTextDiv").hide();
            $("#OtherSeriveRemarks").hide();
            $(".Dissatisfiedwithpreviousservice").hide();
            $(".Distancefrom").hide();
            $(".DissatisfiedwithSalesID").hide();
            $(".DissatisfiedwithInsuranceId").hide();
            $(".ExcBillingId").hide();
            $(".Stolen").hide();
            $(".OtherLast").hide();
            $(".AlreadyServiced").hide();
            $(".VehicleSold").hide();
            $(".KmsNotCov").hide();
            $(".dissatisfiedWithClaims").hide();
            $(".Totalloss").hide();
            $(".callDisconnected").hide();
            $(".vehicleAtWorkshop").hide();
            $(".cngVehicle").hide();
            $(".wantMoreDiscount").hide();
        } else {
            $("#SubmitBtnStolen").hide();
            $("#alreadyservicedDiv1").hide();
            $(".Dissatisfiedwithpreviousservice").show();
            $(".Distancefrom").show();
            $(".DissatisfiedwithSalesID").show();
            $(".DissatisfiedwithInsuranceId").show();
            $(".ExcBillingId").show();
            $(".Stolen").show();
            $(".OtherLast").show();
            $(".AlreadyServiced").show();
            $(".VehicleSold").show();
            $(".KmsNotCov").show();
            $("#stolenHideShowSubmit").hide();
            $(".dissatisfiedWithClaims").show();
            $(".callDisconnected").show();
            $(".vehicleAtWorkshop").show();
            $(".cngVehicle").show();
            $(".outOfStation").show();
            $(".wantMoreDiscount").show();
        }
    });

    $('#wantMoreDiscount').click(function () {
        if ($(this).prop('checked')) {
            $("#SubmitBtnStolen").show();
            $("#stolenHideShowSubmit").show();
            $("#alreadyservicedDiv1").hide();
            $("#txtDissatisfiedwithpreviousservice").hide();
            $("#DistancefromDealerLocationDIV").hide();
            $("#DissatisfactionwithInsuranceREmarksDiv").hide();
            $("#DissatisfactionwithSalesREmarksDiv").hide();
            $("#kmsCoverdTextDiv").hide();
            $("#OtherSeriveRemarks").hide();
            $(".Dissatisfiedwithpreviousservice").hide();
            $(".Distancefrom").hide();
            $(".DissatisfiedwithSalesID").hide();
            $(".DissatisfiedwithInsuranceId").hide();
            $(".ExcBillingId").hide();
            $(".Stolen").hide();
            $(".OtherLast").hide();
            $(".AlreadyServiced").hide();
            $(".VehicleSold").hide();
            $(".KmsNotCov").hide();
            $(".dissatisfiedWithClaims").hide();
            $(".Totalloss").hide();
            $(".callDisconnected").hide();
            $(".vehicleAtWorkshop").hide();
            $(".cngVehicle").hide();
            $(".outOfStation").hide();
        } else {
            $("#SubmitBtnStolen").hide();
            $("#alreadyservicedDiv1").hide();
            $(".Dissatisfiedwithpreviousservice").show();
            $(".Distancefrom").show();
            $(".DissatisfiedwithSalesID").show();
            $(".DissatisfiedwithInsuranceId").show();
            $(".ExcBillingId").show();
            $(".Stolen").show();
            $(".OtherLast").show();
            $(".AlreadyServiced").show();
            $(".VehicleSold").show();
            $(".KmsNotCov").show();
            $("#stolenHideShowSubmit").hide();
            $(".dissatisfiedWithClaims").show();
            $(".callDisconnected").show();
            $(".vehicleAtWorkshop").show();
            $(".cngVehicle").show();
            $(".outOfStation").show();
            $(".wantMoreDiscount").show();
        }
    });

    //Vehicel Sold Info
    $("input[name$='VehicleSoldYes']").click(function () {
        mVehicleSold = $(this).val();
        if (mVehicleSold == "VehicleSold Yes") {
            $("#VehicleSoldClickYes").show();

        } else {
            $("#VehicleSoldClickYes").hide();
        }
    });

    //Purchase
    $("input[name='listingForm.PurchaseYes']").click(function () {
        mPurchase = $(this).val();
        if (mPurchase == "Purchase Yes") {
            $("#PurchaseClickYes").show();
        } else {
            $("#PurchaseClickYes").hide();
        }
    });
    //Last Question
    $("input[name='listingForm.CustomerFeedBackYes']").click(function () {
        mLastQuestion = $(this).val();
        if (mLastQuestion == "Customer Yes") {
            $('#addBtn').modal('show');
        } else {
            $('#addBtn').modal('hide');
        }
    });
    //VEhicelSold LastQuestion
    //Last Question.
    $("input[name='srdisposition.DistancefromDealerLocationRadio']").click(function () {
        varTransferTootherCity = $(this).val();
        if (varTransferTootherCity == "Transfer to other city") {
            $("#txtTransfertoothercity").show();
            $("#txtToofar").hide();
        } else {
            $("#txtTransfertoothercity").hide();
            $("#txtToofar").show();
        }
    });


    //20th may changes addede

    $("input[name$='listingForm.dispoCustAns']").click(function () {
        varWhatdidSay = $(this).val();
        $("input[name='srdisposition.noServiceReason']").val('');
        $(".SmrRemarks").val('');
        if (varWhatdidSay == "INS Others" || varWhatdidSay == "Other" || varWhatdidSay == "Book Appointment" || varWhatdidSay == "Book My Service" || varWhatdidSay == "Rescheduled" || varWhatdidSay == "Call Me Later" || varWhatdidSay == "Service Not Required" || varWhatdidSay == "Renewal Not Required" || varWhatdidSay == "Confirmed" || varWhatdidSay == "ConfirmedInsu" || varWhatdidSay == "Cancel Appointment" || varWhatdidSay == "Cancelled" || varWhatdidSay == "Paid") {
            $('#callMeLaterA').removeAttr("disabled", true);
            $('#alreadyServiced').removeAttr("disabled", true);
            $('#vehicleSold').removeAttr("disabled", true);
            $('#dissatifiedwithPreviousService').removeAttr("disabled", true);
            $('#dissatifiedwithPreviousServices').removeAttr("disabled", true);
            $('#dissatifiedwithPService').removeAttr("disabled", true);
            $('#dissatifiedwithService').removeAttr("disabled", true);
            $('#dissatifiedwithStolen').removeAttr("disabled", true);
            $('#vehicleSoldStolen').removeAttr("disabled", true);
            $('#stolenHideShowSubmit').removeAttr("disabled", true);

            validateCheck();
            $('#finalDiv1').hide();
            $('#CustomerDriveInDiv').hide();
            $('#feedbackDIV').hide();
            $('#CustFeedBack').hide();

            $('.followCntrl').val('');
            $('#date12345,#CommittedTimes,#serviceAdvisor,#serviceBookedTypeDisposition').val('');
        } else {
            $('#alreadyservicedDiv1').hide();
        }
    });


    $("input[name='srdisposition.reasonForHTML']").click(function () {
        varAlreadyservicedadio = $(this).val();

        if ($(this).prop("checked")) {
            if (varAlreadyservicedadio == "Serviced At My Dealer") {
                $('#ServicedMyDealerDiv').show();
                $('#ServicedAtOtherDealerDiv').hide();
            } else {
                $('#ServicedAtOtherDealerDiv').show();
                $('#ServicedMyDealerDiv').hide();
            }
        }
        else {
            $('#ServicedMyDealerDiv').hide();
            $('#ServicedAtOtherDealerDiv').hide();
        }

    });


    //chages is till heree						

    $("input[name='srdisposition.ServicedAtOtherDealerRadio']").click(function () {
        varServicedAtOtherDealer = $(this).val();

        if (varServicedAtOtherDealer == "Autorized Workshop") {
            $('#AutorizedworkshopDIV').show();
            $('.CheckedwithDMS').show();
            $('#NonAutorizedworkshopDiv').hide();
        }
        else if (varServicedAtOtherDealer == "Non Autorized Workshop") {
            $('#NonAutorizedworkshopDiv').show();
            $('#AutorizedworkshopDIV').hide();
            $('.CheckedwithDMS').hide();
        } else {
            $('#AutorizedworkshopDIV').hide();
            $('#NonAutorizedworkshopDiv').hide();
            $('.CheckedwithDMS').hide();
        }
    });

    $("input[name='ir_disposition.typeOfAutherization']").click(function () {
        varServicedAtOtherDealer = $(this).val();

        if (varServicedAtOtherDealer == "OEM Authorized Dealer") {
            $('#AutorizedworkshopDIV').show();
            $('.CheckedwithDMS').show();
        }
        else if (varServicedAtOtherDealer == "Unauthorized Dealer") {
            $('#NonAutorizedworkshopDiv').show();
            $('.CheckedwithDMS').hide();
        } else {
            $('#AutorizedworkshopDIV').hide();
            $('#NonAutorizedworkshopDiv').hide();
            $('.CheckedwithDMS').hide();
        }
    });
    $("input[name='insudisposition.typeOfAutherization']").click(function () {
        varServicedAtOtherDealer = $(this).val();

        if (varServicedAtOtherDealer == "OEM Authorized Dealer") {
            $('#AutorizedworkshopDIV').show();
            $('.CheckedwithDMS').show();
        }
        else if (varServicedAtOtherDealer == "Unauthorized Dealer") {
            $('#NonAutorizedworkshopDiv').show();
            $('.CheckedwithDMS').hide();
        }
        else if (varServicedAtOtherDealer == "Other Dealer") {
            $('#AutorizedworkshopDIV').show();
            $('#NonAutorizedworkshopDiv').hide();
            $('.CheckedwithDMS').hide();
        } else {
            $('#AutorizedworkshopDIV').hide();
            $('#NonAutorizedworkshopDiv').hide();
            $('.CheckedwithDMS').hide();
        }
    });





    $('.NoService').click(function () {
        $('.NoService').not(this).prop('checked', false);
        varAlreadyServicedR = $(this).val();
        if (varAlreadyServicedR = "Already Renewed" || varAlreadyServicedR == "Already Serviced" || varAlreadyServicedR == "Vehicle Sold" || varAlreadyServicedR == "Dissatisfied with previous service" || varAlreadyServicedR == "Distance from Dealer Location" || varAlreadyServicedR == "Dissatisfied with Sales" || varAlreadyServicedR == "Dissatisfied with Insurance" || varAlreadyServicedR == "Kms Not Covered" || varAlreadyServicedR == "Stolen" || varAlreadyServicedR == "Total loss" || varAlreadyServicedR == "Other Serive") {
            //	alreadySericedCheck();
        }
    });

    $("input[name='srdisposition.reasonForHTML']").click(function () {
        varAlreadyServiced = $(this).val();
    });

    //OutBound Upsell Opportunity--------->

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
    $('#InsuranceIDCheck').click(function () {
        if ($(this).prop('checked')) {
            $('#InsuranceSelect').show();
        } else {
            $('#InsuranceSelect').hide();
        }
    });
    $('#RoadSideAsstID').click(function () {
        if ($(this).prop('checked')) {
            $('#RoadSideAssiSelect').show();
        } else {
            $('#RoadSideAssiSelect').hide();
        }
    });

    $('#WARRANTYID').click(function () {
        if ($(this).prop('checked')) {
            $('#WARRANTYSelect').show();
        } else {
            $('#WARRANTYSelect').hide();
        }
    });

    $('#ReFinanceIDCheck').click(function () {
        if ($(this).prop('checked')) {
            $('#ReFinanceSelect').show();
        } else {
            $('#ReFinanceSelect').hide();
        }
    });

    $('#VASID').click(function () {
        if ($(this).prop('checked')) {
            $('#VASTagToSelect').show();
        } else {
            $('#VASTagToSelect').hide();
        }
    });

    $('#LoanID').click(function () {
        if ($(this).prop('checked')) {
            $('#LoanSelect').show();
        } else {
            $('#LoanSelect').hide();
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


    //Inbound Upsell Opportunity--------->
    $('#MaxicareIDCheckIn').click(function () {
        if ($(this).prop('checked')) {
            $('#MaxicareSelectIn').show();
        } else {
            $('#MaxicareSelectIn').hide();
        }
    });
    $('#ShieldIn').click(function () {
        if ($(this).prop('checked')) {
            $('#ShieldSelectIn').show();
        } else {
            $('#ShieldSelectIn').hide();
        }
    });
    $('#RoadSideAssiIDIn').click(function () {
        if ($(this).prop('checked')) {
            $('#RoadSideAssiSelectIn').show();
        } else {
            $('#RoadSideAssiSelectIn').hide();
        }
    });



    $('#InsuranceIDCheckIn').click(function () {
        if ($(this).prop('checked')) {
            $('#InsuranceSelectIn').show();
        } else {
            $('#InsuranceSelectIn').hide();
        }
    });

    $('#WARRANTYIDIn').click(function () {
        if ($(this).prop('checked')) {
            $('#WARRANTYSelectIn').show();
        } else {
            $('#WARRANTYSelectIn').hide();
        }
    });

    $('#ReFinanceIDCheckIn').click(function () {
        if ($(this).prop('checked')) {
            $('#ReFinanceSelectIn').show();
        } else {
            $('#ReFinanceSelectIn').hide();
        }
    });

    $('#VASIDIn').click(function () {
        if ($(this).prop('checked')) {
            $('#VASTagToSelectIn').show();
        } else {
            $('#VASTagToSelectIn').hide();
        }
    });

    $('#LoanIDIn').click(function () {
        if ($(this).prop('checked')) {
            $('#LoanSelectIn').show();
        } else {
            $('#LoanSelectIn').hide();
        }
    });

    $('#EXCHANGEIDIn').click(function () {
        if ($(this).prop('checked')) {
            $('#EXCHANGEIDSelectIn').show();
        } else {
            $('#EXCHANGEIDSelectIn').hide();
        }
    });

    $('#UsedCarIDIn').click(function () {
        if ($(this).prop('checked')) {
            $('#UsedCarSelectIn').show();
        } else {
            $('#UsedCarSelectIn').hide();
        }
    });


    $("input[name='srdisposition.ServicedAtOtherDealerRadio']").click(function () {
        varServicedAtOtherDealer = $(this).val();
        if (varServicedAtOtherDealer == "Autorized Workshop") {
            $('#AutorizedworkshopDIV').show();
            $('#NonAutorizedworkshopDiv').hide();
        } else {
            $('#NonAutorizedworkshopDiv').show();
            $('#AutorizedworkshopDIV').hide();
        }
    });

    $("input[name$='ir_disposition.typeOfAutherization']").click(function () {
        varServicedAtOtherDealer = $(this).val();
        if (varServicedAtOtherDealer == "OEM Authorized Dealer") {
            $('#AutorizedworkshopDIV').show();
            $('#NonAutorizedworkshopDiv').hide();
        } else {
            $('#NonAutorizedworkshopDiv').show();
            $('#AutorizedworkshopDIV').hide();
        }
    });
    $("input[name$='insudisposition.typeOfAutherization']").click(function () {
        varServicedAtOtherDealer = $(this).val();
        if (varServicedAtOtherDealer == "OEM Authorized Dealer") {
            $('#AutorizedworkshopDIV').show();
            $('#NonAutorizedworkshopDiv').hide();
        }
        else if (varServicedAtOtherDealer == "Other Dealer") {
            $('#AutorizedworkshopDIV').show();
            $('#NonAutorizedworkshopDiv').hide();
            $('.CheckedwithDMS').hide();
        }
        else {
            $('#NonAutorizedworkshopDiv').show();
            $('#AutorizedworkshopDIV').hide();
        }
    });


    $("input[name='listingForm.LeadYes']").click(function () {
        mLeadYes = $(this).val();
        if (mLeadYes == "Capture Lead Yes") {
            $("#LeadDiv").show();
        } else {
            $("#LeadDiv").hide();
        }
    });

    //Inbound Call Upsel opportunity -->
    $("input[name$='LeadYesIn']").click(function () {
        mLeadYesIn = $(this).val();
        if (mLeadYesIn == "Capture Lead YesIn") {
            $("#LeadDivIn").show();
        } else {
            $("#LeadDivIn").hide();
        }
    });

    //Inbound Complaints----->
    $("input[name$='userfeedbackIn']").click(function () {
        mfeedbackYesIn = $(this).val();
        if (mfeedbackYesIn == "feedback YesIn") {
            $("#feedbackDIVIn").show();
        } else {
            $("#feedbackDIVIn").hide();
        }
    });

    $("input[name='listingForm.userfeedback']").click(function () {
        mfeedbackYes = $(this).val();
        if (mfeedbackYes == "feedback Yes") {
            $("#feedbackDIV").show();
        } else {
            $("#feedbackDIV").hide();
        }
    });

    $("#NextToModdelDiv").on("click", function () {
        $(this).next("#CustomerDriveInID").focus();
    });

    $("#NextToFinalDiv").on("click", function () {
        $(this).next("#LeadYesID").focus();
    });

    $("input[name$='disposition']").click(function () {
        var mNoOthre = $(this).val();
        if (mNoOthre == "NoOther") {
            $("#NoOthers").show();
        } else {
            $("#NoOthers").hide();
        }
    });
    $('#LandLineNumberCheck').click(function () {
        if ($(this).prop('checked')) {
            $('#AddLandLineNumber').show()
        } else {
            $('#AddLandLineNumber').hide()
        }
    });

    $('#MobileNumberCheck').click(function () {
        if ($(this).prop('checked')) {
            $('#AddMobileNumber').show()
        }
        else {
            $('#AddMobileNumber').hide()
        }
    });
    ///////////////////////////////////////////02/01/2017///////////////////////////////////////
    $('#NextAlreadySerFeedBack').click(function () {

        var dataValue = $(this).attr('data-value');

        var chknoReqUpLead = 0;
        $('[name="listingForm.LeadYesAlradyService"]').each(function () {
            if ($(this).is(':checked')) chknoReqUpLead++;
        });
        if (chknoReqUpLead == 0) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please check one of these.'
            });

        } else {
            var radioValue = $("input[name='listingForm.LeadYesAlradyService']:checked").val();
            if (radioValue == "Capture Lead Yes AlreadyService") {

                var chknoReqUpLeadin = 0;
                $('.AlreadyupsellLeadSelectDivSB').each(function () {
                    if ($(this).is(':checked')) chknoReqUpLeadin++;
                });
                if (chknoReqUpLeadin == 0) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please check one of these'
                    });
                    return false;

                }
                $('#AlreadyServiceFeedBAck').show();
                $('#AlreadyServiceUpsellOpp').hide();

            } else {

                $('#AlreadyServiceFeedBAck').show();
                $('#AlreadyServiceUpsellOpp').hide();

            }
        }

    });

    $('#BackToAlreadySerUpsel').click(function () {
        $('.whatDidCustSayDiv').show();
        $('#AlreadyServiced').attr("checked", true);
        $('.AlreadyServiced').show();
        $('#AlreadyServiceUpsellOpp').hide();
        $('.alreadyservicedDiv1').show();
    });


    $('#BackToAlreadyServUpsell').click(function () {

        //$("input[type=checkbox]"). prop("checked",false);
        $('#AlreadyServiceUpsellOpp').show();
        $('#AlreadyServiceFeedBAck').hide();
    });

    $('#NextToLastAlreadySerQuestion').click(function () {

        var selectValDropNotReq = $('#selected_department').val();

        var selectValRemarksnotReq = $('#commentsNotReq').val();

        var userfeedbackNotReq = 0;
        $('[name="listingForm.userfeedbackAlreadyService"]').each(function () {
            if ($(this).is(':checked')) userfeedbackNotReq++;
        });
        if (userfeedbackNotReq == 0) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Choose one of theses.'
            });
            return false;
        } else {

            if ($("#userfeedbackAlreadyService").prop('checked')) {

                if (selectValDropNotReq == 0) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please Select Department.'
                    });
                    return false;
                }

                if (selectValRemarksnotReq == '') {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Remarks Should not empty.'
                    });
                    return false;
                }
                else {
                    $('#LastQuestionAlreadyServ').show();
                    $('#AlreadyServiceFeedBAck').hide();
                }
            }
            $('#LastQuestionAlreadyServ').show();
            $('#AlreadyServiceFeedBAck').hide();

        }

    });
    $('.BackToCustomerFeedback').click(function () {
        $('#AlreadyServiceFeedBAck').show();
        $('#LastQuestionAlreadyServ').hide();
    });
    ///////doday//////////////////////////////////////////
    $("input[name$='listingForm.LeadYesAlradyService']").click(function () {
        mLeadYesAlradyService = $(this).val();
        if (mLeadYesAlradyService == "Capture Lead Yes AlreadyService") {
            $("#LeadDivAlreadyService").show();
        }
        else if (mLeadYesAlradyService == "Capture Lead No AlreadyService") {
            $("#LeadDivAlreadyService").hide();

            $(".insuLead").prop("checked", false);
        }
        else {

        }
    });


    $('.upsellLeadSelectDiv').click(function () {
        var valuId = $(this).val();
        console.log(" valuId  " + valuId);

        var selectId = "upsellLeadSelect" + valuId;

        console.log(" selectId  " + selectId);

        if ($(this).prop('checked')) {
            $('#' + selectId).show();
        } else {
            $('#' + selectId).hide();
        }
    });

    $('.upsellLeadSelectDivSB').click(function () {
        var valuId = $(this).val();
        console.log(" valuId  " + valuId);

        var selectId = "upsellLeadSelectSB_" + valuId;

        console.log(" selectId  " + selectId);

        if ($(this).prop('checked')) {
            $('#' + selectId).show();
        } else {
            $('#' + selectId).hide();
        }
    });

    $('.AlreadyupsellLeadSelectDivSB').click(function () {
        var valuId = $(this).val();
        console.log(" valuId  " + valuId);

        var selectId = "AlReadyupsellLeadSelectSB_" + valuId;

        console.log(" selectId  " + selectId);

        if ($(this).prop('checked')) {
            $('#' + selectId).show();
        } else {
            $('#' + selectId).hide();
        }
    });

    $('.upsellLeadSelectDivIn').click(function () {
        var valuId = $(this).val();
        console.log(" valuId  " + valuId);

        var selectId = "upsellLeadSelectIn" + valuId;

        console.log(" selectId  " + selectId);

        if ($(this).prop('checked')) {
            $('#' + selectId).show();
        } else {
            $('#' + selectId).hide();
        }
    });
    $('input[type="radio"]').click(function () {
        if ($(this).attr("value") == "feedback Yes AlreadyService") {

            $("#feedbackDIVAlreadyService").show();
        }
        if ($(this).attr("value") == "feedback No AlreadyService") {

            $("#feedbackDIVAlreadyService").hide();
        }

    });


    $('#BackToDissatisfiedDelaerLoc').click(function () {

        $('#DistancefromDealerLocationDIV').show()
        $('#DistanceFoRRQuestion').hide()

    });

    $('#BackToAlreadyServiceFeedBack').click(function () {

        $('#AlreadyServiceFeedBAck').show()
        $('#LastQuestionAlreadyServ').hide()

    });

    $("input[name='listingForm.dispoNotTalk']").click(function () {
        var mNoOthre = $(this).val();
        if (mNoOthre == "Other") {
            $("#NoOthers").show();
            
        } else {

            $("#NoOthers").hide();
        }
    });
    $("input[name='listingForm.dispoNotTalk']").click(function () {
        var NoOther = $(this).val();
        if (NoOther == "NoOthers") {
            if (pkDealer == "KATARIA")
            {
                if ($("#NoOthers").prop('checked')) {

                    var textNoOthers = $('.NoOthersText1').val();
                    if (textNoOthers == '') {
                        Lobibox.notify('warning', {
                            continueDelayOnInactiveTab: true,
                            msg: 'Remarks Should not Empty.'
                        });
                        return false;

                    }
                }
            }
            else
            {

            }
        } else {

            //$("#NoOthers").hide();
        }
    });



    $('#BackToDissatisfiedPreviosInc').click(function () {

        $('#txtDissatisfiedwithpreviousservice').show()
        $('#DisatisfiedPreQuestion').hide()

    });
    $('#NextToInBoundLastQuest').click(function () {
        $("#CustomerNo").attr('checked', true);
        //var selectValDrop1 = $('#selected_department2').val();

        var selectRemarks = $('#commentsDSA').val();

        var userfeedbackInbound = 0;
        $('[name="userfeedbackIn"]').each(function () {
            if ($(this).is(':checked')) userfeedbackInbound++;
        });
        if (userfeedbackInbound == 0) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Choose one of these.'
            });
            return false;
        } else {

            if ($("#feedbackYesIn").prop('checked')) {

                if (selectValDrop1 == 0) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please Select Department.'
                    });
                    return false;
                }

                if (selectRemarks == '') {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Remarks Shouldnot empty.'
                    });
                    return false;
                }
                else {
                    $('#LastQuestionInbound').show();
                    $('#CustFeedBackIn').hide();
                }
            }
            $('#LastQuestionInbound').show();
            $('#CustFeedBackIn').hide();

        }
    });


    $('#ToInboindComplaintSubmit').click(function () {
        var radioBtnInboundLastQ = 0;
        $('[name="radio7"]').each(function () {
            if ($(this).is(':checked')) radioBtnInboundLastQ++;
        });
        if (radioBtnInboundLastQ == 0) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Choose.'
            });
            return false;
        } else {
            $.blockUI();

        }
    });


    $('#BackToToInboindComplaint').click(function () {

        $('#CustFeedBackIn').show()
        $('#LastQuestionInbound').hide()

    });


    //sashi ends//

    //subhendu offer function
    $('#applyOffers').click(function () {

        var offerid = $("input[name='case[]']:checked").val();


        var values = new Array();
        $.each($("input[name='case[]']:checked").closest("td").siblings("td"),
            function () {
                values.push($(this).text());
            });
        $('#selectChkVal').html(values.join(", "));


        $('.saveOffer').click(function () {

            $('#offerSelectedId').val(offerid);

        });

    });

    $('.removeOffer').click(function () {
        $("input[name='case[]']:checked").removeAttr("checked");
        $('#selectChkVal').text(' ');
    });





    $('.abc').on('click', function () {
        if ($(this).hasClass('open')) {
            $('.abc').animate({ right: '-600px' }, 300).removeClass('open');
            //$(this).animate({right:'-3px'},  300).addClass('open');
        } else {
            $('.abc').animate({ right: '-600px' }, 300).removeClass('open');
            $(this).animate({ right: '-3px' }, 300).addClass('open');
        }
    });

    $('#example700').on('click', function (e) {
        e.stopPropagation();
    });

    $('#example7889').on('click', function (e) {
        e.stopPropagation();
    });

    $('input[type="radio"]').click(function () {
        var x = $(this).val();
        $('#submitDndBtn').on("click", function () {
            if (x === "makeDND") {
                console.log("clicked maske dnd");
                $("#btnDnd").addClass("btn btn-danger");
            }
            if (x === "removeDND") {

                console.log("clicked remove dnd");
                $("#btnDnd").removeClass("btn btn-danger");
                $("#btnDnd").addClass("btn btn-success");
            }
        });

    });
    //$('input[type="drop-down"]').click(function () {
    //    var x = $(this).val();
    //    $('#submitDndBtn').on("click", function () {
    //        if (x === "makeDND") {
    //            console.log("clicked maske dnd");
    //            $("#btnDnd").addClass("btn btn-danger");
    //        }
    //        if (x === "removeDND") {

    //            console.log("clicked remove dnd");
    //            $("#btnDnd").removeClass("btn btn-danger");
    //            $("#btnDnd").addClass("btn btn-success");
    //        }
    //    });

    //});



    //validation//



    $('#callMeLaterSubmitService').click(function () {
      
        var dateVal = $('#FollowUpDateService').val();
        var timeVal = $('#FollowUpTimeService').val();
        var reason = $('#followReason').val();
        var Creremarks = $('#listingForm_commentsList_1_').val();
        var OEM = $('#PkOEM').val();
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

        if (OEM == "MARUTI SUZUKI") {
            if (reason == '' || reason == '0' || reason == null) {
                Lobibox.notify('warning', {
                    msg: 'Please select followup reason.'
                });
                return false;
            }
        }

        if ((pkDealer == "INDUS") && (Creremarks == '' || Creremarks.length < 10)) {
            Lobibox.notify('warning',
                {
                    msg: 'CRE Remarks should be minimum 10 characters'
                });
            return false;
        }

        //$.blockUI();
    });


    $(".oeditAddressmodal").click(function () {
        if ($('.addrPin').val() == '()') {
            $('.addrPin').val('');
        }
    });


    $(".addrPin").keydown(function (e) {
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 || (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) || (e.keyCode >= 35 && e.keyCode <= 40)) {
            return;
        }
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
    $('.peditAddressmodal').click(function () {
        if ($('.paddr_line6').val() == '0' || $('.paddr_line6').val() == '()') {
            $('.paddr_line6').val('');
        }
    });
    $('.reditAddressmodal').click(function () {
        if ($('.raddr_line6').val() == '0' || $('.raddr_line6').val() == '()') {
            $('.raddr_line6').val('');
        }
    });
    $('.oeditAddressmodal').click(function () {
        if ($('.paddr_line5').val() == '0' || $('.paddr_line5').val() == '()') {
            $('.paddr_line5').val('');
        }
    });

    var dateToday = new Date();
    var dates = $("#date12345").datepicker({
        //defaultDate: "+1w",
        dateFormat: 'dd-mm-yy',
        maxDate: "+30d",
        minDate: 0,
        onSelect: function (selectedDate) {
            var option = this.id == "date12345" ? "minDate" : "maxDate",
                instance = $(this).data("datepicker"),
                date = $.datepicker.parseDate(instance.settings.dateFormat || $.datepicker._defaults.dateFormat, selectedDate, instance.settings);
            dates.not(this).datepicker("option", option, date);
        }
    });
    var dateTodayext = new Date();
    var dates = $("#date123456").datepicker({
        //defaultDate: "+1w",
        dateFormat: 'dd-mm-yy',
        minDate: 0,
        onSelect: function (selectedDate) {
            var option = this.id == "date123456" ? "minDate" : "maxDate",
                instance = $(this).data("datepicker"),
                date = $.datepicker.parseDate(instance.settings.dateFormat || $.datepicker._defaults.dateFormat, selectedDate, instance.settings);
            //dates.not(this).datepicker("option", option, date);
        }
    });
    var dateToday1 = new Date();
    var dates1 = $("#FollowUpDate").datepicker({
        //defaultDate: "+1w",
        dateFormat: 'dd-mm-yy',
        maxDate: "+180d",
        minDate: 0,
        onSelect: function (selectedDate) {
            var option = this.id == "FollowUpDate" ? "minDate" : "maxDate",
                instance = $(this).data("datepicker"),
                date = $.datepicker.parseDate(instance.settings.dateFormat || $.datepicker._defaults.dateFormat, selectedDate, instance.settings);
            // dates.not(this).datepicker("option", option, date);
        }
    });







    ///validation for book my service

    $('#bookMyserviceSubmit').click(function () {
        var chkincSubmit = 0;
        $('[name="listingForm.CustomerFeedBackYes"]').each(function () {
            if ($(this).is(':checked')) chkincSubmit++;
        });
        if (chkincSubmit == 0) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please check any one in customer info option.'
            });
            return false;

        } else {
            //$.blockUI();
        }

        //-------------Upsell validation ------------------------
        var atLeastOneIsChecked = 0;
        $('[name="listingForm.LeadYes"]').each(function () {
            if ($(this).is(':checked')) atLeastOneIsChecked++;
        });
        if (atLeastOneIsChecked == 0) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please check any one of the upsell.'
            });

        } else {
            if ($("#LeadYesID").prop('checked')) {


                var checkeds = $('.myOutCheckbox').is(':checked');

                if (checkeds) {

                } else {

                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please check any one from upsell.'

                    });
                    return false;
                }

            } else if ($("#LeadNoID").prop('checked')) {

            }


            //$("#CustFeedBack").show();
            //$("#finalDiv1").hide();
        }



        //-------------department feed start -------------------------------
        var selectValDrop = $('#selected_department1').val();

        var selectValRemarks = $('#commentsOfFB').val();

        var userfeedbackOutbound = 0;
        $('[name="listingForm.userfeedback"]').each(function () {
            if ($(this).is(':checked')) userfeedbackOutbound++;
        });
        if (userfeedbackOutbound == 0) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Choose any one from feedback/complaints.'
            });
            return false;
        } else {

            if ($("#feedbackYes").prop('checked')) {

                if (selectValDrop == 0) {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Please Select Department.'
                    });
                    return false;
                }

                if (selectValRemarks == '') {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'Remarks should not be empty.'
                    });
                    return false;
                }
                else {
                    //$("#LastQuestion").show();
                    //$("#CustFeedBack").hide();
                }
            }
            //$("#LastQuestion").show();
            //$("#CustFeedBack").hide();

        }
    });
    $('#nonContactValidation').click(function () {
        var Creremarks = $('#listingForm_commentsList_9_').val();

        var chkincSubmit = 0;
        $('[name="listingForm.dispoNotTalk"]').each(function () {
            if ($(this).is(':checked')) chkincSubmit++;
        });
        if (chkincSubmit == 0) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Select one.'
            });
            return false;


        }
        else
        {
            if ($("#NOOthersCheck").prop('checked')) {
                var textNoOthers = $('.NoOthersText1').val();
                if (pkDealer == "KATARIA" && textNoOthers == '') {
                    Lobibox.notify('warning', {
                        continueDelayOnInactiveTab: true,
                        msg: 'please specify the reason.'
                    });
                    return false;
                }
            }
            if ((pkDealer == "INDUS") && (Creremarks == '' || Creremarks.length < 10)) {
                Lobibox.notify('warning',
                    {
                        msg: 'CRE Feedback should be minimum 10 characters'
                    });
                return false;
            } else {}

            $.blockUI();

        }
        
    });

    //$('#customer_lead_tag').click(function () {
    //    var leadtag = $('#customer_lead_tag').val();

    //    if (leadtag > 3) {
    //        Lobibox.notify('warning', {
    //            continueDelayOnInactiveTab: true,
    //            msg: 'Cannot select more than 3 checkbox.'
    //        });
    //        return false;


    //    }
    //    else
    //    { }     
    //});



    //added function 22/12/2016
    $('#inBoundLeadSourceSelectVal,#serviceTypeAltIdDataVal').on('change', function () {
        if ($('#inBoundLeadSourceSelectVal').val() != 0 && $('#serviceTypeAltIdDataVal').val() != 0) {
            $('#booksMyServiceDisplayBlock').css('display', 'block');
        }
        else {
            $('#booksMyServiceDisplayBlock').css('display', 'none');
        }
    });

    $('#alreadyServiced').click(function () {
        var chknoReqSubmit = 0;
        $('[name="radio7"]').each(function () {
            if ($(this).is(':checked')) chknoReqSubmit++;
        });
        if (chknoReqSubmit == 0) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please choose one.'
            });
            return false;

        } else {

            $.blockUI();

        }
    });
    $('#vehicleSoldStolen').click(function () {
        var chknoReqOthers = 0;
        $('[name="radio8"]').each(function () {
            if ($(this).is(':checked')) chknoReqOthers++;
        });
        if (chknoReqOthers == 0) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please choose one.'
            });
            return false;

        } else {

            $.blockUI();

        }
    });
    $('#dissatifiedwithService').click(function () {
        var chknoReqInsu = 0;
        $('[name="radio6"]').each(function () {
            if ($(this).is(':checked')) chknoReqInsu++;
        });
        if (chknoReqInsu == 0) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please choose one.'
            });
            return false;

        } else {

            $.blockUI();

        }
    });
    $('#dissatifiedwithPService').click(function () {
        var chknoReqsales = 0;
        $('[name="radio5"]').each(function () {
            if ($(this).is(':checked')) chknoReqsales++;
        });
        if (chknoReqsales == 0) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please choose one.'
            });
            return false;

        } else {

            $.blockUI();

        }
    });
    $('#dissatisifiedwithclaims').click(function () {
        var chknoReqInsu = 0;
        $('[name="radio7"]').each(function () {
            if ($(this).is(':checked')) chknoReqInsu++;
        });
        if (chknoReqInsu == 0) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please choose one.'
            });
            return false;

        } else {

            $.blockUI();

        }
    });

    $("#backToDissSatiSalse").click(function () {
        $("#DissatisfactionwithSalesREmarksDiv").show();
        $("#DisstisFiedSaleRQuestion").hide();
    });


    $("#bsckTodisInsu").click(function () {
        $("#DissatisfactionwithInsuranceREmarksDiv").show();
        $("#DisstisInsurancQuestion").hide();
    });
    $("#backclaim").click(function () {
        $("#DissatisfactionwithclaimsREmarksDiv").hide();
        $("#Dissatisifiedwithclaimquestion").hide();
    });
    $("#BacktoOtherInsu").click(function () {
        $("#OtherSeriveRemarks").show();
        $("#OthersLastQuestion").hide();
    });

    //shashi added on 4th jan 17
    //Incomming Call Back to next---------------------------------------->
    $("#NextToLeadInbound").click(function () {

        var inboundInc = 0;
        $('[name="typeOfPickupIn"]').each(function () {
            if ($(this).is(':checked')) inboundInc++;
        });
        if (inboundInc == 0) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Select one of these.'
            });
        }
        else 
        {
            $("#finalDiv1Inbound").show();
            $("#CustomerDriveInDivIn").hide();
        }

        var upselFlag = $('#selectedUpselOpp').val();

        //alert(upselFlag);

        if (upselFlag == "0" || upselFlag == null || upselFlag == "") {
            $("#LeadNoIDIn").prop("checked", true);
        } else {
            $("#LeadYesIDIn").prop("checked", true);
            $("#LeadDivIn").show();
        }
        //loadLeadBasedOnLocation();
        var sr_int_id = $('#srdispo_id').val();
        //alert("sr_int_id : "+sr_int_id);		

        var urlDisposition = "/CRE/upsellSelectedLastSB/" + sr_int_id + "";
        $.ajax({

            url: urlDisposition

        }).done(function (upselData) {

            console.log(upselData);

            for (var i = 0; i < upselData.length; i++) {

                if (upselData[i].upSellType == "Insurance") {

                    $('#InsuranceIDCheckIn').prop('checked', true);
                    $('#InsuranceSelectIn').show();
                    $('#comments8').val(upselData[i].upsellComments);


                } else if (upselData[i].upSellType == "Warranty / EW") {
                    $('#WARRANTYIDIn').prop('checked', true);
                    $('#WARRANTYSelectIn').show();
                    $('#comments9').val(upselData[i].upsellComments);

                } else if (upselData[i].upSellType == "Re-Finance / New Car Finance") {

                    $('#ReFinanceIDCheckIn').prop('checked', true);
                    $('#ReFinanceSelectIn').show();
                    $('#comments11').val(upselData[i].upsellComments);


                } else if (upselData[i].upSellType == "VAS") {

                    $('#VASIDIn').prop('checked', true);
                    $('#VASTagToSelectIn').show();
                    $('#comments10').val(upselData[i].upsellComments);

                } else if (upselData[i].upSellType == "Sell Old Car") {

                    $('#LoanIDIn').prop('checked', true);
                    $('#LoanSelectIn').show();
                    $('#comments12').val(upselData[i].upsellComments);

                } else if (upselData[i].upSellType == "Buy New Car / Exchange") {

                    $('#EXCHANGEIDIn').prop('checked', true);
                    $('#EXCHANGEIDSelectIn').show();
                    $('#comments13').val(upselData[i].upsellComments);

                } else if (upselData[i].upSellType == "UsedCar") {

                    $('#UsedCarIDIn').prop('checked', true);
                    $('#UsedCarSelectIn').show();
                    $('#comments14').val(upselData[i].upsellComments);



                }
            }
        });



    });


    $('#example7889').DataTable({
        "paging": false,
        "ordering": false,
        "info": false,
        "searching": false
    });

    $('#example700').DataTable({
        "paging": false,
        "ordering": false,
        "info": false,
        "searching": false
    });
    $('#example800').DataTable({
        "fixedHeader": true,
        "scrollX": true,
        "scrollY": 400,
        "bDestroy": true,
        "paging": false,
        "searching": false,
        "ordering": false,
        "bInfo": false
    });



    //Added by Shashidhar 25 jan 2017 
    $(".onlyAlphabetOnly").keypress(function (a) {
        var b = a.charCode;
        b >= 65 && b <= 120 || 32 == b || 0 == b || a.preventDefault()
    }), $("#LeadNoID").click(function () {
        $("#InsuranceIDCheck").attr("checked", !1),
            $("#WARRANTYID").attr("checked", !1),
            $("#VASID").attr("checked", !1),
            $("#ReFinanceIDCheck").attr("checked", !1),
            $("#LoanID").attr("checked", !1),
            $("#EXCHANGEID").attr("checked", !1),
            $("#UsedCarID").attr("checked", !1),
            $("#InsuranceSelect").hide(),
            $("#WARRANTYSelect").hide(),
            $("#VASTagToSelect").hide(),
            $("#ReFinanceSelect").hide(),
            $("#LoanSelect").hide(),
            $("#EXCHANGEIDSelect").hide(),
            $("#UsedCarSelect").hide(),
            $("#sellOldCarLead1").val('0'),
            $("#commentsSellOld").val('')

    }), $("#LeadNoIDIn").click(function () {
        $("#InsuranceIDCheckIn").attr("checked", !1), $("#WARRANTYIDIn").attr("checked", !1), $("#VASIDIn").attr("checked", !1), $("#ReFinanceIDCheckIn").attr("checked", !1), $("#LoanIDIn").attr("checked", !1), $("#EXCHANGEIDIn").attr("checked", !1), $("#UsedCarIDIn").attr("checked", !1), $("#InsuranceSelectIn").hide(), $("#WARRANTYSelectIn").hide(), $("#VASTagToSelectIn").hide(), $("#ReFinanceSelectIn").hide(), $("#LoanSelectIn").hide(), $("#EXCHANGEIDSelectIn").hide(), $("#UsedCarSelectIn").hide()
    }), $("#CustomerDriveInID").click(function () {
        $("#time_FromDriver").val(""), $("#time_ToDriver").val(""), $("#driverIdSelect").val("0")

    }), $("#BookMyService").click(function () {

        var servBookExist = $('#servicebook_id').val();

        //alert(servBookExist);
        if (servBookExist != "") {
            Lobibox.confirm({
                msg: "This service is already booked do you want to modify?",
                callback: function ($this, type) {
                    if (type === 'yes') {

                        $("#FollowUpDate").val(""), $("#FollowUpTime").val(""), $("#AlreadyServiced").attr("checked", !1), $("#VehicleSold").attr("checked", !1), $("#Dissatisfiedwithpreviousservice").attr("checked", !1), $("#Distancefrom").attr("checked", !1), $("#DissatisfiedwithSalesID").attr("checked", !1), $("#DissatisfiedwithInsuranceId").attr("checked", !1), $("#Stolen").attr("checked", !1), $("#Totalloss").attr("checked", !1),
                            $("#OtherReason").attr("checked", !1),
                            $("#alreadyservicedDiv1").hide(),
                            $("#VehicelSoldYesRNo").hide(),
                            $("#txtDissatisfiedwithpreviousservice").hide(),
                            $("#DistancefromDealerLocationDIV").hide(),
                            $("#DissatisfactionwithSalesREmarksDiv").hide(),
                            $("#DissatisfactionwithInsuranceREmarksDiv").hide(),
                            $("#OtherSeriveRemarks").hide(),
                            $("#stolenHideShowSubmit").hide(),
                            $("#stolenDamageSubmit").hide()
                        //$("#serviceBookDiv").show()

                    } else if (type === 'no') {
                        $("#serviceBookDiv").hide();
                        $("#SMRInteractionFirst").show();
                        $("#DidYouTalkDiv").show();
                        $("#whatDidCustSayDiv").hide();
                        $('#SpeakYes').attr('checked', false);
                        $('#SpeakNo').attr('checked', false);
                        varWhatdidSay = "";
                        $("input[name='listingForm.dispoCustAns']").prop('checked', false);

                    }

                }
            });

        } else {
            $("#FollowUpDate").val(""), $("#FollowUpTime").val(""), $("#AlreadyServiced").attr("checked", !1), $("#VehicleSold").attr("checked", !1), $("#Dissatisfiedwithpreviousservice").attr("checked", !1), $("#Distancefrom").attr("checked", !1), $("#DissatisfiedwithSalesID").attr("checked", !1), $("#DissatisfiedwithInsuranceId").attr("checked", !1), $("#Stolen").attr("checked", !1), $("#Totalloss").attr("checked", !1),
                $("#OtherReason").attr("checked", !1),
                $("#alreadyservicedDiv1").hide(),
                $("#VehicelSoldYesRNo").hide(),
                $("#txtDissatisfiedwithpreviousservice").hide(),
                $("#DistancefromDealerLocationDIV").hide(),
                $("#DissatisfactionwithSalesREmarksDiv").hide(),
                $("#DissatisfactionwithInsuranceREmarksDiv").hide(),
                $("#OtherSeriveRemarks").hide(),
                $("#stolenHideShowSubmit").hide(),
                $("#stolenDamageSubmit").hide()
        }
    }), $("#BookMyAppointment").click(function () {

        var servBookExist = $('#appointBookId').val();

        //alert(servBookExist);
        if (servBookExist != "") {
            Lobibox.confirm({
                msg: "This Insurance is already booked do you want to modify?",
                callback: function ($this, type) {
                    if (type === 'yes') {

                        $("#FollowUpDate").val(""), $("#FollowUpTime").val(""), $("#AlreadyServiced").attr("checked", !1), $("#VehicleSold").attr("checked", !1), $("#Dissatisfiedwithpreviousservice").attr("checked", !1), $("#Distancefrom").attr("checked", !1), $("#DissatisfiedwithSalesID").attr("checked", !1), $("#DissatisfiedwithInsuranceId").attr("checked", !1), $("#Stolen").attr("checked", !1), $("#Totalloss").attr("checked", !1),
                            $("#OtherReason").attr("checked", !1),
                            $("#alreadyservicedDiv1").hide(),
                            $("#VehicelSoldYesRNo").hide(),
                            $("#txtDissatisfiedwithpreviousservice").hide(),
                            $("#DistancefromDealerLocationDIV").hide(),
                            $("#DissatisfactionwithSalesREmarksDiv").hide(),
                            $("#DissatisfactionwithInsuranceREmarksDiv").hide(),
                            $("#OtherSeriveRemarks").hide(),
                            $("#stolenHideShowSubmit").hide(),
                            $("#stolenDamageSubmit").hide()





                    } else if (type === 'no') {
                        $("#SMRInteractionFirst").show();
                        $("#DidYouTalkDiv").show();
                        $("#whatDidCustSayDiv").hide();
                        $("#serviceBookDiv").hide();
                        $('#SpeakYes').attr('checked', false);
                        $('#SpeakNo').attr('checked', false);

                    }

                }
            });

        } else {
            $("#FollowUpDate").val(""), $("#FollowUpTime").val(""), $("#AlreadyServiced").attr("checked", !1), $("#VehicleSold").attr("checked", !1), $("#Dissatisfiedwithpreviousservice").attr("checked", !1), $("#Distancefrom").attr("checked", !1), $("#DissatisfiedwithSalesID").attr("checked", !1), $("#DissatisfiedwithInsuranceId").attr("checked", !1), $("#Stolen").attr("checked", !1), $("#Totalloss").attr("checked", !1),
                $("#OtherReason").attr("checked", !1),
                $("#alreadyservicedDiv1").hide(),
                $("#VehicelSoldYesRNo").hide(),
                $("#txtDissatisfiedwithpreviousservice").hide(),
                $("#DistancefromDealerLocationDIV").hide(),
                $("#DissatisfactionwithSalesREmarksDiv").hide(),
                $("#DissatisfactionwithInsuranceREmarksDiv").hide(),
                $("#OtherSeriveRemarks").hide(),
                $("#stolenHideShowSubmit").hide(),
                $("#stolenDamageSubmit").hide()
        }





    }), $("#CallMeLatter").click(function () {
        $("#serviceBookedTypeSelect").val("0"),
            $("#workshop").val("0"),
            $("#date12345").val(""),
            $("#CommittedTimes").val(""),
            $("#serviceAdvisor").val("0"),
            $("#AlreadyServiced").attr("checked", !1),
            $("#VehicleSold").attr("checked", !1),
            $("#Dissatisfiedwithpreviousservice").attr("checked", !1),
            $("#Distancefrom").attr("checked", !1),
            $("#DissatisfiedwithSalesID").attr("checked", !1),
            $("#DissatisfiedwithInsuranceId").attr("checked", !1),
            $("#Stolen").attr("checked", !1),
            $("#Totalloss").attr("checked", !1),
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

            $("input[type=checkbox]").prop("checked", false);
        $("#InsuranceSelect").hide(),
            $("#WARRANTYSelect").hide(),
            $("#VASTagToSelect").hide(),
            $("#ReFinanceSelect").hide(),
            $("#LoanSelect").hide(),
            $("#EXCHANGEIDSelect").hide(),
            $("#UsedCarSelect").hide(),
            $("#alreadyservicedDiv1").hide(),
            $("#VehicelSoldYesRNo").hide(),
            $("#txtDissatisfiedwithpreviousservice").hide(),
            $("#DistancefromDealerLocationDIV").hide(),
            $("#DissatisfactionwithSalesREmarksDiv").hide(),
            $("#DissatisfactionwithInsuranceREmarksDiv").hide(),
            $("#OtherSeriveRemarks").hide(),
            $("#stolenHideShowSubmit").hide(),
            $("#stolenDamageSubmit").hide(),
            $("#OtherReason").attr("checked", !1)
    }),

        $("#ServiceNotRequired").click(function () {
            $("#serviceBookedTypeSelect").val("0"),
                $("#workshop").val("0"),
                $("#date12345").val(""),
                $("#CommittedTimes").val(""),
                $("#serviceAdvisor").val("0"),
                $("#FollowUpDate").val(""),
                $("#FollowUpTime").val(""),
                $(".AlreadyServiced").show(),
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

                $("input[type=checkbox]").prop("checked", false);
            $("#InsuranceSelect").hide(),
                $("#WARRANTYSelect").hide(),
                $("#VASTagToSelect").hide(),
                $("#ReFinanceSelect").hide(),
                $("#LoanSelect").hide(),
                $("#EXCHANGEIDSelect").hide(),
                $("#UsedCarSelect").hide(),
                $(".OtherLast").show()
        })


    //Add phone by subendhu

    $('.numberOnly').keypress(function (e) {

        if (isNaN(this.value + "" + "." + String.fromCharCode(e.charCode))) {
            return false;
        }

    });

    $(".textOnlyAccepted").keypress(function (e) {
        var code = e.keyCode || e.which;
        if ((code < 65 || code > 90) && (code < 97 || code > 122) && code != 32 && code != 46) {
            return false;
        }
    });



    //$("#savePhoneCust").on('click', function () {

    //    //alert("ad new phone");
    //    var lastValue = parseFloat($('#preffered_contact_num').val()) + 1;
    //    var conLenthSave = $('#preffered_contact_num').children('option').length;
    //    if (conLenthSave >= 1) {
    //        $("#deleteNum").show()
    //    }

    //    var custMobNo = $("#myPhoneNum").val();
    //    var remarks = $("#myPhoneRemark").val();

    //    if (custMobNo.length < 10) {

    //        Lobibox.notify('warning', {
    //            continueDelayOnInactiveTab: true,
    //            msg: 'Mobile Number Is Invalid.'
    //        });
    //        return false;

    //    }
    //    if (remarks.length < 5) {

    //        Lobibox.notify('warning', {
    //            continueDelayOnInactiveTab: true,
    //            msg: 'Please Enter the Remark.'
    //        });
    //        var conLenth = $('#preffered_contact_num').children('option').length;
    //        if (conLenth == 1) {
    //            $("#deleteNum").hide();
    //        }
    //        return false;

    //    }

    //    var wyzUser_id = document.getElementById('wyzUser_Id').value;
    //    var customer_Id = document.getElementById('customer_Id').value;
    //    var urlDisposition = siteRoot+"/CallLogging/saveaddcustomermobno/";

    //    $.ajax({
    //        type: 'POST',
    //        url: urlDisposition,
    //        datatype: 'json',
    //        data: { remarks: '' + remarks, custMobNo: custMobNo },
    //        cache: false,
    //        success: function (data) {
    //            $('#myPhoneRemark').val('');
    //            if (data != 0) {

    //                //$('#ddl_phone_no').val(custMobNo);
    //                $('#pref_number_callini').val(custMobNo);

    //                $("#ddl_phone_no").prepend($("<option></option>").text(lastValue).html(custMobNo).val(custMobNo));

    //                $("#ddl_phone_no option:first").prop('selected', true);


    //                $("#preffered_contact_num").prepend($("<option></option>").text(lastValue).html(custMobNo).val(data));

    //                $("#preffered_contact_num option:first").prop('selected', true);
    //                var conLenth = $('#preffered_contact_num').children('option').length;
    //                if (conLenth == 1) {
    //                    $("#deleteNum").hide();
    //                }

    //            } else {

    //                Lobibox.notify('warning', {
    //                    continueDelayOnInactiveTab: true,
    //                    msg: 'Mobile Number Is Aready Exisiting.'
    //                });
    //            }
    //        }, error(error) {

    //        }
    //    });
    //});
    ///////////////////Shashi 25th march 2017//////////////////////
    $(".datepicMYDropDown").datepicker({
        changeMonth: true,
        changeYear: true,
        maxDate: '0',
        dateFormat: 'dd-mm-yy',
        yearRange: "-100:+0"

    });

    $(".datepickerPlus30Days").datepicker({
        maxDate: "+30d",
        dateFormat: 'dd-mm-yy',
        minDate: 0
    });

    /* $( "#date12345" ).datepicker({
       maxDate: "+30d",
       minDate:0
    }); */
    $("input[name$='AddOnsYes']").click(function () {
        var varAddOnsYes = $(this).val();

        if (varAddOnsYes == "Yes") {
            $("#AllAdd_onsDiv").show();

        }
        if (varAddOnsYes == "No") {
            $("#AllAdd_onsDiv").hide();
            $('.Add_Onschk').attr("checked", false);
        } else { }

    });

    $("input[name$='PremiumYes']").click(function () {
        varPremiumYes = $(this).val();

        if (varPremiumYes == "Yes") {
            $("#InsurancePremiumDiv").show();

        }
        if (varPremiumYes == "No") {
            $("#InsurancePremiumDiv").hide();
            $("#InsuCompSelectID").val('0');
            $("#InsuDSAID").val('');

        }
        else {


        }

    });
    //$("input[name$='nomineeYes']").click(function () {
    //    varNomineeYes = $(this).val();

    //    if (varNomineeYes == "Yes") {
    //        $("#nomineeDiv").show();

    //    }
    //    if (varNomineeYes == "No") {
    //        $("#nomineeDiv").hide();
    //        $("#NomineeNameID").val('');
    //        $("#NomineeAgeID").val('');
    //        $("#NomineeRelationID").val('');
    //        $("#AppointeeNameID").val('');
    //    }
    //    else {


    //    }
    //});

    $("#editFinalPremium").click(function () {

        $("#PremiumwithTax").attr('readonly', false).focus();
    });


    $("#editFinalPremiumInB").click(function () {
        $("#PremiumwithTaxInB").attr('readonly', false).focus();
    });

    ////< !----------Inbound Inurance----------------------->
    $("input[name$='AddOnsYesInB']").click(function () {
        var varAddOnsYesInB = $(this).val();

        if (varAddOnsYesInB == "Yes") {
            $("#AllAdd_onsDivInB").show();

        }
        if (varAddOnsYesInB == "No") {
            $("#AllAdd_onsDivInB").hide();
            $('.Add_OnschkInB').attr("checked", false);
        } else { }

    });

    $("input[name$='PremiumYesInB']").click(function () {
        varPremiumYesInB = $(this).val();

        if (varPremiumYesInB == "Yes") {
            $("#InsurancePremiumDivInB").show();

        }
        if (varPremiumYesInB == "No") {
            $("#InsurancePremiumDivInB").hide();
            $("#InsuCompSelectIDInB").val('0');
            $("#InsuDSAIDInB").val('');

        }
        else {


        }

    });
    $("input[name$='nomineeYesInB']").click(function () {
        varNomineeYesInB = $(this).val();

        if (varNomineeYesInB == "Yes") {
            $("#nomineeDivInB").show();

        }
        if (varNomineeYesInB == "No") {
            $("#nomineeDivInB").hide();
            $("#NomineeNameIDInB").val('');
            $("#NomineeAgeIDInB").val('');
            $("#NomineeRelationIDInB").val('');
            $("#AppointeeNameIDInB").val('');
        }
        else {


        }
    });

    $("#editFinalPremiumInB").click(function () {
        $("#PremiumwithTaxInB").attr('disabled', false).focus();
    });

    ////< !----------inbound Flow-------------------->
    $("#NextInsurnceAddOns1").click(function () {
        var varinsurInb = $("#time_FromDriverInHM").val();
        var varinsurInbTime = $("#time_ToDriverInHM").val();
        var varinsurInbSR = $("#time_FromDriverIn").val();
        var varinsurInbTimeSR = $("#time_ToDriverIn").val();
        var varSwrmsSeltIdInBSR = $("#ShowroomsSelectIdInB").val();



        if (document.getElementById('homeVisitIdIn').checked) {
            if (varinsurInb == '') {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'From Time Is Required.'
                });
                return false;
            }
            if (varinsurInbTime == '') {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'To Time Is Required.'
                });
                return false;
            }


            $("#Add_OnsIndurace1InB").show();
            $("#HomeAdnShowrromVisit").hide();
        }
        else if (document.getElementById('showroomVisitIdIn').checked) {
            if (varinsurInbSR == '') {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'From Time Is Required.'
                });
                return false;
            }
            if (varinsurInbTimeSR == '') {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'To Time Is Required.'
                });
                return false;
            }
            if (varSwrmsSeltIdInBSR == '0') {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please Select List Of Showrooms.'
                });
                return false;
            }


            $("#Add_OnsIndurace1InB").show();
            $("#HomeAdnShowrromVisit").hide();

        }
        else {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please check one.'
            });
        }





    });











    $("#BackToNewInsu1InB").click(function () {

        $("#HomeAdnShowrromVisit").show();
        $("#Add_OnsIndurace1InB").hide();

    });

    $("#NextToNewInsu1InB").click(function () {
        if (document.getElementById('AddOnsNoRIdInB').checked) {
            $("#PremiumInsu2InB").show();
            $("#HomeAdnShowrromVisit").hide();
            $("#Add_OnsIndurace1InB").hide();

        }
        else if (document.getElementById('AddOnsYesRIdInB').checked) {
            $("#PremiumInsu2InB").show();
            $("#HomeAdnShowrromVisit").hide();
            $("#Add_OnsIndurace1InB").hide();

        }
        else {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please check one.'
            });
            return false;
        }




    });

    $("#BackToNewInsu2InB").click(function () {

        $("#Add_OnsIndurace1InB").show();
        $("#PremiumInsu2InB").hide();

    });


    $("#nextToCustomerDriveIn").click(function () {

        //var varRenewalTy=$("#renewalTypeIn").val();
        //var RenewalModeIDS=$("#RenewalModeIDS").val();
        //var varSelectDateinsura=$(".SelectDateinsura").val();
        var vaWorkShop = $("#workshopIn").val();
        var vadate123456 = $("#date123456").val();
        var vaCommittedTimesIn = $("#CommittedTimesIn").val();
        var vaserviceAdviIn = $("#serviceAdvisorIdIn").val();

        if (vaWorkShop == '0') {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Select WorkShop.'
            });
            return false;

        }
        else if (vadate123456 == '') {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Select Date.'
            });
            return false;

        }
        else if (vaCommittedTimesIn == '') {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Select Time.'
            });
            return false;

        }
        else if (vaserviceAdviIn == '0' || vaserviceAdviIn == null) {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Select Service Advisor.'
            });
            return false;
        }
        else {

            $("#CustomerDriveInDivIn").show();
            $("#HomeAdnShowrromVisit").show();
            $("#InCallserviceBookDiv").hide();
            $("#BookMyServiceIn").hide();
        }

    });
    $("#BackToinsuhmVisit").click(function () {

        $("#BookMyServiceIn").show();
        $("#InCallserviceBookDiv").show();
        $("#HomeAdnShowrromVisit").hide();


    });
    $("#NextToNewInsu3InB").click(function () {
        if (document.getElementById('nomineeYesIDInB').checked) {
            var varNomineeNameInB = $('#NomineeNameIDInB').val();
            var varNomineeAgeIDInB = $('#NomineeAgeIDInB').val();
            var varNomineeRelIDInB = $('#NomineeRelationIDInB').val();
            var varAppointNameIdInB = $('#AppointeeNameIDInB').val();


            if (varNomineeNameInB == '') {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please Enter Nominee Name.'
                });
                return false;
            }
            if (varNomineeAgeIDInB == '') {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please Enter Nominee Age.'
                });
                return false;
            }
            if (varNomineeRelIDInB == '') {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please Enter Nominee Relation with Owner.'
                });
                return false;
            }
            if (varAppointNameIdInB == '') {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: 'Please Enter Appointee Name.'
                });
                return false;
            }

            $("#finalDiv1Inbound").show();
            $("#nomineeDetails3InB").hide();
        }
        else if (document.getElementById('nomineeNoIDInB').checked) {
            $("#finalDiv1Inbound").show();
            $("#nomineeDetails3InB").hide();
        }
        else {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please check one.'
            });
        }


    });

    $("#BackToLeadIn").click(function () {
        $("#CustomerDriveInDivIn").show();
        $("#nomineeDetails3InB").show();
        $("#finalDiv1Inbound").hide();



    });
    $("#BackToNewInsu3InB").click(function () {
        $("#PremiumInsu2InB").show();
        $("#nomineeDetails3InB").hide();


    });


    $('.timepicker_7').timepicker({
        showPeriod: false, // 24 hours formate
        onHourShow: timepicker7OnHourShowCallback,
        showPeriodLabels: false,

    });
    $('#appointmentTime1').timepicker({
        showPeriod: false, // 24 hours formate
        onHourShow: timepicker7OnHourShowCallback,
        showPeriodLabels: false,
        defaultTime: '12:00',
        dynamic: true,
    });
    function timepicker7OnHourShowCallback(hour) {
        if ((hour > 20) || (hour < 8)) {
            return false;
        }
        return true;
    }

    $('.timePickRange7to19').timepicker({
        showPeriod: false, // 24 hours formate
        onHourShow: timepick,
        showPeriodLabels: false,

    });

    function timepick(hour) {
        if ((hour > 19) || (hour < 7)) {
            return false;
        }
        return true;
    }

    $('.single-input').timepicker({
        showPeriodLabels: false,
    });


    //DND

    var dndFlag = $('#dndCust').val();

    if (dndFlag == 'true') {
        //alert(dndFlag);
        $("#dndcheckbox1").prop("checked", true);
        $("#DNDbtn").show();
    } else if (dndFlag == 'false') {
        $("#dndcheckbox2").prop("checked", true);
    }


    $('input[name="doNotDisturb"]').click(function () {
        var vDND = $(this).val();
        if (vDND == 'true') {
            $("#DNDbtn").show();
        }
        else if (vDND == 'false') {
            $("#DNDbtn").hide();

        }
        else { }

    });

    //dispo

    $("#AddAddrMMSSave").click(function () {
        //	alert("AddAddrMMSSave");
        var vAddr1 = $("#AddAddress1All").val();
        var vAddr2 = $("#AddAddress2All").val();
        var vState4 = $("#AddAddrMMSState").val();
        var vcity3 = $("#cityInputPopup3").val();
        var vpin5 = $("#PinCode1MMS").val();

        var ResultAll = vAddr1.concat("," + vAddr2 + " , " + vcity3 + " , " + vState4 + "," + vpin5);

        $("#AddressMSSId").prepend($("<option></option>").html(ResultAll));
        $("#AddressMSSId option:first").prop('selected', true);

    });

    //curent mileahe validation

    $("#kmsSubmitID").click(function () {
        var vCurrMile = $("#CurrentMileage").val();
        var vexpVisit = $("#expectedVisitDT").val();
        //alert(vCurrMile);
        if (vCurrMile == "0") {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Enter Current Mileage.'
            });
            return false;


        } else if (vexpVisit == "") {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Enter Expected Visit Date.'
            });
            return false;

        } else if (vCurrMile == "") {
            $("#CurrentMileage").val(0);
            return true;

        } else {
            return true;
        }

    });
    //change service booking date
    $(".datepickerchange").datepicker({
        dateFormat: 'dd-mm-yy',
        maxDate: "+30d",
        minDate: 0,

    });



});

//Drvier schedule

function tableText(tableCell) {
    //alert('clicked two inside'+tableCell.id);
    vtempValue = parseInt(document.getElementById("tempValue").value);


    if (vtempValue == tableCell.cellIndex || vtempValue == 0) {
        vtempValue = tableCell.cellIndex//    table.rows[0].cells[j].innerText;

    }
    else { return; }
    console.log("tablecel id : " + tableCell.id);
    var className = tableCell.className;

    if (className == "") {
        //alert('clicked 4');
        $.confirm({
            title: 'Confirm!',
            closeIcon: true,
            content: 'Do you want Assign' + "<br>" + tableCell.id,
            buttons: {
                Yes: function () {
                    vstartValue = parseInt(document.getElementById("startValue").value);
                    vendValue = parseInt(document.getElementById("endValue").value);


                    if (vstartValue === 0) {
                        vstartValue = tableCell.parentElement.rowIndex;
                        document.getElementById("startValue").value = tableCell.parentElement.rowIndex;
                    }
                    else if (vendValue === 0) {
                        vendValue = tableCell.parentElement.rowIndex;
                        document.getElementById("endValue").value = vendValue;

                    }

                    $(tableCell).addClass('ColorGreen');
                    if (vendValue === 0) {
                        vendValue = tableCell.parentElement.rowIndex;
                    }
                    if (vstartValue != 0 && vendValue != 0) {
                        if (vstartValue < tableCell.parentElement.rowIndex) {
                            if (vendValue < tableCell.parentElement.rowIndex) {
                                vendValue = tableCell.parentElement.rowIndex;
                            }
                        } else {
                            vstartValue = tableCell.parentElement.rowIndex;
                        }
                        if (vstartValue > vendValue) {
                            minValue = vendValue;
                            maxValue = vstartValue;
                        } else {
                            minValue = vstartValue;
                            maxValue = vendValue;
                        }

                        var table = document.getElementById("tableID");
                        var tableCellheader = table.rows[0].cells[tableCell.cellIndex];

                        //console.log("tableCellheader : "+tableCellheader.id);
                        //console.log("tableCellheader innerText: "+tableCellheader.innerText);

                        document.getElementById("driverValue").value = tableCellheader.id;
                        $('#newDriver').html(tableCellheader.innerText);

                        console.log("min and max value : " + minValue, maxValue);

                        for (var i = minValue; i < maxValue; i++) {
                            var tableCell1 = table.rows[i].cells[tableCell.cellIndex];
                            $(tableCell1).addClass('ColorGreen');
                        }

                        document.getElementById("startValue").value = minValue;
                        document.getElementById("endValue").value = maxValue;
                        minValue = '';
                        maxValue = '';

                    }


                    document.getElementById("tempIncreValue").value = $('#tableID .ColorGreen').length;
                    document.getElementById("tempValue").value = tableCell.cellIndex;
                    if ($('#tableID .ColorGreen').length === 0) {
                        document.getElementById("startValue").value = 0;
                        document.getElementById("endValue").value = 0;
                        document.getElementById("tempValue").value = 0;
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

                    vstartValue = parseInt(document.getElementById("startValue").value);
                    vendValue = parseInt(document.getElementById("endValue").value);
                    var table1 = document.getElementById('tableID');
                    $(tableCell).removeClass('ColorGreen');
                    if (vstartValue !== tableCell.parentElement.rowIndex && vendValue === tableCell.parentElement.rowIndex) {
                        for (i = vendValue - 1; i >= vstartValue; i--) {
                            var tableCell2 = table1.rows[i].cells[tableCell.cellIndex];
                            if ($(tableCell2).hasClass('ColorGreen')) {
                                vendValue = i;
                                document.getElementById("endValue").value = i;
                                break;
                            }
                            continue;
                        }
                    } else if (vstartValue === tableCell.parentElement.rowIndex && vendValue !== tableCell.parentElement.rowIndex) {
                        for (i = vstartValue + 1; i <= vendValue; i++) {
                            var tableCell2 = table1.rows[i].cells[tableCell.cellIndex];
                            if ($(tableCell2).hasClass('ColorGreen')) {
                                vstartValue = i;
                                document.getElementById("startValue").value = i;
                                break;
                            }
                            continue;
                        }
                    } else if (vstartValue === tableCell.parentElement.rowIndex && vendValue === tableCell.parentElement.rowIndex) {
                        document.getElementById("startValue").value = 0;
                        document.getElementById("endValue").value = 0;
                        document.getElementById("tempValue").value = 0;
                    }


                    document.getElementById("tempIncreValue").value = $('#tableID .ColorGreen').length;
                    document.getElementById("tempValue").value = tableCell.cellIndex;
                    if ($('#tableID .ColorGreen').length === 0) {
                        document.getElementById("startValue").value = 0;
                        document.getElementById("endValue").value = 0;
                        document.getElementById("tempValue").value = 0;
                    }

                },
                No: function () {
                    $(tableCell).addClass('ColorGreen');

                }
            }
        });
    }




}
//showing the driver schedule
//function ajaxAssignBtnBkreview(workshop, dateis) {
//    //alert("AssignBtnBkreview");
//    //$.blockUI();


//    //var mcallInOut = $("input[name$='InOutCallName']").val();

//    var mcallInOut = $("input[name='InOutCallName']:checked").val();

//    if (mcallInOut === "InCall") {
//        workshop = "workshopIn";
//        dateis = "date123456";

//    }
//    console.log(mcallInOut);
//    console.log(workshop);
//    console.log(dateis);

//    var newdates = $('#changeserviceBookingDate').val();
//    //alert(newdates);
//    if (newdates != "") {
//        $('#newbookingdate').html(newdates);
//        $('#' + dateis).val(newdates);
//    } else {
//        var newDateSB = $('#' + dateis).val();

//        $('#newbookingdate').html(newDateSB);
//        $('#changeserviceBookingDate').val(newDateSB);
//    }


//    var scheduleDate = document.getElementById(dateis).value;
//    var workshopId = document.getElementById(workshop).value;

//    var urlPath = "/driverListSchedule/" + workshopId + "/" + scheduleDate;
//    $.ajax({
//        url: urlPath
//    }).done(function (dataDriver) {
//        var data = dataDriver.driverListData;
//        var timeslot = dataDriver.timeSlotData;

//        $('#tableID').empty();

//        var table = document.getElementById("tableID");
//        var header = table.createTHead();
//        var row = header.insertRow(0);


//        for (var i = data.length - 1; i >= 0; i--) {
//            var cell = row.insertCell(0);
//            cell.outerHTML = "<th id=" + data[i].id + ">" + data[i].userName + "</th>";
//        }

//        var cell = row.insertCell(0);
//        cell.outerHTML = "<th>Time slot</th>";

//        for (var j = 0; j < timeslot.length; j++) {
//            tr = $('<tr/>');
//            tr.append('<td id=' + timeslot[0].timeRange + ' At ' + timeslot[j].timeRange + '>' + timeslot[j].timeRange + '</td>');
//            for (var k = 0; k < data.length; k++) {

//                var blockedTime = data[k].listTime;
//                var result = inArray(timeslot[j].startTime, blockedTime)
//                //console.log("Time is result: "+result);  
//                if (result) {
//                    tr.append('<td id=' + timeslot[j].timeRange + ' class="ColorRed"></td>');
//                } else {

//                    tr.append('<td id=' + timeslot[j].timeRange + '></td>');
//                }

//            }
//            $('#tableID').append(tr);

//            $.fn.dataTable.ext.errMode = 'none';
//            $($.fn.dataTable.tables(true)).DataTable()
//                .columns.adjust();
//            $('#tableID').dataTable({

//                "fixedHeader": true,
//                "scrollX": true,
//                "scrollY": 200,

//                "paging": false,
//                "searching": false,
//                "ordering": false,
//                "bInfo": false
//            });
//        }

//        var firsftSelect;
//        if (table != null) {
//            for (var i = 1; i < table.rows.length; i++) {
//                for (var j = 1; j < table.rows[i].cells.length; j++) {
//                    //table.rows[i].cells[j].id= table.rows[0].cells[j].innerText +" At " + table.rows[i].cells[0].innerText;
//                    //console.log("class name : "+table.rows[i].cells[j].className )
//                    var blockedclass = table.rows[i].cells[j].className;

//                    if (blockedclass == "ColorRed") { } else {
//                        table.rows[i].cells[j].onclick = function () {
//                            //alert('clicked one ');
//                            tableText(this);


//                        };
//                    }

//                }
//            }
//        }
//        // $.unblockUI();

//    });

//}

//finding matching data with array
function inArray(needle, haystack) {
    var count = haystack.length;
    for (var i = 0; i < count; i++) {
        console.log("blocked Time" + haystack[i].TotalHours)
        console.log("Time slot", needle.TotalHours)
        if (haystack[i].TotalHours === needle.TotalHours) {
            var times = haystack[i];
            var blockd = needle;
            return true;
        }
        //else
        //{
        //    return false;

        //}
    }
    return false;
}


function cancelBooking() {

    $("#alreadyserviceDIV").show();
    $("#ServeBookCancelBTN").hide();
    //$.confirm({
    //    title: 'Confirm!',
    //    closeIcon: true,
    //    content: 'VEHICLE will be blocked For next 24 HOURS, Do you really want to cancel the Booking?',
    //    buttons: {
    //        Yes: function () {
    //            $("#alreadyserviceDIV").show();
    //            $("#ServeBookCancelBTN").hide();
    //        },
    //        No: function () {
    //            $("#alreadyserviceDIV").hide();
    //            $("#ServeBookCancelBTN").hide();

    //        }
    //    }
    //});


}
function cancelPickup() {

    $.confirm({
        title: 'Confirm!',
        closeIcon: true,
        content: 'Do you want to cancel Pickup ?',
        buttons: {
            Yes: function () {
                $("#ServeBookCancelBTN").show();
                $("#alreadyserviceDIV").hide();
            },
            No: function () {
                $("#ServeBookCancelBTN").hide();
                $("#alreadyserviceDIV").hide();

            }
        }
    });


}


function getSMSbyLocAndSMSType() {
    console.log("i9nisde the functio n");

    var customerId = $('#customer_Id').val();
    var vehicleId = $('#vehical_Id').val();
    var locId = $('#locId').val();
    var smstypeid = $('#smstype').val();
    var typeOfDispo = $('#typeOfDispoPageView').val();
    var urlLink = "/CRE/getSMSbyLocAndSMSType/" + locId + "/" + smstypeid + "/" + customerId + "/" + vehicleId + "/" + typeOfDispo;
    console.log(urlLink);
    $.ajax({
        url: urlLink

    }).done(function (smsname) {
        console.log(smsname);



        $('#smstemplate').empty();

        $('#smstemplate').val(smsname);

    });

}

//Delete phone no update

$('#infoBtn').click(function () {
    var vPhNumlength = $('#preffered_contact_num').children('option').length;

    if (vPhNumlength <= 1) {
        $("#deleteNum").hide();

    }
});

var vPhNumlength = $('#preffered_contact_num').children('option').length;

if (vPhNumlength <= 1) {
    $("#deleteNum").hide();

}
else {
    var userrole = $("#userrole").val();
    var DealerCode = $("#PkDealercode").val();
    if (DealerCode == "KATARIA") {
        if (userrole == "CREManager") {
            $("#deleteNum").show();
        } else {
            $("#deleteNum").hide();
        }
    } else {
        $("#deleteNum").show();
    }

}
//$("#dltPhoneCust").click(function () {

//    var remarks = document.getElementById('myPhnRemark').value;

//    if (remarks.length < 5) {
//        Lobibox.notify('warning', {
//            msg: 'Please Enter the Remark.'

//        });
//        return false;
//    }
//    var userId = document.getElementById('wyzUser_Id').value;
//    var phnId = document.getElementById('preffered_contact_num').value;
//    if (confirm('Are you sure want to Delete ?')) {
//        var urlLink = "/CRE/deletePhoneNumberOfCustomer/" + phnId + "/" + userId;
//        $.ajax({
//            'type': 'POST',
//            'url': urlLink,
//            'data': {
//                remarks: '' + remarks,
//            }
//        }).done(function () {
//            $('#preffered_contact_num option:selected').remove();
//            $('#ddl_phone_no option:selected').remove();
//            var conLenth = $('#preffered_contact_num').children('option').length;
//            if (conLenth == 1) {
//                $("#deleteNum").hide();
//            }
//        });
//        $('#myPhnRemark').val('');
//    }
//    var conLenth = $('#preffered_contact_num').children('option').length;
//    if (conLenth == 1) {
//        $("#deleteNum").hide();
//    }

//});

var fullDisAddress = $("#prefAdressUpdate").text()
$("#prefAdressUpdate").attr('title', fullDisAddress);
//var onlyTootip = fullDisAddress.substring(0, 55);
//$("#prefAdressUpdate").text(onlyTootip);




// New Insurance Design Chnages on 20th june 2018 //


$("input[name$='UpdateAddOn']").click(function () {
    var varUpdateAddOn = $(this).val();
    if (varUpdateAddOn == 'Yes') {
        $("#UpdateAddONYESDIV").show();
    }
    else {
        $("#UpdateAddONYESDIV").hide();
    }

});
$("input[name$='LeadYesRNR']").click(function () {
    var leadRNR = $(this).val();
    if (leadRNR == "Capture Lead Yes") {
        $("#LeadDivRNR").show();

    }
    else {
        $("#LeadDivRNR").hide();

    }

});
$("input[name$='CustomerfeedbackRNR']").click(function () {
    var leadRNR = $(this).val();
    if (leadRNR == "Yes") {
        $("#FeedBackYESDivRNR").show();
    }
    else {
        $("#FeedBackYESDivRNR").hide();
    }

});


//Insurance change july

$("input[name$='Customerfeedback']").click(function () {
    var leadRNR = $(this).val();
    if (leadRNR == "Yes") {
        $("#FeedBackYESDiv").show();
    }
    else {
        $("#FeedBackYESDiv").hide();
    }

});
//till here

$('#RSAIDInsu').click(function () {
    if ($(this).prop('checked')) {
        $('#RSATagToSelectInsu').show();
    } else {
        $('#RSATagToSelectInsu').hide();
    }
});

$('#EXCHANGEIDInsu').click(function () {
    if ($(this).prop('checked')) {
        $('#EXCHANGEIDSelectInsu').show();
    }
    else {
        $('#EXCHANGEIDSelectInsu').hide();
    }
});
$("#MaxicareIDCheckInsu").click(function () {
    if ($(this).prop('checked')) {
        $("#MaxicareSelectInsu").show()
    }
    else {
        $("#MaxicareSelectInsu").hide()
    }
});
$("#ShieldIDInsu").click(function () {
    if ($(this).prop('checked')) {
        $("#ShieldSelectInsu").show();
    }
    else {
        $("#ShieldSelectInsu").hide();
    }

});
$("#InsuranceIDCheckInsu").click(function () {
    if ($(this).prop('checked')) {
        $("#InsuranceSelectInsu").show();
    } else {
        $("#InsuranceSelectInsu").hide();
    }

});
$("#UsedCarIDInsu").click(function () {
    if ($(this).prop('checked')) {
        $("#UsedCarSelectInsu").show();
    } else {
        $("#UsedCarSelectInsu").hide();
    }

});



//Insurance changes 19th 6 2018 
$("input[name$='InsuranceAppointment']").click(function () {
    var varInsuCancel = $(this).val();

    if (varInsuCancel == "Insurance Appointment") {

        cancelInsurance();


    }
    else {


    }

});

function cancelInsurance() {

    $.confirm({
        title: 'Confirm!',
        closeIcon: true,
        content: 'VEHICLE will be blocked For next 24 HOURS, Do you really want to cancel the Appointment?',
        buttons: {
            Yes: function () {
                $("#alreadyserviceDIV").show();
                $("#confirmInsuComments").hide();
                $("#CancelInsuAppo").hide();
            },
            No: function () {
                $("#confirmInsuComments").show();
                $("#alreadyserviceDIV").hide();
                $("#CancelInsuAppo").hide();

            }
        }
    });


}


function setTomorrowDateAndTime() {
    var date = new Date();
    //  var tomorrow = new Date(date.getFullYear(), date.getMonth(), (date.getDate() + 1));
    $('#insdate123').datepicker({
        autoclose: true,
        dateFormat: 'dd-mm-yy',
        autoclose: true,
    }).datepicker();
} function setTomorrowDate() {
    var date = new Date();
    //  var tomorrow = new Date(date.getFullYear(), date.getMonth(), (date.getDate() + 1));
    $('#insdate12345').datepicker({
        autoclose: true,
        dateFormat: 'dd-mm-yy',
        autoclose: true,
    }).datepicker();
}
//Field Service Executive Schedular

function tableTextInsurance(tableCell) {
    //alert('clicked two inside'+tableCell.id);
    vtempValueIns = parseInt(document.getElementById("tempValueIns").value);


    if (vtempValueIns == tableCell.cellIndex || vtempValueIns == 0) {
        vtempValueIns = tableCell.cellIndex//    table.rows[0].cells[j].innerText;

    }
    else { return; }
    console.log("tablecel id : " + tableCell.id);
    var className = tableCell.className;

    if (className == "") {
        //alert('clicked 4');
        $.confirm({
            title: 'Confirm!',
            closeIcon: true,
            content: 'Do you want Assign' + "<br>" + tableCell.id,
            buttons: {
                Yes: function () {
                    vstartValueIns = parseInt(document.getElementById("startValueIns").value);
                    vendValueIns = parseInt(document.getElementById("endValueIns").value);


                    if (vstartValueIns === 0) {
                        vstartValueIns = tableCell.parentElement.rowIndex;
                        document.getElementById("startValueIns").value = tableCell.parentElement.rowIndex;
                    }
                    else if (vendValueIns === 0) {
                        vendValueIns = tableCell.parentElement.rowIndex;
                        document.getElementById("endValueIns").value = vendValueIns;

                    }

                    $(tableCell).addClass('ColorGreen');
                    if (vendValueIns === 0) {
                        vendValueIns = tableCell.parentElement.rowIndex;
                    }
                    if (vstartValueIns != 0 && vendValueIns != 0) {
                        if (vstartValueIns < tableCell.parentElement.rowIndex) {
                            if (vendValueIns < tableCell.parentElement.rowIndex) {
                                vendValueIns = tableCell.parentElement.rowIndex;
                            }
                        } else {
                            vstartValueIns = tableCell.parentElement.rowIndex;
                        }
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
                        document.getElementById("endValueIns").value = 0;
                        document.getElementById("tempValueIns").value = 0;
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
                    $(tableCell).addClass('ColorGreen');

                }
            }
        });
    }
}
//chethan Auto SMS aded
$("input[name$='listingForm.dispoCustAns']").click(function () {
    enablemsmsbookingmode = $(this).val();
    $('#issmsenbledsave').val("true");
    $("#sendbookingsmschk").prop("checked", true);

    if (enablemsmsbookingmode == "Book My Service" || enablemsmsbookingmode == "Rescheduled" || enablemsmsbookingmode == "Confirmed" || enablemsmsbookingmode == "Book Appointment" || enablemsmsbookingmode == "Reschedule" || enablemsmsbookingmode == "Call Me Later" || enablemsmsbookingmode == "Service Not Required") {
        $("#SendSMSDiv").show();
    }
    else {
        $("#SendSMSDiv").hide();


    }
});


$("#newServiceBook").click(function () {
    var Creremarks = $('#listingForm_commentsList_7_').val();


         if ((pkDealer == "INDUS") && (Creremarks == '' || Creremarks.length < 10)) {
        Lobibox.notify('warning',
            {
                msg: 'CRE Feedback should be minimum 10 characters'
            });
        return false;
    }
});