<template>
        <div class="row">
            <div class="twelve columns">
                <div class="panel">
                    <user_admin_screen></user_admin_screen>
                    <admin_products v-show="productsLoaded" :products="products"></admin_products>
                    <admin_add_products v-show="addProducts" :platforms="platforms" :publishers="publishers" :genres="genres"></admin_add_products>
                    <admin_add_platform :show="showAddPlatform"></admin_add_platform>
                    <admin_add_genre :show="showAddGenre"></admin_add_genre>
                    <a href="#addGenre" @click="showGenresAddMenu">Genre toevoegen</a>
                    <a href="#addPlatform" @click="showPlatformsAddMenu">Platform toevoegen</a>
                    <a href="#addProduct" @click="showProductsAddMenu">Maak een nieuw product</a>
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
                addProducts: false,
                editProducts: false,
                showAddGenre: false,
                showAddPlatform: false,
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
            },
            showGenresAddMenu: function(){
                this.showAddGenre = true;
            },
            showPlatformsAddMenu: function(){
                this.showAddPlatform = true;
            }

        },
        created: function(){
            this.getAllProducts();
            this.getAllPlatforms();
            this.getAllGenres();
        }
    }
</script>