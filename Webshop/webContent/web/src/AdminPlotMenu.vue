<template>
    <div class="container margin-top">
        <div style="margin: 0 auto;" v-if="plot_active == false">
            <h1>Plot menu</h1>
            <button @click="ShowPlot1">Plot 1</button><br>
            <button @click="ShowPlot2">Plot 2</button><br>
            <button @click="ShowPlot3">Plot 3</button><br>
            <button @click="CloseMenu">Close</button>
        </div>
        <div>

        </div>
    </div>
</template>
<script>
    export default{
        data(){
            plot_active = false;
        },
        methods:{
            CloseMenu: function(){
                this.$emit('close');
            },
            ShowPlot1: function(){

            },
            ShowPlot2: function(){

            },
            ShowPlot3: function(){

            }
        }
    }
</script>
