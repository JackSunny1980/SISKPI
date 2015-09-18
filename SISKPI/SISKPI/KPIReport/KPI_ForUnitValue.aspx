<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_ForUnitValue.aspx.cs"
    Inherits="SISKPI.KPI_ForUnitValue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISKPI</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap-theme.css" rel="stylesheet" />
    <link href="../Content/themes/Styles/style.css" rel="stylesheet" />
    <link href="../Content/main.css" rel="stylesheet" />
    <link href="../Content/themes/Styles/gridview.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="JS/kpi_public.js" type="text/javascript"></script>
    <script src="../js/DatePicker/WdatePicker.js" type="text/javascript" defer="defer"></script>
</head>
<body>
    <form id="form1" runat="server">
        <input id="hidSelectedItem" type="hidden" value="0" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="header">
            <div class="tools_bar">
                <a id="btn_Add" title="新增" class="tools_btn" style="display:none;"><span><b style="background: url('/Content/Themes/images/16/Add.png') 50% 4px no-repeat;">新增</b></span></a>
                <a id="btn_Edit" title="编辑" class="tools_btn" style="display:none;"><span><b style="background: url('/Content/Themes/images/16/bullet_edit.png') 50% 4px no-repeat;">编辑</b></span></a>
                <a id="btn_Del" title="删除" class="tools_btn" style="display:none;"><span><b style="background: url('/Content/Themes/images/16/DeleteRed.png') 50% 4px no-repeat;">删除</b></span></a>
                <a id="btn_Select" title="查看" class="tools_btn" style="display:none;"><span><b style="background: url('/Content/Themes/images/16/shape_square_select.png') 50% 4px no-repeat;">查看</b></span></a>
                <a id="btn_Search" title="查询" class="tools_btn" style="display:block;"><span><b style="background: url('/Content/Themes/images/16/search.png') 50% 4px no-repeat;">查询</b></span></a>
                <a id="btn_Export" title="导出到Excel" class="tools_btn" style="display:block;"><span><b style="background: url('/Content/Themes/images/16/export.png') 50% 4px no-repeat;">导出</b></span></a>
                <a id="btn_Return" title="返回" class="tools_btn" style="display:block;"><span><b style="background: url('/Content/Themes/images/16/back.png') 50% 4px no-repeat;">返回</b></span></a>
                <asp:Button runat="server" Text="" style="display:none;" ID="btnAdd" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnEdit" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnDel" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnSelect" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnSearch" OnClick="btnSearch_Click" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnExport" OnClick="btnExport_Click" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnReturn" OnClick="btnReturn_Click" />
            </div>
            <div class="btnbarcontetn" style="margin-top: 1px; background: #fff">
                <div>
                    <table border="0" class="frm-find" style="height: 45px;">
                        <tbody>
                            <tr>
                                <th>
                                    <asp:Label ID="Label3" runat="server" Text="值别：" ></asp:Label>
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddlShift" runat="server" Width="120px" CssClass="select"></asp:DropDownList>
                                </td>
                                <th>
                                    <asp:Label ID="Label1" runat="server" Text="开始时间:"></asp:Label>
                                </th>
                                <td>
                                    <input class="Wdate" id="txt_ST" visible="true" style="width: 140px" onfocus="WdatePicker({el:'txt_ST',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" type="text" runat="server" value="" />
                                </td>
                                <th>
                                    <asp:Label ID="Label2" runat="server" Text="结束时间:"></asp:Label>
                                </th>
                                <td>
                                    <input class="Wdate" id="txt_ET" visible="true" style="width: 140px" onfocus="WdatePicker({el:'txt_ET',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" type="text" runat="server" value="" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="datagrid-title-panel">
            <div class="datagrid-title">
                <img src="/Content/Themes/Images/32/202323.png" width="25" height="25" style="vertical-align: middle;">
                按机组统计报表查询
            </div>
        </div>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="width:100%;">
                    <span style="float: left">
                        <asp:Label ID="lblInfor" runat="server" Width="300px" ForeColor="Red" Font-Bold="true" Text="报表"></asp:Label>
                    </span>
                    <asp:GridView ID="gvData" runat="server" CssClass="table table-hover table-bordered table-condensed datagrid"
                        AutoGenerateColumns="True" ShowFooter="True" 
                        EmptyDataText="没有满足条件的数据" AllowPaging="false" Width="100%" 
                        OnRowDataBound="gvData_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="序号" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</body>
</html>
