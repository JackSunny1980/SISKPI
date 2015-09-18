<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysParameterPage.aspx.cs"
    MasterPageFile="~/MasterPage/ContentMasterPage.Master" Inherits="SISKPI.KPISYS.SysParameterPage" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UP1" runat="Server">
        <ContentTemplate>
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <th>机组
                            </th>
                            <th>奖金金额
                            </th>
                            <th>单位
                            </th>
                            <th>奖励名次
                            </th>
                            <th>备注
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="tr1" onmouseover="javascript:this.className='tr3';" 
                                    onmouseout="javascript:this.className='tr1'">
                        <td class="VLine" align="center">
                            <asp:Literal runat="server" ID="lblSysName" Text='<%# Eval("SysName") %>' Visible="false" />
                            <asp:Literal runat="server" ID="lblSysCode" Text='<%# Eval("SysCode") %>' Visible="false" />
                            <asp:Literal runat="server" ID="lblSysDesc" Text='<%# Eval("SysDesc") %>' />
                        </td>
                        <td class="VLine" align="center">
                            <asp:TextBox runat="server" ID="txtSysValue" CssClass="inputCss" Text='<%# Eval("SysValue") %>' />
                        </td>
                        <td class="VLine" align="center">元
                        </td>
                        <td class="VLine" align="center">
                            <asp:TextBox runat="server" ID="txtSysValue2" CssClass="inputCss" Text='<%# Eval("SysValue2") %>' />
                        </td>
                        <td class="VLine" align="center">
                            <asp:TextBox runat="server" ID="txtSysNote" CssClass="inputCss" Text='<%# Eval("SysNote") %>'
                                Width="70%" />
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="tr2" onmouseover="javascript:this.className='tr3';" 
                                    onmouseout="javascript:this.className='tr2'">
                        <td class="VLine" align="center">
                            <asp:Literal runat="server" ID="lblSysName" Text='<%# Eval("SysName") %>' Visible="false" />
                            <asp:Literal runat="server" ID="lblSysCode" Text='<%# Eval("SysCode") %>' Visible="false" />
                            <asp:Literal runat="server" ID="lblSysDesc" Text='<%# Eval("SysDesc") %>' />
                        </td>
                        <td class="VLine" align="center">
                            <asp:TextBox runat="server" ID="txtSysValue" CssClass="inputCss" Text='<%# Eval("SysValue") %>' />
                        </td>
                        <td class="VLine" align="center">元
                        </td>
                        <td class="VLine" align="center">
                            <asp:TextBox runat="server" ID="txtSysValue2" CssClass="inputCss" Text='<%# Eval("SysValue2") %>' />
                        </td>
                        <td class="VLine" align="center">
                            <asp:TextBox runat="server" ID="txtSysNote" CssClass="inputCss" Text='<%# Eval("SysNote") %>'
                                Width="70%" />
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
    <p align="center">
        <asp:Button ID="btnSave" CssClass="buttonCss" runat="server" Text="保存" OnClick="btnSave_Click" />
    </p>
</asp:Content>
