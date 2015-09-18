//样式
function readyIndex() {
    $("#toolbar img").hover(function () {
        $(this).addClass("pageBase_Div");
    }, function () {
        $(this).removeClass("pageBase_Div");
    });
    $("#topnav li").click(function () {
        $("#topnav li").find('a').removeClass("onnav")
        $(this).find('a').addClass("onnav");
    });
    $(".sub-menu div").click(function () {
        $('.sub-menu div').removeClass("selected")
        $(this).addClass("selected");
    }).hover(function () {
        $(this).addClass("navHover");
    }, function () {
        $(this).removeClass("navHover");
    });
    $(".sub-menu").hover(function () {
        $(this).css("overflow", "auto");
    }, function () {
        $(this).css("overflow", "hidden");
    });
}
/**自应高度**/
var MainContent_subtract = 122;
var Sidebar_subtract = 148;
function iframeresize() {
    resizeU();
    $(window).resize(resizeU);
    function resizeU() {
        var divkuangH = $(window).height();
        $("#MainContent").height(divkuangH - MainContent_subtract - 1);
    }
}
/**安全退出**/
function IndexOut(path,toPath) {
    top.showConfirmMsg('您好：请您确认是否退出系统？', function (r) {
        if (r) {
            getAjax(path, "action=Outlogin", function (data) {
                window.location.href = toPath;
            })
        }
    });
}
//当前日期
function writeDateInfo() {
    var now = new Date();
    var year = now.getFullYear(); //getFullYear getYear
    var month = now.getMonth();
    var date = now.getDate();
    var day = now.getDay();
    var hour = now.getHours();
    var minu = now.getMinutes();
    var sec = now.getSeconds();
    var week;
    month = month + 1;
    if (month < 10) month = "0" + month;
    if (date < 10) date = "0" + date;
    if (hour < 10) hour = "0" + hour;
    if (minu < 10) minu = "0" + minu;
    if (sec < 10) sec = "0" + sec;
    var arr_week = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
    week = arr_week[day];
    var time = "";
    time = year + "年" + month + "月" + date + "日" + " " + hour + ":" + minu + ":" + sec;

    $("#datetime").text(time);
    var timer = setTimeout("writeDateInfo()", 1000);
}
var Contentheight = "";
var Contentwidth = "";
var FixedTableHeight = "";
//最大化
function Maximize() {
    $("#Header").hide();
    $("#full_screen").attr('src', '/Content/Themes/Images/16/arrow_inout.gif').attr('title', '还原').attr('onclick', 'Fullrestore()');
    MainContent_subtract = MainContent_subtract - 70;
    Sidebar_subtract = Sidebar_subtract - 70;
    iframeresize();
}
//还原
function Fullrestore() {
    $("#Header").show();
    $("#full_screen").attr('src', '/Content/Themes/Images/16/arrow_out.gif').attr('title', '最大化').attr('onclick', 'Maximize()');
    MainContent_subtract = MainContent_subtract + 70;
    Sidebar_subtract = Sidebar_subtract + 70;
    iframeresize();
}
//=================动态菜单tab标签========================
function AddTabMenu(tabid, url, name, img, Isclose, IsReplace) {
    SetSystemId(tabid);
    if (url == "" || url == "#") {
        url = '@Url.Action("Page404", "Error", new { Area = "Common" })';
    }
    var tabs_container = top.$("#tabs_container");
    var ContentPannel = top.$("#ContentPannel");
    if (IsReplace == 'true') {
        top.RemoveDiv(tabid);
    }
    if (Isclose != 'false') { //判断是否带关闭tab
        top.$(".navigation").hide();
    } else {
        top.$(".navigation").show();
    }
    if (top.document.getElementById("tabs_" + tabid) == null) { //如果当前tabid存在直接显示已经打开的tab
        Loading(true);
        tabs_container.find('li').removeClass('selected');
        ContentPannel.find('iframe').hide();
        if (Isclose != 'false') { //判断是否带关闭tab
            tabs_container.append("<li id=\"tabs_" + tabid + "\" class='selected' win_close='true'><span title='" + name + "' onclick=\"javascript:AddTabMenu('" + tabid + "','" + url + "','" + name + "','" + img + "','true')\"><a href=\"javascript:;\"><img src=\"/Content/Themes/Images/32/" + img + "\" width=\"20\" height=\"20\">" + name + "</a></span><a class=\"win_close\" title=\"关闭当前窗口\" onclick=\"RemoveDiv('" + tabid + "')\"></a></li>");
        } else {
            tabs_container.append("<li id=\"tabs_" + tabid + "\" class=\"selected\" onclick=\"javascript:AddTabMenu('" + tabid + "','" + url + "','" + name + "','" + img + "','false')\"><a><img src=\"/Content/Themes/Images/32/" + img + "\" width=\"20\" height=\"20\">" + name + "</a></li>");
        }
        ContentPannel.append("<iframe id=\"tabs_iframe_" + tabid + "\" name=\"tabs_iframe_" + tabid + "\" height=\"100%\" width=\"100%\" src=\"" + url + "\" frameBorder=\"0\"></iframe>");
    }
    else {
        tabs_container.find('li').removeClass('selected');
        ContentPannel.find('iframe').hide();
        tabs_container.find('#tabs_' + tabid).addClass('selected');
        top.document.getElementById("tabs_iframe_" + tabid).style.display = 'block';
    }
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
    var tablist = tabs_container.find('li');
    var pannellist = ContentPannel.find('iframe');
    if (tablist.length > 0) {
        tablist[tablist.length - 1].className = 'selected';
        pannellist[tablist.length - 1].style.display = 'block';
    }
    if (tablist.length == '1') {
        top.$(".navigation").show();
    }
}