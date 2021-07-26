function getGraphSeries() {

    var postData = JSON.stringify({
        "Id": "servicemonitoraggregate-040fa886-249c-401e-9faa-d86bdc089ffa"
    });

    PostJson("https://localhost:5001/ServiceMonitor/queryservice", postData, function (data) {
            Highcharts.stockChart('container', {
                chart: {
                    events: {
                        load: function () {

                            //// set up the updating of the chart each second
                            //var series = this.series[0];
                            //setInterval(function () {
                            //    var x = (new Date()).getTime(), // current time
                            //        y = Math.round(Math.random() * 100);
                            //    series.addPoint([x, y], true, true);
                            //}, 1000);
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