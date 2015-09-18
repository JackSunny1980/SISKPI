<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_PositionConfig.aspx.cs"
    Inherits="SISKPI.KPI_PositionConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISPosition Config</title>
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
            window.open("Position_SubPlantConfig.aspx?plantid=" + plantid, "newwindow", "Width=" + iWidth + " ,Height=" + iHeight + ",top=" + iTop + ",left=" + iLeft);

        }

        function UnitUpdate(unitid) {

            var iWidth = 600; //模态窗口宽度
            var iHeight = 500; //模态窗口高度
            var iTop = (window.screen.height - iHeight) / 2;
            var iLeft = (window.screen.width - iWidth) / 2;

            //更新机组，keyid="........"
            window.open("Position_SubUnitConfig.aspx?unitid=" + unitid, "newwindow", "Width=" + iWidth + " ,Height=" + iHeight + ",top=" + iTop + ",left=" + iLeft);
        }

        function UnitWork(unitid) {
            //配置 Work，keyid="........", type=2
            window.location.href = "Position_Base.aspx?unitid=" + unitid;
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
                                <asp:Label ID="lblTitle" runat="server" Class="title" Text="岗位配置"></asp:Label>
                            </td>
                        </tr>
                        <tr style="width: 100%">
                            <td align="center" style="width: 100%">
                                <div style="width: 100%">
                                    <fieldset class="field_info" style="width: 95%;">
                                        <legend>岗位信息</legend>
                                        <table class="table" style="width: 98%;">
                                            <tr align="left" style="width: 100%;">
                                                <td align="left" style="width: 100%;">
                                                    <asp:GridView ID="gvPosition" CssClass="GridViewStyle" Width="100%" runat="server"
                                                        AutoGenerateColumns="False" EmptyDataText="没有满足条件的数据" AllowPaging="False" OnRowDataBound="gvPosition_RowDataBound"
                                                        OnRowCommand="gvPosition_RowCommand" OnPageIndexChanging="gvPosition_PageIndexChanging"
                                                        OnRowCancelingEdit="gvPosition_RowCancelingEdit" OnRowEditing="gvPosition_RowEditing"
                                                        OnRowUpdating="gvPosition_RowUpdating">
                                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                                        <RowStyle CssClass="GridViewRowStyle" />
                                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                                        <PagerStyle CssClass="GridViewPagerStyle" />
                                                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="序号">
                                                                <ItemTemplate>
                                                                    <input type="hidden" runat="server" id="positionid" value='<%# Eval("PositionID").ToString()%>' />
                                                                    <%#  Container.DataItemIndex + 1%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="40px" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="PositionName" HeaderText="名称">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PositionDesc" HeaderText="描述">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PositionWeight" HeaderText="权重">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="是否交接盘" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle Width="40px"></ControlStyle>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlHand" runat="server" DataSourceID="GetYesNo" DataTextField="Name"
                                                                        DataValueField="Value">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="是否交接班" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlShift" runat="server" DataSourceID="GetYesNo" DataTextField="Name"
                                                                        DataValueField="Value">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ControlStyle Width="40px"></ControlStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="是否有效" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle Width="40px"></ControlStyle>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlValid" runat="server" DataSourceID="GetYesNo" DataTextField="Name"
                                                                        DataValueField="Value">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="PositionNote" HeaderText="备注">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:CommandField HeaderText="配置" ShowEditButton="True" EditText="编辑" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="delete" runat="server" Text="删除" CommandName="dataDelete" CommandArgument='<%# Eval("PositionID") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="60px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr align="center" style="width: 100%;">
                                                <td align="center" style="width: 100%;">
                                                    <asp:Button ID="btnAddPosition" Width="120px" runat="server" Text="新增岗位" OnClick="btnAddPosition_Click" />
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
