<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_SubUserGroup.aspx.cs"
    Inherits="SISKPI.KPI_SubUserGroup" %>

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
                <td align="center" style="width: 100%" class="table">
                    <table style="width: 95%">
                        <tr style="width: 100%">
                            <td align="left" style="width: 40%;" valign="middle">
                                <span style="float: left; vertical-align: middle;">
                                    <asp:Label ID="Label1" runat="server" Width="60px" Text="SIS用户:"></asp:Label>
                                </span><span style="float: right"></span>
                            </td>
                            <td align="left" style="width: 10%;">
                            </td>
                            <td align="left" style="width: 50%;">
                                <asp:Label ID="Label5" runat="server" Width="60px" Text="SIS角色:"></asp:Label>
                                <asp:DropDownList ID="ddl_Groups" runat="server" Width="120px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddl_Groups_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="width: 100%">
                <td align="center" style="width: 100%" class="table">
                    <table style="width: 95%">
                        <tr style="width: 100%">
                            <td align="left" style="width: 40%;" valign="middle">
                                <table style="width: 100%">
                                    <tr style="width: 100%">
                                        <td align="left" style="width: 30%;" valign="middle">
                                            <asp:ImageButton ID="btnNotCheckALL" runat="server" ImageUrl="../imgs/checkall.jpg"
                                                Width="50px" Height="30px" ToolTip="全选" OnClick="btnNotCheckALL_Click" />
                                        </td>
                                        <td align="left" style="width: 30%;" valign="middle">
                                            <asp:ImageButton ID="btnNotCheckNot" runat="server" ImageUrl="../imgs/checknot.jpg"
                                                Width="50px" Height="30px" ToolTip="全不选" OnClick="btnNotCheckNot_Click" />
                                        </td>
                                        <td align="left" style="width: 30%;" valign="middle">
                                            <asp:ImageButton ID="btnNotCheck" runat="server" ImageUrl="../imgs/check.jpg" Width="50px"
                                                Height="30px" ToolTip="反选" OnClick="btnNotCheck_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="left" style="width: 10%;">
                            </td>
                            <td align="left" style="width: 50%;">
                                <table style="width: 100%">
                                    <tr style="width: 100%">
                                        <td align="left" style="width: 30%;" valign="middle">
                                            <asp:ImageButton ID="btnCheckALL" runat="server" ImageUrl="../imgs/checkall.jpg"
                                                Width="50px" Height="30px" ToolTip="全选" OnClick="btnCheckALL_Click" />
                                        </td>
                                        <td align="left" style="width: 30%;" valign="middle">
                                            <asp:ImageButton ID="btnCheckNot" runat="server" ImageUrl="../imgs/checknot.jpg"
                                                Width="50px" Height="30px" ToolTip="全不选" OnClick="btnCheckNot_Click" />
                                        </td>
                                        <td align="left" style="width: 30%;" valign="middle">
                                            <asp:ImageButton ID="btnCheck" runat="server" ImageUrl="../imgs/check.jpg" Width="50px"
                                                Height="30px" ToolTip="反选" OnClick="btnCheck_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="width: 100%">
                <td align="center" style="width: 100%" class="table">
                    <table style="width: 95%">
                        <tr style="width: 100%">
                            <td style="width: 40%;" align="left">
                                <%--<div style="vertical-align: top; border-style: inset; overflow: auto; width: 40%;
                                    height: 400px;" id="divtag">
                                    <p align="left">
                                    </p>
                                </div>--%>
                                <fieldset class="field_info" style="width: 95%; height: 400px; vertical-align: top;">
                                    <legend>未授权用户</legend>
                                    <asp:CheckBoxList ID="cbxNotUsers" runat="server" BackColor="GhostWhite" Width="200px">
                                    </asp:CheckBoxList>
                                </fieldset>
                            </td>
                            <td style="width: 10%; vertical-align: top;">
                                <table style="width: 100%">
                                    <tr style="width: 100%; vertical-align: top;">
                                        <td align="center" style="width: 100%;">
                                            <asp:Label ID="Label3" runat="server" Text="  "></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="width: 100%">
                                        <td align="center" style="width: 100%;">
                                            <asp:ImageButton ID="btnRight" runat="server" ImageUrl="../imgs/moveright.green.jpg"
                                                Width="40px" Height="30px" ToolTip="添 加" OnClick="btnRight_Click" />
                                        </td>
                                    </tr>
                                    <tr style="width: 100%">
                                        <td align="center" style="width: 100%;">
                                            <asp:Label ID="Label4" runat="server" Text="  "></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="width: 100%">
                                        <td align="center" style="width: 100%;">
                                            <asp:ImageButton ID="btnLeft" runat="server" ImageUrl="../imgs/moveleft.green.jpg"
                                                Width="40px" Height="30px" ToolTip="移 除" OnClick="btnLeft_Click" />
                                        </td>
                                    </tr>
                                    <tr style="width: 100%">
                                        <td align="center" style="width: 100%;">
                                            <asp:Label ID="Label6" runat="server" Text="  "></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="width: 100%">
                                        <td align="center" style="width: 100%;">
                                            <asp:ImageButton ID="btnTransfer" runat="server" ImageUrl="../imgs/movetransfer.jpg"
                                                Width="40px" Height="30px" ToolTip="交 换" OnClick="btnTransfer_Click" />
                                        </td>
                                    </tr>
                                    <tr style="width: 100%">
                                        <td align="center" style="width: 100%;">
                                            <asp:Label ID="Label7" runat="server" Text="  "></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="width: 100%">
                                        <td align="center" style="width: 100%;">
                                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="../imgs/save.jpg" Width="30px"
                                                Height="30px" ToolTip="保存" OnClick="btnSave_Click" />
                                        </td>
                                    </tr>
                                    <tr style="width: 100%">
                                        <td align="center" style="width: 100%;">
                                            <asp:Label ID="Label8" runat="server" Text="  "></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="width: 100%">
                                        <td align="center" style="width: 100%;">
                                            <asp:ImageButton ID="btnDel" runat="server" ImageUrl="../imgs/delete.jpg" Width="30px"
                                                Height="30px" ToolTip="删除" OnClick="btnDel_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%;" align="left">
                                <%--<div style="vertical-align: top; border-style: inset; overflow: auto; width: 40%;
                                    height: 400px;" id="div1">
                                    <p align="left">
                                    </p>
                                </div>--%>
                                <fieldset class="field_info" style="width: 95%; height: 400px; vertical-align: top;">
                                    <legend>授权用户</legend>
                                    <asp:CheckBoxList ID="cbxUsers" runat="server" BackColor="GhostWhite" Width="200px">
                                    </asp:CheckBoxList>
                                </fieldset>
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
