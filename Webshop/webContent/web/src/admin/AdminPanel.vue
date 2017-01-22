<template>
        <div class="row">
            <h1>Test</h1>
            <div class="twelve columns">
                <div class="panel">
                    <admin_products v-show="productsLoaded" :products="products"></admin_products>
                </div>
            </div>
        </div>
</template>

<script>
    export default{
        data: function(){
            return{
                products:[],
                productsLoaded: true
            }
        },
        methods:{
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
            }
        },
        created: function(){
            this.getAllProducts();
        }
    }
</script>