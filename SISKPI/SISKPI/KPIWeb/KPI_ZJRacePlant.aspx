<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="KPI_ZJRacePlant.aspx.cs"
    Inherits="SISKPI.KPI_ZJRacePlant" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISKPI</title>
    <link href="../CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <script src="../js/ProcessBar.js" type="text/javascript"></script>
    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../js/Common.js" type="text/javascript"></script>
    <script src="../js/DatePicker/WdatePicker.js" type="text/javascript" defer="defer"></script>
    <script src="../js/tab/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="../js/Tab/jquery.tabs.js" type="text/javascript"></script>
    <script src="../js/Config.js" type="text/javascript"></script>
    <script src="../js/GridViewColor.js" type="text/javascript"></script>
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
                    <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                    <asp:Label ID="lblTitle" runat="server" Class="title" Text="值际竞赛月报表"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 100%" align="center">
                    <div style="width: 95%" align="left">
                        <table style="width: 100%" class="table">
                            <tr style="width: 100%">
                                <td style="width: 100%" align="left">
                                    <span style="float: left"></span><span style="float: right">
                                        <asp:Button ID="btnExport" runat="server" Width="100px" Text="导出为Excel" OnClick="btnExport_Click" />
                                    </span>
                                    <%--<asp:Label ID="Label2" runat="server" Text="机组号："></asp:Label>
                                            <asp:DropDownList ID="ddlUnit" runat="server" Width="120px" AutoPostBack="true" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged">
                                            </asp:DropDownList>--%>
                                    <input id="saheight" runat="server" type="text" value="1024" style="width: 10px;
                                        visibility: hidden;" />
                                    <input id="sawidth" runat="server" type="text" value="768" style="width: 10px; visibility: hidden;" />
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td style="width: 100%" align="center">
                                    <span style="float: inherit">
                                        <asp:Label ID="lblPlant" runat="server" Text="单元："></asp:Label>
                                        <asp:DropDownList ID="ddlPlant" runat="server" Width="120px">
                                        </asp:DropDownList>
                                        <asp:Label ID="Label3" runat="server" Text="月份："></asp:Label>
                                        <input class="Wdate" id="txt_Month" visible="true" style="width: 100px" onfocus="WdatePicker({el:'txt_Month',dateFmt:'yyyy-MM',skin:'whyGreen'})"
                                            type="text" runat="server" value="" />
                                        <asp:Button ID="btnQuery" runat="server" Width="100px" Text="查  询" OnClick="btnQuery_Click" />
                                    </span><span style="float: right"></span>
                                </td>
                            </tr>
                            <tr style="width: 100%; text-align: center;">
                                <td style="width: 100%;" align="center">
                                    <asp:GridView ID="gvBonus" CssClass="GridViewStyle" Width="100%" runat="server" AutoGenerateColumns="true"
                                        EmptyDataText="没有满足条件的数据" AllowPaging="false" OnRowDataBound="gvBonus_RowDataBound"
                                        OnRowCommand="gvBonus_RowCommand" OnRowCreated="gvBonus_RowCreated">
                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                        <RowStyle CssClass="GridViewRowStyle" />
                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                        <PagerStyle CssClass="GridViewPagerStyle" />
                                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                        <EmptyDataTemplate>
                                            <table cellpadding="0" cellspacing="0" width="100%" border="1" style="background-color: #6591CE;
                                                color: White;">
                                                <tr style="width: 100%">
                                                    <td width="10%" rowspan="2">
                                                        组别
                                                    </td>
                                                    <td width="10%" rowspan="2">
                                                        机组
                                                    </td>
                                                    <td width="20%" colspan="2">
                                                        分数
                                                    </td>
                                                    <td width="20%" colspan="2">
                                                        名次
                                                    </td>
                                                    <td width="20%" colspan="2">
                                                        分数
                                                    </td>
                                                    <td width="20%" colspan="2">
                                                        名次
                                                    </td>
                                                </tr>
                                                <tr style="width: 100%">
                                                    <td width="10%">
                                                        本月
                                                    </td>
                                                    <td width="10%">
                                                        本年
                                                    </td>
                                                    <td width="10%">
                                                        本月
                                                    </td>
                                                    <td width="10%">
                                                        本年
                                                    </td>
                                                    <td width="10%">
                                                        本月
                                                    </td>
                                                    <td width="10%">
                                                        本年
                                                    </td>
                                                    <td width="10%">
                                                        本月
                                                    </td>
                                                    <td width="10%">
                                                        本年
                                                    </td>
                                                </tr>
                                                <tr style="width: 100%; background-color: White; color: Black;">
                                                    <td width="10%">
                                                        1
                                                    </td>
                                                    <td width="10%">
                                                        #1机组
                                                    </td>
                                                    <td width="10%">
                                                    </td>
                                                    <td width="10%">
                                                    </td>
                                                    <td width="10%">
                                                    </td>
                                                    <td width="10%">
                                                    </td>
                                                    <td width="10%">
                                                    </td>
                                                    <td width="10%">
                                                    </td>
                                                    <td width="10%">
                                                    </td>
                                                    <td width="10%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="10" align="center" style="background-color: White; color: Black;">
                                                        没有数据显示
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
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
