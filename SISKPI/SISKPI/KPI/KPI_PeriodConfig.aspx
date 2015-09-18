<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_PeriodConfig.aspx.cs"
    Inherits="SISKPI.KPI_PeriodConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISKPI</title>
    <link href="../CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <script src="../js/ProcessBar.js" type="text/javascript"></script>
    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../js/Common.js" type="text/javascript"></script>
    <script src="../js/tab/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="../js/Tab/jquery.tabs.js" type="text/javascript"></script>
    <script src="../js/Config.js" type="text/javascript"></script>
    <script src="../js/GridViewColor.js" type="text/javascript"></script>
    <script type="text/javascript">
        
    </script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%; text-align: center;">
    <div style="margin: 0px; width: 100%; text-align: center;">
        <table width="100%" align="left" class="table" id="table1">
            <tr style="width: 100%">
                <td align="center" style="width: 100%">
                    <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                    <asp:Label ID="lblTitle" runat="server" Class="title" Text="班配置"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%">
                <td align="center" style="width: 100%">
                    <div style="width: 100%">
                        <fieldset class="field_info" style="width: 95%;">
                            <legend>班信息</legend>
                            <table class="table" style="width: 98%;">
                                <tr align="left" style="width: 100%;">
                                    <td align="left" style="width: 100%;">
                                        <asp:Label ID="Label1" runat="server" Text="注：班的数量不能超过26个."></asp:Label>
                                    </td>
                                </tr>
                                <tr align="left" style="width: 100%;">
                                    <td align="left" style="width: 100%;">
                                        <asp:GridView ID="gvPeriod" CssClass="GridViewStyle" Width="100%" runat="server"
                                            AutoGenerateColumns="False" EmptyDataText="没有满足条件的数据" AllowPaging="False" OnRowDataBound="gvPeriod_RowDataBound"
                                            OnRowCommand="gvPeriod_RowCommand" OnRowCancelingEdit="gvPeriod_RowCancelingEdit"
                                            OnRowEditing="gvPeriod_RowEditing" OnRowUpdating="gvPeriod_RowUpdating">
                                            <FooterStyle CssClass="GridViewFooterStyle" />
                                            <RowStyle CssClass="GridViewRowStyle" />
                                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                            <PagerStyle CssClass="GridViewPagerStyle" />
                                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <input type="hidden" runat="server" id="periodid" value='<%# Eval("PeriodID").ToString()%>' />
                                                        <%#  Container.DataItemIndex + 1%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PeriodCode" HeaderText="代码" ReadOnly>
                                                    <ControlStyle Width="100px"></ControlStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PeriodName" HeaderText="名称">
                                                    <ControlStyle Width="100px"></ControlStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PeriodDesc" HeaderText="描述">
                                                    <ControlStyle Width="100px"></ControlStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PeriodStartHour" HeaderText="开始时间">
                                                    <ControlStyle Width="60px"></ControlStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PeriodEndHour" HeaderText="结束时间">
                                                    <ControlStyle Width="60px"></ControlStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PeriodHours" HeaderText="时长[小时]">
                                                    <ControlStyle Width="60px"></ControlStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="是否IDL" ItemStyle-HorizontalAlign="Center">
                                                    <ControlStyle Width="40px"></ControlStyle>
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlIDL" runat="server" DataSourceID="GetYesNo" DataTextField="Name"
                                                            DataValueField="Value">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="是否有效" ItemStyle-HorizontalAlign="Center">
                                                    <ControlStyle Width="40px"></ControlStyle>
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlValid" runat="server" DataSourceID="GetYesNo" DataTextField="Name"
                                                            DataValueField="Value">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PeriodNote" HeaderText="备注">
                                                    <ControlStyle Width="100px"></ControlStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:CommandField HeaderText="配置" ShowEditButton="True" HeaderStyle-Width="60px"
                                                    ItemStyle-HorizontalAlign="Center" EditText="编辑" />
                                                <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="delete" runat="server" Text="删除" CommandName="dataDelete" CommandArgument='<%# Eval("PeriodID") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="60px" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr align="center" style="width: 100%;">
                                    <td align="center" style="width: 100%;">
                                        <asp:Button ID="btnAddPeriod" Width="160px" runat="server" Text="新增班信息" OnClick="btnAddPeriod_Click" />
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
