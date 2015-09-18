$(function () {

    //$('#gvList').Scrollable({
    //    ScrollHeight: document.documentElement.clientHeight - 136 
    //});

    var selectId = $('#hidSelectedItem').val();
    if (selectId != "0") {
        $('#gvList tr[id=' + selectId + ']').addClass("datagrid-row-select");
    }

    $('#gvList tr[id]').click(function () {
        $('#gvList tr[id]').removeClass("datagrid-row-select");
        $(this).addClass("datagrid-row-select");
        $('#hidSelectedItem').val($(this).attr("id"));
    });

    $('#btnDetail').click(function () {
        selectId = $('#hidSelectedItem').val();
        var searchTime = $('#txtYearMonth').val();
        if (selectId != "0") {
            window.open('PersonScoreDetail.aspx?personId=' + selectId + '&searchTime=' + searchTime + '', 'newwindow', 'width=800,height=600,scrollbars=yes,top=10,left=200');
        }
    });
});