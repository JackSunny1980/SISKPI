<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_SubUserPassword.aspx.cs"
    Inherits="SISKPI.KPI_SubUserPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SIS用户信息管理</title>
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
                    <asp:Label ID="Label2" runat="server" Class="title" Text="SIS用户信息管理"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%">
                <td align="center" style="width: 100%">
                    <table style="width: 95%" class="table" >
                        <tr style="width: 100%">
                            <td style="width: 30%;">
                                用户代码：
                            </td>
                            <td style="width: 50%;" align="left">
                                <asp:TextBox ID="tbxUserCode" Width="140px" runat="server" Enabled="false"></asp:TextBox>
                                <asp:Label ID="Label1" runat="server" Font-Size="16px" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                        </tr>
                        <tr style="width: 100%">
                            <td style="width: 30%;">
                                用户名称：
                            </td>
                            <td style="width: 50%;" align="left">
                                <asp:TextBox ID="tbxUserName" Width="140px" runat="server"></asp:TextBox>
                                <asp:Label ID="Label3" runat="server" Font-Size="16px" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                        </tr>
                        <tr style="width: 100%" id="trold" runat="server">
                            <td style="width: 30%;">
                                旧密码：
                            </td>
                            <td style="width: 50%;" align="left">
                                <asp:TextBox ID="tbxUserPassword" Width="140px" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:Label ID="Label4" runat="server" Font-Size="16px" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                        </tr>
                        <tr style="width: 100%">
                            <td style="width: 30%;">
                                新密码：
                            </td>
                            <td style="width: 50%;" align="left">
                                <asp:TextBox ID="tbxUserP1" Width="140px" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:Label ID="Label5" runat="server" Font-Size="16px" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                        </tr>
                        <tr style="width: 100%">
                            <td style="width: 30%;">
                                确认新密码：
                            </td>
                            <td style="width: 50%;" align="left">
                                <asp:TextBox ID="tbxUserP2" Width="140px" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:Label ID="Label6" runat="server" Font-Size="16px" ForeColor="Red" Text="*"></asp:Label>
                                <asp:CompareValidator ID="valid1" runat="server" ControlToValidate="tbxUserP1" ControlToCompare="tbxUserP2"
                                    Type="String" Operator="equal" Display="dynamic" SetFocus>两次密码不一致</asp:CompareValidator>
                            </td>
                        </tr>
                        <tr style="width: 100%">
                            <td style="width: 30%;">
                                EMail地址：
                            </td>
                            <td style="width: 50%;" align="left">
                                <asp:TextBox ID="tbxUserEMail" Width="140px" runat="server"></asp:TextBox>
                                <asp:Label ID="Label7" runat="server" Font-Size="16px" ForeColor="Red" Text="*"></asp:Label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="请输入合法的EMail地址"
                                    ControlToValidate="tbxUserEMail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                            </td>
                        </tr>
                        <tr style="width: 100%">
                            <td style="width: 30%;">
                                移动电话：
                            </td>
                            <td style="width: 50%;" align="left">
                                <asp:TextBox ID="tbxUserPhone" Width="140px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="width: 100%">
                            <td style="width: 30%;">
                                岗位名称：
                            </td>
                            <td style="width: 50%;" align="left">
                                <asp:TextBox ID="tbxUserTitle" Width="140px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="width: 100%">
                            <td style="width: 30%;">
                            </td>
                            <td style="width: 50%;" align="center">
                                <span style="float: left">
                                    <asp:Button ID="btnConfirm" runat="server" Width="100px" Text="保 存" OnClick="btnConfirm_Click" />
                                </span>
                            </td>
                        </tr>
                        <tr style="width: 100%">
                            <td style="width: 30%;">
                            </td>
                            <td style="width: 50%;" align="center">
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
