<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sistop.aspx.cs" Inherits="SISKPI.sistop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>SISKPI</title>
    <link rel="shortcut icon" href="favicon.ico" />
    <link href="CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="js/DatePicker/WdatePicker.js"
        defer="defer"></script>
    <script src="js/ProcessBar.js" type="text/javascript"></script>
    <script src="js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="js/Common.js" type="text/javascript"></script>
    <script src="js/tab/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="js/Tab/jquery.tabs.js" type="text/javascript"></script>
    <script src="js/Config.js" type="text/javascript"></script>
    <script src="js/GridViewColor.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            background: url(images/head_logo.png) repeat-x;
            font-size: 12px;
            line-height: 100%;
            margin: 0px;
            padding: 0px;
            color: White;
            font-weight: bold;
        }
        
        .lblstyle
        {
            border: #D87E26 0px solid;
            cursor: hand;
            color: white;
            background: none;
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

        function setCalendar() {
            calendar = new Date();
            day = calendar.getDay();
            month = calendar.getMonth();
            date = calendar.getDate();
            year = calendar.getYear();
            if (year < 2000) year = 1900 + year;
            cent = parseInt(year / 100);
            g = year % 19;
            k = parseInt((cent - 17) / 25);
            i = (cent - parseInt(cent / 4) - parseInt((cent - k) / 3) + 19 * g + 15) % 30;
            i = i - parseInt(i / 28) * (1 - parseInt(i / 28) * parseInt(29 / (i + 1)) * parseInt((21 - g) / 11));
            j = (year + parseInt(year / 4) + i + 2 - cent + parseInt(cent / 4)) % 7;
            l = i - j;
            emonth = 3 + parseInt((l + 40) / 44);
            edate = l + 28 - 31 * parseInt((emonth / 4));
            emonth--;
            var dayname = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
            var monthname = new Array("1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月");

            document.getElementById("nowd").innerHTML = "今天是 " + year + "年" + monthname[month] + date + "日 " + dayname[day];
        }

        function setMainURL() {
            var url = parent.parent.main.location.href;
            document.getElementById('txtMainURL').value = url;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" cellspacing="0">
    <div style="width: 100%; height: 100%; margin: 0 0 0 0;" cellspacing="0">
        <table style="width: 100%; height: 100%; margin: 0 0 0 0; border: 0;" cellspacing="0">
            <%--background-image: url(Images/head_logo.png); --%>
            <tr style="width: 100%; border: 0;" cellspacing="0">
                <td style="width: 30%; border: 0;" cellspacing="0" align="left" valign="middle">
                    <img src="Images/logo.gif" height="32px" border='0' alt="厂级监控信息系统(SIS)" />
                </td>
                <td style="width: 300px;" cellspacing="0">
                    <input id="txtMainURL" type="text" runat="server" style="visibility: hidden" />
                </td>
                <td style="width: 10%; border: 0;" cellspacing="0" valign="middle" align="left">
                    <img src="Images/1000.gif" height="16px" border='0' alt="S" /><asp:Button ID="btnFirst" 
                        CssClass="lblstyle" runat="server" Text="首页" ToolTip="导航到首页" OnClick="btnFirst_Click" />
                </td>
                <td style="width: 10%; border: 0;" cellspacing="0" valign="middle" align="left">
                    <img src="Images/1004.gif" height="16px" border='0' alt="S" /><asp:Button ID="btnSetFirst"
                        CssClass="lblstyle" runat="server" Text="置为首页" ToolTip="设置首页" OnClick="btnSetFirst_Click" />
                </td>
                <td style="width: 10%; border: 0;" cellspacing="0" valign="middle" align="left">
                    <img src="Images/1003.gif" height="16px" border='0' alt="S" /><asp:Button ID="btnLog"
                        CssClass="lblstyle" runat="server" Text="登录" ToolTip="更换用户" OnClick="btnLog_Click" />
                </td>
                <td id="Td1" style="width: 10%; border: 0;" cellspacing="0" align="right">
                    <asp:Button ID="btnPassword" CssClass="lblstyle" runat="server" Text="欢迎您:" ToolTip="编辑密码"
                        OnClick="btnPassword_Click" />
                </td>
                <td id="nowd" style="width: 30%; border: 0; font-size: 12px" cellspacing="0" align="right">
                    今天是 2009年10月09日 星期五
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        setCalendar();
    </script>
    </form>
</body>
</html>
