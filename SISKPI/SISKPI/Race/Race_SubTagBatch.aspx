<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Race_SubTagBatch.aspx.cs"
    Inherits="SISKPI.Race_SubTagBatch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISKPI</title>
    <link href="../CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../js/ProcessBar.js" type="text/javascript"></script>
    <script src="../js/Common.js" type="text/javascript"></script>
    <script src="../js/DatePicker/WdatePicker.js" type="text/javascript" defer="defer"></script>
    <script src="../js/GridViewColor.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            call();
        });

        function call() {
            SetTableWidth('table1');
            SetTableWidth('table2');
            SetDivWidth('divtag');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="float: left; width: 100%;">
        <table width="100%" class="table" align="center">
            <tr style="width: 100%">
                <td align="center" style="width: 100%">
                    <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                    <asp:Label ID="lblTitle" runat="server" Class="title" Text="值际竞赛标签批处理"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%">
                <td align="center" style="width: 100%">
                    <div style="width: 90%; text-align: right;">
                        <asp:Button ID="btnReturn" runat="server" Text="返 回" Width="80px" OnClick="btnReturn_Click" />
                    </div>
                </td>
            </tr>
            <tr style="width: 100%;">
                <td align="center" style="width: 100%;">
                    <fieldset style="width: 90%;">
                        <legend>Excel标签点配置</legend>
                        <table width="95%" class="table" align="center">
                            <%--<tr style="width: 100%;">
                                <td align="left" style="width: 100%;">
                                    <span style="float: left;">
                                        <asp:Label ID="Label1" runat="server" Width="100px" Text="电厂属性:"></asp:Label>
                                        <select style="width: 120px" runat="server" id="ddlExcelPlantID">
                                        </select>
                                    </span>
                                </td>
                                </tr>--%>
                            <tr style="width: 100%;">
                                <td align="left" style="width: 100%;">
                                    <span style="float: left;">
                                        <asp:Label ID="Label5" runat="server" Width="100px" Text="机组属性:"></asp:Label>
                                        <select style="width: 120px" runat="server" id="ddlExcelUnitID">
                                        </select>
                                    </span>
                                </td>
                            </tr>
                            <%--<tr style="width: 100%;">
                                <td align="left" style="width: 100%;">
                                    <span style="float: left;">
                                        <asp:Label ID="Label2" runat="server" Width="100px" Text="参数类型:"></asp:Label>
                                    </span><span style="float: left;">
                                        <asp:RadioButtonList ID="rblTagType" Width="300px" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="HOUR" >时报</asp:ListItem>
                                            <asp:ListItem Value="SHIFT" Selected="True">值报</asp:ListItem>
                                            <asp:ListItem Value="MONTH">月报</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </span>
                                </td>
                            </tr>--%>
                            <tr style="width: 100%;">
                                <td style="width: 100%;" align="right">
                                    <asp:Button ID="btnExcelExport" Width="80px" runat="server" Text="导 出" OnClick="btnExcelExport_Click" />
                                </td>
                            </tr>
                            <tr style="width: 100%;">
                                <td style="width: 100%;">
                                    <table class="table" id="table3" width="100%">
                                        <tr style="width: 100%;">
                                            <td align="left" style="width: 100%;">
                                            </td>
                                        </tr>
                                        <tr style="width: 100%;">
                                            <td align="left" style="width: 100%;">
                                                <span style="float: left">
                                                    <asp:Label ID="Label3" runat="server" Width="100px" Text="选择文件:"></asp:Label>
                                                    <asp:FileUpload ID="fuExcel" runat="server" />
                                                </span><span style="float: left">
                                                    <asp:Label ID="Label8" runat="server" Width="60px" Text="输入表单:"></asp:Label>
                                                    <asp:TextBox ID="tbxSheet" runat="server" Text="Sheet1" Width="120px"></asp:TextBox>
                                                    <asp:Label ID="Label4" runat="server" Width="100px" Text="选择配置:"></asp:Label>
                                                    <select style="width: 100px" runat="server" id="ddlExcelMode">
                                                    </select>
                                                </span><span style="float: right">
                                                    <asp:Button ID="btnExcelImport" Width="80px" runat="server" Text="导 入" OnClick="btnExcelImport_Click" />
                                                </span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <%--
            <tr style="width: 100%;">
                <td style="width: 100%;">
                </td>
            </tr>
            <tr style="width: 100%;">
                <td style="width: 100%;">
                    <div style="width: 100%; background-color: Aqua;">
                        <fieldset style="width: 100%">
                            <legend>标签点与实时数据库同步配置</legend>
                            <table width="100%"  class="table"  align="center">
                                <tr>
                                    <td>
                                        <span style="float: left">
                                            <asp:Label ID="Label4" runat="server" Width="100px" Text="机组属性:"></asp:Label>
                                            <select style="width: 120px" runat="server" id="ddlSynUnitID">
                                            </select>
                                        </span><span style="float: right">
                                            <asp:Button ID="btnSynPI" Width="80px" runat="server" Text="同  步" OnClick="btnSynPI_Click" />
                                        </span>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
            </tr>
            <tr style="width: 100%;">
                <td style="width: 100%;">
                </td>
            </tr>
            <tr style="width: 100%; background-color: Orange;">
                <td style="width: 100%;">
                    <div style="width: 100%;">
                        <fieldset style="width: 100%">
                            <legend>数据库标签点删除配置</legend>
                            <table width="100%"  class="table"  align="center">
                                <tr>
                                    <td>
                                        <span style="float: left">
                                            <asp:Label ID="Label5" runat="server" Width="100px" Text="机组属性:"></asp:Label>
                                            <select style="width: 120px" runat="server" id="ddlDelUnitID">
                                            </select>
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span style="float: left">
                                            <asp:Label ID="Label6" runat="server" Width="100px" Text="查询规则:"></asp:Label>
                                            <asp:TextBox ID="txtDelCondition" runat="server" Width="120px"></asp:TextBox>
                                            <asp:Label ID="Label7" runat="server" Width="100px" Text="通配符(% _)"></asp:Label>
                                        </span><span style="float: right">
                                            <asp:Button ID="btnDelApply" runat="server" Width="80px" Text="删  除" OnClick="btnDelApply_Click" />
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblDelInfor" runat="server" Text="点击删除!"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
            </tr>
            --%>
        </table>
    </div>
    <div id="Lay1" style="z-index: 100; visibility: hidden; left: 1%; width: 260px; cursor: crosshair;
        position: absolute; top: -20px; height: 14%; background-color: transparent; text-align: center">
        <br />
        <b></b>
        <table align="center" style="width: 260px; height: 40px" class="BoderTable">
            <tr>
                <td>
                    <div>
                        <font color="#800080" size="2" style="font-weight: bold">配置正在执行，请稍候...</font></div>
                    <div style="border-right: black 1px solid; padding-right: 2px; border-top: black 1px solid;
                        padding-left: 2px; font-size: 8pt; padding-bottom: 2px; border-left: black 1px solid;
                        padding-top: 2px; border-bottom: black 1px solid">
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
