
function InputPercentBlur(obj) {
    var val = $('#' + obj).val();
    if (val != "") {
        if (val >= 100 || val <= 0) {
            alert('超限百分比只能输入0-100之间的数值！');
            $('#' + obj).val('');
            $('#' + obj).focus();
        }
    }
}
$(document).ready(function() {

    $('#rad_rep1').click(function() {
        changeTab(1);
    });
    $('#rad_rep2').click(function() {
        changeTab(2);
    });
    $('#rad_rep3').click(function() {
        changeTab(3);
    });
    $('#rad_rep4').click(function() {
        changeTab(4);
    });

    SetTableWidth('table1');
    SetDivWidth('div1');
});

//触发绑定tab
function changeTab(id) {
    $('#tabContent').triggerTab(id);

}

//默认执行函数
$(function() {
    $('#tabContent').tabs();
    //BindVals();
});

function bindRepMethod(obj) {
    o = obj;
    $("[name='rep']:radio").each(function(i) { this.checked = false; });
    $("#" + obj).attr("checked", true);
}

//服务器注册脚本事件调用附件脚本
function call() {
    $('#tabContent').tabs();
    $("[name='rblModel']:radio").each(function(i) { $(this).css({ 'border-width': '0px' }) });
    SetTableWidth('table1');
    SetDivWidth('div1');
    HighCheck();
    LowCheck();
    HotCheck();
    GoodCheck();
    ReplaceCheck();
}

//输入PE点移出触发事件
function InputLineBlur(f) {
    if ($("#txtDesignxLine2").val() == "")
        $("#txtDesignxLine2").val($("#txtDesignxLine").val());
    else if (f) {
        if (confirm("是否替换替代值PE点")) {
            $("#txtDesignxLine2").val($("#txtDesignxLine").val());
        }
    }
}
//选择输入PE点触发事件
function PEOnCheck() {
    $("#txtDesignxLine").attr("disabled", false);
    $("#txtDesignxLine").attr("class", "");
    //$("#txtDesignxLine").select();
    $("#ddlDesignxLine > option").each(function() { if (this.value == "") this.selected = true; });
    $("#ddlDesignxLine").attr("disabled", true);
}
function RepPEOnCheck() {
    $("#txtDesignxLine2").attr("disabled", false);
    $("#txtDesignxLine2").attr("class", "");
    //$("#txtDesignxLine2").select();
    $("#ddlDesignxLine2 > option").each(function() { if (this.value == "") this.selected = true; });
    $("#ddlDesignxLine2").attr("disabled", true);
}


//根据输出测点单位绑定
function BindUnit() {
    $("#txt_ParamEngunit").val($("#txt_ParamEngunit").val());
}

function BindHotTagA() {
    $("#txt_ParamTag").val($("#txt_ParamTag").val());
}

//OpenTagInfo窗口
function CallTagInfoDialog(tagname) {
    var iWidth = 600; //模态窗口宽度
    var iHeight = 500; //模态窗口高度
    var iTop = (window.screen.height - iHeight) / 2;
    var iLeft = (window.screen.width - iWidth) / 2;

    //更新，keyid="........"
    window.open("TagInfo.aspx?tagname=" + tagname, "newwindow", "Width=" + iWidth + " ,Height=" + iHeight + ",top=" + iTop + ",left=" + iLeft);
}

function showWindow(p) {
    var iWidth = 600; //模态窗口宽度
    var iHeight = 500; //模态窗口高度
    var iTop = (window.screen.height - iHeight) / 2;
    var iLeft = (window.screen.width - iWidth) / 2;

    //更新，keyid="........"
    window.open(p, "newwindow", "Width=" + iWidth + " ,Height=" + iHeight + ",top=" + iTop + ",left=" + iLeft);

}

function showWindow2(p, w, h) {

    var iWidth = 600; //模态窗口宽度
    var iHeight = 500; //模态窗口高度
    var iTop = (window.screen.height - iHeight) / 2;
    var iLeft = (window.screen.width - iWidth) / 2;

    //更新，keyid="........"
    window.open(p, "newwindow", "Width=" + iWidth + " ,Height=" + iHeight + ",top=" + iTop + ",left=" + iLeft);
}

//测点查询窗体调用父窗体事件
function FillControl(type, value) {
    var taginfo = value.split("|");

    if (type == 1) {
        $("#txt_OutTagName").val(taginfo[0]);
        $("#txt_OutTagDesc").val(taginfo[1]);
        $("#txt_OutTagUnit").val(taginfo[2]);
        $("#txt_OutTagName2").val(taginfo[0]);
        $("#txt_OutTagUnit2").val(taginfo[2]);
    }
    else if (type == 2) {
        $("#txt_InputTag").val(taginfo[0]);
    }
}
function divshow() {
    $("#divShow").show();
}

function divhide() {
    $("#divShow").hide();
}



//上限
function HighCheck() {
    if (!$("#chk_IsHigh").attr("checked")) {
        $("#txt_High").val("");
        $("#txt_High").attr("class", "readonlyInputText");
        $("#txt_High").attr("disabled", true);
    }
    else {
        $("#txt_High").attr("class", "");
        $("#txt_High").attr("disabled", false);
    }
}
//下限
function LowCheck() {
    if (!$("#chk_IsLow").attr("checked")) {
        $("#txt_Low").val("");
        $("#txt_Low").attr("class", "readonlyInputText");
        $("#txt_Low").attr("disabled", true);
    }
    else {
        $("#txt_Low").attr("class", "");
        $("#txt_Low").attr("disabled", false);
    }
}