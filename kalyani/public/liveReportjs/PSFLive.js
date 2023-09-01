function clearFilter() {
    $('#creLists').multiselect("deselectAll", false).multiselect("refresh");
    $('#Campaignid').multiselect("deselectAll", false).multiselect("refresh");
    $('#LiveWorkshop').multiselect("deselectAll", false).multiselect("refresh");
}
function ReloadPSFData() {
    if ($.fn.DataTable.isDataTable("#tblcallPSFCallScoreCardLive"))
    {
        var table = $("#tblcallPSFCallScoreCardLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblcallPSFCallScoreCardLive thead th").remove();
        callPSFprocedure(9,"tblcallPSFCallScoreCardLive");
    }
    if ($.fn.DataTable.isDataTable("#tblPSFDataAvailLive")) {
        var table = $("#tblPSFDataAvailLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblPSFDataAvailLive thead th").remove();
        callPSFprocedure(10,'tblPSFDataAvailLive');
    }
    if ($.fn.DataTable.isDataTable("#tblPSFIntradayLive")) {
        var table = $("#tblPSFIntradayLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblPSFIntradayLive thead th").remove();
        callPSFprocedure(11,'tblPSFIntradayLive');
    }
}
function callPSFprocedure(Id, tblId)
{

    var selected_CRE = $('#creLists option:selected').toArray().map(item => item.value).join();
    var selected_Campaign = $('#Campaignid option:selected').toArray().map(item => item.value).join();
    var selected_Workshop = $('#LiveWorkshop option:selected').toArray().map(item => item.value).join();
    var reportId = Id;
    var parameters = { selected_CRE: selected_CRE, selected_Workshop: selected_Workshop, reportId: reportId, selected_Campaign: selected_Campaign };

        if (!$.fn.DataTable.isDataTable("#" + tblId)) {
            {
                $("#" + tblId + "_button").hide();
                callPSFCallScoreCardLive_tabledata(parameters, tblId);
            }
        }
}
function getPSFTableData(parameters) {
    return $.ajax({
        "url": siteRoot + "/PSFReports/getPSFReports",
        "type": "POST",
        "data": { PSFData: JSON.stringify(parameters) },
        "datatype": "json"
    });
}
function callPSFCallScoreCardLive_tabledata(parameters, tblId) {
    $('#' + tblId + '_Spinner').show();
    getPSFTableData(parameters).done(function (records) {
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
function getPSFIntradaycharts() {

    var selected_CRE = $('#creLists option:selected').toArray().map(item => item.value).join();
    var selected_Campaign = $('#Campaignid option:selected').toArray().map(item => item.value).join();
    var selected_Workshop = $('#LiveWorkshop option:selected').toArray().map(item => item.value).join();
    var reportId = 12;
    var parameters = { selected_CRE: selected_CRE, selected_Workshop: selected_Workshop, reportId: reportId, selected_Campaign: selected_Campaign };
    $('#barException').text("");


    $.ajax({
        type: "POST",
        url: siteRoot + "/PSFReports/getPSFChart",
        data: { PSFbarData: JSON.stringify(parameters) },
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

                $('#psfbarChart').remove();
                $('iframe.chartjs-hidden-iframe').remove();
                $('#psfcanvascontainer').append('<canvas id="psfbarChart"><canvas>');

                var ctx = $("#psfbarChart").get(0).getContext("2d");
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
                $('#psfbarChartDiv').show();

            }
            else {
                $('#psfbarChartDiv').hide();
                $('#barException').text(results.exception);
            }
        }
    });
}
