<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage/ContentMasterPage.Master"
    AutoEventWireup="true" CodeBehind="KPI_RealValue.aspx.cs" Inherits="SISKPI.KPI_RealValue" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Timer ID="Timer1" runat="server" Interval="60000" OnTick="Timer1_Tick">
    </asp:Timer>
    <div>
        <span style="float: left;">
            <asp:Label ID="nowtime" runat="server" Font-Bold="true" Text="当前时间：2012-12-03 12:25:00"></asp:Label>
        </span><span style="float: right;">
            <asp:Label ID="nowshift" runat="server" Font-Bold="true" Text="当前值班：一值"></asp:Label>
        </span>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <asp:Repeater ID="gvReal" runat="server">
                <HeaderTemplate>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <th>序号</th>
                            <%--  <th>测点</th>--%>
                            <th>测点名称</th>
                            <th>单位</th>
                            <th>实时值</th>
                            <th>时间戳</th>
                            <th>趋势</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="tr1" onmouseover="javascript:this.className='tr3';"
                        onmouseout="javascript:this.className='tr1'">
                        <td class="VLine grid-number-col" align="center"><%# Container.ItemIndex + 1%></td>
                        <%--  <td class="VLine" align="center"><%# Eval("RealCode") %></td>--%>
                        <td class="VLine" align="center"><%# Eval("RealDesc") %></td>
                        <td class="VLine" align="center"><%# Eval("RealEngunit") %></td>
                        <td class="VLine" align="center"><%# Eval("RealValue") %></td>
                        <td class="VLine" align="center"><%# Eval("RealTime") %></td>
                        <td class="VLine" align="center">
                            <a href="KPI_RealTrend.aspx?RealID=<%#Eval("RealID")%>" target="_self">
                                <img src="../images/chart_curve.png" alt="趋势" border="0" />
                            </a>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="tr2" onmouseover="javascript:this.className='tr3';"
                        onmouseout="javascript:this.className='tr2'">
                        <td class="VLine grid-number-col" align="center"><%# Container.ItemIndex + 1%></td>
                        <%-- <td class="VLine" align="center"><%# Eval("RealCode") %></td>--%>
                        <td class="VLine" align="center"><%# Eval("RealDesc") %></td>
                        <td class="VLine" align="center"><%# Eval("RealEngunit") %></td>
                        <td class="VLine" align="center"><%# Eval("RealValue") %></td>
                        <td class="VLine" align="center"><%# Eval("RealTime") %></td>
                        <td class="VLine" align="center">
                            <a href="KPI_RealTrend.aspx?RealID=<%#Eval("RealID")%>" target="_self">
                                <img src="../images/chart_curve.png" alt="趋势" border="0" />
                            </a>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--   <div style="display: none;">
        <asp:Label ID="lblPlant" runat="server" Text="单元号："></asp:Label>
        <asp:DropDownList ID="ddlPlant" runat="server" Width="120px" AutoPostBack="true"
            OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Label ID="lblUnit" runat="server" Text="机组号："></asp:Label>
        <asp:DropDownList ID="ddlUnit" runat="server" Width="120px" AutoPostBack="true" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged">
        </asp:DropDownList>
    </div>--%>
</asp:Content>
