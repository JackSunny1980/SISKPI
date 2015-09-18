<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_ManagementScore.aspx.cs" Inherits="SISKPI.KPI.KPI_ManagementScore" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>日常管理考核录入</title>
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
    <script src="../js/DatePicker/WdatePicker.js" type="text/javascript" defer="defer"></script>
    <script type="text/javascript">
        function clearUI() {
            var date = new Date();
            var month = date.getMonth() + 1;
            if (month < 10) month = "0" + month;            
            var strDate = date.getFullYear() + "-" + month + "-" + date.getDate();
            $("#txtCheckDate").val(strDate);
            $("#txtDescript").val("");
            $("#txtTagScore").val("0.0");
            var argument = "Action=InputTags,args=" + $("#drpTagCategorys").val();
            <%=ClientCallback %>;
        }
        function getUI() {
            var data = new Object();
            data.CheckDate = $("#txtCheckDate").val();
            data.Descript = $("#txtDescript").val();
            data.TagScore = $("#txtTagScore").val();
            data.TagID = $("#drpTags").val();
            data.TagCategorys = $("#drpTagCategorys").val();
            data.DetailList = new Array();
            var personData;
            $("#PersonGrid tr").each(function (index, item) {
                var Cells = $(this).find("td");
                personData = $(Cells[0]).text() + "," + $(Cells[1]).text()+","+$(Cells[3]).children("input[type='text'").val();
                data.DetailList.push(personData);              
            });
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
                    height: 400,
                    width: 600,
                    modal: true,
                    buttons: {
                        "确定": function () {
                            var data = getUI();
                            data.Action = "SaveInputTagCategory1";
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
                            data.Action = "SaveInputTagCategory1";
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

        function selectPerson() {           
            var sURL = "SelectPersonDialog.aspx?rand=" + Math.random();
            var sFeatures = "dialogHeight:400px;dialogWidth:600px;center:yes;help:no;status:no;rsizable:yes";
            var vArguments = "";
            var urlValue = window.showModalDialog(sURL, vArguments, sFeatures);
        }

        function onSelectedChange() {
            var argument = "Action=InputTags,args=" + $("#drpTagCategorys").val();
            <%=ClientCallback %>;
        }

        function processCallback(result, context) {
            $("#drpTags").empty();
            var result = eval('(' + result + ')');
            var data = result.data;
            var action = result.Action;
            if (action == "InputTags") {
                var options = "";
                for (i = 0; i < data.length; i++) {
                    options = options + "<option value='" + data[i].InputID + "'>" + data[i].InputDesc + "</option>";
                }
                $("#drpTags").append(options);
            }

            if (action == "InitialPerson") {
                var content = "";
                for (i = 0; i < data.length; i++) {
                    content += "<tr>";
                    content += "<td align=\"center\">" + data[i].Shift + "</td>";
                    content += "<td align=\"center\">" + data[i].PersonName + "</td>";
                    content += "<td align=\"center\">" + data[i].PositionName + "</td>";
                    content += "<td align=\"center\">";
                    content += "<input type=\"text\" value=\"" + data[i].Rate + "\" /></td>";
                    content += "<td align=\"center\">";
                    content += "<input type=\"button\" value=\"删除\" class=\"btn btn-default\" /></td>";
                    content += "</tr>";
                }               
                $("#PersonGrid").empty().append(content);
            }
        }
        
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
                日常管理考核录入
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Repeater ID="ManagementScores" runat="server">
                    <HeaderTemplate>
                        <table id="DataGrid" class="datagrid table table-hover table-bordered table-condensed">
                            <thead>
                                <tr>
                                    <th>考核日期
                                    </th>
                                    <th>值别
                                    </th>
                                    <th>工作类别

                                    </th>
                                    <th>工作项目
                                    </th>
                                    <th>工作项得分
                                    </th>
                                    <th>得分系数
                                    </th>
                                    <th>个人得分
                                    </th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr data='{"ConstantID":"<%#Eval("ConstantID") %>","ConstantName":"<%#Eval("ConstantName") %>","ConstantValue": "<%#Eval("ConstantValue")%>"}'>
                            <td>
                                <%#Eval("CheckDate") %>
                            </td>
                            <td>
                                <%#Eval("Shift") %>
                            </td>
                            <td>
                                <%#Eval("Shift") %>
                            </td>
                            <td>
                                <%#Eval("TagName") %>
                            </td>
                            <td>
                                <%#Eval("TagScore") %>
                            </td>
                            <td>
                                <%#Eval("Rate") %>
                            </td>
                            <td>
                                <%#Eval("Score") %>
                            </td>

                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dialog" title="日常管理考核录入" style="display: block;">
           
        </div>
    </form>
</body>
    <script type="text/javascript">
     var argument = "Action=InitialPerson,args=1";
        <%=ClientCallback %>;
     </script>
</html>
