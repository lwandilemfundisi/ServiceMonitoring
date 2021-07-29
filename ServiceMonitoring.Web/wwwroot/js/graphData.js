function getGraphSeries() {

    var postData = JSON.stringify({
        "Id": "servicemonitoraggregate-3637f267-c79d-4659-9fb6-d648378b7549"
    });

    PostJson("https://localhost:5001/ServiceMonitor/queryservice", postData, function (data) {
            chart = Highcharts.stockChart('container', {
                chart: {
                    events: {
                        load: function () {
                            //var series = this.series[0];
                            //setInterval(function () {
                            //    PostJson("https://localhost:5001/ServiceMonitor/queryservice", postData, function (data) {
                            //        var graph = series.graph,
                            //            area = series.area,
                            //            currentShift = (graph && graph.shift) || 0;
                            //        Highcharts.each([graph, area, series.graphNeg, series.areaNeg], function (shape) {
                            //            if (shape) {
                            //                shape.shift = currentShift + 1;
                            //            }
                            //        });

                            //        var newSeries = parseData(data);
                            //        for (idxi = 0; idxi < newSeries.length; idxi++) {
                            //            series.data[idxi].remove(false, false);
                            //            series.addPoint(newSeries[0].data[idxi], true, true);
                            //        }
                            //    });
                            //}, 1000);
                        }
                    }
                },
                plotOptions: {
                    series: {
                        turboThreshold: 1000,
                        cursor: 'pointer',
                        point: {
                            events: {
                                click: function () {
                                    alert('Category: ' + this.category + ', value: ' + this.y);
                                }
                            }
                        }
                    }
                },
                rangeSelector: {
                    allButtonsEnabled: true,
                    buttons: [{
                        type: 'millisecond',
                        count: 5000,
                        text: '5s'
                    }, {
                        type: 'millisecond',
                        count: 10000,
                        text: '10s'
                    }]
                },
                title: {
                    text: 'Live Api Performance Monitor'
                },
                exporting: {
                    enabled: false
                },
                series: parseData(data)
            });
    });
}

function PostJson(url, data, callback) {

    return $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: 'POST',
        url: url,
        data: data,
        success: callback,
        error: function (xhr, error) {
            console.debug(xhr); console.debug(error);
        },
        dataType: 'json',
        async: false
    });
}

function parseData(dataFromPost) {
    var data = []

    for (i = 0; i < dataFromPost.length; i++) {
        data.push({
            name: dataFromPost[i].name,
            data: createData(dataFromPost[i].data)
        });
    };

    return data;
}

function createData(d) {
    var data = [];
    for (ii = 0; ii < d.length; ii += 1) {
        data.push([new Date(d[ii].key).getTime(), d[ii].value]);
    };
    return data;
}

function setUpSignalR() {
    var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:5001/sendmethodlog").build();

    connection.on("SendMethodLog", function (data) {
        var obj = JSON.parse(data);
        var series = chart.series[0];
        //var graph = series.graph,
        //    area = series.area,
        //    currentShift = (graph && graph.shift) || 0;
        //Highcharts.each([graph, area, series.graphNeg, series.areaNeg], function (shape) {
        //    if (shape) {
        //        shape.shift = currentShift + 1;
        //    }
        //});
        //series.data[0].remove(false, false);
        series.addPoint([new Date(obj.ExecutionTime).getTime(), obj.TimeElapsed], true, true);
    });

    async function start() {
        try {
            await connection.start();
            console.log("SignalR Connected.");
        } catch (err) {
            console.log(err);
            setTimeout(start, 5000);
        }
    };

    connection.onclose(async () => {
        await start();
    });

    start();
}