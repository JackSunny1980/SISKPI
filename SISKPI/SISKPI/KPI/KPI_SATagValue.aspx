<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_SATagValue.aspx.cs"
 MasterPageFile="~/MasterPage/ContentMasterPage.Master"   Inherits="SISKPI.KPI.KPI_SATagValue" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    机组 <asp:DropDownList ID="drpUnits" CssClass="select" runat="server"/>
    开始时间     
    <asp:TextBox ID="txtStartDate" CssClass="inputCss Wdate" runat="server"
        onfocus="WdatePicker({el:'txtStartDate',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
                      
    结束时间 <asp:TextBox ID="txtEndDate" CssClass="inputCss Wdate" runat="server"
        onfocus="WdatePicker({el:'txtEndDate',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
   
    <asp:Button ID="btnSearch" runat="server" CssClass="buttonCss" Text="检 索" OnClick="btnSearch_Click" />
    <asp:Button ID="btnExport" OnClick="btnExport_Click" Text="数据导出" runat="server" CssClass="buttonCss" />               
        <asp:UpdatePanel ID="UP1" runat="server">
            <ContentTemplate>
                <asp:Repeater ID="SATagValueRepeater" runat="server">
                    <HeaderTemplate>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <th>值次
                                </th>
                                <th>安全指标
                                </th>
                                <th>累计超限次数
                                </th>
                                <th>累计超限时长(分钟)
                                </th>
                                <th>累计得分
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="tr1" onmouseover="javascript:this.className='tr3';"
                                onmouseout="javascript:this.className='tr1'">
                            <td align="center" id="Shift" runat="server" class="VLine" >
                                <%# Eval("Shift") %>
                            </td>
                            <td class="VLine" align="center">
                                <%# Eval("SAName")%>
                            </td>
                            <td class="VLine" align="center">
                                <%# Eval("TotalCount")%>
                            </td>
                            <td class="VLine" align="center">
                                <%# Eval("TotalDuration")%>
                            </td>
                            <td class="VLine" align="center">
                                <%# Eval("SAScore") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                         <tr class="tr2" onmouseover="javascript:this.className='tr3';"
                                onmouseout="javascript:this.className='tr2'">
                            <td align="center" id="Shift" runat="server" class="VLine" >
                                <%# Eval("Shift") %>
                            </td>
                            <td class="VLine" align="center">
                                <%# Eval("SAName")%>
                            </td>
                            <td class="VLine" align="center">
                                <%# Eval("TotalCount")%>
                            </td>
                            <td class="VLine" align="center">
                                <%# Eval("TotalDuration")%>
                            </td>
                            <td class="VLine" align="center">
                                <%# Eval("SAScore") %>
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
