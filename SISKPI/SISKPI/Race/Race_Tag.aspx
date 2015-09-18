<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Race_Tag.aspx.cs" Inherits="SISKPI.Race_Tag" %>

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
    <form id="form1" runat="server" style="width: 100%">
    <div style="width: 100%">
        <table style="width: 100%" align="center" class="table" id="table1">
            <tr style="width: 100%">
                <td style="width: 100%" align="center">
                    <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                    <asp:Label ID="lblTitle" runat="server" Class="title" Text="值际竞赛标签管理"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 100%" align="center">
                    <div style="width: 90%" align="center">
                        <table style="width: 100%" align="center" class="table" id="table3">
                            <tr style="width: 100%">
                                <td style="width: 100%" align="center">
                                    <fieldset style="width: 100%">
                                        <legend>自定义查询</legend>
                                        <table style="width: 100%" class="table" id="table2">
                                            <tr style="width: 100%">
                                                <td align="left" style="width: 120px">
                                                    选择机组
                                                </td>
                                                <td align="left" style="width: 150px">
                                                    <select style="width: 150px" runat="server" id="ddlUnitInfor">
                                                    </select>
                                                </td>
                                                <td align="left" style="width: 120px">
                                                </td>
                                                <td align="left" style="width: 300px">
                                                    <%--参数类型<asp:RadioButtonList ID="rblTagType" Width="300px" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="HOUR" >时报</asp:ListItem>
                                        <asp:ListItem Value="SHIFT" >值报</asp:ListItem>
                                        <asp:ListItem Value="MONTH">月报</asp:ListItem>
                                    </asp:RadioButtonList>--%>
                                                </td>
                                                <td align="left" style="width: 120px">
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td align="left" style="width: 120px">
                                                    标签名
                                                </td>
                                                <td align="left" style="width: 150px">
                                                    <asp:TextBox ID="txt_TagName" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                                <td align="left" style="width: 120px">
                                                    标签描述
                                                </td>
                                                <td align="left" style="width: 150px">
                                                    <asp:TextBox ID="txt_TagDesc" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                                <td align="left" style="width: 150px">
                                                    <asp:Button ID="btnSearch" Width="150px" runat="server" Text="查 询" OnClick="btnSearch_Click" />
                                                </td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td align="center" style="width: 120px">
                                                    <asp:Button ID="btnBatchPro" runat="server" Text="批处理" Width="150px" OnClick="btnBatchPro_Click" />
                                                    <%--<asp:Button ID="btnAdd" runat="server" Width="120px" Text="添加新标签点" OnClick="btnAdd_Click" />--%>
                                                </td>
                                                <td align="center" style="width: 150px">
                                                    <asp:Button ID="btnSort" Width="150px" runat="server" Text="排 序" OnClick="btnSort_Click" />
                                                </td>
                                                <td align="center" style="width: 120px">
                                                </td>
                                                <td align="center" style="width: 150px">
                                                    <%--<asp:Button ID="btnReturn" runat="server" Text="返 回" Width="120px" OnClick="btnReturn_Click" />--%>
                                                </td>
                                                <td align="center" style="width: 150px">
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td style="width: 100%">
                                    <asp:GridView ID="gvTag" CssClass="GridViewStyle" Width="100%" runat="server" AutoGenerateColumns="False"
                                        EmptyDataText="没有满足条件的数据" OnRowCommand="gvTag_RowCommand" OnRowDataBound="gvTag_RowDataBound"
                                        AllowPaging="true" OnPageIndexChanging="gvTag_PageIndexChanging" PageSize="50">
                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                        <RowStyle CssClass="GridViewRowStyle" />
                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                        <PagerStyle CssClass="GridViewPagerStyle" />
                                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号" ControlStyle-Width="80px">
                                                <ItemTemplate>
                                                    <input type="hidden" runat="server" id="tagid" value='<%# Eval("TagID").ToString()%>' />
                                                    <%#  Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="UnitName" HeaderText="机组" ControlStyle-Width="100px">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TagName" HeaderText="名称" ControlStyle-Width="100px"></asp:BoundField>
                                            <asp:TemplateField HeaderText="描述" ControlStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_TagDesc" runat="server" ToolTip='<%# Eval("TagDesc").ToString()%>'
                                                        Text='<%# Eval("TagDesc").ToString().Length > 50 ? Eval("TagDesc").ToString().Substring(0,50)+".." : Eval("TagDesc").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TagType" HeaderText="点类型" ControlStyle-Width="60px"></asp:BoundField>
                                            <asp:BoundField DataField="TagEngunit" HeaderText="单位" ControlStyle-Width="60px">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TagIndex" HeaderText="序号" ControlStyle-Width="60px"></asp:BoundField>
                                            <asp:BoundField DataField="TagFilterExp" HeaderText="过滤式" ControlStyle-Width="100px">
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="计算式" ControlStyle-Width="200px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_TagCalcExp" runat="server" ToolTip='<%# Eval("TagCalcExp").ToString()%>'
                                                        Text='<%# Eval("TagCalcExp").ToString().Length > 200 ? Eval("TagCalcExp").ToString().Substring(0,200)+".." : Eval("TagCalcExp").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TagCalcExpType" HeaderText="类型" />
                                            <asp:BoundField DataField="TagCalcType" HeaderText="模式" />
                                            <asp:BoundField DataField="TagFactor" HeaderText="倍率" ControlStyle-Width="40px">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TagOffset" HeaderText="偏差" />
                                            <%--
                            <asp:BoundField DataField="TagUnitName" HeaderText="机组" ControlStyle-Width="60px">
                            </asp:BoundField>
                            <asp:BoundField DataField="TagTableName" HeaderText="表" ControlStyle-Width="60px">
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input class="GridViewButtonStyle" type="button" runat="server" id="update" value='编辑' />
                                </ItemTemplate>
                                <HeaderStyle Width="30px" />
                            </asp:TemplateField>
                                            --%>
                                            <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton CssClass="buttonstyle" ID="lb_delete" runat="server" Text="删除" CommandName="dataDelete"
                                                        CommandArgument='<%# Eval("TagID") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Width="30px" />
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
        <table align="center" style="width: 260px; height: 45px" class="BoderTable">
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
