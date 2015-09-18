<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_Group.aspx.cs" Inherits="SISKPI.KPI_Group" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISOPM</title>
    <link href="../CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <base target="_self" />
    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../js/ProcessBar.js" type="text/javascript"></script>
    <script src="../js/Common.js" type="text/javascript"></script>
    <script src="../js/DatePicker/WdatePicker.js" type="text/javascript" defer="defer"></script>
    <script src="../js/GridViewColor.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            call();
        });

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
    <div style="width: 100%; text-align: center;">
        <table style="width: 100%" align="center" class="table" id="table1">
            <tr style="width: 100%">
                <td align="center" style="width: 100%">
                    <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                    <asp:Label ID="lblTitle" runat="server" Class="title" Text="SIS系统管理"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%">
                <td valign="middle" align="center" style="width: 100%; height: 40px;">
                    <div  align="center" style="width: 95%">
                        <table class="table" style="width: 100%;">
                            <tr align="left" style="width: 100%;">
                                <td align="left" style="width: 20%;">
                                    <asp:Button ID="btnMenu" Width="100px" runat="server" Text="菜单管理" OnClick="btnMenu_Click" />
                                </td>
                                <td align="left" style="width: 20%;">
                                    <asp:Button ID="btnUser" Width="100px" runat="server" Text="用户管理" OnClick="btnUser_Click" />
                                </td>
                                <td align="left" style="width: 20%;">
                                    <asp:Button ID="btnGroup" Width="100px" runat="server" Text="权限管理" OnClick="btnGroup_Click" />
                                </td>
                                <td align="left" style="width: 100%;">
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 100%" align="center">
                    <table class="table" id="table3" style="width: 95%">
                        <tr style="width: 100%">
                            <td style="width: 100%" align="center">
                                <span style="float: left">
                                    <%--<asp:Button ID="btnSISAdmin" runat="server" Width="100px" Text="初始化" 
                                    onclick="btnSISAdmin_Click" />--%>
                                </span><span style="float: right">
                                    <asp:Label ID="Label1" runat="server" Width="80px" Text="组名称:"></asp:Label>
                                    <asp:TextBox ID="txt_GroupName" runat="server" Width="100px"></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" Width="100px" Text="查 询" OnClick="btnSearch_Click" />
                                </span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 100%" align="center">
                    <asp:GridView ID="gvGroup" CssClass="GridViewStyle" Width="95%" runat="server" AutoGenerateColumns="False"
                        EmptyDataText="没有满足条件的数据" AllowPaging="True" OnRowCancelingEdit="gvGroup_RowCancelingEdit"
                        OnRowEditing="gvGroup_RowEditing" OnRowUpdating="gvGroup_RowUpdating" OnRowDataBound="gvGroup_RowDataBound"
                        OnRowCommand="gvGroup_RowCommand" OnPageIndexChanging="gvGroup_PageIndexChanging"
                        PageSize="26">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <input type="hidden" runat="server" id="groupid" value='<%# Eval("GroupID").ToString()%>' />
                                    <%#  Container.DataItemIndex + 1%>
                                </ItemTemplate>
                                <HeaderStyle Width="40px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="GroupCode" HeaderText="编码" ControlStyle-Width="50px" ReadOnly="true"
                                ControlStyle-Height="20px" />
                            <asp:BoundField DataField="GroupName" HeaderText="名称" ControlStyle-Width="50px" />
                            <asp:BoundField DataField="GroupDesc" HeaderText="描述" ControlStyle-Width="50px" />
                            <asp:BoundField DataField="GroupIsValid" HeaderText="有效性" ControlStyle-Width="50px" />
                            <asp:BoundField DataField="GroupNote" HeaderText="备注" ControlStyle-Width="50px" />
                            <asp:CommandField HeaderText="操作" ItemStyle-HorizontalAlign="Center" ShowEditButton="True"
                                EditText="修改" />
                            <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lb_delete" runat="server" Text="删除" CommandName="dataDelete"
                                        CommandArgument='<%# Eval("GroupID") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="60px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 100%" align="center">
                    <table class="table" id="table2" style="width: 95%">
                        <tr style="width: 100%">
                            <td width="120px" align="left">
                                <asp:Label ID="Label3" runat="server" Text="名称"></asp:Label>
                            </td>
                            <td width="120px" align="left">
                                <asp:TextBox ID="tbx_GroupName" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td width="120px" align="left">
                                <asp:Label ID="Label4" runat="server" Text="描述"></asp:Label>
                            </td>
                            <td width="120px" align="left">
                                <asp:TextBox ID="tbx_GroupDesc" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td width="120px" align="left">
                                <asp:Label ID="Label5" runat="server" Text="有效性"></asp:Label>
                            </td>
                            <td width="120px" align="left">
                                <select style="width: 100px" runat="server" id="ddl_GroupIsValid">
                                    <option value="1" selected>有效</option>
                                    <option value="0">无效</option>
                                </select>
                            </td>
                            <td width="120px" align="left">
                                <asp:Label ID="Label7" runat="server" Text="备注"></asp:Label>
                            </td>
                            <td width="120px" align="left">
                                <asp:TextBox ID="tbx_GroupNote" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td width="120px" align="left">
                                <asp:Button ID="btnAdd" runat="server" Text="添 加" Width="100px" OnClick="btnAdd_Click" />
                            </td>
                        </tr>
                    </table>
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
                        <span id="progress31">&nbsp;</span> <span id="progress32">&nbsp;</span> <span id="progress33">
                            &nbsp;</span> <span id="progress34">&nbsp;</span> <span id="progress35">&nbsp;</span>
                        <span id="progress36">&nbsp;</span> <span id="progress37">&nbsp;</span> <span id="progress38">
                            &nbsp;</span> <span id="progress39">&nbsp;</span> <span id="progress40">&nbsp;</span>
                        <span id="progress41">&nbsp;</span> <span id="progress42">&nbsp;</span> <span id="progress43">
                            &nbsp;</span> <span id="progress44">&nbsp;</span> <span id="progress45">&nbsp;</span>
                        <span id="progress46">&nbsp;</span> <span id="progress47">&nbsp;</span> <span id="progress48">
                            &nbsp;</span> <span id="progress49">&nbsp;</span> <span id="progress50">&nbsp;</span>
                        <span id="progress51">&nbsp;</span> <span id="progress52">&nbsp;</span> <span id="progress53">
                            &nbsp;</span> <span id="progress54">&nbsp;</span> <span id="progress55">&nbsp;</span>
                        <span id="progress56">&nbsp;</span> <span id="progress57">&nbsp;</span> <span id="progress58">
                            &nbsp;</span> <span id="progress59">&nbsp;</span> <span id="progress60">&nbsp;</span>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
