<template>
    <div class="container margin-top"><!-- Start container -->
        <div class="container"></div>
        <div class="ten columns"><h1 align="center">Jouw winkelwagen.</h1></div>
        <div class="ten columns">
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
                    <tr v-for="cartLine in cart.CartLines" >
                        <td><img class="img-thumbnail thumbnail" :src="cartLine.Product.Image[0]"></td>
                        <td>{{cartLine.Product.GameTitle}}</td>
                        <td><input v-model="cartLine.Amount" type="number" min="0" max="20" @change="update"></td>
                        <td>{{(cartLine.SubTotal/100).toFixed(2)}}</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td><strong>Totaal:</strong> &euro;{{(cart.TotalPrice/100).toFixed(2)}}</td>
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
                console.log('test');
            },
            update:function () {
                window.shoppingcart.UpdateCart(this.cart);
            }
        },
        created: function () {
            window.shoppingcart.registerOnChangedshoppingCart(this.changedShoppingCart);
            this.changedShoppingCart(window.shoppingcart)
        }
    }
</script>
