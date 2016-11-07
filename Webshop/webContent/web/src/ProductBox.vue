<template>
    <div class="three columns product">
        Aantal spelers: {{product.MinPlayers}} - {{product.MaxPlayers}}
        <span class="product_price">&euro; {{product.Price / 100}} </span>
        <a v-bind:href="'#product/' + product.EAN" @click="show_details">Bekijk meer</a>
        <img v-bind:src="product.Image[0]" alt="" class="thumbnail">
        <a @click.prevent="add_to_cart" href="#">add to cart</a>
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