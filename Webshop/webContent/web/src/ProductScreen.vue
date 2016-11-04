<template>
    <div id="games_overview">
        <h1>Nieuwe games</h1>
        <div id="popular_games" class="spotlight_games"><!-- Start spotlight games -->
            <div class="three columns product" v-for="product in products">
                <pre>{{product}}</pre>
                <div class="product_title">{{ product.GameTitle }}</div>
                Aantal spelers: {{product.MinPlayers}} - {{product.MaxPlayers}}
                Genre: {{product.Genres.name}} <br />
                <span class="product_price">EUR {{product.Price / 100}} </span>
                <a v-bind:href="'#product/' + product.EAN">Bekijk meer</a>
                <!--<img v-bind:src="product.Image" alt="">-->
            </div>
        </div><br /><!-- End spotlight games -->
    </div>
</template>

<script>
    export default{
        data: function(){
            return{
                products:[]
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
                    console.log(base.products);
                };

                xhr.send();
            }
        },
        created: function(){
            this.getData();
        }
    }
</script>