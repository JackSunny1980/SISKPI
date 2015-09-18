<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_Average.aspx.cs" Inherits="SISKPI.KPI_Average" %>

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
                $('#KeyID').val();
                $('#KeyTarget1').val("");
                $('#KeyTarget2').val("");
                $('#KeyDesign').val("");
                $('#KeyDiffMoney').val("");
                $('#KeyOptMoney').val("");
                $('#KeyIndex').val("");

                $("#ECCode").removeAttr("disabled");
                $("#ECName").removeAttr("disabled");
                $("#KeyEngunit").removeAttr("disabled");
                $("#KeyTarget1").removeAttr("disabled");
                $("#KeyTarget2").removeAttr("disabled");

                $("#dialog").dialog({
                    resizable: false,
                    height: 400,
                    width: 800,
                    modal: true,
                    buttons: {
                        "确定": function () {
                            var ECCode = $('#ECCode').val();
                            var ECName = $('#ECName').val();
                            var KeyEngunit = $('#KeyEngunit').val();
                            var KeyTarget1 = $('#KeyTarget1').val();
                            var KeyTarget2 = $('#KeyTarget2').val();
                            var KeyDesign = $('#KeyDesign').val();
                            var KeyDiffMoney = $('#KeyDiffMoney').val();
                            var KeyOptMoney = $('#KeyOptMoney').val();
                            var KeyIndex = $('#KeyIndex').val();
                            $.ajax({
                                type: "POST",
                                url: "KPI_Services.ashx",
                                data: {
                                    Method: "KPI_Average_Add",
                                    ECCode: ECCode,
                                    ECName: ECName,
                                    KeyEngunit: KeyEngunit,
                                    KeyTarget1: KeyTarget1,
                                    KeyTarget2: KeyTarget2,
                                    KeyDesign: KeyDesign,
                                    KeyDiffMoney: KeyDiffMoney,
                                    KeyOptMoney: KeyOptMoney,
                                    KeyIndex: KeyIndex
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
                    $('#KeyID').val(selectedId.split(',')[0]);
                    $('#ECCode').val(selectedId.split(',')[1]);
                    $('#ECName').val(selectedId.split(',')[2]);
                    $('#KeyEngunit').val(selectedId.split(',')[3]);
                    $('#KeyTarget1').val(selectedId.split(',')[4]);
                    $('#KeyTarget2').val(selectedId.split(',')[5]);
                    $('#KeyDesign').val(selectedId.split(',')[6]);
                    $('#KeyDiffMoney').val(selectedId.split(',')[7]);
                    $('#KeyOptMoney').val(selectedId.split(',')[8]);
                    $('#KeyIndex').val(selectedId.split(',')[9]);

                    $("#ECCode").attr("disabled", "disabled");
                    $("#ECName").attr("disabled", "disabled");
                    $("#KeyEngunit").attr("disabled", "disabled");
                    $("#KeyTarget1").attr("disabled", "disabled");
                    $("#KeyTarget2").attr("disabled", "disabled");

                    $("#dialog").dialog({
                        resizable: false,
                        height: 400,
                        width: 800,
                        modal: true,
                        buttons: {
                            "确定": function () {
                                var KeyID = $('#KeyID').val();
                                var ECCode = $('#ECCode').val();
                                var ECName = $('#ECName').val();
                                var KeyEngunit = $('#KeyEngunit').val();
                                var KeyTarget1 = $('#KeyTarget1').val();
                                var KeyTarget2 = $('#KeyTarget2').val();
                                var KeyDesign = $('#KeyDesign').val();
                                var KeyDiffMoney = $('#KeyDiffMoney').val();
                                var KeyOptMoney = $('#KeyOptMoney').val();
                                var KeyIndex = $('#KeyIndex').val();
                                $.ajax({
                                    type: "POST",
                                    url: "KPI_Services.ashx",
                                    data: {
                                        Method: "KPI_Average_Edit",
                                        KeyID: KeyID,
                                        ECCode: ECCode,
                                        ECName: ECName,
                                        KeyEngunit: KeyEngunit,
                                        KeyTarget1: KeyTarget1,
                                        KeyTarget2: KeyTarget2,
                                        KeyDesign: KeyDesign,
                                        KeyDiffMoney: KeyDiffMoney,
                                        KeyOptMoney: KeyOptMoney,
                                        KeyIndex: KeyIndex
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
                if (confirm("是否删除？") == false) {
                    return false;
                }
                var selectedId = $('#hidSelectedItem').val();
                if (selectedId == "0") {
                } else {
                    var KeyID = selectedId.split(',')[0];
                    $.ajax({
                        type: "POST",
                        url: "KPI_Services.ashx",
                        data: {
                            Method: "KPI_Average_Del",
                            KeyID: KeyID
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

            $('#ECCode').change(function () {
                var ECCode = $('#ECCode').val();
                $.ajax({
                    type: "POST",
                    url: "KPI_Services.ashx",
                    data: {
                        Method: "KPI_SubWeb_GetECNameByECCode",
                        ECCode: ECCode
                    },
                    success: function (data) {
                        var data = eval('(' + data + ')');
                        if (data.status == "ok") {
                            $('#ECName').val(data.ECName);
                            $('#KeyEngunit').val(data.KeyEngunit);
                            //alert("删除成功");
                            //location.href = location.href;
                        } else {
                            alert(data.status);
                        }
                    },
                    error: function (msg) {
                    }
                });
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
                <a id="btn_Select" title="查看" class="tools_btn" style="display:none;"><span><b style="background: url('/Content/Themes/images/16/shape_square_select.png') 50% 4px no-repeat;">查看</b></span></a>
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
                        EmptyDataText="没有满足条件的数据" AllowPaging="false" DataKeyNames="KeyID,ECCode,ECName,KeyEngunit,KeyTarget1,KeyTarget2,KeyDesign,KeyDiffMoney,KeyOptMoney,KeyIndex" Width="100%"
                        OnRowDataBound="gvData_RowDataBound" >
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <%#  Container.DataItemIndex + 1%>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ECCode" HeaderText="指标" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ECName" HeaderText="名称" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="KeyEngunit" HeaderText="单位" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="KeyTarget1" HeaderText="目标值1" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="KeyTarget2" HeaderText="目标值2" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="keyDesign" HeaderText="设计值" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="keyDiffMoney" HeaderText="偏差奖金" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="KeyOptMoney" HeaderText="最优区间奖金" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="KeyIndex" HeaderText="序号" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        
        <div id="dialog" title="编辑" style="display:none;">
            <table id="table2" class="table" style="width: 100%; text-align: center;">
                <tr>
                    <td colspan="4">
                        自定义报表数据源设置
                    </td>
                </tr>
                <tr style="width: 100%;display:none;">
                    <td align="right">
                        序号：
                    </td>
                    <td align="left" colspan="3">
                        <asp:TextBox ID="KeyID" runat="server" Text="" Width="80%"></asp:TextBox>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td align="right">
                        指标：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ECCode" runat="server" CssClass="select" 
                             DataTextField="Name" DataValueField="Code" Width="80%">
                        </asp:DropDownList>
                    </td>
                    <td colspan="2"></td>
                </tr>
                <tr style="width: 100%">
                    <td align="right" style="width:120px;">
                        名称：
                    </td>
                    <td align="left" style="width:280px;">
                        <asp:TextBox ID="ECName" runat="server" Text="" Width="80%" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td align="right" style="width:120px;">
                        目标值1：
                    </td>
                    <td align="left" style="width:280px;">
                        <asp:DropDownList ID="KeyTarget1" runat="server" CssClass="select" Width="80%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td align="right">
                        单位：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="KeyEngunit" runat="server" Text="" Width="80%"></asp:TextBox>
                    </td>
                    <td align="right">
                        目标值2：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="KeyTarget2" runat="server" CssClass="select" Width="80%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td align="right">
                        设计值：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="KeyDesign" runat="server" Text="" Width="80%"></asp:TextBox>
                    </td>
                    <td align="right">
                        偏差奖金：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="KeyDiffMoney" runat="server" Text="" Width="80%"></asp:TextBox>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td align="right">
                        序号：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="KeyIndex" runat="server" Text="" Width="80%"></asp:TextBox>
                    </td>
                    <td align="right">
                        最优区间奖金：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="KeyOptMoney" runat="server" Text="" Width="80%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
