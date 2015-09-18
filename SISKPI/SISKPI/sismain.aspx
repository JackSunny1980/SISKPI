<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sismain.aspx.cs" Inherits="SISKPI.sismain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>神华国华：运行实时绩效管理系统(KPI)</title>
    <link rel="shortcut icon" href="favicon.ico" />
    <link href="CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="js/DatePicker/WdatePicker.js"
        defer="defer"></script>
    <script src="js/ProcessBar.js" type="text/javascript"></script>
    <script src="js/Common.js" type="text/javascript"></script>
    <%--
    <script src="js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="js/tab/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="js/Tab/jquery.tabs.js" type="text/javascript"></script>
    <script src="js/Config.js" type="text/javascript"></script>
    <script src="js/GridViewColor.js" type="text/javascript"></script>
    --%>
    <script language="JavaScript" type="text/javascript">
        self.moveTo(0, 0);
        self.resizeTo(screen.availWidth, screen.availHeight);
        //self.focus();
    </script>
</head>
<body>
    <frameset rows="40,*" scrolling="no"  cols="*" frameborder="NO" border="0" framespacing="0" id="frame1">
    <frame name="banner" scrolling="no"  noresize src="sistop.aspx" frameborder="0">
    </frame>
    <frameset rows="*"  cols="167,11,*" frameborder="YES" border="0" framespacing="0" id="frame2">
       <frame name="leftmenu" scrolling="no"  src="sisleft.aspx" frameborder="0">
       </frame>
       <frame name="middle" scrolling="no"  src="sismiddle.htm" frameborder="0">
       </frame>
       <frame name="main" scrolling="auto" src="sispitrend.htm" frameborder="0" id="mainpage">
		<noframes>
			<p id="p1">
				此 HTML 框架集显示多个 Web 页。若要查看此框架集，请使用支持 HTML 4.0 及更高版本的 Web 浏览器。
			</p>
		</noframes>
        </frame>
    </frameset>
</frameset>
</body>
</html>
