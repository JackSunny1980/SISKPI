<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/ContentMasterPage.Master"
    CodeBehind="ShiftRaceScore.aspx.cs" Inherits="SISKPI.KPIWeb.ShiftRaceScore" %>


<asp:Content ContentPlaceHolderID="MainContent" ID="Content" runat="server">
    月份<asp:TextBox ID="txtYearMonth" runat="server" CssClass="inputCss Wdate"
        onfocus="WdatePicker({el:'txtYearMonth',dateFmt:'yyyy-MM',skin:'whyGreen'})" />
    <asp:Button ID="btnSearch" runat="server" CssClass="buttonCss" Text="搜 索" OnClick="btnSearch_Click" />
    <asp:Button ID="btnExport" runat="server" CssClass="buttonCss" Text="数据导出" OnClick="btnExport_Click" />
    <asp:UpdatePanel ID="UP1" runat="Server">
        <ContentTemplate>
            <asp:Repeater ID="ShiftBonusRepeater" runat="server">
                <HeaderTemplate>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <th rowspan="2">值别
                            </th>
                            <th rowspan="2">机组
                            </th>
                            <th colspan="2">分数
                            </th>
                            <th colspan="2">名次
                            </th>
                            <th colspan="2">总分
                            </th>
                            <th colspan="2">名次
                            </th>
                        </tr>
                        <tr>
                            <th>本月
                            </th>
                            <th>本年
                            </th>
                            <th>本月
                            </th>
                            <th>本年
                            </th>
                            <th>本月
                            </th>
                            <th>本年
                            </th>
                            <th>本月
                            </th>
                            <th>本年
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="tr1" onmouseover="javascript:this.className;this.className='tr3';" 
                        onmouseout="javascript:this.className='tr1'">
                        <td id="shift" runat="server" class="VLine" align="center">
                            <%# GetShiftName(Eval("Shift"))%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("UnitName")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("ShiftMonthScore")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("ShiftYearScore")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# GetRank(Eval("ShiftMonthOrderNo"))%>
                        </td>
                        <td class="VLine" align="center">
                            <%# GetRank(Eval("ShiftYearOrderNo"))%>
                        </td>
                        <td id="unitMonthScore" runat="server" class="VLine" align="center">
                            <%# Eval("UnitMonthScore")%>
                        </td>
                        <td id="unitYearScore" runat="server" class="VLine" align="center">
                            <%# Eval("UnitYearScore")%>
                        </td>
                        <td id="unitMonthOrder" runat="server" class="VLine" align="center">
                            <%# GetRank(Eval("UnitMonthOrderNo"))%> 
                        </td>
                        <td id="unitYearOrder" runat="server" class="VLine" align="center">
                            <%# GetRank(Eval("UnitYearOrderNo"))%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="tr2" onmouseover="javascript:this.className;this.className='tr3';"
                        onmouseout="javascript:this.className='tr2'">
                        <td id="shift" runat="server" class="VLine" align="center">
                            <%# GetShiftName(Eval("Shift"))%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("UnitName")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("ShiftMonthScore")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("ShiftYearScore")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# GetRank(Eval("ShiftMonthOrderNo"))%>
                        </td>
                        <td class="VLine" align="center">
                            <%# GetRank(Eval("ShiftYearOrderNo"))%>
                        </td>
                        <td id="unitMonthScore" runat="server" class="VLine" align="center">
                            <%# Eval("UnitMonthScore")%>
                        </td>
                        <td id="unitYearScore" runat="server" class="VLine" align="center">
                            <%# Eval("UnitYearScore")%>
                        </td>
                        <td id="unitMonthOrder" runat="server" class="VLine" align="center">
                            <%# GetRank(Eval("UnitMonthOrderNo"))%> 
                        </td>
                        <td id="unitYearOrder" runat="server" class="VLine" align="center">
                            <%# GetRank(Eval("UnitYearOrderNo"))%>
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

