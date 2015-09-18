<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReliabilityDetail.aspx.cs"
    Inherits="SISKPI.Reliability.ReliabilityDetail" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>可靠性台账明细</title>
    <link href="../CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <script src="../js/GridViewColor.js" type="text/javascript"></script>
    <script src="../js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../js/DatePicker/WdatePicker.js" type="text/javascript" defer="defer"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <p>
    </p>
    <p align="center">
        <span class="title">设备可靠性台账明细</span></p>
    <div style="margin-left: auto; margin-right: auto; width: 95%;">
        开始时间：<asp:TextBox class="Wdate" ID="txtStartDate" onfocus="WdatePicker({el:'txtStartDate',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"
            runat="server" />
        结束时间：<asp:TextBox class="Wdate" ID="txtEndDate" onfocus="WdatePicker({el:'txtEndDate',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"
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
                                    设备名称
                                </th>
                                <th>
                                    异常开始日期时间
                                </th>
                                <th>
                                    恢复正常日期时间
                                </th>
                                <th>
                                    持续时间(分钟)
                                </th>
                                <th>
                                    可靠性事件描述
                                </th>
                                <th>
                                    填写人
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="GridViewRowStyle" onmouseover="SetNewColor(this)" onmouseout="SetOldColor(this)">
                            <td class="VLine" align="center">
                                <%# Container.ItemIndex+1%>
                            </td>
                            <td class="VLine" align="center">
                                <%# Eval("KpiName")%>
                            </td>
                            <td class="VLine" align="center">
                                <%# Eval("AlarmStartTime")%>
                            </td>
                            <td class="VLine" align="center">
                                <%# Eval("AlarmEndTime")%>
                            </td>
                            <td class="VLine" align="center">
                               <%# Math.Round(Convert.ToDouble(Eval("DurationMinute")),2)%>
                            </td>
                            <td class="VLine" align="center">
                            </td>
                            <td class="VLine" align="center">
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="GridViewAlternatingRowStyle" onmouseover="SetNewColor(this)" onmouseout="SetOldColor(this)">
                            <td class="VLine" align="center">
                                <%# Container.ItemIndex+1%>
                            </td>
                            <td class="VLine" align="center">
                                <%# Eval("KpiName")%>
                            </td>
                            <td class="VLine" align="center">
                                <%# Eval("AlarmStartTime")%>
                            </td>
                            <td class="VLine" align="center">
                                <%# Eval("AlarmEndTime")%>
                            </td>
                            <td class="VLine" align="center">
                               <%# Math.Round(Convert.ToDouble(Eval("DurationMinute")),2)%>
                            </td>
                            <td class="VLine" align="center">
                            </td>
                            <td class="VLine" align="center">
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:AspNetPager ID="Pager" runat="server" PageAlign="center" PageIndexBox="DropDownList"
                        OnPageChanged="Pager_PageChanged" ButtonImageNameExtension="enable/" CustomInfoTextAlign="Center"
                        DisabledButtonImageNameExtension="disable/" HorizontalAlign="Center" ImagePath="~/images/"
                        MoreButtonType="Text" NavigationButtonType="Image" NumericButtonType="Text" PagingButtonType="Image"
                        AlwaysShow="True" PagingButtonSpacing="8px" NumericButtonCount="5" EnableTheming="True"
                        PageSize="20">
                    </asp:AspNetPager>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
