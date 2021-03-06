﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_RedoConfig.aspx.cs"
    Inherits="SISKPI.KPI_RedoConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISKPI Config</title>
    <base target="_self" />
    <link href="../CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <script src="../js/ProcessBar.js" type="text/javascript"></script>
    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../js/Common.js" type="text/javascript"></script>
    <script src="../js/DatePicker/WdatePicker.js" type="text/javascript" defer="defer"></script>
    <script src="../js/tab/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="../js/Tab/jquery.tabs.js" type="text/javascript"></script>
    <script src="../js/Config.js" type="text/javascript"></script>
    <script src="../js/GridViewColor.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%; text-align: center;">
    <div style="margin: 0px; width: 95%; text-align: center;">
        <table width="100%" align="center" class="table" id="table1">
            <tr style="width: 100%">
                <td valign="middle" align="center" style="width: 100%; height: 40px;">
                    <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                    <asp:Label ID="lblTitle" runat="server" Class="title" Text="历史数据重算管理"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%" class="table_tr">
                <td align="center" style="width: 100%">
                    <div align="center" style="width: 98%">
                        <fieldset class="field_info" style="width: 100%;">
                            <legend>历史重算列表</legend>
                            <table class="table" style="width: 98%;">
                                <tr align="left" style="width: 100%;">
                                    <td align="left" style="width: 100%;">
                                        <asp:RadioButtonList ID="rblRedo" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                            OnSelectedIndexChanged="rblRedo_SelectedIndexChanged">
                                            <asp:ListItem Text="只看本月" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="只看上月" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="查看全部" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr align="left" style="width: 100%;">
                                    <td align="left" style="width: 100%;">
                                        <asp:GridView ID="gvRD" CssClass="GridViewStyle" Width="100%" runat="server" AutoGenerateColumns="False"
                                            EmptyDataText="没有满足条件的数据" AllowPaging="False" OnRowDataBound="gvRD_RowDataBound"
                                            OnRowCommand="gvRD_RowCommand" OnRowCancelingEdit="gvRD_RowCancelingEdit" OnRowEditing="gvRD_RowEditing">
                                            <FooterStyle CssClass="GridViewFooterStyle" />
                                            <RowStyle CssClass="GridViewRowStyle" />
                                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                            <PagerStyle CssClass="GridViewPagerStyle" />
                                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <input type="hidden" runat="server" id="ecid" value='<%# Eval("RDID").ToString()%>' />
                                                        <%#  Container.DataItemIndex + 1%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="RDID" HeaderText="ID" Visible="false">
                                                    <ControlStyle Width="200px"></ControlStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="RDName" HeaderText="集合">
                                                    <ControlStyle Width="200px"></ControlStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="RDStartTime" HeaderText="开始时间">
                                                    <ControlStyle Width="100px"></ControlStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="RDEndTime" HeaderText="结束时间">
                                                    <ControlStyle Width="100px"></ControlStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="RDNote" HeaderText="备注">
                                                    <ControlStyle Width="160px"></ControlStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="是否有效" ItemStyle-HorizontalAlign="Center">
                                                    <ControlStyle Width="40px"></ControlStyle>
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlValid" runat="server" DataSourceID="GetYesNo" DataTextField="Name"
                                                            DataValueField="Value">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="是否采集" ItemStyle-HorizontalAlign="Center">
                                                    <ControlStyle Width="40px"></ControlStyle>
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlCollect" runat="server" DataSourceID="GetYesNo" DataTextField="Name"
                                                            DataValueField="Value">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="是否处理" ItemStyle-HorizontalAlign="Center">
                                                    <ControlStyle Width="40px"></ControlStyle>
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlCalced" runat="server" DataSourceID="GetYesNo" DataTextField="Name"
                                                            DataValueField="Value">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="RDCalcedTime" HeaderText="重算时间">
                                                    <ControlStyle Width="140px"></ControlStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="done" runat="server" Text="可执行" CommandName="dataDone" CommandArgument='<%# Eval("RDID") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="delete" runat="server" Text="删除" CommandName="dataDelete" CommandArgument='<%# Eval("RDID") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="100px" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr align="center" style="width: 100%;">
                                    <td align="center" style="width: 100%;">
                                        <asp:ObjectDataSource ID="GetYesNo" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="GetData" TypeName="SISKPI.KPIDataSetTableAdapters.GetYesNoTableAdapter">
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
            </tr>
            <tr style="width: 100%" class="table_tr">
                <td align="center" style="width: 100%">
                    <div align="left" style="width: 70%">
                        <fieldset class="field_info" style="width: 100%;">
                            <legend>历史重算配置</legend>
                            <table class="table" id="table2" style="width: 100%">
                                <tr style="width: 100%">
                                    <td align="left" style="width: 200px">
                                        选择集合：
                                    </td>
                                    <td align="left" style="width: 50%">
                                        <asp:RadioButtonList ID="rblRDType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                            OnSelectedIndexChanged="rblRDType_SelectedIndexChanged">
                                            <asp:ListItem Text="机组集" Value="0"></asp:ListItem>
                                            <%--<asp:ListItem Text="设备集" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="指标集" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="经济指标" Value="3"></asp:ListItem>--%>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="left" style="width: 20%">
                                        <asp:Button ID="btnAdd" runat="server" Text="添 加" Width="80px" OnClick="btnAdd_Click" />
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td align="left" style="width: 300px">
                                        <span style="float: left;">开始时间： </span>
                                    </td>
                                    <td align="left" style="width: 50%">
                                        <input class="Wdate" id="txt_ST" visible="true" style="width: 140px" onfocus="WdatePicker({el:'txt_ST',dateFmt:'yyyy-MM-dd HH:mm:00',skin:'whyGreen'})"
                                            type="text" runat="server" value="" />
                                    </td>
                                    <td align="left" style="width: 20%">
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td align="left" style="width: 300px">
                                        结束时间：
                                    </td>
                                    <td align="left" style="width: 50%">
                                        <input class="Wdate" id="txt_ET" visible="true" style="width: 140px" onfocus="WdatePicker({el:'txt_ET',dateFmt:'yyyy-MM-dd HH:mm:00',skin:'whyGreen'})"
                                            type="text" runat="server" value="" />
                                    </td>
                                    <td align="left" style="width: 20%">
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td align="left" style="width: 300px">
                                        是否重新采集实时数据：
                                    </td>
                                    <td align="left" style="width: 50%">
                                        <asp:DropDownList ID="ddlRDIsCollect" runat="server">
                                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="否" Value="0" Selected="true"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" style="width: 20%">
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td align="left" style="width: 300px">
                                        备注：
                                    </td>
                                    <td align="left" style="width: 50%">
                                        <asp:TextBox ID="tbxRDNote" runat="server" Width="260px"></asp:TextBox>
                                    </td>
                                    <td align="left" style="width: 20%">
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td align="left" style="width: 300px">
                                        选择集合：
                                    </td>
                                    <td align="left" style="width: 40%">
                                        <asp:CheckBoxList ID="cbxRDID" Width="300px" Height="200px" runat="server">
                                        </asp:CheckBoxList>
                                    </td>
                                    <td align="left" style="width: 20%">
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="Lay1" style="z-index: 100; visibility: hidden; left: 1%; width: 260px; cursor: crosshair;
        position: absolute; top: -20px; height: 14%; background-color: transparent; text-align: center">
        <br />
        <b></b>
        <table align="center" style="width: 260px; height: 45px" class="table">
            <tr>
                <td>
                    <div>
                        <font color="#800080" size="2" style="font-weight: bold">配置正在执行，请稍候...</font></div>
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
