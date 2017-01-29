<template>
    <div><!-- Start container -->
        <div class="container">
            <div class="row">
                <div class="ten.columns offset-by-two-columns">
                    <h1 align="center">Bestelling nummer {{order.OrderNumber}}</h1>
                </div>
            </div>
        </div>
        <div class="container">
            <div class="row">
                <div class="six columns">
                    <div class="element-container">
                        <h3 align="center">Adres</h3>
                        <hr>
                        <table class="u-full-width">
                            <tbody>
                            <tr>
                                <td><strong>Land</strong></td>
                                <td>{{order.DeliveryAddress.Country}}</td>
                            </tr>
                            <tr>
                                <td><strong>Stad</strong></td>
                                <td>{{order.DeliveryAddress.City}}</td>
                            </tr>
                            <tr>
                                <td><strong>Postcode</strong></td>
                                <td>{{order.DeliveryAddress.PostalCode}}</td>
                            </tr>
                            <tr>
                                <td><strong>Straat</strong></td>
                                <td>{{order.DeliveryAddress.Streetname}}</td>
                            </tr>
                            <tr>
                                <td><strong>Huisnummer</strong></td>
                                <td>{{order.DeliveryAddress.Housenumber}}</td>
                            </tr>
                            </tbody>
                        </table>
                    </div>
                </div>

                <div class="six columns">
                    <div class="element-container">
                        <h3 align="center">Bestelling</h3>
                        <hr>
                        <table style="margin-left: 10px; margin-right: 15px;" class="u-full-width">
                            <thead>
                            <tr>
                                <th width="25%">&nbsp;</th>
                                <th width="30%">Naam</th>
                                <th width="20%">Aantal</th>
                                <th width="25%">Prijs</th>
                            </tr>
                            </thead>
                            <tbody>
                            <tr v-for="line in order.OrderLines">
                                <td><img width="50" class="table_thumbnail" v-bind:src="line.Game.Image[0]"></td>
                                <td>{{line.Game.GameTitle}}</td>
                                <td>{{line.Amount}}</td>
                                <td>€{{(line.Amount * ((line.Game.Price/100)).toFixed(2))}}</td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <button @click="account" align="center" class="button-primary">Terug Naar Account</button>
                                </td>
                            </tr>
                            </tbody>
                        </table>

                    </div>
                </div>
            </div>
        </div>
    </div><!-- End container -->
</template>

<script>
    export default{
        props:['order'],
        data:function(){
            return{
            }
        },
        methods:{
            account:function()
            {
                this.$emit("account");
            }
        }
    }
</script>
0
