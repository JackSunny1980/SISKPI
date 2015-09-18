<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReliabilityLedger.aspx.cs"
    Inherits="SISKPI.Reliability.ReliabilityLedger" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>可靠性台账</title>
    <link href="../CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <script src="../js/GridViewColor.js" type="text/javascript"></script>
    <script src="../js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../js/DatePicker/WdatePicker.js" type="text/javascript" defer="defer"></script>
    <script type="text/javascript">
        function openDetailWindow(tagID) {
            var sURL = "ReliabilityDetail.aspx?TagID=" + tagID;
            window.open(sURL, "_blank", "", false);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <p>
    </p>
    <p align="center">
        <span class="title">设备可靠性台账</span></p>
    <div style="margin-left: auto; margin-right: auto; width: 95%;">
        月份：<asp:TextBox class="Wdate" ID="txtYearMonth" onfocus="WdatePicker({el:'txtYearMonth',dateFmt:'yyyy-MM',skin:'whyGreen'})"
            runat="server" />
        <asp:Button ID="btnSearch" runat="server" CssClass="buttonCss" Text="检索" OnClick="btnSearch_Click" />
        <asp:Button ID="btnExport" runat="server" CssClass="buttonCss" Text="导出" OnClick="btnExport_Click" />
        <asp:UpdatePanel ID="UP1" runat="Server">
            <ContentTemplate>
                <asp:Repeater ID="Repeater" runat="server">
                    <HeaderTemplate>
                        <table width="100%" border="1" cellpadding="0" cellspacing="0">
                            <tr class="GridViewHeaderStyle">
                                <th>
                                    序号
                                </th>
                                <th>
                                    值次
                                </th>
                                <th>
                                    设备名称
                                </th>
                                <th>
                                    累计停机时间(分钟)
                                </th>
                                <th>
                                    明细
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="GridViewRowStyle" onmouseover="SetNewColor(this)" onmouseout="SetOldColor(this)">
                            <td class="VLine" align="center">
                                <%# Container.ItemIndex+1%>
                            </td>
                            <td class="VLine" align="center">
                                <%#Eval("Shift")%>
                            </td>
                            <td class="VLine" align="center">
                                <%# Eval("KpiName")%>
                            </td>
                            <td class="VLine" align="center">
                               <%# Math.Round(Convert.ToDouble(Eval("DurationMinute")),2)%>
                            </td>
                            <td align="center">
                                <input type="button" value="明细" class="buttonCss" onclick="openDetailWindow('<%# Eval("TagID") %>');" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="GridViewAlternatingRowStyle" onmouseover="SetNewColor(this)" onmouseout="SetOldColor(this)">
                            <td class="VLine" align="center">
                                <%# Container.ItemIndex+1%>
                            </td>
                            <td class="VLine" align="center">
                                <%#Eval("Shift")%>
                            </td>
                            <td class="VLine" align="center">
                                <%# Eval("KpiName")%>
                            </td>
                            <td class="VLine" align="center">
                                <%# Math.Round(Convert.ToDouble(Eval("DurationMinute")),2)%>
                            </td>
                            <td align="center">
                                <input type="button" value="明细" class="buttonCss" onclick="openDetailWindow('<%# Eval("TagID") %>');" />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
