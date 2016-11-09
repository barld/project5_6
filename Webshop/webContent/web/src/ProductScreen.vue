<template>
    <div>
        <div id="games_overview" v-if="showProducts">
            <h1>Nieuwe games</h1>
            <productbox @show_details="show_details" v-for="product in products" v-bind:product="product"></productbox>
            <br class="clear"/><!-- End spotlight games -->
        </div>
    </div>
</template>

<script>
    export default{
        data: function(){
            return{
                products:[],
                showProductDetails: false,
                showProducts: true
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