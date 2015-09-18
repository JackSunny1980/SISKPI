<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_Menu.aspx.cs" Inherits="SISKPI.KPI_Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISOPM Config</title>
    <base target="_self" />
    <link href="../CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <script src="../js/ProcessBar.js" type="text/javascript"></script>
    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../js/Common.js" type="text/javascript"></script>
    <script src="../js/tab/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="../js/Tab/jquery.tabs.js" type="text/javascript"></script>
    <script src="../js/Config.js" type="text/javascript"></script>
    <script src="../js/GridViewColor.js" type="text/javascript"></script>
    <script type="text/javascript">
        function postBackByObject() {
            __doPostBack("", "");
        }

        //        function PlantUpdate(plantid) {
        //            var iWidth = 600; //模态窗口宽度
        //            var iHeight = 500; //模态窗口高度
        //            var iTop = (window.screen.height - iHeight) / 2;
        //            var iLeft = (window.screen.width - iWidth) / 2;

        //            //更新，keyid="........"
        //            window.open("OPM_SubPlantConfig.aspx?plantid=" + plantid, "newwindow", "Width=" + iWidth + " ,Height=" + iHeight + ",top=" + iTop + ",left=" + iLeft);

        //        }

    </script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%; text-align: center;">
    <div style="margin: 0px; width: 95%; text-align: center;">
        <%--
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
            </ContentTemplate>
        </asp:UpdatePanel>
        --%>
        <table width="100%" align="left" class="table" id="table1">
            <tr style="width: 100%" class="table_tr">
                <td align="left" style="width: 100%">
                    <table class="table" id="table2" style="width: 100%">
                        <tr style="width: 100%">
                            <td valign="middle" align="center" style="width: 100%; height: 40px;">
                                <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                                <asp:Label ID="lblTitle" runat="server" Class="title" Text="SIS系统管理"></asp:Label>
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
                            <td align="center" style="width: 100%">
                                <table class="table" style="width: 98%;">
                                    <tr align="left" style="width: 100%;">
                                        <td align="left" width="40%">
                                            <asp:Label ID="Label1" runat="server" Width="60px" Text="授权角色:"></asp:Label>
                                            <asp:DropDownList ID="ddl_MenuGroups" runat="server" Width="120px" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddl_MenuGroups_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right" width="60%">
                                        </td>
                                    </tr>
                                    <tr align="left" style="width: 100%;">
                                        <td align="left" valign="top" width="40%">
                                            <fieldset class="field_info" style="width: 95%; height: 100%; vertical-align: top;">
                                                <legend>菜单列表</legend>
                                                <table class="table" style="width: 100%;">
                                                    <tr align="left" style="width: 100%;">
                                                        <td align="left" width="100%">
                                                            <table class="table" style="width: 100%;">
                                                                <tr align="left" style="width: 100%;">
                                                                    <td align="center" width="100%">
                                                                        <span style="float: left">
                                                                            <asp:ImageButton ID="btnLeft" runat="server" ImageUrl="../imgs/moveleft.green.jpg"
                                                                                Width="30px" Height="30px" ToolTip="左移" OnClick="btnLeft_Click" />
                                                                            <asp:ImageButton ID="btnUp" runat="server" ImageUrl="../imgs/moveup.green.jpg" Width="30px"
                                                                                Height="30px" ToolTip="上移" OnClick="btnUp_Click" />
                                                                            <asp:ImageButton ID="btnDown" runat="server" ImageUrl="../imgs/movedown.green.jpg"
                                                                                Width="30px" Height="30px" ToolTip="下移" OnClick="btnDown_Click" />
                                                                            <asp:ImageButton ID="btnRight" runat="server" ImageUrl="../imgs/moveright.green.jpg"
                                                                                Width="30px" Height="30px" ToolTip="右移" OnClick="btnRight_Click" />
                                                                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="../imgs/save.jpg" Width="30px"
                                                                                Height="30px" ToolTip="保存" OnClick="btnSave_Click" />
                                                                        </span><span style="float: right">
                                                                            <asp:ImageButton ID="btnDel" runat="server" ImageUrl="../imgs/delete.jpg" Width="30px"
                                                                                Height="30px" ToolTip="删除" OnClick="btnDel_Click" />
                                                                        </span>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr align="left" style="width: 100%;">
                                                        <td align="left" width="100%">
                                                            <asp:TreeView ID="TVWMenu" runat="server" Width="98%" Height="100%" ShowLines="False"
                                                                ShowCheckBoxes="All" OnSelectedNodeChanged="TVWMenu_SelectedNodeChanged" OnTreeNodeCheckChanged="TVWMenu_TreeNodeCheckChanged">
                                                                <%--<ParentNodeStyle Font-Bold="True" ForeColor="#000000" />--%>
                                                                <%--
                                                                    <LevelStyles>
                                                                        <asp:TreeNodeStyle CssClass="nodeLevel1" />
                                                                        <asp:TreeNodeStyle CssClass="nodeLevel2" />
                                                                        <asp:TreeNodeStyle CssClass="nodeLevel3" />
                                                                    </LevelStyles>
                                                                --%>
                                                                <HoverNodeStyle Font-Underline="False" />
                                                                <SelectedNodeStyle BorderColor="#FF3300" BackColor="Yellow" BorderWidth="1px" />
                                                            </asp:TreeView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                        <td align="left" valign="top" width="60%">
                                            <div style="width: 100%; height: 400px; vertical-align: top;">
                                                <fieldset class="field_info" style="width: 95%;">
                                                    <legend>菜单编辑</legend>
                                                    <table class="table" style="width: 90%;">
                                                        <tr align="left" style="width: 100%;">
                                                            <td align="left" width="30%">
                                                            </td>
                                                            <td align="left" width="70%">
                                                                <table class="table" style="width: 98%;">
                                                                    <tr align="left" style="width: 100%;">
                                                                        <td align="left" width="100px">
                                                                            <asp:Button ID="btnMenuAdd" runat="server" Width="80px" Text="增 加" OnClick="btnMenuAdd_Click" />
                                                                        </td>
                                                                        <td align="left" width="100px">
                                                                            <asp:Button ID="btnMenuModify" runat="server" Width="80px" Text="修 改" OnClick="btnMenuModify_Click" />
                                                                        </td>
                                                                        <td align="left" width="100px">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr align="left" style="width: 100%;">
                                                            <td align="left" width="30%">
                                                                上级节点：
                                                            </td>
                                                            <td align="left" width="70%">
                                                                <select runat="server" id="ddl_MenuParentID" style="width: 120px">
                                                                </select>
                                                            </td>
                                                        </tr>
                                                        <tr align="left" style="width: 100%;">
                                                            <td align="left" width="30%">
                                                                菜单编码：
                                                            </td>
                                                            <td align="left" width="70%">
                                                                <asp:TextBox ID="tbx_MenuCode" runat="server" Text="BD01" Enabled="false"></asp:TextBox>
                                                                <asp:Label ID="lbl_MenuID" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="left" style="width: 100%;">
                                                            <td align="left" width="30%">
                                                                菜单名称：
                                                            </td>
                                                            <td align="left" width="70%">
                                                                <asp:TextBox ID="tbx_MenuName" runat="server" Text=""></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr align="left" style="width: 100%;">
                                                            <td align="left" width="30%">
                                                                菜单描述：
                                                            </td>
                                                            <td align="left" width="70%">
                                                                <asp:TextBox ID="tbx_MenuDesc" runat="server" Text=""></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr align="left" style="width: 100%;">
                                                            <td align="left" width="30%">
                                                                菜单是否显示：
                                                            </td>
                                                            <td align="left" width="70%">
                                                                <asp:RadioButtonList ID="rbl_MenuIsDisplay" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="显示" Value="1" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="隐藏" Value="0"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr align="left" style="width: 100%;">
                                                            <td align="left" width="30%">
                                                                菜单类型：
                                                            </td>
                                                            <td align="left" width="70%">
                                                                <asp:RadioButtonList ID="rbl_MenuType" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="目录" Value="1" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="页面" Value="0"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr align="left" style="width: 100%;">
                                                            <td align="left" width="30%">
                                                                菜单地址：
                                                            </td>
                                                            <td align="left" width="70%">
                                                                <asp:TextBox ID="tbx_MenuURL" runat="server" Text="http://www.baidu.com" Width="300px"
                                                                    Style="vertical-align: top;"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr align="left" style="width: 100%;">
                                                            <td align="left" width="30%">
                                                                菜单图标：
                                                            </td>
                                                            <td align="left" width="70%">
                                                                <asp:TextBox ID="tbx_MenuGIF" runat="server" Text="100.gif" Width="300px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr align="left" style="width: 100%;">
                                                            <td align="left" width="30%">
                                                                菜单显示方式：
                                                            </td>
                                                            <td align="left" width="70%">
                                                                <asp:RadioButtonList ID="rbl_MenuTarget" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="左侧" Value="1" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="弹窗" Value="0"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <%--
                                                        <tr align="left" style="width: 100%;">
                                                            <td align="left" width="30%">
                                                                菜单角色集合：
                                                            </td>
                                                            <td align="left" width="70%">
                                                                <asp:Button ID="btnMenuGroups" runat="server" Width="120px" Text="请选择..." OnClick="btnMenuGroups_Click" />
                                                                <asp:Label ID="lbl_MenuGroups" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        --%>
                                                        <tr align="left" style="width: 100%;">
                                                            <td align="left" width="30%">
                                                                菜单有效性：
                                                            </td>
                                                            <td align="left" width="70%">
                                                                <asp:RadioButtonList ID="rbl_MenuIsValid" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="有效" Value="1" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="无效" Value="0"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr align="left" style="width: 100%;">
                                                            <td align="left" width="30%">
                                                                菜单备注：
                                                            </td>
                                                            <td align="left" width="70%">
                                                                <asp:TextBox ID="tbx_MenuNote" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr align="left" style="width: 100%;">
                                                            <td align="left" width="30%">
                                                            </td>
                                                            <td align="left" width="70%">
                                                            </td>
                                                        </tr>
                                                        <tr align="left" style="width: 100%;">
                                                            <td align="left" width="30%">
                                                                <asp:Button ID="btnImport" runat="server" Text="导入子菜单" OnClick="btnImport_Click" />
                                                            </td>
                                                            <td align="left" width="70%">
                                                                <asp:Label ID="Label3" runat="server" Width="60px" Text="选择文件:"></asp:Label>
                                                                <asp:FileUpload ID="fuExcel" runat="server" Width="200px" />
                                                            </td>
                                                        </tr>
                                                        <tr align="left" style="width: 100%;">
                                                            <td align="left" width="30%">
                                                            </td>
                                                            <td align="left" width="70%">
                                                                <asp:Label ID="Label4" runat="server" Width="60px" Text="输入表单:"></asp:Label>
                                                                <asp:TextBox ID="tbxSheet" runat="server" Text="Sheet1"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <table class="table" style="width: 98%;">
                                    <tr align="left" style="width: 100%;">
                                        <td align="left" style="width: 100%;">
                                        </td>
                                    </tr>
                                </table>
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
