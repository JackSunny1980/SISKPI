<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_SATagSnapshotValue.aspx.cs"
    Inherits="SISKPI.KPI.KPI_SATagSnapshotValue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全指标实时得分</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap-theme.css" rel="stylesheet" />
    <link href="../Content/themes/Styles/style.css" rel="stylesheet" />
    <link href="../Content/main.css" rel="stylesheet" />
    <link href="../Content/themes/Styles/gridview.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript">
        function myrefresh() {
            window.location.reload();
        }
        setTimeout('myrefresh()', 60000); //指定1分钟刷新一次 
    </script>
</head>
<body>
    <form id="form1" runat="server">
       
             <div class="datagrid-title-panel">
                <div class="datagrid-title">
                    <img class="pull-left" src="/Content/Themes/Images/32/202323.png" width="25" height="25"  style="vertical-align: middle;">
                      <span class="pull-left">安全指标实时绩效</span>
                    <strong class="pull-right text-warning" >当前值次：<asp:Literal ID="lblShift" runat="server" /></strong>
                </div>
            </div>
            <asp:Repeater ID="SATagValueRepeater" runat="server">
                <HeaderTemplate>
                    <table style="width:100%"  class="datagrid table table-hover table-bordered table-condensed">
                        <tr>
                            <th>安全指标
                            </th>
                            <th>计算时间
                            </th>
                            <th>超限次数
                            </th>
                            <th>超限时长
                            </th>
                            <th>实时得分
                            </th>
                            <th>本值得分
                            </th>
                            <th>本月累计
                            </th>
                            <th>本年累计
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <%# DataBinder.Eval(Container.DataItem, "SAName")%>
                        </td>
                        <td align="center">
                            <%# DataBinder.Eval(Container.DataItem,"CalcDateTime") %>
                        </td>
                        <td align="center">
                            <%# DataBinder.Eval(Container.DataItem, "TotalCount")%>
                        </td>
                        <td align="center">
                            <%# DataBinder.Eval(Container.DataItem, "TotalDuration")%>
                        </td>
                        <td align="center">
                            <%# DataBinder.Eval(Container.DataItem,"SAScore") %>
                        </td>
                        <td align="center">
                            <%# DataBinder.Eval(Container.DataItem, "ShiftScore")%>
                        </td>
                        <td align="center">
                            <%# DataBinder.Eval(Container.DataItem, "MonthScore")%>
                        </td>
                        <td align="center">
                            <%# DataBinder.Eval(Container.DataItem, "YearScore")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
      
    </form>
</body>
</html>
