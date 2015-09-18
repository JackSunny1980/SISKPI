<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/ContentMasterPage.Master"
    CodeBehind="PersonScoreDetail.aspx.cs" Inherits="SISKPI.KPIWeb.PersonScoreDetail" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <h3>个人得分明细
    </h3>
    <asp:Repeater runat="server" ID="DetailRepeater">
        <HeaderTemplate>
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th>日期
                    </th>
                    <th>得分
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="tr1" onmouseover="javascript:this.className='tr3';"
                onmouseout="javascript:this.className='tr1'">
                <td class="VLine" align="center"><%# Eval("CheckDate") %> </td>
                <td class="VLine" align="center"><%# Eval("Score") %></td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="tr2" onmouseover="javascript:this.className='tr3';"
                onmouseout="javascript:this.className='tr2'">
                <td class="VLine" align="center"><%# Eval("CheckDate") %></td>
                <td class="VLine" align="center"><%# Eval("Score") %></td>
            </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
