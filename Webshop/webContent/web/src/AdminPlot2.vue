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
                        Start datum: <input type="date" name="StartDate" :value="today" v-model="begin_date"><br>
                        Eind datum: <input type="date" name="EndDate" :value="today" v-model="end_date"><br>
                    </fieldset>
                </div>
            </form>
            <div class="three columns statistic_fill_div">
                <button @click="LoadGenreStatistics" name="isSubmitted" class="statistic_send_button">Send</button>
                <button @click="$emit('closed')">Close</button>
            </div>
        </div>
        <div class="statistics_canvas" id="s_canvas_div">
            <canvas id="s_canvas"></canvas>
        </div>
    </div>
</template>
<script>
    import LineChartDrawer from './BarChartDrawer'

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
        }
    }
</script>