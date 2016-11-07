<template>
    <div>
        <div id="games_overview">
            <h1>Nieuwe games</h1>
            <productbox @show_details="show_details" v-for="product in products" v-bind:product="product"></productbox>
            <br class="clear"/><!-- End spotlight games -->
        </div>
        <div v-show="showProductDetails" class="popup" ><!-- Start login screen -->
            <div class="close_btn" @click="closeProductDetails">
                <i class="fa fa-times fa-5x" aria-hidden="true"></i>
            </div>
            <div class="inner_padding">
                {{ product }}
            </div>
        </div>
    </div>
</template>

<script>
    export default{
        data: function(){
            return{
                products:[],
                product: [],
                showProductDetails: false
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
            },
            productDetails: function(game){
                this.product = game;
                this.showProductDetails = true;
            },
            closeProductDetails: function(){
                this.showProductDetails = false;
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