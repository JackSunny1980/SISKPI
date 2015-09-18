<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true"
    MasterPageFile="~/MasterPage/ContentMasterPage.Master"
    CodeBehind="KPI_ECValue.aspx.cs" Inherits="SISKPI.KPI_ECValue" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .Alarm {
            color: red;
            background-color: yellow;
        }
    </style>
</asp:Content>
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
    <asp:Repeater ID="ECValueRepeater" runat="server">
        <HeaderTemplate>
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th>序号</th>
                    <th>机组</th>
                    <th>指标名称</th>
                    <th>单位</th>
                    <th>最优区间</th>
                    <th>实时值</th>
                    <th>本轮得分</th>
                    <th>本值累计</th>
                    <th>本月累计</th>
                    <th>计算时间</th>
                    <th>图表</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="tr1" onmouseover="javascript:this.className='tr3';"
                onmouseout="javascript:this.className='tr1'">
                <td class="VLine grid-number-col" align="center"><%#  Container.ItemIndex + 1 %></td>
                <td id="UnitName" runat="server" class="VLine" align="center"><%# Eval("UnitName") %></td>
                <td class="VLine" align="center"><%# Eval("ECName") %></td>
                <td class="VLine" align="center"><%# Eval("EngunitName") %></td>
                <td class="VLine" align="center"><%# Eval("ECOptExp") %></td>
                <td class="VLine <%# GetClass(Eval("ECQulity")) %>" align="center"><%# Eval("ECValue") %></td>
                <td class="VLine" align="center"><%# Eval("ECScore") %></td>
                <td class="VLine" align="center"><%# Eval("ECScoreDay") %></td>
                <td class="VLine" align="center"><%# Eval("ECScoreMonth") %></td>
                <td class="VLine" align="center"><%# Eval("ECTime") %></td>
                <td class="VLine" align="center">
                    <a href="ECChartPage.aspx?ECID=<%#Eval("ECID")%>&ECName=<%#GetECName(Eval("ECName"))%>&Kind=1" target="_self">
                        <img src="../images/chart.png" alt="图表" />
                    </a>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="tr2" onmouseover="javascript:this.className='tr3';"
                onmouseout="javascript:this.className='tr2'">
                <td class="VLine grid-number-col" align="center"><%#  Container.ItemIndex + 1 %></td>
                <td id="UnitName" runat="server" class="VLine" align="center"><%# Eval("UnitName")%></td>
                <td class="VLine" align="center"><%# Eval("ECName") %></td>
                <td class="VLine" align="center"><%# Eval("EngunitName") %></td>
                <td class="VLine" align="center"><%# Eval("ECOptExp") %></td>
                <td class="VLine <%# GetClass(Eval("ECQulity")) %>" align="center"><%# Eval("ECValue") %></td>
                <td class="VLine" align="center"><%# Eval("ECScore") %></td>
                <td class="VLine" align="center"><%# Eval("ECScoreDay") %></td>
                <td class="VLine" align="center"><%# Eval("ECScoreMonth") %></td>
                <td class="VLine" align="center"><%# Eval("ECTime") %></td>
                <td class="VLine" align="center">
                    <a href="ECChartPage.aspx?ECID=<%#Eval("ECID")%>&ECName=<%#GetECName(Eval("ECName"))%>&Kind=1" target="_self">
                        <img src="../images/chart.png" alt="图表" />
                    </a>
                </td>
            </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
