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
    BindVals();
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

//选择设计曲线触发事件
function xLineOnCheck() {
    $("#txtDesignxLine").val("");
    $("#txtDesignxLine").attr("class", "readonlyInputText");
    $("#txtDesignxLine").attr("disabled", true);
    $("#ddlDesignxLine").attr("disabled", false);
}
function RepxLineOnCheck() {
    $("#txtDesignxLine2").val("");
    $("#txtDesignxLine2").attr("class", "readonlyInputText");
    $("#txtDesignxLine2").attr("disabled", true);
    $("#ddlDesignxLine2").attr("disabled", false);
}

//设计曲线变更
function SelectxlineChange(f) {
    var flag = false;
    var val = "";
    $("#ddlDesignxLine > option").each(function() { if (this.selected && this.value != "") { flag = true; val = this.value; } });

    if (val != "") {

        if (($("#ddlDesignxLine2").val() != "") && f) {
            if (confirm("是否替换替代方法设计曲线？")) {
                $("#ddlDesignxLine2").val(val);
            }
        }
        else if ($("#ddlDesignxLine2").val() == "") {
            $("#ddlDesignxLine2").val(val);
        }
    }
}

//回传之后触发事件
function BindVals() {
    SelectxlineChange();
    InputLineBlur();
    if ($("#rad_xLine2").attr("checked")) PEOnCheck();
    if ($("#rad_xLine1").attr("checked")) xLineOnCheck();
    if ($("#rad_subrep1").attr("checked")) RepPEOnCheck();
    if ($("#rad_subrep2").attr("checked")) RepxLineOnCheck();

    $("#txt_OutTagUnit2").val($("#txt_OutTagUnit").val());
    $("#txt_OutTagName2").val($("#txt_OutTagName").val());

    $("[name='rblModel']:radio").each(function(i) { $(this).css({ 'border-width': '0px' }) });

    HotCheck();
    GoodCheck();
    //ReplaceCheck();

    if (!$("#rad_xLine2").attr("checked")) {       
        $("#txtDesignxLine").attr("class", "readonlyInputText");
        $("#txtDesignxLine").attr("disabled", true);
    }
    if (!$("#rad_subrep1").attr("checked")) {
        $("#txtDesignxLine2").attr("class", "readonlyInputText");
        $("#txtDesignxLine2").attr("disabled", true);
    }
}
//根据输出测点单位绑定
function BindUnit() {
    $("#txt_OutTagUnit2").val($("#txt_OutTagUnit").val());
}

function BindHotTagA() {
    $("#txt_OutTagName2").val($("#txt_OutTagName").val());
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

//热力监督控制
function HotCheck() {
    if (!$("#chk_IsHotMonitor").attr("checked")) {
        $("#txt_MonitorDesc").val("");
        $("#txt_MonitorDesc").attr("class", "readonlyInputText");
        $("#txt_MonitorDesc").attr("disabled", true);
        
        $("#txt_MonitorTag").val("");
        $("#txt_MonitorTag").attr("class", "readonlyInputText");
        $("#txt_MonitorTag").attr("disabled", true);

        $("#ddl_MonitorRule").attr("disabled", true);        
    }
    else {
        $("#txt_MonitorDesc").attr("class", "");
        $("#txt_MonitorDesc").attr("disabled", false);
        //$("#txt_MonitorDesc").select();

        $("#txt_MonitorTag").attr("class", "");
        $("#txt_MonitorTag").attr("disabled", false);
        
        $("#ddl_MonitorRule").attr("disabled", false);  
    }
}

//有效性验证
function GoodCheck() {
    if (!$("#chk_IsCheck").attr("checked")) {
        $("#chk_IsHL").attr("checked", false);
        $("#chk_IsxLine").attr("checked", false);
        $("#chk_IsSigma").attr("checked", false);

        $("#chk_IsHL").attr("disabled", true);
        $("#chk_IsxLine").attr("disabled", true);
        $("#chk_IsSigma").attr("disabled", true);
    }
    else {
    
        $("#chk_IsHL").attr("disabled", false);
        $("#chk_IsxLine").attr("disabled", false);
        $("#chk_IsSigma").attr("disabled", false);
    }

    HLCheck();

    xLineCheck();
    
    sigmaCheck();
}

function HLCheck() {

    if (!$("#chk_IsHL").attr("checked")) {


        $("#chk_IsHigh").attr("checked", false);
        $("#chk_IsHigh").attr("disabled", true);
        
        $("#txt_High").val("");
        $("#txt_High").attr("class", "readonlyInputText");
        $("#txt_High").attr("disabled", true);

        $("#chk_IsLow").attr("checked", false);
        $("#chk_IsLow").attr("disabled", true);
        
        $("#txt_Low").val("");
        $("#txt_Low").attr("class", "readonlyInputText");
        $("#txt_Low").attr("disabled", true);
        
    }
    else {
        $("#chk_IsHigh").attr("disabled", false);        
        $("#chk_IsLow").attr("disabled", false);
     }

}

//设计曲线
function xLineCheck() {
    if (!$("#chk_IsxLine").attr("checked")) {
        $("#txt_xLinePercent").val("");
        $("#txt_xLinePercent").attr("class", "readonlyInputText");
        $("#txt_xLinePercent").attr("disabled", true);

        $("#ddl_xLineInterpolateType").attr("disabled", true);

        $("#rad_xLine2").attr("disabled", true);
        $("#rad_xLine1").attr("disabled", true);

        $("#rad_xLine2").attr("checked", false);
        $("#rad_xLine1").attr("checked", false);

        $("#ddlDesignxLine > option").each(function() { if (this.value == "") this.selected = true; });
        $("#ddlDesignxLine").attr("disabled", true);

        $("#txtDesignxLine").val("");
        $("#txtDesignxLine").attr("class", "readonlyInputText");
        $("#txtDesignxLine").attr("disabled", true);
    }
    else {  
//        if(!$("#rad_xLine2").attr("checked"))
//        {
//            $("#txtDesignxLine").attr("class", "readonlyInputText");
//            $("#txtDesignxLine").attr("disabled", true);
//        }
//        else{
//            $("#txtDesignxLine").attr("class", "");
//            $("#txtDesignxLine").attr("disabled", false);
//        }
//        
//        if (!$("#rad_xLine1").attr("checked")) {
//            $("#ddlDesignxLine > option").each(function() { if (this.value == "") this.selected = true; });
//            $("#ddlDesignxLine").attr("disabled", true); 
//        }
//        else{
//            $("#ddlDesignxLine").attr("disabled", false); 
//        }
//        
        $("#txt_xLinePercent").attr("class", "");
        $("#txt_xLinePercent").attr("disabled", false );
        
        $("#ddl_xLineInterpolateType").attr("disabled", false);

        $("#rad_xLine2").attr("disabled", false);
        $("#rad_xLine1").attr("disabled", false);
    }
}

//检验标准
function sigmaCheck() {
    if (!$("#chk_IsSigma").attr("checked")) {
        $("#ddl_sigma > option").each(function() { if (this.value == "0") this.selected = true; });
        $("#ddl_sigma").attr("disabled", true);
    }
    else {
        $("#ddl_sigma").attr("disabled", false);
    }
}


//替代方法
function ReplaceCheck() {
    if (!$("#chk_IsReplace").attr("checked")) {
        $("#rad_rep1").attr("checked", false);
        $("#rad_rep1").attr("disabled", true);
        $("#rad_rep2").attr("checked", false);
        $("#rad_rep2").attr("disabled", true);

        $("#rad_rep3").attr("checked", false);
        $("#rad_rep3").attr("disabled", true);
        $("#rad_rep4").attr("checked", false);
        $("#rad_rep4").attr("disabled", true);
        
    }
    else {

        $("#rad_rep1").attr("checked", false);
        $("#rad_rep1").attr("disabled", false);
        $("#rad_rep2").attr("checked", false);
        $("#rad_rep2").attr("disabled", false);
        $("#rad_rep3").attr("checked", false);
        $("#rad_rep3").attr("disabled", false);
        $("#rad_rep4").attr("checked", false);
        $("#rad_rep4").attr("disabled", false);       
               
    }

}