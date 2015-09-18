<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_InputTagCategory.aspx.cs" Inherits="SISKPI.KPI.KPI_InputTagCategory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>日常管理考核指标类别维护</title>
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
        function clearUI() {
            $("#hConstantID").val("");
            $("#hConstantCode").val("");
            $("#txtConstantName").val("");           
        }
        function getUI() {
            var data = new Object();
            data.ConstantID = $("#hConstantID").val();
            data.ConstantCode = $("#hConstantCode").val();
            data.ConstantName = $("#txtConstantName").val();
            return data;
        }

        function setUI(data) {
            $("#hConstantID").val(data.ConstantID);
            $("#hConstantCode").val(data.ConstantCode);
            $("#txtConstantName").val(data.ConstantName);
        }

        function successCallback(data) {
            if (data.Status == "ok") {
                $('#dialog').dialog("close");
                alert(data.Message);
                location.href = location.href;
            } else {
                alert(data.Message);
            }
        }

        $(document).ready(function () {
            var url = "Services.ashx";
            $('#btn_Add').click(function () {
                clearUI();
                $("#dialog").dialog({
                    resizable: false,
                    height: 200,
                    width: 400,
                    modal: true,
                    buttons: {
                        "确定": function () {
                            var data = getUI();
                            data.Action = "SaveInputTagCategory";
                            $.post(url, data, successCallback, "json");
                        },
                        "取消": function () {
                            $(this).dialog("close");
                        }
                    }
                });
            });

            $('#btn_Edit').click(function () {
                var hSelectedValue = $('#hSelectedValue').val();
                if (hSelectedValue == "") return;
                var data = eval('(' + hSelectedValue + ')');
                setUI(data);
                $("#dialog").dialog({
                    resizable: false,
                    height: 200,
                    width: 400,
                    modal: true,
                    buttons: {
                        "确定": function () {
                            var data = getUI();
                            data.Action = "SaveInputTagCategory";
                            $.post(url, data, successCallback, "json");
                        },
                        "取消": function () {
                            $(this).dialog("close");
                        }
                    }
                });
            });

            $("#btn_Del").click(function (event) {
                var hSelectedValue = $('#hSelectedValue').val();
                if (hSelectedValue == "") return;
                var data = eval('(' + hSelectedValue + ')');
                data.Action = "DeleteInputTagCategory";
                $.post(url, data, function (data) {
                    alert(data.Message);
                    location.href = location.href;
                }, "json");
            });

            $("#DataGrid tr").click(function () {
                $("#DataGrid tr").removeClass("datagrid-row-select");
                $(this).addClass("datagrid-row-select");
                $('#hSelectedValue').val($(this).attr("data"));
            });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <input id="hSelectedValue" type="hidden" value="" />
        <input id="hConstantCode" type="hidden" value="" />
        <input id="hConstantID" type="hidden" value="" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="header">
            <div class="tools_bar">
                <a id="btn_Add" title="新增" class="tools_btn" style="display: block;"><span><b style="background: url('/Content/Themes/images/16/Add.png') 50% 4px no-repeat;">新增</b></span></a> <a id="btn_Edit" title="编辑" class="tools_btn" style="display: block;">
                    <span><b style="background: url('/Content/Themes/images/16/bullet_edit.png') 50% 4px no-repeat;">编辑</b></span></a> <a id="btn_Del" title="删除" class="tools_btn" style="display: block;">
                        <span><b style="background: url('/Content/Themes/images/16/DeleteRed.png') 50% 4px no-repeat;">删除</b></span></a> <a id="btn_Select" title="查看" class="tools_btn" style="display: none;">
                            <span><b style="background: url('/Content/Themes/images/16/shape_square_select.png') 50% 4px no-repeat;">查看</b></span></a> <a id="btn_Search" title="查询" class="tools_btn" style="display: none;">
                                <span><b style="background: url('/Content/Themes/images/16/search.png') 50% 4px no-repeat;">查询</b></span></a> <a id="btn_Export" title="导出到Excel" class="tools_btn" style="display: none;">
                                    <span><b style="background: url('/Content/Themes/images/16/export.png') 50% 4px no-repeat;">导出</b></span></a> <a id="btn_Return" title="返回" class="tools_btn" style="display: none;">
                                        <span><b style="background: url('/Content/Themes/images/16/back.png') 50% 4px no-repeat;">返回</b></span></a>
            </div>
        </div>
        <div class="datagrid-title-panel">
            <div class="datagrid-title">
                <img src="/Content/Themes/Images/32/202323.png" width="25" height="25" style="vertical-align: middle;" />
                日常管理考核指标类别维护
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Repeater ID="TagCategorys" runat="server">
                    <HeaderTemplate>
                        <table id="DataGrid" class="datagrid table table-hover table-bordered table-condensed">
                            <thead>
                                <tr>                                    
                                    <th>指标类别
                                    </th>                                   
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr data='{"ConstantID":"<%#Eval("ConstantID") %>","ConstantName":"<%#Eval("ConstantName") %>","ConstantValue": "<%#Eval("ConstantValue")%>"}'>
                            <td>
                                <%#Eval("ConstantName") %>
                            </td>                                                    
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>               
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dialog" title="日常管理考核指标类别维护" style="display: none;">
            <table id="table2" class="table table-hover table-bordered table-condensed">
                <tr>
                    <td>指标类别
                    </td>
                    <td>
                        <asp:TextBox ID="txtConstantName" runat="server" Width="99%" />
                    </td>
                </tr>                
            </table>
        </div>
    </form>
</body>
</html>
