<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_SubLinqTag.aspx.cs"
    Inherits="SISKPI.KPI_SubLinqTag" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISOPM</title>
    <link href="../CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" href="../js/tab/jquery.tabs.css" type="text/css" media="print, projection, screen" />
    <!--[if lte IE 7]>
        <link rel="stylesheet" href="../js/tab/jquery.tabs-ie.css" type="text/css" media="projection, screen">
        <![endif]-->
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
    <form id="form1" runat="server" style="width: 100%">
    <div style="margin: 0px; width: 100%; text-align: center;">
        <table width="100%" class="table" id="table1" align="center">
            <tr style="width: 100%">
                <td style="width: 100%" align="center">
                    <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                    <asp:Label ID="lblTitle" runat="server" Class="title" Text="实时显示指标配置"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 100%">
                    <div align="center" style="width: 100%">
                        <table class="table" style="width: 90%">
                            <tr style="width: 100%">
                                <td align="left" style="width: 100%">
                                    <span style="float: left">
                                        <asp:Label ID="lbl_Name" runat="server" Width="400px" Text="lbl"></asp:Label>
                                    </span><span style="float: right">
                                        <asp:Button ID="btnClose" runat="server" Width="80px" Text="返 回" OnClick="btnClose_Click" />
                                    </span>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td width="100%">
                                    <asp:GridView ID="gvTag" CssClass="GridViewStyle" Width="100%" runat="server" AutoGenerateColumns="False"
                                        EmptyDataText="当前无有效指标!" AllowPaging="False" OnRowDataBound="gvTag_RowDataBound"
                                        OnRowCommand="gvTag_RowCommand">
                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                        <RowStyle CssClass="GridViewRowStyle" />
                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                        <PagerStyle CssClass="GridViewPagerStyle" />
                                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号">
                                                <ItemTemplate>
                                                    <input type="hidden" runat="server" id="keyid" value='<%# Eval("TagID").ToString()%>' />
                                                    <%#  Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="UnitName" HeaderText="机组" />
                                            <asp:BoundField DataField="ECName" HeaderText="指标" />
                                            <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="delete" runat="server" Text="删除" CommandName="dataDelete" CommandArgument='<%# Eval("TagID") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Width="60px" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <table style="width: 100%" class="BoderTable">
                                        <tr style="width: 100%">
                                            <td width="180px" align="center">
                                                <asp:Label ID="Label8" Width="40px" runat="server" Text="机组"></asp:Label>
                                                <asp:DropDownList ID="ddl_UnitID" runat="server" Width="120px" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddl_UnitID_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="180px" align="center">
                                                <asp:Label ID="Label2" Width="40px" runat="server" Text="指标"></asp:Label>
                                                <asp:DropDownList ID="ddl_ECID" runat="server" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="160px" align="left">
                                                <asp:Button ID="btnAdd" runat="server" Width="80px" Text="增  加" OnClick="btnAdd_Click" />
                                            </td>
                                        </tr>
                                    </table>
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
