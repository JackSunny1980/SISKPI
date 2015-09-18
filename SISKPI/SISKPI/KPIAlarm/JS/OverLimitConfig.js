/// <reference path="../Script/jquery-1.10.2-vsdoc.js" />

$(document).ready(function () {
    var flag = "";
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
        settingToolState();
        checkSelectedItem();
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
            $('#DialogPanel').dialog({ title: "编辑超限配置信息" });
            $('#hidFlag').val(flag);
            $('#DialogPanel').dialog("open");
        } else {
            var tips = $("#messageTips");
            $('#MessageDialog').dialog({ title: "编辑班组配置信息" });
            tips.text("请选择要编辑的信息？").addClass("ui-state-highlight");
            $('#MessageDialog').dialog("open");
        }
        
        
    });

    $('#btnDelete').click(function () {
        deleteTips();
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
            var rows = $('.GridViewRowStyle').length + $('.GridViewAlternatingRowStyle').length;
            if (selectNum == 0 || selectNum < rows) {
                $('#chbSelectAll').attr("checked", false);
            } else if (selectNum == rows) {
                $('#chbSelectAll').attr("checked", true);
            }
        }
    });

    $('#dropRealTagList').change(function () {
        $('#selectedValue').val($(this).val());
    });

    settingToolState();
});



function checkSearch() {
    var selectedUnit = $('#txtSearchKey').val();

    if (selectedUnit == "") {
        alert("请输入需要查询的标签名称。");
        return false
    }
    return true;
}

function updateTips(t) {
    var tips = $("#diaglogTips");
    tips.text(t).addClass("ui-state-highlight");
    setTimeout(function () {
        tips.removeClass("ui-state-highlight", 1500);
    }, 500);
}

function selectedRow(obj) {
    var controlID = "#" + obj.id;
    $('#hidCode').val(obj.value);
    $(controlID).parent().nextAll().children("label").each(function () {
        switch ($(this).attr("for")) {
            case "RealDesc":
                var idValue = this.id;
                $("#dropRealTagList").val(idValue);
                $('#selectedValue').val(idValue);
                return;
            case "First":
                $("#txtFirstLimiting").val($(this).html());
                return;
            case "Second":
                $("#txtSecondLimiting").val($(this).html());
                return;
            case "Third":
                $("#txtThirdLimiting").val($(this).html());
                return;
            case "Comment":
                $("#txtComment").val($(this).html());
                return;
        }
    });
}

function clearForm() {
    var tagName = $("#dropRealTagList"),
        firstLimiting = $("#txtFirstLimiting"),
        secondLimiting = $("#txtSecondLimiting"),
        thirdLimiting = $("#txtThirdLimiting"),
        hidFlag = $('#hidFlag'),
        hidCode = $('#hidCode'),
        comment = $("#txtComment"),
        allFields = $([]).add(firstLimiting).add(secondLimiting).add(thirdLimiting).add(hidFlag).add(hidCode).add(comment);
    allFields.val("").removeClass("ui-state-error");
    tagName.val("T01");
}

function checkForm() {
    var tagName = $("#dropRealTagList"),
         firstLimiting = $("#txtFirstLimiting"),
         secondLimiting = $("#txtSecondLimiting"),
         thirdLimiting = $("#txtThirdLimiting"),
         allFields = $([]).add(tagName).add(firstLimiting).add(secondLimiting);
         


    var regexp = /^\+{0,1}[0-9]{0,}\.{0,1}[0-9]{0,3}$/;
    if (tagName.val() == "T01") {
        tagName.addClass("ui-state-error");
        updateTips("请选择标签名称！");
        return false;
    }

    if (firstLimiting.val() != "" && !(regexp.test(firstLimiting.val()))) {
        firstLimiting.addClass("ui-state-error");
        updateTips("请输入有效的一限值！");
        return false;
    }

    if (secondLimiting.val() != "" && !(regexp.test(secondLimiting.val()))) {
        secondLimiting.addClass("ui-state-error");
        updateTips("请输入有效的二限值！");
        return false;
    }

    if (thirdLimiting.val() != "" && !(regexp.test(thirdLimiting.val()))) {
        thirdLimiting.addClass("ui-state-error");
        updateTips("请输入有效的三限值！");
        return false;
    }

    $("#DialogPanel").dialog("close");
    return true;
}

function deleteTips() {
   
    var tips = $("#messageTips");
    $('#MessageDialog').dialog({ title: "删除超限配置信息" });
    if (BindSelectedItem()) {
        tips.text("你确定要删除选择的信息？").addClass("ui-state-highlight");
    } else {
        tips.text("请选择要删除选择的信息？").addClass("ui-state-highlight");
    }
    $('#MessageDialog').dialog("open");
}

function deleteSelectedItems() {
    var checkednum = $("input[name='chkItem']:checked").length;
    var multipleCode = $("#hidMultipleCode"),
        hidCode=$('#hidCode');
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

function checkSelectedItem() {
    var checkednum = $("input[name='chkItem']:checked").length;

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

function settingToolState() {
    $('#btnUpdate').attr("disabled", "disabled");
    $('#btnDelete').attr("disabled", "disabled");
}
function BindSelectedItem() {
    var checkedItem = $("input[name='chkItem']:checked").not("#chbSelectAll");
    var checkednum = checkedItem.length;

    if (checkednum == 1) {
        selectedRow(checkedItem[0]);
        return true;
    }
    return false;
}