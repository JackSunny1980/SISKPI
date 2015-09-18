//得到网站根路径
function getRootPath() {
    var strFullPath = window.document.location.href;
    var strPath = window.document.location.pathname;
    var pos = strFullPath.indexOf(strPath);
    var prePath = strFullPath.substring(0, pos);
    var postPath = strPath.substring(0, strPath.substr(1).indexOf('/') + 1);
    return (prePath );
}
//得到url参数值
function getQueryString(paramName) {
    paramName = paramName.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]").toLowerCase();
    var reg = "[\\?&]" + paramName + "=([^&#]*)";
    var regex = new RegExp(reg);
    var regResults = regex.exec(window.location.href.toLowerCase());
    if (regResults == null) return "";
    else return regResults[1];
}

function SetTableWidth(table) {
    var screenHeight = window["mainWindow"].offsetHeight;
    var screenWidth = window["mainWindow"].offsetWidth-40;
    $("#" + table).attr("width", screenWidth);
}

function SetDivWidth(div) {
    var screenWidth = window["mainWindow"].offsetWidth - 40
    $("#" + div).css({ 'width': screenWidth });
}
 
