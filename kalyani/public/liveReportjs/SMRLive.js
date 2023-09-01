function clearsmrfilters() {
    $('#smrCres').multiselect("deselectAll", false).multiselect("refresh");
    $('#smrCampaignid').multiselect("deselectAll", false).multiselect("refresh");
    $('#smrLiveWorkshop').multiselect("deselectAll", false).multiselect("refresh");
    $('#smrfromDate').val('');
    $('#toDate').val('');
}

function smrFilterrefresh() {

    if ($.fn.DataTable.isDataTable("#tblFordCRECallLive")) {
        var table = $("#tblFordCRECallLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblFordCRECallLive thead th").remove();
        callSMRProcedure(10, 'tblFordCRECallLive');
    }
    if ($.fn.DataTable.isDataTable("#tblFordIntraDaySMRLive")) {
        var table = $("#tblFordIntraDaySMRLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblFordIntraDaySMRLive thead th").remove();
        callSMRProcedure(11, 'tblFordIntraDaySMRLive');
    }
    if ($.fn.DataTable.isDataTable("#tblFordCampaignSMRLive")) {
        var table = $("#tblFordCampaignSMRLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblFordCampaignSMRLive thead th").remove();
        callSMRProcedure(12, 'tblFordCampaignSMRLive');
    }
    if ($.fn.DataTable.isDataTable("#tblFordLocationSMRLive")) {
        var table = $("#tblFordLocationSMRLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblFordLocationSMRLive thead th").remove();
        callSMRProcedure(13, 'tblFordLocationSMRLive');
    }
    if ($.fn.DataTable.isDataTable("#tblFordDataAvailSMRLive")) {
        var table = $("#tblFordDataAvailSMRLive").DataTable();
        table.clear();
        table.destroy(); $("#tblFordDataAvailSMRLive thead th").remove();
        callSMRProcedure(14, 'tblFordDataAvailSMRLive');
    }
    if ($.fn.DataTable.isDataTable("#tblOverDueBookingsLive")) {
        var table = $("#tblOverDueBookingsLive").DataTable();
        table.clear();
        table.destroy(); $("#tblOverDueBookingsLive thead th").remove();
        callSMRProcedure(15, 'tblOverDueBookingsLive');
    }
    if ($.fn.DataTable.isDataTable("#tblFollowUpCallsLive")) {
        var table = $("#tblFollowUpCallsLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblFollowUpCallsLive thead th").remove();
        callSMRProcedure(16, 'tblFollowUpCallsLive');
    }
    if ($.fn.DataTable.isDataTable("#tblFordNthDaySMRLive")) {
        var table = $("#tblFordNthDaySMRLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblFordNthDaySMRLive thead th").remove();
        callSMRProcedure(17, 'tblFordNthDaySMRLive');
    }
    if ($.fn.DataTable.isDataTable("#tblFordNminusOneSMRLive")) {
        var table = $("#tblFordNminusOneSMRLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblFordNminusOneSMRLive thead th").remove();
        callSMRProcedure(18, 'tblFordNminusOneSMRLive');
    }
    if ($.fn.DataTable.isDataTable("#tblFordNthDaySMRCRELive")) {
        var table = $("#tblFordNthDaySMRCRELive").DataTable();
        table.clear();
        table.destroy();
        $("#tblFordNthDaySMRCRELive thead th").remove();
        callSMRProcedure(19, 'tblFordNthDaySMRCRELive');
    }
    if ($.fn.DataTable.isDataTable("#tblFordNminusOneSMRCRELive")) {
        var table = $("#tblFordNminusOneSMRCRELive").DataTable();
        table.clear();
        table.destroy();
        $("#tblFordNminusOneSMRCRELive thead th").remove();
        callSMRProcedure(20, 'tblFordNminusOneSMRCRELive');
    }
    if ($.fn.DataTable.isDataTable("#tblcallhistorySMRLive")) {
        var table = $("#tblcallhistorySMRLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblcallhistorySMRLive thead th").remove();
        callSMRProcedure(21, 'tblcallhistorySMRLive');
    }
}

function callSMRProcedure(Id, tblId) {
    var selected_CRE = $('#smrCres option:selected').toArray().map(item => item.value).join();
    var selected_Workshop = $('#smrLiveWorkshop option:selected').toArray().map(item => item.value).join();
    var selected_Campaign = $('#smrCampaignid option:selected').toArray().map(item => item.value).join();
    var fromdate = $('#smrfromDate').val();
    var todate = $('#smrtoDate').val();
    var reportId = Id;
    var parameters = { selected_CRE: selected_CRE, selected_Workshop: selected_Workshop, reportId: reportId, selected_Campaign: selected_Campaign, fromDate: fromdate, toDate: todate };

    if (!$.fn.DataTable.isDataTable("#" + tblId)) {
        {
            $("#" + tblId + "_button").hide();
            callSMRLiveTable(parameters, tblId);
        }
    }
}
function getSMRTableData(parameters) {
return $.ajax({
        "url": siteRoot + "/SMRLive/getSMRReports",
        "type": "POST",
        "data": { smrData: JSON.stringify(parameters) },
        "datatype": "json"
    });
}
function callSMRLiveTable(parameters, tblId) {
    $('#' + tblId + '_Spinner').show();
    getSMRTableData(parameters).done(function (records) {
        var jsonrecords = JSON.parse(records.data);
        var my_columns = [];
        if (records.exception === "" && jsonrecords.length > 0) {
            $.each(jsonrecords[0], function (key, value) {
                var my_item = {};
                my_item.data = key;
                my_item.title = key;
                my_columns.push(my_item);
            });

            $('#' + tblId).DataTable({
                data: jsonrecords,
                "columns": my_columns,
                "paging": false,
                "searching": false,
                "order": [[0, "asc"]],
                "pageLength": 50,
                "initComplete": function (settings, exception) {
                    $('#' + tblId + '_Spinner').hide();
                    $("#" + tblId + "_button").show();

                }

            });
            var buttons = new $.fn.dataTable.Buttons('#' + tblId, {
                buttons: [
                    {
                        extend: 'csvHtml5',
                        text: '<i class="fa fa-download">&nbsp;&nbsp;Excel</i>',
                        titleAttr: 'Excel',
                        filename: tblId

                    }
                ]
            }).container().appendTo($('#' + tblId + '_xl'));
        }
        else
        {
            var dataset = [{ "Result": "No Data Available" }];

            var my_columns = [];
            $.each(dataset[0], function (key, value) {
                var my_item = {};
                my_item.data = key;
                my_item.title = key;
                my_columns.push(my_item);
            });

            $('#' + tblId).DataTable({
                data: dataset,
                "columns": my_columns,
                "paging": false,
                "searching": false,
                "order": [[0, "asc"]],
                "pageLength": 50,
                "initComplete": function (settings, exception) {
                    $('#' + tblId + '_Spinner').hide();
                    $("#" + tblId + "_button").show();

                }
            });
            $('#tblException').text(records.exception);
        }

    });
}
function getTodaysCREReports() {

    var workshope_list = $('#creworkshops option:selected').toArray().map(item => item.value).join();

    $.ajax({
        url: siteRoot + '/SMRLive/todayCrecallReport/',
        type: "GET",
        data: { workshops: workshope_list, moduletype: 1 },
        beforeSend: function () {
            $('#mainLoader').css({ 'display': 'block' });
        },
        success: function (result) {
            if (result.success == true) {
                //debugger
                var bytes = new Uint8Array(result.robj.FileContents);
                var blob = new Blob([bytes], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
                var link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob);
                link.download = "todayscallReport.xlsx";
                link.click();
            }
            else if (result.success == false) {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: result.error
                });
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            if (textStatus == 'Unauthorized') {
                alert('custom message. Error: ' + errorThrown);
            } else {
                alert('custom message. Error: ' + errorThrown);
            }
        },
        complete: function () {
            $("#creworkshops").multiselect('selectAll', false);
            $("#creworkshops").multiselect('updateButtonText')
            $('#mainLoader').css({ 'display': 'none' });
        },
        error: function (err) {
            console.log(err);
        }
    });

}
function getSMRIntradaycharts() {

    $('#barException').text("");

    var selected_CRE = $('#smrCres option:selected').toArray().map(item => item.value).join();
    var selected_Workshop = $('#smrLiveWorkshop option:selected').toArray().map(item => item.value).join();
    var selected_Campaign = $('#smrCampaignid option:selected').toArray().map(item => item.value).join();

    var reportId = 22;
    var parameters = { selected_CRE: selected_CRE, selected_Workshop: selected_Workshop, reportId: reportId, selected_Campaign: selected_Campaign };

    $.ajax({
        type: "POST",
        url: siteRoot + "/SMRLive/getSMRChart",
        data: { smrbarData: JSON.stringify(parameters) },
        dataType: "json",
        success: function (results) {
            if (results.success == true) {
                var aLabels = results.data[0];
                var aDatasets1 = results.data[1];
                var dataT = {
                    labels: aLabels,
                    datasets: [{
                        label: "Calls",
                        data: aDatasets1,
                        fill: false,
                        backgroundColor: "#4082c4",
                        borderColor: "black",
                        borderWidth: 1
                    }]
                };

                $('#SMRbarChart').remove();
                $('iframe.chartjs-hidden-iframe').remove();
                $('#smrcanvascontainer').append('<canvas id="SMRbarChart"><canvas>');

                var ctx = $("#SMRbarChart").get(0).getContext("2d");
                Chart.defaults.global.defaultFontColor = 'black';

                var myNewChart = new Chart(ctx, {
                    type: 'bar',
                    data: dataT,
                    options: {
                        title: { display: true, text: 'Intra Day Live Calls' },
                        legend: { position: 'bottom' },
                        "hover": {
                            "animationDuration": 0
                        },
                        "animation": {
                            "duration": 1,
                            "onComplete": function () {
                                var chartInstance = this.chart,
                                    ctx = chartInstance.ctx;

                                ctx.font = Chart.helpers.fontString(Chart.defaults.global.defaultFontSize, Chart.defaults.global.defaultFontStyle, Chart.defaults.global.defaultFontFamily);
                                ctx.textAlign = 'center';
                                ctx.textBaseline = 'bottom';

                                this.data.datasets.forEach(function (dataset, i) {
                                    var meta = chartInstance.controller.getDatasetMeta(i);
                                    meta.data.forEach(function (bar, index) {
                                        var data = dataset.data[index];
                                        ctx.fillText(data, bar._model.x, bar._model.y - 5);
                                    });
                                });
                            }

                        },
                        scales: {
                            xAxes: [{
                                gridLines: { display: false, zeroLineColor: '#ffcc33' }, display: true, scaleLabel: {
                                    display: true, labelString: ''
                                }, ticks: { fontcolor: 'black' }
                            }],
                            yAxes: [{ gridLines: { display: false, zeroLineColor: '#ffcc33' }, display: true, scaleLabel: { display: true, labelString: '' }, ticks: { beginAtZero: true, fontcolor: 'black' } }]
                        }

                    }
                });
                $('#SMRbarChartDiv').show();

            }
            else {
                $('#SMRbarChartDiv').hide();
                $('#barException').text(results.exception);
            }
        }
    });
}
