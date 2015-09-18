//*后台管理页JS函数，Jquery扩展
//*作者：zhaojun
//*时间：2012年01月08日
/*
获取动态tab标签当前iframeID
*/
function Current_iframeID() {
    var tabs_container = top.$("#tabs_container");
    return "tabs_iframe_" + tabs_container.find('.selected').attr('id').substr(5);
}
//表单tab切换
function Tabchange(id) {
    $('.ScrollBar').find('.tabPanel').hide();
    $('.ScrollBar').find("#" + id).show();
}
//搜索回车键
document.onkeydown = function (e) {
    if (!e) e = window.event; //火狐中是 window.event
    if ((e.keyCode || e.which) == 13) {
        var btnSearch = document.getElementById("btnSearch");
        if (btnSearch != null) {
            btnSearch.focus(); //让另一个控件获得焦点就等于让文本输入框失去焦点
            btnSearch.click();
        }
    }
}
/*
屏蔽快捷键F1-F12
*/
function Shieldkeydown() {
    $("*").keydown(function (e) {
        e = window.event || e || e.which;
        if (e.keyCode == 112 || e.keyCode == 113
            || e.keyCode == 114 || e.keyCode == 115
            || e.keyCode == 116 || e.keyCode == 117
            || e.keyCode == 118 || e.keyCode == 119
            || e.keyCode == 120 || e.keyCode == 121
            || e.keyCode == 122 || e.keyCode == 123) {
            e.keyCode = 0;
            return false;
        }
    })
}
/*
初始化
*/
$(document).ready(function () {
    $('.aspNetHidden').hide();
    //document.onselectstart = function () { return false; }
    //$(document).bind("contextmenu", function () {
    //    return false;
    //});
    //Shieldkeydown();
    publicobjcss();
    Loading(false);
    GetTabClick();
})
/*
切换验证码
*/
function ToggleCode(obj, codeurl) {
    $("#txtCode").val("");
    $("#" + obj).attr("src", codeurl + "?time=" + Math.random());
}
/*
回调
*/
function windowload() {
    rePage();
}

/*
刷新当前页面
*/
function rePage() {
    Loading(true);
    window.location.href = window.location.href.replace('#', '');
    return false;
}
function Replace() {
    Loading(true);
    window.location.reload();
    return false;
}
/*
返回上一级
*/
function bntback() {
    window.history.back(-1);
    Loading(true)
}
/*
跳转页面
*/
function Urlhref(url) {
    Loading(true);
    window.location.href = url;
    return false;
}
/*
中间加载对话窗
*/
function Loading(bool) {
    if (bool) {
        top.$("#loading").show();
    } else {
        setInterval(loadinghide, 900);
    }
}
function loadinghide() {
    if (top.document.getElementById("loading") != null) {
        top.$("#loading").hide();
    }
}
/**
Top 加载对话窗
msg:提示信息
time：停留时间ms
type：提示类型（1、success 2、error 3、warning）
**/
function showTopMsg(msg, time, type) {
    MsgTips(time, msg, 300, type);
}
function BeautifulGreetings() {
    var now = new Date();
    var hour = now.getHours();
    if (hour < 3) { return ("夜深了,早点休息吧！") }
    else if (hour < 9) { return ("早上好！") }
    else if (hour < 12) { return ("上午好！") }
    else if (hour < 14) { return ("中午好！") }
    else if (hour < 18) { return ("下午好！") }
    else if (hour < 23) { return ("晚上好！") }
    else { return ("夜深了,早点休息吧！") }
}
/**
短暂提示
msg: 显示消息
time：停留时间ms
type：类型 4：成功，5：失败，3：警告
**/
function showTipsMsg(msg, time, type) {
    if (type == 4) {
        top.showTopMsg(msg, time, 'success');//头部提示,1、success 2、error 3、warning
    } else if (type == 5) {
        top.showTopMsg(msg, time, 'error');//头部提示,1、success 2、error 3、warning
    } else if (type == 3) {
        top.showTopMsg(msg, time, 'warning');//头部提示,1、success 2、error 3、warning
    }
}

function showFaceMsg(msg) {
    top.art.dialog({
        id: 'faceId',
        title: '温馨提醒',
        content: msg,
        icon: 'face-smile',
        time: 10,
        background: '#000',
        opacity: 0.1,
        lock: true,
        okVal: '关闭',
        ok: true
    });
}
function showWarningMsg(msg) {
    top.art.dialog({
        id: 'warningId',
        title: '系统提示',
        content: msg,
        icon: 'warning',
        time: 10,
        background: '#000',
        opacity: 0.1,
        lock: true,
        okVal: '关闭',
        ok: true
    });
}
/**
警告提示
msg: 显示消息
callBack：函数
**/
function showConfirmMsg(msg, callBack) {
    top.art.dialog({
        id: 'confirmId',
        title: '系统提示',
        content: msg,
        icon: 'warning',
        background: '#000000',
        opacity: 0.1,
        lock: true,
        button: [{
            name: '确定',
            callback: function () {
                callBack(true);
            },
            focus: true
        }, {
            name: '取消',
            callback: function () {
                this.close();
                return false;
            }
        }]
    });
}
/**右下角滑动通知  
*/
function showNotice(_id, _title, _width, content, time) {
    top.art.dialog.notice({
        id: _id,
        title: _title,
        width: _width,
        content: content,
        icon: 'face-smile',
        time: time
    });
}
/*弹出网页
/*url:          表示请求路径
/*_id:          ID
/*_title:       标题名称
/*width:        宽度
/*height:       高度
---------------------------------------------------*/
function openDialog(url, _id, _title, _width, _height, left, top) {
    art.dialog.open(url, {
        id: _id,
        title: _title,
        width: _width,
        height: _height,
        left: left + '%',
        top: top + '%',
        background: '#000000',
        opacity: 0.1,
        lock: true,
        resize: false,
        close: function () { }
    }, false);
}
/*弹出网页
/*url:          表示请求路径
/*_id:          ID
/*_title:       标题名称
/*width:        宽度
/*height:       高度
---------------------------------------------------*/
function FillopenDialog(url, _id, _title) {
    art.dialog.open(url, {
        id: _id,
        title: _title,
        width: '100%',
        height: '100%',
        background: '#000000',
        opacity: 0.1,
        lock: true,
        resize: false,
        close: function () { }
    }, false);
}
//窗口关闭
function OpenClose() {
    art.dialog.close();
}
/*验证
/*id:        表示请求
--------------------------------------------------*/
function IsEditdata(id) {
    var isOK = true;
    if (id == undefined || id == "") {
        isOK = false;
        showTipsMsg("很抱歉，您当前未选中任何一行！", 4000, 3);
    } else if (id.split(",").length > 1) {
        isOK = false;
        showTipsMsg("很抱歉，一次只能选择一条记录！", 4000, 3);
    }
    return isOK;
}
function IsDelData(id) {
    var isOK = true;
    if (id == undefined || id == "") {
        isOK = false;
        showTipsMsg("很抱歉，您当前未选中任何一行！", 4000, 3);
    }
    return isOK;
}
function IsChecked(id) {
    var isOK = true;
    if (id == undefined || id == "") {
        isOK = false;
        showTipsMsg("您当前未选中任何一行！", 4000, 3);
    }
    return isOK;
}
//验证是否为空
function IsNullOrEmpty(str) {
    var isOK = true;
    if (str == undefined || str == "") {
        isOK = false;
    }
    return isOK;
}
/*删除数据
/*url:        表示请求路径
/*parm：      条件参数
--------------------------------------------------*/
function delConfig(url, parm, count) {
    DeleteConfig(url, parm, "注：删除操作不可恢复，您确定要继续么？", count);
}
function DeleteConfig(url, parm, Msg, count) {
    if (count == undefined) {
        count = 1;
    }
    showConfirmMsg(Msg, function (r) {
        if (r) {
            getAjax(url, parm, function (rs) {
                if (rs.toLocaleLowerCase() == 'true') {
                    showTipsMsg("成功删除 " + count + " 笔记录。", 2000, 4);
                    windowload();
                } else if (rs.toLocaleLowerCase() == 'false') {
                    showTipsMsg("删除失败，请稍后重试", 4000, 5);
                } else {
                    showTipsMsg(rs, 4000, 3);
                }
            });
        }
    });
}
/*验证数据是否存在
/*url:        表示请求路径
/*parm：      条件参数
--------------------------------------------------*/
function IsExist_Data(url, parm) {
    var num = 0;
    getAjax(url, parm, function (rs) {
        num = parseInt(rs);
    });
    return num;
}
/* 请求Ajax 带返回值
--------------------------------------------------*/
function getAjax(url, parm, callBack) {
    $.ajax({
        type: 'post',
        dataType: "text",
        url: url,
        data: parm,
        cache: false,
        async: false,
        success: function (msg) {
            callBack(msg);
        }
    });
}
/**
数据验证完整性
**/
function CheckDataValid(id) {
    if (!JudgeValidate(id)) {
        return false;
    } else {
        return true;
    }
}
/********
接收地址栏参数
key:参数名称
**********/
function GetQuery(key) {
    var search = location.search.slice(1); //得到get方式提交的查询字符串
    var arr = search.split("&");
    for (var i = 0; i < arr.length; i++) {
        var ar = arr[i].split("=");
        if (ar[0] == key) {
            return ar[1];
        }
    }
    return "";
}
/********
修改地址栏URL参数
destiny:原来URL地址
par:修改参数
par_value:修改值
**********/
function changeURLPar(destiny, par, par_value) {
    var pattern = par + '=([^&]*)';
    var replaceText = par + '=' + par_value;
    if (destiny.match(pattern)) {
        var tmp = '/\\' + par + '=[^&]*/';
        tmp = destiny.replace(eval(tmp), replaceText);
        return (tmp);
    }
    else {
        if (destiny.match('[\?]')) {
            return destiny + '&' + replaceText;
        }
        else {
            return destiny + '?' + replaceText;
        }
    }
    return destiny + '\n' + par + '\n' + par_value;
}
/**
文本框只允许输入数字
**/
function Keypress(obj) {
    $("#" + obj).bind("contextmenu", function () {
        return false;
    });
    $("#" + obj).css('ime-mode', 'disabled');
    $("#" + obj).keypress(function (e) {
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });
}
/**
只能输入数字和小数点
**/
function IsNumberbox(obj) {
    $("#" + obj).bind("contextmenu", function () {
        return false;
    });
    $("#" + obj).css('ime-mode', 'disabled');
    $("#" + obj).bind("keydown", function (e) {
        var key = window.event ? e.keyCode : e.which;
        if (isFullStop(key)) {
            return $(this).val().indexOf('.') < 0;
        }
        return (isSpecialKey(key)) || ((isNumber(key) && !e.shiftKey));
    });
    function isNumber(key) {
        return key >= 48 && key <= 57
    }
    function isSpecialKey(key) {
        return key == 8 || key == 46 || (key >= 37 && key <= 40) || key == 35 || key == 36 || key == 9 || key == 13
    }
    function isFullStop(key) {
        return key == 190 || key == 110;
    }
}
/**
获取选中复选框值
值：1,2,3,4
**/
function CheckboxValue() {
    var reVal = '';
    $('[type = checkbox]').each(function () {
        if ($(this).attr("checked")) {
            reVal += $(this).val() + ",";
        }
    });
    reVal = reVal.substr(0, reVal.length - 1);
    return reVal;
}
/** div 自应表格高度**/
function divresize(element, height) {
    resizeU();
    $(window).resize(resizeU);
    function resizeU() {
        $(element).css("height", $(window).height() - height);
    }
}
//Tab标签切换
function GetTabClick() {
    $(".tab_list_top div").click(function () {
        $(".tab_list_top div").removeClass("actived");
        $(this).addClass("actived"); //添加选中样式   
    })
}
/**iframe自应高度**/
function iframeresize(height) {
    if (height == undefined) {
        height = 0;
    }
    resizeU();
    $(window).resize(resizeU);
    function resizeU() {
        var iframeMain = $(window).height();
        $("#iframeMainContent").height(iframeMain - height);
    }
}
function divresize_From(height) {
    resizeU();
    $(window).resize(resizeU);
    function resizeU() {
        $(".div-body-From").width($(window).width() - 3);
        $(".div-body-From").css("height", $(window).height() - height);
    }
}
/**
初始化样式
**/
/*****************树表格********************************/
function dndexampleCss() {
    $(".example tbody tr").click(function () {
        $(this).removeClass("tdhover");
        $('.example tr').removeClass("selected");
        $(this).addClass("selected"); //添加选中样式   
    }).hover(function () {
        if (!$(this).hasClass('selected')) {
            $(this).addClass("tdhover");
        }
    }, function () {
        $(this).removeClass("tdhover");
    });
}
function publicobjcss() {
    dndexampleCss();
    /*****************按钮********************************/
    $(".l-btn").hover(function () {
        $(this).addClass("l-btnhover");
        $(this).find('span').addClass("l-btn-lefthover");
    }, function () {
        $(this).removeClass("l-btnhover");
        $(this).find('span').removeClass("l-btn-lefthover");
    });
    $(".tools_bar .tools_btn").hover(function () {
        if ($(this).attr('disabled') != 'disabled') {
            $(this).addClass("tools_btn-hover");
            $(this).find('span').addClass("tools_btn-hover");
        }
    }, function () {
        $(this).removeClass("tools_btn-hover");
        $(this).find('span').removeClass("tools_btn-hover");
    });
    /*****************自定义复选框********************************/
    $(".panelcheck").click(function () {
        if (!$(this).hasClass('checkbuttonOk')) {
            $(this).attr('class', 'checkbuttonOk panelcheck');
            $(this).find('.triangleNo').attr('class', 'triangleOk');

        } else {
            $(this).attr('class', 'checkbuttonNo panelcheck');
            $(this).find('.triangleOk').attr('class', 'triangleNo');
        }
    })

    $(".sub-menu div").click(function () {
        $('.sub-menu div').removeClass("selected")
        $(this).addClass("selected");
    }).hover(function () {
        $(this).addClass("navHover");
    }, function () {
        $(this).removeClass("navHover");
    });
}
/*****************树形样式********************************/
function treeAttrCss() {
    $(".black").treeview({
        control: "#treecontrol",
        persist: "cookie",
        cookieId: "treeview-gray"
    });
    treeCss();
}
//树样式
function treeCss() {
    $(".strTree li div").css("cursor", "pointer");
    $(".strTree li div").click(function () {
        $(".strTree li div").removeClass("collapsableselected");
        $(this).addClass("collapsableselected"); //添加选中样式
    }).hover(function () {
        $(this).addClass("collapsablehover"); //添加选中样式
    }, function () {
        $(".strTree li div").removeClass("collapsablehover");
    });
}
/**********复选框 全选/反选**************/
var CheckAllLinestatus = 0;
function CheckAllLine() {
    if (CheckAllLinestatus == 0) {
        CheckAllLinestatus = 1;
        $("#checkAllOff").attr('title', '反选');
        $("#checkAllOff").attr('id', 'checkAllOn');
        $("#dnd-example :checkbox").attr("checked", true);
    } else {
        CheckAllLinestatus = 0;
        $("#checkAllOn").attr('title', '全选');
        $("#checkAllOn").attr('id', 'checkAllOff');
        $("#dnd-example :checkbox").attr("checked", false);
    }
}
//树表格复选框，点击子，把父也打勾
function ckbValueObj(e) {
    var item_id = '';
    var arry = new Array();
    arry = e.split('-');
    for (var i = 0; i < arry.length - 1; i++) {
        item_id += arry[i] + '-';
    }
    item_id = item_id.substr(0, item_id.length - 1);
    if (item_id != "") {
        $("#" + item_id).attr("checked", true);
        ckbValueObj(item_id);
    }
}
/**
* 金额格式(保留2位小数)后格式化成金额形式
*/
function FormatCurrency(num) {
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
        num = num.substring(0, num.length - (4 * i + 3)) + '' +
                num.substring(num.length - (4 * i + 3));
    return (((sign) ? '' : '-') + num + '.' + cents);
}
//字符串转日期格式，strDate要转为日期格式的字符串
function GetStrToDate(strDate) {
    var date = eval('new Date(' + strDate.replace(/\d+(?=-[^-]+$)/,
    function (a) { return parseInt(a, 10) - 1; }).match(/\d+/g) + ')');
    return date;
}
//日期加天数得到新的日期  
function DateAdd(sdate, days) {
    var a = new Date(sdate);
    a = a.valueOf();
    a = a + days * 24 * 60 * 60 * 1000;
    a = new Date(a);
    return a;
}
//格式化日期：yyyy-MM-dd  
function formatDate(date) {
    var myyear = date.getFullYear();
    var mymonth = date.getMonth() + 1;
    var myweekday = date.getDate();

    if (mymonth < 10) {
        mymonth = "0" + mymonth;
    }
    if (myweekday < 10) {
        myweekday = "0" + myweekday;
    }
    return (myyear + "-" + mymonth + "-" + myweekday);
}
/**     
 * 对Date的扩展，将 Date 转化为指定格式的String     
 * 月(M)、日(d)、12小时(h)、24小时(H)、分(m)、秒(s)、周(E)、季度(q) 可以用 1-2 个占位符     
 * 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字)     
 * eg:     
 * (new Date()).pattern("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423     
 * (new Date()).pattern("yyyy-MM-dd E HH:mm:ss") ==> 2009-03-10 二 20:09:04     
 * (new Date()).pattern("yyyy-MM-dd EE hh:mm:ss") ==> 2009-03-10 周二 08:09:04     
 * (new Date()).pattern("yyyy-MM-dd EEE hh:mm:ss") ==> 2009-03-10 星期二 08:09:04     
 * (new Date()).pattern("yyyy-M-d h:m:s.S") ==> 2006-7-2 8:9:4.18     
使用：(eval(value.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"))).pattern("yyyy-M-d h:m:s.S");
 */
Date.prototype.pattern = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份        
        "d+": this.getDate(), //日        
        "h+": this.getHours() % 12 == 0 ? 12 : this.getHours() % 12, //小时        
        "H+": this.getHours(), //小时        
        "m+": this.getMinutes(), //分        
        "s+": this.getSeconds(), //秒        
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度        
        "S": this.getMilliseconds() //毫秒        
    };
    var week = {
        "0": "/u65e5",
        "1": "/u4e00",
        "2": "/u4e8c",
        "3": "/u4e09",
        "4": "/u56db",
        "5": "/u4e94",
        "6": "/u516d"
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    if (/(E+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "/u661f/u671f" : "/u5468") : "") + week[this.getDay() + ""]);
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
}
/** 
* 获取本周、本季度、本月 开始日期、结束日期   begin
*/


/** 
* 调用  type：类型, BeginControID：开始时间控件, EndControID：结束时间控件
*/
function Quickseldate(type, BeginControID, EndControID) {
    var begintime, endtime;
    if (type == "week") {
        endtime = getWeekEndDate();
        begintime = getWeekStartDate(endtime);
    } else if (type == "month") {
        endtime = getMonthEndDate();
        begintime = getMonthStartDate(endtime);
    } else if (type == "quarter") {
        begintime = getQuarterStartDate();
        endtime = getQuarterEndDate();
    }
    $("#" + BeginControID).val(begintime);
    $("#" + EndControID).val(endtime);
}

var now = new Date();                    //当前日期  
var nowDayOfWeek = now.getDay();         //今天本周的第几天  
var nowDay = now.getDate();              //当前日  
var nowMonth = now.getMonth();           //当前月  
var nowYear = now.getYear();             //当前年  
nowYear += (nowYear < 2000) ? 1900 : 0;  //  
//获得某月的天数  
function getMonthDays(myMonth) {
    var monthStartDate = new Date(nowYear, myMonth, 1);
    var monthEndDate = new Date(nowYear, myMonth + 1, 1);
    var days = (monthEndDate - monthStartDate) / (1000 * 60 * 60 * 24);
    return days;
}
//获得本季度的开始月份  
function getQuarterStartMonth() {
    var quarterStartMonth = 0;
    if (nowMonth < 3) {
        quarterStartMonth = 0;
    }
    if (2 < nowMonth && nowMonth < 6) {
        quarterStartMonth = 3;
    }
    if (5 < nowMonth && nowMonth < 9) {
        quarterStartMonth = 6;
    }
    if (nowMonth > 8) {
        quarterStartMonth = 9;
    }
    return quarterStartMonth;
}
//获得本周的开始日期  
function getWeekStartDate() {
    var weekStartDate = new Date(nowYear, nowMonth, nowDay - nowDayOfWeek);
    return weekStartDate.pattern("yyyy-MM-dd") + " 00:00";
}
//获得本周的结束日期  
function getWeekEndDate() {
    var weekEndDate = new Date(nowYear, nowMonth, nowDay + (6 - nowDayOfWeek));
    return weekEndDate.pattern("yyyy-MM-dd") + " 23:59";
}
//获得本月的开始日期  
function getMonthStartDate() {
    var monthStartDate = new Date(nowYear, nowMonth, 1);
    return monthStartDate.pattern("yyyy-MM-dd") + " 00:00";
}
//获得本月的结束日期  
function getMonthEndDate() {
    var monthEndDate = new Date(nowYear, nowMonth, getMonthDays(nowMonth));
    return monthEndDate.pattern("yyyy-MM-dd") + " 23:59";
}
//获得本季度的开始日期  
function getQuarterStartDate() {
    var quarterStartDate = new Date(nowYear, getQuarterStartMonth(), 1);
    return quarterStartDate.pattern("yyyy-MM-dd") + " 00:00";
}
//或的本季度的结束日期  
function getQuarterEndDate() {
    var quarterEndMonth = getQuarterStartMonth() + 2;
    var quarterStartDate = new Date(nowYear, quarterEndMonth, getMonthDays(quarterEndMonth));
    return quarterStartDate.pattern("yyyy-MM-dd") + " 23:59";
}
/** 
* 获取本周、本季度、本月 开始日期、结束日期   end
*/

/**
Table固定表头
gv:             table id
scrollHeight:   高度
scrollWidth:    宽度
**/
function FixedTableHeader(gv, scrollHeight, scrollWidth) {
    var gvn = $(gv).clone(true).removeAttr("id");
    $(gvn).find("tr:not(:first)").remove();
    $(gv).before(gvn);
    $(gv).find("tr:first").remove();
    $(gv).wrap("<div id='FixedTable' style='width:" + scrollWidth + "px;height:" + scrollHeight + "px;overflow-y: auto; overflow-x: hidden; padding: 0;margin: 0;'></div>");
}
//右键菜单
(function (menu) {
    jQuery.fn.contextmenu = function (options) {
        var defaults = {
            offsetX: 2,        //鼠标在X轴偏移量
            offsetY: 2,        //鼠标在Y轴偏移量
            items: [],       //菜单项
            action: $.noop()  //自由菜单项回到事件
        };
        var opt = menu.extend(true, defaults, options);
        function create(e) {
            var m = menu('<ul class="simple-contextmenu"></ul>').appendTo(document.body);

            menu.each(opt.items, function (i, item) {
                if (item) {
                    if (item.type == "split") {
                        menu("<div class='m-split'></div>").appendTo(m);
                        return;
                    }
                    var row = menu('<li><a href="javascript:void(0)"><span></span></a></li>').appendTo(m);
                    item.icon ? menu('<img src="' + item.icon + '">').insertBefore(row.find('span')) : '';
                    item.text ? row.find('span').text(item.text) : '';

                    if (item.action) {
                        row.find('a').click(function () {
                            item.action(e.target);
                        });
                    }
                }
            });
            return m;
        }

        this.live('contextmenu', function (e) {
            var m = create(e).show();
            var left = e.pageX + opt.offsetX, top = e.pageY + opt.offsetY, p = {
                wh: menu(window).height(),
                ww: menu(window).width(),
                mh: m.height(),
                mw: m.width()
            }
            top = (top + p.mh) >= p.wh ? (top -= p.mh) : top;
            //当菜单超出窗口边界时处理
            left = (left + p.mw) >= p.ww ? (left -= p.mw) : left;
            m.css({
                zIndex: 10000,
                left: left,
                top: top
            });
            $(document.body).live('contextmenu click', function () {
                m.remove();
            });

            return false;
        });
        return this;
    }
})(jQuery);

/**加载表格函数
Begin
obj_ID:元素ID, url:请求地址, colM:表头名称, sort:要显示字段, PageSize：每页大小
**/
var GetRowIndex = -1;//索引号
function PQLoadGrid(obj_ID, url, colM, sort, PageSize, topVisible) {
    GetRowIndex = -1;
    var dataModel = {
        location: "remote",
        sorting: "remote",
        paging: "remote",
        dataType: "JSON",
        method: "GET",
        curPage: 1,
        rPP: PageSize,
        sortIndx: 0,
        sortDir: "up",
        rPPOptions: [20, 30, 40, 50, 100, 500, 1000],
        getUrl: function () {
            var orderField = (this.sortIndx == "0") ? "" : sort[this.sortIndx];
            var sortDir = (this.sortDir == "up") ? "desc" : "asc";
            if (this.curPage == 0) {
                this.curPage = 1;
            }
            return {
                url: url, data: "pqGrid_PageIndex=" + this.curPage + "&pqGrid_PageSize=" +
                    this.rPP + "&pqGrid_OrderField=" + orderField + "&pqGrid_OrderType=" + sortDir + "&pqGrid_Sort=" + escape(sort)
            };
        },
        getData: function (dataJSON) {
            if (dataJSON == null && dataJSON.totalRecords <= 0) {
                showTipsMsg("没有找到您要的相关数据！", 5000, 5);
            }
            return { curPage: dataJSON.curPage, totalRecords: dataJSON.totalRecords, data: dataJSON.data };
        }
    }
    if (!IsNullOrEmpty(topVisible)) {
        topVisible = false;
    }
    $(obj_ID).pqGrid({
        dataModel: dataModel,
        colModel: colM,
        editable: false,
        topVisible: topVisible,
        oddRowsHighlight: false,
        columnBorders: true,
        freezeCols: 0,
        rowSelect: function (evt, ui) {
            GetRowIndex = ui.rowIndxPage;
        }
    });
    pqGridResize(obj_ID, -52, -4);
}
function PQLoadGridNoPage(obj_ID, url, colM, sort) {
    GetRowIndex = -1;
    var dataModel = {
        location: "remote",
        sorting: "remote",
        paging: "",
        dataType: "JSON",
        method: "GET",
        sortIndx: 0,
        sortDir: "up",
        getUrl: function () {
            var orderField = (this.sortIndx == "0") ? "" : sort[this.sortIndx];
            var sortDir = (this.sortDir == "up") ? "desc" : "asc";
            return {
                url: url, data: "pqGrid_OrderField=" + orderField + "&pqGrid_OrderType=" + sortDir + "&pqGrid_Sort=" + escape(sort)
            };
        },
        getData: function (dataJSON) {
            if (dataJSON == null) {
                showTipsMsg("没有找到您要的相关数据！", 5000, 5);
            }
            return { data: dataJSON };
        }
    }
    $(obj_ID).pqGrid({
        dataModel: dataModel,
        colModel: colM,
        editable: false,
        bottomVisible: false,
        oddRowsHighlight: false,
        columnBorders: true,
        freezeCols: 0,
        rowSelect: function (evt, ui) {
            GetRowIndex = ui.rowIndxPage;
        }
    });
    pqGridResize(obj_ID, -36, -4);
}
/**表格自应高度
obj：ID，lose_height:-高度,lose_width:-宽度
**/
function pqGridResize(obj, lose_height, lose_width) {
    var grid_height = $(window).height() + lose_height;
    var grid_width = $(window).width() + lose_width;
    var grid1 = $(obj).pqGrid({
        width: grid_width,
        height: grid_height
    });
}
/**表格自应高度
obj：ID，lose_height:-高度,lose_width:-宽度
**/
function pqGridResizefixed(obj, lose_height, lose_width) {
    var grid_width = $(window).width() + lose_width;
    var grid1 = $(obj).pqGrid({
        width: grid_width,
        height: lose_height
    });
}
/**获取表格列值
obj：ID，rowCode：列号
**/
function GetPqGridRowValue(obj_ID, rowCode) {
    if (GetRowIndex != -1) {
        var DM = $(obj_ID).pqGrid("option", "dataModel");
        var data = DM.data;
        return data[GetRowIndex][rowCode];
    }
    else {
        return null;
    }
}
/**加载表格
end
**/
//自动补全表格
var IndetableRow_autocomplete = 0;
var scrollTopheight = 0;
function autocomplete(Objkey, width, height, data, callBack) {
    if ($('#' + Objkey).attr('readonly') == 'readonly') {
        return false;
    }
    if ($('#' + Objkey).attr('disabled') == 'disabled') {
        return false;
    }
    IndetableRow_autocomplete = 0;
    scrollTopheight = 0;
    var X = $("#" + Objkey).offset().top;
    var Y = $("#" + Objkey).offset().left;
    $("#div_gridshow").html("");
    if ($("#div_gridshow").attr("id") == undefined) {
        $('body').append('<div id="div_gridshow" style="overflow: auto;z-index: 1000;border: 1px solid #A8A8A8;border-top: 0px solid #A8A8A8;width:' + width + ';height:' + height + ';position: absolute; background-color: #fff; display: none;"></div>');
    } else {
        $("#div_gridshow").height(height);
        $("#div_gridshow").width(width);
    }
    var sbhtml = '<table class="tableobj">';
    if (data != "") {
        sbhtml += '<tbody>' + data + '</tbody>';
    } else {
        sbhtml += '<tbody><tr><td style="color:red;text-align:center;width:' + width + ';">没有找到您要的相关数据！</td></tr></tbody>';
    }
    sbhtml += '</table>';
    $("#div_gridshow").html(sbhtml);
    $("#div_gridshow").css("left", Y).css("top", X + 25).show();
    if (data != "") {
        $("#div_gridshow").find('tbody tr').each(function (r) {
            if (r == 0) {
                $(this).addClass('selected');
            }
        });
    }
    $("#div_gridshow").find('tbody tr').click(function () {
        var value = "";
        $(this).find('td').each(function (i) {
            value += $(this).text() + "≌";
        });
        if ($('#' + Objkey).attr('readonly') == 'readonly') {
            return false;
        }
        if ($('#' + Objkey).attr('disabled') == 'disabled') {
            return false;
        }
        callBack(value);
        $("#div_gridshow").hide();
    });
    $("#div_gridshow").find('tbody tr').hover(function () {
        $(this).addClass("selected");
    }, function () {
        $(this).removeClass("selected");
    });
    //任意键关闭
    document.onclick = function (e) {
        var e = e ? e : window.event;
        var tar = e.srcElement || e.target;
        if (tar.id != 'div_gridshow') {
            if ($(tar).attr("id") == 'div_gridshow' || $(tar).attr("id") == Objkey) {
                $("#div_gridshow").show();
            } else {
                $("#div_gridshow").hide();
            }
        }
    }
}
//方向键上,方向键下,回车键
function autocompletekeydown(Objkey, callBack) {
    $("#" + Objkey).keydown(function (e) {
        switch (e.keyCode) {
            case 38: // 方向键上
                if (IndetableRow_autocomplete > 0) {
                    IndetableRow_autocomplete--
                    $("#div_gridshow").find('tbody tr').removeClass('selected');
                    $("#div_gridshow").find('tbody tr').each(function (r) {
                        if (r == IndetableRow_autocomplete) {
                            scrollTopheight -= 22;
                            $("#div_gridshow").scrollTop(scrollTopheight);
                            $(this).addClass('selected');
                        }
                    });
                }
                break;
            case 40: // 方向键下
                var tindex = $("#div_gridshow").find('tbody tr').length - 1;
                if (IndetableRow_autocomplete < tindex) {
                    IndetableRow_autocomplete++;
                    $("#div_gridshow").find('tbody tr').removeClass('selected');
                    $("#div_gridshow").find('tbody tr').each(function (r) {
                        if (r == IndetableRow_autocomplete) {
                            scrollTopheight += 22;
                            $("#div_gridshow").scrollTop(scrollTopheight);
                            $(this).addClass('selected');
                        }
                    });
                }
                break;
            case 13:  //回车键
                var value = "";
                $("#div_gridshow").find('tbody tr').each(function (r) {
                    if (r == IndetableRow_autocomplete) {
                        $(this).find('td').each(function (i) {
                            value += $(this).text() + "≌";
                        });
                    }
                });
                if ($('#' + Objkey).attr('readonly') == 'readonly') {
                    return false;
                }
                if ($('#' + Objkey).attr('disabled') == 'disabled') {
                    return false;
                }
                callBack(value);
                $("#div_gridshow").hide();
                break;
            default:
                break;
        }
    })
}
//树下拉框
function combotree(Objkey, width, height, data) {
    $("#" + Objkey).bind("contextmenu", function () {
        return false;
    });
    $("#" + Objkey).css('ime-mode', 'disabled');
    $("#" + Objkey).keypress(function (e) {
        return false;
    });
    if ($('#' + Objkey).attr('readonly') == 'readonly') {
        return false;
    }
    if ($('#' + Objkey).attr('disabled') == 'disabled') {
        return false;
    }
    var X = $("#" + Objkey).offset().top;
    var Y = $("#" + Objkey).offset().left;
    $("#div_treeshow").html("");
    if ($("#div_treeshow").attr("id") == undefined) {
        $('body').append('<div id="div_treeshow" style="overflow: auto;border: 1px solid #A8A8A8;width:' + width + ';height:' + height + ';position: absolute; background-color: #fff; display: none;"></div>');
    } else {
        $("#div_treeshow").height(height);
        $("#div_treeshow").width(width);
    }
    var sbhtml = '';
    if (data != "") {
        sbhtml += '<ul class="black strTree">' + data + '</ul>';
    } else {
        sbhtml += '<ul class="black strTree"><li><div><span style="color:red;">暂无数据</span></div></li></ul>';
    }
    sbhtml += '</table>';
    $("#div_treeshow").html(sbhtml);
    $("#div_treeshow").css("left", Y).css("top", X + 23).show();
    $("#div_treeshow").css("z-index", "1000");
    //任意键关闭
    document.onclick = function (e) {
        var e = e ? e : window.event;
        var tar = e.srcElement || e.target;
        if (tar.id != 'div_treeshow') {
            if ($(tar).attr("id") == 'ParentName') {
                $("#div_treeshow").show();
            } else {
                $("#div_treeshow").hide();
            }
        }
    }
    treeAttrCss();
}
/***
 * 自动关闭弹出内容提示
timeOut : 4000,				//提示层显示的时间
msg : "恭喜你!你已成功操作此插件，谢谢使用！",			//显示的消息
speed : 300,				//滑动速度
type : "success"			//提示类型（1、success 2、error 3、warning）
***/
function MsgTips(timeOut, msg, speed, type) {
    $(".tip_container").remove();
    var bid = parseInt(Math.random() * 100000);
    $("body").prepend('<div id="tip_container' + bid + '" class="container tip_container"><div id="tip' + bid + '" class="mtip"><span id="tsc' + bid + '"></span></div></div>');
    var $this = $(this);
    var $tip_container = $("#tip_container" + bid);
    var $tip = $("#tip" + bid);
    var $tipSpan = $("#tsc" + bid);
    //先清楚定时器
    clearTimeout(window.timer);
    //主体元素绑定事件
    $tip.attr("class", type).addClass("mtip");
    $tipSpan.html(msg);
    $tip_container.slideDown(speed);
    //提示层隐藏定时器
    window.timer = setTimeout(function () {
        $tip_container.slideUp(speed);
        $(".tip_container").remove();
    }, timeOut);
    //鼠标移到提示层时清除定时器
    $tip_container.live("mouseover", function () {
        clearTimeout(window.timer);
    });
    //鼠标移出提示层时启动定时器
    $tip_container.live("mouseout", function () {
        window.timer = setTimeout(function () {
            $tip_container.slideUp(speed);
            $(".tip_container").remove();
        }, timeOut);
    });
    $("#tip_container" + bid).css("left", ($(window).width() - $("#tip_container" + bid).width()) / 2);
    //$("#tip_container" + bid).css("top", ($(window).height() - $("#tip_container" + bid).height()) / 2);
}
//提示标签
function TipToMouse(_title, e) {
    var top = e.offset().top - 5;
    var left = e.offset().left + e.width();
    $("body").prepend("<div class='tooltipdi'><span><b></b><em></em>" + _title + "</span></div>");
    $(".tooltipdi").css(
    {
        "top": top + "px",
        "left": left + "px"
    }
    ).show();
    var _width = $(".tooltipdi").width();
    $(".tooltipdi").width(_width - 5);
    $(e).blur(function () {
        $(".tooltipdi").remove();
    })
}
//省略字符串
function suolve(str) {
    var sub_length = 15;
    var temp1 = str.replace(/[^\x00-\xff]/g, "**");//精髓   
    var temp2 = temp1.substring(0, sub_length);
    //找出有多少个*   
    var x_length = temp2.split("\*").length - 1;
    var hanzi_num = x_length / 2;
    sub_length = sub_length - hanzi_num;//实际需要sub的长度是总长度-汉字长度   
    var res = str.substring(0, sub_length);
    if (sub_length < str.length) {
        var end = res + "…";
    } else {
        var end = res;
    }
    return end;
}

//置顶
function moveTop(id) {
    var selId = id;
    var selObj = document.getElementById(selId);
    if (selObj.options.length == 0 || selObj.options[0].selected) {
        return false;
    }
    var count = selObj.options.length - 1;
    for (var i = selObj.options.length - 1; i >= 0; i--) {
        var op = selObj.options[count];
        if (op.selected) {
            selObj.remove(count);
            selObj.options.add(op, 0);
        } else {
            count--;
            continue;
        }
    }
}
//置底
function moveBottom(id) {
    var selId = id;
    var selObj = document.getElementById(selId);
    if (selObj.options.length == 0 || selObj.options[selObj.options.length - 1].selected) {
        return false;
    }
    var count = 0;
    for (var i = 0; i < selObj.options.length; i++) {
        var op = selObj.options[count];
        if (op.selected) {
            selObj.remove(count);
            selObj.options.add(op);
        } else {
            count++;
            continue;
        }
    }
}
//上移
function moveUp(id) {
    var selId = id;
    var selObj = document.getElementById(selId);
    if (selObj.options.length == 0 || selObj.options[0].selected) {
        return false;
    }
    for (var i = 0; i < selObj.options.length; i++) {
        var op = selObj.options[i];
        if (op.selected) {
            selObj.remove(i);
            selObj.options.add(op, i - 1);
        } else {
            continue;
        }
    }
}
//下移
function moveDown(id) {
    var selId = id;
    var selObj = document.getElementById(selId);
    if (selObj.options.length == 0 || selObj.options[selObj.options.length - 1].selected) {
        return false;
    }
    for (var i = selObj.options.length - 1; i >= 0; i--) {
        var op = selObj.options[i];
        if (op.selected) {
            selObj.remove(i);
            selObj.options.add(op, i + 1);
        } else {
            continue;
        }
    }
}
//全选
function selAll(id) {
    var selId = id;
    var selObj = document.getElementById(selId);
    if (typeof selObj == "undefined" || selObj == null || selObj == false) {
        return false;
    }
    for (var i = 0; i < selObj.options.length; i++) {
        selObj.options[i].selected = true;
    }
}
//左、右移动
function moveSelOp(selFromId, selToId) {
    var selFromObj = document.getElementById(selFromId);
    var selToObj = document.getElementById(selToId);
    for (var i = 0; i < selFromObj.options.length; i++) {
        var selFromField = selFromObj.options[i];
        if (!selFromField.selected) {
            continue;
        } else {
            var selToField = document.createElement("OPTION");
            selToField.text = selFromField.text;
            selToField.value = selFromField.value;
            selToObj.options.add(selToField);
            selFromObj.removeChild(selFromField);
            i--;
        }
    }
}
//=================动态菜单tab标签========================
function AddTabMenu(tabid, url, name, Isclose, IsReplace) {
    SetSystemId(tabid);
    if (url == "" || url == "#") {
        url = "/ErrorPage/404.aspx";
    }
    var tabs_container = top.$("#tabs_container");
    var ContentPannel = top.$("#ContentPannel");
    if (IsReplace == 'true') {
        top.RemoveDiv(tabid);
    }
    if (top.document.getElementById("tabs_" + tabid) == null) { //如果当前tabid存在直接显示已经打开的tab
        Loading(true);
        tabs_container.find('div').removeClass('selected');
        ContentPannel.find('iframe').hide();
        if (Isclose != 'false') { //判断是否带关闭tab
            tabs_container.append("<div id=\"tabs_" + tabid + "\" class=\"selected\"><a class=\"tab\">" + name + "</a><a onclick=\"RemoveDiv('" + tabid + "')\" class=\"close\" title=\"关闭当前窗口\"></a></div>");
            tabs_container.find('#tabs_' + tabid).find('.tab').attr("onclick", "javascript:AddTabMenu('" + tabid + "','" + url + "','" + name + "','true');");
            tabs_container.find('#tabs_' + tabid).attr("Is_Win_close", "true");
        } else {
            tabs_container.append("<div id=\"tabs_" + tabid + "\" class=\"selected\" onclick=\"javascript:AddTabMenu('" + tabid + "','" + url + "','" + name + "','false')\"><a class=\"tab\">" + name + "</a></div>");
        }
        ContentPannel.append("<iframe id=\"tabs_iframe_" + tabid + "\" name=\"tabs_iframe_" + tabid + "\" height=\"100%\" width=\"100%\" src=\"" + url + "\" frameBorder=\"0\"></iframe>");
    }
    else {
        tabs_container.find('div').removeClass('selected');
        ContentPannel.find('iframe').hide();
        tabs_container.find('#tabs_' + tabid).addClass('selected');
        top.document.getElementById("tabs_iframe_" + tabid).style.display = 'block';
    }
}
//关闭所有tab
function All_CloseTabMenu() {
    top.$("#div_tab").find("[Is_Win_close=true]").each(function () {
        RemoveDiv($(this).attr('id'))
    });
}
//关闭当前tab
function ThisCloseTab() {
    var tabs_container = top.$("#tabs_container");
    top.RemoveDiv(tabs_container.find('.selected').attr('id').substr(5));
}
//关闭事件
function RemoveDiv(obj) {
    var tabs_container = top.$("#tabs_container");
    var ContentPannel = top.$("#ContentPannel");
    tabs_container.find("#tabs_" + obj).remove();
    ContentPannel.find("#tabs_iframe_" + obj).remove();
    var tablist = tabs_container.find('div');
    var pannellist = ContentPannel.find('iframe');
    if (tablist.length > 0) {
        tablist[tablist.length - 1].className = 'selected';
        pannellist[tablist.length - 1].style.display = 'block';
    }
}
//设置模块ID存储 Session，用来显示按钮
function SetSystemId(MenuId) {
    //getAjax("/Frame/Frame.ashx", "action=SetSystemId&SystemId=" + MenuId, function (data) {
    //})
    getAjax("/Session/Execute", "action=SetSystemId&SystemId=" + MenuId, function (data) {
    })
}