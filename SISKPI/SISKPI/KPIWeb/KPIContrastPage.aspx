<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ContentMasterPage.Master" 
    AutoEventWireup="true" CodeBehind="KPIContrastPage.aspx.cs" Inherits="SISKPI.KPIWeb.KPIContrastPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    开始日期<asp:TextBox ID="txtStartDate" runat="server" CssClass="inputCss Wdate"
        onfocus="WdatePicker({el:'txtStartDate',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
    截止日期<asp:TextBox ID="txtEndDate" runat="server" CssClass="inputCss Wdate"
        onfocus="WdatePicker({el:'txtEndDate',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
    对比开始日期<asp:TextBox ID="txtLStartDate" runat="server" CssClass="inputCss Wdate"
        onfocus="WdatePicker({el:'txtLStartDate',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
    对比截止日期<asp:TextBox ID="txtLEndDate" runat="server" CssClass="inputCss Wdate"
        onfocus="WdatePicker({el:'txtLEndDate',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
    <asp:Button ID="btnSearch" runat="server" CssClass="buttonCss" Text="搜 索" OnClick="btnSearch_Click" />
    <asp:Button ID="btnExport" runat="server" CssClass="buttonCss" Text="数据导出" OnClick="btnExport_Click" />
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <asp:Repeater ID="KPIRepeater" runat="server">
                <HeaderTemplate>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <th rowspan="2">指标</th>
                            <th colspan="5"><%#StartDate %>至<%#EndDate %>累计均值</th>
                            <th colspan="5"><%#LStartDate %>至<%#LEndDate %>累计均值</th>
                        </tr>
                        <tr>
                            <th>一值</th>
                            <th>二值</th>
                            <th>三值</th>
                            <th>四值</th>
                            <th>五值</th>
                            <th>一值</th>
                            <th>二值</th>
                            <th>三值</th>
                            <th>四值</th>
                            <th>五值</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="tr1" onmouseover="javascript:this.className='tr3';"
                        onmouseout="javascript:this.className='tr1'">
                        <td class="VLine" align="center"><%#Eval("ECName") %></td>
                        <td class="VLine" align="center"><%#GetNumericString(Eval("Shift1Value"))%></td>
                        <td class="VLine" align="center"><%#GetNumericString(Eval("Shift2Value")) %></td>
                        <td class="VLine" align="center"><%#GetNumericString(Eval("Shift3Value")) %></td>
                        <td class="VLine" align="center"><%#GetNumericString(Eval("Shift4Value")) %></td>
                        <td class="VLine" align="center"><%#GetNumericString(Eval("Shift5Value")) %></td>

                        <td class="VLine" align="center"><%#GetNumericString(Eval("Shift1HValue")) %></td>
                        <td class="VLine" align="center"><%#GetNumericString(Eval("Shift2HValue")) %></td>
                        <td class="VLine" align="center"><%#GetNumericString(Eval("Shift3HValue")) %></td>
                        <td class="VLine" align="center"><%#GetNumericString(Eval("Shift4HValue")) %></td>
                        <td class="VLine" align="center"><%#GetNumericString(Eval("Shift5HValue")) %></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="tr2" onmouseover="javascript:this.className='tr3';"
                        onmouseout="javascript:this.className='tr2'">
                        <td class="VLine" align="center"><%#Eval("ECName") %></td>
                        <td class="VLine" align="center"><%#GetNumericString(Eval("Shift1Value")) %></td>
                        <td class="VLine" align="center"><%#GetNumericString(Eval("Shift2Value")) %></td>
                        <td class="VLine" align="center"><%#GetNumericString(Eval("Shift3Value")) %></td>
                        <td class="VLine" align="center"><%#GetNumericString(Eval("Shift4Value")) %></td>
                        <td class="VLine" align="center"><%#GetNumericString(Eval("Shift5Value")) %></td>

                        <td class="VLine" align="center"><%#GetNumericString(Eval("Shift1HValue")) %></td>
                        <td class="VLine" align="center"><%#GetNumericString(Eval("Shift2HValue")) %></td>
                        <td class="VLine" align="center"><%#GetNumericString(Eval("Shift3HValue")) %></td>
                        <td class="VLine" align="center"><%#GetNumericString(Eval("Shift4HValue")) %></td>
                        <td class="VLine" align="center"><%#GetNumericString(Eval("Shift5HValue")) %></td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
           <%-- <asp:AsyncPostBackTrigger ControlID="btnExport" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
