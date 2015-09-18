<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_InputTagConfig.aspx.cs"
    Inherits="SISKPI.KPI_InputTagConfig" EnableEventValidation="false" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <title>手动录入指标管理</title>
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
            $("#hInputID").val("");
            $("#txtInputCode").val("");
            $("#txtInputDesc").val("");
            $("#txtInputEngunit").val("");
            $("#drpInputType").val("");
        }
        function getUI() {
            var data = new Object();
            data.InputID = $("#hInputID").val();
            data.InputCode = $("#txtInputCode").val();
            data.InputDesc = $("#txtInputDesc").val();
            data.InputEngunit = $("#txtInputEngunit").val();
            data.InputType = $("#drpInputType").val();
            return data;
        }
        function setUI(data) {
            $("#hInputID").val(data.InputID);
            $("#txtInputCode").val(data.InputCode);
            $("#txtInputDesc").val(data.InputDesc);
            $("#txtInputEngunit").val(data.InputEngunit);
            $("#drpInputType").val(data.InputType);
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
                    height: 300,
                    width: 600,
                    modal: true,
                    buttons: {
                        "确定": function () {
                            var data = getUI();
                            data.Action = "SaveInputTag";
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
                    height: 300,
                    width: 600,
                    modal: true,
                    buttons: {
                        "确定": function () {
                            var data = getUI();
                            data.Action = "SaveInputTag";
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
                data.Action = "DeleteInputTag";
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
        <input id="hInputID" type="hidden" value="" />
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

            <div class="btnbarcontetn" style="margin-top: 1px; background: #fff">
                <div>
                    <table border="0" class="frm-find" style="height: 45px;">
                        <tbody>
                            <tr>
                                <th>指标类别：
                                </th>
                                <td>
                                    <asp:DropDownList ID="drpTagCategorys" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="btnSearch" CssClass="btnSearch" Text="检索" runat="server" OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="datagrid-title-panel">
            <div class="datagrid-title">
                <img src="/Content/Themes/Images/32/202323.png" width="25" height="25" style="vertical-align: middle;" />
                手动录入指标管理
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>


                <asp:Repeater ID="Tags" runat="server">
                    <HeaderTemplate>
                        <table id="DataGrid" class="datagrid table table-hover table-bordered table-condensed">
                            <thead>
                                <tr>
                                    <th>指标代码
                                    </th>
                                    <th>指标名称
                                    </th>
                                    <th>指标单位
                                    </th>
                                    <th>指标类别
                                    </th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr data='{"InputID":"<%#Eval("InputID") %>","InputCode":"<%#Eval("InputCode") %>","InputDesc": "<%#Eval("InputDesc")%>", "InputEngunit":"<%#Eval("InputEngunit")%>",InputType:<%#Eval("InputType")%>,
"InputIndex":<%#Eval("InputIndex")%>}'>
                            <td>
                                <%#Eval("InputCode") %>
                            </td>
                            <td>
                                <%#Eval("InputDesc")%>
                            </td>
                            <td>
                                <%#Eval("InputEngunit")%>
                            </td>
                            <td>
                                <%#Eval("TagCategory")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:AspNetPager ID="Pager" runat="server" PageAlign="center" PageIndexBox="DropDownList"
                        OnPageChanged="Pager_PageChanged" ButtonImageNameExtension="enable/" CustomInfoTextAlign="Center"
                        DisabledButtonImageNameExtension="disable/" HorizontalAlign="Center" ImagePath="~/images/"
                        MoreButtonType="Text" NavigationButtonType="Image" NumericButtonType="Text" PagingButtonType="Image"
                        AlwaysShow="True" PagingButtonSpacing="8px" NumericButtonCount="5" EnableTheming="True"
                        PageSize="20">
                    </asp:AspNetPager>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            </Triggers>
        </asp:UpdatePanel>
        <div id="dialog" title="手动录入指标管理" style="display: none;">
            <table id="table2" class="table table-hover table-bordered table-condensed">
                <tr>
                    <td>指标代码
                    </td>
                    <td>
                        <asp:TextBox ID="txtInputCode" runat="server" Width="99%" />
                    </td>
                </tr>
                <tr>
                    <td>指标名称
                    </td>
                    <td>
                        <asp:TextBox ID="txtInputDesc" runat="server" Width="99%" />
                    </td>
                </tr>
                <tr>
                    <td>指标单位
                    </td>
                    <td>
                        <asp:TextBox ID="txtInputEngunit" runat="server" Width="99%" />
                    </td>
                </tr>
                <tr>
                    <td>指标类别
                    </td>
                    <td>
                        <asp:DropDownList ID="drpInputType" runat="server" Width="99%" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
