<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_OverLimitConfigSetting.aspx.cs"
    MasterPageFile="~/MasterPage/ContentMasterPage.Master" Inherits="SISKPI.KPIAlarm.KPI_OverLimitConfigSetting" %>


<asp:Content ID="Header" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        function btnDataImport() {
            var sURL = "OverLimitConfigDialog.aspx?rand=" + Math.random();
            var vArguments = "";
            var sFeatures = "dialogHeight=300px;dialogWidth=400px;center:yes;help:no;status:no;rsizable:yes";
            window.showModalDialog(sURL, vArguments, sFeatures);
            $("#btnRefresh").click();
        }

        $(function () {
            $("#tabs").tabs();
        });

        function onSelected(index) {
            $("#tabs").tabs("option", "active", index);
        }
    </script>
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    标签名称
    <asp:TextBox ID="txtSearchKey" CssClass="inputCss" runat="server" />
    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="buttonCss"
        OnClientClick="return checkSearch();"
        OnClick="btnSearch_Click" />
    <input type="button" class="buttonCss" value="批量导入" onclick="btnDataImport();" />
    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">配置信息</a></li>
            <li><a href="#tabs-2">超限配置录入</a></li>
        </ul>
        <div id="tabs-1">
            <asp:UpdatePanel ID="UP1" runat="server">
                <ContentTemplate>
                    <asp:Repeater ID="ConfigRepeater" runat="server">
                        <HeaderTemplate>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <th>序号</th>
                                    <th>测点名称</th>
                                    <th>低一限</th>
                                    <th>低二限</th>
                                    <th>低三限</th>
                                    <th>高一限</th>
                                    <th>高二限</th>
                                    <th>高三限</th>
                                    <th>高四限</th>
                                    <th>计算类型</th>
                                    <th>备注</th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="tr1" onmouseover="javascript:this.className='tr3';"
                                onmouseout="javascript:this.className='tr1'">
                                <td class="VLine" align="center"><%# Container.ItemIndex + 1 %></td>
                                <td class="VLine" align="center"><%#Eval("TagName") %></td>
                                <td class="VLine" align="center"><%#Eval("LowLimit1Value") %></td>
                                <td class="VLine" align="center"><%# Eval("LowLimit2Value")%></td>
                                <td class="VLine" align="center"><%# Eval("LowLimit3Value")%></td>
                                <td class="VLine" align="center"><%# Eval("FirstLimitingValue")%></td>
                                <td class="VLine" align="center"><%# Eval("SecondLimitingValue")%></td>
                                <td class="VLine" align="center"><%# Eval("ThirdLimitingValue")%></td>
                                <td class="VLine" align="center"><%# Eval("FourthLimitingValue")%></td>
                                <td class="VLine" align="center"><%# Eval("OverLimitComputeTypeText")%></td>
                                <td class="VLine" align="center">
                                    <asp:Button ID="btnDetail" runat="server" Text="查看" CommandName="Select" CssClass="buttonCss"
                                        OnClientClick="onSelected(1)" CommandArgument='<%#Eval("OverLimitConfigID") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="tr2" onmouseover="javascript:this.className='tr3';"
                                onmouseout="javascript:this.className='tr2'">
                                <td class="VLine" align="center"><%# Container.ItemIndex + 1 %></td>
                                <td class="VLine" align="center"><%#Eval("TagName") %></td>
                                <td class="VLine" align="center"><%#Eval("LowLimit1Value") %></td>
                                <td class="VLine" align="center"><%# Eval("LowLimit2Value")%></td>
                                <td class="VLine" align="center"><%# Eval("LowLimit3Value")%></td>
                                <td class="VLine" align="center"><%# Eval("FirstLimitingValue")%></td>
                                <td class="VLine" align="center"><%# Eval("SecondLimitingValue")%></td>
                                <td class="VLine" align="center"><%# Eval("ThirdLimitingValue")%></td>
                                <td class="VLine" align="center"><%# Eval("FourthLimitingValue")%></td>
                                <td class="VLine" align="center"><%# Eval("OverLimitComputeTypeText")%></td>
                                <td class="VLine" align="center">
                                    <asp:Button ID="btnDetail" runat="server" Text="查看" CommandName="Select" CssClass="buttonCss"
                                        OnClientClick="onSelected(1)" CommandArgument='<%#Eval("OverLimitConfigID") %>' />
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
            <asp:UpdatePanel ID="UP2" runat="server">
                <ContentTemplate>


                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="HVLine">测点名称</td>
                            <td class="HVLine">
                                <asp:DropDownList ID="drpTags" runat="server" />
                            </td>
                            <td class="HVLine">计算类型</td>
                            <td class="HVLine">
                                <asp:RadioButton ID="rbFixedValue" runat="server" Text="固定值" Checked="True" GroupName="CheckOne" />
                                <asp:RadioButton ID="rbRealTimeValue" runat="server" Text="实时曲线" GroupName="CheckOne" />
                            </td>
                        </tr>
                        <tr>
                            <td class="VLine">低一限值</td>
                            <td class="VLine">
                                <asp:TextBox CssClass="inputCss" ID="txtLowLimit1Value"
                                    runat="server" /></td>
                            <td class="VLine">低二限值</td>
                            <td class="VLine">
                                <asp:TextBox CssClass="inputCss" ID="txtLowLimit2Value"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="VLine">低三限值</td>
                            <td class="VLine">
                                <asp:TextBox CssClass="inputCss" ID="txtLowLimit3Value"
                                    runat="server" /></td>
                            <td class="VLine">高一限值</td>
                            <td class="VLine">
                                <asp:TextBox CssClass="inputCss" ID="txtFirstLimiting"
                                    runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="VLine">高二限值</td>
                            <td class="VLine">
                                <asp:TextBox CssClass="inputCss" ID="txtSecondLimiting"
                                    runat="server" /></td>
                            <td class="VLine">高三限值</td>
                            <td class="VLine">
                                <asp:TextBox CssClass="inputCss" ID="txtThirdLimiting"
                                    runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="VLine">高四限值</td>
                            <td class="VLine">
                                <asp:TextBox CssClass="inputCss" ID="txtFourthLimiting"
                                    runat="server" /></td>
                            <td class="VLine">备注</td>
                            <td class="VLine">
                                <asp:TextBox CssClass="inputCss" ID="txtComment" runat="server" /></td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center" class="VLine">
                                <asp:Button CssClass="buttonCss" ID="btnNew" runat="server" Text="新建" OnClick="btnNew_Click" />
                                <asp:Button CssClass="buttonCss" ID="btnEdit" runat="server" Text="编辑" OnClick="btnEdit_Click" />
                                <asp:Button CssClass="buttonCss" ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_Click"
                                    OnClientClick="javascript:return confirm('你确定要删除该数据吗？')" />
                                <asp:Button CssClass="buttonCss" ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
                                <asp:Button CssClass="buttonCss" ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <input id="hidFlag" type="hidden" runat="server" />
    <input id="hidCode" type="hidden" runat="server" />
    <input id="hidMultipleCode" type="hidden" runat="server" />
</asp:Content>
