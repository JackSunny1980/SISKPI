<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_CurveTagConfig.aspx.cs"
    Inherits="SISKPI.KPI_CurveTagConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="mainWindow">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SISKPI</title>
    <link href="../CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" href="../js/tab/jquery.tabs.css" type="text/css" media="print, projection, screen" />
    <!--[if lte IE 7]>
        <link rel="stylesheet" href="../js/tab/jquery.tabs-ie.css" type="text/css" media="projection, screen">
        <![endif]-->
    <script src="../js/ProcessBar.js" type="text/javascript"></script>
    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../js/Common.js" type="text/javascript"></script>
    <script src="../js/tab/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="../js/Tab/jquery.tabs.js" type="text/javascript"></script>
    <script src="../js/Config.js" type="text/javascript"></script>
    <script src="../js/GridViewColor.js" type="text/javascript"></script>
    <style type="text/css">
        .lblstyle2
        {
            border: #D87E26 0px solid;
            cursor: hand;
            color: black;
            background: none;
            font-size: 10pt;
            font-weight: bolder;
            width: 100%;
        }
        
        .calcstyle
        {
            border: White 1px solid;
            cursor: hand;
            color: Blue;
            background: White;
            font-size: 10pt;
            font-weight: bolder;
            width: 30px;
        }
    </style>
    <script type="text/javascript">
        function apply() {
            window.opener = null;
            window.close();
        }

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

        function checkLeave() {
            //如果txtFlag的值不为0则提示
            if (document.getElementById("txtFlag").value != "0")
                event.returnValue = "页面值已经修改，是否真的不保存？";
        }

        //onbeforeunload="checkLeave()"

    </script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
    <div style="margin: 0px; width: 100%; text-align: center;">
        <table width="100%" class="table" id="table1" align="center">
            <tr style="width: 100%;">
                <td width="100%" align="center">
                    <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                    <asp:Label ID="lblTitle" runat="server" Class="title" Text="曲线指标配置"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%">
                <td width="100%" align="center" valign="top">
                    <div style="width: 95%;">
                        <fieldset class="field_info" style="width: 100%;">
                            <legend>曲线指标</legend>
                            <asp:GridView ID="gvCurve" CssClass="GridViewStyle" Width="100%" runat="server" AutoGenerateColumns="False"
                                EmptyDataText="没有满足条件的数据" AllowPaging="False" OnRowDataBound="gvCurve_RowDataBound"
                                OnRowCommand="gvCurve_RowCommand" OnRowCancelingEdit="gvCurve_RowCancelingEdit"
                                OnRowEditing="gvCurve_RowEditing" OnRowUpdating="gvCurve_RowUpdating">
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <RowStyle CssClass="GridViewRowStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <HeaderStyle CssClass="GridViewHeaderStyle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <input type="hidden" runat="server" id="curveid" value='<%# Eval("CurveID").ToString()%>' />
                                            <%#  Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                        <HeaderStyle Width="40px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CurveCode" HeaderText="代码" ItemStyle-HorizontalAlign="Center">
                                        <ControlStyle Width="100px"></ControlStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CurveName" HeaderText="名称" ItemStyle-HorizontalAlign="Center">
                                        <ControlStyle Width="200px"></ControlStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CurveDesc" HeaderText="描述" ItemStyle-HorizontalAlign="Center">
                                        <ControlStyle Width="200px"></ControlStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CurveGroup" HeaderText="组别" ItemStyle-HorizontalAlign="Center">
                                        <ControlStyle Width="100px"></ControlStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CurveMonth" HeaderText="有效月份" ItemStyle-HorizontalAlign="Center">
                                        <ControlStyle Width="200px"></ControlStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="是否有效" ItemStyle-HorizontalAlign="Center">
                                        <ControlStyle Width="40px"></ControlStyle>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlValid" runat="server" DataSourceID="GetYesNo" DataTextField="Name"
                                                DataValueField="Value">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="config" runat="server" Text="选择" CommandName="dataConfig" CommandArgument='<%# Eval("CurveID") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="copy" runat="server" Text="拷贝" CommandName="dataCopy" CommandArgument='<%# Eval("CurveID") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                    </asp:TemplateField>
                                    <asp:CommandField HeaderText="配置" ShowEditButton="True" HeaderStyle-Width="60px"
                                        ItemStyle-HorizontalAlign="Center" EditText="编辑" />
                                    <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="delete" runat="server" Text="删除" CommandName="dataDelete" CommandArgument='<%# Eval("CurveID") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </fieldset>
                    </div>
                </td>
            </tr>
            <tr style="width: 100%">
                <td width="100%" align="center" valign="top">
                    <asp:Label ID="Label1" runat="server" Text="相同组别下的曲线类型应完全一致!!"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%">
                <td width="100%" align="center" valign="top">
                    <asp:Button ID="btnAddCurve" runat="server" Text="新增曲线" Width="120px" OnClick="btnAddCurve_Click" />
                    <asp:ObjectDataSource ID="GetYesNo" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetData" TypeName="SISKPI.KPIDataSetTableAdapters.GetYesNoTableAdapter">
                    </asp:ObjectDataSource>
                </td>
            </tr>
            <tr runat="server">
                <td align="center" style="width: 100%">
                    <div style="width: 95%;">
                        <table style="width: 100%;">
                            <tr runat="server" style="width: 100%;">
                                <td align="left" style="width: 100%;">
                                    <asp:Label ID="lblInfor" runat="server" Width="400px" ForeColor="Red" Font-Bold="true"
                                        Text="当前曲线:无"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr style="width: 100%">
                <td width="100%" align="center" valign="top">
                    <div style="width: 95%;">
                        <fieldset class="field_info" style="width: 100%;">
                            <legend>曲线配置</legend>
                            <table style="width: 100%;" class="table">
                                <tr style="width: 100%;">
                                    <td style="width: 100%; vertical-align: top;">
                                        <table style="width: 100%;" class="table">
                                            <tr runat="server" id="tr3">
                                                <td align="left" style="width: 60px">
                                                    <asp:Label ID="Label7" runat="server" Width="60px" Text="区间类型:"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 100px">
                                                    <asp:DropDownList ID="ddl_CurveType" Width="100px" runat="server" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddl_CurveType_SelectedIndexChanged">
                                                        <asp:ListItem Value="-1" Text="无" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="0" Text="定值"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="一维"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="二维"></asp:ListItem>
                                                        <%--<asp:ListItem Value="3" Text="三维" Enabled="false"></asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left" style="width: 60px">
                                                    <asp:Label ID="lblGetType" runat="server" Width="60px" Text="取值类型:"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 100px">
                                                    <asp:DropDownList ID="ddl_CurveGetType" Width="100px" runat="server">
                                                        <asp:ListItem Value="-1" Text="向下取值"></asp:ListItem>
                                                        <asp:ListItem Value="0" Text="线性插值" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="向上取值"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left" style="width: 60px">
                                                </td>
                                                <td align="left" style="width: 100px">
                                                </td>
                                                <td align="right">
                                                    <span style="float: left">
                                                        <asp:Button ID="btnXLineDel" Width="80px" runat="server" Text="清 除" OnClick="btnXLineDel_Click" />
                                                    </span><span style="float: right">
                                                        <asp:Button ID="btnXLineSave" Width="80px" runat="server" Text="保 存" OnClick="btnXLineSave_Click" />
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="tr1">
                                                <td align="left" style="width: 60px">
                                                    <span runat="server" id="spanX" style="float: inherit">
                                                        <asp:Label ID="Label4" runat="server" Width="60px" Text="X基准:"></asp:Label>
                                                    </span>
                                                </td>
                                                <td align="left" style="width: 100px">
                                                    <span runat="server" id="spanXX" style="float: inherit">
                                                        <asp:DropDownList ID="ddl_CurveXRealTag" Width="100px" runat="server">
                                                        </asp:DropDownList>
                                                    </span>
                                                </td>
                                                <td align="left" style="width: 60px">
                                                    <span runat="server" id="spanY" style="float: inherit">
                                                        <asp:Label ID="Label9" runat="server" Width="60px" Text="Y基准:"></asp:Label>
                                                    </span>
                                                </td>
                                                <td align="left" style="width: 100px">
                                                    <span runat="server" id="spanYY" style="float: inherit">
                                                        <asp:DropDownList ID="ddl_CurveYRealTag" Width="100px" runat="server">
                                                        </asp:DropDownList>
                                                    </span>
                                                </td>
                                                <td align="left" style="width: 60px">
                                                    <%--<span runat="server" id="spanZ" style="float: inherit; visibility: hidden;">
                                                        <asp:Label ID="Label12" runat="server" Width="60px" Text="Z基准"></asp:Label>
                                                    </span>--%>
                                                </td>
                                                <td align="left" style="width: 60px">
                                                    <%--<span runat="server" id="spanZZ" style="float: inherit; visibility: hidden;">
                                                        <asp:DropDownList ID="ddl_CurveZRealTag" Width="100px" runat="server">
                                                        </asp:DropDownList>
                                                    </span>--%>
                                                </td>
                                                <td align="right">
                                                    <span style="float: left">
                                                        <asp:Button ID="btnAddX" Width="80px" runat="server" Text="新增列" OnClick="btnAddX_Click" />
                                                    </span><span style="float: inherit"></span><span style="float: right;">
                                                        <asp:Button ID="btnAddY" Width="80px" runat="server" Text="新增行" OnClick="btnAddY_Click" />
                                                    </span>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width: 100%;" class="table">
                                            <tr id="Tr2" runat="server">
                                                <td align="center">
                                                    <asp:TextBox ID="tbxXLineXYZ" runat="server" Text="0.00"></asp:TextBox>
                                                    <asp:GridView ID="gvXLine" CssClass="GridViewStyle" Width="100%" runat="server" AutoGenerateColumns="True"
                                                        EmptyDataText="没有满足条件的数据" AllowPaging="False" OnRowDataBound="gvXLine_RowDataBound"
                                                        OnRowCommand="gvXLine_RowCommand" OnRowCancelingEdit="gvXLine_RowCancelingEdit"
                                                        OnRowEditing="gvXLine_RowEditing" OnRowUpdating="gvXLine_RowUpdating">
                                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                                        <RowStyle CssClass="GridViewRowStyle" />
                                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                                        <PagerStyle CssClass="GridViewPagerStyle" />
                                                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                                        <Columns>
                                                            <%--<asp:TemplateField HeaderText="序号">
                                                                <ItemTemplate>
                                                                    <input type="hidden" runat="server" id="xlineid" value='<%# Eval("XLineID").ToString()%>' />
                                                                    <%#  Container.DataItemIndex%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="40px" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>--%>
                                                            <asp:CommandField HeaderText="配置" ItemStyle-HorizontalAlign="Center" ShowEditButton="True"
                                                                EditText="编辑" />
                                                            <asp:TemplateField HeaderText="配置" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="delete" runat="server" Text="删除" CommandName="dataDelete" CommandArgument='<%# Container.DataItemIndex %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td style="width: 100%; vertical-align: top;" align="center">
                                                </td>
                                            </tr>
                                            <tr runat="server">
                                                <td align="center">
                                                    <%--<asp:Button ID="btnClear" Width="120px" runat="server" Text="清空" OnClick="btnDelA_Click" />
                                                                <asp:Button ID="btnCopy" Width="120px" runat="server" Text="拷贝" OnClick="btnCopyA_Click" />--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
            </tr>
            <tr style="width: 100%">
                <td width="100%" align="center" valign="top">
                    <div style="width: 95%;">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="Lay1" style="z-index: 100; visibility: hidden; left: 1%; width: 260px; cursor: crosshair;
        position: absolute; top: -20px; height: 14%; background-color: transparent; text-align: center">
        <br />
        <b></b>
        <table align="center" style="width: 260px; height: 45px" class="BoderTable">
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
