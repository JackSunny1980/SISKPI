<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_Config.aspx.cs" Inherits="SISKPI.KPI_Config" %>

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
    <script src="../js/tab/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="../js/Tab/jquery.tabs.js" type="text/javascript"></script>
    <script src="../js/Config.js" type="text/javascript"></script>
    <script src="../js/GridViewColor.js" type="text/javascript"></script>
    <script type="text/javascript">
        function PlantUpdate(plantid) {
            var iWidth = 600; //模态窗口宽度
            var iHeight = 500; //模态窗口高度
            var iTop = (window.screen.height - iHeight) / 2;
            var iLeft = (window.screen.width - iWidth) / 2;
            
            //更新，keyid="........"
            window.open("KPI_SubPlantConfig.aspx?plantid=" + plantid, "newwindow", "Width=" + iWidth + " ,Height=" + iHeight + ",top=" + iTop + ",left=" + iLeft);
          
        }

        function UnitUpdate(unitid) {

            var iWidth = 600; //模态窗口宽度
            var iHeight = 500; //模态窗口高度
            var iTop = (window.screen.height - iHeight) / 2;
            var iLeft = (window.screen.width - iWidth) / 2;

            //更新机组，keyid="........"
            window.open("KPI_SubUnitConfig.aspx?unitid=" + unitid, "newwindow", "Width=" + iWidth + " ,Height=" + iHeight + ",top=" + iTop + ",left=" + iLeft);
          }

        function UnitWork(unitid) {
            //配置 Work，keyid="........", type=2
            window.location.href = "KPI_Base.aspx?unitid=" + unitid;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%; text-align: center;">
    <div style="margin: 0px; width: 95%; text-align: center;">
        <table width="100%" align="left" class="table" id="table1">
            <tr style="width: 100%" class="table_tr">
                <td align="left" style="width: 100%">
                    <table class="table" id="table2" style="width: 100%">
                        <tr style="width: 100%">
                            <td valign="middle" align="center" style="width: 100%; height: 40px;">
                                <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                                <asp:Label ID="lblTitle" runat="server" Class="title" Text="机组配置"></asp:Label>
                            </td>
                        </tr>
                        <tr style="width: 100%">
                            <td align="center" style="width: 100%">
                                <div style="width: 100%">
                                    <fieldset class="field_info" style="width: 95%;">
                                        <legend>电厂信息</legend>
                                        <table class="table" style="width: 98%;">
                                            <tr align="left" style="width: 100%;">
                                                <td align="left" style="width: 100%;">
                                                    <asp:Button ID="btnAddPlant" Width="120px" runat="server" Text="新增电厂" OnClick="btnAddPlant_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table class="table" style="width: 98%;">
                                            <tr align="left" style="width: 100%;">
                                                <td align="left" style="width: 100%;">
                                                    <asp:GridView ID="gvPlant" CssClass="GridViewStyle" Width="100%" runat="server" AutoGenerateColumns="False"
                                                        EmptyDataText="没有满足条件的数据" AllowPaging="True" OnRowDataBound="gvPlant_RowDataBound"
                                                        OnRowCommand="gvPlant_RowCommand" OnPageIndexChanging="gvPlant_PageIndexChanging"
                                                        PageSize="4">
                                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                                        <RowStyle CssClass="GridViewRowStyle" />
                                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                                        <PagerStyle CssClass="GridViewPagerStyle" />
                                                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="序号">
                                                                <ItemTemplate>
                                                                    <input type="hidden" runat="server" id="plantid" value='<%# Eval("PlantID").ToString()%>' />
                                                                    <%#  Container.DataItemIndex + 1%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="40px" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="PlantName" HeaderText="电厂" ReadOnly="true" ControlStyle-Width="60px">
                                                                <ControlStyle Width="60px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PlantCode" HeaderText="代码" ControlStyle-Width="100px">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PlantDesc" HeaderText="描述" ControlStyle-Width="100px">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PlantIndex" HeaderText="序号" ControlStyle-Width="100px">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PlantIsValid" HeaderText="有效性" ControlStyle-Width="60px">
                                                                <ControlStyle Width="60px"></ControlStyle>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PlantNote" HeaderText="备注" ControlStyle-Width="100px">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <input class="GridViewButtonStyle" type="button" runat="server" id="plantupdate" value='编辑' />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="60px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="delete" runat="server" Text="删除" CommandName="dataDelete" CommandArgument='<%# Eval("PlantID") %>' />
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
                            <td align="center" style="width: 100%">
                                <div style="width: 100%">
                                    <fieldset class="field_info" style="width: 95%;">
                                        <legend>机组信息</legend>
                                        <table class="table" style="width: 98%;">
                                            <tr align="left" style="width: 100%;">
                                                <td align="left" width="100px">
                                                    机组名称：
                                                </td>
                                                <td align="left" width="100px">
                                                    <asp:TextBox ID="txt_UnitName" runat="server"></asp:TextBox>
                                                </td>
                                                <td align="left" width="100px">
                                                    机组描述：
                                                </td>
                                                <td align="right" width="100px">
                                                    <asp:TextBox ID="txt_UnitDesc" runat="server"></asp:TextBox>
                                                </td>
                                                <td align="left" width="100px">
                                                    <asp:Button ID="btnSearchUnit" Width="80px" runat="server" Text="查 询" OnClick="btnSearchUnit_Click" />
                                                </td>
                                                <td align="left" width="30%">
                                                </td>
                                            </tr>
                                            <tr align="left" style="width: 100%;">
                                                <td colspan="6" align="left" style="width: 100%;">
                                                    <asp:Button ID="btnAddUnit" Width="120px" runat="server" Text="新增机组" OnClick="btnAddUnit_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table class="table" style="width: 98%;">
                                            <tr align="left" style="width: 100%;">
                                                <td align="left" style="width: 100%;">
                                                    <asp:GridView ID="gvUnit" CssClass="GridViewStyle" Width="100%" runat="server" AutoGenerateColumns="False"
                                                        EmptyDataText="没有满足条件的数据" AllowPaging="True" OnRowDataBound="gvUnit_RowDataBound"
                                                        OnRowCommand="gvUnit_RowCommand" OnPageIndexChanging="gvUnit_PageIndexChanging"
                                                        PageSize="10">
                                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                                        <RowStyle CssClass="GridViewRowStyle" />
                                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                                        <PagerStyle CssClass="GridViewPagerStyle" />
                                                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="序号">
                                                                <ItemTemplate>
                                                                    <input type="hidden" runat="server" id="unitid" value='<%# Eval("UnitID").ToString()%>' />
                                                                    <%#  Container.DataItemIndex + 1%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="40px" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="PlantName" HeaderText="电厂" ReadOnly="true" ControlStyle-Width="60px">
                                                                <ControlStyle Width="60px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="UnitCode" HeaderText="代码" ReadOnly="true" ControlStyle-Width="60px">
                                                                <ControlStyle Width="60px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="UnitName" HeaderText="名称" ReadOnly="true" ControlStyle-Width="60px">
                                                                <ControlStyle Width="60px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="UnitDesc" HeaderText="描述" ControlStyle-Width="100px">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="UnitMW" HeaderText="额定负荷" ControlStyle-Width="60px">
                                                                <ControlStyle Width="60px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="UnitIsKPI" HeaderText="考核" ControlStyle-Width="60px">
                                                                <ControlStyle Width="60px"></ControlStyle>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="UnitIsSnapshot" HeaderText="实时考核" ControlStyle-Width="60px">
                                                                <ControlStyle Width="60px"></ControlStyle>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="UnitIsSort" HeaderText="排名考核" ControlStyle-Width="60px">
                                                                <ControlStyle Width="60px"></ControlStyle>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="UnitNote" HeaderText="备注" ControlStyle-Width="100px">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <%--<asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <input class="GridViewButtonStyle" type="button" runat="server" id="work" value='配置倒班' />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="60px" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <input class="GridViewButtonStyle" type="button" runat="server" id="unitupdate" value='编辑' />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="60px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="delete" runat="server" Text="删除" CommandName="dataDelete" CommandArgument='<%# Eval("UnitID") %>' />
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
