<template>
    <div class="three columns product">
        <div style="margin: 0 auto;">
            <span class="productbox_gametitle" v-bind:title="product.GameTitle">{{product.GameTitle}}</span>
            <span class="productbox_platformtitle" v-bind:title="product.Platform.PlatformTitle">({{product.Platform.PlatformTitle}})</span>
            <div class="thumbnail_container">
                <img v-bind:src="product.Image[0]" alt="" class="thumbnail">
            </div>
            <div class="centerbox">
                <a v-bind:href="'#product/' + product.EAN" @click="show_details">Bekijk details</a>
                <span class="product_price"><br>&euro;{{(product.Price / 100).toFixed(2)}} </span>
                <a class="button productbox_button" @click.prevent="add_to_cart" href="#">Voeg toe</a>
            </div>
        </div>
    </div>
</template>

<script>
    export default{
        props:['product'],
        data: function(){
            return{
            }
        },

        methods: {
            add_to_cart:function () {
                window.context.ShoppingCart.addToCart(this.product);
            },
            show_details:function () {
                this.$emit('show_details', this.product)
            }
        }
    }
</script>