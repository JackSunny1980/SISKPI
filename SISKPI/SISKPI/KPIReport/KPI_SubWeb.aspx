<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_SubWeb.aspx.cs" Inherits="SISKPI.KPI_SubWeb" %>

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

            $('#btn_Sort').click(function () { $('#btnSort').click(); });

            $('#btnAdd').click(function () {
                $('#KeyID').val("");
                $('#WebCode').val($('#lblInfor').html());
                $('#KeyIndex').val("");

                $("#WebCode").removeAttr("disabled");
                $("#ECCode").removeAttr("disabled");
                $("#ECName").removeAttr("disabled");
                $("#KeyEngunit").removeAttr("disabled");
                $("#KeyCalcType").removeAttr("disabled");

                $("#dialog").dialog({
                    resizable: false,
                    height: 320,
                    width: 450,
                    modal: true,
                    buttons: {
                        "确定": function () {
                            var KeyID = $('#KeyID').val();
                            var WebCode = $('#WebCode').val();
                            var ECCode = $('#ECCode').val();
                            var ECName = $('#ECName').val();
                            var KeyEngunit = $('#KeyEngunit').val();
                            var KeyCalcType = $('#KeyCalcType').val();
                            var KeyIndex = $('#KeyIndex').val();
                            $.ajax({
                                type: "POST",
                                url: "KPI_Services.ashx",
                                data: {
                                    Method: "KPI_SubWeb_Add",
                                    KeyID: KeyID,
                                    WebCode: WebCode,
                                    ECCode: ECCode,
                                    ECName: ECName,
                                    KeyEngunit: KeyEngunit,
                                    KeyCalcType: KeyCalcType,
                                    KeyIndex: KeyIndex
                                },
                                success: function (data) {
                                    var data = eval('(' + data + ')');
                                    if (data.status == "ok") {
                                        $('#dialog').dialog("close");
                                        alert("添加成功");
                                        location.href = location.href;
                                    } else {
                                        alert(data.status);
                                    }
                                },
                                error: function (msg) {
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
                    $('#WebCode').val(selectedId.split(',')[1]);
                    $('#ECCode').val(selectedId.split(',')[2]);
                    $('#ECName').val(selectedId.split(',')[3]);
                    $('#KeyEngunit').val(selectedId.split(',')[4]);
                    $('#KeyCalcType').val(selectedId.split(',')[5]);
                    $('#KeyIndex').val(selectedId.split(',')[6]);

                    $("#WebCode").attr("disabled", "disabled");
                    $("#ECCode").attr("disabled", "disabled");
                    $("#ECName").attr("disabled", "disabled");
                    $("#KeyEngunit").attr("disabled", "disabled");
                    $("#KeyCalcType").attr("disabled", "disabled");

                    $("#dialog").dialog({
                        resizable: false,
                        height: 320,
                        width: 450,
                        modal: true,
                        buttons: {
                            "确定": function () {
                                var KeyID = $('#KeyID').val();
                                var WebCode = $('#WebCode').val();
                                var ECCode = $('#ECCode').val();
                                var ECName = $('#ECName').val();
                                var KeyEngunit = $('#KeyEngunit').val();
                                var KeyCalcType = $('#KeyCalcType').val();
                                var KeyIndex = $('#KeyIndex').val();
                                $.ajax({
                                    type: "POST",
                                    url: "KPI_Services.ashx",
                                    data: {
                                        Method: "KPI_SubWeb_Edit",
                                        KeyID: KeyID,
                                        WebCode: WebCode,
                                        ECCode: ECCode,
                                        ECName: ECName,
                                        KeyEngunit: KeyEngunit,
                                        KeyCalcType: KeyCalcType,
                                        KeyIndex: KeyIndex
                                    },
                                    success: function (data) {
                                        var data = eval('(' + data + ')');
                                        if (data.status == "ok") {
                                            $('#dialog').dialog("close");
                                            alert("修改成功");
                                            location.href = location.href;
                                        } else {
                                            alert(data.status);
                                        }
                                    },
                                    error: function (msg) {
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
                            Method: "KPI_SubWeb_Del",
                            KeyID: KeyID
                        },
                        success: function (data) {
                            var data = eval('(' + data + ')');
                            if (data.status == "ok") {
                                alert("删除成功");
                                location.href = location.href;
                            } else {
                                alert(data.status);
                            }
                        },
                        error: function (msg) {
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

            $('#btnReturn').click(function () {
                location.href = "KPI_Web.aspx";
                return false;
            });
            $('#btnSort').click(function () {
                var WebCode = $('#lblInfor').html();
                if (WebCode == "") {
                } else {
                    location.href = "KPI_SubWebSort.aspx?webcode=" + WebCode;
                }
                return false;
            });
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
                <a id="btn_Select" title="查看" class="tools_btn" style="display:none;"><span><b style="background: url('/Content/Themes/images/16/shape_square_select.png') 50% 4px no-repeat;">选中</b></span></a>
                <a id="btn_Search" title="查询" class="tools_btn" style="display:none;"><span><b style="background: url('/Content/Themes/images/16/search.png') 50% 4px no-repeat;">查询</b></span></a>
                <a id="btn_Export" title="导出到Excel" class="tools_btn" style="display:none;"><span><b style="background: url('/Content/Themes/images/16/export.png') 50% 4px no-repeat;">导出</b></span></a>
                
                <a id="btn_Sort" title="参数排序" class="tools_btn" style="display:block;" ><span><b style="background: url('/Content/Themes/images/16/table_sort.png') 50% 4px no-repeat;">参数排序</b></span></a>
                
                <a id="btn_Return" title="返回" class="tools_btn" style="display:block;"><span><b style="background: url('/Content/Themes/images/16/back.png') 50% 4px no-repeat;">返回</b></span></a>
                <asp:Button runat="server" Text="" style="display:none;" ID="btnAdd" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnEdit" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnDel" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnSelect" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnSearch" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnExport" />
                <asp:Button runat="server" Text="" style="display:none;" ID="btnReturn" />

                <asp:Button runat="server" Text="" style="display:none;" ID="btnSort" />
            </div>
        </div>
        <div class="datagrid-title-panel">
            <div class="datagrid-title">
                <img src="/Content/Themes/Images/32/202323.png" width="25" height="25" style="vertical-align: middle;">
                自定义报表管理
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="width:100%">
                    <span style="float: left;width:100%;">
                        <asp:Label ID="Label1" Width="120px" Font-Bold="true" runat="server" Text="当前选定的集合：" />
                        <asp:Label ID="lblInfor" Width="100px" ForeColor="Red" Font-Bold="true" runat="server"
                            Text="XXX" />
                    </span>
                    <span style="float: left;width:100%;"> 1)一张报表使用统一的计算方法；2)统计类型分：0-得分、1-得分率、2-合格率、3-最大值、4-最小值、5-算数平均值、6-加权平均值、7-累计值、8-累计除法。
                    </span>
                    
                    <asp:GridView ID="gvData" CssClass="table table-hover table-bordered table-condensed datagrid" runat="server" AutoGenerateColumns="False"
                        EmptyDataText="没有满足条件的数据" AllowPaging="false" DataKeyNames="KeyID,WebCode,ECCode,ECName,KeyEngunit,KeyCalcType,KeyIndex" Width="100%"
                        OnRowDataBound="gvData_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <input type="hidden" runat="server" id="keyid" value='<%# Eval("KeyID").ToString()%>' />
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ECCode" HeaderText="指标" ReadOnly="true" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ECName" HeaderText="名称" ReadOnly="true" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="KeyEngunit" HeaderText="单位" ReadOnly="true" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="KeyCalcType" HeaderText="统计类型" ReadOnly="true" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="KeyIndex" HeaderText="显示序号" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <div id="dialog" title="编辑" style="display:none;">
            <table id="table2" class="table" style="width: 100%; text-align: center;">
                <tr style="width: 100%;display:none;">
                    <td align="right">
                        序号：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="KeyID" runat="server" Text="" Width="80%"></asp:TextBox>
                    </td>
                </tr>
                <tr style="width: 100%;display:none;">
                    <td align="right">
                        代码：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="WebCode" runat="server" Text="" Width="80%"></asp:TextBox>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td align="right" style="width:100px;">
                        指标：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ECCode" runat="server"  CssClass="select" 
                             DataTextField="Name" DataValueField="Code" Width="80%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td align="right">
                        名称：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="ECName" runat="server" Text="" Width="80%" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td align="right">
                        单位：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="KeyEngunit" runat="server" Text="" Width="80%"></asp:TextBox>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td align="right">
                        统计类型：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="KeyCalcType" runat="server" CssClass="select" 
                             DataTextField="Name" DataValueField="Value" Width="80%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td align="right">
                        显示序号：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="KeyIndex" runat="server" Text="" Width="80%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
