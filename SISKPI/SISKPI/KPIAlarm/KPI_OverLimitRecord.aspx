<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_OverLimitRecord.aspx.cs"
    MasterPageFile="~/MasterPage/ContentMasterPage.Master" Inherits="SISKPI.KPIAlarm.KPI_OverLimitRecord" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <%--  <td>工作单元:
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpPlants" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpPlants_SelectedIndexChanged" /></td>
            --%>
            <td>机组</td>
            <td>
                <asp:DropDownList ID="drpUnits" runat="server" CssClass="dropdown" AutoPostBack="True"
                    OnSelectedIndexChanged="drpUnits_SelectedIndexChanged" /></td>
            <td>指标</td>
            <td>
                <asp:DropDownList ID="drpKPI" runat="server" CssClass="dropdown" /></td>
            <td>值次</td>
            <td>
                <asp:DropDownList ID="drpShifts" runat="server" CssClass="dropdown">
                    <asp:ListItem Value="">【所有值次】</asp:ListItem>
                    <asp:ListItem Value="1">1值</asp:ListItem>
                    <asp:ListItem Value="2">2值</asp:ListItem>
                    <asp:ListItem Value="3">3值</asp:ListItem>
                    <asp:ListItem Value="4">4值</asp:ListItem>
                    <asp:ListItem Value="5">5值</asp:ListItem>
                </asp:DropDownList></td>
            <td>开始时间</td>
            <td>
                <asp:TextBox CssClass="Wdate inputCss" ID="txtBeginTime" runat="server" onfocus="WdatePicker({el:'txtBeginTime',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" Style="width: 100px; height: 25px;" /></td>
            <td>结束时间</td>
            <td>
                <asp:TextBox CssClass="Wdate inputCss" ID="txtEndTime" runat="server" onfocus="WdatePicker({el:'txtEndTime',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" Style="width: 100px; height: 25px;" /></td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="检索" CssClass="buttonCss" OnClick="btnSearch_Click"
                    OnClientClick="return checkSearch();" /></td>
            <td>
                <asp:Button ID="btnExport" runat="server" Text="导出" CssClass="buttonCss" OnClick="btnExport_Click" /></td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UP2" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Repeater ID="OverLimitRecordRepeater" runat="server">
                <HeaderTemplate>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <th>序号
                            </th>
                            <th>指标
                            </th>
                            <th>测点
                            </th>
                            <th>测点代码
                            </th>
                            <th>起报时间
                            </th>
                            <th>解报时间
                            </th>
                            <th>时长(秒)
                            </th>
                            <th>超限标准
                            </th>
                            <th>超限极值
                            </th>
                            <th>偏移量
                            </th>
                            <th>超限类型
                            </th>
                            <th>值次
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="tr1" onmouseover="javascript:this.className='tr3';"
                        onmouseout="javascript:this.className='tr1'">
                        <td class="VLine" align="center">
                            <%# Container.ItemIndex + 1%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("SAName")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("RealDesc")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("RealCode")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("AlarmStartTime")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("AlarmEndTime")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("Duration")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("StandardValue")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("AlarmValue")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("Offset")%>
                        </td>
                        <td class="VLine" align="center">
                            <%#GetAlarmType(Eval("AlarmType"))%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("Shift")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="tr2" onmouseover="javascript:this.className='tr3';"
                        onmouseout="javascript:this.className='tr2'">
                        <td class="VLine" align="center">
                            <%# Container.ItemIndex + 1%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("SAName")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("RealDesc")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("RealCode")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("AlarmStartTime")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("AlarmEndTime")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("Duration")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("StandardValue")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("AlarmValue")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("Offset")%>
                        </td>
                        <td class="VLine" align="center">
                            <%#GetAlarmType(Eval("AlarmType"))%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("Shift")%>
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
    </asp:UpdatePanel>
</asp:Content>
