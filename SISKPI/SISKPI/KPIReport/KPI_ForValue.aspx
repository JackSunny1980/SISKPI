<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_ForValue.aspx.cs" Inherits="SISKPI.KPI_ForValue" %>

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

    <script type="text/javascript">
        $(document).ready(function () {
            $('#rbt1').click(function () {
                $('#gvData tr[id]').removeClass("datagrid-row-select");
                $('#hidSelectedItem').val('0');
                $('#hidRbt').val('0');
                $('#divData').show();
                $('#divData1').hide();
            });
            $('#rbt2').click(function () {
                $('#gvData1 tr[id]').removeClass("datagrid-row-select");
                $('#hidSelectedItem').val('0');
                $('#hidRbt').val('1');
                $('#divData').hide();
                $('#divData1').show();
            });
            $('#rbt1').click();

            $('#btnSelect').click(function () {
                var selectedId = $('#hidSelectedItem').val();
                if (selectedId != "0") {
                    if ($('#hidRbt').val() == '0') {
                        location.href = "KPI_ForShiftValue.aspx?webcode=" + selectedId;
                    } else {
                        location.href = "KPI_ForUnitValue.aspx?webcode=" + selectedId;
                    }
                }
                return false;
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <input id="hidSelectedItem" type="hidden" value="0" runat="server">
        <input id="hidRbt" type="hidden" value="0" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="header">
            <div class="tools_bar">
                <a id="btn_Add" title="新增" class="tools_btn" style="display:none;"><span><b style="background: url('/Content/Themes/images/16/Add.png') 50% 4px no-repeat;">新增</b></span></a>
                <a id="btn_Edit" title="编辑" class="tools_btn" style="display:none;"><span><b style="background: url('/Content/Themes/images/16/bullet_edit.png') 50% 4px no-repeat;">编辑</b></span></a>
                <a id="btn_Del" title="删除" class="tools_btn" style="display:none;"><span><b style="background: url('/Content/Themes/images/16/DeleteRed.png') 50% 4px no-repeat;">删除</b></span></a>
                <a id="btn_Select" title="查看" class="tools_btn" style="display:block;"><span><b style="background: url('/Content/Themes/images/16/shape_square_select.png') 50% 4px no-repeat;">查看</b></span></a>
                <a id="btn_Search" title="查询" class="tools_btn" style="display:none;"><span><b style="background: url('/Content/Themes/images/16/search.png') 50% 4px no-repeat;">查询</b></span></a>
                <a id="btn_Export" title="导出" class="tools_btn" style="display:none;"><span><b style="background: url('/Content/Themes/images/16/export.png') 50% 4px no-repeat;">导出</b></span></a>
                <a id="btn_Return" title="返回" class="tools_btn" style="display:none;"><span><b style="background: url('/Content/Themes/images/16/back.png') 50% 4px no-repeat;">返回</b></span></a>
                <asp:Button runat="server" Text="" style="display:none;" ID="btnAdd" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnEdit" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnDel" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnSelect" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnSearch" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnExport" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnReturn" />
            </div>
            
        </div>
        <div class="datagrid-title-panel">
            <div class="datagrid-title">
                <img src="/Content/Themes/Images/32/202323.png" width="25" height="25" style="vertical-align: middle;">
                自定义报表查询
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <span>
                        <asp:RadioButton AutoPostBack="false" ID="rbt1" runat="server" GroupName="rbt" Text="分值统计报表" Checked="true"/>
                        <asp:RadioButton AutoPostBack="false" ID="rbt2" runat="server" GroupName="rbt" Text="分机组统计报表" />
                        
                        <asp:DropDownList ID="WebType" runat="server"  DataValueField="Value" DataTextField="Name" style="display:none;">
                        </asp:DropDownList>
                    </span>
                </div>
                <div id="divData" style="display:block;width:100%;">
                    <asp:GridView ID="gvData" runat="server" CssClass="table table-hover table-bordered table-condensed datagrid" 
                        AutoGenerateColumns="False" DataKeyNames="WebID,WebCode,WebType" Width="100%" 
                        EmptyDataText="没有满足条件的数据" AllowPaging="false" 
                        OnRowDataBound="gvData_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="序号" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%#  Container.DataItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="WebCode" HeaderText="代码" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="WebDesc" HeaderText="描述" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="WebType" HeaderText="类型" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div id="divData1" style="display:block;width:100%;">
                    <asp:GridView ID="gvData1" runat="server" CssClass="table table-hover table-bordered table-condensed datagrid" 
                        AutoGenerateColumns="False" DataKeyNames="WebID,WebCode,WebType" Width="100%"
                        EmptyDataText="没有满足条件的数据" AllowPaging="false" 
                        OnRowDataBound="gvData1_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="序号" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%#  Container.DataItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="WebCode" HeaderText="代码" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="WebDesc" HeaderText="描述" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="WebType" HeaderText="类型" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
