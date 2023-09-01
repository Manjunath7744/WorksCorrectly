function clearFilter() {
    $('#creLists').multiselect("deselectAll", false).multiselect("refresh");
    $('#Campaignid').multiselect("deselectAll", false).multiselect("refresh");
    $('#LiveWorkshop').multiselect("deselectAll", false).multiselect("refresh");

    $('#fromDate').val('');
    $('#toDate').val('');

}
function ReloadInsuranceData() {
    if ($.fn.DataTable.isDataTable("#tblInsuranceCRECallLive")) {
        var table = $("#tblInsuranceCRECallLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblInsuranceCRECallLive thead th").remove();
        callINSprocedure(13,'tblInsuranceCRECallLive');
    }


    if ($.fn.DataTable.isDataTable("#tblInsuranceIntraDaySMRLive")) {
        var table = $("#tblInsuranceIntraDaySMRLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblInsuranceIntraDaySMRLive thead th").remove();
        callINSprocedure(14,'tblInsuranceIntraDaySMRLive');
    }

    if ($.fn.DataTable.isDataTable("#tblInsuranceLocationLive")) {
        var table = $("#tblInsuranceLocationLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblInsuranceLocationLive thead th").remove();
        callINSprocedure(15,'tblInsuranceLocationLive');
    }

    if ($.fn.DataTable.isDataTable("#tblInsuranceCampaignSMRLive")) {
        var table = $("#tblInsuranceCampaignSMRLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblInsuranceCampaignSMRLive thead th").remove();
        callINSprocedure(16,'tblInsuranceCampaignSMRLive');
    }
    if ($.fn.DataTable.isDataTable("#tblInsuranceDataAvailSMRLive")) {
        var table = $("#tblInsuranceDataAvailSMRLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblInsuranceDataAvailSMRLive thead th").remove();
        callINSprocedure(17,'tblInsuranceDataAvailSMRLive');
    }
    if ($.fn.DataTable.isDataTable("#tblInsOverDueLive")) {
        var table = $("#tblInsOverDueLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblInsOverDueLive thead th").remove();
        callINSprocedure(18,'tblInsOverDueLive');
    }
    if ($.fn.DataTable.isDataTable("#tblInsFollowUpCallsLive")) {
        var table = $("#tblInsFollowUpCallsLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblInsFollowUpCallsLive thead th").remove();
        callINSprocedure(19,'tblInsFollowUpCallsLive');
    }
    if ($.fn.DataTable.isDataTable("#tblInsNthDayCallsLive")) {
        var table = $("#tblInsNthDayCallsLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblInsNthDayCallsLive thead th").remove();
        callINSprocedure(20,'tblInsNthDayCallsLive');
    }
    if ($.fn.DataTable.isDataTable("#tblInsNminusDayCallsLive")) {
        var table = $("#tblInsNminusDayCallsLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblInsNminusDayCallsLive thead th").remove();
        callINSprocedure(21,'tblInsNminusDayCallsLive');
    }
    if ($.fn.DataTable.isDataTable("#tblInscallhistoryLive")) {
        var table = $("#tblInscallhistoryLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblInscallhistoryLive thead th").remove();
        callINSprocedure(22,'tblInscallhistoryLive');
    }
    if ($.fn.DataTable.isDataTable("#tblIntradayInsuranceManager")) {
        var table = $("#tblIntradayInsuranceManager").DataTable();
        table.clear();
        table.destroy();
        $("#tblIntradayInsuranceManager thead th").remove();
        callINSprocedure(23,'tblIntradayInsuranceManager');
    }
    if ($.fn.DataTable.isDataTable("#tblndayinsfldapptmntstats")) {
        var table = $("#tblndayinsfldapptmntstats").DataTable();
        table.clear();
        table.destroy();
        $("#tblndayinsfldapptmntstats thead th").remove();
        callINSprocedure(24,'tblndayinsfldapptmntstats');
    }

}


function callINSprocedure(Id, tblId) {
    var selected_CRE = $('#creLists option:selected').toArray().map(item => item.value).join();
    var selected_Campaign = $('#Campaignid option:selected').toArray().map(item => item.value).join();
    var selected_Workshop = $('#LiveWorkshop option:selected').toArray().map(item => item.value).join();
    var fromdate = $('#fromDate').val();
    var todate = $('#toDate').val();
    var reportId = Id;
    var parameters = { selected_CRE: selected_CRE, selected_Workshop: selected_Workshop, reportId: reportId, selected_Campaign: selected_Campaign, fromDate: fromdate, toDate: todate };
    if (!$.fn.DataTable.isDataTable("#" + tblId)) {
        {
            $("#" + tblId + "_button").hide();
            callINSLiveTable(parameters, tblId);
        }
    }
}
function getINSTableData(parameters) {
    return $.ajax({
        "url": siteRoot + "/InsuranceLiveForm/getINSReports",
        "type": "POST",
        "data": { INSData: JSON.stringify(parameters) },
        "datatype": "json"
    });
}

function callINSLiveTable(parameters, tblId) {
    $('#' + tblId + '_Spinner').show();
    getINSTableData(parameters).done(function (records) {
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
        else {
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


function getTodaysINSCREReports() {

    var workshope_list = $('#INScreworkshops option:selected').toArray().map(item => item.value).join();

    $.ajax({

        url: siteRoot + '/SMRLive/todayCrecallReport/',
        type: "GET",
        data: { workshops: workshope_list, moduletype: 2 },
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
            $("#INScreworkshops").multiselect('selectAll', false);
            $("#INScreworkshops").multiselect('updateButtonText');
            $('#mainLoader').css({ 'display': 'none' });
        },
        error: function (err) {
            console.log(err);
        }
    });

}

function downloadINSReports() {
    var reportId, reportName;
    reportId = $('#INSbucketID').val();
    var name = $("#INSdwnldmodaltitle").text();
    reportName = name.replace(/[\. ,:-]+/g, "")
    var connectionId = $.connection.hub.id;

    var smsTYpe_list = $('#InsSmsinteractiontype option:selected').toArray().map(item => item.value).join();
    var smsStatus_list = $('#InsSmsinteractionstatus option:selected').toArray().map(item => item.value).join();
    var cre_list = $('#inscre option:selected').toArray().map(item => item.value).join();
    var workshope_list = $('#insworkshop option:selected').toArray().map(item => item.value).join();
    var campaign_list = $('#inscallcampaign option:selected').toArray().map(item => item.value).join();
    var callfromDate = $('#inscalldatefrom').val();
    var calltoDate = $('#inscalldateto').val();
    var smstype = $("input:radio[name=SendType]:checked").val()


    var downloadFilterLists = {};
    if (reportId == undefined) {
        reportId = 1;
        reportName = "Ndayappointmentdata";
    }
    downloadFilterLists = { reportId: reportId, workshopLists: workshope_list, campaignLists: campaign_list, creLists: cre_list, callfromDate: callfromDate, calltoDate: calltoDate, reportName: reportName, smstypeLists: smsTYpe_list, smsstatusLists: smsStatus_list, smstype: smstype, connectionId: connectionId  }

    if (callfromDate == "" || calltoDate == "") {
        Lobibox.notify('warning', {
            msg: 'Please Select  From and To Date Filters.'
        });
        return false;
    }

    $.when(getINSReports(downloadFilterLists)).then
        (function successHandler(res) {
            console.log(res);
            if (res.success == true) {
                window.location = siteRoot + "/InsuranceLiveForm/DownloadINSALL/?reportName=" + reportName;
            }
            else if (res.success == false) {
                Lobibox.notify('warning', {
                    continueDelayOnInactiveTab: true,
                    msg: res.error
                });
                return false;
            }
            else {
                console.log(res.error);
            }

        },
            function errorHandler(res) {
                console.log(err);
            })

        ;

}
function getINSReports(downloadFilterLists) {
    return $.ajax({
        url: siteRoot + '/InsuranceLiveForm/INSDownloadFilter/',
        type: "post",
        data: { INSdownloadFilters: JSON.stringify(downloadFilterLists) },
    });
}

function getINSIntradaycharts() {
    $('#barException').text("");
    var selected_CRE = $('#creLists option:selected').toArray().map(item => item.value).join();
    var selected_Campaign = $('#Campaignid option:selected').toArray().map(item => item.value).join();
    var selected_Workshop = $('#LiveWorkshop option:selected').toArray().map(item => item.value).join();
    var reportId = 24;
    var parameters = { selected_CRE: selected_CRE, selected_Workshop: selected_Workshop, reportId: reportId, selected_Campaign: selected_Campaign};


    $.ajax({
        type: "POST",
        url: siteRoot + "/InsuranceLiveForm/getINSChart",
        data: { INSbarData: JSON.stringify(parameters) },
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
                $('#INSbarChart').remove();
                $('iframe.chartjs-hidden-iframe').remove();
                $('#INScanvascontainer').append('<canvas id="INSbarChart"><canvas>');


                var ctx = $("#INSbarChart").get(0).getContext("2d");
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
                $('#PSFbarChartDiv').show();

            }
            else {
                $('#PSFbarChartDiv').hide();
                $('#barException').text(results.exception);
            }
        }
    });
}
