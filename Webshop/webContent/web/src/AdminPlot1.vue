<template>
    <div class="container margin-top">
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
                        Start datum: <input type="date" name="StartDate"><br>
                        Eind datum: <input type="date" name="EndDate"><br>
                    </fieldset>
                </div>
                <div class="three columns statistic_fill_div">
                    <button v-on:click.prevent="LoadOrderAmountStatistics" type="submit" name="isSubmitted" class="statistic_send_button">Send</button>
                    <button @click="$emit('closed')">Close</button>
                </div>
            </form>
        </div>
    </div>
</template>
<script src="Chart.js"></script>
<script>
    export default{
        data(){
            return {timespans: null}
        },
        methods:{
            LoadTimespans: function () {
                var base = this;
                var xhr = new XMLHttpRequest();
                xhr.open("GET", "/api/AdminStatistics/TimeSpans/");
                xhr.setRequestHeader('Content-type', "Application/JSON", true);
                xhr.onload = function(){
                    console.log(JSON.parse(xhr.response));
                    base.timespans = JSON.parse(xhr.response);
                }
                xhr.send("");
            },
            LoadOrderAmountStatistics: function(){
                var elements = document.getElementsByName("TimeSpan");
                var timeSpan = "";
                for(var i = 0; i < elements.length; i++){
                    if(elements[i].checked){
                        timeSpan = elements[i].value;
                    }
                }
                var startTime = new Date(document.getElementsByName("StartDate")[0].value);
                var endTime = new Date(document.getElementsByName("EndDate")[0].value);

                var xhr = new XMLHttpRequest();
                xhr.open("POST", "/api/AdminStatistics/SalesAmountStatistics/");
                xhr.setRequestHeader('Content-type', "Application/JSON", true);
                xhr.onload = function(){
                    console.log(JSON.parse(xhr.response));
                }
                console.log(JSON.stringify({TimeSpan:timeSpan , BeginDate:startTime, EndDate:endTime}));
                xhr.send(JSON.stringify({TimeSpan:timeSpan , BeginDate:startTime, EndDate:endTime}));
            }
        },
        created: function () {
            this.LoadTimespans();
        }
    }
</script>