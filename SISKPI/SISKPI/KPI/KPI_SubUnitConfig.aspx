<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_SubUnitConfig.aspx.cs"
    Inherits="SISKPI.KPI_SubUnitConfig" %>

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
    </script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
    <div style="margin: 0px; width: 100%; text-align: center;">
        <table width="100%" class="table" id="table1" align="center">
            <tr style="width: 100%">
                <td width="100%" align="center">
                    <asp:Image ID="Image2" runat="server" Height="30px" ImageUrl="../imgs/logo.gif" />
                    <asp:Label ID="lblTitle" runat="server" Class="title" Text="机组信息配置"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%">
                <td width="100%" align="center">
                    <div style="width: 60%;">
                        <table class="table" width="100%">
                            <tr style="width: 100%;">
                                <td align="right" colspan="2">
                                    <asp:Button ID="btnCancel" runat="server" Width="80px" Text="关 闭" OnClick="btnCancel_Click" />
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td width="135px" align="left">
                                    电厂：
                                </td>
                                <td width="135px" align="left">
                                    <select style="width: 200px" runat="server" id="ddl_PlantID" name="ddlPlant">
                                    </select>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td width="135px" align="left">
                                    代码：
                                </td>
                                <td width="135px" align="left">
                                    <input id="txt_UnitCode" runat="server" style="width: 200px" type="text" />
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td width="135px" align="left">
                                    名称：
                                </td>
                                <td width="135px" align="left">
                                    <input id="txt_UnitName" runat="server" style="width: 200px" type="text" />
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td width="135px" align="left">
                                    描述：
                                </td>
                                <td width="135px" align="left">
                                    <input id="txt_UnitDesc" runat="server" style="width: 200px;" type="text" />
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td width="135px" align="left">
                                    序号：
                                </td>
                                <td width="135px" align="left">
                                    <input id="txt_UnitIndex" runat="server" style="width: 200px" type="text" />
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td width="135px" align="left">
                                    有效性：
                                </td>
                                <td width="135px" align="left">
                                    <select runat="server" id="ddl_UnitIsValid" style="width: 200px">
                                        <option value="0">无效</option>
                                        <option value="1">有效</option>
                                    </select>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td width="135px" align="left">
                                    前缀：
                                </td>
                                <td width="135px" align="left">
                                    <input id="txt_UnitPrefix" runat="server" style="width: 200px;" type="text" />
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td width="135px" align="left">
                                    额定负荷：
                                </td>
                                <td width="135px" align="left">
                                    <input id="txt_UnitMW" runat="server" style="width: 200px;" type="text" />
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td width="135px" align="left">
                                    负荷标签：
                                </td>
                                <td width="135px" align="left">
                                    <input id="txt_UnitMWTag" runat="server" style="width: 200px;" type="text" />
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td width="135px" align="left">
                                    运行条件：
                                </td>
                                <td width="135px" align="left">
                                    <input id="txt_UnitCondition" runat="server" style="width: 200px;" type="text" />需要符合PE计算公式
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td width="135px" align="left">
                                    考核：
                                </td>
                                <td width="135px" align="left">
                                    <select runat="server" id="ddl_UnitIsKPI" style="width: 200px">
                                        <option value="0">否</option>
                                        <option value="1">是</option>
                                    </select>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td width="135px" align="left">
                                    实时考核：
                                </td>
                                <td width="135px" align="left">
                                    <select runat="server" id="ddl_UnitIsSnapshot" style="width: 200px">
                                        <option value="0">否</option>
                                        <option value="1">是</option>
                                    </select>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td width="135px" align="left">
                                    排名考核：
                                </td>
                                <td width="135px" align="left">
                                    <select runat="server" id="ddl_UnitIsSort" style="width: 200px">
                                        <option value="0">否</option>
                                        <option value="1">是</option>
                                    </select>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td width="135px" align="left">
                                    安全考核：
                                </td>
                                <td width="135px" align="left">
                                    <select runat="server" id="ddl_UnitIsSecurity" style="width: 200px">
                                        <option value="0">否</option>
                                        <option value="1">是</option>
                                    </select>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td width="135px" align="left">
                                    发电考核：
                                </td>
                                <td width="135px" align="left">
                                    <select runat="server" id="ddl_UnitIsPower" style="width: 200px">
                                        <option value="0">否</option>
                                        <option value="1">是</option>
                                    </select>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td width="135px" align="left">
                                    倒班设置：
                                </td>
                                <td width="135px" align="left">
                                    <select style="width: 200px" runat="server" id="ddl_UnitWorkID">
                                    </select>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td width="135px" align="left">
                                    备注：
                                </td>
                                <td width="135px" align="left">
                                    <input id="txt_UnitNote" runat="server" style="width: 200px;" type="text" />
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td width="135px" align="left">
                                </td>
                                <td width="135px" align="left">
                                    <asp:Button ID="btnTagCheck" Width="100px" runat="server" Text="审核测点" OnClick="btnTagCheck_Click" />
                                </td>
                            </tr>
                            <tr style="width: 100%;">
                                <td align="right" colspan="2">
                                    <asp:Button ID="btnApply" runat="server" Width="80px" Text="应 用" OnClick="btnApply_Click" />
                                </td>
                            </tr>
                        </table>
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
