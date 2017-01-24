<template>
        <div class="row">
            Hoi
            <div class="twelve columns">
                <div class="panel">
                    <user_admin_screen></user_admin_screen>
                    <admin_products v-show="productsLoaded" :products="products"></admin_products>
                    <admin_add_products v-show="addProducts" :platforms="platforms" :publishers="publishers" :genres="genres"></admin_add_products>
                    <a href="#" @click="showProductsAddMenu">Maak een nieuw product</a>
                </div>
            </div>
        </div>
</template>

<script>
    export default{
        data: function(){
            return{
                products:[],
                platforms:[],
                publishers:[],
                genres:[],
                productsLoaded: false,
                addProducts: false
            }
        },
        methods:{
            hideProducts: function(){
              this.productsLoaded = false;
            },
            getAllProducts: function(){
                var base = this;
                var xhr = new XMLHttpRequest();

                xhr.open("GET", "/api/product/all");
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                xhr.onload = function () {
                    base.products = JSON.parse(xhr.response);
                    base.productsLoaded = true;
                };

                xhr.send();
            },
            getAllPlatforms: function(){
                var base = this;
                var xhr = new XMLHttpRequest();

                xhr.open("GET", "/api/platform/all");
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                xhr.onload = function () {
                    base.platforms = JSON.parse(xhr.response);
                };

                xhr.send();
            },
            getAllPublishers: function(){
                var base = this;
                var xhr = new XMLHttpRequest();

                xhr.open("GET", "/api/publishers/all");
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                xhr.onload = function () {
                    base.publishers = JSON.parse(xhr.response);
                };

                xhr.send();
            },
            getAllGenres: function(){
                var base = this;
                var xhr = new XMLHttpRequest();

                xhr.open("GET", "/api/genre/all");
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                xhr.onload = function () {
                    base.genres = JSON.parse(xhr.response);
                };

                xhr.send();
            },
            showProductsAddMenu: function(){
                this.addProducts = true;
                this.productsLoaded = false;
            }
        },
        created: function(){
            this.getAllProducts();
            this.getAllPlatforms();
            this.getAllGenres();
            //this.getAllPublishers();
        }
    }
</script>