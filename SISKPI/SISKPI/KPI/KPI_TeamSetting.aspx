<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_TeamSetting.aspx.cs"
    Inherits="SISKPI.KPI.KPI_TeamSetting" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CSS/MainCSS.css" rel="stylesheet" type="text/css" />
    <link href="../KPIAlarm/CSS/KPIAlarm.css" rel="stylesheet" type="text/css" />
    <link href="../KPIAlarm/CSS/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../KPIAlarm/CSS/PagingStyle.css" rel="stylesheet" type="text/css" />
    <link href="../KPIAlarm/CSS/DialogStyle.css" rel="stylesheet" type="text/css" />
    <script src="../KPIAlarm/Script/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="../KPIAlarm/Script/jquery-ui-1.10.3.custom.js" type="text/javascript"></script>
    <script src="../js/TeamSetting.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <input id="hidFlag" type="hidden" runat="server" />
        <input id="hidCode" type="hidden" runat="server" />
        <input id="hidMultipleCode" type="hidden" runat="server" />
        <div id="DialogPanel">
            <p id="diaglogTips" class="row_P validateTips">
            </p>
            <p class="row_P">
                <label for="name">
                    单元组:</label>
                <select id="dropPlantList" class="dropdown" runat="server">
                </select>
                <input id="hidSelectedPlant" type="hidden" runat="server" />
            </p>
            <p class="row_P">
                <label>
                    运行值:</label>
                <select id="dropShiftList" class="dropdown" runat="server">
                </select>
                <input id="hidSelectedShift" type="hidden" runat="server" />
                <input id="hidSelectedShiftName" type="hidden" runat="server" />
            </p>
            <p class="row_P">
                <label>
                    人员:</label>
                <select id="dropPersonList" class="dropdown" runat="server">
                </select>
                <input id="hidSelectPerson" type="hidden" runat="server" />
            </p>
            <p class="row_P">
                <label>
                    岗位:</label>
                <asp:TextBox CssClass="textc ui-widget-content ui-corner-all" ID="txtPosition" Enabled="false" ReadOnly="true"
                    runat="server" />
                <input id="hidPosition" type="hidden" runat="server" />
            </p>
            <p class="row_P">
                <label>
                    替班人员:</label>
                <select id="dropTeamPersonList" class="dropdown" runat="server">
                </select>
                <input id="hidSelectedTeamPerson" type="hidden" runat="server" />
            </p>
            <p class="row_P">
                <label>
                    备注:</label>
                <asp:TextBox CssClass="text ui-widget-content ui-corner-all" ID="txtTeamNote"
                    runat="server" />
            </p>
            <div class="ui-dialog-buttonpane ui-widget-content ui-helper-clearfix">
                <div class="ui-dialog-buttonset">
                    <asp:Button ID="btnSave" runat="server" Text="保 存" CssClass="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only"
                        OnClientClick="return checkForm();" OnClick="btnSave_Click" />
                    <input id="btnCancel" type="button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only"
                        value="取 消" />
                </div>
            </div>
        </div>
        <div id="MessageDialog">
            <p id="messageTips" class="row_P validateTips">
            </p>
            <div class="ui-dialog-buttonpane ui-widget-content ui-helper-clearfix">
                <div class="ui-dialog-buttonset">
                    <asp:Button ID="btnSumbit" runat="server" Text="确定" CssClass="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only"
                        OnClientClick="deleteSelectedItems();" OnClick="btnSumbit_Click" />
                    <input id="btnDeleteCancel" type="button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only"
                        value="取 消" />
                </div>
            </div>
        </div>

        <div class="layout">
            <div class="header">
                <h2>班组配置信息</h2>
            </div>
            <div class="editPanel">
                <ul>
                    <li>
                        <input id="btnCreate" type="button" value="新增" class="button" />
                    </li>
                    <li><span class="divide"></span></li>
                    <li>
                        <input id="btnUpdate" type="button" value="修改" class="button" />
                    </li>
                    <li><span class="divide"></span></li>
                    <li>
                        <input id="btnDelete" type="button" value="删除" class="button" />
                    </li>
                    <li><span class="divide"></span></li>
                    <li>
                        <asp:Button ID="btnRefresh" runat="server" Text="刷新" CssClass="button" OnClick="btnRefresh_Click" />
                    </li>
                    <li><span class="divide"></span></li>
                    <li>
                        <input id="btnClear" type="button" value="清空数据" class="button" />
                    </li>
                    <li><span class="divide"></span></li>
                    <li>
                        <asp:Button ID="btnBatch" runat="server" Text="批量处理" CssClass="button"
                            OnClick="btnBatch_Click" />
                    </li>
                </ul>
            </div>
            <div class="listPanel">
                <asp:GridView ID="gvValue" CssClass="GridViewStyle" Width="96%" runat="server" AutoGenerateColumns="False"
                    EmptyDataText="没有满足条件的数据">
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="选择">
                            <HeaderTemplate>
                                <input id="chbSelectAll" name="chkItem" type="checkbox" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input id="<%# string.Format("chb_{0}",Eval("TeamID"))%>" name="chkItem"
                                    type="checkbox" value="<%# Eval("TeamID")%>" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="单元组">
                            <ItemTemplate>
                                <label for="PlantName" id="<%# string.Format("{0}",Eval("PlantID"))%>">
                                    <%# Eval("PlantName")%></label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="运行值">
                            <ItemTemplate>
                                <label for="ShiftName" id="<%# string.Format("{0}",Eval("ShiftID"))%>">
                                    <%# Eval("ShiftName")%></label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="人员">
                            <ItemTemplate>
                                <label for="PersonName" id="<%# string.Format("{0}",Eval("PersonID"))%>">
                                    <%# Eval("PersonName")%></label>
                                <input id="hidCode" type="hidden" value="<%# string.Format("{0}/{1}/{2}",Eval("PersonID"),Eval("PositionID"),Eval("PositionName"))%>" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="岗位">
                            <ItemTemplate>
                                <label for="PositionName" id="<%# string.Format("{0}",Eval("PositionID"))%>">
                                    <%# Eval("PositionName")%></label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="替班人员">
                            <ItemTemplate>
                                <label for="TeamPersonName" id="<%# string.Format("{0}",Eval("TeamPersonID"))%>">
                                    <%# Eval("TeamPersonName")%></label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">
                            <ItemTemplate>
                                <label for="TeamNote" id="<%# string.Format("TeamNote&{0}",Eval("TeamID"))%>">
                                    <%# Eval("TeamNote")%></label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <asp:AspNetPager ID="Pager" runat="server" PageAlign="center" PageIndexBox="DropDownList"
                OnPageChanged="Pager_PageChanged" ButtonImageNameExtension="enable/" CustomInfoTextAlign="Center"
                DisabledButtonImageNameExtension="disable/" HorizontalAlign="Center" ImagePath="~/images/"
                MoreButtonType="Text" NavigationButtonType="Image" NumericButtonType="Text" PagingButtonType="Image"
                AlwaysShow="True" PagingButtonSpacing="8px" NumericButtonCount="5" EnableTheming="True"
                PageSize="20">
            </asp:AspNetPager>
        </div>
    </form>
</body>
</html>
