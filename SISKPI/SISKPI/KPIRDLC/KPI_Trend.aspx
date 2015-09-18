<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="KPI_Trend.aspx.cs"
    Inherits="SISKPI.KPI_Trend" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISOPM</title>
    <script language="javascript" type="text/javascript" src="../js/DatePicker/WdatePicker.js"
        defer="defer"></script>
    <link href="../CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <script src="../js/ProcessBar.js" type="text/javascript"></script>
    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../js/tab/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="../js/Tab/jquery.tabs.js" type="text/javascript"></script>
    <script src="../js/Common.js" type="text/javascript"></script>
    <script src="../js/Config.js" type="text/javascript"></script>
    <script src="../js/GridViewColor.js" type="text/javascript"></script>
    <script type="text/javascript">
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
    <form id="form1" runat="server">
    <div style="margin: 0px; width: 100%;" align="center">
<%--        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Timer ID="Timer1" runat="server" Interval="600000" OnTick="Timer1_Tick">
                </asp:Timer>--%>
                <table class="table" style="width: 95%;" align="center">
                    <tr style="width: 100%">
                        <td align="center" style="width: 100%">
                            <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                            <asp:Label ID="lblTitle" runat="server" Class="title" Text="实时指标历史数据查询"></asp:Label>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td style="width: 100%" align="center">
                            <div style="width: 95%" align="right">
                                <table style="width: 100%">
                                    <tr style="width: 100%">
                                        <td style="width: 30%" align="left">
                                            <asp:Label ID="Label3" runat="server" Text="开始时间:  "></asp:Label>
                                            <input class="Wdate" id="txt_ST" visible="true" style="width: 140px" onfocus="WdatePicker({el:'txt_ST',dateFmt:'yyyy-MM-dd HH:mm:00',skin:'whyGreen'})"
                                                type="text" runat="server" value="" />
                                        </td>
                                        <td style="width: 30%" align="left">
                                            <asp:Label ID="Label1" runat="server" Text="结束时间:  "></asp:Label>
                                            <input class="Wdate" id="txt_ET" visible="true" style="width: 140px" onfocus="WdatePicker({el:'txt_ET',dateFmt:'yyyy-MM-dd HH:mm:00',skin:'whyGreen'})"
                                                type="text" runat="server" value="" />
                                        </td>
                                        <td style="width: 30%" align="right">
                                        </td>
                                    </tr>
                                    <tr style="width: 100%">
                                        <td style="width: 30%" align="left">
                                            <asp:Label ID="Label2" runat="server" Text="选择指标:  "></asp:Label>
                                            <asp:DropDownList ID="ddl_EC" runat="server" Width="120px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 30%" align="left">
                                            <asp:Button ID="btnQuery" runat="server" Text="查 询" Width="120px" 
                                                onclick="btnQuery_Click" />
                                        </td>
                                        <td style="width: 30%" align="right">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td align="center" style="width: 100%">
                            <div style="width: 100%">
                                <fieldset class="field_info" style="width: 95%;">
                                    <legend>趋势曲线</legend>
                                    <table style="width: 100%">
                                        <tr style="width: 100%;">
                                            <td style="width: 80%" align="center">
                                                <asp:Chart ID="pieHC" runat="server" Height="400px" Width="900px">
                                                </asp:Chart>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td style="width: 100%" align="center">
                            <asp:GridView ID="gvHC" CssClass="GridViewStyle" Width="100%" Visible="true" runat="server"
                                AutoGenerateColumns="False" EmptyDataText="没有满足条件的数据" AllowPaging="false" OnRowDataBound="gvHC_RowDataBound">
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <RowStyle CssClass="GridViewRowStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <HeaderStyle CssClass="GridViewHeaderStyle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <input type="hidden" runat="server" id="keyid" value='<%# Eval("ParamID").ToString()%>' />
                                            <%#  Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ParamName" HeaderText="名称"></asp:BoundField>
                                    <asp:BoundField DataField="ParamEngunit" HeaderText="单位">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ParamBValue" HeaderText="影响煤耗(g/kw.h)">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ParamQValue" HeaderText="影响热耗(kJ/kw.h)" Visible="false">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
<%--            </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
    <div id="Lay1" style="z-index: 100; visibility: hidden; left: 1%; width: 260px; cursor: crosshair;
        position: absolute; top: -20px; height: 14%; background-color: transparent; text-align: center">
        <br>
        <b></b>
        <table align="center" style="width: 260px; height: 40px" class="BoderTable">
            <tr>
                <td>
                    <div>
                        <font color="green" size="2" style="font-weight: bold">正在查询数据库，请稍候...</font></div>
                    <div style="border-right: green 1px solid; padding-right: 2px; border-top: green 1px solid;
                        padding-left: 2px; font-size: 8pt; padding-bottom: 2px; border-left: green 1px solid;
                        padding-top: 2px; border-bottom: green 1px solid">
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
