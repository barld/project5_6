<template>
    <div class="three columns product">
        <div style="margin: 0 auto;">
            <span class="productbox_gametitle"v-bind:title="product.GameTitle">{{product.GameTitle}}</span>
            <span class="productbox_platformtitle">({{product.Platform.PlatformTitle}})</span>
            <div class="thumbnail_container">
                <img v-bind:src="product.Image[0]" alt="" class="thumbnail">
            </div>
            <span class="product_price"><br>&euro; {{(product.Price / 100).toFixed(2)}} </span><br>
            <a v-bind:href="'#product/' + product.EAN" @click="show_details">Details</a>
            <a class="button" @click.prevent="add_to_cart" href="#"><i class="fa fa-shopping-cart" aria-hidden="true"></i> Voeg toe</a>
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
                window.shoppingcart.addToCart(this.product);
            },
            show_details:function () {
                this.$emit('show_details', this.product)
            }
        }
    }
</script>