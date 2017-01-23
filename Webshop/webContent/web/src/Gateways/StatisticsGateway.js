/**
 * Created by Gregor on 23-1-2017.
 */

export default
class StatisticsGateway {
    constructor() {
    }

    LoadTimespans (callback) {
        if(typeof callback !== "function"){
            console.log("F:LoadTimespans M:Warning not all parameters have been correctly filled.");
            console.log("Values passed are:");
            console.log("callback = " + callback);
            console.log("exiting function");
            return null;
        }

        var xhr = new XMLHttpRequest();
        xhr.open("GET", "/api/Statistics/TimeScales/");
        xhr.setRequestHeader('Content-type', "Application/JSON", true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.response);
            callback(data);
            //console.log(JSON.parse(xhr.response));
        }
        xhr.send("");
    }

    LoadOrderAmountStatistics(begin_date, end_date, time_scale, callback) {
        if(!begin_date || !end_date || !time_scale || typeof callback !== "function"){
            console.log("F:LoadOrderAmountStatistics M:Warning not all parameters have been correctly filled.");
            console.log("Values passed are:");
            console.log("begin_date = " + begin_date);
            console.log("end_date = " + end_date);
            console.log("time_scale = " + time_scale);
            console.log("callback = " + callback);
            console.log("exiting function");
            return null;
        }

        var xhr = new XMLHttpRequest();
        xhr.open("POST", "/api/Statistics/SalesAmountStatistics/");
        xhr.setRequestHeader('Content-type', "Application/JSON", true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.response);
            console.log(data);
            callback(data);
        }
        xhr.send(JSON.stringify({TimeScale: time_scale, BeginDate: begin_date, EndDate: end_date}));
    }
}