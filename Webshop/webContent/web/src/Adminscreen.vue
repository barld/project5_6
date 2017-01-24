<template>
    <div>
        <div v-if="show_admin_screen">
            <h1 align="center">Admin Panel</h1>
            <div class="row">
                <div class="ten.columns offset-by-two-columns">
                    <div class="panel">
                        <table style="margin-top: 20px;" class="u-full-width">
                            <thead>
                                <th style="width: 40%">Username</th>
                                <th style="width: 20%">Gender</th>
                                <th style="width: 20%">Edit</th>
                                <th style="width: 20%; margin-right: 10px">Delete</th>
                            </thead>
                            <tbody>
                                <tr>
                                    <td><p>JFConstancia@hotmail.com</p></td>
                                    <td><p>Male</p></td>
                                    <td><a href="#"><button class="button-primary">Edit</button></a></td>
                                    <td><a href="#"><button class="button-primary">Delete</button></a></td>
                                </tr>
                                <tr>
                                    <td><p>info@superict.nl</p></td>
                                    <td><p>Male</p></td>
                                    <td><a href="#"><button class="button-primary">Edit</button></a></td>
                                    <td><a href="#"><button class="button-primary">Delete</button></a></td>
                                </tr>
                                <tr>
                                    <td><p>hallo@420.com</p></td>
                                    <td><p>Female</p></td>
                                    <td><a href="#"><button class="button-primary">Edit</button></a></td>
                                    <td><a href="#"><button class="button-primary">Delete</button></a></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <h1>Admin plots</h1>
            <button class="plotbutton" @click="ShowPlot(1)">Verkoopaantal statistieken</button><br>
            <button class="plotbutton" @click="ShowPlot(2)">Populaire catagorien in tijdsframe</button><br>
            <button class="plotbutton" @click="ShowPlot(3)">Geen idee</button><br>
        </div>
        <adminplot1 v-on:closed="ClosePlots" v-if="plot_number == 1"></adminplot1>
        <adminplot2 v-on:closed="ClosePlots" v-if="plot_number == 2"></adminplot2>
        <adminplot3 v-on:closed="ClosePlots" v-if="plot_number == 3"></adminplot3>
    </div>
</template>

<script>
    export default{
        data:function(){
            return{
                show_admin_screen: true,
                show_admin_plot_menu: false,
                plot_number: 0
            }
        },
        methods:{
            ShowPlot:function(val){
                if(Number.isInteger(val) && val > 0 && val < 4){
                    this.show_admin_screen = false;
                    this.plot_number = val;
                }
            },
            ClosePlots:function(){
                this.show_admin_screen = true;
                this.plot_number = 0;
            }
        }
    }
</script>