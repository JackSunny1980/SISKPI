<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_SATagConfig.aspx.cs"
    Inherits="SISKPI.KPI_SATagConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISKPI Config</title>
    <base target="_self" />
    <link href="../CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <script src="../js/ProcessBar.js" type="text/javascript"></script>
    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../js/Common.js" type="text/javascript"></script>
    <script src="../js/tab/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="../js/Tab/jquery.tabs.js" type="text/javascript"></script>
    <script src="../js/Config.js" type="text/javascript"></script>
    <script src="../js/GridViewColor.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%; text-align: center;">
    <div style="margin: 0px; width: 95%; text-align: center;">
        <table width="100%" align="left" class="table" id="table1">
            <tr style="width: 100%" class="table_tr">
                <td align="left" style="width: 100%">
                    <table class="table" id="table2" style="width: 100%">
                        <tr style="width: 100%">
                            <td valign="middle" align="center" style="width: 100%; height: 40px;">
                                <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                                <asp:Label ID="lblTitle" runat="server" Class="title" Text="安全指标配置"></asp:Label>
                            </td>
                        </tr>
                        <tr style="width: 100%">
                            <td valign="middle" align="center" style="width: 100%; height: 40px;">
                                <table class="table" style="width: 98%;">
                                    <tr align="left" style="width: 100%;">
                                        <td style="width: 20%;">
                                            <asp:Button ID="btnAdd" Width="120px" runat="server" Text="新增指标" OnClick="btnAdd_Click" />
                                        </td>
                                        <td style="width: 20%;">
                                            <asp:Button ID="btnSort" Style="width: 100px" runat="server" Text="指标排序" 
                                                onclick="btnSort_Click" />
                                        </td>
                                        <td style="width: 20%;">
                                            <asp:Button ID="btnCopy" Style="width: 100px" runat="server" Text="复制机组" Visible="false" />
                                        </td>
                                        <td style="width: 20%;">
                                        </td>
                                        <td style="width: 40%;">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="width: 100%">
                            <td valign="middle" align="center" style="width: 100%; height: 40px;">
                                <table class="table" style="width: 98%;">
                                    <tr align="left" style="width: 100%;">
                                        <td style="width: 15%;">
                                            <asp:Label ID="Label1" runat="server" Text="机组集:"></asp:Label>
                                            <select style="width: 100px" runat="server" id="ddl_UnitID" name="ddlUnit">
                                            </select>
                                        </td>
                                        <td style="width: 15%;">
                                            <asp:Label ID="Label2" runat="server" Text="设备集:"></asp:Label>
                                            <select style="width: 100px" runat="server" id="ddl_SeqID" name="ddlSeq">
                                            </select>
                                        </td>
                                        <td style="width: 15%;">
                                            <asp:Label ID="Label3" runat="server" Text="指标集:"></asp:Label>
                                            <select style="width: 100px" runat="server" id="ddl_KpiID" name="ddlKpi">
                                            </select>
                                        </td>
                                        <%--<td style="width: 20%;">
                                            <asp:Label ID="Label4" runat="server" Text="周期集:"></asp:Label>
                                            <select style="width: 40px" runat="server" id="ddl_CycleID" name="ddlCycle">
                                            </select>
                                        </td>--%>
                                        <td style="width: 15%;">
                                            <asp:Label ID="Label5" runat="server" Text="统计指标:"></asp:Label>
                                            <select style="width: 100px" runat="server" id="ddl_SAID" name="ddlSA">
                                            </select>
                                        </td>
                                        <td style="width: 40%;">
                                            <span style="float: left">
                                                <asp:Button ID="btnQuery" Style="width: 100px" runat="server" Text="查 询" OnClick="btnQuery_Click" />
                                            </span><span style="float: right"></span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="width: 100%">
                            <td align="center" style="width: 100%">
                                <div style="width: 100%">
                                    <fieldset class="field_info" style="width: 98%;">
                                        <legend>指标信息</legend>
                                        <table class="table" style="width: 98%;">
                                            <tr align="left" style="width: 100%;">
                                                <td align="left" style="width: 100%;">
                                                    <asp:GridView ID="gvSA" CssClass="GridViewStyle" Width="100%" runat="server" AutoGenerateColumns="False"
                                                        EmptyDataText="没有满足条件的数据" AllowPaging="False" OnRowDataBound="gvSA_RowDataBound"
                                                        OnRowCommand="gvSA_RowCommand" OnRowCancelingEdit="gvSA_RowCancelingEdit" OnRowEditing="gvSA_RowEditing">
                                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                                        <RowStyle CssClass="GridViewRowStyle" />
                                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                                        <PagerStyle CssClass="GridViewPagerStyle" />
                                                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="序号">
                                                                <ItemTemplate>
                                                                    <input type="hidden" runat="server" id="ecid" value='<%# Eval("SAID").ToString()%>' />
                                                                    <%#  Container.DataItemIndex + 1%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="40px" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="SACode" HeaderText="代码">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SAName" HeaderText="名称">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="EngunitName" HeaderText="单位">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SAIndex" HeaderText="显示顺序">
                                                                <ControlStyle Width="100px"></ControlStyle>
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="考核周期" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle Width="40px"></ControlStyle>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlCycle" runat="server" DataSourceID="GetCycles" DataTextField="Name"
                                                                        DataValueField="ID">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="页面集" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle Width="40px"></ControlStyle>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlWeb" runat="server" DataSourceID="GetSAWebs" DataTextField="Name"
                                                                        DataValueField="Value">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="是否有效" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle Width="40px"></ControlStyle>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlValid" runat="server" DataSourceID="GetYesNo" DataTextField="Name"
                                                                        DataValueField="Value">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="是否计算" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle Width="40px"></ControlStyle>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlCalc" runat="server" DataSourceID="GetYesNo" DataTextField="Name"
                                                                        DataValueField="Value">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="是否显示" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle Width="40px"></ControlStyle>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlDisplay" runat="server" DataSourceID="GetYesNo" DataTextField="Name"
                                                                        DataValueField="Value">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="是否计总" ItemStyle-HorizontalAlign="Center">
                                                                <ControlStyle Width="40px"></ControlStyle>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlTotal" runat="server" DataSourceID="GetYesNo" DataTextField="Name"
                                                                        DataValueField="Value">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="copy" runat="server" Text="复制" CommandName="dataCopy" CommandArgument='<%# Eval("SAID") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="40px" />
                                                            </asp:TemplateField> --%>
                                                            <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="view" runat="server" Text="浏览" CommandName="dataView" CommandArgument='<%# Eval("SAID") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="40px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="delete" runat="server" Text="删除" CommandName="dataDelete" CommandArgument='<%# Eval("SAID") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="40px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr align="center" style="width: 100%;">
                                                <td align="center" style="width: 100%;">
                                                    <asp:ObjectDataSource ID="GetYesNo" runat="server" OldValuesParameterFormatString="original_{0}"
                                                        SelectMethod="GetData" TypeName="SISKPI.KPIDataSetTableAdapters.GetYesNoTableAdapter">
                                                    </asp:ObjectDataSource>
                                                    <asp:ObjectDataSource ID="GetCycles" runat="server" OldValuesParameterFormatString="original_{0}"
                                                        SelectMethod="GetData" TypeName="SISKPI.KPIDataSetTableAdapters.GetCyclesTableAdapter"
                                                        InsertMethod="Insert">
                                                        <InsertParameters>
                                                            <asp:Parameter Name="ID" Type="String" />
                                                            <asp:Parameter Name="Name" Type="String" />
                                                        </InsertParameters>
                                                    </asp:ObjectDataSource>
                                                    <asp:ObjectDataSource ID="GetSAWebs" runat="server" OldValuesParameterFormatString="original_{0}"
                                                        SelectMethod="GetData" 
                                                        TypeName="SISKPI.KPIDataSetTableAdapters.GetSAWebTableAdapter">
                                                    </asp:ObjectDataSource>
                                                   
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </td>
                        </tr>
                    </table>
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
