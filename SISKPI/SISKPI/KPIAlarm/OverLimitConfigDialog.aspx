<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OverLimitConfigDialog.aspx.cs" Inherits="SISKPI.KPIAlarm.OverLimitConfigDialog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>超限配置导入</title>
    <base target="_self">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap-theme.css" rel="stylesheet" />
    <link href="../Content/themes/Styles/style.css" rel="stylesheet" />
    <link href="../Content/main.css" rel="stylesheet" />
    <script type="text/javascript">
        function ShowMessage() {
            alert("正在导入数据请耐心等待！");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <p class="text-center">
            <asp:FileUpload ID="FileUpload1" runat="server" />
        </p>
         <p class="text-center">
            <asp:Button ID="btnDataImport" runat="server" Text="导入" CssClass="btn btn-default" 
                OnClick="btnDataImport_Click" OnClientClick="ShowMessage();" />
            <input type="button" value="关闭" class="btn btn-default" onclick="window.close();" />
        </p>       
        <p>
            <asp:Label ID="lblError" runat="server" CssClass="text-danger" />
        </p>
    </form>
</body>
</html>
