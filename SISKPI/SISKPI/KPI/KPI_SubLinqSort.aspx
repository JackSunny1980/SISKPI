<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_SubLinqSort.aspx.cs"
    Inherits="SISKPI.KPI_SubLinqSort" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISOPM</title>
    <link href="../CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <script src="../js/ProcessBar.js" type="text/javascript"></script>
    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../js/Common.js" type="text/javascript"></script>
    <script src="../js/tab/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="../js/Tab/jquery.tabs.js" type="text/javascript"></script>
    <script src="../js/Config.js" type="text/javascript"></script>
    <script src="../js/GridViewColor.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            call();
        });

        function call() {
            SetTableWidth('table1');
            SetDivWidth('div1');
            SetDivWidth('divtag');
            SetTableWidth('table2');
            //SetTableWidth('table3');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 0px; width: 100%; text-align: center;">
        <table width="100%" class="BoderTable" align="center">
            <tr style="width: 100%">
                <td style="width: 100%" align="center">
                    <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                    <asp:Label ID="lblTitle" runat="server" Class="title" Text="SIS报表指标排序"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 100%" align="center">
                    <div style="width: 60%">
                        <table width="100%" class="BoderTable" align="center">
                            <tr style="width: 100%">
                                <td style="width: 100%" align="right">
                                    <asp:Button ID="btnClose" runat="server" Width="60px" Text="关 闭" OnClick="btnClose_Click" />
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td align="right">
                                    <span style="float: left">
                                        <asp:Label ID="lblUnit" runat="server" Text="所属电厂:"></asp:Label>
                                        <select style="width: 120px" runat="server" id="ddl_PlantID" name="ddlUnit">
                                        </select>
                                        <asp:Label ID="Label1" runat="server" Text="类型:"></asp:Label>
                                        <select runat="server" id="ddl_ZBCate" style="width: 120px">
                                            <option value="0">重要</option>
                                            <option value="1">机组</option>
                                            <option value="2">锅炉</option>
                                            <option value="3">汽机</option>
                                            <option value="4">电气</option>
                                            <option value="5">化学</option>
                                            <option value="6">其它1</option>
                                            <option value="7">其它2</option>
                                            <option value="8">其它3</option>
                                        </select>
                                    </span><span style="float: right">
                                        <asp:Button ID="btnQuery" runat="server" Width="60px" Text="查 询" OnClick="btnQuery_Click" />
                                    </span>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td align="right">
                                    <span style="float: left">
                                        <asp:Button ID="btnTop" runat="server" Width="60px" Text="移到顶部" OnClick="btnTop_Click" />
                                        <asp:Button ID="btnUp" runat="server" Width="60px" Text="上移" OnClick="btnUp_Click" />
                                        <asp:Button ID="btnDown" runat="server" Width="60px" Text="下移" OnClick="btnDown_Click" />
                                        <asp:Button ID="btnBottom" runat="server" Width="60px" Text="移到底部" OnClick="btnBottom_Click" />
                                    </span><span style="float: right">
                                        <input id="txtIndex" style="width: 0px; visibility: hidden;" runat="server" value="-1"
                                            type="text" />
                                        <asp:Button ID="btnApply" runat="server" Width="60px" Text="应用" OnClick="btnApply_Click" />
                                    </span>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td>
                                    <asp:GridView ID="gvCS" CssClass="GridViewStyle" Width="100%" runat="server" AutoGenerateColumns="False"
                                        EmptyDataText="没有满足条件的数据" AllowPaging="False" OnPageIndexChanging="gvCS_PageIndexChanging"
                                        OnRowDataBound="gvCS_RowDataBound" PageSize="100">
                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                        <RowStyle CssClass="GridViewRowStyle" />
                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                        <PagerStyle CssClass="GridViewPagerStyle" />
                                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号">
                                                <ItemTemplate>
                                                    <input type="hidden" runat="server" id="CSIndex" value='<%# Eval("ZBID").ToString()%>' />
                                                    <%#  Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ZBName" HeaderText="名称">
                                                <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="描述">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesc" runat="server" ToolTip='<%# Eval("ZBDesc").ToString()%>'
                                                        Text='<%# Eval("ZBDesc").ToString().Length > 10 ? Eval("ZBDesc").ToString().Substring(0,10)+".." : Eval("ZBDesc").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
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
                        <font color="#800080" size="2" style="font-weight: bold">操作正在执行，请稍候...</font></div>
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
