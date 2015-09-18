<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sismenutree.aspx.cs" Inherits="SISKPI.sismenutree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head id="Head1" runat="server">
    <title>SISKPI</title>
    <link href="Default/style.css" rel="stylesheet" type="text/css" />
    <link href="Default/theme.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="Default/JS/JSCookTree.js"></script>
    <script type="text/javascript" language="JavaScript" src="Default/JS/theme.js"></script>
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
    <style>
        a
        {
            text-decoration: none;
        }
    </style>
    <script type="text/javascript" language="JavaScript">
    
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


        var myMenu =[<%=SysMenu %>];

    </script>
</head>
<body style="font-family: MS Shell Dlg; font-size: 12px; background-color: White;">
    <div id="myMenuID">
    </div>
</body>
<script type="text/javascript" language="javascript">

    ctDraw('myMenuID', myMenu, ctThemeXP1, 'ThemeXP', 0, 0);

</script>
</html>
