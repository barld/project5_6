/**
 * Created by Gregor on 23-1-2017.
 */

export default
class StatisticsGateway {
    constructor() {
    }

    LoadTimespans () {
        var xhr = new XMLHttpRequest();
        xhr.open("GET", "/api/AdminStatistics/TimeSpans/");
        xhr.setRequestHeader('Content-type', "Application/JSON", true);
        xhr.onload = function () {
            //console.log(JSON.parse(xhr.response));
        }
        xhr.send("");
    }

    LoadOrderAmountStatistics(timespan) {
        var startTime = new Date(document.getElementsByName("StartDate")[0].value);
        var endTime = new Date(document.getElementsByName("EndDate")[0].value);

        var xhr = new XMLHttpRequest();
        xhr.open("POST", "/api/AdminStatistics/SalesAmountStatistics/");
        xhr.setRequestHeader('Content-type', "Application/JSON", true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.response);
        }
        xhr.send(JSON.stringify({TimeSpan: timeSpan, BeginDate: startTime, EndDate: endTime}));
    }
}