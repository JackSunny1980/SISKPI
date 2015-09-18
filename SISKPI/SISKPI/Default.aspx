<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SISKPI.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>厂级监控信息系统(SIS)</title>
    <link rel="shortcut icon" href="favicon.ico" />
    <link href="CSS/MainCSS.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        body
        {
            margin-left: 0px;
            margin-top: 0px;
        }
        .STYLE1
        {
            font-size: 12px;
            color: #333333;
        }
        .STYLE3
        {
            font-size: 12px;
            margin-left: 10px;
            color: #6699FF;
        }
        a.STYLE3:hover
        {
            text-decoration: none;
        }
        a
        {
            font-size: 12px;
            color: #66CCFF;
        }
        a:hover
        {
            color: #3366FF;
        }
        a:link
        {
            color: #3366FF;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function openUrl(url) {
            window.opener = null;
            window.close();
            window.open(url, '_blank', 'top=0,left=0,status=yes,menubar=no,resizable=yes,scrollbars=yes,width=' + window.screen.width + ',height=' + window.screen.height);
        }       
    </script>
</head>
<body oncontextmenu="return false;">
    <form id="form1" runat="server">
    <div>
        <table  class="table"   border="0" align="center" cellpadding="0" cellspacing="0" background="Images/bg.jpg"
            style="height:746px; width:1024px;background-repeat: no-repeat;">
            <tr style="height: 260px">
                <td height="240px" valign="bottom">
                    <table  class="table"   width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="78" valign="bottom">
                                <table border="0" cellspacing="0" cellpadding="0" style="margin-left: 160px">
                                    <tr>
                                        <td>
                                            <img src="Images/logo.gif" width="334" height="60" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="bottom">
                                <table width="277" height="50" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="42">
                                            <span class="STYLE1">帐户</span>
                                        </td>
                                        <td width="235">
                                            <label>
                                                <asp:TextBox ID="TB_UserName" runat="server" CssClass="STYLE1" Text="sisdemo" Width="128px"></asp:TextBox>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="STYLE1">
                                            密码
                                        </td>
                                        <td>
                                            <label>
                                                <asp:TextBox ID="TB_Password" runat="server" CssClass="STYLE1" TextMode="password"
                                                    Width="128px"></asp:TextBox>
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr>
                            <td width="52%" height="42" align="center">
                                &nbsp;
                            </td>
                            <td width="48%" valign="bottom">
                                <table border="0" cellspacing="0" cellpadding="0"  class="table"  >
                                    <tr>
                                        <td width="277">
                                            <asp:ImageButton ID="IBnt_Login" runat="server" Width="76px" Height="31px" 
                                                ImageUrl="~/Images/button.gif" onclick="IBnt_Login_Click"
                                                />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height: 486px">
                <td height="486px">
                    &nbsp;
                </td>
            </tr><%----%>
        </table>
    </div>
    </form>
</body>
</html>
