<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_MgrLeft.aspx.cs" Inherits="SISKPI.KPI_MgrLeft" %>

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
            //document.getElementById("kpiTable").style.height = height + "px";

            //var width = window.screen.width;
            //document.getElementById("kpiTable").style.width = width + "px";
        }
        
    </script>
</head>
<body onload="setFrameHW();" style="background-color: White;">
    <form id="form1" runat="server" cellspacing="0">
    <div style="width: 200px; height: 100%; margin: 0; background-color: White;">
        <asp:TreeView ID="TVWMenu" runat="server" Width="100%" Height="100%" ShowLines="False">
            <%--OnSelectedNodeChanged="TVWMenu_SelectedNodeChanged"--%>
            <HoverNodeStyle Font-Underline="False" />
            <SelectedNodeStyle BorderColor="#FF3300" BackColor="Yellow" BorderWidth="0px" />
        </asp:TreeView>
    </div>
    </form>
</body>
</html>
