<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_Mgr.aspx.cs" Inherits="SISKPI.KPI_Mgr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>神华国华：运行实时绩效管理系统(KPI)</title>
    <link rel="shortcut icon" href="favicon.ico" />
    <link href="CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <%--    <script language="javascript" type="text/javascript" src="js/DatePicker/WdatePicker.js"
        defer="defer"></script>--%>
    <script src="js/ProcessBar.js" type="text/javascript"></script>
    <script src="js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="js/tab/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="js/Tab/jquery.tabs.js" type="text/javascript"></script>
    <%--    <script src="js/Common.js" type="text/javascript"></script>
    <script src="js/Config.js" type="text/javascript"></script>--%>
    <script src="js/GridViewColor.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        //        $(document).ready(function () {
        //            call();
        //        });

        //        function call() {
        //            SetTableWidth('table1');
        //            SetDivWidth('div1');
        //            SetDivWidth('divtag');
        //            SetTableWidth('table2');
        //            //SetTableWidth('table3');
        //        }

        function setFrameHW() {
            //var height = document.documentElement.clientHeight;  //window.screen.height;
            //document.getElementById("kpiInfor").style.height = height + "px";
            //document.getElementById("kpiReport").style.height = height + "px";
            //document.getElementById("divmgr").style.height = height + "px";

            var width = document.documentElement.clientWidth - 200;
            document.getElementById("divleft").style.width = "200px";
            document.getElementById("divright").style.width = width + "px";
        }
        
    </script>
</head>
<body onload="setFrameHW();" style="width: 100%; height: 100%; background-color: White;">
    <form id="form1" runat="server" cellspacing="0" style="width: 100%; height: 100%;">
    <div id="divmgr" style="width: 100%; height: 100%; float: left; vertical-align: top;">
        <div id="divleft" style="width: 195px; height: 550px; float: left; vertical-align: top;">
            <iframe name="IframeMgrLeft" width="100%" height="100%" src="KPI_MgrLeft.aspx" runat="server"
                frameborder="0" scrolling="yes" style="overflow: auto;"></iframe>
        </div>
       <%-- <div id="divmiddle" style="width: 11px; height: 550px; float: left; vertical-align: top;">
            <iframe name="IframeMgrMiddle" width="100%" height="100%" src="kpimiddle.htm"  runat="server"
                frameborder="0" scrolling="no" style="overflow: auto;"></iframe>
        </div>--%>
        <div id="divright" style="width: auto; height: 550px; float: right; vertical-align: top;">
            <iframe name="IframeMgrRight" width="100%" height="100%" src="sisintro.htm" runat="server"
                frameborder="0" scrolling="auto" style="overflow: auto;"></iframe>
        </div>
    </div>
    </form>
</body>
</html>
