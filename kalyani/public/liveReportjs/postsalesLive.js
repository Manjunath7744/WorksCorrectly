function clearFilter() {
    $('#creLists').multiselect("deselectAll", false).multiselect("refresh");
    $('#Campaignid').multiselect("deselectAll", false).multiselect("refresh");
    $('#LiveWorkshop').multiselect("deselectAll", false).multiselect("refresh");
}
function ReloadPostSalesData() {
    if ($.fn.DataTable.isDataTable("#tblcallPostSalesCallLive")) {
        var table = $("#tblcallPostSalesCallLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblcallPostSalesCallLive thead th").remove();
        callPostSalesprocedure(9, "tblcallPostSalesCallLive");
    }
    if ($.fn.DataTable.isDataTable("#tblPostSalesIntradayLive")) {
        var table = $("#tblPostSalesIntradayLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblPostSalesIntradayLive thead th").remove();
        callPostSalesprocedure(10, 'tblPostSalesIntradayLive');
    }
    if ($.fn.DataTable.isDataTable("#tblPostSalesLocationLive")) {
        var table = $("#tblPostSalesLocationLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblPostSalesLocationLive thead th").remove();
        callPostSalesprocedure(11, 'tblPostSalesLocationLive');
    }
    if ($.fn.DataTable.isDataTable("#tblPostSalesFollowupLive")) {
        var table = $("#tblPostSalesFollowupLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblPostSalesFollowupLive thead th").remove();
        callPostSalesprocedure(12, 'tblPostSalesFollowupLive');
    }
    if ($.fn.DataTable.isDataTable("#tblPostSalesDataAvailLive")) {
        var table = $("#tblPostSalesDataAvailLive").DataTable();
        table.clear();
        table.destroy();
        $("#tblPostSalesDataAvailLive thead th").remove();
        callPostSalesprocedure(13, 'tblPostSalesDataAvailLive');
    }
    

}
function callPostSalesprocedure(Id, tblId) {

    var selected_CRE = $('#creLists option:selected').toArray().map(item => item.value).join();
    var selected_Campaign = $('#Campaignid option:selected').toArray().map(item => item.value).join();
    var selected_Workshop = $('#LiveWorkshop option:selected').toArray().map(item => item.value).join();
    var reportId = Id;
    var parameters = { selected_CRE: selected_CRE, selected_Workshop: selected_Workshop, reportId: reportId, selected_Campaign: selected_Campaign };

    if (!$.fn.DataTable.isDataTable("#" + tblId)) {
        {
            $("#" + tblId + "_button").hide();
            callPostSalesCallLive_tabledata(parameters, tblId);
        }
    }
}
function getPostSalesTableData(parameters) {
    return $.ajax({
        "url": siteRoot + "/PostSalesLive/getPostSales",
        "type": "POST",
        "data": { PostSalesData: JSON.stringify(parameters) },
        "datatype": "json"
    });
}
function callPostSalesCallLive_tabledata(parameters, tblId) {
    $('#' + tblId + '_Spinner').show();
    getPostSalesTableData(parameters).done(function (records) {
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

