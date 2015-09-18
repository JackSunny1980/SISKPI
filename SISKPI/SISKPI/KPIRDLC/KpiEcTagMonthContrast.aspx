<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KpiEcTagMonthContrast.aspx.cs"
    Inherits="RdlcReportBasic.KpiTagMonthReport.KpiEcTagMonthContrast" %>

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
                    基准月份：
                </td>
                <td>
                    <asp:DropDownList ID="ddlBaseYear" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    年
                </td>
                <td>
                    <asp:DropDownList ID="ddlBaseMonth" runat="server">
                        <asp:ListItem Value='01'>01</asp:ListItem>
                        <asp:ListItem Value='02'>02</asp:ListItem>
                        <asp:ListItem Value='03'>03</asp:ListItem>
                        <asp:ListItem Value='04'>04</asp:ListItem>
                        <asp:ListItem Value='05'>05</asp:ListItem>
                        <asp:ListItem Value='06'>06</asp:ListItem>
                        <asp:ListItem Value='07'>07</asp:ListItem>
                        <asp:ListItem Value='08'>08</asp:ListItem>
                        <asp:ListItem Value='09'>09</asp:ListItem>
                        <asp:ListItem Value='10'>10</asp:ListItem>
                        <asp:ListItem Value='11'>11</asp:ListItem>
                        <asp:ListItem Value='12'>12</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    月
                </td>
                <td>
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
                    考核单元：
                </td>
                <td style="font-size: 13px" class="style10">
                    <asp:DropDownList ID="ddlUnitEntity" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblCondition" runat="server" Text="," Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3" style="font-size: 13px;">
                    对比月份：
                </td>
                <td>
                    <asp:DropDownList ID="ddlContYear" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    年
                </td>
                <td>
                    <asp:DropDownList ID="ddlContMonth" runat="server">
                        <asp:ListItem Value='01'>01</asp:ListItem>
                        <asp:ListItem Value='02'>02</asp:ListItem>
                        <asp:ListItem Value='03'>03</asp:ListItem>
                        <asp:ListItem Value='04'>04</asp:ListItem>
                        <asp:ListItem Value='05'>05</asp:ListItem>
                        <asp:ListItem Value='06'>06</asp:ListItem>
                        <asp:ListItem Value='07'>07</asp:ListItem>
                        <asp:ListItem Value='08'>08</asp:ListItem>
                        <asp:ListItem Value='09'>09</asp:ListItem>
                        <asp:ListItem Value='10'>10</asp:ListItem>
                        <asp:ListItem Value='11'>11</asp:ListItem>
                        <asp:ListItem Value='12'>12</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    月
                </td>
                <td>
                </td>
                <td class="style6">
                </td>
                <td style="font-size: 13px" class="style8">
                    <asp:DropDownList ID="ddlShift" runat="server" Width="60px" Visible="false">
                        <asp:ListItem Value="14">全部</asp:ListItem>
                        <asp:ListItem Value="12">后夜</asp:ListItem>
                        <asp:ListItem Value="23">白班</asp:ListItem>
                        <asp:ListItem Value="34">前夜</asp:ListItem>
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
            <LocalReport ReportPath="KpiTagMonthReport\KpiEcTagMonthContrast.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="odsMonth" Name="DataSet1" />
                    <rsweb:ReportDataSource DataSourceId="odsUnitID" Name="DataSet2" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="odsMonth" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="RdlcReportBasic.DsEcHourTableAdapters.KPI_Ec_MonthContrastResultTableAdapter">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlBaseYear" Name="BaseYear" PropertyName="SelectedValue"
                    Type="Decimal" />
                <asp:ControlParameter ControlID="ddlBaseMonth" Name="BaseMonth" PropertyName="SelectedValue"
                    Type="Decimal" />
                <asp:ControlParameter ControlID="ddlContYear" Name="ContYear" PropertyName="SelectedValue"
                    Type="Decimal" />
                <asp:ControlParameter ControlID="ddlContMonth" Name="ContMonth" PropertyName="SelectedValue"
                    Type="Decimal" />
                <asp:ControlParameter ControlID="ddlTeam" Name="Team" PropertyName="SelectedValue"
                    Type="String" />
                <asp:ControlParameter ControlID="ddlUnitEntity" Name="Entity" PropertyName="SelectedValue"
                    Type="String" />
                <asp:ControlParameter ControlID="ddlKPIName" Name="Condition" PropertyName="SelectedValue"
                    Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsUnitName" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="RdlcReportBasic.DsEcHourTableAdapters.KPI_UNIT_PTTableAdapter">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsUnitID" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="RdlcReportBasic.DsEcHourTableAdapters.GetUnitTitleTableAdapter">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlUnitEntity" Name="Entity" PropertyName="SelectedValue"
                    Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsKPITag" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="RdlcReportBasic.DsEcHourTableAdapters.KPI_Ec_TagCheckedQueryTableAdapter">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlUnitEntity" Name="Entity" PropertyName="SelectedValue"
                    Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsYear" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="RdlcReportBasic.DSTableAdapters.GetYearListTableAdapter">
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
