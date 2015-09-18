$(function () {
    //$('#gvSnapshot').Scrollable({
    //    //ScrollHeight: 500,
    //    ScrollHeight: document.documentElement.clientHeight - 160 
    //});
    var selectId = $('#hidSelectedItem').val();
    if (selectId != "0") {
        $('#gvSnapshot tr[id=' + selectId + ']').addClass("datagrid-row-select");
    }

    $('#gvSnapshot tr[id]').click(function () {
        $('#gvSnapshot tr[id]').removeClass("datagrid-row-select");
        $(this).addClass("datagrid-row-select");
        $('#hidSelectedItem').val($(this).attr("id"));
    });

    $('#btnSearch').click(function () {
        selectId = $('#hidSelectedItem').val();
        if (selectId != "0") {
            window.open('KPI_ECData.aspx?ECID=' + selectId + '', 'newwindow', 'width=800,height=600,scrollbars=yes,top=10,left=200');
        }
    });

    $('#btnTrack').click(function () {
        selectId = $('#hidSelectedItem').val();
        if (selectId != "0") {
            window.open('KPI_ECTrack.aspx?ECID=' + selectId + '', 'newwindow', 'width=800,height=600,scrollbars=yes,top=10,left=200');
        }
    });

    $('#btnAnalyze').click(function () {
        var queryTime = $('#hidQueryTime').val();
        if (queryTime != "0") {
            window.open('KPI_BarAnalyze.aspx?queryTime=' + queryTime + '', 'newwindow', 'width=800,height=600,scrollbars=yes,top=10,left=200');
        }
    });
});