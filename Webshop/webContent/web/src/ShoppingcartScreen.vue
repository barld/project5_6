<template>
    <div class="container margin-top"><!-- Start container -->
        <div class="ten columns">
            <h1 align="center">Jouw winkelwagen.</h1>
            <table class="u-full-width">
                <thead>
                    <tr>
                        <th>&nbsp;</th>
                        <th>Naam</th>
                        <th width="30">Aantal</th>
                        <th width="230">Prijs</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="cartline in cart.CartLines">
                        <td><img class="table_thumbnail" v-bind:src="cartline.Product.Image[0]"></td>
                        <td>{{cartline.Product.GameTitle}}</td>
                        <td><input type="number" v-model="cartline.Amount" @change="update" min="0" max="20"></td>
                        <td>€{{((cartline.SubTotal/100)).toFixed(2)}}
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td><strong>Totaal:</strong> €{{(cart.TotalPrice/100).toFixed(2)}}</td>
                    </tr>
                </tbody>
            </table>
            <div class="form-btn" align="right">
                <input type="submit" align="right" class="button-primary" value="Proceed to checkout!" @click="close">
            </div>
        </div>
    </div><!-- End container -->
</template>
<script>
    export default{
        props:["shoppingcart"],
        data(){
            return{
                cart: {}
            }
        },
        methods: {
            close:function(){
                this.$emit("close");
            },
            changedShoppingCart:function (sc) {
                this.cart = sc.cart;
            },
            update:function () {
                window.context.ShoppingCart.UpdateCart(this.cart);
            }
        },
        created: function () {
            window.context.ShoppingCart.registerOnChangedshoppingCart(this.changedShoppingCart);
            this.changedShoppingCart(window.shoppingcart)
        }
    }
</script>
