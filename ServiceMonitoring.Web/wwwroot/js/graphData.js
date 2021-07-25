

function getGraphSeries() {

    var postData = JSON.stringify({
        "Id": "servicemonitoraggregate-040fa886-249c-401e-9faa-d86bdc089ffa"
    });

    PostJson("https://localhost:5001/ServiceMonitor/queryservice", postData, function (data) {

        Highcharts.chart('container', {

            title: {
                text: 'Solar Employment Growth by Sector, 2010-2016'
            },

            subtitle: {
                text: 'Source: thesolarfoundation.com'
            },

            yAxis: {
                title: {
                    text: 'Number of Employees'
                }
            },

            xAxis: {
                accessibility: {
                    rangeDescription: 'Range: 2010 to 2017'
                }
            },

            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'middle'
            },

            plotOptions: {
                series: {
                    label: {
                        connectorAllowed: false
                    },
                    pointStart: 2010
                }
            },

            series: data,

            responsive: {
                rules: [{
                    condition: {
                        maxWidth: 500
                    },
                    chartOptions: {
                        legend: {
                            layout: 'horizontal',
                            align: 'center',
                            verticalAlign: 'bottom'
                        }
                    }
                }]
            }
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

    //return jQuery.ajax({
    //    headers: {
    //        'Accept': 'application/json',
    //        'Content-Type': 'application/json'
    //    },
    //    'type': 'POST',
    //    'url': url,
    //    'data': JSON.stringify(data),
    //    'dataType': 'json',
    //    'success': callback
    //})
}