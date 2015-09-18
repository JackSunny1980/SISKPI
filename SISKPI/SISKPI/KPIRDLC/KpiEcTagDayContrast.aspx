<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KpiEcTagDayContrast.aspx.cs"
    Inherits="RdlcReportBasic.KpiTagDayReport.KpiEcTagDayContrast" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style3
        {
            height: 26px;
            width: 71px;
        }
        .style5
        {
            width: 100px;
        }
        .style6
        {
            width: 43px;
        }
        .style8
        {
            width: 68px;
        }
        .style10
        {
            width: 150px;
        }
        .style11
        {
            width: 100px;
        }
    </style>
</head>
<body  onresize="bodyresize();">
    <script language="javascript">
        function bodyresize() {
            var o_r = document.getElementById("rvKPI");
            o_r.style.width = (document.body.clientWidth).toString() + "px";
        }
    </script>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table cellpadding="0" cellspacing="0" style="width: 700px; height: 26px; font-size: 13px;">
            <tr>
                <td style="font-size: 13px" class="style5">
                    基准日期：
                </td>
                <td style="font-size: 13px" width="140px">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div style="width: 114px">
                                <asp:TextBox ID="tbStartDate" runat="server" Width="114px"></asp:TextBox>
                            </div>
                            <div id="Calendardiv" style="display: none; z-index: 2; position: absolute">
                                <asp:Calendar ID="Calendar1" runat="server" BackColor="#FFFFCC" BorderColor="#FFCC66"
                                    BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                    ForeColor="#0000C0" Height="200px" OnSelectionChanged="Calendar1_SelectionChanged"
                                    OnVisibleMonthChanged="Calendar1_VisibleMonthChanged" ShowGridLines="True" Width="160px">
                                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                                    <SelectorStyle BackColor="#FFCC66" />
                                    <OtherMonthDayStyle ForeColor="#CC9966" />
                                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                                    <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                                </asp:Calendar>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="style6">
                    班次：
                </td>
                <td style="font-size: 13px" class="style8">
                    <asp:DropDownList ID="ddlShift" runat="server" Width="60px">
                        <asp:ListItem Value="14">全部</asp:ListItem>
                        <asp:ListItem Value="12">后夜</asp:ListItem>
                        <asp:ListItem Value="23">白班</asp:ListItem>
                        <asp:ListItem Value="34">前夜</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="font-size: 13px" class="style11">
                    考核单元：
                </td>
                <td style="font-size: 13px" class="style10">
                    <asp:DropDownList ID="ddlUnitEntity" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td>
                    <%--<asp:Label ID="lblCondition" runat="server" Text="," Visible="false"></asp:Label>--%>
                </td>
            </tr>
            <tr>
                <td class="style3" style="font-size: 13px;">
                    对比日期：
                </td>
                <td style="font-size: 13px; width: 114px">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div style="width: 114px">
                                <asp:TextBox ID="tbEndDate" runat="server" Width="114px"></asp:TextBox>
                            </div>
                            <div id="Calendardiv2" style="display: none; z-index: 2; position: absolute">
                                <asp:Calendar ID="Calendar2" runat="server" BackColor="#FFFFCC" BorderColor="#FFCC66"
                                    BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                    ForeColor="#0000C0" Height="200px" OnSelectionChanged="Calendar2_SelectionChanged"
                                    OnVisibleMonthChanged="Calendar2_VisibleMonthChanged" ShowGridLines="True" Width="160px">
                                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                                    <SelectorStyle BackColor="#FFCC66" />
                                    <OtherMonthDayStyle ForeColor="#CC9966" />
                                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                                    <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                                </asp:Calendar>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="style6">
                    值别：
                </td>
                <td style="font-size: 13px" class="style8">
                    <asp:DropDownList ID="ddlTeam" runat="server" Width="60px">
                        <asp:ListItem Value="16">全部</asp:ListItem>
                        <asp:ListItem Value="12">1值</asp:ListItem>
                        <asp:ListItem Value="23">2值</asp:ListItem>
                        <asp:ListItem Value="34">3值</asp:ListItem>
                        <asp:ListItem Value="45">4值</asp:ListItem>
                        <asp:ListItem Value="56">5值</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="font-size: 13px" class="style11">
                    指标：
                </td>
                <td>
                    <asp:DropDownList ID="ddlKPIName" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td style="font-size: 13px" align="center">
                    <asp:Button ID="ReadBtton" runat="server" Text="查 询" Width="80px" OnClick="ReadBtton_Click" />
                </td>
            </tr>
        </table>
        <rsweb:ReportViewer ID="rvKPI" runat="server" Font-Names="Verdana" Font-Size="8pt"
            InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
            BackColor="#6591CE" LinkDisabledColor="ActiveCaptionText" ShowZoomControl="true"
            SizeToReportContent="true" ZoomMode="FullPage" Width="100%" Style="margin-right: 0px">
            <LocalReport ReportPath="KpiTagDayReport\KpiEcTagDayContrast.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="odsDay" Name="DataSet1" />
                    <rsweb:ReportDataSource DataSourceId="odsTitle" Name="DataSet2" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="odsDay" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="RdlcReportBasic.DsEcHourTableAdapters.KPI_Ec_DayContrastResultTableAdapter">
            <SelectParameters>
                <asp:ControlParameter ControlID="tbStartDate" Name="BaseTime" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="tbEndDate" Name="ContTime" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="ddlTeam" Name="Team" PropertyName="SelectedValue"
                    Type="String" DefaultValue="" />
                <asp:ControlParameter ControlID="ddlShift" Name="Shift" PropertyName="SelectedValue"
                    Type="String" DefaultValue="" />
                <asp:ControlParameter ControlID="ddlUnitEntity" Name="Entity" PropertyName="SelectedValue"
                    Type="String" />
                <asp:ControlParameter ControlID="ddlKPIName" Name="Condition" PropertyName="SelectedValue"
                    Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsUnitName" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="RdlcReportBasic.DsEcHourTableAdapters.KPI_UNIT_PTTableAdapter">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsTitle" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="RdlcReportBasic.DsEcHourTableAdapters.GetEcQueryTitleTableAdapter">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlUnitEntity" Name="Entity" PropertyName="SelectedValue"
                    Type="String" />
                <asp:ControlParameter ControlID="tbStartDate" Name="logDate" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="tbEndDate" Name="EndDate" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsKPITag" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="RdlcReportBasic.DsEcHourTableAdapters.KPI_TAG_DEFTableAdapter">
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
