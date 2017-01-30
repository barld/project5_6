<template>
    <div>
        <div id="games_overview" v-if="showProducts">
            <h1>Nieuwe games</h1>
            <productbox @show_details="show_details" v-for="product in products" v-bind:product="product" v-bind:user_status="user_status"></productbox>
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
                    let products = JSON.parse(xhr.response);
                    products.forEach(product => product.HaveGameResultWish = false);
                    products.forEach(product => product.HaveGameResultFav = false);
                    products.forEach(product => base.already_own_game(product, "Wish List"));
                    products.forEach(product => base.already_own_game(product, "Favourite List"));
                    base.products = products;
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
                var base = this;
                var xhr = new XMLHttpRequest();
                xhr.open("POST", "/api/user/AlreadyHaveGame/");
                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", false);
                var gameInformation = {EAN:product.EAN, TitleOfList:listTitle};
                xhr.send(JSON.stringify(gameInformation));
                
            //    return true;
                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    if(JSON.parse(xhr.response) != null && JSON.parse(xhr.response) == true)
                    {
                        //console.log("Response is true: " + this.product.EAN);
                        if(listTitle == "Wish List")
                        {
                            console.log("True: Wish");
                            product.HaveGameResultWish = true;
                        }
                        else
                        {
                            console.log("True: Fav");
                            product.HaveGameResultFav = true;
                        }
                    }
                    else
                    {
                        //console.log("Response is null or false: " + this.product.EAN);
                        if(listTitle == "Wish List")
                        {
                            console.log("False: Wish");
                            product.HaveGameResultWish = false;     
                        }
                        else
                        {
                            console.log("False: Fav");
                            product.HaveGameResultFav = false;
                        }
                    }
                }
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