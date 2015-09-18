<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="KPI_Web.aspx.cs" Inherits="SISKPI.KPI_Web" %>

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

	<link href="../Scripts/jquery-ui-1.11.1/jquery-ui.css" rel="stylesheet">
    <script src="../Scripts/jquery-ui-1.11.1/jquery-ui.js" type="text/javascript"></script>
    
    
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnAdd').click(function () {
                $('#WebID').val("");
                $('#WebCode').val("");
                $("#WebCode").removeAttr("disabled");
                $('#WebDesc').val("");
                $('#WebNote').val("");
                $("#dialog").dialog({
                    resizable: false,
                    height: 300,
                    width: 450,
                    modal: true,
                    buttons: {
                        "确定": function () {
                            var WebID = $('#WebID').val();
                            var WebCode = $('#WebCode').val();
                            var WebDesc = $('#WebDesc').val();
                            var WebType = $('#WebType').val();
                            var WebNote = $('#WebNote').val();
                            $.ajax({
                                type: "POST",
                                url: "KPI_Services.ashx",
                                data: {
                                    Method: "KPI_Web_Add",
                                    WebID: WebID,
                                    WebCode: WebCode,
                                    WebDesc: WebDesc,
                                    WebType: WebType,
                                    WebNote: WebNote
                                },
                                success: function (data) {
                                    var data = eval('(' + data + ')');
                                    if (data.status == "ok") {
                                        $('#dialog').dialog("close");
                                        alert("添加成功");
                                        location.href = location.href;
                                        //$('#btnRefresh').click();
                                    } else {
                                        alert(data.status);
                                    }
                                },
                                error: function (msg) {
                                    // alert("获取数据失败！");
                                }
                            });
                            return false;
                        },
                        "取消": function () {
                            $(this).dialog("close");
                        }
                    }
                });
                return false;
            });
            $('#btnEdit').click(function () {
                var selectedId = $('#hidSelectedItem').val();
                if (selectedId == "0") {
                } else {
                    $('#WebID').val(selectedId.split(',')[0]);
                    $('#WebCode').val(selectedId.split(',')[1]);
                    $("#WebCode").attr("disabled", "disabled");
                    $('#WebDesc').val(selectedId.split(',')[2]);
                    $('#WebType').val(selectedId.split(',')[3]);
                    $('#WebNote').val(selectedId.split(',')[4]);
                    $("#dialog").dialog({
                        resizable: false,
                        height: 300,
                        width: 450,
                        modal: true,
                        buttons: {
                            "确定": function () {
                                var WebID = $('#WebID').val();
                                var WebCode = $('#WebCode').val();
                                var WebDesc = $('#WebDesc').val();
                                var WebType = $('#WebType').val();
                                var WebNote = $('#WebNote').val();
                                $.ajax({
                                    type: "POST",
                                    url: "KPI_Services.ashx",
                                    data: {
                                        Method: "KPI_Web_Edit",
                                        WebID: WebID,
                                        WebCode: WebCode,
                                        WebDesc: WebDesc,
                                        WebType: WebType,
                                        WebNote: WebNote
                                    },
                                    success: function (data) {
                                        var data = eval('(' + data + ')');
                                        if (data.status == "ok") {
                                            $('#dialog').dialog("close");
                                            alert("修改成功");
                                            location.href = location.href;
                                            //$('#btnRefresh').click();
                                        } else {
                                            alert(data.status);
                                        }
                                    },
                                    error: function (msg) {
                                        // alert("获取数据失败！");
                                    }
                                });
                                return false;
                            },
                            "取消": function () {
                                $(this).dialog("close");
                            }
                        }
                    });
                }
                return false;
            });
            $('#btnDel').click(function () {
                if (confirm("是否删除？")==false) {
                    return false;
                }
                var selectedId = $('#hidSelectedItem').val();
                if (selectedId == "0") {
                } else {
                    var WebCode = selectedId.split(',')[1];
                    $.ajax({
                        type: "POST",
                        url: "KPI_Services.ashx",
                        data: {
                            Method: "KPI_Web_Del",
                            WebCode: WebCode
                        },
                        success: function (data) {
                            var data = eval('(' + data + ')');
                            if (data.status == "ok") {
                                alert("删除成功");
                                location.href = location.href;
                                //$('#btnRefresh').click();
                            } else {
                                alert(data.status);
                            }
                        },
                        error: function (msg) {
                            // alert("获取数据失败！");
                        }
                    });
                }
                return false;
            });
            $('#btnSelect').click(function () {
                var selectedId = $('#hidSelectedItem').val();
                if (selectedId == "0") {
                } else {
                    var WebCode = selectedId.split(',')[1];
                    location.href = "KPI_SubWeb.aspx?webcode=" + WebCode;
                }
                return false;
            });
            //$('#btnRefresh').click();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <input id="hidSelectedItem" type="hidden" value="0" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="header">
            <div class="tools_bar">
                <a id="btn_Add" title="新增" class="tools_btn" style="display:block;"><span><b style="background: url('/Content/Themes/images/16/Add.png') 50% 4px no-repeat;">新增</b></span></a>
                <a id="btn_Edit" title="编辑" class="tools_btn" style="display:block;"><span><b style="background: url('/Content/Themes/images/16/bullet_edit.png') 50% 4px no-repeat;">编辑</b></span></a>
                <a id="btn_Del" title="删除" class="tools_btn" style="display:block;"><span><b style="background: url('/Content/Themes/images/16/DeleteRed.png') 50% 4px no-repeat;">删除</b></span></a>
                <a id="btn_Select" title="查看" class="tools_btn" style="display:block;"><span><b style="background: url('/Content/Themes/images/16/shape_square_select.png') 50% 4px no-repeat;">查看</b></span></a>
                <a id="btn_Search" title="查询" class="tools_btn" style="display:none;"><span><b style="background: url('/Content/Themes/images/16/search.png') 50% 4px no-repeat;">查询</b></span></a>
                <a id="btn_Export" title="导出到Excel" class="tools_btn" style="display:none;"><span><b style="background: url('/Content/Themes/images/16/export.png') 50% 4px no-repeat;">导出</b></span></a>
                <a id="btn_Return" title="返回" class="tools_btn" style="display:none;"><span><b style="background: url('/Content/Themes/images/16/back.png') 50% 4px no-repeat;">返回</b></span></a>
                <asp:Button runat="server" Text="" style="display:none;" ID="btnAdd" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnEdit" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnDel" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnSelect" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnSearch" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnExport" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnReturn"/>
                <asp:Button runat="server" Text="" style="display:none;" ID="btnRefresh" OnClick="btnRefresh_Click"/>
            </div>
        </div>
        <div class="datagrid-title-panel">
            <div class="datagrid-title">
                <img src="/Content/Themes/Images/32/202323.png" width="25" height="25" style="vertical-align: middle;">
                自定义报表管理
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="width:100%">
                    <asp:GridView ID="gvData" CssClass="table table-hover table-bordered table-condensed datagrid" runat="server" AutoGenerateColumns="False"
                        EmptyDataText="没有满足条件的数据" AllowPaging="false" DataKeyNames="WebID,WebCode,WebDesc,WebType,WebNote" Width="100%"
                        OnRowDataBound="gvData_RowDataBound" >
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <input type="hidden" runat="server" id="webid" value='<%# Eval("WebID").ToString()%>' />
                                    <%#  Container.DataItemIndex + 1%>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="WebCode" HeaderText="代码" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="WebDesc" HeaderText="描述" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="WebType" HeaderText="类型" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="WebNote" HeaderText="备注" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

        <div id="dialog" title="编辑" style="display:none;">
            <table id="table1" class="table" style="width: 100%; text-align: center;">
                <tr style="width: 100%;display:none;">
                    <td align="right">
                        序号：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="WebID" runat="server" Text="" Width="80%"></asp:TextBox>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td align="right" style="width:100px;">
                        代码：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="WebCode" runat="server" Text="" Width="80%"></asp:TextBox>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td align="right">
                        描述：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="WebDesc" runat="server" Text="" Width="80%"></asp:TextBox>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td align="right">
                        类型：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="WebType" runat="server" CssClass="select" 
                                DataTextField="Name" DataValueField="Value" Width="80%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td align="right">
                        备注：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="WebNote" runat="server" Text="" Width="80%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
