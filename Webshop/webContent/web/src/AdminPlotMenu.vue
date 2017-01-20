<template>
    <div>
        <div style="margin: 0 auto;" v-if="!plot_active">
            <h1>Plot menu</h1>
            <button @click="ShowPlot(1)">Plot 1</button><br>
            <button @click="ShowPlot(2)">Plot 2</button><br>
            <button @click="ShowPlot(3)">Plot 3</button><br>
            <button @click="CloseMenu">Close</button>
        </div>
        <adminplot1 v-on:closed="ClosePlot" v-if="plot_number == 1"></adminplot1>
        <adminplot2 v-on:closed="ClosePlot" v-if="plot_number == 2"></adminplot2>
        <adminplot3 v-on:closed="ClosePlot" v-if="plot_number == 3"></adminplot3>
    </div>
</template>
<script>
    export default{
        data(){
            return {plot_active: false, plot_number: null}
        },
        methods:{
            CloseMenu: function(){
                this.$emit('close_plot_menu');
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
