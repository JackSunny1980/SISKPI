<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ContentMasterPage.Master" AutoEventWireup="true" CodeBehind="ECChartPage.aspx.cs" Inherits="SISKPI.KPIWeb.ECChartPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">
        $(function () {
            $("#tabs").tabs();
        });

        function onSelected(index) {
            $("#tabs").tabs("option", "active", index);
        }

        function ReturnECValue() {
            var URL = "<%=ReturnURL%>";
            window.open(URL, "_self", "", false);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    开始时间
    <asp:TextBox ID="txtStartDate" runat="server" CssClass="inputCss Wdate"
        onfocus="WdatePicker({el:'txtStartDate',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
    结束时间<asp:TextBox ID="txtEndDate" runat="server" CssClass="inputCss Wdate"
        onfocus="WdatePicker({el:'txtEndDate',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
    <asp:Button ID="btnSearch" runat="server" CssClass="buttonCss" Text="搜 索" OnClick="btnSearch_Click" />
    <asp:Button ID="btnExport" runat="server" CssClass="buttonCss" Text="数据导出" OnClick="btnExport_Click" />
    <input type="button" value="返回" class="buttonCss" onclick="ReturnECValue();" />
    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">数据</a></li>
            <li><a href="#tabs-2">图表</a></li>
        </ul>
        <div id="tabs-1">
            <asp:UpdatePanel ID="UP1" runat="Server">
                <ContentTemplate>
                    <asp:Repeater ID="ECDataRepeater" runat="server">
                        <HeaderTemplate>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <th>指标</th>
                                    <th>计量单位</th>
                                    <th>考核日期
                                    </th>
                                    <th>值别
                                    </th>
                                    <th>最小值
                                    </th>
                                    <th>平均值
                                    </th>
                                    <th>最大值
                                    </th>
                                    <th>累计得分
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="tr1" onmouseover="javascript:this.className;this.className='tr3';"
                                onmouseout="javascript:this.className='tr1'">
                                <td class="VLine" align="center"><%# ECName%> </td>
                                <td class="VLine" align="center"><%# Eval("EngunitName")%> </td>
                                <td class="VLine" align="center"><%# Eval("CheckDate")%> </td>
                                <td class="VLine" align="center"><%# Eval("Shift")%>值 </td>
                                <td class="VLine" align="center"><%# Eval("MinValue")%> </td>
                                <td class="VLine" align="center"><%# Eval("AvgValue")%> </td>
                                <td class="VLine" align="center"><%# Eval("MaxValue")%> </td>
                                <td class="VLine" align="center"><%# Eval("Score")%> </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="tr2" onmouseover="javascript:this.className;this.className='tr3';"
                                onmouseout="javascript:this.className='tr2'">
                                <td class="VLine" align="center"><%#ECName%> </td>
                                <td class="VLine" align="center"><%# Eval("EngunitName")%> </td>
                                <td class="VLine" align="center"><%# Eval("CheckDate")%> </td>
                                <td class="VLine" align="center"><%# Eval("Shift")%>值 </td>
                                <td class="VLine" align="center"><%# Eval("MinValue")%> </td>
                                <td class="VLine" align="center"><%# Eval("AvgValue")%> </td>
                                <td class="VLine" align="center"><%# Eval("MaxValue")%> </td>
                                <td class="VLine" align="center"><%# Eval("Score")%> </td>
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
        <div id="tabs-2">
            <asp:UpdatePanel ID="UP2" runat="server">
                <ContentTemplate>
                    <asp:Chart ID="Chart1" runat="server" BorderlineColor="26, 59, 105" TextAntiAliasingQuality="Normal"
                        BorderLineStyle="Solid" BackGradientType="TopBottom" BackGradientEndColor="65, 140, 240"
                        Width="1000px" Height="800px" runat="server">
                        <Legends>
                            <asp:Legend Font="宋体, 10pt" IsTextAutoFit="False" Name="ChartLegend" Alignment="Center" Docking="Bottom">
                            </asp:Legend>

                        </Legends>
                        <Titles>
                            <asp:Title Name="Default" Font="宋体, 12pt" Text="" />
                        </Titles>
                        <Series>
                            <asp:Series Name="MinValueSeries" Color="Khaki" ShadowOffset="0" YValueType="Double"
                                XValueType="DateTime" ChartArea="ChartArea1" ChartType="Spline" Legend="ChartLegend" LegendText="最小值">

                                <EmptyPointStyle BorderWidth="0" />
                            </asp:Series>

                            <asp:Series Name="AvgValueSeries" Color="LimeGreen" ShadowOffset="0" YValueType="Double"
                                XValueType="DateTime" ChartArea="ChartArea1" ChartType="Spline" Legend="ChartLegend" LegendText="平均值">
                                <EmptyPointStyle BorderWidth="0" />
                            </asp:Series>

                            <asp:Series Name="MaxValueSeries" Color="Red" ShadowOffset="0" YValueType="Double"
                                XValueType="DateTime" ChartArea="ChartArea1" ChartType="Spline" Legend="ChartLegend" LegendText="最大值">
                                <EmptyPointStyle BorderWidth="0" />
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" BackColor="White" ShadowOffset="5">
                                <AxisY IsStartedFromZero="False">
                                    <LabelStyle Format="N2"></LabelStyle>
                                    <MajorTickMark Enabled="False"></MajorTickMark>
                                    <MajorGrid LineColor="LightGray" LineDashStyle="DashDot"></MajorGrid>
                                </AxisY>
                                <AxisX IsLabelAutoFit="False">
                                    <MinorGrid Enabled="True" LineColor="LightGray" LineDashStyle="DashDot" />
                                    <LabelStyle Format="MM月dd日HH时" Angle="0"></LabelStyle>
                                    <MajorGrid LineColor="LightGray" Interval="Auto" IntervalType="Hours" LineDashStyle="DashDot"></MajorGrid>
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                        <BorderSkin SkinStyle="Emboss" BackColor="DodgerBlue" />
                    </asp:Chart>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <%-- <asp:Chart ID="Chart1" runat="server">
        <Series>
            <asp:Series Name="Series1">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>--%>
</asp:Content>
