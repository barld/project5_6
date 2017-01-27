<template>
    <detail_screen v-if="show_detail" v-on:account="show_account_page" v-bind:order="order_to_show"></detail_screen>
    <div v-else> 
        <div><!-- Start container -->
            <div class="container">
                <div class="row">
                    <div class="ten.columns offset-by-two-columns">
                        <h1 align="center">Account</h1>
                    </div>
                </div>
            </div>
            <div class="container">
                <div class="row">
                    <div class="six columns">
                        <div class="element-container">
                            <h3 align="center">Mijn Lijsten</h3>
                            <hr>
                            <h1 align="center">=(</h1>

                        </div>
                        <br>
                        <button @click="product" class="button-primary">Bekijk Producten</button>
                    </div>

                    <div class="six columns">
                        <div class="element-container">
                            <h3 align="center">Geschiedenis</h3>
                            <hr>
                            <table class="u-full-width">
                                <thead>
                                <tr>
                                    <th width="35%">Bestelling Nummer</th>
                                    <th width="65%">Details</th>
                                </tr>
                                </thead>
                                <tbody>
                                <tr v-for="order in orders">
                                    <td>{{order.OrderNumber}}</td>
                                    <td><button @click="detail(order)" class="button-primary">Details!</button></td>
                                </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div><!-- End container -->
    </div>
</template>

<script>
    export default{
        data:function(){
            return{
                orders: [],
                show_detail: false,
                order_to_show: {}
            }
        },
        methods:{
            detail:function(order)
            {
                this.order_to_show = order;
                this.show_detail = true;
            },
            product:function(){
                this.$emit("product");
            },
            show_account_page:function(){
                this.show_detail = false;
            }
        },
        created: function() {window.context.Order.GetOrders(orders => this.orders = orders);}
    }
</script>
