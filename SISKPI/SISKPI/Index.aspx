<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SISKPI.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>神华国华：运行实时绩效管理系统</title>
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
            margin-left: 0px;
            margin-top: 0px;
        }
        
        /*定义全局样式 */
        input, select, textarea, div
        {
            font: 12px Arial /* 此部分为表单和容器的字体定义 */;
        }
        /* 所有表单定义默认 */
        input, select, textarea
        {
            border: 1px solid #EFEFEF;
        }
        /* 利用鼠标事件 :hover 来定义当鼠标经过时样式 */
        input:hover, select:hover, textarea:hover
        {
            background: #F0F9FB;
            border: 1px solid #1D95C7;
        }
        /* 由于 :hover 事件只有 Mozilla 支持，因此为方便IE使用 expression 批量定义 */
        input.input, select, textarea
        {
            tesion: expression(onmouseover=function() 
	        {this.style.backgroundColor="#F0F9FB";this.style.border="1px solid #1D95C7"}, 
	        onmouseout=function()
	        {this.style.backgroundColor="#FFFFFF";this.style.border="1px solid #EFEFEF"});
        }
        
        .input-text
        {
            height: 20px;
            margin-left: 2px;
            margin-right: 4px;
        }
        #usercode
        {
            font-size: 12px;
            width: 100px;
        }
        #userpassword
        {
            font-size: 12px;
            width: 100px;
        }
        
        .STYLE1
        {
            font-size: 12px;
            color: #333333;
        }
        .STYLE3
        {
            font-size: 12px;
            margin-left: 10px;
            color: #6699FF;
        }
        a.STYLE3:hover
        {
            text-decoration: none;
        }
        a
        {
            font-size: 12px;
            color: #66CCFF;
        }
        a:hover
        {
            color: #3366FF;
        }
        a:link
        {
            color: #3366FF;
        }
    </style>
    <script language="javascript" type="text/javascript">
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
    
    </script>
</head>
<body oncontextmenu="return false;">

    <script language="JavaScript" type="text/javascript">                     
<!--
        dCol = "0000FF"
        fCol = "FF0000"
        sCol = "00FF00"
        mCol = "000000"
        hCol = "000000"
        ClockHeight = 40;
        ClockWidth = 40;
        ClockFromMouseY = 0;
        ClockFromMouseX = 100;

        d = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
        m = new Array("1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月");
        date = new Date();
        day = date.getDate();
        year = date.getYear();
        if (year < 2000) year = year + 1900;
        TodaysDate = "年 " + m[date.getMonth()] + " " + day + "日 " + d[date.getDay()] + " " + year;
        D = TodaysDate.split('');
        H = '...';
        H = H.split('');
        M = '....';
        M = M.split('');
        S = '.....';
        S = S.split('');
        Face = '1 2 3 4 5 6 7 8 9 10 11 12';
        font = 'Arial';
        size = 1;
        speed = 0.6;
        ns = (document.layers);
        ie = (document.all);
        Face = Face.split(' ');
        n = Face.length;
        a = size * 10;
        ymouse = 0;
        xmouse = 0;
        scrll = 0;
        props = "<font face=" + font + " size=" + size + " color=" + fCol + ">";
        props2 = "<font face=" + font + " size=" + size + " color=" + dCol + ">";
        Split = 360 / n;
        Dsplit = 360 / D.length;
        HandHeight = ClockHeight / 4.5
        HandWidth = ClockWidth / 4.5
        HandY = -7;
        HandX = -2.5;
        scrll = 0;
        step = 0.06;
        currStep = 0;
        y = new Array(); x = new Array(); Y = new Array(); X = new Array();
        for (i = 0; i < n; i++) { y[i] = 0; x[i] = 0; Y[i] = 0; X[i] = 0 }
        Dy = new Array(); Dx = new Array(); DY = new Array(); DX = new Array();
        for (i = 0; i < D.length; i++) { Dy[i] = 0; Dx[i] = 0; DY[i] = 0; DX[i] = 0 }
        if (ns) {
            for (i = 0; i < D.length; i++)
                document.write('<layer name="nsDate' + i + '" top=0 left=0 height=' + a + ' width=' + a + '><center>' + props2 + D[i] + '</font></center></layer>');
            for (i = 0; i < n; i++)
                document.write('<layer name="nsFace' + i + '" top=0 left=0 height=' + a + ' width=' + a + '><center>' + props + Face[i] + '</font></center></layer>');
            for (i = 0; i < S.length; i++)
                document.write('<layer name=nsSeconds' + i + ' top=0 left=0 width=15 height=15><font face=Arial size=3 color=' + sCol + '><center><b>' + S[i] + '</b></center></font></layer>');
            for (i = 0; i < M.length; i++)
                document.write('<layer name=nsMinutes' + i + ' top=0 left=0 width=15 height=15><font face=Arial size=3 color=' + mCol + '><center><b>' + M[i] + '</b></center></font></layer>');
            for (i = 0; i < H.length; i++)
                document.write('<layer name=nsHours' + i + ' top=0 left=0 width=15 height=15><font face=Arial size=3 color=' + hCol + '><center><b>' + H[i] + '</b></center></font></layer>');
        }
        if (ie) {
            document.write('<div id="Od" style="position:absolute;top:0px;left:0px"><div style="position:relative">');
            for (i = 0; i < D.length; i++)
                document.write('<div id="ieDate" style="position:absolute;top:0px;left:0;height:' + a + ';width:' + a + ';text-align:center">' + props2 + D[i] + '</font></div>');
            document.write('</div></div>');
            document.write('<div id="Of" style="position:absolute;top:0px;left:0px"><div style="position:relative">');
            for (i = 0; i < n; i++)
                document.write('<div id="ieFace" style="position:absolute;top:0px;left:0;height:' + a + ';width:' + a + ';text-align:center">' + props + Face[i] + '</font></div>');
            document.write('</div></div>');
            document.write('<div id="Oh" style="position:absolute;top:0px;left:0px"><div style="position:relative">');
            for (i = 0; i < H.length; i++)
                document.write('<div id="ieHours" style="position:absolute;width:16px;height:16px;font-family:Arial;font-size:16px;color:' + hCol + ';text-align:center;font-weight:bold">' + H[i] + '</div>');
            document.write('</div></div>');
            document.write('<div id="Om" style="position:absolute;top:0px;left:0px"><div style="position:relative">');
            for (i = 0; i < M.length; i++)
                document.write('<div id="ieMinutes" style="position:absolute;width:16px;height:16px;font-family:Arial;font-size:16px;color:' + mCol + ';text-align:center;font-weight:bold">' + M[i] + '</div>');
            document.write('</div></div>')
            document.write('<div id="Os" style="position:absolute;top:0px;left:0px"><div style="position:relative">');
            for (i = 0; i < S.length; i++)
                document.write('<div id="ieSeconds" style="position:absolute;width:16px;height:16px;font-family:Arial;font-size:16px;color:' + sCol + ';text-align:center;font-weight:bold">' + S[i] + '</div>');
            document.write('</div></div>')
        }
        (ns) ? window.captureEvents(Event.MOUSEMOVE) : 0;
        function Mouse(evnt) {
            ymouse = (ns) ? evnt.pageY + ClockFromMouseY - (window.pageYOffset) : event.y + ClockFromMouseY;
            xmouse = (ns) ? evnt.pageX + ClockFromMouseX : event.x + ClockFromMouseX;
        }
        (ns) ? window.onMouseMove = Mouse : document.onmousemove = Mouse;
        function ClockAndAssign() {
            time = new Date();
            secs = time.getSeconds();
            sec = -1.57 + Math.PI * secs / 30;
            mins = time.getMinutes();
            min = -1.57 + Math.PI * mins / 30;
            hr = time.getHours();
            hrs = -1.575 + Math.PI * hr / 6 + Math.PI * parseInt(time.getMinutes()) / 360;
            if (ie) {
                Od.style.top = window.document.body.scrollTop;
                Of.style.top = window.document.body.scrollTop;
                Oh.style.top = window.document.body.scrollTop;
                Om.style.top = window.document.body.scrollTop;
                Os.style.top = window.document.body.scrollTop;
            }
            for (i = 0; i < n; i++) {
                var F = (ns) ? document.layers['nsFace' + i] : ieFace[i].style;
                F.top = y[i] + ClockHeight * Math.sin(-1.0471 + i * Split * Math.PI / 180) + scrll;
                F.left = x[i] + ClockWidth * Math.cos(-1.0471 + i * Split * Math.PI / 180);
            }
            for (i = 0; i < H.length; i++) {
                var HL = (ns) ? document.layers['nsHours' + i] : ieHours[i].style;
                HL.top = y[i] + HandY + (i * HandHeight) * Math.sin(hrs) + scrll;
                HL.left = x[i] + HandX + (i * HandWidth) * Math.cos(hrs);
            }
            for (i = 0; i < M.length; i++) {
                var ML = (ns) ? document.layers['nsMinutes' + i] : ieMinutes[i].style;
                ML.top = y[i] + HandY + (i * HandHeight) * Math.sin(min) + scrll;
                ML.left = x[i] + HandX + (i * HandWidth) * Math.cos(min);
            }
            for (i = 0; i < S.length; i++) {
                var SL = (ns) ? document.layers['nsSeconds' + i] : ieSeconds[i].style;
                SL.top = y[i] + HandY + (i * HandHeight) * Math.sin(sec) + scrll;
                SL.left = x[i] + HandX + (i * HandWidth) * Math.cos(sec);
            }
            for (i = 0; i < D.length; i++) {
                var DL = (ns) ? document.layers['nsDate' + i] : ieDate[i].style;
                DL.top = Dy[i] + ClockHeight * 1.5 * Math.sin(currStep + i * Dsplit * Math.PI / 180) + scrll;
                DL.left = Dx[i] + ClockWidth * 1.5 * Math.cos(currStep + i * Dsplit * Math.PI / 180);
            }
            currStep -= step;
        }
        function Delay() {
            scrll = (ns) ? window.pageYOffset : 0;
            Dy[0] = Math.round(DY[0] += ((ymouse) - DY[0]) * speed);
            Dx[0] = Math.round(DX[0] += ((xmouse) - DX[0]) * speed);
            for (i = 1; i < D.length; i++) {
                Dy[i] = Math.round(DY[i] += (Dy[i - 1] - DY[i]) * speed);
                Dx[i] = Math.round(DX[i] += (Dx[i - 1] - DX[i]) * speed);
            }
            y[0] = Math.round(Y[0] += ((ymouse) - Y[0]) * speed);
            x[0] = Math.round(X[0] += ((xmouse) - X[0]) * speed);
            for (i = 1; i < n; i++) {
                y[i] = Math.round(Y[i] += (y[i - 1] - Y[i]) * speed);
                x[i] = Math.round(X[i] += (x[i - 1] - X[i]) * speed);
            }
            ClockAndAssign();
            setTimeout('Delay()', 30);
        }
        if (ns || ie) window.onload = Delay;                     
//-->                     
    </script>
    <form id="form1" runat="server">
    <div style="width: 100%; height: 100%;">
        <table class="table" border="0" align="center" cellpadding="0" cellspacing="0" style="height: 746px;
            width: 1024px; background-image: url(Images/bg.jpg); background-repeat: no-repeat;">
            <tr style="height: 180px; width: 100%">
                <td height="180px" valign="bottom">
                </td>
            </tr>
            <tr style="height: 100px; width: 100%">
                <td style="height: 100px; width: 100%" valign="bottom">
                    <table class="table" width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr style="height: 80px; width: 100%">
                            <td style="height: 80px; width: 520px; vertical-align: bottom;">
                                <img alt="SIS" src="Images/logo.gif" width="334" height="60" style="margin-left: 120px;" />
                            </td>
                            <td style="height: 80px; width: 500px; vertical-align: bottom; text-align: left;">
                                <table width="277px" border="0" cellpadding="0" cellspacing="0">
                                    <tr style="width: 100%; text-align: left;">
                                        <td width="42px">
                                            <span class="STYLE1">帐户</span>
                                        </td>
                                        <td width="235px">
                                            <%--<asp:TextBox ID="TB_UserName" runat="server" CssClass="STYLE1" Text="sisdemo" 
                                            Width="128px"></asp:TextBox>--%>
                                            <input name="usercode" type="text" runat="server" class="input-text" id="usercode"
                                                size="15" onmouseover="this.focus()" tabindex="1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="42px">
                                            <span class="STYLE1">密码</span>
                                        </td>
                                        <td width="235px">
                                            <%--<asp:TextBox ID="TB_Password" runat="server" CssClass="STYLE1" TextMode="password"
                                                Width="128px"></asp:TextBox>--%>
                                            <input name="userpassword" type="password" runat="server" class="input-text" id="userpassword"
                                                size="15" onmouseover="this.focus()" tabindex="2" />
                                            <select id="CookieDate" name="CookieDate" style="width: 80px; height: 20px;" runat="server">
                                                <option value="0">不保存</option>
                                                <option value="1">保存一天</option>
                                                <option value="2">保存一月</option>
                                                <option value="3">保存一年</option>
                                            </select>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="height: 40px; width: 500px; vertical-align: bottom; text-align: left;">
                                <table width="277px" border="0" cellspacing="0" cellpadding="0">
                                    <tr style="height: 40px; width: 100%; text-align: left;">
                                        <td style="height: 40px; width: 100%; vertical-align: bottom; text-align: center;">
                                            <span style="float: left">
                                                <asp:Button ID="btnLogin" runat="server" Text="登 录" Width="75px" Height="20px" class="btn"
                                                    onmouseover="this.className='btn_mouseover'" onmouseout="this.className='btn_mouseout'"
                                                    onmousedown="this.className='btn_mousedown'" onmouseup="this.className='btn_mouseup'"
                                                    OnClick="btnLogin_Click" />
                                            </span><span style="float: right">
                                                <asp:Button ID="btnDemo" runat="server" Text="游 客" Width="75px" Height="20px" class="btn"
                                                    onmouseover="this.className='btn_mouseover'" onmouseout="this.className='btn_mouseout'"
                                                    onmousedown="this.className='btn_mousedown'" onmouseup="this.className='btn_mouseup'"
                                                    OnClick="btnDemo_Click" />
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height: 300px">
                <td height="300px">
                </td>
            </tr>
            <tr style="height: 150px">
                <td height="150px">
                    <div id="CopyRightBody">
                        <table align="center" border="0" cellpadding="0" cellspacing="0" width="90%">
                            <tbody>
                                <tr>
                                    <td style="border-bottom: 1px dashed #666; color: #666" align="center" height="30">
                                        <a target="_blank" id="syshelp" href="SISHelp.htm">系统帮助</a>&nbsp;&nbsp;|&nbsp;&nbsp;版权说明&nbsp;&nbsp;|&nbsp;&nbsp;网站地图&nbsp;&nbsp;|&nbsp;&nbsp;联系我们
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-top: 5px; color: #666" align="center" height="25">
                                        版权所有：<a target="_blank" id="A1" href="http://www.bdxyit.com">北斗信息</a>&nbsp;&nbsp; 技术管理：<a target="_blank" id="A2" href="http://www.bdxyit.com">北京北斗兴业信息技术有限公司</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="color: #666" align="center" height="20">
                                        技术支持：010-65569686/8081&nbsp;&nbsp;传真：010-68415289&nbsp;&nbsp;值班手机：13910053356
                                    </td>
                                </tr>
                                <tr>
                                    <td style="color: #666" align="center" height="20">
                                        北京地址：北京市海淀区西三环北路11号海通时代商务中心A3座 &nbsp;&nbsp;邮编：100089
                                    </td>
                                </tr>
                                <tr>
                                    <td style="color: #666" align="center" height="20">
                                        西安地址：西安市碑林区交大路帝源豪庭大厦B座27E &nbsp;&nbsp;邮编：710000
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" height="50">
                                        &nbsp;
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
