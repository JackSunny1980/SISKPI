// 路径配置
require.config({
    paths: {
        echarts: '../ECharts/echarts'
    }
});

// 使用
require(
            [
                 'echarts',
                'echarts/chart/line',
                'echarts/chart/bar',
                'echarts/chart/pie',
                'echarts/chart/gauge',
                'echarts/chart/funnel'
            ]
        );

$(document).ready(
          function () {
              callService();
          });

function callService() {
    $.ajax({
        type:"POST",
        url: "KPI_BarAnalyze.aspx/GetAnalyzeData",
        data: {},
        cache: false,
        async: false,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d) {
                DrawBars(data.d, "barChart");
            }
        },
        error: function (msg) {
            alert("系统发生错误");
        }
    });
}