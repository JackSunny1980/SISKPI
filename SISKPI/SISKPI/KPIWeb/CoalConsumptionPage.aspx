<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ContentMasterPage.Master" 
    AutoEventWireup="true" CodeBehind="CoalConsumptionPage.aspx.cs" 
    Inherits="SISKPI.KPIWeb.CoalConsumptionPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .Alarm {
            color: #F00;           
            font-weight:bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Timer ID="Timer1" runat="server" Interval="60000" OnTick="Timer1_Tick">
    </asp:Timer>

    <div>
        <span style="float: left;">
            <asp:Label ID="nowtime" runat="server" Font-Bold="true" Text="当前时间：2012-12-03 12:25:00"></asp:Label>
        </span><span style="float: right;">
            <asp:Label ID="nowshift" runat="server" Font-Bold="true" Text="当前值班：一值"></asp:Label>
        </span>
    </div>
    <asp:Repeater ID="ECValueRepeater" runat="server">
        <HeaderTemplate>
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th>序号</th>
                    <th>机组</th>
                    <th>项目</th>
                    <th>单位</th>
                    <th>基准值</th>
                    <th>供电煤耗</th>                   
                    <th>计算时间</th>
                    <th>图表</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="tr1 <%# GetClass(Eval("ECID")) %>" >
                <td class="VLine grid-number-col" align="center"><%#  Container.ItemIndex + 1 %></td>
                <td id="UnitName" runat="server" class="VLine" align="center"><%# Eval("UnitName") %></td>
                <td class="VLine" align="center"><%# Eval("ECName") %></td>
                <td class="VLine" align="center"><%# Eval("EngunitName") %></td>
                <td class="VLine" align="center"><%# Eval("ECNote") %></td>
                <td class="VLine"  align="center"><%# Eval("ECValue") %></td>                
                <td class="VLine" align="center"><%# Eval("ECTime") %></td>
                <td class="VLine" align="center">
                    <a href="ECChartPage.aspx?ECID=<%#Eval("ECID")%>&ECName=<%#GetECName(Eval("ECName"))%>&Kind=2" target="_self">
                        <img src="../images/chart.png" alt="图表"  border="0"/>
                    </a>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="tr2  <%# GetClass(Eval("ECID")) %>" >
                <td class="VLine grid-number-col" align="center"><%#  Container.ItemIndex + 1 %></td>
                <td id="UnitName" runat="server" class="VLine" align="center"><%# Eval("UnitName") %></td>
                <td class="VLine" align="center"><%# Eval("ECName") %></td>
                <td class="VLine" align="center"><%# Eval("EngunitName") %></td>
                <td class="VLine" align="center"><%# Eval("ECNote") %></td>
                <td class="VLine"  align="center"><%# Eval("ECValue") %></td>                
                <td class="VLine" align="center"><%# Eval("ECTime") %></td>
                <td class="VLine" align="center">
                    <a href="ECChartPage.aspx?ECID=<%#Eval("ECID")%>&ECName=<%#GetECName(Eval("ECName"))%>&Kind=2" target="_self">
                        <img src="../images/chart.png" alt="图表" border="0" />
                    </a>
                </td>
            </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
     <asp:Button ID="Button1" runat="server" Text="Button" />
</asp:Content>
