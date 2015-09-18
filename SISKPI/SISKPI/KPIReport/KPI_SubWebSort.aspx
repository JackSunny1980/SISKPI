<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_SubWebSort.aspx.cs"
    Inherits="SISKPI.KPI_SubWebSort" EnableEventValidation="false" %>

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
    
    <script src="../js/ProcessBar.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btn_Top').click(function () { $('#btnTop').click(); });
            $('#btn_Up').click(function () { $('#btnUp').click(); });
            $('#btn_Down').click(function () { $('#btnDown').click(); });
            $('#btn_Bottom').click(function () { $('#btnBottom').click(); });
            $('#btn_Apply').click(function () { $('#btnApply').click(); });

            $('#btnApply').click(function () {
                setDivPos('Lay1');
                Lay1.style.visibility = '';
                progress_update();
                return true;
            });
            $('#btnReturn').click(function () {
                var WebCode = $('#lblInfor2').html();
                location.href = "KPI_SubWeb.aspx?webcode=" + WebCode;
                return false;
            });

        });
        function call() {
        }
    </script>
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
                <a id="btn_Select" title="查看" class="tools_btn" style="display:none;"><span><b style="background: url('/Content/Themes/images/16/shape_square_select.png') 50% 4px no-repeat;">选中</b></span></a>
                <a id="btn_Search" title="查询" class="tools_btn" style="display:none;"><span><b style="background: url('/Content/Themes/images/16/search.png') 50% 4px no-repeat;">查询</b></span></a>
                <a id="btn_Export" title="导出到Excel" class="tools_btn" style="display:none;"><span><b style="background: url('/Content/Themes/images/16/export.png') 50% 4px no-repeat;">导出</b></span></a>
                
                <a id="btn_Top" title="移到顶部" class="tools_btn" style="display:block;" ><span><b style="background: url('/Content/Themes/images/16/table_sort.png') 50% 4px no-repeat;">移到顶部</b></span></a>
                <a id="btn_Up" title="上移" class="tools_btn" style="display:block;" ><span><b style="background: url('/Content/Themes/images/16/table_sort.png') 50% 4px no-repeat;">上移</b></span></a>
                <a id="btn_Down" title="下移" class="tools_btn" style="display:block;" ><span><b style="background: url('/Content/Themes/images/16/table_sort.png') 50% 4px no-repeat;">下移</b></span></a>
                <a id="btn_Bottom" title="移到底部" class="tools_btn" style="display:block;" ><span><b style="background: url('/Content/Themes/images/16/table_sort.png') 50% 4px no-repeat;">移到底部</b></span></a>
                <a id="btn_Apply" title="应用" class="tools_btn" style="display:block;" ><span><b style="background: url('/Content/Themes/images/16/table_sort.png') 50% 4px no-repeat;">应用</b></span></a>
                
                <a id="btn_Return" title="返回" class="tools_btn" style="display:block;"><span><b style="background: url('/Content/Themes/images/16/back.png') 50% 4px no-repeat;">返回</b></span></a>
                <asp:Button runat="server" Text="" style="display:none;" ID="btnAdd" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnEdit" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnDel" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnSelect" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnSearch" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnExport" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnReturn"/>
                
                <asp:Button runat="server" Text="" style="display:none;" ID="btnTop" OnClick="btnTop_Click" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnUp" OnClick="btnUp_Click" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnDown" OnClick="btnDown_Click" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnBottom" OnClick="btnBottom_Click" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnApply" OnClick="btnApply_Click" />
            </div>
        </div>
        <div class="datagrid-title-panel">
            <div class="datagrid-title">
                <img src="/Content/Themes/Images/32/202323.png" width="25" height="25" style="vertical-align: middle;">
                报表指标排序
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="width:100%">
                    <span style="float: left;">
                        <asp:Label ID="Label1" Width="120px" Font-Bold="true" runat="server" Text="当前选定的集合：" />
                        <asp:Label ID="lblInfor" ForeColor="Red" Font-Bold="true" runat="server" Text="指标集: " />
                        <asp:Label ID="lblInfor2" Width="100px" ForeColor="Red" Font-Bold="true" runat="server" Text="" />
                        <input id="txtIndex" style="width: 0px; visibility: hidden;" runat="server" value="-1" type="text" />
                    </span>
                    
                    <asp:GridView ID="gvData" runat="server" CssClass="table table-hover table-bordered table-condensed datagrid" AutoGenerateColumns="False"  
                        EmptyDataText="没有满足条件的数据" AllowPaging="false" DataKeyNames="KeyID" Width="100%" 
                        OnPageIndexChanging="gvData_PageIndexChanging" OnRowDataBound="gvData_RowDataBound" PageSize="100">
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <input type="hidden" runat="server" id="said" value='<%# Eval("KeyID").ToString()%>' />
                                    <%#  Container.DataItemIndex + 1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ECCode" HeaderText="代码" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ECName" HeaderText="名称" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        
    <div id="Lay1" style="z-index: 100; visibility: hidden; left: 0%; width: 100%; cursor: crosshair;
        position: absolute; top: 0px; height: 100%; background-color: transparent; text-align: center">
        <br />
        <b></b>
        <table align="center" style="width: 260px; height: 40px" class="BoderTable">
            <tr>
                <td>
                    <div>
                        <font color="green" size="2" style="font-weight: bold">正在查询数据库，请稍候...</font></div>
                    <div style="border-right: green 1px solid; padding-right: 2px; border-top: green 1px solid;
                        padding-left: 2px; font-size: 8pt; padding-bottom: 2px; border-left: green 1px solid;
                        padding-top: 2px; border-bottom: green 1px solid">
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
