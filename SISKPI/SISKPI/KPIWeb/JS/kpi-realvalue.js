
$(function () {
   
    //$('#gvReal').Scrollable({
    //    //ScrollHeight: 500;
    //    ScrollHeight: document.documentElement.clientHeight - 136 
    //});
    var selectId = $('#hidSelectedItem').val();
    if (selectId != "0") {
        $('#gvReal tr[id=' + selectId + ']').addClass("datagrid-row-select");
    }

    $('#gvReal tr[id]').click(function () {
        $('#gvReal tr[id]').removeClass("datagrid-row-select");
        $(this).addClass("datagrid-row-select");
        $('#hidSelectedItem').val($(this).attr("id"));
    });

    $('#btnView').click(function () {
        selectId = $('#hidSelectedItem').val();
        if (selectId != "0") {
            window.open('KPI_RealTrend.aspx?RealID=' + selectId + '', 'newwindow', 'width=800,height=600,scrollbars=yes,top=10,left=200');
        }
    });

});