<template>
    <div class="three columns product">
        <div style="margin: 0 auto;">
            <span class="productbox_gametitle" v-bind:title="product.GameTitle">{{product.GameTitle}} <i style="float: right;" class="fa fa-heart-o" aria-hidden="true"></i><i @click="add_to_wishlist(product.EAN)" style="float: right;" class="fa fa-star-o fa-1x" aria-hidden="true"></i></span>
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
                // GameTitle = null
            }
        },

        methods: {
            add_to_cart:function () {
                window.context.ShoppingCart.addToCart(this.product);
            },
            show_details:function () {
                this.$emit('show_details', this.product)
            },
            add_to_wishlist:function (ean) {
                var base = this;
                var xhr = new XMLHttpRequest();
                xhr.open("POST", "/api/user/AddToList/");

                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                var gameInformation = {EAN:ean, TitleOfList:'Wish List'};
                xhr.send(JSON.stringify(gameInformation));
                console.log("EAN is:" + ean);

                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    if(JSON.parse(xhr.response) != null){
                        base.MyLists = JSON.parse(xhr.response);
                        console.log(base.MyLists);
                    }
                }
            },
            add_to_favoritelist:function (ean) {
                console.log(`Gevonden: ${ean}`);
            },
        }
    }
</script>