$(function () {
    var selectId = $('#hidSelectedItem').val();
    if (selectId != "0") {
        $('#gvData tr[id=' + selectId + ']').addClass("datagrid-row-select");
        $('#gvData1 tr[id=' + selectId + ']').addClass("datagrid-row-select");
    }

    $('#gvData tr[id]').click(function () {
        $('#gvData tr[id]').removeClass("datagrid-row-select");
        $(this).addClass("datagrid-row-select");
        $('#hidSelectedItem').val($(this).attr("id"));
    });

    $('#gvData1 tr').click(function () {
        $('#gvData1 tr[id]').removeClass("datagrid-row-select");
        $(this).addClass("datagrid-row-select");
        $('#hidSelectedItem').val($(this).attr("id"));
    });

    $('#btn_Add').click(function () { $('#btnAdd').click(); });
    $('#btn_Edit').click(function () { $('#btnEdit').click(); });
    $('#btn_Del').click(function () { $('#btnDel').click(); });
    $('#btn_Select').click(function () { $('#btnSelect').click(); });
    $('#btn_Search').click(function () { $('#btnSearch').click(); });
    $('#btn_Export').click(function () { $('#btnExport').click(); });
    $('#btn_Return').click(function () { $('#btnReturn').click(); });

    //$('#btn_Add').click(function () {
    //    selectId = $('#hidSelectedItem').val();
    //    if (selectId != "0") {
    //        window.open('KPI_RealTrend.aspx?RealID=' + selectId + '', 'newwindow', 'width=800,height=600,scrollbars=yes,top=10,left=200');
    //    }
    //});

});