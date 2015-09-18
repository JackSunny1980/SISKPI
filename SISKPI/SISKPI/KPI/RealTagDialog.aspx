<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RealTagDialog.aspx.cs"
    Inherits="SISKPI.KPI.RealTagDialog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择安全指标对应的测点</title>
    <%-- <link href="../CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <link href="../CSS/jquery-ui-1.9.2.custom.min.css" type="text/css" rel="Stylesheet" />
    <script src="../js/GridViewColor.js" type="text/javascript"></script>
    <script src="../js/jquery-1.8.3.js" type="text/javascript"></script>--%>
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

        function closeWindow() {
            self.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Repeater ID="TagRepeater" runat="server">
                    <HeaderTemplate>
                        <table class="datagrid table table-hover table-bordered table-condensed">
                            <tr>
                                <th width="10%"></th>
                                <th width="30%">测点代码
                                </th>
                                <th width="60%">测点名称
                                </th>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="VLine" align="center">
                                <asp:CheckBox ID="chkSelected" runat="server" />
                            </td>
                            <td class="VLine" align="center">
                                <asp:Literal runat="server" ID="lblRealID" Text='<%# Eval("RealID") %>' Visible="false" />
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
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSave1" />
            </Triggers>
        </asp:UpdatePanel>
        <p></p>
        <p align="center">
            <asp:Button runat="server" CssClass="btn btn-default" Text="保存" ID="btnSave1" OnClick="btnSave_Click" />
            <input type="button" class="btn btn-default" value="关闭" onclick="closeWindow();" />
        </p>
    </form>
</body>
</html>
