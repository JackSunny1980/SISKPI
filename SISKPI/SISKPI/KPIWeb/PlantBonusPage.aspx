<%@ Page Language="C#" MasterPageFile="~/MasterPage/ContentMasterPage.Master" 
    AutoEventWireup="true" CodeBehind="PlantBonusPage.aspx.cs" Inherits="SISKPI.KPIWeb.PlantBonusPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    月份<asp:TextBox ID="txtYearMonth" runat="server" CssClass="inputCss Wdate" 
        onfocus="WdatePicker({el:'txtYearMonth',dateFmt:'yyyy-MM',skin:'whyGreen'})" />
    <asp:Button ID="btnSearch" runat="server" CssClass="buttonCss" Text="搜 索" OnClick="btnSearch_Click" />
    <asp:Button ID="btnExport" runat="server" CssClass="buttonCss" Text="数据导出" OnClick="btnExport_Click" />

    <asp:UpdatePanel ID="UP1" runat="Server">
        <ContentTemplate>
            <asp:Repeater ID="PlantBonusRepeater" runat="server">
                <HeaderTemplate>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <th>值次
                            </th>
                            <th>1#机
                            </th>
                            <th>2#机
                            </th>
                            <th>全厂
                            </th>
                            <th>出力系数
                            </th>
                            <th>合计
                            </th>                           
                            <th>绩效                          
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="tr1" onmouseover="javascript:this.className='tr3';"
                        onmouseout="javascript:this.className='tr1'">
                        <td id="UnitName" runat="server" class="VLine" align="center">
                             <%# GetShiftName(Eval("Shift"))%>
                        </td>
                        <td class="VLine" align="center">
                           <%# Eval("Unit1Bonus")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("Unit2Bonus")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("PlantBonus")%>
                        </td>
                       <td class="VLine" align="center">
                            <%# Eval("PowerCapacity")%>
                        </td>
                       <td class="VLine" align="center">
                            <%# Eval("TotalBonus")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("Achievement")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="tr2" onmouseover="javascript:this.className='tr3';"
                        onmouseout="javascript:this.className='tr2'">
                         <td id="UnitName" runat="server" class="VLine" align="center">
                             <%# GetShiftName(Eval("Shift"))%>
                        </td>
                        <td class="VLine" align="center">
                           <%# Eval("Unit1Bonus")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("Unit2Bonus")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("PlantBonus")%>
                        </td>
                       <td class="VLine" align="center">
                            <%# Eval("PowerCapacity")%>
                        </td>
                       <td class="VLine" align="center">
                            <%# Eval("TotalBonus")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("Achievement")%>
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
