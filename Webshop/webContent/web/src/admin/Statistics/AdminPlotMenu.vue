<template>
    <div>
        <div style="margin: 0 auto;" v-if="!plot_active">
            <h1>Plot menu</h1>
            <button class="admin_button button-primary line_break" @click="ShowPlot(1)">Verkoopaantal statistieken</button><br>
            <button class="admin_button button-primary line_break" @click="ShowPlot(2)">Populaire catagorien in tijdsframe</button><br>
            <button class="admin_button button-primary line_break" @click="ShowPlot(3)">Meest gewilde games</button><br>
            <button class="admin_button button-primary line_break" @click="CloseMenu">Close</button>
        </div>
        <statistics_plot_1 v-on:closed="ClosePlot" v-if="plot_number == 1"></statistics_plot_1>
        <statistics_plot_2 v-on:closed="ClosePlot" v-if="plot_number == 2"></statistics_plot_2>
        <statistics_plot_3 v-on:closed="ClosePlot" v-if="plot_number == 3"></statistics_plot_3>
    </div>
</template>
<script>
    export default{
        data(){
            return {plot_active: false, plot_number: null}
        },
        methods:{
            CloseMenu: function(){
                this.$emit('close');
            },
            ShowPlot: function(plot_number){
                this.plot_number = plot_number;
                this.plot_active = true;
            },
            ClosePlot: function(){
                this.plot_active = false;
                this.plot_number = null;
            }
        }
    }
</script>
