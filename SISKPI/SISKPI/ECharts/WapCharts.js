//饼图  【*】data:数据;  【*】id:div id;  name:饼图名称
//data:数据格式：{name：xxx,value:xxx}...
function DrawPie(data, id, name) {

    var option = ECharts.ChartOptionTemplates.Pie(data, name);
    var container = document.getElementById(id);
    opt = ECharts.ChartConfig(container, option);
    ECharts.Charts.RenderChart(opt);

}
//柱状图 【*】data:数据;  【*】id:div id; 
//data:数据格式：{name：xxx,value:xxx}...
function DrawBar(data, id) {

    var option = ECharts.ChartOptionTemplates.Bar(data);
    var container = document.getElementById(id);
    opt = ECharts.ChartConfig(container, option);
    ECharts.Charts.RenderChart(opt);

}
//分组柱状图 【*】data:数据;  【*】id:div id; 
//data:数据格式：{name：xxx,group:xxx,value:xxx}...
function DrawBars(data, id) {

    var option = ECharts.ChartOptionTemplates.Bars(data);
    var container = document.getElementById(id);
    opt = ECharts.ChartConfig(container, option);
    ECharts.Charts.RenderChart(opt);

}

//横向分组柱状图 【*】data:数据;  【*】id:div id; 
//data:数据格式：{name：xxx,group:xxx,value:xxx}...
function DrawHorBars(data, id) {

    var option = ECharts.ChartOptionTemplates.HorBars(data);
    var container = document.getElementById(id);
    opt = ECharts.ChartConfig(container, option);
    ECharts.Charts.RenderChart(opt);

}
//柱状图折线图混合  【*】data:数据;  【*】id:div id;  xDes:柱状图描述;  yDes:折线描述
//data:数据格式：{name：xxx,value:xxx}...
function DrawBarLine(data, id, xDes, yDes) {
    var option = ECharts.ChartOptionTemplates.BarLine(data, xDes, yDes);
    var container = document.getElementById(id);
    opt = ECharts.ChartConfig(container, option);
    ECharts.Charts.RenderChart(opt);

}

//分组柱状图折线图混合  【*】data:数据;  【*】id:div id;  xDes:柱状图描述;  yDes:折线描述
//data:数据格式：{name：xxx,group:xxx,value:xxx,lineflag:line}...
function DrawBarsLine(data, id, xDes, yDes) {
    //格式
    var option = ECharts.ChartOptionTemplates.BarsLine(data, xDes, yDes);
    var container = document.getElementById(id);
    opt = ECharts.ChartConfig(container, option);
    ECharts.Charts.RenderChart(opt);

}

//折线图 【*】data:数据;  【*】id:div id; 
//data:数据格式：{name：xxx，value:xxx}... 
function DrawLine(data, id) {

    var option = ECharts.ChartOptionTemplates.Line(data);
    var container = document.getElementById(id);
    opt = ECharts.ChartConfig(container, option);
    ECharts.Charts.RenderChart(opt);
}

//分组折线图 【*】data:数据;  【*】id:div id; 
//data:数据格式：{name：xxx,group:xxx,value:xxx}... 
function DrawLines(data, id) {

    var option = ECharts.ChartOptionTemplates.Lines(data);
    var container = document.getElementById(id);
    opt = ECharts.ChartConfig(container, option);
    ECharts.Charts.RenderChart(opt);
}

//仪表盘  【*】data:数据;  【*】id:div id; 
//data:数据格式：{name：xxx,value:xxx}...
function DrawGauge(data, id) {

    var option = ECharts.ChartOptionTemplates.Gauge(data);
    var container = document.getElementById(id);
    opt = ECharts.ChartConfig(container, option);
    ECharts.Charts.RenderChart(opt);

}

//漏斗图  【*】data:数据;  【*】id:div id; 
//data:数据格式：{name：xxx,value:xxx}...
function DrawFunnel(ydata, sdata, id) {

    var option = ECharts.ChartOptionTemplates.Funnel(ydata, sdata);
    var container = document.getElementById(id);
    opt = ECharts.ChartConfig(container, option);
    ECharts.Charts.RenderChart(opt);

}

//饼图漏斗图 【*】data:数据;  【*】id:div id; 
//data:数据格式：{name：xxx,value:xxx}...
function DrawPieFunnel(data, id) {

    var option = ECharts.ChartOptionTemplates.Piefunnel(data);
    var container = document.getElementById(id);
    opt = ECharts.ChartConfig(container, option);
    ECharts.Charts.RenderChart(opt);

}

