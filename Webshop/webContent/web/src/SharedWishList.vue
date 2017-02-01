<template>
    <div v-if="showProducts">
        <h1>wish list</h1>
        <div>
            <table class="u-full-width">
                <thead>
                    <tr>
                        <!--<th><img  class="thumbnail" v-bind:src="product.Image" alt=""></th>-->
                        <thead>
                            <th>Afbeelding</th>
                            <th>Titel</th>
                            <th>Prijs</th>
                            <th>Platform</th>
                        </thead>
                        <tbody v-for="product in products.Games">
                            <tr>
                                <td><img class="thumb" v-bind:src="product.Image" alt=""></td>
                                <td>{{ product.GameTitle }}</td>
                                <td>&euro;{{(product.Price / 100).toFixed(2)}} </td>
                                <td>{{ product.Platform.PlatformTitle }}</td>
                                <td><a class="button productbox_button" @click.prevent="add_to_cart(product)" href="#">Voeg toe</a></td>
                            </tr>
                        </tbody>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
</template>

<script>
    export default{
        props: [],
        data: function() {
            return {
                products: [],
                showProducts: false
            }
        },
        methods:
        {
            add_to_cart:function (product) {
                window.context.ShoppingCart.addToCart(product);
            },
             parseQueryString: function() {
                var str = window.location.search;
                var objURL = {};

                str.replace(
                    new RegExp( "([^?=&]+)(=([^&]*))?", "g" ),
                    function( $0, $1, $2, $3 ){
                        objURL[ $1 ] = $3;
                    }
                );
                return objURL;
            }
        },
        created: function(){
        //     var searchParams = {};
        //     window.location.search.substr(1).split("&").forEach(function(item) {searchParams[item.split("=")[0]] = item.split("=")[1]});
        //    // var some = window.location.search;
        //     //console.log(some);
            var params = this.parseQueryString();
            var id = params["id"];
            if(id !== undefined){
                var xhr = new XMLHttpRequest();

                xhr.open('GET', `/api/user/sharedwishlist/?id=${id}`);
                xhr.setRequestHeader('Content-type', 'Application/JSON', true);

                var base = this;

                xhr.onload = function(){
                    base.products = JSON.parse(xhr.response);
                    base.showProducts = true;
                };

                xhr.send();
            }

        }
    }
</script>

<style>
    .thumb{
        width:150px;
        height:150px;
    }
</style>