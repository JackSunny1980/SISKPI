<%@ Page Language="C#" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="KPI_RealTrend.aspx.cs"
    Inherits="SISKPI.KPI_RealTrend" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>实时标签数据分析</title>
    <script language="javascript" type="text/javascript" src="../js/DatePicker/WdatePicker.js"
        defer="defer"></script>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap-theme.css" rel="stylesheet" />
    <link href="../Content/themes/Styles/style.css" rel="stylesheet" />
     <link href="../Content/main.css" rel="stylesheet" />
    <link href="../Content/themes/Styles/gridview.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.js" type="text/javascript"></script>

    <%--    <script type="text/javascript">
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
    </script>--%>
    <script type="text/javascript">
	   function ReturnRealValue() {
            window.open("KPI_RealValue.aspx", "_self", "", false);
        }
	</script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 0px; width: 100%;" align="center">
            <%--        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Timer ID="Timer1" runat="server" Interval="600000" OnTick="Timer1_Tick">
                </asp:Timer>--%>
            <table class="table" style="width: 95%;" align="center">
                <%--    <tr style="width: 100%">
                <td align="center" style="width: 100%">
                    <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                    <asp:Label ID="lblTitle" runat="server" Class="title" Text="实时标签数据分析"></asp:Label>
                </td>
            </tr>--%>
                <tr style="width: 100%">
                    <td style="width: 100%" align="center">
                        <div style="width: 95%" align="right">
                            <table style="width: 100%">
                                <tr style="width: 100%">
                                    <td style="width: 30%" align="left">
                                        <asp:Label ID="Label3" runat="server" Text="开始时间:  "></asp:Label>
                                        <input class="Wdate" id="txt_ST" visible="true" style="width: 140px" onfocus="WdatePicker({el:'txt_ST',dateFmt:'yyyy-MM-dd HH:mm:00',skin:'whyGreen'})"
                                            type="text" runat="server" value="" />
                                    </td>
                                    <td style="width: 30%" align="left">
                                        <asp:Label ID="Label1" runat="server" Text="结束时间:  "></asp:Label>
                                        <input class="Wdate" id="txt_ET" visible="true" style="width: 140px" onfocus="WdatePicker({el:'txt_ET',dateFmt:'yyyy-MM-dd HH:mm:00',skin:'whyGreen'})"
                                            type="text" runat="server" value="" />
                                    </td>
                                    <td style="width: 30%" align="left">
                                        <asp:Button ID="btnQuery" CssClass="btn" runat="server" Text="查 询" Width="100px" OnClick="btnQuery_Click" />
                                         <input type="button" value="返回" class="buttonCss" onclick="ReturnRealValue();" />
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 30%" align="left"></td>
                                    <td style="width: 30%" align="left"></td>
                                    <td style="width: 30%" align="right"></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td align="center" style="width: 100%">
                        <div style="width: 95%">
                            <fieldset class="field_info" style="width: 100%;">
                                <legend class="text-center">指标趋势及数据</legend>
                                <table style="width: 100%">
                                    <tr style="width: 100%;">
                                        <td style="width: 100%" align="center">
                                            <asp:Chart ID="pieHC" runat="server" Height="300px" Width="700px">
                                            </asp:Chart>
                                        </td>
                                    </tr>
                                    <tr style="width: 100%">
                                        <td style="width: 100%" align="center">
                                            <asp:GridView ID="gvReal" CssClass="datagrid table table-hover table-bordered table-condensed" runat="server" AutoGenerateColumns="False"
                                                EmptyDataText="没有满足条件的数据" Width="700px" AllowPaging="true" PageSize="20" OnRowDataBound="gvReal_RowDataBound"
                                                OnPageIndexChanging="gvReal_PageIndexChanging">
                                                <PagerStyle CssClass="paging" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="序号">
                                                        <ItemTemplate>
                                                            <input type="hidden" runat="server" id="RealID" value='<%# Eval("RealID").ToString()%>' />
                                                            <%#  Container.DataItemIndex + 1%>
                                                            <%--  <input type="hidden" runat="server" id="color" value='<%# Eval("BKColor").ToString()%>' />
                                            <input type="hidden" runat="server" id="good" value='<%# Eval("BKGood").ToString()%>' />--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="RealDesc" HeaderText="名称">

                                                        <HeaderStyle Width="120px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RealCode" HeaderText="标签点">

                                                        <HeaderStyle Width="120px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RealEngunit" HeaderText="单位">
                                                        <HeaderStyle Width="60px" />

                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RealValue" HeaderText="实时值">
                                                        <HeaderStyle Width="120px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RealTime" HeaderText="时间戳">
                                                        <HeaderStyle Width="120px" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </td>
                </tr>
            </table>
            <%--            </ContentTemplate>
        </asp:UpdatePanel>--%>
        </div>
    </form>
</body>
</html>
