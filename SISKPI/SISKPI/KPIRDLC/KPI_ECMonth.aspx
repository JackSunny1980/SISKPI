<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_ECMonth.aspx.cs" Inherits="SISKPI.KPI_ECMonth" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISKPI Config</title>
    <base target="_self" />
    <script language="javascript" type="text/javascript" src="../js/DatePicker/WdatePicker.js"
        defer="defer"></script>
    <link href="../CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <script src="../js/ProcessBar.js" type="text/javascript"></script>
    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../js/tab/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="../js/Tab/jquery.tabs.js" type="text/javascript"></script>
    <%--<script src="../js/Common.js" type="text/javascript"></script>--%>
    <%--<script src="../js/Config.js" type="text/javascript"></script>--%>
    
    <script type="text/javascript" language="javascript">
        function OpenOvertimeDlog(frmWin, width, height) {
            var me;
            var action;
            action = frmWin;

            // 把父页面窗口对象当作参数传递到对话框中，以便对话框操纵父页自动刷新。 
            me = "KpiEcTagDayMultiselect.aspx?action=" + action + "";

            // 显示对话框。
            //var sRet = window.showModalDialog('a.html','title','scrollbars=no;resizable=no;help=no;status=no;dialogTop=25; dialogLeft=0;dialogHeight=350px;dialogwidth=410px;');
            window.showModalDialog(me, null, 'dialogWidth=' + width + 'px;dialogHeight=' + height + 'px;help:no;status:no')

            //window.open ("page.html", "newwindow", "height=100, width=400, toolbar =no, menubar=no, scrollbars=no, resizable=no, location=no, status=no") //写成一行
            //window.open(me, "", "width=" + width + ",height=" + height + ",toolbar =no, menubar=no, scrollbars=no, resizable=no, location=no, status=no") 
        } 
    </script>
</head>
<body onresize="bodyresize();">
    <script language="javascript" type="text/javascript">
        function bodyresize() {
            var o_r = document.getElementById("rvKPI");
            o_r.style.width = (document.body.clientWidth).toString() + "px";
        }
    </script>
    <form id="form1" runat="server" style="width: 100%; text-align: center;">
    <div style="margin: 0px; width: 100%; text-align: center;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table width="100%" align="center" class="table" id="table1">
            <tr style="width: 100%">
                <td valign="middle" align="center" style="width: 100%; height: 40px;">
                    <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                    <asp:Label ID="lblTitle" runat="server" Class="title" Text="实时指标日报表"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%" class="table_tr">
                <td align="center" style="width: 100%">
                    <div style="margin: 0px; width: 95%; text-align: left;">
                        <table class="table" id="table2" style="width: 95%">
                            <tr style="width: 100%">
                                <td style="width: 300px;">
                                    <asp:Label ID="Label1" runat="server" Text="选择日期:"></asp:Label>
                                    <input class="Wdate" id="txt_Day" visible="true" style="width: 120px" onfocus="WdatePicker({el:'txt_Day',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"
                                        type="text" runat="server" value="" />
                                    <asp:Button ID="btnQuery" runat="server" Text="查 询" Width="80px" OnClick="btnQuery_Click" />
                                </td>
                                <td style="width: 200px;">
                                    <asp:Label ID="lblQueryDay" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblWebID" runat="server" Text="" Visible="false"></asp:Label>
                                </td>
                                <td style="width: 200px;">
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr style="width: 100%">
                <td valign="middle" align="center" style="width: 100%;">
                    <div style="margin: 0px; width: 95%; text-align: center;">
                        <rsweb:ReportViewer ID="rvKPI" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                            BackColor="#6591CE" LinkDisabledColor="ActiveCaptionText" SizeToReportContent="True"
                            ZoomMode="FullPage" Width="100%" Style="margin-right: 0px" DocumentMapWidth="100%">
                            <LocalReport ReportPath="KPIRDLC\KPI_ECDay.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource DataSourceId="odsECDay" Name="dsData" />
                                    <%--<rsweb:ReportDataSource DataSourceId="odsTitle" Name="DataSet2" />--%>
                                    <rsweb:ReportDataSource DataSourceId="odsWebDesc" Name="dsTitle" />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </div>
                </td>
            </tr>
            <tr style="width: 100%">
                <td valign="middle" align="center" style="width: 100%;">
                    <%--<rsweb:ReportDataSource DataSourceId="odsTitle" Name="DataSet2" />--%>
                    <asp:ObjectDataSource ID="odsECDay" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetData" TypeName="SISKPI.KPIECSetTableAdapters.ECDayQueryTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="lblQueryDay" Name="QueryDay" PropertyName="Text"
                                Type="String" />
                            <asp:ControlParameter ControlID="lblWebID" Name="WebID" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsWebDesc" runat="server" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetData" TypeName="SISKPI.KPIECSetTableAdapters.GetWebDescTableAdapter">
                        <InsertParameters>
                            <asp:Parameter Name="WebDesc" Type="String" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:ControlParameter ControlID="lblWebID" Name="WebID" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <%-- <asp:ObjectDataSource ID="odsDay" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetData" TypeName="RdlcReportBasic.DsEcHourTableAdapters.KPI_Ec_DayReportResultTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="tbStartDate" Name="BeginTime" PropertyName="Text"
                                Type="String" DefaultValue="" />
                            <asp:ControlParameter ControlID="ddlStartHour" Name="hour1" PropertyName="SelectedValue"
                                Type="String" />
                            <asp:ControlParameter ControlID="tbEndDate" Name="EndTime" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="ddlEndHour" Name="hour2" PropertyName="SelectedValue"
                                Type="String" />
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
                    </asp:ObjectDataSource>--%>
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
                        <font color="#800080" size="2" style="font-weight: bold">操作正在执行，请稍候...</font></div>
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
