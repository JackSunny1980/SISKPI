<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_System.aspx.cs" Inherits="SISKPI.KPI_System" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISKPI System Config</title>
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

        //js 只能输入数字和小数
        function clearNoNum(obj) {

            //先把非数字的都替换掉，除了数字和.
            obj.value = obj.value.replace(/[^\d.]/g, "");
            //必须保证第一个为数字而不是.
            obj.value = obj.value.replace(/^\./g, "");
            //保证只有出现一个.而没有多个.
            obj.value = obj.value.replace(/\.{2,}/g, ".");
            //保证.只出现一次，而不能出现两次以上
            obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");

        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 0px; width: 90%; text-align: left;">
        <table align="left" class="table" id="table1" width="100%">
            <tr style="width: 100%">
                <td align="center" style="width: 100%">
                    <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                    <asp:Label ID="Label2" runat="server" Class="title" Text="系统数据管理"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%">
                <td align="center" width="100%">
                    <fieldset class="field_info" style="width: 90%;">
                        <legend>系统信息管理</legend>
                        <table class="table" width="100%">
                            <tr style="width: 100%">
                                <td align="center" width="100%">
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td align="left" width="100%">
                                    <span style="float: left">
                                        <asp:Label ID="Label1" runat="server" Text="交接运行方式:"></asp:Label>
                                    </span><span style="float: left">
                                        <asp:RadioButtonList ID="rbnKPIAuto" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="rbnKPIAuto_SelectedIndexChanged">
                                            <asp:ListItem Text="自动" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="手动" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </span>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td align="left" width="100%">
                                    <span style="float: left">
                                        <asp:Label ID="Label6" runat="server" Text="系统时间定位方式:"></asp:Label>
                                    </span><span style="float: left">
                                        <asp:RadioButtonList ID="rbnKPITimeMode" runat="server" AutoPostBack="true" 
                                        RepeatDirection="Horizontal" 
                                        onselectedindexchanged="rbnKPITimeMode_SelectedIndexChanged">
                                            <asp:ListItem Text="本地系统时间" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="数据库服务器时间" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </span>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td align="left" width="100%">
                                    <span style="float: left">
                                        <asp:Label ID="Label3" runat="server" Text="系统偏置时间(秒):"></asp:Label>
                                    </span><span style="float: left">
                                        <asp:TextBox ID="tbxKPIOffset" runat="server" Text="0"></asp:TextBox>
                                        <asp:Button ID="btnUpdateOffset" runat="server" Width="80px" Text="保 存" OnClick="btnUpdateOffset_Click" />
                                    </span>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td align="left" width="100%">
                                    <span style="float: left">
                                        <asp:Label ID="Label5" runat="server" Text="登录显示路径:"></asp:Label>
                                    </span><span style="float: left">
                                        <asp:TextBox ID="tbxKPIFirtURL" runat="server" Width="300px" Text="..\KPIWeb\KPI_ECValue.aspx?eccode=sh03"></asp:TextBox>
                                        <asp:Button ID="btnUpdateKPIFirstURL" runat="server" Width="80px" 
                                        Text="保 存" onclick="btnUpdateKPIFirstURL_Click" />
                                    </span>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td align="center" width="100%">
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td align="center" width="100%">
                                    <span style="float: left">
                                        <asp:Label ID="Label4" runat="server" Text="是否重启服务:"></asp:Label>
                                    </span><span style="float: left">
                                        <asp:RadioButtonList ID="rbnKPIReload" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="rbnKPIReload_SelectedIndexChanged">
                                            <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="否" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </span>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td align="center" width="100%">
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td style="width: 100%">
                                    <asp:GridView ID="gvSys" CssClass="GridViewStyle" Width="100%" runat="server" AutoGenerateColumns="False"
                                        EmptyDataText="没有满足条件的数据" OnRowCancelingEdit="gvSys_RowCancelingEdit" OnRowEditing="gvSys_RowEditing"
                                        OnRowUpdating="gvSys_RowUpdating" OnRowDataBound="gvSys_RowDataBound">
                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                        <RowStyle CssClass="GridViewRowStyle" />
                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                        <PagerStyle CssClass="GridViewPagerStyle" />
                                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="keyid" Text='<%# Eval("SysID").ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SysName" HeaderText="名称" ReadOnly="true" />
                                            <asp:BoundField DataField="SysDesc" HeaderText="描述" ReadOnly="true" />
                                            <asp:BoundField DataField="SysIsValid" HeaderText="有效性" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SysValue" HeaderText="设定值" />
                                            <asp:BoundField DataField="SysNote" HeaderText="备注" />
                                            <asp:CommandField HeaderText="配置" Visible="false" ItemStyle-HorizontalAlign="Center"
                                                ShowEditButton="True" EditText=" 编辑 " ControlStyle-Width="60px" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
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
                        <font color="#800080" size="2" style="font-weight: bold">配置正在执行，请稍候...
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
