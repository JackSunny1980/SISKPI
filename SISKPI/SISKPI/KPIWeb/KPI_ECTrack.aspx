<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_ECTrack.aspx.cs"
    Inherits="SISKPI.KPI_ECTrack" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISKPI</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap-theme.css" rel="stylesheet" />
    <link href="../Content/main.css" rel="stylesheet" />
    <link href="../Content/themes/Styles/style.css" rel="stylesheet" />
    <link href="../Content/themes/Styles/gridview.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" class="form-horizontal" runat="server">
    	<div class="col-lg-12 col-sm-12" style="margin: 10px 0 0 0;">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <span class="glyphicon glyphicon-road" aria-hidden="true"></span>经济指标计算追踪
                    </h3>
                </div>
                <div class="panel-body">
                     <div class="row" style=" margin:0">
                        <div class="form-group">
                        	<div style=" height:30px">
                            	<label id="lblECCode" runat="server" for="txtECCode" class="col-sm-2 control-label label-text" style="text-align:left">指标代码:</label>
                            </div>
                            <div class="col-sm-10" style="width:100%">
                                <input type="text" runat="server" class="form-control label-text reset-txtArea" id="txtECCode" readonly="">
                            </div>
                        </div>
                        <div class="form-group">
                        	<div style=" height:30px">
                            	<label id="lblECName" runat="server" for="txtECName" class="col-sm-2 control-label label-text" style="text-align:left">指标名称:</label>
                            </div>
                            <div class="col-sm-10" style="width:100%">
                                <input type="text" runat="server" class="form-control label-text reset-txtArea" id="txtECName" readonly="">
                            </div>
                        </div>
                        <div class="form-group">
                        	<div style=" height:30px">
                            	<label id="Label3" runat="server" for="tbx_ECCalcExp" class="col-sm-2 control-label label-text" style="text-align:left">计算表达式:</label>
                            </div>
                            <div class="col-sm-10" style="width:100%">
                                <textarea id="tbx_ECCalcExp" runat="server" class="form-control  reset-txtArea" readonly="" rows="3"></textarea>
                            </div>
                        </div>
                        <div class="form-group">
                        	<div style=" height:30px">
                            	<label id="Label4" runat="server" for="tbx_ECExpression" class="col-sm-2 control-label label-text" style="text-align:left">计算结果:</label>
                            </div>
                            <div class="col-sm-10" style="width:100%">
                                <textarea id="tbx_ECExpression" runat="server" class="form-control  reset-txtArea" readonly="" rows="3"></textarea>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <asp:GridView ID="gvTag" CssClass="datagrid table table-hover table-bordered table-condensed" Width="500px" Visible="false" runat="server" AutoGenerateColumns="False"
                            EmptyDataText="没有标签、函数或系数存在" AllowPaging="False" OnRowDataBound="gvTag_RowDataBound"
                            OnRowCancelingEdit="gvTag_RowCancelingEdit" OnRowEditing="gvTag_RowEditing" OnRowUpdating="gvTag_RowUpdating">
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
                    </div>
                </div>
            </div>
        </div>
    
       


        <%--<div style="margin: 0px; width: 100%; text-align: center;">
            <table width="100%" align="center">
                <tr>
                    <td style="width: 100%" align="center">
                        <h3 class="text-center">经济指标计算追踪
                        </h3>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" align="center">
                        <div style="width: 60%">
                            <table width="100%" align="center">
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblECCode" runat="server" CssClass="control-label label-text" Text="指标代码: "></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblECName" runat="server" CssClass="control-label label-text" Text="指标名称: "></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="Label1" CssClass="control-label label-text" runat="server" Text="计算表达式:"></asp:Label>
                                    </td>
                                    <td>
                                        <textarea id="tbx_ECCalcExp" runat="server" class="form-control  reset-txtArea" readonly="" rows="3"></textarea>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="Label2" runat="server" CssClass="control-label label-text" Text="计算结果:"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <textarea id="tbx_ECExpression" runat="server" class="form-control  reset-txtArea" readonly="" rows="3"></textarea>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>--%>
    </form>
</body>
</html>
