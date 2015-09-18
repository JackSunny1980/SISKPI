<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectPersonDialog.aspx.cs" Inherits="SISKPI.KPI.SelectPersonDialog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>选择参与人员</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap-theme.css" rel="stylesheet" />
    <link href="../Content/themes/Styles/style.css" rel="stylesheet" />
    <link href="../Content/main.css" rel="stylesheet" />
    <link href="../Content/themes/Styles/gridview.css" rel="stylesheet" />
    <%--<link href="CSS/PagingStyle.css" rel="stylesheet" type="text/css" />--%>
    <link href="../Scripts/jquery-ui-1.11.1/jquery-ui.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.11.1/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        function submitForm() {
            alert("SSSSS");
            $("#btnClose").click();
            alert("22222222");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="SM">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UP1" runat="server">
            <ContentTemplate>
                <div class="btnbarcontetn" style="margin-top: 1px; background: #fff">
                    <table border="0" class="frm-find" style="height: 45px;">
                        <tbody>
                            <tr>
                                <th>工作单元：
                                </th>
                                <td>
                                    <asp:DropDownList ID="drpPlants" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpPlants_SelectedIndexChanged" />
                                </td>
                                <th>机组：
                                </th>
                                <td>
                                    <asp:DropDownList ID="drpUnits" runat="server" />
                                </td>
                                <th>值别：
                                </th>
                                <td>
                                    <asp:DropDownList ID="drpShifts" runat="server">
                                        <asp:ListItem Value="1">1值</asp:ListItem>
                                        <asp:ListItem Value="2">2值</asp:ListItem>
                                        <asp:ListItem Value="3">3值</asp:ListItem>
                                        <asp:ListItem Value="4">4值</asp:ListItem>
                                        <asp:ListItem Value="5">5值</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearch" CssClass="btnSearch" Text="检索" runat="server" OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <asp:Repeater ID="PersonRepeater" runat="server">
                    <HeaderTemplate>
                        <table class="datagrid table table-hover table-bordered table-condensed">
                            <thead>
                                <th></th>
                                <th>人员</th>
                                <th>岗位</th>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkSelected" runat="server" />
                                <asp:Literal ID="lblPersonID" runat="server" Text='<%#Eval("PersonID") %>' Visible="false" />
                            </td>
                            <td><asp:Literal ID="lblPersonName" runat="server" Text='<%#Eval("PersonName") %>' /></td>
                            <td><%#Eval("PositionName") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <p></p>
                <p class="text-center">
                    <asp:Button ID="btnClose" runat="server" Text="关闭" CssClass="btn btn-default" OnClick="btnClose_Click" />
                    <input type="button" value="Submit" onclick="submitForm();" />
                </p>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</body>
</html>
