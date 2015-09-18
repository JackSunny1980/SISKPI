<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_SubSATagConfig.aspx.cs"
    Inherits="SISKPI.KPI_SubSATagConfig" %>

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
                    <asp:Label ID="lblTitle" runat="server" Class="title" Text="安全指标配置"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%">
                <td width="100%" align="center">
                    <div style="width: 95%;">
                        <table class="table" width="100%">
                            <tr style="width: 100%;">
                                <td style="width: 100%; padding:0 25px 0 0" align="right">
                                    <asp:Button ID="btnReturn"  Width="80px" runat="server" Text="返回"
                                        ToolTip="返回" OnClick="btnReturn_Click" />
                                </td>
                            </tr>
                              <tr >
                                <td style="width: 100%;padding:8px 25px 0 0"" align="right">
                                    <asp:Button ID="btnApply" runat="server" Width="80px" Text="应 用" OnClick="btnApply_Click" />
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
                                                <td width="135px" style="padding:5px 0" align="left">
                                                    所属机组：
                                                </td>
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    <asp:DropDownList ID="ddl_UnitID" Width="200px" runat="server" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddl_UnitID_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <%--<select style="width: 200px" runat="server" id="ddl_UnitID" name="ddlUnit">
                                                    </select>--%>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    所属设备：
                                                </td>
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    <select style="width: 200px" runat="server" id="ddl_SeqID" name="ddlSeq">
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    所属指标：
                                                </td>
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    <asp:DropDownList ID="ddl_KpiID" Width="200px" runat="server" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddl_KpiID_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <%--<select style="width: 200px" runat="server" id="ddl_KpiID" name="ddlKpi">
                                                    </select>--%>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    指标单位：
                                                </td>
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    <select style="width: 200px" runat="server" id="ddl_EngunitID" name="ddlEngunit">
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    考核周期：
                                                </td>
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    <select style="width: 200px" runat="server" id="ddl_CycleID" name="ddlCycle">
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    是否有效：
                                                </td>
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    <select runat="server" id="ddl_SAIsValid" style="width: 200px">
                                                        <option value="0">否</option>
                                                        <option value="1" selected>是</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    是否计算：
                                                </td>
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    <select runat="server" id="ddl_SAIsCalc" style="width: 200px">
                                                        <option value="0">否</option>
                                                        <option value="1" selected>是</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    是否显示：
                                                </td>
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    <select runat="server" id="ddl_SAIsDisplay" style="width: 200px">
                                                        <option value="0">否</option>
                                                        <option value="1" selected>是</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    是否计总：
                                                </td>
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    <select runat="server" id="ddl_SAIsTotal" style="width: 200px">
                                                        <option value="0">否</option>
                                                        <option value="1" selected>是</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    页面集：
                                                </td>
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    <select style="width: 200px" runat="server" id="ddl_SAWeb">
                                                    </select>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                                <td width="48%" align="left" valign="top">
                                    <fieldset class="field_info" style="width: 95%;">
                                        <legend>指标信息</legend>
                                        <table class="table" width="100%">
                                            <tr style="width: 100%">
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    指标代码：
                                                </td>
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    <input id="txt_SACode" runat="server" style="width: 200px; background-color: Yellow"
                                                        type="text" />
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    指标名称：
                                                </td>
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    <input id="txt_SAName" runat="server" style="width: 200px; background-color: Yellow"
                                                        type="text" />
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    指标描述：
                                                </td>
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    <input id="txt_SADesc" runat="server" style="width: 200px;" type="text" />
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    显示顺序：
                                                </td>
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    <input id="txt_SAIndex" runat="server" style="width: 200px" type="text" />
                                                </td>
                                            </tr>
                                       
                                       <%--     <tr style="width: 100%; display: none;">
                                                <td width="135px" align="left">
                                                    <asp:Label ID="Label3" runat="server" Text="过滤条件:"></asp:Label>
                                                </td>
                                                <td width="135px" align="left">
                                                    <asp:TextBox ID="tbx_SAFilterExp" Wrap="true" Width="300px" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>--%>
                                               <tr style="width: 100%">

                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    <asp:Label ID="Label1" runat="server" Text="超限次数表达式:"></asp:Label>
                                                </td>
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    <asp:TextBox ID="txtCountExpression" Wrap="true" Width="300px" runat="server" Height="30px"></asp:TextBox>
                                                </td>
                                            </tr>
                                               <tr style="width: 100%">

                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    <asp:Label ID="Label2" runat="server" Text="超限时长表达式:"></asp:Label>
                                                </td>
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    <asp:TextBox ID="txtDurationExpression" Wrap="true" Width="300px" runat="server" Height="30px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">

                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    <asp:Label ID="Label4" runat="server" Text="计算表达式:"></asp:Label>
                                                </td>
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    <asp:TextBox ID="tbx_SACalcExp" Wrap="true" Width="300px" runat="server" Height="30px"></asp:TextBox>
                                                </td>
                                            </tr>
                                                 <tr style="width: 100%">
                                                <td width="135px" style="padding:5px 0"  align="left">
                                                    备注：
                                                </td>
                                                <td width="135px" style="padding:5px 0"   align="left">
                                                    <input id="txt_SANote" runat="server" style="width: 200px;" type="text" />
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
         <%--   <tr style="width: 100%">
                <td width="100%" align="center" valign="top">
                    <div style="width: 95%;">
                        <table class="table" width="100%">
                            <tr style="width: 100%">
                                <td width="100%" align="left" valign="top">
                                    <fieldset class="field_info" style="width: 90%;">
                                        <legend>安全区间设置</legend>
                                        <table class="table" width="100%">
                                            <tr style="width: 100%">
                                                <td width="100%" align="left" valign="top">
                                                    <asp:Label ID="Label6" runat="server" Text="区间表达式与得分表达式的常用系数为x0(自身值),t0（持续时间）,c1（本小时次数）,c2（本班次数）；区间表达式计算结果为布尔型，有且只有一个为真；得分表达式计算结果为数值型!"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="100%" align="left" valign="top">
                                                    <asp:GridView ID="gvSecurity" CssClass="GridViewStyle" Width="100%" runat="server"
                                                        AutoGenerateColumns="False" EmptyDataText="没有满足条件的数据" AllowPaging="False" OnRowDataBound="gvSecurity_RowDataBound"
                                                        OnRowCommand="gvSecurity_RowCommand" OnRowCancelingEdit="gvSecurity_RowCancelingEdit"
                                                        OnRowEditing="gvSecurity_RowEditing" OnRowUpdating="gvSecurity_RowUpdating">
                                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                                        <RowStyle CssClass="GridViewRowStyle" />
                                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                                        <PagerStyle CssClass="GridViewPagerStyle" />
                                                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="序号">
                                                                <ItemTemplate>
                                                                    <input type="hidden" runat="server" id="securityid" value='<%# Eval("SecurityID").ToString()%>' />
                                                                    <%#  Container.DataItemIndex + 1%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="40px" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="SAName" HeaderText="指标" ItemStyle-HorizontalAlign="Center"
                                                                ReadOnly="true">
                                                                <ControlStyle Width="300px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SecurityExp" HeaderText="区间表达式" ItemStyle-HorizontalAlign="Center"
                                                                ReadOnly="true">
                                                                <ControlStyle Width="300px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SecurityGainExp" HeaderText="得分表达式" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle Width="300px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="是否最优" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle Width="40px"></ControlStyle>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlOptimal" runat="server" DataSourceID="GetYesNo" DataTextField="Name"
                                                                        DataValueField="Value">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="是否报警" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle Width="40px"></ControlStyle>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlAlarm" runat="server" DataSourceID="GetYesNo" DataTextField="Name"
                                                                        DataValueField="Value">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="是否有效" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle Width="40px"></ControlStyle>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlValid" runat="server" DataSourceID="GetYesNo" DataTextField="Name"
                                                                        DataValueField="Value">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="WorkNote" HeaderText="备注" ControlStyle-Width="100px">
                                                                <ControlStyle Width="60px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:CommandField HeaderText="配置" ItemStyle-HorizontalAlign="Center" ShowEditButton="True"
                                                                EditText="编辑" />
                                                            <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="data" runat="server" Text="测试" CommandName="dataTest" CommandArgument='<%# Eval("SecurityID") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="60px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="delete" runat="server" Text="删除" CommandName="dataDelete" CommandArgument='<%# Eval("SecurityID") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="60px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td width="100%" align="center" valign="top">
                                                    <asp:Button ID="btnAddSecurity" runat="server" Text="新增区间" Width="120px" OnClick="btnAddSecurity_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>--%>
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
