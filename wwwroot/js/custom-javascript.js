function scrollToTop() {
    window.scrollTo({ top: 0, behavior: "smooth" });
}

function changeClassName(classNameOld, classNameNew) {
    const el = document.querySelector(`.${classNameOld}`);
    if (el) {
        el.className = classNameNew;
    }
}

window.saveAsFile = function (fileName, bytesBase64) {
    var link = document.createElement('a');
    link.download = fileName;
    link.href = "data:application/pdf;base64," + bytesBase64;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

window.saveElementAsPdf = function (elementId, options) {
    console.log("options:", options);
    console.log("options.image:", options.image);
    var element = document.getElementById(elementId);
    var opt = {
        margin: options.margin,
        filename: options.fileName,
        image: { type: options.image.type, quality: options.image.quality },
        html2canvas: { scale: options.html2canvas.scale },
        jsPDF: { unit: options.jsPDF.unit, format: options.jsPDF.format, orientation: options.jsPDF.orientation }
    };
    html2pdf().set(opt).from(element).save();
}

//window.saveElementAsPdf = function (elementId, fileName) {
//    var element = document.getElementById(elementId);
//        var opt = {
//            margin: 100,
//            filename: fileName,
//            image: { type: 'jpeg', quality: 0.98 },
//            html2canvas: { scale: 2 },
//            jsPDF: { unit: 'mm', format: 'a4', orientation: 'portrait' }
//        };
//    var originalBorder = element.style.border;
//    element.style.border = "none";
//    html2pdf().from(element).save(fileName).then(() => {
//        element.style.border = originalBorder;
//    });
//}

window.setIndeterminateSelection = (element, indeterminate) => {
    if (element) {
        element.indeterminate = indeterminate;
    }
};

window.renderApexChart = function (seriesData, categories, width = "100%", height = "100%") {
    var options = {
        chart: { height: 380, width: width, type: "line" },
        series: [{ name: "Series 1", data: seriesData }],
        xaxis: { categories: categories }
    };
    var chart = new ApexCharts(document.querySelector("#chart"), options);
    chart.render();
}

window.renderApexChartMulti = function (options) {
    var options = {
        chart: {
            type: options.chart.type,
            height: options.height,
            width: options.width
        },
        series: options.series,
        tooltip: {
            y: {
                formatter: function (val) {
                    return val.toFixed(2);
                }
            }
        },
        yaxis: {
            labels: {
                formatter: function (val) {
                    return val.toFixed(2);
                }
            }
        },
        xaxis: {
            categories: options.xaxis.categories
        }
    };

    // Destroy existing chart if present
    if (window.chartInstance) {
        window.chartInstance.destroy();
    }
    window.chartInstance = new ApexCharts(document.querySelector("#chart"), options);
    window.chartInstance.render();
}

window.renderApexBarChart = function (options) {
    var options = {
        chart: {
            type: options.chart.type,
            height: options.height,
            width: options.width
        },
        series: options.series,
        dataLabels: {
            enabled: true,
            formatter: function (val, { series, seriesIndex, dataPointIndex, w }) {
                if (seriesIndex === 0) {
                    // Index var
                    return val.toFixed(0);
                } else {
                    return val.toFixed(2);
                }
                return val;
            },
            offsetY: -20,
            style: {
                fontSize: '12px',
                colors: ["#304758"]
            }
        },
        plotOptions: {
            bar: {
                borderRadius: 1,
                dataLabels: {
                    position: 'top',
                },
            }
        },
        tooltip: {
            enabled: true,
            y: {
                formatter: function (val, { series, seriesIndex, dataPointIndex, w }) {
                    if (seriesIndex === 1) {
                        // Index var
                        return val.toFixed(0);
                    } else {
                        return val.toFixed(2);
                    }
                    return val;
                }
            }
        },
        yaxis: {
            labels: {
                formatter: function (val) {
                    return val.toFixed(0);
                }
            }
        },
        xaxis: {
            categories: options.xaxis.categories
        }
    };
    // Destroy existing chart if present
    if (window.chartInstance) {
        window.chartInstance.destroy();
    }
    window.chartInstance = new ApexCharts(document.querySelector("#chart"), options);
    window.chartInstance.render();
}

window.updateApexChart = function (options) {
    var chartDiv = document.getElementById("chart");
    console.log("updateApexChart called. Instance:", window.myApexChartInstance, "Div:", chartDiv, "Options:", options);
    if (window.myApexChartInstance && chartDiv) {
        console.log("Chart instance found ");
        //console.log("Calling updateOptions with:", options);
        //window.myApexChartInstance.updateOptions(options, true, true, true);
        console.log("Calling updateOptions with:", options.xaxis.categories);
        //window.myApexChartInstance.updateOptions({
        //    xaxis: { categories: options.xaxis.categories }
        // ...other options...
        //}, true, true, true);
        console.log("Calling updateSeries with:", options.series);
        window.myApexChartInstance.updateSeries(options.series, true);
    } else if (chartDiv) {
        console.log("Create new chart ");
        window.myApexChartInstance = new ApexCharts(chartDiv, options);
        window.myApexChartInstance.render();
    } else {
        console.error("Chart div not found!");
    }
};

window.notifyDotNetAfterChartDiv = function (dotNetRef) {
    setTimeout(() => {
        dotNetRef.invokeMethodAsync('OnChartDivReady');
    }, 0); // Or use MutationObserver?
};

window.notifyChartDivReady = function (dotNetHelper) {
    setTimeout(function () {
        dotNetHelper.invokeMethodAsync('RenderChartFromJS');
    }, 0);
}

window.disposeApexChart = function () {
    if (window.myApexChartInstance) {
        window.myApexChartInstance.destroy();
        window.myApexChartInstance = null;
    }
}