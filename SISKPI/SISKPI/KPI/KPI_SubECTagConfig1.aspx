<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_SubECTagConfig1.aspx.cs"
    Inherits="SISKPI.KPI_SubECTagConfig1" %>

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
                                <td align="center" style="width: 20%; background-color: Orange">
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
                    </div>
                </td>
            </tr>
            <tr style="width: 100%">
                <td width="100%" align="center" valign="top">
                    <div style="width: 95%;">
                        <table class="table" width="100%">
                            <tr style="width: 100%">
                                <td width="48%" align="left" valign="top">
                                    <fieldset class="field_info" style="width: 95%;">
                                        <legend>指标属性</legend>
                                        <table class="table" width="100%">
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    所属机组：
                                                </td>
                                                <td width="135px" align="left">
                                                    <asp:DropDownList ID="ddl_UnitID" Width="200px" runat="server" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddl_UnitID_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <%--<select style="width: 200px" runat="server" id="ddl_UnitID" name="ddlUnit">
                                                    </select>--%>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    所属设备：
                                                </td>
                                                <td width="135px" align="left">
                                                    <select style="width: 200px" runat="server" id="ddl_SeqID" name="ddlSeq">
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    所属指标：
                                                </td>
                                                <td width="135px" align="left">
                                                    <asp:DropDownList ID="ddl_KpiID" Width="200px" runat="server" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddl_KpiID_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <%--<select style="width: 200px" runat="server" id="ddl_KpiID" name="ddlKpi">
                                                    </select>--%>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    指标单位：
                                                </td>
                                                <td width="135px" align="left">
                                                    <select style="width: 200px" runat="server" id="ddl_EngunitID" name="ddlEngunit">
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    考核周期：
                                                </td>
                                                <td width="135px" align="left">
                                                    <select style="width: 200px" runat="server" id="ddl_CycleID" name="ddlCycle">
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    是否有效：
                                                </td>
                                                <td width="135px" align="left">
                                                    <select runat="server" id="ddl_ECIsValid" style="width: 200px">
                                                        <option value="0">否</option>
                                                        <option value="1" selected>是</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    是否计算：
                                                </td>
                                                <td width="135px" align="left">
                                                    <select runat="server" id="ddl_ECIsCalc" style="width: 200px">
                                                        <option value="0">否</option>
                                                        <option value="1" selected>是</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    是否考核：
                                                </td>
                                                <td width="135px" align="left">
                                                    <select runat="server" id="ddl_ECIsAsses" style="width: 200px">
                                                        <option value="0">否</option>
                                                        <option value="1" selected>是</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    是否负值清零：
                                                </td>
                                                <td width="135px" align="left">
                                                    <select runat="server" id="ddl_ECIsZero" style="width: 200px">
                                                        <option value="0">否</option>
                                                        <option value="1" selected >是</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    是否显示：
                                                </td>
                                                <td width="135px" align="left">
                                                    <select runat="server" id="ddl_ECIsDisplay" style="width: 200px">
                                                        <option value="0">否</option>
                                                        <option value="1" selected>是</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    是否计总：
                                                </td>
                                                <td width="135px" align="left">
                                                    <select runat="server" id="ddl_ECIsTotal" style="width: 200px">
                                                        <option value="0">否</option>
                                                        <option value="1" selected>是</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    指标集：
                                                </td>
                                                <td width="135px" align="left">
                                                    <select style="width: 200px" runat="server" id="ddl_ECWeb">
                                                    </select>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                                <td width="48%" align="left"  valign="top">
                                    <fieldset class="field_info" style="width: 95%;">
                                        <legend>指标信息</legend>
                                        <table class="table" width="100%">
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    指标代码：
                                                </td>
                                                <td width="135px" align="left">
                                                    <input id="txt_ECCode" runat="server" style="width: 200px; background-color: Yellow" 
                                                        type="text" />
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    指标名称：
                                                </td>
                                                <td width="135px" align="left">
                                                    <input id="txt_ECName" runat="server" style="width: 200px; background-color: Yellow"
                                                        type="text" />
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    指标描述：
                                                </td>
                                                <td width="135px" align="left">
                                                    <input id="txt_ECDesc" runat="server" style="width: 200px;" type="text" />
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    显示顺序：
                                                </td>
                                                <td width="135px" align="left">
                                                    <input id="txt_ECIndex" runat="server" style="width: 200px;background-color: Yellow" type="text" />
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    设计值：
                                                </td>
                                                <td width="135px" align="left">
                                                    <input id="txt_ECDesign" runat="server" style="width: 200px;" type="text" />
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    最优区间：
                                                </td>
                                                <td width="135px" align="left">
                                                    <input id="txt_ECOptimal" runat="server" style="width: 200px;" type="text" />
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    最大值：
                                                </td>
                                                <td width="135px" align="left">
                                                    <input id="txt_ECMaxValue" runat="server" style="width: 200px;" type="text" />
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    最小值：
                                                </td>
                                                <td width="135px" align="left">
                                                    <input id="txt_ECMinValue" runat="server" style="width: 200px;" type="text" />
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    指标权重：
                                                </td>
                                                <td width="135px" align="left">
                                                    <input id="txt_ECWeight" runat="server" style="width: 200px; background-color: Yellow"
                                                        type="text" />
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    权重分母：
                                                </td>
                                                <td width="135px" align="left">
                                                    <input id="txt_ECDenom" runat="server" style="width: 200px; background-color: Yellow"
                                                        type="text" />
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    计算等级：
                                                </td>
                                                <td width="135px" align="left">
                                                    <input id="txt_ECCalcClass" runat="server" style="width: 200px; background-color: Yellow"
                                                        type="text" />
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" align="left">
                                                    备注：
                                                </td>
                                                <td width="135px" align="left">
                                                    <input id="txt_ECNote" runat="server" style="width: 200px;" type="text" />
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
