<template>
    <div>
        <h1>Meest populaire gernes</h1>
        <div class="row">
            <form>
                <div class="three columns statistic_fill_div">
                    <fieldset>
                        <legend>Tijdsframe</legend>
                        <template v-for="item in time_scale_options">
                            <input type="radio" name="TimeSpan" class="statistics_radio" :value="item" v-model="time_scale"><span>{{ item }}</span><br>
                        </template>
                    </fieldset>
                </div>
                <div class="six columns statistic_fill_div">
                    <fieldset>
                        <legend>Begin- en einddatum</legend>
                        Start datum: <input title="yyyy-mm-dd" type="date" name="StartDate" :value="today" v-model="begin_date"><br>
                        Eind datum: <input title="yyyy-mm-dd" type="date" name="EndDate" :value="today" v-model="end_date"><br>
                    </fieldset>
                </div>
            </form>
            <div class="three columns statistic_fill_div">
                <button @click="LoadGenreStatistics" name="isSubmitted" class="statistic_send_button">Send</button>
                <button @click="$emit('closed')">Close</button>
            </div>
        </div>
        <div class="statistics_canvas" id="s_canvas_div">
            <canvas id="s_canvas" ></canvas>
        </div>
    </div>
</template>
<script>
    import Chart from 'chart.js'

    export default{
        data(){
            return{time_scale:"", begin_date:"", end_date:"", time_scale_options:null}
        },
        methods:{
            LoadGenreStatistics: function(){
                if(!this.time_scale || !this.begin_date || !this.end_date){
                    alert("Niet alles is ingevuld.");
                    return;
                }

                window.context.Statistics.LoadGenresStatistics(this.begin_date, this.end_date, this.time_scale, this.ShowGenreStatistics);
            },

            ShowGenreStatistics: function(data){
                console.log(data);
                let pr = prepareData(data);

                Chart.defaults.global.elements.line.fill = false;
                Chart.defaults.global.elements.line.lineTension = 0.2;
                Chart.defaults.global.elements.line.borderCapStyle = 'butt';
                Chart.defaults.global.elements.point.borderWidth = 1;
                Chart.defaults.global.elements.point.hoverRadius = 5;
                Chart.defaults.global.elements.point.hoverBorderWidth = 2;
                Chart.defaults.global.elements.point.radius = 1;
                Chart.defaults.global.elements.point.hitRadius = 10;

                let ctx = document.getElementById("s_canvas");
                let datasetModel = {
                    label: "My Second dataset",
                    backgroundColor: "rgba(75,192,192,0.4)",
                    borderColor: "rgba(75,192,192,1)",
                    pointBorderColor: "rgba(75,192,192,1)",
                    pointHoverBackgroundColor: "rgba(75,192,192,1)",
                    data: [92, 85, 12, 21, 55, 14, 74]
                };
                let datasets = [];
                let genreKeys = Object.keys(data[0].GenreAmounts);
                for(let i = 0; i < pr.values.length; i++){
                    let inst = Object.create(datasetModel);
                    inst.label = genreKeys[i];
                    inst.data = pr.values[i];
                    datasets[i] = inst;
                }

                var options = {
                    scales: {
                        yAxes: [{
                            ticks: {
                                callback: function (value) {
                                    return value + "%"
                                }
                            },
                            scaleLabel: {
                                display: true,
                                labelString: "Percentage"
                            }
                        }]
                    }
                }

                let chartData = {
                    labels: pr.labels,
                    datasets: datasets
                };

                let myChart = new Chart(ctx, {
                    type: 'line',
                    data: chartData,
                    options: options
                });
            },

            PrepareForm: function(data){
                console.log(data);
                let date = new Date();
                this.time_scale_options = data;
                this.begin_date =  this.Today(date);
                this.end_date = this.Today(date)
            },

            Today: function(d){
                let today = d.getFullYear() + "-" + ('0'+(d.getMonth() +1)).slice(-2)+"-" + ('0' + d.getDate()).slice(-2);
                console.log(today);
                return today;
            }
        },
        created: function(){
            window.context.Statistics.LoadTimespans(this.PrepareForm);
        },
        mounted: function(){
            let c = document.getElementById("s_canvas");
            c.width = 900;
            c.height = 550;
        }
    }

    function prepareData(data){
        let labels = [];
        let values = [];
        let genreKeys = Object.keys(data[0].GenreAmounts);

        for(let i = 0; i < genreKeys.length; i++){
            values[i] = [];
        }

        for(let i = 0; i < data.length; i++){
            labels[i] = data[i].KeyString;
            for(let y = 0; y < genreKeys.length; y++){
                console.log(i + " " + y);
                values[y][i] = (Math.round((data[i].GenreAmounts[genreKeys[y]] / data[i].DateTotal * 100) * 10) / 10);
            }
        }

        console.log({labels: labels, values: values});
        return {labels: labels, values: values};
    }
</script>