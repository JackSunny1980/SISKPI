/// <reference path="../KPIAlarm/Script/jquery-1.10.2-vsdoc.js" />
$(document).ready(function () {
    var flag = "";
    var hidSelectPerson = $('#hidSelectPerson'),
        hidPosition = $('#hidPosition'),
        txtPosition = $('#txtPosition'),
        hidSelectedPlant = $('#hidSelectedPlant'),
        hidSelectedShift = $('#hidSelectedShift'),
        hidSelectedShiftName = $('#hidSelectedShiftName'),
        hidSelectedTeamPerson = $('#hidSelectedTeamPerson'),
        allPersonPartFields = $([]).add(hidSelectPerson).add(hidPosition).add(txtPosition);
    $('#DialogPanel').dialog({
        autoOpen: false,
        width: 350,
        bgiframe: true,
        modal: true
    });

    $('#MessageDialog').dialog({
        autoOpen: false,
        width: 350,
        bgiframe: true,
        modal: true
    });

    $("#btnCancel").click(function () {
        checkSelectedItem();
        settingToolState();
        $('#DialogPanel').dialog("close");
    });
    $("#btnDeleteCancel").click(function () {
        $('#MessageDialog').dialog("close");
    });

    $('#btnCreate').click(function () {
        $('#DialogPanel').dialog({ title: "新增超限配置信息" });
        clearForm();
        $("input[name='chkItem']").each(function () {
            $(this).attr("checked", false);
        });
        flag = "INSERT";
        $('#hidFlag').val(flag);
        $('#DialogPanel').dialog("open");
    });

    $('#btnUpdate').click(function () {
        flag = "UPDATE";
        if (BindSelectedItem()) {
            $('#DialogPanel').dialog({ title: "编辑班组配置信息" });
            $('#hidFlag').val(flag);
            $('#DialogPanel').dialog("open");
        } else {
            var tips = $("#messageTips");
            $('#MessageDialog').dialog({ title: "编辑班组配置信息" });
            tips.text("请选择要编辑的信息？").addClass("ui-state-highlight");
            $('#MessageDialog').dialog("open"); 
         }
    });

    $('#chbSelectAll').click(function () {
        var o = this;
        $("input[name='chkItem']").each(function () {
            $(this).attr("checked", o.checked);
        });
    });

    $("input[name='chkItem']").click(function () {

        var selectNum = checkSelectedItem();
        var o = this;
        if (!o.checked) {
            $('#chbSelectAll').attr("checked", false);
        } else {
            if (selectNum == 0 || selectNum < 10) {
                $('#chbSelectAll').attr("checked", false);
            } else if (selectNum == 10) {
                $('#chbSelectAll').attr("checked", true);
            }
        }

    });

    $('#dropPersonList').change(function () {
        var selectedValue = $(this).val();
        var aryItems = selectedValue.split("/");
        if (aryItems.length == 3) {
            hidSelectPerson.val(aryItems[0]);
            hidPosition.val(aryItems[1]);
            txtPosition.val(aryItems[2]);
        } else {
            allPersonPartFields.val("");
        }
    });

    $('#dropPlantList').change(function () {
        if ($(this).val() != "P01") {
            hidSelectedPlant.val($(this).val());
        } else {
            hidSelectedPlant.val("");
        }
    });

    $('#dropShiftList').change(function () {
        if ($(this).val() != "S01") {
            hidSelectedShift.val($(this).val());
            hidSelectedShiftName.val($(this).text());
        } else {
            hidSelectedShift.val("");
            hidSelectedShiftName.val("");
        }
    });

    $('#dropTeamPersonList').change(function () {
        if ($(this).val() != "TP01") {
            hidSelectedTeamPerson.val($(this).val());
        } else {
            hidSelectedTeamPerson.val("");
        }
    });

    $('#btnDelete').click(function () {
        $('#hidFlag').val('');
        deleteTips();
    });

    $('#btnClear').click(function () {
        flag = "CLEAR";
        $('#hidFlag').val(flag);
        deleteAllData();
    });

    settingToolState();
});

function selectedRow(obj) {
    var controlID = "#" + obj.id;
    $('#hidCode').val(obj.value);
    $(controlID).parent().nextAll().children("label").each(function () {
        var idValue = this.id;
        switch ($(this).attr("for")) {
            case "PlantName":
                $("#dropPlantList").val(idValue);
                $('#hidSelectedPlant').val(idValue);
                return;
            case "ShiftName":
                $("#dropShiftList").val(idValue);
                $('#hidSelectedShift').val(idValue);
                $('#hidSelectedShiftName').val($(this).html());
                return;
            case "PersonName":
                $("#dropPersonList").val($(this).parent().find(":hidden").val());
                $('#hidSelectPerson').val(idValue);
                return;
            case "PositionName":
                $("#txtPosition").val($(this).html());
                $('#hidPosition').val(idValue);
                return;
            case "TeamPersonName":
                $("#dropTeamPersonList").val(idValue);
                $('#hidSelectedTeamPerson').val(idValue);
                return;
            case "TeamNote":
                $("#txtTeamNote").val($(this).html());
                return;
        }
    });
}

function checkSelectedItem() {
    var checkednum = $("input[name='chkItem']:checked").not("#chbSelectAll").length;

    if (checkednum == 0) {
        $('#btnUpdate').attr("disabled", "disabled");
        $('#btnDelete').attr("disabled", "disabled");
    } else if (checkednum > 1) {
        $('#btnUpdate').attr("disabled", "disabled");
        $('#btnDelete').removeAttr("disabled");
    } else {
        $('#btnUpdate').removeAttr("disabled");
        $('#btnDelete').removeAttr("disabled");
    }
    return checkednum;
}

function BindSelectedItem() {
    var checkedItem = $("input[name='chkItem']:checked").not("#chbSelectAll");
    var checkednum = checkedItem.length;

    if (checkednum==1)
    {
        selectedRow(checkedItem[0]);
        return true;
    }
    return false;
 }

function settingToolState() {
    $('#btnUpdate').attr("disabled", "disabled");
    $('#btnDelete').attr("disabled", "disabled");
}

function updateTips(t) {
    var tips = $("#diaglogTips");
    tips.text(t).addClass("ui-state-highlight");
    setTimeout(function () {
        tips.removeClass("ui-state-highlight", 1500);
    }, 500);
}

function checkForm() {
    var dropPlantList = $('#dropPlantList'),
        dropShiftList = $('#dropShiftList'),
        dropPersonList = $('#dropPersonList'),
        dropTeamPersonList = $('#dropTeamPersonList');

    if (dropPlantList.val() == "P01") {
        dropPlantList.addClass("ui-state-error");
        updateTips("请选择单元组！");
        return false;
    }

    if (dropShiftList.val() == "S01") {
        dropShiftList.addClass("ui-state-error");
        updateTips("请选择运行值！");
        return false;
    }

    if (dropPersonList.val() == "PE01") {
        dropPersonList.addClass("ui-state-error");
        updateTips("请选择人员！");
        return false;
    }

    if (dropTeamPersonList.val() == "TP01") {
        dropTeamPersonList.addClass("ui-state-error");
        updateTips("请选择替班人员！");
        return false;
    }
    $("#DialogPanel").dialog("close");
    return true;
}

function clearForm() {
    var dropPlantList = $('#dropPlantList'),
        hidSelectedPlant = $('#hidSelectedPlant'),
        dropShiftList = $('#dropShiftList'),
        hidSelectedShift = $('#hidSelectedShift'),
        hidSelectedShiftName = $('#hidSelectedShiftName'),
        dropPersonList = $('#dropPersonList'),
        hidSelectPerson = $('#hidSelectPerson'),
        dropTeamPersonList = $('#dropTeamPersonList'),
        hidSelectedTeamPerson = $('#hidSelectedTeamPerson'),
        hidPosition = $('#hidPosition'),
        txtPosition = $('#txtPosition'),
        txtTeamNote = $('#txtTeamNote'),
        allFields = $([]).add(hidSelectedPlant).add(hidSelectedShift).add(hidSelectedShiftName).add(hidSelectPerson).add(hidSelectedTeamPerson).add(hidPosition).add(txtPosition).add(txtTeamNote);
    allFields.val("").removeClass("ui-state-error");
    dropPlantList.val("P01");
    dropShiftList.val("S01");
    dropPersonList.val("PE01");
    dropTeamPersonList.val("TP01");
}

function deleteTips() {
    var tips = $("#messageTips");
    $('#MessageDialog').dialog({ title: "删除班组配置信息" });
    if (BindSelectedItem()) {
        tips.text("你确定要删除选择的信息？").addClass("ui-state-highlight");
    } else {
        tips.text("请选择要删除选择的信息？").addClass("ui-state-highlight");
    }
    $('#MessageDialog').dialog("open");
}

function deleteSelectedItems() {
    if ($('#hidFlag').val() != "CLEAR") {
        var checkednum = $("input[name='chkItem']:checked").length;
        var multipleCode = $("#hidMultipleCode"),
        hidCode = $('#hidCode');
        multipleCode.val("");
        if (checkednum > 1) {
            $("#hidMultipleCode").val("");
            var strId = '';
            $("input[name='chkItem']:checked").each(function () {
                strId += '&' + $(this).val();
            });
            multipleCode.val(strId);
            hidCode.val("");
        }
    }
}

function deleteAllData() {
    var tips = $("#messageTips");
    $('#MessageDialog').dialog({ title: "删除班组配置信息" });
    tips.text("你确定要清空所有的数据信息！").addClass("ui-state-highlight");
    $('#MessageDialog').dialog("open");
}