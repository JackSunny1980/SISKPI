<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="KPI_ECData.aspx.cs"
    Inherits="SISKPI.KPI_ECData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISKPI</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap-theme.css" rel="stylesheet" />
    <link href="../Content/themes/Styles/style.css" rel="stylesheet" />
    <link href="../Content/themes/Styles/gridview.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.js" type="text/javascript"></script>
     <script language="javascript" type="text/javascript" src="../js/DatePicker/WdatePicker.js"
        defer="defer"></script>
</head>
<body>
    <form id="form1" runat="server">
        <%-- 
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Timer ID="Timer1" runat="server" Interval="60000" OnTick="Timer1_Tick">
    </asp:Timer>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
        <div class="col-lg-12 col-sm-12" style="margin: 10px 0 0 0;">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>指标历史数据查询
                    </h3>
                </div>
                <div class="panel-body">
                	<div style="margin: 0px; width: 100%; text-align: center;">
           				<table class="table" style="width: 100%; text-align: center;">
                            <tr style="width: 100%">
                                <td style="width: 100%; border:0" align="center">
                                    <div style="width: 95%" align="right">
                                        <table style="width: 100%">
                                            <%--
                                                <td style="width: 100px" align="right">
                                                <asp:Label ID="Label2" runat="server" Text="查询月份:"></asp:Label>
                                            </td>
                                            <td style="width: 70%" align="left">
                                                
                                                <input class="Wdate" id="txt_Month" style="width: 120px" onfocus="WdatePicker({el:'txt_Month',dateFmt:'yyyy-MM',skin:'whyGreen'})"
                                                    type="text" runat="server" value="" />
                                                <asp:Button ID="btnQuery" runat="server" Text="查 询" Width="100px" OnClick="btnQuery_Click"
                                                    CssClass="btn" onmouseover="this.className='btn_mouseover'" onmouseout="this.className='btn_mouseout'" />
                                                
                                            </td>--%>
                                            <%--
                                                <tr style="width: 100%">
                                                <td style="width: 100%" align="left">
                                                    <span style="float: right;">
                                                        <asp:Button ID="btnExport" runat="server" Text="导出到EXCEL" Width="120px" OnClick="btnExport_Click"
                                                            CssClass="btn" onmouseover="this.className='btn_mouseover'" onmouseout="this.className='btn_mouseout'" />
                                                    </span></td>
                                            </tr>--%>
                                            <tr>
                                                <td style="width: 100%;" align="center" >
                                                    <span style="float: left;">开始时间：<input class="Wdate" id="txt_ST" visible="true" style="width: 140px"
                                                        onfocus="WdatePicker({el:'txt_ST',dateFmt:'yyyy-MM-dd HH:mm:00',skin:'whyGreen'})"
                                                        type="text" runat="server" value="" />
                                                        结束时间：<input class="Wdate" id="txt_ET" visible="true" style="width: 140px" onfocus="WdatePicker({el:'txt_ET',dateFmt:'yyyy-MM-dd HH:mm:00',skin:'whyGreen'})"
                                                            type="text" runat="server" value="" />
                                                    </span><span style="float: left;">
                                                        <asp:Button ID="btnQuery" runat="server" CssClass="btnSearch" Text="查 询" Width="80px" OnClick="btnQuery_Click"  style=" margin-left:4px; height:20px"/></span>
                                                    <input id="saheight" runat="server" type="text" value="1024" style="width: 10px; visibility: hidden;" />
                                                    <input id="sawidth" runat="server" type="text" value="768" style="width: 10px; visibility: hidden;" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr style="width: 100%; text-align: center;">
                                <td style="width: 100%; border:0" align="center">
                                    <asp:GridView ID="gvEC" CssClass="datagrid table table-hover table-bordered table-condensed" Width="95%" runat="server" AutoGenerateColumns="False"
                                        EmptyDataText="没有满足条件的数据" AllowPaging="false" OnRowDataBound="gvEC_RowDataBound"
                                        OnRowCommand="gvEC_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号">
                                                <ItemTemplate>
                                                    <input type="hidden" runat="server" id="SSID" value='<%# Eval("SSID").ToString()%>' />
                                                    <%#  Container.DataItemIndex + 1%>
                                                    <input type="hidden" runat="server" id="qulity" value='<%# Eval("ECQulity").ToString()%>' />
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px" />
                                              
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ECName" HeaderText="指标名称">
                                               
                                                <HeaderStyle Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ECValue" HeaderText="指标数值">
                                                <HeaderStyle Width="120px" />
                                            
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ECScore" HeaderText="指标得分">
                                                <HeaderStyle Width="120px" />
                                            
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ECTime" HeaderText="计算时间">
                                             
                                                <HeaderStyle Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ECPeriod" HeaderText="班次">
                                             
                                                <HeaderStyle Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ECShift" HeaderText="值次">
                                            
                                                <HeaderStyle Width="120px" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div> 
                </div>
        	</div>
         </div>
        
    </form>
</body>
</html>
