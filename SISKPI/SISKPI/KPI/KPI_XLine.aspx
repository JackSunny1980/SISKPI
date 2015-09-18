<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_XLine.aspx.cs" Inherits="SISKPI.KPI_XLine" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISKPI</title>
    <link href="../CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../js/Common.js" type="text/javascript"></script>
    <%--    <script src="../js/DatePicker/WdatePicker.js" type="text/javascript" defer="defer"></script>--%>
    <script src="../js/GridViewColor.js" type="text/javascript"></script>
    <script src="../js/ProcessBar.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            call();
        });

        function call() {
            SetTableWidth('table1');
            SetDivWidth('divtag');
            SetDivWidth('div1');
            SetTableWidth('table2');
        }


        function XLineQuery(xlineid) {

            var iWidth = 700; //模态窗口宽度
            var iHeight = 500; //模态窗口高度
            var iTop = (window.screen.height - iHeight) / 2;
            var iLeft = (window.screen.width - iWidth) / 2;

            //更新机组，keyid="........"
            window.open("OPM_SubXLines.aspx?xlineid=" + xlineid, "newwindow", "Width=" + iWidth + " ,Height=" + iHeight + ",top=" + iTop + ",left=" + iLeft);
        }

        function XLineConfig(xlineid) {
            //配置 稳态，keyid="........", type=2
            window.location.href = "OPM_SubXConfig.aspx?opr=edit&xlineid=" + xlineid;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%; text-align: left;">
        <table width="100%" align="center" class="table" id="table1">
            <tr style="width: 100%" class="table_tr">
                <td valign="middle" align="center" style="width: 100%; height: 40px;">
                    <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                    <asp:Label ID="Label2" runat="server" Class="title" Text="SIS系统曲线信息"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%" class="table_tr">
                <td style="width: 100%">
                    <table class="tableinner">
                        <tr style="width: 100%">
                            <td style="width: 10%" align="left">
                                <asp:Label ID="Label1" runat="server" Text="曲线名称"></asp:Label>
                            </td>
                            <td style="width: 20%" align="left">
                                <asp:TextBox ID="txt_XLineName" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 10%" align="left">
                                <asp:Button ID="btnSearch" Width="100px" runat="server" Text="查 询" OnClick="btnSearch_Click" />
                            </td>
                            <td style="width: 30%" align="right">
                                <asp:Button ID="btnAddXline" Width="100px" runat="server" Text="添加曲线" OnClick="btnAddXline_Click" />
                            </td>
                            <td style="width: 30%" align="right">
                                <asp:Button ID="btnBatchPro" runat="server" Text="批处理曲线" Width="100px" OnClick="btnBatchPro_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="width: 100%" class="table_tr">
                <td style="width: 100%" align="center">
                    <asp:GridView ID="gvXLine" CssClass="GridViewStyle" Width="95%" runat="server" AutoGenerateColumns="False"
                        EmptyDataText="没有满足条件的数据" OnRowCommand="gvXLine_RowCommand" OnRowDataBound="gvXLine_RowDataBound"
                        OnRowCancelingEdit="gvXLine_RowCancelingEdit" OnRowEditing="gvXLine_RowEditing"
                        OnRowUpdating="gvXLine_RowUpdating">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <input type="hidden" runat="server" id="xlineid" value='<%# Eval("XLineID").ToString()%>' />
                                    <%#  Container.DataItemIndex + 1%>
                                </ItemTemplate>
                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="XLineName" HeaderText="名称" HeaderStyle-Width="160px" />
                            <asp:BoundField DataField="XLineDesc" HeaderText="描述" HeaderStyle-Width="100px" />
                            <asp:BoundField DataField="XLineEngunit" HeaderText="单位" HeaderStyle-Width="40px" />
                            <asp:BoundField DataField="XLineNote" HeaderText="备注" HeaderStyle-Width="100px" />
                            <asp:TemplateField HeaderText="配置" HeaderStyle-Width="60px">
                                <ItemTemplate>
                                    <input class="GridViewButtonStyle" type="button" runat="server" id="lbquery" value='浏览' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="配置" HeaderStyle-Width="60px">
                                <ItemTemplate>
                                    <input class="GridViewButtonStyle" type="button" runat="server" id="lbconfig" value='配置' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="配置" ShowEditButton="True" HeaderStyle-Width="60px"
                                EditText="编辑" />
                            <asp:TemplateField HeaderText="配置" HeaderStyle-Width="60px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbdelete" runat="server" Text="删除" CommandName="delete" CommandArgument='<%# Eval("XLineID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <div id="Lay1" style="z-index: 100; visibility: hidden; left: 1%; width: 260px; cursor: crosshair;
        position: absolute; top: -20px; height: 14%; background-color: transparent; text-align: center">
        <br />
        <b></b>
        <table align="center" style="width: 260px; height: 45px" class="table">
            <tr>
                <td>
                    <div>
                        <font color="#800080" size="2" style="font-weight: bold">配置正在执行，请稍候...e="font-weight:
                            bold">配置正在执行，请稍候...</font></div>
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
                        <span id="progress31">&nbsp;</span> <span id="progress32">&nbsp;</span> <span id="progress33">
                            &nbsp;</span> <span id="progress34">&nbsp;</span> <span id="progress35">&nbsp;</span>
                        <span id="progress36">&nbsp;</span> <span id="progress37">&nbsp;</span> <span id="progress38">
                            &nbsp;</span> <span id="progress39">&nbsp;</span> <span id="progress40">&nbsp;</span>
                        <span id="progress41">&nbsp;</span> <span id="progress42">&nbsp;</span> <span id="progress43">
                            &nbsp;</span> <span id="progress44">&nbsp;</span> <span id="progress45">&nbsp;</span>
                        <span id="progress46">&nbsp;</span> <span id="progress47">&nbsp;</span> <span id="progress48">
                            &nbsp;</span> <span id="progress49">&nbsp;</span> <span id="progress50">&nbsp;</span>
                        <span id="progress51">&nbsp;</span> <span id="progress52">&nbsp;</span> <span id="progress53">
                            &nbsp;</span> <span id="progress54">&nbsp;</span> <span id="progress55">&nbsp;</span>
                        <span id="progress56">&nbsp;</span> <span id="progress57">&nbsp;</span> <span id="progress58">
                            &nbsp;</span> <span id="progress59">&nbsp;</span> <span id="progress60">&nbsp;</span>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
