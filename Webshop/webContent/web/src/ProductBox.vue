<template>
    <div class="three columns product">
        <div style="margin: 0 auto;">
            <span class="productbox_gametitle" v-bind:title="product.GameTitle">
                {{product.GameTitle}}<i v-bind:class="{'fa fa-heart': HaveGameResultFav, 'fa fa-heart-o': !HaveGameResultFav}" @click="add_to_list(product.EAN, 'Favourite List')" v-if="user_status" style="float: right;" aria-hidden="true"></i><i v-if="checkUserLoggedIn()" @click="add_to_list(product.EAN, 'Wish List')" style="float: right;" v-bind:class="{'fa fa-star fa-1x': have_in_wishlist, 'fa fa-star-o fa-1x': !have_in_wishlist}" aria-hidden="true"></i>
            </span>
            <!--<span v-bind:class="{productbox_platformtitle}" v-bind:title="product.Platform.PlatformTitle">({{product.Platform.PlatformTitle}})</span>-->
            <span v-bind:title="product.Platform.PlatformTitle">({{product.Platform.PlatformTitle}})</span>
            <div class="thumbnail_container">
                <img v-if="product.Image !== null" v-bind:src="product.Image[0]" alt="" class="thumbnail">
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
        props:['product', 'user_status', 'have_in_wishlist'],
        data: function(){
            return{
                // GameTitle = null
                AddToListResult: false,
                HaveGameResultWish: false,
                HaveGameResultFav: false,
            }
        },

        methods: {
            add_to_cart:function () {
                window.context.ShoppingCart.addToCart(this.product);
            },
            show_details:function () {
                this.$emit('show_details', this.product)
            },
            add_to_list:function (ean, listTitle) {
                var base = this;
                var xhr = new XMLHttpRequest();
                xhr.open("POST", "/api/user/AddToList/");

                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                var gameInformation = {EAN:ean, TitleOfList:listTitle};
                xhr.send(JSON.stringify(gameInformation));

                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    if(JSON.parse(xhr.response) != null)
                    {
                        var result = JSON.parse(xhr.response);
                        if(result)
                        {
                            base.AddToListResult = true;
                        }
                        else
                        {
                            base.AddToListResult = false;
                        }
                    }
                    else
                    {
                        console.log("Response is null");
                    }
                }
            },
            checkUserLoggedIn:function()
            {
                if(this.user_status != null)
                {
                    if(this.user_status.Email)
                        return true;
                }
                return false;
            }
        }
    }
</script>