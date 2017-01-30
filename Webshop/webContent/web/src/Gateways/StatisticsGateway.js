/**
 * Created by Gregor on 23-1-2017.
 */

export default
class StatisticsGateway {
    constructor() {
    }

    LoadTimespans (callback) {
        if(typeof callback !== "function"){
            console.log("Function:LoadTimespans Message:Warning not all parameters have been correctly filled.");
            console.log("Values passed are:");
            console.log("callback = " + callback);
            console.log("exiting function");
            return;
        }

        let xhr = new XMLHttpRequest();
        xhr.open("GET", "/api/Statistics/TimeScales/");
        xhr.setRequestHeader('Content-type', "Application/JSON", true);
        xhr.onload = function () {
            //console.log(JSON.parse(xhr.response));
            callback(JSON.parse(xhr.response));
        };
        xhr.send();
    }

    LoadOrderAmountStatistics(begin_date, end_date, time_scale, callback) {
        if(!begin_date || !end_date || !time_scale || typeof callback !== "function"){
            console.log("Function:LoadOrderAmountStatistics Message:Warning not all parameters have been correctly filled.");
            console.log("Values passed are:");
            console.log("begin_date = " + begin_date);
            console.log("end_date = " + end_date);
            console.log("time_scale = " + time_scale);
            console.log("callback = " + callback);
            console.log("exiting function");
            return;
        }

        let xhr = new XMLHttpRequest();
        xhr.open("POST", "/api/Statistics/SalesAmountStatistics/");
        xhr.setRequestHeader('Content-type', "Application/JSON", true);
        xhr.onload = function () {
            //console.log(JSON.parse(xhr.response));
            callback(JSON.parse(xhr.response));
        };
        xhr.send(JSON.stringify({TimeScale: time_scale, BeginDate: begin_date, EndDate: end_date}));
    }

    LoadGenresStatistics(begin_date, end_date, time_scale, callback){
        if(!begin_date || !end_date || !time_scale || typeof callback !== "function"){
            console.log("Function:LoadGenresStatistics Message:Warning not all parameters have been correctly filled.");
            console.log("Values passed are:");
            console.log("begin_date = " + begin_date);
            console.log("end_date = " + end_date);
            console.log("time_scale = " + time_scale);
            console.log("callback = " + callback);
            console.log("exiting function");
            return;
        }

        let xhr = new XMLHttpRequest();
        xhr.open("POST", "/api/Statistics/GenreAmountStatistics/");
        xhr.setRequestHeader('Content-type', "Application/JSON", true);
        xhr.onload = function () {
            //console.log(JSON.parse(xhr.response));
            callback(JSON.parse(xhr.response));
        };
        //console.log("TimeScale = " + time_scale + "BeginDate" + begin_date + "EndDate" + end_date);
        xhr.send(JSON.stringify({TimeScale: time_scale, BeginDate: begin_date, EndDate: end_date}));
    }

    LoadGenres(callback){
        if(typeof callback !== "function"){
            console.log("Function:LoadGenres Message:Warning not all parameters have been correctly filled.");
            console.log("Values passed are:");
            console.log("callback = " + callback);
            console.log("exiting function");
            return;
        }
        
        let xhr = new XMLHttpRequest();
        xhr.open("GET", "/api/Genre/All/");
        xhr.onload = function(){
            console.log(JSON.parse(xhr.response));
            callback(JSON.parse(xhr.response));
        }
        xhr.send();
    }

    LoadWishListStatistics(amount, genres, callback){
        if(typeof callback !== "function" || !amount){
            console.log("Function:LoadWishListStatistics Message:Warning not all parameters have been correctly filled.");
            console.log("Values passed are:");
            console.log("callback = " + callback);
            console.log("amount = " + amount);
            console.log("exiting function");
            return;
        }
        if(!Array.isArray(genres) || genres.length < 1){
            genres = ["all"];
        }

        let xhr = new XMLHttpRequest();
        xhr.open("POST", "/api/Statistics/WishListStatistics/");
        xhr.onload = function(){
            //console.log(JSON.parse(xhr.response));
            callback(JSON.parse(xhr.response));
        }
        xhr.send(JSON.stringify({Genre: genres, Amount: amount}));
    }
}