//注：此文件是调用图表函数文件，可自行定义，自行决定文件所要放的路径， 
//但要注意调整echarts: '../ECharts/echarts' 路径
// 路径配置
require.config({
    paths: {
        echarts: 'ECharts/echarts'
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
//获取竖向分组柱状图
//柱状图 【*】data:数据;  【*】divId:div id; 
//data:数据格式：{name：xxx,group:xxx,value:xxx}...
function GetBars(url, divId, cmd)
{
    $.ajax({
        url: url,
        data: { cmd: cmd},
        cache: false,
        async: false,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data) {
                DrawBars(data, divId)
            }
        },
        error: function (msg) {
            alert("系统发生错误");
        }
    });
}
//获取横向分组柱状图
//柱状图 【*】data:数据;  【*】divId:div id; 
//data:数据格式：{name：xxx,group:xxx,value:xxx}...
function GetHorBars(url, divId, cmd) {
    $.ajax({
        url: url,
        data: { cmd: cmd },
        cache: false,
        async: false,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data) {
                DrawHorBars(data, divId)
            }
        },
        error: function (msg) {
            alert("系统发生错误");
        }
    });
}

//获取竖向柱状图
//柱状图 【*】data:数据;  【*】divId:div id; 
//data:数据格式：{name：xxx,value:xxx}...
function GetBar(url, divId, cmd) {
    $.ajax({
        url: url,
        data: { cmd: cmd },
        cache: false,
        async: false,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data) {
                DrawBar(data, divId)
            }
        },
        error: function (msg) {
            alert("系统发生错误");
        }
    });
}
//获取饼图
//饼图  【*】data:数据;  【*】divid:div id;  title:饼图标题
//data:数据格式：{name：xxx,value:xxx}...
function GetPie(url, divId, title, cmd)
{
    $.ajax({
        url: url,
        data: { cmd: cmd },
        cache: false,
        async: false,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data) {
                DrawPie(data, divId, title)
            }
        },
        error: function (msg) {
            alert("系统发生错误");
        }
    });
}
//柱状图折线图混合  【*】data:数据;  【*】divId:div id;  xDes:柱状图描述;  yDes:折线描述
//data:数据格式：{name：xxx,value:xxx}...
function GetBarLine(url, divId, xDes, yDes, cmd) {
    $.ajax({
        url: url,
        data: { cmd: cmd },
        cache: false,
        async: false,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data) {
                DrawBarLine(data, divId, xDes, yDes)
            }
        },
        error: function (msg) {
            alert("系统发生错误");
        }
    });
}
//分组柱状图折线图混合  【*】data:数据;  【*】divId:div id;  xDes:柱状图描述;  yDes:折线描述
//data:数据格式：{name：xxx,group:xxx,value:xxx,lineflag:line}...
function GetBarsLine(url, divId, xDes, yDes, cmd) {
    $.ajax({
        url: url,
        data: { cmd: cmd },
        cache: false,
        async: false,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data) {
                DrawBarsLine(data, divId, xDes, yDes)
            }
        },
        error: function (msg) {
            alert("系统发生错误");
        }
    });
}
//折线图 【*】data:数据;  【*】divId:div id; 
//data:数据格式：{name：xxx，value:xxx}... 
function GetLine(url, divId, cmd) {
    $.ajax({
        url: url,
        data: { cmd: cmd },
        cache: false,
        async: false,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data) {
                DrawLine(data, divId)
            }
        },
        error: function (msg) {
            alert("系统发生错误");
        }
    });
}
//分组折线图 【*】data:数据;  【*】divId:div id; 
//data:数据格式：{name：xxx,group:xxx,value:xxx}... 
function GetLines(url, divId, cmd) {
    $.ajax({
        url: url,
        data: {cmd:cmd},
        cache: false,
        async: false,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data) {
                DrawLines(data, divId)
            }
        },
        error: function (msg) {
            alert("系统发生错误");
        }
    });
}
//仪表盘  【*】data:数据;  【*】id:div id; 
//data:数据格式：{name：xxx,value:xxx}...
function GetGauge(url, divId, cmd) {
    $.ajax({
        url: url,
        data: { cmd: cmd },
        cache: false,
        async: false,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data) {
                DrawGauge(data, divId)
            }
        },
        error: function (msg) {
            alert("系统发生错误");
        }
    });
}