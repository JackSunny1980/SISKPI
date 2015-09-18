<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_SubECTagConfig2.aspx.cs"
    Inherits="SISKPI.KPI_SubECTagConfig2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISKPI</title>
    <link href="../CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" href="../js/tab/jquery.tabs.css" type="text/css" media="print, projection, screen" />
    <!--[if lte IE 7]>
        <link rel="stylesheet" href="../js/tab/jquery.tabs-ie.css" type="text/css" media="projection, screen">
        <![endif]-->
    <script src="../js/ProcessBar.js" type="text/javascript"></script>
    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../js/Common.js" type="text/javascript"></script>
    <script src="../js/tab/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="../js/Tab/jquery.tabs.js" type="text/javascript"></script>
    <script src="../js/Config.js" type="text/javascript"></script>
    <script src="../js/GridViewColor.js" type="text/javascript"></script>
    <style type="text/css">
        .lblstyle2
        {
            border: #D87E26 0px solid;
            cursor: hand;
            color: black;
            background: none;
            font-size: 10pt;
            font-weight: bolder;
            width: 100%;
        }
        
        .calcstyle
        {
            border: White 1px solid;
            cursor: hand;
            color: Blue;
            background: White;
            font-size: 10pt;
            font-weight: bolder;
            width: 30px;
        }
    </style>
    <script type="text/javascript">

        function Clear() {
            document.getElementById("tbx_ECCalcExp").value = "";
        }

        function InputN(obj) {
            var old = document.getElementById("tbx_ECCalcExp").value;
            var add = obj.value;
            document.getElementById("tbx_ECCalcExp").value = old + add;

        }

        function InputC(obj) {
            var old = document.getElementById("tbx_ECCalcExp").value;
            var add = obj.value;
            document.getElementById("tbx_ECCalcExp").value = old + add;
        }

        function InputF(obj) {
            var old = document.getElementById("tbx_ECCalcExp").value;
            var add = obj.value;
            document.getElementById("tbx_ECCalcExp").value = old + add;

        }

        function InputS(obj) {
            var old = document.getElementById("tbx_ECCalcExp").value;
            var add = obj.value;
            document.getElementById("tbx_ECCalcExp").value = old + add;

        }

        function InputIFC(val) {
            var old = document.getElementById("tbx_ECCalcExp").value;
            var add = val;
            document.getElementById("tbx_ECCalcExp").value = old + val;

        }

        function apply() {
            window.opener = null;
            window.close();
        }

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
<body>
    <form id="form1" runat="server" style="width: 100%">
    <div style="margin: 0px; width: 100%; text-align: center;">
        <table width="100%" class="table" id="table1" align="center">
            <tr style="width: 100%">
                <td width="100%" align="center">
                    <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                    <asp:Label ID="lblTitle" runat="server" Class="title" Text="经济指标配置"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%">
                <td width="100%" align="center">
                    <table class="table" width="100%">
                        <tr style="width: 100%;">
                            <td align="center" style="width: 20%;">
                                <asp:Button ID="btnStep1" CssClass="lblstyle2" runat="server" Text="第一步，指标设置" ToolTip="实时指标设置"
                                    OnClick="btnStep1_Click" />
                            </td>
                            <td align="center" style="width: 20%; background-color: Orange">
                                <asp:Button ID="btnStep2" CssClass="lblstyle2" runat="server" Text="第二步，算法设置" ToolTip="算法设置"
                                    OnClick="btnStep2_Click" />
                            </td>
                            <td align="center" style="width: 20%;">
                                <asp:Button ID="btnStep3" CssClass="lblstyle2" runat="server" Text="第三步，实时考核设置" ToolTip="实时考核设置"
                                    OnClick="btnStep3_Click" />
                            </td>
                            <td align="center" style="width: 20%;">
                                <asp:Button ID="btnStep4" CssClass="lblstyle2" runat="server" Text="第四步，排名考核设置" ToolTip="排名考核设置"
                                    OnClick="btnStep4_Click" />
                            </td>
                            <td align="center" style="width: 20%; background-color: Yellow">
                                <asp:Button ID="btnReturn" CssClass="lblstyle2" runat="server" Text="返回" ToolTip="返回"
                                    OnClick="btnReturn_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="width: 100%">
                <td width="100%" align="center" valign="top">
                    <div style="width: 98%;">
                        <table class="table" width="100%">
                            <tr style="width: 100%">
                                <td width="320px" align="center" valign="top">
                                    <table class="table" width="100%">
                                        <tr style="width: 100%">
                                            <td width="100%" align="center" valign="top">
                                                <fieldset class="field_info" style="width: 95%;">
                                                    <legend>数字类</legend>
                                                    <table border="5" class="table" bgcolor="#6591CE" align="center" cellspacing="0"
                                                        cellpadding="0" width="100%">
                                                        <tr align="center">
                                                            <td>
                                                                <input type="button" id="NN01" style="width: 50px" value="1" onclick="InputN(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NN02" style="width: 50px" value="2" onclick="InputN(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NN03" style="width: 50px" value="3" onclick="InputN(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NN04" style="width: 50px" value="4" onclick="InputN(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NN05" style="width: 50px" value="5" onclick="InputN(this);" />
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td>
                                                                <input type="button" id="NN06" style="width: 50px" value="6" onclick="InputN(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NN07" style="width: 50px" value="7" onclick="InputN(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NN08" style="width: 50px" value="8" onclick="InputN(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NN09" style="width: 50px" value="9" onclick="InputN(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NN10" style="width: 50px" value="0" onclick="InputN(this);" />
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NC00" style="width: 50px" value="." onclick="InputN(this);" />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                                <fieldset class="field_info" style="width: 95%;">
                                                    <legend>符号类</legend>
                                                    <table border="5" class="table" bgcolor="#6591CE" align="center" cellspacing="0"
                                                        cellpadding="0" width="100%">
                                                        <tr align="center">
                                                            <td>
                                                                <input type="button" id="NC01" style="width: 50px" value="+" onclick="InputC(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NC02" style="width: 50px" value="-" onclick="InputC(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NC03" style="width: 50px" value="*" onclick="InputC(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NC04" style="width: 50px" value="/" onclick="InputC(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NC05" style="width: 50px" value="," onclick="InputC(this);" />
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td>
                                                                <input type="button" id="NC06" style="width: 50px" value="(" onclick="InputC(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NC07" style="width: 50px" value=")" onclick="InputC(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NC08" style="width: 50px" value="&&" onclick="InputC(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NC09" style="width: 50px" value="||" onclick="InputC(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NC10" style="width: 50px" value="$" disabled onclick="InputC(this);" />
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td>
                                                                <input type="button" id="NC11" style="width: 50px" value=">" onclick="InputC(this);" />
                                                            </td>
                                                            <td>
                                                                <%--value="≥" --%>
                                                                <input type="button" id="NC12" style="width: 50px" value="&gt;=" onclick="InputC(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NC13" style="width: 50px" value="==" onclick="InputC(this);" />
                                                            </td>
                                                            <td>
                                                                <%--value="≤"--%>
                                                                <input type="button" id="NC14" style="width: 50px" value="&lt;=" onclick="InputC(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NC15" style="width: 50px" value="<" onclick="InputC(this);" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                                <fieldset class="field_info" style="width: 95%;">
                                                    <legend>数学计算类</legend>
                                                    <table border="5" class="table" bgcolor="#6591CE" align="center" cellspacing="0"
                                                        cellpadding="0" width="100%">
                                                        <tr align="center">
                                                            <td>
                                                                <input type="button" id="NF01" style="width: 60px" value="ABS" onclick="InputF(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NF02" style="width: 60px" value="ACOS" onclick="InputF(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NF03" style="width: 60px" value="ASIN" onclick="InputF(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NF04" style="width: 60px" value="ATAN" onclick="InputF(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NF05" style="width: 60px;" value="AVERAGE" onclick="InputF(this);" />
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td>
                                                                <input type="button" id="NF06" style="width: 60px" value="COS" onclick="InputF(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NF07" style="width: 60px" value="EXP" onclick="InputF(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NF08" style="width: 60px" value="FACT" onclick="InputF(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NF09" style="width: 60px" value="FLOOR" onclick="InputF(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NF10" style="width: 60px" value="INT" onclick="InputF(this);" />
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td>
                                                                <input type="button" id="NF11" style="width: 60px" value="LN" onclick="InputF(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NF12" style="width: 60px" value="LOG" onclick="InputF(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NF13" style="width: 60px" value="LOG10" onclick="InputF(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NF14" style="width: 60px" value="MAX" onclick="InputF(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NF15" style="width: 60px" value="MIN" onclick="InputF(this);" />
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td>
                                                                <input type="button" id="NF16" style="width: 60px" value="MOD" onclick="InputF(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NF17" style="width: 60px" value="PI" onclick="InputF(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NF18" style="width: 60px" value="POWER" onclick="InputF(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NF19" style="width: 60px" value="PRODUCT" onclick="InputF(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NF20" style="width: 60px" value="ROUND" onclick="InputF(this);" />
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td>
                                                                <input type="button" id="NF21" style="width: 60px" value="SIN" onclick="InputF(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NF22" style="width: 60px" value="SQRT" onclick="InputF(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NF23" style="width: 60px" value="SQUARE" onclick="InputF(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NF24" style="width: 60px" value="TAN" onclick="InputF(this);" />
                                                            </td>
                                                            <td>
                                                                <input type="button" id="NF25" style="width: 60px" value="SUM" onclick="InputF(this);" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                                <fieldset class="field_info" style="width: 95%;">
                                                    <legend>字符串类</legend>
                                                    <table border="5" class="table" bgcolor="#6591CE" align="center" cellspacing="0"
                                                        cellpadding="0" width="100%">
                                                        <tr style="width: 100%">
                                                            <td width="100%" align="left">
                                                                <table>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <input type="button" id="NS01" style="width: 50px" value="NOW" onclick="InputS(this);" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" id="NS02" style="width: 50px" value="CHAR" onclick="InputS(this);" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" id="NS03" style="width: 50px" value="LOWER" onclick="InputS(this);" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" id="NS04" style="width: 50px" value="LEFT" onclick="InputS(this);" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr style="width: 100%">
                                                            <td width="100%" align="left">
                                                                <table>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <input type="button" id="NS05" style="width: 50px" value="LEN" onclick="InputS(this);" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" id="NS06" style="width: 50px" value="SUBSTRING" onclick="InputS(this);" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" id="NS07" style="width: 50px" value="TRIM" onclick="InputS(this);" />
                                                                        </td>
                                                                        <td>
                                                                            <input type="button" id="NS08" style="width: 50px" value="UPPER" onclick="InputS(this);" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                                <fieldset class="field_info" style="width: 95%;">
                                                    <legend>专业类</legend>
                                                    <table border="5" class="table" bgcolor="#6591CE" align="center" cellspacing="0"
                                                        cellpadding="0" width="100%">
                                                        <tr style="width: 100%">
                                                            <td width="100%" align="left">
                                                                <table>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <input type="button" id="btnPSK" style="width: 210px" value="饱和温度求压力" onclick="InputIFC('PSK');" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <input type="button" id="btnTSK" style="width: 210px" value="饱和压力求温度" onclick="InputIFC('TSK');" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <input type="button" id="btnHPT" style="width: 210px" value="过冷水、过热蒸汽压力、温度求焓" onclick="InputIFC('H_PT');" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <%--<tr style="width: 100%">
                                                            <td width="100%" align="left">
                                                                <table>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <input type="button" id="Button13" style="width: 210px" value=".." onclick="InputX();" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <input type="button" id="Button14" style="width: 210px" value=".." onclick="InputX();" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <input type="button" id="Button15" style="width: 210px" value=".." onclick="InputX();" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        --%></table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="600px" align="center" valign="top">
                                    <table class="table" width="100%">
                                        <tr style="width: 100%">
                                            <td width="100%" align="center">
                                                <fieldset class="field_info" style="width: 95%;">
                                                    <legend>计算配置</legend>
                                                    <table class="table" width="100%">
                                                        <tr style="width: 100%">
                                                            <td width="100%" align="left">
                                                                <asp:Label ID="lbl_ECCode" runat="server" Text="指标代码:"></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Button ID="btnExample" runat="server" Width="80px" Text="函数示例" OnClick="btnExample_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr style="width: 100%">
                                                            <td width="100%" align="left">
                                                                <asp:Label ID="lbl_ECName" runat="server" Text="指标名称:"></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Button ID="btnTest" runat="server" Width="80px" Text="测 试" OnClick="btnTest_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr style="width: 100%; display: none;">
                                                            <td width="100%" align="left">
                                                                <asp:Label ID="Label3" runat="server" Text="过滤条件:"></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                            </td>
                                                        </tr>
                                                        <tr style="width: 100%; display: none">
                                                            <td width="100%" align="left">
                                                                <asp:TextBox ID="tbx_ECFilterExp" Wrap="true" Width="300px" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="center">
                                                            </td>
                                                        </tr>
                                                        <tr style="width: 100%">
                                                            <td width="100%" align="left">
                                                                <asp:Label ID="Label4" runat="server" Text="计算表达式:"></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                            </td>
                                                        </tr>
                                                        <tr style="width: 100%">
                                                            <td width="100%" align="left">
                                                                <asp:TextBox ID="tbx_ECCalcExp" Wrap="true" TextMode="MultiLine" Width="400px" runat="server" Height="50px"></asp:TextBox>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Button ID="btnApply" runat="server" Width="80px" Text="应 用" OnClick="btnApply_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr style="width: 100%">
                                                            <td width="100%" align="left">
                                                                <asp:Label ID="Label5" runat="server" Text="中文描述:"></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                            </td>
                                                        </tr>
                                                        <tr style="width: 100%">
                                                            <td width="100%" align="left">
                                                                <asp:TextBox ID="tbx_ECCalcDesc" Wrap="true"  TextMode="MultiLine" Width="400px" runat="server" Height="50px"></asp:TextBox>
                                                            </td>
                                                            <td align="center">
                                                                <input type="button" id="btnClear" style="width: 80px" value="清 空" onclick="Clear();" />
                                                            </td>
                                                        </tr>
                                                        <tr style="width: 100%">
                                                            <td width="100%" align="center">
                                                                <asp:Label ID="Label6" runat="server" Text="---" ForeColor="White"></asp:Label>
                                                                <asp:Label ID="Label1" runat="server" Text="---" ForeColor="White"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <%--
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    计算类型：
                                                </td>
                                                <td width="135px" align="left">
                                                    <select style="width: 200px" runat="server" id="ddl_ECCalcType" name="ddltype">
                                                        <option value="0">标签点计算</option>
                                                        <option value="1">指标计算</option>
                                                        <option value="2">表达式计算</option>
                                                    </select>
                                                </td>
                                                </tr>
                                                        --%>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="600px" align="center" valign="top">
                                                <table class="table" width="100%">
                                                    <tr style="width: 100%">
                                                        <td width="100%" align="center">
                                                            <fieldset class="field_info" style="width: 95%;">
                                                                <legend>自定义函数</legend>
                                                                <table class="table" width="100%">
                                                                    <tr style="width: 100%">
                                                                        <td width="100%" align="left">
                                                                            <asp:DropDownList ID="ddlCustomFunction" runat="server" Width="400px" 
                                                                                AutoPostBack="true" 
                                                                                onselectedindexchanged="ddlCustomFunction_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </fieldset>
                                                        </td>
                                                    </tr>
                                                    <tr style="width: 100%">
                                                        <td width="100%" align="center">
                                                            <div style="width: 98%">
                                                                <table class="table" width="100%">
                                                                    <tr style="width: 100%">
                                                                        <td width="48%" align="left" valign="top">
                                                                            <fieldset class="field_info" style="width: 95%;">
                                                                                <legend>实时标签</legend>
                                                                                <table class="table" width="100%">
                                                                                    <tr style="width: 100%">
                                                                                        <td width="100%" align="left">
                                                                                            标签名称：<input id="txt_TAG" runat="server" style="width: 60px" type="text" />
                                                                                            <asp:Button ID="btnSearchTAG" runat="server" Width="80px" Text="搜索" OnClick="btnSearchTAG_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="width: 100%">
                                                                                        <td width="100%" align="left">
                                                                                            <asp:ListBox ID="lbx_TAG" Width="100%" Height="220px" runat="server" AutoPostBack="true"
                                                                                                OnSelectedIndexChanged="lbx_TAG_SelectedIndexChanged"></asp:ListBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                        </td>
                                                                        <td width="48%" align="left" valign="top">
                                                                            <fieldset class="field_info" style="width: 95%;">
                                                                                <legend>计算指标</legend>
                                                                                <table class="table" width="100%">
                                                                                    <tr style="width: 100%">
                                                                                        <td width="100%" align="left">
                                                                                            指标名称：<input id="txt_KPI" runat="server" style="width: 60px" type="text" />
                                                                                            <asp:Button ID="btnSearchKPI" runat="server" Width="80px" Text="搜索" OnClick="btnSearchKPI_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="width: 100%">
                                                                                        <td width="100%" align="left">
                                                                                            <asp:ListBox ID="lbx_KPI" Width="100%" Height="220px" runat="server" AutoPostBack="true"
                                                                                                OnSelectedIndexChanged="lbx_KPI_SelectedIndexChanged"></asp:ListBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr style="width: 100%">
                <td width="100%" align="center" valign="top">
                </td>
            </tr>
        </table>
    </div>
    <div id="Lay1" style="z-index: 100; visibility: hidden; left: 1%; width: 260px; cursor: crosshair;
        position: absolute; top: -20px; height: 14%; background-color: transparent; text-align: center">
        <br />
        <b></b>
        <table align="center" style="width: 260px; height: 45px" class="BoderTable">
            <tr>
                <td>
                    <div>
                        <font color="#800080" size="2" style="font-weight: bold">配置正在执行，请稍候...</font></div>
                    <div style="border-right: black 1px solid; padding-right: 2px; border-top: black 1px solid;
                        padding-left: 2px; font-size: 8pt; padding-bottom: 2px; border-left: black 1px solid;
                        padding-top: 2px; border-bottom: black 1px solid">
                        <span id="progress1">&nbsp;</span> <span id="progress2">&nbsp;</span> <span id="progress3">
                            &nbsp;</span> <span id="progress4">&nbsp;</span> <span id="progress5">&nbsp;</span>
                        <span id="progress6">&nbsp;</span> <span id="progress7">&nbsp;</span> <span id="progress8">
                            &nbsp;</span> <span id="progress9">&nbsp;</span> <span id="progress10">&nbsp;</span>
                        <span id="progress11">&nbsp;</span> <span id="progress12">&nbsp;</span> <span id="progress13">
                            &nbsp;</span> <span id="progress14">&nbsp;</span> <span id="progress15">&nbsp;</span>
                        <span id="progress16">&nbsp;</span> <span id="progress17">&nbsp;</span> <span id="progress18">
                            &nbsp;</span> <span id="progress19">&nbsp;</span> <span id="progress20">&nbsp;</span>
                        <span id="progress21">&nbsp;</span> <span id="progress22">&nbsp;</span> <span id="progress23">
                            &nbsp;</span> <span id="progress24">&nbsp;</span> <span id="progress25">&nbsp;</span>
                        <span id="progress26">&nbsp;</span> <span id="progress27">&nbsp;</span> <span id="progress28">
                            &nbsp;</span> <span id="progress29">&nbsp;</span> <span id="progress30">&nbsp;</span>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
