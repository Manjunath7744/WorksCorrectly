//Notification for Service
function ajaxRequestForPopUpNotificationCRE() {
    var url = siteRoot + "/Home/getServiceNotificationToday"
    //debugger;
    $.ajax({
        type: 'POST',
        url: url,
        datatype: 'json',
        data: {  },
        cache: false,
        success: function (res) {
            if (res.success == true) {
                $("#followUpCountOfToday").html(res.data.length);
                var existDate = '';
                for (i = 0; i < res.data.length; i++) {
                    tr = $('<tr/>');
                    tr.append("<td>" + res.data[i].callInteraction_id + "</td>");
                    tr.append("<td>" + res.data[i].dealerCode + "</td>");
                    tr.append("<td>" + res.data[i].customerName + "</td>");
                    tr.append("<td>" + res.data[i].followUpTime + "</td>");
                    tr.append("<td>" + res.data[i].vehicle_vehicle_id + "</td>");
                    tr.append("<td>" + res.data[i].customer_id + "</td>");
                    $('#tblfubNote').append(tr);

                    if (res.data[i].followUpTime != null) {
                        console.log("existDate :" + existDate + " res.data[i].followUpTime " + res.data[i].followUpTime);
                        if (existDate != res.data[i].followUpTime) {

                            var now = new Date();

                            console.log("not same both times" + res.data[i].followUpTime);
                            var fupTime = toDate(res.data[i].followUpTime, 'H:MM')
                            var millisTill10 = new Date(now.getFullYear(), now.getMonth(), now.getDate(), fupTime.getHours(), fupTime.getMinutes(), 0, 0) - now;
                            console.log(" millisTill10 " + millisTill10 + " i is " + i);
                            if (millisTill10 > 0) {

                                setTimeout(function () { showNotify(); }, millisTill10);

                            }

                        } else {
                            console.log("same as previous  both times");

                            var now = new Date();
                            now.setMinutes(now.getMinutes() + 3);
                            console.log("the +3 time is :" + now + " res.data[i].followUpTime is " + res.data[i].followUpTime);

                            var fupTime = toDate(res.data[i].followUpTime, 'H:MM')
                            var millisTill10 = new Date(now.getFullYear(), now.getMonth(), now.getDate(), fupTime.getHours(), fupTime.getMinutes(), 0, 0) - now;

                            console.log(" millisTill10 " + millisTill10 + " i is " + i);

                            if (millisTill10 > 0) {
                                setTimeout(function () { showNotify(); }, millisTill10);
                            }
                        }

                        existDate = res.data[i].followUpTime;
                    }
                }
            }
            else {
                Lobibox_notify('warning', {
                    msg: 'Some thing went wrong..FollowUp'
                });
            }
        }, error(error) {

        }
    });
}

//SERviceRemainder notify
function showNotify() {
    var d = new Date();
    var h = addZero(d.getHours());
    var m = addZero(d.getMinutes());

    var fupTime = h + ":" + m;

    var table = document.getElementById("tblfubNote");
    for (var i = 0, row; row = table.rows[i]; i++) {

        //alert("service : "+fupTime+" followuptime"+row.cells[3].innerHTML)
        if (fupTime === row.cells[3].innerHTML) {
            Lobibox_notify('info', {
                delay: 24 * 5000,
                msg: 'Service Follow Up is required for ' + row.cells[2].innerHTML,
                onClickUrl: siteRoot + "/Home/redirectToCallLogging//CRE%2cS%2c" + row.cells[5].innerHTML + "%2c" + row.cells[4].innerHTML
            });
        }
    }
}

//Notification for PSF
function ajaxRequestForPsfPopupNotificationCRE() {
    //alert("ajaxRequestForPsfPopupNotificationCRE");
    var url = siteRoot + "/Home/getPSFFollowUpNotificationToday";

    $.ajax({
        type: 'POST',
        url: url,
        datatype: 'json',
        data: {},
        cache: false,
        success: function (res) {
            if (res.success == true) {
                $("#psffollowUpCountOfToday").html(res.data.length);

                console.log(" data.length :" + res.data.length);
                var existDate = '';
                for (i = 0; i < res.data.length; i++) {
                    tr = $('<tr/>');
                    tr.append("<td>" + res.data[i].customer_id + "</td>"); //0
                    tr.append("<td>" + res.data[i].vehicle_id + "</td>"); //1
                    tr.append("<td>" + res.data[i].psfAssignedInteraction_id + "</td>"); //2
                    tr.append("<td>" + res.data[i].campaign_id + "</td>"); //3
                    tr.append("<td>" + res.data[i].customer_name + "</td>"); //4
                    tr.append("<td>" + res.data[i].callinteraction_id + "</td>");//5
                    tr.append("<td>" + res.data[i].psfFollowUpTime + "</td>");//6
                    tr.append("<td>" + res.data[i].psfLoginUser + "</td>");//7

                    $('#tblforfollowuptemppsf').append(tr);
                    if (res.data[i].psfFollowUpTime != null) {

                        console.log("existDate :" + existDate + " res.data[i].psfFollowUpTime " + res.data[i].psfFollowUpTime);
                        if (existDate != res.data[i].psfFollowUpTime) {

                            var now = new Date();

                            console.log("not same both times" + res.data[i].psfFollowUpTime);
                            var fupTime = toDate(res.data[i].psfFollowUpTime, 'H:MM')
                            var millisTill10 = new Date(now.getFullYear(), now.getMonth(), now.getDate(), fupTime.getHours(), fupTime.getMinutes(), 0, 0) - now;
                            console.log(" millisTill10 " + millisTill10 + " i is " + i);
                            if (millisTill10 > 0)
                            {
                                setTimeout(function ()
                                { showPSFNotify(); }, millisTill10);

                            }

                        } else {
                            console.log("same as previous  both times");

                            var now = new Date();
                            now.setMinutes(now.getMinutes() + 3);
                            console.log("the +3 time is :" + now + " res.data[i].psfFollowUpTime is " + res.data[i].psfFollowUpTime);

                            var fupTime = toDate(res.data[i].psfFollowUpTime, 'H:MM')
                            var millisTill10 = new Date(now.getFullYear(), now.getMonth(), now.getDate(), fupTime.getHours(), fupTime.getMinutes(), 0, 0) - now;

                            console.log(" millisTill10 " + millisTill10 + " i is " + i);

                            if (millisTill10 > 0) {
                                setTimeout(function () { showPSFNotify(); }, millisTill10);
                            }


                        }

                        existDate = res.data[i].psfFollowUpTime;
                    }
                }
            }
            else {
                alert(res.error);
            }
        }, error(error) {

        }
    });
}

//PSF Notification
function showPSFNotify() {
    //alert("showPSFNotify");

    var d = new Date();
    var h = addZero(d.getHours());
    var m = addZero(d.getMinutes());

    var fupTime = h + ":" + m;

    var table = document.getElementById("tblforfollowuptemppsf");
    for (var i = 0, row; row = table.rows[i]; i++) {
      // alert("psf"+row.cells[15].innerHTML);
       //alert("psf"+fupTime);
       
        var onClkUrl;
        //patter CRE/Customer_is/Vehicle_is/psfassign_id/0/campaign

        if (row.cells[7].innerHTML == "PSF") {
            onClkUrl = `${siteRoot}/Home/redirectToCallLogging/CRE%2c${row.cells[0].innerHTML}%2c${row.cells[1].innerHTML}%2c${row.cells[2].innerHTML}%2c${0}%2c${row.cells[3].innerHTML}`
        }
        else {
            onClkUrl = `${siteRoot}/Home/redirectToCallLogging/CRE%2c${row.cells[0].innerHTML}%2c${row.cells[1].innerHTML}%2c${row.cells[2].innerHTML}%2c${900}%2c${0}`
        }
        
        if (fupTime === row.cells[6].innerHTML) {
            Lobibox_notify('info', {
                delay: 24 * 5000,
                msg: 'PSF Follow Up is required for ' + row.cells[4].innerHTML + "",
                onClickUrl: onClkUrl
            });
        }
    }
}
//Insurance FollowUp notification
function ajaxRequestForInsurancePopupNotificationCRE() {
    var url = siteRoot + "/Home/getInsuranceNotificationToday";
    $.ajax({
        type: 'POST',
        url: url,
        datatype: 'json',
        data: {},
        cache: false,
        success: function (res) {
            if (res.success == true) {
                console.log(" data.length :" + res.data.length);

                console.log(res);
                $("#followUpCountOfTodayIns").html(res.data.length);
                var existDate = '';
                for (i = 0; i < res.data.length; i++) {
                    tr = $('<tr/>');
                    tr.append("<td>" + res.data[i].callInteraction_id + "</td>");
                    tr.append("<td>" + res.data[i].dealerCode + "</td>");
                    tr.append("<td>" + res.data[i].customerName + "</td>");
                    tr.append("<td>" + res.data[i].followUpTime + "</td>");
                    tr.append("<td>" + res.data[i].vehicle_vehicle_id + "</td>");
                    tr.append("<td>" + res.data[i].customer_id + "</td>");
                    $('#tblforfollowuptempinsurance').append(tr);
                    if (res.data[i].followUpTime != null) {


                        console.log("existDate :" + existDate + " res.data[i].followUpTime " + res.data[i].followUpTime);
                        if (existDate != res.data[i].followUpTime) {

                            var now = new Date();

                            console.log("not same both times" + res.data[i].followUpTime);
                            var fupTime = toDate(res.data[i].followUpTime, 'H:MM')
                            var millisTill10 = new Date(now.getFullYear(), now.getMonth(), now.getDate(), fupTime.getHours(), fupTime.getMinutes(), 0, 0) - now;
                            console.log(" millisTill10 " + millisTill10 + " i is " + i);
                            if (millisTill10 > 0) {

                                setTimeout(function () { showInsuranceNotify(); }, millisTill10);
                            }
                        } else {
                            console.log("same as previous  both times");

                            var now = new Date();
                            now.setMinutes(now.getMinutes() + 3);
                            console.log("the +3 time is :" + now + " res.data[i].followUpTime is " + res.data[i].followUpTime);

                            var fupTime = toDate(res.data[i].followUpTime, 'H:MM')
                            var millisTill10 = new Date(now.getFullYear(), now.getMonth(), now.getDate(), fupTime.getHours(), fupTime.getMinutes(), 0, 0) - now;

                            console.log(" millisTill10 " + millisTill10 + " i is " + i);

                            if (millisTill10 > 0) {
                                setTimeout(function () {
                                    showInsuranceNotify();
                                }, millisTill10);
                            }
                        }
                        existDate = res.data[i].followUpTime;
                    }
                }
            }
            else {
                alert(res.error);
            }
        }, error(error) {

        }
    });
}

function showInsuranceNotify() {
    var d = new Date();
    var h = addZero(d.getHours());
    var m = addZero(d.getMinutes());

    var fupTime = h + ":" + m;

    var table = document.getElementById("tblforfollowuptempinsurance");
    for (var i = 0, row; row = table.rows[i]; i++) {

        //alert("insurance"+row.cells[3].innerHTML);
        //alert("insurance fupTime "+fupTime);
        if (fupTime === row.cells[3].innerHTML) {
            Lobibox_notify('info', {
                delay: 24 * 5000,
                msg: 'Insurance Follow Up is required for ' + row.cells[2].innerHTML,
                onClickUrl: siteRoot + "/Home/redirectToCallLogging/CRE%2cI%2c" + row.cells[5].innerHTML + "%2c" + row.cells[4].innerHTML
            });
        }
    }


}


//supporting function
function addZero(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}

function toDate(dStr, format) {
    var now = new Date();
    if (format == "H:MM") {
        now.setHours(dStr.substr(0, dStr.indexOf(":")));
        now.setMinutes(dStr.substr(dStr.indexOf(":") + 1));
        now.setSeconds(0);
        return now;
    } else
        return "Invalid Format";
}