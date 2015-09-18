<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_PersonConfig.aspx.cs"
    MasterPageFile="~/MasterPage/ContentMasterPage.Master" Inherits="SISKPI.KPI_PersonConfig" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">
        $(function () {
            $("#tabs").tabs();
        });

        function onSelected(index) {
            $("#tabs").tabs("option", "active", index);
        }
    </script>
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">


    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">人员信息</a></li>
            <li><a href="#tabs-2">人员信息录入</a></li>
        </ul>
        <div id="tabs-1">
            专业<asp:DropDownList ID="drpSSpecialFields" runat="server" Width="120px">
                <asp:ListItem Value="1">机组</asp:ListItem>
                <asp:ListItem Value="2">脱硫</asp:ListItem>
                <asp:ListItem Value="3">化学</asp:ListItem>
                <asp:ListItem Value="4">输煤</asp:ListItem>
                <asp:ListItem Value="5">全厂主管</asp:ListItem>
            </asp:DropDownList>
            机组:<asp:DropDownList ID="drpSUnits" runat="server" />
            值次:<asp:DropDownList ID="drpSShifts" runat="server" />
            <asp:Button ID="btnSearch" runat="server" Text="检索" OnClick="btnSearch_Click" CssClass="buttonCss" />
            <asp:UpdatePanel ID="UP1" runat="Server">
                <ContentTemplate>
                    <asp:Repeater ID="PersonRepeater" runat="server" OnItemCommand="PersonItemCommand">
                        <HeaderTemplate>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr class="GridViewHeaderStyle">
                                    <th>序号
                                    </th>
                                    <th>工号
                                    </th>
                                    <th>姓名
                                    </th>
                                    <th>专业
                                    </th>
                                    <th>机组
                                    </th>
                                    <th>值次
                                    </th>
                                    <th>岗位
                                    </th>
                                    <th>是否启用
                                    </th>
                                    <th>操作
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="tr1" onmouseover="javascript:this.className;this.className='tr3';"
                                            onmouseout="javascript:this.className='tr1'">
                                <td class="VLine" align="center">
                                    <%# Container.ItemIndex + 1 %>
                                    <asp:Literal ID="lblPersonID" runat="server" Text=' <%# DataBinder.Eval(Container.DataItem, "PersonID")%>'
                                        Visible="false" />
                                </td>
                                <td class="VLine" align="center">
                                    <%# DataBinder.Eval(Container.DataItem, "PersonCode")%>
                                </td>
                                <td class="VLine" align="center">
                                    <%# DataBinder.Eval(Container.DataItem, "PersonName")%>
                                </td>
                                <td class="VLine" align="center">
                                    <%# DataBinder.Eval(Container.DataItem, "SpecialField")%>
                                </td>
                                <td class="VLine" align="center">
                                    <%# DataBinder.Eval(Container.DataItem,"UnitName") %>
                                </td>
                                <td class="VLine" align="center">
                                    <%# DataBinder.Eval(Container.DataItem,"Shift") %>
                                </td>
                                <td class="VLine" align="center">
                                    <%# DataBinder.Eval(Container.DataItem, "PositionName")%>
                                </td>
                                <td class="VLine" align="center">
                                    <%# IsUsed(DataBinder.Eval(Container.DataItem, "PersonIsValid"))%>
                                </td>
                                <td class="VLine" align="center">
                                    <asp:Button ID="btnDetail" runat="server" Text="查看" CommandName="Select" CssClass="buttonCss"
                                        OnClientClick="onSelected(1)" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="tr2" onmouseover="javascript:this.className;this.className='tr3';"
                                            onmouseout="javascript:this.className='tr2'">
                                <td class="VLine" align="center">
                                    <%# Container.ItemIndex + 1 %>
                                    <asp:Literal ID="lblPersonID" runat="server" Text=' <%# DataBinder.Eval(Container.DataItem, "PersonID")%>'
                                        Visible="false" />
                                </td>
                                <td class="VLine" align="center">
                                    <%# DataBinder.Eval(Container.DataItem, "PersonCode")%>
                                </td>
                                <td class="VLine" align="center">
                                    <%# DataBinder.Eval(Container.DataItem, "PersonName")%>
                                </td>
                                <td class="VLine" align="center">
                                    <%# DataBinder.Eval(Container.DataItem, "SpecialField")%>
                                </td>
                                <td class="VLine" align="center">
                                    <%# DataBinder.Eval(Container.DataItem,"UnitName") %>
                                </td>
                                <td class="VLine" align="center">
                                    <%# DataBinder.Eval(Container.DataItem,"Shift") %>
                                </td>
                                <td class="VLine" align="center">
                                    <%# DataBinder.Eval(Container.DataItem, "PositionName")%>
                                </td>
                                <td class="VLine" align="center">
                                    <%# IsUsed(DataBinder.Eval(Container.DataItem, "PersonIsValid"))%>
                                </td>
                                <td class="VLine" align="center">
                                    <asp:Button ID="btnDetail" runat="server" Text="查看" CommandName="Select" CssClass="buttonCss"
                                        OnClientClick="onSelected(1)" />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:AspNetPager ID="Pager" runat="server" PageAlign="center" PageIndexBox="DropDownList"
                        OnPageChanged="Pager_PageChanged" ButtonImageNameExtension="enable/" CustomInfoTextAlign="Center"
                        DisabledButtonImageNameExtension="disable/" HorizontalAlign="Center" ImagePath="~/images/"
                        MoreButtonType="Text" NavigationButtonType="Image" NumericButtonType="Text" PagingButtonType="Image"
                        AlwaysShow="True" PagingButtonSpacing="8px" NumericButtonCount="5" EnableTheming="True"
                        PageSize="20">
                    </asp:AspNetPager>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div id="tabs-2">
            <asp:UpdatePanel ID="UP2" runat="Server">
                <ContentTemplate>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="HVLine">人员工号
                            </td>
                            <td class="HVLine">
                                <asp:TextBox CssClass="inputCss" ID="txtPersonCode" runat="server" />
                            </td>
                            <td class="HVLine">人员姓名
                            </td>
                            <td class="HVLine">
                                <asp:TextBox CssClass="inputCss" ID="txtPersonName" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="VLine">专业
                            </td>
                            <td class="VLine">
                                <asp:DropDownList ID="drpSpecialFields" runat="server" Width="120px">
                                    <asp:ListItem Value="1">机组</asp:ListItem>
                                    <asp:ListItem Value="2">脱硫</asp:ListItem>
                                    <asp:ListItem Value="3">化学</asp:ListItem>
                                    <asp:ListItem Value="4">输煤</asp:ListItem>
                                    <asp:ListItem Value="5">全厂主管</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="VLine">岗位
                            </td>
                            <td class="VLine">
                                <asp:DropDownList ID="drpPositions" runat="server" Width="120px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="VLine">值次
                            </td>
                            <td class="VLine">
                                <asp:DropDownList ID="drpShifts" runat="server" Width="120px" />
                            </td>
                            <td class="VLine">机组
                            </td>
                            <td class="VLine">
                                <asp:DropDownList ID="drpUnits" runat="server" Width="120px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="VLine">描述
                            </td>
                            <td class="VLine">
                                <asp:TextBox CssClass="inputCss" ID="txtPersonDesc" runat="server" />
                            </td>
                            <td class="VLine">是否启用
                            </td>
                            <td class="VLine">
                                <asp:CheckBox ID="chkPersonIsValid" runat="server" Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="VLine">备注
                            </td>
                            <td class="VLine" colspan="3">
                                <asp:TextBox CssClass="inputCss" TextMode="MultiLine" Height="50px" Width="98%" ID="txtPersonNote"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center" class="VLine">
                                <asp:Button CssClass="buttonCss" ID="btnNew" runat="server" Text="新建" OnClick="btnNewPerson_Click" />
                                <asp:Button CssClass="buttonCss" ID="btnEdit" runat="server" Text="编辑" OnClick="btnEditPerson_Click" />
                                <asp:Button CssClass="buttonCss" ID="btnDelete" runat="server" Text="删除" OnClick="btnDeletePerson_Click"
                                    OnClientClick="javascript:return confirm('你确定要删除该数据吗？')" />
                                <asp:Button CssClass="buttonCss" ID="btnSave" runat="server" Text="保存" OnClick="btnSavePerson_Click" />
                                <asp:Button CssClass="buttonCss" ID="btnCancel" runat="server" Text="取消" OnClick="btnCancelPerson_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
