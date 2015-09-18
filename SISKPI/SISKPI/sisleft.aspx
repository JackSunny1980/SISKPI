<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sisleft.aspx.cs" Inherits="SISKPI.sisleft" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head>
    <title></title>
    <link href="Default/style.css" rel="stylesheet" type="text/css" />
    <%--   
    <script language="javascript" type="text/javascript" src="js/DatePicker/WdatePicker.js"
        defer="defer"></script>
    --%>
    <script src="js/ProcessBar.js" type="text/javascript"></script>
    <script src="js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="js/Common.js" type="text/javascript"></script>
    <script src="js/tab/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="js/Tab/jquery.tabs.js" type="text/javascript"></script>
    <script src="js/Config.js" type="text/javascript"></script>
    <%--   
    <script src="js/GridViewColor.js" type="text/javascript"></script>
    --%>
    <style type="text/css" id="popupmanager">
        .popupMenu
        {
            border-right: #666666 1px solid;
            padding-right: 1px;
            border-top: #666666 1px solid;
            padding-left: 1px;
            padding-bottom: 1px;
            border-left: #666666 1px solid;
            width: 100px;
            padding-top: 1px;
            border-bottom: #666666 1px solid;
            background-color: #f9f8f7;
        }
        
        .popupMenuTable
        {
            background-repeat: repeat-y;
        }
        .popupMenuTable TD
        {
            font-size: 12px;
            cursor: default;
            font-family: MS Shell Dlg;
        }
        .popupMenuRow
        {
            padding-right: 1px;
            padding-left: 1px;
            padding-bottom: 1px;
            padding-top: 1px;
            height: 21px;
        }
        .popupMenuRowHover
        {
            border-right: #0a246a 1px solid;
            border-top: #0a246a 1px solid;
            border-left: #0a246a 1px solid;
            border-bottom: #0a246a 1px solid;
            height: 21px;
            background-color: #b6bdd2;
        }
        .popupMenuSep
        {
            left: 28px;
            width: expression(parentElement.offsetWidth-27);
            position: relative;
            height: 1px;
            background-color: #a6a6a6;
        }
        
        BODY
        {
            padding-right: 0px;
            padding-left: 0px;
            padding-bottom: 0px;
            margin: 0px;
            padding-top: 0px;
            background-color: White;
        }
        #divMenuBox TD
        {
            padding-right: 0px;
            padding-left: 0px;
            padding-bottom: 0px;
            padding-top: 0px;
        }
        #divFavContent
        {
            display: none;
        }
        *
        {
            font-size: 12px;
            font-family: MS Shell Dlg;
        }
        IMG
        {
            vertical-align: middle;
        }
    </style>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            call();
        });

        function call() {
            SetTableWidth('table1');
            SetDivWidth('div1');
            SetDivWidth('divtag');
            SetTableWidth('table2');
            //SetTableWidth('table3');
        }

        function sismenuHideLeft() {
            var strLocal1 = "200,*";
            var strLocal2 = "7,*";

            alert("abb");

            //            if (parent.frames[2].imgleft.innerText == 3)//Is display state;
            //            {
            //                var strLocal = "0," + String(dMiddleWidth) + ",*";
            //                top.MainFrame.cols = strLocal;
            //                top.ChildFrame.rows = "0,*,0";

            //                parent.bLeftStatus = false;
            //                parent.frames[1].imgleft.innerText = 4;
            //            } else {
            //                var strLocal = String(dLeftWidth) + "," + String(dMiddleWidth) + ",*";
            //                top.MainFrame.cols = strLocal;
            //                top.ChildFrame.rows = "20,*,20";

            //                parent.bLeftStatus = true;
            //                parent.frames[1].imgleft.innerText = 3;

            //                MChangePBSize(false);
            //            }
        }

    </script>
</head>
<body>
    <script type="text/javascript" language="javascript">
var prefixes = ["MSXML2.DomDocument", "Microsoft.XMLDOM", "MSXML.DomDocument", "MSXML3.DomDocument"];
var dom;

var FolderCount = <%=RowCount*2 %>;
var thumbCount = 4;
var FolderLeavings = 4;
var menuMargin = 14;

var arrayFolder;
var tbl;
var fileTD;
var rightArrow;
var handleOffsetHeight;

var currentThumbCount,currentMenuId,currentTD;

function getDomObject(){
	for (var i = 0; i < prefixes.length; i++) {
		try{dom = new ActiveXObject(prefixes[i]);}catch(ex){};
	}
}

window.onload = function(){
	/*initialize*/
	getDomObject();
	getXML();
	getCookie();
	//currentMenuId = 0;
	createMenu();
	//document.getElementById("ifrm").contentWindow.attachEvent("onresize",loadHTML2);

	arrayFolder = new Array();
	tbl = document.getElementById("tbl");
	fileTD = document.getElementById("fileTD");
	rightArrow = document.getElementById("rightArrow");
	
	for(var i=0;i<currentThumbCount;i++){
		delRow();
	}

	memorizeThumb();

	getHandleOffsetHeight();
};

window.onbeforeunload = function(){
	setCookie(currentThumbCount,currentMenuId);
};

window.onresize = function(){
	if(window.document.body.offsetWidth<120) parent.document.getElementById("frame2").cols = "120,*";

	memorizeThumb();
	getHandleOffsetHeight();
};

function getXML(){
	dom.async = false;
	dom.loadXML('<%=strSysMenu%>');	
}

function getHandleOffsetHeight(){
	var tblTop = 0;
	obj = tbl.rows[3];
	while(obj.tagName!="BODY"){
		tblTop += obj.offsetTop;
		obj = obj.offsetParent;
	}
	handleOffsetHeight = tblTop;
}

function memorizeThumb(){
	var remainRows = Math.ceil((document.body.clientHeight-menuMargin-102)/23)-1;
	if(tbl.rows.length-4>remainRows){
		for(var i=1;i<=tbl.rows.length-4-remainRows;i++){
			if(tbl.rows.length>4){
				delRow();
			}
		}
	}else if(tbl.rows.length-4<remainRows){
		for(var i=1;i<=remainRows-(tbl.rows.length-4);i++){
			if(arrayFolder.length>currentThumbCount){
				addRow();
			}
		}
	}
}

function loadHTML(){
	with(document.getElementById("ifrm")){
		try{
			contentWindow.document.body.style.margin = "0";
			contentWindow.document.body.style.padding = "0";
			var nodeMenubar = dom.selectSingleNode("//menubar[@id='"+currentMenuId+"']");

			if(nodeMenubar.getAttribute("extra")==null){
				//contentWindow.document.body.innerHTML = "<div style='padding:10px;font-size:12px'>"+nodeMenubar.firstChild.text+"</div>";
				contentWindow.document.body.innerHTML = "";
				detachEvent("onload",loadHTML);
				src = "sismenutree.aspx?id="+dom.selectSingleNode("//menubar[@id='"+currentMenuId+"']").getAttribute("levelid");
			}else if(nodeMenubar.getAttribute("extra")=="systemSetting"){
				detachEvent("onload",loadHTML);
				src = "";
			}
		}catch(e){}
	}
}

function loadHTML2(){
	var nodeMenubar = dom.selectSingleNode("//menubar[@id='"+currentMenuId+"']");
	if(nodeMenubar.getAttribute("extra")==null){
		with(document.getElementById("ifrm").contentWindow.document.body){
			style.fontFamily = "MS Shell Dlg";
			style.fontSize = "12px";
			innerHTML = nodeMenubar.firstChild.text;
		}
	}
}

function createMenu(){
	var oTbl,oTR,oTD;

	oTbl = document.createElement("table");
	oTbl.id = "tbl";
	oTbl.cellSpacing = "1";
	oTbl.className = "OTTable";

	oTR = oTbl.insertRow();
	oTD = oTR.insertCell();
	if(dom.selectSingleNode("//menubar[@id='"+currentMenuId+"']")==null){
		currentMenuId=0;
	}
	oTD.setAttribute("menuid",dom.selectSingleNode("//menubar[@id='"+currentMenuId+"']").getAttribute("id"));
	oTD.className = "folder";
	oTD.innerHTML = "<img class='topicon' src='"+dom.selectSingleNode("//menubar[@id='"+currentMenuId+"']").getAttribute("icon")+"'/>" +"<span class='topword'>"+ dom.selectSingleNode("//menubar[@id='"+currentMenuId+"']").getAttribute("name")+"</span><img class='topimg' src='Default/Images/top_bg.png' class='topimg'>";
	oTR = oTbl.insertRow();
	oTD = oTR.insertCell();
	oTD.id = "fileTD";
	oTD.className = "file";
	var oIframe = document.createElement("iframe");
	oIframe.id = "ifrm";
	oIframe.style.width = "100%";
	oIframe.style.height = "100%";
	oIframe.frameBorder = "0";
	oIframe.attachEvent("onload",loadHTML);
	oTD.appendChild(oIframe);

	oTR = oTbl.insertRow();
	oTD = oTR.insertCell();
	oTD.className = "handle";
	oTD.onmousedown = function(){mousedown();};
	oTD.onmouseup = function(){mouseup();};
	oTD.onmousemove = function(){mousemove();};

	var oNodes = dom.selectNodes("//menubar");
	for(i=0;i<oNodes.length;i++){
		oTR = oTbl.insertRow();
		oTD = oTR.insertCell();
		oTD.setAttribute("menuid",dom.selectSingleNode("//menubar["+i+"]").getAttribute("id"));
		oTD.className = "folder";
		oTD.attachEvent("onmouseover",folderMouseOver);
		oTD.attachEvent("onmouseout",folderMouseOut);
		oTD.onclick = function(){slideFolder(this);};
		oTD.innerHTML = "<img class='topicon' src='"+dom.selectSingleNode("//menubar["+i+"]").getAttribute("icon")+"'/>" +"<span class='topword'>"+dom.selectSingleNode("//menubar["+i+"]").getAttribute("name")+"</span><img class='topimg' src='Default/Images/top_bg.png' class='topimg'>";
	}

	oTR = oTbl.insertRow();
	oTD = oTR.insertCell();
	oTD.id = "thumbBox";
	//oTD.innerHTML = "<img id='rightArrow' src='Default/Images/open_win_arrow.gif'/>" ; //onclick=alert('hello')
	document.getElementById("divMenuBox").rows[0].cells[0].appendChild(oTbl);
}

function folderMouseOver(){
	var o = window.event.srcElement;
	var this_width = o.width;
	if(o.tagName!="TD"){
		o.parentNode.className="folderMouseOver";
	}
	//if(o.tagName=="IMG") o.className = "topimgover";
}

function folderMouseOut(){
	var o = window.event.srcElement;
	if(o.tagName!="TD"){
		if(o.parentNode.menuid!=currentMenuId){
			o.parentNode.className="folder";
		}
	}
}

function hideLoading(){
	document.getElementById("loading").style.display = "none";
	document.getElementById("ifrm").detachEvent("onload",hideLoading);
}


function slideFolder(o){
	//o.detachEvent("onmouseover",folderMouseOver);
	//o.detachEvent("onmouseout",folderMouseOut);
	//alert(currentTD);
	if(currentTD!=null && currentTD!=o.getAttribute("menuid")){
		if(tbl.rows[3+parseInt(currentTD)]!=null && (tbl.rows.length-1)!=(3+parseInt(currentTD))){
			tbl.rows[3+parseInt(currentTD)].cells[0].className = "folder";
			tbl.rows[3+parseInt(currentTD)].cells[0].attachEvent("onmouseover",folderMouseOver);
			tbl.rows[3+parseInt(currentTD)].cells[0].attachEvent("onmouseout",folderMouseOut);
		}
	}
	currentTD = o.getAttribute("menuid");
	
	//document.getElementById(currentMenuId).attachEvent("onmouseover",folderMouseOver);
	//document.getElementById(currentMenuId).attachEvent("onmouseout",folderMouseOut);

	clickRow = o.parentElement.rowIndex;
	currentMenuId = o.getAttribute("menuid");

	var nodeMenubar = dom.selectSingleNode("//menubar[@id='"+currentMenuId+"']");
	tbl.rows[0].firstChild.innerHTML = "<img class='topicon' src='"+dom.selectSingleNode("//menubar[@id='"+currentMenuId+"']").getAttribute("icon")+"'/>" +"<span class='topword'>"+ dom.selectSingleNode("//menubar[@id='"+currentMenuId+"']").getAttribute("name")+"</span><img class='topimg' src='Default/Images/top_bg.png' class='topimg'>";

	var oIframe = document.getElementById("ifrm");

	if(nodeMenubar.getAttribute("extra")==null){
		//oIframe.contentWindow.attachEvent("onresize",loadHTML2);
		//oIframe.contentWindow.document.body.innerHTML = nodeMenubar.firstChild.text;
		loadHTML();
	}else if(nodeMenubar.getAttribute("extra")=="systemSetting"){
		//oIframe.contentWindow.document.body.innerHTML = "Loading...";
		//fileTD.insertAdjacentHTML("afterBegin","<div id='loading'' style='font-family:MS Shell Dlg;font-size:12px;color:#999;font-weight:bold;margin:5px'><img src='/images/loading2.gif' style='vertical-align:middle;margin-right:0'/> Loading...</div>");
		oIframe.detachEvent("onload",loadHTML);
		//oIframe.attachEvent("onload",hideLoading);
		oIframe.src = "/MainMenuTree.jsp";
	}
	//setCookie(currentThumbCount,currentMenuId);
}

function mousedown(){
	el = window.event.srcElement;
	while(el.tagName!="TD"){
		el = el.parentElement;
	}
	el.setCapture();
}

function mouseup(){
	el.releaseCapture();
}

function mousemove(){
	getHandleOffsetHeight();

	window.event.cancelBubble = false;
	cliX = window.event.clientX;
	cliY = window.event.clientY;

	if(cliY<100) return false;
	if(cliY>handleOffsetHeight+22 && tbl.rows.length<=FolderCount && tbl.rows.length>FolderLeavings){
		delRow();
		currentThumbCount++;
		handleOffsetHeight+=22;
	}
	if(cliY<handleOffsetHeight-22 && tbl.rows.length<FolderCount && tbl.rows.length>=FolderLeavings){
		addRow();
		currentThumbCount--;
		handleOffsetHeight-=22;
	}
	//setCookie(currentThumbCount,currentMenuId);
}

function delRow(){

	if(tbl.rows[tbl.rows.length-2].firstChild.className=="handle") return false;
	arrayFolder.push(tbl.rows[tbl.rows.length-2].firstChild.getAttribute("menuid")+"|"+tbl.rows[tbl.rows.length-2].firstChild.innerHTML);
	
	var oImg = document.createElement("img");
	oImg.setAttribute("menuid",tbl.rows[tbl.rows.length-2].firstChild.getAttribute("menuid"));

//alert(tbl.rows[tbl.rows.length-2].firstChild.children[0].nextSibling.innerHTML);
	oImg.setAttribute("menuname",tbl.rows[tbl.rows.length-2].firstChild.children[0].nextSibling.innerHTML);
	oImg.src = tbl.rows[tbl.rows.length-2].firstChild.firstChild.src;
	oImg.onclick = function(){slideFolder(this)};

	if(arrayFolder.length<=thumbCount){
		tbl.rows[tbl.rows.length-1].firstChild.insertBefore(oImg,rightArrow);
	}else{
		insertToPopupMenu(oImg);
	}

	tbl.deleteRow(tbl.rows.length-2);
}

function insertToPopupMenu(o){
	var tbl,tbl2,tr,td;
	tbl = document.createElement("table");
	tbl.cellspacing = 0;
	tbl.cellpadding = 0;
	tbl.width = "100%";
	tbl.height = "100%";
	tr = tbl.insertRow();
	td = tr.insertCell();
	td.width = 28;
	td.innerHTML = "<img src='"+o.src+"'/>";
	td = tr.insertCell();
	td.innerHTML = o.getAttribute("menuname");

	tbl2 = document.getElementById("divFavContent").firstChild.firstChild;
	tr = tbl2.insertRow();
	td = tr.insertCell();
	tr.height = 22;
	td.className = "popupMenuRow";
	td.setAttribute("menuid",o.getAttribute("menuid"));
	td.appendChild(tbl);
}

function addRow(){
	var oTR = tbl.insertRow(tbl.rows.length-1);
	var oTD = document.createElement("td");
	var arrayTmp = arrayFolder.pop().split("|");
	oTD.setAttribute("menuid",arrayTmp[0]);
	oTD.innerHTML = arrayTmp[1];
	if(arrayTmp[0]==currentMenuId){
		oTD.className = "folderMouseOver";
	}else{
		oTD.className = "folder";
		oTD.attachEvent("onmouseover",folderMouseOver);
		oTD.attachEvent("onmouseout",folderMouseOut);
	}
	oTD.onclick = function(){slideFolder(this)};
	oTR.appendChild(oTD);

	if(document.getElementById("divFavContent").firstChild.firstChild.rows.length>2){
		document.getElementById("divFavContent").firstChild.firstChild.deleteRow();
	}else{
		var tmp = tbl.rows[tbl.rows.length-1].firstChild;
		tmp.removeChild(tmp.children[tmp.children.length-2]);
	}
}

function setCookie(cThumbCount,cMenuId){ 
	var cookieDate = new Date();
	cookieDate.setTime(cookieDate.getTime() + 10*365*24*60*60*1000);
	document.cookie = "cookieLeftMenuWV00000026="+cThumbCount+","+cMenuId+";expires="+cookieDate.toGMTString();
}

function getCookie(){ 
	var cookieData = new String(document.cookie); 
	var cookieHeader = "cookieLeftMenuWV00000026=" 
	var cookieStart = cookieData.indexOf(cookieHeader) + cookieHeader.length; 
	var cookieEnd = cookieData.indexOf(";", cookieStart); 
	if(cookieEnd==-1){ 
		cookieEnd = cookieData.length;
	}
	if(cookieData.indexOf(cookieHeader)!=-1){ 
		currentThumbCount = cookieData.substring(cookieStart, cookieEnd).split(",")[0];
		currentMenuId = cookieData.substring(cookieStart, cookieEnd).split(",")[1];
	}else{
		currentThumbCount = 0;
		currentMenuId = dom.selectSingleNode("//menubar[0]").getAttribute("id");
	}
}



/*
==============================================
PopupMenu
==============================================
*/
var oPopup = window.createPopup();
function GetPopupCssText(){
	var styles = document.styleSheets;
	var csstext = "";
	for(var i=0; i<styles.length; i++){
		if (styles[i].id == "popupmanager")
			csstext += styles[i].cssText;
	}
	return csstext;
}

function showFav(){
	var popupX = 0;
	var popupY = 0;
	contentBox = document.getElementById("divFavContent");
	var o = event.srcElement;
	while(o.tagName!="BODY"){
		popupX += o.offsetLeft;
		popupY += o.offsetTop;
		o = o.offsetParent;
	}
	var oPopBody = oPopup.document.body;
	var s = oPopup.document.createStyleSheet();
	s.cssText = GetPopupCssText();
    oPopBody.innerHTML = contentBox.innerHTML;
	oPopBody.attachEvent("onmouseout",mouseout);

	//
	for(var i=0;i<oPopup.document.getElementsByTagName("TD").length;i++){
		if(oPopup.document.getElementsByTagName("TD")[i].getAttribute("menuid")!=null){
			oPopup.document.getElementsByTagName("TD")[i].onclick = function(){slideFolder(this);};
			oPopup.document.getElementsByTagName("TD")[i].onmouseover = function(){this.className='popupMenuRowHover';};
			oPopup.document.getElementsByTagName("TD")[i].onmouseout = function(){this.className='popupMenuRow';};
		}
	}

	oPopup.show(0, 0, 100, 0);
	var realHeight = oPopBody.scrollHeight;
	oPopup.hide();

	oPopup.show(popupX+20, popupY, 100, realHeight, document.body);
}

function mouseout(){
	var x = oPopup.document.parentWindow.event.clientX;
	var y = oPopup.document.parentWindow.event.clientY
	if(x<0 || y<0) oPopup.hide();
}
    </script>
    <div class="menu_css" id="menu_div">
        <%--  <img class="menubg" src="Default/Images/menu_bg.gif">--%>
        <table class="menuBox" id="divMenuBox" cellspacing="0">
            <tbody>
                <tr>
                    <%-- --%>
                    <td style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; padding-top: 0px;
                        height: 100%">
                    </td>
                    <%-- 
                    <td class="menu_css" id="menu_td" style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px;
                        width: 5px; padding-top: 0px; height: 100%; vertical-align: middle; background-image: url(Default/Images/menu_td_rt_bg.gif);"
                        onclick="sismenuHideLeft()">                        
                         <span id="imgleft"  style="cursor: hand; font-size:8; color: inactivecaption; font-family: Webdings">
                            3</span>
                        
                            <img class="menu_move" src="Default/Images/menu_srco_point.gif" onclick="<javascript> alert('hello')</javascript>">
                        <img class="menu_move_bg" src="Default/Images/menu_td_rt_bg.gif">
                       
                    </td> --%>
                </tr>
            </tbody>
        </table>
    </div>
    <%--
    <div id="divFavContent">
        <div class="popupMenu">
            <table class="popupMenuTable" height="100%" cellspacing="0" cellpadding="0" width="100%"
                border="0">
                <tbody>
                    <tr height="22">
                        <td class="popupMenuRow" id="popupWin_Menu_Setting" onmouseover="this.className='popupMenuRowHover';"
                            onmouseout="this.className='popupMenuRow';">
                            <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td width="28">
                                            &nbsp;
                                        </td>
                                        <td style="cursor: hand" onclick="parent.parent.main.location.href='/general/ipanel/shortcut/index.php';">
                                            个性设置
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr height="3">
                        <td>
                            <div class="popupMenuSep">
                                <img height="1"></div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    --%>
</body>
</html>
