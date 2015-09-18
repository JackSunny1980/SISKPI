var ECharts = {

    ChartConfig: function (container, option) {
        this.Colors = ['#910000', '#1aadce', '#492970', '#f28f43', '#77a1e5', '#c42525', '#a6c96a', '#6f2fd8', '#531750', '#2f7ed8', '#0d233a', '#8bbc21', '#d7c332', '#9a7400', '#5ace1a', '#910044', '#ffb81c', '#e5e65b', '#d12270', '#6ad0f0', '#3337e2', '#770808', '#df6237', '#07799e', '#f5b688', '#004b91', '#c340e3', '#4b9cad', '#cc4800', '#ff91c2', '#00913d', '#145207', '#2f5bfc', '#e34063', '#b794f1', '#4900c2', '#f09797', '#66892a', '#5d68f8', '#c577e5']; //默认配色

        var chart_path = "../ECharts/echarts/chart"; //配置图表请求路径 
        var map_path = "../ECharts/echarts/echarts-map"; //配置地图的请求路径 

        require.config({//引入常用的图表类型的配置
            paths: {
                'echarts': chart_path,
                'echarts/chart/bar': chart_path,
                'echarts/chart/pie': chart_path,
                'echarts/chart/line': chart_path,
                'echarts/chart/gauge': chart_path,
                'echarts/chart/k': chart_path,
                'echarts/chart/scatter': chart_path,
                'echarts/chart/radar': chart_path,
                'echarts/chart/chord': chart_path,
                'echarts/chart/force': chart_path,
                'echarts/chart/funnel': chart_path,
                'echarts/chart/map': map_path
            }
        });
        this.option = { chart: {}, option: option, container: container };
        return this.option;

    },

    ChartDataFormate: {
        FormateNOGroupData: function (data) {
            //data的格式如上的Result1，这种格式的数据，多用于饼图、单一的柱形图的数据源
            var categories = [];
            var datas = [];
            for (var i = 0; i < data.length; i++) {
                categories.push(data[i].name || "");
                datas.push({ name: data[i].name, value: data[i].value || 0 });
            }
            return { category: categories, data: datas };
        },


        FormateGroupData: function (data, type, is_stack) {
            //data的格式如上的Result2，type为要渲染的图表类型：可以为line，bar，is_stack表示为是否是堆积图，这种格式的数据多用于展示多条折线图、分组的柱图
            var chart_type = 'line';
            if (type)
                chart_type = type || 'line';

            var xAxis = [];
            var group = [];
            var series = [];

            for (var i = 0; i < data.length; i++) {
                for (var j = 0; j < xAxis.length && xAxis[j] != data[i].name; j++);
                if (j == xAxis.length)
                    xAxis.push(data[i].name);

                for (var k = 0; k < group.length && group[k] != data[i].group; k++);
                if (k == group.length)
                    group.push(data[i].group);
            }


            for (var i = 0; i < group.length; i++) {
                var temp = [];
                for (var j = 0; j < data.length; j++) {
                    if (group[i] == data[j].group) {
                        if (type == "map") {
                            temp.push({ name: data[j].name, value: data[i].value });
                        } else {
                            temp.push(data[j].value);
                        }
                    }

                }



                switch (type) {
                    case 'bar':
                        var series_temp = { name: group[i], data: temp, type: chart_type };
                        if (is_stack)
                            series_temp = $.extend({}, { stack: 'stack' }, series_temp);
                        break;

                    case 'map':
                        var series_temp = {
                            name: group[i], type: chart_type, mapType: 'china', selectedMode: 'single',
                            itemStyle: {
                                normal: { label: { show: true } },
                                emphasis: { label: { show: true } }
                            },
                            data: temp
                        };
                        break;

                    case 'line':
                        var series_temp = { name: group[i], data: temp, type: chart_type };
                        if (is_stack)
                            series_temp = $.extend({}, { stack: 'stack' }, series_temp);
                        break;

                    default:
                        var series_temp = { name: group[i], data: temp, type: chart_type };
                }
                series.push(series_temp);
            }
            return { category: group, xAxis: xAxis, series: series };
        },
        //单柱状图和折线图，如：完成量和完成百分比
        FormateBarLineData: function (data, name1, name2) {
            var xAxis = [];
            var series = [];
            var s1 = [];
            var s2 = [];
            for (var i = 1; i < data.length; i++) {
                if (!xAxis.contains(data[i].name))
                    xAxis.push(data[i].name);
                s1.push(data[i].value);
                var growth = 0;
                if (data[i].value != data[i - 1].value)
                    if (data[i - 1].value != 0)
                        growth = Math.round((data[i].value / data[i - 1].value - 1) * 100);
                    else
                        growth = 100;
                s2.push(growth);
            }
            series.push({ name: name1, data: s1, type: 'bar' });
            series.push({ name: name2, data: s2, type: 'line', yAxisIndex: 1 });
            return { category: [name1, name2], xAxis: xAxis, series: series };
        },
        //分组柱状图和单曲线图
        FormateBarsLineData: function (data, xDes, yDes) {
            var categories = [];
            var linename = '';
            var group = [];
            var xAxis = [];
            var series = [];
            var lines = [];
            for (var i = 1; i < data.length; i++) {
                if (data[i].lineflag != 'line') {
                    for (var j = 0; j < xAxis.length && xAxis[j] != data[i].name; j++);
                    if (j == xAxis.length)
                        xAxis.push(data[i].name);
                    for (var k = 0; k < group.length && group[k] != data[i].group; k++);
                    if (k == group.length) {
                        group.push(data[i].group);
                        categories.push(data[i].group);
                    }
                }
                else {
                    linename = data[i].group || "";
                    lines.push({ name: data[i].name, value: data[i].value || 0 });
                }
            }
            for (var i = 0; i < group.length; i++) {
                var temp = [];
                for (var j = 0; j < data.length; j++) {
                    if (data[j].lineflag != 'line') {
                        if (group[i] == data[j].group) {
                            temp.push(data[j].value);
                        }
                    }
                }
                series.push({ name: group[i], data: temp, type: 'bar' });
            }
            categories.push(linename);
            series.push({ name: linename, data: lines, type: 'line', yAxisIndex: 1 });
            return { category: categories, xAxis: xAxis, series: series };
        },
        FormateMapData: function (data, name) {
            var categroy = [];
            var series = [];
            var names = [];
            var max = function (data) { //求出最大值,赋值给值域漫游的最大值 
                var names = new Array();
                var groups = new Array();
                var values = new Array();
                for (var i = 0; i < data.length; i++) {
                    if (!names.contains(data[i].name))
                        names.push(data[i].name);
                    if (!groups.contains(data[i].group))
                        groups.push(data[i].group);
                }
                for (var i = 0; i < names.length; i++) {
                    var value = 0;
                    for (var j = 0; j < data.length; j++) {
                        if (names[i] == data[j].name)
                            value += data[j].value;
                    }
                    values.push(value);
                }
                var max = Math.max.apply(Math, values);
                return Math.ceil(max / 100) * 100;
            };
            for (var i = 0; i < data.length; i++) {
                if (!categroy.contains(data[i].group))
                    categroy.push(data[i].group);
                if (!names.contains(data[i].name))
                    names.push(data[i].name);
            }
            for (var i = 0; i < categroy.length; i++) {
                var temp_data = [];
                for (var j = 0; j < names.length; j++) {
                    for (var k = 0; k < data.length; k++) {
                        if (data[k].group == categroy[i] && names[j] == data[k].name)
                            temp_data.push({ name: names[j], value: data[k].value });
                    }
                }
                var temp_series = {
                    name: categroy[i],
                    type: 'map',
                    mapType: 'china',
                    itemStyle: {
                        normal: {
                            label: { show: true },
                            color: '#ffd700',
                            emphasis: {
                                label: { show: true }
                            }
                        }
                    },
                    data: temp_data
                };
                series.push(temp_series);
            }
            return { category: categroy, series: series, max: max };
        }
    },


    ChartOptionTemplates: {
        CommonOption: {
            //通用的图表基本配置 
            tooltip: {
                trigger: 'axis'//tooltip触发方式:axis以X轴线触发,item以每一个数据项触发 
            },
            toolbox: {
                show: true, //是否显示工具栏 
                feature: {
                    mark: true,
                    dataView: { readOnly: false }, //数据预览 
                    restore: true, //复原 
                    saveAsImage: true //是否保存图片 
                }
            }
        },

        CommonLineOption: {//通用的折线图表的基本配置 
            tooltip: {
                trigger: 'axis'
            },
            calculable: true,
            toolbox: {
                show: true,
                feature: {
                    dataView: { readOnly: false }, //数据预览
                    restore: true, //复原
                    saveAsImage: true, //是否保存图片
                    magicType: ['line', 'bar']//支持柱形图和折线图的切换 
                }
            }
        },
        Piefunnel: function (data, name) {
            //data:数据格式：{name：xxx,value:xxx}...
            var piefunnel_datas = ECharts.ChartDataFormate.FormateNOGroupData(data);
            var option = {
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b} : {c} ({d}%)"
                },
                legend: {
                    //orient: 'vertical',
                    padding: 5,
                    itemGap: 5,
                    y: 'top',
                    data: piefunnel_datas.category
                },
                toolbox: {
                    show: true,
                    orient: 'vertical',
                    x: 'right',
                    y: 'bottom',
                    feature: {
                        mark: { show: false },
                        dataView: { show: true, readOnly: false },
                        magicType: {
                            show: true,
                            type: ['pie', 'funnel'],
                            option: {
                                funnel: {
                                    x: '25%',
                                    y: '25%',
                                    width: '50%',
                                    funnelAlign: 'center',
                                    max: 1548
                                }
                            }
                        },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                calculable: true,
                series: [
                        {
                            name: '发电量',
                            type: 'pie',
                            radius: ['40%', '60%'],
                            center: ['50%', '60%'],
                            minAngle: 5,
                            itemStyle: {
                                normal: {
                                    label: {
                                        show: false
                                    },
                                    labelLine: {
                                        show: false
                                    }
                                },
                                emphasis: {
                                    label: {
                                        show: true,
                                        position: 'center',
                                        textStyle: {
                                            fontSize: '15',
                                            fontWeight: 'bold'
                                        }
                                    }
                                }
                            },
                            data: piefunnel_datas.data
                        }
                ]
            };
            return $.extend({}, ECharts.ChartOptionTemplates.CommonOption, option);
        },

        //自定义漏斗图， 用于显示预计和实际的差异
        Funnel: function (ydata, sdata, name) {
            //data:数据格式：{name：xxx,value:xxx}...
            var funnel_ydatas = ECharts.ChartDataFormate.FormateNOGroupData(ydata);
            var funnel_sdatas = ECharts.ChartDataFormate.FormateNOGroupData(sdata);
            var option = {
                color: [
                    'rgba(255, 69, 0, 0.5)',
                    'rgba(255, 150, 0, 0.5)',
                    'rgba(255, 200, 0, 0.5)',
                    'rgba(155, 200, 50, 0.5)',
                    'rgba(55, 200, 100, 0.5)'
                ],
                title: {
                    text: '',
                    subtext: ''
                },
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b} : {c}%"
                },
                toolbox: {
                    show: true,
                    feature: {
                        mark: { show: true },
                        dataView: { show: true, readOnly: true },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                legend: {
                    data: funnel_datas.category
                },
                series: [

                    {
                        name: '预期',
                        type: 'funnel',
                        x: '45%',
                        width: '45%',
                        itemStyle: {
                            normal: {
                                label: {
                                    formatter: '{b}预期'
                                },
                                labelLine: {
                                    show: false
                                }
                            },
                            emphasis: {
                                label: {
                                    position: 'inside',
                                    formatter: '{b}预期 : {c}%'
                                }
                            }
                        },
                        data: funnel_ydatas.data
                    },
                    {
                        name: '实际',
                        type: 'funnel',
                        x: '45%',
                        width: '45%',
                        maxSize: '80%',
                        itemStyle: {
                            normal: {
                                borderColor: '#fff',
                                borderWidth: 2,
                                label: {
                                    position: 'inside',
                                    formatter: '{c}%',
                                    textStyle: {
                                        color: '#fff'
                                    }
                                }
                            },
                            emphasis: {
                                label: {
                                    position: 'inside',
                                    formatter: '{b}实际 : {c}%'
                                }
                            }
                        },
                        data: funnel_sdatas.data
                    }
                ]
            };
            return $.extend({}, ECharts.ChartOptionTemplates.CommonOption, option);
        },

        Pie: function (data, name) {
            //data:数据格式：{name：xxx,value:xxx}...
            var pie_datas = ECharts.ChartDataFormate.FormateNOGroupData(data);

            var option = {
                tooltip: {
                    trigger: 'item',
                    formatter: '{b} : {c} ({d}/%)',
                    show: true
                },

                legend: {
                    show: true,
                    //orient: 'vertical',
                    //x: 'left',
                    padding: 5,
                    itemGap: 5,
                    y: 'top',
                    data: pie_datas.category
                },

                calculable: true,

                toolbox: {
                    show: true,
                    orient: 'vertical',
                    x: 'right',
                    y: 'bottom',
                    feature: {
                        mark: { show: false },
                        dataView: { show: true, readOnly: false },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                series: [
                    {
                        name: name || "",
                        type: 'pie',
                        radius: '50%',
                        minAngle: 5,
                        center: ['50%', '60%'],
                        data: pie_datas.data
                    }
                ]
            };
            return $.extend({}, ECharts.ChartOptionTemplates.CommonOption, option);
        },

        Gauge: function (data, name) {
            //data:数据格式：{name：xxx,value:xxx}...
            var gauge_datas = ECharts.ChartDataFormate.FormateNOGroupData(data);

            var option = {
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b} : {c}%"
                },
                toolbox: {
                    show: true,
                    y: 'bottom',
                    x: 'center',
                    feature: {
                        mark: { show: false },
                        dataView: { show: true, readOnly: false },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                series: [
                {
                    name: '完成率',
                    type: 'gauge',
                    min: 0,
                    max: 100,
                    center: ['50%', '50%'],
                    splitNumber: 5,       // 分割段数，默认为5
                    axisLine: {            // 坐标轴线
                        lineStyle: {       // 属性lineStyle控制线条样式
                            color: [[0.2, '#228b22'], [0.8, '#48b'], [1, '#ff4500']],
                            width: 5
                        }
                    },

                    axisTick: {            // 坐标轴小标记
                        splitNumber: 15,   // 每份split细分多少段
                        length: 10,        // 属性length控制线长
                        lineStyle: {       // 属性lineStyle控制线条样式
                            color: 'auto',
                            width: 1,
                            type: 'solid'
                        }
                    },
                    axisLabel: {           // 坐标轴文本标签，详见axis.axisLabel
                        textStyle: {       // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                            color: 'auto',
                            fontSize: '10px'
                        }
                    },
                    splitLine: {           // 分隔线
                        show: true,        // 默认显示，属性show控制显示与否
                        length: 13,         // 属性length控制线长
                        lineStyle: {       // 属性lineStyle（详见lineStyle）控制线条样式
                            color: 'auto'
                        }
                    },
                    pointer: {
                        width: 5,
                        color: 'auto'
                    },
                    title: {
                        show: true,
                        offsetCenter: [0, '-135%'],       // x, y，单位px
                        textStyle: {       // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                            fontFamily: 'Arial, Verdana, sans-serif',
                            color: '#333',
                            fontSize: 15
                        }
                    },
                    detail: {
                        formatter: '{value}%',
                        textStyle: {       // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                            color: 'auto',
                            fontWeight: 'bolder',
                            fontSize: 13
                        }
                    },
                    data: gauge_datas.data
                }
                ]
            };
            return $.extend({}, ECharts.ChartOptionTemplates.CommonOption, option);
        },

        Line: function (data, name) {//data:数据格式：{name：xxx,value:xxx}...
            var line_datas = ECharts.ChartDataFormate.FormateNOGroupData(data, 'line');
            var option = {
                tootip: {
                    trigger: 'axis',
                    formatter: '{b}: {c}'
                },
                toolbox: {
                    show: true,
                    orient: 'vertical',
                    x: 'right',
                    y: 'bottom',
                    feature: {
                        mark: { show: true },
                        dataView: { show: true, readOnly: false },
                        magicType: { show: true, type: ['line', 'bar'] },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                calculable: true,
                xAxis: [{
                    type: 'category', //X轴均为category，Y轴均为value
                    axisLabel: {
                        show: true,
                        interval: 'auto',
                        rotate: 30,
                        margion: 14
                    },
                    data: line_datas.category,
                    boundaryGap: false//数值轴两端的空白策略
                }],
                yAxis: [{
                    name: name || '',
                    type: 'value',
                    splitArea: { show: true }
                }],
                series: [{
                    type: 'line',
                    name: name || '',
                    axisLabel: { interval: 0 },
                    data: line_datas.data
                }]
            };
            return $.extend({}, ECharts.ChartOptionTemplates.CommonLineOption, option);
        },

        Lines: function (data, name, is_stack) {
            //data:数据格式：{name：xxx,group:xxx,value:xxx}... 
            var stackline_datas = ECharts.ChartDataFormate.FormateGroupData(data, 'line', is_stack);
            var option = {
                legend: {
                    data: stackline_datas.category
                },
                toolbox: {
                    show: true,
                    feature: {
                        mark: { show: true },
                        dataView: { show: true, readOnly: false },
                        magicType: { show: true, type: ['line', 'bar'] },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                calculable: true,
                xAxis: [{
                    type: 'category', //X轴均为category，Y轴均为value 
                    axisLabel: {
                        show: true,
                        interval: 'auto',
                        rotate: 30,
                        margion: 14
                    },
                    data: stackline_datas.xAxis,
                    boundaryGap: false//数值轴两端的空白策略 
                }],

                yAxis: [{
                    name: name || '',
                    type: 'value',
                    splitArea: { show: true }
                }],
                series: stackline_datas.series
            };
            return $.extend({}, ECharts.ChartOptionTemplates.CommonLineOption, option);
        },
        Bar: function (data, name) {//data:数据格式：{name：xxx,value:xxx}...
            var bar_datas = ECharts.ChartDataFormate.FormateNOGroupData(data, 'bar');
            var option = {
                tooltip: {
                    trigger: item,
                    formatter: "{b}:{c}"
                },
                legend: {
                    show: true,
                    backgroundColor: 'rgba(0,0,0,0)',
                    data: bar_datas.category
                },
                toolbox: {
                    show: true,
                    orient: 'vertical',
                    x: 'right',
                    y: 'bottom',
                    feature: {
                        mark: { show: true },
                        dataView: { show: true, readOnly: false },
                        magicType: { show: true, type: ['line', 'bar',] },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                calculable: true,
                xAxis: [{
                    type: 'category',
                    data: bar_datas.category,
                    axisLabel: {
                        show: true,
                        interval: 'auto',
                        rotate: 20, //旋转角度
                        margin: 8//距离X轴的距离
                    }
                }],
                yAxis: [{
                    name: name || '',
                    type: 'value',
                    nameLocation: 'end',
                    boundaryGap: [0, 0.01]
                }],
                series: [{
                    name: name || '',
                    axisLabel: { interval: 0 },
                    type: 'bar',
                    data: bar_datas.data
                }]

            };
            return $.extend({}, ECharts.ChartOptionTemplates.CommonLineOption, option);
        },
        Bars: function (data, name, is_stack) {
            //data:数据格式：{name：xxx,group:xxx,value:xxx}...
            var bars_dates = ECharts.ChartDataFormate.FormateGroupData(data, 'bar', is_stack);
            var option = {
                tooltip: {
                    trigger: 'axis'              //可选为：'item' | 'axis' 
                    //                    axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                    //                        type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                    //                    }
                },
                legend: {
                    show: true,
                    backgroundColor: 'rgba(0,0,0,0)',
                    data: bars_dates.category
                },
                toolbox: {
                    show: true,
                    orient: 'vertical',
                    x: 'right',
                    y: 'bottom',
                    feature: {
                        mark: { show: true },
                        dataView: { show: true, readOnly: false },
                        magicType: { show: true, type: ['line', 'bar' ] }, //'stack', 'tiled'
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                calculable: true,
                xAxis: [{
                    type: 'category',
                    data: bars_dates.xAxis,
                    axisLabel: {
                        show: true,
                        interval: 'auto',
                        rotate: 20, //旋转角度
                        margin: 8//距离X轴的距离
                    }
                }],

                yAxis: [{
                    type: 'value',
                    name: name || '',
                    splitArea: { show: true }
                }],
                series: bars_dates.series
            };
            return $.extend({}, ECharts.ChartOptionTemplates.CommonLineOption, option);
        },
        HorBars: function (data, name, is_stack) {
            //data:数据格式：{name：xxx,group:xxx,value:xxx}...
            var bars_dates = ECharts.ChartDataFormate.FormateGroupData(data, 'bar', is_stack);
            var option = {
                tooltip: {
                    trigger: 'axis'              //可选为：'item' | 'axis' 
                    //                    axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                    //                        type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                    //                    }
                },
                legend: {
                    show: true,
                    backgroundColor: 'rgba(0,0,0,0)',
                    data: bars_dates.category
                },
                toolbox: {
                    show: true,
                    orient: 'vertical',
                    x: 'right',
                    y: 'bottom',
                    feature: {
                        mark: { show: true },
                        dataView: { show: true, readOnly: false },
                        magicType: { show: true, type: ['line', 'bar'] },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                calculable: true,
                xAxis: [{
                    type: 'value',
                    name: name || '',
                    splitArea: { show: true }
                }],

                yAxis: [{
                    type: 'category',
                    data: bars_dates.xAxis,
                    axisLabel: {
                        show: true,
                        interval: 'auto',
                        rotate: 20, //旋转角度
                        margin: 8//距离X轴的距离
                    }
             
                }],
                series: bars_dates.series
            };
            return $.extend({}, ECharts.ChartOptionTemplates.CommonLineOption, option);
        },
        BarsLine: function (data, xDes, yDes) {//data:数据格式：{name：xxx,group:xxx,value:xxx,lineflag:xxx}...
            var barsline_dates = ECharts.ChartDataFormate.FormateBarsLineData(data, xDes, yDes);
            var option = {
                legend: {
                    data: barsline_dates.category
                },
                toolbox: {
                    show: true,
                    orient: 'vertical',
                    x: 'right',
                    y: 'bottom',
                    feature: {
                        mark: { show: true },
                        dataView: { show: true, readOnly: false },
                        magicType: { show: true, type: ['line', 'bar'] }, // 'stack', 'tiled'
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                calculable: true,
                xAxis: [{
                    type: 'category',
                    data: barsline_dates.xAxis,
                    axisLabel: {
                        show: true,
                        interval: 'auto',
                        rotate: 30,
                        margion: 14
                    }
                }],
                yAxis: [{
                    type: 'value',
                    name: xDes || '',
                    splitArea: { show: false }
                },
					{
					    type: 'value',
					    name: yDes || '',
					    axisLabel: {
					        boundaryGap: [0, 0.01],
					        formatter: "{value}"
					    },
					    splitLine: { show: true }
					}],
                series: barsline_dates.series
            };
            return $.extend({}, ECharts.ChartOptionTemplates.CommonLineOption, option);
        },
        BarLine: function (data, xDes, yDes) {//data:数据格式：{name：xxx,value:xxx}...
            var barline_dates = ECharts.ChartDataFormate.FormateBarLineData(data, xDes, yDes);
            var option = {
                legend: {
                    data: barline_dates.category
                },
                xAxis: [{
                    type: 'category',
                    data: barline_dates.xAxis,
                    trigger: 'axis'
                }],
                yAxis: [{
                    type: 'value',
                    name: xDes || '',
                    splitArea: { show: false }
                },
					{
					    type: 'value',
					    name: yDes || '',
					    axisLabel: {
					        boundaryGap: [0, 0.01],
					        formatter: "{value}%"
					    },
					    splitLine: { show: true }
					}],
                series: barline_dates.series
            };
            return $.extend({}, ECharts.ChartOptionTemplates.CommonLineOption, option);
        }
    },

    Charts: {
        RenderChart: function (option) {
            require([
                'echarts',
                'echarts/chart/line',
                'echarts/chart/bar',
                'echarts/chart/pie',
                'echarts/chart/gauge',
                'echarts/chart/k',
                'echarts/chart/scatter',
                'echarts/chart/radar',
                'echarts/chart/chord',
                'echarts/chart/force',
                'echarts/chart/funnel',
                'echarts/chart/map'
            ],

              function (ec) {
                  echarts = ec;
                  if (option.chart && option.chart.dispose)
                      option.chart.dispose();

                  option.chart = echarts.init(option.container);
                  window.onresize = option.chart.resize;
                  option.chart.setOption(option.option, true);
              });
        },
        RenderMap: function (option) {
            require([
                'echarts',
                'echarts/chart/map'
            ], function (ec) {
                echarts = ec;
                if (option.chart && option.chart.dispose)
                    option.chart.dispose();
                option.chart = echarts.init(option.container);
                option.chart.setOption(option.option, true);

                var echartConfig = require('echarts/config');
                option.chart.on(echartConfig.EVENT.MAP_SELECTED, function (param) {//绑定地图选择事件
                    var regions = []; //用来存储用户选择的省市
                    alert('heel0');
                    for (var i = 0; i < option.option.series[0].data.length; i++) {
                        var temp = option.option.series[0].data[i];
                        if (param.selected[temp.name])
                            regions.push(temp.name);
                    }
                    alert(regions[0]); //示例，在获取用户选择的省份之后就可以根据获取的信息做相应的处理
                });
                option.chart.on(echartConfig.EVENT.LEGEND_SELECTED, function (param) {//绑定图例选择事件
                    var legends = [];
                    for (var l in param.selected) {
                        if (param.selected[l])
                            legends.push(l);
                    }
                    alert(legends[0]); //示例：获取用户所选的图例，然后进行相应的处理
                });
            });
        }
    }
};
