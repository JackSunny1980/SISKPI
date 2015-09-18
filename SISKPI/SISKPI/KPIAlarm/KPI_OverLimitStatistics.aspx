<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_OverLimitStatistics.aspx.cs"
    Inherits="SISKPI.KPIAlarm.KPI_OverLimitStatistics" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SISKPI</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap-theme.css" rel="stylesheet" />
    <link href="../Content/themes/Styles/style.css" rel="stylesheet" />
    <link href="../Content/main.css" rel="stylesheet" />
    <link href="../Content/themes/Styles/gridview.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.js" type="text/javascript"></script>
    <link href="CSS/PagingStyle.css" rel="stylesheet" type="text/css" />
    <script src="../js/DatePicker/WdatePicker.js" type="text/javascript" defer="defer"></script>
    <script src="JS/OverLimitRecord.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <div class="tools_bar">
                <a runat="server" onserverclick="btnExport_Click" title="数据导出" id="btnExport" class="tools_btn"><span><b style="background: url('/Content/Themes/images/16/excel.png') 50% 4px no-repeat;">导出Excel</b></span></a>
                <a runat="server" onserverclick="btnRefresh_Click" title="刷新" id="btnRefresh" class="tools_btn"><span><b style="background: url('/Content/Themes/images/16/refresh.png') 50% 4px no-repeat;">刷 新</b></span></a>

            </div>
            <div class="btnbarcontetn" style="margin-top: 1px; background: #fff">
                <div>
                    <table border="0" class="frm-find" style="height: 45px;">
                        <tbody>
                            <tr>
                                <th>机组：
                                </th>
                                <td>
                                    <select id="dropUnityList" class="select" runat="server">
                                    </select><input id="selectedValue" type="hidden" runat="server" style="width: 100px;" />
                                </td>
                                <th>指标：
                                </th>
                                <td>
                                    <asp:DropDownList ID="drpTags" CssClass="select" runat="server">
                                        <asp:ListItem Value="">【所有指标】</asp:ListItem>
                                        <asp:ListItem Value="炉膛压力">炉膛压力</asp:ListItem>
                                        <asp:ListItem Value="最终过热器">最终过热器</asp:ListItem>
                                        <asp:ListItem Value="再热器A出口蒸汽温度">再热器A出口蒸汽温度</asp:ListItem>
                                        <asp:ListItem Value="再热器B出口蒸汽温度">再热器B出口蒸汽温度</asp:ListItem>
                                        <asp:ListItem Value="前墙上部水冷壁出口温度">前墙上部水冷壁出口温度</asp:ListItem>
                                        <asp:ListItem Value="侧墙上部水冷壁出口温度">侧墙上部水冷壁出口温度</asp:ListItem>
                                        <asp:ListItem Value="前墙螺旋水冷壁出口温度">前墙螺旋水冷壁出口温度</asp:ListItem>
                                        <asp:ListItem Value="侧墙螺旋水冷壁出口温度">侧墙螺旋水冷壁出口温度</asp:ListItem>
                                        <asp:ListItem Value="后墙螺旋水冷壁出口温度">后墙螺旋水冷壁出口温度</asp:ListItem>
                                        <asp:ListItem Value="大屏过热器出口壁温度">大屏过热器出口壁温度</asp:ListItem>
                                        <asp:ListItem Value="高温过热器出口管屏温度">高温过热器出口管屏温度</asp:ListItem>
                                        <asp:ListItem Value="高温再热器出口管屏温度">高温再热器出口管屏温度</asp:ListItem>
                                        <asp:ListItem Value="屏式过热器">屏式过热器</asp:ListItem>
                                        <asp:ListItem Value="SCR出口NOx含量">SCR出口NOx含量</asp:ListItem>
                                        <asp:ListItem Value="排放CO">排放CO</asp:ListItem>
                                    </asp:DropDownList>
                                </td>

                                <th>开始时间：
                                </th>
                                <td>
                                    <input class="txt" id="txtBeginTime" type="text" runat="server" onfocus="WdatePicker({el:'txtBeginTime',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" style="width: 100px; height: 25px;" />
                                </td>

                                <th>结束时间：
                                </th>
                                <td>
                                    <input class="txt" id="txtEndTime" type="text" runat="server" onfocus="WdatePicker({el:'txtEndTime',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" style="width: 100px; height: 25px;" />
                                </td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="buttonCss" Text="检 索" OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="datagrid-title-panel">
            <div class="datagrid-title">
                <img src="/Content/Themes/Images/32/202323.png" width="25" height="25" style="vertical-align: middle;">
                超限统计报表
            </div>
        </div>
        <asp:GridView ID="gvValue" CssClass="datagrid table table-hover table-bordered table-condensed" runat="server" AutoGenerateColumns="False"
            ShowFooter="false" EmptyDataText="没有满足条件的数据" AllowPaging="false">
            <Columns>
                <asp:TemplateField HeaderText="序号">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="KpiName" HeaderText="指标名称" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="AlarmTypeName" HeaderText="超限类型">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Duration" HeaderText="超限时长">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="AlarmCount" HeaderText="超限次数">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:AspNetPager ID="Pager" runat="server" PageAlign="center" PageIndexBox="DropDownList"
            OnPageChanged="Pager_PageChanged" ButtonImageNameExtension="enable/" CustomInfoTextAlign="Center"
            DisabledButtonImageNameExtension="disable/" HorizontalAlign="Center" ImagePath="~/images/"
            MoreButtonType="Text" NavigationButtonType="Image" NumericButtonType="Text" PagingButtonType="Image"
            AlwaysShow="True" PagingButtonSpacing="8px" NumericButtonCount="5" EnableTheming="True"
            PageSize="20">
        </asp:AspNetPager>
    </form>
</body>
</html>
