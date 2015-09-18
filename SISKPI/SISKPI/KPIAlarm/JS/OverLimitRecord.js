
$(document).ready(function () {

    //$('#txtBeginTime').focus(function () {
    //    WdatePicker({ el: 'txtBeginTime', dateFmt: 'yyyy-MM-dd', skin: 'whyGreen' })
    //});
    //$('#txtEndTime').focus(function () {
    //    WdatePicker({ el: 'txtEndTime', dateFmt: 'yyyy-MM-dd', skin: 'whyGreen' })
    //});
    $('#dropUnityList').change(function () {
        var selectedValue = $('#dropUnityList').val();
        $('#selectedValue').val(selectedValue);
    });

});

function checkSearch() {
   // var txtSearchKey = $('#txtSearchKey').val();
    var selectedUnit = $('#dropUnityList').val();
    var beginTime = $('#txtBeginTime').val();
    var endTime = $('#txtEndTime').val();

//    if (txtSearchKey == "") {
//        alert("请输入指标信息。");
//        return false
//    }

    if (selectedUnit == "" || selectedUnit == "U01") {
        alert("请选择查询的机组信息。");
        return false
    }

    if (beginTime == "" || endTime == "") {
        alert("开始时间或者结束时间不能为空。");
        return false;
    }

    beginTime = new Date(Date.parse(beginTime.replace("-", "/")));
    endTime = new Date(Date.parse(endTime.replace("-", "/")));

    if (endTime < beginTime) {
        alert("结束时间不能小于开始时间");
        return false;
    }
    return true;
}