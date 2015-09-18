<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/ContentMasterPage.Master" CodeBehind="ShiftScore.aspx.cs"
    Inherits="SISKPI.KPIWeb.ShiftScore" %>

<asp:Content ContentPlaceHolderID="MainContent" ID="Content" runat="server" ClientIDMode="Static">
    开始时间
    <asp:TextBox ID="txtStartDate" runat="server" CssClass="inputCss Wdate"
        onfocus="WdatePicker({el:'txtStartDate',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
    结束时间<asp:TextBox ID="txtEndDate" runat="server" CssClass="inputCss Wdate"
        onfocus="WdatePicker({el:'txtEndDate',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
    <asp:Button ID="btnSearch" runat="server" CssClass="buttonCss" Text="搜 索" OnClick="btnSearch_Click" />
    <asp:Button ID="btnExport" runat="server" CssClass="buttonCss" Text="数据导出" OnClick="btnExport_Click" />
    <asp:UpdatePanel ID="UP1" runat="Server">
        <ContentTemplate>
            <asp:Repeater ID="ScoreRepeater" runat="server">
                <HeaderTemplate>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <th>机组
                            </th>
                            <th>值次
                            </th>
                            <th>经济得分
                            </th>
                           <%-- <th>安全得分
                            </th>--%>
                            <th>总得分
                            </th>
                            <th>监盘总时间(小时)
                            </th>
                            <th>平均得分/小时
                            </th>
                            <th>排名
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="tr1" onmouseover="javascript:this.className;this.className='tr3';" onmouseout="javascript:this.className='tr1'">
                        <td id="UnitName" runat="server" class="VLine" align="center">
                            <%# Eval("UnitName")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# GetShiftName(DataBinder.Eval(Container.DataItem, "Shift"))%>
                        </td>
                        <td class="VLine" align="center">
                            <%# DataBinder.Eval(Container.DataItem, "ECScore")%>
                        </td>
                        <%--<td class="VLine" align="center">
                            <%# DataBinder.Eval(Container.DataItem, "SASCore")%>
                        </td>--%>
                        <td class="VLine" align="center">
                            <%# DataBinder.Eval(Container.DataItem,"TotalScore") %>
                        </td>
                        <td class="VLine" align="center">
                            <%# DataBinder.Eval(Container.DataItem, "TotalHours")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Math.Round(Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "AvgScore")),2)%>
                        </td>
                        <td class="VLine" align="center">
                            <%# GetRank(DataBinder.Eval(Container.DataItem, "OrderNo"))%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="tr2" onmouseover="javascript:this.className;this.className='tr3';" onmouseout="javascript:this.className='tr2'">
                        <td id="UnitName" runat="server" class="VLine" align="center">
                            <%# Eval("UnitName")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# GetShiftName(DataBinder.Eval(Container.DataItem, "Shift"))%>
                        </td>
                        <td class="VLine" align="center">
                            <%# DataBinder.Eval(Container.DataItem, "ECScore")%>
                        </td>
                        <%--<td class="VLine" align="center">
                            <%# DataBinder.Eval(Container.DataItem, "SASCore")%>
                        </td>--%>
                        <td class="VLine" align="center">
                            <%# DataBinder.Eval(Container.DataItem,"TotalScore") %>
                        </td>
                        <td class="VLine" align="center">
                            <%# DataBinder.Eval(Container.DataItem, "TotalHours")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Math.Round(Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "AvgScore")),2)%>
                        </td>
                        <td class="VLine" align="center">
                            <%# GetRank(DataBinder.Eval(Container.DataItem, "OrderNo"))%>
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

</asp:Content>

