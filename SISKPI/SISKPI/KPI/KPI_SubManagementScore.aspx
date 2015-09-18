<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_SubManagementScore.aspx.cs" Inherits="SISKPI.KPI.KPI_SubManagementScore" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>日常管理考核录入</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap-theme.css" rel="stylesheet" />
    <link href="../Content/themes/Styles/style.css" rel="stylesheet" />
    <link href="../Content/main.css" rel="stylesheet" />
    <link href="../Content/themes/Styles/gridview.css" rel="stylesheet" />
    <link href="../Scripts/jquery-ui-1.11.1/jquery-ui.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.11.1/jquery-ui.js" type="text/javascript"></script>
    <script src="../js/DatePicker/WdatePicker.js" type="text/javascript" defer="defer"></script>
    <script type="text/javascript">
        function selectPerson() {
            var sURL = "SelectPersonDialog.aspx?rand=" + Math.random();
            var sFeatures = "dialogHeight:400px;dialogWidth:600px;center:yes;help:no;status:no;rsizable:yes";
            var vArguments = "";
            var urlValue = window.showModalDialog(sURL, vArguments, sFeatures);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UP1" runat="server">
            <ContentTemplate>
                <table id="table2" class="datagrid table table-hover table-bordered table-condensed">
                    <tr>
                        <td>工作类别
                        </td>
                        <td>
                            <asp:DropDownList ID="drpTagCategorys" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpTagCategorys_SelectedIndexChanged"/>
                        </td>
                        <td>工作项目
                        </td>
                        <td>
                            <asp:DropDownList ID="drpTags" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpTags_SelectedIndexChanged">
                                <asp:ListItem>负荷率</asp:ListItem>
                                <asp:ListItem>定期工作</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>工作项得分</td>
                        <td>
                            <asp:TextBox ID="txtTagScore" runat="server" /></td>
                        <td>考核日期</td>
                        <td>
                            <asp:TextBox ID="txtCheckDate" runat="server" CssClass="Wdate" onfocus="WdatePicker({el:'txtCheckDate',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" /></td>
                    </tr>
                    <tr>
                        <td>工作描述</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtDescript" Width="99%" Height="80px" runat="server" TextMode="MultiLine" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UP2" runat="server">
            <ContentTemplate>
                <asp:Repeater ID="PersonRepeater" runat="server">
                    <HeaderTemplate>
                        <table class="datagrid table table-hover table-bordered table-condensed">
                            <thead>
                                <tr>
                                    <th>值别</th>
                                    <th>人员</th>
                                    <th>岗位</th>
                                    <th>得分率(%)</th>
                                    <th>操作</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td align="center">1</td>
                            <td align="center">韩娜</td>
                            <td align="center">值长</td>
                            <td align="center">
                                <input type="text" value="80" /></td>
                            <td align="center">
                                <input type="button" value="删除" /></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </ContentTemplate>
        </asp:UpdatePanel>
        <p></p>
        <p class="text-center">
            <input type="button" value="选择参与人员" onclick="selectPerson();" />
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Button" />
        </p>
    </form>
</body>
</html>
