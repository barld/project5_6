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
        props:['user_status','event_bus'],
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
                console.log("called getdata");
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
                    console.log("Retrieved game data");
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
                console.log("TEST");
            //    return true;
                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    if(JSON.parse(xhr.response) != null && JSON.parse(xhr.response) == true)
                    {
                        //console.log("Response is true: " + this.product.EAN);
                        if(listTitle == "Wish List")
                        {
                            product.HaveGameResultWish = true;
                        }
                        else
                        {
                            product.HaveGameResultFav = true;
                        }
                    }
                    else
                    {
                        //console.log("Response is null or false: " + this.product.EAN);
                        if(listTitle == "Wish List")
                        {
                            product.HaveGameResultWish = false;     
                        }
                        else
                        {
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
            console.log(this.event_bus);
            this.event_bus.$on('user_logged_in', function(){ this.getData();}.bind(this));
            this.event_bus.$on('user_logged_out', function(){ this.getData();}.bind(this));
            this.getData();
        },
        close: function(){
            this.$emit('close');
        }
    }
</script>