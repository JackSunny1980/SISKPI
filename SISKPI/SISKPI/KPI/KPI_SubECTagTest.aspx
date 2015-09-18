<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_SubECTagTest.aspx.cs"
    Inherits="SISKPI.KPI_SubECTagTest" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISKPI</title>
    <link href="../CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <script src="../js/ProcessBar.js" type="text/javascript"></script>
    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../js/Common.js" type="text/javascript"></script>
    <script src="../js/tab/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="../js/Tab/jquery.tabs.js" type="text/javascript"></script>
    <script src="../js/Config.js" type="text/javascript"></script>
    <script src="../js/GridViewColor.js" type="text/javascript"></script>
    <script type="text/javascript">    
        var t;
        t = self.setInterval("ShowTime()", 10);

        function ShowTime() {
            var Today = new Date(); //这条语句要放在该函数内，而不能作为全局变量
            hours = Today.getHours();
            minutes = Today.getMinutes(); 
            second = Today.getSeconds();
            if (hours < 10) hours = "0" + hours
            if (minutes < 10) minutes = "0" + minutes
            if (second < 10) second = "0" + second
            document.all.time.value = hours + ":" + minutes + ":" + second;
        }

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
        <table width="100%" class="table" align="center">
            <tr style="width: 100%">
                <td style="width: 100%" align="center">
                    <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                    <asp:Label ID="lblTitle" runat="server" Class="title" Text="计算表达式验证"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 100%" align="center">
                    <div style="width: 60%">
                        <table width="100%" class="table" align="center">
                            <tr style="width: 100%">
                                <td style="width: 100%" align="left">
                                    <%--<asp:Button ID="btnClose" runat="server" Width="60px" Text="关 闭" OnClick="btnClose_Click" />--%>
                                    当前时间：<input id="time" type="text" style="width: 120px" />
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td style="width: 100%" align="left">
                                    <asp:Label ID="Label1" runat="server" Text="计算表达式:"></asp:Label>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td style="width: 100%" align="left">
                                    <asp:TextBox ID="tbx_Exp" runat="server" Width="400px" Height="50px" Wrap="true" TextMode="MultiLine"></asp:TextBox>
                                    <asp:Button ID="btnEval" runat="server" Width="60px" Text="解 析" 
                                        onclick="btnEval_Click"/>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td style="width: 100%" align="left">
                                    <asp:Label ID="Label2" runat="server" Text="计算结果:"></asp:Label>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td style="width: 100%" align="left">
                                    <asp:TextBox ID="tbx_Result" runat="server" Width="400px" Height="50px" Wrap="true" TextMode="MultiLine"></asp:TextBox>
                                    <asp:Button ID="btnCalc" runat="server" Width="60px" Text="计 算" OnClick="btnCalc_Click" />
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td style="width: 100%" align="left">
                                    <asp:GridView ID="gvTag" CssClass="GridViewStyle" Width="500px" runat="server" AutoGenerateColumns="False"
                                        EmptyDataText="没有标签、函数或系数存在" AllowPaging="False" OnRowDataBound="gvTag_RowDataBound"
                                        OnRowCancelingEdit="gvTag_RowCancelingEdit" OnRowEditing="gvTag_RowEditing" OnRowUpdating="gvTag_RowUpdating">
                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                        <RowStyle CssClass="GridViewRowStyle" />
                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                        <PagerStyle CssClass="GridViewPagerStyle" />
                                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号">
                                                <ItemTemplate>
                                                    <input type="hidden" runat="server" id="key" value='<%# Eval("Key").ToString()%>' />
                                                    <%#  Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Key" HeaderText="代码" ReadOnly="true">
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Value" HeaderText="数值">
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:BoundField>
                                            <asp:CommandField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"
                                                ShowEditButton="True" EditText="输入" />
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
