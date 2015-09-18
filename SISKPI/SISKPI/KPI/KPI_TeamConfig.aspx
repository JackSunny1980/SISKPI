<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_TeamConfig.aspx.cs"
    Inherits="SISKPI.KPI_TeamConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISTeam Config</title>
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
            window.open("Team_SubPlantConfig.aspx?plantid=" + plantid, "newwindow", "Width=" + iWidth + " ,Height=" + iHeight + ",top=" + iTop + ",left=" + iLeft);

        }

        function UnitUpdate(unitid) {

            var iWidth = 600; //模态窗口宽度
            var iHeight = 500; //模态窗口高度
            var iTop = (window.screen.height - iHeight) / 2;
            var iLeft = (window.screen.width - iWidth) / 2;

            //更新机组，keyid="........"
            window.open("Team_SubUnitConfig.aspx?unitid=" + unitid, "newwindow", "Width=" + iWidth + " ,Height=" + iHeight + ",top=" + iTop + ",left=" + iLeft);
        }

        function UnitWork(unitid) {
            //配置 Work，keyid="........", type=2
            window.location.href = "Team_Base.aspx?unitid=" + unitid;
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
                                <asp:Label ID="lblTitle" runat="server" Class="title" Text="班组配置"></asp:Label>
                            </td>
                        </tr>
                        <tr style="width: 100%">
                            <td align="center" style="width: 100%">
                                <div style="width: 100%">
                                    <fieldset class="field_info" style="width: 95%;">
                                        <legend>班组信息</legend>
                                        <table class="table" style="width: 98%;">
                                            <tr align="left" style="width: 100%;">
                                                <td align="right" style="width: 100%;">
                                                    <span style="float: left">
                                                        <asp:Button ID="btnClear" Width="120px" runat="server" Text="清空所有记录" 
                                                        onclick="btnClear_Click" />
                                                    </span><span style="float: right">
                                                        <asp:Button ID="btnBatch" Width="120px" runat="server" Text="班组批处理" OnClick="btnBatch_Click" />
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr align="left" style="width: 100%;">
                                                <td align="left" style="width: 100%;">
                                                    <asp:GridView ID="gvTeam" CssClass="GridViewStyle" Width="100%" runat="server" AutoGenerateColumns="False"
                                                        EmptyDataText="没有满足条件的数据" AllowPaging="False" OnRowDataBound="gvTeam_RowDataBound"
                                                        OnRowCommand="gvTeam_RowCommand" OnRowCancelingEdit="gvTeam_RowCancelingEdit"
                                                        OnRowEditing="gvTeam_RowEditing" OnRowUpdating="gvTeam_RowUpdating">
                                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                                        <RowStyle CssClass="GridViewRowStyle" />
                                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                                        <PagerStyle CssClass="GridViewPagerStyle" />
                                                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="序号">
                                                                <ItemTemplate>
                                                                    <input type="hidden" runat="server" id="teamid" value='<%# Eval("TeamID").ToString()%>' />
                                                                    <%#  Container.DataItemIndex + 1%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="40px" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <%--<asp:BoundField DataField="ShiftName" HeaderText="运行值" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PersonName" HeaderText="人员" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PositionName" HeaderText="岗位" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="TeamName" HeaderText="替班人员" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                            </asp:BoundField>--%>
                                                            <asp:TemplateField HeaderText="单元组" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlPlant" runat="server" DataSourceID="GetPlants" DataTextField="Name"
                                                                        DataValueField="ID" AppendDataBoundItems="true">
                                                                        <asp:ListItem Value="">无</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="运行值" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlShift" runat="server" DataSourceID="GetShifts" DataTextField="Name"
                                                                        DataValueField="ID" AppendDataBoundItems="true">
                                                                        <asp:ListItem Value="">无</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="人员" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlPerson" runat="server" DataSourceID="GetPersons" DataTextField="Name"
                                                                        DataValueField="ID" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlPerson_SelectedIndexChanged">
                                                                        <asp:ListItem Value="">无</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="岗位" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlPosition" runat="server" Enabled="false" DataSourceID="GetPositions"
                                                                        DataTextField="Name" DataValueField="ID" AppendDataBoundItems="true">
                                                                        <asp:ListItem Value="">无</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="替班人员" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlTeamPerson" runat="server" DataSourceID="GetPersons" DataTextField="Name"
                                                                        DataValueField="ID" AppendDataBoundItems="true">
                                                                        <asp:ListItem Value="">无</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--AppendDataBoundItems=true，关键--%>
                                                            <asp:BoundField DataField="TeamNote" HeaderText="备注" ControlStyle-Width="100px">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:CommandField HeaderText="配置" ShowEditButton="True" HeaderStyle-Width="60px"
                                                                ItemStyle-HorizontalAlign="Center" EditText="编辑" />
                                                            <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="delete" runat="server" Text="删除" CommandName="dataDelete" CommandArgument='<%# Eval("TeamID") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="60px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <%--<tr align="center" style="width: 100%;">
                                                <td align="left" style="width: 100%;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                运行值:<asp:DropDownList ID="ddlShift" runat="server" DataSourceID="GetShifts" DataTextField="Name"
                                                                    DataValueField="ID">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                人员:<asp:DropDownList ID="ddlPerson" runat="server" DataSourceID="GetPersons" DataTextField="Name"
                                                                    DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlPerson_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                岗位:<asp:DropDownList ID="ddlPosition" runat="server" DataSourceID="GetPositions"
                                                                    DataTextField="Name" DataValueField="ID">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                替班人员:<asp:DropDownList ID="ddlTeamPerson" runat="server" DataSourceID="GetPersons"
                                                                    DataTextField="Name" DataValueField="ID" AppendDataBoundItems="true">
                                                                    <asp:ListItem Value="">无</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>--%>
                                            <tr align="center" style="width: 100%;">
                                                <td align="center" style="width: 100%;">
                                                    <asp:Button ID="btnAddTeam" Width="120px" runat="server" Text="新增人员" OnClick="btnAddTeam_Click" />
                                                    <asp:ObjectDataSource ID="GetPlants" runat="server" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}"
                                                        SelectMethod="GetData" TypeName="SISKPI.KPIDataSetTableAdapters.GetPlantsTableAdapter">
                                                        <InsertParameters>
                                                            <asp:Parameter Name="ID" Type="String" />
                                                            <asp:Parameter Name="Name" Type="String" />
                                                        </InsertParameters>
                                                    </asp:ObjectDataSource>
                                                    <asp:ObjectDataSource ID="GetPositions" runat="server" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}"
                                                        SelectMethod="GetData" TypeName="SISKPI.KPIDataSetTableAdapters.GetPositionsTableAdapter">
                                                        <InsertParameters>
                                                            <asp:Parameter Name="ID" Type="String" />
                                                            <asp:Parameter Name="Name" Type="String" />
                                                        </InsertParameters>
                                                    </asp:ObjectDataSource>
                                                    <asp:ObjectDataSource ID="GetShifts" runat="server" OldValuesParameterFormatString="original_{0}"
                                                        SelectMethod="GetData" TypeName="SISKPI.KPIDataSetTableAdapters.GetShiftsTableAdapter"
                                                        InsertMethod="Insert">
                                                        <InsertParameters>
                                                            <asp:Parameter Name="ID" Type="String" />
                                                            <asp:Parameter Name="Name" Type="String" />
                                                        </InsertParameters>
                                                    </asp:ObjectDataSource>
                                                    <asp:ObjectDataSource ID="GetPersons" runat="server" OldValuesParameterFormatString="original_{0}"
                                                        SelectMethod="GetData" TypeName="SISKPI.KPIDataSetTableAdapters.GetPersonsTableAdapter"
                                                        InsertMethod="Insert">
                                                        <InsertParameters>
                                                            <asp:Parameter Name="ID" Type="String" />
                                                            <asp:Parameter Name="Name" Type="String" />
                                                        </InsertParameters>
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
