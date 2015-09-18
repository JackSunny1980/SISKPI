<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/ContentMasterPage.Master"
    CodeBehind="PersonScore.aspx.cs" Inherits="SISKPI.KPIWeb.PersonScore" %>

<asp:Content ContentPlaceHolderID="MainContent" ID="Content" runat="server">
    月份<asp:TextBox ID="txtYearMonth" runat="server" CssClass="inputCss Wdate"
        onfocus="WdatePicker({el:'txtYearMonth',dateFmt:'yyyy-MM',skin:'whyGreen'})" />
    <asp:DropDownList ID="ddlPosition" runat="server" CssClass="select" Width="120px" AutoPostBack="true">
    </asp:DropDownList>
    <asp:Button ID="btnSearch" runat="server" CssClass="buttonCss" Text="搜 索" OnClick="btnSearch_Click" />
    <asp:Button ID="Button1" runat="server" CssClass="buttonCss" Text="数据导出" OnClick="btnExport_Click" />
    <asp:UpdatePanel ID="UP1" runat="Server">
        <ContentTemplate>
            <asp:Repeater ID="PersonScoreRepeater" runat="server">
                <HeaderTemplate>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <thead>
                            <tr>
                                <th>值次
                                </th>
                                <th>姓名
                                </th>
                                <th>岗位
                                </th>
                                <th>权重
                                </th>
                                <th>得分
                                </th>
                                <th>奖金
                                </th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr id="<%# Eval("PersonID")%>" class="tr1" onmouseover="javascript:this.className='tr3';" 
                        onmouseout="javascript:this.className='tr1'">
                        <td id="Shift" runat="server" class="VLine" align="center">
                            <%# Eval("Shift")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("PersonName")%>  
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("PositionName")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("PositionWeight")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("Score")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("Bonus")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                     <tr id="<%# Eval("PersonID")%>"  class="tr2" onmouseover="javascript:this.className='tr3';" 
                         onmouseout="javascript:this.className='tr2'">
                        <td id="Shift" runat="server" class="VLine" align="center">
                            <%# Eval("Shift")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("PersonName")%>  
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("PositionName")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("PositionWeight")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("Score")%>
                        </td>
                        <td class="VLine" align="center">
                            <%# Eval("Bonus")%>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
           

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
