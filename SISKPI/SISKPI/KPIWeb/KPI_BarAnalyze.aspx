<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_BarAnalyze.aspx.cs" Inherits="SISKPI.KPIWeb.KPI_BarAnalyze" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>指标分析</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/bootstrap-theme.css" rel="stylesheet" />
    <link href="../Content/themes/Styles/style.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="../ECharts/echarts/echarts.js" type="text/javascript"></script>
    <script src="../ECharts/WapCharts.js" type="text/javascript"></script>
    <script src="../ECharts/MyECharts.js" type="text/javascript"></script>
    <script src="JS/kpi-baranalyze.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="col-lg-12 col-sm-12" style="margin: 10px 0 0 0;">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <span class="glyphicon glyphicon-stats" aria-hidden="true"></span>指标分析
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div id="barChart" style="width: 100%; height: 450px;"></div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
