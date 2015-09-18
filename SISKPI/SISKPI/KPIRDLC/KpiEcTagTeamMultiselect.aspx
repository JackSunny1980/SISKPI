<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KpiEcTagTeamMultiselect.aspx.cs"
    Inherits="RdlcReportBasic.KpiTagTeamReport.KpiEcTagTeamMultiselect" 
    EnableEventValidation="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择指标</title>
    <base target="_self" />
</head>
<body style="margin: 0px 0px;">
    <form id="form1" runat="server">
    <div style="margin: 0px; width: 98%; text-align: center;">
        <table width="480px" class="noBoderTable" align="center">
            <tr style="width: 100%;">
                <td align="right">
                    <div style="vertical-align: top; width: 95%; height: 400px" id="div1">
                        <table width="98%" class="noBoderTable" align="center">
                            <tr style="width: 100%;">
                                <td align="right">
                                    <span style="float: left">
                                        <asp:Button ID="btnClose" runat="server" Width="60px" Text="取  消" OnClick="btnClose_Click" />
                                    </span><span style="float: right">
                                        <asp:Button ID="btnApply" runat="server" Width="60px" Text="确  认" OnClick="btnApply_Click" />
                                    </span>
                                </td>
                            </tr>
                            <tr style="width: 100%;">
                                <td align="right">
                                    <span style="float: right">
                                        <asp:Button ID="btnAll" runat="server" Width="60px" Text="全  选" OnClick="btnAll_Click" />
                                        <asp:Button ID="btnNot" runat="server" Width="60px" Text="全不选" OnClick="btnNot_Click" />
                                        <asp:Button ID="btnFei" runat="server" Width="60px" Text="反  选" OnClick="btnFei_Click" />
                                    </span>
                                </td>
                            </tr>
                            <tr style="width: 100%;">
                                <td align="left">
                                    <div style="vertical-align: top; border-style:inset;overflow: auto; width: 95%; height: 400px;" id="divtag">
                                        <p align="left">
                                            <asp:CheckBoxList ID="cbxKPIName" runat="server" Width="95%" 
                                                AutoPostBack="false">
                                            </asp:CheckBoxList>
                                        </p>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
