$("#AddAddrMMSSaveMe").on('click', function () {
    var dealerName = $('#PkDealercode').val();

    var address1 = $("#AddAddress1All").val();
    var address2 = $("#AddAddress2All").val();
    var state = $("#AddAddrMMSState").val();
    var city = $("#cityInputPopup3").val();
    var pin = $("#PinCode1MMS").val();

    if (dealerName == "INDUS" || dealerName == "ADVAITHHYUNDAI") {
        if (address1 == '') {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Enter Enter AddressLine1'
            });
            return false;
        }

        //else if (address2 == '') {
        //    $("#AddAddress2All").val(" ");
        //}
    }
    else
    {

        if (address1 == '' && address2 == '' && state == '' && city == '' && pin == '') {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Enter The Mandatory Fields .'
            });
            return false;

        }
        else if (address1 == '') {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Enter Enter AddressLine1'
            });
            return false;
        }

        else if (address2 == '') {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Please Enter AddressLine2'
            });
            return false;
        }
        else if (state == '') {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Enter State'
            });
            return false;

        }
        else if (city == "--SELECT--") {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Enter City'
            });
            return false;

        }
        else if (pin == '') {
            Lobibox.notify('warning', {
                continueDelayOnInactiveTab: true,
                msg: 'Enter Pincode'
            });
            return false;

        }
    }
});


$("#saveEmailCust").on('click', function () {

    var customerEmail = $("#myEmailNum").val();
    var re = /^(([^<>()[\]\\.,;:\s@@\"]+(\.[^<>()[\]\\.,;:\s@@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,3}))$/igm;
    if (re.test(customerEmail)) {

    } else {
        $('#myEmailNum').val('');
        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Invalid Email Address.'
        });
        return false;
    }
});

$("#savePhoneCust").on('click', function () {
    //alert("ad new phone");
    var lastValue = parseFloat($('#preffered_contact_num').val()) + 1;
    var conLenthSave = $('#preffered_contact_num').children('option').length;
    if (conLenthSave >= 1) {
        $("#deleteNum").show()
    }

    var custMobNo = $("#myPhoneNum").val();
    var remarks = $("#myPhoneRemark").val();

    if (custMobNo.length < 10) {

        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Mobile Number Is Invalid.'
        });
        return false;

    }
    if (remarks.length < 5) {

        Lobibox.notify('warning', {
            continueDelayOnInactiveTab: true,
            msg: 'Please Enter the Remark(Min character length is 5).'
        });
        var conLenth = $('#preffered_contact_num').children('option').length;
        if (conLenth == 1) {
            $("#deleteNum").hide();
        }
        return false;

    }
});


$("#dltPhoneCust").click(function () {

    var remarks = document.getElementById('myPhnRemark').value;

    if (remarks.length < 5) {
        Lobibox.notify('warning', {
            msg: 'Please Enter the Remark.'

        });
        return false;
    }
    var phnId = document.getElementById('preffered_contact_num').value;
    if (confirm('Are you sure want to Delete ?')) {
        var urlLink = siteRoot+"/customerUpdate/deletePhone/";
        $.ajax({
            type: 'POST',
            url: urlLink,
            datatype: 'json',
            data: { phnId: phnId, remarks: remarks },
            cache: false,
            success: function (res) {
                if (res.success == true) {
                    Lobibox.notify('success', {
                        msg: 'Mobile number deleted successfully'
                    });
                    $('#ddl_phone_no').empty();
                    $("#preffered_contact_num").empty();
                    var appenddata1 = "";
                    var appenddata2 = "";
                    var Phone = "";

                    for (var i = 0; i < res.phnum.length; i++) {
                        Phone = Phone + res.phnum[i].phoneNo + ",";
                        appenddata1 += "<option value = '" + res.phnum[i].id + " '>" + res.phnum[i].phoneNo + " </option>";
                        appenddata2 += "<option value = '" + res.phnum[i].id + " '>" + res.phnum[i].phoneNo + " </option>";
                    }

                    if (res.phnum.length == 1) {
                        $('#deleteNum').hide();
                    }

                    $("#preffered_contact_num").append(appenddata1);
                    $("#ddl_phone_no").append(appenddata2);
                    $("#prefPhone").text(Phone);


                    $('#myPhnRemark').val('');
                    $('#myModalUpdatePhone').modal("hide");

                }
                else {
                    Lobibox.notify('warning', {
                        msg: 'Cannot Delete Primary Mobile number'
                    });
                    $('#myPhnRemark').val('');
                    $('#myModalUpdatePhone').modal("hide");
                    
                    return false;

                }
            }, error(error) {

            }
        });
    }
});

//$("#dltPhoneCust").click(function () {

//    var remarks = document.getElementById('myPhnRemark').value;

//    if (remarks.length < 5) {
//        Lobibox.notify('warning', {
//            msg: 'Please Enter the Remark.'

//        });
//        return false;
//    }
//    var phnId = $('#preffered_contact_num opetion:selected').text();
//    if (confirm('Are you sure want to Delete ?')) {
//        var urlLink = siteRoot+"/customerUpdate/deletePhone/";
//        $.ajax({
//            type: 'POST',
//            url: urlLink,
//            datatype: 'json',
//            data: { phnId: phnId, remarks: remarks },
//            cache: false,
//            success: function (res) {
//                if (res.success == true) {
//                    Lobibox.notify('info', {
//                        msg: 'Mobile number deleted successfully'
//                    });
//                }
//                else {
//                    alert(res.error);
//                }
//            }, error(error) {

//            }
//        });
//    }
//});
