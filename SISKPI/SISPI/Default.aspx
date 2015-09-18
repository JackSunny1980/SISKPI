<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Default.aspx.cs"
    Inherits="SISPI.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISKPI</title>
    <link href="CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <script src="js/ProcessBar.js" type="text/javascript"></script>
    <script src="js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="js/Common.js" type="text/javascript"></script>
    <script src="js/DatePicker/WdatePicker.js" type="text/javascript" defer="defer"></script>
    <script src="js/tab/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="js/Tab/jquery.tabs.js" type="text/javascript"></script>
    <script src="js/Config.js" type="text/javascript"></script>
    <script src="js/GridViewColor.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function getscreen() {

            // screen.availWidth 获得屏幕宽度
            // screen.availHeight 获得屏幕高度
            document.getElementById("saheight").value = screen.availHeight;
            document.getElementById("sawidth").value = screen.availWidth;

        }

    </script>
</head>
<body onload="getscreen();">
    <form id="form1" runat="server">
    <div style="margin: 0px; width: 100%; text-align: center;">
        <table class="table" style="width: 100%; text-align: center;">
            <tr style="width: 100%;">
                <td align="center" style="width: 100%;">
                            <asp:Image ID="Image2" runat="server" Height="40px" ImageUrl="find.gif" />
                    <asp:Label ID="lblTitle" runat="server" Class="title" Text="标签历史数据查询"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 100%" align="center">
                    <div style="width: 700px" align="right">
                        <table style="width: 100%" class="table">
                            <tr style="width: 100%; text-align: center;">
                                <td style="width: 100%" align="center">
                                    <span style="float: left;">选择指标：<asp:DropDownList ID="ddlTags" Width="310px" runat="server">
                                    </asp:DropDownList>
                                    </span><span style="float: right;"> 
                                     <asp:Button ID="btnExport" runat="server" Text="导 出" Width="80px" 
                                        onclick="btnExport_Click"/></span>                                 
                                    </span>
                                </td>
                            </tr>
                            <tr style="width: 100%; text-align: center;">
                                <td style="width: 100%" align="center">
                                    <span style="float: left;">开始日期：<input class="Wdate" id="txt_ST" visible="true" style="width: 120px"
                                        onfocus="WdatePicker({el:'txt_ST',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" type="text"
                                        runat="server" value="" />
                                        结束日期：<input class="Wdate" id="txt_ET" visible="true" style="width: 120px" onfocus="WdatePicker({el:'txt_ET',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"
                                            type="text" runat="server" value="" />
                                    </span><span style="float: right;">
                                        <asp:Button ID="btnQuery" runat="server" Text="查 询" Width="80px" OnClick="btnQuery_Click" /></span>
                                    <input id="saheight" runat="server" type="text" value="1024" style="width: 10px;
                                        visibility: hidden;" />
                                    <input id="sawidth" runat="server" type="text" value="768" style="width: 10px; visibility: hidden;" />
                                </td>
                            </tr>
                            <tr style="width: 100%; text-align: center;">
                                <td style="width: 100%;" align="center">
                                    <asp:GridView ID="gvEC" CssClass="GridViewStyle" Width="100%" runat="server" AutoGenerateColumns="False"
                                        EmptyDataText="没有满足条件的数据" AllowPaging="false" OnRowDataBound="gvEC_RowDataBound"
                                        OnRowCommand="gvEC_RowCommand">
                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                        <RowStyle CssClass="GridViewRowStyle" />
                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                        <PagerStyle CssClass="GridViewPagerStyle" />
                                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号">
                                                <ItemTemplate>
                                                    <input type="hidden" runat="server" id="TagID" value='<%# Eval("TagID").ToString()%>' />
                                                    <%#  Container.DataItemIndex + 1%>
                                                    <input type="hidden" runat="server" id="qulity" value='<%# Eval("TagStatus").ToString()%>' />
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TagName" HeaderText="标签名称">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TagUnit" HeaderText="单位">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TagTime" HeaderText="历史时间">
                                                <HeaderStyle Width="120px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TagValue" HeaderText="历史数值">
                                                <HeaderStyle Width="120px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TagStatus" HeaderText="状态">
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
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
        <br>
        <b></b>
        <table align="center" style="width: 260px; height: 40px" class="BoderTable">
            <tr>
                <td>
                    <div>
                        <font color="green" size="2" style="font-weight: bold">正在查询数据库，请稍候...</font></div>
                    <div style="border-right: green 1px solid; padding-right: 2px; border-top: green 1px solid;
                        padding-left: 2px; font-size: 8pt; padding-bottom: 2px; border-left: green 1px solid;
                        padding-top: 2px; border-bottom: green 1px solid">
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
