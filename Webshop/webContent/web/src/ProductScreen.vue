<template>
    <div>
        <div id="games_overview" v-if="showProducts">
            <h1>Nieuwe games</h1>
            <productbox @show_details="show_details" v-for="product in products" v-bind:product="product" v-bind:user_status="user_status" v-bind:have_in_wishlist="already_own_game(product.EAN, 'Wish List')"></productbox>
            <br class="clear"/><!-- End spotlight games -->
        </div>
    </div>
</template>

<script>
    export default{
        props:['user_status'],
        data: function(){
            return{
                products:[],
                showProductDetails: false,
                showProducts: true,
                HaveGameResultWish: false
            }
        },
        methods: {
            getData: function () {
                var base = this;

                var xhr = new XMLHttpRequest();
                xhr.open("GET", "/api/product/all");

                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    base.products = JSON.parse(xhr.response);
                };

                xhr.send();

                if(this.products.count > 0)
                {
                    this.showProductDetails = true;
                }
                else
                {
                    this.showProductDetails = false;
                }
            },
            already_own_game:function(product, listTitle)
            {
                alert("Product Title: " + product.EAN);
                var base = this;
                var xhr = new XMLHttpRequest();
                xhr.open("POST", "/api/user/AlreadyHaveGame/");
                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);
                var gameInformation = {EAN:product.EAN, TitleOfList:listTitle};
                xhr.send(JSON.stringify(gameInformation));
                
               return true;
                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    if(JSON.parse(xhr.response) != null && JSON.parse(xhr.response) == true)
                    {
                        //console.log("Response is true: " + this.product.EAN);
                        if(listTitle = "Wish List")
                        {
                            console.log("True: Wish");
                            base.HaveGameResultWish = true;
                        }
                        else
                        {
                            console.log("True: Fav");
                            base.HaveGameResultFav = true;
                        }
                    }
                    else
                    {
                        //console.log("Response is null or false: " + this.product.EAN);
                        if(listTitle = "Wish List")
                        {
                            console.log("False: Wish");
                            base.HaveGameResultWish = false;     
                        }
                        else
                        {
                            console.log("False: Fav");
                            base.HaveGameResultFav = false;
                        }
                    }
                }
                return false;
            },
            show_details: function (game) {
                this.$emit('show_details', game)
            }

        },
        created: function(){
            this.getData();
        },
        close: function(){
            this.$emit('close');
        }
    }
</script>