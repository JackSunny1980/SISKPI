<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_WorkConfig.aspx.cs"
    Inherits="SISKPI.KPI_WorkConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISKPI</title>
    <link href="../CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <base target="_self" />
    <script language="javascript" type="text/javascript" src="../js/DatePicker/WdatePicker.js"
        defer="defer"></script>
    <link rel="stylesheet" href="../js/tab/jquery.tabs.css" type="text/css" media="print, projection, screen" />
    <!--[if lte IE 7]>
        <link rel="stylesheet" href="../js/tab/jquery.tabs-ie.css" type="text/css" media="projection, screen">
        <![endif]-->
    <script src="../js/ProcessBar.js" type="text/javascript"></script>
    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../js/Common.js" type="text/javascript"></script>
    <script src="../js/tab/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="../js/Tab/jquery.tabs.js" type="text/javascript"></script>
    <script src="../js/Config.js" type="text/javascript"></script>
    <script src="../js/GridViewColor.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            call();
        });

        function apply() {
            window.opener = null;
            window.close();
        }


        function call() {
            SetTableWidth('table1');
            SetDivWidth('div1');
            SetDivWidth('divtag');
            SetTableWidth('table2');
            //SetTableWidth('table3');
        }          
    </script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
    <div style="margin: 0px; width: 100%; text-align: center;">
        <table width="100%" align="center" class="table" id="table1">
            <tr style="width: 100%">
                <td align="center" style="width: 100%">
                    <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                    <asp:Label ID="lblTitle" runat="server" Class="title" Text="倒班配置"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%">
                <td width="100%">
                    <div style="width: 95%">
                        <fieldset class="field_info" style="width: 100%;">
                            <legend>倒班信息</legend>
                            <table style="width: 100%;" class="table">
                                <tr style="width: 100%;">
                                    <td width="100%">
                                        <asp:GridView ID="gvWork" CssClass="GridViewStyle" Width="100%" runat="server" AutoGenerateColumns="False"
                                            EmptyDataText="没有满足条件的数据" AllowPaging="False" OnRowDataBound="gvWork_RowDataBound"
                                            OnRowCommand="gvWork_RowCommand">
                                            <FooterStyle CssClass="GridViewFooterStyle" />
                                            <RowStyle CssClass="GridViewRowStyle" />
                                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                            <PagerStyle CssClass="GridViewPagerStyle" />
                                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <input type="hidden" runat="server" id="workid" value='<%# Eval("WorkID").ToString()%>' />
                                                        <%#  Container.DataItemIndex + 1%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="WorkName" HeaderText="名称" ReadOnly="true">
                                                    <ControlStyle Width="60px"></ControlStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="WorkDesc" HeaderText="描述">
                                                    <ControlStyle Width="100px"></ControlStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="WorkStartTime" HeaderText="开始时间">
                                                    <ControlStyle Width="60px"></ControlStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="WorkEndTime" HeaderText="结束时间">
                                                    <ControlStyle Width="60px"></ControlStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="WorkBaseShifts" HeaderText="基准值数">
                                                    <ControlStyle Width="60px"></ControlStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="WorkBaseDays" HeaderText="基准天数">
                                                    <ControlStyle Width="60px"></ControlStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="是否有效" ItemStyle-HorizontalAlign="Center">
                                                    <ControlStyle Width="40px"></ControlStyle>
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlValid" runat="server" DataSourceID="GetYesNo" DataTextField="Name"
                                                            DataValueField="Value">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="WorkNote" HeaderText="备注" ControlStyle-Width="100px">
                                                    <ControlStyle Width="100px"></ControlStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="view" runat="server" Text="详情" CommandName="dataView" CommandArgument='<%# Eval("WorkID") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="60px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="delete" runat="server" Text="删除" CommandName="dataDelete" CommandArgument='<%# Eval("WorkID") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="60px" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 100%; margin: 0px;">
                    <div style="width: 95%">
                        <table style="width: 100%;" class="table">
                            <tr style="width: 100%;">
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 100%; margin: 0px;">
                    <div style="width: 95%;">
                        <fieldset class="field_info" style="width: 100%;">
                            <legend>运转分配</legend>
                            <table style="width: 100%;" class="table">
                                <tr style="width: 100%;">
                                    <td style="width: 100%; vertical-align: top;">
                                        <table style="width: 100%;" class="table">
                                            <tr runat="server" id="tr1">
                                                <td align="left" style="width: 30%">
                                                    <asp:Label ID="Label4" runat="server" Width="80px" Text="名称："></asp:Label>
                                                    <input id="txt_WorkName" style="width: 120px" type="text" runat="server" value="五班三运转" />
                                                </td>
                                                <td align="left" style="width: 30%">
                                                    <asp:Label ID="Label5" runat="server" Width="80px" Text="描述："></asp:Label>
                                                    <input id="txt_WorkDesc" style="width: 120px" type="text" runat="server" value="" />
                                                </td>
                                                <td align="left" style="width: 40%">
                                                    <asp:Label ID="Label6" runat="server" Width="80px" Text="备注："></asp:Label>
                                                    <input id="txt_WorkNote" style="width: 120px" type="text" runat="server" value="" />
                                                    <input id="txt_WorkID" type="hidden" runat="server" value="" />
                                                </td>
                                            </tr>
                                            <tr runat="server" id="tr2">
                                                <td align="left" style="width: 30%">
                                                    <asp:Label ID="Label2" runat="server" Width="80px" Text="开始时间："></asp:Label>
                                                    <input class="Wdate" id="txt_date" style="width: 120px" onfocus="WdatePicker({el:'txt_date',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"
                                                        type="text" runat="server" value="" />
                                                </td>
                                                <td align="left" style="width: 30%">
                                                    <asp:Label ID="Label1" runat="server" Width="80px" Text="基准值数: "></asp:Label>
                                                    <asp:DropDownList ID="ddl_BaseShifts" Width="120px" runat="server" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddl_BaseShifts_SelectedIndexChanged">
                                                        <asp:ListItem Value="1" Text="一班"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="两班"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="三班"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="四班"></asp:ListItem>
                                                        <asp:ListItem Value="5" Text="五班" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="6" Text="六班"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left" style="width: 40%">
                                                    <asp:Label ID="Label3" runat="server" Width="80px" Text="基准天数: "></asp:Label>
                                                    <asp:DropDownList ID="ddl_BaseDays" Width="120px" runat="server" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddl_BaseDays_SelectedIndexChanged">
                                                        <asp:ListItem Value="1" Text="1天"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="2天"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="3天"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="4天"></asp:ListItem>
                                                        <asp:ListItem Value="5" Text="5天" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="6" Text="6天"></asp:ListItem>
                                                        <asp:ListItem Value="7" Text="7天"></asp:ListItem>
                                                        <asp:ListItem Value="8" Text="8天"></asp:ListItem>
                                                        <asp:ListItem Value="9" Text="9天"></asp:ListItem>
                                                        <asp:ListItem Value="10" Text="10天"></asp:ListItem>
                                                        <asp:ListItem Value="90">90天</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="tr3">
                                                <td align="left" style="width: 30%">
                                                    <asp:Label ID="Label7" runat="server" Width="80px" Text="是否有效："></asp:Label>
                                                    <asp:DropDownList ID="ddl_WorkIsValid" runat="server">
                                                        <asp:ListItem Text="是" Value="1" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left" style="width: 30%">
                                                </td>
                                                <td align="left" style="width: 40%">
                                                    <span style="float: left">
                                                        <asp:Button ID="btnEditWork" Width="120px" runat="server" Text="修改当前倒班" OnClick="btnEditWork_Click" />
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="tr_date">
                                                <td align="center" colspan="3">
                                                    <span style="float: right">
                                                        <asp:Button ID="btnAddWork" Width="120px" runat="server" Text="另存为新倒班" OnClick="btnAddWork_Click" /></span>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width: 100%;" class="table">
                                            <tr runat="server" id="tr_day">
                                                <td align="left" style="width: 200px; height: 10px;">
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:Label ID="lbl_day00" runat="server" Width="40px" Text="1"></asp:Label>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:Label ID="lbl_day01" runat="server" Width="40px" Text="2"></asp:Label>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:Label ID="lbl_day02" runat="server" Width="40px" Text="3"></asp:Label>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:Label ID="lbl_day03" runat="server" Width="40px" Text="4"></asp:Label>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:Label ID="lbl_day04" runat="server" Width="40px" Text="5"></asp:Label>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:Label ID="lbl_day05" runat="server" Width="40px" Text="6"></asp:Label>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:Label ID="lbl_day06" runat="server" Width="40px" Text="7"></asp:Label>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:Label ID="lbl_day07" runat="server" Width="40px" Text="8"></asp:Label>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:Label ID="lbl_day08" runat="server" Width="40px" Text="9"></asp:Label>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:Label ID="lbl_day09" runat="server" Width="40px" Text="10"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="tr_00">
                                                <td align="center" style="width: 200px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_shift00" Width="100px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period00" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period01" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period02" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period03" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period04" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period05" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period06" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period07" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period08" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period09" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="tr_01">
                                                <td align="center" style="width: 200px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_shift01" Width="100px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period10" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period11" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period12" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period13" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period14" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period15" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period16" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period17" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period18" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period19" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="tr_02">
                                                <td align="center" style="width: 200px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_shift02" Width="100px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period20" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period21" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period22" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period23" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period24" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period25" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period26" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period27" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period28" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period29" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="tr_03">
                                                <td align="center" style="width: 200px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_shift03" Width="100px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period30" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period31" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period32" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period33" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period34" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period35" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period36" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period37" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period38" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period39" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="tr_04">
                                                <td align="center" style="width: 200px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_shift04" Width="100px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period40" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period41" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period42" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period43" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period44" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period45" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period46" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period47" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period48" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 70px; height: 10px;">
                                                    <asp:DropDownList ID="ddl_period49" Width="50px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tbody runat="server" id="tbody">
                                                <tr>
                                                    <td align="center" style="width: 200px; height: 10px;">
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        11
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        12
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        13
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        14
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        15
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        16
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        17
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        18
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        19
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        20
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_05">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period010" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period011" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period012" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period013" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period014" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period015" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period016" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period017" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period018" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period019" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_06">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period110" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period111" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period112" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period113" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period114" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period115" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period116" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period117" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period118" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period119" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_07">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period210" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period211" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period212" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period213" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period214" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period215" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period216" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period217" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period218" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period219" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_08">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period310" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period311" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period312" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period313" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period314" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period315" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period316" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period317" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period318" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period319" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_09">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period410" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period411" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period412" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period413" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period414" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period415" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period416" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period417" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period418" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period419" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width: 200px; height: 10px;">
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        21
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        22
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        23
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        24
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        25
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        26
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        27
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        28
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        29
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        30
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_10">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period020" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period021" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period022" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period023" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period024" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period025" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period026" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period027" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period028" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period029" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_11">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period120" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period121" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period122" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period123" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period124" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period125" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period126" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period127" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period128" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period129" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_12">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period220" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period221" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period222" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period223" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period224" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period225" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period226" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period227" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period228" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period229" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_13">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period320" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period321" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period322" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period323" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period324" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period325" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period326" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period327" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period328" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period329" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_14">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period420" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period421" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period422" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period423" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period424" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period425" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period426" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period427" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period428" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period429" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width: 200px; height: 10px;">
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        31
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        32
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        33
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        34
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        35
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        36
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        37
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        38
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        39
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        40
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_15">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period030" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period031" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period032" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period033" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period034" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period035" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period036" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period037" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period038" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period039" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_16">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period130" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period131" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period132" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period133" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period134" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period135" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period136" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period137" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period138" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period139" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_17">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period230" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period231" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period232" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period233" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period234" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period235" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period236" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period237" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period238" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period239" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_18">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period330" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period331" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period332" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period333" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period334" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period335" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period336" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period337" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period338" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period339" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_19">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period430" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period431" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period432" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period433" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period434" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period435" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period436" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period437" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period438" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period439" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width: 200px; height: 10px;">
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        41
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        42</td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        43
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        44
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        45
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        46
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        47
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        48
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        49
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        50
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_20">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period040" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period041" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period042" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period043" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period044" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period045" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period046" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period047" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period048" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period049" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_21">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period140" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period141" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period142" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period143" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period144" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period145" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period146" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period147" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period148" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period149" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_22">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period240" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period241" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period242" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period243" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period244" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period245" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period246" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period247" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period248" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period249" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_23">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period340" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period341" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period342" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period343" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period344" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period345" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period346" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period347" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period348" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period349" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_24">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period440" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period441" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period442" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period443" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period444" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period445" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period446" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period447" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period448" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period449" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width: 200px; height: 10px;">
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        51
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        52
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        53
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        54
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        55
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        56
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        57
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        58
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        59
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        60
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_25">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period050" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period051" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period052" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period053" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period054" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period055" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period056" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period057" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period058" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period059" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_26">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period150" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period151" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period152" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period153" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period154" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period155" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period156" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period157" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period158" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period159" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_27">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period250" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period251" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period252" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period253" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period254" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period255" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period256" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period257" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period258" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period259" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_28">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period350" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period351" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period352" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period353" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period354" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period355" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period356" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period357" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period358" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period359" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_29">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period450" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period451" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period452" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period453" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period454" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period455" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period456" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period457" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period458" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period459" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width: 200px; height: 10px;">
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        61
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        62</td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        63
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        64
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        65
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        66
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        67
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        68
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        69
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        70
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_30">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period060" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period061" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period062" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period063" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period064" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period065" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period066" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period067" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period068" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period069" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_31">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period160" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period161" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period162" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period163" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period164" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period165" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period166" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period167" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period168" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period169" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_32">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period260" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period261" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period262" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period263" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period264" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period265" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period266" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period267" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period268" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period269" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_33">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period360" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period361" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period362" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period363" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period364" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period365" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period366" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period367" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period368" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period369" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_34">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period460" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period461" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period462" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period463" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period464" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period465" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period466" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period467" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period468" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period469" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width: 200px; height: 10px;">
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        71
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        72</td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        73
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        74
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        75
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        76
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        77
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        78
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        79
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        80
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_35">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period070" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period071" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period072" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period073" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period074" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period075" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period076" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period077" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period078" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period079" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_36">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period170" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period171" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period172" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period173" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period174" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period175" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period176" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period177" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period178" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period179" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_37">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period270" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period271" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period272" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period273" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period274" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period275" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period276" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period277" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period278" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period279" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_38">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period370" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period371" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period372" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period373" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period374" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period375" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period376" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period377" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period378" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period379" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_39">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period470" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period471" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period472" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period473" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period474" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period475" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period476" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period477" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period478" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period479" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width: 200px; height: 10px;">
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        81
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        82</td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        83
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        84
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        85
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        86
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        87
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        88
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        89
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        90
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_40">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period080" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period081" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period082" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period083" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period084" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period085" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period086" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period087" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period088" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period089" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_41">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period180" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period181" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period182" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period183" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period184" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period185" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period186" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period187" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period188" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period189" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_42">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period280" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period281" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period282" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period283" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period284" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period285" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period286" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period287" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period288" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period289" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_43">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period380" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period381" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period382" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period383" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period384" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period385" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period386" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period387" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period388" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period389" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_44">
                                                    <td>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period480" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period481" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period482" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period483" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period484" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period485" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period486" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period487" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period488" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 70px; height: 10px;">
                                                        <asp:DropDownList ID="ddl_period489" Width="50px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 100%; margin: 0px;">
                    <asp:ObjectDataSource ID="GetShiftCodes" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetData" TypeName="SISKPI.KPIDataSetTableAdapters.GetShiftCodesTableAdapter">
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="GetPeriodCodes" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetData" TypeName="SISKPI.KPIDataSetTableAdapters.GetPeriodCodesTableAdapter"
                        InsertMethod="Insert">
                        <InsertParameters>
                            <asp:Parameter Name="Name" Type="String" />
                            <asp:Parameter Name="Code" Type="String" />
                        </InsertParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="GetYesNo" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetData" TypeName="SISKPI.KPIDataSetTableAdapters.GetYesNoTableAdapter">
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </div>
    <div id="Lay1" style="z-index: 100; visibility: hidden; left: 1%; width: 260px; cursor: crosshair;
        position: absolute; top: -20px; height: 14%; background-color: transparent; text-align: center">
        <br />
        <b></b>
        <table align="center" style="width: 260px; height: 45px" class="BoderTable">
            <tr>
                <td>
                    <div>
                        <font color="#800080" size="2" style="font-weight: bold">操作正在执行，请稍候...</font></div>
                    <div style="border-right: black 1px solid; padding-right: 2px; border-top: black 1px solid;
                        padding-left: 2px; font-size: 8pt; padding-bottom: 2px; border-left: black 1px solid;
                        padding-top: 2px; border-bottom: black 1px solid">
                        <span id="progress1">&nbsp;</span> <span id="progress2">&nbsp;</span> <span id="progress3">
                            &nbsp;</span> <span id="progress4">&nbsp;</span> <span id="progress5">&nbsp;</span>
                        <span id="progress6">&nbsp;</span> <span id="progress7">&nbsp;</span> <span id="progress8">
                            &nbsp;</span> <span id="progress9">&nbsp;</span> <span id="progress10">&nbsp;</span>
                        <span id="progress11">&nbsp;</span> <span id="progress12">&nbsp;</span> <span id="progress13">
                            &nbsp;</span> <span id="progress14">&nbsp;</span> <span id="progress15">&nbsp;</span>
                        <span id="progress16">&nbsp;</span> <span id="progress17">&nbsp;</span> <span id="progress18">
                            &nbsp;</span> <span id="progress19">&nbsp;</span> <span id="progress20">&nbsp;</span>
                        <span id="progress21">&nbsp;</span> <span id="progress22">&nbsp;</span> <span id="progress23">
                            &nbsp;</span> <span id="progress24">&nbsp;</span> <span id="progress25">&nbsp;</span>
                        <span id="progress26">&nbsp;</span> <span id="progress27">&nbsp;</span> <span id="progress28">
                            &nbsp;</span> <span id="progress29">&nbsp;</span> <span id="progress30">&nbsp;</span>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
