<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Race_Real.aspx.cs"
    Inherits="SISKPI.Race_Real" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISKPI</title>
    <link href="../CSS/MainCSSA.css" type="text/css" rel="stylesheet" />
    <script src="../js/ProcessBar.js" type="text/javascript"></script>
    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../js/Common.js" type="text/javascript"></script>
    <script src="../js/tab/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="../js/Tab/jquery.tabs.js" type="text/javascript"></script>
    <script src="../js/Config.js" type="text/javascript"></script>
    <script src="../js/DatePicker/WdatePicker.js" type="text/javascript" defer="defer"></script>
    <script src="../js/GridViewColor.js" type="text/javascript"></script>
    <style type="text/css">
        .lblstyle2
        {
            border: #D87E26 0px solid;
            cursor: hand;
            color: white;
            background: none;
            font-size: 10pt;
            font-weight: bolder;
        }
    </style>
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
    <div style="margin: 0px; width: 100%;" align="center">
        <%--        
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Timer ID="Timer1" runat="server" Interval="6000000" OnTick="Timer1_Tick">
                </asp:Timer>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
        <table class="table" style="width: 95%;" align="center">
            <tr style="width: 100%">
                <td align="center" style="width: 100%">
                    <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                    <asp:Label ID="lblTitle" runat="server" Class="title" Text="值际竞赛统计"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 100%" align="center">
                    <asp:Label ID="Label5" runat="server" Text="" Width="100%"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 100%" align="center">
                    <div style="width: 95%; margin: 0 0 0 0;" cellspacing="0">
                        <%--#6591CE--%>
                        <table style="width: 100%; height: 100%; margin: 0 0 0 0;" cellspacing="0">
                            <tr style="width: 100%; height: 100%; margin: 0 0 0 0;">
                                <td style="width: 100%; height: 100%; vertical-align: middle;" align="left">
                                    <asp:Panel ID="pButton" runat="server" Width="100%" Height="100%">
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 100%" align="center">
                    <table class="table" style="width: 95%">
                        <tr style="width: 100%">
                            <td style="width: 30%" align="left">
                                <asp:Button ID="btnCurrent" runat="server" Text="实时运行成本" Width="100%" BackColor="LightYellow"
                                    OnClick="btnCurrent_Click" />
                            </td>
                            <td style="width: 30%" align="left">
                                <asp:Button ID="btnCurrentMonth" runat="server" Text="本月运行成本" Width="100%" BackColor="LightYellow"
                                    OnClick="btnCurrentMonth_Click" />
                            </td>
                            <td style="width: 30%" align="right">
                                <asp:Button ID="btnCurrentYear" runat="server" Text="本年运行成本" Width="100%" BackColor="LightYellow"
                                    OnClick="btnCurrentYear_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 100%" align="center">
                    <asp:Label ID="Label2" runat="server" Text="" Width="100%"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 100%" align="center">
                    <table class="table" style="width: 95%">
                        <tr style="width: 100%">
                            <%--<td style="width: 30%" align="left">
                                        <asp:Label ID="lblUnit" runat="server" Text="机组负荷:  "></asp:Label>
                                        <asp:Label ID="lblThree" runat="server" Font-Bold="true" Text=">30% Pe"></asp:Label>
                                    </td>
                                    <td style="width: 30%" align="left">
                                        <asp:Label ID="Label2" runat="server" Text="     机组状态:  "></asp:Label>
                                        <asp:Label ID="lblStab" runat="server" Font-Bold="true" Text="稳定"></asp:Label>
                                    </td>--%>
                            <td style="width: 30%" align="left">
                                <div id="divDay" runat="server">
                                    <asp:Label ID="Label1" runat="server" Text="选择日期:  " Style="width: 30%"></asp:Label>
                                    <input class="Wdate" id="txt_Day" readonly style="width: 30%" onfocus="WdatePicker({el:'txt_Day',dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"
                                        type="text" runat="server" value="" />
                                    <asp:Button ID="btnDay" runat="server" Text="查 询" Width="30%" OnClick="btnDay_Click" />
                                </div>
                                <div id="divMonth" runat="server">
                                    <asp:Label ID="Label3" runat="server" Text="选择月份:  " Style="width: 30%"></asp:Label>
                                    <input class="Wdate" id="txt_Month" readonly style="width: 30%" onfocus="WdatePicker({el:'txt_Month',dateFmt:'yyyy-MM',skin:'whyGreen'})"
                                        type="text" runat="server" value="" />
                                    <asp:Button ID="btnMonth" runat="server" Text="查 询" Width="30%" OnClick="btnMonth_Click" />
                                </div>
                                <div id="divYear" runat="server">
                                    <asp:Label ID="Label4" runat="server" Text="选择年份:  " Style="width: 30%"></asp:Label>
                                    <input class="Wdate" id="txt_Year" readonly style="width: 30%" onfocus="WdatePicker({el:'txt_Year',dateFmt:'yyyy',skin:'whyGreen'})"
                                        type="text" runat="server" value="" />
                                    <asp:Button ID="btnYear" runat="server" Text="查 询" Width="30%" OnClick="btnYear_Click" />
                                </div>
                            </td>
                            <td style="width: 50%" align="left">
                                <asp:Label ID="lblInfor" runat="server" Text="请选择查询时间: " Width="100%"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 100%" align="center">
                    <asp:GridView ID="gvValue" CssClass="GridViewStyle" Width="95%" Visible="true" runat="server"
                        AutoGenerateColumns="false" AllowSorting="true" EmptyDataText="没有满足条件的数据" AllowPaging="false"
                        OnRowDataBound="gvValue_RowDataBound" OnSorting="gvValue_Sorting">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                    </asp:GridView>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 100%" align="center">
                    <div style="width: 95%; text-align: left;">
                        <asp:Label ID="lblNotice" runat="server" Text="注：1、红色行为当前运行值；2、默认为 运行成本 升序排列。 " Width="100%"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr style="width: 100%">
                <td align="center" style="width: 100%">
                    <asp:Chart ID="colValue" runat="server" Height="400" Width="800">
                    </asp:Chart>
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
