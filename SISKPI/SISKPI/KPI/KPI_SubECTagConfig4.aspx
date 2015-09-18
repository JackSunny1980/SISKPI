<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_SubECTagConfig4.aspx.cs"
    Inherits="SISKPI.KPI_SubECTagConfig4" %>

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
    </style>
    <script type="text/javascript">
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
                    <div style="width: 95%;">
                        <table class="table" width="98%">
                            <tr style="width: 100%;">
                                <td align="center" style="width: 20%;">
                                    <asp:Button ID="btnStep1" CssClass="lblstyle2" runat="server" Text="第一步，指标设置" ToolTip="实时指标设置"
                                        OnClick="btnStep1_Click" />
                                </td>
                                <td align="center" style="width: 20%;">
                                    <asp:Button ID="btnStep2" CssClass="lblstyle2" runat="server" Text="第二步，算法设置" ToolTip="算法设置"
                                        OnClick="btnStep2_Click" />
                                </td>
                                <td align="center" style="width: 20%;">
                                    <asp:Button ID="btnStep3" CssClass="lblstyle2" runat="server" Text="第三步，实时考核设置" ToolTip="实时考核设置"
                                        OnClick="btnStep3_Click" />
                                </td>
                                <td align="center" style="width: 20%; background-color: Orange">
                                    <asp:Button ID="btnStep4" CssClass="lblstyle2" runat="server" Text="第四步，排名考核设置" ToolTip="排名考核设置"
                                        OnClick="btnStep4_Click" />
                                </td>
                                <td align="center" style="width: 20%; background-color: Yellow">
                                    <asp:Button ID="btnReturn" CssClass="lblstyle2" runat="server" Text="返回" ToolTip="返回"
                                        OnClick="btnReturn_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr style="width: 100%">
                <td width="100%" align="center" valign="top">
                    <div style="width: 95%;">
                        <table class="table" width="100%">
                            <tr style="width: 100%">
                                <td width="30%" align="left" valign="top">
                                    <asp:Label ID="lbl_ECCode" runat="server" Width="300px" Text="指标代码：10_load"></asp:Label>
                                </td>
                                <td width="70%" align="left" valign="top">
                                    <asp:Label ID="lbl_ECName" runat="server" Width="400px" Text="指标名称：#1机组负荷"></asp:Label>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td width="100%" align="left" colspan="2">
                                    <fieldset class="field_info" style="width: 500px;">
                                        <legend>排名考核</legend>
                                        <table class="table" width="100%">
                                            <tr style="width: 100%">
                                                <td width="200px" align="left">
                                                    <asp:CheckBox ID="cbx_ECIsSort" runat="server" Text="是否排名考核" 
                                                        AutoPostBack="True" oncheckedchanged="cbx_ECIsSort_CheckedChanged" />
                                                </td>
                                                <td width="200px"  align="left">
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="200px" align="left">
                                                    计算方法：
                                                </td>
                                                <td width="200px"  align="left">
                                                    <asp:DropDownList ID="ddl_ECType" runat="server" DataSourceID="GetSort" DataTextField="Name"
                                                        DataValueField="Value">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="200px" align="left">
                                                    排名方法：
                                                </td>
                                                <td width="200px"  align="left">
                                                    <select runat="server" id="ddl_ECSort" style="width: 200px">
                                                        <option value="0">从小到大</option>
                                                        <option value="1" selected>从大到小</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="200px" align="left">
                                                    排名得分：
                                                </td>
                                                <td width="200px" align="left">
                                                    <input id="txt_ECScore" runat="server" style="width: 200px" type="text" />
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="200px" align="left">
                                                    例外排名表达式(多个采用,分隔):
                                                </td>
                                                <td width="200px" align="left">
                                                    <input id="txt_ECExExp" runat="server" style="width: 200px;" type="text" />
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="200px" align="left">
                                                    例外排名得分(对应表达式)：
                                                </td>
                                                <td width="200px" align="left">
                                                    <input id="txt_ECExScore" runat="server" style="width: 200px;" type="text" />
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
            <tr style="width: 100%">
                <td width="100%" align="center" valign="top">
                    <asp:Button ID="btnApply" runat="server" Width="80px" Text="应 用" OnClick="btnApply_Click" />
                    <asp:ObjectDataSource ID="GetSort" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetData" TypeName="SISKPI.KPIDataSetTableAdapters.GetSortTableAdapter">
                    </asp:ObjectDataSource>
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
