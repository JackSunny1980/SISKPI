<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_User.aspx.cs" Inherits="SISKPI.KPI_User" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>KPI_User Config</title>
    <link href="../CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/DatePicker/WdatePicker.js"
        defer="defer"></script>
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

        function call() {
            SetTableWidth('table1');
            SetDivWidth('div1');
            SetDivWidth('divtag');
            SetTableWidth('table2');
            //SetTableWidth('table3');
        }


        function UserUpdate(userid) {
            var iWidth = 600; //模态窗口宽度
            var iHeight = 500; //模态窗口高度
            var iTop = (window.screen.height - iHeight) / 2;
            var iLeft = (window.screen.width - iWidth) / 2;

            //更新，keyid="........"
            window.open("KPI_SubUserConfig.aspx?opr=edit&userid=" + userid, "newwindow", "Width=" + iWidth + " ,Height=" + iHeight + ",top=" + iTop + ",left=" + iLeft);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 0px; width: 100%; text-align: left;">
        <table align="left" class="table" id="table1" width="100%">
            <tr style="width: 100%">
                <td align="center" style="width: 100%">
                    <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                    <asp:Label ID="Label2" runat="server" Class="title" Text="SIS系统管理"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%">
                <td valign="middle" align="center" style="width: 100%; height: 40px;">
                    <div align="center" style="width: 95%">
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
                <td align="center" width="100%">
                    <div style="width: 95%">
                        <table class="table" width="100%">
                            <tr style="width: 100%">
                                <td style="width: 20%;" align="right" valign="middle">
                                    <span style="float: left">
                                        <asp:Button ID="btnAddUser" runat="server" Width="100px" Text="新增用户" OnClick="btnAddUser_Click" />
                                        <%--<asp:Button ID="btnSISAdmin" runat="server" Width="100px" Text="初始化" OnClick="btnSISAdmin_Click" />--%>
                                    </span>
                                </td>
                                <td style="width: 20%;" align="right" valign="middle">
                                    <span style="float: left">
                                        <asp:Button ID="btnAddGroup" runat="server" Width="100px" Text="角色授权" OnClick="btnAddGroup_Click" />
                                    </span>
                                </td>
                                <td style="width: 20%;" align="right" valign="middle">
                                    <span style="float: left">
                                    </span>
                                </td>
                                <td style="width: 50%;" align="right" valign="middle">
                                    <span style="float: right">
                                        <asp:Label ID="Label1" runat="server" Width="80px" Text="姓名:"></asp:Label>
                                        <asp:TextBox ID="tbx_UserName" Width="100px" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnQuery" runat="server" Width="100px" Text="查 询" OnClick="btnQuery_Click" />
                                    </span>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr style="width: 100%">
                <td align="center" width="100%">
                    <div style="width: 95%">
                        <table class="table" width="100%">
                            <tr style="width: 100%">
                                <td style="width: 100%">
                                    <%-- <fieldset class="field_info" style="width: 95%;">
                                    <legend>SIS系统用户</legend>
                                </fieldset>--%>
                                    <asp:GridView ID="gvUser" CssClass="GridViewStyle" Width="100%" AllowPaging="True"
                                        runat="server" AutoGenerateColumns="False" PageSize="20" EmptyDataText="没有满足条件的数据"
                                        OnRowDataBound="gvUser_RowDataBound" OnRowCommand="gvUser_RowCommand">
                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                        <RowStyle CssClass="GridViewRowStyle" />
                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                        <PagerStyle CssClass="GridViewPagerStyle" />
                                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号">
                                                <ItemTemplate>
                                                    <input type="hidden" runat="server" id="userid" value='<%# Eval("UserID").ToString()%>' />
                                                    <%#  Container.DataItemIndex + 1%>
                                                    <input type="hidden" runat="server" id="userisvalid" value='<%# Eval("UserIsValid").ToString()%>' />
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="UserCode" HeaderText="用户代码" />
                                            <asp:BoundField DataField="UserName" HeaderText="用户名称" />
                                            <asp:BoundField DataField="UserEMail" HeaderText="用户EMail" />
                                            <asp:BoundField DataField="UserPhone" HeaderText="用户电话" />
                                            <asp:BoundField DataField="UserTitle" HeaderText="用户岗位" />
                                            <asp:TemplateField HeaderText="有效性">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="lb_valid" runat="server" Height="20px" Width="20px" ImageUrl="../imgs/valid.jpg"
                                                        CommandName="validUpdate" CommandArgument='<%# Eval("UserID") + "$"+ Eval("UserIsValid") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="password" runat="server" Text="重置密码" CommandName="passwordUpdate"
                                                        CommandArgument='<%# Eval("UserID") + "$"+ "123456" %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Width="60px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <input class="GridViewButtonStyle" type="button" runat="server" id="userupdate" value='修改信息' />
                                                </ItemTemplate>
                                                <HeaderStyle Width="60px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_delete" runat="server" Text="删除用户" CommandName="dataDelete"
                                                        CommandArgument='<%# Eval("UserID")+ "$"+ "123456" %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Width="60px" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr style="width: 100%">
                <td align="center" width="100%">
                    <div style="width: 95%">
                        <table class="table" width="100%">
                            <tr style="width: 100%">
                                <td style="width: 50%;" align="left">
                                    <%-- <span style="float: left"></span><span style="float: inherit"></span>--%>
                                    <asp:Label ID="Label3" runat="server" Width="60px" Text="选择文件:"></asp:Label>
                                    <asp:FileUpload ID="fuExcel" runat="server" Width="200px" />
                                </td>
                                <td style="width: 50%;" align="right">
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td style="width: 50%;" align="left">
                                    <%-- <span style="float: left"></span><span style="float: inherit"></span>--%>
                                    <asp:Label ID="Label4" runat="server" Width="60px" Text="输入表单:"></asp:Label>
                                    <asp:TextBox ID="tbxSheet" runat="server" Text="Sheet1"></asp:TextBox>
                                    <asp:Button ID="btnImport" runat="server" Width="100px" Text="导入用户" OnClick="btnImport_Click" />
                                </td>
                                <td style="width: 50%;" align="left">
                                    <asp:Button ID="btnExport" runat="server" Width="100px" Text="导出用户" OnClick="btnExport_Click" />
                                </td>
                            </tr>
                        </table>
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
