<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_RDLCConfig.aspx.cs"
    Inherits="SISKPI.KPI_RDLCConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISOPM</title>
    <link href="../CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../js/Common.js" type="text/javascript"></script>
    <%--    <script src="../js/tab/jquery.history_remote.pack.js" type="text/javascript"></script>--%>
    <%--    <script src="../js/Tab/jquery.tabs.js" type="text/javascript"></script>--%>
    <%--    <script src="../js/Config.js" type="text/javascript"></script>--%>
    <script src="../js/GridViewColor.js" type="text/javascript"></script>
    <script src="../js/ProcessBar.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            call();
        });

        function call() {
            SetTableWidth('table1');
            SetDivWidth('divtag');
            SetDivWidth('div1');
            SetTableWidth('table2');
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 0px; width: 100%; text-align: center;">
        <table id="table1" class="table" style="width: 100%; text-align: center;">
            <tr style="width: 100%" class="table_tr">
                <td align="center" style="width: 100%">
                    <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                    <asp:Label ID="lblTitle" runat="server" Class="title" Text="报表指标配置"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%" class="table_tr">
                <td style="width: 100%" align="center">
                    <div style="width: 95%" align="right">
                        <table class="table" style="width: 100%">
                            <tr style="width: 100%; text-align: center;" class="table_tr">
                                <td style="width: 100%;" align="center">
                                    <asp:GridView ID="gvWeb" CssClass="GridViewStyle" Width="100%" runat="server" AutoGenerateColumns="False"
                                        EmptyDataText="没有满足条件的数据" AllowPaging="false" OnRowDataBound="gvWeb_RowDataBound"
                                        OnRowCommand="gvWeb_RowCommand" OnRowCancelingEdit="gvWeb_RowCancelingEdit" OnRowEditing="gvWeb_RowEditing"
                                        OnRowUpdating="gvWeb_RowUpdating">
                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                        <RowStyle CssClass="GridViewRowStyle" />
                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                        <PagerStyle CssClass="GridViewPagerStyle" />
                                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号">
                                                <ItemTemplate>
                                                    <input type="hidden" runat="server" id="webid" value='<%# Eval("WebID").ToString()%>' />
                                                    <%#  Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="WebCode" HeaderText="代码">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="WebDesc" HeaderText="描述">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="类型" ItemStyle-HorizontalAlign="Center">
                                                <ControlStyle Width="80px"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlType" runat="server" DataSourceID="GetRDLCType" DataTextField="Name"
                                                        DataValueField="Value">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="WebNote" HeaderText="备注">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="配置">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="select" runat="server" Text="选择" CommandName="dataSelect" CommandArgument='<%# Eval("WebID") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:CommandField HeaderText="配置" ShowEditButton="True" HeaderStyle-Width="60px"
                                                ItemStyle-HorizontalAlign="Center" EditText="编辑" />
                                            <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="delete" runat="server" Text="删除" CommandName="dataDelete" CommandArgument='<%# Eval("WebID") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Width="60px" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td style="width: 100%" align="center">
                                    <%--<asp:Label ID="lblUnit" runat="server" Text="选择集合:  "></asp:Label>
                                    <select style="width: 120px" runat="server" id="ddl_WebID">
                                    </select>
                                    <asp:Button ID="btnQuery" Width="80px" runat="server" Text="查 询" OnClick="btnQuery_Click" />
                                    --%>
                                    <asp:Button ID="btnWeb" Width="120px" runat="server" Text="新增集合" OnClick="btnAddWeb_Click" />
                                    <asp:ObjectDataSource ID="GetRDLCType" runat="server" OldValuesParameterFormatString="original_{0}"
                                        SelectMethod="GetData" 
                                        TypeName="SISKPI.KPIDataSetTableAdapters.GetRDLCTypeTableAdapter">
                                    </asp:ObjectDataSource>
                                    <asp:ObjectDataSource ID="GetRDLCCalc" runat="server" OldValuesParameterFormatString="original_{0}"
                                        SelectMethod="GetData" 
                                        TypeName="SISKPI.KPIDataSetTableAdapters.GetRDLCCalcTableAdapter">
                                    </asp:ObjectDataSource>
                                    <asp:ObjectDataSource ID="GetYesNo" runat="server" OldValuesParameterFormatString="original_{0}"
                                        SelectMethod="GetData" TypeName="SISKPI.KPIDataSetTableAdapters.GetYesNoTableAdapter">
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr style="width: 100%; text-align: center;" class="table_tr">
                <td style="width: 100%" align="center">
                    <div style="width: 95%" align="right">
                        <table style="width: 100%" class="table">
                            <tr style="width: 100%">
                                <td style="width: 50%" align="left">
                                    <asp:Label ID="Label1" Width="200px" Font-Bold="true" runat="server" Text="当前选定的集合：" />
                                    <asp:Label ID="lblInfor" Width="200px" ForeColor="Red" Font-Bold="true" runat="server"
                                        Text="XXX" />
                                </td>
                                <td style="width: 50%" align="right">
                                    <asp:Button ID="btnSync" Width="80px" runat="server" Text="同步参数" OnClick="btnSync_Click" />
                                    <asp:Button ID="btnAdd" Width="80px" runat="server" Text="新增参数" OnClick="btnAdd_Click" />
                                    <asp:Button ID="btnSort" Width="80px" runat="server" Text="参数排序" OnClick="btnSort_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr style="width: 100%; text-align: center;" class="table_tr">
                <td style="width: 100%;" align="center">
                    <asp:GridView ID="gvKey" CssClass="GridViewStyle" Width="95%" runat="server" AutoGenerateColumns="False"
                        EmptyDataText="没有满足条件的数据" AllowPaging="false" OnRowDataBound="gvKey_RowDataBound"
                        OnRowCommand="gvKey_RowCommand" 
                        onrowcancelingedit="gvKey_RowCancelingEdit" onrowediting="gvKey_RowEditing" 
                        onrowupdating="gvKey_RowUpdating">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <input type="hidden" runat="server" id="keyid" value='<%# Eval("KeyID").ToString()%>' />
                                    <%#  Container.DataItemIndex + 1%>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ECCode" HeaderText="指标代码" ReadOnly="true">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ECName" HeaderText="指标名称" ReadOnly="true">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="WebCode" HeaderText="集合名称" ReadOnly="true">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="KeyEngunit" HeaderText="指标单位" ReadOnly="true">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="计算类型" ItemStyle-HorizontalAlign="Center">
                                <ControlStyle Width="80px"></ControlStyle>
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlCalc" runat="server" DataSourceID="GetRDLCCalc" DataTextField="Name"
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
                            <asp:CommandField HeaderText="配置" ShowEditButton="True" HeaderStyle-Width="60px"
                                ItemStyle-HorizontalAlign="Center" EditText="编辑" />
                            <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lb_delete" runat="server" Text="删除" CommandName="dataDelete"
                                        CommandArgument='<%# Eval("KeyID") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="60px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <div id="Lay1" style="z-index: 100; visibility: hidden; left: 1%; width: 260px; cursor: crosshair;
        position: absolute; top: -20px; height: 14%; background-color: transparent; text-align: center">
        <br />
        <b></b>
        <table align="center" style="width: 260px; height: 40px" class="BoderTable">
            <tr>
                <td>
                    <div>
                        <font color="green" size="2" style="font-weight: bold">正在查询数据库，请稍候...</font></div>
                    <div style="border-right: green 1px solid; padding-right: 2px; border-top: green 1px solid;
                        padding-left: 2px; font-size: 8pt; padding-bottom: 2px; border-left: green 1px solid;
                        padding-top: 2px; border-bottom: green 1px solid">
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
