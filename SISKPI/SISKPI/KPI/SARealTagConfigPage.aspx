<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SARealTagConfigPage.aspx.cs"
    Inherits="SISKPI.KPI.SARealTagConfigPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!--<link href="../CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <link href="../CSS/jquery-ui-1.9.2.custom.min.css" type="text/css" rel="Stylesheet" />
    <script src="../js/GridViewColor.js" type="text/javascript"></script>
    <script src="../js/jquery-1.8.3.js" type="text/javascript"></script>-->

    <link href="../Content/themes/Styles/style.css" rel="stylesheet" />
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap-theme.css" rel="stylesheet" />
    <link href="../Content/main.css" rel="stylesheet" />
    <link href="../Content/themes/Styles/gridview.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="../Scripts/json2.js" type="text/javascript"></script>
    <script src="JS/statisticTagConfig.js" type="text/javascript"></script>
    <script type="text/javascript">
        function selectedTag(UnitID, SAID) {
            var sURL = "RealTagDialog.aspx?rand=" + Math.random() + "&UnitID=" + UnitID + "&SAID=" + SAID;
            var height = 400;
            var width = 600;
            window.showModalDialog(sURL, null, "dialogHeight=" + height + "px;dialogWidth=" + width + "px");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="btnbarcontetn" style="height: 50px; line-height: 45px; background: #fff; margin: 0">
            <asp:UpdatePanel ID="UP3" runat="server">
                <ContentTemplate>
                    工作单元<asp:DropDownList ID="drpPlants" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpPlants_SelectedIndexChanged" />
                    选择机组<asp:DropDownList ID="drpUnits" runat="server" />
                    <asp:Button ID="btnSearch" runat="server" Text="检索" CssClass="btn btn-default" OnClick="btnSearch_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="datagrid-title-panel">
            <div class="datagrid-title">
                <img src="/Content/Themes/Images/32/202323.png" width="25" height="25" style="vertical-align: middle;" />
                安全指标关联测点配置
            </div>
        </div>
        <asp:UpdatePanel ID="UP1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Repeater ID="SARepeater" runat="server" OnItemCommand="ItemCommand">
                    <HeaderTemplate>
                        <table class="datagrid table table-hover table-bordered table-condensed">
                            <tr>
                                <th width="10%">序号
                                </th>
                                <th width="45%">指标名称
                                </th>
                                <th width="45%">操作
                                </th>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="VLine" align="center">
                                <%# Container.ItemIndex + 1%>
                            </td>
                            <td class="VLine" align="center">
                                <asp:Literal runat="server" ID="lblSAID" Text='<%# Eval("SAID") %>' Visible="false" />
                                <asp:Literal runat="server" ID="lblSAName" Text='<%# Eval("SAName") %>' />
                            </td>
                            <td class="VLine" align="center">
                                <input type="button" class="btn btn-default" value="添加测点" onclick='selectedTag("<%# Eval("UnitID") %>    ","<%# Eval("SAID") %>    ");' />
                                <asp:Button ID="btnSelected" runat="server" CssClass="btn btn-default" Text="查看测点" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <span class="text-primary alert">
                    <asp:Literal  ID="lblMsg" runat="server" Text="安全指标对应的测点" /></span>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UP2" runat="server">
            <ContentTemplate>
                <asp:Repeater ID="TagRepeater" runat="server">
                    <HeaderTemplate>
                        <table class="datagrid table table-hover table-bordered table-condensed">
                            <tr>
                                <th width="10%">序号
                                </th>
                                <th width="45%">测点代码
                                </th>
                                <th width="45%">测点名称
                                </th>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="VLine" align="center">
                                <%# Container.ItemIndex + 1%>
                            </td>
                            <td class="VLine" align="center">
                                <%# Eval("RealCode") %>
                            </td>
                            <td class="VLine" align="center">
                                <%# Eval("RealDesc")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
