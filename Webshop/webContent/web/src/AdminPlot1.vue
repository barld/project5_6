<template>
    <div>
        <h1>Verkoopaantal statistieken</h1>
        <div class="row">
            <form>
                <div class="three columns statistic_fill_div">
                    <fieldset>
                        <legend>Tijdsframe</legend>
                        <template v-for="item in timespans">
                            <div>
                                <input type="radio" name="TimeSpan" class="statistics_radio" :value="item" ><span>{{ item }}</span>
                            </div>
                        </template>
                    </fieldset>
                </div>
                <div class="six columns statistic_fill_div">
                    <fieldset>
                        <legend>Begin- en einddatum</legend>
                        Start datum: <input type="date" name="StartDate" :value="today"><br>
                        Eind datum: <input type="date" name="EndDate" :value="today"><br>
                    </fieldset>
                </div>
            </form>
            <div class="three columns statistic_fill_div">
                <button @click="LoadOrderAmountStatistics" name="isSubmitted" class="statistic_send_button">Send</button>
                <button @click="$emit('closed')">Close</button>
            </div>
        </div>
        <div class="statistics_canvas" id="s_canvas_div">
            <canvas id="s_canvas"></canvas>
        </div>
    </div>
</template>
<script>
    import BarChartDrawer from './BarChartDrawer'

    export default{
        data(){
            return {timespans: null, salesData: null, today: ""}
        },
        methods:{
            LoadTimespans: function () {
                window.context.Statistics.LoadTimespans(this.ShowTimespans);
            },
            ShowTimespans: function(data){
                console.log(data);
                this.timespans = data;
            },
            LoadOrderAmountStatistics: function(){
                var elements = document.getElementsByName("TimeSpan");
                var time_scale = "";
                for(var i = 0; i < elements.length; i++){
                    if(elements[i].checked){
                        time_scale = elements[i].value;
                    }
                }
                var begin_date = new Date(document.getElementsByName("StartDate")[0].value);
                var end_date = new Date(document.getElementsByName("EndDate")[0].value);

                window.context.Statistics.LoadOrderAmountStatistics(begin_date, end_date, time_scale, this.CreateOrderAmountChart);
            },
            CreateOrderAmountChart(data){
                CreateChart(data);
            }
        },
        created: function () {
            this.LoadTimespans();
            var d = new Date();
            this.today = d.getFullYear() + "-" + ('0'+(d.getMonth() +1)).slice(-2)+"-" + ('0' + d.getDate()).slice(-2);
            console.log(this.today);
        },
        mounted: function(){
            var c = document.getElementById("s_canvas");
            c.width = 900;
            c.height = 450;
        }
    }
    function CreateChart(data){
        var keys = Object.keys(data).sort();

        if(typeof data === "undefined" || data === null){
            alert("Geen data geladen.");
            return;
        }
        var chartDrawer = new BarChartDrawer(document.getElementById("s_canvas"));
        chartDrawer.SetBackgroundColor("#EDEDED");

        //console.log(data);
        //console.log(keys);

        chartDrawer.DrawGraph(data, keys)
    }
</script>