<template>
    <div>
        <h1>Meest gewilde games</h1>
        <div class="row">
            <form>
                <div class="five columns statistic_fill_div">
                    <fieldset>
                        <legend>Aantal zichtbare spellen</legend>
                        <input type="number" name="Amount" class="statistics_text" :value="item" v-model="amount"><br>
                    </fieldset>
                </div>
                <div class="four columns statistic_fill_div">
                    <fieldset>
                        <legend>Filter op genres</legend>
                        <select class="genre_select" title="De genres waarop gefilterd moet worden." :size="genres.length" v-model="selected_genres" multiple>
                            <option v-for="genre in genres" :value="genre">{{genre}}</option>
                        </select>
                    </fieldset>
                </div>
            </form>
            <div class="three columns statistic_fill_div">
                <button @click="loadGenreStatistics" name="isSubmitted" class="statistic_send_button">Send</button>
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
            return{amount:0, genres:[],selected_genres:[]}
        },
        methods:{
            loadGenreStatistics: function(){
                if(!this.amount){
                    alert("Het aantal moet groter dan nul zijn.");
                    return;
                }

                window.context.Statistics.LoadWishListStatistics(this.amount, this.selected_genres, this.showWishListStatistics);
            },

            showWishListStatistics: function(data){
                console.log(data);
                let pr = prepareData(data);

                Chart.defaults.global.elements.rectangle.backgroundColor = "rgba(75,192,192,0.4)";
                Chart.defaults.global.elements.rectangle.borderColor = "rgba(75,192,192,1)";
                Chart.defaults.global.elements.point.borderWidth = 1;
                Chart.defaults.global.elements.point.hoverRadius = 5;
                Chart.defaults.global.elements.point.hoverBorderWidth = 2;
                Chart.defaults.global.elements.point.radius = 1;
                Chart.defaults.global.elements.point.hitRadius = 10;

                let ctx = document.getElementById("s_canvas");
                let datasetModel = {
                    label:pr.labels,
                    data:pr.values
                };

                let myChart = new Chart(ctx, {
                    type: 'line',
                    data: datasetModel
                });
            },

            prepareGenres(data){
                let temp = [];
                for(let i = 0; i < data.length; i++) {
                    console.log(data[i].Name);
                    temp[i] = data[i].Name;
                }
                this.genres = temp;
            },
        },

        created: function(){
            window.context.Statistics.LoadGenres(this.prepareGenres);
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


        for(let i = 0; i < data.length; i++){
            labels[i] = data[i].GameTitle;
            values[i] = data[i].Count;
        }

        console.log({labels: labels, values: values});
        return {labels: labels, values: values};
    }
</script>