<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KpiEcTagTeamReport.aspx.cs"
    Inherits="RdlcReportBasic.KpiTagTeamReport.KpiEcTagTeamReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 28px;
            width: 108px;
        }
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
        .style12
        {
            width: 17px;
        }
        .style13
        {
            width: 45px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function OpenOvertimeDlog(frmWin, width, height) {
            var me;
            var action;
            action = frmWin;

            // 把父页面窗口对象当作参数传递到对话框中，以便对话框操纵父页自动刷新。 
            me = "KpiEcTagTeamMultiselect.aspx?action=" + action + "";

            // 显示对话框。
            //var sRet = window.showModalDialog('a.html','title','scrollbars=no;resizable=no;help=no;status=no;dialogTop=25; dialogLeft=0;dialogHeight=350px;dialogwidth=410px;');
            window.showModalDialog(me, null, 'dialogWidth=' + width + 'px;dialogHeight=' + height + 'px;help:no;status:no')

            //window.open ("page.html", "newwindow", "height=100, width=400, toolbar =no, menubar=no, scrollbars=no, resizable=no, location=no, status=no") //写成一行
            //window.open(me, "", "width=" + width + ",height=" + height + ",toolbar =no, menubar=no, scrollbars=no, resizable=no, location=no, status=no") 
        } 
    </script>
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
                    开始日期：
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
                <td style="font-size: 13px" class="style13">
                    <asp:DropDownList ID="ddlStartHour" runat="server" Width="40px">
                        <asp:ListItem>00</asp:ListItem>
                        <asp:ListItem>01</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style12">
                    时
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
                    <asp:Label ID="lblCondition" runat="server" Text="," Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3" style="font-size: 13px;">
                    结束日期：
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
                <td style="font-size: 13px" class="style13" align="left">
                    <asp:DropDownList ID="ddlEndHour" runat="server" Width="40px">
                        <asp:ListItem>00</asp:ListItem>
                        <asp:ListItem>01</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style12">
                    时
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
                    <asp:Button ID="btnKPIName" runat="server" Text="请选择指标..." Width="150px" />
                </td>
                <td style="font-size: 13px" align="center">
                    <asp:Button ID="ReadBtton" runat="server" Text="查 询" Width="80px" OnClick="ReadBtton_Click" />
                </td>
                <%--
                <td class="style1" style="font-size: 13px; z-index: 101;">
                    <asp:DropDownList ID="ddlKPIName" runat="server" Width="150px" Visible="false">
                    </asp:DropDownList>
                </td>
                --%>
            </tr>
        </table>
        <rsweb:ReportViewer ID="rvKPI" runat="server" Font-Names="Verdana" Font-Size="8pt"
            InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
            BackColor="#6591CE" LinkDisabledColor="ActiveCaptionText" ShowZoomControl="true"
            SizeToReportContent="true" ZoomMode="FullPage" Width="100%" Style="margin-right: 0px">
            <LocalReport ReportPath="KpiTagTeamReport\KpiEcTagTeamReport.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="odsTeam" Name="DataSet1" />
                    <rsweb:ReportDataSource DataSourceId="odsTitle" Name="DataSet2" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="odsTeam" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="RdlcReportBasic.DsEcHourTableAdapters.KPI_Ec_TeamReportResultTableAdapter">
            <SelectParameters>
                <asp:ControlParameter ControlID="tbStartDate" Name="StartTime" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="tbEndDate" Name="EndTime" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="ddlTeam" Name="Team" PropertyName="SelectedValue"
                    Type="String" DefaultValue="" />
                <asp:ControlParameter ControlID="ddlShift" Name="Shift" PropertyName="SelectedValue"
                    Type="String" DefaultValue="" />
                <asp:ControlParameter ControlID="ddlUnitEntity" Name="Entity" PropertyName="SelectedValue"
                    Type="String" />
                <asp:ControlParameter ControlID="lblCondition" Name="Condition" PropertyName="Text"
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
            SelectMethod="GetData" TypeName="RdlcReportBasic.DsEcHourTableAdapters.KPI_Ec_TagCheckedQueryTableAdapter">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlUnitEntity" Name="Entity" PropertyName="SelectedValue"
                    Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
